Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Namespace tcpc
    Partial Class wo_list
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

            If Not IsPostBack Then

                Me.Security.Register("358000098", "用工单财务结算")
                Me.Security.Register("358000099", "工序定额核准")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then
                '// 注册页面权限--20131126 caixia
                Security.Register(361200003, "用工单汇报（工时）")  'wo_cost/wo_WKspec.aspx
                Security.Register(361200002, "返工单汇报（工时)")  'wo_cost/WKreturn.aspx   
                Security.Register(361200004, "维修汇报（工时）")  'wo_cost/wo_WKother.aspx  
                Security.Register(361200005, "补包装汇报（工时）")  'wo_cost/WKpackage.aspx
                Security.Register(361200001, "加工单汇报（工时）")  'wo_cost/wo_WKnormal.aspx
                Security.Register(358000055, "返工单查询")        'wo_cost/wo_edit_r3.aspx  
                Security.Register(358000057, "用工单查询 ）")     'wo_cost/wo_edit_a3.aspx
                Security.Register(358000051, "加工单查询 ")      'wo_cost/wo_edit3.aspx
                'Security.Register( ")  'wo_cost/wo_edit_avg.aspx-- 在 Menu表未找到


                '// Get Site
                item = New ListItem
                item.Value = 0
                item.Text = "--"
                dd_site.Items.Add(item)

                StrSql = "SELECT qad_site FROM Domain_Mes WHERE plantcode ='" & Session("PlantCode") & "' "
                reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
                While reader.Read
                    item = New ListItem
                    item.Value = reader(0)
                    item.Text = reader(0)
                    dd_site.Items.Add(item)
                End While
                reader.Close()
                '// End get site

                '//Get CostCenter
                item = New ListItem
                item.Value = 0
                item.Text = "--"
                dd_cc.Items.Add(item)

                GetCostCenter()
                '//End get cost center

                txb_date1.Text = Format(Today, "yyyy-MM-01")
                txb_date2.Text = Format(Today, "yyyy-MM-dd")

                BindData()
            End If
        End Sub
        Sub BindData()

            Dim dm As String = ""
            Dim strQad As String = ""
            '//-------------  Get coefficient      ----------
            Dim coefficient As Decimal = 1.02
            '//----------------------------------------
            StrSql = " select cd.wocd_site,cd.wocd_cc,cd.wocd_nbr,cd.wocd_id,cd.wocd_part, sum(CASE WHEN ISNULL(cd.wocd_type,'')='' THEN  Round(isnull(cd.wocd_cost,0) * " & coefficient & ",5) ELSE CASE WHEN ISNULL(cd.wocd_type,'')='B' THEN Round(cd.wocd_price *cd.wocd_proc_qty *ISNULL(cd.wocd_pcost,0)*" & coefficient & ",5) ELSE isnull(cd.wocd_cost,0) END END) ,CASE WHEN ISNULL(cd.wocd_type,'')='B' or ISNULL(cd.wocd_type,'')='W' THEN 0 ELSE ISNULL(w.wo_gl,0) * " & coefficient & " End,isnull(cd.wocd_type,''),0   "
            StrSql &= " from wo_cost_detail cd "
            StrSql &= " Left Outer Join (SELECT wo_tec,sum(wo_gl) as wo_gl FROM tcpc0.dbo.wo_tec Where wo_del = 0 and  Ltrim(Rtrim(wo_procName))<>N'维修' Group by  wo_tec ) as w ON w.wo_tec = cd.wocd_tec "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If
            StrSql &= " where cd.id is not null "
            If txb_wonbr.Text.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_nbr ='" & txb_wonbr.Text.Trim() & "' "
            End If
            If txb_woid.Text.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_id ='" & txb_woid.Text.Trim() & "' "
            End If
            If dd_cc.SelectedIndex > 0 Then
                StrSql &= " and cd.wocd_cc ='" & dd_cc.SelectedValue & "' "
            End If
            If dd_site.SelectedIndex > 0 Then
                StrSql &= " and cd.wocd_site ='" & dd_site.SelectedValue & "' "
            End If
            If txb_part.Text.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_part like '%" & txb_part.Text.Trim() & "%' "
            End If
            If txb_date1.Text.Trim.Length = 10 And IsDate(txb_date1.Text) Then
                StrSql &= " and cd.wocd_date >= '" & txb_date1.Text & "'"
            End If
            If txb_date2.Text.Trim.Length = 10 And IsDate(txb_date2.Text) Then
                StrSql &= " and cd.wocd_date <= '" & txb_date2.Text & "'"
            End If

            StrSql &= " group by cd.wocd_site,cd.wocd_cc,cd.wocd_nbr,cd.wocd_id,cd.wocd_part,cd.wocd_type,w.wo_gl order by cd.wocd_site,cd.wocd_cc,cd.wocd_nbr,cd.wocd_id,cd.wocd_part "


            'Response.Write(StrSql)

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_nbr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_part", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_comp", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_price", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cost", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_rep", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_diff", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_go", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_go3", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_go4", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("appr_date", System.Type.GetType("System.String")))

            dtl.Columns.Add(New DataColumn("wo_unplan", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("wo_site") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("wo_cc") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("wo_nbr") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("wo_id") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("wo_part") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("wo_rep") = Format(.Rows(i).Item(5), "##0.##")
                        drow.Item("wo_price") = Format(.Rows(i).Item(6), "##0.#####")
                        drow.Item("type") = .Rows(i).Item(7)
                        If drow.Item("type") <> "" And drow.Item("type") <> "T" And drow.Item("type") <> "W" And drow.Item("type") <> "B" Then
                            drow.Item("wo_go") = "<u>核准</u>"
                        End If
                        If drow.Item("type") <> "" And drow.Item("type") <> "R" And drow.Item("type") <> "T" And drow.Item("type") <> "W" And drow.Item("type") <> "B" Then
                            drow.Item("wo_go3") = "<u>结算</u>"
                        End If
                        drow.Item("wo_go4") = "<u>平均</u>"


                        If drow.Item("type") = "" Or drow.Item("type") = "R" Or drow.Item("type") = "W" Or drow.Item("type") = "B" Then

                            dm = GetDomain(.Rows(i).Item(0).ToString().Trim())

                            strQad = GetQadData(dm, .Rows(i).Item(0).ToString().Trim(), .Rows(i).Item(2).ToString().Trim(), .Rows(i).Item(3).ToString().Trim())
                            If strQad.Trim.Length > 0 Then
                                drow.Item("wo_comp") = Format(CDec(strQad.Substring(0, strQad.IndexOf(","))), "##0.#####")
                                drow.Item("wo_date") = strQad.Substring(strQad.IndexOf(",") + 1)
                            End If

                            If drow.Item("type") <> "" Then
                                'StrSql = "Select approveddate from wo_order where woo_site='" & .Rows(i).Item(0).ToString().Trim() & "' and woo_nbr='" & .Rows(i).Item(2).ToString().Trim() & "' and deletedBy is null"
                                StrSql = "Select top 1 wocd_apprdate from wo_cost_detail where wocd_site='" & .Rows(i).Item(0).ToString().Trim() & "' and wocd_nbr='" & .Rows(i).Item(2).ToString().Trim() & "' and wocd_id ='" & .Rows(i).Item(3).ToString().Trim() & "' "
                                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                                If reader.Read() Then
                                    If Not IsDBNull(reader(0)) Then
                                        drow.Item("appr_date") = Format(reader(0), "yy-MM-dd")
                                    End If
                                End If
                                reader.Close()
                            End If

                            If IsDBNull(drow.Item("wo_comp")) Then
                                drow.Item("wo_cost") = 0
                            Else
                                drow.Item("wo_cost") = Format(drow.Item("wo_comp") * drow.Item("wo_price"), "##0.#####")
                            End If
                            'drow.Item("wo_cost") = Format(drow.Item("wo_comp") * drow.Item("wo_price"), "##0.#####")

                        ElseIf drow.Item("type") = "T" Then
                            Dim rd As SqlDataReader = Nothing
                            drow.Item("wo_comp") = "0"
                            drow.Item("wo_cost") = drow.Item("wo_rep")
                            drow.Item("wo_date") = .Rows(i).Item(2).ToString().Trim()
                        Else '计划外用工
                            Dim rd As SqlDataReader = Nothing

                            StrSql = "Select isnull(woo_qty_comp,0),acctApprDate, approveddate  from wo_order where woo_site='" & .Rows(i).Item(0).ToString().Trim() & "' and woo_nbr='" & .Rows(i).Item(2).ToString().Trim() & "' and deletedBy is null"
                            rd = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                            If rd.Read() Then
                                drow.Item("wo_comp") = Format(rd(0), "##0.#####")
                                'drow.Item("wo_cost") = Format(.Rows(i).Item(6) * rd(0), "##0.##")

                                If Not IsDBNull(rd(1)) Then
                                    drow.Item("wo_date") = Format(rd(1), "yy-MM-dd")
                                End If

                                If Not IsDBNull(rd(2)) Then
                                    drow.Item("appr_date") = Format(rd(2), "yy-MM-dd")
                                End If
                            End If
                            rd.Close()
                        End If


                        If IsDBNull(drow.Item("wo_cost")) And Not IsDBNull(drow.Item("wo_rep")) Then
                            drow.Item("wo_diff") = -CDec(drow.Item("wo_rep"))
                        ElseIf Not IsDBNull(drow.Item("wo_cost")) And IsDBNull(drow.Item("wo_rep")) Then
                            drow.Item("wo_diff") = CDec(drow.Item("wo_cost"))
                        ElseIf Not IsDBNull(drow.Item("wo_cost")) And Not IsDBNull(drow.Item("wo_rep")) Then
                            drow.Item("wo_diff") = CDec(drow.Item("wo_cost")) - CDec(drow.Item("wo_rep"))
                        End If

                        'drow.Item("wo_unplan") = Format(.Rows(i).Item(8), "##0.##")

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
            If e.CommandName.CompareTo("wo_edit") = 0 Then
                If e.Item.Cells(19).Text = "R" Then
                    Response.Redirect("WKreturn.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim())
                ElseIf e.Item.Cells(19).Text = "A" Or e.Item.Cells(19).Text = "T" Then
                    Response.Redirect("wo_WKspec.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&cc=" & e.Item.Cells(2).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim())
                ElseIf e.Item.Cells(19).Text = "W" Then
                    Response.Redirect("wo_WKother.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim())
                ElseIf e.Item.Cells(19).Text = "B" Then
                    Response.Redirect("WKpackage.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim())
                Else
                    Response.Redirect("wo_WKnormal.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim())
                End If
            ElseIf e.CommandName.CompareTo("wo_edit1") = 0 Then
                If e.Item.Cells(19).Text = "R" Then
                    Response.Redirect("wo_edit_r3.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim())
                ElseIf e.Item.Cells(19).Text = "W" Then
                    Response.Redirect("wo_WKother.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&cc=" & e.Item.Cells(2).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&sch=1")
                ElseIf e.Item.Cells(19).Text = "B" Then
                    Response.Redirect("WKpackage.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&cc=" & e.Item.Cells(2).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&sch=1")
                ElseIf e.Item.Cells(19).Text = "T" Or e.Item.Cells(19).Text = "A" Then
                    Response.Redirect("wo_edit_a3.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim())
                Else
                    Response.Redirect("wo_edit3.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim())
                End If
            ElseIf e.CommandName.CompareTo("wo_edit2") = 0 Then

                If Not Me.Security("358000099").isValid Then
                    ltlAlert.Text = "alert('没有核准工序定额的权限!'); "
                    Exit Sub
                End If

                If CDec(e.Item.Cells(7).Text) = 0 Then
                    ltlAlert.Text = "alert('此加工单没有完工入库数!'); "
                    Exit Sub
                End If

                StrSql = " UPDATE wo_cost_detail SET  "
                StrSql &= " wocd_appr='" & Session("uID") & "',wocd_apprdate=getdate() "
                StrSql &= " where id is not null "
                StrSql &= " and wocd_site ='" & e.Item.Cells(1).Text & "' "
                StrSql &= " and wocd_cc ='" & e.Item.Cells(2).Text & "' "
                StrSql &= " and wocd_nbr ='" & e.Item.Cells(3).Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                'StrSql = " Update wo_order set woo_price =' " & CDec(e.Item.Cells(12).Text) / CDec(e.Item.Cells(7).Text) & "' "
                StrSql = " UPDATE wo_order SET "
                StrSql &= " approvedBy='" & Session("uID") & "',approveddate=getdate() "
                StrSql &= " ,acctapprBy='" & Session("uID") & "',acctapprdate=getdate() "
                StrSql &= " ,approvedName=N'" & Session("uName") & "' "
                StrSql &= " where woo_id is not null "
                StrSql &= " and woo_site ='" & e.Item.Cells(1).Text & "' "
                StrSql &= " and woo_cc_to ='" & e.Item.Cells(2).Text & "' "
                StrSql &= " and woo_nbr ='" & e.Item.Cells(3).Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                BindData()
            ElseIf e.CommandName.CompareTo("wo_edit3") = 0 Then

                If Not Me.Security("358000098").isValid Then
                    ltlAlert.Text = "alert('没有结算工序定额的权限!'); "
                    Exit Sub
                End If
                If e.Item.Cells(11).Text.Length = 8 Then
                    ltlAlert.Text = "alert('已结算工序定额!'); "
                    Exit Sub
                End If


                If CDec(e.Item.Cells(7).Text) = 0 Then
                    ltlAlert.Text = "alert('此加工单没有完工入库数!'); "
                    Exit Sub
                End If

                StrSql = " Update wo_order SET "
                StrSql &= " acctapprBy='" & Session("uID") & "',acctapprdate=getdate() "
                StrSql &= " ,acctapprName=N'" & Session("uName") & "' "
                StrSql &= " where woo_id is not null "
                StrSql &= " and woo_site ='" & e.Item.Cells(1).Text & "' "
                StrSql &= " and woo_cc_to ='" & e.Item.Cells(2).Text & "' "
                StrSql &= " and woo_nbr ='" & e.Item.Cells(3).Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                BindData()
            ElseIf e.CommandName.CompareTo("wo_avg") = 0 Then
                Response.Redirect("wo_edit_avg.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim())
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('wo_list_export.aspx?st=" & txb_date1.Text & "&et=" & txb_date2.Text & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Sub GetCostCenter1()
            Dim conn As OdbcConnection = Nothing
            Dim comm As OdbcCommand = Nothing
            Dim dr As OdbcDataReader

            Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
            ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

            Dim sql As String = "Select cc_ctr, cc_desc from PUB.cc_mstr where cc_domain='szx' and cc_active=1"
            sql &= " and cc_ctr<>'' order by cc_ctr "
            Try
                conn = New OdbcConnection(connectionString)
                conn.Open()
                comm = New OdbcCommand(sql, conn)
                dr = comm.ExecuteReader()
                While (dr.Read())
                    item = New ListItem
                    item.Value = dr.GetValue(0).ToString()
                    item.Text = dr.GetValue(1).ToString() & "-" & dr.GetValue(0).ToString()
                    dd_cc.Items.Add(item)

                End While
                dr.Close()
                conn.Close()

            Catch oe As OdbcException
                Response.Write(oe.Message)
            Finally
                If conn.State = System.Data.ConnectionState.Open Then
                    conn.Close()
                End If
            End Try

            comm.Dispose()
            conn.Dispose()
        End Sub
        Sub GetCostCenter()

            Dim sql As String = "Select cc_ctr, cc_desc from QAD_data..cc_mstr where cc_domain='szx' and cc_active=1"
            sql &= " and cc_ctr<>'' order by cc_ctr "

            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sql)
            While reader.Read
                item = New ListItem
                item.Value = reader(0)
                item.Text = reader(0) & "-" & reader(1)
                dd_cc.Items.Add(item)
            End While

        End Sub

        Function GetDomain(ByVal str As String) As String
            StrSql = "SELECT realdomain FROM Domain_Mes WHERE qad_site ='" & str & "' "
            GetDomain = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, StrSql)
        End Function

        Function GetQadData1(ByVal strDomain As String, ByVal strSite As String, ByVal strNbr As String, ByVal strLot As String) As String
            Dim strQuery As String = ""
            Dim conn As OdbcConnection = Nothing
            Dim comm As OdbcCommand = Nothing
            Dim dr As OdbcDataReader
            Try
                Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                StrSql = "Select wo_qty_comp,wo_close_date from PUB.wo_mstr where wo_domain='" & strDomain & "' "
                StrSql &= " and wo_site='" & strSite & "' "
                StrSql &= " and wo_nbr='" & strNbr & "' "
                StrSql &= " and wo_lot='" & strLot & "' "

                conn = New OdbcConnection(connectionString)
                conn.Open()
                comm = New OdbcCommand(StrSql, conn)
                dr = comm.ExecuteReader()
                If (dr.Read()) Then
                    If Not IsDBNull(dr.GetValue(0)) Then
                        strQuery = dr.GetValue(0).ToString() & ","
                    Else
                        strQuery = "0" & ","
                    End If

                    If Not IsDBNull(dr.GetValue(1)) Then
                        strQuery = strQuery & Format(dr.GetValue(1), "yy-MM-dd").ToString()
                    End If

                End If
                dr.Close()
                conn.Close()
                'GetQadData = strQuery

            Catch ex As Exception
                Response.Write(ex.Message)
                'GetQadData = ""
            Finally
                If conn.State = System.Data.ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Function

        Function GetQadData(ByVal strDomain As String, ByVal strSite As String, ByVal strNbr As String, ByVal strLot As String) As String
            Dim strQuery As String = ""
            Dim conn As OdbcConnection = Nothing
            Dim comm As OdbcCommand = Nothing
            Dim dr As OdbcDataReader
            Try
                Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                StrSql = "Select wo_qty_comp,wo_close_date from QAD_data..wo_mstr where wo_domain='" & strDomain & "' "
                StrSql &= " and wo_site='" & strSite & "' "
                StrSql &= " and wo_nbr='" & strNbr & "' "
                StrSql &= " and wo_lot='" & strLot & "' "

                reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
                While reader.Read
                    If Not IsDBNull(reader(0)) Then
                        strQuery = reader(0) & ","
                    Else
                        strQuery = "0" & ","
                    End If

                    If Not IsDBNull(reader(1)) Then
                        strQuery = strQuery & Format(reader(1), "yy-MM-dd").ToString()
                    End If
                End While

                GetQadData = strQuery

            Catch ex As Exception
                Response.Write(ex.Message)
                GetQadData = ""
            Finally

            End Try
        End Function


    End Class
End Namespace