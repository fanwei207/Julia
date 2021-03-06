'!*******************************************************************************!
'* @@ NAME				:	InspectBody.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for InspectBody.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	March 25 2005
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class InspectBody
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
    Dim reader As SqlDataReader
    Dim chk As New adamClass
    Dim ds As DataSet

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            dropdownValue()
            workshop.Items.Add(New ListItem("--", "0"))

            specialType.Items.Add(New ListItem("--", "0"))
            specialType.Items.Add(New ListItem("焊锡", "1"))
            specialType.Items.Add(New ListItem("打氯仿", "2"))
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

    Sub workshopdrop()
        workshop.Items.Clear()
        workshop.Items.Add(New ListItem("--", "0"))
        strSql = " SELECT id, name FROM Workshop where workshopID is null and departmentID='" & department.SelectedValue & "' "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read()
            Dim tempListItem As New ListItem
            tempListItem.Value = reader(0)
            tempListItem.Text = reader(1)
            workshop.Items.Add(tempListItem)
        End While
        reader.Close()
        workshop.SelectedIndex = 0
    End Sub

    Sub bandworkshop(ByVal sender As Object, ByVal e As System.EventArgs)
        If department.SelectedIndex > 0 Then
            workshopdrop()
        Else
            workshop.Items.Clear()
            workshop.Items.Add(New ListItem("--", "0"))
        End If
    End Sub

    Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
        SaleBind(1)
    End Sub

    Sub SaleBind(ByVal temp As Integer)
        strSql = " Select u.userID,u.userName as name,r.roleName,d.Name,isnull(sc1.systemcodeName,''),datediff(year,u.birthday,getdate()) as birthday ,w.name ,uh.healthCheckDate,u.userNo "
        strSql &= " From User_Health uh "
        strSql &= " Inner join tcpc0.dbo.users u on u.userID=uh.userID "
        strSql &= " Left outer JOIN Roles r ON u.roleID=r.roleID "
        strSql &= " Left outer JOIN Departments d ON d.departmentID=u.departmentID "
        strSql &= " Left outer JOIN tcpc0.dbo.systemCode sc1 ON u.sexID=sc1.systemcodeID "
        strSql &= " Left outer JOIN tcpc0.dbo.systemCodeType st1 ON sc1.systemcodetypeID=st1.systemcodetypeID and st1.systemCodeTypeName='Sex' "
        strSql &= " Left outer JOIN Workshop w ON w.id=u.workshopID  "
        strSql &= " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and u.organizationID=" & Session("orgID")
        strSql &= " and u.leaveDate is null  and u.specialWorkType<>'0' "
        strSql &= " and u.isTemp='" & Session("temp") & "'"
        If temp <> 0 Then
            If workerNoSearch.Text.Trim() <> "" Then
                strSql &= " and cast(u.userNo as varchar)='" & workerNoSearch.Text.Trim & "'"
            End If
            If workerNameSearch.Text.Trim() <> "" Then
                strSql &= " and lower(u.userName) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
            End If
            If department.SelectedValue > 0 Then
                strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
            End If
            If workshop.SelectedIndex > 0 Then
                strSql &= " and u.workshopID=" & workshop.SelectedValue
            End If
            If specialType.SelectedIndex > 0 Then
                strSql &= " and u.specialWorkType=" & specialType.SelectedValue
            End If
        End If
        strSql &= " and uh.status='0' and dateadd(year,1,uh.healthCheckDate)<=getdate() "
        strSql &= " Order by u.userID "

        'Response.Write(strSql)
        'Exit Sub
        'Session("EXSQL") = strSql
        'Session("EXTitle") = "<b>工号</b>~^<b>姓名</b>~^<b>职务</b>~^<b>部门</b>~^<b>工段</b>~^<b>性别</b>~^<b>年龄</b>~^<b>体检日期</b>~^"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("roleName", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("departmentName", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("sex", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("age", System.Type.GetType("System.Int32")))
        'dt.Columns.Add(New DataColumn("uniform", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("healthCheckDate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
        'dt.Columns.Add(New DataColumn("uniformID", System.Type.GetType("System.String")))
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("userNo") = .Rows(i).Item(8)
                    dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                    dr1.Item("roleName") = .Rows(i).Item(2).ToString().Trim()
                    dr1.Item("departmentName") = .Rows(i).Item(3).ToString().Trim()
                    dr1.Item("sex") = .Rows(i).Item(4).ToString().Trim()
                    dr1.Item("age") = .Rows(i).Item(5)
                    dr1.Item("workshop") = .Rows(i).Item(6).ToString().Trim()
                    dr1.Item("healthCheckDate") = Format(.Rows(i).Item(7), "yyyy-MM-dd")
                    dr1.Item("userID") = .Rows(i).Item(0)
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
        ds.Reset()

    End Sub

    Sub changeuniform(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim j As Integer
        Dim flag As Boolean = False

        strSql = " Delete From User_Health where status='1' "
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

        For j = 0 To DataGrid1.Items.Count - 1
            If CType(DataGrid1.Items(j).FindControl("changed"), CheckBox).Checked = True Then
                flag = True
                strSql = " Insert into User_Health (userID,healthCheckDate,status,createdBy,createdDate)(select userID,healthCheckDate,'1'," & Session("uID") & ",getdate() From  User_Health where userID='" & DataGrid1.Items(j).Cells(9).Text & "' and status='0')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            End If
        Next
        'SaleBind(1)
        If flag = False Then
            ltlAlert.Text = "alert('没有选择员工！')"
            Exit Sub
        End If
        ltlAlert.Text = "window.open('/admin/InspctBodyexcel.aspx?type=0','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') "

        'For j = 0 To DataGrid1.Items.Count - 1
        '    CType(DataGrid1.Items(j).FindControl("changed"), CheckBox).Checked = False
        'Next
    End Sub

    Sub all_check(ByVal sender As Object, ByVal e As System.EventArgs)
        'SaleBind(1)
        Dim j As Integer
        For j = 0 To DataGrid1.Items.Count - 1
            CType(DataGrid1.Items(j).FindControl("changed"), CheckBox).Checked = True
        Next
    End Sub

    Sub comfirmpeople(ByVal sender As Object, ByVal e As System.EventArgs)
        'Response.Redirect(chk.urlRand("/admin/ComfirmInspect.aspx"))
        Dim j As Integer
        For j = 0 To DataGrid1.Items.Count - 1
            If CType(DataGrid1.Items(j).FindControl("changed"), CheckBox).Checked = True Then
                strSql = " update User_Health set status='2' where userID='" & DataGrid1.Items(j).Cells(9).Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                strSql = " Insert into User_Health (userID,healthCheckDate,status,createdBy,createdDate) Values ('" & DataGrid1.Items(j).Cells(9).Text & "','" & DateTime.Now.ToShortDateString() & "','0'," & Session("uID") & ",getdate() )"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            End If
        Next
        SaleBind(1)
    End Sub


    Public Sub editUser(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandName.CompareTo("editUser") = 0) Then
            ltlAlert.Text = "window.open('/admin/ComfirmInspect.aspx?uid=" & e.Item.Cells(9).Text & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') "
        End If
    End Sub
End Class

End Namespace
