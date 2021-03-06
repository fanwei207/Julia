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


Partial Class partUnitimport
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
                'Try

                    With myDataset
                        If .Rows.Count > 0 Then
                            If .Columns(0).ColumnName <> "部件号" Or .Columns(2).ColumnName <> "单位(可空)" Or .Columns(3).ColumnName <> "转换前单位(可空)" Then
                                myDataset.Reset()
                                ltlAlert.Text = "alert('导入文件不是部件单位导入模版.'); "
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
                                    _mininv = "0"
                                Else
                                    _mininv = .Rows(i).Item(1)
                                End If

                                If .Rows(i).IsNull(2) Then
                                    _unit = ""
                                Else
                                    _unit = .Rows(i).Item(2)
                                End If

                                If .Rows(i).IsNull(3) Then
                                    _tranunit = ""
                                Else
                                    _tranunit = .Rows(i).Item(3)
                                End If

                                If .Rows(i).IsNull(4) Then
                                    _tranrate = ""
                                Else
                                    _tranrate = .Rows(i).Item(4)
                                End If

                                'fileds validation
                                If (_code.Trim().Length > 0) Then
                                    If _code.Trim().IndexOf(" ") <> -1 Or _code.Trim().IndexOf("/") <> -1 Or _code.Trim().IndexOf("_") <> -1 Or _code.Trim().IndexOf("\") <> -1 Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，部件号不能包含空格，斜杠，下划线！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
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


                                    If boolError = False Then
                                        strSQL = " Select id from Items where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                        ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)
                                        If ds.Tables(0).Rows.Count <= 0 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，部件不存在！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            ErrorRecord = ErrorRecord + 1
                                        Else
                                            strSQL = " Select id from Items where code=N'" & chk.sqlEncode(_code.Trim()) & "' And type=0 "
                                            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) > 0 Then
                                                strSQL = " Update Items SET min_inv='" & _mininv.Trim() & " '," _
                                                       & " modifiedBy='" & Session("uID") & "'," _
                                                       & " modifiedDate='" & DateTime.Now() & "'," _
                                                       & " plantcode='" & Session("plantcode") & "'," _
                                                       & " unit=N'" & chk.sqlEncode(_unit.Trim()) & "'," _
                                                       & " tranUnit=N'" & chk.sqlEncode(_tranunit.Trim()) & "'"
                                                If _tranrate.Trim.Length > 0 Then
                                                    strSQL &= " ,tranRate='" & _tranrate.Trim() & "'"
                                                End If
                                                strSQL &= " Where id='" & ds.Tables(0).Rows(0).Item(0).ToString().Trim() & "'"

                                            End If
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
                        ltlAlert.Text = "alert('部件单位导入成功.'); window.location.href='" & chk.urlRand("/part/partUnitimport.aspx") & "';"
                    Else
                        ltlAlert.Text = "alert('部件单位导入结束，有错误！'); window.location.href='" & chk.urlRand("/part/partUnitimport.aspx?err=y") & "';"
                    End If
            End If
        End If
    End Sub

End Class

End Namespace
