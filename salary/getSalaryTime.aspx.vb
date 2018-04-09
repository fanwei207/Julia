'!*******************************************************************************!
'* @@ NAME				:	getSalaryTime.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for getSalaryTime.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	January 30, 2007
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Drawing
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.OleDb


Namespace tcpc

    Partial Class getSalaryTime
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents filename As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents workshop As System.Web.UI.WebControls.DropDownList
        'Protected WithEvents ltlAlert As Literal

        Dim strSql As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass
        Protected WithEvents uploadBtn As System.Web.UI.HtmlControls.HtmlInputButton
        Protected WithEvents Bchange As System.Web.UI.WebControls.Button

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then

                imputedmonthload()
                Dropdownlist2.SelectedIndex = DateTime.Now.Month - 1

                yeartextbox.Text = DateTime.Now.Year

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


        Private Sub imputedmonthload()
            ' Dropdownlist2.Items.Add(New ListItem("--", "0"))
            Dim i As Integer
            For i = 1 To 12
                Dim tempListItem As New ListItem
                tempListItem.Value = i
                tempListItem.Text = i
                Dropdownlist2.Items.Add(tempListItem)
            Next
        End Sub
        Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
            If IsNumeric(yeartextbox.Text.Trim) Then
            Else
                ltlAlert.Text = "alert('输入年份只能为数字!');Form1.yeartextbox.focus();"
                Exit Sub
            End If
            If Len(yeartextbox.Text.Trim) < 0 Or CInt(yeartextbox.Text.Trim) < 1900 Then
                ltlAlert.Text = "alert('输入年份有误!例如:2004');Form1.yeartextbox.focus();"
                Exit Sub
            End If
            SaleBind(1)
        End Sub

        Sub SaleBind(ByVal temp As Integer)
            Dim ds As DataSet
            strSql = " Select o.id,u.userID,u.userNo,u.username,isnull(d.name,'') as dname From EmployeeTime o "
            strSql &= " INNER JOIN  tcpc0.dbo.Users u ON u.userID=o.usercode "
            strSql &= " inner join departments d on d.departmentID=u.departmentID "
            'strSql &= " left outer join workshop w on w.id=u.workshopID "
            strSql &= " Where month(o.currentdate)='" & Dropdownlist2.SelectedValue & "' and year(o.currentdate)='" & yeartextbox.Text.Trim() & "' "
            'strSql &= " and u.isTemp='" & Session("temp") & "'"
            If temp > 0 Then
                If workerNoSearch.Text.Trim() <> "" Then
                    strSql &= " and cast(u.userNo as varchar)='" & workerNoSearch.Text.Trim & "'"
                End If
                If workerNameSearch.Text.Trim() <> "" Then
                    strSql &= " and lower(u.username) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
                End If
                If department.SelectedValue > 0 Then
                    strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
                End If
                strSql &= " and u.plantCode='" & Session("PlantCode") & "' "
            End If
            strSql &= " order by o.usercode "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
            'dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim dr1 As DataRow
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("userNo") = .Rows(i).Item("userNo").ToString()
                        dr1.Item("userID") = .Rows(i).Item("userID").ToString()
                        dr1.Item("userName") = .Rows(i).Item("userName")

                        dr1.Item("ID") = .Rows(i).Item("ID")
                        dr1.Item("department") = .Rows(i).Item("dname")
                        'dr1.Item("workshop") = .Rows(i).Item("wname")
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
        End Sub

        Public Sub Edit_delete(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
            strSql = " Delete From EmployeeTime "
            strSql &= " Where id =" & e.Item.Cells(5).Text.Trim()
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            SaleBind(1)
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            SaleBind(1)
        End Sub

        Sub workerNo_changed(ByVal sender As Object, ByVal e As System.EventArgs)
            'INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID= u.workTypeID and Substring(s.systemCodeName,1,2)=N'计时'
            Dim exitFlag As Boolean = False
            strSql = " SELECT userID,userName,leaveDate " _
                    & " FROM tcpc0.dbo.Users u  " _
                    & " WHERE cast(u.userNo as varchar)='" & workerNo.Text.Trim & "'"
            strSql &= " and u.isTemp='" & Session("temp") & "'"
            strSql &= " and u.plantCode='" & Session("PlantCode") & "' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                exitFlag = True
                If reader("leaveDate").ToString() <> "" Then
                    If DateAdd(DateInterval.Month, 2, CDate(reader("leaveDate"))) < DateAdd(DateInterval.Month, 1, CDate(yeartextbox.Text.Trim & "/" & Dropdownlist2.SelectedValue & "/1")) Then
                        ltlAlert.Text = "alert('此员工已离职！');Form1.workerNo.focus();"
                        workerNo.Text = ""
                        workerName.Text = ""
                        userID.Text = ""
                        Exit Sub
                    Else
                        ltlAlert.Text = "alert('此员工属于离职员工！');Form1.BtnSave.focus();"
                    End If


                End If

                workerName.Text = reader("userName")
                userID.Text = reader(0)
                ltlAlert.Text = "Form1.BtnSave.focus();"
            End While
            reader.Close()
            If exitFlag = False Then
                If workerNo.Text <> "" Then
                    ltlAlert.Text = "alert('工号不存在！');"
                    ltlAlert.Text = "Form1.workerNo.focus();"
                End If
                workerNo.Text = ""
                workerName.Text = ""
                userID.Text = ""
            End If
        End Sub

        Sub BtnSave_click(ByVal sender As Object, ByVal e As System.EventArgs)
            If yeartextbox.Text.Trim() = "" Then
                ltlAlert.Text = "alert('年份不能为空!');Form1.yeartextbox.focus();"
                Exit Sub
            Else
                If IsNumeric(yeartextbox.Text.Trim) Then
                    If CInt(yeartextbox.Text.Trim) < 1900 Then
                        ltlAlert.Text = "alert('输入年份有误!');Form1.yeartextbox.focus();"
                        Exit Sub
                    End If
                Else
                    ltlAlert.Text = "alert('输入年份只能为数字!');Form1.yeartextbox.focus();"
                    Exit Sub
                End If
            End If
            Dim eid As String = ""
            strSql = " select id from EmployeeTime where month(currentdate)='" & Dropdownlist2.SelectedValue & "' and year(currentdate)='" & yeartextbox.Text.Trim & "' and usercode='" & userID.Text.Trim() & "' "
            eid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
            If eid <> "" Then
                ltlAlert.Text = "alert('输入数据已存在!');Form1.workerNo.focus();"
                Exit Sub
            Else
                Dim edate As String = yeartextbox.Text.Trim & "-" & Dropdownlist2.SelectedValue & "-1"
                strSql = " insert into EmployeeTime(usercode,currentdate,creatby,creatday) values ('" & userID.Text.Trim() & "','" & edate & "','" & Session("uID") & "',getdate())"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                workerNo.Text = ""
                workerName.Text = ""
                userID.Text = ""
                ltlAlert.Text = "alert('保存成功!');Form1.workerNo.focus();"
                SaleBind(1)
            End If
        End Sub


    End Class

End Namespace


