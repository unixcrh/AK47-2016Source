CREATE FULLTEXT INDEX ON [SC].[SchemaUserSnapshot]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaUser_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO
GO

CREATE FULLTEXT INDEX ON [SC].[OperationLog]
	([SearchContent] LANGUAGE 2052, [Subject] LANGUAGE 2052)
	KEY INDEX [PK_OperationLog]
	ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO
GO

CREATE FULLTEXT INDEX ON [SC].[SchemaApplicationSnapshot]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaApplicationSnapshot_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaGroupSnapshot]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaGroupSnapshot_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaMembersSnapshot] ([SearchContent] LANGUAGE 2052) KEY INDEX [IX_SchemaMembersSnapshot_RowUniqueID] ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaOrganizationSnapshot]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaOrganizationSnapshot_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaObjectSnapshot]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaObjectSnapshot_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaPermissionSnapshot]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaPermissionSnapshot_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaRelationObjectsSnapshot]
	([SearchContent] LANGUAGE 2052)
	KEY INDEX [IX_SchemaRelationObjectsSnapshot_RowUniqueID]
	ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaRoleSnapshot]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaRoleSnapshot_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaMembersSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaMembersSnapshot_Current_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaGroupSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaGroupSnapshot_Current_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaApplicationSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaApplicationSnapshot_Current_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaObjectSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaObjectSnapshot_Current_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaUserSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaUserSnapshot_Current_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaRoleSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaRoleSnapshot_Current_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaRelationObjectsSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaRelationObjectsSnapshot_Current_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaPermissionSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaPermissionSnapshot_Current_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

CREATE FULLTEXT INDEX ON [SC].[SchemaOrganizationSnapshot_Current]
    ([SearchContent] LANGUAGE 2052)
    KEY INDEX [IX_SchemaOrganizationSnapshot_Current_RowID]
    ON [SCFullTextIndex] WITH CHANGE_TRACKING AUTO

GO

/*Xml Indices*/





