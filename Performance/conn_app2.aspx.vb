Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Web.Mail


Namespace tcpc


Partial Class conn_app2
        Inherits System.Web.UI.Page
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
    Dim ret As Integer
    Protected WithEvents countLabel As System.Web.UI.WebControls.Label
    Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lbl_username As System.Web.UI.WebControls.Label
    Protected WithEvents panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents SelectTypeDropDown As System.Web.UI.WebControls.DropDownList

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

            'load
            BindAttend()

            Dim reader1 As SqlDataReader

            If Request("mid") <> "" Then
                StrSql = "select conn_user_name,conn_content from knowdb.dbo.conn_mstr where conn_mstr_id='" & CInt(Request("mid")) & "'"
                reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                With reader1.Read()
                    lbl_dispcontent.Text = reader1(1)
                    lbl_username.Text = "申请人:" + CStr(reader1(0))
                End With
                reader1.Close()

                lbl_appcontent.Visible = False
                txb_appcontent.Visible = False
                End If

                If Request("moni_id") <> Nothing Then
                    StrSql = "select moni_desc from tcpc0.dbo.perf_monitor where moni_id='" & CInt(Request("moni_id")) & "'"
                    reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                    With reader1.Read()
                        txb_appcontent.Text = reader1(0)
                    End With
                    reader1.Close()
                End If


            StrSql = "select isnull(email,'') from tcpc0.dbo.users where userid='" & Session("uID") & "'"
            reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            With reader1.Read()
                txb_email.Text = reader1(0)
            End With
            reader1.Close()

            If Datagrid1.Items.Count <= 0 Then
                Datagrid1.Visible = False
            Else
                Datagrid1.Visible = True
            End If

            txb_choose.Text = Request("choose")
            txb_chooseid.Text = Request("chooseid")
            txb_cc.Text = Request("cc")
            txb_ccid.Text = Request("ccid")

        End If
    End Sub

    Sub BindAttend()
        StrSql = "select attid,conn_mstr_id,attuser,attdate,attname from knowdb.dbo.conn_attach where conn_mstr_id = '" & Request("mid") & "' "
        StrSql &= " union select attid,0,attuser,attdate,attname from knowdb.dbo.conn_attachtemp where attuserid = '" & Session("uID") & "'"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

        Dim total As Integer
        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("attid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("docid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("attname", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("attUser", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("attdate", System.Type.GetType("System.String")))
        Dim drow As DataRow
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("attid") = .Rows(i).Item("attid").ToString().Trim()
                    drow.Item("docid") = .Rows(i).Item("conn_mstr_id").ToString().Trim()
                    drow.Item("attname") = .Rows(i).Item("attname").ToString().Trim()
                    drow.Item("attuser") = .Rows(i).Item("attuser").ToString().Trim()
                    If IsDBNull(.Rows(i).Item("attdate")) = False Then
                        drow.Item("attdate") = Format(.Rows(i).Item("attdate"), "yy-MM-dd")
                    Else
                        drow.Item("attdate") = ""
                    End If

                    dtl.Rows.Add(drow)
                Next
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)
        Datagrid1.DataSource = dvw
        Datagrid1.DataBind()
        ds.Reset()
        If Datagrid1.Items.Count <= 0 Then
            Datagrid1.Visible = False
        Else
            Datagrid1.Visible = True
        End If

    End Sub

    Private Sub btn_input_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_input.Click
        Dim fname As String = ""
        Dim intLastBackslash As Integer

        If filename.Value.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('请选择上传文件！')"
            Return
        End If


        fname = filename.PostedFile.FileName
        intLastBackslash = fname.LastIndexOf("\")
        fname = fname.Substring(intLastBackslash + 1)
        If (fname.Trim().Length <= 0) Then
            ltlAlert.Text = "alert('请选择上传文件.')"
            Return
        End If

        Dim imgdatastream As Stream
        Dim imgdatalen As Integer
        Dim imgtype As String
        Dim imgdata() As Byte

        imgdatastream = filename.PostedFile.InputStream
        imgdatalen = filename.PostedFile.ContentLength
        imgtype = filename.PostedFile.ContentType

        ReDim imgdata(imgdatalen)

        imgdatastream.Read(imgdata, 0, imgdatalen)

        StrSql = "knowdb.dbo.conn_InsertAttachTemp"

        Dim params(4) As SqlParameter
        params(0) = New SqlParameter("@fname", fname)
        params(1) = New SqlParameter("@imgdata", imgdata)
        params(2) = New SqlParameter("@imgtype", imgtype)
        params(3) = New SqlParameter("@uID", Session("uID"))
        params(4) = New SqlParameter("@username", Session("uName"))
        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)
        If ret < 0 Then
            ltlAlert.Text = "alert('上传失败')"
            Exit Sub
        End If

        BindAttend()
    End Sub

    Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand

        If e.CommandName.CompareTo("docview") = 0 Then
            ltlAlert.Text = "var w=window.open('conn_docview.aspx?attid=" & e.Item.Cells(0).Text.Trim() & "&mid=" & e.Item.Cells(1).Text.Trim() & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        ElseIf e.CommandName.CompareTo("docdelete") = 0 Then
            If e.Item.Cells(3).Text.Trim() <> Session("uName") Then
                ltlAlert.Text = "alert('只能删除自己上传附件！')"
                Exit Sub
            Else
                StrSql = "delete from knowdb.dbo.conn_attachtemp where attid = '" & e.Item.Cells(0).Text.Trim() & "' and attuser=N'" & Session("uName") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            End If

        End If
        BindAttend()

    End Sub

    Private Sub btn_next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_next.Click
        If Session("uID") = Nothing Then
            Exit Sub
        End If

        Dim mailfrom As String
        Dim mailto As String
        Dim mailcc As String
        Dim i As Integer

        If txb_appcontent.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('请填写申请内容！')"
            Exit Sub
        ElseIf txb_appcontent.Text.Trim.Length > 1000 Then
            ltlAlert.Text = "alert('申请内容过长,不能超过1000个字节！')"
            Exit Sub
        End If


        If txb_email.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('请填写自己的邮箱地址！')"
            Exit Sub
            ElseIf baseDomain.checkDomainOR(txb_email.Text.Trim) <> True Then
                ltlAlert.Text = "alert('请填写正确邮箱地址！')"
                Exit Sub
        End If

        If txb_chooseid.Text.Length <= 0 Or txb_choose.Text.Length <= 0 Then
            ltlAlert.Text = "alert('请选择申请提交给谁！')"
            Exit Sub
        End If

        StrSql = "knowdb.dbo.conn_submit"
            Dim params(8) As SqlParameter
        params(0) = New SqlParameter("@uID", Session("uID"))
        params(1) = New SqlParameter("@username", Session("uName"))
        params(2) = New SqlParameter("@appcontent", txb_appcontent.Text.Trim())
        params(3) = New SqlParameter("@chooseid", txb_chooseid.Text.Trim())
        params(4) = New SqlParameter("@ccid", txb_ccid.Text.Trim())
        params(5) = New SqlParameter("@email", txb_email.Text.Trim())
        params(6) = New SqlParameter("@plantCode", Session("plantCode"))
            params(7) = New SqlParameter("@deptID", Session("deptID"))
            If Request("moni_id") <> Nothing Then
                params(8) = New SqlParameter("@connID", Request("moni_id"))
            Else
                params(8) = New SqlParameter("@connID", 0)
            End If


            ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)
            If ret <= 0 Then
                ltlAlert.Text = "alert('申请失败')"
                Exit Sub
            Else

                mailfrom = ConfigurationManager.AppSettings("AdminEmail").ToString()
                mailto = ""
                mailcc = ""


                Dim mail As MailMessage = New MailMessage
                Dim mailstr() As String = Split(txb_chooseid.Text.Trim, ",")

                For i = 0 To mailstr.Length - 2
                    If mailstr(i).Trim.Length > 0 Then
                        StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & CInt(mailstr(i).Trim()) & " and email <>''"
                        If IsDBNull(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)) Or SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Nothing Then

                            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf("@") > 1 And SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf(".com") > 1 Then
                                mailto = mailto + SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.Trim + ";"
                            End If

                        End If
                    End If

                Next

                Dim mailstr2() As String = Split(txb_ccid.Text.Trim, ",")

                For i = 0 To mailstr2.Length - 2
                    If mailstr2(i).Trim.Length > 0 Then
                        StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & CInt(mailstr2(i).Trim()) & " and email <>''"
                        If IsDBNull(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)) Or SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Nothing Then
                            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf("@") > 1 And SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf(".com") > 1 Then
                                mailcc = mailcc + SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.Trim + ";"
                            End If
                        End If
                    End If

                Next

                If mailto <> Nothing Then
                    If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
                        'mail.To = mailto.Substring(0, mailto.IndexOf(";"))
                        'mail.Cc = mailto.Substring(mailto.IndexOf(";") + 1)
                        mail.To = mailto
                        mail.Cc = mailcc
                        mail.From = mailfrom
                        mail.Subject = "100系统--内部业务联系单"
                        'mail.Body = txb_appcontent.Text

                        mail.Body = "<META HTTP-EQUIV='Content-Type' CONTENT='text/html;charset=gb2312'><html><head></head><body>" & txb_appcontent.Text & "<br><br>详情及相关的图片视频等附件请登录<a href='" & baseDomain.getPortalWebsite() & "/'>" & baseDomain.getPortalWebsite() & "/</a>的内部联系单。请及时处理此通知单。</body></html>"
                        mail.BodyFormat = MailFormat.Html


                        If Request("moni_id") <> Nothing Then
                            mail.Subject = "100系统--监控系统纠错通知单,同时已发送至公司主管领导和巡检小组"

                            If Session("PlantCode") = 2 Then
                                mail.Cc = mail.Cc ' ";yn.wang@;gedongwei.szx@" 老代码，写死的邮箱
                            ElseIf Session("PlantCode") = 5 Then
                                mail.Cc = mail.Cc ' ";xieyihua.yql@" 老代码写死邮箱
                            End If
                            mail.Cc = mail.Cc ' ";tangfuying.szx@"写死邮箱
                            'mail.Cc = mail.Cc & ";zhaonaiqi@"写死邮箱

                        End If

                        BasePage.SSendEmail(mailfrom, mailto, mailcc, mail.Subject, mail.Body)

                        'SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                        'SmtpMail.Send(mail)
                    End If
                End If


                If Request("moni_id") <> Nothing Then
                    Response.Redirect("/Performance/perf_monitor.aspx")
                Else
                    Response.Redirect("conn_list.aspx")
                End If

            End If
        End Sub

    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        StrSql = "update knowdb.dbo.conn_mstr set conn_closeddate = getdate() where conn_closeddate is null and conn_deleteddate is null and mid = '" & Request("mid") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            If Request("moni_id") <> Nothing Then
                Response.Redirect("/Performance/perf_monitor.aspx")
            Else
                Response.Redirect("conn_list.aspx")
            End If
        End Sub

        Private Sub btn_choose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_choose.Click
            ltlAlert.Text = "var w=window.open('conn_choose2.aspx?mid=" & Request("mid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End Sub

        Private Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
            If Request("moni_id") <> Nothing Then
                Response.Redirect("/Performance/perf_monitor.aspx")
            Else
                Response.Redirect("conn_list.aspx")
            End If
        End Sub

        Private Sub btn_cc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cc.Click
            ltlAlert.Text = "var w=window.open('conn_choose3.aspx?mid=" & Request("mid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End Sub
    End Class

End Namespace
