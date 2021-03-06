'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-4-28
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Item Replace Code
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class ItemReplace
        Inherits System.Web.UI.Page
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strsql As String

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

                If Request("psid") <> Nothing Then
                    strsql = " Select i.code From Items i Inner Join Product_Stru ps ON i.id=ps.childID Where ps.productStruID='" & Request("psid") & "'"
                    lblTitle.Text = "修改编号为<font size='4pt'>" & SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) & "</font>的替代品"
                End If
                BindData()
            End If
            ltlAlert.Text = "Form1.txtItem.focus();"
        End Sub

        Sub BindData()
            Dim dst As DataSet
            strsql = " Select pr.prodreplaceID, i.code From product_replace pr Inner Join Items i On pr.itemID_temp=i.id Where pr.itemID_temp<>0 And pr.prodStruID='" & Request("psid") & "'"

            dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)

            Dim dtlReplace As New DataTable
            dtlReplace.Columns.Add(New DataColumn("replaceID", System.Type.GetType("System.String")))
            dtlReplace.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dtlReplace.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))

            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    Dim drowReplace As DataRow
                    For i = 0 To .Rows.Count - 1
                        drowReplace = dtlReplace.NewRow()
                        drowReplace.Item("replaceID") = .Rows(i).Item(0).ToString().Trim()
                        drowReplace.Item("code") = .Rows(i).Item(1).ToString().Trim()
                        drowReplace.Item("gsort") = i + 1
                        dtlReplace.Rows.Add(drowReplace)
                    Next
                End If
            End With
            Dim dvPart As DataView
            dvPart = New DataView(dtlReplace)

            Try
                dgReplace.DataSource = dvPart
                dgReplace.DataBind()
            Catch
            End Try
        End Sub

        Private Sub DeleteBtn_Click(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgReplace.ItemCommand
            If e.CommandName.CompareTo("DeleteBtn") = 0 Then
                strsql = " Update product_replace Set itemID_temp=0 Where prodreplaceID='" & e.Item.Cells(0).Text.Trim() & "'"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

                Response.Redirect(chk.urlRand("/product/ItemReplace.aspx?psid=" & Request("psid") & "&id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st")), True)
            End If
        End Sub

        Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
            Dim itemID As String = Nothing
            strsql = " Select ID From Items Where Code=N'" & chk.sqlEncode(txtItem.Text.Trim()) & "' And status<>2 "
            itemID = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)
            If itemID = Nothing Or itemID = "0" Then
                ltlAlert.Text = "alert('输入的" & txtItem.Text.Trim() & "不存在！');"
                Exit Sub
            Else
                strsql = " Insert Into product_replace(prodStruID, itemID_temp) Values('" & Request("psid") & "','" & itemID.Trim() & "')"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)

                Response.Redirect(chk.urlRand("/product/ItemReplace.aspx?psid=" & Request("psid") & "&id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st")), True)
            End If
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=" & Request("semi") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st")), True)
        End Sub
    End Class

End Namespace
