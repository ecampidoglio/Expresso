# This script is automatically invoked by Octopus Deploy
# to perform the deployment.
# Other scripts that will be invoked as well, if present:
# - PreDeploy.ps1
# - Deploy.ps1
# - PostDeploy.ps1
# - DeployFailed.ps1

& .\Expresso.Database.exe | Write-Host
