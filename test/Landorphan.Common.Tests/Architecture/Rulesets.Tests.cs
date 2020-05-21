namespace Landorphan.Common.Tests.Architecture
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
            All_ruleset_should_not_have_duplicate_rule_ids_implementation();
        }

        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void Default_Source_Ruleset_should_not_have_duplicate_rule_ids()
        {
            Default_Source_Ruleset_should_not_have_duplicate_rule_ids_implementation();
        }

        [TestMethod]
        [TestCategory(TestTiming.IdeOnly)]
        public void Default_Test_Ruleset_should_not_have_duplicate_rule_ids()
        {
            Default_Test_Ruleset_should_not_have_duplicate_rule_ids_implementation();
        }
    }
}
