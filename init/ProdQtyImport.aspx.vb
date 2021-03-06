'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-02-16
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Products Trans From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class ProdQtyImport
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents Comparevalidator3 As System.Web.UI.WebControls.CompareValidator

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
                Session("EXSQL1") = " Select ErrorInfo From PartQtyImportError Where userID='" & Session("uID") & "' order by id"
                ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
            End If

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
        Dim reader As SqlDataReader

        Dim ret As Integer
        Dim retRecord As Integer = 0

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer

        Dim errorflag As Boolean = False

        strCatFolder = Server.MapPath("/import")
        If Not Directory.Exists(strCatFolder) Then
            Try
                Directory.CreateDirectory(strCatFolder)
            Catch
                ltlAlert.Text = "alert('创建文件目录失败.')"
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
                Dim _type As String
                Dim _code As String
                Dim _enterdate As String
                Dim _enterqty As String
                Dim _place As String
                Dim _status As String
                Dim _notes As String

                Dim prodID As String
                Dim placeID As String
                Dim statusID As String
                    Dim myDataset As DataTable

                Try
                        myDataset = Me.GetExcelContents(strFileName)
                Catch
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    ltlAlert.Text = "alert('导入文件必须是Excel格式.');"
                    Exit Sub
                Finally
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                End Try

                Dim total2 As Integer = 0
                Try
                        With myDataset
                            If .Rows.Count > 0 Then
                                If .Columns(0).ColumnName <> "类型" Or .Columns(1).ColumnName <> "产品型号" Then
                                    myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是产品进出库导入模版.'); "
                                    Exit Sub
                                End If

                                strSQL = " Delete From PartQtyImportError Where userID='" & Session("uID") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                                total2 = .Rows.Count
                                For i = 0 To .Rows.Count - 1
                                    prodID = ""
                                    placeID = ""
                                    errorflag = False
                                    _type = Nothing
                                    _code = Nothing
                                    _enterdate = Nothing
                                    _enterqty = Nothing
                                    _place = Nothing
                                    _status = Nothing
                                    statusID = ""
                                    _notes = Nothing

                                    If .Rows(i).IsNull(0) Then
                                        _type = ""
                                    Else
                                        _type = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _code = " "
                                    Else
                                        _code = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        _enterdate = ""
                                    Else
                                        _enterdate = .Rows(i).Item(2)
                                    End If

                                    If .Rows(i).IsNull(3) Then
                                        _enterqty = ""
                                    Else
                                        _enterqty = .Rows(i).Item(3)
                                    End If

                                    If .Rows(i).IsNull(4) Then
                                        _place = ""
                                    Else
                                        _place = .Rows(i).Item(4)
                                    End If

                                    If .Rows(i).IsNull(5) Then
                                        _status = ""
                                    Else
                                        _status = .Rows(i).Item(5)
                                    End If

                                    If .Rows(i).IsNull(6) Then
                                        _notes = ""
                                    Else
                                        _notes = .Rows(i).Item(6)
                                    End If

                                    'fileds validation 
                                    If _type.Trim().Length > 0 And _code.Trim().Length > 0 Then
                                        If _type.Trim().Length <= 0 Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & ":类型不能为空','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        ElseIf UCase(_type.Trim()) <> "IN" And UCase(_type.Trim()) <> "OUT" And UCase(_type.Trim()) <> "RET" And UCase(_type.Trim()) <> "MV" And UCase(_type.Trim()) <> "DOUT" And UCase(_type.Trim()) <> "DIN" Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & ":类型必须是IN、OUT、RET、MV、DOUT、DIN','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If

                                        If UCase(_type.Trim()) = "IN" Then
                                            _type = "I"
                                        ElseIf UCase(_type.Trim()) = "OUT" Then
                                            _type = "O"
                                        ElseIf UCase(_type.Trim()) = "RET" Then
                                            _type = "R"
                                        ElseIf UCase(_type.Trim()) = "DOUT" Then
                                            _type = "DO"
                                        ElseIf UCase(_type.Trim()) = "DIN" Then
                                            _type = "DI"
                                        ElseIf UCase(_type.Trim()) = "MV" Then
                                            _type = "M"
                                        End If

                                        If _code.Trim().Length <= 0 Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "：产品代码不能为空','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        Else
                                            strSQL = " Select id  From Items Where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                            prodID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                            If prodID = Nothing Then
                                                errorflag = True
                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "：" & _code.Trim() & "产品代码不存在或该产品已经停用','" & Session("uID") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                            End If
                                        End If

                                        If _enterdate.Trim().Length <= 0 Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "：日期不能为空','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        ElseIf IsDate(_enterdate.Trim()) = False Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "：日期非法','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If

                                        If (_enterqty.Trim().Length <= 0) Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "：数量不能为空','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        ElseIf IsNumeric(_enterqty.Trim()) = False Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "：数量不是数字','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        ElseIf Val(_enterqty.Trim()) <= 0 Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                     & " Values(N'行 " & (i + 2).ToString & "：数量必须大于零','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        ElseIf CInt(_enterqty.Trim()) <> _enterqty.Trim() Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "：数量不是整数','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If

                                        If (_place.Trim().Length <= 0) Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "：仓库名称不能为空','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        Else
                                            strSQL = " Select w.warehouseID From warehouse w "
                                            If Session("uRole") <> 1 Then
                                                strSQL &= " Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "' And w.name=N'" & chk.sqlEncode(_place.Trim()) & "'"
                                            Else
                                                strSQL &= " Where w.name=N'" & chk.sqlEncode(_place.Trim()) & "'"
                                            End If
                                            placeID = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strSQL)
                                            If placeID = Nothing Then
                                                errorflag = True
                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "：仓库名称不存在或无权操作该仓库','" & Session("uID") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                            End If
                                        End If

                                        If _status.Trim().Length > 0 Then
                                            strSQL = " Select id From tcpc0.dbo.Status Where statusName=N'" & chk.sqlEncode(_status.Trim()) & "'"
                                            statusID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                            If statusID = Nothing Then
                                                errorflag = True
                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "输入的状态不存在','" & Session("uID") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            statusID = "0"
                                        End If

                                        If _status.Trim().Length > 255 Then
                                            errorflag = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "输入的备注长度太长','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If

                                        If errorflag = False Then
                                            strSQL = "ProductInventory_ProdInOut_Insert_Temp"
                                            Dim params(8) As SqlParameter
                                            params(0) = New SqlParameter("@type", _type.Trim())
                                            params(1) = New SqlParameter("@placeid", Convert.ToInt32(placeID))
                                            params(2) = New SqlParameter("@prodid", Convert.ToInt32(prodID))
                                            params(3) = New SqlParameter("@enterdate", Convert.ToDateTime(_enterdate.Trim))
                                            params(4) = New SqlParameter("@enterqty", Convert.ToInt32(_enterqty))
                                            params(5) = New SqlParameter("@intUserID", Convert.ToInt32(Session("uID")))
                                            params(6) = New SqlParameter("@status", Convert.ToInt32(statusID.Trim()))
                                            params(7) = New SqlParameter("@notes", _notes.Trim())
                                            params(8) = New SqlParameter("@line", Convert.ToInt32(i + 2))

                                            ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                            If ret < 0 Then
                                                errorflag = True
                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                & " Values(N'行 " & (i + 2).ToString & "ERR" & ret.ToString() & "','" & Session("uID") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                            End If
                                        End If
                                        If errorflag = True Then
                                            retRecord = retRecord + 1
                                        End If
                                    End If
                                Next
                            Else
                                errorflag = True
                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                       & " Values(N'行 " & (i + 2).ToString & "TYPE或CODE不存在','" & Session("uID") & "')"
                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                            End If
                        End With
                    myDataset.Reset()

                    If retRecord > 0 Then
                        ltlAlert.Text = "alert('产品进出库导入结束，有错误！'); window.location.href='/Init/ProdQtyImport.aspx?sum=" & total2.ToString() & "&err=y&rm=" & DateTime.Now() & Rnd() & "';"
                    Else
                        ltlAlert.Text = "alert('产品进出库导入成功！'); window.location.href='/Init/ProdQtyImport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                    End If
                Catch
                    Response.Write(strSQL)

                    ltlAlert.Text = "alert('产品进出库失败！');"
                    Return
                End Try
            End If
        End If
    End Sub

End Class

End Namespace

