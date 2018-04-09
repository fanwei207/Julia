Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc

Partial Class wo_cost_wo_workproEnter
    Inherits BasePage
    Public chk As New adamClass
    Dim strSql As String


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""
        Dim nRet As Integer
        If Not IsPostBack Then
            BindData()
        End If
    End Sub


    Sub BindData()
        Dim reader As SqlDataReader
        Dim Query As String
        '// Get the search sql sentance

        strSql = "sp_WO_WorkprocedureSelect"
        Dim params(4) As SqlParameter
        params(0) = New SqlParameter("@creat", CInt(Session("uID")))
        params(1) = New SqlParameter("@woTec", txtWpro.Text.Trim())
        params(2) = New SqlParameter("@woProName", txtProName.Text.Trim())
        params(3) = New SqlParameter("@woGl", txtGline.Text.Trim())
        params(4) = New SqlParameter("@plantCode", Session("PlantCode"))
        Query = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, strSql, params).ToString()
        '// Query = sql sentance for workprocedure

        Dim total As Integer = 0

        Session("EXTitle1") = "<b>工艺代码</b>~^150^<b>工序名称</b>~^<b>工时定额</b>~^<b>创建人</b>~^200^<b>工艺描述</b>~^200^<b>零件描述</b>~^<b>工号</b>~^<b>日期</b>~^"
        Session("EXSQL1") = Query

        '//get workproce data
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, Query)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("woID", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("woTec", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("woProc", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("woGl", System.Type.GetType("System.Decimal")))
        dt.Columns.Add(New DataColumn("woCreat", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("woPdesc", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("woRdesc", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("woDate", System.Type.GetType("System.String")))
        Dim dr As DataRow
        While reader.Read
            dr = dt.NewRow()
            dr.Item("woID") = reader(0)
            dr.Item("woTec") = reader(1)
            dr.Item("woProc") = reader(2)
            dr.Item("woGl") = reader(3)
            dr.Item("woCreat") = reader(4)
            dr.Item("woPdesc") = reader(6)
            dr.Item("woRdesc") = reader(5)
            dr.Item("userID") = reader(7)
            dr.Item("woDate") = Format(reader(8), "yyMMdd")
            dt.Rows.Add(dr)
            total = total + 1
        End While
        reader.Close()

        Label1.Text = total.ToString()
        Dim dv As DataView
        dv = New DataView(dt)

        Try
            'dv.Sort = Session("orderby") & Session("orderdir")
            DataGrid1.DataSource = dv
            DataGrid1.DataBind()
        Catch
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        DataGrid1.CurrentPageIndex = 0
        BindData()
    End Sub

    Private Sub dgreturnDetail_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(8)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

            Case ListItemType.AlternatingItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(8)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

            Case ListItemType.EditItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(8)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")
                CType(e.Item.Cells(3).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(120)

        End Select
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If e.CommandName.CompareTo("DeleteBtn") = 0 Then
            If e.Item.Cells(9).Text.Trim <> Session("uid").ToString() And Session("uRole") <> 1 Then
                ltlAlert.Text = "alert('没有删除此数据的权限！');"
                Exit Sub
            Else
                strSql = "UPDATE  wo_Tec SET wo_del=1,wo_modified='" & Session("uID") & "',wo_modifydate=getdate()  WHERE id='" & e.Item.Cells(0).Text & "'"
                'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
                BindData()
            End If


        End If
    End Sub

    Public Sub Edit_edit(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand

        If e.Item.Cells(9).Text.Trim <> Session("uid").ToString() And Session("uRole") <> 1 Then
            ltlAlert.Text = "alert('没有编辑此数据的权限！');"
            Exit Sub
        Else
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            BindData()
        End If
    End Sub

    Public Sub Edit_cancel(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        DataGrid1.EditItemIndex = -1
        BindData()
    End Sub

    Public Sub Edit_update(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        Dim str1 As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text
        If str1.Length = 0 Then
            str1 = "0"
        Else
            If Not IsNumeric(str1) Then
                ltlAlert.Text = "alert('必须输入数字型！');"
                Exit Sub
            End If
        End If
        strSql = "UPDATE wo_Tec SET wo_gl = '" & str1 & "', wo_modified='" & Session("uID") & "',wo_modifydate=getdate()  WHERE id='" & e.Item.Cells(0).Text & "'"
        'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
        DataGrid1.EditItemIndex = -1
        BindData()
    End Sub

    Function IsNumeric(ByVal val As String) As Boolean
        Try
            Dim d As Double
            d = Convert.ToDouble(val)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim wflag, pflag, rflag As Boolean
        Dim strInt As String
        strInt = "^[0-9]+$"
        If txtTec.Text.Trim.Length = 0 Then
            ltlAlert.Text = "alert('工艺流程不能为空！');"
            Exit Sub
        End If
        If txtWpr.Text.Trim.Length = 0 Then
            ltlAlert.Text = "alert('工序名称不能为空！');"
            Exit Sub
        End If
        If txtWGl.Text.Trim.Length = 0 Then
            ltlAlert.Text = "alert('指标不能为空！');"
            Exit Sub
        ElseIf IsNumeric(txtWGl.Text.Trim()) = False Then 'System.Text.RegularExpressions.Regex.IsMatch(txtWGl.Text.Trim(), strInt, System.Text.RegularExpressions.RegexOptions.Multiline)
            ltlAlert.Text = "alert('指标只能为数字！');"
            Exit Sub
        End If


        '//judge whether the wo_tec exist
        strSql = "IF NOT EXISTS (SELECT * FROM wo_Tec WHERE wo_tec='" & txtTec.Text.Trim() & "' AND wo_del =0 ) "
        strSql &= "  SELECT 0 ELSE SELECT 1 "
        'wflag = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
        wflag = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql)

        If wflag = False Then
            strSql = "IF NOT EXISTS (SELECT * FROM QAD_DATA..ro_det WHERE ro_routing='" & txtTec.Text.Trim() & "' ) "
            strSql &= "  SELECT 0 ELSE SELECT 1 "
            rflag = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql)

            If rflag = False Then
                If getTec(txtTec.Text.Trim()) = False Then
                    ltlAlert.Text = "alert('输入工艺流程代码不存在，请重新输入');form1.txtTec.focus();"
                    Exit Sub
                End If
            End If
        End If
        '// end judge value for wo_tec

        strSql = "IF NOT EXISTS (SELECT * FROM wo_Tec WHERE wo_tec='" & txtTec.Text.Trim() & "' AND wo_procName =N'" & chk.sqlEncode(txtWpr.Text.Trim()) & "' AND wo_del =0) "
        strSql &= "  SELECT 1 ELSE SELECT 0 "
        'pflag = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
        pflag = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql)

        If pflag Then
            strSql = " INSERT INTO wo_Tec(wo_tec, wo_procName, wo_gl, wo_creat, wo_createddate, wo_del, wo_tecHours,wo_pdesc,wo_rdesc )"
            If wflag Then
                strSql &= " SELECT TOP 1 wo_Tec,N'" & chk.sqlEncode(txtWpr.Text.Trim()) & "','" & txtWGl.Text & "','" & Session("uID") & "',getdate(),0,wo_tecHours,wo_pdesc,wo_rdesc "
                strSql &= " FROM wo_Tec WHERE wo_tec='" & txtTec.Text.Trim() & "' AND wo_del =0 "
            Else
                strSql &= " VALUES ('" & txtTec.Text.Trim() & "',N'" & chk.sqlEncode(txtWpr.Text.Trim()) & "','" & txtWGl.Text & "','" & Session("uID") & "',getdate(),0,'" & lbldesc.Text.Substring(0, lbldesc.Text.IndexOf(",")) & "',N'" & chk.sqlEncode(lbldesc.Text.Substring(lbldesc.Text.IndexOf(",") + 1)) & "',N'" & lblRdesc.Text & "') "
            End If

            'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)

            txtWpr.Text = ""
            txtWGl.Text = ""
            lbldesc.Text = ""
        Else
            ltlAlert.Text = "alert('输入工工序名称已存在，请重新输入');form1.txtWpr.focus();"
            Exit Sub
        End If

    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        txtTec.Text = ""
        txtWpr.Text = ""
        txtWGl.Text = ""
        BindData()
        DataGrid1.Visible = True
        tbInput.Visible = False
        tbTop.Visible = True
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        DataGrid1.Visible = False
        tbInput.Visible = True
        tbTop.Visible = False
        txtTec.Text = ""
        txtWpr.Text = ""
        txtWGl.Text = ""
    End Sub

    Function getTec(ByVal str As String) As Boolean
        Dim getTecFlag As Boolean = False
        Dim reader As SqlDataReader

        Dim conn As OdbcConnection = Nothing
        Dim comm As OdbcCommand = Nothing
        Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
        'Dim strdomain As String
        strSql = "SELECT realdomain FROM Domain_Mes WHERE plantCode='" & Session("Plantcode") & "'  ORDER BY Mainsite DESC"
        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
        While reader.Read

            If getTecFlag = False Then
                Dim odr As OdbcDataReader

                'strSql = "SELECT ro_routing,ro_run,ro_desc FROM PUB.ro_det WHERE ro_routing ='" & str & "' AND ro_domain='" & reader(0) & "' "
                strSql = "SELECT ro_routing,ro_run,ro_desc FROM PUB.ro_det WHERE ro_routing ='" & str & "' "
                Try
                    conn = New OdbcConnection(connectionString)
                    conn.Open()
                    comm = New OdbcCommand(strSql, conn)
                    odr = comm.ExecuteReader()
                    If (odr.Read()) Then
                        If Not IsDBNull(odr.GetValue(0)) Then
                            getTecFlag = True
                            lbldesc.Text = odr.GetValue(1)
                            lblRdesc.Text = odr.GetValue(2)
                        End If
                    End If
                Catch oe As OdbcException
                    Response.Write(oe.Message)
                Finally
                    odr.Close()
                    If conn.State = System.Data.ConnectionState.Open Then
                        conn.Close()
                    End If
                End Try

                comm.Dispose()
                conn.Dispose()
            End If

            '// If find the routing of the part,get the description for it
            If getTecFlag Then
                Dim odr1 As OdbcDataReader
                'strSql = "SELECT pt_desc1,pt_desc2 FROM PUB.pt_mstr WHERE pt_domain='" & reader(0) & "' AND pt_part='" & str.Substring(0, 14) & "' "
                strSql = "SELECT pt_desc1,pt_desc2 FROM PUB.pt_mstr WHERE  pt_part='" & str.Substring(0, 14) & "' "
                Try
                    conn = New OdbcConnection(connectionString)
                    conn.Open()
                    comm = New OdbcCommand(strSql, conn)
                    odr1 = comm.ExecuteReader()
                    If (odr1.Read()) Then
                        lbldesc.Text = lbldesc.Text & "," & odr1.GetValue(0).ToString() & odr1.GetValue(1).ToString()
                        Exit While
                    End If

                Catch oe As OdbcException
                    Response.Write(oe.Message)
                Finally
                    odr1.Close()
                    If conn.State = System.Data.ConnectionState.Open Then
                        conn.Close()
                    End If
                End Try

                comm.Dispose()
                conn.Dispose()

            End If
        End While
        reader.Close()

        Return getTecFlag
    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim tl As Integer = 0
        tl = CInt(Label1.Text.Trim())

        If tl > 65000 Then
            ltlAlert.Text = "alert('导出工序定额条数不能大于65000');"
            Exit Sub
        End If
        ltlAlert.Text = "var w=window.open('wo_wp_export.aspx" & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); 	"

    End Sub
End Class
