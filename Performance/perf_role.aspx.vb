Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class perf_role
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass
        Dim item As ListItem

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

                DropDownList1.SelectedIndex = 0

                Dim StrSql As String
                Dim ds As DataSet
                StrSql = "SELECT perf_type_id,perf_type From tcpc0.dbo.perf_type order by perf_type_id "
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            DropDownList1.Items.Add(item)
                        Next
                    End If
                End With
                ds.Reset()

                BindData()
            End If
        End Sub

        Function createSQL() As String
            createSQL = " SELECT perf_defi_id,perf_type,perf_cause,isnull(perf_mark_min,0),isnull(perf_mark_max,0),isnull(perf_mark2_min,0),isnull(perf_mark2_max,0),isnull(perf_mark3_min,0),isnull(perf_mark3_max,0),isnull(perf_mark4_min,0),isnull(perf_mark4_max,0),createdDate,createdBy,modifiedDate,modifiedBy ,isnull(perf_demerit,0),isnull(perf_warning,0),isnull(perf_drop,0) " _
              & " From tcpc0.dbo.perf_definition where perf_type_id ='" & DropDownList1.SelectedValue & "' " _
              & " Order by perf_defi_id desc"
        End Function


        Private Sub BindData()
            Dim strSQL As String
            Dim ds As DataSet

            strSQL = createSQL()   
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim total As Integer = 0

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("mark1", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("mark2", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("mark21", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("mark22", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("mark31", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("mark32", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("mark41", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("mark42", System.Type.GetType("System.Decimal")))


            dt.Columns.Add(New DataColumn("mark51", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("mark52", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("mark53", System.Type.GetType("System.Decimal")))

            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("cdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("cby", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("mdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("mby", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("mark1") = Format(.Rows(i).Item(3), "##0.####")
                        dr1.Item("mark2") = Format(.Rows(i).Item(4), "##0.####")
                        dr1.Item("mark21") = Format(.Rows(i).Item(5), "##0.####")
                        dr1.Item("mark22") = Format(.Rows(i).Item(6), "##0.####")
                        dr1.Item("mark31") = Format(.Rows(i).Item(7), "##0.####")
                        dr1.Item("mark32") = Format(.Rows(i).Item(8), "##0.####")
                        dr1.Item("mark41") = Format(.Rows(i).Item(9), "##0.####")
                        dr1.Item("mark42") = Format(.Rows(i).Item(10), "##0.####")

                        dr1.Item("mark51") = Format(.Rows(i).Item(15), "##0.####")
                        dr1.Item("mark52") = Format(.Rows(i).Item(16), "##0.####")
                        dr1.Item("mark53") = Format(.Rows(i).Item(17), "##0.####")
                        dr1.Item("name") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("cdate") = Format(.Rows(i).Item(11), "yy-MM-dd")
                        dr1.Item("cby") = .Rows(i).Item(12).ToString().Trim()
                        dr1.Item("mdate") = Format(.Rows(i).Item(13), "yy-MM-dd")
                        dr1.Item("mby") = .Rows(i).Item(14).ToString().Trim()
                        dt.Rows.Add(dr1)
                        total = total + 1
                    Next
                End If
            End With
            Label2.Text = "数量：" & total.ToString()
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub
        Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing ButtonColumn
                    myDeleteButton = e.Item.Cells(15)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")

                Case ListItemType.AlternatingItem
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(15)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")
                Case ListItemType.EditItem
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(15)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")

                    CType(e.Item.Cells(1).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(300)
                    CType(e.Item.Cells(2).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(3).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(4).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(5).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(6).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(7).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(8).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(9).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(10).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(11).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
                    CType(e.Item.Cells(12).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(30)
            End Select

        End Sub

        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
            Dim str As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            If str.Trim = "0新增原因" Then
                Dim strSQL As String
                strSQL = "delete from tcpc0.dbo.perf_definition  where perf_defi_id=" & e.Item.Cells(19).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            End If

            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub
        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
            Dim strSQL As String 
            Dim str As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('原因不能为空.')"
                Exit Sub
            End If
            Dim str1 As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            If (str1.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str1) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If
            Dim str2 As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text
            If (str2.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str2) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If

            If CDec(str1) > CDec(str2) Then
                ltlAlert.Text = "alert('最大分不能小于最小分.')"
                Exit Sub
            End If


            Dim str21 As String = CType(e.Item.Cells(4).Controls(0), TextBox).Text
            If (str1.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str21) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If
            Dim str22 As String = CType(e.Item.Cells(5).Controls(0), TextBox).Text
            If (str22.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str22) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If

            If CDec(str21) > CDec(str22) Then
                ltlAlert.Text = "alert('最大分不能小于最小分.')"
                Exit Sub
            End If

            Dim str31 As String = CType(e.Item.Cells(6).Controls(0), TextBox).Text
            If (str31.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str31) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If
            Dim str32 As String = CType(e.Item.Cells(7).Controls(0), TextBox).Text
            If (str32.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str32) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If

            If CDec(str31) > CDec(str32) Then
                ltlAlert.Text = "alert('最大分不能小于最小分.')"
                Exit Sub
            End If

            Dim str41 As String = CType(e.Item.Cells(8).Controls(0), TextBox).Text
            If (str41.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str41) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If
            Dim str42 As String = CType(e.Item.Cells(9).Controls(0), TextBox).Text
            If (str42.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str42) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If

           



            If CDec(str41) > CDec(str42) Then
                ltlAlert.Text = "alert('最大分不能小于最小分.')"
                Exit Sub
            End If

            Dim str51 As String = CType(e.Item.Cells(11).Controls(0), TextBox).Text
            If (str51.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str51) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If
            Dim str52 As String = CType(e.Item.Cells(10).Controls(0), TextBox).Text
            If (str52.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str52) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If


            Dim str53 As String = CType(e.Item.Cells(12).Controls(0), TextBox).Text
            If (str53.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('分数不能为空.')"
                Exit Sub
            ElseIf Not IsNumeric(str53) Then
                ltlAlert.Text = "alert('分数必须为数值型.')"
                Exit Sub
            End If

            If str.Trim = "0新增原因" Then
                strSQL = "delete from tcpc0.dbo.perf_definition  where perf_defi_id=" & e.Item.Cells(19).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            Else
                strSQL = "update tcpc0.dbo.perf_definition set perf_cause=N'" & str & "',perf_mark_min='" & str1 & "',perf_mark_max='" & str2 & "',perf_mark2_min='" & str21 & "',perf_mark2_max='" & str22 & "',perf_mark3_min='" & str31 & "',perf_mark3_max='" & str32 & "',perf_mark4_min='" & str41 & "',perf_mark4_max='" & str42 & "',perf_demerit='" & str51 & "',perf_warning='" & str52 & "',perf_drop='" & str53 & "',modifiedBy=N'" & Session("uName") & "',modifiedDate=getdate() where perf_defi_id=" & e.Item.Cells(19).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            End If
            DataGrid1.EditItemIndex = -1
            BindData()

        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strSQL As String
                strSQL = "delete from tcpc0.dbo.perf_definition where perf_defi_id = '" & e.Item.Cells(19).Text() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = -1
                BindData()
            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            Dim strSQL As String


            strSQL = "Insert into tcpc0.dbo.perf_definition(perf_type_id,perf_type, perf_cause,perf_mark_min,createdBy,createdDate,modifiedBy,modifiedDate,perf_mark_max,perf_mark2_min,perf_mark2_max,perf_mark3_min,perf_mark3_max,perf_mark4_min,perf_mark4_max,perf_demerit,perf_warning) "
            strSQL &= " values('" & DropDownList1.SelectedValue & "',N'" & DropDownList1.SelectedItem.Text & "',N'0新增原因',0,N'" & Session("uName") & "',getdate(),N'" & Session("uName") & "',getdate(),0,0,0,0,0,0,0,0,0) "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            DataGrid1.EditItemIndex = 0
            BindData()
        End Sub
        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            DataGrid1.CurrentPageIndex = 0
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            Dim EXHeader As String = "考评分维护     导出日期：" & Format(DateTime.Today, "yyyy-MM-dd")
            Dim EXTitle As String = "<b>类型</b>~^450^<b>原因</b>~^<b>最小扣分1</b>~^<b>最大扣分1</b>~^<b>最小扣分2</b>~^<b>最大扣分2</b>~^<b>最小扣分3</b>~^<b>最大扣分3</b>~^<b>最小扣分4</b>~^<b>最大扣分4</b>~^<b>创建日期</b>~^<b>创建人</b>~^<b>修改日期</b>~^<b>修改人</b>~^<b>记过</b>~^<b>警告</b>~^"
             
            Dim ExSql As String = createSQL() 
            Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)

        End Sub
    End Class

End Namespace
