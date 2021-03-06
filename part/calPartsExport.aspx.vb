Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class calPartsExport
    Inherits System.Web.UI.Page
    Public chk As New adamClass
    Dim strSql As String
    Dim ds As DataSet

    Dim row As TableRow
    Dim cell As TableCell
    Dim lbl As Label

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
        ReportsRow(tb_report, 1, 1, "导出:", Session("uName"), "", "")
        ReportsRow(tb_report, 1, 1, "导出时间:", Format(DateTime.Now, "yyyy-MM-dd"), "", "")
        ReportsRow(tb_report, 1, 1, "数据来源:", Session("importHist"), "", "")

        'add product list
        ReportsRow(tb_report, 3, 1, "", "", "", "")
        ReportsRow(tb_report, 3, 2, "<b>产品</b>", "", "", "")
        ReportsRow(tb_report, 1, 2, "产品编码", "产品数量", "", "")

        strSql = " SELECT p.Code, item_qty " _
             & " FROM part_order_mrp_tmp pomt " _
             & " INNER JOIN tcpc0.dbo.Items p ON p.ID=pomt.item_id AND p.type=2 " _
             & " WHERE sessionID='" & Session("uID") & "' AND item_type='PROD'" _
             & " ORDER BY p.Code "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    ReportsRow(tb_report, 1, 1, .Rows(i).Item("Code"), Format(.Rows(i).Item("item_qty"), "##,##0.00"), "", "")
                Next
            End If
        End With
        ds.Reset()


        'add part list
        ReportsRow(tb_report, 3, 1, "", "", "", "")
        ReportsRow(tb_report, 3, 2, "<b>部件</b>", "", "", "")
        ReportsRow(tb_report, 2, 2, "类别", "部件号", "部件描述", "所需数量")

        strSql = " SELECT p.Code, pomt.item_qty,pc.name as category,ISNULL(p.description,'') as partDesc " _
            & " FROM part_order_mrp_tmp pomt " _
            & " INNER JOIN tcpc0.dbo.Items p ON p.ID=pomt.item_id AND p.type=0 " _
            & " INNER JOIN tcpc0.dbo.ItemCategory pc ON p.category=pc.id AND pc.type=0 " _
            & " WHERE sessionID='" & Session("uID") & "' AND item_type='PART'" _
            & " ORDER BY p.Code "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    ReportsRow(tb_report, 2, 1, .Rows(i).Item("category"), .Rows(i).Item("Code"), .Rows(i).Item("partDesc"), Format(.Rows(i).Item("item_qty"), "##,##0.00"))
                Next
            End If
        End With
        ds.Reset()


        'add INTM List
        ReportsRow(tb_report, 3, 1, "", "", "", "")
        ReportsRow(tb_report, 3, 2, "<b>中间件</b>", "", "", "")
        ReportsRow(tb_report, 2, 2, "类别", "产品编码", "产品描述", "所需数量")

        strSql = " SELECT p.Code, item_qty,ISNULL(p.description,'') as description,pc.name as category " _
            & " FROM part_order_mrp_tmp pomt " _
            & " INNER JOIN tcpc0.dbo.Items p ON p.ID=pomt.item_id AND p.type<>0 " _
            & " INNER JOIN tcpc0.dbo.Itemcategory pc ON pc.id=p.category AND pc.type<>0 " _
            & " WHERE sessionID='" & Session("uID") & "' AND item_type='INTM'" _
            & " ORDER BY p.Code "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    ReportsRow(tb_report, 2, 2, .Rows(i).Item("category"), .Rows(i).Item("Code"), .Rows(i).Item("description"), Format(.Rows(i).Item("item_qty"), "##,##0.00"))
                Next
            End If
        End With
        ds.Reset()

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=partsDetail.xls")
        Response.Flush()
    End Sub

    Sub ReportsRow(ByVal paraTable As Table, ByVal paraRowType As Integer, ByVal paraRowBack As Integer, _
                    ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String)
        row = New TableRow
        row.Width = New Unit(800)

        Select Case paraRowType
            Case 1 'is title
                row.HorizontalAlign = HorizontalAlign.Left
                row.BorderWidth = New Unit(0)

                cell = New TableCell
                If paraRowBack = 1 Then
                    cell.BackColor = Color.White
                Else
                    cell.BackColor = Color.LightGray
                End If
                cell.Text = str1.Trim()
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str2.Trim()
                If paraRowBack = 1 Then
                    cell.BackColor = Color.White
                Else
                    cell.BackColor = Color.LightGray
                End If
                cell.ColumnSpan = 3
                row.Cells.Add(cell)

            Case 2 'is normal row
                row.HorizontalAlign = HorizontalAlign.Left
                row.BorderWidth = New Unit(0)

                cell = New TableCell
                If paraRowBack = 1 Then
                    cell.BackColor = Color.White
                    cell.HorizontalAlign = HorizontalAlign.Center
                Else
                    cell.BackColor = Color.LightGray
                    cell.HorizontalAlign = HorizontalAlign.Center
                End If
                cell.Text = str1.Trim()
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                If paraRowBack = 1 Then
                    cell.BackColor = Color.White
                    cell.HorizontalAlign = HorizontalAlign.Left
                Else
                    cell.BackColor = Color.LightGray
                    cell.HorizontalAlign = HorizontalAlign.Center
                End If
                cell.Text = str2.Trim()
                cell.Width = New Unit(200)
                row.Cells.Add(cell)

                cell = New TableCell
                If paraRowBack = 1 Then
                    cell.BackColor = Color.White
                    cell.HorizontalAlign = HorizontalAlign.Left
                Else
                    cell.BackColor = Color.LightGray
                    cell.HorizontalAlign = HorizontalAlign.Center
                End If
                cell.Text = str3.Trim()
                cell.Width = New Unit(400)
                row.Cells.Add(cell)

                cell = New TableCell
                If paraRowBack = 1 Then
                    cell.BackColor = Color.White
                    cell.HorizontalAlign = HorizontalAlign.Right
                Else
                    cell.BackColor = Color.LightGray
                    cell.HorizontalAlign = HorizontalAlign.Center
                End If
                cell.Text = str4.Trim()
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

            Case 3
                row.HorizontalAlign = HorizontalAlign.Left
                row.BorderWidth = New Unit(0)

                cell = New TableCell
                If paraRowBack = 1 Then
                    cell.BackColor = Color.White
                Else
                    cell.BackColor = Color.LightGray
                End If
                cell.Text = str1.Trim()
                cell.ColumnSpan = 4
                row.Cells.Add(cell)
        End Select

        paraTable.Rows.Add(row)
    End Sub
End Class

End Namespace
