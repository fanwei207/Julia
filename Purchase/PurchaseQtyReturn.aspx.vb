'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-7-15
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Return Parts Qty to Inventory
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class PurchaseQtyReturn
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
    Sub txtProdDescBinding(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim existflag As Boolean = False
        strSql = " Select p.partID, p.partDesc, pc.name From Parts p " _
               & " Inner Join Part_category pc On pc.part_cat_id=p.categoryID " _
               & " Where partCode=N'" & chk.sqlEncode(partCode.Text.Trim()) & "'"
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

    Sub txtDeptCodeInputed(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exist As Boolean = False
        strSql = " Select departmentID,name From departments where active='1' and code='" & chk.sqlEncode(DepartmentCode.Text.Trim()) & "' "
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
            ltlAlert.Text = "Form1.txtEnterDate.focus();"
        End If
    End Sub

    ' Save the datas about parts
    Sub addRecord(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmd As SqlCommand
        Dim con As SqlConnection = New SqlConnection(chk.dsnx)
        Dim param As New SqlParameter
        Dim strEnterDate As String = txtEnterDate.Text.Trim()
        Dim retValue As Integer = -1

        If warehouse.SelectedValue = "0" Then
            ltlAlert.Text = "alert('仓库名称不能为空！');"
            Exit Sub
        ElseIf DepartmentCode.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('部门代码不能为空！');"
            Exit Sub
        ElseIf txtEnterDate.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('退库日期不能为空！');"
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
        End If

        strSql = " Delete From PartQtyImportTemp Where userID='" & Session("uID") & "' And plantID='" & Session("plantcode") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

        strSql = " Insert Into PartQtyImportTemp(type, partID, comp_dept_ID, enterdate, enterqty, placeID, userID, plantID, status) " _
               & " Values('R','" & partID.Text.Trim() & "','" & DepartmentID.Text.Trim() & "','" & strEnterDate.Trim() & "','" & txtPartQty.Text.Trim() _
               & "','" & warehouse.SelectedValue & "','" & Session("uID") & "','" & Session("plantcode") & "','" & status.SelectedValue & "')"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

        'process part qty increase
        cmd = New SqlCommand("Purchase_PartQtyChangeImport", con)
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
            ltlAlert.Text = "alert('材料退库成功！');window.location.href='/Purchase/PurchaseQtyReturn.aspx?rm=" & DateTime.Now() & Rnd() & "';"
        Else
            ltlAlert.Text = "alert('材料退库失败！');window.location.href='/Purchase/PurchaseQtyReturn.aspx?rm=" & DateTime.Now() & Rnd() & "';"
        End If
    End Sub

End Class

End Namespace
