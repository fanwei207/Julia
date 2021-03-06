USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_deleteCalendar]    Script Date: 2015/6/8 15:35:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fanwei>
-- Create date: <20150515>
-- Description:	<删除期间>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcu_deleteCalendar]
	 @pageID UNIQUEIDENTIFIER
	, @uID INT
	, @retValue BIT OUT
	, @errMsg NVARCHAR(400) OUTPUT
	, @pcus_starDate DATETIME
	, @pcus_endDate DATETIME
	, @pcus_type NVARCHAR(200) 

AS
BEGIN
	SET @retValue = 1
	SET @errMsg = N''
	DECLARE @error INT 
	SET @error = 0

	IF( EXISTS(SELECT *
				FROM dbo.pcu_calendar
				WHERE pcus_starDate = @pcus_starDate
				AND pcus_endDate = @pcus_endDate
				AND pcus_type = @pcus_type
				AND pcus_isCimload = 0
				))
	BEGIN
		BEGIN TRAN
		INSERT INTO dbo.pcu_calendar_hist
		        (  pcus_id,
				  pcus_starDate ,
		          pcus_endDate ,
		          pcus_price ,
		          pcus_type ,
		          pcus_isCimload ,
		          createBy ,
		          createDate ,
		          createName ,
		          modifyBy ,
		          modifyDate ,
		          modifyName ,
		          pcus_CimloadBy ,
		          pcus_CimloadDate ,
		          deleteBy ,
		          deleteDate
		        )
		SELECT  pcus_id,
				pcus_starDate ,
		          pcus_endDate ,
		          pcus_price ,
		          pcus_type ,
		          pcus_isCimload ,
		          createBy ,
		          createDate ,
		          createName ,
		          modifyBy ,
		          modifyDate ,
		          modifyName ,
		          pcus_CimloadBy ,
		          pcus_CimloadDate ,
		          @uID,
		          GETDATE()
		FROM pcu_calendar 
		WHERE pcus_starDate = @pcus_starDate
		AND pcus_endDate = @pcus_endDate
		AND pcus_type = @pcus_type
		IF(@@error <> 0) SET @error = -1
       
		DELETE FROM  dbo.pcu_calendar
		WHERE pcus_starDate = @pcus_starDate
		AND pcus_endDate = @pcus_endDate
		AND pcus_type = @pcus_type
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
		SET @errMsg = N'该期间cimload已完成，不能删除'
	END
END




