USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_checkSection]    Script Date: 2015/6/8 14:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fanwei>
-- Create date: <20150504>
-- Description:	<检验区间>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcu_checkSection]
	
	@pageID UNIQUEIDENTIFIER 
	, @xmlParam xml
	, @retValue BIT OUTPUT 
	, @errMsg NVARCHAR(400) OUTPUT
	, @pcud_type NVARCHAR(50)
	, @pcud_section_start DECIMAL(18,5)
	, @pcud_section_end DECIMAL(18,5)
AS 
BEGIN
/*

DECLARE @uID INT = 13
	,@uName  NVARCHAR(20) = N'管理员'
	,@retValue BIT   
	,@errMsg NVARCHAR(400) 
	,@pcud_type NVARCHAR(50) =N'铁价'
	,@pcud_section_start DECIMAL(18,5)= 50
	,@pcud_section_end DECIMAL(18,5)=99.9
	
EXEC sp_pcu_checkSection @uID,@uName,@retValue output,@errMsg output,@pcud_type,@pcud_section_start,@pcud_section_end

	select @retValue,@errMsg
*/
	SET @retValue = 1
	SET @errMsg =N''
	IF(ISNUMERIC(@pcud_section_start)=1 AND ISNUMERIC(@pcud_section_end)=1)
	BEGIN
		IF(@pcud_section_start >= 0 AND @pcud_section_end >= 0)
		BEGIN  
			IF(@pcud_section_end <= @pcud_section_start)
			BEGIN
				SET @retValue = 0
				SET @errMsg =N'新增时区间的最高价格必须大于最低价格'
			END
			ELSE
			BEGIN
				IF(EXISTS(SELECT * FROM  dbo.pcu_type WHERE pcut_type=@pcud_type) )
				BEGIN
					IF( EXISTS(SELECT * 
							FROM dbo.pcu_section
							WHERE pcud_section_start <= @pcud_section_start 
							AND pcud_section_end >= @pcud_section_start 
							AND pcud_type =@pcud_type)
						OR   EXISTS(SELECT * 
							FROM dbo.pcu_section
							WHERE pcud_section_start <= @pcud_section_end 
							AND pcud_section_end >= @pcud_section_end
							AND pcud_type =@pcud_type))
					BEGIN
						SET @retValue = 0
						SET @errMsg =N'价格的区间不可重叠，请查清原有区间再添加'           
					END
					ELSE
					BEGIN
						--排除区间包含的情况                   
						IF( EXISTS(SELECT * 
							FROM dbo.pcu_section
							WHERE pcud_section_start >= @pcud_section_start 
							AND pcud_section_start <= @pcud_section_end 
							AND pcud_type =@pcud_type)
						OR   EXISTS(SELECT * 
							FROM dbo.pcu_section
							WHERE pcud_section_end >= @pcud_section_start 
							AND pcud_section_end <= @pcud_section_end
							AND pcud_type =@pcud_type))
						BEGIN
							SET @retValue = 0
							SET @errMsg =N'价格的区间不可重叠，请查清原有区间再添加'   
						END	
					END                 
				END
				ELSE
				BEGIN
					SET @retValue = 0
					SET @errMsg =N'类型不存在'
				END
			END
		END
		ELSE
		begin
			SET @retValue = 0
			SET @errMsg =N'价格区间必须是大于0的数字'
		END          
	END
	ELSE
	BEGIN
		SET @retValue = 0
		SET @errMsg =N'价格区间必须是数字'
	END
	
	IF(@retValue<>0)
	BEGIN
		
		
		BEGIN TRAN
		
		INSERT INTO dbo.pcu_section
		        ( 
		          pcud_type ,
		          pcud_section_start ,
		          pcud_section_end ,
		          createby ,
		          createname ,
		          createdate 
		        )
		VALUES  (
		          @pcud_type , -- pcud_type - nvarchar(50)
		          @pcud_section_start , -- pcud_section_start - decimal
		          @pcud_section_end , -- pcud_section_end - decimal
		          @xmlParam.value('(/Param/uID/@Value)[1]', 'int') , -- createby - int
		          @xmlParam.value('(/Param/uName/@Value)[1]', 'nvarchar(max)') , -- createname - nvarchar(50)
		          GETDATE()  -- createdate - datetime
		        )
		 
		IF(@@error <> 0)
		BEGIN
			ROLLBACK TRAN
			SET @retValue = 0
			SET @errMsg =N'数据库出错，请联系管理员！'
			
		END
		ELSE
		BEGIN
			COMMIT TRAN
			SET @retValue = 1
		END
		
		
		 
	END

END

