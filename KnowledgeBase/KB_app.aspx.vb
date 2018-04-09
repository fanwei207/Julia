Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Web.Mail


Namespace tcpc


Partial Class KB_app
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
            BindSug()

            If Request("docid") <> "" Then
                Dim reader1 As SqlDataReader
                StrSql = "select docuser,doccontent from knowdb.dbo.application where docid='" & CInt(Request("docid")) & "'"
                reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                With reader1.Read()
                    lbl_dispcontent.Text = reader1("doccontent")
                    lbl_username.Text = "申请人:" + CStr(reader1("docuser"))
                End With
                SelectTypeDropDown.SelectedValue = CInt(Request("typeid"))
                SelectTypeDropDown.Enabled = False
                lbl_appcontent.Visible = False
                txb_appcontent.Visible = False
            Else
                txb_sug.Visible = False
                lbl_sug.Visible = False
            End If

            If Request("typeid") <> "" Then
                SelectTypeDropDown.SelectedValue = CInt(Request("typeid"))
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
                        drow.Item("attdate") = Format(.Rows(i).Item("attdate"), "yyyy-MM-dd")
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
        StrSql = "select sugid,docid,sugcontent,suguser,sugdate from knowdb.dbo.Suggestion where docid = '" & Request("docid") & "' "
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
                    drow.Item("sugid") = .Rows(i).Item("sugid").ToString().Trim()
                    drow.Item("docid") = .Rows(i).Item("docid").ToString().Trim()
                    drow.Item("sugcontent") = .Rows(i).Item("suguser").ToString().Trim() + "签于" + .Rows(i).Item("sugdate").ToString() + "--" + .Rows(i).Item("sugcontent").ToString().Trim()
                    drow.Item("suguser") = .Rows(i).Item("suguser").ToString().Trim()
                    If IsDBNull(.Rows(i).Item("sugdate")) = False Then
                        drow.Item("sugdate") = Format(.Rows(i).Item("sugdate"), "yyyy-MM-dd")
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

        If Request("docstatus") = "CLOSE" Then
            ltlAlert.Text = "alert('此项申请已经关闭，不能编辑！')"
            Exit Sub
        End If

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
        Dim i As Integer
        Dim mailto As String
        mailfrom = ""
        mailto = ""

        If Request("docstatus") = "CLOSE" Then
            ltlAlert.Text = "alert('此项申请已经关闭，不能提交！')"
            Exit Sub
        End If

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

            If txb_chooseid.Text.Length <= 0 Or txb_choose.Text.Length <= 0 Then
                ltlAlert.Text = "alert('请选择提交人！')"
                Exit Sub
            End If

        Else
            StrSql = "select docid from knowdb.dbo.application where docid = '" & Request("docid") & "' and docstatus is null "
            If IsDBNull(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)) Or SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) = Nothing Then
                ltlAlert.Text = "alert('此申请已经结束！')"
                Exit Sub
            End If

            If txb_sug.Text.Trim.Length > 1000 Then
                ltlAlert.Text = "alert('会签内容不能超过1000个字节！')"
                Exit Sub
            End If

            isnew = False
            docid = CInt(Request("docid"))

            If txb_chooseid.Text.Length <= 0 Or txb_choose.Text.Length <= 0 Then
                ltlAlert.Text = "alert('请选择提交人！')"
                Exit Sub
            End If

        End If




        StrSql = "knowdb.dbo.submit"
        Dim params(9) As SqlParameter
        params(0) = New SqlParameter("@typeid", SelectTypeDropDown.SelectedItem.Value)
        params(1) = New SqlParameter("@typename", SelectTypeDropDown.SelectedItem.Text)
        params(2) = New SqlParameter("@appcontent", txb_appcontent.Text.Trim())
        params(3) = New SqlParameter("@sugcontent", txb_sug.Text.Trim())
        params(4) = New SqlParameter("@uID", Session("uID"))
        params(5) = New SqlParameter("@username", Session("uName"))
        params(6) = New SqlParameter("@docid", docid)
        params(7) = New SqlParameter("@isnew", isnew)
        params(8) = New SqlParameter("@choose", "," + txb_chooseid.Text.Trim())
        params(9) = New SqlParameter("@chooseuser", txb_choose.Text.Trim())

        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)
        If ret < 0 Then
            ltlAlert.Text = "alert('上传失败')"
            Exit Sub
        Else

            StrSql = "SELECT email From tcpc0.dbo.users Where  plantCode='" & Session("plantcode") & "' and deleted=0 and isactive=1 and leavedate is null and userid=" & Session("uID") & " Order by UserName "
            If IsDBNull(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)) Then
                    mailfrom = ConfigurationManager.AppSettings("AdminEmail").ToString()
            Else
                mailfrom = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
            End If

            Dim mail As MailMessage = New MailMessage


            Dim mailstr() As String = Split(txb_chooseid.Text.Trim, ",")

            For i = 0 To mailstr.Length - 2
                StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & CInt(mailstr(i).Trim()) & ""
                If IsDBNull(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)) Or SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Nothing Then

                    mailto = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

                        If mailto.Trim.Length > 0 And baseDomain.checkDomainOR(mailto) Then
                            mail.To = mailto
                            mail.From = mailfrom
                            mail.Subject = SelectTypeDropDown.SelectedItem.Text

                            If Request("docid") <> "" Then
                                mail.Body = lbl_dispcontent.Text
                            Else
                                mail.Body = txb_appcontent.Text
                            End If
                            BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                            'SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                            'SmtpMail.Send(mail)
                        End If

                End If

            Next

            Response.Redirect("../knowledgebase/kb_applist.aspx")

        End If


    End Sub

    Private Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        If Request("docstatus") = "CLOSE" Then
            ltlAlert.Text = "alert('此项申请已经关闭！')"
            Exit Sub
        End If

        StrSql = "update knowdb.dbo.application set docstatus = 'CLOSE' where docid = '" & Request("docid") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
        Response.Redirect("../knowledgebase/kb_applist.aspx")
    End Sub

    Private Sub btn_choose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_choose.Click
        If Request("docstatus") = "CLOSE" Then
            ltlAlert.Text = "alert('此项申请已经关闭，不能编辑！')"
            Exit Sub
        End If
        ltlAlert.Text = "var w=window.open('/knowledgebase/kb_docchoose.aspx?typeid=" & SelectTypeDropDown.SelectedValue & "&docid=" & Request("docid") & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
    End Sub

    Private Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
        Response.Redirect("../knowledgebase/kb_applist.aspx")
    End Sub
End Class

End Namespace
