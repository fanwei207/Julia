'* @@ NAME				:	PasswordReset.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for PasswordReset.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	February 11 2009
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class PasswordReset
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Dim strSql As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then 
                Button2.Attributes.Add("onclick", "return confirm('工号与姓名是否正确？');")
            End If
        End Sub

        Sub workerNo_changed(ByVal sender As Object, ByVal e As System.EventArgs)
            username.Text = ""
            userID.Text = ""
            userIC.Text = ""
            strSql = " select userID,username,IC from tcpc0.dbo.users where isnull(roleID,-1)<>1 and userno='" & usercode.Text.Trim & "' and plantcode='" & Session("PlantCode") & "' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                username.Text = reader(1)
                userID.Text = reader(0)
                userIC.Text = reader(2)
            End While
            reader.Close()
            If userID.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('工号不存在！');Form1.usercode.focus();"
                usercode.Text = ""
                Exit Sub
            End If
            If (userIC.Text.Trim.Length > 0) Then
                userIC.Text = userIC.Text.Trim.Substring(6, 8)
            End If
            Button2.Enabled = True
        End Sub

        Sub BtnSave_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
            If usercode.Text.Length <= 0 Then
                ltlAlert.Text = "alert('工号不能为空，请输入工号！');Form1.usercode.focus();"
                Exit Sub 
            End If
             
            If userID.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('条件不符合，请重新输入！');Form1.usercode.focus();"
                Exit Sub
            End If

            strSql = " update tcpc0.dbo.users set userPWD='" & chk.encryptPWD(usercode.Text.ToUpper().Trim() + userIC.Text.Trim()) & "',isPWDReset=1 where roleID<>1 and userID='" & userID.Text & "' "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            ltlAlert.Text = "alert('修改成功！');Form1.usercode.focus();"
            usercode.Text = ""
            userID.Text = ""
            username.Text = ""
        End Sub
    End Class

End Namespace
