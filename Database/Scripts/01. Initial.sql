CREATE SCHEMA Query
GO
CREATE TABLE dbo.[User]
(
	UserId INT NOT NULL IDENTITY,
	UserKey nvarchar(128) NOT NULL,
	UserName varchar(128) NOT NULL,
	Email nvarchar(512) NULL,
	EmailConfirmed bit NOT NULL,
	PasswordHash nvarchar(MAX) NULL,
	CreateServerTime datetime NOT NULL,
	CONSTRAINT PK_User PRIMARY KEY CLUSTERED ( UserId ) 
)
GO
ALTER TABLE dbo.[User] ADD CONSTRAINT User_UserKey UNIQUE (UserKey)
GO
ALTER TABLE dbo.[User] ADD CONSTRAINT User_UserName UNIQUE (UserName)
GO
CREATE TABLE dbo.[Role]
(
	RoleId INT NOT NULL IDENTITY,
	RoleName varchar(128) NOT NULL,
	CONSTRAINT PK_Role PRIMARY KEY CLUSTERED ( RoleId ) 
)
GO
ALTER TABLE dbo.[Role] ADD CONSTRAINT Role_RoleName UNIQUE (RoleName)
GO
CREATE TABLE dbo.[UserRole]
(
	UserId INT NOT NULL,
	RoleId INT NOT NULL,
	CONSTRAINT PK_UserRole PRIMARY KEY CLUSTERED ( UserId, RoleId ),
	CONSTRAINT FK_UserRole_User FOREIGN KEY (UserId) REFERENCES [User](UserId),
	CONSTRAINT FK_UserRole_Role FOREIGN KEY (RoleId) REFERENCES [Role](RoleId),
)
GO
CREATE TABLE Query.DashboardPageProject
(
	DashboardPageProjectId INT NOT NULL IDENTITY,
	ProjectKey UNIQUEIDENTIFIER NOT NULL,
	Name VARCHAR(50) NOT NULL,
	VersionCount INT NOT NULL DEFAULT 0,
	SessionCount INT NOT NULL DEFAULT 0,
	IssueTypeCount INT NOT NULL DEFAULT 0,
	IssueCount INT NOT NULL DEFAULT 0,
	DashboardColor VARCHAR(20) NOT NULL,
	CONSTRAINT PK_DashboardPageProject PRIMARY KEY CLUSTERED ( DashboardPageProjectId ) 
)
GO
ALTER TABLE Query.DashboardPageProject ADD CONSTRAINT DashboardPageProject_ProjectKey UNIQUE (ProjectKey)
GO
CREATE TABLE Query.ProjectPageProject
(
	ProjectPageProjectId INT NOT NULL IDENTITY,
	ProjectKey UNIQUEIDENTIFIER NOT NULL,
	Name VARCHAR(50) NOT NULL,
	DashboardColor VARCHAR(20) NOT NULL,
	ProjectApiKey VARCHAR(50) NOT NULL,
	CONSTRAINT PK_ProjectPageProject PRIMARY KEY CLUSTERED ( ProjectPageProjectId ) 
);
GO
ALTER TABLE Query.ProjectPageProject ADD CONSTRAINT ProjectPageProject_ProjectKey UNIQUE (ProjectKey)
GO
CREATE TABLE Query.ProjectPageApplication
(
	ProjectPageApplicationId INT NOT NULL IDENTITY,
	ProjectKey UNIQUEIDENTIFIER NOT NULL,
	ApplicationKey UNIQUEIDENTIFIER NOT NULL,
	Name VARCHAR(50) NOT NULL,
	VersionCount INT NOT NULL DEFAULT 0,
	CONSTRAINT PK_ProjectPageApplication PRIMARY KEY CLUSTERED ( ProjectPageApplicationId ) 
);
GO
ALTER TABLE Query.ProjectPageApplication ADD CONSTRAINT ProjectPageApplication_ProjectKey UNIQUE (ProjectKey)
GO
ALTER TABLE Query.ProjectPageApplication ADD CONSTRAINT ProjectPageApplication_ApplicationKey UNIQUE (ApplicationKey)
GO
CREATE TABLE Query.ProjectPageVersion
(
	ProjectPageVersionId INT NOT NULL IDENTITY,
	ProjectKey UNIQUEIDENTIFIER NOT NULL,
	ApplicationKey UNIQUEIDENTIFIER NOT NULL,
	VersionKey UNIQUEIDENTIFIER NOT NULL,
	VersionNumber VARCHAR(128) NOT NULL,
	SessionCount INT NOT NULL DEFAULT 0,
	IssueTypeCount INT NOT NULL DEFAULT 0,
	IssueCount INT NOT NULL DEFAULT 0,
	LastUpdateServerTime DateTime,
	Enviroments NVARCHAR(MAX) NULL,
	CONSTRAINT PK_ProjectPageVersion PRIMARY KEY CLUSTERED ( ProjectPageVersionId ) 
);
GO
ALTER TABLE Query.ProjectPageVersion ADD CONSTRAINT ProjectPageVersion_ProjectKey UNIQUE (ProjectKey)
GO
ALTER TABLE Query.ProjectPageVersion ADD CONSTRAINT ProjectPageVersion_ApplicationKey UNIQUE (ApplicationKey)
GO
ALTER TABLE Query.ProjectPageVersion ADD CONSTRAINT ProjectPageVersion_VersionKey UNIQUE (VersionKey)
GO
CREATE TABLE Query.VersionPageVersion
(
	VersionPageVersionId INT NOT NULL IDENTITY,
	ProjectKey UNIQUEIDENTIFIER NOT NULL,
	ApplicationKey UNIQUEIDENTIFIER NOT NULL,
	VersionKey UNIQUEIDENTIFIER NOT NULL,
	ProjectName VARCHAR(50) NOT NULL,
	ApplicationName VARCHAR(1024) NOT NULL,
	VersionNumber VARCHAR(128) NOT NULL
	CONSTRAINT PK_VersionPageVersion PRIMARY KEY CLUSTERED ( VersionPageVersionId )
);
GO
ALTER TABLE Query.VersionPageVersion ADD CONSTRAINT VersionPageVersion_ProjectKey UNIQUE (ProjectKey)
GO
ALTER TABLE Query.VersionPageVersion ADD CONSTRAINT VersionPageVersion_ApplicationKey UNIQUE (ApplicationKey)
GO
ALTER TABLE Query.VersionPageVersion ADD CONSTRAINT VersionPageVersion_VersionKey UNIQUE (VersionKey)
GO
CREATE TABLE Query.VersionPageIssueType
(
	VersionPageIssueTypeId INT NOT NULL IDENTITY,
	ProjectKey UNIQUEIDENTIFIER NOT NULL,
	ApplicationKey UNIQUEIDENTIFIER NOT NULL,
	VersionKey UNIQUEIDENTIFIER NOT NULL,
	IssueTypeKey UNIQUEIDENTIFIER NOT NULL,
	Ticket INT NOT NULL,
	[Type] VARCHAR(2048) NOT NULL,
	IssueCount INT NOT NULL DEFAULT 0,
	[Level] NVARCHAR(50) NOT NULL,
	LastIssueServerTime DATETIME,
	[Message] NVARCHAR(MAX) NULL,
	Enviroments NVARCHAR(MAX) NULL,
	CONSTRAINT PK_VersionPageIssueType PRIMARY KEY CLUSTERED ( VersionPageIssueTypeId )
);
GO
ALTER TABLE Query.VersionPageIssueType ADD CONSTRAINT VersionPageIssueType_ProjectKey UNIQUE (ProjectKey)
GO
ALTER TABLE Query.VersionPageIssueType ADD CONSTRAINT VersionPageIssueType_ApplicationKey UNIQUE (ApplicationKey)
GO
ALTER TABLE Query.VersionPageIssueType ADD CONSTRAINT VersionPageIssueType_VersionKey UNIQUE (VersionKey)
GO
ALTER TABLE Query.VersionPageIssueType ADD CONSTRAINT VersionPageIssueType_IssueTypeKey UNIQUE (IssueTypeKey)
GO
CREATE TABLE Query.IssueTypePageIssueType
(
	IssueTypePageIssueTypeId INT NOT NULL IDENTITY,
	ProjectKey UNIQUEIDENTIFIER NOT NULL,
	ApplicationKey UNIQUEIDENTIFIER NOT NULL,
	VersionKey UNIQUEIDENTIFIER NOT NULL,
	IssueTypeKey UNIQUEIDENTIFIER NOT NULL,
	ProjectName VARCHAR(50) NOT NULL,
	ApplicationName VARCHAR(1024) NOT NULL,
	VersionNumber VARCHAR(128) NOT NULL,
	Ticket INT NOT NULL,
	[Type] VARCHAR(2048) NOT NULL,
	[Level] NVARCHAR(50) NOT NULL,
	[Message] NVARCHAR(MAX) NULL,
	StackTrace NVARCHAR(MAX) NULL,
	CONSTRAINT PK_IssueTypePageIssueType PRIMARY KEY CLUSTERED ( IssueTypePageIssueTypeId )
);
GO
ALTER TABLE Query.IssueTypePageIssueType ADD CONSTRAINT IssueTypePageIssueType_ProjectKey UNIQUE (ProjectKey)
GO
ALTER TABLE Query.IssueTypePageIssueType ADD CONSTRAINT IssueTypePageIssueType_ApplicationKey UNIQUE (ApplicationKey)
GO
ALTER TABLE Query.IssueTypePageIssueType ADD CONSTRAINT IssueTypePageIssueType_VersionKey UNIQUE (VersionKey)
GO
ALTER TABLE Query.IssueTypePageIssueType ADD CONSTRAINT IssueTypePageIssueType_IssueTypeKey UNIQUE (IssueTypeKey)
GO
CREATE TABLE Query.IssueTypePageIssue 
(
	IssueTypePageIssueId INT NOT NULL IDENTITY,
	ProjectKey UNIQUEIDENTIFIER NOT NULL,
	ApplicationKey UNIQUEIDENTIFIER NOT NULL,
	VersionKey UNIQUEIDENTIFIER NOT NULL,
	IssueTypeKey UNIQUEIDENTIFIER NOT NULL,
	LastUpdateServerTime DATETIME NOT NULL,
	ApplicationUserName VARCHAR(128) NULL,
	Enviroment NVARCHAR(128) NULL,
	Data NVARCHAR(MAX) NULL,
	CONSTRAINT PK_IssueTypePageIssue PRIMARY KEY CLUSTERED ( IssueTypePageIssueId )
);
GO
ALTER TABLE Query.IssueTypePageIssue ADD CONSTRAINT IIssueTypePageIssue_ProjectKey UNIQUE (ProjectKey)
GO
ALTER TABLE Query.IssueTypePageIssue ADD CONSTRAINT IssueTypePageIssue_ApplicationKey UNIQUE (ApplicationKey)
GO
ALTER TABLE Query.IssueTypePageIssue ADD CONSTRAINT IssueTypePageIssue_VersionKey UNIQUE (VersionKey)
GO
ALTER TABLE Query.IssueTypePageIssue ADD CONSTRAINT IssueTypePageIssue_IssueTypeKey UNIQUE (IssueTypeKey)
GO
CREATE TABLE dbo.Project
(
	ProjectId INT NOT NULL IDENTITY,
	ProjectKey UNIQUEIDENTIFIER NOT NULL,
	CreationServerTime DATETIME NOT NULL,
	Name VARCHAR(50) NOT NULL,
	DashboardColor VARCHAR(20) NOT NULL,
	ProjectApiKey VARCHAR(50) NOT NULL,
	LastTicket INT NOT NULL DEFAULT 0,
	OwnerUserId INT NOT NULL,
	CONSTRAINT PK_Project PRIMARY KEY CLUSTERED ( ProjectId ),
	CONSTRAINT FK_Project_User FOREIGN KEY (OwnerUserId) REFERENCES [User](UserId)
);
GO
ALTER TABLE Project ADD CONSTRAINT Project_ProjectApiKey UNIQUE (ProjectApiKey)
GO
ALTER TABLE Project ADD CONSTRAINT Project_ProjectKey UNIQUE (ProjectKey)
GO
CREATE TABLE ProjectUser
(
	ProjectUserId INT NOT NULL IDENTITY,
	ProjectId INT NOT NULL,
	UserId INT NOT NULL,
	[Role] varchar(50) NOT NULL,
	CONSTRAINT PK_ProjectUser PRIMARY KEY CLUSTERED ( ProjectUserId ),
	CONSTRAINT FK_ProjectUser_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
	CONSTRAINT FK_ProjectUser_User FOREIGN KEY (UserId) REFERENCES [User](UserId),
);
GO
ALTER TABLE ProjectUser ADD CONSTRAINT ProjectUser_Project_User UNIQUE (ProjectId,UserId)
GO
CREATE TABLE ProjectInvitation
(
	ProjectInvitationId INT NOT NULL IDENTITY,
	ProjectId INT NOT NULL,
	UserId INT NULL,
	UserEmail nvarchar(512) NULL,
	InviterUserId INT NULL,
	InviteCode VARCHAR(50) NOT NULL,
	ServerCreateTime DATETIME NOT NULL,
	CONSTRAINT PK_ProjectInvitation PRIMARY KEY CLUSTERED ( ProjectInvitationId ),
	CONSTRAINT FK_ProjectInvitation_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
	CONSTRAINT FK_ProjectInvitation_User FOREIGN KEY (UserId) REFERENCES [User](UserId),
	CONSTRAINT FK_ProjectInvitation_InviterUser FOREIGN KEY (InviterUserId) REFERENCES [User](UserId),
)
GO
ALTER TABLE ProjectInvitation ADD CONSTRAINT ProjectInvitation_InviteCode UNIQUE (InviteCode)
GO
CREATE TABLE dbo.[Application]
(
	ApplicationId INT NOT NULL IDENTITY,
	ApplicationKey UNIQUEIDENTIFIER NOT NULL,
	ProjectId INT NOT NULL,
	Name VARCHAR(50) NOT NULL,
	CreationServerTime DATETIME NOT NULL,
	CONSTRAINT PK_Application PRIMARY KEY CLUSTERED ( ApplicationId ),
	CONSTRAINT FK_Application_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId)
);
GO
ALTER TABLE [Application] ADD CONSTRAINT Application_ApplicationKey UNIQUE (ApplicationKey)
GO
CREATE UNIQUE NONCLUSTERED INDEX [ApplicationSignature] ON [dbo].[Application]
(
	[ProjectId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE TABLE dbo.[Version]
(
	VersionId INT NOT NULL IDENTITY,
	VersionKey UNIQUEIDENTIFIER NOT NULL,
	ApplicationId INT NOT NULL,
	VersionNumber VARCHAR(128) NOT NULL,
	BuildTime DATETIME NULL,
	SupportToolkitVersion VARCHAR(1024) NOT NULL,
	CreationServerTime DATETIME NOT NULL,
	CONSTRAINT PK_Version PRIMARY KEY CLUSTERED ( VersionId ),
	CONSTRAINT FK_Version_Application FOREIGN KEY (ApplicationId) REFERENCES [Application](ApplicationId)
);
GO
ALTER TABLE [Version] ADD CONSTRAINT Version_VersionKey UNIQUE (VersionKey)
GO
CREATE UNIQUE NONCLUSTERED INDEX [VersionSignature] ON [dbo].[Version]
(
	[ApplicationId] ASC,
	[VersionNumber] ASC,
	[BuildTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE TABLE dbo.IssueType
(
	IssueTypeId INT NOT NULL IDENTITY,
	IssueTypeKey UNIQUEIDENTIFIER NOT NULL,
	VersionId INT NOT NULL,
	[Type] VARCHAR(2048) NOT NULL,
	[Level] NVARCHAR(50) NOT NULL,
	[Message] NVARCHAR(MAX) NULL,
	StackTrace NVARCHAR(MAX) NULL,
	Ticket INT NOT NULL,
	CreationServerTime DATETIME NOT NULL,
	CONSTRAINT PK_IssueType PRIMARY KEY CLUSTERED ( IssueTypeId ),
	CONSTRAINT FK_IssueType_Version FOREIGN KEY (VersionId) REFERENCES [Version](VersionId)
);
GO
ALTER TABLE [IssueType] ADD CONSTRAINT IssueType_IssueTypeKey UNIQUE (IssueTypeKey)
GO
CREATE TABLE dbo.Machine
(
	MachineId INT NOT NULL IDENTITY,
	MachineKey UNIQUEIDENTIFIER NOT NULL,
	ProjectId INT NOT NULL,
	Fingerprint VARCHAR(128) NOT NULL,
	Name VARCHAR(128) NOT NULL,
	CreationServerTime DATETIME NOT NULL,
	CONSTRAINT PK_Machine PRIMARY KEY CLUSTERED ( MachineId ),
	CONSTRAINT FK_Machine_ProjectId FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId)
);
GO
ALTER TABLE Machine ADD CONSTRAINT Machine_MachineKey UNIQUE (MachineKey)
GO
CREATE TABLE dbo.MachineData
(
	MachineDataId INT NOT NULL IDENTITY,
	MachineId INT NOT NULL,
	CreationServerTime DATETIME NOT NULL,
	Name VARCHAR(1024) NOT NULL,
	Value VARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_MachineData PRIMARY KEY CLUSTERED ( MachineDataId ),
	CONSTRAINT FK_MachineData_Machine FOREIGN KEY (MachineId) REFERENCES [Machine](MachineId)
);
GO
CREATE TABLE dbo.ApplicationUser
(
	ApplicationUserId INT NOT NULL IDENTITY,
	ApplicationUserKey UNIQUEIDENTIFIER NOT NULL,
	ProjectId INT NOT NULL,
	Fingerprint VARCHAR(128) NOT NULL,
	UserName VARCHAR(128) NOT NULL,
	CreationServerTime DATETIME NOT NULL,
	CONSTRAINT PK_ApplicationUser PRIMARY KEY CLUSTERED ( ApplicationUserId ),
	CONSTRAINT FK_ApplicationUser_ProjectId FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId)
);
GO
ALTER TABLE ApplicationUser ADD CONSTRAINT ApplicationUser_ApplicationUserKey UNIQUE (ApplicationUserKey)
GO
CREATE TABLE dbo.[Session]
(
	SessionId INT NOT NULL IDENTITY,
	SessionKey UNIQUEIDENTIFIER NOT NULL,
	StartClientTime DATETIME NOT NULL,
	StartServerTime DATETIME NOT NULL,
	LastUsedServerTime DATETIME NOT NULL,
	EndServerTime DATETIME NULL,
	CallerIp VARCHAR(45) NOT NULL,
	Enviroment NVARCHAR(128) NULL,
	MachineId INT NOT NULL,
	ApplicationUserId INT NOT NULL,
	VersionId INT NOT NULL,
	CONSTRAINT PK_Session PRIMARY KEY CLUSTERED ( SessionId ),
	CONSTRAINT FK_Issue_Machine FOREIGN KEY (MachineId) REFERENCES [Machine](MachineId),
	CONSTRAINT FK_Issue_ApplicationUser FOREIGN KEY (ApplicationUserId) REFERENCES ApplicationUser(ApplicationUserId),
	CONSTRAINT FK_Issue_Version FOREIGN KEY (VersionId) REFERENCES [Version](VersionId),
);
GO
ALTER TABLE [Session] ADD CONSTRAINT Issue_SessionKey UNIQUE (SessionKey)
GO
CREATE TABLE dbo.Issue
(
	IssueId INT NOT NULL IDENTITY,
	IssueKey UNIQUEIDENTIFIER NOT NULL,
	IssueTypeId INT NOT NULL,
	CreationClientTime DATETIME NOT NULL,
	CreationServerTime DATETIME NOT NULL,
	SessionId INT NOT NULL,
	--MachineId INT NOT NULL,
	--ApplicationUserId INT NOT NULL,
	CONSTRAINT PK_Issue PRIMARY KEY CLUSTERED ( IssueId ),
	CONSTRAINT FK_Issue_IssueType FOREIGN KEY (IssueTypeId) REFERENCES IssueType(IssueTypeId),
	CONSTRAINT FK_Issue_Session FOREIGN KEY (SessionId) REFERENCES [Session](SessionId),
	--CONSTRAINT FK_Issue_Machine FOREIGN KEY (MachineId) REFERENCES [Machine](MachineId),
	--CONSTRAINT FK_Issue_ApplicationUser FOREIGN KEY (ApplicationUserId) REFERENCES ApplicationUser(ApplicationUserId),
);
GO
ALTER TABLE [Issue] ADD CONSTRAINT Issue_IssueKey UNIQUE (IssueKey)
GO
CREATE TABLE dbo.IssueData
(
	IssueDataId INT NOT NULL IDENTITY,
	IssueId INT NOT NULL,
	Name VARCHAR(1024) NOT NULL,
	Value VARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_IssueData PRIMARY KEY CLUSTERED ( IssueDataId ),
	CONSTRAINT FK_IssueData_Issue FOREIGN KEY (IssueId) REFERENCES Issue(IssueId)
);
GO
CREATE TABLE dbo.Setting
(
	SettingId INT NOT NULL IDENTITY,
	Name VARCHAR(128) NOT NULL,
	Value VARCHAR(1024) NOT NULL,	
	CONSTRAINT PK_Setting PRIMARY KEY CLUSTERED ( SettingId ),
);
GO
