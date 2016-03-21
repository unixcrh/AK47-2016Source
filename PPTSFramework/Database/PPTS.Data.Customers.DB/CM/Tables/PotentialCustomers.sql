CREATE TABLE [CM].[PotentialCustomers]
(
	[CustomerID] NVARCHAR(36) NOT NULL PRIMARY KEY,
	[CustomerName] NVARCHAR(128) NULL,
	[CustomerCode] NVARCHAR(64) NULL,
    [IsSingleParent] INT NULL DEFAULT 0, 
    [EntranceGrade] NVARCHAR(32) NULL, 
    [Branch] NVARCHAR(32) NULL, 
    [SchoolYear] NVARCHAR(32) NULL, 
    [Character] NVARCHAR(MAX) NULL, 
    [Birthday] DATETIME NULL, 
	[Email] nvarchar(255) NULL,
    [Grade] NVARCHAR(32) NULL, 
    [StudentType] NVARCHAR(32) NULL DEFAULT 51, 
    [TeachingLocation] NVARCHAR(36) NULL, 
    [ConsultingSite] NVARCHAR(36) NULL, 
    [SalesStage] NVARCHAR(32) NULL, 
    [PurchaseIntention] NVARCHAR(32) NULL DEFAULT 0, 
	[IDType] NVARCHAR(32) NULL,
	[IDNumbar] NVARCHAR(64) NULL,
	[ContactType] NVARCHAR(32) NULL,
	[SourceMainType] NVARCHAR(32) NULL,
	[SourceSubType] NVARCHAR(32) NULL,
	[Status] NVARCHAR(32) NULL DEFAULT 0,
	[CreatorID] NVARCHAR(36) NULL,
	[CreatorName] NVARCHAR(64) NULL,
	[CreateTime] DATETIME NULL DEFAULT GETDATE(),
	[SchoolID] NVARCHAR(32) NULL,
	[Gender] NVARCHAR(32) NULL,
	[VipType] NVARCHAR(32) NULL,
	[VipLevel] NVARCHAR(32) NULL,
	[LastFollowupTime] DATETIME NULL,
	[NextFollowupTime] DATETIME NULL,
    [TenantCode] NVARCHAR(36) NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否单亲',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'IsSingleParent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'入学大年级。对应类别C_CODE_ABBR_CUSTOMER_GRADE（年级）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'EntranceGrade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'文理科(C_CODE_ABBR_STUDENTBRANCH)。1:文科，2:理科，3:不分科',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Branch'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学年制(C_CODE_ABBR_ACDEMICYEAR)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'SchoolYear'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生描述',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Character'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生生日',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Birthday'
GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前年级。年级(C_CODE_ABBR_CUSTOMER_GRADE)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Grade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学生类型(C_CODE_ABBR_CUSTOMER_STUDENTTYPE)，默认51',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'StudentType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教学地点的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'TeachingLocation'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询地点的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'ConsultingSite'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'销售阶段(C_CODE_ABBR_Customer_CRM_SalePhase)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'SalesStage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'购买意愿(C_CODE_ABBR_Customer_CRM_PurchaseIntent)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'PurchaseIntention'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'潜在客户表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户ID命名规则：家长P+年份后两位+月+日+999999，学生S+年份后两位+月份+日期+999999',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户状态(C_Code_Abbr_BO_CTI_CustomerStatus)0=未确认客户信息, 1 = 确认客户信息, 9=无效用户（逻辑删除）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'性别(C_CODE_ABBR_GENDER)。1--男，2--女',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Gender'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = 'SchoolID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'vip客户(C_CODE_ABBR_CUSTOMER_VipType)。1:关系vip客户 2:大单vip客户',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'VipType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'vip客户等级（C_CODE_ABBR_CUSTOMER_VipLevel）。1:A级 2:B级 3:C级',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'VipLevel'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后一次跟进时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'LastFollowupTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预计下次回访时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'NextFollowupTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'邮件地址',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'Email'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'接触方式(C_CODE_ABBR_Customer_CRM_NewContactType)。"1：呼入 2：呼出 3：直访 4：在线咨询-乐语 5：在线咨询-其他"',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'ContactType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'证件类型(C_CODE_ABBR_BO_Customer_CertificateType)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'IDType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'证件号码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'IDNumbar'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'信息来源一级分类(C_Code_Abbr_BO_Customer_Source)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'SourceMainType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'信息来源二级分类(C_Code_Abbr_BO_Customer_Source)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'PotentialCustomers',
    @level2type = N'COLUMN',
    @level2name = N'SourceSubType'