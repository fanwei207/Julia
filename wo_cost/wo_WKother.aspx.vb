Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Data
Imports WO2Group

Partial Class wo_cost_wo_WKother
    Inherits BasePage
    Public chk As New adamClass
    Public hr_salary As New WO2
    Dim StrSql As String
    Dim ds As DataSet
    Dim nRet As Integer
    Dim item As ListItem
    Dim reader As SqlDataReader

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
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

            dd_group.Items.Add(New ListItem("--", "0"))

            If Request("d") <> Nothing Then
                txb_date.Text = Request("d")
            Else
                txb_date.Text = Format(Today, "yyyy-MM-dd")
            End If

            If Request("cc") <> Nothing And Request("site") <> Nothing Then
                txb_cc.Text = Request("cc")
                dd_site.SelectedValue = Request("site")
                txb_wonbr.Text = Request("nbr")
            End If

            If Request("sch") Is Nothing Then
                StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                btn_clear.Attributes.Add("onclick", "return confirm('确定要清除所有数据，重新开始汇报?');")
            End If

            getWOInfo()

            BindData()
        End If
    End Sub

    Sub BindData()

        Dim strTemp As String

        If Request("sch") Is Nothing Then
            strTemp = "tcpc0.dbo.wo_cost_detail_tmp"
        Else
            strTemp = "wo_cost_detail"
        End If

        StrSql = " select cd.id,cd.wocd_site,cd.wocd_nbr,cd.wocd_id,cd.wocd_cc, cd.wocd_part,cd.wocd_user_no,cd.wocd_username,cd.wocd_proc_name,isnull(cd.wocd_proc_qty,0)" _
               & "          , isnull(cd.wocd_proc_adj, 0), isnull(cd.wocd_cost, 0), cd.wocd_date, isnull(cd.createdName, ''), isnull(cd.wocd_group_id, 0), isnull(cd.wocd_price, 0) " _
               & "          , wocd_cost_unitPrice = Isnull(wocd_cost_unitPrice, 0), wocd_unitPrice = Isnull(wocd_unitPrice, 0)" _
               & " from " & strTemp & " cd "
        If Session("uRole") <> 1 Then
            StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
        End If
        StrSql &= " where cd.id is not null  and isnull(cd.wocd_type,'')='W'"

        If Session("uRole") <> 1 Then
            StrSql &= " and cd.createdBy='" & Session("uID") & "'"
        End If
        If Request("nbr") <> Nothing Then
            StrSql &= " and cd.wocd_nbr ='" & Request("nbr").ToString() & "' "
        End If
        If dd_site.SelectedIndex > 0 Then
            StrSql &= " and cd.wocd_site ='" & dd_site.SelectedValue & "' "
        End If
        If txb_cc.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_cc ='" & txb_cc.Text.Trim() & "' "
        End If

        If txb_proc.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_proc_name =N'" & txb_proc.Text.Trim() & "' "
        End If

        StrSql &= " order by cd.wocd_date desc,cd.wocd_user_no  "


        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("sid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("user_name", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_name", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_qty", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_adj", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_price1", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("wo_date_comp", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_cc", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_nbr", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_id", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("wo_created", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_pri", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("wo_site", System.Type.GetType("System.String")))

        dtl.Columns.Add(New DataColumn("proc_unitPrice", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_cost_unitPrice", System.Type.GetType("System.String")))

        Dim drow As DataRow
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1

                    drow = dtl.NewRow()
                    drow.Item("sid") = i + 1
                    drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                    drow.Item("proc_nbr") = .Rows(i).Item(2).ToString().Trim()
                    drow.Item("proc_id") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("proc_cc") = .Rows(i).Item(4).ToString().Trim()
                    drow.Item("user_id") = .Rows(i).Item(6).ToString().Trim()
                    drow.Item("user_name") = .Rows(i).Item(7).ToString().Trim()
                    drow.Item("proc_name") = .Rows(i).Item(8).ToString().Trim()
                    drow.Item("proc_qty") = Format(.Rows(i).Item(9), "##0.#####")
                    drow.Item("proc_adj") = .Rows(i).Item(14).ToString()
                    drow.Item("proc_price1") = Format(.Rows(i).Item(11), "##0.#####") '汇报工时
                    If Not IsDBNull(.Rows(i).Item(12)) Then
                        drow.Item("wo_date_comp") = Format(.Rows(i).Item(12), "yy-MM-dd")
                    End If
                    drow.Item("wo_created") = .Rows(i).Item(13).ToString().Trim()

                    drow.Item("proc_pri") = Format(.Rows(i).Item(10), "##0.#####") '指标
                    drow.Item("wo_site") = .Rows(i).Item(1).ToString().Trim()

                    drow.Item("proc_unitPrice") = Format(.Rows(i).Item("wocd_unitPrice"), "##0.#####") '工序单价
                    drow.Item("proc_cost_unitPrice") = Format(.Rows(i).Item("wocd_cost_unitPrice"), "##0.#####") '汇报总额

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

        If e.CommandName.CompareTo("proc_del") = 0 Then
            Dim strTemp As String

            If Request("sch") Is Nothing Then
                strTemp = "tcpc0.dbo.wo_cost_detail_tmp"
            Else
                strTemp = "wo_cost_detail"
            End If

            StrSql = "Delete from " & strTemp & " where id='" & e.Item.Cells(1).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            If Request("sch") <> Nothing Then
                Dim count As Decimal = 0
                StrSql = " SELECT ISNULL(SUM(ISNULL(wocd_unplan, 1)),0) " _
                       & " FROM " & strTemp _
                       & " WHERE wocd_nbr = '" & e.Item.Cells(5).Text & "'" _
                       & "      And wocd_site= '" & e.Item.Cells(15).Text & "' " _
                       & "      And wocd_group_id = '" & e.Item.Cells(10).Text & "'" _
                       & "      And datediff(day, wocd_date, '" & CDate(e.Item.Cells(12).Text).ToShortDateString() & "') = 0 " _
                       & "      And wocd_type = 'W'" _
                       & "      And (wocd_proc_qty - " & e.Item.Cells(8).Text & ") = 0 "
                count = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)


                If count > 0 Then
                    StrSql = "  Update  wo_cost_detail" _
                            & " SET wocd_cost = Round( ISNULL(wocd_unplan, 1) * " & CDec(e.Item.Cells(8).Text) * CDec(txb_procprice.Text) / count & ", 5)" _
                            & "     , wocd_proc_adj = ROUND(ISNULL(wocd_unplan, 1) / " & count & ", 5) " _
                            & "     , wocd_cost_unitPrice = Round( ISNULL(wocd_unplan, 1) * " & CDec(e.Item.Cells(8).Text) * CDec(txtUnitPrice.Text) / count & ", 5)"
                    StrSql &= " WHERE wocd_nbr ='" & e.Item.Cells(5).Text & "' And wocd_site='" & e.Item.Cells(15).Text & "'" _
                            & "     And wocd_group_id ='" & e.Item.Cells(10).Text & "'  And datediff(day,wocd_date,'" & CDate(e.Item.Cells(12).Text).ToShortDateString() & "')=0 " _
                            & "     And wocd_type='W'  AND (wocd_proc_qty - " & e.Item.Cells(8).Text & ")=0 "
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                End If
            End If

            BindData()
        End If
    End Sub

    Protected Sub btnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssign.Click


        If txb_date.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('入库日期 不能为空！');"
            Return
        Else
            Try
                Dim _dc As DateTime = Convert.ToDateTime(txb_date.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('入库日期 格式不正确！');"
                Return
            End Try
        End If

        If txb_cc.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('成本中心 不能为空！');"
            Return
        End If

        If txb_procprice.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('工序定额 不能为空！');"
            Return
        Else
            Try
                Dim _dc As Decimal = Convert.ToDecimal(txb_procprice.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('工序定额 只能是数字！');"
                Return
            End Try
        End If

        If txb_proc.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('工序 不能为空！');"
            Return
        End If

        If txb_qty.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('数量 不能为空！');"
            Return
        Else
            Try
                Dim _dc As Decimal = Convert.ToDecimal(txb_qty.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('数量 只能是数字！');"
                Return
            End Try
        End If


        '/--Add by Simon in September,14 2009 ------/
        '/-- 如果datagride里没有数据则推出此过程 -----/
        If Datagrid1.Items.Count = 0 Then
            ltlAlert.Text = "alert('没有可分配的数据.')"
            Exit Sub
        End If
        '/-- END -------------------------------/

        '已汇报各道工序数量之和
        Dim wocd_fixed As Decimal = 0
        StrSql = " SELECT ISNULL(Sum(ISNULL(wocd_proc_qty, 0)), 0) " _
            & "    FROM (   SELECT wocd_proc_qty " _
            & "             FROM wo_cost_detail " _
            & "             WHERE wocd_site='" & dd_site.SelectedValue & "' And wocd_nbr='" & txb_wonbr.Text & "' And wocd_type='W' " _
            & "             Group By wocd_site,wocd_nbr,wocd_group_id,wocd_date,wocd_proc_qty" _
            & "         ) As a "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        If reader.HasRows = True Then
            While reader.Read
                wocd_fixed = reader(0)
            End While
        End If
        reader.Close()

        If wocd_fixed + CDec(txb_qty.Text) > CDec(lbl_fixup.Text) Then
            ltlAlert.Text = "alert('汇报数据已超过修复数量！')"
            Exit Sub
        End If

        Dim i As Integer = 0
        For i = 0 To Datagrid1.Items.Count - 1

            StrSql = "update tcpc0.dbo.wo_cost_detail_tmp set wocd_proc_qty = '" & txb_qty.Text & "' Where id='" & Datagrid1.Items(i).Cells(1).Text & "'"

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
        Next

        BindData()
    End Sub

    Function getWOInfo() As Boolean
        Dim dm As String = ""
        dm = GetDomain(dd_site.SelectedValue)

        If txb_wonbr.Text.Trim.Length > 0 Then
            StrSql = " SELECT pt_part,pt_desc1 + pt_desc2,wo_qty_rjct,ISNULL(t.fixup,0),wo_lot FROM QAD_data..wo_mstr "
            StrSql &= " INNER JOIN (SELECT tr_domain, tr_part, Ltrim(Rtrim(tr_serial)) as torder,SUM(tr_qty_loc) as fixup FROM Qad_data..tr_hist INNER JOIN Qad_data..trgl_det ON trgl_domain =tr_domain AND trgl_trnbr=tr_trnbr WHERE Ltrim(Rtrim(tr_serial))='" & txb_wonbr.Text.Trim() & "' And tr_domain='" & dm & "' AND  trgl_cr_acct='50010070' And tr_type='RCT-UNP' GROUP BY tr_domain,tr_serial,tr_part) t  ON t.tr_domain =wo_domain and torder = wo_nbr  and tr_part = wo_part "
            StrSql &= " LEFT OUTER JOIN QAD_data..pt_mstr ON pt_domain=wo_domain AND wo_part=pt_part "
            StrSql &= " WHERE wo_domain='" & dm & "' AND wo_nbr='" & txb_wonbr.Text.Trim() & "' and wo_qty_rjct>0  and ISNULL(wo_type,'')='' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            If (reader.HasRows = True) Then
                While reader.Read
                    lbl_part.Text = reader(0).ToString()
                    lbl_desc.Text = reader(1).ToString()
                    lbl_reject.Text = Format(reader(2), "##0.#####")
                    lbl_fixup.Text = Format(reader(3), "##0.#####")
                    lbl_ID.Text = reader(4).ToString()
                End While
            Else
                ltlAlert.Text = "alert('加工单不存在或加工单没有次品数量！.')"
                WKReset()
                reader.Close()
                Exit Function
            End If
            reader.Close()
        Else
            WKReset()
            Exit Function
        End If

        'Group build
        If lbl_cc1.Text.Length <= 0 Or txb_cc.Text <> lbl_cc1.Text Then

            lbl_cc1.Text = txb_cc.Text

            Dim ls As ListItem
            dd_group.Items.Clear()

            If Session("Plantcode") = 1 Then
                ls = New ListItem
                ls.Value = 0
                ls.Text = "--"
                dd_group.Items.Add(ls)

                StrSql = "select wog_id,wog_name from tcpc0.dbo.wo_group where deletedBy is null and wog_cc='" & txb_cc.Text & "'  order by wog_name"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                While (reader.Read())
                    ls = New ListItem
                    ls.Value = reader(0)
                    ls.Text = reader(1).ToString.Trim()
                    dd_group.Items.Add(ls)
                End While
                reader.Close()
            Else
                Dim fs As DataTable = hr_salary.SelectGroupInfo()
                Dim i As Integer
                If fs.Rows.Count > 0 Then
                    For i = 0 To fs.Rows.Count - 1
                        ls = New ListItem
                        ls.Value = fs.Rows(i).Item(0)
                        ls.Text = fs.Rows(i).Item(1)

                        dd_group.Items.Add(ls)
                    Next
                End If
            End If

        End If  'End Group 


        ' Workprocedure
        If lbl_part.Text.Trim <> lbl_item.Text.Trim Then
            lbl_item.Text = lbl_part.Text

            StrSql = " SELECT wo_procName, wo_gl" _
                   & "      , wo_price_szx = Isnull(wo_price_szx, 0) " _
                   & "      , wo_price_zql = Isnull(wo_price_szx, 0) " _
                   & "      , wo_price_yql = Isnull(wo_price_szx, 0) " _
                   & " FROM tcpc0..wo_Tec WHERE wo_Tec = '" & lbl_item.Text & "' and Ltrim(Rtrim(wo_procName))=N'维修' and wo_del=0 "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            If (reader.HasRows = True) Then
                While reader.Read
                    txb_proc.Text = reader(0)
                    txb_procprice.Text = reader("wo_gl")

                    If Session("Plantcode") = 1 Then
                        txtUnitPrice.Text = reader("wo_price_szx")
                    ElseIf Session("Plantcode") = 2 Then
                        txtUnitPrice.Text = reader("wo_price_zql")
                    ElseIf Session("Plantcode") = 5 Then
                        txtUnitPrice.Text = reader("wo_price_yql")
                    ElseIf Session("Plantcode") = 8 Then
                        txtUnitPrice.Text = reader("wo_price_hql")
                    End If
                End While
            Else
                txb_proc.Text = ""
                txb_procprice.Text = ""
            End If
            reader.Close()
        End If 'End workprocedure

        'If the status is searching information
        If Request("sch") <> Nothing Then
            dd_site.Enabled = False
            txb_date.Enabled = False
            txb_wonbr.Enabled = False
            txb_cc.Enabled = False
            dd_group.Enabled = False
            txb_no.Enabled = False
            txb_qty.Enabled = False
            btnAssign.Visible = False
            btn_save.Visible = False
            btnAdd.Visible = False
            btn_clear.Visible = False
            btn_woload.Visible = False
        End If

    End Function

    Protected Sub btn_woload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_woload.Click
        getWOInfo()

        BindData()

    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click

        Dim Saveflag As Boolean = False

        StrSql = "sp_wo_InserFixupData"
        Dim params(1) As SqlParameter
        params(0) = New SqlParameter("uid", CInt(Session("uID")))
        params(1) = New SqlParameter("plantCode", CInt(Session("PlantCode")))
        Saveflag = Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, StrSql, params))

        If Saveflag = False Then
            ltlAlert.Text = "alert('保存有误，请重新操作！.')"
            Exit Sub
        End If
        'dd_proc.SelectedIndex = 0
        dd_group.SelectedIndex = 0
        txb_no.Text = ""

        BindData()
    End Sub

    Protected Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click

       Response.Redirect("wo_list.aspx?rm=" & Now)
    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click

        StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

        BindData()

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        If txb_date.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('入库日期 不能为空！');"
            Return
        Else
            Try
                Dim _dc As DateTime = Convert.ToDateTime(txb_date.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('入库日期 格式不正确！');"
                Return
            End Try
        End If

        If txb_cc.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('成本中心 不能为空！');"
            Return
        End If

        If txb_procprice.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('工序定额 不能为空！');"
            Return
        Else
            Try
                Dim _dc As Decimal = Convert.ToDecimal(txb_procprice.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('工序定额 只能是数字！');"
                Return
            End Try
        End If

        If txb_proc.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('工序 不能为空！');"
            Return
        End If

        If txb_qty.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('数量 不能为空！');"
            Return
        Else
            Try
                Dim _dc As Decimal = Convert.ToDecimal(txb_qty.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('数量 只能是数字！');"
                Return
            End Try
        End If

        'lbl_fixup取自tr_hist的计划外入库数量
        'lbl_reject取自wo_qty_rjct，即工单的报废数量
        '此判断在100系统中无法修复，说明QAD中的计划外入库数量本身就大于次品数
        If CDec(lbl_fixup.Text) > CDec(lbl_reject.Text) Then
            ltlAlert.Text = "alert('QAD计划外产品修复数量大于该工单的次品数量.')"
            Exit Sub
        End If

        Dim Decompare As Decimal = 0
        '汇总出已经汇报（wo_cost_detail）和即将汇报（wo_cost_detail_tmp）的数量和
        StrSql &= " SELECT ISNULL(SUM(ISNULL(c.Anum,0)),0) " _
               & "  FROM ("
        StrSql &= "         SELECT ISNULL(Sum(ISNULL(wocd_proc_qty,0)),0) as Anum " _
               & "          FROM (" _
               & "                  SELECT wocd_proc_qty " _
               & "                  FROM tcpc0..wo_cost_detail_tmp "
        StrSql &= "                 WHERE wocd_site='" & dd_site.SelectedValue & "' And wocd_nbr='" & txb_wonbr.Text & "' " _
               & "                      And wocd_type='W' And createdBy ='" & Session("uid") & "'" _
               & "                  Group By wocd_site,wocd_nbr,wocd_group_id,wocd_date,wocd_proc_qty " _
               & "               ) As a  "
        StrSql &= "         Union"
        StrSql &= "         SELECT ISNULL(Sum(ISNULL(wocd_proc_qty,0)),0) as Anum " _
               & "          FROM (" _
               & "                  SELECT wocd_proc_qty " _
               & "                  FROM wo_cost_detail "
        StrSql &= "                 WHERE wocd_site='" & dd_site.SelectedValue & "' And wocd_nbr='" & txb_wonbr.Text & "' And wocd_type='W' " _
               & "                  Group By wocd_site,wocd_nbr,wocd_group_id,wocd_date,wocd_proc_qty" _
               & "               ) As b " _
               & "        )  As c"
        Decompare = CDec(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql))

        If Decompare > CDec(lbl_fixup.Text) Then
            ltlAlert.Text = "alert('汇报总数据已超过修复数量！')"
            Exit Sub
        End If

        If Session("uRole") <> 1 Then
            StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & txb_cc.Text & "' and perm_userid='" & Session("uID") & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                ltlAlert.Text = "alert('无操作此成本中心的权限.')"
                Exit Sub
            End If
        End If

        If dd_group.SelectedIndex > 0 Then
            '组别内分配系数
            If Session("Plantcode") = 1 Then
                StrSql = "Select wod_user_id,wod_user_no,wod_user_name,isnull(wod_rate,1) from tcpc0.dbo.wo_group_detail where wod_group_id='" & dd_group.SelectedValue & "'"
            Else
                StrSql = " SELECT wo2_userID , wo2_userno,wo2_username,1 FROM wo2_group_detail WHERE wo2_groupID='" & dd_group.SelectedValue & "'"
            End If
            'StrSql = "Select wod_user_id,wod_user_no,wod_user_name,isnull(wod_rate,1) from tcpc0.dbo.wo_group_detail where wod_group_id='" & dd_group.SelectedValue & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While reader.Read
                Try
                    '不能重复添加
                    StrSql = "If Not Exists(Select wocd_userid From wo_cost_detail_tmp " _
                           & "              Where wocd_site = '" & dd_site.SelectedItem.Text & "' And wocd_nbr = '" & txb_wonbr.Text & "' And wocd_id = " & lbl_ID.Text _
                           & "                      And wocd_date = '" & txb_date.Text & "' And wocd_group_id = " & dd_group.SelectedValue _
                           & "                      And wocd_userid = " & reader(0).ToString().Trim() & " And Isnull(wocd_proc_id, 0) = 0 And wocd_proc_name = N'" & txb_proc.Text & "')" _
                           & " Begin " _
                           & "      INSERT INTO wo_cost_detail_tmp(wocd_userid, wocd_user_no, wocd_username, wocd_proc_id, wocd_proc_name, wocd_site, wocd_cc, createdDate, createdBy " _
                           & "                                  , wocd_part, wocd_price, wocd_pcost, wocd_group_id, wocd_type, wocd_appr, wocd_apprdate, wocd_nbr, wocd_date " _
                           & "                                  , wocd_rate, createdName, wocd_id, wocd_unitPrice) " _
                           & "      SELECT '" & reader(0) & "','" & reader(1) & "',N'" & reader(2) & "',0,N'" & txb_proc.Text & "','" & dd_site.SelectedValue & "','" & txb_cc.Text & "',getdate(),'" & Session("uID") & "'" _
                           & "              ,'" & lbl_part.Text.Trim & "', 0,'" & txb_procprice.Text & "','" & dd_group.SelectedValue & "','W','" & Session("uID") & "','" & txb_date.Text & "','" & txb_wonbr.Text.Trim() & "'" _
                           & "              ,'" & txb_date.Text & "','" & reader(3) & "',N'" & Session("uName") & "','" & lbl_ID.Text.Trim() & "'"

                    If txtUnitPrice.Text = "" Then
                        StrSql = StrSql & " , Null"
                    Else
                        StrSql = StrSql & " , '" & txtUnitPrice.Text & "'"
                    End If

                    StrSql &= "      FROM Users " _
                           & "       WHERE  userID='" & reader(0).ToString().Trim() & "' " _
                           & "              AND PlantCode='" & Session("PlantCode") & "' AND deleted=0 AND isactive=1 " _
                           & "              AND (leavedate IS NULL OR leavedate >= '" & txb_date.Text & "') " _
                           & " End "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, StrSql)
                Catch ex As Exception
                    ltlAlert.Text = "alert('增加时发生错误！请刷新后重新操作！')"
                    Exit Sub
                End Try
            End While
            reader.Close()
        End If

        '上面是按组别添加，这里往下是按个人添加
        If txb_no.Text <> "" Then
            'Modified by Simon to limite User's leave date
            StrSql = "Select userid,username from tcpc0.dbo.users where PlantCode='" & Session("PlantCode") & "'and userNo='" & txb_no.Text & "' and deleted=0 and isactive=1 and (leavedate is null or leavedate >= '" & txb_date.Text & "')"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim StrSql2 As String
                    Try
                        StrSql2 = "If Not Exists(Select wocd_userid From tcpc0.dbo.wo_cost_detail_tmp " _
                               & "              Where wocd_site = '" & dd_site.SelectedItem.Text & "' And wocd_nbr = '" & txb_wonbr.Text & "' And wocd_id = " & lbl_ID.Text _
                               & "                      And wocd_date = '" & txb_date.Text & "' And Isnull(wocd_group_id, 0) = 0 " _
                               & "                      And wocd_userid = " & .Rows(0).Item(0).ToString().Trim() & " And Isnull(wocd_proc_id, 0) = 0 And wocd_proc_name = N'" & txb_proc.Text & "')" _
                               & " Begin " _
                               & "      Insert into tcpc0.dbo.wo_cost_detail_tmp(wocd_userid,wocd_user_no,wocd_username,wocd_proc_id,wocd_proc_name,wocd_site,wocd_cc,createdDate,createdBy" _
                               & "                                             , wocd_part,wocd_price,wocd_pcost,wocd_type,wocd_appr,wocd_apprdate,wocd_nbr,wocd_date,createdName,wocd_id, wocd_unitPrice) " _
                               & "      Values('" & .Rows(0).Item(0).ToString().Trim() & "','" & txb_no.Text & "',N'" & .Rows(0).Item(1).ToString().Trim() & "',0,N'" & txb_proc.Text & "'" _
                               & "              , '" & dd_site.SelectedValue & "','" & txb_cc.Text & "',getdate(),'" & Session("uID") & "','" & lbl_part.Text.Trim & "',0,'" & txb_procprice.Text & "', 'W'" _
                               & "              , '" & Session("uID") & "', '" & txb_date.Text & "','" & txb_wonbr.Text.Trim() & "','" & txb_date.Text & "',N'" & Session("uName") & "','" & lbl_ID.Text.Trim() & "'"

                        If txtUnitPrice.Text = "" Then
                            StrSql2 &= " , Null"
                        Else
                            StrSql2 &= " , '" & txtUnitPrice.Text & "'"
                        End If

                        StrSql2 &= " End"

                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql2)
                    Catch
                        ltlAlert.Text = "alert('增加时发生错误！请刷新后重新操作！')"
                        Exit Sub
                    End Try
                Else
                    ds.Reset()
                    ltlAlert.Text = "alert('工号不存在。')"
                    Exit Sub
                End If
            End With
            ds.Reset()
        End If
        txb_no.Text = ""
        dd_group.SelectedIndex = 0

        BindData()
    End Sub


    Function GetDomain(ByVal str As String) As String
        StrSql = "SELECT realdomain FROM Domain_Mes WHERE qad_site ='" & str & "' "
        GetDomain = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, StrSql)
    End Function


    Sub WKReset()
        lbl_part.Text = ""
        lbl_desc.Text = ""
        lbl_reject.Text = ""
        lbl_fixup.Text = ""
        lbl_ID.Text = ""
    End Sub
End Class
