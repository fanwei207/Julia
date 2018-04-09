Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Web.Mail


Namespace tcpc


Partial Class perf_app
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
    Dim nRet As Integer
    Dim item As ListItem
    Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents panel1 As System.Web.UI.WebControls.Panel

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
            'check 

            dd_type.SelectedIndex = 0
            item = New ListItem("--")
            item.Value = -1
            dd_type.Items.Add(item)

            StrSql = "SELECT perf_type_id,perf_type From tcpc0.dbo.perf_type order by perf_type_id "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        dd_type.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()
            loadCause()
        End If
    End Sub

    Sub loadCause()
        While dd_cause.Items.Count > 0
            dd_cause.Items.RemoveAt(0)
        End While

        If dd_type.SelectedIndex > 0 Then
            Dim ls As ListItem

            item = New ListItem("--")
                item.Value = 0
                dd_cause.Items.Add(item)
                dd_cause.SelectedIndex = 0

            Dim reader As SqlDataReader
            StrSql = "SELECT perf_defi_id,perf_cause From tcpc0.dbo.perf_definition where perf_type_id ='" & dd_type.SelectedValue & "' order by perf_type_id,perf_cause "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0).ToString()
                ls.Text = reader(1).ToString.Trim
                dd_cause.Items.Add(ls)
            End While
            reader.Close()
        End If
    End Sub
    Private Sub btn_next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_next.Click
        If dd_cause.SelectedIndex <= 0 Then
            ltlAlert.Text = "alert('请选择原因！')"
            Exit Sub
        End If

        If txb_note.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('请输入说明！')"
            Exit Sub
        ElseIf txb_note.Text.Trim.Length > 1000 Then
            ltlAlert.Text = "alert('说明不能超过500个字！')"
            Exit Sub
        End If

        StrSql = "Insert into perf_mstr(perf_cause,perf_notes,perf_act_date,perf_act_by,perf_act_name,perf_type) "
        StrSql &= " values(N'" & dd_type.SelectedItem.Text.Trim() & "-" & dd_cause.SelectedItem.Text & "',N'" & chk.sqlEncode(txb_note.Text) & "' "
        StrSql &= " ,getdate(),'" & Session("uID") & "',N'" & Session("uName") & "',N'" & dd_type.SelectedItem.Text.Trim() & "')  Select @@IDENTITY"

        'Response.Write(StrSql)
        'Exit Sub

        Dim id As Integer
        id = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
        If id < 0 Then
            ltlAlert.Text = "alert('创建考评失败!')"
            Exit Sub
        Else
            Response.Redirect("perf_mark.aspx?mid=" & id)
        End If
    End Sub


    Private Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
        Response.Redirect("perf_mstr.aspx?mid=" & Request("mid"))

    End Sub

    Private Sub dd_cause_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dd_cause.SelectedIndexChanged
    End Sub

    Private Sub dd_type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dd_type.SelectedIndexChanged
        loadCause()
    End Sub
End Class

End Namespace
