'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-5
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Part Used On Which Products
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class partUsedByList
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents lblReplace As System.Web.UI.WebControls.Label
        Dim nRet As Integer
        Dim strSql As String

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub



        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("19070103", "部件结构替换")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then

                BtnReplace.Enabled = Me.Security("19070103").isValid

                strSql = " Select code From Items Where id='" & Request("id") & "' And status<>2 "
                lblPartCode.Text = "<font size='4pt'>" & SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSql) & "</font>"

                strSql = " Delete From ItemUsedList Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSql)

                calculateProductStructure(Request("id"), 0)
                calculateProductReplace(Request("id"), 0)

                BindData()
            End If
        End Sub

        Sub BindData()
            Dim dst As DataSet
            Dim cnt As Integer

            strSql = " Select iul.id, Isnull(iul.grade,0), i.code, i.item_qad as qad, i.description, convert(varchar(19),i.createdDate,121) as createdDate, " _
                   & " CASE WHEN l.xxwkf_log01=1 THEN N'生产锁定' WHEN l.xxwkf_log02=1 THEN N'安规锁定' WHEN l.xxwkf_log03=1 THEN N'技术锁定' ELSE '' END as lock ,case when  l.xxwkf_log01=1 THEN xxwkf_chr02 else '' end as xxwkf_chr02 From tcpc0.dbo.Items i " _
                   & " Inner Join tcpc0.dbo.ItemUsedList iul On i.id=iul.itemID And iul.userID='" & Session("uID") & "' And iul.plantID='" & Session("plantCode") & "' " _
                   & " left join  QAD_Data.dbo.xxwkf_mstr l ON i.item_qad=l.xxwkf_chr01" _
                   & " Where i.status<>2 And i.type<>0 Order By iul.id "

            Session("EXTitle") = "200^<b>产品型号</b>~^120^<b>QAD号</b>~^500^<b>产品描述</b>~^120^<b>进库日期</b>~^<b>锁定</b>~^<b>锁定信息</b>~^"
            Session("EXSQL") = strSql
            Session("EXHeader") = "部件号~" & lblPartCode.Text & "~^"

            dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("qad", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("desc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("grade", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("tid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("createddate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("lock", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("xxwkf_chr02", System.Type.GetType("System.String")))
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim drow As DataRow
                    cnt = 0
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("gsort") = i + 1
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("code") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("qad") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("desc") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("grade") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("tid") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("createddate") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("lock") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("xxwkf_chr02") = .Rows(i).Item(7).ToString().Trim()
                        dtl.Rows.Add(drow)
                        cnt = cnt + 1
                    Next
                    BtnReplace.Enabled = True
                Else
                    BtnReplace.Enabled = False
                End If
                lblCount.Text = "数量: " & cnt.ToString().Trim()
            End With
            dst.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Try
                dgProdList.DataSource = dvw
                dgProdList.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgProdList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProdList.PageIndexChanged
            dgProdList.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub BtnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReplace.Click
            Dim partID As String
            Dim reader, reader1 As SqlDataReader
            Dim ver, i, retValue As Integer
            Dim param(4) As SqlParameter
            Dim param1(1) As SqlParameter
            'Dim iulHash As New Hashtable

            If Not Me.Security("19070103").isValid Then
                ltlAlert.Text = "alert('没有权限对部件进行替换！');"
                Exit Sub
            Else
                strSql = " Select id From Items Where code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "' And status<>2 And Type=0"
                partID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSql)
                If partID = "" Or partID = Nothing Then
                    ltlAlert.Text = "alert('你输入的部件编号" & txtCode.Text.Trim() & "不存在！');"
                    Exit Sub
                Else
                    For i = 0 To dgProdList.Items.Count - 1
                        If CType(dgProdList.Items(i).FindControl("chkUpdate"), CheckBox).Checked = True Then
                            strSql = "Update ItemUsedList set userID='" & Session("uID") & "',plantID='" & Session("plantCode") & "', grade='" & dgProdList.Items(i).Cells(4).Text.Trim() & "',isUpdate=1 where id='" & dgProdList.Items(i).Cells(0).Text.Trim() & "'"
                        Else
                            strSql = "Update ItemUsedList set userID='" & Session("uID") & "',plantID='" & Session("plantCode") & "', grade='" & dgProdList.Items(i).Cells(4).Text.Trim() & "',isUpdate=0 where id='" & dgProdList.Items(i).Cells(0).Text.Trim() & "'"
                        End If
                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSql)
                    Next

                    'create new item in itemusedlist teble
                    param1(0) = New SqlParameter("@intUserID", Session("uID"))
                    param1(1) = New SqlParameter("@intPlantID", Session("plantCode"))
                    retValue = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_ItemUsedListUpdate", param1)

                    If retValue < 0 Then
                        ltlAlert.Text = "alert('替换失败！'); window.location.href='/part/partlist.aspx?code=" & Request("code") & "&cat=" _
                                      & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "&try=" & Request("try") & "&rm=" & DateTime.Now() & Rnd() & "';"
                    Else
                        param(0) = New SqlParameter("@itemS", CInt(Request("id")))
                        param(1) = New SqlParameter("@itemT", partID)
                        param(2) = New SqlParameter("@intUserID", Session("uID"))
                        param(3) = New SqlParameter("@intPlantID", Session("plantCode"))
                        If txtDesc.Text.Trim().Length > 0 Then
                            param(4) = New SqlParameter("@adddesc", chk.sqlEncode(txtDesc.Text.Trim()))
                        Else
                            param(4) = New SqlParameter("@adddesc", "")
                        End If
                        retValue = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_StruReplace", param)

                        If retValue < 0 Then
                            ltlAlert.Text = "alert('替换失败！'); window.location.href='/part/partlist.aspx?code=" & Request("code") & "&cat=" _
                                          & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "&try=" & Request("try") & "&rm=" & DateTime.Now() & Rnd() & "';"
                        Else
                            SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique")
                            ltlAlert.Text = "alert('替换成功！'); window.location.href='/part/partlist.aspx?code=" & Request("code") & "&cat=" _
                                          & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "&try=" & Request("try") & "&rm=" & DateTime.Now() & Rnd() & "';"
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            If Request("st") <> Nothing Then
                If Request("code") <> Nothing Then
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&st=true"), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&pg=" & Request("pg") & "&st=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&st=true"), True)
                        End If
                    End If
                Else
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?cat=" & Request("cat") & "&st=true"), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?pg=" & Request("pg") & "&st=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?st=true"), True)
                        End If
                    End If
                End If
            ElseIf Request("try") <> Nothing Then
                If Request("code") <> Nothing Then
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&try=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&try=true"), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&pg=" & Request("pg") & "&try=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&try=true"), True)
                        End If
                    End If
                Else
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?cat=" & Request("cat") & "&pg=" & Request("pg") & "&try=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?cat=" & Request("cat") & "&try=true"), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?pg=" & Request("pg") & "&try=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?try=true"), True)
                        End If
                    End If
                End If
            Else
                If Request("code") <> Nothing Then
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&cat=" & Request("cat")), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&pg=" & Request("pg")), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code")), True)
                        End If
                    End If
                Else
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx?cat=" & Request("cat")), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partlist.aspx?pg=" & Request("pg")), True)
                        Else
                            Response.Redirect(chk.urlRand("partlist.aspx"), True)
                        End If
                    End If
                End If
            End If
        End Sub
        Private Function calculateProductStructure(ByVal str As String, ByVal grade As Integer)
            Dim reader As SqlDataReader

            strSql = " Select ps.productID,case when l.xxwkf_log01=1 or l.xxwkf_log02=1 or l.xxwkf_log03=1 then 1 else 0 end as lock From Product_stru ps INNER JOIN items it ON it.id = ps.productID left join  QAD_Data.dbo.xxwkf_mstr l ON it.item_qad=l.xxwkf_chr01  Where  ps.childID='" & str.Trim() & "'"


            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
            While reader.Read()
                If CheckBox2.Checked = True Then
                    strSql = ""
                    If reader(1) = 1 Then
                        strSql = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "
                    End If
                Else
                    strSql = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "
                End If

                If strSql <> "" Then
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
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

            strSql = " Select ps.productID,case when l.xxwkf_log01=1 or l.xxwkf_log02=1 or l.xxwkf_log03=1 then 1 else 0 end as lock From Product_stru ps " _
              & " Inner Join product_replace pr On pr.prodStruID=ps.productStruID  INNER JOIN items it ON it.id = ps.productID left join  QAD_Data.dbo.xxwkf_mstr l ON it.item_qad=l.xxwkf_chr01" _
              & " Where  pr.itemID='" & str.Trim() & "' "
           

            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
            While reader.Read()
                If CheckBox2.Checked = True Then
                    If reader(1) = 1 Then
                        strSql = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "

                    End If
                Else
                    strSql = " Insert Into ItemUsedList(itemID, userID, plantID, isUpdate, grade) Values('" & reader(0) & "','" & Session("uID") & "','" & Session("plantCode") & "','0','" & grade + 1 & "') "

                End If
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
                If CheckBox1.Checked = True And grade <= 10 Then
                    calculateProductReplace(reader(0), grade + 1)
                End If
            End While
            reader.Close()
        End Function

        Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            strSql = " Delete From ItemUsedList Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSql)

            calculateProductStructure(Request("id"), 0)
            calculateProductReplace(Request("id"), 0)

            BindData()
        End Sub

        Private Sub BtnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAll.Click
            Dim m As Integer
            For m = 0 To dgProdList.Items.Count - 1
                CType(dgProdList.Items(m).FindControl("chkUpdate"), CheckBox).Checked = True
            Next
        End Sub

        Private Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
            Dim n As Integer
            For n = 0 To dgProdList.Items.Count - 1
                CType(dgProdList.Items(n).FindControl("chkUpdate"), CheckBox).Checked = False
            Next
        End Sub

        Protected Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
            strSql = " Delete From ItemUsedList Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSql)

            calculateProductStructure(Request("id"), 0)
            calculateProductReplace(Request("id"), 0)

            BindData()
        End Sub

        Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
            Dim EXTitle As String = "100^<b>QAD</b>~^100^<b>物料号</b>~^200^<b>描述</b>~^100^<b>QAD描述1</b>~^100^<b>QAD描述2</b>~^"
            strSql = " Select i.item_qad,i.code,i.description,i.item_qad_desc1,i.item_qad_desc2 From tcpc0.dbo.Items i  " _
                  & " Inner Join tcpc0.dbo.ItemUsedList iul On i.id=iul.itemID And iul.userID='" & Session("uID") & "' And iul.plantID='" & Session("plantCode") & "' " _
                  & " Where i.status<>2 And i.type<>0 Order By iul.id "
            Me.ExportExcel(chk.dsn0(), EXTitle, strSql, False)
        End Sub
    End Class

End Namespace
