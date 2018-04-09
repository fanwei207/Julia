


Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data





Partial Class part_PartSizeHistory
    Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer
    Dim strSQL As String

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

            strSQL = " Select code From Items Where id=" & Request("id")
            code.Text = "<font size=4>" & SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) & "</font>"
            BindData()
        End If
    End Sub

    Sub BindData()
        Dim dst As DataSet

        strSQL = " Select p.version,  Isnull(p.box_weight,0), Isnull(p.box_size,0), Isnull(p.box_length,0), Isnull(p.box_width,0), Isnull(p.box_depth,0), " _
               & " Isnull(p.num_per_pack,0), Isnull(p.num_per_box,0), Isnull(p.box_per_pallet,0), u.UserName, p.createdDate " _
               & " From tcpc0.dbo.Product_physical_his p " _
               & " Left Outer Join tcpc0.dbo.users u On u.UserID=p.createdBy " _
               & " Where prod_id=" & Request("id") & " Order By p.version Desc "

        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

        Session("EXHeader") = "部件号~" & code.Text.Trim() & "~^日期~" & Format(DateTime.Now, "yyyy-MM-dd") & "~^"
        Session("EXSQL") = strSQL
        Session("EXTitle") = "<b>版本号</b>~^<b>重量</b>~^<b>体积</b>~^<b>长度</b>~^<b>宽度</b>~^<b>深度</b>~^<b>只数/包装</b>~^<b>只数/箱</b>~^<b>箱数/货盘</b>~^<b>操作员</b>~^<b>操作日期</b>~^"

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("version", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("box", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("weight", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("size", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("pack", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("pallet", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("length", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("width", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("depth", System.Type.GetType("System.Decimal")))
        dtl.Columns.Add(New DataColumn("createdBy", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("createdDate", System.Type.GetType("System.DateTime")))

        With dst.Tables(0)
            If dst.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("box") = .Rows(i).Item(7)
                    drow.Item("weight") = .Rows(i).Item(1)
                    drow.Item("size") = .Rows(i).Item(2)
                    drow.Item("pack") = .Rows(i).Item(6)
                    drow.Item("pallet") = .Rows(i).Item(8)
                    drow.Item("createdBy") = .Rows(i).Item(9).ToString().Trim()
                    drow.Item("createdDate") = Convert.ToDateTime(.Rows(i).Item(10)).ToShortDateString().Trim()
                    drow.Item("version") = .Rows(i).Item(0)
                    drow.Item("length") = .Rows(i).Item(3)
                    drow.Item("width") = .Rows(i).Item(4)
                    drow.Item("depth") = .Rows(i).Item(5)
                    dtl.Rows.Add(drow)
                Next
            Else
                ltlAlert.Text = "alert('没有你要的历史纪录!');"
                Session("EXHeader") = Nothing
                Session("EXSQL") = Nothing
                Session("EXTitle") = Nothing
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)

        Try
            DataGrid1.DataSource = dvw
            DataGrid1.DataBind()
        Catch
        End Try
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DataGrid1.CurrentPageIndex = 0
        BindData()
    End Sub

    Private Sub back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles back.Click
        If Request("st") = "true" Then
            If Request("code") <> Nothing Then
                If Request("cat") <> Nothing Then
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=true"), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&st=true"), True)
                    End If
                Else
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&pg=" & Request("pg") & "&st=true"), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&st=true"), True)
                    End If
                End If
            Else
                If Request("cat") <> Nothing Then
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=true"), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&st=true"), True)
                    End If
                Else
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&pg=" & Request("pg") & "&st=true"), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&st=true"), True)
                    End If
                End If
            End If
        ElseIf Request("st") = "false" Then
            If Request("code") <> Nothing Then
                If Request("cat") <> Nothing Then
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=false"), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&st=false"), True)
                    End If
                Else
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&pg=" & Request("pg") & "&st=false"), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&st=false"), True)
                    End If
                End If
            Else
                If Request("cat") <> Nothing Then
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=false"), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&st=false"), True)
                    End If
                Else
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&pg=" & Request("pg") & "&st=false"), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&st=false"), True)
                    End If
                End If
            End If
        Else
            If Request("code") <> Nothing Then
                If Request("cat") <> Nothing Then
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat")), True)
                    End If
                Else
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&pg=" & Request("cat")), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&code=" & Request("code")), True)
                    End If
                End If
            Else
                If Request("cat") <> Nothing Then
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat")), True)
                    End If
                Else
                    If Request("pg") <> Nothing Then
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id") & "&pg=" & Request("pg")), True)
                    Else
                        Response.Redirect(chk.urlRand("partSizeEdit.aspx?id=" & Request("id")), True)
                    End If
                End If
            End If
        End If
    End Sub
End Class


