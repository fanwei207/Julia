Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Data
Imports WO2Group
Partial Class wo_cost_WKpackage
    Inherits BasePage
    Public chk As New adamClass
    Dim StrSql As String
    Dim ds As DataSet
    Dim nRet As Integer
    Dim item As ListItem
    Dim reader As SqlDataReader
    Public hr_salary As New WO2


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""
        If Not IsPostBack Then

            StrSql = "SELECT qad_site FROM Domain_Mes WHERE plantcode ='" & Session("PlantCode") & "' and qad_site IN ('1000','3000','2000')"
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
            While reader.Read
                item = New ListItem
                item.Value = reader(0)
                item.Text = reader(0)
                dd_site.Items.Add(item)
            End While
            reader.Close()


            dd_group.Items.Add(New ListItem("--", "0"))


            If Request("dd") = Nothing Then
                txb_date.Text = Format(Today, "yyyy-MM-dd")
            Else
                txb_date.Text = Request("dd")
            End If

            If Request("nbr") <> Nothing And Request("site") <> Nothing Then
                txb_wonbr.Text = Request("nbr")
                dd_site.SelectedValue = Request("site")
                getWOInfo()
            End If


            StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            btn_clear.Attributes.Add("onclick", "return confirm('确定要清除所有数据，重新开始汇报?');")


            BindData()
        End If
    End Sub

    Sub BindData()

        If Request("sch") <> Nothing Then  'sch isn't nothing means the status is searching.
            StrSql = " select cd.id, cd.wocd_site, cd.wocd_nbr, cd.wocd_id, cd.wocd_cc, cd.wocd_part, cd.wocd_user_no, cd.wocd_username, cd.wocd_proc_name, isnull(cd.wocd_proc_qty, 0) " _
                   & "      , isnull(cd.wocd_proc_adj,0), isnull(cd.wocd_price, 0) * isnull(cd.wocd_pcost, 0) * isnull(cd.wocd_proc_qty, 0) * 1.02 As wcost , cd.wocd_date" _
                   & "      , isnull(cd.createdName, '') ,isnull(cd.wocd_price, 0), isnull(cd.wocd_pcost, 0), cd.wocd_proc_adj, cd.wocd_unitPrice, cd.wocd_cost_unitPrice " _
                   & " from wo_cost_detail cd "
        Else
            StrSql = " select cd.id, cd.wocd_site, cd.wocd_nbr, cd.wocd_id, cd.wocd_cc, cd.wocd_part, cd.wocd_user_no, cd.wocd_username, cd.wocd_proc_name, isnull(cd.wocd_proc_qty,0)" _
                   & "      , isnull(cd.wocd_proc_adj,0), isnull(cd.wocd_price, 0) * isnull(cd.wocd_pcost, 0)* isnull(cd.wocd_proc_qty, 0) * 1.02 As wcost , cd.wocd_date" _
                   & "      , isnull(cd.createdName, ''), isnull(cd.wocd_price, 0), isnull(cd.wocd_pcost,0), cd.wocd_rate, cd.wocd_unitPrice, cd.wocd_cost_unitPrice " _
                   & " from tcpc0.dbo.wo_cost_detail_tmp cd "
        End If

        If Session("uRole") <> 1 Then
            StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
        End If
        StrSql &= " where cd.id is not null  and isnull(cd.wocd_type,'') ='B' "

        If Session("uRole") <> 1 Then
            StrSql &= "and cd.createdBy='" & Session("uID") & "' "
        End If

        If dd_site.SelectedIndex > 0 Then
            StrSql &= " and cd.wocd_site ='" & dd_site.SelectedValue & "' "
        End If
        If txb_wonbr.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_nbr ='" & txb_wonbr.Text.Trim() & "' "
        End If

        StrSql &= " and cd.wocd_cc ='" & lbl_cc.Text.Trim() & "' "

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
        dtl.Columns.Add(New DataColumn("proc_cost", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_price", System.Type.GetType("System.String"))) 'wocd_price
        dtl.Columns.Add(New DataColumn("wo_date_comp", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_cc", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_ID", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_nbr", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("wo_created", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_pcost", System.Type.GetType("System.String"))) 'wocd_pcost 最终的分配系数
        dtl.Columns.Add(New DataColumn("proc_rate", System.Type.GetType("System.String"))) '手动输入的分配系数

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
                    drow.Item("proc_cc") = .Rows(i).Item(4).ToString().Trim()
                    drow.Item("user_id") = .Rows(i).Item(6).ToString().Trim()
                    drow.Item("user_name") = .Rows(i).Item(7).ToString().Trim()
                    drow.Item("proc_name") = .Rows(i).Item(8).ToString().Trim()
                    drow.Item("proc_qty") = Format(.Rows(i).Item(9), "##0.#####")
                    drow.Item("proc_adj") = Format(.Rows(i).Item(10), "##0.#####")
                    drow.Item("proc_cost") = Format(.Rows(i).Item(11), "##0.#####") '汇报总工时
                    drow.Item("proc_price") = Format(.Rows(i).Item(14), "##0.#####")
                    If Not IsDBNull(.Rows(i).Item(12)) Then
                        drow.Item("wo_date_comp") = Format(.Rows(i).Item(12), "yy-MM-dd")
                    End If
                    drow.Item("wo_created") = .Rows(i).Item(13).ToString().Trim()
                    drow.Item("proc_pcost") = Format(.Rows(i).Item(15), "##0.#####")
                    drow.Item("proc_ID") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("proc_rate") = .Rows(i).Item(16) 'Format(.Rows(i).Item(16), "##0.#####")

                    drow.Item("proc_unitPrice") = .Rows(i).Item("wocd_unitPrice").ToString() 'Format(.Rows(i).Item("wocd_unitPrice"), "##0.#####")
                    drow.Item("proc_cost_unitPrice") = .Rows(i).Item("wocd_cost_unitPrice").ToString()

                    dtl.Rows.Add(drow)
                Next
            End If
        End With
        ds.Reset()

        Dim dvw As DataView
        dvw = New DataView(dtl)

        Datagrid1.DataSource = dvw
        Datagrid1.DataBind()

        If Request("sch") <> Nothing Then
            Datagrid1.Columns(8).Visible = False
            Datagrid1.Columns(17).Visible = False

            Datagrid1.Columns(10).Visible = True
            Datagrid1.Columns(12).Visible = True
        End If

    End Sub
    Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
        If e.CommandName.CompareTo("proc_del") = 0 Then
            If e.Item.Cells(18).Text.CompareTo(Session("uName")) <> 0 Then
                ltlAlert.Text = "alert('只有创建者能删除此记录！.')"
                Exit Sub
            End If

            If Request("sch") <> Nothing Then
                Dim str As String
                Dim blflag As Boolean = False

                StrSql = "SELECT id,wocd_site, wocd_nbr, wocd_group_id,CONVERT(varchar(10), wocd_date, 120) As wdate,wocd_proc_qty  FROM wo_cost_detail Where id='" & e.Item.Cells(1).Text & "'"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                While reader.Read

                    str = "sp_wo_UpdatePackage"
                    Dim params(7) As SqlParameter
                    params(0) = New SqlParameter("wsite", reader(1))
                    params(1) = New SqlParameter("wnbr", reader(2))
                    params(2) = New SqlParameter("wgroup", reader(3))
                    params(3) = New SqlParameter("wdate", reader(4))
                    params(4) = New SqlParameter("wqty", reader(5))
                    params(5) = New SqlParameter("id", reader(0))
                    params(6) = New SqlParameter("plantCode", CInt(Session("PlantCode")))

                    blflag = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, str, params)
                End While
                reader.Close()
                ' StrSql = "Delete From wo_cost_detail where  id='" & reader(0) & "' "
                If blflag = True Then
                    ltlAlert.Text = "alert('删除有误，请重新操作！.')"
                    Exit Sub
                End If

            Else
                StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where id='" & e.Item.Cells(1).Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            End If
        ElseIf e.CommandName.CompareTo("proc_save") = 0 Then
            Dim str1 As String = CType(e.Item.Cells(8).FindControl("txb_qty"), TextBox).Text
            StrSql = "update tcpc0.dbo.wo_cost_detail_tmp set wocd_rate = '" & str1 & "'  where id='" & e.Item.Cells(1).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

        End If
        BindData()
    End Sub


    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click

        If txb_date.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('完工日期 不能为空！');"
            Return
        Else
            Try
                Dim _dc As DateTime = Convert.ToDateTime(txb_date.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('完工日期 格式不正确！');"
                Return
            End Try
        End If

        If txb_wonbr.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('加工单号 不能为空！');"
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

        If Session("uRole") <> 1 Then
            StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & lbl_cc.Text & "' and perm_userid='" & Session("uID") & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                ltlAlert.Text = "alert('无操作此成本中心的权限.')"
                Exit Sub
            End If
        End If

        'lbl_comp值取自于tr_hist的计划外入库数量
        'lbl_cost值取自于wo_qty_rjct，即工单的报废数量
        If CDec(lbl_cost.Text) < CDec(lbl_comp.Text) Then
            ltlAlert.Text = "alert('QAD计划外产品修复数量大于该工单的次品数量.')"
            Exit Sub
        End If

        If dd_group.SelectedIndex > 0 Then
            StrSql = "SELECT wo2_userID , wo2_userno,wo2_username,1,ISNULL(wo2_sop_rate,0) FROM wo2_group_detail WHERE wo2_groupID ='" & dd_group.SelectedValue & "' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While reader.Read
                Try
                    StrSql = "If Not Exists(Select wocd_userid From wo_cost_detail_tmp " _
                           & "              Where wocd_site = '" & dd_site.SelectedItem.Text & "' And wocd_nbr = '" & txb_wonbr.Text & "' And wocd_id = " & lbl_ID.Text _
                           & "                      And wocd_date = '" & txb_date.Text & "' And wocd_group_id = " & dd_group.SelectedValue _
                           & "                      And wocd_userid = " & reader(0).ToString().Trim() & " And Isnull(wocd_proc_id, 0) = 0 And wocd_proc_name = N'" & txb_proc.Text & "')" _
                           & " Begin " _
                           & "      INSERT INTO wo_cost_detail_tmp(wocd_userid, wocd_user_no, wocd_username, wocd_proc_id, wocd_proc_name, wocd_site, wocd_cc, wocd_nbr, createdDate, createdBy" _
                           & "                                  , wocd_price, wocd_type, wocd_group_id, wocd_date, wocd_rate, createdName, wocd_unplan, wocd_line, wocd_id, wocd_unitPrice) " _
                           & "      SELECT '" & reader(0) & "', '" & reader(1) & "', N'" & reader(2) & "', '" & lbl_type.Text & "', N'" & txb_proc.Text & "', '" & dd_site.SelectedValue & "'" _
                           & "              , '" & lbl_cc.Text & "', '" & txb_wonbr.Text & "' ,getdate(), '" & Session("uID") & "', '" & txb_procprice.Text & "','B', '" & dd_group.SelectedValue & "'" _
                           & "              , '" & txb_date.Text & "','" & reader(3) & "',N'" & Session("uName") & "', '" & reader(4).ToString() & "', Null, '" & lbl_ID.Text.Trim() & "'"

                    If txtUnitPrice.Text = "" Then
                    StrSql = StrSql & " , Null"
                            Else
                    StrSql = StrSql & " , '" & txtUnitPrice.Text & "'"
                            End If
                    StrSql &= "      FROM Users WHERE  userID='" & reader(0).ToString().Trim() & "' AND PlantCode='" & Session("PlantCode") & "' AND deleted=0 AND isactive=1 AND (leavedate IS NULL OR leavedate >= '" & txb_date.Text & "') " _
                           & " End"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, StrSql)

                Catch ex As Exception
            ltlAlert.Text = "alert('增加时发生错误！请刷新后重新操作！')"
            Exit Sub
        End Try
            End While
        reader.Close()
        End If

        If txb_no.Text <> "" Then
            'Modified by Simon to limite User's leave date
            StrSql = "Select userid,username from tcpc0.dbo.users where PlantCode='" & Session("PlantCode") & "'and userNo='" & txb_no.Text & "' and deleted=0 and isactive=1 and (leavedate is null or leavedate >= '" & txb_date.Text & "')"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Try
                        Dim StrSql2 As String
                        StrSql2 = "If Not Exists(Select wocd_userid From tcpc0.dbo.wo_cost_detail_tmp " _
                               & "              Where wocd_site = '" & dd_site.SelectedItem.Text & "' And wocd_nbr = '" & txb_wonbr.Text & "' And wocd_id = " & lbl_ID.Text _
                               & "                      And wocd_date = '" & txb_date.Text & "' And Isnull(wocd_group_id, 0) = 0 " _
                               & "                      And wocd_userid = " & .Rows(0).Item(0).ToString().Trim() & " And Isnull(wocd_proc_id, 0) = " & lbl_type.Text & " And wocd_proc_name = N'" & txb_proc.Text & "')" _
                               & " Begin " _
                               & "      Insert into tcpc0.dbo.wo_cost_detail_tmp(wocd_userid, wocd_user_no, wocd_username, wocd_proc_id, wocd_proc_name, wocd_site, wocd_cc, wocd_nbr, createdDate, createdBy " _
                               & "                                             , wocd_price, wocd_type, wocd_date, createdName, wocd_unplan, wocd_line, wocd_id, wocd_unitPrice) " _
                               & "      Values( '" & .Rows(0).Item(0).ToString().Trim() & "', '" & txb_no.Text & "', N'" & .Rows(0).Item(1).ToString().Trim() & "', '" & lbl_type.Text & "', N'" & txb_proc.Text & "'" _
                               & "              , '" & dd_site.SelectedValue & "', '" & lbl_cc.Text & "', '" & txb_wonbr.Text & "', getdate(), '" & Session("uID") & "', '" & txb_procprice.Text & "', 'B'" _
                               & "              , '" & txb_date.Text & "', N'" & Session("uName") & "', '" & dropPostion.SelectedValue & "', Null, '" & lbl_ID.Text.Trim() & "'"

                        If txtUnitPrice.Text = "" Then
                            StrSql2 &= " , Null"
                        Else
                            StrSql2 &= " , '" & txtUnitPrice.Text & "'"
                        End If
                        StrSql2 &= " End"

                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql2)
                    Catch ex As Exception
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
        dropPostion.SelectedIndex = 0

        BindData()
    End Sub

    Function getWOInfo() As Boolean
        Dim dm As String = ""
        dm = GetDomain(dd_site.SelectedValue)

        If txb_wonbr.Text.Trim.Length <= 0 Then
            WKReset()
            Exit Function
        Else
            'judge the work order whether fit condition
            StrSql = " SELECT pt_part,pt_desc1 + pt_desc2,wo_qty_rjct,ISNULL(t.fixup,0),wo_lot,ISNULL(r.wo2_ro_run,0),1.02,wo_qty_rjct,wo_routing,ISNULL(r.wo2_ro_id,0),wo_flr_cc " _
                   & " FROM QAD_data..wo_mstr "
            StrSql &= " INNER JOIN (SELECT tr_domain, tr_part, Ltrim(Rtrim(tr_serial)) as torder,SUM(tr_qty_loc) as fixup FROM Qad_data..tr_hist INNER JOIN Qad_data..trgl_det ON trgl_domain =tr_domain AND trgl_trnbr=tr_trnbr WHERE Ltrim(Rtrim(tr_serial))='" & txb_wonbr.Text.Trim() & "' And tr_domain='" & dm & "' AND  trgl_cr_acct='50010070' And tr_type='RCT-UNP' GROUP BY tr_domain,tr_serial,tr_part) t  ON t.tr_domain =wo_domain and torder = wo_nbr  and tr_part = wo_part "
            StrSql &= " LEFT OUTER JOIN QAD_data..pt_mstr ON pt_domain=wo_domain AND wo_part=pt_part "
            StrSql &= " LEFT OUTER JOIN tcpc0.dbo.wo2_routing r on r.wo2_ro_routing = wo_routing and r.wo2_mop_proc=1090 "
            StrSql &= " WHERE wo_domain='" & dm & "' AND wo_nbr='" & txb_wonbr.Text.Trim() & "' and wo_qty_rjct>0  and ISNULL(wo_type,'')='' "
            StrSql &= " And wo_routing like '1%0'  and substring(wo_routing,3,1)<> '7' And (  wo_site in('1000','3000')   "
            StrSql &= " or (wo_site='2000' And Substring(wo_routing,1,2) IN ('13','14') )  )"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            If (reader.HasRows = True) Then
                While reader.Read
                    lbl_part.Text = reader(0).ToString()
                    lbl_desc.Text = reader(1).ToString()
                    lbl_comp.Text = Format(reader(3), "##0.#####")
                    lbl_ID.Text = reader(4).ToString()
                    lbl_hour.Text = Format(reader(5) * reader(3) * reader(6), "##0.#####")
                    lbl_cost.Text = reader(7)
                    lbl_rate.Text = reader(8)
                    lbl_type.Text = reader(9)
                    lbl_cc.Text = reader(10)
                End While
            Else
                ltlAlert.Text = "alert('加工单不存在或加工单不符合条件！.')"
                WKReset()
                reader.Close()
                Exit Function
            End If
            reader.Close()
        End If


        ' Group 
        If lbl_cc1.Text.Length <= 0 Or lbl_cc.Text <> lbl_cc1.Text Then
            lbl_cc1.Text = lbl_cc.Text


            Dim ls As ListItem
            dd_group.Items.Clear()

            If Session("Plantcode") = 1 Then
                ls = New ListItem
                ls.Value = 0
                ls.Text = "--"
                dd_group.Items.Add(ls)

                StrSql = "select wog_id,wog_name from tcpc0.dbo.wo_group where deletedBy is null and wog_cc='" & lbl_cc.Text & "'  order by wog_name"
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
        End If

        ' Workprocedure time
        StrSql = " SELECT wo2_ro_run = ISNULL(wo2_ro_run,0), wo2_mop_procname " _
               & "      , wo2_price_up = Case " & Session("PlantCode") & " When 1 Then Isnull(wo2_price_szx, 0) When 2 Then Isnull(wo2_price_zql, 0) When 5 Then Isnull(wo2_price_yql, 0) End" _
               & " FROM tcpc0..wo2_routing " _
               & " WHERE wo2_ro_routing='" & lbl_rate.Text & "' And wo2_mop_proc = 1090 "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While reader.Read()
            txb_proc.Text = reader("wo2_mop_procname")
            txb_procprice.Text = reader("wo2_ro_run")
            txtUnitPrice.Text = reader("wo2_price_up")
        End While
        reader.Close()

        dropPostionBind()

        If Request("sch") <> Nothing Then
            dd_site.Enabled = False
            txb_date.Enabled = False
            txb_wonbr.Enabled = False

            dd_group.Enabled = False
            txb_no.Enabled = False
            txb_qty.Enabled = False
            btn_add.Visible = False
            btn_save.Visible = False
            btn_assign.Visible = False
            btn_clear.Visible = False
            btn_woload.Visible = False
            dropPostion.Enabled = False
            btn_back.Visible = True
        End If
    End Function

    Protected Sub btn_woload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_woload.Click

        getWOInfo()

        BindData()

    End Sub

    Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
        Dim strMes As String
        strMes = "工号：" & e.Item.Cells(2).Text & "  姓名：" & e.Item.Cells(3).Text

        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing ButtonColumn
                myDeleteButton = e.Item.Cells(18)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除  " & strMes & "  吗?');")
            Case ListItemType.AlternatingItem
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing your ButtonColumn
                myDeleteButton = e.Item.Cells(18)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除  " & strMes & "  吗?');")
            Case ListItemType.EditItem
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing your ButtonColumn
                myDeleteButton = e.Item.Cells(18)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除  " & strMes & "  吗?');")
                CType(e.Item.Cells(8).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(50)
                CType(e.Item.Cells(8).Controls(0), TextBox).Height = System.Web.UI.WebControls.Unit.Pixel(18)

        End Select



    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        txb_wonbr.Text = ""

        lbl_cc.Text = ""

        lbl_cost.Text = ""

        lbl_price.Text = ""
        lbl_rate.Text = ""
        lbl_desc.Text = ""


        StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)


        txb_wonbr.Focus()
        BindData()

    End Sub

    Protected Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
        If (Request("parent") = "358000160") Then
            Response.Redirect("wo_list_up.aspx?rm=" & Now)
        Else
            Response.Redirect("wo_list.aspx?rm=" & Now)
        End If
    End Sub

    Protected Sub btn_assign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_assign.Click

        If txb_date.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('完工日期 不能为空！');"
            Return
        Else
            Try
                Dim _dc As DateTime = Convert.ToDateTime(txb_date.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('完工日期 格式不正确！');"
                Return
            End Try
        End If

        If txb_wonbr.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('加工单号 不能为空！');"
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

        'lbl_comp值取自于tr_hist的计划外入库数量
        'lbl_cost值取自于wo_qty_rjct，即工单的报废数量        'judge qty compare total
        If CDec(lbl_comp.Text.Trim()) > CDec(lbl_cost.Text) Then
            ltlAlert.Text = "alert('此张加工单不符合条件！')"
            Exit Sub
        End If

        Dim decReport As Decimal = 0
        decReport = reportTotal()

        If decReport + CDec(txb_qty.Text) > CDec(lbl_comp.Text.Trim()) Then
            ltlAlert.Text = "alert('输入数量已经超过最大汇报数！')"
            Exit Sub
        End If

        Dim i As Integer = 0
        For i = 0 To Datagrid1.Items.Count - 1
            Dim str1 As String = CType(Datagrid1.Items(i).Cells(8).FindControl("txb_qty"), TextBox).Text
            StrSql = " update tcpc0.dbo.wo_cost_detail_tmp " _
                   & " set wocd_rate= '" & str1 & "', wocd_proc_qty = '" & txb_qty.Text & "'" _
                   & " where id='" & Datagrid1.Items(i).Cells(1).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
        Next

        txb_qty.Text = ""

        BindData()

    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click

        Dim saveflag As Boolean = False

        StrSql = " SELECT Count(*) FROM wo_cost_detail_tmp where wocd_site ='" & dd_site.SelectedValue & "' And wocd_ID ='" & lbl_ID.Text & "' And wocd_type='B'  And ISNULL(wocd_proc_qty,0) =0 "
        If Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, StrSql)) > 0 Then
            ltlAlert.Text = "alert('加工单数量没有分配！')"
            Exit Sub
        End If

        StrSql = "SELECT Count(*) FROM wo_cost_detail WHERE wocd_site ='" & dd_site.SelectedValue & "' And wocd_ID ='" & lbl_ID.Text & "' And wocd_type='B'  "
        If Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, StrSql)) > 0 Then
            ltlAlert.Text = "alert('加工单已经汇报，请删除已汇报信息再操作！')"
            Exit Sub
        End If

        StrSql = "sp_wo_UpdatePackageRate"
        Dim params(4) As SqlParameter
        params(0) = New SqlParameter("uid", CInt(Session("uID")))
        params(1) = New SqlParameter("plantCode", CInt(Session("PlantCode")))
        params(2) = New SqlParameter("wosite", dd_site.SelectedValue)
        params(3) = New SqlParameter("woid", lbl_ID.Text)
        saveflag = Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, StrSql, params))

        If saveflag = False Then
            ltlAlert.Text = "alert('保存有误，请重新操作！.')"
            Exit Sub
        End If

        dd_group.SelectedIndex = 0
        dropPostion.SelectedValue = 0
        txb_no.Text = ""
        txb_qty.Text = ""

        BindData()
    End Sub

    Function GetDomain(ByVal str As String) As String
        StrSql = "SELECT realdomain FROM Domain_Mes WHERE qad_site ='" & str & "' "
        GetDomain = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, StrSql)
    End Function


    Sub WKReset()
        lbl_cc.Text = ""
        lbl_comp.Text = ""
        lbl_cost.Text = ""
        lbl_price.Text = ""
        lbl_rate.Text = ""
        lbl_desc.Text = ""
        lbl_type.Text = ""
        lbl_hour.Text = ""
        lbl_ID.Text = ""
        lbl_cc1.Text = ""



        While dd_group.Items.Count > 0
            dd_group.Items.Clear()
        End While

    End Sub


    Sub dropPostionBind()
        While dropPostion.Items.Count > 0
            dropPostion.Items.Clear()
        End While

        dropPostion.DataSource = hr_salary.SelectSOPInfo(1090)
        dropPostion.DataBind()

    End Sub

    Function reportTotal() As Decimal

        StrSql = " SELECT Sum(ISNULL(wocd_proc_qty,0)) FROM (SELECT wocd_proc_qty FROM (SELECT wocd_proc_qty,wocd_site,wocd_nbr,wocd_group_id,wocd_date  FROM wo_cost_detail WHERE wocd_site='" & dd_site.SelectedValue & "' And wocd_nbr='" & txb_wonbr.Text & "' And wocd_type='B' "
        StrSql &= "            Union  SELECT wocd_proc_qty,wocd_site,wocd_nbr,wocd_group_id,wocd_date  FROM tcpc0..wo_cost_detail_tmp WHERE wocd_site='" & dd_site.SelectedValue & "' And wocd_nbr='" & txb_wonbr.Text & "' And wocd_type='B') As a1 Group By wocd_site,wocd_nbr,wocd_group_id,wocd_date,wocd_proc_qty) As B "
        Return SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
    End Function

End Class
