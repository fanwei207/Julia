'!*******************************************************************************!
'* @@ NAME				:	ComfirmInspect.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for ComfirmInspect.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	March 25 2005
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class ComfirmInspect
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then

                SaleBind(0)
            End If
        End Sub

        Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
            SaleBind(1)
        End Sub

        Sub SaleBind(ByVal temp As Integer)
            strSql = " Select u.userNo,u.userName as name,r.roleName,d.Name,w.name ,uh.healthCheckDate "
            strSql &= " From User_Health uh "
            strSql &= " Inner join tcpc0.dbo.users u on u.userID=uh.userID "
            strSql &= " Left outer JOIN Roles r ON u.roleID=r.roleID "
            strSql &= " Left outer JOIN Departments d ON d.departmentID=u.departmentID "
            strSql &= " Left outer JOIN Workshop w ON w.id=u.workshopID  "

            strSql &= " where uh.status='2' and  uh.userID=" & Request("uid")
            strSql &= " Order by uh.healthCheckDate desc "

            'Session("EXSQL") = strSql
            'Session("EXTitle") = "<b>工号</b>~^<b>姓名</b>~^<b>职务</b>~^<b>部门</b>~^<b>工段</b>~^<b>性别</b>~^<b>年龄</b>~^<b>体检日期</b>~^"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("roleName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("departmentName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("healthCheckDate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.String")))
            Dim i As Integer
            With ds.Tables(0)
                If (.Rows.Count > 0) Then

                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("userID") = .Rows(i).Item(0)
                        dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("roleName") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("departmentName") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("workshop") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("healthCheckDate") = Format(.Rows(i).Item(5), "yyyy-MM-dd")
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
            numtotal.Text = i.ToString()
        End Sub

        Sub changeuniform(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim j As Integer
            For j = 0 To DataGrid1.Items.Count - 1
                If CType(DataGrid1.Items(j).FindControl("changed"), CheckBox).Checked = True Then
                    strSql = " update User_Health set status='2' where userID='" & DataGrid1.Items(j).Cells(0).Text & "' "
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                    strSql = " Insert into User_Health (userID,healthCheckDate,status,createdBy,createdDate) Values ('" & DataGrid1.Items(j).Cells(0).Text & "','" & DateTime.Now.ToShortDateString() & "','0'," & Session("uID") & ",getdate() )"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                End If
            Next
            SaleBind(1)
        End Sub

        Sub all_check(ByVal sender As Object, ByVal e As System.EventArgs)
            'SaleBind(1)
            Dim j As Integer
            For j = 0 To DataGrid1.Items.Count - 1
                CType(DataGrid1.Items(j).FindControl("changed"), CheckBox).Checked = True
            Next
        End Sub

        Sub backInspectBody(ByVal sender As Object, ByVal e As System.EventArgs)
            Response.Redirect(chk.urlRand("/admin/InspectBody.aspx"))
        End Sub

        Sub export_click(ByVal sender As Object, ByVal e As System.EventArgs)
            ltlAlert.Text = "window.open('/admin/InspctBodyexcel.aspx?uid=" & Request("uid") & "&type=1','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');window.parent.close(); "
        End Sub

        Public Sub Edit_delete(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
            If (e.CommandName.CompareTo("Delete") = 0) Then
                If DateAdd(DateInterval.Year, 1, CDate(e.Item.Cells(5).Text)) <= DateTime.Now Then
                    ltlAlert.Text = "alert('此条记录不能删除！')"
                    Exit Sub
                Else
                    strSql = " Delete From User_Health where userID='" & e.Item.Cells(0).Text & "' and status='0' "
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                    strSql = " update User_Health set status='0' where userID='" & e.Item.Cells(0).Text & "' and healthCheckDate='" & e.Item.Cells(5).Text & "' and status='2'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                End If
            End If
        End Sub
    End Class

End Namespace
