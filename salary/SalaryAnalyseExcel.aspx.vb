'!*******************************************************************************!
'* @@ NAME				:	SalaryAnalyseExcel.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for SalaryAnalyseExcel.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	April 26 2005
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

Partial Class SalaryAnalyseExcel
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

            Dim maxoverpay As Decimal = 0
            Dim mixoverpay As Decimal = 0
            Dim amountoverpay As Decimal = 0

            Dim maxs As Decimal = 0
            Dim mins As Decimal = 1000000
            Dim avgs As Decimal = 0

            Dim maxd As Decimal = 0
            Dim mind As Decimal = 1000000
            Dim avgd As Decimal = 0
            Dim totald As Decimal = 0

            Dim maxp As Decimal = 0
            Dim minp As Decimal = 1000000
            Dim avgp As Decimal = 0
            Dim totalp As Decimal = 0

            Dim people As Decimal = 0

                'Query = " Select  p.basicsalary+p.superfluitysalary+p.saturdaysalary+p.nightallowance as salary,p.overdays,p.oversalary,d.departmentID,d.name,w.id From piecesalary p  "
                'Query &= " INNER join departments d on d.departmentID=p.departmentID "
                'Query &= " INNER join Workshop w on w.id=p.workshopID and w.workshopID is null "
                'Query &= " Where year(p.salaryDate)= '" & Request("year") & "' and month(p.salaryDate)= '" & Request("sate") & "' "

                'Query &= " order by d.departmentID,w.ID "

                Query = "SELECT h.hr_Salary_workpay AS salary,h.hr_Salary_duereward AS overdays,h.hr_Salary_over AS oversalary,d.departmentID,d.name,w.id "
                Query &= " FROM hr_fin_mstr h "
                'Query &= " FROM hr_Salary_mstr h"
                Query &= " INNER JOIN departments d on d.departmentID=h.hr_Salary_departmentID "
                Query &= " INNER JOIN Workshop w on w.id=h.hr_Salary_workShopID  "
                Query &= " WHERE YEAR(h.hr_Salary_salaryDate) ='" & Request("year") & "' AND MONTH(h.hr_Salary_salaryDate)= '" & Request("sate") & "' "
                Query &= "  AND h.hr_Salary_workpay > 1000  AND h.hr_Salary_Exchange =0 "
                Query &= " ORDER BY d.departmentID,w.ID "

            HeaderTableProc(Request("year"), Request("sate"))

            'Response.Write(Query)
            'Exit Sub

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            Dim depID As Integer = 0
            Dim wID As Integer = 0
            Dim ds As DataSet
            Dim workshop As New Hashtable
            Dim i As Integer
            Dim number As Integer
            'Dim numtotal As Integer
            Dim totalpiece As Decimal = 0
            Dim totalpeople As Decimal = 0
            Dim totaloverdays As Decimal = 0
            Dim totaloversalary As Decimal = 0

            Dim deflag As Boolean = False
            Dim woflag As Boolean = False
            Dim wrow As Boolean = False
            Dim dnum As Integer
            Dim dname As String

            Dim piece As Decimal = 0
            Dim allpeople As Decimal = 0
            Dim overdays As Decimal = 0
            Dim oversalary As Decimal = 0

            Dim tableConst As New Table
            While reader.Read
               
                If reader("departmentID") <> depID Then

                    depID = reader("departmentID")

                    If deflag = True Then
                        ' last workshop in department 
                        totalpiece = totalpiece + amountsalary
                        totalpeople = totalpeople + people
                        totaloverdays = totaloverdays + amountoverdays
                        totaloversalary = totaloversalary + amountoverpay

                        If maxs < maxsalary Then
                            maxs = maxsalary
                        End If
                        If mins > mixsalary Then
                            mins = mixsalary
                        End If

                        If maxd < maxoverdays Then
                            maxd = maxoverdays
                        End If
                        If mind > mixoverdays Then
                            mind = mixoverdays
                        End If
                        'totald = totald + amountoverdays

                        If maxp < maxoverpay Then
                            maxp = maxoverpay
                        End If
                        If minp > mixoverpay Then
                            minp = mixoverpay
                        End If

                        If wrow = True Then
                                DataTableRow(1, dname, workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), Math.Round(amountoverdays / people, 2).ToString(), amountoverdays.ToString(), maxoverpay.ToString(), mixoverpay.ToString(), amountoverpay.ToString(), dnum, tableConst)
                            wrow = False
                        Else
                                DataTableRow(2, "", workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), Math.Round(amountoverdays / people, 2).ToString(), amountoverdays.ToString(), maxoverpay.ToString(), mixoverpay.ToString(), amountoverpay.ToString(), 0, tableConst)
                        End If
                            DataTableRow(2, "", "", "", "合计", "", totalpiece, totalpeople, "", "", "", totaloverdays, "", "", totaloversalary, 0, tableConst)
                            DataTableRow(2, "", "", maxs.ToString(), mins.ToString(), Math.Round(totalpiece / totalpeople, 2).ToString(), "", "", maxd.ToString(), mind.ToString(), Math.Round(totaloverdays / totalpeople, 2).ToString(), "", maxp, minp, "", 1, tableConst)
                        ' all deparetments's amount
                        piece = piece + totalpiece
                        allpeople = allpeople + totalpeople
                        overdays = overdays + totaloverdays
                        oversalary = oversalary + totaloversalary
                    End If
                    dname = reader("name")
                    workshop.Clear()
                    number = 0

                    ' how many workshops in the department 
                    Query = " Select id,name From Workshop where departmentID='" & reader("departmentID") & "' and workshopID IS NULL order by ID "
                    ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            workshop.Add(ds.Tables(0).Rows(i).Item(0), ds.Tables(0).Rows(i).Item(1))
                        Next
                        dnum = ds.Tables(0).Rows.Count + 2
                    End If

                    Dim DataTable As New Table
                    DataTable.CellSpacing = 0
                    DataTable.CellPadding = 2
                    DataTable.GridLines = GridLines.Both
                    'DataTable.BorderWidth = New Unit(0)
                    DataTable.BorderColor = Color.Black
                    DataTable.Width = New Unit(1400)

                    tableConst = DataTable

                        DataTableRow(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, tableConst)


                    ds.Reset()
                    totalpiece = 0
                    totalpeople = 0
                    totaloverdays = 0
                    totaloversalary = 0
                    amountsalary = 0
                    people = 0
                    amountoverdays = 0
                    amountoverpay = 0
                    woflag = False
                    wrow = True

                    maxs = 0
                    mins = 1000000
                    maxd = 0
                    mind = 1000000
                    totald = 0
                    maxsalary = 0
                    mixsalary = 1000000
                    maxoverdays = 0
                    mixoverdays = 1000000
                    maxoverpay = 0
                    mixoverpay = 1000000

                End If

                If reader("id") <> wID Then
                    'Add the total  in same department
                    totalpiece = totalpiece + amountsalary
                    totalpeople = totalpeople + people
                    totaloverdays = totaloverdays + amountoverdays
                    totaloversalary = totaloversalary + amountoverpay


                    If woflag = True Then
                        If wrow = True Then
                                DataTableRow(1, reader("name"), workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), Math.Round(amountoverdays / people, 2).ToString(), amountoverdays.ToString(), maxoverpay.ToString(), mixoverpay.ToString(), amountoverpay.ToString(), dnum, tableConst)
                            wrow = False
                        Else
                                DataTableRow(2, "", workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), Math.Round(amountoverdays / people, 2).ToString(), amountoverdays.ToString(), maxoverpay.ToString(), mixoverpay.ToString(), amountoverpay.ToString(), 0, tableConst)
                        End If
                    End If

                    wID = reader("id")

                    If maxs < maxsalary Then
                        maxs = maxsalary
                    End If
                    If mins > mixsalary Then
                        mins = mixsalary
                    End If

                    If maxd < maxoverdays Then
                        maxd = maxoverdays
                    End If
                    If mind > mixoverdays Then
                        mind = mixoverdays
                    End If
                    'totald = totald + amountoverdays
                    If maxp < maxoverpay Then
                        maxp = maxoverpay
                    End If
                    If minp > mixoverpay Then
                        minp = mixoverpay
                    End If

                    amountsalary = 0
                    people = 0
                    amountoverdays = 0
                    amountoverpay = 0
                    maxsalary = 0
                    mixsalary = 1000000
                    maxoverdays = 0
                    mixoverdays = 1000000
                    maxoverpay = 0
                    mixoverpay = 1000000
                End If

                'For pieceSalary compared
                If maxsalary < reader("salary") Then
                    maxsalary = reader("salary")
                End If

                If mixsalary > reader("salary") Then
                    mixsalary = reader("salary")
                End If

                amountsalary = amountsalary + reader("salary")

                'For overtimedays
                If maxoverdays < reader("overdays") Then
                    maxoverdays = reader("overdays")
                End If

                If mixoverdays > reader("overdays") Then
                    mixoverdays = reader("overdays")
                End If

                amountoverdays = amountoverdays + reader("overdays")

                'For overtime salary
                If maxoverpay < reader("oversalary") Then
                    maxoverpay = reader("oversalary")
                End If

                If mixoverpay > reader("oversalary") Then
                    mixoverpay = reader("oversalary")
                End If

                amountoverpay = amountoverpay + reader("oversalary")

                people = people + 1

                woflag = True
                deflag = True

            End While

                If deflag = True Then
                    totalpiece = totalpiece + amountsalary
                    totalpeople = totalpeople + people
                    totaloverdays = totaloverdays + amountoverdays
                    totaloversalary = totaloversalary + amountoverpay

                    piece = piece + totalpiece
                    allpeople = allpeople + totalpeople
                    overdays = overdays + totaloverdays
                    oversalary = oversalary + totaloversalary

                    If maxs < maxsalary Then
                        maxs = maxsalary
                    End If
                    If mins > mixsalary Then
                        mins = mixsalary
                    End If

                    If maxd < maxoverdays Then
                        maxd = maxoverdays
                    End If
                    If mind > mixoverdays Then
                        mind = mixoverdays
                    End If
                    'totald = totald + amountoverdays
                    If maxp < maxoverpay Then
                        maxp = maxoverpay
                    End If
                    If minp > mixoverpay Then
                        minp = mixoverpay
                    End If
                    If wrow = True Then
                        DataTableRow(1, dname, workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), Math.Round(amountoverdays / people, 2).ToString(), amountoverdays.ToString(), maxoverpay.ToString(), mixoverpay.ToString(), amountoverpay.ToString(), 0, tableConst)
                    Else
                        DataTableRow(2, "", workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxoverdays.ToString(), mixoverdays.ToString(), Math.Round(amountoverdays / people, 2).ToString(), amountoverdays.ToString(), maxoverpay.ToString(), mixoverpay.ToString(), amountoverpay.ToString(), 0, tableConst)
                    End If
                    DataTableRow(2, "", "", "", "合计", "", totalpiece.ToString(), totalpeople.ToString(), "", "", "", totaloverdays.ToString(), "", "", totaloversalary.ToString(), 0, tableConst)
                    DataTableRow(2, "", "", maxs.ToString(), mins.ToString(), Math.Round(totalpiece / totalpeople, 2).ToString(), "", "", maxd.ToString(), mind.ToString(), Math.Round(totaloverdays / totalpeople, 2).ToString(), "", maxp, minp, "", 1, tableConst)
                    reader.Close()
                    Dim DataTable1 As New Table
                    DataTable1.CellSpacing = 0
                    DataTable1.CellPadding = 2
                    DataTable1.GridLines = GridLines.Both
                    'DataTable.BorderWidth = New Unit(0)
                    DataTable1.BorderColor = Color.Black
                    DataTable1.Width = New Unit(1400)
                    tableConst = DataTable1

                    DataTableRow(3, "", "总计", "", "", "", piece, allpeople, "", "", "", overdays, "", "", oversalary, 0, tableConst)
                End If



                Response.ContentType = "application/vnd.ms-excel"
                Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            End If

    End Sub

    Sub HeaderTableProc(ByVal str As String, ByVal str1 As String)
        Dim HeaderTable As New Table
        HeaderTable.CellSpacing = 0
        HeaderTable.CellPadding = 2
        HeaderTable.GridLines = GridLines.None
        HeaderTable.BorderWidth = New Unit(0)
        'HeaderTable.BorderColor = Color.Black
        HeaderTable.Width = New Unit(1400)

        row = New TableRow
        row.BackColor = Color.White


        row.HorizontalAlign = HorizontalAlign.Center
        row.VerticalAlign = VerticalAlign.Middle

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Text &= "<b><font size=4>" & str & "年" & str1 & "月工资分析</font></b>"
        cell.Width = New Unit(1400)
        row.Cells.Add(cell)

        HeaderTable.Rows.Add(row)
        ReportTablesHolder.Controls.Add(HeaderTable)
    End Sub

        Sub DataTableRow(ByVal ty As Integer, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String _
                , ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String _
                , ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal num As Integer, ByVal tableTemp As Table)



            If ty = 0 Then


                row = New TableRow
                row.BackColor = Color.White
                row.Width = New Unit(1500)

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
                cell.Text &= "<b>工资</b>"
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
                cell.Text &= "<b>应发金额</b>"
                cell.Width = New Unit(300)
                cell.ColumnSpan = 4
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>加班费</b>"
                cell.Width = New Unit(320)
                cell.ColumnSpan = 3
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
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>最大加班</b>"
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>最小加班</b>"
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>总加班费</b>"
                cell.Width = New Unit(120)
                row.Cells.Add(cell)

                tableTemp.Rows.Add(row)
                ReportTablesHolder.Controls.Add(tableTemp)
            Else


                If ty = 1 Then
                    'Dim DataTable As New Table
                    'DataTable.CellSpacing = 0
                    'DataTable.CellPadding = 2
                    'DataTable.GridLines = GridLines.Both
                    ''DataTable.BorderWidth = New Unit(0)
                    'DataTable.BorderColor = Color.Black
                    'DataTable.Width = New Unit(1200)

                    row = New TableRow
                    row.BackColor = Color.White
                    row.Width = New Unit(1500)

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
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str11
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str12
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str13
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str14
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
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str11
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str12
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str13
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str14
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
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str11
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str12
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str13
                        cell.Width = New Unit(100)


                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str14
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
