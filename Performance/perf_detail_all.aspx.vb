Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

Partial Class perf_detail_all
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
    Protected WithEvents btn_fine As System.Web.UI.WebControls.Button

    Dim nRet As Integer
    Dim item As ListItem


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub



    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("89500120", "考评维护")
            End If

        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not IsPostBack Then

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

        Private Function createSQL() As String
            createSQL = "select d.perf_detail_id,d.perf_mstr_id,d.perf_createdby,d.perf_userno,d.perf_uname,d.perf_dept,d.perf_createddate,m.perf_notes, d.perf_cause,d.perf_mark,d.perf_note,isnull(d.perf_duty,0),d.perf_createdname "
            createSQL &= " from perf_details d Inner join Perf_mstr m on m.perf_mstr_id=d.perf_mstr_id  where d.perf_deleted is null and d.perf_hist_id is null "
            If txb_no.Text.Trim.Length > 0 Then
                createSQL &= " and d.perf_userno='" & txb_no.Text.Trim() & "' "
            End If
            If txb_name.Text.Trim.Length > 0 Then
                createSQL &= " and d.perf_uname like N'%" & txb_name.Text.Trim() & "%' "
            End If
            If dd_dept.SelectedIndex > 0 Then
                createSQL &= " and d.perf_dept=N'" & dd_dept.SelectedItem.Text.Trim() & "' "
            End If
            createSQL &= " order by d.perf_detail_id desc"
        End Function
        Sub BindData()

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())

            Dim tot As Decimal = 0

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("perf_detail_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("uno", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("un", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("dp", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("cdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("cause", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("mark", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("note", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("notes", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("oper", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("duty", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("mstr_id", System.Type.GetType("System.String")))


            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("perf_detail_id") = .Rows(i).Item(0)
                        drow.Item("mstr_id") = .Rows(i).Item(1)
                        drow.Item("user_id") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("uno") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("un") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("dp") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("cdate") = Format(.Rows(i).Item(6), "yyyy-MM-dd")
                        drow.Item("cause") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("notes") = .Rows(i).Item(8).ToString().Trim()
                        drow.Item("mark") = Format(.Rows(i).Item(9), "##0.####")
                        drow.Item("note") = .Rows(i).Item(10).ToString().Trim()
                        drow.Item("oper") = .Rows(i).Item(12).ToString().Trim()
                        drow.Item("duty") = .Rows(i).Item(11).ToString().Trim()

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
        If e.CommandName.CompareTo("perf_del") = 0 Then

                If Not Me.Security("89500120").isValid Then
                    ltlAlert.Text = "alert('没有删除的权限！')"
                    Exit Sub
                End If

            If (e.Item.Cells(2).Text.Trim() = Session("uID")) Then
                StrSql = "Update perf_details set perf_deleted=N'" & Session("uName") & "' where perf_hist_id is null and perf_deleted is null and perf_detail_id='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

                BindData()
            Else
                ltlAlert.Text = "alert('只能删除你录入的数据！')"
                Exit Sub
            End If
        End If
    End Sub

    Private Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
        Datagrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub


    Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click 
            Dim EXHeader As String = "考评分按时间显示详细记录,    导出日期:" & Format(Today, "yy-MM-dd")
            Dim EXTitle As String = "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>日期</b>~^350^<b>原因</b>~^320^<b>说明</b>~^50^<b>扣分</b>~^320^<b>描述</b>~^<b>责任</b>~^50^<b>考评人</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)
        End Sub
    Private Sub btn_back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_back.Click
        Response.Redirect("perf_list.aspx?uid=" & Request("uid"))
    End Sub

    Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
        BindData()
    End Sub
End Class

End Namespace













