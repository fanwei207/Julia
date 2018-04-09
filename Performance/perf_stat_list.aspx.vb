Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc

    Partial Class perf_stat_list
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

                BindData()
            End If
        End Sub

        Function createSQL() As String
             If dd_title.SelectedIndex = 0 Then
                createSQL = " select perf_type,perf_cause,SUM(ISNULL(perf_mark,0)) as mark "
                createSQL &= " from perf_details where perf_deleted is null and perf_hist_id is null "
                createSQL &= " group by perf_type,perf_cause order by mark desc, perf_type" 
            Else
                createSQL = " select perf_type,perf_cause,SUM(ISNULL(perf_mark,0)) as mark "
                createSQL &= " from perf_details where perf_deleted is null and perf_hist_id ='" & dd_title.SelectedValue & "' "
                createSQL &= " group by perf_type,perf_cause order by mark desc, perf_type" 
            End If
        End Function

        Sub BindData()

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())
             
            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("perf_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("perf_dept", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("perf_mark", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("perf_no") = .Rows(i).Item(0)
                        drow.Item("perf_dept") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("perf_mark") = Format(.Rows(i).Item(2), "##0.####")
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
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
 
            Dim EXHeader As String = "考评统计年月：" & dd_title.SelectedValue & "      导出日期：" & Format(DateTime.Today, "yyyy-MM-dd")
            Dim EXTitle As String = "<b>分类</b>~^350^<b>原因</b>~^<b>累计扣分</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)

        End Sub

        Private Sub dd_title_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dd_title.SelectedIndexChanged
            BindData()
        End Sub
    End Class

End Namespace













