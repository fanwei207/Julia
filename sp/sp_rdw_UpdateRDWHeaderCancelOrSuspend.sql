USE [RD_Workflow]
GO
/****** Object:  StoredProcedure [dbo].[sp_rdw_UpdateRDWHeaderCancelOrSuspend]    Script Date: 2015-06-23 10:47:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		WangLW
-- Create date: 2015/06/12
-- Description:	sp_RDW_UpdateRDWHeaderCancel
-- =============================================
ALTER PROCEDURE [dbo].[sp_rdw_UpdateRDWHeaderCancelOrSuspend]
(
	@mid varchar(10)
	, @Status varchar(10)
	, @Remark nvarchar(300)
)
As
Begin
	Begin Transaction
		Update RDW_Mstr Set RDW_Status = UPPER(@Status), RDW_Remark = @Remark Where RDW_MstrID = cast(@mid as bigint)
		If (@@Error <> 0)
		Begin
			Rollback
			Select 0
		End
	Else
	Begin
		Commit
		Select 1
	End
End