'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-03-10
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   View Part Warehouse In Detail for Finance Import 
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace tcpc

Partial Class ExportForFinance
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
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
            warehouseDropDownList()
            BindData()
            ltlAlert.Text = "Form1.txtStartDate.focus();"
        End If
    End Sub

    Sub BindData()
        Dim dst As DataSet

        Session("EXTitle") = "150^<b>定单号</b>~^150^<b>材料编号</b>~^150^<b>收料单号</b>~^150^<b>送货单号</b>~^150^<b>送货日期</b>~^100^<b>数量</b>~^150^<b>供应商</b>~^150^<b>仓库</b>~^300^<b>备注</b>~^"
        Session("EXHeader") = "起始日期~" & txtStartDate.Text.Trim() & "~^结束日期~" & txtEndDate.Text.Trim() & "~^"

        strSql = " Select pt.part_tran_id, Isnull(po.part_order_code,''), i.code, Isnull(pt.receivecode,''), '', " _
               & " '',  '', c.company_code, w.name,isnull(pt.notes,'') " _
               & " From Part_tran pt " _
               & " Inner Join tcpc0.dbo.Items i On i.id=pt.part_id And i.status<>2 And i.type<>2 " _
               & " Inner Join warehouse w On pt.warehouseID=w.warehouseID " _
               & " Inner Join part_orders po On pt.order_id=po.part_order_id " _
               & " Inner Join tcpc0.dbo.Companies c On pt.comp_dept_id=c.company_id And c.active=1 " _
               & " Left Outer Join Product_order_plan_mrp popm On pt.mrp_id=popm.mrp_id " _
               & " Left Outer Join Product_order_plan pop On popm.prod_order_plan_id=pop.prod_order_plan_id " _
               & " Where pt.tran_type='I' "
        If warehouseDDL.SelectedIndex <> 0 Then
            strSql &= " And pt.warehouseID='" & warehouseDDL.SelectedValue & "'"
        End If
        If txtStartDate.Text.Trim() <> "" Then
            strSql &= " And pt.tran_date>='" & txtStartDate.Text.Trim() & "'"
        End If
        If txtEndDate.Text.Trim() <> "" Then
            strSql &= " And pt.tran_date<'" & Convert.ToDateTime(txtEndDate.Text.Trim()).AddDays(1) & "'"
        End If
        strSql &= " Order By po.part_order_code, i.code "
        Session("EXSQL") = strSql

        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("company", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("order", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("receive", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("notes", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("gsort") = i + 1
                    drow.Item("order") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("partcode") = .Rows(i).Item(2).ToString().Trim()
                    drow.Item("receive") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("company") = .Rows(i).Item(7).ToString().Trim()
                    drow.Item("notes") = .Rows(i).Item(9).ToString().Trim()
                    dtl.Rows.Add(drow)
                Next
            Else
                Session("EXSQL") = Nothing
                Session("EXHeader") = ""
                Session("EXTitle") = Nothing
            End If
        End With

        Dim dvw As DataView
        dvw = New DataView(dtl)

            Try
                dgTrans.DataSource = dvw
                dgTrans.DataBind()
            Catch
            End Try
        End Sub

    'warehouse dropdown bind Value
    Sub warehouseDropDownList()
        Dim reader As SqlDataReader
        warehouseDDL.Items.Add(New ListItem("--", "0"))
        strSql = " Select w.warehouseID, w.name From warehouse w "
        If Session("uRole") <> 1 Then
            strSql &= " Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "'"
        End If
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read
            Dim tempListItem As New ListItem
            tempListItem.Value = reader(0)
            tempListItem.Text = reader(1)
            warehouseDDL.Items.Add(tempListItem)
        End While
        reader.Close()
    End Sub

    Private Sub dgTrans_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTrans.PageIndexChanged
        dgTrans.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

        Public Sub ReportClick(ByVal sender As Object, ByVal e As System.EventArgs)

            If txtStartDate.Text.Length > 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(txtStartDate.Text)
                Catch ex As Exception
                    ltlAlert.Text = "alert('日期 格式不正确！');"
                    Exit Sub
                End Try 
            End If

            If txtEndDate.Text.Length > 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(txtEndDate.Text)
                Catch ex As Exception
                    ltlAlert.Text = "alert('日期 格式不正确！');"
                    Exit Sub
                End Try
            End If

            dgTrans.CurrentPageIndex = 0
            BindData()
        End Sub

    Private Sub warehouseDDL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles warehouseDDL.SelectedIndexChanged
        dgTrans.CurrentPageIndex = 0
        BindData()
    End Sub
End Class

End Namespace
