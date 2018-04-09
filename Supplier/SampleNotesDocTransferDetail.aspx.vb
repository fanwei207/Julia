Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration
Imports System.Web.Mail
Imports System.Text
Imports System.Data
Imports DocTransfer
Imports CommClass

Partial Class Supplier_SampleNotesDocTransferDetail
    Inherits System.Web.UI.Page
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
        If Not IsPostBack Then
            BindDdlType()
            BindDdlCategory()
            BindBosDocInfo()
            BindData(0)
        End If
    End Sub
    Sub BindBosDocInfo()
        If Not (String.IsNullOrEmpty(Request.QueryString("sourceDocID"))) Then
            Dim bf As BfDoc = New BfDoc()
            bf = BfDocHelper.SelectBosDocInfo(Request.QueryString("sourceDocID"), Request.QueryString("source"))
            lbNbr.Text = bf.Nbr
            lbLine.Text = bf.Line.ToString()
            lbCode.Text = bf.Code
            lbQAD.Text = bf.QAD
            lbVend.Text = bf.Vend
            lbVendName.Text = bf.VendName
            If Request.QueryString("source") = "sp" Then
                lbDomain.Text = Request.QueryString("domain")
            End If
            If Request.QueryString("source") = "pj" Then
                lbStepID.Text = Request.QueryString("stepID")
                lbMid.Text = Request.QueryString("mid")
            End If

            If Request.QueryString("status") = "False" Then
                txtDocVer.Text = "1"
                ddlLevel.SelectedIndex = 3
                txtDocName.Text = bf.DocName
                txtFileName.Text = bf.FileName
                lbFileType.Text = bf.FileType
                lbDocDiskName.Text = bf.DocDiskName
                txtDocDesc.Text = bf.FileDesc


            Else
                Dim af As AfDoc = New AfDoc()
                af = AfDocHelper.SelectBosDocTransferInfo(Request.QueryString("sourceDocID"), Request.QueryString("source"))
                ddlType.SelectedIndex = -1
                ddlType.Items.FindByValue(af.TypeID.ToString()).Selected = True
                ddlType.Enabled = False
                BindDdlCategory()
                ddlCategory.SelectedIndex = -1
                ddlCategory.Items.FindByValue(af.CategoryID.ToString()).Selected = True
                ddlCategory.Enabled = False
                ddlLevel.SelectedIndex = -1
                ddlLevel.Items.FindByValue(af.DocLevel.ToString()).Selected = True
                ddlLevel.Enabled = False
                txtDocName.Text = af.DocName
                txtDocName.Enabled = False
                txtDocDesc.Text = af.FileDesc
                txtDocDesc.Enabled = False
                txtFileName.Text = af.FileName
                txtFileName.Enabled = False
                txtDocVer.Text = af.DocVer.ToString()
                txtDocVer.Enabled = False
                chkIsPublic.Checked = af.IsPublic
                chkIsPublic.Enabled = False
                chkIsApprove.Checked = af.IsApprove
                chkIsApprove.Enabled = False
                chkForAllItems.Checked = af.IsAll
                chkForAllItems.Enabled = False
                chkAccFileName.Enabled = False

                If af.Vend.Trim().Length = 0 Then
                    chkVend.Checked = False
                Else
                    chkVend.Checked = True
                End If
                chkVend.Enabled = False
            End If
        End If

    End Sub
    Sub BindDdlType()

        ddlType.DataSource = AfDocHelper.SelectDocType(Session("uRole").ToString(), Session("uID").ToString())
        ddlType.DataBind()
        ddlType.Items.Insert(0, New ListItem("--", "0"))

    End Sub

    Sub BindDdlCategory()

        ddlCategory.DataSource = AfDocHelper.SelectDocCategory(ddlType.SelectedValue)
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("--", "0"))


    End Sub


    Function createSQL(ByVal vol As Integer) As String
        strSql = " Select Distinct d.id, Case " & Session("uRole") & " When 1 Then 0 Else Case d.createdBy - " & Convert.ToInt32(Session("uID")) & " When 0 Then 0 Else Isnull(d.docLevel,3) - da.doc_acc_level End End As docLevel, " _
               & " hiscnt = Isnull(his.cnt, 0), " _
               & " Isnull(d.docLevel,3) As Level, cnt = Isnull(dt.cnt, 0), vcount = Isnull(dv.vcount, 0), verifycnt = Isnull(verify.cnt, 0), " _
               & " Isnull(d.createdBy,0) As creator ,Isnull(d.filepath,'') As path,typename = d.typename, catename = d.catename, " _
               & " isNewMechanism = Isnull(d.isNewMechanism, 0)," _
               & " d.pictureNo, Case d.isPublic When 1 Then 'Yes' Else 'No' End As isPublic,accFileName = Isnull(d.accFileName, N'')," _
               & " d.createdname, d.createdDate, d.name, Isnull(d.description,'') As description, d.version, Isnull(d.filename,'') As filename, Case d.isApprove When 1 Then 'Yes' Else 'No' End As Approved, Case d.isall When 1 Then 'Yes' Else 'No' End As IsAll" _
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
        If lbQAD.Text.Trim() <> "" Then
            strSql &= "Join qaddoc.dbo.DocumentItem di On di.docid = d.id"
            strSql &= " Where da.approvedBy Is Not Null And di.qad = '" & lbQAD.Text.Trim() & "' "
        Else
            strSql &= " Where da.approvedBy Is Not Null And (d.name Like N'%" & txtDocName.Text.Trim & "%' or d.filename Like N'%" & txtDocName.Text.Trim & "%' or d.accFileName Like N'%" & txtDocName.Text.Trim & "%') "
        End If

        'If vol = 0 Then 

        createSQL = strSql
    End Function

    Sub BindData(ByVal vol As Integer)


        ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, createSQL(vol))

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
        dtl.Columns.Add(New DataColumn("typename", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("catename", System.Type.GetType("System.String")))
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

                    '如果有附加文件，就追加在后面
                    If .Rows(i).Item("accFileName").ToString().Trim().Length > 0 Then
                        Dim _accFileName As String = .Rows(i).Item("accFileName").ToString().Trim()
                        drow.Item("filename") = drow.Item("filename") & "&nbsp;(<a href='/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/" & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                    End If
                    drow.Item("isAppr") = .Rows(i).Item("Approved").ToString().Trim()
                    drow.Item("isall") = .Rows(i).Item("IsAll").ToString().Trim()
                    drow.Item("isPublic") = .Rows(i).Item("isPublic").ToString().Trim()
                    If Convert.ToInt32(.Rows(i).Item("docLevel")) >= 0 Then
                        If Convert.ToBoolean(.Rows(i).Item("isNewMechanism")) Then
                            drow.Item("preview") = "<a href='/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/" & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                        Else
                            drow.Item("preview") = "<a href='/qaddoc/qad_viewdocument.aspx?filepath=" & .Rows(i).Item("path").ToString().Trim() & "&code=" _
                                                 & "document" & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0' target='_blank'><u>Open</u></a>"
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
                    drow.Item("typename") = .Rows(i).Item("typename").ToString().Trim()
                    drow.Item("catename") = .Rows(i).Item("catename").ToString().Trim()
                    dtl.Rows.Add(drow)
                    total = total + 1
                Next
            End If
        End With
        ds.Reset()
        Dim dvw As DataView
        dvw = New DataView(dtl)

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

            If (retvalue = 1) Then

                If File.Exists(Server.MapPath("/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/") & e.Item.Cells(18).Text.Trim()) Then
                    Try
                        File.Delete(Server.MapPath("/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/") & e.Item.Cells(18).Text.Trim())
                    Catch ex As Exception
                        Throw New Exception("文档：" & ex.ToString())
                    End Try
                End If
                '如果有关联文件的话，则删除
                If (e.Item.Cells(19).Text.Trim().Length > 0 And e.Item.Cells(19).Text.Trim() <> "&nbsp;") Then
                    Try
                        File.Delete(Server.MapPath("/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/") & e.Item.Cells(19).Text.Trim())
                    Catch ex As Exception
                        Throw New Exception("关联文档：" & ex.ToString())
                    End Try
                End If
            Else
                ltlAlert.Text = "alert('Delete failure, please delete item links first!')"
            End If

            BindData(0)
            datagrid1.CurrentPageIndex = 0
        ElseIf e.CommandName.CompareTo("associated_item") = 0 Then
            'Me.OpenWindow("/qaddoc/qad_documentitemlist.aspx?id=" & e.Item.Cells(0).Text.Trim())
            BindData(0)
        ElseIf e.CommandName.CompareTo("associated_vend") = 0 Then
            'ltlAlert.Text = "var w=window.open('/qaddoc/qad_documentvendlist.aspx?docid=" & e.Item.Cells(0).Text.Trim() & "','docitem',''); w.focus();"
            BindData(0)
        ElseIf e.CommandName.CompareTo("checkedBy") = 0 Then
            strSql = "qaddoc.dbo.sp_InsertDocumentVerify"
            Dim params As SqlParameter = New SqlParameter("@docid", e.Item.Cells(0).Text.Trim())
            'If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSql, params) < 0 Then
            '    ltlAlert.Text = "alert('Commit failure!")
            'Else
            '    Try
            '        Dim mail As MailMessage = New MailMessage

            '        Dim mailfrom As String
            '        strSql = " Select email From tcpc0.dbo.users Where deleted=0 And isactive=1 And leavedate Is Null And userid=" & Session("uID") & " And email <>''"
            '        mailfrom = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

            '        Dim mailto As String
            '        strSql = " Select Distinct Replace(stuff((Select ';' + Isnull(u.email, '#') From qaddoc.dbo.DocumentAccess da2 " _
            '               & " Inner Join tcpc0.dbo.users u On u.userid = da2.doc_acc_userid Where da2.doc_acc_catid = da.doc_acc_catid And da2.doc_acc_level = 0 " _
            '               & " For Xml Path('')), 1, 1, ''), '#;', '') " _
            '               & " From qaddoc.dbo.DocumentAccess da " _
            '               & " Where da.doc_acc_catid = '" & Request("typeID") & "'"

            '        mailto = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

            '        If mailto <> Nothing Then
            '            If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
            '                Try
            '                    mail.To = mailto
            '                    mail.From = IIf(mailfrom = Nothing, "", mailfrom)
            '                    mail.Subject = "【通知-Doc】请审批上传的新文档"

            '                    Dim strMailBody As New StringBuilder()
            '                    strMailBody.Append("<META HTTP-EQUIV='Content-Type' CONTENT='text/html;charset=gb2312'><html><head></head><body>")
            '                    strMailBody.Append("<br>你好<br>")
            '                    strMailBody.Append(" 在100系统上上传了新的文档，请及时审批，谢谢！<br><br>")
            '                    strMailBody.Append(" 文档所在目录Category: " & Request("typename") & " 的分类<br>&nbsp;Type:" & Request("catename") & "下<br>")
            '                    strMailBody.Append("<br>上传者：" & Session("uName") & "上传了文档：" & e.Item.Cells(18).Text.Trim() & "<br>")
            '                    strMailBody.Append("</body></html>")
            '                    mail.Body = Convert.ToString(strMailBody)
            '                    mail.BodyFormat = MailFormat.Html
            '                    SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
            '                    SmtpMail.Send(mail)
            '                    ltlAlert.Text = "alert('Commit and send email success!');"
            '                Catch ex As Exception
            '                    ltlAlert.Text = "alert('Commit success but send email failure!');"
            '                End Try
            '            End If
            '        End If

            '        e.Item.Cells(13).Enabled = False
            '        e.Item.Cells(13).Text = "&nbsp"
            '        BindData(0)
            '    Catch
            '    End Try
            'End If
        ElseIf e.CommandName.CompareTo("Select") = 0 Then
            'txtDocName.Text = e.Item.Cells(1).Text.Trim()
            'txtFileName.Text = e.Item.Cells(2).Text.Trim()
            txtDocDesc.Text = e.Item.Cells(17).Text.Trim().Replace("&nbsp;", "")
            txtDocVer.Text = e.Item.Cells(6).Text.Trim()
            oldFileName.Text = e.Item.Cells(2).Text.Trim()
            txtDocVer.Enabled = False
            'chkAccFileName.Visible = True
            hidOldFileName.Value = e.Item.Cells(21).Text.Trim()
            hidOldAccFileName.Value = e.Item.Cells(22).Text.Trim()
            ddlType.SelectedIndex = -1
            ddlType.Items.FindByText(e.Item.Cells(3).Text.Trim()).Selected = True
            BindDdlCategory()
            ddlCategory.SelectedIndex = -1
            ddlCategory.Items.FindByText(e.Item.Cells(4).Text.Trim()).Selected = True
            ddlType.Enabled = False
            ddlCategory.Enabled = False
            If e.Item.Cells(8).Text.Trim().ToUpper() = "YES" Then
                chkForAllItems.Checked = True
            Else
                chkForAllItems.Checked = False
            End If

            If e.Item.Cells(9).Text.Trim().ToUpper() = "YES" Then
                chkIsPublic.Checked = True
            Else
                chkIsPublic.Checked = False
            End If
            If e.Item.Cells(7).Text.Trim().ToUpper() = "YES" Then
                chkIsApprove.Checked = True
            Else
                chkIsApprove.Checked = False
            End If
            ddlLevel.SelectedIndex = Convert.ToInt32(e.Item.Cells(5).Text.Trim())
            datagrid1.CurrentPageIndex = 0
            'BindData(1)
        End If
    End Sub

    Private Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
        If chkAccFileName.Checked Then
            ltlAlert.Text = "alert('修改源(关联)文档,请点Modify按钮')"
            Exit Sub
        End If
        If String.IsNullOrEmpty(Request.QueryString("sourceDocID")) Then
            ltlAlert.Text = "alert('没有文档需要转移')"
            Exit Sub
        End If
        If ddlType.SelectedValue = "0" Then
            ltlAlert.Text = "alert('请选择Type')"
            Exit Sub
        End If
        If ddlCategory.SelectedValue = "0" Then
            ltlAlert.Text = "alert('请选择Category')"
            Exit Sub
        End If
        Dim docver As Integer
        Dim docID As Integer
        If Session("uID") = Nothing Then
            ltlAlert.Text = "alert('Session timeout, please relogin!')"
            BindData(0)
            Exit Sub
        End If
        If (txtDocName.Text.Trim().Length <= 0) Then
            ltlAlert.Text = "alert('DocName is required')"
            BindData(0)
            Exit Sub
        End If

        If txtFileName.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('The file is required')"
            BindData(0)
            Exit Sub
        Else
            If txtFileName.Text.Trim().IndexOf("#") > 0 Then
                ltlAlert.Text = "alert('文件名不允许包含字符#')"
                BindData(0)
                Exit Sub
            End If
        End If

        Dim oldPath As String
        If Request.QueryString("source") = "pj" Then
            oldPath = "/TecDocs/ProjectTracking/" + lbStepID.Text.Trim() + "/"
        Else
            oldPath = "/TecDocs/Supplier/" + lbVend.Text.Trim() + "/"
        End If

        Dim newPath As String = "/TecDocs/" + ddlType.SelectedValue + "/" + ddlCategory.SelectedValue + "/"
        If Not File.Exists(Server.MapPath(oldPath + lbDocDiskName.Text.Trim())) Then
            ltlAlert.Text = "alert('源文件不存在')"
            Exit Sub
        Else
            If Not Directory.Exists(Server.MapPath(newPath)) Then
                Try
                    Directory.CreateDirectory(Server.MapPath(newPath))
                Catch ex As Exception
                    ltlAlert.Text = "alert('创建目录失败')"
                    Exit Sub
                End Try
            End If
        End If


        '如果选择了gridview里面的文档记录，则表示对这个文档进行升级’
        If datagrid1.SelectedIndex <> -1 Then
            docID = datagrid1.SelectedItem.Cells(0).Text.Trim()
            docver = datagrid1.SelectedItem.Cells(6).Text.Trim() + 1
            '如果没有选择gridview里面的记录，则表示新增文档'
        Else
            docID = 0
            docver = txtDocVer.Text.Trim()
        End If

        Dim ret As Integer

        strSql = "sp_bos_documentAdd"
        Dim params(23) As SqlParameter
        params(0) = New SqlParameter("@cateID", ddlCategory.SelectedValue)
        params(1) = New SqlParameter("@typeID", ddlType.SelectedValue)
        params(2) = New SqlParameter("@docID", docID)
        params(3) = New SqlParameter("@docname", txtDocName.Text.Trim())
        params(4) = New SqlParameter("@docdesc", txtDocDesc.Text.Trim())
        params(5) = New SqlParameter("@docver", docver)
        params(6) = New SqlParameter("@fname", txtFileName.Text.Trim())
        params(7) = New SqlParameter("@docstatus", 0)
        params(8) = New SqlParameter("@doclevel", ddlLevel.SelectedValue)
        params(9) = New SqlParameter("@docisall", chkForAllItems.Checked)
        params(10) = New SqlParameter("@imgdata", SqlDbType.Binary) '不需要传值
        params(11) = New SqlParameter("@imgtype", lbFileType.Text.Trim())
        params(12) = New SqlParameter("@uID", Session("uID"))
        params(13) = New SqlParameter("@uName", Session("uName"))
        params(14) = New SqlParameter("@picNo", "")
        params(15) = New SqlParameter("@isPublic", chkIsPublic.Checked)
        params(16) = New SqlParameter("@isApprove", chkIsApprove.Checked)
        params(17) = New SqlParameter("@isVend", chkVend.Checked)
        params(18) = New SqlParameter("@sourceDocID", Request.QueryString("sourceDocID"))
        params(19) = New SqlParameter("@source", Request.QueryString("source"))
        params(20) = New SqlParameter("@vend", lbVend.Text.Trim())
        params(21) = New SqlParameter("@vendName", lbVendName.Text.Trim())
        params(22) = New SqlParameter("@qad", lbQAD.Text.Trim())
        params(23) = New SqlParameter("@path", "/TecDocs/" + ddlType.SelectedValue + "/" + ddlCategory.SelectedValue + "/")

        ret = SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.TCPC_Supplier"), CommandType.StoredProcedure, strSql, params)
        If ret < 0 Then
            If ret = -1 Then
                ltlAlert.Text = "alert('所要上传的文件名称FileName已经存在！请重新命名！')"
                txtFileName.ReadOnly = False
                BindData(0)
                Return
            ElseIf ret = -2 Then
                ltlAlert.Text = "alert('所要上传的文档名称DocName已经存在')"
                BindData(0)
                Return
            Else
                ltlAlert.Text = "alert('上传失败')"
                BindData(0)
                Return
            End If

        Else
            '新增和升级都保留原文档'
            Try
                File.Copy(Path.Combine(Server.MapPath(oldPath), lbDocDiskName.Text.Trim()), Path.Combine(Server.MapPath(newPath), txtFileName.Text.Trim()), False)
                ltlAlert.Text = "alert('转移成功')"
            Catch ex As Exception
                ltlAlert.Text = "alert('复制文件失败')"
                BindData(0)
                Exit Sub
            End Try
        End If

        datagrid1.SelectedIndex = -1
        BindData(0)
    End Sub

    Private Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        If Request("source") = "sp" Then
            Response.Redirect("supplier_docsImport.aspx?po=" + lbNbr.Text.Trim() + "&line=" + lbLine.Text.Trim() + "&do=" + lbDomain.Text.Trim() + "&tp=1" + "&rm=" + DateTime.Now.ToString(), True)
        End If
        If Request("source") = "bos" Then
            Response.Redirect("SampleNotesAccDoc.aspx?Mode=Maintain&strNbr=" + lbNbr.Text.Trim() + "&line=" + lbLine.Text.Trim() + "&code=" + Server.UrlEncode(lbCode.Text.Trim()).ToString() + "&qad=" + lbQAD.Text.Trim())
        End If
        If Request("source") = "pj" Then
            Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + lbMid.Text.Trim() + "&did=" + lbStepID.Text.Trim())
        End If



    End Sub

    Private Sub Btnedit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnedit.Click
        Dim docID As Integer
        Dim ret As Integer


        If Session("uID") = Nothing Then
            'ltlAlert.Text = "alert('Session timeout, please relogin!')"
            BindData(0)
            Return
        End If
        If (txtDocName.Text.Trim().Length <= 0) Then
            'ltlAlert.Text = "alert('DocName is required')"
            BindData(0)
            Return
        End If
        If txtFileName.Text.Trim().IndexOf("#") > 0 Then
            ltlAlert.Text = "alert('文件名不允许包含字符#')"
            Return
        End If

        If datagrid1.SelectedIndex <> -1 Then
            docID = datagrid1.SelectedItem.Cells(0).Text.Trim()
        Else
            ltlAlert.Text = "alert('Please select the document')"
            BindData(0)
            Return
        End If
        If (txtDocVer.Text.Trim().Length <= 0) Then
            ltlAlert.Text = "alert('Ver is required')"
            BindData(0)
            Exit Sub
        Else
            If Not IsNumeric(txtDocVer.Text) Then
                ltlAlert.Text = "alert('Ver must be digitals')"
                BindData(0)
                Exit Sub
            End If
        End If
        Dim oldPath As String
        If Request.QueryString("source") = "pj" Then
            oldPath = "/TecDocs/ProjectTracking/" + lbStepID.Text.Trim() + "/"
        Else
            oldPath = "/TecDocs/Supplier/" + lbVend.Text.Trim() + "/"
        End If
        Dim newPath As String = "/TecDocs/" + ddlType.SelectedValue + "/" + ddlCategory.SelectedValue + "/"
        If Not File.Exists(Server.MapPath(oldPath + lbDocDiskName.Text.Trim())) Then
            ltlAlert.Text = "alert('源文件不存在')"
            Exit Sub
        End If

        strSql = "sp_bos_documentMod"
        Dim params(19) As SqlParameter
        params(0) = New SqlParameter("@cateID", ddlCategory.SelectedValue)
        params(1) = New SqlParameter("@typeID", ddlType.SelectedValue)
        params(2) = New SqlParameter("@docID", docID)
        params(3) = New SqlParameter("@docname", txtDocName.Text.Trim())
        params(4) = New SqlParameter("@docdesc", txtDocDesc.Text.Trim())
        params(5) = New SqlParameter("@docver", txtDocVer.Text.Trim())
        params(6) = New SqlParameter("@fname", txtFileName.Text.Trim())
        params(7) = New SqlParameter("@doclevel", ddlLevel.SelectedValue)
        params(8) = New SqlParameter("@imgtype", lbFileType.Text.Trim())
        params(9) = New SqlParameter("@uID", Session("uID"))
        params(10) = New SqlParameter("@uName", Session("uName"))
        params(11) = New SqlParameter("@isPublic", chkIsPublic.Checked)
        params(12) = New SqlParameter("@isApprove", chkIsApprove.Checked)
        params(13) = New SqlParameter("@isVend", chkVend.Checked)
        params(14) = New SqlParameter("@vend", lbVend.Text.Trim())
        params(15) = New SqlParameter("@vendName", lbVendName.Text.Trim())
        params(16) = New SqlParameter("@chkAccFileName", chkAccFileName.Checked)
        params(17) = New SqlParameter("@docisall", chkForAllItems.Checked)
        params(18) = New SqlParameter("@sourceDocID", Request.QueryString("sourceDocID"))
        params(19) = New SqlParameter("@source", Request.QueryString("source"))
        ret = SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.TCPC_Supplier"), CommandType.StoredProcedure, strSql, params)

        If ret < 0 Then
            If ret = -1 Then
                ltlAlert.Text = "alert('所要上传的文件名称FileName已经存在！请重新命名！')"
                txtFileName.ReadOnly = False
                BindData(0)
                Return
            ElseIf ret = -2 Then
                ltlAlert.Text = "alert('所要上传的文档名称DocName已经存在')"
                BindData(0)
                Return
            Else
                ltlAlert.Text = "alert('修改文档失败')"
                BindData(0)
                Return
            End If

        Else
            'copy文件’
            Try
                File.Move(Path.Combine(Server.MapPath(oldPath), lbDocDiskName.Text.Trim()), Path.Combine(Server.MapPath(newPath), txtFileName.Text.Trim()))
            Catch ex As Exception
                ltlAlert.Text = "alert('复制文件失败')"
                Exit Sub
            End Try
            '删除原有的文件’
            Try
                File.Delete(Server.MapPath(newPath + oldFileName.Text.Trim()))
            Catch ex As Exception
                ltlAlert.Text = "alert('删除原有文件失败！')"
                BindData(0)
                Exit Sub
            End Try

        End If
        datagrid1.SelectedIndex = -1
        BindData(0)
    End Sub

    Private Sub Butcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Butcancel.Click
        txtDocName.Text = ""
        txtDocDesc.Text = ""
        txtDocVer.Text = ""
        datagrid1.SelectedIndex = -1
        ddlLevel.SelectedIndex = 0
        BindData(0)
    End Sub

    Protected Sub datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles datagrid1.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item
                If e.Item.Cells(3).Text.Trim() <> "0" And e.Item.Cells(5).Text.Trim().ToUpper() = "NO" And e.Item.Cells(17).Text.Trim() = "0" And e.Item.Cells(18).Text.Trim() = Session("uID").ToString().Trim() Then

                Else
                    e.Item.Cells(14).Enabled = False
                    e.Item.Cells(14).Text = "&nbsp"
                End If

            Case ListItemType.AlternatingItem
                If e.Item.Cells(3).Text.Trim() <> "0" And e.Item.Cells(5).Text.Trim().ToUpper() = "NO" And e.Item.Cells(17).Text.Trim() = "0" And e.Item.Cells(18).Text.Trim() = Session("uID").ToString().Trim() Then

                Else
                    e.Item.Cells(14).Enabled = False
                    e.Item.Cells(14).Text = "&nbsp"
                End If

            Case ListItemType.EditItem
                If e.Item.Cells(3).Text.Trim() <> "0" And e.Item.Cells(5).Text.Trim().ToUpper() = "NO" And e.Item.Cells(17).Text.Trim() = "0" And e.Item.Cells(18).Text.Trim() = Session("uID").ToString().Trim() Then

                Else
                    e.Item.Cells(14).Enabled = False
                    e.Item.Cells(14).Text = "&nbsp"
                End If
        End Select
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        BindDdlCategory()
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        strSql = "sp_bos_checkDocumentCateLinkVend"
        Dim params(2) As SqlParameter
        params(0) = New SqlParameter("@cateID", ddlCategory.SelectedValue)
        params(1) = New SqlParameter("@typeID", ddlType.SelectedValue)
        Dim linkVend As Boolean = Convert.ToBoolean(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.TCPC_Supplier"), CommandType.StoredProcedure, strSql, params))
        If linkVend = True And lbVend.Text <> "" Then
            chkVend.Checked = True
            chkVend.Enabled = False
        End If
    End Sub
End Class


