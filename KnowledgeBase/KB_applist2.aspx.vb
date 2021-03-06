Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

Partial Class KB_applist2
    Inherits System.Web.UI.Page
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
    Protected WithEvents btn_next As System.Web.UI.WebControls.Button
    Protected WithEvents btn_close As System.Web.UI.WebControls.Button
    Protected WithEvents bnt_cancel As System.Web.UI.WebControls.Button


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
            'chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 87100002)

            CheckBox1.Text = "只显示" & Session("uName") & "的记录"

            Dim strSQL As String
            Dim item As ListItem
            Dim ds As DataSet

            SelectTypeDropDown.SelectedIndex = 0
            item = New ListItem("--")
            item.Value = 0
            SelectTypeDropDown.Items.Add(item)

            strSQL = " SELECT TypeID,TypeName " _
                           & " From KnowDB.dbo.DocType" _
                           & " Order by TypeID"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        SelectTypeDropDown.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()

            SelectStatusDropDown.SelectedIndex = 0
            item = New ListItem("--")
            item.Value = 0
            SelectStatusDropDown.Items.Add(item)

            strSQL = " SELECT StatusID,StatusName " _
                           & " From KnowDB.dbo.DocStatus" _
                           & " Order by StatusID"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        SelectStatusDropDown.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()

                txtDate1.Text = Format(Today.AddDays(-30), "yyyy-MM-dd")
                txtDate2.Text = Format(Today, "yyyy-MM-dd")
            CheckBox1.Checked = True


            strSQL = "delete from knowdb.dbo.attachtemp where attuser=N'" & Session("uName") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            BindData()
        End If
    End Sub

    Sub BindData()
            StrSql = "select distinct a.docid,a.typeid,a.typename,a.doccontent, docuserid = isnull(a.docuserid,0), docuser = isnull(a.docuser,''), a.docdate,isnull(a.DocStatus,N'新建申请') as DocStatus from knowdb.dbo.application a left outer Join knowdb.dbo.Suggestion s on s.DocID = a.DocID  left outer Join knowdb.dbo.Attach t on t.DocID = a.DocID where a.DocID is not null "

        If SelectTypeDropDown.SelectedIndex > 0 Then
            StrSql &= " and a.typeid ='" & SelectTypeDropDown.SelectedValue & "' "
        End If

        If CheckBox1.Checked = True Then
            StrSql &= " and (a.doccurrid like '" & "%," + CStr(Session("uID")) + ",%" & "' or s.doccurrid like '" & "%," + CStr(Session("uID")) + ",%" & "') "
        End If

        If SelectStatusDropDown.SelectedIndex > 0 Then
            StrSql &= " and a.statusID ='" & SelectStatusDropDown.SelectedValue & "'"
        Else
            StrSql &= " and a.statusID <>'7' and a.statusID <> '3'"
        End If

        If txtCode.Text.Trim.Length > 0 Then
            StrSql &= " and ( LOWER(a.DocContent) like N'%" & txtCode.Text.Trim.ToLower & "%' "
            StrSql &= " or LOWER(s.SugContent) like N'%" & txtCode.Text.Trim.ToLower & "%'  "
            StrSql &= " or LOWER(t.Attname) like N'%" & txtCode.Text.Trim.ToLower & "%') "
        End If

        If txtDate1.Text.Trim.Length > 0 Then
            If IsDate(txtDate1.Text) Then
                StrSql &= " and DocDate>= '" & CDate(txtDate1.Text) & "' "
            End If
        End If
        If txtDate2.Text.Trim.Length > 0 Then
            If IsDate(txtDate2.Text) Then
                StrSql &= " and DocDate<= '" & CDate(txtDate2.Text).AddDays(1) & "' "
            End If
        End If
        StrSql &= " order by a.docdate desc "

	'response.write(strsql)
	'return

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

        Dim total As Integer

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("typeid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("docid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("docuserid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("docstatus", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("docuser", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("doccont", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("docdate", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("doctaken", System.Type.GetType("System.String")))
        Dim drow As DataRow
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("typeid") = .Rows(i).Item("typeid").ToString().Trim()
                    drow.Item("docid") = .Rows(i).Item("docid").ToString().Trim()
                    drow.Item("docuserid") = .Rows(i).Item("docuserid").ToString().Trim()

                    drow.Item("docstatus") = .Rows(i).Item("DocStatus").ToString().Trim()
                    drow.Item("docuser") = .Rows(i).Item("docuser").ToString().Trim()
                    drow.Item("doccont") = .Rows(i).Item("doccontent").ToString().Trim()
                    If IsDBNull(.Rows(i).Item("docdate")) = False Then
                        drow.Item("docdate") = Format(.Rows(i).Item("docdate"), "yy-MM-dd")
                    Else
                        drow.Item("docdate") = ""
                    End If

                    Dim reader As SqlDataReader
                    Dim Str As String
                    Dim txt As String = ""

                    Str = " Select SugContent  " _
                           & " From knowdb.dbo.Suggestion where DocID='" & drow.Item("docid") & "' " _
                           & " order by SugID "
                    reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, Str)
                    While (reader.Read())
                        txt = txt & reader(0).ToString().Trim() & "<br>"
                    End While
                    reader.Close()

                    drow.Item("doctaken") = txt
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
    Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
        If e.CommandName.CompareTo("docview") = 0 Then
            ltlAlert.Text = "window.open('KB_applist3.aspx?docid=" & e.Item.Cells(1).Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') "
        ElseIf e.CommandName.CompareTo("doctake") = 0 Then
            Response.Redirect("KB_app3.aspx?docid=" & e.Item.Cells(1).Text.Trim())
        End If
    End Sub
    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        BindData()
    End Sub

    Private Sub SelectTypeDropDown_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectTypeDropDown.SelectedIndexChanged
        BindData()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("KB_app2.aspx")
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        BindData()
    End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













