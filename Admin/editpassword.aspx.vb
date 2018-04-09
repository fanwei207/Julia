Imports System
Imports System.Text.RegularExpressions
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class editpassword
        Inherits BasePage 
        'Protected WithEvents ltlAlert As Literal
        Dim chk As New adamClass
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
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
        Protected WithEvents IDNo As System.Web.UI.WebControls.TextBox
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
                ' 
                'Added by Chenyb
                Dim ds As DataSet
                ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.StoredProcedure, "sp_hr_selectPasswordRules")
                With ds.Tables(1)
                    If (.Rows.Count > 0) Then
                        If (Not .Rows(0).IsNull("minLen")) Then
                            minLen.Text = .Rows(0).Item("minLen").ToString()
                        End If
                        If (Not .Rows(0).IsNull("maxLen")) Then
                            maxLen.Text = .Rows(0).Item("maxLen").ToString()
                        End If
                        If (Not .Rows(0).IsNull("hasNumber")) Then
                            hasNumber.Text = .Rows(0).Item("hasNumber")
                        End If
                        If (Not .Rows(0).IsNull("hasLowLetter")) Then
                            hasLowLetter.Text = .Rows(0).Item("hasLowLetter")
                        End If
                        If (Not .Rows(0).IsNull("hasUpLetter")) Then
                            hasUpLetter.Text = .Rows(0).Item("hasUpLetter")
                        End If
                        If (Not .Rows(0).IsNull("hasSpecial")) Then
                            hasSpecial.Text = .Rows(0).Item("hasSpecial")
                        End If
                        If (Not .Rows(0).IsNull("structureDesc")) Then
                            patternDesc.Text = .Rows(0).Item("structureDesc").ToString()
                        End If
                        If (Not .Rows(0).IsNull("numberRegex")) Then
                            numberRegex.Text = .Rows(0).Item("numberRegex").ToString()
                        End If
                        If (Not .Rows(0).IsNull("lowLetterRegex")) Then
                            lowLetterRegex.Text = .Rows(0).Item("lowLetterRegex").ToString()
                        End If
                        If (Not .Rows(0).IsNull("upLetterRegex")) Then
                            upLetterRegex.Text = .Rows(0).Item("upLetterRegex").ToString()
                        End If
                        If (Not .Rows(0).IsNull("specialRegex")) Then
                            specialRegex.Text = .Rows(0).Item("specialRegex").ToString()
                        End If
                    End If
                End With
                ds.Dispose()
                If (patternDesc.Text <> "") Then
                    If (Session("PlantCode") = 98 Or Session("PlantCode") = 99) Then

                        labTips.Text = "Password Rulse:<br />- Must contain numbers, letters, and special characters<br />- The password is valid for: 3 months<br />- The new password cannot be the same as the previous password"
                    Else
                        labTips.Text = patternDesc.Text.Replace("- ", "").Replace(vbCrLf, "<br/>")
                    End If
                End If
                'End Added by Chenyb
            End If
        End Sub

        Function isNumeric(ByVal val As String) As Boolean
            Try
                Dim intVal As Double = Convert.ToDouble(val)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function IsFitRegex() As Boolean
            If (hasNumber.Text = "" Or numberRegex.Text = "" Or hasLowLetter.Text = "" Or lowLetterRegex.Text = "" Or hasUpLetter.Text = "" Or upLetterRegex.Text = "" Or hasSpecial.Text = "" Or specialRegex.Text = "") Then
                Return True
            End If

            If (Convert.ToBoolean(hasNumber.Text)) Then
                If (Not Regex.IsMatch(newpassword.Text.Trim(), numberRegex.Text)) Then
                    Return False
                End If
            End If

            If (Convert.ToBoolean(hasLowLetter.Text)) Then
                If (Not Regex.IsMatch(newpassword.Text.Trim(), lowLetterRegex.Text)) Then
                    Return False
                End If
            End If

            If (Convert.ToBoolean(hasUpLetter.Text)) Then
                If (Not Regex.IsMatch(newpassword.Text.Trim(), upLetterRegex.Text)) Then
                    Return False
                End If
            End If

            If (Convert.ToBoolean(hasSpecial.Text)) Then
                If (Not Regex.IsMatch(newpassword.Text.Trim(), specialRegex.Text)) Then
                    Return False
                End If
            End If

            Return True
        End Function

        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

            If oldpassword.Text.Length = 0 Then
                ltlAlert.Text = "alert('Old Password could not be empty！');"
                Exit Sub
            End If

            If newpassword.Text.Length = 0 Then
                ltlAlert.Text = "alert('New Password could not be empty！');"
                Exit Sub
            End If

            If newpassword1.Text.Length = 0 Then
                ltlAlert.Text = "alert('Confirm Password could not be empty！');"
                Exit Sub
            End If

            If newpassword1.Text.Trim() <> newpassword.Text.Trim() Then
                'ltlAlert.Text = "alert('Old Password and New Password must be equal！');"
                ltlAlert.Text = "alert('New Password and Confirm Password do not match！');"
                Exit Sub
            End If

            Query = " Select userPWD From tcpc0.dbo.users Where userID=" & Session("uID") & " and userPWD= '" & chk.encryptPWD(oldpassword.Text.Trim()) & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            'Response.Write(Query)
            Dim pwd As String
            While (reader.Read())
                pwd = reader("userPWD")
            End While
            reader.Close()

            If pwd = Nothing Then
                ltlAlert.Text = "alert('Old Password error！');"
            Else
                'Add by Chenyb 判断密码的是否满足复杂度
                If (patternDesc.Text <> "" And isNumeric(minLen.Text) And isNumeric(maxLen.Text)) Then
                    If (newpassword.Text.Trim.Length < Convert.ToInt32(minLen.Text) Or newpassword.Text.Trim.Length > Convert.ToInt32(maxLen.Text)) Then
                        ltlAlert.Text = "alert('Lenght of New Password error！\n" & patternDesc.Text.Replace(vbCrLf, "\n") & "')"
                        Exit Sub
                    ElseIf (Not IsFitRegex()) Then
                        ltlAlert.Text = "alert('New Password format error！\n" & patternDesc.Text.Replace(vbCrLf, "\n") & "')"
                        Exit Sub
                    End If
                End If
                '更新用户密码并添加密码使用记录
                Dim param(3) As SqlParameter
                Dim ret As Integer
                param(0) = New SqlParameter("@uID", Session("uID"))
                param(1) = New SqlParameter("@userPWD", chk.encryptPWD(newpassword.Text.Trim()))
                param(2) = New SqlParameter("@retValue", SqlDbType.Int)
                param(2).Direction = ParameterDirection.Output
                ret = SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, "sp_hr_updateUserPassword", param)
                If (Convert.ToInt32(param(2).Value) = 0) Then
                    ltlAlert.Text = "alert('The New Password has been used！')"
                ElseIf (Convert.ToInt32(param(2).Value) = 1) Then

                    '如果是首页定位来的，即强制密码重置的，要重新定位回MasterPage
                    If (Session("isInitPWD") = True Or Session("changePWDNextTime") = True Or Session("isPWDReset") = True Or Session("isPwdValid") = False) Then
                        Session("isInitPWD") = False
                        Session("changePWDNextTime") = False
                        Session("isPWDReset") = False
                        If (Not IsNothing(Session("isPwdValid"))) Then
                            Session("isPwdValid") = True
                        End If

                        Response.Redirect("../MasterPage.aspx?rt=" & DateTime.Now.ToFileTime())
                    End If

                    Session("isInitPWD") = False
                    Session("changePWDNextTime") = False
                    Session("isPWDReset") = False
                    If (Not IsNothing(Session("isPwdValid"))) Then
                        Session("isPwdValid") = True
                    End If

                    ltlAlert.Text = "alert('Success！');"
                End If
                'End Add by Chenyb       
            End If
        End Sub
    End Class

End Namespace
