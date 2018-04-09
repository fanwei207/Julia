Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class accessApproved
        Inherits BasePage
    Dim chk As New adamClass
    Dim item As ListItem
    Dim strSQL As String
    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal
    Dim ds As DataSet
    Public scrollPos As Integer = 0
    Public scrollPosL As Integer = 0

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
        ltlAlert.Text = ""

        If Not IsPostBack Then
             
            role.SelectedIndex = 0
            item = New ListItem(Request("dname"))
            item.Value = Request("did")
            role.Items.Add(item)

            DropDownList1.SelectedIndex = 0
            item = New ListItem(Request("uname"))
            item.Value = Request("uid")
            DropDownList1.Items.Add(item)

            strSQL = "SELECT m.id,m.name,a.userID,m.description,m.isMenu From tcpc0.dbo.Menu m Inner Join AccessRuleApply a On a.approvedBy is not null and m.id=a.moduleID and a.userID=" & DropDownList1.SelectedItem.Value & " where m.sortOrder is not null and m.url<>'null' Order by m.sortOrder "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Item(4) = True Then
                            item = New ListItem(.Rows(i).Item(1) & "--" & .Rows(i).Item(0).ToString())
                        Else
                            item = New ListItem(" -- " & .Rows(i).Item(1) & "--" & .Rows(i).Item(0).ToString())
                        End If
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        CheckBoxList1.Items.Add(item)
                    Next i
                End If
            End With
            ds.Reset()

            BindData()

        End If

        scrollPos = Request("pos")
        scrollPosL = Request("posL")
        If (scrollPos = Nothing) Then
            scrollPos = 0
        End If
        If (scrollPosL = Nothing) Then
            scrollPosL = 0
        End If

        Dim saveScrollPosition As String
        saveScrollPosition = "<script language='javascript'>"
        saveScrollPosition = saveScrollPosition & "document.getElementById('Panel1').onscroll=saveScrollPosition1;"
        saveScrollPosition = saveScrollPosition & "</script>"

            Dim cstype As Type = Me.[GetType]()
            ClientScript.RegisterStartupScript(cstype, "saveScroll", saveScrollPosition)

            'RegisterStartupScript("saveScroll", saveScrollPosition)
    End Sub
    Private Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        If RadioButtonList1.SelectedIndex = 0 Then
            Dim i As Integer
            For i = 0 To CheckBoxList1.Items.Count - 1
                CheckBoxList1.Items(i).Selected = True
            Next
        Else
            If RadioButtonList1.SelectedIndex = 1 Then
                Dim i As Integer
                For i = 0 To CheckBoxList1.Items.Count - 1
                    CheckBoxList1.Items(i).Selected = False
                Next
            End If
        End If
    End Sub
    Private Sub BindData()
        Dim total As Integer = 0

        strSQL = "SELECT a.userID From tcpc0.dbo.Menu m Inner Join AccessRuleApply a On a.approvedBy is not null and  m.id=a.moduleID and a.userID=" & DropDownList1.SelectedItem.Value & " where m.sortOrder is not null and m.url<>'null' Order by m.sortOrder "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    CheckBoxList1.Items(i).Selected = True
                    total = total + 1
                Next i
            End If
        End With
        ds.Reset()

        Label1.Text = total.ToString()
    End Sub


    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect(chk.urlRand("/admin/accessApproveList.aspx?dp=1&pg=" & Request("pg")))
    End Sub


End Class

End Namespace

