USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcm_selectCheckPriceFinishedForPart]    Script Date: 2015/5/16 11:13:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Young Yang
-- Create date: 2014-11-19
-- Description:	查询零件报价所有供应商完成情况
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcm_selectCheckPriceFinishedForPart]
	@part VARCHAR(20)
   ,@ItemCode NVARCHAR(50)  
   ,@finished INT 
   ,@vender VARCHAR(20)
AS
BEGIN
	SELECT a.part,a.ItemCode,a.ItemDescription,a.ItemDesc1,a.ItemDesc2,a.Formate,CAST(ISNULL(b.checkNum,0) AS VARCHAR(3))+'/'+CAST(a.num AS VARCHAR(3)) AS rate,
	CASE WHEN b.checkNum=a.num THEN 1 ELSE 0 END AS finished,PQID
	FROM 
	(SELECT  COUNT(*) AS num,part,ItemCode,MAX(ItemDescription) AS ItemDescription,MAX(ItemDesc1) AS ItemDesc1,
	MAX(ItemDesc2) AS ItemDesc2,MAX(Formate) AS Formate ,PQID 
	FROM dbo.PCM_ApplyDet
	WHERE status>=2 AND status<=4 and isnull(isCancel,0)=0
	AND (@vender='' OR (@vender<>'' AND Vender LIKE REPLACE(@vender,'*','%')))
	GROUP BY Part,ItemCode,PQID) a
	LEFT JOIN 
	(SELECT COUNT(*) AS checkNum,part,ItemCode 
	FROM dbo.PCM_ApplyDet
	WHERE CheckPrice IS NOT NULL AND status=4
	AND (@vender='' OR (@vender<>'' AND Vender LIKE REPLACE(@vender,'*','%')))
	GROUP BY Part,ItemCode) b
	ON a.Part=b.Part AND a.ItemCode=b.ItemCode
    WHERE (@finished=0 OR (@finished=1 AND b.checkNum=a.num) OR (@finished=2 AND ISNULL(b.checkNum,0)<>a.num))
    AND (@part='' OR (@part<>'' AND a.part LIKE REPLACE(@part,'*','%')))
    AND (@ItemCode='' OR (@ItemCode<>'' AND a.ItemCode LIKE REPLACE(@ItemCode,'*','%')))
	
    
END




