$currentPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
$sourcePath = Join-Path $currentPath Expresso
$userModulePath = "$env:UserProfile\Documents\WindowsPowerShell\Modules"
Set-Item env:PSModulePath "$env:PSModulePath;$userModulePath"

Write-Host -ForegroundColor Cyan Starting Expresso Console...
Copy-Item -Recurse -Force $sourcePath $userModulePath
Import-Module Expresso
Set-Location $Home
