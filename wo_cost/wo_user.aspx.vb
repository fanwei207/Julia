Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Namespace tcpc
    Partial Class wo_user
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
                txb_sdate.Text = Format(DateTime.Today, "yyyy-MM-01")
                txb_edate.Text = Format(DateTime.Today, "yyyy-MM-dd")


                Dim reader As SqlDataReader
                StrSql = "SELECT qad_site FROM Domain_Mes WHERE plantCode='" & Session("Plantcode") & "' ORDER BY qad_site "
                reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
                While reader.Read
                    item = New ListItem(reader(0).ToString(), reader(0).ToString())
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
            Dim dm As String = ""

            Dim user_cost As Decimal = 0
            Dim tsite As String = ""
            Dim tcc As String = ""
            Dim tuserid As String = ""
            Dim tuserno As String = ""
            Dim tusername As String = ""
            Dim tnbr As String = ""
            Dim tnbrid As String = ""



            StrSql = " select cd.wocd_userid,cd.wocd_site,cd.wocd_cc,cd.wocd_user_no,cd.wocd_username, SUM(CASE WHEN ISNULL(cd.wocd_type,'')='' THEN Round(isnull(cd.wocd_cost,0)* 1.02,5) ELSE isnull(cd.wocd_cost,0) END) as cost "
            StrSql &= " FROM wo_cost_detail cd "
            If Session("uRole") <> 1 Then
                StrSql &= " INNER JOIN tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If
            StrSql &= " Left Outer Join tcpc0.dbo.Wo_Rate t ON Year(t.workdate) = '" & Year(CDate(txb_sdate.Text)) & "' And Month(t.workdate) = '" & Month(CDate(txb_sdate.Text)) & "' And t.plantcode='" & Session("plantcode") & "' "
            StrSql &= " WHERE cd.id IS NOT NULL  "
            StrSql &= " AND cd.wocd_date >= '" & txb_sdate.Text & "' AND cd.wocd_date <='" & txb_edate.Text & " '"
            If txb_no.Text.Trim.Length > 0 Then
                StrSql &= " AND cd.wocd_user_no ='" & txb_no.Text.Trim() & "' "
            End If
            If txb_name.Text.Trim.Length > 0 Then
                StrSql &= " AND cd.wocd_username =N'" & txb_name.Text.Trim() & "' "
            End If
            If dd_cc.SelectedIndex > 0 Then
                StrSql &= " AND cd.wocd_cc ='" & dd_cc.SelectedValue & "' "
            End If
            If dd_site.SelectedIndex > 0 Then
                StrSql &= " AND cd.wocd_site ='" & dd_site.SelectedValue & "' "
            End If
            StrSql &= " GROUP BY cd.wocd_site,cd.wocd_cc,cd.wocd_userid,cd.wocd_user_no,cd.wocd_username "
            StrSql &= " ORDER by cd.wocd_userid "

            Session("EXSQL") = StrSql

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_rep", System.Type.GetType("System.Decimal")))

            Dim total_lb As Decimal = 0

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("wo_site") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("wo_cc") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("wo_no") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("wo_name") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("wo_rep") = Format(.Rows(i).Item(5), "##0.##")
                        total_lb = total_lb + .Rows(i).Item(5)
                        dtl.Rows.Add(drow)

                    Next
                End If
            End With
            ds.Reset()

            lbl_total.Text = "合计工时:" & Format(total_lb, "##0.##")

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()

            Session("EXHeader") = " <b>工时:</b>" & total_lb.ToString()
            Session("EXSQL") = StrSql
            Session("EXTitle") = "60^<b>地点</b>~^<b>成本中心</b>~^<b>工号</b>~^<b>姓名</b>~^<b>工单工时</b>~^"
        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("wo_cc_list") = 0 Then
                ltlAlert.Text = "var w=window.open('wo_user_export2.aspx?uid=" & e.Item.Cells(0).Text & "&sy=" & txb_sdate.Text & "&ey=" & txb_edate.Text & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
            'ltlAlert.Text = "var w=window.open('wo_user_export1.aspx?site=" & dd_site.SelectedValue & "&cc=" & dd_cc.SelectedValue & "&sy=" & txb_sdate.Text & "&ey=" & txb_edate.Text & "&no=" & txb_no.Text & "&nm=" & txb_name.Text & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
            'Response.Redirect("/public/exportexcel.aspx")
            ltlAlert.Text = "window.open('/public/exportexcel.aspx')"
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub btnExportUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportUser.Click



            StrSql = " SELECT CASE WHEN c.wocd_userid Is Null THEN a.userID ELSE c.wocd_userid END As userid, "
            StrSql &= " CASE WHEN c.wocd_user_no Is Null THEN a.userno ELSE c.wocd_user_no END As userNo, "
            StrSql &= " CASE WHEN c.wocd_username Is Null THEN a.username ELSE c.wocd_username END As userName, "
            StrSql &= " CASE WHEN c.wocd_date Is Null THEN a.workdate ELSE c.wocd_date END As wdate, "
            StrSql &= "  ISNULL(c.cost,0),ISNULL(a.totalhr,0)  "
            StrSql &= " FROM "
            StrSql &= " (SELECT cd.wocd_userid,cd.wocd_user_no,cd.wocd_username,cd.wocd_date,SUM(CASE WHEN ISNULL(cd.wocd_type,'')='' THEN Round(isnull(cd.wocd_cost,0)* 1.02,5) ELSE isnull(cd.wocd_cost,0) END) as cost FROM wo_cost_detail cd "
            If Session("uRole") <> 1 Then
                StrSql &= " INNER JOIN tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If
            StrSql &= " Left Outer Join tcpc0.dbo.Wo_Rate t ON Year(t.workdate) = '" & Year(CDate(txb_sdate.Text)) & "' And Month(t.workdate) = '" & Month(CDate(txb_sdate.Text)) & "' And t.plantcode='" & Session("plantcode") & "' "
            StrSql &= " WHERE cd.id IS NOT NULL  "
            StrSql &= " AND cd.wocd_date >= '" & txb_sdate.Text & "' AND cd.wocd_date <='" & txb_edate.Text & " '"
            StrSql &= " GROUP BY cd.wocd_userid,cd.wocd_user_no,cd.wocd_username,cd.wocd_date) c "
            StrSql &= "  Full Outer Join "
            StrSql &= "  (SELECT userID,CONVERT(varchar(10), starttime, 120) as workdate,Sum(totalhr) as totalhr,userno,username From tcpc0.dbo.hr_Attendance_calendar WHERE starttime >= '" & txb_sdate.Text & "' AND starttime <='" & txb_edate.Text & "' AND plantid='" & Session("plantcode") & "' "
            StrSql &= "   Group by userid,CONVERT(varchar(10), starttime, 120),userno,username )  a ON a.userID = c.wocd_userid And a.workdate =c.wocd_date "


            StrSql &= " ORDER by wdate,userid "

            Session("EXSQL1") = StrSql
            Session("EXTitle1") = "<b>日期</b>~^<b>工号</b>~^<b>姓名</b>~^100^<b>工时</b>~^100^<b>考勤</b>~^"
            ltlAlert.Text = "window.open('/public/exportexcel1.aspx')"
        End Sub

    End Class

End Namespace













