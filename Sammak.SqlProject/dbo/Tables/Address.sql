CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NOT NULL, 
    [StreetAddress] NCHAR(50) NULL, 
    [City] NCHAR(50) NULL, 
    [State] NCHAR(10) NULL, 
    [ZipCode] NCHAR(50) NULL, 
    CONSTRAINT [FK_Address_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person](Id)
)
