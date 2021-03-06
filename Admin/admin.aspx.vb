Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class admin
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
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT p.plantID,p.description,p.plantCode " _
                   & " From plants p"
            If Session("uRole") > 1 Then
                strSQL &= " Inner Join user_plant up on up.plantID=p.plantID and up.userID='" & Session("uID") & "'"
            End If
	    'strSQL &= " Where Isnull(p.inServiceSys,0) <> 0 "
            strSQL &= " Order by p.plantid"
            ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        'dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim() & " (" & .Rows(i).Item(2).ToString().Trim() & ")"
                        dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim()
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
        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Session("PlantCode") = CInt(e.Item.Cells(0).Text)
                Session("orgName") = e.Item.Cells(2).Text

                If Session("PlantCode") > 100 Then
                    Session("PlantCode") = (Session("PlantCode") - 100).ToString()
                    Session("temp") = 1
                Else
                    Session("temp") = 0
                End If


                ltlAlert.Text = "window.top.location.reload();"
                'Response.Redirect("/MasterPage.aspx", True)
            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace
