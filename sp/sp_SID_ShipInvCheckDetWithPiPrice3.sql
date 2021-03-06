USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_SID_ShipInvCheckDetWithPiPrice3]    Script Date: 05/12/2015 13:32:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
  Created By :      Wang LW   
  Created Date :    14/11/2014
  Description :     for importing ship data to system check
*/
ALTER PROCEDURE  [dbo].[sp_SID_ShipInvCheckDetWithPiPrice3]
	@uID as int
	,@checkpricedate as datetime

AS

/*
exec sp_SID_ShipInvCheckDetWithPiPrice3 '13','2014-11-12'

*/

Declare @err as int
SET @err = 0
Declare @SID as int 
set @SID = 0
Declare @nbr as varchar(50)
set @nbr = ''

		select  @nbr = C.sid_fob from
		(
		SELECT B.sid_uid,  LEFT(NBR,LEN(NBR)-1) sid_fob,LEFT(PK,LEN(PK)-1) sid_pk FROM (
		SELECT sid_uid,
			(SELECT  ltrim(rtrim(sid_fob)) +',' FROM SID_PrintPacking_Temp WHERE sid_uid = @uID FOR XML PATH('')) AS NBR
			,(SELECT  ltrim(rtrim(sid_pk)) +',' FROM SID_PrintPacking_Temp WHERE sid_uid = @uID FOR XML PATH('')) AS PK 
		FROM SID_PrintPacking_Temp A
		WHERE sid_uid = @uID
		GROUP BY sid_uid
		) B
		) C
	
		declare @cust varchar(10)
				, @qad	varchar(20)
				, @shipto	varchar(10)
				, @ord_Date	varchar(20)
				, @domain	varchar(10)
		delete sid_price where Pi_createdBy = @uid --and Pi_plantecode = @plantcode
		declare ship_cur cursor for
		  select distinct mstr.sid_so_cust, SID_QAD, case substring(mstr.sid_so_ship,1,1) when 'C' then mstr.sid_so_ship else ad_attn end as so_ship, '', mstr.sid_so_domain
		  From tcpc0.dbo.sid_det det  
			  left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id  
			  --left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr
			  left join QAD_Data.dbo.ad_mstr on mstr.sid_so_ship = ad_addr And mstr.sid_so_cust = ad_ref And mstr.sid_so_domain = ad_domain
			  Where sid_nbr = @nbr
		open ship_cur
		fetch next from ship_cur into @cust, @qad, @shipto, @ord_Date, @domain
		while @@FETCH_STATUS=0
		begin
			
			delete sid_price where Pi_Cust = @cust And Pi_QAD = @qad  And pi_createdby= @uid
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
					select Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,@uid,'' 
					from Pi_PriceList 
					where Pi_Cust=@cust 
						And Pi_QAD=@qad  
						And Pi_ShipTo=@shipto 
						And @checkpricedate>=Pi_StartDate And (@checkpricedate <= Pi_EndDate Or Pi_EndDate Is Null)
						And Pi_DoMain = @domain
				end
			else
				begin
					
					insert into sid_price (Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,Pi_createdBy,Pi_plantecode) 
					select Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,@uid,'' 
					from Pi_PriceList
					where Pi_Cust=@cust 
						And Pi_QAD=@qad
						And Pi_ShipTo Is Null 
						And (@checkpricedate>=Pi_StartDate And (@checkpricedate <= Pi_EndDate  Or Pi_EndDate Is Null))
						And Pi_DoMain = @domain
				end			
			fetch next from ship_cur into @cust, @qad, @shipto, @ord_Date, @domain
		end
		close ship_cur
		deallocate ship_cur	
		if((select count(*)from sid_price where pi_createdby = @uid) > 0)
		begin	
		
			 if(exists
					(
					  select top 200 sid_so_line, SID_PO, sid_nbr,sid_qad,sid_cust_part  ,hts.sid_hst + ' ' + hts.sid_description as sid_cust_partdesc  
					  ,case Pi_UM when 'SETS' then SID_qty_set else sid_qty_pcs end as sid_qty_pcs,Pi_UM as sid_qty_unit,  price.Pi_price1,cast(price.Pi_price2 as decimal(30,4)) as Pi_price2,price.Pi_price3
					  From tcpc0.dbo.sid_det det  
					  left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id  
					  left join sid_price price on price.Pi_Cust = mstr.sid_so_cust And price.Pi_QAD=SID_QAD And price.pi_createdby = 92095
					  inner join SID_PrintPacking_Temp temp on temp.sid_fob = mstr.SID_nbr  
					  left join SID_CustDiscription hts on hts.sid_cust = mstr.sid_so_cust  And sid_partid = det.sid_cust_part  
					  left join QAD_Data.dbo.ad_mstr on mstr.sid_so_ship = ad_addr And mstr.sid_so_cust = ad_ref And mstr.sid_so_cust = ad_ref And mstr.sid_so_domain = ad_domain  
					  Where sid_uid = @uID
						And (det.SID_price3 Is Null Or ((det.SID_price3 Is Null Or det.SID_price3 = 0) And det.sid_price3 != price.Pi_price3))

					)
				)
			set @err =  - 1
		end

 select @err


SET ANSI_NULLS ON
