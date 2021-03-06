Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class rm_hist_list
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

                txtDate1.Text = Format(Today, "yy-MM-01")
                txtDate2.Text = Format(Today, "yy-MM-dd")

                BindSite()
                BindType()
                BindData()
            End If
        End Sub

        Sub BindData() 

            StrSql = "select conn_mstr_id,conn_content,conn_user_id,conn_user_name,conn_date,conn_dept, conn_site, m.conn_typeid, mt.conn_typename "
            StrSql &= " from rmInspection.dbo.conn_mstr  m Left Join rmInspection.dbo.conn_mstrtype mt on m.conn_typeid = mt.conn_typeid "
            StrSql &= "   where conn_closeddate is not null and conn_deleteddate is null"

            If ddl_site.SelectedIndex > 0 Then
                StrSql &= " and conn_site = " & ddl_site.SelectedValue.ToString()
            End If

            If ddl_type.SelectedIndex > 0 Then
                StrSql &= " and m.conn_typeid  = " & ddl_type.SelectedValue.ToString()
            End If

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
            dtl.Columns.Add(New DataColumn("conn_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("conn_typeid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("conn_typename", System.Type.GetType("System.String")))
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
                            drow.Item("docdate") = Format(.Rows(i).Item(4), "yy-MM-dd HH:mm:ss")
                        Else
                            drow.Item("docdate") = ""
                        End If
                        drow.Item("docdept") = .Rows(i).Item(5).ToString().Trim()

                        Dim reader As SqlDataReader
                        Dim Str As String
                        Dim txt As String = ""

                        Str = " Select conn_content,conn_user_name  " _
                               & " From rmInspection.dbo.conn_detail where conn_mstr_id='" & drow.Item("mstrid") & "' " _
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
                        drow.Item("conn_site") = .Rows(i).Item("conn_site").ToString().Trim()
                        drow.Item("conn_typeid") = .Rows(i).Item("conn_typeid").ToString().Trim()
                        drow.Item("conn_typename") = .Rows(i).Item("conn_typename").ToString().Trim()
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

        Sub BindSite()
            StrSql = "SELECT (si_site +' -- '+ si_domain)as si_sitedomain, si_desc  ,si_site  FROM QAD_Data.dbo.si_mstr where right(si_site,3) = '000' order by si_site "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            ddl_site.DataSource = ds
            ddl_site.DataBind()
            Dim item As ListItem = New ListItem("--", "0")
            ddl_site.Items.Insert(0, item)

        End Sub

        Sub BindType()
            StrSql = "SELECT  conn_typeid  , conn_typename FROM  rmInspection.dbo.conn_mstrtype"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            ddl_type.DataSource = ds
            ddl_type.DataBind()
            Dim item As ListItem = New ListItem("--", "0")
            ddl_type.Items.Insert(0, item)

        End Sub

        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("docview") = 0 Then
                ltlAlert.Text = "window.open('rm_detail_list.aspx?mid=" & e.Item.Cells(0).Text.Trim() & "&rm=" & Now.ToLongTimeString() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') "
            End If
        End Sub
        Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
            BindData()
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            ltlAlert.Text = "var w=window.open('rm_exportExcel.aspx?ty=2&d1=" & txtDate1.Text & "&d2=" & txtDate2.Text & "&rm=" & Now.ToLongTimeString() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub

        Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
            Response.Redirect("rm_list.aspx?rm=" & Now.ToLongTimeString())
        End Sub

        Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
            ltlAlert.Text = "var w=window.open('rm_exportExcel.aspx?ty=3&d1=" & txtDate1.Text & "&d2=" & txtDate2.Text & "&rm=" & Now.ToLongTimeString() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex

            BindData()
        End Sub
    End Class

End Namespace













