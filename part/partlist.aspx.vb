'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-1
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   NA
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
    Partial Class partlist
        Inherits BasePage
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass
        Dim nRet As Integer
        Dim strSQL As String

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub



        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            If Not IsPostBack Then

                Me.Security.Register("19030101", "产品目录维护")
                Me.Security.Register("19090001", "产品部件修改历史")
            End If

            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then

                BtnAdd.Enabled = Me.Security("19070101").isValid
                BtnReplace.Enabled = Me.Security("19070101").isValid

                If Request("code") <> "" Or Request("code") <> Nothing Then
                    txtPartCode.Text = Request("code").ToString().Trim()
                End If
                If Request("cat") <> "" Or Request("cat") <> Nothing Then
                    txtCategory.Text = Request("cat").ToString().Trim()
                End If
                If Request("qad") <> "" Or Request("qad") <> Nothing Then
                    txtQad.Text = Request("qad").ToString().Trim()
                End If
                If Request("desc") <> Nothing Then
                    txtDesc.Text = Request("desc").ToString().Trim()
                End If


                radNormal.Checked = True
                If Request("st") <> Nothing Then
                    radStop.Checked = True
                    radNormal.Checked = False
                    radTry.Checked = False
                End If
                If Request("try") <> Nothing Then
                    radTry.Checked = True
                    radStop.Checked = False
                    radNormal.Checked = False
                End If
                If Request("pg") <> Nothing Then
                    DataGrid1.CurrentPageIndex = CInt(Request("pg"))
                End If

                Dim i As Integer
                For i = 0 To DataGrid1.Columns.Count - 1
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, "Select userID from profile_display where userID='" & Session("uID") & "' and moduleID='" & Session("ModuleID") & "' and disableItem=N'" & DataGrid1.Columns(i).HeaderText.Replace("<b>", "").Replace("</b>", "") & "'") > 0 Then
                        DataGrid1.Columns(i).Visible = False
                    End If
                Next

            End If

            If Session("uRole") = 1 Then
                BtnReplace.Visible = True
            Else
                BtnReplace.Visible = False
            End If

            ltlAlert.Text = "Form1.txtPartCode.focus();"
        End Sub

        Protected Overrides Sub BindGridView()
            Dim dst As DataSet
            Dim sqlstr As String
            sqlstr = "sp_part_selectPartList"
            Dim status As Integer
            Dim param(7) As SqlParameter

            If radStop.Checked = True Then
                status = 2
            End If
            If radTry.Checked = True Then
                status = 1
            End If
            If radNormal.Checked = True Then
                status = 0
            End If

            param(0) = New SqlParameter("@status", status)
            param(1) = New SqlParameter("@code", chk.sqlEncode(txtPartCode.Text.Trim.ToLower().Replace("*", "%"))) '判断长度大于0
            param(2) = New SqlParameter("@item_qad", chk.sqlEncode(txtQad.Text.Trim.ToLower().Replace("*", "%")))
            param(3) = New SqlParameter("@name", chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")))
            param(4) = New SqlParameter("@description", chk.sqlEncode(txtDesc.Text.Trim().ToLower().Replace("*", "%")))
            dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sqlstr, param)
            'dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, createSQL())

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("partID", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("item_qad", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("category", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("unitName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("minQty", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("min", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("pcode", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("TranUnit", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("TranRate", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("Tran_Rate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("createddate1", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("createdby1", System.Type.GetType("System.String")))

            Dim num As Integer
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    lblCount.Text = "数量: " & .Rows.Count.ToString()
                    Dim i As Integer
                    i = 0
                    Dim drow As DataRow
                    ' Do While (i < .Rows.Count - 1)
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("partID") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("gsort") = i + 1
                        drow.Item("code") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("description") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("category") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("unitName") = .Rows(i).Item(5).ToString().Trim()
                        drow.Item("min") = .Rows(i).Item(7).ToString().Trim()
                        drow.Item("minQty") = .Rows(i).Item(7)
                        drow.Item("TranUnit") = .Rows(i).Item(8).ToString().Trim()
                        drow.Item("TranRate") = .Rows(i).Item(9)
                        drow.Item("Tran_Rate") = Format(.Rows(i).Item(9), "#,##0.00")
                        drow.Item("createddate1") = .Rows(i).Item(10).ToString().Trim()
                        drow.Item("createdby1") = .Rows(i).Item(11).ToString().Trim()
                        drow.Item("item_qad") = .Rows(i).Item(12).ToString().Trim()
                        dtl.Rows.Add(drow)
                        num = num + 1

                    Next
                    ' Loop

                Else
                    lblCount.Text = "数量: 0"
                    'ltlAlert.Text = "alert('没有找到查询的部件！');"
                    Session("EXSQL") = Nothing
                    Session("EXTitle") = Nothing
                    Session("EXHeader") = Nothing
                End If
            End With
            Dim dvw As DataView
            dvw = New DataView(dtl)
            If (Val(dst.Tables(0).Rows.Count.ToString()) <> num) Then
                lblCount.Text = "数量: 0"
                DataGrid1.DataSource = Nothing
                DataGrid1.DataBind()
                ltlAlert.Text = "alert('查询错误，请重新查询！');"
                Exit Sub
            End If

            Try
                DataGrid1.DataSource = dvw
                DataGrid1.DataBind()
            Catch ex As Exception
                ltlAlert.Text = "alert(" & "查询错误，请联系管理员" & ex.Message & ");"
            End Try
        End Sub
        Public Sub SizeByBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            Dim pg As Integer = DataGrid1.CurrentPageIndex
            If (e.CommandName.CompareTo("SizeByBtn") = 0) Then

                If Not Me.Security("19070400").isValid Then
                    ltlAlert.Text = "alert('没有权限查看产品尺寸！');"
                    Exit Sub
                Else
                    Dim str As String = e.Item.Cells(0).Text()

                    Me.Redirect("/part/partSizeEdit.aspx?type=0&id=" & str)
                End If
            End If
        End Sub
        Public Sub EditBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            Dim pg As Integer = DataGrid1.CurrentPageIndex
            If (e.CommandName.CompareTo("EditBtn") = 0) Then

                If Not Me.Security("19070101").isValid Then
                    ltlAlert.Text = "alert('没有权限对部件进行维护！');"
                    Exit Sub
                Else
                    Dim str As String = e.Item.Cells(0).Text()
                    If radStop.Checked = True Then
                        If txtPartCode.Text.Trim.Length > 0 Then
                            If txtCategory.Text.Trim.Trim.Length > 0 Then
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true"), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&st=true"), False)
                                End If
                            Else
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&st=true"), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&st=true"), False)
                                End If
                            End If
                        Else
                            If txtCategory.Text.Trim.Trim.Length > 0 Then
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&st=true"), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&st=true"), False)
                                End If
                            Else
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&st=true"), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&qad=" & txtQad.Text.Trim() & "&st=true"), False)
                                End If
                            End If
                        End If
                    ElseIf radTry.Checked = True Then
                        If txtPartCode.Text.Trim.Length > 0 Then
                            If txtCategory.Text.Trim.Trim.Length > 0 Then
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&try=true"), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&try=true"), False)
                                End If
                            Else
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&try=true"), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&try=true"), False)
                                End If
                            End If
                        Else
                            If txtCategory.Text.Trim.Trim.Length > 0 Then
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&try=true"), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&try=true"), False)
                                End If
                            Else
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&try=true"), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&qad=" & txtQad.Text.Trim() & "&try=true"), False)
                                End If
                            End If
                        End If
                    ElseIf radNormal.Checked = True Then
                        If txtPartCode.Text.Trim.Length > 0 Then
                            If txtCategory.Text.Trim.Trim.Length > 0 Then
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" & txtCategory.Text.Trim()), False)
                                End If
                            Else
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim()), False)
                                End If
                            End If
                        Else
                            If txtCategory.Text.Trim.Trim.Length > 0 Then
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim()), False)
                                End If
                            Else
                                If pg <> 0 Then
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), False)
                                Else
                                    Response.Redirect(chk.urlRand("addpart.aspx?id=" & str & "&qad=" & txtQad.Text.Trim()), False)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End Sub

        Public Sub UsedByBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            Dim pg As Integer = DataGrid1.CurrentPageIndex
            If (e.CommandName.CompareTo("UsedByBtn") = 0) Then
                Dim str As String = e.Item.Cells(0).Text()
                If radStop.Checked = True Then
                    If txtPartCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&st=true"), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() _
                                                & "&pg=" & pg & "&st=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&st=true"), False)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() _
                                                & "&pg=" & pg & "&st=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&st=true"), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&pg=" & pg & "&st=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&st=true"), False)
                            End If
                        End If
                    End If
                ElseIf radTry.Checked = True Then
                    If txtPartCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg & "&try=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&try=true"), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() _
                                                & "&pg=" & pg & "&try=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() & "&try=true"), False)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() _
                                                & "&pg=" & pg & "&try=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() & "&try=true"), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&pg=" & pg & "&try=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&try=true"), False)
                            End If
                        End If
                    End If
                ElseIf radNormal.Checked = True Then
                    If txtPartCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim()), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim() _
                                                & "&pg=" & pg), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&code=" & txtPartCode.Text.Trim()), False)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim() _
                                                & "&pg=" & pg), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&cat=" & txtCategory.Text.Trim()), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str & "&pg=" & pg), False)
                            Else
                                Response.Redirect(chk.urlRand("partUsedByList.aspx?id=" & str), False)
                            End If
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGridView()
        End Sub

        Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
            Dim pg As Integer = DataGrid1.CurrentPageIndex

            If Not Me.Security("19070101").isValid Then
                ltlAlert.Text = "alert('没有权限添加部件！');"
                Exit Sub
            Else
                If radStop.Checked = True Then
                    If txtPartCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" _
                                                & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" _
                                                & txtCategory.Text.Trim() & "&st=true"), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&st=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&st=true"), False)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&st=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&st=true"), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?pg=" & pg & "&st=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?st=true"), False)
                            End If
                        End If
                    End If
                ElseIf radTry.Checked = True Then
                    If txtPartCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" _
                                                & txtCategory.Text.Trim() & "&pg=" & pg & "&try=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&cat=" _
                                                & txtCategory.Text.Trim() & "&try=true"), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&try=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&try=true"), False)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg & "&try=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&try=true"), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?pg=" & pg & "&try=true"), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?try=true"), False)
                            End If
                        End If
                    End If
                ElseIf radNormal.Checked = True Then
                    If txtPartCode.Text.Trim.Length > 0 Then
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim() & "&pg=" & pg), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() _
                                                & "&cat=" & txtCategory.Text.Trim()), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?code=" & txtPartCode.Text.Trim() & "&qad=" & txtQad.Text.Trim()), False)
                            End If
                        End If
                    Else
                        If txtCategory.Text.Trim.Trim.Length > 0 Then
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim() & "&pg=" & pg), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx?cat=" & txtCategory.Text.Trim() & "&qad=" & txtQad.Text.Trim()), False)
                            End If
                        Else
                            If pg <> 0 Then
                                Response.Redirect(chk.urlRand("addpart.aspx?pg=" & pg), False)
                            Else
                                Response.Redirect(chk.urlRand("addpart.aspx"), False)
                            End If
                        End If
                    End If
                End If
            End If
        End Sub

        Public Sub DocByBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DocByBtn") = 0) Then
                ltlAlert.Text = "var w=window.open('/qaddoc/qad_documentsearchbyitem.aspx?type=0&id=" & e.Item.Cells(0).Text() & "&code=" & Server.UrlEncode(e.Item.Cells(2).Text()) & "','','menubar=0,scrollbars=1,resizable=1,width=800,height=600,top=0,left=0');w.focus();"
            End If
        End Sub

        Public Sub HisBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("HisBtn") = 0) Then
                ltlAlert.Text = "var w=window.open('/product/producthistory.aspx?id=" & e.Item.Cells(0).Text() & "&rm=" & DateTime.Now & "','','menubar=0,scrollbars=1,resizable=1,width=800,height=600,top=0,left=0');w.focus();"
            End If
        End Sub

        Private Sub BtnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQuery.Click
            ltlAlert.Text = ""
            DataGrid1.CurrentPageIndex = 0
            BindGridView()
        End Sub

        Private Sub BtnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReplace.Click
            Dim pg As Integer = DataGrid1.CurrentPageIndex

            If Not Me.Security("19070101").isValid Then
                ltlAlert.Text = "alert('没有权限进行部件维护！');"
                Exit Sub
            Else
                If radStop.Checked = True Then
                    ltlAlert.Text = "var w=window.open('/part/partreplace.aspx?code=" & txtPartCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() _
                                  & "&pg=" & pg & "&st=true','','menubar=0,scrollbars=0,resizable=0,width=500,height=350,top=300,left=300');w.focus(); "
                ElseIf radTry.Checked = True Then
                    ltlAlert.Text = "var w=window.open('/part/partreplace.aspx?code=" & txtPartCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() _
                                  & "&pg=" & pg & "&try=true','','menubar=0,scrollbars=0,resizable=0,width=500,height=350,top=300,left=300');w.focus(); "
                Else
                    ltlAlert.Text = "var w=window.open('/part/partreplace.aspx?code=" & txtPartCode.Text.Trim() & "&cat=" & txtCategory.Text.Trim() _
                                  & "&pg=" & pg & "','','menubar=0,scrollbars=0,resizable=0,width=500,height=350,top=300,left=300');w.focus(); "
                End If
            End If
        End Sub

        Public Sub SupplyBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("SupplyBtn") = 0) Then

                If Not Me.Security("19023311").isValid Then
                    ltlAlert.Text = "alert('没有权限查看供应商！');"
                    Exit Sub
                Else
                    ltlAlert.Text = "var w=window.open('/supply/partsupplylinks.aspx?id=" & e.Item.Cells(0).Text.Trim() & "','供货','menubar=0,scrollbars=0,resizable=0,width=780,height=550,top=100,left=40');w.focus(); "
                End If
            End If
        End Sub

        Private Sub radNormal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radNormal.CheckedChanged
            DataGrid1.CurrentPageIndex = 0
            BindGridView()
        End Sub

        Private Sub radStop_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radStop.CheckedChanged
            DataGrid1.CurrentPageIndex = 0
            BindGridView()
        End Sub

        Private Sub radTry_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radTry.CheckedChanged
            DataGrid1.CurrentPageIndex = 0
            BindGridView()
        End Sub

        Public Sub ToProdBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            Dim pg As Integer = DataGrid1.CurrentPageIndex
            If (e.CommandName.CompareTo("ToProdBtn") = 0) Then

                If Not Me.Security("19030101").isValid Then
                    ltlAlert.Text = "alert('没有权限对部件进行转换！');"
                    Exit Sub
                Else
                    strSQL = " Update Items Set type = 2 Where id ='" & e.Item.Cells(0).Text() & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
                    If radStop.Checked = True Then
                        Response.Redirect(chk.urlRand("partlist.aspx?code=" & txtPartCode.Text.Trim() & "&cat=" _
                                        & txtCategory.Text.Trim() & "&pg=" & pg & "&st=true"), False)
                    ElseIf radTry.Checked = True Then
                        Response.Redirect(chk.urlRand("partlist.aspx?code=" & txtPartCode.Text.Trim() & "&cat=" _
                                        & txtCategory.Text.Trim() & "&pg=" & pg & "&try=true"), False)
                    ElseIf radNormal.Checked = True Then
                        Response.Redirect(chk.urlRand("partlist.aspx?code=" & txtPartCode.Text.Trim() & "&cat=" _
                                        & txtCategory.Text.Trim() & "&pg=" & pg), False)
                    End If
                End If
            End If
        End Sub

        Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.EditItem Then

                If Not Me.Security("19030101").isValid Then
                    DataGrid1.Columns(14).Visible = False
                Else
                    Dim myTransButton As TableCell
                    myTransButton = e.Item.Cells(14)
                    myTransButton.Attributes.Add("onclick", "if(confirm(‘确定要转换为产品吗?’)){client_confirmed = true;} else{ client_confirmed = false; return false;}")
                End If

                e.Item.Cells(9).Enabled = Me.Security("19070101").isValid
                e.Item.Cells(11).Enabled = Me.Security("19023311").isValid
                e.Item.Cells(14).Enabled = Me.Security("19030101").isValid
                e.Item.Cells(15).Enabled = Me.Security("19090001").isValid

            End If
        End Sub

        Protected Sub ButExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButExcel.Click
            Dim EXTitle As String = "200^<b>部件号</b>~^500^<b>部件描述</b>~^70^<b>分类</b>~^70^<b>单位</b>~^70^<b>状态</b>~^100^<b>最小库存量</b>~^100^<b>转换前单位</b>~^100^<b>转换系数</b>~^<b>创建日期</b>~^<b>创建人</b>~^<b>部件QAD号</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsn0(), EXTitle, ExSql, False)
        End Sub

        Function createSQL() As String
            strSQL = " Select top 1000 i.id, i.itemNumber, i.code, isnull(i.description,''), isnull(ic.name,''), isnull(i.unit,''), i.status, Isnull(i.min_inv,0), Isnull(i.tranUnit,''), Isnull(i.tranRate,0),i.createdDate,isnull(u.username,'') ,i.item_qad" _
                   & " From tcpc0.dbo.Items i Left Outer Join tcpc0.dbo.ItemCategory ic ON i.category = ic.id Left Outer Join tcpc0.dbo.users u ON u.userid = i.createdby  Where i.type=0 "
            If radStop.Checked = True Then
                strSQL &= " And i.status=2 "
            End If
            If radTry.Checked = True Then
                strSQL &= " And i.status=1 "
            End If
            If radNormal.Checked = True Then
                strSQL &= " And i.status=0 "
            End If

            If txtPartCode.Text.Trim.Length > 0 Then
                strSQL = strSQL & " and Lower(i.code) like N'" & chk.sqlEncode(txtPartCode.Text.Trim.ToLower().Replace("*", "%")) & "'"
            End If

            If txtQad.Text.Trim.Length > 0 Then
                strSQL = strSQL & " and Lower(i.item_qad) like N'" & chk.sqlEncode(txtQad.Text.Trim.ToLower().Replace("*", "%")) & "'"
            End If

            If txtCategory.Text.Trim.Length > 0 Then
                strSQL = strSQL & " and Lower(ic.name) like N'" & chk.sqlEncode(txtCategory.Text.Trim().ToLower().Replace("*", "%")) & "'"
            End If

            If txtDesc.Text.Trim.Length > 0 Then
                strSQL = strSQL & " and Lower(i.description) Like N'%" & chk.sqlEncode(txtDesc.Text.Trim().ToLower()) & "%'"
            End If

            createSQL = strSQL
        End Function

    End Class

End Namespace
