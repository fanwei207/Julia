Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class rm_list
        Inherits System.Web.UI.Page
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

                BindSite()
                BindType()
                BindSubType(ddl_type.SelectedValue)
                BindData()
            End If
        End Sub

        Sub BindData()

            StrSql = "select  conn_mstr_id,conn_content,conn_user_id,conn_user_name,conn_date,conn_dept, conn_site, m.conn_typeid, mt.conn_typename, m.conn_subtypeid, st.conn_subtypename"
            StrSql &= " from rmInspection.dbo.conn_mstr m Left Join rmInspection.dbo.conn_mstrtype mt on m.conn_typeid = mt.conn_typeid Left Join rmInspection.dbo.conn_mstrsubtype st on m.conn_subtypeid=st.conn_subtypeid"
            StrSql &= " where conn_closeddate is null and conn_deleteddate is null "

            If ddl_site.SelectedIndex > 0 Then
                StrSql &= " and conn_site = " & ddl_site.SelectedValue.ToString()
            End If

            If ddl_type.SelectedIndex > 0 Then
                StrSql &= " and m.conn_typeid  = " & ddl_type.SelectedValue.ToString()
            End If

            If ddl_subtype.SelectedIndex > 0 Then
                StrSql &= " and m.conn_subtypeid  = '" & ddl_subtype.SelectedValue.ToString() & "' "
            End If

            If txb_uname.Text.Trim.Length > 0 Then
                StrSql &= " and conn_user_name like N'%" & txb_uname.Text.Trim & "%' "
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
            StrSql &= " order by conn_date desc "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

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
            dtl.Columns.Add(New DataColumn("conn_subtypeid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("conn_subtypename", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        Dim reader As SqlDataReader
                        Dim Str As String
                        Dim txt As String = ""

                        If CheckBox1.Checked = True And Session("uRole") <> 1 Then
                            Str = " Select conn_taken_name " _
                                   & " From rmInspection.dbo.conn_taken  where conn_mstr_id='" & .Rows(i).Item(0) & "' " _
                                   & " and conn_taken_date is null and conn_type = 1 and conn_taken_id='" & Session("uID") & "' order by conn_date,conn_resp_id "
                        Else
                            Str = " Select conn_taken_name " _
                                        & " From rmInspection.dbo.conn_taken  where conn_mstr_id='" & .Rows(i).Item(0) & "' " _
                                        & " and conn_taken_date is null and conn_type = 1 order by conn_date,conn_resp_id "
                        End If
                        reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, Str)
                        While (reader.Read())
                            txt = txt + "," + reader(0).ToString().Trim()
                        End While
                        reader.Close()

                        If txt.Length > 0 Then
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

                            drow.Item("doctaken") = txt.Substring(1)

                            drow.Item("conn_site") = .Rows(i).Item("conn_site").ToString().Trim()
                            drow.Item("conn_typeid") = .Rows(i).Item("conn_typeid").ToString().Trim()
                            drow.Item("conn_typename") = .Rows(i).Item("conn_typename").ToString().Trim()
                            drow.Item("conn_subtypeid") = .Rows(i).Item("conn_subtypeid").ToString().Trim()
                            drow.Item("conn_subtypename") = .Rows(i).Item("conn_subtypename").ToString().Trim()
                            dtl.Rows.Add(drow)
                        Else
                            If CheckBox1.Checked = False Or Session("uRole") = 1 Then
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
                                drow.Item("doctaken") = "" 

                                drow.Item("conn_site") = .Rows(i).Item("conn_site").ToString().Trim()
                                drow.Item("conn_typeid") = .Rows(i).Item("conn_typeid").ToString().Trim()
                                drow.Item("conn_typename") = .Rows(i).Item("conn_typename").ToString().Trim()
                                drow.Item("conn_subtypeid") = .Rows(i).Item("conn_subtypeid").ToString().Trim()
                                drow.Item("conn_subtypename") = .Rows(i).Item("conn_subtypename").ToString().Trim()
                                dtl.Rows.Add(drow)
                            End If
                        End If
                    Next

                End If
            End With
            ds.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.CurrentPageIndex = 0
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

        Sub BindSubType(typeid As String)
            StrSql = "rmInspection.dbo.usp_rm_selectMstrSubtype"
            Dim params(1) As SqlParameter
            params(0) = New SqlParameter("@typeid", typeid)
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.StoredProcedure, StrSql, params)

            ddl_subtype.DataSource = ds
            ddl_subtype.DataBind()
            Dim item As ListItem = New ListItem("--", "0")
            ddl_subtype.Items.Insert(0, item)

        End Sub


        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("docview") = 0 Then
                ltlAlert.Text = "window.open('rm_detail_list.aspx?mid=" & e.Item.Cells(0).Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') "
            ElseIf e.CommandName.CompareTo("doctake") = 0 Then
                Response.Redirect("rm_app3.aspx?mid=" & e.Item.Cells(0).Text.Trim())
            ElseIf e.CommandName.CompareTo("docdel") = 0 Then
                If e.Item.Cells(1).Text.Trim() <> Session("uID") Then
                    ltlAlert.Text = "alert('你不能删除他人创建的项目！')"
                    Exit Sub
                End If

                StrSql = "select top 1 conn_detail_id from rmInspection.dbo.conn_detail where conn_mstr_id='" & e.Item.Cells(0).Text.Trim() & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                    ltlAlert.Text = "alert('你不能删除已有会签的项目！')"
                    Exit Sub
                End If

                StrSql = "update rmInspection.dbo.conn_mstr set conn_deleteddate=getdate()  where conn_user_id='" & Session("uID") & "' and conn_closeddate is null and conn_deleteddate is null and conn_mstr_id = '" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                BindData()

            End If
        End Sub
        Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
            BindData()
        End Sub

        Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
            Response.Redirect("rm_app2.aspx")
        End Sub

        Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            BindData()
        End Sub

        Protected Sub btnHis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHis.Click
            Response.Redirect("rm_hist_list.aspx")
        End Sub

        Protected Sub btnRpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRpt.Click
            ltlAlert.Text = "var w=window.open('rm_exportExcel.aspx?ty=1&rm=" & Now.ToLongTimeString() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub

        Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            If DropDownList1.SelectedValue = 1 Then
                ltlAlert.Text = "var w=window.open('TCP CHINA不合格品控制程序.doc','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
            ElseIf DropDownList1.SelectedValue = 2 Then
                ltlAlert.Text = "var w=window.open('TCP CHINA不合格品报告.xls','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
            End If
            DropDownList1.SelectedValue = 0
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged

            Datagrid1.CurrentPageIndex = e.NewPageIndex

            BindData()
        End Sub

        Protected Sub ddl_type_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_type.SelectedIndexChanged
            BindSubType(ddl_type.SelectedValue)
        End Sub
    End Class

End Namespace













