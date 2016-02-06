CREATE PROCEDURE [DE].[ClearAllData]
AS
BEGIN
	SET NOCOUNT ON;

    DELETE DE.Acl
    TRUNCATE TABLE DE.Categories
	DELETE DE.EntityFieldSnapshot
	TRUNCATE TABLE  DE.EntityInstance
	DELETE DE.EntityMappingItemSnapshot
	DELETE DE.EntityMappingSnapshot
	DELETE DE.EntitySnapshot
	TRUNCATE TABLE  DE.ETL_CommonDataDictionary
	TRUNCATE TABLE  DE.ETL_EntityJob
	TRUNCATE TABLE  DE.ETL_EntitySchedule
	TRUNCATE TABLE  DE.ETL_ErrorLog
	TRUNCATE TABLE  DE.ETL_ErrorLogMiddleTab
	TRUNCATE TABLE  DE.ETL_JobAndAutoMapping
	TRUNCATE TABLE  DE.ETL_JobAndETLEntityMapping
	TRUNCATE TABLE  DE.ETL_JobAndScheduleMapping
	TRUNCATE TABLE  DE.ETL_JobAndWhereConditionMapping
	TRUNCATE TABLE  DE.ETL_OutEntityWhereCondition
	TRUNCATE TABLE  DE.ETL_WhereCondition
	TRUNCATE TABLE  DE.[Locks]
	TRUNCATE TABLE  DE.OperationLog
	TRUNCATE TABLE  DE.RoleUserError
	TRUNCATE TABLE  DE.ScheduleExecutionCycle
	TRUNCATE TABLE  DE.SchemaMembers
	DELETE DE.SchemaMembersSnapshot
	TRUNCATE TABLE  DE.SchemaObject
	DELETE DE.SchemaObjectSnapshot
	TRUNCATE TABLE  DE.ETL_EntityPropertiesMapping


END