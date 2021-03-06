'!*******************************************************************************!
'* @@ NAME				:	TurnedTempers.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for TurnedTempers.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	July 31 2006
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class TurnedTempers
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Dim strSql As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal

        Dim query As String

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
                changdate.Attributes.Add("onclick", "return confirm('确定要将这些员工转为正式工吗？');")
            End If
        End Sub

        Private Sub SaleBind(ByVal temp As Integer)

            Dim i As Integer = 0
            strSql = " select u.userID,u.userNo,u.username,isnull(d.name,''),isnull(w.name,''),isnull(s.systemCodeName,''),u.enterdate,isnull(s1.systemcodename,''), isnull(s2.systemcodename,'') From tcpc0.dbo.users u "
            strSql &= " left outer join tcpc0.dbo.SystemCode s on u.workTypeID=s.systemCodeID "
            strSql &= " left outer join departments d on d.departmentID=u.departmentID "
            strSql &= " left outer join Workshop w on w.id=u.workshopID "
            strSql &= " Left outer join tcpc0.dbo.systemCode s1 on s1.systemcodeid=u.employtypeid "
            strSql &= " left outer join tcpc0.dbo.systemCode s2 on s2.systemcodeid=u.insuranceTypeID "
            strSql &= " Where u.enterdate Is Not Null and u.plantCode='" & Session("PlantCode") & "' and isTemp='1' and leavedate is null and deleted=0 and isActive=1"
            strSql &= " order by u.userID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("uID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("enterdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("worktype", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("issus", System.Type.GetType("System.String")))
            While reader.Read
                Dim dr1 As DataRow
                dr1 = dt.NewRow()
                i = i + 1
                dr1.Item("gsort") = i
                dr1.Item("userNo") = reader(1)
                dr1.Item("uID") = reader(0)
                dr1.Item("userName") = reader(2)
                dr1.Item("department") = reader(3)
                dr1.Item("workshop") = reader(4)
                dr1.Item("type") = reader(5)
                dr1.Item("enterdate") = Format(reader(6), "yyyy-MM-dd")
                'testtable.Add(i, reader(0))
                dr1.Item("worktype") = reader(7)
                dr1.Item("issus") = reader(8)
                dt.Rows.Add(dr1)

            End While
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
            reader.Close()
            Label1.Text = "<b>人数： </b>" & i.ToString()
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            SaleBind(1)
        End Sub

        Public Sub DataGrid1_deleted(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If e.CommandName.CompareTo("DeleteBtn") = 0 Then
                strSql = " update tcpc0.dbo.users set istemp=0 where userID='" & e.Item.Cells(10).Text.Trim & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                SaleBind(1)
            End If
        End Sub

        Sub update_date(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim testtable As New Hashtable
            Dim testdate As New Hashtable
            Dim m As Integer = 0
            Dim idhashtable As New Hashtable
            Dim choise As Integer = 0
            Dim j As Integer = 0
            idhashtable.Clear()
            testtable.Clear()
            testdate.Clear()

            strSql = " select u.userID,u.enterdate From tcpc0.dbo.users u "
            strSql &= " left outer join tcpc0.dbo.SystemCode s on u.workTypeID=s.systemCodeID "
            strSql &= " left outer join departments d on d.departmentID=u.departmentID "
            strSql &= " left outer join Workshop w on w.id=u.workshopID "
            strSql &= " Where dateadd(day,3,u.enterdate)< getdate() and u.plantCode='" & Session("PlantCode") & "' and isTemp='1' and leavedate is null and deleted=0 and isActive=1"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                j = j + 1
                testtable.Add(j, reader(0))
                testdate.Add(reader(0), reader(1))
            End While
            reader.Close()

            If changedate.Text.Trim = "" Then
                If moded.Text.Trim() = "" Or modid.Text.Trim() = "" Then
                    ltlAlert.Text = "alert('序号不能为空!');"
                    Exit Sub
                ElseIf Not (IsNumeric(modid.Text.Trim()) And IsNumeric(moded.Text.Trim())) Then
                    ltlAlert.Text = "alert('序号只能为数字!');"
                    Exit Sub
                ElseIf CInt(modid.Text.Trim()) < 1 Or CInt(moded.Text.Trim()) > testtable.Count Then
                    ltlAlert.Text = "alert('序号超过范围!');"
                    Exit Sub
                End If

            Else
                If moded.Text.Trim().Length > 0 Or modid.Text.Trim().Length > 0 Then
                    ltlAlert.Text = "alert('日期与序号不能同时填写!');"
                    Exit Sub
                End If
                If Not (IsDate(changedate.Text.Trim)) Then
                    ltlAlert.Text = "alert('输入的不是合法的日期!');"
                    Exit Sub
                End If
                choise = 1
            End If

            Try
                If choise = 0 Then
                    For j = CInt(modid.Text.Trim()) To CInt(moded.Text.Trim())
                        strSql = " update tcpc0.dbo.users set istemp=0 where userID='" & testtable(j) & "'"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                        idhashtable.Add(m, testtable(j))
                        m = m + 1
                    Next
                Else
                    Dim mydate As IDictionaryEnumerator = testdate.GetEnumerator()
                    While mydate.MoveNext()
                        If DateDiff(DateInterval.Day, CDate(mydate.Value.ToString()), CDate(changedate.Text.Trim)) = 0 Then
                            strSql = " update tcpc0.dbo.users set istemp=0 where userID='" & mydate.Key & "'"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                            idhashtable.Add(m, mydate.Key)
                            m = m + 1
                        End If
                    End While
                End If
            Catch ex As Exception
                Dim myEnumerator As IDictionaryEnumerator = idhashtable.GetEnumerator()
                While myEnumerator.MoveNext()
                    strSql = " update tcpc0.dbo.users set istemp=1 where userID='" & myEnumerator.Value.ToString() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                End While
            End Try


            SaleBind(1)
        End Sub

    End Class

End Namespace
