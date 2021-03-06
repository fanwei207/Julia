Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Wage



Namespace tcpc

    Partial Class basicalsalary
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
        'Protected WithEvents ltlAlert As Literal

        Dim strSql As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass
        Dim ds As DataSet

        Protected WithEvents gsort As System.Web.UI.WebControls.Label
        Protected WithEvents customname As System.Web.UI.WebControls.Label
        Protected WithEvents salers As System.Web.UI.WebControls.Label
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents money As System.Web.UI.WebControls.Label
        Protected WithEvents Label3 As System.Web.UI.WebControls.Label
        Protected WithEvents P1 As System.Web.UI.WebControls.Panel
        Protected WithEvents A1 As System.Web.UI.HtmlControls.HtmlAnchor
        Protected WithEvents A2 As System.Web.UI.HtmlControls.HtmlAnchor
        Protected WithEvents A6 As System.Web.UI.HtmlControls.HtmlAnchor
        Protected WithEvents A9 As System.Web.UI.HtmlControls.HtmlAnchor

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then
                typevalue()
                departmentdropdown()
                roledropdown()
                kinddropdown()
                workshop.Items.Add(New ListItem("--", "0"))
                typeID.SelectedIndex = 0
                year.Text = DateTime.Now.Year
                month.SelectedValue = DateTime.Now.Month
                Uid.Text = ""
                strSql = "salary_createBasicSalary"
                Dim params(1) As SqlParameter
                params(0) = New SqlParameter("@wdate", DateTime.Now)
                params(1) = New SqlParameter("@createdBy", Session("uID"))
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSql, params)

                datebind(0)

            End If
        End Sub
        Sub kinddropdown()
            Dropdownlist1.Items.Clear()
            Dropdownlist1.Items.Add(New ListItem("--", "0"))
            strSql = " Select s.systemCodeID,s.systemCodeName From tcpc0.dbo.systemCode s INNER JOIN tcpc0.dbo.systemCodeType sc ON sc.systemCodeTypeID = s.systemCodeTypeID "
            strSql &= " Where (Substring(s.systemCodeName,1,2)=N'计时') and sc.systemCodeTypeName='Work Type'"
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

        Sub typevalue()
            typeID.Items.Clear()
            'typeID.Items.Add(New ListItem("--", "0"))
            Dim mast(10) As String
            Array.Clear(mast, 0, 11)
            mast(0) = "工价"
            mast(1) = "补贴"
            mast(2) = "固定工资"
            mast(3) = "额外补贴"
            mast(4) = "工会费"
            mast(5) = "加班工价"
            mast(6) = "绩效奖"
            mast(7) = "见习"
            mast(8) = "加班工资"
            mast(9) = "基本工资"
            mast(10) = "*考核奖*"  '// 把一部分人不包括在工资中另外的考核奖在这里维护

            Dim j As Integer
            For j = 2 To 7
                Dim tempListItem As New ListItem
                tempListItem.Value = j + 1
                tempListItem.Text = mast(j)
                typeID.Items.Add(tempListItem)
                j = j + 3
            Next
            'End While
            'reader.Close()
        End Sub

        Sub departmentdropdown()
            departmentdrop.Items.Clear()
            departmentdrop.Items.Add(New ListItem("--", "0"))
            strSql = " Select departmentID,name From departments Where isSalary='1' and active='1'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read()
                Dim tempListItem As New ListItem
                tempListItem.Value = reader(0)
                tempListItem.Text = reader(1)
                departmentdrop.Items.Add(tempListItem)
            End While
            reader.Close()
        End Sub

        Sub roledropdown()
            roledrop.Items.Clear()
            roledrop.Items.Add(New ListItem("--", "-1"))
            strSql = " Select roleID,roleName From Roles where roleID is not null order by roleID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read()
                Dim tempListItem As New ListItem
                tempListItem.Value = reader(0)
                tempListItem.Text = reader(1)
                roledrop.Items.Add(tempListItem)
            End While
            reader.Close()
        End Sub
        Sub workdropdown()
            workshop.Items.Clear()
            workshop.Items.Add(New ListItem("--", "0"))
            strSql = " Select w.id,w.name From Workshop w "
            If departmentdrop.SelectedIndex > 0 Then
                strSql &= " INNER JoiN departments d ON d.departmentID=w.departmentID"
            End If
            strSql &= " Where w.departmentID='" & departmentdrop.SelectedValue & "' and w.workshopID is null "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read()
                Dim tempListItem As New ListItem
                tempListItem.Value = reader(0)
                tempListItem.Text = reader(1)
                workshop.Items.Add(tempListItem)
            End While
            reader.Close()
        End Sub

        Sub workshopchange(ByVal s As Object, ByVal e As System.EventArgs)
            If departmentdrop.SelectedValue > 0 Then
                workdropdown()
            End If
        End Sub

        Sub typechange(ByVal s As Object, ByVal e As System.EventArgs)

            'If typeID.SelectedIndex = 5 Then
            '    workshop.Enabled = True
            '    workdropdown()
            '    roledrop.Enabled = False
            '    Dropdownlist1.Enabled = False
            'Else
            workshop.Enabled = True
            roledrop.Enabled = True
            Dropdownlist1.Enabled = True
            roledrop.SelectedIndex = 0
            Dropdownlist1.SelectedIndex = 0
            departmentdrop.SelectedIndex = 0
            workshop.SelectedIndex = 0
            txtComment.Text = ""
            txtComment.Enabled = False

            If typeID.SelectedValue = 11 Then
                txtComment.Enabled = True
            End If
            'End If
            DataGrid1.CurrentPageIndex = 0
            datebind(1)

        End Sub

        Sub namevalue_change(ByVal sender As Object, ByVal e As System.EventArgs)

            If textbox2.Text.Trim() <> "" Then
                Dim exitFlag As Boolean = False
                strSql = " SELECT userName,leaveDate,userID " _
                    & " FROM tcpc0.dbo.users " _
                    & " WHERE cast(userNo as varchar)='" & textbox2.Text.Trim() & "'"
                strSql &= "  and plantCode='" & Session("PlantCode") & "' "
                'strSql &= " and isTemp='" & Session("temp") & "' and plantCode='" & Session("PlantCode") & "' "
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
                While reader.Read
                    exitFlag = True
                    If reader("leaveDate").ToString() <> "" Then
                        If DateAdd(DateInterval.Month, 2, CDate(reader("leaveDate"))) < DateTime.Now Then
                            ltlAlert.Text = "alert('此员工已离职！');Form1.textbox2.focus();"
                            textbox2.Text = ""
                            name.Text = ""
                            Uid.Text = ""
                            Exit Sub
                        Else
                            ltlAlert.Text = "alert('此员工属于离职员工！');Form1.allowance.focus();"
                        End If
                    Else
                        ltlAlert.Text = "Form1.allowance.focus();"
                    End If
                    name.Text = reader(0)
                    Uid.Text = reader(2)
                    workNO.Text = textbox2.Text.Trim()
                    Me.search(Me, New EventArgs())
                    If ds.Tables(0).Rows.Count > 0 Then
                        save.Attributes.Add("onclick", "return confirm('已存在相同记录，是否覆盖？')")
                    Else
                        save.Attributes.Remove("onclick")
                    End If
                End While
                reader.Close()
                If exitFlag = False Then
                    ltlAlert.Text = "alert('工号不存在！');Form1.textbox2.focus();"
                    textbox2.Text = ""
                    name.Text = ""
                    Uid.Text = ""
                End If
            End If
        End Sub

        Sub BtnSave_click(ByVal sender As Object, ByVal e As System.EventArgs)
            'Dim codeID As String
            Dim flag As Boolean = False
            Dim judge As String
            Dim ok As Integer = 0
            'Save tcpc0.dbo.users code  ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            If textbox2.Text.Trim() = "" Then
                ltlAlert.Text = "alert('工号不能为空！');Form1.allowance.focus();"
                Exit Sub
            End If

            If allowance.Text.Trim() = "" Then
                ltlAlert.Text = "alert('金额不能为空！');Form1.allowance.focus();"
                Exit Sub
            End If

            Dim hr_salary As HR = New HR()
            Dim intadjust As Integer = hr_salary.finAdjust(Convert.ToInt32(year.Text), Convert.ToInt32(month.SelectedValue), Convert.ToInt32(Session("PlantCode")), 1)
            If intadjust < 0 Then
                Dim Str As String = "<script language='javascript'> alert('工资已被财务冻结，不能操作!'); </script>"
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Finadjust", Str)
                Exit Sub
            End If
                

            'Get UserID-------
            If textbox2.Text.Trim() <> "" Then
                If Uid.Text <> "" Then
                    ok = 1
                    Dim membership As String
                    Dim userType As String
                    strSql = " Select u.isLabourUnion,isnull(s.systemCodeName,'') From tcpc0.dbo.users u INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID= u.workTypeID "
                    'strSql &= " INNER JOIN tcpc0.dbo.systemCodeType st ON st.systemCodeTypeID = s.systemCodeTypeID and st.SystemCodeTypeName ='Work Type' "
                    strSql &= " Where cast(u.userID as varchar)= '" & Uid.Text.Trim() & "' "
                    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
                    While reader.Read
                        membership = reader(0)
                        userType = reader(1)
                    End While
                    reader.Close()

                    strSql = " Select id From BasicSalary Where cast(userID as varchar)= '" & Uid.Text.Trim() & "' and year(workdate)='" & year.Text.Trim() & "' and month(workdate)='" & month.SelectedValue & "' "
                    judge = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                    If judge Is Nothing Then
                        Select Case typeID.SelectedValue
                            Case 1
                                If userType <> "计件" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,workprice,creatby,creatdate,organizationID,workdate ) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 2
                                strSql = " INSERT INTO BasicSalary ( userID,subsidy,creatby,creatdate,organizationID ,workdate) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                            Case 3
                                If userType <> "计件" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,fixedsalary,creatby,creatdate,organizationID,workdate ) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 4
                                strSql = " INSERT INTO BasicSalary ( userID,extrasubsidy,creatby,creatdate,organizationID,workdate ) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                            Case 5
                                If membership = "True" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,memberShipPay,creatby,creatdate,organizationID,workdate ) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                Else
                                    ltlAlert.Text = "alert('此工号不是会员！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 6
                                strSql = "INSERT INTO BasicSalary ( userID,overprice,creatby,creatdate,organizationID ) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "')"
                            Case 7
                                If userType <> "计件" Then
                                    strSql = "INSERT INTO BasicSalary ( userID,SalaryAdjust,creatby,creatdate,organizationID,workdate ) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 8   'Add by baoxin 080112 for learn jobers
                                If userType = "计时固定" Then
                                    strSql = "INSERT INTO BasicSalary ( userID,tempjobs,creatby,creatdate,organizationID ) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "')"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时固定！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 9 'add by Baoxin 20080912 for improve salary
                                If userType <> "计件" Then
                                    strSql = "INSERT INTO BasicSalary ( userID,overbenefit,creatby,creatdate,organizationID ) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "')"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If

                            Case 10 'add by Baoxin 20090210 for holiday pay
                                strSql = "INSERT INTO BasicSalary ( userID,holidaypay,creatby,creatdate,organizationID) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "')"
                            Case 11 'add by Baoxin 20090415 for access pay
                                If userType <> "计件" Then
                                    strSql = "INSERT INTO BasicSalary ( userID,accesspay,creatby,creatdate,organizationID,comment  ) VALUES ('" & Uid.Text.Trim() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "',N'" & chk.sqlEncode(txtComment.Text.Trim) & "')"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If

                        End Select
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                    Else
                        Select Case typeID.SelectedValue
                            Case 1
                                If userType <> "计件" Then
                                    strSql = " Update BasicSalary set workprice= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 2
                                strSql = " Update BasicSalary set subsidy= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                            Case 3
                                If userType <> "计件" Then
                                    strSql = " Update BasicSalary set fixedsalary= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 4
                                strSql = " Update BasicSalary set extrasubsidy= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                            Case 5
                                If membership = "True" Then
                                    strSql = " Update BasicSalary set memberShipPay= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                                Else
                                    ltlAlert.Text = "alert('此工号不是会员！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 6
                                strSql = " Update BasicSalary set overprice= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                            Case 7
                                If userType <> "计件" Then
                                    strSql = " Update BasicSalary set SalaryAdjust= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 8  'Add by baoxin 080112 for learn jobers
                                If userType = "计时固定" Then
                                    strSql = " Update BasicSalary set tempjobs= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时固定！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                            Case 9 'add by Baoxin 20080912 for improve salary
                                If userType <> "计件" Then
                                    strSql = " Update BasicSalary set overbenefit= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If

                            Case 10 'add by Baoxin 20090210 for holiday pay
                                strSql = " Update BasicSalary set holidaypay= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "'"
                            Case 11 'add by Baoxin 20090415 for access pay
                                If userType <> "计件" Then
                                    strSql = " Update BasicSalary set accesspay= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "',comment=N'" & chk.sqlEncode(txtComment.Text.Trim) & "' Where id= '" & judge & "'"
                                Else
                                    ltlAlert.Text = "alert('此工号不是计时工！');Form1.textbox2.focus();"
                                    Exit Sub
                                End If
                        End Select
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                    End If
                End If
            Else

                Dim have As Boolean = False
                strSql = " Select u.userID,u.isLabourUnion,isnull(s.systemCodeName,'') From tcpc0.dbo.users u INNER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID= u.workTypeID"

                If departmentdrop.SelectedValue <> 0 Then
                    strSql &= " where u.departmentID=" & departmentdrop.SelectedValue
                    have = True
                End If
                If roledrop.SelectedValue <> -1 Then
                    If have = True Then
                        strSql &= " and u.roleID=" & roledrop.SelectedValue
                    Else
                        strSql &= " where u.roleID=" & roledrop.SelectedValue
                        have = True
                    End If
                End If
                If Dropdownlist1.SelectedValue <> 0 Then
                    If have = True Then
                        strSql &= " and u.workTypeID=" & Dropdownlist1.SelectedValue
                    Else
                        strSql &= " where u.workTypeID=" & Dropdownlist1.SelectedValue
                    End If
                End If
                If workshop.SelectedValue <> 0 Then
                    If have = True Then
                        strSql &= " and u.workshopID=" & workshop.SelectedValue
                    Else
                        strSql &= " where u.workshopID=" & workshop.SelectedValue
                    End If
                End If
                strSql &= " and u.leaveDate is null "
                strSql &= "  and u.plantCode='" & Session("PlantCode") & "' "
                'strSql &= " and u.isTemp='" & Session("temp") & "' and u.plantCode='" & Session("PlantCode") & "' "
                strSql &= " order by u.userID "
                'Response.Write(strSql)
                'Exit Sub
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
                While reader.Read
                    ok = 1
                    strSql = " select id From BasicSalary Where cast(userID as varchar)= '" & reader(0).ToString() & "' and year(workdate)='" & year.Text.Trim() & "' and month(workdate)='" & month.SelectedValue & "' "
                    judge = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                    If judge Is Nothing Then
                        Select Case typeID.SelectedValue
                            Case 1
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,workprice,creatby,creatdate,organizationID,workdate ) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                End If
                            Case 2
                                strSql = " INSERT INTO BasicSalary ( userID,subsidy,creatby,creatdate,organizationID,workdate ) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                            Case 3
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,fixedsalary,creatby,creatdate,organizationID,workdate ) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                End If
                            Case 4
                                strSql = " INSERT INTO BasicSalary ( userID,extrasubsidy,creatby,creatdate,organizationID,workdate ) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                            Case 5

                                If reader(1).ToString() = "True" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,memberShipPay,creatby,creatdate,organizationID ,workdate ) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                    'Else
                                    '    ltlAlert.Text = "alert('此工号不是会员！');Form1.textbox2.allowance.focus();"
                                    '    Exit Sub
                                End If
                            Case 6
                                strSql = "INSERT INTO BasicSalary ( userID,overprice,creatby,creatdate,organizationID ,workdate) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                            Case 7
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,SalaryAdjust,creatby,creatdate,organizationID,workdate ) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                End If
                            Case 8   'Add by baoxin 080112 for learn jobers
                                If reader(2).ToString() = "计时固定" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,tempjobs,creatby,creatdate,organizationID,workdate ) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                End If
                            Case 9 'add by Baoxin 20080912 for improve salary
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,overbenefit,creatby,creatdate,organizationID,workdate ) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                                End If
                            Case 10 'add by Baoxin 20090210 for holiday pay
                                strSql = " INSERT INTO BasicSalary ( userID,holidaypay,creatby,creatdate,organizationID,workdate ) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01')"
                            Case 11 'add by Baoxin 20090415 for access pay
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " INSERT INTO BasicSalary ( userID,accesspay,creatby,creatdate,organizationID,workdate ,comment) VALUES ('" & reader(0).ToString() & "','" & allowance.Text.Trim() & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','" & year.Text.Trim() & "-" & month.SelectedValue & "-01',N'" & chk.sqlEncode(txtComment.Text.Trim) & "')"
                                End If
                        End Select
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                    Else
                        Select Case typeID.SelectedValue
                            Case 1
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " Update BasicSalary set workprice= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                                End If
                            Case 2
                                strSql = " Update BasicSalary set subsidy= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                            Case 3
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " Update BasicSalary set fixedsalary= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                                End If
                            Case 4
                                strSql = " Update BasicSalary set extrasubsidy= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                            Case 5

                                If reader(1).ToString() = "True" Then
                                    strSql = " Update BasicSalary set memberShipPay= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                                    'Else
                                    '    ltlAlert.Text = "alert('此工号不是会员！');Form1.textbox2.focus();"
                                    '    Exit Sub
                                End If
                            Case 6
                                strSql = " Update BasicSalary set overprice= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                            Case 7
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " Update BasicSalary set SalaryAdjust= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                                End If
                            Case 8   'Add by baoxin 080112 for learn jobers
                                If reader(2).ToString() = "计时固定" Then
                                    strSql = " Update BasicSalary set tempjobs= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                                End If
                            Case 9 'add by Baoxin 20080912 for improve salary
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " Update BasicSalary set overbenefit= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                                End If

                            Case 10 'add by Baoxin 20090210 for holiday pay
                                strSql = " Update BasicSalary set holidaypay= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "' Where id= '" & judge & "' "
                            Case 11 'add by Baoxin 20090415 for access pay
                                If reader(2).ToString() <> "计件" Then
                                    strSql = " Update BasicSalary set accesspay= '" & allowance.Text.Trim() & "',creatby ='" & Session("uID") & "',creatdate='" & DateTime.Now.ToString() & "',organizationID='" & Session("orgID") & "',comment=N'" & chk.sqlEncode(txtComment.Text.Trim) & "' Where id= '" & judge & "' "
                                End If
                        End Select
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                    End If
                End While
                reader.Close()
            End If
            If ok = 1 Then
                ltlAlert.Text = "alert('保存成功！');Form1.textbox2.focus();"
            Else
                ltlAlert.Text = "alert('没有符合条件的员工！');Form1.textbox2.focus();"
            End If
            departmentdrop.SelectedIndex = 0
            roledrop.SelectedIndex = 0
            Dropdownlist1.SelectedIndex = 0
            Uid.Text = ""
            textbox2.Text = ""
            name.Text = ""
            allowance.Text = ""
            datebind(0)


        End Sub
        Function createSQL(ByVal temp As Integer) As String
            Dim xdate As String = year.Text.Trim() & "-" & month.SelectedValue & "-1"
            Select Case typeID.SelectedValue
                Case 1
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.workprice as amount,u.leavedate , "


                Case 2
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.subsidy as amount,u.leavedate , "


                Case 3
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.fixedsalary as amount,u.leavedate ,"


                Case 4
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.extrasubsidy as amount,u.leavedate , "


                Case 5
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.memberShipPay as amount,u.leavedate, "


                Case 6
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.overprice as amount,u.leavedate ,"

                Case 7
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.SalaryAdjust as amount,u.leavedate ,"

                Case 8
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.tempjobs as amount,u.leavedate ,"

                Case 9 'add by Baoxin 20080912 for improve salary
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.overbenefit as amount,u.leavedate ,"

                Case 10 'add by Baoxin 20090210 for holiday pay
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.holidaypay as amount,u.leavedate ,"

                Case 11 'add by Baoxin 20090415 for access pay
                    strSql = " Select a.ID,a.userID,isnull(d.name,'') as dname,isnull(w.name,'') as wname,s.systemCodeName as worktype,u.userNo,u.userName,a.accesspay as amount,u.leavedate ,"
            End Select
            strSql &= " isnull(s1.systemCodename,''),isnull(s2.systemCodeName,''),isnull(s3.systemCodeName,''),u.enterdate,isnull(s4.roleName,'') as rolename,"
            strSql &= " CASE WHEN u.enterdate is null THEN '' ELSE CASE WHEN MONTH(u.enterdate)<=MONTH(getdate()) THEN datediff(year,u.enterdate,getdate()) ELSE datediff(year,u.enterdate,getdate())-1 END END as workyear,ISNULL(s5.systemCodeName,''), "
            strSql &= " CASE WHEN '" & typeID.SelectedValue & "' =11 THEN a.comment ELSE N'&nbsp;' END AS comment, "
            strSql &= " year(getdate())-year(birthday),certificates"
            strSql &= " From BasicSalary a"
            strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = a.userID "
            strSql &= " left outer join departments d on d.departmentID=u.departmentID "
            strSql &= " left outer join workshop w on w.id=u.workshopID "
            strSql &= " left outer join tcpc0.dbo.systemCode s1 ON s1.systemCodeID=u.contractTypeID"
            strSql &= " left outer join tcpc0.dbo.systemCode s2 ON s2.systemCodeID=u.employTypeID"
            strSql &= " left outer join tcpc0.dbo.systemCode s3 ON s3.systemCodeID=u.insuranceTypeID "
            strSql &= " left outer join Roles s4 ON s4.roleID=u.roleID "
            'add by Baoxin in 20081031
            strSql &= "LEFT OUTER JOIN tcpc0.dbo.systemCode s5 ON  s5.systemCodeID= u.educationID  "  '/ Education for Users

            strSql &= " left outer join (select w.userID,w.worktypeID From WorktypeChange w inner join (select userid,min(changedate) as changedate From WorktypeChange where changedate>='" & xdate & "' group by userID) sd  on sd.userID=w.userID and sd.changedate=w.changedate) wt on wt.userID=u.userID "
            strSql &= " inner join tcpc0.dbo.systemCode s on s.systemCodeID=CASE WHEN wt.workTypeID is null THEN u.workTypeID ELSE wt.workTypeID END"

            Select Case typeID.SelectedValue
                Case 1
                    strSql &= " Where a.workprice is not null "
                Case 2
                    strSql &= " Where a.subsidy is not null   "
                Case 3
                    strSql &= " Where a.fixedsalary is not null   "
                Case 4
                    strSql &= " Where a.extrasubsidy is not null  "
                Case 5
                    strSql &= " Where a.memberShipPay is not null  "
                Case 6
                    strSql &= " Where a.overprice is not null   "
                Case 7
                    strSql &= " Where a.SalaryAdjust is not null   "
                Case 8
                    strSql &= " Where a.tempjobs is not null   "
                Case 9 'add by Baoxin 20080912 for improve salary
                    strSql &= " Where a.overbenefit is not null   "
                Case 10 'add by Baoxin 20090210 for holiday pay
                    strSql &= " Where a.holidaypay is not null   "
                Case 11 'add by Baoxin 20090415 for access pay
                    strSql &= " Where a.accesspay is not null   "
            End Select
            strSql &= " and year(workdate)='" & year.Text.Trim() & "' and month(workdate)='" & month.SelectedValue & "' "
            'strSql &= " and u.isTemp='" & Session("temp") & "'"
            If temp > 0 Then
                If workNO.Text.Trim() <> "" Then
                    strSql &= " and cast(u.userNo as varchar)='" & workNO.Text.Trim & "'"
                End If
                If workName.Text.Trim() <> "" Then
                    strSql &= " and lower(u.userName) like N'%" & workName.Text.Trim.ToLower() & "%'"
                End If
                If departmentdrop.SelectedValue <> 0 Then
                    strSql &= " and u.departmentID=" & departmentdrop.SelectedValue
                End If
                If Dropdownlist1.SelectedValue <> 0 Then
                    strSql &= " and u.workTypeID=" & Dropdownlist1.SelectedValue
                End If
                If workshop.SelectedValue <> 0 Then
                    strSql &= " and u.workshopID=" & workshop.SelectedValue
                End If
                strSql &= " and u.plantCode='" & Session("PlantCode") & "' "
            End If
            strSql &= " order by a.userID  "
            'Response.Write(strSql)
            'Exit Sub


            createSQL = strSql
        End Function

        Sub datebind(ByVal temp As Integer)

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL(temp))
            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("kinds", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ctype", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("leavedate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workyear", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("rolename", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("comment", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim dr1 As DataRow
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("userID") = .Rows(i).Item("userID").ToString()
                        dr1.Item("userName") = .Rows(i).Item("userName")
                        dr1.Item("kinds") = .Rows(i).Item("amount")
                        dr1.Item("ID") = .Rows(i).Item("ID")
                        dr1.Item("ctype") = .Rows(i).Item("worktype")
                        dr1.Item("department") = .Rows(i).Item("dname")
                        dr1.Item("workshop") = .Rows(i).Item("wname")
                        dr1.Item("userNo") = .Rows(i).Item("userNo").ToString()
                        If .Rows(i).Item("leavedate").ToString() = "" Then
                            dr1.Item("leavedate") = ""
                        Else
                            dr1.Item("leavedate") = .Rows(i).Item("leavedate").ToShortDateString()
                        End If
                        dr1.Item("workyear") = .Rows(i).Item("workyear")
                        dr1.Item("rolename") = .Rows(i).Item("rolename")
                        dr1.Item("comment") = .Rows(i).Item("comment")
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

        Public Sub Edit_edit(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            'CType(e.Item.FindControl("unit"), DropDownList).SelectedValue = CInt(e.Item.Cells(12).Text)
            datebind(1)
        End Sub
        Public Sub Edit_cancel(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
            DataGrid1.EditItemIndex = -1
            datebind(1)
        End Sub
        Public Sub Edit_delete(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
            Select Case typeID.SelectedValue
                Case 1
                    strSql = " Update  BasicSalary SET workprice=null "
                Case 2
                    strSql = " Update  BasicSalary SET subsidy=null "
                Case 3
                    strSql = " Update  BasicSalary SET fixedsalary=null "
                Case 4
                    strSql = " Update  BasicSalary SET extrasubsidy=null "
                Case 5
                    strSql = " Update  BasicSalary SET memberShipPay=null "
                Case 6
                    strSql = " Update  BasicSalary SET overprice=null "
                Case 7
                    strSql = " Update  BasicSalary SET SalaryAdjust=null "
                Case 8
                    strSql = " Update  BasicSalary SET tempjobs=null "
                Case 9 'add by Baoxin 20080912 for improve salary
                    strSql = " Update  BasicSalary SET overbenefit=null "
                Case 10  'add by Baoxin 20090210 for holiday pay
                    strSql = " Update  BasicSalary SET holidaypay=null "
                Case 11 'add by Baoxin 20090415 for access pay
                    strSql = " Update  BasicSalary SET accesspay=null "
            End Select
            'strSql = " Update  BasicSalary SET amount=" & CType(e.Item.Cells(3).Controls(0), TextBox).Text
            strSql &= " Where id =" & e.Item.Cells(12).Text.Trim()
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            datebind(1)
        End Sub
        Public Sub Edit_update(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
            Select Case typeID.SelectedValue
                Case 1
                    strSql = " Update  BasicSalary SET workprice=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 2
                    strSql = " Update  BasicSalary SET subsidy=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 3
                    strSql = " Update  BasicSalary SET fixedsalary=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 4
                    strSql = " Update  BasicSalary SET extrasubsidy=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 5
                    strSql = " Update  BasicSalary SET memberShipPay=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 6
                    strSql = " Update  BasicSalary SET overprice=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 7
                    strSql = " Update  BasicSalary SET SalaryAdjust=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 8
                    strSql = " Update  BasicSalary SET tempjobs=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 9  'add by Baoxin 20080912 for improve salary
                    strSql = " Update  BasicSalary SET overbenefit=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 10  'add by Baoxin 20090210 for holiday pay
                    strSql = " Update  BasicSalary SET holidaypay=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
                Case 11  'add by Baoxin 20090415 for access pay
                    strSql = " Update  BasicSalary SET accesspay=" & CType(e.Item.Cells(6).Controls(0), System.Web.UI.WebControls.TextBox).Text
            End Select
            'strSql = " Update  BasicSalary SET amount=" & CType(e.Item.Cells(3).Controls(0), TextBox).Text
            strSql &= " Where id =" & e.Item.Cells(12).Text.Trim()

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            DataGrid1.EditItemIndex = -1
            datebind(1)
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            datebind(1)
        End Sub

        Sub search(ByVal sender As Object, ByVal e As System.EventArgs) Handles Bsearch.Click
            DataGrid1.CurrentPageIndex = 0
            datebind(1)
        End Sub

        Private Sub month_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles month.SelectedIndexChanged
            DataGrid1.CurrentPageIndex = 0
            datebind(1)

        End Sub
        Protected Sub ButExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButExcel.Click
            Dim EXTitle As String
            EXTitle = "100^<b>部门</b>~^<b>工段</b>~^<b>计酬方式</b>~^<b>工号</b>~^<b>姓名</b>~^"
            Select Case typeID.SelectedValue
                Case 1
                    EXTitle &= "<b>工价</b>~^"
                Case 2
                    EXTitle &= "<b>补贴</b>~^"
                Case 3
                    EXTitle &= "<b>固定工资</b>~^"
                Case 4
                    EXTitle &= "<b>额外补贴</b>~^"
                Case 5
                    EXTitle &= "<b>工会费</b>~^"
                Case 6
                    EXTitle &= "<b>加班工价</b>~^"
                Case 7
                    EXTitle &= "<b>工资调整</b>~^"
                Case 8
                    EXTitle &= "<b>见习</b>~^"
                Case 9  'add by Baoxin 20080912 for improve salary
                    EXTitle &= "<b>加班工资</b>~^"
                Case 10  'add by Baoxin 20090210 for holiday pay
                    EXTitle &= "<b>基本工资</b>~^"
                Case 11  'add by Baoxin 20090415 for access pay
                    EXTitle &= "<b>*考核奖*</b>~^"
            End Select
            EXTitle &= "<b>离职日期</b>~^<b>合同类型</b>~^<b>用工性质</b>~^<b>保险类型</b>~^<b>入公司日期</b>~^<b>职务</b>~^<b>工龄</b>~^<b>学历</b>~^<b>备注</b>~^<b>年龄</b>~^<b>证书</b>~^"
            Dim ExSql As String = createSQL(1)
            Me.ExportExcel(chk.dsnx, EXTitle, ExSql, False)

        End Sub
    End Class

End Namespace
