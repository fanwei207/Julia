'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-1-26
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Parts Qty Init From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class PurchaseQtyInit
        Inherits BasePage
    Dim strSQL As String
    'Protected WithEvents ltlAlert As Literal
    Public chk As New adamClass

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
        Dim ds As DataSet
        Dim reader As SqlDataReader

        Dim ret As Integer
        Dim retRecord As Integer = 0

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer

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
        While (i < 2000)
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
                Dim _code, _enterdate, _enterqty, _place, _notes, partID, placeID, _status, statusID As String
                Dim boolError As Boolean = False
                Dim boolError1 As Boolean = False
                    'Dim myDataset As DataSet
                    Dim mydt As DataTable
                Try
                        'myDataset = chk.getExcelContents(strFileName)
                        mydt = GetExcelContents(strFileName)
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

                'Try
                    With mydt
                        If .Rows.Count > 0 Then
                            If .Columns(0).ColumnName <> "材料代码" Or .Columns(5).ColumnName <> "状态(可空)" Then
                                'myDataset.Reset()
                                ltlAlert.Text = "alert('导入文件不是部件初始化导入模版.'); "
                                Exit Sub
                            End If

                            strSQL = " Delete From PartQtyImportError Where userID='" & Session("uID") & "'"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                            For i = 0 To .Rows.Count - 1
                                _code = Nothing
                                _enterdate = Nothing
                                _enterqty = Nothing
                                _place = Nothing
                                _notes = Nothing
                                partID = Nothing
                                placeID = Nothing
                                boolError = False
                                boolError1 = False
                                _status = Nothing
                                statusID = Nothing

                                If .Rows(i).IsNull(0) Then
                                    _code = " "
                                Else
                                    _code = .Rows(i).Item(0)
                                End If

                                If .Rows(i).IsNull(1) Then
                                    _enterdate = ""
                                Else
                                    _enterdate = .Rows(i).Item(1)
                                End If

                                If .Rows(i).IsNull(2) Then
                                    _enterqty = ""
                                Else
                                    _enterqty = .Rows(i).Item(2)
                                End If

                                If .Rows(i).IsNull(3) Then
                                    _place = ""
                                Else
                                    _place = .Rows(i).Item(3)
                                End If

                                If .Rows(i).IsNull(4) Then
                                    _notes = ""
                                Else
                                    _notes = .Rows(i).Item(4)
                                End If

                                If .Rows(i).IsNull(5) Then
                                    _status = ""
                                Else
                                    _status = .Rows(i).Item(5)
                                End If

                                'fileds validation 
                                If _code.Trim().Length > 0 Then
                                    strSQL = " Select id  From Items Where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                    partID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If partID = Nothing Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & _code.Trim() & "：材料代码不存在','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    End If

                                    If (_enterdate.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & "：日期不能为空','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    ElseIf IsDate(_enterdate.Trim()) = False Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & "：日期非法','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    End If

                                    If (_enterqty.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & "：数量不能为空','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    ElseIf IsNumeric(_enterqty.Trim()) = False Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & "：数量不是数字','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    ElseIf Val(_enterqty.Trim()) <= 0 Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & "：数量不能小于等于零','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    End If

                                    If (_place.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & "：仓库名称不能为空','" & Session("uID") & "')"
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
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行" & (i + 2).ToString & "：仓库名称不存在或无权操作该仓库','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If
                                    End If

                                    If _status.Trim().Length > 0 Then
                                        strSQL = " Select id From tcpc0.dbo.Status Where statusName=N'" & chk.sqlEncode(_status.Trim()) & "'"
                                        statusID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                        If statusID = Nothing Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行" & (i + 2).ToString & "：输入的状态不存在','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If
                                    Else
                                        statusID = "0"
                                    End If

                                    If _notes.Trim().Length > 255 Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & "：输入的备注长度超过255','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    End If

                                    If boolError = False Then
                                        strSQL = "Purchase_PartInit"
                                        Dim params(7) As SqlParameter
                                        params(0) = New SqlParameter("@whid", Convert.ToInt32(placeID))
                                        params(1) = New SqlParameter("@partid", Convert.ToInt32(partID))
                                        params(2) = New SqlParameter("@inQty", Convert.ToDecimal(_enterqty.Trim()))
                                        params(3) = New SqlParameter("@inDate", Convert.ToDateTime(_enterdate.Trim()))
                                        params(4) = New SqlParameter("@notes", chk.sqlEncode(_notes.Trim))
                                        params(5) = New SqlParameter("@status", Convert.ToInt32(statusID.Trim()))
                                        params(6) = New SqlParameter("@line", i + 2)
                                        params(7) = New SqlParameter("@intUserID", Convert.ToInt32(Session("uID")))

                                        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                        If ret < 0 Then
                                            boolError1 = True
                                        End If
                                    End If
                                    If boolError1 = True Or boolError = True Then
                                        retRecord = retRecord + 1
                                    End If
                                End If
                            Next
                        End If
                    End With
                    
                    If retRecord = 0 Then
                        ltlAlert.Text = "alert('材料库存初始化导入成功！');window.location.href='/Purchase/PurchaseQtyInit.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                    Else
                        ltlAlert.Text = "alert('材料库存初始化导入有错误！');window.location.href='/Purchase/PurchaseQtyInit.aspx?err=y&rm=" & DateTime.Now() & Rnd() & "';"
                    End If
                    'Catch
                    '    ltlAlert.Text = "alert('上传文件非法，材料库存初始化失败！');"
                    '    Return
                    'End Try
            End If
        End If
    End Sub

End Class

End Namespace
