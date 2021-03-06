USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_checkTypeQadVend]    Script Date: 2015/6/8 15:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fanwei>
-- Create date: <20150505>
-- Description:	<检查类型零件供应商列表>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcu_checkTypeQadVend]
	
	@pageID UNIQUEIDENTIFIER
	, @xmlParam xml
	, @retValue BIT OUTPUT 
	, @errMsg NVARCHAR(400) OUTPUT
	, @pcutv_type NVARCHAR(20)
	, @pcutv_part VARCHAR(20)
	, @pcutv_vend VARCHAR(10)
	, @pcutv_diff_price DECIMAL
AS
BEGIN
/*

DECLARE @uID INT = 13
	,@uName  NVARCHAR(20) = N'管理员'
	,@retValue BIT   
	,@errMsg NVARCHAR(400) 
	,@pcutv_type NVARCHAR(20)=N'铜价'
	,@pcutv_part VARCHAR(20)='25010010003410'
	,@pcutv_vend VARCHAR(10)='S0574006'
	,@pcutv_diff_price DECIMAL 
	
EXEC sp_pcu_checkTypeQadVend @uID,@uName,@retValue output,@errMsg output,@pcutv_type,@pcutv_part,@pcutv_vend,@pcutv_diff_price

*/
	SET @retValue = 1
	SET @errMsg =N''
	
	IF(NOT EXISTS(SELECT * FROM QAD_Data..ad_mstr where ad_type = 'supplier' and ad_addr=@pcutv_vend))
	BEGIN
		SET @retValue = 0
		SET @errMsg =N'供应商不存在'
	END
	ELSE
	BEGIN
		IF(NOT EXISTS(select top 1 * from QAD_Data.dbo.pt_mstr WHERE pt_part =@pcutv_part ))
		BEGIN
			SET @retValue = 0
			SET @errMsg =N'QAD不存在'
		END
		ELSE
		BEGIN
			IF(NOT EXISTS(SELECT * FROM dbo.PC_mstr WHERE pc_part=@pcutv_part AND pc_list=@pcutv_vend))
			BEGIN
				SET @retValue = 0
				SET @errMsg =N'该物料供应商组合不存在于价格表中'
			END
			ELSE
			BEGIN
				IF(EXISTS(SELECT  * FROM dbo.pcu_part_vend_type WHERE pcutv_part=@pcutv_part AND pcutv_vend=@pcutv_vend))
				BEGIN
					SET @retValue = 0
					SET @errMsg =N'该物料供应商组合已存在，请勿重复插入'
				END
			END
		END
	END
	
	IF(@retValue<>0)
	BEGIN
		BEGIN TRAN
		
		INSERT  INTO dbo.pcu_part_vend_type
		        ( pcutv_type ,
		          pcutv_part ,
		          pcutv_vend ,
		          pcutv_diff_price ,
		          createby ,
		          createname ,
		          createdate 
		        )
		VALUES  (  @pcutv_type, -- pcutv_type - nvarchar(50)
		          @pcutv_part , -- pcutv_part - varchar(20)
		          @pcutv_vend , -- pcutv_vend - varchar(10)
		          @pcutv_diff_price , -- pcutv_diff_price - decimal
		          @xmlParam.value('(/Param/uID/@Value)[1]', 'int') , -- createby - int
		          @xmlParam.value('(/Param/uName/@Value)[1]', 'nvarchar(max)') , -- createname - nvarchar(50)
		          GETDATE() 
		        )
		IF(@@error <> 0 )
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

