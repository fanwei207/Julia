'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-7
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Product Structure From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


    Partial Class productStruImport
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim strSQL As String

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
            If Not (IsPostBack) Then

                filetypeDDL.SelectedIndex = 0
                Dim item As ListItem
                item = New ListItem("Excel (.xls) file")
                item.Value = 0
                filetypeDDL.Items.Add(item)
            End If
        End Sub

        Public Sub uploadBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadBtn.ServerClick
            If (Session("uID") = Nothing) Then
                Exit Sub
            End If
            ImportExcelFile()
        End Sub

        Private Sub ImportExcelFile()
            Dim ds As DataSet
            Dim bBomError As Boolean = False
            Dim param(1) As SqlParameter
            Dim retValue As Integer = -1

            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer

            strCatFolder = Server.MapPath("/import")
            If Not Directory.Exists(strCatFolder) Then
                Try
                    Directory.CreateDirectory(strCatFolder)
                Catch
                    ltlAlert.Text = "alert('上传文件失败.')"
                    Return
                End Try
            End If

            strUserFileName = filename.PostedFile.FileName
            intLastBackslash = strUserFileName.LastIndexOf("\")
            strFileName = strUserFileName.Substring(intLastBackslash + 1)
            If (strFileName.Trim().Length <= 0) Then
                ltlAlert.Text = "alert('请选择导入文件.')"
                Return
            End If

            strUserFileName = strFileName 'file name without path

            Dim i As Integer = 0
            While (i < 1000)
                strFileName = strCatFolder & "\f" & i.ToString() & strUserFileName
                If Not (File.Exists(strFileName)) Then
                    Exit While
                End If
                i = i + 1
            End While

            If Not (filename.PostedFile Is Nothing) Then
                If (filename.PostedFile.ContentLength > 8388608) Then
                    ltlAlert.Text = "alert('上传的文件最大为 8 MB.')"
                    Return
                End If
                Try
                    filename.PostedFile.SaveAs(strFileName)
                Catch
                    ltlAlert.Text = "alert('上传文件失败.')"
                    Return
                End Try

                If (File.Exists(strFileName)) Then
                    Dim _parent, _child, _qty, _note, _pos, store, _replace, ItemType, itemNum, itemstr As String
                    Dim _parentID, _childID, ProdID, PartID, SemiProdID, ind As Integer
                    Dim myDataset As DataTable
                    Try
                        myDataset = Me.GetExcelContents(strFileName)
                    Catch
                        If (File.Exists(strFileName)) Then
                            File.Delete(strFileName)
                        End If
                        ltlAlert.Text = "alert('导入文件必须是Excel格式.');"
                        Exit Sub
                    Finally
                        If (File.Exists(strFileName)) Then
                            File.Delete(strFileName)
                        End If
                    End Try

                    Try
                        With myDataset
                            If .Rows.Count > 0 Then
                                If .Columns(0).ColumnName <> "产品型号(可空)" Or .Columns(1).ColumnName <> "半成品编号或部件号" Then
                                    myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是产品结构导入模版.'); "
                                    Exit Sub
                                End If

                                strSQL = " Delete From ProductStruImportTemp Where userID='" & Session("uID") & "' And plantCode='" & Session("plantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                For i = 0 To .Rows.Count - 1
                                    _parent = Nothing
                                    _child = Nothing
                                    _childID = 0
                                    _qty = Nothing
                                    _note = Nothing
                                    _pos = Nothing
                                    _replace = Nothing
                                    itemNum = Nothing
                                    itemstr = ""
                                    PartID = 0
                                    ProdID = 0
                                    SemiProdID = 0
                                    ItemType = Nothing

                                    If .Rows(i).IsNull(0) Then
                                        _parent = ""
                                    Else
                                        _parent = .Rows(i).Item(0)
                                        store = _parent
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _child = " "
                                    Else
                                        _child = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        _qty = "0"
                                    Else
                                        _qty = .Rows(i).Item(2)
                                    End If

                                    If .Rows(i).IsNull(3) Then
                                        ItemType = ""
                                    Else
                                        ItemType = .Rows(i).Item(3)
                                    End If

                                    If .Rows(i).IsNull(4) Then
                                        _pos = ""
                                    Else
                                        _pos = chk.sqlEncode(.Rows(i).Item(4))
                                    End If

                                    If .Rows(i).IsNull(5) Then
                                        _note = ""
                                    Else
                                        _note = .Rows(i).Item(5)
                                    End If

                                    If .Rows(i).IsNull(6) Then
                                        _replace = ""
                                    Else
                                        _replace = .Rows(i).Item(6)
                                    End If

                                    'fileds validation 
                                    If (_parent.Trim().Length <= 0) Then
                                        _parent = ""
                                    End If

                                    If (_child.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('文件格式错误(0) --行 " & (i + 2).ToString & "，半成品编号或部件号不能为空！');"
                                        myDataset.Reset()
                                        Return
                                    End If

                                    If (_qty.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('文件格式错误(0) --行 " & (i + 2).ToString & "，数量不能为空！');"
                                        myDataset.Reset()
                                        Return
                                    ElseIf IsNumeric(_qty.Trim()) = False Then
                                        ltlAlert.Text = "alert('文件格式错误(1) --行 " & (i + 2).ToString & "，数量不是数值！');"
                                        myDataset.Reset()
                                        Return
                                    ElseIf Val(_qty.Trim()) <= 0 Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，数量不能小于零！');"
                                        myDataset.Reset()
                                        Return
                                    End If

                                    If ItemType.Trim().Length = 0 Then
                                        ltlAlert.Text = "alert('文件格式错误(3) --行 " & (i + 2).ToString & "，类型不能为空！');"
                                        myDataset.Reset()
                                        Return
                                    ElseIf ItemType.Trim() <> "1" And ItemType.Trim() <> "0" Then
                                        ltlAlert.Text = "alert('文件格式错误(4) --行 " & (i + 2).ToString & "，类型只能为0或1！');"
                                        myDataset.Reset()
                                        Return
                                    ElseIf ItemType = "1" Then
                                        ItemType = "PROD"
                                    Else
                                        ItemType = "PART"
                                    End If

                                    If (_pos.Trim().Length <= 0) Then
                                        _pos = ""
                                    ElseIf _pos.Trim().Length > 150 Then
                                        _pos = _pos.Substring(0, 150).Trim()
                                    End If

                                    If (_note.Trim().Length <= 0) Then
                                        _note = ""
                                    ElseIf _note.Trim().Length > 255 Then
                                        _note = _note.Substring(0, 255).Trim()
                                    End If

                                    While _replace.Trim().Length > 0
                                        If _replace.IndexOf(",") <> -1 Or _replace.IndexOf("，") <> -1 Then
                                            If _replace.IndexOf(",") <> -1 Then
                                                ind = _replace.IndexOf(",")
                                            ElseIf _replace.IndexOf("，") <> -1 Then
                                                ind = _replace.IndexOf("，")
                                            End If
                                            itemNum = _replace.Substring(0, ind)
                                            _replace = _replace.Substring(ind + 1)
                                        Else
                                            itemNum = _replace
                                            _replace = ""
                                        End If
                                        ProdID = FindProdID(chk.sqlEncode(itemNum))
                                        PartID = FindPartID(chk.sqlEncode(itemNum))
                                        SemiProdID = FindSemiProdID(chk.sqlEncode(itemNum))
                                        If (ProdID > 0 And (PartID > 0 Or SemiProdID > 0)) Then
                                            bBomError = True
                                            ltlAlert.Text = "alert('" & itemNum & "在产品库和部件库中均存在！');"
                                            myDataset.Reset()
                                            Return
                                        End If

                                        If (ProdID = 0 And PartID = 0 And SemiProdID = 0) Then
                                            bBomError = True
                                            ltlAlert.Text = "alert('" & itemNum & "在库中不存在！');"
                                            myDataset.Reset()
                                            Return
                                        End If
                                        If ProdID > 0 Then
                                            itemstr &= ProdID & ","
                                        ElseIf PartID > 0 Then
                                            itemstr &= PartID & ","
                                        ElseIf SemiProdID > 0 Then
                                            itemstr &= SemiProdID & ","
                                        End If
                                    End While

                                    If (Len(_parent.Trim()) > 0) Then
                                        bBomError = False
                                        _parentID = FindProdID(chk.sqlEncode(_parent))
                                        If _parentID <= 0 Then
                                            _parentID = FindSemiProdID(chk.sqlEncode(_parent))
                                            If _parentID <= 0 Then
                                                bBomError = True
                                                ltlAlert.Text = "alert('文件格式错误(5) --行 " & (i + 2).ToString & "产品/半成品型号不存在！');"
                                                myDataset.Reset()
                                                Return
                                            End If
                                        End If
                                    Else
                                        _parent = store
                                    End If

                                    If (bBomError = False) Then
                                        ProdID = FindProdID(chk.sqlEncode(_child))
                                        PartID = FindPartID(chk.sqlEncode(_child))
                                        SemiProdID = FindSemiProdID(chk.sqlEncode(_child))

                                        If (ProdID > 0 And (PartID > 0 Or SemiProdID > 0)) Then
                                            bBomError = True
                                            ltlAlert.Text = "alert('" & _child & "在产品库和部件库中均存在！');"
                                            myDataset.Reset()
                                            Return
                                        End If

                                        If (ProdID = 0 And PartID = 0 And SemiProdID = 0) Then
                                            bBomError = True
                                            ltlAlert.Text = "alert('" & _child & "在库中不存在！');"
                                            myDataset.Reset()
                                            Return
                                        End If

                                        If (ProdID > 0) Then
                                            'ItemType = "PROD"
                                            If (bBomItemExist(chk.sqlEncode(_parent), ProdID, "PROD")) Then
                                                bBomError = True
                                                ltlAlert.Text = "alert('" & _child & "在产品结构中已经存在！');"
                                                myDataset.Reset()
                                                Return
                                            Else
                                                _childID = ProdID
                                            End If
                                        ElseIf (SemiProdID > 0) Then
                                            'ItemType = "PROD"
                                            If (bBomItemExist(chk.sqlEncode(_parent), SemiProdID, "PROD")) Then
                                                bBomError = True
                                                ltlAlert.Text = "alert('" & _child & "在产品结构中已经存在！');"
                                                myDataset.Reset()
                                                Return
                                            Else
                                                _childID = SemiProdID
                                            End If
                                        Else
                                            'ItemType = "PART"
                                            If (bBomItemExist(chk.sqlEncode(_parent), PartID, "PART")) Then
                                                bBomError = True
                                                ltlAlert.Text = "alert('" & _child & "在产品结构中已经存在！');"
                                                myDataset.Reset()
                                                Return
                                            Else
                                                _childID = PartID
                                            End If
                                        End If
                                    End If

                                    If itemstr.Trim().Length > 0 Then
                                        itemstr = itemstr.Trim().Substring(0, Len(itemstr.Trim()) - 1)
                                    End If

                                    If bBomError = False Then
                                        strSQL = " Insert Into ProductStruImportTemp(productID, childID, numOfChild, childCategory, posCode, notes, userID, plantCode, itemstr) " _
                                               & " Values('" & _parentID & "','" & _childID & "','" & CDec(_qty) & "','" & ItemType & "',N'" _
                                               & chk.sqlEncode(_pos.Trim()) & "',N'" & chk.sqlEncode(_note.Trim()) & "','" & Session("uID") & "','" _
                                               & Session("plantCode") & "','" & itemstr.Trim() & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If
                                Next

                                param(0) = New SqlParameter("@intUserID", Session("uID"))
                                param(1) = New SqlParameter("@plantCodeID", Session("PlantCode"))
                                retValue = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, "Item_ProductStruImport", param)
                            End If
                        End With

                        myDataset.Reset()

                        If retValue = 0 Then
                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique")
                            ltlAlert.Text = "alert('产品结构导入成功！');window.location.href='/product/productStruImport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                        Else
                            ltlAlert.Text = "alert('产品结构导入失败！');window.location.href='/product/productStruImport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                        End If
                    Catch
                        ltlAlert.Text = "alert('上传文件失败！');"
                        Return
                    End Try
                End If
            End If
        End Sub

        Public Function FindProdID(ByVal strProdCode As String) As Integer
            strSQL = " Select id From Items Where code =N'" & strProdCode & "' And status<>2 And type=2 "
            If SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL) > 0 Then
                FindProdID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
            Else
                FindProdID = 0
            End If
        End Function

        Public Function FindSemiProdID(ByVal strSemiCode As String) As Integer
            strSQL = " Select id From Items Where code =N'" & strSemiCode & "' And status<>2 And type=1 "
            If SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL) > 0 Then
                FindSemiProdID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
            Else
                FindSemiProdID = 0
            End If
        End Function

        Public Function FindPartID(ByVal strPartCode As String) As Integer
            strSQL = " Select id From Items Where code =N'" & strPartCode & "' And status<>2 And type=0 "
            If SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL) > 0 Then
                FindPartID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
            Else
                FindPartID = 0
            End If
        End Function

        Public Function bBomItemExist(ByVal strProdCode As String, ByVal strItemID As Long, ByVal strItemType As String) As Boolean
            strSQL = " Select Count(ps.childID) From Items i " _
                   & " Inner Join Product_stru ps On ps.productID=i.id " _
                   & " Where code=N'" & strProdCode & "' And ps.childID='" & strItemID & "' And ps.childCategory='" & strItemType & "' And i.status<>2 "
            If SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL) > 0 Then
                bBomItemExist = True
            Else
                bBomItemExist = False
            End If
        End Function

    End Class

End Namespace
