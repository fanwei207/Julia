Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class perf_employee_list
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
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

                btn_del.Attributes.Add("onclick", "return confirm('小心！确定要删除所有的记录吗?');")

                BindData()

            End If
        End Sub

        Function createSQL() As String
            createSQL = "select perf_userid,perf_userno,perf_uname,perf_dept,perf_rate,perf_createdby,perf_createddate from perf_employee "
            createSQL &= " where  perf_userid>0 "
            If txb_no.Text.Trim.Length > 0 Then
                createSQL &= " and perf_userno='" & txb_no.Text.Trim & "'"
            End If
            If txb_name.Text.Trim.Length > 0 Then
                createSQL &= " and perf_uname like N'%" & txb_name.Text.Trim & "%'"
            End If

            If dd_dept.SelectedIndex > 0 Then
                createSQL &= " and perf_dept =N'" & dd_dept.SelectedItem.Text & "'"
            End If

            createSQL &= " order by perf_dept,perf_userno"
        End Function

        Sub BindData()

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())

            Dim total As Integer = 0

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("userno", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("uname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("dept", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("rate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("appr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("apprdate", System.Type.GetType("System.String")))


            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("userno") = .Rows(i).Item(1).ToString()
                        drow.Item("uname") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("dept") = .Rows(i).Item(3).ToString().Trim()

                        drow.Item("user_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("rate") = Format(.Rows(i).Item(4), "##0.##")
                        drow.Item("appr") = .Rows(i).Item(5).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(6)) Then
                            drow.Item("apprdate") = ""
                        Else
                            drow.Item("apprdate") = Format(.Rows(i).Item(6), "yy-MM-dd")
                        End If

                        total = total + 1
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()

            Label1.Text = "总数：" & total.ToString()

        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("perf_del") = 0 Then

                If (e.Item.Cells(5).Text.Trim() = Session("uName")) Then
                    StrSql = "Delete from perf_employee where perf_userid='" & e.Item.Cells(0).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

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

        Private Sub btn_act_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_act.Click
            Response.Redirect("perf_employee.aspx")
        End Sub

        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
            
            Dim EXHeader As String = "奖罚率维护     导出日期：" & Format(DateTime.Today, "yyyy-MM-dd")
            Dim EXTitle As String = "<b>工号</b>~^<b>姓名</b>~^120^<b>部门</b>~^<b>奖罚率</b>~^<b>创建人</b>~^<b>创建日期</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)


        End Sub

        Private Sub btn_del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_del.Click
            StrSql = "Delete from perf_employee where perf_userid>0 "
            If Session("uRole") > 1 Then
                StrSql &= "  and perf_createdby=N'" & Session("uName") & "'"
            End If
            If txb_no.Text.Trim.Length > 0 Then
                StrSql &= " and perf_userno='" & txb_no.Text.Trim & "'"
            End If
            If txb_name.Text.Trim.Length > 0 Then
                StrSql &= " and perf_uname like N'%" & txb_name.Text.Trim & "%'"
            End If

            If dd_dept.SelectedIndex > 0 Then
                StrSql &= " and perf_dept =N'" & dd_dept.SelectedItem.Text & "'"
            End If

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            BindData()

        End Sub

        Private Sub dd_dept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dd_dept.SelectedIndexChanged
            BindData()
        End Sub

        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub
    End Class

End Namespace













