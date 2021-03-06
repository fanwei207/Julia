'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-8-26
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Parts Inventory From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc

Partial Class PartInvImport
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
        Dim ds1 As DataSet

        Dim con As SqlConnection = New SqlConnection(chk.dsnx)
        Dim cmd As SqlCommand
        Dim param As New SqlParameter
        Dim retValue As Integer

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
                Dim _code As String
                Dim _place As String
                Dim _invqty As String
                Dim placeID As String
                Dim partID As String

                Try
                        'Dim myDataset As DataSet = chk.getExcelContents(strFileName)
                        Dim mydt As DataTable = GetExcelContents(strFileName)
                        With mydt
                            If .Rows.Count > 0 Then
                                strSQL = " Delete From PartInvQtyImportTemp Where userID='" & Session("uID") & "' "
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                                For i = 0 To .Rows.Count - 1
                                    If .Rows(i).IsNull(0) Then
                                        _code = ""
                                    Else
                                        _code = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _invqty = " "
                                    Else
                                        _invqty = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        _place = " "
                                    Else
                                        _place = .Rows(i).Item(2)
                                    End If

                                    'fileds validation 
                                    If _code.Trim().Length <= 0 Then
                                        ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，部件号不能为空！');"
                                        'myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If _invqty.Trim().Length <= 0 Then
                                        ltlAlert.Text = "alert('文件格式错误(1) --行 " & (i + 2).ToString & "，部件库存不能为空！');"
                                        'myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf IsNumeric(_invqty.Trim()) = False Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，部件库存数量不是数字！');"
                                        'myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf Val(_invqty.Trim()) < 0 Then
                                        ltlAlert.Text = "alert('文件格式错误(3) --行 " & (i + 2).ToString & "，部件库存数量不能小于零！');"
                                        'myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (_place.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('文件格式错误(4) --行 " & (i + 2).ToString & "，仓库名称不能为空！');"
                                        'myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    Else
                                        strSQL = " Select w.warehouseID From warehouse w "
                                        If Session("uRole") <> 1 Then
                                            strSQL &= " Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "'"
                                        End If
                                        strSQL &= " Where w.name=N'" & chk.sqlEncode(_place.Trim()) & "'"
                                        placeID = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strSQL)
                                        If placeID = Nothing Then
                                            ltlAlert.Text = "alert('文件格式错误(5) --行 " & (i + 2).ToString & "，" & _place.Trim() & "仓库名称不存在或者无权操作该仓库！');"
                                            'myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If

                                    strSQL = " Select id  From Items Where code='" & chk.sqlEncode(_code.Trim()) & "' and Status<>2 And type<>2 "
                                    partID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If partID = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(6) --行 " & (i + 2).ToString & "，" & _code.Trim() & "材料代码不存在或者该材料已经停用！');"
                                        'myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    strSQL = " Insert Into PartInvQtyImportTemp(partID, invqty, placeID, userID, plantID) " _
                                           & " Values('" & partID.Trim() & "','" & _invqty.Trim() & "','" & placeID.Trim() & "','" & Session("uID") & "','" & Session("plantcode") & "')"
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                Next

                                'process part inventory qty import
                                cmd = New SqlCommand("Purchase_PartInvQtyImport", con)
                                If con.State = ConnectionState.Closed Then
                                    con.Open()
                                End If
                                cmd.CommandType = CommandType.StoredProcedure

                                param = cmd.Parameters.Add("@intUserID", SqlDbType.Int)
                                param.Direction = ParameterDirection.Input
                                param.Value = Session("uID")

                                Try
                                    param = cmd.Parameters.Add("param", SqlDbType.Int)
                                    param.Direction = ParameterDirection.ReturnValue

                                    cmd.ExecuteReader()
                                    retValue = param.Value
                                    con.Close()
                                Catch
                                End Try
                            End If
                        End With
                        'myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    If retValue = 0 Then
                        ltlAlert.Text = "alert('材料库存初始化导入成功！');window.location.href='/Purchase/PartInvImport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                    Else
                        ltlAlert.Text = "alert('材料库存初始化导入失败！');window.location.href='/Purchase/PartInvImport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                    End If
                Catch
                    ltlAlert.Text = "alert('上传文件非法！');"
                    Return
                End Try
            End If
        End If
    End Sub

End Class

End Namespace

