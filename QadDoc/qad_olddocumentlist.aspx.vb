Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

    Partial Class qad_olddocumentlist
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then 
                Label2.Text = "Document Name: " & Server.UrlDecode(Request("code"))
                BindData() 
            End If
        End Sub

        Private Sub BindData()
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT id, version, isnull(filepath,''), filename, isNewMechanism = Isnull(isNewMechanism, 0),createdname, createdDate, upgradeName, upgradeDate,PATH,accFileName = Isnull(accFileName, N'')"
            strSQL &= " From qaddoc.dbo.documents_his "
            strSQL &= " where name = N'" & Server.UrlDecode(Request("code")).Trim & "' And typeid = '" & Request("typeID") & "'and cateID='" & Request("cateid") & "' "
            strSQL &= " Order by createddate desc "
            'Response.Write(strSQL)
            'Exit Sub

            'Response.Write("<script>alert('"&strSQL&"')</script>")
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("version", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("filepath", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("filename", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("View", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("createdname", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("createdDate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("upgradeName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("upgradeDate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("accFileName", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("ID") = .Rows(i).Item(0)
                        dr1.Item("version") = .Rows(i).Item(1)
                        dr1.Item("filepath") = .Rows(i).Item(2)
                        dr1.Item("filename") = .Rows(i).Item(3)
                        dr1.Item("accFileName") = .Rows(i).Item("accFileName").ToString().Trim()
                        Dim pathOld As String = .Rows(i).Item("PATH").ToString().Trim()
                        If .Rows(i).Item("accFileName").ToString().Trim().Length > 0 Then
                            Dim _accFileName As String = .Rows(i).Item("accFileName").ToString().Trim()
                            If String.IsNullOrEmpty(pathOld) Then
                                dr1.Item("filename") = dr1.Item("filename") & "&nbsp;(<a href='/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/" & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                            Else
                                dr1.Item("filename") = dr1.Item("filename") & "&nbsp;(<a href='" & pathOld & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                            End If
                        End If
                        Dim path As String = .Rows(i).Item("Path").ToString()
                        If Len(.Rows(i).Item(2).ToString().Trim()) > 0 Then
                            'If Convert.ToBoolean(.Rows(i).Item("isNewMechanism")) Then
                            '    dr1.Item("View") = "<a href='/TecDocs/" & Request("typeID") & "/" & Request("cateid") & "/" & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                            'Else
                            '    dr1.Item("View") = "<a href='/qaddoc/qad_viewdocument.aspx?filepath=" & .Rows(i).Item(2).ToString.Trim _
                            '                     & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0' target='_blank'>Open</a>"
                            'End If
                            If String.IsNullOrEmpty(path) Then
                                dr1.Item("View") = "<a href='/TecDocs/" & Request("typeID") & "/" & Request("cateid") & "/" & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                            Else
                                dr1.Item("View") = "<a href='" & path & .Rows(i).Item("filename").ToString().Trim() & "' target='_blank'><u>Open</u></a>"
                            End If
                        Else
                            dr1.Item("View") = "&nbsp;"
                        End If

                        dr1.Item("createdname") = .Rows(i).Item(5)
                        dr1.Item("createdDate") = Format(.Rows(i).Item("createdDate"), "yyyy-MM-dd")
                        dr1.Item("upgradeName") = .Rows(i).Item(7)
                        dr1.Item("upgradeDate") = .Rows(i).Item("upgradeDate")
                        If dr1.Item("upgradeDate").ToString().Length > 0 Then
                            dr1.Item("upgradeDate") = Format(.Rows(i).Item("upgradeDate"), "yyyy-MM-dd")
                        End If

                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            ds.Reset()

            Dim dv As DataView
            dv = New DataView(dt)
            DataGrid1.DataSource = dv
            DataGrid1.DataBind()

        End Sub

        Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
            Session("orderby1") = e.SortExpression.ToString()
            If Session("orderdir1") = " ASC" Then
                Session("orderdir1") = " DESC"
            Else
                Session("orderdir1") = " ASC"
            End If
            BindData()
        End Sub

        'Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        '    If e.CommandName.CompareTo("viewbtn") = 0 Then
        '        If e.Item.Cells(2).Text.ToString.Trim <> "" Then
        '            ltlAlert.Text = " var w=window.open('/qaddoc/qad_viewdocument.aspx?filepath=" & e.Item.Cells(2).Text.ToString.Trim & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0'); w.focus();"
        '        Else
        '            ltlAlert.Text = "alert('No previous version');"
        '        End If
        '    End If
        'End Sub

    End Class

End Namespace
