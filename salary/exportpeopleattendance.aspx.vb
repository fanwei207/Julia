'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-6-29
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Export PeopleAttendance To Excel
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class exportpeopleattendance
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Dim chk As New adamClass

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim strfinish As String
        Dim ss As String
        If Request("type") = 0 Then
            ss = "JobSalary"
        Else
            ss = "JobSalary_his"
        End If
        PIMasteryRow("工号", "姓名", "日期", "工序名称", "指标", "完成工时", "完成天数", "完成产量", "性质", "输入号", "输入员")
        strSql = " Select u.userNo,u.userName,w.Name,w.guideLine,j.workHours, "
        strSql &= " j.outputs,j.amount,j.ID,j.workProcedureID,w.lowestDailyWage,w.usedTimes,s.systemCodeName,j.workDate,j.timeSection ,u1.userID,u1.username as cname From " & ss & " j INNER JOIN workProcedure w ON "
        strSql &= " w.id = j.workProcedureID  INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID = w.typeID "
        strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = j.userCode " _
               & " INNER JOIN tcpc0.dbo.users u1 ON u1.userID = j.createdBy "
        If Session("uRole") = "1" Then
            If Request("all") = "true" Then
                strSql &= " Where month(j.workDate) = '" & Month(CDate(Request("date"))) & "' and year(j.workDate) = '" & Year(CDate(Request("date"))) & "' "
            Else
                strSql &= " Where j.workDate = '" & Request("date") & "'"
            End If
            strSql &= " and u.isTemp='" & Session("temp") & "'"
        Else
            strSql &= " INNER join manager_worker m ON m.worker=j.createdBy AND m.manager= " & Session("uID")
            If Request("all") = "true" Then
                strSql &= " Where month(j.workDate) = '" & Month(CDate(Request("date"))) & "' and year(j.workDate) = '" & Year(CDate(Request("date"))) & "' "
            Else
                strSql &= " Where j.workDate = '" & Request("date") & "'"
            End If
            strSql &= " and u.isTemp='" & Session("temp") & "'"
            If Request("dept") > 0 Then
                strSql &= " and u.departmentID = " & Request("dept")
            End If
            If Request("id") <> Nothing Then
                strSql &= " and cast(u.userNo as varchar) ='" & Request("id") & "'"
            End If
            If Request("name") <> Nothing Then
                strSql &= " and lower(j.userName) like N'%" & Request("name") & "%'"
            End If
            strSql &= " and u.plantCode='" & Session("PlantCode") & "' "

            strSql &= " union "
            strSql &= " Select u.userNo,u.userName,w.Name,w.guideLine,j.workHours, "
            strSql &= " j.outputs,j.amount,j.ID,j.workProcedureID,w.lowestDailyWage,w.usedTimes,s.systemCodeName,j.workDate,j.timeSection ,u1.userID,u1.username as cname From " & ss & " j INNER JOIN workProcedure w ON "
            strSql &= " w.id = j.workProcedureID  INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID = w.typeID "
            strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = j.userCode " _
                   & " INNER JOIN tcpc0.dbo.users u1 ON u1.userID = j.createdBy "
            If Request("all") = "true" Then
                strSql &= " Where month(j.workDate) = '" & Month(CDate(Request("date"))) & "' and year(j.workDate) = '" & Year(CDate(Request("date"))) & "' "
            Else
                strSql &= " Where j.workDate = '" & Request("date") & "'"
            End If
            strSql &= " and j.createdBy=" & Session("uID")
            strSql &= " and u.isTemp='" & Session("temp") & "'"
        End If
        If Request("dept") > 0 Then
            strSql &= " and u.departmentID = " & Request("dept")
        End If
        If Request("id") <> Nothing Then
            strSql &= " and cast(u.userNo as varchar) ='" & Request("id") & "'"
        End If
        If Request("name") <> Nothing Then
            strSql &= " and lower(j.userName) like N'%" & Request("name") & "%'"
        End If
        strSql &= " and u.plantCode='" & Session("PlantCode") & "' "
        strSql &= " order by j.ID desc"
        'Response.Write(strSql)
        'Exit Sub

        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While (reader.Read())
            If reader("guideLine") = 0 Then
                strfinish = Math.Round(CDbl(reader("workHours")) * 8, 2)
            Else
                strfinish = Math.Round(CInt(reader("outputs")) / CInt(reader("guideLine")), 2)
            End If
            PIMasteryRow(reader("userNo"), reader("userName"), reader("workDate"), reader("Name"), reader("guideLine"), strfinish, reader("workHours"), reader("outputs"), reader("timeSection"), reader("userid"), reader("cname"))
        End While
        reader.Close()
        PIMasteryRow("", "", "", "", "", "", "", "", "", "", "")
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=peopleattendance.xls")
        Response.Flush()
    End Sub

    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal str10 As String)
        row = New TableRow
        If str0 = "工号" Then
            row.BackColor = Color.LightGray
        Else
            row.BackColor = Color.White
        End If
        row.HorizontalAlign = HorizontalAlign.Left
        row.BorderWidth = New Unit(0)


        cell = New TableCell
        If (str0 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str0.Trim()
        End If
        cell.Width = New Unit(80)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        If (str1 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str1.Trim()
        End If
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str2 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str2.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        If (str3 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str3.Trim()
        End If
        cell.Width = New Unit(200)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str4 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str4.Trim()
        End If
        cell.Width = New Unit(80)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str5 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str5.Trim()
        End If
        cell.Width = New Unit(80)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str6 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str6.Trim()
        End If
        cell.Width = New Unit(80)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str7 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str7.Trim()
        End If
        cell.Width = New Unit(80)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str8 = Nothing) Then
            cell.Text = ""
        Else
            If str8 = "性质" Then
                cell.Text = str8
            Else
                Select Case str8
                    Case "0"
                        cell.Text = "A"
                    Case "1"
                        cell.Text = "B"
                    Case "2"
                        cell.Text = "C上"
                    Case "3"
                        cell.Text = "C下"
                    Case "4"
                        cell.Text = "C上国"
                    Case "5"
                        cell.Text = "C下国"
                End Select
            End If
        End If
        cell.Width = New Unit(80)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        If (str9 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str9.Trim()
        End If
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str10 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str10.Trim()
        End If
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub

End Class

End Namespace
