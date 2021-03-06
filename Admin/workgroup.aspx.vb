Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class workgroup
        Inherits BasePage
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

                departmentDropDownList()
                BindData()
            End If
        End Sub

        Sub departmentDropDownList()
            Dim item As ListItem
            Dim query As String
            Dim dst As DataSet


            item = New ListItem("--")
            item.Value = 0
            department.Items.Add(item)
            department.SelectedIndex = 0

            query = "SELECT departmentID,name From Departments WHERE isSalary='1' Order by code  "

            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        department.Items.Add(item)
                    Next
                End If
            End With
            dst.Reset()
        End Sub

        Sub workshopDropDownList()
            Dim item As ListItem
            Dim query As String
            Dim dst As DataSet
            workshop.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
            workshop.Items.Add(item)
            workshop.SelectedIndex = 0

            query = " SELECT w.id, w.name FROM Workshop w INNER JOIN departments d ON w.departmentID = d.departmentID " _
                  & " WHERE d.name=N'" & department.SelectedItem.Text.Trim() & "' AND w.workshopID IS NULL Order by w.code  "

            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        workshop.Items.Add(item)
                    Next
                End If
            End With
            dst.Reset()
        End Sub

        Private Sub department_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles department.SelectedIndexChanged
            If department.SelectedValue <> 0 Then
                workshopDropDownList()
                BindData()
            Else
                workshop.Items.Clear()
                BindData()
            End If
        End Sub

        Private Sub workshop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles workshop.SelectedIndexChanged
            If workshop.SelectedValue <> 0 Then
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim query As String
            Dim ds As DataSet
            query = " SELECT id, code,name From workshop where "
            If department.SelectedIndex > 0 Then
                query &= " departmentID=  " & department.SelectedValue & " AND "
            Else
                query &= " departmentID IS NULL AND "
            End If
            If workshop.SelectedIndex > 0 Then
                query &= " workshopID= " & workshop.SelectedValue
            Else
                query &= " workshopID=-1 "
            End If
            query &= "  Order by id DESC "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("gsort") = i + 1
                        drow.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("code") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("Name") = .Rows(i).Item(2).ToString().Trim()
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dtl)

            Try
                dgworkgroup.DataSource = dv
                dgworkgroup.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgworkgroup_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgworkgroup.SortCommand
            Session("orderby") = e.SortExpression.ToString()
            If Session("orderdir") = " ASC" Then
                Session("orderdir") = " DESC"
            Else
                Session("orderdir") = " ASC"
            End If
            BindData()
        End Sub

        Private Sub dgworkgroup_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgworkgroup.PageIndexChanged
            dgworkgroup.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub dgworkgroup_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgworkgroup.EditCommand
            dgworkgroup.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgworkgroup.CancelCommand
            dgworkgroup.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgworkgroup.UpdateCommand
            ltlAlert.Text = ""
            Dim query As String
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

            query = "update workshop set name=N'" & str & "',code=N'" & str1 & "' where id=" & e.Item.Cells(1).Text
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)

            dgworkgroup.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgworkgroup.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim query As String
                query = "delete from workshop where id = " & e.Item.Cells(1).Text()
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)
                dgworkgroup.EditItemIndex = -1
                BindData()
            End If
        End Sub

        Private Sub dgworkgroup_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgworkgroup.ItemCreated
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing ButtonColumn
                    myDeleteButton = e.Item.Cells(5)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")

                Case ListItemType.AlternatingItem
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(5)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")

                Case ListItemType.EditItem
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(5)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")
            End Select
        End Sub

        Private Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
            ltlAlert.Text = ""
            Dim query As String
            If department.SelectedIndex > 0 Then
                If workshop.SelectedValue > 0 Then
                    query = " Insert Into workshop(code,name,departmentID,workshopID) " _
                          & " Values(N'(新的工段)','0'," & department.SelectedValue & "," & workshop.SelectedValue & ")"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)
                    dgworkgroup.EditItemIndex = 0
                    BindData()
                Else
                    ltlAlert.Text = "alert('请选择一个工段!');"
                    Exit Sub
                End If
            Else
                ltlAlert.Text = "alert('请选择一个部门!');"
                Exit Sub
            End If
        End Sub
    End Class

End Namespace
