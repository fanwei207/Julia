Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb

Namespace tcpc
Partial Class workshopimport
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
                ltlAlert.Text = "alert('创建文件目录失败(1001)！.')"
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
                Dim name As String
                Dim code As String
                Dim department As String
                Try
                        Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                        With myDataset
                            If .Rows.Count > 0 Then
                                Dim did As Integer
                                For i = 0 To .Rows.Count - 1
                                    did = 0
                                    If .Rows(i).IsNull(0) Then
                                        name = ""
                                    Else
                                        name = .Rows(i).Item(0).ToString.Trim()
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        code = " "
                                    Else
                                        code = .Rows(i).Item(1).ToString.Trim()
                                    End If
                                    If .Rows(i).IsNull(2) Then
                                        department = ""
                                    Else
                                        department = .Rows(i).Item(2).ToString.Trim()
                                    End If

                                    'Response.Write(name & "-" & department & "<br>")

                                    If (name.Length > 0 And department.Length > 0) Then
                                        strSQL = " select departmentID from departments where name= N'" & department & "'"
                                        did = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                        If did > 0 Then
                                            strSQL = " select id from workshop where name= N'" & name & "' and departmentID=" & did
                                            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) <= 0 Then
                                                strSQL1 = " Insert Into workshop(name, code, departmentID) " _
                                                          & " Values(N'" & name & "',N'" & code & "',N'" & did.ToString() & "') "
                                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL1)
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
                    ltlAlert.Text = "alert('工段信息导入成功.');"
                Catch
                    ltlAlert.Text = "alert('上传文件非法！');"
                    Return
                End Try
            End If
        End If
    End Sub
End Class

End Namespace
