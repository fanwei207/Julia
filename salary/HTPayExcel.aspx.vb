Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class HTPayExcel
    Inherits System.Web.UI.Page

    Dim sqlStr As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Shared cellind As String
    Dim chk As New adamClass
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
        ind = str.IndexOf("^~^")
        If (ind > -1) Then
            Session("EXHeader") = str.Substring(ind + 1)
        End If

        cellind = ""

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

        Dim uid As String = "0"

        Dim i As Integer = 0
        sqlStr = Session("EXSQL")
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
        While (reader.Read())
            If uid <> reader(0).ToString() Then
                uid = reader(0).ToString()
                str = ""
                For i = reader.FieldCount() - total To reader.FieldCount() - 1
                    str = str & reader(i).ToString() & "~^"
                Next
                PIMasteryRow(str)
            End If
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
                If cell.Text.Trim.Length = 0 Then
                    cellind = cellind & "~" & i.ToString() & "^"
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
        Else
            For i = 0 To 200
                cell = New TableCell
                ind = str.IndexOf("~^")
                If (ind = -1) Then
                    If cellind.IndexOf("~" & i.ToString() & "^") > -1 Then
                        cell.Text = ""
                    Else
                        cell.Text = str
                    End If
                    If IsNumeric(cell.Text) Then
                        If cell.Text.Trim.Length > 8 Then
                            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                        End If
                        cell.HorizontalAlign = HorizontalAlign.Right
                    ElseIf IsDate(cell.Text) Then
                        If str.Length >= 8 Then
                            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy.MM.dd")
                        Else
                            cell.Text = str
                        End If
                    End If
                    str = ""
                    row.Cells.Add(cell)
                    Exit For
                Else
                    If cellind.IndexOf("~" & i.ToString() & "^") > -1 Then
                        cell.Text = ""
                    Else
                        cell.Text = str.Substring(0, ind)
                    End If
                    If IsNumeric(cell.Text) Then
                        If cell.Text.Trim.Length > 8 Then
                            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                        End If
                        cell.HorizontalAlign = HorizontalAlign.Right
                    ElseIf IsDate(cell.Text) Then
                        If str.Substring(0, ind).Length >= 8 Then
                            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy.MM.dd")
                        Else
                            cell.Text = str.Substring(0, ind)
                        End If
                    End If
                    str = str.Substring(ind + 2)
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
                If cell.Text.Trim.Length > 8 Then
                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                End If
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
