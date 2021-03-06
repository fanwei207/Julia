Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class producthistory
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim strSQL As String
        Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox


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

                labreq.Text = Request("id")
                BindData()
            End If
        End Sub
        Sub BindData()
            strSQL = " SELECT i.code, i.description, ic1.name, i.old_code, i.old_description, ic2.name AS name1,us.username,i.createddate " _
                   & " FROM Items_his i INNER JOIN ItemCategory ic1 ON ic1.id = i.category INNER JOIN ItemCategory ic2 ON ic2.id = i.old_category  " _
                   & " inner join users us on us.userID=i.createdby " _
                   & " where i.code is not null "
            If txtCode.Text.Length > 0 Then
                strSQL &= " and i.code= N'" & txtCode.Text.Trim & "'"
            End If
            If txtold_code.Text.Length > 0 Then
                strSQL &= " and i.old_code= N'" & txtold_code.Text.Trim & "'"
            End If
            If txtCategory.Text.Length > 0 Then
                strSQL &= " and ic1.name= N'" & txtCategory.Text.Trim & "'"
            End If
            If txtold_category.Text.Length > 0 Then
                strSQL &= " and ic2.name= N'" & txtold_category.Text.Trim & "'"
            End If
            If labreq.Text.Trim.Length > 0 Then
                strSQL &= " and i.item_id='" & labreq.Text.Trim & "'"
            End If
            'strSQL &= " order by i.createddate "
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSQL)
            'Me.Response.Write(strSQL)
            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("category", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("old_code", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("old_description", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("old_category", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("createdby", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("createddate", System.Type.GetType("System.DateTime")))

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dt.NewRow()
                        drow.Item("gsort") = i + 1
                        drow.Item("code") = .Rows(i).Item("code").ToString().Trim()
                        drow.Item("category") = .Rows(i).Item("name").ToString().Trim()
                        drow.Item("old_code") = .Rows(i).Item("old_code").ToString().Trim()
                        drow.Item("old_category") = .Rows(i).Item("name1").ToString().Trim()
                        drow.Item("description") = .Rows(i).Item("description").ToString().Trim()
                        drow.Item("old_description") = .Rows(i).Item("old_description").ToString().Trim()
                        drow.Item("createdby") = .Rows(i).Item("username").ToString().Trim()
                        drow.Item("createddate") = .Rows(i).Item("createddate")
                        dt.Rows.Add(drow)
                    Next
                End If
            End With

            dgProduct.DataSource = dt
            dgProduct.DataBind()

        End Sub

        Private Sub dgProduct_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProduct.PageIndexChanged
            dgProduct.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
            dgProduct.CurrentPageIndex = 0
            labreq.Text = ""
            BindData()
        End Sub
    End Class

End Namespace
