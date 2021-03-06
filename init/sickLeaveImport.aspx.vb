Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class sickLeaveImport
        Inherits BasePage
    Dim chk As New adamClass
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
            FileTypeDropDownList1.SelectedIndex = 0
            Dim item1 As ListItem
            item1 = New ListItem("Excel (.xls) file with Sheet1$")
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
        Dim reader As SqlDataReader

        strCatFolder = Server.MapPath("/import")
        If Not Directory.Exists(strCatFolder) Then
            Try
                Directory.CreateDirectory(strCatFolder)
            Catch
                ltlAlert.Text = "alert('创建文件目录失败(1001)！.')"
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
                Dim userNo, userName, dtime1, dtime2, notes As String

                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset
                        If .Rows.Count > 0 Then
                            strSQL = " delete from SickLeaveImport where creatBy=" & Session("uID")
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                            Dim str As String
                            For i = 0 To .Rows.Count - 1
                                'a
                                If .Rows(i).IsNull(0) Then
                                    userNo = ""
                                Else
                                    userNo = .Rows(i).Item(0)
                                End If
                                'b
                                If .Rows(i).IsNull(1) Then
                                    userName = ""
                                Else
                                    userName = .Rows(i).Item(1)
                                End If
                                'c
                                If .Rows(i).IsNull(2) Then
                                    dtime1 = ""
                                Else
                                    dtime1 = .Rows(i).Item(2)
                                End If
                                'd
                                If .Rows(i).IsNull(3) Then
                                    dtime2 = ""
                                Else
                                    dtime2 = .Rows(i).Item(3)
                                End If
                                'e
                                If .Rows(i).IsNull(4) Then
                                    notes = ""
                                Else
                                    notes = .Rows(i).Item(4)
                                End If

                                If userNo.Trim().Length <= 0 And userName.Trim().Length <= 0 And dtime1.Trim().Length <= 0 Then

                                Else
                                    If (userNo.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，工号不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (userName.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，姓名不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    strSQL = " select u.userid from tcpc0.dbo.users u where u.userName= N'" & userName & "' and u.userNo='" & userNo.Trim & "' and u.plantCode='" & Session("PlantCode") & "' "
                                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) <= 0 Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，工号和姓名不匹配！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (dtime1.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，起始时间不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    Else
                                        If (IsDate(dtime1.Trim()) = False) Then
                                            ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，起始时间格式非法！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If
                                    If (dtime2.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，结束时间不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    Else
                                        If (IsDate(dtime2.Trim()) = False) Then
                                            ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，结束时间格式非法！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If

                                    If (Year(dtime1) <> Year(dtime2) Or Month(dtime1) <> Month(dtime2)) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，起始时间和结束时间不是同一个月！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    strSQL = "Insert Into SickLeaveImport(usercode,username,startdate,enddate,comment,creatby,creatdate,organizationID) "
                                    strSQL &= " Values('" & userNo & "','" & userName & "','" & dtime1 & "','" & dtime2 & "','" & notes & "','" & Session("uID") & "',getdate(),1)"
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                End If
                            Next
                            Dim mistake As Integer = 0
                            strSQL = "Salary_MoveSickLeaveImport"
                            Dim param As SqlParameter
                            param = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                            mistake = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, param)
                            If mistake > 0 Then
                                ltlAlert.Text = "alert('病假信息导入成功.');"
                            Else
                                ltlAlert.Text = "alert('病假信息导入失败,请重新导入.');"
                            End If

                        End If
                    End With
                myDataset.Reset()
                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If
            End If
        End If
    End Sub

End Class

End Namespace
