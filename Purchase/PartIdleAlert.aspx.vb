'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-01-10
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   View Part Idle Alert
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace tcpc

Partial Class PartIdleAlert
        Inherits BasePage
    Public chk As New adamClass
    Dim strSql As String
    Dim nRet As Integer
    Dim dst As DataSet
    'Protected WithEvents ltlAlert As Literal

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
        If Not IsPostBack() Then 
            whplacedropdownlist()
            Dim params As SqlParameter
            params = New SqlParameter("@type", "PART")
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, "PartProd_GetAlert", params) >= 0 Then
                BindData()
            End If
        End If
        ltlAlert.Text = "Form1.txtCode.focus();"
    End Sub

    Sub whplacedropdownlist()
        Dim reader As SqlDataReader
        Dim ls As New ListItem
        ls.Text = "--"
        ls.Value = "0"
        warehouseDDL.Items.Add(ls)

        strSql = " Select w.warehouseID, w.name From warehouse w "
        If Session("uRole") <> 1 Then
            strSql &= " Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "'"
        End If
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1)
            warehouseDDL.Items.Add(ls)
        End While
        reader.Close()
    End Sub

    Sub BindData()
        Dim total As Decimal = 0.0
        strSql = " Select code, category, lastDate, qty, warehouse, description From PartProdIdleAlert "
        If txtCode.Text.Trim().Length > 0 Then
            strSql &= " Where Lower(code) like N'" & chk.sqlEncode(txtCode.Text.Trim().ToLower().Replace("*", "%")) & "'"
            If txtCategory.Text.Trim().Length > 0 Then
                strSql &= " And Lower(category) like N'" & chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")) & "'"
            End If
            If txtDesc.Text.Trim().Length > 0 Then
                strSql &= " And Lower(description) like N'%" & chk.sqlEncode(txtDesc.Text.Trim().ToLower()) & "%'"
            End If
            If warehouseDDL.SelectedIndex > 0 Then
                strSql &= " And warehouse='" & warehouseDDL.SelectedItem.Text.Trim() & "'"
            End If
        ElseIf txtCategory.Text.Trim().Length > 0 Then
            strSql &= " Where Lower(category) like N'" & chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")) & "'"
            If txtDesc.Text.Trim().Length > 0 Then
                strSql &= " And Lower(description) like N'%" & chk.sqlEncode(txtDesc.Text.Trim().ToLower()) & "%'"
            End If
            If warehouseDDL.SelectedIndex > 0 Then
                strSql &= " And warehouse='" & warehouseDDL.SelectedItem.Text.Trim() & "'"
            End If
        ElseIf txtDesc.Text.Trim().Length > 0 Then
            strSql &= " Where Lower(description) like N'%" & chk.sqlEncode(txtDesc.Text.Trim().ToLower()) & "%'"
            If warehouseDDL.SelectedIndex > 0 Then
                strSql &= " And warehouse='" & warehouseDDL.SelectedItem.Text.Trim() & "'"
            End If
        ElseIf warehouseDDL.SelectedIndex > 0 Then
            strSql &= " Where warehouse='" & warehouseDDL.SelectedItem.Text.Trim() & "'"
        End If

        strSql &= " Order By lastDate DESC "
        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Session("EXHeader") = ""
        Session("EXSQL") = strSql
        Session("EXTitle") = "200^<b>部件号</b>~^<b>部件分类</b>~^120^<b>最后操作日期</b>~^<b>库存数量</b>~^<b>仓库名称</b>~^500^<b>部件描述</b>~^"

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("part_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("format_last_date", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("last_date", System.Type.GetType("System.DateTime")))
        dtl.Columns.Add(New DataColumn("category", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("totalqty", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("total_qty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("warehouse", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("part_code") = .Rows(i).Item(0).ToString().Trim()
                    drow.Item("format_last_date") = Format(.Rows(i).Item(2), "yyyy-MM-dd")
                    drow.Item("last_date") = Convert.ToDateTime(.Rows(i).Item(2)).ToShortDateString().Trim()
                    drow.Item("category") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("totalqty") = Format(.Rows(i).Item(3), "##,##0.00")
                    drow.Item("total_qty") = .Rows(i).Item(3)
                    total = total + CDbl(.Rows(i).Item(3))
                    drow.Item("warehouse") = .Rows(i).Item(4).ToString().Trim()
                    drow.Item("description") = .Rows(i).Item(5).ToString().Trim()
                    dtl.Rows.Add(drow)
                Next
                lblCount.Text = "合计:" & Format(total, "##,##0.00")
            Else
                lblCount.Text = "&nbsp;"
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)

            Session("orderby") = "last_date"

            Try
                dvw.Sort = Session("orderby") & Session("orderdir")
                dgAlert.DataSource = dvw
                dgAlert.DataBind()
            Catch
            End Try
    End Sub

    Private Sub dgAlert_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgAlert.PageIndexChanged
        dgAlert.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub dgAlert_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgAlert.SortCommand
        Session("orderby") = e.SortExpression.ToString()
        If Session("orderdir") = " ASC" Then
            Session("orderdir") = " DESC"
        Else
            Session("orderdir") = " ASC"
        End If
        BindData()
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        BindData()
    End Sub
End Class

End Namespace
