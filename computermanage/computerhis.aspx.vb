Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Partial Class computermanage_computerhis
    Inherits BasePage
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    'Protected WithEvents ltlAlert As Literal
    Public chk As New adamClass
    Dim strSQL = ""
    Dim isMod As Boolean = False

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then

            lbltype.Text = "类型 : " & Server.UrlDecode(Request("type")).Trim()
            lblassetno.Text = "设备编号 : " & Server.UrlDecode(Request("assetno")).Trim()

            BindData()
            LoadDepartments()
            LoadUsername()
            LoadStatus()

            btnAdd.Enabled = True
            btnMod.Enabled = False

        End If
    End Sub
    Private Sub BindData()
        strSQL = ""
        Dim ds As DataSet
        strSQL = "select id,username,departmentname,begindate,enddate,status,demo from computerhis "
        strSQL &= " where cid = '" & Server.UrlDecode(Request("id")).Trim & "' "
        strSQL &= " order by id "

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("departmentname", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("username", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("begindate", System.Type.GetType("System.DateTime")))
        dt.Columns.Add(New DataColumn("enddate", System.Type.GetType("System.DateTime")))
        dt.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("demo", System.Type.GetType("System.String")))

        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("id") = .Rows(i).Item(0)
                    dr1.Item("username") = .Rows(i).Item(1)
                    dr1.Item("departmentname") = .Rows(i).Item(2)
                    dr1.Item("begindate") = .Rows(i).Item(3)
                    dr1.Item("enddate") = .Rows(i).Item(4)
                    dr1.Item("status") = .Rows(i).Item(5)
                    dr1.Item("demo") = .Rows(i).Item(6)
                    dt.Rows.Add(dr1)
                Next
            End If
        End With
        ds.Reset()

        Dim dv As DataView
        dv = New DataView(dt)
        datagrid1.DataSource = dv
        datagrid1.DataBind()
    End Sub

    Protected Sub datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles datagrid1.PageIndexChanged
        datagrid1.CurrentPageIndex = e.NewPageIndex
        datagrid1.EditItemIndex = -1
        BindData()
    End Sub

    Protected Sub datagrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles datagrid1.ItemCommand

        strSQL = ""
        Dim reader As SqlDataReader

        If e.CommandName.CompareTo("DeleteClick") = 0 Then
            strSQL = "delete from computerhis where id =' " & e.Item.Cells(0).Text.Trim() & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            BindData()
            Exit Sub
        ElseIf e.CommandName.CompareTo("Select") = 0 Then
            isMod = True

            If e.Item.Cells(1).Text.Trim() <> "&nbsp;" Then
                txbbegindate.Text = e.Item.Cells(1).Text.Trim()
            End If

            If e.Item.Cells(2).Text.Trim() <> "&nbsp;" Then
                txbenddate.Text = e.Item.Cells(2).Text.Trim()
            End If

            strSQL = "select fixsta_id from tcpc0.dbo.fixsta_mstr where fixsta_name=N'" & e.Item.Cells(5).Text.Trim() & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While (reader.Read())
                drpstatus.SelectedValue = reader(0)
            End While
            reader.Close()

            strSQL = "select departmentid from departments where name=N'" & e.Item.Cells(3).Text.Trim() & "' and active=1 and issalary=1"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While (reader.Read())
                drpdepartment.SelectedValue = reader(0)
            End While
            reader.Close()
            LoadUsername()

            If isMod = False Then
                strSQL = "select userno from tcpc0.dbo.users u inner join departments d on u.departmentid=d.departmentid where leavedate is null and deleted=0 and isActive=1 and username=N'" & e.Item.Cells(4).Text.Trim() & "'and plantcode='" & Session("plantcode") & "'and d.name=N'" & e.Item.Cells(3).Text.Trim() & "'"
            Else
                strSQL = "select userno from tcpc0.dbo.users u inner join departments d on u.departmentid=d.departmentid where deleted=0 and isActive=1 and username=N'" & e.Item.Cells(4).Text.Trim() & "'and plantcode='" & Session("plantcode") & "'and d.name=N'" & e.Item.Cells(3).Text.Trim() & "'"
            End If

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While (reader.Read())
                drpusername.SelectedValue = reader(0)
            End While
            reader.Close()

            If e.Item.Cells(6).Text.Trim() <> "&nbsp;" Then
                txbdemo.Text = e.Item.Cells(6).Text.Trim()
            End If


            btnAdd.Enabled = False
            btnMod.Enabled = True
        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        txbbegindate.Text = ""
        txbenddate.Text = ""
        drpdepartment.SelectedValue = 0
        drpusername.Items.Clear()
        drpstatus.SelectedValue = 0
        btnAdd.Enabled = True
        btnMod.Enabled = False
        txbdemo.Text = ""
        datagrid1.SelectedIndex = -1

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        If txbenddate.Text.Trim().Length > 0 Then
            If Convert.ToDateTime(txbbegindate.Text) > Convert.ToDateTime(txbenddate.Text) Then
                ltlAlert.Text = "alert('开始日期不能大于结束日期！');"
                Exit Sub
            End If
        End If

        If drpdepartment.SelectedValue <= 0 Then
            ltlAlert.Text = "alert('请选择部门！');"
            Exit Sub
        End If

        If drpusername.SelectedItem.Text = "----" Then
            ltlAlert.Text = "alert('请选择使用用户或负责人！');"
            Exit Sub
        End If

        If drpstatus.SelectedValue <= 0 Then
            ltlAlert.Text = "alert('请选择设备状态！');"
            Exit Sub
        End If


        Dim strEnd As String = ""

        strSQL = " select count(*) from computerhis where cid = '" & Server.UrlDecode(Request("id")).Trim() & "'"
        If Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)) > 0 Then

            strSQL = " select enddate from computerhis where cid = '" & Server.UrlDecode(Request("id")).Trim() & "' order by id desc"
            strEnd = Convert.ToString(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL))
            If strEnd = "" Then
                ltlAlert.Text = "alert('请填写上一条记录的结束日期！');"
                Exit Sub
            Else
                If Convert.ToDateTime(strEnd) > Convert.ToDateTime(txbbegindate.Text) Then
                    ltlAlert.Text = "alert('此条开始日期小于上一条记录的结束日期！');"
                    Exit Sub
                End If
            End If

        End If



        If (txbenddate.Text.Trim().Length > 0) Then
            strSQL = " Insert Into computerhis(cid,departmentname,userno,username,assetno,begindate,enddate,status,demo,createddate,createdby) "
        Else
            strSQL = " Insert Into computerhis(cid,departmentname,userno,username,assetno,begindate,status,demo,createddate,createdby) "
        End If

        strSQL &= " values('" & Server.UrlDecode(Request("id")).Trim() & "',N'" & drpdepartment.SelectedItem.Text & "',"
        strSQL &= " '" & drpusername.SelectedValue & "',N'" & drpusername.SelectedItem.Text & "','" & Server.UrlDecode(Request("assetno")).Trim() & "',"
        strSQL &= " '" & txbbegindate.Text.Trim() & "',"

        If txbenddate.Text.Trim().Length > 0 Then
            strSQL &= "'" & txbenddate.Text.Trim() & "',"
        End If

        strSQL &= " N'" & drpstatus.SelectedItem.Text & "',N'" & txbdemo.Text.Trim() & "',getdate(),'" & Convert.ToInt32(Session("uID")) & "'"
        strSQL &= " )"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)


        txbbegindate.Text = ""
        txbenddate.Text = ""
        drpdepartment.SelectedValue = 0
        drpusername.Items.Clear()
        drpstatus.SelectedValue = 0
        txbdemo.Text = ""
        btnAdd.Enabled = True
        btnMod.Enabled = False
        datagrid1.SelectedIndex = -1

        BindData()



    End Sub

    Protected Sub btnMod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMod.Click
        If txbenddate.Text.Trim().Length > 0 Then
            If Convert.ToDateTime(txbbegindate.Text) > Convert.ToDateTime(txbenddate.Text) Then
                ltlAlert.Text = "alert('开始日期不能大于结束日期！');"
                Exit Sub
            End If
        End If

        If drpdepartment.SelectedValue <= 0 Then
            ltlAlert.Text = "alert('请选择部门！');"
            Exit Sub
        End If

        If drpusername.SelectedItem.Text = "----" Then
            ltlAlert.Text = "alert('请选择使用用户或负责人！');"
            Exit Sub
        End If

        If drpstatus.SelectedValue <= 0 Then
            ltlAlert.Text = "alert('请选择设备状态！');"
            Exit Sub
        End If


        strSQL = " update computerhis "
        strSQL &= " set cid = '" & Server.UrlDecode(Request("id")).Trim() & "',departmentname = N'" & drpdepartment.SelectedItem.Text & "',"
        strSQL &= " userno = '" & drpusername.SelectedValue & "',username = N'" & drpusername.SelectedItem.Text & "',assetno = '" & Server.UrlDecode(Request("assetno")).Trim() & "',"
        strSQL &= " begindate = '" & txbbegindate.Text.Trim() & "',"

        If txbenddate.Text.Trim().Length > 0 Then
            strSQL &= " enddate = '" & txbenddate.Text.Trim() & "',"
        Else
            strSQL &= " enddate = null,"
        End If

        strSQL &= " status =  N'" & drpstatus.SelectedItem.Text & "',demo = N'" & txbdemo.Text.Trim() & "',modifieddate = getdate(),modifiedby = '" & Convert.ToInt32(Session("uID")) & "'"
        strSQL &= " where id = '" & datagrid1.SelectedItem.Cells(0).Text & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)


        txbbegindate.Text = ""
        txbenddate.Text = ""
        drpdepartment.SelectedValue = 0
        drpusername.Items.Clear()
        drpstatus.SelectedValue = 0
        btnAdd.Enabled = True
        btnMod.Enabled = False
        datagrid1.SelectedIndex = -1

        BindData()
    End Sub

    Sub LoadDepartments()
        strSQL = ""
        drpdepartment.Items.Clear()
        Dim ls As ListItem
        ls = New ListItem
        ls.Value = 0
        ls.Text = "----"
        drpdepartment.Items.Add(ls)
        Dim reader As SqlDataReader
        strSQL = "select departmentid,name from departments where active=1 and issalary=1"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
        While (reader.Read)
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1).ToString.Trim
            drpdepartment.Items.Add(ls)
        End While
        reader.Close()
        drpdepartment.SelectedValue = 0
    End Sub

    Protected Sub drpdepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdepartment.SelectedIndexChanged
        LoadUsername()
    End Sub
    Sub LoadUsername()

        drpusername.Items.Clear()

        strSQL = ""
        Dim ls As ListItem
        ls = New ListItem
        ls.Value = 0
        ls.Text = "----"
        drpusername.Items.Add(ls)
        drpusername.SelectedValue = 0

        If drpdepartment.SelectedValue > 0 Then

            Dim reader As SqlDataReader
            If isMod = False Then
                strSQL = "SELECT userno,userName From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and leaveDate is null and deleted=0 and leavedate is null and isactive=1 and roleID<>1 And departmentID='" & drpdepartment.SelectedValue & " ' Order by UserName Collate Chinese_PRC_Stroke_ci_as "
            Else
                strSQL = "SELECT userno,userName From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and deleted=0 and isactive=1 and roleID<>1 And departmentID='" & drpdepartment.SelectedValue & " ' Order by UserName Collate Chinese_PRC_Stroke_ci_as "
            End If
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)

            While (reader.Read)
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString()
                drpusername.Items.Add(ls)
            End While

            reader.Close()

        End If
    End Sub
    Sub LoadStatus()
        strSQL = ""
        Dim ls As ListItem
        ls = New ListItem
        ls.Value = 0
        ls.Text = "----"
        drpstatus.Items.Add(ls)
        Dim reader As SqlDataReader
        strSQL = "select fixsta_id,fixsta_name from tcpc0.dbo.fixsta_mstr "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1).ToString.Trim
            drpstatus.Items.Add(ls)
        End While
        reader.Close()
        drpstatus.SelectedValue = 0
    End Sub

End Class
