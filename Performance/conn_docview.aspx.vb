Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration


Namespace tcpc

    Partial Class conn_docview
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

            If CInt(Request("mid")) = 0 Then
                strsql = "select attcontent,atttype,attname from knowdb.dbo.conn_attachtemp where attid='" & CInt(Request("attid")) & "'"
            Else
                strsql = "select attcontent,atttype,attname from knowdb.dbo.conn_attach where attid='" & CInt(Request("attid")) & "'"
            End If

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)
            reader.Read()

            Dim _byte() As Byte = reader("attcontent")
            Dim _filename As String = Date.Now.ToFileTime().ToString() & reader("attname").ToString()
            Dim _fs As FileStream = New FileStream(Server.MapPath("/Excel/") & _filename, FileMode.OpenOrCreate, FileAccess.Write)
            _fs.Write(_byte, 0, _byte.Length)
            _fs.Close()

            ltlAlert.Text = "window.open('/Excel/" & _filename & "', '_blank'); window.close();"
            reader.Close()
        End Sub

    End Class

End Namespace
