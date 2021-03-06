USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_sid_email_PartNOTExistsNotes]    Script Date: 07/06/2015 14:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		WangLW
-- Create date: 2015-06-11
-- Description:	<出运单>客户物料在零件对照表未维护，定时发送邮件
-- =============================================
ALTER PROCEDURE [dbo].[sp_sid_email_PartNOTExistsNotes] --	exec sp_sid_email_PartNOTExistsNotes
	
	
AS
BEGIN
	Set Nocount On
	
		Declare @account varchar(100)  
		Declare @copyaccount varchar(100)  
		Declare @sendto varchar(50)
		Declare @sendcc varchar(50)
		Declare @subjectdesc varchar(50)
		
		update SID_det set SID_cust_part = cust_part
		from SID_det det 
		Left Join (
						select *from
						(
							select  no=row_number() over(partition by case when ISNULL(podet.partNbr,'') <> '' then  podet.qadPart else cp_part end order by getdate())
								, case when ISNULL(podet.partNbr,'') <> ''  then  podet.qadPart else cp_part end As qad 
								, case when ISNULL(podet.partNbr,'') <> '' And ISNULL(podet.sku,'') <> '' And so_ship in('C0000004','C0000067') then podet.xPartNbr 
										when ISNULL(podet.partNbr,'') <> '' And so_ship not in('C0000004','C0000067') then  podet.partNbr else cp_cust_part end As cust_part
							From SID_det det1
							Left Join (
										select so.so_nbr,so.so_cust,so_ord_date,case when so.so_ship like N'C%' then so.so_ship when so.so_ship not like N'C%' then case when isnull(ad_attn,'') = '' then ad_ref end end as so_ship
										from QAD_Data..so_mstr so
										Inner Join QAD_Data..ad_mstr ad on so.so_ship = ad.ad_addr And so.so_domain = ad.ad_domain
										)ad1 on det1.SID_so_nbr = ad1.so_nbr
							Left Join EDI_DB..EdiPoHrd hrd on hrd.soNbr = det1.SID_so_nbr--hrd.poNbr = det1.SID_PO 更改为关联销售单号
							Left Join EDI_DB..EdiPoDet podet on podet.hrd_id = hrd.id And det1.SID_so_line = podet.poLine
							Left Join EDI_DB..cp_mstr cp on  cp.cp_cust = ad1.so_cust 	
								And (ad1.so_ord_date >= Isnull(cp.cp_start_Date, '1900-1-1') And ad1.so_ord_date <= Isnull(cp.cp_end_Date, '3900-1-1'))
								And det1.SID_QAD = cp.cp_part
							where  ISNULL(det1.SID_cust_part,'') = ''
						)a
						where no = 1
					) tmp on tmp.qad=det.SID_QAD
		where  ISNULL(det.SID_cust_part,'') = ''
		
		select @account = sendto, @copyaccount = copyto
		from WorkFlow..Rec_RecipientConfig 
		where id = '90EEA09E-E16A-4303-BD67-C97F98F87ED1'
		
		if @account is null And @copyaccount is null
		Begin
			Set @account = 'ishelp@tcp-china.com'
		End
		
		Declare @tableHTML nvarchar(Max)    
		Set @tableHTML =     N'<span style="font-size:13px;">以下料件号在客户零件对照表未维护或在销售单有效期内未维护，请及时维护：</span><br />'+ 
		N'<table border="1" cellspacing="0" cellpadding="0">' +  
		N'<tr><th>出运单号</th><th>TCP销售单</th><th>客户/货物发往</th><th>客户物料号</th><th>QAD</th><th>订单日期</th></tr>' +   
		Cast((    
				Select Distinct  td = mstr.SID_nbr, '',td = so1.so_nbr, '', td = so_cust, ''
					, td = case when ISNULL(podet.partNbr,'') <> '' And ISNULL(podet.sku,'') <> '' And so_ship in('C0000004','C0000067') then podet.xPartNbr 
								when ISNULL(podet.partNbr,'') <> '' And so_ship not in('C0000004','C0000067') then  podet.partNbr else cp_cust_part end
					, '', td = det.SID_QAD, '', td = CONVERT(varchar(10), so1.so_ord_date, 120), ''
				from SID_mstr mstr
				Inner Join SID_det det on mstr.SID_id = det.SID_id 
				Left Join (
							select so.so_nbr,so.so_cust,so_ord_date,case when so.so_ship like N'C%' then so.so_ship when so.so_ship not like N'C%' then case when isnull(ad_attn,'') = '' then ad_ref end end as so_ship
							from QAD_Data..so_mstr so
							Inner Join QAD_Data..ad_mstr ad on so.so_ship = ad.ad_addr And so.so_domain = ad.ad_domain
							)so1 on det.SID_so_nbr = so1.so_nbr
				Left join EDI_DB..EdiPoHrd hrd on hrd.soNbr = det.SID_so_nbr--hrd.poNbr = det.SID_PO 更改为关联销售单号

				Left join EDI_DB..EdiPoDet podet on podet.hrd_id = hrd.id And det.SID_so_line = podet.poLine
				Left join EDI_DB..cp_mstr cp on  cp.cp_cust = so1.so_cust 	
					And (so1.so_ord_date >= Isnull(cp.cp_start_Date, '1900-1-1') And so1.so_ord_date <= Isnull(cp.cp_end_Date, '3900-1-1'))
					And det.SID_QAD = cp.cp_part

				where  ISNULL(det.SID_cust_part,'') = ''

			For Xml Path('tr'), Type       
			) As nvarchar(Max)) +       
			N'</table>'
			
		If(exists  (select *  from SID_det where  ISNULL(SID_cust_part,'') = '')) 
		Begin
			Exec msdb.dbo.sp_send_dbmail  
			@profile_name = 'sql2005_sendmail'
			,  @recipients = @account
			,  @copy_recipients = @copyaccount
			,  @subject =  N'料件号在客户零件对照表未维护'
			,  @body = @tableHTML
			,  @body_format = 'HTML'
		End
 
END
