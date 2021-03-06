Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Web.Mail
Imports System.Text


Namespace tcpc
    Partial Class doc_assignpermission
        Inherits BasePage 
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet

        Dim item As ListItem


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
                Dim ls As ListItem

                ls = New ListItem
                ls.Value = 0
                ls.Text = "--"
                SelectTypeDropDown.Items.Add(ls)

                Dim reader As SqlDataReader
                StrSql = " Select typeid, typename From qaddoc.dbo.DocumentType t " _
                       & " Inner Join qaddoc.dbo.DocumentAccess a On a.doc_acc_catid=t.typeid And a.doc_acc_level=0 And a.doc_acc_userid='" & Session("uID") _
                       & "' Where isDeleted Is Null " _
                       & " Order By typeid "
                'Response.Write(strSql)

                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                While (reader.Read())
                    ls = New ListItem
                    ls.Value = reader(0)
                    ls.Text = reader(1).ToString.Trim

                    SelectTypeDropDown.Items.Add(ls)
                End While
                reader.Close()

                BindData()
            End If
        End Sub

        Sub BindData()
            StrSql = " Select t.typeid, a.createdBy, t.typename, t.createdName, t.createdDate, a.doc_acc_userid, a.doc_acc_username, a.doc_acc_level, Isnull(a.approvedby, 0) " _
                   & " From qaddoc.dbo.DocumentAccess a " _
                   & " Left Outer Join qaddoc.dbo.DocumentType t On t.typeid=a.doc_acc_catid And t.typeid>0 And t.isDeleted Is Null " _
                   & " Where a.doc_acc_level>0 And a.doc_acc_catid In ( Select doc_acc_catid From qaddoc.dbo.DocumentAccess Where doc_acc_userid = '" & Session("uID") & "' And doc_acc_level = 0 ) "
            If chkAll.Checked = False Then
                StrSql &= " And a.approvedby Is Null And a.approveddate Is Null And a.createdby='" & Session("uID") & "'"
            End If
            If SelectTypeDropDown.SelectedIndex > 0 Then
                StrSql &= " And a.doc_acc_catid=" & SelectTypeDropDown.SelectedValue
            End If
            StrSql &= " Order by typeid"

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("g_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("userid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("username", System.Type.GetType("System.String"))) 
            dtl.Columns.Add(New DataColumn("g_cate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("created_by", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("created_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_cate_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_user_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_user", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("approveid", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("g_no") = i + 1
                        drow.Item("userid") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("username") = .Rows(i).Item(6).ToString().Trim()

                        drow.Item("g_cate") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("created_by") = .Rows(i).Item(3).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(4)) Then
                            drow.Item("created_date") = Format(.Rows(i).Item(4), "yyyy-MM-dd")
                        End If
                        drow.Item("g_cate_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("g_user_id") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("g_user") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("approveid") = .Rows(i).Item(8).ToString().Trim()
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()
        End Sub

        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then 
                StrSql = "Delete From qaddoc.dbo.DocumentAccess where doc_acc_userid = '" & e.Item.Cells(1).Text() & "' and createdby='" & Session("uID") & "' and doc_acc_catid=" & e.Item.Cells(8).Text()
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                Datagrid1.EditItemIndex = -1
                BindData()
            End If
        End Sub

        Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
            If Session("uID") = Nothing Then
                Exit Sub
            End If

            Dim i As Integer
            For i = 0 To Datagrid1.Items.Count - 1
                Dim chb As CheckBox = CType(Datagrid1.Items(i).FindControl("chkSelect"), CheckBox)
                If chb.Checked = True Then
                    Dim lev As DropDownList = CType(Datagrid1.Items(i).FindControl("ddlLevel"), DropDownList)
                    Try
                        StrSql = " Update qaddoc.dbo.DocumentAccess set doc_acc_level='" & lev.SelectedValue & "',approvedby='" & Session("uID") & "',approveddate=getdate()  where doc_acc_userid='" & Datagrid1.Items(i).Cells(1).Text & "' and createdby= " & Session("uID") & " and doc_acc_catid= " & Datagrid1.Items(i).Cells(8).Text
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                        Dim mail As MailMessage = New MailMessage

                        Dim mailfrom As String
                        StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & Session("uID") & " and email <>''"
                        mailfrom = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

                        Dim mailto As String
                        StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & Datagrid1.Items(i).Cells(1).Text & " and email <>''"
                        mailto = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

                        Dim mailcc As String = ""

                        If mailto <> Nothing Then
                            If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
                                
                                mail.To = mailto
                                mail.Cc = mailcc
                                mail.From = mailfrom
                                mail.Subject = "【通知-Doc】查看文档权限申请已通过审批"
                                'mail.Body = txb_appcontent.Text
                                Dim strMailBody As New StringBuilder()
                                strMailBody.Append("<META HTTP-EQUIV='Content-Type' CONTENT='text/html;charset=gb2312'><html><head></head><body>")
                                strMailBody.Append("<br>Dear " & Datagrid1.Items(i).Cells(2).Text & ",<br>")
                                strMailBody.Append("  <br>")
                                strMailBody.Append("你申请查看所在目录Category：" & Datagrid1.Items(i).Cells(3).Text & " 的文档权限申请，审批者 " & Session("uName") & "已审批通过<br>")
                                strMailBody.Append("</body></html>") 
                                mail.Body = Convert.ToString(strMailBody)
                                mail.BodyFormat = MailFormat.Html
                                BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                                'SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                                'SmtpMail.Send(mail)
                            End If
                        End If
                    Catch
                    End Try
                End If

            Next
            BindData()
        End Sub

        Protected Sub btn_help_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_help.Click
            ltlAlert.Text = "var w=window.open('/docs/doc_help.doc','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
        End Sub

        Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(11)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to remove it?');")

                    CType(e.Item.FindControl("ddllevel"), DropDownList).SelectedIndex = Convert.ToInt32(e.Item.Cells(10).Text.Trim()) - 1

                    If e.Item.Cells(9).Text.Trim() <> Session("uID").ToString.Trim() Then
                        CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled = False
                        CType(e.Item.FindControl("ddllevel"), DropDownList).Enabled = False
                        myDeleteButton.Enabled = False
                        myDeleteButton.Text = "&nbsp;"
                    End If

                    If e.Item.Cells(12).Text.Trim() = "0" And CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled Then
                        e.Item.Cells(4).BackColor = Color.Pink
                        e.Item.Cells(4).ToolTip = "Need to approve"
                        CType(e.Item.FindControl("ddllevel"), DropDownList).Enabled = False
                    End If

                    If e.Item.Cells(12).Text.Trim() <> "0" And CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled Then
                        e.Item.Cells(4).BackColor = Color.PaleGreen
                        e.Item.Cells(4).ToolTip = "Have been approved"
                        CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled = False
                        CType(e.Item.FindControl("ddllevel"), DropDownList).Enabled = False
                    End If

                Case ListItemType.AlternatingItem
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(11)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to remove it?');")

                    CType(e.Item.FindControl("ddllevel"), DropDownList).SelectedIndex = Convert.ToInt32(e.Item.Cells(10).Text.Trim()) - 1

                    If e.Item.Cells(9).Text.Trim() <> Session("uID").ToString.Trim() Then
                        CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled = False
                        CType(e.Item.FindControl("ddllevel"), DropDownList).Enabled = False
                        myDeleteButton.Enabled = False
                        myDeleteButton.Text = "&nbsp;"
                    End If

                    If e.Item.Cells(12).Text.Trim() = "0" And CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled Then
                        e.Item.Cells(4).BackColor = Color.Pink
                        e.Item.Cells(4).ToolTip = "Need to approve"
                        CType(e.Item.FindControl("ddllevel"), DropDownList).Enabled = False
                    End If

                    If e.Item.Cells(12).Text.Trim() <> "0" And CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled Then
                        e.Item.Cells(4).BackColor = Color.PaleGreen
                        e.Item.Cells(4).ToolTip = "Have been approved"
                        CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled = False
                        CType(e.Item.FindControl("ddllevel"), DropDownList).Enabled = False
                    End If

                Case ListItemType.EditItem
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(11)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to remove it?');")

                    CType(e.Item.FindControl("ddllevel"), DropDownList).SelectedIndex = Convert.ToInt32(e.Item.Cells(10).Text.Trim()) - 1

                    If e.Item.Cells(9).Text.Trim() <> Session("uID").ToString.Trim() Then
                        CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled = False
                        CType(e.Item.FindControl("ddllevel"), DropDownList).Enabled = False
                        myDeleteButton.Enabled = False
                        myDeleteButton.Text = "&nbsp;"
                    End If

                    If e.Item.Cells(12).Text.Trim() = "0" And CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled Then
                        e.Item.Cells(4).BackColor = Color.Pink
                        e.Item.Cells(4).ToolTip = "Need to approve"
                    End If

                    If e.Item.Cells(12).Text.Trim() <> "0" And CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled Then
                        e.Item.Cells(4).BackColor = Color.PaleGreen
                        e.Item.Cells(4).ToolTip = "Have been approved"
                        CType(e.Item.FindControl("chkSelect"), CheckBox).Enabled = False
                        CType(e.Item.FindControl("ddllevel"), DropDownList).Enabled = False
                    End If
            End Select
        End Sub

        Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
            BindData()
        End Sub

        Protected Sub SelectTypeDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectTypeDropDown.SelectedIndexChanged
            BindData()
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













