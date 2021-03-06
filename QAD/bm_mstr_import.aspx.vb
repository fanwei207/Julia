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


Partial Class bm_mstr_import
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
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
                Dim _parent As String
                Dim _oldchild As String
                Dim _newchild As String
                Dim _unitqty As String
                Dim _rejrate As String
                Dim _oper As String

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
                                If .Columns.Count <> 6 Then
                                    'myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是变更申请导入模版.'); "
                                    Exit Sub
                                End If
                                If .Columns(0).ColumnName <> "父件QAD" Or .Columns(1).ColumnName <> "老子件QAD" Or .Columns(2).ColumnName <> "新子件QAD" Or .Columns(3).ColumnName <> "单件需求量" Or .Columns(4).ColumnName <> "废品率" Or .Columns(5).ColumnName <> "工序" Then
                                    'myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是变更申请导入模版.'); "
                                    Exit Sub
                                End If

                                strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                                strSQL = " Delete From bm_detail_tmp Where userID='" & Session("uID") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                                ErrorRecord = 0

                                For i = 0 To .Rows.Count - 1
                                    boolError = False
                                    _parent = Nothing
                                    _oldchild = Nothing
                                    _newchild = Nothing
                                    _unitqty = Nothing
                                    _rejrate = Nothing
                                    _oper = Nothing

                                    If .Rows(i).IsNull(0) Then
                                        _parent = ""
                                    Else
                                        _parent = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _oldchild = ""
                                    Else
                                        _oldchild = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        _newchild = ""
                                    Else
                                        _newchild = .Rows(i).Item(2)
                                    End If

                                    If .Rows(i).IsNull(3) Then
                                        _unitqty = "0"
                                    Else
                                        _unitqty = .Rows(i).Item(3)
                                    End If

                                    If .Rows(i).IsNull(4) Then
                                        _rejrate = "0"
                                    Else
                                        _rejrate = .Rows(i).Item(4)
                                    End If

                                    If .Rows(i).IsNull(5) Then
                                        _oper = ""
                                    Else
                                        _oper = .Rows(i).Item(5)
                                    End If


                                    'fileds validation
                                    If (_parent.Trim().Length > 0 And _oldchild.Trim().Length > 0 And _newchild.Trim().Length > 0) Then
                                        If UCase(_parent.Trim) <> "NOPARENT" Then
                                            If _parent.Trim.Length <> 14 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",父件QAD必须为14位或者NoParent.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        End If

                                        'If _oldchild.Trim.Length <> 14 Then
                                        '    boolError = True
                                        '    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                        '           & " Values(N'行 " & (i + 2).ToString & ",老子件QAD必须为14位.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        '    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        'End If
                                        If _newchild.Trim.Length <> 14 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & ",新子件QAD必须为14位.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If

                                        'If _unitqty.Trim().Length > 0 Then
                                        '    If Not IsNumeric(_unitqty) Then
                                        '        boolError = True
                                        '        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                        '               & " Values(N'行 " & (i + 2).ToString & ",单件需求量必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        '        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        '    ElseIf Val(_unitqty) < 0 Then
                                        '        boolError = True
                                        '        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                        '               & " Values(N'行 " & (i + 2).ToString & ",单件需求量不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        '        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        '    End If
                                        'End If

                                        If _rejrate.Trim().Length > 0 Then
                                            If Not IsNumeric(_rejrate) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",废品率必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_rejrate) < 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ",废品率不能小于零.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        End If

                                        If boolError = False Then
                                            strSQL = " Insert Into bm_detail_tmp(bm_mstr_id, parent, old_child, new_child, unit_qty, rej_rate, oper, userid) " _
                                                   & " Values('" & Request("bmid") & "', '" & _parent.Trim() & "','" & _oldchild.Trim() & "','" & _newchild.Trim() & "', N'" _
                                                   & _unitqty.Trim() & "','" & _rejrate.Trim() & "','" & _oper.Trim() & "','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                        Else
                                            ErrorRecord = ErrorRecord + 1
                                        End If
                                    Else
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & ",父件QAD,老子件QAD,新子件QAD均不能有空数据.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If
                                Next
                            End If
                        End With
                        'myDataset.Reset()

                    If ErrorRecord = 0 Then
                        If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, "bm_detail_import", New SqlParameter("@userid", Session("uID"))) = -100 Then
                            ltlAlert.Text = "alert('导入不成功,有错误.'); window.location.href='" & chk.urlRand("/QAD/bm_mstr_import.aspx?bmid=" & Request("bmid") & "&err=y") & "';"
                        Else
                            ltlAlert.Text = "alert('导入成功.'); window.location.href='" & chk.urlRand("/QAD/bm_mstr.aspx") & "';"
                        End If
                    Else
                        ltlAlert.Text = "alert('导入结束,有错误.'); window.location.href='" & chk.urlRand("/QAD/bm_mstr_import.aspx?bmid=" & Request("bmid") & "&err=y") & "';"
                    End If
                Catch
                    Response.Write(strSQL)
                    Return
                End Try
            End If
        End If
    End Sub

    Private Sub btnRet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRet.Click
        Response.Redirect("bm_mstr.aspx", True)
    End Sub
End Class

End Namespace

