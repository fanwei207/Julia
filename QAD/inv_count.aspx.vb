Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class inv_count
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Protected WithEvents btn_next As System.Web.UI.WebControls.Button
        Protected WithEvents btn_close As System.Web.UI.WebControls.Button
        Protected WithEvents bnt_cancel As System.Web.UI.WebControls.Button
        Protected WithEvents countLabel As System.Web.UI.WebControls.Label
        Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
        Dim nRet As Integer


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub



        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("85500104", "删除盘点数")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            If Not IsPostBack Then

                If Request("isC") = "已关闭" Then
                    Button1.Enabled = False
                    Button2.Enabled = False
                End If

                Dim strSQL As String
                Dim ds As DataSet

                strSQL = " SELECT startDate,description,isClosed from Inv_Count_Mstr where id='" & Request("invid") & "'"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            Label1.Text = Format(.Rows(i).Item(0), "yyyy-MM-dd")
                            If .Rows(i).Item(2) = True Then
                                Label2.Text = "已关闭"
                            Else
                                Label2.Text = "处理中"
                            End If
                            Label3.Text = .Rows(i).Item(1).ToString()
                        Next
                    End If
                End With
                ds.Reset()

                Button1.Attributes.Add("onclick", "return confirm('只能删除你录入的盘点数据,确定要删除所有符合条件的记录吗?');")


                BindData()
            End If
        End Sub

        Sub BindData()
            StrSql = "select c.inv_id,c.id,c.site,c.loca,c.item,c.item_desc,c.tag_no,isnull(c.inv_qty,0),c.inv_status,c.createdBy,c.createdDate,u.username "
            StrSql &= " from Inv_Count_Detail c inner join tcpc0.dbo.users u on c.createdBy=u.userid where c.inv_id ='" & Request("invid") & "' and c.deletedBy is null "
            If txtSite.Text.Trim.Length > 0 Then
                StrSql &= " and c.site ='" & txtSite.Text.Trim() & "'"
            End If

            If txtLoca.Text.Trim.Length > 0 Then
                StrSql &= " and c.loca ='" & txtLoca.Text.Trim() & "'"
            End If
            If txtItem.Text.Trim.Length > 0 Then
                StrSql &= " and c.item like '" & txtItem.Text.Trim() & "%'"
            End If
            StrSql &= " order by c.tag_no, c.id"

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim total As Integer

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("inv_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("qad_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("lcoa", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("item", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("tag", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("qty", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("des", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("inv_status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("oper", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("inv_id") = .Rows(i).Item(0)
                        drow.Item("qad_id") = .Rows(i).Item(1)
                        drow.Item("site") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("lcoa") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("item") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("tag") = .Rows(i).Item(6).ToString().Trim()
                        drow.Item("qty") = .Rows(i).Item(7)
                        drow.Item("inv_status") = .Rows(i).Item(8).ToString().Trim()
                        drow.Item("des") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("oper") = .Rows(i).Item(11).ToString().Trim()
                        drow.Item("user_id") = .Rows(i).Item(9).ToString().Trim()
                        dtl.Rows.Add(drow)
                        total = total + 1
                    Next
                End If
            End With
            ds.Reset()

            Label4.Text = "标签数:" & total.ToString()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()


        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("inv_del") = 0 Then
                If Label2.Text.Trim = "已关闭" Then
                    ltlAlert.Text = "alert('此盘点事务已关闭！')"
                    Exit Sub
                End If

                If Not Me.Security("85500104").isValid Then
                    ltlAlert.Text = "alert('没有删除盘点数的权限！')"
                    Exit Sub
                End If

                If (e.Item.Cells(11).Text.Trim() = Session("uID") Or Session("uRole") = 1) Then
                    StrSql = "update Inv_Count_Detail set deletedBy =N'" & Session("uName") & "' where deletedBy is null and id='" & e.Item.Cells(1).Text.Trim() & "' and inv_id='" & e.Item.Cells(0).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                    Datagrid1.CurrentPageIndex = 0

                    BindData()
                Else
                    ltlAlert.Text = "alert('只能删除你录入的盘点数据！')"
                    Exit Sub
                End If
            End If
        End Sub
        Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
            Datagrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            'delete
            If Label2.Text.Trim = "已关闭" Then
                ltlAlert.Text = "alert('此盘点事务已关闭！')"
                Exit Sub
            End If

            If Not Me.Security("85500104").isValid Then
                ltlAlert.Text = "alert('没有删除盘点数的权限！')"
                Exit Sub
            End If
            If Session("uRole") = 1 Then
                StrSql = "update Inv_Count_Detail set deletedBy =N'" & Session("uName") & "' where deletedBy is null and  inv_id='" & Request("invid") & "' "
                If txtSite.Text.Trim.Length > 0 Then
                    StrSql &= " and site ='" & txtSite.Text.Trim() & "'"
                End If

                If txtLoca.Text.Trim.Length > 0 Then
                    StrSql &= " and loca ='" & txtLoca.Text.Trim() & "'"
                End If
                If txtItem.Text.Trim.Length > 0 Then
                    StrSql &= " and item like '" & txtItem.Text.Trim() & "%'"
                End If

                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            Else
                StrSql = "update Inv_Count_Detail set deletedBy =N'" & Session("uName") & "' where deletedBy is null and  inv_id='" & Request("invid") & "' and createdBy='" & Session("uID") & "' "
                If txtSite.Text.Trim.Length > 0 Then
                    StrSql &= " and site ='" & txtSite.Text.Trim() & "'"
                End If

                If txtLoca.Text.Trim.Length > 0 Then
                    StrSql &= " and loca ='" & txtLoca.Text.Trim() & "'"
                End If
                If txtItem.Text.Trim.Length > 0 Then
                    StrSql &= " and item like '" & txtItem.Text.Trim() & "%'"
                End If

                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            End If
            Datagrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            'import
            If Label2.Text.Trim = "已关闭" Then
                ltlAlert.Text = "alert('此盘点事务已关闭！')"
                Exit Sub
            End If
            Response.Redirect("inv_count_import.aspx?invid=" & Request("invid") & "&isC=" & Request("isC"))
        End Sub

        Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
            Response.Redirect("inv_count_mstr.aspx?invid=" & Request("invid"))
        End Sub

        Private Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













