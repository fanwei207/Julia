'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-6-27
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Check Part Warehouse Quantity With Part Transaction
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace tcpc

Partial Class partInventoryCheck
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
    Dim reader As SqlDataReader
    Dim nRet As Integer

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

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
            lblCount.Text = "0"
            whplacedropdownlist()
                If Not Me.Security("20030501").isValid Then
                    BtnADJAll.Enabled = False
                Else
                    BtnADJAll.Enabled = True
                    BtnADJAll.Attributes.Add("onclick", "return confirm('确定要调整" & ddlWhPlace.SelectedItem.Text.Trim() & "的所有库存量吗？');")
                End If
        End If
        BindData()
    End Sub

    Sub BindData()
        Dim dst As DataSet
        Dim numMisMatch As Integer = 0

        strSql = " Select i.code, ic.name, pi.total_qty, isnull(SUM(pt.tran_qty),0), pi.part_id, Isnull(pi.status,0) " _
               & " From Part_inv pi " _
               & " Inner Join tcpc0.dbo.Items i On pi.part_id=i.id " _
               & " Left Outer Join Part_tran pt On pi.part_id=pt.part_id And pi.warehouseID=pt.warehouseID " _
               & " Inner Join tcpc0.dbo.ItemCategory ic On i.category=ic.id " _
               & " Where pi.warehouseID='" & ddlWhplace.SelectedValue & "'" _
               & " Group By i.code, ic.name, pi.total_qty, pi.part_id, Isnull(pi.status,0), Isnull(pt.status,0) " _
               & " Having pi.total_qty <> Sum(Isnull(pt.tran_qty,0)) And Isnull(pi.status,0)=Isnull(pt.status,0) "

        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("part_code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("category", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("total_qty", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("totalqty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("sum_trans_qty", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("sumtransqty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("partID", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dtl.NewRow()
                    dr1.Item("gsort") = i + 1
                    dr1.Item("part_code") = .Rows(i).Item(0).ToString().Trim()
                    dr1.Item("category") = .Rows(i).Item(1).ToString().Trim()
                    dr1.Item("total_qty") = Format(.Rows(i).Item(2), "#,##0.00")
                    dr1.Item("sum_trans_qty") = Format(.Rows(i).Item(3), "#,##0.00")
                    dr1.Item("totalqty") = .Rows(i).Item(2)
                    dr1.Item("sumtransqty") = .Rows(i).Item(3)
                    dr1.Item("partID") = .Rows(i).Item(4).ToString().Trim()
                    dr1.Item("status") = .Rows(i).Item(5).ToString().Trim()
                    dtl.Rows.Add(dr1)
                    numMisMatch = numMisMatch + 1
                Next
                lblCount.Text = numMisMatch.ToString().Trim()
            Else
                ltlAlert.Text = "alert('没有发现" & ddlWhplace.SelectedItem.Text.Trim() & "仓库库存不符！');"
                lblCount.Text = "0"
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)

            Session("orderby") = "gsort"

            Try
                dvw.Sort = Session("orderby") & " " & Session("orderdir")
                dgCheck.DataSource = dvw
                dgCheck.DataBind()
            Catch
            End Try
        End Sub

    Public Sub BtnOK(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgCheck.ItemCommand
        Dim strSql As String
        Dim douProdQty As Double = e.Item.Cells(3).Text
        If e.CommandName.CompareTo("BtnOK") = 0 Then 
                If Not Me.Security("20030501").isValid Then
                    ltlAlert.Text = "alert('没有权限进行库存调整！');"
                    Exit Sub
                End If
            strSql = " Update Part_inv Set total_qty='" & CDbl(e.Item.Cells(4).Text.Trim()) & "'," _
                   & " modifiedBy='" & Session("uID") & "'," _
                   & " modifiedDate='" & DateTime.Now() & "'" _
                   & " Where part_id='" & e.Item.Cells(6).Text.Trim() & "' And warehouseID='" & ddlWhplace.SelectedValue _
                   & "' And Isnull(status,0)='" & e.Item.Cells(7).Text.Trim() & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            ltlAlert.Text = "alert('位于" & ddlWhplace.SelectedItem.Text.Trim() & "仓库的" & e.Item.Cells(1).Text.Trim() & "部件库存更正成功！'); " _
                          & "window.location.href='/Purchase/partInventoryCheck.aspx?pl=" & ddlWhplace.SelectedValue & "&rm=" & DateTime.Now() & Rnd() & "';"
        End If
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
        If Request("pl") <> Nothing Then
            ddlWhplace.SelectedValue = Request("pl")
        End If
        reader.Close()
    End Sub

    Private Sub dgCheck_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCheck.ItemCreated
        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myConfirmButton As TableCell
                myConfirmButton = e.Item.Cells(5)
                'Where 1 is the column containing ButtonColumn
                myConfirmButton.Attributes.Add("onclick", "return confirm('确定要调整该材料的库存吗?');")

            Case ListItemType.AlternatingItem
                Dim myConfirmButton As TableCell
                myConfirmButton = e.Item.Cells(5)
                'Where 1 is the column containing ButtonColumn
                myConfirmButton.Attributes.Add("onclick", "return confirm('确定要调整该材料的库存吗?');")

            Case ListItemType.EditItem
                Dim myConfirmButton As TableCell
                myConfirmButton = e.Item.Cells(5)
                'Where 1 is the column containing ButtonColumn
                myConfirmButton.Attributes.Add("onclick", "return confirm('确定要调整该材料的库存吗?');")
        End Select
    End Sub

    Private Sub BtnADJAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnADJAll.Click
        Dim reader As SqlDataReader 
            If Not Me.Security("20030501").isValid Then
                ltlAlert.Text = "alert('没有权限进行库存调整！');"
                Exit Sub
            End If
        strSql = " Select pi.total_qty, isnull(SUM(pt.tran_qty),0), pi.part_id From Part_inv pi " _
               & " Left Outer Join Part_tran pt On pi.part_id=pt.part_id and pt.warehouseID=pi.warehouseID" _
               & " Where pi.warehouseID='" & ddlWhplace.SelectedValue & "'" _
               & " Group By pi.total_qty, pi.part_id Having pi.total_qty<>SUM(isnull(pt.tran_qty,0)) "
        reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strSql)
        While reader.Read()
            strSql = " Update Part_inv Set total_qty='" & reader(1) & "' Where part_id='" & reader(2) & "' And warehouseID='" & ddlWhplace.SelectedValue & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        End While
        reader.Close()
        ltlAlert.Text = "alert('位于" & ddlWhplace.SelectedItem.Text.Trim() & "仓库的材料库存调整成功！'); " _
                      & "window.location.href='/Purchase/partInventoryCheck.aspx?pl=" & ddlWhplace.SelectedValue & "&rm=" & DateTime.Now() & Rnd() & "';"
    End Sub
End Class

End Namespace

