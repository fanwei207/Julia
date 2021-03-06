Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Namespace tcpc
    Partial Class wpl_list
        Inherits System.Web.UI.Page
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
                txt_wostartdate1.Text = Date.Now.Date.AddMonths(-3).ToString("yyyy-MM-dd")
                txt_wostartdate2.Text = Date.Now.Date.AddDays(10).ToString("yyyy-MM-dd") 
                BindData()
            End If
        End Sub
        Sub BindData()
            StrSql = " select top 100 pl_id,pl_site,pl_cc,pl_nbr,pl_wid,pl_part,isnull(pl_qty_ord,0),isnull(pl_qty_comp,0),pl_ord_date,pl_close_date,pl_status,pl_rel_date,pl_due_date,pt_mfg_lead,pt_desc1 + ' ' + pt_desc2"
            StrSql &= " from QAD_Data.dbo.plan_mstr a"
            StrSql &= " Left Outer join QAD_Data.dbo.pt_mstr b on b.pt_domain=a.pl_domain and b.pt_part=substring(a.pl_part,1,14)"
            StrSql &= " where pl_id is not null " 
            If txb_site.Text.Trim.Length > 0 Then
                StrSql &= " and pl_site ='" & txb_site.Text.Trim() & "' "
            End If
            If txb_wonbr.Text.Trim.Length > 0 Then
                StrSql &= " and pl_nbr ='" & txb_wonbr.Text.Trim() & "' "
            End If
            If txb_woid.Text.Trim.Length > 0 Then
                StrSql &= " and pl_wID ='" & txb_woid.Text.Trim() & "' "
            End If
            If txb_cc.Text.Trim.Length > 0 Then
                StrSql &= " and pl_cc ='" & txb_cc.Text.Trim() & "' "
            End If
            If txb_part.Text.Trim.Length > 0 Then
                StrSql &= " and pl_part like '%" & txb_part.Text.Trim() & "%' "
            End If
            StrSql &= " and ( pl_ord_date is null  or ( '" & txt_wostartdate1.Text & "' <= pl_ord_date and pl_ord_date <= '" & txt_wostartdate2.Text & "')) "
            StrSql &= " order by isnull(pl_ord_date,'1900-01-01') desc,pl_nbr desc,pl_wid,isnull(pl_rel_date,'1900-01-01') "

            Session("EXTitle") = "60^<b>地点</b>~^<b>成本中心</b>~^<b>加工单号</b>~^<b>加工单ID</b>~^130^<b>工艺代码</b>~^<b>工单数量</b>~^<b>完工入库</b>~^<b>工单日期</b>~^<b>结算日期</b>~^<b>状态</b>~^<b>计划日期</b>~^<b>截止日期</b>~^<b>制造提前期</b>~^300^<b>描述</b>~^"
            Session("EXSQL") = StrSql

            'Response.Write(StrSql)
            'Exit Sub


            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)


            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_nbr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_part", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_qty", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_comp", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_startdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_closedate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_reldate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_duedate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_desc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_lt", System.Type.GetType("System.String")))

            Dim total As Integer = 0
            Dim total1 As Integer = 0

            Dim nbr As String = ""
            Dim wid As String = ""
            Dim site As String = ""

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        If site <> .Rows(i).Item(1).ToString().Trim() Or nbr <> .Rows(i).Item(3).ToString().Trim() Or wid <> .Rows(i).Item(4).ToString().Trim() Then
                            drow.Item("wo_site") = .Rows(i).Item(1).ToString().Trim()
                            drow.Item("wo_cc") = .Rows(i).Item(2).ToString().Trim()
                            drow.Item("wo_nbr") = .Rows(i).Item(3).ToString().Trim()
                            drow.Item("wo_id") = .Rows(i).Item(4).ToString().Trim()
                            drow.Item("wo_part") = .Rows(i).Item(5).ToString().Trim()
                            drow.Item("wo_qty") = Format(.Rows(i).Item(6), "##0.##")
                            drow.Item("wo_comp") = Format(.Rows(i).Item(7), "##0.##")

                            If Not IsDBNull(.Rows(i).Item(8)) And IsDate(.Rows(i).Item(8)) Then
                                drow.Item("wo_startdate") = Format(.Rows(i).Item(8), "yy-MM-dd")
                            Else
                                drow.Item("wo_startdate") = ""
                            End If
                            If Not IsDBNull(.Rows(i).Item(9)) And IsDate(.Rows(i).Item(9)) Then
                                drow.Item("wo_closedate") = Format(.Rows(i).Item(9), "yy-MM-dd")
                            Else
                                drow.Item("wo_closedate") = ""
                            End If
                            drow.Item("wo_lt") = .Rows(i).Item(13).ToString().Trim()
                            drow.Item("wo_desc") = .Rows(i).Item(14).ToString().Trim()
                            total = total + 1
                        End If
                        total1 = total1 + 1
                        drow.Item("wo_status") = .Rows(i).Item(10).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(11)) And IsDate(.Rows(i).Item(11)) Then
                            drow.Item("wo_reldate") = Format(.Rows(i).Item(11), "yy-MM-dd")
                        Else
                            drow.Item("wo_reldate") = ""
                        End If
                        If Not IsDBNull(.Rows(i).Item(12)) And IsDate(.Rows(i).Item(12)) Then
                            drow.Item("wo_duedate") = Format(.Rows(i).Item(12), "yy-MM-dd")
                        Else
                            drow.Item("wo_duedate") = ""
                        End If
                        dtl.Rows.Add(drow)

                        site = .Rows(i).Item(1).ToString().Trim()
                        nbr = .Rows(i).Item(3).ToString().Trim()
                        wid = .Rows(i).Item(4).ToString().Trim()
                    Next
                End If
            End With
            ds.Reset()

            Label1.Text = "订单数：" & total.ToString() & "--" & Format((total / total1 * 100), "0.000") & "%"

            Dim dv As DataView
            dv = New DataView(dtl)
           
            Try 
                Datagrid1.DataSource = dv
                Datagrid1.DataBind()

            Catch
            End Try


        End Sub

        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("wo_edit") = 0 Then
                'Dim str As String = ""
                'str = "st=" & e.Item.Cells(1).Text.Trim() & "&cc=" & e.Item.Cells(2).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim() & "&rt=" & e.Item.Cells(5).Text.Trim() & "&qty=" & e.Item.Cells(6).Text.Trim() & "&pi=" & e.Item.Cells(7).Text.Trim() & "&cp=" & e.Item.Cells(8).Text.Trim() & "&start=" & e.Item.Cells(10).Text.Trim() & "&end=" & e.Item.Cells(11).Text.Trim() & "&ln=" & e.Item.Cells(12).Text.Trim() & "&dt=" & e.Item.Cells(13).Text.Trim() & "&i=" & e.Item.Cells(0).Text.Trim()
                'str = str.Replace("&nbsp;", "")
                'Response.Redirect("wo2_list_detail.aspx?" & str)
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            Datagrid1.CurrentPageIndex = 0
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
        Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Datagrid1.SortCommand
            Session("orderby") = e.SortExpression.ToString()
            If Session("orderdir") = " ASC" Then
                Session("orderdir") = " DESC"
            Else
                Session("orderdir") = " ASC"
            End If
            BindData()
        End Sub

    End Class
End Namespace