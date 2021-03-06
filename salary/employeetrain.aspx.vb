Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration

Namespace tcpc

    Partial Class employeetrain
        Inherits System.Web.UI.Page
        Dim nRet As Integer
        Dim strSql As String
        Dim ds As DataSet
        Public chk As New adamClass
        Dim reader As SqlDataReader
        'Protected WithEvents ltlAlert As Literal

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ddl1 As System.Web.UI.WebControls.DropDownList


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
                Btnmodify.Enabled = False
                LoadtrainType()
                Loadtrainstatus()
                binddata()
                txbdep.Text = Request("choose")
                txbdepid.Text = Request("chooseid")
            End If
        End Sub
        Sub clearuseless()
            txbsubject.Text = ""
            txbdep.Text = ""
            txbdepid.Text = ""
            txbperson.Text = ""
            txbdate.Text = ""
            txbtime.Text = ""
            Txbresid.Text = ""
            Txbres.Text = ""
            Txbhost.Text = ""
            Txbsite.Text = ""
            Txbmemo.Text = ""
            SelectTypeDropDown.SelectedValue = 0
            selectfinishdropdown.SelectedValue = 0
            ' datagrid1.CurrentPageIndex = 0
            'datagrid1.SelectedIndex = -1
        End Sub

        Sub LoadtrainType()
            Dim ls As ListItem
            ls = New ListItem
            ls.Value = 0
            ls.Text = "----"
            SelectTypeDropDown.Items.Add(ls)
            Dim reader As SqlDataReader
            strSql = "select typeid,typename from tcpc0.dbo.traintype "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                SelectTypeDropDown.Items.Add(ls)
            End While
            reader.Close()
            SelectTypeDropDown.SelectedValue = 0
        End Sub

        'Private Sub SelectTypeDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectTypeDropDown.SelectedIndexChanged
        '    DataGrid1.EditItemIndex = -1
        '    DataGrid1.SelectedIndex = -1
        '    DataGrid1.CurrentPageIndex = 0
        '    BindData()
        '    End Sub
        Sub Loadtrainstatus()
            Dim ls As ListItem
            ls = New ListItem
            ls.Value = 0
            ls.Text = "----"
            selectfinishdropdown.Items.Add(ls)
            Dim reader As SqlDataReader

            strSql = "select statusid,status from tcpc0.dbo.trainstatus "
            'Response.Write(strSql)
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                selectfinishdropdown.Items.Add(ls)
            End While
            reader.Close()
            selectfinishdropdown.SelectedIndex = 2

        End Sub

        'Private Sub SelectfinishDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles selectfinishdropdown.SelectedIndexChanged
        '    datagrid1.EditItemIndex = -1
        '    datagrid1.SelectedIndex = -1
        '    datagrid1.CurrentPageIndex = 0
        '    binddata()
        'End Sub
        Sub binddata()
            strSql = "select id,subject,depid,involvedep,involveemp,traindate,traintime,respid,respdep,host,site,status,memo,typeid from tcpc0.dbo.employeetrain et inner join tcpc0.dbo.trainstatus tt on et.finish=tt.statusid  where plantcode = '" & Session("plantcode") & "'"
            If SelectTypeDropDown.SelectedValue > 0 Then
                strSql &= "and typeid = '" & SelectTypeDropDown.SelectedValue & "'"
            End If
            If selectfinishdropdown.SelectedValue > 0 Then
                strSql &= "and finish = '" & selectfinishdropdown.SelectedValue & "'"
            End If
            If txbsubject.Text.Trim.Length > 0 Then
                strSql &= "and subject like N'%" & txbsubject.Text.Trim & "%'"
            End If
            If txbdep.Text.Trim.Length > 0 Then
                strSql &= "and involvedep like N'%" & txbdep.Text.Trim & "%'"
            End If

            If txbperson.Text.Trim.Length > 0 Then
                strSql &= "and involveemp like N'%" & txbperson.Text.Trim & "%'"
            End If

            If txbdate.Text.Trim.Length > 0 Then
                strSql &= "and traindate = '" & txbdate.Text.Trim & "'"
            End If
            If txbtime.Text.Trim.Length > 0 Then
                strSql &= "and traintime =N'" & txbtime.Text.Trim & "'"
            End If
            If Txbres.Text.Trim.Length > 0 Then
                strSql &= "and respdep like N'" & Txbres.Text.Trim & "'"
            End If
            If Txbhost.Text.Trim.Length > 0 Then
                strSql &= "and host like N'" & Txbhost.Text.Trim & "'"
            End If
            If Txbsite.Text.Trim.Length > 0 Then
                strSql &= "and site like N'" & Txbsite.Text.Trim & "'"
            End If
            If Txbmemo.Text.Trim.Length > 0 Then
                strSql &= "and memo like N'" & Txbmemo.Text.Trim & "'"
            End If
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
            Dim total As Integer
            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("subject", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("depid", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("involvedep", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("involveemp", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("traindate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("traintime", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("respid", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("respdep", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("host", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("site", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("memo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("finish", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("typeid", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dt.NewRow()
                        drow.Item("id") = .Rows(i).Item("id").ToString().Trim()
                        drow.Item("subject") = .Rows(i).Item("subject").ToString().Trim()
                        drow.Item("depid") = .Rows(i).Item("depid").ToString().Trim()
                        drow.Item("involvedep") = .Rows(i).Item("involvedep").ToString().Trim()
                        drow.Item("involveemp") = .Rows(i).Item("involveemp")
                        If IsDBNull(.Rows(i).Item("traindate")) = False Then
                            drow.Item("traindate") = Format(.Rows(i).Item("traindate"), "yyyy-MM-dd")
                        Else
                            drow.Item("traindate") = ""
                        End If
                        drow.Item("traintime") = .Rows(i).Item("traintime").ToString().Trim()
                        drow.Item("respid") = .Rows(i).Item("respid").ToString().Trim()
                        drow.Item("respdep") = .Rows(i).Item("respdep").ToString().Trim()
                        drow.Item("host") = .Rows(i).Item("host").ToString().Trim()
                        drow.Item("site") = .Rows(i).Item("site")
                        drow.Item("memo") = .Rows(i).Item("memo").ToString().Trim()
                        drow.Item("finish") = .Rows(i).Item("status").ToString().Trim()
                        drow.Item("typeid") = .Rows(i).Item("typeid").ToString().Trim()
                        dt.Rows.Add(drow)
                        total = total + 1
                    Next
                End If
            End With
            ds.Reset()
            Dim dvw As DataView
            dvw = New DataView(dt)
            datagrid1.DataSource = dvw
            datagrid1.DataBind()
            Btnadd.Enabled = True
        End Sub

        Private Sub btnser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnser.Click
            datagrid1.CurrentPageIndex = 0
            datagrid1.SelectedIndex = -1
            binddata()
        End Sub

        Private Sub Btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btncancel.Click
            clearuseless()
            datagrid1.SelectedIndex = -1
            Btnadd.Enabled = True
        End Sub
        Private Sub datagrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles datagrid1.EditCommand
            datagrid1.EditItemIndex = e.Item.ItemIndex
            binddata()
        End Sub
        Private Sub Btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnadd.Click
            If txbsubject.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('培训课题 不能为空');"
                Exit Sub
            End If

            If txbdep.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('参培部门 不能为空');"
                Exit Sub
            End If

            If txbperson.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('参培对象 不能为空');"
                Exit Sub
            End If

            If Txbhost.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('主讲人 不能为空');"
                Exit Sub
            End If

            If Txbres.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('负责部门 不能为空');"
                Exit Sub
            End If

            If txbtime.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('培训时间 不能为空');"
                Exit Sub
            End If

            If Txbsite.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('培训地点 不能为空');"
                Exit Sub
            End If

            Dim identityid As String
            Dim detail As Array
            Dim i As Integer
            identityid = ""
            strSql = "select count(id) from tcpc0.dbo.employeetrain where subject =N'" & txbsubject.Text.Trim & "' and involvedep = N'" & txbdep.Text.Trim & "' and  involveemp = N'" & txbperson.Text.Trim & "' and traindate = N'" & txbdate.Text.Trim & "' and traintime = '" & txbtime.Text.Trim & "' and respdep =N'" & Txbres.Text.Trim & "' and host = N'" & Txbhost.Text.Trim & "'and site = N'" & Txbsite.Text.Trim & "'and memo= N'" & Txbmemo.Text.Trim & "'and finish = '" & selectfinishdropdown.SelectedValue & "' and plantcode= '" & Session("plantcode") & "'and typeid = '" & SelectTypeDropDown.SelectedValue & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) Then
                ltlAlert.Text = "alert('相同记录已存在')"
            Else
                strSql = "insert into tcpc0.dbo.employeetrain (typeid,subject,depid,involvedep,involveemp,traindate,traintime,respid,respdep,host,site,memo,finish,plantcode) values (N'" & SelectTypeDropDown.SelectedValue & "',N'" & txbsubject.Text.Trim & "',N'" & txbdepid.Text.Trim & " ',N'" & txbdep.Text.Trim & "',N'" & txbperson.Text.Trim & "',N'" & txbdate.Text.Trim & "',N'" & txbtime.Text.Trim & "',N'" & Txbresid.Text.Trim & "',N'" & Txbres.Text.Trim & "',N'" & Txbhost.Text.Trim & "',N'" & Txbsite.Text.Trim & "',N'" & Txbmemo.Text.Trim & "','2','" & Session("plantcode") & "')"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
                strSql = " select  max(id) from tcpc0.dbo.employeetrain"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
                While (reader.Read())
                    identityid = reader(0).ToString.Trim
                End While
                reader.Close()
                detail = txbdepid.Text.Trim.Split(",")
                For i = 0 To detail.Length - 1
                    If detail(i) <> "" Then
                        strSql = "insert into tcpc0.dbo.traindep (trainid,depid) values ('" & identityid & "','" & detail(i) & "')"
                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
                    End If
                Next
                'detail = txbdepid.Text.Trim
                'If detail.Length > 0 Then
                '    subdet = detail.Substring(1, detail.Length - 2)
                'End If
                'While subdet.Length > 0
                '    pos = subdet.IndexOf(",")
                '    finalid = ""
                '    If pos <> -1 Then
                '        finalid = subdet.Substring(0, pos)
                '        strSql = "insert into tcpc0.dbo.traindep (trainid,depid) values ('" & identityid & "','" & finalid & "')"
                '        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
                '        subdet = subdet.Substring(pos + 1)
                '    Else
                '        strSql = "insert into tcpc0.dbo.traindep (trainid,depid) values ('" & identityid & "','" & subdet & "')"
                '        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
                '        subdet = ""
                '    End If
                'End While
                clearuseless()
                binddata()
            End If

        End Sub
        Private Sub datagrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles datagrid1.ItemCreated
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    e.Item.Cells(15).Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")
                Case ListItemType.AlternatingItem
                    e.Item.Cells(15).Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")
                Case ListItemType.EditItem
                    e.Item.Cells(15).Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")
            End Select
        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles datagrid1.ItemCommand
            If e.CommandName.CompareTo("DeleteClick") = 0 Then
                strSql = "delete from tcpc0.dbo.employeetrain where id =' " & e.Item.Cells(0).Text.Trim() & "' and plantcode = '" & Session("plantcode") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                strSql = "delete from tcpc0.dbo.traindep where trainid =' " & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                clearuseless()
                binddata()

                Exit Sub
            End If

            If e.CommandName.CompareTo("Select") = 0 Then
                txbid.Text = e.Item.Cells(0).Text.Trim()
                txbsubject.Text = e.Item.Cells(1).Text.Trim()
                If e.Item.Cells(2).Text.Trim() <> "&nbsp;" Then
                    txbdepid.Text = e.Item.Cells(2).Text.Trim()
                End If
                txbdep.Text = e.Item.Cells(3).Text.Trim()
                txbperson.Text = e.Item.Cells(4).Text.Trim()
                txbdate.Text = e.Item.Cells(5).Text.Trim()
                txbtime.Text = e.Item.Cells(6).Text.Trim()
                Txbresid.Text = e.Item.Cells(7).Text.Trim()
                Txbres.Text = e.Item.Cells(8).Text.Trim()
                Txbhost.Text = e.Item.Cells(9).Text.Trim()
                Txbsite.Text = e.Item.Cells(10).Text.Trim()
                If e.Item.Cells(12).Text.Trim() <> "&nbsp;" Then
                    Txbmemo.Text = e.Item.Cells(12).Text.Trim()
                End If
                If e.Item.Cells(11).Text.Trim = "完成" Then
                    chkstatus.Checked = True
                Else
                    chkstatus.Checked = False

                End If
                SelectTypeDropDown.SelectedValue = e.Item.Cells(13).Text.Trim()
                selectfinishdropdown.SelectedValue = IIf(e.Item.Cells(11).Text.Trim() = "完成", 1, 2)
                Btnmodify.Enabled = True
                Btnadd.Enabled = False
                Exit Sub
            End If
        End Sub
        Private Sub Btnmodify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnmodify.Click
            Dim detail As Array
            Dim i As Integer
            Dim status As String
            If chkstatus.Checked = True Then
                status = "1"
            Else
                status = "2"
            End If
            strSql = "update tcpc0.dbo.employeetrain set subject = N'" & txbsubject.Text.Trim & "', depid=N'" & txbdepid.Text.Trim & "',involvedep = N'" & txbdep.Text.Trim & "', involveemp = N'" & txbperson.Text.Trim & "', traindate = '" & txbdate.Text.Trim & "', traintime = N'" & txbtime.Text.Trim & "',respid=N'" & Txbresid.Text.Trim & "', respdep = N'" & Txbres.Text.Trim & "', host = N'" & Txbhost.Text.Trim & "', site = N'" & Txbsite.Text.Trim & "', memo = N'" & Txbmemo.Text.Trim & "', finish = '" & status & "' where id = '" & txbid.Text.Trim & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            strSql = "delete from tcpc0.dbo.traindep where trainid= '" & txbid.Text.Trim & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            detail = txbdepid.Text.Trim.Split(",")
            For i = 0 To detail.Length - 1
                If detail(i) <> "" Then
                    strSql = "insert into tcpc0.dbo.traindep (trainid,depid) values ('" & txbid.Text.Trim & "','" & detail(i) & "')"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
                End If
            Next
            clearuseless()
            binddata()
            Btnadd.Enabled = True
            Btnmodify.Enabled = False
        End Sub

        Protected Sub btnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnselect.Click
            ltlAlert.Text = "var w=window.open('traindep.aspx?mid=" & txbdepid.Text.Trim & "&type=1&depname=" & Server.UrlEncode(txbdep.Text.Trim) & "','department','menubar=no,scrollbars = no,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End Sub

        Protected Sub Btnselect1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnselect1.Click
            ltlAlert.Text = "var w=window.open('traindep.aspx?mid=" & Txbresid.Text.Trim & "&type=0&depname=" & Server.UrlEncode(Txbres.Text.Trim) & "','department','menubar=no,scrollbars = no,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
        End Sub

        Protected Sub Btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnprint.Click

            If txbyear.Text.Length = 0 And txbmonth.Text.Length = 0 Then
                strSql = "SELECT tt.typename, et.subject, substring(involvedep,2,len(involvedep)-2), et.involveemp,et.traindate, et.traintime + '&nbsp;', substring(respdep,2,len(respdep)-2), et.host, et.site, ts.status, et.memo FROM  tcpc0.dbo.traintype tt INNER JOIN  tcpc0.dbo.employeetrain et ON et.typeid = tt.typeid INNER JOIN tcpc0.dbo.trainstatus ts ON et.finish = ts.statusid WHERE et.plantcode ='" & Session("plantcode") & "'ORDER BY et.typeid"
                ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSql)
                Session("EXSQL") = strSql
                Session("EXTitle") = "150^<b>培训类型</b>~^250^<b>培训课题</b>~^200^<b>参培部门</b>~^200^<b>参培对象</b>~^100^<b>培训日期</b>~^110^<b>培训时间</b>~^80^<b>负责部门</b>~^80^<b>主讲人</b>~^100^<b>培训地点</b>~^100^<b>完成情况</b>~^300^<b>备注</b>~^"
                Session("EXHeader") = Session("orgname") & "培训计划"
                ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
                Exit Sub

            End If

            If txbyear.Text.Length > 0 And txbmonth.Text.Length = 0 Then
                strSql = "SELECT tt.typename, et.subject, substring(involvedep,2,len(involvedep)-2), et.involveemp, et.traindate, et.traintime + '&nbsp;',substring(respdep,2,len(respdep)-2), et.host, et.site, ts.status, et.memo FROM  tcpc0.dbo.traintype tt INNER JOIN  tcpc0.dbo.employeetrain et ON et.typeid = tt.typeid INNER JOIN tcpc0.dbo.trainstatus ts ON et.finish = ts.statusid where plantcode = '" & Session("plantcode") & "'and year(traindate)='" & txbyear.Text.Trim & "' order by et.typeid"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
                Session("EXSQL") = strSql
                Session("EXTitle") = "150^<b>培训类型</b>~^250^<b>培训课题</b>~^200^<b>参培部门</b>~^200^<b>参培对象</b>~^100^<b>培训日期</b>~^110^<b>培训时间</b>~^80^<b>负责部门</b>~^80^<b>主讲人</b>~^100^<b>培训地点</b>~^100^<b>完成情况</b>~^300^<b>备注</b>~^"
                Session("EXHeader") = Session("orgname") & txbyear.Text.Trim & "年培训计划"
                ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
                Exit Sub
            End If

            If txbyear.Text.Length = 0 And txbmonth.Text.Length > 0 Then
                strSql = "SELECT tt.typename, et.subject, substring(involvedep,2,len(involvedep)-2), et.involveemp, et.traindate, et.traintime + '&nbsp;', substring(respdep,2,len(respdep)-2), et.host, et.site, ts.status, et.memo FROM  tcpc0.dbo.traintype tt INNER JOIN  tcpc0.dbo.employeetrain et ON et.typeid = tt.typeid INNER JOIN tcpc0.dbo.trainstatus ts ON et.finish = ts.statusid where plantcode = '" & Session("plantcode") & "' and month(traindate)='" & txbmonth.Text.Trim & "' order by et.typeid"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
                Session("EXSQL") = strSql
                Session("EXTitle") = "150^<b>培训类型</b>~^250^<b>培训课题</b>~^200^<b>参培部门</b>~^200^<b>参培对象</b>~^100^<b>培训日期</b>~^110^<b>培训时间</b>~^80^<b>负责部门</b>~^80^<b>主讲人</b>~^100^<b>培训地点</b>~^100^<b>完成情况</b>~^300^<b>备注</b>~^"
                Session("EXHeader") = Session("orgname") & txbmonth.Text.Trim & "月培训计划"
                ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
                Exit Sub
            End If

            If txbyear.Text.Length > 0 And txbmonth.Text.Length > 0 Then
                strSql = "SELECT tt.typename, et.subject,substring(involvedep,2,len(involvedep)-2), et.involveemp, et.traindate, et.traintime + '&nbsp;', substring(respdep,2,len(respdep)-2), et.host, et.site, ts.status, et.memo FROM  tcpc0.dbo.traintype tt INNER JOIN  tcpc0.dbo.employeetrain et ON et.typeid = tt.typeid INNER JOIN tcpc0.dbo.trainstatus ts ON et.finish = ts.statusid where plantcode = '" & Session("plantcode") & "' and year(traindate) = '" & txbyear.Text.Trim & "' and month(traindate) = '" & txbmonth.Text.Trim & "' order by et.typeid"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
                Session("EXSQL") = strSql
                Session("EXTitle") = "150^<b>培训类型</b>~^250^<b>培训课题</b>~^200^<b>参培部门</b>~^200^<b>参培对象</b>~^100^<b>培训日期</b>~^110^<b>培训时间</b>~^80^<b>负责部门</b>~^80^<b>主讲人</b>~^100^<b>培训地点</b>~^100^<b>完成情况</b>~^300^<b>备注</b>~^"
                Session("EXHeader") = Session("orgname") & txbyear.Text.Trim & "年" & txbmonth.Text.Trim & "月培训计划"
                ltlAlert.Text = "var w=window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();"
                Exit Sub
            End If
        End Sub

        Protected Sub datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles datagrid1.PageIndexChanged
            If datagrid1.SelectedIndex <> -1 Then
                ltlAlert.Text = "alert('请先取消当前选中行！')"
            Else
                datagrid1.CurrentPageIndex = e.NewPageIndex
                binddata()
            End If
        End Sub
    End Class

End Namespace
