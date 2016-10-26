###
# This script reads AssemblyVersion from a file, uses the major and minor part and replaces the build/revision with provided value.
# It is used by TeamCity to get a...
#	- 'version' for the assembly to be built with.
#	- 'buildNumber' to be displayed and used in TeamCity.
#	- 'prerelease' from the branch name. 'master' is considered not to be prerelease.
###

Param(
  [string]$build = "0",
  [string]$file = "AssemblyVersionInfo.cs",
  [string]$prerelease = "",
  [string]$push = "false"
)

Try
{
	$ErrorActionPreference = 'Stop' 

	$pattern = '\[assembly: AssemblyVersion\("(.*)"\)\]'
	$pre = ''

	if ($prerelease -eq "") {
	}
	elseif ($prerelease -like "*master") {
		$prerelease = ""
	}
	elseif (-Not $prerelease.StartsWith("-")) {
		$pre = $prerelease
		$prerelease = "-" + $prerelease
	}
	else{
		$pre = $prerelease.Substring(1)
	}

	Get-Content $file | Foreach-Object {
		if($_ -match $pattern) {
			# We have found the matching line
			# Edit the version number and put back.
			$fileVersionString = [string]$matches[1]
			$fileVersion = [version]$fileVersionString.replace('*','0')
			write-host ("Found version '" + $fileVersion + "' in file '" + $file + "'.") -f green

			$version = "{0}.{1}.{2}.0" -f $fileVersion.Major, $fileVersion.Minor, $build			
			$buildNumber = "{0}.{1}.{2}{3}" -f $fileVersion.Major, $fileVersion.Minor, $build, $prerelease

			write-host "##teamcity[setParameter name='version' value='$version']"
			write-host "##teamcity[buildNumber '$buildNumber']"
			write-host "##teamcity[setParameter name='prerelease' value='$pre']"

			write-host ("TeamCity variable 'version' set to '" + $version + "'.") -f green
			write-host ("TeamCity variable 'buildNumber' set to '" + $buildNumber + "'.") -f green			
			write-host ("TeamCity variable 'prerelease' set to '" + $pre + "'.") -f green
		}
	}

	# Write the file version back to the AssemblyVersionInfo.cs file
	if ([System.Convert]::ToBoolean($push))
	{
		write-host ("Writing version '" + $version + "' back to file '" + $file + "'.") -f green
		$assemblyVersion = '[assembly: AssemblyVersion("' + $version + '")]'

		(Get-Content $file) `
			-replace $pattern, $assemblyVersion ` |
			Out-File $file

		#Also push 'AssemblyFileVersion' back to the file.
		$pattern = '\[assembly: AssemblyFileVersion\("(.*)"\)\]'
		$assemblyVersion = '[assembly: AssemblyFileVersion("' + $version + '")]'

		(Get-Content $file) `
			-replace $pattern, $assemblyVersion ` |
			Out-File $file
	}
	
	exit 0
}
Catch
{
	write-host ($error[0]) -f "red"
	exit 1
}