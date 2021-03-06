'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-07-04
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Items From Excel To Item_QAD_Code
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


    Partial Class Item_Qad_Import2
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer

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
            If (Session("uID") = Nothing) Then
                Exit Sub
            End If
            ImportExcelFile()
        End Sub

        Sub ImportExcelFile()
            Dim strSQL As String
            Dim ds As DataSet

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
                    Dim _qad As String
                    Dim _desc1 As String
                    Dim _desc2 As String
                    Dim id As Long

                    Dim myDataset As DataSet
                    Dim dt As DataTable
                    Try
                        dt = GetExcelContents(strFileName)
                        'chk.getExcelContents(strFileName)
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
                    'Try

                    With dt
                        If .Rows.Count > 0 Then
                            If .Columns(0).ColumnName <> "元件编号" Or .Columns(1).ColumnName <> "QAD零件号" Or .Columns(2).ColumnName <> "QAD描述1(可空)" Or .Columns(3).ColumnName <> "QAD描述2(可空)" Then
                                'myDataset.Reset()
                                ltlAlert.Text = "alert('导入文件不是准备QAD元件导入模版.'); "
                                Exit Sub
                            End If

                            strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                            strSQL = " Delete From item_qad_tmp Where userid='" & Session("uID") & "' "
                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                            ErrorRecord = 0

                            For i = 0 To .Rows.Count - 1
                                boolError = False
                                _code = Nothing
                                _qad = Nothing
                                _desc1 = Nothing
                                _desc2 = Nothing
                                id = 0

                                If .Rows(i).IsNull(0) Then
                                    _code = ""
                                Else
                                    _code = .Rows(i).Item(0)
                                End If

                                If .Rows(i).IsNull(1) Then
                                    _qad = ""
                                Else
                                    _qad = .Rows(i).Item(1)
                                End If

                                If .Rows(i).IsNull(2) Then
                                    _desc1 = ""
                                Else
                                    _desc1 = .Rows(i).Item(2)
                                End If

                                If .Rows(i).IsNull(3) Then
                                    _desc2 = ""
                                Else
                                    _desc2 = .Rows(i).Item(3)
                                End If

                                'fileds validation
                                If (_code.Trim().Length > 0) Then
                                    If _code.Trim().IndexOf(" ") <> -1 Or _code.Trim().IndexOf("/") <> -1 Or _code.Trim().IndexOf("_") <> -1 Or _code.Trim().IndexOf("\") <> -1 Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，元件编号不能包含空格，斜杠，下划线！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        'Else
                                        '    strSQL = " Select id From items Where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                        '    id = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)
                                        '    If id = 0 Or id = Nothing Then
                                        '        boolError = True
                                        '        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                        '               & " Values(N'行 " & (i + 2).ToString & "，元件编号在数据库里面不存在！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        '        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        '    End If
                                    End If

                                    If (_qad.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，QAD零件号不能为空！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        'ElseIf (_qad.Trim().Length > 10) Then
                                        '    boolError = True
                                        '    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                        '           & " Values(N'行 " & (i + 2).ToString & "，QAD零件号最多只能10！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        '    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        'ElseIf (SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, " Select id From items Where item_qad='" & _qad.Trim() & "'")) > 0 Then
                                        '    boolError = True
                                        '    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                        '           & " Values(N'行 " & (i + 2).ToString & "，QAD零件号已经存在！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        '    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        'ElseIf (SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, " Select id From item_qad_tmp Where item_qad='" & _qad.Trim() & "'")) > 0 Then
                                        '    boolError = True
                                        '    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                        '           & " Values(N'行 " & (i + 2).ToString & "，导入文件中QAD零件号有重复！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        '    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If

                                    If getstrlen(_desc1.Trim()) > 24 Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，QAD描述1长度不能超过24位','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If

                                    If getstrlen(_desc2.Trim()) > 24 Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，QAD描述2长度不能超过24位','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If

                                    If boolError = False Then
                                        'strSQL = " Select Count(*) From item_qad_tmp Where id='" & id & "'"
                                        'If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) <= 0 Then
                                        strSQL = " Insert Into item_qad_tmp(code,item_qad,userid,desc1,desc2) " _
                                               & " Values(N'" & chk.sqlEncode(_code.Trim()) & "','" & _qad.Trim() & "','" & Session("uID") & "',N'" & chk.sqlEncode(_desc1.Trim()) & "',N'" & chk.sqlEncode(_desc2.Trim()) & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        'End If
                                    Else
                                        ErrorRecord = ErrorRecord + 1
                                    End If
                                End If
                            Next
                        End If
                    End With
                    'myDataset.Reset()


                    If ErrorRecord = 0 Then
                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, "UpdateItemQad2", New SqlParameter("@userid", Session("uID")))

                        ltlAlert.Text = "alert('准备QAD元件导入成功.'); window.location.href='" & chk.urlRand("/QAD/Item_Qad_Import2.aspx") & "';"
                    Else
                        Response.Write(ErrorRecord)
                        Response.Write("<br>")
                        Response.Write(strSQL)

                        ltlAlert.Text = "alert('准备QAD元件导入结束，有错误！'); window.location.href='" & chk.urlRand("/QAD/Item_Qad_Import2.aspx?err=y") & "';"
                    End If
                    'Catch

                    '    ltlAlert.Text = "alert('文件导入失败！');"
                    '    Return
                    'End Try
                End If
            End If
        End Sub

        Function getstrlen(ByVal str As String) As Integer
            Dim cc As Char() = str.Trim().ToCharArray()
            Dim intlen As Integer = str.Trim().Length
            Dim i As Integer
            For i = 0 To cc.Length - 1
                If Convert.ToInt32(cc(i)) > 255 Then
                    intlen = intlen + 1
                End If
            Next
            Return intlen
        End Function

    End Class

End Namespace

