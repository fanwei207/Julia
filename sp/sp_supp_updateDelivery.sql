USE [TCPC_Supplier]
GO
/****** Object:  StoredProcedure [dbo].[sp_supp_updateDelivery]    Script Date: 04/01/2015 16:03:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wangdl
-- Create date: 2013-09-24
-- Description:	记录送货单的送货时间和质检时间
-- =============================================
ALTER PROCEDURE [dbo].[sp_supp_updateDelivery]

	 @delivery      varchar(20)
    , @scantype     int
    , @domain		varchar(4) = ''
    , @ip			varchar(20) = ''
AS
Begin

	Declare  @rcp_date datetime 
	        ,@qc_date datetime
			,@error int
			,@prh_rcp_ip varchar(20)
			,@ipDomain varchar(4)
			,@prh_rcp_domain varchar(4)
			
	Set @error = 0
	--根据客户端IP获取所在的域
	Select @ipDomain = dbo.fn_getDomainFromIp(@ip)
	
	Select @rcp_date = prh_rcp_date, @qc_date = prh_qc_date, @prh_rcp_ip = prh_rcp_ip
	From prh_hist
	Where prh_receiver = @delivery
		And (@domain = '' Or (@domain <> '' And prh_domain = @domain)) 		
	--根据第一次扫描的IP，获取第一次扫描的域	
	Select @prh_rcp_domain = dbo.fn_getDomainFromIp(@prh_rcp_ip)
	
	Begin Transaction updateDelivery
	--第一次进厂扫描
	If(@scantype = 0 and @rcp_date is null)
	Begin
		Update prh_hist
		Set prh_rcp_date = GETDATE()
			, prh_rcp_ip = @ip
		Where prh_receiver = @delivery
			And (@domain = '' Or (@domain <> '' And prh_domain = @domain))
		If(@@ERROR <> 0) set @error = -1
	End
	--异地二次扫描
	If(@scantype = 0 and @rcp_date is not null and @ipDomain <> @prh_rcp_domain)
	Begin 
		Update prh_hist
		Set  prh_rcp_last_date = @rcp_date
			,prh_rcp_last_ip = @prh_rcp_ip
			,prh_rcp_date = GETDATE()
			,prh_rcp_ip = @ip 
		Where prh_receiver = @delivery
			And (@domain = '' Or (@domain <> '' And prh_domain = @domain))
		If(@@ERROR <> 0) set @error = -2
	End	
	--质检扫描
	If(@scantype = 1 and @qc_date is null)
	Begin
		Update prh_hist
		Set prh_qc_date = GETDATE()
			, prh_qc_ip = @ip
		Where prh_receiver = @delivery
			And (@domain = '' Or (@domain <> '' And prh_domain = @domain))
		If(@@ERROR <> 0) set @error = -3
	End
	
	If(@@ERROR <> 0 Or @error < 0)
	Begin
		Rollback Transaction updateDelivery
	End
	Else
	Begin
		Commit Transaction updateDelivery
		--如果扫描成功，进行初始化数据
		Insert Into tcpc0..wh_prh_hist(prh_receiver,prh_nbr,prh_rcp_date,prh_insp_date,prh_qc_date,prh_consignment,prh_domain)
		Select prh_receiver,prh_nbr,prh_rcp_date,prh_insp_date,prh_qc_date,prh_consignment,prh_domain
		From TCPC_Supplier..prh_hist
		Where prh_receiver=@delivery 

		Update tcpc0..wh_prh_hist
		Set prh_createBy=13,prh_createDate=GETDATE(),prh_createName=N'管理员',prh_process=0
		Where prh_receiver=@delivery

		Declare @po_nbr varchar(8)
		Select @po_nbr=prh_nbr
		From TCPC_Supplier..prh_hist
		Where prh_receiver=@delivery 

		Insert Into tcpc0..wh_prh_det(prd_nbr,prd_po_nbr,prd_line,prd_part,prd_xpart,prd_um,prd_qty_ord,prd_qty_dev,prd_box_ent,prd_box_sca,prd_factory)
		Select prd_nbr,prd_po_nbr,prd_line,prd_part,prd_xpart,prd_um,prd_qty_ord,prd_qty_dev,prd_box_ent,prd_box_sca,prd_factory
		From TCPC_Supplier..prh_det
		where prd_nbr=@delivery and prd_po_nbr=@po_nbr

		Update tcpc0..wh_prh_det
		Set prd_createBy=13,prd_createDate=GETDATE(),prd_createName=N'管理员'
		where prd_nbr=@delivery and prd_po_nbr=@po_nbr
	End
	
End


