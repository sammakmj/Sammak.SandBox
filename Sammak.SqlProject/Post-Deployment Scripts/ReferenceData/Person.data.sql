MERGE INTO [Person] AS TARGET
USING (VALUES
	('Demo', 'User')
)
AS SOURCE 
(
	FirstName, 
	LastName
)
ON (TARGET.FirstName = SOURCE.FirstName AND TARGET.LastName = SOURCE.LastName)

WHEN MATCHED 
THEN UPDATE SET
	FirstName = SOURCE.FirstName, 
	LastName = SOURCE.LastName

WHEN NOT MATCHED BY TARGET
THEN INSERT (
	FirstName, 
	LastName
)
VALUES (
	SOURCE.FirstName, 
	SOURCE.LastName
);

--WHEN NOT MATCHED BY SOURCE
--THEN DELETE;
