Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class ItemStruEdit
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim strsql As String
        Dim dst As DataSet
        Dim nRet As Integer
        Dim reader As SqlDataReader

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents BtnStruHis As System.Web.UI.WebControls.Button
        Protected WithEvents lblPrev As System.Web.UI.WebControls.Label
        Protected WithEvents lblNext As System.Web.UI.WebControls.Label


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
                'Security check

                BtnPartAddNew.Visible = True
                BtnProdAddNew.Visible = True
                BtnStruSave.Visible = True
                Dim chg As Boolean = False          'check struture change
                Dim cnt As Integer = 0
                strsql = " Select Count(*) From product_stru Where (numofchild_temp<>isnull(numofchild,0) Or notes_temp<>isnull(notes,'') Or isnull(posCode,'')<>posCode_temp) And productID=" & Request("id") & " And  plantCode='" & Session("plantcode") & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)
                If cnt > 0 Then
                    chg = True
                Else
                    strsql = " Select Count(*) From product_replace Where (itemID_temp<>itemID And itemID<>0 And prodStruID in (select productStruID From product_stru Where productID='" & Request("id") & "')) Or itemID is null "
                    cnt = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)
                    If cnt > 0 Then
                        chg = True
                    Else
                        chg = False
                    End If
                End If

                If chg = False Then
                    BtnStruSave.Enabled = False
                Else
                    BtnStruSave.Enabled = True
                End If

                strsql = " Select i.Code From Items i Left Join Product_Stru ps ON i.id=ps.productID Where i.id='" & Request("id") & "'"
                lblProdCode.Text = "<font size='4pt'>" & SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) & "</font>"
                BindPartData()
                BindProdData()
                'BtnStruUpdate.Attributes.Add("onclick", "return confirm('确定要升级吗?');")
                Button1.Attributes.Add("onclick", "return confirm('确定要删除整个产品结构吗?');")

                If Session("uRole") = 1 Then
                    BtnStruUpdate.Visible = True
                Else
                    BtnStruUpdate.Visible = False
                End If
            End If
        End Sub

        '*********************************************************
        ' @@ PURPOSE : TO fill the part data.
        ' @@ INPUTS  : NA
        ' @@ RETURNS : NA
        '*********************************************************
        Sub BindPartData()
            Dim qty As Decimal
            strsql = " Select I.Code, I.Description, ps.numOfChild_temp, ps.notes_temp, ps.productStruID, ps.childID, ps.posCode_temp " _
                   & " From Product_Stru ps " _
                   & " Inner Join Items I On ps.childID=I.ID  And I.status<>2 " _
                   & " Where ps.childCategory='PART' And ps.numOfChild_temp<>-1 And ps.productID=" & Request("id")
            'Me.Response.Write(strsql)
            dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)

            Dim dtlPart As New DataTable
            dtlPart.Columns.Add(New DataColumn("ProdStruID", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("partdesc", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("PartQty", System.Type.GetType("System.Decimal")))
            dtlPart.Columns.Add(New DataColumn("partMemo", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("partPos", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("partReplace", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("pID", System.Type.GetType("System.String")))

            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    Dim drowPart As DataRow
                    Dim strPos As String = ""
                    For i = 0 To .Rows.Count - 1
                        drowPart = dtlPart.NewRow()
                        drowPart.Item("ProdStruID") = .Rows(i).Item(4).ToString().Trim()
                        drowPart.Item("partcode") = .Rows(i).Item(0).ToString().Trim()
                        drowPart.Item("partdesc") = .Rows(i).Item(1).ToString().Trim()
                        qty = CDec(.Rows(i).Item(2))
                        If qty = CInt(qty) Then
                            drowPart.Item("partQty") = CInt(qty)
                        Else
                            drowPart.Item("partQty") = qty
                        End If
                        drowPart.Item("partMemo") = .Rows(i).Item(3).ToString().Trim()
                        drowPart.Item("partPos") = .Rows(i).Item(6).ToString().Trim()
                        drowPart.Item("pID") = .Rows(i).Item(5).ToString().Trim()
                        strsql = " Select i.code From product_replace pr Inner Join Items i On pr.itemID_temp=i.id Where pr.prodStruID='" & .Rows(i).Item(4).ToString().Trim() & "'"
                        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                        While reader.Read()
                            strPos &= reader(0) & ","
                        End While
                        reader.Close()
                        If strPos.Trim() <> "" Then
                            drowPart.Item("partReplace") = strPos.Substring(0, Len(strPos.Trim()) - 1).Trim()
                        Else
                            drowPart.Item("partReplace") = "无"
                        End If
                        dtlPart.Rows.Add(drowPart)
                        strPos = ""
                    Next
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

        '*********************************************************
        ' @@ PURPOSE : TO fill the Semis data.
        ' @@ INPUTS  : NA
        ' @@ RETURNS : NA
        '*********************************************************
        Sub BindProdData()
            Dim qty As Decimal
            strsql = " Select I.Code, I.Description, ps.numOfChild_temp, ps.notes_temp, ps.productStruID, ps.childID, ps.posCode_temp "
            strsql &= " From Product_Stru ps " _
                   & " Inner Join Items I On ps.childID=I.ID And I.type<>0 And I.status<>2 " _
                   & " Where ps.childCategory='PROD' And ps.numOfChild_temp<>-1 And ps.productID=" & Request("id")
            dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)

            Dim dtlProd As New DataTable
            dtlProd.Columns.Add(New DataColumn("ProdStruID", System.Type.GetType("System.String")))
            dtlProd.Columns.Add(New DataColumn("prodcode", System.Type.GetType("System.String")))
            dtlProd.Columns.Add(New DataColumn("proddesc", System.Type.GetType("System.String")))
            dtlProd.Columns.Add(New DataColumn("prodQty", System.Type.GetType("System.Decimal")))
            dtlProd.Columns.Add(New DataColumn("prodMemo", System.Type.GetType("System.String")))
            dtlProd.Columns.Add(New DataColumn("prodPos", System.Type.GetType("System.String")))
            dtlProd.Columns.Add(New DataColumn("prodReplace", System.Type.GetType("System.String")))
            dtlProd.Columns.Add(New DataColumn("pID", System.Type.GetType("System.String")))

            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    Dim drowProd As DataRow
                    Dim strPos As String = ""
                    For i = 0 To .Rows.Count - 1
                        drowProd = dtlProd.NewRow()
                        drowProd.Item("ProdStruID") = .Rows(i).Item(4).ToString().Trim()
                        drowProd.Item("prodcode") = .Rows(i).Item(0).ToString().Trim()
                        drowProd.Item("proddesc") = .Rows(i).Item(1).ToString().Trim()
                        qty = CDec(.Rows(i).Item(2))
                        If qty = CInt(qty) Then
                            drowProd.Item("prodQty") = CInt(qty)
                        Else
                            drowProd.Item("prodQty") = qty
                        End If
                        drowProd.Item("prodMemo") = .Rows(i).Item(3).ToString().Trim()
                        drowProd.Item("prodPos") = .Rows(i).Item(6).ToString().Trim()
                        drowProd.Item("pID") = .Rows(i).Item(5).ToString().Trim()
                        strsql = " Select i.code From product_replace pr Inner Join Items i On pr.itemID_temp=i.id Where pr.prodStruID='" & .Rows(i).Item(4).ToString().Trim() & "'"
                        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                        While reader.Read()
                            strPos &= reader(0) & ","
                        End While
                        reader.Close()
                        If strPos.Trim() <> "" Then
                            drowProd.Item("prodReplace") = strPos.Substring(0, Len(strPos.Trim()) - 1).Trim()
                        Else
                            drowProd.Item("prodReplace") = "无"
                        End If
                        dtlProd.Rows.Add(drowProd)
                        strPos = ""
                    Next
                End If
            End With
            Dim dvProd As DataView
            dvProd = New DataView(dtlProd)

            Try
                dgProd.DataSource = dvProd
                dgProd.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgPart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPart.PageIndexChanged
            dgPart.CurrentPageIndex = e.NewPageIndex()
            BindPartData()
        End Sub

        Private Sub dgProd_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProd.PageIndexChanged
            dgProd.CurrentPageIndex = e.NewPageIndex()
            BindProdData()
        End Sub

        '*********************************************************
        ' @@ PURPOSE : TO delete part data.
        ' @@ INPUTS  : NA
        ' @@ RETURNS : NA
        '*********************************************************
        Public Sub DeletePartBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPart.ItemCommand
            If (e.CommandName.CompareTo("DeletePartBtn") = 0) Then
                strsql = " update Product_Stru set NumOfChild_temp=-1 Where productStruID='" & e.Item.Cells(0).Text.Trim() & "' And plantCode='" & Session("plantcode") & "' "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") _
                                & "&semi=" & Request("semi") & "&st=" & Request("st")), True)
            End If
        End Sub

        '*********************************************************
        ' @@ PURPOSE : TO edit part data.
        ' @@ INPUTS  : NA
        ' @@ RETURNS : NA
        '*********************************************************
        Public Sub EditPartBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPart.ItemCommand
            If (e.CommandName.CompareTo("EditPartBtn") = 0) Then
                Response.Redirect(chk.urlRand("/product/ItemStruModify.aspx?id=" & Request("id") & "&psid=" & e.Item.Cells(0).Text.Trim() _
                                & "&code=" & Request("code") & "&cat=" & Request("cat") & "&semi=" & Request("semi") & "&scat=PART&pg=" _
                                & Request("pg") & "&st=" & Request("st")), True)
            End If
        End Sub

        '*********************************************************
        ' @@ PURPOSE : TO edit Semis data.
        ' @@ INPUTS  : NA
        ' @@ RETURNS : NA
        '*********************************************************
        Public Sub EditProdBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProd.ItemCommand
            If (e.CommandName.CompareTo("EditProdBtn") = 0) Then
                Response.Redirect(chk.urlRand("/product/ItemStruModify.aspx?id=" & Request("id") & "&psid=" & e.Item.Cells(0).Text.Trim() _
                                & "&code=" & Request("code") & "&cat=" & Request("cat") & "&semi=" & Request("semi") & "&scat=PROD&pg=" _
                                & Request("pg") & "&st=" & Request("st")), True)
            End If
        End Sub

        '*********************************************************
        ' @@ PURPOSE : TO delete Semis data.
        ' @@ INPUTS  : NA
        ' @@ RETURNS : NA
        '*********************************************************
        Public Sub DeleteProdBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProd.ItemCommand
            If (e.CommandName.CompareTo("DeleteProdBtn") = 0) Then
                strsql = " UPDATE Product_Stru set NumOfChild_temp=-1 Where productStruID='" & e.Item.Cells(0).Text.Trim() & "' And plantCode='" & Session("plantcode") & "' "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") _
                                & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st")), True)
            End If
        End Sub

        Private Sub BtnPartAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPartAddNew.Click
            Response.Redirect(chk.urlRand("/product/ItemStruModify.aspx?id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") _
                            & "&cat=" & Request("cat") & "&scat=PART&pg=" & Request("pg") & "&st=" & Request("st")), True)
        End Sub

        Private Sub btnProdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnProdAddNew.Click
            Response.Redirect(chk.urlRand("/product/ItemStruModify.aspx?id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") _
                            & "&cat=" & Request("cat") & "&scat=PROD&pg=" & Request("pg") & "&st=" & Request("st")), True)
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            Response.Redirect(chk.urlRand("/product/ItemStructure.aspx?id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") _
                            & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st")), True)
        End Sub

        Private Sub BtnStruSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStruSave.Click
            saveStru()

            ltlAlert.Text = "alert('保存成功！');"
            BtnStruSave.Enabled = False

        End Sub

        Sub GetPart(ByVal proID As Long, ByVal Qty As Decimal, Optional ByVal tag As Integer = 0)
            Dim rd As SqlDataReader
            strsql = "Select ProductStruID,childID,isnull(numOfChild,0),childCategory,notes, posCode From Product_Stru Where productID=" & proID & " And numOfChild<>0 "
            rd = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
            While rd.Read()
                If rd("childCategory") = "PART" Then
                    strsql = "Insert Into CalPart_Temp(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & rd(0) & "','" & Request("id") & "','" & rd(1) & "'," & rd(2) & "*" & Qty & ",'" & rd(3) & "',N'" & rd(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & rd(5).ToString.Replace("'", "''") & "') "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
                Else
                    strsql = "Insert Into CalPart_Temp(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & rd(0) & "','" & Request("id") & "','" & rd(1) & "'," & rd(2) & "*" & Qty & ",'" & rd(3) & "',N'" & rd(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & rd(5).ToString.Replace("'", "''") & "') "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
                    GetPart(rd(1), rd(2) * Qty, tag + 1)
                End If
            End While
            rd.Close()
        End Sub

        Public Sub EditPartReplace_Click(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPart.ItemCommand
            If (e.CommandName.CompareTo("EditPartReplace") = 0) Then
                Response.Redirect("ItemReplace.aspx?psid=" & e.Item.Cells(0).Text.Trim() & "&id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st"), True)
            End If
        End Sub

        Public Sub EditProdReplace_Click(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProd.ItemCommand
            If (e.CommandName.CompareTo("EditProdReplace") = 0) Then
                Response.Redirect("ItemReplace.aspx?psid=" & e.Item.Cells(0).Text.Trim() & "&id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st"), True)
            End If
        End Sub

        Public Sub BtnStruUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStruUpdate.Click
            Dim strSQLa As String
            Dim strSQLb As String
            Dim param(1) As SqlParameter
            Dim ver, i, retValue As Integer
            Dim partID As Integer

            strSQLa = "delete from ItemUsedList where userID='" & Session("uID") & "' and  plantID='" & Session("plantCode") & "' "
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQLa)
            strSQLb = "Insert Into ItemUsedList(itemID, userID, plantID,  isUpdate) Values('" & Request("id") & "','" & Session("uID") & "','" & Session("plantCode") & "',0)"
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQLb)

            Response.Redirect("ItemStruUpdate.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" _
                     & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "try=" & Request("try") & "&rm=" & DateTime.Now() & Rnd() & "' ")

        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            Dim i As Integer
            For i = 0 To dgPart.Items.Count - 1
                strsql = " update Product_Stru set NumOfChild_temp=-1 Where productStruID='" & dgPart.Items(i).Cells(0).Text.Trim() & "' And plantCode='" & Session("plantcode") & "' "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
            Next

            For i = 0 To dgProd.Items.Count - 1
                strsql = " update Product_Stru set NumOfChild_temp=-1 Where productStruID='" & dgProd.Items(i).Cells(0).Text.Trim() & "' And plantCode='" & Session("plantcode") & "' "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
            Next

            saveStru()

            Response.Redirect("/product/productlist.aspx?rm=" & DateTime.Now())
        End Sub

        Private Function saveStru()
            Dim Version As Integer = 1
            ' insert his 
            If Request("id") <> Nothing Then
                strsql = " Delete From CalPart_Temp Where createdBy='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

                GetPart(Request("id"), 1)
                strsql = "select isnull(max(version),0) from product_Stru_His where productID='" & Request("id") & "'"
                Version = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)

                strsql = " select childID,isnull(numofchild,0),childcategory,isnull(notes,''),tag, isnull(posCode,''), productStruID from CalPart_Temp " _
                       & " where createdBy='" & Session("uID") & "' And plantID='" & Session("plantCode") & "' And productID=" & Request("id") & " order by id "
                reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                While reader.Read
                    strsql = "insert into Product_Stru_His(productID,childID,numOfChild,childCategory,notes,tag,createdBy,createdDate,version,plantcode,posCode) "
                    strsql &= " values('" & Request("id") & "','" & reader(0) & "','" & reader(1) & "','" & reader(2) & "',N'" & chk.sqlEncode(reader(3)) & "','" & reader(4) & "','" & Session("uID") & "','" & Format(DateTime.Now, "yyyy-MM-dd") & "'," & Version + 1 & ",'" & Session("plantcode") & "',N'" & chk.sqlEncode(reader(5)) & "') "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

                    strsql = " select top 1 productStruID From Product_Stru_His Where createdBy='" & Session("uID") & "' And plantcode='" & Session("plantCode") & "' Order by productStruID Desc "
                    Dim psID As String = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)

                    'update replace
                    strsql = " Delete From product_replace Where prodStruID='" & reader(6) & "' And itemID_temp=0 "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

                    'insert replace history
                    strsql = " Insert Into product_replace_his(prodStruID, itemID, version) Select " & psID.Trim() & ", itemID," & Version + 1 & " from product_replace where prodStruID='" & reader(6) & "' And itemID_temp<>0 "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
                End While
                reader.Close()
            End If

            'update stru
            strsql = "delete from product_stru where productID=" & Request("id") & " And numofchild_temp=-1 "
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

            strsql = "select productstruID,isnull(numofchild_temp,0),isnull(notes_temp,''), isnull(posCode_temp,'') from product_stru where productID='" & Request("id") & "'"
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
            While (reader.Read)
                strsql = "update product_stru set numofchild='" & reader(1) & "',notes=N'" & chk.sqlEncode(reader(2)) & "',posCode=N'" & chk.sqlEncode(reader(3)) & "',plantCode='" & Session("plantcode") & "'"
                strsql &= "where productstruID='" & reader(0) & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
            End While
            reader.Close()

            'update product structure replace
            strsql = " Select productstruID From product_stru where productID='" & Request("id") & "'"
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
            While reader.Read()
                strsql = " Update product_replace Set itemID=itemID_temp Where prodStruID='" & reader(0) & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
            End While
            reader.Close()
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique")
        End Function
    End Class

End Namespace
