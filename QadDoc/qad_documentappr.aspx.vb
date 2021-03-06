Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.Mail

Namespace tcpc
 
    Partial Class qad_documentappr
        Inherits BasePage 
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strSql As String
        Dim ds As DataSet
        Dim reader As SqlDataReader

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents company As System.Web.UI.WebControls.Label
        Protected WithEvents country As System.Web.UI.WebControls.Label
        Protected WithEvents area As System.Web.UI.WebControls.Label
        Protected WithEvents packDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents onBoardDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents containerType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ordersOutput As System.Web.UI.WebControls.Button


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
                Session("type") = ""
                Session("cate") = "" 
                BindData() 
            End If
        End Sub
        Sub BindData() 
            strSql = "select a.appr_userid,a.appr_username,d.createdBy,d.createdname,d.createdDate,case when a.appr_apprdate is null then 'No' else 'Yes' end,a.appr_apprdate,isnull(a.appr_comments,''),d.typename,d.catename,d.name,d.version,d.description from qaddoc.dbo.documents d "
            strSql &= " Left Outer join qaddoc.dbo.DocumentApprove a on d.id=a.appr_docid "
            strSql &= " where d.id is not null and d.id=" & Request("catid")
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql) 
            'Response.Write(strSql)

            Dim dtl As New DataTable
            Dim total As Integer = 0
            dtl.Columns.Add(New DataColumn("userid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("uname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("comment", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("isappr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("checkeddate", System.Type.GetType("System.String")))

            With ds.Tables(0)
                Dim drow As DataRow
                If .Rows.Count > 0 Then

                    lbl_cat.Text = .Rows(0).Item(9).ToString().Trim()
                    lbl_dir.Text = .Rows(0).Item(8).ToString().Trim()
                    lbl_doc.Text = .Rows(0).Item(10).ToString().Trim()
                    lbl_ver.Text = .Rows(0).Item(11).ToString().Trim()
                    lbl_by.Text = .Rows(0).Item(3).ToString().Trim()
                    lbl_date.Text = .Rows(0).Item(4).ToString().Trim()
                    lbl_desc.Text = .Rows(0).Item(12).ToString().Trim()
                    lbl_uid.Text = .Rows(0).Item(2).ToString().Trim()

                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("userid") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("uname") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("comment") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("isappr") = .Rows(i).Item(5).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(6)) = False Then
                            drow.Item("checkeddate") = Format(.Rows(i).Item(6), "yyyy-MM-dd HH:mm")
                        Else
                            drow.Item("checkeddate") = ""
                        End If
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)
            If (Session("orderby").Length <= 0) Then
                Session("orderby") = "uname"
            End If
            Try
                dvw.Sort = Session("orderby") & Session("orderdir")
                DgDoc.DataSource = dvw
                DgDoc.DataBind()
            Catch 'ex As Exception
                '    Response.Write(ex.Message)
            End Try

        End Sub
        Private Sub DgDoc_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DgDoc.PageIndexChanged
            DgDoc.CurrentPageIndex = e.NewPageIndex
            DgDoc.SelectedIndex = -1
            BindData()
        End Sub
        Private Sub DgDoc_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DgDoc.SortCommand
            Session("orderby") = e.SortExpression.ToString()
            If Session("orderdir") = " ASC" Then
                Session("orderdir") = " DESC"
            Else
                Session("orderdir") = " ASC"
            End If
            BindData()
        End Sub

        Private Sub DgDoc_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgDoc.ItemCommand
            
            BindData()
        End Sub

        Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
            strSql = "Update qaddoc.dbo.DocumentApprove set appr_apprdate=null,appr_comments=null"
            strSql &= " where appr_docid=" & Request("catid") & " and appr_userid='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            Dim mail As MailMessage = New MailMessage

            Dim mailfrom As String
            strSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & Session("uID") & " and email <>''"
            mailfrom = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

            Dim mailto As String
            strSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & lbl_uid.Text & " and email <>''"
            mailto = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

            Dim mailcc As String = ""

            If mailto <> Nothing Then
                If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
                    'mail.To = mailto.Substring(0, mailto.IndexOf(";"))
                    'mail.Cc = mailto.Substring(mailto.IndexOf(";") + 1)
                    mail.To = mailto
                    mail.Cc = mailcc
                    mail.From = mailfrom
                    mail.Subject = "Approval of " & lbl_doc.Text & " has been canceled by " & Session("uName")
                    'mail.Body = txb_appcontent.Text

                    mail.Body = "<META HTTP-EQUIV='Content-Type' CONTENT='text/html;charset=gb2312'><html><head></head><body>Dear " & lbl_by.Text & ",<br><br>Please login the website for more detailed information. <br><br>Thanks. </body></html>"
                    mail.BodyFormat = MailFormat.Html

                    BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                    'SmtpMail.SmtpServer = "10.3.0.65"
                    'SmtpMail.Send(mail)
                End If
            End If 
            txb_search.Text = "" 
            BindData()
        End Sub

        Protected Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
            Response.Redirect("qad_documentapprove.aspx?catid=" & Request("catid") & "&rm=" & DateTime.Now.ToString())
        End Sub

        Protected Sub btn_view_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click

        End Sub

        Protected Sub btn_pver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pver.Click

        End Sub

        Protected Sub btn_appr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_appr.Click
            If txb_search.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('Comments are required.');"
                txb_search.Focus()
                Exit Sub
            End If

            strSql = "Update qaddoc.dbo.DocumentApprove set appr_apprdate=getdate(),appr_comments=N'" & txb_search.Text & "'"
            strSql &= " where appr_docid=" & Request("catid") & " and appr_userid='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
             
            Dim mail As MailMessage = New MailMessage 
            Dim mailfrom As String
            strSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & Session("uID") & " and email <>''"
            mailfrom = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

            Dim mailto As String
            strSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & lbl_uid.Text & " and email <>''"
            mailto = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

            Dim mailcc As String = ""

            If mailto <> Nothing Then
                If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
                    'mail.To = mailto.Substring(0, mailto.IndexOf(";"))
                    'mail.Cc = mailto.Substring(mailto.IndexOf(";") + 1)
                    mail.To = mailto
                    mail.Cc = mailcc
                    mail.From = mailfrom
                    mail.Subject = lbl_doc.Text & " Approved by " & Session("uName")
                    'mail.Body = txb_appcontent.Text

                    mail.Body = "<META HTTP-EQUIV='Content-Type' CONTENT='text/html;charset=gb2312'><html><head></head><body>Dear " & lbl_by.Text & ",<br><br>Please login the website for more detailed information. <br><br>Thanks. </body></html>"
                    mail.BodyFormat = MailFormat.Html
                    BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                    'SmtpMail.SmtpServer = "10.3.0.65"
                    'SmtpMail.Send(mail)
                End If
            End If

            txb_search.Text = "" 
            BindData()
        End Sub
    End Class

End Namespace
