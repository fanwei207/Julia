Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class personnellistDept
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents P1 As System.Web.UI.WebControls.Panel
        Protected WithEvents chkall As System.Web.UI.WebControls.CheckBox
        Shared sortOrder As String = ""
        Protected strTemp As String
        Protected WithEvents BtnSearch As System.Web.UI.WebControls.Button
        Protected WithEvents BtnExport As System.Web.UI.WebControls.Button
        Shared sortDir As String = "ASC"

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
            If Not IsPostBack Then
                Dim item As ListItem

                DropDownList1.Enabled = True

                Dim Query As String
                Dim reader As SqlDataReader
                If (Convert.ToInt32(Session("uRole")) = 1) Then
                    Query = "SELECT d.departmentID,d.Name From Departments d WHERE d.isSalary='1' order by d.name"
                Else
                    Query = "SELECT d.departmentID,d.Name From Departments d Inner Join User_Department ud On ud.departmentID=d.departmentID and ud.userID='" & Session("uID") & "' WHERE d.isSalary='1' order by d.name"
                End If
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
                While reader.Read()
                    item = New ListItem(reader(1))
                    item.Value = Convert.ToInt32(reader(0))
                    DropDownList1.Items.Add(item)
                End While
                reader.Close()
                DropDownList1.SelectedIndex = 0


                If DropDownList1.Items.Count <= 0 Then
                    ltlAlert.Text = "alert('没有查看部门员工的权限！该权限需要申请！');"
                    Exit Sub
                End If

                BindData()
            End If
        End Sub
        Private Sub BindData()
            Dim query As String
            Dim ds As DataSet

            query = " SELECT u.userID,u.userNo,u.userName,r.roleName,d.Name,isnull(w1.name,'') as wname,isnull(w2.name,'') as gname,isnull(sc1.systemcodeName,'') as gender,datediff(year,birthday,getdate()) as age,isnull(sc2.systemcodeName,'') as edu, enterDate "
            query &= " From tcpc0.dbo.users u "
            query &= " Left outer JOIN Roles r ON u.roleID=r.roleID "
            query &= " Left outer JOIN Departments d ON d.departmentID=u.departmentID "
            query &= " Left outer JOIN tcpc0.dbo.systemCode sc1 ON u.sexID=sc1.systemcodeID "
            query &= " Left outer JOIN tcpc0.dbo.systemCodeType st1 ON sc1.systemcodetypeID=st1.systemcodetypeID and st1.systemCodeTypeName='Sex' "
            query &= " Left outer JOIN tcpc0.dbo.systemCode sc2 ON u.educationID=sc2.systemcodeID "
            query &= " Left outer JOIN tcpc0.dbo.systemCodeType st2 ON sc2.systemcodetypeID=st2.systemcodetypeID and st2.systemCodeTypeName='Education' "
            query &= " Left outer JOIN Workshop w1 ON w1.id=u.workshopID "
            query &= " Left outer JOIN Workshop w2 ON w2.id=u.workProcedureID "
            query &= " Where u.plantCode='" & Session("PlantCode") & "' and u.leaveDate is null and u.deleted=0 and u.roleID>1 and u.organizationID=" & Session("orgID")
            query &= " and  u.isTemp='" & Session("temp") & "'  and u.departmentID=" & DropDownList1.SelectedValue
            query &= " Order by u.userID "

            'Response.Write(query)
            'Exit Sub

            Session("EXSQL") = query

            Session("EXTitle") = "<b>工号</b>~^<b>姓名</b>~^<b>职务</b>~^<b>部门</b>~^<b>工段</b>~^<b>班组</b>~^<b>性别</b>~^<b>年龄</b>~^<b>学历</b>~^<b>进入公司日期</b>~^"

            Dim total As Integer = 0

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("roleName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("departmentName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("depart1", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("depart2", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("sex", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("age", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("education", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("enterDate", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("userID") = .Rows(i).Item(0)
                        dr1.Item("gsort") = i + 1
                        dr1.Item("userNo") = .Rows(i).Item(1)
                        dr1.Item("name") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("roleName") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("departmentName") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("depart1") = .Rows(i).Item(5).ToString().Trim()
                        dr1.Item("depart2") = .Rows(i).Item(6).ToString().Trim()
                        dr1.Item("sex") = .Rows(i).Item(7).ToString().Trim()
                        dr1.Item("age") = .Rows(i).Item(8)
                        dr1.Item("education") = .Rows(i).Item(9).ToString().Trim()
                        If .Rows(i).Item(10).ToString().Trim() <> "" Then
                            dr1.Item("enterDate") = Format(.Rows(i).Item(10), "yyyy-MM-dd")
                        Else
                            dr1.Item("enterDate") = ""
                        End If
                        total = total + 1
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Label1.Text = "<b>人数： </b>" & total.ToString()
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub

        Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            BindData()
        End Sub

        Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace
