'!*******************************************************************************!
'* @@ NAME				:	UsersShowPictures.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for UsersShowPictures.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	January 21 2007
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration


Namespace tcpc

Partial Class UsersShowPictures
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim strsql As String
    Dim chk As New adamClass

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim reader As SqlDataReader

        strsql = "Select conetents,type,filename From tcpc0.dbo.Users_Photo where userid='" & Request("uid") & "'"
        'Response.Write(strsql)
        'Exit Sub
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)
        reader.Read()
        Response.Buffer = True
        Response.Expires = 0

        Response.ContentType = CStr(reader("type")).Trim
        Response.BinaryWrite(reader("conetents"))
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(reader("filename")))

        Response.Flush()
        Response.Close()
        ''Response.End()
        reader.Close()
    End Sub

End Class

End Namespace
