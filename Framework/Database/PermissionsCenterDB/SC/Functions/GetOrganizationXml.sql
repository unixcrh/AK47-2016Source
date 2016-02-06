/* 获取组织机构xml数据 */
CREATE FUNCTION [dbo].[GetOrganizationXml]
(
	@MDP_GUID NVARCHAR(38)
)
RETURNS XML
AS
BEGIN
	DECLARE @result XML

	SELECT @result = 
		(SELECT SETID + '-' + DEPTID AS [ID],
		SETID + '-' + DEPTID AS CodeName
		, DESCRSHORT AS Name
		,DESCRSHORT AS DisplayName 
		, DESCR AS Comment
	FROM MDM.DEPT_ALL AS [Object] WHERE MDP_GUID = @MDP_GUID FOR XML AUTO)

	RETURN @result
END