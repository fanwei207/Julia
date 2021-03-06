Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

Partial Class stationeryinlist
        Inherits BasePage
    Protected WithEvents cMsg23 As System.Web.UI.WebControls.CompareValidator
    Dim chk As New adamClass

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
            stationeryDropDownList()
            BindData()
        End If

    End Sub
    Private Sub BindData()
        Literal1.Text = ""

        Dim date1 As New DateTime
        Dim date2 As New DateTime

        Dim strSQL As String
        Dim ds As DataSet
        Dim orgid As Integer = Session("orgID")
        strSQL = " SELECT stationeryID,stationeryTypeID,systemCodeName,quantity,createdDate,s.comments,isnull(c.comments,'0') as price " _
               & " From Stationery s " _
               & " INNER JOIN tcpc0.dbo.systemCode c ON s.stationeryTypeID=c.systemCodeID " _
               & " INNER JOIN tcpc0.dbo.systemCodeType st ON c.systemCodeTypeID=st.systemCodeTypeID " _
               & " WHERE  st.systemCodeTypeName='Stationery Type' and isout=0 and s.organizationID=" & orgid

        If stationery.SelectedValue <> 0 Then
            strSQL = strSQL & " and c.systemCodeName=N'" & stationery.SelectedItem.Text & "'"
        End If
        If startdate.Text <> "" Then
            date1 = CDate(startdate.Text)
            strSQL = strSQL & " and  createdDate >='" & date1 & "'"
        End If
        If enddate.Text <> "" Then
            date2 = CDate(enddate.Text)
            strSQL = strSQL & " and  createdDate <='" & date2.AddDays(1) & "'"
        End If
        strSQL = strSQL & "Order by createdDate desc"

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("stationeryID", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("systemCodeName", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("inQty", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("inDate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("inprice", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("intotal", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("comments", System.Type.GetType("System.String")))
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                Dim pre As Integer = 0
                For i = 0 To .Rows.Count - 1

                    dr1 = dt.NewRow()
                    dr1.Item("stationeryID") = .Rows(i).Item(0).ToString().Trim()
                    dr1.Item("gsort") = i + 1
                    dr1.Item("systemCodeName") = .Rows(i).Item(2).ToString().Trim()
                    dr1.Item("inQty") = .Rows(i).Item(3).ToString().Trim()
                    dr1.Item("inDate") = .Rows(i).Item(4).Toshortdatestring().Trim()
                    dr1.Item("inprice") = .Rows(i).Item(6).ToString.Trim()
                    dr1.Item("intotal") = (CDec(.Rows(i).Item(6).ToString.Trim()) * CDec(.Rows(i).Item(3).ToString().Trim())).ToString()
                    dr1.Item("comments") = .Rows(i).Item(5).ToString().Trim()
                    dt.Rows.Add(dr1)
                Next

            End If
        End With
        Dim dv As DataView
        dv = New DataView(dt)

            Session("orderby") = "gsort"

            Try
                dv.Sort = Session("orderby") & Session("orderdir")
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
    End Sub
    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        Session("orderby") = e.SortExpression.ToString()
        If Session("orderdir") = " ASC" Then
            Session("orderdir") = " DESC"
        Else
            Session("orderdir") = " ASC"
        End If
        BindData()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect(chk.urlRand("stationeryin.aspx"))
    End Sub
    Sub stationeryDropDownList()
        Dim query As String
        Dim cmd As SqlCommand
        Dim con As SqlConnection
        Dim reader As SqlDataReader
        Dim ls As New ListItem
        query = " select c.systemCodeID,c.systemCodeName from tcpc0.dbo.systemCode c " _
              & " inner join tcpc0.dbo.systemCodeType s on c.systemCodeTypeID=s.systemCodeTypeID " _
              & " where s.systemCodeTypeName='Stationery Type' order by c.systemCodeID"
        con = New SqlConnection(chk.dsnx)
        cmd = New SqlCommand(query, con)
        con.Open()
        reader = cmd.ExecuteReader()
        ls = New ListItem
        ls.Text = "--"
        ls.Value = 0
        stationery.Items.Add(ls)
        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1)
            stationery.Items.Add(ls)
        End While
        reader.Close()
        con.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Literal1.Text = ""
        BindData()
    End Sub

    Private Sub dgreturnDetail_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(7)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

            Case ListItemType.AlternatingItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(7)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

            Case ListItemType.EditItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(7)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")
                'CType(e.Item.Cells(6).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(40)
        End Select
    End Sub
    Public Sub Edit_delete(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Dim strSql As String
        strSql = " Delete From Stationery Where stationeryID = " & e.Item.Cells(8).Text.Trim()
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        BindData()
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub
End Class

End Namespace
