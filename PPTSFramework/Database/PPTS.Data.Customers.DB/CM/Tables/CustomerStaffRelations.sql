CREATE TABLE [CM].[CustomerStaffRelations]
(
	[CustomerID] NVARCHAR(36) NOT NULL , 
    [StaffID] NVARCHAR(36) NOT NULL, 
    [StaffName] NVARCHAR(64) NULL, 
    [JobID] NVARCHAR(36) NULL, 
    [RelationType] NVARCHAR(32) NULL DEFAULT 1,
	[CreatorID] NVARCHAR(36) NULL,
	[CreatorName] NVARCHAR(64) NULL,
	[CreateTime] DATETIME NULL DEFAULT GETDATE(),
	[TenantCode] NVARCHAR(36) NULL
    PRIMARY KEY ([CustomerID], [StaffID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户和员工的关系表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_CustomerStaffRelations_StaffID] ON [CM].[CustomerStaffRelations] ([StaffID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'JobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1 销售关系: 学生，销售（咨询师）[原来写的是家长，销售关系，但是看到逻辑是学生和销售关系];2 教管关系：学生，学管（班主任）;3 教学关系: 学生，老师;4 电销关系',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'RelationType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'