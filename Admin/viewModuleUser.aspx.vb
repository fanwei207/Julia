Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class viewModuleUser
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass

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
            If Not IsPostBack Then

                Dim item As ListItem
                Dim strSQL As String
                Dim ds As DataSet

                item = New ListItem("--")
                item.Value = 0
                quanxian.Items.Add(item)
                quanxian.SelectedIndex = 0

                strSQL = "SELECT m.id,m.name,isnull(m.isMenu,0)isMenu  From tcpc0.dbo.Menu m where m.sortOrder is not null and m.url<>'null' Order by m.sortOrder "

                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            If .Rows(i).Item(2) = True Then
                                item = New ListItem(.Rows(i).Item(1))
                            Else
                                item = New ListItem(" -- " & .Rows(i).Item(1))
                            End If
                            quanxian.Items.Add(item)
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        Next i
                    End If
                End With
                ds.Reset()

                dd_dp.SelectedIndex = 0
                item = New ListItem("--")
                item.Value = 0
                dd_dp.Items.Add(item)
                strSQL = "SELECT departmentID,name From departments where issalary=1 order by name"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            dd_dp.Items.Add(item)
                        Next
                    End If
                End With
                ds.Reset()

                btn_del.Attributes.Add("onclick", "return confirm('确定要全部删除吗？');")
            End If
        End Sub

        Private Sub quanxian_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles quanxian.SelectedIndexChanged
            BindData()
        End Sub

        Sub BindData()
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT distinct(u.userName), d.name, u.userID, u.userno From tcpc0.dbo.users u Left Outer Join tcpc0.dbo.AccessRule a On u.userID=a.userID Left outer JOIN Departments d ON d.departmentID=u.departmentID Where u.plantCode='" & Session("PlantCode") & "' "
            If quanxian.SelectedIndex > 0 Then
                If dd_dp.SelectedIndex > 0 Then
                    strSQL &= " and u.departmentID='" & dd_dp.SelectedValue & "'"
                End If
                strSQL &= " and leavedate is null and a.moduleID='" & quanxian.SelectedValue & "' And "
                strSQL &= " u.deleted=0 And u.isactive=1 And u.roleID<>1 And organizationID=" & Session("orgID") & " Order by UserName "
                Session("EXHeader") = "权限~" & quanxian.SelectedItem.Text & "~^工号~" & Session("uName") & " (" & Session("uID") & ")~^日期~" & Format(DateTime.Now, "yyyy-MM-dd")
                Session("EXTitle") = "100^<b>名字</b>~^<b>部门</b>~^<b>ID</b>~^<b>工号</b>~^"
                Session("EXSQL") = strSQL
            End If


            Dim total As Integer = 0

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("Name") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("department") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("userID") = .Rows(i).Item(2)
                        dr1.Item("code") = .Rows(i).Item(3)
                        dt.Rows.Add(dr1)
                        total = total + 1
                    Next
                End If
            End With
            ds.Reset()

            lbl_people.Text = "人数： " & total.ToString()

            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try

        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strSQL As String
                strSQL = " Delete From tcpc0.dbo.AccessRule where userID='" & e.Item.Cells(5).Text & "' and moduleID='" & quanxian.SelectedValue & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                BindData()
            End If
        End Sub

        Protected Sub btn_del_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_del.Click
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT u.userID From tcpc0.dbo.users u Left Outer Join tcpc0.dbo.AccessRule a On u.userID=a.userID Left outer JOIN Departments d ON d.departmentID=u.departmentID Where u.plantCode='" & Session("PlantCode") & "' "
            strSQL &= " and u.departmentID='" & dd_dp.SelectedValue & "'"
            strSQL &= " and a.moduleID='" & quanxian.SelectedValue & "' And "
            strSQL &= " u.roleID<>1"

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        Dim strSQL2 As String
                        strSQL2 = " Delete From tcpc0.dbo.AccessRule where userID='" & .Rows(i).Item(0) & "' and moduleID='" & quanxian.SelectedValue & "'"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL2)
                    Next
                End If
            End With
            ds.Reset()

            dd_dp.SelectedIndex = 0
            BindData()
        End Sub

        Protected Sub dd_dp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_dp.SelectedIndexChanged
            BindData()
        End Sub

        Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace

