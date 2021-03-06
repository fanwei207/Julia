'!*******************************************************************************!
'* @@ NAME				:	qad_bom_Export.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for qad_bom_Export.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	October 29 2007
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class qad_bom_ExportExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim row As TableRow
    Dim cell As TableCell
    Public chk As New adamClass

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Dim Query As String
        Dim reader As SqlDataReader
        Dim param(2) As SqlParameter
        Dim code As String = ""
        Dim qad As String = ""
        If Request("code") <> Nothing Then
            If Len(Request("code").ToString) > 0 Then
                code = Server.UrlDecode(Request("code").ToString().Trim())
            End If
        End If

        If Request("qad") <> Nothing Then
            If Len(Request("qad").ToString) > 0 Then
                qad = Request("qad")
            End If
        End If


        Query = "Export_Stru_QAD"
        param(0) = New SqlParameter("@code", code)
        param(1) = New SqlParameter("@qad", qad)
        param(2) = New SqlParameter("@userID", Session("uid"))
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, Query, param)

        'Response.Write(code & "/" & qad.ToString)
        'Exit Sub
            Query = "select isnull(prod_code,''),isnull(qad,''),isnull(child_code,''),isnull(child_qad,''),isnull(numOfChild,0),isnull(posCode,''), isnull(replace,''), isnull(replace_code,''), isnull(notes,''), isnull(child_desc,'') From Export_Stru_Temp "
        Query &= "  where createdBy='" & Session("uid") & "' order by prod_code,child_code"

        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, Query)
        Dim pcode As String = ""
        Dim flag As Boolean = False
        Dim ok As Boolean = False

        While reader.Read
            If pcode <> reader(0) Then
                If flag = True Then
                        PIMasteryRow("", "", "", "", "", "", "", "", "", "", 2)
                        PIMasteryRow("", "", "", "", "", "", "", "", "", "", 2)
                End If
                pcode = reader(0)
                    PIMasteryRow("<b>父零件</b>", "<b>父部件号</b>", "<b>子零件</b>", "<b>子部件号</b>", "<b>数量</b>", "<b>位号</b>", "<b>替代零件</b>", "<b>替代部件号</b>", "<b>备注</b>", "<b>子件描述</b>", 1)
                ok = False
            End If

            If ok = False Then
                    PIMasteryRow(reader(0), reader(1), reader(2), reader(3), reader(4), reader(5), reader(6), reader(7), reader(8), reader(9), 0)
            Else
                    PIMasteryRow("", "", reader(2), reader(3), reader(4), reader(5), reader(6), reader(7), reader(8), reader(9), 0)
            End If

            flag = True
                'ok = True
        End While
        reader.Close()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
        Response.Flush()
    End Sub

        Sub PIMasteryRow(ByVal str As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal num As Integer)
            row = New TableRow
            row.BackColor = Color.White
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)
            If num = 2 Then
                cell = New TableCell
                cell.Text = "&nbsp;"
                cell.ColumnSpan = 10
                row.Cells.Add(cell)
            Else

                If num = 1 Then
                    row.BackColor = Color.LightGray
                Else
                    row.BackColor = Color.White
                End If

                cell = New TableCell
                cell.Text = str.Trim()
                cell.Width = New Unit(150)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str1.Trim()
                cell.Width = New Unit(200)
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str2
                cell.Width = New Unit(150)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str3.Trim()
                cell.Width = New Unit(200)
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str4.Trim()
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str5.Trim()
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str6.Trim()
                cell.Width = New Unit(100)
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str7.Trim()
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str8.Trim()
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.Text = str9.Trim()
                cell.Width = New Unit(100)
                row.Cells.Add(cell)
            End If

            exl.Rows.Add(row)
        End Sub

End Class

End Namespace



