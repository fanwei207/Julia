Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class department
        Inherits BasePage

        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
        Public chk As New adamClass

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

                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT DepartmentID, Isnull(isSalary,0), Isnull(isManufacture,0), Isnull(isWip,0), Isnull(isRepair,0), code, name " _
                   & " From Departments  where active=1 AND code is not null "
            If txbdeptname.Text.Trim.Length > 0 Then
                strSQL &= " and LOWER(name) like N'%" & txbdeptname.Text.Trim.ToLower & "%' "
            End If
            If txbdeptcode.Text.Trim.Length > 0 Then
                strSQL &= " and LOWER(code) like N'%" & txbdeptcode.Text.Trim.ToLower & "%' "
            End If
            strSQL &= " Order by name"

            Session("EXSQL") = strSQL
            Session("EXTitle") = "<b>编号</b>~^<b>名称</b>~^"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("mfcType", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("wipType", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("repairType", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("repairBack", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("code") = .Rows(i).Item(5).ToString().Trim()
                        dr1.Item("Name") = .Rows(i).Item(6).ToString().Trim()
                        If .Rows(i).Item(1) = True Then
                            dr1.Item("type") = "是"
                        Else
                            dr1.Item("type") = "否"
                        End If
                        If .Rows(i).Item(2) = True Then
                            dr1.Item("mfcType") = "是"
                        Else
                            dr1.Item("mfcType") = "否"
                        End If
                        If .Rows(i).Item(3) = True Then
                            dr1.Item("wipType") = "是"
                        Else
                            dr1.Item("wipType") = "否"
                        End If
                        If .Rows(i).Item(4) = 1 Then
                            dr1.Item("repairType") = "是"
                            dr1.Item("repairBack") = "否"
                        ElseIf .Rows(i).Item(4) = 2 Then
                            dr1.Item("repairType") = "是"
                            dr1.Item("repairBack") = "是"
                        Else
                            dr1.Item("repairType") = "否"
                            dr1.Item("repairBack") = "否"
                        End If
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
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
            Dim strSQL As String
            Dim str As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空.')"
                Exit Sub
            End If

            Dim str1 As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text
            If (str1.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('编号不能为空.')"
                Exit Sub
            End If
            Dim str2, str3, strWip, strRepair As String
            Dim flag As Boolean = True
            Try
                If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("box"), CheckBox).Checked = False Then

                End If
            Catch
                flag = False
            End Try

            If flag = True Then
                If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("box"), CheckBox).Checked = False Then
                    str2 = "0"
                Else
                    str2 = "1"
                End If

                If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("mfcBox"), CheckBox).Checked = False Then
                    str3 = "0"
                Else
                    str3 = "1"
                End If

                If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkWip"), CheckBox).Checked = False Then
                    strWip = "0"
                Else
                    strWip = "1"
                End If

                If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkRepair"), CheckBox).Checked = True Then
                    If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkRepairBack"), CheckBox).Checked = True Then
                        strRepair = "2"
                    Else
                        strRepair = "1"
                    End If
                ElseIf CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkRepairBack"), CheckBox).Checked = True Then
                    strRepair = "2"
                Else
                    strRepair = "0"
                End If

                strSQL = " Update departments Set name=N'" & str & "'," _
                       & " code=N'" & str1 & "'," _
                       & " isSalary='" & str2 & "'," _
                       & " isManufacture='" & str3 & "' "
                '& " ,isWip='" & strWip.Trim() & "' " _
                '& " ,isRepair='" & strRepair.Trim() & "'" _
                strSQL &= " Where DepartmentID='" & e.Item.Cells(1).Text & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            End If  '// End error
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strSQL As String
                strSQL = " Delete From departments Where DepartmentID='" & e.Item.Cells(1).Text() & "'"
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
            strSQL = "Insert Into Departments(code, type, name, isSalary, isManufacture, isWip, isRepair) Values('code',0,N'(新的部门)',0,0,0,0)"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            DataGrid1.EditItemIndex = 0
            BindData()
        End Sub

        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            'Label1
            Dim temp1, temp2, temp3, temp4, temp5 As String
            If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("Label1"), Label).Text = "否" Then
                temp1 = "否"
            End If

            If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("Label2"), Label).Text = "否" Then
                temp2 = "否"
            End If

            If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("lblWip"), Label).Text = "否" Then
                temp3 = "否"
            End If

            If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("lblRepair"), Label).Text = "否" Then
                temp4 = "否"
            End If

            If CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("lblRepairBack"), Label).Text = "否" Then
                temp5 = "否"
            End If

            BindData()

            If temp1 = "否" Then
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("box"), CheckBox).Checked = False
            Else
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("box"), CheckBox).Checked = True
            End If

            If temp2 = "否" Then
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("mfcBox"), CheckBox).Checked = False
            Else
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("mfcBox"), CheckBox).Checked = True
            End If

            If temp3 = "否" Then
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkWip"), CheckBox).Checked = False
            Else
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkWip"), CheckBox).Checked = True
            End If

            If temp4 = "否" Then
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkRepair"), CheckBox).Checked = False
            Else
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkRepair"), CheckBox).Checked = True
            End If

            If temp5 = "否" Then
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkRepairBack"), CheckBox).Checked = False
            Else
                CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("chkRepairBack"), CheckBox).Checked = True
            End If
        End Sub

        Private Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
            DataGrid1.CurrentPageIndex = 0
            BindData()
        End Sub
    End Class

End Namespace
