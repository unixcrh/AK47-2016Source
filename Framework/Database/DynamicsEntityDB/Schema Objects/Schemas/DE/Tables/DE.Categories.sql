CREATE TABLE [DE].[Categories]
(
	[Code] NVARCHAR(36) NOT NULL , 
    [DisplayName] NVARCHAR(100) NOT NULL, 
    [ParentCode] NVARCHAR(36) NULL, 
    [Description] NVARCHAR(255) NULL, 
    [Status] INT NOT NULL DEFAULT ((1)), 
    [VersionStartTime] DATETIME NOT NULL, 
    [VersionEndTime] DATETIME NULL , 
    [SortNo] INT NULL, 
    [Creator] NVARCHAR(36) NOT NULL, 
    [CreateTime] DATETIME NOT NULL DEFAULT (getdate()), 
    [Modifier] NVARCHAR(36) NULL, 
    [ModifyTime] DATETIME NULL, 
    [Level] INT NULL, 
    [FullPath] NVARCHAR(255) NULL, 
    [ValidStatus] BIT NULL, 
    PRIMARY KEY ([Code], [VersionStartTime])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'唯一标识',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'显示名称',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'DisplayName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'父类别标识',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'ParentCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'描述',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = 'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排序号',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'SortNo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'Creator'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改者',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'Modifier'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'级别',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'Level'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'全路径',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'FullPath'

	GO

CREATE INDEX [IX_Categories_Code] ON [DE].[Categories] ([Code])