CREATE TABLE [DE].[ScheduleExecutionCycle]
(
	[Code] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
    [Name] NVARCHAR(50) NULL
)
