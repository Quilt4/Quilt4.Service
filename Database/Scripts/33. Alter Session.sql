/*
   den 26 december 201516:52:29
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
ALTER TABLE dbo.Session
	DROP CONSTRAINT FK__Session__Machine__49C3F6B7
GO
ALTER TABLE dbo.Machine SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Session
	DROP CONSTRAINT FK__Session__UserDat__48CFD27E
GO
ALTER TABLE dbo.UserData SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Session
	DROP CONSTRAINT FK__Session__Version__47DBAE45
GO
ALTER TABLE dbo.Version SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Session
	DROP CONSTRAINT FK__Session__Applica__46E78A0C
GO
ALTER TABLE dbo.Application SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Session
	(
	SessionKey uniqueidentifier NOT NULL,
	ClientStartTime datetime NOT NULL,
	ServerStartTime datetime NOT NULL,
	ServerLastUsedTime datetime NULL,
	ServerEndTime datetime NULL,
	CallerIp nvarchar(45) NOT NULL,
	ApplicationKey uniqueidentifier NOT NULL,
	VersionKey uniqueidentifier NOT NULL,
	ApplicationUserKey uniqueidentifier NULL,
	MachineKey uniqueidentifier NULL,
	Enviroment nvarchar(200) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Session SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Session)
	 EXEC('INSERT INTO dbo.Tmp_Session (SessionKey, ClientStartTime, ServerStartTime, ServerLastUsedTime, ServerEndTime, CallerIp, ApplicationKey, VersionKey, ApplicationUserKey, MachineKey, Enviroment)
		SELECT Id, ClientStartTime, ServerStartTime, ServerLastUsedTime, ServerEndTime, CallerIp, ApplicationId, VersionId, UserDataId, MachineId, Enviroment FROM dbo.Session WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.Issue
	DROP CONSTRAINT FK__Issue__SessionId__3D5E1FD2
GO
DROP TABLE dbo.Session
GO
EXECUTE sp_rename N'dbo.Tmp_Session', N'Session', 'OBJECT' 
GO
ALTER TABLE dbo.Session ADD CONSTRAINT
	PK__Session__3214EC07F170E35C PRIMARY KEY CLUSTERED 
	(
	SessionKey
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Session ADD CONSTRAINT
	FK__Session__Applica__46E78A0C FOREIGN KEY
	(
	ApplicationKey
	) REFERENCES dbo.Application
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Session ADD CONSTRAINT
	FK__Session__Version__47DBAE45 FOREIGN KEY
	(
	VersionKey
	) REFERENCES dbo.Version
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Session ADD CONSTRAINT
	FK__Session__UserDat__48CFD27E FOREIGN KEY
	(
	ApplicationUserKey
	) REFERENCES dbo.UserData
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Session ADD CONSTRAINT
	FK__Session__Machine__49C3F6B7 FOREIGN KEY
	(
	MachineKey
	) REFERENCES dbo.Machine
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Issue ADD CONSTRAINT
	FK__Issue__SessionId__3D5E1FD2 FOREIGN KEY
	(
	SessionId
	) REFERENCES dbo.Session
	(
	SessionKey
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Issue SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
