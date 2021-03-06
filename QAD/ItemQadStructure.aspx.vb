'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-07-10
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Item Qad Structure
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class ItemQadStructure
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim strsql As String
    Dim dst As DataSet
    Dim nRet As Integer

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
            'Security check
                If Not Security("458019").isValid Then
                    BtnTrans.Enabled = False
                Else
                    BtnTrans.Enabled = True
                End If
            clearPartTemp()
            If Request("id") <> Nothing Then
                GetPart(Request("id"), 1)
                strsql = " Select i.item_qad, ps.createdDate From Items i Left Join Product_Stru ps ON i.id=ps.productID Where i.id='" & Request("id") & "'  "
                dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)
                With dst.Tables(0)
                    If .Rows.Count > 0 Then
                        lblProdCode.Text = .Rows(0).Item(0).ToString().Trim()
                        If Not .Rows(0).IsNull(1) Then
                            Label1.Text = .Rows(0).Item(1)
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
    Sub BindData()
        Dim qty As Decimal
        Dim reader As SqlDataReader
        strsql = " Select Isnull(i.item_qad,''), i.Description, cpt.numOfChild, cpt.notes, cpt.productStruID, cpt.childID, Isnull(cpt.tag,0), cpt.ID, cpt.posCode, i.status, ic.name, i.code, Isnull(i.item_qad_desc1,''), Isnull(i.item_qad_desc2,''), Isnull(ip.item_qad,'') " _
               & " From Items i Inner Join ItemCategory ic on ic.id=i.category " _
               & " Inner Join CalPart_temp_stru cpt on cpt.childID=i.id And cpt.CreatedBy='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'" _
               & " Inner Join items ip On ip.id=cpt.productID "
        If CheckBox1.Checked = False Then
            strsql = strsql & " and cpt.tag = 0 "
        End If
        strsql = strsql & " Order By cpt.ID "
        dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)

        Session("EXSQL") = strsql
        'Session("EXHeader") = "/Qad/ItemQadStructureExport.aspx^~^产品型号：" & lblProdCode.Text
        Session("EXHeader") = "/Qad/ItemQadStructureExport.aspx^~^" & lblProdCode.Text
        Session("EXTitle") = "150^<b>父零件</b>~^150^<b>子零件</b>~^350^<b>部件号</b>~^<b>级</b>~^<b>A尺寸</b>~^<b>B尺寸</b>~^<b>C尺寸</b>~^<b>图号</b>~^<b>状态</b>~^<b>分类</b>~^150^<b>数量</b>~^150^<b>位号</b>~^200^<b>替代品</b>~^<b>部件号</b>~^<b>A尺寸</b>~^<b>B尺寸</b>~^<b>C尺寸</b>~^<b>图号</b>~^250^<b>备注</b>~^1500^<b>部件描述</b>~^150^<b>QAD描述1</b>~^150^<b>QAD描述2</b>~^"

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

        With dst.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                Dim j As Integer
                Dim drowPart As DataRow
                Dim strPos As String = ""
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

                    drowPart.Item("partcode") = tag & " " & .Rows(i).Item(0).ToString().Trim() & " ( " & .Rows(i).Item(11).ToString().Trim() & " ) "
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
                    strsql = " Select Isnull(i.item_qad,'')+'('+i.code+')' From product_replace pr Inner Join Items i On pr.itemID=i.id Where pr.prodStruID='" & .Rows(i).Item(4).ToString().Trim() & "'"
                    reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                    While reader.Read()
                        strPos &= reader(0) & ","
                    End While
                    reader.Close()
                    If strPos.Trim() <> "" Then
                        drowPart.Item("partReplace") = strPos.Substring(0, Len(strPos.Trim()) - 1).Trim()
                    Else
                        drowPart.Item("partReplace") = ""
                    End If
                    drowPart.Item("type") = .Rows(i).Item(10).ToString().Trim()
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
        strsql = " Delete From CalPart_temp_stru Where createdBy='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
    End Sub

    Sub GetPart(ByVal proID As Long, ByVal Qty As Decimal, Optional ByVal tag As Integer = 0)
        Dim reader As SqlDataReader
        strsql = "Select ProductStruID,childID,isnull(numOfChild,0),childCategory,notes, posCode From Product_Stru Where productID=" & proID & " And numOfChild<>0 "

        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
        While reader.Read()
            If reader("childCategory") = "PART" Then
                strsql = "Insert Into CalPart_temp_stru(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & reader(0) & "','" & proID & "','" & reader(1) & "'," & reader(2) & "*" & Qty & ",'" & reader(3) & "',N'" & reader(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & reader(5) & "') "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
            Else
                strsql = "Insert Into CalPart_temp_stru(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & reader(0) & "','" & proID & "','" & reader(1) & "'," & reader(2) & "*" & Qty & ",'" & reader(3) & "',N'" & reader(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & reader(5) & "') "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
                GetPart(reader(1), reader(2) * Qty, tag + 1)
            End If
        End While
        reader.Close()

        'Dim params(2) As SqlParameter
        'params(0) = New SqlParameter("@intProdID", Convert.ToInt32(proID))
        'params(1) = New SqlParameter("@intUserID", Convert.ToInt32(Session("uID")))
        'params(2) = New SqlParameter("@intPlantID", Convert.ToInt32(Session("PlantCode")))
        'SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, "Item_ExtendStru", params)

    End Sub

    Private Sub BtnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
        Response.Redirect(chk.urlRand("/Qad/Item_Qad_list.aspx?code=" & Request("code") & "&qad=" & Request("qad") & "&pg=" & Request("pg")), True)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        dgPart.CurrentPageIndex = 0
        BindData()
    End Sub

    Private Sub BtnTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTrans.Click
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, "CopyStru2QadStru", New SqlParameter("@itemid", Convert.ToInt32(Request("id"))))
        ltlAlert.Text = "alert('转换结束！');"
    End Sub
End Class

End Namespace
