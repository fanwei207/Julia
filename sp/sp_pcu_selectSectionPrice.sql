USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_selectSectionPrice]    Script Date: 2015/6/11 16:20:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[sp_pcu_selectSectionPrice]  -- exec sp_pcu_selectSectionPrice 
   @type NVARCHAR(200)
  ,@vend VARCHAR(10)
  ,@QAD VARCHAR(20)
  ,@calender UNIQUEIDENTIFIER 
  ,@section NVARCHAR(200) OUTPUT
AS
BEGIN
/*
DECLARE @type NVARCHAR(200) =N'铜价'
  ,@vend VARCHAR(20) = ''
  ,@QAD VARCHAR(20) = ''
  ,@
*/

IF(@vend='')
BEGIN
	SET @vend=''''''
END


IF(@QAD='')
BEGIN
	SET @QAD=''''''
END



SELECT @section = CAST(ps.pcud_id AS NVARCHAR(200))
FROM dbo.pcu_calendar pc LEFT JOIN  dbo.pcu_section ps ON pc.pcus_type = ps.pcud_type
WHERE pcus_type = @type
AND pcus_id = @calender
AND ps.pcud_section_start <= pc.pcus_price 
AND ps.pcud_section_end >= pc.pcus_price



DECLARE  @sql nvarchar(2000)
SET  @sql = N''
SELECT @sql=@sql+',['+ CAST(CAST(pcud_section_start AS FLOAT) AS NVARCHAR(200))+'--'+CAST(CAST(pcud_section_end AS FLOAT) AS NVARCHAR(200))+']'  
from dbo.pcu_section
WHERE pcud_type = @type
SET @sql=stuff(@sql,1,1,'')--去掉首个';'

SET @sql=N'
SELECT 
pcut_type  as 类型
,pcups_part as QAD
,pcups_vend as 供应商
,pcutv_diff_price as 价格差异
,
'+@sql +N'
,description as 描述
,ad_name as 供应商名
from 
(
SELECT  pcut_type,pcups_part,pcups_vend,pcutv_diff_price,SECTION,pcups_price,description,ad_name
FROM 
(
SELECT   pt.pcut_type,pps.pcups_part,pps.pcups_vend,ppvt.pcutv_diff_price
	,CAST(CAST(pcud_section_start AS FLOAT) AS NVARCHAR(200))+''--''+CAST(CAST(pcud_section_end AS FLOAT) AS NVARCHAR(200)) AS SECTION
	,pps.pcups_price
	,adm.ad_name
	,i.description
FROM dbo.pcu_type pt 
LEFT JOIN dbo.pcu_section ps ON pt.pcut_type=ps.pcud_type 
LEFT JOIN dbo.pcu_part_vend_type ppvt ON ppvt.pcutv_type=pt.pcut_type
LEFT JOIN dbo.pcu_price_section pps ON pps.pcups_type=pt.pcut_type 
AND pps.pcups_part=ppvt.pcutv_part AND pps.pcups_vend=ppvt.pcutv_vend
AND pps.pcud_id=ps.pcud_id
left join	( SELECT ad_addr,MAX(ad_name) ad_name
			from QAD_Data.dbo.ad_mstr
			where ad_type = ''supplier''
			GROUP BY ad_addr
			 ) as adm  on pps.pcups_vend = adm.ad_addr
left join dbo.Items i on pps.pcups_part = i.item_qad
WHERE ISNULL(pps.pcups_part,'''')<>'''' 
	AND ISNULL(pps.pcups_vend,'''')<>'''' 
	and pps.pcups_type=N'''+@type+'''
	and (' + @vend + '='''' or (' +  @vend + ' <>'''' and pps.pcups_vend like '+@vend+'''%''))
	and (' + @QAD+ '='''' or (' + @QAD + ' <>'''' and pps.pcups_part like '+@QAD+'''%''))
	and pps.pcups_isHang = 0
	) AS mid
WHERE ISNULL(mid.SECTION,'''')<>''''
)AS ending
PIVOT( max(pcups_price) FOR SECTION IN ('+@sql +'))  a '

--PRINT @sql

EXECUTE (@sql)



END



