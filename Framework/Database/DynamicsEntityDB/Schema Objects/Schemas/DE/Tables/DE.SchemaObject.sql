CREATE TABLE [DE].[SchemaObject](
	[ID] [nvarchar](36) NOT NULL ,
	[VersionStartTime] [datetime] NOT NULL,
	[VersionEndTime] [datetime] NULL,
	[Status] [int] NULL,
	[Data] [xml] NULL,
	[SchemaType] [nvarchar](64) NULL,
	[CreateDate] [datetime] NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](255) NULL,
 CONSTRAINT [PK_SchemaObject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[VersionStartTime] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [DE].[SchemaObject] ADD  CONSTRAINT [DF_SchemaObject_VersionEndTime]  DEFAULT ('99990909 00:00:00') FOR [VersionEndTime]
GO

ALTER TABLE [DE].[SchemaObject] ADD  CONSTRAINT [DF_SchemaObject_Status]  DEFAULT ((1)) FOR [Status]
GO

ALTER TABLE [DE].[SchemaObject] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'唯一标识',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObject',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObject',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObject',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObject',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'数据',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObject',
    @level2type = N'COLUMN',
    @level2name = N'Data'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'实体类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObject',
    @level2type = N'COLUMN',
    @level2name = N'SchemaType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObject',
    @level2type = N'COLUMN',
    @level2name = N'CreateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObject',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者姓名',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'SchemaObject',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'