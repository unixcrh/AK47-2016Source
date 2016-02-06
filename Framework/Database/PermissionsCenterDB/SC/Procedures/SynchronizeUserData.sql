-- =============================================
-- Author:		贺成德
-- Create date: 2015-07-21
-- Description:	处理人员信息从主数据到权限中心
-- =============================================
CREATE PROCEDURE [dbo].[P_SynchronizeUserData]
		@BatchTime  Datetime
AS
BEGIN
	SET NOCOUNT ON;	

	BEGIN TRANSACTION

	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; 

	SET XACT_ABORT ON;
	--从主数据表中得到每个对象的最后一条记录，构造权限中心的记录的字段，插入到临时表中
	DECLARE @Users TABLE([ID] NVARCHAR(36), [Data] xml, [Status] INT);
	INSERT INTO @Users([ID], [Data], [Status])
	SELECT 
		A.EMPLID AS [ID],
		dbo.GetUserXml(MDP_GUID) AS [Data],
		CASE WHEN A.MDP_OperationType='DELETE' THEN 3 ELSE 1 END AS [Status]
	FROM MDM.PERS_ALL A INNER JOIN
		(SELECT EMPLID, MAX(MDP_BatchTime) AS MAX_BatchTime
		FROM MDM.PERS_ALL
		GROUP BY EMPLID) LASTEST
		ON A.EMPLID = LASTEST.EMPLID  AND A.MDP_BatchTime = LASTEST.MAX_BatchTime
	WHERE MDP_Result IS NULL;

	IF @@ERROR <> 0 GOTO TransActionFail

	--与现有的记录进行比较，找出需要新增的记录，放在临时表@InsertedSchemaObjects中
	DECLARE @InsertedSchemaObjects TABLE([ID] NVARCHAR(36), [Data] xml, [Status] INT, SchemaType NVARCHAR(64))
	INSERT INTO @InsertedSchemaObjects([ID], Data, [Status], SchemaType)
	SELECT [ID], Data, [Status], 'Users'
	FROM @Users U
	WHERE NOT EXISTS(SELECT [ID] FROM SC.SchemaObject WHERE [ID] = U.ID)

	IF @@ERROR <> 0 GOTO TransActionFail

	--与现有的记录进行比较，找出需要修改的记录，放在临时表@UpdatedSchemaObjects中
	DECLARE @UpdatedSchemaObjects TABLE([ID] NVARCHAR(36), [Data] xml, [Status] INT, SchemaType NVARCHAR(64))
	INSERT INTO @UpdatedSchemaObjects([ID], Data, [Status], SchemaType)
	SELECT [ID], Data, [Status], 'Users'
	FROM @Users U
	WHERE EXISTS(SELECT [ID] FROM SC.SchemaObject
		WHERE [ID] = U.ID AND (CAST([Data] AS NVARCHAR(MAX)) <> CAST(U.Data AS NVARCHAR(MAX)) OR [Status] <> U.Status))
	
	IF @@ERROR <> 0 GOTO TransActionFail

	--下面是更新对象的操作
	--插入需要新增的对象
	INSERT INTO SC.SchemaObject([ID], Data, VersionStartTime, VersionEndTime, [Status], SchemaType)
	SELECT [ID], Data, @BatchTime, '9999-09-09 00:00:00.000', [Status], 'Users'
	FROM @InsertedSchemaObjects ;

	IF @@ERROR <> 0 GOTO TransActionFail

	----将当前已存在的对象的结束时间封口
	UPDATE SC.SchemaObject
	SET VersionEndTime = @BatchTime
	FROM @UpdatedSchemaObjects U INNER JOIN SC.SchemaObject SC ON SC.ID = U.ID
	WHERE SC.VersionEndTime = '9999-09-09 00:00:00.000'
		
	IF @@ERROR <> 0 GOTO TransActionFail
	--查询新变更的对象信息
	INSERT INTO SC.SchemaObject([ID], Data, VersionStartTime, VersionEndTime, [Status], SchemaType)
	SELECT [ID], Data, @BatchTime, '9999-09-09 00:00:00.000', [Status], 'Users'
	FROM @UpdatedSchemaObjects;

	IF @@ERROR <> 0 GOTO TransActionFail

	UPDATE A SET A.MDP_Result='DONE' FROM MDM.PERS_ALL A   WHERE MDP_Result IS NULL
	IF @@ERROR <> 0 GOTO TransActionFail

	COMMIT TRANSACTION;

	GOTO QuickOut

	TransActionFail:
		ROLLBACK TRANSACTION

	QuickOut:
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED;	

END