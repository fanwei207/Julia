Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc


    Partial Class wo_order_export2
        Inherits System.Web.UI.Page

        Dim StrSql As String
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

            Dim reader2 As SqlDataReader
            Dim Str2 As String = ""
            Dim i As Integer = 0

            PIHeaderRow4("计划外用工单")
            PIHeaderRow1("公司：TCP CHINA --" & Request("site"), "打印日期:" & Format(Now, "yyyy-MM-dd"))
            PIHeaderRow3("")

            StrSql = " select woo_site, woo_cc_from_n,woo_cc_to_n,woo_nbr,woo_type,woo_qty,woo_req,isnull(woo_qty_comp,0),isnull(woo_price,0),isnull(woo_hours,0),woo_cc_duty_n,woo_supplier_n,acctApprDate,isnull(acctApprName,''),isnull(approvedName,'') from wo_order where woo_id ='" & Request("id") & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            If reader.Read() Then
                PIHeaderRow2("地点:" & reader(0).ToString(), "提出部门:" & reader(1).ToString(), "加工部门:" & reader(2).ToString())
                PIHeaderRow2("工单号:" & reader(3).ToString(), "类型:" & reader(4).ToString(), "数量:" & Format(reader(5), "##0.##"))
                PIHeaderRow3("工艺要求:" & reader(6).ToString())
                PIHeaderRow2("承担供应商:" & reader(11).ToString(), "承担部门:" & reader(10).ToString(), "入库数量:" & Format(reader(7), "##0.##") & "   总工时:" & Format(reader(9), "##0.##"))
                PIHeaderRow3("")

                PIMasteryRow("<b>工号</b>", "<b>姓名</b>", "<b>工序</b>", "<b>工序定额</b>", "<b>数量</b>", "<b>工费</b>", "<b>汇报人</b>", "<b>日期</b>", True)

                Dim total As Decimal = 0
                Dim total2 As Decimal = 0

                StrSql = " select cd.wocd_user_no,cd.wocd_username,cd.wocd_proc_name,cd.wocd_price,isnull(cd.wocd_proc_qty,0)+ isnull(cd.wocd_proc_adj,0),isnull(cd.wocd_cost,0), cd.createdBy, isnull(cd.createdDate,'') from wo_cost_detail cd "
                If Session("uRole") <> 1 Then
                    StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
                End If
                StrSql &= " where cd.id is not null  and isnull(cd.wocd_pcost,0)>0 and isnull(cd.wocd_type,'')<>'' and isnull(cd.wocd_type,'')<>'R' and isnull(cd.wocd_type,'')<>'T'"
                If Request("site") <> Nothing Then
                    StrSql &= " and cd.wocd_site ='" & Request("site") & "' "
                End If
                StrSql &= " and cd.wocd_nbr ='" & reader(3).ToString() & "' "
                StrSql &= " order by cd.wocd_site,cd.wocd_cc,cd.wocd_nbr,cd.wocd_id, cd.wocd_proc_id,cd.createdDate"
                'Response.Write(StrSql)
                'Exit Sub

                reader2 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                While (reader2.Read())
                    If Not IsDBNull(reader(12)) Then
                        total = total + reader2(4)
                        total2 = total2 + reader2(5)
                        PIMasteryRow(reader2(0).ToString(), reader2(1).ToString(), reader2(2).ToString(), reader2(3).ToString(), reader2(4).ToString(), reader2(5).ToString(), reader2(6).ToString(), reader2(7).ToString())
                    End If
                End While
                reader2.Close()

                PIMasteryRow("合计", "", "", "", Format(total, "##0.##"), Format(total2, "##0.##"), "", "")
                PIHeaderRow3("")
                If Not IsDBNull(reader(12)) Then
                    PIHeaderRow2("审核:" & reader(14).ToString(), "财务:" & reader(13).ToString(), "结算日期:" & Format(reader(12), "yyyy-MM-dd"))
                Else
                    PIHeaderRow2("审核:" & reader(14).ToString(), "财务:" & reader(13).ToString(), "结算日期:")
                End If
            End If
            reader.Close()

            'Exit Sub
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            Response.Flush()
        End Sub

        Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, Optional ByVal isTitle As Boolean = False)
            row = New TableRow
            If isTitle Then
                row.HorizontalAlign = HorizontalAlign.Center
            Else
                row.HorizontalAlign = HorizontalAlign.Left
            End If
            row.BackColor = Color.White
            row.VerticalAlign = VerticalAlign.Top
            row.Font.Size = FontUnit.Point(9)

            row.BorderWidth = New Unit(0)


            If isTitle Then
                cell = New TableCell
                cell.BackColor = Color.LightGray
                cell.Width = New Unit(50)
                cell.Text = str0
                row.Cells.Add(cell)
                cell = New TableCell
                cell.BackColor = Color.LightGray
                cell.Width = New Unit(80)
                cell.Text = str1
                row.Cells.Add(cell)
                cell = New TableCell
                cell.BackColor = Color.LightGray
                cell.Width = New Unit(200)
                cell.Text = str2
                row.Cells.Add(cell)
                cell = New TableCell
                cell.BackColor = Color.LightGray
                cell.Width = New Unit(80)
                cell.Text = str3
                row.Cells.Add(cell)
                cell = New TableCell
                cell.BackColor = Color.LightGray
                cell.Width = New Unit(80)
                cell.Text = str4
                row.Cells.Add(cell)
                cell = New TableCell
                cell.BackColor = Color.LightGray
                cell.Width = New Unit(80)
                cell.Text = str5
                row.Cells.Add(cell)
                cell = New TableCell
                cell.BackColor = Color.LightGray
                cell.Width = New Unit(100)
                cell.Text = str6
                row.Cells.Add(cell)
                cell = New TableCell
                cell.BackColor = Color.LightGray
                cell.Width = New Unit(100)
                cell.Text = str7
                row.Cells.Add(cell)
            Else
                cell = New TableCell
                cell.Text = str0
                row.Cells.Add(cell)
                cell = New TableCell
                cell.Text = str1
                row.Cells.Add(cell)
                cell = New TableCell
                cell.Text = str2
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Right
                cell.Text = str3
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Right
                cell.Text = str4
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Right
                cell.Text = str5
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str6
                cell.HorizontalAlign = HorizontalAlign.Right
                row.Cells.Add(cell)

                
                If IsDate(str7) Then
                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text = Format(Convert.ToDateTime(str7), "yyyy-MM-dd")
                    row.Cells.Add(cell)
                End If

                End If

                exl.Rows.Add(row)
        End Sub

        Sub PIHeaderRow1(ByVal str1 As String, ByVal str2 As String)
            row = New TableRow
            row.VerticalAlign = VerticalAlign.Top
            row.Font.Size = FontUnit.Point(9)

            row.BackColor = Color.White
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)

            cell = New TableCell
            cell.Text = str1
            cell.ColumnSpan = 5
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str2
            cell.ColumnSpan = 3
            row.Cells.Add(cell)

            exl.Rows.Add(row)
        End Sub
        Sub PIHeaderRow2(ByVal str1 As String, ByVal str2 As String, ByVal str3 As String)
            row = New TableRow
            row.VerticalAlign = VerticalAlign.Top
            row.Font.Size = FontUnit.Point(9)
            row.BackColor = Color.White
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)

            cell = New TableCell
            cell.Text = str1
            cell.ColumnSpan = 3
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str2
            cell.ColumnSpan = 3
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str3
            cell.ColumnSpan = 2
            row.Cells.Add(cell)

            exl.Rows.Add(row)
        End Sub
        Sub PIHeaderRow3(ByVal str1 As String)
            row = New TableRow
            row.VerticalAlign = VerticalAlign.Top
            row.Font.Size = FontUnit.Point(9)
            row.BackColor = Color.White
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)

            cell = New TableCell
            cell.Text = str1
            cell.ColumnSpan = 8
            row.Cells.Add(cell)

            exl.Rows.Add(row)
        End Sub
        Sub PIHeaderRow4(ByVal str1 As String)
            row = New TableRow
            row.VerticalAlign = VerticalAlign.Top
            row.Font.Size = FontUnit.Point(9)
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)

            cell = New TableCell
            cell.BackColor = Color.LightGray
            cell.Text = str1
            cell.ColumnSpan = 8
            row.Cells.Add(cell)

            exl.Rows.Add(row)
        End Sub
    End Class

End Namespace
