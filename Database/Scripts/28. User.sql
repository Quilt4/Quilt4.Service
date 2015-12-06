CREATE TABLE [User]
(
	UserId INT NOT NULL IDENTITY PRIMARY KEY,
	UserName nvarchar(50) NOT NULL,
	Email nvarchar(512) NULL,
	EmailConfirmed bit NOT NULL,
	PasswordHash nvarchar(MAX) NULL,
	CreateTime datetime NOT NULL
)
GO
ALTER TABLE [Project]
ADD OwnerUserId INT NOT NULL
GO
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
ALTER TABLE dbo.Project ADD CONSTRAINT
	FK_Project_User FOREIGN KEY
	(
	OwnerUserId
	) REFERENCES dbo.[User]
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Project SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Project', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Project', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Project', 'Object', 'CONTROL') as Contr_Per 