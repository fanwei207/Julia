Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class printoffduty
        Inherits BasePage
    Dim chk As New adamClass
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell

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
        If Not IsPostBack Then
             
        End If

        Dim startdate As DateTime
        Dim enddate As DateTime
        Dim i As Integer = 1

        If Request("fr") <> "" Then
            Try
                startdate = CDate(Request("fr"))
            Catch ex As Exception
            End Try
        End If

        If Request("to") <> "" Then
            Try
                enddate = CDate(Request("to"))
            Catch ex As Exception
            End Try
        End If

        PIMasteryRow("<b>序号</b>", "<b>工号</b>", "<b>员工姓名</b>", "<b>所属部门</b>", "<b>所属工段</b>", "<b>所属班组</b>", "<b>请假天数</b>")

        Dim Query As String

        Query = " SELECT u.userNo, u.userName, SUM(d.deductNum), ISNULL(dp.name,N'无'), ISNULL(ws.name,N'无'), ISNULL(wg.name,N'无') FROM DeductMoney d "
        Query &= " INNER JOIN tcpc0.dbo.users u ON d.userCode = u.userID "
        Query &= " INNER JOIN tcpc0.dbo.systemCode sc ON d.typeID = sc.systemCodeID AND sc.systemCodeName = N'请假' "
        Query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Deduct Money Type' "
        Query &= " LEFT OUTER JOIN Workshop wg ON u.workProcedureID = wg.id "
        Query &= " LEFT OUTER JOIN Workshop ws ON u.workshopID = ws.id "
        Query &= " LEFT OUTER JOIN departments dp ON u.departmentID = dp.departmentID "
        Query &= " WHERE  d.organizationID=" & Session("orgID") & " AND d.workDate>='" & startdate & "' AND d.workDate<'" & enddate.AddDays(1) & "'"
        Query &= " and u.isTemp='" & Session("temp") & "' "
        If Request("id") <> "" Then
            Query &= " AND u.userNo='" & Request("id") & "'"
        End If

        If Request("name") <> "" Then
            Query &= " AND u.userName Like N'%" & Request("name") & "%'"
        End If

        If Request("dp") <> 0 Then
            Query &= " AND u.departmentID=" & Request("dp")
        End If

        Query &= " GROUP BY d.userCode, u.userName, dp.name, ws.name, wg.name Order By u.userID "

        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)

        While (reader.Read())
            PIMasteryRow(i, reader(0), reader(1), reader(3), reader(4), reader(5), reader(2))
            i = i + 1
        End While

        reader.Close()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
    End Sub

    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String)
        row = New TableRow
        If str0 = "<b>序号</b>" Then
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
        cell.Width = New Unit(60)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str1 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str1.Trim()
        End If
        cell.Width = New Unit(60)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str2 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str2.Trim()
        End If
        cell.Width = New Unit(100)
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
        cell.Width = New Unit(200)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str5 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str5.Trim()
        End If
        cell.Width = New Unit(200)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str6 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str6.Trim()
        End If
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub
End Class

End Namespace
