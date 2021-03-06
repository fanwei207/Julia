Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Math


Namespace tcpc



    Partial Class tcp_sforecast
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strSql As String
        Dim ds As DataSet
        Dim reader As SqlDataReader

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents company As System.Web.UI.WebControls.Label
        Protected WithEvents country As System.Web.UI.WebControls.Label
        Protected WithEvents area As System.Web.UI.WebControls.Label
        Protected WithEvents packDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents onBoardDate As System.Web.UI.WebControls.TextBox
        Protected WithEvents containerType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents ordersOutput As System.Web.UI.WebControls.Button


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
                BindData()
            End If
        End Sub

        Sub BindData()

            strSql = "select tcp_sfc_id,tcp_sfc_jitem,isnull(tcp_sfc_jdesc,''),isnull(tcp_sfc_qty,0) "
            strSql &= " ,isnull(tcp_sfc_qitem,''),isnull(tcp_sfc_qdesc,''),createdDate"
            strSql &= " from qadplan.dbo.tcp_sfc "
            strSql &= " where tcp_sfc_jitem is not null and isnull(tcp_sfc_qty,0)>0 and (isnull(tcp_sfc_qchild,'')<>'' or isnull(tcp_sfc_qchilddesc,'')<>'' or isnull(tcp_sfc_qchildqty,0)<>0) "
            If txb_item.Text.Trim.Length > 0 Then
                strSql &= " and tcp_sfc_jitem like  '" & txb_item.Text.Replace("*", "%") & "'"
            End If
            If txb_desc.Text.Trim.Length > 0 Then
                strSql &= " and tcp_sfc_jdesc like  N'%" & txb_desc.Text & "%'"
            End If

            If CheckBox1.Checked = True Then
                strSql &= " and isnull(tcp_sfc_qitem,'')<>''"
            Else
                strSql &= " and isnull(tcp_sfc_qitem,'')=''"
            End If

            strSql &= " order by  isnull(tcp_sfc_qty,0) desc"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dtl As New DataTable
            Dim total As Integer = 0
            dtl.Columns.Add(New DataColumn("g_sort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("g_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("j_item", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("j_desc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("j_end", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("j_qty", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("q_item", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("q_desc", System.Type.GetType("System.String")))

            With ds.Tables(0)
                Dim drow As DataRow
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("g_sort") = i + 1
                        drow.Item("g_id") = .Rows(i).Item(0)
                        drow.Item("j_item") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("j_desc") = .Rows(i).Item(2).ToString().Trim()

                        If IsDBNull(.Rows(i).Item(6)) = False Then
                            drow.Item("j_end") = Format(.Rows(i).Item(6), "yyyy-MM-dd")
                        Else
                            drow.Item("j_end") = ""
                        End If

                        drow.Item("j_qty") = Format(.Rows(i).Item(3), "##0")
                        drow.Item("q_item") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("q_desc") = .Rows(i).Item(5).ToString().Trim()

                        total = total + 1
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()
            Label1.Text = "Total: " & total.ToString()

            Dim dvw As DataView
            dvw = New DataView(dtl) 
            Try 
                DgDoc.DataSource = dvw
                DgDoc.DataBind()
            Catch 'ex As Exception
                '    Response.Write(ex.Message)
            End Try

        End Sub
        Private Sub DgDoc_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DgDoc.PageIndexChanged
            DgDoc.CurrentPageIndex = e.NewPageIndex
            DgDoc.SelectedIndex = -1
            BindData()
        End Sub
        Private Sub DgDoc_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DgDoc.SortCommand
            Session("orderby") = e.SortExpression.ToString()
            If Session("orderdir") = " ASC" Then
                Session("orderdir") = " DESC"
            Else
                Session("orderdir") = " ASC"
            End If
            BindData()
        End Sub

        Private Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
            DgDoc.EditItemIndex = -1
            BindData()
        End Sub
        Protected Sub btn_anal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_anal.Click
            Response.Redirect("tcp_sforecast_anal.aspx")
        End Sub

        Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            BindData()
        End Sub
    End Class

End Namespace
