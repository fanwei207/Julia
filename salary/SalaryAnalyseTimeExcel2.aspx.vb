'!*******************************************************************************!
'* @@ NAME				:	SalaryAnalyseTimeExcel2.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for SalaryAnalyseTimeExcel2.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	July 31, 2006
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

Partial Class SalaryAnalyseTimeExcel2
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

            Dim maxdays As Decimal = 0
            Dim mindays As Decimal = 0
            Dim totaldays As Decimal = 0

            Dim maxs As Decimal = 0
            Dim mins As Decimal = 1000000
            Dim avgs As Decimal = 0

            Dim maxd As Decimal = 0
            Dim mind As Decimal = 1000000
            Dim avgd As Decimal = 0
            Dim totald As Decimal = 0


            Dim attendencedays As Integer = 23
            Query = " Select monthlyWorkDays From BaseInfo where year(workdate)= '" & Request("year") & "' and month(workdate)= '" & Request("sate") & "'"
            attendencedays = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)


            Dim people As Decimal = 0

            Query = " Select  p.duereward,isnull(p.benefitsalary,0)+isnull(p.basicesalary,0),d.departmentID,d.name,isnull(p.workday,0),isnull(p.hoursday,0) as attendencedays,p.typeID,isnull(s.systemCodeName,'')  From commonSalary p  "
            Query &= " inner join tcpc0.dbo.users u on u.userID=p.usercode"
            Query &= " INNER join departments d on d.departmentID=u.departmentID "
            Query &= " left outer join tcpc0.dbo.systemCode s on s.systemCodeID=p.typeID "
            Query &= " Where year(p.Sdate)= '" & Request("year") & "' and month(p.Sdate)= '" & Request("sate") & "' and p.flag<>'1' and p.attendanceDays < " & attendencedays.ToString()
            Query &= " order by d.departmentID,p.typeID  "

            HeaderTableProc(Request("year"), Request("sate"))

            'Response.Write(Query)
            'Exit Sub

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            Dim depID As Integer = 0
            Dim wID As Integer = 0
            Dim ds As DataSet

            Dim i As Integer
            Dim number As Integer
            'Dim numtotal As Integer
            Dim totalpiece As Decimal = 0
            Dim totalpeople As Decimal = 0

            Dim deflag As Boolean = False
            Dim woflag As Boolean = False
            Dim wrow As Boolean = False
            Dim dnum As Integer
            Dim dname As String

            Dim piece As Decimal = 0
            Dim allpeople As Decimal = 0
            Dim type As String = ""

            Dim tableConst As New Table
            While reader.Read

                If reader("departmentID") <> depID Then

                    depID = reader("departmentID")

                    If deflag = True Then
                        ' last workshop in department 
                        totalpiece = totalpiece + amountsalary
                        totalpeople = totalpeople + people

                        If maxs < maxsalary Then
                            maxs = maxsalary
                        End If
                        If mins > mixsalary Then
                            mins = mixsalary
                        End If

                        If maxd < maxdays Then
                            maxd = maxdays
                        End If
                        If mind > mindays Then
                            mind = mindays
                        End If
                        totald = totald + totaldays

                        If wrow = True Then
                            DataTableRow(1, dname, Type, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), Math.Round(totaldays / people, 2).ToString(), dnum, tableConst)
                            wrow = False
                        Else
                            DataTableRow(2, "", Type, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), Math.Round(totaldays / people, 2).ToString(), 0, tableConst)
                        End If
                        DataTableRow(2, "", "", "", "合计", "", totalpiece, totalpeople, "", "", "", 0, tableConst)

                        DataTableRow(2, "", "", maxs.ToString(), mins.ToString(), Math.Round(totalpiece / totalpeople, 2).ToString(), "", "", maxd.ToString(), mind.ToString(), Math.Round(totald / totalpeople, 2).ToString(), 1, tableConst)
                        ' all deparetments's amount
                        piece = piece + totalpiece
                        allpeople = allpeople + totalpeople
                    End If
                    dname = reader("name")

                    number = 0

                 

                    Dim DataTable As New Table
                    DataTable.CellSpacing = 0
                    DataTable.CellPadding = 2
                    DataTable.GridLines = GridLines.Both
                    'DataTable.BorderWidth = New Unit(0)
                    DataTable.BorderColor = Color.Black
                    DataTable.Width = New Unit(1400)

                    tableConst = DataTable

                    DataTableRow(0, "", "", "", "", "", "", "", "", "", "", 0, tableConst)


                    ' ds.Reset()
                    totalpiece = 0
                    totalpeople = 0
                    amountsalary = 0
                    people = 0
                    maxs = 0
                    mins = 1000000
                    maxd = 0
                    mind = 1000000
                    totald = 0

                    maxsalary = 0
                    mixsalary = 1000000
                    totaldays = 0
                    maxdays = 0
                    mindays = 1000000

                    woflag = False
                    wrow = True
                    wID = -1
                End If

                If type <> reader(7).ToString() Then
                    'Add the total  in same department
                    totalpiece = totalpiece + amountsalary
                    totalpeople = totalpeople + people

                    If woflag = True Then
                        If wrow = True Then
                            DataTableRow(1, dname, type, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), Math.Round(totaldays / people, 2).ToString(), dnum, tableConst)
                            wrow = False
                        Else
                            DataTableRow(2, "", type, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), Math.Round(totaldays / people, 2).ToString(), 0, tableConst)
                        End If
                    End If

                    If maxs < maxsalary Then
                        maxs = maxsalary
                    End If
                    If mins > mixsalary Then
                        mins = mixsalary
                    End If

                    If maxd < maxdays Then
                        maxd = maxdays
                    End If
                    If mind > mindays Then
                        mind = mindays
                    End If
                    totald = totald + totaldays

                    amountsalary = 0
                    people = 0
                    maxsalary = 0
                    mixsalary = 1000000

                    totaldays = 0
                    maxdays = 0
                    mindays = 1000000

                    type = reader(7)
                End If

                'For pieceSalary compared
                If maxsalary < reader(1) Then
                    maxsalary = reader(1)
                End If

                If mixsalary > reader(1) Then
                    mixsalary = reader(1)
                End If

                amountsalary = amountsalary + reader(1)

                If maxdays < reader(5) Then
                    maxdays = reader(5)
                End If

                If mindays > reader(5) Then
                    mindays = reader(5)
                End If

                totaldays = totaldays + reader(5)

                people = people + 1

                woflag = True
                deflag = True

            End While
            If deflag = True Then
                totalpiece = totalpiece + amountsalary
                totalpeople = totalpeople + people

                piece = piece + totalpiece
                allpeople = allpeople + totalpeople

                If maxs < maxsalary Then
                    maxs = maxsalary
                End If
                If mins > mixsalary Then
                    mins = mixsalary
                End If

                If maxd < maxdays Then
                    maxd = maxdays
                End If
                If mind > mindays Then
                    mind = mindays
                End If
                totald = totald + totaldays

                If wrow = True Then
                    DataTableRow(1, dname, type, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), Math.Round(totaldays / people, 2).ToString(), 0, tableConst)
                Else
                    DataTableRow(2, "", type, maxsalary.ToString(), mixsalary.ToString(), Math.Round(amountsalary / people, 2).ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), Math.Round(totaldays / people, 2).ToString(), 0, tableConst)
                End If

                DataTableRow(2, "", "", "", "合计", "", totalpiece.ToString(), totalpeople.ToString(), "", "", "", 1, tableConst)
                DataTableRow(2, "", "", maxs.ToString(), mins.ToString(), Math.Round(totalpiece / totalpeople, 2).ToString(), "", "", maxd.ToString(), mind.ToString(), Math.Round(totald / totalpeople, 2).ToString(), 1, tableConst)

                reader.Close()

                Dim DataTable1 As New Table
                DataTable1.CellSpacing = 0
                DataTable1.CellPadding = 2
                DataTable1.GridLines = GridLines.Both
                'DataTable.BorderWidth = New Unit(0)
                DataTable1.BorderColor = Color.Black
                DataTable1.Width = New Unit(1400)
                tableConst = DataTable1
                DataTableRow(3, "", "总计", "", "", "", piece, allpeople, "", "", "", 0, tableConst)
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
        cell.Text &= "<b><font size=4>" & str & "年" & str1 & "月工资分析</font></b><br>(实出勤<应出勤)"
        cell.Width = New Unit(1400)
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
            row.Width = New Unit(1400)
            row.VerticalAlign = VerticalAlign.Top

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>部门</b>"
            cell.Width = New Unit(120)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>计酬方式</b>"
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
            cell.Text &= "<b>小时天</b>"
            cell.Width = New Unit(300)
            cell.ColumnSpan = 3
            row.Cells.Add(cell)

            tableTemp.Rows.Add(row)

            row = New TableRow
            row.BackColor = Color.White
            row.VerticalAlign = VerticalAlign.Top

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

            row.BackColor = Color.White
            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>最大</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>最小</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>平均值</b>"
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
                row.Width = New Unit(1400)
                row.VerticalAlign = VerticalAlign.Top

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text = str1
                cell.Width = New Unit(120)
                cell.RowSpan = 7
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
                cell.Width = New Unit(120)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str9
                cell.Width = New Unit(120)
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
                    row.VerticalAlign = VerticalAlign.Top

                    'cell = New TableCell
                    'cell.HorizontalAlign = HorizontalAlign.Center
                    'cell.Text = str1
                    'cell.Width = New Unit(120)
                    'cell.RowSpan = 1
                    'row.Cells.Add(cell)

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
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str9
                    cell.Width = New Unit(120)
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
                    row.VerticalAlign = VerticalAlign.Top

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
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str9
                    cell.Width = New Unit(120)
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

