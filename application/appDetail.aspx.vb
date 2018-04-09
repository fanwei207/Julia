Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class appDetail
        Inherits System.Web.UI.Page
    Dim strSql As String
    Dim ds As DataSet
    Dim reader As SqlDataReader
    Dim chk As New adamClass
    Dim row As TableRow
    Dim cell As TableCell
    'Protected WithEvents ltlAlert As Literal

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
            If Request("id") <> Nothing Then
                DataLoad(Request("id"), Request("act"), Request("c"))
            End If
        End If
    End Sub
    Sub DataLoad(ByVal id As Integer, ByVal act As Integer, ByVal checked As Integer)
        strSql = "SELECT ISNULL(reason,'') as AppReason,ISNULL(description,'') as AppDesc, " _
                & " ISNULL(confirmBy,0) as confirmBy,isnull(approveBy,0) as approveBy, " _
                & " ISNULL(approveDesc,'') as approveDesc,ISNULL(result,'') as result, " _
                & " ISNULL(finishedBy,0) as finishedBy,finishedDate,approveDate,confirmDate,createdDate, " _
                & " ISNULL(u1.userName,'') as confirmName,ISNULL(u2.userName,'') as approveName, " _
                & " ISNULL(u3.userName,'') as finishedName,ISNULL(u4.userName,'') as createdName, " _
                & " al.Status,al.type " _
                & " FROM Applications al " _
                & " LEFT JOIN tcpc0.dbo.users u1 ON al.confirmBy=u1.userID " _
                & " LEFT JOIN tcpc0.dbo.users u2 ON al.approveBy=u2.userID " _
                & " LEFT JOIN tcpc0.dbo.users u3 ON al.finishedBy=u3.userID " _
                & " LEFT JOIN tcpc0.dbo.users u4 ON al.createdBy=u4.userID " _
                & " WHERE al.id='" & id & "'"
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
        While reader.Read

            If act = 2 Then
                selectTypeLabel.Text = "所属类别:"
            Else
                selectTypeLabel.Text = "内容:"
            End If

            Select Case reader("type")
                Case 0
                    selectTypeLabel.Text &= "[程序出错报警]"
                Case 1
                    selectTypeLabel.Text &= "[更新电脑数据]"
                Case 2
                    selectTypeLabel.Text &= "[程序修改]"
                Case 3
                    selectTypeLabel.Text &= "[新增程序]"
            End Select

            row = New TableRow
            cell = New TableCell
            cell.BackColor = System.Drawing.Color.LightGray
            cell.Text = "申请原因"
            cell.ColumnSpan = 3
            row.Cells.Add(cell)
            RecordDataTable.Rows.Add(row)

            row = New TableRow
            cell = New TableCell
            cell.ColumnSpan = 3
            cell.Text = "&nbsp;" & reader("AppReason")
            row.Cells.Add(cell)
            RecordDataTable.Rows.Add(row)

            row = New TableRow
            cell = New TableCell
            cell.Width = New Unit(196)
            cell.BackColor = System.Drawing.Color.LightGray
            cell.Text = "申请内容"
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Width = New Unit(196)
            cell.BackColor = System.Drawing.Color.LightGray
            cell.HorizontalAlign = HorizontalAlign.Center
            cell.Text = "申请人：" & reader("createdName")
            row.Cells.Add(cell)

            cell = New TableCell
            cell.Width = New Unit(196)
            cell.BackColor = System.Drawing.Color.LightGray
            cell.HorizontalAlign = HorizontalAlign.Right
            cell.Text = "申请日期：" & Format(reader("createdDate"), "yyyy-MM-dd")
            row.Cells.Add(cell)

            RecordDataTable.Rows.Add(row)

            row = New TableRow
            cell = New TableCell
            cell.ColumnSpan = 3
            cell.Text = "&nbsp;" & reader("AppDesc")
            row.Cells.Add(cell)
            RecordDataTable.Rows.Add(row)

            Select Case reader("Status")

                Case "已申请"
                    If checked = "2" Then 'if application in approve list
                        go.Visible = True
                        go.Text = "不批准"
                        agree.Visible = True
                        agree.Text = "批准"
                        desc.Visible = True
                        Exit Select
                    End If
                    Select Case act
                        Case 0
                            If reader("type") <> 0 Then
                                go.Visible = True
                                go.Text = "审核"
                                agree.Visible = False
                                desc.Visible = False
                            Else
                                go.Visible = True
                                go.Text = "处理完成"
                                agree.Visible = True
                                agree.Text = "等待处理"
                                desc.Visible = True
                            End If
                        Case 1
                            go.Visible = True
                            go.Text = "申请修改"
                            agree.Visible = False
                            desc.Visible = True
                            desc.Text = reader("AppDesc")
                        Case 2
                            go.Visible = False
                            agree.Visible = False
                            desc.Visible = False
                    End Select
                Case "已审核"
                    row = New TableRow
                    row.BackColor = System.Drawing.Color.LightGray

                    cell = New TableCell
                    cell.Text = "审核通过"
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text = "经手人:" & reader("confirmName")
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Right
                    cell.Text = "审核日期:" & Format(reader("confirmDate"), "yyyy-MM-dd")
                    row.Cells.Add(cell)
                    RecordDataTable.Rows.Add(row)

                    Select Case act
                        Case 2
                            go.Visible = False
                            agree.Visible = False
                            desc.Visible = False
                        Case Else
                            go.Visible = True
                            go.Text = "不批准"
                            agree.Visible = True
                            agree.Text = "批准"
                            desc.Visible = True
                    End Select

                Case "已批准"
                    row = New TableRow
                    row.BackColor = System.Drawing.Color.LightGray

                    cell = New TableCell
                    cell.Text = "批准通过"
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text = "经手人：" & reader("approveName")
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Right
                    cell.Text = "批复日期：" & Format(reader("approveDate"), "yyyy-MM-dd")
                    row.Cells.Add(cell)
                    RecordDataTable.Rows.Add(row)

                    row = New TableRow
                    cell = New TableCell
                    cell.ColumnSpan = 3
                    cell.Text = "&nbsp;" & reader("approveDesc")
                    row.Cells.Add(cell)
                    RecordDataTable.Rows.Add(row)

                    Select Case act
                        Case 0
                            go.Visible = True
                            go.Text = "处理完成"
                            agree.Visible = True
                            agree.Text = "等待处理"
                            desc.Visible = True
                        Case 2
                            go.Visible = False
                            agree.Visible = False
                            desc.Visible = False
                        Case Else
                            go.Visible = True
                            go.Text = "不批准"
                            agree.Visible = True
                            agree.Text = "批准"
                            desc.Visible = True
                            desc.Text = reader("approveDesc")
                    End Select

                Case "未获批准"
                    row = New TableRow
                    row.BackColor = System.Drawing.Color.LightGray

                    cell = New TableCell
                    cell.Text = "批准未通过"
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text = "经手人：" & reader("approveName")
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Right
                    cell.Text = "批复日期：" & Format(reader("approveDate"), "yyyy-MM-dd")
                    row.Cells.Add(cell)
                    RecordDataTable.Rows.Add(row)

                    row = New TableRow
                    cell = New TableCell
                    cell.ColumnSpan = 3
                    cell.Text = "&nbsp;" & reader("approveDesc")
                    row.Cells.Add(cell)
                    RecordDataTable.Rows.Add(row)

                    Select Case act
                        Case 2
                            go.Visible = False
                            agree.Visible = False
                            desc.Visible = False
                        Case Else
                            go.Visible = True
                            go.Text = "完成"
                            agree.Visible = True
                            agree.Text = "批准"
                            desc.Visible = True
                            desc.Text = reader("approveDesc")
                    End Select

                Case "等待处理"
                    row = New TableRow
                    row.BackColor = System.Drawing.Color.LightGray

                    cell = New TableCell
                    cell.Text = "等待处理"
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text = "经手人：" & reader("finishedName")
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.Text = "处理日期:" & Format(reader("finishedDate"), "yyyy-MM-dd")
                    cell.HorizontalAlign = HorizontalAlign.Right
                    row.Cells.Add(cell)
                    RecordDataTable.Rows.Add(row)

                    row = New TableRow
                    cell = New TableCell
                    cell.ColumnSpan = 3
                    cell.Text = "&nbsp;" & reader("result")
                    row.Cells.Add(cell)
                    RecordDataTable.Rows.Add(row)

                    Select Case act
                        Case 2
                            go.Visible = False
                            agree.Visible = False
                            desc.Visible = False
                        Case Else
                            go.Visible = True
                            go.Text = "处理完成"
                            agree.Visible = True
                            agree.Text = "等待处理"
                            desc.Visible = True
                            desc.Text = reader("result")
                    End Select

                Case "已完成"

                    row = New TableRow
                    row.BackColor = System.Drawing.Color.LightGray

                    cell = New TableCell
                    cell.Text = "已经完成"
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.HorizontalAlign = HorizontalAlign.Center
                    cell.Text = "经手人：" & reader("finishedName")
                    row.Cells.Add(cell)

                    cell = New TableCell
                    cell.Text = "处理日期:" & Format(reader("finishedDate"), "yyyy-MM-dd")
                    cell.HorizontalAlign = HorizontalAlign.Right
                    row.Cells.Add(cell)
                    RecordDataTable.Rows.Add(row)

                    row = New TableRow
                    cell = New TableCell
                    cell.ColumnSpan = 3
                    cell.Text = "&nbsp;" & reader("result")
                    row.Cells.Add(cell)
                    RecordDataTable.Rows.Add(row)

                    Select Case act
                        Case 1
                            go.Visible = True
                            go.Text = "修改提交"
                            agree.Visible = False
                            desc.Visible = True
                            desc.Text = reader("result")
                        Case Else
                            go.Visible = False
                            agree.Visible = False
                            desc.Visible = False
                    End Select
            End Select
        End While
        reader.Close()
    End Sub
    Private Sub go_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles go.Click

        If (desc.Text.Length > 1000) And desc.Visible Then
            ltlAlert.Text = "alert(内容不能长于1000个字符。);"
            Exit Sub
        End If

        Select Case go.Text
            Case "申请修改"
                strSql = " UPDATE Applications SET description=N'" & chk.sqlEncode(desc.Text) & "'WHERE id='" & Request("id") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            Case "审核"
                strSql = " UPDATE Applications SET confirmBy='" & Session("uID") & "',confirmDate='" & DateTime.Now & "',status=N'已审核' " _
                    & " WHERE id='" & Request("id") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            Case "不批准"
                strSql = " UPDATE Applications SET approveBy='" & Session("uID") & "',approveDate='" & DateTime.Now _
                    & "',approveDesc=N'" & chk.sqlEncode(desc.Text) & "',status=N'未获批准' " _
                    & " WHERE id='" & Request("id") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            Case "完成"
                strSql = " UPDATE Applications SET approveBy='" & Session("uID") & "',approveDate='" & DateTime.Now _
                    & "',approveDesc=N'" & chk.sqlEncode(desc.Text) & "',status=N'已完成' " _
                    & " WHERE id='" & Request("id") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            Case "处理完成"
                strSql = " UPDATE Applications SET finishedBy='" & Session("uID") & "',finishedDate='" & DateTime.Now _
                    & "',result=N'" & chk.sqlEncode(desc.Text) & "',status=N'已完成' " _
                    & " WHERE id='" & Request("id") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        End Select
        Response.Redirect("/application/appProgram.aspx?v=" & Request("v") & "&c=" & Request("c"))
    End Sub
    Private Sub agree_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles agree.Click

        If (desc.Text.Length > 1000) And desc.Visible Then
            ltlAlert.Text = "alert(内容不能长于1000个字符。);"
            Exit Sub
        End If

        Select Case agree.Text
            Case "批准"
                strSql = " UPDATE Applications SET approveBy='" & Session("uID") & "',approveDate='" & DateTime.Now _
                   & "',approveDesc=N'" & chk.sqlEncode(desc.Text) & "',status=N'已批准' " _
                   & " WHERE id='" & Request("id") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            Case "等待处理"
                strSql = " UPDATE Applications SET finishedBy='" & Session("uID") & "',finishedDate='" & DateTime.Now _
                  & "',result=N'" & chk.sqlEncode(desc.Text) & "',status=N'等待处理' " _
                  & " WHERE id='" & Request("id") & "'"
                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        End Select
        Response.Redirect("/application/appProgram.aspx?v=" & Request("v") & "&c=" & Request("c"))
    End Sub

    Private Sub backToPrepage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backToPrepage.Click
        Response.Redirect("/application/appProgram.aspx?v=" & Request("v") & "&c=" & Request("c"))
    End Sub
End Class

End Namespace
