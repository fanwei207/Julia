'!*******************************************************************************!
'* @@ NAME				:	EmployeAnalyseMonthExcel.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for EmployeAnalyseMonthExcel.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	February 27 2006
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

Partial Class EmployeAnalyseMonthExcel
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
        If Not IsPostBack Then
            PIMasteryHeaderRow(Request("year"), Request("sm"), Request("em"))
            Dim x As Integer = Convert.ToInt32(Request("sm").Trim)
            Dim y As Integer = Convert.ToInt32(Request("em").Trim)
            Dim tdate As DateTime = CDate(Request("year") & "-" & Request("sm") & "-01")
            Dim i As Integer
            Dim j As Integer
            strSql = " Select u.userNo,u.username,isnull(d.name,'') as dname,isnull(w.name,'') as wname, "
            For i = x To y
                If Request("ctype") = 0 Then
                    strSql &= " ISNULL(a" & i.ToString() & ".thours,'0'),ISNULL(b" & i.ToString() & ".workhours,'0'),ISNULL(b" & i.ToString() & ".wdays,'0') , "
                Else
                    strSql &= " ISNULL(b" & i.ToString() & ".thours,'0'),ISNULL(b" & i.ToString() & ".workhours,'0'),ISNULL(b" & i.ToString() & ".wdays,'0') , "
                End If
                strSql &= " ISNULL(c" & i.ToString() & ".overpay,'0')+ISNULL(d" & i.ToString() & ".bonus,'0')+ISNULL(e" & i.ToString() & ".salary,'0'),ISNULL(e" & i.ToString() & ".workpay,'0'),  CASE WHEN f" & i.ToString() & ".usercode is null  THEN '' ELSE 'QIU' END, "
            Next
            strSql &= " u.enterdate,u.leavedate,isnull(s3.systemCodeName,'') as salarytype,ISNULL(r.roleName,'') as rname,isnull(u.comments,'') as comment,isnull(s.systemCodeName,'') as worktype,isnull(s1.systemCodeName,'') as istype,isnull(b.name,'') as bname,isnull(u.bankAccountNo,'') as bankAccountNo,isnull(s4.systemCodeName,'') as contact,CASE WHEN u.enterdate is null THEN '' ELSE CASE WHEN MONTH(u.enterdate)<=MONTH(getdate()) THEN datediff(year,u.enterdate,getdate()) ELSE datediff(year,u.enterdate,getdate())-1 END END as workyear  FRom "
            strSql &= " tcpc0.dbo.users u "

            'Dim access As String = "b" & y.ToString()
            'strSql &= " (select rc.usercode,(isnull(rc.allhours,0)+isnull(rc.overdays,0)+isnull(rc.extramonthupdays,0)+isnull(rc.extraupdays,0)+isnull(rc.extramonthdowndays,0)+isnull(rc.extradowndays,0)) as thours,isnull(at.hdays,0) as workhours ,isnull(at.days,'0') as wdays  FRom ReadyCaculateSalary rc "
            'strSql &= " Left outer join (Select usercode, sum(totalHours/8) as hdays,sum(days) as days from PieceAttendence where year(workdate)='" & Request("year") & "' and month(workdate)='" & y.ToString() & "' group by usercode) at ON at.usercode=rc.usercode  Where Year(rc.salaryDate)='" & Request("year") & "' and Month(rc.salaryDate)='" & y.ToString() & "') " & access

            For j = x To y
                Dim access As String = "a" & j.ToString()
                Dim bccess As String = "b" & j.ToString()
                Dim ccess As String = "c" & j.ToString()
                Dim dccess As String = "d" & j.ToString()
                Dim eccess As String = "e" & j.ToString()
                Dim fccess As String = "f" & j.ToString()
                If Request("ctype") = 0 Then
                    strSql &= " left Outer JOIN (select usercode,(isnull(allhours,0)+isnull(overdays,0)+isnull(extramonthupdays,0)+isnull(extraupdays,0)+isnull(extramonthdowndays,0)+isnull(extradowndays,0)+isnull(extradowndays,0)+isnull(upspecialdays,0)+isnull(downspecialdays,0)) as thours FRom ReadyCaculateSalary  Where Year(salaryDate)='" & Request("year") & "' and Month(salaryDate)='" & j.ToString() & "')" & access & " ON  u.userID= " & access & ".usercode "
                    strSql &= " Left outer join (Select usercode, sum(totalHours/8) as workhours,sum(days) as wdays from PieceAttendence  where year(workdate)='" & Request("year") & "' and month(workdate)='" & j.ToString() & "' group by usercode) " & bccess & " ON  u.userID= " & bccess & ".usercode "
                    strSql &= " Left outer join (Select userCode,sum(isnull(extrasalarey,0)+isnull(salarey,0)+isnull(special,0)) as overpay from OvertimesSalary where year(salarydate)='" & Request("year") & "' and month(salarydate)='" & j.ToString() & "' group by usercode) " & ccess & " ON u.userID=" & ccess & ".usercode "
                    strSql &= " Left outer join (Select userCode,bonus from bonusSalary where year(salarydate)='" & Request("year") & "' and month(salarydate)='" & j.ToString() & "' and flag<'2') " & dccess & " ON u.userID=" & dccess & ".usercode "
                    strSql &= " Left outer join (select usercode,salary,workpay From piecesalary where year(salarydate)='" & Request("year") & "' and month(salarydate)='" & j.ToString() & "' and flag<>'1' ) " & eccess & " ON u.userID=" & eccess & ".usercode "
                    strSql &= " Left outer join  EmployeeEspecial " & fccess & " ON u.userID=" & fccess & ".usercode and year(" & fccess & ".currentdate)= '" & Request("year") & "' and month(" & fccess & ".currentdate)='" & j.ToString() & "' "
                Else
                    strSql &= " Left outer join (Select usercode,'0' as thours,sum(totalHours/8) as workhours,sum(days) as wdays from Attendance  where year(workdate)='" & Request("year") & "' and month(workdate)='" & j.ToString() & "' group by usercode) " & bccess & " ON  u.userID= " & bccess & ".usercode "
                    strSql &= " Left outer join (Select userCode,sum(isnull(extrasalarey,0)+isnull(salarey,0)) as overpay from OvertimesSalaryTime where year(salarydate)='" & Request("year") & "' and month(salarydate)='" & j.ToString() & "'group by usercode) " & ccess & " ON u.userID=" & ccess & ".usercode "
                    strSql &= " Left outer join (Select userCode,bonus from bonusTimeSalary where year(salarydate)='" & Request("year") & "' and month(salarydate)='" & j.ToString() & "') " & dccess & " ON u.userID=" & dccess & ".usercode "
                    strSql &= " Left outer join (select usercode,duereward as salary,workpay From commonSalary where year(sdate)='" & Request("year") & "' and month(sdate)='" & j.ToString() & "' and flag<>'1' ) " & eccess & " ON u.userID=" & eccess & ".usercode "
                    strSql &= " Left outer join  EmployeeEspecial " & fccess & " ON u.userID=" & fccess & ".usercode and year(" & fccess & ".currentdate)= '" & Request("year") & "' and month(" & fccess & ".currentdate)='" & j.ToString() & "' "
                End If
            Next
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.SystemCode s on s.systemCodeID=u.employTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.SystemCode s1 on s1.systemCodeID=u.insuranceTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.SystemCode s3 on s3.systemCodeID=u.workTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.SystemCode s4 on s4.systemCodeID=u.contractTypeID "
            strSql &= " LEFT OUTER JOIN Roles r ON r.roleID = u.roleID "
            strSql &= " LEFT OUTER JOIN Workshop w ON u.workshopID = w.id "
            strSql &= " LEFT OUTER JOIN departments d ON u.departmentID = d.departmentID "
            strSql &= " INNER JOIN tcpc0.dbo.SystemCode s2 on s2.systemCodeID = u.workTypeID INNER JOIN tcpc0.dbo.SystemCodeType st on st.systemCodeTypeID=s2.systemCodeTypeID and systemCodeTypeName='Work Type' "
            strSql &= " left outer join  Bank b on b.id=u.bankID "
            'If Request("ctype") = 0 Then
            '    strSql &= " Where s2.systemCodeName=N'计件' "
            'Else
            '    strSql &= " Where s2.systemCodeName<>N'计件' "
            'End If
            strSql &= " where  u.roleID>1 "
            If Request("all") = "0" Then
                strSql &= " and (u.leavedate is null or u.leavedate>'" & tdate & "')  "
            End If
            'strSql &= " and u.isTemp='" & Session("temp") & "'"
            strSql &= " and u.plantCode='" & Session("PlantCode") & "' "
            'strSql &= " and ( ISNULL(b" & x.ToString() & ".workhours,'0') <> 0 "
            'For j = x + 1 To y
            '    strSql &= "  or  ISNULL(b" & j.ToString() & ".workhours,'0') <> 0 "
            'Next
            strSql &= "  order by u.userID "

            'Response.Write(strSql)
            'Exit Sub
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                Dim ss As String = ""
                Dim ldate As String
                Dim totalfinish As Decimal = 0
                Dim totalhours As Decimal = 0
                Dim totaldays As Decimal = 0
                Dim totalpay As Decimal = 0
                Dim t As Integer
                Dim bankname As String
                Dim banknumber As String
                For t = 0 To y - x
                    ss = ss & reader(t * 6 + 5) & "," & reader(t * 6 + 6) & "," & reader(t * 6 + 4) & "," & reader(t * 6 + 7) & "," & reader(t * 6 + 8) & "," & reader(t * 6 + 9) & ",^"
                    totalfinish = totalfinish + reader(t * 6 + 4)
                    totalhours = totalhours + reader(t * 6 + 5)
                    totaldays = totaldays + reader(t * 6 + 6)
                    totalpay = totalpay + reader(t * 6 + 7)
                Next




                If reader("leavedate").ToString() = "" Then
                    ldate = ""          
                Else
                    ldate = reader("leavedate").ToString()
                    'bankname = ""
                    'banknumber = ""
                End If
                bankname = reader("bname").ToString()
                banknumber = reader("bankAccountNo").ToString()
                PIMasteryRow(reader(0), reader(1), reader(2), reader(3), ss, totalfinish, totalhours, totaldays, totalpay, reader("enterdate").ToString(), ldate, reader("salarytype"), reader("rname"), reader("comment"), reader("worktype"), reader("istype"), bankname, banknumber, reader("contact"), reader("workyear"))
            End While
            reader.Close()

            Response.Clear()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312")
            Response.ContentType = "application/vnd.ms-excel"

            Response.Flush()
        End If

    End Sub
    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal str15 As String, ByVal str16 As String, ByVal str17 As String, ByVal str18 As String, ByVal str19 As String)
        row = New TableRow
        row.HorizontalAlign = HorizontalAlign.Right
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = str0.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str1.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str2.Trim()
        cell.Width = New Unit(200)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str3.Trim()
        cell.Width = New Unit(200)
        row.Cells.Add(cell)

        Dim x As Integer
        Dim cs As String
        Dim ind As Integer
        While (str4.Length > 0)
            ind = str4.IndexOf("^")
            cs = str4.Substring(0, ind)
            For x = 0 To 5
                Dim dex As Integer
                dex = cs.IndexOf(",")
                cell = New TableCell
                cell.Text = cs.Substring(0, dex)
                cell.Width = New Unit(100)
                row.Cells.Add(cell)
                cs = cs.Substring(dex + 1)
            Next
            str4 = str4.Substring(ind + 1)
        End While


        cell = New TableCell
        cell.Text = str5.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str6.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str7.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str8.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str9.Trim()
        If IsDate(cell.Text) Then
            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
        End If
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str10.Trim()
        If IsDate(cell.Text) Then
            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
        End If
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str11.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str12.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str13.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str14.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str15.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str16.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str17.Trim()
        cell.Width = New Unit(100)
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str18.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str19.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub
    Sub PIMasteryHeaderRow(ByVal str0 As String, ByVal str1 As Integer, ByVal str2 As Integer)
        Dim q As Integer
        row = New TableRow
        row.BackColor = Color.LightGray
        row.HorizontalAlign = HorizontalAlign.Right
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = "<b>" & str0.Trim() & "</b>"
        cell.Width = New Unit(560)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 4
        row.Cells.Add(cell)

        For q = str1 To str2
            cell = New TableCell
            cell.Text = " <b>" & q.ToString().Trim() & "月</b>"
            cell.Width = New Unit(300)
            cell.ColumnSpan = 6
            cell.HorizontalAlign = HorizontalAlign.Center
            row.Cells.Add(cell)
        Next
        cell = New TableCell
        cell.Text = "<b>完成天数总计</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.RowSpan = 2
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>小时天总计</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.RowSpan = 2
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>出勤天总计</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.RowSpan = 2
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>应发金额总计</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.RowSpan = 2
        row.Cells.Add(cell)


        cell = New TableCell
        cell.Text = "&nbsp;"
        cell.Width = New Unit(400)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 7
        row.Cells.Add(cell)

        exl.Rows.Add(row)

        'second row
        row = New TableRow
        row.BackColor = Color.LightGray
        cell = New TableCell
        cell.Text = "<b>工号</b>"
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>姓名</b>"
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>部门</b>"
        cell.Width = New Unit(200)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>工段</b>"
        cell.Width = New Unit(200)
        row.Cells.Add(cell)

        For q = str1 To str2
          
            cell = New TableCell
            cell.Text = "<b>小时天</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>出勤天</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>完成天数</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)


            cell = New TableCell
            cell.Text = "<b>应发金额</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>实发金额</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>备注</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)
        Next

        cell = New TableCell
        cell.Text = "<b>入公司日期</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>离职日期</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>计酬方式</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>职务</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>备注</b>"
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
        cell.Text = "<b>银行名称</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>银行账号</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>合同类型</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>工龄</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)
        exl.Rows.Add(row)
    End Sub
End Class

End Namespace
