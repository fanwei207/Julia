Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc


    Partial Class ws_export1
        Inherits System.Web.UI.Page

        Dim strSQL As String
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
            'Dim nRet As Integer
            'nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19062001)
            'If nRet <= 0 Then
            '    Response.Redirect("/public/Message.aspx?type=" & nRet.ToString(), True)
            'End If

            Dim Str As String = ""
            Dim i As Integer
            Dim dm As String = ""
            Dim total_1 As Decimal = 0
            Dim total_2 As Decimal = 0

            PIHeaderRow("流水线动态数据统计  统计日期: " & Request("dd") & " 导出日期: " & Format(Now, "yyyy-MM-dd"))

            Str = "80^<b>公司</b>~^80^<b>成本中心</b>~^130^<b>工段线</b>~^<b>流量</b>~^<b>次品</b>~^<b>一次合格率</b>~^"
            PIMasteryRow(Str, True)



            Dim strPlant As String = ""
            Dim strPlantCode As String = ""
            Dim strCC As String = ""
            Dim strLine As String = ""
            Dim strLineName As String = ""
            Dim qty As Decimal = 0
            Dim qty_bad As Decimal = 0


            If Request("site") = 0 Or Request("site") = 1 Then
                strSQL = " select 1,N'上海振欣 SZX', ls_cc,ls_line,ls_linename,sum(isnull(ls_qty,0)),isnull(ls_status,N'正品') from SZXWS.LineData_SZX.dbo.ls_data "
                strSQL &= " where deletedBy is null  and ls_plant =1 "
                If Request("cc").Trim.Length > 0 And Request("cc").Trim <> "0" Then
                    strSQL &= " and ls_cc ='" & Request("cc").Trim() & "' "
                End If
                If Request("line").Trim.Length > 0 And Request("line").Trim <> "0" Then
                    strSQL &= " and ls_line='" & Request("line").Trim() & "' "
                End If
                If Request("part").Trim.Length > 0 Then
                    strSQL &= " and ls_part like '" & Request("part").Trim().Replace("*", "%") & "' "
                End If

                strSQL &= " and year(createdDate)=year('" & Request("dd") & "') and month(createdDate)=month('" & Request("dd") & "') and day(createdDate)=day('" & Request("dd") & "') "
                strSQL &= " group by ls_cc,ls_line,ls_linename,ls_status"
            End If

            If Request("site") = 0 Or Request("site") = 2 Then
                strSQL &= " Union all ( select 2,N'镇江强凌 ZQL',ls_cc,ls_line,ls_linename,sum(isnull(ls_qty,0)),isnull(ls_status,N'正品') from ZQLWS.LineData_ZQL.dbo.ls_data "
                strSQL &= " where deletedBy is null and ls_plant =2 "
                If Request("cc").Trim.Length > 0 And Request("cc").Trim <> "0" Then
                    strSQL &= " and ls_cc ='" & Request("cc").Trim() & "' "
                End If
                If Request("line").Trim.Length > 0 And Request("line").Trim <> "0" Then
                    strSQL &= " and ls_line='" & Request("line").Trim() & "' "
                End If
                If Request("part").Trim.Length > 0 Then
                    strSQL &= " and ls_part like '" & Request("part").Trim().Replace("*", "%") & "' "
                End If
                strSQL &= " and year(createdDate)=year('" & Request("dd") & "') and month(createdDate)=month('" & Request("dd") & "') and day(createdDate)=day('" & Request("dd") & "') "
                strSQL &= " group by ls_cc,ls_line,ls_linename,ls_status ) "
            End If

            If Request("site") = 0 Or Request("site") = 5 Then
                strSQL &= " Union all (select 5,N'扬州强凌 YQL', ls_cc,ls_line,ls_linename,sum(isnull(ls_qty,0)),isnull(ls_status,N'正品') from YQLWS.LineData_YQL.dbo.ls_data "
                strSQL &= " where deletedBy is null and ls_plant =5 "
                If Request("cc").Trim.Length > 0 And Request("cc").Trim <> "0" Then
                    strSQL &= " and ls_cc ='" & Request("cc").Trim() & "' "
                End If
                If Request("line").Trim.Length > 0 And Request("line").Trim <> "0" Then
                    strSQL &= " and ls_line ='" & Request("line").Trim() & "' "
                End If
                If Request("part").Trim.Length > 0 Then
                    strSQL &= " and ls_part like '" & Request("part").Trim().Replace("*", "%") & "' "
                End If
                strSQL &= " and year(createdDate)=year('" & Request("dd") & "') and month(createdDate)=month('" & Request("dd") & "') and day(createdDate)=day('" & Request("dd") & "') "
                strSQL &= " group by ls_cc,ls_line,ls_linename,ls_status ) "
            End If
            strSQL &= " order by ls_cc,ls_line "

            If strSQL.Substring(0, 10) = " Union all" Then
                strSQL = strSQL.Substring(10)
            End If

            'Response.Write(StrSql)

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While (reader.Read())
                If strCC <> "" And (strPlantCode <> reader(0).ToString().Trim() Or strCC <> reader(2).ToString().Trim() Or strLine <> reader(3).ToString().Trim()) Then
                    Str = strPlant & "~^"
                    Str &= strCC & "~^"
                    Str &= strLineName & "~^"
                    Str &= Format(qty, "##0.##") & "~^"
                    Str &= Format(qty_bad, "##0.##") & "~^"
                    If qty <> 0 Then
                        Str &= Format((1 - qty_bad / qty) * 100, "##0.##") & "~^"
                    Else
                        Str &= Format(0, "##0.##") & "~^"
                    End If

                    PIMasteryRow(Str)

                    strPlant = ""
                    strCC = ""
                    qty = 0
                    qty_bad = 0
                End If
                    strPlantCode = reader(0).ToString()
                    strPlant = reader(1).ToString()
                    strCC = reader(2).ToString()
                    strLine = reader(3).ToString()
                    strLineName = reader(4).ToString()
                    If reader(6).ToString.Trim <> "正品" Then
                        qty_bad = qty_bad + reader(5)
                        total_2 = total_2 + reader(5)
                    Else
                        qty = qty + reader(5)
                        total_1 = total_1 + reader(5)
                    End If
            End While
            reader.Close()
            If strCC <> "" Then
                Str = strPlant & "~^"
                Str &= strCC & "~^"
                Str &= strLineName & "~^"
                Str &= Format(qty, "##0.##") & "~^"
                Str &= Format(qty_bad, "##0.##") & "~^"
                If qty <> 0 Then
                    Str &= Format((1 - qty_bad / qty) * 100, "##0.##") & "~^"
                Else
                    Str &= Format(0, "##0.##") & "~^"
                End If
                PIMasteryRow(Str)

                strPlant = ""
                strCC = ""
                qty = 0
                qty_bad = 0
            End If

            Str = "合计~^~^~^"
            Str &= Format(total_1, "##0.##") & "~^"
            Str &= Format(total_2, "##0.##") & "~^"
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
                        str = ""
                        row.Cells.Add(cell)
                        Exit For
                    Else
                        cell.Text = str.Substring(0, ind)
                        str = str.Substring(ind + 2)
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
                cell.ColumnSpan = 5
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
