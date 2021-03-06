Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class role
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

                BindData()
            End If
        End Sub
        Private Sub BindData()
            Dim strSQL As String = ""
            Dim ds As DataSet
            Select Case DropDownList1.SelectedIndex
                Case 0
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = isnull(roleProportion,0) " _
                       & " From Roles where roleID>=100 and roleID<300 " _
                       & " Order by roleName"
                Case 1
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = isnull(roleProportion,0)" _
                       & " From Roles where roleID>=300 and roleID<500 " _
                       & " Order by roleName"
                Case 2
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = isnull(roleProportion,0) " _
                       & " From Roles where roleID>=500 and roleID<1000 " _
                       & " Order by roleName"
                Case 3
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = isnull(roleProportion,0) " _
                       & " From Roles where roleID>=1000 and roleID<5000 " _
                       & " Order by roleName"
            End Select

            Session("EXSQL") = strSQL
            Session("EXTitle") = "<b>编号</b>~^<b>名称</b>~^<b>系数</b>~^"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("proportion", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("rID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("rID") = .Rows(i).Item(1)
                        dr1.Item("Name") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("proportion") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub

        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub
        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
            Dim strSQL As String
            Dim ds As DataSet
            Dim str As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            Dim dec As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空.')"
                Exit Sub
            End If
            Dim stsql As String = "select * from roles where roleName =N'" & str & "'"
            Dim sr As SqlDataReader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, stsql)
            Dim bb As Boolean = sr.HasRows
            If sr.HasRows Then
                ltlAlert.Text = "alert('名称已经存在111，不能重复.')"
                Exit Sub
            End If

            strSQL = "update roles set roleName=N'" & str & "',roleProportion=" & dec & "where ID=" & e.Item.Cells(6).Text
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            DataGrid1.EditItemIndex = -1
            BindData()

        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strSQL As String
                strSQL = "delete from roles where ID = " & e.Item.Cells(6).Text() & " and roleID>1"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = -1
                BindData()
            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            Dim strSQL As String

            Dim bID As Integer
            Dim eID As Integer

            Select Case DropDownList1.SelectedIndex
                Case 0
                    bID = 100
                    eID = 299
                Case 1
                    bID = 300
                    eID = 499
                Case 2
                    bID = 500
                    eID = 999
                Case 3
                    bID = 1000
                    eID = 5000
            End Select

            strSQL = "sp_SetRoleID"
            Dim params(1) As SqlParameter
            params(0) = New SqlParameter("@beginid", bID)
            params(1) = New SqlParameter("@endid", eID)
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
            DataGrid1.EditItemIndex = 0
            BindData()
        End Sub
        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            DataGrid1.CurrentPageIndex = 0
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub
    End Class

End Namespace
