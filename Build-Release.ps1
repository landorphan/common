Set-StrictMode -Version Latest
$ErrorActionPreference = 'Continue'

$started = [DateTime]::UtcNow

& .\Build-Clean.ps1

dotnet build landorphan.common.sln -c release

$completed = [DateTime]::UtcNow
$elapsed = $completed - $started
"Build-Release:"
"Elapsed:=        $elapsed"
"Started (UTC):=  $started"
"Completed (UTC):=$completed"
