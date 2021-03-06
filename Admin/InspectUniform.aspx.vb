'!*******************************************************************************!
'* @@ NAME				:	InspectUniform.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for InspectUniform.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	March 30 2005
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class InspectUniform
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
           
            type.Items.Add(New ListItem("--", "0"))
            type.Items.Add(New ListItem("长夹克", "1"))
            type.Items.Add(New ListItem("短夹克", "2"))
            type.Items.Add(New ListItem("白大褂", "3"))
            type.SelectedIndex = 0
            dropdownValue()
            name2value.Text = Format(DateTime.Now, "yyyy-MM-dd")
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
    Sub SaleBind(ByVal temp As Integer)
        Dim lok As Boolean = True
        Dim sok As Boolean = True
        Dim wok As Boolean = True


        strSql = " Select uf.userID,u.userName as name,d.Name,isnull(lJack,0),lJackDate,isnull(lJackTotal,0),isnull(sJack,0),sJackDate,isnull(sJackTotal,0),isnull(lCoat,0),lCoatDate,isnull(lCoatTotal,0),isnull(islJack,0),isnull(issJack,0),isnull(islCost,0) "
        strSql &= " From User_Uniform uf "
        strSql &= " Inner join tcpc0.dbo.users u ON u.userID=uf.userID "
        strSql &= " Left outer JOIN Departments d ON d.departmentID=u.departmentID "
        strSql &= " Where u.deleted=0 and u.roleID>1 and u.organizationID=" & Session("orgID")
        strSql &= " and u.leaveDate is null "
        If temp <> 0 Then
            If workerNoSearch.Text.Trim() <> "" Then
                strSql &= " and cast(u.userID as varchar)='" & workerNoSearch.Text.Trim & "'"
            End If
            If workerNameSearch.Text.Trim() <> "" Then
                strSql &= " and lower(u.userName) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
            End If
            If department.SelectedValue > 0 Then
                strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
            End If
            If type.SelectedValue > 0 Then
                Select Case type.SelectedValue
                    Case 1
                        sok = False
                        wok = False
                        strSql &= " and  (uf.islJack<>0 and ((uf.lJack>uf.lJackTotal) or (dateadd(year,4,uf.lJackDate)<=getdate())) )"
                    Case 2
                        lok = False
                        wok = False
                        strSql &= " and (uf.issJack<>0 and ((uf.sJack>uf.sJackTotal) or (dateadd(year,3,uf.sJackDate)<=getdate())) )"
                    Case 3
                        lok = False
                        sok = False
                        strSql &= " and (uf.islCost<>0 and ((uf.lCoat>uf.lCoatTotal) or (dateadd(year,2,uf.lCoatDate)<=getdate()) ) )"
                End Select
            Else
                strSql &= " and ( (uf.islJack<>0 and ((uf.lJack>uf.lJackTotal) or (dateadd(year,4,uf.lJackDate)<=getdate())) )"
                strSql &= " or (uf.issJack<>0 and ((uf.sJack>uf.sJackTotal) or (dateadd(year,3,uf.sJackDate)<=getdate())) )"
                strSql &= " or (uf.islCost<>0 and ((uf.lCoat>uf.lCoatTotal) or (dateadd(year,2,uf.lCoatDate)<=getdate()) ) ))"
            End If
        End If


        strSql &= " Order by uf.userID "
        'Response.Write(strSql)
        'Exit Sub
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("departmentName", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("uniform", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("times", System.Type.GetType("System.String")))
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim j As Integer
                Dim dr1 As DataRow

                For i = 0 To .Rows.Count - 1
                    ' long jack
                    If .Rows(i).Item(12) <> 0 And lok = True Then
                        'compared the quantity
                        If .Rows(i).Item(3) > .Rows(i).Item(5) Then
                            For j = 0 To (.Rows(i).Item(3) - .Rows(i).Item(5) - 1)
                                dr1 = dt.NewRow()
                                dr1.Item("userID") = .Rows(i).Item(0)
                                dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                                dr1.Item("departmentName") = .Rows(i).Item(2).ToString().Trim()
                                dr1.Item("uniform") = "长夹克"
                                dr1.Item("times") = "0"
                                dt.Rows.Add(dr1)
                            Next
                        End If
                        'comparewd the year
                        If .Rows(i).Item(4).ToString() <> "" Then
                            If DateAdd(DateInterval.Year, 4, .Rows(i).Item(4)) <= DateTime.Now Then
                                dr1 = dt.NewRow()
                                dr1.Item("userID") = .Rows(i).Item(0)
                                dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                                dr1.Item("departmentName") = .Rows(i).Item(2).ToString().Trim()
                                dr1.Item("uniform") = "长夹克"
                                dr1.Item("times") = "1"
                                dt.Rows.Add(dr1)
                            End If
                        End If
                    End If
                    ' short jack
                    If .Rows(i).Item(13) <> 0 And sok = True Then
                        'compared the quantity
                        If .Rows(i).Item(6) > .Rows(i).Item(8) Then
                            For j = 0 To (.Rows(i).Item(6) - .Rows(i).Item(8) - 1)
                                dr1 = dt.NewRow()
                                dr1.Item("userID") = .Rows(i).Item(0)
                                dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                                dr1.Item("departmentName") = .Rows(i).Item(2).ToString().Trim()
                                dr1.Item("uniform") = "短夹克"
                                dr1.Item("times") = "0"
                                dt.Rows.Add(dr1)
                            Next
                        End If
                        'comparewd the year
                        If .Rows(i).Item(7).ToString() <> "" Then
                            If DateAdd(DateInterval.Year, 3, .Rows(i).Item(7)) <= DateTime.Now Then
                                dr1 = dt.NewRow()
                                dr1.Item("userID") = .Rows(i).Item(0)
                                dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                                dr1.Item("departmentName") = .Rows(i).Item(2).ToString().Trim()
                                dr1.Item("uniform") = "短夹克"
                                dr1.Item("times") = "1"
                                dt.Rows.Add(dr1)
                            End If
                        End If
                    End If
                    ' White overcoat
                    If .Rows(i).Item(14) <> 0 And wok = True Then
                        'compared the quantity
                        If .Rows(i).Item(9) > .Rows(i).Item(11) Then
                            For j = 0 To (.Rows(i).Item(9) - .Rows(i).Item(11) - 1)
                                dr1 = dt.NewRow()
                                dr1.Item("userID") = .Rows(i).Item(0)
                                dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                                dr1.Item("departmentName") = .Rows(i).Item(2).ToString().Trim()
                                dr1.Item("uniform") = "白大褂"
                                dr1.Item("times") = "0"
                                dt.Rows.Add(dr1)
                            Next
                        End If
                        'comparewd the year
                        If .Rows(i).Item(10).ToString() <> "" Then
                            If DateAdd(DateInterval.Year, 2, .Rows(i).Item(10)) <= DateTime.Now Then
                                dr1 = dt.NewRow()
                                dr1.Item("userID") = .Rows(i).Item(0)
                                dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                                dr1.Item("departmentName") = .Rows(i).Item(2).ToString().Trim()
                                dr1.Item("uniform") = "白大褂"
                                dr1.Item("times") = "1"
                                dt.Rows.Add(dr1)
                            End If
                        End If
                    End If

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

    Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
        SaleBind(1)
    End Sub

    Sub comfirmpeople(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect(chk.urlRand("/admin/CoatInstead.aspx"))
    End Sub

    Sub changeuniform(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim m As Integer
        Dim type As Integer
        For m = 0 To DataGrid1.Items.Count - 1
            If CType(DataGrid1.Items(m).FindControl("changed"), CheckBox).Checked = True Then
                Select Case DataGrid1.Items(m).Cells(3).Text
                    Case "长夹克"
                        type = 1
                    Case "短夹克"
                        type = 2
                    Case "白大褂"
                        type = 3
                End Select
                strSql = " insert into User_UniformDetail (userID,uniform,uniformDate,createdBy,createdDate) Values "
                strSql &= " ('" & DataGrid1.Items(m).Cells(0).Text & "','" & type.ToString() & "','" & name2value.Text & "'," & Session("uID") & ",getdate())"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                If DataGrid1.Items(m).Cells(5).Text = "0" Then
                    Select Case type
                        Case 1
                            strSql = " update User_Uniform set lJackTotal=lJackTotal+'1' where userID='" & DataGrid1.Items(m).Cells(0).Text & "' "
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                            strSql = "update User_Uniform set lJackDate='" & name2value.Text & "'where userID in (select userID From User_UniformDetail where userID='" & DataGrid1.Items(m).Cells(0).Text & "' and lJack=lJackTotal ) "
                        Case 2
                            strSql = " update User_Uniform set sJackTotal=sJackTotal+'1' where userID='" & DataGrid1.Items(m).Cells(0).Text & "' "
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                            strSql = "update User_Uniform set sJackDate='" & name2value.Text & "'where userID in (select userID From User_UniformDetail where userID='" & DataGrid1.Items(m).Cells(0).Text & "' and sJack=sJackTotal ) "
                        Case 3
                            strSql = " update User_Uniform set lCoatTotal=lCoatTotal+'1' where userID='" & DataGrid1.Items(m).Cells(0).Text & "' "
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                            strSql = "update User_Uniform set lCoatDate='" & name2value.Text & "'where userID in (select userID From User_UniformDetail where userID='" & DataGrid1.Items(m).Cells(0).Text & "' and lCoat=lCoatTotal ) "
                    End Select
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                Else
                    Select Case type
                        Case 1
                            strSql = " update User_Uniform set lJackDate='" & name2value.Text & "' where userID='" & DataGrid1.Items(m).Cells(0).Text & "' "
                        Case 2
                            strSql = " update User_Uniform set sJackDate='" & name2value.Text & "' where userID='" & DataGrid1.Items(m).Cells(0).Text & "' "
                        Case 3
                            strSql = " update User_Uniform set lCoatDate='" & name2value.Text & "' where userID='" & DataGrid1.Items(m).Cells(0).Text & "' "
                    End Select
                End If
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            End If
        Next

        SaleBind(1)
    End Sub
End Class

End Namespace
