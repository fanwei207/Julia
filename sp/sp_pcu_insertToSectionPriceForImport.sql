USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_insertToSectionPriceForImport]    Script Date: 2015/7/2 10:40:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<fanwei>
-- Create date: <20150510>
-- Description:	<将试图中的数据插入两张表>
-- =============================================
/*
   DECLARE @retValue BIT
   
exec   sp_pcu_insertToSectionPriceForImport '33F9E70C-99E5-4719-AC43-24B189CC4E76',36983,@retValue


*/

ALTER PROCEDURE [dbo].[sp_pcu_insertToSectionPriceForImport]
	@pageID UNIQUEIDENTIFIER
	,@uID INT
	,@retValue  BIT OUT  
AS
BEGIN
	--col0 类型
	--col1 QAD
	--col2 vend
	--col3 区间起始
	--col4 区间截止
	--col5 价格
	--col6 删除
	--col7 挂起
	
	BEGIN TRAN
		
		DECLARE @uName NVARCHAR(20)
		
		SELECT @uName =  userName
		from dbo.Users
		WHERE userID = @uID
		
		DECLARE @error int
		SET @error = 0

		/*
		  将删除的部分添加历史表
		*/
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
		FROM WorkFlow.dbo.Page_ImportTemp pit 
		INNER JOIN dbo.pcu_price_section pps ON pps.pcups_type = pit.col0 
		AND pps.pcups_part = pit.col1
		AND pps.pcups_vend = pit.col2
		INNER JOIN dbo.pcu_section ps ON ps.pcud_id = pps.pcud_id
		WHERE ps.pcud_section_start = pit.col3 
		AND ps.pcud_section_end = pit.col4
		AND pps.pcups_type = pit.col0
		AND ps.pcud_type = pit.col0
		AND pit.col6 = N'删除'
		AND pit.pageID = @pageID
		AND pit.uID = @uID 
		IF(@@error <> 0) SET @error = -1

		DELETE FROM pcu_price_section 
		WHERE EXISTS(SELECT * 
					FROM WorkFlow.dbo.Page_ImportTemp pit 
					INNER JOIN dbo.pcu_section ps ON ps.pcud_type = pit.col0
					AND ps.pcud_section_start = pit.col3
					AND ps.pcud_section_end = pit.col4
					AND pit.col6 = N'删除'
					AND pit.col1 = pcu_price_section.pcups_part
					AND pit.col2 = pcu_price_section.pcups_vend 
					AND pit.col0 = pcu_price_section.pcups_type
					AND pit.pageID = @pageID
					AND pit.uID = @uID)
		IF(@@error <> 0) SET @error = -1

		/*
			修改的部分
		*/
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
		FROM WorkFlow.dbo.Page_ImportTemp pit 
		INNER JOIN dbo.pcu_price_section pps ON pps.pcups_type = pit.col0 
		AND pps.pcups_part = pit.col1
		AND pps.pcups_vend = pit.col2
		INNER JOIN dbo.pcu_section ps ON ps.pcud_id = pps.pcud_id
		WHERE ps.pcud_section_start = pit.col3 
		AND ps.pcud_section_end = pit.col4
		AND pps.pcups_type = pit.col0
		AND ps.pcud_type = pit.col0
		AND (pps.pcups_price <> pit.col5
		OR  CASE WHEN pit.col7 = N'挂起' THEN 1 ELSE 0 END <> ISNULL(pps.pcups_isHang,0))
		AND pit.col6 = N''
		AND pit.pageID = @pageID
		AND pit.uID = @uID
		IF(@@error <> 0) SET @error = -1

		UPDATE pps
		set pps.pcups_price = cast(pit.col5 AS DECIMAL(18,5)),pps.modifyby = @uID , pps.modifyname = @uName ,pps.modifydate = GETDATE()
		,pcups_isHang = CASE WHEN pit.col7 = N'挂起' THEN 1 ELSE 0 END  , pcups_hangBy = CASE WHEN pit.col7 = N'挂起' THEN @uID ELSE NULL END  
		, pcups_hangDate = CASE WHEN pit.col7 = N'挂起' THEN  GETDATE() ELSE null END 
		FROM dbo.pcu_price_section pps
		INNER JOIN WorkFlow.dbo.Page_ImportTemp pit  ON pps.pcups_type = pit.col0 
		AND pps.pcups_part = pit.col1
		AND pps.pcups_vend = pit.col2
		INNER JOIN dbo.pcu_section ps ON ps.pcud_id = pps.pcud_id
		WHERE ps.pcud_section_start = pit.col3 
		AND ps.pcud_section_end = pit.col4
		AND pps.pcups_type = pit.col0
		AND ps.pcud_type = pit.col0
		AND (pps.pcups_price <> pit.col5
		OR  CASE WHEN pit.col7 = N'挂起' THEN 1 ELSE 0 END <> ISNULL(pps.pcups_isHang,0))
		AND pit.col6 = N''
		AND pit.pageID = @pageID
		AND pit.uID = @uID
		IF(@@error <> 0) SET @error = -1

		/*
			插入部分
			1，原来有对应的供应商QAD
			2，没有对应的供应商与QAD
		*/
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
		SELECT pit.col0,pit.col2,pit.col1,pit.col5,ps.pcud_id,@uID,@uName,GETDATE()
		FROM WorkFlow.dbo.Page_ImportTemp pit 
		LEFT JOIN dbo.pcu_price_section pps ON pps.pcups_type = pit.col0 
		AND pps.pcups_part = pit.col1
		AND pps.pcups_vend = pit.col2
		LEFT JOIN dbo.pcu_section ps ON ps.pcud_section_start = pit.col3
		AND ps.pcud_section_end = pit.col4
		AND ps.pcud_type =pit.col0
		WHERE ps.pcud_section_start = pit.col3 
		AND ps.pcud_section_end = pit.col4
		AND ps.pcud_type = pit.col0
		AND pit.col6 = N''
		AND pit.col7 = N''
		--AND pps.pcups_part IS NULL 
		AND pit.pageID = @pageID
		AND pit.uID = @uID

		IF(@@error <> 0) SET @error = -1

		IF(@@error <> 0 OR @error < 0)
		BEGIN
			ROLLBACK TRAN
			SET @retValue = 0
		END
		ELSE
		BEGIN
			COMMIT TRAN
			SET @retValue = 1
		END
END

