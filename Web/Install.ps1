$ErrorActionPreference = "Stop"
$webSiteName = "Expresso"
$physicalPath = "C:\inetpub\wwwroot\Expresso"

Try {
    Import-Module WebAdministration

    if (Test-Path "IIS:\AppPools\$webSiteName") {
        Remove-WebAppPool $webSiteName
	    Write-Host -ForegroundColor Yellow "Removed existing '$webSiteName' application pool"
    }

    if (Test-Path "IIS:\Sites\$webSiteName") {
        Remove-WebSite $webSiteName
	    Write-Host -ForegroundColor Yellow "Removed existing '$webSiteName' web site"
    }

    New-WebAppPool -Name $webSiteName | Out-Null
    Set-ItemProperty -Path "IIS:\AppPools\$webSiteName" -Name managedRuntimeVersion -Value v4.0
    Write-Host -ForegroundColor Green "Created '$webSiteName' application pool"

    New-WebSite -Id 1 -Name $webSiteName -ApplicationPool $webSiteName -PhysicalPath $physicalPath | Out-Null
    Write-Host -ForegroundColor Green "Created '$webSiteName' web site"
}
Catch {
    Write-Error "Failed to create '$webSiteName' web site: $_"
}
