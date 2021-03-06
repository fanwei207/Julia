Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class itemUsedExport
    Inherits System.Web.UI.Page
    Public chk As New adamClass
    Dim strSql As String
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
        Dim reader As SqlDataReader
        Dim numOrder As Integer = 0
        strSql = " Select i.code, iul.itemID From Items i Inner Join ItemUsedList iul On i.ID=iul.itemID And iul.userID='" & Session("uID") & "' And iul.plantID='" & Session("plantCode") & "' Where i.status<>2 "
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
        While reader.Read()
                PIMasteryRow("材料编号", reader(0), "", "", "", "", "", "")
                PIMasteryRow("序号", "编号", "描述", "分类", "状态", "等级", "锁定", "锁定信息")
            numOrder = calculateProductStructure(reader(1), 0, 0)
            calculateProductReplace(reader(1), numOrder, 0)
                PIMasteryRow("", "", "", "", "", "", "", "")
        End While
        reader.Close()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=itemUsedExport.xls")
        Response.Flush()
    End Sub

    Function calculateProductStructure(ByVal str As String, ByVal numSort As Integer, ByVal grade As Integer) As Integer
        Dim reader As SqlDataReader
        Dim numOrder As Integer = numSort + 1
            strSql = " Select i.code, i.description, ps.productID, ps.productStruID, ic.name, i.status,CASE WHEN l.xxwkf_log01=1 THEN N'生产锁定' WHEN l.xxwkf_log02=1 THEN N'安规锁定' WHEN l.xxwkf_log03=1 THEN N'技术锁定' ELSE '' END,case when  l.xxwkf_log01=1 THEN xxwkf_chr02 else '' end as xxwkf_chr02  From Items i Inner Join Product_stru ps On ps.productID=i.ID Inner Join ItemCategory ic On i.category=ic.id left join  QAD_Data.dbo.xxwkf_mstr l ON i.item_qad=l.xxwkf_chr01 Where ps.childID='" & str.Trim() & "'"
        If Request("type") = Nothing Then
            strSql &= " And i.status<>2 "
        End If
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
        While reader.Read()
                PIMasteryRow(numOrder, reader(0), reader(1), reader(4), reader(5), grade + 1, reader(6), reader(7))
            numOrder = calculateProductStructure(reader(2), numOrder, grade + 1)
            numOrder = numOrder + 1
        End While
        reader.Close()
        Return numOrder - 1
    End Function

    Sub calculateProductReplace(ByVal str As String, ByVal numSort As Integer, ByVal grade As Integer)
        Dim reader As SqlDataReader
        Dim numOrder As Integer = numSort + 1
            strSql = " Select i.code, i.description, ps.productID, ic.name, i.status,CASE WHEN l.xxwkf_log01=1 THEN N'生产锁定' WHEN l.xxwkf_log02=1 THEN N'安规锁定' WHEN l.xxwkf_log03=1 THEN N'技术锁定' ELSE '' END,case when  l.xxwkf_log01=1 THEN xxwkf_chr02 else '' end as xxwkf_chr02 From Items i Inner Join Product_stru ps On ps.productID=i.ID Inner Join product_replace pr On pr.prodStruID=ps.productStruID Inner Join ItemCategory ic On i.category=ic.id left join  QAD_Data.dbo.xxwkf_mstr l ON i.item_qad=l.xxwkf_chr01 Where pr.itemID='" & str.Trim() & "'"
        If Request("type") = Nothing Then
            strSql &= " And i.status<>2 "
        End If
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
        While reader.Read()
                PIMasteryRow(numOrder, reader(0), reader(1), reader(3), reader(4), grade + 1, reader(5), reader(6))
            numOrder = calculateProductStructure(reader(2), numOrder, grade + 1)
            numOrder = numOrder + 1
        End While
        reader.Close()
    End Sub

        Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String)
            Dim s As String = Request("s")
            Dim s1 As String = str3
            s1 = "~" & s1 & "~"
            If s.IndexOf(s1) >= 0 Then
                Exit Sub
            End If

            row = New TableRow
            If str0 = "序号" Then
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
            cell.Width = New Unit(75)
            cell.HorizontalAlign = HorizontalAlign.Center
            row.Cells.Add(cell)

            cell = New TableCell
            If (str1 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str1.Trim()
            End If
            cell.Width = New Unit(200)
            If cell.Text = "编号" Then
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            row.Cells.Add(cell)

            cell = New TableCell
            If (str2 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str2.Trim()
            End If
            cell.Width = New Unit(500)
            If cell.Text = "描述" Then
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            row.Cells.Add(cell)

            cell = New TableCell
            If (str3 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str3.Trim()
            End If
            cell.Width = New Unit(75)
            If cell.Text = "分类" Then
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            row.Cells.Add(cell)

            cell = New TableCell
            If (str4 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str4.Trim()
                If str4.Trim() = "2" Then
                    cell.Text = "停用"
                ElseIf str4.Trim() = "1" Then
                    cell.Text = "试用"
                ElseIf str4.Trim() = "0" Then
                    cell.Text = "使用"
                End If
            End If
            cell.Width = New Unit(75)
            cell.HorizontalAlign = HorizontalAlign.Center
            row.Cells.Add(cell)

            cell = New TableCell
            If (str5 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str5.Trim()
            End If
            cell.Width = New Unit(75)
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            row.Cells.Add(cell)

            cell = New TableCell
            If (str6 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str6.Trim()
            End If
            cell.Width = New Unit(75)
            cell.HorizontalAlign = HorizontalAlign.Center
            row.Cells.Add(cell)

            cell = New TableCell
            If (str7 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str7.Trim()
            End If
            cell.Width = New Unit(75)
            cell.HorizontalAlign = HorizontalAlign.Center
            row.Cells.Add(cell)

            exl.Rows.Add(row)
        End Sub
End Class

End Namespace
