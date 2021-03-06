Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class homepage
        Inherits BasePage
    Dim chk As New adamClass
    Dim item As ListItem
    Dim strSQL As String
    Dim ds As DataSet
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
             
            BindData()
        End If
    End Sub
    Private Sub BindData()
        If Session("uRole") <= 1 Then
            strSQL = "SELECT m.id,m.name,m.description From tcpc0.dbo.Menu m  where  m.sortOrder is not null and isMenu=1 and Isnull(url,'') <> '' and url<>'null' Order by m.sortOrder "
        Else
            strSQL = "SELECT m.id,m.name,m.description,m.sortOrder From tcpc0.dbo.Menu m Inner Join tcpc0.dbo.AccessRule a On m.id=a.moduleID and a.userID=" & Session("uID") & " where  m.isPublic<>1 and m.sortOrder is not null and m.isMenu=1 and Isnull(m.url,'') <> '' and m.url<>'null' "
            strSQL &= "Union SELECT m.id,m.name,m.description,m.sortOrder From tcpc0.dbo.Menu m  where m.isPublic=1 and m.sortOrder is not null and m.isMenu=1 and Isnull(m.url,'') <> '' and m.url<>'null' "
            strSQL &= " Order by m.sortOrder "
        End If
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        Dim ind As Integer = 0
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(1) & ":&nbsp;&nbsp;&nbsp;&nbsp;<font color=purple>" & .Rows(i).Item(2) & "</font>")
                    item.Value = Convert.ToInt32(.Rows(i).Item(0))
                    RadioButtonList1.Items.Add(item)
                    If .Rows(i).Item(0) = Session("homeID") Then
                        RadioButtonList1.Items(i).Selected = True
                    End If
                Next i
            End If
        End With
        ds.Reset()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim i As Integer
        For i = 0 To RadioButtonList1.Items.Count - 1
            If RadioButtonList1.Items(i).Selected = True Then
                strSQL = "Update tcpc0.dbo.users Set homeID=" & RadioButtonList1.SelectedItem.Value & " Where userID=" & Session("uID")
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                Session("homeID") = RadioButtonList1.SelectedItem.Value
                ltlAlert.Text = "alert('  主页设置完成！  ');"
                Exit For
            End If
        Next i
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        strSQL = "Update tcpc0.dbo.users Set homeID=null Where userID=" & Session("uID")
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
        Session("homeID") = 0
        Dim i As Integer
        For i = 0 To RadioButtonList1.Items.Count - 1
            RadioButtonList1.Items(i).Selected = False
        Next i
        ltlAlert.Text = "alert('  已清除用户的主页设置！  ');"
    End Sub
End Class

End Namespace

