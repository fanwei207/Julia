Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration
Imports System.DateTime


Namespace tcpc


Partial Class employeecertalarm
        Inherits BasePage
    Dim nRet As Integer
    Dim strSql As String
    Dim ds As DataSet
    Public chk As New adamClass
    Dim reader As SqlDataReader
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
            BindData()
        End If
    End Sub
    Sub binddata()
        strSql = "select id, userno,username,certname,pos,begindate,enddate,memo,"
            strSql &= " diff = case when datediff(day,enddate,getdate())>0 then N'已过期' else N'未过期' end "
            strSql &= "from tcpc0.dbo.employeecert where plantcode = '" & Session("plantcode") & "'and datediff(day, enddate,getdate()) >-30 and active = '1'"
        If txbNO.Text.Trim.Length > 0 Then
            strSql &= "and userno like '%" & txbNO.Text.Trim & "%'"
        End If
        If txbName.Text.Trim.Length > 0 Then
            strSql &= "and username like N'%" & txbName.Text.Trim & "%'"
        End If
        If Txbpos.Text.Trim.Length > 0 Then
                strSql &= "and pos like N'%" & Txbpos.Text.Trim & "%'"
        End If
        If Txbcert.Text.Trim.Length > 0 Then
            strSql &= "and certname like N'%" & Txbcert.Text.Trim & "%'"
        End If
        If Txbmemo.Text.Trim.Length > 0 Then
            strSql &= "and memo like N'%" & Txbmemo.Text.Trim & "%'"
        End If
        If Txbbegin.Text.Trim.Length > 0 Then
            strSql &= "and begindate = '" & Txbbegin.Text.Trim & "'"
        End If
        If Txbend.Text.Trim.Length > 0 Then
            strSql &= "and enddate = '" & Txbend.Text.Trim & "'"
        End If
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

        Dim total As Integer
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("userno", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("username", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("certname", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("pos", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("begindate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("enddate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("memo", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("diff", System.Type.GetType("System.String")))
        Dim drow As DataRow
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    drow = dt.NewRow()
                    drow.Item("id") = .Rows(i).Item("id").ToString().Trim()
                    drow.Item("userno") = .Rows(i).Item("userno").ToString().Trim()
                    drow.Item("username") = .Rows(i).Item("username").ToString().Trim()
                    drow.Item("certname") = .Rows(i).Item("certname")
                    drow.Item("pos") = .Rows(i).Item("pos").ToString().Trim()
                    If IsDBNull(.Rows(i).Item("begindate")) = False Then
                        drow.Item("begindate") = Format(.Rows(i).Item("begindate"), "yyyy-MM-dd")
                    Else
                        drow.Item("begindate") = ""
                    End If
                    If IsDBNull(.Rows(i).Item("enddate")) = False Then
                        drow.Item("enddate") = Format(.Rows(i).Item("enddate"), "yyyy-MM-dd")
                    Else
                        drow.Item("enddate") = ""
                    End If
                    drow.Item("memo") = .Rows(i).Item("memo").ToString().Trim()
                    drow.Item("diff") = .Rows(i).Item("diff").ToString().Trim()
                    dt.Rows.Add(drow)
                    total = total + 1
                Next
            End If
        End With
        ds.Reset()
        Dim dvw As DataView
        dvw = New DataView(dt)
        datagrid1.DataSource = dvw
        datagrid1.DataBind()

    End Sub

    Private Sub btnser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnser.Click
        datagrid1.CurrentPageIndex = 0
        datagrid1.SelectedIndex = -1
        binddata()
        End Sub

        Protected Sub datagrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles datagrid1.ItemCommand
            If e.CommandName.CompareTo("close") = 0 Then
                strSql = "update tcpc0.dbo.employeecert set active='0' where id =' " & e.Item.Cells(0).Text.Trim() & "' and plantcode = '" & Session("plantcode") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                binddata()
                datagrid1.CurrentPageIndex = 0
                Exit Sub
            End If
        End Sub

        Private Sub datagrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles datagrid1.ItemCreated
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    e.Item.Cells(9).Attributes.Add("onclick", "return confirm('确定要关闭该记录吗?');")
                Case ListItemType.AlternatingItem
                    e.Item.Cells(9).Attributes.Add("onclick", "return confirm('确定要关闭该记录吗?');")
                Case ListItemType.EditItem
                    e.Item.Cells(9).Attributes.Add("onclick", "return confirm('确定要关闭该记录吗?');")
            End Select
        End Sub
        Private Sub Btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btncancel.Click
            txbNO.Text = ""
            txbName.Text = ""
            Txbpos.Text = ""
            Txbcert.Text = ""
            Txbmemo.Text = ""
            Txbbegin.Text = ""
            Txbend.Text = ""
            datagrid1.SelectedIndex = -1
        End Sub
   
    End Class

End Namespace
