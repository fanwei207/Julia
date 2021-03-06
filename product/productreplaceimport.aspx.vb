'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-4-4
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Products From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class productreplaceimport
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    'Dim strTemp As String
    'Dim strParamquery As String = "sp_setSystemChanges"
    'Dim param(2) As SqlParameter

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not (IsPostBack) Then

                If Request("err") = "y" Then
                    Session("EXTitle1") = "500^<b>错误原因</b>~^"
                    Session("EXHeader1") = ""
                    Session("EXSQL1") = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                    ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
                End If

                filetypeDDL.SelectedIndex = 0
                Dim item As ListItem
                item = New ListItem("Excel (.xls) file")
                item.Value = 0
                filetypeDDL.Items.Add(item)
            End If
        End Sub

    Public Sub uploadProdReplaceBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadProdReplaceBtn.ServerClick
        If (Session("uID") = Nothing) Then
            Exit Sub
        End If
        ImportExcelFile()
    End Sub

    Private Sub ImportExcelFile()
        Dim strSQL As String
        Dim ds As DataSet
        Dim strSQL1 As String
        Dim param(3) As SqlParameter

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer
        Dim boolError As Boolean
        Dim ErrorRecord As Integer

        Dim _type As String
        If Request("semi") = "true" Then
            _type = "1"
        Else
            _type = "2"
        End If

        strCatFolder = Server.MapPath("/import")
        If Not Directory.Exists(strCatFolder) Then
            Try
                Directory.CreateDirectory(strCatFolder)
            Catch
                ltlAlert.Text = "alert('上传文件失败(1001)！.')"
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
                ltlAlert.Text = "alert('上传文件失败(1002)！.')"
                Return
            End Try

            If (File.Exists(strFileName)) Then
                Dim _prodID As String
                Dim _code As String
                Dim _category As String
                Dim _catID As String
                Dim codetype As Integer
                Dim numLS As Integer
                Dim numLS1 As Integer
                Dim numLSN As Integer
                Dim numLSN1 As Integer
                Dim numVer As Integer
                Dim numSubVer As Integer
                Dim strItem As String

                Try
                        Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                        With myDataset
                            If .Rows.Count > 0 Then
                                strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                ErrorRecord = 0

                                For i = 0 To .Rows.Count - 1
                                    _code = Nothing
                                    _prodID = Nothing
                                    _category = Nothing
                                    _catID = 0
                                    numLS = Nothing
                                    numLS1 = Nothing
                                    numLSN = Nothing
                                    numLSN1 = Nothing
                                    numVer = Nothing
                                    numSubVer = Nothing
                                    strItem = Nothing
                                    boolError = False

                                    If .Rows(i).IsNull(0) Then
                                        _prodID = ""
                                    Else
                                        _prodID = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _code = ""
                                    Else
                                        _code = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(3) Then
                                        _category = ""
                                    Else
                                        _category = .Rows(i).Item(3)
                                    End If


                                    'fileds validation
                                    If (_prodID.Trim().Length > 0) Then
                                        If (_code.Trim().Length > 0) Then
                                            If _code.Trim().IndexOf(" ") <> -1 Or _code.Trim().IndexOf("/") <> -1 Or _code.Trim().IndexOf("_") <> -1 Or _code.Trim().IndexOf("\") <> -1 Then
                                                boolError = True
                                                ErrorRecord = ErrorRecord + 1
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，修改后产品型号不能包含空格，斜杠，下划线！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            Else
                                                strSQL = " Select id from Items where code='" & _code.Trim() & "' And id<>'" & _prodID.Trim() & "'"
                                                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) > 0 Then
                                                    boolError = True
                                                    ErrorRecord = ErrorRecord + 1
                                                    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                           & " Values(N'行 " & (i + 2).ToString & "，修改后编号已经存在！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                                Else
                                                    strSQL = "Select type from Items where id='" & _prodID.Trim() & "'"
                                                    ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)
                                                    If ds.Tables(0).Rows.Count > 0 Then
                                                        If ds.Tables(0).Rows(0).Item(0).ToString().Trim() <> "2" Then
                                                            boolError = True
                                                            ErrorRecord = ErrorRecord + 1
                                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & "，导入的不是产品！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                                        Else
                                                            If _category.Trim().Length > 0 Then
                                                                param(0) = New SqlParameter("@categoryName", chk.sqlEncode(_category.Trim()))
                                                                param(1) = New SqlParameter("@intUserID", Session("uID"))
                                                                param(2) = New SqlParameter("@type", "0")
                                                                param(3) = New SqlParameter("@intPlant", Session("plantCode"))
                                                                _catID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_SetCategory", param)
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
                                                                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                                                                    Exit Sub
                                                                End If
                                                            ElseIf numLSN1 > 0 Then
                                                                numVer = 0
                                                                numSubVer = 0
                                                                strItem = chk.sqlEncode(_code.Trim().Substring(numLSN1 + 2))
                                                                numLS1 = 0
                                                                If numLSN1 = 1 Then
                                                                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                                                                    Exit Sub
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
                                                                    strItem = chk.sqlEncode(_code.Trim().Substring(numLSN + 2))
                                                                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                                                                    Exit Sub
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
                                                                    strItem = chk.sqlEncode(_code.Trim().Substring(numLS1 + 1))
                                                                    ltlAlert.Text = "alert('部件编号不符合规则，无法添加！');"
                                                                    Exit Sub
                                                                End If
                                                            Else
                                                                numVer = 0
                                                                numSubVer = 0
                                                                strItem = chk.sqlEncode(_code.Trim())
                                                            End If


                                                            If boolError = False Then
                                                                If _catID <> 0 Then
                                                                    strSQL1 = " Update Items SET code=N'" & chk.sqlEncode(_code.Trim()) & "'," _
                                                                            & " category='" & _catID & "'," _
                                                                            & " itemNumber='" & strItem.Trim() & "'," _
                                                                            & " itemVersion='" & numVer & "'," _
                                                                            & " itemSubVersion='" & numSubVer & "'," _
                                                                            & " plantcode='" & Session("plantCode") & "'," _
                                                                            & " modifiedBy='" & Session("uID") & "'," _
                                                                            & " modifiedDate='" & DateTime.Now() & "'" _
                                                                            & " Where id='" & _prodID & "'"
                                                                Else
                                                                    strSQL1 = " Update Items SET Code=N'" & chk.sqlEncode(_code.Trim()) & "'," _
                                                                            & " plantcode='" & Session("plantCode") & "'," _
                                                                            & " itemNumber='" & strItem.Trim() & "'," _
                                                                            & " itemVersion='" & numVer & "'," _
                                                                            & " itemSubVersion='" & numSubVer & "'," _
                                                                            & " modifiedBy='" & Session("uID") & "'," _
                                                                            & " modifiedDate='" & DateTime.Now() & "'" _
                                                                            & " Where partID='" & _prodID & "'"
                                                                End If
                                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL1)
                                                            End If
                                                        End If
                                                        ds.Reset()
                                                    Else
                                                        boolError = True
                                                        ErrorRecord = ErrorRecord + 1
                                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                               & " Values(N'行 " & (i + 2).ToString & "，partID为" & _prodID.Trim() & "的不存在！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End With
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    If ErrorRecord = 0 Then
                        ltlAlert.Text = "alert('产品更新成功.'); window.close(); window.opener.location.href='" & chk.urlRand("/product/productlist.aspx") & "';"
                    Else
                        ltlAlert.Text = "alert('产品更新结束，有错误！'); window.location.href='" & chk.urlRand("/product/productreplaceimport.aspx?err=y") & "';"
                    End If
                Catch
                    ltlAlert.Text = "alert('上传文件非法！');"
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    Return
                End Try
            End If
        End If
    End Sub
End Class

End Namespace
