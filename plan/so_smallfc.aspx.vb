Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Math


Namespace tcpc



    Partial Class so_smallfc
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strSql As String
        Dim strSql2 As String
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
                If Request("qty") <> Nothing Then
                    txb_qty.Text = Request("qty")
                Else
                    If txb_qty.Text.Trim = "" Then
                        txb_qty.Text = "200"
                    End If
                End If
                BindData()
            End If
        End Sub

        Sub BindData()
            If Not IsNumeric(txb_qty.Text) Then
                txb_qty.Text = "200"
            End If

            strSql = "Select sod_domain,sod_nbr,sod_part, isnull(sod_qty_ord,0), isnull(sod_qty_inv,0),sod_req_date, sod_due_date,p.pt_desc1 + '  ' + pt_desc2 "
            strSql &= " from qad_data.dbo.sod_det s left outer join qad_data.dbo.pt_mstr p on p.pt_part=s.sod_part and p.pt_domain=s.sod_domain "
            If txb_desc.Text.Trim.Length > 0 Then
                strSql &= " and p.pt_desc1 + ' ' + p.pt_desc2 like  N'%" & txb_desc.Text & "%'"
            End If
            strSql &= " where sod_domain<>'ATL' and sod_qty_ord>0 and sod_part >='10000000000000' and sod_part<'20000000000000' "
            If txb_item.Text.Trim.Length > 0 Then
                strSql &= " and sod_part like  '" & txb_item.Text.Replace("*", "%") & "'"
            End If
            strSql &= " and sod_qty_ord <='" & txb_qty.Text.Trim & "' "
            If DropDownList1.SelectedIndex > 0 Then
                strSql &= " and sod_domain ='" & DropDownList1.SelectedValue & "' "
            End If

            strSql &= " order by sod_qty_ord "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dtl As New DataTable
            Dim total As Integer = 0
            dtl.Columns.Add(New DataColumn("g_sort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("g_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("g_site", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_so", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_item", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_ord", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_ship", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_shipinv", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_inv", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_start", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_end", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_desc", System.Type.GetType("System.String")))

            Dim qty22 As Decimal = 0

            With ds.Tables(0)
                Dim drow As DataRow
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        strSql2 = "Select sum(isnull(in_qty_oh,0)) from qad_data.dbo.in_mstr "
                        strSql2 &= " where in_qty_oh<>0 and in_part='" & .Rows(i).Item(2).ToString().Trim() & "' and in_domain='" & .Rows(i).Item(0).ToString().Trim() & "' "
                        strSql2 &= " group by in_domain,in_part "
                        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql2)
                        While reader.Read()
                            qty22 = reader(0)
                        End While
                        reader.Close()

                        'If .Rows(i).Item(3) - .Rows(i).Item(4) - qty22 > 0 Then

                        drow = dtl.NewRow()
                        drow.Item("g_sort") = i + 1
                        drow.Item("g_site") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("g_so") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("g_item") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("g_ord") = .Rows(i).Item(3)
                        drow.Item("g_ship") = .Rows(i).Item(4)
                        drow.Item("g_inv") = qty22

                        If IsDBNull(.Rows(i).Item(5)) = False Then
                            drow.Item("g_start") = Format(.Rows(i).Item(5), "yyyy-MM-dd")
                        Else
                            drow.Item("g_start") = ""
                        End If
                        If IsDBNull(.Rows(i).Item(6)) = False Then
                            drow.Item("g_end") = Format(.Rows(i).Item(6), "yyyy-MM-dd")
                        Else
                            drow.Item("g_end") = ""
                        End If
                        drow.Item("g_desc") = .Rows(i).Item(7).ToString().Trim()

                        total = total + 1
                        dtl.Rows.Add(drow)
                        'End If
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
            'Response.Redirect("so_smallfc_anal.aspx?qty=" & txb_qty.Text.Trim)
        End Sub

       
        Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            BindData()
        End Sub
    End Class

End Namespace
