USE [EDI_DB]
GO
/****** Object:  StoredProcedure [dbo].[sp_edi_checkManualPOValidity]    Script Date: 2015/06/23 Tuesday 1:40:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shanzm
-- Create date: 2012-12-17
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_edi_checkManualPOValidity]
	
	@uID bigint
	, @retValue bit Output
AS
BEGIN
/*
	Declare @retValue  bit
	Exec sp_edi_checkManualPOValidity 13, @retValue Output
	select @retValue '@retValue'
*/

	Set @retValue = 0
	
	--客户代码不能为空，并且要再ad_mstr中
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Cust Code is required', '') + N';Cust Code is required'
	Where Not Exists(Select * From QAD_Data.dbo.ad_mstr Where ad_domain In('SZX', 'ZQZ', 'ZQL', 'YQL', 'HQL','TCB') And ad_type = 'Customer' And ad_addr = hrd_cust)
		And hrd_createdBy = @uID
	
	--单位必须合法
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';non-standard UM', '') + N';non-standard UM'
	where Not Exists(SELECT TOP (200) code_fldname, code_value, code_cmmt, code_domain FROM QAD_Data..code_mstr WHERE (code_fldname = 'pt_um') and code_value = det_um)
		And hrd_createdBy = @uID

	--客户代码必须在Qad_Data..edtmx_ref中存在
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Cust Code do not in edtmx_ref table,contact with sysadmin', '') + N';Cust Code do not in edtmx_ref table,contact with sysadmin'
	Where Not Exists(Select * From QAD_Data.dbo.edtmx_ref Where edtmx_tp_site = hrd_cust)
		And Isnull(hrd_cust, '') <> ''
		And hrd_createdBy = @uID
	
	--客户订单不能为空，而且不能重复
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Cust PO is required', '') + N';Cust PO is required'
	Where Isnull(hrd_nbr, '') = ''
		And hrd_createdBy = @uID
		
	--客户订单字符长度不能小于2
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Cust PO must be at least 2 characters long', '') + N';Cust PO must be at least 2 characters long'
	Where Len(Rtrim(hrd_nbr)) <2
		And hrd_createdBy = @uID
		
	--需求日期和截止日期格式
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Req Date is required and muse be a date', '') + N';Req Date is required and muse be a date'
	Where Isdate(hrd_req_date) = 0
		And hrd_createdBy = @uID
	
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Due Date is required and muse be a date', '') + N';Due Date is required and muse be a date'
	Where Isdate(hrd_due_date) = 0
		And hrd_createdBy = @uID
		
	--发货至不能为空，且必须在ad_mstr中
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Ship To is required', '') + N';Ship To is required'
	Where Not Exists(Select * From QAD_Data.dbo.ad_mstr Where ad_domain In('SZX', 'ZQZ', 'ZQL', 'YQL', 'HQL','TCB') And (ad_type = 'Customer' Or ad_addr = 'ship-to') And ad_addr = hrd_shipto)
		And hrd_createdBy = @uID
			
	--行号：是数字 Isnumeric只能判断是不是数字，而不能判断是不是整数
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Line is required and must be a integer', '') + N';Line is required and must be a integer'
	Where (Isnumeric(det_line) = 0 Or det_line Like '%.%' Or Isnull(det_line, '0') = '0')
		And hrd_createdBy = @uID
	
	--客户物料不能为空。零件号找不到，可以进入系统
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Cust Part/QAD is required', '') + N';Cust Part/QAD is required'
	Where Isnull(det_cust_part, '') = ''
		And hrd_createdBy = @uID
		
	--先验证QAD号，再根据客户零件号找QAD号
	Update ManualPoTemp
	Set det_qad = Ltrim(Rtrim(cp_part))
	From cp_mstr
	Where cp_domain = Case When hrd_cust = 'CS310118' Then 'TCB' Else 'SZX' End
		And det_cust_part = cp_cust_part
		And cp_cust =  Case When Exists(Select Top 1 * From CustTrans Where orignalCust = hrd_cust) Then 'C0000035'
							Else hrd_cust End
		And hrd_createdBy = @uID
		And Isnull(cp_start_date, '1900-1-1') <= GetDate()
		And Isnull(cp_end_date, '3900-1-1') >= GetDate()
	
	--订单数量：数字
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Qty is required and must be numeric', '') + N';Qty is required and must be numeric'
	Where Isnumeric(det_ord_qty) = 0
		And hrd_createdBy = @uID	

	--价格：数字
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Price is required and must be numeric', '') + N';Price is required and must be numeric'
	Where Isnumeric(det_price) = 0
		And hrd_createdBy = @uID
			
	--需求日期和截止日期格式
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Req Date is required and muse be a date', '') + N';Req Date is required and muse be a date'
	Where Isdate(det_req_date) = 0
		And hrd_createdBy = @uID
	
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Due Date is required and muse be a date', '') + N';Due Date is required and muse be a date'
	Where Isdate(det_due_date) = 0
		And hrd_createdBy = @uID
		
	--订单行不能重复：客户订单号 + 行号
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Line can not be repeated', '') + N';Line can not be repeated'
	From
	(
		Select nbr = hrd_nbr, line = det_line
		From ManualPoTemp
		Where Isnull(hrd_nbr, '') <> ''
			And Isnumeric(det_line) <> 0
			And hrd_createdBy = @uID
		Group By hrd_nbr, det_line
		Having Count(det_line) > 1
	) mstr
	Where hrd_nbr = nbr
		And det_line = line
		And hrd_createdBy = @uID
	
	--订单行不能存在，但能附加
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Line has entered the system', '') + N';Line has entered the system'
	Where Exists(	Select * 
					From ManualPoHrd hrd
					Left Join ManualPoDet det On det.mpod_hrd_id = hrd.mpo_id
					Where hrd.mpo_nbr = hrd_nbr
						And det.mpod_line = det_line
				)
		And hrd_createdBy = @uID
	
	--如果订单已经提交，则不允许附加
	Update ManualPoTemp
	Set hrd_errMsg = Replace(Isnull(hrd_errMsg, N''), N';Line has been submitted', '') + N';Line has been submitted'
	Where Exists(	Select mpo_cust, mpo_nbr
					From ManualPoHrd
					Where mpo_submittedBy Is Not Null
						And mpo_cust = hrd_cust
						And mpo_nbr = hrd_nbr
				)
		And hrd_createdBy = @uID

	If Exists(	Select Top 1 *
				From ManualPoTemp
				Where hrd_createdBy = @uID
					And Isnull(hrd_errMsg, N'') <> N''
			 )
	Begin
		Set @retValue = 1
	End
	
END



