CREATE TABLE ProjectPageProject (
	Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	DashboardColor NVARCHAR(20) NOT NULL DEFAULT 'blue'
);