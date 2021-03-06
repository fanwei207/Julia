Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports AppLibrary.WriteExcel


Namespace tcpc


    Partial Class ItemStructure
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim strsql As String
        Dim dst As DataSet
        Protected WithEvents Dropdownlist1 As System.Web.UI.WebControls.DropDownList
        Dim nRet As Integer

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        ' Protected WithEvents dgProd As System.Web.UI.WebControls.DataGrid
        Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("19030301", "产品结构替换")
            End If
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then
                'Security check

                BtnStruModify.Enabled = Me.Security("19030301").isValid
                BtnStruCopy.Enabled = Me.Security("19030301").isValid
                BtnStruHis.Enabled = Me.Security("19030302").isValid

                txtCode.Visible = True
                BtnStruCopy.Visible = True
                BtnStruModify.Visible = True
                BtnStruHis.Visible = True
                lblversion.Visible = False
                ddlversion.Visible = False
                Session("isc") = Nothing
                clearPartTemp()
                If Request("id") <> Nothing Then
                    GetPart(Request("id"), 1)
                    strsql = " Select i.Code,i.status,ps.createdDate From Items i Left Join Product_Stru ps ON i.id=ps.productID Where i.id='" & Request("id") & "'  "
                    dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)
                    With dst.Tables(0)
                        If .Rows.Count > 0 Then
                            lblProdCode.Text = .Rows(0).Item(0).ToString()
                            If Not .Rows(0).IsNull(2) Then
                                Label1.Text = .Rows(0).Item(2)
                            End If
                            If .Rows(0).Item(1) = 2 Then
                                BtnStruModify.Enabled = False
                                BtnStruCopy.Enabled = False
                            End If
                        End If
                    End With
                    dst.Reset()
                    BindData()
                    Dim i As Integer
                    For i = 0 To dgPart.Columns.Count - 1
                        If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, "Select userID from profile_display where userID='" & Session("uID") & "' and moduleID='" & Session("ModuleID") & "' and disableItem=N'" & dgPart.Columns(i).HeaderText.Replace("<b>", "").Replace("</b>", "") & "'") > 0 Then
                            dgPart.Columns(i).Visible = False
                        End If
                    Next
                End If
            End If
        End Sub

        '*********************************************************
        ' @@ PURPOSE : TO fill the part data.
        ' @@ INPUTS  : NA
        ' @@ RETURNS : NA
        '*********************************************************
        Function createSQL() As String
            If Session("isc") = "click" Then
                strsql = " Select psh.childID, psh.productStruID, tag = isnull(psh.tag, 0), ID = 0 " _
                      & ",mtlock = case when xx.[xxwkf_log01] = 1 then N'生产锁定' when xx.[xxwkf_log02] = 1 then N'送样锁定' when xx.[xxwkf_log03] = 1 then N'技术锁定' else N'' end " _
                      & "       , Code = Replicate('---', isnull(psh.tag, 0)) + I.Code, qad = item_qad,status = Case I.status When 0 Then N'使用' When 1 Then N'试用' Else N'停用' End " _
                      & "       , ic.name, numOfChild = Cast(psh.numOfChild As Float), psh.posCode " _
                      & "       , partReplace = Replace(Replace((Select Code From product_replace_his pr Left Join Items i1 On i1.id = pr.itemID Where prodStruID = psh.productStruID And pr.version = '" & ddlversion.SelectedValue & "' For xml Path('')), '<Code>', ''), '</Code>', ',') " _
                      & "       , qadReplace = Replace(Replace((Select item_qad From product_replace_his pr Left Join Items i1 On i1.id = pr.itemID Where prodStruID = psh.productStruID And pr.version = '" & ddlversion.SelectedValue & "' For xml Path('')), '<item_qad>', ''), '</item_qad>', ',') " _
                      & "       , psh.notes, I.description " _
                      & " From Product_Stru_His psh " _
                      & " Inner Join Items I On psh.childID=I.ID " _
                      & " Inner Join ItemCategory ic on ic.id=I.category " _
                      & " left join [QAD_Data].[dbo].[xxwkf_mstr] xx on I.item_qad = xx.[xxwkf_chr01]" _
                      & " Where psh.productID=" & Request("id") & " And psh.version='" & ddlversion.SelectedValue & "' "
                If CheckBox1.Checked = False Then
                    strsql = strsql & " and psh.tag = 0 "
                End If
                strsql = strsql & " order by productStruID "
            Else
                strsql = " Select cpt.childID, cpt.productStruID, tag = isnull(cpt.tag,0), cpt.ID " _
                       & ",mtlock = case when xx.[xxwkf_log01] = 1 then N'生产锁定' when xx.[xxwkf_log02] = 1 then N'送样锁定' when xx.[xxwkf_log03] = 1 then N'技术锁定' else N'' end " _
                       & "      , Code = Replicate('---', isnull(cpt.tag,0)) + I.Code, qad = item_qad, status = Case I.status When 0 Then N'使用' When 1 Then N'试用' Else N'停用' End " _
                       & "      , ic.name, numOfChild = Cast(cpt.numOfChild As Float), cpt.posCode " _
                       & "      , partReplace = Replace(Replace((Select Code From product_replace pr Left Join Items i1 On i1.id = pr.itemID Where prodStruID = cpt.productStruID For xml Path('')), '<Code>', ''), '</Code>', ',') " _
                       & "      , qadReplace = Replace(Replace((Select item_qad From product_replace pr Left Join Items i1 On i1.id = pr.itemID Where prodStruID = cpt.productStruID For xml Path('')), '<item_qad>', ''), '</item_qad>', ',') " _
                       & "      , cpt.notes, I.Description " _
                       & " From Items I " _
                       & " Inner Join ItemCategory ic on ic.id = I.category " _
                       & " Inner Join CalPart_temp_stru cpt on cpt.childID = I.ID And cpt.CreatedBy = '" & Session("uID") & "' And plantID = '" & Session("plantCode") & "'"
                If CheckBox1.Checked = False Then
                    strsql = strsql & " and cpt.tag = 0 "
                End If
                strsql = strsql & " left join [QAD_Data].[dbo].[xxwkf_mstr] xx on I.item_qad = xx.[xxwkf_chr01] Order By cpt.ID "
            End If
            createSQL = strsql
        End Function
        Function createSQLoutput() As DataTable
            Dim param(1) As SqlParameter
            strsql = "sp_product_selectStru"

            param(0) = New SqlParameter("@productID", CInt(Request("id")))

            createSQLoutput = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.StoredProcedure, strsql, param).Tables(0)

        End Function
        Sub BindData()
            Dim qty As Decimal
            Dim reader As SqlDataReader
            dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, createSQL())
            Dim dtlPart As New DataTable
            dtlPart.Columns.Add(New DataColumn("ProdStruID", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("partdesc", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("PartQty", System.Type.GetType("System.Decimal")))
            dtlPart.Columns.Add(New DataColumn("partMemo", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("partPos", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("partReplace", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("pID", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("cID", System.Type.GetType("System.Decimal")))
            dtlPart.Columns.Add(New DataColumn("mtlock", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtlPart.Columns.Add(New DataColumn("partqad", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("qadReplace", System.Type.GetType("System.String")))

            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    Dim j As Integer
                    Dim drowPart As DataRow
                    Dim strPos As String = ""
                    For i = 0 To .Rows.Count - 1

                        drowPart = dtlPart.NewRow()
                        drowPart.Item("gsort") = i + 1
                        drowPart.Item("ProdStruID") = .Rows(i).Item("productStruID").ToString().Trim()
                        'status
                        drowPart.Item("status") = .Rows(i).Item("status").ToString()
                        drowPart.Item("mtlock") = .Rows(i).Item("mtlock").ToString()

                        drowPart.Item("partcode") = .Rows(i).Item("Code").ToString().Trim()
                        drowPart.Item("partdesc") = .Rows(i).Item("Description").ToString().Trim()
                        qty = CDec(.Rows(i).Item("numOfChild"))
                        If qty = CInt(qty) Then
                            drowPart.Item("PartQty") = CInt(qty)
                        Else
                            drowPart.Item("PartQty") = qty
                        End If
                        drowPart.Item("partMemo") = .Rows(i).Item("notes").ToString().Trim()
                        drowPart.Item("partPos") = .Rows(i).Item("posCode").ToString().Trim()
                        drowPart.Item("pID") = .Rows(i).Item("childID").ToString().Trim()
                        drowPart.Item("cID") = .Rows(i).Item("ID")
                        drowPart.Item("partReplace") = .Rows(i).Item("partReplace")
                        drowPart.Item("type") = .Rows(i).Item("name").ToString().Trim()

                        drowPart.Item("partqad") = .Rows(i).Item("qad").ToString().Trim()
                        drowPart.Item("qadReplace") = .Rows(i).Item("qadReplace").ToString().Trim()

                        dtlPart.Rows.Add(drowPart)
                        strPos = ""
                    Next
                Else
                    If Session("isc") = "click" Then
                        Session("isc") = Nothing
                        If Request("semi") = "true" Then
                            ltlAlert.Text = "alert('没有该半成品的结构历史！'); window.history.back();"
                        Else
                            ltlAlert.Text = "alert('没有该产品的结构历史！'); window.history.back();"
                        End If
                    End If
                End If
            End With
            Dim dvPart As DataView
            dvPart = New DataView(dtlPart)
            Try
                dgPart.DataSource = dvPart
                dgPart.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgPart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPart.PageIndexChanged
            dgPart.CurrentPageIndex = e.NewPageIndex()
            BindData()
        End Sub

        Sub clearPartTemp()
            strsql = " Delete From CalPart_temp_stru Where createdBy='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
        End Sub

        Sub GetPart(ByVal proID As Long, ByVal Qty As Decimal, Optional ByVal tag As Integer = 0)
            Dim reader As SqlDataReader
            strsql = "Select ProductStruID,childID,isnull(numOfChild,0),childCategory,notes, posCode From Product_Stru Where productID=" & proID & " And numOfChild<>0 "

            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
            While reader.Read()
                If reader("childCategory") = "PART" Then
                    strsql = "Insert Into CalPart_temp_stru(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & reader(0) & "','" & Request("id") & "','" & reader(1) & "'," & reader(2) & "*" & Qty & ",'" & reader(3) & "',N'" & reader(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & reader(5).ToString().Replace("'", "''") & "') "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
                Else
                    strsql = "Insert Into CalPart_temp_stru(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & reader(0) & "','" & Request("id") & "','" & reader(1) & "'," & reader(2) & "*" & Qty & ",'" & reader(3) & "',N'" & reader(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & reader(5).ToString().Replace("'", "''") & "') "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
                    GetPart(reader(1), reader(2) * Qty, tag + 1)
                End If
            End While
            reader.Close()
        End Sub

        Private Sub BtnStruHis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStruHis.Click

            If Not Me.Security("19030302").isValid Then

                ltlAlert.Text = "alert('没有权限查看产品的结构历史记录！');"
                Exit Sub
            Else
                lblversion.Visible = True
                ddlversion.Visible = True
                BtnStruCopy.Visible = False
                txtCode.Visible = False
                BtnStruModify.Visible = False
                BtnStruHis.Visible = False
                Session("isc") = "click"
                ddlversionLoad()
                BindData()
            End If
        End Sub

        Sub ddlversionLoad()
            ddlversion.Items.Clear()
            Dim item As ListItem
            Dim ds As DataSet
            strsql = "select distinct isnull(version,0) as version,isnull(createdDate,'1900-01-01')as createdDate from product_stru_his where productID='" & Request("id") & "' order by version desc "
            ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item("version") & "--" & Format(.Rows(i).Item("createdDate"), "yyyy-MM-dd"))
                        item.Value = .Rows(i).Item("version")
                        ddlversion.Items.Add(item)
                    Next

                End If
            End With
            ds.Reset()
            ddlversion.SelectedIndex = -1
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            If (Session("isc") = "click") Then
                Session("isc") = Nothing
                lblversion.Visible = False
                ddlversion.Visible = False
                BtnStruHis.Visible = True
                BtnStruModify.Visible = True
                BtnStruCopy.Visible = True
                txtCode.Visible = True
                BindData()
            Else
                If Request("semi") <> Nothing Then
                    If Request("st") <> Nothing Then
                        If Request("code") <> Nothing Then
                            If Request("cat") <> Nothing Then
                                If Request("pg") <> Nothing Then
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&semi=true&st=" & Request("st") & ""), True)
                                Else
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&semi=true&st=" & Request("st") & ""), True)
                                End If
                            Else
                                If Request("pg") <> Nothing Then
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?code=" & Request("code") & "&pg=" & Request("pg") & "&semi=true&st=" & Request("st") & ""), True)
                                Else
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?code=" & Request("code") & "&semi=true&st=" & Request("st") & ""), True)
                                End If
                            End If
                        Else
                            If Request("cat") <> Nothing Then
                                If Request("pg") <> Nothing Then
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?cat=" & Request("cat") & "&pg=" & Request("pg") & "&semi=true&st=" & Request("st") & ""), True)
                                Else
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?cat=" & Request("cat") & "&semi=true&st=" & Request("st") & ""), True)
                                End If
                            Else
                                If Request("pg") <> Nothing Then
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?pg=" & Request("pg") & "&semi=true&st=" & Request("st") & ""), True)
                                Else
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?semi=true&st=" & Request("st") & ""), True)
                                End If
                            End If
                        End If
                    Else
                        If Request("code") <> Nothing Then
                            If Request("cat") <> Nothing Then
                                If Request("pg") <> Nothing Then
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?code=" & Request("code") & "&semi=true&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                                Else
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?code=" & Request("code") & "&semi=true&cat=" & Request("cat")), True)
                                End If
                            Else
                                If Request("pg") <> Nothing Then
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?code=" & Request("code") & "&semi=true&pg=" & Request("cat")), True)
                                Else
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?semi=true&code=" & Request("code")), True)
                                End If
                            End If
                        Else
                            If Request("cat") <> Nothing Then
                                If Request("pg") <> Nothing Then
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?cat=" & Request("cat") & "&semi=true&pg=" & Request("pg")), True)
                                Else
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?semi=true&cat=" & Request("cat")), True)
                                End If
                            Else
                                If Request("pg") <> Nothing Then
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?semi=true&pg=" & Request("pg")), True)
                                Else
                                    Response.Redirect(chk.urlRand("/product/semislist.aspx?semi=true"), True)
                                End If
                            End If
                        End If
                    End If
                Else
                    Me.Redirect("/product/productlist.aspx")
                End If
            End If
        End Sub

        Private Sub ddlversion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlversion.SelectedIndexChanged
            BindData()
        End Sub

        Private Sub BtnStruModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnStruModify.Click

            If Not Me.Security("19030301").isValid Then

                ltlAlert.Text = "alert('没有权限修改产品的结构！');"
                Exit Sub
            Else
                If Request("id") <> Nothing Then
                    If Request("semi") <> Nothing Then
                        If Request("st") <> Nothing Then
                            If Request("code") <> Nothing Then
                                If Request("cat") <> Nothing Then
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&semi=true&st=" & Request("st") & ""), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&semi=true&st=" & Request("st") & ""), True)
                                    End If
                                Else
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&pg=" & Request("pg") & "&semi=true&st=" & Request("st") & ""), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&semi=true&st=" & Request("st") & ""), True)
                                    End If
                                End If
                            Else
                                If Request("cat") <> Nothing Then
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&semi=true&st=" & Request("st") & ""), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&semi=true&st=" & Request("st") & ""), True)
                                    End If
                                Else
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&pg=" & Request("pg") & "&semi=true&st=" & Request("st") & ""), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&st=" & Request("st") & ""), True)
                                    End If
                                End If
                            End If
                        Else
                            If Request("code") <> Nothing Then
                                If Request("cat") <> Nothing Then
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&semi=true&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&semi=true&cat=" & Request("cat")), True)
                                    End If
                                Else
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&semi=true&pg=" & Request("cat")), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&code=" & Request("code")), True)
                                    End If
                                End If
                            Else
                                If Request("cat") <> Nothing Then
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&semi=true&pg=" & Request("pg")), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&cat=" & Request("cat")), True)
                                    End If
                                Else
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&pg=" & Request("pg")), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true"), True)
                                    End If
                                End If
                            End If
                        End If
                    Else
                        If Request("st") <> Nothing Then
                            If Request("code") <> Nothing Then
                                If Request("cat") <> Nothing Then
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&st=" & Request("st") & ""), True)
                                    End If
                                Else
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&st=" & Request("st") & ""), True)
                                    End If
                                End If
                            Else
                                If Request("cat") <> Nothing Then
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&st=" & Request("st") & ""), True)
                                    End If
                                Else
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&st=" & Request("st") & ""), True)
                                    End If
                                End If
                            End If
                        Else
                            If Request("code") <> Nothing Then
                                If Request("cat") <> Nothing Then
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat")), True)
                                    End If
                                Else
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&pg=" & Request("cat")), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code")), True)
                                    End If
                                End If
                            Else
                                If Request("cat") <> Nothing Then
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat")), True)
                                    End If
                                Else
                                    If Request("pg") <> Nothing Then
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&pg=" & Request("pg")), True)
                                    Else
                                        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id")), True)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub BtnStruCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStruCopy.Click
            Dim prodID, query, psID As String
            Dim reader As SqlDataReader

            If Not Me.Security("19030301").isValid Then

                ltlAlert.Text = "alert('没有权限拷贝产品的结构！');"

                Exit Sub
            Else
                If txtCode.Text.Trim().Length <= 0 Then
                    ltlAlert.Text = "alert('产品型号不能为空！');"
                    Exit Sub
                ElseIf txtCode.Text.Trim().Length > 50 Then
                    ltlAlert.Text = "alert('产品型号长度不能大于50！');"
                    Exit Sub
                Else
                    strsql = " Select ID From Items Where Code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "' And Type<>0 "
                    prodID = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)
                    If prodID <> Nothing And prodID > "0" Then
                        strsql = "select distinct isnull(productID,0) from Product_Stru Where ProductID='" & prodID & "'"
                        Dim pid As String = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)
                        If pid > "0" Then
                            ltlAlert.Text = "alert('该产品结构已存在！');"
                            Exit Sub
                        End If

                        strsql = " Select childID, isnull(numOfChild,0), childCategory, isnull(notes,''), isnull(posCode,''), isnull(numOfChild_temp,0), " _
                               & " isnull(notes_temp,''), isnull(posCode_temp,''), productStruID From Product_Stru Where productID='" & Request("id") & "'"
                        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                        While reader.Read()
                            query = " Insert Into Product_Stru(productID, childID, numOfChild, childCategory, notes, numOfChild_temp, notes_temp, posCode, posCode_temp, plantCode) " _
                                  & " Values('" & prodID & "','" & reader(0) & "','" & reader(1) & "','" & reader(2) & "',N'" & chk.sqlEncode(reader(3)) _
                                  & "','" & reader(5) & "',N'" & chk.sqlEncode(reader(6)) & "',N'" & reader(4) & "',N'" & reader(7) & "','" & Session("plantcode") & "')"
                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, query)

                            query = " Select Count(*) From product_replace Where prodStruID='" & reader(8) & "'"
                            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, query) > 0 Then
                                query = " Select Top 1 productStruID From Product_Stru Where productID=" & prodID & " Order By productStruID Desc "
                                psID = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, query)

                                query = " Insert Into product_replace(prodStruID, itemID, itemID_temp) Select " & psID.Trim() & ",itemID, itemID_temp From product_replace Where prodStruID='" & reader(8) & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, query)
                            End If
                        End While
                        reader.Close()
                        'check isUnique
                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, "Item_CheckUnique")
                        ltlAlert.Text = "alert('拷贝成功!');"
                    Else
                        ltlAlert.Text = "alert('产品型号不存在，无法拷贝结构！');Form1.document.getElementById('txtcode').value='';Form1.txtcode.focus();"
                        Exit Sub
                    End If
                End If
                txtCode.Text = ""
            End If
        End Sub

        Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            dgPart.CurrentPageIndex = 0
            BindData()
        End Sub

        Protected Sub ButExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButExcel.Click
            Dim EXTitle As String = "350^<b>部件号</b>~^150^<b>QAD</b>~^<b>状态</b>~^<b>分类</b>~^150^<b>数量</b>~^150^<b>位号</b>~^200^<b>替代品</b>~^200^<b>替代品QAD</b>~^250^<b>备注</b>~^1500^<b>部件描述</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsn0, lblProdCode.Text, EXTitle, ExSql, False)

        End Sub

        Protected Sub btnoutput_Click(sender As Object, e As EventArgs) Handles btnoutput.Click
            Dim EXTitle As String = "50^<b>是否新产品(Y/N)</b>~^<b>父级类型</b>~^<b>父级部件号</b>~^150^<b>产品描述</b>~^150^<b>子级部件号</b>~^200^<b>位号(可空)</b>~^100^<b>规格(可空)</b>~^100^<b>数量</b>~^150^<b>备注(可空)</b>~^150^<b>替代品(可空)</b>~^"
            Dim dt As DataTable = createSQLoutput()
            Me.ExportExcel(EXTitle, dt, False)
        End Sub
    End Class

End Namespace
