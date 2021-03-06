Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class materialassign
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim strSQL As String
    Dim chk As New adamClass
    Dim reader As SqlDataReader
    Dim ds As DataSet
    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
                If Me.Security("30010103").isValid Then
                    ltlAlert.Text = "alert('请确认有该项目的编辑权限！');"
                    Exit Sub
                End If
            BindData()
        End If
    End Sub

    Sub BindData()
        strSQL = " SELECT po.order_code AS ordercode, i.code AS prodcode, ii.code AS partcode, isnull(SUM(dpd.real_qty),0) AS realqty" _
               & " FROM Dog_PartIn_Detail dpd" _
               & " INNER JOIN Dog_PartIn dp ON dp.id = dpd.dog_partin_id" _
               & " INNER JOIN Product_order_detail pod ON dp.prod_order_detail_id = pod.prod_order_detail_id" _
               & " INNER JOIN Product_orders po ON pod.prod_order_id = po.prod_order_id" _
               & " INNER JOIN tcpc0.dbo.Items i ON i.id = pod.prod_id" _
               & " INNER JOIN tcpc0.dbo.Items ii ON ii.id = dp.prod_id" _
               & " WHERE dpd.dog_partin_id ='" & Request("id") & "'" _
               & " GROUP BY dpd.dog_partin_id, po.order_code, i.code, ii.code"
       

        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)

        If reader.Read Then
            ordercode.Text = reader("ordercode")
            prodcode.Text = reader("prodcode")
            partcode.Text = reader("partcode")
            realqty.Text = reader("realqty")
        End If
    End Sub

        Private Sub btnTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrans.Click

            If transqty.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('转移数量 不能为空！');"
                Exit Sub
            Else
                Try
                    Dim _n As Integer = Convert.ToInt32(transqty.Text.Trim())
                    If _n <= 0 Then
                        ltlAlert.Text = "alert('转移数量 只能是大于零的整数！');"
                        Exit Sub
                    End If
                Catch ex As Exception
                    ltlAlert.Text = "alert('转移数量 只能是数字！');"
                    Exit Sub
                End Try

            End If

            If toordercode.Text.Trim().ToUpper = ordercode.Text.Trim().ToUpper Then
                ltlAlert.Text = "alert('原定单和目的定单不能相同！');"
                Exit Sub
            End If

            strSQL = " SELECT ISNULL(dp.id, 0) AS id FROM Dog_PartIn dp" _
                   & " INNER JOIN Product_order_detail pod ON dp.prod_order_detail_id = pod.prod_order_detail_id" _
                   & " INNER JOIN Product_orders po ON pod.prod_order_id = po.prod_order_id " _
                   & " INNER JOIN tcpc0.dbo.Items i ON i.id = pod.prod_id" _
                   & " INNER JOIN tcpc0.dbo.Items ii ON ii.id = dp.prod_id" _
                   & " WHERE po.order_code ='" & toordercode.Text.Trim() & "' AND i.code = '" & toprodcode.Text.Trim() & "' AND ii.code ='" & partcode.Text.Trim() & "'"
            Dim id As Integer
            id = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
            If id = 0 Then
                ltlAlert.Text = "alert('目的定单号或目的产品名称不正确！');"
                Exit Sub
            End If

            Dim countnum, transnum As Integer
            transnum = CInt(transqty.Text.Trim())
            If transnum > realqty.Text Then
                ltlAlert.Text = "alert('转移数量不能大于实到数量！');"
                Exit Sub
            End If

            Dim paramscount(2) As SqlParameter
            strSQL = "Purchase_Check_Dog_PartIn_Detail_Existing"
            paramscount(0) = New SqlParameter("@toorder_code", Convert.ToString(toordercode.Text.Trim()))
            paramscount(1) = New SqlParameter("@toprod_code", Convert.ToString(toprodcode.Text.Trim()))
            paramscount(2) = New SqlParameter("@part_code", Convert.ToString(partcode.Text.Trim()))
            countnum = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, paramscount)

            Dim params(7) As SqlParameter
            If countnum <= 0 Then
                strSQL = "Purchase_Dog_PartIn_Detail_Trans_Insert"
                params(0) = New SqlParameter("@order_code", Convert.ToString(ordercode.Text.Trim()))
                params(1) = New SqlParameter("@prod_code", Convert.ToString(prodcode.Text.Trim()))
                params(2) = New SqlParameter("@toorder_code", Convert.ToString(toordercode.Text.Trim()))
                params(3) = New SqlParameter("@toprod_code", Convert.ToString(toprodcode.Text.Trim()))
                params(4) = New SqlParameter("@dog_partin_id", Convert.ToInt32(Request("id")))
                params(5) = New SqlParameter("@part_code", Convert.ToString(partcode.Text.Trim()))
                params(6) = New SqlParameter("@transnum", Convert.ToInt32(transnum))
                params(7) = New SqlParameter("@createdby", Session("uid"))
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
            Else
                strSQL = "Purchase_Dog_PartIn_Detail_Trans"
                params(0) = New SqlParameter("@order_code", Convert.ToString(ordercode.Text.Trim()))
                params(1) = New SqlParameter("@prod_code", Convert.ToString(prodcode.Text.Trim()))
                params(2) = New SqlParameter("@toorder_code", Convert.ToString(toordercode.Text.Trim()))
                params(3) = New SqlParameter("@toprod_code", Convert.ToString(toprodcode.Text.Trim()))
                params(4) = New SqlParameter("@dog_partin_id", Convert.ToInt32(Request("id")))
                params(5) = New SqlParameter("@part_code", Convert.ToString(partcode.Text.Trim()))
                params(6) = New SqlParameter("@transnum", Convert.ToInt32(transnum))
                params(7) = New SqlParameter("@createdby", Session("uid"))
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
            End If
            Response.Redirect(chk.urlRand("/Purchase/purchasenum.aspx?order_code=" & Request("order_code") & "&code=" & Request("code")), True)


        End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(chk.urlRand("/Purchase/purchasenum.aspx?order_code=" & Request("order_code") & "&code=" & Request("code")), True)
    End Sub
End Class

End Namespace
