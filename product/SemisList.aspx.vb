'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-01-18
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Semis Code 
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class SemisList
    Inherits System.Web.UI.Page
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer

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
            nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19030600)
            If nRet <= 0 Then
                Response.Redirect("/public/Message.aspx?type=" & nRet.ToString(), True)
            End If
            nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19030601, True)
            If nRet <= 0 Then
                btnAddNew.Enabled = False
                BtnReplace.Enabled = False
            Else
                btnAddNew.Enabled = True
                BtnReplace.Enabled = True
            End If
            If Request("code") <> "" Or Request("code") <> Nothing Then
                txtCode.Text = Request("code").ToString().Trim()
            End If
            If Request("cat") <> "" Or Request("cat") <> Nothing Then
                txtCategory.Text = Request("cat").ToString().Trim()
            End If
            radNormal.Checked = True
            If Request("st") = "true" Then
                radStop.Checked = True
                radTry.Checked = False
                radNormal.Checked = False
            ElseIf Request("st") = "false" Then
                radTry.Checked = True
                radStop.Checked = False
                radNormal.Checked = False
            End If
            If Request("pg") <> Nothing Then
                dgProduct.CurrentPageIndex = CInt(Request("pg"))
            End If
            BindData()
        End If
        ltlAlert.Text = "Form1.txtCode.focus();"
    End Sub

    Sub BindData()
        Dim strSQL As String
        Dim dst As DataSet

        Session("EXTitle") = "250^<b>产品型号</b>~^500^<b>产品描述</b>~^100^<b>分类</b>~^70^<b>状态</b>~^100^<b>最小库存量</b>~^"

        strSQL = " SELECT i.id, i.itemNumber, c.company_code, i.code, isnull(i.description,''), isnull(ic.name,''), i.status, i.min_inv" _
               & " From tcpc0.dbo.Items i Left Outer Join tcpc0.dbo.ItemCategory ic ON i.category = ic.id " _
               & " Left Outer Join tcpc0.dbo.Companies c On c.company_id=i.customerID " _
               & " Where i.type=1 And i.code is not null "

        If radStop.Checked = True Then
            strSQL &= " And i.status=2 "
        End If
        If radTry.Checked = True Then
            strSQL &= " And i.status=1 "
        End If
        If radNormal.Checked = True Then
            strSQL &= " And i.status=0 "
        End If

        If txtCode.Text.Trim.Length > 0 Then
            strSQL = strSQL & " and Lower(i.code) like N'" & chk.sqlEncode(txtCode.Text.Trim().ToLower().Replace("*", "%")) & "'"
        End If

        If txtCategory.Text.Trim.Length > 0 Then
            strSQL = strSQL & " and Lower(ic.name) Like N'" & chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")) & "'"
        End If

        strSQL = strSQL & " order by ic.name, i.itemNumber,i.itemVersion"
        Session("EXSQL") = strSQL
        Session("EXHeader") = ""

        dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("productID", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("category", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("Customer", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("pcode", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                lblCount.Text = "数量: " & .Rows.Count.ToString()
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("productID") = .Rows(i).Item(0).ToString().Trim()
                    drow.Item("gsort") = i + 1
                    drow.Item("code") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("pcode") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("description") = .Rows(i).Item(4).ToString().Trim()
                    drow.Item("category") = .Rows(i).Item(5).ToString().Trim()
                    If .Rows(i).IsNull(6) Or .Rows(i).Item(6).ToString().Trim() = "0" Then
                        drow.Item("status") = "使用"
                    ElseIf .Rows(i).Item(4).ToString().Trim() = "1" Then
                        drow.Item("status") = "试用"
                    Else
                        drow.Item("status") = "停用"
                    End If
                    drow.Item("Customer") = .Rows(i).Item(2).ToString().Trim()
                    dtl.Rows.Add(drow)
                Next
            Else
                lblCount.Text = "数量: 0"
                'ltlAlert.Text = "alert('没有找到查询的产品！');"
                Session("EXSQL") = Nothing
                Session("EXTitle") = Nothing
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)
        If (Session("orderby").Length <= 0) Then
            Session("orderby") = "gsort"
        End If
        Try
            dvw.Sort = Session("orderby") & " " & Session("orderdir")
            dgProduct.DataSource = dvw
            dgProduct.DataBind()
        Catch
        End Try
    End Sub

    Public Sub EditBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
        Dim str As String = e.Item.Cells(0).Text()
        Dim pg As Integer = dgProduct.CurrentPageIndex
        If (e.CommandName.CompareTo("EditBtn") = 0) Then
            nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19030601, True)
            If nRet <= 0 Then
                ltlAlert.Text = "alert('没有权限编辑该半成品！');"
                Exit Sub
            Else
                If radStop.Checked = True Then
                    If txtCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&st=true&semi=true"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() & "&st=true&semi=true"), True)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&st=true&semi=true"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&pg=" & pg & "&st=true&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&st=true&semi=true"), True)
                            End If
                        End If
                    End If
                ElseIf radTry.Checked = True Then
                    If txtCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&st=false&semi=true"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() & "&st=false&semi=true"), True)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&st=false&semi=true"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&pg=" & pg & "&st=false&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&st=false&semi=true"), True)
                            End If
                        End If
                    End If
                Else
                    If txtCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&semi=true"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() & "&semi=true"), True)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&semi=true"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&pg=" & pg & "&semi=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("addproduct.aspx?id=" & str & "&semi=true"), True)
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dgProduct_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgProduct.SortCommand
        Session("orderby") = e.SortExpression.ToString()
        If Session("orderdir") = "ASC" Then
            Session("orderdir") = "DESC"
        Else
            Session("orderdir") = "ASC"
        End If
        BindData()
    End Sub

    Private Sub dgProduct_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProduct.PageIndexChanged
        dgProduct.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Dim pg As Integer = dgProduct.CurrentPageIndex
        nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19030601, True)
        If nRet <= 0 Then
            ltlAlert.Text = "alert('没有权限添加新的半成品！');"
            Exit Sub
        Else
            If radStop.Checked = True Then
                If txtCode.Text.Trim.Length > 0 Then
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&st=true&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&st=true&semi=true"), True)
                        End If
                    End If
                Else
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&st=true&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?st=true&semi=true"), True)
                        End If
                    End If
                End If
            ElseIf radTry.Checked = True Then
                If txtCode.Text.Trim.Length > 0 Then
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&st=false&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&st=false&semi=true"), True)
                        End If
                    End If
                Else
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&st=false&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?st=false&semi=true"), True)
                        End If
                    End If
                End If
            Else
                If txtCode.Text.Trim.Length > 0 Then
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&semi=true"), True)
                        End If
                    End If
                Else
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/addproduct.aspx?semi=true"), True)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub UsedByBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
        Dim pg As Integer = dgProduct.CurrentPageIndex
        If (e.CommandName.CompareTo("UsedByBtn") = 0) Then
            Dim str As String = e.Item.Cells(0).Text()
            If radStop.Checked = True Then
                If txtCode.Text.Trim.Length > 0 Then
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&st=true&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&st=true&semi=true"), True)
                        End If
                    End If
                Else
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&st=true&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&st=true&semi=true"), True)
                        End If
                    End If
                End If
            ElseIf radTry.Checked = True Then
                If txtCode.Text.Trim.Length > 0 Then
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&st=false&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&st=false&semi=true"), True)
                        End If
                    End If
                Else
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&st=false&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&st=false&semi=true"), True)
                        End If
                    End If
                End If
            Else
                If txtCode.Text.Trim.Length > 0 Then
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&semi=true"), True)
                        End If
                    End If
                Else
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/productUsedByList.aspx?id=" & str & "&semi=true"), True)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub DocByBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
        If (e.CommandName.CompareTo("DocByBtn") = 0) Then
            ltlAlert.Text = "var w=window.open('/document/associatedDoc.aspx?type=1&id=" & e.Item.Cells(0).Text() & "&code=" & e.Item.Cells(2).Text() & "','','menubar=0,scrollbars=1,resizable=1,width=800,height=600,top=0,left=0');w.focus();"
        End If
    End Sub
    Public Sub StruBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
        Dim pg As Integer = dgProduct.CurrentPageIndex
        If (e.CommandName.CompareTo("StruBtn") = 0) Then
            Dim str As String = e.Item.Cells(0).Text()
            If radStop.Checked = True Then
                If txtCode.Text.Trim.Length > 0 Then
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&st=true&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&st=true&semi=true"), True)
                        End If
                    End If
                Else
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&st=true&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&pg=" & pg & "&st=true&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&st=true&semi=true"), True)
                        End If
                    End If
                End If
            ElseIf radTry.Checked = True Then
                If txtCode.Text.Trim.Length > 0 Then
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&st=false&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" _
                                            & txtCode.Text.Trim() & "&st=false&semi=true"), True)
                        End If
                    End If
                Else
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&cat=" _
                                            & txtCategory.Text.Trim() & "&st=false&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&pg=" & pg & "&st=false&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&st=false&semi=true"), True)
                        End If
                    End If
                End If
            Else
                If txtCode.Text.Trim.Length > 0 Then
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" & txtCode.Text.Trim() _
                                            & "&cat=" & txtCategory.Text.Trim() & "&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" & txtCode.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&code=" & txtCode.Text.Trim() & "&semi=true"), True)
                        End If
                    End If
                Else
                    If txtCategory.Text.Trim.Trim.Length > 0 Then
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&semi=true"), True)
                        End If
                    Else
                        If pg <> 0 Then
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&pg=" & pg & "&semi=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & str & "&semi=true"), True)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub SupplyBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
        If (e.CommandName.CompareTo("SupplyBtn") = 0) Then
            nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19031000, True)
            If nRet <= 0 Then
                ltlAlert.Text = "alert('没有权限查看半成品的供应商！');"
                Exit Sub
            Else
                ltlAlert.Text = "var w=window.open('/supply/partsupplylinks.aspx?id=" & e.Item.Cells(0).Text.Trim() & "&semi=true','供货','menubar=0,scrollbars=0,resizable=0,width=780,height=550,top=100,left=40');w.focus(); "
            End If
        End If
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        ltlAlert.Text = ""
        dgProduct.CurrentPageIndex = 0
        BindData()
    End Sub

    Private Sub BtnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReplace.Click
        Dim pg As Integer = dgProduct.CurrentPageIndex
        nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19030601, True)
        If nRet <= 0 Then
            ltlAlert.Text = "alert('没有权限进行产品维护！');"
            Exit Sub
        Else
            If radStop.Checked = True Then
                ltlAlert.Text = "var w=window.open('/product/productreplace.aspx?semi=true&code=" & txtCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() _
                              & "&pg=" & pg & "&st=true','','menubar=0,scrollbars=0,resizable=0,width=500,height=350,top=300,left=300');w.focus(); "
            ElseIf radTry.Checked = True Then
                ltlAlert.Text = "var w=window.open('/product/productreplace.aspx?semi=true&code=" & txtCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() _
                              & "&pg=" & pg & "&st=false','','menubar=0,scrollbars=0,resizable=0,width=500,height=350,top=300,left=300');w.focus(); "
            Else
                ltlAlert.Text = "var w=window.open('/product/productreplace.aspx?semi=true&code=" & txtCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() _
                              & "&pg=" & pg & "','','menubar=0,scrollbars=0,resizable=0,width=500,height=350,top=300,left=300');w.focus(); "
            End If
        End If
    End Sub

    Private Sub radNormal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radNormal.CheckedChanged
        dgProduct.CurrentPageIndex = 0
        BindData()
    End Sub

    Private Sub radStop_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radStop.CheckedChanged
        dgProduct.CurrentPageIndex = 0
        BindData()
    End Sub

    Private Sub radTry_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radTry.CheckedChanged
        dgProduct.CurrentPageIndex = 0
        BindData()
    End Sub
End Class

End Namespace
