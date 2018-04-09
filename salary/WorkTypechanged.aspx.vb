
'!*******************************************************************************!
'* @@ NAME				:	WorkTypechanged.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for WorkTypechanged.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	June 12 2007
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Drawing
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class WorkTypechanged
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
                tolimit.Text = "工号"
                uplable.Text = "姓名"
                year.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now)
                typedropdown()
                datebind(0)
            End If
        End Sub

        Sub typedropdown()
            Dropdownlist1.Items.Add(New ListItem("--", "0"))
            strSql = " Select s.systemCodeID,s.systemCodeName From tcpc0.dbo.systemCode s INNER JOIN tcpc0.dbo.systemCodeType sc ON sc.systemCodeTypeID = s.systemCodeTypeID "
            strSql &= " Where  sc.systemCodeTypeName='Work Type' ORDER BY s.systemCodeID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read()
                Dim tempListItem As New ListItem
                tempListItem.Value = reader(0)
                tempListItem.Text = reader(1)
                Dropdownlist1.Items.Add(tempListItem)
            End While
            reader.Close()
            Dropdownlist1.SelectedIndex = 0
        End Sub

        Sub datebind(ByVal temp As Integer)
            Dim ds As DataSet
            strSql = "select w.id,u.userID,u.userno,u.username,s.systemCodeName,changedate  from WorktypeChange w inner join tcpc0.dbo.Users u on u.userID=w.userID "
            strSql &= " inner join tcpc0.dbo.systemCode s on s.systemCodeID=w.worktypeID"
            strSql &= " where year(changedate)='" & CDate(year.Text.Trim).Year & "' and month(changedate)='" & CDate(year.Text.Trim).Month & "'"
            strSql &= " and u.plantCode='" & Session("PlantCode") & "' "
            strSql &= " and u.isTemp='" & Session("temp") & "'"
            If temp > 0 Then
                If total.Text.Trim() <> "" Then
                    strSql &= " and cast(u.userNo as varchar)='" & total.Text.Trim & "'"
                End If
                If uplimit.Text.Trim() <> "" Then
                    strSql &= " and lower(u.userName) like N'%" & uplimit.Text.Trim.ToLower() & "%'"
                End If
            End If
            strSql &= "  order by  u.userID,changedate desc "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("usercode", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("username", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ctype", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("cDate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim dr1 As DataRow
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("cDate") = Format(.Rows(i).Item(5), "yyyy-MM-dd")
                        dr1.Item("usercode") = .Rows(i).Item(2)
                        dr1.Item("username") = .Rows(i).Item(3)
                        dr1.Item("ctype") = .Rows(i).Item(4)
                        dr1.Item("id") = .Rows(i).Item(0)
                        dr1.Item("userID") = .Rows(i).Item(1)
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
            ds.Reset()
        End Sub

        Sub BtnSave_click(ByVal sender As Object, ByVal e As System.EventArgs)
            'Dim ds As DataSet
            If year.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('日期不能为空!');Form1.year.focus();"
                Exit Sub
            Else
                If Not IsDate(year.Text.Trim) Then
                    ltlAlert.Text = "alert('输入日期不正确!');Form1.year.focus();"
                    Exit Sub
                End If
            End If

            If Dropdownlist1.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('必须选择一项类型!');Form1.year.focus();"
                Exit Sub
            End If
            Dim id As Integer
            strSql = " Select id From WorktypeChange where changedate='" & year.Text.Trim & "' and userID='" & userID.Text.Trim & "' "
            id = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
            If id > 0 Then
                strSql = " update WorktypeChange set worktypeID='" & Dropdownlist1.SelectedValue & "',createby='" & Session("uID") & "',createdate='" & DateTime.Now & "' where id='" & id.ToString() & "'"
            Else
                strSql = "insert into WorktypeChange(userID,worktypeID,changedate,createby,createdate) values ('" & userID.Text.Trim.ToString() & "','" & Dropdownlist1.SelectedValue & "','" & year.Text.Trim & "','" & Session("uID") & "',getdate())"
            End If
            'Response.Write(strSql)
            'Exit Sub
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            ltlAlert.Text = "alert('保存成功！');"
            workerNo.Text = ""
            workerName.Text = ""
            userID.Text = ""
            Dropdownlist1.SelectedIndex = 0
            datebind(1)
        End Sub

        Private Sub DataGrid1_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated

        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            datebind(0)
        End Sub

        Public Sub DataGrid1_delete(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
            strSql = " Delete From WorktypeChange "
            strSql &= " Where id =" & e.Item.Cells(6).Text.Trim()
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            datebind(1)
        End Sub

        Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
            If year.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('日期不能为空!');Form1.year.focus();"
                Exit Sub
            Else
                If Not IsDate(year.Text.Trim) Then
                    ltlAlert.Text = "alert('输入日期不正确!');Form1.year.focus();"
                    Exit Sub
                End If
            End If
            DataGrid1.CurrentPageIndex = 0
            datebind(1)
        End Sub

        Sub workerNo_changed(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim exitFlag As Boolean = False
            strSql = " SELECT userID,userName,leaveDate " _
                    & " FROM tcpc0.dbo.Users u INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID= u.workTypeID  " _
                    & " WHERE cast(u.userNo as varchar)='" & workerNo.Text.Trim & "'"
            strSql &= " and u.isTemp='" & Session("temp") & "'"
            strSql &= " and u.plantCode='" & Session("PlantCode") & "' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                exitFlag = True
                If reader("leaveDate").ToString() <> "" Then
                    If DateAdd(DateInterval.Month, 2, CDate(reader("leaveDate"))) < DateAdd(DateInterval.Month, 1, CDate(year.Text.Trim)) Then
                        ltlAlert.Text = "alert('此员工已离职！');Form1.workerNo.focus();"
                        workerNo.Text = ""
                        workerName.Text = ""
                        userID.Text = ""
                        Exit Sub
                    Else
                        ltlAlert.Text = "alert('此员工属于离职员工！');Form1.Dropdownlist1.focus();"
                    End If
                End If
                workerName.Text = reader("userName")
                userID.Text = reader(0)
                ltlAlert.Text = "Form1.Dropdownlist1.focus();"
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


    End Class

End Namespace
