Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

Partial Class qad_documentitemview
        Inherits BasePage
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    'Protected WithEvents ltlAlert As Literal
    Public chk As New adamClass
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents Button1 As System.Web.UI.WebControls.Button


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
        End If
    End Sub
    Private Sub BindData()
        Dim strSQL As String
        Dim ds As DataSet
        strSQL = "select id,qad,desc0 From qaddoc.dbo.DocumentItem where docid=" & Request("id")
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("desc", System.Type.GetType("System.String")))
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("gsort") = i + 1
                    dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                    dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim()
                    dr1.Item("desc") = .Rows(i).Item(2).ToString().Trim()
                    dt.Rows.Add(dr1)
                Next
            End If
        End With
        Dim dv As DataView
        dv = New DataView(dt)
        If (Session("orderby1").Length <= 0) Then
            Session("orderby1") = "gsort"
        End If
        Try
            dv.Sort = Session("orderby1") & Session("orderdir1")
            DataGrid1.DataSource = dv
            DataGrid1.DataBind()
        Catch
        End Try
    End Sub
    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(2)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")

            Case ListItemType.AlternatingItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(2)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")
            Case ListItemType.EditItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(2)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")
        End Select

    End Sub

    Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand 
        If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
            Dim strSQL As String
            strSQL = "delete from qaddoc.dbo.DocumentItem where id = " & e.Item.Cells(4).Text()
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            DataGrid1.EditItemIndex = -1
            BindData()
        End If
    End Sub


    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        Session("orderby1") = e.SortExpression.ToString()
        If Session("orderdir1") = " ASC" Then
            Session("orderdir1") = " DESC"
        Else
            Session("orderdir1") = " ASC"
        End If
        BindData()
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim strsql As String 
        If txbadd.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('请输入代码!');"
            Exit Sub
        End If
        strsql = "select count(id) from qaddoc.dbo.qad_items where qad=N'" & txbadd.Text.Trim & "' "

        If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) <= 0 Then
            ltlAlert.Text = "alert('所输入代码不存在!');"
            Exit Sub
        End If
        Dim params(3) As SqlParameter
        Dim cnt As Integer
        strsql = "qaddoc.dbo.qad_documentitemAdd"
        params(0) = New SqlParameter("@docID", Request("id"))
        params(1) = New SqlParameter("@qad", txbadd.Text.Trim)
        params(2) = New SqlParameter("@uID", Session("uID"))
        cnt = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strsql, params)
        If cnt < 0 Then
            ltlAlert.Text = "alert('关联产品失败!');"
            Exit Sub
        End If

        txbadd.Text = ""
        DataGrid1.CurrentPageIndex = 0
        BindData()
    End Sub

End Class

End Namespace
