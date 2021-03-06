'!*******************************************************************************!
'* @@ NAME				:	CoatInstead.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for CoatInstead.aspx
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

    Partial Class CoatInstead
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents type As System.Web.UI.WebControls.DropDownList

        Dim strSql As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass
        Dim ds As DataSet

        'Protected WithEvents wname As System.Web.UI.WebControls.TextBox
        Protected WithEvents ldate As System.Web.UI.WebControls.TextBox
        Protected WithEvents sdate As System.Web.UI.WebControls.TextBox
        Protected WithEvents wdate As System.Web.UI.WebControls.TextBox
        Protected WithEvents lflag As System.Web.UI.WebControls.CheckBox
        Protected WithEvents sflag As System.Web.UI.WebControls.CheckBox
        Protected WithEvents wflag As System.Web.UI.WebControls.CheckBox

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
                reger.Text = "1"
                uid.Text = ""
                SaleBind(0)
                ltlAlert.Text = "Form1.wcode.focus();"
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

        Sub namevalue_change(ByVal sender As Object, ByVal e As System.EventArgs)

            If wcode.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('工号不能为空！');"
                Exit Sub
            End If

            Dim exitFlag As Boolean = False
            strSql = " SELECT u.userID,u.userName,u.leaveDate " _
                    & " FROM tcpc0.dbo.users u " _
                    & " WHERE cast(u.userNo as varchar)='" & wcode.Text.Trim & "' and u.plantCode='" & Session("PlantCode") & "' and u.isTemp='" & Session("temp") & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read()
                exitFlag = True

                If reader("leaveDate").ToString() <> "" Then
                    If DateAdd(DateInterval.Month, 2, CDate(reader("leaveDate"))) < DateTime.Now Then
                        ltlAlert.Text = "alert('此员工已离职！');Form1.name2value.focus();"
                        wcode.Text = ""
                        wname.Text = ""
                        uid.Text = ""
                        Exit Sub
                    Else
                        ltlAlert.Text = "alert('此员工属于离职员工！');Form1.name2value.select();Form1.name2value.focus();"
                    End If
                Else
                    ltlAlert.Text = "Form1.name2value.select();Form1.name2value.focus();"
                End If
                wname.Text = reader("userName")
                uid.Text = reader(0)
                'DataGrid1.SelectedIndex = 
                workerNoSearch.Text = reader("userID")


                strSql = "Select userID, isnull(lJack,0),lJackDate,isnull(sJack,0),sJackDate,isnull(lCoat,0),lCoatDate From User_Uniform where userID='" & uid.Text & "' "
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
                With ds.Tables(0)
                    If .Rows.Count > 0 Then
                        'strSql = " Insert into User_Uniform (userID,lJack,lJackTotal,islJack,sJack,sJackTotal,issJack,lCoat,lCoatTotal,islCost) Values "
                        'strSql &= " ('" & wcode.Text & "','" & ljack & "','0','" & llimit.ToString() & "','" & sjack & "','0','" & slimit.ToString() & "','" & wcoat & "','0','" & wlimit.ToString() & "') "

                        If .Rows(0).Item(1).ToString() <> "0" Then
                            wear.Text = .Rows(0).Item(1).ToString()
                        Else
                            wear.Text = ""
                        End If
                        If .Rows(0).Item(2).ToString().Trim() <> "&nbsp;" Then
                            name2value.Text = .Rows(0).Item(2).ToString()
                        Else
                            name2value.Text = ""
                        End If

                        If .Rows(0).Item(3).ToString() <> "0" Then
                            shorte.Text = .Rows(0).Item(3).ToString()
                        Else
                            shorte.Text = ""
                        End If
                        If .Rows(0).Item(4).ToString() <> "&nbsp;" Then
                            Textbox1.Text = .Rows(0).Item(4).ToString()
                        Else
                            Textbox1.Text = ""
                        End If

                        If .Rows(0).Item(5).ToString() <> "0" Then
                            white.Text = .Rows(0).Item(5).ToString()
                        Else
                            white.Text = ""
                        End If
                        If .Rows(0).Item(6).ToString() <> "&nbsp;" Then
                            Textbox2.Text = .Rows(0).Item(6).ToString()
                        Else
                            Textbox2.Text = ""
                        End If
                    End If
                End With


                SaleBind(1)
            End While
            reader.Close()
            If exitFlag = False Then
                If wcode.Text.Trim() <> "" Then
                    ltlAlert.Text = "alert('工号不存在！');Form1.name2value.focus();"
                End If
                wcode.Text = ""
                wname.Text = ""
                uid.Text = ""
            End If

        End Sub

        Sub SaleBind(ByVal temp As Integer)

            Session("EXTitle") = "10^<b>id</b>~^60^<b>工号</b>~^80^<b>姓名</b>~^100^<b>部门</b>~^100^<b>长夹克申领日期</b>~^70^<b>件数</b>~^100^<b>短夹克申领日期</b>~^70^<b>件数</b>~^100^<b>白大袿申领日期</b>~^70^<b>件数</b>~^"

            'strSql = " Select uf.userID,u.userName as name,d.Name,isnull(lJack,0),lJackDate,isnull(lJackTotal,0),isnull(sJack,0),sJackDate,isnull(sJackTotal,0),isnull(lCoat,0),lCoatDate,isnull(lCoatTotal,0),isnull(islJack,0),isnull(issJack,0),isnull(islCost,0) "
            strSql = " Select u.userID,u.userNo,u.userName as name,d.Name,lJackDate,isnull(lJack,0),sJackDate,isnull(sJack,0),lCoatDate,isnull(lCoat,0)"
            strSql &= " From User_Uniform uf "
            strSql &= " Inner join tcpc0.dbo.users u ON u.userID=uf.userID "
            strSql &= " Left outer JOIN Departments d ON d.departmentID=u.departmentID "
            strSql &= " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and u.isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID")
            If chk_LeaveStaff.Checked Then
                strSql &= " "
            Else
                strSql &= " and u.leaveDate is null "
            End If

            If temp <> 0 Then
                If workerNoSearch.Text.Trim() <> "" Then
                    strSql &= " and cast(u.userNo as varchar)='" & workerNoSearch.Text.Trim & "'"
                End If
                If workerNameSearch.Text.Trim() <> "" Then
                    strSql &= " and lower(u.userName) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
                End If
                If department.SelectedValue > 0 Then
                    strSql &= " and u.departmentID = " & department.SelectedValue.ToString()
                End If
            End If


            strSql &= " Order by uf.createdate desc "
            'Response.Write(strSql)
            'Exit Sub
            Session("EXSQL") = strSql
            Session("EXHeader") = ""
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            'dt.Columns.Add(New DataColumn("roleName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("departmentName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("lnum", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ljackdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("snum", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("sjackdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("hname", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("wcoatdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("wnum", System.Type.GetType("System.Int32")))

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("userID") = .Rows(i).Item(0)
                        dr1.Item("userNo") = .Rows(i).Item(1)
                        dr1.Item("name") = "<u>" & .Rows(i).Item(2).ToString().Trim() & "</u>"
                        dr1.Item("hname") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("departmentName") = .Rows(i).Item(3).ToString().Trim()


                        dr1.Item("lnum") = .Rows(i).Item(5).ToString().Trim()
                        If .Rows(i).Item(4).ToString() <> "" Then
                            dr1.Item("ljackdate") = Format(.Rows(i).Item(4), "yyyy-MM-dd")
                        Else
                            dr1.Item("ljackdate") = ""
                        End If
                        dr1.Item("snum") = .Rows(i).Item(7)
                        If .Rows(i).Item(6).ToString() <> "" Then
                            dr1.Item("sjackdate") = Format(.Rows(i).Item(6), "yyyy-MM-dd")
                        Else
                            dr1.Item("sjackdate") = ""
                        End If

                        dr1.Item("wnum") = .Rows(i).Item(9)
                        If .Rows(i).Item(8).ToString() <> "" Then
                            dr1.Item("wcoatdate") = Format(.Rows(i).Item(8), "yyyy-MM-dd")
                        Else
                            dr1.Item("wcoatdate") = ""
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

        End Sub

        Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
            SaleBind(1)
        End Sub

        Sub Save_uniform(ByVal sender As Object, ByVal e As System.EventArgs)

            'Dim userflag As Boolean = False
            Dim ljack As String
            'Dim llimit As Integer = 1
            Dim sjack As String
            'Dim slimit As Integer = 1
            Dim wcoat As String
            'Dim wlimit As Integer = 1
            If wear.Text.Trim() = "" And shorte.Text.Trim() = "" And white.Text.Trim() = "" Then
                ltlAlert.Text = "alert('信息不完整！');Form1.wcode.focus();"
                Exit Sub
            End If

            If wear.Text.Trim() <> "" Then
                ljack = wear.Text.Trim()
                If name2value.Text.Trim = "" Then
                    ltlAlert.Text = "alert('不能没有日期！');Form1.name2value.focus();"
                    Exit Sub
                End If
            Else
                ljack = "0"
            End If

            If shorte.Text.Trim() <> "" Then
                sjack = shorte.Text.Trim()
                If Textbox1.Text.Trim = "" Then
                    ltlAlert.Text = "alert('不能没有日期！');Form1.Textbox1.focus();"
                    Exit Sub
                End If
            Else
                sjack = "0"
            End If

            If white.Text.Trim() <> "" Then
                wcoat = white.Text.Trim()
                If Textbox2.Text.Trim = "" Then
                    ltlAlert.Text = "alert('不能没有日期！');Form1.Textbox2.focus();"
                    Exit Sub
                End If
            Else
                wcoat = "0"
            End If
            If Btsave.Text = "新增" Then
                strSql = "Select userID, lJackDate,isnull(lJack,0),sJackDate,isnull(sJack,0),lCoatDate,isnull(lCoat,0) From User_Uniform where userID='" & uid.Text & "' "
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
                If ds.Tables(0).Rows.Count <= 0 Then
                    strSql = " Insert into User_Uniform (userID,lJack,lJackDate,lJackTotal,islJack,sJack,sJackDate,sJackTotal,issJack,lCoat,lCoatDate,lCoatTotal,islCost,createdate) Values "
                    strSql &= " ('" & uid.Text & "','" & ljack & "',"
                    If name2value.Text.Trim <> "" And ljack <> "0" Then
                        strSql &= " '" & name2value.Text.ToString() & "','0','1','" & sjack & "',"
                    Else
                        strSql &= " null,'0','1','" & sjack & "', "
                    End If

                    If Textbox1.Text.Trim <> "" And sjack <> "0" Then
                        strSql &= " '" & Textbox1.Text.ToString() & "','0','1','" & wcoat & "', "
                    Else
                        strSql &= " null,'0','1','" & wcoat & "',"
                    End If

                    If Textbox2.Text.Trim <> "" And wcoat <> "0" Then
                        strSql &= " '" & Textbox2.Text.ToString() & "','0','1',getdate()) "
                    Else
                        strSql &= " null,'0','1',getdate()) "
                    End If
                Else
                    'reger.Text = "0"
                    strSql = " update User_Uniform set  createdate=getdate() "
                    If ljack <> "0" Then
                        strSql &= " ,lJack='" & ljack & "',lJackDate='" & name2value.Text.ToString() & "'"
                    Else
                        strSql &= " ,lJack=0,lJackDate=null "
                    End If
                    If sjack <> "0" Then
                        strSql &= " ,sJack='" & sjack & "',sJackDate='" & Textbox1.Text.ToString() & "'"
                    Else
                        strSql &= " ,sJack=0,sJackDate=null "
                    End If
                    If wcoat <> "0" Then
                        strSql &= " ,lCoat='" & wcoat & "',lCoatDate='" & Textbox2.Text.ToString() & "' "
                    Else
                        strSql &= " ,lCoat=0,lCoatDate=null "
                    End If
                    strSql &= "Where userID = '" & uid.Text.Trim() & "' "
                End If
            ElseIf Btsave.Text = "保存" Then
                strSql = " update User_Uniform set createdate=getdate() "
                If ljack <> "0" Then
                    strSql &= " ,lJack='" & ljack & "',lJackDate='" & name2value.Text.ToString() & "'"
                Else
                    strSql &= " ,lJack=0,lJackDate=null "
                End If
                If sjack <> "0" Then
                    strSql &= " ,sJack='" & sjack & "',sJackDate='" & Textbox1.Text.ToString() & "'"
                Else
                    strSql &= " ,sJack=0,sJackDate=null "
                End If
                If wcoat <> "0" Then
                    strSql &= " ,lCoat='" & wcoat & "',lCoatDate='" & Textbox2.Text.ToString() & "' "
                Else
                    strSql &= " ,lCoat=0,lCoatDate=null "
                End If
                strSql &= "Where userID = '" & uid.Text.Trim() & "' "
            End If
            'Response.Write(strSql)
            'Exit Sub
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            DataGrid1.SelectedIndex = 0

            checkuniform(reger.Text.Trim(), ljack, sjack, wcoat)

            SaleBind(1)
            wcode.Text = ""
            wname.Text = ""
            wear.Text = ""
            'lflag.Checked = False
            shorte.Text = ""
            'sflag.Checked = False
            white.Text = ""
            'name2value.Text = ""
            'Textbox1.Text = ""
            'Textbox2.Text = ""

            reger.Text = "1"
            ltlAlert.Text = "Form1.wcode.focus();"
            'wflag.Checked = False
            Btsave.Text = "新增"
        End Sub

        Function checkuniform(ByVal temp As Integer, ByVal num1 As String, ByVal num2 As String, ByVal num3 As String)
            Dim i As Integer
            Dim putdate(3) As String
            Dim inspectnum(3) As String
            Array.Clear(putdate, 0, 3)
            Array.Clear(inspectnum, 0, 3)

            putdate(1) = name2value.Text.Trim
            putdate(2) = Textbox1.Text.Trim
            putdate(3) = Textbox2.Text.Trim
            inspectnum(1) = num1
            inspectnum(2) = num2
            inspectnum(3) = num3
            If temp = 0 Then  'edit
                For i = 1 To 3
                    strSql = " Delete From User_UniformDetail Where userUniformDetailID in (select top 1 isnull(userUniformDetailID,'0') From User_UniformDetail Where userID='" & uid.Text.Trim() & "' and uniform='" & i.ToString() & "' order by userUniformDetailID desc)"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                Next

            End If

            For i = 1 To 3
                If inspectnum(i) <> "0" Then
                    strSql = " Insert into User_UniformDetail(userID,uniform,uniformDate,createdBy,createdDate,num) Values "
                    strSql &= " ('" & uid.Text.Trim() & "','" & i.ToString() & "' ,'" & putdate(i) & "','" & Session("uID") & "',getdate(),'" & inspectnum(i) & "')"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                End If
            Next
            'strSql = " Select userUniformDetailID From User_UniformDetail where userID='" & wcode.Text.Trim() & "' and uniformDate='" & & "' and num='" & .Trim() & "' "

        End Function

        Public Sub Edit_edit(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            Btsave.Text = "保存"
            reger.Text = "0"
            wcode.Text = e.Item.Cells(0).Text
            wname.Text = e.Item.Cells(10).Text
            uid.Text = e.Item.Cells(11).Text

            If e.Item.Cells(4).Text <> "0" Then
                wear.Text = e.Item.Cells(4).Text
            Else
                wear.Text = ""
            End If
            If e.Item.Cells(5).Text <> "&nbsp;" Then
                name2value.Text = e.Item.Cells(5).Text
            Else
                name2value.Text = ""
            End If
            'If e.Item.Cells(13).Text = "0" Then
            '    lflag.Checked = True
            'Else
            '    lflag.Checked = False
            'End If

            If e.Item.Cells(6).Text <> "0" Then
                shorte.Text = e.Item.Cells(6).Text
            Else
                shorte.Text = ""
            End If
            If e.Item.Cells(5).Text.Trim() <> "&nbsp;" Then
                Textbox1.Text = e.Item.Cells(5).Text
            Else
                Textbox1.Text = ""
            End If

            'If e.Item.Cells(14).Text = "0" Then
            '    sflag.Checked = True
            'Else
            '    sflag.Checked = False
            'End If

            If e.Item.Cells(8).Text <> "0" Then
                white.Text = e.Item.Cells(8).Text
            Else
                white.Text = ""
            End If

            If e.Item.Cells(7).Text.Trim() <> "&nbsp;" Then
                Textbox2.Text = e.Item.Cells(7).Text
            Else
                Textbox2.Text = ""
            End If

            DataGrid1.SelectedIndex = e.Item.ItemIndex

            'If e.Item.Cells(15).Text = "0" Then
            '    wflag.Checked = True
            'Else
            '    wflag.Checked = False
            'End If

        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            SaleBind(1)
        End Sub

        Public Sub Detail(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("Detail") = 0) Then
                ltlAlert.Text = "window.open('/admin/CoatDetail.aspx?uid=" & e.Item.Cells(11).Text & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0') "
            End If
        End Sub


        Sub changeuniform(ByVal sender As Object, ByVal e As System.EventArgs)
            
            Response.Redirect(chk.urlRand("/admin/InspectUniform.aspx"))
        End Sub
        Sub Btn_CancelOnClick(ByVal sender As Object, ByVal e As System.EventArgs)
            workerNoSearch.Text = String.Empty
            wcode.Text = String.Empty
            wname.Text = String.Empty
            wear.Text = ""
            name2value.Text = ""
            shorte.Text = ""
            Textbox1.Text = ""
            white.Text = ""
            Textbox2.Text = ""
            Btsave.Text = "新增"
            reger.Text = "1"
            DataGrid1.SelectedIndex = -1
            SaleBind(1)
        End Sub
    End Class

End Namespace
