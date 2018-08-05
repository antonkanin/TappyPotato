Param(
    [Parameter(Mandatory=$false)]
    [string]$SourceBranch = "QA",
    
    [Parameter(Mandatory=$false)]
    [string]$TargetBranch = "builds"
    )

# Set-Location $PSCmdlet.MyInvocation.MyCommand.

# Start-Process "git" -WorkingDirectory "..\" -NoNewWindow -ArgumentList "checkout $TargetBranch"

#$PSCmdlet.MyInvocation | Get-Member

$workingDirectory = ($PSCmdlet.MyInvocation.MyCommand.Definition | Split-Path)
Set-Location $workingDirectory\..

git checkout $TargetBranch
git merge $SourceBranch


Set-Location $workingDirectory