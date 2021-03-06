'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-4-4
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Replace Product code And Product Category
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class productreplace
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
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
            If Not IsPostBack Then

                BtnBatch.Enabled = False
                BtnExport.Enabled = False
                BtnImport.Enabled = False
            End If
        End Sub

        Private Sub BtnBatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBatch.Click
            ltlAlert.Text = ""
            Dim strsql, strcode, strquery As String
            Dim reader As SqlDataReader
            Dim param(3) As SqlParameter
            Dim numLS As Integer
            Dim numLS1 As Integer
            Dim numVer As Integer
            Dim numSubVer As Integer
            Dim strItem As String

            If txtSourceCode.Enabled = True Then
                If txtSourceCode.Text.Trim().Length <= 0 Then
                    ltlAlert.Text = "alert('原产品型号不能为空！');txtSourceCode.focus();"
                    Exit Sub
                ElseIf txtTargetCode.Text.Trim().Length <= 0 Then
                    ltlAlert.Text = "alert('修改后产品型号不能为空！');txtTargetCode.focus();"
                    Exit Sub
                ElseIf txtTargetCode.Text.Trim().IndexOf(" ") <> -1 Or txtTargetCode.Text.Trim().IndexOf("/") <> -1 Or txtTargetCode.Text.Trim().IndexOf("_") <> -1 Or txtTargetCode.Text.Trim().IndexOf("\") <> -1 Then
                    ltlAlert.Text = "alert('修改后产品型号不能包含空格，斜杠，下划线！');"
                    Exit Sub
                Else

                    strquery = "Update Items set itemNumber=replace(itemNumber,'" & txtSourceCode.Text.Trim().ToUpper() & "','" & txtTargetCode.Text.Trim().ToUpper() & "') , " _
                         & "code= case when itemversion>0 then 'LS'+convert(nvarchar,itemversion)+'-'+convert(nvarchar,itemsubversion)+replace(code,'" & txtSourceCode.Text.Trim().ToUpper() & "','" & txtTargetCode.Text.Trim().ToUpper() & "') " _
                         & "else   replace(code,'" & txtSourceCode.Text.Trim().ToUpper() & "','" & txtTargetCode.Text.Trim().ToUpper() & "')   end  , " _
                         & " modifiedBy='" & Session("uID") & "'," _
                         & " modifiedDate=getdate() where type<>0  and status<>2 "
                    SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strquery)
                    If Request("semi") = "true" Then
                        ltlAlert.Text = "alert('替换成功！');window.close();if(window.opener){window.opener.location.href='/product/SemisList.aspx?code=" _
                                      & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "';}"
                    Else
                        ltlAlert.Text = "alert('替换成功！');window.close();if(window.opener){window.opener.location.href='/product/productlist.aspx?code=" _
                                      & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "';}"
                    End If
                End If
            End If
            If txtSourceCategory.Enabled = True Then
                If txtSourceCategory.Text.Trim().Length <= 0 Then
                    ltlAlert.Text = "alert('原分类不能为空！');txtSourceCode.focus();"
                    Exit Sub
                ElseIf txtTargetCategory.Text.Trim().Length <= 0 Then
                    ltlAlert.Text = "alert('修改后分类不能为空！');txtTargetCode.focus();"
                    Exit Sub
                Else
                    strsql = " Select id From ItemCategory Where name=N'" & chk.sqlEncode(txtSourceCategory.Text.Trim()) & "' and type<>0 "
                    reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strsql)
                    While reader.Read()
                        param(0) = New SqlParameter("@categoryName", chk.sqlEncode(txtTargetCategory.Text.Trim()))
                        param(1) = New SqlParameter("@intUserID", Session("uID"))
                        param(2) = New SqlParameter("@type", "2")
                        param(3) = New SqlParameter("@intPlant", Session("plantCode"))
                        strcode = SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "Item_SetCategory", param)
                        strquery = " Update Items Set category='" & strcode & "'," _
                                 & " modifiedBy='" & Session("uID") & "'," _
                                 & " modifiedDate=getdate()," _
                                 & " plantcode='" & Session("plantcode") & "'" _
                                 & " Where category='" & reader(0) & "'"
                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strquery)
                    End While
                    If Request("semi") = "true" Then
                        ltlAlert.Text = "alert('替换成功！');window.close();if(window.opener){window.opener.location.href='/product/SemisList.aspx?code=" _
                                      & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "';}"
                    Else
                        ltlAlert.Text = "alert('替换成功！');window.close();if(window.opener){window.opener.location.href='/product/productlist.aspx?code=" _
                                      & Request("code") & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=" & Request("st") & "';}"
                    End If
                End If
            End If
        End Sub

        Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExport.Click
            ltlAlert.Text = ""
            If txtSourceCode.Enabled = True Then
                If txtSourceCode.Text.Trim().Length > 0 Then
                    ltlAlert.Text = "window.close(); var w=window.open('/product/productprint.aspx?code=" & chk.sqlEncode(txtSourceCode.Text.Trim()) & "','productprint','menubar=yes,scrollbars = yes,resizable=yes,width=750,height=500,top=0,left=0');w.focus(); "
                Else
                    ltlAlert.Text = "alert('原产品型号不能为空！');txtSourceCode.focus();"
                End If
            End If
            If txtSourceCategory.Enabled = True Then
                If txtSourceCategory.Text.Trim().Length > 0 Then
                    ltlAlert.Text = "window.close(); var w=window.open('/product/productprint.aspx?cat=" & chk.sqlEncode(txtSourceCategory.Text.Trim()) & "','productprint','menubar=yes,scrollbars = yes,resizable=yes,width=750,height=500,top=0,left=0'); w.focus();"
                Else
                    ltlAlert.Text = "window.close(); var w=window.open('/product/productprint.aspx?all=true','productprint','menubar=yes,scrollbars = yes,resizable=yes,width=750,height=500,top=0,left=0'); w.focus();"
                End If
            End If
        End Sub

        Private Sub BtnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImport.Click
            If Request("semi") = "true" Then
                ltlAlert.Text = "window.location.href='/product/productreplaceimport.aspx?semi=true';"
            Else
                ltlAlert.Text = "window.location.href='/product/productreplaceimport.aspx';"
            End If
        End Sub

        Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
            Response.Redirect("productlist.aspx?code=" + Request.QueryString("code"))
        End Sub

        Private Sub RadCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadCategory.CheckedChanged
            txtSourceCode.Enabled = False
            txtTargetCode.Enabled = False
            txtSourceCategory.Enabled = True
            txtTargetCategory.Enabled = True
            txtSourceCode.Text = ""
            txtTargetCode.Text = ""
            BtnBatch.Enabled = True
            BtnExport.Enabled = True
            BtnImport.Enabled = True
            ltlAlert.Text = "document.getElementById('txtSourceCategory').focus();"
        End Sub

        Private Sub RadCode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadCode.CheckedChanged
            txtSourceCode.Enabled = True
            txtTargetCode.Enabled = True
            txtSourceCategory.Enabled = False
            txtTargetCategory.Enabled = False
            txtSourceCategory.Text = ""
            txtTargetCategory.Text = ""
            BtnBatch.Enabled = True
            BtnExport.Enabled = True
            BtnImport.Enabled = True
            ltlAlert.Text = "document.getElementById('txtSourceCode').focus();"
        End Sub
    End Class

End Namespace
