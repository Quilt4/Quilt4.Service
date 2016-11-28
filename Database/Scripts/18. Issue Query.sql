CREATE TABLE Query.Issue
(
	IssueId INT NOT NULL IDENTITY,
	IssueKey UNIQUEIDENTIFIER NOT NULL,
	[Level] nvarchar(50) NOT NULL,
	Ticket int NOT NULL,
	[Message] nvarchar(max) NULL,
	StackTrace nvarchar(max) NULL,
	Type varchar(max) NOT NULL,
	ProjectName varchar(50) NOT NULL,
	ApplicationName varchar(50) NOT NULL,
	VersionNumber varchar(128) NOT NULL,
	CONSTRAINT PK_Query_Issue PRIMARY KEY CLUSTERED ( IssueId )
)