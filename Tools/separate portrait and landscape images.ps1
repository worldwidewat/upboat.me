[reflection.assembly]::loadwithpartialname("System.Drawing") | Out-Null

$basePath = "INSERT IMAGES PATH HERE"

ForEach($file in (Get-ChildItem -Path $basePath -Filter *.jpg))
{ 
	$image = [Drawing.Image]::FromFile($file.FullName)
	$destination = $basePath + '\portrait'
	if($image.Width -gt $image.Height) {
		$destination = $basePath + '\landscape'
	} 
	$image.Dispose()
	
	Move-Item $file.FullName -Destination $destination
	
}