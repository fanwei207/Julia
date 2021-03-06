Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class ws_plan
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
                
                If Request("dd") <> Nothing Then
                    txb_date.Text = Request("dd")
                Else
                    txb_date.Text = Format(Today, "yyyy-MM-01")
                End If
                If Request("dd1") <> Nothing Then
                    txb_date1.Text = Request("dd1")
                Else
                    txb_date1.Text = Format(Today, "yyyy-MM-dd")
                End If

                If Request("part") <> Nothing Then
                    txb_part.Text = Request("part")
                End If

                If Request("site") <> Nothing Then
                    ddl_site.SelectedValue = Request("site")
                End If

                If Request("nbr") <> Nothing Then
                    txb_nbr.Text = Request("nbr")
                End If

                If Request("lot") <> Nothing Then
                    txb_lot.Text = Request("lot")
                End If

                LoadCC()
                If Request("cc") <> Nothing Then
                    ddl_cc.SelectedValue = Request("cc")
                End If

                BindData()
            End If
        End Sub
        Sub BindData()
            If txb_date.Text.Trim.Length <= 0 Or Not IsDate(txb_date.Text) Then
                txb_date.Text = Format(Today, "yyyy-MM-dd")
            End If

            Dim strPlant As String = ""
            Dim strPlantCode As String = ""
            Dim strCC As String = ""
            Dim qty As Decimal = 0
            Dim qty_bad As Decimal = 0
            Dim strStatus As String = ""
            Dim strNbr As String = ""
            Dim strLot As String = ""
            Dim strStart As DateTime
            Dim strEnd As DateTime
            Dim strPart As String = ""

            If ddl_site.SelectedValue = 1 Or ddl_site.SelectedValue = 0 Then
                StrSql = " select 'szx',N'上海振欣 SZX', d.ls_cc,d.ls_nbr,d.ls_lot,isnull(d.ls_qty,0),isnull(d.ls_status,N'正品'),d.createdDate,d.ls_part from SZXWS.LineData_SZX.dbo.ls_data d inner join SZXWS.LineData_SZX.dbo.ls_item i on i.ls_item_plant=d.ls_plant and i.ls_item_cc=d.ls_cc and i.ls_item_line=d.ls_line and RTRIM(LTRIM(i.ls_item_nbr))=RTRIM(LTRIM(d.ls_nbr)) and RTRIM(LTRIM(i.ls_item_lot))=RTRIM(LTRIM(d.ls_lot)) "
                StrSql &= " where d.deletedBy is null and d.ls_plant =1 "
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and d.ls_cc ='" & ddl_cc.SelectedValue & "' "
                End If
                If txb_nbr.Text.Trim.Length > 0 Then
                    StrSql &= " and d.ls_nbr ='" & txb_nbr.Text.Trim & "' "
                End If
                If txb_lot.Text.Trim.Length > 0 Then
                    StrSql &= " and d.ls_lot ='" & txb_lot.Text.Trim & "' "
                End If
                If txb_part.Text.Trim.Length > 0 Then
                    StrSql &= " and d.ls_part='" & txb_part.Text.Trim & "' "
                End If
                StrSql &= " and i.ls_item_date< DATEADD(day,1,'" & txb_date1.Text & "') and i.ls_item_date>='" & txb_date.Text & "'"
            End If

            If ddl_site.SelectedValue = 2 Or ddl_site.SelectedValue = 0 Then
                StrSql &= " UNION ALL (select 'zql',N'镇江强凌 ZQL', d.ls_cc,d.ls_nbr,d.ls_lot,isnull(d.ls_qty,0),isnull(d.ls_status,N'正品'),d.createdDate,d.ls_part from ZQLWS.LineData_ZQL.dbo.ls_data d inner join ZQLWS.LineData_ZQL.dbo.ls_item i on i.ls_item_plant=d.ls_plant and i.ls_item_cc=d.ls_cc and i.ls_item_line=d.ls_line and RTRIM(LTRIM(i.ls_item_nbr))=RTRIM(LTRIM(d.ls_nbr)) and RTRIM(LTRIM(i.ls_item_lot))=RTRIM(LTRIM(d.ls_lot)) "
                StrSql &= " where d.deletedBy is null and d.ls_plant =2 "
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and d.ls_cc ='" & ddl_cc.SelectedValue & "' "
                End If
                If txb_nbr.Text.Trim.Length > 0 Then
                    StrSql &= " and d.ls_nbr ='" & txb_nbr.Text.Trim & "' "
                End If
                If txb_lot.Text.Trim.Length > 0 Then
                    StrSql &= " and d.ls_lot ='" & txb_lot.Text.Trim & "' "
                End If
                If txb_part.Text.Trim.Length > 0 Then
                    StrSql &= " and d.ls_part='" & txb_part.Text.Trim & "' "
                End If
                StrSql &= " and i.ls_item_date< DATEADD(day,1,'" & txb_date1.Text & "') and i.ls_item_date>='" & txb_date.Text & "')"
            End If
            If ddl_site.SelectedValue = 5 Or ddl_site.SelectedValue = 0 Then
                StrSql &= " UNION ALL (select 'yql',N'扬州强凌 YQL', d.ls_cc,d.ls_nbr,d.ls_lot,isnull(d.ls_qty,0),isnull(d.ls_status,N'正品'),d.createdDate,d.ls_part from YQLWS.LineData_YQL.dbo.ls_data d inner join YQLWS.LineData_YQL.dbo.ls_item i on i.ls_item_plant=d.ls_plant and i.ls_item_cc=d.ls_cc and i.ls_item_line=d.ls_line and RTRIM(LTRIM(i.ls_item_nbr))=RTRIM(LTRIM(d.ls_nbr)) and RTRIM(LTRIM(i.ls_item_lot))=RTRIM(LTRIM(d.ls_lot)) "
                StrSql &= " where d.deletedBy is null  and d.ls_plant =5 "
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and d.ls_cc ='" & ddl_cc.SelectedValue & "' "
                End If
                If txb_nbr.Text.Trim.Length > 0 Then
                    StrSql &= " and d.ls_nbr ='" & txb_nbr.Text.Trim & "' "
                End If
                If txb_lot.Text.Trim.Length > 0 Then
                    StrSql &= " and d.ls_lot ='" & txb_lot.Text.Trim & "' "
                End If
                If txb_part.Text.Trim.Length > 0 Then
                    StrSql &= " and d.ls_part='" & txb_part.Text.Trim & "' "
                End If
                StrSql &= " and i.ls_item_date< DATEADD(day,1,'" & txb_date1.Text & "') and i.ls_item_date>='" & txb_date.Text & "')"
            End If
            StrSql &= " order by d.ls_cc,d.ls_nbr,d.ls_lot,d.createdDate"

            If StrSql.Substring(0, 10) = " UNION ALL" Then
                StrSql = StrSql.Substring(10)
            End If


            'Response.Write(StrSql)
            'Exit Sub

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("g_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_nbr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_lot", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_start", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_end", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_fail", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_effi", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_qad", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_qty", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_diff", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_part", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_ord", System.Type.GetType("System.Decimal")))


            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If strNbr <> "" Then
                            If (strNbr <> .Rows(i).Item(3).ToString().Trim() Or strLot <> .Rows(i).Item(4).ToString().Trim()) Then
                                drow = dtl.NewRow()
                                drow.Item("g_site") = strPlant
                                drow.Item("g_cc") = strCC
                                drow.Item("g_nbr") = strNbr
                                drow.Item("g_lot") = strLot
                                drow.Item("g_start") = strStart
                                drow.Item("g_end") = strEnd
                                If qty <> 0 Then
                                    drow.Item("g_fail") = (1 - qty_bad / qty) * 100
                                Else
                                    drow.Item("g_fail") = 0
                                End If

                                If (strEnd - strStart).Seconds <> 0 Then
                                    drow.Item("g_effi") = (qty) / (strEnd - strStart).Seconds / 3600.0
                                End If
                                drow.Item("g_qty") = (qty)
                                drow.Item("g_qad") = 0

                                Dim conn As OdbcConnection = Nothing
                                Dim comm As OdbcCommand = Nothing
                                Dim dr As OdbcDataReader

                                Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                                ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

                                Dim sql As String = "Select wo_qty_comp,wo_qty_ord from PUB.wo_mstr where wo_domain='" & strPlantCode & "' "
                                sql &= " and wo_nbr='" & strNbr & "' "
                                sql &= " and wo_lot='" & strLot & "' "

                                Try
                                    conn = New OdbcConnection(connectionString)
                                    conn.Open()
                                    comm = New OdbcCommand(sql, conn)
                                    dr = comm.ExecuteReader()
                                    If (dr.Read()) Then
                                        If Not IsDBNull(dr.GetValue(0)) Then
                                            drow.Item("g_qad") = dr.GetValue(0)
                                            drow.Item("g_ord") = dr.GetValue(1)
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

                                drow.Item("g_diff") = drow.Item("g_qty") - drow.Item("g_qad")
                                drow.Item("g_part") = strPart

                                dtl.Rows.Add(drow)
                                strStart = .Rows(i).Item(7)
                                qty = 0
                                qty_bad = 0
                            End If
                                strPlantCode = .Rows(i).Item(0).ToString().Trim()
                                strPlant = .Rows(i).Item(1).ToString().Trim()
                                strCC = .Rows(i).Item(2).ToString().Trim()
                                strNbr = .Rows(i).Item(3).ToString().Trim()
                                strLot = .Rows(i).Item(4).ToString().Trim()
                                strStatus = .Rows(i).Item(6).ToString.Trim
                                If .Rows(i).Item(6).ToString.Trim <> "正品" Then
                                    qty_bad = qty_bad + .Rows(i).Item(5)
                                Else
                                    qty = qty + .Rows(i).Item(5)
                                End If
                                strEnd = .Rows(i).Item(7)
                                strPart = .Rows(i).Item(8)
                            Else
                                strPlantCode = .Rows(i).Item(0).ToString().Trim()
                                strPlant = .Rows(i).Item(1).ToString().Trim()
                                strCC = .Rows(i).Item(2).ToString().Trim()
                                strNbr = .Rows(i).Item(3).ToString().Trim()
                                strLot = .Rows(i).Item(4).ToString().Trim()
                                strStatus = .Rows(i).Item(6).ToString.Trim
                                If .Rows(i).Item(6).ToString.Trim <> "正品" Then
                                    qty_bad = .Rows(i).Item(5)
                                Else
                                    qty = .Rows(i).Item(5)
                                End If
                                strStart = .Rows(i).Item(7)
                                strEnd = .Rows(i).Item(7)
                                strPart = .Rows(i).Item(8)
                            End If
                    Next
                End If
            End With
            ds.Reset()

            If strNbr <> "" Then
                drow = dtl.NewRow()
                drow.Item("g_site") = strPlant
                drow.Item("g_cc") = strCC
                drow.Item("g_nbr") = strNbr
                drow.Item("g_lot") = strLot
                drow.Item("g_start") = strStart
                drow.Item("g_end") = strEnd
                If qty <> 0 Then
                    drow.Item("g_fail") = (1 - qty_bad / qty) * 100
                Else
                    drow.Item("g_fail") = 0
                End If

                drow.Item("g_effi") = (qty) / (strEnd - strStart).Seconds / 3600.0
                drow.Item("g_qty") = (qty)
                drow.Item("g_qad") = 0

                Dim conn As OdbcConnection = Nothing
                Dim comm As OdbcCommand = Nothing
                Dim dr As OdbcDataReader

                Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

                Dim sql As String = "Select wo_qty_comp,wo_qty_ord from PUB.wo_mstr where wo_domain='" & strPlantCode & "' "
                sql &= " and wo_nbr='" & strNbr & "' "
                sql &= " and wo_lot='" & strLot & "' "

                Try
                    conn = New OdbcConnection(connectionString)
                    conn.Open()
                    comm = New OdbcCommand(sql, conn)
                    dr = comm.ExecuteReader()
                    If (dr.Read()) Then
                        If Not IsDBNull(dr.GetValue(0)) Then
                            drow.Item("g_qad") = dr.GetValue(0)
                            drow.Item("g_ord") = dr.GetValue(1)
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

                drow.Item("g_diff") = drow.Item("g_qty") - drow.Item("g_qad")
                drow.Item("g_part") = strPart

                dtl.Rows.Add(drow)

            End If

            Dim dvw As DataView
            dvw = New DataView(dtl)

            If (Session("orderby").Length <= 0) Then
                Session("orderby") = "g_fail"
            End If
            Try
                dvw.Sort = Session("orderby") & Session("orderdir")
                Datagrid1.DataSource = dvw
                Datagrid1.DataBind()
            Catch
            End Try

        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            'Dim str As String = ""
            'If e.CommandName.CompareTo("proc_detail") = 0 Then
            '    str = "ws_display_detail.aspx?dd=" & txb_date.Text & "&cc=" & e.Item.Cells(3).Text & "&site=" & e.Item.Cells(0).Text & "&line=" & e.Item.Cells(1).Text & "&part=" & txb_part.Text
            '    Response.Redirect(str)
            'ElseIf e.CommandName.CompareTo("proc_statistics") = 0 Then
            '    str = "ws_statistics.aspx?dd=" & txb_date.Text & "&cc=" & e.Item.Cells(3).Text & "&site=" & e.Item.Cells(0).Text & "&line=" & e.Item.Cells(1).Text & "&part=" & txb_part.Text
            '    Response.Redirect(str)
            'End If
        End Sub

        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub

        Private Sub LoadCC()
            While ddl_cc.Items.Count > 0
                ddl_cc.Items.RemoveAt(0)
            End While

            If ddl_site.SelectedIndex = 0 Then
                Exit Sub
            End If

            Dim ls As ListItem
            Dim StrSql As String
            Dim reader As SqlDataReader

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            ddl_cc.Items.Add(ls)

            If ddl_site.SelectedValue = 1 Then
                StrSql = "select ls_cc_no,ls_cc_name  from SZXWS.LineData_SZX.dbo.ls_cc where ls_cc_plant='" & ddl_site.SelectedValue & "'"
            ElseIf ddl_site.SelectedValue = 2 Then
                StrSql = "select ls_cc_no,ls_cc_name  from ZQLWS.LineData_ZQL.dbo.ls_cc where ls_cc_plant='" & ddl_site.SelectedValue & "'"
            Else
                StrSql = "select ls_cc_no,ls_cc_name  from YQLWS.LineData_YQL.dbo.ls_cc where ls_cc_plant='" & ddl_site.SelectedValue & "'"
            End If
            'Response.Write(strSql)
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                ddl_cc.Items.Add(ls)
            End While
            reader.Close()
        End Sub


        Protected Sub ddl_site_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_site.SelectedIndexChanged
            LoadCC()
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub Datagrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Datagrid1.SortCommand
            Session("orderby") = e.SortExpression.ToString()
            If Session("orderdir") = " ASC" Then
                Session("orderdir") = " DESC"
            Else
                Session("orderdir") = " ASC"
            End If
            BindData()
        End Sub


    End Class

End Namespace













