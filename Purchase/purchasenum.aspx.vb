Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class purchasenum
        Inherits BasePage
    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal
    Dim strSQL As String
    Dim chk As New adamClass
    Dim reader As SqlDataReader
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
        ltlAlert.Text = ""
        If Not IsPostBack Then  
            Dim ls As New ListItem
            strSQL &= "SELECT pr.id, pr.code,pr.name FROM Procurements pr "
            If Session("uRole") > 1 Then
                strSQL &= " INNER JOIN User_Procurement up ON pr.id = up.procurementID and up.userID='" & Session("uID") & "' "
            End If
            strSQL &= " order by pr.name "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(1)
                ls.Text = reader(2)
                dr_type.Items.Add(ls)
            End While
            tx_order_code.Text = Request("order_code")
            tx_code.Text = Request("code")
            BindData()
        End If
    End Sub
    Sub BindData()
        strSQL = " SELECT dp.id,pto.part_order_code, isnull(dp.rate,1) as rate, dp.prod_id, dp.prod_qty,pl1.company_code as manufactory_code,pl2.company_code as delivery_code, isnull(dp.first_partin_date,'') as first_partin_date, isnull(dp.last_partin_date,'') as last_partin_date,dp.notes, po.order_code, i.code as code,ii.code as partcode,dp.prod_order_detail_id  " _
                & " FROM Dog_PartIn dp left JOIN Product_order_detail pod ON pod.prod_order_detail_id = dp.prod_order_detail_id " _
                & " left JOIN product_orders po ON po.prod_order_id = pod.prod_order_id and order_status<>'CLOSE' " _
                & " inner join part_orders pto on pto.part_order_id=dp.part_order_id " _
                & " left JOIN  tcpc0.dbo.Items i ON i.id = pod.prod_id " _
                & " inner join tcpc0.dbo.Items ii on ii.id=dp.prod_id " _
                & " inner join Procurements pro on ii.code like '%'+pro.code+'%'" _
                & " left join tcpc0.dbo.companies pl1 on pl1.company_id=dp.manufactory_id " _
                & " left join tcpc0.dbo.companies pl2 on pl2.company_id=dp.delivery_id "
        If Session("uRole") > 1 Then
            strSQL &= " inner join User_Procurement upro on pro.id=upro.procurementID and upro.userID='" & Session("uID") & "' "
        End If
        strSQL &= " where dp.id is not null "
        If tx_order_code.Text.Trim.Length > 0 Then
            strSQL &= " and po.order_code='" & tx_order_code.Text.Trim() & "'"
        End If
        If tx_code.Text.Trim.Length > 0 Then
            strSQL &= " and i.code='" & tx_code.Text.Trim() & "'"
        End If
        If dr_type.SelectedValue <> "" Then
            strSQL &= " and ii.code like '%'+'" & dr_type.SelectedValue & "'+'%'"
        End If
        strSQL &= " order by po.order_code,i.code,ii.code "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("did", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("procurement_code", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("rate", System.Type.GetType("System.Double")))
        dt.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("prod_qty", System.Type.GetType("System.Double")))
        dt.Columns.Add(New DataColumn("manufactory_code", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("delivery_code", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("first_partin_date", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("last_partin_date", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("notes", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("order_code", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("prod_code", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        With ds.Tables(0)
            Dim i As Integer
            Dim dr As DataRow
            If .Rows.Count > 0 Then
                For i = 0 To .Rows.Count - 1
                    dr = dt.NewRow()
                    dr.Item("gsort") = i + 1
                    dr.Item("id") = .Rows(i).Item("id").ToString.Trim()
                    dr.Item("did") = .Rows(i).Item("prod_order_detail_id").ToString.Trim()
                    dr.Item("procurement_code") = .Rows(i).Item("part_order_code").ToString.Trim()
                    dr.Item("rate") = .Rows(i).Item("rate")
                    dr.Item("code") = .Rows(i).Item("partcode").ToString.Trim()
                    dr.Item("prod_qty") = .Rows(i).Item("prod_qty") * .Rows(i).Item("rate")
                    dr.Item("manufactory_code") = .Rows(i).Item("manufactory_code").ToString.Trim()
                    dr.Item("delivery_code") = .Rows(i).Item("delivery_code").ToString.Trim()
                    If CDate(.Rows(i).Item("first_partin_date")) = CDate("1900.1.1") Then
                        dr.Item("first_partin_date") = ""
                    Else
                        dr.Item("first_partin_date") = Format(.Rows(i).Item("first_partin_date"), "yyyy-MM-dd")
                    End If
                    If CDate(.Rows(i).Item("last_partin_date")) = CDate("1900.1.1") Then
                        dr.Item("last_partin_date") = ""
                    Else
                        dr.Item("last_partin_date") = Format(.Rows(i).Item("last_partin_date"), "yyyy-MM-dd")
                    End If
                    dr.Item("notes") = .Rows(i).Item("notes").ToString.Trim()
                    dr.Item("order_code") = .Rows(i).Item("order_code").ToString.Trim()
                    dr.Item("prod_code") = .Rows(i).Item("code").ToString.Trim()
                    dt.Rows.Add(dr)
                Next
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dt)

            Session("orderby") = "gsort"

            Try
                dvw.Sort = Session("orderby") & " " & Session("orderdir")
                dgpur.DataSource = dvw
                dgpur.DataBind()
            Catch
            End Try
    End Sub
    Private Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        ltlAlert.Text = ""
        dgpur.CurrentPageIndex = 0
        BindData()
        'tx_order_code.Text = ""
        'tx_code.Text = ""
    End Sub

    Private Sub dgpur_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgpur.PageIndexChanged
        dgpur.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub
    Private Sub dgpur_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgpur.ItemCommand
        Dim id As String = e.Item.Cells(0).Text()
        Dim did As String = e.Item.Cells(1).Text()
        Dim nRet As Integer
        If e.CommandName.CompareTo("EditBtn") = 0 Then 
                If Not Me.Security("30010102").isValid Then
                    ltlAlert.Text = "alert('没有权限编辑采购单详细信息！');"
                    Exit Sub
                End If
            Response.Redirect(chk.urlRand("/Purchase/AddPurDetail.aspx?id=" & id & "&did=" & did & "&order_code=" & tx_order_code.Text.Trim() & "&code=" & tx_code.Text.Trim() & ""), True)
        ElseIf e.CommandName.CompareTo("InputBtn") = 0 Then

                If Not Me.Security("30010105").isValid Then
                    ltlAlert.Text = "alert('没有该项目的编辑权限！');"
                    Exit Sub
                End If
            Response.Redirect(chk.urlRand("/Purchase/AddPurNum.aspx?id=" & id & "&did=" & did & "&order_code=" & tx_order_code.Text.Trim() & "&code=" & tx_code.Text.Trim() & ""), True)
        ElseIf e.CommandName.CompareTo("TranBtn") = 0 Then 
                If Not Me.Security("30010103").isValid Then
                    ltlAlert.Text = "alert('没有权限转移材料！');"
                    Exit Sub
                End If
            Response.Redirect(chk.urlRand("/Purchase/materialassign.aspx?id=" & id & "&order_code=" & tx_order_code.Text.Trim() & "&code=" & tx_code.Text.Trim() & ""), True)
        End If
    End Sub

    Private Sub btnhis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnhis.Click
        Response.Redirect(chk.urlRand("/Purchase/DogPartInHis.aspx"), True)
    End Sub

    Private Sub btn_pur_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pur.Click 
        'Session("EXSQL") = strSQL
        'Session("EXHeader") = ""
        'ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx','','menubar=0,scrollbars=1,resizable=1,width=800,height=600,top=100,left=40');w.focus(); "
        ltlAlert.Text = "var w=window.open('/Purchase/PurTemplate.aspx?order_code=" & tx_order_code.Text.Trim() & "&code=" & tx_code.Text.Trim() & "&type=" & dr_type.SelectedValue & "','','menubar=0,scrollbars=1,resizable=1,width=800,height=600,top=100,left=40');w.focus(); "
    End Sub

    Private Sub btnhisnum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnhisnum.Click
        Response.Redirect(chk.urlRand("/Purchase/DogPartInNumHis.aspx"), True)
    End Sub

    Private Sub dgpur_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgpur.SortCommand
        Session("orderby") = e.SortExpression
        If Session("orderdir") = "ASC" Then
            Session("orderdir") = "DESC"
        Else
            Session("orderdir") = "ASC"
        End If
        BindData()
    End Sub
End Class

End Namespace
