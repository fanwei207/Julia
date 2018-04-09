Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration
Imports CommClass


Namespace tcpc

    Partial Class rdw_bomview
        Inherits System.Web.UI.Page
        Dim strsql As String

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

            If CInt(Request("mid")) = 0 Then
                strsql = "select RDW_Content,RDW_Type,RDW_name from RD_Workflow.dbo.RDW_BOM where RDW_DocsID='" & CInt(Request("did")) & "'"
            Else
                strsql = "select RDW_Content,RDW_Type,RDW_name from RD_Workflow.dbo.RDW_BOM where RDW_DocsID='" & CInt(Request("did")) & "'"
            End If

            reader = SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn_rdw"), CommandType.Text, strsql)
            reader.Read()
            Response.ContentType = CStr(reader("RDW_Type")).Trim
            Response.BinaryWrite(reader("RDW_Content"))
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(reader("RDW_name")))
            Response.Flush()
            Response.Close()
            reader.Close()
        End Sub

    End Class

End Namespace
