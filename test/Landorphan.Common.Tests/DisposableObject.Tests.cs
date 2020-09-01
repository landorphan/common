namespace Landorphan.Common.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using FluentAssertions;
    using Landorphan.Common.Threading;
    using Landorphan.TestUtilities;
    using Landorphan.TestUtilities.TestFacilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable InconsistentNaming

    // NOTE:  Do not create test classes that descend from DisposableArrangeActAssert in this file,
    // The implementation of DisposableArrangeActAssert is a copy of DisposableObject's implementation.

    [TestClass]
    public class DisposableObjectTests : ArrangeActAssert
    {
        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        [TestCategory(WellKnownTestCategories.PreventRegressionBug)]
        public void It_should_be_able_to_inspect_a_dictionary_with_the_same_type_for_both_key_and_value()
        {
            // The implementation of disposable object was assuming a dictionary had 2 type arguments,
            // when the dictionary is say IDictionary<String,String> the TypeArguments array has only 1 element
            // this was causing an index our of range error.
            var target = new DisposableClassWithDictionaryStringString();
            target.Dispose();

            target.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void It_should_dispose_of_all_contained_disposables_in_an_enumerable()
        {
            var targetEnumerable = DisposableHelper.SafeCreate<DisposableEnumerable>();
            var contained = DisposableHelper.SafeCreate(() => new DisposableItem());
            targetEnumerable.AddDisposable(contained);

            targetEnumerable.Dispose();

            contained.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void It_should_dispose_of_auto_properties()
        {
            var target = DisposableHelper.SafeCreate<DisposableItem>();
            target.Dispose();
            target.DisposableReadOnlyAutoProperty.Should().BeNull();
        }

        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void It_should_dispose_of_read_only_fields()
        {
            var target = DisposableHelper.SafeCreate<DisposableItem>();
            target.Dispose();
            target.GetPrivateReadOnlyDisposableField().Should().BeNull();
        }

        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void It_should_handle_contained_nulls_in_an_enumerable()
        {
            var targetEnumerable = DisposableHelper.SafeCreate<DisposableEnumerable>();
            targetEnumerable.AddDisposable(null);
            targetEnumerable.Dispose();

            TestHardCodes.NoExceptionWasThrown.Should().BeTrue();
        }

        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void It_should_handle_inheritance()
        {
            var target = DisposableHelper.SafeCreate<DisposableDescendant>();
            var item0 = DisposableHelper.SafeCreate(() => new DisposableItem());
            target.SetFlatField(item0);

            var item1 = DisposableHelper.SafeCreate(() => new DisposableItem());
            target.SetAnotherField(item1);

            target.Dispose();

            item1.IsDisposed.Should().BeTrue();
            item0.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void It_should_handle_null_fields()
        {
            var target = DisposableHelper.SafeCreate<DisposableEnumerable>();
            target.SetFlatField(null);
            target.Dispose();

            TestHardCodes.NoExceptionWasThrown.Should().BeTrue();
        }

        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void It_should_not_dispose_of_auto_properties_attributed_with_do_not_dispose()
        {
            var target = DisposableHelper.SafeCreate<DisposableItem>();
            target.Dispose();
            target.DoNotDisposeAutoProperty.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void It_should_not_dispose_of_fields_attributed_with_do_not_dispose()
        {
            var target = DisposableHelper.SafeCreate<DisposableItem>();
            target.Dispose();
            target.GetDoNotDisposeField().Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void It_should_throw_when_disposed()
        {
            var target = DisposableHelper.SafeCreate<DisposableItem>();
            target.Dispose();

            // ReSharper disable once AccessToDisposedClosure
            Action throwingAction = target.Method;
            throwingAction.Should().Throw<ObjectDisposedException>();
        }

        [TestClass]
        public class DisposableObject_Dictionary_Tests : ArrangeActAssert
        {
            // the target classes implement a variety of IDictionary<,> implementations
            //  ConcurrentDictionary<T,U>
            //  Dictionary<T,U>
            //  ImmutableDictionary<T,U>
            //  SortedDictionary<T,U>

            [SuppressMessage("SonarLint.CodeSmell", "S3966: Objects should not be disposed more than once")]
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_dispose_a_Dictionary_field_with_a_Disposable_Key()
            {
                using (var target = DisposableHelper.SafeCreate<ClassWithDictionaryFieldWithDisposableKey>())
                {
                    target.Dispose();
                    var actual = target.TestHookGetField();
                    actual.Should().BeNull();
                }
            }

            [SuppressMessage("SonarLint.CodeSmell", "S3966: Objects should not be disposed more than once")]
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_dispose_a_Dictionary_field_with_a_Disposable_Value()
            {
                using (var target = DisposableHelper.SafeCreate<ClassWithDictionaryFieldWithDisposableValue>())
                {
                    target.Dispose();
                    var actual = target.TestHookGetField();
                    actual.Should().BeNull();
                }
            }

            [SuppressMessage("SonarLint.CodeSmell", "S3966: Objects should not be disposed more than once")]
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_dispose_a_Dictionary_field_with_a_Enumerable_Disposable_Value()
            {
                using (var target = DisposableHelper.SafeCreate<ClassWithDictionaryFieldWithEnumerableDisposableValue>())
                {
                    target.Dispose();
                    var actual = target.TestHookGetField();
                    actual.Should().BeNull();
                }
            }

            [SuppressMessage("SonarLint.CodeSmell", "S3966: Objects should not be disposed more than once")]
            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_dispose_a_Dictionary_field_with_an_Enumerable_Disposable_Key()
            {
                using (var target = DisposableHelper.SafeCreate<ClassWithDictionaryFieldWithEnumerableDisposableKey>())
                {
                    target.Dispose();
                    var actual = target.TestHookGetField();
                    actual.Should().BeNull();
                }
            }

            private class ClassWithDictionaryFieldWithDisposableKey : DisposableObject
            {
                private readonly ImmutableDictionary<Mutex, string> _field;

                [SuppressMessage("SonarLint.CodeSmell", "S1144: Unused private types or members should be removed", Justification = "False positive(MWP)")]
                public ClassWithDictionaryFieldWithDisposableKey()
                {
                    var builder = ImmutableDictionary<Mutex, string>.Empty.ToBuilder();
                    builder.Add(DisposableHelper.SafeCreate<Mutex>(), "a name");
                    _field = builder.ToImmutable();
                }

                internal object TestHookGetField()
                {
                    return _field;
                }
            }

            private class ClassWithDictionaryFieldWithDisposableValue : DisposableObject
            {
                private readonly Dictionary<string, Mutex> _field;

                [SuppressMessage("SonarLint.CodeSmell", "S1144: Unused private types or members should be removed", Justification = "False positive(MWP)")]
                public ClassWithDictionaryFieldWithDisposableValue()
                {
                    _field = new Dictionary<string, Mutex> { { "name", DisposableHelper.SafeCreate<Mutex>() } };
                }

                internal object TestHookGetField()
                {
                    return _field;
                }
            }

            private class ClassWithDictionaryFieldWithEnumerableDisposableKey : DisposableObject
            {
                private readonly ConcurrentDictionary<HashSet<Mutex>, string> _field;

                [SuppressMessage("SonarLint.CodeSmell", "S1144: Unused private types or members should be removed", Justification = "False positive(MWP)")]
                public ClassWithDictionaryFieldWithEnumerableDisposableKey()
                {
                    var set = new HashSet<Mutex> { new Mutex(), new Mutex(), new Mutex() };
                    _field = new ConcurrentDictionary<HashSet<Mutex>, string>();
                    _field.TryAdd(set, "another name");
                }

                internal object TestHookGetField()
                {
                    return _field;
                }
            }

            private class ClassWithDictionaryFieldWithEnumerableDisposableValue : DisposableObject
            {
                private readonly SortedDictionary<string, ICollection<Mutex>> _field;

                [SuppressMessage("SonarLint.CodeSmell", "S1144: Unused private types or members should be removed", Justification = "False positive(MWP)")]
                public ClassWithDictionaryFieldWithEnumerableDisposableValue()
                {
                    var col = new Collection<Mutex> { new Mutex(), new Mutex(), new Mutex() };
                    _field = new SortedDictionary<string, ICollection<Mutex>> { { "key value", col } };
                }

                internal object TestHookGetField()
                {
                    return _field;
                }
            }
        }

        private class DisposableClassWithDictionaryStringString : DisposableObject
        {
            private readonly Dictionary<string, string> _myDictionary;

            public DisposableClassWithDictionaryStringString()
            {
                _myDictionary = new Dictionary<string, string> { { "A Key", "A Value" } };
                TestHelp.DoNothing(_myDictionary);
            }
        }

        private class DisposableDescendant : DisposableEnumerable
        {
            private IDisposable anotherField;

            public void SetAnotherField(IDisposable value)
            {
                anotherField = value;
                TestHelp.DoNothing(anotherField);
            }
        }

        private class DisposableEnumerable : DisposableObject, IEnumerable<IDisposable>
        {
            private readonly List<IDisposable> listOfDisposables = new List<IDisposable>();

            private IDisposable flatField;

            public IEnumerator<IDisposable> GetEnumerator()
            {
                return listOfDisposables.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void AddDisposable(IDisposable item)
            {
                ThrowIfDisposed();

                listOfDisposables.Add(item);
            }

            public void SetFlatField(IDisposable value)
            {
                flatField = value;
                TestHelp.DoNothing(flatField);
            }
        }

        private class DisposableItem : DisposableObject
        {
            [DoNotDispose]
            private readonly Mutex _doNotDispose;
            private readonly Mutex _privateReadOnlyDisposableField;

            public DisposableItem()
            {
                _privateReadOnlyDisposableField = new Mutex();
                _doNotDispose = new Mutex();
                DisposableReadOnlyAutoProperty = new NonRecursiveLock();
                DoNotDisposeAutoProperty = new NonRecursiveLock();
            }

            public NonRecursiveLock DisposableReadOnlyAutoProperty { get; }

            [DoNotDispose]
            public IDisposable DoNotDisposeAutoProperty { get; }

            public IDisposable GetDoNotDisposeField()
            {
                return _doNotDispose;
            }

            public IDisposable GetPrivateReadOnlyDisposableField()
            {
                return _privateReadOnlyDisposableField;
            }

            public void Method()
            {
                ThrowIfDisposed();
            }
        }
    }
}
