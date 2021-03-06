Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class ItemStruModify
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer
    Dim strsql As String

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
        Dim reader As SqlDataReader
        If Not IsPostBack Then
            'Security check 
            If Request("id") <> Nothing Then
                strsql = " Select code From Items Where ID='" & Request("id") & "'"
                If Request("semi") = "true" Then
                    lblProdCode.Text = "向半成品编号<font size='4pt'>" & SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) & "</font>添加新的结构"
                Else
                    lblProdCode.Text = "向产品型号<font size='4pt'>" & SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) & "</font>添加新的结构"
                End If
            End If
            If Request("psid") <> Nothing Then
                strsql = " Select I.code From Items I Inner Join Product_Stru ps ON i.id=ps.productID Where ps.productStruID='" & Request("psid") & "'"
                If Request("semi") = "true" Then
                    lblProdCode.Text = "修改半成品编号<font size='4pt'>" & SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) & "</font>的结构"
                Else
                    lblProdCode.Text = "修改产品型号<font size='4pt'>" & SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) & "</font>的结构"
                End If
            End If

            If Request("scat") = "PART" Then
                lblTitle.Text = "部件号："
                If Request("psid") <> Nothing Then
                    strsql = " Select I.Code,isnull(ps.numofchild_temp,0),isnull(ps.notes_temp,''), isnull(ps.posCode_temp,'') From Items I Inner Join Product_Stru ps ON I.ID=ps.childID Where ps.productStruID='" & Request("psid") & "' And I.type=0 "
                    reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                    If reader.Read Then
                        txtCode.Text = reader(0)
                        txtCode.Enabled = False
                        txtQty.Text = reader(1)
                        txtMemo.Text = reader(2)
                        txtPos.Text = reader(3)
                        '''''''''''''''''''''''''''''''
                        'load product_replace
                        Dim strPos As String = ""
                        Dim reader1 As SqlDataReader
                        strsql = " Select i.code,i.id From product_replace pr Inner Join Items i On pr.itemID_temp=i.id Where pr.prodStruID='" & Request("psid") & "'"
                        reader1 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                        If reader1.HasRows Then
                            While reader1.Read()
                                strPos &= reader1(0) & ","
                            End While
                        End If
                        reader1.Close()
                        If strPos.Trim() <> "" Then
                            txtRep.Text = strPos.Substring(0, Len(strPos.Trim()) - 1).Trim()
                        Else
                            txtRep.Text = ""
                        End If
                        '''''''''''''''''''''''''''''''
                    End If
                    reader.Close()
                End If
            End If

            If Request("scat") = "PROD" Then
                lblTitle.Text = "产品型号："
                If Request("psid") <> Nothing Then
                    strsql = " Select I.Code,isnull(ps.numofchild_temp,0),isnull(ps.notes_temp,''), isnull(ps.posCode_temp,'') From Items I Inner Join Product_Stru ps ON I.ID=ps.childID Where ps.productStruID='" & Request("psid") & "' And I.type<>0 "
                    reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                    If reader.Read Then
                        txtCode.Text = reader(0)
                        txtCode.Enabled = False
                        txtQty.Text = reader(1)
                        txtMemo.Text = reader(2)
                        txtPos.Text = reader(3)
                        '''''''''''''''''''''''''''''''
                        'load product_replace
                        Dim strPos As String = ""
                        Dim reader1 As SqlDataReader
                        strsql = " Select i.code,i.id From product_replace pr Inner Join Items i On pr.itemID_temp=i.id Where pr.prodStruID='" & Request("psid") & "'"
                        reader1 = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                        If reader1.HasRows Then
                            While reader1.Read()
                                strPos &= reader1(0) & ","
                            End While
                        End If
                        reader1.Close()
                        If strPos.Trim() <> "" Then
                            txtRep.Text = strPos.Substring(0, Len(strPos.Trim()) - 1).Trim()
                        Else
                            txtRep.Text = ""
                        End If
                        '''''''''''''''''''''''''''''''
                    End If
                    reader.Close()
                End If
            End If
            If txtCode.Enabled = True Then
                ltlAlert.Text = "Form1.txtCode.focus();"
            Else
                ltlAlert.Text = "Form1.txtQty.focus();"
            End If
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim strsql, pID As String

        If txtCode.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('" & lblTitle.Text.Trim() & "的内容不能为空！');"
            Exit Sub
        ElseIf txtQty.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('数量不能为空！');"
            Exit Sub
        ElseIf Val(txtQty.Text.Trim()) <= 0 Then
            ltlAlert.Text = "alert('数量不能小于等于零！');"
            Exit Sub
        End If
        If Request("scat") = "PART" And Request("psid") = Nothing Then
            strsql = " Select ID From Items Where Code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "' And status<>2 and ID<>" & Request("id")
            pID = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)
        End If
        If Request("scat") = "PROD" And Request("psid") = Nothing Then
            strsql = " Select ID From Items Where Code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "' And type<>0 And status<>2 and ID<>" & Request("id")
            pID = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql)
        End If
        If (pID = Nothing Or pID <= "0") And txtCode.Enabled = True Then
            ltlAlert.Text = "alert('输入的" & lblTitle.Text.Trim() & txtCode.Text.Trim() & "不存在或选的是嵌套！');"
            Exit Sub
        Else
            If Request("psid") = Nothing Then
                Dim ProdStruID As Integer
                ProdStruID = 0
                ' Modified By LY 11.20.2006
                If Request("scat") = "PART" Then
                    Dim param(7) As SqlParameter
                    strsql = "insertreplace"
                    param(0) = New SqlParameter("@productID", CInt(Request("id")))
                    param(1) = New SqlParameter("@childID", CInt(pID.Trim))
                    param(2) = New SqlParameter("@numofchild_temp", cdbl(txtQty.Text.Trim()))
                    param(3) = New SqlParameter("@childcategory", "PART")
                    param(4) = New SqlParameter("@notes_temp", chk.sqlEncode(txtMemo.Text.Trim()))
                    param(5) = New SqlParameter("@plantcode", Session("plantcode"))
                    param(6) = New SqlParameter("@posCode_temp", chk.sqlEncode(txtPos.Text.Trim()))
                    param(7) = New SqlParameter("@repcode", chk.sqlEncode(txtRep.Text.Trim()))

                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, strsql, param)
                End If

                If Request("scat") = "PROD" Then
                    Dim param(7) As SqlParameter
                    strsql = "insertreplace"
                    param(0) = New SqlParameter("@productID", CInt(Request("id")))
                    param(1) = New SqlParameter("@childID", CInt(pID.Trim))
                    param(2) = New SqlParameter("@numofchild_temp", cdbl(txtQty.Text.Trim()))
                    param(3) = New SqlParameter("@childcategory", "PROD")
                    param(4) = New SqlParameter("@notes_temp", chk.sqlEncode(txtMemo.Text.Trim()))
                    param(5) = New SqlParameter("@plantcode", Session("plantcode"))
                    param(6) = New SqlParameter("@posCode_temp", chk.sqlEncode(txtPos.Text.Trim()))
                    param(7) = New SqlParameter("@repcode", chk.sqlEncode(txtRep.Text.Trim()))

                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, strsql, param)
                End If

            Else
                'pID = Request("cID").Trim()
                If Request("scat") = "PART" Then
                    Dim param(6) As SqlParameter
                    strsql = "editreplace"
                    param(0) = New SqlParameter("@numofchild_temp", cdbl(txtQty.Text.Trim()))
                    param(1) = New SqlParameter("@childcategory", "PART")
                    param(2) = New SqlParameter("@notes_temp", chk.sqlEncode(txtMemo.Text.Trim()))
                    param(3) = New SqlParameter("@plantcode", Session("plantcode"))
                    param(4) = New SqlParameter("@posCode_temp", chk.sqlEncode(txtPos.Text.Trim()))
                    param(5) = New SqlParameter("@repcode", chk.sqlEncode(txtRep.Text.Trim()))
                    param(6) = New SqlParameter("@prodstruid", CInt(Request("psid")))
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, strsql, param)

                End If
                If Request("scat") = "PROD" Then
                    Dim param(6) As SqlParameter
                    strsql = "editreplace"
                    param(0) = New SqlParameter("@numofchild_temp", cdbl(txtQty.Text.Trim()))
                    param(1) = New SqlParameter("@childcategory", "PROD")
                    param(2) = New SqlParameter("@notes_temp", chk.sqlEncode(txtMemo.Text.Trim()))
                    param(3) = New SqlParameter("@plantcode", Session("plantcode"))
                    param(4) = New SqlParameter("@posCode_temp", chk.sqlEncode(txtPos.Text.Trim()))
                    param(5) = New SqlParameter("@repcode", chk.sqlEncode(txtRep.Text.Trim()))
                    param(6) = New SqlParameter("@prodstruid", CInt(Request("psid")))
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, strsql, param)
                End If
            End If

            Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
        End If
    End Sub

    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        If Request("id") <> Nothing Then
            If Request("semi") <> Nothing Then
                If Request("st") <> Nothing Then
                    If Request("code") <> Nothing Then
                        If Request("cat") <> Nothing Then
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&code=" & Request("code") & "&cat=" & Request("cat") & "&st=" & Request("st") & ""), True)
                            End If
                        Else
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&code=" & Request("code") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&code=" & Request("code") & "&st=" & Request("st") & ""), True)
                            End If
                        End If
                    Else
                        If Request("cat") <> Nothing Then
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&cat=" & Request("cat") & "&st=" & Request("st") & ""), True)
                            End If
                        Else
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&st=" & Request("st") & ""), True)
                            End If
                        End If
                    End If
                Else
                    If Request("code") <> Nothing Then
                        If Request("cat") <> Nothing Then
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&code=" & Request("code") & "&cat=" & Request("cat")), True)
                            End If
                        Else
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&code=" & Request("code") & "&pg=" & Request("cat")), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&code=" & Request("code")), True)
                            End If
                        End If
                    Else
                        If Request("cat") <> Nothing Then
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&cat=" & Request("cat")), True)
                            End If
                        Else
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&semi=true&pg=" & Request("pg")), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?&semi=true&id=" & Request("id")), True)
                            End If
                        End If
                    End If
                End If
            Else
                If Request("st") <> Nothing Then
                    If Request("code") <> Nothing Then
                        If Request("cat") <> Nothing Then
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&st=" & Request("st") & ""), True)
                            End If
                        Else
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&st=" & Request("st") & ""), True)
                            End If
                        End If
                    Else
                        If Request("cat") <> Nothing Then
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&st=" & Request("st") & ""), True)
                            End If
                        Else
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&pg=" & Request("pg") & "&st=" & Request("st") & ""), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&st=" & Request("st") & ""), True)
                            End If
                        End If
                    End If
                Else
                    If Request("code") <> Nothing Then
                        If Request("cat") <> Nothing Then
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&cat=" & Request("cat")), True)
                            End If
                        Else
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code") & "&pg=" & Request("cat")), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&code=" & Request("code")), True)
                            End If
                        End If
                    Else
                        If Request("cat") <> Nothing Then
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&cat=" & Request("cat")), True)
                            End If
                        Else
                            If Request("pg") <> Nothing Then
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id") & "&pg=" & Request("pg")), True)
                            Else
                                Response.Redirect(chk.urlRand("/product/ItemStruEdit.aspx?id=" & Request("id")), True)
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub
End Class

End Namespace
