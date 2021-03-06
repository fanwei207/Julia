Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc

    Partial Class wo_cc_permission
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

                loadUser()

                BindData()
            End If
        End Sub

        Function loadDepartment() As Boolean
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
        End Function
        Function loadUser() As Boolean
            While DropDownList1.Items.Count > 0
                DropDownList1.Items.RemoveAt(0)
            End While

            If role.SelectedIndex = 0 Then
                Exit Function
            End If

            Dim id As Integer = 0
            If Request("uid") <> Nothing Then
                id = Request("uid")
                role.Enabled = False
                role.SelectedIndex = 0
                ddlCompany.SelectedValue = Session("PlantCode")
                ddlCompany.Enabled = False
            End If

            If id > 0 Then
                strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where  plantCode='" & ddlCompany.SelectedValue & "' and deleted=0 and leavedate is null and isactive=1 and userID=" & id & " and organizationID=" & Session("orgID") & " Order by UserNo "
            Else

                strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where  plantCode='" & ddlCompany.SelectedValue & "' and deleted=0 and isactive=1 and leavedate is null and DepartmentID=" & role.SelectedValue & " and organizationID=" & Session("orgID") & " Order by UserNo "

            End If
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1) & "-" & .Rows(i).Item("userno"))
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
            If DropDownList1.Items.Count = 0 Then
                Exit Sub
            End If

            Dim conn As OdbcConnection = Nothing
            Dim comm As OdbcCommand = Nothing
            Dim dr As OdbcDataReader
            Dim i As Integer = 0
            Dim n As Integer = 0
            Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
            ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

            'Dim sql As String = "Select cc_ctr, cc_desc from PUB.cc_mstr where cc_active=1 and cc_domain='szx'"
            Dim strPlant As String = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, "Select PlantCode From plants Where plantID = " & Session("PlantCode"))
            Dim sql As String = "Select cc_ctr, cc_desc from PUB.cc_mstr where cc_active=1 and cc_domain= '" & strPlant & "'"
            sql &= " order by cc_ctr "
            Try
                conn = New OdbcConnection(connectionString)
                conn.Open()
                comm = New OdbcCommand(sql, conn)
                dr = comm.ExecuteReader()
                While (dr.Read())
                    If dr.GetValue(0).ToString() <> "" Then
                        item = New ListItem(dr.GetValue(1).ToString() & "-" & dr.GetValue(0).ToString())
                        item.Value = dr.GetValue(0).ToString()
                        CheckBoxList1.Items.Add(item)

                        strSQL = "SELECT perm_id From tcpc0.dbo.wo_cc_permission cp "
                        strSQL &= " where perm_userID=" & DropDownList1.SelectedValue & " and perm_ccid='" & dr.GetValue(0).ToString() & "'"
                        If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
                            CheckBoxList1.Items(i).Selected = True
                        End If
                        i = i + 1
                    End If
                End While
                dr.Close()
                conn.Close()

            Catch oe As OdbcException
                Response.Write(oe.Message)
                ltlAlert.Text = "alert('QAD服务暂时无法连接，请稍后');"
                n = 1
            Finally
                If conn.State = System.Data.ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
            If n <> 1 Then
                comm.Dispose()
                conn.Dispose()
            End If

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
                strSQL = "SELECT perm_id From tcpc0.dbo.wo_cc_permission Where perm_userid=" & DropDownList1.SelectedItem.Value & " and perm_ccid='" & CheckBoxList1.Items(i).Value & "'"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                If ds.Tables(0).Rows.Count > 0 Then
                    If CheckBoxList1.Items(i).Selected = False Then
                        strSQL = "Delete From tcpc0.dbo.wo_cc_permission Where perm_userid=" & DropDownList1.SelectedItem.Value & " and perm_ccid='" & CheckBoxList1.Items(i).Value & "'"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                    End If
                Else
                    If CheckBoxList1.Items(i).Selected = True Then
                        strSQL = "Insert Into tcpc0.dbo.wo_cc_permission(perm_userid,perm_username,perm_ccid,perm_ccname,createdDate,createdBy) Values(" & DropDownList1.SelectedItem.Value & ",N'" & DropDownList1.SelectedItem.Text & "','" & CheckBoxList1.Items(i).Value & "',N'" & CheckBoxList1.Items(i).Text & "',getdate(),'" & Session("uID") & "')"
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

        Private Sub ddlCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCompany.SelectedIndexChanged
            loadDepartment()
            loadUser()
            BindData()
        End Sub
    End Class

End Namespace

