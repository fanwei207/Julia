USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_sid_SelectPackingDetailsInfo1]    Script Date: 05/12/2015 13:26:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Wang Lw
-- Create date: 2014/09/16
-- Description:	sp_sid_SelectDeclarationInfo
-- =============================================
ALTER PROCEDURE [dbo].[sp_sid_SelectPackingDetailsInfo1]
(
	@shipno nvarchar(50)
	,@uid		int
	,@plantcode int
	,@checkpricedate datetime
)
As

/*
EXEC sp_sid_SelectPackingDetailsInfo1 'EP14121','13','1'

*/
	Begin
	
		declare @cust varchar(10)
				, @qad	varchar(20)
				, @shipto	varchar(10)
				, @ord_Date	varchar(20)
				, @domain	varchar(10)
		delete sid_price where Pi_createdBy = @uid and Pi_plantecode = @plantcode
		declare ship_cur cursor for
		  select distinct so_cust, SID_QAD, case substring(so_ship,1,1) when 'C' then so_ship else ad2.ad_attn end as so_ship, '', so_domain
		  From tcpc0.dbo.sid_det det  
			  left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id  
			  left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr
			  inner join QAD_Data.dbo.ad_mstr ad1 on so_ship = ad1.ad_addr-- and so_cust = ad_ref
			  inner join QAD_Data.dbo.ad_mstr  ad2 on so_cust = ad1.ad_addr
			  Where sid_nbr = @shipno
		open ship_cur
		fetch next from ship_cur into @cust, @qad, @shipto, @ord_Date, @domain
		while @@FETCH_STATUS=0
		begin
			
			if(
					(
					select COUNT(*)from Pi_PriceList 
					where Pi_Cust=@cust 
							And Pi_QAD=@qad
							And Pi_ShipTo=@shipto
							And @checkpricedate>=Pi_StartDate And (@checkpricedate <= Pi_EndDate or Pi_EndDate is null)
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
						And (@checkpricedate>=Pi_StartDate And (@checkpricedate <= Pi_EndDate or Pi_EndDate is null))
						And Pi_DoMain = @domain
				end
			else
				begin
					insert into sid_price (Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,Pi_createdBy,Pi_plantecode) 
					select Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,@uid,@plantcode 
					from Pi_PriceList 
					where Pi_Cust=@cust 
						And Pi_QAD=@qad  
						And (@checkpricedate>=Pi_StartDate And (@checkpricedate <= Pi_EndDate Or Pi_EndDate Is Null))
						And Pi_DoMain = @domain
				end			
			fetch next from ship_cur into @cust, @qad, @shipto, @ord_Date, @domain
		end
		close ship_cur
		deallocate ship_cur
		
		Declare @sql nvarchar(Max)
		Set @sql = null
		Set @sql = '  select *from ( '
		Set @sql = @sql + ' select top 20 sid_no,SID_PO,sid_qad,sid_cust_part,sid_cust_partdesc,case Pi_UM when ''SETS'' then SID_qty_set else sid_qty_pcs end as sid_qty_pcs,Pi_UM as sid_qty_unit, '
		Set @sql = @sql + ' price.Pi_price1,price.Pi_price2,price.Pi_price3,price.PI_UM,sid_qty_box,Pi_UM AS sid_ptype,sid_weight,sid_volume, Pi_Currency '
		Set @sql = @sql + ' From tcpc0.dbo.sid_det det '
		Set @sql = @sql + ' left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '
		Set @sql = @sql + ' left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr '
		Set @sql = @sql + ' left join sid_price price on price.Pi_Cust=so_cust and price.Pi_QAD=SID_QAD	'--and price.pi_shipTo = so_ship '
		Set @sql = @sql + ' Where sid_nbr = ''' + @shipno + ''' '
		Set @sql = @sql + ' order by sid_no ) shipdata '
		Set @sql = @sql + ' union all '
		Set @sql = @sql + ' select null, '''', '''', '''', ''TOTAL PCS'',sum(sid_qty_pcs), '''', null, null, null, null, sum(sid_qty_box), ''CTNS'',sum(sid_weight),sum(sid_volume), '''' '
		Set @sql = @sql + ' From tcpc0.dbo.sid_det det  left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id '
		Set @sql = @sql + ' Where sid_nbr = ''' + @shipno + ''' '
		
					
	select @sql
		
		
		
	End
