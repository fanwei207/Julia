'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-08-07
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Item Qad Replace Code
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class ViewQadReplace
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
            ltlAlert.Text = ""
            If Not IsPostBack Then
                If Not Security("458015").isValid Then
                    BtnDel.Enabled = False
                Else
                    BtnDel.Enabled = True
                End If
                lblProdCode.Text = "QAD零件号为&nbsp;<font size='4pt'>" & Request("parent") & "</font>&nbsp;的结构替代零件&nbsp;&nbsp;&nbsp;操作人：" & SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, " Select Distinct createdBy From Qad_Stru_Replace Where parent='" & Request("parent") & "'")
                BindData()

                Dim i As Integer
                For i = 0 To dgQAD.Columns.Count - 1
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, "Select userID from profile_display where userID='" & Session("uID") & "' and moduleID='" & Session("ModuleID") & "' and disableItem=N'" & dgQAD.Columns(i).HeaderText.Replace("<b>", "").Replace("</b>", "") & "'") > 0 Then
                        dgQAD.Columns(i).Visible = False
                    End If
                Next
            End If
        End Sub

        Sub BindData()
            Dim dst As DataSet
            Session("EXTitle") = "200^<b>父零件</b>~^250^<b>项目号(被替代的物料号)</b>~^200^<b>替代零件</b>~^150^<b>替代数量</b>~^150^<b>备注</b>~^500^<b>说明</b>~^"

            strSQL = " Select parent, oldchild, newchild, qty, Isnull(notes,''), Isnull(memo,'') From tcpc0.dbo.Qad_Stru_Replace Where parent='" & Request("parent") & "'"

            Session("EXSQL") = strSQL
            Session("EXHeader") = ""

            dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("oldchild", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("newchild", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("qty", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("notes", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("memo", System.Type.GetType("System.String")))

            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    lblCount.Text = "数量: " & .Rows.Count.ToString()
                    Dim i As Integer
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("gsort") = i + 1
                        drow.Item("oldchild") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("newchild") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("qty") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("notes") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("memo") = .Rows(i).Item(5).ToString().Trim()
                        dtl.Rows.Add(drow)
                    Next
                Else
                    lblCount.Text = "数量: 0"
                    Session("EXSQL") = Nothing
                    Session("EXTitle") = Nothing
                End If
            End With
            Dim dvw As DataView
            dvw = New DataView(dtl)
            Try
                dgQAD.DataSource = dvw
                dgQAD.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgQAD_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgQAD.SortCommand
            Session("orderby") = e.SortExpression.ToString()
            If Session("orderdir") = "ASC" Then
                Session("orderdir") = "DESC"
            Else
                Session("orderdir") = "ASC"
            End If
            BindData()
        End Sub

        Private Sub dgQAD_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgQAD.PageIndexChanged
            dgQAD.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Public Sub BtnDel_Click(ByVal source As Object, ByVal e As System.EventArgs) Handles BtnDel.Click
            strSQL = " Delete From Qad_Stru_Replace Where parent='" & Request("parent") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

            Response.Redirect(chk.urlRand("/Qad/ViewQadReplace.aspx?parent=" & Request("parent") & "&code=" & Request("code") & "&qad=" & Request("qad") & "&pg=" & Request("pg")), True)
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            Response.Redirect(chk.urlRand("/Qad/Item_Qad_list.aspx?code=" & Request("code") & "&qad=" & Request("qad") & "&pg=" & Request("pg")), True)
        End Sub

    End Class

End Namespace
