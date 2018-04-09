Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

Partial Class perf_rewards
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal 
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
            BindData()
        End If
    End Sub

        Private Function createSQL() As String
            createSQL = " Select e.perf_userid,m.perf_mark,e.perf_userno,e.perf_uname,e.perf_dept,t.perf_rewards"
            createSQL &= " from perf_employee e Inner Join tcpc0.dbo.perf_type t on t.perf_type=e.perf_type and t.perf_rewards is not null"
            createSQL &= " left outer join perf_mstr m on m.perf_user_id = e.perf_userid and m.perf_status<>N'关闭' and m.perf_mark > 0 "
            createSQL &= " where m.perf_mark is null "
            createSQL &= " order by e.perf_dept,e.perf_userno "

        End Function

        Sub BindData()
             
            Dim StrSql As String = createSQL()
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            'Dim total As Integer

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("perf_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("perf_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("perf_dept", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("perf_fine", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.Int32")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("perf_no") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("perf_name") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("perf_dept") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("perf_fine") = Format(.Rows(i).Item(5), "##0.####")
                        drow.Item("user_id") = .Rows(i).Item(0)

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

        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click 
            Dim EXHeader As String = "绩效考核奖励     导出日期：" & Format(DateTime.Today, "yyyy-MM-dd")
            Dim EXTitle As String = "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>奖励</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)

        End Sub

        Private Sub btn_back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_back.Click
            Response.Redirect("perf_mstr.aspx")
        End Sub
    End Class

End Namespace













