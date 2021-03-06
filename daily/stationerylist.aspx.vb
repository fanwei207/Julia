Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class stationerylist
        Inherits BasePage
    Dim chk As New adamClass
    Shared start As String
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents startdate As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents enddate As System.Web.UI.WebControls.TextBox
    Protected WithEvents cMsg23 As System.Web.UI.WebControls.CompareValidator
    Shared enddt As String

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
        Dim strSQL, stationarycode As String
        Dim flag As Boolean = False
        Dim ds As DataSet
        Dim orgid As Integer = Session("orgID")
        strSQL = " SELECT s.stationeryTypeID,c.systemCodeName,s.quantity,s.isOut,s.createdDate " _
               & " From Stationery s " _
               & " INNER JOIN tcpc0.dbo.systemCode c ON s.stationeryTypeID=c.systemCodeID " _
               & " INNER JOIN tcpc0.dbo.systemCodeType st ON c.systemCodeTypeID=st.systemCodeTypeID " _
               & " WHERE st.systemCodeTypeName='Stationery Type'and s.organizationID=" & orgid

        If stationery.SelectedValue <> 0 Then
            strSQL = strSQL & " and c.systemCodeName=N'" & stationery.SelectedItem.Text & "'"
        End If

        strSQL = strSQL & "Order by c.systemCodeName"

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("stationeryTypeID", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("systemCodeName", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("inQty", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("outQty", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("preQty", System.Type.GetType("System.Int32")))
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i, g, j As Integer
                Dim dr1 As DataRow
                Dim pre As Integer = 0
                Dim inqty As Integer = 0
                Dim outqty As Integer = 0
                g = 1
                For i = 0 To .Rows.Count - 1

                    If stationarycode = .Rows(i).Item(1).ToString().Trim() Then
                        If .Rows(i).Item(3) = 0 Then
                            inqty = inqty + .Rows(i).Item(2)
                        Else
                            outqty = outqty + .Rows(i).Item(2)
                        End If
                    Else
                        If flag = True Then
                            pre = pre + inqty - outqty
                            dr1.Item("inQty") = inqty.ToString().Trim()
                            dr1.Item("outQty") = outqty.ToString().Trim()
                            dr1.Item("preQty") = pre.ToString().Trim()

                            dt.Rows.Add(dr1)
                            g = g + 1
                        End If
                        inqty = 0
                        outqty = 0
                        pre = 0
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = g
                        dr1.Item("systemCodeName") = .Rows(i).Item(1).ToString().Trim()
                        stationarycode = .Rows(i).Item(1).ToString().Trim()
                        If .Rows(i).Item(3) = 0 Then
                            inqty = inqty + .Rows(i).Item(2)
                        Else
                            outqty = outqty + .Rows(i).Item(2)
                        End If
                        flag = True
                    End If
                Next
                If flag = True Then
                    pre = pre + inqty - outqty
                    dr1.Item("inQty") = inqty.ToString().Trim()
                    dr1.Item("outQty") = outqty.ToString().Trim()
                    dr1.Item("preQty") = pre.ToString().Trim()
                    dt.Rows.Add(dr1)
                End If
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        BindData()
        'Button3.Visible = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        stationery.SelectedValue = 0
        BindData()
        Button3.Visible = False
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
    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub
End Class

End Namespace

