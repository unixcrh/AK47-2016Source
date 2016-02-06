CREATE TABLE [DE].[ETL_EntityJob]
(
	[Code] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
    [CreateDate] DATETIME NULL DEFAULT (getdate()), 
    [Creator] NVARCHAR(50) NULL, 
    [JobName] NVARCHAR(36) NULL, 
    [JobDescription] NVARCHAR(50) NULL, 
    [JobCategory] NVARCHAR(50) NULL, 
    [IsEnable] BIT NOT NULL DEFAULT 0, 
    [LastTime] DATETIME NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务描述',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntityJob',
    @level2type = N'COLUMN',
    @level2name = 'JobDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntityJob',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntityJob',
    @level2type = N'COLUMN',
    @level2name = N'CreateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务创建者',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntityJob',
    @level2type = N'COLUMN',
    @level2name = N'Creator'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务名称',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntityJob',
    @level2type = N'COLUMN',
    @level2name = 'JobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务分类',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntityJob',
    @level2type = N'COLUMN',
    @level2name = N'JobCategory'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否启用',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntityJob',
    @level2type = N'COLUMN',
    @level2name = N'IsEnable'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后执行时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntityJob',
    @level2type = N'COLUMN',
    @level2name = N'LastTime'