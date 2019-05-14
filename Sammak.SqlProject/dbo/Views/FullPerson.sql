CREATE VIEW [dbo].[FullPerson]
	AS 
	SELECT [p].Id AS PersonId, [p].[FirstName], [p].[LastName], 
		[a].[Id]	AS AddressId, [a].[StreetAddress], 
		[a].[City], [a].[State], [a].[ZipCode]
	
	FROM dbo.Person p
	left join dbo.Address a on p.Id = a.PersonId

