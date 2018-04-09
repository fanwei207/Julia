Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.Mail


Namespace tcpc

Partial Class accessApply
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
            Session("Title") = "用户权限申请"
                Dim i As Integer

                role.SelectedIndex = 0
                i = 0
            strSQL = "SELECT departmentID,name From departments where departmentID=" & Session("deptID") & " order by name"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            role.Items.Add(item)
                        Next
                End If
            End With
                ds.Reset()

                strSQL = ""
                i = 0

                strSQL = "select * from menu where parentId = 0 and ismenu = 1 order by sortorder"
                ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1).ToString() + "-" + .Rows(i).Item(5).ToString())
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            drp_menu.Items.Add(item)
                        Next
                    End If


                End With


            DropDownList1.SelectedIndex = 0
            item = New ListItem(Session("uName"))
            item.Value = Session("uID")
            DropDownList1.Items.Add(item)


                LoadMenu(drp_menu.SelectedValue, 0)


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

            Dim saveScrollPosition As String
            saveScrollPosition = "<script language='javascript'>"
            saveScrollPosition = saveScrollPosition & "document.getElementById('Panel1').onscroll=saveScrollPosition1;"
            saveScrollPosition = saveScrollPosition & "</script>"

            RegisterStartupScript("saveScroll", saveScrollPosition)
    End Sub
        Function LoadMenu(ByVal strPar As Integer, ByVal ll As Integer)

            Dim strA As String
            strA = ""
            ll = ll + 1
            Dim j As Integer
            For j = 1 To ll - 1
                strA = "---" + strA
            Next j
            strSQL = "SELECT m.id,m.name,m.description,m.isMenu,m.url, m.isPublic From tcpc0.dbo.Menu m where parentID = '" & strPar & "' and m.isDisable=0 and m.sortOrder is not null Order by m.sortOrder "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Item(4).ToString().Trim() <> "null" And Convert.ToBoolean(.Rows(i).Item(5)) <> True Then
                            item = New ListItem(strA & .Rows(i).Item(2).ToString())
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            CheckBoxList1.Items.Add(item)
                        End If
                        LoadMenu(Convert.ToInt32(.Rows(i).Item(0)), ll)
                    Next i
                End If
            End With
            ds.Reset()
        End Function

        Protected Sub drp_menu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp_menu.SelectedIndexChanged
            CheckBoxList1.Items.Clear()
            LoadMenu(drp_menu.SelectedValue, 0)
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
        If DropDownList1.Items.Count = 0 Then
            Exit Sub
        End If

            Dim i As Integer
        Dim ret As Integer
        For i = CheckBoxList1.Items.Count - 1 To 0 Step -1
            ret = CheckBoxList1.Items(i).Value
            While (ret > 0) 'repeat untill to root
                'userid, moduleid,ischecked 
                ret = Nodes(DropDownList1.SelectedItem.Value, ret, CheckBoxList1.Items(i).Selected)
                'Response.Write(ret & ",")
            End While
            Next

            Dim mailfrom As String

            strSQL = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & CInt(Session("uID")) & " and email <>''"
            If IsDBNull(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)) Or SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) <> Nothing Then

                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL).ToString.IndexOf("@") > 1 And SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL).ToString.IndexOf(".com") > 1 Then
                    mailfrom = mailfrom + SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL).ToString.Trim + ";"
                End If
            Else
                mailfrom = "admin@tcp-china.com"
            End If



            Dim mail As MailMessage = New MailMessage

            strSQL = "Select email From ApplicationEmails Where type=10 AND (deleted='0' or deleted is null) order by id "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            Dim err As Integer = -1
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                err = 0
                For i = 0 To .Rows.Count - 1
                    Try

                            Mail.To = .Rows(i).Item(0).ToString()
                            Mail.From = mailfrom
                            mail.Subject = "100系统权限申请"
                            mail.Body = "公司代码:" & Convert.ToString(Session("PlantCode")) & "---" & "姓名:" & Convert.ToString(Session("uName"))

                            BasePage.SSendEmail(mailfrom, mail.To, mail.Cc, mail.Subject, mail.Body)

                            'SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                            'SmtpMail.Send(Mail)

                        Catch
                        err = err + 1
                    End Try
                Next
            End If
        End With
        ds.Reset()

        If (err > 0) Then
            ltlAlert.Text = "alert('    申请成功，请等待批准 （通知邮件失败）！    ');"
        Else
            ltlAlert.Text = "alert('    申请成功，请等待批准！    ');"
        End If

        BindData()
    End Sub
    Private Sub role_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles role.SelectedIndexChanged
            If drp_menu.SelectedIndex > 0 Then
                LoadMenu(drp_menu.SelectedValue, 0)
            Else
                LoadMenu(0, 0)
            End If
        'BindData()
    End Sub
    Function Nodes(ByVal ur As String, ByVal md As String, ByVal isChecked As Boolean) As Integer
        Dim i As Integer
        Dim str As String
        Dim reader As SqlDataReader

        Nodes = 0
        strSQL = "SELECT isnull(a.userID,0),m.id,m.parentID From tcpc0.dbo.Menu m Left Outer Join AccessRuleApply a On m.id=a.moduleID and a.userID=" & ur & " and a.moduleID=" & md & " where m.id=" & md
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                If .Rows(i).Item(0) = 0 Then
                    If isChecked = True Then
                        str = "Select userID From AccessRuleApply Where approvedBy is not null and userID=" & DropDownList1.SelectedItem.Value & " and moduleID=" & md
                        If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, str) <= 0 Then
                            str = "Insert Into AccessRuleApply(userID,moduleID,createdDate) Values(" & ur & "," & md & ",'" & Format(DateTime.Now(), "yyyy-MM-dd") & "')"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str)
                        End If
                    End If
                End If
                Nodes = .Rows(i).Item(2)
            End If
        End With
        ds.Reset()
        Return Nodes
    End Function


        Protected Sub CheckBoxList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxList1.SelectedIndexChanged

        End Sub
    End Class

End Namespace

