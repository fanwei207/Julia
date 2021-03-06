Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class cmd_list
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim item As ListItem
        Dim nRet As Integer

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
                BindData()
            End If
        End Sub

        Sub BindData()

            StrSql = "select cmd_mstr_id,cmd_user_id,cmd_content,cmd_date,cmd_taken_name,cmd_closeddate "
            StrSql &= " from KnowDB.dbo.cmd_mstr where cmd_deleteddate is null "

            If CheckBox1.Checked = False Then
                StrSql &= " and cmd_closeddate is null "
            End If

            If txb_uname.Text.Trim.Length > 0 Then
                StrSql &= " and cmd_taken_name like N'%" & txb_uname.Text.Trim & "%' "
            End If

            If txtCode.Text.Trim.Length > 0 Then
                StrSql &= " and LOWER(cmd_content) like N'%" & chk.sqlEncode(txtCode.Text.Trim.ToLower) & "%' "
            End If
            StrSql &= " order by cmd_date "

            Session("EXSQL") = StrSql

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("mstrid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("userid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("doccont", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("doctaken", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docrep", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docclo", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("mstrid") = .Rows(i).Item(0)
                        drow.Item("userid") = .Rows(i).Item(1)
                        drow.Item("doccont") = .Rows(i).Item(2).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(3)) = False Then
                            drow.Item("docdate") = Format(.Rows(i).Item(3), "yy-MM-dd")
                        Else
                            drow.Item("docdate") = ""
                        End If
                        drow.Item("doctaken") = .Rows(i).Item(4).ToString().Trim()

                        If IsDBNull(.Rows(i).Item(5)) = False Then
                            drow.Item("docclo") = Format(.Rows(i).Item(5), "yy-MM-dd")
                        Else
                            drow.Item("docclo") = ""
                        End If

                        Dim reader As SqlDataReader
                        Dim Str As String
                        Dim txt As String = ""

                        Str = " Select cmd_user_name,cmd_date,cmd_content " _
                                   & " From KnowDB.dbo.cmd_detail  where cmd_mstr_id='" & .Rows(i).Item(0) & "' " _
                                   & " order by cmd_date  "
                        reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, Str)
                        While (reader.Read())
                            txt = txt + reader(0).ToString().Trim() & "(" & reader(1).ToString().Trim() & "):" & reader(2).ToString().Trim() & "<br>"
                        End While
                        reader.Close()

                        If txt.Length > 0 Then

                            drow.Item("docrep") = txt
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
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("docact") = 0 Then
                Response.Redirect("cmd_app3.aspx?mid=" & e.Item.Cells(0).Text.Trim())
            End If
        End Sub
        Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
            BindData()
        End Sub

        Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
            Response.Redirect("cmd_app2.aspx")
        End Sub

        Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            BindData()
        End Sub

        Protected Sub btn_Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Export.Click
            ltlAlert.Text = "var w=window.open('cmd_exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub
    End Class

End Namespace













