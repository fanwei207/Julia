Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc
Imports System.Web.Mail

Namespace tcpc
    Partial Class soque_list
        Inherits System.Web.UI.Page
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim nRet As Integer
        Dim item As ListItem
        Dim reader As SqlDataReader


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
                nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 44000011)

                Dim ls As ListItem

                ls = New ListItem
                ls.Value = 0
                ls.Text = "--"
                dd_ws.Items.Add(ls)

                StrSql = "Select soques_id,soques_step from tcpc0.dbo.soque_step order by soques_id "
                reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, StrSql)
                Do While (reader.Read())
                    ls = New ListItem
                    ls.Value = reader(0)
                    ls.Text = reader(1).ToString()
                    dd_ws.Items.Add(ls)
                Loop
                reader.Close()

                dd_ws.SelectedIndex = 0

                BindData()
            End If
        End Sub
        Sub BindData()

            StrSql = "SELECT soque_status_id,soque_id,soque_nbr,soque_line,soque_cus_part,soque_cus,soque_qty_ord,soque_date_ord,soque_date_ship,soque_part,soque_status,soque_leader,soque_note,soque_degreename "
            StrSql &= " FROM tcpc0.dbo.soque_mstr "
            StrSql &= " WHERE soque_id>0 "
            If dd_ws.SelectedIndex > 0 Then
                StrSql &= " and soque_status_id='" & dd_ws.SelectedValue & "' "
            Else
                StrSql &= " and soque_status_id<>1000 "
            End If

            If txb_nbr.Text.Trim.Length > 0 Then
                StrSql &= " and soque_nbr like '%" & txb_nbr.Text & "%'"
            End If
            If txb_cust.Text.Trim.Length > 0 Then
                StrSql &= " and soque_cus like '%" & txb_cust.Text & "%'"
            End If
            If txb_part.Text.Trim.Length > 0 Then
                StrSql &= " and soque_part like '" & txb_part.Text & "%'"
            End If
            StrSql &= " ORDER BY  soque_date_ship "

            Session("EXTitle") = "70^<b>订单号</b>~^30^<b>序号</b>~^120^<b>产品名称</b>~^50^<b>客户</b>~^50^<b>数量</b>~^<b>订单日期</b>~^<b>出运日期</b>~^<b>QAD号</b>~^120^<b>状态</b>~^<b>责任人</b>~^200^<b>备注</b>~^<b>紧急状态</b>~^"
            Session("EXSQL") = StrSql

            'Response.Write(StrSql)
            'Exit Sub

            ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_nbr", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_line", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cpart", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_qty", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_cust", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_dateord", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_dateship", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_part", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_leader", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_note", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("wo_status_id", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("dstatus", System.Type.GetType("System.String")))



            Dim total As Integer = 0

            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("id") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("wo_nbr") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("wo_line") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("wo_cpart") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("wo_cust") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("wo_qty") = Format(.Rows(i).Item(6), "##0")
                        If Not IsDBNull(.Rows(i).Item(7)) And IsDate(.Rows(i).Item(7)) Then
                            drow.Item("wo_dateord") = Format(.Rows(i).Item(7), "yy-MM-dd")
                        Else
                            drow.Item("wo_dateord") = ""
                        End If
                        If Not IsDBNull(.Rows(i).Item(8)) And IsDate(.Rows(i).Item(8)) Then
                            drow.Item("wo_dateship") = Format(.Rows(i).Item(8), "yy-MM-dd")
                        Else
                            drow.Item("wo_dateship") = ""
                        End If
                        drow.Item("wo_part") = .Rows(i).Item(9).ToString().Trim()
                        drow.Item("wo_status") = .Rows(i).Item(10).ToString().Trim()
                        
                        drow.Item("wo_leader") = .Rows(i).Item(11).ToString().Trim()
                                        drow.Item("wo_note") = .Rows(i).Item(12).ToString().Trim()
                        drow.Item("wo_status_id") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("dstatus") = .Rows(i).Item("soque_degreename").ToString().Trim()

                        dtl.Rows.Add(drow)
                        total = total + 1
                    Next
                End If
            End With
            ds.Reset()

            Label1.Text = "数量：" & total.ToString()

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()


        End Sub

        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
            If e.CommandName.CompareTo("wo_detail") = 0 Then
                Dim str As String = ""
                str = "soque_edit.aspx?id=" & e.Item.Cells(0).Text
                Response.Redirect(str)
            End If
        End Sub
        Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
            Datagrid1.CurrentPageIndex = 0
            BindData()
        End Sub
        Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
            ltlAlert.Text = "var w=window.open('/public/exportexcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 			"

        End Sub

        Protected Sub Datagrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
            Select Case e.Item.ItemType
                Case ListItemType.Item

                Case ListItemType.AlternatingItem

            End Select
        End Sub


        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub dd_ws_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_ws.SelectedIndexChanged
            Datagrid1.CurrentPageIndex = 0
            BindData()
        End Sub

        Protected Sub btn_edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_edit.Click
            Dim str As String = ""
            str = "soque_edit.aspx?id=0&rm=" & DateTime.Now.ToString()
            Response.Redirect(str)
        End Sub

        'Protected Sub btn_email_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_email.Click

        '    Dim mailfrom As String
        '    Dim mailto As String
        '    Dim mailcc As String
        '    Dim i As Integer
        '    Dim mail As MailMessage = New MailMessage
        '    Dim mailstr() As String = Split(txb_chooseid.Text.Trim, ",")

        '    For i = 0 To mailstr.Length - 2
        '        If mailstr(i).Trim.Length > 0 Then
        '            StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & CInt(mailstr(i).Trim()) & " and email <>''"
        '            If IsDBNull(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)) Or SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Nothing Then

        '                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf("@") > 1 And SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf(".com") > 1 Then
        '                    mailto = mailto + SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.Trim + ";"
        '                End If

        '            End If
        '        End If

        '    Next

        '    Dim mailstr2() As String = Split(txb_ccid.Text.Trim, ",")

        '    For i = 0 To mailstr2.Length - 2
        '        If mailstr2(i).Trim.Length > 0 Then
        '            StrSql = "SELECT email From tcpc0.dbo.users Where  deleted=0 and isactive=1 and leavedate is null and userid=" & CInt(mailstr2(i).Trim()) & " and email <>''"
        '            If IsDBNull(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)) Or SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <> Nothing Then
        '                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf("@") > 1 And SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.IndexOf(".com") > 1 Then
        '                    mailcc = mailcc + SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql).ToString.Trim + ";"
        '                End If
        '            End If
        '        End If

        '    Next

        '    If mailto <> Nothing Then
        '        If mailto.Trim.Length > 0 And mailto.Trim.IndexOf("@") > 1 Then
        '            mail.To = mailto
        '            mail.Cc = mailcc
        '            mail.From = mailfrom
        '            mail.Subject = "客户订单输入问题汇总"
        '            mail.Body = "状态:" + dd_ws.SelectedItem.Text + "&nbsp;&nbsp;" + "客户：" + Me.txb_cust.Text + "紧急状态:" + Me.Ddl_degree.SelectedItem.Text
        '            mail.Body &= "<br />"
        '            mail.Body &= "订单号:" + txb_nbr.Text + "&nbsp;&nbsp;" + "行:" + txb_line.Text + "&nbsp;&nbsp;" + "产品名称:" + txb_cpart.Text + "&nbsp;&nbsp;" + "QAD:" + Me.txb_part.Text
        '            mail.Body &= "<br />"
        '            mail.Body &= "备注:" + txb_sug.Text
        '            mail.BodyFormat = MailFormat.Html

        '            SmtpMail.SmtpServer = ConfigurationManager.AppSettings("mailServer")
        '            SmtpMail.Send(mail)
        '        End If
        '    End If


        'End Sub

    End Class
End Namespace