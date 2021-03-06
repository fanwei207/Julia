USE [EDI_DB]
GO
/****** Object:  StoredProcedure [dbo].[sp_edi_insertManualPoDet]    Script Date: 2015/06/23 Tuesday 9:38:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shanzm
-- Create date: 2013-1-2
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_edi_insertManualPoDet]
	
	@hrd_id bigint
	, @cust varchar(10)
	, @line bigint
	, @part nvarchar(50)
	, @qad varchar(15)
	, @qty varchar(10)
	, @um varchar(10)
	, @price decimal(18, 5)
	, @reqDate varchar(10)
	, @dueDate varchar(10)
	, @rmks nvarchar(50)
	, @createdName nvarchar(15)
	, @createdBy bigint
	, @retValue bit Output
	, @SKU nvarchar(50) = ''
AS
BEGIN
	
	Set Nocount On
	
	Declare @flg_submit bit--2015-01-23：只能追加行，提交后，不能修改行
		, @cust_interchange varchar(10)--诸如HomeD的客户物料号存在C0000035中
	
	Set @cust_interchange = Case When Exists(Select Top 1 * From CustTrans Where orignalCust = @cust) Then 'C0000035'
							Else @cust End
		
	Begin Transaction edi_insertManualPoDet
	
		Select @qad = cp_part From cp_mstr Where cp_cust = @cust_interchange And cp_cust_part = @part And Isnull(cp_start_date, '1900-1-1') <= GetDate() And Isnull(cp_end_date, '3900-1-1') >= GetDate()
		
		Select @flg_submit = Case When Isnull(mpo_submittedBy, 0) = 0 Then Cast(0 As bit) Else Cast(1 As bit) End From ManualPoHrd Where mpo_id = @hrd_id
		If Exists(Select Top 1 * From ManualPoDet Where mpod_hrd_id = @hrd_id And mpod_line = @line)
		Begin
			--提交的情况下，不可修改
			If(Isnull(@flg_submit, 0) = 0)
			Begin
				Update ManualPoDet
				Set mpod_cust_part = @part
					, mpod_qad = @qad
					, mpod_ord_qty = @qty
					, mpod_um = @um
					, mpod_price = @price
					, mpod_req_date = @reqDate
					, mpod_due_date = @dueDate
					, mpod_rmks = @rmks 
					, mpod_cust = @cust
					, mpod_SKU = @SKU
				Where mpod_hrd_id = @hrd_id
					And mpod_line = @line
			End		
		End
		Else
		Begin
			Insert Into ManualPoDet(mpod_hrd_id, mpod_line, mpod_cust_part, mpod_qad, mpod_ord_qty, mpod_um, mpod_price, mpod_req_date, mpod_due_date, 
						  mpod_rmks, mpod_cust, mpod_createdBy, mpod_createdName, mpod_createdDate, mpod_isAppended,mpod_SKU)
			Values(@hrd_id, @line, @part, @qad, @qty, @um, @price, @reqDate, @dueDate, @rmks, @cust, @createdBy, @createdName, GetDate(), @flg_submit,@SKU)
		End
					
	If(@@Error <> 0)
	Begin
		Set @retValue = 0
		Rollback Transaction edi_insertManualPoDet
	End
	Else
	Begin
		Set @retValue = 1
		Commit Transaction edi_insertManualPoDet
	End
	
END



