param($installPath, $toolsPath, $package, $project)

Import-Module (Join-Path $toolsPath NewRelicHelper.psm1)

Write-Host "***Cleaning up the newrelic.config file ***"  -ForegroundColor DarkGreen
# manually remove newrelic.config since the Nuget uninstaller won't due to it being "modified"
Try{
	$scripts = $project.ProjectItems | Where-Object { $_.Name -eq "newrelic.config" }

	if ($scripts) {
		$scripts.ProjectItems | ForEach-Object { $_.Delete() }
	}
}Catch{
	#Swallow - file has been removed
}