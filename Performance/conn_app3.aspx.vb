Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Web.Mail


Namespace tcpc


    Partial Class conn_app3
        Inherits System.Web.UI.Page
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim ret As Integer
        Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents panel1 As System.Web.UI.WebControls.Panel


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
                 
                BindAttend()
                BindSug()

                If Request("mid") <> "" Then
                    Dim reader1 As SqlDataReader
                    StrSql = "select isnull(conn_user_name,'') ,isnull(conn_content,'') from knowdb.dbo.conn_mstr where conn_mstr_id='" & CInt(Request("mid")) & "'"
                    reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                    With reader1.Read()
                        lbl_dispcontent.Text = reader1(1).ToString()
                        lbl_username.Text = CStr(reader1(0))
                    End With
                    reader1.Close()

                    Dim ddt As DataTable
                    Dim count As Integer
                    StrSql = "select conn_taken_name from knowdb.dbo.conn_taken where conn_type=1 and isnull(conn_isFirst,0) = 1 and conn_mstr_id='" & CInt(Request("mid")) & "'"
                    ddt = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql).Tables(0)
                    count = ddt.Rows.Count
                    If count > 0 Then
                        For count = 1 To count
                            lbl_stName.Text += ddt.Rows(count - 1)(0).ToString() + ";"
                        Next
                    End If

                    StrSql = "select conn_taken_name from knowdb.dbo.conn_taken where conn_type=2 and isnull(conn_isFirst,0) = 1 and conn_mstr_id='" & CInt(Request("mid")) & "'"
                    ddt = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql).Tables(0)
                    count = ddt.Rows.Count
                    If count > 0 Then
                        For count = 1 To count
                            lbl_ccName.Text += ddt.Rows(count - 1)(0).ToString() + ";"
                        Next
                    End If

                    StrSql = "select isnull(email,'') from tcpc0.dbo.users where userid='" & Session("uID") & "'"
                    reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                    With reader1.Read()
                        txb_email.Text = reader1(0)
                    End With
                    reader1.Close()
                Else
                    txb_sug.Visible = False
                    lbl_sug.Visible = False
                End If

                If Datagrid1.Items.Count <= 0 Then
                    Datagrid1.Visible = False
                Else
                    Datagrid1.Visible = True
                End If
                If Datagrid2.Items.Count <= 0 Then
                    Datagrid2.Visible = False
                Else
                    Datagrid1.Visible = True
                End If

                txb_choose.Text = Request("choose")
                txb_chooseid.Text = Request("chooseid")
                txb_cc.Text = Request("cc")
                txb_ccid.Text = Request("ccid")

                btn_close.Attributes.Add("onclick", "return confirm('确定要关闭此内部业务联系单吗?');")

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

        Sub BindSug()
            StrSql = "select conn_detail_id,conn_mstr_id,conn_content,conn_user_name,conn_date from knowdb.dbo.conn_detail where conn_mstr_id = '" & Request("mid") & "' "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim total As Integer
            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("sugid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("docid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("sugcontent", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("sugUser", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("sugdate", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("sugid") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("docid") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("sugcontent") = .Rows(i).Item(3).ToString().Trim() + "签于" + .Rows(i).Item(4).ToString() + "--" + .Rows(i).Item(2).ToString().Trim()
                        drow.Item("suguser") = .Rows(i).Item(3).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(4)) = False Then
                            drow.Item("sugdate") = Format(.Rows(i).Item(4), "yyyy-MM-dd")
                        Else
                            drow.Item("sugdate") = ""
                        End If

                        dtl.Rows.Add(drow)
                    Next

                End If
            End With
            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid2.DataSource = dvw
            Datagrid2.DataBind()
            ds.Reset()

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
                    ltlAlert.Text = "alert('只能删除本次上传附件！')"
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

            If txb_sug.Text.Trim.Length > 1000 Then
                ltlAlert.Text = "alert('会签内容不能超过1000个字节！')"
                Exit Sub
            ElseIf txb_sug.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请填写会签内容！')"
                Exit Sub
            End If

            If txb_choose.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('请选择提交给谁！')"
                Exit Sub
            End If

            Dim docid As Integer
            docid = 0
            Dim mailfrom As String
            Dim mailto As String
            Dim mailcc As String
            Dim i As Integer

            docid = CInt(Request("mid"))

            If txb_email.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请填写自己的邮箱地址！')"
                Exit Sub
            ElseIf txb_email.Text.Trim.IndexOf("@") = -1 Then
                ltlAlert.Text = "alert('请填写正确邮箱地址！')"
                Exit Sub
            End If

            '将抄送的一并写入txb_sug
            Dim _sug = txb_sug.Text.Trim()

            If txb_ccid.Text.Trim().Length > 0 Then

                _sug &= "&nbsp;&nbsp;&nbsp;&nbsp;抄送给：" & txb_cc.Text.Trim()
            End If


            StrSql = "knowdb.dbo.conn_submit3"
            Dim params(9) As SqlParameter
            params(0) = New SqlParameter("@docid", docid)
            params(1) = New SqlParameter("@sugcontent", _sug)
            params(2) = New SqlParameter("@uID", Session("uID"))
            params(3) = New SqlParameter("@username", Session("uName"))
            params(4) = New SqlParameter("@chooseid", txb_chooseid.Text.Trim())
            params(5) = New SqlParameter("@ccid", txb_ccid.Text.Trim())
            params(6) = New SqlParameter("@email", txb_email.Text.Trim())
            params(7) = New SqlParameter("@plantCode", Session("plantCode"))
            params(8) = New SqlParameter("@deptID", Session("deptID"))
            If CheckBox1.Checked = True Then
                params(9) = New SqlParameter("@chk", "y")
            Else
                params(9) = New SqlParameter("@chk", "n")
            End If

            ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)
            If ret <= 0 Then
                ltlAlert.Text = "alert('提交失败')"
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
                        mail.Subject = "100系统内部业务联系单-回签"


                        'mail.Body = "申请内容:" + lbl_dispcontent.Text + "     会签内容:" + txb_sug.Text
                        'mail.Body = mail.Body & "     详情及相关的图片视频等附件请登录 "+baseDomain.getPortalWebsite()+"/ 的内部联系单。"

                        mail.Body = "<META HTTP-EQUIV='Content-Type' CONTENT='text/html;charset=gb2312'><html><head></head><body>申请内容:" & lbl_dispcontent.Text & "<br><br>会签内容:" & txb_sug.Text & "<br><br>详情及相关的图片视频等附件请登录<a href='" & baseDomain.getPortalWebsite() & "/'>" & baseDomain.getPortalWebsite() & "/</a>的内部联系单。请及时处理此通知单。</body></html>"
                        mail.BodyFormat = MailFormat.Html

                        If Request("moni_id") <> Nothing Then
                            mail.Subject = "100系统--监控系统纠错通知单,同时已发送至公司主管领导和巡检小组"

                            If Session("PlantCode") = 2 Then
                                mail.Cc = mail.Cc ' ";yn.wang@;gedongwei.szx@"
                            ElseIf Session("PlantCode") = 5 Then
                                mail.Cc = mail.Cc ' ";xieyihua.yql@"
                            End If
                            mail.Cc = mail.Cc ' ";tangfuying.szx"
                            'mail.Cc = mail.Cc & ";zhaonaiqi"

                        End If
                        BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                        'SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                        'SmtpMail.Send(mail)
                    End If
                End If

                If Request("moni_id") <> Nothing Then
                    Response.Redirect("perf_monitor.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
                Else

                    Response.Redirect("conn_list.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
                End If
            End If
        End Sub

        Private Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
            If Session("uRole") > 1 Then
                If Session("uID") <> 2420 Then   '唐富英
                    StrSql = "Select conn_user_id from knowdb.dbo.conn_mstr where conn_closeddate is null and conn_deleteddate is null and conn_mstr_id = '" & Request("mid") & "' "
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Session("uID") Then
                        ltlAlert.Text = "alert('你不能关闭他人申请的内部业务联系单！')"
                        Exit Sub
                    End If
                End If
            End If

            If ddlscore.SelectedValue = -1 Then
                ltlAlert.Text = "alert('在关闭内部业务联系单之前请先评分！')"
                Exit Sub
            End If

            StrSql = "knowdb.dbo.conn_submit5"
            Dim params(0) As SqlParameter
            params(0) = New SqlParameter("@mid", Request("mid"))

            SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)

            StrSql = "knowdb.dbo.conn_score"
            Dim params1(1) As SqlParameter
            params1(0) = New SqlParameter("@mid", Request("mid"))
            params1(1) = New SqlParameter("@score", ddlscore.SelectedValue.ToString())

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, StrSql, params1)

            If Request("moni_id") <> Nothing Then
                Response.Redirect("perf_monitor.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
            Else
                Response.Redirect("conn_list.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
            End If
        End Sub

        Private Sub btn_choose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_choose.Click
            ltlAlert.Text = "var w=window.open('conn_choose2.aspx?mid=" & Request("mid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End Sub

        Private Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
            If Request("moni_id") <> Nothing Then
                Response.Redirect("perf_monitor.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
            Else
                Response.Redirect("conn_list.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
            End If
        End Sub

        Private Sub btn_cc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cc.Click
            ltlAlert.Text = "var w=window.open('conn_choose3.aspx?mid=" & Request("mid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End Sub

        Private Sub btn_reject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reject.Click
            'If Session("uID") = Nothing Then
            '    Exit Sub
            'End If

            'StrSql = "Select conn_user_id from knowdb.dbo.conn_mstr where conn_closeddate is null and conn_deleteddate is null and conn_mstr_id = '" & Request("mid") & "' "
            'If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Session("uID") Then
            '    ltlAlert.Text = "alert('这不是你发出的内部业务联系单，你没有拒绝的权限！')"
            '    Exit Sub
            'End If

            If txb_sug.Text.Trim.Length > 1000 Then
                ltlAlert.Text = "alert('会签内容不能超过1000个字节！')"
                Exit Sub
            ElseIf txb_sug.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请填写会签内容！')"
                Exit Sub
            End If

            Dim docid As Integer
            docid = 0
            Dim mailfrom As String
            Dim mailto As String
            Dim mailcc As String
            Dim i As Integer

            docid = CInt(Request("mid"))

            If txb_email.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请填写自己的邮箱地址！')"
                Exit Sub
            ElseIf txb_email.Text.Trim.IndexOf("@") = -1 Then
                ltlAlert.Text = "alert('请填写正确邮箱地址！')"
                Exit Sub
            End If

            StrSql = "knowdb.dbo.conn_submit4"
            Dim params(8) As SqlParameter
            params(0) = New SqlParameter("@docid", docid)
            params(1) = New SqlParameter("@sugcontent", txb_sug.Text.Trim())
            params(2) = New SqlParameter("@uID", Session("uID"))
            params(3) = New SqlParameter("@username", Session("uName"))
            params(4) = New SqlParameter("@chooseid", txb_chooseid.Text.Trim())
            params(5) = New SqlParameter("@ccid", txb_ccid.Text.Trim())
            params(6) = New SqlParameter("@email", txb_email.Text.Trim())
            params(7) = New SqlParameter("@plantCode", Session("plantCode"))
            params(8) = New SqlParameter("@deptID", Session("deptID"))

            ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)
            If ret <= 0 Then
                ltlAlert.Text = "alert('提交失败')"
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
                        mail.Subject = "100系统内部业务联系单-拒绝你的回签"
                        mail.Body = "申请内容:" + lbl_dispcontent.Text + "---" + "会签内容:" + txb_sug.Text
                        BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                        'SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                        'SmtpMail.Send(mail)
                    End If
                End If

                If Request("moni_id") <> Nothing Then
                    Response.Redirect("perf_monitor.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
                Else
                    Response.Redirect("conn_list.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
                End If
            End If
        End Sub
    End Class

End Namespace
