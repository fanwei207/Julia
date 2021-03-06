USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_checkViewSectionPrice]    Script Date: 2015/6/8 15:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fanwei>
-- Create date: <20150505>
-- Description:	<区间价格验证>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcu_checkViewSectionPrice]
	
	@pageID UNIQUEIDENTIFIER
	, @xmlParam xml
	, @retValue BIT OUTPUT 
	, @errMsg NVARCHAR(400) OUTPUT
	, @pcups_type NVARCHAR(20)
	, @pcups_part VARCHAR(20)
	, @pcups_vend VARCHAR(10)
	, @pcud_section_start DECIMAL(18,5)
	, @pcud_section_end DECIMAL(18,5)
	, @pcups_price DECIMAL(18,5)
AS
BEGIN
	SET @retValue = 1
	SET @errMsg =N''

/*
1,区间id不能为空
2，QAD 和供应商的组合必须在pcu_part_vend_type表中
3价格是数字
*/

	IF(NOT EXISTS (SELECT  * 
					FROM dbo.pcu_section 
					WHERE pcud_section_start=@pcud_section_start
					AND pcud_section_end = @pcud_section_end
					AND pcud_type = @pcups_type ))
	BEGIN
		SET @retValue = 0
		SET @errMsg = N'价格区间对应不正确'
	END
	ELSE
	BEGIN
		IF(NOT EXISTS(SELECT * 
					FROM dbo.pcu_part_vend_type
					WHERE pcutv_part=@pcups_part
					 AND pcutv_vend=@pcups_vend
					 AND pcutv_type = @pcups_type))
		BEGIN
			SET @retValue = 0
			SET @errMsg = N'供应商物料的组合必须在“供应商物料管理”中'
			
		END
		ELSE
		BEGIN
			IF(EXISTS(SELECT * 
						FROM dbo.pcu_price_section pps 
						LEFT JOIN dbo.pcu_section ps ON pps.pcud_id = ps.pcud_id
						WHERE pps.pcups_part=@pcups_part
						AND pps.pcups_vend=@pcups_vend 
						AND ps.pcud_section_start = @pcud_section_start
						AND ps.pcud_section_end = @pcud_section_end
						AND pps.pcups_type = @pcups_type
						  ))
			BEGIN
				SET @retValue = 0
				SET @errMsg = N'供应商物料的组合不可重复'
			END
			ELSE
			BEGIN
				IF(ISNUMERIC(@pcups_price)=0)
				BEGIN
					SET @retValue = 0
					SET @errMsg = N'价格必须是数字'
				END
				ELSE
				BEGIN
					IF(@pcups_price < 0)
					BEGIN
						SET @retValue = 0
						SET @errMsg = N'价格必须大于0'                  
					END              
				END              
			END
			
		END
	END
	
	
	IF(@retValue <> 0)
	BEGIN
	
		DECLARE @pcud_id UNIQUEIDENTIFIER
		SELECT @pcud_id = pcud_id 
		from dbo.pcu_section
		WHERE pcud_type = @pcups_type
		AND pcud_section_start = @pcud_section_start  
		
		
		BEGIN TRAN
		
		INSERT INTO dbo.pcu_price_section
		        ( pcups_type ,
		          pcups_vend ,
		          pcups_part ,
		          pcups_price ,
		          pcud_id ,
		          createby ,
		          createname ,
		          createdate 
		        )
		VALUES  ( @pcups_type , -- pcups_type - nvarchar(50)
		          @pcups_vend , -- pcups_vend - varchar(10)
		          @pcups_part , -- pcups_part - varchar(20)
		          @pcups_price , -- pcups_price - decimal
		          @pcud_id , -- pcud_id - uniqueidentifier
		          @xmlParam.value('(/Param/uID/@Value)[1]', 'int') , -- createby - int
		          @xmlParam.value('(/Param/uName/@Value)[1]', 'nvarchar(max)') , -- createname - nvarchar(50)
		          GETDATE()
		        )
		
		        
		        
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
	
	
END

