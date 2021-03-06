Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc


Partial Class perf_org
        Inherits BasePage
    'Protected WithEvents ltlAlert As Literal
    Public chk As New adamClass
    Dim strSql As String
    

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then 
            'Response.Write(Session("plantcode")) 
            'delete useless data
            ClearUselessData()
            ClearTempData()
            'load
            LoadposData()
            BindData()
        End If

    End Sub

    Protected Sub ClearUselessData()
        strSql = "delete from tcpc0.dbo.org where positions ='' or positions is null "
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
    End Sub
    Protected Sub ClearTempData()
        strSql = "delete from tcpc0.dbo.orgtemp where createdBy='" & Session("uID") & "' "
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
    End Sub
    Sub LoadposData()
        Dim reader As SqlDataReader
        strSql = " SELECT id  From tcpc0.dbo.org Where parentid is null  Order by id "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read()
            strSql = " insert into tcpc0.dbo.orgtemp values('" & reader(0) & "',0,'" & Session("uID") & "')"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            GetposDetail(reader(0))
        End While
    End Sub
    Sub GetposDetail(ByVal posid As Integer, Optional ByVal tag As Integer = 1)
        Dim ds As DataSet
        Dim i As Integer
        strSql = " Select id From tcpc0.dbo.org Where parentid=" & posid & " Order By id "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                For i = 0 To .Rows.Count - 1
                    strSql = "Insert Into tcpc0.dbo.orgtemp Values('" & .Rows(i).Item(0) & "'," & tag & "," & Session("uID") & ") "
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                    GetposDetail(.Rows(i).Item(0), tag + 1)
                Next
            End If
        End With
        ds.Reset()
    End Sub

    Sub BindData()
        Dim ds As DataSet
        strSql = " SELECT org.id,orgt.tag,ISNULL(org.positions,'') as positions,ISNULL(org.name,'') as name " _
               & " From tcpc0.dbo.org org  INNER JOIN tcpc0.dbo.orgtemp orgt on orgt.posid=org.id and org.plant='" & Session("plantcode") & "'and orgt.createdBy='" & Session("uID") & "' " _
               & " Order by orgt.id "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        'Session("EXHeader") = "/document/DocumentCategoryExport.aspx?typeID=" & SelectTypeDropDown.SelectedValue & "^~^"
        'Session("EXSQL") = strSql
        'Session("EXTitle") = "200^<b>分类名称</b>~^100^<b>是否公开</b>~^"
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("pos", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("pos1", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                'countLabel.Text = "<font color='#996633'>" & .Rows.Count & "</font>条记录"
                Dim i As Integer
                Dim j As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    Dim tag As String = ""
                    dr1 = dt.NewRow()
                    dr1.Item("gsort") = i + 1
                    dr1.Item("id") = .Rows(i).Item("id").ToString().Trim()
                    For j = 1 To .Rows(i).Item("tag")
                        ' tag = tag & "&nbsp;&nbsp;&nbsp;"
                        tag = tag & "----"
                    Next
                    dr1.Item("pos") = tag & .Rows(i).Item("positions").ToString().Trim()
                    dr1.Item("pos1") = .Rows(i).Item("positions").ToString().Trim()
                    dr1.Item("name") = .Rows(i).Item("name").ToString().Trim()
                    dt.Rows.Add(dr1)
                Next
            Else
                'countLabel.Text = "共有<font color='#996633'>0</font>条记录"
            End If
        End With

        Dim dv As DataView
        dv = New DataView(dt)

        'Try

        DataGrid1.DataSource = dv
        DataGrid1.DataBind()

        'Catch
        'End Try

    End Sub



    Private Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Add.Click
        Dim maxID As Integer
        Dim ispub As String
        Dim tag As Integer

        If txb1.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('请输入职位名称!');"
            Exit Sub
        End If
        If txb2.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('请输入姓名!');"
            Exit Sub
        End If
        strSql = "Select id from tcpc0.dbo.org where positions=N'" & txb1.Text.Trim & "'and plant = '" & Session("plantcode") & "'"
        If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
            ltlAlert.Text = "alert('职位名已存在')"
            Exit Sub
        End If


        'Response.Write(DataGrid1.SelectedItem.Cells(0).Text)

        If btn_Add.Text.Trim = "增加下级职位" Then
            strSql = "Insert Into tcpc0.dbo.org(positions,name,parentid,plant) Values(N'" & txb1.Text.Trim & "', N'" & txb2.Text.Trim & "','" & DataGrid1.SelectedItem.Cells(0).Text & "','" & Session("plantcode") & "') "
            'Me.Response.Write(strSql)
            'Exit Sub
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            ClearTempData()
            LoadposData()

        Else
            If btn_Add.Text.Trim = "增加" Then
                strSql = "Insert Into tcpc0.dbo.org(positions,name,plant) Values(N'" & txb1.Text.Trim & "',N'" & txb2.Text.Trim & "','" & Session("plantcode") & "') "
                strSql &= " select @@identity "
                maxID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                strSql = " insert into tcpc0.dbo.orgtemp values('" & maxID & "',0,'" & Session("uID") & "')"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
                GetposDetail(maxID)

            End If
        End If
        txb1.Text = ""
        txb2.Text = ""
        btn_Add.Text = "增加"
        DataGrid1.CurrentPageIndex = 0
        DataGrid1.SelectedIndex = -1
        BindData()
    End Sub

    Private Sub Btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        DataGrid1.SelectedIndex = -1
        txb1.Text = ""
        txb2.Text = ""
        btn_Add.Text = "增加"
    End Sub
    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        DataGrid1.EditItemIndex = -1
        BindData()
    End Sub

    Private Sub Edit(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        DataGrid1.EditItemIndex = e.Item.ItemIndex
        BindData()
    End Sub

    Public Sub Update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand

        Dim str As String = CType(e.Item.FindControl("txbpos"), TextBox).Text.Trim
        Dim str1 As String = CType(e.Item.FindControl("txbname"), TextBox).Text.Trim

        If str.Length <= 0 Then
            ltlAlert.Text = "alert('职位名不能为空')"
            Exit Sub
        End If

        strSql = "Select id from tcpc0.dbo.org where positions=N'" & str & "' and id<>'" & e.Item.Cells(0).Text & "' and plant = '" & Session("plantcode") & "' "
        If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
            ltlAlert.Text = "alert('职位名已存在')"
            Exit Sub
        End If

        If str1.Length <= 0 Then
            ltlAlert.Text = "alert('姓名不能为空')"
            Exit Sub
        End If
        strSql = "update tcpc0.dbo.org set positions=N'" & str & "',name=N'" & str1 & "' where id='" & e.Item.Cells(0).Text & "' and plant='" & Session("plantcode") & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        DataGrid1.EditItemIndex = -1
        BindData()
    End Sub
    Public Sub Cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        ClearUselessData()
        DataGrid1.EditItemIndex = -1
        BindData()
    End Sub
    Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandName.CompareTo("delete") = 0) Then
            strSql = "select count(*) from tcpc0.dbo.org where parentid='" & e.Item.Cells(0).Text() & "' and plant = '" & Session("plantcode") & "' "
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) > 0 Then
                ltlAlert.Text = "alert('该职位有下级职位，不能删除!');"
                Exit Sub
            End If
            strSql = "Delete From tcpc0.dbo.org where id = '" & e.Item.Cells(0).Text() & "' and plant = '" & Session("plantcode") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            DataGrid1.EditItemIndex = -1
            BindData()
        ElseIf (e.CommandName.CompareTo("select")) = 0 Then
            btn_Add.Text = "增加下级职位"
        End If
    End Sub
End Class

End Namespace







