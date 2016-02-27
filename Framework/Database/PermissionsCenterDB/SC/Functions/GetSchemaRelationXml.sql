/*用于学大教育数据同步。 获取对象关系xml数据 */
CREATE FUNCTION [SC].[GetSchemaRelationXml]
(
	@ParentID NVARCHAR(36),
	@ID NVARCHAR(36),
	@ParentSchemaType NVARCHAR(32),
	@ChildSchemaType NVARCHAR(32),
	@IsDefault INT,
	@InnerSort BIGINT
)
RETURNS XML
AS
BEGIN
	DECLARE @result XML

	DECLARE @tempTable TABLE(ParentID NVARCHAR(36), [ID] NVARCHAR(36), ParentSchemaType NVARCHAR(32), ChildSchemaType NVARCHAR(32), [Default] NVARCHAR(32),
		InnerSort BIGINT, FullPath NVARCHAR(414), GlobalSort NVARCHAR(414))

	INSERT INTO @tempTable(ParentID, [ID], ParentSchemaType, ChildSchemaType, [Default], InnerSort, FullPath, GlobalSort)
	SELECT @ParentID, @ID, @ParentSchemaType, @ChildSchemaType, 
	(CASE WHEN @IsDefault = 0 THEN 'False' ELSE 'True' END ) AS [Default],
	@InnerSort,
	'',
	''

	SELECT @result = 
		(SELECT * FROM @tempTable AS [Object] FOR XML AUTO)

	RETURN @result
END
