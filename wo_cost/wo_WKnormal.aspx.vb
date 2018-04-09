Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Data
Imports WO2Group
Partial Class wo_WKnormal
    Inherits BasePage
    Public chk As New adamClass
    Public hr_salary As New WO2
    Dim StrSql As String
    Dim ds As DataSet
    Dim nRet As Integer
    Dim item As ListItem
    Dim reader As SqlDataReader

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""
        If Not IsPostBack Then

            Dim ls As ListItem

            StrSql = "SELECT qad_site FROM Domain_Mes WHERE plantcode ='" & Session("PlantCode") & "' "
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
            While reader.Read
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(0)
                dd_site.Items.Add(ls)
            End While
            reader.Close()


            dd_proc.Items.Add(New ListItem("--", "0"))
            dd_group.Items.Add(New ListItem("--", "0"))
            dd_line.Items.Add(New ListItem("--", "0"))

            If Request("dd") = Nothing Then
                txb_date.Text = Format(Today, "yyyy-MM-dd")
            Else
                txb_date.Text = Request("dd")
            End If

            If Request("nbr") <> Nothing And Request("site") <> Nothing Then
                txb_wonbr.Text = Request("nbr")
                txb_woid.Text = Request("id")
                dd_site.SelectedValue = Request("site")
                getWOInfo()
            End If



            StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            btn_clear.Attributes.Add("onclick", "return confirm('确定要清除所有数据，重新开始汇报?');")

            BindData()
        End If
    End Sub

    Function getWOInfo() As Boolean
        '//get information of work order from QAD
        lbl_cc.Text = ""
        lbl_cc1.Text = ""
        lbl_comp.Text = ""
        lbl_cost.Text = ""
        lbl_part.Text = ""
        lbl_price.Text = ""
        lbl_rate.Text = ""
        lbl_desc.Text = ""
        dd_proc.Items.Clear()
        dd_group.Items.Clear()


        Dim coefficient As Decimal = 1.02

        If txb_wonbr.Text.Trim.Length <= 0 Then
            Exit Function
        End If

        Dim dm As String
        dm = GetDomain(dd_site.SelectedValue)

        StrSql = "SELECT  wo_flr_cc, wo_qty_comp,wo_lot,wo_part,wo_nbr,wo_routing FROM Qad_data.dbo.wo_mstr Where wo_domain= '" & dm & "' And ISNULL(wo_type,'')<>'R' and wo_site='" & dd_site.SelectedValue & "'  and wo_nbr='" & txb_wonbr.Text & "' and ISNULL(wo_line,'') <> ''  "
        If txb_woid.Text.Trim.Length > 0 Then
            StrSql &= " and wo_lot='" & txb_woid.Text & "' "
        End If
        StrSql &= " order by wo_lot "
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
        If (reader.Read()) Then
            lbl_cc.Text = reader(0).ToString()
            lbl_comp.Text = Format(reader(1), "##0.#####")
            txb_woid.Text = reader(2).ToString()
            lbl_part.Text = reader(3).ToString()
            lbl_desc.Text = ""
            lbl_cost.Text = ""
            txb_wonbr.Text = reader(4).ToString()

            lbl_rate.Text = reader(5).ToString()

        Else
            Dim conn As OdbcConnection = Nothing
            Dim comm As OdbcCommand = Nothing
            Dim dr As OdbcDataReader

            Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
            ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;
            Dim sql As String = "Select wo_flr_cc, wo_qty_comp,wo_lot,wo_part,wo_nbr,wo_routing from PUB.wo_mstr where wo_domain='" & dm & "' "
            sql &= " and wo_type<>'R' and wo_site='" & dd_site.SelectedValue & "' and wo_vend <> '' "
            sql &= " and wo_nbr='" & txb_wonbr.Text & "' "
            If txb_woid.Text.Trim.Length > 0 Then
                sql &= " and wo_lot='" & txb_woid.Text & "' "
            End If
            sql &= " order by wo_lot "
            Try
                conn = New OdbcConnection(connectionString)
                conn.Open()
                comm = New OdbcCommand(sql, conn)
                dr = comm.ExecuteReader()
                If (dr.Read()) Then
                    lbl_cc.Text = dr.GetValue(0).ToString()
                    lbl_comp.Text = Format(dr.GetValue(1), "##0.#####")
                    txb_woid.Text = dr.GetValue(2).ToString()
                    lbl_part.Text = dr.GetValue(3).ToString()
                    lbl_desc.Text = ""
                    lbl_cost.Text = ""
                    'lbl_price.Text = ""
                    'lbl_rate.Text = ""
                    txb_wonbr.Text = dr.GetValue(4).ToString()

                    lbl_rate.Text = dr.GetValue(5).ToString()
                    'dm = GetDomain(dd_site.SelectedValue)



                    lbl_quantity.Text = ""
                Else
                    ltlAlert.Text = "alert('此加工单不存在或是返工单!'); "
                    Exit Function
                End If
                dr.Close()
                conn.Close()



            Catch oe As OdbcException
                Response.Write(oe.Message)
            Finally
                If conn.State = System.Data.ConnectionState.Open Then
                    conn.Close()
                    'response.write("ssss")
                End If
            End Try

            'comm.Dispose()
            conn.Dispose()
        End If

        'judge wether the site main site  
        If lbl_rate.Text.Trim.Length = 0 Then  ' If not find routing in Work Order
            lbl_rate.Text = lbl_part.Text
        End If

        '//Get the work procedure for the part in the work order
        lblTec.Text = lbl_rate.Text
        Dim ls As ListItem
        dd_proc.Items.Clear()
        ls = New ListItem
        ls.Value = 0
        ls.Text = "--"
        dd_proc.Items.Add(ls)

        StrSql = " SELECT id, wo_procName, wo_gl, 0, ISNULL(wo_pdesc,'') " _
            & "         , wo_price_up = Case " & Session("PlantCode") & " When 1 Then Isnull(wo_price_szx, 0) When 2 Then Isnull(wo_price_zql, 0) When 5 Then Isnull(wo_price_yql, 0) End" _
            & "    FROM  tcpc0.dbo.wo_Tec WHERE wo_tec ='" & lbl_rate.Text & "' AND wo_del =  0 ORDER BY wo_procName "
        'reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
        While reader.Read
            ls = New ListItem
            ls.Value = reader("id")
            ls.Text = reader("wo_procName") & "*" & reader("wo_gl") & "*" & reader("wo_price_up")
            dd_proc.Items.Add(ls)

            lbl_desc.Text = reader(4)
            lbl_price.Text = reader(3)
        End While
        reader.Close()

        If dd_proc.Items.Count = 1 Then
            ltlAlert.Text = "alert('" & lbl_rate.Text & " 请维护加工单的工序!'); "
            Exit Function
        End If

        If lbl_comp.Text.Length > 0 Then
            Dim orderprice As Decimal = 0
            StrSql = "SELECT Sum(wo_gl) FROM  tcpc0.dbo.wo_Tec WHERE wo_tec ='" & lbl_rate.Text & "' AND wo_del =  0 "
            orderprice = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, StrSql)
            lbl_cost.Text = (Math.Round(CDec(lbl_comp.Text) * orderprice * coefficient, 5)).ToString()
        End If

        If lbl_cc1.Text.Length <= 0 Or lbl_cc.Text <> lbl_cc1.Text Then
            lbl_cc1.Text = lbl_cc.Text
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
                    ls.Text = reader(1).ToString.Trim

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

            dd_line.Items.Clear()

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

    Function GetDomain(ByVal str As String) As String
        StrSql = "SELECT realdomain FROM Domain_Mes WHERE qad_site ='" & str & "' "
        GetDomain = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, StrSql)
    End Function

    Protected Sub btn_woload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_woload.Click
        getWOInfo()

        BindData()

    End Sub

    Sub BindData()
        StrSql = " select cd.id, cd.wocd_site, cd.wocd_nbr, cd.wocd_id, cd.wocd_cc, cd.wocd_part, cd.wocd_user_no, cd.wocd_username, cd.wocd_proc_name, isnull(cd.wocd_proc_qty, 0)" _
            & "         , isnull(cd.wocd_proc_adj, 0), isnull(cd.wocd_cost, 0) * 1.02, cd.wocd_date, isnull(cd.createdName, '')" _
            & "         , isnull(cd.wocd_price, 0) * 1.02, isnull(cd.wocd_unplan,0) " _
            & "         , wocd_cost_unitPrice = Isnull(wocd_cost_unitPrice, 0), wocd_unitPrice = Isnull(wocd_unitPrice, 0)" _
            & " from tcpc0.dbo.wo_cost_detail_tmp cd "
        If Session("uRole") <> 1 Then
            StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
        End If
        StrSql &= " Left Outer Join tcpc0.dbo.wo_Rate t ON Year(t.workdate) = Year(cd.wocd_date) And Month(t.workdate) = Month(cd.wocd_date) And t.plantcode ='" & Session("plantcode") & "' "
        StrSql &= " where cd.id is not null and cd.createdBy='" & Session("uID") & "' and isnull(cd.wocd_type,'')=''"
        If dd_site.SelectedIndex > 0 Then
            StrSql &= " and cd.wocd_site ='" & dd_site.SelectedValue & "' "
        End If
        If txb_wonbr.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_nbr ='" & txb_wonbr.Text.Trim() & "' "
        End If
        If txb_woid.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_id ='" & txb_woid.Text.Trim() & "' "
        End If
        If lbl_cc.Text.Trim.Length > 0 Then
            StrSql &= " and cd.wocd_cc ='" & lbl_cc.Text.Trim() & "' "
        End If
        If dd_proc.SelectedIndex > 0 Then
            StrSql &= " and cd.wocd_proc_id ='" & dd_proc.SelectedValue & "' "
        End If

        StrSql &= " order by cd.createdDate desc,cd.wocd_user_no  "

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
        dtl.Columns.Add(New DataColumn("proc_pri", System.Type.GetType("System.String"))) '指标
        dtl.Columns.Add(New DataColumn("proc_cc", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_nbr", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("proc_id", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("wo_created", System.Type.GetType("System.String")))

        dtl.Columns.Add(New DataColumn("proc_Uunplan", System.Type.GetType("System.String")))

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
                    drow.Item("proc_adj") = Format(.Rows(i).Item(10), "##0.#####")

                    drow.Item("proc_price1") = Format(.Rows(i).Item(11), "##0.#####")
                    If Not IsDBNull(.Rows(i).Item(12)) Then
                        drow.Item("wo_date_comp") = Format(.Rows(i).Item(12), "yy-MM-dd")
                    End If
                    drow.Item("wo_created") = .Rows(i).Item(13).ToString().Trim()
                    drow.Item("proc_pri") = Format(.Rows(i).Item(14), "##0.#####")

                    drow.Item("proc_Uunplan") = Format(.Rows(i).Item(15), "##0.#####") 'wocd_unplan

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
            StrSql = "  update tcpc0.dbo.wo_cost_detail_tmp " _
                    & " set wocd_proc_qty = '" & str1 & "'" _
                    & "      , wocd_cost=" & CDec(str1) & " * wocd_price " _
                    & "      , wocd_cost_unitPrice = " & CDec(str1) & " * wocd_unitPrice " _
                    & " where id='" & e.Item.Cells(1).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            BindData()
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

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

        If txb_woid.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('加工单ID 不能为空！');"
            Return
        End If

        If dd_proc.SelectedIndex = 0 Then
            ltlAlert.Text = "alert('请先选择一道 工序！');"
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

        '增加 毛管的手工封口要到WO2中汇报

        If dd_proc.SelectedItem.Text.Trim.Split("*")(0) = "手工封口" Then
            ltlAlert.Text = "alert('毛管的手工封口，请至WO2中汇报！')"
            Exit Sub
        End If

        If Session("uRole") <> 1 Then
            StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & lbl_cc.Text & "' and perm_userid='" & Session("uID") & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                ltlAlert.Text = "alert('无操作此成本中心的权限.')"
                Exit Sub
            End If
        End If

        If dd_group.SelectedIndex > 0 Then
            '获取组别内分配系数 wocd_rate
            If Session("Plantcode") = 1 Then
                StrSql = "Select wod_user_id, wod_user_no, wod_user_name, isnull(wod_rate, 1) from tcpc0.dbo.wo_group_detail where wod_group_id='" & dd_group.SelectedValue & "'"
            Else
                StrSql = " SELECT wo2_userID , wo2_userno, wo2_username, 1 FROM wo2_group_detail WHERE wo2_groupID='" & dd_group.SelectedValue & "'"
            End If
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While reader.Read
                Try
                    StrSql = "If Not Exists(Select wocd_userid From wo_cost_detail_tmp " _
                           & "              Where wocd_site = '" & dd_site.SelectedItem.Text & "' And wocd_nbr = '" & txb_wonbr.Text & "' And wocd_id = " & txb_woid.Text _
                           & "                      And wocd_date = '" & txb_date.Text & "' And wocd_group_id = " & dd_group.SelectedValue _
                           & "                      And wocd_userid = " & reader(0).ToString().Trim() & " And wocd_proc_id = N'" & dd_proc.SelectedValue() & "')" _
                           & " Begin " _
                           & "          INSERT INTO wo_cost_detail_tmp(wocd_userid,wocd_user_no,wocd_username,wocd_proc_id,wocd_proc_name,wocd_site,wocd_cc,wocd_nbr,wocd_id,createdDate,createdBy" _
                           & "                                          , wocd_part, wocd_price, wocd_pcost, wocd_group_id, wocd_date, wocd_rate, createdName,wocd_line, wocd_tec, wocd_unitPrice) " _
                           & "          SELECT '" & reader(0).ToString().Trim() & "','" & reader(1).ToString().Trim() & "',N'" & reader(2).ToString().Trim() & "','" & dd_proc.SelectedValue() & "'" _
                           & "                  , N'" & dd_proc.SelectedItem.Text.Trim.Split("*")(0) & "'" _
                           & "                  , '" & dd_site.SelectedValue & "','" & lbl_cc.Text & "','" & txb_wonbr.Text & "','" & txb_woid.Text & "',getdate(),'" & Session("uID") & "'" _
                           & "                  , '" & lbl_part.Text & "', '" & dd_proc.SelectedItem.Text.Trim.Split("*")(1) & "', '" & lbl_price.Text & "', '" & dd_group.SelectedValue & "'" _
                           & "                  , '" & txb_date.Text & "', '" & reader(3).ToString().Trim() & "', N'" & Session("uName") & "'"
                    If dd_line.SelectedValue > 0 Then
                        StrSql &= " , N'" & dd_line.SelectedItem.Text & "' "
                    Else
                        StrSql &= " , Null "
                    End If
                    StrSql &= " , '" & lbl_rate.Text & "'"
                    '"', '" & dd_proc.SelectedItem.Text.Trim.Split("*")(2) & "'"
                    If dd_proc.SelectedItem.Text.Trim.Split("*")(2) = "" Then
                        StrSql &= " , Null"
                    Else
                        StrSql &= " , N'" & dd_proc.SelectedItem.Text.Trim.Split("*")(2) & "'"
                    End If

                    StrSql &= "         FROM Users WHERE  userID='" & reader(0).ToString().Trim() & "' AND PlantCode='" & Session("PlantCode") & "' AND deleted=0 AND isactive=1 AND (leavedate IS NULL OR leavedate >= '" & txb_date.Text & "') " _
                            & " End "
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
                               & "              Where wocd_site = '" & dd_site.SelectedItem.Text & "' And wocd_nbr = '" & txb_wonbr.Text & "' And wocd_id = " & txb_woid.Text _
                               & "                      And wocd_date = '" & txb_date.Text & "' And Isnull(wocd_group_id, 0) = 0 " _
                               & "                      And wocd_userid = " & .Rows(0).Item(0).ToString().Trim() & " And Isnull(wocd_proc_id, 0) = N'" & dd_proc.SelectedValue() & "')" _
                               & " Begin " _
                               & "      INSERT INTO tcpc0.dbo.wo_cost_detail_tmp(wocd_userid, wocd_user_no, wocd_username, wocd_proc_id, wocd_proc_name, wocd_site, wocd_cc, wocd_nbr" _
                               & "                                                  , wocd_id, createdDate, createdBy, wocd_part, wocd_price, wocd_pcost, wocd_date, createdName" _
                               & "                                                  , wocd_unitPrice, wocd_line, wocd_tec) " _
                               & "      Values('" & .Rows(0).Item(0).ToString().Trim() & "','" & txb_no.Text & "', N'" & .Rows(0).Item(1).ToString().Trim() & "','" & dd_proc.SelectedValue() & "'" _
                               & "              , N'" & dd_proc.SelectedItem.Text.Trim.Split("*")(0) & "','" & dd_site.SelectedValue & "'" _
                               & "              , '" & lbl_cc.Text & "','" & txb_wonbr.Text & "','" & txb_woid.Text & "',getdate(),'" & Session("uID") & "','" & lbl_part.Text & "'" _
                               & "              , '" & dd_proc.SelectedItem.Text.Trim.Split("*")(1) & "','" & lbl_price.Text & "','" & txb_date.Text & "'" _
                               & "              , N'" & Session("uName") & "'"
                        ' "', '" & dd_proc.SelectedItem.Text.Trim.Split("*")(2) & "'"
                        If dd_proc.SelectedItem.Text.Trim.Split("*")(2) = "" Then
                            StrSql2 &= " , Null"
                        Else
                            StrSql2 &= " , N'" & dd_proc.SelectedItem.Text.Trim.Split("*")(2) & "'"
                        End If


                        If dd_line.SelectedValue > 0 Then
                            StrSql2 &= " , N'" & dd_line.SelectedItem.Text & "'"
                        Else
                            StrSql2 &= " , Null"
                        End If
                        StrSql2 &= "    , '" & lbl_rate.Text & "' ) " _
                                 & " End "
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
        txtUnplan.Text = ""

        BindData()

    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        txb_wonbr.Text = ""
        txb_woid.Text = ""

        lbl_cc.Text = ""
        lbl_comp.Text = ""
        lbl_cost.Text = ""
        lbl_part.Text = ""
        lbl_price.Text = ""
        lbl_rate.Text = ""
        lbl_desc.Text = ""
        lblTec.Text = ""


        StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)


        txb_wonbr.Focus()
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

        If txb_woid.Text.Trim().Length = 0 Then
            ltlAlert.Text = "alert('加工单ID 不能为空！');"
            Return
        End If

        If dd_proc.SelectedIndex = 0 Then
            ltlAlert.Text = "alert('请先选择一道 工序！');"
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

        '分配

        If Datagrid1.Items.Count = 0 Then
            ltlAlert.Text = "alert('没有可分配的数据.')"
            Exit Sub
        End If
        '/-- END -------------------------------/

        Dim i As Integer = 0

        '检验：填写的“数量”必须是数字
        For i = 0 To Datagrid1.Items.Count - 1
            Dim str1 As String = CType(Datagrid1.Items(i).Cells(8).FindControl("txb_qty"), TextBox).Text

            If str1 <> "" Then
                Try
                    Dim n = Convert.ToInt32(str1)
                Catch ex As Exception
                    ltlAlert.Text = "alert('填写的数量必须是数字!')"
                    Return
                End Try
            Else
                CType(Datagrid1.Items(i).Cells(8).FindControl("txb_qty"), TextBox).Text = "0"
            End If
            
        Next

        '将“数量”存入数据库
        i = 0
        For i = 0 To Datagrid1.Items.Count - 1
            Dim str1 As String = CType(Datagrid1.Items(i).Cells(8).FindControl("txb_qty"), TextBox).Text
            StrSql = " update tcpc0.dbo.wo_cost_detail_tmp " _
                   & " set wocd_proc_qty = '" & str1 & "'" _
                   & "     , wocd_cost = " & CDec(str1) & " * isnull(wocd_price, 0) " _
                   & "     , wocd_cost_unitPrice = " & CDec(str1) & " * Isnull(wocd_unitPrice, 0) " _
                   & " where id = '" & Datagrid1.Items(i).Cells(1).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
        Next

        Dim qty As Decimal = CDec(txb_qty.Text)
        Dim rate As Decimal = 0
        '已经汇报的：分配系数设置为0，表示不参加此次计算
        '没有汇报的：如果没有分配系数，则默认为1；如果该员工的分配系数需要大于1，那么只能先讲此人加到组别里面去了
        StrSql = " SELECT SUM(ISNULL(wocd_proc_qty, 0)), SUM(CASE WHEN ISNULL(wocd_proc_qty, 0) > 0 THEN 0 ELSE ISNULL(wocd_rate, 1) END) " _
               & " FROM wo_cost_detail_tmp " _
               & " WHERE createdBy = '" & Session("uID") & "' AND wocd_proc_id = '" & dd_proc.SelectedValue & "' " _
               & " GROUP BY wocd_proc_id "
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
        While reader.Read
            qty = qty - reader(0)
            rate = reader(1)
        End While
        reader.Close()

        If qty > 0 And rate > 0 Then
            '未尚未汇报的，按比例平均分配
            StrSql = " Select id, wocd_rate = isnull(wocd_rate, 1), wocd_price = isnull(wocd_price, 0), isnull(wocd_pcost, 0), wocd_unitPrice = isnull(wocd_unitPrice, 0) " _
                   & " from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "' and isnull(wocd_proc_qty,0) = 0 and wocd_proc_id='" & dd_proc.SelectedValue & "'"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    For i = 0 To .Rows.Count - 1
                        Dim StrSql2 As String = ""
                        Try
                            StrSql2 = " Update tcpc0.dbo.wo_cost_detail_tmp " _
                                    & " set wocd_proc_qty='" & qty / rate * .Rows(i).Item("wocd_rate") & "'" _
                                    & "     , wocd_cost='" & qty / rate * .Rows(i).Item("wocd_rate") * .Rows(i).Item("wocd_price") & "'" _
                                    & "     , wocd_cost_unitPrice='" & qty / rate * .Rows(i).Item("wocd_rate") * .Rows(i).Item("wocd_unitPrice") & "'"
                            StrSql2 &= " where id='" & .Rows(i).Item("id").ToString() & "'"
                            'Response.Write(StrSql2)
                            'Response.Write("--")

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

    Protected Sub dd_proc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_proc.SelectedIndexChanged
        BindData()
        If dd_proc.SelectedIndex > 0 Then
            txtUnplan.Text = (CDec(dd_proc.SelectedItem.Text.Trim.Split("*")(1)) * 6.07).ToString()
        End If
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click


        StrSql = "Insert Into wo_cost_detail(wocd_userid, wocd_user_no, wocd_username, wocd_proc_id, wocd_proc_name, wocd_proc_qty, wocd_fixed_qty, wocd_proc_adj, wocd_price "
        StrSql &= " , wocd_cost, wocd_site, wocd_cc, wocd_nbr, wocd_id, wocd_part, createdDate, createdBy, wocd_pcost, wocd_type, wocd_appr, wocd_apprdate"
        StrSql &= " , wocd_group_id, wocd_date,createdName,wocd_unplan,wocd_line,wocd_tec,wocd_NewCC, wocd_unitPrice, wocd_cost_unitPrice) "
        StrSql &= " SELECT wocd_userid, wocd_user_no, wocd_username, wocd_proc_id, wocd_proc_name, wocd_proc_qty, wocd_fixed_qty, wocd_proc_adj, wocd_price"
        StrSql &= "         , wocd_cost, wocd_site, wocd_cc, wocd_nbr, wocd_id, wocd_part, createdDate, createdBy, wocd_pcost, wocd_type, wocd_appr, wocd_apprdate"
        StrSql &= "         , wocd_group_id, wocd_date,createdName,wocd_unplan,wocd_line,wocd_tec,ISNULL(cc_centerNew,''), wocd_unitPrice, wocd_cost_unitPrice " _
                & " FROM tcpc0.dbo.wo_cost_detail_tmp "
        StrSql &= " LEFT OUTER JOIN tcpc0.dbo.hr_cc_Center ON cc_center = wocd_cc "
        StrSql &= "  WHERE createdBy='" & Session("uID") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

        dd_proc.SelectedIndex = 0
        dd_group.SelectedIndex = 0
        txb_no.Text = ""
        txb_qty.Text = ""
        txb_wonbr.Text = ""
        txb_woid.Text = ""


        StrSql = "Delete from tcpc0.dbo.wo_cost_detail_tmp where createdBy='" & Session("uID") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

        BindData()

    End Sub

    Protected Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click

        Response.Redirect("wo_list.aspx?rm=" & Now)
    End Sub
End Class
