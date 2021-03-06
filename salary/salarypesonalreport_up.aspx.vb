'!*******************************************************************************!
'* @@ NAME				:	salaryPesonalReport.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for salaryPesonalReport.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	March 7 2006
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

Partial Class salaryPesonalReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim strSql As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Dim chk As New adamClass

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim query As String
        Dim typeHashtable As New Hashtable
        query = "select sc.systemCodeID,sc.systemCodeName From tcpc0.dbo.systemCode sc INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Deduct Money Type' "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, query)
        While reader.Read
            typeHashtable.Add(reader(1).ToString(), reader(0))
        End While
        reader.Close()

            strSql = "SELECT u.userNo,u.userName,hr_Salary_basic,hr_Salary_over + hr_adjust_salary,hr_Salary_nightMoney,hr_Salary_oallowance,hr_Salary_subsidies,hr_Salary_holiday,hr_Salary_duereward + hr_adjust_salary, "
            strSql &= " hr_Salary_duct,  hr_Salary_hfound, hr_Salary_rfound, hr_Salary_memship, hr_Salary_tax, hr_Salary_workpay + hr_adjust_salary, hr_Salary_shday,hr_Salary_restleave,hr_Salary_sickleave,hr_Salary_sickleavePay,"
            strSql &= " ISNULL(hr_Salary_maternityDays,0),ISNULL(hr_Salary_maternityPay,0),hr_Salary_currduct,hr_Salary_lastduct, "
            strSql &= " d.name,w.name as wname,s1.systemcodename ,isnull(s2.systemcodename,'') ,hr_Salary_fire,u.enterDate,u.leaveDate,hr_Salary_workyear,hr_Salary_salaryDate AS salarydate "
            strSql &= " FROM hr_fin_mstr_up h "
            strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID = h.hr_Salary_userID "
            strSql &= " INNER JOIN departments d ON d.departmentID = h.hr_Salary_departmentID "
            strSql &= " LEFT OUTER JOIN workshop w ON w.ID = h.hr_Salary_workShopID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s1 ON s1.systemcodeid = h.hr_Salary_employTypeID "
            strSql &= " LEFT OUTER JOIN tcpc0.dbo.systemCode s2 ON s2.systemcodeid = h.hr_Salary_insuranceTypeID "
            strSql &= " "


        If Request("ey").Trim() = "" Or Request("ey") Is Nothing Then
                strSql &= " Where (Year(h.hr_Salary_salaryDate) = " & Request("sy") & ") And (Month(h.hr_Salary_salaryDate) = " & Request("sm") & ") "
        Else
            If Request("ey") = Request("sy") Then
                    strSql &= " Where  (Year(h.hr_Salary_salaryDate) = " & Request("sy") & ")"
                    strSql &= " and (Month(h.hr_Salary_salaryDate) >= " & Request("sm") & ") and (Month(h.hr_Salary_salaryDate) <= " & Request("em") & ")"
            Else
                Dim sdate As String = Request("sy") & "-" & Request("sm") & "-1"
                Dim edate As String = Request("ey") & "-" & Request("em") & "-1"
                    strSql &= " Where  h.hr_Salary_salaryDate >= '" & sdate & "' and  h.hr_Salary_salaryDate <= '" & edate & "'"
            End If
        End If
        strSql &= " and u.userNo='" & Request("cod") & "' "
        strSql &= " and u.plantCode='" & Session("PlantCode") & "'"
            strSql &= " order by h.hr_Salary_salaryDate "

        'Response.Write(strSql)
        'Exit Sub
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        Dim ss As String = ""

        While reader.Read
            If ss = "" Then
                PIMastery(reader(0), reader(1), 0)
            End If

            Dim sdate As String
            If Year(CDate(reader("salarydate"))).ToString() <> ss Then
                ss = Year(CDate(reader("salarydate"))).ToString()
                PIMasteryHeaderRow(Year(CDate(reader("salarydate").ToString())), "")
            End If

                If reader(29).ToString() = "" Then
                    sdate = ""
                Else
                    sdate = reader(29)
                End If
                PIMasteryRow(Month(CDate(reader("salarydate").ToString())), reader(2), reader(3), reader(4), reader(5), reader(6), reader(7), reader(8), reader(9), reader(10), reader(11), reader(12), reader(13), _
                             reader(14), reader(15), reader(16), reader(17), reader(18), reader(19), reader(20), reader(21), reader(22), reader(23), reader(24), reader(25), reader(26), reader(27), reader(28), sdate, reader(30), "", "", "", "", "")
            PIMastery("", "", 1)
        End While
        reader.Close()

        Response.Clear()
        Response.Buffer = True
        Response.Charset = "UTF-8"
        Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312")
        Response.ContentType = "application/vnd.ms-excel"

        Response.Flush()
    End Sub

    Sub PIMastery(ByVal str0 As String, ByVal str1 As String, ByVal temp As Integer)

        row = New TableRow
        row.BorderWidth = New Unit(0)
        If temp = 0 Then
            cell = New TableCell
            cell.Text = "<b>" & str0 & "</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>" & str1 & "</b>"
            cell.Width = New Unit(100)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = ""
            cell.Width = New Unit(2920)
            cell.ColumnSpan = 31
            row.Cells.Add(cell)
        Else
            cell = New TableCell
            cell.Text = ""
            cell.Width = New Unit(3120)
            cell.ColumnSpan = 33
            row.Cells.Add(cell)

        End If
        exl.Rows.Add(row)
    End Sub

    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, _
                           ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal str15 As String, ByVal str16 As String, ByVal str17 As String, ByVal str18 As String, ByVal str19 As String, ByVal str20 As String, ByVal str21 As String, ByVal str22 As String, ByVal str23 As String, ByVal str24 As String, ByVal str25 As String, ByVal str26 As String, ByVal str27 As String, ByVal str28 As String, ByVal str29 As String, ByVal str30 As String, ByVal str31 As String, ByVal str32 As String, ByVal str33 As String, ByVal str34 As String)
        row = New TableRow
        row.HorizontalAlign = HorizontalAlign.Center
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = "<b>" & str0 & "月</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str1
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str2
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str3
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str4
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str5
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str6
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str7
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str8
        cell.Width = New Unit(100)
        row.Cells.Add(cell)


        cell = New TableCell
        cell.Text = str9
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str10
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str11
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str12
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str13
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str14
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str15
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str16
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str17
        cell.Width = New Unit(160)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str18
        cell.Width = New Unit(160)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str19
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str20
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str21
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str22
        'cell.Text = Format(Convert.ToDateTime(str22), "yyyy.MM.dd")
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        'If str23 = "" Then
        '    cell.Text = ""
        'Else
        '    cell.Text = Format(Convert.ToDateTime(str23), "yyyy.MM.dd")
        'End If
        cell.Text = str23
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str24
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str25
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = str26
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        If str27 = "" Then
            cell.Text = ""
        Else
            cell.Text = Format(Convert.ToDateTime(str27), "yyyy.MM.dd")
        End If

        cell.Width = New Unit(100)
        row.Cells.Add(cell)

            cell = New TableCell
            If str28 = "" Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(str27), "yyyy.MM.dd")
            End If
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str29
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = str30
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = str31
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = str32
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = str33
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = str34
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

        exl.Rows.Add(row)

    End Sub

    Sub PIMasteryHeaderRow(ByVal str0 As String, ByVal str1 As String)

        row = New TableRow
        row.BackColor = Color.LightGray
        row.HorizontalAlign = HorizontalAlign.Center
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = "<b>" & str0.Trim() & "年</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>基本工资</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>加班费</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>中夜津贴</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>津贴</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>补贴</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>国假工资</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>应发金额</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>扣款</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>公积金</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>养老金</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)


        cell = New TableCell
            cell.Text = "<b>工会费</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>所得税</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>实发金额</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>国假天数</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>年休假天数</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>病假天数</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>病假工资</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>产假天数</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>产假工资</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>当月未扣款</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>上月余扣款</b>"
        cell.Width = New Unit(160)
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>部门</b>"
            cell.Width = New Unit(160)
            row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>工段</b>"
        cell.Width = New Unit(160)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>用工性质</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>保险类型</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>离职</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>入厂日期</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = "<b>离职日期</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        cell = New TableCell
            cell.Text = "<b>工龄</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>旷工</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>事假</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)



            'cell = New TableCell
            'cell.Text = "<b>基本工资</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>超产天数</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

            'cell = New TableCell
            'cell.Text = "<b>超产工资</b>"
            'cell.Width = New Unit(100)
            'row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub
End Class

End Namespace
