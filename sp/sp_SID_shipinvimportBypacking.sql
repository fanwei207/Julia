USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_SID_shipinvimportBypacking]    Script Date: 05/12/2015 13:27:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
  Created By :      Wang LW   
  Created Date :    16/10/2014
  Description :     for importing ship data to system
*/
ALTER PROCEDURE  [dbo].[sp_SID_shipinvimportBypacking]
@uID as int
,@checkpricedate datetime

AS

/*
exec sp_SID_shipinvimportBypacking '13','2014-10-29'

*/

Declare @err as int
SET @err = 0
Declare @SID as int 
set @SID = 0
Declare @nbr as varchar(50)
set @nbr = ''
Declare @shipno as varchar(50)
set @shipno = ''
Declare @Invoice as varchar(50)
set @Invoice = ''
Declare @po as nvarchar(50)
Set @po = ''
Declare @line as varchar(10)
Set @line = ''
Declare @part as nvarchar(50)
Set @part = ''
Declare @desc as nvarchar(200)
Set @desc = ''
Declare @price as decimal(10,5)
Set @price = 0 
Declare @price1 as decimal(10,5)
Set @price1 = 0
Declare @price3 as decimal(10,5)
Set @price3 = 0
Declare @currency as varchar(10)
Set @currency = ''
Declare @vID as int
Set @vID = 0
Declare @Ptype as varchar(500)
Set @Ptype = ''

Declare @decs as varchar(500)
Set @decs = ''

BEGIN TRANSACTION SIDShipInvImport

		select @shipno = C.sid_fob from
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
		delete sid_price where Pi_createdBy = @uid
		declare ship_cur cursor for
		  select distinct mstr.sid_so_cust,SID_QAD,case substring(mstr.sid_so_ship,1,1) when 'C' then mstr.sid_so_ship else ad_attn end as sid_so_ship,mstr.sid_so_domain
		  From tcpc0.dbo.sid_det det  
			  left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id  
			  --left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr
			  left join QAD_Data.dbo.ad_mstr on mstr.sid_so_ship = ad_addr And mstr.sid_so_cust = ad_ref And mstr.sid_so_domain = ad_domain
			  Where sid_nbr = @shipno
		open ship_cur
		fetch next from ship_cur into @cust, @qad, @shipto, @domain
		while @@FETCH_STATUS=0
		begin
			
			delete sid_price where Pi_Cust = @cust And Pi_QAD = @qad  --and Pi_ShipTo =@shipto
			--首先判定是否存在SHIPTO单价,如无获取公共单价
			if(
					(
					select COUNT(*)
					from Pi_PriceList 
					where Pi_Cust = @cust 
							And Pi_QAD = @qad
							And Pi_ShipTo = @shipto
							And (@checkpricedate >= Pi_StartDate And (@checkpricedate <= Pi_EndDate Or Pi_EndDate Is Null))
							And Pi_DoMain = @domain
					)>0
				)
				
				begin
					if @@error <> 0
					begin
						set @err = - 1
						select @err
						return
					end
					insert into sid_price (Pi_Cust, Pi_QAD, Pi_ShipTo, Pi_price1, Pi_price2, Pi_price3, Pi_Currency, Pi_UM, Pi_createdBy, Pi_plantecode)
					select Pi_Cust, Pi_QAD, Pi_ShipTo, Pi_price1, Pi_price2, Pi_price3, Pi_Currency, Pi_UM,@uid, '' 
					from Pi_PriceList 
					where Pi_Cust = @cust 
						And Pi_QAD = @qad  
						And Pi_ShipTo = @shipto 
						And (@checkpricedate >= Pi_StartDate And (@checkpricedate <= Pi_EndDate Or Pi_EndDate Is Null))
						And Pi_DoMain = @domain
					if @@error <> 0
					begin
						set @err = - 2
						select @err
						return				
					end				
				end
			else
				begin
					
					if @@error <> 0
					begin
						set @err = - 3
						select @err
						return				
					end											
					insert into sid_price (Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,Pi_createdBy,Pi_plantecode) 
					select Pi_Cust,Pi_QAD,Pi_ShipTo,Pi_price1,Pi_price2,Pi_price3,Pi_Currency,Pi_UM,@uid,'' 
					from Pi_PriceList 
					where Pi_Cust=@cust 
						And Pi_QAD=@qad
						And Pi_ShipTo Is Null
						And (@checkpricedate >= Pi_StartDate And (@checkpricedate <= Pi_EndDate Or Pi_EndDate Is Null))
						And Pi_DoMain = @domain
					if @@error <> 0
					begin
						set @err = - 4
						select @err
						return				
					end						
				end			
			fetch next from ship_cur into @cust, @qad, @shipto, @domain
		end
		close ship_cur
		deallocate ship_cur

     
         if(not exists
				(
         		  select top 2000 det.sid_id as sid_vid, mstr.SID_nbr,packing.sid_nbrno as sid_invoice,SID_PO,sid_no as SID_line,sid_cust_part,sid_cust_partdesc,  
				  price.Pi_price1 as SID_price,price.Pi_price2 as SID_price1,Pi_Currency as SID_currency,Pi_UM AS sid_ptype
				  From tcpc0.dbo.sid_det det  
				  left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id  
				  --left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr  
				  left join sid_price price on price.Pi_Cust = mstr.sid_so_cust and price.Pi_QAD=SID_QAD  and price.pi_createdby = @uID--and price.pi_shipTo = so_ship 
				  inner join SID_PrintPacking_Temp temp on temp.sid_fob = mstr.SID_nbr  
				  left join Sid_PackingInfo packing on  packing.SID_nbr = mstr.SID_nbr
				  Where sid_uid = @uID   order by sid_no
				)
			)
			begin
				set @err =  - 5
				select @err
				return				
			end		           
		    Declare shipinvimport Cursor For
		    
					select top 2000 det.sid_id as sid_vid, mstr.SID_nbr,packing.sid_nbrno as sid_invoice,SID_so_nbr,SID_so_line as SID_line,sid_cust_part,sid_cust_partdesc,  
					--price.Pi_price1 as SID_price,price.Pi_price2 as SID_price1  --判断第三方客户，价格取price3
					case when mstr.sid_so_cust = 'C0000035' then price.Pi_price1 else price.Pi_price3 end as SID_price
					, case when mstr.sid_so_cust = 'C0000035' then price.Pi_price2 else price.Pi_price3 end as SID_price1
					,price.Pi_price3
					, Pi_Currency as SID_currency,Pi_UM AS sid_ptype
					--, hts.IIHSCD + ' ' + Ltrim(Rtrim(DRDL01)) as decs
					--,case when mstr.sid_so_ship='C0000006' then hts.IIHSCD  + ' ' + Ltrim(Rtrim(DRDL01)) when  substring(mstr.sid_so_ship,1,1) != 'C' and ad_attn ='C0000006' then  hts.IIHSCD  + ' ' + Ltrim(Rtrim(DRDL01)) else  Ltrim(Rtrim(DRDL01)) end as decs
					, hts.sid_hst + ' ' + hts.sid_description as decs 
					From tcpc0.dbo.sid_det det  
					left join tcpc0.dbo.sid_mstr mstr on det.sid_id=mstr.sid_id  
					--left join  QAD_Data.dbo.so_mstr on  SID_so_nbr=so_nbr  
					left join sid_price price on price.Pi_Cust = mstr.sid_so_cust and price.Pi_QAD=SID_QAD  and price.pi_createdby = @uID--and price.pi_shipTo = so_ship
					inner join SID_PrintPacking_Temp temp on temp.sid_fob = mstr.SID_nbr  
					left join Sid_PackingInfo packing on  packing.SID_nbr = mstr.SID_nbr
					--left join JDE_DATA.dbo.HTSCODES_FromJDEProduction hts on Ltrim(Rtrim(hts.IMLITM)) =  --det.SID_cust_part
					-- case  when mstr.sid_so_ship='C0000004' and substring(SID_cust_part,8,1)='_' then substring(SID_cust_part,9,len(SID_cust_part)-8)when mstr.sid_so_ship='C0000004' and substring(SID_cust_part,11,1)='_' then substring(SID_cust_part,12,len(SID_cust_part)-11)  else det.SID_cust_part end
					left join SID_CustDiscription hts on hts.sid_cust = mstr.sid_so_cust  and sid_partid = det.sid_cust_part
					left join QAD_Data.dbo.ad_mstr on mstr.sid_so_ship = ad_addr and mstr.sid_so_cust = ad_ref and mstr.sid_so_domain = ad_domain
				  Where sid_uid = @uID   order by sid_so_line
  
		    Open shipinvimport
		    Fetch Next From shipinvimport Into @vID,@nbr,@invoice,@po,@line,@part,@desc,@price,@price1,@price3,@currency,@Ptype,@decs
		    While @@Fetch_Status=0
		    Begin
              Set @SID = 0
              select @SID = SID_id from SID_mstr where SID_nbr = @nbr
              if(@SID > 0 )
               Begin
                       
					if(exists(select top 1 * from sid_det where sid_id = @SID and (sid_po = @po or sid_fob = @po or sid_atl = @po or SID_so_nbr = @po)  and sid_cust_part = @part  ))
					begin

					   insert into sid_det_hist(SID_did, SID_id, SID_SNO, SID_SCode, SID_QAD, SID_qty_set, SID_qty_pcs, SID_qty_box, SID_qa, SID_memo, SID_so_nbr, SID_so_line, SID_price,sid_price1,sid_price3,sid_currency,
						  SID_wo, SID_PO, SID_cust_part, SID_weight, SID_volume, sid_invoice,SID_qty_pkgs,SID_fob,SID_fedx,SID_createdby, SID_createddate, SID_modifiedby, SID_modifieddate) 
					   SELECT     SID_did, SID_id, SID_SNO, SID_SCode, SID_QAD, SID_qty_set, SID_qty_pcs, SID_qty_box, SID_qa, SID_memo, SID_so_nbr, SID_so_line, SID_price,sid_price1,sid_price3, sid_currency,
						  SID_wo, SID_PO, SID_cust_part, SID_weight, SID_volume,sid_invoice,SID_qty_pkgs,SID_fob,SID_fedx, SID_createdby, SID_createddate, @uID, getdate()
					   FROM SID_det where sid_id = @SID and SID_so_nbr = @po and Sid_so_line = @line and sid_cust_part = @part
					   if @@error <> 0
					   set @err = - 6
	                   
					   update sid_det set sid_price = @price,sid_price1 =@price1,sid_price3 = @price3,sid_cust_partdesc = @decs,sid_currency = @currency ,sid_invoice = @invoice,SID_Ptype = @Ptype
					   where sid_id = @SID and (sid_po = @po or sid_fob = @po or sid_atl = @po or SID_so_nbr = @po) and Sid_so_line = @line and sid_cust_part = @part  
					   if @@error <> 0
					   set @err = - 7
					   
					   update Sid_PackingInfo set SID_checkprice=1 where SID_nbr = @nbr
	                   
					end   
					else
					begin
						set @err =  - 8
						select @err
						return				
					end
              end 
              else
               begin
					set @err =  - 9
					select @err
					return				
				end		
            
    			Fetch Next From shipinvimport Into @vID,@nbr,@invoice,@po,@line,@part,@desc,@price,@price1,@price3,@currency,@Ptype,@decs
		    End
		    Close shipinvimport
		    Deallocate shipinvimport

  
 IF (@err<>0 or @@error <> 0)	
             Rollback Transaction SIDShipInvImport
 Else
             Commit Transaction SIDShipInvImport

 select @err

