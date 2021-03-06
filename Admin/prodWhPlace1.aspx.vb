Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class prodWhPlace1
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass
        Dim strSQL As String

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
                Dim ds As DataSet

                strSQL = "SELECT warehouseID,name From warehouse order by name"
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
                DropDownList1.SelectedIndex = 0

                ClearUselessData()

                BindData()

            End If
        End Sub

        Private Sub BindData()
            Dim ds As DataSet
            strSQL = " SELECT distinct pwp.placeID,pwp.name From warehousePlace pwp Inner Join warehouse p on p.warehouseid=pwp.warehouseid "
            If DropDownList1.SelectedIndex >= 0 Then
                strSQL &= " where(p.warehouseid = " & DropDownList1.SelectedValue & ")"
            End If
            strSQL &= " Order by pwp.placeID desc "
            Session("EXSQL") = strSQL
            Session("EXTitle") = "<b>编码</b>~^300^<b>库位名称</b>~^"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
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

        Private Sub DataGrid1_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub
        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            If DataGrid1.EditItemIndex = -1 Then
                If (DropDownList1.Items.Count > 0) Then
                    strSQL = "Insert Into warehouseplace(Name,warehouseid) Values(N'(新的类型)','" & DropDownList1.SelectedValue & "')"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                    DataGrid1.CurrentPageIndex = 0

                    DataGrid1.EditItemIndex = 0
                    BindData()
                End If
            Else
                ltlAlert.Text = "alert('请完成上一库位的编辑，然后才能添加。');"
            End If
        End Sub

        Sub ClearUselessData()
            strSQL = "delete from warehouseplace where name =N'(新的类型)' or name is null "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
        End Sub

        Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            DataGrid1.EditItemIndex = -1
            DataGrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Private Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
            ClearUselessData()
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
            ltlAlert.Text = ""
            Dim str As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('仓库名称不能为空.')"
                Exit Sub
            End If
            strSQL = "Select count(*) From warehouseplace Where name=N'" & str & "' and placeID<>" & e.Item.Cells(0).Text
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
                ltlAlert.Text = "alert('仓库名称已经存在！');"
                Exit Sub
            Else
                strSQL = "update warehouseplace set name=N'" & str & "' where placeID=" & e.Item.Cells(0).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = -1
                'Response.Redirect(chk.urlRand("/admin/prodWhPlace1.aspx"), True)
                BindData()
            End If
        End Sub

        Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            ltlAlert.Text = ""
            If (e.CommandName = "DeleteBtn") Then
                strSQL = " Delete From warehouseplace Where placeID='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                DataGrid1.EditItemIndex = -1
                'Response.Redirect(chk.urlRand("/admin/prodWhPlace1.aspx"), True)
                BindData()
            End If
        End Sub

        Private Sub DataGrid1_EditCommand1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

    End Class

End Namespace
