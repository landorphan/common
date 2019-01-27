Set-StrictMode -Version Latest
$ErrorActionPreference = 'Continue'

[String] $sourceRulesetPath = ".\build\BuildFiles\Default.Test.NetCore.FxCop.15.0.WithSonarLint.ruleset"
if(Test-Path $sourceRulesetPath)
{
  [String] $dirPath = ".\test\Landorphan.Common.Tests"
  if(Test-Path $dirPath)
  {
    Copy-Item -Path $sourceRulesetPath -Destination "$dirPath\Landorphan.Common.Tests.NetCore.ruleset"
  }
  else
  {
    Write-Error "Could not find directory $dirPath"
  }
}
else
{
  Write-Error "Could not find file $sourceRulesetPath"
}
