Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class personnelstatisticPrint
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
             
            If Request("ind") = Nothing Then
                Exit Sub
            End If
            Dim ind As Integer
            ind = Request("ind")
            PIMasteryRow("<b>统计项目名称:</b>", Request("txt"))

            Dim query, strsql As String
            Dim ds As DataSet
            Dim total As Integer = 0
            Dim other As Integer = 0

            query = " SELECT COUNT(*) FROM tcpc0.dbo.users WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND organizationID = 1 AND roleID > 1 "
            If Session("conceal") = 0 Then
                query &= " and isTemp='" & Session("temp") & "' "
            End If

            If ind = 13 Then
                query &= " and leavedate is not null "
            Else
                query &= " and leavedate is null "
            End If
            total = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, query)

            PIMasteryRow("<b>总人数:</b>", total.ToString())
            PIMasteryRow("<b>统计项目</b>", "<b>人数</b>")

            Select Case ind
                Case 0
                    query = "  SELECT isnull(d.name,''), COUNT(u.userID) "
                    query &= " FROM Departments d "
                    query &= " left OUTER JOIN tcpc0.dbo.users u ON d.departmentID = u.departmentID AND u.plantCode='" & Session("PlantCode") & "' and u.deleted = 0 AND u.roleID > 1 and isTemp='" & Session("temp") & "' "
                    query &= " AND u.organizationID =" & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " WHERE d.isSalary='1' "
                    query &= " GROUP BY d.name "
                Case 1
                    query = " SELECT ISNULL(r.roleName, ''), COUNT(u.userID) FROM Roles r "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.roleID = r.roleID AND u.plantCode='" & Session("PlantCode") & "' and u.deleted = 0 AND u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID =" & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " GROUP BY r.rolename "
                Case 2
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Education' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.educationID = sc.systemCodeID "
                    query &= " and u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " GROUP BY sc.systemCodeName "
                Case 3
                    query = " SELECT datediff(year,birthday,getdate())"
                    query &= " From tcpc0.dbo.users u "
                    query &= " Where u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID")
                    query &= " and u.plantCode='" & Session("PlantCode") & "' and u.leavedate is null "
                    query &= " order by datediff(year,birthday,getdate())"
                Case 4
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Contract Type' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.contractTypeID = sc.systemCodeID "
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " GROUP BY sc.systemCodeName "
                Case 5
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Occupation' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.occupationID = sc.systemCodeID "
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " GROUP BY sc.systemCodeName "
                Case 6
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Sex' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.sexID = sc.systemCodeID "
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " GROUP BY sc.systemCodeName "
                Case 7
                    query = " SELECT ISNULL(ws.name, ''), COUNT(u.userID) FROM Workshop ws "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON ws.id = u.workshopID AND u.deleted = 0 AND u.roleID > 1 "
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.deleted = 0 AND u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " GROUP BY ws.name "
                Case 8
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Province' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.provinceID = sc.systemCodeID "
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " GROUP BY sc.systemCodeName "
                Case 9
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Marriage' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.marriageID = sc.systemCodeID "
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " GROUP BY sc.systemCodeName "
                Case 10
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Work Type' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.workTypeID = sc.systemCodeID "
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " and u.leavedate is null "
                    query &= " GROUP BY sc.systemCodeName "
                Case 11
                    query = " SELECT YEAR(enterDate), COUNT(userID) FROM tcpc0.dbo.users "
                    query &= " WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 and isTemp='" & Session("temp") & "' AND enterDate IS NOT NULL AND organizationID =" & Session("orgID")
                    query &= " AND leaveDate IS NULL "
                    query &= " GROUP BY YEAR(enterDate) ORDER BY YEAR(enterDate) DESC "
                Case 12
                    query = " SELECT YEAR(employDate), COUNT(userID) FROM tcpc0.dbo.users "
                    query &= " WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 and isTemp='" & Session("temp") & "' AND employDate IS NOT NULL AND organizationID =" & Session("orgID")
                    query &= " AND leaveDate IS NULL "
                    query &= " GROUP BY YEAR(employDate) ORDER BY YEAR(employDate) DESC "
                Case 13
                    query = " SELECT YEAR(leaveDate), COUNT(userID) FROM tcpc0.dbo.users "
                    query &= " WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 and isTemp='" & Session("temp") & "' AND organizationID =" & Session("orgID")
                    query &= " AND leaveDate IS not NULL "
                    query &= " GROUP BY YEAR(leaveDate) ORDER BY YEAR(leaveDate) DESC "
                Case 14
                    Dim reader As SqlDataReader
                    Dim i As Integer = 0
                    strsql = " SELECT sc.systemCodeName FROM tcpc0.dbo.systemCode sc " _
                           & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' "
                    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)

                    While (reader.Read())
                        If i = 0 Then
                            query = " SELECT N'--',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                                  & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID") _
                                  & " and ( retiredFund=0 and houseFund=0 and medicalFund=0 and unemployFund=0 and sretiredFund=0 ) "
                            query &= " and u.leavedate is null "
                        End If
                        i = i + 1
                        query &= " Union SELECT N'" & reader(0) & "',count(userID)," & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID") _
                              & " and ( retiredFund=1 or houseFund=1 or  medicalFund=1 or unemployFund=1 or sretiredFund=1 ) and u.leavedate is null "
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;期中：', null , " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and  isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID") _
                              & " and (retiredFund=0 and houseFund=0 and medicalFund=0 and unemployFund=0 and sretiredFund=0) and u.leavedate is null "
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;四金',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID") & " and houseFund=1 and medicalFund=1 and " _
                              & " unemployFund=1 and retiredFund=1 and sretiredFund=0 and u.leavedate is null "
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;三金',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID") & " and houseFund=0 and medicalFund=1 and " _
                              & " unemployFund=1 and retiredFund=1 and sretiredFund=0 and u.leavedate is null "
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;二金',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID") & " and houseFund=0 and medicalFund=1 and " _
                              & " unemployFund=0 and retiredFund=1 and sretiredFund=0 and u.leavedate is null "
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;一金半',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID") & " and houseFund=0 and medicalFund=0 and " _
                              & " unemployFund=1 and retiredFund=0 and sretiredFund=1 and u.leavedate is null "
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;一金',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID") & " and houseFund=0 and medicalFund=0 and " _
                              & " unemployFund=1 and retiredFund=0 and sretiredFund=0 and u.leavedate is null "
                        i = i + 1
                    End While
                    query &= " order by exp1 "
                Case 15
                    query = " SELECT isLabourUnion, COUNT(userID) FROM tcpc0.dbo.users "
                    query &= " WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 and isTemp='" & Session("temp") & "' AND organizationID = " & Session("orgID")
                    query &= " AND leaveDate IS NULL "
                    query &= " GROUP BY isLabourUnion ORDER BY isLabourUnion "
            End Select

            'Response.Write(query)

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim ii As Integer = 0
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim t1 As Integer = 0
                    Dim t2 As Integer = 0
                    Dim t3 As Integer = 0
                    Dim t4 As Integer = 0
                    Dim t5 As Integer = 0
                    Dim t6 As Integer = 0
                    Dim t7 As Integer = 0
                    If ind = 15 Then
                        For ii = 0 To .Rows.Count - 1
                            If .Rows(ii).Item(0).ToString() = "True" Then
                                PIMasteryRow("工会会员", .Rows(ii).Item(1).ToString().Trim())
                            ElseIf .Rows(ii).Item(0).ToString() = "False" Then
                                PIMasteryRow("非工会会员", .Rows(ii).Item(1).ToString().Trim())
                            Else
                                PIMasteryRow("--", .Rows(ii).Item(1).ToString().Trim())
                            End If
                        Next
                    ElseIf ind = 14 Then
                        For ii = 0 To .Rows.Count - 1
                            PIMasteryRow(.Rows(ii).Item(0).ToString().Trim(), .Rows(ii).Item(1).ToString().Trim())
                        Next
                    ElseIf ind = 13 Then
                        For ii = 0 To .Rows.Count - 1
                            If .Rows(ii).Item(0).ToString().Trim().Length > 0 Then
                                PIMasteryRow(.Rows(ii).Item(0).ToString().Trim(), .Rows(ii).Item(1).ToString().Trim())
                            End If
                        Next
                    ElseIf ind <> 3 Then
                        Dim j As Integer = 0
                        For ii = 0 To .Rows.Count - 1
                            If .Rows(ii).Item(0).ToString().Trim().Length > 0 Then
                                j = j + .Rows(ii).Item(1)
                            End If
                        Next
                        PIMasteryRow("--", (total - j).ToString())
                        For ii = 0 To .Rows.Count - 1
                            If .Rows(ii).Item(0).ToString().Trim().Length > 0 Then
                                PIMasteryRow(.Rows(ii).Item(0).ToString().Trim(), .Rows(ii).Item(1).ToString().Trim())
                            End If
                        Next
                    ElseIf ind = 3 Then
                        total = 0
                        other = 0
                        For ii = 0 To .Rows.Count - 1
                            If .Rows(ii).IsNull(0) = False Then
                                If .Rows(ii).Item(0) <= 20 Then
                                    t2 = t2 + 1
                                End If
                                If .Rows(ii).Item(0) > 20 And .Rows(ii).Item(0) <= 30 Then
                                    t3 = t3 + 1
                                End If
                                If .Rows(ii).Item(0) > 30 And .Rows(ii).Item(0) <= 40 Then
                                    t4 = t4 + 1
                                End If
                                If .Rows(ii).Item(0) > 40 And .Rows(ii).Item(0) <= 50 Then
                                    t5 = t5 + 1
                                End If
                                If .Rows(ii).Item(0) > 50 And .Rows(ii).Item(0) <= 60 Then
                                    t6 = t6 + 1
                                End If
                                If .Rows(ii).Item(0) > 60 Then
                                    t7 = t7 + 1
                                End If
                            Else
                                t1 = t1 + 1
                            End If
                        Next

                        total = t1 + t2 + t3 + t4 + t5 + t6 + t7

                        PIMasteryRow("--", t1.ToString().Trim())
                        PIMasteryRow("20岁以下", t2.ToString().Trim())
                        PIMasteryRow("20—30岁", t3.ToString().Trim())
                        PIMasteryRow("30—40岁", t4.ToString().Trim())
                        PIMasteryRow("40—50岁", t5.ToString().Trim())
                        PIMasteryRow("50—60岁", t6.ToString().Trim())
                        PIMasteryRow("60岁以上", t7.ToString().Trim())
                    End If
                End If
            End With
            ds.Reset()

            While (ii < 40)
                PIMasteryRow("", "")
                ii = ii + 1
            End While
            PIMasteryRow("", "")
            PIMasteryRow("", "")
            PIMasteryRow("", "")
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=personnelstatistac.xls")
            Response.Flush()
        End If
    End Sub
    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String)
        row = New TableRow
        If str0 = "<b>统计项目</b>" Or str0 = "<b>统计项目名称:</b>" Then
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
        cell.Width = New Unit(300)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str1 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str1.Trim()
        End If
        cell.Width = New Unit(100)
        row.Cells.Add(cell)

        Dim i As Integer = 0
        While (i < 26)
            cell = New TableCell
            cell.Text = ""
            cell.Width = New Unit(60)
            row.Cells.Add(cell)
            i = i + 1
        End While

        exl.Rows.Add(row)
    End Sub

End Class

End Namespace
