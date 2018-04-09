Imports System
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class KB_docChoose
    Inherits System.Web.UI.Page

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
            txb_docid.Text = Request("docid")
            txb_typeid.Text = Request("typeid")

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
            loaduser()
        End If

    End Sub
    Function loadDepartment()
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
    End Function
    Function loadUser()
        While cbl_user.Items.Count > 0
            cbl_user.Items.RemoveAt(0)
        End While

        If ddl_department.SelectedIndex = 0 Then
            Exit Function
        End If


        strSQL = "SELECT userID,userName,userno,email From tcpc0.dbo.users Where  plantCode='" & ddl_Company.SelectedValue & "' and deleted=0 and isactive=1 and leavedate is null and DepartmentID=" & ddl_department.SelectedValue & " and organizationID=" & Session("orgID") & " Order by UserName "

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
        loaduser()
    End Sub

    Private Sub ddl_department_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_department.SelectedIndexChanged
        loaduser()
    End Sub

    Private Sub cbl_user_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbl_user.SelectedIndexChanged
        If cbl_user.Items.Count = 0 Then
            Exit Sub
        End If
        Dim i As Integer
        For i = 0 To cbl_user.Items.Count - 1

            If cbl_user.Items(i).Selected = False Then
                txb_chooseid.Text = txb_chooseid.Text.Replace(cbl_user.Items(i).Value + ",", "")
                txb_choose.Text = txb_choose.Text.Replace(cbl_user.Items(i).Text.Substring(0, cbl_user.Items(i).Text.IndexOf("~")) + ",", "")
            ElseIf cbl_user.Items(i).Selected Then
                If txb_choose.Text.IndexOf(cbl_user.Items(i).Text.Substring(0, cbl_user.Items(i).Text.IndexOf("~"))) = -1 Then
                    txb_choose.Text = txb_choose.Text + cbl_user.Items(i).Text.Substring(0, cbl_user.Items(i).Text.IndexOf("~")) + ","
                    txb_chooseid.Text = txb_chooseid.Text + cbl_user.Items(i).Value + ","
                End If
            End If
        Next

    End Sub
End Class

End Namespace
