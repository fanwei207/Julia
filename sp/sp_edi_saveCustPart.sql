USE [EDI_DB]
GO
/****** Object:  StoredProcedure [dbo].[sp_edi_saveCustPart]    Script Date: 2015/06/17 Wednesday 10:10:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        
-- Create date: 
-- Description:    
-- =============================================
ALTER PROCEDURE [dbo].[sp_edi_saveCustPart]
    
    @id bigint
    , @domain varchar(5)
    , @custCode varchar(10)
    , @custPart varchar(50)
    , @qad varchar(20)
    , @stdDate varchar(10)
    , @endDate varchar(10)
    , @comment nvarchar(50)
    , @partd nvarchar(50)
    , @sku nvarchar(50)
    , @uID bigint
    , @uName nvarchar(10)  
	, @ul nvarchar(100) = ''
    , @retValue nvarchar(50) Output

AS
BEGIN
    Declare @std_Date datetime
    Declare @end_Date datetime
    Declare @topend_Date datetime
    Set Nocount On
     Set @retValue = N''
   
    
    
    If Not Exists(Select Top 1 * From Qad_Data.dbo.pt_mstr Where  pt_part = @qad)
    Begin
        Set @retValue = N'物料号不正确!请确保你已正确填写了物料号!'
    End
    if  Exists( Select *
                From cp_mstr
                Where cp_domain = @domain
					And cp_cust = @custCode
                    And cp_cust_part = @custPart
                    And (
                           Isnull( cp_start_date,'') = '' 
                            Or Isnull (cp_end_date,'')= ''
                        )
                    )
				begin
					 Set @retValue = N'历史日期不能留空!'
				end
	if (@retValue = N'') 
	begin
		if Exists(    Select *
                From cp_mstr
                Where cp_domain = @domain
					And cp_cust = @custCode
                    And cp_cust_part = @custPart
                    And (
                            (cp_start_date <= Isnull(@std_Date, '1900-1-1') And cp_end_date >=Isnull(@std_Date, '1900-1-1'))
                            Or (cp_start_date <= Isnull(@end_Date, '3900-1-1') And cp_end_date >= Isnull(@end_Date, '3900-1-1'))
                            Or ( Isnull(@std_Date, '1900-1-1') <= cp_start_date And Isnull(@end_Date, '1900-1-1') >=cp_start_date)
                             Or ( Isnull(@std_Date, '1900-1-1') <= cp_end_date And Isnull(@end_Date, '1900-1-1') >=cp_end_date)
                        )
                    And cp_id <> @id
					And cp_part <> @qad
                        )
            begin
                    
                Set @retValue = N'同一个客户、同一客户零件在一个期间内，只能对应一个QAD号!'
            end
			if Exists(    Select *
                From cp_mstr
                Where cp_domain = @domain
					And cp_cust = @custCode
                    And  cp_part = @qad
                    And (
                            (cp_start_date <= Isnull(@std_Date, '1900-1-1') And cp_end_date >=Isnull(@std_Date, '1900-1-1'))
                            Or (cp_start_date <= Isnull(@end_Date, '3900-1-1') And cp_end_date >= Isnull(@end_Date, '3900-1-1'))
                            Or ( Isnull(@std_Date, '1900-1-1') <= cp_start_date And Isnull(@end_Date, '1900-1-1') >=cp_start_date)
                             Or ( Isnull(@std_Date, '1900-1-1') <= cp_end_date And Isnull(@end_Date, '1900-1-1') >=cp_end_date)
                        )
                    And cp_id <> @id
					And cp_cust_part <> @custPart
                        )
            begin
                    
                Set @retValue = N'同一个客户、同一QAD号，在同一期间内，只能对应一个客户零件!'
            end
	end
    if (@retValue = N'') 
	begin
		if exists (select max(cp_end_date)
			from cp_mstr 
			 Where cp_domain = @domain
				And cp_cust = @custCode
				And cp_cust_part = @custPart)
		begin
			select @topend_Date = max(cp_end_date)
			from cp_mstr 
			 Where cp_domain = @domain
				And cp_cust = @custCode
				And cp_cust_part = @custPart
				
		    set @topend_Date = DATEADD(day,1,@topend_Date)
		   
		    
			if(CAST( @stdDate as datetime)<> @topend_Date )
				begin
					 Set @retValue = N'开始时间必须是'+CONVERT(char(10),@topend_Date,120)
				end
		end
	end
     if(@retValue = N'')
    Begin
    
        Set @stdDate = Isnull(@stdDate, '')
        If(@stdDate = '') 
            begin
                Set @std_Date = null
                set @stdDate= '1900-1-1' --开始日期如果为空的话，默认是1900-1-1
            end
        else
            begin
                set @std_Date = CAST(@stdDate as datetime)
            end
        Set @endDate = Isnull(@endDate, '')
        If(@endDate = '') 
            begin
                Set @end_Date = null
                set @endDate = null 
            end
        else
            begin
                set @end_Date = CAST(@endDate as datetime)
            end
            
      
        
        
        --如果在指定的@stdDate和@endDate找到一条记录，则更新，否则新增，但新增的日期期间必须晚于原先的日期期间
         if (isnull(@std_Date,'')<>'')
        begin
         If    Exists(    Select *
                From cp_mstr
                Where cp_domain = @domain
					And cp_cust = @custCode
                    And cp_cust_part = @custPart
                    And (
                            (cp_start_date <= Isnull(@std_Date, '1900-1-1') And cp_end_date >=Isnull(@std_Date, '1900-1-1'))
                            Or (cp_start_date <= Isnull(@end_Date, '3900-1-1') And cp_end_date >= Isnull(@end_Date, '3900-1-1'))
                            Or ( Isnull(@std_Date, '1900-1-1') <= cp_start_date And Isnull(@end_Date, '1900-1-1') >=cp_start_date)
                             Or ( Isnull(@std_Date, '1900-1-1') <= cp_end_date And Isnull(@end_Date, '1900-1-1') >=cp_end_date)
                        )
                    And cp_id <> @id
                        )
            begin
                    
                Set @retValue = N'所填写的有效期与历史有效期重合!'
            end
        end
       
        end
         if (@retValue = N'')
        begin
		 
                insert into cp_mstr(cp_cust, cp_cust_part, cp_part, cp_comment, cp_cust_partd, cp_domain
					, cp_start_date, cp_end_date, cp_createdBy, cp_createdName, cp_createdDate, cp_ul)
                Values (@custCode, @custPart, @qad, @comment, @partd, @domain
                , @stdDate, @endDate, @uID, @uName, GETDATE(), @ul)
            end
        
        --备份到JDE_Data.XJdeCustSKU中
      if (@retValue = N'')
		  Begin
			If Exists(Select * From JDE_Data.dbo.XJdeCustSKU Where Xord_Cust = @custCode And Xord_Part = @custPart And Xord_Cust_Item <> @sku)
			Begin
				Insert Into JDE_Data.dbo.XJdeCustSKUHist(Xord_Cust, Xord_Cust_Item, Xord_Part, Xord_Cust_Desc, histBy, histName, histDate)
				Select Xord_Cust, Xord_Cust_Item, Xord_Part, Xord_Cust_Desc, @uID, @uName, GetDate()
				From JDE_Data.dbo.XJdeCustSKU
				Where Xord_Cust = @custCode
					And Xord_Part = @custPart
					
				Update JDE_Data.dbo.XJdeCustSKU
				Set Xord_Cust_Item = cpt_sku
				From cp_temp
				Where cpt_createdBy = @uID
					And Xord_Cust = cpt_cust
					And Xord_Part = cpt_cust_part			
					And Xord_Cust_Item <> @sku
			End
	       
			Insert Into JDE_Data.dbo.XJdeCustSKU(Xord_Cust, Xord_Cust_Item, Xord_Part)
			Select @custCode, @sku, @custPart
			Where Not Exists(Select * From JDE_Data.dbo.XJdeCustSKU Where Xord_Cust = @custCode  And Xord_Part = @custPart)
		End
    End



 