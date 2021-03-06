USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_sid_mail_PriceUnMaintain]    Script Date: 05/12/2015 13:31:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		WangLW
-- Create date: 2014-12-09
-- Description:	<出运单>维护送货时间，筛选出未确认送货时间的数据，定时发送邮件。JOB：Extract_QAD_Data_430AM和Extract_QAD_Data_1230AM
-- =============================================
ALTER PROCEDURE [dbo].[sp_sid_mail_PriceUnMaintain] --	exec sp_sid_mail_PriceUnMaintain
	
	
AS
BEGIN
	Set Nocount On
	
	Declare @Body nvarchar(Max) --邮件正文
		, @To varchar(1000) --收件人邮箱
		, @Cc varchar(1000) --抄送邮箱
	
	Set @To = 'hanenrong.szx@tcp-china.com'
	--set @Cc = 'liuyi@tcp-china.com'
	--先删除掉已经维护好的

	if(select COUNT(*)
	from SID_det det
	where (SID_Price is null or  SID_price1 is null)
		And sid_createddate >= '2014-10-01' 
		And sid_qad not in(
								select pi_qad
								from Pi_PriceList pilist 
								where Pi_StartDate <= GETDATE() 
								And (Pi_EndDate >= GETDATE() Or Pi_EndDate Is Null)
								And det.sid_domain = pilist.PI_DOMAIN
							))>0
	begin
		Set @Body = N'<span style="font-size:13px;">以下出运单价格表未维护，请及时维护价格信息：</span><br /><table cellspacing="0" cellpadding="5" style="font-size:13px; text-align:center;border:solid 1px #000000;">'
		Set @Body = @Body + (
								select td = sid_nbr,td = sid_qad,td = mstr.sid_so_cust,td = case when substring(mstr.sid_so_ship,1,1)<>'C' then ad.ad_attn else mstr.sid_so_ship end,td = det.sid_createddate
								from SID_det det
								inner join sid_mstr mstr on det.sid_id = mstr.sid_id
								inner join qad_data..ad_mstr ad on mstr.sid_so_ship = ad.ad_addr And mstr.sid_domain = ad.ad_domain
								where (SID_Price is null or  SID_price1 is null)
								And det.sid_createddate >= '2014-10-01'
								And sid_qad not in(
														select pi_qad
														from Pi_PriceList pilist 
														where Pi_StartDate <= GETDATE() 
														And (Pi_EndDate >= GETDATE()-1 or Pi_EndDate is null)
														And mstr.sid_so_cust = pilist.pi_cust
														And pilist.pi_shipto = case when substring(mstr.sid_so_ship,1,1)<>'C' then ad.ad_attn else mstr.sid_so_ship end
														And mstr.sid_domain = pilist.PI_DOMAIN
													)
								order by det.sid_createddate desc
								For Xml Raw('tr'), Elements
							)
		Set @Body = @Body + '</table>'
		Exec msdb.dbo.sp_send_dbmail
		@profile_name = 'sql2005_sendmail'
		, @recipients = @To
		, @copy_recipients = @Cc
		, @blind_copy_recipients ='wangliwei@tcp-china.com'
		, @subject = N'【重要】出运单价格表未维护列表'
		, @body = @Body
		, @body_format = 'HTML'
	end
	
END
