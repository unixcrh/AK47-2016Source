
CREATE TABLE [DE].[Acl](
	[ContainerID] [nvarchar](36) NOT NULL,
	[ContainerPermission] [nvarchar](64) NOT NULL,
	[MemberID] [nvarchar](36) NOT NULL,
	[VersionStartTime] [datetime] NOT NULL,
	[VersionEndTime] [datetime] NULL,
	[Status] [int] NULL,
	[SortID] [int] NULL,
	[Data] [xml] NULL,
	[ContainerSchemaType] [nvarchar](64) NULL,
	[MemberSchemaType] [nvarchar](64) NULL,
 CONSTRAINT [PK_Acl] PRIMARY KEY CLUSTERED 
(
	[ContainerID] ASC,
	[ContainerPermission] ASC,
	[MemberID] ASC,
	[VersionStartTime] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [DE].[Acl] ADD  CONSTRAINT [DF_Acl_VersionEndTime]  DEFAULT ('99990909 00:00:00') FOR [VersionEndTime]
GO

ALTER TABLE [DE].[Acl] ADD  CONSTRAINT [DF_Acl_Status]  DEFAULT ((1)) FOR [Status]
GO

ALTER TABLE [DE].[Acl] ADD  CONSTRAINT [DF_Acl_InnerSort]  DEFAULT ((0)) FOR [SortID]
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排序ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'SortID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'Data'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'容器ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'ContainerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'容器权限',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'ContainerPermission'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'成员ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'MemberID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'容器类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'ContainerSchemaType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'成员类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'Acl',
    @level2type = N'COLUMN',
    @level2name = N'MemberSchemaType'
GO

CREATE INDEX [IX_Acl_MemberID] ON [DE].[Acl] ([MemberID])