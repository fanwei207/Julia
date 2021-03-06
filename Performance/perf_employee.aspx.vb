Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class perf_employee
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
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList


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

            dept.SelectedIndex = 0
            item = New ListItem("--")
            item.Value = 0
            dept.Items.Add(item)
            strSQL = "SELECT departmentID,name From departments where isnull(active,0)=1 and isnull(isSalary,0)=1 order by name"
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

            BindData()
        End If
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


        If dept.SelectedIndex = 0 Then
            strSQL = "SELECT u.userID,u.userno+'-'+u.userName, isnull(d.name,''),isnull(r.roleName,'') "
            strSQL &= " From tcpc0.dbo.users u Left Outer Join departments d On d.departmentID=u.departmentID "
            strSQL &= " Left Outer Join roles r On r.roleID=u.roleID "
            strSQL &= " Where  u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.isactive=1 and u.roleID<>1 and leaveDate is null and u.organizationID=" & Session("orgID") & " Order by u.UserNo "
            'strSQL &= " Where  u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.isactive=1 and u.roleID<>1 and leaveDate is null and (u.workTypeID=96 or u.workTypeID=278 or u.workTypeID=279 or u.workTypeID=280) and u.organizationID=" & Session("orgID") & " Order by u.UserName "
        Else
            strSQL = "SELECT u.userID,u.userno+'-'+u.userName, isnull(d.name,''),isnull(r.roleName,'') "
            strSQL &= " From tcpc0.dbo.users u Left Outer Join departments d On d.departmentID=u.departmentID "
            strSQL &= " Left Outer Join roles r On r.roleID=u.roleID "
            strSQL &= " Where  u.plantCode='" & Session("PlantCode") & "' and u.departmentID=" & dept.SelectedValue & " and u.deleted=0 and u.isactive=1 and u.roleID<>1 and leaveDate is null and u.organizationID=" & Session("orgID") & " Order by u.UserNo "
            'strSQL &= " Where  u.plantCode='" & Session("PlantCode") & "' and u.departmentID=" & dept.SelectedValue & " and u.deleted=0 and u.isactive=1 and u.roleID<>1 and leaveDate is null and (u.workTypeID=96 or u.workTypeID=278 or u.workTypeID=279 or u.workTypeID=280) and u.organizationID=" & Session("orgID") & " Order by u.UserName "
        End If

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(1) & "  --  " & .Rows(i).Item(3) & " , " & .Rows(i).Item(2))
                    item.Value = Convert.ToInt32(.Rows(i).Item(0))
                    CheckBoxList1.Items.Add(item)
                Next i
                Label1.Text = "人数：" & .Rows.Count.ToString()
            End If
        End With
        ds.Reset()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If txb_rate.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('请输入奖罚率');"
            Exit Sub
        ElseIf Not IsNumeric(txb_rate.Text) Then
            ltlAlert.Text = "alert('奖罚率必须是数值型');"
            Exit Sub
        ElseIf CDec(txb_rate.Text) < 0 Then
            ltlAlert.Text = "alert('奖罚率必须大于等于零');"
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To CheckBoxList1.Items.Count - 1
            If CheckBoxList1.Items(i).Selected = True Then
                strSQL = "SELECT isnull(perf_userid,0) From perf_employee Where perf_userid=" & CheckBoxList1.Items(i).Value
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) <= 0 Then
                    strSQL = "Insert Into perf_employee(perf_userid,perf_userno,perf_uname,perf_dept,perf_rate,perf_createdby,perf_createddate) "
                    strSQL &= " Select u.userid,u.userno,u.username,d.name,'" & txb_rate.Text & "',N'" & Session("uName") & "',getdate() from tcpc0.dbo.users u  Left Outer Join departments d On d.departmentID=u.departmentID "
                    strSQL &= " where u.userid= " & CheckBoxList1.Items(i).Value
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                End If
                CheckBoxList1.Items(i).Selected = False
            End If
        Next

        For i = 0 To CheckBoxList1.Items.Count - 1
            CheckBoxList1.Items(i).Selected = False
        Next

        RadioButtonList1.ClearSelection()

        ltlAlert.Text = "alert('    保存成功！    ');"
    End Sub


    Private Sub dept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dept.SelectedIndexChanged
        BindData()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("perf_employee_list.aspx")
    End Sub
End Class

End Namespace

