Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Partial Class RDW_ProjLogExport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim sqlStr As String
    Dim reader As SqlDataReader


    Dim row As TableRow
    Dim cell As TableCell


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        PIMasteryRow("<b>Project</b>", "<b>Proj Code</b>", "<b>Proj Desc</b>", "<b>SKU#</b>", "<b>Step</b>", "<b>Step Desc</b>", "<b>Message</b>", "<b>Creater</b>", "<b>Create Date</b>" _
                , "<b>UPC</b>", "<b>Product Category</b>", "<b>LED Chip Type</b>", "<b>LED Chip Qty</b>", "<b>Driver Type</b>", "<b>Customer Name</b>")

        BindData()
        Response.ContentType = "application/vnd.ms-excel"
    End Sub
    Private Sub BindData()
        Dim strSQL As String
        Dim ds As DataSet

        strSQL = " select ms.rdw_project,rdw_prodcode,rdw_proddesc,rdw_prodsku,rdw_stepname,rdw_stepdesc,rdw_message,RDw_createname,rdw_createdate "
        strSQL &= " , UPC, ProductCategory, LEDChipType, LEDChipQty, DriverType, CustomerName "
        strSQL &= " from dbo.RDW_Det_Message me inner join rdw_det d on me.rdw_detid = d.rdw_detid "
        strSQL &= " inner join rdw_mstr ms on d.rdw_mstrid = ms.rdw_mstrid "
        strSQL &= " Left Join SKU_Mstr sku On sku.SKU = ms.rdw_prodsku "
        strSQL &= " where 1 = 1  "

        If Request("proj").ToString().Length > 0 Then
            strSQL &= " And ms.rdw_project like Replace('" & Request("proj").ToString() & "', '*', '%')"
        End If

        If Request("msg").ToString().Length > 0 Then
            strSQL &= " And rdw_message like Replace('" & Request("msg").ToString() & "', '*', '%')"
        End If

        If Request("date1").ToString().Trim.Length > 0 Then
            strSQL &= " And Isnull(rdw_createdate, GetDate()) >= '" & Request("date1").ToString() & "'"
        End If

        If Request("date2").ToString().Trim.Length > 0 Then
            strSQL &= " And Isnull(rdw_createdate, GetDate()) < '" & Request("date2").ToString() & "'"
        End If

        If Request("crter").ToString().Length > 0 Then
            strSQL &= " And RDw_createname = '" & Request("crter").ToString() & "'"
        End If

        strSQL = strSQL & " order by rdw_createby,rdw_project,rdw_sort,rdw_createname"

        ds = SqlHelper.ExecuteDataset(System.Configuration.ConfigurationManager.AppSettings("SqlConn.Conn_rdw"), CommandType.Text, strSQL)

        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    PIMasteryRow(.Rows(i).Item(0).ToString().Trim(), _
                                    .Rows(i).Item(1).ToString().Trim(), _
                                    .Rows(i).Item(2).ToString().Trim(), _
                                    .Rows(i).Item(3).ToString().Trim(), _
                                    .Rows(i).Item(4).ToString().Trim(), _
                                    .Rows(i).Item(5).ToString().Trim(), _
                                    .Rows(i).Item(6).ToString().Trim(), _
                                    .Rows(i).Item(7).ToString().Trim(), _
                                    .Rows(i).Item(8).ToString().Trim(), _
                                    .Rows(i).Item(9).ToString().Trim(), _
                                    .Rows(i).Item(10).ToString().Trim(), _
                                    .Rows(i).Item(11).ToString().Trim(), _
                                    .Rows(i).Item(12).ToString().Trim(), _
                                    .Rows(i).Item(13).ToString().Trim(), _
                                    .Rows(i).Item(14).ToString().Trim())
                Next
            End If
        End With
        ds.Reset()
    End Sub
    Sub PIMasteryRow(ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String, ByVal str6 As String, ByVal str7 As String, ByVal str8 As String, ByVal str9 As String, ByVal str10 As String, ByVal str11 As String, ByVal str12 As String, ByVal str13 As String, ByVal str14 As String, ByVal str15 As String)
        row = New TableRow
        row.BackColor = Color.White
        row.HorizontalAlign = HorizontalAlign.Left
        row.BorderWidth = New Unit(0)

        cell = New TableCell
        If (str1 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str1.Trim()
        End If
        cell.Width = New Unit(200)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str2 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str2.Trim()
        End If
        cell.Width = New Unit(150)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str3 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str3.Trim()
        End If
        cell.Width = New Unit(300)
        row.Cells.Add(cell)

        cell = New TableCell
        If (str4 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str4.Trim()
        End If
        cell.Width = New Unit(150)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str5 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str5.Trim()
        End If
        cell.Width = New Unit(200)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str6 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str6.Trim()
        End If
        cell.Width = New Unit(300)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str7 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str7.Trim()
        End If
        cell.Width = New Unit(500)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str8 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str8.Trim()
        End If
        cell.Width = New Unit(70)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str9 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str9.Trim()
        End If
        cell.Width = New Unit(120)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str10 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str10.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str11 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str11.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str12 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str12.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str13 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str13.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str14 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str14.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        cell = New TableCell
        If (str15 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str15.Trim()
        End If
        cell.Width = New Unit(100)
        cell.HorizontalAlign = HorizontalAlign.Left
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub
End Class
