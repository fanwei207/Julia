Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class deductMoney
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents unitlist As System.Web.UI.WebControls.DropDownList

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            If Not IsPostBack Then

                Dim item As ListItem
                Dim strSQL As String
                Dim ds As DataSet

                strSQL = " SELECT s.systemCodeID,s.systemCodeName " _
                   & " From tcpc0.dbo.systemCode s" _
                   & " INNER JOIN tcpc0.dbo.systemCodeType st ON st.systemCodeTypeID=s.systemCodeTypeID " _
                   & " Where st.systemCodeTypeName='Deduct Money Type'" _
                   & " Order by s.systemCodeName"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            DropDownList1.Items.Add(item)
                            If ID = item.Value Then
                                DropDownList1.SelectedIndex = i
                            End If
                        Next
                    Else
                        DropDownList1.SelectedIndex = 0
                    End If
                End With
                ds.Reset()

                BindData()
            End If
        End Sub
        Private Sub BindData()
            Dim strSQL As String
            Dim query As String
            Dim ds As DataSet
            strSQL = " SELECT ID,name,unitPrice,isnull(status,2) as status " _
                   & " From DeductMoneyType where deductTypeID=" & DropDownList1.SelectedValue & " and organizationID=" & Session("orgID") _
                   & " Order by name"
            'For Excel Manu
            query = " SELECT ID,name,unitPrice " _
                   & " From DeductMoneyType where deductTypeID=" & DropDownList1.SelectedValue & " and organizationID=" & Session("orgID") _
                   & " Order by name"

            Session("EXSQL") = query
            Session("EXTitle") = "<b>名称</b>~^<b>单价</b>~^"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("price", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("flagID", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("price") = .Rows(i).Item(2)
                        dr1.Item("name") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("flagID") = .Rows(i).Item(3)
                        Select Case .Rows(i).Item(3)
                            Case 0
                                dr1.Item("status") = "内"
                            Case 1
                                dr1.Item("status") = "外"
                            Case 2
                                dr1.Item("status") = "无"
                        End Select
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
            Dim ds As DataSet
            Dim str As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            Dim flag As String = CType(e.Item.FindControl("unitlist"), DropDownList).SelectedItem.Value
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空.')"
                Exit Sub
            End If
            Dim str1 As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text
            If (str1.Trim.Length <= 0) Then
                strSQL = "update DeductMoneyType set name=N'" & str & "',deductTypeID=" & DropDownList1.SelectedValue
            Else
                Dim up As Decimal
                Try
                    up = Convert.ToDecimal(str1)
                Catch ex As Exception
                    ltlAlert.Text = "alert('金额输入的格式不对');"
                    Exit Sub

                End Try
                strSQL = "update DeductMoneyType set name=N'" & str & "',deductTypeID=" & DropDownList1.SelectedValue & ", unitPrice=" & str1
            End If
            If CInt(flag) <> 2 Then
                strSQL &= " , status='" & flag & "' where ID=" & e.Item.Cells(6).Text
            Else
                strSQL &= " ,status=null where ID=" & e.Item.Cells(6).Text
            End If
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            strSQL = " Update Indicator set DeductMoneyType=DeductMoneyType+1 "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            DataGrid1.EditItemIndex = -1
            BindData()

        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strSQL As String
                strSQL = "delete from DeductMoneyType where ID = " & e.Item.Cells(6).Text()
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                strSQL = " Update Indicator set DeductMoneyType=DeductMoneyType+1 "
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
            strSQL = "Insert Into DeductMoneyType(name,deductTypeID,organizationID) Values(N'(新的扣款)'," & DropDownList1.SelectedValue & "," & Session("orgID") & ")"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            DataGrid1.EditItemIndex = 0
            BindData()
        End Sub
        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex

            BindData()
            'Response.Write(CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("unitlist"), DropDownList).SelectedValue.ToString())
            CType(DataGrid1.Items(e.Item.ItemIndex).FindControl("unitlist"), DropDownList).SelectedValue = CInt(e.Item.Cells(7).Text)
        End Sub

        Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            DataGrid1.CurrentPageIndex = 0
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Public Function unitListSource()
            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
            Dim dr1 As DataRow
            dr1 = dt.NewRow()
            dr1.Item("name") = "无"
            dr1.Item("id") = 2
            dt.Rows.Add(dr1)
            dr1 = dt.NewRow()
            dr1.Item("name") = "内"
            dr1.Item("id") = 0
            dt.Rows.Add(dr1)
            dr1 = dt.NewRow()
            dr1.Item("name") = "外"
            dr1.Item("id") = 1
            dt.Rows.Add(dr1)
            Return dt
        End Function


    End Class

End Namespace
