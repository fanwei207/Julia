USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_NPart_exportMasterListExcelFromPage]    Script Date: 2018/2/1 16:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<lyf>
-- Create date: <2018.1.25>
-- Description:	<show npart master list>
-- =============================================
ALTER PROCEDURE [dbo].[sp_NPart_exportMasterListExcelFromPage] --exec sp_NPart_exportMasterListExcelFromPage '0',N'',N'',6
@moduleNo nvarchar(50),
@applyNo nvarchar(50),
@qad nvarchar(50),
@status nvarchar(50)
AS
BEGIN
	declare @sql nvarchar(max)
	set @sql=N''
	DECLARE @countTable TABLE (
				num NVARCHAR(10),prefix NVARCHAR(50),srefix NVARCHAR(50),isSpace BIT
		)

   	declare @nodeID nvarchar(50)
	set @nodeID = ''
	set @nodeID = case when @status= 2 then 'f98889e9-fdb5-4ad3-9218-07b169e760d6'
	when @status = 3 then '065c05bc-0b1d-4795-a1a4-243a8271afe2'
	when @status = 4 then 'b49ca5f4-65c3-4ce3-92ef-3aa5a3fe22a1'
	when @status = 5 then '9d5267d2-1704-402e-bf80-ed8a74a404ee'
	when @status = 6 then '1f630d71-ff1a-4567-ac55-8a44942ac702'
	end

	--select @nodeID

	

	declare m_cursor cursor FOR

	select partType 
	from Npart_partApply
	where (
			@moduleNo =  '0' 
			or 
				(
					@moduleNo <> '0' and cast(partType as nvarchar(50))=@moduleNo
				)
			) 
	GROUP BY partType

	open m_cursor
	declare @mstrID nvarchar(50)

	fetch next from m_cursor into @mstrID
	while @@FETCH_STATUS = 0
	begin

		INSERT INTO @countTable
			( num ,prefix,srefix,isSpace)
		SELECT N'col'+CAST(a.colNum AS NVARCHAR(50)),a.colPrefix,a.colSuffix,a.colSpace
		FROM (
		select colPrefix,colSuffix,colSpace,ROW_NUMBER()OVER (ORDER BY colSort) colNum  from Npart_partTypeDet
		where cast(partTypeID as nvarchar(50))=@mstrID
		and colEnglishName not in (select partStationaryCol from Npart_partTypeDetStationaryCol)
	
		) AS a
		ORDER BY colNum

		--PRINT 1
		---print @nodeID
	
		SET @sql = @sql+N'UNION 
	    '
		SET @sql = @sql +'
		select  partApplyCode as 申请号,createdName as 申请者
		,convert(nvarchar(10),createdDate,120) as 申请日期,partTypeForQAD+ N'' '' +' 
		SET @sql = @sql + STUFF( (SELECT N'+ N'''+ rtrim(ltrim(ISNULL(prefix,N''))) +N'''+  isnull('+ num + N',N'''') + N'''+ rtrim(ltrim(ISNULL(srefix,N''))) +N'''+ case when ' + CAST(isSpace AS NVARCHAR(5)) + N' = 1 then  N'' '' else N'''' end   '  FROM @countTable FOR XML PATH('')),1,1,N'')   
		SET @sql = @sql + N' as 描述,
		appvResult as 是否同意,appvResultReason as 拒绝原因,part as QAD,um as 单位,addReason as 新增原因,oldPart as 原QAD,Manufacturer1 as 原厂1,Model1 as 型号1
		,Manufacturer2 as 原厂2,Model2 as 型号2,Manufacturer3 as 原厂3,Model3 as 型号3,MPQ as 最小包装量,MOQ as 最小订单量,vend as 供应商,leadtime as 采购周期
		 from Npart_partApply  npa
		where ('''+@mstrID+''' = cast(partType as nvarchar(50))
		and ('''+@applyNo+''' = N'''' or ('''+@applyNo+''' <> N'''' and '''+@applyNo+''' = partApplyCode))
		and ('''+@qad +'''= N'''' or ('''+@qad+''' <> N'''' and '''+@qad+''' = part))
		and ('''+@status+''' = 1 or ('''+@status+''' <> 1 and '''+@status+''' = status)
		or('''+@status+''' <> 1 and appvResult = 1 and status = 10  and '''+@status+''' = ''7'')))
		'

		

		SET @sql = @sql+N'UNION 
	    '
		SET @sql = @sql +'
		select  partApplyCodeas 申请号,createdName as 申请者,convert(nvarchar(10),createdDate,120)  as 申请日期,
		partTypeForQAD+ N'' '' +' 

		SET @sql = @sql + STUFF( (SELECT N'+ N'''+ rtrim(ltrim(ISNULL(prefix,N''))) +N'''+  isnull('+ num + N',N'''') + N'''+ rtrim(ltrim(ISNULL(srefix,N''))) +N'''+ case when ' + CAST(isSpace AS NVARCHAR(5)) + N' = 1 then  N'' '' else N'''' end   '  FROM @countTable FOR XML PATH('')),1,1,N'')
		
		
		SET @sql = @sql + N' as 描述,' 
		SET @sql = @sql + N' appvResult as 是否同意,appvResultReason as 拒绝原因,part as QAD,um as 单位,addReason as 新增原因,oldPart as 原QAD,Manufacturer1 as 原厂1,Model1 as 型号1
		,Manufacturer2 as 原厂2,Model2 as 型号2,Manufacturer3 as 原厂3,Model3 as 型号3,MPQ as 最小包装量,MOQ as 最小订单量,vend as 供应商,leadtime as 采购周期
						'
		SET @sql = @sql + N' from Npart_partApply  npa 
		'
		SET @sql = @sql + N' INNER JOIN WorkFlow.dbo.NWF_WorkFlowInstance wfi ON wfi.SourceId = npa.ID 
		'

	

		SET @sql = @sql + N' AND npa.status = 10
		'
		SET @sql = @sql + N' AND wfi.WFI_Status = 1
		INNER JOIN WorkFlow.dbo.NWF_FlowNodeInstance fni ON wfi.WFI_Id = fni.WFI_Id
		AND fni.FNI_Status = 1
		'
		SET @sql = @sql + N' WHERE '''+@mstrID+''' = cast(partType as nvarchar(50))
		'
		

		
		SET @sql = @sql + N' and ('''+@applyNo+''' = N'''' or ('''+@applyNo+''' <> N'''' and '''+@applyNo+''' = npa.partApplyCode))
		'
		

		SET @sql = @sql + N' and ('''+@qad +'''= N'''' or ('''+@qad+''' <> N'''' and '''+@qad+''' = npa.part))
		'
		

		
		SET @sql = @sql + N' and cast(fni.Node_Id as nvarchar(50)) = '''+ISNULL(@nodeID,N'')+'''
		'


		SET   @mstrID = NULL
		delete from @countTable 
		
		fetch next from m_cursor into @mstrID
	end
	close m_cursor
	deallocate m_cursor
	SET @sql = STUFF(@sql,1,5,N'')
	EXEC(@sql)
END
