Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class myprofile
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
                'dpModule.SelectedIndex = 0
            If Session("uRole") = 1 Then
                strSQL = " Select m.id, m.name From tcpc0.dbo.Menu m  where m.isProfile=1 and sortOrder is not null and url is not null  and url<>'null' order by sortOrder "
            Else
                strSQL = " Select m.id, m.name From tcpc0.dbo.Menu m Inner Join tcpc0.dbo.AccessRule ar On ar.moduleID=m.id Inner Join tcpc0.dbo.users u On u.userID=ar.userID and u.userID=" & Session("uID") & " where  m.isProfile=1 and sortOrder is not null  and url is not null and url<>'null' and (m.Hidden is null or m.Hidden=" & Session("conceal") & ") order by sortOrder "
            End If
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        dpModule.Items.Add(item)
                        If Convert.ToInt32(.Rows(i).Item(0)) = Session("ModuleID") Then
                            dpModule.SelectedIndex = i
                        End If
                    Next i
                End If
            End With
            ds.Reset()

            loadData()

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

        RegisterStartupScript("saveScroll", saveScrollPosition)
    End Sub
    Function loadData()
        CheckBoxList1.Items.Clear()

        strSQL = "SELECT disableItem From profile_menu where moduleID=" & dpModule.SelectedItem.Value & " Order by id "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(0))
                    item.Value = Convert.ToString(.Rows(i).Item(0))
                    CheckBoxList1.Items.Add(item)
                Next i
            End If
        End With
        ds.Reset()

    End Function
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
        strSQL = "SELECT pd.disableItem From profile_menu pm Left Outer Join profile_display pd on pm.moduleID=pd.moduleID and pm.disableItem=pd.disableItem and pd.userID='" & Session("uID") & "' where pm.moduleID=" & dpModule.SelectedItem.Value & " Order by pm.id "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    If Not .Rows(i).IsNull(0) Then
                        CheckBoxList1.Items(i).Selected = True
                    End If
                Next i
            End If
        End With
        ds.Reset()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim str As String
        str = "Delete From profile_display Where userID=" & Session("uID")
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str)

        Dim i As Integer
        Dim ret As Integer
        For i = CheckBoxList1.Items.Count - 1 To 0 Step -1
            If CheckBoxList1.Items(i).Selected Then
                str = "Insert Into profile_display(userID,moduleID,disableItem,createdDate) Values('" & Session("uID") & "','" & dpModule.SelectedValue & "',N'" & CheckBoxList1.Items(i).Text & "',getdate())"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str)
            End If
        Next

        ltlAlert.Text = "alert('    保存成功！    ');"

        BindData()
    End Sub
    Private Sub dpModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dpModule.SelectedIndexChanged
        loadData()
        BindData()
    End Sub

End Class

End Namespace

