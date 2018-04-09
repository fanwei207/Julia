Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc


    Partial Class so_exportDetail
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
            'Dim nRet As Integer
            'nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19062001)
            'If nRet <= 0 Then
            '    Response.Redirect("/public/Message.aspx?type=" & nRet.ToString(), True)
            'End If

            Dim Str As String = ""
            Dim i As Integer

            PIHeaderRow("TCP 24/25 月销售库存数据--" & Format(Now, "yyyy-MM-dd"))

            Str = "100^<b>零件号</b>~^230^<b>描述</b>~^<b>JDE库存</b>~^<b>JDE销量总数</b>~^<b>JDE本月销量</b>~^<b>JDE月平均</b>~^<b>JDE周平均</b>~^<b>QAD库存</b>~^<b>QAD销量总数</b>~^<b>QAD本月销量</b>~^<b>QAD月平均</b>~^<b>QAD周平均</b>~^<b>日期</b>~^<b>JDE第1月</b>~^<b>JDE第2月</b>~^<b>JDE第3月</b>~^<b>JDE第4月</b>~^<b>JDE第5月</b>~^<b>JDE第6月</b>~^<b>JDE第7月</b>~^<b>JDE第8月</b>~^<b>JDE第9月</b>~^<b>JDE第10月</b>~^<b>JDE第11月</b>~^<b>JDE第12月</b>~^<b>JDE第13月</b>~^<b>JDE第14月</b>~^<b>JDE第15月</b>~^<b>JDE第16月</b>~^<b>JDE第17月</b>~^<b>JDE第18月</b>~^<b>JDE第19月</b>~^<b>JDE第20月</b>~^<b>JDE第21月</b>~^<b>JDE第22月</b>~^<b>JDE第23月</b>~^<b>JDE第24月</b>~^<b>QAD第1月</b>~^<b>QAD第2月</b>~^<b>QAD第3月</b>~^<b>QAD第4月</b>~^<b>QAD第5月</b>~^<b>QAD第6月</b>~^<b>QAD第7月</b>~^<b>QAD第8月</b>~^<b>QAD第9月</b>~^<b>QAD第10月</b>~^<b>QAD第11月</b>~^<b>QAD第12月</b>~^<b>QAD第13月</b>~^<b>QAD第14月</b>~^<b>QAD第15月</b>~^<b>QAD第16月</b>~^<b>QAD第17月</b>~^<b>QAD第18月</b>~^<b>QAD第19月</b>~^<b>QAD第20月</b>~^<b>QAD第21月</b>~^<b>QAD第22月</b>~^<b>QAD第23月</b>~^<b>QAD第24月</b>~^"
            PIMasteryRow(Str, True)

            sqlStr = "select tcp_code,tcp_desc,isnull(jqty_oh,0),isnull(jtotal,0),isnull(j25,0),isnull(javg_m,0),isnull(javg_w,0),isnull(qqty_oh,0),isnull(qtotal,0),isnull(q25,0),isnull(qavg_m,0),isnull(qavg_w,0),createdDate,isnull(j1,0),isnull(j2,0),isnull(j3,0),isnull(j4,0),isnull(j5,0),isnull(j6,0),isnull(j7,0),isnull(j8,0),isnull(j9,0),isnull(j10,0),isnull(j11,0),isnull(j12,0),isnull(j13,0),isnull(j14,0),isnull(j15,0),isnull(j16,0),isnull(j17,0),isnull(j18,0),isnull(j19,0),isnull(j20,0),isnull(j21,0),isnull(j22,0),isnull(j23,0),isnull(j24,0),isnull(q1,0),isnull(q2,0),isnull(q3,0),isnull(q4,0),isnull(q5,0),isnull(q6,0),isnull(q7,0),isnull(q8,0),isnull(q9,0),isnull(q10,0),isnull(q11,0),isnull(q12,0),isnull(q13,0),isnull(q14,0) ,isnull(q15,0),isnull(q16,0),isnull(q17,0),isnull(q18,0),isnull(q19,0),isnull(q20,0),isnull(q21,0),isnull(q22,0),isnull(q23,0),isnull(q24,0) "
            sqlStr &= " from qadplan.dbo.tcp_so where tcp_code is not null order by jtotal desc"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                Str = reader(0).ToString() & "~^"
                Str &= reader(1).ToString() & "~^"
                Str &= Format(reader(2), "##0.##") & "~^"
                Str &= Format(reader(3), "##0.##") & "~^"
                Str &= Format(reader(4), "##0.##") & "~^"
                Str &= Format(reader(5), "##0.##") & "~^"
                Str &= Format(reader(6), "##0.##") & "~^"
                Str &= Format(reader(7), "##0.##") & "~^"
                Str &= Format(reader(8), "##0.##") & "~^"
                Str &= Format(reader(9), "##0.##") & "~^"
                Str &= Format(reader(10), "##0.##") & "~^"
                Str &= Format(reader(11), "##0.##") & "~^"
                If IsDBNull(reader(12)) Then
                    Str &= "~^"
                Else
                    Str &= Format(reader(12), "yyyy.MM.dd") & "~^"
                End If
                Str &= Format(reader(13), "##0.##") & "~^"
                Str &= Format(reader(14), "##0.##") & "~^"
                Str &= Format(reader(15), "##0.##") & "~^"
                Str &= Format(reader(16), "##0.##") & "~^"
                Str &= Format(reader(17), "##0.##") & "~^"
                Str &= Format(reader(18), "##0.##") & "~^"
                Str &= Format(reader(19), "##0.##") & "~^"
                Str &= Format(reader(20), "##0.##") & "~^"
                Str &= Format(reader(21), "##0.##") & "~^"
                Str &= Format(reader(22), "##0.##") & "~^"
                Str &= Format(reader(23), "##0.##") & "~^"
                Str &= Format(reader(24), "##0.##") & "~^"
                Str &= Format(reader(25), "##0.##") & "~^"
                Str &= Format(reader(26), "##0.##") & "~^"
                Str &= Format(reader(27), "##0.##") & "~^"
                Str &= Format(reader(28), "##0.##") & "~^"
                Str &= Format(reader(29), "##0.##") & "~^"
                Str &= Format(reader(30), "##0.##") & "~^"
                Str &= Format(reader(31), "##0.##") & "~^"
                Str &= Format(reader(32), "##0.##") & "~^"
                Str &= Format(reader(33), "##0.##") & "~^"
                Str &= Format(reader(34), "##0.##") & "~^"
                Str &= Format(reader(35), "##0.##") & "~^"
                Str &= Format(reader(36), "##0.##") & "~^"

                Str &= Format(reader(37), "##0.##") & "~^"
                Str &= Format(reader(38), "##0.##") & "~^"
                Str &= Format(reader(39), "##0.##") & "~^"
                Str &= Format(reader(40), "##0.##") & "~^"
                Str &= Format(reader(41), "##0.##") & "~^"
                Str &= Format(reader(42), "##0.##") & "~^"
                Str &= Format(reader(43), "##0.##") & "~^"
                Str &= Format(reader(44), "##0.##") & "~^"
                Str &= Format(reader(45), "##0.##") & "~^"
                Str &= Format(reader(46), "##0.##") & "~^"
                Str &= Format(reader(47), "##0.##") & "~^"
                Str &= Format(reader(48), "##0.##") & "~^"
                Str &= Format(reader(49), "##0.##") & "~^"
                Str &= Format(reader(50), "##0.##") & "~^"
                Str &= Format(reader(51), "##0.##") & "~^"
                Str &= Format(reader(52), "##0.##") & "~^"
                Str &= Format(reader(53), "##0.##") & "~^"
                Str &= Format(reader(54), "##0.##") & "~^"
                Str &= Format(reader(55), "##0.##") & "~^"
                Str &= Format(reader(56), "##0.##") & "~^"
                Str &= Format(reader(57), "##0.##") & "~^"
                Str &= Format(reader(58), "##0.##") & "~^"
                Str &= Format(reader(59), "##0.##") & "~^"
                Str &= Format(reader(60), "##0.##") & "~^"
                PIMasteryRow(Str)
            End While
            reader.Close()
            
            
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
