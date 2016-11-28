CREATE TABLE dbo.ProjectTarget
(
	ProjectTargetId INT NOT NULL IDENTITY,
	ProjectId INT NOT NULL,
	TargetType nvarchar(128) NOT NULL,
	Connection nvarchar(1024) NOT NULL,
	[Enabled] bit NOT NULL,
	CONSTRAINT PK_ProjectTarget PRIMARY KEY CLUSTERED ( ProjectTargetId ),
	CONSTRAINT FK_ProjectTarget_Project FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
)