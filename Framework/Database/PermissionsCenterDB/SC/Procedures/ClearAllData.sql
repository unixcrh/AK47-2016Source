-- 清除所有数据，不会清除拼音表数据

CREATE PROCEDURE [SC].[ClearAllData]
AS
BEGIN
	SET NOCOUNT ON;

	EXECUTE SC.ClearNoSchemaData

    TRUNCATE TABLE SC.SchemaDefine
	TRUNCATE TABLE SC.SchemaPropertyDefine
END
