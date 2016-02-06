CREATE TABLE [DE].[ETL_CommonDataDictionary]
(
	[Code] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
    [Key] NVARCHAR(20) NULL, 
    [Value] NVARCHAR(20) NULL, 
    [Category] NVARCHAR(36) NULL, 
    [CreateDate] DATETIME NULL DEFAULT (getdate()), 
    [SortNumber] INT NULL, 
    [IsDelete] BIT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'公共数据主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_CommonDataDictionary',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'公共数据的Key',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_CommonDataDictionary',
    @level2type = N'COLUMN',
    @level2name = N'Key'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'公共数据的Value',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_CommonDataDictionary',
    @level2type = N'COLUMN',
    @level2name = N'Value'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'公共数据的类别',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_CommonDataDictionary',
    @level2type = N'COLUMN',
    @level2name = N'Category'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'公共数据创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_CommonDataDictionary',
    @level2type = N'COLUMN',
    @level2name = N'CreateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'公共数据排序号',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_CommonDataDictionary',
    @level2type = N'COLUMN',
    @level2name = N'SortNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否删除',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_CommonDataDictionary',
    @level2type = N'COLUMN',
    @level2name = N'IsDelete'
GO
