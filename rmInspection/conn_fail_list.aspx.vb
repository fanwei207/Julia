Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class conn_fail_list
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim item As ListItem
        Dim nRet As Integer 
        Protected WithEvents btnNew As System.Web.UI.WebControls.Button



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

            If Not IsPostBack Then 
                ddl_Company.SelectedIndex = 0
                StrSql = "SELECT plantID,description From tcpc0.dbo.plants where plantID<10 order by plantid"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            ddl_Company.Items.Add(item)
                        Next
                        ddl_Company.SelectedValue = Session("PlantCode")
                    End If
                End With
                ds.Reset()

                If Session("uRole") = 1 Then
                    ddl_Company.Enabled = True
                End If

                'txtDate1.Text = Format(DateTime.Today, "yyyy-MM-01")
                'txtDate2.Text = Format(DateTime.Today, "yyyy-MM-dd")
                Textbox1.Text = 25
                BindData()
            End If
        End Sub

        Function createSQL() As String
            Dim mk As Decimal
            StrSql = "select conn_rate from KnowDB.dbo.inner_conn_system where conn_type=N'迟回签'"
            mk = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
            If mk = Nothing Then
                mk = 0
            End If

            Dim hr As Integer
            'StrSql = "select conn_rate from KnowDB.dbo.conn_system where conn_type=N'最小小时差'"
            'hr = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
            'If hr = Nothing Then
            '    hr = 0
            'End If
            Try
                hr = CInt(Textbox1.Text)
            Catch
                hr = 25
            End Try
            createSQL = "  select a.conn_mstr_id,a.conn_taken_id,a.conn_taken_name,a.conn_dept,a.conn_content,a.conn_date, a.conn_taken_date,a.hs, " & mk & ",a.conn_closeddate "
            createSQL &= " from(select t.conn_mstr_id,t.conn_taken_id,t.conn_taken_name,t.conn_dept,t.conn_date, t.conn_taken_date,case when t.conn_diff is null and m.conn_closeddate is null then DATEDIFF(hh,t.conn_date,getdate()) when t.conn_diff is null and m.conn_closeddate is not null then DATEDIFF(hh,t.conn_date,m.conn_closeddate) else t.conn_diff end as hs,m.conn_content,m.conn_closeddate "
            createSQL &= " from KnowDB.dbo.inner_conn_taken t Inner Join KnowDB.dbo.inner_conn_mstr m on m.conn_mstr_id=t.conn_mstr_id "
            createSQL &= " where m.conn_deleteddate is null and t.conn_type=1 "
            createSQL &= " and t.conn_plant='" & ddl_Company.SelectedValue & "' "

            If txb_uname.Text.Trim.Length > 0 Then
                createSQL &= " and t.conn_taken_name like N'%" & txb_uname.Text.Trim & "%' "
            End If

            If txb_dept.Text.Trim.Length > 0 Then
                createSQL &= " and t.conn_dept like N'%" & txb_dept.Text.Trim & "%' "
            End If

            If txtCode.Text.Trim.Length > 0 Then
                createSQL &= " and LOWER(m.conn_content) like N'%" & chk.sqlEncode(txtCode.Text.Trim.ToLower) & "%' "
            End If
            If txtDate1.Text.Trim.Length > 0 Then
                If IsDate(txtDate1.Text) Then
                    createSQL &= " and m.conn_date>= '" & CDate(txtDate1.Text) & "' "
                End If
            End If
            If txtDate2.Text.Trim.Length > 0 Then
                If IsDate(txtDate2.Text) Then
                    createSQL &= " and m.conn_date<= '" & CDate(txtDate2.Text).AddDays(1) & "' "
                End If
            End If
            createSQL &= " ) as a where a.hs >= '" & hr & "' order by a.hs desc"
        End Function

        Sub BindData()
             
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())
             
            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("mstrid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("userid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("docuser", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("doccont", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docdate1", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docdate2", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docmark", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docdept", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docdate3", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("mstrid") = .Rows(i).Item(0)
                        drow.Item("userid") = .Rows(i).Item(1)
                        drow.Item("docuser") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("docdept") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("doccont") = .Rows(i).Item(4).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(5)) = False Then
                            drow.Item("docdate") = Format(.Rows(i).Item(5), "yyMMdd hh:mm:ss")
                        Else
                            drow.Item("docdate") = ""
                        End If
                        If IsDBNull(.Rows(i).Item(6)) = False Then
                            drow.Item("docdate1") = Format(.Rows(i).Item(6), "yyMMdd hh:mm:ss")
                        Else
                            drow.Item("docdate1") = ""
                        End If
                        drow.Item("docdate2") = Format(.Rows(i).Item(7), "##0.##")
                        drow.Item("docmark") = Format(.Rows(i).Item(8), "##0.##")
                        If IsDBNull(.Rows(i).Item(9)) = False Then
                            drow.Item("docdate3") = Format(.Rows(i).Item(9), "yyMMdd hh:mm:ss")
                        Else
                            drow.Item("docdate3") = ""
                        End If 

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
        Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
            BindData()
        End Sub

        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click 
            Dim hr As Integer 
            Try
                hr = CInt(Textbox1.Text)
            Catch
                hr = 25
            End Try

            Dim EXHeader As String = "违规内部任务联系单 小时差（大于）：" & hr.ToString() & "    导出时间" & Format(Now, "yy-MM-dd  hh:mm:ss")
            Dim EXTitle As String = "<b>姓名</b>~^<b>部门</b>~^350^<b>内容</b>~^<b>提交日期</b>~^<b>回签日期</b>~^<b>小时差</b>~^<b>扣分</b>~^<b>关闭日期</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)
        End Sub

        Private Sub ddl_Company_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddl_Company.SelectedIndexChanged
            BindData()
        End Sub

        Private Sub Datagrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("docview") = 0 Then
                ltlAlert.Text = "window.open('conn_detail_list.aspx?mid=" & e.Item.Cells(0).Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') "
            End If
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged

            Datagrid1.CurrentPageIndex = e.NewPageIndex

            BindData()
        End Sub
    End Class

End Namespace













