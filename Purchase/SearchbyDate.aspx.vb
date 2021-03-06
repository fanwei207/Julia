'!*******************************************************************************!
'* @@ NAME				:	SearchbyDate.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for SearchbyDate.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	May 12 2005
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
Partial Class SearchbyDate
        Inherits BasePage

    Dim strSql As String
    Dim reader As SqlDataReader
    Public chk As New adamClass
    Dim dst As DataSet
    'Protected WithEvents ltlAlert As Literal

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Table1 As System.Web.UI.WebControls.Table


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
            txtStartDate.Text = Format(DateTime.Now, "yyyy-MM-01")
            txtEndDate.Text = Format(DateTime.Now, "yyyy-MM-dd")
            whplacedropdownlist()
            typedropdownlist()
            statusdropdownlist()
            BindData()
        End If
    End Sub

    Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
        BindData()
    End Sub

    Sub BindData()
        Dim strtype As String
        Select Case typeDDL.SelectedIndex
            Case 0
                strtype = ""
            Case 1
                strtype = "I"
            Case 2
                strtype = "O"
            Case 3
                strtype = "DR"
            Case 4
                strtype = "RS"
            Case 5
                strtype = "PM"
            Case 6
                strtype = "PMO"
            Case 7
                strtype = "INIT"
            Case 8
                strtype = "ADJ"
        End Select

        strSql = " Select i.code, ic.name, pt.tran_type, Isnull(Sum(pt.tran_qty),0), Case When i.type=0 Then N'部件' Else N'半成品' End As itype, w.name, Isnull(i.unit,''), Isnull(pt.plantid,0), Case Isnull(i.status,0) When 0 Then N'使用' When 1 Then N'试用' Else N'停用' End "
        strSql &= " From tcpc0.dbo.Items i " _
               & " Left Outer Join tcpc0.dbo.ItemCategory ic On i.category=ic.id "
        If txtCat.Text.Trim().Length() > 0 Then
            strSql &= " And ic.name=N'" & chk.sqlEncode(txtCat.Text.Trim()) & "'"
        End If
        strSql &= " Inner Join Part_tran pt ON pt.part_id=i.id And pt.tran_date>='" & txtStartDate.Text.Trim() & "' And pt.tran_date<'" _
               & Convert.ToDateTime(txtEndDate.Text.Trim()).AddDays(1) & "' And Isnull(pt.status,0)='" & ddlStatus.SelectedValue & "'"
        If strtype.Trim().Length > 0 Then
            strSql &= " And pt.tran_type='" & strtype.Trim() & "'"
        End If
        If ddlWhplace.SelectedIndex <> 0 Then
            strSql &= " And pt.warehouseID='" & ddlWhplace.SelectedValue & "'"
        Else
            If Session("uRole") <> 1 Then
                strSql &= " And pt.warehouseID IN (Select w.warehouseID From warehouse w Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "')"
            End If
        End If
        If Session("uRole") <> 1 Then
            strSql &= " Inner Join tcpc0.dbo.User_pricesbycategory up on up.categoryid=i.category and up.userid='" & Session("uID") & "' "
        End If
        strSql &= " Inner Join warehouse w On pt.warehouseID=w.warehouseID " _
               & " Left Outer Join tcpc0.dbo.status s On s.id=pt.status "
        If txtCode.Text.Trim().Length > 0 Then
            strSql &= " Where i.code like N'" & chk.sqlEncode(txtCode.Text.Trim().Replace("*", "%")) & "'"
        End If
        strSql &= "Group By i.code, Isnull(i.status,0), ic.name, pt.tran_type,i.type,w.name, s.statusName, Isnull(i.unit,''), pt.plantid Order By ic.name, w.name, i.code "

        Session("EXSQL") = strSql
      
        Session("EXTitle") = "200^<b>部件号</b>~^50^<b>分类</b>~^50^<b>状态</b>~^200^<b>初始化数量(INIT)</b>~^200^<b>入库数量(IN)</b>~^200^<b>出库数量(OUT)</b>~^200^<b>部门退库数量(RIN)</b>~^200^<b>退供应商数量(ROUT)</b>~^200^<b>委托入库数量(DGI)</b>~^200^<b>委托出库数量(DGO)</b>~^200^<b>委托退库数量(DGRI)</b>~^200^<b>移库入库数量(PM)</b>~^200^<b>移库出库数量(PMO)</b>~^200^<b>调整数量(ADJ)</b>~^100^<b>库存数量</b>~^50^<b>种类</b>~^80^<b>仓库</b>~^50^<b>单位</b>~^"
        Session("EXHeader") = "/Purchase/SearchByDatePrint.aspx?type=" & strtype.Trim() & "^~^"

        Session("EXHeader1") = "开始日期~" & txtStartDate.Text.Trim() & "~^终止日期~" & txtEndDate.Text.Trim() & "~^仓库~" _
                            & ddlWhplace.SelectedItem.Text.Trim() & "~^类型~" & typeDDL.SelectedItem.Text.Trim() _
                            & "~^状态~" & ddlStatus.SelectedItem.Text.Trim() & "~^"

        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("category", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("total", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("itype", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("unit", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("partcode") = .Rows(i).Item(0).ToString().Trim()
                    drow.Item("category") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("type") = .Rows(i).Item(2).ToString().Trim()
                    drow.Item("total") = Math.Abs(.Rows(i).Item(3))
                    drow.Item("itype") = .Rows(i).Item(4).ToString().Trim()
                    If ddlStatus.SelectedValue <> 0 Then
                        drow.Item("status") = ddlStatus.SelectedItem.Text.Trim()
                    Else
                        drow.Item("status") = ""
                    End If
                    drow.Item("unit") = .Rows(i).Item(6).ToString().Trim()
                    dtl.Rows.Add(drow)
                Next
            Else
                'ltlAlert.Text = "alert('没有找到相关记录！');"
                Session("EXSQL") = Nothing
                Session("EXTitle") = Nothing
                Session("EXHeader") = ""
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)

            Session("orderby") = "partcode"

            Try
                dvw.Sort = Session("orderby") & " " & Session("orderdir")
                DataGrid1.DataSource = dvw
                DataGrid1.DataBind()
            Catch
            End Try
            dst.Reset()
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        Session("orderby") = e.SortExpression.ToString().Trim()
        If Session("orderdir") = "ASC" Then
            Session("orderdir") = "DESC"
        Else
            Session("orderdir") = "ASC"
        End If
        BindData()
    End Sub

    Sub Report_click(ByVal sender As Object, ByVal e As System.EventArgs)
        BindData()
    End Sub

    Sub whplacedropdownlist()
        Dim reader As SqlDataReader
        Dim ls As New ListItem

        ls.Value = 0
        ls.Text = "--"
        ddlWhplace.Items.Add(ls)

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
    End Sub

    Private Sub typedropdownlist()
        typeDDL.Items.Add(New ListItem("--", "0"))
        typeDDL.Items.Add(New ListItem("入库", "1"))
        typeDDL.Items.Add(New ListItem("出库", "2"))
        typeDDL.Items.Add(New ListItem("部门退库", "3"))
        typeDDL.Items.Add(New ListItem("退供应商", "4"))
        typeDDL.Items.Add(New ListItem("移库", "5"))
        typeDDL.Items.Add(New ListItem("移库出库", "6"))
        typeDDL.Items.Add(New ListItem("初始化", "7"))
        typeDDL.Items.Add(New ListItem("调整", "8"))
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
    End Sub
End Class

End Namespace
