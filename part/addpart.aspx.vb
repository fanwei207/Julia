'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-2
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   add new part to the datebase
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Text.RegularExpressions


Namespace tcpc


    Partial Class addpart
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Shared oldUnit As String = ""
        Shared oldDesc As String = ""
        Shared oldCode As String = ""
        Shared oldQad As String = ""
        Dim param(3) As SqlParameter
        Dim strSQL As String
        Dim adam As New adamClass()
        Dim flagchange As Boolean  '标记修改时，QAD前后有没有改动

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents statusID As System.Web.UI.WebControls.DropDownList


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
       
            If Not IsPostBack Then

                BtnDelete.Enabled = Me.Security("19070102").isValid

                radStop.Enabled = Me.Security("19070500").isValid
                radNormal.Enabled = Me.Security("19070500").isValid
                radTry.Enabled = Me.Security("19070500").isValid

                If Request("id") = Nothing Then
                    BtnAddNew.Visible = True
                    radStop.Enabled = False
                    BtnDelete.Visible = False
                    ltlAlert.Text = "Form1.txtCode.focus();"
                Else
                    Dim reader As SqlDataReader
                    strSQL = " SELECT i.id, i.code, isnull(i.description,''),  isnull(ic.name,''),  i.status, Isnull(i.min_inv,0), Isnull(i.unit,''), Isnull(i.tranUnit,''), Isnull(i.tranRate,0),isnull(i.isdoc,0),i.item_qad ,isnull( mpi,'')mpi" _
                           & " From Items i Inner Join ItemCategory ic On i.category = ic.id " _
                           & " Where i.id =" & Request("id")
                    reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL)
                    While (reader.Read())
                        txtCode.Text = reader(1).ToString().Trim()
                        txtCategory.Text = reader(3).ToString().Trim()
                        oldDesc = reader(2).ToString().Trim()
                        txtDesc.Text = reader(2).ToString().Trim()
                        txtQad.Text = reader("item_qad").ToString().Trim()
                        hidQad.Value = reader("item_qad").ToString().Trim()
                        txtmpi.Text  = reader("mpi").ToString().Trim()

                        If reader(4) = "2" Then
                            radStop.Checked = True
                            radTry.Checked = False
                            radNormal.Checked = False
                        ElseIf reader(4) = "1" Then
                            radTry.Checked = True
                            radStop.Checked = False
                            radNormal.Checked = False
                        Else
                            radStop.Checked = False
                            radTry.Checked = False
                            radNormal.Checked = True
                        End If
                        txtMinQty.Text = reader(5).ToString().Trim()
                        txtUnit.Text = reader(6).ToString().Trim()
                        oldUnit = reader(6).ToString().Trim()
                        oldCode = reader(1).ToString().Trim()
                        oldQad = reader(10).ToString().Trim()
                        txtTranUnit.Text = reader(7).ToString().Trim()
                        If CInt(reader(8)) = 0 Then
                            txtRate.Text = ""
                        Else
                            txtRate.Text = reader(8).ToString().Trim()
                        End If
                    End While
                    reader.Close()
                    BtnModify.Visible = True
                    BtnDelete.Visible = True

                    If Session("uRole") = 1 Then
                        BtnDelete.Visible = True
                        txtCode.Enabled = True
                    Else
                        'BtnDelete.Visible = False
                        BtnDelete.Visible = Me.Security("19070102").isValid
                        'txtCode.Enabled = False
                        txtCode.Enabled = True
                    End If

                End If
                BtnDelete.Attributes.Add("onclick", "return confirm('确定要删除该部件吗？');")
                BindData()

            End If
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&qad=" & Request("qad") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "&try=" & Request("try")), True)
        End Sub

        Private Sub BtnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModify.Click
            ltlAlert.Text = ""
            Dim cnt As Integer
            Dim catID As Integer = 0
            Dim nt As String = txtDesc.Text.Trim()
            Dim numLS As Integer
            Dim numLS1 As Integer
            Dim numLSN As Integer
            Dim numLSN1 As Integer
            Dim numVer As Integer
            Dim numSubVer As Integer
            Dim strItem As String
            Dim strMpi As String

            If txtQad.Text.Trim() <> hidQad.Value Then
                flagchange = True
            Else
                flagchange = False
            End If

            If txtCode.Text.Trim().Length <= 0 Then
                ltlAlert.Text = "alert('部件号不能为空！');"
                Exit Sub
            ElseIf txtCode.Text.Trim().Length > 50 Then
                ltlAlert.Text = "alert('部件号的最大长度为50个字符！');"
                Exit Sub
            ElseIf txtCode.Text.Trim().IndexOf(" ") <> -1 Or txtCode.Text.Trim().IndexOf("/") <> -1 Or txtCode.Text.Trim().IndexOf("_") <> -1 Or txtCode.Text.Trim().IndexOf("\") <> -1 Then
                ltlAlert.Text = "alert('部件号不能包含空格，斜杠，下划线！');"
                Exit Sub
            End If

            If txtQad.Text.Trim().Length > 14 Then
                ltlAlert.Text = "alert('QAD号的最大长度为14位！');"
                Exit Sub
            ElseIf Not IsNumber(txtQad.Text.Trim().ToString()) Then
                ltlAlert.Text = "alert('QAD号应为一串数字！');"
                Exit Sub
            End If

            If txtCategory.Text.Trim().Length <= 0 Then
                ltlAlert.Text = "alert('分类不能为空！');"
                Exit Sub
            ElseIf txtCategory.Text.Trim().Length > 10 Then
                ltlAlert.Text = "alert('分类的最大长度为10个字符！');"
                Exit Sub
            End If

            If txtmpi.Text.Trim().Length > 20 Then
                ltlAlert.Text = "alert('条形码的最大长度为20位！');"
                Exit Sub
            ElseIf Not Regex.IsMatch(txtmpi.Text.Trim().ToString(), "^[A-Za-z0-9]*$") Then
                ltlAlert.Text = "alert('条形码只能是数字和字母！');"
                Exit Sub
            Else
                strMpi = txtmpi.Text.Trim().ToString()
            End If


            If (nt.Length > 255) Then
                ltlAlert.Text = "alert('文本最大长度为255个字符！');"
                Exit Sub
            End If

            Dim st As Integer
            If radStop.Checked = True Then
                st = 2
            ElseIf radTry.Checked = True Then
                st = 1
            Else
                st = 0
            End If

            If Len(txtUnit.Text.Trim()) > 5 Then
                ltlAlert.Text = "alert('单位的最大长度为5个字符！');"
                Exit Sub
            End If
            If Len(txtTranUnit.Text.Trim()) > 5 Then
                ltlAlert.Text = "alert('转换前单位的最大长度为5个字符！');"
                Exit Sub
            End If
            Dim minqty As Integer
            If txtMinQty.Text.Trim().Length <= 0 Then
                minqty = 0
            ElseIf IsNumeric(txtMinQty.Text.Trim()) = False Then
                ltlAlert.Text = "alert('最小库存量非数值，请重新输入！');"
                Exit Sub
            ElseIf Val(txtMinQty.Text.Trim()) < 0 Then
                ltlAlert.Text = "alert('最小库存量不能为负数，请重新输入！');"
                Exit Sub
            Else
                minqty = CInt(txtMinQty.Text.Trim())
            End If

            Dim rate As Decimal
            If txtRate.Text.Trim().Length <= 0 Then
                rate = 0
            ElseIf IsNumeric(txtRate.Text.Trim()) = False Then
                ltlAlert.Text = "alert('转换系数非数值，请重新输入！');"
                Exit Sub
            ElseIf Val(txtRate.Text.Trim()) <= 0 Then
                ltlAlert.Text = "alert('转换系数不能为零和负数，请重新输入！');"
                Exit Sub
            Else
                rate = CDbl(txtRate.Text.Trim())
            End If

            numLS = InStr(txtCode.Text.Trim().ToUpper(), "LS-")
            numLS1 = InStr(txtCode.Text.Trim().ToUpper(), "LS")
            numLSN = InStr(txtCode.Text.Trim().ToUpper(), "LSN")
            numLSN1 = InStr(txtCode.Text.Trim().ToUpper(), "LSN-")
            If numLS = 1 Then
                numVer = 0
                numSubVer = 0
                strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLS + 2))
                numLS1 = 0
                If numLS = 1 Then
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            ElseIf numLSN1 = 1 Then
                numVer = 0
                numSubVer = 0
                strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLSN1 + 2))
                numLS1 = 0
                If numLSN1 = 1 Then
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            ElseIf numLSN = 1 Then
                numLS = 0
                numLS = InStr(txtCode.Text.Trim().Substring(numLSN + 2).ToUpper(), "-")
                If numLS > 0 Then
                    If IsNumeric(Mid(txtCode.Text.Trim().Substring(numLSN + 2), 1, numLS - 1)) = True Then
                        numVer = Mid(txtCode.Text.Trim().Substring(numLSN + 2), 1, numLS - 1)
                        strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLSN + 2).Substring(numLS))
                    Else
                        numVer = 1
                        strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLSN + 2).Substring(numLS))
                        ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                        Exit Sub
                    End If
                    numLS = 0
                    numLS = InStr(strItem, "-")
                    If numLS > 0 Then
                        If IsNumeric(Mid(strItem.Trim(), 1, numLS - 1)) = True Then
                            numSubVer = Mid(strItem.Trim(), 1, numLS - 1)
                        Else
                            numSubVer = 1
                            ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                            Exit Sub
                        End If
                        strItem = strItem.Substring(numLS)
                    Else
                        numSubVer = 0
                        ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                        Exit Sub
                    End If
                Else
                    numVer = 1
                    numSubVer = 1
                    strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLSN + 2))
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            ElseIf numLS1 = 1 Then
                numLS = 0
                numLS = InStr(txtCode.Text.Trim().Substring(numLS1 + 1).ToUpper(), "-")
                If numLS > 0 Then
                    If IsNumeric(Mid(txtCode.Text.Trim().Substring(numLS1 + 1), 1, numLS - 1)) = True Then
                        numVer = Mid(txtCode.Text.Trim().Substring(numLS1 + 1), 1, numLS - 1)
                        strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLS1 + 1).Substring(numLS))
                    Else
                        numVer = 1
                        strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLS1 + 1).Substring(numLS))
                        ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                        Exit Sub
                    End If
                    numLS = 0
                    numLS = InStr(strItem, "-")
                    If numLS > 0 Then
                        If IsNumeric(Mid(strItem.Trim(), 1, numLS - 1)) = True Then
                            numSubVer = Mid(strItem.Trim(), 1, numLS - 1)
                        Else
                            numSubVer = 1
                            ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                            Exit Sub
                        End If
                        strItem = strItem.Substring(numLS)
                    Else
                        numSubVer = 0
                        ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                        Exit Sub
                    End If
                Else
                    numVer = 1
                    numSubVer = 1
                    strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLS1 + 1))
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            Else
                numVer = 0
                numSubVer = 0
                strItem = chk.sqlEncode(txtCode.Text.Trim())
            End If

            If st = 2 Then
                strSQL = " Select Count(ps.productID) From Product_stru ps Inner Join Items i On i.ID=ps.productID And i.status<>2 Where childID='" & Request("id") & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If cnt > 0 Then
                    ltlAlert.Text = "alert('仍有产品使用该部件无法停用！');"
                    Exit Sub
                End If
                strSQL = " Select Count(ps.productID) From Product_replace pr Inner Join Product_stru ps On pr.prodStruID=ps.productStruID " _
                       & " Inner Join Items i On i.ID=ps.productID And i.status<>2  " _
                       & " Where pr.itemID='" & Request("id") & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If cnt > 0 Then
                    ltlAlert.Text = "alert('仍有产品使用该部件无法停用！');"
                    Exit Sub
                End If
            End If

            If txtQad.Text.Trim().Length > 0 Then
                cnt = 0
                strSQL = " Select Count(*) From Items Where id <> " & Request("id") & " And item_qad = N'" & txtQad.Text.Trim() & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                If cnt > 0 Then
                    ltlAlert.Text = "alert('QAD号被占用，无法修改！');"
                    Exit Sub
                End If
            End If

            strSQL = " Select Count(*) From Items Where code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "' And id<>'" & Request("id") & "'"
            cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
            If cnt < 1 Then
                strSQL = " Select id From ItemCategory Where name='" & chk.sqlEncode(txtCategory.Text.Trim()) & "' And type=0 "
                catID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If catID = 0 Then
                    param(0) = New SqlParameter("@categoryName", chk.sqlEncode(txtCategory.Text.Trim()))
                    param(1) = New SqlParameter("@intUserID", Session("uID"))
                    param(2) = New SqlParameter("@type", "0")
                    param(3) = New SqlParameter("@intPlant", Session("plantCode"))
                    catID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_SetCategory", param)
                End If

                '''''''''''Sava part history
                Dim reader1 As SqlDataReader
                strSQL = " Select i.id, i.code,isnull(i.description,''), isnull(ic.name,''), ic.id " _
                       & " From Items i Left Outer Join ItemCategory ic On ic.id=i.category " _
                       & " Left Outer Join Companies c On c.company_id=i.customerID " _
                       & " Where i.id =" & Request("id")
                reader1 = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL)
                While (reader1.Read())
                    strSQL = " insert into items_his(item_id,code,description,category,old_code,old_description,old_category,createdby,createddate) " _
                             & " values('" & reader1(0) & "',N'" & chk.sqlEncode(txtCode.Text.Trim()) & "',N'" & chk.sqlEncode(txtDesc.Text.Trim()) & "','" & catID & "', " _
                             & " N'" & chk.sqlEncode(reader1(1)) & "',N'" & chk.sqlEncode(reader1(2)) & "','" & reader1(4) & "','" & Session("uID") & "',getdate() )"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                End While
                reader1.Close()

                If Me.Security("19070500").isValid Then
                    strSQL = " Update Items Set code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "'," _
                           & " description=N'" & chk.sqlEncode(txtDesc.Text.Trim()) & "'," _
                           & " category='" & catID & "'," _
                           & " status='" & st & "'," _
                           & " min_inv='" & txtMinQty.Text.Trim() & "'," _
                           & " unit=N'" & chk.sqlEncode(txtUnit.Text.Trim()) & "'," _
                           & " tranUnit=N'" & chk.sqlEncode(txtTranUnit.Text.Trim()) & "',"
                    If rate > 0 Then
                        strSQL &= " tranRate='" & rate & "',"
                    End If
                    If flagchange Then
                        strSQL &= "  item_qad_old = item_qad ,"
                        strSQL &= "  item_qad='" & chk.sqlEncode(txtQad.Text.Trim()) & "',"
                    End If
                    strSQL &= " itemNumber=N'" & strItem & "'," _
                           & " itemVersion='" & numVer & "', " _
                           & " itemSubVersion='" & numSubVer & "'," _
                           & " mpi='" & strMpi & "'," _
                           & " modifiedBy='" & Session("uID") & "'," _
                           & " modifiedDate='" & DateTime.Now() & "'," _
                           & " plantcode='" & Session("plantcode") & "'" _
                           & " Where id= " & Request("id")
                Else
                    strSQL = " Update Items Set code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "'," _
                           & " description=N'" & chk.sqlEncode(txtDesc.Text.Trim()) & "'," _
                           & " category='" & catID & "'," _
                           & " min_inv='" & txtMinQty.Text.Trim() & "'," _
                           & " unit=N'" & chk.sqlEncode(txtUnit.Text.Trim()) & "'," _
                           & " tranUnit=N'" & chk.sqlEncode(txtTranUnit.Text.Trim()) & "',"
                    If rate > 0 Then
                        strSQL &= " tranRate='" & rate & "',"
                    End If 
                    If flagchange Then
                        strSQL &= "  item_qad_old = item_qad ,"
                        strSQL &= "  item_qad='" & chk.sqlEncode(txtQad.Text.Trim()) & "',"
                    End If 
                    strSQL &= " itemNumber=N'" & strItem & "'," _
                           & " itemVersion='" & numVer & "', " _
                           & " itemSubVersion='" & numSubVer & "'," _
                           & " mpi='" & strMpi & "'," _
                           & " modifiedBy='" & Session("uID") & "'," _
                           & " modifiedDate='" & DateTime.Now() & "'," _
                           & " plantcode='" & Session("plantcode") & "'" _
                           & " Where id= " & Request("id")
                End If

                If (txtQad.Text.Trim().Length > 0) Then
                    strSQL &= "  Update ProductTrackingItem Set itm_qad = '" & txtQad.Text.Trim() & "' Where itm_code = '" & txtCode.Text.Trim() & "' And itm_qad <> '" & txtQad.Text.Trim() & "'"
                    strSQL &= "  Update ProductTrackingItemScms Set itm_qad = '" & txtQad.Text.Trim() & "' Where itm_code = '" & txtCode.Text.Trim() & "' And itm_qad <> '" & txtQad.Text.Trim() & "'"
                End If

                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                If Unnamed1_Click() = False Then
                    Exit Sub
                End If
                '如果修改了description（note）或者 单位 就保存到items_confirm表中
                If (String.Compare(oldDesc, txtDesc.Text.Trim()) Or String.Compare(oldUnit, txtUnit.Text.Trim()) Or String.Compare(oldCode, txtCode.Text.Trim()) Or String.Compare(oldQad, txtQad.Text.Trim())) Then
                    strSQL = "sp_part_insertItemsConfirm"
                    Dim sqlParam(11) As SqlParameter
                    sqlParam(0) = New SqlParameter("@itemsCode", txtCode.Text.Trim())
                    sqlParam(1) = New SqlParameter("@itemsQad", txtQad.Text.Trim())
                    sqlParam(2) = New SqlParameter("@oldUnit", oldUnit)
                    sqlParam(3) = New SqlParameter("@newUnit", txtUnit.Text.Trim())
                    sqlParam(4) = New SqlParameter("@oldDesc", oldDesc)
                    sqlParam(5) = New SqlParameter("@newDesc", txtDesc.Text.Trim())
                    sqlParam(6) = New SqlParameter("@createBy", Session("uID"))
                    sqlParam(7) = New SqlParameter("@createName", Session("uName"))
                    sqlParam(8) = New SqlParameter("@type", 1)
                    sqlParam(9) = New SqlParameter("@oldcode", oldCode)
                    sqlParam(10) = New SqlParameter("@oldqad", oldQad)

                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam)

                End If
                Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "&try=" & Request("try")), True)
            Else
                ltlAlert.Text = "alert('已经存在此编号，无法更新！');"
                Exit Sub
            End If
        End Sub

        Private Sub BtnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddNew.Click
            ltlAlert.Text = ""
            Dim minqty As Integer
            Dim cnt As Integer
            Dim catID As Integer = 0
            Dim numtype As Integer
            Dim numLS As Integer
            Dim numLS1 As Integer
            Dim numLSN As Integer
            Dim numLSN1 As Integer
            Dim numVer As Integer
            Dim numSubVer As Integer
            Dim strItem As String
           
            If radNormal.Checked = True Then
                numtype = 0
                If Not Me.Security("19070500").isValid Then
                    numtype = 1
                End If
            Else
                numtype = 1
            End If
            If txtCode.Text.Trim().Length <= 0 Then
                ltlAlert.Text = "alert('部件号不能为空！');"
                Exit Sub
            ElseIf txtCode.Text.Trim().Length > 50 Then
                ltlAlert.Text = "alert('部件号的最大长度为50个字符！');"
                Exit Sub
            ElseIf txtCode.Text.Trim().IndexOf(" ") <> -1 Or txtCode.Text.Trim().IndexOf("/") <> -1 Or txtCode.Text.Trim().IndexOf("_") <> -1 Or txtCode.Text.Trim().IndexOf("\") <> -1 Then
                ltlAlert.Text = "alert('部件号不能包含空格，斜杠，下划线！');"
                Exit Sub
            End If

            If txtQad.Text.Trim().Length > 14 Then
                ltlAlert.Text = "alert('QAD号的最大长度为14位！');"
                Exit Sub
            ElseIf Not IsNumber(txtQad.Text.Trim().ToString()) Then
                ltlAlert.Text = "alert('QAD号应为一串数字！');"
                Exit Sub
            End If

            Dim nt As String = txtDesc.Text.Trim()
            If (nt.Length > 255) Then
                ltlAlert.Text = "alert('文本最大长度为255个字符！');"
                Exit Sub
            End If
            If txtCategory.Text.Trim().Length <= 0 Then
                ltlAlert.Text = "alert('分类不能为空！');"
                Exit Sub
            ElseIf txtCategory.Text.Trim().Length > 10 Then
                ltlAlert.Text = "alert('分类的最大长度为10个字符！');"
                Exit Sub
            End If
            If Len(txtUnit.Text.Trim()) > 5 Then
                ltlAlert.Text = "alert('单位的最大长度为5个字符！');"
                Exit Sub
            End If
            If Len(txtUnit.Text.Trim()) > 5 Then
                ltlAlert.Text = "alert('单位的最大长度为5个字符！');"
                Exit Sub
            End If
            If Len(txtTranUnit.Text.Trim()) > 5 Then
                ltlAlert.Text = "alert('转换前单位的最大长度为5个字符！');"
                Exit Sub
            End If
            If txtMinQty.Text.Trim().Length <= 0 Then
                minqty = 0
            ElseIf IsNumeric(txtMinQty.Text.Trim()) = False Then
                ltlAlert.Text = "alert('最小库存量非数值，请重新输入！');"
                Exit Sub
            ElseIf Val(txtMinQty.Text.Trim()) < 0 Then
                ltlAlert.Text = "alert('最小库存量不能为负数，请重新输入！');"
                Exit Sub
            Else
                minqty = CInt(txtMinQty.Text.Trim())
            End If

            Dim rate As Decimal
            If txtRate.Text.Trim().Length <= 0 Then
                rate = 0
            ElseIf IsNumeric(txtRate.Text.Trim()) = False Then
                ltlAlert.Text = "alert('转换系数非数值，请重新输入！');"
                Exit Sub
            ElseIf Val(txtRate.Text.Trim()) <= 0 Then
                ltlAlert.Text = "alert('转换系数不能为零和负数，请重新输入！');"
                Exit Sub
            Else
                rate = CDbl(txtRate.Text.Trim())
            End If

            Dim strMpi As String
            If txtmpi.Text.Trim().Length > 20 Then
                ltlAlert.Text = "alert('条形码的最大长度为20位！');"
                Exit Sub
            ElseIf Not Regex.IsMatch(txtmpi.Text.Trim().ToString(), "^[A-Za-z0-9]*$") Then
                ltlAlert.Text = "alert('条形码只能是数字和字母！');"
                Exit Sub
            Else
                strMpi = txtmpi.Text.Trim().ToString()
            End If

             
            numLS = InStr(txtCode.Text.Trim().ToUpper(), "LS-")
            numLS1 = InStr(txtCode.Text.Trim().ToUpper(), "LS")
            numLSN = InStr(txtCode.Text.Trim().ToUpper(), "LSN")
            numLSN1 = InStr(txtCode.Text.Trim().ToUpper(), "LSN-")
            If numLS = 1 Then
                numVer = 0
                numSubVer = 0
                strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLS + 2))
                numLS1 = 0
                If numLS = 1 Then
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            ElseIf numLSN1 = 1 Then
                numVer = 0
                numSubVer = 0
                strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLSN1 + 2))
                numLS1 = 0
                If numLSN1 = 1 Then
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            ElseIf numLSN = 1 Then
                numLS = 0
                numLS = InStr(txtCode.Text.Trim().Substring(numLSN + 2).ToUpper(), "-")
                If numLS > 0 Then
                    If IsNumeric(Mid(txtCode.Text.Trim().Substring(numLSN + 2), 1, numLS - 1)) = True Then
                        numVer = Mid(txtCode.Text.Trim().Substring(numLSN + 2), 1, numLS - 1)
                        strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLSN + 2).Substring(numLS))
                    Else
                        numVer = 1
                        strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLSN + 2).Substring(numLS))
                        ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                        Exit Sub
                    End If
                    numLS = 0
                    numLS = InStr(strItem, "-")
                    If numLS > 0 Then
                        If IsNumeric(Mid(strItem.Trim(), 1, numLS - 1)) = True Then
                            numSubVer = Mid(strItem.Trim(), 1, numLS - 1)
                        Else
                            numSubVer = 1
                            ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                            Exit Sub
                        End If
                        strItem = strItem.Substring(numLS)
                    Else
                        numSubVer = 0
                        ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                        Exit Sub
                    End If
                Else
                    numVer = 1
                    numSubVer = 1
                    strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLSN + 2))
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            ElseIf numLS1 = 1 Then
                numLS = 0
                numLS = InStr(txtCode.Text.Trim().Substring(numLS1 + 1).ToUpper(), "-")
                If numLS > 0 Then
                    If IsNumeric(Mid(txtCode.Text.Trim().Substring(numLS1 + 1), 1, numLS - 1)) = True Then
                        numVer = Mid(txtCode.Text.Trim().Substring(numLS1 + 1), 1, numLS - 1)
                        strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLS1 + 1).Substring(numLS))
                    Else
                        numVer = 1
                        strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLS1 + 1).Substring(numLS))
                        ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                        Exit Sub
                    End If
                    numLS = 0
                    numLS = InStr(strItem, "-")
                    If numLS > 0 Then
                        If IsNumeric(Mid(strItem.Trim(), 1, numLS - 1)) = True Then
                            numSubVer = Mid(strItem.Trim(), 1, numLS - 1)
                        Else
                            numSubVer = 1
                            ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                            Exit Sub
                        End If
                        strItem = strItem.Substring(numLS)
                    Else
                        numSubVer = 0
                        ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                        Exit Sub
                    End If
                Else
                    numVer = 1
                    numSubVer = 1
                    strItem = chk.sqlEncode(txtCode.Text.Trim().Substring(numLS1 + 1))
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            Else
                numVer = 0
                numSubVer = 0
                strItem = chk.sqlEncode(txtCode.Text.Trim())
            End If

            strSQL = " Select Count(*) From Items Where code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "'"
            cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
            If cnt < 1 Then
                cnt = 0
                strSQL = " Select Count(*) From items_his Where code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "' Or old_code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If cnt <= 0 Then

                    If txtQad.Text.Trim().Length > 0 Then
                        cnt = 0
                        strSQL = " Select Count(*) From Items Where item_qad = N'" & txtQad.Text.Trim() & "'"
                        cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                        If cnt > 0 Then
                            ltlAlert.Text = "alert('QAD号被占用，无法添加！');"
                            Exit Sub
                        End If
                    End If

                    param(0) = New SqlParameter("@categoryName", chk.sqlEncode(txtCategory.Text.Trim()))
                    param(1) = New SqlParameter("@intUserID", Session("uID"))
                    param(2) = New SqlParameter("@type", "0")
                    param(3) = New SqlParameter("@intPlant", Session("plantCode"))
                    catID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_SetCategory", param)
                    If rate > 0 Then
                        strSQL = " Insert Into Items(code, description, category, status, min_inv, createdBy, createdDate, plantcode, type,item_qad, " _
                               & " unit, itemNumber, itemVersion, itemSubVersion, isUnique, tranUnit, tranRate, mpi ) " _
                               & " Values(N'" & chk.sqlEncode(txtCode.Text.Trim()) & "',N'" & chk.sqlEncode(txtDesc.Text.Trim()) & "','" & catID & "','" _
                               & numtype & "','" & minqty & "','" & Session("uID") & "','" & DateTime.Now() & "','" & Session("plantcode") & "','0','" & chk.sqlEncode(txtQad.Text.Trim()) & "',N'" _
                               & chk.sqlEncode(txtUnit.Text.Trim()) & "','" & strItem.Trim() & "','" & numVer & "','" & numSubVer & "',1, N'" _
                               & chk.sqlEncode(txtTranUnit.Text.Trim()) & "','" & rate & "','" & strMpi & "') Select @@Identity "
                    Else
                        strSQL = " Insert Into Items(code, description, category, status, min_inv, createdBy, createdDate, plantcode, type,item_qad, " _
                               & " unit, itemNumber, itemVersion, itemSubVersion, isUnique, tranUnit,tranRate, mpi) " _
                               & " Values(N'" & chk.sqlEncode(txtCode.Text.Trim()) & "',N'" & chk.sqlEncode(txtDesc.Text.Trim()) & "','" & catID & "','" _
                               & numtype & "','" & minqty & "','" & Session("uID") & "','" & DateTime.Now() & "','" & Session("plantcode") & "','0','" & chk.sqlEncode(txtQad.Text.Trim()) & "',N'" _
                               & chk.sqlEncode(txtUnit.Text.Trim()) & "','" & strItem.Trim() & "','" & numVer & "','" & numSubVer & "',1, N'" _
                               & chk.sqlEncode(txtTranUnit.Text.Trim()) & "','" & rate & "','" & strMpi & "') Select @@Identity "
                    End If
                    Dim id As String = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                    strSQL = " insert into items_his(item_id,code,description,category,old_code,old_description,old_category,createdby,createddate) " _
                           & " values('" & id.Trim() & "',N'" & chk.sqlEncode(txtCode.Text.Trim()) & "',N'" & chk.sqlEncode(txtDesc.Text.Trim()) & "','" & catID & "'," _
                           & " N'" & chk.sqlEncode(txtCode.Text.Trim()) & "',N'" & chk.sqlEncode(txtDesc.Text.Trim()) & "','" & catID & "','" & Session("uID") & "',getdate() )"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                    If Unnamed1_Click() = False Then
                        Exit Sub
                    End If
                    Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "&try=" & Request("try")), True)
                Else
                    ltlAlert.Text = "alert('已经存在此编号的历史记录，无法添加！');"
                    Exit Sub
                End If
            Else
                ltlAlert.Text = "alert('已经存在此编号，无法添加！');"
                Exit Sub
            End If
        End Sub

        Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
            ltlAlert.Text = ""
            Dim strSQL As String
            Dim cnt As Integer

            If Not Me.Security("19070102").isValid Then
                ltlAlert.Text = "alert('没有权限删除该部件！');"
                Exit Sub
            Else
                strSQL = " Select Count(*) From Product_stru ps Inner Join Items i On i.id=ps.productID And i.status<>2 Where ps.childID='" & Request("id") & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If cnt <= 0 Then
                    strSQL = " Select Count(*) From Product_replace Where itemID='" & Request("id") & "'"
                    cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                    If cnt <= 0 Then


                        Dim param(2) As SqlParameter
                        param(0) = New SqlParameter("@id", Request("id"))
                        param(1) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))

                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_product_deleteItemsById", param)

                        Response.Redirect(chk.urlRand("partlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "&try=" & Request("try")), True)
                    Else
                        ltlAlert.Text = "alert('仍有产品使用该部件无法删除！');"
                        Exit Sub
                    End If
                Else
                    ltlAlert.Text = "alert('仍有产品使用该部件无法删除！');"
                    Exit Sub
                End If
            End If
        End Sub

        Function Unnamed1_Click() As Boolean
            Dim bValid As Boolean = True
            Dim row As GridViewRow
            If bValid Then
                For Each row In gv.Rows
                    Dim chkSinger As CheckBox = row.FindControl("chkSinger")
                    Dim chkSinger150 As CheckBox = row.FindControl("chkSinger150")
                    Dim txtAmount As TextBox = row.FindControl("txtAmount")
                    Dim dbcontrl As DataBoundLiteralControl = row.Cells(0).Controls(0)
                    Dim err As String
                    If chkSinger.Checked Or chkSinger150.Checked Then
                        If txtQad.Text.Trim() = "" Then
                            ltlAlert.Text = "alert('无QAD时不能维护关联条目');"
                            bValid = False
                        End If
                        If txtAmount.Text.Trim().Length <> 0 Then
                            Try
                                Dim amount As Integer = Convert.ToInt32(txtAmount.Text.Trim())
                                If amount <= 0 Then
                                    bValid = False
                                    err = "行号:" + dbcontrl.Text.Trim() + "文档数必须大于0!"
                                    ltlAlert.Text = "alert('" + err + "');"
                                    Exit For
                                End If
                            Catch ex As Exception
                                bValid = False
                                err = "行号:" + dbcontrl.Text.Trim() + "文档数必须为数字格式!"
                                ltlAlert.Text = "alert('" + err + "');"
                                Exit For
                            End Try
                        Else
                            bValid = False
                            err = "行号:" + dbcontrl.Text.Trim() + "文档数不能为空!"
                            ltlAlert.Text = "alert('" + err + "');"
                            Exit For
                        End If
                    End If
                Next row
                If bValid Then
                    If TransTempData() Then
                        If TransData(Convert.ToInt32(Session("uID")), txtCode.Text.Trim(), Session("uName").ToString()) Then
                        Else
                            ltlAlert.Text = "alert('文档更新失败，请联系管理员!');"
                            bValid = False
                        End If
                    Else
                        ltlAlert.Text = "alert('文档保存到临时表失败,请联系管理员!');"
                        bValid = False
                    End If
                End If
            End If
            Return bValid
        End Function
        Private Sub BindData()
            gv.DataSource = SelectTrackingType(0)
            gv.DataBind()
        End Sub
        Function SelectTrackingType(ByVal type As Integer) As DataTable
            Try
                Dim dt As DataTable
                Dim param(4) As SqlParameter
                param(0) = New SqlParameter("@type", type)
                param(1) = New SqlParameter("@ptt_type", "--")
                param(2) = New SqlParameter("@ptt_detail", "")
                param(3) = New SqlParameter("@code", txtCode.Text.Trim())
                dt = SqlHelper.ExecuteDataset(adam.dsn0, CommandType.StoredProcedure, "sp_product_selectProductTrackingType", param).Tables(0)
                Return dt
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Function TransTempData() As Boolean
            If ClearTempData(Convert.ToInt32(Session("uID")), txtCode.Text.Trim()) Then
                Dim ProductTrackingItemTemp As New System.Data.DataTable("ProductTrackingItemTemp")
                Dim column As DataColumn

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "itm_code"
                ProductTrackingItemTemp.Columns.Add(column)

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.Int32")
                column.ColumnName = "itm_type"
                ProductTrackingItemTemp.Columns.Add(column)

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.Boolean")
                column.ColumnName = "itm_100"
                ProductTrackingItemTemp.Columns.Add(column)

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.Boolean")
                column.ColumnName = "itm_150"
                ProductTrackingItemTemp.Columns.Add(column)

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.Int32")
                column.ColumnName = "itm_Amount"
                ProductTrackingItemTemp.Columns.Add(column)

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.Int64")
                column.ColumnName = "itm_createBy"
                ProductTrackingItemTemp.Columns.Add(column)

                column = New DataColumn()
                column.DataType = System.Type.GetType("System.DateTime")
                column.ColumnName = "itm_createDate"
                ProductTrackingItemTemp.Columns.Add(column)
                Dim row As GridViewRow
                For Each row In gv.Rows
                    Dim chkSinger As CheckBox = row.FindControl("chkSinger")
                    Dim chkSinger150 As CheckBox = row.FindControl("chkSinger150")
                    Dim txtAmount As TextBox = row.FindControl("txtAmount")
                    If chkSinger.Checked Or chkSinger150.Checked Then
                        Dim amount As Integer = Convert.ToInt32(txtAmount.Text.Trim())
                        Dim tempRow As DataRow = ProductTrackingItemTemp.NewRow()
                        tempRow("itm_code") = txtCode.Text.Trim()
                        tempRow("itm_type") = Convert.ToInt32(gv.DataKeys(row.RowIndex).Value)
                        tempRow("itm_100") = chkSinger.Checked
                        tempRow("itm_150") = chkSinger150.Checked
                        tempRow("itm_Amount") = amount
                        tempRow("itm_createBy") = Convert.ToInt32(Session("uID"))
                        tempRow("itm_createDate") = DateTime.Now
                        ProductTrackingItemTemp.Rows.Add(tempRow)
                    End If
                Next row

                Using bulckCopy As SqlBulkCopy = New SqlBulkCopy(adam.dsn0, SqlBulkCopyOptions.UseInternalTransaction)
                    bulckCopy.DestinationTableName = "ProductTrackingItemTemp"
                    bulckCopy.ColumnMappings.Add("itm_code", "itm_code")
                    bulckCopy.ColumnMappings.Add("itm_type", "itm_type")
                    bulckCopy.ColumnMappings.Add("itm_100", "itm_100")
                    bulckCopy.ColumnMappings.Add("itm_150", "itm_150")
                    bulckCopy.ColumnMappings.Add("itm_Amount", "itm_Amount")
                    bulckCopy.ColumnMappings.Add("itm_createBy", "itm_createBy")
                    bulckCopy.ColumnMappings.Add("itm_createDate", "itm_createDate")
                    Try
                        bulckCopy.WriteToServer(ProductTrackingItemTemp)

                    Catch ex As Exception
                        Return False
                    Finally
                        ProductTrackingItemTemp.Dispose()
                    End Try
                    Return True
                End Using

            Else
                Return False
            End If
        End Function

        Function ClearTempData(ByVal createBy As Integer, ByVal code As String) As Boolean
            Try
                Dim param(3) As SqlParameter
                param(0) = New SqlParameter("@createBy", createBy)
                param(1) = New SqlParameter("@code", code)
                param(2) = New SqlParameter("@retValue", SqlDbType.Bit)
                param(2).Direction = ParameterDirection.Output
                SqlHelper.ExecuteNonQuery(adam.dsn0, CommandType.StoredProcedure, "sp_product_deleteProductTrackingItemTemp", param)
                Return Convert.ToBoolean(param(2).Value)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function TransData(ByVal createBy As Integer, ByVal code As String, ByVal uName As String) As Boolean
            Try
                Dim param(4) As SqlParameter
                param(0) = New SqlParameter("@create", createBy)
                param(1) = New SqlParameter("@code", code)
                param(2) = New SqlParameter("@uName", uName)
                param(3) = New SqlParameter("@retValue", SqlDbType.Bit)
                param(3).Direction = ParameterDirection.Output
                SqlHelper.ExecuteNonQuery(adam.dsn0, CommandType.StoredProcedure, "sp_product_transDataToProductTrackingItem", param)
                Return Convert.ToBoolean(param(3).Value)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Protected Sub gv_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim chkSinger As CheckBox = e.Row.FindControl("chkSinger")
                If Convert.ToBoolean(gv.DataKeys(e.Row.RowIndex).Values("isExists")) Then
                    chkSinger.Checked = True
                End If

                Dim chkSinger150 As CheckBox = e.Row.FindControl("chkSinger150")
                If Convert.ToBoolean(gv.DataKeys(e.Row.RowIndex).Values("isExists150")) Then
                    chkSinger150.Checked = True
                End If
            End If

        End Sub

    End Class

End Namespace
