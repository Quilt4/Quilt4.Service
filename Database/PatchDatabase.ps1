Param(
    [parameter(Mandatory=$false)]
    [alias("f")]
    $fileName,
    [parameter(Mandatory=$false)]
    [alias("c")]
    $connectionString,	
    [parameter(Mandatory=$false)]
    [alias("s")]
    $sourceFolder,
    [parameter(Mandatory=$false)]
    [alias("m")]
    $merchants
	)
	
$pathTime = get-date
$failCounter = 0
$lf = @"


"@

function StartTran
{
	$SqlTran = New-Object System.Data.SqlClient.SqlCommand
	$SqlTran.CommandText = 'BEGIN TRAN Patch'
	$SqlTran.Connection = $SqlConnection
	$requltTran = $SqlTran.ExecuteNonQuery()	
}

function CommitTran
{
	$SqlTran = New-Object System.Data.SqlClient.SqlCommand
	$SqlTran.CommandText = 'COMMIT TRAN Patch'
	$SqlTran.Connection = $SqlConnection
	$requltTran = $SqlTran.ExecuteNonQuery()	
}

function RollbackTran
{
	$SqlTran = New-Object System.Data.SqlClient.SqlCommand
	$SqlTran.CommandText = 'COMMIT TRAN Patch'
	$SqlTran.Connection = $SqlConnection
	$requltTran = $SqlTran.ExecuteNonQuery()	
}

if ($sourceFolder) {} else
{
	$sourceFolder = "Scripts"
}

if ($fileName)
{
	#A file with connection strings
	Write-Host ("FileName: " + $fileName) -f yellow
	$connectionStrings = Get-Content $fileName
}
else
{
	if ($merchants)
	{
		#A connection string as parameter and a list of merchants
		$connectionStrings = New-Object System.Collections.ArrayList($null)
	
		$ms = $merchants.Split(";")
		foreach($merchant in $ms)
		{
			Write-Host ("Merchant: " + $merchant) -f yellow
			$tmp = $connectionStrings.Add($connectionString.Replace('{0}',$merchant))
		}
	}
	else
	{
		#A connection string as parameter
		Write-Host ("Single CS: " + $connectionString) -f yellow
		$connectionStrings = $connectionString
	}
}

#For each connection strings provided
foreach($connectionString in $connectionStrings)
{
	Write-Host ("ConnectionString: " + $connectionString) -f yellow
	Try
	{
		$SqlConnection = New-Object System.Data.SqlClient.SqlConnection
		$SqlConnection.ConnectionString = $connectionString
		$SqlConnection.Open()
	}
	Catch
	{
		Try
		{
			#TRY TO OPEN THE CONNECTION AS MASTER, CREATE THE DATABASE AND OPEN CONNECTION AGAIN
			$MasterSqlConnection = New-Object System.Data.SqlClient.SqlConnection
			$MasterSqlConnection.ConnectionString = $connectionString
			$dbName = $MasterSqlConnection.Database
			$MasterSqlConnection.ConnectionString = $connectionString.Replace("Initial Catalog=" + $dbName + ";","Initial Catalog=master;")
			$MasterSqlConnection.Open()

			Write-Host ("Creating database '" + $dbName + "' on server '" + $MasterSqlConnection.DataSource + "'.") -ForegroundColor "green"
			$SqlCmdCreate = New-Object System.Data.SqlClient.SqlCommand
			$SqlCmdCreate.CommandText = "CREATE DATABASE " + $dbName
			$SqlCmdCreate.Connection = $MasterSqlConnection
			$requltCmdCreate = $SqlCmdCreate.ExecuteNonQuery()

			$MasterSqlConnection.Close()

			$canOpen = 10
			While($canOpen -gt 0)
			{
				Try
				{
					$SqlConnection.Open()
					$canOpen = -1
				}
				Catch
				{
					$canOpen = $canOpen - 1
					Write-Host (".") -NoNewline
					Start-Sleep -s 1
				}
			}
			Write-Host ("")

			if ($canOpen -gt -1)
			{
				Write-Host ("Could not create database within 10 seconds. Try to run the script again or create the database manually.") -ForegroundColor "red"
				exit 1
			}
		}
		Catch
		{
			write-host ($error[0]) -ForegroundColor "red"
			exit 1
		}		
	}
	
	#Create the DBVersion patch table if needed
	$patchTime = Get-Date -format "yyyy-MM-dd HH:mm:ss"

	$SqlCmd1 = New-Object System.Data.SqlClient.SqlCommand
	$SqlCmd1.CommandText = "IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DBVersion')) BEGIN CREATE TABLE DBVersion ( PatchName varchar(250) PRIMARY KEY NOT NULL, PatchTime DateTime NOT NULL, VersionNumber INT NOT NULL, Checksum varchar(50) NULL ) INSERT INTO DBVersion ( PatchName, PatchTime, VersionNumber ) VALUES ('Initial','" + $patchTime + "', 0) END"
	$SqlCmd1.Connection = $SqlConnection
	$reqult1 = $SqlCmd1.ExecuteNonQuery()	

	#Get a list of all patches that is to be ran (file system)
	$Dir = get-childitem $sourceFolder -recurse
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

	write-host ("Applying patches to database '" + $SqlConnection.Database + "' on server '" + $SqlConnection.DataSource + "'...")

	foreach ($item in $sort | Sort-Object Number | Select-Object Name, fullname) 
	{
		StartTran

		#Check if this perticular item
		$SqlCmd2 = New-Object System.Data.SqlClient.SqlCommand
		$SqlCmd2.CommandText = "SELECT * FROM DBVersion WHERE PatchName='" + $item.Name + "'"
		$SqlCmd2.Connection = $SqlConnection
		$result2 = $SqlCmd2.ExecuteReader()		
		if($result2.Read())
		{
			$version = $result2[2]
			$checkSum = $result2[3]
		}
		else
		{
			$version = -1
			$checkSum = '?'
		}
		$result2.Close()
		
		if ($version -gt -1)
		{
			write-host ($version.ToString().PadLeft(3) + " Already applied '" + $item.Name + "'") -foreground "yellow"			
			$failCounter = $failCounter + 0
		} 
		else 
		{	
			#write-host ("Starting to run patch '" + $item.Name + "' at time " + $patchTime + ".") -foreground "magenta"
		
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
					#"--- Executing patch ---"
					#$section
					#"GO"
					$result4 = $SqlCmd4.ExecuteScalar()
				}
				Catch
				{
					$failCounter = $failCounter + 1
					write-host ("  - Failed '" + $item.Name + "'") -foreground "red"
					write-host ($error[0]) -ForegroundColor "red"
					RollbackTran
					exit 1
					return
				}
				
				if ($failCounter -gt 0)
				{
					RollbackTran
					exit $failCounter
					return
				}
			}
			
			#GET THE DATABASE VERSION
			$SqlCmd5 = New-Object System.Data.SqlClient.SqlCommand
			$SqlCmd5.Connection = $SqlConnection
			$SqlCmd5.CommandText = "SELECT ISNULL(MAX(VersionNumber),0) + 1 AS NextVersion FROM DBVersion"
			$result5 = $SqlCmd5.ExecuteReader()
			if($result5.Read())
			{
				$version = $result5[0]
			}
			$result5.Close()
		
			#GET THE CHECKSUM
			$SqlCmdCS = New-Object System.Data.SqlClient.SqlCommand
			$SqlCmdCS.Connection = $SqlConnection
			$SqlCmdCS.CommandText = "SELECT CHECKSUM_AGG(cz) FROM (
--SPs and views
SELECT checksum(DEFINITION) cz FROM sys.sql_modules
UNION ALL
--Tables
SELECT CHECKSUM(
t.TABLE_SCHEMA
, t.TABLE_NAME
, t.TABLE_TYPE
, c.COLUMN_NAME
, c.ORDINAL_POSITION
, c.COLUMN_DEFAULT
, c.IS_NULLABLE
, c.DATA_TYPE
, c.CHARACTER_MAXIMUM_LENGTH
, c.CHARACTER_OCTET_LENGTH
, c.NUMERIC_PRECISION
, c.NUMERIC_PRECISION_RADIX
, c.NUMERIC_SCALE
, c.DATETIME_PRECISION
, c.CHARACTER_SET_CATALOG
, c.CHARACTER_SET_SCHEMA
, c.CHARACTER_SET_NAME
, c.COLLATION_CATALOG
, c.COLLATION_SCHEMA
, c.COLLATION_NAME
, c.DOMAIN_SCHEMA
, c.DOMAIN_NAME) cz
FROM INFORMATION_SCHEMA.TABLES AS t LEFT OUTER JOIN
INFORMATION_SCHEMA.COLUMNS AS c ON t.TABLE_NAME = c.TABLE_NAME

) AS A"
			$resultCS = $SqlCmdCS.ExecuteReader()
			if($resultCS.Read())
			{
				$checkSum = $resultCS[0]
			}
			$resultCS.Close()
		
			#INSERT PATCH INFORMATION TO DATABASE
			$SqlCmd3 = New-Object System.Data.SqlClient.SqlCommand
			$SqlCmd3.CommandText = "INSERT INTO DBVersion ( PatchName, PatchTime, VersionNumber, Checksum ) VALUES ('" + $item.Name + "','" + $patchTime + "', " + $version + ", '" + $checkSum + "')"
			$SqlCmd3.Connection = $SqlConnection
			$result3 = $SqlCmd3.ExecuteNonQuery()
			write-host ("Applied '" + $item.Name + "'") -foreground "green"
			
			$failCounter = $failCounter + 0
		}
		
		CommitTran
	}

	write-host ("Database '" + $SqlConnection.Database + "' is patched to version " + $version + " (CheckSum: " + $checkSum + ").")

	$SqlConnection.Close()
}

exit $failCounter
