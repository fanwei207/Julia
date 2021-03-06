USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_sid_JudgeExistsQADShipImport]    Script Date: 06/24/2015 16:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		WANGLW
-- Create date: 2015-06-10
-- Description:	判断出运单物料号是否存在EDI_DE..CP_PART
-- =============================================
ALTER PROCEDURE [dbo].[sp_sid_JudgeExistsQADShipImport]
	@qad varchar(20)
	, @so_nbr varchar(20)
	, @so_line varchar(20)
	, @po varchar(20) = ''
	, @uID as int = 0
AS
/*
exec sp_sid_JudgeExistsQADShipImport '15100527000120','SS411166','3','OP38919',92095

*/

BEGIN

	If(Exists(Select case when ISNULL(podet.sku,'') <> '' then podet.xPartNbr when  isnull(podet.sku, '') = '' And ISNULL(podet.partNbr,'') <> '' then  podet.partNbr else cp_cust_part end As cust_par
			From EDI_DB..EdiPoHrd hrd
			Left Join EDI_DB..EdiPoDet podet on podet.hrd_id = hrd.id
			Left Join EDI_DB..cp_mstr cp on podet.qadPart = cp.cp_part
			Left Join QAD_Data..so_mstr  so on  cp.cp_cust = so.so_cust 	
				And (so.so_ord_date >= Isnull(cp.cp_start_Date, '1900-1-1') And so.so_ord_date <= Isnull(cp.cp_end_Date, '3900-1-1'))
			where hrd.poNbr =  @po
				And podet.poLine = @so_line
				))	
	Begin
		Select 1
	End
	Else If(Exists(
			Select cp_cust_part 
			From EDI_DB..cp_mstr cp
			inner join QAD_Data..so_mstr so on so.so_cust = cp.cp_cust
				And (so.so_ord_date >= Isnull(cp.cp_start_Date, '1900-1-1') And so.so_ord_date <= Isnull(cp.cp_end_Date, '3900-1-1'))
			Where cp.cp_part = @qad
				And so.so_nbr = @so_nbr
				And ISNULL(cp_cust_part,'') <> ''
				))
				
	Begin
		Select 1
	End
	Else
	Begin
		Select 0
	End
END
