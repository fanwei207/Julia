USE [EDI_DB]
GO
/****** Object:  StoredProcedure [dbo].[sp_edi_selectManualPoDet]    Script Date: 2015/06/23 Tuesday 8:47:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_edi_selectManualPoDet]
	
	@hrd_id bigint
AS
BEGIN
	Set Nocount On
	
	Select mpod_id, mpod_hrd_id, mpod_line, mpod_cust_part, mpod_qad, mpod_ord_qty, mpod_um, mpod_price
		, mpod_req_date, mpod_due_date, mpod_rmks, mpod_createdBy, mpod_createdName, mpod_createdDate, mpod_sod_site
		, mpod_submittedBy = Isnull(mpod_submittedBy, 0)
		, mpod_isAppended = Isnull(mpod_isAppended, 0),mpod_SKU
	From ManualPoDet
	Where mpod_hrd_id = @hrd_id
	and isnull( mpod_860insert,0) = 0
	Order By mpod_line
END


