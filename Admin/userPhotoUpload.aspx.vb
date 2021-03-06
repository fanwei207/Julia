'!*******************************************************************************!
'* @@ NAME				:	userPhotoUpload.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for userPhotoUpload.aspx
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

Partial Class userPhotoUpload
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Public chk As New adamClass
    Dim strSql As String
    Dim ds As DataSet
    Dim reader As SqlDataReader
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
            
            strSql = " Select userNo,username From tcpc0.dbo.Users where userID='" & Request("uid") & "' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                usercode.Text = reader(0)
                username.Text = reader(1)
            End While
            reader.Close()

        End If
    End Sub

    Private Sub Button1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.ServerClick
        Dim imgdatastream As Stream
        Dim imgdatalen, n As Integer
        Dim imgtype, strUserFileName, intLastBackslash, sqlstr, type As String
        Dim imgdata() As Byte
        Dim imgtitle As String = ""

        imgdatastream = filename.PostedFile.InputStream
        imgdatalen = filename.PostedFile.ContentLength
        imgtype = filename.PostedFile.ContentType

        strUserFileName = filename.PostedFile.FileName
        intLastBackslash = strUserFileName.LastIndexOf("\")
        'imgtitle = strUserFileName.Substring(intLastBackslash + 1)
        
        type = strUserFileName.Substring(strUserFileName.LastIndexOf(".") + 1).ToLower()
        imgtitle = Request("uid") & "." & type
        If (imgtitle.Trim().Length <= 0) Then
            ltlAlert.Text = "alert('请选择导入文件.')"
            Exit Sub
        End If
        If type <> "jpg" And type <> "jpeg" And type <> "bmp" And type <> "gif" And type <> "png" Then
            ltlAlert.Text = "alert('导入文件格式不正确，请重新导入.')"
            Exit Sub
        Else
            ReDim imgdata(imgdatalen)
            n = imgdatastream.Read(imgdata, 0, imgdatalen)

            sqlstr = "Delete From tcpc0.dbo.Users_Photo where userID='" & Request("uid") & "' "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sqlstr)

            sqlstr = "insert into tcpc0.dbo.Users_Photo(userID,conetents,type,filename,createdBy,createdDate) values('" & Request("uid") & "', @imgdata ,@imgtype,'" & imgtitle.Trim & "','" & Session("uid") & "',getdate()) "
            Dim paramr As SqlParameter
            paramr = New SqlParameter("@imgdata", SqlDbType.Image)
            paramr.Value = imgdata

            Dim conn As New SqlConnection(chk.dsnx)
            Dim cmd As New SqlCommand(sqlstr, conn)
            cmd.CommandTimeout = 60
            Dim paramData, paramType As SqlParameter

            paramData = New SqlParameter("@imgdata", SqlDbType.Image)
            paramData.Value = imgdata
            cmd.Parameters.Add(paramData)

            paramType = New SqlParameter("@imgtype", SqlDbType.VarChar, 20)
            paramType.Value = imgtype
            cmd.Parameters.Add(paramType)


            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            ltlAlert.Text = "window.opener.location.href='addpersonnel.aspx?id=" & Request("uid") & " ';top.close();"
        End If

    End Sub
End Class

End Namespace
