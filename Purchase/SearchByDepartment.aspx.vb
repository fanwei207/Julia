'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-9-8
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   View Part Summary By Department
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class SearchByDepartment
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
        End Select
        strSql = " Select d.name, i.code, Isnull(Abs(Sum(pt.tran_qty)),0), pt.tran_type, case when i.type=0 then N'部件' else N'半成品' end as itype From departments d " _
               & " Left Outer Join Part_tran pt ON pt.comp_dept_id=d.departmentID And (pt.tran_type='DR' or pt.tran_type='O' or pt.tran_type='PM') " _
               & " And pt.tran_date>='" & txtStartDate.Text.Trim() & "' And pt.tran_date<'" & Convert.ToDateTime(txtEndDate.Text.Trim()).AddDays(1) _
               & "' And Isnull(pt.status,0)='" & ddlStatus.SelectedValue & "'"
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
        strSql &= " Left Outer Join tcpc0.dbo.Items i On pt.part_id = i.id And i.status<>2 " _
               & " Where d.active=1 And d.code=N'" & chk.sqlEncode(txtDeptCode.Text.Trim()) & "'" _
               & " Group By d.name, i.code, pt.tran_type,i.type Order By d.name, i.code "
        Session("EXSQL") = strSql
        Session("EXTitle") = "200^<b>部件号</b>~^100^<b>库存数量</b>~^50^<b>类型</b>~^<b>种类</b>~^"


        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("total", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("itype", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    If IsDBNull(.Rows(i).Item(1)) = False Then
                        drow = dtl.NewRow()
                        drow.Item("partcode") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("type") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("total") = .Rows(i).Item(2)
                        drow.Item("itype") = .Rows(i).Item(4).ToString().Trim()
                        If ddlStatus.SelectedValue <> 0 Then
                            drow.Item("status") = ddlStatus.SelectedItem.Text.Trim()
                        Else
                            drow.Item("status") = ""
                        End If
                        dtl.Rows.Add(drow)
                        lblDpname.Text = .Rows(i).Item(0).ToString().Trim()
                        Session("EXHeader") = "部门名称~" & .Rows(i).Item(0).ToString().Trim() & "~^开始日期~" & txtStartDate.Text.Trim() _
                                            & "~^终止日期~" & txtEndDate.Text.Trim() & "~^仓库~" & ddlWhplace.SelectedItem.Text.Trim() _
                                            & "~^类型~" & typeDDL.SelectedItem.Text.Trim() & "~^"
                    Else
                        ltlAlert.Text = "alert('部门代码为" & txtDeptCode.Text.Trim() & "所对应的记录不存在！');"
                        lblDpname.Text = .Rows(i).Item(0).ToString().Trim()
                        Session("EXHeader") = ""
                        Session("EXSQL") = Nothing
                        Session("EXTitle") = Nothing
                    End If
                Next
            Else
                'If txtDeptCode.Text.Trim().Length > 0 Then
                '    ltlAlert.Text = "alert('没有找到相关记录！');"
                '    lblDpname.Text = ""
                'End If
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
        Session("orderby") = e.SortExpression
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

