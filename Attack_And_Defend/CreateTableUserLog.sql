IF OBJECT_ID(N'dbo.UserLog', N'U') IS NULL CREATE TABLE UserLog(Id integer Identity(1,1) primary key,LogDateTime DateTime,LogDescription nvarchar(50), 
UserId nvarchar(50), Username nvarchar(50));