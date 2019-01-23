Set-StrictMode -Version Latest
$ErrorActionPreference = 'Continue'

Copy-Item -Path .\build\BuildFiles\Default.Test.NetCore.FxCop.15.0.WithSonarLint.ruleset -Destination .\test\Landorphan.Common.Tests\Landorphan.Common.Tests.NetCore.ruleset
