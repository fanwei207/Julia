'!*******************************************************************************!
'* @@ NAME				:	CompareQADTAndP.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for CompareQADTAndP.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	12/24/2008
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System.Drawing
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc

    Partial Class CompareQADTAndP
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents ltlAlert As Literal

        Dim chk As New adamClass
        Dim strSql As String
        Dim reader As SqlDataReader
        'Dim ahashtable As New Hashtable
        'Dim bhashtable As New Hashtable
        'Dim chashtable As New Hashtable

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then

                Dim wdate As String = ""
                Dim pdate As String = ""

                Try ' Update the Z_QAD_wo from Servers 10.3.0.75, get the time display
                    strSql = " select top 1 createddate from Z_QAD_wo "
                    wdate = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql)
                    If wdate.Trim.Length > 0 Then
                        wdatetime.Text = "<font color=#cc0033>QAD加工单数据更新于" & wdate & "</font>"
                    End If
                Catch ex As Exception
                    ltlAlert.Text = "alert('服务器数据更新，请稍后再试');"
                    Return
                End Try

                Try 'Update the Z_QAD_wo from Servers 10.3.0.75, get the time display
                    strSql = " select top 1 createddate from Z_QAD_po "
                    pdate = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql)
                    If pdate.Trim.Length > 0 Then
                        pdatetime.Text = "<font color=#cc0033>QAD采购数据更新于" & pdate & "</font>"
                    End If
                Catch ex As Exception
                    ltlAlert.Text = "alert('服务器数据更新，请稍后再试');"
                    Return
                End Try

                SaleBind(0)   '/load the datagride data
            End If
        End Sub


        Sub searchRecord(ByVal sender As Object, ByVal e As System.EventArgs)
            SaleBind(1)
            DataGrid1.CurrentPageIndex = 0
        End Sub

        Sub SaleBind(ByVal temp As Integer)
            strSql = " select bd.bm_detail_id,b.bm_mstr_id,bd.parent,bd.old_child,bd.new_child,b.bm_desc,bd.unit_qty,bd.rej_rate,isnull(sh_stock_qty_init,0)+isnull(zj_stock_qty_init,0)+isnull(yz_stock_qty_init,0) as sh,"
            strSql &= " isnull(yz_other_qty_init,0)+isnull(sh_other_qty_init,0)+isnull(zj_other_qty_init,0) as zj,isnull(sh_plan_qty_init,0)+isnull(yz_plan_qty_init,0)+isnull(zj_plan_qty_init,0) as yz,b.bm_createdname  "
            strSql &= " ,isnull(p.poqty,0),isnull(w.woqty,0)"
            strSql &= " from bm_mstr b "
            strSql &= " inner join bm_detail  bd on bd.bm_mstr_id = b.bm_mstr_id "

            strSql &= " left outer join  (select sum(pod_qty_ord-pod_qty_rcvd) as poqty,part from Z_QAD_po group by part) p on p.part=bd.old_child "
            strSql &= " left outer join (select part,sum(wod_qty_req - wod_qty_iss) as woqty from Z_QAD_wo group by part) w on w.part=bd.old_child"
            'strSql &= " ( select case when z.part is null then w.part else z.part end as part, z.pdomain,z.poqty,w.wdomain,w.woqty From"
            'strSql &= "  (select sum(pod_qty_ord-pod_qty_rcvd) as poqty,part,pdomain  from Z_QAD_po group by pdomain,part) z  "
            'strSql &= " full outer join (select part,wdomain,sum(wod_qty_req - wod_qty_iss) as woqty from Z_QAD_wo group by wdomain,part)w on w.part=z.part and z.pdomain = w.wdomain ) pw on pw.part =bd.old_child"

            strSql &= " where b.bm_deletedName is null "
            If temp > 0 Then
                If dealID.Text.Trim.Length > 0 Then
                    strSql &= " and b.bm_mstr_id = '" & dealID.Text.Trim & "' "
                End If

                If parents.Text.Trim.Length > 0 Then
                    strSql &= " and lower(bd.parent) like N'" & parents.Text.Trim.ToLower & "%' "
                End If

                If son.Text.Trim.Length > 0 Then
                    strSql &= " and lower(bd.old_child) like N'" & son.Text.Trim.ToLower & "%' "
                End If

                If replaceson.Text.Trim.Length > 0 Then
                    strSql &= " and lower(bd.new_child) like N'" & replaceson.Text.Trim.ToLower & "%' "
                End If

            End If
            'strSql &= " and lower(bd.old_child) <>'noqad' and bd.parent<>'11011827000410' " '
            strSql &= " order by bd.old_child,b.bm_mstr_id,bd.parent "

            'Response.Write(strSql)
            'Exit Sub
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)

            Dim dt As New DataTable
            Dim i As Integer = 0
            Dim dr1 As DataRow
            '/----------------------custom define the table cell flag -----------------------/
            dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("processID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("parent", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("son", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("rson", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("reason", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("require", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("rat", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("sqty", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("zqty", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("yqty", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("qadsqty", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("qadzqty", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("qadyqty", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("createby", System.Type.GetType("System.String")))
            '/--------------------------------------------------------------------------------/

            Dim bpart As String = ""
            'Dim bid As String = ""
            'Dim flag As Boolean = False
            Dim inv As Decimal = 0
            'Dim zql As Decimal = 0
            'Dim yql As Decimal = 0
            While reader.Read

                dr1 = dt.NewRow()
                'bid = reader(0)

                dr1.Item("id") = reader(0)
                dr1.Item("processID") = reader(1)
                dr1.Item("parent") = reader(2)
                dr1.Item("son") = reader(3)
                dr1.Item("rson") = reader(4)
                dr1.Item("reason") = reader(5)
                dr1.Item("require") = reader(6)
                dr1.Item("rat") = reader(7)
                dr1.Item("sqty") = reader(8)
                dr1.Item("zqty") = reader(9)
                dr1.Item("yqty") = reader(10)
                dr1.Item("createby") = reader(11)

                'End If

                If bpart <> reader(3) Then
                    bpart = reader(3)
                    inv = getdata(reader(3))
                    If inv = -1 Then
                        ltlAlert.Text = "alert('连接QAD服务器失败，请重试');"
                        Exit Sub
                    End If
                End If

                dr1.Item("qadsqty") = inv
                dr1.Item("qadzqty") = reader(13)
                dr1.Item("qadyqty") = reader(12)

                dt.Rows.Add(dr1)

            End While
            reader.Close()

            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            SaleBind(1)
        End Sub

        Public Sub datagrid1_DeleteCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If e.CommandName.CompareTo("Detail") = 0 Then
                ltlAlert.Text = "var w=window.open('/QAD/CompareQADDetail.aspx?id=" & e.Item.Cells(0).Text.Trim() & "&inv=" & e.Item.Cells(8).Text.Trim() & "&wo=" & e.Item.Cells(9).Text.Trim() & "&po=" & e.Item.Cells(10).Text.Trim() & "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();"
            End If

            If e.CommandName.CompareTo("delete") = 0 Then
                Dim nRet As Integer
                '/-------------------------judge whether the modul belong to user-------------------------------/
                If Not Me.Security("100103082").isValid Then
                    Response.Redirect("/public/Message.aspx?type=" & nRet.ToString(), True)
                End If
                '/----------------------------------------------------------------------------------------------/
                strSql = " Update bm_detail set sh_plan_qty_init =0 , sh_stock_qty_init=0, sh_other_qty_init=0, zj_plan_qty_init=0, "
                strSql &= " zj_stock_qty_init=0, zj_other_qty_init=0, yz_plan_qty_init=0, yz_stock_qty_init=0,"
                strSql &= " yz_other_qty_init=0, isfinish=0 where bm_detail_id = '" & e.Item.Cells(0).Text & "' "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)

                SaleBind(1)
            End If
        End Sub

        Function getdata(ByVal part As String) As Decimal
            If Not IsNumeric(part) Then  'the part value is 'NoQad'
                Return 0
            End If

            Dim query As String
            Dim total As Decimal = 0


            Try
                'query = "select isnull(qty,0) from  "
                'query &= " OPENROWSET('MSDASQL','DSN=mfgtcp;uid=zxdz;pwd=zxdz;HOST=10.3.0.75;port=60055;DB=mfgtcp;',"
                'query &= " 'select cast(sum(ld_qty_oh) as decimal(18,6)) as qty from pub.ld_det where ld_qty_oh <> 0 and  ld_part=''" & part & "'' and ld_status <> ''SHIPED'' ')  "
                query = " select isnull(cast(sum(ld_qty_oh) as decimal(18,6)),0) as qty from qad_data.qad_data.dbo.ld_det where ld_qty_oh <> 0 and  ld_part='" & part & "' and ld_status <> 'SHIPED'  "
                total = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, query)
                'While Qadreader.Read
                '    Response.Write(reader(0).ToString() & "<br>")
                'End While
                'Qadreader.Close()
            Catch ex As Exception
                'Response.Write(ex.ToString & " <br>")
                'Response.Write(query)
                Return -1
            End Try

            Return total
        End Function
    End Class

End Namespace
