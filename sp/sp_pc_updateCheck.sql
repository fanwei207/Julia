USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pc_updateCheck]    Script Date: 2015/6/11 14:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fanwei>
-- Create date: <20141123>
-- Description:	<核价或保存方法>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pc_updateCheck]
	@uID int
	,@curr varchar(5)
	,@gvTable xml
	,@IMID char(9)
AS
begin
	declare @Imstatue int,@flag int 
	set @flag=1
	select @Imstatue=status
	from dbo.PC_InquiryMstr
	where IMID=@IMID
	
	DECLARE @tableTemp TABLE (
								IMID CHAR(9)
								,applyDetID int		
								,price DECIMAL(18,5)
								,priceSelf DECIMAL(18,5)
								,checkPrice DECIMAL(18,5)
								,createdBy INT
								,createdDate DATETIME
								,priceDiscount NVARCHAR(400)
								,PriceFinish BIT
								,isChange BIT DEFAULT 0
							)
							
		INSERT INTO @tableTemp
		        ( IMID ,
		          applyDetID ,
		          price ,
		          priceSelf ,
		          checkPrice ,
		          createdBy ,
		          createdDate ,
		          priceDiscount ,
		          PriceFinish
		        )
		select Tab.Col.value('IMID[1]','char(9)') as IMID
			   ,Tab.Col.value('applyDetID[1]','int') as applyDetID
			   ,Tab.Col.value('price[1]','decimal(18,5)') as price
			   ,Tab.Col.value('priceSelf[1]','decimal(18,5)') as priceSelf
			   ,Tab.Col.value('checkPrice[1]','decimal(18,5)') as checkPrice
			   ,Tab.Col.value('createdBy[1]','int') as createdBy
			   ,Tab.Col.value('createdDate[1]','datetime') as createdDate
			   ,Tab.Col.value('priceDiscount[1]','nvarchar(400)') as priceDiscount
			   ,Tab.Col.value('PriceFinish[1]','bit') as priceDiscount
			   from  @gvTable.nodes('//gvTable')as Tab(Col)

	
	
	
		begin tran checkPrice
			--存历史表
			insert into dbo.PC_InquiryDetHist
				        ( IMID ,
				          applyDetID ,
				          price ,
				          priceBy ,
				          priceDate ,
				          priceSelf ,
				          priceFinish ,
				          priceDiscount ,
				          checkPrice ,
				          checkBy ,
				          checkDate ,
				          checkPriceFinish ,
				          createdBy ,
				          createdDate
				        )
				 select  id.IMID,id.applyDetID,id.price,id.priceBy,id.priceDate,id.priceSelf,id.priceFinish,id.priceDiscount,id.checkPrice,id.checkBy,id.checkDate,id.checkPriceFinish,@uID,getdate()
				 from dbo.PC_InquiryDet id left join dbo.PC_ApplyDet ad on ad.DetId=id.applyDetID
				 left join (
														select Tab.Col.value('IMID[1]','char(9)') as IMID
														,Tab.Col.value('applyDetID[1]','int') as applyDetID
														,Tab.Col.value('price[1]','decimal(18,5)') as price
													   ,Tab.Col.value('priceSelf[1]','decimal(18,5)') as priceSelf
													   ,Tab.Col.value('checkPrice[1]','decimal(18,5)') as checkPrice
													   ,Tab.Col.value('createdBy[1]','int') as createdBy
													   ,Tab.Col.value('createdDate[1]','datetime') as createdDate
													   ,Tab.Col.value('priceDiscount[1]','nvarchar(400)') as priceDiscount
													   from  @gvTable.nodes('//gvTable')as Tab(Col)
													 ) s on id.IMID =s.IMID and s.applyDetID = id.applyDetID
				 
				 where id.IMID=@IMID 
				 and (isnull(id.price,-1)<>isnull(s.price,-1) or isnull(id.priceSelf,-1)<>isnull(s.priceSelf,-1) or isnull(id.checkPrice,-1)<>isnull(s.checkPrice,-1)or isnull(id.priceDiscount,'')<> isnull(s.priceDiscount,''))
				 and ad.status<=4
				update dbo.PC_InquiryMstr
				set modifiedBy=@uID,modifiedDate=getdate()
				where  IMID=@IMID    
				IF(@@ROWCOUNT>0  )
				BEGIN
					set @flag=1
				end
				
				
				
				
				UPDATE	tt
				SET isChange = 1 
				FROM @tableTemp tt LEFT JOIN PC_ApplyDet ad 
				ON tt.applyDetID=ad.DetId
				LEFT JOIN dbo.PC_InquiryDet id
				ON id.applyDetID=tt.applyDetID
				where id.IMID=@IMID
				 and ad.status<=4 
				 AND --价格出现改动
				 (
					(id.price=0 AND tt.price<>0)
					  OR (id.price<>0 AND tt.price<>id.price)
				 )
				 AND tt.PriceFinish=1
				IF(@@ROWCOUNT>0  )
				BEGIN
						DECLARE @bodyMxl NVARCHAR(MAX),@Addressee NVARCHAR(200),@cc NVARCHAR(200)
				
						SET @bodyMxl=N'以下物料和供应商价格出现修改，请修改成本<br/>'
						SET @bodyMxl=@bodyMxl +(	SELECT  N'QAD:',ad.Part,N'  供应商：',ad.Vender ,'<br/>'
						from @tableTemp tt LEFT JOIN dbo.PC_ApplyDet ad 
						ON tt.applyDetID =ad.DetId
						WHERE tt.isChange = 1 AND tt.PriceFinish=1
						FOR XML PATH(''))
						
						SELECT @Addressee = sendto ,@cc = copyto
						from WorkFlow.dbo.Rec_RecipientConfig
						WHERE  id='9C89CDBA-4069-4701-B4DC-CC589319A6B3'
						Exec msdb.dbo.sp_send_dbmail
						@profile_name = 'sql2005_sendmail'
						, @recipients = @Addressee
						, @subject = N'物料成本需要修改'--主题
						, @body = @bodyMxl--内容
						, @body_format = 'HTML'	
						, @copy_recipients = @cc


					UPDATE pc
					set pc.pc_price = CASE WHEN ISNULL(tt.price,0) = 0 THEN tt.priceSelf/1.17 ELSE tt.Price/1.17 END
						,pc.pc_amt = CASE WHEN ISNULL(tt.price,0) = 0 THEN CAST(cast(tt.priceSelf/1.17 AS DECIMAL(18,5)) AS VARCHAR(100))+';0;0;0;0;0;0;0;0;0;0;0;0;0;0' 
							 ELSE CAST(cast(tt.Price/1.17 AS DECIMAL(18,5)) AS VARCHAR(100))+';0;0;0;0;0;0;0;0;0;0;0;0;0;0' END                 
					from @tableTemp tt 
					LEFT JOIN dbo.PC_ApplyDet ad 
					ON tt.applyDetID =ad.DetId
					LEFT JOIN dbo.PC_mstr pc ON pc.pc_part = ad.part 
					AND pc.pc_list = ad.vender
					AND pc.pc_start = '2008-09-11'
					WHERE tt.isChange = 1 
					AND tt.PriceFinish=1
					AND pc.pc_list is NOT NULL
					AND ad.part is NOT NULL                   
					
				END
				
													 --若报价修改了修改报价
						 update dbo.PC_InquiryDet 
						 set  price=Tab.Col.value('price[1]','decimal(18,5)'),priceSelf=Tab.Col.value('priceSelf[1]','decimal(18,5)'),priceBy=Tab.Col.value('createdBy[1]','int')
						 	,priceDate=Tab.Col.value('createdDate[1]','datetime'),priceDiscount=Tab.Col.value('priceDiscount[1]','nvarchar(400)')
						 from @gvTable.nodes('//gvTable')as Tab(Col) left join dbo.PC_ApplyDet ad  on Tab.Col.value('applyDetID[1]','int') =ad.DetId
						 where IMID=@IMID
						 and applyDetID=Tab.Col.value('applyDetID[1]','int') 
						 and ad.status<=4 
						 and
						 (
							PC_InquiryDet.price is null  or(PC_InquiryDet.price is not null and PC_InquiryDet.price <>Tab.Col.value('price[1]','decimal(18,5)') or Tab.Col.value('price[1]','decimal(18,5)' ) is  null  )
							or PC_InquiryDet.priceSelf is null  or(PC_InquiryDet.priceSelf is not null and PC_InquiryDet.priceSelf <>Tab.Col.value('priceSelf[1]','decimal(18,5)') or Tab.Col.value('priceSelf[1]','decimal(18,5)')  is  null  )
							or PC_InquiryDet.priceDiscount is null   or(PC_InquiryDet.priceDiscount is not null and PC_InquiryDet.priceDiscount <>Tab.Col.value('priceDiscount[1]','nvarchar(400)') or Tab.Col.value('priceDiscount[1]','nvarchar(400)')  is null  )
						 )
						
						 
						
						 
						--若核价修改了修改核价
					 	 update dbo.PC_InquiryDet 
						 set  checkPrice=Tab.Col.value('checkPrice[1]','decimal(18,5)'),checkBy=Tab.Col.value('createdBy[1]','int')
						 	,checkDate=Tab.Col.value('createdDate[1]','datetime'),modifiedBy=@uID,modifiedDate=getdate()
						 from @gvTable.nodes('//gvTable')as Tab(Col)  left join dbo.PC_ApplyDet ad  on Tab.Col.value('applyDetID[1]','int') =ad.DetId
						 where IMID=@IMID and applyDetID=Tab.Col.value('applyDetID[1]','int') 
						 and ad.status<=4 
						and 
						   (
								dbo.PC_InquiryDet.checkPrice is null   or(dbo.PC_InquiryDet.checkPrice is not null and dbo.PC_InquiryDet.checkPrice <>Tab.Col.value('checkPrice[1]','decimal(18,5)') or Tab.Col.value('checkPrice[1]','decimal(18,5)') is null )
							
							)
							
						update dbo.PC_ApplyDet 
						set Price=id.price,PriceBy=id.priceBy,PriceDate=id.priceDate,PriceSelf=id.PriceSelf,modifiedBy=id.modifiedBy,modifiedDate=id.modifiedDate
							,Curr=im.curr,PriceBasis=Tab.Col.value('PriceBasis[1]','nvarchar(400)'),FinCheckPriceBasis=Tab.Col.value('FinCheckPriceBasis[1]','nvarchar(400)')
						from PC_InquiryDet id left join dbo.PC_ApplyDet ad   on id.applyDetID=ad.DetId
						left join dbo.PC_InquiryMstr im on im.IMID = id.IMID 
						left join @gvTable.nodes('//gvTable')as Tab(Col) on ad.DetId=Tab.Col.value('applyDetID[1]','int')
						where DetId=id.applyDetID and im.IMID=@IMID
						
						
				 
		if(@Imstatue>0  
		and not exists(
						select id.CheckPrice
						from dbo.PC_InquiryDet id left join dbo.PC_ApplyDet ad on ad.DetId=id.applyDetID
						where id.IMID=@IMID and isnull(ad.isCancel,0)=0 and isnull(id.CheckPrice,-1)=-1 and ad.status>=0
					  )
		and exists(select * from dbo.PC_InquiryBasis where IMID=@IMID and type=1 and modifiedBy is null  )
		  )--核价
	begin
		set @flag=2
	end
				
		if(@flag=2)
		begin
			update dbo.PC_InquiryMstr
			set modifiedBy=@uID,modifiedDate=getdate(),status=2
			where  IMID=@IMID   
			
			
			

							
						--修改申请详表的状态
						update dbo.PC_ApplyDet
						set status=4,CheckPrice=id.checkPrice,CheckDate=id.checkDate,checkBy=id.createdBy,modifiedBy=@uID,modifiedDate=getdate()
						from PC_InquiryDet id  left join PC_ApplyDet ad on ad.DetId=id.applyDetID
						where ad.DetId in (
											select applyDetID
											from dbo.PC_InquiryDet
											where IMID=@IMID 
										)and ad.status<=4 and ad.status>=0
			
		end
		else
		begin
			update dbo.PC_InquiryMstr
			set modifiedBy=@uID,modifiedDate=getdate(),status=1
			where  IMID=@IMID   
			

				

				update ad
				set status=4,CheckPrice=id.checkPrice,CheckDate=id.checkDate,checkBy=id.createdBy,modifiedBy=@uID,modifiedDate=getdate()
				from PC_InquiryDet id  left join PC_ApplyDet ad on ad.DetId=id.applyDetID
				where ad.DetId in (
									select applyDetID
									from dbo.PC_InquiryDet
									where IMID=@IMID  and checkPrice is not null
								) and ad.status<=4 and ad.status>=0
								
				update ad
				set status=3, CheckPrice=id.checkPrice,CheckDate=id.checkDate,checkBy=id.createdBy,modifiedBy=@uID,modifiedDate=getdate()
				from PC_InquiryDet id  left join PC_ApplyDet ad on ad.DetId=id.applyDetID
				where ad.DetId in (
									select applyDetID
									from dbo.PC_InquiryDet
									where IMID=@IMID  and checkPrice is null
								) and ad.status<=4 and ad.status>=0
								
				
		end
		
		
		
		
			if(@@error <>0)  
			begin
				select 0
				rollback tran checkPrice
			end
			else
			BEGIN
				select @flag
				commit tran checkPrice
			END
	 
       --select Tab.Col.value('IMID[1]','char(9)'),Tab.Col.value('applyDetID[1]','int'),Tab.Col.value('price[1]','decimal(18,5)')
       --,Tab.Col.value('priceSelf[1]','decimal(18,5)'),Tab.Col.value('checkPrice[1]','decimal(18,5)'),Tab.Col.value('createdBy[1]','int')
       --,Tab.Col.value('createdDate[1]','datetime'),Tab.Col.value('priceDiscount[1]','nvarchar(400)')
       --from  @gvTable.nodes('//gvTable')as Tab(Col)
END
