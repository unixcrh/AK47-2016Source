/*用于学大教育数据同步。 获取组织机构xml数据 */
CREATE FUNCTION [SC].[GetSchemaOrganizationXml]
(
	@ID NVARCHAR(36),
	@Name NVARCHAR(255),
	@DisplayName NVARCHAR(255),
	@CodeName NVARCHAR(64),
	@DepartmentType NVARCHAR(32),
	@Description NVARCHAR(255)
)
RETURNS XML
AS
BEGIN
	DECLARE @result XML

	DECLARE @tempTable TABLE([ID] NVARCHAR(36), Name NVARCHAR(255), DisplayName NVARCHAR(255), CodeName NVARCHAR(64), DepartmentType NVARCHAR(32), [Description] NVARCHAR(255))

	INSERT INTO @tempTable([ID], Name, DisplayName, CodeName, DepartmentType, [Description])
	SELECT @ID AS [ID],
		@Name AS Name,
		@DisplayName AS DisplayName,
		@CodeName AS CodeName,
		@DepartmentType AS DepartmentType,
		@Description AS Comment

	SELECT @result = 
		(SELECT * FROM @tempTable AS [Object] FOR XML AUTO)

	RETURN @result
END
