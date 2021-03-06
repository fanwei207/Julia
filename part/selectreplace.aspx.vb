Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System


Namespace tcpc


Partial Class selectreplace
    Inherits System.Web.UI.Page
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

    ' Protected WithEvents dgProd As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel


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
            clearPartTemp()
            If Request("code") <> Nothing Then
                lblProdCode.Text = Server.UrlDecode(Request("code"))
                clearPartTemp()
                strsql = "select id from items where code=N'" & Server.UrlDecode(Request("code")) & "'"
                GetPart(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql), 1)
                BindData()
            End If
        End If
    End Sub
    Sub BindData()
        Dim qty As Decimal
        strsql = " Select I.Code, I.Description, cpt.numOfChild, cpt.notes, cpt.productStruID, cpt.childID,isnull(cpt.tag,0),cpt.ID, cpt.posCode,I.status,ic.name " _
               & " From Items I  Inner Join ItemCategory ic on ic.id=I.category " _
               & " Inner Join Rep_CalPart_Temp cpt on cpt.childID=I.ID And cpt.CreatedBy='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
        strsql = strsql & " Order By cpt.id "

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
        dtlPart.Columns.Add(New DataColumn("cID", System.Type.GetType("System.Decimal")))
        dtlPart.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
        dtlPart.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
        dtlPart.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dtlPart.Columns.Add(New DataColumn("RID", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                Dim j As Integer
                Dim drowPart As DataRow
                Dim strPos As String = ""
                Dim strPos1 As String = ""
                For i = 0 To .Rows.Count - 1

                    drowPart = dtlPart.NewRow()
                    drowPart.Item("gsort") = i + 1
                    drowPart.Item("ProdStruID") = .Rows(i).Item(4).ToString().Trim()
                    'status
                    If .Rows(i).Item(9).ToString().Trim() = "0" Then
                        drowPart.Item("status") = "使用"
                    ElseIf .Rows(i).Item(9).ToString().Trim() = "1" Then
                        drowPart.Item("status") = "试用"
                    Else
                        drowPart.Item("status") = "停用"
                    End If

                    Dim tag As String = ""
                    For j = 1 To .Rows(i).Item(6)
                        tag = tag & "---"
                    Next

                    drowPart.Item("partcode") = tag & " " & .Rows(i).Item(0).ToString().Trim()
                    drowPart.Item("partdesc") = .Rows(i).Item(1).ToString().Trim()
                    qty = CDec(.Rows(i).Item(2))
                    If qty = CInt(qty) Then
                        drowPart.Item("PartQty") = CInt(qty)
                    Else
                        drowPart.Item("PartQty") = qty
                    End If
                    drowPart.Item("partMemo") = .Rows(i).Item(3).ToString().Trim()
                    drowPart.Item("partPos") = .Rows(i).Item(8).ToString().Trim()
                    drowPart.Item("pID") = .Rows(i).Item(5).ToString().Trim()
                    drowPart.Item("cID") = .Rows(i).Item(7)
                    'load product_replace
                    strsql = " Select i.code,i.id From product_replace pr Inner Join Items i On pr.itemID=i.id Where pr.prodStruID='" & .Rows(i).Item(4).ToString().Trim() & "'"
                    reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                    If reader.HasRows Then
                        Dim r As Integer = 0
                        While reader.Read()
                            strPos &= reader(0) & ","
                            strPos1 &= reader(1) & ","
                            r += 1
                        End While
                        strPos = CStr(r) & strPos
                        strPos1 = CStr(r) & strPos1
                    End If
                    reader.Close()
                    If strPos.Trim() <> "" Then
                        drowPart.Item("partReplace") = strPos.Substring(0, Len(strPos.Trim()) - 1).Trim()
                        drowPart.Item("RID") = strPos1.Substring(0, Len(strPos1.Trim()) - 1).Trim()
                        'Me.Response.Write(strPos.Substring(0, Len(strPos.Trim()) - 1).Trim())
                        'Me.Response.Write("<br>")
                        'Me.Response.Write(strPos1.Substring(0, Len(strPos1.Trim()) - 1).Trim())
                        'Me.Response.Write("<br>")
                    Else
                        drowPart.Item("partReplace") = ""
                        drowPart.Item("RID") = ""
                    End If

                    drowPart.Item("type") = .Rows(i).Item(10).ToString().Trim()
                    dtlPart.Rows.Add(drowPart)
                    strPos = ""
                    strPos1 = ""
                Next
            End If
        End With
        Dim dvPart As DataView
        dvPart = New DataView(dtlPart)
        'Try
        dgPart.DataSource = dvPart
        dgPart.DataBind()
        'Catch
        'End Try
        dst.Reset()
    End Sub
    Private Sub dgPart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPart.PageIndexChanged
        dgPart.CurrentPageIndex = e.NewPageIndex()
        BindData()
    End Sub

    Private Sub dgPart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPart.SortCommand
        Session("orderby") = e.SortExpression.ToString()
        If Session("orderdir") = " ASC" Then
            Session("orderdir") = " DESC"
        Else
            Session("orderdir") = " ASC"
        End If
        BindData()
    End Sub

    Sub clearPartTemp()
        strsql = " Delete From Rep_CalPart_Temp Where createdBy='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
    End Sub

    Sub GetPart(ByVal proID As Integer)

        Dim param(2) As SqlParameter
        strsql = "Part_GetPart"
        param(0) = New SqlParameter("@intProdID", CInt(proID))
        param(1) = New SqlParameter("@intUserID", CInt(Session("uID")))
        param(2) = New SqlParameter("@intPlantID", CInt(Session("plantCode")))
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, strsql, param)
    End Sub
    Sub GetPart(ByVal proID As Long, ByVal Qty As Decimal, Optional ByVal tag As Integer = 0)
        Dim reader As SqlDataReader
        strsql = "Select ProductStruID,childID,isnull(numOfChild,0),childCategory,notes, posCode From Product_Stru Where productID=" & proID & " And numOfChild<>0 "
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
        While reader.Read()
            If reader("childCategory") = "PART" Then
                strsql = "Insert Into Rep_CalPart_Temp(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & reader(0) & "','" & proID & "','" & reader(1) & "'," & reader(2) & "*" & Qty & ",'" & reader(3) & "',N'" & reader(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & reader(5) & "') "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
            Else
                strsql = "Insert Into Rep_CalPart_Temp(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & reader(0) & "','" & proID & "','" & reader(1) & "'," & reader(2) & "*" & Qty & ",'" & reader(3) & "',N'" & reader(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & reader(5) & "') "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
                GetPart(reader(1), reader(2) * Qty, tag + 1)
            End If
        End While
        reader.Close()

    End Sub

    Private Sub dgPart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPart.ItemDataBound
        Dim drpRep As New DropDownList
        If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
            drpRep = DirectCast(e.Item.FindControl("drpRep"), DropDownList)

            Dim strRID As String = e.Item.Cells(12).Text.Replace("&nbsp;", "").Trim
            Dim strRCODE As String = e.Item.Cells(2).Text.Trim
            If strRID.Trim.Length <= 0 Then
                drpRep.Visible = False
            ElseIf strRID.Trim.Length > 0 Then
                Dim ht As New Hashtable
                Dim k As Integer = CInt(strRID.Substring(0, 1))
                Dim m As Integer
                For m = 0 To k - 1
                    If strRID.IndexOf(",") > 0 Then
                        ht.Add(strRID.Substring(1, strRID.IndexOf(",") - 1), strRCODE.Substring(1, strRCODE.IndexOf(",") - 1))
                        strRID = strRID.Substring(strRID.IndexOf(","))
                        strRCODE = strRCODE.Substring(strRCODE.IndexOf(","))
                    Else
                        strRID = strRID.Substring(1)
                        strRCODE = strRCODE.Substring(1)
                        ht.Add(strRID, strRCODE)
                    End If
                Next
                Dim dt As New DataTable
                Dim drow As DataRow
                dt.Columns.Add(New DataColumn("repcode", System.Type.GetType("System.String")))
                dt.Columns.Add(New DataColumn("repid", System.Type.GetType("System.String")))
                drow = dt.NewRow()
                drow.Item("repid") = "0"
                drow.Item("repcode") = "--"
                dt.Rows.Add(drow)

                Dim myEnumerator As IDictionaryEnumerator = ht.GetEnumerator()
                While myEnumerator.MoveNext()
                    drow = dt.NewRow()
                    drow.Item("repid") = myEnumerator.Key
                    drow.Item("repcode") = myEnumerator.Value
                    dt.Rows.Add(drow)
                End While
                myEnumerator.Reset()
                Dim dv As DataView
                dv = New DataView(dt)
                drpRep.DataSource = dv
                drpRep.DataBind()
            End If

        End If
    End Sub
    Sub drprepchange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim item As System.Web.UI.WebControls.DataGridItem = DirectCast(DirectCast(sender, Control).Parent.Parent, DataGridItem)
        Dim ddl As DropDownList
        ddl = DirectCast(item.FindControl("drpRep"), DropDownList)
        If ddl.SelectedValue > 0 Then
            findrep(item.Cells(11).Text, item.Cells(10).Text, item.Cells(0).Text, ddl.SelectedValue)
        ElseIf ddl.SelectedValue = 0 Then
            clearPartTemp()
            strsql = "select id from items where code=N'" & Server.UrlDecode(Request("code")) & "'"
            GetPart(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql))
        End If
        BindData()
    End Sub
    Sub findrep(ByVal cid As String, ByVal pid As String, ByVal struid As String, ByVal repid As String)
        Dim param(4) As SqlParameter
        strsql = "updatereplace"
        param(0) = New SqlParameter("@cid", CInt(cid))
        param(1) = New SqlParameter("@pid", CInt(pid))
        param(2) = New SqlParameter("@struid", CInt(struid))
        param(3) = New SqlParameter("@repid", CInt(repid))
        param(4) = New SqlParameter("@uid", CInt(Session("uID")))
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, strsql, param)
    End Sub


    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        strsql = "tranreplace"
        Dim param1(2) As SqlParameter
        param1(0) = New SqlParameter("@qty", Convert.ToDouble(Request("qty")))
        param1(1) = New SqlParameter("@mrpid", CInt(Request("mrpid")))
        param1(2) = New SqlParameter("@uid", CInt(Session("uID")))
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, strsql, param1)
        ltlAlert.Text = "post();"
    End Sub
End Class

End Namespace
