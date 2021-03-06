'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-1-23
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Modify by LY 2006.12.15
'*@     Function    :   Import Parts Supply From Excel
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


    Partial Class PartSupplyImport
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then
                Me.Security.Register("19070500", "部件状态修改")

            End If
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
            Dim param(3) As SqlParameter

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
                    Dim _code, _company, _status, _memo As String
                    Dim partID, compID, cnt As Integer
                    Dim err As String = ""

                    strSQL = "delete from supply_input_error where createdby=" & Session("uID") & " "
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                    'Try
                    'Dim myDataset As DataSet = chk.getExcelContents(strFileName)
                    Dim myDatadt As DataTable = GetExcelContents(strFileName)
                    With myDatadt
                        If .Rows.Count > 0 Then
                            For i = 0 To .Rows.Count - 1
                                _code = Nothing
                                _company = Nothing
                                _status = Nothing
                                _memo = Nothing
                                partID = 0
                                compID = 0
                                cnt = 0
                                err = ""

                                If .Rows(i).IsNull(0) Then
                                    _code = ""
                                Else
                                    _code = .Rows(i).Item(0)
                                End If

                                If .Rows(i).IsNull(1) Then
                                    _company = ""
                                Else
                                    _company = .Rows(i).Item(1)
                                    If _company.Trim().IndexOf("，") Then
                                        _company = _company.Replace("，", ",")
                                    End If
                                End If

                                If .Rows(i).IsNull(2) Or .Rows(i).Item(2).ToString().Trim() = "0" Or .Rows(i).Item(2).ToString().Trim() = "使用" _
                                    Or .Rows(i).Item(2).ToString().Trim() = "TRUE" Then
                                    _status = "0"
                                ElseIf .Rows(i).Item(2).ToString().Trim() = "2" Or .Rows(i).Item(2).ToString().Trim() = "停用" _
                                    Or .Rows(i).Item(2).ToString().Trim() = "false" Then
                                    _status = "2"
                                Else
                                    _status = "1"
                                End If

                                If .Rows(i).IsNull(3) Then
                                    _memo = ""
                                Else
                                    _memo = .Rows(i).Item(3)
                                End If

                                If _code.Trim.Length <= 0 And _company.Trim.Length <= 0 Then
                                    'myDataset.Reset()
                                    File.Delete(strFileName)
                                    Exit For
                                End If

                                'fileds validation 
                                If (_code.Trim().Length <= 0) Then
                                    'ltlAlert.Text = "alert('行 " & i.ToString & "，材料代码不能为空！');"
                                    'myDataset.Reset()
                                    'File.Delete(strFileName)
                                    'Return
                                    err = "行 " & i + 2.ToString & "，材料代码不能为空！"
                                End If

                                If (_company.Trim().Length <= 0) Then
                                    'ltlAlert.Text = "alert('文件格式错误(1) --行 " & i.ToString & "，供应商编号不能为空！');"
                                    'myDataset.Reset()
                                    'File.Delete(strFileName)
                                    'Return
                                    err = err & "行 " & i + 2.ToString & "，供应商编号不能为空！"
                                End If

                                strSQL = "Select id from Items where code=N'" & chk.sqlEncode(_code.Trim()) & "' and status<>2 "
                                partID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                                If partID > 0 Then

                                    If _company.IndexOf(",") > 0 Then
                                        Dim ht As New Hashtable
                                        Dim j As Integer = 1
                                        While _company.Trim().Length > 0
                                            If _company.IndexOf(",") > 0 Then
                                                ht.Add(j, _company.Trim().Substring(0, _company.Trim().IndexOf(",")))
                                                _company = _company.Substring(_company.IndexOf(",") + 1)
                                            Else
                                                ht.Add(j, _company.Trim())
                                                _company = ""
                                            End If
                                            j = j + 1
                                        End While

                                        Dim myenum As IDictionaryEnumerator = ht.GetEnumerator
                                        While myenum.MoveNext
                                            strSQL = " Select c.company_id from Companies c " _
                                                    & " Inner Join SystemCode sc ON c.company_type=sc.systemCodeID And sc.systemCodeName=N'供应商' " _
                                                    & " Inner Join SystemCodeType sct ON sc.systemCodeTypeID=sct.systemCodeTypeID And sct.systemCodeTypeName='Company Type' " _
                                                    & " Where company_code=N'" & chk.sqlEncode(myenum.Value) & "' And c.active=1 And c.deleted=0 "
                                            compID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                                            If compID > 0 Then
                                                strSQL = " Select Count(*)  From Company_part Where companyID='" & compID & "' And partID='" & partID & "'"
                                                cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                                                If cnt > 0 Then
                                                    If Not Me.Security("19070500").isValid Then
                                                        strSQL = " Update Company_part Set status='" & _status & "'," _
                                                               & " comments=N'" & _memo.Trim() & "'," _
                                                               & " modifiedBy='" & Session("uID") & "'," _
                                                               & " modifiedDate='" & DateTime.Now() & "'" _
                                                               & " Where companyID='" & compID & "' And partID='" & partID & "'"
                                                    Else
                                                        strSQL = " Update Company_part Set comments=N'" & _memo.Trim() & "'," _
                                                               & " modifiedBy='" & Session("uID") & "'," _
                                                               & " modifiedDate='" & DateTime.Now() & "'" _
                                                               & " Where companyID='" & compID & "' And partID='" & partID & "'"
                                                    End If
                                                Else
                                                    strSQL = " Insert Into Company_part(companyID, partID, status, comments, createdBy, createdDate) " _
                                                           & " Values('" & compID & "','" & partID & "','" & _status.Trim() & "','" & _memo.Trim() _
                                                           & "','" & Session("uID") & "',getdate())"
                                                End If
                                                If err = "" Then
                                                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                                End If
                                            Else
                                                err = err & "行 " & i + 2.ToString & "，供应商编号" & myenum.Value & "不存在！"
                                            End If  'compID>0
                                        End While
                                        myenum.Reset()
                                    Else
                                        strSQL = " Select c.company_id from Companies c " _
                                               & " Inner Join SystemCode sc ON c.company_type=sc.systemCodeID And sc.systemCodeName=N'供应商' " _
                                               & " Inner Join SystemCodeType sct ON sc.systemCodeTypeID=sct.systemCodeTypeID And sct.systemCodeTypeName='Company Type' " _
                                               & " Where company_code=N'" & chk.sqlEncode(_company.Trim()) & "' And c.active=1 And c.deleted=0 "
                                        compID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                                        If compID > 0 Then
                                            strSQL = " Select Count(*)  From Company_part Where companyID='" & compID & "' And partID='" & partID & "'"
                                            cnt = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                                            If cnt > 0 Then
                                                If Not Me.Security("19070500").isValid Then
                                                    strSQL = " Update Company_part Set status='" & _status & "'," _
                                                           & " comments=N'" & _memo.Trim() & "'," _
                                                           & " modifiedBy='" & Session("uID") & "'," _
                                                           & " modifiedDate='" & DateTime.Now() & "'" _
                                                           & " Where companyID='" & compID & "' And partID='" & partID & "'"
                                                Else
                                                    strSQL = " Update Company_part Set comments=N'" & _memo.Trim() & "'," _
                                                           & " modifiedBy='" & Session("uID") & "'," _
                                                           & " modifiedDate='" & DateTime.Now() & "'" _
                                                           & " Where companyID='" & compID & "' And partID='" & partID & "'"
                                                End If
                                            Else
                                                strSQL = " Insert Into Company_part(companyID, partID, status, comments, createdBy, createdDate) " _
                                                       & " Values('" & compID & "','" & partID & "','" & _status.Trim() & "','" & _memo.Trim() _
                                                       & "','" & Session("uID") & "',getdate())"
                                            End If
                                            If err = "" Then
                                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                                            End If
                                        Else
                                            err = err & "行 " & i + 2.ToString & "，供应商编号不存在！"
                                        End If  'compID>0
                                    End If   'indexof(",")
                                Else
                                    err = err & "行 " & i + 2.ToString & "，部件编号不存在！"
                                End If   'partID>0
                                If err <> "" Then
                                    strSQL = " insert into supply_input_error(prod_code,err_info,company_code,createdby) values(N'" & _code & "',N'" & err & "','" & _company & "'," & Session("uID") & ")"
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                    err = ""
                                End If
                            Next
                        End If
                    End With
                    'myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    ' ltlAlert.Text = "alert('导入成功.'); window.location.href='/supply/PartSupplyImport.aspx';"
                    'Catch
                    '    ltlAlert.Text = "alert('上传文件非法！');"
                    '    ' Return
                    'End Try
                    strSQL = "select prod_code,company_code,err_info From supply_input_error where createdby='" & Session("uid") & "' "
                    Session("EXSQL1") = strSQL
                    Session("EXTitle1") = "<b>产品编号</b>~^<b>公司代码</b>~^<b>出错信息</b>~^"

                    ltlAlert.Text = "window.open('/public/exportExcel1.aspx', '_blank');"
                End If
            End If
        End Sub

    End Class

End Namespace
