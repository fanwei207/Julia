Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Web.Mail
Imports System.Text

Namespace tcpc
    Partial Class doc_applypermission
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim nRet As Integer
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
                BindData()
            End If
        End Sub

        Sub BindData()
            StrSql = " SELECT typeid,createdBy,typename,createdName,createdDate " _
                   & " From qaddoc.dbo.DocumentType where typeid>0 and isDeleted is null "
            StrSql &= " Order by typeid"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("g_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_cate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("created_by", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("created_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_cate_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_user_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_user", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("g_no") = i + 1
                        drow.Item("g_cate") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("created_by") = .Rows(i).Item(3).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(4)) Then
                            drow.Item("created_date") = Format(.Rows(i).Item(4), "yyyy-MM-dd")
                        End If
                        drow.Item("g_cate_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("g_user_id") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("g_user") = .Rows(i).Item(3).ToString().Trim()

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
            If e.CommandName.CompareTo("g_edit") = 0 Then
            End If
        End Sub

        Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
            If Session("uID") = Nothing Then
                Exit Sub
            End If
            Dim readerLev As SqlDataReader
            Dim strSqlLev As String
            strSqlLev = " Select Distinct userid,level From tcpc0.dbo.users Where userid = " & Session("uID") '& Request("id").ToString() & "'"
            readerLev = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSqlLev)
            While (readerLev.Read())
                If readerLev(1).ToString.Trim = Nothing Then
                    ltlAlert.Text = "alert('人事未设定Level值，请联系人事到编辑员工信息设定Level值!');"
                    Exit Sub
                End If
            End While

            Dim i As Integer
            For i = 0 To Datagrid1.Items.Count - 1
                Dim chb As CheckBox = CType(Datagrid1.Items(i).FindControl("chkSelect"), CheckBox)
                If chb.Checked = True Then
                    Dim ddl As DropDownList = CType(Datagrid1.Items(i).FindControl("ddlAdmin"), DropDownList)
                    If ddl.Items.Count > 0 Then
                        If ddl.SelectedValue > 0 Then
                            Dim lev As DropDownList = CType(Datagrid1.Items(i).FindControl("ddlLevel"), DropDownList)

                            Try
                                StrSql = " SELECT count(doc_acc_userid) From qaddoc.dbo.DocumentAccess where doc_acc_userid= " & Session("uID") & " and doc_acc_catid= " & Datagrid1.Items(i).Cells(7).Text
                                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                                    StrSql = " Update qaddoc.dbo.DocumentAccess set doc_acc_level='" & lev.SelectedValue & "',approvedby=null , approveddate=null , createdby='" & ddl.SelectedValue _
                                           & "' where doc_acc_userid= " & Session("uID") & " and doc_acc_catid= " & Datagrid1.Items(i).Cells(7).Text
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                                Else
                                    StrSql = " Insert Into qaddoc.dbo.DocumentAccess(doc_acc_userid,doc_acc_username,doc_acc_catid,doc_acc_catname,doc_acc_level,createdBy,createdDate) "
                                    StrSql &= " Values('" & Session("uID") & "',N'" & Session("uName") & "','" & Datagrid1.Items(i).Cells(7).Text & "',N'" & Datagrid1.Items(i).Cells(1).Text & "','" & lev.SelectedValue & "','" & ddl.SelectedValue & "',getdate()) "
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                                End If

                                Dim mail As MailMessage = New MailMessage

                                Dim mailfrom As String
                                StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & Session("uID") & " and email <>''"
                                mailfrom = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

                                Dim mailto As String
                                StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & ddl.SelectedValue & " and email <>''"
                                mailto = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

                                Dim mailcc As String = ""

                                If mailto <> Nothing Then
                                    If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
                                        'mail.To = mailto.Substring(0, mailto.IndexOf(";"))
                                        'mail.Cc = mailto.Substring(mailto.IndexOf(";") + 1)
                                        mail.To = mailto
                                        mail.Cc = mailcc
                                        mail.From = mailfrom
                                        mail.Subject = "【通知-Doc】申请查看文档权限,请审批"
                                        Dim strMailBody As New StringBuilder()
                                        strMailBody.Append("<META HTTP-EQUIV='Content-Type' CONTENT='text/html;charset=gb2312'><html><head></head><body>")
                                        strMailBody.Append("<br>你好<br><br>")
                                        strMailBody.Append("<br> " & Session("uName") & "申请查看文档 目录Category：" & Datagrid1.Items(i).Cells(1).Text & "的权限，请及时审批<br>")
                                        strMailBody.Append(" 谢谢<br>")
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

                    End If
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
                    Dim ddl As DropDownList = CType(e.Item.FindControl("ddlAdmin"), DropDownList)
                    If ddl.Items.Count <= 0 Then
                        Dim ls As ListItem
                        Dim reader As SqlDataReader
                        StrSql = " Select 0 As doc_acc_userid, '--' As doc_acc_username Union All Select Distinct doc_acc_userid,doc_acc_username " _
                               & " From qaddoc.dbo.DocumentAccess da " _
                               & " Inner Join tcpc0.dbo.users u On u.userID = da.doc_acc_userid And u.leavedate Is Null And u.deleted=0 And u.isactive=1 " _
                               & " Where doc_acc_level = 0 And doc_acc_catid= " & e.Item.Cells(7).Text _
                               & " Order By doc_acc_username "

                        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                        While (reader.Read())
                            ls = New ListItem
                            ls.Value = reader(0)
                            ls.Text = reader(1).ToString.Trim
                            ddl.Items.Add(ls)
                        End While
                        reader.Close()

                        If ddl.Items.Count > 1 Then
                            'CType(e.Item.FindControl("ddlAdmin"), DropDownList).BackColor = Color.Pink
                            e.Item.Cells(2).BackColor = Color.Pink
                            'e.Item.Cells(2).ToolTip = "Have admin list"
                        End If
                    End If


                    Dim ddllevel As DropDownList = CType(e.Item.FindControl("ddllevel"), DropDownList)
                    Dim readerLev As SqlDataReader
                    Dim strSqlLev As String
                    strSqlLev = " Select Distinct userid,level From tcpc0.dbo.users Where userid = " & Session("uID") '& Request("id").ToString() & "'"

                    readerLev = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSqlLev)
                    While (readerLev.Read())
                        If readerLev(1).ToString.Trim = Nothing Then
                            ddllevel.SelectedIndex = 4
                            ddllevel.Enabled = False
                        Else
                            ddllevel.SelectedValue = readerLev(1).ToString.Trim
                            ddllevel.Enabled = False
                        End If
                    End While
                    readerLev.Close()
                    If ddllevel.Items.Count > 1 Then
                        e.Item.Cells(4).BackColor = Color.Pink
                    End If

                Case ListItemType.AlternatingItem
                    Dim ddl As DropDownList = CType(e.Item.FindControl("ddlAdmin"), DropDownList)
                    If ddl.Items.Count <= 0 Then
                        Dim ls As ListItem
                        Dim reader As SqlDataReader
                        StrSql = " Select 0 As doc_acc_userid, '--' As doc_acc_username Union All Select Distinct doc_acc_userid,doc_acc_username " _
                               & " From qaddoc.dbo.DocumentAccess da " _
                               & " Inner Join tcpc0.dbo.users u On u.userID = da.doc_acc_userid And u.leavedate Is Null And u.deleted=0 And u.isactive=1 " _
                               & " Where doc_acc_level = 0 And doc_acc_catid= " & e.Item.Cells(7).Text _
                               & " Order By doc_acc_username "

                        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                        While (reader.Read())
                            ls = New ListItem
                            ls.Value = reader(0)
                            ls.Text = reader(1).ToString.Trim
                            ddl.Items.Add(ls)
                        End While
                        reader.Close()

                        If ddl.Items.Count > 1 Then
                            'CType(e.Item.FindControl("ddlAdmin"), DropDownList).BackColor = Color.Pink
                            e.Item.Cells(2).BackColor = Color.Pink
                            'e.Item.Cells(2).ToolTip = "Have admin list"
                        End If
                    End If

                    Dim ddllevel As DropDownList = CType(e.Item.FindControl("ddllevel"), DropDownList)
                    Dim readerLev As SqlDataReader
                    Dim strSqlLev As String
                    strSqlLev = " Select Distinct userid,level From tcpc0.dbo.users Where userid =  " & Session("uID")

                    readerLev = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSqlLev)
                    While (readerLev.Read())
                        If readerLev(1).ToString.Trim = Nothing Then
                            ddllevel.SelectedIndex = 4
                            ddllevel.Enabled = False
                        Else
                            ddllevel.SelectedValue = readerLev(1).ToString.Trim
                            ddllevel.Enabled = False
                        End If
                    End While
                    readerLev.Close()
                    If ddllevel.Items.Count > 1 Then
                        e.Item.Cells(4).BackColor = Color.Pink
                    End If

                Case ListItemType.EditItem
                    Dim ddl As DropDownList = CType(e.Item.FindControl("ddlAdmin"), DropDownList)
                    If ddl.Items.Count <= 0 Then
                        Dim ls As ListItem
                        Dim reader As SqlDataReader
                        StrSql = " Select 0 As doc_acc_userid, '--' As doc_acc_username Union All Select Distinct doc_acc_userid,doc_acc_username " _
                               & " From qaddoc.dbo.DocumentAccess da " _
                               & " Inner Join tcpc0.dbo.users u On u.userID = da.doc_acc_userid And u.leavedate Is Null And u.deleted=0 And u.isactive=1 " _
                               & " Where doc_acc_level = 0 And doc_acc_catid= " & e.Item.Cells(7).Text _
                               & " Order By doc_acc_username "

                        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                        While (reader.Read())
                            ls = New ListItem
                            ls.Value = reader(0)
                            ls.Text = reader(1).ToString.Trim
                            ddl.Items.Add(ls)
                        End While
                        reader.Close()

                        If ddl.Items.Count > 1 Then
                            'CType(e.Item.FindControl("ddlAdmin"), DropDownList).BackColor = Color.Pink
                            e.Item.Cells(2).BackColor = Color.Pink
                            'e.Item.Cells(2).ToolTip = "Have admin list"
                        End If
                    End If
            End Select
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex 
            BindData()
        End Sub
    End Class
End Namespace













