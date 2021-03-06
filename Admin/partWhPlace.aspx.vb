Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class partWhPlace
        Inherits BasePage
        Shared sortOrder As String = ""
        Shared sortDir As String = "ASC"
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
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


                Dim item As ListItem
                Dim ds As DataSet

                item = New ListItem("--")
                item.Value = 0
                DropDownList1.Items.Add(item)

                strSql = "SELECT plantID,description From Plants order by plantCode"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
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
                partWhPlaceListLoad()
            End If
        End Sub
        Private Sub partWhPlaceListLoad()
            Dim ds As DataSet
            If DropDownList1.SelectedIndex = 0 Then
                strSql = " SELECT pwp.part_wh_place_id,pwp.name,p.plantCode From Part_wh_place pwp Inner Join Plants p on p.plantID=pwp.plantID Order by pwp.part_wh_place_id desc "
            Else
                strSql = " SELECT pwp.part_wh_place_id,pwp.name,p.plantCode From Part_wh_place pwp Inner Join Plants p on p.plantID=pwp.plantID where pwp.plantID='" & DropDownList1.SelectedValue & "' Order by pwp.part_wh_place_id desc "
            End If
            Session("EXSQL") = strSql
            Session("EXTitle") = "<b>编码</b>~^300^<b>库位名称</b>~^<b>公司代码</b>~^"

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                partWhPlaceList.DataSource = dv
                partWhPlaceList.DataBind()
            Catch
            End Try
        End Sub
        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles partWhPlaceList.CancelCommand
            ClearUselessData()
            partWhPlaceList.EditItemIndex = -1
            partWhPlaceListLoad()
        End Sub
        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles partWhPlaceList.UpdateCommand
            ltlAlert.Text = ""
            Dim str As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text.Trim
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空.')"
                Exit Sub
            End If
            strSql = "Select count(*) From Part_wh_place Where name=N'" & str & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                ltlAlert.Text = "alert('库位名称已经存在！');"
                Exit Sub
            Else
                strSql = "update Part_wh_place set name=N'" & str & "' where part_wh_place_id=" & e.Item.Cells(0).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                partWhPlaceList.EditItemIndex = -1
                Response.Redirect(chk.urlRand("/admin/partWhPlace.aspx"), True)
            End If
        End Sub
        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles partWhPlaceList.ItemCommand
            ltlAlert.Text = ""
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                strSql = " Select count(*) From Part_inv Where part_wh_place_id='" & e.Item.Cells(0).Text.Trim() & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                    ltlAlert.Text = "alert('库位名称为" & e.Item.Cells(1).Text.Trim() & "的已经使用了，无法删除！');"
                    Exit Sub
                End If
                strSql = " Delete From Part_wh_place Where part_wh_place_id='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                strSql = " Delete From User_Part_Warehouse Where part_wh_place_id='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                partWhPlaceList.EditItemIndex = -1
                Response.Redirect(chk.urlRand("/admin/partWhPlace.aspx"), True)
            ElseIf (e.CommandName.CompareTo("rightBtn") = 0) Then
                Response.Redirect(chk.urlRand("/admin/partWhPlaceLink.aspx?id=" & e.Item.Cells(0).Text.Trim()), True)
            End If
        End Sub
        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles partWhPlaceList.PageIndexChanged
            partWhPlaceList.CurrentPageIndex = e.NewPageIndex
            partWhPlaceList.EditItemIndex = -1
            partWhPlaceListLoad()
        End Sub
        Private Sub addBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addBtn.Click
            If partWhPlaceList.EditItemIndex = -1 Then
                If DropDownList1.SelectedIndex = 0 Then
                    ltlAlert.Text = "alert('请选择公司。');"
                    Exit Sub
                End If
                strSql = "Insert Into Part_wh_place(Name,plantID)Values('','" & DropDownList1.SelectedValue & "')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                sortOrder = "gsort"
                sortDir = "ASC"
                partWhPlaceList.CurrentPageIndex = 0

                partWhPlaceList.EditItemIndex = 0
                partWhPlaceListLoad()
            Else
                ltlAlert.Text = "alert('请完成上一库位的编辑，然后才能添加。');"
            End If
        End Sub
        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles partWhPlaceList.EditCommand
            partWhPlaceList.EditItemIndex = e.Item.ItemIndex
            partWhPlaceListLoad()
        End Sub
        Sub ClearUselessData()
            strSql = "delete from Part_wh_place where name ='' or name is null "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        End Sub

        Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            partWhPlaceList.EditItemIndex = -1
            partWhPlaceList.CurrentPageIndex = 0
            partWhPlaceListLoad()
        End Sub
    End Class

End Namespace
