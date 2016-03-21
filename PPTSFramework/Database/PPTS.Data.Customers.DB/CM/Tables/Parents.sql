CREATE TABLE [CM].[Parents]
(
	[ParentID] NVARCHAR(36) NOT NULL PRIMARY KEY,
	[ParentName] NVARCHAR(128) NULL,
	[CustomerCode] NVARCHAR(64) NULL,
	[Gender] NVARCHAR(32) NULL,
    [Industry] NVARCHAR(32) NULL, 
    [Career] NVARCHAR(32) NULL, 
    [Income] NVARCHAR(32) NULL, 
    [Birthday] DATETIME NULL, 
	[IDType] NVARCHAR(32) NULL,
	[IDNumbar] NVARCHAR(64) NULL,
	[Country] NVARCHAR(32) NULL DEFAULT '1004115',
	[Province] NVARCHAR(32) NULL,
	[City] NVARCHAR(32) NULL,
	[County] NVARCHAR(32) NULL,
	[AddressDetail] NVARCHAR(MAX) NULL,
	[CreatorID] NVARCHAR(36) NULL,
	[CreatorName] NVARCHAR(64) NULL,
	[CreateTime] DATETIME NULL DEFAULT GETDATE(),
    [TenantCode] NVARCHAR(36) NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'ParentID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所属行业(C_CODE_ABBR_INDUSTRY)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'Industry'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'从事职业',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'Career'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家庭年收入(C_CODE_ABBR_HOMEINCOME)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'Income'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长生日',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'Birthday'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长关系表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'ParentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'证件类型(C_CODE_ABBR_BO_Customer_CertificateType)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'IDType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'证件号码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'IDNumbar'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'性别(C_CODE_ABBR_GENDER)。1--男，2--女',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'Gender'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'国家(C_CODE_ABBR_COUNTRY)。默认是中国1004115',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'Country'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'省份(C_CODE_ABBR_LOCATION)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'Province'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'城市(C_CODE_ABBR_LOCATION)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'City'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区县(C_CODE_ABBR_LOCATION)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'County'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'详细地址描述',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'AddressDetail'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户ID命名规则：家长P+年份后两位+月+日+999999，学生S+年份后两位+月份+日期+999999',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Parents',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'