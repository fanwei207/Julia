'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-17
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   According ID field to Print 
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class PrintCard
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim sqlStr As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Dim nRet As Integer
    Dim recordcount As Integer = 0

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
                If Session("CardList") <> Nothing Then
                    sqlStr = Session("CardList")
                    Dim dst As DataSet = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sqlStr)
                    With dst.Tables(0)
                        recordcount = .Rows.Count
                        If recordcount <= 0 Then
                            ltlAlert.Text = " alert('没有数据，无法打印！');window.close();"
                            Exit Sub
                        End If
                    End With
                    dst.Reset()
                ElseIf Request("id") <> Nothing Then
                    getRecordCount()
                Else
                    ltlAlert.Text = "window.close();"
                    Exit Sub
                End If
                filltable()
                Session("CardList") = Nothing
            End If
    End Sub
    Sub getRecordCount()
        ltlAlert.Text = ""
        recordcount = 0
        Dim strID As String = Request("id")
        Dim reci, recj, ind As Integer
        Dim strTemp, strID1, strID2 As String

        While (strID.Length > 0)
            If strID.IndexOf(",") <> -1 Or strID.IndexOf("，") <> -1 Then
                If strID.IndexOf(",") <> -1 Then
                    ind = strID.IndexOf(",")
                ElseIf strID.IndexOf("，") <> -1 Then
                    ind = strID.IndexOf("，")
                End If
                strTemp = strID.Substring(0, ind)
                strID = strID.Substring(ind + 1)
            Else
                strTemp = strID
                strID = ""
            End If
            ind = strTemp.IndexOf("-")
            If ind <> -1 Then
                strID1 = strTemp.Substring(0, ind)
                strID2 = strTemp.Substring(ind + 1)
            Else
                strID1 = strTemp
                strID2 = strTemp
            End If
            Try
                reci = CInt(strID1)
            Catch
                reci = -1
            End Try
            Try
                recj = CInt(strID2)
            Catch
                recj = -1
            End Try
            If (reci = -1 And recj = -1) Then

            Else
                If (reci = -1) Then
                    reci = recj
                End If
                If (recj = -1) Then
                    recj = reci
                End If
                recordcount = recordcount + Math.Abs(recj - reci) + 1
            End If
        End While
    End Sub

    Sub filltable()
        Dim strID As String = Request("id")
        Dim reci, recj, posi, ind As Integer
        Dim strTemp, strID1, strID2 As String
        Dim strUserID(recordcount - 1) As String

        Dim line As Integer = 10
        Dim totalpage As Integer = 1
        Dim tb, tb1 As Table
        Dim dst As DataSet


        Dim i As Integer = -1
        Dim j, k, n As Integer
        Dim _orgname As String
        Dim _uname As String
        Dim _uid As String
        Dim _dept As String
        Dim _role As String

        posi = 0
        If Session("CardList") = Nothing Then
            While (strID.Length > 0)
                If strID.IndexOf(",") <> -1 Or strID.IndexOf("，") <> -1 Then
                    If strID.IndexOf(",") <> -1 Then
                        ind = strID.IndexOf(",")
                    ElseIf strID.IndexOf("，") <> -1 Then
                        ind = strID.IndexOf("，")
                    End If
                    strTemp = strID.Substring(0, ind)
                    strID = strID.Substring(ind + 1)
                Else
                    strTemp = strID
                    strID = ""
                End If
                ind = strTemp.IndexOf("-")
                If ind <> -1 Then
                    strID1 = strTemp.Substring(0, ind)
                    strID2 = strTemp.Substring(ind + 1)
                Else
                    strID1 = strTemp
                    strID2 = strTemp
                End If
                Try
                    reci = CInt(strID1)
                Catch
                    reci = -1
                End Try
                Try
                    recj = CInt(strID2)
                Catch
                    recj = -1
                End Try
                If (reci = -1 And recj = -1) Then

                Else
                    If (reci = -1) Then
                        reci = recj
                    End If
                    If (recj = -1) Then
                        recj = reci
                    End If
                    If (reci > recj) Then
                        ind = recj
                        recj = reci
                        reci = ind
                    End If
                    While (reci <= recj)
                        strUserID(posi) = reci
                        reci = reci + 1
                        posi = posi + 1
                    End While
                End If
            End While
        Else
            sqlStr = Session("CardList")
            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sqlStr)
            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    For j = 0 To recordcount - 1
                        strUserID(j) = CInt(.Rows(j).Item(0))
                    Next
                End If
            End With
            dst.Reset()
        End If

        totalpage = recordcount
        If totalpage > 0 Then
            If totalpage Mod line = 0 Then
                totalpage = totalpage / line
            Else
                totalpage = (totalpage - (totalpage Mod line)) / line + 1
            End If
        End If

        k = 0
        n = 1
        For j = 0 To recordcount - 1
            sqlStr = " Select u.userNo, u.userName, r.roleName, d.name " _
                   & " From tcpc0.dbo.users u " _
                   & " Inner Join departments d On u.departmentID = d.departmentID And d.active=1 " _
                   & " Inner Join Roles r On u.roleID = r.roleID " _
                   & " Where u.deleted=0 And u.isActive=1 And u.userID='" & strUserID(j) & "' And u.roleID<>1 And u.leaveDate IS NULL And u.organizationID='" & Session("orgID") & "'"
            sqlStr &= " and u.isTemp='" & Session("temp") & "' and plantCode='" & Session("PlantCode") & "'"

            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sqlStr)
            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    If i <= 0 Then
                        If i = 0 Then
                            Dim lbl1 As New Label
                            lbl1.Text = "<p Style = 'page-break-before:always;'></p>"
                            PlaceHolder1.Controls.Add(lbl1)
                        End If
                        k = k + 1
                        tb = New Table
                        tb.CellSpacing = 0
                        tb.CellPadding = 0
                        tb.GridLines = GridLines.None
                        tb.BorderWidth = New Unit(1)
                        tb.BorderColor = System.Drawing.Color.Black
                        tb.Width = New Unit(360)
                        tb.Height = New Unit(180)
                        fillRow(tb, "<IMG height='80' alt='' src='/images/logo" & Session("PlantCode") & ".gif' width='220'></IMG>", "", "", "")
                        fillRow(tb, "", "", "", "", 5)
                        fillRow(tb, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;姓名：<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Name", "<b><font size='4pt'>" & .Rows(0).Item(1).ToString().Trim() & "</font></b>", "", "", 3)
                        'fillRow(tb, "Name：", "<hr width=130></hr>", "", "", 2)
                        fillRow(tb, "", "", "", "", 5)
                        fillRow(tb, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;职务：<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Post", "<b><font size='4pt'>" & .Rows(0).Item(2).ToString().Trim() & "</font></b>", "", "", 3)
                        'fillRow(tb, "Post：", "<hr width=130></hr>", "", "", 2)
                        fillRow(tb, "", "", "", "", 5)
                        fillRow(tb, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;部门：<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dept", "<b><font size='4pt'>" & .Rows(0).Item(3).ToString().Trim() & "</font></b>", "工号：<br>&nbsp;No.", "<font size='4pt'>" & .Rows(0).Item(0).ToString().Trim() & "</font>", 1)
                        'fillRow(tb, "Dept：", "<hr width=130></hr>", "No.：", "<hr width=50></hr>", 4)
                        fillRow(tb, "", "", "", "", 5)

                        If n = 1 Then
                            tb1 = New Table
                            tb1.CellSpacing = 0
                            tb1.CellPadding = 0
                            tb1.GridLines = GridLines.None
                            tb1.BorderWidth = New Unit(0)
                            tb1.Width = New Unit(730)
                            tb1.Height = New Unit(180)
                            fillRow1(tb1)
                            PlaceHolder1.Controls.Add(tb1)
                        End If

                        If n = 1 Then
                            tb1.Rows(0).Cells(0).Controls.Add(tb)
                            tb = Nothing
                            n = n + 1
                        Else
                            tb1.Rows(0).Cells(1).Controls.Add(tb)
                            tb = Nothing
                            tb1 = Nothing
                            n = 1
                        End If
                        'PlaceHolder1.Controls.Add(tb)
                        'tb = Nothing

                        If n = 1 Then
                            Dim lbl2 As New Label
                            lbl2.Text = "<table><tr><td height=2></td></tr></table>"
                            PlaceHolder1.Controls.Add(lbl2)
                        End If

                        If k Mod line = 0 Then
                            i = 0
                            k = 0
                        Else
                            i = -1
                        End If
                    End If
                End If
            End With
            dst.Reset()
        Next
    End Sub
    Function fillRow(ByVal tb As Table, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, Optional ByVal type As Integer = 0)
        row = New TableRow
        row.BackColor = Color.White
        row.HorizontalAlign = HorizontalAlign.Left
        row.BorderWidth = New Unit(1)
        row.Width = New Unit(360)

        If type = 0 Then
            cell = New TableCell
            If str1 = "" Then
                cell.Text = ""
            Else
                cell.Text = str1
            End If
            cell.Width = New Unit(250)
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.ColumnSpan = 2
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "照<br><br>片"
            cell.Width = New Unit(110)
            cell.BorderWidth = New Unit(0)
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.VerticalAlign = VerticalAlign.Middle
            cell.RowSpan = 6
            cell.ColumnSpan = 2
            row.Cells.Add(cell)
        ElseIf type = 1 Then
            cell = New TableCell
            If str1 = "" Then
                cell.Text = ""
            Else
                cell.Text = str1
            End If
            cell.Width = New Unit(70)
            cell.Height = New Unit(20)
            cell.HorizontalAlign = HorizontalAlign.Left
            row.Cells.Add(cell)

            cell = New TableCell
            If str2 = "" Then
                cell.Text = ""
            Else
                cell.Text = str2
            End If
            cell.Width = New Unit(180)
            cell.Height = New Unit(20)
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.VerticalAlign = VerticalAlign.Middle
            row.Cells.Add(cell)

            cell = New TableCell
            If str3 = "" Then
                cell.Text = ""
            Else
                cell.Text = str3
            End If
            cell.Width = New Unit(50)
            cell.Height = New Unit(20)
            cell.HorizontalAlign = HorizontalAlign.Left
            row.Cells.Add(cell)

            cell = New TableCell
            If str4 = "" Then
                cell.Text = ""
            Else
                cell.Text = str4
            End If
            cell.Width = New Unit(60)
            cell.Height = New Unit(20)
            cell.HorizontalAlign = HorizontalAlign.Left
            cell.VerticalAlign = VerticalAlign.Middle
            row.Cells.Add(cell)
        ElseIf type = 3 Then
            cell = New TableCell
            If str1 = "" Then
                cell.Text = ""
            Else
                cell.Text = str1
            End If
            cell.Width = New Unit(70)
            cell.Height = New Unit(20)
            cell.HorizontalAlign = HorizontalAlign.Left
            row.Cells.Add(cell)

            cell = New TableCell
            If str2 = "" Then
                cell.Text = ""
            Else
                cell.Text = str2
            End If
            cell.Width = New Unit(180)
            cell.Height = New Unit(20)
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.VerticalAlign = VerticalAlign.Middle
            row.Cells.Add(cell)
            
        ElseIf type = 5 Then
            cell = New TableCell
            cell.Text = ""
            cell.Width = New Unit(250)
            cell.Height = New Unit(3)
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.ColumnSpan = 2
            row.Cells.Add(cell)
        End If
        tb.Rows.Add(row)
    End Function

    Function fillRow1(ByVal tb1 As Table)
        row = New TableRow
        row.BackColor = Color.White
        row.HorizontalAlign = HorizontalAlign.Left
        row.BorderWidth = New Unit(1)
        row.Width = New Unit(730)
        row.Height = New Unit(180)

        cell = New TableCell
        cell.Width = New Unit(365)
        row.Cells.Add(cell)

        cell = New TableCell
        cell.Width = New Unit(365)
        row.Cells.Add(cell)

        tb1.Rows.Add(row)
    End Function

End Class

End Namespace
