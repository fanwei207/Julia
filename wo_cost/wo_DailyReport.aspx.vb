
Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Partial Class wo_cost_wo_DailyReport
    Inherits BasePage
    Dim chk As New adamClass
    Dim strSQL As String
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
        If Not IsPostBack Then

            dropCenterDatabind()
            txtDate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now)

            BindData()
        End If

    End Sub

    Private Sub dropCenterDatabind()
        Dim strdomain As String
        Dim item As ListItem


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
        Dim strdomain As String
        strSQL = " SELECT qad_domain FROM Domain_Mes WHERE plantCode='" & Session("Plantcode") & "' GROUP BY qad_domain"
        strdomain = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)


        Dim i As Integer
        Dim flag As Boolean = False
        Dim str, strCenter As String
        strCenter = ""
        If txtCenter.Text.Trim.Length > 0 Then
            str = txtCenter.Text.Trim()
            While str.Length > 0
                i = str.IndexOf(",")
                If i <> -1 Then
                    If flag = False Then
                        strCenter = " AND ( "
                    Else
                        strCenter &= " OR "
                    End If
                    If CheckCenter(str.Substring(0, i)) = False Then
                        ltlAlert.Text = "alert( '" & str.Substring(0, i) & " 成本中心不存在');Form1.txtCenter.focus();"
                        Exit Sub
                    End If
                    strCenter &= " wocd_cc = '" & str.Substring(0, i) & "'"
                    str = str.Substring(i + 1)
                Else
                    If CheckCenter(str) = False Then
                        ltlAlert.Text = "alert( '" & str & " 成本中心不存在');Form1.txtCenter.focus();"
                        Exit Sub
                    End If

                    If flag = False Then
                        strCenter = " AND wocd_cc = '" & str & "'"
                    Else
                        strCenter &= " OR wocd_cc = '" & str & "' )"
                    End If
                    str = ""
                End If
                flag = True
            End While
        End If

        'Get the total users for everyday every user's Type
        Dim people(2) As Integer
        people(0) = 0
        people(1) = 0
        people(2) = 0

        Dim total(1) As Decimal
        total(0) = 0
        total(1) = 0

        Dim avg(1) As String
        avg(0) = "0%"
        avg(1) = "0%"


        strSQL = " SELECT SUM(Apeople),SUM(Bpeople),SUM(people) FROM "
        strSQL &= " (SELECT ISNULL(SUM(CASE WHEN p.userType=394 THEN 1 ELSE 0 END),0) AS Apeople,ISNULL(SUM(CASE WHEN p.userType=395 THEN 1 ELSE 0 END),0) AS Bpeople,ISNULL(COUNT(wocd_userid),0) AS People "
        strSQL &= " FROM (SELECT wocd_userid,u.userType FROM wo_cost_detail "
        strSQL &= " INNER JOIN tcpc0.dbo.Users u ON wocd_userid = u.userID "
        strSQL &= " WHERE wocd_date ='" & txtDate.Text.Trim() & "'  AND ISNULL(wocd_type,'') ='' "
        strSQL &= " AND LOWER(wocd_nbr) NOT LIKE N'%w%' AND SUBSTRING(LOWER(wocd_nbr),1,1) <> 'x' AND SUBSTRING(LOWER(wocd_nbr),1,2) <> 'zy' "
        strSQL &= " AND u.userNo <> '999A' AND u.userNo <> '999B' "

        If txtWonbr.Text.Trim().Length > 0 Then
            strSQL &= " AND wocd_nbr = '" & txtWonbr.Text.Trim() & "' "
        End If
        If txtWoid.Text.Trim().Length > 0 Then
            strSQL &= " AND wocd_id = '" & txtWoid.Text.Trim() & "' "
        End If
        If txtPart.Text.Trim().Length > 0 Then
            strSQL &= " AND wocd_part = '" & txtPart.Text.Trim() & "' "
        End If
        If strCenter.Trim.Length > 0 Then
            strSQL &= strCenter
        End If
        strSQL &= " GROUP BY wocd_userid,u.userType ) p "
        strSQL &= " UNION "
        strSQL &= " SELECT  SUM(ISNULL(t.wo_tmpUserA,0)) AS Apeople, SUM(ISNULL(t.wo_tmpUserB,0)) AS Bpeople, SUM(ISNULL(t.wo_tmpUserA,0)) + SUM(ISNULL(t.wo_tmpUserB,0)) AS People "
        strSQL &= " FROM (SELECT u.userNO,wocd_cc FROM wo_cost_detail "
        strSQL &= " INNER JOIN tcpc0.dbo.Users u ON wocd_userid = u.userID "
        strSQL &= " WHERE wocd_date ='" & txtDate.Text.Trim() & "'  AND ISNULL(wocd_type,'') = '' "
        strSQL &= " AND LOWER(wocd_nbr) NOT LIKE N'%w%' AND SUBSTRING(LOWER(wocd_nbr),1,1) <> 'x' AND SUBSTRING(LOWER(wocd_nbr),1,2) <> 'zy' "
        strSQL &= " AND (u.userNo ='999A' OR u.userNo='999B') "
        'If dropSite.SelectedValue > 0 Then
        '    strSQL &= " AND wocd_site = '" & dropSite.SelectedValue & "' "
        'End If
        If txtWonbr.Text.Trim().Length > 0 Then
            strSQL &= " AND wocd_nbr = '" & txtWonbr.Text.Trim() & "' "
        End If
        If txtWoid.Text.Trim().Length > 0 Then
            strSQL &= " AND wocd_id = '" & txtWoid.Text.Trim() & "' "
        End If
        If txtPart.Text.Trim().Length > 0 Then
            strSQL &= " AND wocd_part = '" & txtPart.Text.Trim() & "' "
        End If
        If strCenter.Trim.Length > 0 Then
            strSQL &= strCenter
        End If
        strSQL &= " GROUP BY u.userNO,wocd_cc) pt "
        strSQL &= " LEFT OUTER JOIN wo_tmpUser t  ON t.wo_tmpcc =pt.wocd_cc AND t.wo_tmpdate ='" & txtDate.Text.Trim() & "' AND ISNULL(t.deletedBy,0) = 0"
        strSQL &= " WHERE pt.wocd_cc NOT IN (SELECT wo_relationcc FROM wo_tmpUser WHERE wo_tmpdate ='" & txtDate.Text.Trim() & "' AND ISNULL(wo_relationcc,'') <> '') ) a "

        'Response.Write(strSQL)
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
        While reader.Read
            people(0) = reader(0)
            people(1) = reader(1)
            people(2) = reader(2)
        End While
        reader.Close()

        flag = False
        'strSQL = "SELECT  wocd_type,wocd_nbr, wocd_id, wocd_part,wocd_cc,l.cost,ROUND(l.cost/c.wo_price,5),'" & txtDate.Text & "' ,Apeople + Atmp ,Bpeople + Btmp,Total FROM "  ',Cpeople
        strSQL = "SELECT  wocd_type,wocd_nbr, wocd_id, wocd_part,wocd_cc,0,l.cost,'" & txtDate.Text & "' ,Apeople + Atmp ,Bpeople + Btmp,Total FROM "  ',Cpeople
        strSQL &= " (SELECT  wocd_type,wocd_nbr, wocd_id, wocd_part,wocd_cc,"   'SUM(ISNULL(w.qty,0)) AS qty,"
        strSQL &= " SUM(ISNULL(wocd_cost,0)) AS cost,SUM(CASE WHEN w.userType=394 THEN 1 ELSE 0 END) AS Apeople,SUM(CASE WHEN w.userType=395 THEN 1 ELSE 0 END) AS Bpeople, "
        strSQL &= " SUM(CASE WHEN w.userType=396 THEN 1 ELSE 0 END) AS Cpeople,Count(CASE WHEN w.userNo<>'999A' AND w.userNo<>'999B' THEN w.userNo END) AS Total, "
        strSQL &= "  SUM(CASE WHEN w.userNo='999A' THEN ISNULL(t.wo_tmpUserA,0) ELSE 0 END) AS Atmp, SUM(CASE WHEN w.userNo='999B' THEN ISNULL(t.wo_tmpUserB,0) ELSE 0 END) AS Btmp "
        strSQL &= "  FROM "
        strSQL &= " (SELECT  wocd_nbr, wocd_id, wocd_part,wocd_cc, u.userType,wocd_userid ,wocd_type,SUM(wocd_cost) AS wocd_cost,u.userNO "
        strSQL &= " FROM wo_cost_detail "
        strSQL &= " LEFT OUTER JOIN tcpc0.dbo.Users u ON wocd_userid = u.userID "

        strSQL &= " WHERE wocd_date ='" & txtDate.Text.Trim() & "' AND ISNULL(wocd_type,'') = ''  "
        strSQL &= " AND LOWER(wocd_nbr) NOT LIKE N'%w%' AND SUBSTRING(LOWER(wocd_nbr),1,1) <> 'x' AND SUBSTRING(LOWER(wocd_nbr),1,2) <> 'zy' "
        'If dropSite.SelectedValue > 0 Then
        '    strSQL &= " AND wocd_site = '" & dropSite.SelectedValue & "' "
        'End If
        If txtWonbr.Text.Trim().Length > 0 Then
            strSQL &= " AND wocd_nbr = '" & txtWonbr.Text.Trim() & "' "
        End If
        If txtWoid.Text.Trim().Length > 0 Then
            strSQL &= " AND wocd_id = '" & txtWoid.Text.Trim() & "' "
        End If
        If txtPart.Text.Trim().Length > 0 Then
            strSQL &= " AND wocd_part = '" & txtPart.Text.Trim() & "' "
        End If

        If strCenter.Trim.Length > 0 Then
            strSQL &= strCenter
        End If

        strSQL &= " GROUP BY wocd_nbr, wocd_id, wocd_part,wocd_cc,wocd_userid ,u.userType,wocd_type,u.userNo) w "
        strSQL &= " LEFT OUTER JOIN wo_tmpUser t  ON t.wo_tmpcc =w.wocd_cc AND t.wo_tmpdate ='" & txtDate.Text.Trim() & "' AND ISNULL(t.deletedBy,0) = 0 "
        'strSQL &= " INNER JOIN tcpc0.dbo.wo_CostCenter c ON c.wo_center = w.wocd_cc "
        strSQL &= " GROUP BY wocd_type,wocd_nbr, wocd_id, wocd_part,wocd_cc) l "
        strSQL &= " LEFT OUTER JOIN (SELECT wo_center,wo_price FROM tcpc0.dbo.wo_CostCenter WHERE deletedBy IS NULL AND YEAR(wo_date)='" & Year(txtDate.Text.Trim()) & "' AND MONTH(wo_date)='" & Month(txtDate.Text.Trim()) & "' ) "
        strSQL &= " AS c ON c.wo_center = l.wocd_cc  "

        strSQL &= " ORDER BY wocd_cc,wocd_nbr, wocd_id, wocd_part "



        'Response.Write(strSQL)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("site", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("wo_type", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("wo_nbr", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("wo_ID", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("wo_part", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("Costcenter", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("qty", System.Type.GetType("System.Decimal")))
        dt.Columns.Add(New DataColumn("wo_date", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("wo_cost", System.Type.GetType("System.Decimal")))
        dt.Columns.Add(New DataColumn("apeople", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("bpeople", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("cpeople", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("people", System.Type.GetType("System.Int32")))

        Dim dr1 As DataRow
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
        While reader.Read
            dr1 = dt.NewRow()
            dr1.Item("site") = ""
            dr1.Item("wo_type") = reader(0).ToString().Trim()
            dr1.Item("wo_nbr") = reader(1).ToString().Trim()
            dr1.Item("wo_ID") = reader(2)
            dr1.Item("wo_part") = reader(3).ToString().Trim()
            dr1.Item("Costcenter") = reader(4).ToString().Trim()
            dr1.Item("qty") = 0
            dr1.Item("wo_date") = reader(7)
            'dr1.Item("wo_cost") = reader(6)
            dr1.Item("wo_cost") = reader(6)
            dr1.Item("apeople") = reader(8)
            dr1.Item("bpeople") = reader(9)
            dr1.Item("cpeople") = "0"
            dr1.Item("people") = reader(10)

            total(0) = total(0) + 0
            total(1) = total(1) + reader(6)
            dt.Rows.Add(dr1)
        End While
        reader.Close()

        lblTotalqty.Text = total(0).ToString()
        lblTotalcost.Text = total(1).ToString()
        lblPeopleA.Text = people(0).ToString()
        lblPeopleB.Text = people(1).ToString()
        lblPeople.Text = people(2).ToString()

        If people(2) > 0 Then
            avg(0) = (Math.Round(people(0) / people(2), 4) * 100).ToString() & " %"
            avg(1) = (Math.Round(people(1) / people(2), 4) * 100).ToString() & " %"
        End If

        Session("EXHeader") = "<b>日期:</b>" & txtDate.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>域:</b>" & strdomain & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>成本中心:</b>" & txtCenter.Text.Trim & "^"
        Session("EXHeader") &= " <b>总工费:</b>" & total(0).ToString() & " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;        <b>总工时:</b>" & total(1).ToString() & "^"
        Session("EXHeader") &= " <b>生产人数(A):</b>" & people(0).ToString() & "  &nbsp;&nbsp;&nbsp;&nbsp;      <b>辅助人员(B):</b>" & people(1).ToString() & "  &nbsp;&nbsp;&nbsp;&nbsp;     <b>总人数:</b>" & people(2).ToString() & "^"
        Session("EXHeader") &= " <b>生产人数(A)占总人数的比例 :</b>" & avg(0) & "^"
        Session("EXHeader") &= " <b>辅助人员(B)占总人数的比例 :</b>" & avg(1)
        Session("EXTitle") = "<b>工单类型</b>~^<b>加工单号</b>~^<b>加工单ID</b>~^<b>零件号</b>~^<b>成本中心</b>~^<b>汇报总工费</b>~^<b>汇报总工时</b>~^<b>汇报日期</b>~^<b>生产人数(A)</b>~^<b>辅助人员(B)</b>~^<b>人员总数</b>~^"
        Session("EXSQL") = strSQL

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

    Function CheckCenter(ByVal str As String) As Boolean
        Dim i As Integer
        Dim flag As Boolean = False
        For i = 0 To dropCenter.Items.Count - 1
            If str = dropCenter.Items(i).Value.ToString() Then
                flag = True
            End If
        Next
        CheckCenter = flag
    End Function
End Class

