Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class ws_statistics21
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
                If Request("dd") <> Nothing Then
                    txb_date.Text = Request("dd")
                Else
                    txb_date.Text = Format(Today, "yyyy-MM-dd")
                End If

                If Request("site") <> Nothing Then
                    ddl_site.SelectedValue = Request("site")
                End If



                Dim StrSql As String

                StrSql = "Delete from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "'or userid=0"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

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


                ddl_type.SelectedValue = 2

                LoadData()
            End If
        End Sub
        Private Sub LoadData()
            Dim StrSql As String
            Dim ds As DataSet
            Dim i As Integer
            Dim iYear As Integer
            Dim iMonth As Integer
            Dim iDay As Integer
            Dim iEnd As Integer

            Dim strDate As String = ""

            If txb_date.Text.Trim.Length > 0 Then
                If Not IsDate(txb_date.Text) Then
                    If Request("dd") <> Nothing Then
                        txb_date.Text = Request("dd")
                    Else
                        txb_date.Text = Format(Today, "yyyy-MM-dd")
                    End If
                End If

                iYear = CDate(txb_date.Text).Year
                iMonth = CDate(txb_date.Text).Month
                iDay = CDate(txb_date.Text).Day
                iEnd = DateTime.DaysInMonth(iYear, iMonth)
                strDate = iYear.ToString & "-" & iMonth.ToString & "-01"

                For i = 0 To iEnd - 1
                    StrSql = "Insert into SZXWS.LineData_SZX.dbo.ls_display(userid,dispx,qty,qty_bad) "
                    StrSql &= " values('" & Session("uID") & "','" & i.ToString & "',0,0)"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                Next

                If ddl_site.SelectedValue = 1 Then
                    StrSql = " select isnull(ls_qty,0),isnull(ls_status,N'正品'),createdDate from SZXWS.LineData_SZX.dbo.ls_data "
                ElseIf ddl_site.SelectedValue = 2 Then
                    StrSql = " select isnull(ls_qty,0),isnull(ls_status,N'正品'),createdDate from ZQLWS.LineData_ZQL.dbo.ls_data "
                Else
                    StrSql = " select isnull(ls_qty,0),isnull(ls_status,N'正品'),createdDate from YQLWS.LineData_YQL.dbo.ls_data "
                End If
                StrSql &= " where deletedBy is null "
                StrSql &= " and ls_plant ='" & ddl_site.SelectedValue & "'"
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

                StrSql &= " and year(createdDate)=year('" & txb_date.Text & "') and month(createdDate)=month('" & txb_date.Text & "')"
                StrSql &= " order by ls_cc,ls_line "
                'Response.Write(StrSql)
                'Exit Sub

                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)


                With ds.Tables(0)
                    If .Rows.Count > 0 Then
                        For i = 0 To .Rows.Count - 1
                            StrSql = "Update SZXWS.LineData_SZX.dbo.ls_display set "
                            If .Rows(i).Item(1) <> "正品" Then
                                StrSql &= "qty_bad=qty_bad +  " & .Rows(i).Item(0)
                            Else
                                StrSql &= "qty=qty +  " & .Rows(i).Item(0)
                            End If
                            total = total + .Rows(i).Item(0)
                            StrSql &= " where userid='" & Session("uID") & "' and dispx=Datediff(day,'" & strDate & "','" & .Rows(i).Item(2) & "')"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                        Next
                    End If
                End With
                ds.Reset()
            End If
        End Sub

       
        Protected Sub ddl_site_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_site.SelectedIndexChanged
            While ddl_cc.Items.Count > 0
                ddl_cc.Items.RemoveAt(0)
            End While
            While ddl_line.Items.Count > 0
                ddl_line.Items.RemoveAt(0)
            End While

            If ddl_site.SelectedIndex = 0 Then
                Exit Sub
            End If

            LoadCC()

        End Sub

        Protected Sub ddl_cc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_cc.SelectedIndexChanged
            While ddl_line.Items.Count > 0
                ddl_line.Items.RemoveAt(0)
            End While

            If ddl_cc.SelectedIndex = 0 Then
                Exit Sub
            End If

            LoadLine()

        End Sub

        Protected Sub ddl_line_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_line.SelectedIndexChanged
            Dim StrSql As String
            StrSql = "Delete from SZXWS.LineData_SZX.dbo.ls_display where userid='" & Session("uID") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

            LoadData()
        End Sub

        Protected Sub btn_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_list.Click
            If ddl_type.SelectedValue = 1 Then
                Response.Redirect("ws_statistics.aspx?dd=" & txb_date.Text & "&site=" & ddl_site.SelectedValue & "&cc=" & ddl_cc.SelectedValue & "&line=" & ddl_line.SelectedValue & "&part=" & txb_part.Text)
            ElseIf ddl_type.SelectedValue = 2 Then
                Response.Redirect("ws_statistics21.aspx?dd=" & txb_date.Text & "&site=" & ddl_site.SelectedValue & "&cc=" & ddl_cc.SelectedValue & "&line=" & ddl_line.SelectedValue & "&part=" & txb_part.Text)
            ElseIf ddl_type.SelectedValue = 3 Then
                Response.Redirect("ws_statistics31.aspx?dd=" & txb_date.Text & "&site=" & ddl_site.SelectedValue & "&cc=" & ddl_cc.SelectedValue & "&line=" & ddl_line.SelectedValue & "&part=" & txb_part.Text)
            ElseIf ddl_type.SelectedValue = 4 Then
                Response.Redirect("ws_statistics41.aspx?dd=" & txb_date.Text & "&site=" & ddl_site.SelectedValue & "&cc=" & ddl_cc.SelectedValue & "&line=" & ddl_line.SelectedValue & "&part=" & txb_part.Text)
            ElseIf ddl_type.SelectedValue = 5 Then
                Response.Redirect("ws_statistics51.aspx?dd=" & txb_date.Text & "&site=" & ddl_site.SelectedValue & "&cc=" & ddl_cc.SelectedValue & "&line=" & ddl_line.SelectedValue & "&part=" & txb_part.Text)
            End If

        End Sub


        Protected Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
            Response.Redirect("ws_display.aspx?dd=" & Request("dd") & "&part=" & Request("part"))
        End Sub

        Private Sub LoadCC()
            While ddl_cc.Items.Count > 0
                ddl_cc.Items.RemoveAt(0)
            End While
            While ddl_line.Items.Count > 0
                ddl_line.Items.RemoveAt(0)
            End While

          

            Dim ls As ListItem
            Dim StrSql As String
            Dim reader As SqlDataReader

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            ddl_cc.Items.Add(ls)

            If ddl_site.SelectedValue = 1 Then
                StrSql = "select ls_cc_no,ls_cc_name  from SZXWS.LineData_SZX.dbo.ls_cc where ls_cc_plant='" & ddl_site.SelectedValue & "'"
            ElseIf ddl_site.SelectedValue = 2 Then
                StrSql = "select ls_cc_no,ls_cc_name  from ZQLWS.LineData_ZQL.dbo.ls_cc where ls_cc_plant='" & ddl_site.SelectedValue & "'"
            Else
                StrSql = "select ls_cc_no,ls_cc_name  from YQLWS.LineData_YQL.dbo.ls_cc where ls_cc_plant='" & ddl_site.SelectedValue & "'"
            End If
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

            If ddl_cc.SelectedIndex = 0 Then
                Exit Sub
            End If

            Dim ls As ListItem
            Dim StrSql As String
            Dim reader As SqlDataReader

            ls = New ListItem
            ls.Value = 0
            ls.Text = "--"
            ddl_line.Items.Add(ls)

            If ddl_site.SelectedValue = 1 Then
                StrSql = "select ls_line_id,ls_line_name  from SZXWS.LineData_SZX.dbo.ls_line where ls_line_plant='" & ddl_site.SelectedValue & "' and ls_line_cc='" & ddl_cc.SelectedValue & "'"
            ElseIf ddl_site.SelectedValue = 2 Then
                StrSql = "select ls_line_id,ls_line_name  from ZQLWS.LineData_ZQL.dbo.ls_line where ls_line_plant='" & ddl_site.SelectedValue & "' and ls_line_cc='" & ddl_cc.SelectedValue & "'"
            Else
                StrSql = "select ls_line_id,ls_line_name  from YQLWS.LineData_YQL.dbo.ls_line where ls_line_plant='" & ddl_site.SelectedValue & "' and ls_line_cc='" & ddl_cc.SelectedValue & "'"
            End If
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
    End Class

End Namespace













