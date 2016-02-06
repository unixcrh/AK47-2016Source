
CREATE TABLE [DE].[SchemaMembersSnapshot](
	[ContainerID] [nvarchar](36) NOT NULL,
	[MemberID] [nvarchar](36) NOT NULL,
	[VersionStartTime] [datetime] NOT NULL,
	[VersionEndTime] [datetime] NULL DEFAULT ('99990909 00:00:00'),
	[Status] [int] NULL DEFAULT ((1)),
	[InnerSort] [int] NULL,
	[Data] [xml] NULL,
	[SchemaType] [nvarchar](64) NULL,
	[ContainerSchemaType] [nvarchar](64) NULL,
	[MemberSchemaType] [nvarchar](64) NULL,
	[CreateDate] [datetime] NULL DEFAULT (getdate()),
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](255) NULL,
	[RowUniqueID] [nvarchar](36) NOT NULL DEFAULT (CONVERT([nvarchar](36),newid())),
 [SearchContent] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_SchemaMembers] PRIMARY KEY CLUSTERED 
(
	[ContainerID] ASC,
	[MemberID] ASC,
	[VersionStartTime] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [DE].[SchemaMembers] ADD  CONSTRAINT [DF_SchemaMembers_VersionEndTime]  DEFAULT ('99990909 00:00:00') FOR [VersionEndTime]
GO

ALTER TABLE [DE].[SchemaMembers] ADD  CONSTRAINT [DF_SchemaMembers_Status]  DEFAULT ((1)) FOR [Status]
GO

ALTER TABLE [DE].[SchemaMembers] ADD  CONSTRAINT [DF_SchemaMembers_InnerSort]  DEFAULT ((0)) FOR [InnerSort]
GO

ALTER TABLE [DE].[SchemaMembers] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'容器的ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'ContainerID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成员的ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'MemberID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本开始时间' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'VersionStartTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本结束时间' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'VersionEndTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对象的状态' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内部排序号' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'InnerSort'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对象的属性集合' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'Data'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对象的Schema名称' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'SchemaType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'容器的Schema名称' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'ContainerSchemaType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成员的Schema名称' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'MemberSchemaType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人的ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人的名称' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成员关系对象表' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'SchemaMembers'
GO



EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'容器ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'ContainerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'成员ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'MemberID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'内容数据',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'Data'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'实体类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'SchemaType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'容器实体类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'ContainerSchemaType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'成员实体类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'MemberSchemaType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'CreateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建这姓名',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排序',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'InnerSort'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'搜索内容',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaMembersSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'SearchContent'