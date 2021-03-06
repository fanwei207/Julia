Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class bankEdit
        Inherits BasePage
        'Protected WithEvents ltlAlert As Literal
        Dim chk As New adamClass
        Dim item As ListItem
        Dim Query As String
        Dim reader As SqlDataReader


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents cMsg As System.Web.UI.WebControls.ValidationSummary
        Protected WithEvents rolename As System.Web.UI.WebControls.TextBox
        Protected WithEvents departmentName As System.Web.UI.WebControls.TextBox
        Protected WithEvents PersonnelType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cMsg3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
        Protected WithEvents IDNo As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then

                If Request("id") = "" Then
                    Button3.Visible = True
                Else
                    Button2.Visible = True
                    Query = " SELECT name, isnull(code,''),isnull(accountNo,''),isnull(address,''),isnull(zip,''),isnull(phone,''),isnull(fax,''),id  "
                    Query = Query & " From bank Where id= " & Request("id")
                    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)

                    While (reader.Read())
                        gname.Text = reader(0)
                        gcode.Text = reader(1)
                        gno.Text = reader(2)
                        gaddr.Text = reader(3)
                        gzip.Text = reader(4)
                        gphone.Text = reader(5)
                        gfax.Text = reader(6)
                    End While
                    reader.Close()
                End If
            End If
        End Sub
        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

            Dim pattern As String = "[0-9-]+"
            Dim reg As New Regex(pattern, RegexOptions.IgnoreCase)
            Dim matches As MatchCollection

            If gname.Text.Length = 0 Then
                ltlAlert.Text = "alert('银行名称 不能为空！');"
                Exit Sub
            End If

            If gno.Text.Length = 0 Then
                ltlAlert.Text = "alert('银行账号 不能为空！');"
                Exit Sub
            Else
                pattern = "[0-9-]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(gno.Text.Trim)

                If matches.Count <= 0 Then
                    ltlAlert.Text = "alert('银行帐号必须为数字！');"
                    Exit Sub
                End If
            End If

            If gzip.Text.Length = 0 Then
                pattern = "[0-9-]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(gzip.Text.Trim)

                If matches.Count <= 0 Then
                    ltlAlert.Text = "alert('邮编必须为数字！');"
                    Exit Sub
                End If
            End If

            If gphone.Text.Length = 0 Then
                pattern = "[0-9 -]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(gphone.Text.Trim)

                If matches.Count <= 0 Then
                    ltlAlert.Text = "alert('电话必须为数字和空格符！');"
                    Exit Sub
                End If
            End If

            If gfax.Text.Length = 0 Then
                pattern = "[0-9 -]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(gfax.Text.Trim)

                If matches.Count <= 0 Then
                    ltlAlert.Text = "alert('传真必须为数字和空格符！');"
                    Exit Sub
                End If
            End If

            'modify
            Query = " Update bank Set name= N'" & chk.sqlEncode(gname.Text) & "', "
            Query = Query & "code='" & gcode.Text & "',"
            Query = Query & "accountNo='" & gno.Text & "',"
            Query = Query & "address='" & gaddr.Text & "',"
            Query = Query & "zip='" & gzip.Text & "',"
            Query = Query & "phone='" & gphone.Text & "',"
            Query = Query & "fax='" & gfax.Text & "' "
            Query = Query & "  Where id=" & Request("id")
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
            Response.Redirect(chk.urlRand("/admin/bank.aspx"))
        End Sub
        Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

            Dim pattern As String = "[0-9-]+"
            Dim reg As New Regex(pattern, RegexOptions.IgnoreCase)
            Dim matches As MatchCollection

            If gname.Text.Length = 0 Then
                ltlAlert.Text = "alert('银行名称 不能为空！');"
                Exit Sub
            End If

            If gno.Text.Length = 0 Then
                ltlAlert.Text = "alert('银行账号 不能为空！');"
                Exit Sub
            Else
                pattern = "[0-9-]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(gno.Text.Trim)

                If matches.Count <= 0 Then
                    ltlAlert.Text = "alert('银行帐号必须为数字！');"
                    Exit Sub
                End If
            End If

            If gzip.Text.Length = 0 Then
                pattern = "[0-9-]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(gzip.Text.Trim)

                If matches.Count <= 0 Then
                    ltlAlert.Text = "alert('邮编必须为数字！');"
                    Exit Sub
                End If
            End If

            If gphone.Text.Length = 0 Then
                pattern = "[0-9 -]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(gphone.Text.Trim)

                If matches.Count <= 0 Then
                    ltlAlert.Text = "alert('电话必须为数字和空格符！');"
                    Exit Sub
                End If
            End If

            If gfax.Text.Length = 0 Then
                pattern = "[0-9 -]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(gfax.Text.Trim)

                If matches.Count <= 0 Then
                    ltlAlert.Text = "alert('传真必须为数字和空格符！');"
                    Exit Sub
                End If
            End If

            'new
            Query = " Insert Into bank (name,code,accountNo,address,zip, phone, fax, organizationID)"
            Query = Query & " Values(N'" & chk.sqlEncode(gname.Text) & "',"
            Query = Query & " '" & gcode.Text & "',"
            Query = Query & " '" & gno.Text & "',"
            Query = Query & " '" & gaddr.Text & "',"
            Query = Query & " '" & gzip.Text & "',"
            Query = Query & " '" & gphone.Text & "',"
            Query = Query & " '" & gfax.Text & "',"
            Query = Query & " 1)"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
            Response.Redirect(chk.urlRand("/admin/bank.aspx"))
        End Sub
        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            Response.Redirect(chk.urlRand("/admin/bank.aspx"))
        End Sub

    End Class

End Namespace
