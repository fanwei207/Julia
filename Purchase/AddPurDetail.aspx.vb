Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class AddPurDetail
        Inherits BasePage
    Dim strSQL As String
    Dim chk As New adamClass
    Dim reader As SqlDataReader
    Dim ds As DataSet
    Dim param As SqlParameter
    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal

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
        If Not IsPostBack() Then 
            BindData()
            BindItem()
        End If
    End Sub
    Sub BindData()
        strSQL = "Purchase_DogPartIn_GetDogPartInInfoForUpdate"
        param = New SqlParameter("@id", Request("id"))
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.StoredProcedure, strSQL, param)
        While reader.Read
            procurement_code.Text = Convert.ToString(reader("procurement_code"))
            rate.Text = Format(reader("rate"), "#.###")
            manufactory_code.Text = Convert.ToString(reader("manufactory_code"))
            delivery_code.Text = Convert.ToString(reader("delivery_code"))
            If CDate(Format(reader("first_partin_date"), "yyyy-MM-dd")) = CDate("1900.1.1") Then
                first_partin_date.Text = ""
            Else
                first_partin_date.Text = Format(reader("first_partin_date"), "yyyy-MM-dd")
            End If
            If CDate(Format(reader("last_partin_date"), "yyyy-MM-dd")) = CDate("1900.1.1") Then
                last_partin_date.Text = ""
            Else
                last_partin_date.Text = Format(reader("last_partin_date"), "yyyy-MM-dd")
            End If
            notes.Text = Convert.ToString(reader("notes"))
            pur_code_qty.Text = Convert.ToInt16(reader("prod_qty"))
            pur_code.Text = Convert.ToString(reader("code"))
            order_code.Text = Convert.ToString(reader("order_code"))
        End While
        reader.Close()
    End Sub
    Sub BindItem()
        strSQL = "Purchase_DogPartIn_GetProdOrderDetailInfo"
        param = New SqlParameter("@dog_partin_id", Request("id"))
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.StoredProcedure, strSQL, param)
        While reader.Read
            code.Text = reader("code")
            ship_qty.Text = CStr((CInt(reader("order_qty"))))
            If CDate(Format(reader("deliver_date"), "yyyy-MM-dd")) = CDate("1900.1.1") Then
                ship_date.Text = ""
            Else
                ship_date.Text = Format(reader("deliver_date"), "yyyy-MM-dd")
            End If
            If CDate(Format(reader("deliver_date_end"), "yyyy-MM-dd")) = CDate("1900.1.1") Then
                ship_date_end.Text = ""
            Else
                ship_date_end.Text = Format(reader("deliver_date_end"), "yyyy-MM-dd")
            End If
            company_code.Text = Convert.ToString(reader("company_code"))
        End While
        reader.Close()
    End Sub

    Private Sub edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edit.Click
        If first_partin_date.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('首批到货日期不能为空');"
            Exit Sub
        Else
            If Not IsDate(first_partin_date.Text.Trim) Then
                ltlAlert.Text = "alert('请输入正确的日期格式（年-月-日）');"
                Exit Sub
            End If
        End If

        If last_partin_date.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('必须到货日期不能为空');"
            Exit Sub
        Else
            If Not IsDate(last_partin_date.Text.Trim) Then
                ltlAlert.Text = "alert('请输入正确的日期格式（年-月-日）');"
                Exit Sub
            End If
        End If

        If rate.Text.Trim.Length <= 0 Then
            ltlAlert.Text = "alert('订货比例不能为空');"
            Exit Sub
        Else
            If Not IsNumeric(rate.Text.Trim) Then
                ltlAlert.Text = "alert('订货比例为数字格式');"
                Exit Sub
            End If
        End If

        strSQL = "select isnull(dp.id,0) from dog_partin dp inner join tcpc0.dbo.Plants pl1 on pl1.plantID=dp.manufactory_id inner join tcpc0.dbo.Plants pl2 on pl2.plantId=dp.delivery_id" _
               & " where  dp.id='" & Request("id") & "' and dp.procurement_code='" & procurement_code.Text.Trim() & "' and dp.rate='" & rate.Text.Trim() & "'" _
               & " and pl1.plantcode='" & manufactory_code.Text.Trim() & "' and pl2.plantcode='" & delivery_code.Text.Trim() & "' and dp.first_partin_date='" & CDate(first_partin_date.Text.Trim()) & "'" _
               & " and  dp.last_partin_date='" & CDate(last_partin_date.Text.Trim()) & "' and dp.notes='" & notes.Text.Trim() & "'"
        If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
            ltlAlert.Text = "alert('未做任何修改！');"
            Exit Sub
        End If

        Dim params(8) As SqlParameter
        strSQL = "Purchase_DogPartIn_UpdateDogPartInInfo"
        params(0) = New SqlParameter("@id", Convert.ToInt32(Request("id")))
        params(1) = New SqlParameter("@procurement_code", Convert.ToString(procurement_code.Text.Trim()))
        params(2) = New SqlParameter("@rate", Convert.ToDecimal(rate.Text.Trim()))
        params(3) = New SqlParameter("@manufactory_code", Convert.ToString(manufactory_code.Text.Trim()))
        params(4) = New SqlParameter("@delivery_code", Convert.ToString(delivery_code.Text.Trim()))
        params(5) = New SqlParameter("@first_partin_date", Convert.ToDateTime(first_partin_date.Text.Trim()))
        params(6) = New SqlParameter("@last_partin_date", Convert.ToDateTime(last_partin_date.Text.Trim()))
        params(7) = New SqlParameter("@notes", Convert.ToString(notes.Text.Trim()))
        params(8) = New SqlParameter("@modifiedby", Session("uid"))
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
        Response.Redirect(chk.urlRand("/Purchase/purchasenum.aspx?order_code=" & Request("order_code") & "&code=" & Request("code")), True)
    End Sub

    Private Sub cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancel.Click
        Response.Redirect(chk.urlRand("/Purchase/purchasenum.aspx?order_code=" & Request("order_code") & "&code=" & Request("code")), True)
    End Sub
End Class

End Namespace
