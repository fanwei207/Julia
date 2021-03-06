USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_checkViewSectionPriceImport]    Script Date: 2015/6/15 16:22:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<fanwei>
-- Create date: <20150508>
-- Description:	<检验区间价格插入>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcu_checkViewSectionPriceImport]
	@pageID UNIQUEIDENTIFIER
	,@uID INT
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
	

	DELETE FROM  WorkFlow.dbo.Page_ImportTemp
	WHERE  pageID = @pageID 
		AND uID = @uID
		AND ISNULL(col0,N'') = N''
		AND ISNULL(col1,N'') = N''
		AND ISNULL(col2,N'') = N''
		AND ISNULL(col3,N'') = N''
		AND ISNULL(col4,N'') = N''
		AND ISNULL(col5,N'') = N''
		AND ISNULL(col6,N'') = N''
		AND ISNULL(col7,N'') = N''

	UPDATE pit
	SET error = isnull(ERROR,N'') + N'类型不存在;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND(NOT EXISTS(
			SELECT  * 
			FROM dbo.pcu_type 
			WHERE  pcut_type = pit.col0
			))

	UPDATE pit
	SET error = isnull(ERROR,N'') + N'QAD长度应为14位，可能存在异常字符;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND LEN(col1) <> 14

	UPDATE pit
	SET error = isnull(ERROR,N'') + N'供应商长度应为8位，可能存在异常字符;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND LEN(col2) <> 8
		
				
	UPDATE pit
	SET error = isnull(ERROR,N'') + N'该类型下，不存在该QAD与供应商组合;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND(not EXISTS(
			SELECT  * 
			FROM dbo.pcu_part_vend_type 
			WHERE pcutv_type = col0
			AND pcutv_part = col1
			AND pcutv_vend =col2
			))
	
	
			
	UPDATE pit
	SET error = isnull(ERROR,N'') + N'价格必须是数字;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND ISNUMERIC(pit.col5) = 0
	
	UPDATE pit
	SET error = isnull(ERROR,N'') + N'该类型下，区间不存在;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND(not EXISTS(
			SELECT  * 
			FROM dbo.pcu_section 
			WHERE pcud_type = col0
			AND pcud_section_start = col3
			AND pcud_section_end =col4
			))
	
	UPDATE pit
	SET error = isnull(ERROR,N'') + N'该excel有两条相同数据，请检查;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND EXISTS (	
					SELECT *
					FROM WorkFlow.dbo.Page_ImportTemp pit1
					WHERE  pit1.col1 = pit.col1
					AND  pit1.col2 = pit.col2
					AND  pit1.col0 = pit.col0
					AND  pit1.col3 = pit.col3
					AND  pit1.col4 = pit.col4
					AND  pit1.pageID = @pageID 
					AND pit1.uID = @uID
					HAVING COUNT(*)>=2
					)
    
	UPDATE pit
	SET error = isnull(ERROR,N'') + N'excel的“删除”这一列只能填写“删除”或不填写，请检查excel;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND pit.col6 <> '' 
		AND pit.col6 <>N'删除'

	UPDATE pit
	SET error = isnull(ERROR,N'') + N'excel的“挂起”这一列只能填写“挂起”或不填写，请检查excel;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND pit.col7 <> '' 
		AND pit.col7 <>N'挂起'
 
END

