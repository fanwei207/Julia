'!*******************************************************************************!
'* @@ NAME				:	EmployeAnalyseExcel.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for SalaryAnalyseExcel.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	February 22 2006
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

Partial Class EmployeAnalyseExcel
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
    Shared typeHashtable As New Hashtable
    Shared simpleHashtable As New Hashtable
    Shared totalHashtable As New Hashtable

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            Dim Dprew As String = ""
            Dim Dwid As String = ""
            Dim Dname As String
            Dim Wname As String
            Dim Dstring As String = ""
            Dim j As Integer
            Dim flag As Boolean = False
            strSql = " SELECT d.name AS dname, d.departmentID,isnull(w.name,'') AS wname, isnull(w.id,0) FROM departments d LEFT OUTER JOIN"
            strSql &= " Workshop w ON w.departmentID = d.departmentID WHERE (w.workshopID IS NULL) AND (d.isSalary = 1)  ORDER BY d.departmentID,w.id "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            Dim i As Integer = 0
            While reader.Read
                If Dprew <> reader(1).ToString() Then
                    If flag = True Then
                        Wname = Wname & "0@"
                        Dstring = Dstring & Dname & "^" & i.ToString() & "~"
                        Dwid = Dwid & Dprew & "x@" & Dprew & "y@"
                        i = 0
                    End If
                    Dprew = reader(1).ToString()
                    Dname = reader(0).ToString()

                End If

                i = i + 1

                'No workshopID
                If reader(3).ToString() = "0" Then
                    i = 0
                Else
                    Dwid = Dwid & reader(3).ToString() & "@"
                    Wname = Wname & reader(2).ToString() & "@"
                End If
                flag = True
            End While
            reader.Close()
            Wname = Wname & "0@"
            Dstring = Dstring & Dname & "^" & i.ToString() & "~"
            Dwid = Dwid & Dprew & "x@" & Dprew & "y@"
            '// Get all deparment's Name and their worshop name

            'Response.Write(Dwid)
            'Exit Sub
            typeHashtable.Clear()
            simpleHashtable.Clear()
            totalHashtable.Clear()

            '// 
            Dim worktype(4) As String
            Array.Clear(worktype, 0, 4)
            i = 0
            strSql = " select s.systemCodeID,s.systemCodeName FRom tcpc0.dbo.SystemCode s inner join tcpc0.dbo.SystemCodeType st on st.systemCodeTypeID=s.systemCodeTypeID and st.systemCodeTypeName='Work Type' order by s.systemCodeID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                typeHashtable.Add(reader(0).ToString(), reader(1))
                worktype(i) = reader(0).ToString()
                i = i + 1
            End While
            j = i
            reader.Close()
            '// Get worktype

            ' Add key and value in hashtable for output
            Dim proW As String = Dwid
            Dim ind As Integer
            While proW.Length > 0
                ind = proW.IndexOf("@")
                If (ind > -1) Then
                    simpleHashtable.Add(proW.Substring(0, ind), 0)
                    totalHashtable.Add(proW.Substring(0, ind), 0)
                End If
                proW = proW.Substring(ind + 1)
            End While

            'copy hashtable
            Dim rowhashtable As New Hashtable
            Dim doublehashtable As New Hashtable

            Dim myEnumerat As IDictionaryEnumerator = simpleHashtable.GetEnumerator()
            While myEnumerat.MoveNext()
                rowhashtable.Add(myEnumerat.Key, myEnumerat.Value)
                doublehashtable.Add(myEnumerat.Key, myEnumerat.Value)
            End While


            strSql = " Select count(ep.UID), ep.workdate,u.departmentID,isnull(u.workshopID,'0'),u.workTypeID From "
            strSql &= " (Select CASE WHEN pt.usercode IS NULL THEN at.usercode ELSE pt.usercode END as UID,CASE WHEN pt.workdate IS NULL THEN at.workdate ELSE pt.workdate END as workdate From "
            strSql &= " (select p.usercode,p.workdate From PieceAttendence p Where year(p.workdate)='" & Request("year") & "' and month(p.workdate)='" & Request("month") & "' and Day(p.workdate)>='" & Request("day1") & "' and Day(p.workdate)<='" & Request("day2") & "' ) pt"
            strSql &= " Full Outer JOIN "
            strSql &= " (select a.usercode,a.workdate From Attendance a Where year(a.workdate)='" & Request("year") & "' and month(a.workdate)='" & Request("month") & "' and Day(a.workdate)>='" & Request("day1") & "' and Day(a.workdate)<='" & Request("day2") & "' ) at ON at.usercode=pt.usercode and at.workdate=pt.workdate ) ep "
            strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID=ep.UID"
            strSql &= "  group by ep.workdate,u.workTypeID,u.departmentID,u.workshopID order by ep.workdate,u.workTypeID "

            'Response.Write(strSql)
            'Exit Sub
            i = 0
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            Dim iday As Integer = 0
            Dim dID As String
            Dim wID As String
            Dim wtype As String = ""
            Dim inputs As String
            Dim dpeople As Integer = 0
            'Dim totalRow As Integer = 0
            'Dim total As Integer = 0
            'Dim amount As Integer
            'Dim especial As Integer
            flag = False
            While reader.Read
                If iday < Day(CDate(reader(1).ToString())) Then
                    If flag = True Then
                        ' output every total people numbers in a workshop,department and day
                        rowhashtable(dID & "y") = rowhashtable(dID & "y") + dpeople
                        doublehashtable(dID & "y") = doublehashtable(dID & "y") + dpeople
                        totalHashtable(dID & "y") = totalHashtable(dID & "y") + dpeople

                        dpeople = 0
                        'To output a row for supplying
                        While worktype(i) <> wtype
                            PIMasteryRow(typeHashtable(worktype(i)), Dwid, simpleHashtable)
                            i = i + 1
                        End While

                        ' Output the day rows 
                        PIMasteryRow(typeHashtable(wtype), Dwid, rowhashtable)
                        i = i + 1
                        While i < j
                            PIMasteryRow(typeHashtable(worktype(i)), Dwid, simpleHashtable)
                            i = i + 1
                        End While

                        PIMasteryRow("小计", Dwid, doublehashtable)

                        rowhashtable.Clear()
                        doublehashtable.Clear()
                        Dim myEnumerator As IDictionaryEnumerator = simpleHashtable.GetEnumerator()
                        While myEnumerator.MoveNext()
                            rowhashtable.Add(myEnumerator.Key, myEnumerator.Value)
                            doublehashtable.Add(myEnumerator.Key, myEnumerator.Value)
                        End While
                        flag = False
                        i = 0
                    End If

                    iday = Day(CDate(reader(1).ToString()))
                    PIMasteryHeaderRow(Dstring, Wname, Month(CDate(reader(1).ToString())), iday)
                    dID = -1
                    wID = -1
                End If

                If wtype <> reader(4).ToString() Then
                    If flag = True Then
                        ' output every total people numbers in a workshop,department and day
                        rowhashtable(dID & "y") = rowhashtable(dID & "y") + dpeople
                        doublehashtable(dID & "y") = doublehashtable(dID & "y") + dpeople
                        totalHashtable(dID & "y") = totalHashtable(dID & "y") + dpeople
                        dpeople = 0

                        'To output a row for supplying
                        While worktype(i) <> wtype
                            PIMasteryRow(typeHashtable(worktype(i)), Dwid, simpleHashtable)
                            i = i + 1
                        End While
                        PIMasteryRow(typeHashtable(wtype), Dwid, rowhashtable)
                        i = i + 1
                        rowhashtable.Clear()
                        Dim myEnum As IDictionaryEnumerator = simpleHashtable.GetEnumerator()
                        While myEnum.MoveNext()
                            rowhashtable.Add(myEnum.Key, myEnum.Value)
                        End While
                        flag = False
                    End If
                    wtype = reader(4).ToString()
                    dID = -1
                    wID = -1
                End If

                If dID <> reader(2).ToString() Then
                    If flag = True Then
                        ' output every total people numbers in a workshop,department and day
                        
                        rowhashtable(dID & "y") = rowhashtable(dID & "y") + dpeople
                        doublehashtable(dID & "y") = doublehashtable(dID & "y") + dpeople
                        totalHashtable(dID & "y") = totalHashtable(dID & "y") + dpeople
                        dpeople = 0
                        flag = False
                    End If
                    dID = reader(2).ToString()
                    wID = -1
                End If

                If wID <> reader(3).ToString() Then
                    wID = reader(3).ToString()
                    If wID = "0" Then
                        'Ouput some people in the department but no workshop
                        rowhashtable(dID & "x") = rowhashtable(dID & "x") + CInt(reader(0))
                        doublehashtable(dID & "x") = doublehashtable(dID & "x") + CInt(reader(0))
                        totalHashtable(dID & "x") = totalHashtable(dID & "x") + CInt(reader(0))
                    Else
                        rowhashtable(wID) = rowhashtable(wID) + CInt(reader(0))
                        doublehashtable(wID) = doublehashtable(wID) + CInt(reader(0))
                        totalHashtable(wID) = totalHashtable(wID) + CInt(reader(0))
                    End If

                End If
                'amount = reader(0)
                dpeople = dpeople + CInt(reader(0))

                flag = True
            End While
            reader.Close()
            If flag = True Then
                rowhashtable(dID & "y") = rowhashtable(dID & "y") + dpeople
                doublehashtable(dID & "y") = doublehashtable(dID & "y") + dpeople
                totalHashtable(dID & "y") = totalHashtable(dID & "y") + dpeople

                While worktype(i) <> wtype
                    PIMasteryRow(typeHashtable(worktype(i)), Dwid, simpleHashtable)
                    i = i + 1
                End While

                PIMasteryRow(typeHashtable(wtype), Dwid, rowhashtable)
                i = i + 1
                While i < j
                    PIMasteryRow(typeHashtable(worktype(i)), Dwid, simpleHashtable)
                    i = i + 1
                End While

                PIMasteryRow("小计", Dwid, doublehashtable)
                PIMasteryRow("总计", Dwid, totalHashtable)
            End If
            'clear hashtable

            Response.Clear()
            Response.Buffer = True
            Response.Charset = "UTF-8"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312")
            Response.ContentType = "application/vnd.ms-excel"

            Response.Flush()

            rowhashtable.Clear()
            doublehashtable.Clear()
            typeHashtable.Clear()
            simpleHashtable.Clear()
            totalHashtable.Clear()

        End If
    End Sub

    'Sub PIMasteryRowT(ByVal str0 As String, ByVal yeshashtable As Hashtable)
    '    row = New TableRow
    '    row.BackColor = Color.LightGray
    '    cell = New TableCell
    '    cell.Text = "<b>" & str0 & "</b>"
    '    cell.Width = New Unit(100)
    '    row.Cells.Add(cell)

    '    Dim myEnumerator As IDictionaryEnumerator = yeshashtable.GetEnumerator()

    '    While myEnumerator.MoveNext()
    '        cell = New TableCell
    '        cell.Text = "<b>" & myEnumerator.Key.ToString() & "</b>"
    '        cell.Width = New Unit(120)
    '        row.Cells.Add(cell)
    '    End While
    '    exl.Rows.Add(row)
    'End Sub

    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal yeshashtable As Hashtable)
        Dim total As Integer = 0
        row = New TableRow

        cell = New TableCell
        cell.Text = "<b>" & str0 & "</b>"
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        Dim ind As Integer
        While str1.Length > 0
            ind = str1.IndexOf("@")
            If (ind > -1) Then
                cell = New TableCell
                cell.Text = "<b>" & yeshashtable(str1.Substring(0, ind)).ToString() & "</b>"
                If str1.Substring(0, ind).Substring(str1.Substring(0, ind).Length - 1) = "y" Then
                    total = total + CInt(yeshashtable(str1.Substring(0, ind)))
                End If
                cell.Width = New Unit(120)
                row.Cells.Add(cell)
            End If
            str1 = str1.Substring(ind + 1)
        End While
        'Dim myEnumerator As IDictionaryEnumerator = yeshashtable.GetEnumerator()

        'While myEnumerator.MoveNext()
        '    cell = New TableCell
        '    cell.Text = "<b>" & myEnumerator.Value.ToString() & "</b>"
        '    cell.Width = New Unit(120)
        '    row.Cells.Add(cell)
        'End While
        cell = New TableCell
        cell.Text = "<b>" & total.ToString() & "</b>"
        cell.Width = New Unit(120)
        row.Cells.Add(cell)

        exl.Rows.Add(row)

    End Sub

    Sub PIMasteryHeaderRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String)
        row = New TableRow
        row.BackColor = Color.LightGray
        row.HorizontalAlign = HorizontalAlign.Center
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        cell.Text = "<b>" & str2.Trim() & "月" & str3.Trim() & "日</b>"
        cell.Width = New Unit(100)
        cell.RowSpan = 2
        row.Cells.Add(cell)

        Dim ind As Integer
        Dim dex As Integer
        While str0.Length > 0
            ind = str0.IndexOf("^")
            If (ind > -1) Then
                cell = New TableCell
                cell.Text = "<b>" & str0.Substring(0, ind) & "</b>"
                str0 = str0.Substring(ind + 1)
            End If
            dex = str0.IndexOf("~")
            If (dex > -1) Then
                cell.ColumnSpan = CInt(str0.Substring(0, dex)) + 1
                cell.Width = New Unit((CInt(str0.Substring(0, dex)) + 1) * 120)
                str0 = str0.Substring(dex + 1)
            End If
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Text = "<b>小计</b>"
            cell.Width = New Unit(120)
            cell.RowSpan = 2
            row.Cells.Add(cell)

        End While
        cell = New TableCell
        cell.Text = "<b>总计</b>"
        cell.Width = New Unit(120)
        cell.RowSpan = 2
        row.Cells.Add(cell)

        exl.Rows.Add(row)

        'second row
        row = New TableRow
        row.BackColor = Color.LightGray
        Dim sstr As String
        While str1.Length > 0
            ind = str1.IndexOf("@")
            If (ind > -1) Then
                cell = New TableCell
                If str1.Substring(0, ind) = "0" Then
                    sstr = "&nbsp;"
                Else
                    sstr = str1.Substring(0, ind)
                End If
                cell.Text = "<b>" & sstr & "</b>"
                cell.Width = New Unit(120)
                row.Cells.Add(cell)
            End If
            str1 = str1.Substring(ind + 1)
        End While
        exl.Rows.Add(row)
    End Sub

    'Function ADDhashtable(ByVal str As String)
    '    Dim ind As Integer
    '    Dim pp As String
    '    While str.Length > 0
    '        ind = str.IndexOf("@")
    '        If (ind > -1) Then
    '            simpleHashtable.Add(str.Substring(0, ind), 0)
    '            totalHashtable.Add(str.Substring(0, ind), 0)
    '            str = str.Substring(ind + 1)
    '        Else
    '            pp = str
    '            simpleHashtable.Add(pp & "x", 0)
    '            totalHashtable.Add(pp & "x", 0)
    '            str = ""
    '        End If

    '    End While
    '    simpleHashtable.Add(pp & "y", 0)
    '    totalHashtable.Add(pp & "y", 0)
    'End Function

End Class

End Namespace
