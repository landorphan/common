﻿namespace Landorphan.Common.Tests.Architecture
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Landorphan.TestUtilities;
    using Landorphan.TestUtilities.ReusableTestImplementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable InconsistentNaming

    [TestClass]
    public class TestArchitecture_Tests : TestArchitecturalRequirements
    {
        [SuppressMessage("SonarLint.CodeSmell", "S2699: Tests should include assertions", Justification = "Base implementation has assertion (MWP)")]
        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void All_But_Excluded_Tests_Descend_From_TestBase()
        {
            All_But_Excluded_Tests_Descend_From_TestBase_Implementation();
        }

        [SuppressMessage("SonarLint.CodeSmell", "S2699: Tests should include assertions", Justification = "Base implementation has assertion (MWP)")]
        [TestMethod]
        [TestCategory(TestTiming.CheckIn)]
        public void All_Tests_Not_Ignored_Have_Exactly_One_Timing_Category()
        {
            All_Tests_Not_Ignored_Have_Exactly_One_Timing_Category_Implementation();
        }

        [SuppressMessage("SonarLint.CodeSmell", "S2699: Tests should include assertions", Justification = "This 'Test' outputs trace information in the IDE")]
        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void Find_Ignored_Tests()
        {
            var asm = typeof(TestArchitecture_Tests).Assembly;
            Find_ignored_tests_in_assembly(asm);
        }

        protected override Assembly GetTestAssembly()
        {
            return GetType().Assembly;
        }
    }
}
