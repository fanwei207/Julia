Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class workprocedure
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected strTemp As String
        'Protected WithEvents ltlAlert As Literal
        Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
        Dim item As ListItem
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


            If Not IsPostBack Then


                Dim item As ListItem
                Dim strSQL As String
                Dim ds As DataSet
                Departmentdropdownlist()


                item = New ListItem("--")
                item.Value = 0
                workshop.Items.Add(item)
                workshop.SelectedIndex = 0


                item = New ListItem("--")
                item.Value = 0
                DropDownList1.Items.Add(item)
                DropDownList1.SelectedIndex = 0

                strSQL = "SELECT systemcodeid,systemcodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemcodetypeid=s.systemcodetypeid Where st.systemcodetypename='Work Procedure Type'  Order by s.systemcodeid "
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

                BindData()
            End If
        End Sub
        Private Sub BindData()
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT w.id,w.unitPrice,w.name,s.systemcodename, w.guideLine,w.wdate,isnull(d.name,''),isnull(wp.name,''),w.deductName,w.deductRate,w.deductPrice,isnull(w.newdeductprice,0) as newdeductprice ,CASE WHEN w.flag=1 THEN N'禁' ELSE '' END,isnull(w.wallowance,0),isnull(w.wpercent,0)   " _
              & " From workprocedure w " _
              & " INNER JOIN tcpc0.dbo.Systemcode s ON w.typeID=s.systemcodeID " _
              & " left outer join departments d on d.departmentID=w.departmentID " _
              & " left outer join workshop wp on wp.id=w.workshopID " _
              & " where organizationID=" & Session("orgID")

            If DropDownList1.SelectedIndex > 0 Then
                strSQL = strSQL & " and  w.TypeID=" & DropDownList1.SelectedValue
            End If

            If cname.Text.Trim().Length > 0 Then
                strSQL = strSQL & " and  replace(w.name,' ','') like N'%" & cname.Text.Trim.Replace(" ", "") & "%' "
            End If

            If cguideline.Text.Trim().Length > 0 Then
                strSQL = strSQL & " and  w.guideLine='" & cguideline.Text.Trim() & "' "
            End If

            If limitbutton.Checked Then
                strSQL = strSQL & " and  w.flag='1' "
            End If

            If department.SelectedValue > 0 Then
                strSQL &= " and w.departmentID='" & department.SelectedValue & "'"
            End If

            If workshop.SelectedValue > 0 Then
                strSQL &= " and w.workshopID='" & workshop.SelectedValue & "'"
            End If

            strSQL = strSQL & " Order by w.id desc"
            'Response.Write(strSQL)
            'Exit Sub
            If Session("uID") <> 5674 Then
                Session("EXSQL") = strSQL
                Session("EXTitle") = "200^<b>工序名称</b>~^<b>工序性质</b>~^<b>指标</b>~^<b>日期</b>~^120^<b>部门</b>~^120^<b>工段</b>~^<b>扣款种类</b>~^<b>扣款率</b>~^<b>扣款金额</b>~^<b>新单价</b>~^<b>禁用</b>~^<b>补贴</b>~^<b>上限</b>~^"
            End If
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("gname", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gcategory", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gguideline", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("gunitprice", System.Type.GetType("System.String")))
            'dt.Columns.Add(New DataColumn("glowest", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("gdeducttype", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gdeduct", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("gID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gdeductPrice", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("newprice", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("limited", System.Type.GetType("System.String")))

            dt.Columns.Add(New DataColumn("department", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("wallowance", System.Type.GetType("System.Decimal")))
            dt.Columns.Add(New DataColumn("wpercent", System.Type.GetType("System.Decimal")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("gname") = .Rows(i).Item(2).ToString().Trim()
                        dr1.Item("gcategory") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("gguideline") = .Rows(i).Item(4)
                        dr1.Item("gunitprice") = .Rows(i).Item(5).ToShortDateString()
                        dr1.Item("department") = .Rows(i).Item(6)
                        dr1.Item("workshop") = .Rows(i).Item(7)
                        If Not .Rows(i).IsNull(8) Then
                            dr1.Item("gdeducttype") = .Rows(i).Item(8).ToString().Trim()
                            dr1.Item("gdeduct") = .Rows(i).Item(9)
                        End If
                        dr1.Item("gID") = .Rows(i).Item(0).ToString().Trim()
                        If Not .Rows(i).IsNull(10) Then
                            dr1.Item("gdeductPrice") = .Rows(i).Item(10)
                        End If
                        If .Rows(i).Item(4) = 0 Then
                            dr1.Item("newprice") = Format(.Rows(i).Item(11), "0.00")
                        Else
                            dr1.Item("newprice") = .Rows(i).Item(11)
                        End If

                        dr1.Item("limited") = .Rows(i).Item(12)
                        dr1.Item("wallowance") = .Rows(i).Item(13)
                        dr1.Item("wpercent") = .Rows(i).Item(14)
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try

            If Session("uID") = 5674 Then
                DataGrid1.Columns(16).Visible = False
                DataGrid1.Columns(5).Visible = False
                DataGrid1.Columns(6).Visible = False
                DataGrid1.Columns(7).Visible = False
                DataGrid1.Columns(8).Visible = False
                DataGrid1.Columns(9).Visible = False
                DataGrid1.Columns(10).Visible = False
                DataGrid1.Columns(11).Visible = False
                DataGrid1.Columns(12).Visible = False
                DataGrid1.Columns(13).Visible = False
                DataGrid1.Columns(14).Visible = False
                DataGrid1.Columns(15).Visible = False

            End If
        End Sub

        Public Sub editBt(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("editBt") = 0) Then
                Dim str As String = e.Item.Cells(0).Text
                Response.Redirect(chk.urlRand("workprocedureEdit.aspx?id=" & str))
            End If
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strSQL As String
                strSQL = "delete from workprocedure where id = " & e.Item.Cells(0).Text()
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                strSQL = " Update Indicator set workProcedure=workProcedure+1 "
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)


                BindData()
            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            Response.Redirect(chk.urlRand("workprocedureEdit.aspx"))
        End Sub

        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            BindData()
        End Sub
        Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
            Response.Redirect(chk.urlRand("workprocedureimport.aspx"))
        End Sub

        Sub Departmentdropdownlist()
            Dim strSQL As String
            Dim reader As SqlDataReader

            item = New ListItem("--")
            item.Value = 0
            department.Items.Add(item)
            department.SelectedIndex = 0
            strSQL = " SELECT departmentID,Name From Departments WHERE isSalary='1' AND  active='1' order by departmentID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                department.Items.Add(item)
            End While
            reader.Close()
        End Sub

        Private Sub Department_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles department.SelectedIndexChanged
            If department.SelectedValue <> 0 Then
                workshopDropDownList()
            Else
                workshop.Items.Clear()

                item = New ListItem("--")
                item.Value = 0
                workshop.Items.Add(item)
                workshop.SelectedIndex = 0
            End If

        End Sub

        Sub workshopDropDownList()
            Dim strSQL As String
            Dim reader As SqlDataReader
            workshop.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
            workshop.Items.Add(item)
            workshop.SelectedIndex = 0

            strSQL = " SELECT w.id, w.name FROM Workshop w INNER JOIN departments d ON w.departmentID = d.departmentID " _
                  & " WHERE d.name=N'" & department.SelectedItem.Text.Trim() & "' AND w.workshopID IS NULL Order by w.code  "

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While reader.Read
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                workshop.Items.Add(item)
            End While
            reader.Close()
        End Sub
    End Class

End Namespace
