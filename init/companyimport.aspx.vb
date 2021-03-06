Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class companyimport
        Inherits BasePage
    Dim chk As New adamClass
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

            cmdInitCompanyPWD.Attributes.Add("onclick", "return confirm('确定要初始化供应商密码吗?');")
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

        Dim strSQL1 As String
        Dim ds1 As DataSet

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
                Dim company_type, company_code, company_Name, street, city, state, country As String
                Dim zip, phone, fax, mobile, email, contact, comments, active As String
                'Try
                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset
                        If .Rows.Count > 0 Then
                            For i = 0 To .Rows.Count - 1
                                If .Rows(i).IsNull(0) Then
                                    company_type = ""
                                Else
                                    company_type = .Rows(i).Item(0)
                                End If

                                If .Rows(i).IsNull(1) Then
                                    company_code = ""
                                Else
                                    company_code = .Rows(i).Item(1)
                                End If
                                If .Rows(i).IsNull(2) Then
                                    company_Name = ""
                                Else
                                    company_Name = .Rows(i).Item(2)
                                End If
                                If .Rows(i).IsNull(3) Then
                                    street = ""
                                Else
                                    street = .Rows(i).Item(3)
                                End If
                                If .Rows(i).IsNull(4) Then
                                    city = ""
                                Else
                                    city = .Rows(i).Item(4)
                                End If
                                If .Rows(i).IsNull(5) Then
                                    state = ""
                                Else
                                    state = .Rows(i).Item(5)
                                End If
                                If .Rows(i).IsNull(6) Then
                                    country = ""
                                Else
                                    country = .Rows(i).Item(6)
                                End If
                                If .Rows(i).IsNull(7) Then
                                    zip = ""
                                Else
                                    zip = .Rows(i).Item(7)
                                End If
                                If .Rows(i).IsNull(8) Then
                                    phone = ""
                                Else
                                    phone = .Rows(i).Item(8)
                                End If
                                If .Rows(i).IsNull(9) Then
                                    fax = ""
                                Else
                                    fax = .Rows(i).Item(9)
                                End If
                                If .Rows(i).IsNull(10) Then
                                    mobile = ""
                                Else
                                    mobile = .Rows(i).Item(10)
                                End If
                                If .Rows(i).IsNull(11) Then
                                    email = ""
                                Else
                                    email = .Rows(i).Item(11)
                                End If
                                If .Rows(i).IsNull(12) Then
                                    contact = ""
                                Else
                                    contact = .Rows(i).Item(12)
                                End If
                                If .Rows(i).IsNull(13) Then
                                    comments = ""
                                Else
                                    comments = .Rows(i).Item(13)
                                End If
                                If .Rows(i).IsNull(14) Then
                                    active = 1
                                Else
                                    active = .Rows(i).Item(14)
                                End If

                                If (company_code.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，公司代码不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                If (company_Name.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('文件格式错误(1) --行 " & i + 2.ToString & "，公司名称不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                Dim reader As SqlDataReader
                                strSQL = "select * from Companies where company_code='" & company_code & "' "
                                reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL)
                                If reader.Read() Then
                                    ltlAlert.Text = "alert('文件格式错误(2) --行 " & i + 2.ToString & "，该公司代码已经存在！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                reader.Close()
                                If (company_type.Trim().Length > 0) Then
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Company Type') and systemcodename=N'" & company_type & "' "
                                    company_type = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If company_type = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(8) --行 " & (i + 2).ToString & "，所输入的供应商类型有误！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (country.Trim().Length > 0) Then
                                    Dim temp As String
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Country and Area') and systemcodename=N'" & country & "' "
                                    temp = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If temp = Nothing Then
                                        'ltlAlert.Text = "alert('文件格式错误(8) --行 " & (i + 2).ToString & "，所输入的国家不存在！');"
                                        'myDataset.Reset()
                                        'File.Delete(strFileName)
                                        'Return
                                        strSQL = "SELECT st.systemCodeTypeID FROM tcpc0.dbo.systemCodeType st WHERE st.systemCodeTypeName = 'Country and Area' "
                                        temp = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                                        strSQL = "Insert Into tcpc0.dbo.systemCode (systemCodeTypeID,systemCodeName) Values('" & temp & "',N'" & country & "') Select @@IDENTITY"
                                        temp = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    End If
                                    country = temp
                                End If
                                If (state.Trim().Length > 0) Then
                                    Dim temp As String
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Province') and systemcodename=N'" & state & "' "
                                    temp = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If temp = Nothing Then
                                        'ltlAlert.Text = "alert('文件格式错误(8) --行 " & (i + 2).ToString & "，所输入的省份不存在！');"
                                        'myDataset.Reset()
                                        'File.Delete(strFileName)
                                        'Return
                                        strSQL = "SELECT st.systemCodeTypeID FROM tcpc0.dbo.systemCodeType st WHERE st.systemCodeTypeName = 'Province' "
                                        temp = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

                                        strSQL = "Insert Into tcpc0.dbo.systemCode (systemCodeTypeID,systemCodeName) Values('" & temp & "',N'" & state & "') Select @@IDENTITY "
                                        temp = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    End If
                                    state = temp
                                End If
                                strSQL = " Insert Into Companies(company_type, company_code, company_Name, street,city, state, country, zip, phone, " _
                                       & " fax, mobile, email, contact, comments, active,createdby,createddate,deleted,organizationID) " _
                                       & " Values('" & company_type & "',N'" & company_code & "', N'" & company_Name & "',N'" & street _
                                        & "',N'" & city & "',N'" & state & "',N'" & country & "','" & zip & "',N'" _
                                       & phone & "',N'" & fax & "', N'" & mobile & "', N'" & email & "', N'" _
                                      & contact & "', N'" & comments & "','" & active & "',N'" & Session("uID") & " ',getdate(),0,1) "
                                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                            Next
                        End If
                    End With
                myDataset.Reset()
                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If
                ltlAlert.Text = "alert('公司信息导入成功.');"

                'Catch
                '    ltlAlert.Text = "alert('上传文件非法！');"
                '    Return
                'End Try
            End If
        End If
    End Sub

    Private Sub cmdInitCompanyPWD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInitCompanyPWD.Click
        Dim pwd, str As String
        Dim strsql As String
        Dim reader As SqlDataReader
       
            If Not Me.Security("19060502").isValid Then
                ltlAlert.Text = "alert('没有权限初始化供应商密码.');"
                Exit Sub
            End If

            strsql = "select company_id from companies where company_type=222"
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
            While (reader.Read)
                pwd = chk.encryptPWD(reader(0).ToString())
                str = "update companies set pwd='" & pwd & "' where company_id =" & reader(0)
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, str)
            End While
            ltlAlert.Text = "alert('供应商密码初始化成功.');"
    End Sub
End Class

End Namespace
