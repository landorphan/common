## Static Code Analysis strategy

We are moving away from *.ruleset analyzer configuration to global analyzer configuration files.  Requirements:  1) support xplat, 2) support CI/CD command line builds, 3) be editor agnostic.

Global analyzer files were chosen despite having to be specifically referenced in each project file because *.editorconfig is overloaded with concerns, and we only have 2 templates "source" and "test",
and because we switching from *ruleset msbuild projects which already require specifying the same in the project file.  Additionally, as templates, copy and paste from global analyzer config files to editor 
config files is an easy operation for teams choosing to so do.

The prior ruleset files are maintained.  See Default.Source.16.5.WithSonarLint.ruleset and Default.Test.16.5.WithSonarLint.ruleset.

### Implementation details

Configuration for each rule is XOR set to "warning" or "none".  Because we rely on command line builds we treat warnings as errors on CI/CD builds, so we eschew "error" statuses because we want 
the developer to be able to see all of the issues in an IDE environment.  That leaves:
   * "Hidden" - The violation is not visible to the user. The IDE is notified of the violation, however.
   * "Info" - The violation generates a message in the Error List.

Neither are in use because we have gated check-ins.  Our goal is to show the developer what the CI/CD gate will see with an eye toward showing all, rather than failing on first error.  This also 
assists with xplat and local command line builds.

###
Our global suppression strategy is bifurcated into source and test strategies.  We believe test should be at the same coding standard as source, but there are common testing scenarios that run afoul
of what would be expected in source files. For example, tests often check for ANY exception being thrown, and thus lack specificty expected in source for exception handling.  We exclude the rule 
against catching all exceptions in test projects as a consequence.

Suppressions are pragmatic, and suggestions for teams grabbing these files based on our experience.  Your mileage may vary.  For example (going deep), S2328 do not reference mutable fields in 
<code>GetHashCode()</code> implementations is based on the implementations of unique collections (they only check for uniqueness when items are inserted -- mutable objects allow for duplicates in 
sets of the same after mutation).  If .Net had been built from the ground up with this notion of "const correctness" for collections, all would be good, but application years (decades) after 
<code>GetHashCode()</code> was put in every .Net class is  simply too noisy in our opinion.  Teams that need "const correctness" in their collections already know this behavior whereas teams that are
using plain old class objects for short-lived web display purposes receive no benefit from this rule.  If you disagree, just update the rule behavior in your project.

NOTE:  IDE warnings (e.g., IDE0079) are being configured in .editorconfig.