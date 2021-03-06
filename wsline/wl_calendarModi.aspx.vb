Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Namespace tcpc
    Partial Class wl_calendarModi
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim nRet As Integer
        Dim item As ListItem
        Dim reader As SqlDataReader


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub



        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("14020116", "A类考勤数据维护")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then

                ddl_site.SelectedValue = Session("PlantCode")
                ddl_site.Enabled = False
                txb_year.Text = Format(Today, "yyyy-MM-dd")

                LoadCC()

                BindData()
            End If
        End Sub
        Sub LoadCC()
            ddl_cc.Items.Clear()

            Dim ls As ListItem
            Dim StrSql As String

            StrSql = " Select distinct a.cc,a.ccname from tcpc0.dbo.hr_Attendance_CC a "
            If Session("uRole") <> 1 Then
                StrSql &= " INNER JOIN tcpc0.dbo.wo_cc_permission cp ON cp.perm_userid='" & Session("uID") & "' AND cp.perm_ccid=a.cc "
            End If
            StrSql &= " Where a.plantID='" + ddl_site.SelectedValue + "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem()
                ls.Value = reader(0).ToString().Trim()
                ls.Text = reader(1).ToString().Trim() & "- " & reader(0).ToString().Trim()
                ddl_cc.Items.Add(ls)
            End While
            reader.Close()
            ls = New ListItem("--", "0")
            ddl_cc.Items.Insert(0, ls)

        End Sub

        Sub BindData()
            StrSql = "SELECT a.AttendanceID,a.AttendanceUserID,a.AttendanceUserCenter"
            StrSql &= " ,a.AttendanceUserCenterName,a.AttendanceUserCode,a.AttendanceUserName,case when a.AttendanceUserType=394 then 'A' when a.AttendanceUserType=395 then 'B' when a.AttendanceUserType=396 then 'C' when a.AttendanceUserType=397 then 'D' when a.AttendanceUserType=398 then 'E' end As AttendanceUserType "
            StrSql &= " ,a.checkTime,a.checktype,isnull(a.isCompany,0) isCompany,a.AttendanceUserNo,a.modifiedby,a.modifiedDate,ISNULL(a.isManual,0) isManual ,Isnull(a.Sensorid,0) Sensorid"
            StrSql &= " FROM tcpc0.dbo.hr_AttendanceInfo a "
            StrSql &= " where a.plantID='" & ddl_site.SelectedValue & "'"
            If (ddl_cc.SelectedIndex > 0) Then
                StrSql &= " and a.AttendanceUserCenter='" & ddl_cc.SelectedValue & "'"
            End If

            If (ddl_type.SelectedIndex > 0) Then
                StrSql &= " and a.AttendanceUserType='" & ddl_type.SelectedValue & "'"
            End If

            If (txb_userno.Text.Trim().Length > 0) Then
                StrSql &= " and a.AttendanceUserCode='" & txb_userno.Text.Trim() & "'"
            End If
            If (txb_year.Text.Trim().Length > 0) Then
                StrSql &= " and year(a.checkTime)=year('" & txb_year.Text.Trim() & "') and month(a.checkTime)=month('" & txb_year.Text.Trim() & "') "
            End If
            If (ddl_atten.SelectedIndex > 0) Then
                StrSql &= " and a.checktype='" & ddl_atten.SelectedValue & "'"
            End If
            If ddl_company.SelectedValue = 0 Then
                StrSql &= " and a.isManual=1 and  isnull(a.isCompany,0)= 0 "
            ElseIf ddl_company.SelectedValue = 1 Then
                StrSql &= " and a.isManual=1 and  a.isCompany= 1 "
            End If
            StrSql &= " and isnull(isDisable,0)=0 "
            StrSql &= " and isnull(a.AttendanceUserCode,'')<>'' "
            StrSql &= " order by a.AttendanceUserCode,a.checkTime "

            '
       
            ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql)
           
            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("g_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("c_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("u_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_cc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_no", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_name", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_type", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_atten", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_iscomp", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_comp", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_machine", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_modi", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_modidate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_Sensorid", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1

                        drow = dtl.NewRow()

                        drow.Item("g_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("c_id") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("u_id") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("g_cc") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("g_no") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("g_name") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("g_type") = .Rows(i).Item(6).ToString().Trim()
                        If Not IsDBNull(.Rows(i).Item(7)) And IsDate(.Rows(i).Item(7)) Then
                            drow.Item("g_date") = Format(.Rows(i).Item(7), "yyyy-MM-dd HH:mm:ss")
                        Else
                            drow.Item("g_date") = ""
                        End If
                        drow.Item("g_atten") = .Rows(i).Item(8).ToString().Trim()
                        If .Rows(i).Item(13) = True Then
                            drow.Item("g_iscomp") = .Rows(i).Item(13).ToString().Trim()
                            If .Rows(i).Item(9) = True Then
                                drow.Item("g_comp") = "公事"
                            Else
                                drow.Item("g_comp") = "私事"
                            End If
                        End If
                        drow.Item("g_machine") = .Rows(i).Item(10).ToString()
                        drow.Item("g_modi") = .Rows(i).Item(11).ToString()
                        If Not IsDBNull(.Rows(i).Item(12)) Then
                            drow.Item("g_modidate") = Format(.Rows(i).Item(12), "yyyy-MM-dd")
                        End If
                        drow.Item("g_Sensorid") = .Rows(i).Item(14).ToString()
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()


            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw

            If Datagrid1.PageCount <= Datagrid1.CurrentPageIndex Then
                Datagrid1.CurrentPageIndex = 0
            End If

            Datagrid1.DataBind()

        End Sub
        Protected Sub Datagrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.CancelCommand
            Datagrid1.EditItemIndex = -1
            BindData()
        End Sub
        Protected Sub Datagrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.EditCommand
            Datagrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub
        Protected Sub Datagrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.UpdateCommand
            Dim _cc As String = CType(e.Item.Cells(3).FindControl("lb_cc"), TextBox).Text
            Dim _cname As String = ""
            Dim _utype As String = CType(e.Item.Cells(6).FindControl("lb_type"), TextBox).Text
            Dim _atype As String = CType(e.Item.Cells(8).FindControl("lb_atype"), TextBox).Text

            StrSql = "select ccname from tcpc0.dbo.hr_Attendance_CC where plantID='" & ddl_site.SelectedValue & "' and cc='" & _cc & "'"
            _cname = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, StrSql)
            If _cname = Nothing Then
                ltlAlert.Text = "alert('请输入有效的成本中心')"
                Exit Sub
            ElseIf _cname = "" Then
                ltlAlert.Text = "alert('请输入有效的成本中心')"
                Exit Sub
            End If

            If _utype = "A" Then
                If Not Me.Security("14020116").isValid Then
                    ltlAlert.Text = "alert('没有权限维护类型是A类的员工！');"
                    Exit Sub
                End If
                _utype = "394"
            ElseIf _utype = "B" Then
                _utype = "395"
            ElseIf _utype = "C" Then
                _utype = "396"
            ElseIf _utype = "D" Then
                _utype = "397"
            ElseIf _utype = "E" Then
                _utype = "398"
            Else
                ltlAlert.Text = "alert('请输入员工类型A、B、C、D、E')"
                Exit Sub
            End If

            If _atype <> "I" And _atype <> "O" Then
                ltlAlert.Text = "alert('请输入考勤类型I、O')"
                Exit Sub
            End If

            StrSql = "Update tcpc0.dbo.hr_AttendanceInfo set AttendanceUserCenter='" & _cc & "',AttendanceUserCenterName=N'" & _cname & "',AttendanceUserType='" & _utype & "',CheckType='" & _atype & "',modifiedby=N'" & Session("uName") & "',modifiedDate=getdate(), ImportedBy='" & Session("uID") & "', ImportedDate = getdate() "
            StrSql &= " where AttendanceID='" & e.Item.Cells(0).Text & "' "
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, StrSql)

            Datagrid1.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("g_del") = 0 Then
                If Datagrid1.CurrentPageIndex = Datagrid1.PageCount - 1 Then
                    Datagrid1.CurrentPageIndex = 0
                End If

                StrSql = "Update tcpc0.dbo.hr_AttendanceInfo set isDisable=1,modifiedby=N'" & Session("uName") & "',modifiedDate=getdate() "
                StrSql &= " where AttendanceID='" & e.Item.Cells(0).Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, StrSql)


                BindData()
            End If
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub ddl_site_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_site.SelectedIndexChanged
            ltlAlert.Text = ""
            LoadCC()
        End Sub

        Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
            If ddl_cc.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('请选择成本中心')"
                Exit Sub
            End If
            If ddl_type.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('请选择员工类型')"
                Exit Sub
            End If
            If ddl_atten.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('请选择考勤类型')"
                Exit Sub
            End If
            If ddl_company.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('请选择补漏类型')"
                Exit Sub
            End If
            If txb_userno.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('请输入工号')"
                Exit Sub
            End If

            If txb_year.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('请输入考勤时间yyyy-MM-dd HH:mm:ss')"
                Exit Sub
            ElseIf txb_year.Text.Trim.Length < 11 Then
                ltlAlert.Text = "alert('请输入考勤时间yyyy-MM-dd HH:mm:ss')"
                Exit Sub
            ElseIf Not IsDate(txb_year.Text) Then
                ltlAlert.Text = "alert('请输入考勤时间yyyy-MM-dd HH:mm:ss')"
                Exit Sub
            ElseIf (Convert.ToDateTime(txb_year.Text) <= System.DateTime.Now.AddDays(-45) Or Convert.ToDateTime(txb_year.Text) >= System.DateTime.Now) Then
                ltlAlert.Text = "alert('只能补漏过去45天的考勤时间')"
                Exit Sub
            End If

            Dim _uid As String = ""
            Dim _uname As String = ""
            Dim _muid As String = ""
            Dim _utype As String = ""

            StrSql = "select userID,userName,isnull(Fingerprint,0),usertype from tcpc0.dbo.users where plantCode='" & ddl_site.SelectedValue & "' and userNo='" & txb_userno.Text.Trim & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql)
            While reader.Read()
                _uid = reader(0).ToString()
                _uname = reader(1).ToString()
                _muid = reader(2).ToString()
                _utype = reader(3)
            End While
            reader.Close()

            If _uid = "" Then
                Exit Sub
            Else
                If _utype = 394 Then
                    If Not Me.Security("14020116").isValid Then
                        ltlAlert.Text = "alert('没有权限维护类型是A类的员工！');"
                        Exit Sub
                    End If
                End If
            End If


            StrSql = "Insert Into tcpc0.dbo.hr_AttendanceInfo(CheckTime,CheckType,PlantID,isManual,AttendanceUserID,AttendanceUserName,AttendanceUserCode,AttendanceUserCenter,AttendanceUserCenterName,AttendanceUserType,isDisable,isCompany,modifiedBy,modifiedDate,AttendanceUserNo,ImportedBy,ImportedDate) "
            StrSql &= " values('" & txb_year.Text & "','" & ddl_atten.SelectedValue & "','" & ddl_site.SelectedValue & "',1,'" & _uid & "',N'" & _uname & "','" & txb_userno.Text & "','" & ddl_cc.SelectedValue & "',N'" & ddl_cc.SelectedItem.Text & "','" & ddl_type.SelectedValue & "',0,'" & ddl_company.SelectedValue & "',N'" & Session("uName") & "',getdate(),'" & _muid & "','" & Session("uID") & "', getdate())"
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, StrSql)

            Datagrid1.EditItemIndex = -1
            BindData()


        End Sub

        Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
            ltlAlert.Text = ""
            Datagrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click
            ltlAlert.Text = ""
            ddl_type.SelectedIndex = 0
            ddl_atten.SelectedIndex = 0
            ddl_company.SelectedIndex = 0
            txb_userno.Text = ""
            Datagrid1.CurrentPageIndex = 0

            BindData()
        End Sub

        Protected Sub btn_exportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_exportExcel.Click
            ltlAlert.Text = "var w=window.open('wl_calendarModiToExcel.aspx?plantID=" & ddl_site.SelectedValue & "&userCenter=" & ddl_cc.SelectedValue & "&userType=" & ddl_type.SelectedValue & "&userCode=" & txb_userno.Text.Trim() & "&checkTime=" & txb_year.Text.Trim() & "&checktype=" & ddl_atten.SelectedValue & "&isCompany=" & ddl_company.SelectedValue & "&rt=" & DateTime.Now.ToString() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
        End Sub

         
        Protected Sub btn_exportExcel2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_exportExcel2.Click
            ltlAlert.Text = "var w=window.open('wl_calendarModiIncludToExcel.aspx?plantID=" & ddl_site.SelectedValue & "&userCenter=" & ddl_cc.SelectedValue & "&userType=" & ddl_type.SelectedValue & "&userCode=" & txb_userno.Text.Trim() & "&checkTime=" & txb_year.Text.Trim() & "&checktype=" & ddl_atten.SelectedValue & "&isCompany=" & ddl_company.SelectedValue & "&rt=" & DateTime.Now.ToString() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
        End Sub

        Protected Sub btn_updateCC_Click(sender As Object, e As System.EventArgs) Handles btn_updateCC.Click

            If Not IsDate(txb_year.Text.Trim()) Then
                Alert("考勤日期格式不正确！")
                Return
            End If

            StrSql = "sp_hr_modifyUserCostCenterByDate"
            Dim params(2) As SqlParameter
            params(0) = New SqlParameter("@date", txb_year.Text.Trim())
            params(1) = New SqlParameter("@plantID", Session("PlantCode"))
            SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)

            Datagrid1.EditItemIndex = -1
            BindData()
        End Sub
    End Class
End Namespace