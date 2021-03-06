Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class printpersonnelinfo
        Inherits System.Web.UI.Page
        Dim chk As New adamClass
        Dim reader As SqlDataReader
        Dim row As TableRow
        Dim cell As TableCell

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
             
            End If

            Dim i As Integer = 0
            Dim birth, enter, leave, employ, specialWorkType, cleave, wldate, contractDate As String
            Dim levdatefr As DateTime
            Dim entdatefr As DateTime
            Dim levdateto As DateTime
            Dim entdateto As DateTime
            If Session("conceal") = 1 Then
                PIMasteryRow("<b>工号</b>", "<b>姓名</b>", "<b>职务</b>", "<b>性别</b>", "<b>生日</b>", "<b>家庭地址</b>", "<b>目前住址</b>", _
                             "<b>身份证号码</b>", "<b>家庭地址邮编</b>", "<b>目前住址邮编</b>", "<b>电话号码</b>", "<b>手机号码</b>", "<b>电子邮件</b>", _
                             "<b>传真号码</b>", "<b>调入日期</b>", "<b>部门</b>", "<b>职称</b>", "<b>学历</b>", "<b>证书</b>", "<b>进入单位日期</b>", "<b>离开单位日期</b>", _
                             "<b>备注</b>", "<b>合同类型</b>", "<b>户籍</b>", "<b>工段</b>", "<b>计酬方式</b>", "<b>班组</b>", "<b>婚姻状况</b>", _
                             "<b>介绍人</b>", "<b>转正日期</b>", "<b>用工性质</b>", "<b>工会会员</b>", "<b>保险类型</b>", "<b>特殊工种</b>", "<b>工龄</b>", "<b>住房公积金</b>", "<b>养老保险金</b>", "<b>医疗保险金</b>", "<b>失业保险金</b>", "<b>补充养老</b>", "<b>地区</b>", "<b>特长</b>", "<b>公司辞退</b>", "<b>入工会时间</b>", "<b>工种</b>", "<b>劳务公司</b>", "<b>派遣日期</b>", "<b>工龄２</b>", "<b>年龄</b>", "<b>合同日期</b>", "<b>员工类型</b>", "<b>考勤编号</b>", "<b>英文名</b>", "<b>身份证有效期</b>", _
                             "<b>保险-缴费年月</b>", "<b>保险-转出年月</b>", "<b>公积金-缴费年月</b>", "<b>公积金-转出年月</b>", "<b>户口类型</b>", "<b>政治面貌</b>", "<b>民族</b>")
            Else
                PIMasteryRow("<b>工号</b>", "<b>姓名</b>", "<b>职务</b>", "<b>部门</b>", "<b>性别</b>", _
                                 "<b>年龄</b>", "<b>进入公司日期</b>", "<b>离职日期</b>", "<b>工龄</b>", _
                                 "<b>身证号</b>", "<b>户籍</b>", "<b>联系地址</b>", "<b>保险类型</b>", "<b>电话号码</b>", "<b>手机号码</b>", "<b>考勤编号</b>", "<b>岗位</b>", "<b>身份证有效期</b>", "<b>员工类型</b>", "英文名", "身份证有效期", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            End If

            Dim Query As String
            If Session("conceal") = 1 Then
                Query = " SELECT u.userNo, u.userName, ISNULL(r.roleName,''), ISNULL(sc1.systemCodeName,'') AS sex, u.birthday, ISNULL(u.homeAddress,''), " _
                      & " ISNULL(u.currentAddress,''), ISNULL(u.IC,''), ISNULL(u.homeZip,''), ISNULL(u.currentZip,''), ISNULL(u.phone,''), ISNULL(u.mobile,''), " _
                      & " ISNULL(u.email,''), ISNULL(u.fax,''), ISNULL(d.name,''), ISNULL(sc2.systemCodeName ,'') AS occupation, " _
                      & " ISNULL(sc3.systemCodeName,'') AS education, ISNULL(u.certificates,''), u.enterDate, u.leaveDate, ISNULL(u.comments,''), " _
                      & " ISNULL(sc4.systemCodeName, '') AS contracttype, ISNULL(sc5.systemCodeName,'') AS province, ISNULL(w1.name,'') AS workshop, " _
                      & " ISNULL(sc6.systemCodeName,'') AS worktype, ISNULL(w2.name,'') AS workgroup, ISNULL(sc7.systemCodeName,'') AS marriage, ISNULL(u.introducer,''), " _
                      & " u.employDate, ISNULL(sc8.systemCodeName,'') AS employtype, ISNULL(u.isLabourUnion,0), ISNULL(sc9.systemCodeName,'') AS insurancetype, " _
                      & " CASE WHEN ISNULL(u.houseFund,0)=1 THEN N'有'ELSE N'无'END ,CASE WHEN  ISNULL(u.retiredFund,0)=1 THEN N'有'ELSE N'无'END,CASE WHEN ISNULL(u.medicalFund,0)=1 THEN N'有'ELSE N'无'END ,CASE WHEN ISNULL(u.unemployFund,0)=1 THEN N'有'ELSE N'无'END , CASE WHEN ISNULL(u.sretiredFund,0)=1 THEN N'有'ELSE N'无'END,ISNULL(specialWorkType,0) as specialWorkType,CASE WHEN u.enterdate is null THEN '' ELSE CASE WHEN MONTH(u.enterdate)<=MONTH(getdate()) THEN datediff(year,u.enterdate, Isnull(u.leaveDate, getdate())) ELSE datediff(year,u.enterdate, Isnull(u.leaveDate, getdate()))-1 END END as workyear,isnull(u.area,''),isnull(u.begood,''),CASE WHEN ISNULL(u.leaveBycp,0)=1 THEN N'是'ELSE N'否'END,u.labedate,isnull(wk.name,''),  "
                Query &= " isnull(u.comp,''),u.wldate, case when u.wldate is null THEN '' ELSE CASE WHEN MONTH(u.wldate)<=MONTH(getdate()) THEN datediff(year,u.wldate,getdate()) ELSE datediff(year,u.wldate,getdate())-1 END END,CASE WHEN u.birthday is null then '' else datediff(year,u.birthday,getdate()) end"
                Query &= " ,u.contractDate,ISNULL(sc10.systemCodeName,'')  As userType,ISNULL(Fingerprint,'') As Fingerprint,isnull(u.englishName,''),isnull(u.IDeffectivedate,''),  "
                'Added By Chenyb    增加导出缴费年月、转出年月、公积金缴费年月、转出年月、户口类型、政治面貌、民族
                Query &= " ISNULL(insurancePayDate, '') as insurancePayDate, ISNULL(insuranceFinishDate, '') as insuranceFinishDate, "
                Query &= " isnull(houseFundPayDate, '') as houseFundPayDate, isnull(houseFundFinishDate, '') as houseFundFinishDate, "

                If Session("PlantCode") = 1 Then
                    Query &= " case when Isnull(isFarmRegister, 1) = 1 then N'农业户口' else N'非农业户口' end as registerType, isnull(politicalStatus, N'') as politicalStatus, isnull(nation, N'') as nation"
                Else
                    Query &= " case when isFarmRegister = 1 then N'农业户口' else N'非农业户口' end as registerType, isnull(politicalStatus, N'') as politicalStatus, isnull(nation, N'') as nation"
                End If

                Query &= " , exchangeDate = Isnull((SELECT TOP 1 ExchangeDate FROM User_Exchange_Department WHERE userID = u.userID ORDER BY createdDate desc,modifiedDate desc), '')"
                'End Added By Chenyb 

            Else
                Query = " SELECT u.userNo, u.userName, ISNULL(r.roleName,''),ISNULL(d.name,''), ISNULL(sc1.systemCodeName,'') AS sex,CASE WHEN u.birthday is null then '' else datediff(year,u.birthday,getdate()) end,case when u.contractTypeID=31 then u.wldate else u.enterDate end as enterDate, u.leaveDate, " _
                      & " CASE WHEN u.contractTypeID=31 then case when u.wldate is null THEN '' ELSE CASE WHEN MONTH(u.wldate)<=MONTH(getdate()) THEN datediff(year,u.wldate,getdate()) ELSE datediff(year,u.wldate,getdate())-1 END END else case when u.enterdate is null THEN '' ELSE CASE WHEN MONTH(u.enterdate)<=MONTH(getdate()) THEN datediff(year,u.enterdate, Isnull(u.leaveDate, getdate())) ELSE datediff(year,u.enterdate, Isnull(u.leaveDate, getdate()))-1 END END end as workyear, " _
                      & " ISNULL(u.IC,''), ISNULL(sc5.systemCodeName,'') AS province, ISNULL(u.homeAddress,''), ISNULL(sc9.systemCodeName,'') AS insurancetype,ISNULL(u.phone,'') as phone, ISNULL(u.mobile,'') as mobile, ISNULL(Fingerprint,'') As Fingerprint , ISNULL(postName,'') As postName, isnull(u.IDeffectivedate, '') IDeffectivedate, isnull(sc10.systemCodeName, '') uTypeName, isnull(u.englishName,''),isnull(u.IDeffectivedate,'') "
            End If
            Query &= " FROM tcpc0.dbo.users u " _
                    & " LEFT OUTER JOIN Roles r ON r.roleID = u.roleID " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc1 ON u.sexID = sc1.systemCodeID " _
                    & " LEFT OUTER JOIN departments d ON u.departmentID = d.departmentID " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc2 ON u.occupationID = sc2.systemCodeID " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc3 ON u.educationID = sc3.systemCodeID " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc4 ON u.contractTypeID = sc4.systemCodeID " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc5 ON u.provinceID = sc5.systemCodeID " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc6 ON u.workTypeID = sc6.systemCodeID " _
                    & " LEFT OUTER JOIN Workshop w1 ON u.workshopID = w1.id " _
                    & " LEFT OUTER JOIN Workshop w2 ON u.workProcedureID = w2.id " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc7 ON u.marriageID = sc7.systemCodeID " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc8 ON u.employTypeID = sc8.systemCodeID " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc9 ON u.insuranceTypeID = sc9.systemCodeID " _
                    & " LEFT OUTER JOIN tcpc0.dbo.systemCode sc10 ON u.userType = sc10.systemCodeID " _
                    & " LEFT OUTER JOIN workkinds wk ON u.kindswork = wk.ID " _
                    & " WHERE u.plantCode='" & Session("PlantCode") & "' AND u.deleted=0 AND (u.roleID>1 or u.roleID is null) AND u.organizationID ='" & Session("orgID") & "'"

            If Session("conceal") = 0 Then
                Query &= "  and u.departmentID<> '183' and u.userNO NOT IN ('999A','999B') "
            End If

            If Request("dp") <> 0 Then
                Query &= " and u.departmentID=" & Request("dp")
            End If

            If Request("ro") <> 0 Then
                Query &= " and u.roleID=" & Request("ro")
            End If

            If Request("ed") <> 0 Then
                Query &= " and u.educationID=" & Request("ed")
            End If

            If Request("age") <> 0 Then
                If Request("age") = 1 Then
                    Query &= " and datediff(year,birthday,getdate()) < 20 "
                End If
                If Request("age") = 2 Then
                    Query &= " and datediff(year,birthday,getdate()) >= 20 and datediff(year,birthday,getdate()) < 30"
                End If
                If Request("age") = 3 Then
                    Query &= " and datediff(year,birthday,getdate()) >= 30 and datediff(year,birthday,getdate()) < 40"
                End If
                If Request("age") = 4 Then
                    Query &= " and datediff(year,birthday,getdate()) >= 40 and datediff(year,birthday,getdate()) < 50"
                End If
                If Request("age") = 5 Then
                    Query &= " and datediff(year,birthday,getdate()) >= 50 and datediff(year,birthday,getdate()) < 60"
                End If
                If Request("age") = 6 Then
                    Query &= " and datediff(year,birthday,getdate()) >= 60"
                End If
            End If
            If Request("ct") = -1 Then
                Query &= " and u.contracttypeID is null "
            ElseIf Request("ct") = -2 Then
                Query &= " and u.contractTypeID is not null "
            ElseIf Request("ct") <> 0 Then
                Query &= " and u.contractTypeID=" & Request("ct")
            End If

            If Request("op") <> 0 Then
                Query &= " and u.occupationID=" & Request("op")
            End If

            If Request("sex") <> 0 Then
                Query &= " and u.sexID=" & Request("sex")
            End If

            If Request("t1") <> "" Then
                Query &= " and u.certificates like N'%" & HttpUtility.HtmlDecode(Request("t1")).Trim.ToLower() & "%'"
            End If

            If Request("name") <> "" Then
                Query &= " and replace(u.username,' ','') like N'%" & HttpUtility.HtmlDecode(Request("name")).Trim.ToLower() & "%'"
            End If

            If Request("pv") <> 0 Then
                Query &= " and u.provinceID=" & Request("pv")
            End If

            If Request("note") <> "" Then
                Query &= " and u.comments like N'%" & HttpUtility.HtmlDecode(Request("note")).Trim.ToLower() & "%'"
            End If

            If Request("fund") <> 0 Then
                Query &= " and u.workshopID=" & Request("fund")
            End If

            If Request("sc") = Nothing Then
                If Request("hF") <> Nothing Then
                    Query &= " and u.houseFund=1 "
                End If
                If Request("mF") <> Nothing Then
                    Query &= " and u.medicalFund=1 "
                End If
                If Request("uF") <> Nothing Then
                    Query &= " and u.unemployFund=1 "
                End If
                If Request("rF") <> Nothing Then
                    Query &= " and u.retiredFund=1 "
                End If
                If Request("sF") <> Nothing Then
                    Query &= " and u.sretiredFund=1 "
                End If
            Else
                Query &= " and u.houseFund=0 "
                Query &= " and u.medicalFund=0 "
                Query &= " and u.unemployFund=0 "
                Query &= " and u.retiredFund=0 "
                Query &= " and u.sretiredFund=0 "
            End If

            If Request("insurance") = -1 Then
                Query &= " and  u.insuranceTypeID is null "
            ElseIf Request("insurance") = -2 Then
                Query &= " and  u.insuranceTypeID is not null "
            ElseIf Request("insurance") <> 0 Then
                Query &= " and u.insuranceTypeID=" & Request("insurance")
            End If

            If (Request("lu") = 1 Or Request("lu") = 2) And Request("lu") <> Nothing Then
                Query &= " and u.isLabourUnion='" & Request("lu") & "' "
            End If

            If Request("id") <> "" Then
                Query &= " and u.userNo='" & Request("id") & "'"
            End If

            If Request("levfr") <> "" Then
                Try
                    levdatefr = CDate(Request("levfr"))
                    Query &= " AND u.leaveDate>='" & levdatefr & "'"
                Catch ex As Exception
                End Try
            End If

            If Request("levto") <> "" Then
                Try
                    levdateto = CDate(Request("levto"))
                    Query &= " AND u.leaveDate<'" & levdateto.AddDays(1) & "'"
                Catch ex As Exception
                End Try
            End If

            If Request("entfr") <> "" Then
                Try
                    entdatefr = CDate(Request("entfr"))
                    Query &= " AND u.enterDate>='" & entdatefr & "'"
                Catch ex As Exception
                End Try
            End If

            If Request("entto") <> "" Then
                Try
                    entdateto = CDate(Request("entto"))
                    Query &= " AND u.enterDate<'" & entdateto.AddDays(1) & "'"
                Catch ex As Exception
                End Try
            End If

            If Request("all") = Nothing Then
                If Request("lev") = "" Then
                    Query &= " AND u.leavedate is null "
                End If
            End If

            If Request("sWT") > 0 Then
                Query &= " AND u.specialWorkType=" & Request("sWT")
            End If

            If Request("ygxz") > 0 Then
                Query &= " AND u.employTypeID=" & Request("ygxz")
            End If
            If Request("jcfs") > 0 Then
                Query &= " AND u.workTypeID=" & Request("jcfs")
            End If

            If Request("aa") <> "" Then
                Query &= " and u.area like N'%" & HttpUtility.HtmlDecode(Request("aa")).Trim.ToLower() & "%'"
            End If

            If Request("bg") <> "" Then
                Query &= " and u.begood like N'%" & HttpUtility.HtmlDecode(Request("bg")).Trim.ToLower() & "%'"
            End If

            Query &= " ORDER BY u.userID "
            'Response.Write(Query)
            'Exit Sub
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)

            While (reader.Read())
                If Session("conceal") = 1 Then
                    If IsDBNull(reader(4)) = False Then
                        birth = Format(reader(4), "yyyy-MM-dd")
                    Else
                        birth = ""
                    End If
                    If IsDBNull(reader(18)) = False Then
                        enter = Format(reader(18), "yyyy-MM-dd")
                    Else
                        enter = ""
                    End If
                    If IsDBNull(reader(19)) = False Then
                        leave = Format(reader(19), "yyyy-MM-dd")
                    Else
                        leave = ""
                    End If
                    If IsDBNull(reader(28)) = False Then
                        employ = Format(reader(28), "yyyy-MM-dd")
                    Else
                        employ = ""
                    End If

                    Select Case reader("specialWorkType")
                        Case 0
                            specialWorkType = ""
                        Case 1
                            specialWorkType = "焊锡"
                        Case 2
                            specialWorkType = "打氯仿"
                            '    Case 3
                            '        coattype = "白大褂"

                    End Select
                    If IsDBNull(reader(42)) = False Then
                        cleave = Format(reader(42), "yyyy-MM-dd")
                    Else
                        cleave = ""
                    End If

                    If IsDBNull(reader(45)) = False Then
                        wldate = Format(reader(45), "yyyy-MM-dd")
                    Else
                        wldate = ""
                    End If

                    If IsDBNull(reader(48)) = False Then
                        contractDate = Format(reader(48), "yyyy-MM-dd")
                    Else
                        contractDate = ""
                    End If

                    PIMasteryRow(reader(0), reader(1), reader(2), reader(3), birth, reader(5), reader(6), reader(7), reader(8), _
                        reader(9), reader(10), reader(11), reader(12), reader(13), reader("exchangeDate"), reader(14), reader(15), reader(16), reader(17), _
                        enter, leave, reader(20), reader(21), reader(22), reader(23), reader(24), _
                        reader(25), reader(26), reader(27), employ, reader(29), reader(30), reader(31), specialWorkType, reader("workyear"), reader(32), reader(33), reader(34), reader(35), reader(36), reader(39), reader(40), reader(41), cleave, reader(43), reader(44), wldate, reader(46), reader(47), contractDate, reader(49), reader(50), reader(51), reader(52), _
                        reader("insurancePayDate"), reader("insuranceFinishDate"), reader("houseFundPayDate"), reader("houseFundFinishDate"), reader("registerType"), reader("politicalStatus"), reader("nation"))
                Else
                    If IsDBNull(reader(6)) = False Then
                        enter = Format(reader(6), "yyyy-MM-dd")
                    Else
                        enter = ""
                    End If
                    If IsDBNull(reader(7)) = False Then
                        leave = Format(reader(7), "yyyy-MM-dd")
                    Else
                        leave = ""
                    End If
                    PIMasteryRow(reader(0), reader(1), reader(2), reader(3), reader(4), reader(5), enter, leave, reader(8), _
                        reader(9), reader(10), reader(11), reader(12), reader(14), reader(14), reader("Fingerprint"), reader("postName"), reader("IDeffectivedate"), reader("uTypeName"), reader(19), reader(20), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                End If
                i = i + 1
            End While
            reader.Close()

            While (i < 40)
                PIMasteryRow("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                i = i + 1
            End While
            PIMasteryRow("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            PIMasteryRow("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            PIMasteryRow("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=personinfo.xls")
            Response.Flush()
        End Sub

        Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, _
                         ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, _
                         ByVal str10 As String, ByVal str11 As String, ByVal str12 As String, ByVal str13 As String, ByVal exchangeDate As String, ByVal str14 As String, _
                         ByVal str15 As String, ByVal str16 As String, ByVal str17 As String, ByVal str18 As String, ByVal str19 As String, _
                         ByVal str20 As String, ByVal str21 As String, ByVal str22 As String, ByVal str23 As String, ByVal str24 As String, _
                         ByVal str25 As String, ByVal str26 As String, ByVal str27 As String, ByVal str28 As String, ByVal str29 As String, _
                         ByVal str30 As String, ByVal str31 As String, ByVal str32 As String, ByVal str33 As String, ByVal str34 As String, _
                         ByVal str35 As String, ByVal str36 As String, ByVal str37 As String, ByVal str38 As String, ByVal str39 As String, _
                         ByVal str40 As String, ByVal str41 As String, ByVal str42 As String, ByVal str43 As String, ByVal str44 As String, _
                         ByVal str45 As String, ByVal str46 As String, ByVal str47 As String, ByVal str48 As String, ByVal str49 As String, _
                         ByVal str50 As String, ByVal str51 As String, ByVal str52 As String, ByVal insurancePayDate As String, ByVal insuranceFinishDate As String, _
                         ByVal houseFundPayDate As String, ByVal houseFundFinishDate As String, ByVal registerType As String, _
                         ByVal politicalStatus As String, ByVal nation As String)
            row = New TableRow
            If str0 = "<b>工号</b>" Then
                row.BackColor = Color.LightGray
            Else
                row.BackColor = Color.White
            End If
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)

            cell = New TableCell
            If (str0 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str0.Trim()
            End If
            cell.Width = New Unit(60)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str1 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str1.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str2 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str2.Trim()
            End If
            cell.Width = New Unit(160)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str3 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str3.Trim()
            End If
            cell.Width = New Unit(60)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str4 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str4.Trim()
            End If
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str5 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str5.Trim()
            End If
            cell.Width = New Unit(300)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str6 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str6.Trim()
            End If
            cell.Width = New Unit(300)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str7 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str7.Trim()
            End If
            cell.Width = New Unit(150)
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            row.Cells.Add(cell)

            'Added By Chenyb 添加户口类型，政治面貌，民族
            cell = New TableCell
            If (registerType = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = registerType.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (politicalStatus = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = politicalStatus.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (nation = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = nation.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)
            'End Added By Chenyb

            cell = New TableCell
            If (str8 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str8.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str9 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str9.Trim()
            End If
            cell.Width = New Unit(100)
            If Session("conceal") <> 1 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            row.Cells.Add(cell)

            cell = New TableCell
            If (str10 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str10.Trim()
            End If
            cell.Width = New Unit(100)

            row.Cells.Add(cell)

            cell = New TableCell
            If (str11 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str11.Trim()
            End If
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str12 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str12.Trim()
            End If
            cell.Width = New Unit(150)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str13 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str13.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (exchangeDate = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = exchangeDate.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str14 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str14.Trim()
            End If
            cell.Width = New Unit(160)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str15 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str15.Trim()
            End If
            cell.Width = New Unit(80)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str16 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str16.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str17 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str17.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str18 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str18.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str19 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str19.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str20 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str20.Trim()
            End If
            cell.Width = New Unit(500)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str21 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str21.Trim()
            End If
            cell.Width = New Unit(80)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str22 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str22.Trim()
            End If
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str23 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str23.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str24 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str24.Trim()
            End If
            cell.Width = New Unit(80)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str25 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str25.Trim()
            End If
            cell.Width = New Unit(60)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str26 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str26.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str27 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str27.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str28 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str28.Trim()
            End If
            cell.Width = New Unit(80)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str29 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str29.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If str30.Trim().ToLower() = "true" Then
                cell.Text = "是"
            ElseIf str30.Trim().ToLower() = "false" Then
                cell.Text = "否"
            Else
                cell.Text = str30.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str31 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str31.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            'Added By Chenyb    添加缴费年月、转出年月
            cell = New TableCell
            If (insurancePayDate = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = String.Format("{0:yyyy-MM}", insurancePayDate.Trim())
            End If
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (insuranceFinishDate = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = String.Format("{0:yyyy-MM}", insuranceFinishDate.Trim())
            End If
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            cell.Width = New Unit(100)
            row.Cells.Add(cell)
            'End Added By Chenyb

            cell = New TableCell
            If (str32 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str32.Trim()
            End If
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str33 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str33.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str34 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str34.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            'Added By Chenyb    添加公积金缴费年月、转出年月
            cell = New TableCell
            If (houseFundPayDate = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = String.Format("{0:yyyy-MM}", houseFundPayDate.Trim())
            End If
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            cell.Width = New Unit(120)
            row.Cells.Add(cell)

            cell = New TableCell
            If (houseFundFinishDate = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = String.Format("{0:yyyy-MM}", houseFundFinishDate.Trim())
            End If
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            cell.Width = New Unit(120)
            row.Cells.Add(cell)
            'End Added By Chenyb

            cell = New TableCell
            If (str35 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str35.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str36 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str36.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str37 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str37.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str38 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str38.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str39 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str39.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str40 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str40.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str41 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str41.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str42 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str42.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)


            cell = New TableCell
            If (str43 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str43.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str44 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str44.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str45 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str45.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str46 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str46.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str46 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str47.Trim()
            End If
            cell.Width = New Unit(100)
            row.Cells.Add(cell)


            cell = New TableCell
            cell.Text = str48.Trim()
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str49.Trim()
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str50.Trim()
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str51.Trim()
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = str52.Trim()
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            exl.Rows.Add(row)
        End Sub
    End Class

End Namespace
