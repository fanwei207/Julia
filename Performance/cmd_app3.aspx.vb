Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Web.Mail
 
Namespace tcpc 
    Partial Class cmd_app3
        Inherits BasePage
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
               
                'load
                BindAttend()
                BindSug()

                If Request("mid") <> "" Then
                    Dim reader1 As SqlDataReader
                    StrSql = "select isnull(cmd_user_name,'') ,isnull(cmd_content,'') from knowdb.dbo.cmd_mstr where cmd_mstr_id='" & CInt(Request("mid")) & "'"
                    reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                    With reader1.Read()
                        lbl_dispcontent.Text = reader1(1).ToString()
                        lbl_username.Text = CStr(reader1(0))
                    End With
                    reader1.Close()

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

                btn_close.Attributes.Add("onclick", "return confirm('确定要关闭此记录吗?');")
                btn_reject.Attributes.Add("onclick", "return confirm('确定要删除此记录吗?');")


            End If
        End Sub

        Sub BindAttend()
            StrSql = "select attid,cmd_mstr_id,attuser,attdate,attname from knowdb.dbo.cmd_attach where cmd_mstr_id = '" & Request("mid") & "' "
            StrSql &= " union select attid,0,attuser,attdate,attname from knowdb.dbo.cmd_attachtemp where attuserid = '" & Session("uID") & "'"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

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
                        drow.Item("docid") = .Rows(i).Item("cmd_mstr_id").ToString().Trim()
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
            StrSql = "select cmd_detail_id,cmd_mstr_id,cmd_content,cmd_user_name,cmd_date from knowdb.dbo.cmd_detail where cmd_mstr_id = '" & Request("mid") & "' "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

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

            StrSql = "knowdb.dbo.cmd_InsertAttachTemp"

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
                ltlAlert.Text = "var w=window.open('cmd_docview.aspx?attid=" & e.Item.Cells(0).Text.Trim() & "&mid=" & e.Item.Cells(1).Text.Trim() & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
            ElseIf e.CommandName.CompareTo("docdelete") = 0 Then
                If e.Item.Cells(3).Text.Trim() <> Session("uName") Then
                    ltlAlert.Text = "alert('只能删除本次上传附件！')"
                    Exit Sub
                Else
                    StrSql = "delete from knowdb.dbo.cmd_attachtemp where attid = '" & e.Item.Cells(0).Text.Trim() & "' and attuser=N'" & Session("uName") & "'"
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

            StrSql = "knowdb.dbo.cmd_submit3"
            Dim params(9) As SqlParameter
            params(0) = New SqlParameter("@docid", docid)
            params(1) = New SqlParameter("@sugcontent", txb_sug.Text.Trim())
            params(2) = New SqlParameter("@uID", Session("uID"))
            params(3) = New SqlParameter("@username", Session("uName"))
            params(4) = New SqlParameter("@chooseid", txb_chooseid.Text.Trim())
            params(5) = New SqlParameter("@ccid", txb_ccid.Text.Trim())
            params(6) = New SqlParameter("@email", txb_email.Text.Trim())
            params(7) = New SqlParameter("@plantCode", Session("plantCode"))
            params(8) = New SqlParameter("@deptID", Session("deptID"))
            params(9) = New SqlParameter("@chk", "n")
            
            ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)
            If ret <= 0 Then
                ltlAlert.Text = "alert('提交失败')"
                Exit Sub
            Else
                'StrSql = " Update knowdb.dbo.conn_taken set conn_taken_date = getdate() "
                'StrSql &= "  where conn_mstr_id='" & Request("mid") & "' and conn_taken_id='" & Session("uID") & "' and conn_taken_date is null and conn_detail_id <>'" & ret & "' "
                'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                'Response.Write(StrSql)

                mailfrom = txb_email.Text.Trim
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
                        mail.Subject = "执行情况描述-回签"
                        mail.Body = "内容:" + lbl_dispcontent.Text + "---" + "会签:" + txb_sug.Text
                        BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                        'SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                        'SmtpMail.Send(mail)
                    End If
                End If


                Response.Redirect("cmd_list.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
            End If
        End Sub

        Private Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
            StrSql = "Select cmd_user_id from knowdb.dbo.cmd_mstr where cmd_closeddate is null and cmd_deleteddate is null and cmd_mstr_id = '" & Request("mid") & "' "
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Session("uID") Then
                ltlAlert.Text = "alert('你不能关闭记录！')"
                Exit Sub
            End If

            StrSql = "knowdb.dbo.cmd_submit5"
            Dim params(0) As SqlParameter
            params(0) = New SqlParameter("@mid", Request("mid"))

            SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)

            Response.Redirect("cmd_list.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())

        End Sub

        Private Sub btn_choose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_choose.Click
            ltlAlert.Text = "var w=window.open('cmd_choose2.aspx?mid=" & Request("mid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End Sub

        Private Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
            Response.Redirect("cmd_list.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())
        End Sub

        Private Sub btn_cc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cc.Click
            ltlAlert.Text = "var w=window.open('cmd_choose3.aspx?mid=" & Request("mid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End Sub
        Protected Sub btn_reject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_reject.Click
            StrSql = "Select cmd_user_id from knowdb.dbo.cmd_mstr where cmd_closeddate is null and cmd_deleteddate is null and cmd_mstr_id = '" & Request("mid") & "' "
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Session("uID") Then
                ltlAlert.Text = "alert('你不能删除记录！')"
                Exit Sub
            End If


            StrSql = "Update knowdb.dbo.cmd_mstr set cmd_deleteddate = getdate() where cmd_mstr_id='" & Request("mid") & "' "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)


            Response.Redirect("cmd_list.aspx?mid=" & Request("mid") & "&rm=" & Rnd() & DateTime.Now())


        End Sub
    End Class

End Namespace
