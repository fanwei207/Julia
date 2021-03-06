USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pc_generationInquiry]    Script Date: 2015/6/11 10:54:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<fanwei>
-- Create date: <20141126>
-- Description:	<生成询价单>
-- =============================================
--exec sp_pc_generationInquiry @IMID='IM1411027',@company='aaaaa',@createdDate='123',@createdByName='mabo',@uID=13,@curr='RMB'  
ALTER PROCEDURE [dbo].[sp_pc_generationInquiry]
	@IMID char(9)
	,@company nvarchar(100) output
	,@createdDate varchar(20) output
	,@createdByName nvarchar(24) output
	,@uID int
	,@curr varchar(5)
AS
begin
	
	update dbo.PC_InquiryMstr
	set curr=@curr
	where IMID=@IMID
	
	update dbo.PC_ApplyDet
	set Curr = @curr
	where exists(
					select id.applyDetID
					from dbo.PC_InquiryMstr im left join dbo.PC_InquiryDet id  on id.IMID = im.IMID
					where id.applyDetID=DetId and im.IMID=@IMID
				)

	select @createdByName=u.userName,@createdDate=substring(convert(varchar(50),getdate(),120),0,11),@company=p.description
	from dbo.Users u left join dbo.Plants p on u.plantCode=p.plantID
	where u.userID=@uID
	
	select ad.Part as QAD,ad.ItemCode,ad.UM,ad.Formate,ad.ItemDescription,ad.ItemDesc1,ad.ItemDesc2,ad.Curr
	from dbo.PC_InquiryDet id left join dbo.PC_ApplyDet ad on ad.DetId=id.applyDetID
	where IMID=@IMID 
	AND ad.status>=2
	ORDER BY ad.Part
	
	
	
END
