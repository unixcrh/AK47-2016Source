CREATE TABLE [CM].[CustomerRelations]
(
	[CustomerID] NVARCHAR(36) NOT NULL , 
    [ParentID] NVARCHAR(36) NOT NULL, 
    [CustomerRole] NVARCHAR(32) NULL, 
    [ParentRole] NVARCHAR(32) NULL, 
    [IsPrimary] INT NULL DEFAULT 0,
	[CreatorID] NVARCHAR(36) NULL,
	[CreatorName] NVARCHAR(64) NULL,
	[CreateTime] DATETIME NULL DEFAULT GETDATE(),
    [TenantCode] NVARCHAR(36) NULL, 
    PRIMARY KEY ([CustomerID], [ParentID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = N'COLUMN',
    @level2name = N'ParentID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生对家长的亲属关系(C_CODE_ABBR_CHILDMALEDICTIONARY,C_CODE_ABBR_CHILDFEMALEDICTIONARY)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = N'COLUMN',
    @level2name = N'CustomerRole'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长对学生的亲属关系(C_CODE_ABBR_PARENTMALEDICTIONARY,C_CODE_ABBR_PARENTFEMALEDICTIONARY)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = N'COLUMN',
    @level2name = N'ParentRole'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否是主要监护人',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = N'COLUMN',
    @level2name = N'IsPrimary'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长和学员关系表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_CustomerRelations_ParentID] ON [CM].[CustomerRelations] ([ParentID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'