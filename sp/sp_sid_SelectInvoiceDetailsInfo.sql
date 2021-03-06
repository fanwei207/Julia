USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_sid_SelectInvoiceDetailsInfo]    Script Date: 05/12/2015 13:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wang Lw
-- Create date: 2014/09/16	
-- Description:	sp_sid_SelectDeclarationInfo
-- =============================================
ALTER PROCEDURE [dbo].[sp_sid_SelectInvoiceDetailsInfo]
(
	@shipno		nvarchar(50)
	,@uid		varchar(20)
	,@plantcode int
	,@checkpricedate datetime
)
As

/*
EXEC sp_sid_SelectInvoiceDetailsInfo '53166610','92095','1','2014-10-29'

*/
	Begin
	

		declare @cust varchar(10)
				, @qad	varchar(20)
				, @shipto	varchar(10)
				, @ord_Date	varchar(20)	
				, @domain	varchar(10)	
		delete sid_price where Pi_createdBy = @uid and Pi_plantecode = @plantcode
		declare ship_cur cursor for
		  select distinct mstr.sid_so_cust,SID_QAD,case substring(mstr.sid_so_ship,1,1) when 'C' then mstr.sid_so_ship else ad_attn end as sid_so_ship,'', mstr.sid_so_domain
		  From tcpc0.dbo.sid_det det  
			  left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id  
			  left join QAD_Data.dbo.ad_mstr on mstr.sid_so_ship = ad_addr and mstr.sid_so_cust = ad_ref and mstr.sid_so_domain = ad_domain
			  Where sid_nbr = @shipno
		open ship_cur
		fetch next from ship_cur into @cust, @qad, @shipto, @ord_Date, @domain
		while @@FETCH_STATUS=0
		begin
			
			delete sid_price where Pi_Cust = @cust And Pi_QAD = @qad And pi_createdby = @uid --and Pi_ShipTo =@shipto 
			if(
					(
					select COUNT(*)
					from
						(
						select * from Pi_PriceList 
						where Pi_Cust = @cust And Pi_QAD = @qad  
								And (@checkpricedate >= Pi_StartDate And @checkpricedate <= Pi_EndDate) 
								And Pi_ShipTo = @shipto
								And Pi_DoMain = @domain
						union All
						select *from Pi_PriceList 
						where Pi_Cust=@cust and Pi_QAD = @qad  
								And (@checkpricedate >= Pi_StartDate And Pi_EndDate Is Null) 
								And Pi_ShipTo = @shipto
								And Pi_DoMain = @domain
						) A
					)>0
				)
				begin
					insert into sid_price (Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,Pi_createdBy,Pi_plantecode)
					select Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,@uid,@plantcode 
					from Pi_PriceList 
					where Pi_Cust=@cust 
						And Pi_QAD=@qad  
						And Pi_ShipTo=@shipto 
						And ((@checkpricedate >= Pi_StartDate And @checkpricedate <= Pi_EndDate) Or (@checkpricedate>=Pi_StartDate And Pi_EndDate Is Null))
						And Pi_DoMain = @domain
				end
			else
				begin
					insert into sid_price (Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,Pi_createdBy,Pi_plantecode) 
					select Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,@uid,@plantcode 
					from Pi_PriceList 
					where Pi_Cust = @cust 
						And Pi_QAD = @qad
						And Pi_ShipTo Is Null
						And ((@checkpricedate >= Pi_StartDate And @checkpricedate <= Pi_EndDate) Or (@checkpricedate>=Pi_StartDate And Pi_EndDate Is Null))
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
		
			Set @sql = @sql + ' select top 200 sid_so_line,SID_PO,sid_qad,sid_cust_part '
			Set @sql = @sql + ' ,hts.sid_hst + '' '' + hts.sid_description as sid_cust_partdesc '
			Set @sql = @sql + ' ,case Pi_UM when ''SETS'' then cast(SID_qty_set as float) else cast(sid_qty_pcs as float) end as sid_qty_pcs, Pi_UM as sid_qty_unit '
			
			Set @sql = @sql + ' , case Pi_price1 when ''0.00000'' then null else Pi_price1 end as SID_price1 '
			Set @sql = @sql + ' , cast(case Pi_price2 when ''0.00000'' then null else Pi_price2 end as decimal(30,4)) as SID_price2 '
			Set @sql = @sql + ' , case Pi_price3 when ''0.00000'' then null else Pi_price3 end as SID_price3 '
			Set @sql = @sql + ' ,Pi_Currency as SID_currency '
			Set @sql = @sql + ' ,cast(case Pi_UM when ''SETS'' then cast(SID_qty_set as float) else cast(sid_qty_pcs as float) end*case Pi_price1 when ''0.00000'' then null else Pi_price1 end  as decimal(30,2)) as amount1 '
			Set @sql = @sql + ' ,cast(case Pi_UM when ''SETS'' then cast(SID_qty_set as float) else cast(sid_qty_pcs as float) end*case Pi_price2 when ''0.00000'' then null else Pi_price2 end  as decimal(30,2)) as amount2 '
			Set @sql = @sql + ' ,cast(case Pi_UM when ''SETS'' then cast(SID_qty_set as float) else cast(sid_qty_pcs as float) end*case Pi_price3 when ''0.00000'' then null else Pi_price3 end  as decimal(30,2)) as amount3 '
			Set @sql = @sql + ' ,Pi_Currency as SID_currency1 '
			Set @sql = @sql + ' From tcpc0.dbo.sid_det det '
			Set @sql = @sql + ' left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '
			Set @sql = @sql + ' left join sid_price on Pi_Cust = mstr.sid_so_cust and Pi_QAD=SID_QAD and pi_createdby = (' + @uid + ') '--and price.pi_shipTo = so_ship '
			Set @sql = @sql + ' left join SID_CustDiscription hts on hts.sid_cust = mstr.sid_so_cust  and hts.sid_partid = det.sid_cust_part '
			Set @sql = @sql + ' left join QAD_Data.dbo.ad_mstr on mstr.sid_so_ship = ad_addr and mstr.sid_so_cust = ad_ref and mstr.sid_so_cust = ad_ref and mstr.sid_so_domain = ad_domain '

			
			If(@shipno <> '') 
			begin
				Set @sql = @sql + ' Where sid_nbr = ''' + @shipno + '''  order by sid_so_line) ship '
			end
			Set @sql = @sql + ' union all '
			Set @sql = @sql + ' select null, '''', '''','''',''TOTAL'',sum(cast(case Pi_UM when ''SETS'' then cast(SID_qty_set as float) else cast(sid_qty_pcs as float) end as decimal(30,0))),'''',NULL,NULL,NULL,NULL '
			Set @sql = @sql + ' ,sum(cast(case Pi_UM when ''SETS'' then cast(SID_qty_set as float) else cast(sid_qty_pcs as float) end*case Pi_price1 when ''0.00000'' then null else Pi_price1 end  as decimal(30,2))) '
			Set @sql = @sql + ' ,sum(cast(case Pi_UM when ''SETS'' then cast(SID_qty_set as float) else cast(sid_qty_pcs as float) end*case Pi_price2 when ''0.00000'' then null else cast( Pi_price2  as decimal(30,4)) end  as decimal(30,2))) '
			Set @sql = @sql + ' ,sum(cast(case Pi_UM when ''SETS'' then cast(SID_qty_set as float) else cast(sid_qty_pcs as float) end*case Pi_price3 when ''0.00000'' then null else Pi_price3 end  as decimal(30,2))) '
			
			Set @sql = @sql + ' ,(select top 1 Pi_Currency From tcpc0.dbo.sid_det det  left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id  ' 
			Set @sql = @sql + ' left join sid_price on Pi_Cust = mstr.sid_so_cust and Pi_QAD=SID_QAD '
			
			Set @sql = @sql + '  Where sid_nbr =  ''' + @shipno + '''  and Pi_Currency is not null)  '		
			Set @sql = @sql + ' From tcpc0.dbo.sid_det det '
			Set @sql = @sql + ' left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '		
			Set @sql = @sql + ' left join sid_price on Pi_Cust = mstr.sid_so_cust and Pi_QAD=SID_QAD '
			
			If(@shipno <> '') Set @sql = @sql + ' Where sid_nbr = ''' + @shipno + ''' and pi_createdby = (' + @uid + ') '
		end
		else
		begin
			Set @sql = @sql + ' select top 200 sid_so_line,SID_PO,sid_qad,sid_cust_part '
			Set @sql = @sql + ' ,hts.sid_hst + '' '' + hts.sid_description as sid_cust_partdesc '

			Set @sql = @sql + ' ,SID_qty_set as sid_qty_pcs, '''' as sid_qty_unit '
			
			Set @sql = @sql + ' , '''' as SID_price1 '
			Set @sql = @sql + ' , '''' as SID_price2 '
			Set @sql = @sql + ' , '''' as SID_price3 '
			Set @sql = @sql + ' ,	'''' as SID_currency '
			Set @sql = @sql + ' ,	'''' as amount1 '
			Set @sql = @sql + ' ,	'''' as amount2 '
			Set @sql = @sql + ' ,	'''' as amount3 '
			Set @sql = @sql + ' ,	'''' as SID_currency1 '
			Set @sql = @sql + ' From tcpc0.dbo.sid_det det '
			Set @sql = @sql + ' left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '
			Set @sql = @sql + ' left join SID_CustDiscription hts on hts.sid_cust = mstr.sid_so_cust  and hts.sid_partid = det.sid_cust_part '
						
			If(@shipno <> '') 
			begin
 				Set @sql = @sql + ' Where sid_nbr = ''' + @shipno + ''' order by sid_so_line) ship '
			end
			Set @sql = @sql + ' union all '
			Set @sql = @sql + ' select null, '''', '''','''',''TOTAL'',sum(cast(sid_qty_pcs as decimal(30,0))),'''',NULL,NULL,NULL,NULL '
			Set @sql = @sql + ' ,	'''' '
			Set @sql = @sql + ' ,	'''' '
			Set @sql = @sql + ' ,	'''' '
			
			Set @sql = @sql + ' ,'''' '
					
			Set @sql = @sql + ' From tcpc0.dbo.sid_det det '
			Set @sql = @sql + ' left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '		
			
			If(@shipno <> '') Set @sql = @sql + ' Where sid_nbr = ''' + @shipno + ''' '		
		end
		
		Select @sql
	End
	
	


SET ANSI_NULLS ON
