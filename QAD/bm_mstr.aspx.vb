'*@     Create By   :   Ye Bin    
'*@     Create Date :   2008-12-24
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   BOM_MSTR Add And List
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc


    Partial Class bm_mstr
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim dst As DataSet
        Dim nRet As Integer
        Protected WithEvents btn_next As System.Web.UI.WebControls.Button
        Protected WithEvents btn_close As System.Web.UI.WebControls.Button
        Protected WithEvents bnt_cancel As System.Web.UI.WebControls.Button


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
                txt_desc.Text = ""
                txt_bmid.Text = ""
                btn_Add.Text = "新建修改单"

                BindData()
            End If
        End Sub

        Sub BindData()
            StrSql = "select bm_mstr_id, bm_desc, bm_comment, bm_createdDate, bm_closedDate, Isnull(bm_closedBy,''), bm_createdName from bm_mstr where Isnull(bm_deletedBy,'') = '' order by bm_createdDate desc"
            dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("bm_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("bm_status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("sdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("edate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("bmdesc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("bmcommet", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("bm_id") = .Rows(i).Item(0).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(3)) = False Then
                            drow.Item("sdate") = Format(.Rows(i).Item(3), "yy-MM-dd")
                        Else
                            drow.Item("sdate") = ""
                        End If
                        If IsDBNull(.Rows(i).Item(4)) = False Then
                            drow.Item("edate") = Format(.Rows(i).Item(4), "yy-MM-dd")
                        Else
                            drow.Item("edate") = ""
                        End If
                        drow.Item("bmdesc") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("bmcommet") = .Rows(i).Item(2).ToString().Trim()

                        If .Rows(i).Item(5) <> "" Then
                            drow.Item("bm_status") = "已关闭"
                        Else
                            drow.Item("bm_status") = "新创建"
                        End If

                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            dst.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            dgBM.DataSource = dvw
            dgBM.DataBind()


        End Sub

        Private Sub dgBM_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBM.ItemCommand
            If e.CommandName.CompareTo("BMImport") = 0 Then
                If Not Security("87200004").isValid Then
                    ltlAlert.Text = "alert('没有导入修改单的权限！');"
                    Exit Sub
                End If
                If e.Item.Cells(1).Text.Trim() = "已关闭" Then
                    ltlAlert.Text = "alert('修改单已关闭不能再导入变更申请！');"
                    Exit Sub
                End If
                Response.Redirect("bm_mstr_import.aspx?bmid=" & e.Item.Cells(0).Text.Trim(), True)
            ElseIf e.CommandName.CompareTo("BDImport") = 0 Then
                If Not Security("87200005").isValid Then
                    ltlAlert.Text = "alert('没有导入修改单数据的权限！');"
                    Exit Sub
                End If
                If e.Item.Cells(1).Text.Trim() = "已关闭" Then
                    ltlAlert.Text = "alert('修改单已关闭不能再导入变更数量！');"
                    Exit Sub
                End If
                Response.Redirect("bm_detail_import.aspx?bmid=" & e.Item.Cells(0).Text.Trim(), True)
            ElseIf e.CommandName.CompareTo("bm_edit") = 0 Then
                If e.Item.Cells(1).Text.Trim() = "已关闭" Then
                    ltlAlert.Text = "alert('此修改单已关闭！');"
                    Exit Sub
                End If
                txt_desc.Text = e.Item.Cells(4).Text.Trim()
                txt_bmid.Text = e.Item.Cells(0).Text.Trim()
                txt_comment.Text = e.Item.Cells(10).Text.Trim()

                btn_Add.Text = "保存"
            ElseIf e.CommandName.CompareTo("bm_close") = 0 Then
                If e.Item.Cells(1).Text.Trim() = "已关闭" Then
                    ltlAlert.Text = "alert('此修改单已关闭！');"
                    Exit Sub
                End If

                If Not Security("87200003").isValid Then
                    ltlAlert.Text = "alert('没有关闭修改单的权限！');"
                    Exit Sub
                End If

                StrSql = "Update bm_mstr set bm_closedBy = '" & Session("uID") & "', bm_closedName = N'" & Session("uName") & "',bm_closedDate = getdate() where Isnull(bm_closedDate,'') = '' and bm_mstr_id='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, StrSql)
                BindData()
            ElseIf e.CommandName.CompareTo("bm_del") = 0 Then
                If e.Item.Cells(1).Text.Trim() = "已关闭" Then
                    ltlAlert.Text = "alert('此修改单已关闭！');"
                    Exit Sub
                End If

                If Not Security("87200002").isValid Then
                    ltlAlert.Text = "alert('没有删除修改单的权限！');"
                    Exit Sub
                End If

                StrSql = " Select Count(*) From bm_detail Where bm_mstr_id = '" & e.Item.Cells(0).Text.Trim() & "' and " _
                       & " ( isnull(sh_plan_qty_init,0) <> 0 or isnull(sh_stock_qty_init,0) <> 0 or isnull(sh_other_qty_init,0) <> 0 " _
                       & " or isnull(zj_plan_qty_init,0) <> 0 or isnull(zj_stock_qty_init,0) <> 0 or isnull(zj_other_qty_init,0) <> 0 " _
                       & " or isnull(yz_plan_qty_init,0) <> 0 or isnull(yz_stock_qty_init,0) <> 0 or isnull(yz_other_qty_init,0) <> 0 ) "
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, StrSql) > 0 Then
                    ltlAlert.Text = "alert('已有调查数据存在，不能删除！');"
                    Exit Sub
                End If

                StrSql = "Update bm_mstr set bm_deletedBy = '" & Session("uID") & "', bm_deletedName = N'" & Session("uName") & "', bm_deletedDate = getdate() where Isnull(bm_closedDate,'') = '' and bm_mstr_id='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, StrSql)
                BindData()
            End If
        End Sub

        Private Sub btn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Add.Click
            If Not Security("87200001").isValid Then
                ltlAlert.Text = "alert('没有创建和编辑盘点事务的权限！');"
                Exit Sub
            End If

            If txt_desc.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请输入创建修改单的更改原因！');"
                Exit Sub
            End If

            If txt_comment.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请输入创建修改单的处理意见！');"
                Exit Sub
            End If

            If txt_bmid.Text.Trim = "" Then
                StrSql = " Insert Into bm_mstr(bm_desc, bm_comment,bm_createdBy, bm_createdName, bm_createdDate) " _
                       & " Values(N'" & txt_desc.Text.Trim() & "', N'" & txt_comment.Text.Trim() & "','" & Session("uID") & "',N'" & Session("uName") & "', getdate())"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, StrSql)
            Else
                StrSql = "Update bm_mstr set bm_desc =N'" & txt_desc.Text.Trim() & "', bm_comment = N'" & txt_comment.Text.Trim() & "' where Isnull(bm_closedBy,'') = '' And Isnull(bm_deletedBy,'') = '' And bm_mstr_id='" & txt_bmid.Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, StrSql)
            End If

            txt_desc.Text = ""
            txt_bmid.Text = ""
            txt_comment.Text = ""
            btn_Add.Text = "新建修改单"

            BindData()
        End Sub
    End Class

End Namespace













