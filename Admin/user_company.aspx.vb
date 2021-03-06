Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class user_company
        Inherits BasePage
    Dim chk As New adamClass
    Dim item As ListItem
    Dim strSQL As String
    Dim ds As DataSet
    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal

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
            item = New ListItem("--")
            item.Value = 0
                role.Items.Add(item)
                role.SelectedIndex = 0
            strSQL = "SELECT departmentID,name From departments where issalary=1 order by name"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        role.Items.Add(item)
                    Next
                    role.SelectedIndex = 1
                End If
            End With
            ds.Reset()

            loadUser()

            BindData()
        End If
    End Sub
    Function loadUser()
        While DropDownList1.Items.Count > 0
            DropDownList1.Items.RemoveAt(0)
        End While

        If role.SelectedIndex = 0 Then
            Exit Function
        End If

        Dim id As Integer = 0
        If Request("uid") <> Nothing Then
            id = Request("uid")
            role.Enabled = False
            role.SelectedIndex = 0
        End If

            'DropDownList1.SelectedIndex = 0
        'item = New ListItem("--")
        'item.Value = 0
        'DropDownList1.Items.Add(item)
        If id > 0 Then
            strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and deleted=0 and leaveDate is null and isactive=1 and userID=" & id & " and organizationID=" & Session("orgID") & " Order by UserName "
        Else
            strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and deleted=0 and leaveDate is null and isactive=1 and departmentID=" & role.SelectedValue & " and organizationID=" & Session("orgID") & " Order by UserName "
        End If
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(1) & "-" & .Rows(i).Item("userno"))
                    item.Value = Convert.ToInt32(.Rows(i).Item(0))
                    DropDownList1.Items.Add(item)
                    If id = item.Value Then
                        'DropDownList1.SelectedIndex = i + 1
                        DropDownList1.SelectedIndex = i
                    End If
                Next
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
        CheckBoxList1.Items.Clear()
        If DropDownList1.Items.Count = 0 Then
            Exit Sub
        End If

        strSQL = "SELECT p.plantID,p.plantCode, p.description,up.plantID From tcpc0.dbo.plants p Left Outer Join tcpc0.dbo.user_plant up ON up.plantID=p.plantID and up.userID=" & DropDownList1.SelectedValue & " Order by p.plantID "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(1) & "  --  " & .Rows(i).Item(2))
                    item.Value = Convert.ToInt32(.Rows(i).Item(0))
                    CheckBoxList1.Items.Add(item)
                    If Not .Rows(i).IsNull(3) Then
                        CheckBoxList1.Items(i).Selected = True
                    End If
                Next i
            End If
        End With
        ds.Reset()
    End Sub
    Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        RadioButtonList1.SelectedIndex = -1
        BindData()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If DropDownList1.Items.Count = 0 Then
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To CheckBoxList1.Items.Count - 1
            strSQL = "SELECT plantID From tcpc0.dbo.User_plant Where userID=" & DropDownList1.SelectedItem.Value & " and plantID=" & CheckBoxList1.Items(i).Value
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            If ds.Tables(0).Rows.Count > 0 Then
                If CheckBoxList1.Items(i).Selected = False Then
                    strSQL = "Delete From tcpc0.dbo.User_plant Where UserID=" & DropDownList1.SelectedItem.Value & " and plantID=" & CheckBoxList1.Items(i).Value
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                End If
            Else
                If CheckBoxList1.Items(i).Selected = True Then
                    strSQL = "Insert Into tcpc0.dbo.User_plant(UserID,plantID) Values(" & DropDownList1.SelectedItem.Value & "," & CheckBoxList1.Items(i).Value & ")"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                End If
            End If
            ds.Reset()
        Next

        ltlAlert.Text = "alert('    保存成功！    ');"
    End Sub

    Private Sub role_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles role.SelectedIndexChanged
        loadUser()
        BindData()
    End Sub
End Class

End Namespace

