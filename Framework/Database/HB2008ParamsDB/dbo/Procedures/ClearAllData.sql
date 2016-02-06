﻿CREATE PROCEDURE [dbo].[ClearAllData]
AS
BEGIN
	SET NOCOUNT ON;

	TRUNCATE TABLE dbo.PAGE_VIEW_STATE
	TRUNCATE TABLE dbo.PASSPORT_SIGNIN_INFO
	TRUNCATE TABLE dbo.PASSPORT_TICKET
	TRUNCATE TABLE dbo.SWITCHES
	TRUNCATE TABLE dbo.USER_CUSTOM_SEARCH_CONDITION
	TRUNCATE TABLE dbo.USER_RECENT_DATA
	TRUNCATE TABLE dbo.USER_SETTINGS
	TRUNCATE TABLE dbo.USERS_INFO_EXTEND
	TRUNCATE TABLE dbo.USERS_REPORT_LINE

	TRUNCATE TABLE WF.[GROUP]
	TRUNCATE TABLE WF.GROUP_USERS
	TRUNCATE TABLE WF.POST
	TRUNCATE TABLE WF.POST_USERS
END