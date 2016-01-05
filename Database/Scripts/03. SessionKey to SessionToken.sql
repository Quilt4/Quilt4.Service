ALTER TABLE [Session] ADD SessionToken varchar(50)
GO
UPDATE [Session] SET SessionToken = SessionKey
GO
ALTER TABLE [Session] ALTER COLUMN SessionToken varchar(50) NOT NULL
GO
ALTER TABLE [Session] ADD CONSTRAINT Issue_SessionToken UNIQUE (SessionToken)
GO
ALTER TABLE [Session] DROP CONSTRAINT Issue_SessionKey
GO
ALTER TABLE [Session] DROP COLUMN SessionKey
GO