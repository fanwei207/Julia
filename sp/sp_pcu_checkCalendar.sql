USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_checkCalendar]    Script Date: 2015/6/8 15:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fanwei	>
-- Create date: <20150422>
-- Description:	<日期期间表验证>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcu_checkCalendar]

	@pageID UNIQUEIDENTIFIER  
	, @xmlParam xml
	, @retValue BIT OUTPUT 
	, @errMsg NVARCHAR(400) OUTPUT
	, @pcus_type NVARCHAR(200)
	, @pcus_price DECIMAL(18,5)
	, @pcus_starDate DATETIME
	, @pcus_endDate DATETIME
AS
BEGIN
/*

DECLARE @retValue BIT
DECLARE @errMsg NVARCHAR(400)
exec sp_pcu_checkCalendar 11 , N'',@retValue OUTPUT,N'ITTest',12212,'2014-02-01','2014-03-01',@errMsg OUTPUT

*/
	SET @retValue = 1
	SET @errMsg = N''
	DECLARE @lastDate DATETIME
	
	IF(EXISTS(SELECT * FROM dbo.pcu_type WHERE pcut_type=@pcus_type))
	BEGIN
		IF(EXISTS(SELECT * FROM dbo.pcu_calendar WHERE pcus_type=@pcus_type))
		BEGIN
			IF(ISNUMERIC(@pcus_price)=1 )
			BEGIN
				IF(@pcus_price >= 0)
				BEGIN          
					IF(@pcus_starDate>=@pcus_endDate)
					BEGIN
						SET @retValue = 0
						SET @errMsg=N'价格的起始时间必须小于价格结束时间'
					END
					ELSE
					BEGIN
						SELECT @lastDate = MAX(pcus_endDate)
						from dbo.pcu_calendar
						WHERE pcus_type=@pcus_type
						
						IF(@pcus_starDate <> DATEADD(DAY,1,ISNULL(@lastDate,DATEADD(DAY,-1,@pcus_starDate))))
						BEGIN
							SET @retValue = 0
							SET @errMsg=N'期间必须连续，上一个期间的结束日期为：'+CONVERT(NVARCHAR(10),@lastDate,120)
						END
						ELSE
						BEGIN
							IF(EXISTS (SELECT * FROM dbo.pcu_calendar WHERE pcus_isCimload = 0 AND pcus_type = @pcus_type) )
							BEGIN
								SET @retValue = 0
								SET @errMsg=N'该类型已存在活跃期间，请将该类型数据导出cimload再添加期间'
							END
							ELSE
							BEGIN
								IF(NOT EXISTS(SELECT * 
												FROM dbo.pcu_section
												WHERE pcud_section_start <= @pcus_price
												AND pcud_section_end >= @pcus_price
												AND pcud_type = @pcus_type
												))
								BEGIN
									SET @retValue = 0
									SET @errMsg=N'该类型中，您输入的价格没有对应价格区间，请先维护价格区间'
								END
							END
						
						END
					
					END
				END
				ELSE
				BEGIN
					SET @retValue = 0
					SET @errMsg=N'输入的价格必须是大于0 的数字'              
				END                  
			END
			ELSE
			BEGIN
				SET @retValue = 0
				SET @errMsg=N'输入的价格必须是数字'
			END
		END
		ELSE
		BEGIN
						IF(NOT EXISTS(SELECT * 
											FROM dbo.pcu_section
											WHERE pcud_section_start <= @pcus_price
											AND pcud_section_end >= @pcus_price
											AND pcud_type = @pcus_type
											))
						BEGIN
								SET @retValue = 0
								SET @errMsg=N'该类型中，您输入的价格没有对应区间，请先维护区间'
						END
		END      
	END
	ELSE
	BEGIN
		SET @retValue = 0
		SET @errMsg=N'选中类型不在类型表中，请先添加该类型'
	END
	
	IF(@retValue<>0)
	BEGIN
		BEGIN TRAN 
		
		INSERT INTO dbo.pcu_calendar
		        ( pcus_starDate ,
		          pcus_endDate ,
		          pcus_price ,
		          pcus_type ,
		          createBy ,
		          createDate ,
		          createName 
		        )
		VALUES  ( @pcus_starDate , -- pcus_starDate - datetime
		          @pcus_endDate , -- pcus_endDate - datetime
		          @pcus_price , -- pcus_price - decimal
		          @pcus_type , -- pcus_type - nvarchar(50)
		          @xmlParam.value('(/Param/uID/@Value)[1]', 'int') , -- createBy - int
		          GETDATE() , -- createDate - datetime
		          @xmlParam.value('(/Param/uName/@Value)[1]', 'nvarchar(max)')  -- createName - nvarchar(20)
		        )
		        
		        
		IF(@@error<>0 )
		BEGIN
			ROLLBACK TRAN
			SET @retValue=0
			SET @errMsg=N'数据库出错，请联系管理员！'
		END
		ELSE
		BEGIN
			COMMIT TRAN
			SET @retValue=1
		END
	END

END

