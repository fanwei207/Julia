Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class personalDetail
        Inherits BasePage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim row As TableRow
    Dim cell As TableCell
    Public chk As New adamClass



    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化页的用户代码
        If Not IsPostBack Then
            Dim query As String
            Dim reader As SqlDataReader

            query = " SELECT u.userID,u.userName as name,roleName,d.Name,LoginName,userPWD,isnull(sc1.systemcodeName,''),datediff(year,birthday,getdate()),isnull(sc2.systemcodeName,''),enterDate ,isnull(w.name,''),u.leaveDate,isnull(sc3.systemcodeName,''),isnull(u.comments,'') "
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
            query &= " Where u.deleted=0 and u.roleID>1 and u.organizationID=" & Session("orgID")
            query &= " and u.userID=" & Request("uid")
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, query)
            While reader.Read
                Dim flag As String
                Dim leavedate As String
                Dim enterdate As String
                HeaderTableProc("", "", "")
                DataTableRow("工号", "姓名", "职务", "部门", 2)
                DataTableRow(reader(0), reader(1), reader(2), reader(3), 1)
                DataTableRow("", "", "", "", 0)
                If reader(11).ToString() = "" Then
                    flag = "否"
                    leavedate = ""
                Else
                    flag = "是"
                    leavedate = reader(11).ToShortDateString()
                End If
                If reader(9).ToString() = "" Then
                    enterdate = ""
                Else
                    enterdate = reader(9).ToShortDateString()
                End If
                DataTableRow("工段", "性质", "离职", "进入公司时间", 2)
                DataTableRow(reader(10), reader(12), flag, enterdate, 1)
                DataTableRow("", "", "", "", 0)
                DataTableRow("离职时间", "备注", "", "", 2)
                DataTableRow(leavedate, reader(13), "", "", 3)
                DataTableRow("", "", "", "", 0)
            End While
            reader.Close()

        End If
    End Sub

    Sub HeaderTableProc(ByVal str As String, ByVal str1 As String, ByVal str2 As String)
        Dim HeaderTable As New Table
        HeaderTable.CellSpacing = 0
        HeaderTable.CellPadding = 2
        HeaderTable.GridLines = GridLines.Both
        HeaderTable.BorderWidth = New Unit(0)
        HeaderTable.BackColor = Color.Black
        HeaderTable.Width = New Unit(780)

        row = New TableRow
        row.BackColor = Color.White
        row.ForeColor = Color.Gray

        row.HorizontalAlign = HorizontalAlign.Center
        row.VerticalAlign = VerticalAlign.Top

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.VerticalAlign = VerticalAlign.Middle
        cell.Width = New Unit(200)
        cell.Text = "<b><font size=2> &nbsp;&nbsp;" & str & "</font></b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.VerticalAlign = VerticalAlign.Middle
        cell.Width = New Unit(200)
        cell.Text = "<b><font size=2>&nbsp;&nbsp;" & str1 & "</font></b>"
        row.Cells.Add(cell)

        cell = New TableCell
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.VerticalAlign = VerticalAlign.Middle
        cell.Width = New Unit(200)
        cell.Text = "<b><font size=2> &nbsp;&nbsp;" & str2 & "</font></b>"
        row.Cells.Add(cell)

        HeaderTable.Rows.Add(row)
        ReportTablesHolder.Controls.Add(HeaderTable)
    End Sub

    Sub DataTableRow(ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal temp As Integer)
        Dim tableTemp As New Table
        tableTemp.CellSpacing = 0
        tableTemp.CellPadding = 2
        tableTemp.GridLines = GridLines.Both
        tableTemp.BorderWidth = New Unit(1)
        tableTemp.BorderColor = Color.Gray
        tableTemp.Width = New Unit(600)

        row = New TableRow
        If temp = 2 Then
            row.BackColor = Color.SteelBlue
            row.ForeColor = Color.White
        Else
            row.BackColor = Color.WhiteSmoke
        End If

        If temp = 0 Then
            cell = New TableCell
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Width = New Unit(780)
            cell.Text = "&nbsp;"
            row.Cells.Add(cell)
        Else
            If temp = 1 Then
                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Width = New Unit(195)
                cell.Text = str1
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Width = New Unit(195)
                cell.Text = str2
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Width = New Unit(195)
                cell.Text = str3
                row.Cells.Add(cell)

                cell = New TableCell
                cell.HorizontalAlign = HorizontalAlign.Center
                cell.Width = New Unit(195)
                cell.Text = str4
                row.Cells.Add(cell)
            Else
                If temp = 2 Then
                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.VerticalAlign = VerticalAlign.Middle
                    cell.Width = New Unit(195)
                    cell.Text = "<b><font size=2>" & str1 & "</font></b>"
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.VerticalAlign = VerticalAlign.Middle
                    cell.Width = New Unit(195)
                    cell.Text = "<b><font size=2>" & str2 & "</font></b>"
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.VerticalAlign = VerticalAlign.Middle
                    cell.Width = New Unit(195)
                    cell.Text = "<b><font size=2>" & str3 & "</font></b>"
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.VerticalAlign = VerticalAlign.Middle
                    cell.Width = New Unit(195)
                    cell.Text = "<b><font size=2>" & str4 & "</font></b>"
                    row.Cells.Add(cell)
                Else
                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Width = New Unit(195)
                    cell.Text = str1
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Width = New Unit(585)
                    cell.Text = str2
                    cell.ColumnSpan = 3
                    row.Cells.Add(cell)
                End If
            End If
        End If

        tableTemp.Rows.Add(row)
        ReportTablesHolder.Controls.Add(tableTemp)
    End Sub

    Sub backpage(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect(chk.urlRand("personnellist.aspx"))
    End Sub
End Class

End Namespace
