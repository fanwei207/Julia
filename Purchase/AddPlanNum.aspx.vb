Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class AddPlanNum
        Inherits BasePage
    Dim chk As New adamClass
    Dim strsql As String
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
        ltlID.Text = Request("id")
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim reader As SqlDataReader
        Dim reader1 As SqlDataReader
        Dim Nplan_qty As Integer
        Dim Nprod_qty As Integer
        Dim last_partin_date As String = ""
        Dim first_partin_date As String = ""
        Dim rate As Integer
        Nplan_qty = 0
        Nprod_qty = 0
        rate = 0

        strsql = "SELECT isnull(SUM(dpd.plan_qty),0) FROM Dog_PartIn_Detail dpd inner join Dog_PartIn dp on dp.id=dpd.dog_partin_id WHERE dpd.dog_partin_id='" & ltlID.Text.Trim() & "'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)
        While reader.Read()
            Nplan_qty = reader(0)
        End While
        reader.Close()
        strsql = "select prod_qty,isnull(rate,1),isnull(first_partin_date,''),isnull(last_partin_date,'') from dog_partin where id='" & ltlID.Text.Trim() & "'"
        reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strsql)
        While reader1.Read()
            Nprod_qty = reader1(0)
            rate = reader1(1)
            last_partin_date = Convert.ToString(Format(reader1(3), "yyyy-MM-dd"))
            first_partin_date = Convert.ToString(Format(reader1(2), "yyyy-MM-dd"))
        End While
        reader1.Close()
        If plan_date.Text.Trim.Length > 0 Then
            If Not IsDate(plan_date.Text.Trim()) Then
                ltlAlert.Text = "alert('请按照正确日期格式输入！');"
                Exit Sub
            ElseIf CDate(first_partin_date) <> CDate("1900-1-1") And CDate(last_partin_date) <> CDate("1900-1-1") Then
                If CDate(plan_date.Text.Trim()) < CDate(first_partin_date) Or CDate(plan_date.Text.Trim()) > CDate(last_partin_date) Then
                    ltlAlert.Text = "alert('输入的日期应在首次到货日期和最后到货日期之间！');"
                    Exit Sub
                Else
                    strsql = "select isnull(id,0) from Dog_PartIn_Detail where dog_partin_id='" & ltlID.Text.Trim() & "' and plan_date='" & plan_date.Text.Trim() & "'"
                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strsql) > 0 Then
                        ltlAlert.Text = "alert('已经存在该计划日期！');"
                        Exit Sub
                    End If
                End If
            End If
        Else
            ltlAlert.Text = "alert('请输入计划日期！');"
            Exit Sub
        End If
        If plan_qty.Text.Trim.Length > 0 Then
            If Not IsNumeric(plan_qty.Text.Trim()) Then
                ltlAlert.Text = "alert('请输入正确地的计划数！')"
                Exit Sub
            ElseIf (Nplan_qty + CInt(plan_qty.Text.Trim())) > (Nprod_qty * rate) Then
                ltlAlert.Text = "alert('请输入的总计划数大于定购数！')"
                Exit Sub
            End If
        Else
            ltlAlert.Text = "alert('请输入计划数！')"
            Exit Sub
        End If
        Dim param(4) As SqlParameter
        param(0) = New SqlParameter("@plan_qty", Convert.ToInt32(plan_qty.Text.Trim()))
        param(1) = New SqlParameter("@plan_date", Convert.ToDateTime(plan_date.Text.Trim()))
        param(2) = New SqlParameter("@notes", notes.Text.Trim())
        param(3) = New SqlParameter("@user", Session("uID"))
        param(4) = New SqlParameter("@id", ltlID.Text.Trim())
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, "Purchase_insert_qty", param)
        ' ltlAlert.Text = "alert('添加成功!');window.location.href='/Purchase/AddPurNum.aspx?id=" & Request("id") & "&rm=" & DateTime.Now() & Rnd() & "';"
        ltlAlert.Text = "post();"
    End Sub

    Private Sub cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancel.Click
        plan_date.Text = ""
        plan_qty.Text = ""
        notes.Text = ""
    End Sub

    Private Sub close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles close.Click
        ltlAlert.Text = "window.close()"
    End Sub
End Class

End Namespace
