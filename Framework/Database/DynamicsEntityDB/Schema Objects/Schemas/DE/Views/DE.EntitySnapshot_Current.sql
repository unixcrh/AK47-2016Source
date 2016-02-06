CREATE VIEW [DE].[EntitySnapshot_Current]
WITH SCHEMABINDING 
	AS 
   SELECT
   [ID],[RowUniqueID],
		[VersionStartTime],
		[VersionEndTime],
		[Status],
		[CreateDate],
		[Name],
		[Description],
		[CodeName],
		[SearchContent],
		[SchemaType],
		[CreatorID],
		[CreatorName],
		[Comment],
		[CategoryID]
    FROM [DE].[EntitySnapshot]
	WHERE (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112)) AND 
	      ([Status] = 1)
		  GO

		  		  /****** Object:  Index [SchemaObjectSnapshot_Current_ClusteredIndex]    Script Date: 2013/5/17 16:16:13 ******/
CREATE UNIQUE CLUSTERED INDEX [EntitySnapshot_Current_ClusteredIndex] ON [DE].[EntitySnapshot_Current]
(
	[ID] ASC,
	[VersionStartTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE UNIQUE INDEX [IX_EntitySnapshot_Current_RowID] ON [DE].[EntitySnapshot_Current] ([RowUniqueID])

GO

CREATE INDEX [IX_EntitySnapshot_Current_CodeName] ON [DE].[EntitySnapshot_Current] ([CodeName])

GO

CREATE FULLTEXT INDEX ON [DE].[EntitySnapshot_Current]   ([SearchContent] LANGUAGE 2052)
 
    KEY INDEX [IX_EntitySnapshot_Current_RowID]
    ON [DEFullTextIndex] WITH CHANGE_TRACKING AUTO
go





