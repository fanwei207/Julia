Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class errorToEmail
        Inherits BasePage
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass
        Dim strSql As String

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
                'check 

                'delete the useless data
                ClearUselessData()

                'load
                EmailList()
            End If
        End Sub
        Sub EmailList()
            Dim ds As DataSet
            strSql = " SELECT id,type,ISNULL(email,'') as email " _
                   & " From ApplicationEmails  " _
                   & " Where type='" & SelectTypeDropDown.SelectedValue & "' AND (deleted=0 or deleted is null) " _
                   & " Order by id desc "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("email", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    countLabel.Text = "共有<font color='#996633'>" & .Rows.Count & "</font>条记录"
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("id") = .Rows(i).Item("id").ToString().Trim()
                        dr1.Item("email") = .Rows(i).Item("email").ToString().Trim()
                        dt.Rows.Add(dr1)
                    Next
                Else
                    countLabel.Text = "共有<font color='#996633'>0</font>条记录"
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                EmailDataGrid.DataSource = dv
                EmailDataGrid.DataBind()
            Catch
            End Try
        End Sub
        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles EmailDataGrid.CancelCommand
            ClearUselessData()

            EmailDataGrid.EditItemIndex = -1
            EmailList()
        End Sub
        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles EmailDataGrid.UpdateCommand
            Dim str As String = CType(e.Item.FindControl("addrTextBox"), TextBox).Text
            strSql = "update ApplicationEmails set email=N'" & str & "' where id='" & e.Item.Cells(0).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            EmailDataGrid.EditItemIndex = -1
            EmailList()
        End Sub
        Private Sub EmailDataGrid_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles EmailDataGrid.EditCommand
            EmailDataGrid.EditItemIndex = e.Item.ItemIndex
            EmailList()
        End Sub
        Private Sub SelectTypeDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectTypeDropDown.SelectedIndexChanged
            EmailDataGrid.EditItemIndex = -1
            EmailList()
        End Sub
        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles EmailDataGrid.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                strSql = "update  ApplicationEmails set deleted='1' where id = '" & e.Item.Cells(0).Text() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                EmailDataGrid.EditItemIndex = -1
                EmailList()
            End If
        End Sub
        Private Sub EmailDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles EmailDataGrid.PageIndexChanged
            EmailDataGrid.CurrentPageIndex = e.NewPageIndex
            EmailDataGrid.EditItemIndex = -1
            EmailList()
        End Sub
        Private Sub EmailDataGrid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles EmailDataGrid.ItemCreated
            Select Case e.Item.ItemType
                Case ListItemType.EditItem
                    Dim saveBtn As LinkButton
                    saveBtn = CType(e.Item.Cells(3).Controls(0), LinkButton)
                    Dim ctlName As String = "EmailDataGrid__ctl" & e.Item.ItemIndex + 3 & "_addrTextBox"
                    saveBtn.Attributes.Add("onclick", "if(document.getElementById('" & ctlName & "').value.match(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/g)==null){alert('邮件地址格式不正确！');return false;}")
            End Select
        End Sub

        Private Sub AddBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddBtn.Click
            If EmailDataGrid.EditItemIndex = -1 Then
                Dim strSQL As String
                strSQL = "Insert Into ApplicationEmails(type,email,deleted) Values('" & SelectTypeDropDown.SelectedValue & "','','0')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                EmailDataGrid.CurrentPageIndex = 0

                EmailDataGrid.EditItemIndex = 0
                EmailList()
            Else
                ltlAlert.Text = "alert('请完成上一邮件地址的编辑，然后才能添加。');"
            End If
        End Sub
        Sub ClearUselessData()
            strSql = "delete from ApplicationEmails where email ='' or email is null "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        End Sub
    End Class

End Namespace
