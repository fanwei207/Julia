Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

Partial Class conn_detail_list
        Inherits System.Web.UI.Page
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
    Protected WithEvents btn_next As System.Web.UI.WebControls.Button
    Protected WithEvents btn_close As System.Web.UI.WebControls.Button
    Protected WithEvents bnt_cancel As System.Web.UI.WebControls.Button
    Dim reader As SqlDataReader

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
            If Request("mid") <> "" Then
                BindData()
            End If
        End If
    End Sub

    Sub BindData()
        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("attid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("docitem", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("docs", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("view", System.Type.GetType("System.String")))
        Dim drow As DataRow

        Dim i As Integer
        Dim txt As String = ""
        Dim reader2 As SqlDataReader
        Dim StrSql2 As String

            StrSql = "select conn_user_name, conn_dept, conn_content,conn_date,conn_closeddate from knowdb.dbo.inner_conn_mstr "
        StrSql &= " where conn_mstr_id ='" & Request("mid") & "'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "申请人"
            drow.Item("docs") = reader(0).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "部门"
            drow.Item("docs") = reader(1).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "内容"
            drow.Item("docs") = reader(2).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "申请日期"
            drow.Item("docs") = reader(3).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "关闭日期"
            drow.Item("docs") = reader(4).ToString()
            dtl.Rows.Add(drow)

        End While
        reader.Close()

            StrSql = "select distinct conn_taken_name from knowdb.dbo.inner_conn_taken "
        StrSql &= " where conn_mstr_id ='" & Request("mid") & "' and conn_type =1 and conn_detail_id is null and isnull(conn_status,0)=1 "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            txt = txt & reader(0).ToString() & ","
        End While
        reader.Close()

        drow = dtl.NewRow()
        drow.Item("attid") = 0
        drow.Item("docitem") = "提交给"
        drow.Item("docs") = txt
        dtl.Rows.Add(drow)

        txt = ""
            StrSql = "select distinct conn_taken_name from knowdb.dbo.inner_conn_taken "
        StrSql &= " where conn_mstr_id ='" & Request("mid") & "' and conn_type =2  and conn_detail_id is null and isnull(conn_status,0)=1"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            txt = txt & reader(0).ToString() & ","
        End While
        reader.Close()

        drow = dtl.NewRow()
        drow.Item("attid") = 0
        drow.Item("docitem") = "抄送给"
        drow.Item("docs") = txt
        dtl.Rows.Add(drow)

        drow = dtl.NewRow()
        drow.Item("attid") = 0
        drow.Item("docitem") = ""
        drow.Item("docs") = ""
        dtl.Rows.Add(drow)

            StrSql = "select conn_detail_id, conn_content,conn_user_name,conn_date,conn_dept from knowdb.dbo.inner_conn_detail  "
        StrSql &= " where conn_mstr_id ='" & Request("mid") & "' order by conn_detail_id "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "会签人"
            drow.Item("docs") = reader(2).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "部门"
            drow.Item("docs") = reader(4).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "会签日期"
            drow.Item("docs") = reader(3).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "会签意见"
            drow.Item("docs") = reader(1).ToString()
            dtl.Rows.Add(drow)

            txt = ""
                StrSql2 = "select conn_taken_name from knowdb.dbo.inner_conn_taken "
            StrSql2 &= " where conn_mstr_id ='" & Request("mid") & "' and conn_type =1  and conn_detail_id ='" & reader(0).ToString() & "' "
            reader2 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql2)
            While (reader2.Read())
                txt = txt & reader2(0).ToString() & ","
            End While
            reader2.Close()

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "提交给"
            drow.Item("docs") = txt
            dtl.Rows.Add(drow)

            txt = ""
                StrSql2 = "select conn_taken_name from knowdb.dbo.inner_conn_taken "
            StrSql2 &= " where conn_mstr_id ='" & Request("mid") & "' and conn_type =2   and conn_detail_id ='" & reader(0).ToString() & "' "
            reader2 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql2)
            While (reader2.Read())
                txt = txt & reader2(0).ToString() & ","
            End While
            reader2.Close()

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "抄送给"
            drow.Item("docs") = txt
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = ""
            drow.Item("docs") = ""
            dtl.Rows.Add(drow)
        End While
        reader.Close()


            StrSql = "select AttUser,AttDate , Attname,AttType,attID from knowdb.dbo.inner_conn_attach "
        StrSql &= " where conn_mstr_id ='" & Request("mid") & "' order by AttDate"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "附件上传"
            drow.Item("docs") = reader(0).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "上传日期"
            drow.Item("docs") = reader(1).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = reader(4).ToString()
            drow.Item("docitem") = "文件名"
            drow.Item("docs") = reader(2).ToString()
                drow.Item("view") = "<u>打开</u>"
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "文件类型"
            drow.Item("docs") = reader(3).ToString()
            dtl.Rows.Add(drow)
        End While
        reader.Close()
        drow = dtl.NewRow()
        drow.Item("attid") = 0
        drow.Item("docitem") = ""
        drow.Item("docs") = ""
        dtl.Rows.Add(drow)

        Dim dvw As DataView
        dvw = New DataView(dtl)

        Datagrid1.DataSource = dvw
        Datagrid1.DataBind()


    End Sub
    Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
        If e.CommandName.CompareTo("docattach") = 0 Then
            ltlAlert.Text = "var w=window.open('conn_docview.aspx?attid=" & e.Item.Cells(0).Text.Trim() & "&mid=" & Request("mid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End If
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        ltlAlert.Text = "window.close();"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ltlAlert.Text = "window.close();"
    End Sub
End Class

End Namespace













