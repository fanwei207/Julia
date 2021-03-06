Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Namespace tcpc
    Partial Class wo2_rtdetail
        Inherits System.Web.UI.Page
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim nRet As Integer
        Dim item As ListItem
        Dim reader As SqlDataReader
        Dim coefficient As Decimal


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
                Dim wflag As String = "0"
                If Request("rt") <> Nothing Then
                    txb_part.Text = Request("rt")
                    If txb_part.Text.Substring(1, 1) = "1" And txb_part.Text.Substring(3, 2) <> "75" And txb_part.Text.ToUpper.IndexOf("B") = 0 Then
                        wflag = "1"
                    End If
                End If

                coefficient = 1.02

                If Request("a") <> Nothing Then
                    txb_a.Text = Format(CDec(Request("a")) * coefficient, "##0.#####")
                End If
                'txb_date1.Text = Format(Today, "yyyy-MM-01")
                'txb_date2.Text = Format(Today, "yyyy-MM-dd")

                BindData()
            End If
        End Sub
        Sub BindData()
            StrSql = " select wo2_ro_id,wo2_mop_proc,wo2_mop_procname,isnull(wo2_ro_run,0)*" & coefficient & " as tt,r.createdby,r.createddate,r.modifiedby,r.modifieddate,ISNULL(w.wo2_B,0)  "
            StrSql &= " from tcpc0.dbo.wo2_routing r "
            StrSql &= " Left Outer Join tcpc0.dbo.wo2_B_routing  w ON w.wo2_B = wo2_mop_proc "

            StrSql &= " where wo2_ro_id is not null "
            If txb_part.Text.Trim.Length > 0 Then
                StrSql &= " and wo2_ro_routing = '" & txb_part.Text.Trim() & "' "
            Else
                StrSql &= " and wo2_ro_routing = '0' "
            End If

            StrSql &= " order by wo2_mop_proc "

            'Response.Write(StrSql)
            'Exit Sub


            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim tl As Decimal = 0

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_proc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_procname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_time", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_create", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_createdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_modi", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_modidate", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("wo_proc") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("wo_procname") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("wo_time") = Format(.Rows(i).Item(3), "##0.#####")
                        drow.Item("wo_create") = .Rows(i).Item(4)
                        drow.Item("wo_modi") = .Rows(i).Item(6)
                        If Not IsDBNull(.Rows(i).Item(5)) Then
                            drow.Item("wo_createdate") = Format(.Rows(i).Item(5), "yy-MM-dd")
                        End If
                        If Not IsDBNull(.Rows(i).Item(7)) Then
                            drow.Item("wo_modidate") = Format(.Rows(i).Item(7), "yy-MM-dd")
                        End If

                        If (.Rows(i).Item(8) = 0) Then
                            tl = tl + .Rows(i).Item(3)
                        End If
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            ds.Reset()

            drow = dtl.NewRow()
            drow.Item("wo_proc") = "合计"
            drow.Item("wo_time") = Format(tl, "##0.#####")
            dtl.Rows.Add(drow)


            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()

        End Sub

        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click

            Response.Redirect("wo2_rtcompare.aspx?rt=" & DateTime.Now.ToString())
        End Sub

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

    End Class
End Namespace