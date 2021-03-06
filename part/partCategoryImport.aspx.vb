'*@     Create By   :   Zhao Nai Qi   
'*@     Create Date :   2005-6-16
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Parts Category From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


    Partial Class partCategoryImport
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

            Dim strSQL1 As String

            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer
            Dim ErrorRecord As Integer
            Dim boolError As Boolean

            strCatFolder = Server.MapPath("/import")
            If Not Directory.Exists(strCatFolder) Then
                Try
                    Directory.CreateDirectory(strCatFolder)
                Catch
                    ltlAlert.Text = "alert('上传文件失败(1001)！.')"
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
                    ltlAlert.Text = "alert('上传文件失败(1002)！.')"
                    Return
                End Try

                If (File.Exists(strFileName)) Then
                    Dim _name As String
                    Dim _ratio As String
                    Dim _description As String

                    Try
                        Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                        With myDataset
                            If .Rows.Count > 0 Then
                                strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                ErrorRecord = 0

                                For i = 0 To .Rows.Count - 1
                                    boolError = False
                                    _name = Nothing
                                    _ratio = Nothing
                                    _description = Nothing

                                    If .Rows(i).IsNull(0) Then
                                        _name = ""
                                    Else
                                        _name = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _ratio = "1"
                                    Else
                                        _ratio = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        _description = " "
                                    Else
                                        _description = .Rows(i).Item(2)
                                    End If

                                    'fileds validation
                                    If (_name.Trim().Length > 0) Then
                                        If IsNumeric(_ratio.Trim()) = False Then
                                            boolError = True
                                            ErrorRecord = ErrorRecord + 1
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，比率不是数字！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        ElseIf Val(_ratio.Trim()) <= 0 Then
                                            boolError = True
                                            ErrorRecord = ErrorRecord + 1
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，比率不能小于等于零！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If
                                        If (_description.Trim().Length > 0) Then
                                            If boolError = False Then
                                                strSQL = " Select id From ItemCategory Where name=N'" & chk.sqlEncode(_name.Trim()) & "' And type=0 "
                                                ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)
                                                If ds.Tables(0).Rows.Count <= 0 Then
                                                    strSQL1 = " Insert Into ItemCategory(name,ratio,description,type,plantCode) " _
                                                            & " Values(N'" & chk.sqlEncode(_name.Trim()) & "','" & _ratio & "',N'" _
                                                            & chk.sqlEncode(_description.Trim()) & "',0," & Session("plantCode") & ")"
                                                Else
                                                    strSQL1 = " Update ItemCategory Set name=N'" & chk.sqlEncode(_name.Trim()) & "'," _
                                                            & " ratio=" & _ratio & "," _
                                                            & " description=N'" & chk.sqlEncode(_description.Trim()) & "'" _
                                                            & " Where id=" & ds.Tables(0).Rows(0).Item(0)
                                                End If
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL1)
                                                ds.Reset()
                                            End If
                                        Else
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "，部件描述不能为空！','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ErrorRecord = ErrorRecord + 1
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
                            ltlAlert.Text = "alert('材料分类导入成功！'); window.location.href='" & chk.urlRand("/part/partCategoryImport.aspx") & "';"
                        Else
                            ltlAlert.Text = "alert('材料分类导入结束，有错误！'); window.location.href='" & chk.urlRand("/part/partCategoryImport.aspx?err=y") & "';"
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
