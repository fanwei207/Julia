Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    'Imports System.Data.Odbc
    Partial Class perf_mstr
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

            If Not IsPostBack Then

                Me.Security.Register("89500111", "考评奖罚报表")
                Me.Security.Register("89500112", "考评记录删除")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then

                btn_fine.Enabled = Me.Security("89500111").isValid

                btn_close.Attributes.Add("onclick", "return confirm('确定要关闭现在的考评周期,开始新的考评周期吗?');")

                BindData()
            End If
        End Sub

        Private Function createSQL() As String
            createSQL = "select m.perf_mstr_id,m.perf_act_by,isnull(m.perf_hist_id,0),m.perf_act_name,m.perf_act_date, m.perf_cause,m.perf_notes,isnull(d.mark,0) "
            createSQL &= " from perf_mstr m Left Outer Join (select perf_mstr_id,sum(isnull(perf_mark,0)) as mark from perf_details where perf_deleted is null and perf_hist_id is null group by perf_mstr_id ) d on d.perf_mstr_id = m.perf_mstr_id "
            createSQL &= " where m.perf_deletedby is null and m.perf_hist_id is null "
            If txb_name.Text.Trim.Length > 0 Then
                createSQL &= " and m.perf_act_name like N'%" & txb_name.Text.Trim() & "%' "
            End If
            If txb_date.Text.Trim.Length > 0 Then
                If IsDate(txb_date.Text.Trim) Then
                    createSQL &= " and m.perf_act_date>=" & CDate(txb_date.Text.Trim()) & " and m.perf_act_date<" & CDate(txb_date.Text.Trim()).AddDays(1)
                End If
            End If
            If txb_kword.Text.Trim.Length > 0 Then
                createSQL &= " and (m.perf_cause like N'%" & txb_kword.Text.Trim() & "%' or m.perf_notes like N'%" & txb_kword.Text.Trim() & "%') "
            End If
            createSQL &= " order by m.perf_act_date desc,m.perf_mstr_id desc"

        End Function

        Sub BindData()
            
            StrSql = createSQL() 
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim total As Integer = 0

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("sort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("perf_mstr_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("act_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("perf_hist_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("aname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("adate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("acause", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("anote", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("amark", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("sort") = i + 1
                        drow.Item("perf_mstr_id") = .Rows(i).Item(0)
                        drow.Item("act_id") = .Rows(i).Item(1)
                        drow.Item("perf_hist_id") = .Rows(i).Item(2)
                        drow.Item("aname") = .Rows(i).Item(3).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(4)) = False Then
                            drow.Item("adate") = Format(.Rows(i).Item(4), "yy-MM-dd")
                        Else
                            drow.Item("adate") = ""
                        End If
                        drow.Item("acause") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("anote") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("amark") = Format(.Rows(i).Item(7), "##0.####")

                        dtl.Rows.Add(drow)
                        total = total + 1
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
            If e.CommandName.CompareTo("perf_edit") = 0 Then
                ltlAlert.Text = "window.open('perf_showdetail.aspx?mid=" & e.Item.Cells(0).Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') "
            ElseIf e.CommandName.CompareTo("perf_act") = 0 Then
                Response.Redirect("perf_mark.aspx?mid=" & e.Item.Cells(0).Text.Trim())
            ElseIf e.CommandName.CompareTo("perf_del") = 0 Then

                StrSql = "SELECT perf_act_by FROM tcpc1..perf_mstr where perf_mstr_id='" & e.Item.Cells(0).Text.Trim() & "' and perf_hist_id is null and perf_deletedby is null"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                Dim uid As String
                uid = ""
                With ds.Tables(0)
                    If .Rows.Count > 0 Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            uid = .Rows(i).Item(0)
                            
                        Next
                    End If
                End With
                If Me.Security("89500112").isValid Then
                    StrSql = "Update perf_mstr set perf_deletedby=N'" & Session("uName") & "' where perf_mstr_id='" & e.Item.Cells(0).Text.Trim() & "' and perf_hist_id is null and perf_deletedby is null"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                    StrSql = "Update perf_details set perf_deleted=N'" & Session("uName") & "' where perf_mstr_id='" & e.Item.Cells(0).Text.Trim() & "' and perf_hist_id is null and perf_deleted is null"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                    StrSql = "DELETE  FROM  dbo.perf_punish WHERE mid = '" & e.Item.Cells(0).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                Else

                    ltlAlert.Text = "alert('你没有删除权限，不能删除。')"
                End If

              
                BindData()
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub
        Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
            If txb_title.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请输入此考评周期的标题。')"
                Exit Sub
            End If

            If Not Me.Security("89500122").isValid Then

                ltlAlert.Text = "alert('没有关闭考评周期的权限！')"
                Exit Sub
            End If

            StrSql = "perf_create_hist"
            Dim params(1) As SqlParameter
            params(0) = New SqlParameter("@title", txb_title.Text.Trim)
            params(1) = New SqlParameter("@createdname", Session("uName"))
            SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)

            BindData()
        End Sub
        Private Sub btn_action_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_action.Click
            Response.Redirect("perf_Add.aspx")
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click 
            Dim EXHeader As String = "考评记录  导出日期:" & Format(Today, "yy-MM-dd")
            'Dim EXTitle As String = "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>日期</b>~^350^<b>原因</b>~^320^<b>说明</b>~^50^<b>扣分</b>~^320^<b>描述</b>~^<b>责任</b>~^50^<b>考评人</b>~^"
            Dim EXTitle As String = "<b>姓名</b>~^<b>日期</b>~^350^<b>原因</b>~^320^<b>说明</b>~^50^<b>扣分</b>~^"
            Dim ExSql As String = createSQL()
            Dim ds As DataSet = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, ExSql)
            'Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)
            Me.ExportExcel(EXTitle, ds.Tables(0), False)
        End Sub

        Private Sub btn_fine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_fine.Click
            Response.Redirect("perf_fine.aspx?bk=mstr")
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













