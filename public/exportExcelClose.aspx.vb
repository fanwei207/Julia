Namespace tcpc

Partial Class exportExcelClose
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
        '    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Literal1.Text = "alert('没有定义要输出的内容-" & Session("EXTitle") & ":" & Session("EXSQL") & "'); window.close();"
    End Sub

End Class

End Namespace
