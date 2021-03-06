Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
    Partial Class qad_documentapprove
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
               
                LoadDocType()
                LoadDocCate()
                BindData()

            End If
        End Sub
        Sub LoadDocType()
            Dim ls As ListItem
            Dim reader As SqlDataReader
            ls = New ListItem
            ls.Value = 0
            ls.Text = "-----"
            SelectTypeDropDown.Items.Add(ls)

            strSql = " select d.typeid,d.typename from qaddoc.dbo.DocumentType d  "
           
            If SelectTypeDropDown.SelectedValue > 0 Then
                strSql &= "where typeid = '" & SelectTypeDropDown.SelectedValue & "'"
            End If
            strSql &= "order by typeid "
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

        Sub LoadDocCate()
            SelectCateDropDown.Items.Clear()

            Dim ls As ListItem
            Dim reader As SqlDataReader
            ls = New ListItem
            ls.Value = 0
            ls.Text = "-----"
            SelectCateDropDown.Items.Add(ls)

            strSql = " select d.cateid,d.catename from qaddoc.dbo.DocumentCategory d  " 
            strSql &= " where d.catename is not null"
            If SelectTypeDropDown.SelectedValue > 0 Then
                strSql &= " and d.typeid = '" & SelectTypeDropDown.SelectedValue & "'"
            End If
            If SelectCateDropDown.SelectedValue > 0 Then
                strSql &= " and d.cateid = '" & SelectCateDropDown.SelectedValue & "'"
            End If
            strSql &= " order by d.typeid "
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
            strSql = "select d.id,d.typeid,d.cateid,d.typename,d.catename,d.filename,d.name,d.version,d.filepath,d.ispublish,d.status,d.isall,case when a.appr_apprDate is null then 'No'else 'Yes' end as apprn,a.appr_apprDate,d.description from qaddoc.dbo.documents d "
            strSql &= " Inner Join qaddoc.dbo.DocumentApprove a on d.id=a.appr_docid and a.appr_userid='" & Session("uID") & "'"
            strSql &= " where d.id is not null and d.status=0 "
            If chkAll.Checked = False Then
                strSql &= " and a.appr_apprdate is null "
            End If
            If SelectTypeDropDown.SelectedValue > 0 Then
                strSql &= " and d.typeid = '" & SelectTypeDropDown.SelectedValue & "'"
            End If
            If SelectCateDropDown.SelectedValue > 0 Then
                strSql &= " and d.cateid = '" & SelectCateDropDown.SelectedValue & "'"
            End If
            If txb_search.Text.Trim.Length > 0 Then
                strSql &= " and d.name like N'%" & txb_search.Text.Trim & "%'"
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
            dtl.Columns.Add(New DataColumn("ispublic", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("isall", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("checkedname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("checkeddate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("preview", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("old", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("associated_item", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))

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
                        drow.Item("version") = .Rows(i).Item("version")
                        If .Rows(i).Item("status") = 0 Then
                            drow.Item("status") = "No"
                        Else
                            drow.Item("status") = "Yes"
                        End If
                        If .Rows(i).Item("ispublish") = 0 Then
                            drow.Item("ispublic") = "No"
                        Else
                            drow.Item("ispublic") = "Yes"
                        End If
                        If .Rows(i).Item("isall") = 0 Then
                            drow.Item("isall") = "No"
                        Else
                            drow.Item("isall") = "Yes"
                        End If
                        drow.Item("checkedname") = "<u>" & .Rows(i).Item("apprn").ToString().Trim() & "</u>"
                        If IsDBNull(.Rows(i).Item("appr_apprDate")) = False Then
                            drow.Item("checkeddate") = Format(.Rows(i).Item("appr_apprDate"), "yyyy-MM-dd")
                        Else
                            drow.Item("checkeddate") = ""
                        End If
                        drow.Item("preview") = "<a href='/qaddoc/qad_viewdocument.aspx?filepath=" & .Rows(i).Item("filepath").ToString().Trim() & "&code=" & "document" & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0' target='_blank'><u>Open</u></a>"
                        drow.Item("associated_item") = "<a href='/qaddoc/qad_documentitemview.aspx?id=" & .Rows(i).Item("id").ToString().Trim() & "&code=" & "document" & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0' target='_blank'><u>Items</u></a>"
                        drow.Item("old") = "<u>List</u>"
                        total = total + 1
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()
            Label1.Text = "Total: " & total.ToString()

            Dim dvw As DataView
            dvw = New DataView(dtl)
            If (Session("orderby").Length <= 0) Then
                Session("orderby") = "name"
            End If
            Try
                dvw.Sort = Session("orderby") & Session("orderdir")
                DgDoc.DataSource = dvw
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
        Private Sub DgDoc_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DgDoc.SortCommand
            Session("orderby") = e.SortExpression.ToString()
            If Session("orderdir") = " ASC" Then
                Session("orderdir") = " DESC"
            Else
                Session("orderdir") = " ASC"
            End If
            BindData()
        End Sub

        Private Sub DgDoc_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgDoc.ItemCommand
            'Response.Write(e.Item.Cells(2).Text)
            If e.CommandName.CompareTo("associated_item") = 0 Then
                ltlAlert.Text = "var w=window.open('/qaddoc/qad_documentitemlist.aspx?id=" & e.Item.Cells(1).Text.Trim() & "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300'); w.focus();"
            ElseIf e.CommandName.CompareTo("oldview") = 0 Then
                strSql = "select count(id) from qaddoc.dbo.documents_his where Name=N'" & Server.UrlEncode(e.Item.Cells(7).Text.Trim()) & "' and typeid = '" & e.Item.Cells(2).Text.Trim() & "'And cateID=" & e.Item.Cells(3).Text.Trim()
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) Then
                    ltlAlert.Text = "var w=window.open('/qaddoc/qad_olddocumentlist.aspx?code=" & Server.UrlEncode(e.Item.Cells(7).Text.Trim()) & "&typeid=" & e.Item.Cells(2).Text.Trim() & "&cateid=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(1).Text.Trim() & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
                Else
                    ltlAlert.Text = "alert('No previous version！')"
                End If
            ElseIf e.CommandName.CompareTo("Appr") = 0 Then
                ltlAlert.Text = "var w=window.open('qad_documentappr.aspx?catid=" & e.Item.Cells(1).Text.Trim() & "&rm=" & DateTime.Now.ToString() & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
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

        Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
            DgDoc.EditItemIndex = -1
            DgDoc.CurrentPageIndex = 0
            BindData()
        End Sub
    End Class

End Namespace
