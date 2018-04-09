Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient


Namespace tcpc

    Partial Class pactToEndAlert
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim strSql As String
        Dim reader As SqlDataReader
        Dim ds As DataSet
        Shared sortOrder As String = ""
        Shared sortDir As String = "ASC"
        'Protected WithEvents dropDept As System.Web.UI.WebControls.DropDownList
        'Protected WithEvents dropType As System.Web.UI.WebControls.DropDownList

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            If Not IsPostBack() Then
                dropDept.Items.Clear()
                dropType.Items.Clear()
                Dim item As ListItem
                item = New ListItem("--")
                item.Value = 0
                dropDept.Items.Add(item)
                strSql = "SELECT departmentID,Name From Departments WHERE isSalary='1' order by departmentID"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
                While reader.Read()
                    item = New ListItem(reader(1))
                    item.Value = Convert.ToInt32(reader(0))
                    dropDept.Items.Add(item)
                End While
                reader.Close()
                dropDept.SelectedIndex = 0
                dropType.Items.Add(New ListItem("合同有效期", 0))
                dropType.Items.Add(New ListItem("身份证有效期", 1))
                dropType.Items.Add(New ListItem("退休人员到期", 2))
                dropType.SelectedIndex = 0


                BindData()
            End If
        End Sub
        Function createSQL() As String
            Select Case dropType.SelectedValue
                Case 0
                    strSql = " SELECT DATEDIFF(day, u.contractDate, GETDATE()) as NumDate,u.userID,u.userNo,u.userName,ISNULL(dpt.name,'') as dptName,isnull(s.systemcodename,'') as type,u.enterDate, u.contractDate as retireDate "
                Case 1
                    strSql = " SELECT DATEDIFF(day, u.IDeffectivedate, GETDATE()) as NumDate,u.userID,u.userNo,u.userName,ISNULL(dpt.name,'') as dptName,isnull(u.IC,'') as type, u.IDeffectivedate as retireDate "
                Case 2
                    strSql = " SELECT  Case u.sexID  when  14 then  DATEDIFF(day, DATEAdd(year, 50, u.birthday), GETDATE()) else DATEDIFF(day, DATEAdd(year, 50, u.birthday), GETDATE()) end as NumDate," _
                           & " u.userID,u.userNo,u.userName,ISNULL(dpt.name,'') as dptName, " _
                           & "( Convert( nvarchar(10),u.birthday,126) + ' ,' + Case u.sexID  when  14 then N'女' else N'男' end )as type, Case u.sexID  when  14 then DATEAdd(year, 50, u.birthday) else DATEAdd(year, 60, u.birthday) End as retireDate "
            End Select
            strSql &= " FROM tcpc0.dbo.users u " _
                            & " LEFT JOIN departments dpt ON u.departmentID=dpt.departmentID " _
                            & " INNER JOIN tcpc0.dbo.systemcode s on s.systemcodeID=u.contractTypeID " _
                            & " WHERE u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 AND u.isActive=1  AND (leaveDate='' OR leaveDate is null) "
            'strSql &= " and isTemp='" & Session("temp") & "'"


            Select Case dropType.SelectedValue
                Case 0
                    strSql &= " AND u.contractDate IS NOT NULL AND (DATEDIFF(day, u.contractDate, GETDATE()) >= - 180) "
                Case 1
                    strSql &= " AND u.IDeffectivedate IS NOT NULL AND (DATEDIFF(day, u.IDeffectivedate, GETDATE()) >= - 45) "
                Case 2
                    strSql &= " AND u.birthday IS NOT NULL AND " _
                            & " ((DATEDIFF(year, u.birthday, GETDATE()) >= 50 and u.sexID = 14) Or (DATEDIFF(year, u.birthday, GETDATE()) >= 60 and u.sexID = 13) ) "
            End Select



            If txb_workNo.Text.Trim.Length > 0 Then
                strSql &= "AND cast(u.userNo as varchar)='" & chk.sqlEncode(txb_workNo.Text.Trim) & "'"
            End If
            If txb_userName.Text.Trim.Length > 0 Then
                strSql &= "AND u.userName like N'%" & chk.sqlEncode(txb_userName.Text.Trim) & "%' "
            End If
            If dropDept.SelectedIndex > 0 Then
                strSql &= "AND u.departmentID ='" & dropDept.SelectedValue & "' "
            End If


            Select Case dropType.SelectedValue
                Case 0
                    If txb_ContractDateFrom.Text.Trim.Length > 0 Then
                        strSql &= "AND u.contractDate>='" & CDate(txb_ContractDateFrom.Text.Trim) & "'"
                    End If

                    If txb_ContractDateTo.Text.Trim.Length > 0 Then
                        strSql &= "AND u.contractDate<='" & CDate(txb_ContractDateFrom.Text.Trim) & "'"
                    End If
                    strSql &= " ORDER BY u.contractDate,u.userID,u.userName,dpt.name"
                Case 1
                    If txb_ContractDateFrom.Text.Trim.Length > 0 Then
                        strSql &= "AND u.IDeffectivedate>='" & CDate(txb_ContractDateFrom.Text.Trim) & "'"
                    End If

                    If txb_ContractDateTo.Text.Trim.Length > 0 Then
                        strSql &= "AND u.IDeffectivedate<='" & CDate(txb_ContractDateFrom.Text.Trim) & "'"
                    End If
                    strSql &= " ORDER BY u.IDeffectivedate,u.userID,u.userName,dpt.name"
                Case 2
                    If txb_ContractDateFrom.Text.Trim.Length > 0 Then
                        strSql &= "AND u.birthday>='" & CDate(txb_ContractDateFrom.Text.Trim) & "'"
                    End If

                    If txb_ContractDateTo.Text.Trim.Length > 0 Then
                        strSql &= "AND u.birthday<='" & CDate(txb_ContractDateFrom.Text.Trim) & "'"
                    End If
                    strSql &= " ORDER BY u.contractDate,u.userID,u.userName,dpt.name"
            End Select
            createSQL = strSql
        End Function

        Sub BindData()
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())
            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int64")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("dptName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("strContractDate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("contractDate", System.Type.GetType("System.DateTime")))
            dt.Columns.Add(New DataColumn("NumDate", System.Type.GetType("System.Int32")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr = dt.NewRow()
                        dr.Item("userID") = .Rows(i).Item("userID")
                        dr.Item("userNo") = .Rows(i).Item("userNo")
                        dr.Item("userName") = .Rows(i).Item("userName").ToString().Trim()
                        dr.Item("dptName") = .Rows(i).Item("dptName").ToString().Trim()
                        dr.Item("type") = .Rows(i).Item("type").ToString().Trim()
                        If CInt(.Rows(i).Item("NumDate")) <= 0 Then
                            dr.Item("strContractDate") = "<font color=#009933>" & Format(.Rows(i).Item("retireDate"), "yyyy-MM-dd") & "</font>"
                        Else
                            dr.Item("strContractDate") = "<font color=#ff0033>" & Format(.Rows(i).Item("retireDate"), "yyyy-MM-dd") & "</font>"
                        End If
                        dr.Item("contractDate") = .Rows(i).Item("retireDate")
                        dr.Item("NumDate") = -CInt(.Rows(i).Item("NumDate"))
                        dt.Rows.Add(dr)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                dgList.DataSource = dv
                dgList.DataBind()
            Catch
            End Try
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

            If txb_ContractDateFrom.Text.Trim.Length > 0 Then
                If Not IsDate(txb_ContractDateFrom.Text.Trim) Then
                    ltlAlert.Text = "alert('日期范围的最小值应该输入一个日期.');"
                    Exit Sub
                End If
            End If

            If txb_ContractDateTo.Text.Trim.Length > 0 Then
                If Not IsDate(txb_ContractDateTo.Text.Trim) Then
                    ltlAlert.Text = "alert('日期范围的最大值应该输入一个日期.');"
                    Exit Sub
                End If
            End If

            dgList.CurrentPageIndex = 0
            BindData()
        End Sub

        Private Sub dgList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
            dgList.EditItemIndex = -1
            dgList.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub dgList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgList.ItemCreated

            Select Case dropType.SelectedValue
                Case 0
                    dgList.Columns(3).HeaderText = "用工性质"
                    dgList.Columns(4).HeaderText = "合同日期"
                Case 1
                    dgList.Columns(3).HeaderText = "身份证"
                    dgList.Columns(4).HeaderText = "有效期"
                Case 2
                    dgList.Columns(3).HeaderText = "生日日期"
                    dgList.Columns(4).HeaderText = "退休日期"
            End Select
        End Sub
        Protected Sub ButExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButExcel.Click
            Dim EXTitle As String = ""
            Select Case dropType.SelectedValue
                Case 0
                    EXTitle = "<b>工号</b>~^<b>姓名</b>~^250^<b>部门</b>~^<b>用工性质</b>~^<b>入公司日期</b>~^<b>合同日期</b>~^"
                Case 1
                    EXTitle = "<b>工号</b>~^<b>姓名</b>~^250^<b>部门</b>~^<b>身份证</b>~^<b>身份证有效期</b>~^"
                Case 2
                    EXTitle = "<b>工号</b>~^<b>姓名</b>~^250^<b>部门</b>~^<b>生日日期</b>~^<b>退休日期</b>~^"
            End Select
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsnx, EXTitle, ExSql, False)

        End Sub
    End Class

End Namespace
