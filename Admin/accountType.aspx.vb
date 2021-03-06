Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class accountType
        Inherits BasePage

    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected strTemp As String
    'Protected WithEvents ltlAlert As Literal
    Public chk As New adamClass
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
            BindData(False)
        End If
    End Sub
    Private Sub BindData(ByVal sort As Boolean)
        Dim strSQL As String
        Dim ds As DataSet
        strSQL = " SELECT id,name " _
               & " From accountCategory" _
               & " Order by id"
        If sort Then
            strSQL = strSQL & " desc"
        End If
        Session("EXSQL") = strSQL
        Session("EXTitle") = "<b>名称</b>~^"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("gsort") = i + 1
                    dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                    dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim()
                    dt.Rows.Add(dr1)
                Next
            End If
        End With
        Dim dv As DataView
        dv = New DataView(dt)

            Session("orderby") = "gsort"

            Try
                dv.Sort = Session("orderby") & Session("orderdir")
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
    End Sub

    Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        DataGrid1.EditItemIndex = -1
        BindData(False)
    End Sub
    Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        Dim strSQL As String
        Dim str As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
        If (str.Trim.Length <= 0) Then
            ltlAlert.Text = "alert('名称不能为空.')"
            Exit Sub
        End If

        strSQL = "update accountCategory set name=N'" & str & "' where id=" & e.Item.Cells(4).Text
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
        DataGrid1.EditItemIndex = -1
        BindData(False)
    End Sub

    Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
            Dim strSQL As String
            strSQL = "delete from accountCategory where id = " & e.Item.Cells(4).Text()
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            DataGrid1.EditItemIndex = -1
            BindData(False)
        End If
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData(False)
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strSQL As String
        Dim id As String
        strSQL = "Insert Into accountCategory(name) Values(N'(新的类型)')"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
        DataGrid1.EditItemIndex = 0
        BindData(True)
    End Sub
    Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        DataGrid1.EditItemIndex = e.Item.ItemIndex
        BindData(False)
    End Sub

End Class

End Namespace
