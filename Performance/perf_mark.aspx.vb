Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Web.Mail


Namespace tcpc


Partial Class perf_mark
    Inherits System.Web.UI.Page
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
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
            'check 
            If Request("mid") <> Nothing Then
                    StrSql = "SELECT perf_type,perf_cause,perf_notes,perf_fpath,perf_fname From perf_mstr where perf_mstr_id ='" & Request("mid") & "' and perf_deletedby is null and perf_hist_id is null"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        lbl_type.Text = .Rows(0).Item(0).ToString()
                        txb_cause.Text = .Rows(0).Item(1).ToString()
                            txb_comm.Text = .Rows(0).Item(2).ToString()

                            If .Rows(0).Item(3).ToString() <> "" Then
                                lbldoc.Text = .Rows(0).Item(3).ToString()
                                lbn_doc.Text = .Rows(0).Item(4).ToString()
                                lblup.Visible = True
                                lbn_doc.Visible = True
                            End If
                    End If
                End With
                ds.Reset()

                Dim ls As ListItem

                ls = New ListItem("--")
                ls.Value = 0
                    dd_dept.Items.Add(ls)
                    dd_dept.SelectedIndex = 0

                StrSql = "SELECT departmentID,name From departments where isnull(active,0)=1 and isnull(isSalary,0)=1 order by departmentID "
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            ls = New ListItem(.Rows(i).Item(1))
                            ls.Value = Convert.ToInt32(.Rows(i).Item(0))
                            dd_dept.Items.Add(ls)
                        Next
                    End If
                End With
                ds.Reset()

                loadUser()

            End If

        End If
    End Sub

    Sub loadUser()
        If dd_user.Enabled = True Then
            While dd_user.Items.Count > 0
                dd_user.Items.RemoveAt(0)
            End While

            If dd_dept.SelectedIndex > 0 Then
                Dim ls As ListItem

                    ls = New ListItem("--")
                    ls.Value = 0
                    dd_user.Items.Add(ls)
                    dd_user.SelectedIndex = 0

                Dim reader As SqlDataReader
                    StrSql = "select userid,userno,username from tcpc0.dbo.Users where roleID<>1 and plantCode = '" & Session("PlantCode") & "' and isnull(isactive,0)=1 and isnull(deleted,0) =0 and (leaveDate is null or datediff(month,leaveDate,getdate()) <=1 ) and departmentid='" & dd_dept.SelectedValue & "' order by userid"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                While (reader.Read())
                    ls = New ListItem
                    ls.Value = reader(0)
                    ls.Text = reader(1).ToString.Trim() & "-" & reader(2).ToString.Trim
                    dd_user.Items.Add(ls)
                End While
                reader.Close()
            End If
        Else
            Dim ls As ListItem
                ls = New ListItem("--")
            ls.Value = 0
            dd_user.Items.Add(ls)
                dd_user.SelectedIndex = 0

            Dim reader As SqlDataReader
                StrSql = "select userid,userno,username from tcpc0.dbo.Users where roleID<>1 and plantCode = '" & Session("PlantCode") & "' and isnull(isactive,0)=1 and isnull(deleted,0) =0 and (leaveDate is null or datediff(month,leaveDate,getdate()) <=1 ) and userid='" & Request("uid") & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim() & "-" & reader(2).ToString.Trim
                dd_user.Items.Add(ls)
            End While
            reader.Close()
            dd_user.SelectedIndex = 1
        End If
    End Sub
    Private Sub btn_next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_next.Click

        If txb_cause.Text.Trim.Length > 0 And Request("mid") <> Nothing Then
            Dim uno As String = ""
            Dim una As String = ""

            If dd_duty.SelectedIndex <= 0 Then
                ltlAlert.Text = "alert('请选择考评对象！')"
                Exit Sub
            End If
            If dd_user.SelectedIndex <= 0 Then
                ltlAlert.Text = "alert('请选择工号姓名！')"
                Exit Sub
            End If

            If txb_note.Text.Trim.Length > 1000 Then
                ltlAlert.Text = "alert('说明不能超过500个字！')"
                Exit Sub
            End If

            If Not IsNumeric(txb_mark.Text) Then
                ltlAlert.Text = "alert('分数必须为数值型！')"
                Exit Sub
            Else
                Dim ss As String = txb_ref.Text
                If ss.Trim.Length <= 0 Then
                    ltlAlert.Text = "alert('不存在评分标准。')"
                    Exit Sub
                End If
                Dim ind As Integer
                ind = ss.IndexOf("-")
                If ind > -1 Then
                    Try
                        If CDec(txb_mark.Text) < CDec(ss.Substring(0, ind)) Then
                            ltlAlert.Text = "alert('分数不能小于最小分！')"
                            Exit Sub
                        End If
                    Catch
                        ltlAlert.Text = "alert('分数不能小于最小分！')"
                        Exit Sub
                    End Try
                    If CDec(txb_mark.Text) > CDec(ss.Substring(ind + 1)) Then
                        ltlAlert.Text = "alert('分数不能大于最大分！')"
                        Exit Sub
                    End If
                End If

                ss = dd_user.SelectedItem.Text.Trim()
                ind = ss.IndexOf("-")
                If ind > -1 Then
                    uno = ss.Substring(0, ind)
                    una = ss.Substring(ind + 1)
                End If
                'End If
            End If


            StrSql = "perf_insert_mark"
            Dim params(11) As SqlParameter
            params(0) = New SqlParameter("@userid", dd_user.SelectedValue)
            params(1) = New SqlParameter("@cause", txb_cause.Text)
            params(2) = New SqlParameter("@mark", txb_mark.Text)
            params(3) = New SqlParameter("@note", txb_note.Text)
            params(4) = New SqlParameter("@createdby", Session("uID"))
            params(5) = New SqlParameter("@createdname", Session("uName"))
            params(6) = New SqlParameter("@mid", Request("mid"))
            params(7) = New SqlParameter("@type", lbl_type.Text)
            params(8) = New SqlParameter("@duty", dd_duty.SelectedValue)
            params(9) = New SqlParameter("@dept", dd_dept.SelectedItem.Text)
            params(10) = New SqlParameter("@userno", uno)
            params(11) = New SqlParameter("@uname", una)


            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params) < 0 Then
                    ltlAlert.Text = "alert('保存失败!')"
                Exit Sub
            Else
                    ltlAlert.Text = "alert('保存成功!')"
                Exit Sub
            End If

        End If

    End Sub


    Private Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
            Response.Redirect("perf_punishlist.aspx?mid=" & Request("mid"))
    End Sub

    Private Sub dd_dept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dd_dept.SelectedIndexChanged
        loadUser()
        txb_no.Text = ""
    End Sub

    Private Sub dd_duty_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dd_duty.SelectedIndexChanged
        If dd_duty.SelectedValue = 1 Then
            StrSql = "SELECT isnull(perf_mark_min,0),isnull(perf_mark_max,0) From tcpc0.dbo.perf_definition where perf_type =N'" & lbl_type.Text.Trim() & "' and perf_cause=N'" & txb_cause.Text.Trim.Substring(lbl_type.Text.Trim.Length() + 1) & "'"
        ElseIf dd_duty.SelectedValue = 2 Then
            StrSql = "SELECT isnull(perf_mark2_min,0),isnull(perf_mark2_max,0) From tcpc0.dbo.perf_definition where perf_type =N'" & lbl_type.Text.Trim() & "' and perf_cause=N'" & txb_cause.Text.Trim.Substring(lbl_type.Text.Trim.Length() + 1) & "'"
        ElseIf dd_duty.SelectedValue = 3 Then
            StrSql = "SELECT isnull(perf_mark3_min,0),isnull(perf_mark3_max,0)  From tcpc0.dbo.perf_definition where perf_type =N'" & lbl_type.Text.Trim() & "' and perf_cause=N'" & txb_cause.Text.Trim.Substring(lbl_type.Text.Trim.Length() + 1) & "'"
        ElseIf dd_duty.SelectedValue = 4 Then
            StrSql = "SELECT isnull(perf_mark4_min,0),isnull(perf_mark4_max,0)  From tcpc0.dbo.perf_definition where perf_type =N'" & lbl_type.Text.Trim() & "' and perf_cause=N'" & txb_cause.Text.Trim.Substring(lbl_type.Text.Trim.Length() + 1) & "'"
        Else
            txb_ref.Text = ""
            txb_mark.Text = ""
            Exit Sub
        End If
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                txb_ref.Text = Format(.Rows(0).Item(0), "##0.####") & "-" & Format(.Rows(0).Item(1), "##0.####")
                txb_mark.Text = Format(.Rows(0).Item(0), "##0.####")
            End If
        End With
        ds.Reset()
    End Sub

    Private Sub txb_no_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txb_no.TextChanged
        Dim reader As SqlDataReader
            StrSql = "select userid,departmentID from tcpc0.dbo.Users where roleID<>1 and plantCode = '" & Session("PlantCode") & "' and isnull(isactive,0)=1 and isnull(deleted,0) =0 and (leaveDate is null or datediff(month,leaveDate,getdate()) <=1 ) and userno='" & txb_no.Text.Trim() & "' "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        If (reader.Read()) Then
            dd_dept.SelectedValue = reader(1)

            loadUser()

            dd_user.SelectedValue = reader(0)
        Else
            dd_dept.SelectedIndex = 0
            loadUser()
        End If
        reader.Close()
    End Sub

        Protected Sub lbn_doc_Click(sender As Object, e As EventArgs) Handles lbn_doc.Click
            Dim filePath As String
            Dim i As Integer
            filePath = lbldoc.Text
            filePath = filePath.Replace("\\", "/")
            If File.Exists(filePath) = False Then
                ltlAlert.Text = "alert('文件已移除或不存在！')"
                Return
            End If


            '  if (!File.Exists(@filePath))
            '{
            '    ltlAlert.Text = "alert('文件已移除或不存在！')";
            '    return;
            '}
            i = filePath.IndexOf("TecDocs")
            filePath = filePath.Substring(i - 1)
            filePath = filePath.Replace("\", "/")
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();"

        End Sub

        Protected Sub btn_mark_Click(sender As Object, e As EventArgs) Handles btn_mark.Click
            StrSql = "perf_insert_marksubmit"
            Dim params(11) As SqlParameter
            
            params(6) = New SqlParameter("@mid", Request("mid"))
            params(7) = New SqlParameter("@mark", txtmark.Text.Trim())
           


            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params) < 0 Then
                ltlAlert.Text = "alert('确认失败!')"
                Exit Sub
            Else
                ltlAlert.Text = "alert('确认成功!')"
                Exit Sub
            End If
        End Sub
    End Class

End Namespace
