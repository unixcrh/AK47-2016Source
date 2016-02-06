/* 获取用户组织机构xml数据 */
CREATE FUNCTION [dbo].[GetOrgUserXml]
(
	@MDP_GUID NVARCHAR(38)
)
RETURNS XML
AS
BEGIN
	DECLARE @result XML

	SELECT @result = 
		(SELECT (SETID_DEPT+'-'+DEPTID) AS [ParentID]
		,EMPLID AS ID
		,'Organizations' AS ParentSchemaType
		,'Organizations' AS ChildSchemaType
		, CONVERT(NVARCHAR(64),CONVERT(BIGINT,EFFSEQ)) as InnerSort 
		,(CASE WHEN JOB_INDICATOR='P' THEN 'true' ELSE 'false' END ) AS [Default]
		,''  AS FullPath
		,'' AS GlobalSort
	FROM MDM.JOB_ALL AS [Object] WHERE MDP_GUID = @MDP_GUID FOR XML AUTO)

	RETURN @result
END