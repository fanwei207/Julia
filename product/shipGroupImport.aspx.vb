Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc

    Partial Class shipGroupImport
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
            If Not (IsPostBack) Then

                filetypeDDL.SelectedIndex = 0
                Dim item As ListItem
                item = New ListItem("Excel (.xls) file")
                item.Value = 0
                filetypeDDL.Items.Add(item)
            End If

        End Sub

        Private Sub ImportExcelFile()
            Dim strSQL As String
            Dim ds As DataSet

            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer

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
                strFileName = strCatFolder & "\ps" & i.ToString() & strUserFileName
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
                    Dim product_code As String
                    Dim ship_group As String

                    Try
                        Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                        With myDataset
                            If .Rows.Count > 0 Then
                                For i = 0 To .Rows.Count - 1
                                    If .Rows(i).IsNull(0) Then
                                        product_code = ""
                                    Else
                                        product_code = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        ship_group = ""
                                    Else
                                        ship_group = .Rows(i).Item(1)
                                    End If

                                    'fileds validation 
                                    If (product_code.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行" & i.ToString & ",产品型号不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (ship_group.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('文件格式错误(1) --行" & i.ToString & ",出运系列不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (ship_group.Trim().Length > 20) Then
                                        ltlAlert.Text = "alert('文件格式错误(1) --行" & i.ToString & ",出运系列长度不能超过20个字符！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    strSQL = " Select id from Items where code='" & chk.sqlEncode(product_code.Trim()) & "' And type=2 and status<>2 "
                                    ID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If ID > 0 Then
                                        '产品存在
                                        strSQL = " Update Items Set shipGroup=N'" & ship_group & "'" _
                                               & " Where id='" & ID & "'"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                    End If
                                Next
                            End If
                        End With
                        myDataset.Reset()
                        If (File.Exists(strFileName)) Then
                            File.Delete(strFileName)
                        End If
                        ltlAlert.Text = "alert('产品出运系列导入成功！'); window.location.href='/product/shipGroupImport.aspx';"
                    Catch
                        ltlAlert.Text = "alert('上传文件非法!');"
                        Return
                    End Try
                End If
            End If
        End Sub

        Private Sub uploadBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadBtn.Click
            If (Session("uID") = Nothing) Then
                Exit Sub
            End If
            ImportExcelFile()
        End Sub
    End Class

End Namespace

