'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-3
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Export Parts To Excel
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class partprint
    Inherits System.Web.UI.Page
    Dim sqlStr As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Public chk As New adamClass

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
        Dim sqlStr As String
        PIMasteryRow("PartID", "部件号", "部件描述", "分类", "状态")
        If Request("code") = Nothing Then
            If Request("all") = "true" Then
                sqlStr = " Select i.id, i.code, isnull(i.description,''), isnull(ic.name,''), i.status From Items i " _
                       & " Inner Join ItemCategory ic On ic.id=i.category Where i.type=0 Order By i.id "
            Else
                sqlStr = " Select i.id, i.code, isnull(i.description,''), isnull(ic.name,''), i.status From Items i " _
                       & " Inner Join ItemCategory ic On ic.id=i.category " _
                       & " Where ic.name=N'" & Request("cat") & "' Where i.type=0 Order By i.id "
            End If
        End If
        If Request("cat") = Nothing Then
            If Request("all") = "true" Then
                sqlStr = " Select i.id, i.code, isnull(i.description,''), isnull(ic.name,''), i.status From Items i " _
                       & " Inner Join ItemCategory ic On ic.id=i.category Where i.type=0 Order By i.id "
            Else
                sqlStr = " Select i.id, i.code, isnull(i.description,''), isnull(ic.name,''), i.status From Items i " _
                       & " Inner Join ItemCategory ic On ic.id=i.category " _
                       & " Where i.code Like N'%" & Request("code") & "%' Where i.type=0 Order By i.id "
            End If
        End If
        reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, sqlStr)
        While (reader.Read())
            PIMasteryRow(reader(0), reader(1), reader(2), reader(3), reader(4))
        End While
        reader.Close()
        'PIMasteryRow("", "", "", "", "")
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=sheet1.xls")
        Response.Flush()
    End Sub

    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String)
        row = New TableRow
        If str0 = "PartID" Then
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
        cell.Width = New Unit(50)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str1 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str1.Trim()
        End If
        cell.Width = New Unit(200)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str2 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str2.Trim()
        End If
        cell.Width = New Unit(500)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str3 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str3.Trim()
        End If
        cell.Width = New Unit(70)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str4 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str4.Trim()
            If str4.Trim()="2" Then
                cell.Text = "停用"
            ElseIf str4.Trim()="1" Then
                cell.Text = "试用"
            ElseIf str4.Trim() = "0" Then
                cell.Text = "使用"
            End If
        End If
        cell.Width = New Unit(50)
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub

End Class

End Namespace
