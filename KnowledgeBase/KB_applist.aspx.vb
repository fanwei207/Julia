Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

Partial Class KB_applist
    Inherits System.Web.UI.Page
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
    Protected WithEvents btn_next As System.Web.UI.WebControls.Button
    Protected WithEvents btn_close As System.Web.UI.WebControls.Button
    Protected WithEvents bnt_cancel As System.Web.UI.WebControls.Button

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
            BindData()
            StrSql = "delete from knowdb.dbo.attachtemp where attuser=N'" & Session("uName") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
        End If
    End Sub

    Sub BindData()
        StrSql = "select docid,typeid,typename,doccontent,docuserid,docuser,docdate from knowdb.dbo.application  "
        If Session("uRole") = 1 Then
            StrSql &= "where doccurrid is not null "
        Else
            StrSql &= " where  doccurrid like '" & "%," + CStr(Session("uID")) + ",%" & "' "
        End If

        If rb_continue.Checked Then
            StrSql &= " AND docstatus is null "
        End If

        If rb_close.Checked Then
            StrSql &= " AND docstatus is not null "
        End If
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
        Dim total As Integer
        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("typeid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("docid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("docuserid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("typename", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("docuser", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("docdate", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("doccontent", System.Type.GetType("System.String")))
        Dim drow As DataRow
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("typeid") = .Rows(i).Item("typeid").ToString().Trim()
                    drow.Item("docid") = .Rows(i).Item("docid").ToString().Trim()
                    drow.Item("docuserid") = .Rows(i).Item("docuserid").ToString().Trim()
                    drow.Item("typename") = .Rows(i).Item("typename").ToString().Trim()
                    drow.Item("docuser") = .Rows(i).Item("docuser").ToString().Trim()
                    drow.Item("doccontent") = .Rows(i).Item("doccontent").ToString().Trim()
                    If IsDBNull(.Rows(i).Item("docdate")) = False Then
                        drow.Item("docdate") = Format(.Rows(i).Item("docdate"), "yyyy-MM-dd")
                    Else
                        drow.Item("docdate") = ""
                    End If

                    dtl.Rows.Add(drow)
                Next


            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)

        Datagrid1.DataSource = dvw
        Datagrid1.DataBind()
        ds.Reset()

    End Sub
    Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
        Dim docstatus As String

        If rb_continue.Checked Then
            docstatus = "OPEN"
        End If

        If rb_close.Checked Then
            docstatus = "CLOSE"
        End If

        If e.CommandName.CompareTo("docview") = 0 Then
            Response.Redirect("../knowledgebase/kb_app.aspx?typeid=" & e.Item.Cells(0).Text.Trim() & "&docid=" & e.Item.Cells(1).Text.Trim() & "&docstatus=" & docstatus & " ")
        End If
    End Sub

    Private Sub bnt_newapp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_newapp.Click
        Response.Redirect("../knowledgebase/kb_app.aspx")
    End Sub
    Sub RBCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        BindData()
    End Sub
End Class

End Namespace













