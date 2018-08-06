Param(
    [Parameter(Mandatory=$false)]
    [string]$SourceBranch = "QA",
    
    [Parameter(Mandatory=$false)]
    [string]$TargetBranch = "builds"
    )


function Set-UnityProjectSetting {
    Param(
        [Parameter(Mandatory=$true)]
        [string]$settingsFile,
        [Parameter(Mandatory=$true)]
        [string]$name,
        [Parameter(Mandatory=$true)]
        [int]$value
        )


    $content = Get-Content $settingsFile
    $index = 0
    do {
        $line = $content[$index]
        $index++
    }
    while($index -lt $content.Length -and (-not ($line -match "\s?$name\:\s{1}\d+")))
    if ($line -match "\s?$name\:\s{1}\d+")
    {
        Write-Host $line    
    }
    else
    {
        Write-Host $name 'was not found'
    }
}

$workingDirectory = ($PSCmdlet.MyInvocation.MyCommand.Definition | Split-Path)
# Set-Location $workingDirectory\..
Set-Location S:\src\antonkanin\tappypotato


# configure local branches
$branches = git branch -vv

If (($branches | Where-Object {$_ -match "\s\[origin/$SourceBranch"}) -eq $null)
{
    git checkout -b $SourceBranch origin/$SourceBranch
}

if (($branches | Where-Object {$_ -match "\s\[origin/$TargetBranch"}) -eq $null)
{
    git checkout -b $TargetBranch origin/$TargetBranch
}

git checkout $TargetBranch
git fetch origin
git merge origin $SourceBranch
# Get-Content .\Unity\ProjectSettings\ProjectSettings.asset | Where-Object {$_ -match "\s?AndroidBundleVersionCode\:"}
$buildNumber = 50
Set-UnityProjectSetting .\Unity\ProjectSettings\ProjectSettings.asset Standalone $buildNumber
Set-UnityProjectSetting .\Unity\ProjectSettings\ProjectSettings.asset iOS $buildNumber
Set-UnityProjectSetting .\Unity\ProjectSettings\ProjectSettings.asset AndroidBundleVersionCode $buildNumber






#git checkout $TargetBranch
#git merge $SourceBranch