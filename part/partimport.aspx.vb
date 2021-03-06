'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-1
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Parts From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


    Partial Class partimport
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub



        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("19070500", "部件状态修改")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not (IsPostBack) Then
                If Request("err") = "y" Then
                    Session("EXTitle1") = "500^<b>错误原因</b>~^"
                    Session("EXHeader1") = ""
                    Session("EXSQL1") = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                    ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
                End If

                FileTypeDropDownList1.SelectedIndex = 0
                Dim item1 As ListItem
                item1 = New ListItem("Excel (.xls) file")
                item1.Value = 0
                FileTypeDropDownList1.Items.Add(item1)
            End If
        End Sub

        Sub uploadPartBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadPartBtn.ServerClick
            If (Session("uID") = Nothing) Then
                Exit Sub
            End If
            ImportExcelFile()
        End Sub

        Sub ImportExcelFile()
            Dim strSQL As String
            Dim ds As DataSet
            Dim param(3) As SqlParameter

            '材料状态修改
            Dim flag As Boolean
            If Not Me.Security("19070500").isValid Then
                flag = False
            Else
                flag = True
            End If

            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer

            Dim numLS As Integer
            Dim numLS1 As Integer
            Dim numLSN As Integer
            Dim numLSN1 As Integer
            Dim numVer As Integer
            Dim numSubVer As Integer
            Dim strItem As String
            Dim boolError As Boolean
            Dim ErrorRecord As Integer

            strCatFolder = Server.MapPath("/import")
            If Not Directory.Exists(strCatFolder) Then
                Try
                    Directory.CreateDirectory(strCatFolder)
                Catch
                    ltlAlert.Text = "alert('上传文件失败.')"
                    Return
                End Try
            End If

            strUserFileName = filename1.PostedFile.FileName
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

            If Not (filename1.PostedFile Is Nothing) Then
                If (filename1.PostedFile.ContentLength > 8388608) Then
                    ltlAlert.Text = "alert('上传的文件最大为 8 MB.')"
                    Return
                End If
                Try
                    filename1.PostedFile.SaveAs(strFileName)
                Catch
                    ltlAlert.Text = "alert('上传文件失败.')"
                    Return
                End Try

                If (File.Exists(strFileName)) Then
                    Dim _code As String
                    Dim _desc As String
                    Dim _category As String
                    Dim _categoryID As String
                    Dim _status As String
                    Dim _mininv As String
                    Dim _unit As String
                    Dim _tranunit As String
                    Dim _tranrate As String
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
                                If .Columns(0).ColumnName <> "部件号" Or .Columns(5).ColumnName <> "单位(可空)" Or .Columns(6).ColumnName <> "转换前单位(可空)" Then
                                    myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是部件目录导入模版.'); "
                                    Exit Sub
                                End If

                                strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                ErrorRecord = 0

                                For i = 0 To .Rows.Count - 1
                                    boolError = False
                                    If .Rows(i).IsNull(0) Then
                                        _code = ""
                                    Else
                                        _code = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _desc = ""
                                    Else
                                        _desc = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        _category = ""
                                    Else
                                        _category = .Rows(i).Item(2)
                                    End If

                                    If .Rows(i).IsNull(3) Or .Rows(i).Item(3).ToString().Trim() = "0" Or .Rows(i).Item(3).ToString().Trim() = "使用" _
                                        Or .Rows(i).Item(3).ToString().Trim() = "TRUE" Then
                                        _status = "0"
                                        'If flag = False Then
                                        '    _status = "1"
                                        'End If
                                    ElseIf .Rows(i).Item(3).ToString().Trim() = "2" Or .Rows(i).Item(3).ToString().Trim() = "停用" _
                                        Or .Rows(i).Item(3).ToString().Trim() = "false" Then
                                        _status = "2"
                                    Else
                                        _status = "1"
                                    End If

                                    If .Rows(i).IsNull(4) Then
                                        _mininv = "0"
                                    Else
                                        _mininv = .Rows(i).Item(4)
                                    End If

                                    If .Rows(i).IsNull(5) Then
                                        _unit = ""
                                    Else
                                        _unit = .Rows(i).Item(5)
                                    End If

                                    If .Rows(i).IsNull(6) Then
                                        _tranunit = ""
                                    Else
                                        _tranunit = .Rows(i).Item(6)
                                    End If

                                    If .Rows(i).IsNull(7) Then
                                        _tranrate = ""
                                    Else
                                        _tranrate = .Rows(i).Item(7)
                                    End If

                                    'fileds validation
                                    If (_code.Trim().Length > 0) Then
                                        If _code.Trim().IndexOf(" ") <> -1 Or _code.Trim().IndexOf("/") <> -1 Or _code.Trim().IndexOf("_") <> -1 Or _code.Trim().IndexOf("\") <> -1 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，部件号不能包含空格，斜杠，下划线！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        Else
                                            'strSQL = " Select Count(*) From Items Where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                            strSQL = " Select Count(*) From ( "
                                            strSQL += " Select id,code From Items Where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                            strSQL += " union all Select item_id,code From Items_his Where code=N'" & chk.sqlEncode(_code.Trim()) & "' ) item "

                                            Dim cnt As Integer = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                            If cnt > 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，已经存在此编号，无法添加！！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        End If

                                        If (_desc.Trim().Length <= 0) Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，部件描述不能为空！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If

                                        If (_category.Trim().Length <= 0) Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，部件分类不能为空！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        ElseIf _category.Trim().Length > 10 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，部件分类长度不能大于10！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        Else
                                            param(0) = New SqlParameter("@categoryName", _category.Trim())
                                            param(1) = New SqlParameter("@intUserID", Session("uID"))
                                            param(2) = New SqlParameter("@type", "0")
                                            param(3) = New SqlParameter("@intPlant", Session("plantCode"))
                                            _categoryID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_SetCategory", param)
                                        End If

                                        If (_mininv.Trim().Length <= 0) Then
                                            _mininv = "0"
                                        End If

                                        If (_tranrate.Trim().Length > 0) Then
                                            If IsNumeric(_tranrate.Trim()) = False Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，部件单位转换系数不是数字！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_tranrate.Trim()) <= 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，部件单位转换系数不能小于等于零！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _tranrate = ""
                                        End If

                                        If _unit.Trim().Length > 5 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，部件单位长度不能大于5！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If

                                        If _tranunit.Trim().Length > 5 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，部件转换前单位长度不能大于5！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If

                                        numLS = InStr(_code.Trim().ToUpper(), "LS-")
                                        numLS1 = InStr(_code.Trim().ToUpper(), "LS")
                                        numLSN = InStr(_code.Trim().ToUpper(), "LSN")
                                        numLSN1 = InStr(_code.Trim().ToUpper(), "LSN-")
                                        If numLS > 0 Then
                                            numVer = 0
                                            numSubVer = 0
                                            strItem = chk.sqlEncode(_code.Trim().Substring(numLS + 2))
                                            numLS1 = 0
                                            If numLS = 1 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(1)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        ElseIf numLSN1 > 0 Then
                                            numVer = 0
                                            numSubVer = 0
                                            strItem = chk.sqlEncode(_code.Trim().Substring(numLSN1 + 2))
                                            numLS1 = 0
                                            If numLSN1 = 1 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(2)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        ElseIf numLSN > 0 Then
                                            numLS = 0
                                            numLS = InStr(_code.Trim().Substring(numLSN + 2).ToUpper(), "-")
                                            If numLS > 0 Then
                                                If IsNumeric(Mid(_code.Trim().Substring(numLSN + 2), 1, numLS - 1)) = True Then
                                                    numVer = Mid(_code.Trim().Substring(numLSN + 2), 1, numLS - 1)
                                                    strItem = chk.sqlEncode(_code.Trim().Substring(numLSN + 2).Substring(numLS))
                                                Else
                                                    numVer = 1
                                                    strItem = chk.sqlEncode(_code.Trim().Substring(numLSN + 2).Substring(numLS))
                                                    boolError = True
                                                    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                           & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(3)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                                                End If
                                                numLS = 0
                                                numLS = InStr(strItem, "-")
                                                If numLS > 0 Then
                                                    If IsNumeric(Mid(strItem.Trim(), 1, numLS - 1)) = True Then
                                                        numSubVer = Mid(strItem.Trim(), 1, numLS - 1)
                                                    Else
                                                        numSubVer = 1
                                                        boolError = True
                                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                               & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(4)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                                                    End If
                                                    strItem = strItem.Substring(numLS)
                                                Else
                                                    numSubVer = 0
                                                    boolError = True
                                                    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                           & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(5)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                                                End If
                                            Else
                                                numVer = 1
                                                numSubVer = 1
                                                strItem = chk.sqlEncode(_code.Trim().Substring(numLSN + 2))
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(6)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                                            End If
                                        ElseIf numLS1 > 0 Then
                                            numLS = 0
                                            numLS = InStr(_code.Trim().Substring(numLS1 + 1).ToUpper(), "-")
                                            If numLS > 0 Then
                                                If IsNumeric(Mid(_code.Trim().Substring(numLS1 + 1), 1, numLS - 1)) = True Then
                                                    numVer = Mid(_code.Trim().Substring(numLS1 + 1), 1, numLS - 1)
                                                    strItem = chk.sqlEncode(_code.Trim().Substring(numLS1 + 1).Substring(numLS))
                                                Else
                                                    numVer = 1
                                                    strItem = chk.sqlEncode(_code.Trim().Substring(numLS1 + 1).Substring(numLS))
                                                    boolError = True
                                                    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                           & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(7)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                                                End If
                                                numLS = 0
                                                numLS = InStr(strItem, "-")
                                                If numLS > 0 Then
                                                    If IsNumeric(Mid(strItem.Trim(), 1, numLS - 1)) = True Then
                                                        numSubVer = Mid(strItem.Trim(), 1, numLS - 1)
                                                    Else
                                                        numSubVer = 1
                                                        boolError = True
                                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                               & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(8)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                                                    End If
                                                    strItem = strItem.Substring(numLS)
                                                Else
                                                    numSubVer = 0
                                                    boolError = True
                                                    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                           & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(9)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                                                End If
                                            Else
                                                numVer = 1
                                                numSubVer = 1
                                                strItem = chk.sqlEncode(_code.Trim().Substring(numLS1 + 1))
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，部件编号不符合规则，无法添加(10)！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                                            End If
                                        Else
                                            numVer = 0
                                            numSubVer = 0
                                            strItem = chk.sqlEncode(_code.Trim())
                                        End If

                                        If boolError = False Then
                                            'strSQL = " Select id from Items where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                            strSQL = " Select id,code From Items Where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                            strSQL += " union all Select item_id,code From Items_his Where code=N'" & chk.sqlEncode(_code.Trim()) & "' "


                                            ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)
                                            If ds.Tables(0).Rows.Count <= 0 Then
                                                strSQL = " Insert Into Items(code, description, status, category, min_inv, createdBy, createdDate, " _
                                                       & " plantcode, type, unit, itemNumber, itemVersion, itemSubVersion, tranUnit "
                                                If _tranrate.Trim.Length > 0 Then
                                                    strSQL &= " ,tranRate )"
                                                Else
                                                    strSQL &= " )"
                                                End If
                                                strSQL &= " Values(N'" & chk.sqlEncode(_code.Trim()) & "',N'" & chk.sqlEncode(_desc.Trim()) & "','" _
                                                       & _status.Trim() & "','" & _categoryID & "','" & _mininv.Trim() & "','" & Session("uID") & "','" _
                                                       & DateTime.Now & "','" & Session("plantcode") & "','0',N'" & chk.sqlEncode(_unit.Trim()) & "','" _
                                                       & strItem.Trim() & "','" & numVer & "','" & numSubVer & "',N'" & chk.sqlEncode(_tranunit.Trim()) & "'"
                                                If _tranrate.Trim.Length > 0 Then
                                                    strSQL &= " ,'" & _tranrate.Trim() & "' )"
                                                Else
                                                    strSQL &= " )"
                                                End If
                                            Else

                                            End If
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ds.Reset()
                                        Else
                                            ErrorRecord = ErrorRecord + 1
                                        End If
                                    End If
                                Next
                            End If
                        End With
                        myDataset.Reset()

                        If ErrorRecord = 0 Then
                            ltlAlert.Text = "alert('部件导入成功.'); window.location.href='" & chk.urlRand("/part/partimport.aspx") & "';"
                        Else
                            ltlAlert.Text = "alert('部件导入结束，有错误！'); window.location.href='" & chk.urlRand("/part/partimport.aspx?err=y") & "';"
                        End If
                    Catch
                        ltlAlert.Text = "alert('文件导入失败！');"
                        Return
                    End Try
                End If
            End If
        End Sub

    End Class

End Namespace
