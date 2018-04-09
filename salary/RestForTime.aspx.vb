

'!*******************************************************************************!
'* @@ NAME				:	RestForTime.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for  RestForTime.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	11/07/2008
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Drawing
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class RestForTime
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        Dim strSql As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass
        Dim ds As DataSet

        'Protected WithEvents ltlAlert As Literal

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then
                yeartextbox.Text = DateTime.Now.Year.ToString
                month.SelectedIndex = DateTime.Now.Month - 1
                dropdownValue()

                SaleBind(0)
            End If
        End Sub

        Sub dropdownValue()
            department.Items.Clear()
            department.Items.Add(New ListItem("--", "0"))
            strSql = " Select departmentID,name From departments Where isSalary='1' and active='1'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read()
                Dim tempListItem As New ListItem
                tempListItem.Value = reader(0)
                tempListItem.Text = reader(1)
                department.Items.Add(tempListItem)
            End While
            reader.Close()
        End Sub
        Function createSQL(ByVal temp As Integer) As String
            strSql = " select r.id,u.userID,u.userno,u.username,isnull(d.name,''),r.chgdate,isnull(r.ovnumber,0),r.workdate"

            strSql &= "     ,isnull(r.number,0),r.sdate,isnull(r.number1,0),u.enterdate,u.leavedate, '-' + isnull(r.comment,''), money, needSettled = Isnull(needSettled, 0),s.systemCodeName "

            strSql &= " ,case datename(weekday, r.chgdate) when 'Monday' Then N'星期一'"
            strSql &= " when 'Tuesday' then N'星期二' "
            strSql &= " when 'Wednesday' then N'星期三' "
            strSql &= " when 'Thursday' then N'星期四' "
            strSql &= " when 'Friday' then N'星期五' "
            strSql &= " when 'Saturday' then N'星期六' "
            strSql &= " else N'星期日' "
            strSql &= " end weekday "
            strSql &= " ,ISNULL(ws.name,'')"

            strSql &= "from RestTime r "
            strSql &= " inner join tcpc0.dbo.Users u on u.userID= r.usercode "
            strSql &= " left outer join departments d on d.departmentID=u.departmentID "
            strSql &= " left join tcpc0.dbo.systemCode s on s.systemCodeID = u.workTypeID "
            strSql &= " LEFT OUTER JOIN Workshop ws ON ws.id=u.workshopID  "
            If temp > 0 Then
                If workdate.Text.Trim.Length > 0 Then
                    strSql &= " where r.chgdate='" & chgdate.Text.Trim & "'"
                Else
                    strSql &= " where year(chgdate)='" & yeartextbox.Text & "' and month(chgdate)='" & month.SelectedValue & "' "
                End If

                If workerNoSearch.Text.Trim.Length > 0 Then
                    strSql &= " and cast(u.userNo as varchar)='" & workerNoSearch.Text.Trim() & "'"
                End If

                If workerNameSearch.Text.Trim.Length > 0 Then
                    strSql &= " and lower(u.userName) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
                End If

                If department.SelectedValue > 0 Then
                    strSql &= " and u.departmentID='" & department.SelectedValue & "' "
                End If
            Else
                strSql &= " where year(chgdate)='" & yeartextbox.Text & "' and month(chgdate)='" & month.SelectedValue & "' "
            End If
            strSql &= " order by r.ID desc"
            createSQL = strSql
        End Function

        Private Sub SaleBind(ByVal temp As Integer)

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, createSQL(temp))
            Dim dt As New DataTable
            Dim i As Integer = 0
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("weekday", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("number1", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("enterDate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("leavedate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("comment", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ovnumber", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("chgdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("sdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("number2", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("money", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("needSettled", System.Type.GetType("System.Boolean")))
            dt.Columns.Add(New DataColumn("systemCodeName", System.Type.GetType("System.String")))

            Dim dr1 As DataRow
            While reader.Read
                dr1 = dt.NewRow()
                dr1.Item("gsort") = i + 1
                dr1.Item("ID") = reader(0)
                dr1.Item("userID") = reader(1)
                dr1.Item("userNo") = reader(2)
                dr1.Item("userName") = reader(3)
                dr1.Item("weekday") = reader(17)
                dr1.Item("department") = reader(4)
                If reader(5).ToString = "" Then
                    dr1.Item("chgdate") = ""
                Else
                    dr1.Item("chgdate") = reader(5).ToShortDateString()
                End If

                dr1.Item("ovnumber") = reader(6)

                If reader(7).ToString = "" Then
                    dr1.Item("workdate") = ""
                Else
                    dr1.Item("workdate") = reader(7).ToShortDateString()
                End If
                dr1.Item("number1") = reader(8)

                If reader(9).ToString = "" Then
                    dr1.Item("sdate") = ""
                Else
                    dr1.Item("sdate") = reader(9).ToShortDateString()
                End If
                dr1.Item("number2") = reader(10)

                dr1.Item("enterdate") = reader(11).ToShortDateString()
                If reader(12).ToString = "" Then
                    dr1.Item("leavedate") = ""
                Else
                    dr1.Item("leavedate") = reader(12).ToShortDateString()
                End If

                dr1.Item("comment") = reader(13)
                dr1.Item("systemCodeName") = reader(16)

                dr1.Item("money") = reader("money")
                dr1.Item("needSettled") = reader("needSettled")

                dt.Rows.Add(dr1)
            End While
            reader.Close()

            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try

        End Sub

        Public Sub Edit_delete(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
            strSql = "delete from RestTime Where id =" & e.Item.Cells(18).Text.Trim()
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            SaleBind(1)
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            SaleBind(1)
        End Sub



        Sub BtnSave_click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim kk, kkov As Decimal
            If userID.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('条件不符合，请重新输入！');Form1.workerNo.focus();"
                workerNo.Text = ""
                workerName.Text = ""
                enterdate.Text = ""
                Exit Sub
            End If

            If chgdate.Text.Trim() = "" Then
                ltlAlert.Text = "alert('加班日期不能为空！');Form1.workerNo.focus();"
                Exit Sub
            End If

            If Not IsDate(chgdate.Text.Trim()) Then
                ltlAlert.Text = "alert('加班日期的格式不正确！正确格式参考：2012-12-20！');Form1.workerNo.focus();"
                Exit Sub
            End If

            If workdate.Text.Trim() <> "" Then
                If Not IsDate(workdate.Text.Trim()) Then
                    ltlAlert.Text = "alert('调休日1的格式不正确！正确格式参考：2012-12-20！');Form1.workerNo.focus();"
                    Exit Sub
                End If
            End If

            If number.Text.Trim() <> "" Then
                If Not IsNumeric(number.Text.Trim()) Then
                    ltlAlert.Text = "alert('调休天1必须是数字！');Form1.workerNo.focus();"
                    Exit Sub
                End If
            End If

            If sdate.Text.Trim() <> "" Then
                If Not IsDate(sdate.Text.Trim()) Then
                    ltlAlert.Text = "alert('调休日2的格式不正确！正确格式参考：2012-12-20！');Form1.workerNo.focus();"
                    Exit Sub
                End If
            End If

            If number1.Text.Trim() <> "" Then
                If Not IsNumeric(number1.Text.Trim()) Then
                    ltlAlert.Text = "alert('调休天2必须是数字！');Form1.workerNo.focus();"
                    Exit Sub
                End If
            End If

            If number.Text.Trim.Length = 0 Then
                kk = 0
            Else
                kk = CDec(number.Text.Trim)
            End If

            If ovnumber.Text.Trim.Length = 0 Then
                kkov = 0
            Else
                kkov = CDec(ovnumber.Text.Trim)
            End If

            strSql = " select isnull(id,0),isnull(number,0),isnull(ovnumber,0),isnull(needSettled,0) from RestTime where usercode='" & userID.Text.Trim & "' and chgdate='" & chgdate.Text.Trim & "' "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
            If ds.Tables(0).Rows.Count = 0 Then
                If kk <> 0 Or workdate.Text.Trim.Length > 0 Then
                    ltlAlert.Text = "alert('输入有误，必须先录入加班记录！');Form1.workerNo.focus();"
                    Exit Sub
                End If

                If kkov = 0 Then
                    ltlAlert.Text = "alert('输入有误，加班天数不能为空！');Form1.workerNo.focus();"
                    Exit Sub
                End If
                strSql = " insert into RestTime(usercode,chgdate,ovnumber,createdby,createddate,comment) values ('" & userID.Text.Trim & "','" & chgdate.Text.Trim & "','" & kkov & "','" & Session("uid") & "','" & DateTime.Now & "',N'" & chk.sqlEncode(comment.Text.Trim) & "')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                ltlAlert.Text = "alert('保存成功！');Form1.workerNo.focus();"
            Else
                If kkov <> 0 Then
                    ltlAlert.Text = "alert('输入有误，加班记录已存在！');Form1.workerNo.focus();"
                    Exit Sub
                End If
                If ds.Tables(0).Rows(0).Item(3) = 0 Then
                    If ds.Tables(0).Rows(0).Item(1) = 0 Then 'change rest days one
                        If kk = 0 Or workdate.Text.Trim.Length = 0 Then
                            ltlAlert.Text = "alert('输入有误，调休天1与日期1不能为空！');Form1.workerNo.focus();"
                            Exit Sub
                        End If

                        If CDec(number.Text.Trim) > ds.Tables(0).Rows(0).Item(2) Then
                            ltlAlert.Text = "alert('输入有误，调休天1不能大于加班天数 ！');Form1.workerNo.focus();"
                            Exit Sub
                        End If
                        strSql = " Update RestTime set number='" & number.Text & "',workdate='" & workdate.Text.Trim & "' where id='" & ds.Tables(0).Rows(0).Item(0) & "'"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                    Else
                        If number1.Text.Trim.Length = 0 Or sdate.Text.Trim.Length = 0 Then
                            ltlAlert.Text = "alert('输入有误，调休天2与日期2不能为空！');Form1.workerNo.focus();"
                            Exit Sub
                        End If
                        If ds.Tables(0).Rows(0).Item(1) + CDec(number1.Text.Trim) > ds.Tables(0).Rows(0).Item(2) Then
                            ltlAlert.Text = "alert('输入有误，调休天1 + 调休天2 大于加班天数 ！');Form1.workerNo.focus();"
                            Exit Sub
                        End If
                        strSql = " Update RestTime set number1='" & number1.Text & "',sdate='" & sdate.Text.Trim & "' where id='" & ds.Tables(0).Rows(0).Item(0) & "'"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                    End If
                Else
                    ltlAlert.Text = "alert('金额已经结算，不能做调休操作！');"
                    Exit Sub
                End If
            End If
            ds.Reset()

            workerNo.Text = ""
            workerName.Text = ""
            userID.Text = ""

            enterdate.Text = ""
            number.Text = ""
            ovnumber.Text = ""
            SaleBind(1)
        End Sub

        ' The textbox  had text-changed

        Sub workerNo_changed(ByVal sender As Object, ByVal e As System.EventArgs)
            userID.Text = ""
            If chgdate.Text.Trim <> "" Then
                If workerNo.Text.Trim.Length = 0 Then       'UserNo is not null 
                    ltlAlert.Text = "alert('工号不能为空!');Form1.chgdate.focus();"
                    workerName.Text = ""
                    userID.Text = ""
                    enterdate.Text = ""
                    Exit Sub
                Else
                    strSql = " SELECT u.userID,u.userName,u.leaveDate,u.enterdate from tcpc0.dbo.users u "
                    strSql &= " INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID= u.workTypeID and s.systemCodeName<>N'计件' "
                    strSql &= " where cast(u.userNo as varchar)='" & workerNo.Text.Trim() & "'  "
                    strSql &= " and u.plantCode='" & Session("PlantCode") & "'"
                    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
                    While reader.Read
                        If DateDiff(DateInterval.Day, reader(3), CDate(chgdate.Text.Trim)) < 0 Then  ' enterdate < pdate
                            ltlAlert.Text = "alert('此员工属于新员工，输入的日期不正确！');Form1.workerNo.focus();"
                            workerNo.Text = ""
                            workerName.Text = ""
                            userID.Text = ""
                            enterdate.Text = ""
                            Exit Sub
                        End If

                        If reader(2).ToString() <> "" Then
                            If DateDiff(DateInterval.Day, CDate(chgdate.Text.Trim), reader(2)) < 0 Then
                                ltlAlert.Text = "alert('此员工在输入的日期时已离职！');Form1.workerNo.focus();"
                                workerNo.Text = ""
                                workerName.Text = ""
                                userID.Text = ""
                                enterdate.Text = ""
                                Exit Sub
                            End If
                        End If
                        'ltlAlert.Text = "Form1.piece.focus();"
                        workerName.Text = reader(1)
                        userID.Text = reader(0)
                        enterdate.Text = reader(3).ToShortDateString()
                        ltlAlert.Text = "Form1.ovnumber.focus();"
                    End While
                    reader.Close()

                    If userID.Text.Trim.Length = 0 Then
                        ltlAlert.Text = "alert('工号不存在或不属于计时！');Form1.workerNo.focus();"
                        workerNo.Text = ""
                        workerName.Text = ""
                        userID.Text = ""
                        enterdate.Text = ""
                        Exit Sub
                    End If

                End If
            Else
                ltlAlert.Text = "alert('加班日期不能为空!');Form1.chgdate.focus();"
                Exit Sub
            End If
        End Sub


        Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
            If yeartextbox.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('年不能为空!');Form1.yeartextbox.focus();"
                Exit Sub
            Else
                If Not IsNumeric(yeartextbox.Text.Trim) Then
                    ltlAlert.Text = "alert('输入年份只能为数字!');Form1.yeartextbox.focus();"
                    Exit Sub
                End If
            End If

            DataGrid1.CurrentPageIndex = 0
            SaleBind(1)
        End Sub

        Protected Sub chk_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

            Dim index As Int32
            index = CType((CType(sender, CheckBox).Parent.Parent), DataGridItem).ItemIndex

            strSql = "salary_isRestTimeNeedSettled"
            Dim params(2) As SqlParameter
            params(0) = New SqlParameter("@id", DataGrid1.Items(index).Cells(18).Text.Trim())
            params(1) = New SqlParameter("@value", CType(sender, CheckBox).Checked)

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSql, params)
            SaleBind(1)
        End Sub
        Protected Sub ButExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButExcel.Click
            Dim EXTitle As String = "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>加班日期</b>~^<b>加班天数</b>~^<b>调休日1</b>~^<b>调休天1</b>~^<b>调休日2</b>~^<b>调休天2</b>~^100^<b>入司日期</b>~^100^<b>离职日期</b>~^<b>备注</b>~^<b>金额</b>~^<b>结算</b>~^<b>计酬方式</b>~^<b>星期</b>~^<b>工段</b>~^"
            Dim ExSql As String = createSQL(1)
            Me.ExportExcel(chk.dsnx, EXTitle, ExSql, False)
        End Sub

    End Class

End Namespace



