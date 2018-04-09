Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc

Partial Class perf_hist_list
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
    Dim nRet As Integer
    Protected WithEvents btn_close As System.Web.UI.WebControls.Button

    Protected WithEvents btn_action As System.Web.UI.WebControls.Button
    Protected WithEvents btn_rewards As System.Web.UI.WebControls.Button
    Dim item As ListItem


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub



    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("89500111", "考评奖罚报表")
            End If

        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""
        If Not IsPostBack Then

            dd_title.SelectedIndex = 0
            item = New ListItem("--")
            item.Value = 0
            dd_title.Items.Add(item)

            StrSql = "SELECT perf_hist_id,perf_hist_desc From perf_hist order by createdate desc "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        dd_title.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()
            If Request("hid") <> Nothing Then
                dd_title.SelectedValue = Request("hid")
            End If

            dd_dept.SelectedIndex = 0
            item = New ListItem("--")
            item.Value = 0
            dd_dept.Items.Add(item)

            StrSql = "SELECT departmentID,name From departments where isnull(active,0)=1 and isnull(isSalary,0)=1 order by departmentID "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        dd_dept.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()

            BindData()
        End If
    End Sub

        Function createSQL() As String
            createSQL = "select a.perf_userid,a.perf_userno,a.perf_uname,a.perf_dept,a.mark,case when a.mark<40 then N'较轻' when a.mark<60 then N'较重' when a.mark<80 then N'严重' when a.mark<100 then N'致命' else N'除名' end "
            createSQL &= " from (select perf_userid,perf_userno,perf_uname,perf_dept,SUM(ISNULL(perf_mark,0)) as mark "
            createSQL &= " from perf_details where perf_deleted is null and perf_hist_id ='" & dd_title.SelectedValue & "' "
            If txb_no.Text.Trim.Length > 0 Then
                createSQL &= " and perf_userno='" & txb_no.Text.Trim() & "' "
            End If
            If txb_name.Text.Trim.Length > 0 Then
                createSQL &= " and perf_uname like N'%" & txb_name.Text.Trim() & "%' "
            End If
            If dd_dept.SelectedIndex > 0 Then
                createSQL &= " and perf_dept=N'" & dd_dept.SelectedItem.Text.Trim() & "' "
            End If 
            createSQL &= " group by perf_userid,perf_userno,perf_uname,perf_dept) a order by a.mark desc, a.perf_dept,a.perf_userno"
        End Function

        Sub BindData()
            
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())
           
            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("perf_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("perf_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("perf_dept", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("perf_mark", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("perf_status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.Int32")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("user_id") = .Rows(i).Item(0)
                        drow.Item("perf_no") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("perf_name") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("perf_dept") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("perf_mark") = Format(.Rows(i).Item(4), "##0.####")
                        drow.Item("perf_status") = .Rows(i).Item(5).ToString().Trim()
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
            If e.CommandName.CompareTo("perf_edit") = 0 Then
                Response.Redirect("perf_hist_detail.aspx?hid=" & dd_title.SelectedValue & "&uid=" & e.Item.Cells(0).Text.Trim() & "&uno=" & e.Item.Cells(1).Text.Trim() & "&na=" & e.Item.Cells(2).Text.Trim() & "&dp=" & e.Item.Cells(3).Text.Trim() & "&st=" & e.Item.Cells(4).Text.Trim())
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click 
            Dim EXHeader As String = "考评年月：" & dd_title.SelectedItem.Text
            Dim EXTitle As String = "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>日期</b>~^350^<b>原因</b>~^320^<b>说明</b>~^50^<b>扣分</b>~^320^<b>描述</b>~^<b>责任</b>~^50^<b>考评人</b>~^"
            Dim ExSql As String
            ExSql = "select d.perf_userno,d.perf_uname,d.perf_dept,d.perf_createddate,d.perf_cause,m.perf_notes,d.perf_mark,d.perf_note,d.perf_duty,d.perf_createdname "
            ExSql &= " from perf_details d Inner Join Perf_mstr m on m.perf_mstr_id=d.perf_mstr_id where d.perf_deleted is null and d.perf_hist_id='" & dd_title.SelectedValue & "' "
            ExSql &= " order by d.perf_mstr_id, d.perf_createddate " 
            Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)

        End Sub

    Private Sub btn_fine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_fine.Click

            If Not Me.Security("89500111").isValid Then

                ltlAlert.Text = "alert('没有查询奖罚报表的权限！')"
                Exit Sub
            End If
        Response.Redirect("perf_hist_fine.aspx?hid=" & dd_title.SelectedValue)
    End Sub

    Private Sub dd_title_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dd_title.SelectedIndexChanged
        BindData()
    End Sub

        Protected Sub Datagrid1_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













