CREATE TABLE DashboardPageProject (
	Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	Versions INT NOT NULL DEFAULT 0,
	Sessions INT NOT NULL DEFAULT 0,
	IssueTypes INT NOT NULL DEFAULT 0,
	Issues INT NOT NULL DEFAULT 0,
	DashboardColor NVARCHAR(20) NOT NULL DEFAULT 'blue'
);
