Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class wo_group
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
            StrSql = " select g.wog_id, g.wog_site, g.wog_cc, g.wog_name,isnull(gd.qty,0),g.createdDate,g.createdBy from tcpc0.dbo.wo_group g Left Outer Join "
            StrSql &= " (Select count(wod_id) as qty,wod_group_id from tcpc0.dbo.wo_group_detail group by wod_group_id) gd "
            StrSql &= " on gd.wod_group_id=g.wog_id "
            StrSql &= " where g.deletedBy is null "
            StrSql &= " and g.wog_site ='" & dd_site.SelectedValue & "' "
            If txb_cc.Text.Trim.Length > 0 Then
                StrSql &= " and g.wog_cc ='" & txb_cc.Text.Trim() & "' "
            End If
            If txb_name.Text.Trim.Length > 0 Then
                StrSql &= " and g.wog_name like N'%" & txb_name.Text.Trim() & "%' "
            End If
            StrSql &= " order by g.wog_site,g.wog_cc, g.wog_name"

            Session("EXTitle") = "50^<b>地点</b>~^80^<b>成本中心</b>~^130^<b>用户组</b>~^<b>人数</b>~^<b>日期</b>~^50^<b>创建人</b>~^"
            Session("EXSQL") = StrSql
            Session("EXHeader") = "用户组    导出日期: " & Format(DateTime.Today, "yyyy-MM-dd")

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim total As Integer = 0

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("group_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_by", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_member", System.Type.GetType("System.String")))

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
                        drow.Item("proc_by") = .Rows(i).Item(6).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(5)) Then
                            drow.Item("proc_date") = Format(.Rows(i).Item(5), "yy-MM-dd")
                        End If
                        drow.Item("group_member") = "<u>" & .Rows(i).Item(4).ToString().Trim() & "</u>"
                        total = total + .Rows(i).Item(4)
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            lbl_qty.Text = "总人数： " & total.ToString()

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

                StrSql = " Update tcpc0.dbo.wo_group set deletedBy='" & Session("uID") & "',deletedDate=getdate() where wog_id='" & e.Item.Cells(0).Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                StrSql = " Delete from tcpc0.dbo.wo_group_detail  where wod_group_id='" & e.Item.Cells(0).Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                txb_name.Text = ""
                lbl_id.Text = ""

                btn_add.Text = "增加"
                dd_site.Enabled = True
                txb_cc.ReadOnly = False

                BindData()
            ElseIf e.CommandName.CompareTo("proc_list") = 0 Then
                'Response.Redirect("wo_group_list.aspx?gid=" & e.Item.Cells(0).Text)
                Dim str As String = ""
                str = "wo_group_list.aspx?gid=" & e.Item.Cells(0).Text
                Response.Redirect(str)
                'ltlAlert.Text = "var w=window.open('wo_group_list.aspx?gid=" & e.Item.Cells(0).Text & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"
            ElseIf e.CommandName.CompareTo("proc_copy") = 0 Then
                Dim str As String = ""
                str = "wo_group_copy.aspx?gid=" & e.Item.Cells(0).Text & "&site=" & e.Item.Cells(1).Text & "&cc=" & e.Item.Cells(2).Text & "&gp=" & e.Item.Cells(3).Text
                Response.Redirect(str)
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
                    ltlAlert.Text = "alert('请输入用户组.')"
                    Exit Sub
                ElseIf txb_name.Text.Trim.Length > 50 Then
                    ltlAlert.Text = "alert('用户组长度不能大于50.')"
                    Exit Sub
                End If

                StrSql = " Update tcpc0.dbo.wo_group set wog_name=N'" & txb_name.Text & "' where wog_id='" & lbl_id.Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            Else
                '增加
                If txb_cc.Text.Trim.Length <= 0 Then
                    ltlAlert.Text = "alert('请输入成本中心.')"
                    Exit Sub
                End If

                If txb_name.Text.Trim.Length <= 0 Then
                    ltlAlert.Text = "alert('请输入用户组.')"
                    Exit Sub
                ElseIf txb_name.Text.Trim.Length > 50 Then
                    ltlAlert.Text = "alert('用户组长度不能大于50.')"
                    Exit Sub
                Else
                    StrSql = " Select wog_id from tcpc0.dbo.wo_group where deletedBy is null and  wog_site='" & dd_site.SelectedValue & "' and wog_cc='" & txb_cc.Text & "' and wog_name=N'" & txb_name.Text & "'"
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                        ltlAlert.Text = "alert('此用户组已存在.')"
                        Exit Sub
                    End If
                End If


                StrSql = " Insert into tcpc0.dbo.wo_group(wog_site,wog_cc,wog_name,createdDate,createdBy) "
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













