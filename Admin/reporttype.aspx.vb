Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class reporttype
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected strTemp As String
        Shared sortOrder As String = ""
        Shared sortDir As String = "ASC"
        Shared addnew As Boolean = False

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
            Dim query As String
            Dim dst As DataSet
            query = " SELECT s.systemCodeID,s.systemCodeName " _
                   & " From tcpc0.dbo.systemCode s" _
                   & " INNER JOIN tcpc0.dbo.systemCodeType st ON st.systemCodeTypeID=s.systemCodeTypeID " _
                   & " Where st.systemCodeTypeName='Account Report Type'" _
                   & " Order by s.systemCodeName"
            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))

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
                dgReport.DataSource = dvw
                dgReport.DataBind()
            Catch
            End Try
        End Sub

        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgReport.CancelCommand
            Dim query As String
            If addnew = True Then
                query = " DELETE FROM tcpc0.dbo.systemCode WHERE systemCodeID='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)
                addnew = False
            End If
            dgReport.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgReport.UpdateCommand
            Dim query As String
            Dim str As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            If str.Trim().Length <= 0 Then
                ltlAlert.Text = "alert('报表名称不能为空！');"
                Exit Sub
            ElseIf str.Trim().Length > 30 Then
                ltlAlert.Text = "alert('报表名称长度超标(要求30字以内)！');"
                Exit Sub
            Else
                query = "update tcpc0.dbo.systemCode set systemCodeName=N'" & str & "' where systemCodeID=" & e.Item.Cells(0).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)
                dgReport.EditItemIndex = -1
                BindData()
            End If
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgReport.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim query As String
                query = " DELETE FROM tcpc0.dbo.systemCode WHERE systemCodeID='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)
                dgReport.EditItemIndex = -1
                BindData()
            End If
        End Sub

        Private Sub dgReport_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgReport.PageIndexChanged
            dgReport.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
            Dim query As String
            query = "SELECT systemCodeTypeID FROM tcpc0.dbo.systemCodeType WHERE systemCodeTypeName='Account Report Type' "
            Dim id As String = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, query)
            query = "Insert Into tcpc0.dbo.systemCode(systemCodeTypeID, systemCodeName) Values('" & id & "','')"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)
            dgReport.EditItemIndex = 0
            BindData()
        End Sub

        Private Sub dgReport_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgReport.EditCommand
            dgReport.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

    End Class

End Namespace
