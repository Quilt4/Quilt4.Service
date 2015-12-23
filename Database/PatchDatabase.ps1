param($p1) #File where a list of connectionstrings can be read

$pathTime = get-date
$failCounter = 0
$lf = @"


"@

#function IsNullOrEmpty($str) {if ($str) {"String is not empty"} else {"String is null or empty"}}

#List connection strings
$connectionStrings = Get-Content $p1
foreach($connectionString in $connectionStrings)
{
	Try
	{
		$SqlConnection = New-Object System.Data.SqlClient.SqlConnection
		$SqlConnection.ConnectionString = $connectionString
		$SqlConnection.Open()
	}
	Catch
	{
		Write-Host $error[0] -ForegroundColor Red
		exit 1
	}
	
	#Create the DBVersion patch table if needed
	$SqlCmd1 = New-Object System.Data.SqlClient.SqlCommand
	$SqlCmd1.CommandText = "IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DBVersion')) BEGIN CREATE TABLE DBVersion ( PatchName varchar(250) PRIMARY KEY NOT NULL, PatchTime DateTime NOT NULL ) END"
	$SqlCmd1.Connection = $SqlConnection
	$reqult1 = $SqlCmd1.ExecuteNonQuery()

	#Get a list of all patches that is to be ran (file system)
	$Dir = get-childitem "Scripts" -recurse
	$List = $Dir | where {$_.extension -eq ".sql"}
		
	$sort = @()
	foreach($item in $List)
	{
	  $t1 = $item -match '^(\d+).*'
	  $temp = New-Object PSCustomObject -property @{'Number' = [int]$matches[1]}
	  $temp | Add-Member -type NoteProperty -name Name -value $item.Name
	  $temp | Add-Member -type NoteProperty -name fullname -value $item.fullname
	  $sort += $temp
	}

	foreach ($item in $sort | Sort-Object Number | Select-Object Name, fullname) {
		#Check if this perticular item
		$SqlCmd2 = New-Object System.Data.SqlClient.SqlCommand
		$SqlCmd2.CommandText = "SELECT * FROM DBVersion WHERE PatchName='" + $item.Name + "'"
		$SqlCmd2.Connection = $SqlConnection
		$result2 = $SqlCmd2.ExecuteScalar()

		$patchTime = Get-Date -format "yyyy-MM-dd HH:mm:ss"

		if ($result2)
		{
			"Patch '" + $item.Name + "' has already been applied."
			$failCounter = $failCounter + 0
		} 
		else 
		{	
			"Starting to run patch '" + $item.Name + "' at time " + $patchTime + "."
		
			#Open the content of the file.
			
			$fileData = Get-Content $item.fullname
			
			$fileContent = ""
			
			foreach($fileLine in $fileData)
			{
				$fileContent = $fileContent + $fileLine + $lf
			}

			$fileSection = $fileContent -split '\nGO'
			foreach($section in $fileSection)
			{
				#Execute the patch and look at the result.
				$SqlCmd4 = New-Object System.Data.SqlClient.SqlCommand
				$SqlCmd4.CommandTimeout = 300
				$SqlCmd4.CommandText = $section
				$SqlCmd4.Connection = $SqlConnection
				Try
				{
					"--- Executing patch ---"
					$section
					"GO"
					$result4 = $SqlCmd4.ExecuteScalar()
				}
				Catch
				{
					$failCounter = $failCounter + 1
					Write-Host $error[0] -ForegroundColor Red
				}
				
				if ($failCounter -gt 0)
				{
					exit $failCounter
					return
				}
				
				#$result4 | ft
				#$failCounter

				#Execute patch and insert into the database
				$SqlCmd3 = New-Object System.Data.SqlClient.SqlCommand
				$SqlCmd3.CommandText = "INSERT INTO DBVersion ( PatchName, PatchTime ) VALUES ('" + $item.Name + "','" + $patchTime + "')"
				$SqlCmd3.Connection = $SqlConnection
				$result3 = $SqlCmd3.ExecuteNonQuery()
				#$result3 | ft
			}
				
			"Patch '" + $item.Name + "' was applied."
			$failCounter = $failCounter + 0
		}
	}

	$SqlConnection.Close()
}

exit $failCounter
