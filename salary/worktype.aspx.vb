Imports adamFuncs
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc

Partial Class worktype
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

  

    Dim strSql As String
    Dim reader As SqlDataReader
    Dim ds As DataSet
    Dim i As Integer
    Dim sortOrder1 As String = ""
    Dim sortOrder2 As String = ""
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
           

            'Provider=MSDASQL.1;Persist Security Info=False;User ID=zxdz;Data Source=mfgtrain;Extended Properties="
            'SaleBind(0)
            'datebind(0)
            'dropdownValue()
            'ltlAlert.Text = "Form1.usercode.focus();"
            'mnn = New OdbcConnection(constr)
            'mnn.Open()

            'query = " select wo_nbr from pub.wo_mstr where wo_domain='" & reader(1) & " ' and wo_nbr='" & reader(2) & "' and wo_status = 'R' "
            'xyz = New OdbcCommand(query, mnn)

            ''If xyz.ExecuteReader.FieldCount > 0 Then
            ''    Response.Write(reader(0) & "/" & reader(1) & "<br>")
            ''End If
            'xyz.Dispose()
            'mnn.Close()

            Dim query As String
            Dim constr As String = "DSN=mfgtrain;uid=zxdz;pwd=zxdz;HOST=10.3.0.75;port=60057;DB=mfgtrain;"

            Dim conn, mnn As OdbcConnection
            Dim dd, bb As OdbcDataReader
            Dim cmd, xyz As OdbcCommand
            conn = New OdbcConnection(constr)
            conn.Open()
            Try
                strSql &= " select wod_qty_iss,wod_domain ,wod_nbr from pub.wod_det where wod_part = '25010050000190' and (wod_qty_iss <> 0 OR wod_qty_req <> 0) "
                cmd = New OdbcCommand(strSql, conn)
                dd = cmd.ExecuteReader

                While dd.Read
                    mnn = New OdbcConnection(constr)
                    mnn.Open()
                    query = " select wo_nbr from pub.wo_mstr where wo_domain='" & dd(1).ToString() & " ' and wo_nbr='" & dd(2).ToString() & "' and wo_status = 'R' "
                    xyz = New OdbcCommand(query, mnn)

                    If xyz.ExecuteReader.FieldCount > 0 Then
                        Response.Write(dd(0) & "/" & dd(1).ToString() & "<br>")
                    End If
                    xyz.Dispose()

                    mnn.Close()
                End While
            Catch
                Response.Write("!!!!!")

                xyz.Dispose()
                mnn.Close()
                dd.Close()
                cmd.Dispose()
                conn.Close()
                Return
            End Try
            dd.Close()
            cmd.Dispose()
            conn.Close()
        End If
        'If savesuccess = True Then
        '    ltlAlert.Text = "alert('保存成功！');Form1.usercode.focus();"
        '    savesuccess = False
        'End If
    End Sub
    Public Function BarCode(ByVal String1 As String, ByVal ch As Integer, ByVal cw As Integer, ByVal type_code As Integer) As String
        Dim TempStr, code As String

        TempStr = String1.ToLower()
        code = String1

        TempStr = Replace(TempStr, "0", "_|_|__||_||_|")
        TempStr = Replace(TempStr, "1", "_||_|__|_|_||")
        TempStr = Replace(TempStr, "2", "_|_||__|_|_||")
        TempStr = Replace(TempStr, "3", "_||_||__|_|_|")
        TempStr = Replace(TempStr, "4", "_|_|__||_|_||")
        TempStr = Replace(TempStr, "5", "_||_|__||_|_|")
        TempStr = Replace(TempStr, "7", "_|_|__|_||_||")
        TempStr = Replace(TempStr, "6", "_|_||__||_|_|")
        TempStr = Replace(TempStr, "8", "_||_|__|_||_|")
        TempStr = Replace(TempStr, "9", "_|_||__|_||_|")
        TempStr = Replace(TempStr, "a", "_||_|_|__|_||")
        TempStr = Replace(TempStr, "b", "_|_||_|__|_||")
        TempStr = Replace(TempStr, "c", "_||_||_|__|_|")
        TempStr = Replace(TempStr, "d", "_|_|_||__|_||")
        TempStr = Replace(TempStr, "e", "_||_|_||__|_|")
        TempStr = Replace(TempStr, "f", "_|_||_||__|_|")
        TempStr = Replace(TempStr, "g", "_|_|_|__||_||")
        TempStr = Replace(TempStr, "h", "_||_|_|__||_|")
        TempStr = Replace(TempStr, "i", "_|_||_|__||_|")
        TempStr = Replace(TempStr, "j", "_|_|_||__||_|")
        TempStr = Replace(TempStr, "k", "_||_|_|_|__||")
        TempStr = Replace(TempStr, "l", "_|_||_|_|__||")
        TempStr = Replace(TempStr, "m", "_||_||_|_|__|")
        TempStr = Replace(TempStr, "n", "_|_|_||_|__||")
        TempStr = Replace(TempStr, "o", "_||_|_||_|__|")
        TempStr = Replace(TempStr, "p", "_|_||_||_|__|")
        TempStr = Replace(TempStr, "r", "_||_|_|_||__|")
        TempStr = Replace(TempStr, "q", "_|_|_|_||__||")
        TempStr = Replace(TempStr, "s", "_|_||_|_||__|")
        TempStr = Replace(TempStr, "t", "_|_|_||_||__|")
        TempStr = Replace(TempStr, "u", "_||__|_|_|_||")
        TempStr = Replace(TempStr, "v", "_|__||_|_|_||")
        TempStr = Replace(TempStr, "w", "_||__||_|_|_|")
        TempStr = Replace(TempStr, "x", "_|__|_||_|_||")
        TempStr = Replace(TempStr, "y", "_||__|_||_|_|")
        TempStr = Replace(TempStr, "z", "_|__||_||_|_|")
        TempStr = Replace(TempStr, "-", "_|__|_|_||_||")
        TempStr = Replace(TempStr, "*", "_|__|_||_||_|")
        TempStr = Replace(TempStr, "/", "_|__|__|_|__|")
        TempStr = Replace(TempStr, "%", "_|_|__|__|__|")
        TempStr = Replace(TempStr, "+", "_|__|_|__|__|")
        TempStr = Replace(TempStr, ".", "_||__|_|_||_|")
        TempStr = Replace(TempStr, "_", "<span   style='height:" & ch & ";width:" & cw & ";background:#FFFFFF'></span>")
        TempStr = Replace(TempStr, "|", "<span   style='height:" & ch & ";width:" & cw & ";background:#000000'></span>")

        If type_code = 1 Then
            BarCode = TempStr & "<br>" & code
        Else
            BarCode = TempStr
        End If

    End Function
   


    'Sub dropdownValue()

    '    department.Items.Add(New ListItem("--", "0"))

    '    strSql = " Select departmentID,name From departments Where isSalary='1' and active='1'"
    '    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
    '    While reader.Read()
    '        Dim tempListItem As New ListItem
    '        tempListItem.Value = reader(0)
    '        tempListItem.Text = reader(1)
    '        department.Items.Add(tempListItem)
    '    End While
    '    reader.Close()
    'End Sub

    'Sub namevalue_change(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim exitFlag As Boolean = False
    '    strSql = " SELECT  u.userID,u.userName,u.leaveDate " _
    '            & " FROM tcpc0.dbo.users u INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID=u.workTypeID and s.systemCodeName=N'计件'" _
    '            & " INNER JOIN tcpc0.dbo.systemCodeType st ON st.systemCodeTypeID=s.systemCodeTypeID and st.systemCodeTypeName='Work Type'" _
    '            & " WHERE cast(u.userID as varchar)='" & usercode.Text.Trim & "'"
    '    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
    '    While reader.Read()
    '        exitFlag = True
    '        If reader("leaveDate").ToString() <> "" Then
    '            If DateAdd(DateInterval.Month, 2, CDate(reader("leaveDate"))) < CDate(name2value.Text) Then
    '                ltlAlert.Text = "alert('此员工已离职！');Form1.usercode.focus();"
    '                usercode.Text = ""
    '                username.Text = ""
    '                Exit Sub
    '            Else
    '                'Response.Write( CDate(reader("leaveDate")).ToShortDateString)
    '                ltlAlert.Text = "alert('此员工属于离职员工！');Form1.Type1.focus();"
    '            End If
    '        Else
    '            ltlAlert.Text = "Form1.Type1.focus();"
    '        End If
    '        username.Text = reader("userName")
    '        'ltlAlert.Text = "Form1.Type1.focus();"
    '    End While
    '    reader.Close()
    '    If exitFlag = False Then
    '        If usercode.Text <> "" Then
    '            ltlAlert.Text = "alert('工号不存在或者不是计件工！');Form1.usercode.focus();"

    '        End If
    '        usercode.Text = ""
    '        username.Text = ""
    '    End If

    'End Sub

    'Sub BtnSave_click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    If Type1.Text = "" And Type2.Text = "" And Type3.Text = "" Then
    '        ltlAlert.Text = "alert('中夜班数不能为空！');Form1.Type1.focus();"
    '        Exit Sub
    '    End If

    '    Dim i As Integer
    '    Dim mast(2) As String
    '    Array.Clear(mast, 0, 2)
    '    mast(0) = Type1.Text.Trim

    '    mast(1) = Type2.Text.Trim

    '    mast(2) = Type3.Text.Trim


    '    For i = 0 To 2
    '        If mast(i) <> "" Then
    '            strSql = " INSERT INTO Overnight ( userCode,workDate,userName,midNum,nightNum,wholeNum,createdDate,createdBy,organizationID,isok )"
    '            strSql &= " VALUES ( '" & usercode.Text & "', '" & name2value.Text & "',N'" & username.Text.Trim() & "', "
    '            Select Case i
    '                Case 0
    '                    strSql &= " '" & mast(i) & "','0','0', "
    '                    mast(0) = ""
    '                Case 1
    '                    strSql &= " '0','" & mast(i) & "','0', "
    '                    mast(1) = ""
    '                Case 2
    '                    strSql &= " '0','0','" & mast(i) & "', "
    '                    mast(2) = ""
    '            End Select
    '            strSql &= " '" & DateTime.Now.ToString() & "','" & Session("uID") & "','" & Session("orgID") & "','0')"

    '            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

    '        End If
    '    Next


    '    'ltlAlert.Text = "alert('保存成功！');Form1.usercode.focus();"
    '    usercode.Text = ""
    '    username.Text = ""
    '    Type1.Text = ""
    '    Type2.Text = ""
    '    Type3.Text = ""
    '    SaleBind(0)
    '    datebind(0)
    '    savesuccess = True
    '    Response.Redirect(chk.urlRand("\salary\worktype.aspx?sdate=" & name2value.Text))
    'End Sub
    'Private Sub SaleBind(ByVal temp As Integer)
    '    Dim midnight As String = ""
    '    Dim allnight As String = ""
    '    Dim wholenight As String = ""
    '    strSql = " Select midNightAllowance,nightAllowance,wholeNightAllowance From BaseInfo where year(workDate)='" & Year(CDate(name2value.Text)) & "' and month(workDate)='" & Month(CDate(name2value.Text)) & "'"
    '    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
    '    While reader.Read()
    '        midnight = reader(0)
    '        allnight = reader(1)
    '        wholenight = reader(2)
    '    End While
    '    reader.Close()
    '    'strSql = " Select top 1 userCode From Overnight where year(workDate)='" & Year(CDate(name2value.Text)) & "' and month(workDate)='" & Month(CDate(name2value.Text)) & "' "
    '    'ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

    '    strSql = " Select v.userCode,v.userName,sum(v.midNum) as tomid,sum(v.nightNum) as nimid , "
    '    'If (ds.Tables(0).Rows.Count > 0) Then
    '    strSql &= " sum(v.wholeNum) as whmid, sum(v.midNum) * " & midnight & " +sum(v.nightNum) * " & allnight & "+sum(v.wholeNum) * " & wholenight & " as allowance  From Overnight v "
    '    strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = v.userCode "
    '    'Else
    '    '    strSql &= " sum(v.wholeNum) as whmid,sum(v.midNum) * " & midnight & " +sum(v.nightNum) * " & allnight & "+sum(v.wholeNum) * " & wholenight & " as allowance From zxdz_his.dbo.Overnight v "
    '    '    strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = v.userCode"
    '    'End If
    '    'ds.Reset()

    '    strSql &= " Where datepart(month,v.workDate)= '" & CDate(name2value.Text).Month.ToString() & "'"
    '    If temp = 1 Then

    '        If department.SelectedValue > 0 Then
    '            strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
    '        End If

    '        If workerNoSearch.Text.Trim() <> "" Then
    '            strSql &= " and cast(v.userCode as varchar) ='" & workerNoSearch.Text.Trim() & "'"
    '        End If

    '        If workerNameSearch.Text.Trim() <> "" Then
    '            strSql &= " and lower(v.userName) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
    '        End If

    '    End If
    '    strSql &= " Group by v.userCode,v.userName "
    '    strSql &= " order by v.userCode "

    '    Session("EXSQL") = strSql
    '    Session("EXTitle") = "<b>工号</b>~^<b>姓名</b>~^<b>中班</b>~^<b>夜班</b>~^<b>全夜</b>~^<b>津贴</b>~^"
    '    ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
    '    Dim dt As New DataTable
    '    dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
    '    dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("worktype1", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("worktype2", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("worktype3", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("worknum", System.Type.GetType("System.String")))

    '    With ds.Tables(0)
    '        If (.Rows.Count > 0) Then
    '            Dim dr1 As DataRow
    '            For i = 0 To .Rows.Count - 1
    '                dr1 = dt.NewRow()
    '                dr1.Item("gsort") = i + 1
    '                dr1.Item("userID") = .Rows(i).Item("userCode")
    '                dr1.Item("userName") = .Rows(i).Item("userName")
    '                dr1.Item("worktype1") = .Rows(i).Item("tomid")
    '                dr1.Item("worktype2") = .Rows(i).Item("nimid")
    '                dr1.Item("worktype3") = .Rows(i).Item("whmid")
    '                'dr1.Item("worknum") = (CInt(.Rows(i).Item("tomid").ToString()) * CDec(midnight)) + (CInt(.Rows(i).Item("nimid").ToString()) * CDec(allnight)) + (CInt(.Rows(i).Item("whmid").ToString()) * CDec(wholenight))
    '                dr1.Item("worknum") = .Rows(i).Item("allowance")
    '                dt.Rows.Add(dr1)
    '            Next
    '        End If
    '    End With
    '    Dim dv As DataView
    '    dv = New DataView(dt)
    '    If (sortOrder1.Length <= 0) Then
    '        sortOrder1 = "gsort"
    '    End If
    '    dv.Sort = sortOrder1
    '    Try
    '        DataGrid1.DataSource = dv
    '        DataGrid1.DataBind()
    '    Catch
    '    End Try
    'End Sub

    'Private Sub datebind(ByVal temp As Integer)
    '    'strSql = " Select top 1 userCode From Overnight where year(workDate)='" & Year(CDate(name2value.Text)) & "' and month(workDate)='" & Month(CDate(name2value.Text)) & "' "
    '    'ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
    '    'If (ds.Tables(0).Rows.Count > 0) Then
    '    strSql = " Select v.id,v.userCode,v.userName,v.workDate,v.midNum,v.nightNum,v.wholeNum From Overnight v INNER JOIN "
    '    'Else
    '    '    strSql = " Select v.id,v.userCode,v.userName,v.workDate,v.midNum,v.nightNum,v.wholeNum From zxdz_his.dbo.Overnight v INNER JOIN "
    '    'End If
    '    'ds.Reset()
    '    strSql &= " tcpc0.dbo.users u ON u.userID = v.userCode "
    '    If Session("uRole") = "1" Then
    '        strSql &= " Where  v.workDate = '" & name2value.Text & "'"
    '        If temp = 1 Then
    '            If department.SelectedValue > 0 Then
    '                strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
    '            End If

    '            If workerNoSearch.Text.Trim() <> "" Then
    '                strSql &= " and cast(v.userCode as varchar) ='" & workerNoSearch.Text.Trim() & "'"
    '            End If

    '            If workerNameSearch.Text.Trim() <> "" Then
    '                strSql &= " and lower(v.userName) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
    '            End If
    '        End If

    '    Else
    '        strSql &= " INNER join manager_worker m ON m.worker=v.createdBy AND m.manager= " & Session("uID")
    '        strSql &= " Where v.workDate = '" & name2value.Text & "'"
    '        If temp = 1 Then
    '            If department.SelectedValue > 0 Then
    '                strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
    '            End If

    '            If workerNoSearch.Text.Trim() <> "" Then
    '                strSql &= " and cast(v.userCode as varchar) ='" & workerNoSearch.Text.Trim() & "'"
    '            End If

    '            If workerNameSearch.Text.Trim() <> "" Then
    '                strSql &= " and lower(v.userName) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
    '            End If
    '        End If

    '        strSql &= " union "
    '        'If (ds.Tables(0).Rows.Count > 0) Then
    '        strSql &= " Select v.id,v.userCode,v.userName,v.workDate,v.midNum,v.nightNum,v.wholeNum From Overnight v INNER JOIN "
    '        'Else
    '        '    strSql &= " Select v.id,v.userCode,v.userName,v.workDate,v.midNum,v.nightNum,v.wholeNum From zxdz_his.dbo.Overnight v INNER JOIN "
    '        'End If
    '        'ds.Reset()

    '        strSql &= " tcpc0.dbo.users u ON u.userID = v.userCode "
    '        strSql &= " Where v.workDate = '" & name2value.Text & "' and .v.createdBy=" & Session("uID")

    '        If temp = 1 Then
    '            If department.SelectedValue > 0 Then
    '                strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
    '            End If

    '            If workerNoSearch.Text.Trim() <> "" Then
    '                strSql &= " and cast(v.userCode as varchar) ='" & workerNoSearch.Text.Trim() & "'"
    '            End If

    '            If workerNameSearch.Text.Trim() <> "" Then
    '                strSql &= " and lower(v.userName) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
    '            End If
    '        End If

    '    End If
    '    strSql &= " Order by v.id desc "
    '    'Response.Write(strSql)
    '    'Exit Sub
    '    ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

    '    Dim dt As New DataTable
    '    dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
    '    dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("total", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("worktype", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("Date", System.Type.GetType("System.String")))
    '    dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))

    '    With ds.Tables(0)
    '        If (.Rows.Count > 0) Then
    '            Dim dr1 As DataRow
    '            For i = 0 To .Rows.Count - 1
    '                dr1 = dt.NewRow()
    '                dr1.Item("gsort") = i + 1
    '                dr1.Item("userID") = .Rows(i).Item("userCode")
    '                dr1.Item("userName") = .Rows(i).Item("userName")
    '                If .Rows(i).Item("midNum") <> 0 Then
    '                    dr1.Item("total") = .Rows(i).Item("midNum")
    '                    dr1.Item("worktype") = "中班"
    '                End If
    '                If .Rows(i).Item("nightNum") <> 0 Then
    '                    dr1.Item("total") = .Rows(i).Item("nightNum")
    '                    dr1.Item("worktype") = "夜班"
    '                End If
    '                If .Rows(i).Item("wholeNum") <> 0 Then
    '                    dr1.Item("total") = .Rows(i).Item("wholeNum")
    '                    dr1.Item("worktype") = "全夜"
    '                End If
    '                dr1.Item("Date") = .Rows(i).Item("workDate").ToShortDateString()
    '                dr1.Item("ID") = .Rows(i).Item("id")
    '                dt.Rows.Add(dr1)
    '            Next
    '        End If
    '    End With
    '    Dim dv As DataView
    '    dv = New DataView(dt)
    '    If (sortOrder2.Length <= 0) Then
    '        sortOrder2 = "gsort"
    '    End If
    '    dv.Sort = sortOrder2
    '    Try
    '        Datagrid2.DataSource = dv
    '        Datagrid2.DataBind()
    '    Catch
    '    End Try
    'End Sub
    'Private Sub dgreturnDetail_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemCreated
    '    Select Case e.Item.ItemType
    '        Case ListItemType.Item
    '            Dim myDeleteButton As TableCell
    '            myDeleteButton = e.Item.Cells(6)
    '            myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

    '        Case ListItemType.AlternatingItem
    '            Dim myDeleteButton As TableCell
    '            myDeleteButton = e.Item.Cells(6)
    '            myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

    '        Case ListItemType.EditItem
    '            Dim myDeleteButton As TableCell
    '            myDeleteButton = e.Item.Cells(6)
    '            myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")
    '    End Select
    'End Sub




    'Private Sub Datagrid2_deleted(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid2.DeleteCommand

    '    If (e.CommandName.CompareTo("Delete") = 0) Then

    '        strSql = "salary_DeleteOverNight"
    '        Dim params(3) As SqlParameter
    '        params(0) = New SqlParameter("@id", e.Item.Cells(7).Text.Trim)
    '        params(1) = New SqlParameter("@uid", e.Item.Cells(1).Text.Trim)
    '        params(2) = New SqlParameter("@year", Year(CDate(e.Item.Cells(5).Text.Trim)))
    '        params(3) = New SqlParameter("@month", Month(CDate(e.Item.Cells(5).Text.Trim)))
    '        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSql, params)
    '        'strSql = " DELETE FROM Overnight " _
    '        '        & " WHERE id='" & e.Item.Cells(7).Text.Trim & "'"
    '        'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

    '        'Dim datety As String = e.Item.Cells(5).Text.Trim
    '        'strSql = " Update ReadyCaculateSalary set nightallowance='0',midNum='0',nightNum='0',wholeNum='0' Where userCode='" & e.Item.Cells(1).Text.Trim & "' "
    '        'strSql &= " and month(salaryDate)='" & Month(CDate(datety)) & "'"
    '        'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

    '        'strSql = " update Overnight set isok='0' where userCode='" & e.Item.Cells(1).Text.Trim & "'"
    '        'strSql &= " and month(workDate)='" & Month(CDate(datety)) & "'"
    '        'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
    '        SaleBind(1)
    '        datebind(1)
    '    End If

    'End Sub

    'Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
    'DataGrid1.CurrentPageIndex = 0
    'Datagrid2.CurrentPageIndex = 0
    'datebind(1)
    'SaleBind(1)

    'End Sub

    'Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    DataGrid1.CurrentPageIndex = e.NewPageIndex
    '    SaleBind(1)
    'End Sub
    'Private Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
    '    Datagrid2.CurrentPageIndex = e.NewPageIndex
    '    datebind(1)
    'End Sub

    'Sub exportexcel(ByVal sender As Object, ByVal e As System.EventArgs)

    '    'strSql = " Select top 1 userCode From Overnight where year(workDate)='" & Year(CDate(name2value.Text)) & "' and month(workDate)='" & Month(CDate(name2value.Text)) & "' "
    '    'ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
    '    'If (ds.Tables(0).Rows.Count > 0) Then
    '    strSql = " Select v.id,v.userCode,v.userName,v.workDate,v.midNum,v.nightNum,v.wholeNum From Overnight v INNER JOIN "
    '    'Else
    '    '    strSql = " Select v.id,v.userCode,v.userName,v.workDate,v.midNum,v.nightNum,v.wholeNum From zxdz_his.dbo.Overnight v INNER JOIN "
    '    'End If
    '    'ds.Reset()
    '    strSql &= " tcpc0.dbo.users u ON u.userID = v.userCode "
    '    If Session("uRole") = "1" Then
    '        strSql &= " Where  month(v.workDate) = '" & Month(CDate(name2value.Text)) & "' and year(v.workDate) = '" & Year(CDate(name2value.Text)) & "' "
    '        If workerNoSearch.Text.Trim <> "" Then
    '            strSql &= " and cast(v.userCode as varchar) ='" & workerNoSearch.Text.Trim() & "'"
    '        End If
    '    Else
    '        strSql &= " INNER join manager_worker m ON m.worker=v.createdBy AND m.manager= " & Session("uID")
    '        strSql &= " Where month(v.workDate) = '" & Month(CDate(name2value.Text)) & "' and year(v.workDate) = '" & Year(CDate(name2value.Text)) & "' "
    '        If workerNoSearch.Text.Trim <> "" Then
    '            strSql &= " and cast(v.userCode as varchar) ='" & workerNoSearch.Text.Trim() & "'"
    '        End If

    '        strSql &= " union "
    '        'If (ds.Tables(0).Rows.Count > 0) Then
    '        strSql &= " Select v.id,v.userCode,v.userName,v.workDate,v.midNum,v.nightNum,v.wholeNum From Overnight v INNER JOIN "
    '        'Else
    '        '    strSql &= " Select v.id,v.userCode,v.userName,v.workDate,v.midNum,v.nightNum,v.wholeNum From zxdz_his.dbo.Overnight v INNER JOIN "
    '        'End If
    '        'ds.Reset()

    '        strSql &= " tcpc0.dbo.users u ON u.userID = v.userCode "
    '        strSql &= " Where month(v.workDate) = '" & Month(CDate(name2value.Text)) & "' and year(v.workDate) = '" & Year(CDate(name2value.Text)) & "' and .v.createdBy=" & Session("uID")
    '        If workerNoSearch.Text.Trim <> "" Then
    '            strSql &= " and cast(v.userCode as varchar) ='" & workerNoSearch.Text.Trim() & "'"
    '        End If
    '    End If
    '    strSql &= " Order by v.id desc "

    '    Session("EXSQL") = strSql
    '    Session("EXTitle") = "<b>工号</b>~^<b>姓名</b>~^<b>日期</b>~^<b>中班</b>~^<b>夜班</b>~^<b>全夜</b>~^"
    '    Response.Redirect("/public/exportExcel.aspx", True)

    'End Sub
End Class

End Namespace
