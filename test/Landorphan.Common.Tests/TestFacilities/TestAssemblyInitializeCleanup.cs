﻿namespace Landorphan.Common.Tests.TestFacilities
{
    using System.Collections.Immutable;
    using System.Reflection;
    using Landorphan.TestUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public static class TestAssemblyInitializeCleanup
    {
        internal static IImmutableSet<Assembly> AssembliesUnderTest { get; private set; }

        /// <summary>
        /// Performs assembly level initialization.
        /// </summary>
        /// <remarks>
        /// Executes once, before any tests to be executed are run.
        /// </remarks>
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            // quieting intermittent code analysis warning
            TestHelp.DoNothing(context);

            // acquire assemblies under test
            var assemblies = ImmutableHashSet<Assembly>.Empty.ToBuilder();

            // Landorphan.Common
            assemblies.Add(typeof(DisposableHelper).Assembly);

            AssembliesUnderTest = assemblies.ToImmutable();
        }

        /// <summary>
        /// Frees resources obtained by the test assembly.
        /// </summary>
        /// <remarks>
        /// Executes once, after all tests to be executed are run.
        /// </remarks>
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // currently no resources to clean up.
        }
    }
}
