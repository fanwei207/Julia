'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-07-04
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Items From Excel To Item_QAD_Code
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class inv_qad_import
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

            If Request("err") = "y" Then
                Session("EXTitle") = "500^<b>错误原因</b>~^"
                Session("EXHeader") = ""
                Session("EXSQL") = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "' Order By Id "
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
            End If

            FileTypeDropDownList1.SelectedIndex = 0
            Dim item1 As ListItem
            item1 = New ListItem("Excel (.xls) file")
            item1.Value = 0
            FileTypeDropDownList1.Items.Add(item1)
        End If
    End Sub

    Sub uploadPartBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadPartBtn.ServerClick
        If (Session("uID") = Nothing Or Request("invid") = Nothing Or Request("isC") = "已关闭") Then
            Exit Sub
        End If
        ImportExcelFile()
    End Sub

    Sub ImportExcelFile()
        Dim strSQL As String
        Dim strSQL1 As String
        Dim ds As DataSet

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer

        Dim boolError As Boolean
        Dim ErrorRecord As Integer

        strCatFolder = Server.MapPath("/import")
        If Not Directory.Exists(strCatFolder) Then
            Try
                Directory.CreateDirectory(strCatFolder)
            Catch
                ltlAlert.Text = "alert('上传文件失败.')"
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
                ltlAlert.Text = "alert('上传文件失败.')"
                Return
            End Try

            If (File.Exists(strFileName)) Then
                Dim _site As String
                Dim _loca As String
                Dim _qad As String
                Dim _no As String
                Dim _qty As String
                Dim _desc As String
                Dim _status As String
                Dim _unit As String
                Dim _cost As String

                Dim id As Long

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
                Try

                        With mydt
                            If .Rows.Count > 0 Then
                                If .Columns.Count <> 10 Then
                                    'myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是QAD库存数导入模版.'); "
                                    Exit Sub
                                End If
                                If .Columns(0).ColumnName <> "地点" Or .Columns(1).ColumnName <> "库位" Or .Columns(2).ColumnName <> "零件号" Or .Columns(3).ColumnName <> "批序号" Or .Columns(4).ColumnName <> "库存数" Or .Columns(5).ColumnName <> "库存状态" Then
                                    'myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是QAD库存数导入模版.'); "
                                    Exit Sub
                                End If

                                strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                                strSQL = " Delete From inv_qad_tmp Where createdBy='" & Session("uID") & "' "
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                ErrorRecord = 0

                                For i = 0 To .Rows.Count - 1
                                    boolError = False
                                    _site = Nothing
                                    _loca = Nothing
                                    _qad = Nothing
                                    _no = Nothing
                                    _qty = Nothing
                                    _desc = Nothing
                                    _status = Nothing
                                    _unit = Nothing
                                    _cost = Nothing

                                    id = 0

                                    If .Rows(i).IsNull(0) Then
                                        _site = ""
                                    Else
                                        _site = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _loca = ""
                                    Else
                                        _loca = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        _qad = ""
                                    Else
                                        _qad = .Rows(i).Item(2)
                                    End If

                                    If .Rows(i).IsNull(3) Then
                                        _no = ""
                                    Else
                                        _no = .Rows(i).Item(3)
                                    End If
                                    If .Rows(i).IsNull(4) Then
                                        _qty = "0"
                                    Else
                                        _qty = .Rows(i).Item(4)
                                    End If
                                    If .Rows(i).IsNull(5) Then
                                        _status = ""
                                    Else
                                        _status = .Rows(i).Item(5)
                                    End If

                                    If .Rows(i).IsNull(6) Then
                                        _unit = ""
                                    Else
                                        _unit = .Rows(i).Item(6)
                                    End If
                                    If .Rows(i).IsNull(7) Then
                                        _cost = "0"
                                    Else
                                        _cost = .Rows(i).Item(7)
                                    End If
                                    If .Rows(i).IsNull(9) Then
                                        _desc = ""
                                    Else
                                        _desc = .Rows(i).Item(9)
                                    End If

                                    'fileds validation
                                    If (_site.Trim().Length > 0 And _loca.Trim().Length > 0 And _qad.Trim().Length > 0) Then
                                        If _qad.Trim.Length <> 14 Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & ",零件号必须为14位.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If
                                        If Not IsNumeric(_qty) Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & ",库存数必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If
                                        If Not IsNumeric(_cost) Then
                                            boolError = True
                                            strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & ",成本必须为数值型.','" & Session("uID") & "','" & Session("plantCode") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                        End If
                                        If boolError = False Then
                                            strSQL = " Insert Into inv_qad_tmp(inv_id,site,loca,item,inv_qty"
                                            strSQL1 = "  Values('" & Request("invid") & "','" & _site.Trim() & "','" & _loca.Trim() & "','" & _qad.Trim() & "','" & CDec(_qty) & "' "
                                            If _desc <> "" Then
                                                strSQL &= " ,item_desc"
                                                strSQL1 &= ",N'" & chk.sqlEncode(_desc.Trim()) & "'"
                                            End If
                                            If _no <> "" Then
                                                strSQL &= " ,inv_no"
                                                strSQL1 &= ",'" & _no.Trim() & "'"
                                            End If
                                            If _status <> "" Then
                                                strSQL &= " ,inv_status"
                                                strSQL1 &= ",N'" & _status.Trim() & "'"
                                            End If
                                            If _unit <> "" Then
                                                strSQL &= " ,inv_unit"
                                                strSQL1 &= ",N'" & _unit.Trim() & "'"
                                            End If
                                            If _cost <> "" Then
                                                strSQL &= " ,inv_cost"
                                                strSQL1 &= ",'" & CDec(_cost.Trim()) & "'"
                                            End If
                                            strSQL &= " ,createdBy)"
                                            strSQL1 &= ",'" & Session("uID") & "')"

                                            strSQL = strSQL & strSQL1

                                            'Response.Write(strSQL)

                                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                        Else
                                            ErrorRecord = ErrorRecord + 1
                                        End If
                                    End If
                                Next
                            End If
                        End With
                        'myDataset.Reset()




                    If ErrorRecord = 0 Then
                        If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, "inv_qad_import", New SqlParameter("@userid", Session("uID"))) = -100 Then
                            ltlAlert.Text = "alert('导入不成功，有错误.'); window.location.href='" & chk.urlRand("/QAD/Inv_Qad_Import.aspx?invid=" & Request("invid") & "&isC=" & Request("isC")) & "';"
                        Else
                            ltlAlert.Text = "alert('导入成功.'); window.location.href='" & chk.urlRand("/QAD/Inv_Qad.aspx?invid=" & Request("invid") & "&isC=" & Request("isC")) & "';"
                        End If
                    Else
                        Response.Write(ErrorRecord)
                        Response.Write("<br>")
                        Response.Write(strSQL)

                        ltlAlert.Text = "alert('导入结束，有错误.'); window.location.href='" & chk.urlRand("/QAD/Inv_Qad_Import.aspx?invid=" & Request("invid") & "&isC=" & Request("isC") & "&err=y") & "';"
                    End If
                Catch
                    Response.Write(strSQL)
                    ltlAlert.Text = "alert('文件导入失败！');"
                    Return
                End Try
            End If
        End If
    End Sub

   

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("Inv_Qad.aspx?invid=" & Request("invid") & "&isC=" & Request("isC"))
    End Sub
End Class

End Namespace

