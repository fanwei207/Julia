Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class perf_monitor
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
                ls = New ListItem
                ls.Value = 0
                ls.Text = "--"
                dd_chan.Items.Add(ls)

                dd_chan.SelectedIndex = 0
                StrSql = "SELECT chan_id,chan_name From tcpc0.dbo.perf_channel where chan_plantcode='" & Session("PlantCode") & "' order by chan_id"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            dd_chan.Items.Add(item)
                        Next
                    End If
                End With
                ds.Reset()

                dd_dp.SelectedIndex = 0
                item = New ListItem("--")
                item.Value = 0
                dd_dp.Items.Add(item)

                StrSql = "SELECT departmentID,name From departments where isnull(active,0)=1 and isnull(isSalary,0)=1 order by departmentID "
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            dd_dp.Items.Add(item)
                        Next
                    End If
                End With
                ds.Reset()

                txb_date.Text = Now.ToString()

                btn_del.Attributes.Add("onclick", "return confirm('确认要删除吗?');")

                BindData()
            End If
        End Sub
        Sub BindData()
            StrSql = " select moni_id,moni_chan,moni_chan_name,moni_date,moni_dp_name,moni_proc,moni_proc_name,moni_desc,case when isnull(moni_isOK,'N')='Y' then N'合格' else N'不合格' end,createdName,createdDate,isnull(moni_conn_id,0),isnull(moni_dp,0) from tcpc0.dbo.perf_monitor"
            StrSql &= " where deletedName is null and moni_plantcode='" & Session("PlantCode") & "' "

            If dd_chan.SelectedIndex > 0 Then
                StrSql &= " and moni_chan ='" & dd_chan.SelectedValue & "' "
            End If

            If dd_dp.SelectedIndex > 0 Then
                StrSql &= " and moni_dp ='" & dd_dp.SelectedValue & "' "
            End If
            If dd_status.SelectedIndex > 0 Then
                StrSql &= " and moni_isOK ='" & dd_status.SelectedValue & "' "
            End If

            StrSql &= " order by moni_date desc"

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("g_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_chan", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_req", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("created_by", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("created_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_chan_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_sent", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_dp_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_conn_id", System.Type.GetType("System.String")))


            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("g_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("g_chan") = .Rows(i).Item(2).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(3)) Then
                            drow.Item("g_date") = .Rows(i).Item(3).ToString()
                        End If
                        drow.Item("g_cc") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("g_status") = .Rows(i).Item(8).ToString().Trim()
                        drow.Item("g_req") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("created_by") = .Rows(i).Item(9).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(10)) Then
                            drow.Item("created_date") = Format(.Rows(i).Item(10), "yy-MM-dd")
                        End If
                        drow.Item("g_chan_id") = .Rows(i).Item(1).ToString().Trim()
                        If .Rows(i).Item(11) = 0 And .Rows(i).Item(8).ToString().Trim() = "不合格" Then
                            drow.Item("g_sent") = "<font color=red><u>纠错通知</u></font>"
                        Else
                            drow.Item("g_sent") = "<u>纠错通知</u>"
                        End If
                        drow.Item("g_conn_id") = .Rows(i).Item(11).ToString().Trim()
                        drow.Item("g_dp_id") = .Rows(i).Item(12).ToString().Trim()

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
            If e.CommandName.CompareTo("g_edit") = 0 Then
                lbl_id.Text = e.Item.Cells(0).Text
                lbl_uname.Text = e.Item.Cells(8).Text

                dd_chan.SelectedValue = e.Item.Cells(10).Text
                dd_dp.SelectedValue = e.Item.Cells(11).Text

                If e.Item.Cells(4).Text = "不合格" Then
                    dd_status.SelectedIndex = 2
                ElseIf e.Item.Cells(4).Text = "合格" Then
                    dd_status.SelectedIndex = 1
                Else
                    dd_status.SelectedIndex = 0
                End If

                txb_date.Text = e.Item.Cells(2).Text
                txb_req.Text = e.Item.Cells(5).Text


                btn_add.Text = "修改"

                btn_cancel.Enabled = True
                btn_del.Enabled = True

            ElseIf e.CommandName.CompareTo("g_prn") = 0 Then
                'ltlAlert.Text = "var w=window.open('/Performance/conn_app2.aspx?moni_id=" & e.Item.Cells(0).Text & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"

                StrSql = "select count(conn_mstr_id) from knowdb.dbo.conn_mstr where conn_deleteddate is null and conn_mstr_id='" & e.Item.Cells(12).Text & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                    StrSql = "update tcpc0.dbo.perf_monitor set moni_conn_id =null where moni_id='" & e.Item.Cells(0).Text & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                    Response.Redirect("/Performance/conn_app2.aspx?moni_id=" & e.Item.Cells(0).Text)
                    Exit Sub
                End If

                If CInt(e.Item.Cells(12).Text) > 0 Then
                    Response.Redirect("/Performance/conn_app3.aspx?moni_id=" & e.Item.Cells(0).Text & "&mid=" & e.Item.Cells(12).Text)
                Else
                    Response.Redirect("/Performance/conn_app2.aspx?moni_id=" & e.Item.Cells(0).Text)
                End If

            End If
        End Sub
        Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
            If dd_chan.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('请选择监控通道.')"
                Exit Sub
            End If

            If dd_dp.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('请选择部门.')"
                Exit Sub
            End If

            If dd_status.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('请选择状态.')"
                Exit Sub
            End If

            If txb_date.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请输入日期.')"
                Exit Sub
            ElseIf Not IsDate(txb_date.Text.Trim) Then
                ltlAlert.Text = "alert('日期必须是日期型数据.')"
                Exit Sub
            End If

            If txb_req.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请输入情况描述.')"
                Exit Sub
            End If

            If lbl_id.Text.Trim.Length > 0 Then
                '修改

                StrSql = " Update tcpc0.dbo.perf_monitor set moni_chan='" & dd_chan.SelectedValue & "',moni_desc=N'" & txb_req.Text & "' "
                StrSql &= " ,moni_chan_name=N'" & dd_chan.SelectedItem.Text & "',moni_date='" & txb_date.Text & "'"
                StrSql &= " ,moni_dp='" & dd_dp.SelectedValue & "',moni_dp_name=N'" & dd_dp.SelectedItem.Text & "', moni_isOK='" & dd_status.SelectedValue & "'"
                StrSql &= " where moni_id='" & lbl_id.Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            Else
                '增加

                StrSql = " Insert into tcpc0.dbo.perf_monitor(moni_chan,moni_chan_name,moni_date, moni_dp,moni_desc,moni_isOK,createdDate,createdBy,createdName,moni_dp_name,moni_plantcode) "
                StrSql &= " values('" & dd_chan.SelectedValue & "',N'" & dd_chan.SelectedItem.Text & "','" & txb_date.Text & "','" & dd_dp.SelectedValue & "',N'" & txb_req.Text & "','" & dd_status.SelectedValue & "',getdate(),'" & Session("uID") & "',N'" & Session("uName") & "',N'" & dd_dp.SelectedItem.Text & "','" & Session("PlantCode") & "')"

                'Response.Write(StrSql)
                'Exit Sub

                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            End If

            lbl_id.Text = ""
            lbl_uname.Text = ""
            txb_date.Text = Now.ToString()
            txb_req.Text = ""
            dd_chan.SelectedIndex = 0
            dd_dp.SelectedIndex = 0
            dd_status.SelectedIndex = 0

            btn_add.Text = "增加"
            btn_cancel.Enabled = False
            btn_del.Enabled = False


            BindData()
        End Sub

        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            Datagrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
            lbl_id.Text = ""
            lbl_uname.Text = ""
            txb_date.Text = Now.ToString()
            txb_req.Text = ""
            dd_chan.SelectedIndex = 0
            dd_dp.SelectedIndex = 0
            dd_status.SelectedIndex = 0

            btn_add.Text = "增加"
            btn_cancel.Enabled = False
            btn_del.Enabled = False

        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub btn_del_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_del.Click
            If Session("uRole") > 1 And Session("uName") <> lbl_uname.Text Then
                ltlAlert.Text = "alert('没有删除此数据的权限.')"
                Exit Sub
            End If

            StrSql = " Update tcpc0.dbo.perf_monitor set deletedName=N'" & Session("uName") & "',deletedDate=getdate() where moni_id='" & lbl_id.Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            lbl_id.Text = ""
            lbl_uname.Text = ""
            txb_date.Text = Now.ToString()
            txb_req.Text = ""
            dd_chan.SelectedIndex = 0
            dd_dp.SelectedIndex = 0
            dd_status.SelectedIndex = 0

            btn_add.Text = "增加"
            btn_cancel.Enabled = False
            btn_del.Enabled = False

            Datagrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Protected Sub btn_help_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_help.Click
            ltlAlert.Text = "var w=window.open('/docs/监控员在质量方面主要监控的要点.doc','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
        End Sub
    End Class

End Namespace













