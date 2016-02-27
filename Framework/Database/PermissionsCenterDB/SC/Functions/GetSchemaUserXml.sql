/*用于学大教育数据同步。 获取组织机构xml数据 */
CREATE FUNCTION [SC].[GetSchemaUserXml]
(
	@ID NVARCHAR(36),
	@Name NVARCHAR(255),
	@DisplayName NVARCHAR(255),
	@LastName NVARCHAR(255),
	@FirstName NVARCHAR(255),
	@CodeName NVARCHAR(64),
	@Mail NVARCHAR(255),
	@WP NVARCHAR(255),
	@MP NVARCHAR(255),
	@Description NVARCHAR(255)
)
RETURNS XML
AS
BEGIN
	DECLARE @tempTable TABLE([ID] NVARCHAR(36), Name NVARCHAR(255), DisplayName NVARCHAR(255), LastName NVARCHAR(255), FirstName NVARCHAR(255), CodeName NVARCHAR(64),
		Mail NVARCHAR(64), WP NVARCHAR(64), MP NVARCHAR(64), [Description] NVARCHAR(255))

	DECLARE @result XML

	INSERT INTO @tempTable([ID], Name, DisplayName, LastName, FirstName, CodeName, Mail, WP, MP, [Description])
	VALUES(@ID, @Name, @DisplayName, @LastName, @FirstName, @CodeName, @Mail, @WP, @MP, @Description)

	SELECT @result = 
		(SELECT * FROM @tempTable AS [Object] FOR XML AUTO)

	RETURN @result;
END
