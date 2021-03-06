Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

Partial Class perf_showdetail
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
            If Request("mid") <> Nothing Then
                BindData()
            End If
        End If
    End Sub

    Sub BindData()
        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("docitem", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("docs", System.Type.GetType("System.String")))
        Dim drow As DataRow

        Dim i As Integer

            StrSql = "select perf_act_name,perf_act_date,perf_type,perf_cause,perf_notes,perf_fpath,perf_fname,perf_mark,perf_mark_detil from perf_mstr"
        StrSql &= " where perf_mstr_id ='" & Request("mid") & "'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            drow = dtl.NewRow()
            drow.Item("docitem") = "考评人"
            drow.Item("docs") = reader(0).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("docitem") = "考评日期"
            drow.Item("docs") = Format(reader(1), "yyyy-MM-dd")
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("docitem") = "类型"
            drow.Item("docs") = reader(2).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("docitem") = "原因"
            drow.Item("docs") = reader(3).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("docitem") = "说明"
            drow.Item("docs") = reader(4).ToString()
            dtl.Rows.Add(drow)

                If reader(7).ToString() = "1" Then
                    drow = dtl.NewRow()
                    drow.Item("docitem") = "整改意见"
                    drow.Item("docs") = reader(8).ToString()
                    dtl.Rows.Add(drow)
                End If
                drow = dtl.NewRow()
                drow.Item("docitem") = ""
                drow.Item("docs") = ""
                dtl.Rows.Add(drow)

                If reader(5).ToString() <> "" Then
                    lbldoc.Text = reader(5).ToString()
                    lbn_doc.Text = reader(6).ToString()
                    lblup.Visible = True
                    lbn_doc.Visible = True
                End If


            End While
        reader.Close()


        StrSql = "select perf_userno,perf_uname,perf_dept,perf_mark,perf_note,perf_duty from perf_details "
        StrSql &= " where perf_mstr_id ='" & Request("mid") & "' and perf_deleted is null order by perf_detail_id"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            drow = dtl.NewRow()
            drow.Item("docitem") = "考评对象"
            If reader(5) = 1 Then
                    drow.Item("docs") = reader(0).ToString() & " - " & reader(1).ToString() & " (第一责任人)"
                    dtl.Rows.Add(drow)
                  
            ElseIf reader(5) = 2 Then
                    drow.Item("docs") = reader(0).ToString() & " - " & reader(1).ToString() & " (第二责任人)"
                    dtl.Rows.Add(drow)
                   
            ElseIf reader(5) = 3 Then
                    drow.Item("docs") = reader(0).ToString() & " - " & reader(1).ToString() & " (第三责任人)"
                    dtl.Rows.Add(drow)
            Else
                    drow.Item("docs") = reader(0).ToString() & " - " & reader(1).ToString() & " (第四责任人)"
                    dtl.Rows.Add(drow)
            End If


                drow = dtl.NewRow()
                drow.Item("docitem") = "描述"
                drow.Item("docs") = reader(4).ToString()
                dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("docitem") = "部门"
            drow.Item("docs") = reader(2).ToString()
            dtl.Rows.Add(drow)

            drow = dtl.NewRow()
            drow.Item("docitem") = "评分"
            drow.Item("docs") = Format(reader(3), "##0.##")
            dtl.Rows.Add(drow)

          

            drow = dtl.NewRow()
            drow.Item("docitem") = ""
            drow.Item("docs") = ""
            dtl.Rows.Add(drow)
        End While
        reader.Close()



        Dim dvw As DataView
        dvw = New DataView(dtl)

        Datagrid1.DataSource = dvw
        Datagrid1.DataBind()


    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        ltlAlert.Text = "window.close();"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ltlAlert.Text = "window.close();"
    End Sub

        Protected Sub lbn_doc_Click(sender As Object, e As EventArgs) Handles lbn_doc.Click
            Dim filePath As String
            Dim i As Integer
            filePath = lbldoc.Text
            filePath = filePath.Replace("\\", "/")
            If File.Exists(filePath) = False Then
                ltlAlert.Text = "alert('文件已移除或不存在！')"
                Return
            End If


            '  if (!File.Exists(@filePath))
            '{
            '    ltlAlert.Text = "alert('文件已移除或不存在！')";
            '    return;
            '}
            i = filePath.IndexOf("TecDocs")
            filePath = filePath.Substring(i - 1)
            filePath = filePath.Replace("\", "/")
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();"


        End Sub
    End Class

End Namespace













