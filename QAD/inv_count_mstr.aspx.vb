Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class inv_count_mstr
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then

                txb_id.Text = ""

                Button1.Text = "新建盘点"
                BindData()
            End If
        End Sub

        Sub BindData()
            StrSql = "select id,startdate,enddate, description,isClosed from Inv_Count_Mstr order by startdate desc"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim total As Integer

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("inv_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("inv_status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("sdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("edate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("des", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("inv_id") = .Rows(i).Item(0).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(1)) = False Then
                            drow.Item("sdate") = Format(.Rows(i).Item(1), "yy-MM-dd")
                        Else
                            drow.Item("sdate") = ""
                        End If
                        If IsDBNull(.Rows(i).Item(2)) = False Then
                            drow.Item("edate") = Format(.Rows(i).Item(2), "yy-MM-dd")
                        Else
                            drow.Item("edate") = ""
                        End If
                        drow.Item("des") = .Rows(i).Item(3).ToString().Trim()

                        If .Rows(i).Item(4) = True Then
                            drow.Item("inv_status") = "已关闭"
                        Else
                            drow.Item("inv_status") = "新创建"
                        End If

                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()


        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("inv_qad") = 0 Then
                Response.Redirect("inv_qad.aspx?invid=" & e.Item.Cells(0).Text.Trim() & "&isC=" & e.Item.Cells(1).Text.Trim())
            ElseIf e.CommandName.CompareTo("inv_import") = 0 Then
                Response.Redirect("inv_count_importBasis.aspx?invid=" & e.Item.Cells(0).Text.Trim())
            ElseIf e.CommandName.CompareTo("inv_count") = 0 Then
                Response.Redirect("inv_count.aspx?invid=" & e.Item.Cells(0).Text.Trim() & "&isC=" & e.Item.Cells(1).Text.Trim())
            ElseIf e.CommandName.CompareTo("inv_diff") = 0 Then
                ltlAlert.Text = "var w=window.open('/QAD/inv_exportExcel.aspx?invid=" & e.Item.Cells(0).Text.Trim() & "','invdiff','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
            ElseIf e.CommandName.CompareTo("inv_edit") = 0 Then
                If e.Item.Cells(1).Text.Trim() = "已关闭" Then
                    ltlAlert.Text = "alert('此盘点事务已关闭！')"
                    Exit Sub
                End If

                txb_desc.Text = e.Item.Cells(4).Text.Trim()
                txb_id.Text = e.Item.Cells(0).Text.Trim()
                Button1.Text = "保存"

            ElseIf e.CommandName.CompareTo("inv_close") = 0 Then
                If e.Item.Cells(1).Text.Trim() = "已关闭" Then
                    ltlAlert.Text = "alert('此盘点事务已关闭！')"
                    Exit Sub
                End If

                If Not Me.Security("85500102").isValid Then
                    ltlAlert.Text = "alert('没有关闭盘点的权限！')"
                    Exit Sub
                End If

                StrSql = "Update Inv_Count_Mstr set isClosed=1,endDate=getdate() where isClosed <>1 and id='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                BindData()
            ElseIf e.CommandName.CompareTo("inv_del") = 0 Then
                If e.Item.Cells(1).Text.Trim() = "已关闭" Then
                    ltlAlert.Text = "alert('此盘点事务已关闭！')"
                    Exit Sub
                End If

                If Not Me.Security("85500102").isValid Then
                    ltlAlert.Text = "alert('没有删除盘点的权限！')"
                    Exit Sub
                End If

                StrSql = "Select top 1 isnull(id,0) from Inv_Count_Detail where inv_id='" & e.Item.Cells(0).Text.Trim() & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                    ltlAlert.Text = "alert('已有盘点数据存在，不能删除！')"
                    Exit Sub
                End If
                StrSql = "Select top 1 isnull(id,0) from Inv_QAD where inv_id='" & e.Item.Cells(0).Text.Trim() & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                    ltlAlert.Text = "alert('已有库存数据存在，不能删除！')"
                    Exit Sub
                End If

                StrSql = "delete from Inv_Count_Mstr where isClosed <>1 and id='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                BindData()
            End If
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

            If Not Me.Security("85500102").isValid Then
                ltlAlert.Text = "alert('没有创建和编辑盘点事务的权限！')"
                Exit Sub
            End If

            If txb_desc.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请输入创建盘点事务的描述！')"
                Exit Sub
            End If

            If txb_id.Text.Trim = "" Then
                StrSql = "insert into Inv_Count_Mstr(startDate,description,isClosed,createdBy,createdName) Values(getdate(),N'" & txb_desc.Text.Trim() & "',0,'" & Session("uID") & "',N'" & Session("uName") & "')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            Else
                StrSql = "Update Inv_Count_Mstr set description =N'" & txb_desc.Text.Trim() & "' where isClosed <>1 and id='" & txb_id.Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            End If

            txb_desc.Text = ""
            txb_id.Text = ""
            Button1.Text = "新建盘点"

            BindData()
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace













