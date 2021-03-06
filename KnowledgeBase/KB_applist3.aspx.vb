Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

Partial Class KB_applist3
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
            If Request("docid") <> "" Then
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

        StrSql = "select typename,isnull(DocStatus,N'新建申请') as DocStatus , docuser, doccontent,docdate,DocCurr from knowdb.dbo.application "
        StrSql &= " where docid ='" & Request("docid") & "'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "类型"
            drow.Item("docs") = reader(0).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "状态"
            drow.Item("docs") = reader(1).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "申请人"
            drow.Item("docs") = reader(2).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "申请内容"
            drow.Item("docs") = reader(3).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "申请日期"
            drow.Item("docs") = reader(4).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "处理人"
            drow.Item("docs") = reader(5).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = ""
            drow.Item("docs") = ""
            dtl.Rows.Add(drow)
        End While
        reader.Close()


        StrSql = "select SugUser,SugDate , SugContent,DocCurr from knowdb.dbo.Suggestion "
        StrSql &= " where DocID ='" & Request("docid") & "' order by SugDate"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "会签人"
            drow.Item("docs") = reader(0).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "会签日期"
            drow.Item("docs") = reader(1).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "会签意见"
            drow.Item("docs") = reader(2).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = "提交给"
            drow.Item("docs") = reader(3).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("attid") = 0
            drow.Item("docitem") = ""
            drow.Item("docs") = ""
            dtl.Rows.Add(drow)
        End While
        reader.Close()


        StrSql = "select AttUser,AttDate , Attname,AttType,attID from knowdb.dbo.Attach "
        StrSql &= " where DocID ='" & Request("docid") & "' order by AttDate"
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
            drow.Item("view") = "打开"
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
            ltlAlert.Text = "var w=window.open('/knowledgebase/kb_docview.aspx?attid=" & e.Item.Cells(0).Text.Trim() & "&docid=" & Request("docid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
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













