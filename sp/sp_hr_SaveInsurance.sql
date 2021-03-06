USE [tcpc1]
GO
/****** Object:  StoredProcedure [dbo].[sp_hr_SaveInsurance]    Script Date: 06/29/2015 16:33:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Simon
-- Create date: September 3, 2009
-- Description:	Update Insurance
-- =============================================
ALTER  PROCEDURE [dbo].[sp_hr_SaveInsurance] 
	-- Add the parameters for the stored procedure here
@UserID as int,
@UserNo as varchar(5),
@UserName as nvarchar(20),
@hr_accfound_hFound as decimal(9,2),
@hr_accfound_mFound as decimal(9,2),
@hr_accfound_eFound as decimal(9,2),
@hr_accfound_rFound as decimal(9,2),
@hr_accfound_Injury as decimal(9,2),
@createBy as int,
@hr_accfound_date as smalldatetime
, @roleID int
, @deptID int
, @workshop int
, @workgroup int
, @roleTypeID int
, @retValue int output
, @PlantCode int

AS
BEGIN Transaction
    
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	Set @retValue = 1
	
	Declare @users Table(
		userID int
		,userCode varchar(8)
		,userName nvarchar(15)
		,roleID int
		,roleTypeID int
		,workshop int
		,workgroup int
		,deptID int
	)
	Declare @Newusers Table(
		userID int
		,userCode varchar(8)
		,userName nvarchar(15)
		,roleID int
		,roleTypeID int
		,workshop int
		,workgroup int
		,deptID int
	)
	
	Insert into @users ( userID,userCode,userName,roleID,roleTypeID,workshop,workgroup,deptID)
	Select userID,userNo,userName,roleID
	,CASE WHEN roleID >= 100 AND roleID < 300 THEN 0 
	WHEN roleID >= 300 AND roleID < 500 THEN 1 
	WHEN roleID >= 500 AND roleID < 1000 THEN 2 
	WHEN roleID >= 1000 AND roleID < 5000 THEN 3 END AS roleTypeID
	,workshopID,workProcedureID,departmentID
	From Tcpc0.dbo.users
	Where (@UserID = 0 OR ( @UserID <> 0 And userID = @UserID ) )
	And ( @roleID = 0 OR ( @roleID <> 0 And isnull(roleID,0) = @roleID ) )
	And ( @deptID = 0 OR ( @deptID <> 0 And isnull(departmentID,0) = @deptID ) )
	And ( @workshop = 0 OR ( @workshop <> 0 And isnull(workshopID,0) = @workshop ) )
	And ( @workgroup = 0 OR ( @workgroup <> 0 And isnull(workProcedureID,0) = @workgroup ) )
	And ( CASE WHEN roleID >= 100 AND roleID < 300 THEN 0 
		WHEN roleID >= 300 AND roleID < 500 THEN 1 
		WHEN roleID >= 500 AND roleID < 1000 THEN 2 
		WHEN roleID >= 1000 AND roleID < 5000 THEN 3 END ) = @roleTypeID
	And plantCode = @PlantCode
	
	If ( ( select COUNT(*) from @users ) > 0)
	Begin
		--hr_accfound_mstr存在，则修改
		UPDATE hr_accfound_mstr 
		SET hr_accfound_hFound = case @hr_accfound_hFound When 0 Then hr_accfound_hFound Else @hr_accfound_hFound End, --住房公积金
			hr_accfound_mFound = case @hr_accfound_mFound When 0 Then hr_accfound_mFound Else @hr_accfound_mFound End, --医疗保险
			hr_accfound_eFound = case @hr_accfound_eFound When 0 Then hr_accfound_eFound Else @hr_accfound_eFound End, --失业保险
			hr_accfound_rFound = case @hr_accfound_rFound When 0 Then hr_accfound_rFound Else @hr_accfound_rFound End, --养老保险
			hr_accfound_Injury = case @hr_accfound_Injury When 0 Then hr_accfound_Injury Else @hr_accfound_Injury End, --工伤保险
			hr_accfound_mod = @createBy,
			hr_accfound_moddate = getdate()     
		WHERE Exists (Select *  from @users Where userID = hr_accfound_userID ) 
			AND Year(hr_accfound_date)= Year(hr_accfound_date) AND Month(hr_accfound_date)= Month(@hr_accfound_date)
		
		IF(@@ERROR <> 0)
		Set @retValue = 2
		--hr_accfound_mstr不存在，则新增
		
		INSERT INTO  hr_accfound_mstr (hr_accfound_userID,hr_accfound_userCode,
			hr_accfound_userName,hr_accfound_dpID,hr_accfound_date,hr_accfound_hFound,
            hr_accfound_mFound,hr_accfound_eFound,hr_accfound_rFound,
            hr_accfound_Injury,hr_accfound_createBY,hr_accfound_createdate)
		SELECT userID,userCode,userName,deptID,@hr_accfound_date,@hr_accfound_hFound,
			@hr_accfound_mFound,@hr_accfound_eFound,@hr_accfound_rFound,@hr_accfound_Injury,
			@createBy, getdate() 
		FROM @users
		Where Not Exists (Select *  from hr_accfound_mstr Where userID = hr_accfound_userID And Year(hr_accfound_date)= Year(@hr_accfound_date) AND Month(hr_accfound_date)= Month(@hr_accfound_date) )
			
		
		IF(@@ERROR <> 0)
		Set @retValue = 3
		
	End
	
	Else
		Set @retValue = 0

    
    IF (@retValue = 1)
         Commit
    ELSE
         Rollback

