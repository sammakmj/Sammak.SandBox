CREATE TABLE [dbo].[Person] (
    [PersonId]  INT        IDENTITY (1, 1) NOT NULL,
    [FirstName] NCHAR (50) NULL,
    [LastName]  NCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([PersonId] ASC)
);

