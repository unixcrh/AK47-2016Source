CREATE PROCEDURE [SC].[ClearHistoryData]
	@cutOfDay DATETIME = NULL
AS
BEGIN
	/*
	删除@curOfDay之前的历史数据。这个删除操作不包含日志数据。仅仅是不同时间点的穿越数据
	*/
	IF @cutOfDay IS NULL
		SET @cutOfDay = GETDATE()

	DELETE SC.Acl
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.Conditions
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaApplicationSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaGroupSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaMembers
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaMembersSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaObject
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaObjectSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaOrganizationSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaPermissionSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaRelationObjects
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaRelationObjectsSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaRoleSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.SchemaUserSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay

	DELETE SC.UserAndContainerSnapshot
	WHERE VersionStartTime < @cutOfDay AND VersionEndTime < @cutOfDay
END