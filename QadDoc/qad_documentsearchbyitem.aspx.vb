Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
'Imports System.Configuration

Namespace tcpc
    Partial Class qad_documentsearchbyitem
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strSql As String
        Dim ds As DataSet
        Dim reader As SqlDataReader
        Protected WithEvents txbconfirm As System.Web.UI.WebControls.TextBox
        Protected WithEvents txbitem As System.Web.UI.WebControls.TextBox
        Protected WithEvents filename As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents txbcat As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblitemID As System.Web.UI.WebControls.Label
        Protected WithEvents isDocComplete As System.Web.UI.WebControls.CheckBox
        Protected WithEvents isDocIgnore As System.Web.UI.WebControls.CheckBox

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
                BindItemData()
            End If
        End Sub

        Sub BindItemData()
            If Not String.IsNullOrEmpty(Request("code")) Then  ' 从部件目录页面 part/partlist.aspx
                txbold.Text = Request("code")
            End If

            strSql = " select id ,qad,status, (isnull(desc0,'')  +', '+isnull(desc1,'')  + ', ' + isnull(desc2,'')) as desc0,oldcode from qaddoc.dbo.qad_items where qad is not null"
            'strSql = "Select distinct pt_part, pt_status, pt_desc = pt_desc1 + '  ' + pt_desc2 "
            'strSql &= "From Qad_Data.dbo.pt_mstr "
            'strSql &= "Where pt_domain In ('SZX', 'ZQL', 'YQL', 'HQL') "

            If txbcode.Text.Trim <> "" Then
                strSql &= " and qad like '" & txbcode.Text.Trim.Replace("*", "%") & "' "
            End If
            If txbold.Text.Trim <> "" Then
                strSql &= " and oldcode like '" & txbold.Text.Replace("*", "%") & "' "
            End If
            If txbdesc.Text.Trim <> "" Then
                strSql &= " and (isnull(desc0,'') + '  ' + isnull(desc1,'') + '  ' + isnull(desc2,'')) like N'%" & txbdesc.Text.Trim.Replace("*", "%") & "' "
            End If
            strSql &= " order by qad "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
            'Response.Write(strSql)

            Dim dtl As New DataTable
            Dim total As Integer = 0
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("qad", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("oldcode", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("desc0", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))

            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("id") = 0 '.Rows(i).Item("id").ToString().Trim()
                        drow.Item("qad") = .Rows(i).Item("qad").ToString().Trim()
                        drow.Item("oldcode") = .Rows(i).Item("oldcode").ToString().Trim()
                        drow.Item("status") = .Rows(i).Item("status").ToString().Trim()
                        drow.Item("desc0") = .Rows(i).Item("desc0").ToString().Trim()
                        dtl.Rows.Add(drow)
                        total = total + 1
                    Next
                End If
            End With
            ds.Reset()

            Label1.Text = "Total: " & total.ToString()

            Dim dvw As DataView
            dvw = New DataView(dtl) 
                DgItem.DataSource = dvw
                DgItem.DataBind()
         
        End Sub

        Sub BindDocData()
            'Case " & Session("uRole") & " When 1 Then 0 Else Isnull(d.docLevel,3) - da.doc_acc_level End As docLevel, 
            strSql = " Select Distinct d.id, Isnull(d.filepath,'') As path, d.typeid, d.cateid, d.typename, d.catename, d.name, Isnull(d.description,'') As description,Isnull(d.pictureNo,'') As pictureNo, d.version, " _
                   & " d.filepath, Isnull(d.docLevel,3) As Level, Isnull(d.filename,'') As filename, Case d.isall When 1 Then 'Yes' Else 'No' End As IsAll, isNewMechanism = Isnull(d.isNewMechanism, 0),d.accFileName, " _
                   & " hiscnt = Isnull(his.cnt, 0),d.Path as virPath ,Case " & Session("uRole") & " When 1 Then 0 Else Isnull(d.docLevel,3) - isnull(da.doc_acc_level,9) End As docLevel" _
                   & " ,isnull(d.modifiedDate,d.createdDate) as Data" _
                   & " From qaddoc.dbo.documents d " _
                   & " Left Join(	Select typeid, cateid, Name, cnt = Count(*) From qaddoc.dbo.documents_his Group By typeid, cateid, Name ) as his On his.typeid = d.typeid And his.cateid = d.cateid And his.Name = d.Name " _
                   & " Left Outer Join qaddoc.dbo.documentitem di On d.id = di.docid " _
                   & " Left Outer Join qaddoc.dbo.documents_his dh On d.typeid = dh.typeid And d.cateid = dh.cateid And dh.id = d.id " _
                   & IIf(Session("uRole") = 1, "Left Outer", "Inner") & " Join qaddoc.dbo.DocumentAccess da On d.typeID = da.doc_acc_catid And da.approvedBy Is Not Null and da.doc_acc_userid = '" & Session("uID") & "' "

            strSql &= " Where d.isApprove = 1  And (di.qad = '" & Me.iid.Text.Trim & "' or d.isall='1') "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
            'Response.Write(ds)
            'Response.Write(strSql)

            Dim dtl As New DataTable

            dtl.Columns.Add(New DataColumn("docid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("typeid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("cateid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("typename", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("catename", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("filename", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("version", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("level", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("isall", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("preview", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("old", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("pictureNo", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("accFileName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("Data", System.Type.GetType("System.String")))

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
                        drow.Item("version") = .Rows(i).Item("version").ToString().Trim()
                        drow.Item("level") = .Rows(i).Item("Level").ToString().Trim()
                        drow.Item("isall") = .Rows(i).Item("IsAll").ToString().Trim()
                        drow.Item("accFileName") = .Rows(i).Item("accFileName").ToString().Trim()
                        drow.Item("Data") = .Rows(i).Item("Data").ToString().Trim()

                        '验证查看文档权限
                        'Dim reader_doclevel As SqlDataReader
                        Dim iValid As Integer = Integer.Parse(.Rows(i).Item("docLevel").ToString())
                        'Dim strSqldocLevel As String
                        'strSqldocLevel = "Select Case " & Session("uRole") & " When 1 Then 0 Else Isnull(d.docLevel,3) - da.doc_acc_level End As docLevel " _
                        '       & "From qaddoc.dbo.documents d " & IIf(Session("uRole") = 1, "Left Outer", "Inner") & " Join qaddoc.dbo.documentaccess da on d.typeid = da.doc_acc_catid " _
                        '       & "Where d.filepath = '" & .Rows(i).Item("filepath").ToString().Trim() & "'"
                        'If Session("uRole") <> 1 Then
                        '    strSqldocLevel &= " And da.doc_acc_userid = '" & Session("uID") & "' And da.approvedBy Is Not Null "
                        'End If
                        'reader_doclevel = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSqldocLevel)
                        'If (reader_doclevel.Read()) Then
                        '    iValid = Convert.ToInt32(reader_doclevel("docLevel"))
                        'Else
                        '    iValid = -1
                        'End If
                        If iValid >= 0 Then
                            Dim path As String = .Rows(i).Item("virPath").ToString()
                            'If Convert.ToBoolean(.Rows(i).Item("isNewMechanism")) Then
                            '    drow.Item("preview") = "<a href='/TecDocs/" & .Rows(i).Item("typeid").ToString().Trim() & "/" & .Rows(i).Item("cateid").ToString().Trim() & "/" & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                            'Else
                            '    drow.Item("preview") = "<a href='/qaddoc/qad_viewdocument.aspx?filepath=" & .Rows(i).Item("filepath").ToString().Trim() & "&code=" & "document" & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0' target='_blank'><u>Open</u></a>"
                            'End If
                            If String.IsNullOrEmpty(path) Then
                                drow.Item("preview") = "<a href='/TecDocs/" & .Rows(i).Item("typeid").ToString().Trim() & "/" & .Rows(i).Item("cateid").ToString().Trim() & "/" & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                            Else
                                drow.Item("preview") = "<a href='" & path & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                            End If

                            If Convert.ToInt32(.Rows(i).Item("hiscnt")) > 0 Then
                                drow.Item("old") = "<a href='/qaddoc/qad_olddocumentlist.aspx?code=" & Server.UrlEncode(.Rows(i).Item("name").ToString().Trim()) _
                                                     & "&typeid=" & .Rows(i).Item("typeid").ToString().Trim() & "&cateid=" & .Rows(i).Item("cateid").ToString().Trim() & "&id=" & .Rows(i).Item("id").ToString().Trim() _
                                                     & "',''docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300' target='_blank'><u>List</u></a>"
                            Else
                                drow.Item("old") = "&nbsp;"
                            End If

                            '如果有附加文件，就追加在后面
                            If .Rows(i).Item("accFileName").ToString().Trim().Length > 0 Then
                                Dim _accFileName As String = .Rows(i).Item("accFileName").ToString().Trim()
                                If String.IsNullOrEmpty(path) Then
                                    drow.Item("filename") = drow.Item("filename") & "&nbsp;(<a href='/TecDocs/" & .Rows(i).Item("typeid").ToString().Trim() & "/" & .Rows(i).Item("cateid").ToString().Trim() & "/" & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                                Else
                                    drow.Item("filename") = drow.Item("filename") & "&nbsp;(<a href='" & path & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                                End If

                            End If
                        Else
                            drow.Item("preview") = "&nbsp;"
                            drow.Item("old") = "&nbsp;"
                        End If
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Try
                DgDoc.DataSource = dvw
                DgDoc.DataBind()
            Catch
            End Try

        End Sub

        Private Sub DgDoc_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DgDoc.PageIndexChanged
            DgDoc.CurrentPageIndex = e.NewPageIndex
            BindDocData()
        End Sub

        Private Sub DgItem_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DgItem.PageIndexChanged
            DgItem.CurrentPageIndex = e.NewPageIndex
            DgItem.SelectedIndex = -1
            BindItemData()
            Me.iid.Text = ""
            BindDocData()
        End Sub

        Private Sub DgDoc_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgDoc.ItemCommand
            If e.CommandName.CompareTo("associated_item") = 0 Then
                ltlAlert.Text = "var w=window.open('/qaddoc/qad_documentitemlist.aspx?id=" & e.Item.Cells(1).Text.Trim() & "','docitem','menubar=No,scrollbars = No,resizable=No,width=600,height=500,top=200,left=300'); w.focus();"

            ElseIf e.CommandName.CompareTo("oldview") = 0 Then
                strSql = "select count(id) from qaddoc.dbo.documents_his where Name=N'" & Server.UrlEncode(e.Item.Cells(7).Text.Trim()) & "' and typeid = '" & e.Item.Cells(2).Text.Trim() & "'And cateID=" & e.Item.Cells(3).Text.Trim()
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) Then
                    ltlAlert.Text = "var w=window.open('/qaddoc/qad_olddocumentlist.aspx?code=" & Server.UrlEncode(e.Item.Cells(7).Text.Trim()) & "&typeid=" & e.Item.Cells(2).Text.Trim() & "&cateid=" & e.Item.Cells(3).Text.Trim() & "&id=" & e.Item.Cells(1).Text.Trim() & "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
                Else
                    ltlAlert.Text = "alert('No previous version.')"
                End If
            End If
        End Sub

        Private Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
            If DgItem.CurrentPageIndex > 0 Then
                DgItem.CurrentPageIndex = 0
            End If
            BindItemData()
        End Sub

        Private Sub DgItem_check(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgItem.ItemCommand
            If e.CommandName.CompareTo("Select") = 0 Then
                BindItemData()
                BindDocData()
            End If
        End Sub

        Private Sub DgItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DgItem.SelectedIndexChanged
            iid.Text = DgItem.SelectedItem.Cells(2).Text.Trim
            BindDocData()
        End Sub

        Private Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            DgItem.CurrentPageIndex = 0
            DgItem.SelectedIndex = -1
            BindItemData()
            iid.Text = ""
            BindDocData()
        End Sub
    End Class

End Namespace
