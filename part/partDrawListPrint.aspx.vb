'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-3
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Export Part Draw List To Excel for ZXDZ
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class partDrawListPrint
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    'Protected WithEvents ltlAlert As Literal
    Public chk As New adamClass

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
        'Dim strplant As String = Nothing
        Dim strcode As String = Nothing
        Dim flag As Boolean = False
        Dim i As Integer = 1
        Dim tb As Table
        Dim strNo As String
        strSql = " Select pdd.partCode+Isnull(s.statusName,''), pdd.partDesc, pdd.category, pdd.deptName, pdd.enterDate, pdd.enterqty, w.name, pdd.deptCode, pd.partDrawNo, Isnull(pdd.notes,''), Isnull(pdd.unit,'') " _
               & " From PartDrawDetail pdd " _
               & " Inner Join partDraw pd On pd.partDrawID = pdd.partDrawID And pd.deleted=0 " _
               & " Inner Join warehouse w ON pdd.whPlaceID = w.warehouseID " _
               & " Left Outer Join tcpc0.dbo.status s On s.id=pdd.status "
        If Request("uid") <> Nothing Then
            strSql &= " WHERE pdd.createdBy='" & Request("uID") & "'"
        Else
            strSql &= " WHERE pd.partDrawID='" & Request("ID") & "'"
        End If
        strSql &= " Order By w.name, pdd.enterDate, pdd.deptName, pdd.partCode "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        'If reader.Read = False Then
        '    ltlAlert.Text = "alert('已经没有尚未导出的记录了！');window.close();"
        '    Exit Sub
        'End If
        While (reader.Read())
            'If strplant <> reader(6) Then
            'If flag = True Then
            '    tb = New Table
            '    tb.CellSpacing = 0
            '    tb.CellPadding = 0
            '    tb.GridLines = GridLines.None
            '    tb.BorderWidth = New Unit(0)
            '    tb.Width = New Unit(607)
            '    PIMasteryRow(tb, "", "领料人：", "", "部门批准：", "", "", "发料人：", "", 2)
            '    PIMasteryRow(tb, "CWB-06-2(2005/4)", "", "", "", "", "", "", "", 3)
            '    PIMasteryRow(tb, "", "", "", "", "", "", "", "", 0)
            '    PlaceHolder1.Controls.Add(tb)
            '    flag = False
            '    strcode = Nothing
            '    i = 1
            'End If
            strNo = reader(8)
            While strNo.Trim().Length < 10
                strNo = "0" + strNo
            End While
            strNo = "No." & strNo.Trim()
            If strcode <> reader(7) Then
                If flag = True Then
                    tb = New Table
                    tb.CellSpacing = 0
                    tb.CellPadding = 0
                    tb.GridLines = GridLines.None
                    tb.BorderWidth = New Unit(0)
                    tb.Width = New Unit(607)
                    PIMasteryRow(tb, "", "领料人：", "", "部门批准：", "", "", "发料人：", "", 2)
                    PIMasteryRow(tb, "CWB-06-2(2005/4)", "", "", "", "", "", "", "", 3)
                    PIMasteryRow(tb, "", "", "", "", "", "", "", "", 0)
                    PlaceHolder1.Controls.Add(tb)
                    flag = False
                    strcode = Nothing
                    i = 1
                End If
                flag = True
                tb = New Table
                tb.CellSpacing = 0
                tb.CellPadding = 0
                tb.GridLines = GridLines.None
                tb.BorderWidth = New Unit(0)
                tb.Width = New Unit(607)
                PIMasteryRow(tb, Session("orgName") & "非计划用领料单", "", "", "", "", "", "", "", 0)
                PIMasteryRow(tb, "非计划用料单号：" & strNo.Trim(), "打印日期：" & Format(DateTime.Now(), "yyyy年MM月dd日"), "", "", "", "", "", "", 1)
                PIMasteryRow(tb, "领用部门：" & reader(3) & "(" & reader(7) & ")", Format(reader(4), "yyyy年MM月dd日"), "", "", "", "", "", "", 1)
                PlaceHolder1.Controls.Add(tb)
                tb = New Table
                tb.CellSpacing = 0
                tb.CellPadding = 0
                tb.GridLines = GridLines.Both
                tb.BorderWidth = New Unit(1)
                tb.Width = New Unit(607)
                PIMasteryRow(tb, "序号", "部件号", "部件描述", "请领数", "单位", "实发数", "批号", "备注", 2)
            End If
            PIMasteryRow(tb, i, reader(0), reader(1), reader(5), reader(10), "", "", reader(9), 2)
            PlaceHolder1.Controls.Add(tb)
            strcode = reader(7).ToString().Trim()
            'strplant = reader(6).ToString().Trim()
            i = i + 1
            flag = True
            'Else
            '    If strcode <> reader(7) Then
            '        tb = New Table
            '        tb.CellSpacing = 0
            '        tb.CellPadding = 0
            '        tb.GridLines = GridLines.None
            '        tb.BorderWidth = New Unit(0)
            '        tb.Width = New Unit(607)
            '        PIMasteryRow(tb, "", "领料人：", "", "部门批准：", "", "", "发料人：", "", 2)
            '        PIMasteryRow(tb, "CWB-06-2(2005/4)", "", "", "", "", "", "", "", 3)
            '        PIMasteryRow(tb, "", "", "", "", "", "", "", "", 0)
            '        PlaceHolder1.Controls.Add(tb)
            '        flag = False
            '        i = 1
            '        strcode = Nothing
            '        tb = New Table
            '        tb.CellSpacing = 0
            '        tb.CellPadding = 0
            '        tb.GridLines = GridLines.None
            '        tb.BorderWidth = New Unit(0)
            '        tb.Width = New Unit(607)
            '        PIMasteryRow(tb, "上海振欣电子工程有限公司非计划用领料单", "", "", "", "", "", "", "", 0)
            '        PIMasteryRow(tb, "非计划用料单号：" & strNo.Trim(), "打印日期：" & Format(DateTime.Now(), "yyyy年MM月dd日"), "", "", "", "", "", "", 1)
            '        PIMasteryRow(tb, "领用部门：" & reader(3) & "(" & reader(7) & ")", Format(reader(4), "yyyy年MM月dd日"), "", "", "", "", "", "", 1)
            '        PlaceHolder1.Controls.Add(tb)
            '        tb = New Table
            '        tb.CellSpacing = 0
            '        tb.CellPadding = 0
            '        tb.GridLines = GridLines.Both
            '        tb.BorderWidth = New Unit(1)
            '        tb.Width = New Unit(607)
            '        PIMasteryRow(tb, "序号", "部件号", "部件描述", "请领数", "单位", "实发数", "批号", "备注", 2)
            '    End If
            '    PIMasteryRow(tb, i, reader(0), reader(1), reader(5), "", "", "", "", 2)
            '    PlaceHolder1.Controls.Add(tb)
            '    strcode = reader(7).ToString().Trim()
            '    'strplant = reader(6).ToString().Trim()
            '    i = i + 1
            '    flag = True
            'End If
        End While
        If flag = True Then
            tb = New Table
            tb.CellSpacing = 0
            tb.CellPadding = 0
            tb.GridLines = GridLines.None
            tb.BorderWidth = New Unit(0)
            tb.Width = New Unit(607)
            PIMasteryRow(tb, "", "领料人：", "", "部门批准：", "", "", "发料人：", "", 2)
            PIMasteryRow(tb, "CWB-06-2(2005/4)", "", "", "", "", "", "", "", 3)
            PlaceHolder1.Controls.Add(tb)
            PIMasteryRow(tb, "", "", "", "", "", "", "", "", 0)
            PlaceHolder1.Controls.Add(tb)
        Else
            ltlAlert.Text = "alert('已经没有尚未导出的记录了！');window.close();"
            Exit Sub
        End If
        reader.Close()
        'If Request("uID") = Nothing Then
        '    strSql = " Update PartDrawDetail Set isExportOK='1' Where createdBy='" & Session("uID") & "'"
        '    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        '    ltlAlert.Text = "window.opener.reload();"
        'End If
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=partDraw.xls")
        Response.Flush()
    End Sub

    Sub PIMasteryRow(ByVal tb As Table, ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal type As Integer)
        row = New TableRow
        row.BackColor = Color.White
        row.HorizontalAlign = HorizontalAlign.Left
        row.BorderWidth = New Unit(0)
        row.Width = New Unit(607)

        If type = 0 Then
            cell = New TableCell
            If (str0 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str0.Trim()
            End If
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Width = New Unit(607)
            cell.Height = New Unit(40)
            cell.Font.Size = New FontUnit(16)
            cell.ColumnSpan = 8
            row.Cells.Add(cell)
        ElseIf type = 1 Then
            cell = New TableCell
            If (str0 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str0.Trim()
            End If
            cell.HorizontalAlign = HorizontalAlign.Left
            'cell.Width = New Unit(478)
            cell.Height = New Unit(28)
            cell.Font.Size = New FontUnit(10)
            cell.ColumnSpan = 6
            cell.BorderColor = Color.White
            row.Cells.Add(cell)

            cell = New TableCell
            If (str1 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str1.Trim()
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
            cell.Height = New Unit(28)
            'cell.Width = New Unit(129)
            cell.Font.Size = New FontUnit(10)
            cell.ColumnSpan = 2
            cell.BorderColor = Color.White
            row.Cells.Add(cell)
        ElseIf type = 2 Then
            cell = New TableCell
            If (str0 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str0.Trim()
            End If
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Height = New Unit(33)
            cell.Width = New Unit(47)
            cell.Font.Size = New FontUnit(10)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str1 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str1.Trim()
            End If
            If str1 = "部件号" Then
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            cell.Height = New Unit(33)
            cell.Width = New Unit(89)
            cell.Font.Size = New FontUnit(10)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str2 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str2.Trim()
            End If
            If str2 = "部件描述" Then
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            cell.Wrap = True
            cell.Height = New Unit(33)
            cell.Width = New Unit(145)
            cell.Font.Size = New FontUnit(10)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str3 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str3.Trim()
            End If
            If str3 = "请领数" Then
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.HorizontalAlign = HorizontalAlign.Right
            End If
            cell.Height = New Unit(33)
            cell.Width = New Unit(75)
            cell.Font.Size = New FontUnit(10)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str4 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str4.Trim()
            End If
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Height = New Unit(33)
            cell.Width = New Unit(47)
            cell.Font.Size = New FontUnit(10)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str5 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str5.Trim()
            End If
            If str5 = "实发数" Then
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            cell.Height = New Unit(33)
            cell.Width = New Unit(75)
            cell.Font.Size = New FontUnit(10)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str6 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str6.Trim()
            End If
            If str6 = "批号" Then
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            cell.Height = New Unit(33)
            cell.Width = New Unit(68)
            cell.Font.Size = New FontUnit(10)
            row.Cells.Add(cell)

            cell = New TableCell
            If (str7 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str7.Trim()
            End If
            If str7 = "备注" Then
                cell.HorizontalAlign = HorizontalAlign.Center
            Else
                cell.HorizontalAlign = HorizontalAlign.Left
            End If
            cell.Height = New Unit(33)
            cell.Width = New Unit(61)
            cell.Font.Size = New FontUnit(10)
            row.Cells.Add(cell)
        ElseIf type = 3 Then
            cell = New TableCell
            If (str0 = Nothing) Then
                cell.Text = ""
            Else
                cell.Text = str0.Trim()
            End If
            cell.HorizontalAlign = HorizontalAlign.Left
            cell.Height = New Unit(28)
            cell.Width = New Unit(607)
            cell.Font.Size = New FontUnit(10)
            cell.ColumnSpan = 8
            row.Cells.Add(cell)
        End If
        tb.Rows.Add(row)
    End Sub

End Class

End Namespace
