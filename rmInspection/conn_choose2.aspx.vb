Imports System
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class conn_choose2
    Inherits System.Web.UI.Page
    Protected WithEvents txb_typeid As System.Web.UI.WebControls.TextBox

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
        If Not IsPostBack Then
            txb_docid.Text = Request("mid")

            ddl_Company.SelectedIndex = 0
                strSQL = "SELECT plantID,description From tcpc0.dbo.plants where isAdmin=0 order by plantID"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        ddl_Company.Items.Add(item)
                    Next
                    ddl_Company.SelectedValue = Session("PlantCode")
                End If
            End With
            ds.Reset()

            loadDepartment()
            loadUser()
        End If

    End Sub
        Sub loadDepartment()
            While ddl_department.Items.Count > 0
                ddl_department.Items.RemoveAt(0)
            End While

            item = New ListItem("--")
            item.Value = 0
            ddl_department.Items.Add(item)
            strSQL = "SELECT departmentID,name From tcpc" & ddl_Company.SelectedValue & ".dbo.departments where active = 1 and issalary=1 order by name"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        ddl_department.Items.Add(item)
                    Next
                    ddl_department.SelectedIndex = 1
                End If
            End With
            ds.Reset()
        End Sub
        Sub loadUser()
            While cbl_user.Items.Count > 0
                cbl_user.Items.RemoveAt(0)
            End While

            'If ddl_department.SelectedIndex = 0 Then
            '    Exit Function
            'End If
            strSQL = "SELECT userID,userName,userno,email From tcpc0.dbo.users Where  plantCode='" & ddl_Company.SelectedValue & "' and deleted=0 and isactive=1 and leavedate is null "
            If ddl_department.SelectedValue > 0 Then
                strSQL &= " and DepartmentID=" & ddl_department.SelectedValue & " and organizationID=" & Session("orgID") & "  "
                If Me.txb_user.Text.Trim.Length > 0 Then
                    strSQL &= " and username like N'%" & txb_user.Text.Trim & "%'"
                End If
            Else
                If Me.txb_user.Text.Trim.Length > 0 Then
                    strSQL &= " and username like N'%" & txb_user.Text.Trim & "%'"
                Else
                    ltlAlert.Text = "alert('部门和人员中至少选择一个条件！')"
                    Exit Sub
                End If
            End If
            'strSQL &= " order by username "
            strSQL &= " Order by RTRIM(LTRIM(UserName)) COLLATE Chinese_PRC_CS_AS_KS_WS " 'modified by shanzm 2010.2.26

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1) & "~" & .Rows(i).Item("userno") & "~" & .Rows(i).Item("email"))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        cbl_user.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()
        End Sub
    Private Sub ddl_Company_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Company.SelectedIndexChanged
        loadDepartment()
        loadUser()
    End Sub

    Private Sub ddl_department_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_department.SelectedIndexChanged
        loadUser()
    End Sub

    Private Sub cbl_user_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbl_user.SelectedIndexChanged
        If cbl_user.Items.Count = 0 Then
            Exit Sub
        End If
        Dim i As Integer
        For i = 0 To cbl_user.Items.Count - 1

            If cbl_user.Items(i).Selected = False Then
                txb_chooseid.Text = txb_chooseid.Text.Replace("," + cbl_user.Items(i).Value + ",", ",")
                txb_choose.Text = txb_choose.Text.Replace("," + cbl_user.Items(i).Text.Substring(0, cbl_user.Items(i).Text.IndexOf("~")) + ",", ",")

                If txb_chooseid.Text.Trim.Replace(",", "").Trim.Length <= 0 Then
                    txb_chooseid.Text = ""
                End If

                If txb_choose.Text.Trim.Replace(",", "").Trim.Length <= 0 Then
                    txb_choose.Text = ""
                End If

            ElseIf cbl_user.Items(i).Selected Then
                If txb_choose.Text.IndexOf(cbl_user.Items(i).Text.Substring(0, cbl_user.Items(i).Text.IndexOf("~"))) = -1 Then
                    txb_choose.Text = txb_choose.Text + "," + cbl_user.Items(i).Text.Substring(0, cbl_user.Items(i).Text.IndexOf("~")) + ","
                    txb_choose.Text = txb_choose.Text.Replace(",,", ",")
                    txb_chooseid.Text = txb_chooseid.Text + "," + cbl_user.Items(i).Value + ","
                    txb_chooseid.Text = txb_chooseid.Text.Replace(",,", ",")
                End If
            End If
        Next

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ltlAlert.Text = "window.close();"
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ltlAlert.Text = "window.close();"
    End Sub
    Private Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        loadUser()
    End Sub
End Class

End Namespace
