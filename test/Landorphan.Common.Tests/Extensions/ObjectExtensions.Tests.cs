﻿namespace Landorphan.Common.Tests.Extensions
{
   using System;
   using FluentAssertions;
   using Landorphan.TestUtilities;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   // ReSharper disable InconsistentNaming

   public static class ObjectExtensions_Tests
   {
      [TestClass]
      public class When_I_call_As_T : ArrangeActAssert
      {
         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_give_me_null_when_conversion_is_not_possible()
         {
            const String value = "MyValue";
            var asUri = value.As<Uri>();
            asUri.Should().BeNull();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_give_me_the_original_instance_back_when_conversion_is_possible()
         {
            const String value = "MyValue";
            var asObject = value.As<Object>();
            asObject.Should().BeSameAs(value);
         }
      }

      [TestClass]
      public class When_I_call_CastTo_T : ArrangeActAssert
      {
         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_give_me_the_original_instance_back_when_conversion_is_possible()
         {
            const String value = "MyValue";
            var asObject = value.CastTo<Object>();
            asObject.Should().BeSameAs(value);
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_throw_when_conversion_is_not_possible()
         {
            const String value = "MyValue";

            Action throwingAction = () => value.CastTo<Uri>();
            var e = throwingAction.Should().Throw<InvalidCastException>();
            e.And.Message.Should().Be("Unable to cast object of type 'System.String' to type 'System.Uri'.");
         }
      }

      [TestClass]
      public class When_I_call_Is_T : ArrangeActAssert
      {
         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_true_when_type_compatibility_is_present()
         {
            const String value = "MyValue";
            var isObject = value.Is<Object>();
            isObject.Should().BeTrue();
         }

         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_false_when_type_compatibility_is_not_present()
         {
            const String value = "MyValue";
            var isUri = value.Is<Uri>();
            isUri.Should().BeFalse();
         }
      }

      [TestClass]
      public class When_I_call_IsDefaultValue : ArrangeActAssert
      {
         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_false_on_non_default_value_types()
         {
            var value0 = Guid.NewGuid();
            value0.IsDefaultValue().Should().BeFalse();

            var value1 = 123;
            value1.IsDefaultValue().Should().BeFalse();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_false_on_non_null_reference_types()
         {
            var value0 = new Object();
            value0.IsDefaultValue().Should().BeFalse();

            var value1 = String.Empty;
            value1.IsDefaultValue().Should().BeFalse();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_true_on_default_value_value_types()
         {
            Guid.Empty.IsDefaultValue().Should().BeTrue();

            0.IsDefaultValue().Should().BeTrue();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_true_on_null_reference_types()
         {
            ((Object)null).IsDefaultValue().Should().BeTrue();

            ((String)null).IsDefaultValue().Should().BeTrue();
         }
      }

      [TestClass]
      public class When_I_call_IsNotNull : ArrangeActAssert
      {
         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_false_when_the_value_is_null()
         {
            const String value = null;
            var isNotNull = value.IsNotNull();
            isNotNull.Should().BeFalse();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_true_when_the_value_is_not_null()
         {
            const String value = "MyValue";
            var isNotNull = value.IsNotNull();
            isNotNull.Should().BeTrue();
         }
      }

      [TestClass]
      public class When_I_call_IsNull : ArrangeActAssert
      {
         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_false_when_the_value_is_not_null()
         {
            const String value = "MyValue";
            var isNull = value.IsNull();
            isNull.Should().BeFalse();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_return_true_when_the_value_is_null()
         {
            const String value = null;
            var isNull = value.IsNull();
            isNull.Should().BeTrue();
         }
      }

      [TestClass]
      public class When_I_call_UncheckedUnboxValueType : ArrangeActAssert
      {
         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_not_throw()
         {
            const Int64 value0 = -1;
            var actual0 = value0.UncheckedUnboxValueType<UInt64>();
            actual0.Should().Be(18_446_744_073_709_551_615ul);

            var value1 = UInt64.MaxValue;
            var actual1 = value1.UncheckedUnboxValueType<Int64>();
            actual1.Should().Be(-1);
         }
      }
   }
}
