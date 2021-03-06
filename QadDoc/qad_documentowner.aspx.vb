Imports System
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
    Partial Class qad_documentowner
        Inherits BasePage 
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
                txb_owner.Text = Server.UrlDecode(Request("un"))
                txb_ownerid.Text = Request("uid") 

                ddl_Company.SelectedIndex = 0
                strSQL = "SELECT plantID,description From tcpc0.dbo.plants where plantID<10 order by plantid"
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
            strSQL = "SELECT departmentID,name From tcpc" & ddl_Company.SelectedValue & ".dbo.departments where issalary=1 order by name"
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

        Function loadUser() As Boolean
            While cbl_user.Items.Count > 0
                cbl_user.Items.RemoveAt(0)
            End While

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
                    Exit Function
                End If
            End If
            strSQL &= " Order by UserName"

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
        End Function

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

            txb_chooseid.Text = ""
            txb_choose.Text = ""

            Dim i As Integer
            For i = 0 To cbl_user.Items.Count - 1
                If cbl_user.Items(i).Selected Then
                    txb_choose.Text = cbl_user.Items(i).Text.Substring(0, cbl_user.Items(i).Text.IndexOf("~"))
                    txb_chooseid.Text = cbl_user.Items(i).Value
                End If
            Next

            cbl_user.ClearSelection()

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

        Protected Sub btn_ok1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ok1.Click
            strSQL = " Insert Into qaddoc.dbo.DocumentType(typename,createdBy,createdName,createdDate,isDeleted) "
            strSQL &= " Select typename,'" & txb_ownerid.Text & "',N'" & txb_owner.Text & "',getdate(),typeid from qaddoc.dbo.DocumentType  "
            strSQL &= "  where typeid>0 and typeid='" & txb_docid.Text & "' "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            strSQL = " Update qaddoc.dbo.DocumentType set createdBy='" & txb_chooseid.Text & "'"
            strSQL &= " ,createdName=N'" & txb_choose.Text & "' where typeid>0 "
            strSQL &= " and typeid='" & txb_docid.Text & "' and createdBy='" & txb_ownerid.Text & "' and createdName=N'" & txb_owner.Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            strSQL = " Update qaddoc.dbo.documentAccess set doc_acc_userid='" & txb_chooseid.Text & "'"
            strSQL &= " ,doc_acc_username=N'" & txb_choose.Text & "' where doc_acc_catid>0 and doc_acc_level = 0 "
            strSQL &= " and doc_acc_catid='" & txb_docid.Text & "' and doc_acc_userid='" & txb_ownerid.Text & "' and doc_acc_username=N'" & txb_owner.Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            ltlAlert.Text = "window.opener.location = window.opener.location;window.close();"
        End Sub
    End Class

End Namespace
