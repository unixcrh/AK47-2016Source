/* 获取组织机构树xml数据 */
CREATE FUNCTION [dbo].[GetOrgTreeXml]
(
	@MDP_GUID NVARCHAR(38)
)
RETURNS XML
AS
BEGIN
	DECLARE @result XML

	SELECT @result = 
		(SELECT 
		(CASE WHEN PARENT_NODE_NAME='' THEN 'e588c4c6-4097-4979-94c2-9e2429989932'  ELSE SETID+'-'+PARENT_NODE_NAME END) AS [ParentID]
		,(SETID+'-'+TREE_NODE) AS ID
		,'Organizations' AS ParentSchemaType
		,'Organizations' AS ChildSchemaType
		,CONVERT(NVARCHAR(64),CONVERT(BIGINT,TREE_NODE_NUM)) as InnerSort 
		,'true' AS [Default]
		--,0 AS InnerSort
		,''  AS FullPath
		,'' AS GlobalSort
	FROM MDM.ORG_TREE_ALL AS [Object] WHERE MDP_GUID = @MDP_GUID FOR XML AUTO)

	RETURN @result
END