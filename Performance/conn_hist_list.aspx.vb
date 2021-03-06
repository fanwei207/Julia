Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

Partial Class conn_hist_list
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
    Dim item As ListItem
    Dim nRet As Integer 
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button

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
                txtDate1.Text = Format(Today.AddDays(-7), "yyyy-MM-dd")
                txtDate2.Text = Format(Today, "yyyy-MM-dd")

            If Session("uRole") = 1 Then
                    Button1.Visible = True
            End If
            BindData()
        End If
    End Sub

    Sub BindData()

        StrSql = "select conn_mstr_id,conn_content,conn_user_id,conn_user_name,conn_date,conn_dept"
        StrSql &= " from KnowDB.dbo.conn_mstr where conn_closeddate is not null and conn_deleteddate is null"

        If txb_uname.Text.Trim.Length > 0 Then
            StrSql &= " and conn_user_name like N'%" & txb_uname.Text.Trim & "%' "
        End If

        If txb_dept.Text.Trim.Length > 0 Then
            StrSql &= " and conn_dept like N'%" & txb_dept.Text.Trim & "%' "
        End If

        If txtCode.Text.Trim.Length > 0 Then
            StrSql &= " and LOWER(conn_content) like N'%" & chk.sqlEncode(txtCode.Text.Trim.ToLower) & "%' "
        End If
        If txtDate1.Text.Trim.Length > 0 Then
            If IsDate(txtDate1.Text) Then
                StrSql &= " and conn_date>= '" & CDate(txtDate1.Text) & "' "
            End If
        End If
        If txtDate2.Text.Trim.Length > 0 Then
            If IsDate(txtDate2.Text) Then
                StrSql &= " and conn_date<= '" & CDate(txtDate2.Text).AddDays(1) & "' "
            End If
        End If

            StrSql &= " and (conn_user_id = '" & Session("uID") & "' or conn_mstr_id in (Select distinct conn_mstr_id From KnowDB.dbo.conn_taken Where conn_taken_id ='" & Session("uID") & "'))"

            StrSql &= " order by conn_date desc "

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

        Dim total As Integer

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("mstrid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("userid", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("docuser", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("doccont", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("docdate", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("doctaken", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("docdept", System.Type.GetType("System.String")))
        Dim drow As DataRow
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("mstrid") = .Rows(i).Item(0)
                    drow.Item("userid") = .Rows(i).Item(2)
                    drow.Item("docuser") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("doccont") = .Rows(i).Item(1).ToString().Trim()
                    If IsDBNull(.Rows(i).Item(4)) = False Then
                        drow.Item("docdate") = Format(.Rows(i).Item(4), "yy-MM-dd")
                    Else
                        drow.Item("docdate") = ""
                    End If
                    drow.Item("docdept") = .Rows(i).Item(5).ToString().Trim()

                    Dim reader As SqlDataReader
                    Dim Str As String
                    Dim txt As String = ""

                    Str = " Select conn_content,conn_user_name  " _
                           & " From KnowDB.dbo.conn_detail where conn_mstr_id='" & drow.Item("mstrid") & "' " _
                           & " order by conn_detail_id "
                    reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, Str)
                    While (reader.Read())
                        txt = txt + reader(1).ToString().Trim() & "--" + reader(0).ToString().Trim() & "<br>"
                    End While
                    reader.Close()
                    If txt.Length > 0 Then
                        drow.Item("doctaken") = txt
                    Else
                        drow.Item("doctaken") = ""
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
        If e.CommandName.CompareTo("docview") = 0 Then
            ltlAlert.Text = "window.open('conn_detail_list.aspx?mid=" & e.Item.Cells(0).Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') "
        End If
    End Sub
    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        BindData()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ltlAlert.Text = "var w=window.open('conn_exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
    End Sub

End Class

End Namespace













