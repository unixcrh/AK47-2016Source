
CREATE TABLE [DE].[OperationLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ResourceID] [nvarchar](36) NULL,
	[CorrelationID] [nvarchar](36) NULL,
	[Category] [nvarchar](64) NULL,
	[OperatorID] [nvarchar](36) NULL,
	[OperatorName] [nvarchar](128) NULL,
	[RealOperatorID] [nvarchar](36) NULL,
	[RealOperatorName] [nvarchar](128) NULL,
	[RequestContext] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[SchemaType] [nvarchar](64) NULL,
	[OperationType] [nvarchar](64) NULL,
	[SearchContent] [nvarchar](max) NULL,
	[CreateTime] [datetime] NULL,
 [RowUniqueID] NVARCHAR(36) NOT NULL DEFAULT (CONVERT([nvarchar](36),newid())), 
    CONSTRAINT [PK_OperationLog] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


CREATE UNIQUE NONCLUSTERED INDEX [IX_OperationLog_RowID] ON [DE].[OperationLog] ([RowUniqueID])

GO

CREATE FULLTEXT INDEX ON [DE].[OperationLog]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_OperationLog_RowID]
    ON [DEFullTextIndex] WITH CHANGE_TRACKING AUTO

GO


ALTER TABLE [DE].[OperationLog] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'相关对象的ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'ResourceID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'环境上下文ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'CorrelationID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'Category'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'OperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人名称' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'OperatorName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际操作人ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'RealOperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际操作人名称' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'RealOperatorName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求的上下文' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'RequestContext'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主题' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'Subject'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作对象的类型' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'SchemaType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'OperationType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供全文检索的字段' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'SearchContent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志创建时间' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作日志' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'OperationLog'
GO


