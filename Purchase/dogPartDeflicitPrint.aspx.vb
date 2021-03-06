'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-7-12
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Production Schedule List Export To Excel
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class dogPartDeflicitPrint
        Inherits BasePage

    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
    Dim row As TableRow
    Dim cell As TableCell
    Public chk As New adamClass
    Dim myrow As TableRow
    Dim mycell As TableCell

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
        Dim i, j, dgw, cnt As Integer
        Dim strColumn As String
        Dim reader As SqlDataReader
        Dim numQty As Decimal

        mycell = New TableCell
        myrow = New TableRow
        addCell("装箱日期")
        addCell("定单号")
        addCell("产品编号", 200)
        addCell("产品简称")
        addCell("客户代码")
        addCell("出运套数")
        addCell("出运只数")
        addCell("最早运期")
        addCell("最迟运期")
        addCell("采购单号")
        addCell("部件编号", 200)
        addCell("订货比例")
        addCell("订货量")
        addCell("制作地代码")
        addCell("送货地代码")
        addCell("首批到货期")
        addCell("必须到货期")
        addCell("备注", 200)

        cnt = 0
        strSql = " Select name From Procurements Order By id "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read()
            addCell(reader(0))
            cnt = cnt + 1
        End While
        reader.Close()
        exl.Rows.Add(myrow)
        Dim strSum(cnt) As Decimal
        i = 0
        While i <= cnt - 1
            Dim num As Decimal = 0.0
            strSum.SetValue(num, i)
            i = i + 1
        End While

        strSql = " Select Isnull(pod.case_date,''), po.order_code, pd.code, c.company_code, pod.order_set, pod.order_qty, " _
               & " Isnull(po.first_deliver_date, ''), Isnull(pod.deliver_date_end,''), dpi.procurement_code, pt.code, " _
               & " Isnull(dpi.rate,1), Isnull(m.plantCode,''), Isnull(d.plantCode,''), Isnull(dpi.first_partin_date,''), Isnull(dpi.last_partin_date,''), " _
               & " Isnull(dpi.notes,''), dpi.prod_qty, p.id, Sum(Isnull(dpid.plan_qty,0))-Sum(Isnull(dpid.real_qty,0)),  Isnull(pd.simpleCode,'') " _
               & " From Dog_PartIn dpi " _
               & " Inner Join Product_order_detail pod On dpi.prod_order_detail_id=pod.prod_order_detail_id " _
               & " Inner Join Product_orders po On pod.prod_order_id=po.prod_order_id And Upper(order_status)<>'CLOSE' "
        If Request("order") <> Nothing Then
            strSql &= " And po.order_code=N'" & chk.sqlEncode(Server.UrlDecode(Request("order"))) & "'"
        End If
        strSql &= " Inner Join Dog_PartIn_Detail dpid On dpi.id=dpid.dog_partin_id " _
               & " Inner Join tcpc0.dbo.Items pd On pd.id=pod.prod_id "
        If Request("prod") <> Nothing Then
            strSql &= " And pd.code=N'" & chk.sqlEncode(Server.UrlDecode(Request("prod"))) & "'"
        End If
        strSql &= " Inner Join tcpc0.dbo.Items pt On pt.id=dpi.prod_id " _
               & " Inner Join Procurements p On pt.code Like '%'+p.code+'%'" _
               & " Inner Join tcpc0.dbo.Companies c On c.company_id=po.company_id "
        If Request("cust") <> Nothing Then
            strSql &= " And c.company_code=N'" & chk.sqlEncode(Server.UrlDecode(Request("cust"))) & "'"
        End If
        strSql &= " Left Outer Join tcpc0.dbo.Plants m On m.plantID=dpi.manufactory_id " _
               & " Left Outer Join tcpc0.dbo.Plants d On d.plantID=dpi.delivery_id " _
               & " Where Isnull(dpi.first_partin_date,'1900-1-1')<=getdate() " _
               & " Group By Isnull(pod.case_date,''), po.order_code, pd.code, c.company_code, pod.order_set, pod.order_qty, " _
               & " Isnull(po.first_deliver_date, ''), Isnull(pod.deliver_date_end,''), dpi.procurement_code, pt.code, " _
               & " Isnull(dpi.rate,1), m.plantCode, d.plantCode, Isnull(dpi.first_partin_date,''), Isnull(dpi.last_partin_date,''), " _
               & " dpi.notes, dpi.prod_qty, p.id,  Isnull(pd.simpleCode,'') " _
               & " Order by po.order_code, pd.code "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        i = 0
        While reader.Read()
            j = 0
            mycell = New TableCell
            myrow = New TableRow
            If Year(CDate(reader(0))) = "1900" Then
                addCell("")
            Else
                addCell(Format(reader(0), "yyyy-MM-dd"))
            End If
            addCell(reader(1))
            addCell(reader(2), 200)
            addCell(reader(19))
            addCell(reader(3))
            addCell(Format(reader(4), "##,###"))
            addCell(Format(reader(5), "##,###"))
            If Year(CDate(reader(6))) = "1900" Then
                addCell("")
            Else
                addCell(Format(reader(6), "yyyy-MM-dd"))
            End If
            If Year(CDate(reader(7))) = "1900" Then
                addCell("")
            Else
                addCell(Format(reader(7), "yyyy-MM-dd"))
            End If
            addCell(reader(8))
            addCell(reader(9), 200)
            addCell(Format(reader(10), "##0.00").ToString().Trim())
            addCell(Format(CDbl(reader(10)) * CDbl(reader(16)), "##,###.##"))
            addCell(reader(11))
            addCell(reader(12))
            If Year(CDate(reader(13))) = "1900" Then
                addCell("")
            Else
                addCell(Format(reader(13), "yyyy-MM-dd"))
            End If
            If Year(CDate(reader(14))) = "1900" Then
                addCell("")
            Else
                addCell(Format(reader(14), "yyyy-MM-dd"))
            End If
            addCell(reader(15), 200)
            While j <= cnt - 1
                If j = CInt(reader(17)) - 1 Then
                    addCell(Format(reader(18), "##,###.##"))
                    numQty = CDbl(strSum.GetValue(j)) + CDbl(reader(18))
                Else
                    addCell("")
                    numQty = CDbl(strSum.GetValue(j))
                End If
                strSum.SetValue(numQty, j)
                j = j + 1
            End While
            exl.Rows.Add(myrow)
            i = i + 1
        End While

        mycell = New TableCell
        myrow = New TableRow
        addCell("")
        addCell("")
        addCell("", 200)
        addCell("")
        addCell("")
        addCell("")
        addCell("")
        addCell("")
        addCell("")
        addCell("")
        addCell("", 200)
        addCell("")
        addCell("")
        addCell("")
        addCell("")
        addCell("")
        addCell("")
        addCell("合计:", 200)
        j = 0
        While j <= cnt - 1
            addCell(Format(strSum.GetValue(j), "##,###.##").ToString().Trim())
            j = j + 1
        End While
        exl.Rows.Add(myrow)
        i = i + 1

        While (i < 100)
            mycell = New TableCell
            myrow = New TableRow
            addCell("")
            addCell("")
            addCell("", 200)
            addCell("")
            addCell("")
            addCell("")
            addCell("")
            addCell("")
            addCell("")
            addCell("")
            addCell("", 200)
            addCell("")
            addCell("")
            addCell("")
            addCell("")
            addCell("")
            addCell("")
            addCell("")
            j = 0
            While j <= cnt - 1
                addCell("")
                j = j + 1
            End While
            exl.Rows.Add(myrow)
            i = i + 1
        End While

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
        Response.Flush()
    End Sub

    Public Sub addCell(ByVal str As String, Optional ByVal w As Integer = 100)
        mycell = New TableCell
        mycell.Text = str
        mycell.Width = New Unit(w)
        mycell.HorizontalAlign = HorizontalAlign.Center
        myrow.Cells.Add(mycell)
    End Sub
End Class

End Namespace
