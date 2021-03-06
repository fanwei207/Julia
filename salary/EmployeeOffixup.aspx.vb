'!*******************************************************************************!
'* @@ NAME				:	EmployeeOffixup.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for EmployeeOffixup.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	April  29 2006
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Drawing
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.OleDb


Namespace tcpc

Partial Class EmployeeOffixup
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents filename As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents workshop As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ltlAlert As Literal

    Dim strSql As String
    Dim reader As SqlDataReader
    Dim chk As New adamClass
    Protected WithEvents uploadBtn As System.Web.UI.HtmlControls.HtmlInputButton

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then

            imputedmonthload()
            Dropdownlist2.SelectedIndex = DateTime.Now.Month - 1

            yeartextbox.Text = DateTime.Now.Year

            strSql = "salary_createEmployee"
            Dim params(1) As SqlParameter
            params(0) = New SqlParameter("@wdate", DateTime.Now)
            params(1) = New SqlParameter("@createdBy", Session("uID"))
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSql, params)

            dropdownValue()

            SaleBind(0)
        End If
    End Sub
    Sub dropdownValue()

        department.Items.Add(New ListItem("--", "0"))

        strSql = " Select departmentID,name From departments Where  isSalary='1' and active='1'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read()
            Dim tempListItem As New ListItem
            tempListItem.Value = reader(0)
            tempListItem.Text = reader(1)
            department.Items.Add(tempListItem)
        End While
        reader.Close()
    End Sub
  

    Private Sub imputedmonthload()
        ' Dropdownlist2.Items.Add(New ListItem("--", "0"))
        Dim i As Integer
        For i = 1 To 12
            Dim tempListItem As New ListItem
            tempListItem.Value = i
            tempListItem.Text = i
            Dropdownlist2.Items.Add(tempListItem)
        Next
    End Sub
        Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
            If IsNumeric(yeartextbox.Text.Trim) Then
            Else
                ltlAlert.Text = "alert('输入年份只能为数字!');Form1.yeartextbox.focus();"
                Exit Sub
            End If
            If Len(yeartextbox.Text.Trim) < 0 Or CInt(yeartextbox.Text.Trim) < 1900 Then
                ltlAlert.Text = "alert('输入年份有误!例如:2004');Form1.yeartextbox.focus();"
                Exit Sub
            End If
            SaleBind(1)
        End Sub

    Sub SaleBind(ByVal temp As Integer)
        Dim ds As DataSet
        strSql = " Select o.id,u.userID,u.userNo,u.username,isnull(d.name,'') as dname,u.enterdate,u.leavedate From EmployeeEspecial o "
        strSql &= " INNER JOIN  tcpc0.dbo.users u ON u.userID=o.usercode "
        strSql &= " inner join departments d on d.departmentID=u.departmentID "
        'strSql &= " left outer join workshop w on w.id=u.workshopID "
        strSql &= " Where month(o.currentdate)='" & Dropdownlist2.SelectedValue & "' and year(o.currentdate)='" & yeartextbox.Text.Trim() & "' "
            'strSql &= " and u.isTemp='" & Session("temp") & "'"
        If temp > 0 Then
            If workerNoSearch.Text.Trim() <> "" Then
                strSql &= " and cast(u.userNo as varchar)='" & workerNoSearch.Text.Trim & "'"
            End If
            If workerNameSearch.Text.Trim() <> "" Then
                strSql &= " and lower(u.username) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
            End If
            If department.SelectedValue > 0 Then
                strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
            End If
            strSql &= " and u.plantCode='" & Session("PlantCode") & "' "
        End If
        strSql &= " order by o.usercode "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("enterdate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("leavedate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("enterdate1", System.Type.GetType("System.DateTime")))
        dt.Columns.Add(New DataColumn("leavedate1", System.Type.GetType("System.DateTime")))
        'dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim dr1 As DataRow
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("gsort") = i + 1
                    dr1.Item("userNo") = .Rows(i).Item("userNo").ToString()
                    dr1.Item("userID") = .Rows(i).Item("userID").ToString()
                    dr1.Item("userName") = .Rows(i).Item("userName")

                    dr1.Item("ID") = .Rows(i).Item("ID")
                    dr1.Item("department") = .Rows(i).Item("dname")

                    dr1.Item("enterdate") = Format(.Rows(i).Item("enterdate"), "yyyy-MM-dd")
                    If .Rows(i).Item("leavedate").ToString = "" Then
                        dr1.Item("leavedate") = ""
                    Else
                        dr1.Item("leavedate") = Format(.Rows(i).Item("leavedate"), "yyyy-MM-dd")
                    End If

                    dr1.Item("enterdate1") = .Rows(i).Item("enterdate")
                    dr1.Item("leavedate1") = .Rows(i).Item("leavedate")
                    'dr1.Item("workshop") = .Rows(i).Item("wname")
                    dt.Rows.Add(dr1)
                Next
            End If
        End With
        Dim dv As DataView
        dv = New DataView(dt)

        Try
            DataGrid1.DataSource = dv
            DataGrid1.DataBind()
        Catch
        End Try
    End Sub

    Public Sub Edit_delete(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        strSql = " Delete From EmployeeEspecial "
        strSql &= " Where id =" & e.Item.Cells(7).Text.Trim()
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        SaleBind(1)
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        SaleBind(1)
    End Sub

    Sub workerNo_changed(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exitFlag As Boolean = False
        strSql = " SELECT userID,userName,leaveDate " _
                & " FROM tcpc0.dbo.users  u INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID= u.workTypeID and Substring(s.systemCodeName,1,2)=N'计时' " _
                & " WHERE cast(u.userNo as varchar)='" & workerNo.Text.Trim & "'"
            'strSql &= " and u.isTemp='" & Session("temp") & "'"
        strSql &= " and u.plantCode='" & Session("PlantCode") & "' "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read
            exitFlag = True
            If reader("leaveDate").ToString() <> "" Then
                If DateAdd(DateInterval.Month, 2, CDate(reader("leaveDate"))) < DateAdd(DateInterval.Month, 1, CDate(yeartextbox.Text.Trim & "/" & Dropdownlist2.SelectedValue & "/1")) Then
                    ltlAlert.Text = "alert('此员工已离职！');Form1.workerNo.focus();"
                    workerNo.Text = ""
                    workerName.Text = ""
                    userID.Text = ""
                    Exit Sub
                Else
                    ltlAlert.Text = "alert('此员工属于离职员工！');Form1.BtnSave.focus();"
                End If


            End If

            workerName.Text = reader("userName")
            userID.Text = reader(0)
            ltlAlert.Text = "Form1.BtnSave.focus();"
        End While
        reader.Close()
        If exitFlag = False Then
            If workerNo.Text <> "" Then
                ltlAlert.Text = "alert('工号不存在或者不是计时工！');"
                ltlAlert.Text = "Form1.workerNo.focus();"
            End If
            workerNo.Text = ""
            workerName.Text = ""
            userID.Text = ""
        End If
    End Sub

        Sub BtnSave_click(ByVal sender As Object, ByVal e As System.EventArgs)

            If workerNo.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('工号 不能为空!');"
                Exit Sub
            End If

            If yeartextbox.Text.Trim() = "" Then
                ltlAlert.Text = "alert('年份不能为空!');Form1.yeartextbox.focus();"
                Exit Sub
            Else
                If IsNumeric(yeartextbox.Text.Trim) Then
                    If CInt(yeartextbox.Text.Trim) < 1900 Then
                        ltlAlert.Text = "alert('输入年份有误!');Form1.yeartextbox.focus();"
                        Exit Sub
                    End If
                Else
                    ltlAlert.Text = "alert('输入年份只能为数字!');Form1.yeartextbox.focus();"
                    Exit Sub
                End If
            End If
            Dim eid As String = ""
            strSql = " select id from EmployeeEspecial where month(currentdate)='" & Dropdownlist2.SelectedValue & "' and year(currentdate)='" & yeartextbox.Text.Trim & "' and usercode='" & userID.Text.Trim() & "' "
            eid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
            If eid <> "" Then
                ltlAlert.Text = "alert('输入数据已存在!');Form1.workerNo.focus();"
                Exit Sub
            Else
                Dim edate As String = yeartextbox.Text.Trim & "-" & Dropdownlist2.SelectedValue & "-1"
                strSql = " insert into EmployeeEspecial(usercode,currentdate,creatby,creatday) values ('" & userID.Text.Trim() & "','" & edate & "','" & Session("uID") & "',getdate())"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                workerNo.Text = ""
                workerName.Text = ""
                userID.Text = ""
                ltlAlert.Text = "alert('保存成功!');Form1.workerNo.focus();"
                SaleBind(1)
            End If
        End Sub

        Sub TimeExport(ByVal sender As Object, ByVal e As System.EventArgs) Handles Bchange.Click
            Dim Monthx As String
            Dim ldate As String
            Dim mast(2) As Decimal
            Dim personalpay As Decimal
            If Dropdownlist2.SelectedValue.Length = 1 Then
                Monthx = "0" & Dropdownlist2.SelectedValue
            Else
                Monthx = Dropdownlist2.SelectedValue.ToString()
            End If
            ldate = yeartextbox.Text.Trim & Monthx

            Dim attedays As Decimal = 0
            Dim menber As Decimal
            strSql = "SELECT WorkDays,MidNightSubsidy,NightSubsidy,WholeNightSubsidy,Tax,BasicWage,LaborRate FROM hr_bi_mstr WHERE WorkYear='" & yeartextbox.Text & "' AND WorkMonth='" & Dropdownlist2.SelectedValue & "' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                attedays = reader(0)
                mast(0) = reader(1)
                mast(1) = reader(2)
                mast(2) = reader(3)
                personalpay = reader(4)
                menber = reader(5) * reader(6)
            End While
            If attedays = 0 Then
                ltlAlert.Text = "alert('当月应出勤不存在!');"
                Exit Sub
            End If
            Dim currentdate As String = yeartextbox.Text.Trim() & "-" & Dropdownlist2.SelectedValue & "-01"
            Dim monthbonus As DateTime = CDate(currentdate).AddMonths(-2)

            '1-16
            strSql = " select u.userNo,u.username,isnull(pa.totalHours,0)/8.0,'','','','',isnull(d.name,''),'" & currentdate & "',u.enterdate,u.leavedate,isnull(pa.pdays,0),isnull(pa.mid,0),isnull(pa.night,0),isnull(pa.whole,0),isnull(pa.mid,0)*" & mast(0) & "+isnull(pa.night,0)*" & mast(1) & "+isnull(pa.whole,0)*" & mast(2) & ",case when isnull(pa.pdays,0)-" & attedays & " >0 then cast((isnull(pa.pdays,0)-" & attedays & ") as int)*isnull(pa.totalHours,0)/8.0/isnull(pa.pdays,0)+isnull(pa.pdays,0)-" & attedays & "-cast((isnull(pa.pdays,0)-" & attedays & ") as int) else 0 end ,"
            strSql &= " 0, ISNULL(a.hr_accfound_hFound,'0'),"
            If Session("PlantCode") = 2 Or Session("PlantCode") = 3 Then
                strSql &= " isnull( a.hr_accfound_eFound,0)+isnull(a.hr_accfound_rFound,0),"
            Else
                strSql &= " isnull( a.hr_accfound_rFound,0)+isnull( a.hr_accfound_eFound,0)+isnull(a.hr_accfound_mFound,0),"
            End If

            strSql &= " CASE WHEN (u.leavedate is null) or DATEDIFF([Month],u.leavedate,'" & CDate(currentdate) & "')<0 THEN "
            strSql &= " case when u.isLabourUnion=1 then " & menber.ToString() & " else 0 end  ELSE 0 END ,"

            strSql &= " ISNULL(dm.deduct,0),isnull(l.sickDays,0),isnull(l.bussinessDays,0)," & personalpay.ToString() & ",isnull(s.systemCodeName,'')"
            If Session("PlantCode") = 2 Or Session("PlantCode") = 3 Then
                strSql &= ",isnull(a.hr_accfound_mFound,0),0 "
            Else
                strSql &= ",'',0 "
            End If
            strSql &= " ,isnull(u.homeAddress,''),isnull(s4.systemcodename,''),roleName,rest.Number "

            strSql &= " From EmployeeEspecial e INNER JOIN tcpc0.dbo.users u on u.userID=e.usercode "   'and u.isTemp='" & Session("temp") & "'"
            strSql &= " Left outer JOIN Roles r ON u.roleID=r.roleID "
            strSql &= " Left outer join departments d on d.departmentID=u.departmentID "
            strSql &= " left outer JOIN tcpc0.dbo.systemCode s ON s.systemCodeID = u.employTypeID"
            strSql &= " Left outer join tcpc0.dbo.systemCode s4 on s4.systemcodeid=u.provinceID "
            strSql &= " LEFT OUTER JOIN (SELECT qqq.userID As hr_attendance_userID,SUM(qqq.totalhr) As totalHours,SUM(qqq.days) As pdays,"
            strSql &= "					    SUM(CAST(SubString(night,1,1) As Int)) As Mid,SUM(CAST(SubString(night,2,1) As Int)) As Night,SUM(CAST(SubString(night,3,1) As Int)) As Whole "
            strSql &= "	                 FROM "
            strSql &= "	 			    ( SELECT userID,SUM(totalhr) As totalhr,CASE WHEN SUM(totalhr)>=8 THEN 1 ELSE SUM(totalhr)/8 END As days,CASE WHEN Max(CAST(qq.night As int)) =0  THEN '000' ELSE CASE WHEN Max(CAST(qq.night As int)) =1 THEN '001' ELSE  CASE WHEN Max(CAST(qq.night As int)) =10 THEN '010' ELSE   '100'  End  End END As Night  "
            strSql &= "	 			     FROM (SELECT userID,totalhr,CASE WHEN (totalhr>=12 AND h1<=22 AND h2>=30) THEN '001' ELSE  CASE WHEN (totalhr>=8 AND ((h1<=3 AND h1>0) or h2 >24 )) THEN '010' ELSE  CASE WHEN (totalhr>=8 AND ( h2 <=24 And  h2 >=22)) THEN '100' ELSE '000' End End END As night,workday   "
            strSql &= "	                       FROM (SELECT  userID,totalhr,datepart(hh,starttime) As h1,CASE WHEN datepart(hh,endtime)<= datepart(hh,starttime) THEN datepart(hh,endtime)+ 24 ELSE datepart(hh,endtime) END As h2,CONVERT(varchar(10), starttime, 120) As workday "
            strSql &= "                              FROM tcpc0.dbo.hr_Attendance_calendar WHERE uyear='" & yeartextbox.Text.Trim() & "' AND umonth= '" & Dropdownlist2.SelectedValue & "' AND plantid='" & Session("plantcode") & "' "    ' AND userType <>'394' "
            strSql &= "                              AND CAST(starttime As smalldatetime) NOT IN (SELECT HolidayDate FROM hr_holiday_mstr WHERE Year(HolidayDate)='" & yeartextbox.Text.Trim() & "'  and Month(HolidayDate) ='" & Dropdownlist2.SelectedValue & "' )"
            strSql &= "                              ) q ) qq GROUP BY userID,workday ) qqq  GrOUP BY userID) AS pa ON pa.hr_attendance_userID=u.userID  "



            '//扣款
            strSql &= " LEFT OUTER JOIN (SELECT UserCode,SUM(ISNULL(Amount,0)) AS deduct FROM hr_Deduct WHERE YEAR(WorkDate)='" & yeartextbox.Text.Trim() & "' AND MONTH(WorkDate)='" & Dropdownlist2.SelectedValue & "' GROUP BY UserCode) AS dm ON dm.Usercode=u.userID "
            '//请假
            strSql &= " LEFT OUTER JOIN (SELECT userID,SUM(ISNULL(sickDays,0)) AS sickDays,SUM(ISNULL(bussinessDays,0))AS bussinessDays FROM hr_Leave_Mstr WHERE Year(startDate)= '" & yeartextbox.Text.Trim() & "' and Month(startDate) = '" & Dropdownlist2.SelectedValue & "' Group by userID ) AS l ON l.userID=u.userID "
            '//社保
            strSql &= " LEFT OUTER JOIN hr_accfound_mstr a ON a.hr_accfound_userID = u.userID AND Year(hr_accfound_date)= '" & yeartextbox.Text.Trim() & "' AND Month(hr_accfound_date) = '" & Dropdownlist2.SelectedValue & "' "
            '//年休假
            strSql &= " LEFT OUTER JOIN (select SUM(Number) as Number,usercode from hr_RestLeave WHERE Year(WorkDate)= '" & yeartextbox.Text.Trim() & "' and Month(WorkDate) = '" & Dropdownlist2.SelectedValue & "' Group by usercode ) AS rest ON rest.usercode=u.userID "


            'strSql &= " LEFT OUTER JOIN BasicSalary b ON b.userID = u.userID and year(b.workdate)='" & yeartextbox.Text.Trim() & "' and month(b.workdate)='" & Dropdownlist2.SelectedValue & "' "
            'strSql &= " Left outer join bonusTimeSalary bs on bs.usercode=u.userID and month(bs.salarydate)='" & Month(monthbonus) & "' and year(bs.salarydate)='" & Year(monthbonus) & "'"

            strSql &= " where year(e.currentdate)='" & yeartextbox.Text.Trim() & "' and month(e.currentdate)='" & Dropdownlist2.SelectedValue & "'"
            'Response.Write(strSql)
            'Exit Sub
            Session("EXHeader2") = ""
            Session("EXSQL2") = strSql
            Session("EXTitle2") = "<b>工号</b>~^<b>姓名</b>~^<b>月考勤</b>~^100^<b>特别奖励</b>~^150^<b>效益工资月调整值</b>~^150^<b>考核奖月调整值</b>~^150^<b>是否算中夜班费</b>~^150^<b>部门</b>~^<b>日期</b>~^100^<b>入公司日期</b>~^100^<b>离职日期</b>~^<b>出勤天</b>~^<b>中班</b>~^<b>夜班</b>~^<b>全夜</b>~^<b>中夜班费</b>~^<b>双休日</b>~^<b>伙食费</b>~^<b>公积金</b>~^<b>养老金</b>~^<b>工会费</b>~^100^<b>违纪扣款</b>~^<b>病假</b>~^<b>事假</b>~^100^<b>个人税额</b>~^100^<b>用工性质</b>~^"  '26
            If Session("PlantCode") = 2 Or Session("PlantCode") = 3 Then
                Session("EXTitle2") &= "<b>医保</b>~^"
            Else
                Session("EXTitle2") &= "<b></b>~^"
            End If
            Session("EXTitle2") &= "<b>月度奖</b>~^<b>家庭地址</b>~^<b>省市</b>~^<b>职务</b>~^<b>年休假</b>~^"
            ltlAlert.Text = "window.open('/public/exportExcel2.aspx', '_blank');"
        End Sub
End Class

End Namespace
