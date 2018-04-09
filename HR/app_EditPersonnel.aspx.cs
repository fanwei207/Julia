//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class HR_app_EditPersonnel : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {

//    }
//}




Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class HR_app_EditPersonnel
        Inherits BasePage
        'Protected WithEvents ltlAlert As Literal
        Dim chk As New adamClass
        Protected WithEvents Comparevalidator33 As System.Web.UI.WebControls.CompareValidator
        Protected WithEvents Regularexpressionvalidator11 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents Regularexpressionvalidator21 As System.Web.UI.WebControls.RegularExpressionValidator
        'Protected WithEvents Regularexpressionvalidator4 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents Regularexpressionvalidator6 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents organization As System.Web.UI.WebControls.DropDownList
        Protected WithEvents Regularexpressionvalidator7 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents cMsg As System.Web.UI.WebControls.ValidationSummary
        Protected WithEvents rolename As System.Web.UI.WebControls.TextBox
        Protected WithEvents departmentName As System.Web.UI.WebControls.TextBox
        Protected WithEvents PersonnelType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cMsg3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
        Protected WithEvents IDNo As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Regularexpressionvalidator3 As System.Web.UI.WebControls.RegularExpressionValidator
        Dim Query As String
        Dim reader As SqlDataReader
        Dim item As ListItem
        Protected WithEvents uniformDate As System.Web.UI.WebControls.TextBox
        'Protected WithEvents comments1 As System.Web.UI.WebControls.TextBox
        'Protected WithEvents Wtype As System.Web.UI.WebControls.CheckBox

        'Shared treturn As Integer
        'Shared foundtype As String


        'Shared worktypechange As Integer


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub



        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("14020104", "修改和删除员工信息")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then
                'add by shanzm 2013-06-06

                'If Session("PlantCode").ToString() <> "2" And Session("PlantCode").ToString() <> "5" Then
                '    Button5.Visible = False
                'End If

                If Request("id") = "" Then
                    BtnSave.Visible = True
                    InitDropDownList()
                    isActive.Checked = True
                    treturn.Text = "0"

                    'kindswork.SelectedIndex = 0
                    'item = New ListItem("--")
                    'item.Value = 0
                    'kindswork.Items.Add(item)


                    workgroup.Items.Clear()

                    item = New ListItem("--")
                    item.Value = 0
                    workgroup.Items.Add(item)
                    workgroup.SelectedIndex = 0

                    kindswork.Items.Clear()
                    kindsworkDropDownList()
                    roleDropDownList()
                    photo.ImageUrl = "../images/default.gif"
                Else
                    LoginName.Enabled = False
                    BtnModify.Visible = True
                    InitDropDownList()
                    Dim i As Integer
                    Dim ds As DataSet
                    Query = " Select userid From tcpc0.dbo.Users_Photo where userid='" & Request("id") & "' "
                    ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
                    If ds.Tables(0).Rows.Count > 0 Then
                        photo.ImageUrl = "UsersShowPictures.aspx?uid=" & Request("id")
                        'photo.ImageUrl = "/document/viewdocument.aspx?imageID=21"
                    Else
                        photo.ImageUrl = "../images/default.gif"
                    End If

                    Query = " Select loginName, userPWD, enterDate, leaveDate, isnull(contractTypeID,0), isnull(roleID,0), isnull(departmentID,0), " _
                          & " isnull(phone,''), isnull(mobile,''), isnull(email,''), isactive, userName, birthday, isnull(IC,''), isnull(currentAddress,''), " _
                          & " isnull(currentZip,''), isnull(educationID,0), isnull(occupationID,0), isnull(certificates,''), isnull(comments,''), " _
                          & " isnull(sexID,0), isnull(fax,''), isnull(homeaddress,''), isnull(homezip,''), isnull(introducer,''), marriageID, " _
                          & " isnull(userNo,0), isnull(workshopID,0), isnull(workprocedureID,0), isnull(worktypeID,''), houseFund, medicalFund, " _
                          & " isnull(provinceID,0), employDate, unemployFund, retiredFund, isLabourUnion, employTypeID, insuranceTypeID, sretiredFund,contractDate,specialWorkType,healthCheckDate,isnull(area,'') as area,isnull(begood,'') as begood,isnull(kindswork,'') as kindswork,leaveBycp,labedate,fleave,unback,isnull(comp,'') as comp,wldate,userType,ISNULL(Fingerprint,'') As Fingerprint,IDeffectivedate,  " _
                          & " isnull(insurancePayDate, '') as insurancePayDate, isnull(insuranceFinishDate, '') as insuranceFinishDate, " _
                          & " isnull(houseFundPayDate, '') as houseFundPayDate, isnull(houseFundFinishDate, '') as houseFundFinishDate, " _
                          & " case isFarmRegister when 0 then '0'  when 1 then '1' else '--' end isFarmRegister, isnull(politicalStatus, N'--') as politicalStatus, isnull(nation, N'--') as nation,englishname " _
                          & " From tcpc0.dbo.users Where userID= '" & Request("id") & "' and organizationID= " & Session("orgID")
                    'Response.Write(Query)
                    'Exit Sub

                    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)

                    While (reader.Read())
                        LoginName.Text = reader(0)
                        userPWD.Text = reader(1)
                        If reader(2).ToString() = "" Then
                            enterdate.Text = ""
                        Else
                            enterdate.Text = Format(Convert.ToDateTime(reader(2)), "yyyy-MM-dd")
                        End If

                        If reader(3).ToString() = "" Then
                            leavedate.Text = ""
                        Else
                            leavedate.Text = Format(Convert.ToDateTime(reader(3)), "yyyy-MM-dd")
                        End If

                        For i = 1 To contract.Items.Count - 1
                            If reader(4).ToString() = "" Then
                                contract.SelectedIndex = 0
                                Exit For
                            Else
                                If contract.Items(i).Value = reader(4) Then
                                    contract.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next

                        Dim roleValue As Integer
                        If reader(5).ToString() = "" Then
                            role.Items.Clear()

                            item = New ListItem("--")
                            item.Value = 0
                            role.Items.Add(item)
                            role.SelectedIndex = 0
                        Else
                            roleValue = CInt(reader(5))
                            If 100 <= roleValue And roleValue < 300 Then
                                dropRoleType.SelectedIndex = 0
                            ElseIf 300 <= roleValue And roleValue < 500 Then
                                dropRoleType.SelectedIndex = 1
                            ElseIf 500 <= roleValue And roleValue < 1000 Then
                                dropRoleType.SelectedIndex = 2
                            ElseIf 1000 <= roleValue And roleValue < 5000 Then
                                dropRoleType.SelectedIndex = 3
                            End If

                            roleDropDownList()

                            For i = 1 To role.Items.Count - 1
                                If role.Items(i).Value = reader(5) Then
                                    role.SelectedIndex = i
                                End If
                            Next

                            roleProportion()
                        End If



                        For i = 1 To Department.Items.Count - 1
                            If reader(6).ToString() = "" Then
                                Department.SelectedIndex = 0
                                Exit For
                            Else
                                If Department.Items(i).Value = reader(6) Then
                                    Department.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next
                        phone.Text = reader(7)
                        Mobile.Text = reader(8)
                        Email.Text = reader(9)
                        If reader(10) = "0" Then
                            isActive.Checked = False
                        Else
                            isActive.Checked = True
                        End If
                        UserName.Text = reader(11)
                        txtEnglishName.Text = reader("englishname").ToString()
                        If reader(12).ToString() = "" Then
                            birthday.Text = ""
                        Else
                            birthday.Text = Format(Convert.ToDateTime(reader(12)), "yyyy-MM-dd")
                        End If
                        'contract------------------
                        If reader("contractDate").ToString() = "" Then
                            contractdate.Text = ""
                        Else
                            contractdate.Text = Format(Convert.ToDateTime(reader("contractDate")), "yyyy-MM-dd")
                        End If

                        icno.Text = reader(13)
                        currentAddress.Text = reader(14)
                        currentzip.Text = reader(15)
                        For i = 1 To education.Items.Count - 1
                            If reader(16).ToString() = "" Then
                                education.SelectedIndex = 0
                                Exit For
                            Else
                                If education.Items(i).Value = reader(16) Then
                                    education.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next
                        For i = 1 To occupation.Items.Count - 1
                            If reader(17).ToString() = "" Then
                                occupation.SelectedIndex = 0
                                Exit For
                            Else

                                If occupation.Items(i).Value = reader(17) Then
                                    occupation.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next
                        certificate.Text = reader(18)
                        comments.Text = reader(19)
                        For i = 1 To sex.Items.Count - 1
                            If reader(20).ToString() = "" Then
                                sex.SelectedIndex = 0
                                Exit For
                            Else
                                If sex.Items(i).Value = reader(20) Then
                                    sex.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next
                        fax.Text = reader(21)
                        homeaddress.Text = reader(22)
                        homezip.Text = reader(23)
                        introducer.Text = reader(24)
                        For i = 1 To marriage.Items.Count - 1
                            If reader(25).ToString() = "" Then
                                marriage.SelectedIndex = 0
                                Exit For
                            Else
                                If marriage.Items(i).Value = reader(25) Then
                                    marriage.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next
                        usercode.Text = reader(26)
                        workshopDropDownList()
                        For i = 1 To workshop.Items.Count - 1
                            If reader(27).ToString() = "" Then
                                workshop.SelectedIndex = 0
                                Exit For
                            Else
                                If workshop.Items(i).Value = reader(27) Then
                                    workshop.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next
                        workgroupDropDownList()
                        For i = 1 To workgroup.Items.Count - 1
                            If reader(28).ToString() = "" Then
                                workgroup.SelectedIndex = 0
                                Exit For
                            Else
                                If workgroup.Items(i).Value = reader(28) Then
                                    workgroup.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next
                        For i = 0 To worktype.Items.Count - 1
                            If reader(29).ToString() = "" Then
                                worktype.SelectedIndex = 0
                                worktypechange.Text = "0"
                                Exit For
                            Else
                                If worktype.Items(i).Value = reader(29) Then
                                    worktype.SelectedIndex = i
                                    worktypechange.Text = worktype.Items(i).Value.ToString
                                    Exit For
                                End If
                            End If
                        Next

                        If reader(30) = True Then
                            houseFund.Checked = True
                            foundtype.Text = "1"
                        Else
                            houseFund.Checked = False
                            foundtype.Text = "0"
                        End If
                        If reader(31) = True Then
                            medicalFund.Checked = True
                            foundtype.Text = foundtype.Text & "1"
                        Else
                            medicalFund.Checked = False
                            foundtype.Text = foundtype.Text & "0"
                        End If
                        If reader(34) = True Then
                            unemployFund.Checked = True
                            foundtype.Text = foundtype.Text & "1"
                        Else
                            unemployFund.Checked = False
                            foundtype.Text = foundtype.Text & "0"
                        End If
                        If reader(35) = True Then
                            retiredFund.Checked = True
                            foundtype.Text = foundtype.Text & "1"
                        Else
                            retiredFund.Checked = False
                            foundtype.Text = foundtype.Text & "0"
                        End If
                        If reader(39) = True Then
                            sretiredFund.Checked = True
                            foundtype.Text = foundtype.Text & "1"
                        Else
                            sretiredFund.Checked = False
                            foundtype.Text = foundtype.Text & "0"
                        End If

                        'Added By Chenyb
                        txbPayDate.Text = reader("insurancePayDate").ToString()
                        txbFinishDate.Text = reader("insuranceFinishDate").ToString()
                        txbHouseFundPayDate.Text = reader("houseFundPayDate").ToString()
                        txbHouseFundFinishDate.Text = reader("houseFundFinishDate").ToString()
                        dropRegister.SelectedValue = reader("isFarmRegister").ToString()
                        dropPoliticalStatus.SelectedItem.Text = reader("politicalStatus").ToString()
                        dropNation.SelectedItem.Text = reader("nation").ToString()
                        'End Added By Chenyb

                        For i = 1 To province.Items.Count - 1
                            If reader(32).ToString() = "" Then
                                province.SelectedIndex = 0
                                Exit For
                            Else
                                If province.Items(i).Value = reader(32) Then
                                    province.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next

                        If reader(33).ToString() = "" Then
                            employDate.Text = ""
                        Else
                            employDate.Text = Format(Convert.ToDateTime(reader(33)), "yyyy-MM-dd")
                        End If

                        If reader(36) = "0" Then
                            labourunion.Checked = False
                        Else
                            labourunion.Checked = True
                        End If

                        For i = 1 To employtype.Items.Count - 1
                            If reader(37).ToString() = "" Then
                                employtype.SelectedIndex = 0
                                Exit For
                            Else
                                If employtype.Items(i).Value = reader(37) Then
                                    employtype.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next

                        For i = 1 To insurance.Items.Count - 1
                            If reader(38).ToString() = "" Then
                                insurance.SelectedIndex = 0
                                Exit For
                            Else
                                If insurance.Items(i).Value = reader(38) Then
                                    insurance.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next


                        For i = 1 To userType.Items.Count - 1
                            If userType.Items(i).Value = reader("userType").ToString() Then
                                userType.SelectedIndex = i
                                Exit For
                            End If
                        Next


                        If reader("healthCheckDate").ToString() = "" Then
                            healthCheckDate.Text = ""
                            datetype.Text = ""
                        Else
                            healthCheckDate.Text = Format(Convert.ToDateTime(reader("healthCheckDate")), "yyyy-MM-dd")
                            datetype.Text = Format(Convert.ToDateTime(reader("healthCheckDate")), "yyyy-MM-dd")
                        End If
                        especialtype.SelectedIndex = reader("specialWorkType")
                        'Response.Write(reader("specialWorkType"))
                        'If reader("specialWorkType") = "true" Then
                        '    Wtype.Checked = True
                        kindtype.Text = reader("specialWorkType").ToString()

                        Pzone.Text = reader("area").ToString()
                        begood.Text = reader("begood").ToString()
                        'Else
                        '    kindtype.Text = "0"
                        'End If

                        kindsworkDropDownList()
                        For i = 1 To kindswork.Items.Count - 1
                            If reader("kindswork").ToString() = "" Then
                                kindswork.SelectedIndex = 0
                                Exit For
                            Else
                                If kindswork.Items(i).Value = reader("kindswork") Then
                                    kindswork.SelectedIndex = i
                                    Exit For
                                End If
                            End If
                        Next

                        If reader("labedate").ToString() = "" Then
                            labedate.Text = ""
                        Else
                            labedate.Text = Format(Convert.ToDateTime(reader("labedate")), "yyyy-MM-dd")
                        End If

                        If reader("leaveBycp") = True Then
                            Cleave.Checked = True
                        Else
                            Cleave.Checked = False
                        End If

                        If reader("fleave") = True Then
                            falelv.Checked = True
                        Else
                            falelv.Checked = False
                        End If

                        If reader("unback") = True Then
                            unback.Checked = True
                        Else
                            unback.Checked = False
                        End If

                        comp.Text = reader("comp").ToString()
                        If reader("wldate").ToString() = "" Then
                            wldate.Text = ""
                        Else
                            wldate.Text = Format(Convert.ToDateTime(reader("wldate")), "yyyy-MM-dd")
                        End If



                        txtFingerprint.Text = reader("Fingerprint").ToString()


                        If reader("IDeffectivedate").ToString() = "" Then
                            txtIDdate.Text = ""
                        Else
                            txtIDdate.Text = Format(Convert.ToDateTime(reader("IDeffectivedate")), "yyyy-MM-dd")
                        End If
                    End While
                    reader.Close()

                    Query = " SELECT TOP 1 ExchangeDate FROM User_Exchange_Department  WHERE userID='" & Request("id") & "' ORDER BY createdDate desc,modifiedDate desc "
                    Dim exchangeDate As String = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)
                    If exchangeDate <> Nothing Then
                        txb_exchangeDate.Text = Format(CDate(exchangeDate), "yyyy-MM-dd")
                    Else
                        txb_exchangeDate.Text = ""
                    End If

                    BtnDelete.Attributes.Add("onclick", "return confirm('确定要删除该职员吗?')")

                    treturn.Text = "-1"
                    If Request("flag") <> Nothing Then
                        treturn.Text = Request("flag")
                    End If
                End If
            End If
        End Sub
        Private Sub BtnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModify.Click
            If Not Me.Security("14020104").isValid Then
                Response.Redirect("/public/denied.htm", True)
            End If

            Dim fchange As String
            If LoginName.Text.Trim() = "admin" Then
                Exit Sub
            End If
            If txtEnglishName.Text.Trim().Length = 0 Then
                txtEnglishName.Text = ChineseToPinYin.ToPinYin(UserName.Text.Trim)
            ElseIf CheckStringChinese(txtEnglishName.Text.Trim()) Then
                ltlAlert.Text = "alert('英文名不能含有中文字符。');"
                Exit Sub
            End If
            If icno.Text.Length > 0 And Not CheckIdCard(icno.Text.Trim()) Then
                ltlAlert.Text = "alert('身份证号格式不正确!');"
                Exit Sub
            End If
            If comments.Text.Trim().Length > 400 Then
                ltlAlert.Text = "alert('备注不能多于400个字符。');"
                Exit Sub
            End If
            If Department.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('部门不能为空。');"
                Exit Sub
            End If
            If txb_exchangeDate.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('调入日期不能为空。');"
                Exit Sub
            End If
            If worktype.SelectedItem.Text = "计件" Then
                If workshop.SelectedIndex = 0 And workshop.Items.Count > 1 Then
                    ltlAlert.Text = "alert('计酬方式为计件的必须选择所在工段!');"
                    Exit Sub
                End If
            End If
            Dim isact As Integer
            If isActive.Checked = False Then
                isact = 0
            Else
                isact = 1
            End If
            Dim lu As Integer
            If labourunion.Checked = False Then
                lu = 0
            Else
                lu = 1
            End If

            If (userPWD.Text.Trim() = "") Then
                Query = " Update tcpc0.dbo.users Set userName= N'" & chk.sqlEncode(UserName.Text) & "', "
                Query = Query & "englishname=N'" & txtEnglishName.Text.Trim() & "',"
                If enterdate.Text.Trim <> "" Then
                    Query = Query & "enterdate = '" & enterdate.Text & "',"
                Else
                    Query = Query & "enterdate = null,"
                End If
                If leavedate.Text.Trim <> "" Then
                    Query = Query & "leavedate = '" & leavedate.Text & "',"
                Else
                    Query = Query & "leavedate = null,"
                End If
                If contract.SelectedValue <> 0 Then
                    Query = Query & "contractTypeID = '" & contract.SelectedValue & "',"
                    If (contract.SelectedValue <> 30) And (contract.SelectedValue <> 31) Then
                        If contractdate.Text.Trim <> "" Then
                            ltlAlert.Text = "alert('不是合同工或劳务派遣不能填入合同日期!');"
                            Exit Sub
                        End If
                        Query = Query & " contractDate = null,"
                        Query = Query & " wldate = null, "
                    Else
                        If contractdate.Text.Trim = "" Then
                            ltlAlert.Text = "alert('合同日期没有填入!');"
                            Exit Sub
                        End If
                        Query = Query & "contractDate = '" & contractdate.Text & "',"
                        'added by Baoxin 20080529
                        If contract.SelectedValue = 31 Then
                            If comp.Text.Length = 0 Then
                                ltlAlert.Text = "alert('劳务派遣必须填写公司!');"
                                Exit Sub
                            Else
                                If wldate.Text.Trim.Length = 0 Then
                                    ltlAlert.Text = "alert('选择劳务派遣必须填入劳派合同日期!');"
                                    Exit Sub
                                Else
                                    Query = Query & " wldate='" & wldate.Text.Trim & "', "
                                End If
                            End If
                        Else
                            Query = Query & " wldate = null, "
                        End If  'end add 20080529
                    End If

                Else
                    If contractdate.Text.Trim <> "" Then
                        ltlAlert.Text = "alert('没有选择合同类型不能填入合同日期!');"
                        Exit Sub
                    End If
                    Query = Query & "contractTypeID = null,"
                End If
                Query = Query & " comp=N'" & chk.sqlEncode(comp.Text.Trim) & "' , "
                If role.SelectedValue <> 0 Then
                    Query = Query & "roleID = '" & role.SelectedValue & "',"
                Else
                    Query = Query & "roleID = null,"
                End If
                If Department.SelectedValue <> 0 Then
                    Query = Query & "DepartmentID = '" & Department.SelectedValue & "',"
                Else
                    Query = Query & "DepartmentID = null,"
                End If
                Query = Query & "phone='" & phone.Text & "',"
                Query = Query & "mobile='" & Mobile.Text & "',"
                Query = Query & "Email = '" & Email.Text & "',"
                If birthday.Text.Trim <> "" Then
                    Query = Query & "birthday = '" & birthday.Text & "',"
                Else
                    Query = Query & "birthday = null,"
                End If

                Query = Query & "IC = '" & icno.Text & "',"
                If icno.Text.Trim().Length > 0 Then
                    If txtIDdate.Text.Trim().Length = 0 Then
                        ltlAlert.Text = "alert('必须填入身份证有效日期!');"
                        Exit Sub
                    End If
                    Query = Query & "IDeffectivedate = '" & txtIDdate.Text.Trim() & "', "
                End If

                Query = Query & "currentAddress = N'" & chk.sqlEncode(currentAddress.Text) & "',"
                Query = Query & "currentZip = '" & currentzip.Text & "',"
                If education.SelectedValue <> 0 Then
                    Query = Query & "educationID='" & education.SelectedValue & "',"
                Else
                    Query = Query & "educationID = null,"
                End If
                If occupation.SelectedValue <> 0 Then
                    Query = Query & "occupationID='" & occupation.SelectedValue & "',"
                Else
                    Query = Query & "occupationID = null,"
                End If
                Query = Query & "certificates = N'" & chk.sqlEncode(certificate.Text) & "',"
                Query = Query & "comments = N'" & chk.sqlEncode(comments.Text) & "',"
                Query = Query & "modifiedBy = '" & Session("uID") & "',"
                Query = Query & "modifiedDate =getdate(),"
                If sex.SelectedValue <> 0 Then
                    Query = Query & "sexID ='" & sex.SelectedValue & "',"
                Else
                    Query = Query & "sexID = null,"
                End If
                Query = Query & "isactive='" & isact & "',"
                Query = Query & "fax=N'" & fax.Text & "',"
                Query = Query & "homeAddress = N'" & chk.sqlEncode(homeaddress.Text) & "',"
                Query = Query & "homeZip = '" & homezip.Text & "',"
                Query = Query & "introducer = N'" & chk.sqlEncode(introducer.Text) & "',"
                If marriage.SelectedValue <> 0 Then
                    Query = Query & "marriageID ='" & marriage.SelectedValue & "',"
                Else
                    Query = Query & "marriageID = null,"
                End If
                If workshop.SelectedValue <> 0 Then
                    Query = Query & "workshopID ='" & workshop.SelectedValue & "',"
                Else
                    Query = Query & "workshopID = null,"
                End If
                If workgroup.SelectedValue <> 0 Then
                    Query = Query & "workprocedureID ='" & workgroup.SelectedValue & "',"
                Else
                    Query = Query & "workprocedureID = null,"
                End If
                If worktype.SelectedValue <> 0 Then
                    If CInt(worktypechange.Text) <> worktype.SelectedValue Then
                        If worktypedate.Text.Trim = "" Then
                            ltlAlert.Text = "alert('日期不能为空!');"
                            Exit Sub
                        End If
                    End If
                    Query = Query & "worktypeID ='" & worktype.SelectedValue & "',"
                Else
                    Query = Query & "worktypeID = null,"
                End If
                '-------------------------------------------
                If houseFund.Checked = True Then
                    Query = Query & "houseFund =1,"
                    fchange = "1"
                Else
                    Query = Query & "houseFund =0,"
                    fchange = "0"
                End If
                If medicalFund.Checked = True Then
                    Query = Query & "medicalFund =1,"
                    fchange = fchange & "1"
                Else
                    Query = Query & "medicalFund =0,"
                    fchange = fchange & "0"
                End If
                If unemployFund.Checked = True Then
                    Query = Query & "unemployFund =1,"
                    fchange = fchange & "1"
                Else
                    Query = Query & "unemployFund =0,"
                    fchange = fchange & "0"
                End If
                If retiredFund.Checked = True Then
                    Query = Query & "retiredFund =1,"
                    fchange = fchange & "1"
                Else
                    Query = Query & "retiredFund =0,"
                    fchange = fchange & "0"
                End If
                If sretiredFund.Checked = True Then
                    Query = Query & "sretiredFund =1,"
                    fchange = fchange & "1"
                Else
                    Query = Query & "sretiredFund =0,"
                    fchange = fchange & "0"
                End If

                '----------------------------------------------

                If province.SelectedValue <> 0 Then
                    Query = Query & "provinceId ='" & province.SelectedValue & "',"
                Else
                    Query = Query & "provinceId = null,"
                End If
                If insurance.SelectedValue <> 0 Then
                    Query = Query & "insuranceTypeID ='" & insurance.SelectedValue & "',"
                Else
                    Query = Query & "insuranceTypeID = null,"
                End If
                Query = Query & "isLabourUnion='" & lu & "',"
                If employtype.SelectedValue <> 0 Then
                    Query = Query & "employTypeID = '" & employtype.SelectedValue & "',"
                Else
                    Query = Query & "employTypeID = null,"
                End If

                'Added By Chenyb    添加保险的缴费日期、转出日期；户口类型、政治面貌、民族，公积金缴费日期、转出日期
                If (txbPayDate.Text.Trim() <> String.Empty) Then
                    If (Not IsDateTime(txbPayDate.Text.Trim()) And txbPayDate.Text.Trim().Length > 10) Then
                        ltlAlert.Text = "alert('缴费年月格式不对！');"
                        Exit Sub
                    Else
                        Query &= " insurancePayDate = '" & txbPayDate.Text.Trim() & "', "
                    End If
                End If
                If (txbFinishDate.Text.Trim() <> String.Empty) Then
                    If (Not IsDateTime(txbFinishDate.Text.Trim()) And txbFinishDate.Text.Trim().Length > 10) Then
                        ltlAlert.Text = "alert('转出年月格式不对！');"
                        Exit Sub
                    Else
                        Query &= " insuranceFinishDate = '" & txbFinishDate.Text.Trim() & "', "
                    End If
                End If

                If (txbHouseFundPayDate.Text.Trim() <> String.Empty) Then
                    If (Not IsDateTime(txbHouseFundPayDate.Text.Trim()) And txbHouseFundPayDate.Text.Trim().Length > 10) Then
                        ltlAlert.Text = "alert('缴费年月格式不对！');"
                        Exit Sub
                    Else
                        Query &= " houseFundPayDate = '" & txbHouseFundPayDate.Text.Trim() & "', "
                    End If
                End If
                If (txbHouseFundFinishDate.Text.Trim() <> String.Empty) Then
                    If (Not IsDateTime(txbHouseFundFinishDate.Text.Trim()) And txbHouseFundFinishDate.Text.Trim().Length > 10) Then
                        ltlAlert.Text = "alert('转出年月格式不对！');"
                        Exit Sub
                    Else
                        Query &= " houseFundFinishDate = '" & txbHouseFundFinishDate.Text.Trim() & "', "
                    End If
                End If

                Query &= " isFarmRegister=" & dropRegister.SelectedValue & ","
                Query &= " politicalStatus = N'" & dropPoliticalStatus.SelectedItem.Text & "',"
                Query &= " nation=N'" & dropNation.SelectedItem.Text & "',"
                'End Added By Chenyb

                If especialtype.SelectedIndex > 0 Then
                    Query &= " specialWorkType= '" & especialtype.SelectedIndex & "' , "
                    If healthCheckDate.Text.Trim() <> "" Then
                        Query &= " healthCheckDate='" & healthCheckDate.Text.Trim() & "', "
                    Else
                        ltlAlert.Text = "alert('体检日期不能为空!');"
                        Exit Sub
                    End If

                Else
                    Query &= " specialWorkType='0' , "
                    If healthCheckDate.Text.Trim() <> "" Then
                        ltlAlert.Text = "alert('没有选择特殊工种不能填入体检日期!');"
                        Exit Sub
                    Else
                        Query &= " healthCheckDate=null, "
                    End If
                End If

                If Pzone.Text.Trim <> "" Then
                    Query = Query & "area = N'" & chk.sqlEncode(Pzone.Text.Trim) & "',"
                Else
                    Query = Query & "area = null, "
                End If

                If begood.Text.Trim <> "" Then
                    Query = Query & "begood = N'" & chk.sqlEncode(begood.Text.Trim) & "',"
                Else
                    Query = Query & "begood = null, "
                End If

                If kindswork.SelectedValue <> 0 Then
                    Query = Query & "kindswork ='" & kindswork.SelectedValue & "', "
                Else
                    Query = Query & "kindswork = null, "
                End If

                If Cleave.Checked = True Then
                    Query &= " leaveBycp=1, "
                Else
                    Query &= " leaveBycp=0, "
                End If

                If falelv.Checked = True Then
                    Query &= " fleave=1, "
                Else
                    Query &= " fleave=0, "
                End If

                If unback.Checked = True Then
                    Query &= " unback=1, "
                Else
                    Query &= " unback=0, "
                End If

                If labedate.Text.Trim.Length > 0 Then
                    Query &= " labedate='" & labedate.Text.Trim & "', "
                Else
                    Query &= " labedate=null, "
                End If

                Query &= " userType = '" & userType.SelectedValue & "',"

                Dim strSql As String
                strSql = " SELECT userID FROM Users WHERE Fingerprint='" & txtFingerprint.Text.Trim() & "' And userID <>'" & Request("id") & "' and organizationID= " & Session("orgID")
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql) <> Nothing And txtFingerprint.Text.Trim().Length > 0 Then
                    ltlAlert.Text = "alert('考勤编码已存在!');"
                    Exit Sub
                End If

                Query &= " Fingerprint = '" & txtFingerprint.Text.Trim() & "',"

                If employDate.Text.Trim <> "" Then
                    Query = Query & "employDate = '" & employDate.Text & "' Where userID='" & Request("id") & "' and organizationID= " & Session("orgID")
                Else
                    Query = Query & "employDate = null Where userID='" & Request("id") & "' and organizationID= " & Session("orgID")
                End If

                '从这里开始是userPWD.Text.Trim() <> ""时的更新
            Else
                Query = " Update tcpc0.dbo.users Set userName= N'" & chk.sqlEncode(UserName.Text) & "', "
                Query = Query & "englishname=N'" & txtEnglishName.Text.Trim() & "',"
                If enterdate.Text.Trim <> "" Then
                    Query = Query & "enterdate = '" & enterdate.Text & "',"
                Else
                    Query = Query & "enterdate = null,"
                End If
                If leavedate.Text.Trim <> "" Then
                    Query = Query & "leavedate = '" & leavedate.Text & "',"
                Else
                    Query = Query & "leavedate = null,"
                End If
                If contract.SelectedValue <> 0 Then
                    Query = Query & "contractTypeID = '" & contract.SelectedValue & "',"
                    If (contract.SelectedValue <> 30) And (contract.SelectedValue <> 31) Then
                        If contractdate.Text.Trim <> "" Then
                            ltlAlert.Text = "alert('不是合同工或劳务派遣不能填入合同日期!');"
                            Exit Sub
                        End If
                        Query = Query & "contractDate = null,"
                        Query = Query & " wldate = null, "
                    Else
                        If contractdate.Text.Trim = "" Then
                            ltlAlert.Text = "alert('合同日期没有填入!');"
                            Exit Sub
                        End If
                        Query = Query & "contractDate = '" & contractdate.Text & "',"
                        'added by Baoxin 20080529
                        If contract.SelectedValue = 31 Then
                            If comp.Text.Length = 0 Then
                                ltlAlert.Text = "alert('劳务派遣必须填写公司!');"
                                Exit Sub
                            Else

                                If wldate.Text.Trim.Length = 0 Then
                                    ltlAlert.Text = "alert('选择劳务派遣必须填入劳派合同日期!');"
                                    Exit Sub
                                Else
                                    Query = Query & " wldate='" & wldate.Text.Trim & "', "
                                End If
                            End If
                        Else
                            Query = Query & " wldate = null, "
                        End If  'end add 20080529
                    End If
                Else
                    If contractdate.Text.Trim <> "" Then
                        ltlAlert.Text = "alert('没有选择合同类型不能填入合同日期!');"
                        Exit Sub
                    End If
                    Query = Query & "contractTypeID = null,"
                End If

                Query = Query & " comp=N'" & chk.sqlEncode(comp.Text.Trim) & "' , "
                If role.SelectedValue <> 0 Then
                    Query = Query & "roleID='" & role.SelectedValue & "',"
                Else
                    Query = Query & "roleID = null,"
                End If
                If Department.SelectedValue <> 0 Then
                    Query = Query & "DepartmentID = '" & Department.SelectedValue & "',"
                Else
                    Query = Query & "DepartmentID = null,"
                End If
                'Query = Query & "userPWD = '" & chk.encryptPWD(userPWD.Text.Trim()) & "',"
                Query = Query & "phone='" & phone.Text & "',"
                Query = Query & "mobile='" & Mobile.Text & "',"
                Query = Query & "Email = '" & Email.Text & "',"
                If birthday.Text.Trim <> "" Then
                    Query = Query & "birthday = '" & birthday.Text & "',"
                Else
                    Query = Query & "birthday = null,"
                End If
                Query = Query & "IC = '" & icno.Text & "',"
                If icno.Text.Trim().Length > 0 Then
                    If txtIDdate.Text.Trim().Length = 0 Then
                        ltlAlert.Text = "alert('必须填入身份证有效日期!');"
                        Exit Sub
                    End If
                    Query = Query & "IDeffectivedate = '" & txtIDdate.Text.Trim() & "', "
                End If

                Query = Query & "currentAddress = N'" & chk.sqlEncode(currentAddress.Text) & "',"
                Query = Query & "currentZip = '" & currentzip.Text & "',"
                If education.SelectedValue <> 0 Then
                    Query = Query & "educationID='" & education.SelectedValue & "',"
                Else
                    Query = Query & "educationID = null,"
                End If
                If occupation.SelectedValue <> 0 Then
                    Query = Query & "occupationID='" & occupation.SelectedValue & "',"
                Else
                    Query = Query & "occupationID = null,"
                End If
                Query = Query & "certificates = N'" & chk.sqlEncode(certificate.Text) & "',"
                Query = Query & "comments = N'" & chk.sqlEncode(comments.Text) & "',"
                Query = Query & "modifiedBy = '" & Session("uID") & "',"
                Query = Query & "modifiedDate =getdate(),"
                If sex.SelectedValue <> 0 Then
                    Query = Query & "sexID ='" & sex.SelectedValue & "',"
                Else
                    Query = Query & "sexID = null,"
                End If
                Query = Query & "isactive='" & isact & "',"
                Query = Query & "fax=N'" & fax.Text & "',"
                Query = Query & "homeAddress = N'" & chk.sqlEncode(homeaddress.Text) & "',"
                Query = Query & "homeZip = '" & homezip.Text & "',"
                Query = Query & "introducer = N'" & chk.sqlEncode(introducer.Text) & "',"
                If marriage.SelectedValue <> 0 Then
                    Query = Query & "marriageID ='" & marriage.SelectedValue & "',"
                Else
                    Query = Query & "marriageID = null,"
                End If
                If workshop.SelectedValue <> 0 Then
                    Query = Query & "workshopID ='" & workshop.SelectedValue & "',"
                Else
                    Query = Query & "workshopID = null,"
                End If
                If workgroup.SelectedValue <> 0 Then
                    Query = Query & "workprocedureID ='" & workgroup.SelectedValue & "',"
                Else
                    Query = Query & "workprocedureID = null,"
                End If
                If worktype.SelectedValue <> 0 Then
                    If CInt(worktypechange.Text) <> worktype.SelectedValue Then
                        If worktypedate.Text.Trim = "" Then
                            ltlAlert.Text = "alert('日期不能为空!');"
                            Exit Sub
                        End If
                    End If
                    Query = Query & "worktypeID ='" & worktype.SelectedValue & "',"
                Else
                    Query = Query & "worktypeID = null,"
                End If
                '----------------------------------------------
                If houseFund.Checked = True Then
                    Query = Query & "houseFund =1,"
                    fchange = "1"
                Else
                    Query = Query & "houseFund =0,"
                    fchange = "0"
                End If
                If medicalFund.Checked = True Then
                    Query = Query & "medicalFund =1,"
                    fchange = fchange & "1"
                Else
                    Query = Query & "medicalFund =0,"
                    fchange = fchange & "0"
                End If
                If unemployFund.Checked = True Then
                    Query = Query & "unemployFund =1,"
                    fchange = fchange & "1"
                Else
                    Query = Query & "unemployFund =0,"
                    fchange = fchange & "0"
                End If
                If retiredFund.Checked = True Then
                    Query = Query & "retiredFund =1,"
                    fchange = fchange & "1"
                Else
                    Query = Query & "retiredFund =0,"
                    fchange = fchange & "0"
                End If
                If sretiredFund.Checked = True Then
                    Query = Query & "sretiredFund =1,"
                    fchange = fchange & "1"
                Else
                    Query = Query & "sretiredFund =0,"
                    fchange = fchange & "0"
                End If
                '---------------------------------------------
                If province.SelectedValue <> 0 Then
                    Query = Query & "provinceId ='" & province.SelectedValue & "',"
                Else
                    Query = Query & "provinceId = null,"
                End If
                If insurance.SelectedValue <> 0 Then
                    Query = Query & "insuranceTypeID ='" & insurance.SelectedValue & "',"
                Else
                    Query = Query & "insuranceTypeID = null,"
                End If
                Query = Query & "isLabourUnion='" & lu & "',"
                If employtype.SelectedValue <> 0 Then
                    Query = Query & "employTypeID = '" & employtype.SelectedValue & "',"
                Else
                    Query = Query & "employTypeID = null,"
                End If

                'Added By Chenyb    添加保险的缴费日期、转出日期；户口类型、政治面貌、民族，公积金缴费日期、转出日期
                If (txbPayDate.Text.Trim() <> String.Empty) Then
                    If (Not IsDateTime(txbPayDate.Text.Trim()) And txbPayDate.Text.Trim().Length > 10) Then
                        ltlAlert.Text = "alert('缴费年月格式不对！');"
                        Exit Sub
                    Else
                        Query &= " insurancePayDate = '" & txbPayDate.Text.Trim() & "', "
                    End If
                End If
                If (txbFinishDate.Text.Trim() <> String.Empty) Then
                    If (Not IsDateTime(txbFinishDate.Text.Trim()) And txbFinishDate.Text.Trim().Length > 10) Then
                        ltlAlert.Text = "alert('转出年月格式不对！');"
                        Exit Sub
                    Else
                        Query &= " insuranceFinishDate = '" & txbFinishDate.Text.Trim() & "', "
                    End If
                End If

                If (txbHouseFundPayDate.Text.Trim() <> String.Empty) Then
                    If (Not IsDateTime(txbHouseFundPayDate.Text.Trim()) And txbHouseFundPayDate.Text.Trim().Length > 10) Then
                        ltlAlert.Text = "alert('缴费年月格式不对！');"
                        Exit Sub
                    Else
                        Query &= " houseFundPayDate = '" & txbHouseFundPayDate.Text.Trim() & "', "
                    End If
                End If
                If (txbHouseFundFinishDate.Text.Trim() <> String.Empty) Then
                    If (Not IsDateTime(txbHouseFundFinishDate.Text.Trim()) And txbHouseFundFinishDate.Text.Trim().Length > 10) Then
                        ltlAlert.Text = "alert('转出年月格式不对！');"
                        Exit Sub
                    Else
                        Query &= " houseFundFinishDate = '" & txbHouseFundFinishDate.Text.Trim() & "', "
                    End If
                End If

                Query &= " isFarmRegister=" & dropRegister.SelectedValue & ","
                Query &= " politicalStatus = N'" & dropPoliticalStatus.SelectedItem.Text & "',"
                Query &= " nation=N'" & dropNation.SelectedItem.Text & "',"
                'End Added By Chenyb

                If especialtype.SelectedIndex > 0 Then
                    Query &= " specialWorkType='" & especialtype.SelectedIndex & "' , "
                    If healthCheckDate.Text.Trim() <> "" Then
                        Query &= " healthCheckDate='" & healthCheckDate.Text.Trim() & "', "
                    Else
                        ltlAlert.Text = "alert('起始日期不能为空!');"
                        Exit Sub
                    End If

                Else
                    Query &= " specialWorkType='0' , "
                    If healthCheckDate.Text.Trim() <> "" Then
                        ltlAlert.Text = "alert('没有选择特殊工种不能填入起始日期!');"
                        Exit Sub
                    Else
                        Query &= " healthCheckDate=null, "
                    End If
                End If

                If employDate.Text.Trim <> "" Then
                    Query = Query & "employDate = '" & employDate.Text & "', "
                Else
                    Query = Query & "employDate = null, "
                End If

                If Pzone.Text.Trim <> "" Then
                    Query = Query & "area = N'" & chk.sqlEncode(Pzone.Text.Trim) & "' ,"
                Else
                    Query = Query & "area = null, "
                End If

                If begood.Text.Trim <> "" Then
                    Query = Query & "begood = N'" & chk.sqlEncode(begood.Text.Trim) & "', "
                Else
                    Query = Query & "begood = null, "
                End If

                If kindswork.SelectedValue <> 0 Then
                    Query = Query & "kindswork ='" & kindswork.SelectedValue & "', "
                Else
                    Query = Query & "kindswork = null, "
                End If

                If Cleave.Checked = True Then
                    Query &= " leaveBycp=1, "
                Else
                    Query &= " leaveBycp=0, "
                End If

                If falelv.Checked = True Then
                    Query &= " fleave=1, "
                Else
                    Query &= " fleave=0, "
                End If

                If unback.Checked = True Then
                    Query &= " unback=1, "
                Else
                    Query &= " unback=0, "
                End If

                If labedate.Text.Trim.Length > 0 Then
                    Query &= " labedate='" & labedate.Text.Trim & "', "
                Else
                    Query &= " labedate=null, "
                End If

                Query &= " userType = '" & userType.SelectedValue & "', "

                Dim strSql As String
                strSql = " SELECT userID FROM Users WHERE Fingerprint='" & txtFingerprint.Text.Trim() & "' And userID <>'" & Request("id") & "' and organizationID= " & Session("orgID")
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql) <> Nothing And txtFingerprint.Text.Trim().Length > 0 Then
                    ltlAlert.Text = "alert('考勤编码已存在!');"
                    Exit Sub
                End If

                Query &= " Fingerprint = '" & txtFingerprint.Text.Trim() & "' "

                Query &= "  Where userID='" & Request("id").ToString() & "' and organizationID= '" & Session("orgID") & "' "
            End If
            'Response.Write(Query)
            'Exit Sub
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            'Add by BaoXin 060620
            If worktype.SelectedValue <> 0 Then
                If CInt(worktypechange.Text) <> worktype.SelectedValue Then
                    Dim wid As Integer = 0
                    Query = " Select id From WorktypeChange where userID='" & Request("id").ToString() & "' and changedate='" & worktypedate.Text.Trim & "'"
                    wid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)
                    If wid = 0 Then
                        Query = " insert into WorktypeChange(userID,worktypeID,changedate,createby,createdate) values ('" & Request("id").ToString() & "','" & worktypechange.Text.ToString() & "','" & worktypedate.Text.Trim & "','" & Session("uID") & "',getdate())"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                    Else
                        Query = " update WorktypeChange set worktypeID='" & worktypechange.Text.ToString() & "' where id='" & wid.ToString() & "' "
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                    End If
                End If
            End If



            Query = "salary_createAccumulationFound"
            Dim params(1) As SqlParameter
            params(0) = New SqlParameter("@wdate", DateTime.Now.ToShortDateString())
            params(1) = New SqlParameter("@createdBy", Session("uID"))
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, Query, params)

            Dim id As Integer = 0
            Query = " Select isnull(id,0) from SalaryInfo where code='" & Request("id") & "'order by workdate desc"
            id = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)

            If fchange <> foundtype.Text Then

                Query = " Update SalaryInfo set "
                If houseFund.Checked = True Then
                    Query &= " houseFund=0,"
                Else
                    Query &= "houseFund = -1,"
                End If
                If medicalFund.Checked = True Then
                    Query &= " medicalFund=0,"
                Else
                    Query &= "medicalFund = -1,"
                End If
                If unemployFund.Checked = True Then
                    Query &= " unemployFund=0,"
                Else
                    Query &= "unemployFund = -1,"
                End If
                If retiredFund.Checked = True Then
                    Query &= " retiredFund=0,"
                Else
                    Query &= "retiredFund = -1,"
                End If
                If sretiredFund.Checked = True Then
                    Query &= " sretiredFund=0"
                Else
                    Query &= "sretiredFund = -1"
                End If
                'Query &= "where code='" & Request("id") & "' and year(workdate)='" & Year(DateTime.Now) & "' and month(workdate)='" & Month(DateTime.Now) & "'"
                Query &= "where id='" & id.ToString & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
            End If


            ' update User_Uniform and User_Health in condition
            Dim ds As DataSet


            If especialtype.SelectedIndex > 0 Then
                If kindtype.Text <> especialtype.SelectedIndex.ToString() Or datetype.Text <> healthCheckDate.Text Then
                    Query = " Select top 1 healthCheckDate From User_Health where  userID='" & Request("id") & "' order by healthCheckDate desc "
                    ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
                    If ds.Tables(0).Rows.Count <= 0 Then
                        Query = " Update User_Health set status='2' where userID='" & Request("id") & "' "
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

                        Query = " insert into User_Health (userID,healthCheckDate,status,createdBy,createdDate) values "
                        Query &= " ('" & Request("id") & "','" & healthCheckDate.Text & "','0','" & Session("uID") & "',getdate())"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                    ElseIf CDate(ds.Tables(0).Rows(0).Item(0)) < CDate(healthCheckDate.Text) Then
                        Query = " Update User_Health set status='2' where userID='" & Request("id") & "' "
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

                        Query = " insert into User_Health (userID,healthCheckDate,status,createdBy,createdDate) values "
                        Query &= " ('" & Request("id") & "','" & healthCheckDate.Text & "','0','" & Session("uID") & "',getdate())"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                    End If
                End If
            Else
                If kindtype.Text <> especialtype.SelectedIndex.ToString() Then
                    Query = " Update User_Health set status='2' where userID='" & Request("id") & "' "
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

                    'Query = " insert into User_Health (userNo,healthCheckDate,status,createdBy,createdDate) values "
                    'Query &= " (" & usercode.Text & ",'" & healthCheckDate.Text & "','0'," & Session("uID") & ",getdate())"
                    'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                End If
            End If

            Query = " SELECT TOP 1 ExchangeID,isnull(DepartmentID,0) as DepartmentID, isnull(WorkShopID,0) as WorkShopID, isnull(RoleID,0) as RoleID,ExchangeDate FROM User_Exchange_Department  WHERE userID='" & Request("id") & "' ORDER BY createdDate desc,modifiedDate desc "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
            If ds.Tables(0).Rows.Count > 0 Then
                If Department.SelectedValue <> ds.Tables(0).Rows(0).Item("DepartmentID") Or role.SelectedValue <> ds.Tables(0).Rows(0).Item("RoleID") Or workshop.SelectedValue <> ds.Tables(0).Rows(0).Item("WorkShopID") Then 'exchange the department
                    'insert into User_Exchange_Department
                    Query = " INSERT INTO User_Exchange_Department(userID,DepartmentID,RoleID,WorkShopID,ExchangeDate,CreatedBy,CreatedDate,oldDepartmentID,oldRoleID,oldWorkShopID)" _
                            & " VALUES('" & Request("id") & "','" & Department.SelectedValue & "','" & role.SelectedValue & "','" & workshop.SelectedValue & "','" & txb_exchangeDate.Text.Trim _
                            & "','" & Session("uID") & "','" & DateTime.Now & "','" & ds.Tables(0).Rows(0).Item("DepartmentID") & "','" & ds.Tables(0).Rows(0).Item("RoleID") & "','" _
                            & ds.Tables(0).Rows(0).Item("WorkShopID") & "')"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                Else
                    'update User_Exchange_Department
                    Query = " UPDATE User_Exchange_Department SET ExchangeDate='" & txb_exchangeDate.Text.Trim & "'," _
                            & " ModifiedBy='" & Session("uID") & "',ModifiedDate='" & DateTime.Now & "' WHERE ExchangeID='" & ds.Tables(0).Rows(0).Item("ExchangeID") & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                End If
            Else
                'insert into User_Exchange_Department
                Query = " INSERT INTO User_Exchange_Department(userID,DepartmentID,RoleID,WorkShopID,ExchangeDate,CreatedBy,CreatedDate)" _
                           & " VALUES('" & Request("id") & "','" & Department.SelectedValue & "','" & role.SelectedValue & "','" & workshop.SelectedValue & "','" & txb_exchangeDate.Text.Trim & "','" & Session("uID") & "','" & DateTime.Now & "')"
                '& " VALUES('" & Request("id") & "','" & Department.SelectedValue & "','" & txb_exchangeDate.Text.Trim & "','" & Session("uID") & "','" & DateTime.Now & "')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
            End If
            ds.Reset()

            Query = " Update Indicator set users=users+1 "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
            'Response.Redirect(chk.urlRand("/admin/personnellist1.aspx"))
        End Sub
        Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
            If Not Me.Security("14020104").isValid Then
                Response.Redirect("/public/denied.htm", True)
            End If

            If usercode.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('工号 不能为空。');"
                Exit Sub
            End If

            If UserName.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('姓名 不能为空。');"
                Exit Sub
            Else
                If Session("PlantCode") < 50 Then
                    If UserName.Text.Trim.Length > 10 Then
                        ltlAlert.Text = "alert('中国地区人员的姓名，请保持在10个字符以内！');"
                        Exit Sub
                    End If
                End If
            End If

            If txtEnglishName.Text.Trim.Length = 0 Then
                txtEnglishName.Text = ChineseToPinYin.ToPinYin(UserName.Text.Trim)
            ElseIf CheckStringChinese(txtEnglishName.Text.Trim()) Then
                ltlAlert.Text = "alert('英文名不能含有中文字符。');"
                Exit Sub
            End If

            If LoginName.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('用户名 不能为空。');"
                Exit Sub
            End If

            If usercode.Text.Trim.ToString() <> LoginName.Text.Trim.ToString() Then
                ltlAlert.Text = "alert('工号 与用户名 需相同');"
                Exit Sub
            End If

            If birthday.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('生日 不能为空。');"
                Exit Sub
            Else
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(birthday.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('生日 格式不正确。');"
                    Exit Sub
                End Try
            End If
            If icno.Text.Length > 0 And Not CheckIdCard(icno.Text.Trim()) Then
                ltlAlert.Text = "alert('身份证号格式不正确!');"
                Exit Sub
            End If
            If enterdate.Text.Trim.Length > 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(enterdate.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('进厂日期 格式不正确。');"
                    Exit Sub
                End Try
            End If

            If employDate.Text.Trim.Length > 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(employDate.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('转正日期 格式不正确。');"
                    Exit Sub
                End Try
            End If

            If leavedate.Text.Trim.Length > 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(leavedate.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('离开日期 格式不正确。');"
                    Exit Sub
                End Try
            End If

            If contractdate.Text.Trim.Length = 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(contractdate.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('合同日期 格式不正确。');"
                    Exit Sub
                End Try
            End If

            If worktype.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('计酬方式 不能为空。');"
                Exit Sub
            End If

            If worktypedate.Text.Trim.Length > 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(worktypedate.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('计酬日期 格式不正确。');"
                    Exit Sub
                End Try
            End If

            If employtype.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('请选择一项 用功性质。');"
                Exit Sub
            End If

            If healthCheckDate.Text.Trim.Length > 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(healthCheckDate.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('起始日期 格式不正确。');"
                    Exit Sub
                End Try
            End If

            Dim pattern As String = "([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+"
            Dim reg As New Regex(pattern, RegexOptions.IgnoreCase)
            Dim matches As MatchCollection

            If Email.Text.Trim.Length > 0 Then
                pattern = "([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(Email.Text.Trim)

                If matches.Count <= 0 Then

                    ltlAlert.Text = "alert('邮件 格式不正确。');"
                    Exit Sub
                End If
            End If

            If homezip.Text.Trim.Length > 0 Then
                pattern = "[0-9]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(homezip.Text.Trim)

                If matches.Count <= 0 Then

                    ltlAlert.Text = "alert('邮编 只能是数字。');"
                    Exit Sub
                End If
            End If

            If phone.Text.Trim.Length > 0 Then
                pattern = "[0-9()-]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(phone.Text.Trim)

                If matches.Count <= 0 Then

                    ltlAlert.Text = "alert('电话 只能是数字。');"
                    Exit Sub
                End If
            End If

            If Mobile.Text.Trim.Length > 0 Then
                pattern = "[0-9]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(Mobile.Text.Trim)

                If matches.Count <= 0 Then

                    ltlAlert.Text = "alert('手机号码 只能是数字。');"
                    Exit Sub
                End If
            End If

            If txtFingerprint.Text.Trim.Length > 0 Then
                pattern = "[0-9]+"
                reg = New Regex(pattern, RegexOptions.IgnoreCase)
                matches = reg.Matches(txtFingerprint.Text.Trim)

                If matches.Count <= 0 Then

                    ltlAlert.Text = "alert('考勤编号 只能是数字。');"
                    Exit Sub
                End If
            End If

            If role.SelectedIndex = 0 Then

                ltlAlert.Text = "alert('请选择一项 职务。');"
                Exit Sub
            End If

            If txb_exchangeDate.Text.Trim.Length > 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(txb_exchangeDate.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('调入日期 格式不正确。');"
                    Exit Sub
                End Try
            End If


            If labedate.Text.Trim.Length > 0 Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(labedate.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('入会日期 格式不正确。');"
                    Exit Sub
                End Try
            End If


            If comments.Text.Trim().Length > 400 Then
                ltlAlert.Text = "alert('备注不能多于400个字符。');"
                Exit Sub
            End If
            If Department.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('部门不能为空。');"
                Exit Sub
            End If
            If txb_exchangeDate.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('调入日期不能为空。');"
                Exit Sub
            End If
            If userPWD.Text.Trim().Length <= 0 Then
                'ltlAlert.Text = "alert('密码不能为空。');"
                'Exit Sub
                userPWD.Text = "123"
            End If
            If worktype.SelectedItem.Text = "计件" Then
                If workshop.SelectedIndex = 0 And workshop.Items.Count > 1 Then
                    ltlAlert.Text = "alert('计酬方式为计件的必须选择所在工段！');"
                    Exit Sub
                End If
            End If
            Dim isact As Integer
            If isActive.Checked = False Then
                isact = 0
            Else
                isact = 1
            End If

            Dim lu As Integer
            If labourunion.Checked = False Then
                lu = 0
            Else
                lu = 1
            End If

            Dim un As String
            un = LoginName.Text.Trim().ToUpper()
            'If (un.Length <= 0) Then
            '    'un = Convert.ToString(Session("uID")) & DateTime.Now.Hour.ToString() & DateTime.Now.Minute.ToString() & DateTime.Now.Second.ToString() 
            'End If

            Query = "select loginname from tcpc0.dbo.users where plantCode='" & Session("PlantCode") & "' and loginname='" & un & "' and deleted = 0 "
            Query &= "Union select loginname from tcpc0.dbo.UserApprove where plantCode='" & Session("PlantCode") & "' and loginname='" & un & "' and deleted = 0 "

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            If reader.Read() = True Then
                ltlAlert.Text = "alert('该用户名已存在，请选择另外的名字。');"
                Exit Sub
            End If
            reader.Close()

            Query = "select u.IC,u.userNo,p.description from tcpc0.dbo.users u INNER JOIN tcpc0..Plants p ON p.plantID = u.plantcode where u.IC='" & icno.Text.Trim & "' AND u.isActive=1 AND u.deleted=0 AND u.leavedate IS NULL "
            Dim ds As DataSet = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
            If ds.Tables(0).Rows.Count > 0 Then
                ltlAlert.Text = "alert('身份证号已存在，对应工号为" & ds.Tables(0).Rows(0).Item("userNo") & "/" & ds.Tables(0).Rows(0).Item(2).ToString() & "。');"
                Exit Sub
            End If
            ds.Reset()

            Query = " Insert Into tcpc0.dbo.users (loginName, userName, englishname, enterdate, leavedate, contractTypeID,comp,wldate,contractDate, roleID, " _
            & " DepartmentID, userPWD, phone, mobile, Email, birthday, IC, currentAddress, currentZip, " _
            & " educationID, occupationID, certificates, comments, modifiedBy, modifiedDate, organizationID, " _
            & " deleted, sexID, isactive, fax, homeaddress, homezip, introducer, marriageid, workshopID, " _
            & " workprocedureID, worktypeID, houseFund, medicalFund, provinceId, employDate, unemployFund, " _
            & " retiredFund, isLabourUnion, employTypeID, insuranceTypeID, sretiredFund,specialWorkType,healthCheckDate, " _
            & " userNo,isTemp,area,begood,kindswork,plantCode,leaveBycp,fleave,unback,labedate,userType,Fingerprint,IDeffectivedate,isInitPWD, " _
            & " insurancePayDate, insuranceFinishDate, houseFundPayDate,houseFundFinishDate,isFarmRegister, politicalStatus, nation)"
            Query = Query & " Values(N'" & chk.sqlEncode(un) & "', "
            Query = Query & " N'" & chk.sqlEncode(UserName.Text) & "', "
            Query = Query & " N'" & chk.sqlEncode(txtEnglishName.Text.Trim()) & "', "
            If enterdate.Text.Trim = "" Then
                ltlAlert.Text = "alert('进入日期不能为空。');"
                Exit Sub
                'Query = Query & "null,"
            Else
                Query = Query & " '" & enterdate.Text & "',"
            End If
            If leavedate.Text.Trim = "" Then
                Query = Query & "null,"
            Else
                Query = Query & " '" & leavedate.Text & "',"
            End If
            If contract.SelectedValue <> 0 Then
                Query = Query & " '" & contract.SelectedValue & "',"
                If (contract.SelectedValue <> 30) And (contract.SelectedValue <> 31) Then
                    If contractdate.Text.Trim <> "" Then
                        ltlAlert.Text = "alert('不是合同工或劳务派遣不能填入合同日期!');"
                        Exit Sub
                    End If
                    Query = Query & " null,null ,"
                Else
                    If contractdate.Text.Trim = "" Then
                        ltlAlert.Text = "alert('合同日期没有填入!');"
                        Exit Sub
                    End If
                    'added by Baoxin 20080529
                    If contract.SelectedValue = 31 Then
                        If comp.Text.Length = 0 Then
                            ltlAlert.Text = "alert('劳务派遣必须填写公司!');"
                            Exit Sub
                        Else
                            Query = Query & " N'" & chk.sqlEncode(comp.Text.Trim) & "' , "
                            If wldate.Text.Trim.Length = 0 Then
                                ltlAlert.Text = "alert('选择劳务派遣必须填入劳派合同日期!');"
                                Exit Sub
                            Else
                                Query = Query & " '" & wldate.Text.Trim & "', "
                            End If
                        End If
                    Else
                        Query = Query & " null,null ,"
                    End If  'end add 20080529
                End If
                Query = Query & " '" & contractdate.Text.Trim & "',"
            Else
                If contractdate.Text.Trim <> "" Then
                    ltlAlert.Text = "alert('没有选择合同类型不能填入合同日期!');"
                    Exit Sub
                End If
                Query = Query & "null,null,null,null,"
            End If
            If role.SelectedValue <> 0 Then
                Query = Query & " '" & role.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            If Department.SelectedValue <> 0 Then
                Query = Query & " '" & Department.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If

            'Added by Chenyb
            Dim Human As String
            Human = "HR_getUserNo"
            Dim xparams As SqlParameter
            'xparams(0) = New SqlParameter("@isWorker", isworker)
            xparams = New SqlParameter("@plantcode", Session("PlantCode"))
            Dim userNo As String = ""
            If usercode.Text.Length > 0 Then
                userNo = usercode.Text.Trim()
            Else
                userNo = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, Human, xparams)
            End If
            userPWD.Text = userNo & icno.Text.Trim.Substring(6, 8)
            'End added by Chenyb

            Query = Query & " '" & chk.encryptPWD(userPWD.Text.Trim()) & "',"
            Query = Query & " '" & phone.Text & "',"
            Query = Query & " '" & Mobile.Text & "',"
            Query = Query & " '" & Email.Text & "',"
            If birthday.Text.Trim = "" Then
                Query = Query & "null,"
            Else
                Query = Query & " '" & birthday.Text & "',"
            End If
            Query = Query & " '" & icno.Text & "',"
            Query = Query & " N'" & chk.sqlEncode(currentAddress.Text) & "',"
            Query = Query & " '" & currentzip.Text & "',"
            If education.SelectedValue <> 0 Then
                Query = Query & " '" & education.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            If occupation.SelectedValue <> 0 Then
                Query = Query & " '" & occupation.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            Query = Query & " N'" & chk.sqlEncode(certificate.Text) & "',"
            Query = Query & " N'" & chk.sqlEncode(comments.Text) & "',"
            Query = Query & " '" & Session("uID") & "',"
            Query = Query & " getdate(),"
            Query = Query & " '" & Session("orgID") & "',"
            Query = Query & " 0,"
            If sex.SelectedValue <> 0 Then
                Query = Query & " '" & sex.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            Query = Query & " '" & isact & "',"
            Query = Query & " N'" & fax.Text & "',"
            Query = Query & " N'" & chk.sqlEncode(homeaddress.Text) & "',"
            Query = Query & " '" & homezip.Text & "',"
            Query = Query & " N'" & chk.sqlEncode(introducer.Text) & "',"
            If marriage.SelectedValue <> 0 Then
                Query = Query & " '" & marriage.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            If workshop.SelectedValue <> 0 Then
                Query = Query & " '" & workshop.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            If workgroup.SelectedValue <> 0 Then
                Query = Query & " '" & workgroup.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            If worktype.SelectedValue <> 0 Then
                Query = Query & " '" & worktype.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If

            If houseFund.Checked = True Then
                Query = Query & "1,"
            Else
                Query = Query & "0,"
            End If
            If medicalFund.Checked = True Then
                Query = Query & "1,"
            Else
                Query = Query & "0,"
            End If

            If province.SelectedValue <> 0 Then
                Query = Query & " '" & province.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            If employDate.Text.Trim = "" Then
                Query = Query & "null,"
            Else
                Query = Query & " '" & employDate.Text & "',"
            End If

            If unemployFund.Checked = True Then
                Query = Query & "1,"
            Else
                Query = Query & "0,"
            End If
            If retiredFund.Checked = True Then
                Query = Query & "1,"
            Else
                Query = Query & "0,"
            End If


            Query = Query & " '" & lu & "',"
            If employtype.SelectedValue <> 0 Then
                Query = Query & " '" & employtype.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            If insurance.SelectedValue <> 0 Then
                Query = Query & " '" & insurance.SelectedValue & "',"
            Else
                Query = Query & "null,"
            End If
            'If fund.SelectedIndex = 4 Then
            '    Query = Query & "1 ,"
            'Else
            '    Query = Query & "0 ,"
            'End If
            If sretiredFund.Checked = True Then
                Query = Query & "1,"
            Else
                Query = Query & "0,"
            End If

            If especialtype.SelectedIndex > 0 Then
                Query &= " '" & especialtype.SelectedIndex & "' , "
                If healthCheckDate.Text.Trim() <> "" Then
                    Query &= " '" & healthCheckDate.Text.Trim() & "', "
                Else
                    ltlAlert.Text = "alert('起始日期不能为空!');"
                    Exit Sub
                End If
            Else
                Query &= " '0' , "
                If healthCheckDate.Text.Trim() <> "" Then
                    ltlAlert.Text = "alert('没有选择特殊工种不能填入起始日期!');"
                    Exit Sub
                Else
                    Query &= " null ,"
                End If
            End If

            'Query &= " SELECT MAX(userNo) FROM tcpc0.dbo.users "
            Dim isworker As String = "0"
            If worktype.SelectedItem.Text = "计件" Then
                isworker = "1"
            End If
            Query &= " N'" & userNo & "',1,N'" & chk.sqlEncode(Pzone.Text.Trim()) & "',N'" & chk.sqlEncode(begood.Text.Trim()) & "', "
            If kindswork.SelectedValue <> 0 Then
                Query = Query & " '" & kindswork.SelectedValue & "', "
            Else
                Query = Query & " null ,"
            End If
            Query &= " '" & Session("PlantCode") & "' ,"
            If Cleave.Checked = True Then
                Query &= " 1, "
            Else
                Query &= " 0, "
            End If

            If falelv.Checked = True Then
                Query &= " 1, "
            Else
                Query &= " 0, "
            End If

            If unback.Checked = True Then
                Query &= " 1, "
            Else
                Query &= " 0, "
            End If

            If labedate.Text.Trim.Length > 0 Then
                Query &= " '" & labedate.Text.Trim & "' ,"
            Else
                Query &= " null ,"
            End If

            Dim strSql As String
            strSql = " SELECT userID FROM Users WHERE Fingerprint='" & txtFingerprint.Text.Trim() & "'  and organizationID= " & Session("orgID")
            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql) <> Nothing Then
                ltlAlert.Text = "alert('考勤编码已存在!');"
                Exit Sub
            End If

            Query &= "  '" & userType.SelectedValue & "','" & txtFingerprint.Text.Trim() & "',"
            If icno.Text.Trim.Length > 0 Then
                If txtIDdate.Text.Trim.Length > 0 Then
                    Query &= "  '" & txtIDdate.Text.Trim() & "'"
                Else
                    ltlAlert.Text = "alert('必须填写身份证有效期!');"
                    Exit Sub
                End If
            Else
                Query &= " null"
            End If

            Query &= ",1,"

            'Added By Chenyb    添加保险的缴费日期、转出日期，公积金缴费日期、转出日期，户口类型、政治面貌、民族
            If (txbPayDate.Text.Trim() <> String.Empty) Then
                If (Not IsDateTime(txbPayDate.Text.Trim()) And txbPayDate.Text.Trim().Length > 10) Then
                    ltlAlert.Text = "alert('缴费年月格式不对！');"
                    Exit Sub
                Else
                    Query &= "  '" & txbPayDate.Text.Trim() & "', "
                End If
            Else
                Query &= " NULL ,"
            End If
            If (txbFinishDate.Text.Trim() <> String.Empty) Then
                If (Not IsDateTime(txbFinishDate.Text.Trim()) And txbFinishDate.Text.Trim().Length > 10) Then
                    ltlAlert.Text = "alert('转出年月格式不对！');"
                    Exit Sub
                Else
                    Query &= " '" & txbFinishDate.Text.Trim() & "', "
                End If
            Else
                Query &= " NULL ,"
            End If

            If (txbHouseFundPayDate.Text.Trim() <> String.Empty) Then
                If (Not IsDateTime(txbHouseFundPayDate.Text.Trim()) And txbHouseFundPayDate.Text.Trim().Length > 10) Then
                    ltlAlert.Text = "alert('缴费年月格式不对！');"
                    Exit Sub
                Else
                    Query &= " '" & txbHouseFundPayDate.Text.Trim() & "', "
                End If
            Else
                Query &= " NULL ,"
            End If
            If (txbHouseFundFinishDate.Text.Trim() <> String.Empty) Then
                If (Not IsDateTime(txbHouseFundFinishDate.Text.Trim()) And txbHouseFundFinishDate.Text.Trim().Length > 10) Then
                    ltlAlert.Text = "alert('转出年月格式不对！');"
                    Exit Sub
                Else
                    Query &= " '" & txbHouseFundFinishDate.Text.Trim() & "', "
                End If
            Else
                Query &= " NULL ,"
            End If

            If (dropRegister.SelectedValue <> "--") Then
                Query &= " " & dropRegister.SelectedValue & ","
            Else
                Query &= " NULL,"
            End If

            Query &= " N'" & dropPoliticalStatus.SelectedItem.Text & "',"
            Query &= " N'" & dropNation.SelectedItem.Text & "')"
            'End Added By Chenyb

            'Response.Write(Query)
            'Exit Sub
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            'insert into User_Exchange_Department
            Query = " INSERT INTO User_Exchange_Department(userID,DepartmentID,ExchangeDate,CreatedBy,CreatedDate)" _
                    & " (select userID,'" & Department.SelectedValue & "','" & txb_exchangeDate.Text.Trim & "','" & Session("uID") & "','" & DateTime.Now & "' From tcpc0.dbo.users where userNo=N'" & userNo & "')"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            If (LoginName.Text.Trim.Length <= 0) Then
                Dim id As String
                Query = " Select isnull(userID,0) from tcpc0.dbo.users where plantCode='" & Session("PlantCode") & "' and loginName=N'" & chk.sqlEncode(un) & "'"
                id = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)
                If id <> "0" Then
                    Query = " Update tcpc0.dbo.users set loginName='" & userNo.ToString & "' where plantCode='" & Session("PlantCode") & "' and loginName=N'" & chk.sqlEncode(un) & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)


                    Dim comparedate As String
                    Query = "salary_createAccumulationFound"
                    Dim params(1) As SqlParameter
                    params(0) = New SqlParameter("@wdate", CDate(enterdate.Text.Trim))
                    params(1) = New SqlParameter("@createdBy", Session("uID"))
                    comparedate = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, Query, params)

                    Query = " Insert Into SalaryInfo(code, name, salaryDepartmentID, houseFund, medicalFund, unemployFund, retiredFund, sretiredFund,  " _
                          & " organizationID, modifiedBy, modifiedDate,workdate) " _
                          & " values('" & id.ToString & "',N'" & chk.sqlEncode(UserName.Text) & "'," & Department.SelectedValue & ","
                    If houseFund.Checked = True Then
                        Query = Query & "0,"
                    Else
                        Query = Query & "-1,"
                    End If
                    If medicalFund.Checked = True Then
                        Query = Query & "0,"
                    Else
                        Query = Query & "-1,"
                    End If
                    If unemployFund.Checked = True Then
                        Query = Query & "0,"
                    Else
                        Query = Query & "-1,"
                    End If
                    If retiredFund.Checked = True Then
                        Query = Query & "0,"
                    Else
                        Query = Query & "-1,"
                    End If
                    If sretiredFund.Checked = True Then
                        Query = Query & "0,"
                    Else
                        Query = Query & "-1,"
                    End If

                    Query = Query & Session("orgID") & ",'" & Session("uID") & "',getdate(),'" & enterdate.Text & "')"
                    'Response.Write(Query)
                    'Exit Sub
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                    Dim l As Integer = 1
                    Dim tdate As DateTime = CDate(enterdate.Text)
                    While (DateDiff(DateInterval.Month, tdate, CDate(comparedate)) > 0)
                        tdate = tdate.AddMonths(1)
                        Query = " insert into SalaryInfo (code,name,salaryDepartmentID,houseFund,medicalFund,unemployFund,sretiredFund,retiredFund,modifiedBy,modifiedDate,organizationID,workdate)(select code,name,salaryDepartmentID,houseFund,medicalFund,unemployFund,sretiredFund,retiredFund,'" & Session("uID") & "',getdate(),organizationID,'" & tdate & "' from SalaryInfo where workdate='" & enterdate.Text & "')  "
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

                    End While

                    'If coattype.SelectedIndex <> 0 Then
                    '    Query = " insert into User_Uniform(userNo,uniform,uniformDate,createdBy,createdDate) Values "
                    '    Query &= " (" & id.ToString & ",'" & coattype.SelectedIndex & "','" & uniformDate.Text.Trim() & "'," & Session("uID") & ",getdate())"
                    '    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                    'End If

                    If especialtype.SelectedIndex > 0 Then
                        Query = " insert into User_Health (userID,healthCheckDate,status,createdBy,createdDate) values "
                        Query &= " ('" & id.ToString & "','" & healthCheckDate.Text & "','0','" & Session("uID") & "',getdate())"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                    End If
                End If

                Query = " Update Indicator set users=users+1 "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

                'Added by Chenyb 创建新员工之后，将新密码插入到密码使用记录中
                Dim newUserID As String
                Dim dsNewUser As DataSet
                newUserID = ""
                strSql = "select userID from tcpc0.dbo.Users where userNo = '" & userNo & "' and IC = '" & icno.Text & "' and organizationID = " & Session("orgID") & ""
                dsNewUser = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSql)
                With dsNewUser.Tables(0)
                    If (.Rows.Count > 0 And Not .Rows(0).IsNull(0)) Then
                        newUserID = .Rows(0)(0).ToString()
                    End If
                End With
                dsNewUser.Dispose()
                If (newUserID <> Nothing) Then
                    Dim param(3) As SqlParameter
                    param(0) = New SqlParameter("@uID", newUserID)
                    param(1) = New SqlParameter("@userPWD", chk.encryptPWD(userPWD.Text.Trim()))
                    param(2) = New SqlParameter("@createBy", Session("uID"))
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, "sp_hr_insertPasswordRecords", param)
                End If
                'End Added by Chenyb
                ltlAlert.Text = "alert('新员工创建完成！');window.location.href='addpersonnel.aspx';"
            End If
        End Sub

        Sub InitDropDownList()
            contract.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            contract.Items.Add(item)
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Contract Type' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                contract.Items.Add(item)
            End While
            reader.Close()
            contract.SelectedIndex = 0

            education.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            education.Items.Add(item)
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Education' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                education.Items.Add(item)
            End While
            reader.Close()
            education.SelectedIndex = 0

            occupation.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            occupation.Items.Add(item)
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Occupation' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                occupation.Items.Add(item)
            End While
            reader.Close()
            occupation.SelectedIndex = 0

            Department.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            Department.Items.Add(item)
            Query = "SELECT departmentID,Name From Departments WHERE isSalary='1' order by departmentID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                Department.Items.Add(item)
            End While
            reader.Close()
            Department.SelectedIndex = 0

            insurance.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            insurance.Items.Add(item)
            Query = " SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID " _
                  & " Where st.systemCodeTypeName='Insurance Type' order by s.systemCodeID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                insurance.Items.Add(item)
            End While
            reader.Close()
            insurance.SelectedIndex = 0

            sex.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            sex.Items.Add(item)
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Sex' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                sex.Items.Add(item)
            End While
            reader.Close()
            sex.SelectedIndex = 0

            worktype.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            worktype.Items.Add(item)
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Work Type' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                worktype.Items.Add(item)
            End While
            reader.Close()
            worktype.SelectedIndex = 0

            province.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            province.Items.Add(item)
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Province' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                province.Items.Add(item)
            End While
            reader.Close()
            province.SelectedIndex = 0

            marriage.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            marriage.Items.Add(item)
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='marriage' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                marriage.Items.Add(item)
            End While
            reader.Close()
            marriage.SelectedIndex = 0

            employtype.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            employtype.Items.Add(item)
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Employ Type' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                employtype.Items.Add(item)
            End While
            reader.Close()
            employtype.SelectedIndex = 0
            userType.Items.Clear()
            'item = New ListItem("--")
            'item.Value = 0
            'userType.Items.Add(item)
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Access Type' order by s.systemCodeID"
            'Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Access Type' and s.systemCodeName='A' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                userType.Items.Add(item)
            End While
            reader.Close()
            userType.SelectedIndex = 0

        End Sub
        Private Sub BtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            If treturn.Text = "0" Then
                Response.Redirect(chk.urlRand("/admin/contractInformation.aspx?sel=" & treturn.Text.ToString()))
            Else
                Response.Redirect(chk.urlRand("/admin/personnellist1.aspx"))
            End If

        End Sub

        Private Sub Department_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Department.SelectedIndexChanged
            If Department.SelectedValue <> 0 Then
                workshopDropDownList()
            Else
                workshop.Items.Clear()

                item = New ListItem("--")
                item.Value = 0
                workshop.Items.Add(item)
                workshop.SelectedIndex = 0
            End If
            workgroup.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
            workgroup.Items.Add(item)
            workgroup.SelectedIndex = 0
        End Sub

        Sub workshopDropDownList()
            Dim dst As DataSet
            workshop.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
            workshop.Items.Add(item)
            workshop.SelectedIndex = 0

            Query = " SELECT w.id, w.name FROM Workshop w INNER JOIN departments d ON w.departmentID = d.departmentID " _
                  & " WHERE d.name=N'" & Department.SelectedItem.Text.Trim() & "' AND w.workshopID IS NULL Order by w.code  "

            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        workshop.Items.Add(item)
                    Next
                End If
            End With
            dst.Reset()
        End Sub

        Private Sub workshop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles workshop.SelectedIndexChanged
            If workshop.SelectedValue <> 0 Then
                workgroupDropDownList()
            Else
                workgroup.Items.Clear()
                item = New ListItem("--")
                item.Value = 0
                workgroup.Items.Add(item)
                workgroup.SelectedIndex = 0
            End If
        End Sub

        Sub workgroupDropDownList()
            Dim dst As DataSet
            workgroup.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
            workgroup.Items.Add(item)
            workgroup.SelectedIndex = 0

            Query = " SELECT w.id, w.name FROM Workshop w " _
                  & " WHERE w.workshopID=" & workshop.SelectedValue & " Order by w.code  "

            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        workgroup.Items.Add(item)
                    Next
                End If
            End With
            dst.Reset()
        End Sub

        Function IsDateTime(ByVal str As String) As Boolean
            Try
                Dim dd As DateTime
                dd = Convert.ToDateTime(str)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        'add by 20061219
        Private Sub workgroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles workgroup.SelectedIndexChanged


            If workgroup.SelectedValue <> 0 Then
                kindsworkDropDownList()
            Else
                kindswork.Items.Clear()
                item = New ListItem("--")
                item.Value = 0
                kindswork.Items.Add(item)
                kindswork.SelectedIndex = 0
            End If
        End Sub
        Sub kindsworkDropDownList()
            kindswork.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
            kindswork.Items.Add(item)
            kindswork.SelectedIndex = 0

            Dim dst As DataSet
            Query = " SELECT id, name FROM workkinds " _
             & " WHERE workshopID=" & workgroup.SelectedValue & " Order by code  "
            '& "  Order by code  "
            Dim i As Integer = 0
            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
            With dst.Tables(0)
                If (.Rows.Count > 0) Then

                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))

                        kindswork.Items.Add(item)
                    Next
                End If
            End With
            dst.Reset()
        End Sub

        Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
            If Not Me.Security("14020104").isValid Then
                Response.Redirect("/public/denied.htm", True)
            End If

            Query = " UPDATE tcpc0.dbo.users SET deleted=1,modifiedBy='" & Session("uID") & "',modifiedDate=getdate() WHERE plantCode='" & Session("PlantCode") & "' and roleID>1 and userNo='" & usercode.Text.Trim() & "' AND organizationID='" & Session("orgID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            Query = " Update Indicator set users=users+1 "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            Response.Redirect(chk.urlRand("/admin/personnellist1.aspx"))
        End Sub

        Private Sub usercode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles usercode.TextChanged
            Dim uid As String
            Query = "select userID from tcpc0.dbo.users where plantCode='" & Session("PlantCode") & "' and userNo='" & usercode.Text.Trim & "' "
            If Session("conceal") = 0 Then
                Query &= " and isTemp='" & Session("temp") & "' "
            End If

            uid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)
            If Request("id") <> uid Then
                Try
                    Response.Redirect("addpersonnel.aspx?id=" & uid, True)
                Catch ex As Exception
                    Query = " select userNo From tcpc0.dbo.users where plantCode='" & Session("PlantCode") & "' and userID='" & Request("id") & "' "
                    If Session("conceal") = 0 Then
                        Query &= " and isTemp='" & Session("temp") & "' "
                    End If
                    usercode.Text = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)
                    Exit Sub
                End Try
            ElseIf uid = Nothing Then
                LoginName.Text = usercode.Text
            End If
        End Sub
        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            Query = " SELECT top 1 userID  FROM tcpc0.dbo.users " _
                 & " WHERE plantCode='" & Session("PlantCode") & "' and roleID>1 and deleted=0 and isactive=1 and leavedate is null and isTemp='" & Session("temp") & "' order by userID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            Dim url As String
            If (reader.Read()) Then
                url = reader(0).ToString()
            End If
            reader.Close()
            Response.Redirect("addpersonnel.aspx?id=" & url, True)
        End Sub
        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            If (usercode.Text.Trim().Length <= 0) Then
                Query = " SELECT top 1 userID  FROM tcpc0.dbo.users " _
                 & " WHERE plantCode='" & Session("PlantCode") & "' and roleID>1 and deleted=0 and isactive=1 and leavedate is null and isTemp='" & Session("temp") & "' order by userID"
            Else
                Query = " SELECT top 1 userID FROM tcpc0.dbo.users where userID<'" & Request("id") & "'  " _
                     & " and plantCode='" & Session("PlantCode") & "' and roleID>1 and deleted=0 and isactive=1 and leavedate is null and isTemp='" & Session("temp") & "' order by userID desc"
            End If
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            Dim url As String = ""
            If (reader.Read()) Then
                url = reader(0).ToString()
            End If
            reader.Close()
            If (url = Nothing) Then
                Query = " SELECT top 1 userID  FROM tcpc0.dbo.users " _
                             & " WHERE plantCode='" & Session("PlantCode") & "' and roleID>1 and deleted=0 and isactive=1 and leavedate is null and isTemp='" & Session("temp") & "' order by userID"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
                If (reader.Read()) Then
                    url = reader(0).ToString()
                End If
                reader.Close()
            End If
            Response.Redirect("addpersonnel.aspx?id=" & url, True)
        End Sub

        Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
            If (usercode.Text.Trim().Length <= 0) Then
                Query = " SELECT top 1 userID  FROM tcpc0.dbo.users " _
                         & " WHERE plantCode='" & Session("PlantCode") & "' and roleID>1 and deleted=0 and isactive=1 and leavedate is null and isTemp='" & Session("temp") & "' order by userID desc"
            Else
                Query = " SELECT top 1 userID  FROM tcpc0.dbo.users where userID>'" & Request("id") & "'  " _
                     & " and plantCode='" & Session("PlantCode") & "' and roleID>1 and deleted=0 and isactive=1 and leavedate is null and isTemp='" & Session("temp") & "' order by userID"
            End If
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            Dim url As String
            If (reader.Read()) Then
                url = reader(0).ToString()
            End If
            reader.Close()
            If (url = Nothing) Then
                Query = " SELECT top 1 userID  FROM tcpc0.dbo.users " _
                         & " WHERE plantCode='" & Session("PlantCode") & "' and roleID>1 and deleted=0 and isactive=1 and leavedate is null and isTemp='" & Session("temp") & "' order by userID desc"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
                If (reader.Read()) Then
                    url = reader(0).ToString()
                End If
                reader.Close()
            End If

            Response.Redirect("addpersonnel.aspx?id=" & url, True)
        End Sub

        Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
            Query = " SELECT top 1 userID  FROM tcpc0.dbo.users " _
                         & " WHERE plantCode='" & Session("PlantCode") & "' and roleID>1 and deleted=0 and isactive=1 and leavedate is null and isTemp='" & Session("temp") & "' order by userID desc"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            Dim url As String
            If (reader.Read()) Then
                url = reader(0).ToString()
            End If
            reader.Close()
            Response.Redirect("addpersonnel.aspx?id=" & url, True)
        End Sub

        Private Sub btn_exportMoveHist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_exportMoveHist.Click
            If usercode.Text.Trim.Length > 0 Then
                ltlAlert.Text = "window.open('/admin/redeployHistory.aspx?uid=" & Request("id") & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
            End If
        End Sub

        Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click

            Try
                Query = "sp_hr_SynUserCardNo"
                Dim params(2) As SqlParameter
                params(0) = New SqlParameter("@plantCode", Session("PlantCode").ToString())
                params(1) = New SqlParameter("@userNo", usercode.Text.Trim())
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, Query, params)
                ltlAlert.Text = "alert('IC卡号更新成功!');"
            Catch ex As Exception
                ltlAlert.Text = "alert('IC卡号更新失败!');"
            End Try

        End Sub

        Protected Sub dropRoleType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropRoleType.SelectedIndexChanged
            roleDropDownList()
        End Sub

        Sub roleDropDownList()
            Dim dst As DataSet
            role.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
            role.Items.Add(item)
            role.SelectedIndex = 0

            Dim strSQL As String
            Select Case dropRoleType.SelectedIndex
                Case 0
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " _
                       & " From Roles where roleID>=100 and roleID<300 " _
                       & " Order by roleID"
                Case 1
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " _
                       & " From Roles where roleID>=300 and roleID<500 " _
                       & " Order by roleID"
                Case 2
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " _
                       & " From Roles where roleID>=500 and roleID<1000 " _
                       & " Order by roleID"
                Case 3
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " _
                       & " From Roles where roleID>=1000 and roleID<5000 " _
                       & " Order by roleID"
            End Select

            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(2))
                        item.Value = Convert.ToInt32(.Rows(i).Item(1))
                        role.Items.Add(item)
                    Next
                End If
            End With
            dst.Reset()
        End Sub
        Sub roleProportion()
            Dim reader1 As SqlDataReader
            Dim strSQL As String

            strSQL = " Select roleProportion = Isnull(roleProportion, 0) from roles where roleID = " & role.SelectedItem.Value.ToString().Trim()

            reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            If reader1.Read() Then
                txtCoef.Text = reader1("roleProportion")
            End If
            reader1.Close()
        End Sub

        Protected Sub role_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles role.SelectedIndexChanged
            roleProportion()
        End Sub

        Protected Sub UserName_TextChanged(sender As Object, e As System.EventArgs) Handles UserName.TextChanged
            If txtEnglishName.Text.Trim().Length = 0 Then
                txtEnglishName.Text = ChineseToPinYin.ToPinYin(UserName.Text.Trim())
            End If
        End Sub

        Private Function CheckIdCard(idCard As String) As Boolean
            Dim isIDCard1 As String = "^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$"
            Dim isIDCard2 As String = "^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}(\d|X|x)$"

            If idCard.Length = 15 Then
                Dim reg As Regex = New Regex(isIDCard1)
                Return reg.IsMatch(idCard.ToUpper())
            ElseIf idCard.Length = 18 Then
                Dim reg As Regex = New Regex(isIDCard2)
                Return reg.IsMatch(idCard.ToUpper())
            End If
            Return False
        End Function

        Protected Sub icno_TextChanged(sender As Object, e As System.EventArgs) Handles icno.TextChanged
            If icno.Text.Length > 0 And Not CheckIdCard(icno.Text.Trim()) Then
                ltlAlert.Text = "alert('身份证号格式不正确!');"
            End If
        End Sub

        Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
            If usercode.Text.Trim <> "" Then
                ltlAlert.Text = "window.open('/admin/userPhotoUpload.aspx?uid=" & Request("id") & "','','toolbar=0,location=0,directories=0,status=0,menubar=0,resizable=1,scrollbars=1,width=400,height=200');"
            Else
                ltlAlert.Text = "alert('请创建完新员工后再上传照片!');"
                Exit Sub
            End If
        End Sub
    End Class

End Namespace
