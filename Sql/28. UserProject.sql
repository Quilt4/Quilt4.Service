/*
   den 29 november 201516:42:15
   User: 
   Server: DELTA
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
ALTER TABLE dbo.[User] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.[User]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Project
	(
	Id uniqueidentifier NOT NULL,
	CreationDate datetime NOT NULL,
	LastUpdateDate datetime NOT NULL,
	Name nvarchar(50) NOT NULL,
	DashboardColor nvarchar(20) NOT NULL,
	UserId int NOT NULL,
	ProjectApiKey nvarchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Project SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Project)
	 EXEC('INSERT INTO dbo.Tmp_Project (Id, CreationDate, LastUpdateDate, Name, DashboardColor, ProjectApiKey)
		SELECT Id, CreationDate, LastUpdateDate, Name, DashboardColor, ClientToken FROM dbo.Project WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.Application
	DROP CONSTRAINT FK__Applicati__Proje__3D5E1FD2
GO
DROP TABLE dbo.Project
GO
EXECUTE sp_rename N'dbo.Tmp_Project', N'Project', 'OBJECT' 
GO
ALTER TABLE dbo.Project ADD CONSTRAINT
	PK__Project__3214EC079FF0D467 PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Project ADD CONSTRAINT
	NO_DUP_TOKEN UNIQUE NONCLUSTERED 
	(
	ProjectApiKey
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Project ADD CONSTRAINT
	FK_Project_User FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.[User]
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Project', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Project', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Project', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Application ADD CONSTRAINT
	FK__Applicati__Proje__3D5E1FD2 FOREIGN KEY
	(
	ProjectId
	) REFERENCES dbo.Project
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Application SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Application', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Application', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Application', 'Object', 'CONTROL') as Contr_Per 