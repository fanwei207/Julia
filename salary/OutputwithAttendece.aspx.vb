'* @@ NAME				:	OutputwithAttendece.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for OutputwithAttendece.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	 September 21 2006
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class OutputwithAttendece
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'Protected WithEvents ltlAlert As Literal
        Dim strSql As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then
                dropdownValue()
                workshop.Items.Add(New ListItem("--", "0"))
                datebind(0)
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

        Sub workshopchange(ByVal s As Object, ByVal e As System.EventArgs)
            If department.SelectedValue > 0 Then
                workdropdown()
            End If
        End Sub

        Sub workdropdown()
            workshop.Items.Clear()
            workshop.Items.Add(New ListItem("--", "0"))
            strSql = " Select w.id,w.name From Workshop w "
            If department.SelectedIndex > 0 Then
                strSql &= " INNER JoiN departments d ON d.departmentID=w.departmentID"
            End If
            strSql &= " Where w.departmentID='" & department.SelectedValue & "' and w.workshopID is null "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read()
                Dim tempListItem As New ListItem
                tempListItem.Value = reader(0)
                tempListItem.Text = reader(1)
                workshop.Items.Add(tempListItem)
            End While
            reader.Close()
        End Sub


        Private Sub datebind(ByVal temp As Integer)
            Dim ds As DataSet
            Dim i As Integer
            Dim ptable As String = "JobSalaryTemp"
            Dim ptable1 As String = "PieceAttendence"
            If (name2value.Text.Length > 0) Then
                Dim dd As String = CDate(name2value.Text).Year & "-" & CDate(name2value.Text).Month & "-01"
                If DateDiff(DateInterval.Month, CDate(dd), DateTime.Now) > 3 Then
                    ptable = "JobSalaryTemp_his"
                    ptable1 = "PieceAttendence"
                End If
            End If


            strSql = "select w.id,w.name,isnull(d.name,''),isnull(ws.name,''),cast(SUM(oa.output)/8 as decimal(16,6)), cast(SUM(oa.output / oa.total * oa.totalhours)/8 as decimal(16,6)),cast((SUM(oa.output)- SUM(oa.output / oa.total * oa.totalhours))/ SUM(oa.output / oa.total * oa.totalhours) *100 as decimal(5,1)) as reduce From  "
            strSql &= " (select j.workProcedureID,j.usercode,sum(case when j.guideLine=0 then j.outputs else j.outputs/j.guideLine end) as output,po.total, pa.totalhours From JobSalaryTemp j "
            strSql &= " inner join (select sum(case when guideLine=0 then outputs else outputs/guideLine end) as total,usercode,workdate From JobSalaryTemp group by usercode,workdate) po on po.usercode=j.usercode and po.workdate=j.workdate "
            strSql &= " inner join PieceAttendence pa on pa.usercode=j.usercode and pa.workdate=j.workdate "
            strSql &= " where po.total>0  and pa.totalhours > 0  "
            If txtMonth.Text.Trim = "" Then
                strSql &= " and j.workdate='" & name2value.Text.Trim() & "' "
            Else
                strSql &= " and j.workdate>='" & name2value.Text.Trim() & "'  and j.workdate<='" & txtMonth.Text.Trim & "'"
            End If

            strSql &= " group by j.usercode,j.workdate,j.workProcedureID ,po.total,pa.totalhours) oa "
            strSql &= " inner join workProcedure w ON w.id = oa.workProcedureID "
            strSql &= " Left Outer JOIN departments d ON d.departmentID=w.departmentID "
            strSql &= " Left Outer JOIN Workshop ws on ws.id=w.workshopID "
            If temp > 1 Then
                If department.SelectedValue > 0 Then
                    strSql &= " and w.departmentID = " & department.SelectedValue.ToString()
                End If
                If workshop.SelectedValue > 0 Then
                    strSql &= " and w.workshopID=" & workshop.SelectedValue
                End If
                'strSql &= " and u.plantCode='" & Session("PlantCode") & "'"
            End If
            strSql &= " GROUP BY w.id,w.name,d.name,ws.name "
            If reduceSearch.Text.Trim <> "" Then
                strSql &= "  Having cast((SUM(oa.output)- SUM(oa.output / oa.total * oa.totalhours))/ SUM(oa.output / oa.total * oa.totalhours) *100 as decimal(5,1)) >= '" & reduceSearch.Text.Trim & "' "
            End If

            Session("EXSQL") = strSql
            Session("EXTitle") = "200^<b>工序</b>~^120^<b>部门</b>~^120^<b>工段</b>~^<b>工时</b>~^<b>考勤</b>~^<b>百分比</b>~^"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("workprocedure", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("output", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("attendance", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("reduce", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("reduce1", System.Type.GetType("System.Decimal")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("workprocedure") = .Rows(i).Item(1)
                        dr1.Item("department") = .Rows(i).Item(2)
                        dr1.Item("workshop") = .Rows(i).Item(3)
                        dr1.Item("output") = .Rows(i).Item(4)
                        dr1.Item("attendance") = .Rows(i).Item(5)
                        dr1.Item("reduce") = .Rows(i).Item(6).ToString() & "%"
                        dr1.Item("reduce1") = .Rows(i).Item(6)
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                Datagrid2.DataSource = dv
                Datagrid2.DataBind()
            Catch
            End Try

        End Sub

        Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)

            If name2value.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('起始日期 不能为空！');"
                Exit Sub
            Else
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(name2value.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('起始日期 格式不正确！');"
                    Exit Sub
                End Try
            End If

            If txtMonth.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('结束日期 不能为空！');"
                Exit Sub
            Else
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(txtMonth.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('结束日期 格式不正确！');"
                    Exit Sub
                End Try
            End If


            Datagrid2.CurrentPageIndex = 0
            datebind(1)
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            datebind(1)
        End Sub
    End Class

End Namespace
