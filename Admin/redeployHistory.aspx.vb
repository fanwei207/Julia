Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class redeployHistory
        Inherits System.Web.UI.Page
    Dim strSql As String
    Dim ds As DataSet
    Dim row As TableRow
    Dim cell As TableCell
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal

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
            strSql = "  SELECT u.userName,dpt.name as dptName, isnull(ws.name,'') as wsName, isnull(r.roleName,'') as roleName, ExchangeDate " _
                    & " FROM User_Exchange_Department ued " _
                    & " INNER JOIN tcpc0.dbo.users u ON u.userID=ued.UserID " _
                    & " LEFT JOIN departments dpt ON dpt.departmentID=ued.DepartmentID " _
                    & " LEFT JOIN workshop ws ON ws.id = ued.WorkShopID " _
                    & " LEFT JOIN roles r ON r.roleID = ued.RoleID" _
                & " WHERE ued.UserID='" & Request("uID") & "'" _
                & " ORDER BY CreatedDate DESC "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        Dim dt As New DataTable
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                HistoryTitle("员工：", .Rows(0).Item("userName"))
                HistoryTitle("所在部门：", .Rows(0).Item("dptName"))
                HistoryTitle("导出时间：", DateTime.Now)
                    HistoryRow(0, "<b>调动日期</b>", "<b>部门</b>", "<b>工段/工序</b>", "<b>职务/岗位</b>")

                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                        HistoryRow(1, Format(CDate(.Rows(i).Item("ExchangeDate")), "yyyy-MM-dd"), .Rows(i).Item("dptName").ToString(), .Rows(i).Item("wsName").ToString(), .Rows(i).Item("roleName").ToString())
                Next
            Else
                ltlAlert.Text = "alert('无相关记录。');window.close();"
                Exit Sub
            End If
        End With
        ds.Reset()

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=EmployeeMoveInfo.xls")
        Response.Flush()

    End Sub

    Sub HistoryTitle(ByVal str1 As String, ByVal str2 As String)
        row = New TableRow
        row.BackColor = Color.White
        row.BorderWidth = New Unit(0)


        cell = New TableCell
        If (str1 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str1.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str2 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str2.Trim()
        End If
            cell.Width = New Unit(600)
            cell.HorizontalAlign = HorizontalAlign.Left
            cell.ColumnSpan = 3
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub

        Sub HistoryRow(ByVal RowBackColorType As Integer, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String)
            row = New TableRow
            row.BackColor = Color.White
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)


            cell = New TableCell
            If (str1 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str1.Trim()
            End If
            cell.Width = New Unit(100)
            If RowBackColorType = 0 Then
                cell.BackColor = System.Drawing.Color.LightGray
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.BackColor = System.Drawing.Color.White
                cell.HorizontalAlign = HorizontalAlign.Center
            End If
            row.Cells.Add(cell)

            cell = New TableCell
            If (str2 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str2.Trim()
            End If
            cell.Width = New Unit(200)
            If RowBackColorType = 0 Then
                cell.BackColor = System.Drawing.Color.LightGray
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.BackColor = System.Drawing.Color.White
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            row.Cells.Add(cell)

            cell = New TableCell
            If (str3 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str3.Trim()
            End If
            cell.Width = New Unit(200)
            If RowBackColorType = 0 Then
                cell.BackColor = System.Drawing.Color.LightGray
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.BackColor = System.Drawing.Color.White
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            row.Cells.Add(cell)

            cell = New TableCell
            If (str4 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str4.Trim()
            End If
            cell.Width = New Unit(200)
            If RowBackColorType = 0 Then
                cell.BackColor = System.Drawing.Color.LightGray
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.BackColor = System.Drawing.Color.White
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            row.Cells.Add(cell)

            exl.Rows.Add(row)
        End Sub

End Class

End Namespace
