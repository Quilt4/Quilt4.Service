CREATE TABLE Machine (
	Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Fingerprint NVARCHAR(MAX) NOT NULL,
	Name NVARCHAR(MAX) NOT NULL,
	CreationDate DATETIME NOT NULL,
	LastUpdateDate DATETIME NOT NULL
);

GO

CREATE TABLE MachineData (
	Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	MachineId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Machine(Id),
	CreationDate DATETIME NOT NULL,
	LastUpdateDate DATETIME NOT NULL,
	Name NVARCHAR(MAX) NOT NULL,
	Value NVARCHAR(MAX) NOT NULL,
);

GO
