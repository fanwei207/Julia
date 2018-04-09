

Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb



Partial Class part_partSizeImport
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
        Dim s As Integer
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
                ltlAlert.Text = "alert('上传文件失败.')"
                Return
            End Try

            If (File.Exists(strFileName)) Then
                Dim product_code As String
                ' Dim num_in_box As String
                Dim box_weight As String
                Dim box_size As String
                Dim box_l As String
                Dim box_w As String
                Dim box_h As String
                'Dim box_in_pack As String
                'Dim pack_on_pallet As String

                Dim errorStr As String = ""
                Dim id As Integer = 0
                Dim numver As Integer

                Dim weight As Decimal
                Dim size As Decimal
                'Dim box As Integer
                ' Dim pack As Integer
                ' Dim pallet As Integer

                Dim myDataset As DataTable
                Try
                    myDataset = Me.GetExcelContents(strFileName)
                Catch
                    ltlAlert.Text = "alert('导入文件格式必须是Excel文件。');"
                    Return
                End Try

                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If

                Dim total As Integer = 0
                Try

                    With myDataset
                        If .Rows.Count > 0 Then
                            s = .Rows.Count
                            For i = 0 To .Rows.Count - 1
                                If Not .Rows(i).IsNull(0) And Not .Rows(i).IsNull(1) Then
                                    weight = 0.0
                                    size = 0.0
                                    'box = 0
                                    'pack = 0
                                    'pallet = 0
                                    If .Rows(i).IsNull(0) Then
                                        product_code = ""
                                    Else
                                        product_code = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        box_weight = "0.0"
                                    Else
                                        box_weight = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        box_size = "0.0"
                                    Else
                                        box_size = .Rows(i).Item(2)
                                    End If

                                    If .Rows(i).IsNull(3) Then
                                        box_l = "0"
                                    Else
                                        box_l = .Rows(i).Item(3)
                                    End If
                                    If .Rows(i).IsNull(4) Then
                                        box_w = "0"
                                    Else
                                        box_w = .Rows(i).Item(4)
                                    End If
                                    If .Rows(i).IsNull(5) Then
                                        box_h = "0"
                                    Else
                                        box_h = .Rows(i).Item(5)
                                    End If

                                    'If .Rows(i).IsNull(6) Then
                                    '    box_in_pack = "0"
                                    'Else
                                    '    box_in_pack = .Rows(i).Item(6)
                                    'End If

                                    'If .Rows(i).IsNull(7) Then
                                    '    num_in_box = "0"
                                    'Else
                                    '    num_in_box = .Rows(i).Item(7)
                                    'End If

                                    'If .Rows(i).IsNull(8) Then
                                    '    pack_on_pallet = "0"
                                    'Else
                                    '    pack_on_pallet = .Rows(i).Item(8)
                                    'End If

                                    'fileds validation 
                                    If (product_code.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行" & i + 2.ToString & ",产品型号不能为空！');"
                                        myDataset.Reset()
                                        Return
                                    End If

                                    If IsNumeric(box_weight) = False Then
                                        ltlAlert.Text = "alert('行" & i + 2.ToString & ",重量必须是数字！');"
                                        myDataset.Reset()
                                        Return
                                    ElseIf Val(box_weight) <= 0 Then
                                        ltlAlert.Text = "alert('行" & i + 2.ToString & ",重量不能小于等于0！');"
                                        myDataset.Reset()
                                        Return
                                    Else
                                        weight = CDbl(box_weight)
                                    End If

                                    If box_size <> "0.0" Then
                                        If IsNumeric(box_size) = False Then
                                            ltlAlert.Text = "alert('行" & i + 2.ToString & ",体积必须是数字！');"
                                            myDataset.Reset()
                                            Return
                                        ElseIf Val(box_size) <= 0 Then
                                            ltlAlert.Text = "alert('行" & i + 2.ToString & ",体积不能小于等于0！');"
                                            myDataset.Reset()
                                            Return
                                        Else
                                            size = CDbl(box_size)
                                        End If
                                    Else
                                        If IsNumeric(box_l) = False Or IsNumeric(box_w) = False Or IsNumeric(box_h) = False Then
                                            ltlAlert.Text = "alert('行" & i + 2.ToString & ",长宽高必须是数字！');"
                                            myDataset.Reset()
                                            Return
                                        ElseIf Val(box_l) <= 0 Or Val(box_w) <= 0 Or Val(box_h) <= 0 Then
                                            ltlAlert.Text = "alert('行" & i + 2.ToString & ",长宽高不能小于等于0！');"
                                            myDataset.Reset()
                                            Return
                                        Else
                                            size = CDbl(box_l / 100.0) * CDbl(box_w / 100.0) * CDbl(box_h / 100.0)
                                        End If
                                    End If

                                    'If IsNumeric(box_in_pack) = False Then
                                    '    ltlAlert.Text = "alert('行" & i + 2.ToString & ",只数/套必须是数字！');"
                                    '    myDataset.Reset()
                                    '    Return
                                    'ElseIf Val(box_in_pack) <= 0 Then
                                    '    ltlAlert.Text = "alert('行" & i + 2.ToString & ",只数/套不能小于等于0！');"
                                    '    myDataset.Reset()
                                    '    Return
                                    'ElseIf CInt(box_in_pack) <> box_in_pack Then
                                    '    ltlAlert.Text = "alert('行" & i + 2.ToString & ",只数/套不能是小数！');"
                                    '    myDataset.Reset()
                                    '    Return
                                    'Else
                                    '    pack = CInt(box_in_pack)
                                    'End If

                                    'If IsNumeric(num_in_box) = False Then
                                    '    ltlAlert.Text = "alert('行" & i + 2.ToString & ",只数/箱必须是数字！');"
                                    '    myDataset.Reset()
                                    '    Return
                                    'ElseIf Val(num_in_box) <= 0 Then
                                    '    ltlAlert.Text = "alert('行" & i + 2.ToString & ",只数/箱不能小于等于0！');"
                                    '    myDataset.Reset()
                                    '    Return
                                    'ElseIf CInt(num_in_box) <> num_in_box Then
                                    '    ltlAlert.Text = "alert('行" & i + 2.ToString & ",只数/箱不能是小数！');"
                                    '    myDataset.Reset()
                                    '    Return
                                    'Else
                                    '    box = CInt(num_in_box)
                                    'End If

                                    'If IsNumeric(pack_on_pallet) = False Then
                                    '    ltlAlert.Text = "alert('行" & i + 2.ToString & ",箱数/货盘必须是数字！');"
                                    '    myDataset.Reset()
                                    '    Return
                                    'ElseIf Val(pack_on_pallet) < 0 Then
                                    '    ltlAlert.Text = "alert('行" & i + 2.ToString & ",箱数/货盘不能小于0！');"
                                    '    myDataset.Reset()
                                    '    Return
                                    'ElseIf CInt(pack_on_pallet) <> pack_on_pallet Then
                                    '    ltlAlert.Text = "alert('行" & i + 2.ToString & ",箱数/货盘不能是小数！');"
                                    '    myDataset.Reset()
                                    '    Return
                                    'Else
                                    '    pallet = CInt(pack_on_pallet)
                                    'End If

                                    strSQL = " Select id from Items where code=N'" & chk.sqlEncode(product_code.Trim()) & "' And type=0 "
                                    id = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If id > 0 Then
                                        strSQL = " Select top 1 isnull(version,0) From product_physical_his Where prod_id=" & id & " Order By CreatedDate Desc "
                                        numver = CInt(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)) + 1

                                        strSQL = " Update Items Set "
                                        'If box > 0 Then
                                        '    strSQL &= " num_per_box='" & box & "',"
                                        'End If
                                        If weight > 0 Then
                                            strSQL &= " box_weight='" & weight & "',"
                                        End If
                                        If size > 0 Then
                                            strSQL &= " box_size='" & size & "',"
                                        End If
                                        'If pack > 0 Then
                                        '    strSQL &= " num_per_pack='" & pack & "',"
                                        'End If
                                        'If pallet > 0 Then
                                        '    strSQL &= " box_per_pallet='" & pallet & "',"
                                        'End If
                                        If box_l > 0 Then
                                            strSQL &= " box_length='" & box_l & "',"
                                        End If
                                        If box_w > 0 Then
                                            strSQL &= " box_width='" & box_w & "',"
                                        End If
                                        If box_h > 0 Then
                                            strSQL &= " box_depth='" & box_h & "',"
                                        End If
                                        strSQL &= " modifiedBy='" & Session("uID") & "'," _
                                               & " modifiedDate='" & DateTime.Now() & "'" _
                                               & " Where id='" & id & "'"
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                                        strSQL = " Insert Into product_physical_his(prod_id, "
                                        'If box > 0 Then
                                        '    strSQL &= " num_per_box, "
                                        'End If
                                        If weight > 0 Then
                                            strSQL &= " box_weight,"
                                        End If
                                        If size > 0 Then
                                            strSQL &= " box_size,"
                                        End If
                                        'If pack > 0 Then
                                        '    strSQL &= " num_per_pack,"
                                        'End If
                                        'If pallet > 0 Then
                                        '    strSQL &= " box_per_pallet,"
                                        'End If
                                        If box_l > 0 Then
                                            strSQL &= " box_length,"
                                        End If
                                        If box_w > 0 Then
                                            strSQL &= " box_width,"
                                        End If
                                        If box_h > 0 Then
                                            strSQL &= " box_depth,"
                                        End If
                                        strSQL &= " createdBy, createdDate, plantCode, version) " _
                                               & " Values('" & id
                                        'If box > 0 Then
                                        '    strSQL &= "','" & box
                                        'End If
                                        If weight > 0 Then
                                            strSQL &= "','" & weight
                                        End If
                                        If size > 0 Then
                                            strSQL &= "','" & size
                                        End If
                                        'If pack > 0 Then
                                        '    strSQL &= "','" & pack
                                        'End If
                                        'If pallet > 0 Then
                                        '    strSQL &= "','" & pallet
                                        'End If
                                        If box_l > 0 Then
                                            strSQL &= "','" & box_l
                                        End If
                                        If box_w > 0 Then
                                            strSQL &= "','" & box_w
                                        End If
                                        If box_h > 0 Then
                                            strSQL &= "','" & box_h
                                        End If
                                        strSQL &= "','" & Session("uId") & "',getdate(),'" & Session("plantcode") & "','" & numver & "') "
                                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        total = total + 1
                                    End If
                                End If 'skip
                            Next
                        End If
                    End With
                    myDataset.Reset()
                    ltlAlert.Text = "alert('产品尺寸导入成功！');"
                    ' ltlAlert.Text = "alert('产品尺寸导入成功！'); window.location.href='/part/partSizeImport.aspx?t=" & total.ToString() & "&s=" & s.ToString() & "';"
                Catch
                    ltlAlert.Text = "alert('上传文件失败!');"
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



