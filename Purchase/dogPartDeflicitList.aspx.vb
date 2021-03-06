'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-7-12
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Dog Part In Deflicit List And Export To Excel
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class dogPartDeflicitList
        Inherits BasePage

    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer
    Dim strSQL As String
    Dim dst As DataSet

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
            If Request("order") <> Nothing Then
                txtOrderCode.Text = Server.UrlDecode(Request("order"))
            End If
            If Request("prod") <> Nothing Then
                txtProdCode.Text = Server.UrlDecode(Request("prod"))
            End If
            BindData()
        End If
        ltlAlert.Text = "Form1.txtOrderCode.focus();"
    End Sub

    Sub BindData()
        Dim i, j, dgw, cnt As Integer
        Dim strColumn As String
        Dim reader As SqlDataReader
        Dim numQty As Decimal
        dgw = 2000
        j = 0
        i = 0
        strSQL = " Select Count(*) From Procurements "
        cnt = CInt(SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL))

        Session("EXTitle") = "/Purchase/DogPartDeflicitPrint.aspx?order=" & Server.UrlEncode(txtOrderCode.Text.Trim) & "&prod=" & Server.UrlEncode(txtOrderCode.Text.Trim) & "&cust=" & Server.UrlEncode(txtCustCode.Text.Trim()) & "^~^"

        strSQL = " Select Isnull(pod.case_date,''), po.order_code, pd.code, c.company_code, pod.order_total, pod.order_qty, " _
               & " Isnull(po.first_deliver_date, ''), Isnull(pod.deliver_date_end,''), dpi.procurement_code, pt.code, " _
               & " Isnull(dpi.rate,1), m.plantCode, d.plantCode, Isnull(dpi.first_partin_date,''), Isnull(dpi.last_partin_date,''), " _
               & " dpi.notes, dpi.prod_qty, p.id, Sum(Isnull(dpid.plan_qty,0))-Sum(Isnull(dpid.real_qty,0)), Isnull(pd.simpleCode,'') " _
               & " From Dog_PartIn dpi " _
               & " Inner Join Product_order_detail pod On dpi.prod_order_detail_id=pod.prod_order_detail_id " _
               & " Inner Join Product_orders po On pod.prod_order_id=po.prod_order_id And Upper(order_status)<>'CLOSE' "
        If txtOrderCode.Text.Trim().Length > 0 Then
            strSQL &= " And po.order_code=N'" & chk.sqlEncode(txtOrderCode.Text.Trim) & "'"
        End If
        strSQL &= " Inner Join Dog_PartIn_Detail dpid On dpi.id=dpid.dog_partin_id " _
               & " Inner Join tcpc0.dbo.Items pd On pd.id=pod.prod_id "
        If txtProdCode.Text.Trim().Length > 0 Then
            strSQL &= " And pd.code=N'" & chk.sqlEncode(txtProdCode.Text.Trim) & "'"
        End If
        strSQL &= " Inner Join tcpc0.dbo.Items pt On pt.id=dpi.prod_id " _
               & " Inner Join Procurements p On pt.code Like '%'+p.code+'%'" _
               & " Inner Join tcpc0.dbo.Companies c On c.company_id=po.company_id "
        If txtCustCode.Text.Trim.Length > 0 Then
            strSQL &= " And c.company_code=N'" & chk.sqlEncode(txtCustCode.Text.Trim) & "'"
        End If
        strSQL &= " Left Outer Join tcpc0.dbo.Plants m On m.plantID=dpi.manufactory_id " _
               & " Left Outer Join tcpc0.dbo.Plants d On d.plantID=dpi.delivery_id " _
               & " Where Isnull(dpi.first_partin_date,'1900-1-1')<=getdate() " _
               & " Group By Isnull(pod.case_date,''), po.order_code, pd.code, c.company_code, pod.order_total, pod.order_qty, " _
               & " Isnull(po.first_deliver_date, ''), Isnull(pod.deliver_date_end,''), dpi.procurement_code, pt.code, " _
               & " Isnull(dpi.rate,1), m.plantCode, d.plantCode, Isnull(dpi.first_partin_date,''), Isnull(dpi.last_partin_date,''), " _
               & " dpi.notes, dpi.prod_qty, p.id, Isnull(pd.simpleCode,'') " _
               & " Order by po.order_code, pd.code "
        'strSQL = " Select * From dog_part_temp Where userID='" & Session("uID") & "'"
        dst = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, strSQL)

        Session("EXSQL") = strSQL
        Session("EXHeader") = "/Purchase/DogPartDeflicitPrint.aspx?order=" & Server.UrlEncode(txtOrderCode.Text.Trim) & "&prod=" & Server.UrlEncode(txtOrderCode.Text.Trim) & "&cust=" & Server.UrlEncode(txtCustCode.Text.Trim()) & "^~^"

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("order", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("casedate", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("customer", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("shipset", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("shipqty", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("firstdate", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("lastdate", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("procurement", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("partcode", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("rate", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("orderqty", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("manu", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("delivery", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("firstindate", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("lastindate", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("notes", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("simple", System.Type.GetType("System.String")))

        Dim strSum(cnt) As Decimal
        While i <= cnt - 1
            strColumn = "Column" & Str(i + 1)
            dtl.Columns.Add(New DataColumn(strColumn, System.Type.GetType("System.String")))
            Dim num As Decimal = 0.0
            strSum.SetValue(num, i)
            i = i + 1
        End While

        i = 1
        j = 1

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    j = 0
                    drow = dtl.NewRow()
                    drow.Item("gsort") = i + 1
                    If Year(CDate(.Rows(i).Item(0))) = "1900" Then
                        drow.Item("casedate") = ""
                    Else
                        drow.Item("casedate") = Format(.Rows(i).Item(0), "yyyy-MM-dd")
                    End If
                    drow.Item("order") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("code") = .Rows(i).Item(2).ToString().Trim()
                    drow.Item("customer") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("shipset") = .Rows(i).Item(4)
                    drow.Item("shipqty") = .Rows(i).Item(5)
                    If Year(CDate(.Rows(i).Item(6))) = "1900" Then
                        drow.Item("firstdate") = ""
                    Else
                        drow.Item("firstdate") = Format(.Rows(i).Item(6), "yyyy-MM-dd")
                    End If
                    If Year(CDate(.Rows(i).Item(7))) = "1900" Then
                        drow.Item("lastdate") = ""
                    Else
                        drow.Item("lastdate") = Format(.Rows(i).Item(7), "yyyy-MM-dd")
                    End If
                    drow.Item("procurement") = .Rows(i).Item(8).ToString().Trim()
                    drow.Item("partcode") = .Rows(i).Item(9).ToString().Trim()
                    drow.Item("rate") = .Rows(i).Item(10)
                    drow.Item("orderqty") = CDbl(.Rows(i).Item(16)) * CDbl(.Rows(i).Item(10))
                    drow.Item("manu") = .Rows(i).Item(11).ToString().Trim()
                    drow.Item("delivery") = .Rows(i).Item(12).ToString().Trim()
                    If Year(CDate(.Rows(i).Item(13))) = "1900" Then
                        drow.Item("firstindate") = ""
                    Else
                        drow.Item("firstindate") = Format(.Rows(i).Item(13), "yyyy-MM-dd")
                    End If
                    If Year(CDate(.Rows(i).Item(14))) = "1900" Then
                        drow.Item("lastindate") = ""
                    Else
                        drow.Item("lastindate") = Format(.Rows(i).Item(14), "yyyy-MM-dd")
                    End If
                    drow.Item("notes") = .Rows(i).Item(15).ToString().Trim()
                    drow.Item("simple") = .Rows(i).Item(19).ToString().Trim()
                    While j <= cnt - 1
                        strColumn = "Column" & Str(j + 1)
                        If j = CInt(.Rows(i).Item(17)) - 1 Then
                            drow.Item(strColumn) = .Rows(i).Item(18)
                            numQty = CDbl(strSum.GetValue(j)) + CDbl(.Rows(i).Item(18))
                        Else
                            drow.Item(strColumn) = ""
                            numQty = CDbl(strSum.GetValue(j))
                        End If
                        strSum.SetValue(numQty, j)
                        j = j + 1
                    End While
                    dtl.Rows.Add(drow)
                Next
                drow = dtl.NewRow()
                drow.Item("gsort") = i + 1
                drow.Item("notes") = "合计:"
                j = 0
                While j <= cnt - 1
                    strColumn = "Column" & Str(j + 1)
                    drow.Item(strColumn) = strSum.GetValue(j)
                    j = j + 1
                End While
                dtl.Rows.Add(drow)
                lblCount.Text = "数量: " & .Rows.Count
            Else
                lblCount.Text = "数量: 0"
                Session("EXSQL") = Nothing
                Session("EXTitle") = Nothing
                Session("EXHeader") = Nothing
            End If
        End With

        j = 19
        i = 1

        strSQL = " Select name From Procurements Order By id "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
        While reader.Read()
            Dim dyColumn As New BoundColumn
            dyColumn.HeaderText = reader(0)
            dyColumn.HeaderStyle.Width = New Unit(100)
            dyColumn.DataField = "Column" & Str(i)
            dgw = dgw + 100
            dgPSList.Columns.AddAt(j, dyColumn)
            j = j + 1
            i = i + 1
        End While
        reader.Close()

        dgPSList.Width = New Unit(dgw)

        Dim dvw As DataView
        dvw = New DataView(dtl)

            Session("orderby") = "gsort"

            Try
                dvw.Sort = Session("orderby") & Session("orderdir")
                dgPSList.DataSource = dvw
                dgPSList.DataBind()
            Catch
            End Try
        End Sub

    Private Sub BtnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQuery.Click
        BindData()
    End Sub

        Protected Sub dgPSList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPSList.PageIndexChanged
            dgPSList.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace
