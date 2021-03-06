Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class productWhPlaceLink
        Inherits BasePage

        Shared sortOrder As String = ""
        Shared sortDir As String = "ASC"
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim strSql As String

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
            If Request("id") = Nothing Then
                Response.Redirect("/admin/productWHPlace.aspx", True)
                Exit Sub
            End If

            If Not IsPostBack Then


                Dim ds As DataSet
                strSql = " SELECT pwp.name From warehouse pwp where pwp.warehouseID='" & Request("id") & "'"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Label1.Text = ds.Tables(0).Rows(0).Item(0)
                End If
                ds.Reset()
                partWhPlaceListLoad()
            End If
        End Sub
        Private Sub partWhPlaceListLoad()
            Dim ds As DataSet
            strSql = " SELECT u.userID,u.username From User_Warehouse pwp Inner Join tcpc0.dbo.users u on u.userID=pwp.userID where pwp.warehouseID=" & Request("id") & " Order by u.userName "

            Session("EXHeader") = "库位~" & Label1.Text & "~^"
            Session("EXSQL") = strSql
            Session("EXTitle") = "<b>工号</b>~^<b>名称</b>~^"

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                partWhPlaceList.DataSource = dv
                partWhPlaceList.DataBind()
            Catch
            End Try
        End Sub
        Private Sub DataGrid1_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
            partWhPlaceList.CurrentPageIndex = e.NewPageIndex
            partWhPlaceList.EditItemIndex = -1
            partWhPlaceListLoad()
        End Sub
        Private Sub addBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addBtn.Click
            Response.Redirect("/admin/user_prod_wh.aspx?id=" & Request("id") & "&pg=/admin/productWHPlaceLink.aspx", True)
        End Sub

        Private Sub bntBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntBack.Click
            Response.Redirect("/admin/prodWHPlace.aspx?id=" & Request("id"), True)
        End Sub
    End Class

End Namespace
