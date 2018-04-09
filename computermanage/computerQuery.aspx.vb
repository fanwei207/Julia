Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Partial Class computermanage_computerQuery
    Inherits BasePage 
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Public chk As New adamClass
    Dim strSQL = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            
            LoadDepartments()
            LoadUsername()
            LoadStatus()
            LoadComputerType()
            BindData() 
        End If
    End Sub
    Private Sub BindData()
        strSQL = ""
        Dim ds As DataSet
        strSQL = "select t.typename,c.assetno,c.cpu,c.memory,c.harddisk,c.display,c.keyboard,c.mouse,"
        strSQL &= " c.ip,c.mac,c.os,case c.internet when 0 then '' when 1 then N'连接' else N'未连接' end as net, "
        strSQL &= " c.description, d.departmentname, d.userno, d.username, d.begindate, d.enddate, d.status "
        strSQL &= " from computerregister c inner join computertype t on c.typeid = t.typeid "
        strSQL &= " left join (select * from computerhis where id in (select max(id) from computerhis group by assetno) ) d on c.id = d.cid "
        strSQL &= " where c.assetno is not null "

        If drptype.SelectedValue <> "0" Then
            strSQL &= " and t.typename = N'" & drptype.SelectedItem.Text.Trim() & "'"
        End If

        If drpdepartment.SelectedValue <> "0" Then
            strSQL &= " and d.departmentname = N'" & drpdepartment.SelectedItem.Text & "'"
        End If

        If drpusername.SelectedItem.Text <> "----" Then
            strSQL &= " and d.userno = '" & drpusername.SelectedValue.ToString() & "'"
        End If

        If txbassetno.Text.Trim.Length > 0 Then
            strSQL &= " and c.assetno = '" & txbassetno.Text.Trim() & "'"
        End If

        If drpnet.SelectedValue <> "0" Then
            strSQL &= " and c.internet = '" & drpnet.SelectedValue & "' "
        End If

        If drpstatus.SelectedValue <> "0" Then
            strSQL &= " and d.status = N'" & drpstatus.SelectedItem.Text.Trim() & "'"
        End If

        strSQL &= " order by c.assetno "

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        Dim dt As New DataTable
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
        dt.Columns.Add(New DataColumn("net", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("departmentname", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("username", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("begindate", System.Type.GetType("System.DateTime")))
        dt.Columns.Add(New DataColumn("enddate", System.Type.GetType("System.DateTime")))
        dt.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))

        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("typename") = .Rows(i).Item("typename")
                    dr1.Item("assetno") = .Rows(i).Item("assetno")
                    dr1.Item("cpu") = .Rows(i).Item("cpu")
                    dr1.Item("memory") = .Rows(i).Item("memory")
                    dr1.Item("harddisk") = .Rows(i).Item("harddisk")
                    dr1.Item("display") = .Rows(i).Item("display")
                    dr1.Item("keyboard") = .Rows(i).Item("keyboard")
                    dr1.Item("mouse") = .Rows(i).Item("mouse")
                    dr1.Item("ip") = .Rows(i).Item("ip")
                    dr1.Item("mac") = .Rows(i).Item("mac")
                    dr1.Item("os") = .Rows(i).Item("os")
                    dr1.Item("net") = .Rows(i).Item("net")
                    dr1.Item("departmentname") = .Rows(i).Item("departmentname")
                    dr1.Item("username") = .Rows(i).Item("username")
                    dr1.Item("begindate") = .Rows(i).Item("begindate")
                    dr1.Item("enddate") = .Rows(i).Item("enddate")
                    dr1.Item("status") = .Rows(i).Item("status")
                    dr1.Item("description") = .Rows(i).Item("description")

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
    Sub LoadDepartments()
        strSQL = ""

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
            strSQL = "SELECT userno,userName From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and leaveDate is null and deleted=0 and leavedate is null and isactive=1 and roleID<>1 And departmentID='" & drpdepartment.SelectedValue & " ' Order by UserName Collate Chinese_PRC_Stroke_ci_as "
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
    Sub LoadComputerType()

        Dim ls As ListItem
        ls = New ListItem
        ls.Value = 0
        ls.Text = "----"
        drptype.Items.Add(ls)
        Dim reader As SqlDataReader
        strSQL = "select typeid,typename from computertype "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)

        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1).ToString.Trim
            drptype.Items.Add(ls)
        End While
        reader.Close()
        drptype.SelectedValue = 0

    End Sub

    Protected Sub datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles datagrid1.PageIndexChanged
        datagrid1.CurrentPageIndex = e.NewPageIndex
        datagrid1.EditItemIndex = -1
        BindData()
    End Sub

    Protected Sub btnser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnser.Click
        datagrid1.SelectedIndex = -1
        datagrid1.CurrentPageIndex = 0
        BindData()
    End Sub

    Protected Sub drpdepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdepartment.SelectedIndexChanged
        LoadUsername()
    End Sub

    Protected Sub btncel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncel.Click
        datagrid1.SelectedIndex = -1
        drpdepartment.SelectedValue = 0
        drpusername.SelectedValue = 0
        drptype.SelectedValue = 0
        drpnet.SelectedValue = 0
        txbassetno.Text = ""
        drpstatus.SelectedValue = 0
    End Sub
End Class
