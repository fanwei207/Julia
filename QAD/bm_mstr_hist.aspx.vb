'*@     Create By   :   Ye Bin    
'*@     Create Date :   2009-1-21
'*@     Modify By   :   NA
'*@     Modify Date :   
'*@     Function    :   BOM_MSTR History List
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc


    Partial Class bm_mstr_hist
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim dst As DataSet
        Dim nRet As Integer

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
                BindData()
            End If
        End Sub

        Sub BindData()
            StrSql = "select bm_mstr_id, bm_desc, bm_comment, bm_closedDate, Isnull(bm_closedName,''), bm_deletedDate, Isnull(bm_deletedName,'') from bm_mstr where Isnull(bm_deletedBy,'') <> '' or Isnull(bm_closedby,'') <> '' order by bm_createdDate"
            dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, StrSql)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("bm_id", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("bm_status", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("dd", System.Type.GetType("System.DateTime")))
            dtl.Columns.Add(New DataColumn("ed", System.Type.GetType("System.DateTime")))
            dtl.Columns.Add(New DataColumn("ddate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("edate", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("bmdesc", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("bmcommet", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("closeby", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("deleteby", System.Type.GetType("System.String")))

            Dim drow As DataRow
            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("bm_id") = .Rows(i).Item(0).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(5)) = False Then
                            drow.Item("ddate") = Format(.Rows(i).Item(5), "yy-MM-dd")
                            drow.Item("dd") = .Rows(i).Item(5)
                        Else
                            drow.Item("ddate") = ""
                            drow.Item("dd") = "1900-1-1"
                        End If
                        If IsDBNull(.Rows(i).Item(3)) = False Then
                            drow.Item("edate") = Format(.Rows(i).Item(3), "yy-MM-dd")
                            drow.Item("ed") = .Rows(i).Item(3)
                        Else
                            drow.Item("edate") = ""
                            drow.Item("ed") = "1900-1-1"
                        End If
                        drow.Item("bmdesc") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("bmcommet") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("closeby") = .Rows(i).Item(4).ToString().Trim()
                        drow.Item("deleteby") = .Rows(i).Item(6).ToString().Trim()

                        If .Rows(i).Item(6) <> "" Then
                            drow.Item("bm_status") = "ÒÑÉ¾³ý"
                        End If

                        If .Rows(i).Item(4) <> "" Then
                            drow.Item("bm_status") = "ÒÑ¹Ø±Õ"
                        End If

                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            dst.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)
            Try
                dgBM.DataSource = dvw
                dgBM.DataBind()
            Catch
            End Try
        End Sub

        Protected Sub dgBM_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgBM.PageIndexChanged
            dgBM.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub
    End Class

End Namespace
