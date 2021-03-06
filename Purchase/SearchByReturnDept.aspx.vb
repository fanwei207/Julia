'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-9-8
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   View Part Return Summary By Department
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

Partial Class SearchByReturnDept
        Inherits BasePage 
    Dim strSql As String
    Dim reader As SqlDataReader
    Public chk As New adamClass
    Dim dst As DataSet
    Shared sortDir As String = "ASC"
    'Protected WithEvents ltlAlert As Literal
    Shared sortOrder As String = ""

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
            BindData()
        End If
    End Sub

    Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
        BindData()
    End Sub

    Sub BindData()
        strSql = " Select p.partCode, d.name, isnull(sum(pt.tran_qty),0) From tcpc0.dbo.Parts p " _
               & " Inner Join Part_tran pt ON pt.part_id = p.partID And pt.warehouseID='" & ddlWhplace.SelectedValue & "' And pt.tran_type='R' " _
               & " Left Outer Join departments d On pt.comp_dept_id=d.departmentID " _
               & " Inner Join warehouse w On pt.warehouseID = w.warehouseID " _
               & " Where p.status<>2 And pt.tran_date>='" & txtStartDate.Text.Trim() & "' And pt.tran_date<'" _
               & Convert.ToDateTime(txtEndDate.Text.Trim()).AddDays(1) & "'"

        If txtDeptCode.Text.Trim().Length() > 0 Then
            strSql &= " And d.code=N'" & chk.sqlEncode(txtDeptCode.Text.Trim()) & "'"
        End If
        strSql &= " Group By p.partCode, d.name Order By d.name, p.partCode "

        Session("EXSQL") = strSql
        Session("EXTitle") = "200^<b>部件号</b>~^100^<b>部门</b>~^100^<b>库存数量</b>~^"
        Session("EXHeader") = "开始日期~" & txtStartDate.Text.Trim() & "~^终止日期~" & txtEndDate.Text.Trim() & "~^仓库~" & ddlWhplace.SelectedItem.Text.Trim() & "~^"

        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("total", System.Type.GetType("System.Decimal")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("partcode") = .Rows(i).Item(0).ToString().Trim()
                    drow.Item("department") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("total") = .Rows(i).Item(2)
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



            Try
                dvw.Sort = sortOrder & " " & sortDir
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
        sortOrder = e.SortExpression
        If sortDir = "ASC" Then
            sortDir = "DESC"
        Else
            sortDir = "ASC"
        End If
        BindData()
    End Sub

    Sub Report_click(ByVal sender As Object, ByVal e As System.EventArgs)
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
    End Sub
End Class

End Namespace

