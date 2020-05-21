﻿namespace Landorphan.Common.Tests.Architecture
{
    using Landorphan.TestUtilities;
    using Landorphan.TestUtilities.ReusableTestImplementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable InconsistentNaming

    [TestClass]
    public class Ruleset_Tests : RulesetRequirements
    {
        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void All_ruleset_should_not_have_duplicate_rule_ids()
        {
            // ReSharper disable once StringLiteralTypo
            var rulesetPath = "..\\..\\..\\..\\build\\CodeAnalysis\\All.NetFx.15.0.WithSonarLint.ruleset";
            Rulesets_should_not_have_duplicate_rule_ids_implementation(rulesetPath);
        }

        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void Common_ruleset_should_not_have_duplicate_rule_ids()
        {
            Rulesets_should_not_have_duplicate_rule_ids_implementation("..\\..\\..\\..\\source\\Landorphan.Common\\Landorphan.Common.NetStd.ruleset");
        }

        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void Default_Production_Ruleset_NetCore_should_not_have_duplicate_rule_ids()
        {
            Default_Production_Ruleset_NetCore_should_not_have_duplicate_rule_ids_implementation();
        }

        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void Default_Production_Ruleset_NetFx_should_not_have_duplicate_rule_ids()
        {
            Default_Production_Ruleset_NetFx_should_not_have_duplicate_rule_ids_implementation();
        }

        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void Default_Production_Ruleset_NetStd_should_not_have_duplicate_rule_ids()
        {
            Default_Production_Ruleset_NetStd_should_not_have_duplicate_rule_ids_implementation();
        }

        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void Default_Test_Ruleset_NetCore_should_not_have_duplicate_rule_ids()
        {
            Default_Test_Ruleset_NetCore_should_not_have_duplicate_rule_ids_implementation();
        }

        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void Default_Test_Ruleset_NetFx_should_not_have_duplicate_rule_ids()
        {
            Default_Test_Ruleset_NetFx_should_not_have_duplicate_rule_ids_implementation();
        }
    }
}
