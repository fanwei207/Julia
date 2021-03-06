Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

Partial Class DogPartInHis
        Inherits BasePage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgCost As System.Web.UI.WebControls.DataGrid
    Dim strSql As String
    Dim chk As New adamClass

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

    Sub BindData()
        Dim dst As DataSet
        strSql = " SELECT dph.dog_partin_id,dph.procurement_code,isnull(dph.rate,'1') as rate,dph.prod_id AS part,dph.prod_qty,pl1.company_code as delivery_code,isnull(dph.first_partin_date,'') as first_partin_date ," _
               & " isnull(dph.last_partin_date,'') as last_partin_date ,dph.notes,dph.createdBy,dph.createdDate,dph.status,dph.prod_order_detail_id,u.loginName,pl2.company_code as manufactory_code," _
               & " po.order_code,pod.prod_id,i.code,ii.code AS partcode" _
               & " FROM Dog_PartIn_his dph LEFT OUTER JOIN Product_order_detail pod ON " _
               & " pod.prod_order_detail_id = dph.prod_order_detail_id LEFT OUTER JOIN " _
               & " Product_orders po ON po.prod_order_id = pod.prod_order_id AND  " _
               & " po.order_status <> 'CLOSE' INNER JOIN " _
               & " part_orders pto ON pto.part_order_id = dph.part_order_id LEFT OUTER JOIN " _
               & " tcpc0.dbo.Items i ON i.id = pod.prod_id INNER JOIN " _
               & " tcpc0.dbo.Items ii ON ii.id = dph.prod_id INNER JOIN " _
               & " tcpc0.dbo.users u ON dph.createdBy = u.userID INNER JOIN " _
               & " Procurements pro ON ii.code LIKE '%' + pro.code + '%' LEFT OUTER JOIN " _
               & " tcpc0.dbo.Companies pl1 ON  " _
               & " pl1.company_id = dph.manufactory_id LEFT OUTER JOIN " _
               & " tcpc0.dbo.Companies pl2 ON pl2.company_id = dph.delivery_id " _
               & " WHERE dph.dog_partin_id IS NOT NULL "

        If txtOrder.Text.Trim.Length > 0 Then
            strSql = strSql & " AND po.order_code like '%" & txtOrder.Text.Trim() & "%'"
        End If

        If txtProd.Text.Trim.Length > 0 Then
            strSql = strSql & " AND i.code like '%" & txtProd.Text.Trim() & "%'"
        End If

        If txtPart.Text.Trim.Length > 0 Then
            strSql = strSql & " AND ii.code like '%" & txtPart.Text.Trim() & "%'"
        End If

        strSql &= " ORDER BY po.order_code desc,i.code,ii.code, dph.createdDate DESC"
        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("dog_partin_id", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("procurement_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("rate", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("prod_qty", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("delivery_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("first_partin_date", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("last_partin_date", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("notes", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("loginName", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("createdDate", System.Type.GetType("System.DateTime")))
        dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("manufactory_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("order_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
        With dst.Tables(0)

            Dim i As Integer
            Dim drow As DataRow
            For i = 0 To .Rows.Count - 1
                drow = dtl.NewRow
                drow.Item("gsort") = i + 1
                drow.Item("procurement_code") = .Rows(i).Item(1).ToString().Trim()
                drow.Item("rate") = .Rows(i).Item("rate")
                drow.Item("prod_qty") = CInt(.Rows(i).Item("prod_qty")) * CInt(.Rows(i).Item("rate"))
                drow.Item("delivery_code") = Convert.ToString(.Rows(i).Item("delivery_code").ToString().Trim())
                drow.Item("manufactory_code") = Convert.ToString(.Rows(i).Item("manufactory_code").ToString().Trim())
                If CDate(.Rows(i).Item("first_partin_date")) = CDate("1900.1.1") Then
                    drow.Item("first_partin_date") = ""
                Else
                    drow.Item("first_partin_date") = Convert.ToString(Format(.Rows(i).Item("first_partin_date"), "yyyy-MM-dd").ToString.Trim())
                End If
                If CDate(.Rows(i).Item("last_partin_date")) = CDate("1900.1.1") Then
                    drow.Item("last_partin_date") = ""
                Else
                    drow.Item("last_partin_date") = Convert.ToString(Format(.Rows(i).Item("last_partin_date"), "yyyy-MM-dd").ToString.Trim())
                End If
                drow.Item("notes") = Convert.ToString(.Rows(i).Item("notes").ToString().Trim())
                drow.Item("loginName") = Convert.ToString(.Rows(i).Item("loginName").ToString().Trim())
                drow.Item("createdDate") = Format(.Rows(i).Item("createdDate"), "yyyy-MM-dd").ToString.Trim()
                If .Rows(i).Item("status") = 0 Then
                    drow.Item("status") = "新建"
                ElseIf .Rows(i).Item("status") = 1 Then
                    drow.Item("status") = "更新"
                Else
                    drow.Item("status") = "删除"
                End If
                drow.Item("order_code") = .Rows(i).Item("order_code")
                drow.Item("code") = .Rows(i).Item("code")
                drow.Item("partcode") = .Rows(i).Item("partcode")
                dtl.Rows.Add(drow)
            Next

        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)

            Session("orderby") = "gsort"


            Try
                dvw.Sort = Session("orderby") & " " & Session("orderdir")
                dgHis.DataSource = dvw
                dgHis.DataBind()
            Catch
            End Try

        End Sub

    Private Sub dgHis_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgHis.PageIndexChanged
        dgHis.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub dgHis_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgHis.SortCommand
        Session("orderby") = e.SortExpression
        If Session("orderdir") = "ASC" Then
            Session("orderdir") = "DESC"
        Else
            Session("orderdir") = "ASC"
        End If
        BindData()
    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        BindData()
        btnShowAll.Visible = True
    End Sub

    Private Sub btnShowAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAll.Click
        txtOrder.Text = ""
        txtProd.Text = ""
        txtPart.Text = ""
        BindData()
        btnShowAll.Visible = False
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(chk.urlRand("/Purchase/purchasenum.aspx"), True)
    End Sub
End Class

End Namespace
