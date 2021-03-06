'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-9
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Product Used On Which Products
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class productUsedByList
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim query As String

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents lblPartCode As System.Web.UI.WebControls.Label


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("19030301", "产品结构替换")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then

                BtnReplace.Enabled = Me.Security("19030301").isValid

                query = " Select code From Items Where id='" & Request("id") & "' And status<>2 And type<>0 "
                lblProdCode.Text = "<font size='4pt'>" & SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, query) & "</font>"

                query = " Delete From ItemUsedList Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, query)

                calculateProductStructure(Request("id"), 0)
                calculateProductReplace(Request("id"), 0)


                BindData()
            End If
        End Sub

        Sub BindData()
            Dim ds As DataSet
            Dim cnt As Integer

            query = " Select Distinct iul.id, Isnull(iul.grade,0), i.code, i.item_qad as qad, i.description, convert(varchar(19),i.createdDate,121) as createdDate," _
                   & "  CASE WHEN l.xxwkf_log01=1 THEN N'生产锁定' WHEN l.xxwkf_log02=1 THEN N'安规锁定' WHEN l.xxwkf_log03=1 THEN N'技术锁定' ELSE '' END as lock ,case when  l.xxwkf_log01=1 THEN xxwkf_chr02 else '' end as xxwkf_chr02 From tcpc0.dbo.Items i " _
                   & " Inner Join tcpc0.dbo.ItemUsedList iul On i.id=iul.itemID And iul.userID='" & Session("uID") & "' And iul.plantID='" & Session("plantCode") & "' " _
                   & " left join  QAD_Data.dbo.xxwkf_mstr l ON i.item_qad=l.xxwkf_chr01" _
                   & " Where i.status<>2 And i.type<>0 Order By i.code "

            Session("EXTitle") = "200^<b>产品型号</b>~^120^<b>QAD号</b>~^500^<b>产品描述</b>~^120^<b>进库日期</b>~^<b>锁定</b>~^<b>锁定信息</b>~^"
            Session("EXSQL") = query
            Session("EXHeader") = "产品型号~" & lblProdCode.Text & "~^"

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("qad", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("desc", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("grade", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("createddate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("lock", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("xxwkf_chr02", System.Type.GetType("System.String")))

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    lblCount.Text = "数量: " & .Rows.Count.ToString()
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("code") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("qad") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("desc") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("grade") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("createddate") = .Rows(i).Item(5).ToString().Trim()
                        dr1.Item("lock") = .Rows(i).Item(6).ToString().Trim()
                        dr1.Item("xxwkf_chr02") = .Rows(i).Item(7).ToString().Trim()
                        dt.Rows.Add(dr1)
                        cnt = cnt + 1
                    Next
                End If
                lblCount.Text = "数量: " & cnt.ToString().Trim()
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                dgProdList.DataSource = dv
                dgProdList.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgProdList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProdList.PageIndexChanged
            dgProdList.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub BtnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReplace.Click
            Dim strsql As String
            Dim prodID As String
            Dim reader, reader1 As SqlDataReader
            Dim param(4) As SqlParameter
            Dim param1(1) As SqlParameter
            Dim ver, i, retValue As Integer

            If Not Me.Security("19030301").isValid Then

                ltlAlert.Text = "alert('没有权限进行替换操作！');"
                Exit Sub
            Else
                query = " Select id From Items Where code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "' And status<>2 And type<>0 "
                prodID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, query)
                If prodID = "" Or prodID = Nothing Then
                    ltlAlert.Text = "alert('你输入的产品型号" & txtCode.Text.Trim() & "不存在！');"
                    Exit Sub
                Else
                    For i = 0 To dgProdList.Items.Count - 1
                        If CType(dgProdList.Items(i).FindControl("chkUpdate"), CheckBox).Checked = True Then
                            strsql = "Update ItemUsedList set userID='" & Session("uID") & "',plantID='" & Session("plantCode") & "', grade='" & dgProdList.Items(i).Cells(4).Text.Trim() & "',isUpdate=1 where id='" & dgProdList.Items(i).Cells(0).Text.Trim() & "'"
                        Else
                            strsql = "Update ItemUsedList set userID='" & Session("uID") & "',plantID='" & Session("plantCode") & "', grade='" & dgProdList.Items(i).Cells(4).Text.Trim() & "',isUpdate=0 where id='" & dgProdList.Items(i).Cells(0).Text.Trim() & "'"
                        End If
                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strsql)
                    Next

                    'create new item in itemusedlist teble
                    param1(0) = New SqlParameter("@intUserID", Session("uID"))
                    param1(1) = New SqlParameter("@intPlantID", Session("plantCode"))
                    retValue = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_ItemUsedListUpdate", param1)

                    If retValue < 0 Then
                        ltlAlert.Text = "alert('替换失败！');"
                    Else
                        param(0) = New SqlParameter("@itemS", CInt(Request("id")))
                        param(1) = New SqlParameter("@itemT", prodID)
                        param(2) = New SqlParameter("@intUserID", Session("uID"))
                        param(3) = New SqlParameter("@intPlantID", Session("plantCode"))
                        If txtDesc.Text.Trim().Length > 0 Then
                            param(4) = New SqlParameter("@adddesc", chk.sqlEncode(txtDesc.Text.Trim()))
                        Else
                            param(4) = New SqlParameter("@adddesc", "")
                        End If
                        retValue = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_StruReplace", param)

                        If retValue < 0 Then
                            ltlAlert.Text = "alert('替换失败！');"
                        Else
                            SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique")

                            ltlAlert.Text = "alert('替换成功！');"
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            Me.Redirect("/product/productlist.aspx")
        End Sub

        Private Function calculateProductStructure(ByVal str As String, ByVal grade As Integer)
            Dim reader As SqlDataReader
            'strSql = " Select ps.productID From Items i Inner Join Product_stru ps On ps.productID=i.ID " _
            '       & " Where ps.childID='" & str.Trim() & "' And i.status<>2 "
            
            query = " Select ps.productID,case when l.xxwkf_log01=1 or l.xxwkf_log02=1 or l.xxwkf_log03=1 then 1 else 0 end as lock From Product_stru ps INNER JOIN items it ON it.id = ps.productID left join  QAD_Data.dbo.xxwkf_mstr l ON it.item_qad=l.xxwkf_chr01  Where  ps.childID='" & str.Trim() & "'"


            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, query)
            While reader.Read()
                'strSql = " select itemid from ItemUsedList where itemID='" & reader(0) & "'"
                'If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql) <= 0 Then
                If CheckBox2.Checked = True Then
                    query = ""
                    If reader(1) = 1 Then
                        query = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "
                    End If
                Else
                    query = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "
                End If
                If query <> "" Then
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, query)
                End If
                If CheckBox1.Checked = True And grade <= 10 Then
                    calculateProductStructure(reader(0), grade + 1)
                End If
                'End If
            End While
            reader.Close()
        End Function

        Private Function calculateProductReplace(ByVal str As String, ByVal grade As Integer)
            Dim reader As SqlDataReader
          
            query = " Select ps.productID,case when l.xxwkf_log01=1 or l.xxwkf_log02=1 or l.xxwkf_log03=1 then 1 else 0 end as lock From Product_stru ps " _
              & " Inner Join product_replace pr On pr.prodStruID=ps.productStruID  INNER JOIN items it ON it.id = ps.productID left join  QAD_Data.dbo.xxwkf_mstr l ON it.item_qad=l.xxwkf_chr01" _
              & " Where  pr.itemID='" & str.Trim() & "' "
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, query)
            While reader.Read()
                If CheckBox2.Checked = True Then
                    If reader(1) = 1 Then
                        query = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "

                    End If
                Else
                    query = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "

                End If
               SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, query)
                If CheckBox1.Checked = True And grade <= 10 Then
                    calculateProductReplace(reader(0), grade + 1)
                End If
            End While
            reader.Close()
        End Function
        Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            query = " Delete From ItemUsedList Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, query)

            calculateProductStructure(Request("id"), 0)
            calculateProductReplace(Request("id"), 0)

            BindData()
        End Sub

        Protected Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
            query = " Delete From ItemUsedList Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, query)

            calculateProductStructure(Request("id"), 0)
            calculateProductReplace(Request("id"), 0)

            BindData()
        End Sub
    End Class

End Namespace

