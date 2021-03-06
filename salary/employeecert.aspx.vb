Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration


Namespace tcpc

    Partial Class employeecert
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then
                BtnModify.Enabled = False
                binddata()
            End If
        End Sub

        Sub binddata()
            strSql = "select id, userno,username,certname,pos,begindate,enddate,memo from tcpc0.dbo.employeecert where plantcode = '" & Session("plantcode") & "'and active = '1'"
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
            BtnAdd.Enabled = True
        End Sub

        Private Sub btnser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSer.Click
            datagrid1.CurrentPageIndex = 0
            datagrid1.SelectedIndex = -1
            binddata()
        End Sub

        Private Sub Btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
            txbNO.Text = ""
            txbName.Text = ""
            Txbpos.Text = ""
            Txbcert.Text = ""
            Txbmemo.Text = ""
            Txbbegin.Text = ""
            Txbend.Text = ""
            BtnAdd.Enabled = True
            datagrid1.SelectedIndex = -1
        End Sub

        Private Sub datagrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles datagrid1.EditCommand
            datagrid1.EditItemIndex = e.Item.ItemIndex
            binddata()
        End Sub


        Private Sub Btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click

            If txbNO.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('工号 不能为空！');"
                Exit Sub
            End If

            If Txbpos.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('岗位 不能为空！');"
                Exit Sub
            End If

            If Txbcert.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('证书名称 不能为空！');"
                Exit Sub
            End If

            If Txbbegin.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('证书起始日期 不能为空！');"
                Exit Sub
            Else
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(Txbbegin.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('证书起始日期 格式不正确！');"
                    Exit Sub
                End Try
            End If

            If Txbend.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('证书到期日期 不能为空！');"
                Exit Sub
            Else
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(Txbend.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('证书到期日期 格式不正确！');"
                    Exit Sub
                End Try
            End If

            If Convert.ToDateTime(Txbbegin.Text.Trim) > Convert.ToDateTime(Txbend.Text.Trim) Then
                ltlAlert.Text = "alert('到期日期不能等于小于起始日期！');"
                Exit Sub
            End If

            strSql = "select count(id) from tcpc0.dbo.employeecert where userno =N'" & txbNO.Text.Trim & "' and username = N'" & txbName.Text.Trim & "' and  certname = N'" & Txbcert.Text.Trim & "' and pos = N'" & Txbpos.Text.Trim & "' and begindate = '" & Txbbegin.Text.Trim & "' and enddate = '" & Txbend.Text.Trim & "' and memo = N'" & Txbmemo.Text.Trim & "' and plantcode= '" & Session("plantcode") & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) Then
                ltlAlert.Text = "alert('相同记录已存在')"
            Else
                strSql = "insert into tcpc0.dbo.employeecert (userNO,username,certname,pos,begindate,enddate,memo,plantcode,active) values (N'" & txbNO.Text.Trim & "',N'" & txbName.Text.Trim & "',N'" & Txbcert.Text.Trim & "',N'" & Txbpos.Text.Trim & "','" & Txbbegin.Text.Trim & "','" & Txbend.Text.Trim & "',N'" & Txbmemo.Text.Trim & "','" & Session("plantcode") & "','1')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                txbNO.Text = ""
                txbName.Text = ""
                Txbpos.Text = ""
                Txbcert.Text = ""
                Txbmemo.Text = ""
                Txbbegin.Text = ""
                Txbend.Text = ""
                datagrid1.CurrentPageIndex = 0
                datagrid1.SelectedIndex = -1
                binddata()
            End If

        End Sub

        Private Sub txbNO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txbNO.TextChanged
            strSql = "select count(username) from tcpc0.dbo.users where userno = '" & txbNO.Text.Trim & "' and plantcode = '" & Session("plantcode") & "' and leavedate is null and deleted=0 and isActive=1 "
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql) Then
                strSql = "select username from tcpc0.dbo.users where userno = '" & txbNO.Text.Trim & "' and plantcode = '" & Session("plantcode") & "'"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
                While (reader.Read())
                    txbName.Text = reader(0)
                End While
                reader.Close()
            Else
                ltlAlert.Text = "alert('此工号不存在或过期!');Form1.txbNO.focus();"
                Exit Sub
            End If
        End Sub

        Private Sub datagrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles datagrid1.ItemCreated

        End Sub

        Private Sub datagrid1_DeleteCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles datagrid1.ItemCommand
            If e.CommandName.CompareTo("DeleteClick") = 0 Then
                strSql = "delete from tcpc0.dbo.employeecert where id =' " & e.Item.Cells(0).Text.Trim() & "' and plantcode = '" & Session("plantcode") & "'"
                'Response.Write(strSql)
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                binddata()
                datagrid1.CurrentPageIndex = 0
                Exit Sub
            End If

            If e.CommandName.CompareTo("Select") = 0 Then
                txbid.Text = e.Item.Cells(0).Text.Trim()
                txbNO.Text = e.Item.Cells(1).Text.Trim()
                txbName.Text = e.Item.Cells(2).Text.Trim()
                Txbpos.Text = e.Item.Cells(3).Text.Trim()
                Txbcert.Text = e.Item.Cells(4).Text.Trim()
                Txbbegin.Text = e.Item.Cells(5).Text.Trim()
                Txbend.Text = e.Item.Cells(6).Text.Trim()
                If e.Item.Cells(7).Text.Trim() <> "&nbsp;" Then
                    Txbmemo.Text = e.Item.Cells(7).Text.Trim()
                End If

                BtnModify.Enabled = True
                BtnAdd.Enabled = False
                Exit Sub
            End If
        End Sub

        Private Sub Btnmodify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModify.Click

            If txbNO.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('工号 不能为空！');"
                Exit Sub
            End If

            If Txbpos.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('岗位 不能为空！');"
                Exit Sub
            End If

            If Txbcert.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('证书名称 不能为空！');"
                Exit Sub
            End If

            If Txbbegin.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('证书起始日期 不能为空！');"
                Exit Sub
            Else
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(Txbbegin.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('证书起始日期 格式不正确！');"
                    Exit Sub
                End Try
            End If

            If Txbend.Text.Trim.Length = 0 Then
                ltlAlert.Text = "alert('证书到期日期 不能为空！');"
                Exit Sub
            Else
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(Txbend.Text.Trim)
                Catch ex As Exception
                    ltlAlert.Text = "alert('证书到期日期 格式不正确！');"
                    Exit Sub
                End Try
            End If

            If Convert.ToDateTime(Txbbegin.Text.Trim) > Convert.ToDateTime(Txbend.Text.Trim) Then
                ltlAlert.Text = "alert('到期日期不能等于小于起始日期！');"
                Exit Sub
            End If

            strSql = "update  tcpc0.dbo.employeecert set userno =N'" & txbNO.Text.Trim & "' , username = N'" & txbName.Text.Trim & "' , certname = N'" & Txbcert.Text.Trim & "' , pos = N'" & Txbpos.Text.Trim & "' , begindate = '" & Txbbegin.Text.Trim & "' , enddate = '" & Txbend.Text.Trim & "' , memo = N'" & Txbmemo.Text.Trim & "' , plantcode= '" & Session("plantcode") & "' where id = '" & txbid.Text.Trim & "'"
            'Response.Write(strSql)
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            txbNO.Text = ""
            txbName.Text = ""
            Txbpos.Text = ""
            Txbcert.Text = ""
            Txbmemo.Text = ""
            Txbbegin.Text = ""
            Txbend.Text = ""
            datagrid1.CurrentPageIndex = 0
            datagrid1.SelectedIndex = -1
            binddata()
            BtnAdd.Enabled = True
            BtnModify.Enabled = False
        End Sub

        Protected Sub datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles datagrid1.PageIndexChanged
            If datagrid1.SelectedIndex <> -1 Then
                ltlAlert.Text = "alert('请先取消当前选中行！')"
            Else
                datagrid1.CurrentPageIndex = e.NewPageIndex
                binddata()
            End If
        End Sub
    End Class

End Namespace
