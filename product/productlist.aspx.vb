'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-4
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Product Code 
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
    Partial Class productlist
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strSQL As String

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("19023311", "材料-供应商信息查询")
                Me.Security.Register("19070101", "部件目录维护")
                Me.Security.Register("19030604", "产品结构查询")
                Me.Security.Register("100103066", "Search Document by Item")
                Me.Security.Register("19090001", "产品部件修改历史")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then

                btnAddNew.Enabled = Me.Security("19030101").isValid
                BtnReplace.Enabled = Me.Security("19030101").isValid

                If Request("code") <> "" Or Request("code") <> Nothing Then
                    txtCode.Text = Request("code").ToString().Trim()
                End If
                If Request("cat") <> "" Or Request("cat") <> Nothing Then
                    txtCategory.Text = Request("cat").ToString().Trim()
                End If
                If Request("qad") <> "" Or Request("qad") <> Nothing Then
                    txtQad.Text = Request("qad").ToString().Trim()
                End If

                Dim i As Integer
                For i = 0 To dgProduct.Columns.Count - 1
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, "Select Top 1 userID from profile_display where userID='" & Session("uID") & "' and moduleID='" & Session("ModuleID") & "' and disableItem=N'" & dgProduct.Columns(i).HeaderText.Replace("<b>", "").Replace("</b>", "") & "'") > 0 Then
                        dgProduct.Columns(i).Visible = False
                    End If
                Next

                BindGridView()
            End If

            If Session("uRole") = 1 Then
                BtnReplace.Visible = True
            Else
                BtnReplace.Visible = False
            End If

            ltlAlert.Text = "Form1.txtCode.focus();"
        End Sub

        Function createSQL() As String
            strSQL = " Select i.id, i.itemNumber, c.company_code, i.code, Isnull(i.simpleCode,''), isnull(i.description,''), " _
                   & " logs = case when xx.[xxwkf_log01] = 1 then N'生产锁定' when xx.[xxwkf_log02] = 1 then N'送样锁定' when xx.[xxwkf_log03] = 1 then N'技术锁定' else N'' end " _
                   & " ,isnull(ic.name,''), i.status, Isnull(i.min_inv,0), Isnull(i.shipGroup,''),i.createdDate,isnull(u.username,''),i.item_qad" _
                   & " From tcpc0.dbo.Items i Left Outer Join tcpc0.dbo.ItemCategory ic On i.category = ic.id " _
                   & " Left Outer Join tcpc0.dbo.users u On u.userid = i.createdby " _
                   & " Left Outer Join tcpc0.dbo.Companies c On c.company_id=i.customerID " _
                   & " left join [QAD_Data].[dbo].[xxwkf_mstr] xx on i.item_qad = xx.[xxwkf_chr01]" _
                   & " Where i.type=2 And i.code is not null "

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

            If txtQad.Text.Trim.Length > 0 Then
                strSQL = strSQL & " and Lower(i.item_qad) like N'" & chk.sqlEncode(txtQad.Text.Trim().ToLower().Replace("*", "%")) & "'"
            End If

            If txtCategory.Text.Trim.Length > 0 Then
                strSQL = strSQL & " and Lower(ic.name) Like N'" & chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")) & "'"
            End If

            If txtDesc.Text.Trim.Length > 0 Then
                strSQL = strSQL & " and Lower(i.description) Like N'%" & chk.sqlEncode(txtDesc.Text.Trim().ToLower()) & "%'"
            End If

            strSQL = strSQL & " order by ic.name, i.itemNumber,i.itemVersion, i.itemSubVersion "

            createSQL = strSQL
        End Function

        Protected Overrides Sub BindGridView()
            Dim dst As DataSet
            Dim status As Integer
            If radStop.Checked = True Then
                status = 2
            End If
            If radTry.Checked = True Then
                status = 1
            End If
            If radNormal.Checked = True Then
                status = 0
            End If
            Dim sqlstr As String
            sqlstr = "sp_product_selectProductList"
            Dim param(5) As SqlParameter
            param(0) = New SqlParameter("@status", status)
            param(1) = New SqlParameter("@code", chk.sqlEncode(txtCode.Text.Trim().ToLower().Replace("*", "%")))
            param(2) = New SqlParameter("@item_qad", chk.sqlEncode(txtQad.Text.Trim().ToLower().Replace("*", "%")))
            param(3) = New SqlParameter("@name", chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")))
            param(4) = New SqlParameter("@description", chk.sqlEncode(txtDesc.Text.Trim().ToLower()))

            dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sqlstr, param)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("productID", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("category", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("shipGroup", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("Customer", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("pcode", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("Simple", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("createddate1", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("createdby1", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("qad", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("logs", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("ulmd", System.Type.GetType("System.String")))
            Dim num As Integer
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
                        drow.Item("description") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("category") = .Rows(i).Item(6).ToString().Trim()
                        If .Rows(i).IsNull(7) Or .Rows(i).Item(7).ToString().Trim() = "0" Then
                            drow.Item("status") = "使用"
                        ElseIf .Rows(i).Item(7).ToString().Trim() = "1" Then
                            drow.Item("status") = "试用"
                        Else
                            drow.Item("status") = "停用"
                        End If
                        drow.Item("shipGroup") = .Rows(i).Item(9).ToString().Trim()
                        drow.Item("Customer") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("Simple") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("createddate1") = .Rows(i).Item(10).ToString().Trim()
                        drow.Item("createdby1") = .Rows(i).Item(11).ToString().Trim()
                        drow.Item("qad") = .Rows(i).Item("qad").ToString().Trim()
                        drow.Item("logs") = .Rows(i).Item("logs").ToString().Trim()
                        drow.Item("ulmd") = .Rows(i).Item("ulmd").ToString().Trim()
                        dtl.Rows.Add(drow)
                        num = num + 1
                    Next
                Else
                    lblCount.Text = "数量: 0"
                End If
            End With

            If (Val(dst.Tables(0).Rows.Count.ToString()) <> num) Then
                lblCount.Text = "数量: 0"
                dgProduct.DataSource = Nothing
                dgProduct.DataBind()
                ltlAlert.Text = "alert('查询错误，请重新查询！');"
                Exit Sub
            End If
            Try
                dgProduct.DataSource = dtl
                dgProduct.DataBind()
            Catch ex As Exception
                ltlAlert.Text = "alert(" & "查询错误，请联系管理员" & ex.Message & ");"
            End Try
        End Sub

        Public Sub EditBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
            Dim str As String = e.Item.Cells(0).Text()
            Dim pg As Integer = dgProduct.CurrentPageIndex
            If (e.CommandName.CompareTo("EditBtn") = 0) Then

                If Not Me.Security("19030101").isValid Then
                    ltlAlert.Text = "alert('没有权限编辑该产品！');"
                    Exit Sub
                Else
                    Me.Redirect("/product/addproduct.aspx?id=" & str & "&code=" & txtCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" & txtCategory.Text.Trim())
                End If
            End If
        End Sub

        Private Sub dgProduct_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProduct.PageIndexChanged
            dgProduct.CurrentPageIndex = e.NewPageIndex
            BindGridView()
        End Sub

        Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
            Dim pg As Integer = dgProduct.CurrentPageIndex

            If Not Me.Security("19030101").isValid Then
                ltlAlert.Text = "alert('没有权限添加新产品！');"
                Exit Sub
            Else
                If radStop.Checked = True Then
                    If txtCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&st=true"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&pg=" & pg & "&st=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&st=true"), True)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&st=true"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?pg=" & pg & "&st=true"), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?st=true"), True)
                            End If
                        End If
                    End If
                ElseIf radTry.Checked = True Then
                    If txtCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false"), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&st=false"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&pg=" & pg & "&st=false"), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&st=false"), True)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false"), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&st=false"), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?pg=" & pg & "&st=false"), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?st=false"), True)
                            End If
                        End If
                    End If
                Else
                    If txtCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim()), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim() & "&pg=" & pg), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?code=" & txtCode.Text.Trim()), True)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim() & "&pg=" & pg), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?cat=" & txtCategory.Text.Trim()), True)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx?pg=" & pg), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/addproduct.aspx"), True)
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

                Me.Redirect("/product/productUsedByList.aspx?id=" & str)
            End If
        End Sub

        Public Sub StruBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
            Dim pg As Integer = dgProduct.CurrentPageIndex
            If (e.CommandName.CompareTo("StruBtn") = 0) Then
                Dim str As String = e.Item.Cells(0).Text()

                Me.Redirect("/product/ItemStructure.aspx?id=" & str)
            End If
        End Sub

        Public Sub SizeByBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
            Dim pg As Integer = dgProduct.CurrentPageIndex
            If (e.CommandName.CompareTo("SizeByBtn") = 0) Then

                If Not Me.Security("19030400").isValid Then
                    ltlAlert.Text = "alert('没有权限查看产品尺寸！');"
                    Exit Sub
                Else
                    Dim str As String = e.Item.Cells(0).Text()

                    Me.Redirect("/product/productSizeEdit.aspx?id=" & str)
                End If
            End If
        End Sub

        Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
            ltlAlert.Text = ""
            dgProduct.CurrentPageIndex = 0
            BindGridView()
        End Sub

        Public Sub DocByBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
            If (e.CommandName.CompareTo("DocByBtn") = 0) Then
                ltlAlert.Text = "var w=window.open('/qaddoc/qad_documentsearchbyitem.aspx?type=0&id=" & e.Item.Cells(0).Text() & "&code=" & Server.UrlEncode(e.Item.Cells(2).Text()) & "','','menubar=0,scrollbars=1,resizable=1,width=800,height=600,top=0,left=0');w.focus"
            End If
        End Sub

        Public Sub HisBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
            If (e.CommandName.CompareTo("HisBtn") = 0) Then
                ltlAlert.Text = "var w=window.open('/product/producthistory.aspx?id=" & e.Item.Cells(0).Text() & "&rm=" & DateTime.Now & "','','menubar=0,scrollbars=1,resizable=1,width=800,height=600,top=0,left=0');w.focus();"
            End If
        End Sub

        Private Sub BtnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReplace.Click
            Dim pg As Integer = dgProduct.CurrentPageIndex

            If Not Me.Security("19030101").isValid Then
                ltlAlert.Text = "alert('没有权限进行产品维护！');"
                Exit Sub
            Else
                If radStop.Checked = True Then
                    Response.Redirect("/product/productreplace.aspx?code=" & txtCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true")
                ElseIf radTry.Checked = True Then
                    Response.Redirect("/product/productreplace.aspx?code=" & txtCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=false")
                Else
                    Response.Redirect("/product/productreplace.aspx?code=" & txtCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg)
                End If
            End If
        End Sub

        Private Sub radNormal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radNormal.CheckedChanged
            dgProduct.CurrentPageIndex = 0
            BindGridView()
        End Sub

        Private Sub radStop_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radStop.CheckedChanged
            dgProduct.CurrentPageIndex = 0
            BindGridView()
        End Sub

        Private Sub radTry_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radTry.CheckedChanged
            dgProduct.CurrentPageIndex = 0
            BindGridView()
        End Sub

        Private Sub SupplyBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
            If (e.CommandName.CompareTo("SupplyBtn") = 0) Then

                If Not Me.Security("19023311").isValid Then
                    ltlAlert.Text = "alert('没有权限查看供应商！');"
                    Exit Sub
                Else
                    ltlAlert.Text = "var w=window.open('/supply/partsupplylinks.aspx?id=" & e.Item.Cells(0).Text.Trim() & "','供货','menubar=0,scrollbars=0,resizable=0,width=780,height=550,top=100,left=40');w.focus(); "
                End If
            End If
        End Sub

        Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExport.Click

            Dim EXTitle As String = "250^<b>产品型号</b>~^200^<b>产品简称</b>~^500^<b>产品描述</b>~^100^<b>锁定</b>~^100^<b>分类</b>~^70^<b>状态</b>~^100^<b>最小库存量</b>~^200^<b>系列</b>~^<b>创建日期</b>~^<b>创建人</b>~^<b>产品QAD号</b>~^"
            Dim ExSql As String = createSQL()

            Me.ExportExcel(chk.dsnx(), EXTitle, ExSql, False)
        End Sub

        Public Sub ToPartBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProduct.ItemCommand
            Dim pg As Integer = dgProduct.CurrentPageIndex
            If (e.CommandName.CompareTo("ToPartBtn") = 0) Then

                If Not Me.Security("19070101").isValid Then
                    ltlAlert.Text = "alert('没有权限对产品进行转换！');"
                    Exit Sub
                Else
                    strSQL = " Update Items Set type = 0 Where id ='" & e.Item.Cells(0).Text() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                    Me.Redirect("productlist.aspx")
                End If
            End If
        End Sub

        Protected Sub dgProduct_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgProduct.ItemDataBound

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.EditItem Then
                e.Item.Cells(8).Enabled = Me.Security("19030101").isValid '编辑
                e.Item.Cells(9).Enabled = Me.Security("19030604").isValid '结构
                e.Item.Cells(11).Enabled = Me.Security("19023311").isValid '供货
                e.Item.Cells(12).Enabled = Me.Security("19030400").isValid '尺寸
                e.Item.Cells(13).Enabled = Me.Security("100103066").isValid '文档
                e.Item.Cells(14).Enabled = Me.Security("19070101").isValid '转部件
                e.Item.Cells(15).Enabled = Me.Security("19090001").isValid '历史

            End If
        End Sub
    End Class

End Namespace
