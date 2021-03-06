'!*******************************************************************************!
'* @@ NAME				:	fixSalaryAnalyseExcel.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for fixSalaryAnalyseExcel.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	August 13 2006
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

Partial Class fixSalaryAnalyseExcel
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

            strSql = " select u.userID,b.workdate,u.userNo,u.username,isnull(d.name,''),isnull(w.name,''),u.enterdate,u.leavedate,CASE WHEN u.enterdate is null THEN '' ELSE CASE WHEN MONTH(u.enterdate)<=MONTH(getdate()) THEN datediff(year,u.enterdate,getdate()) ELSE datediff(year,u.enterdate,getdate())-1 END END,isnull(s2.systemCodeName,''),ISNULL(r.roleName,'') as rname,isnull(u.comments,'') as comment,isnull(s.systemCodeName,''),isnull(s1.systemCodeName,''),isnull(b.workprice,0),isnull(b.fixedsalary,0),isnull(b.SalaryAdjust,0) "
            strSql &= " From  BasicSalary b inner join tcpc0.dbo.Users u on u.userID=b.userID "
            strSql &= " LEFT OUTER JOIN Roles r ON r.roleID = u.roleID "
            strSql &= " LEFT OUTER JOIN Workshop w ON u.workshopID = w.id "
            strSql &= " LEFT OUTER JOIN departments d ON u.departmentID = d.departmentID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.SystemCode s on s.systemCodeID=u.employTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.SystemCode s1 on s1.systemCodeID=u.insuranceTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.SystemCode s2 on s2.systemCodeID=u.workTypeID "
            strSql &= " where year(b.workdate)='" & Request("year") & "' and month(b.workdate)>='" & Request("sm") & "' and month(b.workdate)<='" & Request("em") & "'"
            strSql &= " order by u.userID,b.workdate "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            Dim usercode As Integer = 0
            Dim flag As Boolean = False
            Dim materail(11) As String
            Dim i, j As Integer
            Dim ss As String
            PIMasteryHeaderRow(Request("year"), Request("sm"), Request("em"))
            While reader.Read
                If usercode <> reader(0) Then
                    If flag = True Then
                        If j - 1 < Request("em") Then
                            For i = 1 To Request("em") - j + 1
                                ss = ss & "0,0,0,^"
                            Next
                        End If

                        PIMasteryRow(materail(0), materail(1), materail(2), materail(3), materail(4), materail(5), materail(6), materail(7), materail(8), materail(9), materail(10), materail(11), ss)
                    End If
                    usercode = reader(0)
                    Array.Clear(materail, 0, 12)

                    For i = 0 To 11
                        If i = 4 Or i = 5 Then
                            If reader(i + 2).ToString() = "" Then
                                materail(i) = " "
                            Else
                                materail(i) = reader(i + 2)
                            End If
                        Else
                                materail(i) = reader(i + 2)
                        End If

                    Next
                    j = Request("sm")
                    ss = ""
                End If

                If j = CDate(reader("workdate")).Month Then
                    ss = ss & reader(14) & "," & reader(15) & "," & reader(16) & ",^"
                Else
                    For i = 1 To CDate(reader("workdate")).Month - j
                        ss = ss & "0,0,0,^"
                    Next
                End If

                j = j + 1
                flag = True
            End While
            reader.Close()

            If flag = True Then
                If j - 1 < Request("em") Then
                    For i = 1 To Request("em") - j + 1
                        ss = ss & "0,0,0,^"
                    Next
                End If

                PIMasteryRow(materail(0), materail(1), materail(2), materail(3), materail(4), materail(5), materail(6), materail(7), materail(8), materail(9), materail(10), materail(11), ss)
            End If

            Response.Clear()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312")
            Response.ContentType = "application/vnd.ms-excel"

            Response.Flush()
        End If
    End Sub
    Sub PIMasteryHeaderRow(ByVal str0 As String, ByVal str1 As Integer, ByVal str2 As Integer)
        Dim q As Integer
        row = New TableRow
        row.BackColor = Color.LightGray
        row.HorizontalAlign = HorizontalAlign.Right
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = "<b>" & str0.Trim() & "</b>"
        cell.Width = New Unit(1360)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 12
        row.Cells.Add(cell)

        For q = str1 To str2
            cell = New TableCell
            cell.Text = " <b>" & q.ToString().Trim() & "月</b>"
            cell.Width = New Unit(300)
            cell.ColumnSpan = 3
            cell.HorizontalAlign = HorizontalAlign.Center
            row.Cells.Add(cell)
        Next
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

        cell = New TableCell
        cell.Text = "<b>入公司日期</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>离职日期</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>工龄</b>"
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

        For q = str1 To str2
            cell = New TableCell
            cell.Text = "<b>工价</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>固定工资</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>工资调整</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)
        Next
        exl.Rows.Add(row)
    End Sub


    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, ByVal str12 As String)
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

        
        cell = New TableCell
        cell.Text = str4.Trim()
        If IsDate(cell.Text) Then
            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
        End If
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str5.Trim()
        If IsDate(cell.Text) Then
            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
        End If
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
        cell.Width = New Unit(100)
        row.Cells.Add(cell)


        cell = New TableCell
        cell.Text = str10.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str11.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        Dim x As Integer
        Dim cs As String
        Dim ind As Integer
        While (str12.Length > 0)
            ind = str12.IndexOf("^")
            cs = str12.Substring(0, ind)
            For x = 0 To 2
                Dim dex As Integer
                dex = cs.IndexOf(",")
                cell = New TableCell
                cell.Text = cs.Substring(0, dex)
                cell.Width = New Unit(100)
                row.Cells.Add(cell)
                cs = cs.Substring(dex + 1)
            Next
            str12 = str12.Substring(ind + 1)
        End While


        exl.Rows.Add(row)
    End Sub
End Class

End Namespace
