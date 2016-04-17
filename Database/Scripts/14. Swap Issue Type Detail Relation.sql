--ALTER TABLE IssueType ADD IssueTypeDetailId INT NULL

UPDATE IT
	SET IT.IssueTypeDetailId = ITD.IssueTypeDetailId
	FROM IssueType AS IT
	INNER JOIN IssueTypeDetail AS ITD ON IT.IssueTypeId = ITD.IssueTypeId

ALTER TABLE IssueTypeDetail DROP CONSTRAINT FK_IssueTypeDetail_IssueType
ALTER TABLE IssueTypeDetail DROP COLUMN IssueTypeId
ALTER TABLE IssueType ADD CONSTRAINT FK_IssueType_IssueTypeDetail FOREIGN KEY (IssueTypeDetailId) REFERENCES IssueTypeDetail(IssueTypeDetailId)
ALTER TABLE IssueType ALTER COLUMN IssueTypeDetailId INT NOT NULL
