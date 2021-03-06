Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

    Partial Class qad_documentsearch
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
                LoadDocSchema()
                LoadDocType()
                If Request("typeID") <> Nothing Then
                    SelectTypeDropDown.SelectedValue = Request("typeID")
                End If
                
                LoadDocCate()

              
                If Request("cateid") <> Nothing Then
                    SelectCateDropDown.SelectedValue = Request("cateid")
                End If
                If Request("docName") <> Nothing Then
                    txb_search.Text = HttpUtility.UrlDecode(Request("docName").ToString())
                End If
                BindData()
            End If
        End Sub

        Sub LoadDocType()
            Dim ls As ListItem
            Dim reader As SqlDataReader
            SelectTypeDropDown.Items.Clear()

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            SelectTypeDropDown.Items.Add(ls)

            strSql = " Select d.typeid, d.typename From qaddoc.dbo.DocumentType d  "

            If SelectTypeDropDown.SelectedValue > 0 Then
                strSql &= " Where d.typeid = '" & SelectTypeDropDown.SelectedValue & "' And d.isDeleted Is Null "
            Else
                strSql &= " Where d.isDeleted Is Null "
            End If
            If SelectSchemaDropDown.SelectedValue > 0 Then
                strSql &= " and Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' "
            End If
            strSql &= " Order By d.typename "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                SelectTypeDropDown.Items.Add(ls)
            End While
            reader.Close()
            SelectTypeDropDown.SelectedValue = 0

        End Sub

        Sub LoadDocSchema()
            Dim ls As ListItem
            Dim reader As SqlDataReader
            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            SelectSchemaDropDown.Items.Add(ls)


            StrSql = " Select Distinct Schemaid,Schemaname From qaddoc.dbo.DocumentSchema Where isDeleted Is Null Order By Schemaid "

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                SelectSchemaDropDown.Items.Add(ls)
            End While
            reader.Close()
        End Sub


        Sub LoadDocCate()
            SelectCateDropDown.Items.Clear()

            Dim ls As ListItem
            Dim reader As SqlDataReader
            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            SelectCateDropDown.Items.Add(ls)

            strSql = " Select d.cateid, d.catename From qaddoc.dbo.DocumentCategory d "

            strSql &= " Where d.catename Is Not Null And d.typeid = '" & SelectTypeDropDown.SelectedValue & "'"
            If SelectCateDropDown.SelectedValue > 0 Then
                strSql &= " And d.cateid = '" & SelectCateDropDown.SelectedValue & "'"
            End If
            strSql &= " Order By d.catename "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                SelectCateDropDown.Items.Add(ls)
            End While
            reader.Close()
            SelectCateDropDown.SelectedValue = 0
        End Sub

        Sub BindData()

            Dim reader_doclevel As SqlDataReader
            Dim accDocLevel As Integer  '用户文档权限访问级
            Dim strSqldocLevel As String
            strSqldocLevel = "select doc_acc_level from qaddoc.dbo.documentaccess where "
            strSqldocLevel &= " doc_acc_catid = '" & SelectTypeDropDown.SelectedValue & "'"
            strSqldocLevel &= " And doc_acc_userid = '" & Session("uID") & "' And approvedBy Is Not Null "
            If (Session("uRole") = 1) Then
                accDocLevel = 0 ' 管理员可查看所有文档
            Else
                reader_doclevel = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSqldocLevel)
                If (reader_doclevel.Read()) Then
                    accDocLevel = Convert.ToInt32(reader_doclevel("doc_acc_level"))
                Else
                    accDocLevel = 6  ' 因文档设了5个级别，且等级1 最高
                End If
                reader_doclevel.Close()
            End If

            strSql = " Select Distinct d.id, Isnull(d.docLevel,3) as docLevel, " _
                   & " Isnull(d.filepath,'') As path, d.typeid, d.cateid, d.typename, d.catename, d.name, Isnull(d.description,'') As description, d.version, " _
                   & "d.filepath, Isnull(d.docLevel,3) As Level, Isnull(d.filename,'') As filename, Case d.isApprove When 1 Then 'Yes' Else 'No' End As Approved, " _
                   & " Case d.isall When 1 Then 'Yes' Else 'No' End As IsAll, isNewMechanism = Isnull(isNewMechanism, 0), d.pictureNo ,d.accFileName," _
                   & " Case d.isPublic When 1 Then 'Yes' Else 'No' End As isPublic, d.Path as virPath," _
                   & " hiscnt = Isnull(his.cnt, 0), " _
                   & " Assocnt = Isnull(dt.cnt, 0)" _
                   & " From qaddoc.dbo.documents d " _
                   & " Left Join(	Select typeid, cateid, Name, cnt = Count(*) From qaddoc.dbo.documents_his  WHERE (typeid =  '" & SelectTypeDropDown.SelectedValue & "' or  '" & SelectTypeDropDown.SelectedValue & "' = '0') And (cateid = '" & SelectCateDropDown.SelectedValue & "' or '" & SelectCateDropDown.SelectedValue & "' = '0')  Group By typeid, cateid, Name ) as his On his.typeid = d.typeid And his.cateid = d.cateid And his.Name = d.Name " _
                   & " Left Join(	Select docid, cnt = Count(*) From qaddoc.dbo.documentitem  WHERE (typeid =  '" & SelectTypeDropDown.SelectedValue & "' or  '" & SelectTypeDropDown.SelectedValue & "' = '0') And (cateid = '" & SelectCateDropDown.SelectedValue & "' or '" & SelectCateDropDown.SelectedValue & "' = '0') Group By docid ) as dt On dt.docid = d.id " _
                   & IIf(Session("uRole") = 1, "Left Outer", "Inner") & " Join qaddoc.dbo.DocumentAccess da On d.typeID = da.doc_acc_catid And da.approvedBy Is Not Null "

            strSql &= " Where d.isApprove = 1 And da.approvedBy Is Not Null "

            strSql &= " And (d.typeid = '" & SelectTypeDropDown.SelectedValue & "' or '" & SelectTypeDropDown.SelectedValue & "' = '0')"

            strSql &= " And (d.cateid = '" & SelectCateDropDown.SelectedValue & "' or '" & SelectCateDropDown.SelectedValue & "' = '0')"

            If txb_search.Text.Trim.Length > 0 Then
                If Request("docName") <> Nothing Then
                    strSql &= " And (d.[name] = N'" & txb_search.Text.Trim & "' Or d.filename = N'" & txb_search.Text.Trim & "')"
                Else
                    strSql &= " And (d.[name] like N'%" & txb_search.Text.Trim & "%' Or d.filename like N'%" & txb_search.Text.Trim & "%')"
                End If

            End If

                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

                Dim dtl As New DataTable
                Dim total As Integer = 0
                dtl.Columns.Add(New DataColumn("docid", System.Type.GetType("System.Int32")))
                dtl.Columns.Add(New DataColumn("typeid", System.Type.GetType("System.Int32")))
                dtl.Columns.Add(New DataColumn("cateid", System.Type.GetType("System.Int32")))
                dtl.Columns.Add(New DataColumn("typename", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("catename", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("filename", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("version", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("isAppr", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("isall", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("preview", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("preview1", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("oldview", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("Level", System.Type.GetType("System.Int32")))
                dtl.Columns.Add(New DataColumn("assText", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("pictureNo", System.Type.GetType("System.String")))
                dtl.Columns.Add(New DataColumn("isPublic", System.Type.GetType("System.String")))

                With ds.Tables(0)
                    Dim drow As DataRow
                    If .Rows.Count > 0 Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            drow = dtl.NewRow()
                            drow.Item("docid") = .Rows(i).Item("id").ToString().Trim()
                            drow.Item("typeid") = .Rows(i).Item("typeid").ToString().Trim()
                            drow.Item("typename") = .Rows(i).Item("typename").ToString().Trim()
                            drow.Item("catename") = .Rows(i).Item("catename").ToString().Trim()
                            drow.Item("cateid") = .Rows(i).Item("cateid").ToString().Trim()
                            drow.Item("name") = .Rows(i).Item("name").ToString().Trim()
                            drow.Item("filename") = .Rows(i).Item("filename").ToString().Trim()
                            drow.Item("description") = .Rows(i).Item("description").ToString().Trim()
                            drow.Item("pictureNo") = .Rows(i).Item("pictureNo").ToString().Trim()
                            drow.Item("version") = .Rows(i).Item("version")
                            drow.Item("isAppr") = .Rows(i).Item("Approved").ToString().Trim()
                            drow.Item("isall") = .Rows(i).Item("IsAll").ToString().Trim()
                            drow.Item("isPublic") = .Rows(i).Item("isPublic").ToString().Trim()

                            '如果有附加文件，就追加在后面 2013-11-13 接上次精神，暂时不放开
                            If .Rows(i).Item("accFileName").ToString().Trim().Length > 0 Then
                                Dim _accFileName As String = .Rows(i).Item("accFileName").ToString().Trim()
                                drow.Item("filename") = drow.Item("filename") & "&nbsp;(<a href='/TecDocs/" & .Rows(i).Item("typeid").ToString().Trim() & "/" & .Rows(i).Item("cateid").ToString().Trim() & "/" & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                            End If

                            '验证查看文档权限 
                            Dim path As String = .Rows(i).Item("virPath").ToString()
                            If Convert.ToInt32(.Rows(i).Item("docLevel")) - accDocLevel >= 0 Or .Rows(i).Item("isPublic").ToString().Trim().ToUpper() = "YES" Then
                                'If Convert.ToBoolean(.Rows(i).Item("isNewMechanism")) Then
                                '    drow.Item("preview") = "<a href='/TecDocs/" & .Rows(i).Item("typeid").ToString().Trim() & "/" & .Rows(i).Item("cateid").ToString().Trim() & "/" & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                                'Else
                                '    drow.Item("preview") = "<a href='/qaddoc/qad_viewdocument.aspx?filepath=" & .Rows(i).Item("path").ToString().Trim() & "&code=" _
                                '                                                         & "document" & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0' target='_blank'><u>Open</u></a>"
                                'End If
                                Dim _viewPath = "http://officewebapps.tcpi.lc/op/view.aspx?src="
                            _viewPath &= Server.UrlEncode(baseDomain.getPortalWebsite())

                                If String.IsNullOrEmpty(path) Then
                                    drow.Item("preview") = "<a href='/TecDocs/" & .Rows(i).Item("typeid").ToString().Trim() & "/" & .Rows(i).Item("cateid").ToString().Trim() & "/" & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                                    If .Rows(i).Item("filename").ToString().Trim().LastIndexOf(".xls") > 0 Or .Rows(i).Item("filename").ToString().Trim().LastIndexOf(".doc") > 0 Or .Rows(i).Item("filename").ToString().Trim().LastIndexOf(".ppt") > 0 Or .Rows(i).Item("filename").ToString().Trim().LastIndexOf(".ppt") > 0 Then
                                        _viewPath &= Server.UrlEncode("/TecDocs/" & .Rows(i).Item("typeid").ToString().Trim() & "/" & .Rows(i).Item("cateid").ToString().Trim() & "/" & .Rows(i).Item("filename").ToString().Trim())
                                        drow.Item("preview1") = "<a href='" & _viewPath & " ' target='_blank'><u>预览</u></a>"
                                    Else
                                        drow.Item("preview1") = ""
                                    End If
                                Else
                                    If .Rows(i).Item("filename").ToString().Trim().LastIndexOf(".xls") > 0 Or .Rows(i).Item("filename").ToString().Trim().LastIndexOf(".doc") > 0 Or .Rows(i).Item("filename").ToString().Trim().LastIndexOf(".ppt") > 0 Or .Rows(i).Item("filename").ToString().Trim().LastIndexOf(".ppt") > 0 Then
                                        _viewPath &= Server.UrlEncode(path & .Rows(i).Item("filename").ToString().Trim())
                                        drow.Item("preview1") = "<a href='" & _viewPath & "' target='_blank'><u>预览</u></a>"
                                    Else
                                        drow.Item("preview1") = ""
                                    End If

                                    drow.Item("preview") = "<a href='" & path & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                                End If

                                If Convert.ToInt32(.Rows(i).Item("hiscnt")) > 0 Then
                                    drow.Item("oldview") = "<a href='/qaddoc/qad_olddocumentlist.aspx?code=" & Server.UrlEncode(.Rows(i).Item("name").ToString().Trim()) _
                                                         & "&typeid=" & .Rows(i).Item("typeid").ToString().Trim() & "&cateid=" & .Rows(i).Item("cateid").ToString().Trim() & "&id=" & .Rows(i).Item("id").ToString().Trim() _
                                                         & "',''docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300' target='_blank'><u>List</u></a>"
                                Else
                                    drow.Item("oldview") = "&nbsp;"
                                End If
                            Else
                                drow.Item("preview") = "&nbsp;"
                                drow.Item("oldview") = "&nbsp;"
                            End If
                            drow.Item("assText") = "<u>" & .Rows(i).Item("Assocnt").ToString.Trim() & "</u>"
                            drow.Item("Level") = .Rows(i).Item("Level").ToString().Trim()
                            total = total + 1
                            dtl.Rows.Add(drow)
                        Next
                    End If
                End With
                ds.Reset()
                ds.Dispose()

                Label1.Text = "Total: " & total.ToString()

                Try
                    DgDoc.DataSource = dtl
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

        Private Sub DgDoc_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgDoc.ItemCommand
            If e.CommandName.CompareTo("associated_item") = 0 Then
                ltlAlert.Text = "var w=window.open('/qaddoc/qad_documentitemlist.aspx?id=" & e.Item.Cells(1).Text.Trim() & "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300'); w.focus();"
            End If
            BindData()
        End Sub

        Private Sub SelectCateDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectCateDropDown.SelectedIndexChanged
            DgDoc.EditItemIndex = -1
            DgDoc.CurrentPageIndex = 0
            BindData()
        End Sub

        Private Sub SelectTypeDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectTypeDropDown.SelectedIndexChanged
            DgDoc.EditItemIndex = -1
            DgDoc.CurrentPageIndex = 0
            LoadDocCate()
            BindData()
        End Sub

        Private Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
            DgDoc.EditItemIndex = -1
            BindData()
            txb_search.Text = ""
        End Sub

        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            ltlAlert.Text = "var w=window.open('/public/SetupDWGTrueView2009_32bit_CHS.exe','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End Sub

        Protected Sub SelectSchemaDropDown_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles SelectSchemaDropDown.SelectedIndexChanged
            DgDoc.EditItemIndex = -1
            DgDoc.CurrentPageIndex = 0
            LoadDocType()
            LoadDocCate()
            BindData()
        End Sub
    End Class

End Namespace
