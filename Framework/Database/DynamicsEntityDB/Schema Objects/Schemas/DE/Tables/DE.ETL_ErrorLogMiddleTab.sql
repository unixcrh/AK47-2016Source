CREATE TABLE [DE].[ETL_ErrorLogMiddleTab]
(
	[Code] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
    [InsertSql] NVARCHAR(MAX) NULL, 
    [CreateDate] DATETIME NULL DEFAULT (getdate()), 
    [Creator] NVARCHAR(36) NULL, 
    [ErrorMessage] NVARCHAR(800) NULL, 
    [EntityCodes] NVARCHAR(3600) NULL, 
    [ErrorType] NVARCHAR(20) NULL, 
    [JobId] NVARCHAR(36) NULL, 
    [EntityCategory] NVARCHAR(36) NULL, 
    [IsSuccess] BIT NULL 
)
GO

  Create nonclustered Index searchIndex on [DE].[ETL_ErrorLogMiddleTab] ([EntityCodes], [EntityCategory],[IsSuccess]);

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'容错日志主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'容错日志SQL',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = N'InsertSql'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'报错时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = N'CreateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作者',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = N'Creator'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'错误信息',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = N'ErrorMessage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'报错实体',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = 'EntityCodes'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'报错类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = N'ErrorType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'执行的任务ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = N'JobId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'此次执行数据的标记',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = N'EntityCategory'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否成功日志',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_ErrorLogMiddleTab',
    @level2type = N'COLUMN',
    @level2name = N'IsSuccess'