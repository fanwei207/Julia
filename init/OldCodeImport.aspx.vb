Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc

    Partial Class OldCodeImport
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


            Dim reader As SqlDataReader

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
                    Dim _old_code, _new_code, _status As String
                    Dim id As Integer
                    Dim issize As String
                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset
                        If .Rows.Count > 0 Then
                            strSQL = " Delete From item_old_temp Where userID='" & Session("uID") & "' "
                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)


                            For i = 0 To .Rows.Count - 1
                                _old_code = Nothing
                                _new_code = Nothing
                                _status = Nothing
                                id = 0
                                issize = Nothing

                                If .Rows(i).IsNull(0) Then
                                    _old_code = ""
                                Else
                                    _old_code = .Rows(i).Item(0)
                                End If

                                If (_old_code.Trim().Length > 0) Then
                                    strSQL = " Select isnull(id,0) from Items where code=N'" & chk.sqlEncode(_old_code.Trim()) & "'"
                                    id = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If id <= 0 Then
                                        strSQL = "select top 1 id,old_code,new_code from item_old where old_code=N'" & chk.sqlEncode(_old_code.Trim()) & "' and new_code is not null "
                                        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSQL)
                                        If reader.HasRows Then
                                            While reader.Read()
                                                strSQL = " select isnull(id,0) from items where code=N'" & reader(2) & "' and num_per_box is not null and num_per_pack is not null and box_per_pallet is not null and box_size is not null"
                                                issize = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)
                                                If issize Is Nothing Or IsDBNull(issize) Then
                                                    issize = "False"
                                                Else
                                                    issize = "True"
                                                End If

                                                strSQL = " Select Case status When 0 Then N'使用' When 1 Then N'试用' Else N'停用' End From items Where code=N'" & reader(2) & "'"
                                                _status = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)

                                                strSQL = "insert into item_old_temp(id,old_code,new_code,userID,issize,status) values('" & i + 1 & "',N'" & chk.sqlEncode(_old_code.Trim()) & "',N'" & reader(2) & "','" & Session("uID") & "','" & issize & "',N'" & _status & "' )"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                                            End While
                                        Else
                                            strSQL = " select isnull(id,0) from items where code=N'" & find(_old_code.Trim()) & "' and num_per_box is not null and num_per_pack is not null and box_per_pallet is not null and box_size is not null"
                                            issize = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)
                                            If issize Is Nothing Or IsDBNull(issize) Then
                                                issize = "False"
                                            Else
                                                issize = "True"
                                            End If

                                            strSQL = " Select Case status When 0 Then N'使用' When 1 Then N'试用' Else N'停用' End From items Where code=N'" & find(_old_code.Trim()) & "'"
                                            _status = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)

                                            strSQL = "insert into item_old_temp(id,old_code,new_code,userID,issize,status) values('" & i + 1 & "',N'" & chk.sqlEncode(_old_code.Trim()) & "',N'" & find(_old_code.Trim()) & "','" & Session("uID") & "','" & issize & "',N'" & _status & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                        End If
                                        reader.Close()
                                    Else
                                        strSQL = " select isnull(id,0) from items where code=N'" & chk.sqlEncode(_old_code.Trim()) & "' and num_per_box is not null and num_per_pack is not null and box_per_pallet is not null and box_size is not null"
                                        issize = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)
                                        If issize Is Nothing Or IsDBNull(issize) Then
                                            issize = "False"
                                        Else
                                            issize = "True"
                                        End If

                                        strSQL = " Select Case status When 0 Then N'使用' When 1 Then N'试用' Else N'停用' End From items Where code=N'" & chk.sqlEncode(_old_code.Trim()) & "'"
                                        _status = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)

                                        strSQL = "insert into item_old_temp(id,old_code,new_code,userID,issize,status) values('" & i + 1 & "',N'" & chk.sqlEncode(_old_code.Trim()) & "',N'" & chk.sqlEncode(_old_code.Trim()) & "','" & Session("uID") & "','" & issize & "',N'" & _status & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                    End If
                                End If
                            Next
                        End If
                    End With
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If

                    strSQL = " Select o.old_code, o.new_code, o.issize, o.status, Case Isnull(i.type,1) When 0 Then N'部件' When 2 Then N'产品半成品' Else '' End From tcpc0.dbo.item_old_temp o Left Outer Join tcpc0.dbo.items i On i.code=o.new_code Where o.userID='" & Session("uid") & "' Order By o.id "
                    Session("EXSQL1") = strSQL
                    Session("EXTitle1") = "<b>旧部件信息</b>~^<b>新部件信息</b>~^<b>尺寸齐全</b>~^<b>状态</b>~^<b>分类</b>~^"

                    ltlAlert.Text = "window.open('/public/exportExcel1.aspx', '_blank');"
                End If
            End If
        End Sub

        Function find(ByVal oldcode As String) As String
            Try
                If oldcode.IndexOf("-成") > 0 Then
                    oldcode = oldcode.Replace("-成", "")
                End If
                If oldcode.IndexOf("-待") > 0 Then
                    oldcode = oldcode.Replace("-待", "")
                End If
                If oldcode.IndexOf("-移") > 0 Then
                    oldcode = oldcode.Replace("-移", "")
                End If
                If oldcode.IndexOf("P-") >= 0 Then
                    oldcode = oldcode.Remove(0, 2)
                End If
                If oldcode.IndexOf("/") > 0 Then
                    oldcode = oldcode.Replace("/", "-")
                End If
                If oldcode.IndexOf(" ") > 0 Then
                    oldcode = oldcode.Replace(" ", "-")
                End If
                ' -- WH、BK、Y、BL、RD、GR、OR、@@=GY、PUR .
                If oldcode.IndexOf("DXD") >= 0 Then
                    If oldcode.IndexOf("WH") > 0 Then
                        oldcode = oldcode.Replace("WH", "-WH-")
                    End If
                    If oldcode.IndexOf("BK") > 0 Then
                        oldcode = oldcode.Replace("BK", "-BK-")
                    End If
                    If oldcode.IndexOf("BL") > 0 Then
                        oldcode = oldcode.Replace("BL", "-BL-")
                    End If
                    If oldcode.IndexOf("RD") > 0 Then
                        oldcode = oldcode.Replace("RD", "-RD-")
                    End If
                    If oldcode.IndexOf("GR") > 0 Then
                        oldcode = oldcode.Replace("GR", "-GR-")
                    End If
                    If oldcode.IndexOf("OR") > 0 Then
                        oldcode = oldcode.Replace("OR", "-OR-")
                    End If
                    If oldcode.IndexOf("OR") > 0 Then
                        oldcode = oldcode.Replace("GY", "-GY-")
                    End If
                    If oldcode.IndexOf("Y") > 0 And oldcode.IndexOf("GY") <= 0 Then
                        oldcode = oldcode.Replace("Y", "-Y-")
                    End If
                    If oldcode.IndexOf("PUR") > 0 Then
                        oldcode = oldcode.Replace("PUR", "-PUR-")
                    End If
                End If

                If oldcode.IndexOf("BT") >= 0 Then
                    oldcode = oldcode.Replace("BT", "BT-")
                End If

                If oldcode.IndexOf("--") >= 0 Then
                    oldcode = oldcode.Replace("--", "-")
                End If

                If oldcode.Substring(0, 3) = "LS-" Then
                    oldcode = oldcode.Replace("LS-", "LS1-1-")
                Else
                    If oldcode.Substring(0, 2) = "LS" And oldcode.Substring(2, 1) <> "-" And oldcode.Substring(3, 1) = "-" Then
                        oldcode = oldcode.Insert(3, "-1")
                    End If
                End If
            Catch
                Response.Write(oldcode & "<br>")
            End Try

            Dim sqlstr As String
            sqlstr = "select isnull(id,0) from items where code=N'" & oldcode & "' "
            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, sqlstr) > 0 Then
                Return oldcode
            Else
                Return ""
            End If

        End Function

    End Class

End Namespace
