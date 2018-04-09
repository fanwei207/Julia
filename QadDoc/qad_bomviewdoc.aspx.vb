Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc

Namespace tcpc
    Partial Class qad_bomviewdoc
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
                If Request("url") = Nothing Then
                    Button1.Enabled = False
                Else
                    Button1.Enabled = True
                End If

                If Request("part") <> Nothing Then
                    txb_bom_code.Text = Request("part")
                End If
                txb_bom_date.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now)
                CreateBom(txb_bom_code.Text)

                BindData()
            End If
        End Sub

        Private Sub CreateBom(ByVal part2 As String)
            'Exit Sub 
            strSql = "Delete from tcpc0.dbo.doc_bom_temp where doc_user_id='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            If part2 <> Nothing Then
                strSql = "Insert Into tcpc0.dbo.doc_bom_temp(doc_bom_par,doc_bom_part,doc_bom_comp,doc_user_id,doc_bom_qty,doc_bom_lel,doc_status) "
                strSql &= " Values('" & part2 & "','" & part2 & "','" & part2 & "','" & Session("uID") & "',1,0,'')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                GetBomFromQad(part2, 0)

            End If
        End Sub

        Private Sub GetBomFromQad(ByVal part As String, ByVal lel As Integer)
            If lel > 9 Then
                Exit Sub
            End If

            Dim reader As SqlDataReader
            Dim bom_par As String
            Dim bom_part As String
            Dim bom_comp As String
            Dim qty As Decimal
            Dim id As Int64 = 0

            Dim bom_date As DateTime
            If txb_bom_date.Text.Trim.Length <> 10 Or Not IsDate(txb_bom_date.Text.Trim) Then
                txb_bom_date.Text = Format(Today, "yyyy-MM-dd")
            End If
            bom_date = txb_bom_date.Text.Trim
            strSql = "WITH BOM" _
                   & " AS" _
                   & " (SELECT ps_par as product,ps_par,ps_comp,ps_qty_per,1 AS lel,CAST(ps_comp AS VARCHAR(max)) AS sort FROM QAD_Data.dbo.ps_mstr " _
                   & "WHERE ps_domain IN ('" & ddl_site.SelectedValue & "','ZQL') " _
                   & " AND Isnull(ps_start, '1900-1-1') <= '" & bom_date & "'" _
                   & " And Isnull(DATEADD(day,1,ps_end), '2900-1-1') > '" & bom_date & "'  " _
                   & " AND ps_par='" & part & "'" _
                   & " and Isnull(ps_ps_code, '') = ''" _
                   & "    UNION ALL " _
                   & " SELECT b.product,a.ps_par,a.ps_comp,a.ps_qty_per,lel+1,b.sort+'-'+a.ps_comp" _
                   & " FROM QAD_Data.dbo.ps_mstr a" _
                   & " INNER JOIN BOM b" _
                   & " ON b.ps_comp=a.ps_par " _
                   & " AND Isnull(a.ps_start, '1900-1-1') <= '" & bom_date & "' " _
                   & " And Isnull(DATEADD(day,1,a.ps_end), '2900-1-1') > '" & bom_date & "'  " _
                   & " and Isnull(a.ps_ps_code, '') = ''" _
                   & " WHERE a.ps_domain IN ('" & ddl_site.SelectedValue & "','ZQL'))" _
                   & " Insert Into tcpc0.dbo.doc_bom_temp(doc_bom_par,doc_bom_part,doc_bom_comp,doc_bom_qty,doc_user_id,doc_bom_lel,doc_status)" _
                   & " select product,ps_par,ps_comp,ps_qty_per," & Session("uID") & ",lel,'' from" _
                   & " (select distinct * from BOM) b order by sort"
            Try
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            Catch
                Response.Write(strSql)
                Exit Sub
            End Try
            'strSql = "Select doc_bom_id,doc_bom_par,doc_bom_part,doc_bom_comp,isnull(doc_bom_qty,0)"
            'strSql &= " From tcpc0.dbo.doc_bom_temp "
            'strSql &= " Where doc_user_id='" & Session("uID") & "' and doc_bom_lel='" & lel & "' and doc_status ='PROD' and doc_bom_comp='" & part & "'"
            'reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
            'While reader.Read()
            '    id = reader(0).ToString()
            '    bom_par = reader(1).ToString()
            '    bom_part = reader(2).ToString()
            '    bom_comp = reader(3).ToString()
            '    qty = reader(4)

            '    Dim str2 As String

            '    Dim sql As String = "Select ps_comp,ps_qty_per "
            '    sql = sql & " from Qad_Data.dbo.ps_mstr "
            '    sql = sql & " where ps_domain='" & ddl_site.SelectedValue & "' and ps_par='" & bom_comp & "'"
            '    sql = sql & "   and (ps_start<='" & bom_date & "' or ps_start is null) and (ps_end>='" & bom_date & "' or ps_end is null)"
            '    sql = sql & "   and Isnull(ps_ps_code, '') = '' "
            '    'Add By Shan Zhiming 2013-12-25：类似线路板的结构，需要到ZQL域中取
            '    sql = sql & " Union "
            '    sql = sql & " Select ps_comp, ps_qty_per "
            '    sql = sql & " from Qad_Data.dbo.ps_mstr "
            '    sql = sql & " where ps_domain='ZQL' and ps_par = '" & bom_comp & "'"
            '    sql = sql & "   and (ps_start <= '" & bom_date & "' or ps_start is null) and (ps_end>='" & bom_date & "' or ps_end is null) "
            '    sql = sql & "   and Isnull(ps_ps_code, '') = '' "

            '    Try
            '        Dim dr As SqlDataReader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sql)
            '        While (dr.Read())
            '            str2 = "Insert Into tcpc0.dbo.doc_bom_temp(doc_bom_par,doc_bom_part,doc_bom_comp,doc_bom_qty,doc_user_id,doc_bom_lel,doc_status) "
            '            str2 &= " Values('" & bom_par & "','" & bom_comp & "','" & dr.GetValue(0).ToString() & "','" & dr.GetValue(1).ToString() & "','" & Session("uID") & "','" & lel + 1.ToString() & "','PROD')"
            '            Try
            '                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str2)
            '            Catch
            '                Response.Write(str2)
            '                Exit Sub
            '            End Try

            '            GetBomFromQad(dr.GetValue(0).ToString(), lel + 1)

            '        End While
            '        dr.Close()

            '    Catch oe As OdbcException
            '        Response.Write(oe.Message)
            '    Finally

            '    End Try

            '    sql = "Update tcpc0.dbo.doc_bom_temp set doc_status ='' where doc_bom_id='" & id & "'"
            '    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, sql)

            'End While
            'reader.Close()

        End Sub

        Sub BindData()
            Dim ds As DataSet
            strSql = " SELECT t.doc_bom_id,t.doc_bom_par,t.doc_bom_part,t.doc_bom_comp,i.code,t.doc_bom_lel,i.description,isnull(q.desc1,'')+isnull(q.desc2,'') " _
                   & " , t.doc_bom_qty, isDoc = Case When pti.itm_qad Is Null Then 0 Else 1 End , nullif(q.pt_pur_lead,0) as pt_pur_lead" _
                   & " From tcpc0.dbo.doc_bom_temp t " _
                   & " Left Outer join tcpc0.dbo.items i on i.item_qad=t.doc_bom_comp " _
                   & " Left Outer join (select distinct pt_part as qad,max(pt_desc1) as desc1,max(pt_desc2) as desc2,max(pt_pur_lead) as pt_pur_lead from qad_data.dbo.pt_mstr Group by pt_part) q on q.qad=t.doc_bom_comp  COLLATE DATABASE_DEFAULT " _
                   & " Left Outer Join ( " _
                   & "                      Select Distinct itm_qad " _
                   & "                      From tcpc0.dbo.ProductTrackingItem " _
                   & "                  ) pti On pti.itm_qad = t.doc_bom_comp "
            strSql = strSql & " Where t.doc_user_id='" & Session("uID") & "' and t.doc_bom_par='" & txb_bom_code.Text.Trim & "' "
            If txb_lel.Text.Trim.Length > 0 Then
                If IsNumeric(txb_lel.Text.Trim) Then
                    strSql &= " and doc_bom_lel <='" & CInt(txb_lel.Text.Trim) & "' "
                End If
            End If
            strSql &= " Order by t.doc_bom_id"

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
            dt.Columns.Add(New DataColumn("isNewMechanism", System.Type.GetType("System.Boolean")))
            dt.Columns.Add(New DataColumn("filename", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("typeid", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("cateid", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("open", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("typename", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("catename", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("isDoc", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gLead", System.Type.GetType("System.String")))



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
                        dr1.Item("gqty") = Format(.Rows(i).Item(8), "##0.##########")
                        dr1.Item("glink") = ""
                        dr1.Item("fid") = 0 '默认是0

                        dr1.Item("gLead") = .Rows(i).Item(10).ToString().Trim()


                        If CheckBox1.Checked Then
                            dr1.Item("gDesc") = .Rows(i).Item(7).ToString().Trim()
                        Else
                            dr1.Item("gDesc") = .Rows(i).Item(6).ToString().Trim()
                        End If

                        dt.Rows.Add(dr1)

                        dr1.Item("isNewMechanism") = False '默认为假

                        If Convert.ToBoolean(.Rows(i).Item("isDoc")) Then
                            dr1.Item("isDoc") = "需要"
                        Else
                            dr1.Item("isDoc") = ""
                        End If


                        If Checkbox2.Checked Then

                            str3 = "select distinct id = Isnull(t.id, 0), d.name, isNewMechanism = Isnull(isNewMechanism, 0), d.filename, d.cateid, d.typeid,d.catename, d.typename,d.filepath, d.accFileName,d.Path, "
                            str3 &= "Case " & Session("uRole") & " When 1 Then 0 Else Isnull(d.docLevel,3) - isnull(da.doc_acc_level,9) End As docLevel, isApprove = Isnull(isApprove, 0)"
                            str3 &= " from qaddoc.dbo.DocumentItem i "
                            str3 &= " Inner Join qaddoc.dbo.Documents d on d.id=i.docid "
                            str3 &= " Left Join qaddoc.dbo.DocumentDetail t on t.id=d.filepath "
                            str3 &= " Left Join qaddoc.dbo.documentaccess da on d.typeid = da.doc_acc_catid and da.doc_acc_userid = '" & Session("uID") & "' And da.approvedBy Is Not Null "
                            str3 &= " where i.qad='" & dr1.Item("gPart") & "' "
                            reader3 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, str3)
                            While reader3.Read()
                                dr1 = dt.NewRow()
                                dr1.Item("fid") = reader3(0)
                                dr1.Item("glink") = reader3(1).ToString()
                                dr1.Item("isNewMechanism") = reader3("isNewMechanism")
                                dr1.Item("filename") = reader3("filename")
                                dr1.Item("typeid") = reader3("typeid")
                                dr1.Item("cateid") = reader3("cateid")
                                dr1.Item("typename") = reader3("typename")
                                dr1.Item("catename") = reader3("catename")

                                '验证查看文档权限
                                'Dim reader_doclevel As SqlDataReader
                                Dim iValid As Integer = Integer.Parse(reader3("docLevel").ToString())
                                'Dim strSqldocLevel As String
                                Dim isApprove As Boolean = Convert.ToBoolean(reader3("isApprove"))

                                'strSqldocLevel = "Select Case " & Session("uRole") & " When 1 Then 0 Else Isnull(d.docLevel,3) - da.doc_acc_level End As docLevel, isApprove = Isnull(isApprove, 0) " _
                                '       & "From qaddoc.dbo.documents d " & IIf(Session("uRole") = 1, "Left Outer", "Inner") & " Join qaddoc.dbo.documentaccess da on d.typeid = da.doc_acc_catid " _
                                '       & "Where d.filepath = '" & reader3("filepath") & "'"
                                'If Session("uRole") <> 1 Then
                                '    strSqldocLevel &= " And da.doc_acc_userid = '" & Session("uID") & "' And da.approvedBy Is Not Null "
                                'End If
                                'reader_doclevel = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSqldocLevel)
                                'If (reader_doclevel.Read()) Then
                                '    iValid = Convert.ToInt32(reader_doclevel("docLevel"))
                                '    isApprove = Convert.ToBoolean(reader_doclevel.Item("isApprove"))
                                'Else
                                '    iValid = -1
                                'End If
                                If iValid >= 0 Then
                                    '如果文档未审核，则不允许打开，且同样适合关联文档
                                    'If Convert.ToBoolean(reader3("isNewMechanism")) Then
                                    '    dr1.Item("open") = "<a href='/TecDocs/" & reader3("typeid") & "/" & reader3("cateid") & "/" & reader3("filename") & "' target='_blank'><u>Open</u></a>"
                                    'Else
                                    '    dr1.Item("open") = "<a href='/qaddoc/qad_viewdocument.aspx?filepath=" & reader3("filepath") & "&code=" & "document" & "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0' target='_blank'><u>Open</u></a>"
                                    'End If
                                    Dim path As String = reader3("Path").ToString()
                                    If String.IsNullOrEmpty(path) Then
                                        dr1.Item("open") = "<a href='/TecDocs/" & reader3("typeid") & "/" & reader3("cateid") & "/" & reader3("filename") & "' target='_blank'><u>Open</u></a>"
                                    Else
                                        dr1.Item("open") = "<a href='" & path & reader3("filename") & "' target='_blank'><u>Open</u></a>"
                                    End If

                                    If Not isApprove Then
                                        dr1.Item("open") = "未审批"
                                    End If

                                    If reader3("accFileName").ToString().Trim().Length > 0 Then
                                        Dim _accFileName As String = reader3("accFileName").ToString().Trim()

                                        If File.Exists(Server.MapPath(path & _accFileName)) Then
                                            dr1.Item("filename") = dr1.Item("filename") & "&nbsp;(<a href='" & path & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                                        ElseIf File.Exists(Server.MapPath("/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/" & _accFileName)) Then
                                            dr1.Item("filename") = dr1.Item("filename") & "&nbsp;(<a href='/TecDocs/" & Request("typeid") & "/" & Request("cateid") & "/" & _accFileName & "' target='_blank' title='" + _accFileName + "'><u>下载关联文件</u></a>)"
                                        End If
                                    End If
                                Else
                                    dr1.Item("open") = "&nbsp;"
                                End If
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
                str3 = "select id = Isnull(t.id, 0), d.name, isNewMechanism = Isnull(isNewMechanism, 0), d.filename from qaddoc.dbo.DocumentItem i Inner Join qaddoc.dbo.Documents d on d.id=i.docid "
                str3 &= " Inner Join qaddoc.dbo.DocumentDetail t on t.id=d.filepath where d.isall=1 "
                reader3 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, str3)
                While reader3.Read()
                    dr1 = dt.NewRow()
                    dr1.Item("gdesc") = "通用文档"
                    dr1.Item("fid") = reader3(0)
                    dr1.Item("glink") = reader3(1).ToString()
                    dr1.Item("isNewMechanism") = reader3("isNewMechanism")
                    dr1.Item("filename") = reader3("filename")
                    dr1.Item("open") = "Open"
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
            Dim sql As String
            Dim qad As String
            sql = "select top 1 isnull(item_qad,'') from items where code = N'" & txb_item_code.Text.Trim & "'"
            If txb_item_code.Text.Trim <> "" And txb_bom_code.Text.Trim = "" Then
                qad = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, sql)
                If (qad = "") Then
                    Me.Alert("No QAD associated with this item")
                    BindData()
                    Return
                Else
                    txb_bom_code.Text = qad
                End If
            End If
            CreateBom(txb_bom_code.Text)
            BindData()
        End Sub

        Private Sub ddl_site_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_site.SelectedIndexChanged
            Dim sql As String
            Dim qad As String
            sql = "select top 1 isnull(item_qad,'') from items where code = N'" & txb_item_code.Text.Trim & "'"
            If Request("part") <> Nothing Then
                txb_bom_code.Text = Request("part")
            Else
                If txb_item_code.Text.Trim <> "" And txb_bom_code.Text.Trim = "" Then
                    qad = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, sql)
                    If (qad = "") Then
                        Me.Alert("No QAD associated with this item")
                        BindData()
                        Return
                    Else
                        txb_bom_code.Text = qad
                    End If
                End If
            End If

            CreateBom(txb_bom_code.Text)
            BindData()
        End Sub

        'Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        '    If (e.CommandName.CompareTo("docview") = 0) Then
        '        If (Convert.ToBoolean(e.Item.Cells(10).Text.Trim)) Then
        '            ltlAlert.Text = "window.open('/TecDocs/" & e.Item.Cells(12).Text & "/" & e.Item.Cells(13).Text & "/" & e.Item.Cells(11).Text & "', '', 'menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0');"
        '        Else
        '            ltlAlert.Text = "window.open('/qaddoc/qad_viewdocument.aspx?code=1&filepath=" & e.Item.Cells(9).Text.Trim & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=600,top=0,left=0');"
        '        End If
        '    End If
        'End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

            If Request("cmd") = "track" Then
                Me.Redirect("/producttrack/producttracking.aspx")
            ElseIf Request("cmd") = "newtrack" Then
                Me.Redirect("/producttrack/producttrackingNew.aspx")
            ElseIf Request("cmd") = "newtrackOEM" Then
                Me.Redirect("/producttrack/producttrackingOEMNew.aspx")
            ElseIf Request("cmd") = "so" Then
                Me.Redirect("/producttrack/producttrackingso.aspx")
            ElseIf Request("cmd") = "wo" Then
                Me.Redirect("/producttrack/producttrackingwo.aspx")
            ElseIf Request("cmd") = "edi" Then
                Me.Redirect("/producttrack/producttrackingedi.aspx")
            ElseIf Request("cmd") = "omsproduct" Then
                Me.Redirect("/OMS/oms_mstr.aspx?index=1&custCode=" & Request("custCode") & "&custName=" & Request("custName"))
            ElseIf Request("q") <> Nothing Then
                Me.Redirect("/producttrack/producttracking.aspx?dom=" & Request("dom") & "&type=" & Request("type") & "&q=" & Request("q"))
            End If
        End Sub

        Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            BindData()
        End Sub

        Private Sub CheckBox2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Checkbox2.CheckedChanged
            BindData()
        End Sub

        Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                'If (CType(e.Item.Cells(8).Controls(0), LinkButton).Text = "未审批") Then
                '    e.Item.Cells(8).Enabled = False
                'End If

                If (e.Item.Cells(8).Text = "未审批") Then
                    e.Item.Cells(8).Enabled = False
                End If

            End If
        End Sub
    End Class

End Namespace
