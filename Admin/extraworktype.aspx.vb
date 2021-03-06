Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class extraworktype
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
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
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT s.systemcodeid,s.systemcodename,s.comments " _
                   & " From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.systemcodetypeid=s.systemcodetypeid where st.systemcodetypename='Extra Work Type'" _
                   & " Order by s.systemcodeid"
            Session("EXSQL") = strSQL
            Session("EXTitle") = "<b>编号</b>~^<b>名称</b>~^<b>备注</b>~^"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("price", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        If .Rows(i).IsNull(2) = True Then
                            dr1.Item("price") = ""
                        Else
                            dr1.Item("price") = .Rows(i).Item(2).ToString()
                        End If
                        dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
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
            Dim str As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空.')"
                Exit Sub
            End If
            Dim str1 As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            If (str1.Trim.Length <= 0) Then
                strSQL = "update tcpc0.dbo.systemCode set systemcodename=N'" & str & "',comments=null where systemcodeID=" & e.Item.Cells(5).Text
            Else
                Dim up As Decimal
                Try
                    up = Convert.ToDecimal(str1)
                Catch ex As Exception
                    ltlAlert.Text = "alert('金额输入的格式不对');"
                    Exit Sub

                End Try
                strSQL = "update tcpc0.dbo.systemCode set systemcodename=N'" & str & "', comments='" & str1 & "' where systemcodeID=" & e.Item.Cells(5).Text
            End If

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            DataGrid1.EditItemIndex = -1
            BindData()

        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strSQL As String
                strSQL = "delete from tcpc0.dbo.systemCode where systemcodeID = " & e.Item.Cells(5).Text()
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
            Dim id As String
            strSQL = "Select SystemCodeTypeID From tcpc0.dbo.systemCodeType Where SystemCodeTypeName='Extra Work Type'"
            id = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
            If id <> Nothing Then
                strSQL = "Insert Into tcpc0.dbo.systemCode(systemCodeName,systemCodeTypeID) Values(N'(新的方式)'," & id & ")"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = 0
                BindData()
            End If
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
