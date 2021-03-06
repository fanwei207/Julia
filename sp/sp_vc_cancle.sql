USE [qadplan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vc_cancle]    Script Date: 04/16/2015 13:54:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Amber Liu
-- Create date: Jan 30,2015
-- Description: sp_vc_cancle
-- =============================================				
Create PROCEDURE [dbo].[sp_vc_cancle]
	
	@vc_id int
	,@re_Value int out
AS

Begin
 
	Begin Transaction vc_updateMstr
		if(not exists ( select * from vc_mstr where vc_id=@vc_id and vc_IsCancle=1))
		Begin
			Update vc_mstr
			Set vc_IsCancle=1
			Where vc_id = @vc_id
		End
		Else
			Set @re_Value=2

	If(@@ERROR<>0)
		Begin
			Set @re_Value=0
			RollBack Transaction vc_updateMstr
		End	
	Else
		Begin
			if(@re_Value<>2) Set @re_Value=1
			Commit Transaction vc_updateMstr
		End
End
