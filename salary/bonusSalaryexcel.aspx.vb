Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class bonusSalaryexcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim sqlStr As String
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
        If Not IsPostBack Then
            'Dim nRet As Integer
            'nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 14030102)
            'If nRet <= 0 Then
            '    Response.Redirect("/public/Message.aspx?type=" & nRet.ToString(), True)
            'End If 
            Dim lastyear As String
            Dim lastmonth As String
            If CInt(Request("sate")) = 1 Then
                lastyear = (CInt(Request("year")) - 1).ToString()
                lastmonth = "12"
            Else
                lastyear = Request("year")
                lastmonth = CInt(Request("sate")) - 1
            End If

            Dim salarydate As DateTime = CDate(Request("year") & "/" & Request("sate") & "/1")
            If Request("temp") = 0 Then
                PIMasteryRow("<b>部门</b>", "<b>工段</b>", "<b>工号</b>", "<b>姓名</b>", "<b>奖励</b>", "<b>质量</b>", "<b>自损</b>", "<b>违纪</b>", "<b>请假</b>", "<b>纠错扣款</b>", "<b>剩余未扣款</b>", "<b>总计</b>", "<b>实发金额</b>", "<b>日期</b>", "<b>性质</b>", "<b>离职</b>", "<b>备注</b>", "<b>完成天数</b>", "<b>上月余扣款</b>", "<b>用工性质</b>", "<b>保险类型</b>", "<b>出勤天</b>", "<b>银行</b>", "<b>账号</b>")
                sqlStr = " Select u.userNo,b.name,d.name as department,isnull(w.name,'') as workshop,b.mangle,b.breakrules,b.quality,s.systemCodeName, "
                sqlStr &= " b.leave,b.deductmoney,b.remainmoney,b.totalmoney,b.bonus,b.duereward,b.enterDate,b.fire, isnull(uc.comment,'') as comment,isnull(s2.cn,0) as scn, CASE WHEN b.remainmoney>0 THEN b.remainmoney-b.deductmoney+b.bonus ELSE b.bonus-b.deductmoney-b.duereward END as monthmoney,isnull(ss.systemCodeName,'') as employType,isnull(su.systemCodeName,'') as insuranceType,isnull(pt.attendencedays,0),isnull(bk.name,''),isnull(u.bankAccountNo,'') From bonusSalary b "
                sqlStr &= " left outer JOIN Workshop w ON w.id=b.workshopID "
                sqlStr &= " Left Outer JOIN (SELECT userCode,(isnull(allhours,0)+isnull(overdays,0)+isnull(extramonthupdays,0)+isnull(extraupdays,0)+isnull(extramonthdowndays,0)+isnull(extradowndays,0)) as cn From ReadyCaculateSalary  where month(salaryDate)= '" & Request("sate") & "'"
                sqlStr &= " and year(salaryDate)='" & Request("year") & "' ) as s2 on b.userCode=s2.userCode "

                'sqlStr &= " LEFT OUTER JOIN bonusSalary bs ON bs.userCode=b.userCode  and bs.flag<'2' and Year(bs.salarydate)='" & lastyear & "' and Month(bs.salarydate)=" & lastmonth
                sqlStr &= " left outer join (Select p.userCode,sum(p.days) as attendencedays From PieceAttendence p Where month(p.workdate)= '" & Request("sate") & "' and year(p.workdate)='" & Request("year") & "' group by userCode ) pt on pt.usercode = b.usercode "
                'Add by Baoxin 060105
                'sqlStr &= " LEFT OUTER JOIN bonusTimeSalary bt ON bt.userCode=b.userCode and Year(bt.salarydate)='" & lastyear & "' and Month(bt.salarydate)=" & lastmonth
            Else
                PIMasteryRow("<b>部门</b>", "<b>工号</b>", "<b>姓名</b>", "<b>奖励</b>", "<b>质量</b>", "<b>自损</b>", "<b>违纪</b>", "<b>请假</b>", "<b>纠错扣款</b>", "<b>剩余未扣款</b>", "<b>总计</b>", "<b>实发金额</b>", "<b>日期</b>", "<b>性质</b>", "<b>离职</b>", "<b>备注</b>", "<b>完成天数</b>", "<b>用工性质</b>", "<b>保险类型</b>", "<b>出勤天</b>", "<b>银行</b>", "<b>账号</b>", "", "")
                sqlStr = " Select u.userNo,b.name,d.name as department,b.mangle,b.breakrules,b.quality,s.systemCodeName, "
                sqlStr &= " b.leave,b.deductmoney,b.remainmoney,b.totalmoney,b.bonus,b.duereward,b.enterDate,b.fire,isnull(uc.comment,'') as comment ,isnull(at.totalHours/8,0) as Hours,isnull(ss.systemCodeName,'') as employType,isnull(su.systemCodeName,'') as insuranceType,isnull(at.attendencedays2,0),isnull(bk.name,''),isnull(u.bankAccountNo,'') From bonusTimeSalary b "
                sqlStr &= " left outer join (Select a.userCode, sum(a.totalHours) as totalHours,sum(a.days) as attendencedays2 From Attendance a Where month(a.workdate)= '" & Request("sate") & "' and year(a.workdate)='" & Request("year") & "' group by userCode ) at on at.usercode = b.usercode "

            End If
            sqlStr &= " left outer join (select  a.UserID,CASE WHEN isnull(d1.name,'')<>'' THEN CASE WHEN isnull(d2.name,'')<>'' THEN (d2.name+ N'调入'+d1.name) ELSE d1.name END ELSE '' END AS comment From User_Exchange_Department a  "
            sqlStr &= " left outer join departments d1 ON d1.departmentID=a.departmentID "
            sqlStr &= " left outer join departments d2 ON d2.departmentID=a.olddepartmentID  "
            sqlStr &= " INNER JOIN (SELECT userID, MAX(createdDate) AS mdate FROM User_Exchange_Department WHERE Year(ExchangeDate) ='" & Request("year") & "'  And Month(ExchangeDate) ='" & Request("sate") & "' GROUP BY UserID) AS ue ON ue.userID = a.userID AND (a.createdDate = ue.mdate) "
            sqlStr &= " where year(a.ExchangeDate)='" & Request("year") & "' and month(a.ExchangeDate)='" & Request("sate") & "' ) as uc ON b.userCode = uc.UserID "

            sqlStr &= " INNER JOIN tcpc0.dbo.users u ON u.userID= b.userCode and u.isTemp='" & Session("temp") & "' "
            sqlStr &= " left outer join  Bank bk on bk.id=u.bankID "
            If Request("type") = 0 Then
                'sqlStr &= " INNER JOIN tcpc0.dbo.users u ON u.userID= b.userCode and DateAdd(month,3,u.enterDate)>'" & salarydate.ToString() & "' "
                sqlStr &= " and DateAdd(month,3,u.enterDate)>'" & salarydate.ToString() & "' "
            End If
            sqlStr &= " INNER JOIN departments d ON d.departmentID=b.departmentID "
            sqlStr &= " INNER JOIN tcpc0.dbo.SystemCode s ON s.systemCodeID= b.workTypeID "
            sqlStr &= " LEFT OUTER JOIN tcpc0.dbo.SystemCode ss ON ss.systemCodeID=u.employTypeID "
            sqlStr &= " LEFT OUTER JOIN tcpc0.dbo.SystemCode su ON su.systemCodeID=u.insuranceTypeID "
            sqlStr &= " Where (datepart(year,b.salarydate)='" & Request("year") & "') and datepart(month,b.salarydate)= " & Request("sate")
           
            If Request("temp") = 0 Then
                sqlStr &= " and b.flag<'2' "
            End If
            sqlStr &= "order by b.userCode "

            'Response.Write(sqlStr)
            'Exit Sub
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            Dim word As String
            While reader.Read
                Dim bank As String
                Dim bankaccountNo As String

                If Request("temp") = 0 Then
                    If reader(15) = True Then
                        word = "True"
                        bank = ""
                        bankaccountNo = ""
                    Else
                        word = "FALSE"
                        bank = reader(22)
                        bankaccountNo = reader(23)
                    End If
                    PIMasteryRow(reader(2), reader(3), reader(0), reader(1), reader(12), reader(6), reader(4), reader(5), reader(8), reader(9), reader(10), reader(11), reader(13), reader(14), reader(7), word, reader(16), reader(17), reader(18), reader(19), reader(20), reader(21), bank, bankaccountNo)
                Else
                    If reader(14) = True Then
                        word = "True"
                        bank = ""
                        bankaccountNo = ""
                    Else
                        word = "FALSE"
                        bank = reader(20)
                        bankaccountNo = reader(21)
                    End If
                    PIMasteryRow(reader(2), reader(0), reader(1), reader(11), reader(5), reader(3), reader(4), reader(7), reader(8), reader(9), reader(10), reader(12), reader(13), reader(6), word, reader(15), reader(16), reader(17), reader(18), reader(19), bank, bankaccountNo, "", "")
                End If
            End While
            reader.Close()

            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
        End If
    End Sub
    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal str15 As String, ByVal str16 As String, ByVal str17 As String, ByVal str18 As String, ByVal str19 As String, ByVal str20 As String, ByVal str21 As String, ByVal str22 As String, ByVal str23 As String)
        row = New TableRow
        If str1 = "工段" Then
            row.BackColor = Color.LightGray
        Else
            row.BackColor = Color.White
        End If
        row.HorizontalAlign = HorizontalAlign.Right
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = str0.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str1.Trim()
        cell.Width = New Unit(120)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str2
        'Response.Write(str2)
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str3.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str4.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str5.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str6.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str7.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str8.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str9.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str10.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str11.Trim()
        cell.Width = New Unit(90)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str12.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str13.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str14.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str15.Trim()
        cell.Width = New Unit(60)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str16.Trim()
        cell.Width = New Unit(120)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str17.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str18.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str19.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str20.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str21.Trim()
        cell.Width = New Unit(100)
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str22.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str23.Trim()
        cell.Width = New Unit(100)
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub
End Class

End Namespace
