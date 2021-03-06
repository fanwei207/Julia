'* @@ NAME				:	KindSearcher.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for KindSearcher.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	September 8 2006
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class KindSearcher
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
    Dim reader As SqlDataReader
    Dim chk As New adamClass

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then

            year.Text = DateTime.Now.Year
            month.SelectedValue = DateTime.Now.Month
            wtype.Items.Add(New ListItem("计件", "0"))
            wtype.Items.Add(New ListItem("计时", "1"))

            atype.Items.Add(New ListItem("计件", "0"))
            atype.Items.Add(New ListItem("计时", "1"))
        End If
    End Sub

    Public Sub salary_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles worker.ServerClick
        Dim reduceValue As String = "0"
        If margin.Text.Trim <> "" Then
            If Not IsNumeric(margin.Text.Trim) Then
                ltlAlert.Text = "alert('差值只能为数字!');Form1.margin.focus();"
                Exit Sub
            Else
                reduceValue = margin.Text.Trim
            End If
        End If
        ltlAlert.Text = "window.open('/salary/attendanceExcelDouble.aspx?yr=" & year.Text & "&ye=" & month.SelectedValue & "&red=" & reduceValue & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') "
    End Sub

    Public Sub work_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wchecked.ServerClick
        Dim basic As String = "0"
        strSql = " Select monthlyWorkDays From BaseInfo "
        strSql &= " Where year(workDate)='" & year.Text.Trim() & "' and month(workDate)='" & month.SelectedValue & "'"
        basic = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

        Dim salarydate As DateTime = CDate(year.Text.Trim() & "/" & month.SelectedValue & "/1")
        Dim salaryenddate As DateTime
        If month.SelectedValue = 12 Then
            salaryenddate = CDate(year.Text.Trim() + 1 & "/1/1").AddDays(-1)
        Else
            salaryenddate = CDate(year.Text.Trim() & "/" & month.SelectedValue + 1 & "/1").AddDays(-1)
        End If

        Dim dd As String = year.Text.Trim() & "-" & month.SelectedValue & "-01"
        Dim ptable As String
        If DateDiff(DateInterval.Month, CDate(dd), DateTime.Now) > 3 Then
            'ptable = "PieceAttendence_his"
            ptable = "PieceAttendence"
        Else
            ptable = "PieceAttendence"
        End If

        strSql = " select  isnull(d.name,''),isnull(w.name,''),u.userNo,u.username,pa.days,pa.totalhours,isnull(dd.ductdays,0),isnull(sk.sickdays,0),u.enterDate,u.leaveDate FRom "
        strSql &= " (select usercode, sum(days) as days, sum(totalhours)/8 as totalhours From " & ptable & " Where year(workdate)='" & year.Text.Trim() & "' and month(workdate)='" & month.SelectedValue & "'  Group by usercode Having sum(days)<'" & basic & "') pa "
        'strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = pa.userCode and u.isTemp='" & Session("temp") & "'"
        strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = pa.userCode "
        strSql &= " left outer join departments d on d.departmentID=u.departmentID "
        strSql &= " left outer join workshop w on w.id=u.workshopID "
        strSql &= " Left Outer JOIN "
        strSql &= " (SELECT usercode, SUM(DATEDIFF([Day], startdate,enddate)) - CASE WHEN SUM(DATEDIFF([Day], startdate, '" & salarydate.ToString() & "')) > 0 THEN SUM(DATEDIFF([Day], startdate, '" & salarydate.ToString() & "')) ELSE 0 END - CASE WHEN SUM(DATEDIFF([Day], '" & salaryenddate.ToString() & "', enddate)) > 0 THEN SUM(DATEDIFF([Day], '" & salaryenddate.ToString() & "', enddate)) ELSE 0 END AS sickdays "
        strSql &= " FROM SickLeave WHERE (startdate <= '" & salaryenddate.ToString() & "') AND (enddate >= '" & salarydate.ToString() & "') GROUP BY usercode) sk on sk.usercode=u.userid "
        strSql &= " left outer join (SELECT dm.usercode, SUM(dm.deductNum) AS ductdays From DeductMoney  dm "
        strSql &= " INNER JOIN tcpc0.dbo.systemCode sc ON dm.typeID = sc.systemCodeID AND sc.systemCodeName = N'请假' "
        strSql &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Deduct Money Type' "
        strSql &= " Where year(dm.workdate)='" & year.Text.Trim() & "' and month(dm.workdate)='" & month.SelectedValue & "' Group by dm.usercode)dd on dd.usercode=u.userID"
        'Response.Write(strSql)
            'Exit Sub

Me.ExportExcel(chk.dsnx(), "200^<b>部门</b>~^120^<b>工段</b>~^<b>工号</b>~^<b>姓名</b>~^<b>出勤天</b>~^<b>小时天</b>~^<b>事假</b>~^<b>病假</b>~^100^<b>入公司日期</b>~^100^<b>离职日期</b>~^" , strSql, False)

    End Sub


    Public Sub leaves_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles leaves.ServerClick
        Dim gg As String = year.Text.Trim() & "." & month.SelectedValue

        Dim salarydate As DateTime = CDate(year.Text.Trim() & "/" & month.SelectedValue & "/1")
        Dim salaryenddate As DateTime
        If month.SelectedValue = 12 Then
            salaryenddate = CDate(year.Text.Trim() + 1 & "/1/1").AddDays(-1)
        Else
            salaryenddate = CDate(year.Text.Trim() & "/" & month.SelectedValue + 1 & "/1").AddDays(-1)
        End If

        Dim dd As String = year.Text.Trim() & "-" & month.SelectedValue & "-01"
        Dim ptable As String
        If DateDiff(DateInterval.Month, CDate(dd), DateTime.Now) > 3 Then
            'ptable = "PieceAttendence_his"
            ptable = "PieceAttendence"
        Else
            ptable = "PieceAttendence"
        End If

        strSql = " SELECT u.userNo, u.userName, ISNULL(dp.name,N'无'), ISNULL(ws.name,N'无'),'" & gg & "',ISNULL(pa.attendenceday,0)+ISNULL(aa.attdays,0),isnull(al.ddays,0),isnull(al.sdays,0),u.enterdate,u.leavedate FROM "
        strSql &= " (select isnull(d .ductdays, 0) as ddays, isnull(s2.days, 0) as sdays, CASE WHEN d .usercode IS NULL THEN s2.usercode ELSE d .usercode END AS Uid From "
        strSql &= " ( SELECT dm.usercode, SUM(dm.deductNum) AS ductdays From DeductMoney  dm "
        strSql &= " INNER JOIN tcpc0.dbo.systemCode sc ON dm.typeID = sc.systemCodeID AND sc.systemCodeName = N'请假' "
        strSql &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Deduct Money Type' "
        strSql &= " WHERE  dm.organizationID=" & Session("orgID") & " AND year(dm.workdate)='" & year.Text.Trim() & "' and month(dm.workdate)='" & month.SelectedValue & "' Group by dm.usercode,dm.typeID ) d "


        strSql &= " Full Outer JOIN "
        'query &= " (SELECT usercode, SUM(DATEDIFF([Day], startdate,enddate)) - CASE WHEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')) > 0 THEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')) ELSE 0 END - CASE WHEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)) > 0 THEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)) ELSE 0 END AS days "
        strSql &= " (SELECT usercode,SUM(CASE WHEN (enddate is not null) THEN DATEDIFF([Day], startdate,enddate)+1 ELSE 0 END) - CASE WHEN SUM(DATEDIFF([Day], startdate, '" & salarydate & "')) > 0 THEN SUM(DATEDIFF([Day], startdate, '" & salarydate & "')+1) ELSE 0 END - CASE WHEN SUM(DATEDIFF([Day], '" & salaryenddate & "', enddate)) > 0 THEN SUM(DATEDIFF([Day], '" & salaryenddate & "', enddate)+1) ELSE 0 END AS days "
        strSql &= " FROM SickLeave WHERE (startdate <= '" & salaryenddate & "') AND (enddate >= '" & salarydate & "') GROUP BY usercode) s2 on s2.usercode=d.usercode )as al "
        'strSql &= " INNER JOIN tcpc0.dbo.users u ON (al.Uid = u.userID) and u.isTemp='" & Session("temp") & "' "
        strSql &= " INNER JOIN tcpc0.dbo.users u ON al.Uid = u.userID "
        strSql &= " left outer join (select usercode, sum(days) as attendenceday From " & ptable & " Where year(workdate)='" & year.Text.Trim() & "' and month(workdate)='" & month.SelectedValue & "'  Group by usercode ) pa ON pa.usercode=al.Uid"
        strSql &= " left outer join (select usercode, sum(days) as attdays From Attendance Where year(workdate)='" & year.Text.Trim() & "' and month(workdate)='" & month.SelectedValue & "'  Group by usercode ) aa ON aa.usercode=al.Uid"

        strSql &= " LEFT OUTER JOIN Workshop ws ON u.workshopID = ws.id "
        strSql &= " LEFT OUTER JOIN departments dp ON u.departmentID = dp.departmentID "

            Me.ExportExcel(chk.dsnx(), "<b>工号</b>~^<b>姓名</b>~^200^<b>部门</b>~^200^<b>工段</b>~^<b>日期</b>~^<b>出勤天</b>~^<b>事假</b>~^<b>病假</b>~^100^<b>入公司日期</b>~^100^<b>离职日期</b>~^", strSql, False)
    End Sub

    Public Sub Analyse_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Analyse.ServerClick

        If startT.Text.Trim() = "" Then
            ltlAlert.Text = "alert('起始日期不能为空!');Form1.startT.focus();"
            Exit Sub
        Else
            If Not IsNumeric(startT.Text.Trim()) Then
                ltlAlert.Text = "alert('起始日期只能为数字!');Form1.startT.focus();"
                Exit Sub
            End If
        End If
        If endT.Text.Trim() = "" Then
            ltlAlert.Text = "alert('结束日期不能为空!');Form1.endT.focus();"
            Exit Sub
        Else
            If Not IsNumeric(endT.Text.Trim()) Then
                ltlAlert.Text = "alert('结束日期只能为数字!');Form1.endT.focus();"
                Exit Sub
            End If
        End If

        ltlAlert.Text = "window.open('/salary/EmployeAnalyseExcel.aspx?year=" & year.Text.Trim() & "&month=" & month.SelectedValue & "&day1=" & startT.Text.Trim() & "&day2=" & endT.Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') "
    End Sub


    Public Sub ratedp_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ratedp.ServerClick
        If smonth.Text.Trim() = "" Then
            ltlAlert.Text = "alert('起始月份不能为空!');Form1.smonth.focus();"
            Exit Sub
        Else
            If Not IsNumeric(smonth.Text.Trim()) Then
                ltlAlert.Text = "alert('起始月份只能为数字!');Form1.smonth.focus();"
                Exit Sub
            Else
                If CInt(smonth.Text.Trim()) < 1 And CInt(smonth.Text.Trim()) > 12 Then
                    ltlAlert.Text = "alert('起始月份不规范!');Form1.smonth.focus();"
                    Exit Sub
                End If
            End If
        End If
        If emonth.Text.Trim() = "" Then
            ltlAlert.Text = "alert('结束月份不能为空!');Form1.emonth.focus();"
            Exit Sub
        Else
            If Not IsNumeric(emonth.Text.Trim()) Then
                ltlAlert.Text = "alert('结束月份只能为数字!');Form1.emonth.focus();"
                Exit Sub
            Else
                If CInt(emonth.Text.Trim()) < 1 And CInt(emonth.Text.Trim()) > 12 Then
                    ltlAlert.Text = "alert('结束月份不规范!');Form1.emonth.focus();"
                    Exit Sub
                End If
            End If
        End If

        If CInt(emonth.Text.Trim()) < CInt(smonth.Text.Trim()) Then
            ltlAlert.Text = "alert('结束月份不能小于起始月份!');Form1.smonth.focus();"
            Exit Sub
        End If

        Dim all As String = "0"
        If Aall.Checked = True Then
            all = "1"
        End If

        ltlAlert.Text = "window.open('/salary/EmployeAnalyseMonthExcel.aspx?year=" & year.Text.Trim() & "&sm=" & smonth.Text.Trim() & "&em=" & emonth.Text.Trim() & "&ctype=" & wtype.SelectedValue & "&all=" & all & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') "
    End Sub

    Public Sub spending_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpendingM.ServerClick
        If syear.Text.Trim() = "" Then
            ltlAlert.Text = "alert('起始年份不能为空!');Form1.syear.focus();"
            Exit Sub
        Else
            If Not IsNumeric(syear.Text.Trim()) Then
                ltlAlert.Text = "alert('起始年份只能为数字!');Form1.syear.focus();"
                Exit Sub
            Else
                If CInt(syear.Text.Trim()) < 1900 Then
                    ltlAlert.Text = "alert('起始年份不规范!');Form1.syear.focus();"
                    Exit Sub
                End If
            End If
        End If

        If eyear.Text.Trim() = "" Then
            ltlAlert.Text = "alert('结束年份不能为空!');Form1.eyear.focus();"
            Exit Sub
        Else
            If Not IsNumeric(eyear.Text.Trim()) Then
                ltlAlert.Text = "alert('结束年份只能为数字!');Form1.eyear.focus();"
                Exit Sub
            Else
                If CInt(eyear.Text.Trim()) < 1900 Then
                    ltlAlert.Text = "alert('结束年份不规范!');Form1.eyear.focus();"
                    Exit Sub
                End If
            End If
        End If

        If CInt(syear.Text.Trim()) > CInt(eyear.Text.Trim()) Then
            ltlAlert.Text = "alert('结束年份不能小于起始年份!');Form1.eyear.focus();"
            Exit Sub
        End If

        If (syear.Text.Trim() = eyear.Text.Trim()) And (endmonth.SelectedValue < startmonth.SelectedValue) Then
            ltlAlert.Text = "alert('结束月份不能小于起始月份!');Form1.endmonth.focus();"
            Exit Sub
        End If
        ltlAlert.Text = "window.open('/salary/Moneyofcompany.aspx?sy=" & syear.Text.Trim() & "&sm=" & startmonth.SelectedValue & "&ey=" & eyear.Text.Trim() & "&em=" & endmonth.SelectedValue & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') "
    End Sub

    Public Sub flywan_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flywan.ServerClick
        If sdate.Text.Trim() = "" Then
            ltlAlert.Text = "alert('起始日期不能为空!');Form1.sdate.focus();"
            Exit Sub
        Else
            If Not IsDate(sdate.Text.Trim()) Then
                ltlAlert.Text = "alert('起始日期不正确!');Form1.sdate.focus();"
                Exit Sub
            End If
        End If

        If edate.Text.Trim() = "" Then
            ltlAlert.Text = "alert('结束日期不能为空!');Form1.edate.focus();"
            Exit Sub
        Else
            If Not IsDate(edate.Text.Trim()) Then
                ltlAlert.Text = "alert('结束日期不正确!');Form1.edate.focus();"
                Exit Sub
            End If
        End If

        strSql = "select g.Uid,u.userNo, u.username, isnull(d .name, ''), isnull(w.name, ''),isnull(g.Fworkdays, 0),isnull(g.Xworkdays, 0) "
        strSql &= " From "
        strSql &= " (Select CASE WHEN p.Uid IS NULL THEN q.Uid ELSE p.Uid END AS Uid,isnull(p.Fworkdays,0) as Fworkdays ,isnull(q.Xworkdays,0) as Xworkdays  "
        strSql &= " From "

        strSql &= " (select CASE WHEN jw.usercode IS NULL THEN k.usercode ELSE jw.usercode END AS Uid,isnull(jw.workhours,0)+ isnull(k.workhours, 0) as Fworkdays "
        strSql &= " From "
        strSql &= " (select j.usercode,sum(case when j.guideLine=0 then j.outputs else j.outputs/j.guideLine end)/8 as workhours From JobSalaryTemp j inner join workProcedure  wp on wp.id=j.workProcedureID and lower(wp.name) like N'%飞利蒲%' where j.workdate>='" & sdate.Text.Trim() & "' and j.workdate<='" & edate.Text.Trim() & "' group by j.usercode) jw "
        strSql &= " Full Outer JOIN  "
        strSql &= " (select jh.usercode,sum(case when jh.guideLine=0 then jh.outputs else jh.outputs/jh.guideLine end)/8 as workhours From JobSalaryTemp_his jh inner join workProcedure  wpw on wpw.id=jh.workProcedureID and lower(wpw.name) like N'%飞利蒲%' where jh.workdate>='" & sdate.Text.Trim() & "' and jh.workdate<='" & edate.Text.Trim() & "' group by jh.usercode)k on k.usercode=jw.usercode)p "

        strSql &= " Full Outer JOIN  "

        strSql &= " (select CASE WHEN za.usercode IS NULL THEN zah.usercode ELSE za.usercode END AS Uid,isnull(za.workhours,0)+ isnull(zah.workhours, 0) as Xworkdays "
        strSql &= " From "
        strSql &= " (select jm.usercode,sum(case when jm.guideLine=0 then jm.outputs else jm.outputs/jm.guideLine end)/8 as workhours From JobSalaryTemp jm inner join workProcedure  wd on wd.id=jm.workProcedureID and lower(wd.name) like N'%喜万年%' where jm.workdate>='" & sdate.Text.Trim() & "' and jm.workdate<='" & edate.Text.Trim() & "' group by jm.usercode) za "
        strSql &= " Full Outer JOIN  "
        strSql &= " (select jmh.usercode,sum(case when jmh.guideLine=0 then jmh.outputs else jmh.outputs/jmh.guideLine end)/8 as workhours From JobSalaryTemp_his jmh inner join workProcedure  wdh on wdh.id=jmh.workProcedureID and lower(wdh.name) like N'%喜万年%' where jmh.workdate>='" & sdate.Text.Trim() & "' and jmh.workdate<='" & edate.Text.Trim() & "' group by jmh.usercode) zah on zah.usercode=za.usercode)q  on q.Uid=p.Uid ) g"

        'or (lower(name) like N'%喜万年%')
        strSql &= " inner join tcpc0.dbo.users u on u.userID=g.Uid "
        strSql &= " LEFT OUTER JOIN Workshop w ON u.workshopID = w.id "
        strSql &= " LEFT OUTER JOIN departments d ON u.departmentID = d.departmentID "
        strSql &= " order by u.userID"
        'Response.Write(strSql)
            'Exit Sub

            Me.ExportExcel(chk.dsnx(), "<b>工号</b>~^<b>姓名</b>~^200^<b>部门</b>~^200^<b>工段</b>~^200^<b>完成天(飞利蒲)</b>~^200^<b>完成天(喜万年)</b>~^", strSql, False)
    End Sub

    Public Sub attendence_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles attendence.ServerClick
        If sdate.Text.Trim() = "" Then
            ltlAlert.Text = "alert('起始日期不能为空!');Form1.sdate.focus();"
            Exit Sub
        Else
            If Not IsDate(sdate.Text.Trim()) Then
                ltlAlert.Text = "alert('起始日期不正确!');Form1.sdate.focus();"
                Exit Sub
            End If
        End If

        If edate.Text.Trim() = "" Then
            ltlAlert.Text = "alert('结束日期不能为空!');Form1.edate.focus();"
            Exit Sub
        Else
            If Not IsDate(edate.Text.Trim()) Then
                ltlAlert.Text = "alert('结束日期不正确!');Form1.edate.focus();"
                Exit Sub
            End If
        End If
        Dim attendenceTable As String
        If atype.SelectedValue = 0 Then
            attendenceTable = "PieceAttendence"
        Else
            attendenceTable = "Attendance"
        End If
        Dim mdate As DateTime = CDate(sdate.Text.Trim()).AddDays(-CDate(sdate.Text.Trim()).Day + 1)
        Dim ndate As DateTime = (CDate(edate.Text.Trim()).AddDays(-CDate(edate.Text.Trim()).Day + 1)).AddMonths(1)

        strSql = " Select u.userNo,u.username,isnull(d.name,''),isnull(w.name,''),isnull(pa.days,0),u.enterdate,u.leavedate,isnull(dx.ductdays,0),isnull(s2.days,0),isnull(s.systemCodeName,''),isnull(u.fax,'')  "
        strSql &= " From (Select usercode,count(usercode) as days From " & attendenceTable & " Where workdate>='" & sdate.Text.Trim() & "' and workdate<='" & edate.Text.Trim() & "' group by usercode) pa "
        strSql &= " inner join tcpc0.dbo.users u on u.userID=pa.usercode "
        strSql &= " LEFT OUTER JOIN Workshop w ON u.workshopID = w.id "
        strSql &= " LEFT OUTER JOIN departments d ON u.departmentID = d.departmentID "
        strSql &= " LEFT OUTER JOIN (SELECT dm.usercode, SUM(dm.deductNum) AS ductdays From DeductMoney  dm "
        strSql &= " INNER JOIN tcpc0.dbo.systemCode sc ON dm.typeID = sc.systemCodeID AND sc.systemCodeName = N'请假' "
        strSql &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Deduct Money Type' "
        strSql &= " WHERE  dm.organizationID=" & Session("orgID") & " AND dm.workDate>='" & mdate & "' AND dm.workDate<'" & ndate & "' Group by dm.usercode,dm.typeID ) dx ON dx.usercode=u.userID "
        strSql &= " left outer join (select usercode,sum((DATEDIFF([Day], startdate,enddate)+1)) as days From SickLeave Where enddate>='" & mdate & "' and enddate<'" & ndate & "' Group by userCode ) s2 ON s2.userCode=u.userID "

        strSql &= " inner join tcpc0.dbo.systemCode s on s.systemCodeID=u.workTypeID"
        'strSql &= " (SELECT usercode,SUM(CASE WHEN (enddate is not null) THEN DATEDIFF([Day], startdate,enddate)+1 ELSE 0 END) - CASE WHEN SUM(DATEDIFF([Day], startdate, '" & sdate.Text.Trim() & "')) > 0 THEN SUM(DATEDIFF([Day], startdate, '" & sdate.Text.Trim() & "')+1) ELSE 0 END - CASE WHEN SUM(DATEDIFF([Day], '" & edate.Text.Trim() & "', enddate)) > 0 THEN SUM(DATEDIFF([Day], '" & edate.Text.Trim() & "', enddate)+1) ELSE 0 END AS days "
        'strSql &= " FROM SickLeave WHERE (startdate <= '" & edate.Text.Trim() & "') AND (enddate >= '" & sdate.Text.Trim() & "') GROUP BY usercode) s2 on s2.usercode=u.userID "
        strSql &= " order by u.userID"

            Me.ExportExcel(chk.dsnx(), "<b>工号</b>~^<b>姓名</b>~^200^<b>部门</b>~^200^<b>工段</b>~^100^<b>出勤天</b>~^100^<b>入公司日期</b>~^100^<b>离职日期</b>~^<b>请假天数</b>~^<b>病假天数</b>~^<b>计酬方式</b>~^<b>上级</b>~^", strSql, False)
    End Sub

    Public Sub fixAnalyse_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fixAnalyse.ServerClick
        If gstart.Text.Trim() = "" Then
            ltlAlert.Text = "alert('起始月份不能为空!');Form1.gstart.focus();"
            Exit Sub
        Else
            If Not IsNumeric(gstart.Text.Trim()) Then
                ltlAlert.Text = "alert('起始月份只能为数字!');Form1.gstart.focus();"
                Exit Sub
            Else
                If CInt(gstart.Text.Trim()) < 1 And CInt(gstart.Text.Trim()) > 12 Then
                    ltlAlert.Text = "alert('起始月份不规范!');Form1.gstart.focus();"
                    Exit Sub
                End If
            End If
        End If
        If gend.Text.Trim() = "" Then
            ltlAlert.Text = "alert('结束月份不能为空!');Form1.gend.focus();"
            Exit Sub
        Else
            If Not IsNumeric(gend.Text.Trim()) Then
                ltlAlert.Text = "alert('结束月份只能为数字!');Form1.gend.focus();"
                Exit Sub
            Else
                If CInt(gend.Text.Trim()) < 1 And CInt(gend.Text.Trim()) > 12 Then
                    ltlAlert.Text = "alert('结束月份不规范!');Form1.gend.focus();"
                    Exit Sub
                End If
            End If
        End If

        If CInt(gend.Text.Trim()) < CInt(gstart.Text.Trim()) Then
            ltlAlert.Text = "alert('结束月份不能小于起始月份!');Form1.gstart.focus();"
            Exit Sub
        End If
        ltlAlert.Text = "window.open('/salary/fixSalaryAnalyseExcel.aspx?year=" & year.Text.Trim() & "&sm=" & gstart.Text.Trim() & "&em=" & gend.Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') "
    End Sub

    Private Sub decompose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles decompose.Click
        Dim sd As String = year.Text.Trim & month.SelectedValue.ToString
        Dim bas As String
        Dim workdays As String
        strSql = " select basicSalary,monthlyWorkDays From BaseInfo where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read
            bas = reader(0)
            workdays = reader(1)
        End While
        reader.Close()

        strSql = "  select " & sd & " as cdate,N'计件',p.num,round(j.old/8,6),s.new,round(a.totalhours/8,6),a.days,p.salary," & workdays & "," & bas & " from "
        strSql &= " (select SUM(CASE WHEN guideLine>0 THEN round(outputs / guideLine,6) ELSE outputs END) as old,month(workdate) as jmonth from JobSalaryTemp where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "' GROUP BY month(workdate)) j  "
        strSql &= "  inner join (select SUM(workhours) as new,month(workdate) as smonth from JobSalary where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "' GROUP BY month(workdate)) s on s.smonth=j.jmonth "
        strSql &= "  inner join (Select sum(days) as days, Sum(totalHours) as totalHours,month(workdate) as amonth From PieceAttendence where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "' GROUP BY month(workdate)) a on a.amonth=j.jmonth "
        strSql &= "  inner join (Select sum(salary) as salary,count(usercode) as num,month(salarydate) as pmonth From piecesalary where year(salarydate)='" & year.Text.Trim & "' and month(salarydate)='" & month.SelectedValue & "' GROUP BY month(salarydate)) p on p.pmonth=j.jmonth "
        strSql &= "  Union "
        strSql &= "  select " & sd & " as cdate,N'生产部计时',count(c.usercode),0,0,sum(c.hoursday),sum(c.workday),sum(c.duereward)," & workdays & "," & bas & "  From commonSalary c "
        strSql &= "  inner join tcpc0.dbo.users u on u.userID=c.usercode"
        strSql &= "  inner join departments d on d.departmentID=u.departmentID "
        strSql &= " where  year(c.sdate)='" & year.Text.Trim & "' and month(c.sdate)='" & month.SelectedValue & "' and lower(d.name) like N'%生产部%'"
        strSql &= "  Union "
        strSql &= "  select " & sd & " as cdate,N'其他部门',count(c.usercode),0,0,sum(c.hoursday),sum(c.workday),sum(c.duereward)," & workdays & "," & bas & "  From commonSalary c "
        strSql &= "  inner join tcpc0.dbo.users u on u.userID=c.usercode"
        strSql &= "  inner join departments d on d.departmentID=u.departmentID "
        strSql &= " where  year(c.sdate)='" & year.Text.Trim & "' and month(c.sdate)='" & month.SelectedValue & "' and lower(d.name) not like N'%生产部%'"
        'Response.Write(strSql)
            'Exit Sub

            Me.ExportExcel(chk.dsnx(), "<b>年月</b>~^<b>分类</b>~^<b>人数</b>~^200^<b>未分解前完成天数</b>~^200^<b>分解后完成天数</b>~^100^<b>小时天</b>~^100^<b>出勤天</b>~^120^<b>应发工资总额</b>~^<b>应出勤</b>~^<b>基本工资</b>~^", strSql, False)
        End Sub

        Sub TestExport(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim Query As String
            If IsNumeric(year.Text.Trim) Then
            Else
                ltlAlert.Text = "alert('输入年份只能为数字!');Form1.year.focus();"
                Exit Sub
            End If

            Query = "Select  u.userno,u.username,isnull(d.name,''),isnull(w.name,''), isnull(r.roleName,''),isnull(t.salary,0),u.enterdate,u.leavedate,case when  u.fleave=0  then  case when u.leavebycp=0 then case when u.unback=0 then isnull(bk.name,'') else '' end else '' end else ''end  ,case when  u.fleave=0  then  case when u.leavebycp=0 then case when u.unback=0 then isnull(u.bankAccountNo,'') else '' end else '' end else ''end, "
            Query &= " isnull(s.systemCodeName,''),isnull(s1.systemCodeName,''),isnull(s3.systemCodeName,''),isnull(s2.systemCodeName,''),u.IC,u.fax "
            Query &= " From (select usercode,sum(salary) as salary from TestPiece where year(workdate)='" & year.Text & "' and month(workdate)='" & month.SelectedValue & "' group by usercode ) t"
            Query &= " Inner join tcpc0.dbo.Users u on u.userID= t.usercode"
            Query &= " left outer join departments d ON d.departmentID=u.departmentID "
            Query &= " left outer join workshop w on w.id=u.workshopID "
            Query &= " left outer join roles r on r.roleID= u.roleID"
            Query &= " left outer join  Bank bk on bk.id=u.bankID "
            Query &= " left outer join tcpc0.dbo.SystemCode s on s.systemCodeID=u.employTypeID"
            Query &= " left outer join tcpc0.dbo.SystemCode s1 on s1.systemCodeID=u.insuranceTypeID"
            Query &= " left outer join tcpc0.dbo.SystemCode s2 on s2.systemCodeID=u.sexID"
            Query &= " left outer join tcpc0.dbo.SystemCode s3 on s3.systemCodeID=u.contractTypeID"
            Query &= " order by t.usercode "

            Me.ExportExcel(chk.dsnx(), "<b>工号</b>~^<b>姓名</b>~^200^<b>部门</b>~^<b>工段</b>~^<b>职务</b>~^<b>总工资</b>~^<b>入公司日期</b>~^<b>离职日期</b>~^<b>银行</b>~^<b>银行账号</b>~^<b>用工性质</b>~^<b>保险类型</b>~^<b>合同类型</b>~^<b>性别</b>~^<b>身份证号</b>~^<b></b>~^", Query, False)
        End Sub

        Sub compareExport(ByVal sender As System.Object, ByVal e As System.EventArgs)


            Dim CompareSalary As String
            If IsNumeric(year.Text.Trim) Then
            Else
                ltlAlert.Text = "alert('输入年份只能为数字!');Form1.year.focus();"
                Exit Sub
            End If

            Dim price As Decimal = 0
            strSql = "  Select basicSalary,monthlyAvgDays From BaseInfo "
            strSql &= " where year(workDate)='" & year.Text.Trim & "' and month(workDate)='" & month.SelectedValue & "' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                price = Math.Round(reader(0) / reader(1), 2)
            End While
            reader.Close()

            CompareSalary = " select u.userno,u.username,a.workdate ,round(a.salary,4),round(isnull(attendence,0),4),isnull(a.totalHours,0),isnull(d.name,''),isnull(w.name,''),u.enterdate,u.leavedate,case when j.usercode is null then N'无' else  N'有' end "
            CompareSalary &= " From ( select case when t.usercode is null then p.usercode else t.usercode end as usercode, case when t.workdate is null then p.workdate else t.workdate end as workdate, isnull(t.salary,0) as salary,case when isnull(p.totalHours,0) =0 then 0 else case when p.totalHours > 8 then (8 + ( p.totalHours - 8) * 1.5) * " & price & " /8 else p.totalHours * " & price & " /8 end end as attendence,p.totalHours from "
            CompareSalary &= " (select usercode,workdate,sum(salary) as salary from TestPiece where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "' group by usercode,workdate) t "
            CompareSalary &= " full outer join "
            CompareSalary &= " (select usercode,workdate,totalhours from PieceAttendence where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "') p on p.usercode=t.usercode and p.workdate = t.workdate) a"
            CompareSalary &= " inner join tcpc0.dbo.Users u on u.userID= a.usercode "
            CompareSalary &= " left outer join departments d ON d.departmentID=u.departmentID "
            CompareSalary &= " left outer join workshop w on w.id=u.workshopID"
            '///---------------------------------------------------------------------
            '/// compare the old salary and new salary 
            CompareSalary &= " left outer join (select usercode,workdate from jobsalary where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "' group by usercode,workdate ) j on j.usercode=a.usercode and j.workdate=a.workdate "

            CompareSalary &= " order by u.userID,a.workdate "
            'Response.Write(CompareSalary)
            'Exit Sub

            Me.ExportExcel(chk.dsnx(), "<b>工号</b>~^<b>姓名</b>~^<b>日期</b>~^<b>单价工资</b>~^<b>出勤工资</b>~^<b>出勤小时</b>~^200^<b>部门</b>~^<b>工段</b>~^<b>入公司日期</b>~^<b>离职日期</b>~^100^<b>老产量</b>~^", CompareSalary, False)
        End Sub

        Sub matchExport(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If IsNumeric(year.Text.Trim) Then
            Else
                ltlAlert.Text = "alert('输入年份只能为数字!');Form1.year.focus();"
                Exit Sub
            End If
            'ltlAlert.Text = "window.open('/salary/TestMatching.aspx?ya=" & TextBox1.Text.Trim() & "&mon=" & DropDownList1.SelectedValue & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') "
            strSql = "select u.userno,u.username,t.workdate  "
            strSql &= " From (select usercode,workdate from TestPiece where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "' group by usercode,workdate) t "
            strSql &= " inner join (select usercode,workdate from jobsalary where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "' group by usercode,workdate ) j on j.usercode=t.usercode and j.workdate=t.workdate "
            strSql &= " inner join tcpc0.dbo.Users u on u.userID = t.usercode"
            strSql &= " order by u.userID,t.workdate "

            Me.ExportExcel(chk.dsnx(), "<b>工号</b>~^<b>姓名</b>~^<b>日期</b>~^", strSql, False)
        End Sub

        Protected Sub btnoutputExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnoutputExcel.Click
            '// Judge the condition is required, year,month,date --------------------
            If year.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('年不能为空!');Form1.year.focus();"
                Exit Sub
            Else
                If Not IsNumeric(year.Text.Trim) Then
                    ltlAlert.Text = "alert('输入年份只能为数字!');Form1.year.focus();"
                    Exit Sub
                End If
            End If

            If txtsdate.Text.Trim.Length > 0 Then
                If Not IsNumeric(txtsdate.Text.Trim) Then
                    ltlAlert.Text = "alert('输入日只能为数字!');Form1.txtsdate.focus();"
                    Exit Sub
                End If
            Else
                txtsdate.Text = "1"
            End If

            If txtedate.Text.Trim.Length > 0 Then
                If Not IsNumeric(txtedate.Text.Trim) Then
                    ltlAlert.Text = "alert('输入日只能为数字!');Form1.txtedate.focus();"
                    Exit Sub
                End If
            Else
                txtedate.Text = CDate(year.Text.Trim & "-" & month.SelectedValue + 1 & "-01").AddDays(-1).Day.ToString
            End If
            '// End  judging ---------------------------------------------------------

            strSql = " select t.id,t.usercode,u.userno,u.username,t.workdate,t.part,t.price,t.amount,t.createddate,u1.username,t.salary,u.fax,t.people,isnull(d.name,''),isnull(w.name,'') From TestPiece t "
            strSql &= "  inner join tcpc0.dbo.users u on u.userID = t.usercode "
            strSql &= "  inner join tcpc0.dbo.users u1 on u1.userID = t.createdby "
            If Session("uRole") <> "1" Then
                strSql &= " INNER join manager_worker m ON m.worker=t.createdBy AND m.manager= " & Session("uID")
            End If

            '//Added Department and Workshop by Simon March,24 2009 for export department name and workshop name
            strSql &= " left outer join Departments d on d.departmentID = u.departmentID "
            strSql &= " left outer join Workshop w on w.id = u.workshopID "
            '// End adding --------------------------------------------------------------------------------//

            strSql &= " where year(t.workdate)='" & year.Text.Trim & "' and month (t.workdate)='" & month.SelectedValue & "'"
            strSql &= " and day(t.workdate)>='" & txtsdate.Text.Trim & "' and day(t.workdate) <='" & txtedate.Text.Trim & "' "

            strSql &= " union "
            strSql &= " select t.id,t.usercode,u.userno,u.username,t.workdate,t.part,t.price,t.amount,t.createddate,u1.username,t.salary,u.fax,t.people,isnull(d.name,''),isnull(w.name,'') From TestPiece t"
            strSql &= " inner join tcpc0.dbo.users u on u.userID = t.usercode "
            strSql &= " inner join tcpc0.dbo.users u1 on u1.userID = t.createdby "

            '//Added Department and Workshop by Simon March,24 2009 for export department name and workshop name
            strSql &= " left outer join Departments d on d.departmentID = u.departmentID "
            strSql &= " left outer join Workshop w on w.id = u.workshopID "
            '// End adding --------------------------------------------------------------------------------//

            strSql &= " where  t.createdby ='" & Session("uID") & "' "
            strSql &= " and year(t.workdate)='" & year.Text.Trim & "' and month (t.workdate)='" & month.SelectedValue & "'"
            strSql &= " and day(t.workdate)>='" & txtsdate.Text.Trim & "' and day(t.workdate) <='" & txtedate.Text.Trim & "' "
            strSql &= " order by t.id desc "

            Me.ExportExcel(chk.dsnx(), "<b>工号</b>~^100^<b>姓名</b>~^100^<b>日期</b>~^200^<b>产品名称</b>~^100^<b>单价</b>~^150^<b>产量</b>~^<b>输入日期</b>~^<b>输入员</b>~^<b>金额</b>~^<b></b>~^<b>人数</b>~^120^<b>部门</b>~^100^<b>工段</b>~^", strSql, False)
        End Sub
End Class

End Namespace
