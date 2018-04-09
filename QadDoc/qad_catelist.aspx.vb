Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

    Partial Class qad_catelist
        Inherits BasePage
        Public chk As New adamClass
        Protected WithEvents lblsubcat As System.Web.UI.WebControls.Label

        Dim strSql As String

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
                'delete useless data
                ClearUselessData()
                ClearTempData()
                'load
                LoadDocSchema()
                If Request("schemaid") <> Nothing Then
                    SelectSchemaDropDown.SelectedValue = Request("schemaid")
                End If
                LoadDocType()
                If Request("cateid") <> Nothing Then
                    SelectTypeDropDown.SelectedValue = Request("cateid")
                End If

                BindProductTrackingType()
                dropTackingDetail.Items.Insert(0, New ListItem("--", "0"))

                LoadDocData()
                BindData()
                btncancel.Visible = False
            End If
        End Sub

        Sub BindProductTrackingType()
            strSql = " Select Distinct ptt_type From qaddoc.dbo.ProductTrackingType "

            Dim ds As DataSet = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSql)

            dropTackingType.DataSource = ds
            dropTackingType.DataBind()

            dropTackingType.Items.Insert(0, New ListItem("--", "0"))
        End Sub

        Sub BindProductTrackingDetail(ByVal type As String)
            strSql = " Select ptt_id, ptt_detail From qaddoc.dbo.ProductTrackingType Where ptt_type = '" & type & "'"

            Dim ds As DataSet = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSql)

            dropTackingDetail.DataSource = ds
            dropTackingDetail.DataBind()

            dropTackingDetail.Items.Insert(0, New ListItem("--", "0"))
        End Sub

        Sub BindData()
            Dim ds As DataSet
            strSql = " SELECT dc.cateid,dc.typeID,dct.tag,ISNULL(dc.catename,'') as catename,case when dc.ispublish =1 then N'Yes' else N'No' end as ispublish " _
                & " , ptt_id = Isnull(trackingtype, 0), ptt_type = Isnull(ptt_type, ''), ptt_detail = Isnull(ptt_detail, ''), linkVend = Isnull(linkVend,0) " _
               & " From qaddoc.dbo.DocumentCategory dc " _
               & " INNER JOIN qaddoc.dbo.DocumentCategoryTemp dct on dct.DocCategoryID=dc.cateid and dct.createdBy='" & Session("uID") & "' " _
               & " Left Join qaddoc.dbo.ProductTrackingType ptt On ptt.ptt_id = dc.trackingtype" _
               & " Where dc.typeid='" & SelectTypeDropDown.SelectedValue & "' " _
               & " Order by dct.id "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("cat", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("cate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("publish", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("pub", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ptt_id", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ptt_type", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ptt_detail", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("linkVend", System.Type.GetType("System.Boolean")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim j As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        Dim tag As String = ""
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("id") = .Rows(i).Item("cateid").ToString().Trim()
                        For j = 1 To .Rows(i).Item("tag")
                            tag = tag & "&nbsp;&nbsp;&nbsp;"
                        Next
                        dr1.Item("cat") = tag & .Rows(i).Item("catename").ToString().Trim()
                        dr1.Item("cate") = .Rows(i).Item("catename").ToString().Trim()
                        dr1.Item("publish") = .Rows(i).Item("ispublish")
                        If .Rows(i).Item("ispublish") = "Yes" Then
                            dr1.Item("pub") = "1"
                        Else
                            dr1.Item("pub") = "0"
                        End If

                        dr1.Item("ptt_id") = .Rows(i).Item("ptt_id").ToString().Trim()
                        dr1.Item("ptt_type") = .Rows(i).Item("ptt_type").ToString().Trim()
                        dr1.Item("ptt_detail") = .Rows(i).Item("ptt_detail").ToString().Trim()
                        dr1.Item("linkVend") = .Rows(i).Item("linkVend")

                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)
            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()

            Catch
            End Try
        End Sub

        Sub LoadDocType()
            Dim ls As ListItem
            Dim reader As SqlDataReader
            SelectTypeDropDown.Items.Clear()

            If Session("uRole") = 1 Or Me.Security("1001030691").isValid Then
                'strSql = " Select Distinct typeid,typename From qaddoc.dbo.DocumentType Where isDeleted Is Null and Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' " & " Order By typeid "
                'strSql = " Select Distinct typeid,typename From qaddoc.dbo.DocumentType Where isDeleted Is Null Order By typeid "
                strSql = " Select Distinct typeid,typename From qaddoc.dbo.DocumentType Where isDeleted Is Null "
                If SelectSchemaDropDown.SelectedValue > 0 Then
                    strSql &= " and Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' " & " Order By typeid "
                End If

            Else
                strSql = " Select Distinct d.typeid,d.typename From qaddoc.dbo.DocumentType d Inner Join qaddoc.dbo.DocumentAccess ua On ua.doc_acc_userid ="
                strSql &= Session("uID") & " And ua.doc_acc_catid = d.typeid Where d.isDeleted Is Null "
                If SelectSchemaDropDown.SelectedValue > 0 Then
                    strSql &= " and Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' " & " Order By typeid "
                End If
                '& Session("uID") & " And ua.doc_acc_catid = d.typeid Where d.isDeleted Is Null and Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' " & " Order By d.typeid "
            End If

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim 
                SelectTypeDropDown.Items.Add(ls)
            End While
            reader.Close()
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

        Sub LoadDocData()
            Dim reader As SqlDataReader
            strSql = " SELECT cateid " _
               & " From qaddoc.dbo.DocumentCategory  " _
               & " Where parentID is null " _
               & " Order by cateid "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read()
                strSql = " insert into qaddoc.dbo.DocumentCategoryTemp values('" & reader(0) & "',0,'" & Session("uID") & "')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                GetDocDetail(reader(0))
            End While
            reader.Close()
        End Sub

        Sub GetDocDetail(ByVal DocCatID As Integer, Optional ByVal tag As Integer = 1)
            Dim ds As DataSet
            Dim i As Integer
            strSql = " Select cateid From qaddoc.dbo.DocumentCategory Where parentID=" & DocCatID & " Order By cateid "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    For i = 0 To .Rows.Count - 1
                        strSql = "Insert Into qaddoc.dbo.DocumentCategoryTemp Values('" & .Rows(i).Item(0) & "'," & tag & "," & Session("uID") & ") "
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                        GetDocDetail(.Rows(i).Item(0), tag + 1)
                    Next
                Else

                End If
            End With
            ds.Reset()
        End Sub

        Private Sub SelectTypeDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectTypeDropDown.SelectedIndexChanged
            DataGrid1.EditItemIndex = -1
            DataGrid1.SelectedIndex = -1
            DataGrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                If Session("uRole") > 1 Then
                    strSql = "Select count(doc_acc_id) from qaddoc.dbo.DocumentAccess where doc_acc_userid='" & Session("uID") & "' and doc_acc_level=0 "
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) <= 0 Then
                        ltlAlert.Text = "alert('Access is denied.')"
                        Exit Sub
                    End If
                End If

                strSql = "select count(*) from qaddoc.dbo.DocumentCategory where parentid='" & e.Item.Cells(0).Text() & "' and typeid='" & SelectTypeDropDown.SelectedValue & "' "
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                    ltlAlert.Text = "alert('It is not able to be deleted if it has the sub-dir.');"
                    Exit Sub
                Else
                    strSql = "select count(*) from qaddoc.dbo.documents where cateid='" & e.Item.Cells(0).Text() & "' and typeid='" & SelectTypeDropDown.SelectedValue & "' "
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                        ltlAlert.Text = "alert('It is not able to be deleted if there are some items belonging to the dir.');"
                        Exit Sub
                    End If
                End If

                strSql = "Delete From qaddoc.dbo.DocumentCategory where cateid = '" & e.Item.Cells(0).Text() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                DataGrid1.EditItemIndex = -1
                DataGrid1.SelectedIndex = -1
                txbadd.Text = ""
                AddBtn.Text = "Add"
                btncancel.Visible = False
                BindData()
            ElseIf (e.CommandName.CompareTo("Select")) = 0 Then
                AddBtn.Text = "Add Sub-dir"
                btncancel.Visible = True
                BindData()
            ElseIf (e.CommandName.CompareTo("myEdit")) = 0 Then

                AddBtn.Text = "Save"
                btncancel.Visible = True

                Dim index As Integer = CType(CType(e.CommandSource, LinkButton).Parent.Parent, DataGridItem).ItemIndex

                hidID.Value = DataGrid1.Items(index).Cells(0).Text.Trim()
                txbadd.Text = DataGrid1.Items(index).Cells(2).Text.Trim()

                Try
                    dropTackingType.SelectedIndex = -1
                    dropTackingDetail.SelectedIndex = -1

                    dropTackingType.Items.FindByValue(DataGrid1.Items(index).Cells(3).Text.Trim()).Selected = True

                    dropTackingType_SelectedIndexChanged(Me, New System.EventArgs())

                    dropTackingDetail.Items.FindByText(DataGrid1.Items(index).Cells(4).Text.Trim()).Selected = True
                Catch ex As Exception

                End Try

            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub AddBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddBtn.Click
            If Session("uRole") > 1 Then
                strSql = "Select count(doc_acc_id) from qaddoc.dbo.DocumentAccess where doc_acc_userid='" & Session("uID") & "' and doc_acc_level=0 "
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) <= 0 Then
                    ltlAlert.Text = "alert('Access is denied.')"
                    Exit Sub
                End If
            End If

            Dim maxID As Integer

            If txbadd.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('Dir name is required.');"
                Exit Sub
            End If

            'Response.Write(DataGrid1.SelectedItem.Cells(0).Text)
            If AddBtn.Text.Trim = "Add Sub-dir" Then
                strSql = "Select cateid from qaddoc.dbo.DocumentCategory where catename=N'" & txbadd.Text.Trim & "' and typeID=" & SelectTypeDropDown.SelectedValue
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                    ltlAlert.Text = "alert('Dir name is exist.')"
                    Exit Sub
                End If

                strSql = "Insert Into qaddoc.dbo.DocumentCategory(typeID,catename,parentID,typename, trackingtype) Values('" & SelectTypeDropDown.SelectedValue & "',N'" & txbadd.Text.Trim & "','" & DataGrid1.SelectedItem.Cells(0).Text & "',N'" & SelectTypeDropDown.SelectedItem.Text & "', " & dropTackingDetail.SelectedValue & ")"
                'Response.Write(SelectTypeDropDown.SelectedItem.Text)

                ' strSql &= " select @@identity "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                ClearTempData()
                LoadDocData()                               'reload data
            ElseIf AddBtn.Text.Trim = "Add" Then
                strSql = "Select cateid from qaddoc.dbo.DocumentCategory where catename=N'" & txbadd.Text.Trim & "' and typeID=" & SelectTypeDropDown.SelectedValue
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                    ltlAlert.Text = "alert('Dir name is exist.')"
                    Exit Sub
                End If

                strSql = "Insert Into qaddoc.dbo.DocumentCategory(typeID,catename,typename, trackingtype) Values('" & SelectTypeDropDown.SelectedValue & "',N'" & txbadd.Text.Trim & "',N'" & SelectTypeDropDown.SelectedItem.Text & "', " & dropTackingDetail.SelectedValue & ") "
                strSql &= " select @@identity "
                maxID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

                strSql = " insert into qaddoc.dbo.DocumentCategoryTemp values('" & maxID & "',0,'" & Session("uID") & "')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                GetDocDetail(maxID)
            ElseIf AddBtn.Text = "Save" Then
                Dim str As String = txbadd.Text.Trim
                If str.Length <= 0 Then
                    ltlAlert.Text = "alert('Type name is required')"
                    Exit Sub
                End If

                strSql = "Select cateid from qaddoc.dbo.DocumentCategory where catename=N'" & str & "' and cateid<>'" & hidID.Value & "' and typeID=" & SelectTypeDropDown.SelectedValue
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                    ltlAlert.Text = "alert('The dir name is exist.')"
                    Exit Sub
                End If

                'If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkispub"), CheckBox).Checked = False Then
                strSql = "update qaddoc.dbo.DocumentCategory "
                strSql &= " set catename=N'" & str & "' "
                strSql &= " , trackingtype = " & dropTackingDetail.SelectedValue
                strSql &= " where cateid='" & hidID.Value & "'"

                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                DataGrid1.EditItemIndex = -1
                BindData()

            End If

            hidID.Value = "0"
            txbadd.Text = ""
            AddBtn.Text = "Add"
            dropTackingType.SelectedIndex = -1
            dropTackingDetail.SelectedIndex = -1
            btncancel.Visible = False
            DataGrid1.CurrentPageIndex = 0
            DataGrid1.SelectedIndex = -1
            BindData()
        End Sub

        Sub ClearUselessData()
            strSql = "delete from qaddoc.dbo.DocumentCategory where catename ='' or catename is null "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        End Sub

        Sub ClearTempData()
            strSql = "delete from qaddoc.dbo.DocumentCategoryTemp where createdBy='" & Session("uID") & "' "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        End Sub

        Private Sub Btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
            DataGrid1.SelectedIndex = -1
            txbadd.Text = ""
            AddBtn.Text = "Add"

            dropTackingType.SelectedIndex = -1
            dropTackingDetail.SelectedIndex = -1

            btncancel.Visible = False
        End Sub

        Protected Sub dropTackingType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropTackingType.SelectedIndexChanged
            If dropTackingType.SelectedIndex <> 0 Then
                dropTackingDetail.Items.Clear()
                BindProductTrackingDetail(dropTackingType.SelectedValue)
            End If
        End Sub

        Protected Sub chkSinger_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim ck As CheckBox = sender
            Dim gvRow As DataGridItem = ck.Parent.Parent
            Dim index As Integer = gvRow.ItemIndex
            strSql = "qaddoc.dbo.qad_documentCateLinkVend"
            Dim params(1) As SqlParameter
            params(0) = New SqlParameter("@cateID", DataGrid1.DataKeys(index).ToString())
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSql, params)
        End Sub

        Protected Sub SelectSchemaDropDown_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles SelectSchemaDropDown.SelectedIndexChanged
            LoadDocType()
            BindData()
        End Sub

        Protected Sub btnback_Click(sender As Object, e As System.EventArgs) Handles btnback.Click
            Response.Redirect("qad_typelist.aspx?schemaid=" & SelectSchemaDropDown.SelectedValue & "&rm=" & DateTime.Now.ToString())
        End Sub
    End Class

End Namespace
