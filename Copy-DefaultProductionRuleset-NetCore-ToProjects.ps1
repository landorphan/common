Set-StrictMode -Version Latest
$ErrorActionPreference = 'Continue'

Copy-Item -Path .\build\BuildFiles\Default.Production.NetCore.FxCop.15.0.WithSonarLint.ruleset -Destination .\test\Landorphan.TestUtilities.NoIoc\Landorphan.TestUtilities.NoIoc.NetCore.ruleset
 