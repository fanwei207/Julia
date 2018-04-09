Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc

Partial Class wo_cost_wo_tmpUser
    Inherits BasePage
    Dim chk As New adamClass
    Dim strSQL As String
    Dim reader As SqlDataReader


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'ltlAlert.Text = ""
        If Not IsPostBack Then
            dropCenterDatabind()
            txtDate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Today)

            BindData()
        End If

    End Sub


    Private Sub dropCenterDatabind()
        Dim strdomain As String
        Dim item As ListItem
        item = New ListItem("--")
        item.Value = 0
        dropCenter.Items.Add(item)

        strSQL = " SELECT qad_domain FROM Domain_Mes WHERE plantCode='" & Session("Plantcode") & "' GROUP BY qad_domain"
        strdomain = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)

        Dim conn As OdbcConnection = Nothing
        Dim comm As OdbcCommand = Nothing
        Dim dr As OdbcDataReader
        'Dim i As Integer = 0

        Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")

        strSQL = "SELECT cc_ctr, cc_desc FROM PUB.cc_mstr WHERE cc_active=1 AND cc_domain= '" & strdomain & "'and cc_ctr<>'' ORDER BY cc_ctr "
        Try
            conn = New OdbcConnection(connectionString)
            conn.Open()
            comm = New OdbcCommand(strSQL, conn)
            dr = comm.ExecuteReader()
            While (dr.Read())
                item = New ListItem(dr.GetValue(1).ToString() & "-" & dr.GetValue(0).ToString())
                item.Value = dr.GetValue(0).ToString()
                dropCenter.Items.Add(item)
            End While
            dr.Close()
        Catch oe As OdbcException
            'Response.Write(oe.Message)
        Finally
            If conn.State = System.Data.ConnectionState.Open Then
                conn.Close()
            End If
            If Not comm Is Nothing Then
                comm.Dispose()
            End If
            If Not conn Is Nothing Then
                conn.Dispose()
            End If
        End Try
    End Sub


    Private Sub BindData()

        strSQL = " SELECT wo_tmpID,wo_tmpcc,wo_tmpDate,wo_tmpUserA,wo_tmpUserB, ISNULL(wo_tmpUserA,0) + ISNULL(wo_tmpUserB,0),ISNULL(wo_relationcc,'') "
        strSQL &= " FROM wo_tmpUser "
        strSQL &= " WHERE ISNULL(deletedBy,0)=0 "
        If chkall.Checked = False Then
            strSQL &= " AND wo_tmpDate ='" & txtDate.Text & "' "
        End If
        strSQL &= "  ORDER BY wo_tmpDate DESC "

        Session("EXTitle") = "<b>成本中心</b>~^<b>日期</b>~^<b>生产人数(A)</b>~^<b>辅助人员(B)</b>~^<b>人员总数</b>~^"
        Session("EXSQL") = strSQL

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("Center", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("Userdate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("WorkerA", System.Type.GetType("System.Decimal")))
        dt.Columns.Add(New DataColumn("WorkerB", System.Type.GetType("System.Decimal")))
        dt.Columns.Add(New DataColumn("RelCenter", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.Int32")))

        Dim dr1 As DataRow
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)

        While reader.Read
            dr1 = dt.NewRow()
            dr1.Item("Center") = reader(1).ToString().Trim()
            dr1.Item("Userdate") = reader(2).ToShortDateString()
            dr1.Item("WorkerA") = reader(3).ToString().Trim()
            dr1.Item("WorkerB") = reader(4)
            dr1.Item("RelCenter") = reader(6).ToString().Trim()
            dr1.Item("ID") = reader(0)
            dt.Rows.Add(dr1)
        End While
        reader.Close()

        Dim dv As DataView
        dv = New DataView(dt)
        Try
            Datagrid1.DataSource = dv
            Datagrid1.DataBind()
        Catch
        End Try
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Datagrid1.CurrentPageIndex = 0
        BindData()
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
        Datagrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub dgreturnDetail_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemCreated
        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(6)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

            Case ListItemType.AlternatingItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(6)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

            Case ListItemType.EditItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(6)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")
        End Select
    End Sub

    Public Sub Edit_delete(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.DeleteCommand

        strSQL = " UPDATE wo_tmpUser SET deletedBy ='" & Session("Uid") & "',deletedDate=getdate() WHERE wo_tmpID='" & e.Item.Cells(0).Text.Trim() & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
        BindData()
    End Sub



    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim strInt As String
        strInt = "^[0-9]+$"

        If dropCenter.SelectedIndex = 0 Then
            ltlAlert.Text = "alert('必须选择成本中心!');"
            Exit Sub
        End If
        If txtDate.Text.Trim.Length = 0 Then
            ltlAlert.Text = "alert('日期不能为空!');"
            Exit Sub
        End If
        If txtDate.Text.Trim.Length <> 0 Then
            Try
                Dim dt As DateTime
                dt = Convert.ToDateTime(txtDate.Text.Trim())
            Catch ex As Exception
                ltlAlert.Text = "alert('日期格式不正确');"
                Exit Sub
            End Try
        End If

        If txtNumberA.Text.Trim.Length = 0 Then
            ltlAlert.Text = "alert('生产人员A人数不能为空！');"
            Exit Sub
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(txtNumberA.Text.Trim(), strInt, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
            ltlAlert.Text = "alert('生产人员A人数只能为数字！');"
            Exit Sub
        End If
        If txtNumberB.Text.Trim.Length = 0 Then
            ltlAlert.Text = "alert('辅助人员B人数不能为空！');"
            Exit Sub
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(txtNumberB.Text.Trim(), strInt, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
            ltlAlert.Text = "alert('辅助人员B人数只能为数字！');"
            Exit Sub
        End If

        If txtCenter.Text.Trim.Length > 0 Then
            Dim i As Integer
            Dim flag As Boolean = False

            If txtCenter.Text.Trim() <> dropCenter.SelectedValue.ToString() Then
                For i = 0 To dropCenter.Items.Count - 1
                    If txtCenter.Text.Trim = dropCenter.Items(i).Value Then
                        flag = True
                    End If
                Next
            End If

            If flag = False Then
                ltlAlert.Text = "alert('输入关联成本中心不存在或不能与成本中心一致!');Form1.txtCenter.focus();"
                Exit Sub
            End If
        End If

        Dim intWid As Integer = 0
        strSQL = "SELECT ISNULL(wo_tmpID,0) FROM wo_tmpUser WHERE wo_tmpDate ='" & txtDate.Text & "' AND ISNULL(deletedBy,0)=0 AND wo_tmpcc = '" & dropCenter.SelectedValue & "' "
        intWid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)

        If intWid > 0 Then
            ltlAlert.Text = "alert('输入日期的记录已存在!');"
            Exit Sub
        Else
            Try
                strSQL = " INSERT INTO wo_tmpUser (wo_tmpDate,wo_tmpUserA,wo_tmpUserB,wo_tmpcc,CreatedBy,CreatedDate"
                If txtCenter.Text.Trim.Length > 0 Then
                    strSQL &= " ,wo_relationcc "
                End If
                strSQL &= " ) VALUES "
                strSQL &= " ('" & txtDate.Text & "','" & txtNumberA.Text & "','" & txtNumberB.Text & "','" & dropCenter.SelectedValue & "','" & Session("Uid") & "',getdate()"
                If txtCenter.Text.Trim.Length > 0 Then
                    strSQL &= " ,'" & txtCenter.Text.Trim & "' "
                End If
                strSQL &= " )"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
            Catch ex As Exception
                ltlAlert.Text = "alert('保存出错,请重新操作!');"
                Exit Sub
            End Try
        End If

        ltlAlert.Text = "alert('保存成功!');"
        dropCenter.SelectedIndex = -1
        txtNumberA.Text = ""
        txtNumberB.Text = ""
        txtCenter.Text = ""


        Datagrid1.CurrentPageIndex = 0
        BindData()
    End Sub
End Class
