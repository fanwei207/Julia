Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class manager_worker
        Inherits BasePage
    Dim chk As New adamClass
    Dim item As ListItem
    Dim strSQL As String
    Dim ds As DataSet
    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal

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
        ltlAlert.Text = ""
        If Not IsPostBack Then 
                'ddlCompany.SelectedIndex = 0
            strSQL = "SELECT plantID,description From tcpc0.dbo.plants where plantID<100 order by plantid"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        ddlCompany.Items.Add(item)
                    Next
                    ddlCompany.SelectedValue = Session("PlantCode")
                End If
            End With
            ds.Reset()

            loadDepartment()
            

            item = New ListItem("--")
            item.Value = 0
            dept.Items.Add(item)
            strSQL = "SELECT departmentID,name From departments where issalary=1 order by name"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        dept.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()
                dept.SelectedIndex = 0

            loadUser()

            BindData()
        End If
    End Sub
        Sub loadDepartment()
            While role.Items.Count > 0
                role.Items.RemoveAt(0)
            End While


            item = New ListItem("--")
            item.Value = 0
            role.Items.Add(item)
            role.SelectedIndex = 0
            strSQL = "SELECT departmentID,name From tcpc" & ddlCompany.SelectedValue & ".dbo.departments where issalary=1 order by name"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        role.Items.Add(item)
                    Next
                    role.SelectedIndex = 1
                End If
            End With
            ds.Reset()
        End Sub

        Sub loadUser()
            While DropDownList1.Items.Count > 0
                DropDownList1.Items.RemoveAt(0)
            End While

            If role.SelectedIndex = 0 Then
                Exit Sub
            End If

            Dim id As Integer = 0
            If Request("uid") <> Nothing Then
                id = Request("uid")
                role.Enabled = False
                role.SelectedIndex = 0
                ddlCompany.SelectedValue = Session("PlantCode")
                ddlCompany.Enabled = False
            End If

            'DropDownList1.SelectedIndex = 0
            'item = New ListItem("--")
            'item.Value = 0
            'DropDownList1.Items.Add(item)
            If id > 0 Then
                strSQL = "SELECT userID,userName + '(' + userNo + ')' From tcpc0.dbo.users Where plantCode='" & ddlCompany.SelectedValue & "' and deleted=0 and isactive=1 and userID=" & id & " and leavedate is null and organizationID=" & Session("orgID") & " Order by UserNo "
            Else
                strSQL = "SELECT userID,userName + '(' + userNo + ')' From tcpc0.dbo.users Where  plantCode='" & ddlCompany.SelectedValue & "' and deleted=0 and isactive=1 and DepartmentID=" & role.SelectedValue & " and leavedate is null and organizationID=" & Session("orgID") & " Order by UserNo "
            End If
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        DropDownList1.Items.Add(item)
                        If id = item.Value Then
                            'DropDownList1.SelectedIndex = i + 1
                            DropDownList1.SelectedIndex = i
                        End If
                    Next
                End If
            End With
            ds.Reset()
        End Sub

    Private Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        If RadioButtonList1.SelectedIndex = 0 Then
            Dim i As Integer
            For i = 0 To CheckBoxList1.Items.Count - 1
                CheckBoxList1.Items(i).Selected = True
            Next
        Else
            If RadioButtonList1.SelectedIndex = 1 Then
                Dim i As Integer
                For i = 0 To CheckBoxList1.Items.Count - 1
                    CheckBoxList1.Items(i).Selected = False
                Next
            End If
        End If
    End Sub
    Private Sub BindData()
        CheckBoxList1.Items.Clear()
        If DropDownList1.Items.Count = 0 Then
            Exit Sub
        End If

        If dept.SelectedIndex = 0 Then
                strSQL = "SELECT u.userID,u.userName + + '(' + userNo + ')', m.manager,isnull(d.name,''),isnull(r.roleName,'') From tcpc0.dbo.users u Left Outer Join manager_worker m ON u.userID=m.worker and m.manager=" & DropDownList1.SelectedValue & "Left Outer Join departments d On d.departmentID=u.departmentID Left Outer Join roles r On r.roleID=u.roleID Where  u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.isactive=1 and leavedate is null and u.organizationID=" & Session("orgID") & " Order by u.UserNo "
        Else
                strSQL = "SELECT u.userID,u.userName + '(' + userNo + ')', m.manager,isnull(d.name,''),isnull(r.roleName,'') From tcpc0.dbo.users u Left Outer Join manager_worker m ON u.userID=m.worker and m.manager=" & DropDownList1.SelectedValue & "Left Outer Join departments d On d.departmentID=u.departmentID Left Outer Join roles r On r.roleID=u.roleID Where  u.plantCode='" & Session("PlantCode") & "' and u.departmentID=" & dept.SelectedValue & " and u.deleted=0 and u.isactive=1 and leavedate is null and u.organizationID=" & Session("orgID") & " Order by u.UserNo "
        End If

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(1) & "  --  " & .Rows(i).Item(4) & " , " & .Rows(i).Item(3))
                    item.Value = Convert.ToInt32(.Rows(i).Item(0))
                    CheckBoxList1.Items.Add(item)
                    If Not .Rows(i).IsNull(2) Then
                        CheckBoxList1.Items(i).Selected = True
                    End If
                Next i
            End If
        End With
        ds.Reset()
    End Sub
    Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        RadioButtonList1.SelectedIndex = -1
        BindData()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If DropDownList1.Items.Count = 0 Then
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To CheckBoxList1.Items.Count - 1
            strSQL = "SELECT worker From Manager_worker Where manager=" & DropDownList1.SelectedItem.Value & " and worker=" & CheckBoxList1.Items(i).Value
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            If ds.Tables(0).Rows.Count > 0 Then
                If CheckBoxList1.Items(i).Selected = False Then
                    strSQL = "Delete From Manager_worker Where manager=" & DropDownList1.SelectedItem.Value & " and worker=" & CheckBoxList1.Items(i).Value
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                End If
            Else
                If CheckBoxList1.Items(i).Selected = True Then
                    strSQL = "Insert Into Manager_worker(manager,worker) Values(" & DropDownList1.SelectedItem.Value & "," & CheckBoxList1.Items(i).Value & ")"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                End If
            End If
            ds.Reset()
        Next

        ltlAlert.Text = "alert('    保存成功！    ');"
    End Sub

    Private Sub role_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles role.SelectedIndexChanged
        loadUser()
        BindData()
    End Sub

    Private Sub dept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dept.SelectedIndexChanged
        BindData()
    End Sub

    Private Sub ddlCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCompany.SelectedIndexChanged
        loadDepartment()
        loadUser()
        BindData()
    End Sub
End Class

End Namespace

