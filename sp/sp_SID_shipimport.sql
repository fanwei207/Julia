USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_SID_shipimport]    Script Date: 07/06/2015 15:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
  Created By :      Liu Yi   
  Created Date :    9/1/2009
  Description :     for importing ship data to system
*/
ALTER PROCEDURE  [dbo].[sp_SID_shipimport]
	@uID as int
As
	Declare @err as int
	Set @err = 0
	Declare @ID as int 
	Set @ID = 0
	Declare @SID as int 
	Set @SID = 0
	Declare @nbr as char(50)
	Set @nbr = ''

	Declare @SID_Det Table( [SID_id] [int] NULL,
							[SID_SNO] [varchar](4) NULL,
							[SID_SCode] [nvarchar](50) NULL,
							[SID_QAD] [nvarchar](50) NULL,
							[SID_qty_set] [decimal](18, 2) NULL,
							[SID_qty_pcs] [decimal](18, 2) NULL,
							[SID_qty_box] [decimal](18, 2) NULL,
							[SID_qa] [nvarchar](20) NULL,
							[SID_memo] [nvarchar](50) NULL,
							[SID_so_nbr] [varchar](10) NULL,
							[SID_so_line] [int] NULL,
							[SID_price] [decimal](10, 5) NULL,
							[SID_wo] [nvarchar](100) NULL,
							[SID_PO] [nvarchar](50) NULL,
							[SID_cust_part] [nvarchar](50) NULL,
							[SID_weight] [decimal](18, 2) NULL,
							[SID_volume] [decimal](18, 3) NULL,
							[SID_createdby] [int] NULL,
							[SID_createddate] [datetime] NULL,
							[SID_qty_pkgs] [decimal](18, 2) NULL,
							[SID_Fob] [nvarchar](50) NULL,
							[SID_Fedx] [nvarchar](50) NULL,
							[SID_Invoice] [varchar](50) NULL,
							[SID_Ptype] [varchar](5) NULL,
							[SID_status] [varchar](50) NULL,
							[SID_ATL] [nvarchar](50) NULL,
							[SID_no] [int] NULL,
							[SID_Commencement] [varchar](20) NULL,
							[SID_BL] [nvarchar](20) NULL,
							[SID_qty_box_sum] [decimal](18, 2) NULL,
							[SID_Cust] [varchar](10) NULL,
							[SID_Shipto] [varchar](10) NULL,
							[SID_Domain] [varchar](10) NULL,
							[SID_Site] [varchar](10) NULL
						 )

	BEGIN TRANSACTION SIDShipImport
		Declare shipimport Cursor For
			Select SID_id, SID_nbr
			From SID_mstr_temp
			Where SID_createdby = @uID
		Open shipimport
		Fetch Next From shipimport Into @ID, @nbr
		While @@Fetch_Status=0
			Begin
				If(Exists(Select top 1 * From SID_mstr Where SID_nbr = @nbr)) 
					Begin
						Set @err = @err - 1
					End 
				Else
					Begin
						
						--插入主表：SID_mstr
						Insert Into SID_mstr(SID_PK, SID_nbr, SID_outdate, SID_via, SID_Ctype, SID_shipdate, SID_shipto, SID_site, SID_PKref, SID_domain, SID_approvedby, SID_createdby, SID_createddate
							, SID_SO_Domain, SID_SO_Cust, SID_SO_Ship)
						Select SID_PK, SID_nbr, SID_outdate, SID_via, SID_Ctype, SID_shipdate, SID_shipto, SID_site, SID_PKref, SID_domain, SID_approvedby, @uID, getdate() 
							, (Select Top 1 so_domain
								from SID_det_temp temp
								Left Join Qad_Data.dbo.so_mstr so On so.so_nbr = SID_so_nbr
								where SID_id in(select SID_id from SID_mstr_temp Where SID_createdby = @uID and SID_id =@ID)
								)
							, (Select Top 1 so_cust
								from SID_det_temp temp
								Left Join Qad_Data.dbo.so_mstr so On so.so_nbr = SID_so_nbr
								where SID_id in(select SID_id from SID_mstr_temp Where SID_createdby = @uID and SID_id =@ID)
								)
							, (Select Top 1 case when substring(so_ship,1,1)='C' then so.so_ship else ad.ad_attn end as so_ship
								from SID_det_temp temp
								Left Join Qad_Data.dbo.so_mstr so On so.so_nbr = SID_so_nbr
								left join qad_data.dbo.ad_mstr ad on ad.ad_addr = so.so_ship and so.so_domain = ad.ad_domain
								where SID_id in(select SID_id from SID_mstr_temp Where SID_createdby = @uID and SID_id =@ID)
								)
						From SID_mstr_temp
						Where SID_createdby = @uID And SID_id = @ID  
						
						Select @SID = @@Identity
              
						If (@@Error <> 0) Set @err = @err - 1
						
						Delete From @SID_Det
						--存入临时表变量/客户物料号从EDI或CP_MSTR中获取 update WANGLW at 2015-06-24
						Insert Into @SID_Det(SID_ID, SID_SNO, SID_SCode, SID_QAD, SID_qty_set, SID_qty_pcs, SID_qty_box, SID_qa, SID_memo, SID_so_nbr, SID_so_line, SID_price,SID_wo, SID_PO, 
										    SID_cust_part, SID_weight, SID_volume,sid_qty_pkgs,sid_fob,sid_fedx,sid_no,sid_atl, SID_createdby, SID_createddate, SID_Domain, SID_Cust, SID_Shipto, SID_Site)
 						Select @SID, SID_SNO, SID_SCode, SID_QAD, SID_qty_set, SID_qty_pcs, SID_qty_box, SID_qa, SID_memo, SID_so_nbr, SID_so_line, SID_price,SID_wo, SID_PO
 								, case when ISNULL(podet.partNbr,'') <> '' And  ISNULL(podet.sku,'') <> '' And so_ship in('C0000004','C0000067') then podet.xPartNbr 
 									when  ISNULL(podet.partNbr,'') <> '' And so_ship not in('C0000004','C0000067') then  podet.partNbr 
 									else cp_cust_part end As cust_part
							   , SID_weight, SID_volume, sid_qty_pkgs, sid_fob, sid_fedx ,sid_no, sid_atl, @uID, getdate()
							   , so_domain, so_cust, so_ship, so_site
						From SID_det_temp temp
						Left Join (
									select so_domain,so_site,so.so_nbr,so.so_cust,so_ord_date,case when so.so_ship like N'C%' then so.so_ship when so.so_ship not like N'C%' then case when isnull(ad_attn,'') = '' then ad_ref end end as so_ship
									from QAD_Data..so_mstr so
									Inner Join QAD_Data..ad_mstr ad on so.so_ship = ad.ad_addr And so.so_domain = ad.ad_domain
									)so1 on temp.SID_so_nbr = so1.so_nbr
						Left Join EDI_DB..EdiPoHrd hrd on hrd.soNbr = temp.SID_so_nbr--hrd.poNbr = temp.SID_PO 更改为关联销售单号
						Left Join EDI_DB..EdiPoDet podet on podet.hrd_id = hrd.id And temp.SID_so_line = podet.poLine
						Left Join (
										select *from
										(
										select  no=row_number() over(partition by cp_cust,cp_part order by cp_start_date desc), cp_part As qad , cp_cust_part
										From SID_det_temp temp
										Left Join QAD_Data..so_mstr  so on temp.SID_so_nbr = so.so_nbr
										Left Join EDI_DB..cp_mstr cp on  cp.cp_cust = so.so_cust 	
											And (so.so_ord_date >= Isnull(cp.cp_start_Date, '1900-1-1') And so.so_ord_date <= Isnull(cp.cp_end_Date, '3900-1-1'))
											And temp.SID_QAD = cp.cp_part						
										Where  SID_createdby = @uID
										)a
										where no = 1
									) tmp on tmp.qad=temp.SID_QAD							
						
						Where SID_id = @ID
							And SID_createdby = @uID
						If (@@Error <> 0) Set @err = @err - 1
						
						--插入明细表：SID_Det
						Insert Into SID_det(SID_ID, SID_SNO, SID_SCode, SID_QAD, SID_qty_set, SID_qty_pcs, SID_qty_box, SID_qa, SID_memo, SID_so_nbr, SID_so_line, SID_price,SID_wo, SID_PO, 
										    SID_cust_part, SID_weight, SID_volume,sid_qty_pkgs,sid_fob,sid_fedx,sid_no,sid_atl, SID_createdby, SID_createddate, SID_Domain, SID_Cust, SID_Shipto, SID_Site)
						Select @SID, SID_SNO, SID_SCode, SID_QAD, SID_qty_set, SID_qty_pcs, SID_qty_box, SID_qa, SID_memo, SID_so_nbr, SID_so_line, SID_price,SID_wo, SID_PO, SID_cust_part, 
							   SID_weight, SID_volume, sid_qty_pkgs, sid_fob, sid_fedx ,sid_no, sid_atl, @uID, getdate()
							   , SID_Domain, SID_Cust, SID_Shipto, SID_Site
						From @SID_Det temp
						Where Not Exists(Select * From SID_det Where SID_ID = temp.SID_ID And SID_so_nbr = temp.SID_so_nbr And SID_so_line = temp.SID_so_line)
						
						If (@@Error <> 0) Set @err = @err - 1 
						
						--Add By Shanzm 2014-05-30:导入时，增加判断Item中是否存在mpi，将没有mpi的QAD存入SID_ItemWithNoMpi
						Insert Into SID_ItemWithNoMpi(itm_cust, itm_so_nbr, itm_qad, itm_cust_part)
						Select Distinct SID_Cust, SID_so_nbr, SID_QAD, SID_Cust_Part
						From @SID_Det
						Where SID_QAD Is Not Null
							And Not Exists(Select * From Items Where item_qad = SID_QAD And Isnull(mpi, '') <> '')
							And Not Exists(Select * From SID_ItemWithNoMpi Where itm_qad = SID_QAD And itm_so_nbr = SID_so_nbr)
							
						If (@@Error <> 0) Set @err = @err - 1  

						--Add By WangLW 2015-06-10:导入时，增加判EDI_DB..CPMSTR中是否存在cp_part，将没有cp_part的QAD存入SID_ItemCppart
						Insert Into SID_ItemCppart(itm_cust, itm_so_nbr, itm_qad, itm_cust_part)
						Select Distinct SID_Cust, SID_so_nbr, SID_QAD, SID_Cust_Part
						From @SID_Det det
						Where SID_QAD Is Not Null
							And Not Exists(Select * From Qad_Data.dbo.so_mstr so Inner Join EDI_DB..cp_mstr cp on det.SID_QAD = cp.cp_part
							And so.so_cust = cp.cp_cust
							And so.so_nbr = det.SID_so_nbr
							And (so.so_ord_date >= Isnull(cp.cp_start_Date, '1900-1-1') And so.so_ord_date <= Isnull(cp.cp_end_Date, '3900-1-1'))
							)
							
						If (@@Error <> 0) Set @err = @err - 1  

						Update sid_det  
						Set sid_det.sid_SCode = qad_data.dbo.code_mstr.code_cmmt
						From qad_data.dbo.code_mstr 
						Where sid_det.sid_sno = qad_data.dbo.code_mstr.code_value
							And qad_data.dbo.code_mstr.code_fldname = 'pt_promo'
							And sid_det.sid_id = @SID
							And sid_det.sid_scode Is Null 
						If(@@Error <> 0) Set @err = @err - 1
						
						--更新ad_attn
						Update SID_Det
						Set SID_Shipto = ad_attn
						From Qad_Data.dbo.ad_mstr
						Where Isnull(SID_Shipto, '') <> ''
							And Left(Isnull(SID_Shipto, ''), 1) <> 'C'
							And ad_type = 'ship-to'
							And ad_ref = SID_Cust
							And ad_addr = SID_Shipto
							And Isnull(ad_attn, '') <> ''
							And sid_id = @SID
						
					End 
    			Fetch Next From shipimport Into @ID, @nbr
		    End
		Close shipimport
		Deallocate shipimport
	
	If(@@Error <> 0) Set @err = @err - 1

 IF (@err<>0 Or @@Error <> 0)	
	Rollback Transaction SIDShipImport
 Else
	Commit Transaction SIDShipImport

 Return @err


