[CmdletBinding()]
Param(
	[Parameter(Mandatory=$True, Position=1)]
	[string]$Version	
)

function GeneratePackage([string] $projPath) 
{
    Write-Host -Verbose 'Generating packages for' $projPath
    & 'msbuild' $projPath '/t:pack' 
}

Get-ChildItem -Filter "*.csproj" -Recurse | ForEach-Object { GeneratePackage $_.FullName }