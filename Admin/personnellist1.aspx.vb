Imports adamFuncs


Namespace tcpc

Partial Class personnellist1
    Inherits System.Web.UI.Page
    'Protected WithEvents ltlAlert As Literal
    Public chk As New adamClass

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
        If Not IsPostBack() Then

        End If

        If Session("linkurl") = Nothing Then
            Session("linkurl") = "/admin/personnellist.aspx"
            Response.Redirect(Session("linkurl"))
        Else
            '    Session("linkurl") = Request.RawUrl
            Response.Redirect(Session("linkurl"))
            'End If
        End If
    End Sub

End Class

End Namespace
