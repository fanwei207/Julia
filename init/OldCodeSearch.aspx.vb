Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

    Partial Class OldCodeSearch
        Inherits BasePage
        Dim chk As New adamClass
        Dim strSql As String
        Dim ds As DataSet

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack() Then

                BindProdCode()
            End If
        End Sub

        Sub BindProdCode()
            strSql = " SELECT id, old_code, ISNULL(new_code, '') AS new_code FROM item_old WHERE id>0"

            If txtOldProdCode.Text.Trim.Length > 0 Then
                strSql &= " AND old_code LIKE N'%" & txtOldProdCode.Text & "%'"
            End If

            If txtNewProdCode.Text.Trim.Length > 0 Then
                strSql &= " AND new_code LIKE N'%" & txtNewProdCode.Text & "%'"
            End If

            strSql &= " ORDER BY old_code"
            ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSql)

            Session("EXSQL") = "select distinct old_code,new_code from tcpc0.dbo.item_old where old_code is not null and new_code is not null "
            Session("EXHeader") = ""
            Session("EXTitle") = "<b>旧部件号</b>~^<b>新部件号</b>~^"

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("old_code", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("new_code", System.Type.GetType("System.String")))

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim drow As DataRow

                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("old_code") = .Rows(i).Item(1).ToString()
                        drow.Item("new_code") = .Rows(i).Item(2).ToString()

                        dtl.Rows.Add(drow)
                    Next
                End If
            End With

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Try
                dgProdCode.DataSource = dvw
                dgProdCode.DataBind()
            Catch
            End Try

        End Sub

        Private Sub dgProdCode_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProdCode.PageIndexChanged
            dgProdCode.CurrentPageIndex = e.NewPageIndex
            BindProdCode()
        End Sub

        Private Sub dgProdCode_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProdCode.ItemCommand
            If e.CommandName.CompareTo("Delete") = 0 Then
                strSql = "DELETE FROM item_old WHERE id='" & e.Item.Cells(0).Text.Trim & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)

                BindProdCode()
            End If
        End Sub

        Private Sub dgProdCode_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgProdCode.ItemCreated
            Select Case e.Item.ItemType

                Case ListItemType.Item
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(3)
                    myDeleteButton.Attributes.Add("onclick", "if(confirm(‘确定要删除该记录吗?’)){client_confirmed = true;}")

                Case ListItemType.AlternatingItem
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(3)
                    myDeleteButton.Attributes.Add("onclick", "if(confirm(‘确定要删除该记录吗?’)){client_confirmed = true;}")

                Case ListItemType.EditItem
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(3)
                    myDeleteButton.Attributes.Add("onclick", "if(confirm(‘确定要删除该记录吗?’)){client_confirmed = true;}")

            End Select
        End Sub

        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            dgProdCode.CurrentPageIndex = 0
            BindProdCode()
        End Sub

        Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            If txtOldProdCode.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请输入旧产品型号！')"
                Exit Sub
            End If

            If txtNewProdCode.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请输入新产品型号！')"
                Exit Sub
            End If

            Dim num As Integer
            strSql = " SELECT COUNT(*) FROM item_old WHERE old_code = '" & txtOldProdCode.Text & "'"
            num = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql)
            If num > 0 Then
                ltlAlert.Text = "alert('旧产品型号已经存在，添加失败！')"
                Exit Sub
            End If

            strSql = " INSERT INTO item_old (old_code, new_code, createby, createddate) VALUES ('" _
                   & txtOldProdCode.Text & "','" & txtNewProdCode.Text & "','" & Session("uid") & "'," & "GetDate())"

            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)

            txtOldProdCode.Text = ""
            txtNewProdCode.Text = ""
            BindProdCode()
        End Sub
    End Class

End Namespace
