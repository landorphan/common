Set-StrictMode -Version Latest
$ErrorActionPreference = 'Continue'

Copy-Item -Path .\build\BuildFiles\Default.Production.NetStd.FxCop.15.0.WithSonarLint.ruleset -Destination .\source\Landorphan.Common\Landorphan.Common.NetStd.ruleset
 