Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Math


Namespace tcpc



    Partial Class so_anal
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
                strSql = " Delete from qadplan.dbo.tcp_anal where tcpa_userid='" & Session("uID") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                BindData()
            End If
        End Sub
        
        Sub BindData()

            If Not IsNumeric(txb_mon.Text) Or CInt(txb_mon.Text) <= 0 Or CInt(txb_mon.Text) > 24 Then
                txb_mon.Text = "12"
            End If

            Dim jj As String = "isnull(ps.j25,0)"
            Dim qq As String = "isnull(ps.q25,0)"

            Dim i As Integer

            For i = 24 To 24 - CInt(txb_mon.Text) + 1 Step -1
                jj &= " + isnull(ps.j" & i.ToString() & ",0)"
                qq &= " + isnull(ps.q" & i.ToString() & ",0)"
            Next
            jj = "(" + jj + ")"
            qq = "(" + qq + ")"

            '月份数
            Dim mm As Decimal = CInt(txb_mon.Text) + DateTime.Today.Day() / 30.42


            strSql = "select ps.tcp_code,ps.tcp_desc,isnull(ps.jqty_oh,0)," & jj & ",isnull(ps.j25,0)," & jj & "/" & mm & "," & jj & "/" & mm & "/4"
            strSql &= " ,isnull(ps.qqty_oh,0) qh," & qq & " as aw,isnull(ps.q25,0)," & qq & "/" & mm & "," & qq & "/" & mm & "/4 ,ps.createdDate,ps.tcp_id,isnull(pa.tcpa_userid,0),isnull(pa.tcpa_qty_fc,0),isnull(ps.q24,0),isnull(ps.q23,0),isnull(ps.q22,0) from qadplan.dbo.tcp_so ps Left outer join qadplan.dbo.tcp_anal pa on pa.tcpa_userid='" & Session("uID") & "'and pa.tcpa_code=ps.tcp_code where ps.tcp_code is not null "
            If txb_item.Text.Trim.Length > 0 Then
                strSql &= " and ps.tcp_code like  '" & txb_item.Text.Replace("*", "%") & "'"
            End If
            If txb_desc.Text.Trim.Length > 0 Then
                strSql &= " and ps.tcp_desc like  N'%" & txb_desc.Text & "%'"
            End If
            strSql &= " order by aw desc,qh desc"
            'Response.Write(mm.ToString())
            'Response.Write(strSql)
      
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dtl As New DataTable
            Dim total As Integer = 0
            dtl.Columns.Add(New DataColumn("g_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("g_item", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_desc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("j_inv", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("j_tot", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("j_mon", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("j_avgm", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("j_avgw", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("q_inv", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("q_tot", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("q_mon", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("q_avgm", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("q_avgw", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("g_date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_sele", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_fc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("g_sort", System.Type.GetType("System.Int32")))

            With ds.Tables(0)
                Dim drow As DataRow
                If .Rows.Count > 0 Then

                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("g_sort") = i + 1
                        drow.Item("g_id") = .Rows(i).Item(13)
                        drow.Item("g_item") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("g_desc") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("j_inv") = .Rows(i).Item(2)
                        drow.Item("j_tot") = .Rows(i).Item(3)
                        drow.Item("j_mon") = .Rows(i).Item(4)
                        drow.Item("j_avgm") = .Rows(i).Item(5)
                        drow.Item("j_avgw") = .Rows(i).Item(6)
                        drow.Item("q_inv") = .Rows(i).Item(7)
                        drow.Item("q_tot") = .Rows(i).Item(8)
                        drow.Item("q_mon") = .Rows(i).Item(9)
                        drow.Item("q_avgm") = .Rows(i).Item(10)
                        drow.Item("q_avgw") = .Rows(i).Item(11)

                        If IsDBNull(.Rows(i).Item(12)) = False Then
                            drow.Item("g_date") = Format(.Rows(i).Item(12), "yyyy-MM-dd")
                        Else
                            drow.Item("g_date") = ""
                        End If
                        If .Rows(i).Item(14) <= 0 Then
                            drow.Item("g_sele") = "N"
                        Else
                            drow.Item("g_sele") = "Y"
                        End If

                        If .Rows(i).Item(15) > 0 Then
                            drow.Item("g_fc") = Format(.Rows(i).Item(15), "##0")
                        Else
                            If .Rows(i).Item(16) + .Rows(i).Item(17) + .Rows(i).Item(18) + .Rows(i).Item(9) > 0 Then
                                'drow.Item("g_fc") = Format(Min(.Rows(i).Item(6), .Rows(i).Item(11)) * 0.8, "##0")
                                drow.Item("g_fc") = Format(.Rows(i).Item(11) * 0.8, "##0")
                            End If
                        End If

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

        Private Sub DgDoc_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgDoc.ItemCommand
            If (e.CommandName.CompareTo("SelectBtn") = 0) Then
                Dim strSQL As String
                Dim str As String = CType(e.Item.Cells(14).Controls(0), LinkButton).Text
                If str = "Y" Then
                    strSQL = " Delete from qadplan.dbo.tcp_anal where tcpa_code = '" & e.Item.Cells(1).Text() & "' and tcpa_userid='" & Session("uID") & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                Else
                    Dim str2 As String = CType(e.Item.Cells(13).FindControl("txb_fc"), TextBox).Text
                    If str2.Trim.Length > 0 Then
                        If IsNumeric(str2) Then
                            If CDec(str2) > 0 Then
                                strSQL = " Insert Into qadplan.dbo.tcp_anal(tcpa_code,tcpa_userid,tcpa_qty_fc,tcpa_desc) values('" & e.Item.Cells(1).Text() & "','" & Session("uID") & "'," & CDec(str2) & ",N'" & e.Item.Cells(15).Text() & "')"
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                            End If
                        End If
                    End If
                    End If

                    BindData()
            End If
        End Sub
        Private Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
            DgDoc.EditItemIndex = -1
            BindData()
        End Sub

        Protected Sub btn_detail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_detail.Click
            ltlAlert.Text = "var w=window.open('so_exportDetail.aspx?uid=" & Session("uID") & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub

        Protected Sub btn_anal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_anal.Click
            ltlAlert.Text = "var w=window.open('so_exportAnal.aspx?uid=" & Session("uID") & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"
        End Sub
    End Class

End Namespace
