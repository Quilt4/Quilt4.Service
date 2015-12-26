/*
   den 26 december 201517:05:31
   User: 
   Server: GAMMA
   Database: Quilt4
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.Issue.ClientCreationDate', N'Tmp_ClientTime_5', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.CreationDate', N'Tmp_ServerTime_6', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.Tmp_ClientTime_5', N'ClientTime', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.Tmp_ServerTime_6', N'ServerTime', 'COLUMN' 
GO
ALTER TABLE dbo.Issue SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
