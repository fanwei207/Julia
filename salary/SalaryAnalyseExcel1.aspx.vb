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

Partial Class SalaryAnalyseExcel1
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

            '//----get the total people in salary
            'Dim apeople As Decimal = 0
            'Query = " select count(usercode) From piecesalary where year(salaryDate)= '" & Request("year") & "' and month(salaryDate)= '" & Request("sate") & "'and flag<>'1' "
            'apeople = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)
            '//----end 
            Dim people As Decimal = 0

            Query = " Select  p.salary,isnull(p.oversalary,0),d.departmentID,d.name,p.workshopID,isnull(v.extrasalarey,0),isnull(r.allhours,0)+isnull(r.overdays,0),isnull(r.extramonthupdays,0)+isnull(r.extraupdays,0)+isnull(r.extramonthdowndays,0)+isnull(r.extradowndays,0),isnull(r.upspecialdays,0)+isnull(r.downspecialdays,0),isnull(pa.days,0) as days,isnull(pa.totalHours,0) as totalHours,isnull(p.attendencedays,0) as attendencedays,p.Exhalfdouble-p.Exdouble+p.overdays,p.Exdouble+p.saturdays  From piecesalary p  "
            ''Query &= " Left Outer Join OvertimesSalary v on v.salaryDate=p.salaryDate and v.usercode=p.usercode"
            Query &= " Left Outer Join (Select usercode,sum(extrasalarey+salarey+isnull(special,0)) as extrasalarey,sum(extratimes+times+isnull(specialdays,0)) as extratimes From OvertimesSalary where year(salaryDate)='" & Request("year") & "' and month(salaryDate)='" & Request("sate") & "' group by usercode) as v on v.usercode=p.usercode "
            Query &= " Left Outer Join (Select usercode,sum(days)as days,sum(totalHours)/8 as totalHours From PieceAttendence where year(workdate)='" & Request("year") & "' and month(workdate)='" & Request("sate") & "' group by usercode) as pa on pa.usercode=p.usercode "
            '################################   Modified by BaoXin in 20090310  #################################################################
            Query &= " left outer JOIN ReadyCaculateSalary r ON r.userCode=p.usercode and month(r.salaryDate)=" & Request("sate") & " and year(r.salaryDate)=" & Request("year")
            ' New piece salary Users
            'Query &= " left outer join (select usercode sum() from TestPiece where year(t.workdate)='" & Request("year") & "' and month (t.workdate)='" & Request("sate") & "' group by usercode ) t on t.usercode=p.usercode"
            '#################################         End   ###################################################################
            Query &= " INNER join departments d on d.departmentID=p.departmentID "
            'Query &= " left outer join Workshop w on w.id=p.workshopID and w.workshopID is null "
            Query &= " Where year(p.salaryDate)= '" & Request("year") & "' and month(p.salaryDate)= '" & Request("sate") & "' and p.flag<>'1' "  'and attendencedays >= " & attendencedays.ToString()
            Query &= " order by d.departmentID,p.workshopID "

            HeaderTableProc(Request("year"), Request("sate"), attendencedays.ToString())

            'Response.Write(Query)
            'Exit Sub

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            Dim depID As Integer = 0
            Dim wID As Integer = -1
            Dim ds As DataSet
            Dim workshop As New Hashtable
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


            Dim allsalary As Decimal = 0
            Dim allattendenceday As Decimal = 0
            Dim allpiece As Decimal = 0
            Dim allald As Decimal = 0

            Dim amountdays As Decimal = 0
            Dim amounthourdays As Decimal = 0
            Dim amountpieces As Decimal = 0
            Dim amountduereward As Decimal = 0
            Dim amountpeople As Decimal = 0
            Dim amountOverday As Decimal = 0
            Dim amountSaturday As Decimal = 0

            Dim qdays As Decimal = 0
            Dim qhourdays As Decimal = 0
            Dim qpieces As Decimal = 0
            Dim qduereward As Decimal = 0
            Dim qpeople As Decimal = 0
            Dim qOverday As Decimal = 0
            Dim qSaturday As Decimal = 0

            Dim lastMp As Decimal = 0
            Dim lastIp As Decimal = 10000000000
            Dim lasrMs As Decimal = 0
            Dim lasrIs As Decimal = 10000000000

            Dim lastdays As Decimal = 0
            Dim lasthourdays As Decimal = 0
            Dim lastpieces As Decimal = 0
            Dim lastduereward As Decimal = 0
            Dim lastpeople As Decimal = 0
            Dim lastOverday As Decimal = 0
            Dim lastSaturday As Decimal = 0

            Dim totalsalary As Decimal = 0

            Dim comparedays As Decimal = 0
            Dim tableConst As New Table

            'add by BaoXin in 20080909
            Dim amountsunday As Decimal = 0
            Dim amountAoverday As Decimal = 0
            Dim qsunday As Decimal = 0
            Dim qAoverday As Decimal = 0
            Dim lastsunday As Decimal = 0
            Dim lastAoverday As Decimal = 0
            Dim middletemp As Decimal = 0

            While reader.Read              '-------------------------------------------------

                If reader("departmentID") <> depID Then

                    depID = reader("departmentID")

                    If deflag = True Then
                        ' last workshop in department

                        totalpiece = totalpiece + amountsalary
                        totalpeople = totalpeople + people

                        If maxs < maxsalary Then
                            maxs = maxsalary
                        End If

                        If mixsalary > 0 Then
                            If mins > mixsalary Then
                                mins = mixsalary
                            End If
                        End If

                        If maxd < maxdays Then
                            maxd = maxdays
                        End If

                        If mindays > 0 Then
                            If mind > mindays Then
                                mind = mindays
                            End If
                        End If

                        totald = totald + totaldays

                        piece = piece + totalpiece
                        allpeople = allpeople + totalpeople

                        If lasrMs < maxs Then
                            lasrMs = maxs
                        End If

                        If mins > 0 Then
                            If lasrIs > mins Then
                                lasrIs = mins
                            End If
                        End If

                        If lastMp < maxd Then
                            lastMp = maxd
                        End If

                        If mind > 0 Then
                            If lastIp > mind Then
                                lastIp = mind
                            End If
                        End If

                        totalsalary = totalsalary + totald


                        If amountsalary = 0 Then
                            allsalary = 0
                        Else
                            allsalary = Math.Round(amountsalary / people, 2)
                        End If

                        If totaldays = 0 Then
                            allattendenceday = 0
                        Else
                            allattendenceday = Math.Round(totaldays / people, 2)
                        End If

                        If totalpiece = 0 Then
                            allpiece = 0
                        Else
                            allpiece = Math.Round(totalpiece / totalpeople, 2)
                        End If

                        If totald = 0 Then
                            allald = 0
                        Else
                            allald = Math.Round(totald / totalpeople, 2)
                        End If

                        qdays = qdays + amountdays
                        qhourdays = qhourdays + amounthourdays
                        qpieces = qpieces + amountpieces
                        qduereward = qduereward + amountduereward
                        qpeople = qpeople + amountpeople
                        qOverday = qOverday + amountOverday
                        qSaturday = qSaturday + amountSaturday
                        qsunday = qsunday + amountsunday
                        qAoverday = qAoverday + amountAoverday

                        If people = 0 Then
                            mixsalary = 0
                            mindays = 0
                        End If

                        If totalpeople = 0 Then
                            mins = 0
                            mind = 0
                        End If

                        If wrow = True Then
                            DataTableRow(1, dname, workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), allsalary.ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), allattendenceday.ToString(), amountdays.ToString(), amounthourdays.ToString(), amountpieces.ToString(), amountOverday.ToString(), amountSaturday.ToString, amountduereward.ToString(), amountpeople.ToString(), "", amountsunday.ToString(), amountAoverday.ToString(), dnum, tableConst)
                            wrow = False
                        Else
                            DataTableRow(2, "", workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), allsalary.ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), allattendenceday.ToString(), amountdays.ToString(), amounthourdays.ToString(), amountpieces.ToString(), amountOverday.ToString(), amountSaturday.ToString, amountduereward.ToString(), amountpeople.ToString(), "", amountsunday.ToString(), amountAoverday.ToString(), 0, tableConst)
                        End If

                        DataTableRow(2, "", "", "", "合计", "", totalpiece, totalpeople, "", "", "", qdays.ToString(), qhourdays.ToString(), qpieces.ToString(), qOverday.ToString, qSaturday.ToString, qduereward.ToString(), qpeople.ToString(), Math.Round(totalpeople / qpeople * 100, 2).ToString() & "%", qsunday.ToString, qAoverday.ToString, 1, tableConst)
                        DataTableRow(2, "", "", maxs.ToString(), mins.ToString(), allpiece.ToString(), "", "", maxd.ToString(), mind.ToString(), allald.ToString(), "", "", "", "", "", "", "", "", "", "", 1, tableConst)
                        ' all deparetments's amount
                        lastdays = lastdays + qdays
                        lasthourdays = lasthourdays + qhourdays
                        lastpieces = lastpieces + qpieces
                        lastduereward = lastduereward + qduereward
                        lastpeople = lastpeople + qpeople
                        lastOverday = lastOverday + qOverday
                        lastSaturday = lastSaturday + qSaturday
                        lastsunday = lastsunday + qsunday
                        lastAoverday = lastAoverday + qAoverday

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
                        'dnum = ds.Tables(0).Rows.Count + 2
                        dnum = ds.Tables(0).Rows.Count
                    End If
                    workshop.Add(0, "其他")
                    dnum = dnum + 3

                    Dim DataTable As New Table
                    DataTable.CellSpacing = 0
                    DataTable.CellPadding = 2
                    DataTable.GridLines = GridLines.Both
                    'DataTable.BorderWidth = New Unit(0)
                    DataTable.BorderColor = Color.Black
                    DataTable.Width = New Unit(1600)

                    tableConst = DataTable

                    DataTableRow(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, tableConst)


                    ds.Reset()
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

                    allsalary = 0
                    allattendenceday = 0
                    allpiece = 0
                    allald = 0

                    amountdays = 0
                    amounthourdays = 0
                    amountpieces = 0
                    amountduereward = 0
                    amountpeople = 0
                    amountOverday = 0
                    amountSaturday = 0
                    amountsunday = 0
                    amountAoverday = 0

                    qdays = 0
                    qhourdays = 0
                    qpieces = 0
                    qduereward = 0
                    qpeople = 0
                    qOverday = 0
                    qSaturday = 0
                    qAoverday = 0
                    qsunday = 0

                    wID = -1

                    woflag = False
                    wrow = True
                End If

                If reader("workshopID") <> wID Then

                    If amountsalary = 0 Then
                        allsalary = 0
                    Else
                        allsalary = Math.Round(amountsalary / people, 2)
                    End If

                    If totaldays = 0 Then
                        allattendenceday = 0
                    Else
                        allattendenceday = Math.Round(totaldays / people, 2)
                    End If

                    If woflag = True Then
                        If people = 0 Then
                            mixsalary = 0
                            mindays = 0
                        End If
                        If wrow = True Then
                            DataTableRow(1, reader("name"), workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), allsalary.ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), allattendenceday.ToString(), amountdays.ToString(), amounthourdays.ToString(), amountpieces.ToString(), amountOverday.ToString(), amountSaturday.ToString, amountduereward.ToString(), amountpeople.ToString(), "", amountsunday.ToString(), amountAoverday.ToString(), dnum, tableConst)
                            wrow = False
                        Else
                            DataTableRow(2, "", workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), allsalary.ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), allattendenceday.ToString(), amountdays.ToString(), amounthourdays.ToString(), amountpieces.ToString(), amountOverday.ToString(), amountSaturday.ToString, amountduereward.ToString(), amountpeople.ToString(), "", amountsunday.ToString(), amountAoverday.ToString(), 0, tableConst)
                        End If
                    End If

                    wID = reader("workshopID")


                    'Add the total  in same department
                    totalpiece = totalpiece + amountsalary
                    totalpeople = totalpeople + people

                    If maxs < maxsalary Then
                        maxs = maxsalary
                    End If

                    If mixsalary > 0 Then
                        If mins > mixsalary Then
                            mins = mixsalary
                        End If
                    End If

                    If maxd < maxdays Then
                        maxd = maxdays
                    End If

                    If mindays > 0 Then
                        If mind > mindays Then
                            mind = mindays
                        End If
                    End If

                    totald = totald + totaldays

                    'totalsalary = totalsalary + totald


                    qdays = qdays + amountdays
                    qhourdays = qhourdays + amounthourdays
                    qpieces = qpieces + amountpieces
                    qduereward = qduereward + amountduereward
                    qpeople = qpeople + amountpeople
                    qOverday = qOverday + amountOverday
                    qSaturday = qSaturday + amountSaturday
                    qsunday = qsunday + amountsunday
                    qAoverday = qAoverday + amountAoverday

                    amountsalary = 0
                    people = 0
                    maxsalary = 0
                    mixsalary = 1000000

                    totaldays = 0
                    maxdays = 0
                    mindays = 1000000

                    amountdays = 0
                    amounthourdays = 0
                    amountpieces = 0
                    amountduereward = 0
                    amountpeople = 0
                    amountOverday = 0
                    amountSaturday = 0
                    amountsunday = 0
                    amountAoverday = 0
                End If

                comparedays = reader("attendencedays")
                'For pieceSalary compared
                If comparedays >= attendencedays Then
                    If maxsalary < reader(0) + reader(5) Then
                        maxsalary = reader(0) + reader(5)
                    End If

                    If mixsalary > reader(0) + reader(5) Then
                        mixsalary = reader(0) + reader(5)
                    End If

                    amountsalary = amountsalary + reader(0) + reader(5)

                    If maxdays < reader(6) + reader(7) + reader(8) Then
                        maxdays = reader(6) + reader(7) + reader(8)
                    End If

                    If mindays > reader(6) + reader(7) + reader(8) Then
                        mindays = reader(6) + reader(7) + reader(8)
                    End If

                    totaldays = totaldays + reader(6) + reader(7) + reader(8)

                    people = people + 1
                End If
                amountdays = amountdays + reader(9)
                amounthourdays = amounthourdays + Math.Round(reader(10), 2)
                amountpieces = amountpieces + reader(6) + reader(7) + reader(8)
                amountduereward = amountduereward + reader(0) + reader(5)
                amountpeople = amountpeople + 1

                amountOverday = amountOverday + reader(12)
                amountSaturday = amountSaturday + reader(13)
                'apeople = apeople + 1

                'add by BaoXin in 20080909
                If reader(9) > attendencedays Then
                    middletemp = Math.Round((reader(9) - attendencedays) * reader(10) / reader(9), 2)
                Else
                    middletemp = 0
                End If
                amountsunday = amountsunday + middletemp
                If reader(10) > attendencedays Then
                    amountAoverday = amountAoverday + (reader(10) - attendencedays - middletemp)
                End If

                woflag = True
                deflag = True

            End While
            'last line ---------------------------------------
            If deflag = True Then
                totalpiece = totalpiece + amountsalary
                totalpeople = totalpeople + people

                piece = piece + totalpiece
                allpeople = allpeople + totalpeople

                If maxs < maxsalary Then
                    maxs = maxsalary
                End If

                If mixsalary > 0 Then
                    If mins > mixsalary Then
                        mins = mixsalary
                    End If
                End If

                If maxd < maxdays Then
                    maxd = maxdays
                End If

                If mindays > 0 Then
                    If mind > mindays Then
                        mind = mindays
                    End If
                End If
                totald = totald + totaldays

                If lasrMs < maxs Then
                    lasrMs = maxs
                End If

                If mins > 0 Then
                    If lasrIs > mins Then
                        lasrIs = mins
                    End If
                End If

                If lastMp < maxd Then
                    lastMp = maxd
                End If

                If mind > 0 Then
                    If lastIp > mind Then
                        lastIp = mind
                    End If
                End If

                totalsalary = totalsalary + totald


                If amountsalary = 0 Then
                    allsalary = 0
                Else
                    allsalary = Math.Round(amountsalary / people, 2)
                End If

                If totaldays = 0 Then
                    allattendenceday = 0
                Else
                    allattendenceday = Math.Round(totaldays / people, 2)
                End If

                If totalpiece = 0 Then
                    allpiece = 0
                Else
                    allpiece = Math.Round(totalpiece / totalpeople, 2)
                End If

                If totald = 0 Then
                    allald = 0
                Else
                    allald = Math.Round(totald / totalpeople, 2)
                End If

                qdays = qdays + amountdays
                qhourdays = qhourdays + amounthourdays
                qpieces = qpieces + amountpieces
                qduereward = qduereward + amountduereward
                qpeople = qpeople + amountpeople
                qOverday = qOverday + amountOverday
                qSaturday = qSaturday + amountSaturday
                qsunday = qsunday + amountsunday
                qAoverday = qAoverday + amountAoverday

                If people = 0 Then
                    mixsalary = 0
                    mindays = 0
                End If

                If totalpeople = 0 Then
                    mins = 0
                    mind = 0
                End If

                If allpeople = 0 Then
                    lasrIs = 0
                    lastIp = 0
                End If

                If wrow = True Then
                    DataTableRow(1, dname, workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), allsalary.ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), allattendenceday.ToString(), amountdays.ToString(), amounthourdays.ToString(), amountpieces.ToString(), amountOverday.ToString(), amountSaturday.ToString, amountduereward.ToString(), amountpeople.ToString(), "", amountsunday.ToString(), amountAoverday.ToString(), dnum, tableConst)
                Else
                    DataTableRow(2, "", workshop.Item(wID), maxsalary.ToString(), mixsalary.ToString(), allsalary.ToString(), amountsalary.ToString(), people.ToString(), maxdays.ToString(), mindays.ToString(), allattendenceday.ToString(), amountdays.ToString(), amounthourdays.ToString(), amountpieces.ToString(), amountOverday.ToString(), amountSaturday.ToString, amountduereward.ToString(), amountpeople.ToString(), "", amountsunday.ToString(), amountAoverday.ToString(), 0, tableConst)
                End If

                DataTableRow(2, "", "", "", "合计", "", totalpiece.ToString(), totalpeople.ToString(), "", "", "", qdays.ToString(), qhourdays.ToString(), qpieces.ToString(), qOverday.ToString, qSaturday.ToString, qduereward.ToString(), qpeople.ToString(), Math.Round(totalpeople / qpeople * 100, 2).ToString() & "%", qsunday.ToString, qAoverday.ToString, 1, tableConst)
                DataTableRow(2, "", "", maxs.ToString(), mins.ToString(), allpiece.ToString(), "", "", maxd.ToString(), mind.ToString(), allald.ToString(), "", "", "", "", "", "", "", "", "", "", 1, tableConst)
                reader.Close()

                Dim DataTable1 As New Table
                DataTable1.CellSpacing = 0
                DataTable1.CellPadding = 2
                DataTable1.GridLines = GridLines.Both
                'DataTable.BorderWidth = New Unit(0)
                DataTable1.BorderColor = Color.Black
                DataTable1.Width = New Unit(1840)
                tableConst = DataTable1

                lastdays = lastdays + qdays
                lasthourdays = lasthourdays + qhourdays
                lastpieces = lastpieces + qpieces
                lastduereward = lastduereward + qduereward
                lastpeople = lastpeople + qpeople
                lastOverday = lastOverday + qOverday
                lastSaturday = lastSaturday + qSaturday
                lastsunday = lastsunday + qsunday
                lastAoverday = lastAoverday + qAoverday

                Dim avgiS As Decimal = 0
                Dim avgiP As Decimal = 0
                If piece = 0 Then
                    avgiS = 0
                Else
                    avgiS = Math.Round(piece / allpeople, 2)
                End If
                If totalsalary = 0 Then
                    avgiP = 0
                Else
                    avgiP = Math.Round(totalsalary / allpeople, 2)
                End If


                DataTableRow(3, "", "总计", lasrMs, lasrIs, avgiS, piece, allpeople, lastMp, lastIp, avgiP, lastdays.ToString(), lasthourdays.ToString(), lastpieces.ToString(), lastOverday.ToString, lastSaturday.ToString, lastduereward.ToString(), lastpeople.ToString(), Math.Round(allpeople / lastpeople * 100, 2).ToString() & "%", lastsunday.ToString, lastAoverday.ToString, 0, tableConst)
            End If

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
        HeaderTable.Width = New Unit(1840)

        row = New TableRow
        row.BackColor = Color.White


        row.HorizontalAlign = HorizontalAlign.Center
        row.VerticalAlign = VerticalAlign.Middle

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Text &= "<b><font size=4>" & str & "年" & str1 & "月工资分析</font></b>&nbsp;<font size=4>应出勤 " & str2 & " 天</font><br>(实出勤>=应出勤)"
        cell.Width = New Unit(1840)
        row.Cells.Add(cell)

        HeaderTable.Rows.Add(row)
        ReportTablesHolder.Controls.Add(HeaderTable)
    End Sub

    Sub DataTableRow(ByVal ty As Integer, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String _
            , ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String _
            , ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal str15 As String, ByVal str16 As String, ByVal str17 As String, ByVal str18 As String, ByVal str19 As String, ByVal str20 As String, ByVal num As Integer, ByVal tableTemp As Table)
        If ty = 0 Then
            row = New TableRow
            row.BackColor = Color.White
            row.Width = New Unit(1840)
            row.VerticalAlign = VerticalAlign.Top

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
            cell.Text &= "<b>计件出勤</b>"
            cell.Width = New Unit(300)
            cell.ColumnSpan = 3
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>出勤天</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>小时天</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>计件出勤</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>加班天</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>双休天</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>应发金额</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>人数</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>>=应出勤人数占本部门总人数的%</b>"
            cell.Width = New Unit(120)
            cell.RowSpan = 2
            row.Cells.Add(cell)


            'added by BaoXin in 20080909
            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>考勤-双休天</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>考勤-加班天</b>"
            cell.Width = New Unit(120)
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

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总计</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总计</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总计</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总计</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总计</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总计</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总计</b>"
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text &= "<b>总计</b>"
            cell.Width = New Unit(120)
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
                'Dim DataTable As New Table
                'DataTable.CellSpacing = 0
                'DataTable.CellPadding = 2
                'DataTable.GridLines = GridLines.Both
                ''DataTable.BorderWidth = New Unit(0)
                'DataTable.BorderColor = Color.Black
                'DataTable.Width = New Unit(1200)

                row = New TableRow
                row.BackColor = Color.White
                row.Width = New Unit(1600)
                row.VerticalAlign = VerticalAlign.Top

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

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str11
                cell.Width = New Unit(120)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str12
                cell.Width = New Unit(120)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str13
                cell.Width = New Unit(120)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str14
                cell.Width = New Unit(120)
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
                cell.Width = New Unit(120)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str18
                cell.Width = New Unit(120)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str19
                cell.Width = New Unit(120)
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Text &= str20
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

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str11
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str12
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str13
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str14
                    cell.Width = New Unit(120)
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
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str18
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str19
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str20
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

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str11
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str12
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str13
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str14
                    cell.Width = New Unit(120)
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
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str18
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str19
                    cell.Width = New Unit(120)
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text &= str20
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
