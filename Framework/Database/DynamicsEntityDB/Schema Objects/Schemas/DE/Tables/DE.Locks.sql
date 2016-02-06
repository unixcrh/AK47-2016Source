CREATE TABLE [DE].[Locks](
	[LockID] [nvarchar](36) NOT NULL,
	[ResourceID] [nvarchar](36) NULL,
	[LockPersonID] [nvarchar](36) NULL,
	[LockPersonName] [nvarchar](255) NULL,
	[LockTime] [datetime] NULL,
	[EffectiveTime] [int] NULL,
	[LockType] [int] NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[LockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [DE].[Locks] ADD  DEFAULT (getdate()) FOR [LockTime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'锁ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'Locks', @level2type=N'COLUMN',@level2name=N'LockID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'相关的资源ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'Locks', @level2type=N'COLUMN',@level2name=N'ResourceID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上锁人的ID' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'Locks', @level2type=N'COLUMN',@level2name=N'LockPersonID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上锁人的名称' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'Locks', @level2type=N'COLUMN',@level2name=N'LockPersonName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上锁的时间' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'Locks', @level2type=N'COLUMN',@level2name=N'LockTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'锁的有效期，以秒为单位' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'Locks', @level2type=N'COLUMN',@level2name=N'EffectiveTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'锁的类型' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'Locks', @level2type=N'COLUMN',@level2name=N'LockType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述信息' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'Locks', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作锁表' , @level0type=N'SCHEMA',@level0name=N'DE', @level1type=N'TABLE',@level1name=N'Locks'
GO


