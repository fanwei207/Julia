Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration

Partial Class computermanage_computermanage
    Inherits BasePage
    Dim nRet As Integer
    Dim strSql As String
    Dim ds As DataSet
    Public chk As New adamClass
    Dim reader As SqlDataReader
    Dim flag1 As Integer
    'Protected WithEvents ltlAlert As Literal

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""

        If Not IsPostBack Then 
            Btnmodify.Enabled = False
            LoadComputerType()
            BindData()
        End If

    End Sub
    Sub clearuseless()
        txbid.Text = ""
        txbassetno.Text = ""
        txbcpu.Text = ""
        txbmemory.Text = ""
        Txbharddisk.Text = ""
        txbdisplay.Text = ""
        txbkeyboard.Text = ""
        txbmouse.Text = ""
        txbdes.Text = ""
        txbip.Text = ""
        txbmac.Text = ""
        txbos.Text = ""
        selecttypedropdown.SelectedValue = 0
        ddlinternet.SelectedValue = 0
        ' datagrid1.CurrentPageIndex = 0
        datagrid1.SelectedIndex = -1
    End Sub

    Sub LoadComputerType()

        Dim ls As ListItem
        ls = New ListItem
        ls.Value = 0
        ls.Text = "----"
        selecttypedropdown.Items.Add(ls)
        Dim reader As SqlDataReader
        strSql = "select typeid,typename from computertype "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)

        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1).ToString.Trim
            selecttypedropdown.Items.Add(ls)
        End While
        reader.Close()
        selecttypedropdown.SelectedValue = 0

    End Sub

    Sub BindData()
        Dim flag As Integer

        strSql = " Select id, typename, assetno, d.name, u.userName, cpu, memory, harddisk, display, keyboard, mouse, ip, mac, os, "
        strSql &= " Case internet When 1 Then N'连接' Else N'未连接' End As internet, description "
        strSql &= " From computerregister cr "
        strSql &= " Inner Join computertype ct On cr.typeid=ct.typeid "
        strSql &= " Left Outer Join Departments d On cr.departmentid = d.departmentID "
        strSql &= " Left Outer Join tcpc0.dbo.Users u On cr.userno = u.userNo And u.plantCode = '" & Session("plantCode") & "'"

        If selecttypedropdown.SelectedValue > 0 Then
            strSql &= " where cr.typeid='" & selecttypedropdown.SelectedValue & "'"
            flag = 1
        End If

        If ddlinternet.SelectedIndex > 0 Then
            If ddlinternet.SelectedValue = 1 Then
                If flag = 1 Then
                    strSql &= " and internet=1"
                Else
                    strSql &= " where internet=1"
                    flag = 1
                End If
            Else
                If flag = 1 Then
                    strSql &= " and internet=2"
                Else
                    strSql &= " where internet=2"
                    flag = 1
                End If
            End If
        End If

        If txbassetno.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and assetno like N'%" & txbassetno.Text.Trim & "%'"
            Else
                strSql &= " where assetno like N'%" & txbassetno.Text.Trim & "%'"
                flag = 1
            End If
        End If

        If txbcpu.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and cpu like N'%" & txbcpu.Text.Trim & "%'"
            Else
                strSql &= " where cpu like N'%" & txbcpu.Text.Trim & "%'"
                flag = 1
            End If
        End If

        If Txbharddisk.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and harddisk like N'%" & Txbharddisk.Text.Trim & "%'"
            Else
                strSql &= " where harddisk like N'%" & Txbharddisk.Text.Trim & "%'"
                flag = 1
            End If
        End If

        If txbdisplay.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and display like N'%" & txbdisplay.Text.Trim & "%'"
            Else
                strSql &= " where display like N'%" & txbdisplay.Text.Trim & "%'"
                flag = 1
            End If
        End If

        If txbkeyboard.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and keyboard like N'%" & txbkeyboard.Text.Trim & "%'"
            Else
                strSql &= " where keyboard like N'%" & txbkeyboard.Text.Trim & "%'"
                flag = 1
            End If
        End If

        If txbmouse.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and mouse like N'%" & txbmouse.Text.Trim & "%'"
            Else
                strSql &= " where mouse like N'%" & txbmouse.Text.Trim & "%'"
                flag = 1
            End If
        End If

        If txbip.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and ip like N'%" & txbip.Text.Trim & "%'"
            Else
                strSql &= " where ip like N'%" & txbip.Text.Trim & "%'"
                flag = 1
            End If
        End If

        If txbmac.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and mac like N'%" & txbmac.Text.Trim & "%'"
            Else
                strSql &= " where mac like N'%" & txbmac.Text.Trim & "%'"
                flag = 1
            End If
        End If

        If txbos.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and os like N'%" & txbos.Text.Trim & "%'"
            Else
                strSql &= " where os like N'%" & txbos.Text.Trim & "%'"
                flag = 1
            End If
        End If

        If txbdes.Text.Trim.Length > 0 Then
            If flag = 1 Then
                strSql &= " and description like N'%" & txbdes.Text.Trim & "%'"
            Else
                strSql &= " where description like N'%" & txbdes.Text.Trim & "%'"
            End If

        End If

        strSql &= " order by assetno"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Session("EXTitle") = "<b>类型</b>~^<b>资产编号</b>~^200^<b>所属部门</b>~^100^<b>姓名</b>~^60^<b>CPU</b>~^<b>内存</b>~^<b>硬盘</b>~^100^<b>显示器</b>~^60^<b>键盘</b>~^60^<b>鼠标</b>~^100^<b>IP地址</b>~^200^<b>MAC地址</b>~^120^<b>操作系统</b>~^80^<b>因特网</b>~^500^<b>描述</b>~^"
        Session("EXSQL") = strSql
        Session("EXHeader") = ""

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("cid", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("typename", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("assetno", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("cpu", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("memory", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("harddisk", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("display", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("keyboard", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("mouse", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("ip", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("mac", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("os", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("internet", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))

        Dim drow As DataRow
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer

                For i = 0 To .Rows.Count - 1
                    drow = dt.NewRow()
                    drow.Item("cid") = .Rows(i).Item("id").ToString().Trim()
                    drow.Item("typename") = .Rows(i).Item("typename").ToString().Trim()
                    drow.Item("assetno") = .Rows(i).Item("assetno").ToString().Trim()
                    drow.Item("cpu") = .Rows(i).Item("cpu").ToString().Trim()
                    drow.Item("memory") = .Rows(i).Item("memory").ToString().Trim()
                    drow.Item("harddisk") = .Rows(i).Item("harddisk").ToString().Trim()
                    drow.Item("display") = .Rows(i).Item("display").ToString().Trim()
                    drow.Item("keyboard") = .Rows(i).Item("keyboard")
                    drow.Item("mouse") = .Rows(i).Item("mouse").ToString().Trim()
                    drow.Item("ip") = .Rows(i).Item("ip").ToString().Trim()
                    drow.Item("mac") = .Rows(i).Item("mac").ToString().Trim()
                    drow.Item("os") = .Rows(i).Item("os").ToString().Trim()
                    drow.Item("internet") = .Rows(i).Item("internet").ToString().Trim()

                    'If .Rows(i).Item("internet") = "1" Then
                    '   drow.Item("internet") = "连接"
                    'Else
                    '   drow.Item("internet") = "未连接"
                    'End If

                    drow.Item("description") = .Rows(i).Item("description").ToString().Trim()
                    dt.Rows.Add(drow)
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
        Btnadd.Enabled = True
        Btnmodify.Enabled = False
    End Sub
    Private Sub Btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnadd.Click

        If selecttypedropdown.SelectedValue = 0 Then
            ltlAlert.Text = "alert('类型不能为空！')"
            Exit Sub
        End If

        If txbassetno.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('资产编号不能为空！')"
            Exit Sub
        Else
            strSql = "select id from computerregister where assetno=N'" & txbassetno.Text.Trim & "'  "
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                ltlAlert.Text = "alert('资产编号已经存在！')"
                Exit Sub
            End If
        End If

        strSql = "insert into computerregister (typeid,assetno,cpu,memory,harddisk,display,keyboard,mouse,ip,mac,os,internet,description,createddate,createdby) values (N'" & selecttypedropdown.SelectedValue & "',N'" & txbassetno.Text.Trim & "',N'" & txbcpu.Text.Trim & "',N'" & txbmemory.Text.Trim & "',N'" & Txbharddisk.Text.Trim & "',N'" & txbdisplay.Text.Trim & "',N'" & txbkeyboard.Text.Trim & "',N'" & txbmouse.Text.Trim & "',N'" & txbip.Text.Trim & "',N'" & txbmac.Text.Trim & "',N'" & txbos.Text.Trim & "','" & ddlinternet.SelectedValue & "',N'" & txbdes.Text.Trim & "',getdate(),'" & Convert.ToInt32(Session("uID")) & "')"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        clearuseless()
        binddata()
    End Sub

    Private Sub Btnmodify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnmodify.Click   

        If selecttypedropdown.SelectedValue = 0 Then
            ltlAlert.Text = "alert('类型不能为空')"
            Exit Sub
        End If

        If txbassetno.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('资产编号不能为空！')"
            Exit Sub
        Else
            strSql = "select id from computerregister where assetno=N'" & txbassetno.Text.Trim & "' and id <> '" & txbid.Text.Trim & "'  "
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                ltlAlert.Text = "alert('资产编号不能重复！')"
                Exit Sub
            End If
        End If


        strSql = "update computerregister set typeid = N'" & selecttypedropdown.SelectedValue & "', assetno=N'" & txbassetno.Text.Trim & "',departmentid = '', userno = '', cpu = '" & txbcpu.Text.Trim & "', memory = N'" & txbmemory.Text.Trim & "',harddisk=N'" & Txbharddisk.Text.Trim & "', display = N'" & txbdisplay.Text.Trim & "', keyboard = N'" & txbkeyboard.Text.Trim & "', mouse = N'" & txbmouse.Text.Trim & "', ip = N'" & txbip.Text.Trim & "', mac = '" & txbmac.Text.Trim & "', os = N'" & txbos.Text.Trim & "',internet='" & ddlinternet.SelectedValue & "',description=N'" & txbdes.Text.Trim & "',modifieddate = getdate(),modifiedby = '" & Convert.ToInt32(Session("uID")) & "' where id = '" & txbid.Text.Trim & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

        clearuseless()
        BindData()
        Btnadd.Enabled = True
        Btnmodify.Enabled = False

    End Sub

    Private Sub datagrid1_DeleteCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles datagrid1.ItemCommand

        If e.CommandName.CompareTo("DeleteClick") = 0 Then

            strSql = "select count(*) from computerhis where cid =' " & e.Item.Cells(0).Text.Trim() & "' "
            If Convert.ToInt16(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)) Then
                ltlAlert.Text = "alert('存在使用记录，不能删除！')"
                Exit Sub
            End If

            strSql = "delete from computerregister where id =' " & e.Item.Cells(0).Text.Trim() & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            clearuseless()
            BindData()
            Exit Sub

        End If

        If e.CommandName.CompareTo("Select") = 0 Then

            clearuseless()

            strSql = "select typeid from computertype where typename=N'" & e.Item.Cells(1).Text.Trim() & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While (reader.Read())
                selecttypedropdown.SelectedValue = reader(0)
            End While
            reader.Close()

            txbid.Text = e.Item.Cells(0).Text.Trim()

            If e.Item.Cells(2).Text.Trim() <> "&nbsp;" Then
                txbassetno.Text = e.Item.Cells(2).Text.Trim()
            End If

            If e.Item.Cells(3).Text.Trim() <> "&nbsp;" Then
                txbcpu.Text = e.Item.Cells(3).Text.Trim()
            End If

            If e.Item.Cells(4).Text.Trim() <> "&nbsp;" Then
                txbmemory.Text = e.Item.Cells(4).Text.Trim()
            End If

            If e.Item.Cells(5).Text.Trim() <> "&nbsp;" Then
                Txbharddisk.Text = e.Item.Cells(5).Text.Trim()
            End If

            If e.Item.Cells(6).Text.Trim() <> "&nbsp;" Then
                txbdisplay.Text = e.Item.Cells(6).Text.Trim()
            End If

            If e.Item.Cells(7).Text.Trim() <> "&nbsp;" Then
                txbkeyboard.Text = e.Item.Cells(7).Text.Trim()
            End If

            If e.Item.Cells(8).Text.Trim() <> "&nbsp;" Then
                txbmouse.Text = e.Item.Cells(8).Text.Trim()
            End If

            If e.Item.Cells(9).Text.Trim() <> "&nbsp;" Then
                txbip.Text = e.Item.Cells(9).Text.Trim()
            End If

            If e.Item.Cells(10).Text.Trim() <> "&nbsp;" Then
                txbmac.Text = e.Item.Cells(10).Text.Trim()
            End If

            If e.Item.Cells(11).Text.Trim() <> "&nbsp;" Then
                txbos.Text = e.Item.Cells(11).Text.Trim()
            End If

            ddlinternet.SelectedValue = IIf(e.Item.Cells(12).Text.Trim() = "连接", 1, 2)

            If e.Item.Cells(16).Text.Trim() <> "&nbsp;" Then
                txbdes.Text = e.Item.Cells(16).Text.Trim()
            End If

            Btnmodify.Enabled = True
            Btnadd.Enabled = False
            flag1 = 1
            Exit Sub
        End If

        If e.CommandName.CompareTo("Detail1") = 0 Then
            ltlAlert.Text = "window.showModalDialog('computerhis.aspx?id=" & Server.UrlEncode(e.Item.Cells(0).Text.Trim()) & "&type=" & Server.UrlEncode(e.Item.Cells(1).Text.Trim()) & "&assetno=" & Server.UrlEncode(e.Item.Cells(2).Text.Trim()) & "', window, 'dialogHeight: 600px; dialogWidth: 700px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');"
        End If

    End Sub

    Protected Sub datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles datagrid1.PageIndexChanged
        If datagrid1.SelectedIndex <> -1 Then
            ltlAlert.Text = "alert('请先取消当前选中行！')"
        Else
            datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End If
    End Sub
End Class
