USE [BarCodeSys]
GO
/****** Object:  StoredProcedure [dbo].[sp_track_selectWoTrackHourly]    Script Date: 04/28/2015 10:25:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shanzm
-- Create date: 2015-04-13
-- Description:	工单每小时流速、及维修记录报表
-- =============================================
ALTER PROCEDURE [dbo].[sp_track_selectWoTrackHourly]
	
	@domain varchar(5)
	, @nbr varchar(15) = ''
	, @part varchar(15) = ''
	, @stdDate datetime
	, @endDate datetime
	, @line varchar(15) = ''
	, @typeID int = 97
AS
BEGIN
/*
	Exec sp_track_selectWoTrackHourly
		@domain = 'ZQL'
		, @nbr = '15Z000626'
		, @stdDate = '2015-4-7'
		, @endDate = '2015-4-29'
*/
	Declare @plantCode varchar(1)
		, @sql nvarchar(Max)
	Set @plantCode = Case @domain When 'szx' Then '1'
								When 'zql' Then '2'
								When 'yql' Then '5'
								When 'hql' Then '8'
					End
	Set @sql = N''				

	Declare @trk Table([trk_id] [int] IDENTITY(1,1) NOT NULL, trk_domain1 varchar(5), trk_nbr1 varchar(15), trk_lot1 varchar(15)
		, trk_domain varchar(5), trk_nbr varchar(15), trk_lot varchar(15)
		, trk_part varchar(15), trk_code nvarchar(50), trk_line varchar(15), wo_rel_date_act datetime
		, qty_ord int, qty_comp int, qty_total int, qty_scrp int, cnt_hour int, cnt_person int
		, trk_time varchar(20), trk_test nvarchar(3000), prc_item nvarchar(50), prc_det nvarchar(3000))

	Declare @trk_domain varchar(5)
		, @trk_nbr varchar(15)
		, @trk_lot varchar(15)
		, @trk_part varchar(15)
		, @trk_code nvarchar(50)
		, @trk_line varchar(15)
		, @wo_rel_date_act datetime
		, @qty_ord int
		, @qty_comp int
		, @qty_total int
		, @qty_scrp int
		, @cnt_hour int
		, @cnt_person int
		, @prcID int
		
		, @trkt_id int
		, @trkt_desc nvarchar(20)
		, @trkt_hours decimal(18, 2)
		, @trkd_qty_hour int
		, @trkd_scrp_test int
		
		, @trk_test nvarchar(3000)
		, @prc_det nvarchar(3000)

		, @prcItemID int
		, @prcdID int
		, @prcdName nvarchar(50)
		, @prcNum int
		
	Declare pp_cursor Cursor For
	
		--Select trk_domain, trk_nbr, trk_lot, trk_part, trk_code, trk_line
		--	, wo_rel_date_act, wo_qty_comp, wo_qty_ord
		--From track_mstr tr
		--Left Join Qad_Data.dbo.wo_mstr wo On wo.wo_nbr collate SQL_Latin1_General_CP1_CI_AS = tr.trk_nbr And wo.wo_lot collate SQL_Latin1_General_CP1_CI_AS = tr.trk_lot
		--Left Join tcpc0.dbo.wo_actRel ac On ac.wo_nbr collate SQL_Latin1_General_CP1_CI_AS = tr.trk_nbr
		--		And ac.wo_lot collate SQL_Latin1_General_CP1_CI_AS = tr.trk_lot
		--Where (
		--		(@domain = 'SZX' And wo.wo_site In ('1000', '2100', '4100')) Or
		--		(@domain = 'ZQL' And wo.wo_site In ('2000', '3000', '1200')) Or
		--		(@domain = 'YQL' And wo.wo_site In ('4000', '1400')) Or
		--		(@domain = 'HQL' And wo.wo_site In ('5000', '1500'))
		--	)
		--	And wo_rel_date_act >= @stdDate
		--	And wo_rel_date_act < @endDate
		--	And (@nbr = '' Or (@nbr <> '' And wo.wo_nbr = @nbr))
		--	And (@part = '' Or (@part <> '' And wo.wo_part = @part))
		--	And (@line = '' Or (@line <> '' And wo.wo_line = @line))
		
		---Update by WangLW at 2015-04-28调整为以订单下达为准
		Select isnull(trk_domain, ac.wo_domain) as trk_domain, isnull(trk_nbr,ac.wo_nbr) as trk_nbr, isnull(trk_lot,ac.wo_lot) as trk_lot
			, isnull(trk_part, ac.wo_part) as trk_part, trk_code, trk_line
			, wo_rel_date_act, wo_qty_comp, wo_qty_ord
		From tcpc0.dbo.wo_actRel ac
		Left Join  track_mstr tr On ac.wo_nbr collate SQL_Latin1_General_CP1_CI_AS = tr.trk_nbr
				And ac.wo_lot collate SQL_Latin1_General_CP1_CI_AS = tr.trk_lot		
		Left Join Qad_Data.dbo.wo_mstr wo On wo.wo_nbr collate SQL_Latin1_General_CP1_CI_AS = tr.trk_nbr And wo.wo_lot collate SQL_Latin1_General_CP1_CI_AS = tr.trk_lot

		Where ac.wo_domain = @domain
			And wo_rel_date_act >= @stdDate
			And wo_rel_date_act < @endDate
			And (@nbr = '' Or (@nbr <> '' And ac.wo_nbr = @nbr))
			And (@part = '' Or (@part <> '' And ac.wo_part = @part))
			And (@line = '' Or (@line <> '' And ac.wo_line = @line))		
		

	Open pp_cursor
	Fetch Next From pp_cursor Into @trk_domain, @trk_nbr, @trk_lot, @trk_part, @trk_code, @trk_line, @wo_rel_date_act, @qty_comp, @qty_ord
	While @@Fetch_Status=0
	Begin
	
		Set @qty_total = 0
		Set @qty_scrp = 0
		Set @cnt_hour = 0
		Set @cnt_person = 0
		
		Set @sql = N'	Select @cnt_person = Count(*)
						From
						(
							Select Distinct wo2_userID
							From tcpc' + @plantCode + '.dbo.wo2_workOrderEnter
							Where wo2_nbr = @trk_nbr
								And wo2_wID = @trk_lot
						) woe'
		Exec sp_executesql @sql, N'@trk_nbr varchar(20), @trk_lot varchar(20), @cnt_person int output'
			, @trk_nbr, @trk_lot, @cnt_person output
		
		Insert Into @trk(trk_domain1, trk_nbr1, trk_lot1, trk_domain, trk_nbr, trk_lot, trk_part, trk_code, trk_line, wo_rel_date_act, cnt_person, qty_comp, qty_ord)
		Values(@trk_domain, @trk_nbr, @trk_lot, @trk_domain, @trk_nbr, @trk_lot, @trk_part, @trk_code, @trk_line, @wo_rel_date_act, @cnt_person, @qty_comp, @qty_ord)

		--线长汇报的：每小时产线流速及不良数
		Declare tskd_cursor Cursor For
			Select trkt_id, trkt_desc, trkt_hours, Isnull(trkd_qty_hour, 0), Isnull(trkd_scrp_test, 0)
			From track_det det
			Left Join track_time t On t.trkt_id = det.trkd_timeID
			Where trkd_nbr = @trk_nbr
				And trkd_lot = @trk_lot
			Order By trkt_id
		Open tskd_cursor
		Fetch Next From tskd_cursor Into @trkt_id, @trkt_desc, @trkt_hours, @trkd_qty_hour, @trkd_scrp_test
		While @@Fetch_Status=0
		Begin
			
			Set @qty_total = @qty_total + @trkd_qty_hour
			Set @cnt_hour = @cnt_hour + @trkt_hours
			Set @qty_scrp = @qty_scrp + @trkd_scrp_test

			Set @trk_test = N''
			Set @trk_test = (
								Select td = ty.trkp_type + ':' + Cast(Isnull(det.trks_qty_scrp, '0') As varchar(18))
								From track_test_det det 
								Left Join track_type ty On ty.trkp_id = det.trks_reason And ty.trkp_group = 'Testing'
								Where trks_nbr = @trk_nbr
									And trks_lot = @trk_lot
									And trks_timeID = @trkt_id
								For Xml Raw('tr'), Elements
							)
			Set @trk_test = Replace(Replace(@trk_test, '</td></tr>', '\n'), '<tr><td>', '')
			SET @trkt_desc=@trkt_desc+'\n'+CAST(@trkd_qty_hour AS VARCHAR(100))
			If Exists(Select Top 1 * From @trk Where trk_nbr1 = @trk_nbr And trk_lot1 = @trk_lot And trk_time Is Null)
			Begin
				Update Top(1) @trk
				Set trk_time = @trkt_desc
					, trk_test = @trk_test
				Where trk_domain1 = @trk_domain
					And trk_nbr1 = @trk_nbr
					And trk_lot1 = @trk_lot
					And trk_time Is Null
			End
			Else
			Begin
				Insert Into @trk(trk_domain1, trk_nbr1, trk_lot1, trk_time, trk_test)
				Values(@trk_domain, @trk_nbr, @trk_lot, @trkt_desc, @trk_test)
			End
			
		Fetch Next From tskd_cursor Into @trkt_id, @trkt_desc, @trkt_hours, @trkd_qty_hour, @trkd_scrp_test
		End
		Close tskd_cursor
		Deallocate tskd_cursor

		--QC汇报的次品明细
		Set @prcID = 0
		Set @sql = N'Select @prcID = prcID From tcpc' + @plantCode + '.dbo.qc_process Where prcOrder = @trk_nbr And prcLine = @trk_lot'
		Exec sp_executesql @sql, N'@trk_nbr varchar(20), @trk_lot varchar(20), @prcID int output'
			, @trk_nbr, @trk_lot, @prcID output
				
		Create Table #qc_procedure(prcdID int, prcItemID int, prcdName nvarchar(50), prcNum int)
		Set @sql = N'	Insert Into #qc_procedure(prcdID, prcItemID, prcdName, prcNum)
						Select  pro.prcdID, prcItemID, prcdName, Isnull(prcNum, 0)
						From tcpc0.dbo.qc_procedure pro
						Left Join tcpc' + @plantCode + '.dbo.qc_processitem prc On prc.prcdID = pro.prcdID And prc.prcID = @prcID
						Where pro.typeID = @typeID
							And Isnull(prcNum, 0) > 0'
		Exec sp_executesql @sql, N'@prcID int, @typeID int'
			, @prcID, @typeID
		
		Create Table #qc_processdefect(pd_def_id int, pd_prcitem_id int, pd_num int)
		
		Declare qc_cursor Cursor For
			Select  prcdID, prcItemID, prcdName, Isnull(prcNum, 0)
			From #qc_procedure
		Open qc_cursor
		Fetch Next From qc_cursor Into @prcdID, @prcItemID, @prcdName, @prcNum
		While @@Fetch_Status=0
		Begin
			
			Delete From #qc_processdefect
			Set @sql = N'	Insert Into #qc_processdefect(pd_def_id, pd_prcitem_id, pd_num)
							Select pd_def_id, pd_prcitem_id, pd_num
							From tcpc' + @plantCode + '.dbo.qc_processdefect
							Where pd_prcitem_id = @prcItemID
								And  pd_num > 0'
			Exec sp_executesql @sql, N'@prcItemID int'
				, @prcItemID
				
			Set @qty_scrp = @qty_scrp + @prcNum

			Set @prc_det = N''
			Set @prc_det = (
								Select td = di.dItemName + ':' + Cast(Isnull(pd.pd_num, '0') As varchar(18))
								From tcpc0.dbo.qc_DefectItem di
								Left Join #qc_processdefect pd On di.dItemID = pd.pd_def_id AND pd_prcitem_id = @prcItemID
								Where pID = 2
									And defID = @prcdID
									And pd_num > 0
								Order By di.orderBy
								For Xml Raw('tr'), Elements
							)
			Set @prc_det = Replace(Replace(@prc_det, '</td></tr>', '\n'), '<tr><td>', '')
			If Exists(Select Top 1 * From @trk Where trk_domain1 = @trk_domain And trk_nbr1 = @trk_nbr And trk_lot1 = @trk_lot And prc_item Is Null)
			Begin
				Update Top(1) @trk
				Set prc_item = @prcdName
					, prc_det = @prc_det
				Where trk_domain1 = @trk_domain
					And trk_nbr1 = @trk_nbr
					And trk_lot1 = @trk_lot
					And prc_item Is Null
			End
			Else
			Begin
				Insert Into @trk(trk_domain1, trk_nbr1, trk_lot1, prc_item, prc_det)
				Values(@trk_domain, @trk_nbr, @trk_lot, @prcdName, @prc_det)
			End			

		Fetch Next From qc_cursor Into @prcdID, @prcItemID, @prcdName, @prcNum
		End
		Close qc_cursor
		Deallocate qc_cursor 
		
		--更新一些主要的信息
		Update @trk
		Set qty_total = @qty_total
			, cnt_hour = @cnt_hour
			, qty_scrp = @qty_scrp
		Where trk_domain = @trk_domain
			And trk_nbr = @trk_nbr
			And trk_lot = @trk_lot

		Drop Table #qc_procedure 
		Drop Table #qc_processdefect

	Fetch Next From pp_cursor Into @trk_domain, @trk_nbr, @trk_lot, @trk_part, @trk_code, @trk_line, @wo_rel_date_act, @qty_comp, @qty_ord
	End
	Close pp_cursor
	Deallocate pp_cursor 

	Select * From @trk

	
	--Select top 10 * From tcpc1.dbo.qc_process Where prcOrder = '15S00201-3' And prcLine = '39585885'
	

		
END
