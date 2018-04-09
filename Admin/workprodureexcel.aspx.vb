Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class workprodureexcel
        Inherits BasePage
    Public chk As New adamClass

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim sqlStr As String
    Dim reader As SqlDataReader


    Dim row As TableRow
    Dim cell As TableCell

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        PIMasteryRow("序号", "工序名", "指标", "计件单价", "计时单价")
        sqlStr = " Select id,name,guideLine,unitPrice,lowestDailyWage From  workProcedure Where  (lowestDailyWage = 0) AND (unitPrice = 0)"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
        While reader.Read
            PIMasteryRow(reader(0), reader(1), reader(2), reader(3), reader(4))
        End While
        reader.Close()
        Response.ContentType = "application/vnd.ms-excel"
    End Sub
    Sub PIMasteryRow(ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String)
        row = New TableRow
        row.HorizontalAlign = HorizontalAlign.Right
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = str1.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str2.Trim()
        cell.Width = New Unit(140)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str3.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str4.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str5.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub
End Class

End Namespace
