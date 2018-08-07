Param(
    [Parameter(Mandatory=$false)]
    [string]$SourceBranch = "QA",
    
    [Parameter(Mandatory=$false)]
    [string]$TargetBranch = "builds",
    
    [Parameter(Mandatory=$false)]
    [string]$ProjectPath = "TappyPotato",

    [Parameter(Mandatory=$true)]
    [int]$build
    
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
        $content[$index - 1] = $line -replace "\s*$name\:\s{1}\d+",($line.Substring(0, $line.IndexOf(": ") + 2) + $value)
    }
    else
    {
        Write-Host $name 'was not found'
    }
    Set-Content -Path $settingsFile -Value $content
}

$workingDirectory = ($PSCmdlet.MyInvocation.MyCommand.Definition | Split-Path)
Set-Location $workingDirectory\..


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
git pull
git fetch origin
git merge origin $SourceBranch

Set-UnityProjectSetting .\$ProjectPath\ProjectSettings\ProjectSettings.asset Standalone $build
Set-UnityProjectSetting .\$ProjectPath\ProjectSettings\ProjectSettings.asset iOS $build
Set-UnityProjectSetting .\$ProjectPath\ProjectSettings\ProjectSettings.asset AndroidBundleVersionCode $build

git add $ProjectPath/ProjectSettings/ProjectSettings.asset
git commit -m "Version: 0.1.0.$build"
git push

git checkout $SourceBranch
git pull
git merge $TargetBranch
git push

git checkout master
git pull
git merge $SourceBranch
git push

git checkout $TargetBranch