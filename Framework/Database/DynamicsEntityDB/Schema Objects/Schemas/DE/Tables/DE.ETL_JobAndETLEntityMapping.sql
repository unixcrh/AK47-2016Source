CREATE TABLE [DE].[ETL_JobAndETLEntityMapping]
(
	[Code] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
    [JobCode] NVARCHAR(36) NULL, 
    [ETLEntityCode] NVARCHAR(36) NULL, 
    [Creator] NVARCHAR(36) NULL, 
    [CreateDate] DATETIME NULL DEFAULT (getdate())
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务实体表主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_JobAndETLEntityMapping',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务Code',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_JobAndETLEntityMapping',
    @level2type = N'COLUMN',
    @level2name = N'JobCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ETL实体Code',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_JobAndETLEntityMapping',
    @level2type = N'COLUMN',
    @level2name = N'ETLEntityCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_JobAndETLEntityMapping',
    @level2type = N'COLUMN',
    @level2name = N'Creator'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_JobAndETLEntityMapping',
    @level2type = N'COLUMN',
    @level2name = N'CreateDate'