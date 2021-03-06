'*@     Create By   :   Ye Bin    
'*@     Create Date :   2008-1-14
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Export Part Inventory Have Qad To Excel
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class ExportInvQad2
    Inherits System.Web.UI.Page
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
       
        Dim i As Integer = 0
        Dim code2 As String = ""
        Dim st2 As String = ""

        PIMasteryRow(Session("orgName"), "盘点表", "库位：", Request("wh"), "保管员", "", "", "抽盘人")
        PIMasteryRow("部件号", "QAD号", "跟踪号", "卡数量", "实盘数", "抽盘数", "分类", "电脑数")

        strsql = " Select v.code,v.qad, v.name,sum(v.qty) as qty,v.st, Isnull(iv.card,'--') as card ,iv.sort1,iv.sort2,iv.sort3 from "
        strsql &= " (Select i.code, isnull(i.item_qad,'') as qad,ic.name,sum(isnull(p.total_qty,0)) as qty,case when s.StatusName=N'待包装' then '' else isnull(s.StatusName,'') end as st " _
                & " From Part_inv p " _
                & " Inner Join tcpc0.dbo.Items i On i.id=p.part_id " _
                & " Inner Join tcpc0.dbo.ItemCategory ic On ic.id=i.category " _
                & " left outer Join tcpc0.dbo.Status s On s.id=isnull(p.status,0)" _
                & " Where p.total_qty<>0 and p.warehouseid='" & Request("wid") & "'" _
                & " group by i.code,i.item_qad,ic.name,s.StatusName "
        strsql &= " UNION " _
                & " Select i.code, isnull(i.item_qad,'') as qad,ic.name,sum(isnull(p.total_qty,0)) as qty,case when s.StatusName=N'待包装' then '' else isnull(s.StatusName,'') end as st  " _
                & " From Product_inv p " _
                & " Inner Join tcpc0.dbo.Items i On i.id=p.prod_id " _
                & " Inner Join tcpc0.dbo.ItemCategory ic On ic.id=i.category " _
                & " left outer Join tcpc0.dbo.Status s On s.id=isnull(p.status,0)" _
                & " Where p.total_qty<>0 and p.warehouseid='" & Request("wid") & "'" _
                & " group by i.code,i.item_qad,ic.name,s.StatusName "
        strsql &= " UNION " _
                & " Select i.code, isnull(i.item_qad,'') as qad ,ic.name,0 as qty,isnull(p.status,'') as st  " _
                & " From  tcpc0.dbo.Inv071231 p " _
                & " Inner Join tcpc0.dbo.Items i On i.code=p.code  " _
                & " Inner Join tcpc0.dbo.ItemCategory ic On ic.id=i.category " _
                & " Where p.plant='" & Session("PlantCode") & "' and isnull(p.wh,'') ='" & Request("wh") & "') as v "
        strsql &= " Left Outer Join  tcpc0.dbo.Inv071231 iv on iv.code=v.code and isnull(iv.status,'')=v.st and iv.plant='" & Session("PlantCode") & "' and isnull(iv.wh,'') ='" & Request("wh") & "' "
        strsql = strsql + " group by v.code,v.st,v.qad, v.name,iv.card,iv.sort1,iv.sort2,iv.sort3 "
        strsql = strsql + " order by iv.sort1,iv.sort2,iv.sort3,v.name,v.code, v.qty desc "

        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)
        While reader.Read()
            If Not (code2 = reader(0).ToString() And st2 = reader(4).ToString()) Then
                PIMasteryRow(reader(0).ToString() & " " & reader(4).ToString(), reader(1).ToString(), reader(5).ToString(), "", "", "", reader(2).ToString(), "")
                code2 = reader(0).ToString()
                st2 = reader(4).ToString()
            End If
        End While
        reader.Close()


        PIMasteryRow("", "", "", "", "", "", "", "")
        Response.Clear()
        Response.Buffer = True
        Response.Charset = "UTF-8"
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=InvQad.xls")
        Response.Flush()
    End Sub

    Sub PIMasteryRow(ByVal str0 As String, ByVal str1 As String, ByVal str2 As String, ByVal str9 As String, ByVal str3 As String, ByVal str5 As String, ByVal str8 As String, ByVal str7 As String)
        row = New TableRow
        row.HorizontalAlign = HorizontalAlign.Left
        row.BorderWidth = New Unit(1)
        row.Height = New Unit(30)
        row.VerticalAlign = VerticalAlign.Top

        cell = New TableCell
        If (str0 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str0.Trim()
        End If
        cell.Width = New Unit(200)
        If IsNumeric(str0) = True Then
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        If (str1 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str1.Trim()
        End If
        cell.Width = New Unit(160)
        If IsNumeric(str1) = True Then
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        If (str2 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str2.Trim()
        End If
        cell.Width = New Unit(95)
        If IsNumeric(str2) = True Then
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        End If
        row.Cells.Add(cell)

        cell = New TableCell
        If (str9 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str9.Trim()
        End If
        cell.Width = New Unit(100)
        If IsNumeric(str9) = True Then
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        End If
        row.Cells.Add(cell)


        cell = New TableCell
        If (str3 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str3.Trim()
        End If
        cell.Width = New Unit(60)
        If IsNumeric(str3) = True Then
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        End If
        row.Cells.Add(cell)


        cell = New TableCell
        If (str5 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str5.Trim()
        End If
        cell.Width = New Unit(60)
        If IsNumeric(str5) = True Then
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        End If
        row.Cells.Add(cell)



        cell = New TableCell
        If (str8 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str8.Trim()
        End If
        cell.Width = New Unit(40)
        If IsNumeric(str8) = True Then
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        End If
        row.Cells.Add(cell)


        cell = New TableCell
        If (str7 = Nothing) Then
            cell.Text = ""
        Else
            cell.Text = str7.Trim()
        End If
        cell.Width = New Unit(100)
        If IsNumeric(str7) = True Then
            cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
        End If
        row.Cells.Add(cell)

        exl.Rows.Add(row)
    End Sub


End Class

End Namespace
