Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class accessForAllDept
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
                item = New ListItem("--")
                item.Value = 0
                role.Items.Add(item)
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


                strSQL = "SELECT m.id,m.name From tcpc0.dbo.Menu m where m.sortOrder is not null and m.url<>'null' Order by m.sortOrder "
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            CheckBoxList1.Items.Add(item)
                        Next i
                    End If
                End With
                ds.Reset()


                'BindData()
            End If

            scrollPos = Request("pos")
            scrollPosL = Request("posL")
            If (scrollPos = Nothing) Then
                scrollPos = 0
            End If
            If (scrollPosL = Nothing) Then
                scrollPosL = 0
            End If

            hidScrollPos.Value = scrollPos.ToString()
            hidScrollPosL.Value = scrollPosL.ToString()

            Dim saveScrollPosition As String
            saveScrollPosition = "<script language='javascript'>"
            saveScrollPosition = saveScrollPosition & "document.getElementById('Panel1').onscroll=saveScrollPosition1;"
            saveScrollPosition = saveScrollPosition & "</script>"

            Dim cstype As Type = Me.[GetType]()
            ClientScript.RegisterStartupScript(cstype, "saveScroll", saveScrollPosition)

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
            Dim i As Integer
            For i = 0 To CheckBoxList1.Items.Count - 1
                CheckBoxList1.Items(i).Selected = False
            Next
        End Sub
        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            Dim i As Integer
            Dim j As Integer
            Dim str As String
            For i = CheckBoxList1.Items.Count - 1 To 0 Step -1
                If CheckBoxList1.Items(i).Selected Then
                    strSQL = "SELECT u.userID,a.userID From tcpc0.dbo.users u Left Outer Join tcpc0.dbo.AccessRule a on a.userID=u.userID and a.moduleID='" & CheckBoxList1.Items(i).Value & "' where u.deleted=0 and u.isactive=1 and u.plantCode='" & Session("PlantCode") & "' and departmentid=" & role.SelectedValue
                    ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                    With ds.Tables(0)
                        If (.Rows.Count > 0) Then
                            For j = 0 To .Rows.Count - 1
                                If IsDBNull(.Rows(j).Item(1)) Then
                                    str = "Insert Into tcpc0.dbo.AccessRule(userID,moduleID,createdBy,createdDate) Values('" & .Rows(j).Item(0) & "','" & CheckBoxList1.Items(i).Value & "','" & Session("uID") & "',getdate())"
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str)
                                End If
                            Next
                        End If
                    End With
                End If
            Next
            ltlAlert.Text = "alert('    保存成功！    ');"

            BindData()
        End Sub
        Private Sub role_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles role.SelectedIndexChanged
            BindData()
        End Sub
    End Class

End Namespace

