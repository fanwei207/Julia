Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class perf_detail
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Protected WithEvents btn_fine As System.Web.UI.WebControls.Button

        Dim nRet As Integer


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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here

            If Not IsPostBack Then

                If Request("uid") <> Nothing Then
                    lbl_userid.Text = Request("uid")
                    Label2.Text = Request("st")


                    If Request("uno") = Nothing Then
                        StrSql = "select u.userNo,u.userName,d.name "
                        StrSql &= " from tcpc0.dbo.users u Inner Join departments d on d.departmentID=u.departmentID "
                        StrSql &= " where u.userid ='" & Request("uid") & "' "
                        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                        With ds.Tables(0)
                            If .Rows.Count > 0 Then
                                lbl_userno.Text = .Rows(0).Item(0).ToString().Trim()
                                lbl_username.Text = .Rows(0).Item(1).ToString().Trim()
                                lbl_dept.Text = .Rows(0).Item(2).ToString().Trim()
                            End If
                        End With
                        ds.Reset()
                    Else
                        lbl_userno.Text = Request("uno")
                        lbl_username.Text = Request("na")
                        lbl_dept.Text = Request("dp")
                    End If
                    BindData()

                End If
            End If
        End Sub

        Sub BindData()
            StrSql = "select perf_detail_id,perf_cause,perf_mark,perf_note,perf_createdname,perf_createddate,perf_createdby,isnull(perf_duty,0),perf_mstr_id "
            StrSql &= " from perf_details where perf_deleted is null and perf_hist_id is null and perf_userid ='" & Request("uid") & "' "
            StrSql &= " order by perf_createddate"

            Session("EXTitle") = "<b>日期</b>~^350^<b>原因</b>~^320^<b>说明</b>~^50^<b>扣分</b>~^320^<b>描述</b>~^<b>责任</b>~^50^<b>考评人</b>~^"
            Session("EXSQL") = "select d.perf_createddate,d.perf_cause,m.perf_notes,d.perf_mark,d.perf_note,d.perf_duty,d.perf_createdname "
            Session("EXSQL") &= " from perf_details d Inner Join Perf_mstr m on m.perf_mstr_id=d.perf_mstr_id where d.perf_deleted is null and d.perf_hist_id is null and d.perf_userid ='" & Request("uid") & "' "
            Session("EXSQL") &= " order by d.perf_createddate"
            Session("EXHeader") = "工号:" & lbl_userno.Text & "  姓名:" & lbl_username.Text & "  部门:" & lbl_dept.Text & "  导出日期:" & Format(Today, "yy-MM-dd")

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim tot As Decimal = 0

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("perf_detail_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("cdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("cause", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("mark", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("note", System.Type.GetType("System.String")))
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
                        drow.Item("cdate") = Format(.Rows(i).Item(5), "yyyy-MM-dd")
                        drow.Item("cause") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("mark") = Format(.Rows(i).Item(2), "##0.####")
                        drow.Item("note") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("oper") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("user_id") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("duty") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("mstr_id") = .Rows(i).Item(8)


                        tot = tot + .Rows(i).Item(2)
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()

            Label1.Text = "累计扣分：" & Format(tot, "##0.####")

            If Request("st") <> "关闭" Then
                If tot < 0 Then
                    Label2.Text = "良好"
                ElseIf tot < 40 Then
                    Label2.Text = "较轻"
                ElseIf tot < 60 Then
                    Label2.Text = "较重"
                ElseIf tot < 80 Then
                    Label2.Text = "严重"
                ElseIf tot < 100 Then
                    Label2.Text = "致命"
                Else
                    Label2.Text = "除名"
                End If
            End If


        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("perf_del") = 0 Then
                If Label2.Text.Trim = "关闭" Then
                    ltlAlert.Text = "alert('此事务已关闭！')"
                    Exit Sub
                End If

                If Not Me.Security("89500120").isValid Then
                    ltlAlert.Text = "alert('没有删除的权限！')"
                    Exit Sub
                End If

                If (e.Item.Cells(9).Text.Trim() = Session("uID")) Then

                    StrSql = "Update perf_details set perf_deleted=N'" & Session("uName") & "' where perf_hist_id is null and perf_deleted is null and perf_detail_id='" & e.Item.Cells(0).Text.Trim() & "'"
                    SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

                    BindData()
                Else
                    ltlAlert.Text = "alert('只能删除你录入的数据！')"
                    Exit Sub
                End If
            ElseIf e.CommandName.CompareTo("perf_detail") = 0 Then
                ltlAlert.Text = "window.open('perf_showdetail.aspx?mid=" & e.Item.Cells(10).Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') "
            End If
        End Sub

        Private Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub


        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub
        Private Sub btn_back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_back.Click
            Response.Redirect("perf_list.aspx?uid=" & Request("uid"))
        End Sub
    End Class

End Namespace













