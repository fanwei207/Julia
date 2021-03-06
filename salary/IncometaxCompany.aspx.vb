'!*******************************************************************************!
'* @@ NAME				:	IncometaxCompany.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for IncometaxCompany.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	June 2 2007
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

Partial Class IncometaxCompany
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim strSql As String
    Dim reader As SqlDataReader
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    'Protected WithEvents compare As System.Web.UI.WebControls.Button

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then 

            TextBox1.Text = DateTime.Now.Year.ToString()
            DropDownList1.SelectedValue = DateTime.Now.Month.ToString()

            
        End If
    End Sub
        Sub Prink_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print.Click
            If IsNumeric(TextBox1.Text.Trim) Then
            Else
                ltlAlert.Text = "alert('输入年份只能为数字!');Form1.TextBox1.focus();"
                Exit Sub
            End If

            strSql = " SELECT u.userNo,u.username,ISNULL(d.name,'') ,a.duereward, a.deuct,a.rfound,a.mfound,a.efound,a.totalfound, a.hfound,a.duereward - a.hfound - a.totalfound - a.deuct,a.tax,s1.systemCodename,u.IC,s.systemCodename,"
            strSql &= "  s2.systemCodename,ISNULL(s3.systemCodename,''),ISNULL(s4.systemCodename,''),u.enterdate,u.leavedate,ISNULL(u.comp,''),u.wldate,u.birthday "
            strSql &= " FROM "
            strSql &= " (SELECT hr_Salary_userID AS uid,hr_Salary_duereward AS duereward,hr_Salary_duct AS deuct,hr_Salary_hfound AS hfound,hr_Salary_AccountRfound AS rfound, hr_Salary_AccountMfound AS mfound, hr_Salary_AccountEfound AS efound, hr_Salary_rfound  AS totalfound,hr_Salary_tax AS tax, hr_Salary_departmentID AS department,"
            strSql &= "  '95' AS worktype, hr_Salary_employTypeID AS EmployTypeID, hr_Salary_insuranceTypeID AS insuranceTypeID "
            strSql &= "  FROM hr_fin_mstr WHERE YEAR(hr_Salary_salaryDate)='" & TextBox1.Text.Trim & "' AND MONTH(hr_Salary_salaryDate) = '" & DropDownList1.SelectedValue & "' "
            strSql &= "  UNION "
            strSql &= "  SELECT hr_Time_SalaryUserID AS uid,hr_Time_SalaryDuereward AS duereward,hr_Time_SalaryDeduct AS deduct,hr_Time_SalaryHfound AS hfound,hr_Time_accrFound AS rfound, hr_Time_accmFound AS mfound, hr_Time_acceFound AS efound, hr_Time_SalaryRfound AS totalfound, hr_Time_SalaryTax AS tax,hr_Time_SalaryDepartment AS department,"
            strSql &= "  hr_Time_SalaryWorktype AS worktype, hr_Time_SalaryEmployTypeID AS EmployTypeID, hr_Time_SalaryInsuranceTypeID AS insuranceTypeID "
            strSql &= "  FROM hr_fin_Time WHERE YEAR(hr_Time_SalaryDate)='" & TextBox1.Text.Trim & "' AND MONTH(hr_Time_SalaryDate) = '" & DropDownList1.SelectedValue & "' "
            strSql &= "  AND hr_Time_SalaryUserID NOT IN (SELECT usercode FROM EmployeeEspecial WHERE YEAR(currentdate) ='" & TextBox1.Text.Trim & "' AND Month(currentdate)='" & DropDownList1.SelectedValue & "' )"
            strSql &= "  ) AS a"
            strSql &= " INNER JOIN tcpc0.dbo.Users u ON u.userID = a.uid "
            strSql &= " LEFT OUTER JOIN departments d ON d.departmentID= a.department "
            strSql &= " INNER JOIN tcpc0.dbo.systemCode s1 ON s1.systemCodeID=u.sexID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s ON s.systemCodeID=a.worktype "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s2 ON s2.systemCodeID=a.EmployTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s3 ON s3.systemCodeID=u.insuranceTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemcode s4 ON s4.systemcodeID=u.contractTypeID "
            strSql &= " WHERE a.uid NOT IN (SELECT usercode FROM EmployeeTime WHERE MONTH(currentdate)='" & DropDownList1.SelectedValue & "' AND YEAR(currentdate)='" & TextBox1.Text.Trim & "') "
            strSql &= " ORDER BY u.userID "

            'Response.Write(strSql)
            'Exit Sub
            Session("EXHeader1") = ""
            Session("EXSQL1") = strSql
            Session("EXTitle1") = "<b>工号</b>~^<b>姓名</b>~^200^<b>部门</b>~^120^<b>应发金额</b>~^<b>实扣款</b>~^<b>养老金</b>~^<b>医疗保险</b>~^<b>失业保险</b>~^100^<b>三险合计</b>~^<b>公积金</b>~^150^<b>小计</b>~^<b>所得税</b>~^<b>性别</b>~^<b>身份证号</b>~^<b>计酬方式</b>~^<b>用工性质</b>~^<b>保险类型</b>~^<b>合同类型</b>~^<b>入公司日期</b>~^<b>离职日期</b>~^<b>派遣公司</b>~^<b>派遣日期</b>~^<b>生日</b>~^"

            ltlAlert.Text = "window.open('/public/exportExcel1.aspx', '_blank');"
        End Sub


   

End Class

End Namespace
