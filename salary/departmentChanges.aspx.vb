Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class departmentChanges
        Inherits BasePage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

    Protected strTemp As String
    'Protected WithEvents ltlAlert As Literal

    Protected WithEvents rolename As System.Web.UI.WebControls.Label
    Protected WithEvents departmentName As System.Web.UI.WebControls.Label
    Protected WithEvents enterDate As System.Web.UI.WebControls.Label
    Shared yr As String
    Shared ye As String
    Public chk As New adamClass

    Dim strSql As String
    Dim i As Integer
    Dim reader As SqlDataReader

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            TextBox1.Text = Format(DateTime.Now, "yyyy-MM-01")
            TextBox2.Text = Format(DateTime.Now, "yyyy-MM-dd")

            BindData()
        End If
    End Sub
    Private Sub BindData()
        If Not IsDate(TextBox1.Text) Then
            TextBox1.Text = Format(DateTime.Now, "yyyy-MM-01")
        End If
        If Not IsDate(TextBox2.Text) Then
            TextBox2.Text = Format(DateTime.Now, "yyyy-MM-dd")
        End If

        Dim ds As DataSet
        Dim people As Integer = 0
            strSql = " Select a.UserID,u.userNo, u.userName,isnull(d1.name,'') as dname ,d.name,isnull(w1.name,'') as dname,isnull(w.name,'') as wname,a.ExchangeDate"
        strSql &= " From User_Exchange_Department a INNER JOIN tcpc0.dbo.users u ON u.userID = a.UserID "
        strSql &= " inner join departments d on d.departmentID=a.departmentID "
        strSql &= " Left outer join workshop w on w.ID=u.workshopID "
            strSql &= " left outer join departments d1 on d1.departmentID=a.olddepartmentID "
            strSql &= " Left outer join workshop w1 on w1.ID=a.oldworkshopid "
        strSql &= " Where a.ExchangeDate>= '" & TextBox1.Text & "' and a.ExchangeDate<DATEADD(day,1, '" & TextBox2.Text & "') "
        strSql &= " and u.plantCode='" & Session("PlantCode") & "' and u.isTemp='" & Session("temp") & "'"
        If TextBox3.Text.Trim.Length > 0 Then
            strSql &= " and u.UserNo= '" & TextBox3.Text.Trim & "' "
        End If
        strSql &= " order by a.UserID,a.ExchangeDate desc, ExchangeID desc"
        'Response.Write(strSql)
        'Exit Sub
            Session("EXTitle") = "<b>工号</b>~^<b>姓名</b>~^150^<b>原部门</b>~^150^<b>部门</b>~^<b>原工段</b>~^150^<b>工段</b>~^<b>调入日期</b>~^"
        Session("EXHeader") = "日期~" & TextBox1.Text & "--" & TextBox2.Text
        Session("EXSQL") = strSql

        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim dt As New DataTable
        'dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("userNo", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("departmentName1", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("departmentName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workshop1", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("hrs", System.Type.GetType("System.DateTime")))

        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("userID") = .Rows(i).Item(0)
                    dr1.Item("userNo") = .Rows(i).Item(1)
                    dr1.Item("name") = .Rows(i).Item(2)
                    dr1.Item("departmentName1") = .Rows(i).Item(3)
                        dr1.Item("departmentName") = .Rows(i).Item(4)
                        dr1.Item("workshop1") = .Rows(i).Item(5)
                        dr1.Item("workshop") = .Rows(i).Item(6)
                        dr1.Item("hrs") = .Rows(i).Item(7)
                    dt.Rows.Add(dr1)
                    people = people + 1
                Next
            End If
        End With

        Dim dv As DataView
        Label1.Text = "<b>人数： </b>" & people.ToString()
        dv = New DataView(dt)

        Try
            DataGrid1.DataSource = dv
            DataGrid1.DataBind()
        Catch
        End Try

    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DataGrid1.CurrentPageIndex = 0
        BindData()
    End Sub
End Class

End Namespace
