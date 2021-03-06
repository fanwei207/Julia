Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Namespace tcpc
    Partial Class wo_cc
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

                txb_date.Text = Format(DateTime.Today, "yyMM")

                StrSql = "SELECT qad_site FROM Domain_Mes WHERE plantcode ='" & Session("PlantCode") & "' "
                reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
                While reader.Read
                    item = New ListItem
                    item.Value = reader(0)
                    item.Text = reader(0)
                    dd_site.Items.Add(item)
                End While
                reader.Close()

                item = New ListItem
                item.Value = 0
                item.Text = "--"
                dd_cc.Items.Add(item)

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
                    'Response.Write(oe.Message)
                Finally
                    If conn.State = System.Data.ConnectionState.Open Then
                        conn.Close()
                    End If
                    If Not comm Is Nothing Then
                        comm.Dispose()
                    End If
                    If Not conn Is Nothing Then
                        conn.Dispose()
                    End If
                End Try
                BindData()
            End If
        End Sub
        Sub BindData()
            '//-------------  Get coefficient      ----------
            Dim coefficient As Decimal = 1.02
            '//----------------------------------------

            Dim dm As String = ""
            StrSql = " select s.wocd_site,s.wocd_cc,s.wocd_nbr,s.wocd_id,s.wocd_part, s.cost,s.pcost,s.type,isnull(ct.wo_cost,0),s.wo_tecHours "
            StrSql &= " from (select cd.wocd_site,cd.wocd_cc,cd.wocd_nbr,cd.wocd_id,cd.wocd_part, sum(CASE WHEN ISNULL(cd.wocd_type,'')='' THEN Round(isnull(cd.wocd_cost,0)*" & coefficient & ",6) ELSE isnull(cd.wocd_cost,0) END ) as cost,isnull(cd.wocd_pcost,0) as pcost,isnull(cd.wocd_type,'') as type,w.wo_tecHours "
            StrSql &= " from wo_cost_detail cd "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If
            StrSql &= " INNER JOIN tcpc0.dbo.wo_Tec w ON w.id = cd.wocd_proc_id "

            StrSql &= " where cd.id is not null and isnull(cd.wocd_pcost,0)>0 "
            If txb_wonbr.Text.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_nbr ='" & txb_wonbr.Text.Trim() & "' "
            End If
            If txb_woid.Text.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_id ='" & txb_woid.Text.Trim() & "' "
            End If
            If dd_cc.SelectedIndex > 0 Then
                StrSql &= " and cd.wocd_cc ='" & dd_cc.SelectedValue & "' "
            End If
            StrSql &= " and cd.wocd_site ='" & dd_site.SelectedValue & "' "
            If txb_part.Text.Trim.Length > 0 Then
                StrSql &= " and cd.wocd_part like '%" & txb_part.Text.Trim() & "%' "
            End If
            StrSql &= " group by cd.wocd_site,cd.wocd_cc,cd.wocd_nbr,cd.wocd_id,cd.wocd_part,cd.wocd_pcost,cd.wocd_type,w.wo_tecHours) s "
            StrSql &= " Left Outer Join wo_cost ct on ct.wo_site=s.wocd_site and ct.wo_nbr=s.wocd_nbr and isnull(ct.wo_id,0)=isnull(s.wocd_id,0) "
            StrSql &= " order by s.wocd_site,s.wocd_cc,s.wocd_nbr,s.wocd_id,s.wocd_part "
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
            dtl.Columns.Add(New DataColumn("wo_comp", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_price", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cost", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_qadcost", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_rep", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_diff", System.Type.GetType("System.String")))

            Dim total_wo As Decimal = 0
            Dim total_lb As Decimal = 0

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1


                        If .Rows(i).Item(7).ToString().Trim() = "" Or .Rows(i).Item(7).ToString().Trim() = "R" Then
                            dm = GetDomain(.Rows(i).Item(0).ToString().Trim())

                            Dim conn As OdbcConnection = Nothing
                            Dim comm As OdbcCommand = Nothing
                            Dim dr As OdbcDataReader

                            Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                            ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

                            Dim sql As String = "Select wo_qty_comp,wo_close_date from PUB.wo_mstr where wo_domain='" & dm & "' "
                            sql &= " and wo_site='" & .Rows(i).Item(0).ToString().Trim() & "' "
                            sql &= " and wo_nbr='" & .Rows(i).Item(2).ToString().Trim() & "' "
                            sql &= " and wo_lot='" & .Rows(i).Item(3).ToString().Trim() & "' "

                            Try
                                conn = New OdbcConnection(connectionString)
                                conn.Open()
                                comm = New OdbcCommand(sql, conn)
                                dr = comm.ExecuteReader()
                                If (dr.Read()) Then
                                    If Not IsDBNull(dr.GetValue(1)) Then
                                        If Format(dr.GetValue(1), "yyMM") = txb_date.Text Then
                                            drow = dtl.NewRow()
                                            drow.Item("wo_site") = .Rows(i).Item(0).ToString().Trim()
                                            drow.Item("wo_cc") = .Rows(i).Item(1).ToString().Trim()
                                            drow.Item("wo_nbr") = .Rows(i).Item(2).ToString().Trim()
                                            drow.Item("wo_id") = .Rows(i).Item(3).ToString().Trim()
                                            drow.Item("wo_part") = .Rows(i).Item(4).ToString().Trim()
                                            drow.Item("wo_rep") = Format(.Rows(i).Item(5), "##0.##")
                                            drow.Item("wo_price") = Format(.Rows(i).Item(9), "##0.#####")

                                            drow.Item("wo_comp") = Format(dr.GetValue(0), "##0.#####")
                                            'drow.Item("wo_cost") = Format(.Rows(i).Item(6) * dr.GetValue(0), "##0.##")
                                            drow.Item("wo_cost") = Format(.Rows(i).Item(9) * dr.GetValue(0), "##0.##")
                                            drow.Item("wo_qadcost") = Format(.Rows(i).Item(9) * dr.GetValue(0), "##0.##")

                                            drow.Item("wo_date") = Format(dr.GetValue(1), "yy-MM-dd")

                                            If .Rows(i).Item(7).ToString().Trim() = "R" Then
                                                drow.Item("wo_diff") = Format(.Rows(i).Item(5) - .Rows(i).Item(5), "##0.##")
                                                total_wo = total_wo + .Rows(i).Item(5)
                                            Else
                                              
                                                drow.Item("wo_diff") = Format(drow.Item("wo_cost") - .Rows(i).Item(5), "##0.##")
                                                total_wo = total_wo + .Rows(i).Item(9) * dr.GetValue(0)
                                            End If
                                            total_lb = total_lb + .Rows(i).Item(5)

                                            dtl.Rows.Add(drow)
                                        End If
                                    End If

                                End If
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
                        ElseIf .Rows(i).Item(7).ToString().Trim() = "T" Then
                            If .Rows(i).Item(2).ToString().Trim().Substring(2) = txb_date.Text Then
                                drow = dtl.NewRow()
                                drow.Item("wo_site") = .Rows(i).Item(0).ToString().Trim()
                                drow.Item("wo_cc") = .Rows(i).Item(1).ToString().Trim()
                                drow.Item("wo_nbr") = .Rows(i).Item(2).ToString().Trim()
                                drow.Item("wo_id") = .Rows(i).Item(3).ToString().Trim()
                                drow.Item("wo_part") = .Rows(i).Item(4).ToString().Trim()
                                drow.Item("wo_rep") = Format(.Rows(i).Item(5), "##0.##")
                                drow.Item("wo_price") = Format(.Rows(i).Item(9), "##0.#####")

                                drow.Item("wo_comp") = "0"
                                drow.Item("wo_date") = .Rows(i).Item(2).ToString().Trim()
                                drow.Item("wo_cost") = Format(.Rows(i).Item(5), "##0.##")
                                drow.Item("wo_qadcost") = Format(.Rows(i).Item(8), "##0.##")

                                drow.Item("wo_diff") = Format(0 - .Rows(i).Item(5), "##0.##")

                                total_wo = total_wo + 0
                                total_lb = total_lb + .Rows(i).Item(5)

                                dtl.Rows.Add(drow)
                            End If
                        Else
                            Dim conn As SqlConnection = Nothing
                            Dim comm As SqlCommand = Nothing
                            Dim dr As SqlDataReader

                            Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn" & Session("PlantCode"))

                            Dim sql As String = "Select isnull(woo_qty_comp,0),acctApprDate  from wo_order where woo_site='" & .Rows(i).Item(0).ToString().Trim() & "' and woo_nbr='" & .Rows(i).Item(2).ToString().Trim() & "' and deletedBy is null"


                            Try
                                conn = New SqlConnection(connectionString)
                                conn.Open()
                                comm = New SqlCommand(sql, conn)
                                dr = comm.ExecuteReader()
                                If (dr.Read()) Then
                                    If Not IsDBNull(dr.GetValue(1)) Then
                                        If Format(dr.GetValue(1), "yyMM") = txb_date.Text Then
                                            drow = dtl.NewRow()
                                            drow.Item("wo_site") = .Rows(i).Item(0).ToString().Trim()
                                            drow.Item("wo_cc") = .Rows(i).Item(1).ToString().Trim()
                                            drow.Item("wo_nbr") = .Rows(i).Item(2).ToString().Trim()
                                            drow.Item("wo_id") = .Rows(i).Item(3).ToString().Trim()
                                            drow.Item("wo_part") = .Rows(i).Item(4).ToString().Trim()
                                            drow.Item("wo_rep") = Format(.Rows(i).Item(5), "##0.##")
                                            drow.Item("wo_price") = Format(.Rows(i).Item(9), "##0.#####")

                                            drow.Item("wo_comp") = Format(dr.GetValue(0), "##0.#####")
                                            'drow.Item("wo_cost") = Format(.Rows(i).Item(6) * dr.GetValue(0), "##0.##")
                                            drow.Item("wo_cost") = 0
                                            drow.Item("wo_qadcost") = Format(.Rows(i).Item(8), "##0.##")


                                            drow.Item("wo_date") = Format(dr.GetValue(1), "yy-MM-dd")
                                            drow.Item("wo_diff") = Format(CDec(drow.Item("wo_cost")) - .Rows(i).Item(5), "##0.##")

                                            total_wo = total_wo + .Rows(i).Item(5)
                                            total_lb = total_lb + .Rows(i).Item(5)

                                            dtl.Rows.Add(drow)


                                        End If
                                    End If

                                End If
                                dr.Close()
                                conn.Close()

                            Catch oe As OdbcException
                                Response.Write(oe.Message)
                            Finally
                                If conn.State = System.Data.ConnectionState.Open Then
                                    conn.Close()
                                End If
                            End Try

                            If Not comm Is Nothing Then
                                comm.Dispose()
                            End If

                            conn.Dispose()

                        End If
                    Next
                End If
            End With
            ds.Reset()

            lbl_total.Text = "合计工时:" & Format(total_lb, "##0.##")
            lbl_diff.Text = "合计差异:" & Format(total_wo - total_lb, "##0.##")

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()
        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("wo_cc_list") = 0 Then
                ltlAlert.Text = "var w=window.open('wo_cc_export2.aspx?site=" & e.Item.Cells(1).Text.Trim() & "&cc=" & e.Item.Cells(2).Text.Trim() & "&nbr=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(4).Text.Trim() & "&yy=" & txb_date.Text & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('wo_cc_export1.aspx?site=" & dd_site.SelectedValue & "&cc=" & dd_cc.SelectedValue & "&yy=" & txb_date.Text & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub


        Function GetDomain(ByVal str As String) As String
            StrSql = "SELECT realdomain FROM Domain_Mes WHERE qad_site ='" & str & "' "
            GetDomain = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, StrSql)
        End Function
    End Class

End Namespace













