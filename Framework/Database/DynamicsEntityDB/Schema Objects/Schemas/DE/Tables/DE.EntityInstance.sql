CREATE TABLE [DE].[EntityInstance]
(
	[ID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Content] XML NULL, 
    [EntityCode] NVARCHAR(36) NULL, 
	[Data] [xml] NULL,
    [Creator] NVARCHAR(36) NULL, 
    [CreateDate] DATETIME NULL DEFAULT (getdate()), 
    [Description] NVARCHAR(255) NULL, 
    [Status] INT NULL DEFAULT ((1)) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'唯一标识',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityInstance',
    @level2type = N'COLUMN',
    @level2name = 'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'名称',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityInstance',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'内容',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityInstance',
    @level2type = N'COLUMN',
    @level2name = N'Content'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'实体标识',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityInstance',
    @level2type = N'COLUMN',
    @level2name = N'EntityCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityInstance',
    @level2type = N'COLUMN',
    @level2name = N'Creator'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityInstance',
    @level2type = N'COLUMN',
    @level2name = 'CreateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'描述',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityInstance',
    @level2type = N'COLUMN',
    @level2name = 'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityInstance',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityInstance',
    @level2type = N'COLUMN',
    @level2name = N'Data'