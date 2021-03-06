USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_sid_SelectPackingDetailsInfo2]    Script Date: 06/23/2015 09:05:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Wang Lw
-- Create date: 2014/09/16
-- Description:	sp_sid_SelectDeclarationInfo
-- =============================================
ALTER PROCEDURE [dbo].[sp_sid_SelectPackingDetailsInfo2]
(
	@shipno nvarchar(50)
	,@uid		varchar(10)
	,@plantcode int	
	,@checkpricedate datetime
)
As

/*
EXEC sp_sid_SelectPackingDetailsInfo2 '53166610','92095','1','2014-10-29'

*/
	Begin
	
		declare @cust varchar(10)
				, @qad	varchar(20)
				, @shipto	varchar(10)
				, @ord_Date	varchar(20)
				, @domain	varchar(10)
		delete sid_price where Pi_createdBy = @uid and Pi_plantecode = @plantcode
		declare ship_cur cursor for
		  select distinct mstr.sid_so_cust, SID_QAD, case substring(mstr.sid_so_ship,1,1) when 'C' then mstr.sid_so_ship else ad_attn end as so_ship, '', mstr.sid_so_domain
		  From tcpc0.dbo.sid_det det  
			  left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id  
			  --left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr
			  left join QAD_Data.dbo.ad_mstr on mstr.sid_so_ship = ad_addr and mstr.sid_so_cust = ad_ref and mstr.sid_so_domain = ad_domain
			  Where sid_nbr = @shipno
		open ship_cur
		fetch next from ship_cur into @cust, @qad, @shipto, @ord_Date, @domain
		while @@FETCH_STATUS=0
		begin
			
			delete sid_price where Pi_Cust = @cust and Pi_QAD = @qad  and pi_createdby= @uid
			if(
					(
					select COUNT(*)from Pi_PriceList 
					where Pi_Cust=@cust 
							And Pi_QAD=@qad
							And Pi_ShipTo=@shipto
							And @checkpricedate>=Pi_StartDate And (@checkpricedate <= Pi_EndDate Or Pi_EndDate Is Null)
							And Pi_DoMain = @domain
					)>0
				)
				begin
					insert into sid_price (Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,Pi_createdBy,Pi_plantecode)
					select Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,@uid,@plantcode 
					from Pi_PriceList 
					where Pi_Cust=@cust 
						And Pi_QAD=@qad  
						And Pi_ShipTo=@shipto 
						And (@checkpricedate>=Pi_StartDate And (@checkpricedate <= Pi_EndDate Or Pi_EndDate Is Null))
						And Pi_DoMain = @domain
				end
			else
				begin
					
					insert into sid_price (Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,Pi_createdBy,Pi_plantecode) 
					select Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,@uid,@plantcode 
					from Pi_PriceList
					where Pi_Cust=@cust 
						And Pi_QAD=@qad
						And Pi_ShipTo is null 
						And (@checkpricedate>=Pi_StartDate And (@checkpricedate <= Pi_EndDate  Or Pi_EndDate Is Null))
						And Pi_DoMain = @domain
				end			
			fetch next from ship_cur into @cust, @qad, @shipto, @ord_Date, @domain
		end
		close ship_cur
		deallocate ship_cur
		
		Declare @sql nvarchar(Max)
		Set @sql = null
		Set @sql = '  select *from ( '
		if((select count(*)from sid_price where pi_createdby = @uid) > 0)
		begin
			Set @sql = @sql + ' select top 200 sid_so_line, SID_PO, sid_nbr,sid_qad,sid_cust_part '
			--Set @sql = @sql + ' ,case so_ship when ''C0000006'' then hts.IIHSCD  + '' '' + Ltrim(Rtrim(DRDL01)) else  Ltrim(Rtrim(DRDL01)) end as sid_cust_partdesc '
			--Set @sql = @sql + ' ,case when mstr.sid_so_ship=''C0000006'' then hts.IIHSCD  + '' '' + Ltrim(Rtrim(DRDL01)) when  substring(mstr.sid_so_ship,1,1) != ''C'' and ad_attn =''C0000006'' then  hts.IIHSCD  + '' '' + Ltrim(Rtrim(DRDL01)) else  Ltrim(Rtrim(DRDL01)) end as sid_cust_partdesc '
			Set @sql = @sql + ' ,hts.sid_hst + '' '' + hts.sid_description as sid_cust_partdesc '
	
			Set @sql = @sql + ' ,case Pi_UM when ''SETS'' then SID_qty_set else sid_qty_pcs end as sid_qty_pcs,Pi_UM as sid_qty_unit, '


			Set @sql = @sql + ' price.Pi_price1,cast(price.Pi_price2 as decimal(30,4)) as Pi_price2,price.Pi_price3,price.PI_UM,sid_qty_box,Pi_UM AS sid_ptype,sid_weight,sid_volume, Pi_Currency '
			Set @sql = @sql +' , docs.PhysicalName as doc_path, docs.name as doc_name '
			Set @sql = @sql + ' From tcpc0.dbo.sid_det det '
			Set @sql = @sql + ' left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '
			--Set @sql = @sql + ' left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr '
			Set @sql = @sql + ' left join sid_price price on price.Pi_Cust = mstr.sid_so_cust and price.Pi_QAD=SID_QAD and price.pi_createdby = (' + @uid + ') '--and price.pi_shipTo = so_ship '
			Set @sql = @sql + ' inner join SID_PrintPacking_Temp temp on temp.sid_fob = mstr.SID_nbr '
			--Set @sql = @sql + ' left join JDE_DATA.dbo.HTSCODES_FromJDEProduction hts on Ltrim(Rtrim(hts.IMLITM)) =  case mstr.sid_so_ship when ''C0000004'' then substring(SID_cust_part,9,len(SID_cust_part)-8) else det.SID_cust_part end '
 			Set @sql = @sql + ' left join SID_CustDiscription hts on hts.sid_cust = mstr.sid_so_cust  and sid_partid = det.sid_cust_part '
 			Set @sql = @sql + ' left join QAD_Data.dbo.ad_mstr on mstr.sid_so_ship = ad_addr and mstr.sid_so_cust = ad_ref and mstr.sid_so_cust = ad_ref and mstr.sid_so_domain = ad_domain '
 			---Update By WangLW at 2015-05-13 增加关联客户附件
 			Set @sql = @sql + ' left join EDI_DB..ManualPoHrd PoHrd on det.sid_po = pohrd.mpo_nbr '
 			Set @sql = @sql + ' left join EDI_DB..ManualPoDocs docs on  pohrd.mpo_id = docs.hrdid '
			
 			Set @sql = @sql + ' Where sid_uid = (' + @uid + ') '
			Set @sql = @sql + ' order by sid_so_line ) shipdata '
			Set @sql = @sql + ' union all '
			Set @sql = @sql + ' select null, '''', '''', '''', '''', ''TOTAL'',sum(case Pi_UM when ''SETS'' then SID_qty_set else sid_qty_pcs end), '''' '
			--增加价格汇总 Update by WangLW at:2015－06－23
			Set @sql = @sql + ' , cast(sum(case Pi_UM when ''SETS'' then SID_qty_set else sid_qty_pcs end*price.Pi_price1)as decimal(30,5)), cast(sum(case Pi_UM when ''SETS'' then SID_qty_set else sid_qty_pcs end*price.Pi_price2)as decimal(30,4)), cast(sum(case Pi_UM when ''SETS'' then SID_qty_set else sid_qty_pcs end*price.Pi_price3)as decimal(30,5)) '
			Set @sql = @sql + ' , null, sum(sid_qty_box), ''CTNS'',sum(sid_weight),sum(sid_volume), '''', '''', '''' '
			Set @sql = @sql + ' From tcpc0.dbo.sid_det det '
			Set @sql = @sql + ' left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '
			Set @sql = @sql + ' inner join SID_PrintPacking_Temp temp on temp.sid_fob = mstr.SID_nbr '
			--Set @sql = @sql + ' left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr '
			Set @sql = @sql + ' left join sid_price price on price.Pi_Cust=mstr.sid_so_cust and price.Pi_QAD=SID_QAD and price.pi_createdby = (' + @uid + ') '

			Set @sql = @sql + ' Where sid_uid = (' + @uid + ')  '
		end
		else
		begin
			Set @sql = @sql + ' select top 200 sid_so_line, SID_PO, sid_nbr,sid_qad,sid_cust_part '
			--Set @sql = @sql + ' , case mstr.sid_so_ship when ''C0000006'' then hts.IIHSCD  + '' '' + Ltrim(Rtrim(DRDL01)) else  Ltrim(Rtrim(DRDL01)) end as sid_cust_partdesc '
			Set @sql = @sql + ' ,hts.sid_hst + '' '' + hts.sid_description as sid_cust_partdesc '
	
			Set @sql = @sql + ' , SID_qty_set as sid_qty_pcs,'''' as sid_qty_unit, '
			Set @sql = @sql + ' '''' as Pi_price1, '''' as Pi_price2,'''' as Pi_price3,'''' as PI_UM,sid_qty_box,'''' AS sid_ptype,sid_weight,sid_volume, '''' as Pi_Currency '
			Set @sql = @sql + ' , docs.PhysicalName as doc_path, docs.name as doc_name '
			Set @sql = @sql + ' From tcpc0.dbo.sid_det det '
			Set @sql = @sql + ' left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '
			--Set @sql = @sql + ' left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr '
			--Set @sql = @sql + ' left join sid_price price on price.Pi_Cust=so_cust and price.Pi_QAD=SID_QAD '
			Set @sql = @sql + ' inner join SID_PrintPacking_Temp temp on temp.sid_fob = mstr.SID_nbr '
			--Set @sql = @sql + ' left join JDE_DATA.dbo.HTSCODES_FromJDEProduction hts on Ltrim(Rtrim(hts.IMLITM)) =  case mstr.sid_so_ship when ''C0000004'' then substring(SID_cust_part,9,len(SID_cust_part)-8) else det.SID_cust_part end '
 			Set @sql = @sql + ' left join SID_CustDiscription hts on hts.sid_cust = mstr.sid_so_cust  and sid_partid = det.sid_cust_part ' 
 			---Update By WangLW at 2015-05-13 增加关联客户附件
 			Set @sql = @sql + ' left join EDI_DB..ManualPoHrd PoHrd on det.sid_po = pohrd.mpo_nbr '
 			Set @sql = @sql + ' left join EDI_DB..ManualPoDocs docs on  pohrd.mpo_id = docs.hrdid '
 						
 			Set @sql = @sql + ' Where sid_uid = (' + @uid + ') '
			Set @sql = @sql + ' order by sid_so_line ) shipdata '
			Set @sql = @sql + ' union all '
			Set @sql = @sql + ' select null, '''', '''', '''', '''', ''TOTAL'',sum(sid_qty_pcs), '''', null, null, null, null, sum(sid_qty_box), ''CTNS'',sum(sid_weight),sum(sid_volume), '''', '''', '''' '
			Set @sql = @sql + ' From tcpc0.dbo.sid_det det '
			Set @sql = @sql + ' left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '
			Set @sql = @sql + ' inner join SID_PrintPacking_Temp temp on temp.sid_fob = mstr.SID_nbr '
			Set @sql = @sql + ' Where sid_uid = (' + @uid + ')  '		
		end	
	select @sql
	
		
	End	
