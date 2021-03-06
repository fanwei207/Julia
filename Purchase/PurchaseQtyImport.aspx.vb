'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-6-13
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Parts Qty In/Out/Retin/Retout/Movein From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class PurchaseQtyImport
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
                Session("EXTitle1") = "500^<b>错误原因</b>~^"
                Session("EXHeader1") = ""
                Session("EXSQL1") = " Select ErrorInfo From PartQtyImportError Where userID='" & Session("uID") & "' Order By id "
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
        'Dim ProdPreQty As String = Nothing
        'Dim strOrderStatus As String = Nothing

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
                Dim _type, _code, _compdept, _enterdate, _enterqty, _place, _plan, _order, _rece_deli, _notes, partID, placeID, compdeptID, planID, orderID, strDate, stryear, strmonth, _status, statusID, _so, _soid As String
                Dim numOrderQty, numQty As Double
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

                Try
                        With mydt
                            If .Rows.Count > 0 Then
                                If .Columns(0).ColumnName <> "类型(IN/OUT/RIN/ROUT/MVIN/MVOUT)" And .Columns(11).ColumnName <> "客户定单号(可空)" Then
                                    'myDataset.Reset()
                                    ltlAlert.Text = "alert('导入文件不是部件进出库导入模版.'); "
                                    Exit Sub
                                End If

                                strSQL = " Delete From PartQtyImportError Where userID='" & Session("uID") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                                For i = 0 To .Rows.Count - 1
                                    _type = Nothing
                                    _code = Nothing
                                    _compdept = Nothing
                                    _enterdate = Nothing
                                    _enterqty = Nothing
                                    _place = Nothing
                                    _plan = Nothing
                                    _order = Nothing
                                    _rece_deli = Nothing
                                    _notes = Nothing
                                    partID = Nothing
                                    placeID = Nothing
                                    compdeptID = Nothing
                                    planID = Nothing
                                    orderID = Nothing
                                    numOrderQty = 0.0#
                                    numQty = 0.0#
                                    boolError = False
                                    boolError1 = False
                                    _status = Nothing
                                    statusID = Nothing
                                    _so = Nothing
                                    _soid = Nothing

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
                                        _compdept = ""
                                    Else
                                        _compdept = .Rows(i).Item(2)
                                    End If

                                    If .Rows(i).IsNull(3) Then
                                        _enterdate = ""
                                    Else
                                        _enterdate = .Rows(i).Item(3)
                                    End If

                                    If .Rows(i).IsNull(4) Then
                                        _enterqty = ""
                                    Else
                                        _enterqty = .Rows(i).Item(4)
                                    End If

                                    If .Rows(i).IsNull(5) Then
                                        _place = ""
                                    Else
                                        _place = .Rows(i).Item(5)
                                    End If

                                    If .Rows(i).IsNull(6) Then
                                        _plan = ""
                                    Else
                                        _plan = .Rows(i).Item(6)
                                    End If

                                    If .Rows(i).IsNull(7) Then
                                        _order = ""
                                    Else
                                        _order = .Rows(i).Item(7)
                                    End If

                                    If .Rows(i).IsNull(8) Then
                                        _rece_deli = ""
                                    Else
                                        _rece_deli = .Rows(i).Item(8)
                                    End If

                                    If .Rows(i).IsNull(9) Then
                                        _notes = ""
                                    Else
                                        _notes = .Rows(i).Item(9)
                                    End If

                                    If .Rows(i).IsNull(10) Then
                                        _status = ""
                                    Else
                                        _status = .Rows(i).Item(10)
                                    End If

                                    If .Rows(i).IsNull(11) Then
                                        _so = ""
                                    Else
                                        _so = .Rows(i).Item(11)
                                    End If

                                    'fileds validation 
                                    If _type.Trim().Length > 0 Then
                                        If UCase(_type.Trim()) <> "IN" And UCase(_type.Trim()) <> "OUT" And UCase(_type.Trim()) <> "RIN" And UCase(_type.Trim()) <> "ROUT" And UCase(_type.Trim()) <> "MVIN" And UCase(_type.Trim()) <> "MVOUT" Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "类型只能为IN、OUT、RIN、ROUT、MVIN或MVOUT','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If
                                        If UCase(_type.Trim()) = "IN" Then
                                            _type = "I"
                                        ElseIf UCase(_type.Trim()) = "OUT" Then
                                            _type = "O"
                                        ElseIf UCase(_type.Trim()) = "RIN" Then
                                            _type = "DR"
                                        ElseIf UCase(_type.Trim()) = "ROUT" Then
                                            _type = "RS"
                                        ElseIf UCase(_type.Trim()) = "MVIN" Then
                                            _type = "PM"
                                        ElseIf UCase(_type.Trim()) = "MVOUT" Then
                                            _type = "PMO"
                                        End If

                                        If (_code.Trim().Length <= 0) Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "材料代码不能为空','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        Else
                                            strSQL = " Select id  From Items Where code=N'" & chk.sqlEncode(_code.Trim()) & "'"
                                            partID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                            If partID = Nothing Then
                                                boolError = True
                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & _code.Trim() & "材料代码不存在或该材料已经停用','" & Session("uID") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                            End If
                                        End If

                                        If (_compdept.Trim().Length <= 0) Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "供应商代码或部门代码不能为空','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        Else
                                            If _type = "I" Or _type = "RS" Then
                                                strSQL = " Select c.company_id From Companies c " _
                                                       & " Inner Join tcpc0.dbo.systemCode sc On sc.systemCodeID=c.company_type And sc.systemCodeName=N'供应商'" _
                                                       & " Inner Join tcpc0.dbo.systemCodeType sct On sc.systemCodeTypeID=sct.systemCodeTypeID And sct.systemCodeTypeName='Company Type' " _
                                                       & " Where company_code=N'" & _compdept.Trim & "'"
                                                compdeptID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                                If compdeptID = Nothing Then
                                                    boolError = True
                                                    strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                           & " Values(N'行 " & (i + 2).ToString & _compdept.Trim() & "供应商代码不存在','" & Session("uID") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                End If
                                            Else
                                                strSQL = " Select departmentID From departments Where code=N'" & _compdept.Trim & "' And active=1 "
                                                compdeptID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                                If compdeptID = Nothing Then
                                                    boolError = True
                                                    strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                           & " Values(N'行 " & (i + 2).ToString & _compdept.Trim() & "部门代码不存在','" & Session("uID") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                End If
                                            End If
                                        End If

                                        If (_enterdate.Trim().Length <= 0) Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "日期不能为空','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        ElseIf IsDate(_enterdate.Trim()) = False Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "日期非法','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        ElseIf Not Me.Security("20030900").isValid < 0 Then
                                            If Day(DateTime.Now) >= 26 Then
                                                strDate = CDate(CStr(Year(Now)) + "-" + CStr(Month(Now)) + "-" + "21")
                                                If DateTime.Compare(strDate, _enterdate.Trim()) < 0 Then
                                                    boolError = True
                                                    strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                           & " Values(N'行 " & (i + 2).ToString & "只能输入当前月21日以后的日期','" & Session("uID") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                End If
                                            Else
                                                If Month(Now) = 1 Then
                                                    strmonth = "12"
                                                    stryear = CStr(Year(Now) - 1)
                                                Else
                                                    strmonth = CStr(Month(Now) - 1)
                                                    stryear = CStr(Year(Now))
                                                End If
                                                strDate = CDate(stryear + "-" + strmonth + "-" + "21")
                                                If DateTime.Compare(strDate, _enterdate.Trim()) < 0 Then
                                                    boolError = True
                                                    strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                           & " Values(N'行 " & (i + 2).ToString & "只能输入上个月21日以后当前月20日已前的日期','" & Session("uID") & "')"
                                                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                End If
                                            End If
                                        End If

                                        If (_enterqty.Trim().Length <= 0) Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "数量不能为空','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        ElseIf IsNumeric(_enterqty.Trim()) = False Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "数量不是数字','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        ElseIf Val(_enterqty.Trim()) <= 0 Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "数量不能小于等于零','" & Session("uID") & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                        End If

                                        If (_place.Trim().Length <= 0) Then
                                            boolError = True
                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                   & " Values(N'行 " & (i + 2).ToString & "仓库名称不能为空','" & Session("uID") & "')"
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
                                                       & " Values(N'行 " & (i + 2).ToString & "仓库名称不存在或无权操作该仓库','" & Session("uID") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
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

                                        If _so.Trim().Length > 0 Then
                                            strSQL = " Select prod_order_id From Product_orders Where order_code='" & chk.sqlEncode(_so.Trim()) & "' And order_status<>'CLOSE' And order_status <> 'FIN' "
                                            _soid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                            If _soid = Nothing Then
                                                boolError = True
                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                       & " Values(N'行 " & (i + 2).ToString & "客户定单号不存在或已完成或已关闭','" & Session("uID") & "')"
                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            _soid = 0
                                        End If

                                        If boolError = False Then
                                            Select Case _type
                                                Case "I"
                                                    'check order
                                                    If _rece_deli.Trim().Length > 0 Then
                                                        If UCase(_rece_deli.Trim()) <> "K-" Then
                                                            If _order.Trim().Length > 0 Then
                                                                Dim recID As Integer = 0
                                                                strSQL = " Select part_order_id From part_orders Where warehouseID=" & placeID & " and part_order_code='" & chk.sqlEncode(_order.Trim()) & "' And status<>1 "
                                                                recID = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strSQL)
                                                                If recID = 0 Then
                                                                    boolError1 = True
                                                                    strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                           & " Values(N'行 " & (i + 2).ToString & "定单号不存在或定单已关闭或定单在所选仓库中不存在！','" & Session("uID") & "')"
                                                                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                                Else
                                                                    orderID = recID.ToString()

                                                                    strSQL = "Purchase_PartIn"
                                                                    Dim params(11) As SqlParameter
                                                                    params(0) = New SqlParameter("@whid", Convert.ToInt32(placeID))
                                                                    params(1) = New SqlParameter("@partid", Convert.ToInt32(partID))
                                                                    params(2) = New SqlParameter("@suppid", Convert.ToInt32(compdeptID))
                                                                    params(3) = New SqlParameter("@inQty", Convert.ToDecimal(_enterqty.Trim()))
                                                                    params(4) = New SqlParameter("@inDate", Convert.ToDateTime(_enterdate.Trim()))
                                                                    params(5) = New SqlParameter("@deliCode", _rece_deli.Trim())
                                                                    params(6) = New SqlParameter("@orderid", Convert.ToInt32(orderID))
                                                                    params(7) = New SqlParameter("@notes", chk.sqlEncode(_notes.Trim))
                                                                    params(8) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                                                                    params(9) = New SqlParameter("@status", Convert.ToInt32(statusID.Trim()))
                                                                    params(10) = New SqlParameter("@line", i + 2)
                                                                    params(11) = New SqlParameter("@poid", Convert.ToInt32(_soid))

                                                                    ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                                                    If ret < 0 Then
                                                                        boolError1 = True
                                                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                               & " Values(N'行 " & (i + 2).ToString & "ERR" & ret.ToString() & "','" & Session("uID") & "')"
                                                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                                    Else
                                                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                               & " Values(N'行 " & (i + 2).ToString & " YES','" & Session("uID") & "')"
                                                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                                    End If
                                                                End If
                                                            Else
                                                                boolError1 = True
                                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                       & " Values(N'行 " & (i + 2).ToString & "定单号不能为空','" & Session("uID") & "')"
                                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                            End If
                                                        End If
                                                    Else
                                                        boolError1 = True
                                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                               & " Values(N'行 " & (i + 2).ToString & "收料单号不能为空','" & Session("uID") & "')"
                                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                    End If
                                                Case "O", "DR"
                                                    'check plancode
                                                    If _plan.Trim().Length > 0 Then
                                                        strSQL = " Select pop.prod_order_plan_id " _
                                                               & " From Product_order_plan pop " _
                                                               & " Where pop.prod_order_plan_code=N'" & chk.sqlEncode(_plan.Trim()) & "' And pop.status<>1 "
                                                        planID = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strSQL)
                                                        If planID = Nothing Then
                                                            boolError1 = True
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & "计划单号不存在或已关闭！','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        End If
                                                    Else
                                                        If _type.Trim() = "0" Then
                                                            boolError1 = True
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & "计划单号不能为空','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        Else
                                                            planID = "0"
                                                        End If
                                                    End If

                                                    If _type.Trim = "O" Then
                                                        strSQL = "Purchase_PartOut"
                                                        Dim params(9) As SqlParameter
                                                        params(0) = New SqlParameter("@whid", Convert.ToInt32(placeID))
                                                        params(1) = New SqlParameter("@partid", Convert.ToInt32(partID))
                                                        params(2) = New SqlParameter("@deptid", Convert.ToInt32(compdeptID))
                                                        params(3) = New SqlParameter("@outQty", Convert.ToDecimal(_enterqty.Trim()))
                                                        params(4) = New SqlParameter("@outDate", Convert.ToDateTime(_enterdate.Trim()))
                                                        params(5) = New SqlParameter("@planid", Convert.ToInt32(planID))
                                                        params(6) = New SqlParameter("@notes", chk.sqlEncode(_notes.Trim))
                                                        params(7) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                                                        params(8) = New SqlParameter("@status", Convert.ToInt32(statusID.Trim()))
                                                        params(9) = New SqlParameter("@line", i + 2)
                                                        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                                        If ret < 0 Then
                                                            boolError1 = True
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & "ERR" & ret.ToString() & "','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        Else
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & " YES','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        End If
                                                    End If

                                                    If _type.Trim = "DR" Then
                                                        strSQL = "Purchase_PartReturnIn"
                                                        Dim params(9) As SqlParameter
                                                        params(0) = New SqlParameter("@whid", Convert.ToInt32(placeID))
                                                        params(1) = New SqlParameter("@partid", Convert.ToInt32(partID))
                                                        params(2) = New SqlParameter("@deptid", Convert.ToInt32(compdeptID))
                                                        params(3) = New SqlParameter("@outQty", Convert.ToDecimal(_enterqty.Trim()))
                                                        params(4) = New SqlParameter("@outDate", Convert.ToDateTime(_enterdate.Trim()))
                                                        params(5) = New SqlParameter("@planid", Convert.ToInt32(planID))
                                                        params(6) = New SqlParameter("@notes", chk.sqlEncode(_notes.Trim))
                                                        params(7) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                                                        params(8) = New SqlParameter("@status", Convert.ToInt32(statusID.Trim()))
                                                        params(9) = New SqlParameter("@line", i + 2)
                                                        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                                        If ret < 0 Then
                                                            boolError1 = True
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & "ERR" & ret.ToString() & "','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        Else
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & " YES','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        End If
                                                    End If

                                                Case "PM"
                                                    If _rece_deli.Trim().Length > 0 Then
                                                        If _order.Trim().Length > 0 Then
                                                            Dim recID As Integer = 0
                                                            strSQL = " Select part_order_id From part_orders Where warehouseID=" & placeID & " and part_order_code='" & chk.sqlEncode(_order.Trim()) & "' And status<>1 "
                                                            recID = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strSQL)
                                                            If recID = 0 Then
                                                                boolError1 = True
                                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                       & " Values(N'行 " & (i + 2).ToString & "定单号不存在或定单在所选仓库中不存在！','" & Session("uID") & "')"
                                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                            Else
                                                                orderID = recID.ToString()
                                                            End If
                                                        Else
                                                            orderID = 0
                                                        End If
                                                        strSQL = "Purchase_PartMove"
                                                        Dim params(10) As SqlParameter
                                                        params(0) = New SqlParameter("@whid", Convert.ToInt32(placeID))
                                                        params(1) = New SqlParameter("@partid", Convert.ToInt32(partID))
                                                        params(2) = New SqlParameter("@deptid", Convert.ToInt32(compdeptID))
                                                        params(3) = New SqlParameter("@outQty", Convert.ToDecimal(_enterqty.Trim()))
                                                        params(4) = New SqlParameter("@outDate", Convert.ToDateTime(_enterdate.Trim()))
                                                        params(5) = New SqlParameter("@receCode", chk.sqlEncode(_rece_deli.Trim()))
                                                        params(6) = New SqlParameter("@notes", chk.sqlEncode(_notes.Trim))
                                                        params(7) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                                                        params(8) = New SqlParameter("@status", Convert.ToInt32(statusID.Trim()))
                                                        params(9) = New SqlParameter("@orderid", Convert.ToInt32(orderID))
                                                        params(10) = New SqlParameter("@line", i + 2)
                                                        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                                        If ret < 0 Then
                                                            boolError1 = True
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & "ERR" & ret.ToString() & "','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        Else
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & " YES','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        End If
                                                    Else
                                                        boolError1 = True
                                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                               & " Values(N'行 " & (i + 2).ToString & "收料单号不能为空','" & Session("uID") & "')"
                                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                    End If

                                                Case "PMO"
                                                    strSQL = "Purchase_PartMoveOut"
                                                    Dim params(8) As SqlParameter
                                                    params(0) = New SqlParameter("@whid", Convert.ToInt32(placeID))
                                                    params(1) = New SqlParameter("@partid", Convert.ToInt32(partID))
                                                    params(2) = New SqlParameter("@deptid", Convert.ToInt32(compdeptID))
                                                    params(3) = New SqlParameter("@pmoQty", Convert.ToDecimal(_enterqty.Trim()))
                                                    params(4) = New SqlParameter("@pmoDate", Convert.ToDateTime(_enterdate.Trim()))
                                                    params(5) = New SqlParameter("@notes", chk.sqlEncode(_notes.Trim))
                                                    params(6) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                                                    params(7) = New SqlParameter("@status", Convert.ToInt32(statusID.Trim()))
                                                    params(8) = New SqlParameter("@line", i + 2)
                                                    ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                                    If ret < 0 Then
                                                        boolError1 = True
                                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                               & " Values(N'行 " & (i + 2).ToString & "ERR" & ret.ToString() & "','" & Session("uID") & "')"
                                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                    Else
                                                        strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                               & " Values(N'行 " & (i + 2).ToString & " YES','" & Session("uID") & "')"
                                                        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                    End If

                                                Case "RS"
                                                    'check order
                                                    If _order.Trim().Length > 0 Then
                                                        strSQL = " Select part_order_id From part_orders Where warehouseID=" & placeID & " and part_order_code='" & chk.sqlEncode(_order.Trim()) & "'"
                                                        orderID = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strSQL)
                                                        If orderID = Nothing Then
                                                            boolError1 = True
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & "定单号不存在或定单在所选仓库中不存在！','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        End If
                                                    Else
                                                        'boolError1 = True
                                                        'strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                        '       & " Values(N'行 " & (i+2).ToString & "定单号不能为空','" & Session("uID") & "')"
                                                        'SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        orderID = "0"
                                                    End If

                                                    If boolError1 = False Then
                                                        'check plan code 
                                                        If _plan.Trim().Length > 0 Then
                                                            strSQL = " Select pop.prod_order_plan_id " _
                                                              & " From Product_order_plan pop " _
                                                               & " Where pop.prod_order_plan_code=N'" & chk.sqlEncode(_plan.Trim()) & "' And pop.status<>1 "
                                                            planID = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, strSQL)
                                                            If planID = Nothing Then
                                                                boolError1 = True
                                                                strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                       & " Values(N'行 " & (i + 2).ToString & "计划单号不存在或已关闭！','" & Session("uID") & "')"
                                                                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                            End If
                                                        Else
                                                            planID = 0
                                                            'boolError1 = True
                                                            'strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                            '       & " Values(N'行 " & (i+2).ToString & "计划单号不能为空','" & Session("uID") & "')"
                                                            'SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        End If
                                                    End If

                                                    If boolError1 = False Then
                                                        'check same record in part_tran
                                                        strSQL = "Purchase_PartReturnOut"
                                                        Dim params(10) As SqlParameter
                                                        params(0) = New SqlParameter("@whid", Convert.ToInt32(placeID))
                                                        params(1) = New SqlParameter("@partid", Convert.ToInt32(partID))
                                                        params(2) = New SqlParameter("@suppid", Convert.ToInt32(compdeptID))
                                                        params(3) = New SqlParameter("@inQty", Convert.ToDecimal(_enterqty.Trim()))
                                                        params(4) = New SqlParameter("@inDate", Convert.ToDateTime(_enterdate.Trim()))
                                                        params(5) = New SqlParameter("@planid", Convert.ToInt32(planID))
                                                        params(6) = New SqlParameter("@orderid", Convert.ToInt32(orderID))
                                                        params(7) = New SqlParameter("@notes", chk.sqlEncode(_notes.Trim))
                                                        params(8) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                                                        params(9) = New SqlParameter("@status", Convert.ToInt32(statusID.Trim()))
                                                        params(10) = New SqlParameter("@line", i + 2)
                                                        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                                        If ret < 0 Then
                                                            boolError1 = True
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & "ERR" & ret.ToString() & "','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        Else
                                                            strSQL = " Insert Into PartQtyImportError(ErrorInfo,userID) " _
                                                                   & " Values(N'行 " & (i + 2).ToString & " YES','" & Session("uID") & "')"
                                                            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL)
                                                        End If
                                                    End If

                                            End Select
                                        End If
                                        If boolError1 = True Or boolError = True Then
                                            retRecord = retRecord + 1
                                        End If
                                    End If
                                Next
                            End If
                        End With
                        'myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If

                    If retRecord = 0 Then
                        ltlAlert.Text = "alert('材料进出库导入成功！');window.location.href='/Purchase/PurchaseQtyimport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                    Else
                        ltlAlert.Text = "alert('材料进出库导入有错误！');window.location.href='/Purchase/PurchaseQtyimport.aspx?err=y&rm=" & DateTime.Now() & Rnd() & "';"
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                    ltlAlert.Text = "alert('上传文件非法，材料进出库失败！');"
                    Return
                End Try
            End If
        End If
    End Sub

End Class

End Namespace
