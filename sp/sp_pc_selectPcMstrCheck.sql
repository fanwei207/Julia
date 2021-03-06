USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pc_selectPcMstrCheck]    Script Date: 2015/6/17 10:33:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Young Yang
-- Create date: 2014-12-19
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pc_selectPcMstrCheck] --EXEC sp_pc_selectPcMstrCheck '','','','all',0
	 @vender varchar(10)
	,@venderName nvarchar(50)
	,@QAD varchar(20)
	,@domain varchar(8)
	,@check bit
	,@diff BIT
	,@is100 BIT
	,@type NVARCHAR(20)  
AS
BEGIN
		
		
		select  ISNULL(p1.pc_id,-1) AS pc_id,
			ISNULL(p1.pc_list,p2.pc_list) AS pc_list ,
			ISNULL(p1.pc_part,p2.pc_part) AS pc_part ,
			ISNULL(p1.pc_um,p2.pc_um) AS pc_um ,
			p1.pc_price ,
			CONVERT(VARCHAR(10),ISNULL(p1.pc_start,p2.pc_start),120) pc_start ,
			CONVERT(VARCHAR(10),ISNULL(p1.pc_expire,p2.pc_expire),120) pc_expire ,
			ISNULL(p1.pc_domain,p2.pc_domain) AS pc_domain ,
			ISNULL(p1.pc_curr,p2.pc_curr) AS pc_curr,
			ISNULL(p1.isCheck,0) AS isCheck ,
			CAST(SUBSTRING(p1.pc_amt,1,CHARINDEX(';',p1.pc_amt)-1) AS DECIMAL(18,5)) AS price1,--100
			CAST(SUBSTRING(p2.pc_amt,1,CHARINDEX(';',p2.pc_amt)-1) AS DECIMAL(18,5)) AS price2,--QAD
			am.ad_name
		FROM dbo.PC_mstr p1
		FULL JOIN QAD_Data.dbo.PC_mstr p2
		ON p1.pc_list=p2.pc_list AND p1.pc_domain=p2.pc_domain AND p1.pc_part=p2.pc_part AND p1.pc_start=p2.pc_start
		AND p1.pc_um=p2.pc_um AND p1.pc_curr=p2.pc_curr AND ISNULL(p1.pc_expire,'3000-12-31')=ISNULL(p2.pc_expire,'3000-12-31') 
		left JOIN(
			SELECT MAX(ad_name) AS ad_name,ad_addr FROM 
			QAD_Data..ad_mstr
			WHERE ad_type = 'supplier' 
			GROUP BY ad_addr
		) am 
		on ISNULL(p1.pc_list,p2.pc_list)=am.ad_addr 
		WHERE (pc_status=1 OR  pc_status IS NULL)
		AND (@QAD ='' or(@QAD<>'' and p1.pc_part = @QAD)OR (@QAD<>'' and p2.pc_part = @QAD))
		and (@vender = '' or (@vender <>'' and p1.pc_list = @vender )OR (@vender <>'' and p2.pc_list = @vender ))
		and (@venderName = '' or (@venderName<>'' and am.ad_name =@venderName ))
		and (@domain='all' or (@domain<>'all' and p1.pc_domain = @domain)OR (@domain<>'all' and p2.pc_domain = @domain))
		AND ISNULL(isCheck,0)=@check
		AND (@is100 = 0 OR (@is100 <> 0 AND EXISTS(
					SELECT * 
					FROM
					(
						SELECT Part,Vender
						FROM dbo.PC_ApplyDet
						WHERE status = 6 
						AND isCancel = 0 
						UNION 
						SELECT Part,Vender
						FROM dbo.PCM_ApplyDet
						WHERE status = 6 
						AND isCancel = 0 
						UNION 
						SELECT pcutv_part AS Part,pcutv_vend AS Vender
						from dbo.pcu_part_vend_type
						) AS  a  
					WHERE a.Part=p1.pc_part 
					AND a.Vender = p1.pc_list
					)))
		AND (@type = '0' 
			OR (@type = '1' 
				AND  NOT EXISTS (SELECT *
								FROM dbo.pcu_part_vend_type ppvt
								INNER JOIN dbo.pcu_price_section pps ON ppvt.pcutv_type = pps.pcups_type
								AND ppvt.pcutv_part = pps.pcups_part
								AND ppvt.pcutv_vend = pps.pcups_vend
								WHERE p1.pc_list = pps.pcups_vend
								AND p1.pc_part =  pps.pcups_part )
				)
			OR (@type <> '0' AND @type <> '1'
				AND  EXISTS  (SELECT *
								FROM dbo.pcu_part_vend_type ppvt
								INNER JOIN dbo.pcu_price_section pps ON ppvt.pcutv_type = pps.pcups_type
								AND ppvt.pcutv_part = pps.pcups_part
								AND ppvt.pcutv_vend = pps.pcups_vend
								WHERE p1.pc_list = pps.pcups_vend
								AND p1.pc_part =  pps.pcups_part
								AND ppvt.pcutv_type = @type
							 )
				)
			)
		and ((@diff = 1 
				AND( (CAST(SUBSTRING(p1.pc_amt,1,CHARINDEX(';',p1.pc_amt)-1) AS DECIMAL(18,5))<> CAST(SUBSTRING(p2.pc_amt,1,CHARINDEX(';',p2.pc_amt)-1) AS DECIMAL(18,5)))
				OR (p1.pc_amt IS NULL )OR (p2.pc_amt IS NULL )))
			or  (@diff = 0 
				AND ( CAST(SUBSTRING(p1.pc_amt,1,CHARINDEX(';',p1.pc_amt)-1) AS DECIMAL(18,5)) = CAST(SUBSTRING(p2.pc_amt,1,CHARINDEX(';',p2.pc_amt)-1) AS DECIMAL(18,5)))))
		ORDER BY p1.pc_part,p1.pc_domain,p1.pc_list,p1.pc_start DESC 

END
