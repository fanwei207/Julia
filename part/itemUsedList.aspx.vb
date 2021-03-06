'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-5-2
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Add Item Code to export used by list
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class itemUsedList
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer
    Dim strsql As String

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label


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
            If Request("pg") <> Nothing Then
                dgUse.CurrentPageIndex = CInt(Request("pg"))
            End If
            BindData()
        End If
        ltlAlert.Text = "Form1.txtItem.focus();"
        BtnClean.Attributes.Add("onclick", "return confirm('确定要清除查询内容吗?');")
    End Sub

    Sub BindData()
        Dim dst As DataSet
        strsql = " Select iul.itemID, i.code From Items i Inner Join ItemUsedList iul On i.ID=iul.itemID And iul.userID='" & Session("uID") & "' And iul.plantID='" & Session("plantCode") & "' Where i.status<>2 "
        dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)

        Dim dtlUse As New DataTable
        dtlUse.Columns.Add(New DataColumn("itemID", System.Type.GetType("System.String")))
        dtlUse.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
        dtlUse.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))

        With dst.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                Dim drowUse As DataRow
                For i = 0 To .Rows.Count - 1
                    drowUse = dtlUse.NewRow()
                    drowUse.Item("itemID") = .Rows(i).Item(0).ToString().Trim()
                    drowUse.Item("code") = .Rows(i).Item(1).ToString().Trim()
                    drowUse.Item("gsort") = i + 1
                    dtlUse.Rows.Add(drowUse)
                Next
            End If
        End With
        Dim dvUse As DataView
        dvUse = New DataView(dtlUse)

            Session("orderby") = "gsort"

            Try
                dvUse.Sort = Session("orderby") & Session("orderdir")
                dgUse.DataSource = dvUse
                dgUse.DataBind()
            Catch
            End Try
    End Sub

    Private Sub dgUse_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUse.ItemCreated
        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing ButtonColumn
                myDeleteButton = e.Item.Cells(3)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")

            Case ListItemType.AlternatingItem
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing your ButtonColumn
                myDeleteButton = e.Item.Cells(3)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")
            Case ListItemType.EditItem
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing your ButtonColumn
                myDeleteButton = e.Item.Cells(3)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")
        End Select
    End Sub

    Private Sub dgUse_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgUse.PageIndexChanged
        dgUse.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub dgUse_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgUse.SortCommand
        Session("orderby") = e.SortExpression.ToString()
        If Session("orderdir") = " ASC" Then
            Session("orderdir") = " DESC"
        Else
            Session("orderdir") = " ASC"
        End If
        BindData()
    End Sub

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Dim itemID As String = Nothing
        strsql = " Select ID From Items Where Code=N'" & chk.sqlEncode(txtItem.Text.Trim()) & "' And status<>2 "
        itemID = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)
        If itemID = Nothing Or itemID = "0" Then
            ltlAlert.Text = "alert('输入的" & txtItem.Text.Trim() & "不存在！');"
            Exit Sub
        Else
            strsql = " Insert Into ItemUsedList(itemID, userID, plantID) Values('" & itemID.Trim() & "','" & Session("uID") & "','" & Session("plantCode") & "')"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

            Response.Redirect(chk.urlRand("/part/itemUsedList.aspx?pg=" & dgUse.CurrentPageIndex), True)
        End If
    End Sub

    Private Sub DeleteBtn_Click(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgUse.ItemCommand
        If e.CommandName.CompareTo("DeleteBtn") = 0 Then
            strsql = " Delete From ItemUsedList Where itemID='" & e.Item.Cells(0).Text.Trim() & "' And userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

            Response.Redirect(chk.urlRand("/part/itemUsedList.aspx?pg=" & dgUse.CurrentPageIndex), True)
        End If
    End Sub

    Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        If radAll.Checked = False And radNoStop.Checked = False Then
            ltlAlert.Text = "alert('请选择导出类别！');"
            Exit Sub
        Else
            If radAll.Checked = True Then
                ltlAlert.Text = "window.open('/part/itemUsedExport.aspx?type=all&s=" & chk.sqlEncode(txtItem.Text.Trim()) & "','itemusedexport','menubar=yes,scrollbars = yes,resizable=yes,width=750,height=500,top=0,left=0') "
            Else
                ltlAlert.Text = "window.open('/part/itemUsedExport.aspx?s=" & chk.sqlEncode(txtItem.Text.Trim()) & "','itemusedexport','menubar=yes,scrollbars = yes,resizable=yes,width=750,height=500,top=0,left=0') "
            End If
        End If
    End Sub

    Private Sub BtnClean_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClean.Click
        strsql = " Delete From ItemUsedList Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

        Response.Redirect(chk.urlRand("/part/itemUsedList.aspx"), True)
    End Sub
End Class

End Namespace
