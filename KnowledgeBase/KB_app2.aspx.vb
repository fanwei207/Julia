Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Web.Mail


Namespace tcpc


Partial Class KB_app2
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
            'check 
            'Dim nRet As Integer
            'nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 100103069)
            'If nRet <= 0 Then
            '    Response.Redirect("/public/Message.aspx?type=" & nRet.ToString(), True)
            'End If

            'load

            LoadDocType()
            BindAttend()
            Dim reader1 As SqlDataReader

            If Request("docid") <> "" Then
                StrSql = "select docuser,doccontent from knowdb.dbo.application where docid='" & CInt(Request("docid")) & "'"
                reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                With reader1.Read()
                    lbl_dispcontent.Text = reader1("doccontent")
                    lbl_username.Text = "申请人:" + CStr(reader1("docuser"))
                End With
                reader1.Close()

                SelectTypeDropDown.SelectedValue = CInt(Request("typeid"))
                SelectTypeDropDown.Enabled = False
                lbl_appcontent.Visible = False
                txb_appcontent.Visible = False
            End If

                'StrSql = "select isnull(email,'') from tcpc0.dbo.users where userid='" & Session("uID") & "'"
                'reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                'With reader1.Read()
                '    txb_email.Text = reader1(0)
                'End With
                'reader1.Close()

            If Request("typeid") <> "" Then
                SelectTypeDropDown.SelectedValue = CInt(Request("typeid"))
            End If

            If Datagrid1.Items.Count <= 0 Then
                Datagrid1.Visible = False
            Else
                Datagrid1.Visible = True
            End If

            txb_choose.Text = Request("choose")
            txb_chooseid.Text = Request("chooseid")

        End If
    End Sub

    Sub BindAttend()
        StrSql = "select attid,docid,attuser,attdate,attname from knowdb.dbo.attach where docid = '" & Request("docid") & "' "
        StrSql &= " union select attid,0,attuser,attdate,attname from knowdb.dbo.attachtemp where attuserid = '" & Session("uID") & "'"
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
                    drow.Item("docid") = .Rows(i).Item("docid").ToString().Trim()
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

    Sub LoadDocType()
        Dim ls As ListItem
        Dim reader As SqlDataReader
        ls = New ListItem
        ls.Value = 0
        ls.Text = "-----"
        SelectTypeDropDown.Items.Add(ls)
        StrSql = "select typeid,typename from knowdb.dbo.DocType "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1).ToString.Trim
            SelectTypeDropDown.Items.Add(ls)
        End While
        reader.Close()

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
            ltlAlert.Text = "alert('请选择导入文件.')"
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

        StrSql = "knowdb.dbo.InsertAttachTemp"

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
            ltlAlert.Text = "var w=window.open('/knowledgebase/kb_docview.aspx?attid=" & e.Item.Cells(0).Text.Trim() & "&docid=" & e.Item.Cells(1).Text.Trim() & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        ElseIf e.CommandName.CompareTo("docdelete") = 0 Then

            If e.Item.Cells(3).Text.Trim() <> Session("uName") Then
                ltlAlert.Text = "alert('只能删除自己上传附件！')"
                Exit Sub
            Else
                StrSql = "delete from knowdb.dbo.attachtemp where attid = '" & e.Item.Cells(0).Text.Trim() & "' and attuser=N'" & Session("uName") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            End If

        End If
        BindAttend()

    End Sub

    Private Sub btn_next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_next.Click
        Dim isnew As Boolean
        Dim docid As Integer
        docid = 0
        Dim mailfrom As String
        Dim mailto As String
        Dim i As Integer


        If Request("docid") = "" Then
            If SelectTypeDropDown.SelectedValue <= 0 Then
                ltlAlert.Text = "alert('请选择申请类型！')"
                Exit Sub
            Else
            End If
            If txb_appcontent.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请填写申请内容！')"
                Exit Sub
            ElseIf txb_appcontent.Text.Trim.Length > 1000 Then
                ltlAlert.Text = "alert('申请内容过长,不能超过1000个字节！')"
                Exit Sub
            End If

            isnew = True
            docid = 0


        End If


            
        If txb_chooseid.Text.Length <= 0 Or txb_choose.Text.Length <= 0 Then
            ltlAlert.Text = "alert('请选择申请提交给谁！')"
            Exit Sub
        End If

        StrSql = "knowdb.dbo.submit2"
        Dim params(11) As SqlParameter
        params(0) = New SqlParameter("@typeid", SelectTypeDropDown.SelectedItem.Value)
        params(1) = New SqlParameter("@typename", SelectTypeDropDown.SelectedItem.Text)
        params(2) = New SqlParameter("@appcontent", txb_appcontent.Text.Trim())
        params(3) = New SqlParameter("@sugcontent", "")
        params(4) = New SqlParameter("@uID", Session("uID"))
        params(5) = New SqlParameter("@username", Session("uName"))
        params(6) = New SqlParameter("@docid", docid)
        params(7) = New SqlParameter("@isnew", isnew)
        params(8) = New SqlParameter("@choose", txb_chooseid.Text.Trim())
        params(9) = New SqlParameter("@chooseuser", txb_choose.Text.Trim())
        params(10) = New SqlParameter("@statusid", 1)
            params(11) = New SqlParameter("@email", String.Empty)

        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)
        If ret < 0 Then
            ltlAlert.Text = "alert('申请失败')"
            Exit Sub
        Else


                'mailfrom = txb_email.Text.Trim
                'mailto = ""


                'Dim mail As MailMessage = New MailMessage
                'Dim mailstr() As String = Split(txb_chooseid.Text.Trim, ",")

                'For i = 0 To mailstr.Length - 2
                '    If mailstr(i).Trim.Length > 0 Then
                '        StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & CInt(mailstr(i).Trim()) & " and email <>''"
                '        If IsDBNull(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)) Or SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Nothing Then

                '            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf("@") > 1 And SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf(".com") > 1 Then
                '                mailto = mailto + SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.Trim + ";"
                '            End If

                '        End If
                '    End If

                'Next

                'If mailto <> Nothing Then
                '    If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
                '        mail.To = mailto.Substring(0, mailto.IndexOf(";"))
                '        mail.Cc = mailto.Substring(mailto.IndexOf(";") + 1)
                '        mail.From = mailfrom
                '        mail.Subject = SelectTypeDropDown.SelectedItem.Text
                '        mail.Body = txb_appcontent.Text
                '            SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                '        SmtpMail.Send(mail)
                '    End If

                'End If


            Response.Redirect("kb_applist2.aspx")

        End If
    End Sub

    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        StrSql = "update knowdb.dbo.application set docstatus = N'完成', statusid=7 where statusid=6 and docid = '" & Request("docid") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
        Response.Redirect("kb_applist2.aspx")
    End Sub

    Private Sub btn_choose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_choose.Click
        ltlAlert.Text = "var w=window.open('/knowledgebase/kb_docchoose2.aspx?typeid=" & SelectTypeDropDown.SelectedValue & "&docid=" & Request("docid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
    End Sub

    Private Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
        Response.Redirect("kb_applist2.aspx")
    End Sub
End Class

End Namespace
