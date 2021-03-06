'*@     Create By   :   Ye Bin
'*@     Create Date :   2005-4-7
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Supplier Add or Modify or delete 
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class supplylist
        Inherits BasePage
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents Button3 As System.Web.UI.WebControls.Button

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

                Session("orderby") = ""
                Session("orderdir") = ""

                Dim strSQL As String
                Dim ds As DataSet
                Dim item As ListItem
                strSQL = "  SELECT s.systemCodeID,  s.systemCodeName  FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID "
                If Session("uRole") <> 1 Then
                    strSQL &= " Inner Join tcpc0.dbo.User_SupplyClient uw On uw.systemCodeID = s.systemCodeID and uw.userID='" & Session("uID") & "'"
                End If
                strSQL &= " WHERE (st.systemCodeTypeName = 'Company Type'  ) ORDER BY s.systemCodeID  "
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            DropDownList1.Items.Add(item)
                        Next
                    End If
                End With
                ds.Reset()

                If Request("companyType") <> Nothing Then
                    DropDownList1.SelectedValue = Request("companyType")
                End If

                BindData()
            End If
        End Sub

        Private Sub BindData()
            Dim Query As String
            Dim Reader As DataSet
            Query = "Select c.company_id,isNull(c.company_code,''),isNull(c.company_name,''),isNull(c.street,''),isNull(c.zip,''),isNull(c.phone,''),"
            Query = Query & " isNull(c.fax,'') , isNull(c.contact,'') , c.active "
            Query = Query & " From companies c where c.company_type='" & DropDownList1.SelectedValue & "'"
            If Textbox1.Text.Trim.Length > 0 Then
                Query = Query & " and lower(company_code) like N'%" & Textbox1.Text.Trim.ToLower() & "%' "
            End If
            If Textbox2.Text.Trim.Length > 0 Then
                Query = Query & " and lower(company_name) like N'%" & Textbox2.Text.Trim.ToLower() & "%' "
            End If
            Query = Query & " order by c.company_id desc"

            Session("EXSQL") = Query
            Session("EXTitle") = "<b>编号</b>~^<b>公司名称</b>~^<b>地址</b>~^<b>邮编</b>~^<b>电话</b>~^<b>传真</b>~^<b>联系人</b>~^<b>IsActive</b>~^"

            Reader = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, Query)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("number", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("gCampanyID", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gCode", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gAddress", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gZip", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gPhone", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gFax", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gContactname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("gActive", System.Type.GetType("System.String")))

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
                        dr1.Item("gAddress") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("gZip") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("gPhone") = .Rows(i).Item(5).ToString().Trim()
                        dr1.Item("gFax") = .Rows(i).Item(6).ToString().Trim()
                        dr1.Item("gContactname") = .Rows(i).Item(7).ToString().Trim()
                        If .Rows(i).Item(8) = True Then
                            dr1.Item("gActive") = "Yes"
                        Else
                            dr1.Item("gActive") = "No"
                        End If
                        dtl.Rows.Add(dr1)
                    Next
                End If
            End With

            Dim dvw As DataView
            dvw = New DataView(dtl)

            Session("orderby") = "number"

            Try
                dvw.Sort = Session("orderby") & Session("orderdir")
                dgSupply.DataSource = dvw
                dgSupply.DataBind()
            Catch
            End Try
        End Sub

        Public Sub editBt(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSupply.ItemCommand
            If (e.CommandName.CompareTo("editBt") = 0) Then
                Dim str As String = e.Item.Cells(1).Text & "&companyType=" & DropDownList1.SelectedValue
                Response.Redirect(chk.urlRand("supplyedit.aspx?id=" & str), True)
            End If
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            Response.Redirect(chk.urlRand("supplyedit.aspx?id=0"), True)
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSupply.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strSQL As String
                strSQL = " Select Count(*) From Product_orders Where company_id=" & e.Item.Cells(1).Text
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
                    ltlAlert.Text = "alert('该供应商已经有定单了，无法删除！');"
                    Exit Sub
                End If
                strSQL = " Select Count(*) From part_orders Where supplier_id=" & e.Item.Cells(1).Text
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
                    ltlAlert.Text = "alert('该供应商已经有定单了，无法删除！');"
                    Exit Sub
                End If
                strSQL = "Delete From Companies Where company_id=" & e.Item.Cells(1).Text
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                Response.Redirect(chk.urlRand("supplylist.aspx"), True)
            End If
        End Sub

        Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSupply.ItemCreated
            Select Case e.Item.ItemType
                Case ListItemType.Item
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(10)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")
                Case ListItemType.AlternatingItem
                    Dim myDeleteButton As TableCell
                    myDeleteButton = e.Item.Cells(10)
                    myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该纪录吗?');")

            End Select

        End Sub

        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            BindData()
        End Sub 
    End Class

End Namespace
