Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient


Namespace tcpc

Partial Class whPlaceCategory
        Inherits BasePage
    Public chk As New adamClass
    Dim strSql As String
    Dim ds As DataSet
    'Protected WithEvents ltlAlert As Literal

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
       
        If Request("popID") = Nothing Then
            btn_back.Enabled = False
        End If


        If Not IsPostBack Then
            add_warehouseItems()
            add_whPlaceItems()
            BindData()
        End If
    End Sub

    Sub BindData()
        Dim filterStr As String = ""

        strSql = " SELECT  categoryID FROM whPlace_CtgFilter  " _
                  & " WHERE whPlaceID='" & drd_whPlace.SelectedValue & "'" _
                  & " ORDER BY categoryID " 'sort is important. different sort may case differtent filter Str
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer

                For i = 0 To .Rows.Count - 1
                    filterStr = filterStr & "," & .Rows(i).Item("categoryID")
                Next
            End If
        End With
        ds.Reset()

        '0 value.for easy to programing.
        If filterStr.Trim.Length <= 0 Then
            filterStr = "0" 'no filter
        Else
            filterStr = filterStr.Substring(1)
        End If

        Session("categoryFiletr") = filterStr
        LoadDgItems(Session("categoryFiletr"))
    End Sub

    Sub LoadDgItems(ByVal filterString As String)
        Dim dsParts, dsProduct As DataSet
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("categoryID", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("type", System.Type.GetType("System.String")))

        'part category
        strSql = " SELECT id,name " _
               & " FROM itemCategory where type=0  "
        If drd_whPlace.SelectedValue = 0 Then
            strSql &= " and id=-1 " 'if warehouse Place is null, no item load.
        End If
        strSql &= " ORDER BY name "
        dsParts = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSql)

        With dsParts.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr As DataRow
                For i = 0 To .Rows.Count - 1
                    dr = dt.NewRow()
                    dr.Item("categoryID") = "*" & .Rows(i).Item("id")
                    dr.Item("name") = .Rows(i).Item("name").ToString().Trim()
                    dr.Item("type") = "材料分类"
                    dt.Rows.Add(dr)
                Next
            End If
        End With
        dsParts.Reset()

        'Product category
        strSql = " SELECT id,name " _
               & " FROM itemCategory where type=1 "
        If drd_whPlace.SelectedValue = 0 Then
            strSql &= " and id=-1 " 'if warehouse Place is null, no item load.
        End If
        strSql &= " ORDER BY name "
        dsProduct = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSql)

        With dsProduct.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr As DataRow
                For i = 0 To .Rows.Count - 1
                    dr = dt.NewRow()
                    dr.Item("categoryID") = "@" & .Rows(i).Item("id")
                    dr.Item("name") = .Rows(i).Item("name").ToString().Trim()
                    dr.Item("type") = "半成品分类"
                    dt.Rows.Add(dr)
                Next
            End If
        End With
        dsProduct.Reset()

        'Product category
        strSql = " SELECT id,name " _
               & " FROM itemCategory where type=2 "
        If drd_whPlace.SelectedValue = 0 Then
            strSql &= " and id=-1 " 'if warehouse Place is null, no item load.
        End If
        strSql &= " ORDER BY name "
        dsProduct = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSql)

        With dsProduct.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr As DataRow
                For i = 0 To .Rows.Count - 1
                    dr = dt.NewRow()
                    dr.Item("categoryID") = "@" & .Rows(i).Item("id")
                    dr.Item("name") = .Rows(i).Item("name").ToString().Trim()
                    dr.Item("type") = "产品分类"
                    dt.Rows.Add(dr)
                Next
            End If
        End With
        dsProduct.Reset()

        Dim dv As DataView
        dv = New DataView(dt)
        dv.Sort = "type,name"
        dv.RowFilter = " categoryID IN ('" & filterString.Replace(",", "','") & "')"
        Try
            dg_CategoryIn.DataSource = dv
            dg_CategoryIn.DataBind()
        Catch
        End Try

        dv = New DataView(dt)
        dv.Sort = "type,name"
        dv.RowFilter = " categoryID NOT IN ('" & filterString.Replace(",", "','") & "')"
        Try
            dg_CategoryOut.DataSource = dv
            dg_CategoryOut.DataBind()
        Catch
        End Try
    End Sub

    Private Sub addBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addBtn.Click
        If drd_whPlace.SelectedValue = 0 Then
            ltlAlert.Text = "alert('请选择库位！');"
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To dg_CategoryOut.Items.Count - 1
            If CType(dg_CategoryOut.Items(i).FindControl("ckb_CategoryOut"), CheckBox).Checked Then
                strSql = "INSERT INTO whPlace_CtgFilter(whPlaceID,categoryID)" _
                        & " VALUES('" & drd_whPlace.SelectedValue & "','" & dg_CategoryOut.Items(i).Cells(0).Text.Trim & "')"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            End If
        Next

        BindData()
    End Sub

    Private Sub deleteBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles deleteBtn.Click
        If drd_whPlace.SelectedValue = 0 Then
            ltlAlert.Text = "alert('请选择库位！');"
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To dg_CategoryIn.Items.Count - 1
            If CType(dg_CategoryIn.Items(i).FindControl("ckb_CategoryIn"), CheckBox).Checked Then
                strSql = " DELETE FROM whPlace_CtgFilter WHERE whPlaceID='" _
                       & drd_whPlace.SelectedValue & "' AND categoryID='" & dg_CategoryIn.Items(i).Cells(0).Text.Trim & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            End If
        Next

        BindData()
    End Sub

    Sub add_warehouseItems()
        drd_wh.Items.Clear()
        Dim item As ListItem
        Dim ds As DataSet

        item = New ListItem("--")
        item.Value = 0
        drd_wh.Items.Add(item)

        strSql = " SELECT warehouseID,name FROM warehouse ORDER BY name "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item("name"))
                    item.Value = Convert.ToInt32(.Rows(i).Item("warehouseID"))
                    drd_wh.Items.Add(item)
                Next
            End If
        End With
        ds.Reset()
        drd_wh.SelectedIndex = 0
    End Sub

    Sub add_whPlaceItems()
        drd_whPlace.Items.Clear()
        Dim item As ListItem
        Dim ds As DataSet

        strSql = " SELECT placeID,name FROM warehousePlace WHERE warehouseID='" & drd_wh.SelectedValue & "' ORDER BY name "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item("name"))
                    item.Value = Convert.ToInt32(.Rows(i).Item("placeID"))
                    drd_whPlace.Items.Add(item)
                Next
            Else
                item = New ListItem("--")
                item.Value = 0
                drd_whPlace.Items.Add(item)
            End If
        End With
        ds.Reset()
        drd_whPlace.SelectedIndex = 0
    End Sub

    Private Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
        Response.Redirect(chk.urlRand("/productOrder/POPPartsWhPlace.aspx?popID=" & Request("popID") & "&r=" & Request("r")))
    End Sub

    Private Sub drd_wh_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drd_wh.SelectedIndexChanged
        add_whPlaceItems()
        BindData()
    End Sub

    Private Sub drd_whPlace_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drd_whPlace.SelectedIndexChanged
        BindData()
    End Sub
End Class

End Namespace
