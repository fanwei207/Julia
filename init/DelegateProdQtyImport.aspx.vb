'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-02-16
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Delegate Products Qty In/Out/Retin From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb
Imports System.Configuration


Namespace tcpc


Partial Class DelegateProdQtyImport
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Protected WithEvents Comparevalidator3 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents uploadProdReplaceBtn As System.Web.UI.HtmlControls.HtmlInputButton
    Dim strSQL As String

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
                Session("EXSQL") = " Select ErrorInfo From PartQtyImportError Where userID='" & Session("uID") & "' order by id"
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
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
        Dim ProdPreQty As String = Nothing
        Dim strOrderStatus As String = Nothing
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
                ltlAlert.Text = "alert('上传文件失败');"
                Return
            End Try

            If (File.Exists(strFileName)) Then
                Dim _type, _code, _enterdate, _enterqty, _place, prodID, placeID, strDate, stryear, strmonth, _plant, plantID, _placeS, placeSID, _status, statusID, _notes As String
                Dim numOrderQty, numQty As Double
                Dim boolError As Boolean = False
                Dim boolError1 As Boolean = False
                    Dim myDataset As DataTable
                Try
                        myDataset = Me.GetExcelContents(strFileName)
                Catch
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    ltlAlert.Text = "alert('上传的文件不是Excel格式的！');"
                    Exit Sub
                Finally
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                End Try

                Try

                        With myDataset
                            If .Rows.Count > 0 Then
                                If .Columns(0).ColumnName <> "类型(IN/OUT/RIN)" Or .Columns(5).ColumnName <> "公司名称(委托方)" Or .Columns(6).ColumnName <> "仓库名称(委托方)" Or .Columns(1).ColumnName <> "半成品型号" Then
                                    ltlAlert.Text = "alert('导入文件不是半成品型号委托进出库导入模版.');"
                                    Exit Sub
                                End If
                                strSQL = " Delete From PartQtyImportError Where userID='" & Session("uID") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                                For i = 0 To .Rows.Count - 1
                                    _type = Nothing
                                    _code = Nothing
                                    _enterdate = Nothing
                                    _enterqty = Nothing
                                    _place = Nothing
                                    prodID = Nothing
                                    placeID = Nothing
                                    _plant = Nothing
                                    plantID = Nothing
                                    _placeS = Nothing
                                    placeSID = Nothing
                                    numOrderQty = 0.0#
                                    numQty = 0.0#
                                    boolError = False
                                    boolError1 = False
                                    _status = Nothing
                                    statusID = Nothing
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
                                        _placeS = ""
                                    Else
                                        _placeS = .Rows(i).Item(4)
                                    End If

                                    If .Rows(i).IsNull(5) Then
                                        _plant = ""
                                    Else
                                        _plant = .Rows(i).Item(5)
                                    End If

                                    If .Rows(i).IsNull(6) Then
                                        _place = ""
                                    Else
                                        _place = .Rows(i).Item(6)
                                    End If

                                    If .Rows(i).IsNull(7) Then
                                        _status = ""
                                    Else
                                        _status = .Rows(i).Item(7)
                                    End If

                                    If .Rows(i).IsNull(8) Then
                                        _notes = ""
                                    Else
                                        _notes = .Rows(i).Item(8)
                                    End If

                                    'fileds validation 
                                    If (_type.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":类型不能为空','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    ElseIf UCase(_type.Trim()) <> "IN" And UCase(_type.Trim()) <> "OUT" And UCase(_type.Trim()) <> "RIN" Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ";类型只能为IN、OUT或RIN','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    End If
                                    If UCase(_type.Trim()) = "IN" Then
                                        _type = "I"
                                    ElseIf UCase(_type.Trim()) = "OUT" Then
                                        _type = "O"
                                    ElseIf UCase(_type.Trim()) = "RIN" Then
                                        _type = "DR"
                                    End If

                                    If (_code.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":半成品型号不能为空','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    Else
                                        strSQL = " Select id  From Items Where code=N'" & chk.sqlEncode(_code.Trim()) & "' and status<>2 "
                                        prodID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                        If prodID = Nothing Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行" & (i + 2).ToString & _code.Trim() & ":半成品型号不存在或该半成品型号已经停用','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If
                                    End If

                                    If (_enterdate.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":日期不能为空','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    ElseIf IsDate(_enterdate.Trim()) = False Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":日期非法','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        'ElseIf chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 20030900, True) < 0 Then
                                        '    If Day(DateTime.Now) >= 26 Then
                                        '        strDate = CDate(CStr(Year(Now)) + "-" + CStr(Month(Now)) + "-" + "21")
                                        '        If DateTime.Compare(strDate, _enterdate.Trim()) < 0 Then
                                        '            boolError = True
                                        '            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                        '                   & " Values(N'行" & (i + 2).ToString & ":只能输入当前月21日以后的日期','" & Session("uID") & "')"
                                        '            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        '        End If
                                        '    Else
                                        '        If Month(Now) = 1 Then
                                        '            strmonth = "12"
                                        '            stryear = CStr(Year(Now) - 1)
                                        '        Else
                                        '            strmonth = CStr(Month(Now) - 1)
                                        '            stryear = CStr(Year(Now))
                                        '        End If
                                        '        strDate = CDate(stryear + "-" + strmonth + "-" + "21")
                                        '        If DateTime.Compare(strDate, _enterdate.Trim()) < 0 Then
                                        '            boolError = True
                                        '            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                        '                   & " Values(N'行" & (i + 2).ToString & ":只能输入上个月21日以后当前月20日已前的日期','" & Session("uID") & "')"
                                        '            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        '        End If
                                        '    End If
                                    End If

                                    If (_enterqty.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":数量不能为空','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    ElseIf IsNumeric(_enterqty.Trim()) = False Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":数量不是数字','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    ElseIf Val(_enterqty.Trim()) <= 0 Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":数量不能小于等于零','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    End If

                                    If (_placeS.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":仓库名称不能为空','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    Else
                                        strSQL = " Select w.warehouseID From warehouse w "
                                        If Session("uRole") <> 1 Then
                                            strSQL &= " Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "' And w.name=N'" & chk.sqlEncode(_placeS.Trim()) & "'"
                                        Else
                                            strSQL &= " Where w.name=N'" & chk.sqlEncode(_placeS.Trim()) & "'"
                                        End If
                                        placeSID = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strSQL)
                                        If placeSID = Nothing Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行" & (i + 2).ToString & ":仓库名称不存在或无权操作该仓库','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If
                                    End If

                                    If (_plant.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":公司名称(委托方)不能为空','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    Else
                                        strSQL = " Select p.plantID From tcpc0.dbo.plants p "
                                        If Session("uRole") <> 1 Then
                                            strSQL &= " Inner Join tcpc0.dbo.user_plant u On u.plantID = p.plantID " _
                                                   & " Where p.plantID<>'" & Session("plantCode") & "' And p.plantCode=N'" _
                                                   & chk.sqlEncode(_plant.Trim()) & "' And u.userID='" & Session("uID") & "'"
                                        Else
                                            strSQL &= " Where p.plantID<>'" & Session("plantCode") & "' And p.plantCode=N'" & chk.sqlEncode(_plant.Trim()) & "'"
                                        End If
                                        plantID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                        If plantID = Nothing Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行" & (i + 2).ToString & ":公司名称(委托方)不存在或无权操作该委托方公司','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If
                                    End If

                                    If (_place.Trim().Length <= 0) Then
                                        boolError = True
                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                               & " Values(N'行" & (i + 2).ToString & ":仓库名称(委托方)不能为空','" & Session("uID") & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                    Else
                                        If plantID <> Nothing Then
                                            strSQL = " Select w.warehouseID From warehouse w "
                                            If Session("uRole") <> 1 Then
                                                strSQL &= " Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "' And w.name=N'" & chk.sqlEncode(_place.Trim()) & "'"
                                            Else
                                                strSQL &= " Where w.name=N'" & chk.sqlEncode(_place.Trim()) & "'"
                                            End If
                                            placeID = SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings("SqlConn.Conn" & plantID.Trim()), CommandType.Text, strSQL)
                                            If placeID = Nothing Then
                                                boolError = True
                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行" & (i + 2).ToString & ":仓库名称(委托方)不存在或无权操作该仓库(委托方)','" & Session("uID") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                            End If
                                        End If
                                    End If

                                    If _status.Trim().Length > 0 Then
                                        strSQL = " Select id From tcpc0.dbo.Status Where statusName=N'" & chk.sqlEncode(_status.Trim()) & "'"
                                        statusID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                        If statusID = Nothing Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "输入的状态不存在','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If
                                    Else
                                        statusID = "0"
                                    End If


                                    If boolError = False Then
                                        strSQL = "Delegate_ProdTranImport_Temp"
                                        Dim params(10) As SqlParameter
                                        params(0) = New SqlParameter("@type", _type.Trim())
                                        params(1) = New SqlParameter("@prodid", Convert.ToInt32(prodID))
                                        params(2) = New SqlParameter("@enterDate", Convert.ToDateTime(_enterdate.Trim()))
                                        params(3) = New SqlParameter("@enterQty", Convert.ToDecimal(_enterqty.Trim()))
                                        params(4) = New SqlParameter("@placeid", Convert.ToInt32(placeID))
                                        params(5) = New SqlParameter("@plantid", Convert.ToInt32(plantID))
                                        params(6) = New SqlParameter("@sid", Convert.ToInt32(Session("plantCode")))
                                        params(7) = New SqlParameter("@swid", Convert.ToInt32(placeSID))
                                        params(8) = New SqlParameter("@intUserID", Convert.ToInt32(Session("uID")))
                                        params(9) = New SqlParameter("@status", Convert.ToInt32(statusID.Trim()))
                                        params(10) = New SqlParameter("@notes", _notes.Trim())

                                        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                        If ret < 0 Then
                                            boolError1 = True
                                        Else
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                            & " Values(N'行 " & (i + 2).ToString & "YES','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If
                                    End If
                                    If boolError1 = True Or boolError = True Then
                                        retRecord = retRecord + 1
                                    End If
                                Next
                            End If
                        End With
                    myDataset.Reset()
                    If retRecord = 0 Then
                        ltlAlert.Text = "alert('半成品型号委托进出库导入成功！');window.location.href='/Init/DelegateProdQtyImport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                    Else
                        ltlAlert.Text = "alert('半成品型号委托进出库导入有错误！');window.location.href='/Init/DelegateProdQtyImport.aspx?err=y&rm=" & DateTime.Now() & Rnd() & "';"
                    End If
                Catch
                    ltlAlert.Text = "alert('上传文件非法，半成品型号委托进出库失败！');"
                    Return
                End Try
            End If
        End If
    End Sub

End Class
End Namespace
