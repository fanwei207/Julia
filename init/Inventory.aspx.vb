'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-12-26
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Export Inventory Report
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient


Namespace tcpc


Partial Class Inventory
        Inherits BasePage
    Public chk As New adamClass
    Dim strSql As String
    Dim nRet As Integer
    'Protected WithEvents ltlAlert As Literal

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents chkReprint As System.Web.UI.WebControls.CheckBox


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""
        If Not IsPostBack() Then 
            companyDropDownList()
            Binddata(0)
        End If
        ltlAlert.Text = "Form1.txtCode.focus();"
    End Sub

    Sub companyDropDownList()
        Dim ls As New ListItem

        ls = New ListItem
        ls.Value = 0
        ls.Text = "--"
        ddlCompany.Items.Add(ls)

        ls = New ListItem
        ls.Value = 1
        ls.Text = "SZX"
        ddlCompany.Items.Add(ls)

        ls = New ListItem
        ls.Value = 2
        ls.Text = "ZQL"
        ddlCompany.Items.Add(ls)

        ls = New ListItem
        ls.Value = 3
        ls.Text = "ZF"
        ddlCompany.Items.Add(ls)

        ls = New ListItem
        ls.Value = 4
        ls.Text = "SF"
        ddlCompany.Items.Add(ls)

        ls = New ListItem
        ls.Value = 5
        ls.Text = "ZQZ"
        ddlCompany.Items.Add(ls)

        ls = New ListItem
        ls.Value = 6
        ls.Text = "YQL"
        ddlCompany.Items.Add(ls)

        ls = New ListItem
        ls.Value = 7
        ls.Text = "TCB"
        ddlCompany.Items.Add(ls)

        ddlCompany.SelectedIndex = 0
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If ddlCompany.SelectedValue = 0 Then
            ltlAlert.Text = "alert('必须选择公司！');"
            Exit Sub
        End If
        If txtNo.Text.Trim().Length > 0 Then
            If IsNumeric(txtNo.Text.Trim()) = False Then
                ltlAlert.Text = "alert('打印份数不是数字！');"
                Exit Sub
            ElseIf Val(txtNo.Text.Trim()) < 0 Then
                ltlAlert.Text = "alert('打印份数不能小于零！');"
                Exit Sub
            ElseIf Val(txtNo.Text.Trim()) <> CInt(txtNo.Text.Trim()) Then
                ltlAlert.Text = "alert('打印份数不是整数！');"
                Exit Sub
            End If
        End If
        If txtCode.Text.Trim().Length > 0 Then
            strSql = " Select Count(*) From Inventory Where code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "' And company='" & ddlCompany.SelectedItem.Text.Trim() & "'"
            If CInt(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql)) = 0 Then
                ltlAlert.Text = "alert('" & ddlCompany.SelectedItem.Text.Trim() & "公司不存在" & chk.sqlEncode(txtCode.Text.Trim()) & "的部件！');"
                Exit Sub
            End If
            If txtCard.Text.Trim().Length > 0 Then
                strSql = " Select Count(*) From Inventory Where code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "' And company='" & ddlCompany.SelectedItem.Text.Trim() & "' And card=N'" & chk.sqlEncode(txtCard.Text.Trim()) & "'"
                If CInt(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql)) = 0 Then
                    ltlAlert.Text = "alert('" & ddlCompany.SelectedItem.Text.Trim() & "公司不存在流水号为" & txtCard.Text.Trim() & "的" & chk.sqlEncode(txtCode.Text.Trim()) & "的部件！');"
                    Exit Sub
                End If
            End If
            If txtNo.Text.Trim().Length > 0 Then
                ltlAlert.Text = "var w=window.open('/init/inventoryprint.aspx?code=" & Server.UrlEncode(chk.sqlEncode(txtCode.Text.Trim())) & "&company=" & ddlCompany.SelectedItem.Text.Trim() _
                              & "&no=" & txtNo.Text.Trim() & "&card=" & Server.UrlEncode(chk.sqlEncode(txtCard.Text.Trim())) & "','','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=600,top=300,left=300');w.focus(); "
            Else
                ltlAlert.Text = "var w=window.open('/init/inventoryprint.aspx?code=" & Server.UrlEncode(chk.sqlEncode(txtCode.Text.Trim())) & "&company=" & ddlCompany.SelectedItem.Text.Trim() _
                              & "&card=" & Server.UrlEncode(chk.sqlEncode(txtCard.Text.Trim())) & "','','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=600,top=300,left=300');w.focus(); "
            End If
        ElseIf txtCard.Text.Trim().Length > 0 Then
            strSql = " Select Count(*) From Inventory Where company='" & ddlCompany.SelectedItem.Text.Trim() & "' And card=N'" & chk.sqlEncode(txtCard.Text.Trim()) & "'"
            If CInt(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSql)) = 0 Then
                ltlAlert.Text = "alert('" & ddlCompany.SelectedItem.Text.Trim() & "公司不存在" & txtCard.Text.Trim() & "的流水号！');"
                Exit Sub
            End If
            If txtNo.Text.Trim().Length > 0 Then
                ltlAlert.Text = "var w=window.open('/init/inventoryprint.aspx?company=" & ddlCompany.SelectedItem.Text.Trim() & "&card=" & Server.UrlEncode(chk.sqlEncode(txtCard.Text.Trim())) _
                              & "&no=" & txtNo.Text.Trim() & "','','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=600,top=300,left=300');w.focus(); "
            Else
                ltlAlert.Text = "var w=window.open('/init/inventoryprint.aspx?company=" & ddlCompany.SelectedItem.Text.Trim() & "&card=" & Server.UrlEncode(chk.sqlEncode(txtCard.Text.Trim())) _
                              & "','','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=600,top=300,left=300');w.focus(); "
            End If
        Else
            If txtNo.Text.Trim().Length > 0 Then
                ltlAlert.Text = "var w=window.open('/init/inventoryprint.aspx?company=" & ddlCompany.SelectedItem.Text.Trim() & "&no=" & txtNo.Text.Trim() & "','','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=600,top=300,left=300');w.focus();"
            Else
                ltlAlert.Text = "var w=window.open('/init/inventoryprint.aspx?company=" & ddlCompany.SelectedItem.Text.Trim() & "','','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=600,top=300,left=300');w.focus();"
            End If
        End If
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        dgList.CurrentPageIndex = 0
        Binddata(1)
    End Sub

    Sub Binddata(ByVal ischk As Integer)
        Dim dst As DataSet
        If ischk = 1 Then
            If ddlCompany.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('必须选择公司！');"
                Exit Sub
            End If
        End If
        strSql = " Select company, loc, card, code, status, custodians From Inventory Where company=N'" & ddlCompany.SelectedItem.Text.Trim() & "'"
        If txtCard.Text.Trim().Length > 0 Then
            strSql &= " And Upper(Substring(card,1," & Len(txtCard.Text.Trim()) & "))='" & UCase(Mid(chk.sqlEncode(txtCard.Text.Trim()), 1, Len(txtCard.Text.Trim()))) & "'"
        End If
        If txtCode.Text.Trim().Length > 0 Then
            strSql &= " And code=N'" & chk.sqlEncode(txtCode.Text.Trim()) & "'"
        End If
        strSql &= " Order By loc, id "
        dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("company", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("code", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("loc", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("card", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("status", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("custodians", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("company") = .Rows(i).Item(0).ToString().Trim()
                    drow.Item("loc") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("card") = .Rows(i).Item(2).ToString().Trim()
                    drow.Item("code") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("status") = .Rows(i).Item(4).ToString().Trim()
                    drow.Item("custodians") = .Rows(i).Item(5).ToString().Trim()
                    dtl.Rows.Add(drow)
                Next
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)
        Try
            dgList.DataSource = dvw
            dgList.DataBind()
        Catch
        End Try
    End Sub

    Private Sub dgList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
        dgList.CurrentPageIndex = e.NewPageIndex
        Binddata(1)
        If dgList.CurrentPageIndex > dgList.PageCount - 1 Then
            dgList.CurrentPageIndex = 0
        End If
        Binddata(1)
    End Sub
End Class

End Namespace
