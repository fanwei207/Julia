USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_deleteSection]    Script Date: 2015/6/8 15:38:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fanwei>
-- Create date: <20150515>
-- Description:	<删除区间>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcu_deleteSection]
	 @pageID UNIQUEIDENTIFIER
	, @uID INT
	, @retValue BIT OUT
	, @errMsg NVARCHAR(400) OUTPUT
	, @pcud_type NVARCHAR(200) 
	, @pcud_section_start DECIMAL(18,5)
AS
BEGIN
	SET @retValue = 1
	SET @errMsg = N''
	
	DECLARE @error INT 
	SET @error = 0

	DECLARE @pcud_section_end DECIMAL(18,5)
	SELECT @pcud_section_end = pcud_section_end
	FROM dbo.pcu_section
	WHERE pcud_type = @pcud_type
	AND pcud_section_start = @pcud_section_start

	IF(not EXISTS(SELECT * 
					FROM dbo.pcu_section ps 
					LEFT JOIN dbo.pcu_price_section pps ON pps.pcud_id = ps.pcud_id
					WHERE ISNULL(pps.pcups_part,'') <> ''
					AND ps.pcud_type = @pcud_type
					AND ps.pcud_section_start = @pcud_section_start ))
	BEGIN
		IF(NOT EXISTS(SELECT * 
					FROM dbo.pcu_calendar
					WHERE pcus_type = @pcud_type
					AND pcus_price <= @pcud_section_end
					AND pcus_price >= @pcud_section_start ))
		BEGIN  
			BEGIN TRAN
	
			INSERT INTO dbo.pcu_section_hist
					( pcud_id ,
					  pcud_type ,
					  pcud_section_start ,
					  pcud_section_end ,
					  createby ,
					  createname ,
					  createdate ,
					  modifyby ,
					  modifyname ,
					  modifydate ,
					  deleteby ,
					  deleteDate
					)
			SELECT pcud_id,pcud_type,pcud_section_start,pcud_section_end,createby,createname,createdate
				,modifyby,modifyname,modifydate,@uID,GETDATE()
			from dbo.pcu_section 
			WHERE pcud_section_start = @pcud_section_start
			AND pcud_type = @pcud_type
			IF(@@error <> 0) SET @error = -1
		
			DELETE FROM pcu_section
			WHERE pcud_section_start = @pcud_section_start
			AND pcud_type = @pcud_type
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
		BEGIN
			SET @retValue = 0
			SET @errMsg = N'该记录在期间管理中有关联数据，请先删除'
		END          
	END
	ELSE
	BEGIN
		SET @retValue = 0
		SET @errMsg = N'该记录在区间价格管理中有关联数据，请先删除'
	END
	
END


