Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class exportExcelQADCompare
    Inherits System.Web.UI.Page

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
        Dim sqlStr11 As String
        Dim reader11 As SqlDataReader
        Dim sqlStr12 As String
        Dim reader12 As SqlDataReader

        Dim str11 As String
        Dim str12 As String
        Dim str13 As String
        Dim part As String = Request("part")

        Dim str As String

        Dim i As Integer = 0

        sqlStr12 = "Delete from Item_qad_test where userid='" & Session("uID") & "'"


        PIHeaderRow("原结构和QAD结构的比较  " & part)
        PIHeaderRow("原结构")

        str11 = "40^<b>级</b>~^120^<b>父零件</b>~^120^<b>子零件</b>~^120^<b>数量</b>~^100^<b>位号</b>~^200^父部件号~^200^子部件号~^220^替代~^120^ ~^120^ ~^120^~^"
        PIMasteryRow(str11, True)

        sqlStr11 = "Select isnull(i1.item_qad,''),isnull(i2.item_qad,''), isnull(ps.numOfChild,0),isnull(ps.posCode,''),isnull(i1.code,''),isnull(i2.code,''),ps.productStruID from Product_stru ps "
        sqlStr11 &= " Inner Join items i1 on i1.id=ps.productID  Inner Join items i2 on i2.id=ps.childID "
        sqlStr11 &= " where i1.item_qad='" & part & "' order by i1.item_qad, i2.item_qad "
        reader11 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr11)
        While (reader11.Read())
            str11 = "0~^"
            For i = 0 To reader11.FieldCount() - 2
                str11 = str11 & reader11(i).ToString() & "~^"
            Next

            str13 = ""
            sqlStr12 = "Select isnull(i.item_qad,'') From product_replace pr Inner Join Items i on i.id=pr.itemID where pr.prodStruID=" & reader11(6).ToString()
            reader12 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr12)
            While (reader12.Read())
                str13 &= "," & reader12(0).ToString()
            End While
            reader12.Close()
            If str13.Length() > 1 Then
                If str13.Substring(0, 1) = "," Then
                    str13 = str13.Substring(1)
                End If
            End If

            str11 = str11 & str13 & "~^"
            PIMasteryRow(str11)

            sqlStr12 = "Insert Into item_qad_test (class,parent,child,qty,userid,repl) values('0','" & reader11(0).ToString() & "','" & reader11(1).ToString() & "','" & reader11(2).ToString() & "','" & Session("uID") & "',N'" & str13 & "')"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, sqlStr12)
            'ExpandLevel(reader11(1), 1, reader11(2))

            str = reader11(5).ToString()
            If str.IndexOf("-BZ") <> -1 Or str.IndexOf("-DC") <> -1 Or str.IndexOf("-DJ") <> -1 Then
                ExpandLevel(str, 1, reader11(2))
            End If
        End While
        reader11.Close()


        PIHeaderRow("QAD结构")
        str12 = "40^<b>级</b>~^120^<b>父零件</b>~^120^<b>子零件</b>~^120^<b>数量</b>~^100^<b>位号</b>~^200^父部件号~^200^子部件号~^220^替代~^"
        PIMasteryRow(str12, True)

        Dim sha As Boolean = False


        sqlStr11 = "Select isnull(qs.parent,''),isnull(qs.child,''),isnull(qs.qty,0),isnull(qs.posCode,''),isnull(i1.code,''),isnull(i2.code,'') from Qad_stru qs"
        sqlStr11 &= " Inner Join items i1 on i1.item_qad=qs.parent  Inner Join items i2 on i2.item_qad=qs.child"
        sqlStr11 &= " where qs.parent='" & part & "' order by qs.parent,qs.child "
        reader11 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr11)
        While (reader11.Read())
            str11 = "0~^"
            For i = 0 To reader11.FieldCount() - 1
                str11 = str11 & reader11(i).ToString() & "~^"
            Next
            str12 = reader11(1)

            str13 = ""
            sqlStr12 = "Select isnull(i.item_qad,'') From Qad_Stru_Replace pr Inner Join Items i on i.item_qad=pr.newchild where pr.parent='" & reader11(0).ToString() & "' and pr.oldchild ='" & reader11(1).ToString() & "'"
            reader12 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr12)
            While (reader12.Read())
                str13 &= "," & reader12(0).ToString()
            End While
            reader12.Close()
            If str13.Length() > 1 Then
                If str13.Substring(0, 1) = "," Then
                    str13 = str13.Substring(1)
                End If

            End If




            sqlStr12 = "Select count(*) from Item_qad_test where class='0' and parent='" & reader11(0).ToString() & "' and child='" & reader11(1).ToString() & "' and qty='" & reader11(2).ToString() & "' and userid='" & Session("uID") & "' and repl =N'" & str13 & "'"
            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, sqlStr12) > 0 Then
                sha = True
            Else
                sha = False
            End If

            'str = reader11(5).ToString()
            'If str.IndexOf("-BZ") <> -1 Then
            '    ExpandLevel2(str, 1, reader11(2))
            'Else
            str11 = str11 & str13 & "~^"
            If sha Then
                PIMasteryRow(str11, False, True)
            Else

                PIMasteryRow(str11)
            End If
            'End If
            'ExpandLevel2(reader11(1), 1, reader11(2))
        End While
        reader11.Close()


        While (i < 100)
            PIMasteryRow("")
            i = i + 1
        End While
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
        Response.Flush()
    End Sub
    Function ExpandLevel(ByVal prod As String, ByVal level As Integer, ByVal qty333 As Decimal)
        Dim sqlStr21 As String
        Dim sqlStr22 As String
        Dim reader21 As SqlDataReader
        Dim reader22 As SqlDataReader

        Dim str21 As String
        Dim j21 As Integer

        Dim str13 As String

        sqlStr21 = "Select isnull(i1.item_qad,''),isnull(i2.item_qad,''), isnull(ps.numOfChild,0)*" & qty333 & ",isnull(ps.posCode,''),isnull(i1.code,''),isnull(i2.code,''),ps.productStruID from Product_stru ps "
        sqlStr21 &= " Inner Join items i1 on i1.id=ps.productID  Inner Join items i2 on i2.id=ps.childID "
        sqlStr21 &= " where i1.code='" & prod & "' order by i1.item_qad, i2.item_qad "

        reader21 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr21)
        While (reader21.Read())
            str21 = level.ToString() & "~^"
            For j21 = 0 To reader21.FieldCount() - 2
                str21 = str21 & reader21(j21).ToString() & "~^"
            Next

            str13 = ""
            sqlStr22 = "Select isnull(i.item_qad,'') From product_replace pr Inner Join Items i on i.id=pr.itemID where pr.prodStruID=" & reader21(6).ToString()
            reader22 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr22)
            While (reader22.Read())
                str13 &= "," & reader22(0).ToString()
            End While
            reader22.Close()
            If str13.Length() > 1 Then
                If str13.Substring(0, 1) = "," Then
                    str13 = str13.Substring(1)
                End If

            End If

            str21 = str21 & str13 & "~^"
            PIMasteryRow(str21)

            sqlStr22 = "Insert Into item_qad_test (class,parent,child,qty,userid,repl) values('" & level.ToString() & "','" & reader21(0).ToString() & "','" & reader21(1).ToString() & "','" & reader21(2).ToString() & "','" & Session("uID") & "',N'" & str13 & "')"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, sqlStr22)

            'ExpandLevel(reader21(4), level + 1, reader21(2))
        End While
        reader21.Close()

    End Function

    Function ExpandLevel2(ByVal prod As String, ByVal level As Integer, ByVal qty333 As Decimal)
        Dim sqlStr31 As String
        Dim reader31 As SqlDataReader
        Dim sqlStr32 As String
        Dim reader32 As SqlDataReader
        Dim str31 As String
        Dim j31 As Integer = level

        Dim str32 As String

        Dim i As Integer

        Dim str As String

        Dim sha As Boolean = False
        sqlStr31 = "Select isnull(qs.parent,''),isnull(qs.child,''),isnull(qs.qty,0)*" & qty333 & ",isnull(qs.posCode,''),isnull(i1.code,''),isnull(i2.code,'') from Qad_stru qs"
        sqlStr31 &= " Inner Join items i1 on i1.item_qad=qs.parent  Inner Join items i2 on i2.item_qad=qs.child "
        sqlStr31 &= " where i2.code='" & prod & "' order by qs.parent,qs.child "
        reader31 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr31)

        While (reader31.Read())
            str31 = level.ToString() & "~^"
            For j31 = 0 To reader31.FieldCount() - 1
                str31 = str31 & reader31(j31).ToString() & "~^"
            Next
            str32 = reader31(1)
            str = reader31(5)

            sqlStr32 = "Select count(*) from Item_qad_test where class='" & level.ToString() & "' and parent='" & reader31(0).ToString() & "' and child='" & reader31(1).ToString() & "' and qty='" & reader31(2).ToString() & "' and userid='" & Session("uID") & "'"
            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, sqlStr32) > 0 Then
                sha = True
            Else
                sha = False
            End If

            If str.IndexOf("-BZ") <> -1 Then
                sqlStr32 = "Select isnull(qs.parent,''),isnull(qs.child,''),isnull(qs.qty,0)*" & qty333 & ",isnull(qs.posCode,''),isnull(i1.code,''),isnull(i2.code,'') from Qad_stru qs"
                sqlStr32 &= " Inner Join items i1 on i1.item_qad=qs.parent  Inner Join items i2 on i2.item_qad=qs.child "
                sqlStr32 &= " where i2.code='" & str & "' order by qs.parent,qs.child "
                reader32 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, sqlStr32)
                While (reader32.Read())
                    str32 = "0~^"
                    For i = 0 To reader32.FieldCount() - 1
                        str32 = str32 & reader32(i).ToString() & "~^"
                    Next
                    PIMasteryRow(str32)
                End While
                reader32.Close()
            Else
                If sha Then
                    PIMasteryRow(str31, False, True)
                Else

                    PIMasteryRow(str31)
                End If
            End If

            'ExpandLevel2(reader31(1), level + 1, reader31(2))
        End While
        reader31.Close()

    End Function



    Sub PIMasteryRow(ByVal str As String, Optional ByVal isTitle As Boolean = False, Optional ByVal isColor As Boolean = False)
        row = New TableRow

        If isTitle Then
            row.BackColor = Color.LightGray
            row.HorizontalAlign = HorizontalAlign.Center
        Else
            row.BackColor = Color.White
            row.HorizontalAlign = HorizontalAlign.Left
        End If

        row.BorderWidth = New Unit(0)


        Dim i As Integer = 0
        Dim ind As Integer

        If isColor = True Then
            row.BackColor = Color.LightPink
        End If

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
            If (i = 0) Then
                i = 1
                cell.ColumnSpan = 4
            Else
                'cell.ColumnSpan = 5
                i = i + 10
            End If
            If IsNumeric(cell.Text) Then
                If cell.Text.Trim.Length > 8 Then
                    cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                End If
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
