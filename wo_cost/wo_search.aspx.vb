Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class wo_search
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

                txb_date1.Text = Format(Today, "yyyy-MM-01")
                txb_date2.Text = Format(Today, "yyyy-MM-dd")

                BindData()
            End If
        End Sub

        Sub BindData()
            If txb_cc.Text.Trim.Length() = 0 And txb_line.Text.Trim.Length = 0 And txb_userno.Text.Trim.Length = 0 And txb_date1.Text.Trim.Length = 0 And txb_date2.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('成本中心、工段线和工号不能同时为空.')"
                Exit Sub
            End If

            Dim people As Integer = GetWoPeople(dd_site.SelectedValue, txb_cc.Text.Trim(), txb_line.Text.Trim(), txb_userno.Text.Trim(), txb_date1.Text, txb_date2.Text)

            StrSql = " select cd.id, cd.wocd_site, cd.wocd_nbr, cd.wocd_id, cd.wocd_cc, cd.wocd_part, cd.wocd_user_no, cd.wocd_username, cd.wocd_proc_name, "
            StrSql &= " isnull(cd.wocd_proc_qty,0), isnull(cd.wocd_proc_adj,0), CASE WHEN ISNULL(cd.wocd_type,'')='' THEN Round(isnull(cd.wocd_cost,0) * 1.02,5) ELSE isnull(cd.wocd_cost,0) END, ISNULL(wocd_unplan,0), cd.wocd_date, "
            StrSql &= " isnull(cd.createdName,''), isnull(cd.wocd_price,0), isnull(cd.wocd_line,'--'), cd.wocd_tec, wg.wog_name from wo_cost_detail cd "
            StrSql &= " left outer join tcpc0.dbo.wo_group wg on wg.wog_id = cd.wocd_group_id "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If

            StrSql &= " Left Outer Join tcpc0.dbo.Wo_Rate t ON Year(t.workdate) = '" & Year(CDate(txb_date1.Text)) & "' And Month(t.workdate) = '" & Month(CDate(txb_date1.Text)) & "' And t.plantcode='" & Session("plantcode") & "'"

            StrSql &= " where cd.id is not null "
            If dd_site.SelectedIndex > 0 Then
                StrSql &= " and cd.wocd_site ='" & dd_site.SelectedValue & "' "
            End If
            If txb_cc.Text.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_cc ='" & txb_cc.Text.Trim() & "' "
            End If
            If txb_line.Text.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_line =N'" & txb_line.Text.Trim() & "' "
            End If
            If txb_userno.Text.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_user_no ='" & txb_userno.Text.Trim() & "' "
            End If

            If txb_date1.Text.Trim.Length = 10 And IsDate(txb_date1.Text) Then
                StrSql &= " and cd.wocd_date >= '" & txb_date1.Text & "'"
            End If
            If txb_date2.Text.Trim.Length = 10 And IsDate(txb_date2.Text) Then
                StrSql &= " and cd.wocd_date <= '" & txb_date2.Text & "'"
            End If
            If dd_wtime.SelectedIndex = 1 Then
                StrSql &= " and cd.wocd_cost =0 "
            ElseIf dd_wtime.SelectedIndex = 2 Then
                StrSql &= " and cd.wocd_cost<>0 "
            End If
            StrSql &= " order by cd.wocd_date desc "

            'Response.Write(StrSql)

            'Session("EXTitle") = "50^<b>地点</b>~^80^<b>加工单号</b>~^80^<b>加工单ID</b>~^80^<b>成本中心</b>~^130^<b>零件号</b>~^<b>工号</b>~^<b>姓名</b>~^200^<b>工序</b>~^<b>数量</b>~^<b>调整</b>~^<b>工时</b>~^<b>非计划</b>~^<b>完工日期</b>~^50^<b>创建人</b>~^<b>工序标准</b>~^<b>工段线</b>~^"
            Session("EXTitle") = "50^<b>地点</b>~^80^<b>加工单号</b>~^80^<b>加工单ID</b>~^80^<b>成本中心</b>~^150^<b>零件号</b>~^<b>工号</b>~^<b>姓名</b>~^200^<b>工序</b>~^<b>数量</b>~^<b>调整</b>~^<b>工时</b>~^<b>非计划</b>~^<b>完工日期</b>~^50^<b>创建人</b>~^<b>工序标准</b>~^<b>工段线</b>~^150^<b>工艺代码</b>~^100^<b>用户组</b>~^"
            Session("EXSQL") = StrSql
            Session("EXHeader") = "工序汇报    导出日期: " & Format(DateTime.Today, "yyyy-MM-dd")


            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("user_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_qty", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("proc_adj", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("proc_price1", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("wo_date_comp", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_nbr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_created", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_line", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_part", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("unplan", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("proc_routing", System.Type.GetType("System.String")))

            Dim total As Integer = 0

            Dim totcost As Decimal = 0


            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("proc_nbr") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("proc_id") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("proc_cc") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("user_id") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("user_name") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("proc_name") = .Rows(i).Item(8).ToString().Trim()
                        drow.Item("proc_qty") = Format(.Rows(i).Item(9), "##0.#####")
                        drow.Item("proc_adj") = Format(.Rows(i).Item(10), "##0.#####")

                        drow.Item("proc_price1") = Format(.Rows(i).Item(11), "##0.#####")
                        If Not IsDBNull(.Rows(i).Item(13)) Then
                            drow.Item("wo_date_comp") = Format(.Rows(i).Item(13), "yy-MM-dd")
                        End If
                        drow.Item("wo_created") = .Rows(i).Item(14).ToString().Trim()
                        drow.Item("wo_line") = .Rows(i).Item(16).ToString().Trim()

                        drow.Item("proc_part") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("proc_routing") = .Rows(i).Item("wocd_tec").ToString().Trim()
                        drow.Item("unplan") = Format(.Rows(i).Item(12), "##0.#####")

                        dtl.Rows.Add(drow)
                        total = total + 1
                        totcost = totcost + .Rows(i).Item(11)
                    Next
                End If
            End With
            ds.Reset()

            lbl_tot.Text = "总次数:" & total.ToString()
            lbl_totcost.Text = "总工时:" & Format(totcost, "##0.##")
            lbl_peo.Text = "人数:" & people.ToString
            If people > 0 Then
                lbl_cost.Text = "人均工时:" & Format(totcost / people, "##0.##")
            Else
                lbl_cost.Text = "人均工时: 0"
            End If
            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()
        End Sub

       
        Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx" & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"
        End Sub

       
        Protected Sub btn_woload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_woload.Click
            BindData()
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub


        Function GetWoPeople(ByVal intSite As Integer, ByVal strCost As String, ByVal strLine As String, ByVal strUserno As String, ByVal strSdate As String, ByVal strEdate As String) As Integer
            StrSql = " select count(*) from "
            StrSql &= " ( select  wocd_user_no from wo_cost_detail cd "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If
            StrSql &= " where cd.id is not null "
            If intSite > 0 Then
                StrSql &= " and cd.wocd_site ='" & intSite.ToString() & "' "
            End If
            If strCost.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_cc ='" & strCost & "' "
            End If
            If strLine.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_line =N'" & strLine.Trim() & "' "
            End If
            If strUserno.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_user_no ='" & strUserno.Trim() & "' "
            End If

            If strSdate.Trim.Length = 10 And IsDate(strSdate) Then
                StrSql &= " and cd.wocd_date >= '" & strSdate & "'"
            End If
            If strEdate.Trim.Length = 10 And IsDate(strEdate) Then
                StrSql &= " and cd.wocd_date <= '" & strEdate & "'"
            End If

            StrSql &= " group by cd.wocd_user_no )  AS derivedtbl_1 "

            GetWoPeople = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
        End Function
    End Class

End Namespace













