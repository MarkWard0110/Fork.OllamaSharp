dotnet pack

# Define the path where the .nupkg files are stored
$path = "src\bin\Release\"

# Get all .nupkg files in the specified directory
$files = Get-ChildItem -Path $path -Filter "*.nupkg"

# Initialize variables to keep track of the latest version and file
$latestVersion = [Version]"0.0.0"
$latestFile = $null

foreach ($file in $files) {
    if ($file.Name -match "OllamaSharp\.(\d+\.\d+\.\d+)\.nupkg") {
        $currentVersion = [Version]$matches[1]
        if ($currentVersion -gt $latestVersion) {
            $latestVersion = $currentVersion
            $latestFile = $file
        }
    }
}

# Output the name of the file with the latest version
if ($null -ne $latestFile) {
    $latestFile.Name
} else {
    "No valid OllamaSharp.nupkg files found."
    exit 0
}


Write-Host "Adding $latestFile to NuGet source"
#\\ds213j.homelan.binaryward.com\BINARY.DATA\SOFTWARE\NuGet\nuget-v6.9.1.exe add src\bin\Release\$latestFile  -source \\ds218.homelan.binaryward.com\dev.data\nuget-store

$nugetPath = "\\ds213j.homelan.binaryward.com\BINARY.DATA\SOFTWARE\NuGet\nuget-v6.9.1.exe"
$sourcePath = "\\ds218.homelan.binaryward.com\dev.data\nuget-store"
$localPath = "$latestFile"

# Building the command string
$command = "& `"$nugetPath`" add `"$localPath`" -source `"$sourcePath`""
Invoke-Expression $command
