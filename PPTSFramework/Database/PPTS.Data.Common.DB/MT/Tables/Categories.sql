CREATE TABLE [MT].[Categories]
(
	[CategoryID] NVARCHAR(64) NOT NULL PRIMARY KEY, 
    [CategoryName] NVARCHAR(255) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'常量类别的ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'CategoryID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'常量类别的名称',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'CategoryName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'常量类别表',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = NULL,
    @level2name = NULL