Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Namespace tcpc
    Partial Class wo2_list_detail4His
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
                nRet = chk.securityCheck(Session("uID").ToString(), Session("uRole").ToString(), Session("orgID").ToString(), "600020002")
                Dim myid As String = "0"
                If Request("i") <> Nothing Then
                    myid = Request("i")
                End If

                StrSql = "SELECT wo_site,wo_flr_cc,wo_nbr,wo_lot,wo_routing,wo_qty_ord,ISNULL(wo_qty_comp,0),wo_line,wo_close_date FROM Qad_data.dbo.wo_mstr WHERE wo_site='" & Request("s") & "' AND wo_lot='" & Request("id") & "' "
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                While reader.Read
                    txb_site.Text = reader(0).ToString()
                    txb_cc.Text = reader(1).ToString()
                    txb_wonbr.Text = reader(2).ToString()
                    txb_woid.Text = reader(3).ToString()
                    txb_part.Text = reader(4).ToString()
                    txb_qty.Text = reader(5).ToString()
                    txb_comp.Text = reader(6).ToString()
                    txb_line.Text = reader(7).ToString()
                    txb_closedate.Text = reader(8).ToString()
                    'txb_start.Text = reader(8).ToString()
                    'txb_end.Text = reader(9).ToString()
                End While
                reader.Close()

                'If Request("st") <> Nothing Then
                '    txb_site.Text = Request("st")
                'End If

                'If Request("cc") <> Nothing Then
                '    txb_cc.Text = Request("cc")
                'End If

                'If Request("nbr") <> Nothing Then
                '    txb_wonbr.Text = Request("nbr")
                'End If

                'If Request("id") <> Nothing Then
                '    txb_woid.Text = Request("id")
                'End If

                'If Request("rt") <> Nothing Then
                '    txb_part.Text = Request("rt")
                'End If

                'If Request("qty") <> Nothing Then
                '    txb_qty.Text = Request("qty")
                'End If



                'If Request("cp") <> Nothing Then
                '    txb_comp.Text = Request("cp")
                'End If

                If Request("pi") <> Nothing Then
                    txb_a.Text = Request("pi")
                    txb_cost.Text = Request("pi") * CDec(txb_comp.Text)
                End If

                'If Request("dt") <> Nothing Then
                '    txb_closedate.Text = Request("dt")
                'End If

                'If Request("ln") <> Nothing Then
                '    txb_line.Text = Server.UrlDecode(Request("ln"))
                'End If
                'If Request("start") <> Nothing Then
                '    txb_start.Text = Request("start")
                'End If
                'If Request("end") <> Nothing Then
                '    txb_end.Text = Request("end")
                'End If
                'txb_date1.Text = Format(Today, "yyyy-MM-01")
                'txb_date2.Text = Format(Today, "yyyy-MM-dd")



                BindData()
            End If
        End Sub
        Sub BindData()
            'Dim myid As String = "0"
            'If Request("i") <> Nothing Then
            '    myid = Request("i")
            'End If

            'Dim myrt As String = "0"
            'If Request("rt") <> Nothing Then
            '    myrt = Request("rt")
            'End If

            StrSql = " select a.wo2_id,a.wo2_proc,a.wo2_postion,a.wo2_userNo,a.wo2_userName,a.wo2_groupName,a.wo2_procName,a.wo2_postName,isnull(a.wo2_postProportion,0),isnull(a.wo2_Proportion,0),isnull(a.wo2_ro_tool,0)* CASE WHEN Substring(a.wo2_Part,1,1)='1' And Substring(a.wo2_Part,3,2)<>'75' And Right(a.wo2_tec,1)<>'B' THEN ISNULL(t.wrate,1) ELSE ISNULL(t.trate,1) END,case when isnull(a.wo2_line_comp,0)=0 then isnull(a.wo2_TimeProportion,0)*isnull(a.wo2_ro_tool,0)* CASE WHEN Substring(a.wo2_Part,1,1)='1' And Substring(a.wo2_Part,3,2)<>'75' And Right(a.wo2_tec,1)='B' THEN ISNULL(t.wrate,1) ELSE ISNULL(t.trate,1) END *" & txb_comp.Text & " else isnull(a.wo2_TimeProportion,0)*isnull(a.wo2_ro_tool,0)* CASE WHEN Substring(a.wo2_Part,1,1)='1' And Substring(a.wo2_Part,3,2)<>'75' THEN ISNULL(t.wrate,1) ELSE ISNULL(t.trate,1) END *a.wo2_line_comp end,a.wo2_creatName,a.wo2_creatdate,a.wo2_modify,a.wo2_modifieddate,ISNULL(a.wo2_line_comp,0),a.wo2_InputLine,isnull(a.wo2_TimeProportion,0),a.wo2_effdate,a.wo2_tec "
            'StrSql = " select a.wo2_id,a.wo2_proc,a.wo2_postion,a.wo2_userNo,a.wo2_userName,a.wo2_groupName,a.wo2_procName,a.wo2_postName,isnull(a.wo2_postProportion,0),isnull(a.wo2_Proportion,0),isnull(b.wo2_ro_run,0)*1.14, isnull(a.wo2_TimeProportion,0)*isnull(b.wo2_ro_run,0) * 1.14 * a.wo2_line_comp,a.wo2_creatName,a.wo2_creatdate,a.wo2_modify,a.wo2_modifieddate,a.wo2_line_comp,a.wo2_InputLine,isnull(a.wo2_TimeProportion,0)"
            StrSql &= " from wo2_WorkOrderEnterHis a " 'Left Outer Join tcpc0.dbo.wo2_routing b on b.wo2_ro_routing='" & txb_part.Text & "' and b.wo2_mop_proc=a.wo2_proc "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid='" & txb_cc.Text & "' "
            End If
            StrSql &= " Left Outer Join tcpc0.dbo.wo_rate t ON Year(t.workdate) = Year(wo2_effdate)  And Month(t.workdate) =  Month(wo2_effdate) And t.plantcode='" & Session("plantcode") & "'"
            StrSql &= " where a.wo2_id is not null "
            'If Request("i") <> Nothing Then
            '    StrSql &= "  and a.wo2_WorkOrderID='" & Request("i") & "' "
            'Else
            '    StrSql &= "  and a.wo2_WorkOrderID= 0 "
            'End If
            StrSql &= " AND a.wo2_site = '" & txb_site.Text.Trim() & "' "
            StrSql &= " AND  a.wo2_wID = '" & txb_woid.Text.Trim() & "' "

            StrSql &= " order by a.wo2_proc,a.wo2_postion,a.wo2_userNo"

            Session("EXTitle") = "60^<b>工号</b>~^<b>姓名</b>~^<b>用户组</b>~^<b>工序</b>~^<b>工位</b>~^<b>工位系数</b>~^<b>分配比例</b>~^<b>工序标准</b>~^<b>工序工时</b>~^<b>录入人</b>~^<b>创建日期</b>~^<b>修改人</b>~^<b>修改日期</b>~^<b>分配线完工入库</b>~^<b>分配线</b>~^<b>系数</b>~^<b>生效日期</b>~^<b>工艺代码</b>~^"
            Session("EXSQL") = StrSql

            'Response.Write(StrSql)
            'Exit Sub

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim total As Decimal = 0

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc2_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_proc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_proc2", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_procrate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_rate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_std", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cost", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_create", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_createdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_modi", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_modidate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_qtycomp", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_line2", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_rate2", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_comp", System.Type.GetType("System.String")))

            dtl.Columns.Add(New DataColumn("wo_group", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_effdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_tec", System.Type.GetType("System.String")))


            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()

                        drow.Item("proc_id") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("proc2_id") = .Rows(i).Item(2).ToString().Trim()

                        drow.Item("wo_no") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("wo_name") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("wo_group") = .Rows(i).Item(5).ToString().Trim()

                        drow.Item("wo_proc") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("wo_proc2") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("wo_procrate") = Format(.Rows(i).Item(8), "##0.##")
                        drow.Item("wo_rate") = Format(.Rows(i).Item(9), "##0.##")
                        drow.Item("wo_std") = Format(.Rows(i).Item(10), "##0.#####")

                        drow.Item("wo_cost") = Format(.Rows(i).Item(11), "##0.#####")

                        drow.Item("wo_create") = .Rows(i).Item(12).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(13)) Then
                            drow.Item("wo_createdate") = Format(.Rows(i).Item(13), "yy-MM-dd")
                        Else
                            drow.Item("wo_createdate") = ""
                        End If
                        drow.Item("wo_modi") = .Rows(i).Item(14).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(15)) Then
                            drow.Item("wo_modidate") = Format(.Rows(i).Item(15), "yy-MM-dd")
                        Else
                            drow.Item("wo_modidate") = ""
                        End If
                        drow.Item("wo_qtycomp") = Format(.Rows(i).Item(16), "##0.#####")
                        drow.Item("wo_line2") = .Rows(i).Item(17).ToString().Trim()
                        drow.Item("wo_rate2") = Format(.Rows(i).Item(18), "##0.#####")

                        If Not IsDBNull(.Rows(i).Item(19)) Then
                            drow.Item("wo_effdate") = Format(.Rows(i).Item(19), "yy-MM-dd")
                        Else
                            drow.Item("wo_effdate") = ""
                        End If

                        drow.Item("wo_tec") = .Rows(i).Item(20).ToString()

                        total = total + .Rows(i).Item(11)
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            Label1.Text = "汇报工时：" & Format(total, "##0.#####")

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()

        End Sub

        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("wo_edit") = 0 Then
                'ltlAlert.Text = "var w=window.open('/public/exportexcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('/public/exportexcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub

        Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item

                    'If e.Item.Cells(19).Text <> "&nbsp;" And e.Item.Cells(19).Text <> "T" And e.Item.Cells(19).Text <> "" Then
                    '    e.Item.Cells(17).Attributes.Add("onclick", "return confirm('确定要批准工序定额吗?');")
                    'Else
                    '    e.Item.Cells(17).Attributes.Remove("onclick")
                    'End If

                    'If e.Item.Cells(19).Text <> "&nbsp;" And e.Item.Cells(19).Text <> "T" And e.Item.Cells(19).Text <> "R" And e.Item.Cells(19).Text <> "" Then
                    '    e.Item.Cells(18).Attributes.Add("onclick", "return confirm('确定要执行财务结算吗?');")
                    'Else
                    '    e.Item.Cells(18).Attributes.Remove("onclick")
                    'End If


                Case ListItemType.AlternatingItem
                    'If e.Item.Cells(19).Text <> "&nbsp;" And e.Item.Cells(19).Text <> "T" And e.Item.Cells(19).Text <> "" Then
                    '    e.Item.Cells(17).Attributes.Add("onclick", "return confirm('确定要批准工序定额吗?');")
                    'Else
                    '    e.Item.Cells(17).Attributes.Remove("onclick")
                    'End If

                    'If e.Item.Cells(19).Text <> "&nbsp;" And e.Item.Cells(19).Text <> "T" And e.Item.Cells(19).Text <> "R" And e.Item.Cells(19).Text <> "" Then
                    '    e.Item.Cells(18).Attributes.Add("onclick", "return confirm('确定要执行财务结算吗?');")
                    'Else
                    '    e.Item.Cells(18).Attributes.Remove("onclick")
                    'End If

            End Select

        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
            'Response.Redirect("wo2_list_slow4.aspx?st=" & Request("st") & "&cc=" & Request("cc") & "&nbr=" & Request("nbr") & "&id=" & Request("id") & "&df=" & Request("df") & "&rt=" & Request("rt") & "&qty=" & Request("qty") & "&pi=" & Request("pi") & "&cp=" & Request("cp") & "&dt=" & Request("dt") & "&start=" & Request("start") & "&end=" & Request("end") & "&ln=" & Request("ln") & "&i=" & Request("i"))
            Response.Redirect("wo2_list_slow4sHis.aspx?st=" & Request("st"))
        End Sub

        Protected Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
            Response.Redirect("wo2_workOrderAdjust.aspx?nbr=" & txb_wonbr.Text & "&id=" & txb_woid.Text & "&st=" & txb_site.Text & "&ln=" & Server.UrlEncode(txb_line.Text) & "&pi=" & txb_a.Text)
        End Sub


        Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click

            'StrSql = "Update wo2_WorkOrderEnter "
            'StrSql &= " SET wo2_ro_tool = r.wo2_ro_run "
            'StrSql &= " FROM tcpc0.dbo.wo2_routing r WHERE r.wo2_ro_routing = wo2_WorkOrderEnter.wo2_tec AND r.wo2_mop_proc = wo2_WorkOrderEnter.wo2_proc "
            'StrSql &= " AND wo2_site='" & Request("s") & "' AND wo2_wid='" & Request("id") & "' "
            'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)


            BindData()
        End Sub
    End Class
End Namespace