Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class applycar
        Inherits BasePage
    Dim chk As New adamClass
    Protected strTemp12 As String
    'Protected WithEvents ltlAlert As Literal
    Protected WithEvents droplistusedtime As System.Web.UI.WebControls.DropDownList
    Shared red As String
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents labelrequest As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        
        If Not IsPostBack Then
           
            carid.Text = "0"
            Dim strSql As String

            strSql = " Delete From applycarDetails Where (cartime is Null) "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            querybind()
            table2BindData()
        End If
    End Sub
    Private Sub btAppend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAppend.Click
        Labelmemory.Text = 0
        Datagrid1.Visible = False
        Table2.Visible = True
        dgOrderDetail.Visible = True

        BtOk.Visible = True
        btCancel.Visible = True
        Button1.Visible = True
        btAppend.Visible = False
        btprint.Visible = False


        table1BindData()
        table2BindData()
        ADD()
    End Sub

    Private Sub BtOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOk.Click
        If Labelmemory.Text = "0" And objtto.Text = "" Or objttotel.Text = "" Or objttofax.Text = "" Or objtcc.Text = "" Or objtsb.Text = "" Or objtfrom.Text = "" Or objtfromtel.Text = "" Or objtfromfax.Text = "" Or objtdate.Text = "" Or objtother.Text = "" Then
            ltlAlert.Text = "alert('表格内容必须填写完整.');"
            Exit Sub
        End If
        Dim strSQL As String
        strSQL = "delete from applycar where carID ='" & Labelmemory.Text & "'"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)


        strSQL = "Insert Into applycar "
        strSQL = strSQL & " ([to],toTel,toFax,cc,sb,[from],fromTel,fromFax,usedDate,comments,createdBy,createdDate,organizationID) "
        strSQL = strSQL & "values (N'" & objtto.Text & "','" & objttotel.Text & "','" & objttofax.Text & "',N'" & objtcc.Text & "',N'" & objtsb.Text & "',N'" & objtfrom.Text & "','" & objtfromtel.Text & "','" & objtfromfax.Text & "','" & objtdate.Text & "',N'" & objtother.Text & "','" & Session("uID") & "', getdate(),'" & Session("orgID") & "')"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

        'strSQL = "delete from applyCarDetails where carID =" & Labelmemory.Text
        'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

        Dim i As Integer
        Dim droplistindex As String
        Dim vstartplace As String
        Dim vendplace As String
        Dim vqty As Integer
        Dim vpersonNum As Integer
        Dim sss As System.DateTime

        For i = 0 To dgOrderDetail.Items.Count - 1
            droplistindex = DateTime.Now.ToShortDateString() & " " & CType(dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(1).Controls(0), TextBox).Text & ":00"
            vstartplace = CType(dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(2).Controls(0), TextBox).Text
            vendplace = CType(dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(3).Controls(0), TextBox).Text

            Try

                sss = System.DateTime.Parse(droplistindex)
            Catch
                ltlAlert.Text = "alert('输入用车时间不正确!例 9：10')"
                table1BindData()
                Exit Sub

            End Try




            If IsNumeric(CType(dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(4).Controls(0), TextBox).Text) Then
                vqty = CType(dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(4).Controls(0), TextBox).Text
            Else
                ltlAlert.Text = "alert('人数和货数必须是数值.')"
                table1BindData()
                Exit Sub
            End If
            If IsNumeric(CType(dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(5).Controls(0), TextBox).Text) Then
                vpersonNum = CType(dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(5).Controls(0), TextBox).Text
            Else
                ltlAlert.Text = "alert('人数和货数必须是数值.')"
                table1BindData()
                Exit Sub
            End If


            strSQL = "Update applyCarDetails "
            strSQL = strSQL & " Set usedDateTime='" & objtdate.Text & "',startPlace=N'" & vstartplace & "',endPlace=N'" & vendplace & "',Qty='" & vqty & "',personNum='" & vpersonNum & "', "
            strSQL = strSQL & " comments=N'" & CType(dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(6).Controls(0), TextBox).Text & "',cartime='" & CType(dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(1).Controls(0), TextBox).Text & "' Where carID='" & dgOrderDetail.Items(dgOrderDetail.EditItemIndex).Cells(7).Text & "'"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
        Next i

        strSQL = "DELETE FROM applyCarDetails WHERE ({ fn LENGTH(startPlace) } = 0) OR ({ fn LENGTH(endPlace) } = 0)"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
        'btAppend.Visible = True
        If ptYesorNo() <> 0 Then
            btprint.Visible = True
        End If

        dgOrderDetail.EditItemIndex = -1
        table2BindData()
        'querybind()
        red = ""
    End Sub

    Private Sub btCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Dim flage As Integer = ptYesorNo()
        btAppend.Visible = True
        If flage <> 0 Then
            btprint.Visible = True
        End If
        BtOk.Visible = False
        btCancel.Visible = False
        Button1.Visible = False
        Dim strSql As String

        strSql = " Delete From applycarDetails Where (cartime is Null)"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        dgOrderDetail.EditItemIndex = -1
            'table2BindData()
            'querybind()
        red = ""
    End Sub
    Private Sub btprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btprint.Click
        Dim script As String
        script = "<script> window.open('applycarPrint.aspx?" _
      & "&printno=" & objtdate.Text & "" _
       & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=600,height=600,top=20,left=40') "
        script = script + "</" + "script>"
        RegisterClientScriptBlock("test", script)
    End Sub
    Private Sub table1BindData()
        Dim Query As String
        Dim ds As DataSet
        Dim cardate As String
        'If Labelmemory.Text = 0 Then
        If red <> "" Then
            Query = " Select usedDateTime From applyCarDetails where carID='" & red & "'"
            cardate = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)
        End If
        Query = "SELECT carID, [to], toTel, toFax, cc, sb, [from], fromTel, fromFax, usedDate, comments FROM applyCar ORDER BY carID"
        'Else
        'Query = "SELECT carID, [to], toTel, toFax, cc, sb, [from], fromTel, fromFax, usedDate, comments FROM applyCar  where carID = " & Labelmemory.Text
        ' End If
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
        Dim vCarid As Integer
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    vCarid = .Rows(i).Item(0)
                    Labelmemory.Text = .Rows(i).Item(0)
                    ' labelrequest.Text = .Rows(i).Item(1) & "    " & labelrequest.Text
                    objtto.Text = .Rows(i).Item(1)
                    objttotel.Text = .Rows(i).Item(2)
                    objttofax.Text = .Rows(i).Item(3)
                    objtcc.Text = .Rows(i).Item(4)
                    objtsb.Text = .Rows(i).Item(5)
                    objtfrom.Text = .Rows(i).Item(6)
                    objtfromtel.Text = .Rows(i).Item(7)
                    objtfromfax.Text = .Rows(i).Item(8)
                    If red <> "" Then
                        objtdate.Text = cardate
                    Else
                        objtdate.Text = .Rows(i).Item(9)
                    End If

                    objtother.Text = .Rows(i).Item(10)
                Next
            End If
        End With

        ' Labelmemory.Text = vCarid

    End Sub
    Private Sub table2BindData()
        Dim Query As String
        Dim Reader As DataSet
        If objtdate.Text <> "" Then
            Query = "SELECT carID, isnull(cartime,'') as cartime, startPlace, endPlace, Qty, personNum,comments FROM applyCarDetails where datepart(month,usedDateTime)='" & CDate(objtdate.Text).Month.ToString & "' and datepart(day,usedDateTime)='" & CDate(objtdate.Text).Day.ToString & "'"
            Query &= " order by carID desc "   'where carID = " & Labelmemory.Text
        Else
            Query = "SELECT carID, isnull(cartime,'') as cartime, startPlace, endPlace, Qty, personNum,comments FROM applyCarDetails order by carID desc "
        End If
        Reader = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("gcarid", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("gtime", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gstartplace", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gendplace", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gqty", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gperson", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("comment", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
        Dim datetrasfer As Date
        With Reader.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("gcarid") = i + 1
                    dr1.Item("gtime") = .Rows(i).Item(1)
                    dr1.Item("gstartplace") = .Rows(i).Item(2).ToString().Trim()
                    dr1.Item("gendplace") = .Rows(i).Item(3).ToString().Trim()
                    dr1.Item("gqty") = .Rows(i).Item(4).ToString().Trim()
                    dr1.Item("gperson") = .Rows(i).Item(5).ToString().Trim()
                    dr1.Item("comment") = .Rows(i).Item(6).ToString().Trim()
                    dr1.Item("id") = .Rows(i).Item(0).ToString().Trim()
                    dt.Rows.Add(dr1)
                Next
            End If
        End With
        Dim dv As DataView
        dv = New DataView(dt)
        dgOrderDetail.DataSource = dv
        dgOrderDetail.DataBind()
    End Sub




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ADD()
    End Sub
    ' ADD a datasource-------------------------------------
    Sub ADD()
        Dim strsql As String
        'strsql = "delete from applyCarDetails where carID =" & Labelmemory.Text
        'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql)
        If dgOrderDetail.EditItemIndex = 0 Then
            ltlAlert.Text = "alert('你必须先保存后才能添加!')"
            Exit Sub
        End If
        Dim i As String
        Dim droplistindex As Integer
        Dim vstartplace As String
        Dim vendplace As String
        Dim vqty As Integer
        Dim vpersonNum As Integer

        Dim Query As String
        'Dim ds As DataSet
        If objtdate.Text <> "" Then
            Query = "Insert Into applyCarDetails "
            Query = Query & " (startPlace,endPlace,Qty,personNum,comments,usedDateTime) "
            Query = Query & "values ('','',0,0,'','" & objtdate.Text & "')"
        Else
            Query = "Insert Into applyCarDetails "
            Query = Query & " (startPlace,endPlace,Qty,personNum,comments) "
            Query = Query & "values ('','',0,0,'')"
        End If
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
        dgOrderDetail.EditItemIndex = 0
        table2BindData()

    End Sub



    Public Sub printBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
        If (e.CommandName.CompareTo("printBtn") = 0) Then
            Dim str As String = e.Item.Cells(8).Text
            'Labelmemory.Text = str
            Dim script As String
            script = "<script> window.open('applycarPrint.aspx?" _
          & "&id=" & str & "" _
           & " ','','menubar=yes,scrollbars = yes,resizable=yes,width=600,height=600,top=20,left=40') "
            script = script + "</" + "script>"
            RegisterClientScriptBlock("test", script)

        End If
    End Sub
    Public Sub EditBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
        If (e.CommandName.CompareTo("EditBtn") = 0) Then
            Dim str As String = e.Item.Cells(8).Text
            Dim j As Integer
            'Labelmemory.Text = str
            Datagrid1.Visible = False
            Table2.Visible = True
            dgOrderDetail.Visible = True

            BtOk.Visible = True
            btCancel.Visible = True
            Button1.Visible = True
            btAppend.Visible = False
            'btprint.Visible = False
            red = str
            table1BindData()
            table2BindData()
            For j = 0 To dgOrderDetail.Items.Count - 1
                If str = dgOrderDetail.Items(j).Cells(7).Text Then
                    dgOrderDetail.EditItemIndex = j
                End If
            Next

            table2BindData()
        End If
    End Sub

    Private Sub querybtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        querybind()
    End Sub
    Private Sub querybind()
        Datagrid1.Visible = True
        Table2.Visible = False
        dgOrderDetail.Visible = False

        btprint.Visible = False
        BtOk.Visible = False
        btCancel.Visible = False
        Button1.Visible = False

        Dim Query As String
        Dim Reader As DataSet
        Query = "SELECT carID, usedDateTime, startPlace, endPlace, Qty, personNum,cartime,comments  FROM applyCarDetails  ORDER BY usedDateTime,cartime desc"
        Reader = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("number", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("gcarid", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("guseddate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gstartplace", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gendplace", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gqty", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gperson", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gusedtime", System.Type.GetType("System.String")))
        Dim datetrasfer As Date
        Dim date1 As Date
        With Reader.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("number") = i + 1
                    dr1.Item("gcarid") = .Rows(i).Item(0)
                    date1 = .Rows(i).Item(1)
                    dr1.Item("gUsedDate") = .Rows(i).Item(1).ToShortDateString
                    dr1.Item("gstartplace") = .Rows(i).Item(2).ToString().Trim()
                    dr1.Item("gendplace") = .Rows(i).Item(3).ToString().Trim()
                    dr1.Item("gqty") = .Rows(i).Item(4).ToString().Trim()
                    dr1.Item("gperson") = .Rows(i).Item(5).ToString().Trim()
                    dr1.Item("gusedtime") = .Rows(i).Item(6).ToString().Trim()
                    dt.Rows.Add(dr1)
                Next
            End If
        End With
        Dim dv As DataView
        dv = New DataView(dt)
        Datagrid1.DataSource = dv
        Datagrid1.DataBind()
    End Sub
    Private Function ptYesorNo()
            Dim tabledate As DateTime
            If objtDate.Text.Length > 0 Then
                tabledate = CDate(objtDate.Text)
            End If


            Dim Query As String
            Dim ds As DataSet
            Query = "SELECT COUNT(carID) AS Expr1 FROM applyCarDetails WHERE datepart(month,usedDateTime)='" & tabledate.Month.ToString & "' and datepart(day,usedDateTime)='" & tabledate.Day.ToString & "'"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
            Dim flage As Integer = 0
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        flage = .Rows(i).Item(0)
                    Next
                End If
            End With
            Return flage
        End Function
    Private Sub dgreturnDetail_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgOrderDetail.ItemCreated
        Select Case e.Item.ItemType
            Case ListItemType.Item
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(8)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

            Case ListItemType.AlternatingItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(8)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

            Case ListItemType.EditItem
                Dim myDeleteButton As TableCell
                myDeleteButton = e.Item.Cells(8)
                myDeleteButton.Attributes.Add("onclick", "return confirm('确定要删除该记录吗？');")

                CType(e.Item.Cells(1).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(60)
                CType(e.Item.Cells(2).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(130)
                CType(e.Item.Cells(3).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(130)
                CType(e.Item.Cells(4).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(60)
                CType(e.Item.Cells(5).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(60)
                CType(e.Item.Cells(6).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(190)
                CType(e.Item.Cells(1).Controls(0), TextBox).MaxLength = 5
                CType(e.Item.Cells(2).Controls(0), TextBox).MaxLength = 100
                CType(e.Item.Cells(3).Controls(0), TextBox).MaxLength = 100
                CType(e.Item.Cells(6).Controls(0), TextBox).MaxLength = 255
        End Select
    End Sub

    Public Sub DELETEBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgOrderDetail.ItemCommand
        If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
            Dim strSql As String
            strSql = " Delete From applycarDetails Where carID='" & e.Item.Cells(7).Text & "'"

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            table2BindData()
        End If
    End Sub
End Class

End Namespace
