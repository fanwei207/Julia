'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-03-12
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Export Search By Date To Excel

Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class SearchByDatePrint
        Inherits BasePage
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
        Dim ind As Integer
        Dim str As String = ""
        Dim total As Decimal = 0.0
        Dim i As Integer = 0
        Dim code As String = ""
        Dim strI As String = "0"
        Dim strO As String = "0"
        Dim strDR As String = "0"
        Dim strRS As String = "0"
        Dim strPM As String = "0"
        Dim strPMO As String = "0"
        Dim strDGI As String = "0"
        Dim strDGO As String = "0"
        Dim strDGRI As String = "0"
        Dim strINIT As String = "0"
        Dim strADJ As String = "0"
        Dim strType As String = ""
        Dim strWarehouse As String = ""
        Dim warehouse As String = ""
        Dim strUnit As String = ""
        Dim strStatus As String = ""

        If Session("EXTitle") = Nothing Or Session("EXSQL") = Nothing Then
            Response.Redirect("exportExcelClose.aspx", True)
            Exit Sub
        End If

        'print Header
        str = Session("EXHeader1")
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

        'print Title
        str = Session("EXTitle")
        PIMasteryRow(str, True)
        While (str.Length() > 0)
            ind = str.IndexOf("~^")
            If (ind = -1) Then
                str = ""
                Exit Sub
            End If
            str = str.Substring(ind + 2)
        End While

        'print contents
        sqlStr = Session("EXSQL")
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
        While (reader.Read())
            If code <> reader(0) Then
                If code <> "" Then
                    If Math.Abs(CDbl(strI)) < Math.Abs(CDbl(strDGI)) Then
                        strI = "0"
                    End If
                    If Math.Abs(CDbl(strO)) < Math.Abs(CDbl(strDGO)) Then
                        strO = "0"
                    End If
                    If Math.Abs(CDbl(strDR)) < Math.Abs(CDbl(strDGRI)) Then
                        strDR = "0"
                    End If
                    str &= strINIT & "~^" & strI & "~^" & Math.Abs(CDbl(strO)) & "~^" & strDR & "~^" & Math.Abs(CDbl(strRS)) & "~^" & Math.Abs(CDbl(strDGI)) & "~^" & strDGO & "~^" & Math.Abs(CDbl(strDGRI)) & "~^" & strPM & "~^" & strPMO & "~^" & strADJ & "~^" & Format(total, "#,##0.00000") & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                    PIMasteryRow(str)
                    total = 0
                    strI = "0"
                    strO = "0"
                    strDR = "0"
                    strRS = "0"
                    strPM = "0"
                    strPMO = "0"
                    strDGI = "0"
                    strDGO = "0"
                    strDGRI = "0"
                    strINIT = "0"
                    strADJ = "0"
                    strType = ""
                    strWarehouse = ""
                    strUnit = ""
                End If
                str = reader(0) & "~^" & reader(1) & "~^" & reader(8) & "~^"
                code = reader(0)
                strWarehouse = reader(5)
                strType = reader(4)
                warehouse = reader(5)
                strUnit = reader(6)
            Else
                If warehouse <> reader(5) Then
                    If Math.Abs(CDbl(strI)) < Math.Abs(CDbl(strDGI)) Then
                        strI = "0"
                    End If
                    If Math.Abs(CDbl(strO)) < Math.Abs(CDbl(strDGO)) Then
                        strO = "0"
                    End If
                    If Math.Abs(CDbl(strDR)) < Math.Abs(CDbl(strDGRI)) Then
                        strDR = "0"
                    End If
                    str &= strINIT & "~^" & strI & "~^" & Math.Abs(CDbl(strO)) & "~^" & strDR & "~^" & Math.Abs(CDbl(strRS)) & "~^" & Math.Abs(CDbl(strDGI)) & "~^" & strDGO & "~^" & Math.Abs(CDbl(strDGRI)) & "~^" & strPM & "~^" & strPMO & "~^" & strADJ & "~^" & Format(total, "#,##0.00000") & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                    PIMasteryRow(str)
                    total = 0
                    strI = "0"
                    strO = "0"
                    strDR = "0"
                    strRS = "0"
                    strPM = "0"
                    strPMO = "0"
                    strDGI = "0"
                    strDGO = "0"
                    strDGRI = "0"
                    strINIT = "0"
                    strADJ = "0"
                    strType = ""
                    strWarehouse = ""
                    strUnit = ""
                End If
                str = reader(0) & "~^" & reader(1) & "~^" & reader(8) & "~^"
                code = reader(0)
                strWarehouse = reader(5)
                strType = reader(4)
                warehouse = reader(5)
                strUnit = reader(6)
            End If
            If Request("type") = Nothing Or Request("type") = "" Then
                Select Case reader(2)
                    Case "I"
                        strI = reader(3)
                        total = total + CDbl(reader(3))
                    Case "O"
                        strO = reader(3)
                        total = total + CDbl(reader(3))
                    Case "DR"
                        strDR = reader(3)
                        total = total + CDbl(reader(3))
                    Case "RS"
                        strRS = reader(3)
                        total = total + CDbl(reader(3))
                    Case "PM"
                        strPM = reader(3)
                        total = total + CDbl(reader(3))
                    Case "PMO"
                        strPMO = reader(3)
                        total = total + CDbl(reader(3))
                    Case "INIT"
                        strINIT = reader(3)
                        total = total + CDbl(reader(3))
                    Case "ADJ"
                        strADJ = reader(3)
                        total = total + CDbl(reader(3))
                    Case "DGO"
                        If CInt(reader(7)) = CInt(Session("orgID")) Then
                            strDGI = reader(3)
                        Else
                            strDGO = reader(3)
                        End If
                        total = total - Math.Abs(CDbl(reader(3)))
                    Case "DGI"
                        If CInt(reader(7)) = CInt(Session("orgID")) Then
                            strDGO = reader(3)
                        Else
                            strDGI = reader(3)
                        End If
                        total = total - Math.Abs(CDbl(reader(3)))
                    Case "DGRO"
                        strDGRI = reader(3)
                        total = total - CDbl(reader(3))
                    Case "DGRI"
                        strDGRI = reader(3)
                        total = total + CDbl(reader(3))
                End Select

            Else
                Select Case Request("type")
                    Case "I"
                        str &= "0~^" & reader(3) & "~^0~^0~^0~^0~^0~^0~^0~^0~^0~^" & reader(3) & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                    Case "O"
                        str &= "0~^0~^" & reader(3) & "~^0~^0~^0~^0~^0~^0~^0~^0~^" & reader(3) & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                    Case "DR"
                        str &= "0~^0~^0~^" & reader(3) & "~^0~^0~^0~^0~^0~^0~^0~^" & reader(3) & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                    Case "RS"
                        str &= "0~^0~^0~^0~^" & reader(3) & "~^0~^0~^0~^0~^0~^0~^" & reader(3) & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                    Case "PM"
                        str &= "0~^0~^0~^0~^0~^0~^0~^" & reader(3) & "~^0~^0~^0~^" & reader(3) & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                    Case "PMO"
                        str &= "0~^0~^0~^0~^0~^0~^0~^0~^" & reader(3) & "~^0~^0~^" & reader(3) & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                    Case "INIT"
                        str &= reader(3) & "0~^0~^0~^0~^0~^0~^0~^0~^0~^0~^" & reader(3) & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                    Case "ADJ"
                        str &= "0~^0~^0~^0~^0~^0~^0~^0~^0~^0~^" & reader(3) & "~^" & reader(3) & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
                End Select
            End If

        End While
        If Math.Abs(CDbl(strI)) < Math.Abs(CDbl(strDGI)) Then
            strI = "0"
        End If
        If Math.Abs(CDbl(strO)) < Math.Abs(CDbl(strDGO)) Then
            strO = "0"
        End If
        If Math.Abs(CDbl(strDR)) < Math.Abs(CDbl(strDGRI)) Then
            strDR = "0"
        End If
        str &= strINIT & "~^" & strI & "~^" & Math.Abs(CDbl(strO)) & "~^" & strDR & "~^" & Math.Abs(CDbl(strRS)) & "~^" & Math.Abs(CDbl(strDGI)) & "~^" & strDGO & "~^" & Math.Abs(CDbl(strDGRI)) & "~^" & strPM & "~^" & strPMO & "~^" & strADJ & "~^" & Format(total, "#,##0.00000") & "~^" & strType & "~^" & strWarehouse & "~^" & strUnit & "~^"
        PIMasteryRow(str)
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
        Session("EXHeader1") = Nothing
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
            cell.ColumnSpan = 5
            i = i + 10
            If IsNumeric(cell.Text) Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            ElseIf IsDate(cell.Text) Then
                If CDate(cell.Text) = CDate("1900-01-01") Then
                    cell.Text = ""
                Else
                    cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy.MM.dd")
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
                        If CDate(cell.Text) = CDate("1900-01-01") Then
                            cell.Text = ""
                        Else
                            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy.MM.dd")
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
                            cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy.MM.dd")
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

End Class

End Namespace
