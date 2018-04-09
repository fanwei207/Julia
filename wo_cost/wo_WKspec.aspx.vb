Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Data
Imports WO2Group

Partial Class wo_cost_wo_WKspec
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
            dd_line.Items.Add(New ListItem("--", "0"))

            If Request("nbr") <> Nothing And Request("site") <> Nothing Then
                txb_wonbr.Text = Request("nbr")
                dd_site.SelectedValue = Request("site")
                getWOInfo()
            End If

            If Request("dd") = Nothing Then
                txb_date.Text = Format(Today, "yyyy-MM-dd")
            Else
                txb_date.Text = Request("dd")
            End If

            StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            btn_clear.Attributes.Add("onclick", "return confirm('确定要清除所有数据，重新开始汇报?');")

            BindData()
        End If
    End Sub

    Sub BindData()
        StrSql = "  select cd.id,cd.wocd_site,cd.wocd_nbr,cd.wocd_id,cd.wocd_cc, cd.wocd_part,cd.wocd_user_no,cd.wocd_username,cd.wocd_proc_name, isnull(cd.wocd_proc_qty, 0)" _
               & "      , isnull(cd.wocd_proc_adj,0),isnull(cd.wocd_cost,0) , cd.wocd_date, isnull(cd.createdName,'') ,isnull(cd.wocd_price,0)" _
               & "      , cd.wocd_duty, wocd_cost_unitPrice = Isnull(wocd_cost_unitPrice, 0), wocd_unitPrice = Isnull(wocd_unitPrice, 0)" _
               & "  from tcpc0.dbo.wo_cost_detail_tmp cd "
        If Session("uRole") <> 1 Then
            StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
        End If
        StrSql &= " where cd.id is not null and cd.createdBy='" & Session("uID") & "' and isnull(cd.wocd_type,'')='A' "

        If dd_site.SelectedIndex > 0 Then
            StrSql &= " and cd.wocd_site ='" & dd_site.SelectedValue & "' "
        End If
        If txb_wonbr.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_nbr ='" & txb_wonbr.Text.Trim() & "' "
        End If
        If lbl_cc.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_cc ='" & lbl_cc.Text.Trim() & "' "
        End If
        If txb_proc.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_proc_name =N'" & chk.sqlEncode(txb_proc.Text.Trim()) & "' "
        End If

        StrSql &= " order by cd.createdDate "


        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

        Label1.Text = "计:" & ds.Tables(0).Rows.Count & "条"

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("sid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("user_name", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_name", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_qty", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_adj", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_price1", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_price2", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("wo_date_comp", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_cc", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_duty", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_nbr", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("wo_created", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_pri", System.Type.GetType("System.String")))

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
                    drow.Item("proc_price1") = Format(.Rows(i).Item(11), "##0.#####")
                    drow.Item("proc_price2") = Format(.Rows(i).Item(14), "##0.#####")
                    If Not IsDBNull(.Rows(i).Item(12)) Then
                        drow.Item("wo_date_comp") = Format(.Rows(i).Item(12), "yy-MM-dd")
                    End If
                    drow.Item("wo_created") = .Rows(i).Item(13).ToString().Trim()
                    drow.Item("proc_pri") = Format(.Rows(i).Item(15), "##0.#####")
                    drow.Item("proc_duty") = .Rows(i).Item(16).ToString().Trim()

                    drow.Item("proc_unitPrice") = Format(.Rows(i).Item("wocd_unitPrice"), "##0.#####")
                    drow.Item("proc_cost_unitPrice") = Format(.Rows(i).Item("wocd_cost_unitPrice"), "##0.#####")

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
            StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where id='" & e.Item.Cells(1).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            BindData()
        ElseIf e.CommandName.CompareTo("proc_save") = 0 Then
            Dim str1 As String = CType(e.Item.Cells(8).FindControl("txb_qty"), TextBox).Text
            StrSql = " update tcpc0.dbo.wo_cost_detail_tmp " _
                   & " set wocd_proc_qty = '" & str1 & "', wocd_cost=" & CDec(str1) & " * wocd_price " _
                   & "      , wocd_cost_unitPrice = " & CDec(str1) & " * wocd_unitPrice " _
                   & " where id='" & e.Item.Cells(1).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            BindData()
        End If
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

        If txb_comp.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('完工入库总数 不能为空！');"
            Return
        Else
            Try
                Dim _dc As Decimal = Convert.ToDecimal(txb_comp.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('完工入库总数 只能是数字！');"
                Return
            End Try
        End If

        If txb_hour.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('总工时 不能为空！');"
            Return
        Else
            Try
                Dim _dc As Decimal = Convert.ToDecimal(txb_hour.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('总工时 只能是数字！');"
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

        StrSql = " select woo_id from wo_order where woo_site='" & dd_site.SelectedValue & "' and woo_nbr='" & txb_wonbr.Text & "' and deletedBy is null and (approvedBy is not null or acctApprBy is not null)"
        If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
            ltlAlert.Text = "alert('此计划外用工单已关闭,不能进行汇报.')"
            Exit Sub
        End If

        If txtUnitPrice.Text <> "" Then
            If Not IsNumeric(txtUnitPrice.Text) Then
                ltlAlert.Text = "alert('工序单价必须是数字！')"
                Exit Sub
            End If
        End If

        '//If the work order has approved , it don't be inputed.
        Dim intAdjust As Integer = 0
        StrSql = "SELECT count(*) FROM wo_cost_detail Where wocd_nbr ='" & txb_wonbr.Text & "' AND wocd_site ='" & dd_site.SelectedValue & "' AND wocd_apprdate IS NOT NULL "
        intAdjust = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
        If intAdjust > 0 Then
            ltlAlert.Text = "alert('此计划外用工单已关闭,不能进行汇报.')"
            Exit Sub
        End If

        '//--------------------------------------------------


        If Session("uRole") <> 1 Then
            StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & lbl_cc.Text & "' and perm_userid='" & Session("uID") & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                ltlAlert.Text = "alert('无操作此成本中心的权限.')"
                Exit Sub
            End If
        End If

        Dim strduty As String
        StrSql = " select woo_cc_duty from wo_order where woo_site='" & dd_site.SelectedValue & "' and woo_nbr='" & txb_wonbr.Text & "' and deletedBy is null and (approvedBy is not null or acctApprBy is not null)"
        strduty = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

        If dd_group.SelectedIndex > 0 Then
            'If Session("Plantcode") = 1 Then
            '    StrSql = "Select wod_user_id,wod_user_no,wod_user_name,isnull(wod_rate,1) from tcpc0.dbo.wo_group_detail where wod_group_id='" & dd_group.SelectedValue & "'"
            'Else
            StrSql = "SELECT wo2_userID , wo2_userno,wo2_username,1 FROM wo2_group_detail WHERE wo2_groupID ='" & dd_group.SelectedValue & "' "
            'End If
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While reader.Read
                Try
                    StrSql = "If Not Exists(Select wocd_userid From wo_cost_detail_tmp " _
                           & "              Where wocd_site = '" & dd_site.SelectedItem.Text & "' And wocd_nbr = '" & txb_wonbr.Text & "'" _
                           & "                      And wocd_date = '" & txb_date.Text & "' And wocd_group_id = " & dd_group.SelectedValue _
                           & "                      And wocd_userid = " & reader(0).ToString().Trim() & " And Isnull(wocd_proc_id, 0) = 0 And wocd_proc_name = N'" & txb_proc.Text & "')" _
                           & " Begin " _
                           & "      INSERT INTO wo_cost_detail_tmp(wocd_userid,wocd_user_no,wocd_username,wocd_proc_id,wocd_proc_name,wocd_site,wocd_cc,wocd_nbr, createdDate " _
                           & "              , createdBy,wocd_price,wocd_type,wocd_group_id,wocd_date,wocd_rate,createdName, wocd_duty, wocd_unitPrice, wocd_line) " _
                           & "      SELECT '" & reader(0) & "','" & reader(1) & "',N'" & reader(2) & "',0,N'" & chk.sqlEncode(txb_proc.Text) & "','" & dd_site.SelectedValue & "'" _
                           & "              , '" & lbl_cc.Text & "','" & txb_wonbr.Text & "',getdate(),'" & Session("uID") & "','" & txb_procprice.Text & "','" & lbl_type.Text & "','" & dd_group.SelectedValue & "'" _
                           & "              , '" & txb_date.Text & "','" & reader(3) & "',N'" & Session("uName") & "', '" & strduty & "'"

                    If txtUnitPrice.Text = "" Then
                        StrSql = StrSql & " , Null"
                    Else
                        StrSql = StrSql & " , '" & txtUnitPrice.Text & "'"
                    End If

                    If dd_line.SelectedValue > 0 Then
                        StrSql &= " , N'" & dd_line.SelectedItem.Text & "' "
                    Else
                        StrSql &= " , Null"
                    End If
                    StrSql &= "     FROM Users WHERE  userID='" & reader(0).ToString().Trim() & "' AND PlantCode='" & Session("PlantCode") & "' AND deleted=0 AND isactive=1 AND (leavedate IS NULL OR leavedate >= '" & txb_date.Text & "') " _
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
                    Dim StrSql2 As String
                    Try
                        StrSql2 = "If Not Exists(Select wocd_userid From tcpc0.dbo.wo_cost_detail_tmp " _
                               & "              Where wocd_site = '" & dd_site.SelectedItem.Text & "' And wocd_nbr = '" & txb_wonbr.Text & "'" _
                               & "                      And wocd_date = '" & txb_date.Text & "' And Isnull(wocd_group_id, 0) = 0 " _
                               & "                      And wocd_userid = " & .Rows(0).Item(0).ToString().Trim() & " And Isnull(wocd_proc_id, 0) = 0 And wocd_proc_name = N'" & txb_proc.Text & "')" _
                               & " Begin " _
                               & "      Insert into tcpc0.dbo.wo_cost_detail_tmp(wocd_userid,wocd_user_no,wocd_username,wocd_proc_id,wocd_proc_name,wocd_site,wocd_cc,wocd_nbr,createdDate " _
                               & "              ,createdBy,wocd_price,wocd_type,wocd_date,createdName, wocd_duty, wocd_unitPrice, wocd_line) " _
                               & "      Values('" & .Rows(0).Item(0).ToString().Trim() & "','" & txb_no.Text & "',N'" & .Rows(0).Item(1).ToString().Trim() & "', 0, N'" & chk.sqlEncode(txb_proc.Text) & "'" _
                               & "              , '" & dd_site.SelectedValue & "','" & lbl_cc.Text & "','" & txb_wonbr.Text & "',getdate(),'" & Session("uID") & "','" & txb_procprice.Text & "','" & lbl_type.Text & "'" _
                               & "              , '" & txb_date.Text & "',N'" & Session("uName") & "','" & strduty & "'"

                        If txtUnitPrice.Text = "" Then
                            StrSql2 &= " , Null"
                        Else
                            StrSql2 &= " , '" & txtUnitPrice.Text & "'"
                        End If

                        If dd_line.SelectedValue > 0 Then
                            StrSql2 &= " , N'" & dd_line.SelectedItem.Text & "')"
                        Else
                            StrSql2 &= " , Null)"
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

    Function getWOInfo() As Boolean
        If txb_wonbr.Text.Trim.Length <= 0 Then
            lbl_cc.Text = ""
            txb_comp.Text = ""
            lbl_cost.Text = ""
            lbl_price.Text = ""
            lbl_rate.Text = ""
            lbl_desc.Text = ""
            lbl_type.Text = ""
            txb_hour.Text = ""


            While dd_group.Items.Count > 0
                dd_group.Items.Clear()
            End While

            While dd_line.Items.Count > 0
                dd_line.Items.Clear()
            End While

            Exit Function
        End If

        StrSql = "Select woo_cc_to,isnull(woo_qty_comp,0),woo_req,woo_type,isnull(woo_hours,0)  from wo_order where woo_site='" & dd_site.SelectedValue & "' and woo_nbr='" & txb_wonbr.Text & "' and deletedBy is null"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                lbl_cc.Text = .Rows(0).Item(0).ToString().Trim()
                txb_comp.Text = Format(.Rows(0).Item(1), "##0.#####")
                lbl_desc.Text = .Rows(0).Item(2).ToString().Trim()
                lbl_cost.Text = "0"
                lbl_price.Text = "0"
                lbl_rate.Text = "1"
                lbl_type.Text = .Rows(0).Item(3).ToString().Trim()
                txb_hour.Text = Format(.Rows(0).Item(4), "##0.##")

            Else
                ltlAlert.Text = "alert('此计划外用工单不存在.')"
                Exit Function
            End If
        End With
        ds.Reset()


        If lbl_cc1.Text.Length <= 0 Or lbl_cc.Text <> lbl_cc1.Text Then
            lbl_cc1.Text = lbl_cc.Text


            While dd_group.Items.Count > 0
                dd_group.Items.RemoveAt(0)
            End While

            Dim ls As ListItem

            Dim reader As SqlDataReader

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            dd_group.Items.Add(ls)

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
            'End If

            While dd_line.Items.Count > 0
                dd_line.Items.RemoveAt(0)
            End While

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            dd_line.Items.Add(ls)
            StrSql = "select wol_id,wol_name from tcpc0.dbo.wo_line where deletedBy is null and wol_cc='" & lbl_cc.Text & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim

                dd_line.Items.Add(ls)
            End While
            reader.Close()

        End If
    End Function

    Protected Sub btn_woload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_woload.Click

        getWOInfo()

        BindData()

    End Sub

    Protected Sub btn_rct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_rct.Click

        If txb_wonbr.Text.Length = 0 Then
            ltlAlert.Text = "alert('加工单 不能为空！')"
            Exit Sub
        End If

        If Not Me.IsNumber(txb_comp.Text) Then
            ltlAlert.Text = "alert('完工入库总数 不能为空，且必须是数字！')"
            Exit Sub
        End If

        If Not Me.IsNumber(txb_hour.Text) Then
            ltlAlert.Text = "alert('总工时 不能为空，且必须是数字！')"
            Exit Sub
        End If

        StrSql = "Update wo_order set woo_qty_comp='" & txb_comp.Text & "',woo_hours='" & txb_hour.Text & "' where woo_site='" & dd_site.SelectedValue & "' and woo_nbr='" & txb_wonbr.Text & "' and deletedBy is null and approvedBy is null and acctApprBy is null"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

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
        Response.Redirect("wo_list.aspx?rm=" & Now)
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

        If txb_comp.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('完工入库总数 不能为空！');"
            Return
        Else
            Try
                Dim _dc As Decimal = Convert.ToDecimal(txb_comp.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('完工入库总数 只能是数字！');"
                Return
            End Try
        End If

        If txb_hour.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('总工时 不能为空！');"
            Return
        Else
            Try
                Dim _dc As Decimal = Convert.ToDecimal(txb_hour.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('总工时 只能是数字！');"
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

        Dim i As Integer = 0
        For i = 0 To Datagrid1.Items.Count - 1
            Dim str1 As String = CType(Datagrid1.Items(i).Cells(8).FindControl("txb_qty"), TextBox).Text
            StrSql = "update tcpc0.dbo.wo_cost_detail_tmp set wocd_proc_qty = '" & str1 & "', wocd_cost=" & CDec(str1) & " * isnull(wocd_price,0), wocd_cost_unitPrice = " & CDec(str1) & " * Isnull(wocd_unitPrice, 0) " _
                & " where id='" & Datagrid1.Items(i).Cells(1).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
        Next

        Dim qty As Decimal = CDec(txb_qty.Text)
        Dim rate As Decimal = 0

        StrSql = " SELECT SUM(ISNULL(wocd_proc_qty,0)),SUM(CASE WHEN ISNULL(wocd_proc_qty,0) >0 THEN 0 ELSE ISNULL(wocd_rate,1) END) " _
            & " FROM wo_cost_detail_tmp WHERE createdBy='" & Session("uID") & "' AND wocd_proc_name= N'" & chk.sqlEncode(txb_proc.Text) & "' GROUP BY wocd_proc_name  "
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
        While reader.Read
            qty = qty - reader(0)
            rate = reader(1)
        End While
        reader.Close()

        If qty > 0 And rate > 0 Then
            StrSql = "Select id, wocd_rate = isnull(wocd_rate,1), wocd_price = isnull(wocd_price,0), wocd_pcost = isnull(wocd_pcost,1), wocd_unitPrice = Isnull(wocd_unitPrice, 0) " _
                & " from tcpc0.dbo.wo_cost_detail_tmp " _
                & " where createdBy='" & Session("uID") & "' and isnull(wocd_proc_qty,0) = 0 and wocd_proc_name=N'" & chk.sqlEncode(txb_proc.Text) & "'"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    For i = 0 To .Rows.Count - 1
                        Dim StrSql2 As String = ""
                        Try
                            StrSql2 = " Update tcpc0.dbo.wo_cost_detail_tmp " _
                                & " set wocd_proc_qty='" & qty / rate * .Rows(i).Item("wocd_rate") & "', wocd_cost='" & qty / rate * .Rows(i).Item("wocd_rate") * .Rows(i).Item("wocd_price") & "'" _
                                & " , wocd_cost_unitPrice = '" & qty / rate * .Rows(i).Item("wocd_rate") * .Rows(i).Item("wocd_unitPrice") & "' "
                            StrSql2 &= " where id='" & .Rows(i).Item("id").ToString() & "'"

                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql2)
                        Catch ex As Exception
                            Response.Write(StrSql2)
                        End Try
                    Next
                End If
            End With
            ds.Reset()
        End If

        txb_qty.Text = ""

        BindData()

    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        StrSql = "Insert Into wo_cost_detail(wocd_userid, wocd_user_no, wocd_username, wocd_proc_id, wocd_proc_name, wocd_proc_qty, wocd_fixed_qty, wocd_proc_adj, wocd_price "
        StrSql &= "     ,wocd_cost, wocd_site, wocd_cc, wocd_nbr, wocd_id, wocd_part, createdDate, createdBy, wocd_pcost, wocd_type, wocd_appr, wocd_apprdate"
        StrSql &= "     , wocd_group_id, wocd_date,createdName, wocd_duty,wocd_line, wocd_unitPrice, wocd_cost_unitPrice) "
        StrSql &= " SELECT wocd_userid, wocd_user_no, wocd_username, wocd_proc_id, wocd_proc_name, wocd_proc_qty, wocd_fixed_qty, wocd_proc_adj, wocd_price"
        StrSql &= "     ,wocd_cost, wocd_site, wocd_cc, wocd_nbr, wocd_id, wocd_part, createdDate, createdBy, wocd_pcost, wocd_type, wocd_appr, wocd_apprdate"
        StrSql &= "     , wocd_group_id, wocd_date,createdName, wocd_duty,wocd_line, wocd_unitPrice, wocd_cost_unitPrice "
        StrSql &= " FROM tcpc0.dbo.wo_cost_detail_tmp WHERE createdBy='" & Session("uID") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

        dd_group.SelectedIndex = 0
        txb_no.Text = ""
        txb_qty.Text = ""

        StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

        BindData()
    End Sub

    Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
        Datagrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub
End Class
