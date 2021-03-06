USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_Hr_AttDinerCompareDetail]    Script Date: 04/02/2015 11:33:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bob
-- Create date: 07/26/11
-- Description:	sp_Hr_AttDinerCompareDetail
-- =============================================
ALTER PROCEDURE [dbo].[sp_Hr_AttDinerCompareDetail]
@year int,
@month int,
@department int,
@userID int,
@PlantCode int,
@Type int
AS

Begin
	DECLARE @SQL AS NVARCHAR(Max)
            IF (@Type =0)
            BEGIN
                SET @SQL = N' SELECT u.userNO,u.userName,u.enterdate as uenter,u.leavedate as uleave,ISNULL(d.name,'''') as dname,t.checkTime as DinerDate,t.checkTime as DinerTime,c.starttime,c.endtime,c.totalhr,t.sensorid as cardnum FROM  '
				SET @SQL =@SQL + N' (SELECT CASE WHEN l.userID IS NULL THEN a.userID ELSE l.userID END as userID,CASE WHEN checkdate IS NULL THEN attdate ELSE checkdate END as cdate FROM '
				SET @SQL =@SQL + N' (SELECT DinnerUserID as userID,Count(DinnerID) as dnum,Convert(varchar(10),CheckTime,120)  as checkdate  FROM tcpc0.dbo.hr_dinnerinfo Where Year(CheckTime)='''+ CAST(@Year as Nvarchar(4)) +''' and Month(CheckTime)='''+ CAST(@month as Nvarchar(4)) +''' and plantID='''+ CAST(@PlantCode as Nvarchar(2)) +''' and  DinnerUserID='''+  CAST(@userID As Nvarchar(10)) +''' Group By DinnerUserID,Convert(varchar(10),CheckTime,120)) as l '
				SET @SQL =@SQL + N' Full outer join '
				SET @SQL =@SQL + N' (SELECT userID,CASE WHEN SUM(totalhr) >=8 THEN 1 ELSE 0 END as totalhr,Convert(varchar(10),starttime,120) as attdate FROM tcpc0.dbo.hr_Attendance_calendar Where uyear='''+ CAST(@Year as Nvarchar(4)) +''' AND umonth='''+ CAST(@month as Nvarchar(4)) +''' AND plantid='''+ CAST(@PlantCode as Nvarchar(2)) +''' and userID ='''+  CAST(@userID As Nvarchar(10)) +''' GROUP BY userID,CONVERT(varchar(10), starttime, 120)) as a '
				SET @SQL =@SQL + N'  ON a.userID =l.userID and checkdate = attdate '
				SET @SQL =@SQL + N' Where (ISNULL(a.totalhr,0)=0 and ISNULL(dnum,0)>0) or (a.totalhr>0 and dnum>totalhr)) as m '
				SET @SQL =@SQL + N'  INNER JOIN tcpc0.dbo.Users u ON u.userID =m.userID '
				SET @SQL =@SQL + N'  LEFT OUTER JOIN tcpc'+ CAST(@PlantCode as Nvarchar(2)) +'.dbo.departments d ON d.departmentID =u.departmentID '
				SET @SQL =@SQL + N' LEFT OUTER JOIN (SELECT DinnerUserID as userID,Convert(varchar(10),CheckTime,120)  as tdate, checkTime, sensorid  FROM tcpc0.dbo.hr_dinnerinfo Where Year(CheckTime)='''+ CAST(@Year as Nvarchar(4)) +''' and Month(CheckTime)='''+ CAST(@month as Nvarchar(4)) +''' and plantID='''+ CAST(@PlantCode as Nvarchar(2)) +''' and  DinnerUserID='''+  CAST(@userID As Nvarchar(10)) +''') as  t ON t.userID =u.userID and t.tdate =cdate '
				SET @SQL =@SQL + N' LEFT OUTER JOIN (SELECT userID,Convert(varchar(10),starttime,120) as adate,starttime,endtime,totalhr FROM tcpc0.dbo.hr_Attendance_calendar Where uyear='''+ CAST(@Year as Nvarchar(4)) +''' AND umonth='''+ CAST(@month as Nvarchar(4)) +''' AND plantid='''+ CAST(@PlantCode as Nvarchar(2)) +''' and userID ='''+  CAST(@userID As Nvarchar(10)) +''') as c ON c.userID =u.userID and c.adate =cdate '
                --SET @SQL =@SQL + N' LEFT OUTER JOIN tcpc0.dbo.hr_DinnerDevice h ON h.'
                --SET @SQL =@SQL + N' WHERE u.userID=''' + CAST(@userID As Nvarchar(10))+ ''' '
				SET @SQL =@SQL + N' ORDER BY cdate'

                    
             END  -- @Type =0
             ELSE
             BEGIN
                IF(@Type =2)
                Begin
                    --SET @SQL = N' SELECT u.userNO,u.userName,Convert(varchar(10),u.enterdate,120)  as uenter,u.leavedate as uleave,ISNULL(d.name,'''') as dname,Convert(varchar(10),t.checkTime,120) as DinerDate,CONVERT(varchar(12) , t.checkTime, 108 ) as DinerTime,c.starttime,c.endtime,c.totalhr,t.sensorid as cardnum FROM  '
SET @SQL = N' SELECT u.userNO,u.userName,u.enterdate as uenter,u.leavedate as uleave,ISNULL(d.name,'''') as dname, w.name as wname, t.checkTime as DinerDate,t.checkTime as DinerTime,c.starttime,c.endtime,c.totalhr,t.sensorid as cardnum FROM  '
					SET @SQL =@SQL + N' (SELECT CASE WHEN l.userID IS NULL THEN a.userID ELSE l.userID END as userID,CASE WHEN checkdate IS NULL THEN attdate ELSE checkdate END as cdate FROM '
					SET @SQL =@SQL + N' (SELECT DinnerUserID as userID,Count(DinnerID) as dnum,Convert(varchar(10),CheckTime,120)  as checkdate  FROM tcpc0.dbo.hr_dinnerinfo Where Year(CheckTime)='''+ CAST(@Year as Nvarchar(4)) +''' and Month(CheckTime)='''+ CAST(@month as Nvarchar(4)) +''' and plantID='''+ CAST(@PlantCode as Nvarchar(2)) +'''  Group By DinnerUserID,Convert(varchar(10),CheckTime,120)) as l '
					SET @SQL =@SQL + N' Full outer join '
					SET @SQL =@SQL + N' (SELECT userID,CASE WHEN SUM(totalhr) >=8 THEN 1 ELSE 0 END as totalhr,Convert(varchar(10),starttime,120) as attdate FROM tcpc0.dbo.hr_Attendance_calendar Where uyear='''+ CAST(@Year as Nvarchar(4)) +''' AND umonth='''+ CAST(@month as Nvarchar(4)) +''' AND plantid='''+ CAST(@PlantCode as Nvarchar(2)) +'''  GROUP BY userID,CONVERT(varchar(10), starttime, 120)) as a '
					SET @SQL =@SQL + N'  ON a.userID =l.userID and checkdate = attdate '
					SET @SQL =@SQL + N' Where (ISNULL(a.totalhr,0)=0 and ISNULL(dnum,0)>0) or (a.totalhr>0 and dnum>totalhr)) as m '
					SET @SQL =@SQL + N'  INNER JOIN tcpc0.dbo.Users u ON u.userID =m.userID '
					SET @SQL =@SQL + N'  LEFT OUTER JOIN tcpc'+ CAST(@PlantCode as Nvarchar(2)) +'.dbo.departments d ON d.departmentID =u.departmentID '
					SET @SQL =@SQL + N' LEFT OUTER JOIN (SELECT DinnerUserID as userID,Convert(varchar(10),CheckTime,120)  as tdate, checkTime, sensorid  FROM tcpc0.dbo.hr_dinnerinfo Where Year(CheckTime)='''+ CAST(@Year as Nvarchar(4)) +''' and Month(CheckTime)='''+ CAST(@month as Nvarchar(4)) +''' and plantID='''+ CAST(@PlantCode as Nvarchar(2)) +''' ) as  t ON t.userID =u.userID and t.tdate =cdate '
					SET @SQL =@SQL + N' LEFT OUTER JOIN (SELECT userID,Convert(varchar(10),starttime,120) as adate,starttime,endtime,totalhr FROM tcpc0.dbo.hr_Attendance_calendar Where uyear='''+ CAST(@Year as Nvarchar(4)) +''' AND umonth='''+ CAST(@month as Nvarchar(4)) +''' AND plantid='''+ CAST(@PlantCode as Nvarchar(2)) +''' ) as c ON c.userID =u.userID and c.adate =cdate '
					SET @SQL =@SQL + N' LEFT OUTER JOIN tcpc'+ CAST(@PlantCode as NVARCHAR(2))+'.dbo.workshop w ON w.id= u.workshopID '
					--SET @SQL =@SQL + N' LEFT OUTER JOIN tcpc0.dbo.hr_DinnerDevice h ON h.'
					--SET @SQL =@SQL + N' WHERE u.userID=''' + CAST(@userID As Nvarchar(10))+ ''' '
					SET @SQL =@SQL + N' ORDER BY d.departmentID,u.userID,cdate'
                End
                Else
					SET @SQL = N'<b>工号</b>~^<b>姓名</b>~^<b>入公司日期</b>~^<b>离职日期</b>~^140^<b>部门</b>~^<b>工段</b>~^<b>就餐日期</b>~^<b>就餐时间</b>~^<b>上班时间</b>~^<b>下班时间</b>~^<b>考勤小时</b>~^<b>餐卡设备编号</b>~^'
               
           
        END



                
  --EXEC (@SQL)
  select(@SQL)
End
