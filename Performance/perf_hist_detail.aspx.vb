Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class perf_hist_detail
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
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            StrSql = "select perf_detail_id,perf_cause,perf_mark,perf_note,perf_createdname,perf_createddate,perf_createdby,isnull(perf_duty,0) "
            StrSql &= " from perf_details where perf_deleted is null and perf_hist_id='" & Request("hid") & "' and perf_userid ='" & Request("uid") & "' "
            StrSql &= " order by perf_createddate"

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

                        tot = tot + .Rows(i).Item(2)
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            If tot = 0 Then
                Response.Redirect("perf_list.aspx?uid=" & Request("uid"))
            End If

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
        Private Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
 
            Dim EXHeader As String = "工号:" & lbl_userno.Text & "  姓名:" & lbl_username.Text & "  部门:" & lbl_dept.Text & "  导出日期:" & Format(Today, "yy-MM-dd")
            Dim EXTitle As String = "<b>日期</b>~^350^<b>原因</b>~^320^<b>说明</b>~^50^<b>扣分</b>~^320^<b>描述</b>~^<b>责任</b>~^50^<b>考评人</b>~^"
            Dim ExSql As String
            ExSql = "select d.perf_createddate,d.perf_cause,m.perf_notes,d.perf_mark,d.perf_note,d.perf_duty,d.perf_createdname "
            ExSql &= " from perf_details d Inner Join Perf_mstr m on m.perf_mstr_id=d.perf_mstr_id where d.perf_deleted is null and d.perf_hist_id='" & Request("hid") & "' and d.perf_userid ='" & Request("uid") & "' "
            ExSql &= " order by d.perf_createddate"
            Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)

             End Sub
        Private Sub btn_back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_back.Click
            Response.Redirect("perf_hist_list.aspx?hid=" & Request("hid"))
        End Sub
    End Class

End Namespace













