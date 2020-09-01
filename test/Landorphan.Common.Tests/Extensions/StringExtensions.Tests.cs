﻿namespace Landorphan.Common.Tests.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using FluentAssertions;
    using Landorphan.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable InconsistentNaming
    // ReSharper disable InvokeAsExtensionMethod

    public static class StringExtensions_Tests
    {
        [TestClass]
        public class When_I_call_IsNotNullNorEmptyNorWhiteSpace : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_false_on_empty()
            {
                string.Empty.IsNotNullNorEmptyNorWhiteSpace().Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_false_on_null()
            {
                ((string)null).IsNotNullNorEmptyNorWhiteSpace().Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_false_on_whitespace()
            {
                " ".IsNotNullNorEmptyNorWhiteSpace().Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_true_on_non_matching_values()
            {
                " x ".IsNotNullNorEmptyNorWhiteSpace().Should().BeTrue();
            }
        }

        [TestClass]
        public class When_I_call_IsNotNullOrEmpty : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_false_on_a_null_string()
            {
                var actual = StringExtensions.IsNotNullNorEmpty(null);
                actual.Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_false_on_an_empty_string()
            {
                var value = string.Empty;
                var actual = StringExtensions.IsNotNullNorEmpty(value);
                actual.Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_true_on_a_populated_string()
            {
                const string value = "A string";
                var actual = StringExtensions.IsNotNullNorEmpty(value);
                actual.Should().BeTrue();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_true_on_a_white_space_string()
            {
                const string value = "   ";
                var actual = StringExtensions.IsNotNullNorEmpty(value);
                actual.Should().BeTrue();
            }
        }

        [TestClass]
        public class When_I_call_IsNullOrEmpty : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_false_on_a_populated_string()
            {
                const string value = "A string";
                var actual = StringExtensions.IsNullOrEmpty(value);
                actual.Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_false_on_a_white_space_string()
            {
                const string value = "   ";
                var actual = StringExtensions.IsNullOrEmpty(value);
                actual.Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_true_on_a_null_string()
            {
                string value = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                var actual = StringExtensions.IsNullOrEmpty(value);
                actual.Should().BeTrue();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_true_on_an_empty_string()
            {
                var value = string.Empty;
                var actual = StringExtensions.IsNullOrEmpty(value);
                actual.Should().BeTrue();
            }
        }

        [TestClass]
        public class When_I_call_IsNullOrEmptyOrWhiteSpace : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_false_on_non_matching_values()
            {
                " x ".IsNullOrEmptyOrWhiteSpace().Should().BeFalse();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_true_on_empty()
            {
                "".IsNullOrEmptyOrWhiteSpace().Should().BeTrue();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_true_on_null()
            {
                ((string)null).IsNullOrEmptyOrWhiteSpace().Should().BeTrue();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_true_on_whitespace()
            {
                " ".IsNullOrEmptyOrWhiteSpace().Should().BeTrue();
            }
        }

        [TestClass]
        public class When_I_call_LeftTrim : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_leave_trailing_while_removing_leading_characters()
            {
                const string value = " \tvalue \t";

                value.LeftTrim().Should().Be("value \t");
                value.LeftTrim(' ').Should().Be("\tvalue \t");
                value.LeftTrim(new[] { ' ', '\t' }).Should().Be("value \t");
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_empty_when_all_characters_match()
            {
                const string value = "  ";

                value.LeftTrim().Length.Should().Be(0);
                value.LeftTrim(' ').Length.Should().Be(0);
                value.LeftTrim(new[] { ' ', '\t' }).Length.Should().Be(0);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_empty_when_invoked_on_empty()
            {
                var value = string.Empty;

                value.LeftTrim().Length.Should().Be(0);
                value.LeftTrim(' ').Length.Should().Be(0);
                value.LeftTrim(new[] { ' ', '\t' }).Length.Should().Be(0);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_null_when_invoked_on_null()
            {
                string value = null;

                // ReSharper disable ExpressionIsAlwaysNull
                value.LeftTrim().Should().BeNull();
                value.LeftTrim(' ').Should().BeNull();
                value.LeftTrim(new[] { ' ', '\t' }).Should().BeNull();

                // ReSharper restore ExpressionIsAlwaysNull
            }
        }

        [TestClass]
        public class When_I_call_ReverseString : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_handle_an_empty_string()
            {
                var original = string.Empty;
                var actual = original.ReverseString();
                actual.Should().Be(string.Empty);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_the_reversed_value()
            {
                const string original = "123";
                const string expected = "321";
                var actual = original.ReverseString();
                actual.Should().Be(expected);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_throw_on_null()
            {
                Action throwingAction = () => ((string)null).ReverseString();
                throwingAction.Should()
                    .Throw<ArgumentNullException>()
                    .And.ParamName.Should()
                    .Be("value");
            }
        }

        [TestClass]
        public class When_I_call_RightTrim : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_leave_leading_while_removing_trailing_characters()
            {
                const string value = " \tvalue\t ";

                value.RightTrim().Should().Be(" \tvalue");
                value.RightTrim(' ').Should().Be(" \tvalue\t");
                value.RightTrim(new[] { ' ', '\t' }).Should().Be(" \tvalue");
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_empty_when_all_characters_match()
            {
                const string value = "  ";

                value.RightTrim().Length.Should().Be(0);
                value.RightTrim(' ').Length.Should().Be(0);
                value.RightTrim(new[] { ' ', '\t' }).Length.Should().Be(0);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_empty_when_invoked_on_empty()
            {
                var value = string.Empty;

                value.RightTrim().Length.Should().Be(0);
                value.RightTrim(' ').Length.Should().Be(0);
                value.RightTrim(new[] { ' ', '\t' }).Length.Should().Be(0);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_null_when_invoked_on_null()
            {
                string value = null;

                // ReSharper disable ExpressionIsAlwaysNull
                value.RightTrim().Should().BeNull();
                value.RightTrim(' ').Should().BeNull();
                value.RightTrim(new[] { ' ', '\t' }).Should().BeNull();

                // ReSharper restore ExpressionIsAlwaysNull
            }
        }

        [SuppressMessage("Sonar.CodeSmell", "S1854: Unused assignments should be removed", Justification = "false positive")]
        [TestClass]
        public class When_I_call_ToSecureString : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_convert_with_fidelity()
            {
                var original = "password";
                using (var secured = original.ToSecureString())
                {
                    var valuePtr = IntPtr.Zero;
                    try
                    {
                        valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secured);
                        var extracted = Marshal.PtrToStringUni(valuePtr);
                        extracted.Should().Be(original);
                    }
                    finally
                    {
                        Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                    }
                }
            }
        }

        [TestClass]
        public class When_I_call_TrimNullToEmpty : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_convert_null_to_empty()
            {
                const string value = null;

                value.TrimNullToEmpty().Length.Should().Be(0);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_remove_leading_and_trailing_whiteSpace()
            {
                const string value = "   \t abc \t   ";

                value.TrimNullToEmpty().Should().Be(value.Trim());
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_the_given_value_when_not_null_nor_trimmable()
            {
                const string value = "abc";

                value.TrimNullToEmpty().Should().Be(value);
            }
        }

        [TestClass]
        public class When_I_call_TrimNullToNull : TestBase
        {
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_remove_leading_and_trailing_whiteSpace()
            {
                const string value = "   \t abc \t   ";

                value.TrimNullToNull().Should().Be(value.Trim());
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_return_null_on_null()
            {
                const string value = null;

                value.TrimNullToNull().Should().BeNull();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_the_given_value_when_not_null_nor_trimmable()
            {
                const string value = "abc";

                value.TrimNullToNull().Should().Be(value);
            }
        }
    }
}
