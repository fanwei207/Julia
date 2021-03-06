Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports AppLibrary.WriteExcel

Namespace tcpc
    Partial Class wo2_list_detail4
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim nRet As Integer
        Dim item As ListItem
        Dim reader As SqlDataReader
        Dim b As Boolean


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
                nRet = chk.securityCheck(Session("uID").ToString(), Session("uRole").ToString(), Session("orgID").ToString(), "600020001")
                Dim myid As String = "0"
                If Request("i") <> Nothing Then
                    myid = Request("i")
                End If

                StrSql = "SELECT wo_site,wo_flr_cc,wo_nbr,wo_lot,wo_routing,wo_qty_ord,ISNULL(wo_qty_comp,0),wo_line,wo_close_date = wo_close_eff FROM Qad_data.dbo.wo_mstr WHERE wo_site='" & Request("s") & "' AND wo_lot='" & Request("id") & "' "
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
                End While
                reader.Close()


                If Request("pi") <> Nothing Then
                    txb_a.Text = Request("pi")
                    txb_cost.Text = Request("pi") * CDec(txb_comp.Text)
                End If

                BindData()
            End If
        End Sub
        Private Function createSQL() As String
            createSQL = " select a.wo2_id,a.wo2_proc,a.wo2_postion,a.wo2_userNo,a.wo2_userName,a.wo2_groupName,a.wo2_procName,a.wo2_postName,isnull(a.wo2_postProportion,0)" _
                    & "     , ISNULL(a.wo2_line_comp,0), wo_std = isnull(a.wo2_ro_tool,0)* 1.02"

            If Convert.ToBoolean(Request("cls").ToString()) Then
                createSQL &= "     , wo_cost = isnull(a.wo2_TimeProportion, 0) * isnull(wo.wo_qty_comp, 0) * isnull(a.wo2_ro_tool, 0) * 1.02 "
            Else
                createSQL &= "     , wo_cost = isnull(a.wo2_TimeProportion, 0) * isnull(a.wo2_line_ord, 0) * isnull(a.wo2_ro_tool, 0) * 1.02 "
            End If
            createSQL &= "     , a.wo2_creatName, a.wo2_creatdate,a.wo2_modify,a.wo2_modifieddate, wo2_line_comp = ISNULL(a.wo2_line_comp,0),a.wo2_InputLine,wo2_TimeProportion = isnull(a.wo2_TimeProportion,0),a.wo2_effdate,a.wo2_tec " _
                    & " From wo2_WorkOrderEnter a "
            If Session("uRole") <> 1 Then
                createSQL &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid='" & txb_cc.Text & "' "
            End If

            If Convert.ToBoolean(Request("cls").ToString()) Then
                createSQL &= " Left Join Qad_Data.dbo.wo_mstr wo On wo.wo_nbr = a.wo2_nbr And wo.wo_lot = a.wo2_wID "
            End If

            createSQL &= " where a.wo2_id is not null "
            createSQL &= " AND a.wo2_site = '" & txb_site.Text.Trim() & "' "
            createSQL &= " AND  a.wo2_wID = '" & txb_woid.Text.Trim() & "' "

            createSQL &= " order by a.wo2_proc,a.wo2_postion,a.wo2_userNo"
        End Function

        Sub BindData()
            StrSql = createSQL()
            Session("EXTitle") = "60^<b>工号</b>~^<b>姓名</b>~^<b>用户组</b>~^<b>工序</b>~^<b>工位</b>~^<b>工位系数</b>~^<b>汇报数量</b>~^<b>标准工时</b>~^<b>汇报工时</b>~^<b>录入人</b>~^<b>创建日期</b>~^<b>修改人</b>~^<b>修改日期</b>~^<b>分配线完工入库</b>~^<b>分配线</b>~^<b>系数</b>~^<b>生效日期</b>~^<b>工艺代码</b>~^"
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
                        drow.Item("wo_std") = Format(.Rows(i).Item("wo_std"), "##0.#####")

                        drow.Item("wo_cost") = Format(.Rows(i).Item("wo_cost"), "##0.#####")

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
                        drow.Item("wo_qtycomp") = Format(.Rows(i).Item("wo2_line_comp"), "##0.#####")
                        drow.Item("wo_line2") = .Rows(i).Item(17).ToString().Trim()
                        drow.Item("wo_rate2") = Format(.Rows(i).Item("wo2_TimeProportion"), "##0.#####")

                        If Not IsDBNull(.Rows(i).Item(19)) Then
                            drow.Item("wo_effdate") = Format(.Rows(i).Item(19), "yy-MM-dd")
                        Else
                            drow.Item("wo_effdate") = ""
                        End If

                        drow.Item("wo_tec") = .Rows(i).Item(20).ToString()

                        total = total + .Rows(i).Item("wo_cost")
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
            Dim dt As DataTable
            dt = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL()).Tables(0)

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
            With dt
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
                        drow.Item("wo_std") = Format(.Rows(i).Item("wo_std"), "##0.#####")

                        drow.Item("wo_cost") = Format(.Rows(i).Item("wo_cost"), "##0.#####")

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
                        drow.Item("wo_qtycomp") = Format(.Rows(i).Item("wo2_line_comp"), "##0.#####")
                        drow.Item("wo_line2") = .Rows(i).Item(17).ToString().Trim()
                        drow.Item("wo_rate2") = Format(.Rows(i).Item("wo2_TimeProportion"), "##0.#####")

                        If Not IsDBNull(.Rows(i).Item(19)) Then
                            drow.Item("wo_effdate") = Format(.Rows(i).Item(19), "yy-MM-dd")
                        Else
                            drow.Item("wo_effdate") = ""
                        End If

                        drow.Item("wo_tec") = .Rows(i).Item(20).ToString()
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            dt.Reset()


            Dim doc As New AppLibrary.WriteExcel.XlsDocument()
            doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls"
            Dim sheet As AppLibrary.WriteExcel.Worksheet = doc.Workbook.Worksheets.Add("加工单工时清单")

            Dim column1 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column1.ColumnIndexStart = 0
            column1.ColumnIndexEnd = 0
            column1.Width = 60 * 6000 / 164
            sheet.AddColumnInfo(column1)

            Dim column2 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column2.ColumnIndexStart = 1
            column2.ColumnIndexEnd = 1
            column2.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column2)

            Dim column3 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column3.ColumnIndexStart = 2
            column3.ColumnIndexEnd = 2
            column3.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column3)

            Dim column4 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column4.ColumnIndexStart = 3
            column4.ColumnIndexEnd = 3
            column4.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column4)

            Dim column5 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column5.ColumnIndexStart = 4
            column5.ColumnIndexEnd = 4
            column5.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column5)

            Dim column6 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column6.ColumnIndexStart = 5
            column6.ColumnIndexEnd = 5
            column6.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column6)

            Dim column7 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column7.ColumnIndexStart = 6
            column7.ColumnIndexEnd = 6
            column7.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column7)

            Dim column8 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column8.ColumnIndexStart = 7
            column8.ColumnIndexEnd = 7
            column8.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column8)

            Dim column9 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column9.ColumnIndexStart = 8
            column9.ColumnIndexEnd = 8
            column9.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column9)

            Dim column10 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column10.ColumnIndexStart = 9
            column10.ColumnIndexEnd = 9
            column10.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column10)

            Dim column11 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column11.ColumnIndexStart = 10
            column11.ColumnIndexEnd = 10
            column11.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column11)

            Dim column12 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column12.ColumnIndexStart = 11
            column12.ColumnIndexEnd = 11
            column12.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column12)

            Dim column13 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column13.ColumnIndexStart = 12
            column13.ColumnIndexEnd = 12
            column13.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column13)

            Dim column14 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column14.ColumnIndexStart = 13
            column14.ColumnIndexEnd = 13
            column14.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column14)

            Dim column15 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column15.ColumnIndexStart = 14
            column15.ColumnIndexEnd = 14
            column15.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column15)

            Dim column16 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column16.ColumnIndexStart = 15
            column16.ColumnIndexEnd = 15
            column16.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column16)

            Dim column17 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column17.ColumnIndexStart = 16
            column17.ColumnIndexEnd = 16
            column17.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column17)

            Dim column18 As New AppLibrary.WriteExcel.ColumnInfo(doc, sheet)
            column18.ColumnIndexStart = 17
            column18.ColumnIndexEnd = 17
            column18.Width = 100 * 6000 / 164
            sheet.AddColumnInfo(column18)

            Dim rowIndex As Int32 = 1
            Dim title() As String = {"工号", "姓名", "用户组", "工序", "工位", "工位系数", "汇报数量", "标准工时", "汇报工时", "录入人", "创建日期", "修改人", "修改日期", "分配线完工入库", "分配线", "系数", "生效日期", "工艺代码"}
            WriteExcel(doc, sheet, rowIndex, title)
            For Each row In dtl.Rows
                rowIndex = rowIndex + 1
                Dim content() As String = {row("wo_no").ToString(), row("wo_name").ToString(), row("wo_group").ToString(), row("wo_proc").ToString(), row("wo_proc2").ToString(), row("wo_procrate").ToString(), row("wo_qtycomp").ToString(), row("wo_std").ToString(), row("wo_cost").ToString(), row("wo_create").ToString(), row("wo_createdate").ToString(), row("wo_modi").ToString(), row("wo_modidate").ToString(), row("wo_qtycomp").ToString(), row("wo_line2").ToString(), row("wo_rate2").ToString(), row("wo_effdate").ToString(), row("wo_tec").ToString()}
                WriteExcel(doc, sheet, rowIndex, content)
                doc.Save(Server.MapPath("/Excel/"), True)
                ltlAlert.Text = "window.open('/Excel/" + doc.FileName + "','_blank');"
            Next
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
            Me.Redirect("wo2_list_slow4s.aspx?st=" & Request("st"))
        End Sub

        Protected Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
            Response.Redirect("wo2_workOrderAdjust.aspx?nbr=" & txb_wonbr.Text & "&id=" & txb_woid.Text & "&st=" & txb_site.Text & "&ln=" & Server.UrlEncode(txb_line.Text) & "&pi=" & txb_a.Text)
        End Sub


        Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click

            StrSql = "Update wo2_WorkOrderEnter "
            StrSql &= " SET wo2_ro_tool = r.wo2_ro_run "
            StrSql &= " FROM tcpc0.dbo.wo2_routing r WHERE r.wo2_ro_routing = wo2_WorkOrderEnter.wo2_tec AND r.wo2_mop_proc = wo2_WorkOrderEnter.wo2_proc "
            StrSql &= " AND wo2_site='" & Request("s") & "' AND wo2_wid='" & Request("id") & "' "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)


            BindData()
        End Sub
        Protected Sub WriteExcel(ByVal doc As AppLibrary.WriteExcel.XlsDocument, ByVal sheet As AppLibrary.WriteExcel.Worksheet, ByVal rowIndex As Int32, ByVal columns() As String)
            Dim XFstyle As AppLibrary.WriteExcel.XF
            XFstyle = doc.NewXF
            XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered
            XFstyle.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered
            XFstyle.Font.FontName = "宋体"
            XFstyle.UseMisc = True
            XFstyle.Font.Bold = False
            XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight

            XFstyle.BottomLineStyle = 1
            XFstyle.LeftLineStyle = 1
            XFstyle.TopLineStyle = 1
            XFstyle.RightLineStyle = 1

            XFstyle.UseBorder = True
            XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue
            XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White
            XFstyle.Pattern = 1

            For i = 0 To UBound(columns)
                sheet.Cells.Add(rowIndex, i + 1, columns(i), XFstyle)
            Next i
        End Sub
    End Class
End Namespace