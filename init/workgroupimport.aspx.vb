Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc

Partial Class workgroupimport
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
                Dim name As String
                Dim code As String
                Dim department As String
                Dim workshop As String
                Try
                        Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                        With myDataset
                            If .Rows.Count > 0 Then
                                For i = 0 To .Rows.Count - 1
                                    If .Rows(i).IsNull(0) Then
                                        name = ""
                                    Else
                                        name = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        code = ""
                                    Else
                                        code = .Rows(i).Item(1)
                                    End If
                                    If .Rows(i).IsNull(2) Then
                                        department = ""
                                    Else
                                        department = .Rows(i).Item(2)
                                    End If
                                    If .Rows(i).IsNull(3) Then
                                        workshop = -1
                                    Else
                                        workshop = .Rows(i).Item(3)
                                    End If

                                    If (name.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，班组名称不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                    If (workshop.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，工段不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                    If (name.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，班组名称不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                    If (department.Trim().Length > 0) Then
                                        strSQL = " select departmentID from departments where Name= N'" & department & "'"
                                        department = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                        If department = Nothing Then
                                            ltlAlert.Text = "alert('文件格式错误(3) --行 " & (i + 2).ToString & "，所输入的部门不存在！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If
                                    If (workshop.Trim().Length > 0) Then
                                        strSQL = " select isnull(ID,0) from workshop where Name= N'" & workshop & "'and departmentID='" & department & "' "
                                        workshop = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                        If workshop = Nothing Then
                                            ltlAlert.Text = "alert('文件格式错误(3) --行 " & (i + 2).ToString & "，所输入的工段不存在！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If
                                    'strSQL = " select workshopID from workshop where workshopID='" & workshop & "' "
                                    'workshop = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                    'if
                                    Dim reader As SqlDataReader
                                    strSQL = " select workshopID from workshop where workshopID='" & workshop & "' and Name= N'" & name & "' "
                                    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                                    If reader.Read() Then
                                        ltlAlert.Text = "alert('文件格式错误(1) --行 " & i + 2.ToString & "，该班组已经存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                    reader.Close()

                                    strSQL1 = " Insert Into workshop(name, code, departmentID,workshopID) " _
                                              & " Values(N'" & name & "','" & code & "','" & department & "','" & workshop & "') "

                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL1)
                                Next
                            End If
                        End With
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    ltlAlert.Text = "alert('班组信息导入成功.');"

                Catch
                    ltlAlert.Text = "alert('上传文件非法！');"
                    Return
                End Try
            End If
        End If
    End Sub
End Class

End Namespace
