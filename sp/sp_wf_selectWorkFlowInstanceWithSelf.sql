USE [WorkFlow]
GO
/****** Object:  StoredProcedure [dbo].[sp_wf_selectWorkFlowInstanceWithSelf]    Script Date: 05/11/2015 09:22:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Liuqj
-- Create date: 2012-12-15
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_wf_selectWorkFlowInstanceWithSelf] -- exec sp_wf_selectWorkFlowInstanceWithSelf '',0,'',13,111,0,1
	@WFN_Nbr varchar(20),
	@Flow_ID int,
	@req_date1 varchar(10),
	@uID int,
	@roleName nvarchar(50),
	@status int,
	@plantCode int
	
AS
BEGIN
	If(@status = 0) --流程正需要处理的人或角色可以看到
	Begin
		Select wfi.WFN_Nbr, wf.Flow_Name, WFN_ReqDate, WFN_DueDate, WFN_FormName, WFN_FormFormat, WFN_FormContent, u.userName as WFN_CreatedBy, WFN_CreatedDate, p.plantCode, remark.FNI_Remark
		From WF_WorkFlowInstance wfi
		Left join WF_WorkFlow wf On wf.Flow_ID = wfi.Flow_ID
		Left join tcpc0..Users u On u.userID = wfi.WFN_CreatedBy
		Left join WF_FlowNodeInstance fni On fni.WFN_Nbr = wfi.WFN_Nbr and fni.FNI_Status = 0 and fni.Sort_Order = wfi.WFN_NextNode
		Left join WF_NodePerson np On np.Node_ID = fni.Node_ID 
		Left Join tcpc0.dbo.Plants p On p.plantID = wfi.WFN_Domain
		Left join WF_FlowNodeInstance remark On  remark.WFN_Nbr = wfi.WFN_Nbr and remark.Sort_Order = 10
		Where WFN_Status = 1
		And (@WFN_Nbr = '' Or (@WFN_Nbr <> '' And wfi.WFN_Nbr = @WFN_Nbr))
		And (@Flow_ID = 0 Or (@Flow_ID <> 0 And wfi.Flow_ID = @Flow_ID))
		And (@req_date1 = '' Or (@req_date1 <> '' And wfi.WFN_ReqDate >= @req_date1))
		And ((np.NodePerson_Type = 0 and np.NodePerson_ObjectName = @roleName/* and np.NodePerson_ObjectID = (Select roleid From Tcpc0..Users Where userID = @uID)*/ 
			and (np.NodePerson_DeptID = 0 or (np.NodePerson_DeptID <> 0 and np.NodePerson_DeptID = (Select departmentID From Tcpc0..Users Where userID = @uID)))
			and wfi.WFN_Domain = @plantCode) 
			Or (np.NodePerson_Type = 1 and np.NodePerson_ObjectID = @uID))
		Order by wfi.WFN_CreatedDate desc
	End
	Else if(@status = 2) --审批流程为通过的人可以看到
	Begin
		Select wfi.WFN_Nbr, wf.Flow_Name, WFN_ReqDate, WFN_DueDate, WFN_FormName, WFN_FormFormat, WFN_FormContent, u.userName as WFN_CreatedBy, WFN_CreatedDate, p.plantCode, remark.FNI_Remark
		From WF_WorkFlowInstance wfi
		Left join WF_WorkFlow wf On wf.Flow_ID = wfi.Flow_ID
		Left join tcpc0..Users u On u.userID = wfi.WFN_CreatedBy
		Left join WF_FlowNodeInstance fni On fni.WFN_Nbr = wfi.WFN_Nbr 
		Left Join tcpc0.dbo.Plants p On p.plantID = wfi.WFN_Domain
		Left join WF_FlowNodeInstance remark On  remark.WFN_Nbr = wfi.WFN_Nbr and remark.Sort_Order = 10
		Where WFN_Status <> 0
		And (@WFN_Nbr = '' Or (@WFN_Nbr <> '' And wfi.WFN_Nbr = @WFN_Nbr))
		And (@Flow_ID = 0 Or (@Flow_ID <> 0 And wfi.Flow_ID = @Flow_ID))
		And (@req_date1 = '' Or (@req_date1 <> '' And wfi.WFN_ReqDate >= @req_date1))
		And fni.FNI_Status = 2 and fni.FNI_RunUID = @uID
		Order by wfi.WFN_CreatedDate desc
	End
	Else --审批流程为不通过的人可以看到
	Begin
		Select wfi.WFN_Nbr, wf.Flow_Name, WFN_ReqDate, WFN_DueDate, WFN_FormName, WFN_FormFormat, WFN_FormContent, u.userName as WFN_CreatedBy, WFN_CreatedDate, p.plantCode, remark.FNI_Remark
		From WF_WorkFlowInstance wfi
		Left join WF_WorkFlow wf On wf.Flow_ID = wfi.Flow_ID
		Left join tcpc0..Users u On u.userID = wfi.WFN_CreatedBy
		Left join WF_FlowNodeInstance fni On fni.WFN_Nbr = wfi.WFN_Nbr 
		Left Join tcpc0.dbo.Plants p On p.plantID = wfi.WFN_Domain
		Left join WF_FlowNodeInstance remark On  remark.WFN_Nbr = wfi.WFN_Nbr and remark.Sort_Order = 10
		Where WFN_Status = 3
		And (@WFN_Nbr = '' Or (@WFN_Nbr <> '' And wfi.WFN_Nbr = @WFN_Nbr))
		And (@Flow_ID = 0 Or (@Flow_ID <> 0 And wfi.Flow_ID = @Flow_ID))
		And (@req_date1 = '' Or (@req_date1 <> '' And wfi.WFN_ReqDate >= @req_date1))
		And fni.FNI_Status = 3 and fni.FNI_RunUID = @uID
		Order by wfi.WFN_CreatedDate desc
	End
END

