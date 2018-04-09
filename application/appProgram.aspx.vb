Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class appProgram
        Inherits BasePage
    Public strSql As String
    Public chk As New adamClass
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
        If Not IsPostBack Then
            'check  

            If Request("v") <> Nothing Then
                selectTypeDDList.SelectedValue = Request("v")
                Select Case Request("c")
                    Case 0
                        applicationRB.Checked = True
                        confirmRB.Checked = False
                        approveRB.Checked = False
                        doneRB.Checked = False
                        allRB.Checked = False
                    Case 1
                        applicationRB.Checked = False
                        confirmRB.Checked = True
                        approveRB.Checked = False
                        doneRB.Checked = False
                        allRB.Checked = False
                    Case 2
                        applicationRB.Checked = False
                        confirmRB.Checked = False
                        approveRB.Checked = True
                        doneRB.Checked = False
                        allRB.Checked = False
                    Case 3
                        applicationRB.Checked = False
                        confirmRB.Checked = False
                        approveRB.Checked = False
                        doneRB.Checked = True
                        allRB.Checked = False
                    Case Else
                        applicationRB.Checked = False
                        confirmRB.Checked = False
                        approveRB.Checked = False
                        doneRB.Checked = False
                        allRB.Checked = True
                End Select
            End If
            If selectTypeDDList.SelectedValue <> 0 Then
                confirmRB.Visible = True
                approveRB.Visible = True
            Else
                If confirmRB.Checked Or approveRB.Checked Then
                    applicationRB.Checked = True
                    confirmRB.Checked = False
                    approveRB.Checked = False
                End If
                confirmRB.Visible = False
                approveRB.Visible = False
            End If
            applicationList()
        End If
    End Sub
    Sub applicationList()
        strSql = " SELECT al.id as appID,al.type,createdBy,ISNULL(confirmBy,0) as confirmBy," _
                & " ISNULL(approveBy,0) as approveBy,ISNULL(finishedBy,0) as finishedBy," _
                & " dbo.calculateAppCategory(al.type),al.status as appStatus,createdDate," _
                & " u.userName,ISNULL(dpt.name,'') as dptName,ISNULL(reason,'') as reason," _
                & " ISNULL(description,''),ISNULL(uc.userName,''),ISNULL(cast(confirmDate as varchar),'')," _
                & " ISNULL(ua.userName,''),ISNULL(cast(approveDate as varchar),''),ISNULL(approveDesc,'')," _
                & " ISNULL(uf.userName,''),ISNULL(cast(finishedDate as varchar),''),ISNULL(result,'') " _
                & " FROM Applications al " _
                & " INNER JOIN tcpc0.dbo.users u ON al.createdby=u.userID " _
                & " LEFT  JOIN tcpc0.dbo.users uc ON al.confirmBy=uc.userID " _
                & " LEFT  JOIN tcpc0.dbo.users ua ON al.approveBy=ua.userID " _
                & " LEFT  JOIN tcpc0.dbo.users uf ON al.finishedBy=uf.userID " _
                & " LEFT JOIN departments dpt ON u.departmentID=dpt.departmentID " _
                & " WHERE al.type='" & selectTypeDDList.SelectedValue & "'"

        If applicationRB.Checked Then
            strSql &= " AND al.status=N'已申请' " _
                    & " AND createdBy='" & Session("uID") & "'"
        End If
        If confirmRB.Checked Then
            If Session("uRole") = 1 Then
                strSql &= " AND al.status=N'已申请' "
            Else
                strSql &= " AND al.status=N'已申请' " _
                        & " AND createdBy In (SELECT u.userID From User_Department ud INNER JOIN tcpc0.dbo.users u ON ud.departmentID=u.departmentID WHERE ud.userID='" & Session("uID") & "')"
            End If
        End If
        If approveRB.Checked Then
            strSql &= " AND (al.status=N'已审核' or al.status=N'已批准' or al.status=N'未获批准')" 
        End If
        If doneRB.Checked Then
            If selectTypeDDList.SelectedValue <> 0 Then
                strSql &= " AND (al.status=N'已批准' or al.status=N'等待处理') "
            Else
                strSql &= " AND (al.status=N'已申请' or al.status=N'等待处理') "
            End If
        End If
            If allRB.Checked Then
                strSql &= " ORDER BY createdDate DESC,al.status "
            Else
                strSql &= " ORDER BY al.status,appID DESC"
            End If

            Session("EXSQL") = strSql
            Session("EXTitle") = "<b>类型</b>~^<b>当前状态</b>~^<b>申请日期</b>~^<b>申请人</b>~^<b>部门</b>~^<b>申请原因</b>~^<b>申请内容</b>~^<b>审核</b>~^<b>审核日期</b>~^<b>批准</b>~^<b>批准日期</b>~^<b>批准意见</b>~^<b>处理</b>~^<b>处理日期</b>~^<b>处理意见</b>~^"

            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("appID", System.Type.GetType("System.Int16")))
            dt.Columns.Add(New DataColumn("appDate", System.Type.GetType("System.DateTime")))
            dt.Columns.Add(New DataColumn("appby", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("appDept", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("appReason", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("OPStatus", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("AppStatus", System.Type.GetType("System.String")))

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("appID") = .Rows(i).Item("appID")
                        dr1.Item("appDate") = .Rows(i).Item("createdDate")
                        dr1.Item("appby") = .Rows(i).Item("userName")
                        dr1.Item("appDept") = .Rows(i).Item("dptName")
                        dr1.Item("appReason") = .Rows(i).Item("reason")
                        dr1.Item("AppStatus") = .Rows(i).Item("appStatus")

                        If applicationRB.Checked Then
                            dr1.Item("OPStatus") = "申请修改"
                        End If
                        If confirmRB.Checked Then
                            dr1.Item("OPStatus") = "审核"
                        End If
                        If approveRB.Checked Then
                            If .Rows(i).Item("appStatus") = "已审核" Or .Rows(i).Item("appStatus") = "已申请" Then
                                dr1.Item("OPStatus") = "批准"
                            Else
                                dr1.Item("OPStatus") = "批准修改"
                            End If
                        End If
                        If doneRB.Checked Then
                            If selectTypeDDList.SelectedValue <> 0 Then
                                If .Rows(i).Item("appStatus") = "已批准" Then
                                    dr1.Item("OPStatus") = "处理"
                                Else
                                    dr1.Item("OPStatus") = "处理修改"
                                End If
                            Else
                                If .Rows(i).Item("appStatus") = "已申请" Then
                                    dr1.Item("OPStatus") = "处理"
                                Else
                                    dr1.Item("OPStatus") = "处理修改"
                                End If
                            End If
                        End If
                        If allRB.Checked Then
                            dr1.Item("OPStatus") = "详细资料"
                        End If

                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)
            'If (Session("orderby").Length <= 0) Then
            '    Session("orderby") = "appID"
            'End If
            Try
                'dv.Sort = Session("orderby") & Session("orderdir")
                appList.DataSource = dv
                appList.DataBind()
            Catch
            End Try
    End Sub
        'Private Sub appList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles appList.SortCommand
        '    Session("orderby") = e.SortExpression.ToString()
        '    If Session("orderdir") = " ASC" Then
        '        Session("orderdir") = " DESC"
        '    Else
        '        Session("orderdir") = " ASC"
        '    End If
        '    applicationList()
        'End Sub
    Sub RadioButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        appList.CurrentPageIndex = 0
        applicationList()
    End Sub
    Private Sub selectTypeDDList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles selectTypeDDList.SelectedIndexChanged
        If selectTypeDDList.SelectedValue <> 0 Then
            confirmRB.Visible = True
            approveRB.Visible = True
        Else
            If confirmRB.Checked Or approveRB.Checked Then
                applicationRB.Checked = True
                confirmRB.Checked = False
                approveRB.Checked = False
            End If
            confirmRB.Visible = False
            approveRB.Visible = False
        End If
        applicationList()
    End Sub
    Private Sub appList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles appList.PageIndexChanged
        appList.CurrentPageIndex = e.NewPageIndex
        applicationList()
    End Sub
    Private Sub appList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles appList.ItemCommand
        '0-apply actions such as confirm,approve,done.
        '1-modify.
        '2-view
        Dim actionType As Integer
        If e.CommandName = "Detail" Then
            If CType(e.Item.Cells(6).Controls(0), LinkButton).Text = "详细资料" Then
                actionType = 2
            Else
                If CType(e.Item.Cells(6).Controls(0), LinkButton).Text.IndexOf("修改") <> -1 Then
                    actionType = 1
                Else
                    actionType = 0
                End If
            End If
            GetAccess(e.Item.Cells(0).Text, actionType)
        End If
    End Sub
    Sub GetAccess(ByVal StrAppID As Integer, ByVal StrAct As Integer)
        Select Case selectTypeDDList.SelectedValue
            Case 0 '程序错误
                accessCheck(StrAppID, StrAct, "", "", 16000013)
            Case 1 '更新电脑数据
                accessCheck(StrAppID, StrAct, 16000004, 16000005, 16000006)
            Case 2 '程序修改
                accessCheck(StrAppID, StrAct, 16000010, 16000011, 16000012)
            Case 3 '新增程序
                accessCheck(StrAppID, StrAct, 16000007, 16000008, 16000009)
        End Select
    End Sub
    Sub accessCheck(ByVal appID As Integer, ByVal actionType As Integer, ByVal pageNum1 As String, ByVal pageNum2 As String, ByVal pageNum3 As String)
        Dim confirm, approve, finish As Boolean
        'can confirm 
        If chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), pageNum1, True) > 0 Then
            confirm = True
        Else
            confirm = False
        End If
        'can approve
        If chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), pageNum2, True) > 0 Then
            approve = True
        Else
            approve = False
        End If
        'can finish
        If chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), pageNum3, True) > 0 Then
            finish = True
        Else
            finish = False
        End If

        If Session("uRole") = 1 Then
            confirm = True
            approve = True
            finish = True
        End If


        '0-done,1-modify,2-view

        If applicationRB.Checked Then
            Response.Redirect("/application/appDetail.aspx?id=" & appID & "&act=1" & "&v=" & selectTypeDDList.SelectedValue & "&c=0")

        ElseIf confirmRB.Checked Then
            If confirm Then
                Response.Redirect("/application/appDetail.aspx?id=" & appID & "&act=0" & "&v=" & selectTypeDDList.SelectedValue & "&c=1")
            Else
                ltlAlert.Text = "alert('您没有审核的权限。')"
            End If

        ElseIf approveRB.Checked Then
            If approve Then
                Response.Redirect("/application/appDetail.aspx?id=" & appID & "&act=" & actionType & "&v=" & selectTypeDDList.SelectedValue & "&c=2")
            Else
                ltlAlert.Text = "alert('您没有批准的权限。')"
            End If

        ElseIf doneRB.Checked Then
            If finish Then
                Response.Redirect("/application/appDetail.aspx?id=" & appID & "&act=" & actionType & "&v=" & selectTypeDDList.SelectedValue & "&c=3")
            Else
                ltlAlert.Text = "alert('您没有处理的权限。')"
            End If
        ElseIf allRB.Checked Then
            Response.Redirect("/application/appDetail.aspx?id=" & appID & "&act=" & actionType & "&v=" & selectTypeDDList.SelectedValue & "&c=4")
        End If
    End Sub

    Private Sub newApp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles newApp.Click
            Response.Redirect("application.aspx")
    End Sub
End Class

End Namespace
