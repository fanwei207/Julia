USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_deletePartVendType]    Script Date: 2015/6/8 15:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<fanwei>
-- Create date: <20150515>
-- Description:	<>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcu_deletePartVendType]
	@pageID UNIQUEIDENTIFIER
	, @uID INT
	, @retValue BIT OUT
	, @errMsg NVARCHAR(400) OUTPUT
	, @pcutv_type NVARCHAR(200) 
	, @pcutv_part NVARCHAR(20) 
	, @pcutv_vend NVARCHAR(10) 


AS
BEGIN

	SET @retValue = 1
	SET @errMsg = N''

	DECLARE @error INT 
	SET @error = 0
	
	IF(NOT EXISTS(SELECT *
					FROM dbo.pcu_part_vend_type ppvt LEFT JOIN dbo.pcu_price_section pps ON ppvt.pcutv_type = pps.pcups_type
					AND ppvt.pcutv_part = pps.pcups_part 
					AND ppvt.pcutv_vend = pps.pcups_vend
					WHERE pps.pcud_id IS NOT NULL
					AND ppvt.pcutv_type = @pcutv_type
					AND pps.pcups_part  = @pcutv_part
					AND pps.pcups_vend = @pcutv_vend))
	BEGIN
		BEGIN TRAN
		
		INSERT INTO dbo.pcu_part_vend_type_hist
		        ( pcutv_type ,
		          pcutv_part ,
		          pcutv_vend ,
		          pcutv_diff_price ,
		          createby ,
		          createname ,
		          createdate ,
		          modifyby ,
		          modifyname ,
		          modifydate ,
		          deleteBy ,
		          deleteDate
		        )
		SELECT  pcutv_type ,
		          pcutv_part ,
		          pcutv_vend ,
		          pcutv_diff_price ,
		          createby ,
		          createname ,
		          createdate ,
		          modifyby ,
		          modifyname ,
		          modifydate ,
		          @uID,
		          GETDATE()
		FROM dbo.pcu_part_vend_type
		WHERE pcutv_type = @pcutv_type
		AND pcutv_part = @pcutv_part
		AND pcutv_vend = @pcutv_vend
		IF(@@error <> 0) SET @error = -1

		DELETE FROM  dbo.pcu_part_vend_type
		WHERE pcutv_type = @pcutv_type
		AND pcutv_part = @pcutv_part
		AND pcutv_vend = @pcutv_vend
		IF(@@error <> 0) SET @error = -1

		IF(@@error <> 0 OR @error < 0)
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
	begin
		SET @retValue = 0
		SET @errMsg = N'该记录在区间价格管理中有关联数据，请先删除'
	END
	
END


