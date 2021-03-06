Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb
'Imports BasePage




Namespace tcpc

    Partial Class NewUserImport
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer

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

            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer
            Dim reader As SqlDataReader

            strCatFolder = Server.MapPath("/import")
            If Not Directory.Exists(strCatFolder) Then
                Try
                    Directory.CreateDirectory(strCatFolder)
                Catch
                    ltlAlert.Text = "alert('创建文件目录失败(1001)！.')"
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
                    Dim loginname, userName, birthday, Department, enterdate, sex As String
                    Dim workshop, employDate, IC, leavedate, homeaddress, insurance, contractdate, homezip, contract As String
                    Dim currentAddress, marriage, role, currentzip, worktype, province, area, employtype, education As String
                    Dim labourunion, isActive, healthCheckDate, occupation, especialtype, certificate, introducer, fax, phone As String
                    Dim Email, Mobile, comments, houseFund, retiredFund, medicalFund, unemployFund, sretiredFund As String
                    Dim begood, kindsworkCode, kindsworkid, wgroup, wgroupID, userType, userTypeID, Fingerprint, IDeffectivedate As String
                    Dim level As String

                    'Dim myDataset As DataSet = chk.getExcelContents(strFileName)

                    Dim getexcel As BasePage = New BasePage()
                    Dim myDatatable As System.Data.DataTable = getexcel.GetExcelContents(strFileName)

                    Dim myDataset As DataSet = New DataSet()
                    myDataset.Tables.Add(myDatatable)


                    With myDataset.Tables(0)
                        If .Rows.Count > 0 Then
                            For m = 0 To .Rows.Count - 1
                                For n = m To .Rows.Count - 2
                                    Dim loginname1, loginname2 As String
                                    If .Rows(m).IsNull(0) Then
                                        loginname1 = ""
                                    Else
                                        loginname1 = .Rows(m).Item(0)
                                    End If
                                    If .Rows(n + 1).IsNull(0) Then
                                        loginname2 = ""
                                    Else
                                        loginname2 = .Rows(n + 1).Item(0)
                                    End If
                                    If loginname1 = loginname2 Then
                                        ltlAlert.Text = "alert('行 " & m + 2.ToString & "和行" & (n + 1) + 2.ToString & "，工号有重复！');"
                                        Return
                                    End If
                                Next
                            Next
                        End If
                    End With
                    With myDataset.Tables(0)
                        If .Rows.Count > 0 Then
                            For i = 0 To .Rows.Count - 1
                                'a
                                If .Rows(i).IsNull(0) Then
                                    loginname = ""
                                Else
                                    loginname = .Rows(i).Item(0)
                                End If
                                'b
                                If .Rows(i).IsNull(1) Then
                                    userName = " "
                                Else
                                    userName = .Rows(i).Item(1)
                                End If
                                'c
                                If .Rows(i).IsNull(2) Then
                                    birthday = ""
                                Else
                                    birthday = .Rows(i).Item(2)
                                End If
                                'd
                                If .Rows(i).IsNull(3) Then
                                    Department = " "
                                Else
                                    Department = .Rows(i).Item(3)
                                End If
                                'e
                                If .Rows(i).IsNull(4) Then
                                    enterdate = " "
                                Else
                                    enterdate = .Rows(i).Item(4)
                                End If
                                'f
                                If .Rows(i).IsNull(5) Then
                                    sex = " "
                                Else
                                    sex = .Rows(i).Item(5)
                                End If
                                'g
                                If .Rows(i).IsNull(6) Then
                                    workshop = " "
                                Else
                                    workshop = .Rows(i).Item(6)
                                End If
                                'h
                                If .Rows(i).IsNull(7) Then
                                    employDate = ""
                                Else
                                    employDate = .Rows(i).Item(7)
                                End If
                                'i
                                If .Rows(i).IsNull(8) Then
                                    IC = " "
                                Else
                                    IC = .Rows(i).Item(8)
                                End If
                                'j
                                If .Rows(i).IsNull(9) Then
                                    leavedate = ""
                                Else
                                    leavedate = .Rows(i).Item(9)
                                End If
                                'k
                                If .Rows(i).IsNull(10) Then
                                    homeaddress = " "
                                Else
                                    homeaddress = .Rows(i).Item(10)
                                End If
                                'l
                                If .Rows(i).IsNull(11) Then
                                    insurance = " "
                                Else
                                    insurance = .Rows(i).Item(11)
                                End If
                                'm
                                If .Rows(i).IsNull(12) Then
                                    contractdate = ""
                                Else
                                    contractdate = .Rows(i).Item(12)
                                End If
                                'n
                                If .Rows(i).IsNull(13) Then
                                    homezip = " "
                                Else
                                    homezip = .Rows(i).Item(13)
                                End If
                                'o
                                If .Rows(i).IsNull(14) Then
                                    contract = " "
                                Else
                                    contract = .Rows(i).Item(14)
                                End If
                                'p
                                If .Rows(i).IsNull(15) Then
                                    currentAddress = " "
                                Else
                                    currentAddress = .Rows(i).Item(15)
                                End If
                                'q
                                If .Rows(i).IsNull(16) Then
                                    marriage = " "
                                Else
                                    marriage = .Rows(i).Item(16)
                                End If
                                'r
                                If .Rows(i).IsNull(17) Then
                                    role = " "
                                Else
                                    role = .Rows(i).Item(17)
                                End If
                                's
                                If .Rows(i).IsNull(18) Then
                                    currentzip = " "
                                Else
                                    currentzip = .Rows(i).Item(18)
                                End If
                                't
                                If .Rows(i).IsNull(19) Then
                                    worktype = " "
                                Else
                                    worktype = .Rows(i).Item(19)
                                End If
                                'u
                                If .Rows(i).IsNull(20) Then
                                    province = " "
                                Else
                                    province = .Rows(i).Item(20)
                                End If
                                'v
                                If .Rows(i).IsNull(21) Then
                                    area = " "
                                Else
                                    area = .Rows(i).Item(21)
                                End If
                                'w
                                If .Rows(i).IsNull(22) Then
                                    employtype = " "
                                Else
                                    employtype = .Rows(i).Item(22)
                                End If
                                'x
                                If .Rows(i).IsNull(23) Then
                                    education = " "
                                Else
                                    education = .Rows(i).Item(23)
                                End If
                                'y
                                If .Rows(i).IsNull(24) Then
                                    labourunion = " "
                                Else
                                    labourunion = .Rows(i).Item(24)
                                End If
                                'z
                                If .Rows(i).IsNull(25) Then
                                    isActive = "1"
                                Else
                                    isActive = .Rows(i).Item(25)
                                End If
                                'aa
                                If .Rows(i).IsNull(26) Then
                                    healthCheckDate = ""
                                Else
                                    healthCheckDate = .Rows(i).Item(26)
                                End If
                                'ab
                                If .Rows(i).IsNull(27) Then
                                    occupation = " "
                                Else
                                    occupation = .Rows(i).Item(27)
                                End If
                                'ac
                                If .Rows(i).IsNull(28) Then
                                    especialtype = " "
                                Else
                                    especialtype = .Rows(i).Item(28)
                                End If
                                'ad
                                If .Rows(i).IsNull(29) Then
                                    certificate = " "
                                Else
                                    certificate = .Rows(i).Item(29)
                                End If
                                'ae
                                If .Rows(i).IsNull(30) Then
                                    introducer = " "
                                Else
                                    introducer = .Rows(i).Item(30)
                                End If
                                'af
                                If .Rows(i).IsNull(31) Then
                                    fax = " "
                                Else
                                    fax = .Rows(i).Item(31)
                                End If
                                'ag
                                If .Rows(i).IsNull(32) Then
                                    phone = " "
                                Else
                                    phone = .Rows(i).Item(32)
                                End If
                                'ah
                                If .Rows(i).IsNull(33) Then
                                    Email = " "
                                Else
                                    Email = .Rows(i).Item(33)
                                End If
                                'ai
                                If .Rows(i).IsNull(34) Then
                                    Mobile = " "
                                Else
                                    Mobile = .Rows(i).Item(34)
                                End If
                                'aj
                                If .Rows(i).IsNull(35) Then
                                    comments = " "
                                Else
                                    comments = .Rows(i).Item(35)
                                End If
                                'ak
                                If .Rows(i).IsNull(36) Then
                                    houseFund = "0"
                                Else
                                    houseFund = .Rows(i).Item(36)
                                End If
                                'al
                                If .Rows(i).IsNull(37) Then
                                    retiredFund = "0"
                                Else
                                    retiredFund = .Rows(i).Item(37)
                                End If
                                'am
                                If .Rows(i).IsNull(38) Then
                                    medicalFund = "0"
                                Else
                                    medicalFund = .Rows(i).Item(38)
                                End If
                                'an
                                If .Rows(i).IsNull(39) Then
                                    unemployFund = "0"
                                Else
                                    unemployFund = .Rows(i).Item(39)
                                End If
                                'ao
                                If .Rows(i).IsNull(40) Then
                                    sretiredFund = "0"
                                Else
                                    sretiredFund = .Rows(i).Item(40)
                                End If

                                If .Rows(i).IsNull(41) Then
                                    begood = " "
                                Else
                                    begood = .Rows(i).Item(41)
                                End If

                                If .Rows(i).IsNull(42) Then
                                    kindsworkCode = " "
                                Else
                                    kindsworkCode = .Rows(i).Item(42)
                                End If


                                If .Rows(i).IsNull(43) Then
                                    wgroup = " "
                                Else
                                    wgroup = .Rows(i).Item(43)
                                End If

                                'Add by Simon in March 15,2010 for User Type
                                If .Rows(i).IsNull(44) Then
                                    userType = " "
                                Else
                                    userType = .Rows(i).Item(44)
                                End If

                                If .Rows(i).IsNull(45) Then
                                    Fingerprint = " "
                                Else
                                    Fingerprint = .Rows(i).Item(45)
                                End If

                                If .Rows(i).IsNull(46) Then
                                    IDeffectivedate = ""
                                Else
                                    IDeffectivedate = .Rows(i).Item(46)
                                End If

                                If .Rows(i).IsNull(47) Then
                                    level = ""
                                Else
                                    level = .Rows(i).Item(47)
                                End If

                                If (loginname.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，工号不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                Else
                                    strSQL = "HR_getUserNo"
                                    Dim xparams As SqlParameter
                                    'xparams(0) = New SqlParameter("@isWorker", loginname)
                                    xparams = New SqlParameter("@plantcode", Session("PlantCode"))
                                    Dim userNo As String = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, strSQL, xparams)

                                    'ERROR

                                    If userNo.CompareTo(loginname) <> 0 Then

                                        ltlAlert.Text = "alert('文件格式错误(1) --行 " & i + 2.ToString & "， 该工号不正确！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                End If

                                If (userName.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，姓名不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                If (role.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，职务不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                If (worktype.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，计酬方式不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                'If (employtype.Trim().Length <= 0) Then
                                '    ltlAlert.Text = "alert('行 " & i + 1.ToString & "，用工性质不能为空！');"
                                '    myDataset.Reset()
                                '    File.Delete(strFileName)
                                '    Return
                                'End If
                                If (Department.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，部门不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                If (IC.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，身份证不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                If (userType.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，员工类型不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                strSQL = "select IC from tcpc0.dbo.users where IC='" & IC & "' and plantcode='" & Session("PlantCode") & "' and isActive=1 AND deleted=0 AND leavedate IS NULL"
                                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                                If reader.Read() Then
                                    ltlAlert.Text = "alert('文件格式错误(1) --行 " & i + 2.ToString & "，该身份证号码已经存在！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                reader.Close()
                                If (birthday.Trim().Length > 0) Then
                                    If (Convert.ToDateTime(birthday.Trim()) > Format(Now, "yyyy-MM-dd")) Then
                                        ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，出生日期格式非法！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (enterdate.Trim().Length > 0) Then
                                    If (Convert.ToDateTime(enterdate.Trim()) > Format(Now, "yyyy-MM-dd")) Then
                                        ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，入厂日期格式非法！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (employDate.Trim().Length > 0) Then
                                    If (Convert.ToDateTime(employDate.Trim()) > Format(Now, "yyyy-MM-dd")) Then
                                        ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，转正日期格式非法！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (leavedate.Trim().Length > 0) Then
                                    If (Convert.ToDateTime(leavedate.Trim()) > Format(Now, "yyyy-MM-dd")) Then
                                        ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，离职日期格式非法！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (contractdate.Trim().Length > 0) Then
                                    If (Convert.ToDateTime(contractdate.Trim()) > Format(Now, "yyyy-MM-dd")) Then
                                        ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，合同日期格式非法！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (healthCheckDate.Trim().Length > 0) Then
                                    If (Convert.ToDateTime(healthCheckDate.Trim()) > Format(Now, "yyyy-MM-dd")) Then
                                        ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，体检日期格式非法！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (homezip.Trim().Length > 0) Then
                                    If (IsNumeric(homezip.Trim()) = False) Then
                                        ltlAlert.Text = "alert('文件格式错误(10) --行 " & (i + 2).ToString & "，邮编不是数字！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (currentzip.Trim().Length > 0) Then
                                    If (IsNumeric(currentzip.Trim()) = False) Then
                                        ltlAlert.Text = "alert('文件格式错误(10) --行 " & (i + 2).ToString & "，当前邮编不是数字！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If

                                strSQL = " select roleID from roles where roleName= N'" & role.Trim & "'"
                                role = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                If role = Nothing Then
                                    ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的职务名称不存在！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                If (sex.Trim().Length > 0) Then
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'sex') and systemcodename=N'" & sex.Trim & "' "
                                    sex = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If sex = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的性别有误！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                strSQL = " select departmentID from departments where name=N'" & Department & "'"
                                Department = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                If Department = Nothing Then
                                    ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的部门不存在！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If
                                If (occupation.Trim().Length > 0) Then
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Occupation') and systemcodename=N'" & occupation.Trim & "' "
                                    occupation = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If occupation = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的职称不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (education.Trim().Length > 0) Then
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Education') and systemcodename=N'" & education.Trim & "' "
                                    education = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If education = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的学历不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (contract.Trim().Length > 0) Then
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Contract Type') and systemcodename=N'" & contract.Trim & "' "
                                    contract = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If contract = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的合同类型不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (province.Trim().Length > 0) Then
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Province') and systemcodename=N'" & province.Trim & "' "
                                    province = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If province = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的省份不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (workshop.Trim.Length() > 0) Then
                                    strSQL = " select id from workshop where Name= N'" & workshop & "' and departmentID='" & Department & "' AND workshopID IS NULL "
                                    workshop = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                    If workshop = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的工段不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If

                                strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Work Type') and systemcodename=N'" & worktype.Trim & "' "
                                worktype = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                If worktype = Nothing Then
                                    ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的计酬方式不存在！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                If (marriage.Trim().Length > 0) Then
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Marriage') and systemcodename=N'" & marriage.Trim & "' "
                                    marriage = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If marriage = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的婚姻状况不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (employtype.Trim().Length > 0) Then
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Employ Type') and systemcodename=N'" & employtype.Trim & "' "
                                    employtype = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If employtype = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的用工性质不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (insurance.Trim().Length > 0) Then
                                    strSQL = "SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Insurance Type') and systemcodename=N'" & insurance.Trim & "' "
                                    insurance = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                    If insurance = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的保险类型有误！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If


                                If (wgroup.Trim.Length > 0) And (workshop.Trim.Length() > 0) Then
                                    strSQL = " select id from workshop where Name= N'" & wgroup & "' and departmentID='" & Department & "' and workshopID=" & workshop
                                    wgroupID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                    If wgroupID = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的班组不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If
                                If (kindsworkCode.Trim().Length > 0) And (wgroup.Trim.Length() > 0) And (workshop.Trim.Length() > 0) Then
                                    strSQL = "select id from workkinds where name=N'" & kindsworkCode.Trim() & "' and workshopID=" & wgroupID
                                    kindsworkid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                    If kindsworkid = Nothing Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的工种有误！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If

                                'Check whether the usertype have exist 
                                strSQL = " SELECT s.systemCodeID FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Access Type') AND systemcodename=N'" & userType.Trim & "' "
                                userTypeID = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)
                                If userTypeID = Nothing Then
                                    ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，所输入的员工类型有误！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                'Added By Lqj--身份证有效期添加，淮安苏红梅提出。
                                If (IDeffectivedate.Trim().Length <= 0) Then
                                    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，身份证有效日期不能为空！');"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                Else
                                    If (Convert.ToDateTime(IDeffectivedate.Trim()) < Format(Now, "yyyy-MM-dd")) Then
                                        ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，身份证有效期日期格式非法！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                End If

                                If level.Trim().Length <= 0 Then
                                    ltlAlert.Text = "alert('LEVEL不能为空。');"
                                    Exit Sub
                                Else

                                    If level.Trim().Length > 1 Then

                                        ltlAlert.Text = "alert('LEVEL 只能是0～5之间数字。');"
                                        Exit Sub
                                    End If

                                    Dim pattern As String = "[0-5]" '"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+"
                                    Dim reg As New Regex(pattern, RegexOptions.IgnoreCase)
                                    Dim matches As MatchCollection
                                    'pattern = "[0-5]+"
                                    reg = New Regex(pattern, RegexOptions.IgnoreCase)
                                    matches = reg.Matches(level.Trim())

                                    If matches.Count <= 0 Then

                                        ltlAlert.Text = "alert('LEVEL 只能是0～5之间数字。');"
                                        Exit Sub
                                    End If
                                End If


                                strSQL1 = " Insert Into tcpc0.dbo.Users (loginName,userNo,userName,roleID,isactive,sexID,birthday,homeAddress,currentAddress,IC,homeZip,currentZip,phone,mobile,email,fax,departmentID,occupationID,educationID,certificates, " _
                                          & " enterDate,leaveDate,contractTypeID,comments,provinceID,workshopID,workTypeID,marriageID,introducer,employDate,employTypeID,isLabourUnion,insuranceTypeID, " _
                                          & " contractDate,specialWorkType,healthCheckDate,houseFund,retiredFund,medicalFund,unemployFund,sretiredFund,deleted,area,userPWD,isTemp,organizationID,begood,kindswork,modifiedBy,modifiedDate,plantcode,workprocedureID,userType,Fingerprint,IDeffectivedate,level) " _
                                          & " Values('" & loginname & "','" & loginname & "',N'" & userName & "',N'" & role & "','" & isActive & "','" & sex & "',"
                                If birthday.Trim.Length > 0 Then
                                    strSQL1 &= " '" & birthday & "',"
                                Else
                                    strSQL1 &= " null,"
                                End If

                                strSQL1 &= " N'" & homeaddress & "',N'" & currentAddress & "','" & IC & "','" & homezip & "'," _
                                          & " '" & currentzip & "','" & phone & "','" & Mobile & "','" & Email & "','" & fax & "','" & Department & "','" & occupation & "','" & education & "',N'" & certificate & "',"

                                If enterdate.Trim.Length > 0 Then
                                    strSQL1 &= " '" & enterdate & "',"
                                Else
                                    strSQL1 &= " null,"
                                End If
                                If leavedate.Trim.Length > 0 Then
                                    strSQL1 &= " '" & leavedate & "',"
                                Else
                                    strSQL1 &= " null,"
                                End If
                                strSQL1 &= " '" & contract & "',N'" & comments & "','" & province & "','" & workshop & "','" & worktype & "','" & marriage & "',N'" & introducer & "',"

                                If employDate.Trim.Length > 0 Then
                                    strSQL1 &= " '" & employDate & "', "
                                Else
                                    strSQL1 &= " null, "
                                End If

                                strSQL1 &= " '" & employtype & "','" & labourunion & "','" & insurance & "',"

                                If contractdate.Trim.Length > 0 Then
                                    strSQL1 &= " '" & contractdate & "',"
                                Else
                                    strSQL1 &= " null,"
                                End If

                                strSQL1 &= " '" & especialtype & "',"

                                If healthCheckDate.Trim.Length > 0 Then
                                    strSQL1 &= " '" & healthCheckDate & "',"
                                Else
                                    strSQL1 &= " null,"
                                End If
                                strSQL1 &= " '" & houseFund & "','" & retiredFund & "','" & medicalFund & "',"

                                strSQL1 &= " '" & unemployFund & "','" & sretiredFund & "','0',N'" & area & "','" & chk.encryptPWD("123456") & "','1',1,N'" & begood.Trim() & "',"
                                If (kindsworkCode.Trim().Length > 0) And (wgroup.Trim.Length() > 0) And (workshop.Trim.Length() > 0) Then
                                    strSQL1 &= " '" & kindsworkid.Trim() & "',"
                                Else
                                    strSQL1 &= " null,"
                                End If
                                strSQL1 &= " '" & Session("uid") & "','" & DateTime.Now & "','" & Session("PlantCode") & "',"
                                If (wgroup.Trim.Length > 0) And (workshop.Trim.Length() > 0) Then
                                    strSQL1 &= " '" & wgroupID.Trim & "' "
                                Else
                                    strSQL1 &= " null "
                                End If
                                strSQL1 &= " ,'" & userTypeID & "','" & Fingerprint & "','" & IDeffectivedate & "','" & level & "') "
                                'strSQL1 &= " '" & unemployFund & "','" & sretiredFund & "','0',null,'" & chk.encryptPWD("123") & "','1',1,null,null)"

                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL1)

                                strSQL = " INSERT INTO User_Exchange_Department(userID,DepartmentID,ExchangeDate,CreatedBy,CreatedDate)" _
                                    & " (select userID,'" & Department & "','" & enterdate.Trim & "','" & Session("uID") & "','" & DateTime.Now & "' From tcpc0.dbo.Users where userNo=N'" & loginname & "')"
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                            Next
                        End If
                    End With
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    ltlAlert.Text = "alert('用户信息导入成功.');"
                End If
            End If
        End Sub
    End Class

End Namespace

