

Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data





Partial Class part_PartSizeEdit
    Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer
    Dim strSQL As String
    Dim reader As SqlDataReader

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents import As System.Web.UI.WebControls.Button


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        If Not IsPostBack Then

            Me.Security.Register("19030403", "查看产品尺寸历史")
        End If

        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""
        If Not IsPostBack Then

            BtnModify.Enabled = Me.Security("19070401").isValid
            BtnHistory.Enabled = Me.Security("19070403").isValid
            BtnDelete.Enabled = Me.Security("19070404").isValid

            If Request("id") <> Nothing Then
                BindData()
            End If
        End If
        ltlAlert.Text = "Form1.weight.focus();"
    End Sub

    Sub BindData()
        strSQL = " Select code, isnull(num_per_box,0), isnull(box_weight,0), isnull(box_size,0), isnull(box_per_pallet,0), " _
               & " isnull(box_per_pallet,0), isnull(box_length,0), isnull(box_width,0), isnull(box_depth,0),isnull(item_qad,'') as item" _
               & " From Items Where id=" & Request("id") ' & " And type=2 "

        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSQL)
        While reader.Read()
            gcode.Text = reader(0)
            txtItem.Text = reader("item")
            If reader(1) = "0" Then
                numInBox.Text = ""
            Else
                numInBox.Text = reader(1)
            End If
           
            If reader(2) = "0" Then
                weight.Text = ""
            Else
                weight.Text = reader(2)
            End If
            If reader(3) = "0" Then
                Size.Text = ""
            Else
                Size.Text = reader(3)
            End If
            If reader(4) = "0" Then
                numOnPallet.Text = ""
            Else
                numOnPallet.Text = reader(4)
            End If
           
            If reader(6) = "0" Then
                length.Text = ""
            Else
                length.Text = reader(6)
            End If
            If reader(7) = "0" Then
                width.Text = ""
            Else
                width.Text = reader(7)
            End If
            If reader(8) = "0" Then
                depth.Text = ""
            Else
                depth.Text = reader(8)
            End If


        End While
        reader.Close()
    End Sub

    Private Sub BtnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModify.Click
        Dim strbox, strweight, strsize, strpack, strpallet, strlength, strdepth, strwidth As String
        Dim numver As Integer
        Dim dblSize As Decimal

        If Not Me.Security("19070401").isValid Then
            ltlAlert.Text = "alert('没有权限修改产品尺寸！');"
            Exit Sub
        Else



            If weight.Text.Trim() <> "" Then
                If Not IsNumeric(weight.Text.Trim()) Then
                    ltlAlert.Text = "alert('重量必须为数字!');"
                    Exit Sub
                ElseIf Val(weight.Text.Trim()) < 0 Then
                    ltlAlert.Text = "alert('重量必须为正数!');"
                    Exit Sub
                End If
            Else
                weight.Text = ""
            End If
            If size.Text.Trim() <> "" Then
                If Not IsNumeric(size.Text.Trim()) Then
                    ltlAlert.Text = "alert('体积必须为数字!');"
                    Exit Sub
                ElseIf Val(size.Text.Trim()) < 0 Then
                    ltlAlert.Text = "alert('体积必须为正数!');"
                    Exit Sub
                End If
            Else
                size.Text = ""
            End If

            If length.Text.Trim() <> "" Then
                If Not IsNumeric(length.Text.Trim()) Then
                    ltlAlert.Text = "alert('长度必须为数字!');"
                    Exit Sub
                ElseIf Val(length.Text.Trim()) < 0 Then
                    ltlAlert.Text = "alert('长度必须为正数!');"
                    Exit Sub
                End If
            Else
                length.Text = ""
            End If
            If width.Text.Trim() <> "" Then
                If Not IsNumeric(width.Text.Trim()) Then
                    ltlAlert.Text = "alert('宽度必须为数字!');"
                    Exit Sub
                ElseIf Val(width.Text.Trim()) < 0 Then
                    ltlAlert.Text = "alert('宽度必须为正数!');"
                    Exit Sub
                End If
            Else
                width.Text = ""
            End If

            If depth.Text.Trim() <> "" Then
                If Not IsNumeric(depth.Text.Trim()) Then
                    ltlAlert.Text = "alert('深度必须为数字!');"
                    Exit Sub
                ElseIf Val(depth.Text.Trim()) < 0 Then
                    ltlAlert.Text = "alert('深度必须为正数!');"
                    Exit Sub
                End If
            Else
                depth.Text = ""
            End If
            If numInBox.Text.Trim() <> "" Then
                If Not IsNumeric(numInBox.Text.Trim()) Then
                    ltlAlert.Text = "alert('只数/套必须为数字!');"
                    Exit Sub
                ElseIf Val(numInBox.Text.Trim()) < 0 Then
                    ltlAlert.Text = "alert('只数/套必须为正数!');"
                    Exit Sub
                End If
            Else
                numInBox.Text = ""
            End If
            If numInBox.Text.Trim() = "0" Then
                ltlAlert.Text = "alert('只数/套不能为0!');"
                Exit Sub
            End If
            If numOnPallet.Text.Trim() <> "" Then
                If Not IsNumeric(numOnPallet.Text.Trim()) Then
                    ltlAlert.Text = "alert('套数/箱必须为数字!');"
                    Exit Sub
                ElseIf Val(numOnPallet.Text.Trim()) < 0 Then
                    ltlAlert.Text = "alert('套数/箱必须为正数!');"
                    Exit Sub
                End If
            Else
                numOnPallet.Text = ""
            End If

            strSQL = " Select isnull(num_per_box,0), isnull(box_weight,0), isnull(box_size,0), isnull(num_per_pack,0), " _
                   & " isnull(box_per_pallet,0), isnull(box_length,0), isnull(box_width,0), isnull(box_depth,0) " _
                   & " From Items Where id=" & Request("id") ' & " And type=2 "
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSQL)
            While reader.Read()
                If reader(0) = "0" Then
                    strbox = ""
                Else
                    strbox = reader(0)
                End If
                If reader(1) = "0" Then
                    strweight = ""
                Else
                    strweight = reader(1)
                End If
                If reader(2) = "0" Then
                    strsize = ""
                Else
                    strsize = reader(2)
                End If
                If reader(3) = "0" Then
                    strpack = ""
                Else
                    strpack = reader(3)
                End If
                If reader(4) = "0" Then
                    strpallet = ""
                Else
                    strpallet = reader(4)
                End If
                If reader(5) = "0" Then
                    strlength = ""
                Else
                    strlength = reader(5)
                End If
                If reader(6) = "0" Then
                    strwidth = ""
                Else
                    strwidth = reader(6)
                End If
                If reader(7) = "0" Then
                    strdepth = ""
                Else
                    strdepth = reader(7)
                End If
            End While
            reader.Close()

            If ((weight.Text.Trim() <> strweight.Trim()) _
                Or (size.Text.Trim() <> strsize.Trim()) _
                Or (strlength.Trim() <> length.Text.Trim()) _
                Or (strdepth.Trim() <> depth.Text.Trim()) _
                Or (strwidth.Trim() <> width.Text.Trim()) _
                Or (strbox.Trim() <> numInBox.Text.Trim()) _
                Or (strpack.Trim() <> numOnPallet.Text.Trim())) Then

                If length.Text.Trim() = "" Or width.Text.Trim() = "" Or depth.Text.Trim() = "" Then
                    'If size.Text.Trim().Length > 0 Then
                    '    dblSize = CDbl(size.Text.Trim())
                    'Else
                    dblSize = 0
                    size.Text = ""
                    'End If
                Else
                    dblSize = CDbl(length.Text.Trim()) * CDbl(width.Text.Trim()) * CDbl(depth.Text.Trim()) / 1000000.0
                    size.Text = CStr(dblSize)
                End If

                strSQL = " Select top 1 isnull(version,0) From product_physical_his Where prod_id=" & Request("id") & " Order By CreatedDate Desc "
                numver = CInt(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL)) + 1

                strSQL = " Update Items Set "



                If weight.Text.Trim().Length > 0 Then
                    strSQL &= " box_weight='" & weight.Text.Trim() & "',"
                Else
                    strSQL &= " box_weight=0,"
                End If

                If dblSize > 0 Then
                    strSQL &= " box_size='" & dblSize & "',"
                Else
                    strSQL &= " box_size=0,"
                End If





                If length.Text.Trim().Length > 0 Then
                    strSQL &= " box_length='" & length.Text.Trim() & "',"
                Else
                    strSQL &= " box_length=0,"
                End If

                If width.Text.Trim().Length > 0 Then
                    strSQL &= " box_width='" & width.Text.Trim() & "',"
                Else
                    strSQL &= " box_width=0,"
                End If

                If depth.Text.Trim().Length > 0 Then
                    strSQL &= " box_depth='" & depth.Text.Trim() & "',"
                Else
                    strSQL &= " box_depth=0,"
                End If

                If numInBox.Text.Trim().Length > 0 Then
                    strSQL &= " num_per_box='" & numInBox.Text.Trim() & "',"
                Else
                    strSQL &= " num_per_box=0,"
                End If

                If numOnPallet.Text.Trim().Length > 0 Then
                    strSQL &= " box_per_pallet='" & numOnPallet.Text.Trim() & "',"
                Else
                    strSQL &= " num_per_pack=0,"
                End If

                strSQL &= " modifiedBy='" & Session("uID") & "'," _
                       & " modifiedDate='" & DateTime.Now() & "'" _
                       & " Where id='" & Request("id") & "'"

                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                strSQL = " Insert Into product_physical_his(prod_id, "
                If weight.Text.Trim().Length > 0 Then
                    strSQL &= " box_weight,"
                End If
                If dblSize > 0 Then
                    strSQL &= " box_size,"
                End If

                If length.Text.Trim().Length > 0 Then
                    strSQL &= " box_length,"
                End If
                If width.Text.Trim().Length > 0 Then
                    strSQL &= " box_width,"
                End If
                If depth.Text.Trim().Length > 0 Then
                    strSQL &= " box_depth,"
                End If
                strSQL &= " createdBy, createdDate, plantCode, version) " _
                       & " Values('" & Request("id")

                If weight.Text.Trim().Length > 0 Then
                    strSQL &= "','" & weight.Text.Trim()
                End If
                If dblSize > 0 Then
                    strSQL &= "','" & dblSize
                End If

                If length.Text.Trim().Length > 0 Then
                    strSQL &= "','" & length.Text.Trim()
                End If
                If width.Text.Trim().Length > 0 Then
                    strSQL &= "','" & width.Text.Trim()
                End If
                If depth.Text.Trim().Length > 0 Then
                    strSQL &= "','" & depth.Text.Trim()
                End If
                strSQL &= "','" & Session("uId") & "',getdate(),'" & Session("plantcode") & "','" & numver & "') "
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)

                ltlAlert.Text = "alert('修改成功!');"
            End If
        End If
    End Sub

    Private Sub BtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
       
                Me.Redirect("/part/partlist.aspx")
          

    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click

        If Me.Security("19070404").isValid Then
            ltlAlert.Text = "alert('没有权限删除产品尺寸！');"
            Exit Sub
        Else
            strSQL = " Update Items Set num_per_box=null, box_weight=null, box_size=null, num_per_pack=null, box_per_pallet=null, box_length=null, box_width=null, box_depth=null Where id='" & Request("id") & "'  And plantCode='" & Session("plantCode") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
            ltlAlert.Text = "alert('删除成功!');"
            BindData()
        End If
    End Sub

    Private Sub BtnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHistory.Click

        If Not Me.Security("19070403").isValid Then
            ltlAlert.Text = "alert('没有权限查看产品尺寸历史！');"
            Exit Sub
        Else
            If Request("st") = "true" Then
                If Request("code") <> Nothing Then
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") _
                                            & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") _
                                            & "&cat=" & Request("cat") & "&st=true"), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") _
                                            & "&pg=" & Request("pg") & "&st=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") & "&st=true"), True)
                        End If
                    End If
                Else
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&cat=" & Request("cat") _
                                            & "&pg=" & Request("pg") & "&st=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&st=true"), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&pg=" & Request("pg") & "&st=true"), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&st=true"), True)
                        End If
                    End If
                End If
            ElseIf Request("st") = "false" Then
                If Request("code") <> Nothing Then
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") _
                                            & "&cat=" & Request("cat") & "&pg=" & Request("pg") & "&st=false"), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") _
                                            & "&cat=" & Request("cat") & "&st=false"), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") _
                                            & "&pg=" & Request("pg") & "&st=false"), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") & "&st=false"), True)
                        End If
                    End If
                Else
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&cat=" & Request("cat") _
                                            & "&pg=" & Request("pg") & "&st=false"), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&cat=" & Request("cat") & "&st=false"), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&pg=" & Request("pg") & "&st=false"), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&st=false"), True)
                        End If
                    End If
                End If
            Else
                If Request("code") <> Nothing Then
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") _
                                            & "&cat=" & Request("cat") & "&pg=" & Request("pg")), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") _
                                            & "&cat=" & Request("cat")), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code") _
                                            & "&pg=" & Request("cat")), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&code=" & Request("code")), True)
                        End If
                    End If
                Else
                    If Request("cat") <> Nothing Then
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&cat=" & Request("cat") _
                                            & "&pg=" & Request("pg")), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&cat=" & Request("cat")), True)
                        End If
                    Else
                        If Request("pg") <> Nothing Then
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id") & "&pg=" & Request("pg")), True)
                        Else
                            Response.Redirect(chk.urlRand("partSizeHistory.aspx?id=" & Request("id")), True)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
End Class



