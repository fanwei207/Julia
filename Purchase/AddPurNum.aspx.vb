Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class AddPurNum
        Inherits BasePage
    Dim chk As New adamClass
    Dim strSQL As String
    Dim reader As SqlDataReader
    Dim reader1 As SqlDataReader
    Dim ds As DataSet
    Dim rplan_qty As Integer
    Dim tplan_qty As Integer
    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal
    Protected WithEvents ship_qty As System.Web.UI.WebControls.Label

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
        If Not IsPostBack Then 
                If Not Me.Security("30010105").isValid Then
                    ltlAlert.Text = "alert('请确认有该项目的编辑权限！');"
                    Exit Sub
                End If
            BindItem()
            BindData()
        End If
    End Sub
    Sub BindData()
        strSQL = "SELECT id,dog_partin_id, plan_qty, isnull(real_qty,0) as real_qty,plan_date, notes FROM Dog_PartIn_Detail where dog_partin_id='" & Request("id") & "' order by plan_date"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("did", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("plan_qty", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("rplan_qty", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("real_qty", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("plan_date", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("notes", System.Type.GetType("System.String")))
        With ds.Tables(0)
            Dim i As Integer
            Dim dr As DataRow
            If .Rows.Count > 0 Then
                For i = 0 To .Rows.Count - 1
                    dr = dt.NewRow()
                    dr.Item("id") = .Rows(i).Item("id").ToString.Trim()
                    dr.Item("did") = .Rows(i).Item("dog_partin_id").ToString.Trim()
                    dr.Item("plan_qty") = .Rows(i).Item("plan_qty")
                    dr.Item("real_qty") = .Rows(i).Item("real_qty")
                    dr.Item("plan_date") = Format(.Rows(i).Item("plan_date"), "yyyy-MM-dd")
                    dr.Item("notes") = .Rows(i).Item("notes").ToString.Trim()
                    'write adjust number
                    rplan_qty = 0
                    tplan_qty = 0
                    strSQL = "select isnull(sum(plan_qty),0),isnull(sum(real_qty),0) from dog_partin_detail where dog_partin_id='" & Request("id") & "' and plan_date<'" & .Rows(i).Item("plan_date") & "'"
                    reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                    If reader1.HasRows Then
                        While reader1.Read()
                            tplan_qty = CInt(reader1(0))
                            rplan_qty = tplan_qty - CInt(reader1(1))
                        End While
                    End If
                    rplan_qty = rplan_qty + CInt(.Rows(i).Item("plan_qty"))
                    dr.Item("rplan_qty") = rplan_qty
                    reader1.Close()
                    dt.Rows.Add(dr)
                Next
            End If
        End With
        Dim dv As New DataView(dt)
        dgpur.DataSource = dv
        dgpur.DataBind()

    End Sub
    Sub BindItem()
        strSQL = " select po.first_deliver_date,isnull(dp.rate,1) as rate,po.order_code,i.code,ii.code as pur_code,pod.order_qty,c.company_code,pod.deliver_date,pod.deliver_date_end,dp.prod_qty" _
               & " from Dog_PartIn dp inner join product_order_detail pod on dp.prod_order_detail_id=pod.prod_order_detail_id " _
               & " inner join product_orders po on po.prod_order_id=pod.prod_order_Id " _
               & " inner join tcpc0.dbo.items i on i.id=pod.prod_id " _
               & " inner join tcpc0.dbo.items ii on ii.id=dp.prod_id" _
               & " inner join tcpc0.dbo.companies c on c.company_id=po.company_id " _
               & " where dp.id='" & CInt(Request("id")) & "' "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
        While reader.Read
            order_code.Text = reader("order_code")
            code.Text = reader("code")
            pur_code.Text = reader("pur_code")
            prod_qty.Text = Format(reader("order_qty"), "###0.00")
            If CDate(Format(reader("deliver_date"), "yyyy-MM-dd")) = CDate("1900.1.1") Then
                ship_date.Text = ""
            Else
                ship_date.Text = Format(reader("deliver_date"), "yyyy-MM-dd")
            End If
            If CDate(Format(reader("deliver_date_end"), "yyyy-MM-dd")) = CDate("1900.1.1") Then
                ship_date_end.Text = ""
            Else
                ship_date_end.Text = Format(reader("deliver_date_end"), "yyyy-MM-dd")
            End If
            company_code.Text = reader("company_code")
            pur_code_qty.Text = Format(reader("prod_qty") * reader("rate"), "###0.00")
        End While
        reader.Close()
    End Sub

    Private Sub addnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addnew.Click
        ltlAlert.Text = "openHR('AddPlanNum.aspx?id=" & Request("id") & "');"
        '  ltlAlert.Text = "var w=window.open('AddPlanNum.aspx?id=" & Request("id") & "','AddPlanNum','menubar=0,scrollbars=0,resizable=1,width=400,height=300,top=100,left=40');w.focus(); "
    End Sub

    Private Sub dgpur_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgpur.UpdateCommand
        Dim reader As SqlDataReader
        Dim Nplan_qty As Integer
        Dim Nreal_qty As Integer
        Dim Nprod_qty As Integer
        Dim rate As Integer
        If Not IsNumeric(CType(e.Item.Cells(3).Controls(0), TextBox).Text.Trim()) Then
            ltlAlert.Text = "alert('输入的计划数不为数字！');"
            Exit Sub
        End If
        If Not IsNumeric(CType(e.Item.Cells(5).Controls(0), TextBox).Text.Trim()) Then
            ltlAlert.Text = "alert('输入的应到数不为数字！');"
            Exit Sub
        End If
        strSQL = "select isnull(id,0) from dog_partin_detail where id='" & e.Item.Cells(0).Text.Trim() & "' and plan_qty='" & CType(e.Item.Cells(3).Controls(0), TextBox).Text.Trim() & "' " _
                & " and real_qty='" & CType(e.Item.Cells(5).Controls(0), TextBox).Text.Trim() & "' and notes='" & CType(e.Item.Cells(8).Controls(0), TextBox).Text.Trim() & "'"
        If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) Then
            'ltlAlert.Text = "alert('未做任何更改');"
            Exit Sub
        End If
        strSQL = "SELECT isnull(SUM(dpd.plan_qty),0), isnull(SUM(dpd.real_qty),0) FROM Dog_PartIn_Detail dpd inner join Dog_PartIn dp on dp.id=dpd.dog_partin_id WHERE dpd.dog_partin_id='" & Request("id") & "' and dpd.id<>'" & e.Item.Cells(0).Text.Trim() & "'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
        While reader.Read()
            Nplan_qty = reader(0)
            Nreal_qty = reader(1)
        End While
        reader.Close()
        strSQL = "select prod_qty,isnull(rate,1) from dog_partin where id='" & Request("id") & "'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
        While reader.Read()
            Nprod_qty = reader(0)
            rate = reader(1)
        End While
        reader.Close()
        'If CType(e.Item.Cells(2).Controls(0), TextBox).Text.Trim.Length > 0 Then
        '    If Not IsDate(CType(e.Item.Cells(2).Controls(0), TextBox).Text.Trim()) Then
        '        ltlAlert.Text = "alert('请按照正确日期格式输入！');"
        '        Exit Sub
        '    End If
        'Else
        '    ltlAlert.Text = "alert('请输入计划日期！');"
        'End If
        If CType(e.Item.Cells(3).Controls(0), TextBox).Text.Trim.Length > 0 Then
            If Not IsNumeric(CType(e.Item.Cells(3).Controls(0), TextBox).Text.Trim()) Then
                ltlAlert.Text = "alert('请输入正确地的计划数！')"
                Exit Sub
            ElseIf (Nplan_qty + CInt(CType(e.Item.Cells(3).Controls(0), TextBox).Text.Trim())) > (Nprod_qty * rate) Then
                ltlAlert.Text = "alert('请输入的总计划数大于定购数！')"
                Exit Sub
            End If
        Else
            ltlAlert.Text = "alert('请输入计划数！')"
        End If
        If CType(e.Item.Cells(5).Controls(0), TextBox).Text.Trim.Length > 0 Then
            If Not IsNumeric(CType(e.Item.Cells(5).Controls(0), TextBox).Text.Trim()) Then
                ltlAlert.Text = "alert('请输入正确地的应到数！')"
                Exit Sub
            ElseIf (Nreal_qty + CInt(CType(e.Item.Cells(5).Controls(0), TextBox).Text.Trim())) > (Nprod_qty * rate) Then
                ltlAlert.Text = "alert('请输入的总应到数大于定购数！')"
                Exit Sub
            End If
        End If
        Dim param(5) As SqlParameter
        param(0) = New SqlParameter("@plan_qty", Convert.ToInt32(CType(e.Item.Cells(3).Controls(0), TextBox).Text.Trim()))
        param(1) = New SqlParameter("@real_qty", CType(e.Item.Cells(5).Controls(0), TextBox).Text.Trim())
        param(2) = New SqlParameter("@plan_date", Convert.ToDateTime(e.Item.Cells(2).Text))
        param(3) = New SqlParameter("@notes", CType(e.Item.Cells(8).Controls(0), TextBox).Text.Trim())
        param(4) = New SqlParameter("@user", Session("uID"))
        param(5) = New SqlParameter("@id", Convert.ToInt32(e.Item.Cells(0).Text))
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, "Purchase_update_qty", param)
        ltlAlert.Text = "alert(修改成功！);"
        dgpur.EditItemIndex = -1
        BindData()
    End Sub

    Private Sub dgpur_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgpur.ItemCommand
        Dim id As String = e.Item.Cells(0).Text()
        Dim did As String = e.Item.Cells(1).Text() 
        If e.CommandName.CompareTo("EditBtn") = 0 Then 
                If Not Me.Security("30010105").isValid Then
                    ltlAlert.Text = "alert('请确认有删除该项目的权限！');"
                    Exit Sub
                End If

        ElseIf e.CommandName.CompareTo("DeleteBtn") = 0 Then 
                If Not Me.Security("30010102").isValid Then
                    ltlAlert.Text = "alert('请确认有删除该项目的权限！');"
                    Exit Sub
                End If
            Dim param(1) As SqlParameter
            param(1) = New SqlParameter("@user", Session("uID"))
            param(0) = New SqlParameter("@id", id)
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, "Purchase_delete_qty", param)
            ltlAlert.Text = "window.location.href='/Purchase/AddPurNum.aspx?id=" & e.Item.Cells(1).Text & "&rm=" & DateTime.Now() & Rnd() & "';"
        End If
    End Sub
    Private Sub dgpur_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgpur.ItemCreated
        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing ButtonColumn
                myDeleteButton = e.Item.Cells(7)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")

            Case ListItemType.AlternatingItem
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing your ButtonColumn
                myDeleteButton = e.Item.Cells(7)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")
            Case ListItemType.EditItem
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing your ButtonColumn
                myDeleteButton = e.Item.Cells(7)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")
                'CType(e.Item.Cells(2).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(90)
                CType(e.Item.Cells(3).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(90)
                CType(e.Item.Cells(5).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(90)
                CType(e.Item.Cells(8).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(300)
        End Select
    End Sub

    Private Sub dgpur_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgpur.CancelCommand
        dgpur.EditItemIndex = -1
        BindData()
    End Sub

    Private Sub dgpur_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgpur.EditCommand
        dgpur.EditItemIndex = e.Item.ItemIndex
        BindData()
    End Sub

    Private Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        Response.Redirect("/Purchase/purchasenum.aspx?order_code=" & Request("order_code") & "&code=" & Request("code") & "")
    End Sub
End Class

End Namespace
