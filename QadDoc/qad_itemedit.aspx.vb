Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
    Partial Class qad_itemedit
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strSql As String
        Dim ds As DataSet
        Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
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
            ltlAlert.Text = ""
            If Not IsPostBack Then
                BindItemData(1) '因初始加载页面时，数据太多，运行很慢2014-03-05 wangcaixia
            End If
        End Sub
        
        Sub BindItemData(ByVal para1 As Integer)
            If para1 = 1 Then
                strSql = " select top 100 id ,qad,status,desc0,oldcode from qaddoc.dbo.qad_items where qad is not null"
            Else
                strSql = " select id ,qad,status,desc0,oldcode from qaddoc.dbo.qad_items where qad is not null"
            End If

            If txbqad.Text.Trim <> "" Then
                strSql &= " and qad like '%" & txbqad.Text.Trim & "%' "
            End If
            If txbdesc.Text.Trim <> "" Then
                strSql &= " and desc0 like N'%" & txbdesc.Text.Trim & "%' "
            End If
            If txbstatus.Text.Trim <> "" Then
                strSql &= " and status = N'" & txbstatus.Text.Trim & "'"
            End If
            strSql &= " order by qad "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
            Dim dtl As New DataTable
            Dim total As Integer = 0
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("qad", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("desc0", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("status1", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("desc01", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("oldcode", System.Type.GetType("System.String")))

            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("gsort") = i + 1
                        drow.Item("id") = .Rows(i).Item("id").ToString().Trim()
                        drow.Item("qad") = .Rows(i).Item("qad").ToString().Trim()
                        drow.Item("status") = .Rows(i).Item("status").ToString().Trim()
                        drow.Item("desc0") = .Rows(i).Item("desc0").ToString().Trim()
                        drow.Item("status1") = .Rows(i).Item("status").ToString().Trim()
                        drow.Item("desc01") = .Rows(i).Item("desc0").ToString().Trim()
                        drow.Item("oldcode") = .Rows(i).Item("oldcode").ToString().Trim()
                        dtl.Rows.Add(drow)
                        total = total + 1
                    Next
                End If
            End With
            ds.Reset()

            Label1.Text = "Total: " & total.ToString()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Try
                DgItem.DataSource = dvw
                DgItem.DataBind()
            Catch
            End Try
        End Sub

        Private Sub DgItem_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DgItem.PageIndexChanged
            DgItem.CurrentPageIndex = e.NewPageIndex
            DgItem.SelectedIndex = -1
            BindItemData(0)
        End Sub

        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgItem.CancelCommand
            DgItem.EditItemIndex = -1
            BindItemData(0)
        End Sub

        Private Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
            BindItemData(0)
            'txbqad.Text = ""
            'txbstatus.Text = ""
            'txbdesc.Text = "" 
        End Sub

        Private Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
            If txbqad.Text.Trim.Length <> 14 Or IsNumeric(txbqad.Text.Trim) = False Then
                ltlAlert.Text = "alert('Item is incorrect.');"
                Exit Sub
            End If

            strSql = "select count(id) from qaddoc.dbo.qad_items where qad = '" & txbqad.Text.Trim & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                ltlAlert.Text = "alert('Item is exist.');"
                Exit Sub
            End If

            If txbstatus.Text.Trim.Length > 10 Then
                ltlAlert.Text = "alert('The max length is 10.');"
                Exit Sub
            End If
            If txbdesc.Text.Trim.Length > 500 Then
                ltlAlert.Text = "alert('The max length is 500.');"
                Exit Sub
            End If

            'add wangcaixia 添加部件号
            Dim oldItem As String = ""
            strSql = "Select code from tcpc0.dbo.Items where item_qad = '" & txbqad.Text.Trim & "'"
            Dim reader As SqlDataReader
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While (reader.Read())
                oldItem = reader("code").ToString()
            End While

            strSql = "insert into qaddoc.dbo.qad_items(qad,status,desc0,oldcode) values('" & txbqad.Text.Trim & "',N'" & txbstatus.Text.Trim & "',N'" & txbdesc.Text.Trim & "',N'" & oldItem & "')"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            BindItemData(0)
            DgItem.CurrentPageIndex = 0
            DgItem.SelectedIndex = -1
            'txbqad.Text = "" 
            txbstatus.Text = ""
            txbdesc.Text = ""
            ltlAlert.Text = "alert('Add successfully!');"
        End Sub

        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgItem.UpdateCommand

            Dim strstatus As String = CType(e.Item.FindControl("txbsta"), TextBox).Text.Trim
            Dim strdesc As String = CType(e.Item.FindControl("Textbox1"), TextBox).Text.Trim

            If (strstatus.Trim.Length <= 0) Or strstatus.Trim.Length > 10 Then
                ltlAlert.Text = "alert('Status is required and the max length is 10.')"
                Exit Sub
            End If
            If strdesc.Trim.Length <= 0 Or strdesc.Trim.Length > 500 Then
                ltlAlert.Text = "alert('Description is required and max length is 500.')"
                Exit Sub
            End If

            strSql = "update qaddoc.dbo.qad_items set status = N'" & strstatus & "',desc0 = N'" & strdesc & "' where qad = '" & e.Item.Cells(1).Text() & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            DgItem.EditItemIndex = -1
            BindItemData(0)

        End Sub

        Private Sub DgItem_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgItem.EditCommand
            DgItem.EditItemIndex = e.Item.ItemIndex
            BindItemData(0)
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgItem.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                strSql = "select count(*) from qaddoc.dbo.documentitem where qad='" & e.Item.Cells(1).Text() & "'  "
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                    ltlAlert.Text = "alert('It is not able to be deleted. Some items have associated with it');"
                    Exit Sub
                End If
                strSql = "Delete From qaddoc.dbo.qad_items where qad = '" & e.Item.Cells(1).Text() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql) 
                DgItem.EditItemIndex = -1
                BindItemData(0)
                ltlAlert.Text = "alert('Delete qad " & e.Item.Cells(1).Text() & " successfully!');"
            End If
        End Sub

        Private Sub DgItem_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DgItem.ItemCreated
            'Select Case e.Item.ItemType
            '    Case ListItemType.Item
            '        Dim myDeleteButton As TableCell
            '        myDeleteButton = e.Item.Cells(5)
            '        myDeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to delete it?');")

            '    Case ListItemType.AlternatingItem
            '        Dim myDeleteButton As TableCell
            '        myDeleteButton = e.Item.Cells(5)
            '        myDeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to delete it?');")

            '    Case ListItemType.EditItem
            '        Dim myDeleteButton As TableCell
            '        myDeleteButton = e.Item.Cells(5)
            '        myDeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to delete it?');")

            'End Select
        End Sub

    End Class

End Namespace


