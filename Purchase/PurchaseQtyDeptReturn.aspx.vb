'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-7-15
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Department Return Parts Qty to Inventory
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
Partial Class PurchaseQtyDeptReturn
        Inherits BasePage

    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
    Dim reader As SqlDataReader
    Public chk As New adamClass
    Dim ds As DataSet

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents partDesc As System.Web.UI.WebControls.Label
    Protected WithEvents partCata As System.Web.UI.WebControls.Label
    Protected WithEvents DepartmentName As System.Web.UI.WebControls.Label
    Protected WithEvents txtOrderCode As System.Web.UI.WebControls.TextBox


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
            warehousedropdownlist()
            statusdrop()
            txtEnterDate.Text = Format(DateTime.Now, "yyyy-MM-dd")
            ltlAlert.Text = "Form1.warehouse.focus();"
        End If
    End Sub

    Sub warehousedropdownlist()
        warehouse.Items.Add(New ListItem("--", "0"))
        strSql = " Select w.warehouseID, w.name From warehouse w "
        If Session("uRole") <> 1 Then
            strSql &= " Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "'"
        End If
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read
            Dim tempListItem As New ListItem
            tempListItem.Value = reader(0)
            tempListItem.Text = reader(1)
            warehouse.Items.Add(tempListItem)
        End While
        reader.Close()
    End Sub

    Sub statusdrop()
        status.Items.Add(New ListItem("--", "0"))
        strSql = " Select id, StatusName From tcpc0.dbo.Status "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read
            Dim tempListItem As New ListItem
            tempListItem.Value = reader(0)
            tempListItem.Text = reader(1)
            status.Items.Add(tempListItem)
        End While
        reader.Close()
    End Sub

    ' dispaly the parts describtion,category and partID from PartCode which user inputed
    Sub txtPartDescBinding(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim existflag As Boolean = False
        strSql = " Select i.id, i.description, ic.name From Items i " _
               & " Inner Join ItemCategory ic On ic.id=i.category And i.type<>2 " _
               & " Where i.status<>2 And i.code=N'" & chk.sqlEncode(partCode.Text.Trim()) & "'"
        reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSql)
        While reader.Read
            existflag = True
            lblPartCat.Text = UCase(reader(2))
            lblPartDesc.Text = UCase(reader(1))
            partID.Text = reader(0)
            ltlAlert.Text = " Form1.CompanyCode.focus();"
        End While
        reader.Close()
        If existflag = False Then
            If partCode.Text.Trim() <> "" Then
                ltlAlert.Text = "alert('材料不存在或者已停用！');Form1.partCode.focus();"
                partCode.Text = ""
                lblPartCat.Text = ""
                lblPartDesc.Text = ""
                partID.Text = ""
            End If
        Else
            partCode.Text = UCase(partCode.Text.Trim())
            ltlAlert.Text = "Form1.DepartmentCode.focus();"
        End If
    End Sub

    ' display the department name from department Code which user inputed
    Sub txtDeptCodeInputed(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exist As Boolean = False
        strSql = " Select departmentID,name From departments where active=1 and code='" & chk.sqlEncode(DepartmentCode.Text.Trim()) & "' "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read
            exist = True
            lblDeptName.Text = UCase(reader(1))
            DepartmentID.Text = reader(0)
            ltlAlert.Text = " Form1.txtEnterDate.focus();"
        End While
        reader.Close()

        If exist = False Then
            If DepartmentCode.Text.Trim() <> "" Then
                ltlAlert.Text = "alert('部门不存在！');Form1.CompanyCode.focus();"
                DepartmentCode.Text = ""
                DepartmentID.Text = ""
                lblDeptName.Text = ""
            End If
        Else
            DepartmentCode.Text = UCase(DepartmentCode.Text.Trim())
            ltlAlert.Text = "Form1.txtPlanCode.focus();"
        End If
    End Sub

    '' check plan Code which user inputed
    'Public Sub txtPlanCodeInputed(ByVal sender As Object, ByVal e As System.EventArgs)
    '    strSql = " Select pop.prod_order_plan_id " _
    '          & " From Product_order_plan pop " _
    '          & " Where pop.prod_order_plan_code=N'" & chk.sqlEncode(txtPlanCode.Text.Trim()) & "' And pop.status<>1 "
    '    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
    '    If reader.Read Then
    '        If Not IsDBNull(reader(0)) Then
    '            lblPlanID.Text = reader(0).ToString()
    '            ltlAlert.Text = "Form1.txtEnterDate.focus();"
    '        Else
    '            ltlAlert.Text = " alert('计划单号" & txtPlanCode.Text & "并不存在或已关闭！');Form1.txtPlanCode.focus();"
    '            txtPlanCode.Text = ""
    '            lblPlanID.Text = ""
    '        End If
    '    Else
    '        ltlAlert.Text = " alert('计划单号" & txtPlanCode.Text & "并不存在或已关闭！');Form1.txtPlanCode.focus();"
    '        txtPlanCode.Text = ""
    '        lblPlanID.Text = ""
    '    End If
    '    reader.Close()
    'End Sub

    ' Save the datas about parts
    Sub addRecord(ByVal sender As Object, ByVal e As System.EventArgs)
        If warehouse.SelectedValue = "0" Then
            ltlAlert.Text = "alert('仓库名称不能为空！');"
            Exit Sub
        ElseIf DepartmentCode.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('部门代码不能为空！');"
            Exit Sub
        ElseIf txtEnterDate.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('输入的日期不能为空！');"
            Exit Sub
        ElseIf IsDate(txtEnterDate.Text.Trim()) = False Then
            ltlAlert.Text = "alert('输入的日期非法！');"
            Exit Sub
        ElseIf DateDiff("d", DateTime.Now, txtEnterDate.Text.Trim()) > 2 Then
            ltlAlert.Text = "alert('输入的日期不能比当前日期晚2天以上');"
            Exit Sub
        ElseIf txtPartQty.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('退库数量不能为空！');"
            Exit Sub
        ElseIf IsNumeric(txtPartQty.Text.Trim()) = False Then
            ltlAlert.Text = "alert('退库数量不是数字！');"
            Exit Sub
        ElseIf Val(txtPartQty.Text.Trim()) <= 0 Then
            ltlAlert.Text = "alert('退库数量不能小于等于零！');"
            Exit Sub
        ElseIf txtPlanCode.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('计划单号不能为空！');"
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

        If txtPlanCode.Text.Trim().Length > 0 Then
            strSql = " Select pop.prod_order_plan_id " _
                   & " From Product_order_plan pop " _
                   & " Where pop.prod_order_plan_code=N'" & chk.sqlEncode(txtPlanCode.Text.Trim()) & "' And pop.status<>1 "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            If reader.Read Then
                If Not IsDBNull(reader(0)) Then
                    lblPlanID.Text = reader(0).ToString()
                Else
                    ltlAlert.Text = " alert('计划单号" & txtPlanCode.Text & "并不存在或已关闭！');Form1.txtPlanCode.focus();"
                    txtPlanCode.Text = ""
                    lblPlanID.Text = ""
                    Exit Sub
                End If
            Else
                ltlAlert.Text = " alert('计划单号" & txtPlanCode.Text & "并不存在或已关闭！');Form1.txtPlanCode.focus();"
                txtPlanCode.Text = ""
                lblPlanID.Text = ""
                Exit Sub
            End If
            reader.Close()
        Else
            lblPlanID.Text = "0"
        End If

        strSql = "Purchase_PartReturnIn"
        Dim ret As Integer
        Dim params(8) As SqlParameter
        params(0) = New SqlParameter("@whid", Convert.ToInt32(warehouse.SelectedValue))
        params(1) = New SqlParameter("@partid", Convert.ToInt32(partID.Text.Trim))
        params(2) = New SqlParameter("@deptid", Convert.ToInt32(DepartmentID.Text.Trim))
        params(3) = New SqlParameter("@outQty", Convert.ToDecimal(txtPartQty.Text.Trim))
        params(4) = New SqlParameter("@outDate", Convert.ToDateTime(txtEnterDate.Text.Trim))
        params(5) = New SqlParameter("@planid", Convert.ToInt32(lblPlanID.Text))
        params(6) = New SqlParameter("@notes", chk.sqlEncode(txtNotes.Text.Trim))
        params(7) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
        params(8) = New SqlParameter("@status", Convert.ToInt32(status.SelectedValue))

        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSql, params)
        If ret < 0 Then
            strSql = " Select top 1 ErrorInfo From PartQtyImportError where userID=" & Session("uID")
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            If reader.Read() Then
                ltlAlert.Text = "alert('" & reader(0) & "'); Form1.txtOrderCode.focus();"
                reader.Close()
                Exit Sub
            End If
            reader.Close()
            ltlAlert.Text = "alert('材料部门退库失败！');"
        Else
            Response.Redirect("/Purchase/PurchaseQtyDeptReturn.aspx?rm=" & DateTime.Now() & Rnd(), True)
        End If
    End Sub

End Class

End Namespace
