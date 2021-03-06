USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_hr_CreateCalendarOri]    Script Date: 2015-06-25 11:28:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ye Bin
-- Create date: 2010/06/18
-- Description:	sp_hr_CreateCalendarOri
-- =============================================
ALTER Procedure [dbo].[sp_hr_CreateCalendarOri]
@plantid int
As

--exec sp_hr_clearCalendar @plantid

Declare @err int
Set @err=0

Declare @cc varchar(4)
Declare @kid int
Declare @uid int
Declare @uno varchar(20)
Declare @uname nvarchar(20)
Declare @utype int
Declare @ktype varchar(5)
declare @time datetime

Declare @kid5 int
Declare @uid5 int
Declare @uno5 varchar(20)
Declare @uname5 nvarchar(20)
Declare @utype5 int
Declare @ktype5 varchar(5)

Set @kid5=-1
Set @uid5 = -1
Declare @st int
Set @st=0

declare @dd1 datetime
declare @dd2 datetime

set @dd2=DateAdd(dd, 1, getdate())
set @dd1=getdate()

if day(@dd1)<7
begin
  set @dd1=DateAdd(MM, -1, @dd1)
  Set @dd2 = Cast(Cast(Year(@dd2) As char(4)) + '-' + Cast(Month(@dd2) As char(2)) + '-02' As Datetime)
end

Set @dd1 = Cast(Cast(Year(@dd1) As char(4)) + '-' + Cast(Month(@dd1) As char(2)) + '-01' As Datetime)
--Set @dd2 = Cast(Cast(Year(@dd2) As char(4)) + '-' + Cast(Month(@dd2) As char(2)) + '-' + Cast(Day(@dd2) As char(2)) As Datetime)

/*
if day(@dd1)<7
begin
  set @dd1=DateAdd(dd,-20,@dd1)
end

while day(@dd1)>1
begin
  set @dd1=DateAdd(dd,-1,@dd1)
end

while DATEPART(hh, @dd1)>0
begin
  set @dd1=DateAdd(hh,-1,@dd1)
end
while DATEPART(mi, @dd1)>0
begin
  set @dd1=DateAdd(mi,-1,@dd1)
end
while DATEPART(ss, @dd1)>0
begin
  set @dd1=DateAdd(ss,-1,@dd1)
end
*/

declare @time1 datetime
declare @time2 datetime
set @time1='1900-01-01'
set @time2='1900-01-01'

declare @timeinte float

Declare @cc51 varchar(4)
Declare @cc52 varchar(4)

Begin Transaction
    Delete from tcpc0.dbo.hr_Attendance_calendar_ori
    where plantID=@plantid and uyear=year(@dd1) and umonth=month(@dd1)
        
	Declare log_cursor Cursor For
    select AttendanceUserNo,checktype,checkTime,AttendanceUserID,AttendanceUserCode,AttendanceUserName,AttendanceUserType,AttendanceUserCenter 
    from tcpc0.dbo.hr_AttendanceInfo 
    where checkTime>=@dd1 and checkTime<= @dd2
    and plantID=@plantid
    and isnull(isDisable,0)=0 And AttendanceUserID Is Not Null
    order by AttendanceUserCenter,AttendanceUserID,checkTime 
	Open log_cursor
	Fetch Next From log_cursor Into @kid, @ktype, @time, @uid, @uno,@uname,@utype,@cc
	While @@Fetch_Status=0
	Begin
      --换人
      if (@uid5 <> -1 and @uid5 <> @uid)
      begin
         if(@time1 <>'1900-01-01' and @time2 <> '1900-01-01' and @cc51=@cc52)
         begin
           set @timeinte = (Datediff(ss,@time1,@time2)-1) / 3600.0
           if (@timeinte > 24)
           begin
               set @timeinte = 0
           end
           else 
           begin
               if (@timeinte >= 12)
               begin
                   if(@plantid = 1 Or @plantid = 2) set @timeinte = @timeinte - 1.5
				   else set @timeinte = @timeinte - 1
               end
               else
               begin 
				  if (@timeinte >= 5 And @plantid = 1)
				  begin
					set @timeinte = @timeinte - 1
				  end
				  else
				  begin 
					if (@timeinte >= 4 and @plantid <> 1)
                    begin
					  --if(@plantid = 1 Or @plantid = 2) set @timeinte = @timeinte - 1
					  if(@plantid = 2) set @timeinte = @timeinte - 1
					  else set @timeinte = @timeinte - 0.5
					end
				  end
               end
           end  
           if (@timeinte > 0)
           begin
              --if not Exists(select userid from tcpc0.dbo.hr_Attendance_calendar
              --where plantid=@plantid and userid=@uid5 
              --and year(starttime)=year(@time1) 
              --and month(starttime)=month(@time1) 
              --and day(starttime)=day(@time1)
              --and @time1 < endtime)
              --begin  
                Insert into tcpc0.dbo.hr_Attendance_calendar_ori(plantid,cc,userid,userno,username,starttime,endtime,totalhr,fingerprint,createdby,createddate,usertype,uyear,umonth,utype)
                values(@plantid,@cc51,@uid5,@uno5,@uname5,@time1,@time2,@timeinte,@kid5,0,getdate(),@utype5,year(@time1),month(@time1),1)
          	    If(@@error<>0)	Set @err=-1
          	    
          	    Set @timeinte = 0
              --end
		   end
        end  
        set @st = 0
        set @time1='1900-01-01'
        set @time2='1900-01-01'
        Set @kid5=-1
        set @cc51=''
        set @cc52=''
      end  --换人

      if (@ktype = 'I')
      begin
         if (@st = 0)
         begin
            set @st = 1
            set @time1=@time
            set @time2='1900-01-01'
            set @cc51=@cc
            set @cc52=''
         end
         else 
         begin  
           if (@st = 1)
           begin 
             set @st = 1
             set @time1=@time
             set @time2='1900-01-01'
             set @cc51=@cc
             set @cc52=''
           end
           else 
           begin 
             if (@st = 2)
             begin
               if(@time1 <>'1900-01-01' and @time2 <> '1900-01-01' and @cc51=@cc52)
               begin
                  set @timeinte = (Datediff(ss,@time1,@time2)-1) / 3600.0
                  
                  --select @uname5,@time1,@time2,@timeinte
                  if (@timeinte > 24)
                  begin
                    set @timeinte = 0
                  end
                  else 
                  begin
                    if (@timeinte >= 12)
                    begin
                      if(@plantid = 1 Or @plantid = 2) set @timeinte = @timeinte - 1.5
					  else set @timeinte = @timeinte - 1
                    end
                    else
                    begin  
                      if (@timeinte >= 5 And @plantid = 1)
				      begin
					    set @timeinte = @timeinte - 1
				      end
				      else
				      begin
                        if (@timeinte >= 4 and @plantid <> 1)
                        begin
                          --if(@plantid = 1 Or @plantid = 2) set @timeinte = @timeinte - 1
                          if(@plantid = 2) set @timeinte = @timeinte - 1
						  else set @timeinte = @timeinte - 0.5
						end
                      end
                    end
                  end  

                  if (@timeinte > 0)
                  begin
              --if not Exists(select userid from tcpc0.dbo.hr_Attendance_calendar
              --where plantid=@plantid and userid=@uid5 
              --and year(starttime)=year(@time1) 
              --and month(starttime)=month(@time1) 
              --and day(starttime)=day(@time1)
              --and @time1 < endtime)
              --begin  
                Insert into tcpc0.dbo.hr_Attendance_calendar_ori(plantid,cc,userid,userno,username,starttime,endtime,totalhr,fingerprint,createdby,createddate,usertype,uyear,umonth,utype)
                values(@plantid,@cc51,@uid5,@uno5,@uname5,@time1,@time2,@timeinte,@kid5,0,getdate(),@utype5,year(@time1),month(@time1),2)
          	    If(@@error<>0)	Set @err=-1
          	    
          	    Set @timeinte = 0
              --end
    		      end
                end
                set @st = 1
                set @time1=@time
                set @time2='1900-01-01'
                set @cc51=@cc
                set @cc52=''
             end   
           end
         end
      end  
      else 
      begin
        if (@ktype = 'O')
        begin
          if (@st = 0)
             set @st=0
          else 
          begin 
            if (@st = 1)
            begin
              set @st = 2
              set @time2=@time
              set @cc52=@cc
            end
            else
            begin  
              if (@st = 2)
              begin
                set @st = 2
                --set @time2=@time
              end
            end
          end
        end
      end   

      set @kid5 =@kid
      set @uid5 =@uid
      set @uno5 =@uno
      set @uname5 =@uname
      set @utype5 =@utype
      set @ktype5 =@ktype

    Fetch Next From log_cursor Into  @kid, @ktype, @time, @uid, @uno,@uname,@utype,@cc
	End
	Close log_cursor
	Deallocate log_cursor

    if @uid5<> -1
    begin 
         if(@time1 <>'1900-01-01' and @time2 <> '1900-01-01' and @cc51=@cc52)
         begin
           set @timeinte = (Datediff(ss,@time1,@time2)-1) / 3600.0
           if (@timeinte > 24)
           begin
               set @timeinte = 0
           end
           else 
           begin
               if (@timeinte >= 12)
               begin
                   if(@plantid = 1 Or @plantid = 2) set @timeinte = @timeinte - 1.5
				   else set @timeinte = @timeinte - 1
               end
               else
               begin  
                  if (@timeinte >= 5 And @plantid = 1)
				  begin
					set @timeinte = @timeinte - 1
				  end
				  else
				  begin
                    if (@timeinte >= 4 and @plantid <> 1)
                    begin
                      --if(@plantid = 1 Or @plantid = 2) set @timeinte = @timeinte - 1
                      if(@plantid = 2) set @timeinte = @timeinte - 1
					  else set @timeinte = @timeinte - 0.5
					end
                  end
               end
           end  
           if (@timeinte > 0)
           begin
              --if not Exists(select userid from tcpc0.dbo.hr_Attendance_calendar
              --where plantid=@plantid and userid=@uid5 
              --and year(starttime)=year(@time1) 
              --and month(starttime)=month(@time1) 
              --and day(starttime)=day(@time1)
              --and @time1 < endtime)
              --begin  
                Insert into tcpc0.dbo.hr_Attendance_calendar_ori(plantid,cc,userid,userno,username,starttime,endtime,totalhr,fingerprint,createdby,createddate,usertype,uyear,umonth,utype)
                values(@plantid,@cc51,@uid5,@uno5,@uname5,@time1,@time2,@timeinte,@kid5,0,getdate(),@utype5,year(@time1),month(@time1),3)
          	    If(@@error<>0)	Set @err=-1
          	    
          	    Set @timeinte = 0
              --end
		   end
        end  
        set @st = 0
        set @time1='1900-01-01'
        set @time2='1900-01-01'
        Set @kid5=-1
    end
		
If(@@Error<>0 or @err<0)
	Rollback Transaction
else
	Commit Transaction
		
Select @err