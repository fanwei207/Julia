Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.Mail


Namespace tcpc


Partial Class application
        Inherits System.Web.UI.Page
    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal
    Dim strSQL As String
    Dim ds As DataSet
    Dim chk As New adamClass

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

    End Sub

    Private Sub go_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles go.Click
        ltlAlert.Text = ""
        If (reason.Text.Length > 500) Then
            ltlAlert.Text = "alert(原因不能长于500个字符。);"
            Exit Sub
        End If

        If (desc.Text.Length > 2000) Then
            ltlAlert.Text = "alert(内容不能长于2000个字符。);"
            Exit Sub
        End If

        If (reason.Text.Length = 0 And desc.Text.Length = 0) Then
            Exit Sub
        End If

        strSQL = "Insert Into Applications(type,reason,description,createdBy,createdDate,status) Values(" & type.SelectedValue & ",N'" & chk.sqlEncode(reason.Text) & "',N'" & chk.sqlEncode(desc.Text) & "'," & Session("uID") & ",getdate(), N'已申请')"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

        strSQL = "Select email From ApplicationEmails Where type='" & type.SelectedValue & "' AND (deleted='0' or deleted is null)"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        Dim err As Integer = -1
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                err = 0
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    Try
                        Dim mail As MailMessage = New MailMessage
                        mail.To = .Rows(i).Item(0)
                            mail.From = ConfigurationManager.AppSettings("AdminEmail").ToString()
                        mail.Subject = Session("uName") & " -- " & type.SelectedItem.Text & " (" & type.SelectedValue.ToString() & ")"
                        mail.Body = "Reason: " & reason.Text & "      Description: " & desc.Text
                        'basic authentication
                        mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
                            'set your username here
                            'mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "fx")
                            'set your password here
                            'mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "123")
                            'your real server goes her
                            BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                            'SmtpMail.SmtpServer = "mail.fengxinsoftware.com"
                            'SmtpMail.Send(mail)
                        Catch
                        err = err + 1
                    End Try
                Next
            End If
        End With
        ds.Reset()
        reason.Text = ""
        desc.Text = ""
        If (err > 0) Then
            ltlAlert.Text = "alert('" & err.ToString() & " 邮件失败！');"
        Else
            ltlAlert.Text = "alert('报警 / 申请邮件已发送！');"
        End If
    End Sub
End Class

End Namespace
