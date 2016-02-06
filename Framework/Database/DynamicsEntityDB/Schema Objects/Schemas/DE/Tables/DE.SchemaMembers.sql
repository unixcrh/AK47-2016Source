
CREATE TABLE [DE].[SchemaMembers](
	[ContainerID] [nvarchar](36) NOT NULL,
	[MemberID] [nvarchar](36) NOT NULL,
	[VersionStartTime] [datetime] NOT NULL,
	[VersionEndTime] [datetime] NULL,
	[Status] [int] NULL,
	[InnerSort] [int] NULL,
	[Data] [xml] NULL,
	[SchemaType] [nvarchar](64) NULL,
	[ContainerSchemaType] [nvarchar](64) NULL,
	[MemberSchemaType] [nvarchar](64) NULL,
	[CreateDate] [datetime] NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](255) NULL,
	[RowUniqueID] [nvarchar](36) NOT NULL DEFAULT (CONVERT([nvarchar](36),newid())),
 CONSTRAINT [PK_SchemaObjectMembers] PRIMARY KEY CLUSTERED 
(
	[ContainerID] ASC,
	[MemberID] ASC,
	[VersionStartTime] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



