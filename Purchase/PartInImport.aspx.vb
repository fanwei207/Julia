'*@     Create By   :   Nai Qi   
'*@     Create Date :   2006-09-01
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Dog PartIn data From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb

Namespace tcpc

Partial Class PartInImport
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents uploadBtn As System.Web.UI.HtmlControls.HtmlInputButton


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
                Session("EXSQL1") = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
            End If

            FileTypeDropDownList1.SelectedIndex = 0
            Dim item1 As ListItem
            item1 = New ListItem("Excel (.xls) file")
            item1.Value = 0
            FileTypeDropDownList1.Items.Add(item1)
        End If
    End Sub

    Sub uploadBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadBtn.ServerClick
        If (Session("uID") = Nothing) Then
            Exit Sub
        End If
        ImportExcelFile()
    End Sub

    Sub ImportExcelFile()
        Dim strSQL As String
        Dim reader As SqlClient.SqlDataReader

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
                ltlAlert.Text = "alert('创建文件目录失败.')"
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
                Dim _order As String = Nothing
                Dim _code As String = Nothing
                Dim _qty As String = Nothing

                Dim _qtyTemp As Double
                Dim _qtyTemp2 As Double

                    'Dim myDataset As DataSet
                    Dim mydt As DataTable
                Try
                        'myDataset = chk.getExcelContents(strFileName)
                        mydt = GetExcelContents(strFileName)
                Catch
                    ltlAlert.Text = "alert('上传文件必须是Excel文件格式.')"
                    Return
                End Try

                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If

                Try
                        With mydt
                            If .Rows.Count > 0 Then
                                strSQL = " Delete From ImportError Where userID='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                ErrorRecord = 0

                                For i = 0 To .Rows.Count - 1
                                    boolError = False
                                    If Not (.Rows(i).IsNull(1) And .Rows(i).IsNull(2)) Then
                                        If Not (.Rows(i).IsNull(2)) Then
                                            If .Rows(i).IsNull(0) Then
                                                _order = ""
                                            Else
                                                _order = .Rows(i).Item(0)
                                            End If

                                            If .Rows(i).IsNull(1) Then
                                                _code = ""
                                            Else
                                                _code = .Rows(i).Item(1)
                                            End If

                                            If .Rows(i).IsNull(2) Then
                                                _qty = ""
                                            Else
                                                _qty = .Rows(i).Item(2)
                                            End If


                                            'fileds validation
                                            If (_code.Trim().Length <= 0) Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，部件编号不能为空！ ','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            Else
                                                strSQL = " Select id from Items where status<>2 and code='" & _code & "'"
                                                If (SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL) <= 0) Then
                                                    boolError = True
                                                    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，部件编号不存在或已停用！ ','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                                End If
                                            End If

                                            If IsNumeric(_qty.Trim()) = False Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，数量必须是数字！ ','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            ElseIf Val(_qty.Trim()) <= 0 Then
                                                boolError = True
                                                strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "，数量不能小于零！ ','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If

                                            If boolError = False Then
                                                If (_order.Trim().Length > 0) Then
                                                    strSQL = " Select dpd.id,isnull(dpd.plan_qty,0), isnull(dpd.real_qty,0) from Dog_PartIn_Detail dpd "
                                                    strSQL &= " Inner Join Dog_PartIn dp on dp.id=dpd.dog_partin_id "
                                                    strSQL &= " Inner Join Product_order_detail od on od.prod_order_detail_id=dp.prod_order_detail_id "
                                                    strSQL &= " Inner Join Product_orders o on o.prod_order_id =od.prod_order_id "
                                                    strSQL &= " Inner Join tcpc0.dbo.Items i on i.id =dp.prod_id and i.code=N'" & _code.Trim() & "'"
                                                    strSQL &= " where dp.first_partin_date is not null and o.order_code='" & _order.Trim() & "' and o.order_status<>'CLOSE' order by dpd.plan_date "
                                                Else
                                                    strSQL = " Select dpd.id,isnull(dpd.plan_qty,0), isnull(dpd.real_qty,0) from Dog_PartIn_Detail dpd "
                                                    strSQL &= " Inner Join Dog_PartIn dp on dp.id=dpd.dog_partin_id "
                                                    strSQL &= " Inner Join Product_order_detail od on od.prod_order_detail_id=dp.prod_order_detail_id "
                                                    strSQL &= " Inner Join Product_orders o on o.prod_order_id =od.prod_order_id "
                                                    strSQL &= " Inner Join tcpc0.dbo.Items i on i.id =dp.prod_id and i.code=N'" & _code.Trim() & "'"
                                                    strSQL &= " where dp.first_partin_date is not null and o.order_status<>'CLOSE' order by dp.first_partin_date, dpd.plan_date "
                                                End If
                                                'Response.Write(strSQL)

                                                reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strSQL)

                                                _qtyTemp = CDec(_qty)
                                                'Response.Write(_qtyTemp.ToString)

                                                While reader.Read
                                                    If _qtyTemp = 0 Then
                                                        Exit While
                                                    End If
                                                    If reader(1) > reader(2) Then
                                                        _qtyTemp2 = reader(1) - reader(2)
                                                        If _qtyTemp >= _qtyTemp2 Then
                                                            strSQL = " Update Dog_PartIn_Detail set real_qty=isnull(plan_qty,0) where id='" & reader(0).ToString() & "'"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                            _qtyTemp = _qtyTemp - _qtyTemp2
                                                        Else
                                                            strSQL = " Update Dog_PartIn_Detail set real_qty=isnull(real_qty,0)+'" & _qtyTemp.ToString() & "' where id='" & reader(0).ToString() & " ' "
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                            _qtyTemp = 0
                                                        End If
                                                    End If
                                                End While
                                                reader.Close()
                                                'Response.Write(_qtyTemp.ToString)
                                                If _qtyTemp > 0 Then
                                                    ErrorRecord = ErrorRecord + 1
                                                    strSQL = " Insert Into ImportError(ErrorInfo, userID, plantID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "导入成功,但还有余数 " & _qtyTemp.ToString() & " .','" & Session("uID") & "','" & Session("plantCode") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                                End If
                                            Else
                                                ErrorRecord = ErrorRecord + 1
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End With
                        'myDataset.Reset()
                    If ErrorRecord = 0 Then
                        ltlAlert.Text = "alert('导入成功.'); window.location.href='" & chk.urlRand("/Purchase/PartInImport.aspx") & "';"
                    Else
                        ltlAlert.Text = "alert('导入结束，有错误，请查看日志！'); window.location.href='" & chk.urlRand("/Purchase/PartInImport.aspx?err=y") & "';"
                    End If
                Catch
                    ltlAlert.Text = "alert('导入文件时发生错误！');"
                    Return
                End Try
            End If
        End If
    End Sub

End Class

End Namespace

