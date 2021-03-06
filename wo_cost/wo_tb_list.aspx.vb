Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Namespace tcpc
    Partial Class wo_tb_list
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
                End Select

                ls = New ListItem
                ls.Value = 0
                ls.Text = "--"
                dd_cc.Items.Add(ls)

                Dim conn As OdbcConnection = Nothing
                Dim comm As OdbcCommand = Nothing
                Dim dr As OdbcDataReader

                Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;
                Dim strPlant As String = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, "Select PlantCode From plants Where plantID = " & Session("PlantCode"))
                Dim sql As String = "Select cc_ctr, cc_desc from PUB.cc_mstr where cc_domain='" & strPlant & "' and cc_active=1 "
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
                        dd_cc.Items.Add(ls)


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
            Select Case dd_site.SelectedValue
                Case "1000"
                    dm = "szx"
                Case "1200"
                    dm = "szx"
                Case "1400"
                    dm = "szx"
                Case "2000"
                    dm = "zql"
                Case "2100"
                    dm = "zql"
                Case "3000"
                    dm = "zqz"
                Case "4000"
                    dm = "yql"
                Case "5000"
                    dm = "tcb"
                Case "6000"
                    dm = "ytc"
                Case "7000"
                    dm = "zfx"
                Case "8000"
                    dm = "zjn"
                Case "9000"
                    dm = "thk"
                Case "a000"
                    dm = "jql"
                Case "b000"
                    dm = "sfx"
                Case "c000"
                    dm = "hql"
            End Select

            Dim intYear = 2000 + CInt(txb_date.Text.Substring(0, 2))
            Dim intMonth = CInt(txb_date.Text.Substring(2, 2))
            Dim bi_work1 As String
            Dim bi_night(2) As Integer
            Array.Clear(bi_night, 0, 3)
            bi_work1 = "0"
            bi_night(0) = 0
            bi_night(1) = 0
            bi_night(2) = 0

            StrSql = "SELECT bi_work1,bi_night1 FROM hr_bi_mstr WHERE WorkYear='" & intYear & "' AND WorkMonth = '" & intMonth & "' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While reader.Read()
                bi_work1 = reader(0).ToString()
                '//确定基础工资信息里的中夜班

                Select Case reader(1)
                    Case 0
                        bi_night(0) = 1  '// 中班
                    Case 1
                        bi_night(1) = 1  '// 夜班
                    Case 2
                        bi_night(2) = 1  '// 全夜
                End Select

            End While
            reader.Close()

            StrSql = " SELECT aa.tb_site,aa.tb_cc,u.userID,u.userNo,u.userName,aa.tbwork, aa.tbdays ,ISNULL(d.name,''),ISNULL(w.name,''),aa.mid,aa.night,aa.whole "
            StrSql &= "  FROM (SELECT a.tb_site,a.tb_cc,a.tb_user_id,SUM(a.tb_work) As tbwork,SUM(a.tb_mid) As mid,SUM(CASE WHEN a.tb_work >= 8 THEN 1 ELSE ROUND(a.tb_work/8,2) END) AS tbdays,SUM(a.tb_night) As night,SUM(a.tb_whole) As whole FROM "
            StrSql &= " wo_timebook a "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid= a.tb_cc  "
            End If
            StrSql &= " WHERE a.deletedBy is null AND YEAR(a.tb_date) = '" & intYear & "' AND MONTH(a.tb_date) = '" & intMonth & "' "
            If txtday.Text.Trim.Length > 0 Then
                StrSql &= " AND  DAY(a.tb_date) = '" & txtday.Text.Trim & "' "
            End If

            If txb_no.Text.Trim.Length > 0 Then
                StrSql &= " AND  a.tb_user_no ='" & txb_no.Text.Trim() & "' "
            End If

            If txb_name.Text.Trim.Length > 0 Then
                StrSql &= " AND  a.tb_user_name =N'" & txb_name.Text.Trim() & "' "
            End If
            StrSql &= " GROUP BY a.tb_site,a.tb_cc,a.tb_user_id ) aa "

            StrSql &= " INNER JOIN tcpc0.dbo.Users u ON u.userID = aa.tb_user_id "
            StrSql &= " LEFT OUTER JOIN departments d ON d.departmentID = u.departmentID "
            StrSql &= " LEFT OUTER JOIN Workshop w ON w.id = u.workshopID "
            StrSql &= " ORDER BY aa.tb_site,u.userID "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_rep", System.Type.GetType("System.String")))

            dtl.Columns.Add(New DataColumn("wo_mid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("wo_night", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("wo_whole", System.Type.GetType("System.Int32")))

            dtl.Columns.Add(New DataColumn("wo_department", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_workshop", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_day", System.Type.GetType("System.String")))
            Dim total_lb As Decimal = 0

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("wo_site") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("wo_cc") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("id") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("wo_no") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("wo_name") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("wo_rep") = Format(.Rows(i).Item(5), "##0.##")

                        drow.Item("wo_day") = Format(.Rows(i).Item(6), "##0.##")
                        drow.Item("wo_department") = .Rows(i).Item(7)
                        drow.Item("wo_workshop") = .Rows(i).Item(8)

                        drow.Item("wo_mid") = .Rows(i).Item(9)
                        drow.Item("wo_night") = .Rows(i).Item(10)
                        drow.Item("wo_whole") = .Rows(i).Item(11)
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
            If e.CommandName.CompareTo("wo_cc_list") = 0 Then
                ltlAlert.Text = "var w=window.open('wo_tb_export2.aspx?site=" & dd_site.SelectedValue & "&uid=" & e.Item.Cells(0).Text & "&yy=" & txb_date.Text & "&dy=" & txtday.Text.Trim & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('wo_tb_export1.aspx?site=" & dd_site.SelectedValue & "&cc=" & dd_cc.SelectedValue & "&yy=" & txb_date.Text & "&no=" & txb_no.Text & "&nm=" & txb_name.Text & "&dy=" & txtday.Text.Trim & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













