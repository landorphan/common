﻿// ReSharper disable once CheckNamespace
namespace Landorphan.TestUtilities
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;
   using System.Reflection;
   using System.Runtime.CompilerServices;
   using Landorphan.Common;
   using Landorphan.Common.Threading;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   /// <summary>
   /// Provides common services for BDD-style (context/specification) unit tests.  Serves as an adapter between the MSTest and  BDD-style tests when
   /// fields and auto-properties of the test class are owned and implement <see cref="IDisposable"/>.
   /// </summary>
   /// <remarks>
   /// <para>
   /// This class uses reflection to dispose of all fields and Auto-Properties in descendant classes provided those fields or auto-properties match the following requirements:
   ///   NOT decorated with [DoNotDispose]
   ///   Implements IDisposable or implements IEnumerable{IDisposable}
   /// Note:  This class does not handle IDictionary{TKey,TValue}.  When a descendant class has an "owned" IDictionary{TKey,TValue} or any other
   /// nested structure containing IDisposable resources, override the <see cref="DisposableObject.ReleaseManagedResources"/> method, calling the base implementation at the end of the override.
   /// </para>
   /// <para>
   /// Call <see cref="DisposableObject.ThrowIfDisposed"/> on member access as appropriate.
   /// </para>
   /// <para>
   /// The implementation of <see cref="IDisposable.Dispose"/> has been altered from the recommended pattern to make it thread-safe to call.
   /// </para>
   /// </remarks>
   [TestClass]
   [SuppressMessage("Microsoft.", "CA1063: Implement IDisposable Correctly", Justification = "Reviewed, deviates to be thread-safe and handle mutiple disposals (MWP)")]
   [SuppressMessage("SonarLint.CodeSmell", "S3881: IDisposable should be implemented correctly", Justification = "Reviewed, deviates to be thread-safe and handle mutiple disposals (MWP)")]
   public abstract class DisposableArrangeActAssert : ArrangeActAssert, INotifyingQueryDisposable
   {
      // eases maintenance
      private static readonly Type t_stopAtImplementationInheritanceType = typeof(DisposableArrangeActAssert);

      // this implementation is copied from Landorphan.Common.DisposableObject
      // C# gives one implementation inheritance, on balance, ArrangeActAssert is the essence.
      private readonly SourceWeakEventHandlerSet<EventArgs> _listenersDisposing = new SourceWeakEventHandlerSet<EventArgs>();

      private InterlockedBoolean _isDisposed = new InterlockedBoolean(false);
      private InterlockedBoolean _isDisposing = new InterlockedBoolean(false);

      /// <inheritdoc/>
      [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Reviewed (MWP)")]
      public void Dispose()
      {
         if (_isDisposed)
         {
            return;
         }

         if (_isDisposing.ExchangeValue(true))
         {
            return;
         }

         Dispose(true);
         _isDisposing = false;
         _isDisposed = true;

         // Use SuppressFinalize in case a subclass implements a finalizer.
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Releases the unmanaged resources used by the <see cref="DisposableObject"/> and optionally releases the managed resources.
      /// </summary>
      /// <param name="disposing">
      /// <c> true </c> to release both managed and unmanaged resources; <c> false </c> to release only unmanaged resources.
      /// </param>
      protected virtual void Dispose(Boolean disposing)
      {
         if (disposing)
         {
            // notify all listeners
            OnDisposing();

            // Clean up managed resources if disposing
            ReleaseManagedResources();
         }

         // Clean up native resources always
         ReleaseUnmanagedResources();
      }

      /// <summary>
      /// Finds and releases all managed resources.
      /// </summary>
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S134: Control flow statements ... should not be nested too deeply",
         Justification = "This method addresses the general problem of disposing, reviewed as acceptable (MWP)")]
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S1541: Methods and properties should not be too complex",
         Justification = "This method addresses the general problem of disposing, reviewed as acceptable (MWP)")]
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S3776:Cognitive Complexity of methods should not be too high",
         Justification = "This method addresses the general problem of disposing, reviewed as acceptable (MWP)")]
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S4056:Overloads with a 'CultureInfo' or an 'IFormatProvider' parameter should be used",
         Justification = "I see no value in applying a culture to a null value (MWP).")]
      protected virtual void ReleaseManagedResources()
      {
         // use reflection to find fields and AutoProperties that implement IDisposable, or are IEnumerable<IDisposable>
         // and not decorated with [DoNotDispose]
         // ..
         // dispose of the same and set the value to null.
         var derivedType = GetType();

         // loop over the implementation inheritance chain...
         while (derivedType != t_stopAtImplementationInheritanceType)
         {
            // ReSharper disable once PossibleNullReferenceException
            var fields = derivedType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
               var value = field.GetValue(this);
               if (value == null)
               {
                  // can ignore null fields in this context.
                  continue;
               }

               var fieldType = value.GetType();
               if (fieldType.IsValueType)
               {
                  // can ignore value type fields in this context.
                  continue;
               }

               var attributeTypesWorking =
                  from o in field.GetCustomAttributes(false)
                  select o.GetType();
               var fieldAttributeTypes = new HashSet<Type>(attributeTypesWorking);

               if (fieldAttributeTypes.Contains(typeof(DoNotDisposeAttribute)))
               {
                  // class designer expressly excluded this field from disposal
                  continue;
               }

               if (fieldAttributeTypes.Contains(typeof(CompilerGeneratedAttribute)))
               {
                  // this field is a compiler generated backing field for an auto-property, inspect the property for [DoNotDispose],
                  // otherwise, treat normally.
                  var propertyName = BackingFieldNameToAutoPropertyName(field.Name);
                  if (IsAutoPropertyDecoratedWithDoNotDispose(derivedType, propertyName))
                  {
                     // class designer expressly excluded the auto-property from disposal, ignore the backing field for the same.
                     continue;
                  }
               }

               // KNOWN:  this field is not decorated with [DoNotDispose]
               // KNOWN:  this field is not a backing field for an auto-property decorated with [DoNotDispose]
               // KNOWN:  this field is a non-null reference
               if (TryHandleSimpleDisposable(value))
               {
                  // the field has been disposed
                  // setting the field to null is not required but it costs next to nothing, and eases testing
                  field.SetValue(this, null);
               }
               else if (TryHandleEnumerableOfDisposables(value))
               {
                  // the field has been disposed
                  // setting the field to null is not required but it costs next to nothing, and eases testing
                  field.SetValue(this, null);
               }
               else if (TryHandleDictionaryOfDisposables(value))
               {
                  // the field has been disposed
                  // setting the field to null is not required but it costs next to nothing, and eases testing
                  field.SetValue(this, null);
               }
            }

            derivedType = derivedType.BaseType;
         }
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      protected virtual void ReleaseUnmanagedResources()
      {
         // no implementation by design
      }

      /// <summary>
      /// Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the <see cref="DisposableObject"/>.
      /// </summary>
      ~DisposableArrangeActAssert()
      {
         Dispose(false);
      }

      /// <inheritdoc/>
      public event EventHandler<EventArgs> Disposing
      {
         add => _listenersDisposing.Add(value);
         remove => _listenersDisposing.Remove(value);
      }

      /// <inheritdoc/>
      public Boolean IsDisposed => _isDisposed;

      /// <inheritdoc/>
      public Boolean IsDisposing => _isDisposing;

      /// <summary>
      /// Notifies all listeners that this instance is being disposed.
      /// </summary>
      protected virtual void OnDisposing()
      {
         _listenersDisposing.Invoke(this, EventArgs.Empty);
      }

      /// <summary>
      /// Throws an <see cref="ObjectDisposedException"/> if this instance has been disposed.
      /// </summary>
      protected void ThrowIfDisposed()
      {
         if (_isDisposed)
         {
            throw new ObjectDisposedException(GetType().Name);
         }
      }

      private String BackingFieldNameToAutoPropertyName(String fieldName)
      {
         // pattern:  <AutoPropertyName>k__BackingField
         var idxStart = fieldName.IndexOf('<');
         var idxEnd = fieldName.IndexOf('>');
         var propertyName = fieldName.Substring(idxStart + 1, idxEnd - idxStart - 1);
         return propertyName;
      }

      private Boolean IsAutoPropertyDecoratedWithDoNotDispose(Type type, String propertyName)
      {
         var rv = false;

         var property = type.GetProperty(propertyName);
         if (property != null)
         {
            var propertyAttributeTypesWorking =
               from o in property.GetCustomAttributes(false)
               select o.GetType();
            var propertyAttributeTypes = new HashSet<Type>(propertyAttributeTypesWorking);
            if (propertyAttributeTypes.Contains(typeof(DoNotDisposeAttribute)))
            {
               // this property is decorated with [DoNotDispose]
               rv = true;
            }
         }

         return rv;
      }

      [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Reviewed(MWP)")]
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S134: Control flow statements ... should not be nested too deeply",
         Justification = "This method addresses the general problem of disposing, reviewed as acceptable (MWP)")]
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S1541: Methods and properties should not be too complex",
         Justification = "This method addresses the general problem of disposing, reviewed as acceptable (MWP)")]
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S2221: Exception should not be caught when not required by called methods",
         Justification = "Reviwed (MWP)")]
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S3776:Cognitive Complexity of methods should not be too high",
         Justification = "This method addresses the general problem of disposing, reviewed as acceptable (MWP)")]
      [SuppressMessage("SonarLint.CodeSmell", "S4056: Overloads with a CultureInfo or an IFormatProvider parameter should be used", Justification = "Not displaying")]
      private Boolean TryHandleDictionaryOfDisposables(Object fieldValue)
      {
         // Handles:
         //    IDictionary<IDisposable,*>,
         //    IDictionary<IEnumerable<IDisposable>,*>,
         //    IDictionary<*, IDisposable>, and
         //    IDictionary<*, IEnumerable<IDisposable>>
         // DOES NOT handle:
         //    IDictionary<IDictionary<,>, *>, nor
         //    IDictionary<*, IDictionary<,>>, nor
         //    any other more other deeply nested structures,
         //    nor does it inspect the actual type of each instance of TKey or TValue, it only looks at TKey.GetType() and TValue.GetType()
         var rv = false;

         // assumes fieldValue has already been checked for null.
         var fieldType = fieldValue.GetType();
         if (fieldType.IsGenericType)
         {
            var genericType = fieldType.GetGenericTypeDefinition();
            var genericImplementsIDictionary = (
               from i in genericType.GetInterfaces()
               where typeof(IDictionary<,>).IsAssignableFrom(i)
               select i).Any();
            if (genericImplementsIDictionary)
            {
               var typeArguments = fieldType.GetGenericArguments();
               var keyType = typeArguments[0];
               var valueType = typeArguments[1];

               IEnumerable keysCollection = null;
               if (typeof(IDisposable).IsAssignableFrom(keyType) || typeof(IEnumerable<IDisposable>).IsAssignableFrom(keyType))
               {
                  try
                  {
                     var keysProperty = fieldType.GetProperty("Keys");
                     // ReSharper disable once PossibleNullReferenceException
                     keysCollection = keysProperty.GetValue(fieldValue) as IEnumerable;
                  }
                  catch (Exception)
                  {
                     // eat the exception.
                     // made best effort to capture the keys collection, there is no telling what the implementation of IDictionary<,> actually is in this code.
                     keysCollection = null;
                  }
               }

               IEnumerable valuesCollection = null;
               if (typeof(IDisposable).IsAssignableFrom(valueType) || typeof(IEnumerable<IDisposable>).IsAssignableFrom(valueType))
               {
                  try
                  {
                     var valuesProperty = fieldType.GetProperty("Values");
                     // ReSharper disable once PossibleNullReferenceException
                     valuesCollection = valuesProperty.GetValue(fieldValue) as IEnumerable;
                  }
                  catch (Exception)
                  {
                     // eat the exception.
                     // made best effort to capture the keys collection, there is no telling what the implementation of IDictionary<,> actually is in this code.
                     valuesCollection = null;
                  }
               }

               var collections = new List<IEnumerable>();
               if (valuesCollection != null)
               {
                  collections.Add(valuesCollection);
               }

               if (keysCollection != null)
               {
                  collections.Add(keysCollection);
               }

               foreach (var col in collections)
               {
                  foreach (var val in col)
                  {
                     if (val == null)
                     {
                        continue;
                     }

                     if (TryHandleSimpleDisposable(val))
                     {
                        // disposed
                        rv = true;
                     }
                     else if (TryHandleEnumerableOfDisposables(val))
                     {
                        // disposed
                        rv = true;
                     }

                     // stop going down the rabbit hole....
                  }
               }
            }
         }

         return rv;
      }

      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S1696:NullReferenceException should not be caught",
         Justification = "Eats the exception in a race condition (MWP)")]
      private Boolean TryHandleEnumerableOfDisposables(Object fieldValue)
      {
         var rv = false;

         if (fieldValue is IEnumerable<IDisposable> disposables)
         {
            // Field is IEnumerable<IDisposable>.
            rv = true;
            foreach (var disposable in disposables)
            {
               try
               {
                  disposable?.Dispose();
               }
               catch (NullReferenceException)
               {
                  // eat the exception
                  // (this is sometimes seen when there are chains of disposables, ownership is not a well defined concept in .Net.
               }
            }
         }

         return rv;
      }

      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S1696:NullReferenceException should not be caught",
         Justification = "Eats the exception in a race condition (MWP)")]
      private Boolean TryHandleSimpleDisposable(Object fieldValue)
      {
         var rv = false;

         if (fieldValue is IDisposable asIDisposable)
         {
            // A simple IDisposable field.
            rv = true;
            try
            {
               asIDisposable.Dispose();
            }
            catch (NullReferenceException)
            {
               // eat the exception
               // (this is sometimes seen when there are chains of disposables, ownership is not a well defined concept in .Net.
            }
         }

         return rv;
      }
   }
}