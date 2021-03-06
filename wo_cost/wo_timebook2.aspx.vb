Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Text.RegularExpressions
Imports WO2Group


Namespace tcpc
    Partial Class wo_timebook2
        Inherits BasePage
        Public chk As New adamClass
        Public hr_salary As New WO2
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
                Dim ls As ListItem
                Select Case Session("PlantCode")
                    Case "1"
                        ls = New ListItem
                        ls.Value = "1000"
                        ls.Text = "1000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2100"
                        ls.Text = "2100"
                        dd_site.Items.Add(ls)
                    Case "2"
                        ls = New ListItem
                        ls.Value = "2000"
                        ls.Text = "2000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1200"
                        ls.Text = "1200"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "3000"
                        ls.Text = "3000"
                        dd_site.Items.Add(ls)
                    Case "5"
                        ls = New ListItem
                        ls.Value = "4000"
                        ls.Text = "4000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1400"
                        ls.Text = "1400"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2400"
                        ls.Text = "2400"
                        dd_site.Items.Add(ls)
                End Select


                If Request("cc") <> Nothing And Request("site") <> Nothing And Request("dd") <> Nothing Then
                    txb_cc.Text = Request("cc")
                    txb_date.Text = Request("dd")
                    dd_site.SelectedValue = Request("site")
                    getWOInfo()
                End If

                txb_date.Text = Format(Today, "yyyy-MM-dd")

                BindData()
            End If
        End Sub

        Sub BindData()
            StrSql = " select a.tb_id,a.tb_user_id,a.tb_cc ,a.tb_date ,a.tb_user_no,a.tb_user_name,a.tb_start,a.tb_end,isnull(a.tb_work,0), isnull(a.tb_rest,0), a.createdDate,a.createdName,a.tb_type,"
            StrSql &= " CASE WHEN isnull(a.tb_mid,0) + isnull(a.tb_night,0) + isnull(a.tb_whole,0) = 0 THEN '' ELSE CASE WHEN isnull(a.tb_mid,0) =1 THEN N'中班' ELSE CASE WHEN isnull(a.tb_night,0)=1 THEN N'夜班' ELSE N'全夜' END END END  from wo_timebook a "
            If Session("uRole") <> 1 Then
                StrSql &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=a.tb_cc "
            End If
            StrSql &= " where a.tb_id is not null and deletedBy is null"
            If dd_site.SelectedIndex > 0 Then
                StrSql &= " and a.tb_site ='" & dd_site.SelectedValue & "' "
            End If

            If txtUserNo.Text.Trim().Length > 0 Then
                StrSql &= " and a.tb_user_no =N'" & txtUserNo.Text.Trim() & "' "
            End If

            If txb_cc.Text.Trim.Length > 0 Then
                StrSql &= " and a.tb_cc ='" & txb_cc.Text.Trim() & "' "
            End If
            If txb_date.Text.Trim.Length > 0 And IsDate(txb_date.Text) Then
                If (chkAll.Checked) Then
                    StrSql &= " AND YEAR(a.tb_date) = '" & CDate(txb_date.Text.Trim()).Year.ToString() & "' AND MONTH(a.tb_date) = '" & CDate(txb_date.Text.Trim()).Month.ToString() & "' "
                Else
                    StrSql &= " and a.tb_date ='" & txb_date.Text.Trim() & "' "
                End If

            End If
            If Session("uRole") <> 1 Then
                StrSql &= "  AND (a.createdBy = '" & Session("uID") & "' OR  a.createdBy IN (SELECT worker FROM manager_worker WHERE manager='" & Session("uID") & "' ) )"
            End If

            'StrSql &= " order by a.tb_date desc,a.tb_user_no "
            StrSql &= " order by a.tb_id desc"

            Session("EXTitle") = "<b>成本中心</b>~^<b>考勤日期</b>~^<b>工号</b>~^<b>姓名</b>~^<b>上班时间</b>~^<b>下班时间</b>~^<b>工作小时</b>~^<b>休息小时</b>~^<b>日期</b>~^50^<b>创建人</b>~^<b>类型</b>~^<b>中夜班</b>~^"
            Session("EXSQL") = StrSql
            Session("EXHeader") = "工序汇报    导出日期: " & Format(DateTime.Today, "yyyy-MM-dd")


            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("user_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("user_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_start", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_end", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_work", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_rest", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_created_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_created", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_type", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("_night", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("r_No", System.Type.GetType("System.Int32")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()
                        drow.Item("_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("user_no") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("user_name") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("_cc") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("_date") = Format(.Rows(i).Item(3), "yy-MM-dd")
                        drow.Item("_start") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("_end") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("_rest") = Format(.Rows(i).Item(9), "##0.##")
                        drow.Item("_work") = Format(.Rows(i).Item(8), "##0.##")
                        If Not IsDBNull(.Rows(i).Item(10)) Then
                            drow.Item("_created_date") = Format(.Rows(i).Item(10), "yy-MM-dd")
                        End If
                        drow.Item("_created") = .Rows(i).Item(11).ToString().Trim()
                        drow.Item("_type") = .Rows(i).Item(12).ToString().Trim()
                        drow.Item("_night") = .Rows(i).Item(13).ToString().Trim()

                        drow.Item("r_No") = i + 1
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
            If e.CommandName.CompareTo("proc_del") = 0 Then
                If Session("uRole") > 1 And Session("uName") <> e.Item.Cells(13).Text Then
                    ltlAlert.Text = "alert('没有删除此数据的权限.')"
                    Exit Sub
                End If
                StrSql = "update wo_timebook set deletedDate = getdate(), deletedBy='" & Session("uID") & "' where tb_id='" & e.Item.Cells(0).Text & "'"

                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                BindData()
            End If
        End Sub
        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.CancelCommand
            Datagrid1.EditItemIndex = -1
            BindData()
        End Sub
        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.UpdateCommand
            If Session("uRole") > 1 And Session("uName") <> e.Item.Cells(13).Text Then
                ltlAlert.Text = "alert('没有修改此数据的权限.')"
                Exit Sub
            End If


            Dim strSQL As String
            Dim str1 As String = CType(e.Item.Cells(7).Controls(0), TextBox).Text
            Dim str2 As String = CType(e.Item.Cells(8).Controls(0), TextBox).Text
            Dim str3 As String = CType(e.Item.Cells(10).Controls(0), TextBox).Text

            If str1.Trim.Length <> 4 Then
                ltlAlert.Text = "alert('请输入上班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            ElseIf Not IsNumeric(str1.Trim.Substring(0, 2)) Then
                ltlAlert.Text = "alert('请输入正确的上班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            ElseIf Not IsNumeric(str1.Trim.Substring(2, 2)) Then
                ltlAlert.Text = "alert('请输入正确的上班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            ElseIf CDec(str1.Trim.Substring(0, 2)) < 0 Or CDec(str1.Trim.Substring(0, 2)) > 23 Then
                ltlAlert.Text = "alert('请输入正确的上班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            ElseIf CDec(str1.Trim.Substring(2, 2)) < 0 Or CDec(str1.Trim.Substring(0, 2)) > 59 Then
                ltlAlert.Text = "alert('请输入正确的上班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            End If

            If str2.Trim.Length <> 4 Then
                ltlAlert.Text = "alert('请输入下班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            ElseIf Not IsNumeric(str2.Trim.Substring(0, 2)) Then
                ltlAlert.Text = "alert('请输入正确的下班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            ElseIf Not IsNumeric(str2.Trim.Substring(2, 2)) Then
                ltlAlert.Text = "alert('请输入正确的下班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            ElseIf CDec(str2.Trim.Substring(0, 2)) < 0 Or CDec(str2.Trim.Substring(0, 2)) > 23 Then
                ltlAlert.Text = "alert('请输入正确的下班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            ElseIf CDec(str2.Trim.Substring(2, 2)) < 0 Or CDec(str2.Trim.Substring(0, 2)) > 59 Then
                ltlAlert.Text = "alert('请输入正确的下班时间，4位xxxx格式，前2位为几点，后2位是几分.')"
                Exit Sub
            End If

            If str3.Trim.Length < 0 Then
                ltlAlert.Text = "alert('请输入休息小时.')"
                Exit Sub
            ElseIf Not IsNumeric(str3.Trim) Then
                ltlAlert.Text = "alert('请输入正确的休息小时，必须是浮点数.')"
                Exit Sub
            End If

            Dim night(2) As Integer
            Dim onwork As Decimal = CInt(str1.Substring(0, 2)) + CInt(str1.Substring(2, 2)) / 60
            Dim offwork As Decimal = CInt(str2.Substring(0, 2)) + CInt(str2.Substring(2, 2)) / 60
            Dim restwork As Decimal = CDec(str3)

            Dim wk As Decimal = 0
            wk = offwork - onwork - restwork

            If wk < 0 Then
                wk = wk + 24
                offwork = offwork + 24
            End If
            Array.Clear(night, 0, 3)

            night(0) = 0
            night(1) = 0
            night(2) = getNight(onwork, offwork, wk, 2)

            If night(2) = 0 Then
                night(1) = getNight(onwork, offwork, wk, 1)
                If night(1) = 0 Then
                    night(0) = getNight(onwork, offwork, wk, 0)
                End If
            End If

            strSQL = "update wo_timebook set tb_start = '" & str1 & "', tb_end='" & str2 & "',tb_rest='" & str3 & "',tb_work='" & wk & "',tb_mid='" & night(0) & "',tb_night='" & night(1) & "',tb_whole='" & night(2) & "' where tb_id='" & e.Item.Cells(0).Text & "'"
            'Response.Write(strSQL)
            'Exit Sub

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            Datagrid1.EditItemIndex = -1
            BindData()
        End Sub
        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.EditCommand
            Datagrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
            If Session("uRole") <> 1 Then
                StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & txb_cc.Text & "' and perm_userid='" & Session("uID") & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                    ltlAlert.Text = "alert('无操作此成本中心的权限.')"
                    Exit Sub
                End If
            End If

            If txb_start.Text.Trim.Length <> 4 Then
                ltlAlert.Text = "alert('请正确输入上班时间。格式为4位数字[例:0830表示八点半]，前2位是小时，后2位是分钟；不足两位数的请补0.')"
                Exit Sub
            ElseIf Not IsNumeric(txb_start.Text.Trim.Substring(0, 2)) Then
                ltlAlert.Text = "alert('上班时间的前2位必须是数字.')"
                Exit Sub
            ElseIf Not IsNumeric(txb_start.Text.Trim.Substring(2, 2)) Then
                ltlAlert.Text = "alert('上班时间的后2位必须是数字.')"
                Exit Sub
            ElseIf CDec(txb_start.Text.Trim.Substring(0, 2)) < 0 Or CDec(txb_start.Text.Trim.Substring(0, 2)) > 23 Then
                ltlAlert.Text = "alert('上班时间的前2位必须在0至23之间.')"
                Exit Sub
            ElseIf CDec(txb_start.Text.Trim.Substring(2, 2)) < 0 Or CDec(txb_start.Text.Trim.Substring(2, 2)) > 59 Then
                ltlAlert.Text = "alert('上班时间的后2位必须在0至59之间.')"
                Exit Sub
            End If

            If txb_end.Text.Trim.Length <> 4 Then
                ltlAlert.Text = "alert('请正确输入下班时间。格式为4位数字[例:0830表示八点半]，前2位是小时，后2位是分钟；不足两位数的请补0.')"
                Exit Sub
            ElseIf Not IsNumeric(txb_end.Text.Trim.Substring(0, 2)) Then
                ltlAlert.Text = "alert('下班时间的前2位必须是数字.')"
                Exit Sub
            ElseIf Not IsNumeric(txb_end.Text.Trim.Substring(2, 2)) Then
                ltlAlert.Text = "alert('下班时间的后2位必须是数字')"
                Exit Sub
            ElseIf CDec(txb_end.Text.Trim.Substring(0, 2)) < 0 Or CDec(txb_end.Text.Trim.Substring(0, 2)) > 23 Then
                ltlAlert.Text = "alert('下班时间的前2位必须在0至23之间.')"
                Exit Sub
            ElseIf CDec(txb_end.Text.Trim.Substring(2, 2)) < 0 Or CDec(txb_end.Text.Trim.Substring(2, 2)) > 59 Then
                ltlAlert.Text = "alert('下班时间的后2位必须在0至59之间.')"
                Exit Sub
            End If

            If txb_rest.Text.Trim.Length < 0 Then
                ltlAlert.Text = "alert('请输入休息小时.')"
                Exit Sub
            ElseIf Not IsNumeric(txb_rest.Text.Trim) Then
                ltlAlert.Text = "alert('请输入正确的休息小时，必须是浮点数.')"
                Exit Sub
            End If

            If txb_date.Text.Trim.Length < 0 Then
                ltlAlert.Text = "alert('请输入考勤日期.')"
                Exit Sub
            ElseIf Not IsDate(txb_date.Text.Trim) Then
                ltlAlert.Text = "alert('请输入正确的考勤日期.')"
                Exit Sub
            End If

            Dim night(2) As Integer
            Dim onwork As Decimal = CInt(txb_start.Text.Substring(0, 2)) + CInt(txb_start.Text.Substring(2, 2)) / 60
            Dim offwork As Decimal = CInt(txb_end.Text.Substring(0, 2)) + CInt(txb_end.Text.Substring(2, 2)) / 60
            Dim restwork As Decimal = CDec(txb_rest.Text)

            Dim wk As Decimal = 0
            wk = offwork - onwork - restwork

            If wk < 0 Then
                wk = wk + 24
                offwork = offwork + 24
            End If
            Array.Clear(night, 0, 3)

            night(0) = 0
            night(1) = 0
            night(2) = getNight(onwork, offwork, wk, 2)

            If night(2) = 0 Then
                night(1) = getNight(onwork, offwork, wk, 1)
                If night(1) = 0 Then
                    night(0) = getNight(onwork, offwork, wk, 0)
                End If
            End If


            If dd_group.SelectedIndex > 0 Then
                If Session("Plantcode") = 1 Then
                    StrSql = " Select wod_user_id,wod_user_no,wod_user_name,isnull(wod_rate,1),ISNULL(tb_id,0) from tcpc0.dbo.wo_group_detail"
                    StrSql &= " LEFT OUTER JOIN wo_timebook  ON tb_user_id =wod_user_id and tb_date = '" & txb_date.Text & "' and  deletedBy is null "
                    StrSql &= " where wod_group_id='" & dd_group.SelectedValue & "'"
                Else
                    StrSql = " SELECT wo2_userID , wo2_userno,wo2_username,1,ISNULL(tb_id,0) FROM wo2_group_detail INNER JOIN tcpc0.dbo.Users u ON u.userID= wo2_userID AND u.PlantCode = '" & Session("Plantcode") & "' "
                    StrSql &= " LEFT OUTER JOIN wo_timebook  ON tb_user_id =wo2_userID and tb_date = '" & txb_date.Text & "' and  deletedBy is null "
                    StrSql &= " WHERE wo2_groupID ='" & dd_group.SelectedValue & "'  AND u.deleted=0 AND u.isactive=1 AND (u.leavedate IS NULL OR u.leavedate >= '" & txb_date.Text & "') "
                End If
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If .Rows.Count > 0 Then
                        Dim i As Integer
                        Dim total As Decimal = 0
                        For i = 0 To .Rows.Count - 1
                            total = total + .Rows(i).Item(3)
                        Next

                        If total > 0 Then
                            Dim message As String = ""
                            For i = 0 To .Rows.Count - 1

                                If .Rows(i).Item(4) = 0 Then
                                    Dim StrSql2 As String = ""
                                    Try
                                        'wk = CInt(txb_end.Text.Substring(0, 2)) - CInt(txb_start.Text.Substring(0, 2)) + (CInt(txb_end.Text.Substring(2, 2)) - CInt(txb_start.Text.Substring(2, 2))) / 60 - CDec(txb_rest.Text)
                                        'If wk <= 0 Then
                                        '    wk = wk + 24
                                        'End If
                                        StrSql2 = " Insert into wo_timebook(tb_user_id,tb_user_no,tb_user_name,tb_start,tb_end,tb_rest,tb_date,createdDate,createdBy,tb_cc,tb_site,tb_type,tb_work,createdName,tb_mid,tb_night,tb_whole) "
                                        StrSql2 &= " Values('" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(1).ToString().Trim() & "',N'" & .Rows(i).Item(2).ToString().Trim() & "','" & txb_start.Text & "','" & txb_end.Text & "','" & txb_rest.Text & "','" & txb_date.Text & "',getdate(),'" & Session("uID") & "','" & txb_cc.Text & "','" & dd_site.SelectedValue & "',N'计件','" & wk & "',N'" & Session("uName") & "',"
                                        StrSql2 &= " '" & night(0) & "','" & night(1) & "','" & night(2) & "')"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql2)
                                    Catch ex As Exception
                                        Response.Write(StrSql2)
                                    End Try
                                Else
                                    message = message & .Rows(i).Item(1).ToString().Trim() & ","

                                End If

                            Next

                            If message.Trim().Length > 0 Then
                                ltlAlert.Text = "alert( '工号" & message & " 已存在当天的考勤。')"
                            End If

                        End If
                    End If
                End With
                ds.Reset()
            End If

                If txb_no.Text.Trim.Length > 0 Then

                    StrSql = "Select userid,username,ISNULL(tb_id,0) from tcpc0.dbo.users "
                    StrSql &= " LEFT OUTER JOIN wo_timebook  ON tb_user_id =userid and datediff(day,tb_date,'" & txb_date.Text & "') = 0 and deletedBy is null"
                    StrSql &= " where PlantCode='" & Session("PlantCode") & "'and userNo='" & txb_no.Text & "' and deleted=0 and isactive=1 and (leavedate is null or leavedate >= '" & txb_date.Text & "')"
                    ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                    With ds.Tables(0)
                        If .Rows.Count > 0 Then
                            'Dim i As Integer
                            'For i = 0 To .Rows.Count - 1
                            If .Rows(0).Item(2) = 0 Then
                                Dim StrSql2 As String = ""
                                Try
                                    'wk = CInt(txb_end.Text.Substring(0, 2)) - CInt(txb_start.Text.Substring(0, 2)) + (CInt(txb_end.Text.Substring(2, 2)) - CInt(txb_start.Text.Substring(2, 2))) / 60 - CDec(txb_rest.Text)
                                    'If wk <= 0 Then
                                    '    wk = wk + 24
                                    'End If

                                    StrSql2 = " Insert into wo_timebook(tb_user_id,tb_user_no,tb_user_name,tb_start,tb_end,tb_rest,tb_date,createdDate,createdBy,tb_cc,tb_site,tb_type,tb_work,createdName,tb_mid,tb_night,tb_whole) "
                                StrSql2 &= " Values('" & .Rows(0).Item(0).ToString().Trim() & "','" & txb_no.Text & "',N'" & .Rows(0).Item(1).ToString().Trim() & "','" & txb_start.Text & "','" & txb_end.Text & "','" & txb_rest.Text & "','" & txb_date.Text & "',getdate(),'" & Session("uID") & "','" & txb_cc.Text & "','" & dd_site.SelectedValue & "',N'计件','" & wk & "',N'" & Session("uName") & "',"
                                    StrSql2 &= " '" & night(0) & "','" & night(1) & "','" & night(2) & "')"
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql2)
                                Catch ex As Exception
                                    Response.Write(StrSql2)
                                End Try
                                'Next
                            Else
                                ltlAlert.Text = "alert('此工号已存在当天的考勤。')"
                                Exit Sub
                            End If
                        Else
                            ds.Reset()
                            ltlAlert.Text = "alert('工号不存在。')"
                            Exit Sub
                        End If
                    End With
                    ds.Reset()
                End If

                BindData()
                dd_group.SelectedIndex = 0
                txb_no.Text = ""
                txb_no.Focus()

        End Sub

        Protected Sub btn_woload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_woload.Click

            getWOInfo()

            Datagrid1.CurrentPageIndex = 0
            BindData()

        End Sub

        Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
            Dim strMes As String
            strMes = "工号：" & e.Item.Cells(2).Text & "   姓名：" & e.Item.Cells(3).Text
            Select Case e.Item.ItemType
                Case ListItemType.Item

                Case ListItemType.AlternatingItem

                Case ListItemType.EditItem
                    CType(e.Item.Cells(7).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(50)
                    CType(e.Item.Cells(7).Controls(0), TextBox).Height = System.Web.UI.WebControls.Unit.Pixel(18)
                    CType(e.Item.Cells(8).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(50)
                    CType(e.Item.Cells(8).Controls(0), TextBox).Height = System.Web.UI.WebControls.Unit.Pixel(18)
                    CType(e.Item.Cells(10).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(50)
                    CType(e.Item.Cells(10).Controls(0), TextBox).Height = System.Web.UI.WebControls.Unit.Pixel(18)

            End Select



        End Sub
        Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx" & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"
        End Sub

        Protected Sub btn_edit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_edit1.Click
            Response.Redirect("wo_tb_list.aspx?rm=" & Now)
        End Sub

        Function getWOInfo() As Boolean

            While dd_group.Items.Count > 0
                dd_group.Items.RemoveAt(0)
            End While
            Dim ls As ListItem

            If Session("Plantcode") = 1 Then
                Dim dm As String = ""
                If txb_cc.Text.Length > 0 Then

                    Select Case dd_site.SelectedValue
                        Case "1000"
                            dm = "szx"
                        Case "1200"
                            dm = "zql"
                        Case "1400"
                            dm = "yql"
                        Case "2000"
                            dm = "zql"
                        Case "2100"
                            dm = "szx"
                        Case "2400"
                            dm = "yql"
                        Case "3000"
                            dm = "zql"
                        Case "4000"
                            dm = "yql"
                    End Select

                    Dim reader As SqlDataReader

                    ls = New ListItem
                    ls.Value = 0
                    ls.Text = "--"
                    dd_group.Items.Add(ls)
                    StrSql = "select wog_id,wog_name from tcpc0.dbo.wo_group where deletedBy is null and wog_cc='" & txb_cc.Text & "'  order by wog_name "
                    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                    While (reader.Read())
                        ls = New ListItem
                        ls.Value = reader(0)
                        ls.Text = reader(1).ToString.Trim

                        dd_group.Items.Add(ls)
                    End While
                    reader.Close()
                End If
            Else

                Dim fs As DataTable = hr_salary.SelectGroupInfo()
                Dim i As Integer
                If fs.Rows.Count > 0 Then
                    For i = 0 To fs.Rows.Count - 1
                        ls = New ListItem
                        ls.Value = fs.Rows(i).Item(0)
                        ls.Text = fs.Rows(i).Item(1)

                        dd_group.Items.Add(ls)
                    Next
                End If
            End If


        End Function

        Function getNight(ByVal s1 As Decimal, ByVal s2 As Decimal, ByVal wk As Decimal, ByVal type As Integer) As Integer
            Dim total As Integer = 0
            Select Case type
                Case 0 '//中班
                    If wk >= 8 And (s2 >= 22 And s2 <= 24) Then
                        total = 1
                    End If

                Case 1 '//夜班

                    If wk >= 8 And (s2 > 24 Or (s1 >= 0 And s1 <= 3)) Then
                        total = 1
                    End If

                Case 2 '// 全夜
                    If wk >= 24 Or (wk >= 12 And s1 <= 22 And s2 >= 30) Then
                        total = 1
                    End If

            End Select

            getNight = total
        End Function

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged

            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













