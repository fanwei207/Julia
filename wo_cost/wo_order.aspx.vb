Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class wo_order
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
                Select Case Session("PlantCode")
                    Case "1"
                        ls = New ListItem
                        ls.Value = "1000"
                        ls.Text = "1000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2100"
                        ls.Text = "2100"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "S110"
                        ls.Text = "S110"
                        dd_site.Items.Add(ls)
                    Case "2"
                        ls = New ListItem
                        ls.Value = "2000"
                        ls.Text = "2000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1200"
                        ls.Text = "1200"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "3000"
                        ls.Text = "3000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "S120"
                        ls.Text = "S120"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "S220"
                        ls.Text = "S220"
                        dd_site.Items.Add(ls)
                    Case "5"
                        ls = New ListItem
                        ls.Value = "4000"
                        ls.Text = "4000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1400"
                        ls.Text = "1400"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2400"
                        ls.Text = "2400"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "S140"
                        ls.Text = "S140"
                        dd_site.Items.Add(ls)
                    Case "8"
                        ls = New ListItem
                        ls.Value = "1500"
                        ls.Text = "1500"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "5000"
                        ls.Text = "5000"
                        dd_site.Items.Add(ls)
                End Select

                Select Case dd_site.SelectedValue
                    Case "1000"
                        lbl_domain.Text = "szx"
                    Case "1200"
                        lbl_domain.Text = "szx"
                    Case "1400"
                        lbl_domain.Text = "szx"
                    Case "2000"
                        lbl_domain.Text = "zql"
                    Case "2100"
                        lbl_domain.Text = "zql"
                    Case "2400"
                        lbl_domain.Text = "zql"
                    Case "3000"
                        lbl_domain.Text = "zql"
                    Case "4000"
                        lbl_domain.Text = "yql"
                        'Case "5000"
                        '    lbl_domain.Text = "tcb"
                    Case "6000"
                        lbl_domain.Text = "ytc"
                    Case "7000"
                        lbl_domain.Text = "zfx"
                    Case "8000"
                        lbl_domain.Text = "zjn"
                    Case "9000"
                        lbl_domain.Text = "thk"
                    Case "a000"
                        lbl_domain.Text = "jql"
                    Case "b000"
                        lbl_domain.Text = "sfx"
                        'Case "c000"
                        '    lbl_domain.Text = "hql"
                    Case "1500"
                        lbl_domain.Text = "hql"
                    Case "5000"
                        lbl_domain.Text = "hql"
                End Select


                CcSuppliers()

                BindData()
            End If
        End Sub
        Sub BindData()
            StrSql = " select woo_id,woo_site,woo_cc_from,woo_cc_to,woo_nbr,woo_type,woo_qty,woo_req, createdDate, isnull(createdName,''),isnull(woo_cc_duty,0),isnull(woo_supplier,0),isnull(woo_rtfile,'') from wo_order"
            StrSql &= " where deletedBy is null "
            StrSql &= " and woo_site ='" & dd_site.SelectedValue & "' "

            If dd_cc1.SelectedIndex > 0 Then
                StrSql &= " and woo_cc_from ='" & dd_cc1.SelectedValue & "' "
            End If

            If dd_cc2.SelectedIndex > 0 Then
                StrSql &= " and woo_cc_to ='" & dd_cc2.SelectedValue & "' "
            End If
            If dd_cc3.SelectedIndex > 0 Then
                StrSql &= " and woo_cc_duty ='" & dd_cc3.SelectedValue & "' "
            End If
            If dd_supplier.SelectedIndex > 0 Then
                StrSql &= " and woo_supplier ='" & dd_supplier.SelectedValue & "' "
            End If


            If txb_nbr.Text.Trim.Length > 0 Then
                StrSql &= " and woo_nbr ='" & txb_nbr.Text.Trim() & "' "
            End If

            StrSql &= " order by woo_site,woo_nbr desc"

            Session("EXTitle") = "60^<b>地点</b>~^150^<b>提出部门</b>~^150^<b>加工部门</b>~^150^<b>加工单号</b>~^30^<b>类型</b>~^<b>数量</b>~^150^<b>承担部门</b>~^500^<b>工艺要求</b>~^350^<b>承担供应商</b>~^<b>工艺文件</b>~^<b>日期</b>~^50^<b>创建人</b>~^"
            Session("EXSQL") = " select woo_site,woo_cc_from_n,woo_cc_to_n,woo_nbr,woo_type,woo_qty,woo_cc_duty_n,woo_req,woo_supplier_n,woo_rtfile, createdDate, isnull(createdName,'') from wo_order"
            Session("EXSQL") &= " where deletedBy is null "
            Session("EXSQL") &= " and woo_site ='" & dd_site.SelectedValue & "' "

            If dd_cc1.SelectedIndex > 0 Then
                Session("EXSQL") &= " and woo_cc_from ='" & dd_cc1.SelectedValue & "' "
            End If

            If dd_cc2.SelectedIndex > 0 Then
                Session("EXSQL") &= " and woo_cc_to ='" & dd_cc2.SelectedValue & "' "
            End If
            If dd_cc3.SelectedIndex > 0 Then
                Session("EXSQL") &= " and woo_cc_duty ='" & dd_cc3.SelectedValue & "' "
            End If
            If dd_supplier.SelectedIndex > 0 Then
                Session("EXSQL") &= " and woo_supplier ='" & dd_supplier.SelectedValue & "' "
            End If
            If txb_nbr.Text.Trim.Length > 0 Then
                Session("EXSQL") &= " and woo_nbr ='" & txb_nbr.Text.Trim() & "' "
            End If
            Session("EXSQL") &= " order by woo_site,woo_cc_from, woo_cc_to,woo_nbr"
            Session("EXHeader") = "计划外用工结算单    导出日期: " & Format(DateTime.Today, "yyyy-MM-dd")

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cc1", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cc2", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_nbr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_type", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_qty", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_req", System.Type.GetType("System.String")))

            dtl.Columns.Add(New DataColumn("proc_by", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cc_duty", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_supplier", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_rtfile", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("wo_site") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("wo_cc1") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("wo_cc2") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("wo_nbr") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("wo_type") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("wo_qty") = Format(.Rows(i).Item(6), "##0.#####")
                        drow.Item("wo_req") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("proc_by") = .Rows(i).Item(9).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(8)) Then
                            drow.Item("proc_date") = Format(.Rows(i).Item(8), "yy-MM-dd")
                        End If
                        drow.Item("wo_cc_duty") = .Rows(i).Item(10).ToString().Trim()
                        drow.Item("wo_supplier") = .Rows(i).Item(11).ToString().Trim()
                        drow.Item("wo_rtfile") = .Rows(i).Item(12).ToString().Trim()


                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)
            Try
                Datagrid1.DataSource = dvw
                Datagrid1.DataBind()
            Catch

            End Try
        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("proc_edit") = 0 Then
                If Not Security("358000080").isValid Then
                    ltlAlert.Text = "alert('无编辑的权限.')"
                    Exit Sub
                End If

                dd_site.SelectedValue = e.Item.Cells(1).Text
                dd_cc1.SelectedValue = e.Item.Cells(2).Text
                dd_cc2.SelectedValue = e.Item.Cells(3).Text
                dd_cc3.SelectedValue = e.Item.Cells(13).Text
                dd_supplier.SelectedValue = e.Item.Cells(14).Text
                txb_nbr.Text = e.Item.Cells(4).Text
                lbl_id.Text = e.Item.Cells(0).Text
                dd_type.SelectedValue = e.Item.Cells(5).Text
                txb_qty.Text = e.Item.Cells(6).Text
                txb_req.Text = e.Item.Cells(7).Text
                txb_rtfile.Text = e.Item.Cells(15).Text.Replace("&nbsp;", "")

                btn_add.Text = "修改"

                dd_site.Enabled = False
                dd_cc1.Enabled = False
                dd_cc2.Enabled = False
                dd_type.Enabled = False
                txb_nbr.Enabled = False

            ElseIf e.CommandName.CompareTo("proc_del") = 0 Then
                If Not Security("358000081").isValid Then
                    ltlAlert.Text = "alert('无删除的权限.')"
                    Exit Sub
                End If


                If Session("uRole") <> 1 Then

                    StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & e.Item.Cells(3).Text & "' and perm_userid='" & Session("uID") & "'"
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                        ltlAlert.Text = "alert('无操作此成本中心的权限B.')"
                        Exit Sub
                    End If
                End If

                StrSql = " Update wo_order set deletedBy='" & Session("uID") & "',deletedDate=getdate() where woo_id='" & e.Item.Cells(0).Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                Datagrid1.CurrentPageIndex = 0
                BindData()
            ElseIf e.CommandName.CompareTo("proc_list") = 0 Then
                Response.Redirect("wo_group_list.aspx?gid=" & e.Item.Cells(0).Text)
            ElseIf e.CommandName.CompareTo("proc_prn") = 0 Then
                ltlAlert.Text = "var w=window.open('wo_order_export2.aspx?site=" & e.Item.Cells(1).Text & "&id=" & e.Item.Cells(0).Text & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"
            ElseIf e.CommandName.CompareTo("proc_attach") = 0 Then
                ltlAlert.Text = "var w=window.open('wo_order_attach.aspx?site=" & e.Item.Cells(1).Text & "&id=" & e.Item.Cells(0).Text & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"
            End If
        End Sub
        Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
            If Not Security("358000080").isValid Then
                ltlAlert.Text = "alert('无编辑的权限.')"
                Exit Sub
            End If

            If dd_type.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('请选择类别.')"
                Exit Sub
            End If

            If Session("uRole") <> 1 Then
                StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & dd_cc2.SelectedValue & "' and perm_userid='" & Session("uID") & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                    ltlAlert.Text = "alert('无操作此成本中心的权限A.')"
                    Exit Sub
                End If
            End If
            If lbl_id.Text.Trim.Length > 0 Then
                StrSql = " Update wo_order set woo_qty='" & txb_qty.Text & "',woo_req=N'" & txb_req.Text & "' "
                StrSql &= " ,woo_cc_duty='" & dd_cc3.SelectedValue & "',woo_cc_duty_n=N'" & dd_cc3.SelectedItem.Text & "'"
                StrSql &= " ,woo_supplier='" & dd_supplier.SelectedValue & "',woo_supplier_n=N'" & dd_supplier.SelectedItem.Text & "'"
                If txb_rtfile.Text.Trim.Length > 0 Then
                    StrSql &= " ,woo_rtfile=N'" & txb_rtfile.Text & "'"
                End If
                StrSql &= " where woo_id='" & lbl_id.Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            Else
                StrSql = " Select count(woo_id) from wo_order where woo_site='" & dd_site.SelectedValue & "' and woo_nbr='" & txb_nbr.Text & "' and deletedBy is null"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                    ltlAlert.Text = "alert('此加工单号已存在.')"
                    Exit Sub
                End If
                StrSql = " Insert into wo_order(woo_site,woo_cc_from,woo_cc_to, woo_nbr,woo_type,woo_qty,woo_req,createdDate,createdBy,woo_cc_duty,woo_supplier,woo_cc_from_n,woo_cc_to_n,woo_cc_duty_n,woo_supplier_n,woo_rtfile,createdName) "
                StrSql &= " values('" & dd_site.SelectedValue & "','" & dd_cc1.SelectedValue & "','" & dd_cc2.SelectedValue & "','" & txb_nbr.Text & "','" & dd_type.SelectedValue & "','" & txb_qty.Text & "',N'" & chk.sqlEncode(txb_req.Text) & "',getdate(),'" & Session("uID") & "','" & dd_cc3.SelectedValue & "','" & dd_supplier.SelectedValue & "',N'" & dd_cc1.SelectedItem.Text & "',N'" & dd_cc2.SelectedItem.Text & "',N'" & dd_cc3.SelectedItem.Text & "',N'" & dd_supplier.SelectedItem.Text & "',N'" & txb_rtfile.Text & "',N'" & Session("uName") & "')"

                'Response.Write(StrSql)
                'Exit Sub

                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            End If

            txb_nbr.Text = ""
            lbl_id.Text = ""
            txb_qty.Text = ""
            txb_req.Text = ""
            dd_cc1.SelectedIndex = 0
            dd_cc2.SelectedIndex = 0
            dd_cc3.SelectedIndex = 0
            dd_supplier.SelectedIndex = 0
            dd_type.SelectedIndex = 0
            txb_rtfile.Text = ""

            btn_add.Text = "增加"
            dd_site.Enabled = True
            dd_cc1.Enabled = True
            dd_cc2.Enabled = True
            dd_type.Enabled = True
            txb_nbr.Enabled = True



            BindData()
        End Sub

        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            Datagrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click

            ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx" & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"
        End Sub

        Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
            txb_nbr.Text = ""
            lbl_id.Text = ""
            txb_qty.Text = ""
            txb_req.Text = ""
            dd_cc1.SelectedIndex = 0
            dd_cc2.SelectedIndex = 0
            dd_cc3.SelectedIndex = 0
            dd_supplier.SelectedIndex = 0
            dd_type.SelectedIndex = 0
            txb_rtfile.Text = ""

            btn_add.Text = "增加"
            dd_site.Enabled = True
            dd_cc1.Enabled = True
            dd_cc2.Enabled = True
            dd_type.Enabled = True
            txb_nbr.Enabled = True

        End Sub

        Protected Sub dd_site_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_site.SelectedIndexChanged
            Select Case dd_site.SelectedValue
                Case "1000"
                    lbl_domain.Text = "szx"
                Case "1200"
                    lbl_domain.Text = "szx"
                Case "1400"
                    lbl_domain.Text = "szx"
                Case "2000"
                    lbl_domain.Text = "zql"
                Case "2100"
                    lbl_domain.Text = "zql"
                Case "2400"
                    lbl_domain.Text = "zql"
                Case "3000"
                    lbl_domain.Text = "zql"
                Case "4000"
                    lbl_domain.Text = "yql"
                    'Case "5000"
                    '    lbl_domain.Text = "tcb"
                Case "6000"
                    lbl_domain.Text = "ytc"
                Case "7000"
                    lbl_domain.Text = "zfx"
                Case "8000"
                    lbl_domain.Text = "zjn"
                Case "9000"
                    lbl_domain.Text = "thk"
                Case "a000"
                    lbl_domain.Text = "jql"
                Case "b000"
                    lbl_domain.Text = "sfx"
                    'Case "c000"
                    '    lbl_domain.Text = "hql"
                Case "1500"
                    lbl_domain.Text = "hql"
                Case "5000"
                    lbl_domain.Text = "hql"

            End Select
            CcSuppliers()
            Datagrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            If lbl_id.Text.Trim.Length > 0 Then
                ltlAlert.Text = "alert('编辑状态不能翻页！.')"
                Exit Sub
            End If
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub


        Protected Sub txtSup_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSup.TextChanged
            If txtSup.Text.Trim.Length > 0 Then
                Dim i As Integer
                For i = 0 To dd_supplier.Items.Count - 1

                    If dd_supplier.Items(i).Text.IndexOf(txtSup.Text.Trim) <> -1 Then
                        dd_supplier.SelectedIndex = i
                    End If
                Next

            End If
        End Sub


        Sub CcSuppliers()

            dd_cc1.Items.Clear()
            dd_cc2.Items.Clear()
            dd_cc3.Items.Clear()
            dd_supplier.Items.Clear()

            Dim ls As ListItem
            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            dd_cc1.Items.Add(ls)

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            dd_cc2.Items.Add(ls)

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            dd_cc3.Items.Add(ls)

            Dim conn As OdbcConnection = Nothing
            Dim comm As OdbcCommand = Nothing
            Dim dr As OdbcDataReader

            Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
            ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

            Dim sql As String = "Select cc_ctr, cc_desc from PUB.cc_mstr where cc_domain='" & lbl_domain.Text & "' and cc_active=1 "
            sql &= " and cc_ctr<>'' order by cc_ctr "
            Try
                conn = New OdbcConnection(connectionString)
                conn.Open()
                comm = New OdbcCommand(sql, conn)
                dr = comm.ExecuteReader()
                While (dr.Read())
                    ls = New ListItem
                    ls.Value = dr.GetValue(0).ToString()
                    ls.Text = dr.GetValue(1).ToString() & "-" & dr.GetValue(0).ToString()
                    dd_cc1.Items.Add(ls)

                    ls = New ListItem
                    ls.Value = dr.GetValue(0).ToString()
                    ls.Text = dr.GetValue(1).ToString() & "-" & dr.GetValue(0).ToString()
                    dd_cc2.Items.Add(ls)

                    ls = New ListItem
                    ls.Value = dr.GetValue(0).ToString()
                    ls.Text = dr.GetValue(1).ToString() & "-" & dr.GetValue(0).ToString()
                    dd_cc3.Items.Add(ls)

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

            Dim conn2 As OdbcConnection = Nothing
            Dim comm2 As OdbcCommand = Nothing

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            dd_supplier.Items.Add(ls)


            sql = "Select ad_addr, ad_name from PUB.ad_mstr a where ad_type = 'Supplier' and ad_domain='" & lbl_domain.Text & "' "
            sql &= " and (substring(ad_addr,1,1)='S' or ad_addr='W0021293' or ad_addr='W0021047' or ad_addr='W0021249') order by ad_addr,ad_name "
            'sql &= " order by ad_name,ad_addr "
            Try
                conn2 = New OdbcConnection(connectionString)
                conn2.Open()
                comm2 = New OdbcCommand(sql, conn2)
                dr = comm2.ExecuteReader()
                While (dr.Read())
                    ls = New ListItem
                    ls.Value = dr.GetValue(0).ToString()
                    ls.Text = dr.GetValue(1).ToString() & "-" & dr.GetValue(0).ToString()
                    dd_supplier.Items.Add(ls)

                End While
                dr.Close()
                conn2.Close()

            Catch oe As OdbcException
                'Response.Write(oe.Message)
            Finally
                If conn2.State = System.Data.ConnectionState.Open Then
                    conn2.Close()
                End If
                If Not comm2 Is Nothing Then
                    comm2.Dispose()
                End If
                If Not conn2 Is Nothing Then
                    conn2.Dispose()
                End If
            End Try

        End Sub

    End Class

End Namespace













