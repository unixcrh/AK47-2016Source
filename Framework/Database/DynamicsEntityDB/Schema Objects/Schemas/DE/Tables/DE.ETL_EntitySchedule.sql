CREATE TABLE [DE].[ETL_EntitySchedule]
(
	[Code] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
    [ScheduleType] NVARCHAR(36) NULL, 
    [StartTime] DATETIME NULL, 
    [EndTime] DATETIME NULL, 
    [NextExecutionTime] DATETIME NULL, 
    [EndExecutionTime] DATETIME NULL, 
    [ExecutionRate ] INT NULL, 
    [Creator] NVARCHAR(36) NULL, 
    [ScheduleName] NVARCHAR(36) NULL, 
    [ScheduleDescription] NVARCHAR(50) NULL, 
    [IsDelete] BIT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划主键',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'Code'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划类型',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'ScheduleType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'StartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'EndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划下次执行时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'NextExecutionTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后执行时间',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'EndExecutionTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划执行频率',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'ExecutionRate '
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划创建者',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'Creator'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划名称',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'ScheduleName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'计划描述',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'ScheduleDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否删除',
    @level0type = N'SCHEMA',
    @level0name = N'DE',
    @level1type = N'TABLE',
    @level1name = N'ETL_EntitySchedule',
    @level2type = N'COLUMN',
    @level2name = N'IsDelete'