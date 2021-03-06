'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-07-04
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Item Qad Code 
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class Item_Qad_List
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
            If Not IsPostBack Then
                Me.Security.Register("458011", "QAD产品删除")
                Me.Security.Register("458013", "QAD产品结构确认")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then
                If Request("code") <> "" Or Request("code") <> Nothing Then
                    txtCode.Text = Server.UrlDecode(Request("code").ToString().Trim())
                End If
                If Request("qad") <> "" Or Request("qad") <> Nothing Then
                    txtQad.Text = Request("qad").ToString().Trim()
                End If
                If Request("pg") <> Nothing Then
                    dgQAD.CurrentPageIndex = CInt(Request("pg"))
                End If
                BindData()

                Dim i As Integer
                For i = 0 To dgQAD.Columns.Count - 1
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, "Select userID from profile_display where userID='" & Session("uID") & "' and moduleID='" & Session("ModuleID") & "' and disableItem=N'" & dgQAD.Columns(i).HeaderText.Replace("<b>", "").Replace("</b>", "") & "'") > 0 Then
                        dgQAD.Columns(i).Visible = False
                    End If
                Next

            End If
            ltlAlert.Text = "Form1.txtQad.focus();"
        End Sub

        Sub BindData()

            Dim dst As DataSet

            Session("EXTitle") = "250^<b>旧编号</b>~^200^<b>QAD编号</b>~^150^<b>QAD描述1</b>~^150^<b>QAD描述2</b>~^100^<b>结构BOM(已做/未做)</b>~^80^<b>BOM制作人</b>~^"

            strSQL = " Select i.id, Isnull(i.code,''), i.item_qad, Isnull(i.item_qad_desc1,''), Isnull(item_qad_desc2,''), Isnull(isChecked,N'未做'), Isnull(checkBy,'--') " _
                   & " From tcpc0.dbo.Items i " _
                   & " Where i.item_qad is not null "
            If NotChk.Checked = True Then
                strSQL &= " And i.isChecked Is Null "
                'Else
                '    strSQL &= " And i.isChecked Is Not Null "
            End If

            If txtQad.Text.Trim.Length > 0 Then
                strSQL = strSQL & " and Lower(i.item_qad) like '" & chk.sqlEncode(txtQad.Text.Trim().ToLower().Replace("*", "%")) & "'"
            End If

            If txtCode.Text.Trim.Length > 0 Then
                strSQL = strSQL & " and Lower(i.code) like N'" & chk.sqlEncode(txtCode.Text.Trim().ToLower().Replace("*", "%")) & "'"
            End If
            strSQL = strSQL & " Order By i.item_qad "
            Session("EXSQL") = strSQL
            Session("EXHeader") = ""

            dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("qad", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("desc1", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("desc2", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("Chk", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("ChkBy", System.Type.GetType("System.String")))

            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    lblCount.Text = "数量: " & .Rows.Count.ToString()
                    Dim i As Integer
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("gsort") = i + 1
                        drow.Item("qad") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("code") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("desc1") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("desc2") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("Chk") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("ChkBy") = .Rows(i).Item(6).ToString().Trim()
                        dtl.Rows.Add(drow)
                    Next
                Else
                    lblCount.Text = "数量: 0"
                    Session("EXSQL") = Nothing
                    Session("EXTitle") = Nothing
                End If
            End With

            Try
                dgQAD.DataSource = dtl
                dgQAD.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgQAD_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgQAD.PageIndexChanged
            dgQAD.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Public Sub StruBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgQAD.ItemCommand
            Dim pg As Integer = dgQAD.CurrentPageIndex
            Dim params(0) As SqlParameter
            Dim param(1) As SqlParameter
            If (e.CommandName.CompareTo("StruBtn") = 0) Then
                strSQL = " Delete From ImportError Where userID=0 And plantID=0 "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                strSQL = " Select Count(*) From items Where id='" & e.Item.Cells(0).Text.Trim() & "' And type=0 "
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL) > 0 Then
                    ltlAlert.Text = "alert('这是材料没有结构！');"
                    Exit Sub
                Else
                    strSQL = "Check_QAD_Structure"
                    params(0) = New SqlParameter("@pid", Convert.ToInt32(e.Item.Cells(0).Text.Trim()))
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, strSQL, params)

                    If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, "Select Count(*) From ImportError Where userID=1 And plantID=0") > 0 Then
                        ltlAlert.Text = "alert('结构中存在没有QAD编号的零件！'); window.location.href='" & chk.urlRand("/Qad/ItemQadStructure.aspx?id=" _
                                    & e.Item.Cells(0).Text.Trim() & "&code=" & Server.UrlEncode(txtCode.Text.Trim()) & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&rm=" & DateTime.Now()) & "';"
                    Else
                        Response.Redirect(chk.urlRand("/Qad/ItemQadStructure.aspx?id=" & e.Item.Cells(0).Text.Trim() & "&code=" & Server.UrlEncode(txtCode.Text.Trim()) & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), True)
                    End If
                End If
            End If
        End Sub

        Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQuery.Click
            dgQAD.CurrentPageIndex = 0
            BindData()
        End Sub

        Private Sub BtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCheck.Click

            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, "Select Count(*) From ImportError Where userID=1 And plantID=0") > 0 Then
                Session("EXTitle") = "500^<b>错误原因</b>~^"
                Session("EXHeader") = ""
                Session("EXSQL") = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID=1 And plantID=0"
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
            End If
        End Sub

        Private Sub BtnUnique_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUnique.Click
            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, "Select Count(item_qad) From Items Where item_qad Is Not Null Group By item_qad Having Count(item_qad)>1") <> Nothing Then
                ltlAlert.Text = "alert('系统中存在重复的QAD编号！');"
            Else
                strSQL = " Delete From ImportError Where userID=0 And plantID=0 "
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, "CheckItemQad")
                If (SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, "Select Count(*) From ImportError Where userID=0 And PlantID=0")) > 0 Then
                    Session("EXTitle") = "500^<b>错误原因</b>~^"
                    Session("EXHeader") = ""
                    Session("EXSQL") = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID=0 And plantID=0"
                    ltlAlert.Text = "alert('系统中存在比当前编号新的尚未有QAD编号的零件！');window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
                Else
                    ltlAlert.Text = "alert('检查合格！');"
                End If
            End If
        End Sub

        Public Sub QADStruBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgQAD.ItemCommand
            Dim pg As Integer = dgQAD.CurrentPageIndex
            If (e.CommandName.CompareTo("QADStruBtn") = 0) Then
                Response.Redirect(chk.urlRand("/Qad/ViewQadStructure.aspx?parent=" & e.Item.Cells(14).Text.Trim() & "&code=" & Server.UrlEncode(txtCode.Text.Trim()) & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), True)
            End If
        End Sub

        Public Sub DelBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgQAD.ItemCommand
            Dim pg As Integer = dgQAD.CurrentPageIndex
            If (e.CommandName.CompareTo("DelBtn") = 0) Then
                If Not Me.Security("458011").isValid Then
                    ltlAlert.Text = "alert('没有权限删除QAD产品名称！');"
                    Exit Sub
                Else
                    strSQL = " Delete From item_qad_tmp Where item_qad='" & e.Item.Cells(14).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                    strSQL = " Delete From item_qad_tmp13 Where item_qad='" & e.Item.Cells(14).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                    strSQL = " Delete From item_qad_tmp112 Where item_qad='" & e.Item.Cells(14).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                    strSQL = " Delete From item_qad_tmp132 Where item_qad='" & e.Item.Cells(14).Text.Trim() & "'" '"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                    strSQL = " Delete From item_qad_tmp134 Where item_qad='" & e.Item.Cells(14).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                    strSQL = " Update items Set item_qad_desc1=null, item_qad_desc2=null, item_qad=null Where item_qad='" & e.Item.Cells(14).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                    Response.Redirect(chk.urlRand("/Qad/Item_Qad_list.aspx?code=" & chk.sqlEncode(txtCode.Text.Trim()) & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), True)
                End If
            End If
        End Sub

        Public Sub RepBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgQAD.ItemCommand
            Dim pg As Integer = dgQAD.CurrentPageIndex
            If (e.CommandName.CompareTo("RepBtn") = 0) Then
                Response.Redirect(chk.urlRand("/Qad/ViewQadReplace.aspx?parent=" & e.Item.Cells(14).Text.Trim() & "&code=" & Server.UrlEncode(txtCode.Text.Trim()) & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), True)
            End If
        End Sub

        Public Sub AlterBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgQAD.ItemCommand
            Dim pg As Integer = dgQAD.CurrentPageIndex
            If (e.CommandName.CompareTo("AlterBtn") = 0) Then
                Response.Redirect(chk.urlRand("/Qad/ViewAlterQadStru.aspx?parent=" & e.Item.Cells(14).Text.Trim() & "&code=" & Server.UrlEncode(txtCode.Text.Trim()) & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), True)
            End If
        End Sub

        Private Sub StruExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StruExport.Click
            Session("EXTitle") = "150^<b>父零件</b>~^50^<b>级</b>~^150^<b>子零件</b>~^150^<b>单位耗用量</b>~^150^<b>产品结构类型</b>~^80^<b>废品率</b>~^80^<b>生效日期</b>~^80^<b>终止日期</b>~^60^<b>工序号</b>~^100^<b>LT Offset</b>~^"
            Session("EXHeader") = ""
            If txtQad.Text.Trim().Length > 0 Then
                Session("EXSQL") = "Select parent, Isnull(class,''), child, qty, Isnull(stru_type,''), Isnull(rej_rate,0), startdate, enddate, Isnull(processNo,''), Isnull(lt,'') From tcpc0.dbo.Qad_Stru Where parent like '" & txtQad.Text.Trim().Replace("*", "%") & "' Order By parent "
            Else
                Session("EXSQL") = "Select parent, Isnull(class,''), child, qty, Isnull(stru_type,''), Isnull(rej_rate,0), startdate, enddate, Isnull(processNo,''), Isnull(lt,'') From tcpc0.dbo.Qad_Stru Order By parent "
            End If
            ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
        End Sub

        Private Sub RepExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RepExport.Click
            Session("EXTitle") = "200^<b>父零件</b>~^250^<b>项目号(被替代的物料号)</b>~^200^<b>替代零件</b>~^150^<b>替代数量</b>~^150^<b>备注</b>~^500^<b>说明</b>~^"
            Session("EXHeader") = ""
            Session("EXSQL") = " Select parent, oldchild, newchild, qty, Isnull(notes,''), Isnull(memo,'') From tcpc0.dbo.Qad_Stru_Replace Order By parent "
            ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
        End Sub

        Private Sub AlterExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlterExport.Click
            Session("EXTitle") = "200^<b>物料号码</b>~^300^<b>替代产品结构代码</b>~^150^<b>参考</b>~^150^<b>备注</b>~^"
            Session("EXHeader") = ""
            Session("EXSQL") = " Select qad_code, alter_qad, Isnull(refer,''), Isnull(notes,'') From tcpc0.dbo.Alter_Qad_Stru Order By qad_code "
            ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
        End Sub

        Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExport.Click
            Session("EXTitle") = "150^<b>父零件</b>~^150^<b>父零件QAD</b>~^150^<b>父零件QAD描述1</b>~^150^<b>父零件QAD描述2</b>~^150^<b>子零件</b>~^150^<b>子零件QAD</b>~^150^<b>子零件QAD描述1</b>~^150^<b>子零件QAD描述2</b>~^150^<b>单位耗用量</b>~^150^<b>备注</b>~^280^<b>位号</b>~^"
            Session("EXHeader") = ""
            If txtQad.Text.Trim().Length > 0 Then
                Session("EXSQL") = " Select pi.code, pi.item_qad, pi.item_qad_desc1, pi.item_qad_desc2, ci.code, ci.item_qad, ci.item_qad_desc1, ci.item_qad_desc2, ps.numOfChild, ps.notes, ps.posCode " _
                                 & " From tcpc0.dbo.Product_stru ps Inner Join tcpc0.dbo.Items pi ON ps.productID=pi.id Inner Join tcpc0.dbo.Items ci ON ps.childID=ci.id Where pi.item_qad like '" & txtQad.Text.Trim().Replace("*", "%") & "' Order By pi.code "
            Else
                Session("EXSQL") = " Select pi.code, pi.item_qad, pi.item_qad_desc1, pi.item_qad_desc2, ci.code, ci.item_qad, ci.item_qad_desc1, ci.item_qad_desc2, ps.numOfChild, ps.notes, ps.posCode " _
                                 & " From tcpc0.dbo.Product_stru ps Inner Join tcpc0.dbo.Items pi ON ps.productID=pi.id Inner Join tcpc0.dbo.Items ci ON ps.childID=ci.id Order By pi.code "
            End If
            ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
        End Sub

        Public Sub UserByBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgQAD.ItemCommand
            If (e.CommandName.CompareTo("UserByBtn") = 0) Then
                Session("EXTitle") = "200^<b>父零件</b>~^"
                Session("EXHeader") = "子零件~" & e.Item.Cells(14).Text.Trim() & "~^"
                Session("EXSQL") = " Select Distinct parent From tcpc0.dbo.Qad_stru Where child='" & e.Item.Cells(14).Text.Trim() & "'"
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
            End If
        End Sub

        Public Sub QADBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgQAD.ItemCommand
            Dim pg As Integer = dgQAD.CurrentPageIndex
            If (e.CommandName.CompareTo("QADBtn") = 0) Then
                Response.Redirect(chk.urlRand("/Qad/ViewQadStru.aspx?parent=" & e.Item.Cells(14).Text.Trim() & "&code=" & Server.UrlEncode(txtCode.Text.Trim()) & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), True)
            ElseIf (e.CommandName.CompareTo("CompBtn") = 0) Then
                Response.Redirect("/QAD/exportExcelQADCompare.aspx?part=" & e.Item.Cells(14).Text.Trim())
            ElseIf (e.CommandName.CompareTo("StatusBtn") = 0) Then
                If Not Me.Security("458013").isValid Then
                    ltlAlert.Text = "alert('没有权限确认已做结构！');"
                    Exit Sub
                Else
                    strSQL = " Update items Set isChecked=N'已做', checkBy=N'" & Session("uName") & "' Where item_qad='" & e.Item.Cells(14).Text.Trim() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                    Response.Redirect(chk.urlRand("/Qad/Item_Qad_list.aspx?code=" & chk.sqlEncode(txtCode.Text.Trim()) & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), True)
                End If
            End If
        End Sub
    End Class

End Namespace
