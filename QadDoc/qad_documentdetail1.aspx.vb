Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration
Imports System.Web.Mail
Imports System.Text


Namespace tcpc

    Partial Class qad_documentdetail1
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strSql As String
        Dim ds As DataSet
        Dim reader As SqlDataReader
        Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
        Public scrollPos As Integer = 0
        Public scrollPosL As Integer = 0
        Dim imgtitle As String

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
                If Not (String.IsNullOrEmpty(Request("docName"))) Then
                    txbname.Text = Request("docName").ToString()
                End If
                BindData(0)
            End If
        End Sub

        Function createSQL(ByVal vol As Integer) As String
            strSql = " Select Distinct d.id, Case " & Session("uRole") & " When 1 Then 0 Else Case d.createdBy - " & Convert.ToInt32(Session("uID")) & " When 0 Then 0 Else Isnull(d.docLevel,3) - da.doc_acc_level End End As docLevel, " _
                   & " hiscnt = Isnull(his.cnt, 0), " _
                   & " Isnull(d.docLevel,3) As Level, cnt = Isnull(dt.cnt, 0), vcount = Isnull(dv.vcount, 0), verifycnt = Isnull(verify.cnt, 0), " _
                   & " Isnull(d.createdBy,0) As creator ,Isnull(d.filepath,'') As path, " _
                   & " isNewMechanism = Isnull(d.isNewMechanism, 0)," _
                   & " d.pictureNo, Case d.isPublic When 1 Then 'Yes' Else 'No' End As isPublic,accFileName = Isnull(d.accFileName, N'')," _
                   & " d.createdname, d.createdDate, d.name,  isnull(d.modifiedby,0) modifiedby, d.modifiedname,d.modifiedDate, Isnull(d.description,'') As description, d.version, Isnull(d.filename,'') As filename, Case d.isApprove When 1 Then 'Yes' Else 'No' End As Approved, Case d.isall When 1 Then 'Yes' Else 'No' End As IsAll,d.Path as virPath" _
                   & " From qaddoc.dbo.documents d " _
                   & " Left Join(	Select typeid, cateid, Name, cnt = Count(*) From qaddoc.dbo.documents_his Group By typeid, cateid, Name ) as his On his.typeid = d.typeid And his.cateid = d.cateid And his.Name = d.Name " _
                   & " Left Join(	Select docid, cnt = Count(*) From qaddoc.dbo.documentitem Group By docid ) as dt On dt.docid = d.id " _
                   & " Left Join(	Select docid, vcount = Count(*) From qaddoc.dbo.documentvend Group By docid ) as dv On dv.docid = d.id " _
                   & " Left Join(	Select docid, typeid, cateid, cnt = Count(*) From qaddoc.dbo.DocumentVerify Where isFailure Is Null Group By docid, typeid, cateid ) As verify On verify.docid = d.id And verify.typeid = d.typeid And verify.cateid = d.cateid " _
                   & " Left Outer Join qaddoc.dbo.documents_his dh On d.typeid = dh.typeid And d.cateid = dh.cateid And dh.id = d.id " _
                   & " Left Outer Join qaddoc.dbo.DocumentAccess da On d.typeID = da.doc_acc_catid "


            If Session("uRole") <> 1 Then
                strSql &= " And da.doc_acc_userid = '" & Session("uID") & "' And da.approvedBy Is Not Null "
            End If
            strSql &= " Where d.typeID='" & Request("typeID") & "' And  d.cateID='" & Request("cateID") & "' And da.approvedBy Is Not Null "
            'If vol = 0 Then 
            If txbname.Text.Trim.Length > 0 Then
                strSql &= " And d.name Like N'%" & txbname.Text.Trim & "%'"
            End If

            If txbdesc.Text.Trim.Length > 0 Then
                strSql &= " And d.description Like N'%" & txbdesc.Text.Trim & "%'"
            End If

            If txtPictureNo.Text.Trim.Length > 0 Then
                strSql &= " And d.pictureNo Like N'%" & txtPictureNo.Text.Trim & "%'"
            End If

            If chk_noApprove.Checked Then
                strSql &= " And Isnull(d.isApprove, 0) = 0 "
            End If
            createSQL = strSql
        End Function

        Sub BindData(ByVal vol As Integer)
            

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL(vol))
            'Response.Write(strSql)

           
            Dim total As Integer
            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("filename", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("filename1", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("version", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("isAppr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("isPublic", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("isall", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("preview", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("oldview", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("assText", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("vendText", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("Level", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("verifycnt", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("creator", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("pictureNo", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("accFileName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("createdname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("createdDate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("modifiedby", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("modifiedname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("modifiedDate", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("id") = .Rows(i).Item("id").ToString().Trim()
                        drow.Item("name") = .Rows(i).Item("name").ToString().Trim()
                        drow.Item("description") = .Rows(i).Item("description").ToString().Trim()
                        drow.Item("pictureNo") = .Rows(i).Item("pictureNo").ToString().Trim()
                        drow.Item("version") = .Rows(i).Item("version")
                        drow.Item("filename") = .Rows(i).Item("filename").ToString().Trim()
                        drow.Item("filename1") = .Rows(i).Item("filename").ToString().Trim()
                        drow.Item("accFileName") = .Rows(i).Item("accFileName").ToString().Trim()
                        Dim path As String = .Rows(i).Item("virPath").ToString().Trim()
                        '如果有附加文件，就追加在后面
                        If .Rows(i).Item("accFileName").ToString().Trim().Length > 0 Then
                            Dim _accFileName As String = .Rows(i).Item("accFileName").ToString().Trim()
                            If String.IsNullOrEmpty(path) Then
                                drow.Item("filename") = drow.Item("filename") & "&nbsp;(<a href='/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/" & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                            Else
                                drow.Item("filename") = drow.Item("filename") & "&nbsp;(<a href='" & path & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                            End If
                        End If
                        drow.Item("isAppr") = .Rows(i).Item("Approved").ToString().Trim()
                        drow.Item("isall") = .Rows(i).Item("IsAll").ToString().Trim()
                        drow.Item("isPublic") = .Rows(i).Item("isPublic").ToString().Trim()

                        If Convert.ToInt32(.Rows(i).Item("docLevel")) >= 0 Then

                            If String.IsNullOrEmpty(path) Then
                                drow.Item("preview") = "<a href='/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/" & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                            Else
                                drow.Item("preview") = "<a href='" & path & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                            End If
                        Else
                            drow.Item("preview") = "&nbsp;"
                        End If
                        If Convert.ToInt32(.Rows(i).Item("hiscnt")) > 0 Then
                            If Convert.ToInt32(.Rows(i).Item("docLevel")) >= 0 Then
                                drow.Item("oldview") = "<a href='/qaddoc/qad_olddocumentlist.aspx?code=" & Server.UrlEncode(.Rows(i).Item("name").ToString().Trim()) _
                                                     & "&typeid=" & Request("typeid") & "&cateid=" & Request("cateid") & "&id=" & .Rows(i).Item("id").ToString().Trim() _
                                                     & "',''docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300' target='_blank'><u>List</u></a>"
                            Else
                                drow.Item("oldview") = "&nbsp;"
                            End If
                        Else
                            drow.Item("oldview") = "&nbsp;"
                        End If
                        drow.Item("assText") = "<u>" & .Rows(i).Item("cnt").ToString.Trim() & "</u>"
                        drow.Item("vendText") = "<u>" & .Rows(i).Item("vcount").ToString.Trim() & "</u>"
                        drow.Item("Level") = .Rows(i).Item("Level").ToString().Trim()
                        drow.Item("verifycnt") = .Rows(i).Item("verifycnt").ToString().Trim()
                        drow.Item("creator") = .Rows(i).Item("creator").ToString().Trim()
                        drow.Item("createdname") = .Rows(i).Item("createdname").ToString().Trim()
                        drow.Item("createdDate") = Format(.Rows(i).Item("createdDate"), )
                        drow.Item("modifiedby") = .Rows(i).Item("modifiedby").ToString().Trim()
                        drow.Item("modifiedname") = .Rows(i).Item("modifiedname").ToString().Trim()
                        drow.Item("modifiedDate") = .Rows(i).Item("modifiedDate").ToString().Trim()
                        dtl.Rows.Add(drow)
                        total = total + 1
                    Next
                End If
            End With
            ds.Reset()
            Label1.Text = "Category: " & Request("typename") & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
            Label1.Text &= "Type: " & Request("catename") & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
            Label1.Text &= "Number of Doc: " & total.ToString()
             
            Dim dvw As DataView
            dvw = New DataView(dtl)

            Dim pageCount As Integer = dvw.Count / datagrid1.PageSize
            If dvw.Count Mod datagrid1.PageSize > 0 Then
                pageCount += 1
            End If


            If datagrid1.CurrentPageIndex >= pageCount Then
                If pageCount = 0 Then
                    datagrid1.CurrentPageIndex = 0
                Else
                    datagrid1.CurrentPageIndex = pageCount - 1
                End If
            End If
            datagrid1.DataSource = dvw
            datagrid1.DataBind()

        End Sub

        Private Sub datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles datagrid1.PageIndexChanged
            datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData(0)
        End Sub

        Private Sub datagrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles datagrid1.ItemCommand
            If e.CommandName.CompareTo("DeleteClick") = 0 Then
                Dim retvalue As Integer
                Dim filepath As String
                strSql = "qaddoc.dbo.qad_documentDel"
                Dim params(4) As SqlParameter
                params(0) = New SqlParameter("@cateID", Request("cateID"))
                params(1) = New SqlParameter("@typeID", Request("typeID"))
                params(2) = New SqlParameter("@docID", e.Item.Cells(0).Text.Trim)
                params(3) = New SqlParameter("@uID", Session("uID"))
                retvalue = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSql, params)
                If (retvalue = 2) Then
                    ltlAlert.Text = "alert('文档正在审批，不可删除');"
                    BindData(0)
                    Return
                ElseIf (retvalue = 3) Then
                    ltlAlert.Text = "alert('文档已通过审批，请联系文档管理员删除');"
                    BindData(0)
                    Return
                ElseIf (retvalue = 1) Then
                    Dim fileName As String = e.Item.Cells(2).Text.Trim()
                    If fileName.Contains("&nbsp;") Then
                        fileName = fileName.Substring(0, fileName.IndexOf("&nbsp;"))
                    End If
                    If File.Exists(Server.MapPath("/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/") & fileName) Then
                        Try
                            File.Delete(Server.MapPath("/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/") & fileName)
                        Catch ex As Exception
                            Throw New Exception("文档：" & ex.ToString())
                        End Try
                    End If
                    '如果有关联文件的话，则删除
                    If (e.Item.Cells(20).Text.Trim().Length > 0 And e.Item.Cells(20).Text.Trim() <> "&nbsp;") Then
                        Try
                            File.Delete(Server.MapPath("/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/") & e.Item.Cells(20).Text.Trim())
                        Catch ex As Exception
                            Throw New Exception("关联文档：" & ex.ToString())
                        End Try
                    End If
                    datagrid1.CurrentPageIndex = 0
                    BindData(0)
                Else
                    ltlAlert.Text = "alert('Delete failure, please delete item links first!');"
                End If

            ElseIf e.CommandName.CompareTo("associated_item") = 0 Then
                If Request("cateID") = 403 Then
                    ltlAlert.Text = "alert('丝印的源文件不应该关联QAD.');"
                    BindData(0)
                    Return
                End If

                ltlAlert.Text = "var w=window.open('/qaddoc/qad_documentitemlist.aspx?id=" & e.Item.Cells(0).Text.Trim() & "','docitem',''); w.focus();"
                BindData(0)
            ElseIf e.CommandName.CompareTo("associated_vend") = 0 Then
                ltlAlert.Text = "var w=window.open('/qaddoc/qad_documentvendlist.aspx?docid=" & e.Item.Cells(0).Text.Trim() & "','docitem',''); w.focus();"
                BindData(0)
            ElseIf e.CommandName.CompareTo("checkedBy") = 0 Then
                strSql = "qaddoc.dbo.sp_InsertDocumentVerify"
                Dim params As SqlParameter = New SqlParameter("@docid", e.Item.Cells(0).Text.Trim())
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSql, params) < 0 Then
                    ltlAlert.Text = "alert('Commit failure!');"
                Else
                    Try
                        Dim mail As MailMessage = New MailMessage

                        Dim mailfrom As String
                        strSql = " Select email From tcpc0.dbo.users Where deleted=0 And isactive=1 And leavedate Is Null And userid=" & Session("uID") & " And email <>''"
                        mailfrom = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

                        Dim mailto As String
                        strSql = " Select Distinct Replace(stuff((Select ';' + Isnull(u.email, '#') From qaddoc.dbo.DocumentAccess da2 " _
                               & " Inner Join tcpc0.dbo.users u On u.userid = da2.doc_acc_userid Where da2.doc_acc_catid = da.doc_acc_catid And da2.doc_acc_level = 0 " _
                               & " For Xml Path('')), 1, 1, ''), '#;', '') " _
                               & " From qaddoc.dbo.DocumentAccess da " _
                               & " Where da.doc_acc_catid = '" & Request("typeID") & "'"

                        mailto = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

                        If mailto <> Nothing Then
                            If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
                                Try
                                    mail.To = mailto
                                    mail.From = IIf(mailfrom = Nothing, ConfigurationManager.AppSettings("AdminEmail"), mailfrom)
                                    mail.Subject = "【通知-Doc】请审批上传的新文档"

                                    Dim strMailBody As New StringBuilder()
                                    strMailBody.Append("<META HTTP-EQUIV='Content-Type' CONTENT='text/html;charset=gb2312'><html><head></head><body>")
                                    strMailBody.Append("<br>你好<br>")
                                    strMailBody.Append(" 在100系统上上传了新的文档，请及时审批，谢谢！<br><br>")
                                    strMailBody.Append(" 文档所在目录Category: " & Request("typename") & " 的分类<br>&nbsp;Type:" & Request("catename") & "下<br>")
                                    strMailBody.Append("<br>上传者：" & Session("uName") & "上传了文档：" & e.Item.Cells(2).Text.Trim() & "<br>")
                                    strMailBody.Append("</body></html>")
                                    mail.Body = Convert.ToString(strMailBody)
                                    mail.BodyFormat = MailFormat.Html
                                    BasePage.SSendEmail(mail.From, mail.To, mail.Cc, mail.Subject, mail.Body)
                                    'SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
                                    'SmtpMail.Send(mail)
                                    ltlAlert.Text = "alert('Commit and send email success!');"
                                Catch ex As Exception
                                    ltlAlert.Text = "alert('Commit success but send email failure!');"
                                End Try
                            End If
                        End If

                        e.Item.Cells(14).Enabled = False
                        e.Item.Cells(14).Text = "&nbsp"
                        BindData(0)
                    Catch
                    End Try
                End If
            ElseIf e.CommandName.CompareTo("Select") = 0 Then



                txbname.Text = e.Item.Cells(1).Text.Trim()
                txbdesc.Text = e.Item.Cells(16).Text.Trim().Replace("&nbsp;", "")
                txbversion.Text = e.Item.Cells(4).Text.Trim()

                'chkAccFileName.Visible = True

                hidOldFileName.Value = e.Item.Cells(19).Text.Trim()
                hidOldAccFileName.Value = e.Item.Cells(20).Text.Trim()

                If e.Item.Cells(6).Text.Trim().ToUpper() = "YES" Then
                    chkall.Checked = True
                Else
                    chkall.Checked = False
                End If

                If e.Item.Cells(7).Text.Trim().ToUpper() = "YES" Then
                    chkIsPublic.Checked = True
                Else
                    chkIsPublic.Checked = False
                End If
                ddlLevel.SelectedIndex = Convert.ToInt32(e.Item.Cells(3).Text.Trim())
                'datagrid1.CurrentPageIndex = 0
                'BindData(1)
            End If
        End Sub

        Private Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
            'If chkAccFileName.Checked Then
            '    ltlAlert.Text = "alert('修改源(关联)文档,请点Modify按钮')"
            '    Return
            'End If

            Dim isall As Integer
            Dim ispublic As Integer
            Dim status As Integer
            Dim filepath As String = ""
            Dim docID As Integer
            Dim fname As String = ""
            Dim accName As String = ""
            Dim docver As Integer
            Dim ret As Integer
            Dim intLastBackslash As Integer
            Dim doclevel As Integer = ddlLevel.SelectedValue

            If Session("uID") = Nothing Then
                ltlAlert.Text = "alert('Session timeout, please relogin!')"
                ' BindData(0)
                Return
            End If
            If chkIsPublic.Checked Then
                ispublic = 1
            Else
                ispublic = 0
            End If

            If chkall.Checked = True Then
                isall = 1
            Else
                isall = 0
            End If
            If (txbname.Text.Trim().Length <= 0) Then
                ltlAlert.Text = "alert('DocName is required')"
                ' BindData(0)
                Return
            End If

            If Not fileAttachFileDoc.HasFile Then
                ltlAlert.Text = "alert('The file is required')"
                ' BindData(0)
                Return
            Else
                If fileAttachFileDoc.FileName.IndexOf("#") > 0 Or fileAttachFileDoc.FileName.IndexOf("+") > 0 Or fileAttachFileDoc.FileName.IndexOf("%") > 0 Then
                    ltlAlert.Text = "alert('The file name can not contain # or + or %')"
                    ' BindData(0)
                    Return
                End If
            End If



            fname = fileAttachFileDoc.FileName
            intLastBackslash = fname.LastIndexOf("\")
            fname = fname.Substring(intLastBackslash + 1)
            If (fname.Trim().Length <= 0) Then
                ltlAlert.Text = "alert('Please choose the file.')"
                'BindData(0)
                Return
            End If

            '文件的后缀名
            Dim fnameSuffix As String

            fnameSuffix = fname.Trim().Substring(fname.Trim().LastIndexOf(".") + 1).ToLower()
            '允许的文件的后缀数组
            Dim SuffixList(25) As String
            SuffixList = {"pdf", "bmp", "png", "jpeg", "jpg", _
            "gif"}


            If (Not (Array.IndexOf(SuffixList, fnameSuffix) >= 0)) Then
                ltlAlert.Text = "alert('The file format can only be pdf or pic format')"
                'BindData(0)
                Return
            End If

            If fileAccFileDoc.HasFile Then
                If fileAccFileDoc.FileName.IndexOf("#") > 0 Or fileAccFileDoc.FileName.IndexOf("+") > 0 Or fileAccFileDoc.FileName.IndexOf("%") > 0 Then
                    ltlAlert.Text = "alert('The file name can not contain # or + or %')"
                    ' BindData(0)
                    Return
                End If
                accName = fileAccFileDoc.FileName
                intLastBackslash = accName.LastIndexOf("\")
                accName = accName.Substring(intLastBackslash + 1)
                If (accName.Trim().Length <= 0) Then
                    ltlAlert.Text = "alert('Please choose the accFile.')"
                    'BindData(0)
                    Return
                End If
            End If

            If accName = fname Then
                ltlAlert.Text = "alert('Doc and Accfile have same name.')"
                'BindData(0)
                Return
            End If

            If datagrid1.SelectedIndex <> -1 Then
                docID = datagrid1.SelectedItem.Cells(0).Text.Trim()
                docver = datagrid1.SelectedItem.Cells(4).Text.Trim() + 1
                Dim strsqltype As String

                strsqltype = "SELECT ISNULL( t.isAppv,0) FROM QadDoc.dbo.Documents d LEFT JOIN QadDoc.dbo.DocumentType t ON d.typeid = t.typeid WHERE d.id = " & docID
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsqltype) = 0 Then
                    Dim params1(2) As SqlParameter
                    params1(0) = New SqlParameter("@docId", docID)
                    params1(1) = New SqlParameter("@lockPart", SqlDbType.VarChar, 500)
                    params1(1).Direction = ParameterDirection.Output
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, "qaddoc.dbo.sp_qad_checkLockULByDoc", params1) = 1 Then
                        ltlAlert.Text = "alert('The associated parts are locked by" & params1(1).Value.ToString() & "，the document can not upgrade！');"
                        Exit Sub
                    End If
                End If
                Dim strsql1 As String
                'strsql1 = "select count(*) from QadDoc.dbo.DocumentItemApprove where docid =" & docID & " and appvResult is null and qad is null"
                strsql1 = "SELECT count(docid) FROM (select docid FROM QadDoc.dbo.DocumentItemApprove where docid =" & docID & "  and appvResult is null and qad is NULL UNION ALL SELECT docid from QadDoc.dbo.DocumentItem_Bak where docid =" & docID & " ) s"
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql1) >= 1 Then
                    ltlAlert.Text = "alert('正在升级审批中，请等待审批结果');"
                    Exit Sub
                End If
            Else
                docID = 0
                If (txbversion.Text.Trim().Length <= 0) Then
                    ltlAlert.Text = "alert('Ver is required')"
                    ' BindData(0)
                    Return
                End If
                If IsNumeric(txbversion.Text) Then

                    docver = Int(Val(txbversion.Text))
                Else

                    ltlAlert.Text = "alert('Ver must be digitals')"
                    'BindData(0)
                    Return
                End If
            End If

            If File.Exists(Server.MapPath("/qaddocitemimport/") & fname) Then
                Try
                    File.Delete(Server.MapPath("/qaddocitemimport") & fname)
                Catch ex As Exception
                    ltlAlert.Text = "alert('Delete the temp file failed！Please try again！')"
                    'ltlAlert.Text = "alert('删除临时文件失败！请刷新后重新操作一次！')"
                    'BindData(0)
                    Return
                End Try
            End If

            Try
                fileAttachFileDoc.MoveTo(Server.MapPath("/qaddocitemimport/") & fname, Brettle.Web.NeatUpload.MoveToOptions.Overwrite)
            Catch ex As Exception
                ltlAlert.Text = "alert('Save the temp file failed！Please try again！')"
                'ltlAlert.Text = "alert('保存临时文件失败！请刷新后重新操作一次！')"
                'BindData(0)
                Return
            End Try

            'add by shanzm 2015-03-31:追加md5，Update by Wanglw 2015-04-03 如果fname为空不追加md5
            Dim _fileMd5 As String
            If (fname <> String.Empty) Then
                _fileMd5 = Me.GetMD5HashFromFile(Server.MapPath("/qaddocitemimport/") & fname)
            End If
            Dim imgdatalen As Integer
            Dim imgtype As String
            Dim size As Decimal

            Dim _accFileMd5 As String
            Dim accImgdatalen As Integer
            Dim accImgtype As String
            Dim accSize As Decimal

            imgdatalen = fileAttachFileDoc.FileContent.Length
            fileAttachFileDoc.FileContent.Close()
            imgtype = fileAttachFileDoc.ContentType
            size = CType(imgdatalen, Decimal) / 1024

            If fileAccFileDoc.HasFile Then
                If File.Exists(Server.MapPath("/qaddocitemimport/") & accName) Then
                    Try
                        File.Delete(Server.MapPath("/qaddocitemimport") & accName)
                    Catch ex As Exception
                        ltlAlert.Text = "alert('Delete the temp file failed！Please try again！')"
                        'ltlAlert.Text = "alert('删除临时文件失败！请刷新后重新操作一次！')"
                        'BindData(0)
                        Return
                    End Try
                End If

                Try
                    fileAccFileDoc.MoveTo(Server.MapPath("/qaddocitemimport/") & accName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite)
                Catch ex As Exception
                    ltlAlert.Text = "alert('Save the temp file failed！Please try again！')"
                    'ltlAlert.Text = "alert('保存临时文件失败！请刷新后重新操作一次！')"
                    'BindData(0)
                    Return
                End Try


                If (accName <> String.Empty) Then
                    _accFileMd5 = Me.GetMD5HashFromFile(Server.MapPath("/qaddocitemimport/") & accName)
                End If


                accImgdatalen = fileAccFileDoc.FileContent.Length
                fileAccFileDoc.FileContent.Close()
                accImgtype = fileAccFileDoc.ContentType
                accSize = CType(accImgdatalen, Decimal) / 1024

            End If

            strSql = "qaddoc.dbo.qad_documentAdd1"
            Dim params(21) As SqlParameter
            params(0) = New SqlParameter("@cateID", Request("cateID"))
            params(1) = New SqlParameter("@typeID", Request("typeID"))
            params(2) = New SqlParameter("@docID", docID)
            params(3) = New SqlParameter("@docname", chk.sqlEncode(txbname.Text.Trim()))
            params(4) = New SqlParameter("@docdesc", chk.sqlEncode(txbdesc.Text.Trim()))
            params(5) = New SqlParameter("@docver", docver)
            params(6) = New SqlParameter("@fname", fname)
            params(7) = New SqlParameter("@docstatus", status)
            params(8) = New SqlParameter("@doclevel", doclevel)
            params(9) = New SqlParameter("@docisall", isall)
            params(10) = New SqlParameter("@imgdata", SqlDbType.Binary) '不需要传值
            params(11) = New SqlParameter("@imgtype", imgtype)
            params(12) = New SqlParameter("@uID", Session("uID"))
            params(13) = New SqlParameter("@picNo", txtPictureNo.Text.Trim())
            params(14) = New SqlParameter("@isPublic", ispublic)
            params(15) = New SqlParameter("@uName", Session("uName"))
            params(16) = New SqlParameter("@size", size)
            params(17) = New SqlParameter("@md5Val", _fileMd5)
            params(18) = New SqlParameter("@accFile", accName)
            params(19) = New SqlParameter("@accFileSize", accSize)
            params(20) = New SqlParameter("@accFileMd5Val", _accFileMd5)

            ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSql, params)

            If ret = -1 Then
                ltlAlert.Text = "alert('The doc filename is existed！Please rename！')"
                'ltlAlert.Text = "alert('所要上传的文件名称FileName已经存在！请重新命名！')"
                'BindData(0)
                Exit Sub
            ElseIf ret = -11 Then
                ltlAlert.Text = "alert('The accFile filename is existed！Please rename！')"
                'ltlAlert.Text = "alert('关联文档所要上传的文件名称FileName已经存在！请重新命名！')"
                'BindData(0)
                Exit Sub
            ElseIf ret = -12 Then
                ltlAlert.Text = "alert('Please upload the accFile！')"
                'ltlAlert.Text = "alert('请上传关联文档！')"
                'BindData(0)
                Exit Sub
            ElseIf ret < -6 Then
                ltlAlert.Text = "alert('The DocName is existed')"
                'ltlAlert.Text = "alert('所要上传的文档名称DocName已经存在')"
                'BindData(0)
                Exit Sub
            ElseIf ret < -1 Then
                ltlAlert.Text = "alert('Failed')"
                'BindData(0)
                Exit Sub
            End If

            Try
                '文档放置位置：/TecDocs/typeID/cateID/
                Dim dirPath As String
                Dim path As String
                dirPath = "/TecDocs/"

                '创建Category文件夹
                dirPath = dirPath & Request("typeID") & "/"
                If Not Directory.Exists(Server.MapPath(dirPath)) Then
                    Directory.CreateDirectory(Server.MapPath(dirPath))
                End If

                '创建Type文件夹
                dirPath = dirPath & Request("cateID") & "/"
                If Not Directory.Exists(Server.MapPath(dirPath)) Then
                    Directory.CreateDirectory(Server.MapPath(dirPath))
                End If

                path = dirPath & fname
                If File.Exists(Server.MapPath(path)) Then
                    File.Delete(Server.MapPath(path))
                End If

                File.Move(Server.MapPath("/qaddocitemimport/") & fname, Server.MapPath(path))
                If fileAccFileDoc.HasFile Then
                    path = dirPath & accName
                    If File.Exists(Server.MapPath(path)) Then
                        File.Delete(Server.MapPath(path))
                    End If
                    File.Move(Server.MapPath("/qaddocitemimport/") & accName, Server.MapPath(path))
                End If

            Catch ex As Exception
                'Throw New Exception("关联文档：" & ex.ToString())
                ltlAlert.Text = "alert('Upload failed！')"
                'ltlAlert.Text = "alert('文档上传失败！应该先删除掉记录后再上传一次！')"
                'BindData(0)
                Exit Sub
            End Try

            chkAccFileName.Visible = False
            txbversion.Text = ""
            txbname.Text = ""
            txbdesc.Text = ""

            chkall.Checked = False
            datagrid1.SelectedIndex = -1
            BindData(0)
        End Sub


        Private Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
            If Request("frm") = Nothing Then
                Response.Redirect(chk.urlRand("/qaddoc/qad_documentlist.aspx?typeID=" & Request("typeID") & "&cateid=" & Request("cateid") _
                    & "&pg=" & Request("pg") & "&rm=" & DateTime.Now()), True)
            Else
                Response.Redirect(chk.urlRand("/qaddoc/doc_verifylist.aspx?typeID=" & Request("typeID") & "&cateid=" & Request("cateid") & "&rm=" & DateTime.Now()), True)
            End If
        End Sub

        Private Sub Btnedit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnedit.Click



            Dim status As Integer = 0
            Dim isall As Integer
            Dim filepath As String = ""
            Dim docID As Integer
            Dim fname As String = ""
            Dim accName As String = ""
            Dim docver As Integer
            Dim ret As Integer
            Dim intLastBackslash As Integer
            Dim imgdatalen As Integer
            Dim imgtype As String
            Dim doclevel As Integer = ddlLevel.SelectedValue
            Dim size As Decimal

            Dim _accFileMd5 As String
            Dim accImgdatalen As Integer
            Dim accImgtype As String
            Dim accSize As Decimal

            If Session("uID") = Nothing Then
                ltlAlert.Text = "alert('Session timeout, please relogin!')"
                'BindData(0)
                Return
            End If

            If chkall.Checked = True Then
                isall = 1
            Else
                isall = 0
            End If
            If (txbname.Text.Trim().Length <= 0) Then
                ltlAlert.Text = "alert('DocName is required')"
                ' BindData(0)
                Return
            End If

            If Not fileAttachFileDoc.HasFile Then
                fname = ""
                intLastBackslash = 0

                imgdatalen = 0
                imgtype = ""

                If chkAccFileName.Checked Then
                    ltlAlert.Text = "alert('勾选 修改关联文档 之后，必须选择一个文件！')"
                    Return
                End If
            Else
                fname = fileAttachFileDoc.FileName
                intLastBackslash = fname.LastIndexOf("\")
                fname = fname.Substring(intLastBackslash + 1)

                imgdatalen = fileAttachFileDoc.FileContent.Length
                fileAttachFileDoc.FileContent.Close()
                imgtype = fileAttachFileDoc.ContentType
                size = CType(imgdatalen, Decimal) / 1024

                If fileAttachFileDoc.FileName.IndexOf("#") > 0 Or fileAttachFileDoc.FileName.IndexOf("+") > 0 Or fileAttachFileDoc.FileName.IndexOf("%") > 0 Then
                    ltlAlert.Text = "alert('The file name can not contain # or +  or %')"
                    'ltlAlert.Text = "alert('文件名不允许包含字符#或者+')"
                    Return
                End If
            End If

            If fileAccFileDoc.HasFile Then
                If fileAccFileDoc.FileName.IndexOf("#") > 0 Or fileAccFileDoc.FileName.IndexOf("+") > 0 Or fileAccFileDoc.FileName.IndexOf("%") > 0 Then
                    ltlAlert.Text = "alert('The file name can not contain # or + or %')"
                    'ltlAlert.Text = "alert('文件名不允许包含字符#或者+')"
                    ' BindData(0)
                    Return
                End If
                accName = fileAccFileDoc.FileName
                intLastBackslash = accName.LastIndexOf("\")
                accName = accName.Substring(intLastBackslash + 1)
                If (accName.Trim().Length <= 0) Then
                    ltlAlert.Text = "alert('Please choose the accFile.')"
                    'BindData(0)
                    Return
                End If
            End If

            If fname.Length > 0 Then
                If File.Exists(Server.MapPath("/qaddocitemimport/") & fname) Then
                    Try
                        File.Delete(Server.MapPath("/qaddocitemimport") & fname)
                    Catch ex As Exception
                        ltlAlert.Text = "alert('Delete the temp file failed！Please try again！')"
                        'ltlAlert.Text = "alert('删除临时文件失败！请刷新后重新操作一次！')"
                        'BindData(0)
                        Return
                    End Try
                End If

                Try
                    If fileAttachFileDoc.HasFile Then
                        fileAttachFileDoc.MoveTo(Server.MapPath("/qaddocitemimport/") & fname, Brettle.Web.NeatUpload.MoveToOptions.Overwrite)
                    End If
                Catch ex As Exception
                    ltlAlert.Text = "alert('Save the temp file failed！Please try again！')"
                    'ltlAlert.Text = "alert('保存临时文件失败！请刷新后重新操作一次！')"
                    'BindData(0)
                    Return
                End Try
            End If

            If fileAccFileDoc.HasFile Then
                If File.Exists(Server.MapPath("/qaddocitemimport/") & accName) Then
                    Try
                        File.Delete(Server.MapPath("/qaddocitemimport") & accName)
                    Catch ex As Exception
                        ltlAlert.Text = "alert('Delete the temp file failed！Please try again！')"
                        'ltlAlert.Text = "alert('删除临时文件失败！请刷新后重新操作一次！')"
                        'BindData(0)
                        Return
                    End Try
                End If

                Try
                    fileAccFileDoc.MoveTo(Server.MapPath("/qaddocitemimport/") & accName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite)
                Catch ex As Exception
                    ltlAlert.Text = "alert('Save the temp file failed！Please try again！')"
                    'ltlAlert.Text = "alert('保存临时文件失败！请刷新后重新操作一次！')"
                    'BindData(0)
                    Return
                End Try


                If (accName <> String.Empty) Then
                    _accFileMd5 = Me.GetMD5HashFromFile(Server.MapPath("/qaddocitemimport/") & accName)
                End If


                accImgdatalen = fileAccFileDoc.FileContent.Length
                fileAccFileDoc.FileContent.Close()
                accImgtype = fileAccFileDoc.ContentType
                accSize = CType(accImgdatalen, Decimal) / 1024

            End If

            If datagrid1.SelectedIndex <> -1 Then
                docID = datagrid1.SelectedItem.Cells(0).Text.Trim()
                docver = datagrid1.SelectedItem.Cells(4).Text.Trim()

                'If chkAccFileName.Checked Then
                'Else
                Dim strsql1 As String
                strsql1 = "select count(*) from QadDoc.dbo.DocumentItem where docid =" & docID & " and qad is not null"

                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql1) >= 1 Then
                    If (fname <> String.Empty Or accName <> String.Empty) Then
                        'ltlAlert.Text = "alert('有关联零件,文档不可修改');"
                        ltlAlert.Text = "alert('The document have associated part,can not modify');"
                        Exit Sub
                    End If

                End If
                strsql1 = "select count(*) from QadDoc.dbo.DocumentItem_Bak where docid =" & docID & " and qad is not null"

                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql1) >= 1 Then
                    If (fname <> String.Empty Or accName <> String.Empty) Then
                        ltlAlert.Text = "alert('The document have associated part,can not modify');"
                        'ltlAlert.Text = "alert('有关联零件,文档不可修改');"
                        Exit Sub
                    End If
                End If

                strsql1 = "select count(*) from QadDoc.dbo.DocumentItemApprove where docid =" & docID & " and appvResult is null and qad is null"

                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql1) >= 1 Then
                    ltlAlert.Text = "alert('正在升级审批中，请等待审批结果');"
                    Exit Sub
                End If
                ' End If

            Else
                ltlAlert.Text = "alert('Please select the document.')"
                'BindData(0)
                Return
            End If


            If (txbversion.Text.Trim().Length <= 0) Then
                ltlAlert.Text = "alert('Ver is required')"
                'BindData(0)
                Exit Sub
            Else
                If Not IsNumeric(txbversion.Text) Then
                    ltlAlert.Text = "alert('Ver must be digitals')"
                    'BindData(0)
                    Exit Sub
                End If
            End If

            If (fname.Trim() <> String.Empty) Then
                If (Not (chkAccFileName.Checked)) Then
                    '文件的后缀名
                    Dim fnameSuffix As String

                    fnameSuffix = fname.Trim().Substring(fname.Trim().LastIndexOf(".") + 1).ToLower()
                    '允许的文件的后缀数组
                    Dim SuffixList(25) As String
                    SuffixList = {"pdf", "bmp", "png", "jpeg", "jpg", _
                    "gif"}


                    If (Not (Array.IndexOf(SuffixList, fnameSuffix) >= 0)) Then
                        ltlAlert.Text = "alert('The file format can only be pdf or pic format')"
                        'ltlAlert.Text = "alert('只能上传PDF和图片格式！')"
                        'BindData(0)
                        Return
                    End If
                End If

            End If

            'add by shanzm 2015-03-31:追加md5  Update by Wanglw 2015-04-03 如果fname为空不追加md5
            Dim _fileMd5 As String
            If (fname <> String.Empty) Then
                _fileMd5 = Me.GetMD5HashFromFile(Server.MapPath("/qaddocitemimport/") & fname)
            End If

            strSql = "qaddoc.dbo.qad_documentMod1"
            Dim params(22) As SqlParameter
            params(0) = New SqlParameter("@cateID", Request("cateID"))
            params(1) = New SqlParameter("@typeID", Request("typeID"))
            params(2) = New SqlParameter("@docID", docID)
            params(3) = New SqlParameter("@docname", chk.sqlEncode(txbname.Text.Trim()))
            params(4) = New SqlParameter("@docdesc", chk.sqlEncode(txbdesc.Text.Trim()))
            params(5) = New SqlParameter("@docver", docver)
            params(6) = New SqlParameter("@fname", fname)
            params(7) = New SqlParameter("@docstatus", status)
            params(8) = New SqlParameter("@doclevel", doclevel)
            params(9) = New SqlParameter("@docisall", isall)
            params(10) = New SqlParameter("@imgdata", SqlDbType.Binary)
            params(11) = New SqlParameter("@imgtype", imgtype)
            params(12) = New SqlParameter("@uID", Session("uID"))
            params(13) = New SqlParameter("@uName", Session("uName"))
            params(14) = New SqlParameter("@picNo", txtPictureNo.Text.Trim())
            params(15) = New SqlParameter("@chkAccFileName", chkAccFileName.Checked)
            params(16) = New SqlParameter("@isPublic", chkIsPublic.Checked)
            params(17) = New SqlParameter("@size", size)
            params(18) = New SqlParameter("@md5Val", _fileMd5)
            params(19) = New SqlParameter("@accFile", accName)
            params(20) = New SqlParameter("@accFileSize", accSize)
            params(21) = New SqlParameter("@accFileMd5Val", _accFileMd5)

            ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSql, params)

            If ret = -1 Then
                ltlAlert.Text = "alert('The doc filename is existed！Please rename！')"
                'ltlAlert.Text = "alert('所要上传的文件的名称已经存在！请重新命名！')"
                'BindData(0)
                Exit Sub
            ElseIf ret = -11 Then
                ltlAlert.Text = "alert('The accFile filename is existed！Please rename！')"
                'ltlAlert.Text = "alert('关联文档所要上传的文件名称FileName已经存在！请重新命名！')"
                'BindData(0)
                Exit Sub
            ElseIf ret < -1 Then
                ltlAlert.Text = "alert('It's failure')"
                ' BindData(0)
                Exit Sub
            End If

            Dim path As String

            path = "/TecDocs/" & Request("typeID") & "/" & Request("cateID") & "/"

            'If fname.Length > 0 Then
            '    Try
            '        '特别是修改的时候，原文件要删除
            '        If chkAccFileName.Checked Then
            '            If File.Exists(Server.MapPath(path) & hidOldAccFileName.Value) Then
            '                File.Move(Server.MapPath(path) & hidOldAccFileName.Value, Server.MapPath("/TecDocs/0/") & "det1-638-" & DateTime.Now.ToFileTime().ToString() & "-" & hidOldAccFileName.Value)
            '            End If
            '        Else
            '            If File.Exists(Server.MapPath(path) & hidOldFileName.Value) Then
            '                File.Move(Server.MapPath(path) & hidOldFileName.Value, Server.MapPath("/TecDocs/0/") & "det1-644-" & DateTime.Now.ToFileTime().ToString() & "-" & hidOldFileName.Value)
            '            End If
            '        End If


            '        '以前的其他文件遗留下来的文档要删除
            '        If File.Exists(Server.MapPath(path) & fname) Then
            '            File.Move(Server.MapPath(path) & fname, Server.MapPath("/TecDocs/0/") & "det1-652-" & DateTime.Now.ToFileTime().ToString() & "-" & fname)
            '        End If

            '        If Not Directory.Exists(Server.MapPath(path)) Then
            '            Directory.CreateDirectory(Server.MapPath(path))
            '        End If

            '        File.Move(Server.MapPath("/qaddocitemimport/") & fname, Server.MapPath(path) & fname)
            '    Catch ex As Exception
            '        ltlAlert.Text = "alert('文档上传失败！应该先删除掉记录后再上传一次！')"
            '        Exit Sub
            '    End Try
            'End If


            Try
                If Not Directory.Exists(Server.MapPath(path)) Then
                    Directory.CreateDirectory(Server.MapPath(path))
                End If
                '特别是修改的时候，原文件要删除
                If accName.Length > 0 Then


                    If File.Exists(Server.MapPath(path) & hidOldAccFileName.Value) Then
                        File.Move(Server.MapPath(path) & hidOldAccFileName.Value, Server.MapPath("/TecDocs/0/") & "det1-638-" & DateTime.Now.ToFileTime().ToString() & "-" & hidOldAccFileName.Value)
                    End If
                    If File.Exists(Server.MapPath(path) & accName) Then
                        File.Move(Server.MapPath(path) & accName, Server.MapPath("/TecDocs/0/") & "det1-652-" & DateTime.Now.ToFileTime().ToString() & "-" & accName)
                    End If
                    File.Move(Server.MapPath("/qaddocitemimport/") & accName, Server.MapPath(path) & accName)
                End If
                If fname.Length > 0 Then
                    If File.Exists(Server.MapPath(path) & hidOldFileName.Value) Then
                        File.Move(Server.MapPath(path) & hidOldFileName.Value, Server.MapPath("/TecDocs/0/") & "det1-644-" & DateTime.Now.ToFileTime().ToString() & "-" & hidOldFileName.Value)
                    End If
                    If File.Exists(Server.MapPath(path) & fname) Then
                        File.Move(Server.MapPath(path) & fname, Server.MapPath("/TecDocs/0/") & "det1-652-" & DateTime.Now.ToFileTime().ToString() & "-" & fname)
                    End If
                    File.Move(Server.MapPath("/qaddocitemimport/") & fname, Server.MapPath(path) & fname)

                End If


                '以前的其他文件遗留下来的文档要删除

            Catch ex As Exception
                ltlAlert.Text = "alert('Upload failed！')"
                'ltlAlert.Text = "alert('文档上传失败！应该先删除掉记录后再上传一次！')"
                Exit Sub
            End Try


            If fileAttachFileDoc.HasFile Then
                Dim returnMessage As String = ""
                If CheckPoConfirm(docID) Then
                    returnMessage += "此文档存在供应商确认的采购单！"
                End If
                If CheckBosConfirm(docID) Then
                    returnMessage += "此文档存在供应商确认的打样单！"
                End If
                If returnMessage <> "" Then
                    ltlAlert.Text = "alert('" + returnMessage + "')"
                End If
            End If

            chkAccFileName.Checked = False
            chkAccFileName.Visible = False
            txbversion.Text = ""
            txbname.Text = ""
            txbdesc.Text = ""
            chkall.Checked = False
            datagrid1.SelectedIndex = -1
            BindData(0)
        End Sub

        Private Function CheckPoConfirm(docId As Integer) As Boolean
            Dim strSql As String = "qaddoc.dbo.sp_checkPoConfirm"
            Dim params(1) As SqlParameter
            params(0) = New SqlParameter("@docId", docId)
            Dim result As Integer = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSql, params)
            If result = 1 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function CheckBosConfirm(docId As Integer) As Boolean
            Dim strSql As String = "qaddoc.dbo.sp_checkBosConfirm"
            Dim params(1) As SqlParameter
            params(0) = New SqlParameter("@docId", docId)
            Dim result As Integer = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSql, params)
            If result = 1 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Sub Butcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Butcancel.Click
            txbname.Text = ""
            txbdesc.Text = ""
            txbversion.Text = ""
            chkall.Checked = False
            chkAccFileName.Visible = False
            datagrid1.SelectedIndex = -1
            ddlLevel.SelectedIndex = 0
            BindData(0)
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            datagrid1.CurrentPageIndex = 0
            datagrid1.SelectedIndex = -1
            BindData(0)
        End Sub

        Protected Sub datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles datagrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    If e.Item.Cells(3).Text.Trim() <> "0" And e.Item.Cells(5).Text.Trim().ToUpper() = "NO" And e.Item.Cells(17).Text.Trim() = "0" And (e.Item.Cells(18).Text.Trim() = Session("uID").ToString().Trim() Or e.Item.Cells(23).Text.Trim() = Session("uID").ToString().Trim()) Then

                    Else
                        e.Item.Cells(14).Enabled = False
                        e.Item.Cells(14).Text = "&nbsp"
                    End If

                Case ListItemType.AlternatingItem
                    If e.Item.Cells(3).Text.Trim() <> "0" And e.Item.Cells(5).Text.Trim().ToUpper() = "NO" And e.Item.Cells(17).Text.Trim() = "0" And (e.Item.Cells(18).Text.Trim() = Session("uID").ToString().Trim() Or e.Item.Cells(23).Text.Trim() = Session("uID").ToString().Trim()) Then

                    Else
                        e.Item.Cells(14).Enabled = False
                        e.Item.Cells(14).Text = "&nbsp"
                    End If

                Case ListItemType.EditItem
                    If e.Item.Cells(3).Text.Trim() <> "0" And e.Item.Cells(5).Text.Trim().ToUpper() = "NO" And e.Item.Cells(17).Text.Trim() = "0" And (e.Item.Cells(18).Text.Trim() = Session("uID").ToString().Trim() Or e.Item.Cells(23).Text.Trim() = Session("uID").ToString().Trim()) Then

                    Else
                        e.Item.Cells(14).Enabled = False
                        e.Item.Cells(14).Text = "&nbsp"
                    End If
            End Select
        End Sub

        Protected Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click
            Dim EXTitle As String = "200^<b>DocName</b>~^300^<b>Description</b>~^80^<b>Ver</b>~^250^<b>FileName</b>~^80^<b>Approved</b>~^180^<b>For All Items</b>~^"
            Dim ExSql As String = createSQL(0)
            Me.ExportExcel(chk.dsnx(), EXTitle, ExSql, False)
        End Sub
    End Class
End Namespace
