Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc

Partial Class CategoryImport
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
        Dim param(3) As SqlParameter

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
                Dim category, user_no As String
                Dim categoryID, userID, j As Integer
                Dim err As String = ""
                Dim ht As New Hashtable

                strSQL = "delete from ImportError where userID=" & Session("uID") & " and plantID='" & Session("PlantCode") & "' "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                'Try
                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset
                        If .Rows.Count > 0 Then
                            For i = 0 To .Rows.Count - 1
                                category = Nothing
                                user_no = Nothing
                                categoryID = 0
                                userID = 0
                                j = 1

                                err = ""

                                If .Rows(i).IsNull(0) Then
                                    category = ""
                                Else
                                    category = .Rows(i).Item(0)
                                End If

                                If .Rows(i).IsNull(1) Then
                                    user_no = ""
                                Else
                                    user_no = .Rows(i).Item(1)
                                End If

                                If category.Trim.Length > 0 And user_no.Trim.Length > 0 Then
                                    strSQL = "Select id from Itemcategory where name=N'" & category & "' and type=0 "
                                    categoryID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If categoryID > 0 Then
                                        While user_no.Length > 0
                                            If user_no.IndexOf(",") > 0 Then
                                                ht.Add(j, user_no.Trim().Substring(0, user_no.Trim().IndexOf(",")))
                                                user_no = user_no.Substring(user_no.IndexOf(",") + 1)
                                            Else
                                                ht.Add(j, user_no.Trim())
                                                user_no = ""
                                            End If
                                            j = j + 1
                                        End While

                                        Dim myenum As IDictionaryEnumerator = ht.GetEnumerator
                                        While myenum.MoveNext
                                            strSQL = "select userID from users where userno='" & myenum.Value & "'"
                                            userID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                            If userID > 0 Then
                                                strSQL = " insert into tcpc0.dbo.User_pricesbycategory(userID,categoryID) values('" & userID & "','" & categoryID & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                            Else
                                                err = err & "行 " & i + 2.ToString & "，此工号" & myenum.Value & "不存在！"
                                            End If

                                        End While
                                    Else
                                        err = err & "行 " & i + 2.ToString & "，部件分类" & category & "不存在！"
                                    End If
                                    If err <> "" Then
                                        strSQL = " insert into ImportError(ErrorInfo,userid,plantid) values(N'" & err & "'," & Session("uID") & "," & Session("PlantCode") & ")"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                        err = ""
                                    End If
                                End If
                            Next
                        End If
                    End With
                myDataset.Reset()
                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If
                ' ltlAlert.Text = "alert('导入成功.'); window.location.href='/supply/PartSupplyImport.aspx';"
                'Catch
                '    ltlAlert.Text = "alert('上传文件非法！');"
                '    ' Return
                'End Try
                strSQL = " select count(*) from ImportError where userID=" & Session("uID") & " and plantID=" & Session("PlantCode") & ""
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) Then
                    strSQL = "select ErrorInfo From tcpc0.dbo.ImportError where userID='" & Session("uid") & "'and plantID=" & Session("PlantCode") & " "
                    Session("EXSQL1") = strSQL
                    Session("EXTitle1") = "<b>出错信息</b>~^"

                    Response.Redirect("/public/exportExcel1.aspx", True)
                End If
            End If
        End If
    End Sub

End Class

End Namespace

