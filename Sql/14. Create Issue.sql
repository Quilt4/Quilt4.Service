CREATE TABLE Issue (
	Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	IssueTypeId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES IssueType(Id),
	ClientCreationDate DATETIME NOT NULL,
	CreationDate DATETIME NOT NULL,
	LastUpdateDate DATETIME NOT NULL,
	Enviroment NVARCHAR(200) NOT NULL,
);