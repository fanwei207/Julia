'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-01-18
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   View Part Tran Over
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace tcpc

Partial Class PurchaseQtyOver
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
            BindData()
        End If
        ltlAlert.Text = "Form1.txtOrder.focus();"
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
        strSql = " Select pto.id, po.part_order_code, i.code, ic.name, pto.totalQty, pto.enterQty, " _
               & " pto.overQty, w.name, i.description, i.id as partid , po.part_order_id, w.warehouseID, Isnull(pto.tran_date,''), pto.receivecode, u.userName  " _
               & " From Part_Tran_Over pto " _
               & " Inner Join tcpc0.dbo.Items i On i.id=pto.partID  " _
               & " Inner Join tcpc0.dbo.ItemCategory ic On i.category=ic.id " _
               & " Inner Join warehouse w On pto.warehouseID=w.warehouseID "
        If Session("uRole") <> 1 Then
            strSql &= " Inner Join User_Warehouse uw On uw.warehouseID=w.warehouseID And uw.userID='" & Session("uID") & "'"
        End If
        strSql &= " Inner Join part_orders po On pto.orderID=po.part_order_id And Isnull(po.status,0)<>1 " _
               & " Inner Join tcpc0.dbo.Users u On u.userID=pto.createdBy "
        If txtOrder.Text.Trim().Length > 0 Then
            strSql &= " Where Lower(po.part_order_code) like '" & chk.sqlEncode(txtOrder.Text.Trim().ToLower().Replace("*", "%")) & "'"
            If txtCode.Text.Trim().Length > 0 Then
                strSql &= " And Lower(i.code) like N'" & chk.sqlEncode(txtCode.Text.Trim().ToLower().Replace("*", "%")) & "'"
            End If
            If txtCategory.Text.Trim().Length > 0 Then
                strSql &= " And Lower(i.category) like N'" & chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")) & "'"
            End If
            If txtDesc.Text.Trim().Length > 0 Then
                strSql &= " And Lower(i.description) like N'%" & chk.sqlEncode(txtDesc.Text.Trim().ToLower()) & "%'"
            End If
            If warehouseDDL.SelectedIndex > 0 Then
                strSql &= " And pto.warehouseID='" & warehouseDDL.SelectedValue & "'"
            End If
        ElseIf txtCode.Text.Trim().Length > 0 Then
            strSql &= " And Lower(i.code) like N'" & chk.sqlEncode(txtCode.Text.Trim().ToLower().Replace("*", "%")) & "'"
            If txtCategory.Text.Trim().Length > 0 Then
                strSql &= " And Lower(i.category) like N'" & chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")) & "'"
            End If
            If txtDesc.Text.Trim().Length > 0 Then
                strSql &= " And Lower(i.description) like N'%" & chk.sqlEncode(txtDesc.Text.Trim().ToLower()) & "%'"
            End If
            If warehouseDDL.SelectedIndex > 0 Then
                strSql &= " And pto.warehouseID='" & warehouseDDL.SelectedValue & "'"
            End If
        ElseIf txtCategory.Text.Trim().Length > 0 Then
            strSql &= " And Lower(i.category) like N'" & chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")) & "'"
            If txtDesc.Text.Trim().Length > 0 Then
                strSql &= " And Lower(i.description) like N'%" & chk.sqlEncode(txtDesc.Text.Trim().ToLower()) & "%'"
            End If
            If warehouseDDL.SelectedIndex > 0 Then
                strSql &= " And pto.warehouseID='" & warehouseDDL.SelectedValue & "'"
            End If
        ElseIf txtDesc.Text.Trim().Length > 0 Then
            strSql &= " And Lower(i.description) like N'%" & chk.sqlEncode(txtDesc.Text.Trim().ToLower()) & "%'"
            If warehouseDDL.SelectedIndex > 0 Then
                strSql &= " And pto.warehouseID='" & warehouseDDL.SelectedValue & "'"
            End If
        ElseIf warehouseDDL.SelectedIndex > 0 Then
            strSql &= " And pto.warehouseID='" & warehouseDDL.SelectedValue & "'"
        End If

        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("order_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("part_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("category", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("totalqty", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("total_qty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("enterqty", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("enter_qty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("overqty", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("over_qty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("warehouse", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("partid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("orderid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("warehouseID", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("tran_date", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("trandate", System.Type.GetType("System.DateTime")))
        dtl.Columns.Add(New DataColumn("receive", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("user", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                    drow.Item("order_code") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("part_code") = .Rows(i).Item(2).ToString().Trim()
                    drow.Item("category") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("totalqty") = Format(.Rows(i).Item(4), "##,##0.00")
                    drow.Item("total_qty") = .Rows(i).Item(4)
                    drow.Item("enterqty") = Format(.Rows(i).Item(5), "##,##0.00")
                    drow.Item("enter_qty") = .Rows(i).Item(5)
                    drow.Item("overqty") = Format(.Rows(i).Item(6), "##,##0.00")
                    drow.Item("over_qty") = .Rows(i).Item(6)
                    total = total + CDbl(.Rows(i).Item(6))
                    drow.Item("warehouse") = .Rows(i).Item(7).ToString().Trim()
                    drow.Item("description") = .Rows(i).Item(8).ToString().Trim()
                    drow.Item("partid") = .Rows(i).Item(9)
                    drow.Item("orderid") = .Rows(i).Item(10)
                    drow.Item("warehouseID") = .Rows(i).Item(11)
                    drow.Item("tran_date") = Format(.Rows(i).Item(12), "yyyy-MM-dd")
                    drow.Item("trandate") = .Rows(i).Item(12)
                    drow.Item("receive") = .Rows(i).Item(13)
                    drow.Item("user") = .Rows(i).Item(14)
                    dtl.Rows.Add(drow)
                Next
                lblCount.Text = "合计:" & Format(total, "##,##0.00")
            Else
                lblCount.Text = "&nbsp;"
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)

            Session("orderby") = "part_code"

            Try
                dvw.Sort = Session("orderby") & Session("orderdir")
                dgAlert.DataSource = dvw
                dgAlert.DataBind()
            Catch
            End Try
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

    Function getNumericalOrder() As Integer
        Dim ds As DataSet
        Dim codeNoTemp As String

        strSql = " SELECT max(cast(ISNULL(num_order,'0') as int(8))) as stockOrderNo FROM part_orders "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                If IsDBNull(.Rows(0).Item("stockOrderNo")) Then
                    codeNoTemp = 1
                Else
                    codeNoTemp = (CInt(.Rows(0).Item("stockOrderNo").ToString()) + 1).ToString()
                End If
            Else
                codeNoTemp = 1
            End If
        End With
        ds.Reset()
        Return codeNoTemp
    End Function

    Private Sub dgAlert_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAlert.ItemCommand
        If e.CommandName = "AcceptBtn" Then
            Dim params(7) As SqlParameter
            params(0) = New SqlParameter("@whid", Convert.ToInt32(e.Item.Cells(16).Text.Trim))
            params(1) = New SqlParameter("@partid", Convert.ToInt32(e.Item.Cells(14).Text.Trim))
            params(2) = New SqlParameter("@inQty", Convert.ToDecimal(e.Item.Cells(7).Text.Trim))
            params(3) = New SqlParameter("@overQty", Convert.ToDecimal(e.Item.Cells(8).Text.Trim))
            params(4) = New SqlParameter("@orderid", Convert.ToInt32(e.Item.Cells(15).Text.Trim))
            params(5) = New SqlParameter("@uID", Convert.ToInt32(Session("uID")))
            params(6) = New SqlParameter("@num_order", Convert.ToInt32(getNumericalOrder()))
            params(7) = New SqlParameter("@id", Convert.ToInt32(e.Item.Cells(0).Text.Trim))
            SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, "Purchase_PartInOver", params)
        ElseIf e.CommandName = "RejectBtn" Then
            strSql = " Delete From Part_Tran_Over Where id='" & e.Item.Cells(0).Text.Trim() & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        Else
            Exit Sub
        End If
        Response.Redirect(chk.urlRand("/purchase/purchaseQtyOver.aspx"), True)
    End Sub

    Private Sub dgAlert_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgAlert.PageIndexChanged
        dgAlert.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub
End Class

End Namespace
