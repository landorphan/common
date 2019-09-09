# requires -Version 5.1

<#
  .SYNOPSIS
    Builds the solution as release, and all of its dependencies.
  .EXAMPLE
    & Build-Relase.ps1
  .INPUTS
    (None)
  .OUTPUTS
    (None)
#>
[CmdletBinding()]
param
(
  [Parameter(Position = 0,HelpMessage = 'The solution file to use (needed when more than one solution file exists).')]
  [System.String]$SolutionFileName
)
begin
{
  Set-StrictMode -Version Latest

  $started = [datetime]::UtcNow

  if ($null -eq (Get-Module -Name 'mwp.utilities'))
  {
    $ConfirmPreference = "High" #([High], Medium, Low, None)
    $DebugPreference = "Continue" #([SilentlyContinue], Continue, Inquire, Stop)
    $ErrorActionPreference = "Continue" #(SilentlyContinue, [Continue], Suspend <!--NOT ALLOWED -->, Inquire, Stop)
    $InformationPreference = "Continue" #(SilentlyContinue, Continue, Inquire, Stop)
    $VerbosePreference = "Continue" #([SilentlyContinue], Continue, Inquire, Stop)
    $WarningPreference = "Inquire" #(SilentlyContinue, [Continue], Inquire, Stop)
  }
  else
  {
    Use-CallerPreference -Cmdlet $PSCmdlet -SessionState $ExecutionContext.SessionState
  }

  $thisScriptDirectory = Split-Path $script:MyInvocation.MyCommand.Path
  $setVarScript = Join-Path -Path (Split-Path $thisScriptDirectory) -ChildPath 'Set-BuildVariables.ps1'
  $removeVarScript = Join-Path -Path (Split-Path $thisScriptDirectory) -ChildPath 'Remove-BuildVariables.ps1'
  $buildCleanScript = Join-Path -Path $thisScriptDirectory -ChildPath 'Build-Clean.ps1'
}
process
{
  try
  {
    & $buildCleanScript -SolutionFileName $SolutionFileName
    & $setVarScript -SolutionFileName $SolutionFileName

    if ($null -eq $buildSolution)
    {
      Write-Error 'No Visual Studio solution found.'
      return 1
    }

    if ($buildSolution -is [array])
    {
      Write-Error '"Multiple Visual Studio solutions found; this script expects a single solution file.'
      return 2
    }

    # Use this syntax if a .Net Framework is added
    # Verbosity: q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic].
    # MSbuild.exe /property:Configuration=Release /verbosity:normal /restore /detailedsummary $buildSolution

    dotnet build $buildSolution -c release
  }
  finally
  {
    & $removeVarScript
  }
}
end
{
  $completed = [datetime]::UtcNow
  $elapsed = $completed - $started
  'Build-Release:'
  "  Elapsed        := $elapsed"
  "  Started   (UTC):= $started"
  "  Completed (UTC):= $completed"
}
