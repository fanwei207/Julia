USE [RD_Workflow]
GO
/****** Object:  StoredProcedure [dbo].[sp_RDW_SelectProjectByTemplates]    Script Date: 2016/3/15 16:31:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		mabo
-- Create date: 2015-11-23
-- Description:	Select Project Steps 
-- =============================================
ALTER PROCEDURE [dbo].[sp_RDW_SelectProjectByTemplates] 

	@prod nvarchar(100)
	,@prodcode nvarchar(50)
	,@start varchar(10)
	,@status varchar(20)
	,@cateid varchar(6) ='0'
	,@region varchar(15)
	,@plantCode INT
    ,@mstrId UNIQUEIDENTIFIER    
	,@LampType nvarchar(100) = ''
   
AS

BEGIN 	
	Declare @sql nvarchar(max)
	DECLARE @Node VARCHAR(max)
	IF(@plantCode <> 99 )
	Begin

		Select  rmstr.RDW_LampType [Type]
		,rmstr.RDW_EngineerTeam [Engineering Team]
		,rmstr.RDW_priority [Priority]
		,RDW_Project ppa
		,RDW_ProdCode LNA
		,RDW_ProdDesc [Description]
		,RDW_Comments [Comments / Status]
		,ppa_requestorPMName [Product Manager]
		,[ppa_requestorR&DName] [R&DProjectLeader]
		,ppa_rpaDate [Required Availability Date]
		,ppa_prodCost [Target Cost FOB Shanghai]
		,rmstr.RDW_EStarDLC [ESTAR / DLC]
		,pvt.[ANSI Shape ANSI形状] ANSI
		,pvt.[Voltage (+/-)   电压范围（+/-）] Voltage
		,pvt.[Power Factor  功率因数] [Power Factor]
		,pvt.[DIM/ND 调光不调光] [Dim / ND]
		,pvt.[Base Type(s)   底座类型] [Base Type]
		,pvt.[Lumens   流明] Lumens
		,pvt.[CBCP (PAR & MR only) 中心光强（只针对PAR灯和MR灯）] CBCP
		,pvt.[Light Distribution 光分布] [Light Distribution]
		,pvt.[CCT Tolerance 色温容差] MinCCT
		,pvt.[CRI 显色指数] MinCRI
		,pmstr.ppa_keyCustomer Customer
		,c.cate_region Region
		,rmstr.RDW_Status [Status]
				,step.RDW_Code Code
		,Case When ISNULL(det.RDW_Status,0)= 2  then SUBSTRING(CONVERT(VARCHAR(10),det.RDW_FinishDate,112),3,6) 
		When ISNULL(det.RDW_Status,0) = 0 And det.RDW_DetID is null Then 'N/A'
		When ISNULL(det.RDW_Status,0) = 0 And det.RDW_DetID is not null 
		And ((ISNULL(step.RDW_IsApprove,0)=0 AND  DATEADD(day,1,det.RDW_EndDate) < GETDATE()) 
		OR (ISNULL(step.RDW_IsApprove,0)=1 AND DATEADD(day,1,CASE WHEN ISNULL(det.RDW_Predecessor,0)=0 THEN lastDet.RDW_FinishDate ELSE preDet.RDW_FinishDate END) < GETDATE()))  then '-EXPIRE'  Else '' End prodStatus
	Into #tb1
	FROM (SELECT *
		From RDW_Mstr mstr
		WHERE EXISTS(SELECT * FROM dbo.RDW_Det det INNER JOIN dbo.Rdw_StandardStep ss ON det.RDW_Code=ss.RDW_Code WHERE ss.RDW_MstrId=@mstrId AND det.RDW_MstrID=mstr.RDW_MstrID)
		) rmstr
	left join ppa_mstr pmstr on pmstr.ppa_mstrID = rmstr.RDW_PPAMstrID
	--left join ppa_det det on pmstr.ppa_mstrID = det.ppa_mstrID
	left join RDW_Category c ON c.cate_id = rmstr.RDW_Category
	left join (
			select ppa_mstrID,[5ea8f10b-afb5-49b6-b02c-2ea6d034cc05] [CCT Tolerance 色温容差],[7970000a-93e8-46a1-a2a6-bfb548e4a8c2] [CRI 显色指数]
			   ,[d438d0ed-4d3f-4b46-a5fb-cb9bcc35f307] [Lumens   流明],[ce7c490e-6aca-45e1-8eba-a144a8947aa4] [Light Distribution 光分布]
			   ,[edb231a7-7f89-4493-8cef-823cbaf25a04] [CBCP (PAR & MR only) 中心光强（只针对PAR灯和MR灯）]
			   ,[e5da5fe7-7106-4a78-911c-90c7111824f7] [Base Type(s)   底座类型],[0f5798fe-0ee6-4e90-96a5-c436233f648f] [DIM/ND 调光不调光]
			   ,[35e3c77a-d4e5-48d9-a90d-a5510f1b2154] [Power Factor  功率因数],[85f5ca01-2b3e-4cd9-b45c-79969894764c] [Voltage (+/-)   电压范围（+/-）]
			   ,[304C5116-4FFB-40E3-9652-39AEE4192EAF] [ANSI Shape ANSI形状]
	from (
	select ppa_mstrID,ppa_metricID,isnull(ppa_target,'') as ppa_target
			from ppa_det
			) p PIVOT
			(max([ppa_target]) FOR [ppa_metricID] in (
						[5ea8f10b-afb5-49b6-b02c-2ea6d034cc05],[7970000a-93e8-46a1-a2a6-bfb548e4a8c2],[d438d0ed-4d3f-4b46-a5fb-cb9bcc35f307],[ce7c490e-6aca-45e1-8eba-a144a8947aa4]
					   ,[edb231a7-7f89-4493-8cef-823cbaf25a04],[e5da5fe7-7106-4a78-911c-90c7111824f7],[0f5798fe-0ee6-4e90-96a5-c436233f648f]
					   ,[35e3c77a-d4e5-48d9-a90d-a5510f1b2154],[85f5ca01-2b3e-4cd9-b45c-79969894764c],[304C5116-4FFB-40E3-9652-39AEE4192EAF]
					   )) as pvta
				) pvt
				 on pvt.ppa_mstrID = pmstr.ppa_mstrID
    cross join (select * FROM RDW_StandardStep WHERE ISNULL(RDW_Delete,0)=0 AND RDW_MstrId=@mstrId) step 
		Left join RDW_Category cate
		On cate.cate_id = rmstr.RDW_Category
		Left Join RDW_Det det 
		On rmstr.RDW_MstrID = det.RDW_MstrID and step.RDW_Code = det.RDW_Code
		LEFT JOIN dbo.RDW_Det preDet
		ON det.RDW_MstrID=preDet.RDW_MstrID AND det.RDW_Predecessor=preDet.RDW_DetID
		LEFT JOIN dbo.RDW_Det lastDet
		ON det.RDW_MstrID=lastDet.RDW_MstrID AND CAST(CAST(SUBSTRING(det.RDW_TaskID,0,CHARINDEX('.',det.RDW_TaskID,0)) AS DECIMAL)-1 AS VARCHAR(5))+'.'=lastDet.RDW_TaskID

	Where ( @prod = '' OR ( @prod <> '' And rmstr.RDW_Project = @prod))
		And ( @status = '--' OR( @status <> '--' And rmstr.RDW_Status = @status) )
		And ( @start = '' OR( @start <> '' And rmstr.RDW_StartDate >= @start) )
		And ( @prodcode = '' OR( @prodcode <> '' And rmstr.RDW_ProdCode = @prodcode) )
		And ( @cateid = '0' OR( @cateid <> '0' And rmstr.RDW_Category = @cateid) )
		And ( @region = '--' OR (@region <> '--' And isnull(cate.cate_region,'') = @region) )
		AND (@LampType='' OR(@LampType<>'' AND @LampType = rmstr.RDW_LampType))
	Order By step.RDW_Sort asc		
	
		Set @Node = STUFF(( Select ',' + Quotename(RDW_Code) From Rdw_StandardStep  WHERE ISNULL(RDW_Delete,0)=0 AND RDW_MstrId=@mstrId order by RDW_Sort FOR XML PATH('')),1,1,'')
	
		Set @sql = ' select * From #tb1 Pivot( MAX(prodStatus) for Code in ( ' +  @Node + ')) tb1'
		exec (@sql)
		drop table #tb1














		--Select 
		--cate.cate_name,mstr.RDW_Project prodName,mstr.RDW_ProdCode prodCode,mstr.RDW_ProdDesc prodDesc
		--,mstr.RDW_EndDate,SUBSTRING(mstr.RDW_MgrName,2,LEN(mstr.RDW_MgrName)-2) AS RDW_MgrName,'' AS RDW_Leader
		--,step.RDW_Code Code
		--,convert(varchar(10),mstr.RDW_StartDate,120) RDW_StartDate,mstr.RDW_Status
		--,Case When ISNULL(det.RDW_Status,0)= 2  then SUBSTRING(CONVERT(VARCHAR(10),det.RDW_FinishDate,112),3,6) 
		--When ISNULL(det.RDW_Status,0) = 0 And det.RDW_DetID is null Then 'N/A'
		--When ISNULL(det.RDW_Status,0) = 0 And det.RDW_DetID is not null 
		--And ((ISNULL(step.RDW_IsApprove,0)=0 AND  DATEADD(day,1,det.RDW_EndDate) < GETDATE()) 
		--OR (ISNULL(step.RDW_IsApprove,0)=1 AND DATEADD(day,1,CASE WHEN ISNULL(det.RDW_Predecessor,0)=0 THEN lastDet.RDW_FinishDate ELSE preDet.RDW_FinishDate END) < GETDATE()))  then '-EXPIRE'  Else '' End prodStatus
		--Into #tb1
		--From RDW_Mstr mstr
		--cross join (select * FROM RDW_StandardStep WHERE ISNULL(RDW_Delete,0)=0) step 
		--Left join RDW_Category cate
		--On cate.cate_id = mstr.RDW_Category
		--Left Join RDW_Det det 
		--On mstr.RDW_MstrID = det.RDW_MstrID and step.RDW_Code = det.RDW_Code
		--LEFT JOIN dbo.RDW_Det preDet
		--ON det.RDW_MstrID=preDet.RDW_MstrID AND det.RDW_Predecessor=preDet.RDW_DetID
		--LEFT JOIN dbo.RDW_Det lastDet
		--ON det.RDW_MstrID=lastDet.RDW_MstrID AND CAST(CAST(SUBSTRING(det.RDW_TaskID,0,CHARINDEX('.',det.RDW_TaskID,0)) AS DECIMAL)-1 AS VARCHAR(5))+'.'=lastDet.RDW_TaskID
		--Where ( @prod = '' OR ( @prod <> '' And mstr.RDW_Project = @prod))
		--And ( @status = '--' OR( @status <> '--' And mstr.RDW_Status = @status) )
		--And ( @start = '' OR( @start <> '' And mstr.RDW_StartDate >= @start) )
		--And ( @prodcode = '' OR( @prodcode <> '' And mstr.RDW_ProdCode = @prodcode) )
		--And ( @cateid = '0' OR( @cateid <> '0' And mstr.RDW_Category = @cateid) )
		--And ( @region = '--' OR (@region <> '--' And isnull(cate.cate_region,'') = @region) )

		--Order By step.RDW_Sort asc		
	
		--Set @Node = STUFF(( Select ',' + Quotename(RDW_Code) From Rdw_StandardStep WHERE ISNULL(RDW_Delete,0)=0 order by RDW_Sort FOR XML PATH('')),1,1,'')
	
		--Set @sql = ' select * From #tb1 Pivot( MAX(prodStatus) for Code in ( ' +  @Node + ')) tb1'
		--exec (@sql)
		--drop table #tb1
	End
	Else
	Begin
		Select  rmstr.RDW_LampType [Type]
		,rmstr.RDW_EngineerTeam [Engineering Team]
		,rmstr.RDW_priority [Priority]
		,RDW_Project ppa
		,RDW_ProdCode LNA
		,RDW_ProdDesc [Description]
		,RDW_Comments [Comments / Status]
		,ppa_requestorPMName [Product Manager]
		,[ppa_requestorR&DName] [R&DProjectLeader]
        ,ppa_rpaDate [Required Availability Date]
		,ppa_prodCost [Target Cost FOB Shanghai]
		,rmstr.RDW_EStarDLC [ESTAR / DLC]
		,pvt.[ANSI Shape ANSI形状] ANSI
		,pvt.[Voltage (+/-)   电压范围（+/-）] Voltage
		,pvt.[Power Factor  功率因数] [Power Factor]
		,pvt.[DIM/ND 调光不调光] [Dim / ND]
		,pvt.[Base Type(s)   底座类型] [Base Type]
		,pvt.[Lumens   流明] Lumens
		,pvt.[CBCP (PAR & MR only) 中心光强（只针对PAR灯和MR灯）] CBCP
		,pvt.[Light Distribution 光分布] [Light Distribution]
		,pvt.[CCT Tolerance 色温容差] MinCCT
		,pvt.[CRI 显色指数] MinCRI
		,'' Customer
		,c.cate_region Region
		,rmstr.RDW_Status [Status]
				,step.RDW_Code Code
		,Case When ISNULL(det.RDW_Status,0)= 2  then SUBSTRING(CONVERT(VARCHAR(10),det.RDW_FinishDate,112),3,6) 
		When ISNULL(det.RDW_Status,0) = 0 And det.RDW_DetID is null Then 'N/A'
		When ISNULL(det.RDW_Status,0) = 0 And det.RDW_DetID is not null 
		And ((ISNULL(step.RDW_IsApprove,0)=0 AND  DATEADD(day,1,det.RDW_EndDate) < GETDATE()) 
		OR (ISNULL(step.RDW_IsApprove,0)=1 AND DATEADD(day,1,CASE WHEN ISNULL(det.RDW_Predecessor,0)=0 THEN lastDet.RDW_FinishDate ELSE preDet.RDW_FinishDate END) < GETDATE()))  then '-EXPIRE'  Else '' End prodStatus
	Into #tb2
	FROM (SELECT *
		From RDW_Mstr mstr
		WHERE EXISTS(SELECT * FROM dbo.RDW_Det det INNER JOIN dbo.Rdw_StandardStep ss ON det.RDW_Code=ss.RDW_Code WHERE ss.RDW_MstrId=@mstrId AND det.RDW_MstrID=mstr.RDW_MstrID AND ss.RDW_MstrId=@mstrId)
		) rmstr
	left join ppa_mstr pmstr on pmstr.ppa_projIdentifier = rmstr.RDW_Project
	--left join ppa_det det on pmstr.ppa_mstrID = det.ppa_mstrID
	left join RDW_Category c ON c.cate_id = rmstr.RDW_Category
	left join (
			select ppa_mstrID,[5ea8f10b-afb5-49b6-b02c-2ea6d034cc05] [CCT Tolerance 色温容差],[7970000a-93e8-46a1-a2a6-bfb548e4a8c2] [CRI 显色指数]
			   ,[d438d0ed-4d3f-4b46-a5fb-cb9bcc35f307] [Lumens   流明],[ce7c490e-6aca-45e1-8eba-a144a8947aa4] [Light Distribution 光分布]
			   ,[edb231a7-7f89-4493-8cef-823cbaf25a04] [CBCP (PAR & MR only) 中心光强（只针对PAR灯和MR灯）]
			   ,[e5da5fe7-7106-4a78-911c-90c7111824f7] [Base Type(s)   底座类型],[0f5798fe-0ee6-4e90-96a5-c436233f648f] [DIM/ND 调光不调光]
			   ,[35e3c77a-d4e5-48d9-a90d-a5510f1b2154] [Power Factor  功率因数],[85f5ca01-2b3e-4cd9-b45c-79969894764c] [Voltage (+/-)   电压范围（+/-）]
			   ,[304C5116-4FFB-40E3-9652-39AEE4192EAF] [ANSI Shape ANSI形状]
	from (
	select ppa_mstrID,ppa_metricID,isnull(ppa_target,'') as ppa_target
			from ppa_det
			) p PIVOT
			(max([ppa_target]) FOR [ppa_metricID] in (
						[5ea8f10b-afb5-49b6-b02c-2ea6d034cc05],[7970000a-93e8-46a1-a2a6-bfb548e4a8c2],[d438d0ed-4d3f-4b46-a5fb-cb9bcc35f307],[ce7c490e-6aca-45e1-8eba-a144a8947aa4]
					   ,[edb231a7-7f89-4493-8cef-823cbaf25a04],[e5da5fe7-7106-4a78-911c-90c7111824f7],[0f5798fe-0ee6-4e90-96a5-c436233f648f]
					   ,[35e3c77a-d4e5-48d9-a90d-a5510f1b2154],[85f5ca01-2b3e-4cd9-b45c-79969894764c],[304C5116-4FFB-40E3-9652-39AEE4192EAF]
					   )) as pvta
				) pvt
				 on pvt.ppa_mstrID = pmstr.ppa_mstrID
    cross join (select * FROM RDW_StandardStep WHERE ISNULL(RDW_Delete,0)=0 AND RDW_MstrId=@mstrId) step 
		Left join RDW_Category cate
		On cate.cate_id = rmstr.RDW_Category
		Left Join RDW_Det det 
		On rmstr.RDW_MstrID = det.RDW_MstrID and step.RDW_Code = det.RDW_Code
		LEFT JOIN dbo.RDW_Det preDet
		ON det.RDW_MstrID=preDet.RDW_MstrID AND det.RDW_Predecessor=preDet.RDW_DetID
		LEFT JOIN dbo.RDW_Det lastDet
		ON det.RDW_MstrID=lastDet.RDW_MstrID AND CAST(CAST(SUBSTRING(det.RDW_TaskID,0,CHARINDEX('.',det.RDW_TaskID,0)) AS DECIMAL)-1 AS VARCHAR(5))+'.'=lastDet.RDW_TaskID

	Where ( @prod = '' OR ( @prod <> '' And rmstr.RDW_Project = @prod))
		And ( @status = '--' OR( @status <> '--' And rmstr.RDW_Status = @status) )
		And ( @start = '' OR( @start <> '' And rmstr.RDW_StartDate >= @start) )
		And ( @prodcode = '' OR( @prodcode <> '' And rmstr.RDW_ProdCode = @prodcode) )
		And ( @cateid = '0' OR( @cateid <> '0' And rmstr.RDW_Category = @cateid) )
		And ( @region = '--' OR (@region <> '--' And isnull(cate.cate_region,'') = @region) )
		And ( rmstr.RDW_Category <> 147 )
		AND (@LampType='' OR(@LampType<>'' AND @LampType = rmstr.RDW_LampType))
	Order By step.RDW_Sort asc		
	
		Set @Node = STUFF(( Select ',' + Quotename(RDW_Code) From Rdw_StandardStep WHERE ISNULL(RDW_Delete,0)=0 AND RDW_MstrId=@mstrId order by RDW_Sort FOR XML PATH('')),1,1,'')
	
		Set @sql = ' select * From #tb2 Pivot( MAX(prodStatus) for Code in ( ' +  @Node + ')) tb1'
		exec (@sql)
		drop table #tb2






		--Select 
		--cate.cate_name,mstr.RDW_Project prodName,mstr.RDW_ProdCode prodCode,mstr.RDW_ProdDesc prodDesc
		--,mstr.RDW_EndDate,SUBSTRING(mstr.RDW_MgrName,2,LEN(mstr.RDW_MgrName)-2) AS RDW_MgrName,'' AS RDW_Leader
		--,step.RDW_Code Code
		--,convert(varchar(10),mstr.RDW_StartDate,120) RDW_StartDate,mstr.RDW_Status
		--,Case When ISNULL(det.RDW_Status,0)= 2  then SUBSTRING(CONVERT(VARCHAR(10),det.RDW_FinishDate,112),3,6) 
		--When ISNULL(det.RDW_Status,0) = 0 And det.RDW_DetID is null Then 'N/A'
		--When ISNULL(det.RDW_Status,0) = 0 And det.RDW_DetID is not null 
		--And ((ISNULL(step.RDW_IsApprove,0)=0 AND  DATEADD(day,1,det.RDW_EndDate) < GETDATE()) 
		--OR (ISNULL(step.RDW_IsApprove,0)=1 AND DATEADD(day,1,CASE WHEN ISNULL(det.RDW_Predecessor,0)=0 THEN lastDet.RDW_FinishDate ELSE preDet.RDW_FinishDate END) < GETDATE()))  then '-EXPIRE'  Else '' End prodStatus
		--Into #tb2
		--From RDW_Mstr mstr
		--cross join (select * FROM RDW_StandardStep WHERE ISNULL(RDW_Delete,0)=0) step 
		--Left join RDW_Category cate
		--On cate.cate_id = mstr.RDW_Category
		--Left Join RDW_Det det 
		--On mstr.RDW_MstrID = det.RDW_MstrID and step.RDW_Code = det.RDW_Code
		--LEFT JOIN dbo.RDW_Det preDet
		--ON det.RDW_MstrID=preDet.RDW_MstrID AND det.RDW_Predecessor=preDet.RDW_DetID
		--LEFT JOIN dbo.RDW_Det lastDet
		--ON det.RDW_MstrID=lastDet.RDW_MstrID AND CAST(CAST(SUBSTRING(det.RDW_TaskID,0,CHARINDEX('.',det.RDW_TaskID,0)) AS DECIMAL)-1 AS VARCHAR(5))+'.'=lastDet.RDW_TaskID
		--Where ( @prod = '' OR ( @prod <> '' And mstr.RDW_Project = @prod))
		--And ( @status = '--' OR( @status <> '--' And mstr.RDW_Status = @status) )
		--And ( @start = '' OR( @start <> '' And mstr.RDW_StartDate >= @start) )
		--And ( @prodcode = '' OR( @prodcode <> '' And mstr.RDW_ProdCode = @prodcode) )
		--And ( @cateid = '0' OR( @cateid <> '0' And mstr.RDW_Category = @cateid) )
		--And ( @region = '--' OR (@region <> '--' And isnull(cate.cate_region,'') = @region) )
		--And ( mstr.RDW_Category <> 147 )

		--Order By step.RDW_Sort asc
	
		--Set @Node = STUFF(( Select ',' + Quotename(RDW_Code) From Rdw_StandardStep WHERE ISNULL(RDW_Delete,0)=0 order by RDW_Sort FOR XML PATH('')),1,1,'')
	
		--Set @sql = ' select * From #tb2 Pivot( MAX(prodStatus) for Code in ( ' +  @Node + ')) tb2'
		--exec (@sql)
		--drop table #tb2
	End
END





