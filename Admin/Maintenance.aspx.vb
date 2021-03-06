Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class Maintenance
        Inherits BasePage
        'Protected WithEvents ltlAlert As Literal
        Dim chk As New adamClass
        Dim query As String


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

            If Not IsPostBack Then

                BindData()
            End If
        End Sub

        Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
            Dim strZip As String
            Dim strPhone As String
            Dim strMobile As String
            Dim strEmail As String
            strZip = "^[0-9]+$"
            strPhone = "^[0-9()-]+$"
            strMobile = "^[0-9]\d{6,15}$"
            strEmail = "^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$"
            If CName.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('公司名称(中文)不能为空。');"
                Exit Sub
            ElseIf CName.Text.Trim().Length > 50 Then
                ltlAlert.Text = "alert('公司名称(中文)不能多于50个字符。');"
                Exit Sub
            ElseIf EName.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('公司名称(英文)不能为空。');"
                Exit Sub
            ElseIf EName.Text.Trim().Length > 100 Then
                ltlAlert.Text = "alert('公司名称(英文)不能多于100个字符。');"
                Exit Sub
            ElseIf Ccode.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('公司代码不能为空。');"
                Exit Sub
            ElseIf Ccode.Text.Trim().Length > 10 Then
                ltlAlert.Text = "alert('公司代码不能多于10个字符。');"
                Exit Sub
            ElseIf Address.Text.Trim().Length > 50 Then
                ltlAlert.Text = "alert('地址不能多于50个字符。');"
                Exit Sub
            ElseIf Phone.Text.Trim().Length > 15 Then
                ltlAlert.Text = "alert('电话号码不能多于15个字符。');"
                Exit Sub
            ElseIf Zip.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(Zip.Text.Trim(), strZip, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('邮编只能为数字。');"
                Exit Sub
            ElseIf Zip.Text.Trim().Length > 10 Then
                ltlAlert.Text = "alert('邮编不能多于10个字符。');"
                Exit Sub
            ElseIf Phone.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(Phone.Text.Trim(), strPhone, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('电话号码只能为数字()以及-。');"
                Exit Sub
            ElseIf Mobile.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(Mobile.Text.Trim(), strMobile, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('手机号码只能为数字。');"
                Exit Sub
            ElseIf Mobile.Text.Trim().Length > 15 Then
                ltlAlert.Text = "alert('手机号码不能多于15个字符。');"
                Exit Sub
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(Email.Text.Trim(), strEmail, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('请输入有效的电子邮件地址。');"
                Exit Sub
            ElseIf Email.Text.Trim().Length > 50 Then
                ltlAlert.Text = "alert('电子邮件不能多于50个字符。');"
                Exit Sub
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(Fax.Text.Trim(), strPhone, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('传真号码只能为数字()以及-。');"
                Exit Sub
            ElseIf Fax.Text.Trim().Length > 15 Then
                ltlAlert.Text = "alert('传真号码不能多于15个字符。');"
                Exit Sub
            Else

            End If
            query = " UPDATE Organization SET name=N'" & CName.Text.Trim() & "'"
            If Address.Text.Trim().Length > 0 Then
                query = query & " , address=N'" & Address.Text.Trim() & "'"
            End If
            If Zip.Text.Trim().Length > 0 Then
                query = query & " , zip='" & Zip.Text.Trim() & "'"
            End If
            If Phone.Text.Trim().Length > 0 Then
                query = query & " , phone='" & Phone.Text.Trim() & "'"
            End If
            If Mobile.Text.Trim().Length > 0 Then
                query = query & " , mobile='" & Mobile.Text.Trim() & "'"
            End If
            If Email.Text.Trim().Length > 0 Then
                query = query & " , email='" & Email.Text.Trim() & "'"
            End If
            If Fax.Text.Trim().Length > 0 Then
                query = query & " , fax='" & Fax.Text.Trim() & "'"
            End If
            If Ccode.Text.Trim().Length > 0 Then
                query = query & " "
            End If
            query = query & ", englishName='" & EName.Text.Trim() & "', code='" & Ccode.Text.Trim() & "' WHERE organizationID='" & Session("orgID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)
            ltlAlert.Text = "alert('保存成功!');"
            BtnSave.Enabled = False
            BindData()
        End Sub

        Sub BindData()
            Dim dst As DataSet
            query = " SELECT ISNULL(name,'') as name, ISNULL(address,'') as address, ISNULL(zip,'') as zip, ISNULL(phone,'') as phone, " _
                  & " ISNULL(mobile,'') as mobile, ISNULL(email,'') as email, ISNULL(fax,'') as fax, ISNULL(englishName,'') as englishName,isnull(code,'') as code " _
                  & " FROM Organization " _
                  & " WHERE organizationID='" & Session("orgID") & "'"
            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)
            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    CName.Text = .Rows(0).Item("name").ToString().Trim()
                    EName.Text = .Rows(0).Item("englishName").ToString().Trim()
                    Ccode.Text = .Rows(0).Item("code").ToString().Trim
                    Address.Text = .Rows(0).Item("address").ToString().Trim()
                    Zip.Text = .Rows(0).Item("zip").ToString().Trim()
                    Phone.Text = .Rows(0).Item("phone").ToString().Trim()
                    Mobile.Text = .Rows(0).Item("mobile").ToString().Trim()
                    Email.Text = .Rows(0).Item("email").ToString().Trim()
                    Fax.Text = .Rows(0).Item("fax").ToString().Trim()
                End If
            End With
        End Sub

    End Class

End Namespace

