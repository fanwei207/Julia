'*@     Create By   :   Nai Qi    
'*@     Create Date :   2005-6-18
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   edit supplies and part link 
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient


Namespace tcpc 
Partial Class partsupplylinks
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
    Dim reader As SqlDataReader

    Dim dst As DataSet
    Dim strTemp As String
    Dim strParamquery As String = "sp_setSystemChanges"
    Dim param As New SqlParameter
    Dim sysparam(2) As SqlParameter

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents txtprodcode As System.Web.UI.WebControls.TextBox
    Protected WithEvents unit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtorderqty As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtprice As System.Web.UI.WebControls.TextBox


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
                Dim nRet As Boolean 
                If Request("semi") = "true" Then
                    nRet = Me.Security("19031000").isValid
                    'chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19031000, False, True)
                Else
                    'nRet = Me.Security("19023311").isValid
                    nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19023311, False, True)
                End If
                If Not nRet Then
                    If Request("semi") = "true" Then
                        ltlAlert.Text = "alert('没有权限查看半成品的供应商！');window.close();"
                    Else
                        ltlAlert.Text = "alert('没有权限查看材料的供应商！');window.close();"
                    End If
                    Exit Sub
                End If

                strSql = " Select code, isnull(description,'') From Items Where id='" & Request("id") & "'"
                dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql)
                If dst.Tables(0).Rows.Count > 0 Then
                    title.Text = dst.Tables(0).Rows(0).Item(0).ToString.Trim & "&nbsp;&nbsp;&nbsp;&nbsp;" & dst.Tables(0).Rows(0).Item(1).ToString.Trim
                End If
                dst.Reset()

                dwStatus.SelectedIndex = 2
                BindData()
            End If
    End Sub

    Sub BindData()
        'Dim strstatus As String
        strSql = " Select c.company_id,c.company_code, isnull(cp.status,2), isnull(cp.comments,''), cp.createdDate, u.userName ,c.company_name " _
               & " From tcpc0.dbo.company_part cp " _
               & " Inner Join tcpc0.dbo.companies c On c.company_id = cp.companyid " _
               & " Inner Join tcpc0.dbo.Items i On i.id=cp.partID left outer Join tcpc0.dbo.users u on u.userID=cp.modifiedBy" _
               & " Where cp.partid='" & Request("id") & "'"
        strSql &= " Order By i.code "

        dst = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, strSql)

        Session("EXHeader1") = "部件号~" & title.Text & "~^日期~" & Format(DateTime.Now, "yyyy-MM-dd") & "~^状态~0-使用 1-试验 2-停用~^"
        Session("EXSQL1") = strSql
        Session("EXTitle1") = "<b>公司代码</b>~^<b>状态</b>~^200^<b>说明</b>~^<b>开始日期</b>~^<b>批准</b>~^L~800^<b>公司名称</b>~^"

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("_partcode", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("_partstatus", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("_partcomm", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("_partdate", System.Type.GetType("System.DateTime")))
        dtl.Columns.Add(New DataColumn("_partappr", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("_partdesc", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("_pid", System.Type.GetType("System.String")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("_partcode") = .Rows(i).Item(1).ToString().Trim()
                    Select Case .Rows(i).Item(2)
                        Case 0
                            drow.Item("_partstatus") = "使用"
                        Case 1
                            drow.Item("_partstatus") = "<font color=blue>试验</font>"
                        Case 2
                            drow.Item("_partstatus") = "<font color=red>停用</font>"
                    End Select
                    drow.Item("_partdate") = .Rows(i).Item(4)
                    drow.Item("_partcomm") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("_partappr") = .Rows(i).Item(5).ToString().Trim()
                    drow.Item("_partdesc") = .Rows(i).Item(6).ToString().Trim()
                    drow.Item("_pid") = .Rows(i).Item(0).ToString().Trim()
                    dtl.Rows.Add(drow)
                Next
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl)
      
        Try
            dvw.Sort = Session("orderby1") & Session("orderdir1")
            dgOrder.DataSource = dvw
            dgOrder.DataBind()
        Catch
        End Try
    End Sub

    Public Sub cmdReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ltlAlert.Text = "window.close();"
    End Sub

    Public Sub dgOrder_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgOrder.SelectedIndexChanged
        lblID.Text = dgOrder.SelectedItem.Cells(7).Text.Trim()

        If lblID.Text.Trim() = "" Then
            ltlAlert.Text = "alert('请选择要修改的公司！');"
            Exit Sub
        End If
        txtPartCode.Text = dgOrder.SelectedItem.Cells(1).Text.Trim()
        txtComments.Text = dgOrder.SelectedItem.Cells(3).Text.Trim()
        If txtComments.Text = "&nbsp;" Then
            txtComments.Text = ""
        End If
        Select Case dgOrder.SelectedItem.Cells(2).Text.Trim()
            Case "使用"
                dwStatus.SelectedIndex = 0
            Case "<font color=blue>试验</font>"
                dwStatus.SelectedIndex = 1
            Case "<font color=red>停用</font>"
                dwStatus.SelectedIndex = 2
        End Select
        txtPartCode.Enabled = False
        BtnAddNew.Text = "保存修改"
    End Sub

    Private Sub BtnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddNew.Click
            If Not Me.Security("19023312").isValid Then
                ltlAlert.Text = "alert('没有权限修改！');"
                Exit Sub
            End If

        txtPartCode.Enabled = True

        If txtPartCode.Text.Trim().Length <= 0 Then
            ltlAlert.Text = "alert('公司代码不能为空！');"
            Exit Sub
        End If

        If BtnAddNew.Text = "添加" Then
            Dim pid As String = ""
            strSql = " Select company_id From Companies Where company_code=N'" & chk.sqlEncode(txtPartCode.Text.Trim()) & "'"
            dst = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql)
            If dst.Tables(0).Rows.Count <= 0 Then
                dst.Reset()
                ltlAlert.Text = "alert('公司代码不存在！');"
                Exit Sub
            Else
                pid = dst.Tables(0).Rows(0).Item(0).ToString().Trim()
            End If
            dst.Reset()

            strSql = " Select * From company_part Where companyid='" & pid & "' and partid=" & Request("id")
            reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSql)
            While reader.Read
                reader.Close()
                ltlAlert.Text = "alert('" & txtPartCode.Text.Trim() & "此公司代码已经存在!');"
                Exit Sub
            End While
            reader.Close()
            strSql = " Insert into company_part(companyid,partid,status,comments,createdBy,createdDate,modifiedBy,modifiedDate)Values('" & pid & "','" & Request("id") & "'," & dwStatus.SelectedValue & ",'" & txtComments.Text.Trim & "'," & Session("uID") & ",getdate()," & Session("uID") & ",getdate())"
            Try
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSql)
            Catch
            End Try
        Else
            If lblID.Text.Trim() = "" Then
                ltlAlert.Text = "alert('请选择要修改的公司！');"
                Exit Sub
            End If

            strSql = " Update company_part Set status='" & dwStatus.SelectedValue & "'," _
                   & " comments='" & txtComments.Text.Trim() & "'," _
                   & " modifiedBy='" & Session("uID") & "'," _
                   & " modifiedDate=getdate()" _
                   & " Where companyid='" & lblID.Text.Trim() & "' and partid=" & Request("id")
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSql)
        End If

        Response.Redirect(chk.urlRand("/supply/partsupplylinks.aspx?bk=" & Request("bk") & "&id=" & Request("id")), True)
    End Sub 
End Class

End Namespace
