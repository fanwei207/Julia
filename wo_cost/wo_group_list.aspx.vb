Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class wo_group_list
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
                BindData()
            End If
        End Sub
        Sub BindData()
            StrSql = " Select wod_id,wod_site,wod_cc,wod_group_name,wod_user_name + '-' + wod_user_no,isnull(wod_rate,1),createdDate,createdBy from tcpc0.dbo.wo_group_detail where wod_group_id='"
            StrSql &= Request("gid") & "' "
            StrSql &= " order by wod_site,wod_cc,wod_user_name"

            Session("EXTitle1") = "50^<b>地点</b>~^80^<b>成本中心</b>~^130^<b>用户组</b>~^150^<b>组员</b>~^<b>岗位系数</b>~^<b>日期</b>~^50^<b>创建人</b>~^"
            Session("EXSQL1") = StrSql
            Session("EXHeader1") = "用户组员   导出日期: " & Format(DateTime.Today, "yyyy-MM-dd")

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("group_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_by", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("proc_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_member", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_rate", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("group_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("group_site") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("group_cc") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("group_name") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("proc_by") = .Rows(i).Item(7).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(6)) Then
                            drow.Item("proc_date") = Format(.Rows(i).Item(6), "yy-MM-dd")
                        End If
                        drow.Item("group_member") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("group_rate") = Format(.Rows(i).Item(5), "##0.##")
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
            If e.CommandName.CompareTo("proc_del") = 0 Then
                StrSql = " Delete from tcpc0.dbo.wo_group_detail where wod_id='" & e.Item.Cells(0).Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                BindData()
            End If
        End Sub
        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            Response.Redirect("wo_group.aspx?rt=" + DateTime.Now.ToString())
        End Sub

        Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('/public/exportExcel1.aspx" & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"
        End Sub

        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.CancelCommand
            Datagrid1.EditItemIndex = -1
            BindData()
        End Sub
        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.UpdateCommand
            Dim strSQL As String
            Dim str2 As String = CType(e.Item.Cells(5).Controls(0), TextBox).Text
            If Not IsNumeric(str2) Then
                ltlAlert.Text = "alert('请输入正确的系数.')"
                Exit Sub
            End If

            strSQL = "update tcpc0.dbo.wo_group_detail set wod_rate = '" & str2 & "'  where wod_id='" & e.Item.Cells(0).Text & "'"
            'Response.Write(strSQL)
            'Exit Sub
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

            Datagrid1.EditItemIndex = -1
            BindData()
        End Sub
        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.EditCommand
            Datagrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













