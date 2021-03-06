USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcu_insertPriceToPcMstr]    Script Date: 2015/7/6 11:28:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<fanwei>
-- Create date: <20150507>
-- Description:	<数据导入插入到pc_mstr>
-- =============================================
/*
DECLARE @returnVal int  = 0 
EXEC sp_pcu_insertPriceToPcMstr N'铜价2.5k',13,'D7F56313-0A3C-4DFF-A6B2-A77CAD417F17',@returnVal output
print @returnVal
*/
ALTER PROCEDURE [dbo].[sp_pcu_insertPriceToPcMstr]
	@type NVARCHAR(200)
	, @uID INT
	, @calendarID UNIQUEIDENTIFIER
	, @returnVal INT  OUTPUT  
AS
Begin
	/*
		前提：
			新铜价，要走价格新增流程。
		约定：原价格期间 = 原期间
			  新价格期间 = 新期间
		参数：
			 @returnVal = 0是系统错误 
					   = 1数据正常，修改导入100系统pc_mstr且导出cimload
					   = 2数据中有结束时间不对的项目
					   = 3 导出已经导出期间的cimload
		1、允许重复导cimload。但，连续两个月、第二个月已导入的情况下，不予执行
		2、错误情况：
			a、原期间任意截止日期超过新期间开始日期的（请挂起，下月再进）
		    b、
		3、规定：
			a、原期间截止日期，落在新期间上的：价格生效日期取原期间截止日期，价格截止日期取信价格截止日期
			b、原期间截止日期，位于新期间左侧：价格生效日期取原价格截止日期后一天，价格截止日期取信价格截止日期
		
	*/


	DECLARE @error INT
		, @pc_date datetime --pc_mstr统一插入时间，以便差异化取出当前修改的记录

	Set @error = 0
	Set @returnVal = -1
	Set @pc_date = GetDate()

	DECLARE @last_record TABLE (
		id INT,pc_part VARCHAR(18), pc_list VARCHAR(10), pc_curr VARCHAR(5), pc_um VARCHAR(2)
		, pc_amt VARCHAR(100), pc_price DECIMAL(18,5), pc_start DATETIME, pc_expire DATETIME
	)
	--2，首先判断，calendarID对应的期间是否已经导出过
	--		是。那么直接通过数据查出，不插入pc_mstr表
	If( Exists(	Select *
				From dbo.pcu_calendar
				Where pcus_type = @type
					And pcus_id = @calendarID
			   )
		And Not Exists(	Select *
						From dbo.pcu_calendar
						Where Isnull(pcus_isCimload, 0) = 1
						And pcus_type = @type
						And pcus_endDate > (Select pcus_endDate From dbo.pcu_calendar Where pcus_id = @calendarID)
					   )
	  )
	Begin
			
		DECLARE @beginDate DATETIME
			, @endDate DATETIME
			, @commodityPrice DECIMAL(18,5)

			, @sectionID UNIQUEIDENTIFIER
		
		Select @beginDate = Max(pcus_starDate)
			, @endDate = Max(pcus_endDate)
			, @commodityPrice = Max(pcus_price)
		From dbo.pcu_calendar
		Where pcus_type = @type
			And pcus_id = @calendarID

		Select @sectionID = pcud_id
		From dbo.pcu_section
		Where pcud_type = @type
			And pcud_section_start <= @commodityPrice
			And pcud_section_end >= @commodityPrice

		--2.a、原期间任意截止日期超过新期间开始日期的（请挂起，下月再进）
		If Exists(
					Select Top 1 *
					From dbo.PC_mstr
					Where Exists(Select * From pcu_part_vend_type Where pcutv_vend = pc_list And pcutv_part = pc_part And Isnull(pcutv_isHang, 0) = 0)
						And Isnull(pc_expire, @endDate) > @endDate
				 )
		Begin
			Set @returnVal = 2 --数据中有结束时间不对的项目，请联系管理员
		End
		Else
		Begin
			
			Begin Tran
			/*
				原期间，和新期间比较情况：
				1、新期间包含原期间时：
					a、原期间的开始日期大于新期间的开始日期：
						原期间的截止日期，要么取自身，或取原期间开始日期的
						新期间的开始日期，取原期间开始日期的后一天，或原期间截止日期的后一天，取决于是否为空
				2、原期间位于新期间左侧时：
					a、原期间的开始日期小于新期间的开始日期
				3、由于价格可能存在多个域中，每个域原则上相同的价格也可能因为维护的原则，导致生效日期不对应
					原则上，应在此之前判断各域价格的相同情况
			*/
				Declare @domain varchar(10)
				Set @domain = ''

				Declare pp_cursor Cursor For
				
					Select domain = 'SZX'
					Union
					Select domain = 'ZQL'
					Union
					Select domain = 'ZQZ'
					Union
					Select domain = 'YQL'
					Union
					Select domain = 'HQL'

				Open pp_cursor
				Fetch Next From pp_cursor Into @domain
				While @@Fetch_Status=0
				Begin
					--获取最后一个价格
					DELETE FROM @last_record
					INSERT INTO @last_record
					Select id, pc_part, pc_list, pc_curr, pc_um, pc_amt, pc_price, pc_start, pc_expire
					From (
						Select id = pc_id, pc_part, pc_list, pc_curr, pc_um, pc_amt, pc_price, pc_start, pc_expire
							, number = Row_Number() Over (Partition By pc_part, pc_list Order By Isnull(pc_expire, '3000-1-1' ) Desc)
						From dbo.PC_mstr  
						Inner Join dbo.pcu_part_vend_type ppvt On pc_list = ppvt.pcutv_vend And pc_part = ppvt.pcutv_part 
						Where ppvt.pcutv_type = @type
							And pc_domain = @domain
							And Isnull(pcutv_isHang, 0) = 0
					) AS son 
					Where number = 1
						--不参与计算：原期间的截止日期 = 新期间的截止日期
						And (pc_expire Is Null Or pc_expire < @endDate)
					--按域更新老价格
					UPDATE pm
					Set pm.pc_expire = Case When pm.pc_start >= @beginDate Then Isnull(pm.pc_expire, pm.pc_start) 
										Else Isnull(pm.pc_expire, Dateadd(Day, -1, @beginDate))
									End
						, pc_date = @pc_date
					From dbo.PC_mstr pm 
					Where Exists(Select * From @last_record Where id = pm.pc_id)
						--排除掉原期间和新期间相同的
						And Not (pc_start = @beginDate And pc_expire = @endDate)
					IF(@@error <> 0) SET @error = -1
					--按域新增价格
					Insert Into pc_mstr(pc_list, pc_part, pc_um, pc_price, pc_price1, pc_amt, pc_start, pc_expire, pc_domain, pc_curr, pc_date)
					Select pcups_vend, pcups_part, Isnull(pt_um, 'EA')
						, pc_price = Cast(pcups.pcups_price  / 1.17 as decimal(18, 5))
						, pcups.pcups_price
						, pc_amt = Cast(Cast(pcups.pcups_price  / 1.17 as decimal(18, 5)) As VARCHAR(100)) + ';0;0;0;0;0;0;0;0;0;0;0;0;0;0'
						, pc_start = Case When Isnull(rec.pc_start, @beginDate) > @beginDate 
										Then Isnull(Dateadd(Day, 1, rec.pc_expire), Isnull(Dateadd(Day, 1, rec.pc_start), @beginDate))
										Else Isnull(Dateadd(Day, 1, rec.pc_expire), @beginDate)
									 End
						, NULL --pc_expire = @endDate
						, @domain, Isnull(vd_curr, 'RMB')
						, pc_date = @pc_date
					From (
						Select *
						From pcu_price_section
						Where pcud_id = @sectionID
					) pcups
					Left Join Qad_data.dbo.pt_mstr pt On pt_domain = @domain And pt_part = pcups_part
					Left Join Qad_Data.dbo.vd_mstr ad On vd_domain = @domain And vd_addr = pcups_vend
					Left Join @last_record rec On pcups.pcups_vend = rec.pc_list And pcups.pcups_part = rec.pc_part
					Where pt_part Is Not Null
						And pcups.pcups_type = @type
						--排除掉原期间和新期间相同的
						And Not (rec.pc_start = @beginDate And rec.pc_expire = @endDate)
						--不参与计算：原期间的开始日期 = 新期间的开始日期
						And (Isnull(rec.pc_start, Dateadd(Day, -1, @beginDate)) <> @beginDate)

					IF(@@error <> 0) SET @error = -1

				Fetch Next From pp_cursor Into @domain
				End
				Close pp_cursor
				Deallocate pp_cursor

				--将当前期间修改成为不活跃期间
				UPDATE dbo.pcu_calendar
				Set pcus_isCimload = 1
					, pcus_CimloadBy = @uID
					, pcus_CimloadDate = @pc_date
				Where pcus_type = @type
					And pcus_id = @calendarID
				If(@@error <> 0) Set @error = -1
				
			If(@@error<>0 OR @error < 0)
			Begin
				Rollback Tran
				Set @returnVal = 0
			End
			Else
			Begin
				Commit Tran
				Set @returnVal = 1
			End
			
		End
	End
	Else
	Begin
		Set @returnVal = 3 --不存在没有导入价格的期间
	END
    

	IF(@returnVal = 1 OR @returnVal = 3)
	begin
		Select pc_list, pc_curr, pc_empty1 = '', pc_part, pc_um, pc_start, pc_expire, pc_empty2 = 'p', pc_empty3 = 1
			, pc_price = MAX(pc_price), pc_price1 = MAX(pc_price1) 
		From dbo.PC_mstr p 
		Inner Join dbo.pcu_part_vend_type ppvt On ppvt.pcutv_part = p.pc_part And ppvt.pcutv_vend = p.pc_list
		Inner Join dbo.pcu_calendar pc On  p.pc_expire = pc.pcus_endDate
		Where ppvt.pcutv_type = @type 
			And p.pc_expire=(	Select MAX(pcus_endDate)
								From pcu_calendar
								Where pcus_type = @type
									And pcus_id = @calendarID
							)
			And pc.pcus_id = @calendarID
			And pc.pcus_type = @type
			And Isnull(pcutv_isHang, 0) = 0
			AND pc_domain = 'SZX'
			And pc_date = @pc_date
		Group By pc_list,pc_curr, pc_part, pc_um, pc_start, pc_expire 
		Order By pc_list, pc_part

		Select pc_list, pc_curr, pc_empty1 = '', pc_part, pc_um, pc_start, pc_expire, pc_empty2 = 'p', pc_empty3 = 1
			, pc_price = MAX(pc_price), pc_price1 = MAX(pc_price1) 
		From dbo.PC_mstr p 
		Inner Join dbo.pcu_part_vend_type ppvt On ppvt.pcutv_part = p.pc_part And ppvt.pcutv_vend = p.pc_list
		Inner Join dbo.pcu_calendar pc On  p.pc_expire = pc.pcus_endDate
		Where ppvt.pcutv_type = @type 
			And p.pc_expire=(	Select MAX(pcus_endDate)
								From pcu_calendar
								Where pcus_type = @type
									And pcus_id = @calendarID
							)
			And pc.pcus_id = @calendarID
			And pc.pcus_type = @type
			And Isnull(pcutv_isHang, 0) = 0
			AND pc_domain = 'ZQL'
			And pc_date = @pc_date
		Group By pc_list,pc_curr, pc_part, pc_um, pc_start, pc_expire 
		Order By pc_list, pc_part

		Select pc_list, pc_curr, pc_empty1 = '', pc_part, pc_um, pc_start, pc_expire, pc_empty2 = 'p', pc_empty3 = 1
			, pc_price = MAX(pc_price), pc_price1 = MAX(pc_price1) 
		From dbo.PC_mstr p 
		Inner Join dbo.pcu_part_vend_type ppvt On ppvt.pcutv_part = p.pc_part And ppvt.pcutv_vend = p.pc_list
		Inner Join dbo.pcu_calendar pc On  p.pc_expire = pc.pcus_endDate
		Where ppvt.pcutv_type = @type 
			And p.pc_expire=(	Select MAX(pcus_endDate)
								From pcu_calendar
								Where pcus_type = @type
									And pcus_id = @calendarID
							)
			And pc.pcus_id = @calendarID
			And pc.pcus_type = @type
			And Isnull(pcutv_isHang, 0) = 0
			AND pc_domain = 'ZQZ'
			And pc_date = @pc_date
		Group By pc_list,pc_curr, pc_part, pc_um, pc_start, pc_expire 
		Order By pc_list, pc_part

		Select pc_list, pc_curr, pc_empty1 = '', pc_part, pc_um, pc_start, pc_expire, pc_empty2 = 'p', pc_empty3 = 1
			, pc_price = MAX(pc_price), pc_price1 = MAX(pc_price1) 
		From dbo.PC_mstr p 
		Inner Join dbo.pcu_part_vend_type ppvt On ppvt.pcutv_part = p.pc_part And ppvt.pcutv_vend = p.pc_list
		Inner Join dbo.pcu_calendar pc On  p.pc_expire = pc.pcus_endDate
		Where ppvt.pcutv_type = @type 
			And p.pc_expire=(	Select MAX(pcus_endDate)
								From pcu_calendar
								Where pcus_type = @type
									And pcus_id = @calendarID
							)
			And pc.pcus_id = @calendarID
			And pc.pcus_type = @type
			And Isnull(pcutv_isHang, 0) = 0
			AND pc_domain = 'YQL'
			And pc_date = @pc_date
		Group By pc_list,pc_curr, pc_part, pc_um, pc_start, pc_expire 
		Order By pc_list, pc_part

		Select pc_list, pc_curr, pc_empty1 = '', pc_part, pc_um, pc_start, pc_expire, pc_empty2 = 'p', pc_empty3 = 1
			, pc_price = MAX(pc_price), pc_price1 = MAX(pc_price1) 
		From dbo.PC_mstr p 
		Inner Join dbo.pcu_part_vend_type ppvt On ppvt.pcutv_part = p.pc_part And ppvt.pcutv_vend = p.pc_list
		Inner Join dbo.pcu_calendar pc On  p.pc_expire = pc.pcus_endDate
		Where ppvt.pcutv_type = @type 
			And p.pc_expire=(	Select MAX(pcus_endDate)
								From pcu_calendar
								Where pcus_type = @type
									And pcus_id = @calendarID
							)
			And pc.pcus_id = @calendarID
			And pc.pcus_type = @type
			And Isnull(pcutv_isHang, 0) = 0
			AND pc_domain = 'HQL'
			And pc_date = @pc_date
		Group By pc_list,pc_curr, pc_part, pc_um, pc_start, pc_expire 
		Order By pc_list, pc_part
	end
End

