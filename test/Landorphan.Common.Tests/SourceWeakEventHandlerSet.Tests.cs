namespace Landorphan.Common.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Landorphan.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable InconsistentNaming

    public static class SourceWeakEventHandlerSetTests
    {
        [TestClass]
        public class When_I_add_a_null_event_handler : ArrangeActAssert
        {
            private bool actual;
            private SourceWeakEventHandlerSet<EventArgs> target;

            protected override void ArrangeMethod()
            {
                target = new SourceWeakEventHandlerSet<EventArgs>();
            }

            protected override void ActMethod()
            {
                actual = target.Add(null);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_not_add_and_should_not_throw()
            {
                actual.Should().BeFalse();
            }
        }

        [TestClass]
        public class When_I_add_an_anonymous_event_handler : ArrangeActAssert
        {
            private EventArgs actualEventArgs;
            private bool actualRegistered;
            private object actualSender;
            private bool duplicateRegistered;
            private EventArgs expectedEventArgs;
            private object expectedSender;
            private SourceWeakEventHandlerSet<EventArgs> target;

            protected override void ArrangeMethod()
            {
                target = new SourceWeakEventHandlerSet<EventArgs>();
            }

            protected override void ActMethod()
            {
                var eh = new EventHandler<EventArgs>(
                    (o, e) =>
                    {
                        actualSender = o;
                        actualEventArgs = e;
                    });

                actualRegistered = target.Add(eh);

                expectedSender = this;
                expectedEventArgs = EventArgs.Empty;

                duplicateRegistered = target.Add(eh);

                target.Invoke(expectedSender, expectedEventArgs);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_invokes_the_event_handler()
            {
                ReferenceEquals(actualSender, expectedSender).Should().BeTrue();
                actualSender.Should().BeSameAs(expectedSender);
                actualEventArgs.Should().Be(expectedEventArgs);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_add_the_event_handler()
            {
                actualRegistered.Should().BeTrue();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_not_add_duplicates()
            {
                duplicateRegistered.Should().BeFalse();
            }
        }

        [TestClass]
        public class When_I_add_an_instance_event_handler : ArrangeActAssert
        {
            private EventArgs actualEventArgs;
            private bool actualRegistered;
            private object actualSender;
            private EventArgs expectedEventArgs;
            private object expectedSender;
            private SourceWeakEventHandlerSet<EventArgs> target;

            protected override void ArrangeMethod()
            {
                target = new SourceWeakEventHandlerSet<EventArgs>();
            }

            protected override void ActMethod()
            {
                var eh = new EventHandler<EventArgs>(InstanceMethod);

                actualRegistered = target.Add(eh);

                expectedSender = this;
                expectedEventArgs = EventArgs.Empty;

                target.Invoke(expectedSender, expectedEventArgs);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_invokes_the_event_handler()
            {
                actualSender.Should().Be(expectedSender);
                actualEventArgs.Should().Be(expectedEventArgs);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_add_the_event_handler()
            {
                actualRegistered.Should().BeTrue();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_not_add_duplicates()
            {
                var eh = new EventHandler<EventArgs>(InstanceMethod);

                var duplicateRegistered = target.Add(eh);
                duplicateRegistered.Should().BeFalse();
            }

            private void InstanceMethod(object sender, EventArgs e)
            {
                actualSender = sender;
                actualEventArgs = e;
            }
        }

        [TestClass]
        public class When_I_add_an_static_event_handler : ArrangeActAssert
        {
            private static EventArgs actualEventArgs;
            private static object actualSender;
            private bool actualRegistered;
            private EventArgs expectedEventArgs;
            private object expectedSender;
            private SourceWeakEventHandlerSet<EventArgs> target;

            protected override void ArrangeMethod()
            {
                target = new SourceWeakEventHandlerSet<EventArgs>();
            }

            protected override void ActMethod()
            {
                var eh = new EventHandler<EventArgs>(StaticMethod);

                actualRegistered = target.Add(eh);

                expectedSender = this;
                expectedEventArgs = EventArgs.Empty;

                target.Invoke(expectedSender, expectedEventArgs);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_invokes_the_event_handler()
            {
                actualSender.Should().Be(expectedSender);
                actualEventArgs.Should().Be(expectedEventArgs);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_add_the_event_handler()
            {
                actualRegistered.Should().BeTrue();
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_not_add_duplicates()
            {
                var eh = new EventHandler<EventArgs>(StaticMethod);

                var duplicateRegistered = target.Add(eh);
                duplicateRegistered.Should().BeFalse();
            }

            private static void StaticMethod(object sender, EventArgs e)
            {
                actualSender = sender;
                actualEventArgs = e;
            }
        }

        [TestClass]
        public class When_I_clear_event_handlers : ArrangeActAssert
        {
            private static EventArgs actualEventArgs;
            private static object actualSender;
            private SourceWeakEventHandlerSet<EventArgs> target;

            protected override void ArrangeMethod()
            {
                target = new SourceWeakEventHandlerSet<EventArgs>();
                var ehStatic = new EventHandler<EventArgs>(StaticMethod);
                target.Add(ehStatic);
                var ehInstance = new EventHandler<EventArgs>(InstanceMethod);
                target.Add(ehInstance);
            }

            protected override void ActMethod()
            {
                target.Clear();
                target.Invoke(this, EventArgs.Empty);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_invokes_no_event_handlers()
            {
                actualSender.Should().BeNull();
                actualEventArgs.Should().BeNull();
            }

            [SuppressMessage("SonarLint.CodeSmell", "S4144: Methods should not have identical implementations")]
            private static void StaticMethod(object sender, EventArgs e)
            {
                actualSender = sender;
                actualEventArgs = e;
            }

            [SuppressMessage("SonarLint.CodeSmell", "S4144: Methods should not have identical implementations")]
            private void InstanceMethod(object sender, EventArgs e)
            {
                actualSender = sender;
                actualEventArgs = e;
            }
        }

        [TestClass]
        public class When_I_remove_a_null_event_handler : ArrangeActAssert
        {
            private bool actual;
            private SourceWeakEventHandlerSet<EventArgs> target;

            protected override void ArrangeMethod()
            {
                target = new SourceWeakEventHandlerSet<EventArgs>();
            }

            protected override void ActMethod()
            {
                actual = target.Remove(null);
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_not_unregister_and_should_not_throw()
            {
                actual.Should().BeFalse();
            }
        }

        [TestClass]
        public class When_I_set_a_instance_subscriber_to_null_and_garbage_collect : DisposableArrangeActAssert
        {
            private Guid keptInstanceId;
            private EventSink keptInstanceSink;
            private TextWriterTraceListener memoryListener;
            private MemoryStream memoryStream;
            private Guid removedInstanceId;
            private EventSink removedInstanceSink;
            private EventSource source;
            private List<string> traceLines;

            protected override void TeardownTestMethod()
            {
                Trace.Listeners.Remove(memoryListener);
                base.TeardownTestMethod();
            }

            protected override void ArrangeMethod()
            {
                traceLines = new List<string>();

                memoryStream = DisposableHelper.SafeCreate(
                    () =>
                    {
                        var rv = DisposableHelper.SafeCreate(() => new MemoryStream());
                        rv.Capacity = 1024 * 1024;
                        rv.Position = 0;
                        return rv;
                    });
                var streamWriter = new StreamWriter(memoryStream) { AutoFlush = true };
                memoryListener = new TextWriterTraceListener(streamWriter);
                Trace.Listeners.Add(memoryListener);

                source = new EventSource();

                removedInstanceSink = new EventSink(source);
                removedInstanceId = removedInstanceSink.Id;
                TestHelp.DoNothing(removedInstanceSink);

                keptInstanceSink = new EventSink(source);
                keptInstanceId = keptInstanceSink.Id;
                TestHelp.DoNothing(keptInstanceSink);
            }

            [SuppressMessage("SonarLint.CodeSmell", "S1215:GC.Collect should not be called")]
            [SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.GC.Collect")]
            protected override void ActMethod()
            {
                removedInstanceSink = null;
                GC.Collect();

                // test failed intermittently without the delay, garbage collection message was missing.
                Task.Delay(TimeSpan.FromMilliseconds(1)).Wait();
                source.DoIt();
                Trace.Flush();

                memoryStream.Position = 0;
                string text;
                using (var reader = new StreamReader(memoryStream))
                {
                    text = reader.ReadToEnd();
                }

                traceLines.AddRange(text.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_garbage_collect_the_removed_event_sink()
            {
                traceLines.Should().Contain(string.Format(CultureInfo.InvariantCulture, "{0}: Instance garbage collected.", removedInstanceId));
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_not_notify_the_removed_event_sink()
            {
                traceLines.Should().NotContain(string.Format(CultureInfo.InvariantCulture, "{0}: Instance event received.", removedInstanceId));
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_notify_the_kept_event_sink()
            {
                traceLines.Should().Contain(string.Format(CultureInfo.InvariantCulture, "{0}: Instance event received.", keptInstanceId));
            }

            [TestMethod]
            [TestCategory(TestTiming.CheckIn)]
            public void It_should_notify_the_static_event_sink()
            {
                traceLines.Should().Contain(string.Format(CultureInfo.InvariantCulture, "{0}: Static event received.", Guid.Empty));
            }
        }

        internal class EventSink
        {
            private readonly EventSource _eventSource;

            internal EventSink(EventSource eventSource)
            {
                eventSource.ArgumentNotNull(nameof(eventSource));
                _eventSource = eventSource;
                _eventSource.MyEvent += InstanceEventSourceMyEvent;
                _eventSource.MyEvent += StaticEventSourceMyEvent;
            }

            ~EventSink()
            {
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", Id, "Instance garbage collected."));
                _eventSource.MyEvent -= InstanceEventSourceMyEvent;
            }

            public Guid Id { get; } = Guid.NewGuid();

            private void InstanceEventSourceMyEvent(object sender, EventArgs e)
            {
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", Id, "Instance event received."));
            }

            private void StaticEventSourceMyEvent(object sender, EventArgs e)
            {
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", Guid.Empty, "Static event received."));
            }
        }

        internal class EventSource
        {
            private readonly SourceWeakEventHandlerSet<EventArgs> _listeners = new SourceWeakEventHandlerSet<EventArgs>();

            public event EventHandler<EventArgs> MyEvent
            {
                add => _listeners.Add(value);

                remove => _listeners.Remove(value);
            }

            internal void DoIt()
            {
                RaiseMyEvent(EventArgs.Empty);
            }

            private void RaiseMyEvent(EventArgs e)
            {
                _listeners.Invoke(this, e);
            }
        }
    }
}
