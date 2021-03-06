Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class prodWhPlace
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

                ClearUselessData()
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim ds As DataSet
            strSQL = " SELECT pwp.warehouseID,pwp.name From warehouse pwp Order by pwp.warehouseID desc "
            Session("EXSQL") = strSQL
            Session("EXTitle") = "<b>编码</b>~^300^<b>仓库名称</b>~^"

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
                strSQL = "Insert Into warehouse(Name) Values(N'(新的类型)')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                DataGrid1.CurrentPageIndex = 0

                DataGrid1.EditItemIndex = 0
                BindData()
            Else
                ltlAlert.Text = "alert('请完成上一库位的编辑，然后才能添加。');"
            End If
        End Sub

        Sub ClearUselessData()
            strSQL = "delete from warehouse where name =N'(新的类型)' or name is null "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
        End Sub

        Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            strSQL = "Select count(*) From warehouse Where name=N'" & str & "' and warehouseID<>" & e.Item.Cells(0).Text
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
                ltlAlert.Text = "alert('仓库名称已经存在！');"
                Exit Sub
            Else
                strSQL = "update warehouse set name=N'" & str & "' where warehouseID=" & e.Item.Cells(0).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = -1
                Response.Redirect(chk.urlRand("/admin/prodWhPlace.aspx"), True)
            End If
        End Sub

        Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            ltlAlert.Text = ""
            If (e.CommandName = "DeleteBtn") Then
                strSQL = " Select count(*) From Product_inv Where warehouseID='" & e.Item.Cells(0).Text.Trim() & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
                    ltlAlert.Text = "alert('仓库名称为" & e.Item.Cells(1).Text.Trim() & "的已经使用了，无法删除！');"
                    Exit Sub
                End If
                strSQL = " Select count(*) From Part_inv Where warehouseID='" & e.Item.Cells(0).Text.Trim() & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
                    ltlAlert.Text = "alert('仓库名称为" & e.Item.Cells(1).Text.Trim() & "的已经使用了，无法删除！');"
                    Exit Sub
                End If

                strSQL = " Delete From warehouse Where warehouseID='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                DataGrid1.EditItemIndex = -1
                Response.Redirect(chk.urlRand("/admin/prodWhPlace.aspx"), True)
            ElseIf (e.CommandName.CompareTo("rightBtn") = 0) Then
                Response.Redirect(chk.urlRand("/admin/productWhPlaceLink.aspx?id=" & e.Item.Cells(0).Text.Trim()), True)
            End If
        End Sub

        Private Sub DataGrid1_EditCommand1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

    End Class

End Namespace
