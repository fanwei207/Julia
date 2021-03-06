'!*******************************************************************************!
'* @@ NAME				:	SalaryAnalyseTimeExcel1.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for SalaryAnalyseTimeExcel1.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	July 28 2006
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

Partial Class SalaryAnalyseTimeExcel1
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

                'Dim attendencedays As Integer = 23
                'Query = " Select monthlyWorkDays From BaseInfo where year(workdate)= '" & Request("year") & "' and month(workdate)= '" & Request("sate") & "'"
                'attendencedays = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)

                Dim i As Integer
                Dim people As Decimal = 0

                Dim salary(11) As Decimal '//工资 || 最小工资(0-类别/ 1-部门 / 2-所有)；最大工资(3-类别/ 4-部门 / 5-所有)； 平均工资(6-类别/ 7-部门 / 8-所有)； 总计  (9-类别/ 10-部门 / 11-所有)

                Dim attadence(8) As Decimal  '//出勤天（>8小时值为1） || 最小出勤天(0-类别/ 1-部门 / 2-所有)；最大出勤天(3-类别/ 4-部门 / 5-所有)； 总计(6-类别/ 7-部门 / 8-所有)

                Dim hoursday(8) As Decimal  '//小时天 || 最小小时天(0-类别/ 1-部门 / 2-所有)；最大小时天(3-类别/ 4-部门 / 5-所有)； 总计(6-类别/ 7-部门 / 8-所有)  

                Dim Over(8) As Decimal  '//加班工资 || 最小加班工资(0-类别/ 1-部门 / 2-所有)；最大加班工资(3-类别/ 4-部门 / 5-所有)； 总计(6-类别/ 7-部门 / 8-所有)

                Dim holiday(8) As Decimal  '//国假工资 || 最小国假工资(0-类别/ 1-部门 / 2-所有)；最大国假工资(3-类别/ 4-部门 / 5-所有)； 总计(6-类别/ 7-部门 / 8-所有)

                Dim duereward(8) As Decimal  '//应发金额 || 最小应发金额(0-类别/ 1-部门 / 2-所有)；最大应发金额(3-类别/ 4-部门 / 5-所有)； 总计(6-类别/ 7-部门 / 8-所有)

                '//初始化 ----- //
                For i = 0 To 2
                    salary(i) = 1000000
                    attadence(i) = 1000000
                    hoursday(i) = 1000000
                    Over(i) = 1000000
                    holiday(i) = 1000000
                    duereward(i) = 1000000
                Next

                For i = 3 To 8
                    salary(i) = 0
                    attadence(i) = 0
                    hoursday(i) = 0
                    Over(i) = 0
                    holiday(i) = 0
                    duereward(i) = 0
                Next

                salary(9) = 0
                salary(10) = 0
                salary(11) = 0
                '//End---

                Query = "  SELECT hr_Time_SalaryDuereward, hr_Time_SalaryWorkpay,d.departmentID,d.name, hr_Time_SalaryAssess,hr_Time_SalaryHoliday,hr_Time_SalaryAttendance,hr_Time_SalaryAttDay,hr_Time_SalaryWorktype,ISNULL(s.systemCodeName,'') "
                Query &= " FROM hr_fin_Time h "
                Query &= " INNER JOIN tcpc0.dbo.Users u ON u.userID = h.hr_Time_SalaryUserID "
                Query &= " LEFT OUTER JOIN departments d ON d.departmentID = h.hr_Time_SalaryDepartment "
                Query &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s on s.systemCodeID= h.hr_Time_SalaryWorktype "
                Query &= " WHERE YEAR(h.hr_Time_SalaryDate)='" & Request("year") & "' AND MONTH(h.hr_Time_SalaryDate) ='" & Request("sate") & "' "
                Query &= " ORDER BY h.hr_Time_SalaryDepartment,h.hr_Time_SalaryWorktype "


                HeaderTableProc(Request("year"), Request("sate"), "")

                'Response.Write(Query)
                'Exit Sub

                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
                Dim depID As Integer = 0
                Dim ds As DataSet
                'Dim workshop As New Hashtable
                'Dim i As Integer
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


               
                Dim tableConst As New Table
                Dim type As String = ""

                While reader.Read

                    If reader("departmentID") <> depID Then

                        depID = reader("departmentID")

                        If deflag = True Then
                            ' last workshop in department

                            totalpeople = totalpeople + people '部门总人数
                            allpeople = allpeople + totalpeople '总人数

                            '//工资-------//
                            salary(1) = MinValue(salary(1), salary(0))
                            salary(4) = MaxValue(salary(4), salary(3))

                            salary(2) = MinValue(salary(2), salary(1))
                            salary(5) = MaxValue(salary(5), salary(4))

                            salary(10) = salary(10) + salary(9)

                            salary(6) = Math.Round(salary(9) / people, 2)
                            salary(7) = Math.Round(salary(10) / totalpeople, 2)
                            '//End

                            '//出勤天-----//
                            attadence(1) = MinValue(attadence(1), attadence(0))
                            attadence(4) = MaxValue(attadence(4), attadence(3))

                            attadence(2) = MinValue(attadence(2), attadence(1))
                            attadence(5) = MaxValue(attadence(5), attadence(4))

                            attadence(7) = attadence(7) + attadence(6)
                            '//End

                            '//小时天----//
                            hoursday(1) = MinValue(hoursday(1), hoursday(0))
                            hoursday(4) = MaxValue(hoursday(4), hoursday(3))

                            hoursday(2) = MinValue(hoursday(2), hoursday(1))
                            hoursday(5) = MaxValue(hoursday(5), hoursday(4))

                            hoursday(7) = hoursday(7) + hoursday(6)
                            '//End
                           
                            '//加班工资----//
                            Over(1) = MinValue(Over(1), Over(0))
                            Over(4) = MaxValue(Over(4), Over(3))

                            Over(2) = MinValue(Over(2), Over(1))
                            Over(5) = MaxValue(Over(5), Over(4))

                            Over(7) = Over(7) + Over(6)
                            '//End

                            '//国假工资----//
                            holiday(1) = MinValue(holiday(1), holiday(0))
                            holiday(4) = MaxValue(holiday(4), holiday(3))

                            holiday(2) = MinValue(holiday(2), holiday(1))
                            holiday(5) = MaxValue(holiday(5), holiday(4))

                            holiday(7) = holiday(7) + holiday(6)
                            '//End

                            '//应发金额----//
                            duereward(1) = MinValue(duereward(1), duereward(0))
                            duereward(4) = MaxValue(duereward(4), duereward(3))

                            duereward(2) = MinValue(duereward(2), duereward(1))
                            duereward(5) = MaxValue(duereward(5), duereward(4))

                            duereward(7) = duereward(7) + duereward(6)
                            '//End
                          
                            '// For All 
                            salary(11) = salary(11) + salary(10)
                            attadence(8) = attadence(8) + attadence(7)
                            hoursday(8) = hoursday(8) + hoursday(7)
                            Over(8) = Over(8) + Over(7)
                            holiday(8) = holiday(8) + holiday(7)
                            duereward(8) = duereward(8) + duereward(7)
                            '// End
                           
                            If wrow = True Then
                                DataTableRow(1, dname, type, salary(3).ToString(), salary(0).ToString(), salary(6).ToString(), salary(9).ToString(), people.ToString(), attadence(3).ToString(), attadence(0).ToString(), attadence(6).ToString(), hoursday(3).ToString(), hoursday(0).ToString(), hoursday(6).ToString(), Over(3).ToString(), Over(0).ToString(), Over(6).ToString(), holiday(3).ToString(), holiday(0).ToString(), holiday(6).ToString(), duereward(3).ToString(), duereward(0).ToString(), duereward(6).ToString(), dnum, tableConst)
                                wrow = False
                            Else
                                DataTableRow(2, "", type, salary(3).ToString(), salary(0).ToString(), salary(6).ToString(), salary(9).ToString(), people.ToString(), attadence(3).ToString(), attadence(0).ToString(), attadence(6).ToString(), hoursday(3).ToString(), hoursday(0).ToString(), hoursday(6).ToString(), Over(3).ToString(), Over(0).ToString(), Over(6).ToString(), holiday(3).ToString(), holiday(0).ToString(), holiday(6).ToString(), duereward(3).ToString(), duereward(0).ToString(), duereward(6).ToString(), 0, tableConst)
                            End If

                            DataTableRow(2, "", "", "", "合计", "", salary(10).ToString(), totalpeople, "", "", attadence(7).ToString(), "", "", hoursday(7).ToString(), "", "", Over(7).ToString(), "", "", holiday(7).ToString(), "", "", duereward(7).ToString(), 1, tableConst)
                            DataTableRow(2, "", "", salary(4).ToString(), salary(1).ToString(), salary(8).ToString(), "", "", attadence(4).ToString(), attadence(1).ToString(), "", hoursday(4).ToString(), hoursday(1).ToString(), "", Over(4).ToString(), Over(1).ToString(), "", holiday(4).ToString(), holiday(1).ToString(), "", duereward(4).ToString(), duereward(1).ToString(), "", 1, tableConst)
                            ' all deparetments's amount
                          
                        End If '// 跳过第一条记录

                        dname = reader("name")

                        Dim DataTable As New Table
                        DataTable.CellSpacing = 0
                        DataTable.CellPadding = 2
                        DataTable.GridLines = GridLines.Both
                        'DataTable.BorderWidth = New Unit(0)
                        DataTable.BorderColor = Color.Black
                        DataTable.Width = New Unit(1400)

                        tableConst = DataTable

                        DataTableRow(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, tableConst)

                        totalpeople = 0
                        people = 0
                       

                        type = "111111"

                        woflag = False
                        wrow = True
                    End If

                    If type <> reader(9).ToString() Then  'defferent type row


                        If woflag = True Then
                            salary(6) = Math.Round(salary(9) / people, 2)
                            'if the people is zero
                            'If people = 0 Then
                            '    mixsalary = 0
                            '    mindays = 0
                            'End If
                            salary(10) = salary(10) + salary(9)
                            totalpeople = totalpeople + people

                            salary(1) = MinValue(salary(1), salary(0))
                            salary(4) = MaxValue(salary(4), salary(3))

                            attadence(1) = MinValue(attadence(1), attadence(0))
                            attadence(4) = MaxValue(attadence(4), attadence(3))

                            hoursday(1) = MinValue(hoursday(1), hoursday(0))
                            hoursday(4) = MaxValue(hoursday(4), hoursday(3))

                            Over(1) = MinValue(Over(1), Over(0))
                            Over(4) = MaxValue(Over(4), Over(3))

                            holiday(1) = MinValue(holiday(1), holiday(0))
                            holiday(4) = MaxValue(holiday(4), holiday(3))

                            duereward(1) = MinValue(duereward(1), duereward(0))
                            duereward(4) = MaxValue(duereward(4), duereward(3))

                            attadence(7) = attadence(7) + attadence(6)
                            hoursday(7) = hoursday(7) + hoursday(6)
                            Over(7) = Over(7) + Over(6)
                            holiday(7) = holiday(7) + holiday(6)
                            duereward(7) = duereward(7) + duereward(6)

                            If wrow = True Then
                                DataTableRow(1, dname, type, salary(3).ToString(), salary(0).ToString(), salary(6).ToString(), salary(9).ToString(), people.ToString(), attadence(3).ToString(), attadence(0).ToString(), attadence(6).ToString(), hoursday(3).ToString(), hoursday(0).ToString(), hoursday(6).ToString(), Over(3).ToString(), Over(0).ToString(), Over(6).ToString(), holiday(3).ToString(), holiday(0).ToString(), holiday(6).ToString(), duereward(3).ToString(), duereward(0).ToString(), duereward(6).ToString(), dnum, tableConst)
                                wrow = False
                            Else
                                DataTableRow(2, "", type, salary(3).ToString(), salary(0).ToString(), salary(6).ToString(), salary(9).ToString(), people.ToString(), attadence(3).ToString(), attadence(0).ToString(), attadence(6).ToString(), hoursday(3).ToString(), hoursday(0).ToString(), hoursday(6).ToString(), Over(3).ToString(), Over(0).ToString(), Over(6).ToString(), holiday(3).ToString(), holiday(0).ToString(), holiday(6).ToString(), duereward(3).ToString(), duereward(0).ToString(), duereward(6).ToString(), 0, tableConst)
                            End If


                            '---------------------------------------------------/
                            'reset the Parameters to default
                            '---------------------------------------------------/
                            people = 0
                            salary(0) = 1000000
                            attadence(0) = 1000000
                            hoursday(0) = 1000000
                            Over(0) = 1000000
                            holiday(0) = 1000000
                            duereward(0) = 1000000

                            For i = 0 To 1
                                salary(i * 1 + 3) = 0
                                attadence(i * 1 + 3) = 0
                                hoursday(i * 1 + 3) = 0
                                Over(i * 1 + 3) = 0
                                holiday(i * 1 + 3) = 0
                                duereward(i * 1 + 3) = 0
                            Next
                            salary(9) = 0
                            '-----------------------------------------------/

                        End If

                       


                        type = reader(9)
                    End If

                    'comparedays = reader(7)
                    'For Time Salary compared (Condition is the days of hours >attendence days in current month )
                    'If comparedays >= attendencedays Then

                    '//实发工资-----------//
                    salary(0) = MinValue(salary(0), reader(1))
                    salary(3) = MaxValue(salary(3), reader(1))
                    salary(9) = salary(9) + reader(1)
                    '// End 实发工资 -----//

                    '//出勤天-----------//
                    attadence(0) = MinValue(attadence(0), reader(7))
                    attadence(3) = MaxValue(attadence(3), reader(7))
                    attadence(6) = attadence(6) + reader(7)
                    '// End 出勤天 -----//

                    people = people + 1  '// 人数
                    'End If

                    '//小时天-----------//
                    hoursday(0) = MinValue(hoursday(0), reader(6))
                    hoursday(3) = MaxValue(hoursday(3), reader(6))
                    hoursday(6) = hoursday(6) + reader(6)
                    '// End 小时天 -----//

                    '//加班工资-----------//
                    Over(0) = MinValue(Over(0), reader(4))
                    Over(3) = MaxValue(Over(3), reader(4))
                    Over(6) = Over(6) + reader(4)
                    '// End 加班工资 -----//

                    '//国假工资-----------//
                    holiday(0) = MinValue(holiday(0), reader(5))
                    holiday(3) = MaxValue(holiday(3), reader(5))
                    holiday(6) = holiday(6) + reader(5)
                    '// End 国假工资 -----//


                    duereward(0) = MinValue(duereward(0), reader(0))
                    duereward(3) = MaxValue(duereward(3), reader(0))
                    duereward(6) = duereward(6) + reader(0)




                    deflag = True
                    woflag = True
                End While

                'last line --------------------------------------------------------------------------------------------------------------------------------
                If deflag = True Then

                    totalpeople = totalpeople + people
                    allpeople = allpeople + totalpeople

                    '//工资-------//
                    salary(1) = MinValue(salary(1), salary(0))
                    salary(4) = MaxValue(salary(4), salary(3))

                    salary(2) = MinValue(salary(2), salary(1))
                    salary(5) = MaxValue(salary(5), salary(4))

                    salary(10) = salary(10) + salary(9)

                    salary(6) = Math.Round(salary(9) / people, 2)
                    salary(7) = Math.Round(salary(10) / totalpeople, 2)
                    '//End

                    '//出勤天-----//
                    attadence(1) = MinValue(attadence(1), attadence(0))
                    attadence(4) = MaxValue(attadence(4), attadence(3))

                    attadence(2) = MinValue(attadence(2), attadence(1))
                    attadence(5) = MaxValue(attadence(5), attadence(4))

                    attadence(7) = attadence(7) + attadence(6)
                    '//End

                    '//小时天----//
                    hoursday(1) = MinValue(hoursday(1), hoursday(0))
                    hoursday(4) = MaxValue(hoursday(4), hoursday(3))

                    hoursday(2) = MinValue(hoursday(2), hoursday(1))
                    hoursday(5) = MaxValue(hoursday(5), hoursday(4))

                    hoursday(7) = hoursday(7) + hoursday(6)
                    '//End

                    '//加班工资----//
                    Over(1) = MinValue(Over(1), Over(0))
                    Over(4) = MaxValue(Over(4), Over(3))

                    Over(2) = MinValue(Over(2), Over(1))
                    Over(5) = MaxValue(Over(5), Over(4))

                    Over(7) = Over(7) + Over(6)
                    '//End

                    '//国假工资----//
                    holiday(1) = MinValue(holiday(1), holiday(0))
                    holiday(4) = MaxValue(holiday(4), holiday(3))

                    holiday(2) = MinValue(holiday(2), holiday(1))
                    holiday(5) = MaxValue(holiday(5), holiday(4))

                    holiday(7) = holiday(7) + holiday(6)
                    '//End

                    '//应发金额----//
                    duereward(1) = MinValue(duereward(1), duereward(0))
                    duereward(4) = MaxValue(duereward(4), duereward(3))

                    duereward(2) = MinValue(duereward(2), duereward(1))
                    duereward(5) = MaxValue(duereward(5), duereward(4))

                    duereward(7) = duereward(7) + duereward(6)
                    '//End

                    '// For All 
                    salary(11) = salary(11) + salary(10)
                    attadence(8) = attadence(8) + attadence(7)
                    hoursday(8) = hoursday(8) + hoursday(7)
                    Over(8) = Over(8) + Over(7)
                    holiday(8) = holiday(8) + holiday(7)
                    duereward(8) = duereward(8) + duereward(7)
                    '// End


                    If wrow = True Then
                        DataTableRow(1, dname, type, salary(3).ToString(), salary(0).ToString(), salary(6).ToString(), salary(9).ToString(), people.ToString(), attadence(3).ToString(), attadence(0).ToString(), attadence(6).ToString(), hoursday(3).ToString(), hoursday(0).ToString(), hoursday(6).ToString(), Over(3).ToString(), Over(0).ToString(), Over(6).ToString(), holiday(3).ToString(), holiday(0).ToString(), holiday(6).ToString(), duereward(3).ToString(), duereward(0).ToString(), duereward(6).ToString(), dnum, tableConst)
                    Else
                        DataTableRow(2, "", type, salary(3).ToString(), salary(0).ToString(), salary(6).ToString(), salary(9).ToString(), people.ToString(), attadence(3).ToString(), attadence(0).ToString(), attadence(6).ToString(), hoursday(3).ToString(), hoursday(0).ToString(), hoursday(6).ToString(), Over(3).ToString(), Over(0).ToString(), Over(6).ToString(), holiday(3).ToString(), holiday(0).ToString(), holiday(6).ToString(), duereward(3).ToString(), duereward(0).ToString(), duereward(6).ToString(), 0, tableConst)
                    End If

                    DataTableRow(2, "", "", "", "合计", "", salary(10).ToString(), totalpeople, "", "", attadence(7).ToString(), "", "", hoursday(7).ToString(), "", "", Over(7).ToString(), "", "", holiday(7).ToString(), "", "", duereward(7).ToString(), 1, tableConst)
                    DataTableRow(2, "", "", salary(4).ToString(), salary(1).ToString(), salary(8).ToString(), "", "", attadence(4).ToString(), attadence(1).ToString(), "", hoursday(4).ToString(), hoursday(1).ToString(), "", Over(4).ToString(), Over(1).ToString(), "", holiday(4).ToString(), holiday(1).ToString(), "", duereward(4).ToString(), duereward(1).ToString(), "", 1, tableConst)
                    reader.Close()

                    Dim DataTable1 As New Table
                    DataTable1.CellSpacing = 0
                    DataTable1.CellPadding = 2
                    DataTable1.GridLines = GridLines.Both
                    'DataTable.BorderWidth = New Unit(0)
                    DataTable1.BorderColor = Color.Black
                    DataTable1.Width = New Unit(1400)
                    tableConst = DataTable1

                   

                    'Dim avgiS As Decimal = 0
                    'Dim avgiP As Decimal = 0
                    'If piece = 0 Then
                    '    avgiS = 0
                    'Else
                    '    avgiS = Math.Round(piece / allpeople, 2)
                    'End If
                    'If totalsalary = 0 Then
                    '    avgiP = 0
                    'Else
                    '    avgiP = Math.Round(totalsalary / allpeople, 2)
                    'End If


                    DataTableRow(3, "", "总计", salary(5).ToString(), salary(2).ToString(), salary(8).ToString(), salary(11).ToString(), totalpeople, attadence(5).ToString(), attadence(2).ToString(), attadence(8).ToString(), hoursday(5).ToString(), hoursday(2).ToString(), hoursday(8).ToString(), Over(5).ToString(), Over(2).ToString(), Over(8).ToString(), holiday(5).ToString(), holiday(2).ToString(), holiday(8).ToString(), duereward(5).ToString(), duereward(2).ToString(), duereward(8).ToString(), 0, tableConst)
                End If
                Array.Clear(salary, 0, 12)
                Array.Clear(attadence, 0, 9)
                Array.Clear(hoursday, 0, 9)
                Array.Clear(Over, 0, 9)
                Array.Clear(holiday, 0, 9)
                Array.Clear(duereward, 0, 9)

                Response.ContentType = "application/vnd.ms-excel"
                Response.AppendHeader("content-disposition", "attachment; filename=report.xls")

            End If

    End Sub

    Sub HeaderTableProc(ByVal str As String, ByVal str1 As String, ByVal str2 As String)
        Dim HeaderTable As New Table
        HeaderTable.CellSpacing = 0
        HeaderTable.CellPadding = 2
        HeaderTable.GridLines = GridLines.None
        HeaderTable.BorderWidth = New Unit(0)
        'HeaderTable.BorderColor = Color.Black
        HeaderTable.Width = New Unit(1280)

        row = New TableRow
        row.BackColor = Color.White


        row.HorizontalAlign = HorizontalAlign.Center
        row.VerticalAlign = VerticalAlign.Middle

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b><font size=4>" & str & "年" & str1 & "月工资分析</font></b>"
            'cell.Text &= "<b><font size=4>" & str & "年" & str1 & "月工资分析</font></b>&nbsp;<font size=4>应出勤 " & str2 & " 天</font><br>(实出勤>=应出勤)"
        cell.Width = New Unit(1460)
        row.Cells.Add(cell)

        HeaderTable.Rows.Add(row)
        ReportTablesHolder.Controls.Add(HeaderTable)
    End Sub

        Sub DataTableRow(ByVal ty As Integer, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String _
                , ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String _
                , ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String _
                , ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal str15 As String _
                , ByVal str16 As String, ByVal str17 As String, ByVal str18 As String, ByVal str19 As String _
                , ByVal str20 As String, ByVal str21 As String, ByVal str22 As String, ByVal num As Integer, ByVal tableTemp As Table)
            If ty = 0 Then
                row = New TableRow
                row.BackColor = Color.White
                'row.Width = New Unit(1460)
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
                cell.Width = New Unit(100)
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
                cell.Text &= "<b>出勤天()</b>"
                cell.Width = New Unit(300)
                cell.ColumnSpan = 3
                row.Cells.Add(cell)

                'cell = New TableCell
                'cell.HorizontalAlign = HorizontalAlign.Center
                '    cell.Text &= "<b>出勤</b>"
                'cell.Width = New Unit(120)
                'row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>小时天</b>"
                cell.Width = New Unit(300)
                cell.ColumnSpan = 3
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>加班费</b>"
                cell.Width = New Unit(300)
                cell.ColumnSpan = 3
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>国假工资</b>"
                cell.Width = New Unit(300)
                cell.ColumnSpan = 3
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>应发金额</b>"
                cell.Width = New Unit(300)
                cell.ColumnSpan = 3
                row.Cells.Add(cell)


                'cell = New TableCell
                'cell.HorizontalAlign = HorizontalAlign.Center
                'cell.Text &= "<b>>=应出勤人数占本部门总人数的%</b>"
                'cell.Width = New Unit(120)
                'cell.RowSpan = 2
                'row.Cells.Add(cell)

                tableTemp.Rows.Add(row)

                row = New TableRow
                row.BackColor = Color.White
                row.VerticalAlign = VerticalAlign.Top

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>名称</b>"
                cell.Width = New Unit(100)
                row.Cells.Add(cell)

                '// 工资-------
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
                '// ------------------------

                '// 出勤天-----------
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

                'cell = New TableCell
                'cell.HorizontalAlign = HorizontalAlign.Center
                'cell.Text &= "<b>平均值</b>"
                'cell.Width = New Unit(120)
                'row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= "<b>总计</b>"
                cell.Width = New Unit(100)
                row.Cells.Add(cell)
                '//-----------------

                '// 小时天-------------------------
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
                cell.Text &= "<b>总计</b>"
                cell.Width = New Unit(100)
                row.Cells.Add(cell)
                '//-----------------------

                '//加班费---------------------
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
                cell.Text &= "<b>总计</b>"
                cell.Width = New Unit(100)
                row.Cells.Add(cell)
                '//--------------------------

                '//国假工资---------------------------
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
                cell.Text &= "<b>总计</b>"
                cell.Width = New Unit(100)
                row.Cells.Add(cell)
                '//--------------------------

                '// 应发金额---------------------
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
                cell.Text &= "<b>总计</b>"
                cell.Width = New Unit(100)
                row.Cells.Add(cell)
                '//---------------------

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
                    row.Width = New Unit(1460)
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
                    cell.Width = New Unit(100)
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
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str15
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str16
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str17
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str18
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str19
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str20
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str21
                    cell.Width = New Unit(100)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str22
                    cell.Width = New Unit(100)
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
                        'cell.RowSpan = num
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
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str15
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str16
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str17
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        'cell = New TableCell
                        'cell.HorizontalAlign = HorizontalAlign.Center
                        'cell.Text &= str16
                        'cell.Width = New Unit(120)
                        'row.Cells.Add(cell)
                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str18
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str19
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str20
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str21
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str22
                        cell.Width = New Unit(100)
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
                        cell.ColumnSpan = 2
                        cell.Text &= str2
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
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str14
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str15
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str16
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str17
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str18
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str19
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str20
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str21
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        cell = New TableCell
                        cell.HorizontalAlign = HorizontalAlign.Center
                        cell.Text &= str22
                        cell.Width = New Unit(100)
                        row.Cells.Add(cell)

                        'cell = New TableCell
                        'cell.HorizontalAlign = HorizontalAlign.Center
                        'cell.Text &= str16
                        'cell.Width = New Unit(120)
                        'row.Cells.Add(cell)

                        tableTemp.Rows.Add(row)

                        ReportTablesHolder.Controls.Add(tableTemp)

                    End If
                End If
            End If
        End Sub


        Function MaxValue(ByVal d1 As Decimal, ByVal d2 As Decimal) As Decimal
            If d1 < d2 Then
                d1 = d2
            End If
            MaxValue = d1
        End Function

        Function MinValue(ByVal s1 As Decimal, ByVal s2 As Decimal) As Decimal
            If s2 >= 0 Then
                If s1 > s2 Then
                    s1 = s2
                End If
            End If
            MinValue = s1
        End Function


    End Class

End Namespace

