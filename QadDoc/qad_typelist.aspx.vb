Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc

Namespace tcpc
    Partial Class qad_typelist
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
            'Security.Register("100103268", "Category/Dir Certificate")
        End Sub

#End Region
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then
                btn_del.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this category?');")
                LoadDocSchema()
                If Request("schemaid") <> Nothing Then
                    SelectSchemaDropDown.SelectedValue = Request("schemaid")
                End If

                BindData(0)
            End If
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


        Sub BindData(ByVal vol As Integer)
            StrSql = " SELECT typeid, createdBy, typename, createdName, createdDate,isCertificated , isAppv = isnull(isAppv,0)" _
                   & " From qaddoc.dbo.DocumentType where typeid>0 and isDeleted is null "
            If vol = 0 Then
                If txb_cate.Text.Trim.Length > 0 Then
                    StrSql &= " and typename like N'" & txb_cate.Text.Replace("*", "%") & "' "
                End If
                If txb_owner.Text.Trim.Length > 0 Then
                    StrSql &= " and createdName like N'" & txb_owner.Text.Replace("*", "%") & "' "
                End If

            End If
            If SelectSchemaDropDown.SelectedValue > 0 Then

                StrSql &= " and Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' "
            End If
            StrSql &= " Order by typeid"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("g_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_cate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_certificated", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_Appv", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("created_by", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("created_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_cate_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_user_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_user", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("g_no") = i + 1
                        drow.Item("g_cate") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("created_by") = "<u>" & .Rows(i).Item(3).ToString().Trim() & "</u>"
                        If Not IsDBNull(.Rows(i).Item(4)) Then
                            drow.Item("created_date") = Format(.Rows(i).Item(4), "yyyy-MM-dd")
                        End If
                        drow.Item("g_cate_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("g_user_id") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("g_user") = .Rows(i).Item(3).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(5)) And (.Rows(i).Item(5).ToString().Trim() = "True") Then
                            drow.Item("g_certificated") = "认证"
                        Else
                            drow.Item("g_certificated") = "非认证"
                        End If
                        If Not IsDBNull(.Rows(i).Item(6)) And (.Rows(i).Item(6).ToString().Trim() = "True") Then
                            drow.Item("g_Appv") = "审批"
                        Else
                            drow.Item("g_Appv") = "不审批"
                        End If
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

        Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
            If txb_cate.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('Category name is required.')"
                Exit Sub
            End If

            If lbl_id.Text.Trim.Length > 0 Then
                '修改
                If Session("uRole") > 1 And lbl_uid.Text <> Convert.ToString(Session("uID")) Then
                    ltlAlert.Text = "alert('Action is denied.')"
                    Exit Sub
                End If

                StrSql = "Select typeid from qaddoc.dbo.DocumentType where typename=N'" & txb_cate.Text & "' and typeid<>'" & lbl_id.Text & "' "
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                    ltlAlert.Text = "alert('Category has alread been exist')"
                    Exit Sub
                End If

                StrSql = "update qaddoc.dbo.DocumentType set typename = N'" & txb_cate.Text & "' where typeid=" & lbl_id.Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                StrSql = "update qaddoc.dbo.DocumentCategory set typename = N'" & txb_cate.Text & "' where typeid=" & lbl_id.Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                StrSql = "update qaddoc.dbo.Documentitem set typename = N'" & txb_cate.Text & "' where typeid=" & lbl_id.Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                StrSql = "update qaddoc.dbo.Documents set typename = N'" & txb_cate.Text & "' where typeid=" & lbl_id.Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                StrSql = "update qaddoc.dbo.Documents_his set typename = N'" & txb_cate.Text & "' where typeid=" & lbl_id.Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                StrSql = "update qaddoc.dbo.documentAccess set doc_acc_catname = N'" & txb_cate.Text & "' where doc_acc_catid=" & lbl_id.Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            Else
                '增加
                Dim id As Integer = 0
                StrSql = "Insert Into qaddoc.dbo.DocumentType(typename,createdBy,createdName,createdDate,isCertificated, Schemaid) Values(N'" & txb_cate.Text & "','" & Session("uID") & "',N'" & Session("uName") & "',getdate(),1, '" & SelectSchemaDropDown.SelectedValue & "')  Select @@IDENTITY"
                id = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
                If id > 0 Then
                    StrSql = "Insert Into qaddoc.dbo.documentAccess(doc_acc_userid,doc_acc_username,doc_acc_catid,doc_acc_catname,createdBy,createdDate,approvedBy,approvedDate,doc_acc_level) "
                    StrSql &= " Values('" & Session("uID") & "',N'" & Session("uName") & "','" & id & "',N'" & txb_cate.Text.Trim & "','" & Session("uID") & "',getdate(),'" & Session("uID") & "',getdate(),0)"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                End If

            End If

            lbl_id.Text = ""
            lbl_uid.Text = ""

            txb_cate.Text = ""

            btn_add.Text = "Add"
            btn_cancel.Enabled = False
            btn_del.Enabled = False

            BindData(0)
        End Sub

        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            Datagrid1.CurrentPageIndex = 0
            BindData(0)
        End Sub

        Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
            lbl_id.Text = ""
            lbl_uid.Text = ""

            txb_cate.Text = ""

            btn_add.Text = "Add"
            btn_cancel.Enabled = False
            btn_del.Enabled = False

            BindData(0)
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData(0)
        End Sub

        Protected Sub btn_del_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_del.Click
            If Session("uRole") > 1 And lbl_uid.Text <> Convert.ToString(Session("uID")) Then
                ltlAlert.Text = "alert('Action is denied.')"
                Exit Sub
            End If

            Dim strSQL As String
            strSQL = "select count(*) from qaddoc.dbo.DocumentCategory where typeID='" & lbl_id.Text & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
                ltlAlert.Text = "alert('You are not able to delete this category if there are some directories associating with the category.')"
                Exit Sub
            Else
                'strSQL = "delete from qaddoc.dbo.DocumentType where typeid = " & lbl_id.Text
                'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                strSQL = "Update qaddoc.dbo.DocumentType set isDeleted=" & lbl_id.Text & " where typeid = " & lbl_id.Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                strSQL = "delete from qaddoc.dbo.documentAccess where doc_acc_catid = " & lbl_id.Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            End If

            lbl_id.Text = ""
            lbl_uid.Text = ""
            txb_cate.Text = ""

            btn_add.Text = "Add"
            btn_cancel.Enabled = False
            btn_del.Enabled = False

            Datagrid1.CurrentPageIndex = 0
            BindData(0)
        End Sub

        Protected Sub btn_help_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_help.Click
            ltlAlert.Text = "var w=window.open('/docs/doc_help.doc','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
        End Sub

        Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    If Session("uID").ToString().Trim() <> e.Item.Cells(12).Text.Trim() Then

                        e.Item.Cells(4).Enabled = False
                        e.Item.Cells(4).Text = "&nbsp;"
                        e.Item.Cells(5).Enabled = False
                        e.Item.Cells(5).Text = "&nbsp;"
                        e.Item.Cells(6).Enabled = False
                        e.Item.Cells(6).Text = "&nbsp;"
                    End If
                    '100103268
                    If Security("100103268").isValid = False Then
                        CType(e.Item.Cells(2).FindControl("lbt_certificate"), LinkButton).Enabled = False
                    End If

                Case ListItemType.AlternatingItem
                    If Session("uID").ToString().Trim() <> e.Item.Cells(12).Text.Trim() Then

                        e.Item.Cells(4).Enabled = False
                        e.Item.Cells(4).Text = "&nbsp;"
                        e.Item.Cells(5).Enabled = False
                        e.Item.Cells(5).Text = "&nbsp;"
                        e.Item.Cells(6).Enabled = False
                        e.Item.Cells(6).Text = "&nbsp;"
                    End If
                    If Security("100103268").isValid = False Then
                        CType(e.Item.Cells(2).FindControl("lbt_certificate"), LinkButton).Enabled = False
                    End If

                Case ListItemType.EditItem
                    If Session("uID").ToString().Trim() <> e.Item.Cells(12).Text.Trim() Then
                        e.Item.Cells(6).Enabled = False
                        e.Item.Cells(6).Text = "&nbsp;"
                        e.Item.Cells(4).Enabled = False
                        e.Item.Cells(4).Text = "&nbsp;"
                        e.Item.Cells(5).Enabled = False
                        e.Item.Cells(5).Text = "&nbsp;"
                    End If
            End Select
        End Sub

        Protected Sub Datagrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("g_edit") = 0 Then
                lbl_id.Text = e.Item.Cells(11).Text
                lbl_uid.Text = e.Item.Cells(12).Text

                txb_cate.Text = e.Item.Cells(1).Text

                btn_add.Text = "Modify"

                btn_cancel.Enabled = True
                btn_del.Enabled = True
                BindData(1)
            ElseIf e.CommandName.CompareTo("g_certificate") = 0 Then
                lbl_id.Text = e.Item.Cells(11).Text
                lbl_uid.Text = e.Item.Cells(12).Text
                Dim strSQL As String
                strSQL = "Update qaddoc.dbo.DocumentType Set isCertificated = ~isCertificated where typeID='" & lbl_id.Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                BindData(0)
            ElseIf e.CommandName.CompareTo("g_Appv") = 0 Then
                lbl_id.Text = e.Item.Cells(11).Text
                lbl_uid.Text = e.Item.Cells(12).Text
                Dim strSQL As String
                strSQL = "Update qaddoc.dbo.DocumentType Set isAppv = ~isnull( isAppv,0) where typeID='" & lbl_id.Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                BindData(0)
            ElseIf e.CommandName.CompareTo("g_admin") = 0 Then
                If e.Item.Cells(12).Text.Trim() = Convert.ToString(Session("uID")) Then
                    ltlAlert.Text = "window.open('doc_manager_admin.aspx?cateid=" & e.Item.Cells(11).Text().Trim() & "&uid=" & e.Item.Cells(12).Text().Trim() & "&un=" & Server.UrlEncode(e.Item.Cells(13).Text()) & "&rm=" & DateTime.Now.ToString() & "', '_blank');"
                End If
            ElseIf e.CommandName.CompareTo("g_access") = 0 Then
            ElseIf e.CommandName.CompareTo("g_type") = 0 Then
                If Session("uRole") > 1 And e.Item.Cells(12).Text.Trim() <> Session("uID").ToString() Then
                    ltlAlert.Text = "alert('只能操作自己创建的记录')"
                    BindData(0)
                    Exit Sub
                End If

                Response.Redirect("qad_catelist.aspx?cateid=" & e.Item.Cells(11).Text().Trim() & "&schemaid=" & SelectSchemaDropDown.SelectedValue & "&uid=" & e.Item.Cells(12).Text().Trim() & "&rm=" & DateTime.Now.ToString())
            ElseIf e.CommandName.CompareTo("g_apply") = 0 Then
            ElseIf e.CommandName.CompareTo("g_owner") = 0 Then
                If Session("uRole") > 1 And e.Item.Cells(12).Text() <> Session("uID") Then
                    ltlAlert.Text = "alert('只能操作自己创建的记录')"
                    BindData(0)
                    Exit Sub
                End If

                ltlAlert.Text = "var w=window.open('qad_documentowner.aspx?mid=" & e.Item.Cells(11).Text().Trim() & "&uid=" & e.Item.Cells(12).Text().Trim() _
                              & "&un=" & Server.UrlEncode(e.Item.Cells(13).Text()) & "&rm=" & DateTime.Now.ToString() _
                              & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
                BindData(0)
            End If
        End Sub

        Protected Sub SelectSchemaDropDown_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles SelectSchemaDropDown.SelectedIndexChanged
            Datagrid1.CurrentPageIndex = 0
            BindData(0)
        End Sub
    End Class

End Namespace













