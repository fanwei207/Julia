Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class wo_edit_a3
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim nRet As Integer
        Dim item As ListItem
        Dim reader As SqlDataReader


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

                StrSql = "SELECT qad_site FROM Domain_Mes WHERE plantcode ='" & Session("PlantCode") & "' "
                reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
                While reader.Read
                    item = New ListItem
                    item.Value = reader(0)
                    item.Text = reader(0)
                    dd_site.Items.Add(item)
                End While
                reader.Close()


                If Request("nbr") <> Nothing And Request("site") <> Nothing Then
                    txb_wonbr.Text = Request("nbr")
                    dd_site.SelectedValue = Request("site")
                    getWOInfo()
                End If

                btnDelete.Attributes.Add("onclick", "return confirm('确定要删除这些数据吗?');")

                BindData()
            End If
        End Sub

        Sub BindData()
            StrSql = " select cd.id,cd.wocd_site,cd.wocd_nbr,cd.wocd_id,cd.wocd_cc, cd.wocd_part,cd.wocd_user_no,cd.wocd_username,cd.wocd_proc_name,isnull(cd.wocd_proc_qty,0),isnull(cd.wocd_proc_adj,0),isnull(cd.wocd_cost,0) , cd.wocd_date,isnull(cd.createdName,'') ,isnull(cd.wocd_price,0), Isnull(wocd_duty,''),isnull(cd.wocd_line,'--') from wo_cost_detail cd "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If
            StrSql &= " where cd.id is not null and isnull(cd.wocd_type,'')<>'' and isnull(cd.wocd_type,'')<>'R' and isnull(cd.wocd_type,'')<>'T'"

            'If dd_site.SelectedIndex > 0 Then
            StrSql &= " and cd.wocd_site ='" & dd_site.SelectedValue & "' "
            'End If
            'If txb_wonbr.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_nbr ='" & txb_wonbr.Text.Trim() & "' "
            'End If
            'If lbl_cc.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_cc ='" & lbl_cc.Text.Trim() & "' "
            'End If
            StrSql &= " order by cd.wocd_date desc, cd.id "


            Session("EXTitle") = "50^<b>地点</b>~^80^<b>加工单号</b>~^80^<b>加工单ID</b>~^80^<b>成本中心</b>~^130^<b>零件号</b>~^<b>工号</b>~^<b>姓名</b>~^200^<b>工序</b>~^<b>数量</b>~^<b>调整</b>~^<b>工费</b>~^<b>完工日期</b>~^50^<b>创建人</b>~^<b>工序标准</b>~^<b>承担部门</b>~^<b>工段线</b>~^"
            Session("EXSQL") = StrSql
            Session("EXHeader") = "工序汇报    导出日期: " & Format(DateTime.Today, "yyyy-MM-dd")


            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("user_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_qty", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_adj", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_price1", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_price2", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_date_comp", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_nbr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_created", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_duty", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_line", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("proc_nbr") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("proc_cc") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("user_id") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("user_name") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("proc_name") = .Rows(i).Item(8).ToString().Trim()
                        drow.Item("proc_qty") = Format(.Rows(i).Item(9), "##0.#####")
                        drow.Item("proc_adj") = Format(.Rows(i).Item(10), "##0.#####")
                        drow.Item("proc_price1") = Format(.Rows(i).Item(11), "##0.#####")
                        drow.Item("proc_price2") = Format(.Rows(i).Item(14), "##0.#####")
                        If Not IsDBNull(.Rows(i).Item(12)) Then
                            drow.Item("wo_date_comp") = Format(.Rows(i).Item(12), "yy-MM-dd")
                        End If
                        drow.Item("wo_created") = .Rows(i).Item(13).ToString().Trim()
                        drow.Item("proc_duty") = .Rows(i).Item(15).ToString().Trim()
                        drow.Item("wo_line") = .Rows(i).Item(16).ToString().Trim()
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
            If e.CommandName.CompareTo("proc_del") = 0 Then
                If Session("uRole") > 1 And Session("uName") <> e.Item.Cells(12).Text Then
                    ltlAlert.Text = "alert('没有删除此数据的权限.')"
                    Exit Sub
                End If

                StrSql = "Delete from wo_cost_detail where id='" & e.Item.Cells(0).Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                BindData()
            End If
        End Sub
        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.CancelCommand
            Datagrid1.EditItemIndex = -1
            BindData()
        End Sub
        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.UpdateCommand
            If Session("uRole") > 1 And Session("uName") <> e.Item.Cells(12).Text Then
                ltlAlert.Text = "alert('没有修改此数据的权限.')"
                Exit Sub
            End If


            Dim strSQL As String
            'Dim str1 As String = e.Item.Cells(7).Text
            Dim str2 As String = CType(e.Item.Cells(8).Controls(0), TextBox).Text
            If Not IsNumeric(str2) Then
                ltlAlert.Text = "alert('请输入正确的数量.')"
                Exit Sub
            End If

            strSQL = "update wo_cost_detail set wocd_proc_adj = '" & str2 & "', wocd_cost=" & CDec(str2) & " * wocd_price,wocd_fixed_qty='y' where id='" & e.Item.Cells(0).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            Datagrid1.EditItemIndex = -1
            BindData()
        End Sub
        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.EditCommand
            Datagrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub
        Function getWOInfo() As Boolean
            If txb_wonbr.Text.Trim.Length <= 0 Then
                lbl_cc.Text = ""
                txb_comp.Text = ""
                lbl_cost.Text = ""
                lbl_price.Text = ""
                lbl_rate.Text = ""
                lbl_desc.Text = ""
                lbl_type.Text = ""
                txb_hour.Text = ""


                Exit Function
            End If

            StrSql = "Select woo_cc_to,isnull(woo_qty_comp,0),woo_req,woo_type,isnull(woo_hours,0)  from wo_order where woo_site='" & dd_site.SelectedValue & "' and woo_nbr='" & txb_wonbr.Text & "' and deletedBy is null"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    lbl_cc.Text = .Rows(0).Item(0).ToString().Trim()
                    txb_comp.Text = Format(.Rows(0).Item(1), "##0.#####")
                    lbl_desc.Text = .Rows(0).Item(2).ToString().Trim()
                    lbl_cost.Text = "0"
                    lbl_price.Text = "0"
                    lbl_rate.Text = "1"
                    lbl_type.Text = .Rows(0).Item(3).ToString().Trim()
                    txb_hour.Text = Format(.Rows(0).Item(4), "##0.##")

                Else
                    ltlAlert.Text = "alert('此计划外用工单不存在.')"
                    Exit Function
                End If
            End With
            ds.Reset()

            'Added By Simon in Dec 10,2009
            'Delete the data by workprocedure
            '//---------------------------------------
            dropWorkproc.Items.Clear()
            Dim i As Integer = 1
            Dim ls As ListItem
            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            dropWorkproc.Items.Add(ls)


            StrSql = "select cd.wocd_proc_name from wo_cost_detail cd "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If
            StrSql &= " where cd.id is not null and isnull(cd.wocd_type,'')<>'' and isnull(cd.wocd_type,'')<>'R' and isnull(cd.wocd_type,'')<>'T'"
            StrSql &= " and cd.wocd_site ='" & dd_site.SelectedValue & "' "
            StrSql &= " and cd.wocd_nbr ='" & txb_wonbr.Text.Trim() & "' "
            StrSql &= " and cd.wocd_cc ='" & lbl_cc.Text.Trim() & "'"
            StrSql &= " GROUP BY cd.wocd_proc_name ORDER BY wocd_proc_name"
            Dim reader As SqlDataReader
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = i
                ls.Text = reader(0).ToString()
                dropWorkproc.Items.Add(ls)
                i = i + 1
            End While
            reader.Close()
            '//------------END -----------------------------
           
        End Function

        Protected Sub btn_woload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_woload.Click

            getWOInfo()

            BindData()

        End Sub

        Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx" & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"
        End Sub

        Protected Sub btn_edit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_edit1.Click
            Response.Redirect("wo_list.aspx?rm=" & Now)
        End Sub

        Protected Sub btn_rct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_rct.Click
            If Not IsNumeric(txb_comp.Text) Then
                ltlAlert.Text = "alert(txb_comp.Text & '--请输入正确的数量.')"
                Exit Sub
            End If
            If Not IsNumeric(txb_hour.Text) Then
                ltlAlert.Text = "alert(txb_hour.Text & '--请输入正确的工时.')"
                Exit Sub
            End If

            StrSql = "Update wo_order set woo_qty_comp='" & txb_comp.Text & "',woo_hours='" & txb_hour.Text & "' where woo_site='" & dd_site.SelectedValue & "' and woo_nbr='" & txb_wonbr.Text & "' and deletedBy is null and approvedBy is null and acctApprBy is null"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

        End Sub


        Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            If (dropWorkproc.SelectedIndex = 0 Or txtDate.Text.Trim().Length = 0) Then
                ltlAlert.Text = "alert('必须选择工序和日期.')"
                Exit Sub
            Else
                If Not IsDate(txtDate.Text.Trim()) Then
                    ltlAlert.Text = "alert('日期输入不正确.')"
                    Exit Sub
                End If
            End If

            StrSql = "DELETE FROM wo_cost_detail WHERE  wocd_proc_name = N'" & dropWorkproc.SelectedItem.Text & "' AND createdBy = '" & Session("Uid") & "'"
            StrSql &= " AND wocd_site ='" & dd_site.SelectedValue & "' "
            StrSql &= " AND wocd_nbr ='" & txb_wonbr.Text.Trim() & "' "
            StrSql &= " AND wocd_cc ='" & lbl_cc.Text.Trim() & "'"
            StrSql &= " AND datediff(day,wocd_date,'" & txtDate.Text.Trim() & "') = 0 "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            'Complete product quantity

            getWOInfo()

            BindData()
        End Sub

    End Class

End Namespace













