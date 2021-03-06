'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-10-30
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   List Status
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class Status
        Inherits BasePage
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass
        Dim strSql As String
        Dim dst As DataSet

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
            End If
        End Sub

        Private Sub BindData()
            strSql = " Select id, StatusName From tcpc0.dbo.Status Order By id Desc "
            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))

            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("gsort") = i + 1
                        drow.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("Name") = .Rows(i).Item(1).ToString().Trim()
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            Dim dvw As DataView
            dvw = New DataView(dtl)

            Try
                dgStatus.DataSource = dvw
                dgStatus.DataBind()
            Catch
            End Try
        End Sub

        Public Sub Edit_edit(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgStatus.ItemCommand
            If (e.CommandName.CompareTo("Edit_edit") = 0) Then
                dgStatus.EditItemIndex = e.Item.ItemIndex
                BindData()
            End If
        End Sub

        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgStatus.CancelCommand
            dgStatus.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgStatus.UpdateCommand
            Dim strSQL As String
            Dim str As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空！')"
                Exit Sub
            End If

            strSQL = " Update Status Set StatusName=N'" & chk.sqlEncode(str.Trim()) & "' Where id=" & e.Item.Cells(4).Text
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            dgStatus.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgStatus.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                strSql = " Delete From tcpc0.dbo.Status Where id=" & e.Item.Cells(4).Text()
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                dgStatus.EditItemIndex = -1
                BindData()
            End If
        End Sub

        Private Sub dgStatus_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgStatus.PageIndexChanged
            dgStatus.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
            strSql = " Insert Into Status(StatusName) Values(N'(新的)')"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            dgStatus.EditItemIndex = 0
            BindData()
        End Sub

        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgStatus.EditCommand
            dgStatus.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub
    End Class

End Namespace
