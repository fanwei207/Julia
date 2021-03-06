'*@     Create By   :   Ye Bin    
'*@     Create Date :   2009-12.26
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import From Excel To BM_MSTR
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


    Partial Class bm_detail_import
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
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
                If Request("err") = "y" Then
                    Session("EXTitle") = "500^<b>错误原因</b>~^"
                    Session("EXHeader") = ""
                    Session("EXSQL") = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "' Order By Id "
                    ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
                End If

                FileTypeDropDownList1.SelectedIndex = 0
                Dim item1 As ListItem
                item1 = New ListItem("Excel (.xls) file")
                item1.Value = 0
                FileTypeDropDownList1.Items.Add(item1)
            End If
        End Sub

        Sub uploadPartBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadPartBtn.ServerClick
            If (Session("uID") = Nothing Or Request("bmid") = Nothing) Then
                Exit Sub
            End If
            strSQL = "select count(*) from bm_mstr where Isnull(bm_closedBy,'') = '' or Isnull(bm_deletedBy,'') = '' And bm_mstr_id = '" & Request("bmid") & "'"
            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) = 0 Then
                ltlAlert.Text = "alert('此修改单已关闭/已删除！');"
                Exit Sub
            End If
            strSQL = "select Count(*) from bm_detail where bm_mstr_id = '" & Request("bmid") & "'"
            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) <= 0 Then
                ltlAlert.Text = "alert('此修改单尚未有变更申请的数据！');"
                Exit Sub
            End If
            ImportExcelFile()
        End Sub

        Sub ImportExcelFile()
            Dim dst As DataSet

            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer

            Dim boolError As Boolean
            Dim ErrorRecord As Integer

            strCatFolder = Server.MapPath("/import")
            If Not Directory.Exists(strCatFolder) Then
                Try
                    Directory.CreateDirectory(strCatFolder)
                Catch
                    ltlAlert.Text = "alert('上传文件失败.');"
                    Return
                End Try
            End If

            strUserFileName = filename1.PostedFile.FileName
            intLastBackslash = strUserFileName.LastIndexOf("\")
            strFileName = strUserFileName.Substring(intLastBackslash + 1)
            If (strFileName.Trim().Length <= 0) Then
                ltlAlert.Text = "alert('请选择导入文件.');"
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
                    ltlAlert.Text = "alert('上传的文件最大为 8 MB.');"
                    Return
                End If
                Try
                    filename1.PostedFile.SaveAs(strFileName)
                Catch
                    ltlAlert.Text = "alert('上传文件失败.');"
                    Return
                End Try

                If (File.Exists(strFileName)) Then
                    Dim _qad As String
                    Dim _shplan As String
                    Dim _shstock As String
                    Dim _shother As String
                    Dim _zjplan As String
                    Dim _zjstock As String
                    Dim _zjother As String
                    Dim _yzplan As String
                    Dim _yzstock As String
                    Dim _yzother As String

                    'Dim myDataset As DataSet
                    Dim mydt As DataTable
                    Try
                        'myDataset = chk.getExcelContents(strFileName)
                        mydt = GetExcelContents(strFileName)
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

                        With mydt
                            If .Rows.Count > 0 Then
                                If .Columns.Count <> 10 Then
                                    'myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是变更数量导入模版.'); "
                                    Exit Sub
                                End If
                                If .Columns(0).ColumnName <> "QAD" Or .Columns(1).ColumnName <> "上海计划采购数据" Or .Columns(2).ColumnName <> "上海仓库库存数据" Or .Columns(3).ColumnName <> "上海其他数据" Or .Columns(7).ColumnName <> "扬州计划采购数据" Or .Columns(8).ColumnName <> "扬州仓库库存数据" Or .Columns(9).ColumnName <> "扬州其他数据" Then
                                    'myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是变更数量导入模版.'); "
                                    Exit Sub
                                End If

                                strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                                strSQL = " Delete From bm_detail_tmp Where userID='" & Session("uID") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                                ErrorRecord = 0

                                For i = 0 To .Rows.Count - 1
                                    boolError = False
                                    _qad = Nothing
                                    _shplan = Nothing
                                    _shstock = Nothing
                                    _shother = Nothing
                                    _zjplan = Nothing
                                    _zjstock = Nothing
                                    _zjother = Nothing
                                    _yzplan = Nothing
                                    _yzstock = Nothing
                                    _yzother = Nothing

                                    If .Rows(i).IsNull(0) Then
                                        _qad = ""
                                    Else
                                        _qad = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _shplan = "0"
                                    Else
                                        _shplan = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        _shstock = ""
                                    Else
                                        _shstock = .Rows(i).Item(2)
                                    End If

                                    If .Rows(i).IsNull(3) Then
                                        _shother = "0"
                                    Else
                                        _shother = .Rows(i).Item(3)
                                    End If

                                    If .Rows(i).IsNull(4) Then
                                        _zjplan = "0"
                                    Else
                                        _zjplan = .Rows(i).Item(4)
                                    End If

                                    If .Rows(i).IsNull(5) Then
                                        _zjstock = ""
                                    Else
                                        _zjstock = .Rows(i).Item(5)
                                    End If

                                    If .Rows(i).IsNull(6) Then
                                        _zjother = "0"
                                    Else
                                        _zjother = .Rows(i).Item(6)
                                    End If

                                    If .Rows(i).IsNull(7) Then
                                        _yzplan = "0"
                                    Else
                                        _yzplan = .Rows(i).Item(7)
                                    End If

                                    If .Rows(i).IsNull(8) Then
                                        _yzstock = ""
                                    Else
                                        _yzstock = .Rows(i).Item(8)
                                    End If

                                    If .Rows(i).IsNull(9) Then
                                        _yzother = "0"
                                    Else
                                        _yzother = .Rows(i).Item(9)
                                    End If

                                    'fileds validation
                                    If (_qad.Trim().Length > 0) Then
                                        If _shplan.Trim().Length > 0 Then
                                            If Not IsNumeric(_shplan) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",上海计划采购数据必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_shplan) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",上海计划采购数据不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _shplan = "0"
                                        End If

                                        If _shstock.Trim().Length > 0 Then
                                            If Not IsNumeric(_shstock) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",上海仓库库存数据必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_shstock) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",上海仓库库存数据不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _shstock = "0"
                                        End If

                                        If _shother.Trim().Length > 0 Then
                                            If Not IsNumeric(_shother) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",上海其他数据必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_shother) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",上海其他数据不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _shother = "0"
                                        End If

                                        If _zjplan.Trim().Length > 0 Then
                                            If Not IsNumeric(_zjplan) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",镇江计划采购数据必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_zjplan) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",镇江计划采购数据不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _zjplan = "0"
                                        End If

                                        If _zjstock.Trim().Length > 0 Then
                                            If Not IsNumeric(_zjstock) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",镇江仓库库存数据必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_zjstock) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",镇江仓库库存数据不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _zjstock = "0"
                                        End If

                                        If _zjother.Trim().Length > 0 Then
                                            If Not IsNumeric(_zjother) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",镇江其他数据必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_zjother) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",镇江其他数据不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _zjother = "0"
                                        End If

                                        If _yzplan.Trim().Length > 0 Then
                                            If Not IsNumeric(_yzplan) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",扬州计划采购数据必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_yzplan) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",扬州计划采购数据不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _yzplan = "0"
                                        End If

                                        If _yzstock.Trim().Length > 0 Then
                                            If Not IsNumeric(_yzstock) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",扬州仓库库存数据必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_yzstock) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",扬州仓库库存数据不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _yzstock = "0"
                                        End If

                                        If _yzother.Trim().Length > 0 Then
                                            If Not IsNumeric(_yzother) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",扬州其他数据必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_yzother) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",扬州其他数据不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _yzother = "0"
                                        End If

                                        If boolError = False Then
                                            strSQL = " Insert Into bm_detail_tmp(bm_mstr_id, old_child, sh_plan_qty_init, sh_stock_qty_init, sh_other_qty_init, zj_plan_qty_init, zj_stock_qty_init," _
                                                   & " zj_other_qty_init, yz_plan_qty_init, yz_stock_qty_init, yz_other_qty_init,  userid) " _
                                                   & " Values('" & Request("bmid") & "', '" & _qad.Trim() & "','" & _shplan.Trim() & "','" & _shstock.Trim() & "', '" _
                                                   & _shother.Trim() & "','" & _zjplan.Trim() & "','" & _zjstock.Trim() & "','" & _zjother.Trim() & "','" & _yzplan.Trim() & "','" _
                                                   & _yzstock.Trim() & "','" & _yzother.Trim() & "','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                        Else
                                            ErrorRecord = ErrorRecord + 1
                                        End If
                                    Else
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & ",QAD不能为空.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If
                                Next
                            End If
                        End With
                        'myDataset.Reset()

                        If ErrorRecord = 0 Then
                            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, "bm_detail_import1", New SqlParameter("@userid", Session("uID"))) = -100 Then
                                ltlAlert.Text = "alert('导入不成功,有错误.'); window.location.href='" & chk.urlRand("/QAD/bm_detail_import.aspx?bmid=" & Request("bmid") & "&err=y") & "';"
                            Else
                                ltlAlert.Text = "alert('导入成功.'); window.location.href='" & chk.urlRand("/QAD/bm_mstr.aspx") & "';"
                            End If
                        Else
                            ltlAlert.Text = "alert('导入结束,有错误.'); window.location.href='" & chk.urlRand("/QAD/bm_detail_import.aspx?bmid=" & Request("bmid") & "&err=y") & "';"
                        End If
                    Catch
                        Response.Write(strSQL)
                        Return
                    End Try
                End If
            End If
        End Sub

        Private Sub BtnRet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRet.Click
            Response.Redirect("bm_mstr.aspx", True)
        End Sub
    End Class

End Namespace

