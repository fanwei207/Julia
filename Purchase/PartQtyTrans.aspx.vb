'*@     Create By   :   Ye Bin    
'*@     Create Date :   2006-12-11
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Transfer Part Warehouse Quantity To Rejects
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

Partial Class PartQtyTran
        Inherits BasePage 
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim reader As SqlDataReader
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
                If Not Me.Security("20030504").isValid Then
                    ltlAlert.Text = "alert('没有权限进行库存废品转出！'); window.close();"
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
            warehousedrop()
            statusdrop()
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

    'warehouse dropdown bind Value
    Sub warehousedrop()
        warehouse.Items.Add(New ListItem("--", "0"))
        strSql = " Select w.warehouseID, w.name From warehouse w "
        If Session("uRole") <> 1 Then
            strSql &= " Inner Join User_Warehouse uw On uw.warehouseID = w.warehouseID Where uw.userID='" & Session("uID") & "'"
        End If
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read()
            Dim tempListItem As New ListItem
            tempListItem.Value = reader(0)
            tempListItem.Text = reader(1)
            warehouse.Items.Add(tempListItem)
        End While
        reader.Close()
    End Sub

    Sub statusdrop()
        status.Items.Add(New ListItem("--", "0"))
        strsql = " Select id, StatusName From tcpc0.dbo.Status "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read()
            Dim tempListItem As New ListItem
            tempListItem.Value = reader(0)
            tempListItem.Text = reader(1)
            status.Items.Add(tempListItem)
        End While
        reader.Close()
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Dim strdate As String
        Dim strqty As String

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

        If warehouse.SelectedIndex = 0 Then
            ltlAlert.Text = "alert('转入废品仓库必须选择！');"
            Exit Sub
        End If

        If txtNotes.Text.Trim().Length > 255 Then
            ltlAlert.Text = "alert('备注长度不能超过255！');"
            Exit Sub
        End If

        strsql = "Purchase_PartTrans"
        Dim ret As Integer
        Dim params(8) As SqlParameter
        params(0) = New SqlParameter("@placeid", Convert.ToInt32(Request("placeID")))
        params(1) = New SqlParameter("@whid", Convert.ToInt32(warehouse.SelectedValue))
        params(2) = New SqlParameter("@partid", Convert.ToInt32(Request("partID")))
        params(3) = New SqlParameter("@tranQty", Convert.ToDecimal(txtPartQty.Text.Trim))
        params(4) = New SqlParameter("@tranDate", Convert.ToDateTime(txtEnterDate.Text.Trim))
        params(5) = New SqlParameter("@notes", chk.sqlEncode(txtNotes.Text.Trim))
        params(6) = New SqlParameter("@status", Convert.ToInt32(status.SelectedValue))
        params(7) = New SqlParameter("@st", Convert.ToInt32(Request("st")))
        params(8) = New SqlParameter("@intUserID", Convert.ToInt32(Session("uID")))
        
        ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strsql, params)
        If ret < 0 Then
            strsql = " Select top 1 ErrorInfo From PartQtyImportError where userID=" & Session("uID")
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)
            If reader.Read() Then
                ltlAlert.Text = "alert('" & reader(0) & "'); Form1.txtOrderCode.focus();"
                reader.Close()
                Exit Sub
            End If
            reader.Close()
            ltlAlert.Text = "alert('库存转废品失败！');window.close();window.opener.location.replace('/Purchase/SearchbyPartCode.aspx?code=" & Request("code") _
                      & "&cat=" & Request("cat") & "&placeID=" & Request("placeID") & "&st=" & Request("st") & "');"
        Else
            ltlAlert.Text = "alert('库存转废品成功！');window.close();window.opener.location.replace('/Purchase/SearchbyPartCode.aspx?code=" & Request("code") _
                      & "&cat=" & Request("cat") & "&placeID=" & Request("placeID") & "&st=" & Request("st") & "');"
        End If

    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        ltlAlert.Text = "window.close();"
    End Sub
End Class

End Namespace
