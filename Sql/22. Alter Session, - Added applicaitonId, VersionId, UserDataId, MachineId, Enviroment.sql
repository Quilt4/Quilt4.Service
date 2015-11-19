ALTER TABLE Session
ADD ApplicationId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Application(Id)

ALTER TABLE Session
ADD VersionId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Version(Id)

ALTER TABLE Session
ADD UserDataId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES UserData(Id)

ALTER TABLE Session
ADD MachineId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Machine(Id)

ALTER TABLE Session
ADD Enviroment NVARCHAR(200) NOT NULL