﻿Set-StrictMode -Version Latest
$ErrorActionPreference = 'Continue'

$started = [DateTime]::UtcNow

dotnet clean landorphan.common.sln > $null
dotnet clean landorphan.common.sln -c debug > $null
dotnet clean landorphan.common.sln -c release > $null

# Attempt to delete each and every bin and obj directory
Get-ChildItem -inc bin,obj -rec | Remove-Item -rec -force

# Attempt to delete each and every packages directory
Get-ChildItem -inc packages -rec | Remove-Item -rec -force

$completed = [DateTime]::UtcNow
$elapsed = $completed - $started
"Build-Clean:"
"Elapsed:=        $elapsed"
"Started (UTC):=  $started"
"Completed (UTC):=$completed"
