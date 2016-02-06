/* 将整数扩展为指定的位数，前面补0 */
CREATE FUNCTION [SC].[FormatInteger]
(
	@data BIGINT,
	@len INT
)
RETURNS NVARCHAR(64) AS
BEGIN
	DECLARE @result NVARCHAR(128)

	SET @result = CAST(@data AS NVARCHAR(128))

	DECLARE @diff AS INT

	SET @diff = @len - LEN(@data)

	IF (@diff > 0)
		SET @result = REPLICATE('0', @diff) + @result

	RETURN @result

END