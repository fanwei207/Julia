Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient


Namespace tcpc

    Partial Class probationAlert
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim strSql As String
        Dim reader As SqlDataReader
        Dim ds As DataSet

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
            If Not IsPostBack() Then
                BindData()
            End If
        End Sub

        Function createSQL() As String
            strSql = " SELECT  u.userID,u.userNo,u.userName,ISNULL(dpt.name,'') as dptName,u.enterDate,DATEADD(Month,3,u.enterDate) as endDate " _
                & " FROM tcpc0.dbo.users u " _
                & " LEFT JOIN departments dpt ON u.departmentID=dpt.departmentID " _
                & " WHERE u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 AND u.isActive=1 AND u.enterDate IS NOT NULL AND (leaveDate='' OR leaveDate is null) " _
         '& " AND DATEADD(Month,2,u.enterDate)<=GETDATE() AND DATEADD(Month,3,u.enterDate)>=GETDATE()  "
            ' & " WHERE u.deleted=0 AND u.isActive=1 AND u.enterDate IS NOT NULL AND employDate IS NULL  " _
            'employDate IS NULL indicate that the man not employ
            strSql &= " and u.isTemp='" & Session("temp") & "'"
            If txb_workNo.Text.Trim.Length > 0 Then
                strSql &= "AND cast(u.userNo as varchar)='" & chk.sqlEncode(txb_workNo.Text.Trim) & "'"
            End If
            If txb_userName.Text.Trim.Length > 0 Then
                strSql &= "AND u.userName like N'%" & chk.sqlEncode(txb_userName.Text.Trim) & "%' "
            End If
            If txb_department.Text.Trim.Length > 0 Then
                strSql &= "AND dpt.name like N'%" & chk.sqlEncode(txb_department.Text.Trim) & "%' "
            End If
            If txb_enterDateFrom.Text.Trim.Length = 0 And txb_enterDateTo.Text.Trim.Length = 0 Then
                strSql &= " AND DATEADD(Month,2,u.enterDate)<=GETDATE() AND DATEADD(Month,3,u.enterDate)>=GETDATE() "
            Else
                If txb_enterDateFrom.Text.Trim.Length > 0 Then
                    strSql &= "AND DATEADD(Month,3,u.enterDate)>='" & CDate(txb_enterDateFrom.Text.Trim) & "'"
                End If

                If txb_enterDateTo.Text.Trim.Length > 0 Then
                    strSql &= "AND DATEADD(Month,3,u.enterDate)<='" & CDate(txb_enterDateTo.Text.Trim) & "'"
                End If
            End If
            strSql &= " ORDER BY u.userID,u.userName,dpt.name,u.enterDate "
            createSQL = strSql
        End Function
        Sub BindData()

            'Response.Write(strSql)
            'Exit Sub
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int64")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("dptName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("enterDate", System.Type.GetType("System.DateTime")))
            dt.Columns.Add(New DataColumn("endDate", System.Type.GetType("System.DateTime")))

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
                        dr.Item("enterDate") = .Rows(i).Item("enterDate")
                        dr.Item("endDate") = .Rows(i).Item("endDate")
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

            If txb_enterDateFrom.Text.Trim.Length > 0 Then
                If Not IsDate(txb_enterDateFrom.Text.Trim) Then
                    ltlAlert.Text = "alert('转正到期范围的最小值应该输入一个日期.');"
                    Exit Sub
                End If
            End If

            If txb_enterDateTo.Text.Trim.Length > 0 Then
                If Not IsDate(txb_enterDateTo.Text.Trim) Then
                    ltlAlert.Text = "alert('转正到期范围的最大值应该输入一个日期.');"
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
        Protected Sub ButExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButExcel.Click
            Dim EXTitle As String = "<b>工号</b>~^<b>姓名</b>~^250^<b>部门</b>~^<b>进厂日期</b>~^<b>转正到期</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsnx, EXTitle, ExSql, False)

        End Sub
    End Class

End Namespace
