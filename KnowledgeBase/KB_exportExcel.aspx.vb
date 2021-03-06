Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class KB_exportExcel
    Inherits System.Web.UI.Page

    Dim sqlStr As String
    Dim reader As SqlDataReader
    Dim row As TableRow
    Dim cell As TableCell
    Public chk As New adamClass
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
        'Dim nRet As Integer
        'nRet = chk.securityCheck(Session("uID"), Session("uRole"), Session("orgID"), 19062001)
        'If nRet <= 0 Then
        '    Response.Redirect("/public/Message.aspx?type=" & nRet.ToString(), True)
        'End If

        If Request("docid") <> "" Then
            sqlStr = "select typename,isnull(DocStatus,N'新建申请') as DocStatus , docuser, doccontent,docdate from knowdb.dbo.application "
            sqlStr &= " where docid ='" & Request("docid") & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                PIMasteryRow("类型", reader(0).ToString())
                PIMasteryRow("状态", reader(1).ToString())
                PIMasteryRow("申请人", reader(2).ToString())
                PIMasteryRow("申请内容", reader(3).ToString())
                PIMasteryRow("申请日期", reader(4).ToString())
            End While
            reader.Close()
            PIMasteryRow("", "")

            sqlStr = "select SugUser,SugDate , SugContent from knowdb.dbo.Suggestion "
            sqlStr &= " where SugType = 1 and DocID ='" & Request("docid") & "' order by SugDate"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                PIMasteryRow("处理人", reader(0).ToString())
                PIMasteryRow("处理日期", reader(1).ToString())
                PIMasteryRow("处理意见", reader(2).ToString())
            End While
            reader.Close()
            PIMasteryRow("", "")

            sqlStr = "select SugUser,SugDate , SugContent from knowdb.dbo.Suggestion "
            sqlStr &= " where SugType = 2 and DocID ='" & Request("docid") & "' order by SugDate"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                PIMasteryRow("批准人", reader(0).ToString())
                PIMasteryRow("批准日期", reader(1).ToString())
                PIMasteryRow("批准意见", reader(2).ToString())
            End While
            reader.Close()
            PIMasteryRow("", "")

            sqlStr = "select SugUser,SugDate , SugContent from knowdb.dbo.Suggestion "
            sqlStr &= " where SugType = 3 and DocID ='" & Request("docid") & "' order by SugDate"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                PIMasteryRow("测试人", reader(0).ToString())
                PIMasteryRow("测试日期", reader(1).ToString())
                PIMasteryRow("测试结果", reader(2).ToString())
            End While
            reader.Close()
            PIMasteryRow("", "")

            sqlStr = "select SugUser,SugDate , SugContent from knowdb.dbo.Suggestion "
            sqlStr &= " where SugType = 4 and DocID ='" & Request("docid") & "' order by SugDate"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                PIMasteryRow("备注人", reader(0).ToString())
                PIMasteryRow("备注日期", reader(1).ToString())
                PIMasteryRow("备注", reader(2).ToString())
            End While
            reader.Close()
            PIMasteryRow("", "")

            sqlStr = "select AttUser,AttDate , Attname,AttType from knowdb.dbo.Attach "
            sqlStr &= " where DocID ='" & Request("docid") & "' order by AttDate"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, sqlStr)
            While (reader.Read())
                PIMasteryRow("附件人", reader(0).ToString())
                PIMasteryRow("上传日期", reader(1).ToString())
                PIMasteryRow("文件名", reader(2).ToString())
                PIMasteryRow("文件类型", reader(3).ToString())
            End While
            reader.Close()
            PIMasteryRow("", "")
        End If

        Dim i As Integer = 0
        While i < 10
            PIMasteryRow("", "")
            i = i + 1
        End While

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition", "attachment; filename=report.xls")
        Response.Flush()

    End Sub

    Sub PIMasteryRow(ByVal str1 As String, ByVal str2 As String)
        row = New TableRow
        row.BackColor = Color.White
        row.HorizontalAlign = HorizontalAlign.Left
        row.BorderWidth = New Unit(0)


        cell = New TableCell
        cell.Width = New Unit(80)
        cell.Text = str1

        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)


        cell = New TableCell
        cell.Text = str2
        cell.Width = New Unit(500)
        If IsNumeric(cell.Text) Then
            If cell.Text.Trim.Length > 8 Then
                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
            End If
            cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf IsDate(cell.Text) Then
            If CDate(cell.Text) = CDate("1900-01-01") Then
                cell.Text = ""
            Else
                cell.Text = Format(Convert.ToDateTime(cell.Text), "yyyy-MM-dd")
            End If
        End If
        row.Cells.Add(cell)

        Dim i As Integer = 0
        While i < 20
            cell = New TableCell
            cell.Text = ""
            cell.Width = New Unit(60)
            row.Cells.Add(cell)
            i = i + 1
        End While
        exl.Rows.Add(row)
    End Sub


End Class

End Namespace
