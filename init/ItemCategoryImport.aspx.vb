Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class ItemCategoryImport
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Protected WithEvents Comparevalidator3 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents uploadProdReplaceBtn As System.Web.UI.HtmlControls.HtmlInputButton

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
           
            filetypeDDL.SelectedIndex = 0
            Dim item As ListItem
            item = New ListItem("Excel (.xls) file")
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

        Dim strSQL1 As String
        Dim ds1 As DataSet

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer

        strCatFolder = Server.MapPath("/import")
        If Not Directory.Exists(strCatFolder) Then
            Try
                Directory.CreateDirectory(strCatFolder)
            Catch
                ltlAlert.Text = "alert('上传文件失败.')"
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
                ltlAlert.Text = "alert('上传文件失败.')"
                Return
            End Try

            If (File.Exists(strFileName)) Then
                Dim item_code, item_cat, _type As String
                Dim item_id As Integer
                'Try

                    Dim myDataset As DataTable
                Try
                        myDataset = Me.GetExcelContents(strFileName)
                Catch
                    ltlAlert.Text = "alert('上传文件必须是Excel文件.')"

                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    Return
                End Try

                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If
                    With myDataset
                        If .Rows.Count > 0 Then

                            For i = 0 To .Rows.Count - 1
                                If Not .Rows(i).IsNull(0) Then
                                    If .Rows(i).IsNull(0) Then
                                        item_code = ""
                                    Else
                                        item_code = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        item_cat = ""
                                    Else
                                        item_cat = .Rows(i).Item(1)
                                    End If
                                    If .Rows(i).IsNull(2) Then
                                        _type = ""
                                    Else
                                        _type = .Rows(i).Item(2)
                                    End If

                                    If (item_code.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，部件号 / 产品号不能为空！');"
                                        myDataset.Reset()
                                        Return
                                    End If
                                    If (item_cat.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('文件格式错误(1) --行 " & i + 2.ToString & "，分类不能为空！');"
                                        myDataset.Reset()
                                        Return
                                    End If
                                    If (_type.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('文件格式错误(1) --行 " & i + 2.ToString & "，类型不能为空！');"
                                        myDataset.Reset()
                                        Return
                                    End If

                                    item_id = 0
                                    strSQL = "select id from ItemCategory where name='" & item_cat & "' "
                                    item_id = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)
                                    If item_id <= 0 Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & i + 2.ToString & "，该分类不存在！');"
                                        myDataset.Reset()
                                        Return
                                    End If

                                    If _type = "0" Then
                                        strSQL = "Update Items set category='" & item_id.ToString() & "' where code=N'" & item_code & "' and type=0"
                                    Else
                                        strSQL = "Update Items set category='" & item_id.ToString() & "' where code=N'" & item_code & "' and type=1"
                                    End If
                                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                End If
                            Next
                        End If
                    End With
                myDataset.Reset()
                ltlAlert.Text = "alert('导入成功.');"

                'Catch
                '    ltlAlert.Text = "alert('上传文件非法！');"
                '    Return
                'End Try
            End If
        End If
    End Sub
End Class

End Namespace
