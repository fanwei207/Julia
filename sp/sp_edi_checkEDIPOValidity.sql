USE [EDI_DB]
GO
/****** Object:  StoredProcedure [dbo].[sp_edi_checkEDIPOValidity]    Script Date: 07/30/2015 14:19:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shanzm
-- Create date: 2012-12-17
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_edi_checkEDIPOValidity]
	
	@uID bigint
	, @bPlanDate bit = 0
	, @bDueDate bit = 0
	, @bUnitPrice bit = 0
	, @bSite bit = 0
	, @bCust bit = 0
	, @bPart bit = 0
	, @bCustPart bit = 0
	, @bSoNbr bit = 0
	, @bQty bit = 0
	, @retValue bit Output
AS
BEGIN
/*
	Declare @retValue  bit
	Exec sp_edi_checkEDIPOValidity 13, 1, 1, 1, 1, @retValue Output
	select @retValue '@retValue'
*/
	Create Table #edi_po(poNbr varchar(20) Collate SQL_Latin1_General_CP1_CI_AS, poLine varchar(4) Collate SQL_Latin1_General_CP1_CI_AS, reqDate datetime, planDate datetime)
	--两个月内接到的订单，且是尚未导入QAD的
	Insert Into #edi_po
	Select hrd.poNbr, det.poLine, det.reqDate, det.planDate
	From EdiPoHrd hrd
	Inner Join EdiPoDet det On det.hrd_id = hrd.id
	Where PoRecDate >= DateAdd(m, -12, GetDate())
		--And Isnull(finished, 0) = 0 --陈凯锋也需要修改已经完成的
		
		
	--更新时订单行要存在
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';订单行不存在', '') + N';订单行不存在'
	From ediTemp t
	Left Join #edi_po e On e.poNbr = t.et_tcp_po And Cast(e.poLine As int) = Cast(t.et_line As int)
	Where e.poLine Is Null
		And t.et_createdBy = @uID
			
	--计划日期 不能为空、格式要正确
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';计划日期格式不对', '') + N';计划日期格式不对'
	From #edi_po edi
	Where edi.poNbr = et_tcp_po
		And edi.poLine = et_line
		And (Isdate(et_planDate) = 0)
		And et_createdBy = @uID
		And @bPlanDate = 1
	
	
	--截止日期 不能为空、格式要正确、不能晚于需求日期
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';截止日期格式不对', '') + N';截止日期格式不对'
	From #edi_po edi
	Where edi.poNbr = et_tcp_po
		And edi.poLine = et_line
		And (Isdate(et_due_date) = 0)
		And (LEN(et_planDate) <> 0)
		And et_createdBy = @uID
		And @bDueDate = 1
		
	--制地
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';制地不正确', '') + N';制地不正确'
	Where (et_site <> '1000'
			And et_site <> '1200' And et_site <> '1400' And et_site <> '1500'
			And et_site <> '2000' And et_site <> '2100' And et_site <> '2400'
			And et_site <> '3000' And et_site <> '3200' And et_site <> '3400'
			And et_site <> '4000' And et_site <> '4100'
			And et_site <> '5000'
		  )
		And et_createdBy = @uID
		And @bSite = 1
		
	--备注：是数字
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';备注不是数字', '') + N';备注不是数字'
	Where Isnumeric(et_rmks) = 0
		And et_createdBy = @uID
		And @bUnitPrice = 1
	
	--采购单价：是数字 = 备注 * 0.91
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';采购单价不是数字', '') + N';采购单价不是数字'
	Where Isnumeric(et_price) = 0
		And et_createdBy = @uID
		And @bUnitPrice = 1
	
	--客户
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';客户代码不存在', '') + N';客户代码不存在'
	Where et_createdBy = @uID
		And @bCust = 1	
		And et_cust not in (  Select distinct ad_addr 
							  From QAD_DATA.dbo.ad_mstr
							  Where ad_domain In ('ATL', 'SZX', 'ZQL', 'YQL', 'HQL')
								 And ad_type = 'customer'
							)

	
	--QAD号
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';QAD不存在', '') + N';QAD不存在'
	Where et_createdBy = @uID
		And @bPart = 1	
		And et_qad not in (  Select distinct pt_part 
							  From QAD_DATA.dbo.pt_mstr
							  Where pt_domain In ('ATL', 'SZX', 'ZQL', 'YQL', 'HQL')
							)
	--客户零件
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';客户零件不能为空', '') + N';客户零件不能为空'
	Where et_createdBy = @uID
	And @bCustPart = 1	
	And Isnull(et_item, '') = ''

	--销售单不存在
	Update ediTemp
	Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';销售单不存在', '') + N';销售单不存在'
	Where et_createdBy = @uID
		And @bSoNbr = 1
		And Isnull(et_szx_so, '') <> ''
		And Not Exists(  Select so_nbr
						 From QAD_DATA.dbo.so_mstr
						 Where so_domain In ('ATL', 'SZX', 'ZQL', 'YQL', 'HQL')
							 And so_nbr = et_szx_so
						)

	--采购单价和价格表比较:pi_mstr.pi_min_price * 10
	Declare @et_id bigint
			, @et_qad varchar(20)
			, @et_rmks decimal(18, 5)
			, @PoRecDate datetime
			, @min_price decimal(18, 5)
			, @cus_code varchar(15)
	
	Set @et_id = 0
	Set @et_qad = ''
	Set @cus_code = ''
	Set @et_rmks = 0
	Set @PoRecDate = NULL
	Set @min_price = 0
	
	Declare pp_cursor Cursor For
		Select et_id, et_qad, et_rmks, reqDate, cusCode = 'C0000035'
		From ediTemp et
		Left Join EdiPoHrd hrd On hrd.poNbr = et.et_tcp_po
		Left Join EdiPoDet det On det.hrd_id = hrd.id
		Where Isnull(et_errMsg, N'') = N''
			And et_createdBy = @uID
			And Isnull(det.errMsg, N'') <> N''--价格无错的，不允许再修改
			And @bUnitPrice = 1 --只在更新价格的时候判断
	Open pp_cursor
	Fetch Next From pp_cursor Into @et_id, @et_qad, @et_rmks, @PoRecDate, @cus_code
	While @@Fetch_Status=0
	Begin

		Set @min_price = 0
	
		Select Top 1 @min_price = pi_min_price * 10
		From Qad_Data..pi_mstr
		Where pi_domain = 'SZX'
			And pi_cs_code = @cus_code
			And pi_part_code = @et_qad
			And Cast(Isnull(pi_start, '1901-1-1') As datetime) <= Cast(@PoRecDate As datetime)
			And Cast(Isnull(pi_expire, '3099-1-1') As datetime) >= Cast(@PoRecDate As datetime)
		Order By pi_start Desc

		If(@min_price = 0)
		Begin
			Update ediTemp
			Set et_errMsg = Replace(Isnull(et_errMsg, N''), N';价格不一致。QAD价格为0', '') + N';价格不一致。QAD价格为0'
			Where et_id = @et_id
		End
		Else
		Begin
			If(@min_price <> @et_rmks)
			Begin
				Update ediTemp
				Set et_errMsg = N'价格不一致。QAD价格为' + Cast(@min_price As varchar(20))
				Where et_id = @et_id
			End
		End

		Fetch Next From pp_cursor Into @et_id, @et_qad, @et_rmks, @PoRecDate, @cus_code
	End
	Close pp_cursor
	Deallocate pp_cursor
	

	--select * from ediTemp where et_createdBy = 13
	If Exists(	Select *
				From ediTemp
				Where et_createdBy = @uID
					And Isnull(et_errMsg, N'') <> N''
					And Isnull(et_errMsg, N'') Not Like N'%价格不一致%'
			 )
	Begin
		Set @retValue = 1
	End
	
	
	Drop Table #edi_po
END



