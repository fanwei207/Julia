'!*******************************************************************************!
'* @@ NAME				:	InspctBodyexcel.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for InspctBodyexcel.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	April 11 2005
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class InspctBodyexcel
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim sqlStr As String
    Dim reader As SqlDataReader

    Dim chk As New adamClass
    Dim row As TableRow
    Dim cell As TableCell

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim strSql As String 
        strSql = " Select u.userNo,u.userName as name,r.roleName,d.Name,isnull(sc1.systemcodeName,''),isnull(datediff(year,u.birthday,getdate()),'') as birthday ,isnull(w.name,'') ,uh.healthCheckDate,u.specialWorkType "
        strSql &= " From User_Health uh "
        strSql &= " Inner join tcpc0.dbo.users u on u.userID=uh.userID "
        strSql &= " Left outer JOIN Roles r ON u.roleID=r.roleID "
        strSql &= " Left outer JOIN Departments d ON d.departmentID=u.departmentID "
        strSql &= " Left outer JOIN tcpc0.dbo.systemCode sc1 ON u.sexID=sc1.systemcodeID "
        strSql &= " Left outer JOIN tcpc0.dbo.systemCodeType st1 ON sc1.systemcodetypeID=st1.systemcodetypeID and st1.systemCodeTypeName='Sex' "
        strSql &= " Left outer JOIN Workshop w ON w.id=u.workshopID  "
        If Request("type") = "0" Then
            PIMasteryRow("工号", "姓名", "职务", "部门", "工段", "性别", "年龄", "体检日期", "工种")
            strSql &= " where uh.status='1' "
        Else
            PIMasteryRow("工号", "姓名", "职务", "部门", "工段", "体检日期", "工种", "", "")
            strSql &= " where uh.status='2' and uh.userID=" & Request("uid")
        End If
        strSql &= " and  u.plantCode='" & Session("PlantCode") & "' and u.isTemp='" & Session("temp") & "'"
        'Response.Write(strSql)
        'Exit Sub
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read
            Dim worktype As String
            If reader(8) = 1 Then
                worktype = "焊锡"
            Else
                worktype = "打氯仿"
            End If
            If Request("type") = "0" Then
                PIMasteryRow(reader(0), reader(1), reader(2), reader(3), reader(4), reader(5), reader(6), reader(7), worktype)
            Else
                PIMasteryRow(reader(0), reader(1), reader(2), reader(3), reader(6), reader(7), worktype, "", "")
            End If
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
    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String)
        row = New TableRow
        If str1 = "姓名" Then
            row.BackColor = Color.LightGray
        Else
            row.BackColor = Color.White
        End If
        row.HorizontalAlign = HorizontalAlign.Right
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = str0.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str1.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str2
        'Response.Write(str2)
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str3.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str4.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str5.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Text = str6.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)


        cell = New TableCell
        cell.Text = str7.Trim()
        cell.Width = New Unit(100)
        row.Cells.Add(cell)


        cell = New TableCell
        cell.Text = str8.Trim()
        cell.Width = New Unit(80)
        row.Cells.Add(cell)
        exl.Rows.Add(row)
    End Sub
End Class

End Namespace
