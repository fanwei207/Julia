'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-07-30
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Qad Structure
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class ViewQadStructure
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim strsql As String
        Dim dst As DataSet
        Dim nRet As Integer

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        ' Protected WithEvents dgProd As System.Web.UI.WebControls.DataGrid
        Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
        Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then

                If Not Security("458012").isValid Then
                    BtnDelete.Enabled = False
                Else
                    BtnDelete.Enabled = True
                End If
                If Not Security("458013").isValid Then
                    BtnConfirm.Enabled = False
                Else
                    BtnConfirm.Enabled = True
                End If
                lblProdCode.Text = "父零件为<font size='4pt'>" & Request("parent") & "</font>的结构&nbsp;&nbsp;&nbsp;操作人：" & SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, " Select Distinct createBy From Qad_Stru Where parent='" & Request("parent") & "'") & "&nbsp;&nbsp;&nbsp;核对人：" & SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, " Select checkBy From Items Where item_qad='" & Request("parent") & "'")
                BindData()
            End If
            BtnDelete.Attributes.Add("onclick", "return confirm('确定要删除整个产品结构吗?');")
            BtnConfirm.Attributes.Add("onclick", "return confirm('确定要确认该产品结构吗?');")
        End Sub

        '*********************************************************
        ' @@ PURPOSE : TO fill the part data.
        ' @@ INPUTS  : NA
        ' @@ RETURNS : NA
        '*********************************************************
        Sub BindData()
            Dim qty As Decimal
            Dim reader As SqlDataReader
            strsql = " Select id, parent, Isnull(class,''), child, qty, Isnull(stru_type,''), Isnull(rej_rate,0), startdate, enddate, Isnull(processNo,''), Isnull(lt,''), Isnull(posCode,'') " _
                   & " From tcpc0.dbo.Qad_Stru " _
                   & " Where parent='" & Request("parent") & "'"

            Session("EXTitle") = "150^<b>父零件</b>~^50^<b>级</b>~^150^<b>子零件</b>~^150^<b>单位耗用量</b>~^150^<b>产品结构类型</b>~^80^<b>废品率</b>~^80^<b>生效日期</b>~^80^<b>终止日期</b>~^60^<b>工序号</b>~^100^<b>LT Offset</b>~^250^<b>位号</b>~^"
            Session("EXHeader") = ""
            Session("EXSQL") = strsql

            dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)

            Dim dtlPart As New DataTable
            dtlPart.Columns.Add(New DataColumn("class", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("child", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("qty", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("stru", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("rej", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("start", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("end", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("pno", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("lt", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("pos", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))

            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    Dim j As Integer
                    Dim drowPart As DataRow
                    Dim strPos As String = ""
                    For i = 0 To .Rows.Count - 1
                        drowPart = dtlPart.NewRow()
                        drowPart.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        drowPart.Item("class") = .Rows(i).Item(2).ToString().Trim()
                        drowPart.Item("child") = .Rows(i).Item(3).ToString().Trim()
                        drowPart.Item("qty") = .Rows(i).Item(4).ToString().Trim()
                        drowPart.Item("stru") = .Rows(i).Item(5).ToString().Trim()
                        drowPart.Item("rej") = .Rows(i).Item(6).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(7)) = True Then
                            drowPart.Item("start") = ""
                        Else
                            drowPart.Item("start") = .Rows(i).Item(7).ToString().Trim()
                        End If
                        If IsDBNull(.Rows(i).Item(8)) = True Then
                            drowPart.Item("end") = ""
                        Else
                            drowPart.Item("end") = .Rows(i).Item(8).ToString().Trim()
                        End If
                        drowPart.Item("pno") = .Rows(i).Item(9).ToString().Trim()
                        drowPart.Item("lt") = .Rows(i).Item(10).ToString().Trim()
                        drowPart.Item("pos") = .Rows(i).Item(11).ToString().Trim()
                        dtlPart.Rows.Add(drowPart)
                    Next
                    If Not Security("458012").isValid Then
                        BtnDelete.Enabled = False
                    Else
                        BtnDelete.Enabled = True
                    End If
                Else
                    BtnDelete.Enabled = False
                    BtnConfirm.Enabled = False
                End If
            End With
            Dim dvPart As DataView
            dvPart = New DataView(dtlPart)
            Try
                dgPart.DataSource = dvPart
                dgPart.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgPart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPart.PageIndexChanged
            dgPart.CurrentPageIndex = e.NewPageIndex()
            BindData()
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            Response.Redirect(chk.urlRand("/Qad/Item_Qad_list.aspx?code=" & Request("code") & "&qad=" & Request("qad") & "&pg=" & Request("pg")), True)
        End Sub

        Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
            strsql = " Delete From Qad_stru Where parent='" & Request("parent") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

            strsql = " Update Items Set isChecked=null, checkby=null Where item_qad='" & Request("parent") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

            Response.Redirect(chk.urlRand("/Qad/ViewQadStructure.aspx?parent=" & Request("parent") & "&code=" & Request("code") & "&qad=" & Request("qad") & "&pg=" & Request("pg")), True)
        End Sub

        Private Sub BtnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConfirm.Click
            strsql = " Update items Set isChecked=N'已核', checkBy=N'" & Session("uName") & "' Where item_qad='" & Request("parent") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

            Response.Redirect(chk.urlRand("/Qad/ViewQadStructure.aspx?parent=" & Request("parent") & "&code=" & Request("code") & "&qad=" & Request("qad") & "&pg=" & Request("pg")), True)
        End Sub

        Public Sub DelBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPart.ItemCommand
            Dim pg As Integer = dgPart.CurrentPageIndex
            If (e.CommandName.CompareTo("DelBtn") = 0) Then
                If Not Security("458012").isValid Then
                    ltlAlert.Text = "alert('没有权限删除结构！');"
                    Exit Sub
                Else
                    strsql = " Delete From Qad_Stru Where id='" & e.Item.Cells(1).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

                    Response.Redirect(chk.urlRand("/Qad/ViewQadStructure.aspx?parent=" & Request("parent") & "&code=" & Request("code") & "&qad=" & Request("qad") & "&pg=" & Request("pg")), True)
                End If
            End If
        End Sub

        Private Sub dgQAD_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPart.ItemCreated
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing ButtonColumn
                    myDeleteButton = e.Item.Cells(0)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")

                Case ListItemType.AlternatingItem
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(0)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")

                Case ListItemType.EditItem
                    Dim myDeleteButton As TableCell
                    'Where 1 is the column containing your ButtonColumn
                    myDeleteButton = e.Item.Cells(0)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗?');")
            End Select
        End Sub
    End Class
End Namespace
