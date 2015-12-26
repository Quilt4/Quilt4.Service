/*
   den 26 december 201517:00:56
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
EXECUTE sp_rename N'dbo.Issue.Id', N'Tmp_IssueKey', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.IssueTypeId', N'Tmp_IssueTypeKey_1', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.SessionId', N'Tmp_SessionKey_2', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.MachineId', N'Tmp_MachineKey_3', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.UserDataId', N'Tmp_UserDataKey_4', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.Tmp_IssueKey', N'IssueKey', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.Tmp_IssueTypeKey_1', N'IssueTypeKey', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.Tmp_SessionKey_2', N'SessionKey', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.Tmp_MachineKey_3', N'MachineKey', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Issue.Tmp_UserDataKey_4', N'UserDataKey', 'COLUMN' 
GO
ALTER TABLE dbo.Issue
	DROP COLUMN LastUpdateDate
GO
ALTER TABLE dbo.Issue SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
