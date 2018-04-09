'* @@ NAME				:	PermissionCopyAMove.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for PermissionCopyAMove.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	September 18 2009
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

    Partial Class PermissionCopyAMove
        Inherits BasePage
       
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

        Public chk As New adamClass
        Dim StrSql As String
        Dim ds As DataSet
        Dim reader As SqlDataReader
        Dim nRet As Integer


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then

                btnCopyPermission.Attributes.Add("onclick", "return confirm('确定复制权限？');")
                btnMovePermission.Attributes.Add("onclick", "return confirm('确定移转(除)权限？');")
            End If
        End Sub



        Protected Sub btnCopyPermission_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyPermission.Click

            Dim intOldUid As Integer = CheckUser(txtOlduser.Text.Trim())
            Dim intNewUid As Integer = CheckUser(txtNewuser.Text.Trim())

            If intOldUid = 0 Then
                ltlAlert.Text = "alert('工号不存在或已离职 ！');Form1.txtOlduser.focus();"
                Exit Sub
            End If

            If intNewUid = 0 Then
                ltlAlert.Text = "alert('工号不存在或已离职！');Form1.txtNewuser.focus();"
                Exit Sub
            End If

            '// 如果复制的员工已经有部分权限则，
            StrSql = " INSERT INTO AccessRule (userID,moduleID,createdBy,createdDate) "
            StrSql &= " SELECT '" & intNewUid.ToString() & "',moduleID,13,getdate() FROM AccessRule "
            StrSql &= " WHERE userID ='" & intOldUid.ToString() & "' "
            StrSql &= " AND moduleID NOT IN (SELECT moduleID FROM AccessRule WHERE userID ='" & intNewUid.ToString() & "')"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, StrSql)

            txtOlduser.Text = ""
            txtNewuser.Text = ""
            ltlAlert.Text = "alert('用户权限复制成功！');Form1.txtOlduser.focus();"
        End Sub



        Protected Sub btnMovePermission_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMovePermission.Click
           
            Dim intOldUid As Integer = CheckLeaveUser(txtOlduser.Text.Trim())
            Dim intNewUid As Integer = -1
            Dim strMsg As String

            If intOldUid = 0 Then
                ltlAlert.Text = "alert('工号不存在！');Form1.txtOlduser.focus();"
                Exit Sub
            End If

            If txtNewuser.Text.Trim.Length > 0 Then
                intNewUid = CheckUser(txtNewuser.Text.Trim())
                If intNewUid = 0 Then
                    ltlAlert.Text = "alert('工号不存在或已离职！');Form1.txtNewuser.focus();"
                    Exit Sub
                End If
            End If

            If intNewUid < 0 Then
                strMsg = "alert('用户权限移除成功！');Form1.txtOlduser.focus(); "
            Else
                StrSql = "UPDATE AccessRule SET userID='" & intNewUid & "' "
                StrSql &= " WHERE userID ='" & intOldUid & "' "
                StrSql &= " And moduleID NOT IN (SELECT moduleID FROM AccessRule WHERE userID ='" & intNewUid.ToString() & "' )"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, StrSql)

                strMsg = "alert('用户权限移转成功！');Form1.txtOlduser.focus(); "
            End If
            StrSql = "DELETE FROM AccessRule WHERE userID='" & intOldUid & "' "
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, StrSql)

            txtOlduser.Text = ""
            txtNewuser.Text = ""
            ltlAlert.Text = strMsg
        End Sub


        Function CheckUser(ByVal UserNo) As Integer
            Dim intUid As Integer = 0
            StrSql = "SELECT userID FROM Users "
            StrSql &= " WHERE leavedate IS NULL AND isactive=1 AND deleted=0 and (roleID>1 OR roleID is null) AND plantcode='" & Session("PlantCode") & "' AND userno='" & UserNo & "'  "
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
            While reader.Read()
                intUid = reader(0)
            End While
            reader.Close()

            CheckUser = intUid
        End Function

        Function CheckLeaveUser(ByVal UserNo) As Integer
            Dim intUid As Integer = 0

            StrSql = "SELECT userID FROM Users "
            StrSql &= " WHERE  isactive=1 AND deleted=0 and (roleID>1 OR roleID is null) AND plantcode='" & Session("PlantCode") & "' AND userno='" & UserNo & "'  "
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, StrSql)
            While reader.Read()
                intUid = reader(0)
            End While
            reader.Close()

            CheckLeaveUser = intUid
        End Function
    End Class
End Namespace
