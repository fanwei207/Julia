'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-10-23
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Menu From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb

Namespace tcpc
Partial Class MenuImport
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

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer

        Dim boolError As Boolean
        Dim boolExist As Boolean
        Dim ErrorRecord As Integer
        Dim cmd As SqlCommand
        Dim con As SqlConnection = New SqlConnection(chk.dsnx)
        Dim param As New SqlParameter
        Dim retValue As Integer = -1

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
                Dim _id As String
                Dim _name As String
                Dim _width As String
                Dim _url As String
                Dim _parentID As String
                Dim _desc As String
                Dim _sort As String
                Dim _ismenu As String
                Dim _hidden As String

                Try
                    Dim myDataset As DataSet = getExcelContents(strFileName)
                    With myDataset.Tables(0)
                        If .Rows.Count > 0 Then
                            strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                            ErrorRecord = 0

                            For i = 0 To .Rows.Count - 1
                                boolError = False
                                boolExist = False

                                If .Rows(i).IsNull(0) Then
                                    _id = ""
                                Else
                                    _id = .Rows(i).Item(0)
                                End If

                                If .Rows(i).IsNull(1) Then
                                    _name = ""
                                Else
                                    _name = .Rows(i).Item(1)
                                End If

                                If .Rows(i).IsNull(2) Then
                                    _width = ""
                                Else
                                    _width = .Rows(i).Item(2)
                                End If

                                If .Rows(i).IsNull(3) Then
                                    _url = ""
                                Else
                                    _url = .Rows(i).Item(3)
                                End If

                                If .Rows(i).IsNull(4) Then
                                    _parentID = "0"
                                Else
                                    _parentID = .Rows(i).Item(4)
                                End If

                                If .Rows(i).IsNull(5) Then
                                    _desc = ""
                                Else
                                    _desc = .Rows(i).Item(5)
                                End If

                                If .Rows(i).IsNull(6) Then
                                    _sort = ""
                                Else
                                    _sort = .Rows(i).Item(6)
                                End If

                                If .Rows(i).IsNull(7) Then
                                    _ismenu = "1"
                                Else
                                    _ismenu = .Rows(i).Item(7)
                                End If

                                If .Rows(i).IsNull(8) Then
                                    _hidden = ""
                                Else
                                    _hidden = .Rows(i).Item(8)
                                End If

                                'fileds validation
                                If (_id.Trim().Length > 0) Then
                                    strSQL = " Select Count(*) From Menu Where id='" & _id.Trim() & "'"
                                    If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) > 0 Then
                                        boolExist = True
                                    ElseIf IsNumeric(_id.Trim()) = False Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，ID不是数字！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    ElseIf Val(_id.Trim()) <= 0 Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，ID不能小于等于零！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If

                                    If (_name.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，name不能为空！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    ElseIf _name.Trim().Length > 30 Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，name不能长度大于30！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If

                                    If (_width.Trim().Length > 0) Then
                                        If IsNumeric(_width.Trim()) = False Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，width不是数字！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        ElseIf Val(_width.Trim()) < 0 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，width不能小于等于零！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If
                                    End If

                                    If _url.Trim().Length > 255 Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，url不能长度大于255！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If

                                    If _parentID.Trim() <> "0" Then
                                        strSQL = " Select Count(*) From Menu Where id='" & _parentID.Trim() & "'"
                                        If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) <= 0 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，parentID不存在！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If
                                    End If

                                    If _desc.Trim().Length > 300 Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，description不能长度大于300！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If

                                    If _sort.Trim() <> "" Then
                                        If IsNumeric(_sort.Trim()) = False Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，sortOrder不是数字，非法！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        ElseIf Val(_sort.Trim()) < 0 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，sortOrder不能小于等于零！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If
                                    End If

                                    If _ismenu.Trim() <> "0" And _ismenu <> "1" Then
                                        boolError = True
                                        strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                               & " Values(N'行 " & (i + 2).ToString & "，ismenu只允许0和1！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If

                                    If _hidden.Trim().Length > 0 Then
                                        If _hidden.Trim() <> "0" And _hidden <> "1" Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，hidden只允许0和1！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If
                                    End If

                                    If boolError = False Then
                                        If boolExist = False Then
                                            strSQL = " Insert Into Menu(id, name, "
                                            If _width.Trim.Length > 0 Then
                                                strSQL &= " width, "
                                            End If
                                            strSQL &= " url, parentID, description, sortOrder, isMenu "
                                            If _hidden.Trim().Length > 0 Then
                                                strSQL &= ", Hidden"
                                            End If
                                            strSQL &= ") Values('" & _id.Trim() & "',N'" & _name.Trim() & "',"
                                            If _width.Trim().Length > 0 Then
                                                strSQL &= "'" & _width.Trim() & "',"
                                            End If
                                            strSQL &= "'" & _url.Trim() & "','" & _parentID.Trim() & "',N'" & _desc.Trim() & "','" & _sort.Trim() & "','" & _ismenu.Trim()
                                            If _hidden.Trim().Length > 0 Then
                                                strSQL &= "','" & _hidden
                                            End If
                                            strSQL &= "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If
                                    Else
                                        ErrorRecord = ErrorRecord + 1
                                    End If
                                End If
                                _id = Nothing
                                _name = Nothing
                                _width = Nothing
                                _url = Nothing
                                _parentID = Nothing
                                _desc = Nothing
                                _sort = Nothing
                                _ismenu = Nothing
                                _hidden = Nothing
                            Next
                        End If
                    End With
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If

                    If ErrorRecord = 0 Then
                        ltlAlert.Text = "alert('菜单导入成功.'); window.location.href='" & chk.urlRand("/init/MenuImport.aspx") & "';"
                    Else
                        ltlAlert.Text = "alert('菜单导入结束，有错误！'); window.location.href='" & chk.urlRand("/init/MenuImport.aspx?err=y") & "';"
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

    Function getExcelContents(ByVal pFile As String, Optional ByVal sSheet As String = "Sheet1", Optional ByVal isIncreased As Boolean = False) As DataSet
        Dim str As String = sSheet

        If isIncreased Then
            Dim i As Integer = 1
            Do While i < 100
                str = str & " " & i.ToString().Trim() & " "
                Try
                    'Dim myOleDbConnection As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & pFile & "; Extended Properties=""Excel 8.0; HDR=NO; IMEX=1;""") no header row
                    Dim myOleDbConnection As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & pFile & "; Extended Properties=""Excel 8.0; HDR=YES; IMEX=1;""")
                    Dim myOleDbCommand As OleDbCommand = New OleDbCommand("SELECT * FROM [" & str & "$]", myOleDbConnection) '如果你想读出Sheet2的内,把Sheet1$改成Sheet2$即可
                    Dim myData As OleDbDataAdapter = New OleDbDataAdapter(myOleDbCommand)

                    Dim myDataset As New DataSet
                    myData.Fill(myDataset) '完成从OledbDataAdapter对象到DataSet的转换
                    getExcelContents = myDataset
                    myOleDbConnection.Close()
                    Exit Do
                Catch
                    i = i + 1
                    str = sSheet
                End Try
            Loop
        Else
            Dim myOleDbConnection As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & pFile & "; Extended Properties=""Excel 8.0; HDR=YES; IMEX=1;""")
            Dim myOleDbCommand As OleDbCommand = New OleDbCommand("SELECT * FROM [" & str & "$]", myOleDbConnection) '如果你想读出Sheet2的内,把Sheet1$改成Sheet2$即可
            Dim myData As OleDbDataAdapter = New OleDbDataAdapter(myOleDbCommand)

            Dim myDataset As New DataSet
            myData.Fill(myDataset) '完成从OledbDataAdapter对象到DataSet的转换
            getExcelContents = myDataset
            myOleDbConnection.Close()
        End If
    End Function
End Class
End Namespace
