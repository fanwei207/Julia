USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pc_selectInquiryDet]    Script Date: 2015/6/11 10:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fanwei>
-- Create date: <20141123>
-- Description:	<查找询价单详单>
-- =============================================

ALTER PROCEDURE [dbo].[sp_pc_selectInquiryDet]
	@IMID char(9)
	,@vender varchar(10) output
	,@venderName nvarchar(50) output
	,@statue int output
	,@curr varchar(5) output 
	,@create nvarchar(20) output
	,@createDate varchar(20) output 
	,@venderPhone varchar(30) output
	,@venderEmail nvarchar(100) output
AS
BEGIN
	
	select  @vender=im.vender,@venderName=im.venderName,@statue=im.status,@curr=im.curr,@create=u.userName
	,@createDate=substring( convert(varchar(30), im.createdDate,120),0,11),@venderPhone=su.usr_phone,@venderEmail=su.usr_email
	from dbo.PC_InquiryMstr im left join dbo.Users u on im.createdBy=u.userID
	left join  TCPC_Supplier..usr_mstr su on im.vender=su.usr_userName
	where im.IMID=@IMID
	
	select id.applyDetID, ad.Part,ad.ItemCode,ad.Formate,id.price,id.priceSelf,ad.UM,ad.PriceBasis,ad.FinCheckPriceBasis,ad.PriceFinish
	,id.checkPrice,id.priceDiscount,ad.status,ad.ItemDescription,ad.ItemDesc1,ad.ItemDesc2,ad.InfoFrom,isnull(ad.isCancel,0) isCancel
	,Description = case when items.Description = ad.ItemDescription then null else items.Description end
	,item_qad_desc1 = case when items.item_qad_desc1 = ad.ItemDesc1 then null else items.item_qad_desc1 end
	,item_qad_desc2 = case when items.item_qad_desc2 = ad.ItemDesc2 then null else items.item_qad_desc2 end
	from dbo.PC_InquiryDet id  left join dbo.PC_ApplyDet ad on id.applyDetID=ad.DetId
	left join items on part = item_qad
	where id.IMID=@IMID and ad.status>=2
	ORDER BY ad.Part
	
	
END
