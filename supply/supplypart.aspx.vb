'*@     Create By   :   Nai Qi
'*@     Create Date :   2005-6-18
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Supplier - Part Link
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class supplypart
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents Button3 As System.Web.UI.WebControls.Button
        Shared sortDir As String = "ASC"
        Shared sortOrder As String = ""

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

        Private Sub BindData()
            Dim Query As String
            Dim Reader As DataSet
            Query = "Select c.company_id,isNull(c.company_code,''),isNull(c.company_name,''),isNull(c.phone,''),"
            Query = Query & " isNull(c.fax,'') , isNull(c.contact,''), ISNULL(lk.total, 0)"
            Query = Query & " From tcpc0.dbo.companies c inner join tcpc0.dbo.systemCode sc on c.company_type=sc.systemCodeID and sc.SystemCodeName=N'供应商' Inner Join tcpc0.dbo.systemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID and systemCodeTypeName='Company Type' LEFT OUTER JOIN (SELECT companyID, COUNT(*) AS total FROM tcpc0.dbo.company_part GROUP BY companyID) lk ON lk.companyID = c.company_id where c.active=1 "
            If Textbox1.Text.Trim.Length > 0 Then
                Query = Query & " and lower(company_code) like N'%" & Textbox1.Text.Trim.ToLower() & "%' "
            End If
            If Textbox2.Text.Trim.Length > 0 Then
                Query = Query & " and lower(company_name) like N'%" & Textbox2.Text.Trim.ToLower() & "%' "
            End If
            Query = Query & " order by c.company_name desc"

            Session("EXSQL") = Query
            Session("EXTitle") = "<b>编号</b>~^200^<b>公司名称</b>~^100^<b>电话</b>~^100^<b>传真</b>~^<b>联系人</b>~^<b>材料种类数</b>~^"

            'Response.Write(Query)

            Reader = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("number", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("gCampanyID", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gCode", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gPhone", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gFax", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gContactname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gLink", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gLink1", System.Type.GetType("System.Int32")))

            With Reader.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dtl.NewRow()
                        dr1.Item("number") = i + 1
                        dr1.Item("gCampanyID") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("gCode") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("gName") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("gPhone") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("gFax") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("gContactname") = .Rows(i).Item(5).ToString().Trim()
                        If Security("19023302").isValid Then
                            dr1.Item("gLink") = "<a href='javascript:myopen(0);'>" & .Rows(i).Item(6).ToString().Trim() & "</a>"
                        Else
                            dr1.Item("gLink") = "<a href='javascript:myopen(" & .Rows(i).Item(0).ToString().Trim() & ");'>" & .Rows(i).Item(6).ToString().Trim() & "</a>"
                        End If
                        dr1.Item("gLink1") = .Rows(i).Item(6)
                        dtl.Rows.Add(dr1)
                    Next
                End If
            End With
            Reader.Reset()

            Dim dvw As DataView
            dvw = New DataView(dtl)
            If (sortOrder.Length <= 0) Then
                sortOrder = "number"
            End If
            Try
                dvw.Sort = sortOrder & " " & sortDir
                dgSupply.DataSource = dvw
                dgSupply.DataBind()
            Catch
            End Try
        End Sub
        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            BindData()
        End Sub
        Private Sub dgSupply_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSupply.SortCommand
            sortOrder = e.SortExpression.ToString()
            If sortDir = "ASC" Then
                sortDir = "DESC"
            Else
                sortDir = "ASC"
            End If
            BindData()
        End Sub

        Protected Sub dgSupply_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSupply.PageIndexChanged
            dgSupply.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

    End Class

End Namespace
