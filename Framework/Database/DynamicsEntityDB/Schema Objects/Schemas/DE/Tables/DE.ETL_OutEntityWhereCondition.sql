CREATE TABLE [DE].[ETL_OutEntityWhereCondition]
(
	[Code] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
	[OutEntityId] NVARCHAR(36) NULL, 
    [FileldName] NVARCHAR(20) NULL, 
    [Opration] NVARCHAR(10) NULL, 
    [FileldValue] NVARCHAR(100) NULL, 
    [IsDelete] BIT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'外部实体条件主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_OutEntityWhereCondition',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'外部实体主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_OutEntityWhereCondition',
    @level2type = N'COLUMN',
    @level2name = N'OutEntityId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'字段名称',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_OutEntityWhereCondition',
    @level2type = N'COLUMN',
    @level2name = N'FileldName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_OutEntityWhereCondition',
    @level2type = N'COLUMN',
    @level2name = N'Opration'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'字段值',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_OutEntityWhereCondition',
    @level2type = N'COLUMN',
    @level2name = N'FileldValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否删除',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_OutEntityWhereCondition',
    @level2type = N'COLUMN',
    @level2name = N'IsDelete'