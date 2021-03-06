USE [RD_Workflow]
GO
/****** Object:  StoredProcedure [dbo].[sp_RDW_InsertRDWHeader]    Script Date: 07/08/2015 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ye Bin
-- Create date: 2009/10/27
-- Description:	sp_RDW_InsertRDWHeader
-- Modify: wangcaixia 2013-11-11 add @sku,  代替原来的@code，@code为产品立项表编号
-- Modify：wangcaixia 2014-01-22 因新建项目套用模板，步骤顺序打乱，不再从模板步骤成员套用到项目步骤中付出
-- Modified by: wangcaixia 2014-04-09 ,因项目Code按项目分类自增
-- MODIFYBY lIUJUNHONG
-- =============================================
ALTER PROCEDURE [dbo].[sp_RDW_InsertRDWHeader]
(
	@proj nvarchar(100)
	, @code nvarchar(50)= ''
	, @desc nvarchar(250)
	, @sku nvarchar(50)
	, @start varchar(10)
	, @end varchar(10)
	, @stand nvarchar(500)
	, @memo nvarchar(200)
	, @template bigint
	, @uid varchar(10)
	, @cate int = 0
)
As
/*
Exec sp_RDW_InsertRDWHeader
	@proj = 'PRO-30-12-2011-01'
	, @code = 'SKU-28-12-2011-1'
	, @desc = N'aa'
	, @sku = '9999'
	, @start = '2011-12-1'
	, @end = '2012-2-1'
	, @stand = N'bb'
	, @memo = N'cc'
	, @template = 37
	, @uid = 1
*/
	Declare @err bigint
	Set @err = 0
	
	Declare @mid bigint
	Set @mid = 0
      

	Begin Transaction
		If(Not Exists(Select * From RDW_Mstr Where RDW_Project = @proj))
			Begin
				--先清空RDW_Mstr
				Update RDW_Mstr
				Set RDW_T_UserID = Null, RDW_T_MstrID = null
				Where RDW_T_UserID = @uid
				If(@@error <> 0) Set @err = -1

				Insert Into RDW_Mstr(RDW_Project, RDW_Category, RDW_ProdCode, RDW_ProdDesc, RDW_Standard, RDW_ProdSKU, RDW_StartDate, RDW_EndDate, RDW_Memo, RDW_Status, RDW_CreatedBy, RDW_CreatedDate, RDW_Template, RDW_T_UserID, RDW_T_MstrID)
				Values(@proj, @cate, @code, @desc, @stand, @sku,  @start, @end, @memo, 'PROCESS', @uid, getdate(), @template, @uid, @template)
				
				If(@@error <> 0) Set @err = -1
				Else 
				Begin
				Set @mid = @@Identity 
				Update RDW_Category 
				set cate_ProjCode = Right('0000000000'+Convert(varchar(10), Cast(cate_ProjCode as int) + 1),LEN(cate_ProjCode))
				where cate_id = @cate
				End
				 		

				If(Isnull(@template, 0) <> 0)
				Begin
					If Exists(Select * From RDW_TepmlateMstr Where RDW_MstrID = @template)
					Begin
						--使用中间变量RDW_T_UserID和RDW_T_StepID后，就不必要用游标了
						--具体的原理是将RDW_TepmlateMstr、RDW_Step和RDW_Step_Mbr悉数插入对应表，但RDW_T_UserID和RDW_T_StepID
						--两个字段保留有原先的对应关系，所以，只要最后再根据彼此关系做一次批量更新就可以了

						--再清空RDW_Det
						Update RDW_Det
						Set RDW_T_UserID = Null, RDW_T_StepID = null
						Where RDW_T_UserID = @uid
						If(@@error <> 0) Set @err = -2

						--再清空RDW_Det_Mbr
						Update RDW_Det_Mbr
						Set RDW_T_UserID = Null, RDW_T_MemberID = null
						Where RDW_T_UserID = @uid
						If(@@error <> 0) Set @err = -3

						--顺序插入记录 -- select top 100 * from RDW_Det
						Insert Into RDW_Det(RDW_MstrID, RDW_StepName, RDW_Duration, RDW_Predecessor, RDW_StartDate, RDW_EndDate, 
								  RDW_Status, RDW_CreatedBy, RDW_CreatedDate, RDW_T_UserID, RDW_T_StepID, RDW_TaskID, RDW_isTemp
								  , RDW_Sort, RDW_needTracking , RDW_extraStep) 
						Select @mid, RDW_StepName,RDW_Duration, RDW_Predecessor,  null ,null
						       ,Null, @uid, GetDate(), @uid , step.RDW_StepID ,Replace(s.Step, ' ', '') Step, 0
						       ,s.sort, RDW_needTracking,step.RDW_extraStep 
						From dbo.SortTasks(@template, 0, '') s
						Left Join RDW_Step step On step.RDW_StepID = s.StepID
						Left Join dbo.SortTasks(@template, 0, '') sort On sort.StepID = step.RDW_Predecessor
						Where step.RDW_MstrID = @template
						Order By s.sort
						
						If(@@error <> 0) Set @err = -4
						
				  
						

						Update RDW_Det
						Set RDW_ParentID = Isnull(parent.RDW_DetID, 0), RDW_Predecessor = pre.RDW_DetID
						From RDW_Step step
						Left Join (Select RDW_DetID, RDW_T_StepID As T_StepID From RDW_Det Where RDW_T_UserID = @uid) parent On parent.T_StepID = step.RDW_ParentID
						Left Join (Select RDW_DetID, RDW_T_StepID As T_StepID From RDW_Det Where RDW_T_UserID = @uid) pre On pre.T_StepID = step.RDW_Predecessor
						Where RDW_Det.RDW_MstrID = @mid
							And RDW_Det.RDW_T_UserID = @uid
							And RDW_Det.RDW_T_StepID = step.RDW_StepID
						If(@@error <> 0) Set @err = -5
						
						Update	RDW_Det
						Set RDW_StartDate = @start
							,RDW_EndDate = Dateadd(Day,RDW_Duration,@start)
						Where RDW_MstrID = @mid 
							And isnull(RDW_Predecessor,0) = 0;
						
						With duration
						As(
							Select RDW_DetID,RDW_MstrID,RDW_Predecessor
								, startDate = cast(@start as datetime) , endDate =DATEADD(day,RDW_Duration,@start)
							From RDW_Det
							Where RDW_MstrID = @mid 
							And isnull(RDW_Predecessor,0) = 0
							And ISNULL(RDW_ParentID,0) = 0
							
							Union All
							
							Select det.RDW_DetID,det.RDW_MstrID,det.RDW_Predecessor
								, startDate = DateAdd(Day,1,duration.endDate) 
								, endDate = DateAdd(Day,det.RDW_Duration+1,duration.endDate)
							From RDW_Det det
							inner join  duration On det.RDW_Predecessor = duration.RDW_DetID
						)					
						
						update d set RDW_StartDate=p.startDate,RDW_EndDate=p.endDate
						from RDW_Det d
						inner join duration p
						on d.RDW_DetID=p.RDW_DetID;
						
						With Child
						As
						(
							Select RDW_DetID,RDW_MstrID,RDW_ParentID
								, startDate = RDW_StartDate, endDate = RDW_EndDate
							FRom RDW_Det
							Where RDW_MstrID = @mid 
								And ISNULL(RDW_ParentID,0) = 0
								
							Union ALL
							
							Select det.RDW_DetID,det.RDW_MstrID,det.RDW_ParentID
								, startDate = child.startDate
								, endDate = DATEADD(day,det.RDW_Duration,child.startDate) 
							From RDW_Det det
							inner join  Child On det.RDW_ParentID = Child.RDW_DetID
						)
						
						update d set RDW_StartDate=c.startDate,RDW_EndDate=c.endDate
						from RDW_Det d
						inner join child c
						on d.RDW_DetID=c.RDW_DetID;
						
						Update RDW_Mstr
						Set RDW_EndDate =
								(Select top 1 RDW_EndDate
								From RDW_Det
								Where RDW_extraStep = 0 And RDW_MstrID = @mid
								Order By RDW_EndDate desc )
						Where RDW_MstrID = @mid	
						
						Update RDW_Mstr
						Set RDW_EndDate = @end
						Where RDW_MstrID = @mid	
						And RDW_EndDate is null					
						
					End
				End
			End
		Else
			Begin
				Set @err = -3
			End
		insert into RDW_Det_Ptr(RDW_DetID,RDW_PartnerID,RDW_PartnerName)
		select d.RDW_DetID,p.RDW_UserID UserID ,ISNULL(u.englishName,p.RDW_UserName) UserName
        from RDW_Step s 
		INNER join RDW_Step_Ptr p
		on s.RDW_StepID=p.RDW_StepID and s.RDW_MstrID=@template
		inner join RDW_Det d
		on s.RDW_TaskID=d.RDW_TaskID and d.RDW_MstrID=@mid
		Left JOIN tcpc0.dbo.users u
		ON u.UserID=p.RDW_UserID
		
		insert into RDW_Det_Mbr(RDW_DetID,RDW_EvaluateID,RDW_EvaluateName)
		select d.RDW_DetID DetID,M.RDW_UserID UserID ,ISNULL(u.englishName,M.RDW_UserName) UserName
        from RDW_Step s 
		INNER join RDW_Step_Mbr M
		on s.RDW_StepID=M.RDW_StepID and s.RDW_MstrID=@template
		inner join RDW_Det d
		on s.RDW_TaskID=d.RDW_TaskID and d.RDW_MstrID=@mid
		Left JOIN tcpc0.dbo.users u
		ON u.UserID=M.RDW_UserID
		
	If (@@error <> 0 Or @err < 0)
		Begin
			Rollback
			Select 0
		End
	Else
		Begin
			Commit
			Select @mid
		End





