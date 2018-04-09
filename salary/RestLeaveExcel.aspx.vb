'!*******************************************************************************!
'* @@ NAME				:	RestLeaveExcel.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for RestLeaveExcel.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	October 27 2008
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Drawing
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Wage


Namespace tcpc

Partial Class RestLeaveExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim StrSql As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
        Dim chk As New adamClass
        Dim hr As New HR

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            'Dim nRet As Integer
            'nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 100103077)
            'If nRet <= 0 Then
            '    Response.Redirect("/public/Message.aspx?type=" & nRet.ToString(), True)
            'End If
            DataGridLoad()

            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
        End If
    End Sub

    Sub DataGridLoad()
        Dim startdate As String
        Dim enddate As String

            'startdate = Request("ye") & "-" & "03-01"
            'enddate = DateAdd(DateInterval.Day, -1, CDate((Request("ye") + 1).ToString & "-" & "03-01")).ToShortDateString
            startdate = Request("ye") & "-" & "01-01"
            enddate = Request("ye") & "-" & "12-31"

        StrSql = " select u.userID,r.workdate,isnull(d.name,''),isnull(w.name,''),u.userno,u.username,u.enterdate,u.leavedate,isnull(s.systemcodename,''),isnull(s1.systemcodename,''),isnull(s2.systemcodename,''),isnull(r.workyear,0),isnull(sc.days,0),isnull(r.restday,0),isnull(r.number,0) "
        StrSql &= " from  tcpc0.dbo.Users u "
            'StrSql &= " left outer join (select usercode,workdate,workyear,restday,number From RestLeave  where workdate >='" & startdate & "' and workdate <='" & enddate & "')  r  on u.userID= r.usercode "
            StrSql &= " left outer join (select usercode,workdate,workyear,restday,number From hr_RestLeave  where workdate >='" & startdate & "' and workdate <='" & enddate & "')  r  on u.userID= r.usercode "
        StrSql &= " left outer join departments d on d.departmentID = u.departmentID"
        StrSql &= " left outer join workshop w on w.id=u.workshopID "
            'StrSql &= " left outer join (SELECT usercode,SUM(CASE WHEN (enddate is not null) THEN DATEDIFF([Day], startdate,enddate)+1 ELSE 0 END) - CASE WHEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')) > 0 THEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')+1) ELSE 0 END - CASE WHEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)) > 0 THEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)+1) ELSE 0 END AS days "
            StrSql &= " left outer join (SELECT userID,SUM(ISNULL(sickDays,0)) AS days "
            StrSql &= " FROM hr_Leave_mstr WHERE (startDate <= '" & enddate & "') AND (endDate >= '" & startdate & "') GROUP BY userID) as sc on sc.userID = u.userID "
        StrSql &= " left outer join tcpc0.dbo.systemcode s on s.systemcodeID=u.workTypeID "
        StrSql &= " left outer join tcpc0.dbo.systemcode s1 on s1.systemcodeID=u.employTypeID"
        StrSql &= " left outer join tcpc0.dbo.systemcode s2 on s2.systemcodeID=u.insuranceTypeID"
        StrSql &= " where u.deleted =0 and u.isActive=1 and u.plantCode='" & Session("PlantCode") & "' and (u.leavedate is null or u.leavedate >= '" & startdate & "') "
            'StrSql &= " and ( u.enterdate is not null and u.enterdate< case when month(u.enterdate)>=3 then '" & CDate(enddate).AddYears(-2) & "' else '" & CDate(enddate).AddYears(-1) & "' end)"
            StrSql &= " and ( u.enterdate is not null and u.enterdate<  '" & CDate(enddate).AddYears(-1) & "') "
            'StrSql &= " and u.userno='12' "
        StrSql &= "  order by u.userID ,r.workdate"

        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)

        HeaderTableProc()
        Dim usernum As String
        Dim remain As Decimal = 0
        Dim flag As Boolean = False
        Dim ss As String = ""
        Dim m As Integer = 3
        Dim n As Integer = 0
        Dim i As Integer
        Dim month As Integer
        Dim total As Decimal
        Dim amount As Decimal = 0
        Dim ldate As String = ""
        Dim export(10) As String
        Array.Clear(export, 0, 11)

        Dim sort As New Hashtable
        Dim workyear As Integer = 0
            Dim restday As Integer = 0

            Dim enterdate As DateTime
            While reader.Read()

                If usernum <> reader(0) Then
                    If flag = True Then
                        sort(n) = amount
                        sort(13) = total
                        export(10) = hr.Restday(enterdate, CDate(enddate))

                        DataTableRow(export(0), export(1), export(2), export(3), export(4), ldate, export(5), export(6), export(7), export(8), export(9), export(10), sort, (CDec(export(10)) - total).ToString)
                    End If
                    usernum = reader(0)
                    month = 0
                    amount = 0
                    total = 0

                    Array.Clear(export, 0, 11)

                    'default the value to hasttalbe
                    For i = 1 To 13
                        sort(i) = 0
                    Next

                    If reader(7).ToString = "" Then
                        ldate = ""
                    Else
                        ldate = reader(7).ToShortDateString()
                    End If
                End If

                '/* get the month in next year, it add to 12  (3 - 14) 
                If reader(1).ToString <> "" Then ' No restleave data but have restdays for yearholiday
                    n = CDate(reader(1)).Month
                    'If n < m Then
                    '    n = 12 + n
                    'End If

                    If month <> n Then
                        If flag = True Then
                            sort(month) = amount
                            amount = 0
                        End If
                        month = n
                    End If
                    export(8) = reader(11)
                    'export(10) = reader(13)
                    
                    'Else
                    ' workyear = DateDiff(DateInterval.Year, CDate(reader(6)), CDate(enddate))
                  

                End If
                workyear = DateDiff(DateInterval.Year, CDate(reader(6)), CDate(enddate))
                export(8) = workyear - 1
                If CDate(reader(6)).Month <= CDate(enddate).Month Then
                    restday = workyear - 1
                Else
                    restday = workyear - 2
                End If

                enterdate = CDate(reader(6))
                'Select Case restday
                '    Case Is <= 9
                '        export(10) = 5
                '    Case Is <= 19
                '        export(10) = 10
                '    Case Is >= 20
                '        export(10) = 15
                'End Select

                amount = amount + reader(14)
                total = total + reader(14)
                flag = True '/* the first data read not to do something

                '/* For last the port data  Array
                export(0) = reader(2)
                export(1) = reader(3)
                export(2) = reader(4)
                export(3) = reader(5)
                export(4) = reader(6)
                export(5) = reader(8)
                export(6) = reader(9)
                export(7) = reader(10)
                export(9) = reader(12)

            End While
        If flag = True Then
            sort(n) = amount
                sort(13) = total
                export(10) = hr.Restday(enterdate, CDate(enddate))
            DataTableRow(export(0), export(1), export(2), export(3), export(4), ldate, export(5), export(6), export(7), export(8), export(9), export(10), sort, (CDec(export(10)) - total).ToString)
        End If
        sort.Clear()
        Array.Clear(export, 0, 11)
        reader.Close()


    End Sub


   

    Sub HeaderTableProc()
        Dim HeaderTable As New Table
        HeaderTable.CellSpacing = 0
        HeaderTable.CellPadding = 1
        HeaderTable.GridLines = GridLines.Both
        HeaderTable.BorderWidth = New Unit(0)
        HeaderTable.BorderColor = Color.Black
        HeaderTable.Width = New Unit(1830)


        row = New TableRow
        row.BackColor = Color.White
        row.VerticalAlign = VerticalAlign.Top

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(150)
        cell.RowSpan = 2
        cell.Text = "<b>部门</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(100)
        cell.RowSpan = 2
        cell.Text = "<b>工段</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(80)
        cell.RowSpan = 2
        cell.Text = "<b>工号</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(80)
        cell.RowSpan = 2
        cell.Text = "<b>姓名</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(80)
        cell.RowSpan = 2
        cell.Text = "<b>入公司日期</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(80)
        cell.RowSpan = 2
        cell.Text = "<b>离职日期</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(80)
        cell.RowSpan = 2
        cell.Text = "<b>计酬方式</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(80)
        cell.RowSpan = 2
        cell.Text = "<b>用工性质</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(80)
        cell.RowSpan = 2
        cell.Text = "<b>保险类型</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(60)
        cell.RowSpan = 2
        cell.Text = "<b>工龄</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(60)
        cell.RowSpan = 2
        cell.Text = "<b>病假</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(80)
        cell.RowSpan = 2
        cell.Text = "<b>应休年休</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(520)
        cell.ColumnSpan = 13
        cell.Text = "<b>已享受</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.RowSpan = 2
        cell.Text = "<b>剩余</b>"
        row.Cells.Add(cell)

        HeaderTable.Rows.Add(row)

        row = New TableRow
        row.BackColor = Color.White
        row.VerticalAlign = VerticalAlign.Top

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Right
            cell.VerticalAlign = VerticalAlign.Top
            cell.Width = New Unit(40)
            cell.Text = "<b>1月</b>"
            row.Cells.Add(cell)

            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Right
            cell.VerticalAlign = VerticalAlign.Top
            cell.Width = New Unit(40)
            cell.Text = "<b>2月</b>"
            row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>3月</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>4月</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>5月</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>6月</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>7月</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>8月</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>9月</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>10月</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>11月</b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>12月</b>"
        row.Cells.Add(cell)

     

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.VerticalAlign = VerticalAlign.Top
        cell.Width = New Unit(40)
        cell.Text = "<b>总计</b>"
        row.Cells.Add(cell)

        HeaderTable.Rows.Add(row)
        ReportTablesHolder.Controls.Add(HeaderTable)
    End Sub

    Sub DataTableRow(ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, _
                     ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, ByVal str12 As String, ByVal str13 As Hashtable, ByVal str14 As String)
        Dim tTable As New Table
        tTable.CellSpacing = 0
        tTable.CellPadding = 1
        tTable.GridLines = GridLines.Both
        tTable.BorderWidth = New Unit(0)
        tTable.BorderColor = Color.Black
        tTable.Width = New Unit(1830)

        row = New TableRow
        row.BackColor = Color.White

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(150)
        cell.Text = str1
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(100)
        cell.Text = str2
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(80)
        cell.Text = str3
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(80)
        cell.Text = str4
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(80)
        cell.Text = str5
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(80)
        cell.Text = str6
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(80)
        cell.Text = str7
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(80)
        cell.Text = str8
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(80)
        cell.Text = str9
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(60)
        cell.Text = str10
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(60)
        cell.Text = str11
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(80)
        cell.Text = str12
        row.Cells.Add(cell)

        Dim ind As Integer
            For ind = 1 To 13
                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Width = New Unit(40)
                cell.Text = str13(ind)
                row.Cells.Add(cell)
            Next

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Width = New Unit(40)
        cell.Text = str14
        row.Cells.Add(cell)

        tTable.Rows.Add(row)
        ReportTablesHolder.Controls.Add(tTable)
        End Sub


        Function resday(ByVal enterdate As DateTime, ByVal startdate As DateTime, ByVal enddate As DateTime) As Decimal


            Return 0
        End Function

End Class

End Namespace
