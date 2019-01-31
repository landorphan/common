namespace Landorphan.Common
{
   using System;
   using System.Collections.Generic;

   /// <summary>
   /// Type extensions.
   /// </summary>
   public static class TypeExtensions
   {
      /// <summary>
      /// Determines whether the specified type is static.
      /// </summary>
      /// <param name="type">
      /// The type.
      /// </param>
      /// <returns>
      /// <c> true </c> if the given type is static; otherwise <c> false </c>.
      /// </returns>
      public static Boolean IsStatic(this Type type)
      {
         type.ArgumentNotNull(nameof(type));

         return type.IsAbstract && type.IsSealed;
      }

      /// <summary>
      /// Determines whether the specified type is a value type, or a well-known immutable type.
      /// </summary>
      /// <param name="type">
      /// The type.
      /// </param>
      /// <returns>
      /// <c> true </c> if the given type is a value type or a well-known immutable type (e.g. <see cref="System.String"/>); otherwise <c> false </c>.
      /// </returns>
      /// <remarks>
      /// Is in no way related to <see cref="System.Collections.Immutable"/>.
      /// </remarks>
      public static Boolean IsValueTypeOrWellKnownImmutableType(this Type type)
      {
         type.ArgumentNotNull(nameof(type));

         var wellKnownImmutableTypes = new List<Type> {typeof(String)};

         return type.IsValueType || wellKnownImmutableTypes.Contains(type);
      }
   }
}
