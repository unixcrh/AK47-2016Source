CREATE PROCEDURE [CM].[ClearAllData]
AS
BEGIN
	TRUNCATE TABLE CM.CustomerStaffRelations
	TRUNCATE TABLE CM.CustomerRelations
	DELETE CM.Parents
	DELETE CM.PotentialCustomers
END
