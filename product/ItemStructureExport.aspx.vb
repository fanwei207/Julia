Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class ItemStructureExport
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
            If Session("EXTitle") = Nothing Or Session("EXSQL") = Nothing Then
                Response.Redirect("exportExcelClose.aspx", True)
                Exit Sub
            End If

            Dim str As String = Session("EXHeader")
            Dim ind As Integer
            'Response.Write(Session("EXSQL") & "<br>")
            'Response.Write(Session("EXTitle"))
            'Exit Sub
            Dim total As Integer = 0
            str = Session("EXHeader")
            While (str.Length() > 0)
                ind = str.IndexOf("^")
                If (ind = -1) Then
                    PIHeaderRow(str)
                    str = ""
                Else
                    PIHeaderRow(str.Substring(0, ind))
                    str = str.Substring(ind + 1)
                End If
            End While

            str = Session("EXTitle")
            PIMasteryRow(str, True)
            While (str.Length() > 0)
                ind = str.IndexOf("~^")
                If (ind = -1) Then
                    total = total + 1
                    str = ""
                    Exit Sub
                End If
                total = total + 1
                str = str.Substring(ind + 2)
            End While

            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strPos As String = ""
            sqlStr = Session("EXSQL")
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr)

            Dim strStatus As String
            Dim strCode As String
            Dim strDesc As String
            Dim strQty As String
            Dim strNotes As String
            Dim strPosition As String
            Dim strType As String
            Dim strRepl As String

            Dim sqlStr2 As String
            Dim reader2 As SqlDataReader

            While (reader.Read())
                'status
                If reader(9).ToString() = "0" Then
                    strStatus = "使用"
                ElseIf reader(9).ToString() = "1" Then
                    strStatus = "试用"
                Else
                    strStatus = "停用"
                End If
                Dim tag As String = ""

                For j = 1 To reader(6)
                    tag = tag & "---"
                Next
                strCode = tag & " " & reader(0).ToString()
                strDesc = reader(1).ToString()
                strQty = reader(2).ToString()
                strNotes = reader(3).ToString()
                strPosition = reader(8).ToString()

                sqlStr2 = " Select i.code From product_replace pr Inner Join Items i On pr.itemID=i.id Where pr.prodStruID='" & reader(4).ToString() & "'"
                reader2 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr2)
                While reader2.Read()
                    strPos &= reader2(0) & ","
                End While
                reader2.Close()

                If strPos.Trim() <> "" Then
                    strRepl = strPos.Substring(0, Len(strPos.Trim()) - 1).Trim()
                Else
                    strRepl = ""
                End If
                strType = reader(10)

                strPos = ""
                str = strCode & "~^" & strStatus & "~^" & strType & "~^" & strQty & "~^" & strPosition & "~^" & strRepl & "~^" & strNotes & "~^" & strDesc & "~^"
                PIMasteryRow(str)
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
                            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy.MM.dd")
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
                            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy.MM.dd")
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
                If (i = 0) Then
                    i = 1
                Else
                    cell.ColumnSpan = 5
                    i = i + 10
                End If
                If IsNumeric(cell.Text) Then
                    'If cell.Text.Trim.Length > 8 Then
                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                    'End If
                ElseIf IsDate(cell.Text) Then
                    cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy.MM.dd")
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
