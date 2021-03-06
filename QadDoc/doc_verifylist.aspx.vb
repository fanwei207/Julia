Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Configuration
Imports System.Web.Mail
Imports System.Text


Namespace tcpc
    Partial Class doc_verifylist
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
               
                BindCategory()
            BindData()
            End If
        End Sub
        Sub BindCategory() 
            Dim ls As ListItem 
            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            SelectTypeDropDown.Items.Add(ls)

            ''& " Inner Join qaddoc.dbo.DocumentAccess a On a.doc_acc_catid=t.typeid And a.doc_acc_level=0 And a.doc_acc_userid='" & Session("uID") _
            Dim reader As SqlDataReader
            StrSql = " Select typeid, typename From qaddoc.dbo.DocumentType t  "
            If chkAll.Checked = False Then
                StrSql &= "Inner Join qaddoc.dbo.DocumentAccess a On a.doc_acc_catid=t.typeid And a.doc_acc_userid='" & Session("uID") & "' And a.doc_acc_level=0 "
            End If
            StrSql &= " Where isDeleted Is Null   Order By typeid "
            'Response.Write(strSql)

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim 
                SelectTypeDropDown.Items.Add(ls)
            End While
            reader.Close()
        End Sub

        Sub BindData()
            StrSql = " Select id, docid, typeid, typename, cateid, catename, [name], description, version, filename, verifyBy " _
                   & " From qaddoc.dbo.DocumentVerify " _
                   & " Where verifyResult Is Null And isFailure Is Null "
            If chkAll.Checked = False Then
                StrSql &= " And verifyBy ='" & Session("uID") & "'"
            End If
            If SelectTypeDropDown.SelectedIndex > 0 Then
                StrSql &= " And typeid=" & SelectTypeDropDown.SelectedValue
            End If
            StrSql &= " Order by typeid, cateid, docid "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("No", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("typeid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("cateid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("uid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("TypeName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("CateName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("DocName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("FileName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("DocVer", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("Docid", System.Type.GetType("System.Int32")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("No") = i + 1
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("typeid") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("cateid") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("uid") = .Rows(i).Item(10).ToString().Trim()
                        drow.Item("TypeName") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("CateName") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("DocName") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("FileName") = .Rows(i).Item(9).ToString().Trim()
                        drow.Item("DocVer") = Convert.ToInt32(.Rows(i).Item(8).ToString().Trim())
                        drow.Item("Docid") = Convert.ToInt32(.Rows(i).Item(1).ToString().Trim())
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

        Private Sub datagrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("DetailBtn") = 0 Then
                Response.Redirect("/qaddoc/qad_documentdetail1.aspx?typeID=" & e.Item.Cells(1).Text.Trim() & "&cateid=" + e.Item.Cells(2).Text.Trim() _
                    & "&typename=" & e.Item.Cells(12).Text.Trim() & "&catename=" & e.Item.Cells(13).Text.Trim() & "&docName= " & e.Item.Cells(5).Text.Trim() & "&frm=verify&rm=" & DateTime.Now(), True)
            ElseIf e.CommandName.CompareTo("PassBtn") = 0 Then

                'Dim params1(2) As SqlParameter
                'params1(0) = New SqlParameter("@docId", e.Item.Cells(14).Text.Trim())
                'params1(1) = New SqlParameter("@lockPart", SqlDbType.VarChar, 500)
                'params1(1).Direction = ParameterDirection.Output
                'If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, "qaddoc.dbo.sp_qad_checkLockULByDoc", params1) = 1 Then
                '    ltlAlert.Text = "alert('文档关联零件所属产品" & params1(1).Value.ToString() & "已锁定，不能升级，请拒绝！');"
                '    Exit Sub
                'End If
                StrSql = "qaddoc.dbo.sp_UpdateDocumentVerify"

                Dim params(4) As SqlParameter
                params(0) = New SqlParameter("@id", Convert.ToInt64(e.Item.Cells(0).Text.Trim()))
                params(1) = New SqlParameter("@uid", Convert.ToInt64(Session("uID")))
                params(2) = New SqlParameter("@result", True)
                params(3) = New SqlParameter("@reason", CType(e.Item.Cells(8).FindControl("txtReason"), TextBox).Text.Trim())

                If Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)) Then
                    ltlAlert.Text = "alert('Commit success!');"

                    BindData()
                Else
                    ltlAlert.Text = "alert('Commit failure, please try again!');"
                End If
            ElseIf e.CommandName.CompareTo("NoPassBtn") = 0 Then
                If Len(CType(e.Item.Cells(8).FindControl("txtReason"), TextBox).Text.Trim()) = 0 Then
                    ltlAlert.Text = "alert('Please fill the failure reason of this document!');"
                    Exit Sub
                Else
                    StrSql = "qaddoc.dbo.sp_UpdateDocumentVerify"

                    Dim params(4) As SqlParameter
                    params(0) = New SqlParameter("@id", Convert.ToInt64(e.Item.Cells(0).Text.Trim()))
                    params(1) = New SqlParameter("@uid", Convert.ToInt64(Session("uID")))
                    params(2) = New SqlParameter("@result", False)
                    params(3) = New SqlParameter("@reason", CType(e.Item.Cells(8).FindControl("txtReason"), TextBox).Text.Trim())

                    Try
                        If Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)) = True Then
                            Dim mail As MailMessage = New MailMessage

                            Dim mailfrom As String
                            StrSql = " Select email From tcpc0.dbo.users Where deleted=0 And isactive=1 And leavedate Is Null And userid=" & Session("uID") & " And email <>''"
                            mailfrom = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

                            Dim mailto As String
                            StrSql = " Select u.email From qaddoc.dbo.Documents d " _
                                   & " Inner Join tcpc0.dbo.users u On u.userid = d.createdBy And u.deleted = 0 And u.isActive = 1 And u.leavedate Is Null " _
                                   & " Where d.id = '" & e.Item.Cells(14).Text.Trim() & "'"

                            mailto = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

                            If mailto <> Nothing Then
                                If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
                                    Try
                                        mail.To = mailto
                                        mail.From = IIf(mailfrom = Nothing, ConfigurationManager.AppSettings("AdminEmail"), mailfrom)
                                        mail.Subject = "【通知-Doc】上传的文档审批未通过，请知悉"
                                        Dim strMailBody As New StringBuilder()
                                        strMailBody.Append("<META HTTP-EQUIV='Content-Type' CONTENT='text/html;charset=gb2312'><html><head></head><body>")
                                        strMailBody.Append("<br>你好:<br><br>")
                                        strMailBody.Append(" 你上传的文档( <b>DocName:</b>  " & e.Item.Cells(5).Text.Trim() & " ) 审批未通过，原因： " & CType(e.Item.Cells(10).FindControl("txtReason"), TextBox).Text.Trim() & "<br>")
                                        strMailBody.Append(" 文档所在目录 <b>Category:</b> " & e.Item.Cells(12).Text.Trim() & " &nbsp;<br> &nbsp;<b>Type:</b> " & e.Item.Cells(13).Text.Trim() & "<br>")
                                        strMailBody.Append("</body></html>")
                                        mail.Body = Convert.ToString(strMailBody)
                                        mail.BodyFormat = MailFormat.Html
                                        BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                                        'SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                                        'SmtpMail.Send(mail)

                                        e.Item.Cells(10).Enabled = False
                                        e.Item.Cells(10).Text = "&nbsp"
                                        e.Item.Cells(11).Enabled = False
                                        e.Item.Cells(11).Text = "&nbsp" 
                                        ltlAlert.Text = "alert('系统已处理你的审批（Nopass），且发送邮件通知上传人');"
                                    Catch ex As Exception
                                        ltlAlert.Text = "alert('Commit success but send email failure!');"
                                    End Try
                              
                                End If
                            Else
                                BindData()
                            End If
                        Else
                            ltlAlert.Text = "alert('Commit failure, please try again!');"
                        End If
                    Catch
                    End Try
                End If
            End If
        End Sub

        Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
            BindData()
        End Sub

        Protected Sub SelectTypeDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectTypeDropDown.SelectedIndexChanged
            BindData()
        End Sub

        Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    If e.Item.Cells(3).Text.Trim() = Session("uID").ToString().Trim() Then
                        e.Item.Cells(10).Attributes.Add("onclick", "return confirm('Are you sure you want to set pass of this document?');")
                        e.Item.Cells(11).Attributes.Add("onclick", "return confirm('Are you sure you want to set not pass of this document?');")
                    Else
                        e.Item.Cells(10).Enabled = False
                        e.Item.Cells(10).Text = "&nbsp"
                        e.Item.Cells(11).Enabled = False
                        e.Item.Cells(11).Text = "&nbsp"
                    End If

                Case ListItemType.AlternatingItem
                    If e.Item.Cells(3).Text.Trim() = Session("uID").ToString().Trim() Then
                        e.Item.Cells(10).Attributes.Add("onclick", "return confirm('Are you sure you want to set pass of this document?');")
                        e.Item.Cells(11).Attributes.Add("onclick", "return confirm('Are you sure you want to set not pass of this document?');")
                    Else
                        e.Item.Cells(10).Enabled = False
                        e.Item.Cells(10).Text = "&nbsp"
                        e.Item.Cells(11).Enabled = False
                        e.Item.Cells(11).Text = "&nbsp"
                    End If

                Case ListItemType.EditItem
                    If e.Item.Cells(3).Text.Trim() = Session("uID").ToString().Trim() Then
                        e.Item.Cells(10).Attributes.Add("onclick", "return confirm('Are you sure you want to set pass of this document?');")
                        e.Item.Cells(11).Attributes.Add("onclick", "return confirm('Are you sure you want to set not pass of this document?');")
                    Else
                        e.Item.Cells(10).Enabled = False
                        e.Item.Cells(10).Text = "&nbsp"
                        e.Item.Cells(11).Enabled = False
                        e.Item.Cells(11).Text = "&nbsp"
                    End If
            End Select
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













