Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class conn_exportExcel
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

        Dim Str As String = ""

        Str = "50^<b>申请人</b>~^<b>部门</b>~^200^<b>内容</b>~^60^<b>日期</b>~^480^<b>回签意见</b>~^60^<b>关闭日期</b>~^"
        PIMasteryRow(Str, True)

        Dim i As Integer
        sqlStr = "select conn_user_name,conn_dept,conn_content,conn_date,conn_closeddate,conn_mstr_id"
        sqlStr &= " from KnowDB.dbo.conn_mstr where conn_deleteddate is null"
        sqlStr &= " order by conn_date desc "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
        While (reader.Read())
            Str = ""
            For i = 0 To 3
                Str = Str & reader(i).ToString() & "~^"
            Next

            Dim reader2 As SqlDataReader
            Dim Str2 As String = ""
            Dim txt2 As String = ""

            Str2 = " Select conn_content,conn_user_name  " _
                   & " From KnowDB.dbo.conn_detail where conn_mstr_id='" & reader(5).ToString() & "' " _
                   & " order by conn_detail_id "
            reader2 = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, Str2)
            While (reader2.Read())
                txt2 = txt2 & "<b>" & reader2(1).ToString().Trim() & "</b> -- " + reader2(0).ToString().Trim() & "      "
            End While
            reader2.Close()
            Str = Str & txt2 & "~^"
            If IsDBNull(reader(4)) Then
                Str = Str & " ~^"
            Else
                Str = Str & reader(4).ToString() & "~^"
            End If

            PIMasteryRow(Str)
        End While
        reader.Close()

        While (i < 100)
            PIMasteryRow("")
            i = i + 1
        End While
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
