Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration

Namespace tcpc

    Partial Class qad_viewdocument
        Inherits BasePage
        Dim strsql As String
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal

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

        Public Sub loaddoc()
            Dim reader1 As SqlDataReader
            If Request("code") <> "" Then
                strsql = "select dd.content,dd.type, d.filename from qaddoc.dbo.documentdetail dd inner join qaddoc.dbo.documents d on d.filepath=dd.id where dd.id='" & CInt(Request("filepath")) & "'"
            Else
                strsql = "select dd.content,dd.type, d.filename from qaddoc.dbo.documentdetail dd inner join qaddoc.dbo.documents_his d on d.filepath=dd.id where dd.id='" & CInt(Request("filepath")) & "'"
            End If

            reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)
            reader1.Read()

            If (Not IsDBNull(reader1("content"))) Then
                Dim _byte() As Byte = reader1("content")
                Dim _filename As String = Date.Now.ToFileTime().ToString() & reader1("filename").ToString()
                Dim _fs As FileStream = New FileStream(Server.MapPath("/Excel/") & _filename, FileMode.OpenOrCreate, FileAccess.Write)
                _fs.Write(_byte, 0, _byte.Length)
                _fs.Close()

                ltlAlert.Text = "window.open('/Excel/" & _filename & "', '_blank'); window.close();"
                reader1.Close()
            Else
                Me.Alert("文件不存在！")
            End If
        End Sub



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            'Response.Write("filepath")
            If Request("filepath") = Nothing Or Request("filepath").Trim() = "" Then
                Exit Sub
            End If
            If Session("urole") = "1" Then
                loaddoc()
            Else
                If Request("code") <> "" Then
                    strsql = " If Exists(Select * From qaddoc.dbo.documents d " & IIf(Session("uRole") = 1, "Left Outer", "Inner") & " Join qaddoc.dbo.documentaccess da on d.typeid = da.doc_acc_catid Where d.filepath = '" & Request("filepath") & "'And da.doc_acc_userid = '" & Session("uid") & "') Select 1 Else Select 0 "
                    If Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strsql)) = True Then
                        loaddoc()
                    Else
                        ltlAlert.Text = "alert('你没有权限打开此类文档，请申请！')"
                    End If
                   
                Else
                    strsql = " If Exists(Select * From qaddoc.dbo.documents_his d " & IIf(Session("uRole") = 1, "Left Outer", "Inner") & " Join qaddoc.dbo.documentaccess da on d.typeid = da.doc_acc_catid Where d.filepath = '" & Request("filepath") & "'And da.doc_acc_userid = '" & Session("uid") & "') Select 1 Else Select 0 "
                    If Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strsql)) = True Then
                        loaddoc()
                    Else
                        ltlAlert.Text = "alert('你没有权限打开此类文档，请申请！')"
                    End If
 
                End If    'code if
            End If   'urole if
        End Sub
    End Class

End Namespace
