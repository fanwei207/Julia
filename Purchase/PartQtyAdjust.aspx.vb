'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-6-22
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Adjust Part Warehouse Quantity
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
Partial Class PartQtyAdjust
        Inherits BasePage

    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim strsql As String
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
        If Not Page.IsPostBack Then
            Dim cnt As Integer = -1
                   If Not Me.Security("20030900").isValid Then
                    ltlAlert.Text = "alert('没有权限进行库存调整！'); window.close();"
                End If
            If Session("uRole") <> 1 Then
                strsql = " Select Count(*) From User_Warehouse Where warehouseID='" & Request("placeID") & "' And userID='" & Session("uID") & "'"
                cnt = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strsql)
            Else
                cnt = 1
            End If
            If cnt <= 0 Then
                ltlAlert.Text = "alert('没有权限对" & Request("place") & "仓库进行库存调整！'); window.close();"
            End If
            If Request("name") <> "--" Then
                lblTitle.Text = Server.UrlDecode(Request("place")) & "仓库的" & "状态为" & Server.UrlDecode(Request("name")) & "的" _
                              & Server.UrlDecode(Request("partcode")) & "/" & Server.UrlDecode(Request("partdesc"))
            Else
                lblTitle.Text = Server.UrlDecode(Request("place")) & "仓库的" & Server.UrlDecode(Request("partcode")) _
                              & "/" & Server.UrlDecode(Request("partdesc"))
            End If
            ltlAlert.Text = "Form1.txtEnterDate.focus();"
        End If
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Dim strdate As String
        Dim strqty As String
        Dim diffQty As Decimal = 0.0
        Dim preQty As String = Nothing
        Dim totalQty As Decimal = 0.0
        Dim numMaxTransID As Integer

        If txtEnterDate.Text.Trim().Length > 0 Then
            If IsDate(txtEnterDate.Text.Trim()) = False Then
                ltlAlert.Text = "alert('日期非法！');"
                Exit Sub
            Else
                strdate = CDate(txtEnterDate.Text.Trim())
            End If
        Else
            ltlAlert.Text = "alert('日期不能为空！');"
            Exit Sub
        End If
        If txtPartQty.Text.Trim().Length > 0 Then
            If IsNumeric(txtPartQty.Text.Trim()) = True Then
                If Val(txtPartQty.Text.Trim()) > 0 Then
                    strqty = txtPartQty.Text.Trim()
                Else
                    ltlAlert.Text = "alert('数量不能小于等于零！');"
                    Exit Sub
                End If
            Else
                ltlAlert.Text = "alert('数量不是数字！');"
                Exit Sub
            End If
        Else
            ltlAlert.Text = "alert('数量不能为空！');"
            Exit Sub
        End If

        diffQty = CDec(strqty) - CDec(Request("totalQty"))

        strsql = " Insert Into part_tran(part_id, tran_date, tran_qty, tran_type, createdBy, createdDate, warehouseID, status) " _
               & " Values('" & Request("partID") & "','" & strdate & "','" & diffQty & "','ADJ','" & Session("uID") & "','" _
               & DateTime.Now() & "','" & Request("placeID") & "','" & Request("st") & "')"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql)

        strsql = " Select isnull(total_qty,0) From Part_inv Where part_id='" & Request("partID") & "' And warehouseID='" & Request("placeID") & "' And Isnull(status,0)='" & Request("st") & "'"
        preQty = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strsql)

        If preQty = Nothing Then
            strsql = " Insert Into Part_inv(part_id, total_qty, order_qty, modifiedBy, modifiedDate, warehouseID, status) " _
                   & " Values('" & Request("partID") & "','" & diffQty & "','0','" & Session("uID") & "','" & DateTime.Now() _
                   & "','" & Request("placeID") & "','" & Request("st") & "')"
        Else
            totalQty = diffQty + CDec(preQty)
            strsql = " Update Part_inv Set total_qty=" & totalQty & "," _
                   & " modifiedBy='" & Session("uID") & "'," _
                   & " modifiedDate='" & DateTime.Now() & "'" _
                   & " Where part_id='" & Request("partID") & "' And warehouseID='" & Request("placeID") & "' And Isnull(status,0)='" & Request("st") & "'"
        End If
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql)

        ltlAlert.Text = "alert('库存调整成功！');window.close();window.opener.location.replace('/Purchase/SearchbyPartCode.aspx?code=" & Request("code") _
                      & "&cat=" & Request("cat") & "&placeID=" & Request("placeID") & "&st=" & Request("st") & "');"
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        ltlAlert.Text = "window.close();"
    End Sub
End Class

End Namespace

