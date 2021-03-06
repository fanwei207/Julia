USE [RD_Workflow]
GO
/****** Object:  StoredProcedure [dbo].[sp_rdw_UpdateRDWHeader]    Script Date: 2015-06-23 10:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ye Bin
-- Create date: 2009/10/28
-- Description:	sp_RDW_UpdateRDWHeader
-- Modify:2013-11-12 wangcaixia add @SKu
-- =============================================
ALTER PROCEDURE [dbo].[sp_rdw_UpdateRDWHeader]
(
	@mid varchar(10)
	, @code nvarchar(50)
	, @sku nvarchar(50)
	, @desc nvarchar(250)
	, @memo nvarchar(200)
	, @start varchar(10)
	, @end varchar(10)
	, @stand nvarchar(500)
	, @proj nvarchar(100)
	, @cate bigint = 0
	, @status VARCHAR(10) = ''
)
As

	Declare @task_startdate datetime
	Declare @task_enddate datetime
	Declare @retValue bit
	Declare @err int
	Set @err = 0

	Begin Transaction
		--判断项目的日期是否在已经维护好的任务范围之内

		insert into RDW_MstrHist(RDW_MstrID, RDW_Category, RDW_Project, RDW_ProdCode, RDW_ProdDesc, RDW_Standard, RDW_ProdSKU, RDW_StartDate, RDW_EndDate
						, RDW_FinishDate, RDW_Memo, RDW_Partner, RDW_PartnerName, RDW_Status, RDW_CreatedBy, RDW_CreatedDate, RDW_Template
						, RDW_T_UserID, RDW_T_MstrID, RDW_pmID, RDW_pm, RDW_prodcode_old, RDW_Remark)
		select RDW_MstrID, RDW_Category, RDW_Project, RDW_ProdCode, RDW_ProdDesc, RDW_Standard, RDW_ProdSKU, RDW_StartDate, RDW_EndDate
								, RDW_FinishDate, RDW_Memo, RDW_Partner, RDW_PartnerName, RDW_Status, RDW_CreatedBy, RDW_CreatedDate, RDW_Template
								, RDW_T_UserID, RDW_T_MstrID, RDW_pmID, RDW_pm, RDW_prodcode_old, RDW_Remark
		from RDW_Mstr 
		where RDW_MstrID = @mid
		If(@@ERROR <> 0)
		SET @err = -1

		Update RDW_Mstr 
		Set RDW_ProdCode = @code, RDW_ProdDesc = @desc, RDW_Standard = @stand , RDW_ProdSKU = @sku
			, RDW_Memo = @memo, RDW_StartDate = @start, RDW_EndDate = @end, RDW_Project = @proj
			, RDW_Category = @cate,RDW_Status=@status
		Where RDW_MstrID = @mid

		Set @retValue = 1

	If (@@Error <> 0 Or @err < 0)
		Begin
			Rollback
			Select @retValue
		End
	Else
		Begin
			Commit
			Select @retValue
		End
