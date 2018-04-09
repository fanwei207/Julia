Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class outputSearchCond
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents ltlAlert As Literal
    Dim item As ListItem
    Dim Query As String
    Dim reader As SqlDataReader
    Dim chk As New adamClass
    Protected WithEvents workHour1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents workHour2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents output1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents output2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents workday1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents workday2 As System.Web.UI.WebControls.TextBox


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            startDate.Text = DateTime.Today.Year.ToString()
            DropDownList1.SelectedValue = DateTime.Today.Month

            'Query = "Auto_EverydayJobSalary"
            'Dim ret As Integer
            'ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, Query)
            'If (ret < 0) Then
            'ltlAlert.Text = "alert('Auto_EverydayJobSalary Error Code: " & ret.ToString() & " ');"
            'End If

            Departmentdropdownlist()
        End If
    End Sub
    Sub Departmentdropdownlist()

        item = New ListItem("--")
        item.Value = 0
        Department.Items.Add(item)
        Query = "SELECT departmentID,Name From Departments where isSalary=1 order by departmentID"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
        While reader.Read()
            item = New ListItem(reader(1))
            item.Value = Convert.ToInt32(reader(0))
            Department.Items.Add(item)
        End While
            reader.Close()
            Department.SelectedIndex = 0
    End Sub
End Class

End Namespace
