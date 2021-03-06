'!*******************************************************************************!
'* @@ NAME				:	qad_bom_Export.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for qad_bom_Export.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	October 29 2007
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class inv_count_Export
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        ltlAlert.Text = ""
        If Not (IsPostBack) Then

            Dim item As ListItem
            Dim ds As DataSet
            Dim strSQL As String

            item = New ListItem("--")
            item.Value = 0
            DropDownList1.Items.Add(item)
            strSQL = "SELECT warehouseID,name From warehouse order by warehouseID"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        DropDownList1.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()

            DropDownList1.SelectedIndex = 0
        End If

    End Sub

    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        If DropDownList1.SelectedIndex > 0 Then
            Response.Redirect("/QAD/ExportInvQad2.aspx?wh=" & DropDownList1.SelectedItem.Text & "&wid=" & DropDownList1.SelectedValue)
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DropDownList1.SelectedIndex > 0 Then
            Response.Redirect("/QAD/ExportInvQad22.aspx?wh=" & DropDownList1.SelectedItem.Text & "&wid=" & DropDownList1.SelectedValue)
        End If
    End Sub
End Class

End Namespace
