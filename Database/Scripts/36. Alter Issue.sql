/*
   den 26 december 201517:07:27
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
ALTER TABLE dbo.Issue
	DROP CONSTRAINT FK__Issue__SessionId__3D5E1FD2
GO
ALTER TABLE dbo.Session SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Issue
	DROP CONSTRAINT FK__Issue__UserDataI__45F365D3
GO
ALTER TABLE dbo.UserData SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Issue
	DROP CONSTRAINT FK__Issue__MachineId__4316F928
GO
ALTER TABLE dbo.Machine SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Issue
	DROP CONSTRAINT FK__Issue__IssueType__37A5467C
GO
ALTER TABLE dbo.IssueType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Issue
	(
	IssueKey uniqueidentifier NOT NULL,
	IssueTypeKey uniqueidentifier NOT NULL,
	ClientTime datetime NOT NULL,
	ServerTime datetime NOT NULL,
	SessionKey uniqueidentifier NOT NULL,
	MachineKey uniqueidentifier NULL,
	ApplicationUserKey uniqueidentifier NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Issue SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Issue)
	 EXEC('INSERT INTO dbo.Tmp_Issue (IssueKey, IssueTypeKey, ClientTime, ServerTime, SessionKey, MachineKey, ApplicationUserKey)
		SELECT IssueKey, IssueTypeKey, ClientTime, ServerTime, SessionKey, MachineKey, UserDataKey FROM dbo.Issue WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.IssueData
	DROP CONSTRAINT FK__IssueData__Issue__3A81B327
GO
DROP TABLE dbo.Issue
GO
EXECUTE sp_rename N'dbo.Tmp_Issue', N'Issue', 'OBJECT' 
GO
ALTER TABLE dbo.Issue ADD CONSTRAINT
	PK__Issue__3214EC07AB692649 PRIMARY KEY CLUSTERED 
	(
	IssueKey
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Issue ADD CONSTRAINT
	FK__Issue__IssueType__37A5467C FOREIGN KEY
	(
	IssueTypeKey
	) REFERENCES dbo.IssueType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Issue ADD CONSTRAINT
	FK__Issue__MachineId__4316F928 FOREIGN KEY
	(
	MachineKey
	) REFERENCES dbo.Machine
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Issue ADD CONSTRAINT
	FK__Issue__UserDataI__45F365D3 FOREIGN KEY
	(
	ApplicationUserKey
	) REFERENCES dbo.UserData
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Issue ADD CONSTRAINT
	FK__Issue__SessionId__3D5E1FD2 FOREIGN KEY
	(
	SessionKey
	) REFERENCES dbo.Session
	(
	SessionKey
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.IssueData ADD CONSTRAINT
	FK__IssueData__Issue__3A81B327 FOREIGN KEY
	(
	IssueId
	) REFERENCES dbo.Issue
	(
	IssueKey
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.IssueData SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
