Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class ws_anal31
        Inherits BasePage
        Public chk As New adamClass
        Dim nRet As Integer
        'Protected WithEvents ltlAlert As Literal
        Public total As Decimal

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
                'If Request("dd1") <> Nothing Then
                '    txb_date.Text = Request("dd1")
                'Else
                    txb_date.Text = "2009-01-01"
                'End If
                'If Request("dd2") <> Nothing Then
                'txb_date2.Text = Request("dd2")
                'Else
                txb_date2.Text = Format(Today, "yyyy-MM-dd")
                'End If

                If Request("site") <> Nothing Then
                    ddl_site.SelectedValue = Request("site")
                End If

                Dim StrSql As String


                LoadCC()
                If Request("cc") <> Nothing Then
                    ddl_cc.SelectedValue = Request("cc")
                End If

                LoadLine()
                If Request("line") <> Nothing Then
                    ddl_line.SelectedValue = Request("line")
                End If

                If Request("part") <> Nothing Then
                    txb_part.Text = Request("part")
                End If

                ddl_type.SelectedValue = 3

                LoadData()
            End If
        End Sub
        Private Sub LoadData()
            Dim StrSql As String
            Dim ds As DataSet
            Dim i As Integer

            StrSql = "Delete from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "'or userid=0"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)


            Dim strDate As String = ""

            If txb_date.Text.Trim.Length <= 0 And Not IsDate(txb_date.Text) Then
                'If Request("dd1") <> Nothing Then
                '    txb_date.Text = Request("dd1")
                'Else
                    txb_date.Text = "2009-01-01"
                'End If
            End If
            If txb_date2.Text.Trim.Length <= 0 And Not IsDate(txb_date2.Text) Then
                'If Request("dd2") <> Nothing Then
                'txb_date2.Text = Request("dd2")
                'Else
                txb_date2.Text = Format(Today, "yyyy-MM-dd")
                'End If
            End If

            total=0

            For i = 0 To 51
                StrSql = "Insert into SZXWS.LineData_SZX.dbo.ls_display(userid,dispx,qty,qty_bad,isOK) "
                StrSql &= " values('" & Session("uID") & "','" & i.ToString & "',0,0,0)"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
            Next

            StrSql = ""

            If ddl_site.SelectedValue = 1 Or ddl_site.SelectedValue = 0 Then
                StrSql = " select isnull(ls_qty,0),isnull(ls_status,N'正品'),createdDate from SZXWS.LineData_SZX.dbo.ls_data "
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant =1"
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and isnull(ls_cc,0) ='" & ddl_cc.SelectedValue & "' "
                End If
                If ddl_line.SelectedIndex > 0 Then
                    StrSql &= " and isnull(ls_line,0) ='" & ddl_line.SelectedValue & "' "
                End If
                If Not Request("part") Is Nothing Then
                    If Request("part").ToString.Trim.Length > 0 Then
                        StrSql &= " and ls_part like '" & Request("part").Replace("*", "%") & "' "
                    End If
                End If
                StrSql &= " and createdDate>='" & CDate(txb_date.Text) & "'and createdDate<DateAdd(dd, 1, '" & CDate(txb_date2.Text) & "')"
            End If

            If ddl_site.SelectedValue = 2 Or ddl_site.SelectedValue = 0 Then
                StrSql &= " UNION ALL (select isnull(ls_qty,0),isnull(ls_status,N'正品'),createdDate from ZQLWS.LineData_ZQL.dbo.ls_data "
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant =2"
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and isnull(ls_cc,0) ='" & ddl_cc.SelectedValue & "' "
                End If

                If ddl_line.SelectedIndex > 0 Then
                    StrSql &= " and isnull(ls_line,0) ='" & ddl_line.SelectedValue & "' "
                End If

                If Not Request("part") Is Nothing Then
                    If Request("part").ToString.Trim.Length > 0 Then
                        StrSql &= " and ls_part like '" & Request("part").Replace("*", "%") & "' "
                    End If
                End If

                StrSql &= " and createdDate>='" & CDate(txb_date.Text) & "'and createdDate<DateAdd(dd, 1, '" & CDate(txb_date2.Text) & "'))"
            End If
            If ddl_site.SelectedValue = 5 Or ddl_site.SelectedValue = 0 Then
                StrSql &= " UNION ALL (select isnull(ls_qty,0),isnull(ls_status,N'正品'),createdDate from YQLWS.LineData_YQL.dbo.ls_data "
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant =5"
                If ddl_cc.SelectedIndex > 0 Then
                    StrSql &= " and isnull(ls_cc,0) ='" & ddl_cc.SelectedValue & "' "
                End If

                If ddl_line.SelectedIndex > 0 Then
                    StrSql &= " and isnull(ls_line,0) ='" & ddl_line.SelectedValue & "' "
                End If

                If Not Request("part") Is Nothing Then
                    If Request("part").ToString.Trim.Length > 0 Then
                        StrSql &= " and ls_part like '" & Request("part").Replace("*", "%") & "' "
                    End If
                End If

                StrSql &= " and createdDate>='" & CDate(txb_date.Text) & "'and createdDate<DateAdd(dd, 1, '" & CDate(txb_date2.Text) & "'))"
            End If

            StrSql &= " order by createdDate "

            If StrSql.Substring(0, 10) = " UNION ALL" Then
                StrSql = StrSql.Substring(10)
            End If

            'Response.Write(StrSql)
            'Exit Sub

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim mid As Integer

            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    For i = 0 To .Rows.Count - 1
                        mid = 0
                        StrSql = " Select id from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' and createdDate='" & Format(.Rows(i).Item(2), "yyyy") & "' and dispx=DATEPART(wk,'" & .Rows(i).Item(2) & "') and line is null"
                        mid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
                        If mid > 0 Then
                            StrSql = "Update SZXWS.LineData_SZX.dbo.ls_display set "
                            If .Rows(i).Item(1) <> "正品" Then
                                StrSql &= " qty_bad=qty_bad +  " & .Rows(i).Item(0)
                            Else
                                StrSql &= " qty=qty + " & .Rows(i).Item(0)
                            End If
                            total = total + .Rows(i).Item(0)
                            StrSql &= " where id='" & mid & "'"
                        Else
                            StrSql = "Insert into SZXWS.LineData_SZX.dbo.ls_display(userid,dispx,qty,qty_bad,createdDate) "
                            If .Rows(i).Item(1) <> "正品" Then
                                StrSql &= " values('" & Session("uID") & "',DATEPART(wk,'" & .Rows(i).Item(2) & "')" & ",0," & .Rows(i).Item(0) & ",'" & Format(.Rows(i).Item(2), "yyyy") & "')"
                            Else
                                StrSql &= " values('" & Session("uID") & "',DATEPART(wk,'" & .Rows(i).Item(2) & "')" & "," & .Rows(i).Item(0) & ",0,'" & Format(.Rows(i).Item(2), "yyyy") & "')"
                            End If
                        End If
                        Try
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                        Catch
                            Response.Write(StrSql)
                            Exit Sub
                        End Try
                    Next
                End If
            End With
            ds.Reset()

            StrSql = " Select dispx, sum(qty),sum(qty_bad), count(id) from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' and isOK is null and line is null group by dispx"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    For i = 0 To .Rows.Count - 1
                        StrSql = "Update SZXWS.LineData_SZX.dbo.ls_display set qty =" & .Rows(i).Item(1) & ",qty_bad=" & .Rows(i).Item(2) & ",isOK=" & .Rows(i).Item(3)
                        StrSql &= " where userid='" & Session("uID") & "' and dispx=" & .Rows(i).Item(0) & " and isOK is not null and line is null"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                    Next
                End If
            End With
            ds.Reset()


            'sencond line
            If txb_date3.Text.Trim.Length > 0 And  IsDate(txb_date3.Text) Then
               If txb_date4.Text.Trim.Length > 0 And IsDate(txb_date4.Text) Then
            
                    Dim total1 As Decimal = 0

                    For i = 0 To 51
                        StrSql = "Insert into SZXWS.LineData_SZX.dbo.ls_display(userid,dispx,qty,qty_bad,isOK,line) "
                        StrSql &= " values('" & Session("uID") & "','" & i.ToString & "',0,0,0,1)"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                    Next

                    StrSql = ""

                    If ddl_site.SelectedValue = 1 Or ddl_site.SelectedValue = 0 Then
                        StrSql = " select isnull(ls_qty,0),isnull(ls_status,N'正品'),createdDate from SZXWS.LineData_SZX.dbo.ls_data "
                        StrSql &= " where deletedBy is null "
                        StrSql &= " and ls_plant =1"
                        If ddl_cc.SelectedIndex > 0 Then
                            StrSql &= " and isnull(ls_cc,0) ='" & ddl_cc.SelectedValue & "' "
                        End If
                        If ddl_line.SelectedIndex > 0 Then
                            StrSql &= " and isnull(ls_line,0) ='" & ddl_line.SelectedValue & "' "
                        End If
                        If Not Request("part") Is Nothing Then
                            If Request("part").ToString.Trim.Length > 0 Then
                                StrSql &= " and ls_part like '" & Request("part").Replace("*", "%") & "' "
                            End If
                        End If
                        StrSql &= " and createdDate>='" & CDate(txb_date3.Text) & "'and createdDate<DateAdd(dd, 1, '" & CDate(txb_date4.Text) & "')"
                    End If

                    If ddl_site.SelectedValue = 2 Or ddl_site.SelectedValue = 0 Then
                        StrSql &= " UNION ALL (select isnull(ls_qty,0),isnull(ls_status,N'正品'),createdDate from ZQLWS.LineData_ZQL.dbo.ls_data "
                        StrSql &= " where deletedBy is null "
                        StrSql &= " and ls_plant =2"
                        If ddl_cc.SelectedIndex > 0 Then
                            StrSql &= " and isnull(ls_cc,0) ='" & ddl_cc.SelectedValue & "' "
                        End If

                        If ddl_line.SelectedIndex > 0 Then
                            StrSql &= " and isnull(ls_line,0) ='" & ddl_line.SelectedValue & "' "
                        End If

                        If Not Request("part") Is Nothing Then
                            If Request("part").ToString.Trim.Length > 0 Then
                                StrSql &= " and ls_part like '" & Request("part").Replace("*", "%") & "' "
                            End If
                        End If

                        StrSql &= " and createdDate>='" & CDate(txb_date3.Text) & "'and createdDate<DateAdd(dd, 1, '" & CDate(txb_date4.Text) & "'))"
                    End If
                    If ddl_site.SelectedValue = 5 Or ddl_site.SelectedValue = 0 Then
                        StrSql &= " UNION ALL (select isnull(ls_qty,0),isnull(ls_status,N'正品'),createdDate from YQLWS.LineData_YQL.dbo.ls_data "
                        StrSql &= " where deletedBy is null "
                        StrSql &= " and ls_plant =5"
                        If ddl_cc.SelectedIndex > 0 Then
                            StrSql &= " and isnull(ls_cc,0) ='" & ddl_cc.SelectedValue & "' "
                        End If

                        If ddl_line.SelectedIndex > 0 Then
                            StrSql &= " and isnull(ls_line,0) ='" & ddl_line.SelectedValue & "' "
                        End If

                        If Not Request("part") Is Nothing Then
                            If Request("part").ToString.Trim.Length > 0 Then
                                StrSql &= " and ls_part like '" & Request("part").Replace("*", "%") & "' "
                            End If
                        End If

                        StrSql &= " and createdDate>='" & CDate(txb_date3.Text) & "'and createdDate<DateAdd(dd, 1, '" & CDate(txb_date4.Text) & "'))"
                    End If

                    StrSql &= " order by createdDate "

                    If StrSql.Substring(0, 10) = " UNION ALL" Then
                        StrSql = StrSql.Substring(10)
                    End If

                    'Response.Write(StrSql)
                    'Exit Sub

                    ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)


                    With ds.Tables(0)
                        If .Rows.Count > 0 Then
                            For i = 0 To .Rows.Count - 1
                                mid = 0
                                StrSql = " Select id from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' and createdDate='" & Format(.Rows(i).Item(2), "yyyy") & "' and dispx=DATEPART(wk,'" & .Rows(i).Item(2) & "') and line is not null"
                                mid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
                                If mid > 0 Then
                                    StrSql = "Update SZXWS.LineData_SZX.dbo.ls_display set "
                                    If .Rows(i).Item(1) <> "正品" Then
                                        StrSql &= " qty_bad=qty_bad +  " & .Rows(i).Item(0)
                                    Else
                                        StrSql &= " qty=qty + " & .Rows(i).Item(0)
                                    End If
                                    total1 = total1 + .Rows(i).Item(0)
                                    StrSql &= " where id='" & mid & "'"
                                Else
                                    StrSql = "Insert into SZXWS.LineData_SZX.dbo.ls_display(userid,dispx,qty,qty_bad,createdDate,line) "
                                    If .Rows(i).Item(1) <> "正品" Then
                                        StrSql &= " values('" & Session("uID") & "',DATEPART(wk,'" & .Rows(i).Item(2) & "')" & ",0," & .Rows(i).Item(0) & ",'" & Format(.Rows(i).Item(2), "yyyy") & "',1)"
                                    Else
                                        StrSql &= " values('" & Session("uID") & "',DATEPART(wk,'" & .Rows(i).Item(2) & "')" & "," & .Rows(i).Item(0) & ",0,'" & Format(.Rows(i).Item(2), "yyyy") & "',1)"
                                    End If
                                End If
                                Try
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                                Catch
                                    Response.Write(StrSql)
                                    Exit Sub
                                End Try
                            Next
                        End If
                    End With
                    ds.Reset()

                    StrSql = " Select dispx, sum(qty),sum(qty_bad), count(id) from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "' and isOK is null and line is not null group by dispx"
                    ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                    With ds.Tables(0)
                        If .Rows.Count > 0 Then
                            For i = 0 To .Rows.Count - 1
                                StrSql = "Update SZXWS.LineData_SZX.dbo.ls_display set qty =" & .Rows(i).Item(1) & ",qty_bad=" & .Rows(i).Item(2) & ",isOK=" & .Rows(i).Item(3)
                                StrSql &= " where userid='" & Session("uID") & "' and dispx=" & .Rows(i).Item(0) & " and isOK is not null and line is not null"
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                            Next
                        End If
                    End With
                    ds.Reset()

                    If total1 > total Then
                        total = total1
                    End If


               end if
            end if

        End Sub
        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            If ddl_type.SelectedValue = 1 Then
                Response.Redirect("ws_anal.aspx?dd1=" & txb_date.Text & "&dd2=" & txb_date2.Text & "&site=" & ddl_site.SelectedValue & "&cc=" & ddl_cc.SelectedValue & "&line=" & ddl_line.SelectedValue & "&part=" & txb_part.Text)
            ElseIf ddl_type.SelectedValue = 2 Then
                Response.Redirect("ws_anal21.aspx?dd1=" & txb_date.Text & "&dd2=" & txb_date2.Text & "&site=" & ddl_site.SelectedValue & "&cc=" & ddl_cc.SelectedValue & "&line=" & ddl_line.SelectedValue & "&part=" & txb_part.Text)
            ElseIf ddl_type.SelectedValue = 3 Then
                LoadData()
            ElseIf ddl_type.SelectedValue = 4 Then
                Response.Redirect("ws_anal41.aspx?dd1=" & txb_date.Text & "&dd2=" & txb_date2.Text & "&site=" & ddl_site.SelectedValue & "&cc=" & ddl_cc.SelectedValue & "&line=" & ddl_line.SelectedValue & "&part=" & txb_part.Text)
            End If

        End Sub
        Private Sub LoadCC()
            While ddl_cc.Items.Count > 0
                ddl_cc.Items.RemoveAt(0)
            End While

            Dim ls As ListItem
            Dim StrSql As String
            Dim reader As SqlDataReader

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            ddl_cc.Items.Add(ls)

            StrSql = "select ls_cc_no,'SZX-' + ls_cc_name  from SZXWS.LineData_SZX.dbo.ls_cc where ls_cc_plant='1'"

            StrSql &= " UNION ALL (select ls_cc_no,'ZQL-' +ls_cc_name  from ZQLWS.LineData_ZQL.dbo.ls_cc where ls_cc_plant='2') "

            StrSql &= " UNION ALL (select ls_cc_no,'YQL-' +ls_cc_name  from YQLWS.LineData_YQL.dbo.ls_cc where ls_cc_plant='5')"

            'Response.Write(strSql)
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                ddl_cc.Items.Add(ls)
            End While
            reader.Close()
        End Sub
        Private Sub LoadLine()
            While ddl_line.Items.Count > 0
                ddl_line.Items.RemoveAt(0)
            End While

            Dim ls As ListItem
            Dim StrSql As String
            Dim reader As SqlDataReader

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            ddl_line.Items.Add(ls)

            StrSql = "select ls_line_id,'SZX-' +ls_line_name  from SZXWS.LineData_SZX.dbo.ls_line where ls_line_plant='1'"
            StrSql &= " UNION ALL (select ls_line_id,'ZQL-' +ls_line_name  from ZQLWS.LineData_ZQL.dbo.ls_line where ls_line_plant='2' )"
            StrSql &= " UNION ALL (select ls_line_id,'YQL-' +ls_line_name  from YQLWS.LineData_YQL.dbo.ls_line where ls_line_plant='5')"
            'Response.Write(strSql)
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim
                ddl_line.Items.Add(ls)
            End While
            reader.Close()
        End Sub

        Protected Sub ddl_site_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_site.SelectedIndexChanged
            LoadCC()
        End Sub

        Protected Sub ddl_cc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_cc.SelectedIndexChanged
            LoadLine()
        End Sub
    End Class

End Namespace













