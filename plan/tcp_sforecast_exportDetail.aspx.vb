Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc

    Partial Class tcp_sforecast_exportDetail
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

            PIHeaderRow("TCP预测--" & Format(Now, "yyyy-MM-dd"))
            Dim ww As Integer = CDate(Format(Today, "yyyy-01-01")).DayOfWeek()

            Str = "100^<b>零件号</b>~^230^<b>描述</b>~^<b>预测</b>~^100^<b>父零件号</b>~^230^<b>描述</b>~^<b>预测</b>~^100^<b>客户零件</b>~^230^<b>描述</b>~^<b>JDE更新日期</b>~^"
            PIMasteryRow(Str, True)

            sqlStr = "Select ps.tcp_sfc_qchild,ps.tcp_sfc_qchilddesc,ps.tcp_sfc_qchildqty,ps.tcp_sfc_qitem,ps.tcp_sfc_qdesc,ps.tcp_sfc_qty,ps.tcp_sfc_jitem,ps.tcp_sfc_jdesc,createddate "
            sqlStr &= " from qadplan.dbo.tcp_sfc ps Inner Join qadplan.dbo.tcp_anal an on an.tcpa_code=ps.tcp_sfc_qchild and an.tcpa_userid='" & Session("uID") & "'"
            sqlStr &= " where ps.tcp_sfc_qchild is not null and isnull(tcp_sfc_qchildqty,0) > 0 and (isnull(tcp_sfc_qchild,'')<>'' or isnull(tcp_sfc_qchilddesc,'')<>'' or isnull(tcp_sfc_qchildqty,0)<>0) "
            sqlStr &= " order by ps.tcp_sfc_qchild,ps.tcp_sfc_qty desc "

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                Str = reader(0).ToString() & "~^"
                Str &= reader(1).ToString() & "~^"
                Str &= Format(reader(2), "##0.##") & "~^"
                Str &= reader(3).ToString() & "~^"
                Str &= reader(4).ToString() & "~^"
                Str &= Format(reader(5), "##0.##") & "~^"
                Str &= reader(6).ToString() & "~^"
                Str &= reader(7).ToString() & "~^"

                If IsDBNull(reader(8)) Then
                    Str &= "~^"
                Else
                    Str &= Format(reader(8), "yyyy-MM-dd") & "~^"
                End If
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
