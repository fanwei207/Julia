'!*******************************************************************************!
'* @@ NAME				:	overtimeSalaryAnalyseExcel.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for overtimeSalaryAnalyseExcel.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	May 11 2005
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

Partial Class overtimeSalaryAnalyseExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim Query As String
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

            Dim maxsalary As Decimal = 0
            Dim mixsalary As Decimal = 0
            Dim avgsalary As Decimal = 0
            Dim amountsalary As Decimal = 0

            Dim maxoverdays As Decimal = 0
            Dim mixoverdays As Decimal = 0
            Dim amountoverdays As Decimal = 0

            Dim people As Decimal = 0

            Query = " select v.extrasalarey,v.extratimes,d.departmentID,d.name,w.id,w.name as workshopName From OvertimesSalary v "
            Query &= " INNER join departments d on d.departmentID=v.departmentID "
            Query &= " INNER join Workshop w on w.id=v.workProcedureID "
            Query &= " where salarydate='" & Request("sate") & "' "
            Query &= " order by v.departmentID,v.workProcedureID "
            HeaderTableProc(Request("sate"))

         
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            Dim tableConst As New Table
            Dim depID As Integer = 0
            Dim wID As Integer = 0

            Dim totalpeople As Decimal = 0
            Dim totaloverdays As Decimal = 0
            Dim totaloversalary As Decimal = 0

            Dim deflag As Boolean = False
            Dim woflag As Boolean = False
            Dim wrow As Boolean = False
            Dim dname As String
            Dim wname As String
            Dim dnum As String

            Dim allpeople As Decimal = 0
            Dim overdays As Decimal = 0
            Dim oversalary As Decimal = 0
            While reader.Read
                If reader("departmentID") <> depID Then

                    If deflag = True Then
                        totalpeople = totalpeople + people
                        totaloverdays = totaloverdays + amountoverdays
                        totaloversalary = totaloversalary + amountsalary

                        If wrow = True Then
                            DataTableRow(1, dname, wname, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), amountoverdays.ToString(), dnum + 1, tableConst)
                            wrow = False
                        Else
                            DataTableRow(2, "", wname, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), amountoverdays.ToString(), 0, tableConst)
                        End If
                        DataTableRow(2, "", "合计", "", "", "", totaloversalary.ToString(), totalpeople.ToString(), "", "", totaloverdays.ToString(), 1, tableConst)

                        allpeople = allpeople + totalpeople
                        overdays = overdays + totaloverdays
                        oversalary = oversalary + totaloversalary
                    End If

                    dname = reader("name")
                    depID = reader("departmentID")
                    Query = " Select count(id) as num  From Workshop where departmentID='" & reader("departmentID") & "' and workshopID IS NULL group by departmentID "
                    dnum = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)

                    Dim DataTable As New Table
                    DataTable.CellSpacing = 0
                    DataTable.CellPadding = 2
                    DataTable.GridLines = GridLines.Both
                    'DataTable.BorderWidth = New Unit(0)
                    DataTable.BorderColor = Color.Black
                    DataTable.Width = New Unit(1100)

                    tableConst = DataTable

                    DataTableRow(0, "", "", "", "", "", "", "", "", "", "", 0, tableConst)
                    totalpeople = 0
                    totaloverdays = 0
                    totaloversalary = 0
                    amountsalary = 0
                    people = 0
                    amountoverdays = 0
                    woflag = False
                    wrow = True
                End If

                If reader("id") <> wID Then
                    totalpeople = totalpeople + people
                    totaloverdays = totaloverdays + amountoverdays
                    totaloversalary = totaloversalary + amountsalary
                    If woflag = True Then
                        If wrow = True Then
                            DataTableRow(1, reader("name"), wname, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), amountoverdays.ToString(), dnum + 1, tableConst)
                            wrow = False
                        Else
                            DataTableRow(2, "", wname, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), amountoverdays.ToString(), 0, tableConst)
                        End If
                    End If

                    wID = reader("id")
                    wname = reader("workshopName")

                    amountsalary = 0
                    people = 0
                    amountoverdays = 0

                    maxsalary = 0
                    mixsalary = 10000
                    maxoverdays = 0
                    mixoverdays = 10000
                    
                End If

                If maxsalary < reader("extrasalarey") Then
                    maxsalary = reader("extrasalarey")
                End If

                If mixsalary > reader("extrasalarey") Then
                    mixsalary = reader("extrasalarey")
                End If

                amountsalary = amountsalary + reader("extrasalarey")

                'For overtimedays
                If maxoverdays < reader("extratimes") Then
                    maxoverdays = reader("extratimes")
                End If

                If mixoverdays > reader("extratimes") Then
                    mixoverdays = reader("extratimes")
                End If

                amountoverdays = amountoverdays + reader("extratimes")

                people = people + 1

                woflag = True
                deflag = True
            End While

            totalpeople = totalpeople + people
            totaloverdays = totaloverdays + amountoverdays
            totaloversalary = totaloversalary + amountsalary


            allpeople = allpeople + totalpeople
            overdays = overdays + totaloverdays
            oversalary = oversalary + totaloversalary

            DataTableRow(2, "", wname, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), amountoverdays.ToString(), 0, tableConst)
            DataTableRow(2, "", "合计", "", "", "", totaloversalary.ToString(), totalpeople.ToString(), "", "", totaloverdays.ToString(), 1, tableConst)



            DataTableRow(3, "", "总计", "", "", "", oversalary, allpeople, "", "", overdays, 0, tableConst)
            reader.Close()

            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")


        End If
    End Sub

    Sub HeaderTableProc(ByVal str As String)
        Dim HeaderTable As New Table
        HeaderTable.CellSpacing = 0
        HeaderTable.CellPadding = 2
        HeaderTable.GridLines = GridLines.None
        HeaderTable.BorderWidth = New Unit(0)
        'HeaderTable.BorderColor = Color.Black
        HeaderTable.Width = New Unit(1100)

        row = New TableRow
        row.BackColor = Color.White

        row.HorizontalAlign = HorizontalAlign.Center
        row.VerticalAlign = VerticalAlign.Middle

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Text &= "<b><font size=4>" & str & "工资分析</font></b>"
        cell.Width = New Unit(1100)
        row.Cells.Add(cell)

        HeaderTable.Rows.Add(row)
        ReportTablesHolder.Controls.Add(HeaderTable)
    End Sub

    Sub DataTableRow(ByVal ty As Integer, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String _
           , ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String _
       , ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal num As Integer, ByVal tableTemp As Table)
        If ty = 0 Then


            row = New TableRow
            row.BackColor = Color.White
            row.Width = New Unit(1100)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>部门</b>"
            cell.Width = New Unit(120)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>工段名称</b>"
            cell.Width = New Unit(160)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>加班工资</b>"
            cell.Width = New Unit(420)
            cell.ColumnSpan = 4
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>人数</b>"
            cell.Width = New Unit(80)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>最大加班天</b>"
            cell.Width = New Unit(100)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>最小加班天</b>"
            cell.Width = New Unit(100)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总加班天</b>"
            cell.Width = New Unit(120)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            tableTemp.Rows.Add(row)

            row = New TableRow
            row.BackColor = Color.White
            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>最大</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>最小</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>平均值</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总计</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            tableTemp.Rows.Add(row)
            ReportTablesHolder.Controls.Add(tableTemp)
        Else
            If ty = 1 Then
                row = New TableRow
                row.BackColor = Color.White
                row.Width = New Unit(1100)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = str1
                cell.Width = New Unit(120)
                cell.RowSpan = num
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str2
                cell.Width = New Unit(160)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str3
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str4
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str5
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str6
                cell.Width = New Unit(120)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str7
                cell.Width = New Unit(80)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str8
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str9
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str10
                cell.Width = New Unit(120)
                row.Cells.Add(cell)
                tableTemp.Rows.Add(row)
            Else
                If ty = 2 Then
                    row = New TableRow
                    row.BackColor = Color.White

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str2
                    cell.Width = New Unit(160)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str3
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str4
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str5
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str6
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str7
                    cell.Width = New Unit(80)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str8
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str9
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str10
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    tableTemp.Rows.Add(row)

                    If num = 1 Then
                        ReportTablesHolder.Controls.Add(tableTemp)
                    End If
                Else
                    row = New TableRow
                    row.BackColor = Color.White


                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str2
                    cell.ColumnSpan = 2
                    cell.Width = New Unit(280)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str3
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str4
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str5
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str6
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str7
                    cell.Width = New Unit(80)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str8
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str9
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str10
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    tableTemp.Rows.Add(row)

                    ReportTablesHolder.Controls.Add(tableTemp)
                End If
            End If
        End If
    End Sub

End Class

End Namespace
