CREATE TABLE [DE].[RoleUserError](
	[ID] [nvarchar](36) NOT NULL,
	[AGR_NAME] [nvarchar](255) NOT NULL,
	[UNAME] [nvarchar](255) NOT NULL,
	[SapClient] [nvarchar](50) NOT NULL,
	[PlatID] [nvarchar](36) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_RoleUserError] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'RoleUserError',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'角色名称',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'RoleUserError',
    @level2type = N'COLUMN',
    @level2name = N'AGR_NAME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ERP用户名',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'RoleUserError',
    @level2type = N'COLUMN',
    @level2name = N'UNAME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'SapClient号',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'RoleUserError',
    @level2type = N'COLUMN',
    @level2name = N'SapClient'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'板块标识',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'RoleUserError',
    @level2type = N'COLUMN',
    @level2name = N'PlatID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'RoleUserError',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO