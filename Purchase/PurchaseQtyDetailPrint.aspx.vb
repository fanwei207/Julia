'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-5-14
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Export Part Inventory Detail To Excel
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class PurchaseQtyDetailPrint
        Inherits BasePage
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Dim strsql As String
    Public chk As New adamClass

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
        Dim strOrder As String = ""
        Dim strdept As String = ""
        PIMasteryRow("部件号", Server.UrlDecode(Request("partcode")), "", "", "", "", "", "", "", "")
        PIMasteryRow("部件描述", Server.UrlDecode(Request("partdesc")), "", "", "", "", "", "", "", "")

        PIMasteryRow("仓库", Server.UrlDecode(Request("place")), "", "", "", "", "", "", "", "")
        PIMasteryRow("状态", Server.UrlDecode(Request("name")), "", "", "", "", "", "", "", "")

        strsql = " Select Isnull(Sum(tran_qty),0) From Part_tran Where part_id='" & Request("partID") & "' And warehouseID='" _
               & Request("placeID") & "' And Isnull(status,0)='" & Request("st") & "'"
        If Request("type") <> "" Then
            strsql &= " And tran_type='" & Request("type") & "'"
        End If
        PIMasteryRow("库存数量", SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strsql), "", "", "", "", "", "", "", "")

        PIMasteryRow("", "", "", "", "", "", "", "", "", "")
        PIMasteryRow("出入库情况", "", "", "", "", "", "", "", "", "")
        PIMasteryRow("日期", "数量", "部门/供应商编号", "定单号", "类型", "计划单号", "送货单号", "收料单号", "备注", "公司")
        If Session("plantCode") = "6" Then
            strsql = " Select Isnull(pt.plantID,0), Isnull(pt.order_id,0), pt.tran_date, pt.tran_qty, pt.tran_type, " _
                   & " Case pt.tran_type When 'I' Then c.company_name+'('+c.company_code+')' When 'RS' Then c.company_name+'('+c.company_code+')' Else d.name+'('+d.code+')' End, Isnull(po.part_order_code,'')," _
                   & " Isnull(pop.prod_order_plan_code,''),  Isnull(pt.deliverycode,''), Isnull(pt.receivecode,''), Isnull(pt.notes,''), Isnull(p.plantcode,''), pt.part_tran_id " _
                   & " From Part_tran pt " _
                   & " Inner Join warehouse w On pt.warehouseID=w.warehouseID " _
                   & " Left Outer Join part_orders po On pt.order_id=po.part_order_id " _
                   & " Left Outer Join tcpc0.dbo.users u On u.userID=pt.createdBy  " _
                   & " Left Outer Join tcpc0.dbo.Companies c On pt.comp_dept_id=c.company_id And c.active=1 " _
                   & " Left Outer Join departments d On pt.comp_dept_id=d.departmentID And d.active=1 " _
                   & " Left Outer Join Product_order_plan pop On pt.mrp_id=pop.prod_order_plan_id " _
                   & " Left Outer Join tcpc0.dbo.plants p On p.plantID=pt.plantid " _
                   & " Where pt.part_id='" & Request("partID") & "' And pt.warehouseID='" & Request("placeID") _
                   & "' And Isnull(pt.status,0)='" & Request("st") & "'"
        Else
            strsql = " Select Isnull(pt.plantID,0), Isnull(pt.order_id,0), pt.tran_date, pt.tran_qty, pt.tran_type, " _
                   & " Case pt.tran_type When 'I' Then c.company_name+'('+c.company_code+')' When 'RS' Then c.company_name+'('+c.company_code+')' Else d.name+'('+d.code+')' End, Isnull(po.part_order_code,'')," _
                   & " Isnull(pop.prod_order_plan_code,''),  Isnull(pt.deliverycode,''), Isnull(pt.receivecode,''), Isnull(pt.notes,''), Isnull(p.plantcode,''), pt.part_tran_id" _
                   & " From Part_tran pt " _
                   & " Inner Join warehouse w On pt.warehouseID=w.warehouseID " _
                   & " Left Outer Join part_orders po On pt.order_id=po.part_order_id " _
                   & " Left Outer Join tcpc0.dbo.users u On u.userID=pt.createdBy  " _
                   & " Left Outer Join tcpc0.dbo.Companies c On pt.comp_dept_id=c.company_id And c.active=1 " _
                   & " Left Outer Join departments d On pt.comp_dept_id=d.departmentID And d.active=1 " _
                   & " Left Outer Join Product_order_plan_mrp popm On pt.mrp_id=popm.mrp_id " _
                   & " Left Outer Join Product_order_plan pop On popm.prod_order_plan_id=pop.prod_order_plan_id " _
                   & " Left Outer Join tcpc0.dbo.plants p On p.plantID=pt.plantid " _
                   & " Where pt.part_id='" & Request("partID") & "' And pt.warehouseID='" & Request("placeID") _
                   & "' And Isnull(pt.status,0)='" & Request("st") & "'"
        End If
        If Request("from") <> Nothing Then
            strsql &= " And pt.tran_date>='" & Request("from") & "'"
        End If

        If Request("to") <> Nothing Then
            strsql &= " And pt.tran_date<'" & Convert.ToDateTime(Request("to")).AddDays(1) & "'"
        End If

        If Request("type") <> Nothing Then
            strsql &= " And pt.tran_type='" & Request("type") & "'"
        End If
        strsql &= " Order By pt.tran_date DESC "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)
        While (reader.Read())
            If CInt(reader(0)) = 0 Then
                PIMasteryRow(Format(reader(2), "yyyy-MM-dd"), reader(3).ToString(), reader(5).ToString(), reader(6).ToString(), reader(4).ToString(), reader(7).ToString().Trim(), reader(8).ToString().Trim(), reader(9).ToString().Trim(), reader(10).ToString().Trim(), reader(11).ToString().Trim())
            Else
                Select Case CInt(reader(0))
                    Case 1
                        strOrder = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, "Select Isnull(order_code,'') From tcpc1.dbo.Product_orders Where prod_order_id='" & reader(1) & "'")
                        If reader(4) <> "I" And reader(4) <> "RS" Then
                            strdept = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, " Select code+'('+ name + ')' From tcpc1.dbo.departments Where departmentID=(Select comp_dept_id From part_tran Where part_tran_id='" & reader(12) & "')")
                        End If
                    Case 2
                        strOrder = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, "Select Isnull(order_code,'') From tcpc2.dbo.Product_orders Where prod_order_id='" & reader(1) & "'")
                        If reader(4) <> "I" And reader(4) <> "RS" Then
                            strdept = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, " Select code+'('+ name + ')' From tcpc2.dbo.departments Where departmentID=(Select comp_dept_id From part_tran Where part_tran_id='" & reader(12) & "')")
                        End If
                    Case 3
                        strOrder = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, "Select Isnull(order_code,'') From tcpc3.dbo.Product_orders Where prod_order_id='" & reader(1) & "'")
                        If reader(4) <> "I" And reader(4) <> "RS" Then
                            strdept = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, " Select code+'('+ name + ')' From tcpc3.dbo.departments Where departmentID=(Select comp_dept_id From part_tran Where part_tran_id='" & reader(12) & "')")
                        End If
                    Case 4
                        strOrder = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, "Select Isnull(order_code,'') From tcpc4.dbo.Product_orders Where prod_order_id='" & reader(1) & "'")
                        If reader(4) <> "I" And reader(4) <> "RS" Then
                            strdept = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, " Select code+'('+ name + ')' From tcpc4.dbo.departments Where departmentID=(Select comp_dept_id From part_tran Where part_tran_id='" & reader(12) & "')")
                        End If
                    Case 5
                        strOrder = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, "Select Isnull(order_code,'') From tcpc5.dbo.Product_orders Where prod_order_id='" & reader(1) & "'")
                        If reader(4) <> "I" And reader(4) <> "RS" Then
                            strdept = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, " Select code+'('+ name + ')' From tcpc5.dbo.departments Where departmentID=(Select comp_dept_id From part_tran Where part_tran_id='" & reader(12) & "')")
                        End If
                    Case 6
                        strOrder = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, "Select Isnull(order_code,'') From tcpc6.dbo.Product_orders Where prod_order_id='" & reader(1) & "'")
                        If reader(4) <> "I" And reader(4) <> "RS" Then
                            strdept = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, " Select code+'('+ name + ')' From tcpc6.dbo.departments Where departmentID=(Select comp_dept_id From part_tran Where part_tran_id='" & reader(12) & "')")
                        End If
                End Select
                If strOrder = Nothing Then
                    strOrder = ""
                End If
                PIMasteryRow(Format(reader(2), "yyyy-MM-dd"), reader(3).ToString(), strdept, strOrder.Trim(), reader(4).ToString(), reader(7).ToString().Trim(), reader(8).ToString().Trim(), reader(9).ToString().Trim(), reader(10).ToString().Trim(), reader(11).ToString().Trim())
            End If
        End While
        PIMasteryRow("", "", "", "", "", "", "", "", "", "")
        Response.Clear()
        Response.Buffer = True
        Response.Charset = "UTF-8"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=InventoryDetail.xls")
        Response.Flush()
    End Sub

    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String)
        row = New TableRow
        row.HorizontalAlign = HorizontalAlign.Left
        row.BorderWidth = New Unit(0)


        cell = New TableCell
        If (str0 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str0.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str1 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str1.Trim()
        End If
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Right
        cell.Wrap = True
        row.Cells.Add(cell)

        cell = New TableCell
        If (str2 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str2.Trim()
        End If
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        If (str3 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str3.Trim()
        End If
        cell.Width = New Unit(150)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        If (str4 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str4.Trim()
        End If
        cell.Width = New Unit(80)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        If (str5 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str5.Trim()
        End If
        cell.Width = New Unit(150)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        If (str6 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str6.Trim()
        End If
        cell.Width = New Unit(150)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        cell = New TableCell
        If (str7 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str7.Trim()
        End If
        cell.Width = New Unit(150)
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        row.Cells.Add(cell)

        cell = New TableCell
        If (str8 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str8.Trim()
        End If
        cell.Width = New Unit(150)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        cell = New TableCell
        If (str9 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str9.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Center
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub

End Class
End Namespace
