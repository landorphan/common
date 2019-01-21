namespace Landorphan.TestUtilities.NoIoc.Tests
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Xml.Linq;
   using FluentAssertions;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   // ReSharper disable InconsistentNaming

   [TestClass]
   public class RuleSet_Tests : TestBase
   {
      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void Default_Production_Ruleset_NetCore_should_not_have_duplicate_rule_ids()
      {
         var rulesetPath = "..\\..\\..\\..\\build\\BuildFiles\\Default.Production.NetCore.FxCop.15.0.WithSonarLint.ruleset";
         Ruleset_should_not_have_duplicate_rule_id_implementation(rulesetPath);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void Default_Production_Ruleset_NetFx_should_not_have_duplicate_rule_ids()
      {
         var rulesetPath = "..\\..\\..\\..\\build\\BuildFiles\\Default.Production.NetFx.FxCop.15.0.WithSonarLint.ruleset";
         Ruleset_should_not_have_duplicate_rule_id_implementation(rulesetPath);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void Default_Production_Ruleset_NetStd_should_not_have_duplicate_rule_ids()
      {
         var rulesetPath = "..\\..\\..\\..\\build\\BuildFiles\\Default.Production.NetStd.FxCop.15.0.WithSonarLint.ruleset";
         Ruleset_should_not_have_duplicate_rule_id_implementation(rulesetPath);
      }

      // no such thing as Test .Net Standard

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void Default_Test_Ruleset_NetCore_should_not_have_duplicate_rule_ids()
      {
         var rulesetPath = "..\\..\\..\\..\\build\\BuildFiles\\Default.Test.NetCore.FxCop.15.0.WithSonarLint.ruleset";
         Ruleset_should_not_have_duplicate_rule_id_implementation(rulesetPath);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void Default_Test_Ruleset_NetFx_should_not_have_duplicate_rule_ids()
      {
         var rulesetPath = "..\\..\\..\\..\\build\\BuildFiles\\Default.Test.NetFx.FxCop.15.0.WithSonarLint.ruleset";
         Ruleset_should_not_have_duplicate_rule_id_implementation(rulesetPath);
      }

      private void Ruleset_should_not_have_duplicate_rule_id_implementation(String rulesetPath)
      {
         var ruleIdCountMap = new SortedDictionary<String, Int32>(StringComparer.OrdinalIgnoreCase);

         var doc = XDocument.Load(rulesetPath);
         var ruleSets = doc.Descendants("RuleSet");
         foreach (var ruleSet in ruleSets)
         {
            var ruleCollections = ruleSets.Descendants("Rules");
            foreach (var ruleCollection in ruleCollections)
            {
               var rules = ruleCollection.Descendants("Rule");
               foreach (var rule in rules)
               {
                  var key = rule.Attribute("Id").Value;
                  if (ruleIdCountMap.ContainsKey(key))
                  {
                     ruleIdCountMap[key] += 1;
                  }
                  else
                  {
                     ruleIdCountMap.Add(key, 1);
                  }
               }
            }
         }

         var echoedTestSet = false;

         var duplicates = false;
         foreach (var kvp in ruleIdCountMap)
         {
            if (kvp.Value > 1)
            {
               duplicates = true;
               if (!echoedTestSet)
               {
                  Trace.WriteLine(rulesetPath);
                  echoedTestSet = true;
               }

               Trace.WriteLine(kvp.Key + " has " + kvp.Value + " instances");
            }
         }

         duplicates.Should().BeFalse();
      }
   }
}
