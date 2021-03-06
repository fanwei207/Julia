USE [TCPC_Supplier]
GO
/****** Object:  StoredProcedure [dbo].[sp_pur_mail_UnconfirmedAndUndeliveredPO]    Script Date: 2015/08/20 Thursday 2:51:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Chenyb
-- Create date: 2013-08-23
-- Description:	每天向供应商发送下达超过24小时未确认的订单、距离截止日期还有48小时未送货的订单
-- =============================================
ALTER PROCEDURE [dbo].[sp_pur_mail_UnconfirmedAndUndeliveredPO] -- exec sp_pur_mail_UnconfirmedAndUndeliveredPO
	
AS
BEGIN

	/*
		内部供应商代码
		SZX-S0021003	ZQL-S0511014	YQL-S0514003	HQL-S0517001
    */
    	
	Declare @account varchar(1000)
      , @account_copy varchar(1000)
      , @userNo varchar(8)
	  , @mailError nvarchar(2000)
	  , @userCompanyName nvarchar(100)
	  , @unConfirmedPO nvarchar(max)	--下达超过24小时未确认的订单
	  , @unDeliveredPO nvarchar(max)	--距离截止日期有48小时未发送的订单
	  , @tableHTML nvarchar(Max)		--邮件内容
	
	set @unConfirmedPO = N''
	set @unDeliveredPO = N''
	
	Declare p_cursor cursor for
		select distinct po_vend, usr_companyName, usr_email--, ty
		from
		(	
			--24小时未确认
			select distinct qad.po_vend, usr_companyName, usr_email, 'unconfirmed' as ty
			from QAD_DATA.QAD_Data.dbo.po_mstr qad 
			left join TCPC_Supplier.dbo.usr_mstr on usr_loginName = po_vend
			Left Join TCPC_Supplier.dbo.po_mstr net On qad.po_domain = net.po_domain And qad.po_nbr = net.po_nbr
			where qad.po_domain in ('SZX', 'ZQL', 'YQL', 'HQL')
				And qad.po_vend not in ('S0021003','S0511014','S0514003','S0517001')
				And ISNULL(qad.po_stat, '') = ''
				And usr_isActive = 1
				And ISNULL(usr_email, '') <> ''
				And net.po_con_date Is Null 
				And dbo.fn_getDateDiff
				(
					case when isnull(qad.po_pst_id, '') <> '' and isdate(convert(varchar(19), qad.po_pst_id, 120)) = 1 
								then case when datepart(hour, convert(varchar(19), qad.po_pst_id, 120)) < 12 then convert(varchar(10), convert(varchar(19), qad.po_pst_id, 120), 120)+ ' 12:00:00' 
										  else convert(varchar(10), dateadd(day, 1, convert(varchar(19), qad.po_pst_id, 120)), 120) + ' 08:00:00' 
									 end
						when isnull(qad.po_pst_id, '') = '' and isnull(qad.po_fst_id, '') <> '' and isdate(convert(varchar(19), qad.po_fst_id, 120)) = 1
								then case when datepart(hour, convert(varchar(19), qad.po_fst_id, 120)) < 12 then convert(varchar(10), convert(varchar(19), qad.po_fst_id, 120), 120)+ ' 12:00:00' 
										  else convert(varchar(10), dateadd(day, 1, convert(varchar(19), qad.po_fst_id, 120)), 120) + ' 08:00:00' 
									 end
						when isnull(qad.po_pst_id, '') = '' and isnull(qad.po_fst_id, '') = '' and isnull(qad.po_ord_date, '') <> ''
								then qad.po_ord_date
						else getdate()
					end, getdate() 
				) >= 24
				And qad.po_ord_date > '2013-07-01'
				--And qad.po_vend = 'S0025005'
			union all
			--48小时催货
			select distinct po_vend, usr_companyName, usr_email, 'undelivered' as ty
			from QAD_DATA.QAD_Data.dbo.po_mstr qad
			Left Join QAD_DATA.QAD_Data.dbo.pod_det pod On pod.pod_domain = qad.po_domain And pod.pod_nbr = qad.po_nbr
			left join TCPC_Supplier.dbo.usr_mstr on usr_loginName = po_vend
			where po_domain in ('SZX', 'ZQL', 'YQL', 'HQL')
				And po_vend not in ('S0021003','S0511014','S0514003','S0517001')
				And ISNULL(po_stat, '') = ''
				And ISNULL(pod_status, '') = ''
				And pod_qty_ord > pod_qty_rcvd --有些已经送货完毕了，但状态没有设置成C，这里人为筛选一下
				--以pod_due_date为基准
				And CONVERT(varchar(10), pod.pod_due_date, 120) <= CONVERT(varchar(10), DATEADD(DAY, 2, GETDATE()), 120)
				And usr_isActive = 1
				And ISNULL(usr_email, '') <> ''
				--And po_vend = 'S0025005'
		) usrs
	Open p_cursor
	Fetch next from p_cursor into @userNo, @userCompanyName, @account 
	while(@@FETCH_STATUS = 0)
	Begin
		--set @account = 'liuyi@tcp-china.com'
		if(CHARINDEX('@', @account) > 2 and CHARINDEX('@', @account) < LEN(@account))
		Begin		
			Begin Try		
				set @unConfirmedPO = CAST(
										   (
											  select qad.po_nbr as td, qad.po_domain as td, CONVERT(varchar(10), qad.po_ord_date, 120) as td, CONVERT(varchar(10), qad.po_due_date, 120) as td 
													, case when ISNULL(qad.po_consignment, 0) = 0 then N'否' else N'是' end as td 
											  from QAD_DATA.QAD_Data.dbo.po_mstr qad
											  Left Join TCPC_Supplier.dbo.po_mstr net On qad.po_domain = net.po_domain And qad.po_nbr = net.po_nbr
											  where qad.po_domain in ('SZX', 'ZQL', 'YQL', 'HQL')
												  And ISNULL(qad.po_stat, '') = ''
												  And net.po_con_date Is Null 
												  And Exists(Select pod_nbr From QAD_Data.dbo.pod_det Where pod_domain = qad.po_domain And pod_nbr = qad.po_nbr)
												  And dbo.fn_getDateDiff
												  (
													  case when isnull(qad.po_pst_id, '') <> '' and isdate(convert(varchar(19), qad.po_pst_id, 120)) = 1 
																then case when datepart(hour, convert(varchar(19), qad.po_pst_id, 120)) < 12 then convert(varchar(10), convert(varchar(19), qad.po_pst_id, 120), 120)+ ' 12:00:00' 
																		  else convert(varchar(10), dateadd(day, 1, convert(varchar(19), qad.po_pst_id, 120)), 120) + ' 08:00:00' 
														  			 end
														  when isnull(qad.po_pst_id, '') = '' and isnull(qad.po_fst_id, '') <> '' and isdate(convert(varchar(19), qad.po_fst_id, 120)) = 1
																then case when datepart(hour, convert(varchar(19), qad.po_fst_id, 120)) < 12 then convert(varchar(10), convert(varchar(19), qad.po_fst_id, 120), 120)+ ' 12:00:00' 
																		  else convert(varchar(10), dateadd(day, 1, convert(varchar(19), qad.po_fst_id, 120)), 120) + ' 08:00:00' 
																	 end
														  when isnull(qad.po_pst_id, '') = '' and isnull(qad.po_fst_id, '') = '' and isnull(qad.po_ord_date, '') <> ''
																then qad.po_ord_date
														  else getdate()
													  end, getdate() 
												  ) >= 24
												  And qad.po_vend = @userNo
												  And qad.po_ord_date > '2013-07-01'
											  order by qad.po_vend, qad.po_ord_date
											  For Xml Raw('tr'), elements
										   ) as nvarchar(max)
										 )
				
				set @unDeliveredPO = CAST(
										   (
											  select po_nbr as td, po_domain as td, CONVERT(varchar(10), po_ord_date, 120) as td
												, pod_line as td, CONVERT(varchar(10), pod_due_date, 120) as td, pod_part as td, cast(cast(pod_qty_ord as integer) as varchar) as td, cast(cast(pod_qty_rcvd as integer) as varchar) as td
												, case when ISNULL(po_consignment, 0) = 0 then N'否' else N'是' end as td 
											  from QAD_DATA.QAD_Data.dbo.po_mstr po
											  Left Join QAD_DATA.QAD_Data.dbo.pod_det pod On pod.pod_domain = po.po_domain And pod.pod_nbr = po.po_nbr
											  where po_domain in ('SZX', 'ZQL', 'YQL', 'HQL')
											    And ISNULL(po_stat, '') = ''
											    And ISNULL(pod_status, '') = ''
											    And pod_qty_ord > pod_qty_rcvd
											    And CONVERT(varchar(10), pod.pod_due_date, 120) <= CONVERT(varchar(10), DATEADD(DAY, 2, GETDATE()), 120)
											    And po_vend = @userNo
											order by po_nbr, po_ord_date, pod_line
											  For Xml Raw('tr'), elements
										   ) as nvarchar(max)
										 )
				
				set @tableHTML = N'<span style="font-size:13px;">尊敬的' + @userCompanyName + N'：</span><br/>'
				   + N'<span style="font-size:13px;">&nbsp;&nbsp;您好！</span><br/>'
				if(@unConfirmedPO <> N'')
				Begin
					set @tableHTML = @tableHTML 
					+ N'<span style="font-size:13px;">&nbsp;&nbsp;以下是TCP（中国）发给您处的订单，这些订单已经下达超过24小时但贵公司还未确认，特意提醒：</span><br/>'
					+ N'<span style="font-size:13px;">&nbsp;&nbsp;详情请至https://supplier.tcp-china.com/ 下供应商->订单列表 菜单查询</span><br/>'
					+ N'<table cellspacing="0" cellpadding="5" style="font-size:13px; text-align:center;border:solid 1px #000000;">' 
					+ N'<tr><th style="width:80px;">采购订单</th><th style="width:60px;">公司</th><th style="width:80px;">订单日期</th><th style="width:80px;">截止日期</th><th style="width:70px;">是否寄售</th></tr>'
					+ @unConfirmedPO
				    + N'</table><br/><br/>' 
				End
				if(@unDeliveredPO <> N'')
				Begin
					set @tableHTML = @tableHTML
					+ N'<span style="font-size:13px;">&nbsp;&nbsp;以下是TCP（中国）发给您处的订单，这些订单已经超期未送或者即将到期（提前2天），特意提醒：</span><br/>'
				    + N'<span style="font-size:13px;">&nbsp;&nbsp;详情请至https://supplier.tcp-china.com/ 下供应商->订单列表 菜单查询</span><br/>'
				    + N'<table cellspacing="0" cellpadding="5" style="font-size:13px; text-align:center;border:solid 1px #000000;">' 
				    + N'<tr><th style="width:80px;">采购订单</th><th style="width:60px;">公司</th><th style="width:80px;">订单日期</th><th style="width:40px;">订单行</th><th style="width:80px;">截止日期</th><th style="width:120px;">零件号</th><th style="width:80px;">已订购量</th><th style="width:80px;">收货量</th><th style="width:70px;">是否寄售</th></tr>'
				    + @unDeliveredPO
				    + N'</table><br/><br/>'
				End
				set @tableHTML = @tableHTML    
				   + N'<span style="font-size:13px;">&nbsp;&nbsp;给您带来的任何不便敬请谅解！</span><br/>'
				   + N'<span style="font-size:13px;">&nbsp;&nbsp;如果您对订单的数据有任何疑问，请联系我们公司对应的采购员，谢谢！</span><br/>'
				   + N'<span style="font-size:13px;">&nbsp;&nbsp;请注意：订单数据于每日上午4：30和中午12：50同步两次</span><br/>'
				   + N'<span style="font-size:13px;">&nbsp;&nbsp;备注：SZX：上海强凌电子有限公司</span><br/><br/>'
				   + N'<span style="font-size:13px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ZQL ：镇江强凌电子有限公司</span><br/>'
				   + N'<span style="font-size:13px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ZQZ：镇江强灵照明有限公司</span><br/>'
				   + N'<span style="font-size:13px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HQL：淮安强陵照明有限公司</span><br/>'
				   + N'<span style="font-size:13px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;YQL：扬州强凌有限公司</span><br/>'
				 
				 Exec msdb.dbo.sp_send_dbmail
					  @profile_name = 'sql2005_sendmail'
					  , @recipients = @account
					  , @subject = N'【提醒】TCP采购订单———请勿回复此邮件'
					  , @body = @tableHTML
					  , @body_format = 'HTML'
			End Try
			Begin Catch
				set @mailError = ISNULL(@mailError, N'') + CAST(@userNo as nvarchar) + '#' + CAST(ERROR_MESSAGE() as nvarchar) + N';'
			End Catch
		End
		Fetch next from p_cursor into @userNo, @userCompanyName, @account
	End
		Close p_cursor
		Deallocate p_cursor
	
	if(ISNULL(@mailError, N'') <> N'')
	Begin
		set @account = 'ishelp@tcp-china.com'
		set @account_copy = ''
		set @tableHTML = N'<H1>' + CONVERT(varchar(10), GETDATE(), 120) + N'发送失败的订单</H1>'
			+ N'<table border="1" cellspacing="0" cellpadding="0" style="text-align:center;">' 
			+ N'<tr><th>供应商代码</th><th>错误描述</th></tr>'
			+ CAST(
					  (
						  select SUBSTRING(String, 1, CHARINDEX('#', String) - 1) as td
							, SUBSTRING(String, CHARINDEX('#', String) + 1, LEN(String)) as td 
						  from dbo.Split(@mailError)
						  For Xml Raw('tr'), elements
					  ) as nvarchar(max)
                 )
           + N'</table>'
           
        Exec msdb.dbo.sp_send_dbmail
			  @profile_name = 'sql2005_sendmail'
			  , @recipients = @account
			  , @subject = N'【通知】发送失败的邮件'
			  , @body = @tableHTML
			  , @body_format = 'HTML'
	End
END



