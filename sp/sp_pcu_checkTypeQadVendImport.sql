USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_checkTypeQadVendImport]    Script Date: 2015/6/15 16:31:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<fanwei>
-- Create date: <20150505>
-- Description:	<批量导入QAD 供应商验证>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcu_checkTypeQadVendImport]
	@pageID UNIQUEIDENTIFIER
	,@uID INT
    , @plantCode int = 1  
AS
BEGIN
	--col0 类型
	--col1 QAD
	--col2 vend
	--col3 差价
		

	DELETE FROM  WorkFlow.dbo.Page_ImportTemp
	WHERE  pageID = @pageID 
		AND uID = @uID
		AND ISNULL(col0,N'') = N''
		AND ISNULL(col1,N'') = N''
		AND ISNULL(col2,N'') = N''
		AND ISNULL(col3,N'') = N''


	UPDATE pit
	SET error = isnull(ERROR,N'') + N'供应商不存在;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE pit.pageID = @pageID 
		AND pit.uID = @uID
		AND NOT EXISTS(SELECT * 
					FROM QAD_Data..ad_mstr 
					where ad_type = 'supplier' 
					and ad_addr = pit.col2 )
				
	UPDATE pit
	SET error = isnull(ERROR,N'') + N'QAD不存在;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE pit.pageID = @pageID 
		AND pit.uID = @uID
		AND NOT EXISTS(select pt_part
					from QAD_DATA.dbo.pt_mstr
					where pt_part = pit.col1 )
	
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
	SET error = isnull(ERROR,N'') + N'价格差异只能是数字;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE pit.pageID = @pageID 
		AND pit.uID = @uID
		AND ISNUMERIC(pit.col3) = 0
	
	
	
	UPDATE pit
	SET error = isnull(ERROR,N'') + N'类型不存在;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND NOT EXISTS(SELECT * 
				from dbo.pcu_type 
				WHERE pcut_type = pit.col0 )
				
	UPDATE pit
	SET error = isnull(ERROR,N'') + N'该物料供应商的组合不存在于价格表中;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND NOT EXISTS (
						SELECT * 
						FROM dbo.PC_mstr
						WHERE pit.col1 = pc_part 
						AND pit.col2 = pc_list
						)
					
	UPDATE pit
	SET error = isnull(ERROR,N'') +N'该物料供应商的组合已在数据中，请勿重复插入;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE  pit.pageID = @pageID 
		AND pit.uID = @uID
		AND  EXISTS (
						SELECT * 
						FROM dbo.pcu_part_vend_type
						WHERE pit.col1 = pcutv_part 
						AND pit.col2 = pcutv_vend
						AND pit.col0 = pcutv_Type
					)
	UPDATE pit
	SET error =isnull(ERROR,N'') + N'该物料供应商的组合在本excel中有多条记录，请勿重复插入;'
	FROM WorkFlow.dbo.Page_ImportTemp pit
	WHERE   pit.pageID = @pageID 
		AND pit.uID = @uID
		AND EXISTS (	
					SELECT *
					FROM WorkFlow.dbo.Page_ImportTemp pit1
					WHERE  pit1.col1 = pit.col1
					AND  pit1.col2 = pit.col2
					AND  pit1.col0 = pit.col0
					AND  pit1.pageID = @pageID 
					AND pit1.uID = @uID
					HAVING COUNT(*)>=2
					)
END


