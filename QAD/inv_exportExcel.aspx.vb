Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class inv_exportExcel
    Inherits System.Web.UI.Page

    Dim sqlStr As String
    Dim sqlStr1 As String
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

        If Request("invid") <> "" Then
            PIMasteryRow("地点", "库位", "零件号", "盘点数", "库存数", "差异", "成本", "金额", "单位", "描述")
            sqlStr = "SELECT isnull(a.qty2,0) as qty2,a.inv_id,a.site,a.loca,a.item,a.item_desc,isnull(b.qty1,0) as qty1,b.inv_id,b.site,b.loca,b.item,isnull(a.inv_cost,0),isnull(a.inv_unit,''),b.item_desc "
            sqlStr &= " FROM (SELECT SUM(inv_qty) AS qty2, inv_id, site, loca, item, item_desc,inv_cost,inv_unit "
            sqlStr &= " FROM inv_qad  WHERE (inv_id = '" & Request("invid") & "') AND (deletedBy IS NULL) GROUP BY inv_id, site, loca, item, item_desc,inv_cost,inv_unit) a "
            sqlStr &= " FULL OUTER JOIN "
            sqlStr &= " (SELECT SUM(inv_qty) AS qty1, inv_id, site, loca, item,item_desc"
            sqlStr &= " FROM Inv_count_detail WHERE loca <> 'NA' and (inv_id = '" & Request("invid") & "') AND (deletedBy IS NULL) GROUP BY inv_id, site, loca, item, item_desc) b "
            sqlStr &= " ON a.inv_id = b.inv_id AND a.site = b.site And a.loca = b.loca And a.item = b.item order by a.site,a.item,b.site,b.loca"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                If IsDBNull(reader(1)) Then
                    PIMasteryRow(reader(8).ToString(), reader(9).ToString(), reader(10).ToString(), Format(reader(6), "##,0.####"), Format(reader(0), "##,0.####"), Format(reader(6) - reader(0), "##,0.####"), "", "", "", reader(13).ToString())
                Else
                    PIMasteryRow(reader(2).ToString(), reader(3).ToString(), reader(4).ToString(), Format(reader(6), "##,0.####"), Format(reader(0), "##,0.####"), Format(reader(6) - reader(0), "##,0.####"), Format(reader(11), "##,0.#####"), Format(reader(11) * (reader(6) - reader(0)), "##,0.#####"), reader(12).ToString(), reader(5).ToString())
                End If
            End While
            reader.Close()
            PIMasteryRow("", "", "", "", "", "", "", "", "", "")
            PIMasteryRow("", "", "", "", "", "", "", "", "", "")
            PIMasteryRow("", "", "", "", "", "", "", "", "", "")
            PIMasteryRow("", "", "", "", "", "", "", "", "", "")


            Dim i As Integer = 0
            While i < 50
                PIMasteryRow("", "", "", "", "", "", "", "", "", "")
                i = i + 1
            End While


            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            Response.Flush()
        End If

    End Sub

    Sub PIMasteryRow(ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal qty1 As String, ByVal qty2 As String, ByVal qty3 As String, ByVal str41 As String, ByVal str51 As String, ByVal str42 As String, ByVal str4 As String)
        row = New TableRow
        row.BackColor = Color.White
        row.HorizontalAlign = HorizontalAlign.Center
        row.BorderWidth = New Unit(0)


        cell = New TableCell
        cell.Width = New Unit(60)
        cell.Text = str1

        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)


        cell = New TableCell
        cell.Text = str2
        cell.Width = New Unit(70)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str3
        cell.Width = New Unit(130)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = qty1
        cell.Width = New Unit(90)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = qty2
        cell.Width = New Unit(90)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = qty3
        cell.Width = New Unit(70)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str41
        cell.Width = New Unit(90)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str51
        cell.Width = New Unit(120)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str42
        cell.Width = New Unit(50)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Left

        cell.Text = str4
        cell.Width = New Unit(400)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        Dim i As Integer = 0
        While i < 20
            cell = New TableCell
            cell.Text = ""
            cell.Width = New Unit(60)
            row.Cells.Add(cell)
            i = i + 1
        End While
        exl.Rows.Add(row)
    End Sub


End Class

End Namespace
