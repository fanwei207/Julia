Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc

Namespace tcpc

    Partial Class qad_bomviewdocNew
        Inherits BasePage
        'Protected WithEvents ltlAlert As Literal

        Public chk As New adamClass
        Dim strSql As String

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
                'check  
                If Request("url") = Nothing Then
                    Button1.Enabled = False
                Else
                    Button1.Enabled = True
                End If

                If Request("part") <> Nothing Then
                    txb_bom_code.Text = Request("part")
                End If
                CreateBom(txb_bom_code.Text)
                BindData()
            End If
        End Sub

        Private Sub CreateBom(ByVal part2 As String)
            'Exit Sub 
            strSql = "Delete from doc_bom_temp where doc_user_id='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)

            If part2 <> Nothing Then
                strSql = "Insert Into doc_bom_temp(doc_bom_par,doc_bom_part,doc_bom_comp,doc_user_id,doc_bom_qty,doc_bom_lel,doc_status) "
                strSql &= " Values('" & part2 & "','" & part2 & "','" & part2 & "','" & Session("uID") & "',1,0,'PROD')"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)

                GetBomFromQad(part2, 0)

            End If
        End Sub

        Private Sub GetBomFromQad(ByVal part As String, ByVal lel As Integer)
            If lel > 9 Then
                Exit Sub
            End If
            Dim reader As SqlDataReader
            Dim reader1 As SqlDataReader
            Dim bom_par As String
            Dim bom_part As String
            Dim bom_comp As String
            Dim qty As Decimal
            Dim id As Int64 = 0
            Dim sql As String = ""

            Dim bom_date As DateTime
            If txb_bom_date.Text.Trim.Length <> 10 Or Not IsDate(txb_bom_date.Text.Trim) Then
                txb_bom_date.Text = Format(Today, "yyyy-MM-dd")
            End If
            bom_date = txb_bom_date.Text.Trim

            strSql = "Select doc_bom_id, doc_bom_par, doc_bom_part, doc_bom_comp, isnull(doc_bom_qty,0) "
            strSql &= " From doc_bom_temp "
            strSql &= " Where doc_user_id='" & Session("uID") & "' and doc_bom_lel='" & lel & "' and doc_status ='PROD' and doc_bom_comp='" & part & "'"
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
            While reader.Read()
                id = reader(0).ToString()
                bom_par = reader(1).ToString()
                bom_part = reader(2).ToString()
                bom_comp = reader(3).ToString()
                qty = reader(4)

                If bom_par.Substring(0, 1) = "1" And bom_par.Substring(13, 1) = "0" Then
                    sql = " Select ps_comp, ps_qty_per from QAD_Data.dbo.ps_mstr where ps_domain='szx' "
                Else
                    sql = " Select ps_comp, ps_qty_per from QAD_Data.dbo.ps_mstr where ps_domain='zql' "
                End If
                sql &= " and ps_par='" & bom_comp & "' and (ps_start<='" & bom_date & "' or ps_start is null) and (ps_end>='" & bom_date & "' or ps_end is null) "

                reader1 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sql)

                While (reader1.Read())
                    sql = "Insert Into doc_bom_temp(doc_bom_par,doc_bom_part,doc_bom_comp,doc_bom_qty,doc_user_id,doc_bom_lel,doc_status) "
                    sql &= " Values('" & bom_par & "','" & bom_comp & "','" & reader1(0).ToString().Trim() & "','" & reader1(1).ToString().Trim()
                    sql &= "','" & Session("uID") & "','" & lel + 1.ToString() & "','PROD')"
                    Try
                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, sql)
                    Catch
                        Response.Write(sql)
                        Exit Sub
                    End Try

                    GetBomFromQad(reader1(0).ToString(), lel + 1)

                End While
                reader1.Close()

                sql = "Update doc_bom_temp set doc_status ='' where doc_bom_id='" & id & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, sql)
            End While
            reader.Close()

        End Sub

        Sub BindData()

            Dim ds As DataSet
            strSql = " SELECT t.doc_bom_id,t.doc_bom_par,t.doc_bom_part,t.doc_bom_comp,i.code,t.doc_bom_lel,i.description,isnull(q.desc1,'')+isnull(q.desc2,''),t.doc_bom_qty " _
                   & " From tcpc0.dbo.doc_bom_temp t Left Outer join tcpc0.dbo.items i on i.item_qad=t.doc_bom_comp " _
                   & " Left Outer join qaddoc.dbo.qad_items q on q.qad=t.doc_bom_comp  COLLATE DATABASE_DEFAULT " _
                   & " Where t.doc_user_id='" & Session("uID") & "' and t.doc_bom_par='" & txb_bom_code.Text.Trim & "' "
            If txb_lel.Text.Trim.Length > 0 Then
                If IsNumeric(txb_lel.Text.Trim.Length) Then
                    strSql &= " and doc_bom_lel <='" & CInt(txb_lel.Text.Trim) & "' "
                End If
            End If
            strSql &= " Order by t.doc_bom_id"
            'Response.Write(strSql)
            'Exit Sub


            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim reader3 As SqlDataReader
            Dim str3 As String

            Dim dt As New DataTable
            Dim dr1 As DataRow
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("gPart", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gCode", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("glevel", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gDesc", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("glink", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gqty", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("fid", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1

                        Select Case .Rows(i).Item(5)
                            Case 0
                                dr1.Item("glevel") = .Rows(i).Item(5)
                            Case 1
                                dr1.Item("glevel") = "-" & .Rows(i).Item(5)
                            Case 2
                                dr1.Item("glevel") = "--" & .Rows(i).Item(5)
                            Case 3
                                dr1.Item("glevel") = "---" & .Rows(i).Item(5)
                            Case 4
                                dr1.Item("glevel") = "----" & .Rows(i).Item(5)
                            Case 5
                                dr1.Item("glevel") = "-----" & .Rows(i).Item(5)
                            Case 6
                                dr1.Item("glevel") = "------" & .Rows(i).Item(5)
                            Case 7
                                dr1.Item("glevel") = "-------" & .Rows(i).Item(5)
                            Case 8
                                dr1.Item("glevel") = "--------" & .Rows(i).Item(5)
                        End Select
                        dr1.Item("id") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("gPart") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("gCode") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("gqty") = Format(.Rows(i).Item(8), "##0.#####")
                        dr1.Item("glink") = ""
                        If CheckBox1.Checked Then
                            dr1.Item("gDesc") = .Rows(i).Item(7).ToString().Trim()
                        Else
                            dr1.Item("gDesc") = .Rows(i).Item(6).ToString().Trim()
                        End If
                        dt.Rows.Add(dr1)

                        If Checkbox2.Checked Then

                            str3 = "select t.id,d.name from qaddoc.dbo.DocumentItem i Inner Join qaddoc.dbo.Documents d on d.id=i.docid "
                            str3 &= " Inner Join qaddoc.dbo.DocumentDetail t on t.id=d.filepath where i.qad='" & dr1.Item("gPart") & "' "
                            reader3 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, str3)
                            While reader3.Read()
                                dr1 = dt.NewRow()
                                dr1.Item("fid") = reader3(0)
                                dr1.Item("glink") = reader3(1).ToString()
                                dt.Rows.Add(dr1)
                            End While
                            reader3.Close()
                        End If
                    Next
                End If
            End With
            ds.Reset()
            dr1 = dt.NewRow()
            dt.Rows.Add(dr1)

            If Checkbox2.Checked Then
                str3 = "select t.id,d.name from qaddoc.dbo.DocumentItem i Inner Join qaddoc.dbo.Documents d on d.id=i.docid "
                str3 &= " Inner Join qaddoc.dbo.DocumentDetail t on t.id=d.filepath where d.isall=1 "
                reader3 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, str3)
                While reader3.Read()
                    dr1 = dt.NewRow()
                    dr1.Item("gdesc") = "Í¨ÓÃÎÄµµ"
                    dr1.Item("fid") = reader3(0)
                    dr1.Item("glink") = reader3(1).ToString()
                    dt.Rows.Add(dr1)
                End While
                reader3.Close()
            End If

            Dim dv As DataView
            dv = New DataView(dt)
            DataGrid1.DataSource = dv
            DataGrid1.DataBind()
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            DataGrid1.EditItemIndex = -1
            BindData()
        End Sub

        Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated

        End Sub

        Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand

        End Sub

        Private Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
            CreateBom(txb_bom_code.Text)
            DataGrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("docview") = 0) Then
                If e.Item.Cells(9).Text.Trim.Length > 0 Then
                    ltlAlert.Text = "window.open('/qaddoc/qad_viewdocument.aspx?code=1&filepath=" & e.Item.Cells(9).Text.Trim & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0');"
                Else
                    ltlAlert.Text = ""
                End If
            End If
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            Response.Redirect(Request("url"))
        End Sub

        Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            BindData()
        End Sub

        Private Sub CheckBox2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Checkbox2.CheckedChanged
            BindData()
        End Sub
    End Class

End Namespace
