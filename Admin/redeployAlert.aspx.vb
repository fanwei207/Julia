Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient


Namespace tcpc

    Partial Class redeployAlert
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
            strSql = " SELECT ued.ExchangeID,DATEADD(Month, 3,ued.ExchangeDate) as NumDate,u.userID,u.userNo,u.userName,ISNULL(dpt.name,'') as dptName,ued.ExchangeDate " _
                    & " FROM User_Exchange_Department ued " _
                    & " INNER JOIN tcpc0.dbo.users u ON u.userID=ued.userID " _
                    & " LEFT JOIN departments dpt ON ued.departmentID=dpt.departmentID " _
                    & " WHERE (ued.ExchangeID IN (SELECT max(ExchangeID) FROM User_Exchange_Department GROUP BY UserID)) AND (ued.isCompleted is null or ued.isCompleted=0)   " _
                    & " AND DATEADD(Month, 3,ued.ExchangeDate)<=getdate() AND u.deleted=0 AND u.isActive=1 AND ((dpt.isManufacture is null) or (dpt.isManufacture=0)) "
            strSql &= " and u.plantCode='" & Session("PlantCode") & "' and u.isTemp='" & Session("temp") & "'"
            If txb_workNo.Text.Trim.Length > 0 Then
                strSql &= "AND cast(u.userNo as varchar)='" & chk.sqlEncode(txb_workNo.Text.Trim) & "'"
            End If
            If txb_userName.Text.Trim.Length > 0 Then
                strSql &= "AND u.userName like N'%" & chk.sqlEncode(txb_userName.Text.Trim) & "%' "
            End If
            If txb_department.Text.Trim.Length > 0 Then
                strSql &= "AND dpt.name like N'%" & chk.sqlEncode(txb_department.Text.Trim) & "%' "
            End If
            If txb_ExchangeDateFrom.Text.Trim.Length > 0 Then
                strSql &= "AND ued.ExchangeDate>='" & CDate(txb_ExchangeDateFrom.Text.Trim) & "'"
            End If

            If txb_ExchangeDateTo.Text.Trim.Length > 0 Then
                strSql &= "AND ued.ExchangeDate<='" & CDate(txb_ExchangeDateTo.Text.Trim) & "'"
            End If
            strSql &= " ORDER BY u.userID,u.userName,dpt.name,ued.ExchangeDate "
            createSQL = strSql
        End Function

        Sub BindData()
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())
            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int64")))
            dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ExchangeID", System.Type.GetType("System.Int64")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("dptName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ExchangeDate", System.Type.GetType("System.DateTime")))

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr = dt.NewRow()
                        dr.Item("userID") = .Rows(i).Item("userID")
                        dr.Item("userNo") = .Rows(i).Item("userNo")
                        dr.Item("ExchangeID") = .Rows(i).Item("ExchangeID")
                        dr.Item("userName") = .Rows(i).Item("userName").ToString().Trim()
                        dr.Item("dptName") = .Rows(i).Item("dptName").ToString().Trim()
                        dr.Item("ExchangeDate") = .Rows(i).Item("ExchangeDate")
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

            If txb_ExchangeDateFrom.Text.Trim.Length > 0 Then
                If Not IsDate(txb_ExchangeDateFrom.Text.Trim) Then
                    ltlAlert.Text = "alert('调动日期范围的最小值应该输入一个日期.');"
                    Exit Sub
                End If
            End If

            If txb_ExchangeDateTo.Text.Trim.Length > 0 Then
                If Not IsDate(txb_ExchangeDateTo.Text.Trim) Then
                    ltlAlert.Text = "alert('调动日期范围的最大值应该输入一个日期.');"
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

        Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgList.ItemCreated

        End Sub

        Private Sub dgList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgList.ItemCommand
            If e.CommandName = "close" Then
                strSql = "  UPDATE User_Exchange_Department SET isCompleted=1 WHERE ExchangeID='" & e.Item.Cells(0).Text.Trim & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                BindData()
            End If
        End Sub

        Protected Sub ButExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButExcel.Click
            Dim EXTitle As String = "<b>工号</b>~^<b>姓名</b>~^300^<b>部门</b>~^<b>调动日期</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsnx, EXTitle, ExSql, False)

        End Sub
    End Class

End Namespace
