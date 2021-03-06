'!*******************************************************************************!
'* @@ NAME				:	contractInformation.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for contractInformation.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	March 18 2005
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class contractInformation
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents ltlAlert As Literal

    Dim strSql As String
    Dim reader As SqlDataReader
    Dim chk As New adamClass
    Dim ds As DataSet
    Shared sortOrder As String = ""
    Shared sortDir As String = "ASC"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            type.Items.Add(New ListItem("--", "0"))
            type.Items.Add(New ListItem("员工试用期", "1"))
            type.Items.Add(New ListItem("员工合同期", "2"))
            If Request("sel") <> Nothing Then
                type.SelectedIndex = Request("sel")
            End If
            dropdownValue()
            SaleBind(0)
        End If
    End Sub
    Sub dropdownValue()
        department.Items.Add(New ListItem("--", "0"))

        strSql = " Select departmentID,name From departments Where  isSalary='1' and active='1'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read()
            Dim tempListItem As New ListItem
            tempListItem.Value = reader(0)
            tempListItem.Text = reader(1)
            department.Items.Add(tempListItem)
        End While
        reader.Close()
    End Sub
    Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
        SaleBind(1)
    End Sub

    Sub SaleBind(ByVal temp As Integer)
            'If type.SelectedIndex > 0 Then
            strSql = " Select u.userID,u.userNo,u.userName as name,r.roleName,d.Name,isnull(sc1.systemcodeName,''),datediff(year,u.birthday,getdate()) as birthday,isnull(sc2.systemcodeName,''),u.enterDate ,w.name,u.contractDate "
            strSql &= " From tcpc0.dbo.users u "
            strSql &= " Left outer JOIN Roles r ON u.roleID=r.roleID "
            strSql &= " Left outer JOIN Departments d ON d.departmentID=u.departmentID "
            strSql &= " Left outer JOIN tcpc0.dbo.systemCode sc1 ON u.sexID=sc1.systemcodeID "
            strSql &= " Left outer JOIN tcpc0.dbo.systemCodeType st1 ON sc1.systemcodetypeID=st1.systemcodetypeID and st1.systemCodeTypeName='Sex' "
            strSql &= " Left outer JOIN tcpc0.dbo.systemCode sc2 ON u.educationID=sc2.systemcodeID "
            strSql &= " Left outer JOIN tcpc0.dbo.systemCodeType st2 ON sc2.systemcodetypeID=st2.systemcodetypeID and st2.systemCodeTypeName='Education' "
            strSql &= " Left outer JOIN Workshop w ON w.id=u.workshopID  "
            strSql &= " Where isnull(u.plantCode,0)='" & Session("PlantCode") & "' and isnull(u.deleted,0)=0 and isnull(u.roleID,100)>1 "
            'strSql &= " and u.isTemp='" & Session("temp") & "' "
            'for chiose rearch button--------
            'If temp <> 0 Then
            If workerNoSearch.Text.Trim() <> "" Then
                strSql &= " and cast(u.userNo as varchar)='" & workerNoSearch.Text.Trim & "'"
            End If
            If workerNameSearch.Text.Trim() <> "" Then
                strSql &= " and lower(u.userName) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
            End If
            If department.SelectedValue > 0 Then
                strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
            End If
            'End If
            ' for Type display data
            Select Case type.SelectedValue
                Case 1
                    strSql &= " and employDate is null and leaveDate is null "
                Case 2
                    strSql &= " and contractDate is not null and datediff(month,u.contractDate,getdate())>='10' and leaveDate is null"
                Case 0
                    strSql &= " and leaveDate is null "
            End Select

            strSql &= " Order by u.userID "

            Session("EXTitle") = "60^<b>工号</b>~^60^<b>姓名</b>~^70^<b>职务</b>~^120^<b>部门</b>~^30^<b>性别</b>~^30^<b>年龄</b>~^40^<b>学历</b>~^120^<b>进入公司日期</b>~^<b>工段</b>~^120^<b>合同到期日期</b>~^"
            Session("EXSQL") = strSql
            Session("EXHeader") = "员工合同查询    导出日期: " & Format(DateTime.Today, "yyyy-MM-dd")

            'Response.Write(strSql)
            'Exit Sub
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable

            'dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("roleName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("departmentName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("sex", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("age", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("education", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("enterDate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            'dt.Columns.Add(New DataColumn("edate", System.Type.GetType("System.DateTime")))
            dt.Columns.Add(New DataColumn("contDate", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("userID") = .Rows(i).Item(0)
                        dr1.Item("userNo") = .Rows(i).Item(1)
                        'dr1.Item("gsort") = i + 1
                        dr1.Item("name") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("roleName") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("departmentName") = .Rows(i).Item(4).ToString().Trim()
                        'dr1.Item("userName") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("sex") = .Rows(i).Item(5).ToString().Trim()
                        dr1.Item("age") = .Rows(i).Item(6)
                        dr1.Item("education") = .Rows(i).Item(7).ToString().Trim()
                        If .Rows(i).Item(8).ToString() <> "" Then
                            dr1.Item("enterDate") = .Rows(i).Item(8).ToShortDateString()
                        Else
                            dr1.Item("enterDate") = ""
                        End If
                        dr1.Item("workshop") = .Rows(i).Item(9).ToString().Trim()

                        If .Rows(i).Item(10).ToString() <> "" Then
                            dr1.Item("contDate") = .Rows(i).Item(10).ToShortDateString()
                        Else
                            dr1.Item("contDate") = ""
                        End If
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try

            'End If
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        SaleBind(1)
    End Sub

    Public Sub editBt(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandName.CompareTo("editBt") = 0) Then
            Dim str As String = e.Item.Cells(10).Text
                'ltlAlert.Text = "openWin('addpersonnel.aspx?flag=" & type.SelectedIndex & "&id='+'" & str & "' );"
                Response.Redirect("addpersonnel.aspx?flag=" & type.SelectedIndex & "&id=" & str)
        End If
    End Sub

    Sub type_choise(ByVal sender As Object, ByVal e As System.EventArgs)
        If type.SelectedIndex > 0 Then
            SaleBind(1)
        End If
    End Sub
End Class

End Namespace
