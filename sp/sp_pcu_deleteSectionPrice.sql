USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_deleteSectionPrice]    Script Date: 2015/6/11 9:27:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<fanwei>
-- Create date: <20150514>
-- Description:	<删除价格区间表>
-- =============================================
/*
	DECLARE  @pageID UNIQUEIDENTIFIER = '33f9e70c-99e5-4719-ac43-24b189cc4e76'
	, @uID INT = 13 
	, @retValue bit   
	, @errMsg NVARCHAR(400) 
	, @pcud_section_start DECIMAL(18,5) = N'2000.10000'
	, @pcud_section_end DECIMAL(18,5) = N'4000.00000'
	, @pcups_vend NVARCHAR(10)  = N'S1000015'
	, @pcups_part NVARCHAR(20)  = N'30030050000480'
	, @pcud_type NVARCHAR(200)  = N'铜价2k'
	
	exec sp_pcu_deleteSectionPrice @pageID,@uID,@retValue out,@errMsg out,@pcud_section_start,@pcud_section_end,@pcups_vend,@pcups_part,@pcud_type

	select @retValue,@errMsg
*/
ALTER PROCEDURE [dbo].[sp_pcu_deleteSectionPrice] 
	 @pageID UNIQUEIDENTIFIER
	, @uID INT
	, @retValue BIT OUT
	, @errMsg NVARCHAR(400) OUTPUT
	, @pcud_section_start DECIMAL(18,5)
	, @pcud_section_end DECIMAL(18,5)
	, @pcups_vend NVARCHAR(10) 
	, @pcups_part NVARCHAR(20) 
	, @pcud_type NVARCHAR(200) 
	
	
AS
BEGIN


	SET @retValue = 1
	SET @errMsg = N''

	BEGIN TRAN
		INSERT INTO dbo.pcu_price_section_hist
	        ( pcups_type ,
	          pcups_vend ,
	          pcups_part ,
	          pcups_price ,
	          pcud_id ,
	          createby ,
	          createname ,
	          createdate ,
	          modifyby ,
	          modifyname ,
	          modifydate ,
	          deleteBy ,
	          deleteDate ,
			  pcups_isHang ,
			  pcups_hangBy ,
			  pcups_hangDate
	        )
	SELECT pps.pcups_type,pps.pcups_vend,pps.pcups_part,pps.pcups_price,pps.pcud_id
		,pps.createby,pps.createname,pps.createdate,pps.modifyby,pps.modifyname,pps.modifydate
		,@uID,GETDATE(),pps.pcups_isHang,pps.pcups_hangBy,pps.pcups_hangDate
	from dbo.pcu_price_section pps LEFT JOIN dbo.pcu_part_vend_type ppvt ON pps.pcups_type = ppvt.pcutv_type
	AND pps.pcups_part = ppvt.pcutv_part
	AND pps.pcups_vend = ppvt.pcutv_vend
	LEFT JOIN dbo.pcu_section ps ON ps.pcud_type = pps.pcups_type
	AND ps.pcud_id = pps.pcud_id
	WHERE pps.pcups_type = @pcud_type
	AND pps.pcups_part = @pcups_part
	AND pps.pcups_vend = @pcups_vend
	AND ps.pcud_section_start = @pcud_section_start
	AND ps.pcud_section_end = @pcud_section_end
	IF(@@rowcount = 1 OR @@ERROR <> 0)
	BEGIN
		DELETE FROM  dbo.pcu_price_section
		WHERE pcups_type = @pcud_type
		AND pcups_part = @pcups_part
		AND pcups_vend = @pcups_vend
		AND pcud_id = (SELECT pcud_id
						FROM dbo.pcu_section
						WHERE pcud_type = @pcud_type
						AND pcud_section_start =  @pcud_section_start
						AND pcud_section_end = @pcud_section_end)
		IF(@@error <> 0 )
		BEGIN
			ROLLBACK TRAN
			SET @retValue = 0
			SET @errMsg = N'数据库出错，请联系管理员！'
		END
		ELSE
		BEGIN
			COMMIT TRAN
			SET @retValue = 1
		END
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		SET @retValue = 0
		SET @errMsg = N'历史表插入错误，请联系管理员'
	END
	
END


