Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class PurTemplate
        Inherits BasePage
    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
    Dim row As TableRow
    Dim cell As TableCell
    Public chk As New adamClass
    Dim myrow As TableRow
    Dim mycell As TableCell
    Dim reader, reader1 As SqlDataReader
    Dim Rnt As Integer
    Dim i As Integer

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
        'Try
        myrow = New TableRow
        addCell_H("装箱日期时间", 90, 1, 1)
        addCell_H("产品名称", 150, 1, 1)
        addCell_H("定单号", 100, 1, 1)
        addCell_H("客户代码", 50, 1, 1)
        addCell_H("出运数量(套)", 60, 1, 1)
        addCell_H("出运数量(只)", 60, 1, 1)
        addCell_H("最早运期", 90, 1, 1)
        addCell_H("最迟运期", 90, 1, 1)
        addCell_H("采购单号", 100, 1, 1)
        addCell_H("部件号", 150, 1, 1)
        addCell_H("订货比例", 50, 1, 1)
        addCell_H("订货数量", 60, 1, 1)
        addCell_H("制作地代码", 60, 1, 1)
        addCell_H("送货地代码", 60, 1, 1)
        addCell_H("首期到货日期", 90, 1, 1)
        addCell_H("必须到货日期", 90, 1, 1)
        addCell_H("备注", 100, 1, 1)
        'write head
        Rnt = 1
        Try
            strSql = "SELECT MAX(cnt) AS rnt FROM ( SELECT COUNT(id) AS cnt FROM Dog_PartIn_Detail GROUP BY dog_partin_id ) DERIVEDTBL "
            Rnt = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
        Catch
        End Try

        For i = 1 To Rnt
            addCell_H("计划日期", 90, 1, 1)
            addCell_H("应到数", 60, 1, 1)
            addCell_H("实到数", 60, 1, 1)
            addCell_H("备注", 100, 1, 1)
        Next
        While i < Rnt + 40
            addCell("", 60, 1, 1)
            i = i + 1
        End While

        exl.Rows.Add(myrow)
        'myrow = New TableRow
        'exl.Rows.Add(myrow)
        ' write content
        getcontent(Rnt)

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=PurchaseImportTemplate.xls")
        Response.Flush()
        'Catch ex As Exception
        '    ltlAlert.Text = "alert('导出错误！');window.close();"
        '    Exit Sub
        'End Try
    End Sub
    Sub getcontent(ByVal rnt As Integer)
        strSql = " SELECT isnull(pod.case_date,' '),i.code as code,po.order_code,c.company_code,pod.order_set,dp.prod_qty,isnull(pod.deliver_date,''),isnull(pod.deliver_date_end,''),isnull(dp.procurement_code,''),ii.code as partcode," _
                & " isnull(dp.rate,1),isnull(pl1.plantcode,'') as manufactory_code,isnull(pl2.plantcode,'') as delivery_code,isnull(dp.first_partin_date,''),isnull(dp.last_partin_date,''),isnull(dp.notes,''),dp.id,pod.order_qty " _
               & " FROM Dog_PartIn dp INNER JOIN Product_order_detail pod ON pod.prod_order_detail_id = dp.prod_order_detail_id " _
               & " INNER JOIN product_orders po ON po.prod_order_id = pod.prod_order_id  " _
               & " INNER JOIN  tcpc0.dbo.Items i ON i.id = pod.prod_id " _
               & " inner join tcpc0.dbo.Items ii on ii.id=dp.prod_id " _
               & " inner join Procurements pro on ii.code like '%'+pro.code+'%'" _
               & " inner join tcpc0.dbo.companies c on c.company_id=po.company_id" _
               & " left join tcpc0.dbo.Plants pl1 on pl1.plantID=dp.manufactory_id " _
               & " left join tcpc0.dbo.plants pl2 on pl2.plantID=dp.delivery_id "
        If Session("uRole") > 1 Then
            strSql &= " inner join User_Procurement upro on pro.id=upro.procurementID and upro.userID='" & Session("uID") & "' "
        End If
        strSql &= " where po.order_status<>'CLOSE'"
        If Request("order_code") <> "" Then
            strSql &= " and po.order_code='" & Request("order_code") & "'"
        End If
        If Request("code") <> "" Then
            strSql &= " and i.code='" & Request("code") & "'"
        End If
        If Request("type") <> "" Then
            strSql &= " and ii.code like '%'+'" & Request("type") & "'+'%'"
        End If
        strSql &= " order by po.order_code,i.code,ii.code"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        Dim line As Integer = 0
        While reader.Read()
            myrow = New TableRow
            addCell(reader(0).ToString(), 90, 1, 1)
            addCell(reader(1).ToString(), 150, 1, 1)
            addCell(reader(2).ToString(), 100, 1, 1)
            addCell(reader(3).ToString(), 50, 1, 1)
            addCell(reader(4).ToString(), 60, 1, 1)
            addCell(reader(17).ToString(), 60, 1, 1)
            addCell(reader(6).ToString(), 90, 1, 1)
            addCell(reader(7).ToString(), 90, 1, 1)
            addCell(reader(8).ToString(), 100, 1, 1)
            addCell(reader(9).ToString(), 150, 1, 1)
            addCell(reader(10).ToString(), 50, 1, 1)
            addCell((reader(10) * reader(5)).ToString(), 50, 1, 1)
            addCell(reader(11).ToString(), 60, 1, 1)
            addCell(reader(12).ToString(), 60, 1, 1)
            addCell(reader(13).ToString(), 90, 1, 1)
            addCell(reader(14).ToString(), 90, 1, 1)
            addCell(reader(15).ToString(), 100, 1, 1)
            strSql = "select isnull(plan_date,''),plan_qty,real_qty,notes from dog_partin_detail where dog_partin_id='" & reader(16) & "'"
            reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            Dim i As Integer = 0
            If reader1.HasRows Then
                While reader1.Read
                    addCell(reader1(0).ToString(), 90, 1, 1)
                    addCell(reader1(1).ToString(), 60, 1, 1)
                    addCell(reader1(2).ToString(), 60, 1, 1)
                    addCell(reader1(3).ToString(), 100, 1, 1)
                    i = i + 1
                End While
            End If
            reader1.Close()
            While i < rnt
                addCell("", 90, 1, 1)
                addCell("", 60, 1, 1)
                addCell("", 60, 1, 1)
                addCell("", 100, 1, 1)
                i = i + 1
            End While
            While i < rnt + 40
                addCell("", 60, 1, 1)
                i = i + 1
            End While
            exl.Rows.Add(myrow)
            line = line + 1
        End While
        reader.Close()

        While line < 300
            myrow = New TableRow
            addCell("", 90, 1, 1)
            addCell("", 150, 1, 1)
            addCell("", 100, 1, 1)
            addCell("", 50, 1, 1)
            addCell("", 60, 1, 1)
            addCell("", 60, 1, 1)
            addCell("", 90, 1, 1)
            addCell("", 90, 1, 1)
            addCell("", 100, 1, 1)
            addCell("", 150, 1, 1)
            addCell("", 50, 1, 1)
            addCell("", 50, 1, 1)
            addCell("", 60, 1, 1)
            addCell("", 60, 1, 1)
            addCell("", 90, 1, 1)
            addCell("", 90, 1, 1)
            addCell("", 100, 1, 1)
            i = 0
            While i < rnt + 43
                addCell("", 60, 1, 1)
                i = i + 1
            End While
            exl.Rows.Add(myrow)
            line = line + 1
        End While
    End Sub
    Public Sub addCell_H(ByVal str As String, Optional ByVal w As Integer = 100, Optional ByVal col As Integer = 1, Optional ByVal row As Integer = 1)
        mycell = New TableCell
        mycell.Text = str
        mycell.Width = New Unit(w)
        mycell.ColumnSpan = col  'the num of column
        mycell.RowSpan = row  'the num of row
        mycell.HorizontalAlign = HorizontalAlign.Center
        myrow.Cells.Add(mycell)
    End Sub
    Public Sub addCell(ByVal str As String, Optional ByVal w As Integer = 100, Optional ByVal col As Integer = 1, Optional ByVal row As Integer = 1)
        mycell = New TableCell
        mycell.Text = str
        If mycell.Text.Length > 6 Then
            If IsDate(mycell.Text) Then
                If CDate(mycell.Text) = CDate("1900.1.1") Then
                    mycell.Text = ""
                Else
                    mycell.Text = Format(CDate(mycell.Text), "yyyy-MM-dd")
                End If
            End If
        End If
        mycell.Width = New Unit(w)
        mycell.ColumnSpan = col  'the num of column
        mycell.RowSpan = row  'the num of row
        mycell.HorizontalAlign = HorizontalAlign.Center
        myrow.Cells.Add(mycell)
    End Sub

End Class

End Namespace
