Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.Odbc


Namespace tcpc
    Partial Class wo_group_copy
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim nRet As Integer
        Dim item As ListItem


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
            If Not IsPostBack Then
                Dim ls As ListItem
                Select Case Session("PlantCode")
                    Case "1"
                        ls = New ListItem
                        ls.Value = "1000"
                        ls.Text = "1000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2100"
                        ls.Text = "2100"
                        dd_site.Items.Add(ls)
                    Case "2"
                        ls = New ListItem
                        ls.Value = "2000"
                        ls.Text = "2000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1200"
                        ls.Text = "1200"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "3000"
                        ls.Text = "3000"
                        dd_site.Items.Add(ls)
                    Case "5"
                        ls = New ListItem
                        ls.Value = "4000"
                        ls.Text = "4000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1400"
                        ls.Text = "1400"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2400"
                        ls.Text = "2400"
                        dd_site.Items.Add(ls)
                End Select

                If Request("site") <> Nothing Then
                    lbl_site.Text = Request("site")
                End If
                If Request("cc") <> Nothing Then
                    lbl_cc.Text = Request("cc")
                End If
                If Request("gp") <> Nothing Then
                    lbl_group.Text = Request("gp")
                End If
            End If
        End Sub

        Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
            Response.Redirect("wo_group.aspx?site=" & Request("site") & "&cc=" & Request("cc") & "&gp=" & Request("gp"))
        End Sub

        Protected Sub btn_copy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_copy.Click

            If Session("uRole") <> 1 Then
                StrSql = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & txb_cc.Text & "' and perm_userid='" & Session("uID") & "'"
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) <= 0 Then
                    ltlAlert.Text = "alert('无操作此成本中心的权限.')"
                    Exit Sub
                End If
            End If

            If txb_cc.Text = "" Then
                ltlAlert.Text = "alert('请输入新的成本中心.')"
                txb_cc.Focus()
                Exit Sub
            End If
            If txb_name.Text = "" Then
                ltlAlert.Text = "alert('请输入新的用户组的名称.')"
                txb_name.Focus()
                Exit Sub
            End If

            StrSql = " select wog_id from tcpc0.dbo.wo_group where deletedBy is null and wog_site='" & dd_site.SelectedValue & "' and wog_cc='" & txb_cc.Text & " ' and wog_name=N'" & txb_name.Text & "'"
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql) > 0 Then
                ltlAlert.Text = "alert('此用户组已存在.')"
                txb_name.Focus()
                Exit Sub
            End If

            Dim str2 As String = ""
            Dim id2 As Integer = 0
            StrSql = "Insert into tcpc0.dbo.wo_group(wog_site,wog_cc,wog_name,createdDate,createdBy) values('" & dd_site.SelectedValue & "','" & txb_cc.Text & "',N'" & txb_name.Text & "',getdate(),'" & Session("uID") & "')   Select @@IDENTITY"
            id2 = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)
            If id2 > 0 Then
                'Response.Write(id2)
                StrSql = " select wod_user_id,wod_user_no,wod_user_name,isnull(wod_rate,1) from tcpc0.dbo.wo_group_detail where wod_group_id='" & Request("gid") & "'"
                'Response.Write(StrSql)
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If .Rows.Count > 0 Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            str2 = "Insert into tcpc0.dbo.wo_group_detail(wod_site,wod_cc,wod_group_id,wod_group_name,wod_user_id,wod_user_no,wod_user_name,wod_rate,createdDate,createdBy) "
                            str2 &= " values('" & dd_site.SelectedValue & "','" & txb_cc.Text & "','" & id2.ToString() & "',N'" & txb_name.Text & "','" & .Rows(i).Item(0).ToString().Trim() & "','" & .Rows(i).Item(1).ToString().Trim() & "',N'" & .Rows(i).Item(2).ToString().Trim() & "','" & .Rows(i).Item(3).ToString().Trim() & "',getdate(),'" & Session("uID") & "')"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str2)
                            'Response.Write(str2)
                        Next
                    End If
                End With
                ds.Reset()
            End If


            Response.Redirect("wo_group.aspx?site=" & Request("site") & "&cc=" & Request("cc") & "&gp=" & Request("gp"))
        End Sub
    End Class

End Namespace













