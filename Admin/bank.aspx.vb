Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class bank
        Inherits BasePage
        Shared sortOrder As String = ""
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
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
                Button1.Enabled = Me.Security("19021102").isValid

                BindData()
            End If
        End Sub
        Private Sub BindData()
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT id, name, isnull(code,''),isnull(accountNo,''),isnull(address,''),isnull(zip,null),isnull(phone,''),isnull(fax,'') " _
                   & " From bank " _
                   & " Order by name"
            Session("EXSQL") = strSQL
            Session("EXTitle") = "<b>名称</b>~^<b>编号</b>~^<b>帐号</b>~^<b>地址</b>~^<b>邮编</b>~^<b>电话</b>~^<b>传真</b>~^"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("gname", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gcode", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gno", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gaddr", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gzip", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gphone", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gfax", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gID", System.Type.GetType("System.String")))
            Dim cat As String
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("gname") = .Rows(i).Item(1).ToString()
                        dr1.Item("gcode") = .Rows(i).Item(2).ToString()
                        dr1.Item("gno") = .Rows(i).Item(3).ToString()
                        dr1.Item("gaddr") = .Rows(i).Item(4).ToString()
                        dr1.Item("gzip") = .Rows(i).Item(5).ToString()
                        dr1.Item("gphone") = .Rows(i).Item(6).ToString()
                        dr1.Item("gfax") = .Rows(i).Item(7).ToString()
                        dr1.Item("gID") = .Rows(i).Item(0).ToString().Trim()
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

        Public Sub editBt(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("editBt") = 0) Then
                Dim str As String = e.Item.Cells(0).Text
                Response.Redirect(chk.urlRand("bankEdit.aspx?id=" & str))
            End If
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            Dim strSQL As String
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then

                strSQL = "delete from bank where id = " & e.Item.Cells(0).Text()
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                BindData()
            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            Response.Redirect(chk.urlRand("bankEdit.aspx"))
        End Sub

        Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                e.Item.Cells(8).Enabled = Me.Security("19021102").isValid
                e.Item.Cells(9).Enabled = Me.Security("19021103").isValid
            End If
            '
        End Sub
    End Class

End Namespace
