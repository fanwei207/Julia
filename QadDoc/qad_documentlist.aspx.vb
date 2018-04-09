Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
    Partial Class documentlist
        Inherits BasePage 
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass
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
                LoadDocSchema()
                LoadDocType()
                If Request("typeID") <> Nothing Then
                    SelectTypeDropDown.SelectedValue = Request("typeID")
                End If
                If Request("pg") <> Nothing Then
                    DataGrid1.CurrentPageIndex = Request("pg")
                End If
                BindData()
            End If
        End Sub

        Sub BindData()
            Dim ds As DataSet
            strSql = " Select cateid, typeID, catename, typename, Sum(cnt) As cnt " _
                   & " From ( " _
                   & " Select dc.cateid, dc.typeID, dc.catename, dc.typename, Isnull(dr.cnt, 0) as cnt " _
                   & " From qaddoc.dbo.DocumentCategory dc " _
                   & " Left Outer Join " _
                   & " ( " _
                   & "    Select d.typeid, d.cateID, Count(*) as cnt, Isnull(d.doclevel,3) As lvl " _
                   & "    From qaddoc.dbo.documents d "
            If Session("uRole") <> 1 Then
                strSql &= " Inner Join qaddoc.dbo.DocumentAccess ua On d.typeID = ua.doc_acc_catid And Isnull(d.doclevel,3) >= ua.doc_acc_level And ua.doc_acc_userid =  '" & Session("uID") & "' And ua.approvedBy Is Not Null "
            End If
            strSql &= "    Group By d.typeid, d.cateID, Isnull(d.doclevel,3) " _
                   & " ) dr On dr.typeid = dc.typeid And dr.cateid = dc.cateid "

            If SelectSchemaDropDown.SelectedValue > 0 Then
                strSql &= " Left Join qaddoc.dbo.DocumentType tp on tp.typeid = dc .typeid "
            End If
            If Session("uRole") <> 1 Then
                strSql &= " Where dc.typeid In (Select Distinct doc_acc_catid From qaddoc.dbo.DocumentAccess Where Isnull(dr.lvl,3) >= doc_acc_level And doc_acc_userid = '" & Session("uID") & "' And approvedBy Is Not Null) "
                If SelectTypeDropDown.SelectedValue > 0 Then
                    strSql &= "  And dc.typeid='" & SelectTypeDropDown.SelectedValue & "' "
                End If
                If SelectSchemaDropDown.SelectedValue > 0 Then
                    strSql &= " And Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' "
                End If
            Else
                If SelectTypeDropDown.SelectedValue > 0 Then
                    strSql &= "  Where dc.typeid='" & SelectTypeDropDown.SelectedValue & "' "
                    If SelectSchemaDropDown.SelectedValue > 0 Then
                        strSql &= " And Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' "
                    End If
                Else
                    If SelectSchemaDropDown.SelectedValue > 0 Then
                        strSql &= " Where Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' "
                    End If
                End If
            End If
            strSql &= " ) doc Group By cateid, typeID, catename, typename " _
                   & " Order By typeid, cateid "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("typeid", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("cateid", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("typename", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("catename", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("fqty", System.Type.GetType("System.Double")))
            With ds.Tables(0)
                Dim total As Integer = 0
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim j As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        Dim tag As String = ""
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("typeid") = .Rows(i).Item("typeid").ToString().Trim()
                        dr1.Item("cateid") = .Rows(i).Item("cateid").ToString().Trim()
                        dr1.Item("catename") = .Rows(i).Item("catename").ToString().Trim()
                        dr1.Item("typename") = .Rows(i).Item("typename").ToString().Trim()
                        dr1.Item("fqty") = .Rows(i).Item("cnt").ToString().Trim()
                        total = total + dr1.Item("fqty")
                        dt.Rows.Add(dr1)
                    Next
                End If
                countLabel.Text = "Number of Type: " & .Rows.Count & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Number of Doc: " & total.ToString
            End With
            ds.Reset()

            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
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


        Sub LoadDocType()
            Dim ls As ListItem
            Dim reader As SqlDataReader
            SelectTypeDropDown.Items.Clear()
            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            SelectTypeDropDown.Items.Add(ls)

            If Session("uRole") = 1 Then
                strSql = " Select Distinct typeid,typename From qaddoc.dbo.DocumentType Where isDeleted Is Null  "
                If SelectSchemaDropDown.SelectedValue > 0 Then
                    strSql &= " and Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' "
                End If
                strSql &= " Order By typeid "
            Else
                strSql = " Select Distinct d.typeid,d.typename From qaddoc.dbo.DocumentType d Inner Join qaddoc.dbo.DocumentAccess ua On ua.doc_acc_userid =" _
                        & Session("uID") & " And ua.doc_acc_catid = d.typeid Where d.isDeleted Is Null "
                '& Session("uID") & " And ua.doc_acc_catid = d.typeid Where d.isDeleted Is Null and Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' " & " Order By d.typeid "
                If SelectSchemaDropDown.SelectedValue > 0 Then
                    strSql &= " and Schemaid = '" & SelectSchemaDropDown.SelectedValue & "' "
                End If
                strSql &= " Order By d.typeid "
            End If

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

        Private Sub SelectTypeDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectTypeDropDown.SelectedIndexChanged
            DataGrid1.CurrentPageIndex = 0
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
            Session("orderby") = e.SortExpression.ToString()
            If Session("orderdir") = " ASC" Then
                Session("orderdir") = " DESC"
            Else
                Session("orderdir") = " ASC"
            End If
            BindData()
        End Sub
        Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If e.CommandName.CompareTo("DocDetail") = 0 Then
                Response.Redirect("/qaddoc/qad_documentDetail1.aspx?typeID=" & e.Item.Cells(0).Text.Trim & "&cateid=" & e.Item.Cells(1).Text.Trim & "&pg=" & DataGrid1.CurrentPageIndex & "&typename=" & Server.UrlEncode(e.Item.Cells(3).Text.Trim) & "&catename=" & Server.UrlEncode(e.Item.Cells(4).Text.Trim)) 
            End If
        End Sub


        Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
            If e.Item.ItemIndex >= 0 And e.Item.ItemIndex <= CType(sender, DataGrid).PageSize() - 1 Then 

            End If
        End Sub

        Protected Sub SelectSchemaDropDown_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles SelectSchemaDropDown.SelectedIndexChanged
            LoadDocType()
            BindData()
        End Sub
    End Class

End Namespace
