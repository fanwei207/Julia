Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class ws_display1
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
                
                If Request("dd") <> Nothing Then
                    txb_date.Text = Request("dd")
                Else
                    txb_date.Text = Format(Today, "yyyy-MM-dd")
                End If

                If Request("part") <> Nothing Then
                    txb_part.Text = Request("part")
                End If

                If Request("site") <> Nothing Then
                    ddl_site.SelectedValue = Request("site")
                End If

                LoadCC()
                If Request("cc") <> Nothing Then
                    ddl_cc.SelectedValue = Request("cc")
                End If

                LoadLine()

                If Request("line") <> Nothing Then
                    ddl_line.SelectedValue = Request("line")
                End If


         
                BindData()
            End If
        End Sub
        Sub BindData()
            If txb_date.Text.Trim.Length <= 0 Or Not IsDate(txb_date.Text) Then
                txb_date.Text = Format(Today, "yyyy-MM-dd")
            End If

            Dim strPlant As String = ""
            Dim strPlantCode As String = ""
            Dim strCC As String = ""
            Dim strLine As String = ""
            Dim strLineName As String = ""
            Dim qty As Decimal = 0
            Dim qty_bad As Decimal = 0
            Dim strStatus As String = ""

            If ddl_site.SelectedValue = 1 Then
                StrSql = " select sum(isnull(ls_qty,0)) from SZXWS.LineData_SZX.dbo.ls_data "
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant =1 "
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and ls_cc ='" & ddl_cc.SelectedValue & "' "
                End If
                If ddl_line.SelectedIndex > 0 Then
                    StrSql &= " and ls_line ='" & ddl_line.SelectedValue & "' "
                End If
                If txb_part.Text.Trim.Length > 0 Then
                    StrSql &= " and ls_part like '" & txb_part.Text.Trim.Replace("*", "%") & "' "
                End If
                StrSql &= " and year(createdDate)=year('" & txb_date.Text & "') and month(createdDate)=month('" & txb_date.Text & "') and day(createdDate)=day('" & txb_date.Text & "') "
                Try
                    qty = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
                Catch
                    qty = 0
                End Try

                StrSql = " select 1,N'上海振欣 SZX', ls_cc,ls_line,ls_linename,isnull(ls_qty,0),isnull(ls_status,N'正品') from SZXWS.LineData_SZX.dbo.ls_data "
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant =1 "
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and ls_cc ='" & ddl_cc.SelectedValue & "' "
                End If
                If ddl_line.SelectedIndex > 0 Then
                    StrSql &= " and ls_line ='" & ddl_line.SelectedValue & "' "
                End If
                If txb_part.Text.Trim.Length > 0 Then
                    StrSql &= " and ls_part like '" & txb_part.Text.Trim().Replace("*", "%") & "' "
                End If

                StrSql &= " and year(createdDate)=year('" & txb_date.Text & "') and month(createdDate)=month('" & txb_date.Text & "') and day(createdDate)=day('" & txb_date.Text & "') "
            ElseIf ddl_site.SelectedValue = 2 Then
                StrSql = " select sum(isnull(ls_qty,0)) from ZQLWS.LineData_ZQL.dbo.ls_data "
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant =2 "
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and ls_cc ='" & ddl_cc.SelectedValue & "' "
                End If
                If ddl_line.SelectedIndex > 0 Then
                    StrSql &= " and ls_line ='" & ddl_line.SelectedValue & "' "
                End If
                If txb_part.Text.Trim.Length > 0 Then
                    StrSql &= " and ls_part like '" & txb_part.Text.Trim.Replace("*", "%") & "' "
                End If
                StrSql &= " and year(createdDate)=year('" & txb_date.Text & "') and month(createdDate)=month('" & txb_date.Text & "') and day(createdDate)=day('" & txb_date.Text & "') "
                Try
                    qty = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
                Catch
                    qty = 0
                End Try


                StrSql = " select 2,N'镇江强凌 ZQL',ls_cc,ls_line,ls_linename,isnull(ls_qty,0),isnull(ls_status,N'正品') from ZQLWS.LineData_ZQL.dbo.ls_data "
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant =2 "
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and ls_cc ='" & ddl_cc.SelectedValue & "' "
                End If
                If ddl_line.SelectedIndex > 0 Then
                    StrSql &= " and ls_line ='" & ddl_line.SelectedValue & "' "
                End If
                If txb_part.Text.Trim.Length > 0 Then
                    StrSql &= " and ls_part like '" & txb_part.Text.Trim().Replace("*", "%") & "' "
                End If
                StrSql &= " and year(createdDate)=year('" & txb_date.Text & "') and month(createdDate)=month('" & txb_date.Text & "') and day(createdDate)=day('" & txb_date.Text & "' )"
            ElseIf ddl_site.SelectedValue = 5 Then
                StrSql = " select sum(isnull(ls_qty,0)) from YQLWS.LineData_YQL.dbo.ls_data "
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant =5 "
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and ls_cc ='" & ddl_cc.SelectedValue & "' "
                End If
                If ddl_line.SelectedIndex > 0 Then
                    StrSql &= " and ls_line ='" & ddl_line.SelectedValue & "' "
                End If
                If txb_part.Text.Trim.Length > 0 Then
                    StrSql &= " and ls_part like '" & txb_part.Text.Trim.Replace("*", "%") & "' "
                End If
                StrSql &= " and year(createdDate)=year('" & txb_date.Text & "') and month(createdDate)=month('" & txb_date.Text & "') and day(createdDate)=day('" & txb_date.Text & "') "
                Try
                    qty = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
                Catch
                    qty = 0
                End Try

                StrSql = " select 5,N'扬州强凌 YQL', ls_cc,ls_line,ls_linename,isnull(ls_qty,0),isnull(ls_status,N'正品') from YQLWS.LineData_YQL.dbo.ls_data "
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant =5 "
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and ls_cc ='" & ddl_cc.SelectedValue & "' "
                End If
                If ddl_line.SelectedIndex > 0 Then
                    StrSql &= " and ls_line ='" & ddl_line.SelectedValue & "' "
                End If
                If txb_part.Text.Trim.Length > 0 Then
                    StrSql &= " and ls_part like '" & txb_part.Text.Trim().Replace("*", "%") & "' "
                End If
                StrSql &= " and year(createdDate)=year('" & txb_date.Text & "') and month(createdDate)=month('" & txb_date.Text & "') and day(createdDate)=day('" & txb_date.Text & "')"
            End If
            StrSql &= " order by ls_status "

             'Response.Write(StrSql)
            'Exit Sub


            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)


            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("group_pid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_lid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_line", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_total", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_bad", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("group_pass", System.Type.GetType("System.Decimal")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If strStatus <> "" And strStatus <> .Rows(i).Item(6).ToString().Trim() And qty <> 0 Then
                            drow = dtl.NewRow()
                            drow.Item("group_site") = strPlant
                            If ddl_cc.SelectedIndex > 0 Then
                                drow.Item("group_cc") = strCC
                            End If
                            If ddl_line.SelectedIndex > 0 Then
                                drow.Item("group_line") = strLineName
                            End If
                            drow.Item("group_total") = Format(qty_bad, "##0.##")
                            drow.Item("group_bad") = strStatus
                            drow.Item("group_pass") = qty_bad / qty * 100

                            drow.Item("group_pid") = strPlantCode
                            drow.Item("group_lid") = strLine

                            dtl.Rows.Add(drow)
                            strPlant = ""
                            strCC = ""
                            strStatus = ""
                            qty_bad = 0
                        End If
                        strPlantCode = .Rows(i).Item(0).ToString().Trim()
                        strPlant = .Rows(i).Item(1).ToString().Trim()
                        strCC = .Rows(i).Item(2).ToString().Trim()
                        strLine = .Rows(i).Item(3).ToString().Trim()
                        strLineName = .Rows(i).Item(4).ToString().Trim()
                        strStatus = .Rows(i).Item(6).ToString.Trim
                        qty_bad = qty_bad + .Rows(i).Item(5)
                    Next
                End If
            End With
            ds.Reset()

            If strStatus <> "" And qty <> 0 Then
                drow = dtl.NewRow()
                drow.Item("group_site") = strPlant
                If ddl_cc.SelectedIndex > 0 Then
                    drow.Item("group_cc") = strCC
                End If
                If ddl_line.SelectedIndex > 0 Then
                    drow.Item("group_line") = strLineName
                End If

                drow.Item("group_total") = Format(qty_bad, "##0.##")
                drow.Item("group_bad") = strStatus
                drow.Item("group_pass") = qty_bad / qty * 100
                drow.Item("group_pid") = strPlantCode
                drow.Item("group_lid") = strLine

                dtl.Rows.Add(drow)
            End If
            Dim dvw As DataView
            dvw = New DataView(dtl)
            dvw.Sort = "group_pass desc"


            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()
        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            Dim str As String = ""
            If e.CommandName.CompareTo("proc_detail") = 0 Then
                str = "ws_display_detail.aspx?dd=" & txb_date.Text & "&cc=" & e.Item.Cells(3).Text & "&site=" & e.Item.Cells(0).Text & "&line=" & e.Item.Cells(1).Text & "&part=" & txb_part.Text
                Response.Redirect(str)
            ElseIf e.CommandName.CompareTo("proc_statistics") = 0 Then
                str = "ws_statistics.aspx?dd=" & txb_date.Text & "&cc=" & e.Item.Cells(3).Text & "&site=" & e.Item.Cells(0).Text & "&line=" & e.Item.Cells(1).Text & "&part=" & txb_part.Text
                Response.Redirect(str)
            End If
        End Sub

        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            BindData()
        End Sub

        Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
             Response.Redirect("ws_display.aspx?dd=" & Request("dd") & "&part=" & Request("part"))
        End Sub

        Private Sub LoadCC()
            While ddl_cc.Items.Count > 0
                ddl_cc.Items.RemoveAt(0)
            End While
            While ddl_line.Items.Count > 0
                ddl_line.Items.RemoveAt(0)
            End While

            Dim ls As ListItem
            Dim StrSql As String
            Dim reader As SqlDataReader

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            ddl_cc.Items.Add(ls)

            If ddl_site.SelectedValue = 1 Then
                StrSql = "select ls_cc_no,ls_cc_name  from SZXWS.LineData_SZX.dbo.ls_cc where ls_cc_plant='" & ddl_site.SelectedValue & "'"
            ElseIf ddl_site.SelectedValue = 2 Then
                StrSql = "select ls_cc_no,ls_cc_name  from ZQLWS.LineData_ZQL.dbo.ls_cc where ls_cc_plant='" & ddl_site.SelectedValue & "'"
            Else
                StrSql = "select ls_cc_no,ls_cc_name  from YQLWS.LineData_YQL.dbo.ls_cc where ls_cc_plant='" & ddl_site.SelectedValue & "'"
            End If
            'Response.Write(strSql)
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                ddl_cc.Items.Add(ls)
            End While
            reader.Close()
        End Sub

        Private Sub LoadLine()
            While ddl_line.Items.Count > 0
                ddl_line.Items.RemoveAt(0)
            End While

            If ddl_cc.SelectedIndex = 0 Then
                Exit Sub
            End If

            Dim ls As ListItem
            Dim StrSql As String
            Dim reader As SqlDataReader

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            ddl_line.Items.Add(ls)

            If ddl_site.SelectedValue = 1 Then
                StrSql = "select ls_line_id,ls_line_name  from SZXWS.LineData_SZX.dbo.ls_line where ls_line_plant='" & ddl_site.SelectedValue & "' and ls_line_cc='" & ddl_cc.SelectedValue & "'"


            ElseIf ddl_site.SelectedValue = 2 Then
                StrSql = "select ls_line_id,ls_line_name  from ZQLWS.LineData_ZQL.dbo.ls_line where ls_line_plant='" & ddl_site.SelectedValue & "' and ls_line_cc='" & ddl_cc.SelectedValue & "'"
            Else
                StrSql = "select ls_line_id,ls_line_name  from YQLWS.LineData_YQL.dbo.ls_line where ls_line_plant='" & ddl_site.SelectedValue & "' and ls_line_cc='" & ddl_cc.SelectedValue & "'"
            End If
            'Response.Write(strSql)
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                ddl_line.Items.Add(ls)
            End While
            reader.Close()
        End Sub

        Protected Sub ddl_site_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_site.SelectedIndexChanged
            LoadCC()
        End Sub

        Protected Sub ddl_cc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_cc.SelectedIndexChanged
            LoadLine()
        End Sub
        
      
    End Class

End Namespace













