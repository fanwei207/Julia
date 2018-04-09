Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class productUPCInfo
        Inherits BasePage
        Protected strTemp As String
        Protected Plantcode As String
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
                Plantcode = Request("Plantcode")
                BindData()
            End If
        End Sub
        Private Sub BindData()
            Dim strSQL As String
            Dim ds As DataSet
            Plantcode = Request("Plantcode")
            If Plantcode = "TCB" Then
                strSQL = " SELECT sc.systemCodeID,sc.comments,sc.systemCodeName From tcpc0.dbo.systemCode sc " _
                       & " Inner Join tcpc0.dbo.systemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID " _
                       & " Where sct.systemCodeTypeName='product Serial Number' Order by sc.systemCodeID"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

                Dim dt1 As New DataTable
                dt1.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
                dt1.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
                dt1.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        Dim dr1 As DataRow
                        For i = 0 To .Rows.Count - 1
                            dr1 = dt1.NewRow()
                            dr1.Item("gsort") = .Rows(i).Item(1).ToString().Trim()
                            dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                            dr1.Item("Name") = .Rows(i).Item(2).ToString().Trim()
                            dt1.Rows.Add(dr1)
                        Next
                    End If
                End With
                ds.Reset()

                Dim dv1 As DataView
                dv1 = New DataView(dt1)
                Try
                    DataGrid1.DataSource = dv1
                    DataGrid1.DataBind()
                Catch
                End Try

                strSQL = " SELECT sc.systemCodeID,sc.comments,sc.systemCodeName From tcpc0.dbo.systemCode sc " _
               & " Inner Join tcpc0.dbo.systemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID " _
               & " Where sct.systemCodeTypeName='package Number' Order by sc.systemCodeID"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

                Dim dt2 As New DataTable
                dt2.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
                dt2.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
                dt2.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        Dim dr1 As DataRow
                        For i = 0 To .Rows.Count - 1
                            dr1 = dt2.NewRow()
                            dr1.Item("gsort") = .Rows(i).Item(1).ToString().Trim()
                            dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                            dr1.Item("Name") = .Rows(i).Item(2).ToString().Trim()
                            dt2.Rows.Add(dr1)
                        Next
                    End If
                End With
                ds.Reset()
                Dim dv2 As DataView
                dv2 = New DataView(dt2)

                Try
                    Datagrid2.DataSource = dv2
                    Datagrid2.DataBind()
                Catch
                End Try
            Else
                strSQL = " SELECT sc.systemCodeID,sc.comments,sc.systemCodeName From tcpc0.dbo.systemCode sc " _
                      & " Inner Join tcpc0.dbo.systemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID " _
                      & " Where sct.systemCodeTypeName='product Serial NumberTW' Order by sc.systemCodeID"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

                Dim dt1 As New DataTable
                dt1.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
                dt1.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
                dt1.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        Dim dr1 As DataRow
                        For i = 0 To .Rows.Count - 1
                            dr1 = dt1.NewRow()
                            dr1.Item("gsort") = .Rows(i).Item(1).ToString().Trim()
                            dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                            dr1.Item("Name") = .Rows(i).Item(2).ToString().Trim()
                            dt1.Rows.Add(dr1)
                        Next
                    End If
                End With
                ds.Reset()

                Dim dv1 As DataView
                dv1 = New DataView(dt1)
                Try
                    DataGrid1.DataSource = dv1
                    DataGrid1.DataBind()
                Catch
                End Try

                strSQL = " SELECT sc.systemCodeID,sc.comments,sc.systemCodeName From tcpc0.dbo.systemCode sc " _
               & " Inner Join tcpc0.dbo.systemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID " _
               & " Where sct.systemCodeTypeName='package NumberTW' Order by sc.systemCodeID"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

                Dim dt2 As New DataTable
                dt2.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
                dt2.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
                dt2.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        Dim dr1 As DataRow
                        For i = 0 To .Rows.Count - 1
                            dr1 = dt2.NewRow()
                            dr1.Item("gsort") = .Rows(i).Item(1).ToString().Trim()
                            dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                            dr1.Item("Name") = .Rows(i).Item(2).ToString().Trim()
                            dt2.Rows.Add(dr1)
                        Next
                    End If
                End With
                ds.Reset()
                Dim dv2 As DataView
                dv2 = New DataView(dt2)

                Try
                    Datagrid2.DataSource = dv2
                    Datagrid2.DataBind()
                Catch
                End Try
            End If


        End Sub
        Public Sub Edit_cancel(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub
        Public Sub Edit_update(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
            Dim strSQL As String
            Dim str As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空.')"
                Exit Sub
            End If
            Plantcode = Request("Plantcode")
            If Plantcode = "TCB" Then
                Dim strCheck As String = "select * from tcpc0.dbo.systemCode where systemCodeName=N'" & str & "'  and systemCodeID<>" & e.Item.Cells(3).Text & "  and systemCodeTypeId=39"
                Dim dta As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strCheck).Tables(0)
                If (dta.Rows.Count > 0) Then
                    ltlAlert.Text = "alert('此名称已经存在，无须再添加！')"
                    Exit Sub
                End If
                strSQL = "update tcpc0.dbo.systemCode set systemCodeName=N'" & str & "' where systemCodeID=" & e.Item.Cells(3).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = -1
                BindData()
            Else
                Dim strCheck As String = "select * from tcpc0.dbo.systemCode where systemCodeName=N'" & str & "'  and systemCodeID<>" & e.Item.Cells(3).Text & "  and systemCodeTypeId=44"
                Dim dta As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strCheck).Tables(0)
                If (dta.Rows.Count > 0) Then
                    ltlAlert.Text = "alert('此名称已经存在，无须再添加！')"
                    Exit Sub
                End If
                strSQL = "update tcpc0.dbo.systemCode set systemCodeName=N'" & str & "' where systemCodeID=" & e.Item.Cells(3).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                DataGrid1.EditItemIndex = -1
                BindData()
            End If

        End Sub
        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Public Sub Edit_cancel2(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid2.CancelCommand
            Datagrid2.EditItemIndex = -1
            BindData()
        End Sub
        Public Sub Edit_update2(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid2.UpdateCommand
            Dim strSQL As String
            Dim str As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            If (str.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('名称不能为空.')"
                Exit Sub
            End If
            Plantcode = Request("Plantcode")
            If Plantcode = "TCB" Then
                Dim strCheck As String = "select * from tcpc0.dbo.systemCode where systemCodeName=N'" & str & "' and systemCodeID<>" & e.Item.Cells(3).Text & "and systemcodetypeid=40"
                Dim dta As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strCheck).Tables(0)
                If (dta.Rows.Count > 0) Then
                    ltlAlert.Text = "alert('此名称已经存在，无须再添加！')"
                    Exit Sub
                End If
                strSQL = "update tcpc0.dbo.systemCode set systemCodeName=N'" & str & "' where systemCodeID=" & e.Item.Cells(3).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                Datagrid2.EditItemIndex = -1
                BindData()
            Else
                Dim strCheck As String = "select * from tcpc0.dbo.systemCode where systemCodeName=N'" & str & "' and systemCodeID<>" & e.Item.Cells(3).Text & "and systemcodetypeid=45"
                Dim dta As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strCheck).Tables(0)
                If (dta.Rows.Count > 0) Then
                    ltlAlert.Text = "alert('此名称已经存在，无须再添加！')"
                    Exit Sub
                End If
                strSQL = "update tcpc0.dbo.systemCode set systemCodeName=N'" & str & "' where systemCodeID=" & e.Item.Cells(3).Text
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                Datagrid2.EditItemIndex = -1
                BindData()
            End If

        End Sub
        Private Sub DataGrid2_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid2.EditCommand
            Datagrid2.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.EditItem
                    CType(e.Item.Cells(1).Controls(0), TextBox).Width = New Unit(480)
            End Select
        End Sub

        Private Sub Datagrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.EditItem
                    CType(e.Item.Cells(1).Controls(0), TextBox).Width = New Unit(510)
            End Select
        End Sub

        Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            If (txtType.Text.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('产品类型名称不能为空.')"
                Exit Sub
            End If
            Plantcode = Request("Plantcode")
            If Plantcode = "TCB" Then
                Dim sql2 As String = "select * from tcpc0.dbo.systemCode where systemCodeName=N'" & txtType.Text.Trim & "' and systemcodetypeid=39"
                Dim dt2 As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sql2).Tables(0)
                If (dt2.Rows.Count > 0) Then
                    ltlAlert.Text = "alert('此类型名称已经存在，无须再次添加！')"
                    Exit Sub
                End If
                Dim sqlCheck As String = "SELECT sc.comments From tcpc0.dbo.systemCode sc "
                sqlCheck &= " Inner Join tcpc0.dbo.systemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID  "
                sqlCheck &= " Where sct.systemCodeTypeName='product Serial Number' Order by sc.systemCodeID "
                Dim dt As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sqlCheck).Tables(0)
                Dim comm As String = "" & (dt.Rows.Count + 1)
                Dim sql As String = "insert into tcpc0.dbo.systemCode (SystemCodeTypeId,systemCodeName,comments) values(39,N'" & txtType.Text.Trim & "',N'" & comm & "')"
                Dim flag As Integer = SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sql)
                If (flag > 0) Then
                    ltlAlert.Text = "alert('添加成功!')"
                Else
                    ltlAlert.Text = "alert('添加失败!')"
                End If
                txtType.Text = ""
                BindData()
            Else
                Dim sql2 As String = "select * from tcpc0.dbo.systemCode where systemCodeName=N'" & txtType.Text.Trim & "' and systemcodetypeid=44"
                Dim dt2 As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sql2).Tables(0)
                If (dt2.Rows.Count > 0) Then
                    ltlAlert.Text = "alert('此类型名称已经存在，无须再次添加！')"
                    Exit Sub
                End If
                Dim sqlCheck As String = "SELECT sc.comments From tcpc0.dbo.systemCode sc "
                sqlCheck &= " Inner Join tcpc0.dbo.systemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID  "
                sqlCheck &= " Where sct.systemCodeTypeName='product Serial NumberTW' Order by sc.systemCodeID "
                Dim dt As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sqlCheck).Tables(0)
                Dim comm As String = "" & (dt.Rows.Count + 1)
                Dim sql As String = "insert into tcpc0.dbo.systemCode (SystemCodeTypeId,systemCodeName,comments) values(44,N'" & txtType.Text.Trim & "',N'" & comm & "')"
                Dim flag As Integer = SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sql)
                If (flag > 0) Then
                    ltlAlert.Text = "alert('添加成功!')"
                Else
                    ltlAlert.Text = "alert('添加失败!')"
                End If
                txtType.Text = ""
                BindData()
            End If

        End Sub

        Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
                DataGrid1.CurrentPageIndex = e.NewPageIndex
                BindData()
        End Sub

        Protected Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteClick") = 0) Then
                Plantcode = Request("Plantcode")
                If Plantcode = "TCB" Then
                    Dim str1 As String = "select count(*)  from  tcpc0.dbo.ProductUPC p"
                    str1 &= " inner join  tcpc0.dbo.systemCode sc1 on sc1.comments=cast(p.productSerialNumber as varchar)"
                    str1 &= " INNER JOIN tcpc0.dbo.systemCodeType sct1 ON sct1.systemCodeTypeID=sc1.systemCodeTypeID "
                    str1 &= " WHERE sct1.systemCodeTypeName='product Serial Number' and sc1.systemCodeTypeID=39  and sc1.systemCodeName=N'" & e.Item.Cells(1).Text & " '"
                    If Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, str1)) Then
                        ltlAlert.Text = "alert('存在使用记录，不能删除！')"
                        Exit Sub
                    End If
                    Dim str2 As String = "delete from tcpc0.dbo.systemCode where systemCodeID=" & e.Item.Cells(3).Text
                    Dim flag As Integer = SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str2)
                    If (flag > 0) Then
                        ltlAlert.Text = "alert('删除成功！')"
                        If (DataGrid1.Items.Count = 1) Then
                            If (DataGrid1.CurrentPageIndex = 0) Then
                                DataGrid1.CurrentPageIndex = 0
                            Else
                                DataGrid1.CurrentPageIndex -= 1
                            End If
                        End If
                    Else
                        ltlAlert.Text = "alert('删除失败！')"
                    End If
                    BindData()
                Else
                    Dim str1 As String = "select count(*)  from  tcpc0.dbo.ProductUPC p"
                    str1 &= " inner join  tcpc0.dbo.systemCode sc1 on sc1.comments=cast(p.productSerialNumber as varchar)"
                    str1 &= " INNER JOIN tcpc0.dbo.systemCodeType sct1 ON sct1.systemCodeTypeID=sc1.systemCodeTypeID "
                    str1 &= " WHERE sct1.systemCodeTypeName='product Serial Number' and sc1.systemCodeTypeID=44  and sc1.systemCodeName=N'" & e.Item.Cells(1).Text & " '"
                    If Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, str1)) Then
                        ltlAlert.Text = "alert('存在使用记录，不能删除！')"
                        Exit Sub
                    End If
                    Dim str2 As String = "delete from tcpc0.dbo.systemCode where systemCodeID=" & e.Item.Cells(3).Text
                    Dim flag As Integer = SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str2)
                    If (flag > 0) Then
                        ltlAlert.Text = "alert('删除成功！')"
                        If (DataGrid1.Items.Count = 1) Then
                            If (DataGrid1.CurrentPageIndex = 0) Then
                                DataGrid1.CurrentPageIndex = 0
                            Else
                                DataGrid1.CurrentPageIndex -= 1
                            End If
                        End If
                    Else
                        ltlAlert.Text = "alert('删除失败！')"
                    End If
                    BindData()
                End If

            End If
        End Sub
        Protected Sub btnAddPackType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddPackType.Click
            If (txtPack.Text.Trim.Length <= 0) Then
                ltlAlert.Text = "alert('包装类型名称不能为空.')"
                Exit Sub
            End If
            Plantcode = Request("Plantcode")
            If Plantcode = "TCB" Then
                Dim sql2 As String = "select * from tcpc0.dbo.systemCode where systemCodeName=N'" & txtPack.Text.Trim & "' and systemcodetypeid=40"
                Dim dt2 As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sql2).Tables(0)
                If (dt2.Rows.Count > 0) Then
                    ltlAlert.Text = "alert('此包装类型名称已经存在，无须再次添加！')"
                    Exit Sub
                End If
                Dim sqlCheck As String = "SELECT sc.comments From tcpc0.dbo.systemCode sc "
                sqlCheck &= " Inner Join tcpc0.dbo.systemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID  "
                sqlCheck &= " Where sct.systemCodeTypeName='package Number' Order by sc.systemCodeID "
                Dim dt As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sqlCheck).Tables(0)
                Dim comm As String = "" & (dt.Rows.Count + 1)           '新记录的编号comments
                Dim sql As String = "insert into tcpc0.dbo.systemCode (SystemCodeTypeId,systemCodeName,comments) values(40,N'" & txtPack.Text.Trim & "',N'" & comm & "')"
                Dim flag As Integer = SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sql)
                If (flag > 0) Then
                    ltlAlert.Text = "alert('添加成功!')"
                Else
                    ltlAlert.Text = "alert('添加失败!')"
                End If
                txtPack.Text = ""
                BindData()
            Else
                Dim sql2 As String = "select * from tcpc0.dbo.systemCode where systemCodeName=N'" & txtPack.Text.Trim & "' and systemcodetypeid=45"
                Dim dt2 As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sql2).Tables(0)
                If (dt2.Rows.Count > 0) Then
                    ltlAlert.Text = "alert('此包装类型名称已经存在，无须再次添加！')"
                    Exit Sub
                End If
                Dim sqlCheck As String = "SELECT sc.comments From tcpc0.dbo.systemCode sc "
                sqlCheck &= " Inner Join tcpc0.dbo.systemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID  "
                sqlCheck &= " Where sct.systemCodeTypeName='package NumberTW' Order by sc.systemCodeID "
                Dim dt As DataTable = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, sqlCheck).Tables(0)
                Dim comm As String = "" & (dt.Rows.Count + 1)           '新记录的编号comments
                Dim sql As String = "insert into tcpc0.dbo.systemCode (SystemCodeTypeId,systemCodeName,comments) values(45,N'" & txtPack.Text.Trim & "',N'" & comm & "')"
                Dim flag As Integer = SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sql)
                If (flag > 0) Then
                    ltlAlert.Text = "alert('添加成功!')"
                Else
                    ltlAlert.Text = "alert('添加失败!')"
                End If
                txtPack.Text = ""
                BindData()
            End If
        End Sub

        Protected Sub Datagrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

       
        Protected Sub Datagrid2_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid2.ItemCommand
            If e.CommandName.CompareTo("DeleteClick") = 0 Then
                Plantcode = Request("Plantcode")
                If Plantcode = "TCB" Then
                    Dim str1 As String = "select count(*)  from  tcpc0.dbo.ProductUPC p"
                    str1 &= " inner join  tcpc0.dbo.systemCode sc1 on sc1.comments=cast(p.productSerialNumber as varchar)"
                    str1 &= " INNER JOIN tcpc0.dbo.systemCodeType sct1 ON sct1.systemCodeTypeID=sc1.systemCodeTypeID "
                    str1 &= " WHERE sct1.systemCodeTypeName='package Number' and sc1.systemCodeTypeID=40  and sc1.systemCodeName=N'" & e.Item.Cells(1).Text & " '"
                    If Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, str1)) Then
                        ltlAlert.Text = "alert('存在使用记录，不能删除！')"
                        Exit Sub
                    End If
                    Dim str2 As String = "delete from tcpc0.dbo.systemCode where systemCodeID=" & e.Item.Cells(3).Text
                    Dim flag As Integer = SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str2)
                    If (flag > 0) Then
                        ltlAlert.Text = "alert('删除成功！')"
                        If (Datagrid2.Items.Count = 1) Then
                            If (Datagrid2.CurrentPageIndex = 0) Then
                                Datagrid2.CurrentPageIndex = 0
                            Else
                                Datagrid2.CurrentPageIndex -= 1
                            End If
                        End If
                    Else
                        ltlAlert.Text = "alert('删除失败！')"
                    End If
                    BindData()
                Else
                    Dim str1 As String = "select count(*)  from  tcpc0.dbo.ProductUPC p"
                    str1 &= " inner join  tcpc0.dbo.systemCode sc1 on sc1.comments=cast(p.productSerialNumber as varchar)"
                    str1 &= " INNER JOIN tcpc0.dbo.systemCodeType sct1 ON sct1.systemCodeTypeID=sc1.systemCodeTypeID "
                    str1 &= " WHERE sct1.systemCodeTypeName='package Number' and sc1.systemCodeTypeID=45  and sc1.systemCodeName=N'" & e.Item.Cells(1).Text & " '"
                    If Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, str1)) Then
                        ltlAlert.Text = "alert('存在使用记录，不能删除！')"
                        Exit Sub
                    End If
                    Dim str2 As String = "delete from tcpc0.dbo.systemCode where systemCodeID=" & e.Item.Cells(3).Text
                    Dim flag As Integer = SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str2)
                    If (flag > 0) Then
                        ltlAlert.Text = "alert('删除成功！')"
                        If (Datagrid2.Items.Count = 1) Then
                            If (Datagrid2.CurrentPageIndex = 0) Then
                                Datagrid2.CurrentPageIndex = 0
                            Else
                                Datagrid2.CurrentPageIndex -= 1
                            End If
                        End If
                    Else
                        ltlAlert.Text = "alert('删除失败！')"
                    End If
                    BindData()
                End If

            End If
        End Sub

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Plantcode = Request("Plantcode")
            If Plantcode = "TCB" Then
                Response.Redirect("/upcProgram/productUPC.aspx")
            Else
                Response.Redirect("/upcProgram/productUPCTQL.aspx")
            End If

        End Sub
    End Class

End Namespace
