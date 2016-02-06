CREATE TABLE [DE].[ETL_JobAndAutoMapping]
(
	[ID] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
    [JobID] NVARCHAR(36) NOT NULL , 
    [IsAuto] BIT NOT NULL, 
    [IsIncrement] BIT NOT NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_JobAndAutoMapping',
    @level2type = N'COLUMN',
    @level2name = 'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Job的ID',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_JobAndAutoMapping',
    @level2type = N'COLUMN',
    @level2name = N'JobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否自动',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_JobAndAutoMapping',
    @level2type = N'COLUMN',
    @level2name = N'IsAuto'