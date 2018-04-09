Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc

Namespace tcpc

Partial Class pl_items
        Inherits BasePage
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    'Protected WithEvents ltlAlert As Literal
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
                btn_clear.Attributes.Add("onclick", "return confirm('确定要全部清除吗?');") 
            BindData()
        End If
    End Sub
    Private Sub BindData()
        Dim strSQL As String
        Dim ds As DataSet
            strSQL = " SELECT isnull(pl_item,''),isnull(pl_desc,''),isnull(pl_qty,0),id " _
                   & " From qadplan.dbo.pl_item where pl_userid='" & Session("uID") & "'" _
                   & " Order by pl_item"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            'Response.Write(strSQL)
            'Exit Sub

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("item", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("desc", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("qty", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("item") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("desc") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("qty") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("id") = .Rows(i).Item(3).ToString().Trim()
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            ds.Reset()

            Dim dv As DataView
            dv = New DataView(dt)
            DataGrid1.DataSource = dv
            DataGrid1.DataBind()
        End Sub
    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing ButtonColumn
                    myDeleteButton = e.Item.Cells(5)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除吗?');")

            Case ListItemType.AlternatingItem
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(5)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除吗?');")
            Case ListItemType.EditItem
                Dim myDeleteButton As TableCell
                'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(5)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除吗?');")
        End Select

    End Sub

    Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        DataGrid1.EditItemIndex = -1
        BindData()
    End Sub
    Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
            Dim strSQL As String
            Dim str As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            If (str.Trim.Length <> 14) Then
                ltlAlert.Text = "alert('请输入14位零件号.')"
                Exit Sub
            End If

            Dim str1 As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text

            Dim str2 As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text
            If Not IsNumeric(str2) Then
                ltlAlert.Text = "alert('请输入正确的数量.')"
                Exit Sub
            Else
                If str2.Length <= 0 Then
                    ltlAlert.Text = "alert('请输入数量.')"
                    Exit Sub
                End If
            End If

                strSQL = "update qadplan.dbo.pl_item set pl_item = '" & str & "',pl_desc=N'" & str1 & "',pl_qty='" & str2 & "' where id=" & e.Item.Cells(6).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                DataGrid1.EditItemIndex = -1
                BindData()
        End Sub

    Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
            Dim strSQL As String
            
                strSQL = "delete from qadplan.dbo.pl_item where id = '" & e.Item.Cells(6).Text() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)


                DataGrid1.EditItemIndex = -1
                BindData()
            End If
    End Sub


    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        Session("orderby") = e.SortExpression.ToString()
        If Session("orderdir") = " ASC" Then
            Session("orderdir") = " DESC"
        Else
            Session("orderdir") = " ASC"
        End If
        BindData()
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    
    Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        DataGrid1.EditItemIndex = e.Item.ItemIndex
        BindData()
    End Sub

        Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
            Dim strSQL As String
            strSQL = "Insert Into qadplan.dbo.pl_Item(pl_userid) Values(N'" & Session("uID") & "')"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            DataGrid1.EditItemIndex = 0
            BindData()
        End Sub

        Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click
            Dim strSQL As String
            strSQL = "Delete from qadplan.dbo.pl_Item Where pl_userid='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            DataGrid1.EditItemIndex = 0
            BindData()
        End Sub

        Protected Sub btn_pcb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pcb.Click
            'Exit Sub
            Dim strSQL As String
            Dim strSQL2 As String
            strSQL = "Delete from qadplan.dbo.pl_alloc where pl_userid='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            Dim ds As DataSet
            strSQL = " SELECT pl_item,pl_qty,id " _
                   & " From qadplan.dbo.pl_item where pl_userid='" & Session("uID") & "'" _
                   & " Order by pl_item"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Item(0).ToString().Trim().Substring(0, 4) = "2101" Then
                            strSQL2 = "Insert Into qadplan.dbo.pl_alloc(pl_userid,pl_par,pl_part,pl_comp,pl_qty,pl_level,pl_status,pl_is,pl_domain) "
                            strSQL2 &= " Values('" & Session("uID") & "','" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(0).ToString().Trim() & "'," & .Rows(i).Item(1) & ",0,'PROD',1,'" & ddl_site.SelectedValue & "')"
                        Else
                            strSQL2 = "Insert Into qadplan.dbo.pl_alloc(pl_userid,pl_par,pl_part,pl_comp,pl_qty,pl_level,pl_status,pl_is,pl_domain) "
                            strSQL2 &= " Values('" & Session("uID") & "','" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(0).ToString().Trim() & "'," & .Rows(i).Item(1) & ",0,'PROD',0,'" & ddl_site.SelectedValue & "')"
                        End If
                        'Response.Write(strSQL2)
                        'Exit Sub
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL2)

                        GetBomFromQadPCB(.Rows(i).Item(0).ToString().Trim(), 0)
                    Next
                End If
            End With
            ds.Reset()

            strSQL = "Delete from qadplan.dbo.pl_alloc where pl_userid='" & Session("uID") & "' and not (pl_is=1 and pl_status='P')"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            ltlAlert.Text = "var w=window.open('pl_exportExcel.aspx?ty=1&uid=" & Session("uID") & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"

        End Sub

        Function GetBomFromQadPCB(ByVal part As String, ByVal lel As Integer) As Boolean
            If lel > 9 Then
                Exit Function
            End If

            Dim strSQL3 As String

            Dim reader As SqlDataReader
            Dim bom_par As String
            Dim bom_part As String
            Dim bom_comp As String
            Dim qty As Decimal
            Dim id As Int64 = 0
            Dim isOK As Integer
            Dim isOK2 As Integer

            Dim bom_date As DateTime = Now

            strSQL3 = "Select id,pl_par,pl_part,pl_comp,isnull(pl_qty,0),isnull(pl_is,0)"
            strSQL3 &= " From qadplan.dbo.pl_alloc "
            strSQL3 &= " Where pl_userid='" & Session("uID") & "' and pl_level='" & lel & "' and pl_status ='PROD' and pl_comp='" & part & "'"
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSQL3)
            While reader.Read()
                id = reader(0).ToString()
                bom_par = reader(1).ToString()
                bom_part = reader(2).ToString.Substring(0, 14)
                bom_comp = reader(3).ToString.Substring(0, 14)
                qty = reader(4)
                isOK = reader(5)

                Dim str2 As String

                Dim conn As OdbcConnection
                Dim comm As OdbcCommand
                Dim dr As OdbcDataReader

                Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

                Dim sql As String = "Select ps_comp,ps_qty_per from PUB.ps_mstr where ps_domain='" & ddl_site.SelectedValue & "' and ps_par='" & bom_comp & "'"
                sql = sql & " and (ps_start<='" & bom_date & "' or ps_start is null) and (ps_end>='" & bom_date & "' or ps_end is null)"
                Try
                    conn = New OdbcConnection(connectionString)
                    conn.Open()
                    comm = New OdbcCommand(sql, conn)
                    dr = comm.ExecuteReader()
                    While (dr.Read())
                        If dr.GetValue(0).ToString().Length() = 14 Then
                            isOK2 = isOK
                            If isOK2 = 0 Then
                                If dr.GetValue(0).ToString().Substring(0, 4) = "2101" Then
                                    isOK2 = 1
                                End If
                            End If

                            str2 = "Insert Into qadplan.dbo.pl_alloc(pl_par,pl_part,pl_comp,pl_qty,pl_userid,pl_level,pl_status,pl_is,pl_domain) "
                            str2 &= " Values('" & bom_par & "','" & bom_comp & "','" & dr.GetValue(0).ToString() & "'," & dr.GetValue(1).ToString() * qty & ",'" & Session("uID") & "','" & lel + 1.ToString() & "','PROD'," & isOK2 & ",'" & ddl_site.SelectedValue & "')"
                            Try
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str2)
                            Catch
                                Response.Write(str2)
                                Exit Function
                            End Try

                            sql = "Update qadplan.dbo.pl_alloc set pl_status ='' where id='" & id & "'"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sql)

                            GetBomFromQadPCB(dr.GetValue(0).ToString(), lel + 1)
                        End If
                    End While
                    dr.Close()
                    conn.Close()

                Catch oe As OdbcException
                    Response.Write(oe.Message)
                Finally
                    If conn.State = System.Data.ConnectionState.Open Then
                        conn.Close()
                    End If
                End Try

                comm.Dispose()
                conn.Dispose()

                sql = "Update qadplan.dbo.pl_alloc set pl_status ='P' where id='" & id & "' and pl_status='PROD'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sql) 
            End While
            reader.Close()

        End Function
        Protected Sub btn_tube_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_tube.Click
            'Exit Sub
            Dim strSQL As String
            Dim strSQL2 As String
            strSQL = "Delete from qadplan.dbo.pl_alloc where pl_userid='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            Dim ds As DataSet
            strSQL = " SELECT pl_item,pl_qty,id " _
                   & " From qadplan.dbo.pl_item where pl_userid='" & Session("uID") & "'" _
                   & " Order by pl_item"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Item(0).ToString().Trim().Substring(0, 3) = "220" Then
                            strSQL2 = "Insert Into qadplan.dbo.pl_alloc(pl_userid,pl_par,pl_part,pl_comp,pl_qty,pl_level,pl_status,pl_is,pl_domain) "
                            strSQL2 &= " Values('" & Session("uID") & "','" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(0).ToString().Trim() & "'," & .Rows(i).Item(1) & ",0,'PROD',1,'" & ddl_site.SelectedValue & "')"
                        Else
                            strSQL2 = "Insert Into qadplan.dbo.pl_alloc(pl_userid,pl_par,pl_part,pl_comp,pl_qty,pl_level,pl_status,pl_is,pl_domain) "
                            strSQL2 &= " Values('" & Session("uID") & "','" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(0).ToString().Trim() & "'," & .Rows(i).Item(1) & ",0,'PROD',0,'" & ddl_site.SelectedValue & "')"
                        End If
                        'Response.Write(strSQL2)
                        'Exit Sub
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL2)

                        GetBomFromQadTube(.Rows(i).Item(0).ToString().Trim(), 0)
                    Next
                End If
            End With
            ds.Reset()

            strSQL = "Delete from qadplan.dbo.pl_alloc where pl_userid='" & Session("uID") & "' and  not (pl_is=1 and pl_status='P')"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

            ltlAlert.Text = "var w=window.open('pl_exportExcel.aspx?ty=2&uid=" & Session("uID") & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub
        Function GetBomFromQadTube(ByVal part As String, ByVal lel As Integer) As Boolean
            If lel > 9 Then
                Exit Function
            End If

            Dim strSQL3 As String

            Dim reader As SqlDataReader
            Dim bom_par As String
            Dim bom_part As String
            Dim bom_comp As String
            Dim qty As Decimal
            Dim id As Int64 = 0
            Dim isOK As Integer
            Dim isOK2 As Integer

            Dim bom_date As DateTime = Now

            strSQL3 = "Select id,pl_par,pl_part,pl_comp,isnull(pl_qty,0),isnull(pl_is,0)"
            strSQL3 &= " From qadplan.dbo.pl_alloc "
            strSQL3 &= " Where pl_userid='" & Session("uID") & "' and pl_level='" & lel & "' and pl_status ='PROD' and pl_comp='" & part & "'"
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSQL3)
            While reader.Read()
                id = reader(0).ToString()
                bom_par = reader(1).ToString()
                bom_part = reader(2).ToString.Substring(0, 14)
                bom_comp = reader(3).ToString.Substring(0, 14)
                qty = reader(4)
                isOK = reader(5)

                Dim str2 As String

                Dim conn As OdbcConnection
                Dim comm As OdbcCommand
                Dim dr As OdbcDataReader

                Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

                Dim sql As String = "Select ps_comp,ps_qty_per from PUB.ps_mstr where ps_domain='" & ddl_site.SelectedValue & "' and ps_par='" & bom_comp & "'"
                sql = sql & " and (ps_start<='" & bom_date & "' or ps_start is null) and (ps_end>='" & bom_date & "' or ps_end is null)"
                Try
                    conn = New OdbcConnection(connectionString)
                    conn.Open()
                    comm = New OdbcCommand(sql, conn)
                    dr = comm.ExecuteReader()
                    While (dr.Read())
                        If dr.GetValue(0).ToString().Length() = 14 Then
                            isOK2 = isOK
                            If isOK2 = 0 Then
                                If dr.GetValue(0).ToString().Substring(0, 3) = "220" Then
                                    isOK2 = 1
                                End If
                            End If

                            str2 = "Insert Into qadplan.dbo.pl_alloc(pl_par,pl_part,pl_comp,pl_qty,pl_userid,pl_level,pl_status,pl_is,pl_domain) "
                            str2 &= " Values('" & bom_par & "','" & bom_comp & "','" & dr.GetValue(0).ToString() & "'," & dr.GetValue(1).ToString() * qty & ",'" & Session("uID") & "','" & lel + 1.ToString() & "','PROD'," & isOK2 & ",'" & ddl_site.SelectedValue & "')"
                            Try
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str2)
                            Catch
                                Response.Write(lel.ToString() & str2)
                                Exit Function
                            End Try

                            sql = "Update qadplan.dbo.pl_alloc set pl_status ='' where id='" & id & "'"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sql)

                            GetBomFromQadTube(dr.GetValue(0).ToString(), lel + 1)
                        End If
                    End While
                    dr.Close()
                    conn.Close()

                Catch oe As OdbcException
                    Response.Write(oe.Message)
                Finally
                    If conn.State = System.Data.ConnectionState.Open Then
                        conn.Close()
                    End If
                End Try

                comm.Dispose()
                conn.Dispose()

                sql = "Update qadplan.dbo.pl_alloc set pl_status ='P' where id='" & id & "' and pl_status='PROD'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sql)


            End While
            reader.Close()

        End Function
    End Class

End Namespace
