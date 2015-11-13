CREATE TABLE ProjectPageVersion (
	Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	ProjectId UNIQUEIDENTIFIER NOT NULL,
	ApplicationId UNIQUEIDENTIFIER NOT NULL,
	Version NVARCHAR(MAX) NOT NULL,
	Sessions INT NOT NULL DEFAULT 0,
	IssueTypes INT NOT NULL DEFAULT 0,
	Issues INT NOT NULL DEFAULT 0,
	Last DateTime,
	Enviroments NVARCHAR(MAX) NOT NULL,
);