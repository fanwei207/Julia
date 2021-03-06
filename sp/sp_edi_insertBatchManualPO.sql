USE [EDI_DB]
GO
/****** Object:  StoredProcedure [dbo].[sp_edi_insertBatchManualPO]    Script Date: 2015/06/23 Tuesday 9:11:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_edi_insertBatchManualPO]
	
	@uID bigint
	, @uName nvarchar(10)
	, @plantCode   int = 0
	, @retValue bit Output

AS
BEGIN
/*
	Declare @retValue  bit
	Exec sp_edi_insertbatchEDIPO 13, 0, 1, 1, 1, @retValue Output
	select @retValue '@retValue'
*/	
	Set Nocount On
	
	Declare @error int
	Set @error = 0
	
	Begin Transaction edi_insertBatchManualPO
		--操作人对应的域
		Declare @domain varchar(4)
		Select @domain = plantCode
		From tcpc0.dbo.Plants
		Where plantID = @plantCode
		
		Insert Into ManualPoHrd(mpo_cust, mpo_nbr, mpo_req_date, mpo_due_date, mpo_shipto, mpo_shipvia 
								, mpo_rmks, mpo_createdBy, mpo_createdName, mpo_createdDate, mpo_domain, mpo_channel)
		Select hrd_cust, hrd_nbr, hrd_req_date, hrd_due_date, hrd_shipto, hrd_shipvia, hrd_rmks, hrd_createdBy, @uName, GetDate(), @domain, channel
		From
		(
			Select hrd_cust, hrd_nbr, hrd_req_date, hrd_due_date, hrd_shipto, hrd_shipvia, hrd_rmks, hrd_createdBy
				, row = Row_Number() Over(Partition By hrd_nbr Order By hrd_req_date Asc, hrd_due_date Desc), channel
			From ManualPoTemp
			Where hrd_createdBy = @uID
		) mstr
		Where row = 1
			And Not Exists(Select * From ManualPoHrd Where mpo_cust = hrd_cust And mpo_nbr = hrd_nbr)
		If(@@Error <> 0) Set @error = -1
		
		Insert Into ManualPoDet(mpod_hrd_id, mpod_line, mpod_cust_part, mpod_qad, mpod_ord_qty, mpod_um, mpod_price
								, mpod_req_date, mpod_due_date, mpod_rmks, mpod_createdBy, mpod_createdName, mpod_createdDate, mpod_cust,mpod_SKU)
		Select hrd.mpo_id, det_line, det_cust_part, det_qad, det_ord_qty, det_um, det_price
				, det_req_date, det_due_date, det_rmks, hrd_createdBy, @uName, GetDate(), tmp.hrd_cust,SKU
		From ManualPoTemp tmp
		Left Join ManualPoHrd hrd On hrd.mpo_cust = tmp.hrd_cust And hrd.mpo_nbr = tmp.hrd_nbr
		Where hrd_createdBy = @uID
			And Not Exists(Select * From ManualPoDet Where mpod_hrd_id = hrd.mpo_id And mpod_line = det_line)
		If(@@Error <> 0) Set @error = -2
	
	If(@@Error <> 0 Or @error < 0)
	Begin
		Set @retValue = 0 
		Rollback Transaction edi_insertBatchManualPO
	End
	Else
	Begin
		Set @retValue = 1
		Commit Transaction edi_insertBatchManualPO
	End
END
