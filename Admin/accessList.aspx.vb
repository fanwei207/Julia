Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
    Partial Class accessList
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
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
                
                If Request("pg") <> Nothing Then
                    DataGrid1.CurrentPageIndex = CInt(Request("pg"))
                End If
                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim strSQL As String
            Dim strSQL1 As String = ""
            Dim ds As DataSet
            Dim reader As SqlDataReader

            strSQL = " Select plantID, plantCode, description From plants Where isAdmin = 0 And plantID <> 99 Order By plantID"
            reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL)

            While reader.Read()
                strSQL1 &= " Select distinct a.userID, u.userName, d.name, a.createdDate, '', '', u.departmentID, '" _
                        & reader(1) & "' As Plant, '" & reader(0) & "' As PlantID, N'" & reader(2) & "(" & reader(1) & ")' As Org " _
                        & " From tcpc" & reader(0) & ".dbo.accessRuleApply a " _
                        & " Inner Join tcpc0.dbo.users u On u.userID=a.userID " _
                        & " Inner Join tcpc" & reader(0) & ".dbo.departments d On d.departmentID=u.departmentID " _
                        & " Where a.approvedBy Is Null " _
                        & " Union All "
            End While
            reader.Close()
            strSQL1 = strSQL1.Substring(0, Len(strSQL1) - 12)
            strSQL1 &= " Order By plantID, a.createdDate "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL1)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("dept", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("dtime", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("appr", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("atime", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("rID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("dID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("un", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("plant", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("plantID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("org", System.Type.GetType("System.String")))

            Dim total As Integer

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("rID") = .Rows(i).Item(0)
                        dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("dept") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("dtime") = Format(.Rows(i).Item(3), "yyyy-MM-dd")
                        dr1.Item("appr") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("dID") = .Rows(i).Item(6)
                        dr1.Item("un") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("plant") = .Rows(i).Item(7).ToString().Trim()
                        dr1.Item("plantID") = .Rows(i).Item(8).ToString().Trim()
                        dr1.Item("org") = .Rows(i).Item(9).ToString().Trim()
                        dt.Rows.Add(dr1)
                        total = total + 1
                    Next
                End If
            End With
            ds.Reset()

            Label2.Text = "×ÜÊý£º " & total.ToString()

            If Request("pg") <> Nothing Then
                If CInt(Request("pg")) > 0 Then
                    If CInt(Request("pg")) * 3 > (total - 1) Then
                        DataGrid1.CurrentPageIndex = CInt(Request("pg")) - 1
                    End If
                End If
            End If

            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If e.CommandName = "editBtn" Then
                Session("PlantCode") = CInt(e.Item.Cells(10).Text)
                Session("orgName") = e.Item.Cells(11).Text
                Response.Redirect("accessApprove.aspx?fr=all&pg=" & DataGrid1.CurrentPageIndex.ToString() & "&uid=" & e.Item.Cells(1).Text & "&uname=" & e.Item.Cells(9).Text & "&did=" & e.Item.Cells(8).Text & "&dname=" & e.Item.Cells(3).Text & "&rm=" & DateTime.Now())
            End If
        End Sub
    End Class

End Namespace