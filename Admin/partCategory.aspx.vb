Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

    Partial Class partCategory
        Inherits BasePage
        Shared sortOrder As String = ""
        Shared sortDir As String = "ASC"
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass

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
                ClearUselessData()
                BindData()
            End If
        End Sub
        Function createSQL() As String
            createSQL = " SELECT ic.id, ic.name, isnull(ic.description,''), ic.ratio, icp.name  " _
                   & " From tcpc0.dbo.ItemCategory ic Left Outer Join tcpc0.dbo.ItemCategory icp On icp.id=ic.parentID And icp.type=0 " _
                   & " Where ic.name is not null And ic.type=0 "
            If _txtName.Text.Trim.Length > 0 Then
                createSQL &= " and LOWER(ic.name) like N'%" & _txtName.Text.Trim.ToLower & "%' "
            End If
            If _txtDesc.Text.Trim.Length > 0 Then
                createSQL &= " and LOWER(ic.description) like N'%" & _txtDesc.Text.Trim.ToLower & "%' "
            End If
            createSQL &= " Order by ic.name "
        End Function


        Private Sub BindData()
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, createSQL())

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("desc", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ratio", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("newCategory", System.Type.GetType("System.Boolean")))
            dt.Columns.Add(New DataColumn("parent", System.Type.GetType("System.String")))

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("desc") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("ratio") = .Rows(i).Item(3).ToString().Trim()
                        If dr1.Item("Name") = "" Then
                            dr1.Item("newCategory") = "True"
                        Else
                            dr1.Item("newCategory") = "False"
                        End If
                        dr1.Item("parent") = .Rows(i).Item(4).ToString().Trim()
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub

        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
            ClearUselessData()
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
            If Not MyBase.Security("19023402").isValid Then
                ltlAlert.Text = "alert('请确认是否有编辑部件分类的权限。')"
                Exit Sub
            End If
            Dim strSQL, parentID As String
            Dim str1 As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text.Trim
            Dim str2 As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text.Trim
            Dim str3 As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text.Trim
            Dim str4 As String = CType(e.Item.Cells(4).Controls(0), TextBox).Text.Trim

            If (str1.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空.')"
                Exit Sub
            End If

            If (str3.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('比率不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str3) Then
                ltlAlert.Text = "alert('比率应为数字型数据.')"
                Exit Sub
            End If

            If str4.Trim().Length > 0 Then
                strSQL = " Select id From ItemCategory Where UPPER(name)=N'" & str4.ToUpper & "'"
                parentID = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)
                If parentID = Nothing Then
                    ltlAlert.Text = "alert('所属分类不存在！');"
                    Exit Sub
                End If
            Else
                parentID = "0"
            End If

            If e.Item.Cells(8).Text = "True" Then
                strSQL = " Select count(*) From ItemCategory Where UPPER(name)=N'" & str1.ToUpper & "'"
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) > 0 Then
                    ltlAlert.Text = "alert('类名已经存在！');"
                    Exit Sub
                End If
            End If
            strSQL = " Update ItemCategory Set name=N'" & str1 & "',description=N'" & str2 & "',ratio='" & str3 & "', parentID='" & parentID.Trim() & "', modifiedBy='" & Session("uID") & "', modifiedDate=getdate(), plantCode='" & Session("plantCode") & "' Where id=" & e.Item.Cells(7).Text
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                If Not MyBase.Security("19023402").isValid Then
                    ltlAlert.Text = "alert('请确认是否有编辑部件分类的权限。')"
                    Exit Sub
                End If
                Dim strSQL As String
                Dim ds As DataSet
                strSQL = " Select id From Items Where category = '" & e.Item.Cells(7).Text() & "'"
                ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSQL)
                If ds.Tables(0).Rows.Count > 0 Then
                    ltlAlert.Text = "alert('此材料分类在使用中，不能被删除。')"
                    Exit Sub
                End If
                ds.Reset()
                strSQL = " Delete From ItemCategory Where id = '" & e.Item.Cells(7).Text() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = -1
                BindData()
            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            Dim strSQL As String
            If DataGrid1.EditItemIndex = -1 Then
                strSQL = " Insert Into ItemCategory(name, ratio, type, plantcode, createdBy, createdDate) Values('',1,0," & Session("plantCode") & ",'" & Session("uID") & "',getdate())"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                DataGrid1.CurrentPageIndex = 0

                DataGrid1.EditItemIndex = 0
                BindData()
            Else
                ltlAlert.Text = "alert('请先完成上一类别的更新，然后添加！')"
            End If
        End Sub

        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Sub ClearUselessData()
            Dim strSQL As String
            strSQL = " Delete From ItemCategory where name ='' or name is null "
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
        End Sub

        Private Sub _bntSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _bntSearch.Click
            DataGrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
            Dim EXTitle As String = "<b>名称</b>~^300^<b>描述</b>~^<b>比率</b>~^<b>父分类</b>~^"
            Dim ExSql As String = createSQL()

            Me.ExportExcel(chk.dsn0(), EXTitle, ExSql, False)
        End Sub
    End Class

End Namespace

