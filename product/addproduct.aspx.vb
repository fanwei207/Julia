'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-10
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Add New Product
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class addproduct
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim strSQL As String
        Shared oldDesc As String = ""
        Shared oldCode As String = ""
        Shared oldQad As String = ""
        'Protected WithEvents chkSemi As System.Web.UI.WebControls.CheckBox
        Dim nRet As Integer
        Dim adam As New adamClass()
        Dim flagchange As Boolean  '标记修改时，QAD前后有没有改动


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents statusID As System.Web.UI.WebControls.DropDownList
        Protected WithEvents pcode As System.Web.UI.WebControls.TextBox


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then
                If Not Me.Security("19030800").isValid Then
                    radStop.Enabled = False
                    radNormal.Enabled = False
                    radTry.Enabled = False
                End If

                If Request("id") = "" Then
                    BtnAddNew.Visible = True
                    radStop.Enabled = False
                    BtnDelete.Visible = False
                    ltlAlert.Text = "Form1.gcode.focus();"
                Else
                    Dim reader As SqlDataReader
                    strSQL = " Select i.id, i.code,isnull(i.description,''), isnull(ic.name,''), i.status, Isnull(i.min_inv,0), Isnull(c.company_code,''), Isnull(i.simpleCode,''), i.item_qad, i.mpi " _
                       & " From Items i Left Outer Join ItemCategory ic On ic.id=i.category " _
                       & " Left Outer Join users u On u.userid=i.createdBy " _
                       & " Left Outer Join Companies c On c.company_id=i.customerID " _
                       & " Where i.id =" & Request("id")
                    reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL)
                    While (reader.Read())
                        gcode.Text = reader(1).ToString().Trim()
                        txtCat.Text = reader(3).ToString().Trim()
                        note.Text = reader(2).ToString().Trim()
                        oldDesc = reader(2).ToString().Trim()
                        oldCode = reader(1).ToString().Trim()
                        oldQad = reader(8).ToString().Trim()
                        txtCustomer.Text = reader(6).ToString().Trim()
                        If reader(4) = "2" Then
                            radStop.Checked = True
                            radTry.Checked = False
                            radNormal.Checked = False
                        ElseIf reader(4) = "1" Then
                            radStop.Checked = False
                            radTry.Checked = True
                            radNormal.Checked = False
                        Else
                            radStop.Checked = False
                            radTry.Checked = False
                            radNormal.Checked = True
                        End If
                        txtMinQty.Text = reader(5).ToString().Trim()
                        txtSimple.Text = reader(7).ToString().Trim()
                        txtQad.Text = reader("item_qad").ToString().Trim()
                        hidQad.Value = reader("item_qad").ToString().Trim()
                        txtmpi.Text = reader("mpi").ToString().Trim()
                    End While
                    reader.Close()
                    BtnModify.Visible = True
                    BtnDelete.Visible = True



                    If Session("uRole") = 1 Then
                        BtnDelete.Visible = True
                        gcode.Enabled = True
                    Else
                        BtnDelete.Visible = Me.Security("19030102").isValid
                        'gcode.Enabled = False
                        gcode.Enabled = True
                    End If
                End If
                'If Request("semi") = "true" Then
                '    chkSemi.Checked = True
                'Else
                '    chkSemi.Checked = False
                'End If
                BtnDelete.Attributes.Add("onclick", "return confirm('确定要删除该产品吗？');")
                BindData()


            End If
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            Me.Redirect("/product/productlist.aspx?code=" & Request("code") & "&qad=" & Request("qad") & "&cat=" & Request("cat"))

        End Sub

        Private Sub BtnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModify.Click
            Dim nt As String = note.Text.Trim()
            Dim cnt, catID, numLS, numLS1, numVer, numType, st, numSubVer, numLSN, numLSN1 As Integer
            Dim param(3) As SqlParameter
            Dim strItem As String
            Dim strSimple As String = txtSimple.Text.Trim()
            Dim strMpi As String

            If (nt.Length > 255) Then
                ltlAlert.Text = "alert('备注文本最大长度为255个字符！');"
                Exit Sub
            End If
            If gcode.Text.Trim().Length <= 0 Then
                ltlAlert.Text = "alert('产品型号不能为空！');"
                Exit Sub
            ElseIf gcode.Text.Trim().Length > 50 Then
                ltlAlert.Text = "alert('产品型号的最大长度为50个字符！');"
                Exit Sub
            ElseIf gcode.Text.Trim().IndexOf(" ") <> -1 Or gcode.Text.Trim().IndexOf("/") <> -1 Or gcode.Text.Trim().IndexOf("_") <> -1 Or gcode.Text.Trim().IndexOf("\") <> -1 Then
                ltlAlert.Text = "alert('产品型号不能包含空格，斜杠，下划线！');"
                Exit Sub
            End If

            If txtQad.Text.Trim().Length > 14 Then
                ltlAlert.Text = "alert('QAD号的最大长度为14位！');"
                Exit Sub
            ElseIf Not IsNumber(txtQad.Text.Trim().ToString()) Then
                ltlAlert.Text = "alert('QAD号应为一串数字！');"
                Exit Sub
            End If

            If txtCat.Text.Trim().Length <= 0 Then
                ltlAlert.Text = "alert('分类不能为空！');"
                Exit Sub
            ElseIf txtCat.Text.Trim().Length > 10 Then
                ltlAlert.Text = "alert('分类的最大长度为10个字符！');"
                Exit Sub
            End If
            If txtCustomer.Text.Trim().Length > 30 Then
                ltlAlert.Text = "alert('所属客户代码的最大长度为30个字符！');"
                Exit Sub
            End If
            If strSimple.Trim().Length > 20 Then
                ltlAlert.Text = "alert('产品简称的最大长度为20个字符！');"
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

            If txtQad.Text.Trim() <> hidQad.Value Then
                flagchange = True
            Else
                flagchange = False
            End If

            If radStop.Checked = True Then
                st = 2
            ElseIf radTry.Checked = True Then
                st = 1
            Else
                st = 0
                If Not Me.Security("19030800").isValid Then
                    st = 1
                End If
            End If

            numType = 2

            Dim custID As String = Nothing
            If txtCustomer.Text.Trim().Length > 0 Then
                strSQL = " Select c.company_id From Companies c " _
                       & " Inner Join systemCode sc On sc.systemCodeID=c.company_type And sc.systemCodeName=N'客户'" _
                       & " Inner Join systemCodeType sct On sc.systemCodeTypeID=sct.systemCodeTypeID And sct.systemCodeTypeName='Company Type' " _
                       & " Where c.company_code='" & chk.sqlEncode(txtCustomer.Text.Trim()) & "' And c.deleted=0 And c.active=1 "
                custID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If custID = Nothing Then
                    ltlAlert.Text = "alert('所属客户代码不存在或者该客户无效！');"
                    Exit Sub
                End If
            Else
                custID = "0"
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

            If Not Me.Security("19030502").isValid Then
                strSimple = ""
            End If

            If st = 2 Then
                strSQL = " Select Count(ps.productID) From Product_stru ps Inner Join Items i On i.ID=ps.productID And i.status<>2 Where childID='" & Request("id") & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If cnt > 0 Then
                    ltlAlert.Text = "alert('仍有产品使用该产品无法停用！');"
                    Exit Sub
                End If
                strSQL = " Select Count(ps.productID) From Product_replace pr Inner Join Product_stru ps On pr.prodStruID=ps.productStruID " _
                       & " Inner Join Items i On i.ID=ps.productID And i.status<>2  " _
                       & " Where pr.itemID='" & Request("id") & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If cnt > 0 Then
                    ltlAlert.Text = "alert('仍有产品使用该产品无法停用！');"
                    Exit Sub
                End If
            ElseIf st = 0 Or st = 1 Then
                strSQL = " Select Count(ps.childID) From Product_stru ps Inner Join Items i On i.ID=ps.childID And i.status=2 Where productID='" & Request("id") & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If cnt > 0 Then
                    ltlAlert.Text = "alert('该产品仍有使用停用的材料无法启用！');"
                    Exit Sub
                End If
                strSQL = " Select Count(pr.itemID) From Product_replace pr Inner Join Product_stru ps On pr.prodStruID=ps.productStruID " _
                       & " Inner Join Items i On i.ID=pr.itemID And i.status=2  " _
                       & " Where ps.productID='" & Request("id") & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If cnt > 0 Then
                    ltlAlert.Text = "alert('该产品仍有使用停用的材料无法启用！');"
                    Exit Sub
                End If
            End If

            param(0) = New SqlParameter("@categoryName", chk.sqlEncode(txtCat.Text.Trim()))
            param(1) = New SqlParameter("@intUserID", Session("uID"))
            param(2) = New SqlParameter("@type", "2")
            param(3) = New SqlParameter("@intPlant", Session("plantCode"))
            catID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_SetCategory", param)

            numLS = InStr(gcode.Text.Trim().ToUpper(), "LS-")
            numLS1 = InStr(gcode.Text.Trim().ToUpper(), "LS")
            numLSN = InStr(gcode.Text.Trim().ToUpper(), "LSN")
            numLSN1 = InStr(gcode.Text.Trim().ToUpper(), "LSN-")
            If numLS = 1 Then
                numVer = 0
                numSubVer = 0
                strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLS + 2))
                numLS1 = 0
                If numLS = 1 Then
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            ElseIf numLSN1 = 1 Then
                numVer = 0
                numSubVer = 0
                strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLSN1 + 2))
                numLS1 = 0
                If numLSN1 = 1 Then
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            ElseIf numLSN = 1 Then
                numLS = 0
                numLS = InStr(gcode.Text.Trim().Substring(numLSN + 2).ToUpper(), "-")
                If numLS > 0 Then
                    If IsNumeric(Mid(gcode.Text.Trim().Substring(numLSN + 2), 1, numLS - 1)) = True Then
                        numVer = Mid(gcode.Text.Trim().Substring(numLSN + 2), 1, numLS - 1)
                        strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLSN + 2).Substring(numLS))
                    Else
                        numVer = 1
                        strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLSN + 2).Substring(numLS))
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
                    strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLSN + 2))
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            ElseIf numLS1 = 1 Then
                numLS = 0
                numLS = InStr(gcode.Text.Trim().Substring(numLS1 + 1).ToUpper(), "-")
                If numLS > 0 Then
                    If IsNumeric(Mid(gcode.Text.Trim().Substring(numLS1 + 1), 1, numLS - 1)) = True Then
                        numVer = Mid(gcode.Text.Trim().Substring(numLS1 + 1), 1, numLS - 1)
                        strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLS1 + 1).Substring(numLS))
                    Else
                        numVer = 1
                        strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLS1 + 1).Substring(numLS))
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
                    strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLS1 + 1))
                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                    Exit Sub
                End If
            Else
                numVer = 0
                numSubVer = 0
                strItem = chk.sqlEncode(gcode.Text.Trim())
            End If

            If txtQad.Text.Trim().Length > 0 Then
                cnt = 0
                strSQL = " Select Count(*) From Items Where status<>2 and id <> " & Request("id") & " And item_qad = N'" & txtQad.Text.Trim() & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                If cnt > 0 Then
                    ltlAlert.Text = "alert('QAD号被占用，无法修改！');"
                    Exit Sub
                End If
            End If
            '整灯必须勾选丝印和包装工艺
            '裸灯必须勾选丝印、包装工艺、认证证书
            Dim re As Int32
            re = RelatedItemConstraint()

            If re = 0 Then Exit Sub

            '''''''''''Sava product history
            Dim reader1 As SqlDataReader
            strSQL = " Select i.id, i.code,isnull(i.description,''), isnull(ic.name,''), ic.id " _
                   & " From Items i Left Outer Join ItemCategory ic On ic.id=i.category " _
                   & " Left Outer Join Companies c On c.company_id=i.customerID " _
                   & " Where i.id =" & Request("id")
            reader1 = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL)
            While (reader1.Read())
                strSQL = " insert into items_his(item_id,code,description,category,old_code,old_description,old_category,createdby,createddate) " _
                         & " values('" & reader1(0) & "',N'" & chk.sqlEncode(gcode.Text.Trim()) & "',N'" & chk.sqlEncode(note.Text.Trim()) & "','" & catID & "', " _
                         & " N'" & reader1(1) & "',N'" & reader1(2) & "','" & reader1(4) & "','" & Session("uID") & "',getdate() )"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
            End While
            reader1.Close()

            strSQL = " Select Count(*) From Items Where code=N'" & chk.sqlEncode(gcode.Text.Trim()) & "' And id<>" & Request("id")
            cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
            If cnt < 1 Then
                If Request("semi") = "true" Then
                    If Me.Security("19030900").isValid Then
                        strSQL = " Update Items Set code=N'" & chk.sqlEncode(gcode.Text.Trim()) & "'," _
                               & " description=N'" & chk.sqlEncode(note.Text.Trim()) & "'," _
                               & " category='" & catID & "'," _
                               & " min_inv='" & minqty & " '," _
                               & " itemVersion=" & numVer & "," _
                               & " itemNumber=N'" & strItem & "'," _
                               & " itemSubVersion='" & numSubVer & "',"
                        If flagchange Then
                            strSQL &= "  item_qad_old = item_qad ,"
                            strSQL &= "  item_qad='" & chk.sqlEncode(txtQad.Text.Trim()) & "',"
                        End If
                        strSQL &= " modifiedBy='" & Session("uID") & "'," _
                               & " modifiedDate='" & DateTime.Now() & "'," _
                               & " mpi='" & strMpi & "'," _
                               & " plantcode='" & Session("plantCode") & "'," _
                               & " customerID='" & custID.Trim() & "'," _
                               & " status='" & st & "'," _
                               & " type='" & numType & "'" _
                               & " Where id= " & Request("id")
                    Else
                        strSQL = " Update Items Set code=N'" & chk.sqlEncode(gcode.Text.Trim()) & "'," _
                               & " description=N'" & chk.sqlEncode(note.Text.Trim()) & "'," _
                               & " category='" & catID & "'," _
                               & " min_inv='" & minqty & " '," _
                               & " itemVersion=" & numVer & "," _
                               & " itemNumber=N'" & strItem & "'," _
                               & " itemSubVersion='" & numSubVer & "',"
                        If flagchange Then
                            strSQL &= "  item_qad_old = item_qad ,"
                            strSQL &= "  item_qad='" & chk.sqlEncode(txtQad.Text.Trim()) & "',"
                        End If
                        strSQL &= " modifiedBy='" & Session("uID") & "'," _
                           & " modifiedDate='" & DateTime.Now() & "'," _
                            & " mpi='" & strMpi & "'," _
                           & " plantcode='" & Session("plantCode") & "'," _
                           & " customerID='" & custID.Trim() & "'," _
                           & " type='" & numType & "'" _
                           & " Where id= " & Request("id")

                    End If
                Else
                    If Me.Security("19030800").isValid Then
                        strSQL = " Update Items Set code=N'" & chk.sqlEncode(gcode.Text.Trim()) & "'," _
                               & " simpleCode=N'" & chk.sqlEncode(strSimple.Trim()) & "'," _
                               & " description=N'" & chk.sqlEncode(note.Text.Trim()) & "'," _
                               & " category='" & catID & "'," _
                               & " min_inv='" & minqty & " '," _
                               & " itemVersion=" & numVer & "," _
                               & " itemNumber=N'" & strItem & "'," _
                               & " itemSubVersion='" & numSubVer & "',"
                        If flagchange Then
                            strSQL &= "  item_qad_old = item_qad ,"
                            strSQL &= "  item_qad='" & chk.sqlEncode(txtQad.Text.Trim()) & "',"
                        End If
                        strSQL &= " modifiedBy='" & Session("uID") & "'," _
                           & " modifiedDate='" & DateTime.Now() & "'," _
                            & " mpi='" & strMpi & "'," _
                           & " status='" & st & "'," _
                           & " plantcode='" & Session("plantCode") & "'," _
                           & " customerID='" & custID.Trim() & "'," _
                           & " type='" & numType & "'" _
                           & " Where id= " & Request("id")
                    Else
                        strSQL = " Update Items Set code=N'" & chk.sqlEncode(gcode.Text.Trim()) & "'," _
                               & " simpleCode=N'" & chk.sqlEncode(strSimple.Trim()) & "'," _
                               & " description=N'" & chk.sqlEncode(note.Text.Trim()) & "'," _
                               & " category='" & catID & "'," _
                               & " min_inv='" & minqty & " '," _
                               & " itemVersion=" & numVer & "," _
                               & " itemNumber=N'" & strItem & "'," _
                              & " itemSubVersion='" & numSubVer & "',"
                        If flagchange Then
                            strSQL &= "  item_qad_old = item_qad ,"
                            strSQL &= "  item_qad='" & chk.sqlEncode(txtQad.Text.Trim()) & "',"
                        End If
                        strSQL &= " modifiedBy='" & Session("uID") & "'," _
                           & " modifiedDate='" & DateTime.Now() & "'," _
                            & " mpi='" & strMpi & "'," _
                           & " plantcode='" & Session("plantCode") & "'," _
                           & " customerID='" & custID.Trim() & "'," _
                           & " type='" & numType & "'" _
                           & " Where id= " & Request("id")
                    End If
                End If

                If (txtQad.Text.Trim().Length > 0) Then
                    strSQL &= "  Update ProductTrackingItem Set itm_qad = '" & txtQad.Text.Trim() & "' Where itm_code = '" & gcode.Text.Trim() & "' And itm_qad <> '" & txtQad.Text.Trim() & "'"
                    strSQL &= "  Update ProductTrackingItemScms Set itm_qad = '" & txtQad.Text.Trim() & "' Where itm_code = '" & gcode.Text.Trim() & "' And itm_qad <> '" & txtQad.Text.Trim() & "'"
                End If

                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                If Unnamed1_Click() = False Then
                    Exit Sub
                End If

                '如果修改了description（note）就保存到items_confirm表中
                If (String.Compare(oldDesc, note.Text.Trim()) Or oldCode.CompareTo(gcode.Text) <> 0 Or oldQad.CompareTo(txtQad.Text) <> 0) Then
                    strSQL = "sp_part_insertItemsConfirm"
                    Dim sqlParam(11) As SqlParameter
                    sqlParam(0) = New SqlParameter("@itemsCode", gcode.Text.Trim())
                    sqlParam(1) = New SqlParameter("@itemsQad", txtQad.Text.Trim())
                    sqlParam(2) = New SqlParameter("@oldUnit", "")
                    sqlParam(3) = New SqlParameter("@newUnit", "")
                    sqlParam(4) = New SqlParameter("@oldDesc", oldDesc)
                    sqlParam(5) = New SqlParameter("@newDesc", note.Text.Trim())
                    sqlParam(6) = New SqlParameter("@createBy", Session("uID"))
                    sqlParam(7) = New SqlParameter("@createName", Session("uName"))
                    sqlParam(8) = New SqlParameter("@type", 10)
                    sqlParam(9) = New SqlParameter("@oldcode", oldCode)
                    sqlParam(10) = New SqlParameter("@oldqad", oldQad)

                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSQL, sqlParam)
                End If

                Me.Redirect("/product/productlist.aspx")
            Else
                ltlAlert.Text = "alert('已经存在此编号，无法更新！');"
                Exit Sub
            End If

        End Sub

        Private Sub BtnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddNew.Click
            ltlAlert.Text = ""
            Dim nt As String = note.Text.Trim()
            Dim cnt, catID, numLS, numLS1, numVer, numType, st, numSubVer, numLSN, numLSN1 As Integer
            Dim param(3) As SqlParameter
            Dim strItem As String
            Dim strSimple As String = txtSimple.Text.Trim()

            If (nt.Length > 255) Then
                ltlAlert.Text = "alert('备注文本最大长度为255个字符！');"
                Exit Sub
            End If

            If gcode.Text.Trim().Length <= 0 Then
                ltlAlert.Text = "alert('产品型号不能为空！');"
                Exit Sub
            ElseIf gcode.Text.Trim().Length > 50 Then
                ltlAlert.Text = "alert('产品型号的最大长度为50个字符！');"
                Exit Sub
            ElseIf gcode.Text.Trim().IndexOf(" ") <> -1 Or gcode.Text.Trim().IndexOf("/") <> -1 Or gcode.Text.Trim().IndexOf("_") <> -1 Or gcode.Text.Trim().IndexOf("\") <> -1 Then
                ltlAlert.Text = "alert('产品型号不能包含空格，斜杠，下划线！');"
                Exit Sub
            End If

            If txtQad.Text.Trim().Length > 14 Then
                ltlAlert.Text = "alert('QAD号的最大长度为14位！');"
                Exit Sub
            ElseIf Not IsNumber(txtQad.Text.Trim().ToString()) Then
                ltlAlert.Text = "alert('QAD号应为一串数字！');"
                Exit Sub
            End If

            If txtCat.Text.Trim().Length <= 0 Then
                ltlAlert.Text = "alert('分类不能为空！');"
                Exit Sub
            ElseIf txtCat.Text.Trim().Length > 10 Then
                ltlAlert.Text = "alert('分类的最大长度为10个字符！');"
                Exit Sub
            End If
            If txtCustomer.Text.Trim().Length > 30 Then
                ltlAlert.Text = "alert('所属客户代码的最大长度为30个字符！');"
                Exit Sub
            End If
            If strSimple.Trim().Length > 20 Then
                ltlAlert.Text = "alert('产品简称的最大长度为20个字符！');"
                Exit Sub
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

            If radStop.Checked = True Then
                st = 2
            ElseIf radTry.Checked = True Then
                st = 1
            Else
                st = 0
            End If

            numType = 2

            If Not Me.Security("19030502").isValid Then
                strSimple = ""
            End If

            Dim custID As String = Nothing
            If txtCustomer.Text.Trim().Length > 0 Then
                strSQL = " Select c.company_id From Companies c " _
                       & " Inner Join systemCode sc On sc.systemCodeID=c.company_type And sc.systemCodeName=N'客户'" _
                       & " Inner Join systemCodeType sct On sc.systemCodeTypeID=sct.systemCodeTypeID And sct.systemCodeTypeName='Company Type' " _
                       & " Where c.company_code='" & chk.sqlEncode(txtCustomer.Text.Trim()) & "' And c.deleted=0 And c.active=1 "
                custID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If custID = Nothing Then
                    ltlAlert.Text = "alert('所属客户代码不存在或者该客户无效！');"
                    Exit Sub
                End If
            Else
                custID = "0"
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

            strSQL = " Select Count(*) From Items Where code=N'" & chk.sqlEncode(gcode.Text.Trim()) & "'"
            cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
            If cnt < 1 Then
                cnt = 0
                strSQL = " Select Count(*) From items_his Where code=N'" & chk.sqlEncode(gcode.Text.Trim()) & "' Or old_code=N'" & chk.sqlEncode(gcode.Text.Trim()) & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                If cnt <= 0 Then
                    param(0) = New SqlParameter("@categoryName", chk.sqlEncode(txtCat.Text.Trim()))
                    param(1) = New SqlParameter("@intUserID", Session("uID"))
                    param(2) = New SqlParameter("@type", "2")
                    param(3) = New SqlParameter("@intPlant", Session("plantCode"))
                    catID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_SetCategory", param)

                    numLS = InStr(gcode.Text.Trim().ToUpper(), "LS-")
                    numLS1 = InStr(gcode.Text.Trim().ToUpper(), "LS")
                    numLSN = InStr(gcode.Text.Trim().ToUpper(), "LSN")
                    numLSN1 = InStr(gcode.Text.Trim().ToUpper(), "LSN-")
                    If numLS = 1 Then
                        numVer = 0
                        numSubVer = 0
                        strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLS + 2))
                        numLS1 = 0
                        If numLS = 1 Then
                            ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                            Exit Sub
                        End If
                    ElseIf numLSN1 = 1 Then
                        numVer = 0
                        numSubVer = 0
                        strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLSN1 + 2))
                        numLS1 = 0
                        If numLSN1 = 1 Then
                            ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                            Exit Sub
                        End If
                    ElseIf numLSN = 1 Then
                        numLS = 0
                        numLS = InStr(gcode.Text.Trim().Substring(numLSN + 2).ToUpper(), "-")
                        If numLS > 0 Then
                            If IsNumeric(Mid(gcode.Text.Trim().Substring(numLSN + 2), 1, numLS - 1)) = True Then
                                numVer = Mid(gcode.Text.Trim().Substring(numLSN + 2), 1, numLS - 1)
                                strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLSN + 2).Substring(numLS))
                            Else
                                numVer = 1
                                strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLSN + 2).Substring(numLS))
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
                            strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLSN + 2))
                            ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                            Exit Sub
                        End If
                    ElseIf numLS1 = 1 Then
                        numLS = 0
                        numLS = InStr(gcode.Text.Trim().Substring(numLS1 + 1).ToUpper(), "-")
                        If numLS > 0 Then
                            If IsNumeric(Mid(gcode.Text.Trim().Substring(numLS1 + 1), 1, numLS - 1)) = True Then
                                numVer = Mid(gcode.Text.Trim().Substring(numLS1 + 1), 1, numLS - 1)
                                strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLS1 + 1).Substring(numLS))
                            Else
                                numVer = 1
                                strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLS1 + 1).Substring(numLS))
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
                            strItem = chk.sqlEncode(gcode.Text.Trim().Substring(numLS1 + 1))
                            ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                            Exit Sub
                        End If
                    Else
                        numVer = 0
                        numSubVer = 0
                        strItem = chk.sqlEncode(gcode.Text.Trim())
                    End If

                    If txtQad.Text.Trim().Length > 0 Then
                        cnt = 0
                        strSQL = " Select Count(*) From Items Where item_qad = N'" & txtQad.Text.Trim() & "'"
                        cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                        If cnt > 0 Then
                            ltlAlert.Text = "alert('QAD号被占用，无法添加！');"
                            Exit Sub
                        End If
                    End If

                    '整灯必须勾选丝印和包装工艺
                    '裸灯必须勾选丝印、包装工艺、认证证书
                    Dim re As Int32
                    re = RelatedItemConstraint()

                    If re = 0 Then Exit Sub

                    strSQL = " Insert Into Items(code, item_qad, description, category, status, type, plantCode, itemNumber, itemVersion, " _
                           & " createdBy, createdDate, customerID, itemSubVersion, min_inv, isUnique, simpleCode,mpi) " _
                           & " Values(N'" & chk.sqlEncode(gcode.Text.Trim()) & "','" & chk.sqlEncode(txtQad.Text.Trim()) & "',N'" & chk.sqlEncode(note.Text.Trim()) & "','" _
                           & catID & "','" & st & "','" & numType & "','" & Session("plantCode") & "','" & strItem & "','" & numVer _
                           & "','" & Session("uID") & "','" & DateTime.Now() & "','" & custID.Trim() & "','" & numSubVer _
                           & "','" & minqty & "',1,N'" & chk.sqlEncode(strSimple.Trim()) & "','" & strMpi & "') Select @@IDentity "
                    Dim id As String = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                    strSQL = " insert into items_his(item_id,code,description,category,old_code,old_description,old_category,createdby,createddate) " _
                             & " values('" & id.Trim() & "',N'" & chk.sqlEncode(gcode.Text.Trim()) & "',N'" & chk.sqlEncode(note.Text.Trim()) & "','" & catID & "'," _
                             & " N'" & chk.sqlEncode(gcode.Text.Trim()) & "',N'" & chk.sqlEncode(note.Text.Trim()) & "','" & catID & "','" & Session("uID") & "',getdate() )"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                    If Unnamed1_Click() = False Then
                        Exit Sub
                    End If
                    Response.Redirect(chk.urlRand("productlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "&try=" & Request("try")), True)
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
            Dim cnt As Integer

            If Not Me.Security("19030102").isValid Then
                ltlAlert.Text = "alert('没有权限删除该产品！');"
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

                        Response.Redirect(chk.urlRand("productlist.aspx?code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "&try=" & Request("try")), True)
                    Else
                        ltlAlert.Text = "alert('仍有产品使用该产品无法删除！');"
                        Exit Sub
                    End If
                Else
                    ltlAlert.Text = "alert('仍有产品使用该产品无法删除！');"
                    Exit Sub
                End If
            End If
        End Sub

        Function Unnamed1_Click() As Boolean
            
            Dim bValid As Boolean = True
            Dim chkNum As Integer = 0
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
                        If TransData(Convert.ToInt32(Session("uID")), gcode.Text.Trim(), Session("uName").ToString()) Then
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
                param(3) = New SqlParameter("@code", gcode.Text.Trim())
                dt = SqlHelper.ExecuteDataset(adam.dsn0, CommandType.StoredProcedure, "sp_product_selectProductTrackingType", param).Tables(0)
                Return dt
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Function TransTempData() As Boolean
            If ClearTempData(Convert.ToInt32(Session("uID")), gcode.Text.Trim()) Then
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
                        tempRow("itm_code") = gcode.Text.Trim()
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
        '整灯必须勾选丝印和包装工艺
        '裸灯必须勾选丝印、包装工艺、认证证书
        Function RelatedItemConstraint() As Int32
            Dim flag As Int32 = 0
            Dim flag2 As Int32 = 0
            Dim flag3 As Int32 = 0
            If txtQad.Text().StartsWith("1") Then
                If txtQad.Text().EndsWith("0") Then
                    Dim row As GridViewRow
                    For Each row In gv.Rows
                        Dim chkSinger As CheckBox = row.FindControl("chkSinger")
                        REM 整灯必须勾选丝印和包装工艺


                        If Convert.ToInt32(gv.DataKeys(row.RowIndex).Values("ptt_id")) = 1 Then
                            If chkSinger.Checked Then
                                flag = 1
                            End If
                        End If
                        If Convert.ToInt32(gv.DataKeys(row.RowIndex).Values("ptt_id")) = 2 Then
                            If chkSinger.Checked Then
                                flag2 = 1
                            End If
                        End If
                    Next row
                    If flag = 0 Or flag2 = 0 Then
                        ltlAlert.Text = "alert('整灯必须勾选丝印和包装工艺，否则无法添加！');"
                        Return 0
                        Exit Function
                    End If
                ElseIf txtQad.Text().EndsWith("1") Then
                    Dim row As GridViewRow
                    For Each row In gv.Rows
                        Dim chkSinger As CheckBox = row.FindControl("chkSinger")
                        REM 裸灯必须勾选丝印、包装工艺、认证证书

                        If Convert.ToInt32(gv.DataKeys(row.RowIndex).Values("ptt_id")) = 3 Then
                            If chkSinger.Checked Then
                                flag = 1
                            End If
                        End If
                        If Convert.ToInt32(gv.DataKeys(row.RowIndex).Values("ptt_id")) = 4 Then
                            If chkSinger.Checked Then
                                flag2 = 1
                            End If
                        End If
                        If Convert.ToInt32(gv.DataKeys(row.RowIndex).Values("ptt_id")) = 22 Then
                            If chkSinger.Checked Then
                                flag3 = 1
                            End If
                        End If
                    Next row
                    If flag = 0 Or flag2 = 0 Then
                        ltlAlert.Text = "alert('裸灯必须同时勾选裸灯标准、裸灯工艺，（美国产品必选认证证书），无法添加！');"
                        Return 0
                        Exit Function
                    End If
                End If
            End If
            Return 1
        End Function

    End Class

End Namespace
