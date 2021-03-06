USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_wo_selectWoActRel]    Script Date: 04/23/2015 08:36:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Young Yang
-- Create date: 2014-8-1
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_wo_selectWoActRel]
	@woNbr VARCHAR(18)
   ,@part VARCHAR(18)
   ,@relDateFrom VARCHAR(10)
   ,@relDateTo VARCHAR(10)
   ,@actDateFrom VARCHAR(10)
   ,@actDateTo VARCHAR(10)
   ,@domain VARCHAR(8)
AS
BEGIN
	SELECT  wo.wo_id , 
	        w.wo_flr_cc,       
	        u.userName AS wo_createdName,
	        wo.wo_nbr ,
	        wo.wo_lot ,
	        wo.wo_part ,
	        wo.wo_rel_date ,
            CAST(bo.wo_plandate AS DATETIME) AS wo_plandate,
	        wo.wo_rel_date_act ,
	        wt.wo_online,
	        w.wo_site,
	        wo.wo_line,
	        wo_ctr,
	        wo.wo_domain, 
	        wo.wo_zhouqizhang
	FROM dbo.wo_actRel wo
	LEFT JOIN dbo.Users u
	ON wo.wo_createdby=u.userID
	LEFT JOIN QAD_Data.dbo.wo_mstr w
	ON wo.wo_nbr=w.wo_nbr AND wo.wo_lot=w.wo_lot
	LEFT JOIN dbo.bigOrder bo
	ON wo.wo_nbr=bo.wo_nbr AND wo.wo_lot=bo.wo_lot
	LEFT JOIN 
	(
		SELECT wo_nbr,wo_lot,wo_online,wo_offline,wo_onlineReporterName,wo_offlineReporterName FROM tcpc1.dbo.wo_track
		UNION 
		SELECT wo_nbr,wo_lot,wo_online,wo_offline,wo_onlineReporterName,wo_offlineReporterName FROM tcpc2.dbo.wo_track
		UNION 
		SELECT wo_nbr,wo_lot,wo_online,wo_offline,wo_onlineReporterName,wo_offlineReporterName FROM tcpc5.dbo.wo_track
		UNION 
		SELECT wo_nbr,wo_lot,wo_online,wo_offline,wo_onlineReporterName,wo_offlineReporterName FROM tcpc8.dbo.wo_track
	) wt
	ON wo.wo_lot=wt.wo_lot AND wo.wo_nbr = wt.wo_nbr
	WHERE (@woNbr='' OR (@woNbr<>'' AND wo.wo_nbr LIKE '%'+@woNbr+'%'))
	AND (@part='' OR (@part<>'' AND wo.wo_part=@part))
	AND (@relDateFrom='' OR (@relDateFrom<>'' AND wo.wo_rel_date >=@relDateFrom))
	AND (@relDateTo='' OR (@relDateTo<>'' AND wo.wo_rel_date < DATEADD(DAY,1,@relDateTo)))
	AND (@actDateFrom='' OR (@actDateFrom<>'' AND wo.wo_rel_date_act >=@actDateFrom))
	AND (@relDateFrom='' OR (@actDateTo<>'' AND wo.wo_rel_date_act < DATEADD(DAY,1,@actDateTo)))
	AND (@domain='--' OR (@domain<>'--' AND wo.wo_domain=@domain))	
	ORDER BY wo.wo_rel_date_act,wo.wo_createdby
END
