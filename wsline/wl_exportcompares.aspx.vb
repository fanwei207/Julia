Imports System.Data.SqlClient
Imports adamFuncs
Imports System.Data
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc

    Partial Class wl_exportcompares
        Inherits System.Web.UI.Page

        Dim StrSql As String
        Dim StrSql2 As String
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

            Dim Str As String = ""



            If Request("a") <> 1 Then
                PIHeaderRow("有考勤无工单工时  导出日期:" & Today.ToString())
                Str = "60^<b>成本中心</b>~^50^<b>工号</b>~^70^<b>姓名</b>~^70^<b>考勤机号</b>~^110^<b>比较日期</b>~^110^<b>上班时间</b>~^110^<b>下班时间</b>~^<b>考勤工时</b>~^"
                PIMasteryRow(Str, True)

                StrSql = " select a.userno,a.username,a.fingerprint,a.starttime,a.endtime,a.totalhr,isnull(b.wo2_userID,0),isnull(c.wocd_userid,0)"
                StrSql += " from tcpc0.dbo.hr_Attendance_calendar a "
                StrSql += " left outer Join wo2_WorkOrderEnter b "
                StrSql += " on a.userid=b.wo2_userID and year(b.wo2_effdate)=year(a.starttime) and month(b.wo2_effdate)=month(a.starttime) and day(b.wo2_effdate)=day(a.starttime) "
                StrSql += " and b.wo2_NewCenter='" + Request("cc") + "' "
                StrSql += " left outer Join wo_cost_detail c "
                StrSql += " on a.userid=c.wocd_userid and year(c.wocd_date)=year(a.starttime) and month(c.wocd_date)=month(a.starttime) and day(c.wocd_date)=day(a.starttime) "
                StrSql += " and c.wocd_NewCC='" + Request("cc") + "' "
                StrSql += " where a.plantID='" + Request("pl") + "' and a.cc='" + Request("cc") + "' "
                'StrSql += " and year(a.starttime)='" + Request("yy") + "' and Month(a.starttime)='" + Request("mm") + "' "
                StrSql += " and a.starttime >'" + Request("date1") + "' "
                If Request("date2") <> "" Then
                    StrSql += " and a.starttime <'" + Request("date2") + "' "
                End If

                StrSql += " and a.usertype=394 and a.totalhr > 0"
                StrSql += " order by a.userno,a.starttime "

                'Response.Write(StrSql)
                'Exit Sub

                reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql)
                While (reader.Read())
                    If reader(6) = 0 And reader(7) = 0 Then
                        Str = Request("cc") & "~^"
                        Str &= reader(0).ToString() & "~^"
                        Str &= reader(1).ToString() & "~^"
                        Str &= reader(2).ToString() & "~^"
                        Str &= Format(reader(3), "yyyy-MM-dd") & "~^"
                        Str &= reader(3).ToString() & "~^"
                        Str &= reader(4).ToString() & "~^"
                        Str &= reader(5).ToString() & "~^"

                        PIMasteryRow(Str)
                    End If
                End While
                reader.Close()
            Else
                PIHeaderRow("无考勤有工单工时  导出日期:" & Today.ToString())

                Str = "60^<b>成本中心</b>~^50^<b>工号</b>~^70^<b>姓名</b>~^110^<b>比较日期</b>~^"
                PIMasteryRow(Str, True)

                reader = GetWoWithnotHr(CInt(Request("pl")), CInt(Request("cc")), Request("date1"), Request("date2"), "")

                'reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql)
                While (reader.Read())
                    If reader(3) = 0 Then
                        Str = Request("cc") & "~^"
                        Str &= reader(0).ToString() & "~^"
                        Str &= reader(1).ToString() & "~^"
                        'Str &= Format$(reader(2), "yyyy-MM-dd") & "~^"
                        Str &= reader(2).ToString() & "~^"
                        PIMasteryRow(Str)
                    End If
                End While
                reader.Close()
            End If


            'While (i < 100)
            '    PIMasteryRow("")
            '    i = i + 1
            'End While
            'Exit Sub
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=workshopcalendar.xls")
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
                        'If IsNumeric(cell.Text) Then
                        '    If cell.Text.Trim.Length > 6 Then
                        '        If cell.Text.Trim.IndexOf(".") = -1 Then
                        '            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                        '        End If
                        '    End If
                        '    cell.HorizontalAlign = HorizontalAlign.Right
                        'ElseIf IsDate(cell.Text) Then
                        '    If CDate(cell.Text) = CDate("1900-01-01") Then
                        '        cell.Text = ""
                        '    Else
                        '        cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd HH:mm")
                        '    End If
                        'End If
                        str = ""
                        row.Cells.Add(cell)
                        Exit For
                    Else
                        cell.Text = str.Substring(0, ind)
                        str = str.Substring(ind + 2)
                        'If IsNumeric(cell.Text) Then
                        '    If cell.Text.Trim.Length > 6 Then
                        '        If cell.Text.Trim.IndexOf(".") = -1 Then
                        '            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                        '        End If
                        '    End If
                        '    cell.HorizontalAlign = HorizontalAlign.Right
                        'ElseIf IsDate(cell.Text) Then
                        '    If CDate(cell.Text) = CDate("1900-01-01") Then
                        '        cell.Text = ""
                        '    Else
                        '        cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd HH:mm")
                        '    End If
                        'End If
                    End If
                    row.Cells.Add(cell)
                Next

                If i < 30 Then
                    For ind = i To 30
                        cell = New TableCell
                        cell.Text = ""
                        cell.Width = New Unit(60)
                        row.Cells.Add(cell)
                    Next
                End If
            End If

            exl.Rows.Add(row)
        End Sub
        Function GetWoWithnotHr(ByVal plantcode As Integer, ByVal workcenter As Integer, ByVal date1 As String, ByVal date2 As String, ByVal userNo As String) As SqlDataReader
            Dim param(5) As SqlParameter
            param(0) = New SqlParameter("@plantcode", plantcode)
            param(1) = New SqlParameter("@workcenter", workcenter)
            param(2) = New SqlParameter("@date1", date1)
            param(3) = New SqlParameter("@date2", date2)
            param(4) = New SqlParameter("@userNo", userNo)

            GetWoWithnotHr = SqlHelper.ExecuteReader(chk.dsnx, CommandType.StoredProcedure, "sp_wo2_selectWoWithnotHr", param)
        End Function

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

            If i < 30 Then
                For ind = i To 30
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
