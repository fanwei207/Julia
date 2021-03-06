'!*******************************************************************************!
'* @@ NAME				:	SearchbyPartCode.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for SearchbyPartCode.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	May 11 2005
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class SearchbyPartCode
        Inherits BasePage

    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
    Dim reader As SqlDataReader
    Public chk As New adamClass
    Dim dst As DataSet
    Protected WithEvents CheckBox1 As System.Web.UI.WebControls.CheckBox
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
           
            If Request("code") <> Nothing Then
                code.Text = Request("code")
            End If
            If Request("cat") <> Nothing Then
                type.Text = Request("cat")
            End If
            If Request("pg") <> Nothing Then
                DataGrid1.CurrentPageIndex = CInt(Request("pg"))
            End If
            If Request("inv") <> Nothing Then
                inv.Checked = True
            End If
            whplacedropdownlist()
            statusdropdownlist()
            typeDropdownlist()
            BindData()

            Dim i As Integer
            For i = 0 To DataGrid1.Columns.Count - 1
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, "Select userID from profile_display where userID='" & Session("uID") & "' and moduleID='" & Session("ModuleID") & "' and disableItem=N'" & DataGrid1.Columns(i).HeaderText.Replace("<b>", "").Replace("</b>", "") & "'") > 0 Then
                    DataGrid1.Columns(i).Visible = False
                End If
            Next
        End If
        ltlAlert.Text = "Form1.code.focus();"
    End Sub

    Sub BindData()
        Session("EXTitle") = "200^<b>部件号</b>~^500^<b>部件描述</b>~^70^<b>分类</b>~^70^<b>种类</b>~^100^<b>当前库存量</b>~^100^<b>可用库存量</b>~^50^<b>单位</b>~^"
        Session("EXHeader") = "仓库~" & ddlWhplace.SelectedItem.Text.Trim() & "~^"

        strSql = " Select i.id, Isnull(i.min_inv,0), i.code, i.description,  ic.name, case when i.type=0 then N'部件' else N'半成品' end as itype, Isnull(pi.total_qty,0), Isnull(pi.total_qty,0)-Isnull(plan_qty,0), isnull(i.unit,'') " _
               & " From tcpc0.dbo.Items i " _
               & " Left Outer Join tcpc0.dbo.ItemCategory ic On i.category=ic.id " _
               & " Left Outer Join Part_inv pi On i.id=pi.part_id And pi.warehouseID='" & ddlWhplace.SelectedValue.Trim() _
               & "' And Isnull(pi.status,0)=" & ddlStatus.SelectedValue
        'If Session("uRole") <> 1 Then
        '    strSql &= " Inner Join tcpc0.dbo.User_pricesbycategory up on up.categoryid=i.category and up.userid='" & Session("uID") & "' "
        'End If
        strSql &= " Where i.status='" & ddlType.SelectedValue & "'"
        If code.Text.Trim <> "" Then
            strSql &= " And lower(i.code) like N'%" & chk.sqlEncode(code.Text.Trim().ToLower()) & "%'"
        End If
        If type.Text.Trim <> "" Then
            strSql &= "  And ic.name=N'" & chk.sqlEncode(type.Text.Trim()) & "'"
        End If
        If inv.Checked = True Then
            strSql &= "  And pi.total_qty<>0 "
        End If
        strSql &= " Order By ic.name, i.code "
        Session("EXSQL") = strSql

        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("partID", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("PartDescription", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("Category", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("quantity", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("qty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("mintotal", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("minqty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("actual", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("actualqty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("unit", System.Type.GetType("System.String")))

        With dst.Tables(0)
            lblCount.Text = "数量:" & .Rows.Count.ToString().Trim()
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("gsort") = i + 1
                    drow.Item("partID") = .Rows(i).Item(0).ToString().Trim()
                    drow.Item("partcode") = .Rows(i).Item(2).ToString().Trim()
                    drow.Item("PartDescription") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("Category") = .Rows(i).Item(4).ToString().Trim()
                    drow.Item("quantity") = Format(.Rows(i).Item(6), "##,##0.00").ToString().Trim()
                    drow.Item("qty") = .Rows(i).Item(6)
                    drow.Item("mintotal") = Format(.Rows(i).Item(1), "##,##0.00").ToString().Trim()
                    drow.Item("minqty") = .Rows(i).Item(1)
                    drow.Item("type") = .Rows(i).Item(5)
                    drow.Item("actual") = Format(.Rows(i).Item(7), "##,##0.00").ToString().Trim()
                    drow.Item("actualqty") = .Rows(i).Item(7)
                    drow.Item("unit") = .Rows(i).Item(8).ToString().Trim()
                    dtl.Rows.Add(drow)
                Next
            Else
                'ltlAlert.Text = "alert('没有找到查询的部件！');"
                Session("EXSQL") = Nothing
                Session("EXTitle") = Nothing
                Session("EXHeader") = Nothing
            End If
        End With

        Dim dvw As DataView
        dvw = New DataView(dtl)

            Session("orderby") = "gsort"

            Try
                dvw.Sort = Session("orderby") & " " & Session("orderdir")
                DataGrid1.DataSource = dvw
                DataGrid1.DataBind()
            Catch
            End Try
            dst.Reset()
        End Sub

    Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
        DataGrid1.CurrentPageIndex = 0
        BindData()
    End Sub

    Public Sub BtnDetail(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandName.CompareTo("BtnDetail") = 0) Then
            Response.Redirect(chk.urlRand("/Purchase/PurchaseQtyDetail.aspx?partID=" & e.Item.Cells(0).Text() & "&placeID=" & ddlWhplace.SelectedValue _
                            & "&place=" & Server.UrlEncode(ddlWhplace.SelectedItem.Text.Trim()) & "&partcode=" & Server.UrlEncode(e.Item.Cells(1).Text.Trim()) _
                            & "&partdesc=" & Server.UrlEncode(e.Item.Cells(12).Text.Trim()) & "&totalQty=" & e.Item.Cells(5).Text.Trim() & "&code=" _
                            & chk.sqlEncode(code.Text.Trim()) & "&minQty=" & e.Item.Cells(6).Text.Trim() & "&cat=" & chk.sqlEncode(type.Text.Trim()) _
                            & "&pg=" & DataGrid1.CurrentPageIndex & "&st=" & ddlStatus.SelectedValue & "&name=" & Server.UrlEncode(ddlStatus.SelectedItem.Text.Trim())), True)
        End If
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex()
        BindData()
    End Sub

    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        Session("orderby") = e.SortExpression.ToString()
        If Session("orderdir") = "ASC" Then
            Session("orderdir") = "DESC"
        Else
            Session("orderdir") = "ASC"
        End If
        BindData()
    End Sub

    Sub whplacedropdownlist()
        Dim reader As SqlDataReader
        Dim ls As New ListItem

        strSql = " Select w.warehouseID, w.name From warehouse w "
        If Session("uRole") <> 1 Then
            strSql &= " Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "'"
        End If
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1)
            ddlWhplace.Items.Add(ls)
        End While
        reader.Close()
        If Request("placeID") <> Nothing Then
            ddlWhplace.SelectedValue = Request("placeID")
        End If
    End Sub

    Public Sub BtnAdjust(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            Dim cnt As Integer 
            If (e.CommandName.CompareTo("BtnAdjust") = 0) Then 
                If (Not Me.Security("10000030").isValid) Then  
                    ltlAlert.Text = "alert('没有权限进行材料库存的调整！');"
                    Exit Sub
                Else
                    If Session("uRole") <> 1 Then
                        If Request("placeID") = Nothing Then
                            strSql = " Select Count(*) From User_Warehouse Where warehouseID='" & ddlWhplace.SelectedValue & "' And userID='" & Session("uID") & "'"
                        Else
                            strSql = " Select Count(*) From User_Warehouse Where warehouseID='" & Request("placeID") & "' And userID='" & Session("uID") & "'"
                        End If
                        cnt = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                    Else
                        cnt = 1
                    End If
                    If cnt > 0 Then
                        ltlAlert.Text = "var w=window.open('/Purchase/PartQtyAdjust.aspx?partID=" & e.Item.Cells(0).Text().Trim() & "&partcode=" _
                                      & Server.UrlEncode(e.Item.Cells(1).Text.Trim()) & "&partdesc=" & Server.UrlEncode(e.Item.Cells(12).Text.Trim()) _
                                      & "&placeID=" & ddlWhplace.SelectedValue & "&place=" & Server.UrlEncode(ddlWhplace.SelectedItem.Text.Trim()) _
                                      & "&totalQty=" & e.Item.Cells(5).Text.Trim() & "&code=" & chk.sqlEncode(code.Text.Trim()) & "&cat=" _
                                      & chk.sqlEncode(type.Text.Trim()) & "&st=" & ddlStatus.SelectedValue & "&name=" _
                                      & Server.UrlEncode(ddlStatus.SelectedItem.Text.Trim()) _
                                      & "','InventoryADJ','menubar=no,scrollbars = no,resizable=no,width=600,height=300,top=100,left=100'); w.focus();"
                    Else
                        ltlAlert.Text = "alert('没有权限对" & ddlWhplace.SelectedItem.Text.Trim() & "仓库进行库存调整！');"
                        Exit Sub
                    End If
                End If
            End If
    End Sub

    Public Sub BtnIOSum(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandName.CompareTo("BtnIOSum") = 0) Then
            ltlAlert.Text = "var w=window.open('/Purchase/partIOSum.aspx?id=" & e.Item.Cells(0).Text().Trim() & "&partcode=" _
                          & Server.UrlEncode(e.Item.Cells(1).Text.Trim()) & "&partdesc=" & Server.UrlEncode(e.Item.Cells(12).Text.Trim()) _
                          & "&pid=" & ddlWhplace.SelectedValue & "&st=" & ddlStatus.SelectedValue & "&name=" _
                          & Server.UrlEncode(ddlStatus.SelectedItem.Text.Trim()) & "','TransIOSum','menubar=no,scrollbars = no,resizable=no,width=600,height=300,top=100,left=100'); w.focus();"
        End If
    End Sub

    Sub statusdropdownlist()
        ddlStatus.Items.Add(New ListItem("--", "0"))
        strSql = " Select id, StatusName From tcpc0.dbo.Status "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read
            Dim tempListItem As New ListItem
            tempListItem.Value = reader(0)
            tempListItem.Text = reader(1)
            ddlStatus.Items.Add(tempListItem)
        End While
        reader.Close()
        If Request("st") <> Nothing Then
            ddlStatus.SelectedValue = Request("st")
        End If
    End Sub

    Public Sub BtnTranRej(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        Dim cnt As Integer
        If (e.CommandName.CompareTo("BtnTranRej") = 0) Then 
                If (Not Me.Security("20030504").isValid) Then 
                    ltlAlert.Text = "alert('没有权限进行材料库存转废品库存的操作！');"
                    Exit Sub
                Else
                    If Session("uRole") <> 1 Then
                        strSql = " Select Count(*) From User_Warehouse Where warehouseID='" & Request("placeID") & "' And userID='" & Session("uID") & "'"
                        cnt = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                    Else
                        cnt = 1
                    End If
                    If cnt > 0 Then
                        ltlAlert.Text = "var w=window.open('/Purchase/PartQtyTrans.aspx?partID=" & e.Item.Cells(0).Text().Trim() & "&partcode=" _
                                      & Server.UrlEncode(e.Item.Cells(1).Text.Trim()) & "&partdesc=" & Server.UrlEncode(e.Item.Cells(12).Text.Trim()) _
                                      & "&placeID=" & ddlWhplace.SelectedValue & "&place=" & Server.UrlEncode(ddlWhplace.SelectedItem.Text.Trim()) _
                                      & "&code=" & chk.sqlEncode(code.Text.Trim()) & "&cat=" & chk.sqlEncode(type.Text.Trim()) _
                                      & "&st=" & ddlStatus.SelectedValue & "&name=" & Server.UrlEncode(ddlStatus.SelectedItem.Text.Trim()) _
                                      & "','PartTran','menubar=no,scrollbars = no,resizable=no,width=600,height=300,top=100,left=100'); w.focus();"
                    Else
                        ltlAlert.Text = "alert('没有权限对" & ddlWhplace.SelectedItem.Text.Trim() & "仓库进行操作！');"
                        Exit Sub
                    End If
                End If
            End If
    End Sub

    Sub typedropdownlist()
        ddlType.Items.Add(New ListItem("使用", "0"))
        ddlType.Items.Add(New ListItem("试用", "1"))
        ddlType.Items.Add(New ListItem("停用", "2"))
        ddlType.SelectedIndex = 0
    End Sub
End Class

End Namespace
