'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-6-13
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   NA
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

Partial Class TranQtyAmountPrint
        Inherits BasePage
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Dim strsql As String
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
        Dim un As String = Nothing
        Dim uc As String = Nothing
        Dim str1 As String = ""
        Dim str2 As String = ""
        Dim str3 As String = ""
        Dim str4 As String = ""
        Dim str5 As String = ""
        Dim str6 As String = ""
        Dim str7 As String = ""
        Dim str8 As String = ""
        Dim str9 As String = ""
        Dim str10 As String = ""
        Dim str11 As String = ""
        Dim str12 As String = ""
        Dim str13 As String = ""
        Dim str14 As String = ""
        Dim str15 As String = ""
        Dim str16 As String = ""
        Dim str17 As String = ""
        Dim str18 As String = ""
        Dim str19 As String = ""
        Dim str20 As String = ""
        Dim str21 As String = ""
        Dim str22 As String = ""
        Dim str23 As String = ""
        Dim str24 As String = ""
        Dim str25 As String = ""
        Dim str26 As String = ""
        Dim str27 As String = ""
        Dim str28 As String = ""
        Dim str29 As String = ""
        Dim str30 As String = ""
        Dim str31 As String = ""

        PIMasteryRow("日期:", Request("year") & "年" & Request("month") & "月", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
        PIMasteryRow("工号", "姓名", "1日", "2日", "3日", "4日", "5日", "6日", "7日", "8日", "9日", "10日", "11日", "12日", "13日", "14日", "15日", "16日", "17日", "18日", "19日", "20日", "21日", "22日", "23日", "24日", "25日", "26日", "27日", "28日", "29日", "30日", "31日")

        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Session("EXSQL"))
        While reader.Read()
            If un <> reader(1) Then
                If un <> Nothing Then
                    PIMasteryRow(uc, un, str1, str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16, str17, str18, str19, str20, str21, str22, str23, str24, str25, str16, str27, str28, str29, str30, str31)
                    str1 = ""
                    str2 = ""
                    str3 = ""
                    str4 = ""
                    str5 = ""
                    str6 = ""
                    str7 = ""
                    str8 = ""
                    str9 = ""
                    str10 = ""
                    str11 = ""
                    str12 = ""
                    str13 = ""
                    str14 = ""
                    str15 = ""
                    str16 = ""
                    str17 = ""
                    str18 = ""
                    str19 = ""
                    str20 = ""
                    str21 = ""
                    str22 = ""
                    str23 = ""
                    str24 = ""
                    str25 = ""
                    str26 = ""
                    str27 = ""
                    str28 = ""
                    str29 = ""
                    str30 = ""
                    str31 = ""
                End If
            End If
            un = reader(1)
            uc = reader(2)
            Select Case CInt(CDate(reader(0)).Day)
                Case 1
                    str1 = reader(3)
                Case 2
                    str2 = reader(3)
                Case 3
                    str3 = reader(3)
                Case 4
                    str4 = reader(3)
                Case 5
                    str5 = reader(3)
                Case 6
                    str6 = reader(3)
                Case 7
                    str7 = reader(3)
                Case 8
                    str8 = reader(3)
                Case 9
                    str9 = reader(3)
                Case 10
                    str10 = reader(3)
                Case 11
                    str11 = reader(3)
                Case 12
                    str12 = reader(3)
                Case 13
                    str13 = reader(3)
                Case 14
                    str14 = reader(3)
                Case 15
                    str15 = reader(3)
                Case 16
                    str16 = reader(3)
                Case 17
                    str17 = reader(3)
                Case 18
                    str18 = reader(3)
                Case 19
                    str19 = reader(3)
                Case 20
                    str20 = reader(3)
                Case 21
                    str21 = reader(3)
                Case 22
                    str22 = reader(3)
                Case 23
                    str23 = reader(3)
                Case 24
                    str24 = reader(3)
                Case 25
                    str25 = reader(3)
                Case 26
                    str26 = reader(3)
                Case 27
                    str27 = reader(3)
                Case 28
                    str28 = reader(3)
                Case 29
                    str29 = reader(3)
                Case 30
                    str30 = reader(3)
                Case 31
                    str31 = reader(3)
            End Select
            'Else
            'Select Case CInt(CDate(reader(0)).Day)
            '    Case 1
            '        str1 = reader(3)
            '    Case 2
            '        str2 = reader(3)
            '    Case 3
            '        str3 = reader(3)
            '    Case 4
            '        str4 = reader(3)
            '    Case 5
            '        str5 = reader(3)
            '    Case 6
            '        str6 = reader(3)
            '    Case 7
            '        str7 = reader(3)
            '    Case 8
            '        str8 = reader(3)
            '    Case 9
            '        str9 = reader(3)
            '    Case 10
            '        str10 = reader(3)
            '    Case 11
            '        str11 = reader(3)
            '    Case 12
            '        str12 = reader(3)
            '    Case 13
            '        str13 = reader(3)
            '    Case 14
            '        str14 = reader(3)
            '    Case 15
            '        str15 = reader(3)
            '    Case 16
            '        str16 = reader(3)
            '    Case 17
            '        str17 = reader(3)
            '    Case 18
            '        str18 = reader(3)
            '    Case 19
            '        str19 = reader(3)
            '    Case 20
            '        str20 = reader(3)
            '    Case 21
            '        str21 = reader(3)
            '    Case 22
            '        str22 = reader(3)
            '    Case 23
            '        str23 = reader(3)
            '    Case 24
            '        str24 = reader(3)
            '    Case 25
            '        str25 = reader(3)
            '    Case 26
            '        str26 = reader(3)
            '    Case 27
            '        str27 = reader(3)
            '    Case 28
            '        str28 = reader(3)
            '    Case 29
            '        str29 = reader(3)
            '    Case 30
            '        str30 = reader(3)
            '    Case 31
            '        str31 = reader(3)
            'End Select
            'End If
        End While
        reader.Close()
        PIMasteryRow(uc, un, str1, str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16, str17, str18, str19, str20, str21, str22, str23, str24, str25, str16, str27, str28, str29, str30, str31)
        PIMasteryRow("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
        Response.Clear()
        Response.Buffer = True
        Response.Charset = "UTF-8"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=Summary.xls")
        Response.Flush()
    End Sub

    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, _
                     ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, _
                     ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal str15 As String, ByVal str16 As String, ByVal str17 As String, _
                     ByVal str18 As String, ByVal str19 As String, ByVal str20 As String, ByVal str21 As String, ByVal str22 As String, ByVal str23 As String, _
                     ByVal str24 As String, ByVal str25 As String, ByVal str26 As String, ByVal str27 As String, ByVal str28 As String, ByVal str29 As String, _
                     ByVal str30 As String, ByVal str31 As String, ByVal str32 As String)
        row = New TableRow
        row.HorizontalAlign = HorizontalAlign.Left
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        If (str0 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str0.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str1 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str1.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        cell.Wrap = True
        row.Cells.Add(cell)

        cell = New TableCell
        If (str2 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str2.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str3 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str3.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str4 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str4.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str5 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str5.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str6 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str6.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str7 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str7.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str8 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str8.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str9 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str9.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str10 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str10.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str11 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str11.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str12 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str12.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str13 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str13.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str14 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str14.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str15 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str15.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str16 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str16.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str17 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str17.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str18 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str18.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str19 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str19.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str20 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str20.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str21 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str21.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str22 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str22.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str23 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str23.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str24 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str24.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str25 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str25.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str26 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str26.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str27 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str27.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str28 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str28.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str29 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str29.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str30 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str30.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str31 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str31.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        cell = New TableCell
        If (str32 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str32.Trim()
        End If
        cell.Width = New Unit(50)
        cell.HorizontalAlign = HorizontalAlign.Right
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub

End Class

End Namespace
