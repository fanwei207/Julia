'* @@ NAME				:	attendanceExcelDouble.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for attendanceExcelDouble.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	December 28 2005
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class attendanceExcelDouble
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim strSql As String
    Dim reader As SqlDataReader

    Dim chk As New adamClass
    Dim row As TableRow
    Dim cell As TableCell
    Dim i As Integer

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            Dim yr As String = Request("yr")
            Dim ye As String = Request("ye")
            Dim reduceValue As String = Request("red")
            Dim lastyr As String
            Dim lastye As String

            Dim prewyr As String
            Dim prewye As String

            If CInt(ye) <= 2 Then
                prewyr = (CInt(yr) - 1).ToString()
                If ye = "2" Then
                    prewye = "12"
                Else
                    prewye = "11"
                End If
            Else
                prewyr = yr
                prewye = (CInt(ye) - 2).ToString()
            End If

            'between last year and the first month in this year 
            If ye = "1" Then
                lastyr = (CInt(yr) - 1).ToString()
                lastye = "12"
            Else
                lastyr = yr
                lastye = (CInt(ye) - 1).ToString()
            End If

            Dim ptable As String
            Dim lastptable As String
            Dim prewptable As String
            Dim dd As String = Request("yr") & "-" & Request("ye") & "-01"
            Dim pp As String = lastyr & "-" & lastye & "-01"
            Dim qq As String = prewyr & "-" & prewye & "-01"

            If DateDiff(DateInterval.Month, CDate(qq), DateTime.Now) > 3 Then
                'prewptable = "PieceAttendence_his"
                prewptable = "PieceAttendence"
            Else
                prewptable = "PieceAttendence"
            End If

            If DateDiff(DateInterval.Month, CDate(pp), DateTime.Now) > 3 Then
                'lastptable = "PieceAttendence_his"
                lastptable = "PieceAttendence"
            Else
                lastptable = "PieceAttendence"
            End If

            If DateDiff(DateInterval.Month, CDate(dd), DateTime.Now) > 3 Then
                'ptable = "PieceAttendence_his"
                ptable = "PieceAttendence"
            Else
                ptable = "PieceAttendence"
            End If

            Dim ds As DataSet
            Dim leavedate As String


            strSql = " select u.userNo,u.username,isnull(d.name,''),isnull(w.name,''),isnull(Att.hour,0),isnull(Att.day,0),isnull(Att.hour1,0),isnull(Att.day1,0),isnull(Att.hour2,0),isnull(Att.day2,0),u.enterDate,u.leaveDate "
            strSql &= " From "
            'Workdays and workhours in this Month  and workhours<workhours
            strSql &= " ( Select CASE WHEN (Month.usercode IS NULL) THEN CASE WHEN (lastMonth.usercode IS NULL)THEN PrewMonth.usercode ELSE lastMonth.usercode END ELSE Month.usercode END AS UID,isnull(Month.workhours,0) as hour,isnull(Month.workdays,0) as day,isnull(lastMonth.workhours,0) as hour1,isnull(lastMonth.workdays,0) as day1,isnull(PrewMonth.workhours,0) as hour2,isnull(PrewMonth.workdays,0) as day2 From "

            strSql &= " (Select rc.usercode,ISNULL(rc.allhours, 0) + ISNULL(rc.overdays, 0) + ISNULL(rc.extramonthupdays, 0) + ISNULL(rc.extraupdays, 0) + ISNULL(rc.extramonthdowndays, 0) + ISNULL(rc.extradowndays, 0)+ISNULL(rc.upspecialdays, 0)+ISNULL(rc.downspecialdays, 0) as workhours,isnull(pa.totalhours,0)as workdays From ReadyCaculateSalary rc left outer join "
            strSql &= " (select usercode,sum(totalhours)/8 as totalhours From " & ptable & " where month(workdate)='" & ye & "' and year(workdate)='" & yr & "' Group by usercode ) as pa  on pa.usercode=rc.usercode  where month(rc.salarydate)='" & ye & "' and year(rc.salarydate)='" & yr & "' ) as Month"
            'strSql &= " INNER JOIN "
            strSql &= " FULL OUTER JOIN "
            'Workdays and workhours in last Month 
            strSql &= " (Select r.usercode,ISNULL(r.allhours, 0) + ISNULL(r.overdays, 0) + ISNULL(r.extramonthupdays, 0) + ISNULL(r.extraupdays, 0) + ISNULL(r.extramonthdowndays, 0) + ISNULL(r.extradowndays, 0)+ISNULL(r.upspecialdays, 0)+ISNULL(r.downspecialdays, 0) as workhours,isnull(pt.totalhours,0)as workdays From ReadyCaculateSalary r left outer join "
            strSql &= " (select usercode,sum(totalhours)/8 as totalhours From " & lastptable & " where month(workdate)='" & lastye & "' and year(workdate)='" & lastyr & "' Group by usercode ) as pt  on pt.usercode=r.usercode  where month(r.salarydate)='" & lastye & "' and year(r.salarydate)='" & lastyr & "' ) as lastMonth on lastMonth.usercode=Month.usercode "
            'Workdays and workhours in the Month befor last Month 
            strSql &= " Full OUTER JOIN "
            strSql &= " (Select pr.usercode,ISNULL(pr.allhours, 0) + ISNULL(pr.overdays, 0) + ISNULL(pr.extramonthupdays, 0) + ISNULL(pr.extraupdays, 0) + ISNULL(pr.extramonthdowndays, 0) + ISNULL(pr.extradowndays, 0)+ISNULL(pr.upspecialdays, 0)+ISNULL(pr.downspecialdays, 0) as workhours,isnull(pw.totalhours,0)as workdays From ReadyCaculateSalary pr left outer join "
            strSql &= " (select usercode,sum(totalhours)/8 as totalhours From " & prewptable & " where month(workdate)='" & prewye & "' and year(workdate)='" & prewyr & "' Group by usercode ) as pw  on pw.usercode=pr.usercode  where month(pr.salarydate)='" & prewye & "' and year(pr.salarydate)='" & prewyr & "' ) as PrewMonth on PrewMonth.usercode=lastMonth.usercode ) as Att"

            'strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = Att.UID and u.isTemp='" & Session("temp") & "'"
            strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = Att.UID "
            strSql &= " left outer join departments d on d.departmentID=u.departmentID "
            strSql &= " left outer join workshop w on w.id=u.workshopID "

            strSql &= " Where (isnull(Att.hour,0)-isnull(Att.day,0)<'" & reduceValue & "') or (isnull(Att.hour1,0)-isnull(Att.day1,0)<'" & reduceValue & "') or (isnull(Att.hour2,0)-isnull(Att.day2,0)<'" & reduceValue & "')"
            strSql &= " Order by u.userID "
            'Response.Write(strSql)
            'Exit Sub
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            PIMasteryRow(ye, lastye, prewye, "", "", "", "", "", "", "", "", "", "", "", "", 0)
            While reader.Read
                If reader(11).ToString() = "" Then
                    leavedate = ""
                Else
                    leavedate = reader(11)
                End If
                PIMasteryRow(reader(0), reader(1), reader(2), reader(3), reader(4), reader(5), reader(4) - reader(5), reader(6), reader(7), reader(6) - reader(7), reader(8), reader(9), reader(8) - reader(9), reader(10), leavedate, 1)
            End While
            reader.Close()


            Response.Clear()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312")
            Response.ContentType = "application/vnd.ms-excel"

            Response.Flush()

        End If
    End Sub
    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal temp As Integer)
        row = New TableRow
        row.HorizontalAlign = HorizontalAlign.Right
        row.BorderWidth = New Unit(0)
        If temp = 0 Then
            cell = New TableCell
            cell.Text = "<b>工号</b>"
            cell.Width = New Unit(80)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>姓名</b>"
            cell.Width = New Unit(80)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>部门</b>"
            cell.Width = New Unit(180)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>工段</b>"
            cell.Width = New Unit(120)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>" & str0.Trim() & "月</b>"
            cell.Width = New Unit(280)
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.ColumnSpan = 3
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = " <b>" & str1.Trim() & "月</b>"
            cell.Width = New Unit(280)
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.ColumnSpan = 3
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = " <b>" & str2.Trim() & "月</b>"
            cell.Width = New Unit(280)
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.ColumnSpan = 3
            row.Cells.Add(cell)


            cell = New TableCell
            cell.Text = "<b>入公司日期</b>"
            cell.Width = New Unit(100)
            cell.RowSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>离职日期</b>"
            cell.RowSpan = 2
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            exl.Rows.Add(row)
            'the second row
            row = New TableRow
            cell = New TableCell
            cell.Text = "<b>完成天数</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>小时天</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>差值</b>"
            cell.Width = New Unit(80)
            row.Cells.Add(cell)
            'last month
            cell = New TableCell
            cell.Text = "<b>完成天数</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>小时天</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>差值</b>"
            cell.Width = New Unit(80)
            row.Cells.Add(cell)
            'prew month
            cell = New TableCell
            cell.Text = "<b>完成天数</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>小时天</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>差值</b>"
            cell.Width = New Unit(80)
            row.Cells.Add(cell)
        Else
            cell = New TableCell
            cell.Text = str0.Trim()
            cell.Width = New Unit(80)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str1.Trim()
            cell.Width = New Unit(80)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str2.Trim()
            cell.Width = New Unit(180)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str3.Trim()
            cell.Width = New Unit(120)
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
            cell.Width = New Unit(80)
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
            cell.Width = New Unit(80)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str10.Trim()
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str11.Trim()
            cell.Width = New Unit(100)
            row.Cells.Add(cell)


            cell = New TableCell
            cell.Text = str12.Trim()
            cell.Width = New Unit(80)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str13.Trim()
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str14.Trim()
            cell.Width = New Unit(100)
            row.Cells.Add(cell)
        End If
        exl.Rows.Add(row)
    End Sub
End Class

End Namespace

