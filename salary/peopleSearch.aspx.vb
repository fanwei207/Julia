Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class peopleSearch
    Inherits System.Web.UI.Page
    Dim chk As New adamClass

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim reader As SqlDataReader
    Dim strSql As String
    Dim ds As DataSet

    'Protected WithEvents ltlAlert As Literal
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
            Session("orderby") = ""
            Session("orderdir") = ""
            usercode.Text = ""
            username.Text = ""
            ltlAlert.Text = "Form1.usercode.focus();"
            If Request("type") Is Nothing Then
                type.Text = "0"
            Else
                type.Text = Request("type")
            End If
        End If
    End Sub

    Sub search_click(ByVal sender As Object, ByVal e As System.EventArgs)

        If usercode.Text.Trim() = "" And username.Text.Trim() = "" Then
            ltlAlert.Text = "alert('员工工号和姓名不能为空 ！');Form1.usercode.focus();"
            Exit Sub
        End If
        If usercode.Text.Trim() <> "" And username.Text.Trim() <> "" Then
            ltlAlert.Text = "alert('员工工号和姓名不能同时查询 ！');Form1.usercode.focus();"
            Exit Sub
        End If

        sdatabind()


    End Sub

    Function sdatabind()

        Dim i As Integer
        strSql = " Select u.userID,u.userNo,u.userName,isnull(d.name,''),isnull(w.name,''),u.leavedate From tcpc0.dbo.users u "
        strSql &= " left outer JOIN departments d ON d.departmentID=u.departmentID "
        strSql &= " left outer JOIN Workshop w ON w.id=u.workshopID "
        strSql &= "  where u.deleted=0 and u.isActive=1 "
        If type.Text.Trim <> "0" Then
            If type.Text.Trim = "95" Then
                strSql &= " and u.workTypeID='" & type.Text.Trim & "' "
            Else
                strSql &= " and u.workTypeID<>'95' "
            End If
        End If
        If usercode.Text.Trim() <> "" Then
            strSql &= "and cast(u.userNo as varchar)='" & usercode.Text.Trim() & "' "
        End If
        If username.Text.Trim() <> "" Then
            strSql &= " and lower(u.userName) like N'%" & username.Text.Trim().ToLower() & "%'"
        End If
        strSql &= " and u.plantCode='" & Session("PlantCode") & "'"
        'strSql &= " and u.isTemp='" & Session("temp") & "'"

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("leavedate", System.Type.GetType("System.String")))
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1

                    dr1 = dt.NewRow()
                    dr1.Item("userID") = .Rows(i).Item(0)
                    dr1.Item("userNo") = .Rows(i).Item(1)
                    dr1.Item("userName") = .Rows(i).Item(2)
                    dr1.Item("department") = .Rows(i).Item(3)
                    dr1.Item("workshop") = .Rows(i).Item(4)
                    dr1.Item("leavedate") = .Rows(i).Item(5)
                    dt.Rows.Add(dr1)
                Next
                Dim dv As DataView
                dv = New DataView(dt)
                If (Session("orderby").Length <= 0) Then
                    Session("orderby") = "userID"
                End If
                'Response.Write(sortDir)
                Try
                    dv.Sort = Session("orderby") & Session("orderdir")
                    Datagrid2.DataSource = dv
                    Datagrid2.DataBind()
                Catch
                End Try
            Else
                If type.Text = "0" Then
                    ltlAlert.Text = "alert('所查员工不存在！');Form1.usercode.focus();"
                Else
                    If type.Text = "95" Then
                        ltlAlert.Text = "alert('所查员工不是计件工或不存在！');Form1.usercode.focus();"
                    Else
                        ltlAlert.Text = "alert('所查员工不是计时工或不存在！');Form1.usercode.focus();"
                    End If
                End If
                Exit Function
            End If
        End With

    End Function


End Class

End Namespace
