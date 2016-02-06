CREATE VIEW [DE].[SchemaMembersSnapshot_Current]
WITH SCHEMABINDING 
	AS  
	 SELECT
	 [SearchContent],
	 [RowUniqueID],
		[ContainerID],
		[MemberID],
		[VersionStartTime],
		[VersionEndTime],
		[Status],
		[InnerSort],
		[Data],
		[SchemaType],
		[ContainerSchemaType],
		[MemberSchemaType],
		[CreateDate],
		[CreatorID],
		[CreatorName]
    FROM [DE].[SchemaMembersSnapshot]
	WHERE (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112)) AND 
	      ([Status] = 1)
		  GO


/****** Object:  Index [SchemaMembersSnapshot_Current_ClusteredIndex]    Script Date: 2013/5/17 16:16:13 ******/
CREATE UNIQUE CLUSTERED INDEX [SchemaMembersSnapshot_Current_ClusteredIndex] ON [DE].[SchemaMembersSnapshot_Current]
(
	[ContainerID] ASC,
	[MemberID] ASC,
	[VersionStartTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE UNIQUE INDEX [IX_SchemaMembersSnapshot_Current_RowID] ON [DE].[SchemaMembersSnapshot_Current] ([RowUniqueID])

GO

CREATE INDEX [IX_SchemaMembersSnapshot_Current_MemberID] ON [DE].[SchemaMembersSnapshot_Current] ([MemberID])

GO

CREATE FULLTEXT INDEX ON [DE].[SchemaMembersSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaMembersSnapshot_Current_RowID]
    ON [DEFullTextIndex] WITH CHANGE_TRACKING AUTO
