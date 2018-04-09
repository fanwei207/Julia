USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_m5_selectM5Mstr]    Script Date: 6/12/2015 1:53:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_m5_selectM5Mstr]
	
	@project int
	, @no varchar(20) = ''
	, @stdDate varchar(10)
	, @endDate varchar(10)
	, @types int = 0
	, @isApprove bit = 0
	, @isAgree bit = 0
	, @desc nvarchar(500) = N''
	, @type int --1:仅未审批的 2:验证未决 3:仅未验证的 4:仅未同意的 5:未执行
	, @uID int = 0
AS
BEGIN
/*
	Exec sp_m5_selectM5Mstr
		@project = 0
		, @no = ''
		, @stdDate = ''
		, @endDate = ''
		, @types = 0
		, @isApprove = 0
		, @isAgree = 0
		, @desc = N''
		, @type = 5
		, @uID = 56110
*/
	Declare @accessValid bit --该人员是否具有决定“是否需要验证”的权限
	Select @accessValid = 1 From AccessRule Where moduleID = 560000463 And userID = @uID
	Set @accessValid = Isnull(@accessValid, 0)

	Select m5_no, m5_project, pro.m5p_project, m5_desc, m5_market
		, m5_needValid = Isnull(m5_needValid, 0)
		, m5_isApproved = Isnull(m5_isApproved, 0)
		, m5_apprBy, m5_apprName, m5_apprDate
		, m5_isAgreed = Isnull(m5_isAgreed, 0)
		, m5_agreeBy, m5_agreeName, m5_agreeDate, m5_effDate
		, m5_createBy, m5_createName, m5_createDate
		, m5_isClose = Isnull(m5_isClose, 0)
		, convert(char(10),m5_closeDate,120) as m5_closeDate
	From m5_mstr mstr
	Left Join m5_project pro On pro.m5p_id = mstr.m5_project
	Where (@project = 0 Or (@project <> 0 And m5_project = @project))
		And (@no = '' Or (@no <> '' And m5_no = @no))
		And (@stdDate = '' Or (@stdDate <> '' And m5_createDate >= @stdDate))
		And (@endDate = '' Or (@endDate <> '' And m5_createDate < @endDate))
		And (@types = 0 
			Or (@types = 1  And (m5_isApproved Is Null Or m5_isApproved = 1) And m5_isAgreed Is Null And Isnull(m5_isClose, 0) = 0) 
			Or (@types = 2  And (m5_isApproved = 0 Or  m5_isAgreed = 0) And Isnull(m5_isClose, 0) = 0)
			Or (@types = 3  And m5_isAgreed = 1 And Isnull(m5_isClose, 0) = 0)
			Or (@types = 4  And m5_isApproved Is Not Null And m5_isAgreed Is Null And m5_isClose = 1))
		And (@desc = N'' Or (@desc <> N'' And m5_desc Like Replace(@desc, '*', '%')))
		And (@isAgree = 0 Or (@isAgree = 1 And mstr.m5_isAgreed Is Null))
		And (
			@type = 0
			--未评审
			--Or (@type = 1 And (@type = 1 AND m5_isApproved = 1 And Isnull(m5_isClose, 0) = 0 AND m5_needValid IS NULL And Exists(Select * From m5_effect_user Where m5eu_userid = @uID) And Not Exists(Select * From m5_effect_det d INNER JOIN dbo.m5_effect_user u ON d.m5ed_effectID=u.m5eu_effectid  Where d.m5ed_no = mstr.m5_no And u.m5eu_userid = @uID)))
			Or (@type = 1 And (@type = 1 AND m5_isApproved = 1 And Isnull(m5_isClose, 0) = 0 AND m5_needValid IS NULL And Exists(Select * From m5_effect_user Where m5eu_userid = @uID) And Not Exists(Select * From m5_effect_det d INNER JOIN dbo.m5_effect_user u ON d.m5ed_effectID=u.m5eu_effectid  Where d.m5ed_no = mstr.m5_no)))
			--验证未决
			Or (@type = 2 And (@type = 2 And m5_isApproved = 1 And Isnull(m5_isClose, 0) = 0 And m5_isAgreed Is Null And (m5_createBy = @uID Or @accessValid = 1) And m5_needValid Is Null And Exists(Select * From m5_effect me left join m5_effect_det med on me.m5e_id = med.m5ed_id AND med.m5ed_no=mstr.m5_no WHERE med.m5ed_id IS NULL)))
			--未验证
			--Or (@type = 3 And (@type = 3 AND m5_needValid = 1 And Isnull(m5_isClose, 0) = 0 AND m5_isAgreed IS NULL AND  Exists(Select * From m5_valid_user Where m5vu_userid = @uID) AND NOT Exists(Select * From m5_valid_det d INNER JOIN dbo.m5_valid_user u ON d.m5vd_effectID=u.m5vu_validid Where d.m5vd_no = mstr.m5_no And u.m5vu_userid = @uID)))
			Or (@type = 3 And (@type = 3 AND m5_needValid = 1 And Isnull(m5_isClose, 0) = 0 AND m5_isAgreed IS NULL AND  Exists(Select * From m5_valid_user Where m5vu_userid = @uID) AND NOT Exists(Select * From m5_valid_det d INNER JOIN dbo.m5_valid_user u ON d.m5vd_effectID=u.m5vu_validid Where d.m5vd_no = mstr.m5_no)))
			--未同意
			Or (@type = 4 And (@type = 4 AND ((m5_needValid =1 And Isnull(m5_isClose, 0) = 0 AND m5_isAgreed IS NULL AND NOT EXISTS(SELECT * FROM dbo.m5_valid v LEFT JOIN dbo.m5_valid_det d ON v.m5v_id=d.m5vd_effectID AND d.m5vd_no=mstr.m5_no WHERE d.m5vd_id IS NULL )) OR m5_needValid =0) AND EXISTS(SELECT * FROM dbo.AccessRule WHERE moduleID=560000462 AND userID=@uID)))
			--未执行
			Or (@type = 5 And (@type = 5 And Exists(Select * From m5_execute_user Where m5xu_no = m5_no And m5xu_userid = @uID) And m5_isExcuted Is Null))
		)
	Order By m5_no Asc

END

