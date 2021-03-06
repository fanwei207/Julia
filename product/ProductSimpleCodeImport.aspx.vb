'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-9-5
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Product Simple Code From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


    Partial Class ProductSimpleCodeImport
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal

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

                filetypeDDL.SelectedIndex = 0
                Dim item As ListItem
                item = New ListItem("Excel (.xls) file.")
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
            Dim strSQL As String
            Dim ds As DataSet

            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer

            Dim ErrorRecord As Integer

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
                    Dim id, _code, _simple As String
                    Try
                        Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                        With myDataset
                            If .Rows.Count > 0 Then
                                strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                ErrorRecord = 0

                                For i = 0 To .Rows.Count - 1
                                    _code = Nothing
                                    _simple = Nothing
                                    id = Nothing

                                    If .Rows(i).IsNull(0) Then
                                        _code = ""
                                    Else
                                        _code = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _simple = " "
                                    Else
                                        _simple = .Rows(i).Item(1)
                                    End If

                                    'fileds validation 
                                    If (_code.Trim().Length > 0) Then
                                        strSQL = " Select id from Items where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                        id = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                        If id = Nothing Or id = "" Then
                                            ErrorRecord = ErrorRecord + 1
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，产品型号不存在！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        Else
                                            strSQL = " Update Items SET SimpleCode=N'" & chk.sqlEncode(_simple.Trim()) & "'," _
                                                   & " modifiedBy='" & Session("uID") & "'," _
                                                   & " modifiedDate=getdate() " _
                                                   & " Where id='" & id.Trim() & "'"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
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
                            ltlAlert.Text = "alert('产品简称导入成功.'); window.location.href='" & chk.urlRand("/product/ProductSimpleCodeImport.aspx") & "';"
                        Else
                            ltlAlert.Text = "alert('产品简称导入结束，有错误！'); window.location.href='" & chk.urlRand("/product/ProductSimpleCodeImport.aspx?err=y") & "';"
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
