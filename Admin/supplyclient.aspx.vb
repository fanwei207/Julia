Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class supplyclient_aspx
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal
        Dim item As ListItem
        Dim strSQL As String
        Dim dst As DataSet

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

                'ddlRole.SelectedIndex = 0
                item = New ListItem("--")
                item.Value = 0
                ddlRole.Items.Add(item)
                strSQL = "SELECT departmentID,name From departments where issalary=1 order by name"
                dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With dst.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            ddlRole.Items.Add(item)
                        Next
                        ddlRole.SelectedIndex = 1
                    End If
                End With
                dst.Reset()

                loadUser()

                BindData()

                If Request("pg") <> Nothing Then
                    bntBack.Enabled = True
                Else
                    bntBack.Enabled = False
                End If
            End If
        End Sub

        Function loadUser()
            While ddlUser.Items.Count > 0
                ddlUser.Items.RemoveAt(0)
            End While

            If ddlRole.SelectedIndex = 0 Then
                Exit Function
            End If

            Dim id As Integer = 0
            If Request("uID") <> Nothing Then
                id = Request("uID")
                ddlRole.Enabled = False
                ddlRole.SelectedIndex = 0
            End If

            'ddlUser.SelectedIndex = 0
            If id > 0 Then
                strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and deleted=0 and leavedate is null and isactive=1 and userID=" & id & " and organizationID=" & Session("orgID") & " Order by UserName "
            Else
                strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and deleted=0 and leavedate is null and isactive=1 and DepartmentID=" & ddlRole.SelectedValue & " and organizationID=" & Session("orgID") & " Order by UserName "
            End If
            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1) & "-" & .Rows(i).Item("userno"))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        ddlUser.Items.Add(item)
                        If id = item.Value Then
                            ddlUser.SelectedIndex = i
                        End If
                    Next
                End If
            End With
            dst.Reset()
        End Function

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
            If ddlUser.Items.Count = 0 Then
                Exit Sub
            End If

            Dim strSQL As String
            Dim ds As DataSet
            Dim item As ListItem
            strSQL = "  SELECT s.systemCodeID,  s.systemCodeName, us.userID  FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID " _
                    & " Left Outer Join tcpc0.dbo.User_SupplyClient us On us.systemCodeID=s.systemCodeID And us.userID='" & ddlUser.SelectedValue & "'" _
                    & " WHERE (st.systemCodeTypeName = 'Company Type'  ) ORDER BY s.systemCodeID  "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        CheckBoxList1.Items.Add(item)
                        If Not .Rows(i).IsNull(2) Then
                            CheckBoxList1.Items(i).Selected = True
                        End If
                    Next
                End If
            End With
            ds.Reset()

        End Sub

        Private Sub ddlUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlUser.SelectedIndexChanged
            RadioButtonList1.SelectedIndex = -1
            BindData()
        End Sub

        Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
            If ddlUser.Items.Count = 0 Then
                Exit Sub
            End If

            Dim i As Integer
            For i = 0 To CheckBoxList1.Items.Count - 1
                strSQL = "SELECT systemCodeID From tcpc0.dbo.User_SupplyClient Where userID=" & ddlUser.SelectedItem.Value & " and systemCodeID=" & CheckBoxList1.Items(i).Value
                dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                If dst.Tables(0).Rows.Count > 0 Then
                    If CheckBoxList1.Items(i).Selected = False Then
                        strSQL = " Delete From tcpc0.dbo.User_SupplyClient Where userID=" & ddlUser.SelectedItem.Value & " and systemCodeID=" & CheckBoxList1.Items(i).Value
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                    End If
                Else
                    If CheckBoxList1.Items(i).Selected = True Then
                        strSQL = "Insert Into tcpc0.dbo.user_SupplyClient(userID,systemCodeID) Values(" & ddlUser.SelectedItem.Value & "," & CheckBoxList1.Items(i).Value & ")"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                    End If
                End If
                dst.Reset()
            Next

            ltlAlert.Text = "alert('    保存成功！    ');"
        End Sub

        Private Sub ddlRole_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRole.SelectedIndexChanged
            loadUser()
            BindData()
        End Sub

        Private Sub bntBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntBack.Click
            Response.Redirect(Request("pg") & "?id=" & Request("id"), True)
        End Sub
    End Class

End Namespace

