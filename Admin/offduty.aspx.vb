Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class offduty
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim reader As SqlDataReader
        Dim dst As DataSet
        Dim query As String
        Shared sortOrder As String = ""
        Shared sortDir As String = "ASC"

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
            If Not IsPostBack Then
                departmentdropdownlist()
                txtStartDate.Text = DateTime.Today.Year.ToString() & "-" & DateTime.Today.Month.ToString() & "-01"
                txtEndDate.Text = Format(DateTime.Today, "yyyy-MM-dd")
                BindData()
            End If
        End Sub

        Sub departmentdropdownlist()
            department.Items.Add(New ListItem("--", "0"))
            query = " SELECT departmentID,Name From Departments WHERE isSalary='1' AND active='1' order by departmentID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, query)
            While reader.Read()
                Dim tempListItem As New ListItem
                tempListItem.Value = reader(0)
                tempListItem.Text = reader(1)
                department.Items.Add(tempListItem)
            End While
            reader.Close()
        End Sub

        Sub BindData()
            Dim startdate As DateTime = CDate(txtStartDate.Text)
            Dim enddate As DateTime = CDate(txtEndDate.Text)
            Dim i As Integer = 0
            Dim ok As Integer = 0

            query = " SELECT u.userID,u.userNo, u.userName, ISNULL(dp.name,N'无'), ISNULL(ws.name,N'无'), ISNULL(wg.name,N'无'),ISNULL(s.systemCodeName,N'无'),isnull(al.ddays,0),isnull(al.sdays,0),isnull(rl.number,0),u.enterdate,u.leavedate,ISNULL(sc4.systemCodeName, '') AS contracttype,CASE WHEN u.enterdate is null THEN '' ELSE CASE WHEN MONTH(u.enterdate)<=MONTH(getdate()) THEN datediff(year,u.enterdate,getdate()) ELSE datediff(year,u.enterdate,getdate())-1 END END as workyear,isnull(u.comp,''),u.wldate, "
            query &= " case when u.wldate is null THEN '' ELSE CASE WHEN MONTH(u.wldate)<=MONTH(getdate()) THEN datediff(year,u.wldate,getdate()) ELSE datediff(year,u.wldate,getdate())-1 END END  FROM "
            'Add by Baoxin 060119
            query &= " (select isnull(d .ductdays, 0) as ddays, isnull(s2.days, 0) as sdays, CASE WHEN d .usercode IS NULL THEN s2.usercode ELSE d .usercode END AS Uid From "
            query &= " ( SELECT dm.usercode, SUM(dm.deductNum) AS ductdays From DeductMoney  dm "
            query &= " INNER JOIN tcpc0.dbo.systemCode sc ON dm.typeID = sc.systemCodeID AND sc.systemCodeName = N'请假' "
            query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Deduct Money Type' "
            query &= " WHERE  dm.organizationID=" & Session("orgID") & " AND dm.workDate>='" & startdate & "' AND dm.workDate<'" & enddate.AddDays(1) & "' Group by dm.usercode,dm.typeID ) d "


            query &= " Full Outer JOIN "
            'query &= " (SELECT usercode, SUM(DATEDIFF([Day], startdate,enddate)) - CASE WHEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')) > 0 THEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')) ELSE 0 END - CASE WHEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)) > 0 THEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)) ELSE 0 END AS days "
            query &= " (SELECT usercode,SUM(CASE WHEN (enddate is not null) THEN DATEDIFF([Day], startdate,enddate)+1 ELSE 0 END) - CASE WHEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')) > 0 THEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')+1) ELSE 0 END - CASE WHEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)) > 0 THEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)+1) ELSE 0 END AS days "
            query &= " FROM SickLeave WHERE (startdate <= '" & enddate & "') AND (enddate >= '" & startdate & "') GROUP BY usercode) s2 on s2.usercode=d.usercode )as al "
            query &= " INNER JOIN tcpc0.dbo.users u ON (al.Uid = u.userID) "

            query &= " INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID= u.workTypeID "
            query &= " LEFT OUTER JOIN Workshop wg ON u.workProcedureID = wg.id "
            query &= " LEFT OUTER JOIN Workshop ws ON u.workshopID = ws.id "
            query &= " LEFT OUTER JOIN departments dp ON u.departmentID = dp.departmentID "
            query &= " LEFT OUTER JOIN tcpc0.dbo.systemCode sc4 ON u.contractTypeID = sc4.systemCodeID "

            'add by Baoxin in 20090302 (holiday for year) 
            query &= " left outer join (select usercode,sum(number) as number from RestLeave where workdate>='" & startdate & "' and workdate<='" & enddate & "' group by usercode ) rl on rl.usercode= u.userID "

            query &= " Where  u.plantCode='" & Session("PlantCode") & "' and u.isTemp='" & Session("temp") & "' "

            If txtNo.Text.Trim() <> "" Then
                query &= " and u.userNo='" & txtNo.Text.Trim() & "'"
            End If

            If txtName.Text.Trim() <> "" Then
                query &= " AND u.userName Like N'%" & txtName.Text.Trim() & "%'"
            End If

            If department.SelectedIndex <> 0 Then
                query &= " AND u.departmentID=" & department.SelectedValue
            End If

            query &= " Order By u.userID "

            Session("EXSQL") = query
            Session("EXTitle") = "<b>工号</b>~^100^<b>员工姓名</b>~^200^<b>所属部门</b>~^200^<b>所属工段</b>~^200^<b>所属班组</b>~^100^<b>性质</b>~^100^<b>请假天数</b>~^100^<b>病假天数</b>~^100^<b>年休</b>~^100^<b>入公司日期</b>~^100^<b>离职日期</b>~^100^<b>合同类型</b>~^100^<b>工龄</b>~^100^<b>劳务公司</b>~^100^<b>派遣日期</b>~^<b>工龄2</b>~^"
            'Response.Write(query)
            'Exit Sub
            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("restday", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("dept", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("sickday", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("holiday", System.Type.GetType("System.Decimal")))

            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("gsort") = i + 1
                        drow.Item("userID") = .Rows(i).Item(0)
                        drow.Item("userNo") = .Rows(i).Item(1)
                        drow.Item("userName") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("restday") = .Rows(i).Item(7)
                        drow.Item("dept") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("workshop") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("group") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("type") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("sickday") = .Rows(i).Item(8)
                        drow.Item("holiday") = .Rows(i).Item(9)
                        dtl.Rows.Add(drow)
                    Next
                    BtnExport.Enabled = True
                Else
                    BtnExport.Enabled = False
                End If
            End With
            Dim dvw As DataView
            dvw = New DataView(dtl)

            Try
                dgoffduty.DataSource = dvw
                dgoffduty.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgoffduty_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgoffduty.PageIndexChanged
            dgoffduty.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
            BindData()
        End Sub

        Public Sub detailEntry(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgoffduty.ItemCommand
            If (e.CommandName.CompareTo("detail") = 0) Then
                Response.Redirect("offdutydetail.aspx?id=" & e.Item.Cells(10).Text & "&sdate=" & txtStartDate.Text & "&edate=" & txtEndDate.Text)
            End If
        End Sub

        Private Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click
            ltlAlert.Text = " var w=window.open('printoffduty.aspx"
            Dim ref As Boolean = False
            If txtNo.Text.Trim() <> "" Then
                ltlAlert.Text &= "?id=" & txtNo.Text.Trim()
                ref = True
            End If
            If txtName.Text.Trim() <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&name=" & txtName.Text.Trim()
                Else
                    ltlAlert.Text &= "?name=" & txtName.Text.Trim()
                    ref = True
                End If
            End If
            If department.SelectedValue <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&dp=" & department.SelectedValue.Trim()
                Else
                    ltlAlert.Text &= "?dp=" & department.SelectedValue.Trim()
                    ref = True
                End If
            End If
            If txtStartDate.Text.Trim() <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&fr=" & txtStartDate.Text.Trim()
                Else
                    ltlAlert.Text &= "?fr=" & txtStartDate.Text.Trim()
                    ref = True
                End If
            End If
            If txtEndDate.Text.Trim() <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&to=" & txtEndDate.Text.Trim()
                Else
                    ltlAlert.Text &= "?to=" & txtEndDate.Text.Trim()
                    ref = True
                End If
            End If
            ltlAlert.Text &= "','rest','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0');w.focus(); "
        End Sub

        Public Sub uploadBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadBtn.ServerClick
            Dim startdate As DateTime = CDate(txtStartDate.Text)
            Dim enddate As DateTime = CDate(txtEndDate.Text)
            Dim strSql As String

            strSql = " Select u.userNo, u.userName,isnull(dp.name,''),isnull(ws.name,''),d.comments,d.workdate,d.amount,u.enterDate,u.leaveDate From DeductMoney d "
            strSql &= " INNER JOIN tcpc0.dbo.users u ON d.userCode = u.userID "
            strSql &= " LEFT OUTER JOIN Workshop ws ON u.workshopID = ws.id "
            strSql &= " LEFT OUTER JOIN departments dp ON u.departmentID = dp.departmentID "
            strSql &= " where lower(d.comments) like N'%旷工%'"
            strSql &= " and d.workDate>='" & startdate & "' AND d.workDate<'" & enddate.AddDays(1) & "'"
            strSql &= " and u.plantCode='" & Session("PlantCode") & "' and u.isTemp='" & Session("temp") & "'"
            If txtNo.Text.Trim() <> "" Then
                query &= " AND u.userNo='" & txtNo.Text.Trim() & "'"
            End If

            If txtName.Text.Trim() <> "" Then
                query &= " AND u.userName Like N'%" & txtName.Text.Trim() & "%'"
            End If

            If department.SelectedIndex <> 0 Then
                query &= " AND u.departmentID=" & department.SelectedValue
            End If

            strSql &= " order by u.userID,d.workdate "
            Session("EXSQL1") = strSql
            Session("EXTitle1") = "<b>工号</b>~^100^<b>姓名</b>~^120^<b>部门</b>~^160^<b>工段</b>~^160^<b>备注</b>~^100^<b>日期</b>~^<b>金额</b>~^100^<b>入厂日期</b>~^100^<b>离职日期</b>~^"
            Response.Redirect("/public/exportExcel1.aspx", True)
        End Sub

        Public Sub Bexcel_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bexcel.ServerClick
            Dim startdate As DateTime = CDate(txtStartDate.Text)
            Dim enddate As DateTime = CDate(txtEndDate.Text)
            Dim strSql As String

            strSql = "Select u.userNo,u.username,ISNULL(dp.name,N'无'), ISNULL(ws.name,N'无'), ISNULL(wg.name,N'无'),ISNULL(s.systemCodeName,N'无'),isnull(d.ductdays,0),isnull(s2.days,0),isnull(rl.number,0),u.enterdate,u.leavedate "
            strSql &= " From tcpc0.dbo.users u "
            strSql &= " INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID= u.workTypeID "
            strSql &= " LEFT OUTER JOIN Workshop wg ON u.workProcedureID = wg.id "
            strSql &= " LEFT OUTER JOIN Workshop ws ON u.workshopID = ws.id "
            strSql &= " LEFT OUTER JOIN departments dp ON u.departmentID = dp.departmentID "
            'add by baoxin  in 20090302 (holiday for year)
            query &= " left outer join (select usercode,sum(number) as number from RestLeave where workdate>='" & enddate & "' and workdate<='" & startdate & "' group by usercode ) rl on rl.usercode= u.userID "

            strSql &= " left outer join "
            strSql &= " ( SELECT dm.usercode, SUM(dm.deductNum) AS ductdays From DeductMoney  dm "
            strSql &= " INNER JOIN tcpc0.dbo.systemCode sc ON dm.typeID = sc.systemCodeID AND sc.systemCodeName = N'请假' "
            strSql &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Deduct Money Type' "
            strSql &= " WHERE  dm.organizationID=" & Session("orgID") & " AND dm.workDate>='" & startdate & "' AND dm.workDate<'" & enddate.AddDays(1) & "' Group by dm.usercode,dm.typeID ) d ON d.usercode=u.userID "
            strSql &= " left outer join "
            strSql &= " (SELECT usercode,SUM(CASE WHEN (enddate is not null) THEN DATEDIFF([Day], startdate,enddate)+1 ELSE 0 END) - CASE WHEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')) > 0 THEN SUM(DATEDIFF([Day], startdate, '" & startdate & "')+1) ELSE 0 END - CASE WHEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)) > 0 THEN SUM(DATEDIFF([Day], '" & enddate & "', enddate)+1) ELSE 0 END AS days "
            strSql &= " FROM SickLeave WHERE (startdate <= '" & enddate & "') AND (enddate >= '" & startdate & "') GROUP BY usercode) s2 on s2.usercode=u.userID "

            strSql &= " Where (u.leavedate is null or u.leavedate>='" & startdate & "') and u.plantCode='" & Session("PlantCode") & "' and u.isTemp='" & Session("temp") & "'"
            Session("EXSQL1") = strSql
            Session("EXTitle1") = "<b>工号</b>~^100^<b>员工姓名</b>~^200^<b>所属部门</b>~^200^<b>所属工段</b>~^200^<b>所属班组</b>~^100^<b>性质</b>~^100^<b>请假天数</b>~^100^<b>病假天数</b>~^100^<b>年休</b>100^<b>入公司日期</b>~^100^<b>离职日期</b>~^"
            Response.Redirect("/public/exportExcel1.aspx", True)
        End Sub
    End Class

End Namespace
