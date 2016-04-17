CREATE TABLE IssueTypeDetail
(
	IssueTypeDetailId INT NOT NULL IDENTITY,
	ParentIssueTypeDetailId INT NULL,
	IssueTypeId INT NULL,
	[Message] NVARCHAR(MAX) NULL,
	StackTrace NVARCHAR(MAX) NULL,
	[Type] VARCHAR(2048) NOT NULL,
	CONSTRAINT PK_IssueTypeDetail PRIMARY KEY CLUSTERED ( IssueTypeDetailId ),
	CONSTRAINT FK_IssueTypeDetail_IssueTypeDetail FOREIGN KEY (ParentIssueTypeDetailId) REFERENCES IssueTypeDetail(IssueTypeDetailId),
	CONSTRAINT FK_IssueTypeDetail_IssueType FOREIGN KEY (IssueTypeId) REFERENCES IssueType(IssueTypeId),
)
GO
INSERT INTO IssueTypeDetail (ParentIssueTypeDetailId, IssueTypeId, [Message], StackTrace, [Type]) SELECT NULL, IssueTypeId, [Message], StackTrace, [Type] FROM IssueType
GO
ALTER TABLE IssueType DROP COLUMN [Type]
ALTER TABLE IssueType DROP COLUMN [StackTrace]
ALTER TABLE IssueType DROP COLUMN [Message]