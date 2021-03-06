'!*******************************************************************************!
'* @@ NAME				:	salaryPesonalReportTime.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for salaryPesonalReportTime.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	April  2006
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Drawing
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class salaryPesonalReportTime
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim strSql As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Dim chk As New adamClass

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
            'Dim query As String
            'Dim typeHashtable As New Hashtable
            'query = "select sc.systemCodeID,sc.systemCodeName From tcpc0.dbo.systemCode sc INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Deduct Money Type' "
            'reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, query)
            'While reader.Read
            '    typeHashtable.Add(reader(1).ToString(), reader(0))
            'End While
            'reader.Close()

            'strSql &= " Select u.userNo,u.userName,isnull(d.name,''),r.roleName,c.workday,c.hoursday,c.basicesalary,c.benefitsalary,c.duereward,c.houseFund,c.retiredFund ,"
            'strSql &= " c.membershipPay,c.messpay,c.incometax,c.workpay,isnull(s1.systemCodeName,''),isnull(s2.systemCodeName,''),u.enterdate,u.leavedate,case when u.leavedate is null then 'False' else 'True' end ,c.kindssubsidy,"
            'strSql &= " c.nightworkPay,c.deductmoney,c.attendanceHours,c.workprice,c.fixedsalary,isnull(o1.extratimes,0) as days1,isnull(o2.extratimes,0) as days2,(isnull(o2.times,0)+isnull(o1.times,0)) as times1,isnull(o1.extrasalarey,0) as fee1,isnull(o2.extrasalarey,0) as fee2,(isnull(o1.salarey,0)+isnull(o2.salarey,0)) as fee3,isnull(b.bonus,0),c.duereward+isnull(o1.extrasalarey,0)+isnull(o2.extrasalarey,0) +isnull(o1.salarey,0)+isnull(o2.salarey,0)+isnull(b.bonus,0) as totalsalary,(c.workpay+isnull(o1.extrasalarey,0)+isnull(o2.extrasalarey,0) +isnull(o1.salarey,0)+isnull(o2.salarey,0)+isnull(b.duereward,0)) as duereward,isnull(c.weekendDays,0),isnull(c.leaveday,0),u.IC,isnull(s3.systemCodeName,''),isnull(s4.systemCodeName,''),"
            'strSql &= " isnull(db.brule,0)-isnull(dn.neglectWork,0) as brule,isnull(dn.neglectWork,0) as neglectWork,isnull(dl.leave,0) as leave,isnull(sk.sickday,0) as sickday,c.sdate "
            'strSql &= " From commonSalary c "
            'strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = c.usercode and u.isTemp='" & Session("temp") & "'"
            'strSql &= " INNER JOIN departments d ON d.departmentID = u.departmentID "
            'strSql &= " left outer join Roles r on r.roleID=u.roleID "
            'strSql &= " left outer JOIN bonusTimeSalary b ON b.userCode = c.userCode and year(b.salarydate)=year(c.sdate) and month(b.salarydate)=month(c.sdate)"
            'strSql &= " left outer JOIN OvertimesSalaryTime o1 ON o1.userCode = c.userCode and year(o1.salarydate)=year(c.sdate) and month(o1.salarydate)=month(c.sdate) and Day(o1.salarydate)=1 "
            'strSql &= " left outer JOIN OvertimesSalaryTime o2 ON o2.userCode = c.userCode and year(o2.salarydate)=year(c.sdate) and month(o2.salarydate)=month(c.sdate) and Day(o2.salarydate)=15 "
            'strSql &= " left outer JOIN tcpc0.dbo.systemCode s1 ON s1.systemcodeid = u.employtypeid "
            'strSql &= " left outer JOIN tcpc0.dbo.systemCode s2 ON s2.systemcodeid = u.insuranceTypeID "
            'strSql &= " left outer JOIN tcpc0.dbo.systemCode s3 ON s3.systemcodeid = u.sexID "

            strSql = "SELECT u.userNo,u.userName,ISNULL(d.name,''),r.roleName,h.hr_Time_SalaryAttDay,h.hr_Time_SalaryAttendance,hr_Time_SalaryBasic,"
            strSql &= " hr_Time_SalaryAssess + hr_adjust_salary,hr_Time_SalaryBenefit,hr_Time_SalaryNightWork,hr_Time_SalaryAllowance,hr_Time_SalaryHoliday,hr_Time_SalaryDuereward + hr_adjust_salary, "
            strSql &= " hr_Time_SalaryHfound, hr_Time_SalaryRfound,hr_Time_SalaryMember,hr_Time_SalaryDeduct,hr_Time_SalaryTax,hr_Time_SalaryWorkpay +hr_adjust_salary ,"
            strSql &= " hr_Time_SalaryHdays,hr_Time_SalaryAnnual,hr_Time_SalarySleave,hr_Time_SalarySLeavePay,hr_Time_SalaryMaternityDays,hr_Time_SalaryMaternityPay, "
            strSql &= " hr_Time_SalaryCDeduct,hr_Time_SalaryLDeduct,hr_Time_Salaryfixedsalary,ISNULL(s4.systemcodeName,''),ISNULL(s1.systemcodeName,''),ISNULL(s2.systemcodeName,''),"
            strSql &= " u.enterdate,u.leavedate,CASE WHEN u.leavedate IS NULL THEN 'False' ELSE 'True' END,u.IC,ISNULL(s3.systemcodeName,'') ,h.hr_Time_SalaryDate"
            strSql &= " FROM hr_fin_Time h "
            strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = h.hr_Time_SalaryUserID "
            strSql &= " LEFT OUTER JOIN departments d ON d.departmentID = h.hr_Time_SalaryDepartment "
            strSql &= " LEFT OUTER JOIN Roles r ON r.roleID=u.roleID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s1 ON s1.systemcodeid = h.hr_Time_SalaryEmployTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s2 ON s2.systemcodeid = h.hr_Time_SalaryInsuranceTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s3 ON s3.systemcodeid = u.sexID"
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s4 ON s4.systemcodeid = h.hr_Time_SalaryWorktype "
        Dim dd As String
        If Request("ey").Trim() = "" Or Request("ey") Is Nothing Then
            dd = Request("sy") & "-" & Request("sm") & "-1"
        Else
            dd = Request("ey") & "-" & Request("em") & "-1"
        End If
            'strSql &= " left outer join (select w.userID,w.worktypeID From WorktypeChange w inner join (select userid,min(changedate) as changedate From WorktypeChange where changedate>='" & dd & "' group by userID) sd  on sd.userID=w.userID and sd.changedate=w.changedate) ww on ww.userID=u.userID "
            'strSql &= " left outer JOIN tcpc0.dbo.systemCode s4 ON s4.systemcodeid = CASE WHEN ww.workTypeID is null THEN u.workTypeID ELSE ww.workTypeID END"

        ' strSql &= " left outer JOIN (Select usercode , month(workdate) AS ms, year(workdate) AS ys,sum(days) as adays, sum(totalHours)/8 as hdays from PieceAttendence  group by usercode,year(workdate),month(workdate)) as ss on ss.usercode=p.usercode and  ss.ys = year(p.salarydate) AND ss.ms = Month(p.salarydate) "
            'strSql &= " left outer JOIN (select usercode, year(startdate) AS ys, month(startdate) AS ms,sum(CASE WHEN (enddate is not null) THEN (DATEDIFF([Day], startdate,enddate)+1) ELSE 0 END) as sickday FRom SickLeave group by usercode, year(startdate), month(startdate) ) sk on sk.usercode=c.usercode and sk.ys = year(c.sdate) AND sk.ms = month(c.sdate)"
            'strSql &= " left outer JOIN (select usercode, month(workdate) AS ms, year(workdate) AS ys,sum(amount) as brule From DeductMoney Where typeID='" & typeHashtable("违纪") & "' group by usercode,year(workdate),month(workdate)) db on db.usercode=c.usercode and db.ys = year(c.sdate) AND db.ms = month(c.sdate)"
            'strSql &= " left outer JOIN (select usercode, month(workdate) AS ms, year(workdate) AS ys,sum(amount) as neglectWork From DeductMoney Where typeID='" & typeHashtable("违纪") & "' and lower(comments) like N'%旷工%' group by usercode,year(workdate),month(workdate)) dn on dn.usercode=c.usercode and  dn.ys = year(c.sdate) AND  dn.ms = month(c.sdate)"
            'strSql &= " left outer JOIN (select usercode, month(workdate) AS ms, year(workdate) AS ys,sum(deductNum) as leave From DeductMoney Where typeID='" & typeHashtable("请假") & "'  group by usercode,year(workdate),month(workdate)) dl on dl.usercode=c.usercode and  dl.ys = year(c.sdate) AND  dl.ms = month(c.sdate)"
        If Request("ey").Trim() = "" Or Request("ey") Is Nothing Then
                strSql &= " Where (Year(h.hr_Time_SalaryDate) = " & Request("sy") & ") And (Month(h.hr_Time_SalaryDate) = " & Request("sm") & ") "
        Else
            If Request("ey") = Request("sy") Then
                    strSql &= " Where  (Year(h.hr_Time_SalaryDate) = " & Request("sy") & ")"
                    strSql &= " and (Month(h.hr_Time_SalaryDate) >= " & Request("sm") & ") and (Month(h.hr_Time_SalaryDate) <= " & Request("em") & ")"
            Else
                Dim sdate As String = Request("sy") & "-" & Request("sm") & "-1"
                Dim edate As String = Request("ey") & "-" & Request("em") & "-1"
                    strSql &= " Where  h.hr_Time_SalaryDate >= '" & sdate & "' and  h.hr_Time_SalaryDate <= '" & edate & "'"
            End If

        End If
        strSql &= " and u.userNo='" & Request("cod") & "' "
        strSql &= " and u.plantCode='" & Session("PlantCode") & "'"
            strSql &= " ORDER BY  h.hr_Time_SalaryDate "
        'Response.Write(strSql)
        'Exit Sub
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        Dim ss As String = ""

        While reader.Read
            If ss = "" Then
                PIMastery(reader(0), reader(1), 0)
            End If

            Dim ldate As String
                If Year(CDate(reader("hr_Time_SalaryDate"))).ToString() <> ss Then
                    ss = Year(CDate(reader("hr_Time_SalaryDate"))).ToString()
                    PIMasteryHeaderRow(Year(CDate(reader("hr_Time_SalaryDate").ToString())), "")
                End If

                If reader(32).ToString() = "" Then
                    ldate = ""
                Else
                    ldate = reader(32)
                End If
                PIMasteryRow(Month(CDate(reader("hr_Time_SalaryDate").ToString())), reader(2), reader(3), reader(4), reader(5), reader(6), reader(7), reader(8), reader(9), reader(10), reader(11), reader(12), reader(13), _
                         reader(14), reader(15), reader(16), reader(17), reader(18), reader(19), reader(20), reader(21), reader(22), reader(23), reader(24), reader(25), reader(26), reader(27), reader(28), reader(29), reader(30), reader(31), ldate, reader(33), reader(34), reader(35), "", "", "", "", "", "", "", "")
            PIMastery("", "", 1)
        End While
        reader.Close()

        Response.Clear()
        Response.Buffer = True
        Response.Charset = "UTF-8"
        Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312")
        Response.ContentType = "application/vnd.ms-excel"

        Response.Flush()
    End Sub

    Sub PIMastery(ByVal str0 As String, ByVal str1 As String, ByVal temp As Integer)

        row = New TableRow
        row.BorderWidth = New Unit(0)
        If temp = 0 Then
            cell = New TableCell
            cell.Text = "<b>" & str0 & "</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>" & str1 & "</b>"
            cell.Width = New Unit(160)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = ""
            cell.Width = New Unit(4000)
            cell.ColumnSpan = 34
            row.Cells.Add(cell)
        Else
            cell = New TableCell
            cell.Text = ""
            cell.Width = New Unit(4260)
            cell.ColumnSpan = 36
            row.Cells.Add(cell)

        End If
        exl.Rows.Add(row)
    End Sub

    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, _
                           ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal str15 As String, ByVal str16 As String, ByVal str17 As String, ByVal str18 As String, ByVal str19 As String, ByVal str20 As String, ByVal str21 As String, ByVal str22 As String, ByVal str23 As String, ByVal str24 As String, ByVal str25 As String, ByVal str26 As String, ByVal str27 As String, _
                     ByVal str28 As String, ByVal str29 As String, ByVal str30 As String, ByVal str31 As String, ByVal str32 As String, ByVal str33 As String, ByVal str34 As String, ByVal str35 As String, ByVal str36 As String, ByVal str37 As String, ByVal str38 As String, ByVal str39 As String, ByVal str40 As String, ByVal str41 As String, ByVal str42 As String)
        row = New TableRow
        row.HorizontalAlign = HorizontalAlign.Center
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = "<b>" & str0 & "月</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str1
        cell.Width = New Unit(160)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str2
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str3
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str4
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str5
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str6
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str7
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str8
        cell.Width = New Unit(100)
        row.Cells.Add(cell)


        cell = New TableCell
        cell.Text = str9
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str10
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str11
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str12
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str13
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str14
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str15
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str16
            'cell.Text = Format(Convert.ToDateTime(str16), "yyyy.MM.dd")
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            'If str17 = "" Then
            '    cell.Text = ""
            'Else
            '    cell.Text = Format(Convert.ToDateTime(str17), "yyyy.MM.dd")
            'End If
            cell.Text = str17
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str18
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str19
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str20
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str21
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str22
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str23
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str24
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str25
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str26
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str27
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str28
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str29
        cell.Width = New Unit(100)
            'cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        cell = New TableCell
            'cell.Text = str30
            cell.Text = Format(Convert.ToDateTime(str30), "yyyy.MM.dd")
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            'cell.Text = str31
            If str31 = "" Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(str31), "yyyy.MM.dd")
            End If
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str32
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str33
        cell.Width = New Unit(100)
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str34
        cell.Width = New Unit(100)
            'cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str35
        cell.Width = New Unit(100)
            'cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str36
        cell.Width = New Unit(100)
            'cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str37
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str38
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str39
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str40
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str41
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str42
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        exl.Rows.Add(row)

    End Sub

    Sub PIMasteryHeaderRow(ByVal str0 As String, ByVal str1 As String)

        row = New TableRow
        row.BackColor = Color.LightGray
        row.HorizontalAlign = HorizontalAlign.Center
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = "<b>" & str0.Trim() & "年</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>部门</b>"
        cell.Width = New Unit(160)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>职务</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>出勤天</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>小时天</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>基本工资</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>加班工资</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>效益工资</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>中夜津贴</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)


            cell = New TableCell
            cell.Text = "<b>其他津贴</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>国假工资</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>应发金额</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>公积金</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>养老金</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>工会费</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>扣款</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>所得税</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>实发金额</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>国假天数</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>年假天数</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>病假天数</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>病假工资</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>产假天数</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>产假工资</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)


            cell = New TableCell
            cell.Text = "<b>当月剩余扣款</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>上月余扣款</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>固定工资</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>分类</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>用工性质</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>保险类型</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>入厂日期</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>离职日期</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>离职</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>各项津贴</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>中夜津贴</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>应扣金额</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>应出勤</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>工价</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>固定工资</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>上加班天</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>下加班天</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>国假加班天</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>上加班费</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>下加班费</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>国假加班费</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>应发奖金</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>应发总金额</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>实发总金额</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>双休日天</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>请假天数</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>身份证号码</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>性别</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>分类</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>违纪</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>旷工</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>事假</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>病假</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub

End Class

End Namespace
