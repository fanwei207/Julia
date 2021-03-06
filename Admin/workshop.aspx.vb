Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class Workshop
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass
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

                Dim item As ListItem
                Dim strSQL As String
                Dim ds As DataSet


                item = New ListItem("--")
                item.Value = 0
                DropDownList1.Items.Add(item)
                DropDownList1.SelectedIndex = 0

                strSQL = "SELECT departmentID,name From Departments WHERE isSalary='1' Order by code  "

                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            DropDownList1.Items.Add(item)
                        Next
                    End If
                End With
                ds.Reset()

                BindData()
            End If
        End Sub
        Private Sub BindData()
            lbl_delete.Visible = False
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT id, code,name,isnull(computer,0) From workshop where "
            If DropDownList1.SelectedIndex > 0 Then
                strSQL = strSQL & " departmentID=  " & DropDownList1.SelectedValue & " AND "
            End If
            strSQL = strSQL & "  workshopID IS NULL Order by id desc"
            'Response.Write(strSQL)
            'Exit Sub
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("comp", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("code") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("Name") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("comp") = .Rows(i).Item(3).ToString().Trim()
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub

        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub
        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
            Dim strSQL As String
            Dim str As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空.')"
                Exit Sub
            End If

            Dim str1 As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text
            If (str1.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('编号不能为空.')"
                Exit Sub
            End If

            Dim str2 As String = CType(e.Item.Cells(4).Controls(0), TextBox).Text
            If (str2.Trim.Length <= 0) Then
                str2 = "0"
            End If

            strSQL = "update workshop set name=N'" & str & "',code=N'" & str1 & "' ,computer='" & str2 & "' where id=" & e.Item.Cells(1).Text
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strSQL As String
                strSQL = "delete from workshop where id = " & e.Item.Cells(1).Text() & "  and Not Exists(select top(2) * from  tcpc0.dbo.Users U where U.workshopID= id) "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = -1
                BindData()
                lbl_delete.Visible = True
            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            If DropDownList1.SelectedIndex > 0 Then
                Dim strSQL As String
                strSQL = "Insert Into workshop(code,name,departmentID,computer) Values(N'(新的工段)','0'," & DropDownList1.SelectedValue & ",0)"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = 0
                BindData()
            Else
                ltlAlert.Text = "alert('请选择一个部门!')"
                Exit Sub
            End If
        End Sub
        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            'DataGrid1.EditItemIndex = -1
            BindData()
        End Sub
    End Class

End Namespace
