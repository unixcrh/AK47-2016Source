CREATE TABLE [DE].[EntityFieldSnapshot](
	[ID] [nvarchar](36) NOT NULL,
	[VersionStartTime] [datetime] NOT NULL,
	[VersionEndTime] [datetime] NULL DEFAULT ('99990909 00:00:00'),
	[Status] [int] NULL DEFAULT ((1)),
	[CreateDate] [datetime] NULL DEFAULT (getdate()),
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[FieldType] [nvarchar](255) NULL,
	[Length][int] NULL,
	[DefaultValue] [nvarchar](64) NULL,
	[SearchContent] [nvarchar](max) NULL,
	[SchemaType] [nvarchar](36) NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] NVARCHAR(255) NULL,
	[Comment] [nvarchar](255) NULL,
	[RowUniqueID] [nvarchar](36) NOT NULL DEFAULT (CONVERT([nvarchar](36),newid())),
 [CodeName] NVARCHAR(255) NULL, 
    [SortNo] INT NULL, 
    [ReferenceEntityCodeName] NVARCHAR(255) NULL, 
    CONSTRAINT [PK_EntityField] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[VersionStartTime] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


GO


CREATE UNIQUE NONCLUSTERED INDEX [IX_EntityFieldSnapshot_RowID] ON [DE].[EntityFieldSnapshot] ([RowUniqueID])

GO

CREATE FULLTEXT INDEX ON [DE].[EntityFieldSnapshot]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_EntityFieldSnapshot_RowID]
    ON [DEFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE INDEX [IX_EntityFieldSnapshot_StartTime] ON [DE].[EntityFieldSnapshot] ([VersionStartTime])

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'唯一标识',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'CreateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'名称',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'描述',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = 'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'字段类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'FieldType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'字段长度',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'Length'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'默认值',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'DefaultValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'搜索内容',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'SearchContent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'实体类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'SchemaType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = 'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'注释',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'Comment'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人名字',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = 'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'可读唯一标识',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'CodeName'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'排序号',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'EntityFieldSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'SortNo'