Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

    Partial Class conn_list
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim item As ListItem
        Dim nRet As Integer





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

            If Not IsPostBack Then

                BindData()
            End If
        End Sub

        Sub BindData()

            StrSql = "select conn_mstr_id,conn_content,conn_user_id,conn_user_name,conn_date,conn_dept"
            StrSql &= " from KnowDB.dbo.conn_mstr where conn_closeddate is null and conn_deleteddate is null "

            If txb_uname.Text.Trim.Length > 0 Then
                StrSql &= " and conn_user_name like N'%" & txb_uname.Text.Trim & "%' "
            End If

            If txb_dept.Text.Trim.Length > 0 Then
                StrSql &= " and conn_dept like N'%" & txb_dept.Text.Trim & "%' "
            End If

            If txtCode.Text.Trim.Length > 0 Then
                StrSql &= " and LOWER(conn_content) like N'%" & chk.sqlEncode(txtCode.Text.Trim.ToLower) & "%' "
            End If
            StrSql &= " and (conn_mstr_id in (Select distinct conn_mstr_id From KnowDB.dbo.conn_taken Where conn_taken_id ='" & Session("uID") & "'"

            If CheckBox1.Checked Then
                StrSql &= " and conn_taken_date is null)"
            Else
                StrSql &= " )"
            End If

            StrSql &= " Or Conn_user_id = '" & Session("uID") & "')"
            StrSql &= " order by conn_date desc "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim total As Integer

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("mstrid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("userid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("docuser", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("doccont", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docdate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("doctaken", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("doccc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("doccontent", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("docdept", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        Dim reader As SqlDataReader
                        Dim reader1 As SqlDataReader
                        Dim reader2 As SqlDataReader
                        Dim Str As String
                        Dim sq As String
                        Dim sr As String
                        Dim txt As String = ""
                        Dim cxt As String = ""
                        Dim con As String = ""

                        If CheckBox1.Checked = True And Session("uRole") <> 1 Then
                            Str = " Select distinct conn_taken_name " _
                                   & " From KnowDB.dbo.conn_taken  where conn_mstr_id='" & .Rows(i).Item(0) & "' " _
                                   & " and conn_taken_date is null and conn_taken_id='" & Session("uID") & "'"
                            sq = " Select conn_taken_name " _
                                   & " From KnowDB.dbo.conn_taken  where conn_mstr_id='" & .Rows(i).Item(0) & "' " _
                                   & " and conn_taken_date is null and conn_type = 2 and conn_date=(select top 1 conn_date from KnowDB.dbo.conn_taken  where conn_mstr_id='" & .Rows(i).Item(0) & "' and conn_taken_id='" & Session("uID") & "' order by conn_resp_id desc) order by conn_date,conn_resp_id "
                        Else
                            Str = " Select conn_taken_name " _
                                        & " From KnowDB.dbo.conn_taken  where conn_mstr_id='" & .Rows(i).Item(0) & "' " _
                                        & " and conn_taken_date is null and conn_type = 1 order by conn_date,conn_resp_id "
                            sq = " Select conn_taken_name " _
                                    & " From KnowDB.dbo.conn_taken  where conn_mstr_id='" & .Rows(i).Item(0) & "' " _
                                    & " and conn_taken_date is null and conn_type = 2 order by conn_date,conn_resp_id "
                        End If
                        sr = " Select conn_content,conn_user_name  " _
                           & " From KnowDB.dbo.conn_detail where conn_mstr_id='" & .Rows(i).Item(0) & "' " _
                           & " order by conn_detail_id "
                        reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, Str)
                        While (reader.Read())
                            txt = txt + "," + reader(0).ToString().Trim()
                        End While
                        reader.Close()
                        reader1 = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, sq)
                        While (reader1.Read())
                            cxt = cxt + "," + reader1(0).ToString().Trim()
                        End While
                        reader1.Close()
                        reader2 = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, sr)
                        While (reader2.Read())
                            con = con + reader2(1).ToString().Trim() & "--" + reader2(0).ToString().Trim() & "<br>"
                        End While
                        reader2.Close()
                        If txt.Length > 0 Then
                            drow = dtl.NewRow()
                            drow.Item("mstrid") = .Rows(i).Item(0)
                            drow.Item("userid") = .Rows(i).Item(2)
                            drow.Item("docuser") = .Rows(i).Item(3).ToString().Trim()
                            drow.Item("doccont") = .Rows(i).Item(1).ToString().Trim()
                            If IsDBNull(.Rows(i).Item(4)) = False Then
                                drow.Item("docdate") = Format(.Rows(i).Item(4), "yy-MM-dd")
                            Else
                                drow.Item("docdate") = ""
                            End If
                            drow.Item("docdept") = .Rows(i).Item(5).ToString().Trim()

                            drow.Item("doctaken") = txt.Substring(1)
                            If cxt.Length > 0 Then
                                drow.Item("doccc") = cxt.Substring(1)
                            Else
                                drow.Item("doccc") = ""
                            End If
                            If con.Length > 0 Then
                                drow.Item("doccontent") = con
                            Else
                                drow.Item("doccontent") = ""
                            End If
                            dtl.Rows.Add(drow)
                        Else
                            If CheckBox1.Checked = False Or Session("uRole") = 1 Then
                                drow = dtl.NewRow()
                                drow.Item("mstrid") = .Rows(i).Item(0)
                                drow.Item("userid") = .Rows(i).Item(2)
                                drow.Item("docuser") = .Rows(i).Item(3).ToString().Trim()
                                drow.Item("doccont") = .Rows(i).Item(1).ToString().Trim()
                                If IsDBNull(.Rows(i).Item(4)) = False Then
                                    drow.Item("docdate") = Format(.Rows(i).Item(4), "yy-MM-dd")
                                Else
                                    drow.Item("docdate") = ""
                                End If
                                drow.Item("docdept") = .Rows(i).Item(5).ToString().Trim()

                                drow.Item("doctaken") = ""
                                If cxt.Length > 0 Then
                                    drow.Item("doccc") = cxt.Substring(1)
                                Else
                                    drow.Item("doccc") = ""
                                End If
                                If con.Length > 0 Then
                                    drow.Item("doccontent") = con
                                Else
                                    drow.Item("doccontent") = ""
                                End If
                                dtl.Rows.Add(drow)

                            End If
                        End If
                    Next

                End If
            End With
            ds.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()


        End Sub
        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("docview") = 0 Then
                ltlAlert.Text = "window.open('conn_detail_list.aspx?mid=" & e.Item.Cells(0).Text.Trim() & "','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') "
            ElseIf e.CommandName.CompareTo("doctake") = 0 Then
                Response.Redirect("conn_app3.aspx?mid=" & e.Item.Cells(0).Text.Trim())
            ElseIf e.CommandName.CompareTo("docdel") = 0 Then
                If e.Item.Cells(1).Text.Trim() <> Session("uID") Then
                    ltlAlert.Text = "alert('你不能删除他人申请的内部业务联系单！')"
                    Exit Sub
                End If

                StrSql = "select top 1 conn_detail_id from knowdb.dbo.conn_detail where conn_mstr_id='" & e.Item.Cells(0).Text.Trim() & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                    ltlAlert.Text = "alert('你不能删除已有会签的内部业务联系单！')"
                    Exit Sub
                End If

                StrSql = "update knowdb.dbo.conn_mstr set conn_deleteddate=getdate()  where conn_user_id='" & Session("uID") & "' and conn_closeddate is null and conn_deleteddate is null and conn_mstr_id = '" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)
                BindData()

            End If
        End Sub
        Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
            BindData()
        End Sub

        Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
            Response.Redirect("conn_app2.aspx")
        End Sub

        Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            BindData()
        End Sub
    End Class

End Namespace













