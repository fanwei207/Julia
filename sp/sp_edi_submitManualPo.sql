USE [EDI_DB]
GO
/****** Object:  StoredProcedure [dbo].[sp_edi_submitManualPo]    Script Date: 2015/06/23 Tuesday 1:34:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_edi_submitManualPo]
	
	@uID bigint
	, @uName nvarchar(10)
	, @idList varchar(Max)
	, @retValue bit Output
AS
BEGIN

/*
	Declare @retValue bit
	Exec sp_edi_submitManualPo 13, N'管理员', '3337;', @retValue Output
	select @retValue 'retValue'
*/
	Declare @error int
		, @cust varchar(10)
		, @nbr nvarchar(20)
		, @isSubmit bit--是否已提交，否则是追加行
		, @isAppend bit--追加行
		, @id bigint--hrd_id
		, @hrd_id bigint--hrd_id
		, @Flow_Id uniqueidentifier
		, @Node_Id uniqueidentifier
		, @sId uniqueidentifier
	
	Set @Flow_Id = '8c930bdd-4c65-45e6-a9d7-3edd873005a3'
	Set @Node_Id = 'd46930cc-12f1-4acb-a1a7-7ff6d83378f6'
	
	Set @error = 0
	Set @cust = ''
	Set @nbr = N''
	Set @isSubmit = 0
	Set @isAppend = 0
	Set @id = 0
	Set @hrd_id = 0

	Begin Transaction edi_submitManualPo
	
		Declare pp_cursor Cursor For
			Select Distinct mpo_id, mpo_cust, mpo_nbr
				, Case When mpo_submittedDate Is Not Null Then 1 Else 0 End
			From dbo.SplitExt(@idList, ';') sp
			Left Join ManualPoHrd mhrd On mhrd.mpo_id = sp.String
			Left Join ManualPoDet mdet On mdet.mpod_hrd_id = mhrd.mpo_id
			Where Not Exists(
								Select * 
								From EdiPoHrd hrd
								Left Join EdiPoDet det On det.hrd_id = hrd.id
								Where (poNbr = mhrd.mpo_nbr Or fob = mhrd.mpo_nbr)
									And poLine = mdet.mpod_line
							)			
		Open pp_cursor
		Fetch Next From pp_cursor Into @id, @cust, @nbr, @isSubmit
		While @@Fetch_Status = 0
		Begin
		
			Set @hrd_id = 0
			Select @hrd_id = id From EdiPoHrd Where cusCode = @cust And poNbr = @nbr
			--如果之前有提交过，否则，先插入HRD
			--dueDate格式：YYMMDD
			--非HomeD（C0000004）不用fob
			If(Isnull(@hrd_id, 0) = 0)
			Begin
				Insert Into EdiPoHrd(cntCode, shipto, poNbr, fob, dueDate, shipVia, cusCode, PoRecDate, isManualed, CustName, fileType, rmk, mpo_domain, mpo_channel, mpo_rmks)
				Select 'HRD', mpo_shipto, mpo_nbr, Case When Not Exists(Select Top 1 * From CustTrans Where orignalCust = mpo_cust) Then '' Else mpo_nbr End
					, Replace(Right(Convert(varchar(10), mpo_due_date, 111), 8), '/', '')
					, mpo_shipvia, mpo_cust, GetDate(), 1
					, (Select Top 1 ad_name From QAD_Data.dbo.ad_mstr Where (ad_domain = 'SZX' Or ad_domain = 'TCB' Or ad_domain = 'ZQL' Or ad_domain = 'YQL' Or ad_domain = 'HQL') And (ad_type = 'ship-to' Or ad_type = 'customer') And ad_addr = mpo_shipto)
					, '999'
					, (Select Top 1 ad_name From QAD_Data.dbo.ad_mstr Where (ad_domain = 'SZX' Or ad_domain = 'TCB' Or ad_domain = 'ZQL' Or ad_domain = 'YQL' Or ad_domain = 'HQL') And (ad_type = 'ship-to' Or ad_type = 'customer') And ad_addr = mpo_shipto)
					, mpo_domain
					, mpo_channel
					, mpo_rmks
				From ManualPoHrd
				Where mpo_id = @id
				
				Set @hrd_id = @@IDENTITY
			End
			
			--@hrd_id正常获取的情况下
			If(Isnull(@hrd_id, 0) > 0)
			Begin
				Update ManualPoHrd
				Set mpo_submittedBy = @uID
					, mpo_submittedName = @uName
					, mpo_submittedDate = GetDate()
				Where mpo_id = @id
				If(@@Error <> 0) Set @error = -1
			
				Insert Into EdiPoDet(cntCode_det, hrd_id, poLine, partNbr, qadPart, ordQty, um, price
										, reqDate, dueDate, qad_dueDate, qad_perfDate, errMsg, det_rmks, mpod_rmks,sku,xPartNbr)
				Select 'DET', @hrd_id, mpod_line, mpod_cust_part,CASE WHEN hrd.mpo_domain='TCB' THEN det.mpod_qad ELSE NULL END, mpod_ord_qty, mpod_um, mpod_price
						, Replace(Right(Convert(varchar(10), mpod_req_date, 111), 8), '/', '')
						, Replace(Right(Convert(varchar(10), mpod_due_date, 111), 8), '/', '')
						, Replace(Right(Convert(varchar(10), mpod_req_date, 111), 8), '/', '')
						, Replace(Right(Convert(varchar(10), mpod_due_date, 111), 8), '/', '')
						, Case When Isnull(mpod_qad, N'') = N'' Then N'客户物料不存在或者事务限定ADD-SO'
							Else Case When isnull(hrd.mpo_domain,'SZX') = 'TCB' Then N'' Else Case When (Select Count(*) From QAD_Data..pt_mstr Where (pt_domain = 'SZX' Or pt_domain = 'ZQZ') And pt_part = mpod_qad And pt_status In ('NOCOST_A', 'NOCOST_N', 'STOP')) > 0 Then N'客户物料不存在或者事务限定ADD-SO' Else N'' End End
						  End
						, mpod_rmks
						, mpod_rmks
						, mpod_SKU
						, case when hrd.mpo_shipto = 'C0000035' and LEN(mpod_SKU) = 6 then SUBSTRING(mpod_SKU,1,3) + '-'+SUBSTRING(mpod_SKU,4,3) + '-' + mpod_cust_part
						 when isnull(mpod_SKU,'') = '' then null
						else mpod_SKU+ '-'+ mpod_cust_part end
				From ManualPoDet det
				Join ManualPoHrd hrd On hrd.mpo_id = det.mpod_hrd_id
				Where mpod_hrd_id = @id
					And mpod_submittedBy Is Null
					And Isnull(mpod_isAppended, 0) = @isSubmit
				If(@@Error <> 0) Set @error = -2
			
				--Add by Wangdl 2014-05-09
				--如果是TCB订单，则直接在手工订单提交的时候分配EDI订单域为TCB，地点为8000
				Declare @domian varchar(4)
				Select @domian = isnull(mpo_domain,'SZX')
				From EdiPoHrd
				Where id = @hrd_id
				If @domian = 'TCB'
				Begin
					Update EdiPoHrd
					Set  domain = 'TCB'
						,domain_date = GETDATE()
					Where id = @hrd_id
					If(@@Error <> 0) Set @error = -4
					
					Update EdiPoDet
					Set  domain = 'TCB'
						,site = '8000'
						,site_date = GETDATE()
					Where hrd_id = @hrd_id
					If(@@Error <> 0) Set @error = -5
				End
			
			
				--启动审批
				If @domian <> 'TCB'
				Begin
					
					Insert Into EDIPoApprove(id, poNbr, hrdid, fob, shipto, shipVia, cusCode, CustName, soNbr, PoRecDate, loadDate
							, domain, domain_date, isShipped
							, partNbr, sku, ordQty, price, poLine, dueDate, reqDate, qadPart,totalPrice,atlPrice,tcpPrice,custPrice,det_PoRecDate)
					Select NEWID(), poNbr, hrd_id, fob, shipto, shipVia, cusCode, CustName, soNbr, PoRecDate, det.loadDate
						, det.domain, domain_date, isShipped
						, det.partNbr, det.sku, cast(cast(det.ordQty as decimal) as int), det.price, det.poLine, det.dueDate, det.reqDate, det.qadPart,CAST(CAST(ordQty AS FLOAT)*CAST(price AS FLOAT) AS VARCHAR(50))
						, (SELECT TOP 1 Pi_price1 FROM tcpc0.dbo.Pi_PriceList WHERE Pi_Cust=hrd.cusCode AND Pi_QAD=det.qadPart
				           AND ISNULL(Pi_StartDate,'1900-1-1')<=hrd.PoRecDate AND DATEADD(day,1,ISNULL(Pi_EndDate,'2079-1-1'))>hrd.PoRecDate
				           ORDER BY Pi_price1)
				        , (SELECT TOP 1 Pi_price2 FROM tcpc0.dbo.Pi_PriceList WHERE Pi_Cust=hrd.cusCode AND Pi_QAD=det.qadPart
				           AND ISNULL(Pi_StartDate,'1900-1-1')<=hrd.PoRecDate AND DATEADD(day,1,ISNULL(Pi_EndDate,'2079-1-1'))>hrd.PoRecDate
				           ORDER BY Pi_price1)
						, (SELECT TOP 1 Pi_price3 FROM tcpc0.dbo.Pi_PriceList WHERE Pi_Cust=hrd.cusCode AND Pi_QAD=det.qadPart
				           AND ISNULL(Pi_StartDate,'1900-1-1')<=hrd.PoRecDate AND DATEADD(day,1,ISNULL(Pi_EndDate,'2079-1-1'))>hrd.PoRecDate
				           ORDER BY Pi_price1)
						, det.det_PoRecDate
						
					From EdiPoHrd hrd
					Left Join EdiPoDet det On det.hrd_id = hrd.id
					Where Not Exists(Select * From EDIPoApprove Where poNbr = hrd.poNbr And poLine = det.poLine)
						And hrd.id = @hrd_id
						
					Declare pd_cursor Cursor For
						Select id
						From EDIPoApprove
						WHERE hrdid = @hrd_id
							And Exists(Select * From ManualPoDet Where mpod_hrd_id = @id And Isnull(mpod_isAppended, 0) = @isSubmit And poLine = mpod_line)
					Open pd_cursor
					Fetch Next From pd_cursor Into @sId
					While @@Fetch_Status=0
					Begin
					
						Exec WorkFlow.dbo.sp_nwf_startWorkflow
							@FlowId = @Flow_Id
							, @SourceId = @sId
							, @UserId = 13

						Fetch Next From pd_cursor Into @sId
					END
					Close pd_cursor
					Deallocate pd_cursor	
				END 
			
			    Update ManualPoDet
				Set mpod_submittedBy = @uID
					, mpod_submittedName = @uName
					, mpod_submittedDate = GetDate()
					, mpod_isAppended = NULL
					, mpod_isAppSubmit = 1
				Where mpod_hrd_id = @id
					And Isnull(mpod_isAppended, 0) = @isSubmit
				If(@@Error <> 0) Set @error = -3
			End
				
			
		Fetch Next From pp_cursor Into @id, @cust, @nbr, @isSubmit
		End
		Close pp_cursor
		Deallocate pp_cursor
	
		--Add By Shan Zhiming 2013-12-24
		--产品文档数不齐全的，要提示。这里就全部更新了
		Update EdiPoDet
		Set errMsg = N''
		From qaddoc.dbo.ProductTrackingNew
		Where product = qadPart
			And cnt_need > cnt_fact
			And cnt_need > 0
			And errMsg Like N'该产品的文档不齐全%'
	
	If(@@Error <> 0 Or @error < 0)
	Begin
		Set @retValue = 0
		Rollback Transaction edi_submitManualPo
	End
	Else
	Begin
		Set @retValue = 1
		Commit Transaction edi_submitManualPo
	End
	
END

