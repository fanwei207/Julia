

Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

'Partial Class upcProgram_productupcTQL
'    Inherits System.Web.UI.Page

'End Class
Namespace tcpc

    Partial Class upcProgram_productupcTQL
        Inherits BasePage
        Dim strSql As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then
                productSerialNumberLoad()
                packageNumberLoad()

                If Not Security("5005").isValid Then
                    DataGrid1.Columns(5).Visible = False
                    apProductUPCInfo1.Disabled = True
                    apProductUPCInfo2.Disabled = True
                    productUPC.Visible = False
                End If

                DataGridLoad()
            End If
        End Sub
        Sub productSerialNumberLoad()
            Dim ls As New ListItem
            strSql = " SELECT systemCodeName,comments FROM tcpc0.dbo.systemCode" _
                    & " INNER JOIN tcpc0.dbo.systemCodeType ON tcpc0.dbo.systemCode.systemCodeTypeID=tcpc0.dbo.systemCodeType.systemCodeTypeID" _
                    & " WHERE tcpc0.dbo.systemCodeType.SystemCodeTypeName='product Serial NumberTW'"
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
            While reader.Read
                ls = New ListItem
                ls.Value = reader("comments")
                ls.Text = reader("systemCodeName")
                productSerialNumber.Items.Add(ls)
            End While
            reader.Close()
        End Sub
        Sub packageNumberLoad()
            Dim ls As New ListItem
            strSql = " SELECT systemCodeName,comments FROM tcpc0.dbo.systemCode" _
                    & " INNER JOIN tcpc0.dbo.systemCodeType ON tcpc0.dbo.systemCode.systemCodeTypeID=tcpc0.dbo.systemCodeType.systemCodeTypeID" _
                    & " WHERE tcpc0.dbo.systemCodeType.SystemCodeTypeName='package NumberTW'"
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSql)
            While reader.Read
                ls = New ListItem
                ls.Value = reader("comments")
                ls.Text = reader("systemCodeName")
                packageNumber.Items.Add(ls)
            End While
            reader.Close()
        End Sub
        Sub productUPC_click(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not Security("5005").isValid Then
                ltlAlert.Text = "alert('请确认有生成条形码的权限！');"
                Exit Sub
            End If
            If (Textbox1.Text.Trim.Length() <= 0) Then
                ltlAlert.Text = "alert('请输入产品名称！');"
                Exit Sub
            End If
            If (productSerialNumber.SelectedIndex = 0) Then
                ltlAlert.Text = "alert('请选择产品类型！');"
                Exit Sub
            End If
          

           

            If (packageNumber.SelectedIndex = 0) Then
                ltlAlert.Text = "alert('请选择包装类型！');"
                Exit Sub
            End If

            Dim ds As DataSet
            Dim upc As String
            Dim pb As Integer
            Dim read As SqlDataReader

            If power.Text.Trim.Length < 2 Then
                power.Text = "0" & power.Text.Trim
            End If

            strSql = " SELECT UPC " _
                    & " FROM ProductUPC " _
                    & " WHERE name='" & Textbox1.Text.Trim & "'"

            ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                ltlAlert.Text = "alert('此产品的条码已经存在，为：" & ds.Tables(0).Rows(0).Item("UPC") & "');Form1.productSerialNumber.focus();"
            Else
                read = SqlHelper.ExecuteReader(chk.dsn0, CommandType.StoredProcedure, "sp_upc_newupcTQL")
                While (read.Read())
                    upc = read.Item(0)
                End While
                'upc &= "69344495" & productSerialNumber.SelectedValue & power.Text.Trim & packageNumber.SelectedValue


                pb = calculatePB(upc.Trim)
                If pb <> -1 Then
                    upc &= pb
                Else
                    ltlAlert.Text = "alert('错误——数据长度不符合要求.');Form1.productSerialNumber.focus();"
                    Exit Sub
                End If
                strSql = " INSERT INTO ProductUPC " _
                        & " (factoryCode,productSerialNumber,power,packageNumber,UPC,name,plantCode) " _
                        & " Values " _
                        & " (69344495,'" & productSerialNumber.SelectedValue & "','" & power.Text.Trim _
                        & "','" & packageNumber.SelectedValue & "','" & upc.Trim & "',N'" & Textbox1.Text.Trim & "',97)"
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSql)
                ltlAlert.Text = "alert('生成的条码为：" & upc & "');Form1.productSerialNumber.focus();"
                productSerialNumber.SelectedIndex = 0
                power.Text = ""
                packageNumber.SelectedIndex = 0
                Textbox1.Text = ""
                DataGridLoad()
            End If
            productSerialNumber.SelectedIndex = 0
            power.Text = ""
            packageNumber.SelectedIndex = 0
            Textbox1.Text = ""
        End Sub
        Sub search_click(ByVal sender As Object, ByVal e As System.EventArgs)
            DataGridLoad()
        End Sub
        Function calculatePB(ByVal inputStr As String) As Integer
            If inputStr.Trim.Length <> 12 Then
                calculatePB = "-1"
            Else
                Dim i As Integer
                Dim input As String
                Dim oud As Integer = 0
                Dim odd As Integer = 0
                Dim result As Integer
                input = StrReverse(inputStr.Trim)

                For i = 0 To input.Trim.Length - 1
                    If i Mod 2 = 0 Then
                        oud += Val(input.Chars(i))
                    Else
                        odd += Val(input.Chars(i))
                    End If
                Next
                result = 10 - CInt(Right(CStr(oud * 3 + odd), 1))
                If result = 10 Then
                    calculatePB = 0
                Else
                    calculatePB = result
                End If
            End If
        End Function
        Sub DataGridLoad()
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = " SELECT p.id,p.factoryCode as code, isnull(p.name,'') as name,sc1.systemCodeName as productSerialNumber, " _
                    & " p.power,sc2.systemCodeName as packageNumber,UPC " _
                    & " From tcpc0.dbo.ProductUPC p  " _
                    & " inner  Join tcpc0.dbo.systemCode sc1 On sc1.comments=cast(p.productSerialNumber as varchar) " _
                    & " INNER JOIN tcpc0.dbo.systemCodeType sct1 ON sct1.systemCodeTypeID=sc1.systemCodeTypeID " _
                    & " inner  Join tcpc0.dbo.systemCode sc2 On sc2.comments=cast(p.packageNumber as varchar) " _
                    & " INNER JOIN tcpc0.dbo.systemCodeType sct2 ON sct2.systemCodeTypeID=sc2.systemCodeTypeID " _
                    & " WHERE sct1.systemCodeTypeName='product Serial NumberTW' and sct2.systemCodeTypeName='package NumberTW' and p.plantCode=97 "
            If (Textbox1.Text.Trim.Length > 0) Then
                strSQL = strSQL & " and LOWER(p.name) like N'%" & Textbox1.Text.Trim.ToLower() & "%' "
            End If
            If (productSerialNumber.SelectedIndex > 0) Then
                strSQL = strSQL & " and p.productSerialNumber=" & productSerialNumber.SelectedValue
            End If
            If (power.Text.Trim.Length > 0 And power.Text.Trim <> "0") Then
                strSQL = strSQL & " and p.power='" & power.Text & "'  "
            End If
            If (packageNumber.SelectedIndex > 0) Then
                strSQL = strSQL & " and p.packageNumber=" & packageNumber.SelectedValue
            End If
            strSQL = strSQL & " order by p.UPC "

            'Response.Write(strSQL)
            'Exit Sub

            Session("EXSQL") = strSQL
            Session("EXTitle") = "80^<b>厂商代码</b>~^150^<b>产品名称</b>~^400^<b>产品类型</b>~^80^<b>功率</b>~^550^<b>包装类型</b>~^150^<b>产品条码</b>~^"
            ds = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("productSerialNumber", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("power", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("packageNumber", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("UPC", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("gid", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("code") = .Rows(i).Item("code").ToString().Trim()
                        dr1.Item("productSerialNumber") = .Rows(i).Item("productSerialNumber").ToString().Trim()
                        dr1.Item("power") = .Rows(i).Item("power").ToString().Trim()
                        dr1.Item("packageNumber") = .Rows(i).Item("packageNumber").ToString().Trim()
                        dr1.Item("UPC") = .Rows(i).Item("UPC").ToString().Trim()
                        dr1.Item("name") = .Rows(i).Item("name").ToString().Trim()
                        dr1.Item("gid") = .Rows(i).Item("id").ToString().Trim()
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            ds.Reset()
            Dim dv As DataView
            dv = New DataView(dt)
            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub

        Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                If Not Security("5005").isValid Then
                    ltlAlert.Text = "alert('请确认有删除条形码的权限！');"
                    Exit Sub
                End If

                Dim strSQLhis As String
                strSQLhis = "insert into ProductUPCHis([factoryCode] ,[productSerialNumber] ,[power] ,[packageNumber],[UPC] ,[name] ,[createdBy] ,[createdDate],[plantCode] ,[serial])" _
                           & " select  [factoryCode] ,[productSerialNumber] ,[power] ,[packageNumber],[UPC] ,[name] ," & Session("uID").ToString() & ",GETDATE(),[plantCode] ,[serial] from ProductUPC" _
                           & " where id = " & e.Item.Cells(6).Text()
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQLhis)

                Dim strSQL As String
                strSQL = "delete from ProductUPC where id = " & e.Item.Cells(6).Text()
                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                DataGridLoad()
            End If
        End Sub

        Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            DataGridLoad()
        End Sub
    End Class

End Namespace
