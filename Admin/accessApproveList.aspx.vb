Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

Partial Class accessApproveList
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
             
            If Request("dp") <> Nothing Then
                DropDownList1.SelectedValue = Request("dp")
            End If

            If Request("pg") <> Nothing Then
                DataGrid1.CurrentPageIndex = CInt(Request("pg"))
            End If
            BindData()
        End If
    End Sub
    Private Sub BindData()
        Dim strSQL As String
        Dim ds As DataSet
        Select Case DropDownList1.SelectedIndex
            Case 0
                strSQL = " SELECT distinct a.userID,u.userName,d.name,a.createdDate,'','',u.departmentID from accessRuleApply a Inner Join tcpc0.dbo.users u on u.userID=a.userID Inner Join departments d on d.departmentID=u.departmentID where a.approvedBy is null order by a.createdDate "
            Case 1
                strSQL = " SELECT distinct a.userID,u.userName,d.name,a.createdDate,ua.userName,a.approvedDate,u.departmentID from accessRuleApply a Inner Join tcpc0.dbo.users u on u.userID=a.userID Inner Join departments d on d.departmentID=u.departmentID Inner Join tcpc0.dbo.users ua On ua.userID=a.approvedBy where a.approvedBy is not null order by a.createdDate desc"
        End Select

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

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
                    If DropDownList1.SelectedIndex = 1 Then
                        dr1.Item("atime") = Format(.Rows(i).Item(5), "yyyy-MM-dd")
                    End If
                    dr1.Item("dID") = .Rows(i).Item(6)
                    dr1.Item("un") = .Rows(i).Item(1).ToString().Trim()
                    dt.Rows.Add(dr1)
                    total = total + 1
                Next
            End If
        End With
        ds.Reset()

        Label2.Text = "总数： " & total.ToString()

        'Response.Write(DataGrid1.PageCount.ToString())

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

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        DataGrid1.CurrentPageIndex = 0
        DataGrid1.EditItemIndex = -1
        BindData()
    End Sub

    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If e.CommandName = "editBtn" Then
            If DropDownList1.SelectedValue = 0 Then
                Response.Redirect("accessApprove.aspx?pg=" & DataGrid1.CurrentPageIndex.ToString() & "&uid=" & e.Item.Cells(1).Text & "&uname=" & e.Item.Cells(9).Text & "&did=" & e.Item.Cells(8).Text & "&dname=" & e.Item.Cells(3).Text & "&rm=" & DateTime.Now())
            Else
                Response.Redirect("accessApproved.aspx?pg=" & DataGrid1.CurrentPageIndex.ToString() & "&uid=" & e.Item.Cells(1).Text & "&uname=" & e.Item.Cells(9).Text & "&did=" & e.Item.Cells(8).Text & "&dname=" & e.Item.Cells(3).Text & "&rm=" & DateTime.Now())
            End If

        End If
    End Sub
End Class

End Namespace
