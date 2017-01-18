function GeneratePackage([string] $packagePath) 
{
    $apiKey = '7d933a1a-19a3-4ad8-8312-b0eaddf0bec6'
    Write-Host -Verbose 'Publishing packages for' $packagePath
    & 'nuget' 'push' $packagePath $apiKey '-s' 'https://www.myget.org/F/superjmn/api/v3/index.json'
}

Get-ChildItem -Filter "*.nupkg" -Recurse | ForEach-Object { GeneratePackage $_.FullName }