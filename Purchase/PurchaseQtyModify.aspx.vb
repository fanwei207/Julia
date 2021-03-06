'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-03-02
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   View Part Warehouse In/Out Detail 
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace tcpc

Partial Class PurchaseQtyModify
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim strsql As String
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
        Dim reader As SqlDataReader

        If Not Page.IsPostBack Then
            Dim cnt As Integer = -1 
                If Me.Security("20030501").isValid Then
                    ltlAlert.Text = "alert('没有权限进行库存调整！'); window.close();"
                End If
            lblTitle.Text = Server.UrlDecode(Request("place")) & "仓库的" & Server.UrlDecode(Request("partcode")) & "/" & Server.UrlDecode(Request("partdesc"))
            If Request("type") = "I" Or Request("type") = "RS" Then
                lblCompDept.Text = "供应商编号："
                txtCompDept.ReadOnly = True
            Else
                lblCompDept.Text = "部门编号："
                txtCompDept.ReadOnly = False
            End If
            strsql = " Select pt.tran_date, Case When pt.tran_type='I' Then abs(pt.tran_qty)/Isnull(i.tranRate,1) When pt.tran_type='RS' Then abs(pt.tran_qty)*Isnull(i.tranRate,1) Else abs(pt.tran_qty) End, " _
                   & " pt.tran_type, Case pt.tran_type When 'I' Then Isnull(c.company_code,'') When 'RS' Then Isnull(c.company_code,'') Else Isnull(d.code,'') End, " _
                   & " Isnull(pop.prod_order_plan_code,''), Isnull(po.part_order_code,''), Isnull(pt.deliverycode,''), Isnull(pt.notes,''), " _
                   & " Isnull(pt.comp_dept_id,0), Isnull(pt.order_id,0), Isnull(pt.mrp_id,0) " _
                   & " From Part_tran pt " _
                   & " Inner Join tcpc0.dbo.items i On i.id=pt.part_id " _
                   & " Left Outer Join part_orders po On pt.order_id=po.part_order_id " _
                   & " Left Outer Join tcpc0.dbo.Companies c On pt.comp_dept_id=c.company_id And c.active=1 " _
                   & " Left Outer Join departments d On pt.comp_dept_id=d.departmentID And d.active=1 " _
                   & " Left Outer Join Product_order_plan_mrp popm On pt.mrp_id=popm.mrp_id " _
                   & " Left Outer Join Product_order_plan pop On popm.prod_order_plan_id=pop.prod_order_plan_id " _
                   & " Where pt.part_tran_id='" & Request("ID") & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strsql)

            While reader.Read()
                txtEnterDate.Text = reader(0)
                txtPartQty.Text = reader(1)
                txtType.Text = reader(2)
                txtCompDept.Text = reader(3)
                txtOrder.Text = reader(5)
                txtPlan.Text = reader(4)
                txtDelivery.Text = reader(6)
                txtNotes.Text = reader(7)
                lblCompDeptID.Text = reader(8)
                lblQty.Text = reader(1)
                lblOrderID.Text = reader(9)
                lblPlanID.Text = reader(10)
            End While
            ltlAlert.Text = "Form1.txtEnterDate.focus();"
        End If
    End Sub

    Private Sub BtnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Dim compdeptID As String = Nothing
        Dim cmd As SqlCommand
        Dim con As SqlConnection = New SqlConnection(chk.dsnx)
        Dim param As New SqlParameter
        Dim retValue As Integer

        If txtCompDept.ReadOnly = False Then
            If txtCompDept.Text.Trim().Length > 0 Then
                strsql = " Select departmentID From departments where active=1 and code='" & chk.sqlEncode(txtCompDept.Text.Trim()) & "'"
                compdeptID = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strsql)
                If compdeptID = Nothing Then
                    ltlAlert.Text = "alert('" & txtCompDept.Text.Trim() & "部门编号不存在！');"
                    Exit Sub
                Else
                    lblCompDeptID.Text = compdeptID.Trim()
                End If
            End If
        End If

        If txtEnterDate.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('输入的日期不能为空！');"
            Exit Sub
        ElseIf IsDate(txtEnterDate.Text.Trim()) = False Then
            ltlAlert.Text = "alert('输入的日期非法！');"
            Exit Sub
        ElseIf DateDiff("d", DateTime.Now, txtEnterDate.Text.Trim()) > 2 Then
            ltlAlert.Text = "alert('输入的日期不能比当前日期晚2天以上');"
            Exit Sub
        ElseIf txtPartQty.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('数量不能为空！');"
            Exit Sub
        ElseIf IsNumeric(txtPartQty.Text.Trim()) = False Then
            ltlAlert.Text = "alert('数量不是数字！');"
            Exit Sub
        ElseIf Val(txtPartQty.Text.Trim()) <= 0 Then
            ltlAlert.Text = "alert('数量不能小于等于零！');"
            Exit Sub
        End If

            If Not Me.Security("20030900").isValid Then
                Dim strDate, stryear, strmonth As String
                If Day(DateTime.Now) >= 26 Then
                    strDate = CDate(CStr(Year(Now)) + "-" + CStr(Month(Now)) + "-" + "21")
                    If DateTime.Compare(strDate, txtEnterDate.Text) < 0 Then
                        txtEnterDate.Text = ""
                        ltlAlert.Text = "alert('只能输入当前月21日以后的日期'); Form1.txtEnterDate.focus();"
                        Exit Sub
                    End If
                Else
                    If Month(Now) = 1 Then
                        strmonth = "12"
                        stryear = CStr(Year(Now) - 1)
                    Else
                        strmonth = CStr(Month(Now) - 1)
                        stryear = CStr(Year(Now))
                    End If
                    strDate = CDate(stryear + "-" + strmonth + "-" + "21")
                    If DateTime.Compare(strDate, txtEnterDate.Text) < 0 Then
                        txtEnterDate.Text = ""
                        ltlAlert.Text = "alert('只能输入上个月21日以后当前月20日已前的日期');Form1.txtEnterDate.focus();"
                        Exit Sub
                    End If
                End If
            End If

        If txtType.Text = "I" Then
            If txtOrder.Text.Trim().Length > 0 Then
                strsql = " Select Sum(pod.order_qty*Isnull(i.tranRate,1)) From part_orders po Inner Join Part_order_detail pod ON po.part_order_id = pod.part_order_id " _
                       & " Inner Join tcpc0.dbo.Items i On i.id=pod.part_id " _
                       & " Where pod.part_id='" & Request("pid") & "'  And po.part_order_code='" & chk.sqlEncode(txtOrder.Text.Trim()) & "'"
                If lblCompDeptID.Text.Trim() <> "0" Then
                    strsql &= " And po.supplier_id='" & lblCompDeptID.Text.Trim() & "'"
                End If
                Dim numOrderQty As Decimal = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strsql)

                strsql = " Select Isnull(Sum(pt.tran_qty),0) From Part_tran pt Inner Join part_orders po ON pt.order_id=po.part_order_id " _
                       & " Where pt.part_id='" & Request("pid") & "' And po.part_order_code='" & chk.sqlEncode(txtOrder.Text.Trim()) & "'"
                Dim numQty As Double = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strsql)

                If CDbl(txtPartQty.Text.Trim()) > numOrderQty - numQty + CDbl(lblQty.Text.Trim()) Then
                    ltlAlert.Text = "alert('入库数量已超过定单数量,现允许入库数量为" & numOrderQty - numQty + lblQty.Text.Trim() & "');"
                    Exit Sub
                End If
            End If
        End If

        'check repeat record
        strsql = " Select part_tran_id From Part_tran Where part_id='" & Request("pid") & "' And abs(tran_qty)='" & CDbl(txtPartQty.Text.Trim()) _
               & "' And tran_type='" & txtType.Text.Trim() & "' And tran_date='" & txtEnterDate.Text.Trim() & "'And warehouseID='" & Request("placeID") _
               & "' And notes=N'" & chk.sqlEncode(txtNotes.Text.Trim()) & "'"
        If lblCompDeptID.Text.Trim() <> "0" Then
            strsql &= " And comp_dept_id='" & lblCompDeptID.Text.Trim() & "' "
        End If
        If lblOrderID.Text <> "0" Then
            strsql &= " And order_id='" & lblOrderID.Text.Trim() & "'"
        End If
        If lblPlanID.Text.Trim() <> "0" Then
            strsql &= " And mrp_id='" & lblPlanID.Text.Trim() & "'"
        End If
        If txtDelivery.Text.Trim().Length > 0 Then
            strsql &= " And deliverycode='" & txtDelivery.Text.Trim() & "'"
        End If
        If SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strsql) > 0 Then
            ltlAlert.Text = "alert('相同的记录已经存在！');"
            Exit Sub
        End If

        strsql = " Delete From PartQtyImportTemp Where userID='" & Session("uID") & "' And plantID='" & Session("plantcode") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql)

        strsql = " Insert Into PartQtyImportTemp(type, partID, comp_dept_ID, enterdate, enterqty, placeID, userID, " _
               & " plantID, planID, orderID, notes, tranid, status) " _
               & " Values('" & txtType.Text.Trim() & "','" & Request("pid") & "','" & lblCompDeptID.Text.Trim() & "','" & txtEnterDate.Text.Trim() _
               & "','" & txtPartQty.Text.Trim() & "','" & Request("placeID") & "','" & Session("uID") & "','" & Session("plantcode") & "','" _
               & lblPlanID.Text.Trim() & "','" & lblOrderID.Text.Trim() & "',N'" & chk.sqlEncode(txtNotes.Text.Trim()) & "','" _
               & Request("ID") & "','" & Request("st") & "')"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql)

        'process part qty modify
        cmd = New SqlCommand("Purchase_PartTranModify", con)
        con.Open()
        cmd.CommandType = CommandType.StoredProcedure

        param = cmd.Parameters.Add("@intUserID", SqlDbType.Int)
        param.Direction = ParameterDirection.Input
        param.Value = Session("uID")

        param = cmd.Parameters.Add("@plantID", SqlDbType.Int)
        param.Direction = ParameterDirection.Input
        param.Value = Session("plantcode")

        Try
            param = cmd.Parameters.Add("param", SqlDbType.Int)
            param.Direction = ParameterDirection.ReturnValue

            cmd.ExecuteReader()
            retValue = param.Value
            con.Close()
        Catch
        End Try

        If retValue = 0 Then
            ltlAlert.Text = "alert('材料修改成功！');window.close();window.opener.location.href='/Purchase/PurchaseQtyDetail.aspx?partID=" _
                          & Request("partID") & "&fr=" & Request("fr") & "&to=" & Request("to") & "&code=" & Request("code") & "&cat=" _
                          & Request("cat") & "&placeID=" & Request("placeID") & "&partcode=" & Server.UrlEncode(Request("partcode")) & "&ty=" & Request("ty") _
                          & "&partdesc=" & Server.UrlEncode(Request("partdesc")) & "&place=" & Server.UrlEncode(Request("place")) _
                          & "&st=" & Request("st") & "&rm=" & DateTime.Now() & Rnd() & "'; "
        Else
            ltlAlert.Text = "alert('材料修改失败！');window.close();"
        End If
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        ltlAlert.Text = "window.close();"
    End Sub
End Class

End Namespace
