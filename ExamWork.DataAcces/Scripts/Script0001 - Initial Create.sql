Create table[dbo].[Streets] 
(
 Id uniqueidentifier not null primary key,
 CreationDate datetime2 not null,
 DeletedDate datetime2,
 Name nvarchar(max) not null
)
Create table[dbo].[Cities] 
(
 Id uniqueidentifier not null primary key,
 CreationDate datetime2 not null,
 DeletedDate datetime2,
 Name nvarchar(max) not null,
 StreetId uniqueidentifier not null foreign key(StreetId) references [dbo].[Streets] (Id)
)

Create table[dbo].[Countries] 
(
 Id uniqueidentifier not null primary key,
 CreationDate datetime2 not null,
 DeletedDate datetime2,
 Name nvarchar(max) not null,
 CityId uniqueidentifier not null foreign key(CityId) references [dbo].[Cities](Id)
)