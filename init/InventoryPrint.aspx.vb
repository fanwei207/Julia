'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-12-26
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Print Inventory Report
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace tcpc

Partial Class InventoryPrint
        Inherits System.Web.UI.Page 
    Public chk As New adamClass
    Dim strsql As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    'Protected WithEvents ltlAlert As Literal

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
        If Not IsPostBack Then
            filltable()
        End If
    End Sub

    Sub filltable()
        Dim _company As String = Request("company")
        Dim _code As String = Server.UrlDecode(Request("code"))
        Dim _card As String = Server.UrlDecode(Request("card"))
        Dim _status As String = Nothing
        Dim _loc As String = Nothing
        Dim _no As Integer = 1
        Dim id As Integer = 0
        Dim tb As Table
        If Request("no") <> Nothing Then
            If CInt(Request("no")) > 0 Then
                _no = CInt(Request("no"))
            End If
        End If
        id = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, "Select id From Inventory Where Upper(card)=N'" & UCase(Server.UrlDecode(Request("card"))) & "'")
        strsql = "Select Top " & _no & " company, code, card, Replace(loc,N'仓库',''), Isnull(status,'--') From Inventory Where company='" & Request("company") & "'"
        If Request("code") <> Nothing Then
            strsql &= " And code=N'" & Server.UrlDecode(Request("code")) & "'"
        End If
        If Request("card") <> Nothing Then
            strsql &= " And id>=" & id
            'strsql &= " And Upper(card)>=N'" & UCase(Server.UrlDecode(Request("card"))) & "'"
        End If
        strsql &= " Order By loc, id "
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
        Dim flag As Boolean = False
        While reader.Read()
            If flag = True Then
                Dim lbl1 As New Label
                lbl1.Text = "<p Style = 'page-break-before:always;'></p>"
                PlaceHolder1.Controls.Add(lbl1)
            End If
            tb = New Table
            tb.CellSpacing = 0
            tb.CellPadding = 2
            tb.GridLines = GridLines.None
            tb.BorderWidth = New Unit(0)
            tb.Style.Add("MARGIN-TOP", "0")
            tb.Width = New Unit(750)
            If reader(4) = "--" Then
                fillRow(tb, "", "<font size='5pt'>" & _company.Trim() & "</font>", "<font size='3pt'>" & reader(1) & "</font>", "<font size='3pt'>" & reader(3) & "</font>")
            Else
                fillRow(tb, "", "<font size='5pt'>" & _company.Trim() & "</font>", "<font size='3pt'>" & reader(1) & "状态" & reader(4) & "</font>", "<font size='3pt'>" & reader(3) & "</font>")
            End If
            fillRow(tb, "", "", "", "")
            fillRow(tb, "", "", "", "<font size='3pt'>" & reader(2) & "</font>")
            PlaceHolder1.Controls.Add(tb)
            flag = True
        End While
        reader.Close()
    End Sub

    Function fillRow(ByVal tb As Table, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String)
        row = New TableRow
        row.BackColor = Color.White
        row.HorizontalAlign = HorizontalAlign.Left
        row.VerticalAlign = VerticalAlign.Top
        row.BorderWidth = New Unit(0)
        row.Width = New Unit(800)

        cell = New TableCell
        If str1 = "" Then
            cell.Text = "&nbsp;"
        Else
            cell.Text = str1
        End If
        cell.Width = New Unit(140)
        row.Cells.Add(cell)

        cell = New TableCell
        If str2 = "" Then
            cell.Text = "&nbsp;"
        Else
            cell.Text = str2
        End If
        cell.Width = New Unit(60)
        row.Cells.Add(cell)

        cell = New TableCell
        If str3 = "" Then
            cell.Text = "&nbsp;"
        Else
            cell.Text = str3
        End If
        cell.Width = New Unit(400)
        cell.VerticalAlign = VerticalAlign.Middle
        row.Cells.Add(cell)

        cell = New TableCell
        If str4 = "" Then
            cell.Text = "&nbsp;"
        Else
            cell.Text = str4
        End If
        cell.Width = New Unit(200)
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Bottom
        row.Cells.Add(cell)

        tb.Rows.Add(row)
    End Function
End Class

End Namespace
