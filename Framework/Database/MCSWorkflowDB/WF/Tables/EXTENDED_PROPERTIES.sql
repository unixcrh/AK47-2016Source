CREATE TABLE [WF].[EXTENDED_PROPERTIES]
(
	[ID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
    [RESOURCE_ID] NVARCHAR(36) NULL, 
    [TYPE] NVARCHAR(36) NULL, 
    [DATA] NVARCHAR(MAX) NULL, 
	[FORMAT] NVARCHAR(32) NULL DEFAULT 'xml',
    [TENANT_CODE] NVARCHAR(36) NULL DEFAULT 'D5561180-7617-4B67-B68B-1F0EA604B509'
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'记录的ID',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'EXTENDED_PROPERTIES',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所属的对象的ID',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'EXTENDED_PROPERTIES',
    @level2type = N'COLUMN',
    @level2name = 'RESOURCE_ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据所属的类别',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'EXTENDED_PROPERTIES',
    @level2type = N'COLUMN',
    @level2name = N'TYPE'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据内容',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'EXTENDED_PROPERTIES',
    @level2type = N'COLUMN',
    @level2name = 'DATA'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户编码',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'EXTENDED_PROPERTIES',
    @level2type = N'COLUMN',
    @level2name = N'TENANT_CODE'
GO

CREATE INDEX [IX_EXTENDED_PROPERTIES_RESOURCE_ID] ON [WF].[EXTENDED_PROPERTIES] ([RESOURCE_ID])

GO

CREATE INDEX [IX_EXTENDED_PROPERTIES_TENANT_CODE] ON [WF].[EXTENDED_PROPERTIES] ([TENANT_CODE])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扩展数据表',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'EXTENDED_PROPERTIES',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据的格式',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'EXTENDED_PROPERTIES',
    @level2type = N'COLUMN',
    @level2name = N'FORMAT'