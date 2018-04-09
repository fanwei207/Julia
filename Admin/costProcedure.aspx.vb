Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc

Partial Class admin_costProcedure
    Inherits BasePage
    Dim chk As New adamClass
    Dim nRet As Integer
    Dim strSQL As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ltlAlert.Text = ""
        If Not IsPostBack Then
            txtYear.Text = DateTime.Now.Year.ToString()
            dropmonth.SelectedValue = DateTime.Now.Month.ToString()
            dropCostBindData()
            BindData()
        End If

    End Sub

    Private Sub dropCostBindData()
        Dim strdomain As String
        Dim item As ListItem
        item = New ListItem("--")
        item.Value = 0
        dropCost.Items.Add(item)

        strSQL = " SELECT qad_domain FROM Domain_Mes WHERE plantCode='" & Session("Plantcode") & "' GROUP BY qad_domain"
        strdomain = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)

        Dim conn As OdbcConnection = Nothing
        Dim comm As OdbcCommand = Nothing
        Dim dr As OdbcDataReader
        'Dim i As Integer = 0

        Dim connectionString As String = ConfigurationManager.AppSettings("SqlConn.Conn9")

        strSQL = "SELECT cc_ctr, cc_desc FROM PUB.cc_mstr WHERE cc_active=1 AND cc_domain= '" & strdomain & "' ORDER BY cc_ctr "
        Try
            conn = New OdbcConnection(connectionString)
            conn.Open()
            comm = New OdbcCommand(strSQL, conn)
            dr = comm.ExecuteReader()
            While (dr.Read())
                item = New ListItem(dr.GetValue(1).ToString())
                item.Value = dr.GetValue(0).ToString()
                dropCost.Items.Add(item)
            End While
            dr.Close()
        Catch oe As OdbcException
            Response.Write(oe.Message)
        Finally
            If conn.State = System.Data.ConnectionState.Open Then
                conn.Close()
            End If
        End Try

        comm.Dispose()
        conn.Dispose()
    End Sub


    Private Sub BindData()
        strSQL = "SELECT wo_ID,wo_center as costcenter,wo_desc ,wo_price "
        strSQL &= " FROM wo_CostCenter "
        strSQL &= " WHERE deletedBy IS NULL "
        strSQL &= " AND YEAR(wo_date) = '" & txtYear.Text.Trim() & "' AND MONTH(wo_date)='" & dropmonth.SelectedValue & "' "
        If txtCostCenter.Text.Trim.Length > 0 Then
            strSQL &= " and LOWER(wo_center) like N'%" & txtCostCenter.Text.Trim.ToLower & "%' "
        End If
        If txtPrice.Text.Trim.Length > 0 Then
            strSQL &= " and LOWER(wo_price) like N'%" & txtPrice.Text.Trim.ToLower & "%' "
        End If
        strSQL &= " Order by wo_center "
        'Response.Write(strSQL)
        Dim reader As SqlDataReader
        reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL)

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("wo_id", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("costcenter", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("wo_desc", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("wo_price", System.Type.GetType("System.String")))

        Dim dr1 As DataRow
        While reader.Read
            dr1 = dt.NewRow()
            dr1.Item("wo_id") = reader(0).ToString().Trim()
            dr1.Item("costcenter") = reader(1).ToString().Trim()
            dr1.Item("wo_desc") = reader(2).ToString().Trim()
            dr1.Item("wo_price") = reader(3).ToString().Trim()
            dt.Rows.Add(dr1)
        End While
        reader.Close()

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

    Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandName.CompareTo("DeleteBtn") = 0) Then

            If Not Me.Security("358000121").isValid Then
                Response.Redirect("/public/Message.aspx?type=0", True)
            End If

            Dim strSQL As String
            strSQL = " UPDATE wo_CostCenter SET deletedBy='" & Session("uid") & "',deletedDate =getdate()   Where wo_id='" & e.Item.Cells(0).Text() & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
            DataGrid1.EditItemIndex = -1
            BindData()
        End If
    End Sub

    Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand

        Dim str1 As String
        str1 = CType(e.Item.Cells(3).Controls(0), TextBox).Text

        If (str1.Trim.Length = 0) Then
            ltlAlert.Text = "alert('标准单价不能为空.')"
            Exit Sub
        Else
            If Not IsNumeric(str1) Then
                ltlAlert.Text = "alert('标准单价只能为数字.')"
                Exit Sub
            End If
        End If



        strSQL = "UPDATE wo_CostCenter SET wo_price = '" & str1 & "' WHERE wo_id = '" & e.Item.Cells(0).Text() & "'"
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
        DataGrid1.EditItemIndex = -1
        BindData()
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand

        If Not Me.Security("358000121").isValid Then
            Response.Redirect("/public/Message.aspx?type=0", True)
        End If
        DataGrid1.EditItemIndex = e.Item.ItemIndex
        BindData()
    End Sub


    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Me.Security("358000121").isValid Then
            Response.Redirect("/public/Message.aspx?type=0", True)
        End If

        If (txtCostCenter.Text.Trim.Length = 0 Or txtPrice.Text.Trim.Length = 0) Then
            ltlAlert.Text = "alert('成本中心与标准单价不能为空 ');"
            Exit Sub
        End If
        Dim strdesc As String = ""
        Dim i As Integer
        Dim strdate = txtYear.Text.Trim & "-" & dropmonth.SelectedValue & "-01"

        strSQL = "SELECT wo_id FROM wo_CostCenter WHERE wo_center = '" & txtCostCenter.Text.Trim() & "' AND YEAR(wo_date) = '" & txtYear.Text.Trim() & "' AND MONTH(wo_date)='" & dropmonth.SelectedValue & "' AND deletedBy IS NULL "
        strdesc = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL)

        If strdesc Is Nothing Then
            strdesc = ""
            For i = 1 To dropCost.Items.Count - 1
                If dropCost.Items(i).Value = txtCostCenter.Text.Trim() Then
                    strdesc = dropCost.Items(i).Text
                    Exit For
                End If
            Next
            If strdesc.Trim.Length > 0 Then
                strSQL = "INSERT INTO wo_CostCenter(wo_center,wo_desc,wo_price,createdBy,createdDate,wo_date)"
                strSQL &= "VALUES('" & txtCostCenter.Text.Trim() & "',N'" & strdesc & "','" & txtPrice.Text.Trim() & "','" & Session("uid") & "',getdate(),'" & strdate & "')"
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                txtCostCenter.Text = ""
                txtPrice.Text = ""
                BindData()
            Else
                ltlAlert.Text = "alert('此成本中心不存在 ');"
            End If
        Else
            ltlAlert.Text = "alert('此成本中心的记录已存在 ');"
        End If
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        DataGrid1.CurrentPageIndex = 0
        BindData()
    End Sub
End Class
