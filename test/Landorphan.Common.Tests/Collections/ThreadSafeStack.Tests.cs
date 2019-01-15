﻿namespace Landorphan.Common.Tests.Collections
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using FluentAssertions;
   using Landorphan.Common.Collections;
   using Landorphan.TestUtilities;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   // ReSharper disable InconsistentNaming

   public static class ThreadSafeStack_Tests
   {
      // TODO: serialization test.

      [TestClass]
      public class When_I_create_a_configured_ThreadSafeStack : DisposableArrangeActAssert
      {
         private IEqualityComparer<Object> expectedEqualityComparer;
         private Boolean expectedIgnoresNullValues;
         private ThreadSafeStack<Object> target;

         protected override void ArrangeMethod()
         {
            expectedEqualityComparer = new ReferenceEqualityComparer<Object>();
            expectedIgnoresNullValues = false;
            target = new ThreadSafeStack<Object>(expectedEqualityComparer, expectedIgnoresNullValues, null);
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_treat_nulls_as_configured()
         {
            target.IgnoresNullValues.Should().Be(expectedIgnoresNullValues);
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_use_the_specified_comparer()
         {
            target.EqualityComparer.Should().BeSameAs(expectedEqualityComparer);
         }
      }

      [TestClass]
      public class When_I_create_a_default_ThreadSafeStack : DisposableArrangeActAssert
      {
         private readonly ThreadSafeStack<Int32> target = new ThreadSafeStack<Int32>();

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_be_empty()
         {
            ((ICountEnumerable)target).Count.Should().Be(0);
            target.IsEmpty.Should().BeTrue();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_be_enumerable()
         {
            target.Should().BeAssignableTo(typeof(IEnumerable));
            target.Should().BeAssignableTo(typeof(IEnumerable<Int32>));
            target.GetEnumerator().Should().NotBeNull();
            ((IEnumerable)target).GetEnumerator().Should().NotBeNull();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_be_thread_safe()
         {
            target.IsThreadSafe.Should().BeTrue();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_employ_a_default_comparer()
         {
            target.EqualityComparer.Should().Be(EqualityComparer<Int32>.Default);
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_ignore_null_values()
         {
            target.IgnoresNullValues.Should().BeTrue();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_not_be_read_only()
         {
            target.IsReadOnly.Should().BeFalse();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_not_be_serializable()
         {
            target.GetType().IsSerializable.Should().BeFalse();
         }
      }

      [TestClass]
      public class When_I_have_a_ThreadSafeStack_and_call_Clear : DisposableArrangeActAssert
      {
         private readonly List<Int32> expected = new List<Int32> {0, 1, 2};
         private readonly ThreadSafeStack<Int32> target = new ThreadSafeStack<Int32>();

         protected override void ArrangeMethod()
         {
            foreach (var item in expected)
            {
               target.Push(item);
            }
         }

         protected override void ActMethod()
         {
            target.Clear();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_be_empty()
         {
            target.IsEmpty.Should().BeTrue();
         }
      }

      [TestClass]
      public class When_I_have_a_ThreadSafeStack_and_call_Contains : DisposableArrangeActAssert
      {
         private readonly List<Int32> expected = new List<Int32> {0, 1, 2};
         private readonly ThreadSafeStack<Int32> target = new ThreadSafeStack<Int32>();

         protected override void ArrangeMethod()
         {
            foreach (var item in expected)
            {
               target.Push(item);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_be_false_for_values_not_contained()
         {
            var below = (from value in target select value).Min() - 1;
            var above = (from value in target select value).Max() + 1;

            ((IContainsEnumerable<Int32>)target).Contains(below).Should().BeFalse();
            ((IContainsEnumerable<Int32>)target).Contains(above).Should().BeFalse();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_be_true_for_values_contained()
         {
            var values = (from value in target select value).Distinct().ToList();
            foreach (var v in values)
            {
               ((IContainsEnumerable<Int32>)target).Contains(v).Should().BeTrue();
            }
         }
      }

      [TestClass]
      public class When_I_have_a_ThreadSafeStack_and_call_Peek : DisposableArrangeActAssert
      {
         private readonly List<Int32> expected = new List<Int32> {0, 1, 2};
         private readonly ThreadSafeStack<Int32> target = new ThreadSafeStack<Int32>();
         private Int32 value;

         protected override void ArrangeMethod()
         {
            foreach (var item in expected)
            {
               target.Push(item);
            }
         }

         protected override void ActMethod()
         {
            value = target.Peek();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_Return_The_Last_Value()
         {
            value.Should().Be(2);
         }
      }

      [TestClass]
      public class When_I_have_a_ThreadSafeStack_and_call_Pop : DisposableArrangeActAssert
      {
         private readonly List<Int32> expected = new List<Int32> {3, 3, 0, 1, 2, 2};
         private readonly ThreadSafeStack<Int32> target = new ThreadSafeStack<Int32>();
         private Int32 originalCount;
         private Int32 value;

         protected override void ArrangeMethod()
         {
            foreach (var item in expected)
            {
               target.Push(item);
            }

            originalCount = ((ICountEnumerable)target).Count;
         }

         protected override void ActMethod()
         {
            value = target.Pop();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_Remove_Only_One_Value()
         {
            value.Should().Be(2);
            ((ICountEnumerable)target).Count.Should().Be(originalCount - 1);
         }
      }

      [TestClass]
      public class When_I_have_a_ThreadSafeStack_and_call_Push : DisposableArrangeActAssert
      {
         private readonly List<Int32> expected = new List<Int32> {3, 2, 1, 0, 0, 2, 4};
         private readonly ThreadSafeStack<Int32> target = new ThreadSafeStack<Int32>();

         protected override void ActMethod()
         {
            foreach (var item in expected)
            {
               target.Push(item);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_contain_the_expected_values_In_The_Expected_Order()
         {
            var lst = new List<Int32>();
            while (((ICountEnumerable)target).Count > 0)
            {
               var v = target.Pop();
               lst.Add(v);
            }

            expected.Reverse();
            lst.SequenceEqual(expected).Should().BeTrue();
         }
      }

      [TestClass]
      public class When_I_have_a_ThreadSafeStack_that_does_not_ignore_null_values_and_push_a_null : DisposableArrangeActAssert
      {
         private readonly ThreadSafeStack<Object> target = new ThreadSafeStack<Object>(null, false, null);

         protected override void ActMethod()
         {
            target.Push(null);
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_contain_the_expected_values()
         {
            target.Should().Contain(new Object[] {null});
         }
      }

      [TestClass]
      public class When_I_have_a_ThreadSafeStack_that_ignores_null_values_and_push_a_null : DisposableArrangeActAssert
      {
         private readonly ThreadSafeStack<Object> target = new ThreadSafeStack<Object>(null, true, null);

         protected override void ActMethod()
         {
            target.Push(null);
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_contain_the_expected_values()
         {
            target.Should().BeEmpty();
         }
      }

      [TestClass]
      public class When_I_have_an_empty_ThreadSafeStack_and_call_Peek : DisposableArrangeActAssert
      {
         private readonly ThreadSafeStack<Int32> target = new ThreadSafeStack<Int32>();

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_throw()
         {
            Action throwingAction = () => target.Peek();
            throwingAction.Should().Throw<InvalidOperationException>().WithMessage("*empty*");
         }
      }

      [TestClass]
      public class When_I_have_an_empty_ThreadSafeStack_and_call_Pop : DisposableArrangeActAssert
      {
         private readonly ThreadSafeStack<Int32> target = new ThreadSafeStack<Int32>();

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_throw()
         {
            Action throwingAction = () => target.Pop();
            throwingAction.Should().Throw<InvalidOperationException>().WithMessage("*empty*");
         }
      }
   }
}
