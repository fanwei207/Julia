Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class DogPartInNumHis
        Inherits BasePage
    Dim strsql As String
    Dim chk As New adamClass
    Dim ds As DataSet

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
    Sub BindData()
        strsql = "SELECT dph.id, dph.procurement_code, isnull(dph.rate,1) as rate, dph.prod_id AS part, " _
               & " dph.prod_qty, pl1.company_code AS manufactory_code, isnull(dph.first_partin_date,'') as first_partin_date, " _
               & " isnull(dph.last_partin_date,'') as last_partin_date, us.loginname, dpd.createdDate, dpd.status, " _
               & " dph.prod_order_detail_id,pl2.company_code AS delivery_code, " _
               & " po.order_code, pod.prod_id, i.code, ii.code AS partcode, isnull(dpd.plan_qty,0) as plan_qty, isnull(dpd.real_qty,0) as real_qty, " _
               & " isnull(dpd.plan_date,'') as plan_date, dpd.notes " _
               & " FROM Dog_PartIn dph LEFT OUTER JOIN Product_order_detail pod ON " _
               & " pod.prod_order_detail_id = dph.prod_order_detail_id LEFT OUTER JOIN " _
               & " Product_orders po ON po.prod_order_id = pod.prod_order_id AND " _
               & " po.order_status <> 'CLOSE' INNER JOIN " _
               & " part_orders pto ON pto.part_order_id = dph.part_order_id LEFT OUTER JOIN " _
               & " tcpc0.dbo.Items i ON i.id = pod.prod_id INNER JOIN " _
               & " tcpc0.dbo.Items ii ON ii.id = dph.prod_id INNER JOIN " _
               & " tcpc0.dbo.users us ON dph.createdBy = us.userID INNER JOIN " _
               & " Procurements pro ON ii.code LIKE '%' + pro.code + '%' LEFT OUTER JOIN " _
               & " tcpc0.dbo.Companies pl1 ON pl1.company_id = dph.manufactory_id LEFT OUTER JOIN " _
               & " tcpc0.dbo.Companies pl2 ON pl2.company_id = dph.delivery_id INNER JOIN " _
               & " Dog_PartIn_Detail_his dpd ON dpd.dog_partin_id = dph.id " _
               & " WHERE (dph.id IS NOT NULL) "
        If txtOrder.Text.Trim.Length > 0 Then
            strsql = strsql & " AND po.order_code like '%" & txtOrder.Text.Trim() & "%'"
        End If
        If txtProd.Text.Trim.Length > 0 Then
            strsql = strsql & " AND i.code like '%" & txtProd.Text.Trim() & "%'"
        End If
        If txtPart.Text.Trim.Length > 0 Then
            strsql = strsql & " AND ii.code like '%" & txtPart.Text.Trim() & "%'"
        End If
        strsql &= " ORDER BY po.order_code,i.code,ii.code,dpd.plan_date"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strsql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("procurement_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("rate", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("prod_qty", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("delivery_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("first_partin_date", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("last_partin_date", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("notes", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("loginName", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("createdDate", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("manufactory_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("order_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("plan_date", System.Type.GetType("System.DateTime")))
        dtl.Columns.Add(New DataColumn("plan_qty", System.Type.GetType("System.Double")))
        dtl.Columns.Add(New DataColumn("real_qty", System.Type.GetType("System.Double")))

        With ds.Tables(0)
            Dim i As Integer
            Dim drow As DataRow
            For i = 0 To .Rows.Count - 1
                drow = dtl.NewRow
                drow.Item("gsort") = i + 1
                drow.Item("procurement_code") = .Rows(i).Item("procurement_code").ToString().Trim()
                drow.Item("rate") = CInt(.Rows(i).Item("rate"))
                drow.Item("prod_qty") = CInt(.Rows(i).Item("prod_qty")) * CInt(.Rows(i).Item("rate"))
                drow.Item("delivery_code") = .Rows(i).Item("delivery_code").ToString().Trim()
                drow.Item("manufactory_code") = .Rows(i).Item("manufactory_code").ToString().Trim()
                If CDate(.Rows(i).Item("first_partin_date")) = CDate("1900.1.1") Then
                    drow.Item("first_partin_date") = ""
                Else
                    drow.Item("first_partin_date") = Format(.Rows(i).Item("first_partin_date"), "yyyy-MM-dd")
                End If
                If CDate(.Rows(i).Item("last_partin_date")) = CDate("1900.1.1") Then
                    drow.Item("last_partin_date") = ""
                Else
                    drow.Item("last_partin_date") = Format(.Rows(i).Item("last_partin_date"), "yyyy-MM-dd")
                End If
                drow.Item("notes") = .Rows(i).Item("notes").ToString().Trim()
                drow.Item("loginName") = .Rows(i).Item("loginName").ToString().Trim()
                drow.Item("createdDate") = Format(.Rows(i).Item("createddate"), "yyyy-MM-dd")
                If .Rows(i).Item("status") = "0" Then
                    drow.Item("status") = "新建"
                ElseIf .Rows(i).Item("status") = "1" Then
                    drow.Item("status") = "更新"
                Else
                    drow.Item("status") = "删除"
                End If
                drow.Item("order_code") = .Rows(i).Item("order_code")
                drow.Item("code") = .Rows(i).Item("code")
                drow.Item("partcode") = .Rows(i).Item("partcode")
                If CDate(.Rows(i).Item("plan_date")) <> CDate("1900.1.1") Then
                    drow.Item("plan_date") = .Rows(i).Item("plan_date")
                End If
                drow.Item("plan_qty") = .Rows(i).Item("plan_qty")
                drow.Item("real_qty") = .Rows(i).Item("real_qty")
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
        dgHis.CurrentPageIndex = 0
        BindData()
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(chk.urlRand("/Purchase/purchasenum.aspx"), True)
    End Sub
End Class

End Namespace
