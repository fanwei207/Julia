Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Namespace tcpc 
Partial Class user_document
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal
    'Protected WithEvents RadioButtonList1 As System.Web.UI.WebControls.RadioButtonList
    'Protected WithEvents CheckBoxList1 As System.Web.UI.WebControls.CheckBoxList
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
           
            item = New ListItem("--")
            item.Value = 0
                ddlRole.Items.Add(item)
                ddlRole.SelectedIndex = 0
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
            strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where  plantCode='" & Session("PlantCode") & "' and deleted=0 and isactive=1 and leavedate is null and userID=" & id & " and organizationID=" & Session("orgID") & " Order by UserName "
        Else
            strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where  plantCode='" & Session("PlantCode") & "' and deleted=0 and isactive=1 and leavedate is null and DepartmentID=" & ddlRole.SelectedValue & " and organizationID=" & Session("orgID") & " Order by UserName "
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
    Private Sub BindData()
        If ddlUser.Items.Count = 0 Then
            Exit Sub
        End If

        strSQL = " Select dt.typeid,dt.typename,isnull(ud.ispublish,1),ud.userID From qaddoc.dbo.documenttype dt " _
               & " Left Outer Join qaddoc.dbo.User_Document ud On ud.typeID=dt.typeid and ud.userID=" & ddlUser.SelectedValue _
               & " Order by dt.typename "
        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("chk", System.Type.GetType("System.Boolean")))
        dt.Columns.Add(New DataColumn("docType", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("ispublish", System.Type.GetType("System.Boolean")))
        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("id") = .Rows(i).Item(0)
                    dr1.Item("docType") = .Rows(i).Item(1).ToString.Trim
                    dr1.Item("ispublish") = Not .Rows(i).Item(2)
                    If Not .Rows(i).IsNull(3) Then
                        dr1.Item("chk") = True
                    Else
                        dr1.Item("chk") = False
                    End If
                    dt.Rows.Add(dr1)
                Next i
            End If
        End With
        Dim dv As DataView
        dv = New DataView(dt)
        Try
            DataGrid1.DataSource = dv
            DataGrid1.DataBind()
        Catch
        End Try
        dst.Reset()
    End Sub

    Private Sub ddlUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlUser.SelectedIndexChanged
        DataGrid1.SelectedIndex = -1
        BindData()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If ddlUser.Items.Count = 0 Then
            Exit Sub
        End If
        Dim i As Integer
        strSQL = " Delete From qaddoc.dbo.User_Document Where userID=" & ddlUser.SelectedItem.Value
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

        For i = 0 To DataGrid1.Items.Count - 1
            If CType(DataGrid1.Items(i).FindControl("chkType"), CheckBox).Checked = True Then
                strSQL = "Insert Into qaddoc.dbo.User_Document(userID,typeID,ispublish) Values(" & ddlUser.SelectedItem.Value & "," & DataGrid1.Items(i).Cells(0).Text & " "
                If CType(DataGrid1.Items(i).FindControl("chkpublish"), CheckBox).Checked = True Then
                    strSQL &= ",0)"
                Else
                    strSQL &= ",1)"
                End If
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            End If
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

