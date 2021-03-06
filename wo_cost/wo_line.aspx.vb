Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class wo_line
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim nRet As Integer
        Dim item As ListItem


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

                Dim ls As ListItem
                Select Case Session("PlantCode")
                    Case "1"
                        ls = New ListItem
                        ls.Value = "1000"
                        ls.Text = "1000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2100"
                        ls.Text = "2100"
                        dd_site.Items.Add(ls)
                    Case "2"
                        ls = New ListItem
                        ls.Value = "2000"
                        ls.Text = "2000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1200"
                        ls.Text = "1200"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "3000"
                        ls.Text = "3000"
                        dd_site.Items.Add(ls)
                    Case "5"
                        ls = New ListItem
                        ls.Value = "4000"
                        ls.Text = "4000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1400"
                        ls.Text = "1400"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2400"
                        ls.Text = "2400"
                        dd_site.Items.Add(ls)
                End Select
                BindData()
            End If
        End Sub
        Sub BindData()
            StrSql = " select g.wol_id, g.wol_site, g.wol_cc, g.wol_name,g.createdDate,g.createdBy from tcpc0.dbo.wo_line g "
            StrSql &= " where g.deletedBy is null "
            StrSql &= " and g.wol_site ='" & dd_site.SelectedValue & "' "
            If txb_cc.Text.Trim.Length > 0 Then
                StrSql &= " and g.wol_cc ='" & txb_cc.Text.Trim() & "' "
            End If
            If txb_name.Text.Trim.Length > 0 Then
                StrSql &= " and g.wol_name like N'%" & txb_name.Text.Trim() & "%' "
            End If
            StrSql &= " order by g.wol_site,g.wol_cc, g.wol_name"

            Session("EXTitle") = "50^<b>地点</b>~^80^<b>成本中心</b>~^130^<b>工段线</b>~^<b>日期</b>~^50^<b>创建人</b>~^"
            Session("EXSQL") = StrSql
            Session("EXHeader") = "工段线    导出日期: " & Format(DateTime.Today, "yyyy-MM-dd")

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("group_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_by", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_date", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("group_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("group_site") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("group_cc") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("group_name") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("proc_by") = .Rows(i).Item(5).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(4)) Then
                            drow.Item("proc_date") = Format(.Rows(i).Item(4), "yy-MM-dd")
                        End If
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()


            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()
        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("proc_edit") = 0 Then
                dd_site.SelectedValue = e.Item.Cells(1).Text
                txb_cc.Text = e.Item.Cells(2).Text
                txb_name.Text = e.Item.Cells(3).Text
                lbl_id.Text = e.Item.Cells(0).Text

                btn_add.Text = "修改"

                dd_site.Enabled = False
                txb_cc.ReadOnly = True

            ElseIf e.CommandName.CompareTo("proc_del") = 0 Then
                If Session("uRole") <> 1 Then
                    StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & txb_cc.Text & "' and perm_userid='" & Session("uID") & "'"
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                        ltlAlert.Text = "alert('无操作此成本中心的权限.')"
                        Exit Sub
                    End If
                End If

                StrSql = " Update tcpc0.dbo.wo_line set deletedBy='" & Session("uID") & "',deletedDate=getdate() where wol_id='" & e.Item.Cells(0).Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                txb_name.Text = ""
                lbl_id.Text = ""

                btn_add.Text = "增加"
                dd_site.Enabled = True
                txb_cc.ReadOnly = False

                BindData()
            ElseIf e.CommandName.CompareTo("proc_copy") = 0 Then
                'Dim str As String = ""
                'str = "wo_group_copy.aspx?gid=" & e.Item.Cells(0).Text & "&site=" & e.Item.Cells(1).Text & "&cc=" & e.Item.Cells(2).Text & "&gp=" & e.Item.Cells(3).Text
                'Response.Redirect(str)
            End If
        End Sub
        Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
            If Session("uRole") <> 1 Then
                StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & txb_cc.Text & "' and perm_userid='" & Session("uID") & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                    ltlAlert.Text = "alert('无操作此成本中心的权限.')"
                    Exit Sub
                End If
            End If

            If lbl_id.Text.Trim.Length > 0 Then
                '修改
                If txb_name.Text.Trim.Length <= 0 Then
                    ltlAlert.Text = "alert('请输入工段线.')"
                    Exit Sub
                ElseIf txb_name.Text.Trim.Length > 50 Then
                    ltlAlert.Text = "alert('工段线长度不能大于50.')"
                    Exit Sub
                End If

                StrSql = " Update tcpc0.dbo.wo_line set wol_name=N'" & txb_name.Text & "' where wol_id='" & lbl_id.Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            Else
                '增加
                If txb_cc.Text.Trim.Length <= 0 Then
                    ltlAlert.Text = "alert('请输入成本中心.')"
                    Exit Sub
                End If

                If txb_name.Text.Trim.Length <= 0 Then
                    ltlAlert.Text = "alert('请输入工段线.')"
                    Exit Sub
                ElseIf txb_name.Text.Trim.Length > 50 Then
                    ltlAlert.Text = "alert('工段线长度不能大于50.')"
                    Exit Sub
                Else
                    StrSql = " Select wol_id from tcpc0.dbo.wo_line where deletedBy is null and  wol_site='" & dd_site.SelectedValue & "' and wol_cc='" & txb_cc.Text & "' and wol_name=N'" & txb_name.Text & "'"
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                        ltlAlert.Text = "alert('此工段线已存在.')"
                        Exit Sub
                    End If
                End If


                StrSql = " Insert into tcpc0.dbo.wo_line(wol_site,wol_cc,wol_name,createdDate,createdBy) "
                StrSql &= " values('" & dd_site.SelectedValue & "','" & txb_cc.Text & "',N'" & txb_name.Text & "',getdate(),'" & Session("uID") & "')"

                'Response.Write(StrSql)
                'Exit Sub

                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)



            End If


            txb_name.Text = ""
            lbl_id.Text = ""

            btn_add.Text = "增加"
            dd_site.Enabled = True
            txb_cc.ReadOnly = False

            BindData()
        End Sub

        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            lbl_id.Text = ""
            btn_add.Text = "增加"
            dd_site.Enabled = True

            txb_cc.ReadOnly = False

            BindData()
        End Sub

        Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click

            ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx" & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"
        End Sub

        Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(5)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确认要删除吗?');")

                Case ListItemType.AlternatingItem
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(5)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确认要删除吗?');")

                Case ListItemType.EditItem
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(5)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确认要删除吗?');")
            End Select
        End Sub

        Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
            txb_name.Text = ""

            lbl_id.Text = ""

            btn_add.Text = "增加"
            dd_site.Enabled = True

            txb_cc.ReadOnly = False
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













