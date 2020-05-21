namespace Landorphan.Common.Tests
{
    using FluentAssertions;
    using Landorphan.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable InconsistentNaming

    public static class ReferenceEqualityComparerTests
    {
        [TestClass]
        public class When_evaluating_the_hashcode_of_null : ArrangeActAssert
        {
            private ReferenceEqualityComparer<object> target;

            protected override void ArrangeMethod()
            {
                target = new ReferenceEqualityComparer<object>();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_zero()
            {
                var actual = target.GetHashCode(null);
                actual.Should().Be(0);
            }
        }

        [TestClass]
        public class When_I_call_ReferenceEqualityComparer_Equals : ArrangeActAssert
        {
            private readonly ReferenceEqualityComparer<object> target = new ReferenceEqualityComparer<object>();

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_be_false_when_x_and_y_are_unique_instances()
            {
                var actual = target.Equals(new object(), new object());
                actual.Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_be_false_when_x_is_not_null_and_y_is_null()
            {
                var actual = target.Equals(new object(), null);
                actual.Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_be_false_when_x_is_null_and_y_is_not_null()
            {
                var actual = target.Equals(null, new object());
                actual.Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_be_true_when_both_x_and_y_are_null()
            {
                var actual = target.Equals(null, null);
                actual.Should().BeTrue();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_be_truee_when_x_and_y_are_the_same_instance()
            {
                var obj = new object();
                var actual = target.Equals(obj, obj);
                actual.Should().BeTrue();
            }
        }
    }
}
