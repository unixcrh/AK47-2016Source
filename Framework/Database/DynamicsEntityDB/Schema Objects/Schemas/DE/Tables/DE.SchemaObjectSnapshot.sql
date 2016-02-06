
CREATE TABLE [DE].[SchemaObjectSnapshot](
	[ID] [nvarchar](36) NOT NULL,
	[VersionStartTime] [datetime] NOT NULL,
	[VersionEndTime] [datetime] NULL,
	[Status] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Name] [nvarchar](255) NULL,
	[DisplayName] [nvarchar](255) NULL,
	[CodeName] [nvarchar](64) NULL,
	[SearchContent] [nvarchar](max) NULL,
	[RowUniqueID] [nvarchar](36) NOT NULL,
	[SchemaType] [nvarchar](36) NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](255) NULL,
	[Comment] [nvarchar](255) NULL,
    CONSTRAINT [PK_SchemaObjectSnapshot] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[VersionStartTime] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [DE].[SchemaObjectSnapshot] ADD  CONSTRAINT [DF_SchemaObjectSnapshot_VersionEndTime]  DEFAULT ('99990909 00:00:00') FOR [VersionEndTime]
GO

ALTER TABLE [DE].[SchemaObjectSnapshot] ADD  CONSTRAINT [DF_SchemaObjectSnapshot_Status]  DEFAULT ((1)) FOR [Status]
GO

ALTER TABLE [DE].[SchemaObjectSnapshot] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO


ALTER TABLE [DE].[SchemaObjectSnapshot] ADD  CONSTRAINT [DF_SchemaObjectSnapshot_RowUniqueID]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [RowUniqueID]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_SchemaObjectSnapshot_RowID] ON [DE].[SchemaObjectSnapshot] ([RowUniqueID])

GO

CREATE FULLTEXT INDEX ON [DE].[SchemaObjectSnapshot]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaObjectSnapshot_RowID]
    ON [DEFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE INDEX [IX_SchemaObjectSnapshot_StartTime] ON [DE].[SchemaObjectSnapshot] ([VersionStartTime])

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'唯一标识',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'CreateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'名称',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'显示名称',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'DisplayName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'搜索内容',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'SearchContent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'实体类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'SchemaType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者姓名',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'注释',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObjectSnapshot',
    @level2type = N'COLUMN',
    @level2name = N'Comment'
GO
