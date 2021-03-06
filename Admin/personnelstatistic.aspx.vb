Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class personneltj
        Inherits BasePage
        Dim chk As New adamClass
        Dim ageHashTable As New Hashtable
        Dim item As ListItem
        Shared sortOrder As String = ""
        Shared sortDir As String = "ASC"
        'Protected WithEvents ltlAlert As Literal


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
            ltlAlert.Text = ""
            If Not IsPostBack Then
                statisticdropdownlist()
                BindData()
            End If
        End Sub
        Private Sub BindData()
            Dim query, strsql As String
            Dim ds As DataSet
            Dim total As Integer = 0
            Dim other As Integer = 0

            query = " SELECT COUNT(*) FROM tcpc0.dbo.users WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND organizationID = 1 AND roleID > 1  "
            'If Session("conceal") = 0 Then
            '    query &= " and isTemp='" & Session("temp") & "' "
            'End If

            If statistic.SelectedIndex <> 13 Then
                query &= " and leavedate is null "
            Else
                query &= " and leavedate is not null "
            End If
            total = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, query)

            Select Case statistic.SelectedIndex
                Case 0
                    query = "  SELECT isnull(d.name,''), COUNT(u.userID) "
                    query &= " FROM Departments d "
                    'query &= " left OUTER JOIN tcpc0.dbo.users u ON d.departmentID = u.departmentID AND u.plantCode='" & Session("PlantCode") & "' and u.deleted = 0 AND u.roleID > 1 and isTemp='" & Session("temp") & "'"
                    query &= " left OUTER JOIN tcpc0.dbo.users u ON d.departmentID = u.departmentID AND u.plantCode='" & Session("PlantCode") & "' and u.deleted = 0 AND u.roleID > 1 "
                    query &= " AND u.organizationID =" & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " WHERE d.isSalary='1' "
                    query &= " GROUP BY d.name "

                Case 1
                    query = " SELECT ISNULL(r.roleName, ''), COUNT(u.userID) FROM Roles r "
                    'query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.roleID = r.roleID AND u.plantCode='" & Session("PlantCode") & "' and u.deleted = 0 AND u.roleID > 1 AND u.organizationID =" & Session("orgID") & " and isTemp='" & Session("temp") & "'"
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.roleID = r.roleID AND u.plantCode='" & Session("PlantCode") & "' and u.deleted = 0 AND u.roleID > 1 AND u.organizationID =" & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " GROUP BY r.rolename "

                Case 2
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Education' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.educationID = sc.systemCodeID "
                    'query &= " and u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID")
                    query &= " and u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " GROUP BY sc.systemCodeName "

                Case 3
                    query = " SELECT datediff(year,birthday,getdate())"
                    query &= " From tcpc0.dbo.users u "
                    'query &= " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1 and isTemp='" & Session("temp") & "' and u.organizationID=" & Session("orgID")
                    query &= " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " order by datediff(year,birthday,getdate())"

                Case 4
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Contract Type' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.contractTypeID = sc.systemCodeID "
                    'query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 AND u.organizationID = " & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " GROUP BY sc.systemCodeName "

                Case 5
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Occupation' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.occupationID = sc.systemCodeID "
                    'query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1  AND u.organizationID = " & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " GROUP BY sc.systemCodeName "

                Case 6
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Sex' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.sexID = sc.systemCodeID "
                    'query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1  AND u.organizationID = " & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " GROUP BY sc.systemCodeName "

                Case 7
                    query = " SELECT ISNULL(ws.name, ''), COUNT(u.userID) FROM Workshop ws "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON ws.id = u.workshopID AND u.deleted = 0 AND u.roleID > 1 "
                    'query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.deleted = 0 AND u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.deleted = 0 AND u.roleID > 1  AND u.organizationID = " & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " GROUP BY ws.name "

                Case 8
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Province' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.provinceID = sc.systemCodeID "
                    'query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 AND u.organizationID = " & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " GROUP BY sc.systemCodeName "

                Case 9
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Marriage' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.marriageID = sc.systemCodeID "
                    'query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1  AND u.organizationID = " & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " GROUP BY sc.systemCodeName "

                Case 10
                    query = " SELECT ISNULL(sc.systemCodeName, ''), COUNT(u.userID) FROM tcpc0.dbo.systemCode sc "
                    query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Work Type' "
                    query &= " LEFT OUTER JOIN tcpc0.dbo.users u ON u.workTypeID = sc.systemCodeID "
                    'query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1 and isTemp='" & Session("temp") & "' AND u.organizationID = " & Session("orgID")
                    query &= " AND u.plantCode='" & Session("PlantCode") & "' and u.roleID > 1  AND u.organizationID = " & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " and u.leavedate is null "
                    End If
                    query &= " GROUP BY sc.systemCodeName "

                Case 11
                    query = " SELECT YEAR(enterDate), COUNT(userID) FROM tcpc0.dbo.users "
                    'query &= " WHERE u.plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 and isTemp='" & Session("temp") & "' AND enterDate IS NOT NULL AND organizationID =" & Session("orgID")
                    query &= " WHERE u.plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 AND enterDate IS NOT NULL AND organizationID =" & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " AND leaveDate IS NULL "
                    End If
                    query &= " GROUP BY YEAR(enterDate) ORDER BY YEAR(enterDate) DESC "

                Case 12
                    query = " SELECT YEAR(employDate), COUNT(userID) FROM tcpc0.dbo.users "
                    'query &= " WHERE u.plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 and isTemp='" & Session("temp") & "' AND employDate IS NOT NULL AND organizationID =" & Session("orgID")
                    query &= " WHERE u.plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 AND employDate IS NOT NULL AND organizationID =" & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " AND leaveDate IS NULL "
                    End If
                    query &= " GROUP BY YEAR(employDate) ORDER BY YEAR(employDate) DESC "

                Case 13
                    query = " SELECT YEAR(leaveDate), COUNT(userID) FROM tcpc0.dbo.users "
                    'query &= " WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 and isTemp='" & Session("temp") & "' AND organizationID =" & Session("orgID")
                    query &= " WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1  AND organizationID =" & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " AND leaveDate IS not NULL "
                    End If
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
                                  & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID") _
                                  & " and ( retiredFund=0 and houseFund=0 and medicalFund=0 and unemployFund=0 and sretiredFund=0) "
                            If chkall.Checked = False Then
                                query &= " and u.leavedate is null "
                            End If
                        End If
                        i = i + 1
                        query &= " Union SELECT N'" & reader(0) & "',count(userID)," & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID") _
                              & " and ( retiredFund=1 or houseFund=1 or  medicalFund=1 or unemployFund=1 or sretiredFund=1)"
                        If chkall.Checked = False Then
                            query &= " and u.leavedate is null "
                        End If
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;期中：', null , " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID") _
                              & " and (retiredFund=0 and houseFund=0 and medicalFund=0 and unemployFund=0 and sretiredFund=0) "
                        If chkall.Checked = False Then
                            query &= " and u.leavedate is null "
                        End If
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;四金',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID") & " and houseFund=1 and medicalFund=1 and unemployFund=1 and retiredFund=1 and sretiredFund=0 "
                        If chkall.Checked = False Then
                            query &= " and u.leavedate is null "
                        End If
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;三金',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID") & " and houseFund=0 and medicalFund=1 and unemployFund=1 and retiredFund=1 and sretiredFund=0 "
                        If chkall.Checked = False Then
                            query &= " and u.leavedate is null "
                        End If
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;二金',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID") & " and houseFund=0 and medicalFund=1 and unemployFund=0 and retiredFund=1 and sretiredFund=0 "
                        If chkall.Checked = False Then
                            query &= " and u.leavedate is null "
                        End If
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;一金半',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID") & " and houseFund=0 and medicalFund=0 and unemployFund=1 and retiredFund=0 and sretiredFund=1 "
                        If chkall.Checked = False Then
                            query &= " and u.leavedate is null "
                        End If
                        i = i + 1
                        query &= " Union SELECT N'&nbsp;&nbsp;&nbsp;&nbsp;一金',count(userID), " & i & " as exp1 From tcpc0.dbo.users u " _
                              & " INNER JOIN tcpc0.dbo.systemCode sc ON u.insuranceTypeID = sc.systemCodeID AND sc.systemCodeName =N'" & reader(0) & "'" _
                              & " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Insurance Type' " _
                              & " Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.roleID>1  and u.organizationID=" & Session("orgID") & " and houseFund=0 and medicalFund=0 and unemployFund=1 and retiredFund=0 and sretiredFund=0 "
                        If chkall.Checked = False Then
                            query &= " and u.leavedate is null "
                        End If
                        i = i + 1
                    End While
                    query &= " order by exp1 "

                Case 15
                    query = " SELECT isLabourUnion, COUNT(userID) FROM tcpc0.dbo.users "
                    'query &= " WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1 and isTemp='" & Session("temp") & "' AND organizationID = " & Session("orgID")
                    query &= " WHERE plantCode='" & Session("PlantCode") & "' and deleted = 0 AND roleID > 1  AND organizationID = " & Session("orgID")
                    If chkall.Checked = False Then
                        query &= " AND leaveDate IS NULL "
                    End If
                    query &= " GROUP BY isLabourUnion ORDER BY isLabourUnion "

            End Select

            'Response.Write(query)

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("part", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("num", System.Type.GetType("System.Int32")))


            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim t1 As Integer = 0
                    Dim t2 As Integer = 0
                    Dim t3 As Integer = 0
                    Dim t4 As Integer = 0
                    Dim t5 As Integer = 0
                    Dim t6 As Integer = 0
                    Dim t7 As Integer = 0
                    Dim dr1 As DataRow
                    If statistic.SelectedValue = 15 Then
                        For i = 0 To .Rows.Count - 1
                            dr1 = dt.NewRow()
                            dr1.Item("gsort") = i + 1
                            If .Rows(i).Item(0).ToString() = "True" Then
                                dr1.Item("part") = "工会会员"
                            ElseIf .Rows(i).Item(0).ToString() = "False" Then
                                dr1.Item("part") = "非工会会员"
                            Else
                                dr1.Item("part") = "--"
                            End If
                            If Not .Rows(i).IsNull(1) Then
                                dr1.Item("num") = .Rows(i).Item(1).ToString().Trim()
                                total = total + .Rows(i).Item(1)
                            End If
                            dt.Rows.Add(dr1)
                        Next
                    ElseIf statistic.SelectedValue = 14 Then
                        For i = 0 To .Rows.Count - 1
                            dr1 = dt.NewRow()
                            dr1.Item("gsort") = i + 1
                            dr1.Item("part") = .Rows(i).Item(0).ToString().Trim()
                            If Not .Rows(i).IsNull(1) Then
                                dr1.Item("num") = .Rows(i).Item(1).ToString().Trim()
                            End If
                            dt.Rows.Add(dr1)
                        Next
                    ElseIf statistic.SelectedValue = 13 Then
                        For i = 0 To .Rows.Count - 1
                            If .Rows(i).Item(0).ToString().Trim().Length > 0 Then
                                dr1 = dt.NewRow()
                                dr1.Item("gsort") = i + 1
                                dr1.Item("part") = .Rows(i).Item(0).ToString().Trim()
                                dr1.Item("num") = .Rows(i).Item(1).ToString().Trim()
                                dt.Rows.Add(dr1)
                            End If
                        Next
                    ElseIf statistic.SelectedValue <> 3 Then
                        Dim j As Integer = 2
                        For i = 0 To .Rows.Count - 1
                            If .Rows(i).Item(0).ToString().Trim().Length > 0 Then
                                dr1 = dt.NewRow()
                                dr1.Item("gsort") = j
                                dr1.Item("part") = .Rows(i).Item(0).ToString().Trim()
                                If i = 0 Then
                                    other = total - CInt(.Rows(i).Item(1))
                                Else
                                    other = other - CInt(.Rows(i).Item(1))
                                End If
                                dr1.Item("num") = .Rows(i).Item(1).ToString().Trim()
                                dt.Rows.Add(dr1)
                                j = j + 1
                            End If
                        Next
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = 1
                        dr1.Item("part") = "--"
                        dr1.Item("num") = other.ToString().Trim()
                        dt.Rows.Add(dr1)

                    ElseIf statistic.SelectedValue = 3 Then
                        total = 0
                        other = 0
                        For i = 0 To .Rows.Count - 1
                            If .Rows(i).IsNull(0) = False Then
                                If .Rows(i).Item(0) <= 20 Then
                                    t2 = t2 + 1
                                End If
                                If .Rows(i).Item(0) > 20 And .Rows(i).Item(0) <= 30 Then
                                    t3 = t3 + 1
                                End If
                                If .Rows(i).Item(0) > 30 And .Rows(i).Item(0) <= 40 Then
                                    t4 = t4 + 1
                                End If
                                If .Rows(i).Item(0) > 40 And .Rows(i).Item(0) <= 50 Then
                                    t5 = t5 + 1
                                End If
                                If .Rows(i).Item(0) > 50 And .Rows(i).Item(0) <= 60 Then
                                    t6 = t6 + 1
                                End If
                                If .Rows(i).Item(0) > 60 Then
                                    t7 = t7 + 1
                                End If
                            Else
                                t1 = t1 + 1
                            End If
                        Next

                        total = t1 + t2 + t3 + t4 + t5 + t6 + t7

                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = 1
                        dr1.Item("part") = "--"
                        dr1.Item("num") = t1
                        dt.Rows.Add(dr1)

                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = 2
                        dr1.Item("part") = "20岁以下"
                        dr1.Item("num") = t2
                        dt.Rows.Add(dr1)

                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = 3
                        dr1.Item("part") = "20—30岁"
                        dr1.Item("num") = t3
                        dt.Rows.Add(dr1)

                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = 4
                        dr1.Item("part") = "30—40岁"
                        dr1.Item("num") = t4
                        dt.Rows.Add(dr1)

                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = 5
                        dr1.Item("part") = "40—50岁"
                        dr1.Item("num") = t5
                        dt.Rows.Add(dr1)

                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = 6
                        dr1.Item("part") = "50—60岁"
                        dr1.Item("num") = t6
                        dt.Rows.Add(dr1)

                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = 7
                        dr1.Item("part") = "60岁以上"
                        dr1.Item("num") = t7
                        dt.Rows.Add(dr1)

                    End If
                End If
            End With

            Label1.Text = "<b>人数: </b>" & total.ToString()

            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub
        Sub statisticdropdownlist()


            item = New ListItem("部门")
            item.Value = 0
            statistic.Items.Add(item)

            item = New ListItem("职务")
            item.Value = 1
            statistic.Items.Add(item)

            item = New ListItem("学历")
            item.Value = 2
            statistic.Items.Add(item)

            item = New ListItem("年龄")
            item.Value = 3
            statistic.Items.Add(item)

            item = New ListItem("合同类型")
            item.Value = 4
            statistic.Items.Add(item)

            item = New ListItem("职称")
            item.Value = 5
            statistic.Items.Add(item)

            item = New ListItem("性别")
            item.Value = 6
            statistic.Items.Add(item)

            item = New ListItem("工段")
            item.Value = 7
            statistic.Items.Add(item)

            item = New ListItem("户口所在省市")
            item.Value = 8
            statistic.Items.Add(item)

            item = New ListItem("婚姻状况")
            item.Value = 9
            statistic.Items.Add(item)

            item = New ListItem("计酬方式")
            item.Value = 10
            statistic.Items.Add(item)

            item = New ListItem("进入单位日期")
            item.Value = 11
            statistic.Items.Add(item)

            item = New ListItem("转正日期")
            item.Value = 12
            statistic.Items.Add(item)

            item = New ListItem("离开单位日期")
            item.Value = 13
            statistic.Items.Add(item)

            item = New ListItem("社会保险")
            item.Value = 14
            statistic.Items.Add(item)

            item = New ListItem("工会")
            item.Value = 15
            statistic.Items.Add(item)

            statistic.SelectedIndex = 0
        End Sub

        Private Sub statistic_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles statistic.SelectedIndexChanged
            BindData()
        End Sub

        Private Sub chkall_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkall.CheckedChanged
            BindData()
        End Sub
        Private Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click
            ltlAlert.Text = " var w=window.open('personnelstatisticPrint.aspx"
            ltlAlert.Text &= "?ind=" & statistic.SelectedIndex.ToString()
            ltlAlert.Text &= "&txt=" & statistic.SelectedItem.Text
            ltlAlert.Text &= "','','menubar=yes,scrollbars = yes,resizable=yes,width=700,height=500,top=0,left=0');w.focus(); "
        End Sub

        Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace
