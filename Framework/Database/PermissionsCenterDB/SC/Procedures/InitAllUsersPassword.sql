/*初始化所有用户的密码*/
CREATE PROCEDURE [SC].[InitAllUsersPassword]
AS
BEGIN
	TRUNCATE TABLE SC.UserPassword

	--插入用户的密码
	INSERT [SC].[UserPassword] ([UserID], [PasswordType], [AlgorithmType], [Password])
	SELECT SC.ID, 'MCS.Authentication', 'MCS.MD5', 'B0-81-DB-E8-5E-1E-C3-FF-C3-D4-E7-D0-22-74-00-CD'
	FROM SC.SchemaObject SC
	WHERE SchemaType = 'Users' AND VersionEndTime = CAST(N'9999-09-09 00:00:00.000' AS DateTime)
END
