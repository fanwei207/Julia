USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_sid_selectprintpackinglist]    Script Date: 05/12/2015 13:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_sid_selectprintpackinglist]
	@pk varchar(20) = ''
	,@nbr varchar(20) = ''
	,@shipdate1 varchar(20) = ''
	,@shipdate2 varchar(20) = ''
	,@cust varchar(20) = ''
	,@lastcust varchar(20) = ''
	,@bldate1 varchar(20) = ''
	,@bldate2 varchar(20) = ''
as

/*

exec sp_sid_selectprintpackinglist '','ZSY14045','','','','','',''
*/

begin
	
	
	select mstr.sid_nbr as nbr, mstr.SID_PK as PK,  convert(varchar(11),mstr.SID_shipdate,120) as  shipdated
	
	, mstr.SID_site as shipsiteby, det.SID_QAD as QAD, det.SID_cust_part as cust_part
	, det.SID_qty_set as qty_set, det.SID_qty_pcs as qty_pcs,det.SID_so_nbr as so_nbr, det.SID_so_line as so_line, det.SID_ATL as ATL, det.SID_PO as PO, so.so_fob as fob

	, price.Pi_UM as UM, det.SID_Ptype as Ptype, packing.sid_shipdate as shipdate
	--, price.Pi_price1 as price1, case when price.Pi_UM='SETS' then  det.SID_qty_set*price.Pi_price1 else det.SID_qty_pcs*price.Pi_price1 end as amount1
	--, price.Pi_price2 as price2, case when price.Pi_UM='SETS' then  det.SID_qty_set*price.Pi_price2 else det.SID_qty_pcs*price.Pi_price2 end as amount2
	, det.sid_price, case when price.Pi_UM='SETS' then  det.SID_qty_set*det.sid_price else det.SID_qty_pcs*det.sid_price end as amount1
	, det.sid_price1, case when price.Pi_UM='SETS' then  det.SID_qty_set*det.sid_price1 else det.SID_qty_pcs*det.sid_price1 end as amount2
	, case when det.sid_price3 is null or det.sid_price3 = 0 then price.Pi_price3 else det.sid_price3 end as price3
	--, case when price.Pi_UM = 'SETS' and det.sid_price3 is null or det.sid_price3 = 0  then  det.SID_qty_set*price.Pi_price3 else det.SID_qty_pcs*det.sid_price3 end as amount3
	,case when price.Pi_UM = 'SETS' and (det.sid_price3 is null or det.sid_price3 = 0)  then  det.SID_qty_set*price.Pi_price3 
		when price.Pi_UM = 'SETS' and  det.sid_price3 != 0  then  det.SID_qty_set*det.sid_price3
		when price.Pi_UM = 'PCS' and (det.sid_price3 is null or det.sid_price3 = 0)  then  det.SID_qty_pcs*price.Pi_price3 
		when price.Pi_UM = 'PCS' and  det.sid_price3 != 0  then  det.SID_qty_pcs*det.sid_price3
		else det.SID_qty_pcs*det.sid_price3 end as amount3
	, det.SID_Invoice as Invoice
	,so.so_cust as cust, ad1.ad_name as name, case when substring(mstr.sid_so_ship,1,1) = 'C' then mstr.sid_so_ship else ad2.ad_attn end as ship, ad2.ad_name as name, '' as printdate , mstr.sid_domain as domain, ''
	from SID_mstr mstr 
	inner join SID_det det on mstr.SID_id = det.SID_id
	left join QAD_Data..so_mstr so on so.so_nbr = det.SID_so_nbr
	inner join sid_packinginfo packing on packing.SID_nbr = mstr.sid_nbr And sid_checkprice = '1'
	left join QAD_Data..ad_mstr ad1 on mstr.sid_so_cust = ad1.ad_addr And mstr.sid_so_domain =  ad1.ad_domain
	left join QAD_Data..ad_mstr ad2 on mstr.sid_so_ship = ad2.ad_addr And mstr.sid_so_domain =  ad2.ad_domain
	left join Pi_PriceList price on price.Pi_Cust = mstr.sid_so_cust 
		And price.Pi_QAD = det.SID_QAD  
		And price.Pi_ShipTo = case when substring(mstr.sid_so_ship,1,1) = 'C' then mstr.sid_so_ship else ad2.ad_attn end
		And (packing.sid_shipdate >= price.Pi_StartDate and (packing.sid_shipdate <= price.Pi_EndDate or price.Pi_EndDate is null))
		And  price.Pi_DoMain = mstr.sid_so_domain
	where	(@nbr = '' or (@nbr <> '' and mstr.sid_nbr = @nbr))--'53176651'
			And (@pk = '' or (@pk <> '' and mstr.SID_PK = @pk))
			And (@shipdate1 = '' or (@shipdate1 <> '' and mstr.SID_shipdate >= @shipdate1))
			And (@shipdate2 = '' or (@shipdate2 <> '' and mstr.SID_shipdate <= @shipdate2))
			And (@cust = '' or (@cust <> '' and so.so_cust = @cust))
			And (@lastcust = '' or (@lastcust <> '' and so.so_ship = @lastcust))
			--and (@bldate1 = '' or (@bldate1 <> '' and mstr.SID_PK >= @bldate1))
			--and (@bldate2 = '' or (@bldate2 <> '' and mstr.SID_PK <= @bldate2))

end
