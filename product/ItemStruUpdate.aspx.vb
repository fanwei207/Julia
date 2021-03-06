Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class ItemStruUpdate
        Inherits BasePage
    Public chk As New adamClass
    Dim strSQL As String
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer
    Shared isRadChk As String


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RadClearALL As System.Web.UI.WebControls.RadioButton
    Protected WithEvents RadCheckAll As System.Web.UI.WebControls.RadioButton

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
            If Request("pg") <> Nothing Then
                dgUse.CurrentPageIndex = CInt(Request("pg"))
            End If
            strSQL = " Select i.Code From Items i Left Join Product_Stru ps ON i.id=ps.productID Where i.id='" & Request("id") & "'"
            ItemLabel.Text = "<font size='4pt'>" & SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) & "</font>"
            BindData()
            'BtnUpdate.Attributes.Add("onclick", "return confirm('确定要升级吗?');")
        End If
    End Sub
    Sub BindData()
        Dim dst As DataSet

        calculateProductStructure(Request("id"), 0)
        calculateProductReplace(Request("id"), 0)

        'strSQL = " Select Distinct i.id, iul.grade, i.code, i.description From tcpc0.dbo.Items i " _
        '& " Inner Join tcpc0.dbo.ItemUsedList iul On i.id=iul.itemID And iul.userID='" & Session("uID") & "' And iul.plantID='" & Session("plantCode") & "' " _
        '& " Where i.status<>2 And i.type<>0 Order By i.code "

        strSQL = " Select Distinct i.id,i.code,i.status,i.description,Isnull(iul.grade,0) From tcpc0.dbo.Items i " _
                       & " Inner Join tcpc0.dbo.ItemUsedList iul On i.id=iul.itemID And iul.userID='" & Session("uID") & "' And iul.plantID='" & Session("plantCode") & "' " _
                       & " Where i.status<>2 And i.type<>0 Order By i.code "
        dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)

        Dim dtlUse As New DataTable
        dtlUse.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
        'dtlUse.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
        dtlUse.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
        dtlUse.Columns.Add(New DataColumn("productID", System.Type.GetType("System.String")))
        dtlUse.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))
        dtlUse.Columns.Add(New DataColumn("grade", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                Dim drowUse As DataRow
                For i = 0 To .Rows.Count - 1
                    drowUse = dtlUse.NewRow()
                    drowUse.Item("code") = .Rows(i).Item(1).ToString().Trim()
                    ' drowUse.Item("name") = .Rows(i).Item(1).ToString().Trim()
                    drowUse.Item("description") = .Rows(i).Item(3).ToString().Trim()
                    drowUse.Item("grade") = .Rows(i).Item(4).ToString().Trim()
                    drowUse.Item("productID") = .Rows(i).Item(0).ToString().Trim()
                    If .Rows(i).Item(2).ToString.Trim() = "0" Then
                        drowUse.Item("status") = "使用"
                    Else
                        If .Rows(i).Item(2).ToString.Trim() = "1" Then
                            drowUse.Item("status") = "试用"
                        Else
                            If .Rows(i).Item(2).ToString.Trim() = "2" Then
                                drowUse.Item("status") = "停用"
                            End If
                        End If
                    End If
                    dtlUse.Rows.Add(drowUse)
                Next
            End If
        End With
        Dim dvUse As DataView
        dvUse = New DataView(dtlUse)
        If (Session("orderby").Length <= 0) Then
            Session("orderby") = "productID"
        End If
        Try
            dvUse.Sort = Session("orderby") & Session("orderdir")
            dgUse.DataSource = dvUse
            dgUse.DataBind()
        Catch
        End Try
        Dim j As Integer
        For j = 0 To dgUse.Items.Count - 1
            If dgUse.Items(j).Cells(2).Text = Request("id") Then
                CType(dgUse.Items(j).FindControl("isUpdate"), CheckBox).Checked = True
                CType(dgUse.Items(j).FindControl("isUpdate"), CheckBox).Enabled = False
            End If
        Next
    End Sub

    Private Function calculateProductStructure(ByVal str As String, ByVal grade As Integer)
        Dim reader As SqlDataReader
        strSQL = " Select ps.productID From Product_stru ps Where ps.childID='" & str.Trim() & "'"
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSQL)
        While reader.Read()
            strSQL = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
            calculateProductStructure(reader(0), grade + 1)
        End While
        reader.Close()
    End Function

    Private Function calculateProductReplace(ByVal str As String, ByVal grade As Integer)
        Dim reader As SqlDataReader
        strSQL = " Select ps.productID From Product_stru ps " _
               & " Inner Join product_replace pr On pr.prodStruID=ps.productStruID " _
               & " Where pr.itemID='" & str.Trim() & "' "
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSQL)
        While reader.Read()
            strSQL = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
            calculateProductReplace(reader(0), grade + 1)
        End While
        reader.Close()
    End Function

    Private Sub BtnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBack.Click
        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") _
                        & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st")), True)
    End Sub

    Private Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        If RadioButtonList1.SelectedIndex = 0 Then
            Dim i As Integer
            For i = 0 To dgUse.Items.Count - 1
                CType(dgUse.Items(i).FindControl("isUpdate"), CheckBox).Checked = True
            Next
        Else
            If RadioButtonList1.SelectedIndex = 1 Then
                Dim i As Integer
                For i = 0 To dgUse.Items.Count - 1
                    CType(dgUse.Items(i).FindControl("isUpdate"), CheckBox).Checked = False
                    If dgUse.Items(i).Cells(2).Text = Request("id") Then
                        CType(dgUse.Items(i).FindControl("isUpdate"), CheckBox).Checked = True
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        Dim strSQLa As String
        Dim strSQLb As String
        Dim param(1) As SqlParameter
        Dim ver, i, retValue As Integer
        Dim partID As Integer
        Dim productID As Integer

        'update the chose node 
        strSQLa = " Delete From ItemUsedList Where userID=" & Session("uID") & " And plantID=" & Session("plantCode")
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQLa)

        For i = 0 To dgUse.Items.Count - 1
            productID = dgUse.Items(i).Cells(2).Text()

            If CType(dgUse.Items(i).Cells(3).FindControl("isupdate"), CheckBox).Checked Then
                strSQLb = "Insert Into ItemUsedList(itemID, userID, plantID,  isUpdate,grade) Values('" & productID & "','" & Session("uID") & "','" & Session("plantCode") & "',1,'" & dgUse.Items(i).Cells(4).Text().Trim() & "') "
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQLb)
            End If

        Next
        param(0) = New SqlParameter("@intUserID", Session("uID"))
        param(1) = New SqlParameter("@intPlantID", Session("plantCode"))
        retValue = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_ItemUsedListUpdate", param)

        If retValue < 0 Then
            ltlAlert.Text = "alert('保存更新失败！'); window.location.href='/product/ItemStruEdit.aspx?id=" & Request("id") & "code=" & Request("code") & "&cat=" _
                          & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "try=" & Request("try") & "&rm=" & DateTime.Now() & Rnd() & "';"
        Else
            param(0) = New SqlParameter("@intUserID", Session("uID"))
            param(1) = New SqlParameter("@intPlantID", Session("plantCode"))
            retValue = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Product_StructureUpgrade", param)

            If retValue < 0 Then
                ltlAlert.Text = "alert('保存更新失败了！'); window.location.href='/product/ItemStruEdit.aspx?id=" & Request("id") & "code=" & Request("code") & "&cat=" _
                              & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "try=" & Request("try") & "&rm=" & DateTime.Now() & Rnd() & "';"
            Else
                ltlAlert.Text = "alert('保存更新成功！'); window.location.href='/product/ItemStruEdit.aspx?id=" & Request("id") & "code=" & Request("code") & "&cat=" _
                              & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "try=" & Request("try") & "&rm=" & DateTime.Now() & Rnd() & "';"
            End If
        End If
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique")
        Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") _
                        & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st")), True)
    End Sub
End Class

End Namespace
