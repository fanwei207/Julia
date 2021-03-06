'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-6-13
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   Import Parts Qty In/Out/Ret From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb

Namespace tcpc

Partial Class ParchaseQtyImport
    Inherits System.Web.UI.Page
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Protected WithEvents Comparevalidator3 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents uploadProdReplaceBtn As System.Web.UI.HtmlControls.HtmlInputButton

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
        Dim ProdPreQty As String = Nothing
        Dim strOrderStatus As String = Nothing
        Dim con As SqlConnection = New SqlConnection(chk.dsnx)
        Dim cmd As SqlCommand
        Dim param As New SqlParameter
        Dim strflag As String = Nothing
        Dim retValue As Integer = -1

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
                Dim _type As String
                Dim _code As String
                Dim _compdept As String
                Dim _enterdate As String
                Dim _enterqty As String
                Dim _place As String
                Dim _plan As String
                Dim partID As String = Nothing
                Dim placeID As String = Nothing
                Dim compdeptID As String = Nothing
                Dim planID As String

                Try
                    Dim myDataset As DataSet = chk.getExcelContents(strFileName)
                    With myDataset.Tables(0)
                        If .Rows.Count > 0 Then
                            strSQL = " Delete From PartQtyImportTemp Where userID='" & Session("uID") & "' And plantID='" & Session("plantcode") & "'"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                            For i = 0 To .Rows.Count - 1
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
                                    _compdept = "0"
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
                                    _plan = .Rows(i).Item(5)
                                End If

                                'fileds validation 
                                If (_type.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，类型不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                ElseIf UCase(_type.Trim()) <> "IN" And UCase(_type.Trim()) <> "OUT" Then
                                    ltlAlert.Text = "alert('文件格式错误(1) --行 " & (i + 2).ToString & "，类型只能为IN或者OUT！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                If _type.Trim() = "IN" Then
                                    _type = "I"
                                ElseIf _type.Trim() = "OUT" Then
                                    _type = "O"
                                Else
                                    _type = "R"
                                End If

                                If (_code.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，材料代码不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                If (_compdept.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，供应商代码或部门代码不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                If (_enterdate.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，进/出货日期不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                ElseIf IsDate(_enterdate.Trim()) = False Then
                                    ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，进/出货日期非法！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                If (_enterqty.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，进/出货数量不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                ElseIf IsNumeric(_enterqty.Trim()) = False Then
                                    ltlAlert.Text = "alert('文件格式错误(3) --行 " & (i + 2).ToString & "，进/出货数量不是数字！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                ElseIf Val(_enterqty.Trim()) <= 0 Then
                                    ltlAlert.Text = "alert('文件格式错误(4) --行 " & (i + 2).ToString & "，进/出货数量不能小于等于零！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                If (_place.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，仓库名称不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                strSQL = " Select id From Items Where Code='" & chk.sqlEncode(_code.Trim()) & "' and status<>2  "
                                partID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                If partID = Nothing Then
                                    ltlAlert.Text = "alert('文件格式错误(6) --行 " & (i + 2).ToString & "，" & _code.Trim() & "材料代码不存在或者该材料已经停用！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                'process warehouse place add
                                cmd = New SqlCommand("ProductInventory_SetWarehouse", con)
                                If con.State = ConnectionState.Closed Then
                                    con.Open()
                                End If
                                cmd.CommandType = CommandType.StoredProcedure

                                param = cmd.Parameters.Add("@placeName", SqlDbType.NVarChar)
                                param.Direction = ParameterDirection.Input
                                param.Value = chk.sqlEncode(_place.Trim())

                                Try
                                    param = cmd.Parameters.Add("param", SqlDbType.Int)
                                    param.Direction = ParameterDirection.ReturnValue

                                    cmd.ExecuteReader()
                                    placeID = param.Value
                                    con.Close()
                                Catch
                                End Try

                                If _type = "I" Then
                                    strSQL = " Select c.company_id From Companies c " _
                                           & " Inner Join tcpc0.dbo.systemCode sc On sc.systemCodeID=c.company_type And sc.systemCodeName=N'供应商'" _
                                           & " Inner Join tcpc0.dbo.systemCodeType sct On sc.systemCodeTypeID=sct.systemCodeTypeID And sct.systemCodeTypeName='Company Type' " _
                                           & " Where company_code=N'" & _compdept.Trim & "'"
                                    compdeptID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If compdeptID = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(8) --行 " & (i + 2).ToString & "，" & _compdept.Trim() & "供应商代码不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If _type = "O" Then
                                    strSQL = " Select departmentID From departments Where code=N'" & _compdept.Trim & "' And active='1'"
                                    compdeptID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                    If compdeptID = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，" & _compdept.Trim() & "部门代码不存在或者该部门无效！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                    strSQL = " Select pop.prod_order_plan_code, popm.mrp_id, popm.destinationID AS deptID, popmd.item_id " _
                                           & " From Product_order_plan pop " _
                                           & " Inner Join Product_order_plan_mrp popm ON pop.prod_order_plan_id=popm.prod_order_plan_id And popm.destinationType='1' " _
                                           & " Inner Join Product_order_plan_mrp_detail popmd ON popm.mrp_id = popmd.mrp_id And popmd.item_type = 'PROD' " _
                                           & " Inner Join tcpc0.dbo.Items i On popmd.item_id=i.id And i.status<>2 " _
                                           & " Inner Join departments d On d.departmentID=popm.destinationID And d.active='1' " _
                                           & " Where pop.prod_order_plan_code=N'" & chk.sqlEncode(_plan.Trim()) & "' And i.id='" _
                                           & partID.Trim() & "' And d.departmentID='" & compdeptID.Trim() & "'"
                                    planID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                    If planID = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(11) --行 " & (i + 2).ToString & "，" & _plan.Trim() & "计划单号不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If _type = "R" Then
                                    strSQL = " Select c.company_id From Companies c " _
                                           & " Inner Join tcpc0.dbo.systemCode sc On sc.systemCodeID=c.company_type And sc.systemCodeName=N'供应商'" _
                                           & " Inner Join tcpc0.dbo.systemCodeType sct On sc.systemCodeTypeID=sct.systemCodeTypeID And sct.systemCodeTypeName='Company Type' " _
                                           & " Where company_code=N'" & _compdept.Trim & "'"
                                    compdeptID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If compdeptID = Nothing Then
                                        strSQL = " Select departmentID From departments Where code=N'" & _compdept.Trim & "' And active='1'"
                                        compdeptID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                        If compdeptID = Nothing Then
                                            ltlAlert.Text = "alert('文件格式错误(10) --行 " & (i + 2).ToString & "，" & _compdept.Trim() & "部门代码不存在或者该部门无效或供应商代码不存在！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If
                                End If

                                If _type <> "O" Then
                                    strSQL = " Insert Into PartQtyImportTemp(type, partID, comp_dept_ID, enterdate, enterqty, placeID, userID, plantID) " _
                                           & " Values('" & _type.Trim() & "','" & partID.Trim() & "','" & compdeptID.Trim() & "','" & _enterdate.Trim() & "','" _
                                           & _enterqty.Trim() & "','" & placeID.Trim() & "','" & Session("uID") & "','" & Session("plantcode") & "')"
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                Else
                                    strSQL = " Insert Into PartQtyImportTemp(type, partID, comp_dept_ID, enterdate, enterqty, placeID, userID, plantID, planID) " _
                                           & " Values('" & _type.Trim() & "','" & partID.Trim() & "','" & compdeptID.Trim() & "','" & _enterdate.Trim() & "','" _
                                           & _enterqty.Trim() & "','" & placeID.Trim() & "','" & Session("uID") & "','" & Session("plantcode") & "','" & planID.Trim() & "')"
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                End If
                            Next

                            'process part qty import
                            cmd = New SqlCommand("Purchase_PartQtyChangeImport", con)
                            If con.State = ConnectionState.Closed Then
                                con.Open()
                            End If
                            cmd.CommandType = CommandType.StoredProcedure

                            param = cmd.Parameters.Add("@intUserID", SqlDbType.Int)
                            param.Direction = ParameterDirection.Input
                            param.Value = Session("uID")

                            param = cmd.Parameters.Add("@plantID", SqlDbType.Int)
                            param.Direction = ParameterDirection.Input
                            param.Value = Session("plantcode")

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
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    If retValue = 0 Then
                        ltlAlert.Text = "alert('材料进出货导入成功！');window.location.href='/Purchase/PurchaseQtyimport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                    Else
                        ltlAlert.Text = "alert('材料进出货导入失败！');window.location.href='/Purchase/PurchaseQtyimport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
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
