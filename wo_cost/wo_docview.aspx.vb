Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration


Namespace tcpc

    Partial Class wo_docview
        Inherits System.Web.UI.Page
        Dim strsql As String
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
            Dim reader As SqlDataReader

            strsql = "select att_content,att_type,att_name from wo_attach where att_id='" & CInt(Request("attid")) & "'"
            
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)
            reader.Read()
            Response.ContentType = CStr(reader("att_type")).Trim
            Response.BinaryWrite(reader("att_content"))
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(reader("att_name")))
            Response.Flush()
            Response.Close()
            reader.Close()
        End Sub

    End Class

End Namespace
