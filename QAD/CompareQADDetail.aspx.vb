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
'* @@ TEMPLATE DATE		:	12/25/2008
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

    Partial Class CompareQADDetail
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Dim chk As New adamClass
        Dim strSql As String
        Dim reader As SqlDataReader
        'Protected WithEvents ltlAlert As Literal
        Dim sitehashtableA As New Hashtable
        Dim sitehashtableB As New Hashtable
        Dim sitehashtableC As New Hashtable

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then
                '/----------------------------------------------------------------------------------------------/
                If Request("id") <> Nothing Then

                    strSql = " select b.parent,b.old_child,b.new_child,m.bm_comment,m.bm_desc from bm_detail b "
                    strSql &= " inner join bm_mstr m on m.bm_mstr_id=b.bm_mstr_id "
                    strSql &= " where b.bm_detail_id = '" & Request("id") & "' "
                    reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
                    While reader.Read
                        Aparent.Text = reader(0)
                        pson.Text = reader(1)
                        rpson.Text = reader(2)
                        comment.Text = reader(3)
                        reason.Text = reader(4)
                        If getdata(reader(1)) = False Then
                            ltlAlert.Text = "alert('服务器中断，请稍候再试');"
                            Exit Sub
                        End If

                    End While
                    reader.Close()

                    localinv.Text = Request("inv")
                    localwo.Text = Request("wo")
                    localpo.Text = Request("po")
                End If
                SaleBind()
            End If

        End Sub
        Sub SaleBind()
            Dim i, j As Integer
            Dim mn(6) As String
            Array.Clear(mn, 0, 7)
            mn(0) = "1000"
            mn(1) = "1200"
            mn(2) = "1400"
            mn(3) = "2000"
            mn(4) = "2100"
            mn(5) = "3000"
            mn(6) = "4000"

            j = Math.Max(sitehashtableA.Count, sitehashtableB.Count)
            j = Math.Max(j, sitehashtableC.Count)
            If j = 0 Then
                Response.Write(sitehashtableA.Count.ToString() & "/" & sitehashtableB.Count.ToString() & "/" & sitehashtableC.Count.ToString())
                Exit Sub
            Else
                Dim dt As New DataTable
                Dim dr1 As DataRow
                dt.Columns.Add(New DataColumn("site", System.Type.GetType("System.Int32")))
                dt.Columns.Add(New DataColumn("Inv", System.Type.GetType("System.String")))
                dt.Columns.Add(New DataColumn("Wo", System.Type.GetType("System.String")))
                dt.Columns.Add(New DataColumn("Po", System.Type.GetType("System.String")))
                For i = 0 To mn.Length - 1
                    dr1 = dt.NewRow()
                    dr1.Item("site") = mn(i)
                    dr1.Item("Inv") = Math.Round(sitehashtableA(mn(i)), 6)
                    dr1.Item("Wo") = Math.Round(sitehashtableB(mn(i)), 6)
                    dr1.Item("Po") = Math.Round(sitehashtableC(mn(i)), 6)
                    dt.Rows.Add(dr1)
                Next
                Dim dv As DataView
                dv = New DataView(dt)
                Try
                    Datagrid1.DataSource = dv
                    Datagrid1.DataBind()
                Catch
                End Try
            End If
            sitehashtableA.Clear()
            sitehashtableB.Clear()
            sitehashtableC.Clear()
        End Sub

        Function getdata(ByVal part As String) As Boolean
            sitehashtableA.Clear()
            sitehashtableB.Clear()
            sitehashtableC.Clear()
            Dim query As String
            Dim Qadreader As SqlDataReader

            Try
                query = "select qty,ld_site from  "
                query &= " OPENROWSET('MSDASQL','DSN=mfgtcp;uid=zxdz;pwd=zxdz;HOST=10.3.0.75;port=60055;DB=mfgtcp;',"
                query &= " 'select cast(sum(ld_qty_oh) as decimal(18,6)) as qty,ld_site from pub.ld_det where ld_qty_oh <> 0 and  ld_part=''" & part & "'' and ld_status <> ''SHIPED''  group by ld_site')    "
                Qadreader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, query)
                While Qadreader.Read
                    sitehashtableA.Add(Qadreader(1), Qadreader(0))
                    'If sitehashtableA.Contains(Qadreader(1)) = True Then
                    '    sitehashtableA(Qadreader(1)) = sitehashtableA(Qadreader(1)) + Qadreader(0)
                    'End If
                End While
                Qadreader.Close()
            Catch ex As Exception
                Response.Write(ex.ToString & " <br>")
                Return False
            End Try

            query = " select case when z.part is null then w.part else z.part end as part, isnull(z.site,'') as psite,z.poqty,isnull(w.site,'') as wsite,w.woqty From"
            query &= "  (select sum(pod_qty_ord-pod_qty_rcvd) as poqty,part,site  from Z_QAD_po where part='" & part & "' group by site,part) z  "
            query &= " full outer join (select part,site,sum(wod_qty_req - wod_qty_iss) as woqty from Z_QAD_wo where part='" & part & "' group by site,part)w on w.part=z.part and z.site = w.site "
            Qadreader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, query)
            While Qadreader.Read
                If Qadreader(1) <> "" Then
                    sitehashtableC.Add(Qadreader(1), Qadreader(2))
                End If
                If Qadreader(3) <> "" Then
                    sitehashtableB.Add(Qadreader(3), Qadreader(4))
                End If
            End While
            Qadreader.Close()

            getdata = True
        End Function

        Protected Sub Datagrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            SaleBind()
        End Sub
    End Class

End Namespace
