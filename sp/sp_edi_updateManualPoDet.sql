USE [EDI_DB]
GO
/****** Object:  StoredProcedure [dbo].[sp_edi_updateManualPoDet]    Script Date: 2015/06/23 Tuesday 9:46:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shanzm
-- Create date: 2013-1-2
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_edi_updateManualPoDet]
	
	@id bigint
	, @qty varchar(10)
	, @price decimal(18, 5)
	, @reqDate varchar(10)
	, @dueDate varchar(10)
	, @rmks nvarchar(50)
	, @modifiedName nvarchar(15)
	, @modifiedBy bigint
	, @retValue bit Output
	, @SKU nvarchar(50) = ''
AS
BEGIN
	
	Set Nocount On
		
	Begin Transaction edi_updateManualPoDet
	
		Update ManualPoDet
		Set mpod_ord_qty = @qty
			, mpod_price = @price
			, mpod_req_date = @reqDate
			, mpod_due_date = @dueDate
			, mpod_rmks = @rmks
			, mpod_modifiedBy = @modifiedBy
			, mpod_modifiedName = @modifiedName
			, mpod_modifiedDate = GetDate()
			, mpod_SKU = @SKU
		Where mpod_id = @id
				
	If(@@Error <> 0)
	Begin
		Set @retValue = 0
		Rollback Transaction edi_updateManualPoDet
	End
	Else
	Begin
		Set @retValue = 1
		Commit Transaction edi_updateManualPoDet
	End
	
END


