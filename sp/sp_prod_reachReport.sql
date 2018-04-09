USE [RD_Workflow]
GO
/****** Object:  StoredProcedure [dbo].[sp_prod_reachReport]    Script Date: 5/20/2015 1:29:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	 mabo
-- Create date: 2015-03-12
-- Description:查看报表
-- =============================================
ALTER PROCEDURE [dbo].[sp_prod_reachReport] --select *  from prod_mstr
	@projectname nvarchar(50)
	,@prodname nvarchar(50)
	,@no nvarchar(50) = ''
	,@status int
	,@createdate1 datetime = ''
	,@plandate1 datetime = ''
	,@enddate1 datetime = ''
	,@createdate2 datetime = ''
	,@plandate2 datetime = ''
	,@enddate2 datetime = ''
	,@overdate datetime = ''
as
begin
	/*
		exec sp_prod_reachReport 'IT-0517-001','ITT4','',0,'2015-05-17','2015-05-28','',''
	*/
	if @status = 0
	begin
		select prod_Status = case when r.rdw_mstrid is not null
								then case when isnull(rw.RDW_Status,'') = 'CANCEL' then 3
										else case when rdc.RDW_ClosedDate is not null then 4
												else case when r.rdw_status = 2 then 2
														else case when Isnull(w.wo_status, '') = 'C' then 1 else 0 end
													 end
											 end 
									 end
								else case when Isnull(w.wo_status, '') = 'C' then 1 else 0 end
							 end
				,prod_id,prod_No,prod_ProjectName,prod_Code,prod_QAD,prod_PCB,prod_CreateBy
				,prod_CreateByName
				,isnull(Convert(varchar(10),prod_CreateByDate,120),'') as prod_CreateByDate
				,prod_ReviewOpinion
				,prod_CreateFName,prod_CreateFPath
				,isnull(Convert(varchar(10),prod_PlanDate,120),'') as prod_PlanDate
				,isnull(Convert(varchar(10),prod_EndDate,120),'') as prod_EndDate
				,prod_mid
				,prod_did
				,wo_part,wo_status
				,isnull(Convert(varchar(10),wo_rel_date,120),'') as wo_rel_date
				,wo_qty_ord,wo_qty_comp
				,isnull(Convert(varchar(10),wo_close_date,120),'') as wo_close_date
				,wo_nbr,wo_domain
		from prod_mstr p
		left join(
			Select wo_domain
				, wo_nbr = Case When wo_site = 'S120' Then wo_nbr + 'Z' Else wo_nbr End
				, wo_part, wo_status, wo_rel_date, wo_close_date
				, wo_qty_ord = Cast(wo_qty_ord as Float)
				, wo_qty_comp = Cast(wo_qty_comp as Float)
			From QAD_Data.dbo.wo_mstr
			Where wo_site In ('S120', 'S200')
		) w on (p.prod_no + 'Z') = w.wo_nbr
		left join RDW_Det r on r.rdw_mstrid = p.prod_mid and r.rdw_DetID = p.prod_did
		left join RDW_Mstr rw on rw.RDW_MstrID = p.prod_mid
		left join RDW_Det_Closed rdc on rdc.rdw_mstrid = p.prod_mid and rdc.rdw_DetID = p.prod_did
		where (@projectname = '' or (@projectname <> '' and prod_ProjectName like replace(@projectname, '*', '%')))
			and (@prodname = '' or (@prodname <> '' and prod_Code like replace(@prodname, '*', '%')))
			and (@no = '' or (@no <> '' and prod_No like replace(@no, '*', '%')))
			and (@createdate1 = '' or (@createdate1 <> '' and Convert(varchar(10), prod_CreateByDate, 120) >= @createdate1))			
			and (@createdate2 = '' or (@createdate2 <> '' and Convert(varchar(10), prod_CreateByDate, 120) <= @createdate2))
			and (@plandate1 = '' or (@plandate1 <> '' and Convert(varchar(10), prod_PlanDate, 120) >= @plandate1))
			and (@plandate2 = '' or (@plandate2 <> '' and Convert(varchar(10), prod_PlanDate, 120) <= @plandate2))
			and (@enddate1 = '' or (@enddate1 <> '' and Convert(varchar(10), prod_EndDate, 120) >= @enddate1))
			and (@enddate2 = '' or (@enddate2 <> '' and Convert(varchar(10), prod_EndDate, 120) <= @enddate2))
	end
	if @status = 1
	begin
		select prod_Status = 0
				,prod_id,prod_No,prod_ProjectName,prod_Code,prod_QAD,prod_PCB,prod_CreateBy
				,prod_CreateByName
				,isnull(Convert(varchar(10),prod_CreateByDate,120),'') as prod_CreateByDate,prod_ReviewOpinion
				,prod_CreateFName,prod_CreateFPath
				,isnull(Convert(varchar(10),prod_PlanDate,120),'') as prod_PlanDate
				,isnull(Convert(varchar(10),prod_EndDate,120),'') as prod_EndDate
				,prod_mid
				,prod_did
				,wo_part,wo_status
				,isnull(Convert(varchar(10),wo_rel_date,120),'') as wo_rel_date
				,wo_qty_ord,wo_qty_comp
				,isnull(Convert(varchar(10),wo_close_date,120),'') as wo_close_date
				,wo_nbr,wo_domain
		from prod_mstr p 
		left join(
			Select wo_domain
				, wo_nbr = Case When wo_site = 'S120' Then wo_nbr + 'Z' Else wo_nbr End
				, wo_part, wo_status, wo_rel_date, wo_close_date
				, wo_qty_ord = Cast(wo_qty_ord as Float)
				, wo_qty_comp = Cast(wo_qty_comp as Float)
			From QAD_Data.dbo.wo_mstr
			Where wo_site In ('S120', 'S200')
		) w on (p.prod_no + 'Z') = w.wo_nbr
		left join RDW_Det r on r.rdw_mstrid = p.prod_mid and r.rdw_DetID = p.prod_did and r.rdw_status = 2
		where (@projectname = '' or (@projectname <> '' and prod_ProjectName like replace(@projectname, '*', '%')))
			and (@prodname = '' or (@prodname <> '' and prod_Code like replace(@prodname, '*', '%')))
			and (@no = '' or (@no <> '' and prod_No like replace(@no, '*', '%')))
			and (@createdate1 = '' or (@createdate1 <> '' and Convert(varchar(10), prod_CreateByDate, 120) >= @createdate1))			
			and (@createdate2 = '' or (@createdate2 <> '' and Convert(varchar(10), prod_CreateByDate, 120) <= @createdate2))
			and (@plandate1 = '' or (@plandate1 <> '' and Convert(varchar(10), prod_PlanDate, 120) >= @plandate1))
			and (@plandate2 = '' or (@plandate2 <> '' and Convert(varchar(10), prod_PlanDate, 120) <= @plandate2))
			and (@enddate1 = '' or (@enddate1 <> '' and Convert(varchar(10), prod_EndDate, 120) >= @enddate1))
			and (@enddate2 = '' or (@enddate2 <> '' and Convert(varchar(10), prod_EndDate, 120) <= @enddate2))
			and r.rdw_mstrid Is Null
			and Isnull(w.wo_status, '') <> 'C'
	end
	if @status = 2
	begin
		select prod_Status = 1
				,prod_id,prod_No,prod_ProjectName,prod_Code,prod_QAD,prod_PCB,prod_CreateBy
				,prod_CreateByName
				,isnull(Convert(varchar(10),prod_CreateByDate,120),'') as prod_CreateByDate,prod_ReviewOpinion
				,prod_CreateFName,prod_CreateFPath
				,isnull(Convert(varchar(10),prod_PlanDate,120),'') as prod_PlanDate
				,isnull(Convert(varchar(10),prod_EndDate,120),'') as prod_EndDate
				,prod_mid
				,prod_did
				,wo_part,wo_status
				,isnull(Convert(varchar(10),wo_rel_date,120),'') as wo_rel_date
				,wo_qty_ord = Cast(wo_qty_ord as float)
				,wo_qty_comp = Cast(wo_qty_comp as float)
				,isnull(Convert(varchar(10),wo_close_date,120),'') as wo_close_date
				,wo_nbr,wo_domain
		from prod_mstr p 
		left join(
			Select wo_domain
				, wo_nbr = Case When wo_site = 'S120' Then wo_nbr + 'Z' Else wo_nbr End
				, wo_part, wo_status, wo_rel_date, wo_close_date
				, wo_qty_ord = Cast(wo_qty_ord as Float)
				, wo_qty_comp = Cast(wo_qty_comp as Float)
			From QAD_Data.dbo.wo_mstr
			Where wo_site In ('S120', 'S200')
		) w on (p.prod_no + 'Z') = w.wo_nbr
		left join RDW_Det r on r.rdw_mstrid = p.prod_mid and r.rdw_DetID = p.prod_did and r.rdw_status = 2
		where (@projectname = '' or (@projectname <> '' and prod_ProjectName like replace(@projectname, '*', '%')))
			and (@prodname = '' or (@prodname <> '' and prod_Code like replace(@prodname, '*', '%')))
			and (@no = '' or (@no <> '' and prod_No like replace(@no, '*', '%')))
			and (@createdate1 = '' or (@createdate1 <> '' and Convert(varchar(10), prod_CreateByDate, 120) >= @createdate1))			
			and (@createdate2 = '' or (@createdate2 <> '' and Convert(varchar(10), prod_CreateByDate, 120) <= @createdate2))
			and (@plandate1 = '' or (@plandate1 <> '' and Convert(varchar(10), prod_PlanDate, 120) >= @plandate1))
			and (@plandate2 = '' or (@plandate2 <> '' and Convert(varchar(10), prod_PlanDate, 120) <= @plandate2))
			and (@enddate1 = '' or (@enddate1 <> '' and Convert(varchar(10), prod_EndDate, 120) >= @enddate1))
			and (@enddate2 = '' or (@enddate2 <> '' and Convert(varchar(10), prod_EndDate, 120) <= @enddate2))
			and Isnull(w.wo_status, '') = 'C'
	end
	if @status = 3
	begin
		select prod_Status = 2
				,prod_id,prod_No,prod_ProjectName,prod_Code,prod_QAD,prod_PCB,prod_CreateBy
				,prod_CreateByName
				,isnull(Convert(varchar(10),prod_CreateByDate,120),'') as prod_CreateByDate,prod_ReviewOpinion
				,prod_CreateFName,prod_CreateFPath
				,isnull(Convert(varchar(10),prod_PlanDate,120),'') as prod_PlanDate
				,isnull(Convert(varchar(10),prod_EndDate,120),'') as prod_EndDate
				,prod_mid
				,prod_did
				,wo_part,wo_status
				,isnull(Convert(varchar(10),wo_rel_date,120),'') as wo_rel_date
				,wo_qty_ord,wo_qty_comp
				,isnull(Convert(varchar(10),wo_close_date,120),'') as wo_close_date
				,wo_nbr,wo_domain
		from prod_mstr p 
		left join(
			Select wo_domain
				, wo_nbr = Case When wo_site = 'S120' Then wo_nbr + 'Z' Else wo_nbr End
				, wo_part, wo_status, wo_rel_date, wo_close_date
				, wo_qty_ord = Cast(wo_qty_ord as Float)
				, wo_qty_comp = Cast(wo_qty_comp as Float)
			From QAD_Data.dbo.wo_mstr
			Where wo_site In ('S120', 'S200')
		) w on (p.prod_no + 'Z') = w.wo_nbr
		left join RDW_Det r on r.rdw_mstrid = p.prod_mid and r.rdw_DetID = p.prod_did and r.rdw_status = 2
		where (@projectname = '' or (@projectname <> '' and prod_ProjectName like replace(@projectname, '*', '%')))
			and (@prodname = '' or (@prodname <> '' and prod_Code like replace(@prodname, '*', '%')))
			and (@no = '' or (@no <> '' and prod_No like replace(@no, '*', '%')))
			and (@createdate1 = '' or (@createdate1 <> '' and Convert(varchar(10), prod_CreateByDate, 120) >= @createdate1))			
			and (@createdate2 = '' or (@createdate2 <> '' and Convert(varchar(10), prod_CreateByDate, 120) <= @createdate2))
			and (@plandate1 = '' or (@plandate1 <> '' and Convert(varchar(10), prod_PlanDate, 120) >= @plandate1))
			and (@plandate2 = '' or (@plandate2 <> '' and Convert(varchar(10), prod_PlanDate, 120) <= @plandate2))
			and (@enddate1 = '' or (@enddate1 <> '' and Convert(varchar(10), prod_EndDate, 120) >= @enddate1))
			and (@enddate2 = '' or (@enddate2 <> '' and Convert(varchar(10), prod_EndDate, 120) <= @enddate2))
			and r.rdw_mstrid Is Not Null
	end
	--项目取消
	if @status = 4
	begin
		select prod_Status = 3
				,prod_id,prod_No,prod_ProjectName,prod_Code,prod_QAD,prod_PCB,prod_CreateBy
				,prod_CreateByName
				,isnull(Convert(varchar(10),prod_CreateByDate,120),'') as prod_CreateByDate,prod_ReviewOpinion
				,prod_CreateFName,prod_CreateFPath
				,isnull(Convert(varchar(10),prod_PlanDate,120),'') as prod_PlanDate
				,isnull(Convert(varchar(10),prod_EndDate,120),'') as prod_EndDate
				,prod_mid
				,prod_did
				,wo_part,wo_status
				,isnull(Convert(varchar(10),wo_rel_date,120),'') as wo_rel_date
				,wo_qty_ord,wo_qty_comp
				,isnull(Convert(varchar(10),wo_close_date,120),'') as wo_close_date
				,wo_nbr,wo_domain
		from prod_mstr p 
		left join(
			Select wo_domain
				, wo_nbr = Case When wo_site = 'S120' Then wo_nbr + 'Z' Else wo_nbr End
				, wo_part, wo_status, wo_rel_date, wo_close_date
				, wo_qty_ord = Cast(wo_qty_ord as Float)
				, wo_qty_comp = Cast(wo_qty_comp as Float)
			From QAD_Data.dbo.wo_mstr
			Where wo_site In ('S120', 'S200')
		) w on (p.prod_no + 'Z') = w.wo_nbr
		left join RDW_Det r on r.rdw_mstrid = p.prod_mid and r.rdw_DetID = p.prod_did and r.rdw_status = 2
		left join RDW_Mstr rw on rw.RDW_MstrID = p.prod_mid
		where (@projectname = '' or (@projectname <> '' and prod_ProjectName like replace(@projectname, '*', '%')))
			and (@prodname = '' or (@prodname <> '' and prod_Code like replace(@prodname, '*', '%')))
			and (@no = '' or (@no <> '' and prod_No like replace(@no, '*', '%')))
			and (@createdate1 = '' or (@createdate1 <> '' and Convert(varchar(10), prod_CreateByDate, 120) >= @createdate1))			
			and (@createdate2 = '' or (@createdate2 <> '' and Convert(varchar(10), prod_CreateByDate, 120) <= @createdate2))
			and (@plandate1 = '' or (@plandate1 <> '' and Convert(varchar(10), prod_PlanDate, 120) >= @plandate1))
			and (@plandate2 = '' or (@plandate2 <> '' and Convert(varchar(10), prod_PlanDate, 120) <= @plandate2))
			and (@enddate1 = '' or (@enddate1 <> '' and Convert(varchar(10), prod_EndDate, 120) >= @enddate1))
			and (@enddate2 = '' or (@enddate2 <> '' and Convert(varchar(10), prod_EndDate, 120) <= @enddate2))
			and isnull(rw.RDW_Status,'') = 'CANCEL'
	end
	--项目关闭
	if @status = 5
	begin
		select prod_Status = 4
				,prod_id,prod_No,prod_ProjectName,prod_Code,prod_QAD,prod_PCB,prod_CreateBy
				,prod_CreateByName
				,isnull(Convert(varchar(10),prod_CreateByDate,120),'') as prod_CreateByDate,prod_ReviewOpinion
				,prod_CreateFName,prod_CreateFPath
				,isnull(Convert(varchar(10),prod_PlanDate,120),'') as prod_PlanDate
				,isnull(Convert(varchar(10),prod_EndDate,120),'') as prod_EndDate
				,prod_mid
				,prod_did
				,wo_part,wo_status
				,isnull(Convert(varchar(10),wo_rel_date,120),'') as wo_rel_date
				,wo_qty_ord,wo_qty_comp
				,isnull(Convert(varchar(10),wo_close_date,120),'') as wo_close_date
				,wo_nbr,wo_domain
		from prod_mstr p 
		left join(
			Select wo_domain
				, wo_nbr = Case When wo_site = 'S120' Then wo_nbr + 'Z' Else wo_nbr End
				, wo_part, wo_status, wo_rel_date, wo_close_date
				, wo_qty_ord = Cast(wo_qty_ord as Float)
				, wo_qty_comp = Cast(wo_qty_comp as Float)
			From QAD_Data.dbo.wo_mstr
			Where wo_site In ('S120', 'S200')
		) w on (p.prod_no + 'Z') = w.wo_nbr
		left join RDW_Det r on r.rdw_mstrid = p.prod_mid and r.rdw_DetID = p.prod_did and r.rdw_status = 2
		left join RDW_Det_Closed rdc on rdc.rdw_mstrid = p.prod_mid and rdc.rdw_DetID = p.prod_did
		where (@projectname = '' or (@projectname <> '' and prod_ProjectName like replace(@projectname, '*', '%')))
			and (@prodname = '' or (@prodname <> '' and prod_Code like replace(@prodname, '*', '%')))
			and (@no = '' or (@no <> '' and prod_No like replace(@no, '*', '%')))
			and (@createdate1 = '' or (@createdate1 <> '' and Convert(varchar(10), prod_CreateByDate, 120) >= @createdate1))			
			and (@createdate2 = '' or (@createdate2 <> '' and Convert(varchar(10), prod_CreateByDate, 120) <= @createdate2))
			and (@plandate1 = '' or (@plandate1 <> '' and Convert(varchar(10), prod_PlanDate, 120) >= @plandate1))
			and (@plandate2 = '' or (@plandate2 <> '' and Convert(varchar(10), prod_PlanDate, 120) <= @plandate2))
			and (@enddate1 = '' or (@enddate1 <> '' and Convert(varchar(10), prod_EndDate, 120) >= @enddate1))
			and (@enddate2 = '' or (@enddate2 <> '' and Convert(varchar(10), prod_EndDate, 120) <= @enddate2))
			and rdc.RDW_ClosedDate is not null
	end
end