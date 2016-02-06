-- =============================================
-- Author:		贺成德
-- Create date: 2015-07-21
-- Description:	主数据同步回调存储过程
CREATE PROCEDURE [dbo].[P_MasterDataCallbackPro]
		@P1 nvarchar(500)--ETL批次号
	,@P2 nvarchar(500)--视图组名称
AS
BEGIN
	--统一当前的时间
	DECLARE @BatchTime DATETIME
	SET @BatchTime = GETDATE()
	
	PRINT '处理人员信息';
	EXEC  [dbo].[P_SynchronizeUserData]	@BatchTime;
	PRINT @@ERROR

	PRINT '处理组织机构信息';
	EXEC  [dbo].P_SynchronizeOrganizationsData	@BatchTime
	PRINT @@ERROR

	PRINT '处理组织机构信息';
	EXEC  [dbo].[P_SynchronizeOrgTreeData]	@BatchTime
	PRINT @@ERROR

	PRINT '处理组织机构信息';
	EXEC  [dbo].[P_SynchronizeOrgUserData]	@BatchTime
	PRINT @@ERROR


	--刷新相关的快照
	--数据更新完后，需要刷新权限中心的有关快照数据。
	DELETE SC.SchemaUserSnapshot
	DELETE SC.SchemaOrganizationSnapshot
	DELETE SC.SchemaRelationObjectsSnapshot
 
	EXECUTE SC.GenerateSchemaSnapshot 'Users'
	EXECUTE SC.GenerateSchemaSnapshot 'Organizations'
	EXECUTE SC.GenerateSchemaSnapshot 'RelationObjects'
 
	--生成用户Password表
	--在快照生成后，直接生成用户的密码表即可。我们的密码都是Password。
	TRUNCATE TABLE [SC].[UserPassword]
 
	INSERT INTO [SC].[UserPassword](UserID, PasswordType, AlgorithmType, [Password])
	SELECT ID, 'MCS.Authentication', 'MCS.MD5', 'B0-81-DB-E8-5E-1E-C3-FF-C3-D4-E7-D0-22-74-00-CD'
	FROM [SC].SchemaUserSnapshot_Current
 
 
	--生成组织和人员关系的附加字段
	--这里仅仅需要调用一个存储过程即可：
	--EXECUTE SC. GenerateFullPaths

	
END