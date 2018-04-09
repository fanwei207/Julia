USE [RD_Workflow]
GO
/****** Object:  StoredProcedure [dbo].[sp_rdw_SelectRdwQueryList]    Script Date: 06/17/2015 17:30:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ye Bin
-- Create date: 2011/01/11
-- Description:	sp_RDW_SelectRdwQueryLis
-- Modify: Wangcaixia 2013-11-13 prod 为prod , add  select rm.RDW_ProdSKU, rd.RDW_TaskID
-- Modify: Young Yang 2014-5-19 添加qad号查询条件
-- Modify: Young Yang 2014-5-26 添加category查询条件
--Modify: Liu jun hong 2014-9-15 添加状态No Expired & no completed
-- =============================================
ALTER PROCEDURE [dbo].[sp_rdw_SelectRdwQueryList]
(
	@proj nvarchar(100),
	@prod nvarchar(50),
	@start varchar(10),
	@status varchar(20),
	@uid varchar(10),
	@viewall bit,
	@showall int,  --  bit--->int
	@qad varchar(15) = '',
	@cateid varchar(6) ='0',
	@sku nvarchar(50)='--'
)

/* 
Exec sp_RDW_SelectRdwQueryList
	@proj = ''
	, @prod = ''
	, @start = ''
	, @status = 'PROCESS'
	, @uid = 13
	, @viewall = 1
	, @showall = 1
*/
As
	Begin
		Declare @sql nvarchar(Max)
		Set @sql = null		
		
		Set @sql = ' Select rm.RDW_MstrID As ID, DateDiff(day, Isnull(rd.RDW_EndDate, getdate()), Isnull(rd.RDW_FinishDate, getdate())) As DelayDays, '
		Set @sql = @sql + ' rm.RDW_Standard As Standard, rm.RDW_Partner As Partner, rm.RDW_Status As Status, Case When rd.RDW_ParentID=0 and IsNull(rd.RDW_Predecessor,0)>0 and IsNull(prd.RDW_Status,0)<>2 Then 0 Else 1 End as isActive,'
		Set @sql = @sql + ' rm.RDW_CreatedBy As CreatedBy, isnull(rm.RDW_CreatedDate,''1900-01-01'') As CreatedDate, ' 
	    Set @sql = @sql + ' isnull(rd.RDW_StartDate,''1900-01-01'') As StartDate, isnull(rd.RDW_EndDate,''1900-01-01'') As EndDate, isnull(rd.RDW_FinishDate,''1900-01-01'') As FinishDate, '
		Set @sql = @sql + ' ct.cate_name,rM.RDW_Project As Project,rM.RDW_ProdCode As ProdCode, rM.RDW_ProdDesc As ProdDesc, '
		Set @sql = @sql + ' rM.RDW_CreatedDate As CreatedDate, U.userName As Creater, rd.RDW_TaskID As RDW_TaskID, rd.rdw_StepName as CurrStep, '
		Set @sql = @sql + ' rd.RDW_StartDate As StartDate, rd.RDW_EndDate As EndDate, rd.RDW_FinishDate As FinishDate, '
		Set @sql = @sql + ' Isnull(ptr.[values], '''') As RDW_Ptr, Isnull(mbr.[values],'''') As RDW_Mbr,'
		Set @sql = @sql + ' rd.RDW_StepDesc, rm.RDW_ProdSKU As RDW_ProdSKU, sku.ProductCategory, sku.Lumens, sku.Voltage, sku.Wattage,  '
		Set @sql = @sql + ' sku.BeamAngle,sku.CCT,sku.CRI,sku.DriverType, sku.LEDChipType, sku.STKorMTO, sku.UPC, sku.UL, sku.CreateUser,rm.rdw_memo As Memo'
		Set @sql = @sql + ' ,Case when Isnull(rd.RDW_FinishDate, getdate()) > 0 '
		Set @sql = @sql + ' And Case When rd.RDW_ParentID=0 and IsNull(rd.RDW_Predecessor,0)>0 and IsNull(prd.RDW_Status,0)<>2 Then 0 Else 1 End  = 1 '
		Set @sql = @sql + ' And ISNULL(isnull(rd.RDW_FinishDate,''1900-01-01''),'''') = '''' then  ''Haved Delay'' else N'''' end as Delays '		
		Set @sql = @sql + ' From RDW_Mstr rm '
		Set @sql = @sql + ' inner Join RDW_Det rd On rm.RDW_MstrID = rd.RDW_MstrID '
		SET @sql = @sql + ' Left Join RDW_Category ct On ct.cate_id = rm.RDW_Category '
		Set @sql = @sql + ' Left Outer Join '
		Set @sql = @sql + ' ( '
		Set @sql = @sql + '		Select * '
		Set @sql = @sql + '		From '
		Set @sql = @sql + '		( '
		Set @sql = @sql + '			Select Distinct RDW_DetID '
		Set @sql = @sql + '			From RDW_Det_Mbr '
		Set @sql = @sql + '		) rdm '
		Set @sql = @sql + '		Outer Apply '
		Set @sql = @sql + '		( '
		Set @sql = @sql + '			Select [values]=Stuff(Replace(Replace( '
		Set @sql = @sql + '			( '
		Set @sql = @sql + '				Select case rdw_result when 1 then LTrim(Rtrim(RDW_EvaluateName)) else N''*'' + LTrim(Rtrim(RDW_EvaluateName)) end As RDW_EvaluateName FROM RDW_Det_Mbr rdm1 '
		Set @sql = @sql + '				WHERE RDW_DetID = rdm.RDW_DetID '
		Set @sql = @sql + '				FOR XML AUTO '
		Set @sql = @sql + '			), ''<rdm1 RDW_EvaluateName="'', '', ''), ''"/>'', ''''), 1, 1, '''') '
		Set @sql = @sql + '		) As rdm1 '
		Set @sql = @sql + '	) As mbr On rd.RDW_DetID = mbr.RDW_DetID '
		Set @sql = @sql + ' Left Outer Join '
		Set @sql = @sql + ' ( '
		Set @sql = @sql + '		Select * '
		Set @sql = @sql + '		From '
		Set @sql = @sql + '		( '
		Set @sql = @sql + '			Select Distinct RDW_DetID '
		Set @sql = @sql + '			From RDW_Det_Ptr '
		Set @sql = @sql + '		) rdp '
		Set @sql = @sql + '		Outer Apply '
		Set @sql = @sql + '		( '
		Set @sql = @sql + '			Select [values]=Stuff(Replace(Replace( '
		Set @sql = @sql + '			( '
		Set @sql = @sql + '				Select case rdw_result when 1 then LTrim(Rtrim(RDW_PartnerName)) else N''*''+LTrim(Rtrim(RDW_PartnerName)) end  As RDW_PartnerName FROM RDW_Det_Ptr rdp1 '
		Set @sql = @sql + '				WHERE RDW_DetID = rdp.RDW_DetID '
		Set @sql = @sql + '				FOR XML AUTO '
		Set @sql = @sql + '			), ''<rdp1 RDW_PartnerName="'', '', ''), ''"/>'', ''''), 1, 1, '''') '
		Set @sql = @sql + '		) As rdp1 '
		Set @sql = @sql + '	) As ptr On rd.RDW_DetID = ptr.RDW_DetID '
		Set @sql = @sql + ' inner Join tcpc0.dbo.Users u On u.userID = rm.RDW_CreatedBy '
				-- Add SKU
		Set @sql = @sql + '  Left Join SKU_Mstr sku on rm.RDW_ProdSKU = sku.SKU '
		Set @sql = @sql + '  Left Join RDW_Det prd on rd.RDW_Predecessor = prd.RDW_DetID '
		
		IF(@qad<>'')
			BEGIN
				Set @sql = @sql + ' INNER JOIN (SELECT DISTINCT RDW_MstrID  FROM dbo.RDW_QAD qad WHERE qad Like ''%'+@qad+'%'') qad '
				Set @sql = @sql + ' ON rm.RDW_MstrID=qad.RDW_MstrID '
			END 
		--Set @sql = @sql + ' Where rm.RDW_Status = ''' + @status + ''' And Isnull(rd.RDW_isTemp, 0) = 1 '
		Set @sql = @sql + ' Where rm.RDW_Status = ''' + @status + ''''
		
		If(@viewall <> 1)
			Begin
				Set @sql = @sql + ' And (rm.RDW_Partner Like ''%;' + @uid + ';%'' Or rm.RDW_CreatedBy = ''' + @uid + '''  Or rm.RDW_pmID Like ''%;' + @uid + ';%'' Or rm.RDW_MstrID In '
				Set @sql = @sql + ' ( Select Distinct m.RDW_MstrID From RDW_Det_Mbr rdm Inner Join RDW_Det rd On rdm.RDW_DetID = rd.RDW_DetID '
				Set @sql = @sql + ' Inner Join RDW_Mstr m On rd.RDW_MstrID = m.RDW_MstrID Where rdm.RDW_EvaluateID = ''' + @uid + ''' ) Or rm.RDW_MstrID In '
				Set @sql = @sql + ' ( Select Distinct m.RDW_MstrID From RDW_Det_Ptr rdp Inner Join RDW_Det rd On rdp.RDW_DetID = rd.RDW_DetID '
				Set @sql = @sql + ' Inner Join RDW_Mstr m On rd.RDW_MstrID = m.RDW_MstrID Where rdp.RDW_PartnerID = ''' + @uid + ''' )) '
				Set @sql = @sql + ' And IsNull(prd.RDW_Status,0)<>2 And IsNull(rd.RDW_Predecessor,0)>0'
			End
		
		--If(@showall <> 1)
		If(@showall =0)--show expired step
			Begin
				Set @sql = @sql + ' And DateDiff(day, Isnull(rd.RDW_EndDate, getdate()), Isnull(rd.RDW_FinishDate, getdate())) > 0 and rd.RDW_FinishDate is null '--未完成已过期
				Set @sql = @sql + ' And ('
				Set @sql = @sql + '			( IsNull(prd.RDW_Status,0)=2 And IsNull(rd.RDW_Predecessor,0)>0 )'	--有前置步骤且前置步骤已完成且过期
				Set @sql = @sql + '			 Or'
				Set @sql = @sql + '			  IsNull(rd.RDW_Predecessor,0)=0 '--无前置步骤
				Set @sql = @sql + '		)'
				
			End
		Else If(@showall=4)--show No Expired & no completed
			Begin
				Set @sql= @sql + ' And rd.RDW_FinishDate is null And DateDiff(day,Isnull(rd.RDW_EndDate,getdate()),Isnull(rd.RDW_FinishDate,getdate()))<=0'
			End
            	
		If(@proj <> '') Set @sql = @sql + ' And rm.RDW_Project Like N''%' + @proj + '%'' '
			
		If(@prod <> '') Set @sql = @sql + ' And rm.RDW_ProdCode Like N''%' + @prod + '%'' '
		
		If(@start <> '') Set @sql = @sql + ' And rm.RDW_StartDate >= ''' + @start + ''' '
		
		IF(@cateid<>'0') SET @sql = @sql + ' And rm.RDW_Category =  ''' + @cateid + ''' '
		
		If(@sku <> '--') Set @sql = @sql + ' And rm.RDW_ProdSku Like N''%' + @sku + '%'' '
				
        Set @sql = @sql + ' Order By rm.RDW_CreatedDate, rm.RDW_Project, rm.RDW_ProdCode, rd.RDW_Sort '
		Exec (@sql)
	End
