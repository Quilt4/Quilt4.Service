UPDATE [User] SET LastName = FirstName + ' ' + LastName
ALTER TABLE [User] DROP COLUMN FirstName
EXEC sp_rename '[User].LastName', 'FullName';
