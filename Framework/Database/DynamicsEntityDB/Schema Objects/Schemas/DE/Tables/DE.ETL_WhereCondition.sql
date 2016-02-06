CREATE TABLE [DE].[ETL_WhereCondition]
(
	[ID] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
    [JOB_ID] NVARCHAR(36) NOT NULL, 
    [ETLEntity_ID] NVARCHAR(36) NOT NULL, 
    [ETLOuterEntity_ID] NVARCHAR(36) NOT NULL, 
    [Condition] NVARCHAR(2048) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_WhereCondition',
    @level2type = N'COLUMN',
    @level2name = 'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务的主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_WhereCondition',
    @level2type = N'COLUMN',
    @level2name = 'JOB_ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ETL实体主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_WhereCondition',
    @level2type = N'COLUMN',
    @level2name = N'ETLEntity_ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ETL外部实体主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_WhereCondition',
    @level2type = N'COLUMN',
    @level2name = N'ETLOuterEntity_ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'条件内容',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_WhereCondition',
    @level2type = N'COLUMN',
    @level2name = N'Condition'