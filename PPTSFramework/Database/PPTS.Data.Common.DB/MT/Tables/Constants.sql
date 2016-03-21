CREATE TABLE [MT].[Constants]
(
	[Category] NVARCHAR(64) NOT NULL, 
    [Key] NVARCHAR(32) NOT NULL,
	[ParentKey] NVARCHAR(32) NULL, 
    [Value] NVARCHAR(64) NULL, 
    [SortNo] INT NULL DEFAULT 0, 
    [IsValidate] INT NULL DEFAULT 1, 
    PRIMARY KEY ([Category], [Key])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'常量表',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Constants',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'类别',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Constants',
    @level2type = N'COLUMN',
    @level2name = N'Category'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'常量的Key',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Constants',
    @level2type = N'COLUMN',
    @level2name = N'Key'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'常量的值',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Constants',
    @level2type = N'COLUMN',
    @level2name = N'Value'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'内部序号',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Constants',
    @level2type = N'COLUMN',
    @level2name = N'SortNo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'父常量的Key',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Constants',
    @level2type = N'COLUMN',
    @level2name = N'ParentKey'
GO

CREATE INDEX [IX_Contants_ParentKey] ON [MT].[Constants] ([ParentKey])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否可用',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Constants',
    @level2type = N'COLUMN',
    @level2name = N'IsValidate'