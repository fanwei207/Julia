Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc

    Partial Class wl_exportcalendar
        Inherits System.Web.UI.Page

        Dim StrSql As String
        Dim StrSql2 As String
        Dim reader As SqlDataReader
        Dim row As TableRow
        Dim cell As TableCell
        Public chk As New adamClass
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            Dim Str As String = ""

            PIHeaderRow("车间考勤  导出日期:" & Today.ToString())

            If Request("a") = 1 Then

                Str = "60^<b>成本中心</b>~^50^<b>工号</b>~^70^<b>姓名</b>~^60^<b>员工类型</b>~^70^<b>考勤机号</b>~^110^<b>时间</b>~^60^<b>考勤类型</b>~^60^<b>输入者</b>~^<b>输入日期</b>~^<b>录入</b>~^<b>剔除</b>~^"
                PIMasteryRow(Str, True)

                StrSql = "SELECT a.AttendanceUserCenterName,a.AttendanceUserCode,a.AttendanceUserName,a.AttendanceUserType,a.AttendanceUserNo,a.checkTime,a.checktype,a.ImportedBy,a.ImportedDate,a.isManual,a.isDisable "

                StrSql &= " FROM tcpc0.dbo.hr_AttendanceInfo a "

                StrSql &= " where a.checkTime>='" & Request("dd1") & "' and a.checkTime< DateAdd(dd,1,'" & Request("dd2") & "')"
                StrSql &= " and a.plantID='" & Request("pl") & "'"

                If Convert.ToInt32(Request("cc")) > 0 Then
                    StrSql &= " and a.AttendanceUserCenter='" & Request("cc") & "'"
                End If

                If Request("co") <> Nothing Then
                    StrSql &= " and a.AttendanceUserCode='" & Request("co") & "'"
                End If
                StrSql &= " order by a.AttendanceUserNo,a.checkTime "

                reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql)
                While (reader.Read())
                    Str = reader(0).ToString() & "~^"
                    Str &= reader(1).ToString() & "~^"
                    Str &= reader(2).ToString() & "~^"
                    Str &= reader(3).ToString() & "~^"
                    Str &= reader(4).ToString() & "~^"
                    Str &= reader(5).ToString() & "~^"
                    Str &= reader(6).ToString() & "~^"
                    Str &= reader(7).ToString() & "~^"
                    Str &= reader(8).ToString() & "~^"
                    Str &= reader(9).ToString() & "~^"
                    Str &= reader(10).ToString() & "~^"

                    PIMasteryRow(Str)

                End While
                reader.Close()
            ElseIf Request("a") = 2 Then
                Str = "60^<b>成本中心</b>~^50^<b>工号</b>~^70^<b>姓名</b>~^120^<b>所在部门</b>~^60^<b>员工类型</b>~^100^<b>工段</b>~^100^<b>班组</b>~^100^<b>工种</b>~^70^<b>考勤机号</b>~^110^<b>上班时间</b>~^110^<b>下班时间</b>~^60^<b>考勤工时</b>~^110^<b>刷新时间</b>~^<b>中夜班</b>~^80^<b>考勤小时2</b>~^100^<b>实际上班时间</b>~^100^<b>实际下班时间</b>~^"
                PIMasteryRow(Str, True)

                StrSql = " select a.userno,a.username,d.name+'(' + d.code + ')',case when a.usertype=394 then 'A' when a.usertype=395 then 'B' when a.usertype=396 then 'C' when a.usertype=397 then 'D' when a.usertype=398 then 'E' end ,ws.name,wh.name,wk.name,a.fingerprint,a.starttime,a.endtime,a.totalhr,a.createddate,ISNUll(q.night,''),"
                'StrSql &= "totalhr2 = Case when ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2) >=12 then ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)-1.5 when ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)>=5 then ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)-1 else ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2) end,"
                StrSql &= "totalhr2 = isnull(a.totalhr,0.0) + isnull(a.totalhrOver,0.0),"
                StrSql &= "a.starttimeOri, a.endtimeOri  "
                If Request("his") Is Nothing Then
                    If Request("b") = "1" Then
                        StrSql &= " from tcpc0.dbo.hr_Attendance_calendar_ori a "
                    Else
                        StrSql &= " from tcpc0.dbo.hr_Attendance_calendar a "
                    End If
                Else
                    StrSql &= " FROM tcpc0.dbo.hr_Attendance_calendar_his a "
                End If
                StrSql &= " Inner Join tcpc0.dbo.users u On u.userid = a.userid "
                StrSql &= " Inner Join departments d On u.departmentid = d.departmentid "
                StrSql &= " LEFT OUTER JOIN (SELECT userID, "
                StrSql &= "                 CASE WHEN (totalhr>=12 AND h1<=22 AND h2>=30) THEN N'全夜' "
                StrSql &= "                 ELSE "
                StrSql &= "                 CASE WHEN (totalhr>=8 AND ((h1<=3 AND h1>=0) or h2 >24 )) THEN N'夜班' "
                StrSql &= "                 ELSE "
                StrSql &= "                 CASE WHEN (totalhr>=8 AND ( h2 <=24 And  h2 >=22)) THEN N'中班' "
                StrSql &= "                 ELSE NULL  "
                StrSql &= "                 END  "
                StrSql &= "                 END   "
                StrSql &= "                 END As night,starttime    "
                StrSql &= "                 FROM  "
                StrSql &= "                 (SELECT  userID,totalhr,datepart(hh,starttime)+ ROUND(datepart(mi,starttime)/60.0,2) As h1, "
                StrSql &= "                  CASE WHEN datediff(day,starttime,endtime)<> 0 THEN datepart(hh,endtime) +ROUND(datepart(mi,endtime)/60.0,2)+ 24 ELSE datepart(hh,endtime) + Round(datepart(mi,endtime)/60.0,2) END As h2, "
                StrSql &= "                 starttime "
                If Request("his") Is Nothing Then
                    If Request("b") = "1" Then
                        StrSql &= " from tcpc0.dbo.hr_Attendance_calendar_ori a "
                    Else
                        StrSql &= " from tcpc0.dbo.hr_Attendance_calendar a "
                    End If
                Else
                    StrSql &= " FROM tcpc0.dbo.hr_Attendance_calendar_his  "
                End If
                StrSql &= "   WHERE uyear='" & Request("yy") & "' AND umonth='" & Request("mm") & "' AND plantid='" & Request("pl") & "'"

                If Convert.ToInt32(Request("cc")) > 0 Then

                    StrSql &= " AND cc ='" & Request("cc") & "' "
                End If

                StrSql += ") As qq   )   as q oN q.userID = a.userID And  q.starttime = a.starttime "
                StrSql &= " Left Join workshop ws on ws.id = u.workshopID "
                StrSql &= " Left Join workshop wh on wh.id = u.workprocedureID "
                StrSql &= " Left Join workkinds wk on wk.id = u.kindswork "
                StrSql &= " where a.plantID='" & Request("pl") & "'"

                If Convert.ToInt32(Request("cc")) > 0 Then

                    StrSql &= " and a.cc='" & Request("cc") & "' "
                End If

                StrSql &= "and year(a.starttime)='" & Request("yy") & "' and Month(a.starttime)='" & Request("mm") & "' "
                If Request("day") <> Nothing Then
                    StrSql &= " and Day(a.starttime)='" & Request("day") & "' "
                End If
                If Request("co") <> Nothing Then
                    StrSql &= " AND a.userno = '" & Request("co") & "'"
                End If

                StrSql &= " order by a.userno,a.starttime "

                'Response.Write(StrSql)
                'Exit Sub

                reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql)
                While (reader.Read())
                    Str = Request("cc") & "~^"
                    Str &= reader(0).ToString() & "~^"
                    Str &= reader(1).ToString() & "~^"
                    Str &= reader(2).ToString() & "~^"
                    Str &= reader(3).ToString() & "~^"
                    Str &= reader(4).ToString() & "~^"
                    Str &= reader(5).ToString() & "~^"
                    Str &= reader(6).ToString() & "~^"
                    Str &= reader(7).ToString() & "~^"
                    Str &= reader(8).ToString() & "~^"
                    Str &= reader(9).ToString() & "~^"
                    Str &= reader(10).ToString() & "~^"
                    Str &= reader(11).ToString() & "~^"
                    Str &= reader(12).ToString() & "~^"
                    Str &= reader(13).ToString() & "~^"
                    Str &= reader(14).ToString() & "~^"
                    Str &= reader(15).ToString() & "~^"
                    PIMasteryRow(Str)

                End While
                reader.Close()
            ElseIf Request("a") = 3 Then
                Str = "60^<b>成本中心</b>~^50^<b>工号</b>~^70^<b>姓名</b>~^120^<b>所在部门</b>~^60^<b>员工类型</b>~^100^<b>工段</b>~^100^<b>班组</b>~^100^<b>工种</b>~^60^<b>考勤工时</b>~^<b>中班</b>~^<b>夜班</b>~^<b>全夜</b>~^80^<b>考勤小时2</b>~^100^<b>实际上班时间</b>~^100^<b>实际下班时间</b>~^"
                PIMasteryRow(Str, True)
                StrSql = " select a.userno,a.username,d.name + '(' + d.code + ')', a.atype ,ws.name,wh.name,wk.name,a.totalhr,ISNULL(n.mid,0),ISNULL(n.night,0),ISNULL(n.whole,0),a.totalhr2,a.starttimeOri, a.endtimeOri  from "
                StrSql &= " (select a.userID,a.usertype,a.userno,a.username,case when a.usertype=394 then 'A' when a.usertype=395 then 'B' when a.usertype=396 then 'C' when a.usertype=397 then 'D' when a.usertype=398 then 'E' end  as atype ,sum(a.totalhr) as totalhr,"
                'StrSql &= " sum(Case when ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2) >=12 then ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)-1.5 when ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)>=5 then ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2)-1 else ROUND(DATEDIFF(MI,a.starttime,a.endtime)/60.0,2) end) AS totalhr2,"
                StrSql &= " isnull(a.totalhr,0.0) + isnull(a.totalhrOver,0.0) AS totalhr2,"
                StrSql &= " a.starttimeOri, a.endtimeOri "
                If Request("his") Is Nothing Then
                    If Request("b") = "1" Then
                        StrSql &= " from tcpc0.dbo.hr_Attendance_calendar_ori a "
                    Else
                        StrSql &= " from tcpc0.dbo.hr_Attendance_calendar a "
                    End If
                Else
                    StrSql &= " FROM tcpc0.dbo.hr_Attendance_calendar_his a "
                End If
                StrSql &= " where a.plantID='" + Request("pl") + "'"

                If Convert.ToInt32(Request("cc")) > 0 Then

                    StrSql &= "' and a.cc='" & Request("cc") & "' "
                End If

                StrSql &= " and year(a.starttime)='" & Request("yy") & "' and Month(a.starttime)='" & Request("mm") & "' "
                If Request("day") <> Nothing Then
                    StrSql &= " and Day(a.starttime)='" & Request("day") & "' "
                End If
                If Request("co") <> Nothing Then
                    StrSql &= " AND a.userno = '" & Request("co") & "'"
                End If

                StrSql &= " group by a.userid,a.userno,a.username,a.usertype ) As a "
                StrSql &= " Inner Join tcpc0.dbo.users u On u.userid = a.userid "
                StrSql &= " Inner Join departments d On u.departmentid = d.departmentid "
                StrSql &= " LEFT OUTER JOIN ( SELECT userID ,usertype,SUM(CAST(SubString(night,1,1) As Int)) As mid,SUM(CAST(SubString(night,2,1) As Int)) As night, SUM(CAST(SubString(night,3,1) As Int))  As whole  "
                StrSql &= "                  FROM ( SELECT userID, usertype,CASE WHEN Max(CAST(qq.night As int)) =0  THEN '000' ELSE CASE WHEN Max(CAST(qq.night As int)) =1 THEN '001' ELSE CASE WHEN Max(CAST(qq.night As int)) =10 THEN '010' ELSE '100' END END END As night  "
                StrSql &= "                         FROM( SELECT userID,usertype,CASE WHEN (totalhr>=12 AND h1<=22 AND h2>=30) THEN '001' ELSE CASE WHEN (totalhr>=8 AND ((h1<=3 AND h1>=0) or h2 >24 )) THEN '010' ELSE CASE WHEN (totalhr>=8 AND ( h2 <=24 And  h2 >=22)) THEN '100' ELSE '000' END END END As night,workday FROM  "
                StrSql &= "                               (SELECT  userID,totalhr,usertype,datepart(hh,starttime)+ ROUND(datepart(mi,starttime)/60.0,2) As h1,CASE WHEN datepart(hh,endtime) + ROUND(datepart(mi,starttime)/60.0,2)<= datepart(hh,starttime) + ROUND(datepart(mi,starttime)/60.0,2) THEN datepart(hh,endtime) +ROUND(datepart(mi,endtime)/60.0,2)+ 24 ELSE datepart(hh,endtime) + Round(datepart(mi,endtime)/60.0,2) END As h2,CONVERT(varchar(10), starttime, 120) As workday "
                If Request("his") Is Nothing Then
                    If Request("b") = "1" Then
                        StrSql &= " from tcpc0.dbo.hr_Attendance_calendar_ori a "
                    Else
                        StrSql &= " from tcpc0.dbo.hr_Attendance_calendar a "
                    End If
                Else
                    StrSql &= " FROM tcpc0.dbo.hr_Attendance_calendar_his  "
                End If
                StrSql &= "  WHERE uyear='" & Request("yy") & "' AND umonth= '" & Request("mm") & "' AND plantid='" & Request("pl") & "' "

                If Convert.ToInt32(Request("cc")) > 0 Then

                    StrSql &= "' AND cc='" & Request("cc") & "'"
                End If

                StrSql &= "  ) q ) qq GROUP BY userID,workday,usertype ) qqq  GrOUP BY userID, usertype )  as n  ON n.userID =a.userID And n.usertype =a.usertype "
                StrSql &= " Left Join workshop ws on ws.id = u.workshopID "
                StrSql &= " Left Join workshop wh on wh.id = u.workprocedureID "
                StrSql &= " Left Join workkinds wk on wk.id = u.kindswork "
                StrSql &= " order by a.userno "

                'Response.Write(StrSql)
                'Exit Sub

                reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql)
                While (reader.Read())
                    Str = Request("cc") & "~^"
                    Str &= reader(0).ToString() & "~^"
                    Str &= reader(1).ToString() & "~^"
                    Str &= reader(2).ToString() & "~^"
                    Str &= reader(3).ToString() & "~^"
                    Str &= reader(4).ToString() & "~^"
                    Str &= reader(5).ToString() & "~^"
                    Str &= reader(6).ToString() & "~^"
                    Str &= reader(7).ToString() & "~^"
                    Str &= reader(8).ToString() & "~^"
                    Str &= reader(9).ToString() & "~^"
                    Str &= reader(10).ToString() & "~^"
                    Str &= reader(11).ToString() & "~^"
                    Str &= reader(12).ToString() & "~^"
                    Str &= reader(13).ToString() & "~^"
                    PIMasteryRow(Str)

                End While
                reader.Close()
            ElseIf Request("a") = 4 Then
                Str = "60^<b>成本中心</b>~^50^<b>工号</b>~^70^<b>姓名</b>~^60^<b>员工类型</b>~^120^<b>上班时间</b>~^120^<b>下班时间</b>~^60^<b>考勤工时</b>~^"
                PIMasteryRow(Str, True)

                StrSql = "SELECT    a.cc,  a.userno, a.username, a.usertype,  a.starttime, a.endtime, a.totalhr "
                If Request("his") Is Nothing Then
                    StrSql &= " FROM tcpc0.dbo.hr_Attendance_calendar a "
                Else
                    StrSql &= " FROM tcpc0.dbo.hr_Attendance_calendar_his  a "
                End If
                'StrSql &= " FROM  tcpc0.dbo.hr_Attendance_calendar AS a  "

                StrSql &= " INNER JOIN (SELECT plantid, userid, YEAR(starttime) AS y, MONTH(starttime) AS m, DAY(starttime) AS d"
                If Request("his") Is Nothing Then
                    StrSql &= " FROM tcpc0.dbo.hr_Attendance_calendar  "
                Else
                    StrSql &= " FROM tcpc0.dbo.hr_Attendance_calendar_his  "
                End If
                StrSql &= "  GROUP BY plantid, userid, YEAR(starttime), MONTH(starttime), DAY(starttime)"
                StrSql &= " HAVING  (COUNT(userid) > 1)) AS b ON a.plantid = b.plantid AND a.userid = b.userid AND YEAR(a.starttime) = b.y AND MONTH(a.starttime) = b.m AND Day(a.starttime) = b.d "
                StrSql &= " WHERE a.plantid='" & Request("pl") & "'  "
                If Request("co") <> Nothing Then
                    StrSql &= " and a.userno='" & Request("co") & "'"
                End If
                StrSql &= " and year(a.starttime)='" & Request("yy") & "' and Month(a.starttime)='" & Request("mm") & "' "
                StrSql &= " ORDER BY a.plantid, a.userid, a.starttime "

                'Response.Write(StrSql)
                'Exit Sub

                reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql)
                While (reader.Read())
                    Str = reader(0).ToString() & "~^"
                    Str &= reader(1).ToString() & "~^"
                    Str &= reader(2).ToString() & "~^"
                    Str &= reader(3).ToString() & "~^"
                    Str &= Format(reader(4), "yyyy-MM-dd HH:mm:ss") & "~^"
                    Str &= Format(reader(5), "yyyy-MM-dd HH:mm:ss") & "~^"
                    Str &= reader(6).ToString() & "~^"

                    PIMasteryRow(Str)

                End While
                reader.Close()
            End If


            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=workshopcalendar.xls")
            Response.Flush()
        End Sub

        Sub PIMasteryRow(ByVal str As String, Optional ByVal isTitle As Boolean = False)
            row = New TableRow
            If isTitle Then
                row.BackColor = Color.LightGray
                row.HorizontalAlign = HorizontalAlign.Center
            Else
                row.BackColor = Color.White
                row.HorizontalAlign = HorizontalAlign.Left
            End If
            row.VerticalAlign = VerticalAlign.Top
            row.Font.Size = FontUnit.Point(9)

            row.BorderWidth = New Unit(0)

            Dim i As Integer = 0
            Dim ind As Integer

            If isTitle Then
                Dim wid As Integer
                For i = 0 To 200
                    wid = 100
                    cell = New TableCell
                    ind = str.IndexOf("~^")
                    If (ind = -1) Then
                        ind = str.IndexOf("L~")
                        If (ind > -1) Then
                            cell.HorizontalAlign = HorizontalAlign.Left
                            str = str.Substring(2)
                        End If

                        ind = str.IndexOf("^")
                        If (ind = -1) Then
                            cell.Text = str
                        Else
                            wid = Convert.ToInt32(str.Substring(0, ind))
                            cell.Text = str.Substring(ind + 1)
                        End If
                        str = ""
                        cell.Width = New Unit(wid)
                        row.Cells.Add(cell)
                        Exit For
                    Else
                        cell.Text = str.Substring(0, ind)
                        str = str.Substring(ind + 2)

                        ind = cell.Text.IndexOf("L~")
                        If (ind > -1) Then
                            cell.HorizontalAlign = HorizontalAlign.Left
                            cell.Text = cell.Text.Substring(2)
                        End If

                        ind = cell.Text.IndexOf("^")
                        If (ind > -1) Then
                            wid = Convert.ToInt32(cell.Text.Substring(0, ind))
                            cell.Text = cell.Text.Substring(ind + 1)
                        End If
                    End If
                    cell.Width = New Unit(wid)
                    row.Cells.Add(cell)
                Next

                If i < 50 Then
                    For ind = i To 50
                        cell = New TableCell
                        cell.Text = ""
                        cell.Width = New Unit(60)
                        row.Cells.Add(cell)
                    Next
                End If
            Else
                For i = 0 To 200
                    cell = New TableCell
                    ind = str.IndexOf("~^")
                    If (ind = -1) Then
                        cell.Text = str
                        If IsNumeric(cell.Text) Then
                            If cell.Text.Trim.Length > 6 Then
                                If cell.Text.Trim.IndexOf(".") = -1 Then
                                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                                End If
                            End If
                            cell.HorizontalAlign = HorizontalAlign.Right
                        ElseIf IsDate(cell.Text) Then
                            If CDate(cell.Text) = CDate("1900-01-01") Then
                                cell.Text = ""
                            Else
                                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd HH:mm")
                            End If
                        End If
                        str = ""
                        row.Cells.Add(cell)
                        Exit For
                    Else
                        cell.Text = str.Substring(0, ind)
                        str = str.Substring(ind + 2)
                        If IsNumeric(cell.Text) Then
                            If cell.Text.Trim.Length > 6 Then
                                If cell.Text.Trim.IndexOf(".") = -1 Then
                                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                                End If
                            End If
                            cell.HorizontalAlign = HorizontalAlign.Right
                        ElseIf IsDate(cell.Text) Then
                            If CDate(cell.Text) = CDate("1900-01-01") Then
                                cell.Text = ""
                            Else
                                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd HH:mm")
                            End If
                        End If
                    End If
                    row.Cells.Add(cell)
                Next

                If i < 30 Then
                    For ind = i To 30
                        cell = New TableCell
                        cell.Text = ""
                        cell.Width = New Unit(60)
                        row.Cells.Add(cell)
                    Next
                End If
            End If

            exl.Rows.Add(row)
        End Sub

        Sub PIHeaderRow(ByVal str As String)
            row = New TableRow
            row.BackColor = Color.White
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)

            Dim i As Integer = 0
            Dim ind As Integer

            While (str.Length > 0)
                cell = New TableCell
                ind = str.IndexOf("~")
                If (ind = -1) Then
                    cell.Text = str
                    str = ""
                Else
                    cell.Text = str.Substring(0, ind)
                    str = str.Substring(ind + 1)
                End If
                'If (i = 0) Then
                '    i = 1
                'Else
                cell.ColumnSpan = 8
                i = i + 10
                'End If
                If IsNumeric(cell.Text) Then
                    'If cell.Text.Trim.Length > 8 Then
                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                    'End If
                ElseIf IsDate(cell.Text) Then
                    If CDate(cell.Text) = CDate("1900-01-01") Then
                        cell.Text = ""
                    Else
                        cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
                    End If
                End If
                row.Cells.Add(cell)
            End While

            If i < 30 Then
                For ind = i To 30
                    cell = New TableCell
                    cell.Text = ""
                    cell.Width = New Unit(60)
                    row.Cells.Add(cell)
                Next
            End If

            exl.Rows.Add(row)
        End Sub
    End Class

End Namespace
