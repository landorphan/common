﻿// ReSharper disable once CheckNamespace

namespace Landorphan.TestUtilities.ReusableTestImplementations
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using FluentAssertions;
    using Landorphan.Common;

    // ReSharper disable StringLiteralTypo
    // ReSharper disable InconsistentNaming

    /// <summary>
    /// Test implementations for static analyzer ruleset files.
    /// </summary>
    public class RulesetRequirements : TestBase
    {
        public void All_ruleset_should_not_have_duplicate_rule_ids_implementation()
        {
            // ReSharper disable once StringLiteralTypo
            var rulesetPath = "..\\..\\..\\..\\build\\CodeAnalysis\\AllRules.16.5.WithSonarLint.ruleset";
            Rulesets_should_not_have_duplicate_rule_ids_implementation(rulesetPath);
        }
        // there is no such thing as .Net Standard Test project

        /// <summary>
        /// Asserts the default source ruleset contains no duplicate rule id values.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1707: Identifiers should not contain underscores")]
        protected void Default_Source_Ruleset_should_not_have_duplicate_rule_ids_implementation()
        {
            var rulesetPath = "..\\..\\..\\..\\build\\CodeAnalysis\\Default.Source.16.5.WithSonarLint.ruleset";
            Rulesets_should_not_have_duplicate_rule_ids_implementation(rulesetPath);
        }

        /// <summary>
        /// Asserts the default test ruleset contains no duplicate rule id values.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1707: Identifiers should not contain underscores")]
        protected void Default_Test_Ruleset_should_not_have_duplicate_rule_ids_implementation()
        {
            var rulesetPath = "..\\..\\..\\..\\build\\CodeAnalysis\\Default.Test.16.5.WithSonarLint.ruleset";
            Rulesets_should_not_have_duplicate_rule_ids_implementation(rulesetPath);
        }

        /// <summary>
        /// Asserts the given ruleset file contains no duplicate rule id values.
        /// </summary>
        /// <param name="rulesetPath">
        /// The path the the ruleset file to inspect.
        /// </param>
        [SuppressMessage("Microsoft.Naming", "CA1707: Identifiers should not contain underscores")]
        [SuppressMessage("SonarLint.CodeSmell", "S3776: Cognitive Complexity of methods should not be too high")]
        protected void Rulesets_should_not_have_duplicate_rule_ids_implementation(string rulesetPath)
        {
            rulesetPath.ArgumentNotNull(nameof(rulesetPath));
            if (!File.Exists(rulesetPath))
            {
                throw new FileNotFoundException(null, rulesetPath);
            }

            var ruleIdCountMap = new SortedDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            Debug.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(Directory.GetCurrentDirectory());

            var doc = XDocument.Load(rulesetPath);
            var ruleSets = doc.Descendants("RuleSet");
            foreach (var ruleSet in ruleSets)
            {
                var ruleCollections = ruleSet.Descendants("Rules");
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

            var count = (
                from kvp in ruleIdCountMap
                where kvp.Value > 1
                select kvp).Count();
            if (count > 0)
            {
                Trace.WriteLine($"{count} duplicate rule ids");
            }

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
