Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class ws_display_detail
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim nRet As Integer
        Dim item As ListItem


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
            ltlAlert.Text = ""
            If Not IsPostBack Then
               
                txb_date.Text = Request("dd")
                ddl_site.SelectedValue = Request("site")

                Dim ls As ListItem
                Dim StrSql As String
                Dim reader As SqlDataReader

                ls = New ListItem
                ls.Value = 0
                ls.Text = "--"
                ddl_pt.Items.Add(ls)

                If ddl_site.SelectedValue = 1 Then
                    StrSql = "select ls_pt_id,ls_pt_name  from SZXWS.LineData_SZX.dbo.ls_point where ls_pt_plant='" & ddl_site.SelectedValue & "' and ls_pt_cc='" & Request("cc") & "' and ls_pt_line='" & Request("line") & "'"
                ElseIf ddl_site.SelectedValue = 2 Then
                    StrSql = "select ls_pt_id,ls_pt_name  from ZQLWS.LineData_ZQL.dbo.ls_point where ls_pt_plant='" & ddl_site.SelectedValue & "' and ls_pt_cc='" & Request("cc") & "' and ls_pt_line='" & Request("line") & "'"
                Else
                    StrSql = "select ls_pt_id,ls_pt_name  from YQLWS.LineData_YQL.dbo.ls_point where ls_pt_plant='" & ddl_site.SelectedValue & "' and ls_pt_cc='" & Request("cc") & "' and ls_pt_line='" & Request("line") & "'"
                End If
                'Response.Write(strSql)
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                While (reader.Read())
                    ls = New ListItem
                    ls.Value = reader(0)
                    ls.Text = reader(1).ToString.Trim
                    ddl_pt.Items.Add(ls)
                End While
                reader.Close()

                ls = New ListItem
                ls.Value = 0
                ls.Text = "--"
                ddl_st.Items.Add(ls)

                If ddl_site.SelectedValue = 1 Then
                    StrSql = "select distinct ls_st_name  from SZXWS.LineData_SZX.dbo.ls_status where ls_st_plant='" & ddl_site.SelectedValue & "' and ls_st_cc='" & Request("cc") & "' and ls_st_line='" & Request("line") & "' "
                ElseIf ddl_site.SelectedValue = 2 Then
                    StrSql = "select distinct ls_st_name  from ZQLWS.LineData_ZQL.dbo.ls_status where ls_st_plant='" & ddl_site.SelectedValue & "' and  ls_st_cc='" & Request("cc") & "' and ls_st_line='" & Request("line") & "' "
                Else
                    StrSql = "select distinct ls_st_name  from YQLWS.LineData_YQL.dbo.ls_status where ls_st_plant='" & ddl_site.SelectedValue & "' and  ls_st_cc='" & Request("cc") & "' and ls_st_line='" & Request("line") & "' "
                End If
                'Response.Write(strSql)
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                Dim i As Integer = 1
                While (reader.Read())
                    ls = New ListItem
                    ls.Value = i
                    ls.Text = reader(0).ToString.Trim
                    ddl_st.Items.Add(ls)
                    i = i + 1
                End While
                reader.Close()

                BindData()
            End If
        End Sub
        Sub BindData()
            If txb_date.Text.Trim.Length <= 0 Or Not IsDate(txb_date.Text) Then
                txb_date.Text = Format(Today, "yyyy-MM-dd")
            End If

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("group_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_line", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_pt", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_total", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_user", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_item", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_desc", System.Type.GetType("System.String")))


            Dim total1 As Decimal = 0
            Dim total2 As Decimal = 0

            If ddl_site.SelectedValue = 1 Then
                StrSql = " select ls_id,ls_line,ls_cc,ls_linename,ls_pointname,ls_part,ls_desc, isnull(ls_qty,0),isnull(ls_status,N'正品'),createdBy,createdDate,ls_nbr,ls_lot from SZXWS.LineData_SZX.dbo.ls_data "
            ElseIf ddl_site.SelectedValue = 2 Then
                StrSql = " select ls_id,ls_line,ls_cc,ls_linename,ls_pointname,ls_part,ls_desc, isnull(ls_qty,0),isnull(ls_status,N'正品'),createdBy,createdDate,ls_nbr,ls_lot from ZQLWS.LineData_ZQL.dbo.ls_data "
            Else
                StrSql = " select ls_id,ls_line,ls_cc,ls_linename,ls_pointname,ls_part,ls_desc, isnull(ls_qty,0),isnull(ls_status,N'正品'),createdBy,createdDate,ls_nbr,ls_lot from YQLWS.LineData_YQL.dbo.ls_data "
            End If

            StrSql &= " where deletedBy is null "
            StrSql &= " and ls_plant = '" & Request("site") & "'"
            StrSql &= " and ls_cc ='" & Request("cc") & "' "
            StrSql &= " and ls_line ='" & Request("line") & "' "
            If Request("part") <> Nothing And Request("part").ToString.Trim.Length > 0 Then
                StrSql &= " and ls_part like '" & Request("part").Replace("*", "%") & "' "
            End If
            StrSql &= " and year(createdDate)=year('" & txb_date.Text & "') and month(createdDate)=month('" & txb_date.Text & "') and day(createdDate)=day('" & txb_date.Text & "') "
            If ddl_pt.SelectedIndex > 0 Then
                StrSql &= " and ls_point = '" & ddl_pt.SelectedValue & "'"
            End If
            If ddl_st.SelectedIndex > 0 Then
                StrSql &= " and ls_status = N'" & ddl_st.SelectedItem.Text & "'"
            End If
            StrSql &= " order by ls_cc,ls_line,createddate desc"

            Session("EXTitle") = "80^<b>成本中心</b>~^130^<b>工段线</b>~^130^<b>采样点</b>~^130^<b>零件号</b>~^250^<b>描述</b>~^<b>数量</b>~^150^<b>状态</b>~^50^<b>创建人</b>~^<b>日期</b>~^<b>加工单</b>~^<b>加工单ID</b>~^"
            Session("EXSQL") = StrSql
            Session("EXHeader") = "流水线动态数据    导出日期: " & Format(DateTime.Today, "yyyy-MM-dd")


            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            'Response.Write(StrSql)
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("group_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("group_site") = ddl_site.SelectedItem.Text
                        drow.Item("group_cc") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("group_line") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("group_pt") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("group_item") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("group_desc") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("group_total") = Format(.Rows(i).Item(7), "##0.##")
                        drow.Item("group_status") = .Rows(i).Item(8).ToString().Trim()
                        drow.Item("group_user") = .Rows(i).Item(9).ToString().Trim()
                        drow.Item("group_date") = .Rows(i).Item(10).ToString().Trim()
                        dtl.Rows.Add(drow)
                        If .Rows(i).Item(8).ToString().Trim() <> "正品" Then
                            total2 = total2 + .Rows(i).Item(7)
                        Else
                            total1 = total1 + .Rows(i).Item(7)
                        End If
                    Next
                End If

            End With
            ds.Reset()

            If total1 <> 0 Then
                lbl_qty.Text = "流量：" & Format(total1, "##0.##") & " &nbsp;&nbsp;&nbsp;次品：" & Format(total2, "##0.##") & " &nbsp;&nbsp;&nbsp;一次合格率： " & Format((1 - (total2 / total1)) * 100, "##0.##")
            Else
                lbl_qty.Text = "流量：" & Format(total1, "##0.##") & " &nbsp;&nbsp;&nbsp;次品：" & Format(total2, "##0.##") & " &nbsp;&nbsp;&nbsp;一次合格率： " & Format(0, "##0.##")
            End If

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()
        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            Dim str As String = ""
            If e.CommandName.CompareTo("proc_del") = 0 Then

                If ddl_site.SelectedValue = 1 Then
                    str = "update SZXWS.LineData_SZX.dbo.ls_data set deleteddate=getdate(),deletedBy=N'" & Session("uName") & "' where deletedBy is null and createdBy=N'" & Session("uName") & "' and ls_id='" & e.Item.Cells(0).Text & "'"
                ElseIf ddl_site.SelectedValue = 2 Then
                    str = "update ZQLWS.LineData_ZQL.dbo.ls_data set deleteddate=getdate(),deletedBy=N'" & Session("uName") & "' where deletedBy is null and createdBy=N'" & Session("uName") & "' and ls_id='" & e.Item.Cells(0).Text & "'"
                Else
                    str = "update YQLWS.LineData_YQL.dbo.ls_data set deleteddate=getdate(),deletedBy=N'" & Session("uName") & "' where deletedBy is null and createdBy=N'" & Session("uName") & "' and ls_id='" & e.Item.Cells(0).Text & "'"
                End If

                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str)

                BindData()
            End If
        End Sub

        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub

        Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('/public/exportexcel.aspx?ty=2&uid=" & Session("uID") & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub

        Protected Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
            Response.Redirect("ws_display.aspx?dd=" & Request("dd") & "&part=" & Request("part"))
        End Sub

        Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing ButtonColumn
                    myDeleteButton = e.Item.Cells(8)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除吗?');")

                Case ListItemType.AlternatingItem
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(8)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除吗?');")
                Case ListItemType.EditItem
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(8)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除吗?');")
            End Select

        End Sub

       

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()

        End Sub


    End Class

End Namespace













