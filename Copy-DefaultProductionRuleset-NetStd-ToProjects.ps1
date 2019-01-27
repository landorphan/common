Set-StrictMode -Version Latest
$ErrorActionPreference = 'Continue'

[String] $sourceRulesetPath = ".\build\BuildFiles\Default.Production.NetStd.FxCop.15.0.WithSonarLint.ruleset"
if(Test-Path $sourceRulesetPath)
{
  [String] $dirPath = ".\source\Landorphan.Common"
  if(Test-Path $dirPath)
  {
    Copy-Item -Path $sourceRulesetPath -Destination "$dirPath\Landorphan.Common.NetStd.ruleset"
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
