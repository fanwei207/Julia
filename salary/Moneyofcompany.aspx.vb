'!*******************************************************************************!
'* @@ NAME				:	Moneyofcompany.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for Moneyofcompany.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	September 1 2006
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Drawing
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class Moneyofcompany
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim strSql As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Dim chk As New adamClass

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then

            PIMasteryHeaderRow(Request("sy"))

            Dim departmentHashtable As New Hashtable



            Response.Clear()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312")
            Response.ContentType = "application/vnd.ms-excel"

            Response.Flush()
        End If
    End Sub

    Sub PIMasteryHeaderRow(ByVal str0 As String)
        row = New TableRow
        row.BackColor = Color.LightGray
        row.HorizontalAlign = HorizontalAlign.Right
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = "<b>" & str0.Trim() & "</b>"
        cell.Width = New Unit(80)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.VerticalAlign = VerticalAlign.Middle
        cell.RowSpan = 2
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>部门</b>"
        cell.Width = New Unit(120)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.VerticalAlign = VerticalAlign.Middle
        cell.RowSpan = 2
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>工段</b>"
        cell.Width = New Unit(120)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.VerticalAlign = VerticalAlign.Middle
        cell.RowSpan = 2
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>出勤天</b>"
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 3
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>完成天数</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.VerticalAlign = VerticalAlign.Middle
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>小时天</b>"
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 3
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>基本工资</b>"
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 3
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>加班工资</b>"
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 3
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>应发工资</b>"
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 3
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>人数</b>"
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 3
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>平均工资</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.VerticalAlign = VerticalAlign.Middle
        cell.RowSpan = 2
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>实发工资</b>"
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.ColumnSpan = 3
        row.Cells.Add(cell)

        exl.Rows.Add(row)

        'the second title
        row = New TableRow
        row.BackColor = Color.LightGray

        cell = New TableCell
        cell.Text = "<b>计时</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>计件</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>小计</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>计件</b>"
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        Dim i As Integer
        For i = 0 To 5
            cell = New TableCell
            cell.Text = "<b>计时</b>"
            cell.Width = New Unit(100)
            cell.HorizontalAlign = HorizontalAlign.Center
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>计件</b>"
            cell.Width = New Unit(100)
            cell.HorizontalAlign = HorizontalAlign.Center
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>小计</b>"
            cell.Width = New Unit(100)
            cell.HorizontalAlign = HorizontalAlign.Center
            row.Cells.Add(cell)
        Next

        exl.Rows.Add(row)
    End Sub
End Class

End Namespace
