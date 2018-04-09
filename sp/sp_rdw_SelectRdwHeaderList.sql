USE [RD_Workflow]
GO
/****** Object:  StoredProcedure [dbo].[sp_rdw_SelectRdwHeaderList]    Script Date: 06/15/2015 16:57:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ye Bin
-- Create date: 2009/10/27
-- Description:	sp_RDW_SelectRdwHeaderList
--Modify: Wangcaixia 20131119  add select ProdSku
-- =============================================
ALTER Procedure [dbo].[sp_rdw_SelectRdwHeaderList]
 -- select * from RDW_Mstr
(
	@proj nvarchar(100),
	@prod nvarchar(50),
	@sku nvarchar(50),
	@start varchar(10),
	@msg nvarchar(100),
	@status varchar(20),
	@uid varchar(10),
	@all BIT,
	@cateid varchar(6) ='0'
	,@keyword nvarchar(100)
)  
As
Begin
		Declare @sql nvarchar(Max)
		Set @sql = null
		
		Set @sql = ' Select Distinct rm.RDW_MstrID As ID, rm.RDW_Partner As Partner, rm.RDW_ProdSku As ProdSku,'
		Set @sql = @sql + ' rm.RDW_CreatedBy As CreatedBy,  rm.RDW_Remark,'
		Set @sql = @sql + '	ct.cate_name ,isnull(rm.RDW_Project,'''') As Project, rm.RDW_ProdCode As ProdCode, rm.RDW_CreatedDate As CreatedDate, '
	    Set @sql = @sql + ' isnull(cast(u.englishName as nvarchar(50)),u.userName)  As Creater, rm.RDW_ProdDesc As ProdDesc,rm.RDW_StartDate As StartDate, '
	    Set @sql = @sql + ' rm.RDW_EndDate As EndDate, rm.RDW_FinishDate As FinishDate, rm.RDW_Status As Status, rm.RDW_PartnerName  As PartnerName, '
	    Set @sql = @sql + ' rm.RDW_pm As Leader,rm.RDW_Standard As STANDARD,rm.RDW_Memo As Memo,message.RDW_CreateName as LastOper,message.RDW_CreateDate as LastOperDate'
		Set @sql = @sql + ' From RDW_Mstr rm '
		Set @sql = @sql + ' Inner Join tcpc0.dbo.Users u On u.userID = rm.RDW_CreatedBy '
		SET @sql = @sql + ' Left Join RDW_Category ct On ct.cate_id = rm.RDW_Category '
		SET @sql = @sql + ' Left Join( '
		SET @sql = @sql + ' SELECT ROW_NUMBER() OVER(PARTITION BY m.RDW_MstrID ORDER BY dm.RDW_CreateDate DESC) AS id,
							m.RDW_MstrID,dm.RDW_CreateName,dm.RDW_CreateDate 
							FROM dbo.RDW_Det_Message dm
							INNER JOIN dbo.RDW_Det d
							ON dm.RDW_DetID=d.RDW_DetID
							INNER JOIN dbo.RDW_Mstr m
							ON d.RDW_MstrID=m.RDW_MstrID) message '
		SET @sql = @sql + ' ON rm.RDW_MstrID=message.RDW_MstrID And message.id=1'
		If(@msg <> '')
			Begin
				Set @sql = @sql + ' Left Outer Join RDW_Det rd On rd.RDW_MstrID = rm.RDW_MstrID '
				Set @sql = @sql + ' Left Outer Join RDW_Det_Message rdm On rdm.RDW_DetID = rd.RDW_DetID '
			End
		Set @sql = @sql + ' Where rm.RDW_Status = ''' + @status + ''' '
		If(@all <> 1)  
			Begin
				Set @sql = @sql + ' And (rm.RDW_Partner Like ''%;' + @uid + ';%'' Or rm.RDW_CreatedBy = ''' + @uid + ''' Or   '
				Set @sql = @sql + '  rm.RDW_pmID Like ''%;' + @uid + ';%''  Or rm.RDW_MstrID In '
				Set @sql = @sql + ' (Select Distinct m.RDW_MstrID From RDW_Det_Mbr rdm Inner Join RDW_Det rd On rdm.RDW_DetID = rd.RDW_DetID '
				Set @sql = @sql + ' Inner Join RDW_Mstr m On rd.RDW_MstrID = m.RDW_MstrID Where rdm.RDW_EvaluateID = ''' + @uid + ''') Or rm.RDW_MstrID In '
				Set @sql = @sql + ' (Select Distinct m.RDW_MstrID From RDW_Det_Ptr rdp Inner Join RDW_Det rd On rdp.RDW_DetID = rd.RDW_DetID '
				Set @sql = @sql + ' Inner Join RDW_Mstr m On rd.RDW_MstrID = m.RDW_MstrID Where rdp.RDW_PartnerID = ''' + @uid + ''')) '
			End
		/*
		--If(@uid <> '0')
		--	Begin
				Set @sql = @sql + ' And (rm.RDW_Partner Like ''%;' + @uid + ';%'' Or rm.RDW_CreatedBy = ''' + @uid + ''' Or ''' + @uid + ''' In '
				Set @sql = @sql + ' (Select Distinct RDW_EvaluateID From RDW_Det_Mbr Where RDW_DetID In (Select RDW_DetID From RDW_Mstr))) '
		--	End
		*/
		
		If(@proj <> '') Set @sql = @sql + ' And rm.RDW_Project Like N''%' + @proj + '%'' '
			
		If(@prod <> '') Set @sql = @sql + ' And (rm.RDW_ProdCode Like N''%' + @prod + '%'' or rm.RDW_prodcode_old Like N''%' + @prod + '%'') '
		
		If(@sku <> '--') Set @sql = @sql + ' And rm.RDW_ProdSku Like N''%' + @sku + '%'' '
		
		If(@start <> '') Set @sql = @sql + ' And rm.RDW_StartDate >= ''' + @start + ''' '
		
		If(@msg <> '') Set @sql = @sql + ' And rdm.RDW_Message Like N''%' + @msg + '%'' '
		
		IF(@cateid <> '0') SET @sql = @sql + ' And rm.RDW_Category =  ''' + @cateid + ''' '
		
		IF(@keyword <> '')
			Begin
			    SET @sql = @sql + ' And rm.RDW_MstrID in (select RDW_MstrID from RDW_Mstr where RDW_ProdDesc Like N''%' + @keyword + '%'' '
			    SET @sql = @sql + ' Union All select RDW_MstrID from RDW_Mstr where RDW_Memo Like N''%' + @keyword + '%'' '
			    SET @sql = @sql + ' Union All select RDW_MstrID from RDW_Mstr where RDW_Standard Like N''%' + @keyword + '%'' '
			    SET @sql = @sql + ' Union All Select RDW_MstrID From RDW_QAD Where Qad = ''' + @keyword + '''  '
			    SET @sql = @sql + ' Union All Select d.RDW_MstrID From RDW_Det d inner join RDW_Det_Docs dd on d.RDW_detID=dd.RDW_detID Where dd.RDW_name Like N''%' + @keyword + '%'' '
			    SET @sql = @sql + ' Union All Select d.RDW_MstrID From RDW_Det d inner join RDW_Det_Message dm on d.RDW_detID=dm.RDW_detID Where dm.RDW_Message Like N''%' + @keyword + '%'')'		               
			END 
		exec  (@sql)
	End




