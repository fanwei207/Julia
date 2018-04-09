Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports CommClass

Namespace tcpc
    Partial Class RDW_Approval
        Inherits BasePage
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
            If Not IsPostBack Then
                Me.Security.Register("170008", "注册查看所有项目的权限")
            End If
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then
                BindMode()
                BindData()
            End If
        End Sub

        Sub BindMode()
            ddlMode.Items.Add(New ListItem("Only show myself", "0"))
            If Me.Security("170008").isValid Then
                ddlMode.Items.Add(New ListItem("Show all", "1"))
            End If
            ddlMode.Items(0).Selected = True
        End Sub

        Sub CreateSql()
            StrSql = "SELECT m.RDW_mstrid,d.RDW_detid,a.RDW_EvaluateID,m.RDW_project,m.RDW_prodcode,m.RDW_proddesc "
            StrSql &= " ,d.RDW_TaskID,d.RDW_StepName ,d.RDW_StartDate,d.RDW_EndDate"
            StrSql &= " ,u.UserName,mu.UserName as Approver,m.RDW_proddesc,m.RDW_ProdSKU  "
            StrSql &= " FROM RDW_Det d Inner Join RDW_Mstr m "
            StrSql &= " on d.RDW_mstrid=m.RDW_mstrid Inner Join RDW_Det_Mbr a "
            StrSql &= " on a.RDW_detid=d.RDW_detid Inner Join tcpc0.dbo.Users u On u.userID = m.RDW_CreatedBy "
            StrSql &= " Inner Join tcpc0.dbo.Users mu On mu.userID = a.RDW_EvaluateID "
            StrSql &= " where isnull(m.RDW_Status,'')='PROCESS' "
            StrSql &= " and isnull(a.RDW_Result,0)=0 and isnull(a.RDW_Approve,0)=1 "
            'If Session("uRole") <> 1 Then
            If ddlMode.SelectedValue = "0" Then
                StrSql &= " and a.RDW_EvaluateID='" & Session("uID") & "'"
            End If
            'End If
            StrSql &= " order by d.RDW_EndDate "
        End Sub

        Sub BindData()
            CreateSql()

            ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_rdw"), CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("g_mid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_did", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_uid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_proj", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_prodcode", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_proddesc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_start", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_end", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_creater", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_step", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_taskid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_sku", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_approver", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()

                        drow.Item("g_mid") = .Rows(i).Item("RDW_mstrid").ToString().Trim()
                        drow.Item("g_did") = .Rows(i).Item("RDW_detid").ToString().Trim()
                        drow.Item("g_uid") = .Rows(i).Item("RDW_EvaluateID").ToString().Trim()
                        drow.Item("g_proj") = .Rows(i).Item("RDW_project").ToString().Trim()
                        drow.Item("g_prodcode") = .Rows(i).Item("RDW_prodcode").ToString().Trim()
                        drow.Item("g_proddesc") = .Rows(i).Item("RDW_proddesc").ToString().Trim()
                        If IsDBNull(.Rows(i).Item("RDW_StartDate")) Then
                            drow.Item("g_start") = ""
                        Else
                            If IsDate(.Rows(i).Item("RDW_StartDate")) Then
                                drow.Item("g_start") = Format(.Rows(i).Item("RDW_StartDate"), "yyyy-MM-dd")
                            Else
                                drow.Item("g_start") = .Rows(i).Item("RDW_StartDate").ToString().Trim()
                            End If
                        End If
                        If IsDBNull(.Rows(i).Item("RDW_EndDate")) Then
                            drow.Item("g_end") = ""
                        Else
                            If IsDate(.Rows(i).Item("RDW_EndDate")) Then
                                drow.Item("g_end") = Format(.Rows(i).Item("RDW_EndDate"), "yyyy-MM-dd")
                            Else
                                drow.Item("g_end") = .Rows(i).Item("RDW_EndDate").ToString().Trim()
                            End If
                        End If
                        drow.Item("g_creater") = .Rows(i).Item("UserName").ToString().Trim()
                        drow.Item("g_step") = .Rows(i).Item("RDW_StepName").ToString().Trim()
                        drow.Item("g_sku") = .Rows(i).Item("RDW_ProdSKU").ToString().Trim()
                        drow.Item("g_taskid") = .Rows(i).Item("RDW_TaskID").ToString().Trim()
                        drow.Item("g_approver") = .Rows(i).Item("Approver").ToString().Trim()
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


        Private Sub datagrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("g_appr") = 0 Then
                Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" & e.Item.Cells(0).Text & "&did=" & e.Item.Cells(1).Text & "&fr=znqap&rm=" & Now.ToString)
            End If

            If e.CommandName.CompareTo("gobom") = 0 Then
                ltlAlert.Text = "var w=window.open('/RDW/RDW_doclist.aspx?mid=" & e.Item.Cells(0).Text & "&fr=znqap&&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
            End If

        End Sub
        Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    'e.Item.Cells(11).Attributes.Add("onclick", "return confirm('确定要删除此数据吗?');")

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

                    If e.Item.Cells(2).Text <> Session("uID").ToString() Then
                        e.Item.Cells(11).Enabled = False
                    End If

                Case ListItemType.AlternatingItem
                    'e.Item.Cells(11).Attributes.Add("onclick", "return confirm('确定要删除此数据吗?');")
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
                    If e.Item.Cells(2).Text <> Session("uID").ToString() Then
                        e.Item.Cells(11).Enabled = False
                    End If
                Case ListItemType.EditItem
                    'Dim DRV As DataRowView = CType(e.Item.DataItem, DataRowView)
                    'Dim CurrentStatus As String = DRV("wo_status")
                    'Dim DDL As DropDownList = CType(e.Item.Cells(12).Controls(1), DropDownList)

                    'Dim SQL As String = "SELECT wl_status_id,wl_status FROM tcpc0.dbo.wl_group where wl_group='" & e.Item.Cells(3).Text.Trim.Substring(0, 2) & "' ORDER BY wl_status_id"
                    'Dim DS As New DataSet
                    'DS = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, SQL)

                    'DDL.DataSource = DS.Tables(0).DefaultView
                    'DDL.DataTextField = "wl_status"
                    'DDL.DataValueField = "wl_status_id"
                    'DDL.DataBind()

                    'Dim item As ListItem
                    'item = DDL.Items.FindByText(CurrentStatus)
                    'If Not item Is Nothing Then item.Selected = True
            End Select

        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub ddlMode_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMode.SelectedIndexChanged
            BindData()
        End Sub

        Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
            CreateSql()
            Dim title As String = "150^<b>Project</b>~^120^<b>Project Code</b>~^80^<b>Step No,</b>~^160^<b>Step Name</b>~^<b>Step StartDate</b>~^<b>Step EndDate</b>~^<b>Creator</b>~^<b>Approver</b>~^200^<b>Project Desc</b>~^200^<b>SKU#</b>~^"
            ExportExcel(admClass.getConnectString("SqlConn.Conn_rdw"), title, StrSql, False)
        End Sub
    End Class
End Namespace