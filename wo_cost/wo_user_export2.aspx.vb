Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc


    Partial Class wo_user_export2
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

            Dim Str As String = ""
            Dim i As Integer
            Dim dm As String = ""

            PIHeaderRow("工单员工工序报表:  从 " & Request("sy") & " 至 " & Request("ey") & " ，导出日期:" & Format(Now, "yyyy-MM-dd"))

            Str = "50^<b>工号</b>~^50^<b>姓名</b>~^50^<b>地点</b>~^80^<b>加工单号</b>~^80^<b>加工单ID</b>~^80^<b>成本中心</b>~^130^<b>零件号</b>~^200^<b>工序</b>~^<b>数量</b>~^<b>工时</b>~^<b>工序标准</b>~^<b>日期</b>~^<b>工单类型</b>~^<b>创建日期</b>~^50^<b>创建人</b>~^"
            PIMasteryRow(Str, True)

            Dim total As Decimal = 0


            StrSql = " select cd.wocd_user_no,cd.wocd_username,cd.wocd_site,cd.wocd_nbr,cd.wocd_id,cd.wocd_cc, cd.wocd_part,cd.wocd_proc_name,cd.wocd_proc_qty,CASE WHEN ISNULL(cd.wocd_type,'')='' THEN Round(isnull(cd.wocd_cost,0) * 1.02, 5) ELSE isnull(cd.wocd_cost,0) END ,CASE WHEN ISNULL(cd.wocd_type,'')='' THEN Round(isnull(cd.wocd_price,0) * 1.02,5) ELSE isnull(cd.wocd_price,0) END As wocd_price,wocd_date, isnull(cd.wocd_type,''),cd.createdDate,cd.createdBy from wo_cost_detail cd "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If

            StrSql &= " where cd.id is not null "
            ' and isnull(cd.wocd_pcost,0)>0 "
            StrSql &= " AND cd.wocd_date >= '" & Request("sy") & "' AND cd.wocd_date <='" & Request("ey") & "'"
            If Request("uid") <> Nothing Then
                StrSql &= " and cd.wocd_userid ='" & Request("uid") & "' "
            End If
            StrSql &= " order by cd.createdDate desc "

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())

               
                Str = reader(0).ToString() & "~^"
                Str &= reader(1).ToString() & "~^"
                Str &= reader(2).ToString() & "~^"
                Str &= reader(3).ToString() & "~^"
                Str &= reader(4).ToString() & "~^"
                Str &= reader(5).ToString() & "~^"
                Str &= reader(6).ToString() & "~^"
                Str &= reader(7).ToString() & "~^"
                Str &= reader(8).ToString() & "~^"
                Str &= reader(9).ToString() & "~^"
                Str &= reader(10).ToString() & "~^"
                Str &= reader(11).ToString() & "~^"
                Str &= reader(12).ToString() & "~^"
                Str &= reader(13).ToString() & "~^"
                Str &= reader(14).ToString() & "~^"
                total = total + reader(9)
                PIMasteryRow(Str)

                


            End While
            reader.Close()


            Str = "合计~^"
            Str &= "~^"
            Str &= "~^"
            Str &= "~^"
            Str &= "~^"
            Str &= "~^"
            Str &= "~^"
            Str &= "~^"
            Str &= "~^"
            Str &= Format(total, "##0.##") & "~^"
            Str &= "~^"
            Str &= "~^"
            Str &= "~^"
            PIMasteryRow(Str)


            While (i < 100)
                PIMasteryRow("")
                i = i + 1
            End While
            'Exit Sub
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            Response.Flush()
        End Sub

        Sub PIMasteryRow(ByVal str As String, Optional ByVal isTitle As Boolean = False)
            row = New TableRow
            If isTitle Then
                row.BackColor = Color.LightGray
                row.HorizontalAlign = HorizontalAlign.Center
            Else
                row.BackColor = Color.White
                row.HorizontalAlign = HorizontalAlign.Left
            End If
            row.VerticalAlign = VerticalAlign.Top
            row.Font.Size = FontUnit.Point(9)

            row.BorderWidth = New Unit(0)

            Dim i As Integer = 0
            Dim ind As Integer

            If isTitle Then
                Dim wid As Integer
                For i = 0 To 200
                    wid = 100
                    cell = New TableCell
                    ind = str.IndexOf("~^")
                    If (ind = -1) Then
                        ind = str.IndexOf("L~")
                        If (ind > -1) Then
                            cell.HorizontalAlign = HorizontalAlign.Left
                            str = str.Substring(2)
                        End If

                        ind = str.IndexOf("^")
                        If (ind = -1) Then
                            cell.Text = str
                        Else
                            wid = Convert.ToInt32(str.Substring(0, ind))
                            cell.Text = str.Substring(ind + 1)
                        End If
                        str = ""
                        cell.Width = New Unit(wid)
                        row.Cells.Add(cell)
                        Exit For
                    Else
                        cell.Text = str.Substring(0, ind)
                        str = str.Substring(ind + 2)

                        ind = cell.Text.IndexOf("L~")
                        If (ind > -1) Then
                            cell.HorizontalAlign = HorizontalAlign.Left
                            cell.Text = cell.Text.Substring(2)
                        End If

                        ind = cell.Text.IndexOf("^")
                        If (ind > -1) Then
                            wid = Convert.ToInt32(cell.Text.Substring(0, ind))
                            cell.Text = cell.Text.Substring(ind + 1)
                        End If
                    End If
                    cell.Width = New Unit(wid)
                    row.Cells.Add(cell)
                Next

                If i < 50 Then
                    For ind = i To 50
                        cell = New TableCell
                        cell.Text = ""
                        cell.Width = New Unit(60)
                        row.Cells.Add(cell)
                    Next
                End If
            Else
                For i = 0 To 200
                    cell = New TableCell
                    ind = str.IndexOf("~^")
                    If (ind = -1) Then
                        cell.Text = str
                        If IsNumeric(cell.Text) Then
                            If cell.Text.Trim.Length > 8 Then
                                If cell.Text.Trim.IndexOf(".") = -1 Then
                                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                                End If
                            End If
                            cell.HorizontalAlign = HorizontalAlign.Right
                        ElseIf IsDate(cell.Text) Then
                            If CDate(cell.Text) = CDate("1900-01-01") Then
                                cell.Text = ""
                            Else
                                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
                            End If
                        End If
                        str = ""
                        row.Cells.Add(cell)
                        Exit For
                    Else
                        cell.Text = str.Substring(0, ind)
                        str = str.Substring(ind + 2)
                        If IsNumeric(cell.Text) Then
                            If cell.Text.Trim.Length > 8 Then
                                If cell.Text.Trim.IndexOf(".") = -1 Then
                                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                                End If
                            End If
                            cell.HorizontalAlign = HorizontalAlign.Right
                        ElseIf IsDate(cell.Text) Then
                            If CDate(cell.Text) = CDate("1900-01-01") Then
                                cell.Text = ""
                            Else
                                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
                            End If
                        End If
                        End If
                        row.Cells.Add(cell)
                Next

                If i < 50 Then
                    For ind = i To 50
                        cell = New TableCell
                        cell.Text = ""
                        cell.Width = New Unit(60)
                        row.Cells.Add(cell)
                    Next
                End If
            End If

            exl.Rows.Add(row)
        End Sub

        Sub PIHeaderRow(ByVal str As String)
            row = New TableRow
            row.BackColor = Color.White
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)

            Dim i As Integer = 0
            Dim ind As Integer

            While (str.Length > 0)
                cell = New TableCell
                ind = str.IndexOf("~")
                If (ind = -1) Then
                    cell.Text = str
                    str = ""
                Else
                    cell.Text = str.Substring(0, ind)
                    str = str.Substring(ind + 1)
                End If
                'If (i = 0) Then
                '    i = 1
                'Else
                cell.ColumnSpan = 8
                i = i + 10
                'End If
                If IsNumeric(cell.Text) Then
                    'If cell.Text.Trim.Length > 8 Then
                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                    'End If
                ElseIf IsDate(cell.Text) Then
                    If CDate(cell.Text) = CDate("1900-01-01") Then
                        cell.Text = ""
                    Else
                        cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
                    End If
                End If
                row.Cells.Add(cell)
            End While

            If i < 50 Then
                For ind = i To 50
                    cell = New TableCell
                    cell.Text = ""
                    cell.Width = New Unit(60)
                    row.Cells.Add(cell)
                Next
            End If

            exl.Rows.Add(row)
        End Sub
    End Class

End Namespace
