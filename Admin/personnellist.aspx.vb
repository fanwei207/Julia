Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

    Partial Class personnellist
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents P1 As System.Web.UI.WebControls.Panel
        Protected WithEvents chkall As System.Web.UI.WebControls.CheckBox
        Shared sortOrder As String = ""
        Protected strTemp As String
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
                Session("linkurl") = Request.RawUrl

                BindData()
            End If
        End Sub
        Private Sub BindData()
            Dim query As String
            Dim ds As DataSet
            Dim levdatefr As DateTime
            Dim entdatefr As DateTime
            Dim levdateto As DateTime
            Dim entdateto As DateTime

            query = " SELECT u.userNo,u.userName as name,roleName,d.Name,LoginName,userPWD,isnull(sc1.systemcodeName,''),datediff(year,birthday,getdate()),isnull(sc2.systemcodeName,''),"
            If Request("conceal") = 0 Then
                query &= " case when u.contractTypeID=31 then u.wldate else u.enterDate end as enterDate ,w.name,u.leaveDate,isnull(sc3.systemcodeName,''),u.comments,CASE WHEN u.contractTypeID=31 then case when u.wldate is null THEN '' ELSE CASE WHEN MONTH(u.wldate)<=MONTH(Isnull(u.leaveDate, getdate())) THEN datediff(year,u.wldate,Isnull(u.leaveDate, getdate())) ELSE datediff(year,u.wldate,Isnull(u.leaveDate, getdate()))-1 END END else case when u.enterdate is null THEN '' ELSE CASE WHEN MONTH(u.enterdate)<=MONTH(Isnull(u.leaveDate, getdate())) THEN datediff(year,u.enterdate, Isnull(u.leaveDate, getdate())) ELSE datediff(year,u.enterdate, Isnull(u.leaveDate, getdate()))-1 END END end,"
            Else
                query &= " u.enterDate ,w.name,u.leaveDate,isnull(sc3.systemcodeName,''),u.comments,CASE WHEN u.enterdate is null THEN '' ELSE CASE WHEN MONTH(u.enterdate)<=MONTH(getdate()) THEN datediff(year,u.enterdate, Isnull(u.leaveDate, getdate())) ELSE datediff(year,u.enterdate, Isnull(u.leaveDate, getdate()))-1 END END ,"
            End If
            query &= " isnull(sc4.systemcodeName,''),isnull(u.IC,''), ISNULL(sc6.systemCodeName,'') AS province,isnull(sc5.systemcodeName,''),u.userID, uType = isnull(sc7.systemCodeName,'') "
            query &= " From tcpc0.dbo.users u "
            query &= " Left outer JOIN Roles r ON u.roleID=r.roleID "
            query &= " Left outer JOIN Departments d ON d.departmentID=u.departmentID "
            query &= " Left outer JOIN tcpc0.dbo.systemCode sc1 ON u.sexID=sc1.systemcodeID "
            query &= " Left outer JOIN tcpc0.dbo.systemCodeType st1 ON sc1.systemcodetypeID=st1.systemcodetypeID and st1.systemCodeTypeName='Sex' "
            query &= " Left outer JOIN tcpc0.dbo.systemCode sc2 ON u.educationID=sc2.systemcodeID "
            query &= " Left outer JOIN tcpc0.dbo.systemCodeType st2 ON sc2.systemcodetypeID=st2.systemcodetypeID and st2.systemCodeTypeName='Education' "
            query &= " Left outer JOIN Workshop w ON w.id=u.workshopID and w.workshopID is null "
            query &= " Left outer JOIN tcpc0.dbo.systemCode sc3 ON sc3.systemcodeID=u.workTypeID "
            query &= " Left outer JOIN tcpc0.dbo.systemCodeType st3 ON sc3.systemcodetypeID=st3.systemcodetypeID and st3.systemCodeTypeName='Work Procedure Type' "
            query &= " LEFT OUTER JOIN tcpc0.dbo.systemCode sc4 ON u.employTypeID = sc4.systemCodeID "
            query &= " LEFT OUTER JOIN tcpc0.dbo.systemCode sc5 ON u.insuranceTypeID = sc5.systemCodeID "
            query &= " LEFT OUTER JOIN tcpc0.dbo.systemCode sc6 ON u.provinceID = sc6.systemCodeID "
            query &= " LEFT OUTER JOIN tcpc0.dbo.systemCode sc7 ON u.userType = sc7.systemCodeID "
            query &= " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and (u.roleID>1 or u.roleID is null) and u.organizationID=" & Session("orgID")

            If Request("conceal") = 0 And Session("uRole") <> 1 Then
                query &= "   AND u.departmentID<> '183' AND u.userNO NOT IN ('999A','999B')"
            End If

            If Request("dp") <> 0 Then
                query &= " and u.departmentID=" & Request("dp")
            End If
            Dim str = Request("ro")
            Dim str1 = Request("RoleType")
            If Request("ro") <> 0 Then
                query &= " and u.roleID=" & Request("ro")
            Else
                If Request("RoleType") = 0 Then
                    'query &= " and u.roleID > 1 "
                ElseIf (Request("RoleType") = 1) Then
                    query &= " and u.roleID>=100 and u.roleID<300"
                ElseIf (Request("RoleType") = 2) Then
                    query &= " and u.roleID>=300 and u.roleID<500"
                ElseIf (Request("RoleType") = 3) Then
                    query &= " and u.roleID>=500 and u.roleID<1000"
                ElseIf (Request("RoleType") = 3) Then
                    query &= " and u.roleID>=1000 and u.roleID<5000"
                End If
            End If

            If Request("ed") <> 0 Then
                query &= " and u.educationID=" & Request("ed")
            End If

            If Request("age") <> 0 Then
                If Request("age") = 1 Then
                    query &= " and datediff(year,birthday,getdate()) < 20 "
                End If
                If Request("age") = 2 Then
                    query &= " and datediff(year,birthday,getdate()) >= 20 and datediff(year,birthday,getdate()) < 30"
                End If
                If Request("age") = 3 Then
                    query &= " and datediff(year,birthday,getdate()) >= 30 and datediff(year,birthday,getdate()) < 40"
                End If
                If Request("age") = 4 Then
                    query &= " and datediff(year,birthday,getdate()) >= 40 and datediff(year,birthday,getdate()) < 50"
                End If
                If Request("age") = 5 Then
                    query &= " and datediff(year,birthday,getdate()) >= 50 and datediff(year,birthday,getdate()) < 60"
                End If
                If Request("age") = 6 Then
                    query &= " and datediff(year,birthday,getdate()) >= 60"
                End If
            End If
            If Request("ct") = -1 Then
                query &= " and u.contracttypeID is null "
            ElseIf Request("ct") = -2 Then
                query &= " and u.contractTypeID is not null "
            ElseIf Request("ct") <> 0 Then
                query &= " and u.contractTypeID=" & Request("ct")
            End If

            If Request("op") <> 0 Then
                query &= " and u.occupationID=" & Request("op")
            End If

            If Request("sex") <> 0 Then
                query &= " and u.sexID=" & Request("sex")
            End If

            If Request("t1") <> "" Then
                query &= " and u.certificates like N'%" & HttpUtility.HtmlDecode(Request("t1")).Trim.ToLower() & "%'"
            End If

            If Request("name") <> "" Then
                query &= " and replace(u.username,' ','') like N'%" & HttpUtility.HtmlDecode(Request("name")).Trim.ToLower() & "%'"
            End If

            If Request("pv") <> 0 Then
                query &= " and u.provinceID=" & Request("pv")
            End If

            If Request("note") <> "" Then
                query &= " and u.comments like N'%" & HttpUtility.HtmlDecode(Request("note")).Trim.ToLower() & "%'"
            End If

            If Request("fund") <> 0 Then
                query &= " and u.workshopID=" & Request("fund")
            End If
            If Request("sc") = Nothing Then
                If Request("hF") <> Nothing Then
                    query &= " and u.houseFund=1 "
                End If
                If Request("mF") <> Nothing Then
                    query &= " and u.medicalFund=1 "
                End If
                If Request("uF") <> Nothing Then
                    query &= " and u.unemployFund=1 "
                End If
                If Request("rF") <> Nothing Then
                    query &= " and u.retiredFund=1 "
                End If
                If Request("sF") <> Nothing Then
                    query &= " and u.sretiredFund=1 "
                End If
            Else
                query &= " and u.houseFund=0 "
                query &= " and u.medicalFund=0 "
                query &= " and u.unemployFund=0 "
                query &= " and u.retiredFund=0 "
                query &= " and u.sretiredFund=0 "
            End If


            If Request("insurance") = -1 Then
                query &= " and  u.insuranceTypeID is null "
            ElseIf Request("insurance") = -2 Then
                query &= " and  u.insuranceTypeID is not null "
            ElseIf Request("insurance") <> 0 Then
                query &= " and u.insuranceTypeID=" & Request("insurance")
            End If

            If (Request("lu") = 1 Or Request("lu") = 2) And Request("lu") <> Nothing Then
                query &= " and u.isLabourUnion='" & Request("lu") & "' "
            End If

            If Request("id") <> "" Then
                Try
                    'Dim ID As Integer = CInt(Request("id"))
                    query &= " and u.userNo='" & Request("id") & "'"
                Catch ex As Exception
                End Try

            End If

            If Request("levfr") <> "" Then
                Try
                    levdatefr = CDate(Request("levfr"))
                    query &= " AND u.leaveDate>='" & levdatefr & "'"
                Catch ex As Exception
                End Try
            End If

            If Request("levto") <> "" Then
                Try
                    levdateto = CDate(Request("levto"))
                    query &= " AND u.leaveDate<'" & levdateto.AddDays(1) & "'"
                Catch ex As Exception
                End Try
            End If

            If Request("entfr") <> "" Then
                Try
                    entdatefr = CDate(Request("entfr"))
                    query &= " AND u.enterDate>='" & entdatefr & "'"
                Catch ex As Exception
                End Try
            End If

            If Request("entto") <> "" Then
                Try
                    entdateto = CDate(Request("entto"))
                    query &= " AND u.enterDate<'" & entdateto.AddDays(1) & "'"
                Catch ex As Exception
                End Try
            End If

            If Request("all") = Nothing Then
                If Request("lev") = "" Then
                    query &= " AND u.leavedate is null "
                End If
            End If

            'If Request("wear") <> Nothing Then
            '    If Request("wear") <> "0" Then
            '        query &= " AND u.uniform=" & Request("wear")
            '    End If
            'End If

            If Request("sWT") > 0 Then
                query &= " AND u.specialWorkType=" & Request("sWT")
            End If

            If Request("ygxz") > 0 Then
                query &= " AND u.employTypeID=" & Request("ygxz")
            End If
            If Request("jcfs") > 0 Then
                query &= " AND u.workTypeID=" & Request("jcfs")
            End If

            If Request("idc") <> "" Then
                Try
                    'Dim cID As Integer = CInt(Request("idc"))
                    query &= " and u.IC='" & Request("idc") & "'"
                Catch ex As Exception
                End Try

            End If
            If Request("aa") <> "" Then
                query &= " and u.area like N'%" & HttpUtility.HtmlDecode(Request("aa")).Trim.ToLower() & "%'"
            End If

            If Request("bg") <> "" Then
                query &= " and u.begood like N'%" & HttpUtility.HtmlDecode(Request("bg")).Trim.ToLower() & "%'"
            End If

            query &= " Order by u.userID "

            'Response.Write(query)
            'Exit Sub

            Dim total As Integer = 0

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("roleName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("departmentName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userPWD", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("sex", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("age", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("education", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("enterDate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("edate", System.Type.GetType("System.DateTime")))
            dt.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("leave", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("leavedate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workyear", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("usertype", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ICard", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("provinceID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("insuranceType", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("uType", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("userNo") = .Rows(i).Item(0)
                        dr1.Item("gsort") = i + 1
                        dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("roleName") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("departmentName") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("userName") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("userPWD") = .Rows(i).Item(5).ToString().Trim()
                        dr1.Item("sex") = .Rows(i).Item(6).ToString().Trim()
                        dr1.Item("age") = .Rows(i).Item(7)
                        dr1.Item("education") = .Rows(i).Item(8).ToString().Trim()
                        If .Rows(i).Item(9).ToString().Trim() <> "" Then
                            dr1.Item("enterDate") = .Rows(i).Item(9).ToShortDateString()
                            dr1.Item("edate") = .Rows(i).Item(9).ToShortDateString()
                        Else
                            dr1.Item("enterDate") = DateTime.Now().ToShortDateString()
                            dr1.Item("edate") = DateTime.Now().ToShortDateString()
                        End If
                        dr1.Item("workshop") = .Rows(i).Item(10).ToString().Trim()
                        dr1.Item("type") = .Rows(i).Item(12)
                        If .Rows(i).Item(11).ToString().Trim() <> "" Then
                            dr1.Item("leavedate") = .Rows(i).Item(11).ToShortDateString()
                            dr1.Item("leave") = "是"
                        Else
                            dr1.Item("leavedate") = ""
                            dr1.Item("leave") = "否"
                        End If
                        dr1.Item("workyear") = .Rows(i).Item(14)

                        dr1.Item("usertype") = .Rows(i).Item(15)
                        dr1.Item("ICard") = .Rows(i).Item(16)
                        dr1.Item("provinceID") = .Rows(i).Item(17)
                        dr1.Item("insuranceType") = .Rows(i).Item(18)
                        dr1.Item("userID") = .Rows(i).Item(19)
                        dr1.Item("uType") = .Rows(i).Item(20)
                        total = total + 1
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            If Request("conceal") = 1 Then
                Label1.Text = "<b>人数： </b>" & total.ToString()
            End If
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try

            'If Session("uid") = 12 Or Session("uid") = 1122 Then
            If Session("uid") = "1122" Then
                DataGrid1.Columns(5).Visible = False
                DataGrid1.Columns(6).Visible = False
                DataGrid1.Columns(7).Visible = False
                DataGrid1.Columns(12).Visible = False
                DataGrid1.Columns(13).Visible = False
                DataGrid1.Columns(14).Visible = False
                DataGrid1.Columns(15).Visible = False
                DataGrid1.Columns(16).Visible = False
            Else
                If Request("conceal") = 1 Then
                    DataGrid1.Columns(8).Visible = False
                    DataGrid1.Columns(9).Visible = False
                    DataGrid1.Columns(14).Visible = False
                    DataGrid1.Columns(15).Visible = False
                Else
                    DataGrid1.Columns(8).Visible = False
                    DataGrid1.Columns(9).Visible = False
                    DataGrid1.Columns(4).Visible = False
                    DataGrid1.Columns(7).Visible = False
                    DataGrid1.Columns(13).Visible = False

                End If
            End If
        End Sub

        Public Sub editBt(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("editBt") = 0) Then
                Dim str As String = e.Item.Cells(19).Text
                'ltlAlert.Text = "openWin('addpersonnel.aspx?id='+'" & str & "' );"

                Response.Redirect("addpersonnel.aspx?id=" & str)
            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub chkall_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkall.CheckedChanged
            BindData()
        End Sub

        Public Sub editUser(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("editUser") = 0) Then
                Dim str As String = e.Item.Cells(19).Text
                If Session("uid") = "12" Then
                    'ltlAlert.Text = " openWin('personalDetail.aspx?uid='+'" & str & "&fr=person');"
                    Response.Redirect("personalDetail.aspx?uid=" & str & "&fr=person")
                Else
                    'ltlAlert.Text = " openWin('access2.aspx?uid='+'" & str & "&fr=person');"
                    Response.Redirect("access2.aspx?uid=" & str & "&fr=person")
                End If

            End If
        End Sub
        Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.ServerClick
            If (Request("conceal") = 0) Then
                Response.Redirect("personelsearchConceal.aspx")
            Else
                Response.Redirect("personelsearchConceal1.aspx")
            End If

        End Sub
        Private Sub BtnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAll.Click
            Session("linkurl") = ""
            Response.Redirect(chk.urlRand("personnellist.aspx?conceal=" & Request("conceal")))
        End Sub

        Private Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click

            Session("conceal") = Request("conceal")

            ltlAlert.Text = " var w=window.open('printpersonnelinfo.aspx"
            Dim ref As Boolean = False
            If Request("dp") <> 0 Then
                ltlAlert.Text &= "?dp=" & Request("dp")
                ref = True
            End If
            If Request("ro") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&ro=" & Request("ro")
                Else
                    ltlAlert.Text &= "?ro=" & Request("ro")
                    ref = True
                End If
            End If
            If Request("ed") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&ed=" & Request("ed")
                Else
                    ltlAlert.Text &= "?ed=" & Request("ed")
                    ref = True
                End If
            End If
            If Request("age") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&age=" & Request("age")
                Else
                    ltlAlert.Text &= "?age=" & Request("age")
                    ref = True
                End If
            End If
            If Request("ct") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&ct=" & Request("ct")
                Else
                    ltlAlert.Text &= "?ct=" & Request("ct")
                    ref = True
                End If
            End If
            If Request("op") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&op=" & Request("op")
                Else
                    ltlAlert.Text &= "?op=" & Request("op")
                    ref = True
                End If
            End If
            If Request("sex") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&sex=" & Request("sex")
                Else
                    ltlAlert.Text &= "?sex=" & Request("sex")
                    ref = True
                End If
            End If
            If Request("t1") <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&t1=" & Request("t1")
                Else
                    ltlAlert.Text &= "?t1=" & Request("t1")
                    ref = True
                End If
            End If
            If Request("insurance") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&insurance=" & Request("insurance")
                Else
                    ltlAlert.Text &= "?insurance=" & Request("insurance")
                    ref = True
                End If
            End If
            If Request("name") <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&name=" & Request("name")
                Else
                    ltlAlert.Text &= "?name=" & Request("name")
                    ref = True
                End If
            End If
            If Request("pv") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&pv=" & Request("pv")
                Else
                    ltlAlert.Text &= "?pv=" & Request("pv")
                    ref = True
                End If
            End If
            If Request("note") <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&note=" & Request("note")
                Else
                    ltlAlert.Text &= "?note=" & Request("note")
                    ref = True
                End If
            End If
            If Request("fund") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&fund=" & Request("fund")
                Else
                    ltlAlert.Text &= "?fund=" & Request("fund")
                    ref = True
                End If
            End If
            If Request("lu") <> 0 Then
                If ref = True Then
                    ltlAlert.Text &= "&lu=" & Request("lu")
                Else
                    ltlAlert.Text &= "?lu=" & Request("lu")
                    ref = True
                End If
            End If
            If Request("id") <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&id=" & Request("id")
                Else
                    ltlAlert.Text &= "?id=" & Request("id")
                    ref = True
                End If
            End If
            If Request("entfr") <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&entfr=" & Request("entfr")
                Else
                    ltlAlert.Text &= "?entfr=" & Request("entfr")
                    ref = True
                End If
            End If
            If Request("entto") <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&entto=" & Request("entto")
                Else
                    ltlAlert.Text &= "?entto=" & Request("entto")
                    ref = True
                End If
            End If
            If Request("levfr") <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&levfr=" & Request("levfr")
                Else
                    ltlAlert.Text &= "?levfr=" & Request("levfr")
                    ref = True
                End If
            End If
            If Request("levto") <> "" Then
                If ref = True Then
                    ltlAlert.Text &= "&levto=" & Request("levto")
                Else
                    ltlAlert.Text &= "?levto=" & Request("levto")
                    ref = True
                End If
            End If
            If Request("all") <> Nothing Then
                If ref = True Then
                    ltlAlert.Text &= "&all=true"
                Else
                    ltlAlert.Text &= "?all=true"
                    ref = True
                End If
            End If

            If Request("sWT") <> Nothing Then
                If ref = True Then
                    ltlAlert.Text &= "&sWT=" & Request("sWT")
                Else
                    ltlAlert.Text &= "?sWT=" & Request("sWT")
                    ref = True
                End If
            End If

            If Request("jcfs") <> Nothing Then
                If ref = True Then
                    ltlAlert.Text &= "&jcfs=" & Request("jcfs")
                Else
                    ltlAlert.Text &= "?jcfs=" & Request("jcfs")
                    ref = True
                End If
            End If

            If Request("ygxz") <> Nothing Then
                If ref = True Then
                    ltlAlert.Text &= "&ygxz=" & Request("ygxz")
                Else
                    ltlAlert.Text &= "?ygxz=" & Request("ygxz")
                    ref = True
                End If
            End If

            ltlAlert.Text &= "','personnelinfo','menubar=yes,scrollbars = yes,resizable=yes,width=700,height=500,top=0,left=0');w.focus(); "
        End Sub

        Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
            If (e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item) Then

                Try
                    Dim str As String
                    Dim d As DateTime = Convert.ToDateTime(e.Item.Cells(10).Text)
                    str = d.Year.ToString()

                    If (d.Month < 10) Then
                        str &= "-0" & d.Month.ToString()
                    Else
                        str &= "-" & d.Month.ToString()
                    End If

                    If (d.Day < 10) Then
                        str &= "-0" & d.Day.ToString()
                    Else
                        str &= "-" & d.Day.ToString()
                    End If

                    e.Item.Cells(10).Text = str
                Catch ex As Exception

                End Try

                Try
                    Dim str As String
                    Dim d As DateTime = Convert.ToDateTime(e.Item.Cells(11).Text)
                    str = d.Year.ToString()

                    If (d.Month < 10) Then
                        str &= "-0" & d.Month.ToString()
                    Else
                        str &= "-" & d.Month.ToString()
                    End If

                    If (d.Day < 10) Then
                        str &= "-0" & d.Day.ToString()
                    Else
                        str &= "-" & d.Day.ToString()
                    End If

                    e.Item.Cells(11).Text = str
                Catch ex As Exception

                End Try
            End If
        End Sub
    End Class

End Namespace
