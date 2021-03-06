Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Data
Namespace tcpc
    Partial Class wo2_rtcompare
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
                Dim sql As String
                sql = "Select plantCode From Plants Where plantID=" & Session("PlantCode").ToString()
                Dim dt As DataTable
                dt = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, sql).Tables(0)
                txb_dm.Text = dt.Rows(0).Item(0).ToString()

                txb_diff.Text = "0"
                BindData()
            End If
        End Sub
        Function createSQL() As String
            If Not IsNumeric(txb_diff.Text) Then
                txb_diff.Text = "0"
            End If

            StrSql = " select b.ro_domain,a.wo2_ro_routing,b.ro_desc,isnull(Cast(b.ro_tool as decimal(9,5)),0),isnull(a.tt,0),(isnull(Cast(b.ro_tool as decimal(9,5)),0)-isnull(a.tt,0)) as df from ("
            StrSql &= " select wo2_ro_routing,sum(isnull(wo2_ro_run,0)) as tt"
            'StrSql &= " from tcpc0.dbo.wo2_routing where wo2_ro_id is not null and wo2_mop_proc<>'2090'"
            StrSql &= " from tcpc0.dbo.wo2_routing where wo2_ro_id is not null and wo2_mop_proc NOT IN (SELECT wo2_B FROM tcpc0.dbo.wo2_B_routing )"
            If txb_part.Text.Trim.Length > 0 Then
                StrSql &= " and wo2_ro_routing like '" & txb_part.Text.Trim() & "%' "
            End If
            StrSql &= " group by wo2_ro_routing ) a left outer join (SELECT ro_domain,ro_routing,ro_desc,ro_run - ISNULL(t.wocost,0) as ro_tool FROM QAD_Data.dbo.ro_det Left outer join (SELECT wo_Tec,Round(Sum(wo_gl),5) as wocost FROM tcpc0.dbo.Wo_Tec Where wo_del =0 AND Substring(wo_tec,1,4)<>'2101' AND Ltrim(Rtrim(wo_procName))<>N'维修' GROUP BY wo_Tec ) t ON ro_routing =wo_tec ) b "
            StrSql &= " on a.wo2_ro_routing = b.ro_routing "
            If txb_dm.Text.Trim.Length > 0 Then
                StrSql &= "  and b.ro_domain='" & txb_dm.Text & "' "
            End If
            StrSql &= " where a.wo2_ro_routing is not null "
            StrSql &= " and ABS(isnull(Cast(b.ro_tool as decimal(9,5)),0)-isnull(a.tt,0)) >='" & txb_diff.Text & "' "

            If chb_diff.Checked = False Then
                StrSql &= " and isnull(Cast(b.ro_tool as decimal(9,5)),0) > 0 "
            End If

            StrSql &= " order by b.ro_domain,a.wo2_ro_routing "

            createSQL = StrSql
        End Function
        Sub BindData()
            If Not IsNumeric(txb_diff.Text) Then
                txb_diff.Text = "0"
            End If

            Session("EXTitle") = "60^<b>地点</b>~^130^<b>工艺代码</b>~^230^<b>工艺描述</b>~^<b>工单工时</b>~^<b>工序工时</b>~^<b>差异</b>~^"
            Session("EXSQL") = StrSql

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())

            Dim total As Integer = 0

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_part", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_desc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_a", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_proc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_diff", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("wo_site") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("wo_part") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("wo_desc") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("wo_a") = Format(CDec(.Rows(i).Item(3)), "##0.#####")
                        drow.Item("wo_proc") = Format(.Rows(i).Item(4), "##0.#####")
                        drow.Item("wo_diff") = Format(.Rows(i).Item(5), "##0.#####")
                        dtl.Rows.Add(drow)
                        total = total + 1
                    Next
                End If
            End With
            ds.Reset()

            Label1.Text = "数量： " & total.ToString()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()
        End Sub

        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("wo_edit") = 0 Then
                Response.Redirect("wo2_rtdetail.aspx?dm=" & e.Item.Cells(1).Text.Trim() & "&rt=" & e.Item.Cells(2).Text.Trim() & "&a=" & e.Item.Cells(4).Text.Trim())
                ' ltlAlert.Text = "var w=window.open('wo2_rtdetail.aspx?dm=" & e.Item.Cells(1).Text.Trim() & "&rt=" & e.Item.Cells(2).Text.Trim() & "&a=" & e.Item.Cells(4).Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            If txb_dm.Text.Length = 0 Then
                ltlAlert.Text = "alert('域不能为空');"
            Else
                BindData()
            End If
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
            If txb_dm.Text.Length = 0 Then
                ltlAlert.Text = "alert('域不能为空');"
            Else
                Dim EXTitle As String = "60^<b>地点</b>~^130^<b>工艺代码</b>~^230^<b>工艺描述</b>~^<b>工单工时</b>~^<b>工序工时</b>~^<b>差异</b>~^"
                Dim ExSql As String = createSQL()
                Me.ExportExcel(chk.dsn0, EXTitle, ExSql, False)
            End If
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
            If txb_dm.Text.Length = 0 Then
                ltlAlert.Text = "alert('域不能为空');"
            Else
                Datagrid1.CurrentPageIndex = e.NewPageIndex
                BindData()
            End If
        End Sub

        Protected Sub chb_diff_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb_diff.CheckedChanged
            If txb_dm.Text.Length = 0 Then
                ltlAlert.Text = "alert('域不能为空');"
            Else
                BindData()
            End If
        End Sub
    End Class
End Namespace