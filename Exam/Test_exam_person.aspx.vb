Imports System
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Partial Class Test_Test_exam_person
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

        strSQL = "SELECT userID,userName,userno,email From tcpc0.dbo.users Where plantCode='" & ddl_Company.SelectedValue & "' and deleted=0 and isactive=1 and leavedate is null "
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
        ckball.Checked = False
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

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    ltlAlert.Text = "window.close();"
    'End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    ltlAlert.Text = "window.close();"
    'End Sub
    Private Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        loadUser()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        If String.IsNullOrEmpty(Request.QueryString("exam_id")) Then
            ltlAlert.Text = "alert('非法参数！必须传递参数exam_id！');"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("SqlConn.Conn_WF")

        If Not String.IsNullOrEmpty(txb_chooseid.Text) Then
            Try
                Dim strSql As String = "sp_test_saveExamPerson"
                Dim param(4) As SqlParameter
                param(0) = New SqlParameter("@exam", Request.QueryString("exam_id"))
                param(1) = New SqlParameter("@userList", txb_chooseid.Text)
                param(2) = New SqlParameter("@uID", Session("uID").ToString())
                param(3) = New SqlParameter("@retValue", SqlDbType.Bit)
                param(3).Direction = ParameterDirection.Output

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param)

                If Not Convert.ToBoolean(param(3).Value) Then
                    ltlAlert.Text = "alert('保存失败！');"
                Else
                    ltlAlert.Text = "alert('保存成功！');"
                End If
            Catch ex As Exception
                ltlAlert.Text = "alert('数据库操作失败！请联系管理员！');"
            End Try
        Else
            ltlAlert.Text = "alert('请先至少指定一个人员！');"
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("Test_exam_view.aspx?id=" & Request.QueryString("flowId"), True)
    End Sub

    Protected Sub ckball_CheckedChanged(sender As Object, e As EventArgs) Handles ckball.CheckedChanged

        Dim i As Integer
        If ckball.Checked = True Then

            For i = 0 To cbl_user.Items.Count - 1
                cbl_user.Items(i).Selected = True

            Next
        Else
            For i = 0 To cbl_user.Items.Count - 1
                cbl_user.Items(i).Selected = False

            Next
        End If


        

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
End Class
