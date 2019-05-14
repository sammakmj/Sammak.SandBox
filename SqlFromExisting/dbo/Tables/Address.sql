CREATE TABLE [dbo].[Address] (
    [Id]            INT        IDENTITY (1, 1) NOT NULL,
    [PersonId]      INT        NOT NULL,
    [StreetAddress] NCHAR (50) NULL,
    [City]          NCHAR (50) NULL,
    [State]         NCHAR (10) NULL,
    [ZipCode]       NCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Address_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([PersonId])
);

