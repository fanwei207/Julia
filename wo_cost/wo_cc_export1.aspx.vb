Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc


    Partial Class wo_cc_export1
        Inherits System.Web.UI.Page

        Dim sqlStr As String
        Dim reader As SqlDataReader
        Dim row As TableRow
        Dim cell As TableCell
        Public chk As New adamClass
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

            Dim Str As String = ""
            Dim i As Integer
            Dim dm As String = ""
            Dim total_wo As Decimal = 0
            Dim total_lb As Decimal = 0

            PIHeaderRow("工单工费清单    导出日期: " & Format(Now, "yyyy-MM-dd"))

            '//-------------  Get coefficient      ----------
            Dim coefficient As Decimal = 1.02
            '//----------------------------------------

            'Str = "50^<b>地点</b>~^<b>加工单号</b>~^<b>加工单ID</b>~^50^<b>成本中心</b>~^120^<b>零件号</b>~^<b>完工入库数</b>~^<b>工单标准</b>~^<b>标准成本</b>~^<b>QAD成本</b>~^<b>结算日期</b>~^<b>汇报工费</b>~^<b>差异</b>~^"
            Str = "50^<b>地点</b>~^<b>加工单号</b>~^<b>加工单ID</b>~^50^<b>成本中心</b>~^120^<b>零件号</b>~^<b>完工入库数</b>~^<b>工单标准</b>~^<b>标准工时</b>~^<b>结算日期</b>~^<b>汇报工时</b>~^<b>差异</b>~^"
            PIMasteryRow(Str, True)

            sqlStr = " select s.wocd_site,s.wocd_nbr,s.wocd_id,s.wocd_cc,s.wocd_part, s.cost,s.pcost,s.type,isnull(ct.wo_cost,0),s.wo_tecHours "
            sqlStr &= " from (select cd.wocd_site,cd.wocd_cc,cd.wocd_nbr,cd.wocd_id,cd.wocd_part, sum(CASE WHEN ISNULL(cd.wocd_type,'')='' THEN Round(isnull(cd.wocd_cost,0)*" & coefficient & ",6) ELSE isnull(cd.wocd_cost,0) END) as cost,isnull(cd.wocd_pcost,0) as pcost,isnull(cd.wocd_type,'') as type,w.wo_tecHours "
            sqlStr &= " from wo_cost_detail cd "
            If Session("uRole") <> 1 Then
                sqlStr &= " Inner Join tcpc0.dbo.wo_cc_permission cp on cp.perm_userid='" & Session("uID") & "' and cp.perm_ccid=cd.wocd_cc "
            End If
            sqlStr &= " INNER JOIN tcpc0.dbo.wo_Tec w ON w.id = cd.wocd_proc_id "
            sqlStr &= " where cd.id is not null and isnull(cd.wocd_pcost,0)>0 "
            If Request("nbr") <> Nothing Then
                sqlStr &= " and cd.wocd_nbr ='" & Request("nbr") & "' "
            End If
            If Request("id") <> Nothing Then
                sqlStr &= " and cd.wocd_id ='" & Request("id") & "' "
            End If
            If Request("cc") <> "0" Then
                sqlStr &= " and cd.wocd_cc ='" & Request("cc") & "' "
            End If
            sqlStr &= " and cd.wocd_site ='" & Request("site") & "' "
            If Request("p") <> Nothing Then
                sqlStr &= " and cd.wocd_part like '%" & Request("p") & "%' "
            End If
            sqlStr &= " group by cd.wocd_site,cd.wocd_cc,cd.wocd_nbr,cd.wocd_id,cd.wocd_part,cd.wocd_pcost,cd.wocd_type) s "
            sqlStr &= " Left Outer Join wo_cost ct on ct.wo_site=s.wocd_site and ct.wo_nbr=s.wocd_nbr and isnull(ct.wo_id,0)=isnull(s.wocd_id,0) "
            sqlStr &= " order by s.wocd_site,s.wocd_cc,s.wocd_nbr,s.wocd_id,s.wocd_part "

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                If reader(7).ToString().Trim() = "" Or reader(7).ToString().Trim() = "R" Then

                    dm = GetDomain(reader(0).ToString())

                    Dim conn As OdbcConnection = Nothing
                    Dim comm As OdbcCommand = Nothing
                    Dim dr As OdbcDataReader

                    Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                    ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

                    Dim sql As String = "Select wo_qty_comp,wo_close_date from PUB.wo_mstr where wo_domain='" & dm & "' "
                    sql &= " and wo_site='" & reader(0).ToString() & "' "
                    sql &= " and wo_nbr='" & reader(1).ToString() & "' "
                    sql &= " and wo_lot='" & reader(2).ToString() & "' "

                    Try
                        conn = New OdbcConnection(connectionString)
                        conn.Open()
                        comm = New OdbcCommand(sql, conn)
                        dr = comm.ExecuteReader()
                        If (dr.Read()) Then
                            If Not IsDBNull(dr.GetValue(1)) Then
                                If Format(dr.GetValue(1), "yyMM") = Request("yy") Then
                                    Str = reader(0).ToString() & "~^"
                                    Str &= reader(1).ToString() & "~^"
                                    Str &= reader(2).ToString() & "~^"
                                    Str &= reader(3).ToString() & "~^"
                                    Str &= reader(4).ToString() & "~^"

                                    Str = Str & Format(dr.GetValue(0), "##0.#####") & "~^"
                                    Str = Str & Format(reader(9), "##0.#####") & "~^"
                                    Str = Str & Format(reader(9) * dr.GetValue(0), "##0.#####") & "~^"
                                    'Str = Str & Format(reader(8), "##0.#####") & "~^"
                                    Str = Str & Format(dr.GetValue(1), "yy-MM-dd")
                                    Str = Str & "~^"
                                    Str = Str & Format(reader(5), "##0.#####") & "~^"

                                    If reader(7).ToString().Trim() = "R" Then
                                        total_wo = total_wo + reader(5)
                                        Str = Str & "0~^"
                                    Else
                                        total_wo = total_wo + reader(9) * dr.GetValue(0)
                                        Str = Str & Format(reader(9) * dr.GetValue(0) - reader(5), "##0.#####") & "~^"
                                    End If
                                    total_lb = total_lb + reader(5)
                                    PIMasteryRow(Str)

                                End If
                            End If
                        End If
                        dr.Close()
                        conn.Close()

                    Catch oe As OdbcException
                        Response.Write(oe.Message)
                    Finally
                        If conn.State = System.Data.ConnectionState.Open Then
                            conn.Close()
                        End If
                    End Try

                    comm.Dispose()
                    conn.Dispose()
                ElseIf reader(7).ToString().Trim() = "T" Then
                    If reader(1).ToString().Substring(2) = Request("yy") Then
                        Str = reader(0).ToString() & "~^"
                        Str &= reader(1).ToString() & "~^"
                        Str &= reader(2).ToString() & "~^"
                        Str &= reader(3).ToString() & "~^"
                        Str &= reader(4).ToString() & "~^"

                        Str = Str & "0~^"
                        Str = Str & Format(0, "##0.#####") & "~^"
                        Str = Str & Format(reader(5), "##0.#####") & "~^"
                        'Str = Str & Format(reader(8), "##0.#####") & "~^"
                        Str = Str & reader(1).ToString()
                        Str = Str & "~^"
                        Str = Str & Format(reader(5), "##0.#####") & "~^"
                        Str = Str & Format(0 - reader(5), "##0.#####") & "~^"

                        total_wo = total_wo + 0
                        total_lb = total_lb + reader(5)
                        PIMasteryRow(Str)

                    End If
                Else
                    Dim conn As SqlConnection = Nothing
                    Dim comm As SqlCommand = Nothing
                    Dim dr As SqlDataReader

                    Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn" & Session("PlantCode"))

                    Dim sql As String = "Select isnull(woo_qty_comp,0),acctApprDate  from wo_order where woo_site='" & reader(0).ToString().Trim() & "' and woo_nbr='" & reader(1).ToString().Trim() & "' and deletedBy is null"

                    Try
                        conn = New SqlConnection(connectionString)
                        conn.Open()
                        comm = New SqlCommand(sql, conn)
                        dr = comm.ExecuteReader()
                        If (dr.Read()) Then
                            If Not IsDBNull(dr.GetValue(1)) Then
                                If Format(dr.GetValue(1), "yyMM") = Request("yy") Then
                                    Str = reader(0).ToString() & "~^"
                                    Str &= reader(1).ToString() & "~^"
                                    Str &= reader(2).ToString() & "~^"
                                    Str &= reader(3).ToString() & "~^"
                                    Str &= reader(4).ToString() & "~^"

                                    Str = Str & Format(dr.GetValue(0), "##0.#####") & "~^"
                                    Str = Str & Format(0, "##0.#####") & "~^"
                                    Str = Str & Format(0, "##0.#####") & "~^"
                                    'Str = Str & Format(reader(8), "##0.#####") & "~^"
                                    Str = Str & Format(dr.GetValue(1), "yy-MM-dd")
                                    Str = Str & "~^"
                                    Str = Str & Format(reader(5), "##0.#####") & "~^"
                                    Str = Str & "0~^"

                                    total_wo = total_wo + reader(5)
                                    total_lb = total_lb + reader(5)
                                    PIMasteryRow(Str)

                                End If
                            End If
                        End If
                        dr.Close()
                        conn.Close()

                    Catch oe As OdbcException
                        Response.Write(oe.Message)
                    Finally
                        If conn.State = System.Data.ConnectionState.Open Then
                            conn.Close()
                        End If
                    End Try

                    comm.Dispose()
                    conn.Dispose()

                End If

            End While
            reader.Close()

            Str = "合计~^~^~^~^~^~^~^~^~^~^"
            Str &= Format(total_lb, "##0.##") & "~^"
            Str &= Format(total_wo - total_lb, "##0.##") & "~^"
            PIMasteryRow(Str)


            While (i < 100)
                PIMasteryRow("")
                i = i + 1
            End While
            'Exit Sub
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
            Response.Flush()
        End Sub

        Sub PIMasteryRow(ByVal str As String, Optional ByVal isTitle As Boolean = False)
            row = New TableRow
            If isTitle Then
                row.BackColor = Color.LightGray
                row.HorizontalAlign = HorizontalAlign.Center
            Else
                row.BackColor = Color.White
                row.HorizontalAlign = HorizontalAlign.Left
            End If
            row.VerticalAlign = VerticalAlign.Top
            row.Font.Size = FontUnit.Point(9)

            row.BorderWidth = New Unit(0)

            Dim i As Integer = 0
            Dim ind As Integer

            If isTitle Then
                Dim wid As Integer
                For i = 0 To 200
                    wid = 100
                    cell = New TableCell
                    ind = str.IndexOf("~^")
                    If (ind = -1) Then
                        ind = str.IndexOf("L~")
                        If (ind > -1) Then
                            cell.HorizontalAlign = HorizontalAlign.Left
                            str = str.Substring(2)
                        End If

                        ind = str.IndexOf("^")
                        If (ind = -1) Then
                            cell.Text = str
                        Else
                            wid = Convert.ToInt32(str.Substring(0, ind))
                            cell.Text = str.Substring(ind + 1)
                        End If
                        str = ""
                        cell.Width = New Unit(wid)
                        row.Cells.Add(cell)
                        Exit For
                    Else
                        cell.Text = str.Substring(0, ind)
                        str = str.Substring(ind + 2)

                        ind = cell.Text.IndexOf("L~")
                        If (ind > -1) Then
                            cell.HorizontalAlign = HorizontalAlign.Left
                            cell.Text = cell.Text.Substring(2)
                        End If

                        ind = cell.Text.IndexOf("^")
                        If (ind > -1) Then
                            wid = Convert.ToInt32(cell.Text.Substring(0, ind))
                            cell.Text = cell.Text.Substring(ind + 1)
                        End If
                    End If
                    cell.Width = New Unit(wid)
                    row.Cells.Add(cell)
                Next

                If i < 50 Then
                    For ind = i To 50
                        cell = New TableCell
                        cell.Text = ""
                        cell.Width = New Unit(60)
                        row.Cells.Add(cell)
                    Next
                End If
            Else
                For i = 0 To 200
                    cell = New TableCell
                    ind = str.IndexOf("~^")
                    If (ind = -1) Then
                        cell.Text = str
                        If IsNumeric(cell.Text) Then
                            If cell.Text.Trim.Length > 8 Then
                                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                            End If
                            cell.HorizontalAlign = HorizontalAlign.Right
                        ElseIf IsDate(cell.Text) Then
                            If CDate(cell.Text) = CDate("1900-01-01") Then
                                cell.Text = ""
                            Else
                                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
                            End If
                        End If
                        str = ""
                        row.Cells.Add(cell)
                        Exit For
                    Else
                        cell.Text = str.Substring(0, ind)
                        str = str.Substring(ind + 2)
                        If IsNumeric(cell.Text) Then
                            If cell.Text.Trim.Length > 8 Then
                                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                            End If
                            cell.HorizontalAlign = HorizontalAlign.Right
                        ElseIf IsDate(cell.Text) Then
                            If CDate(cell.Text) = CDate("1900-01-01") Then
                                cell.Text = ""
                            Else
                                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
                            End If
                        End If
                    End If
                    row.Cells.Add(cell)
                Next

                If i < 50 Then
                    For ind = i To 50
                        cell = New TableCell
                        cell.Text = ""
                        cell.Width = New Unit(60)
                        row.Cells.Add(cell)
                    Next
                End If
            End If

            exl.Rows.Add(row)
        End Sub

        Sub PIHeaderRow(ByVal str As String)
            row = New TableRow
            row.BackColor = Color.White
            row.HorizontalAlign = HorizontalAlign.Left
            row.BorderWidth = New Unit(0)

            Dim i As Integer = 0
            Dim ind As Integer

            While (str.Length > 0)
                cell = New TableCell
                ind = str.IndexOf("~")
                If (ind = -1) Then
                    cell.Text = str
                    str = ""
                Else
                    cell.Text = str.Substring(0, ind)
                    str = str.Substring(ind + 1)
                End If
                'If (i = 0) Then
                '    i = 1
                'Else
                cell.ColumnSpan = 5
                i = i + 10
                'End If
                If IsNumeric(cell.Text) Then
                    'If cell.Text.Trim.Length > 8 Then
                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                    'End If
                ElseIf IsDate(cell.Text) Then
                    If CDate(cell.Text) = CDate("1900-01-01") Then
                        cell.Text = ""
                    Else
                        cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
                    End If
                End If
                row.Cells.Add(cell)
            End While

            If i < 50 Then
                For ind = i To 50
                    cell = New TableCell
                    cell.Text = ""
                    cell.Width = New Unit(60)
                    row.Cells.Add(cell)
                Next
            End If

            exl.Rows.Add(row)
        End Sub

        Function GetDomain(ByVal str As String) As String
            Dim StrSql As String
            StrSql = "SELECT realdomain FROM Domain_Mes WHERE qad_site ='" & str & "' "
            GetDomain = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, StrSql)
        End Function

    End Class

End Namespace
