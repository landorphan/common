To Enable code analysis in .Net Core:
   install Nuget Microsoft.NetCore.Analyzers
Create a ruleset (ape the existing implementation).
   Create a file named "ProjectName.RuleSet" next to the csproj file
   Include it in the project
   Set the build action to C# analyzer additional file
   Edit the CSPROJ file adding "<CodeAnalysisRuleSet>Project.Name.ruleset</CodeAnalysisRuleSet>"
   In .Net Core projects, there is no need to add <RunCodeAnalysis>true</RunCodeAnalysis>, it has no effect.


GOTCHA:  takeaway use /// <inheritdoc/> above any [SuppressMessage] attributes

SYNTAX UNFLAGGED:
      /// <inheritdoc/>
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S4018: Generic methods should provide type parameters",
         Justification = "This generic method delegates implementation to the non-generic version.  I want one implementation (MWP)")]

SYNTAX FLAGGED:
      [SuppressMessage(
         "SonarLint.CodeSmell",
         "S4018: Generic methods should provide type parameters",
         Justification = "This generic method delegates implementation to the non-generic version.  I want one implementation (MWP)")]
      /// <inheritdoc/>