Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc

Namespace tcpc
    Partial Class pl_exportExcel
        Inherits BasePage 

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
            'Put user code to initialize the page here 

            Dim Str As String = ""
            Dim i As Integer

            If Request("ty") = 1 Then
                PIHeaderRow("元器件供应商备料--" & Format(Now, "yyyy-MM-dd"))

                Str = "100^<b>零件号</b>~^230^<b>描述</b>~^20^<b>产品类</b>~^20^<b>单位<b>~^130^<b>数量</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^"
                PIMasteryRow(Str, True)

                sqlStr = "select pl_comp,pl_domain, sum(pl_qty) from qadplan.dbo.pl_alloc where pl_userid='" & Session("uID") & "' and pl_is=1 and pl_status='P' group by pl_comp,pl_domain order by pl_comp"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
                While (reader.Read())
                    Str = reader(0).ToString() & "~^"

                    Dim conn As OdbcConnection
                    Dim comm As OdbcCommand
                    Dim dr As OdbcDataReader

                    Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                    ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

                    Dim sql As String = "Select pt_desc1,pt_prod_line,pt_um from PUB.pt_mstr where pt_domain='" & reader(1).ToString() & "' and pt_part='" & reader(0).ToString() & "'"
                    Try
                        conn = New OdbcConnection(connectionString)
                        conn.Open()
                        comm = New OdbcCommand(sql, conn)
                        dr = comm.ExecuteReader()
                        While (dr.Read())
                            Str = Str & dr.GetValue(0).ToString() & "~^"
                            Str = Str & dr.GetValue(1).ToString() & "~^"
                            Str = Str & dr.GetValue(2).ToString() & "~^"
                        End While
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

                    Str = Str & reader(2).ToString() & "~^"

                    Dim pclist As String = ""
                    sql = "Select distinct pc_list,pc_amt from PUB.pc_mstr where pc_domain='" & reader(1).ToString() & "' and pc_part='" & reader(0).ToString() & "' and (pc_expire is null or pc_expire>='" & Today & "') order by pc_list,pc_amt"
                    Response.Write(sql)

                    Try
                        conn = New OdbcConnection(connectionString)
                        conn.Open()
                        comm = New OdbcCommand(sql, conn)
                        dr = comm.ExecuteReader()
                        While (dr.Read())
                            If pclist <> dr.GetValue(0).ToString() Then
                                Str = Str & dr.GetValue(0).ToString() & "~^"
                                pclist = dr.GetValue(1).ToString()
                                Str = Str & pclist.Substring(0, pclist.IndexOf(";")) & "~^"
                                pclist = dr.GetValue(0).ToString()
                            End If
                        End While
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


                    PIMasteryRow(Str)
                End While
                reader.Close()
            ElseIf Request("ty") = 2 Then
                PIHeaderRow("毛管原料供应商备料--" & Format(Now, "yyyy-MM-dd"))

                Str = "100^<b>零件号</b>~^230^<b>描述</b>~^20^<b>产品类</b>~^20^<b>单位<b>~^130^<b>数量</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^60^<b>供应商</b>~^60^<b>采购价</b>~^"
                PIMasteryRow(Str, True)

                sqlStr = "select pl_comp,pl_domain, sum(pl_qty) from qadplan.dbo.pl_alloc where pl_userid='" & Session("uID") & "' and pl_is=1 and pl_status='P' group by pl_comp,pl_domain order by pl_comp"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
                While (reader.Read())
                    Str = reader(0).ToString() & "~^"

                    Dim conn As OdbcConnection
                    Dim comm As OdbcCommand
                    Dim dr As OdbcDataReader

                    Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
                    ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

                    Dim sql As String = "Select pt_desc1,pt_prod_line,pt_um from PUB.pt_mstr where pt_domain='" & reader(1).ToString() & "' and pt_part='" & reader(0).ToString() & "'"
                    Try
                        conn = New OdbcConnection(connectionString)
                        conn.Open()
                        comm = New OdbcCommand(sql, conn)
                        dr = comm.ExecuteReader()
                        While (dr.Read())
                            Str = Str & dr.GetValue(0).ToString() & "~^"
                            Str = Str & dr.GetValue(1).ToString() & "~^"
                            Str = Str & dr.GetValue(2).ToString() & "~^"
                        End While
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

                    Str = Str & reader(2).ToString() & "~^"

                    Dim pclist As String = ""
                    sql = "Select distinct pc_list,pc_amt from PUB.pc_mstr where pc_domain='" & reader(1).ToString() & "' and pc_part='" & reader(0).ToString() & "' and (pc_expire is null or pc_expire>='" & Today & "') order by pc_list,pc_amt"
                    Response.Write(sql)

                    Try
                        conn = New OdbcConnection(connectionString)
                        conn.Open()
                        comm = New OdbcCommand(sql, conn)
                        dr = comm.ExecuteReader()
                        While (dr.Read())
                            If pclist <> dr.GetValue(0).ToString() Then
                                Str = Str & dr.GetValue(0).ToString() & "~^"
                                pclist = dr.GetValue(1).ToString()
                                Str = Str & pclist.Substring(0, pclist.IndexOf(";")) & "~^"
                                pclist = dr.GetValue(0).ToString()
                            End If
                        End While
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


                    PIMasteryRow(Str)
                End While
                reader.Close()

            End If

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
    End Class

End Namespace
