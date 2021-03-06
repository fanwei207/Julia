Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb
Imports System.Data

Namespace tcpc

    Partial Class CodeConvert
        Inherits BasePage
        Dim chk As New adamClass
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
            ltlAlert.Text = ""
            If Not (IsPostBack) Then
                filetypeDDL.SelectedIndex = 0
                Dim item As ListItem
                item = New ListItem("Excel (.xls) file.")
                item.Value = 0
                filetypeDDL.Items.Add(item)
            End If
        End Sub

        Public Sub uploadBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadBtn.ServerClick
            If (Session("uID") = Nothing) Then
                Exit Sub
            End If
            ImportExcelFile()
        End Sub

        Private Sub ImportExcelFile()
            Dim strSQL As String
            Dim cs As Boolean = True
            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer

            strCatFolder = Server.MapPath("/import")
            If Not Directory.Exists(strCatFolder) Then
                Try
                    Directory.CreateDirectory(strCatFolder)
                Catch
                    ltlAlert.Text = "alert('上传文件失败(1001)！.')"
                    Return
                End Try
            End If

            strUserFileName = filename.PostedFile.FileName
            intLastBackslash = strUserFileName.LastIndexOf("\")
            strFileName = strUserFileName.Substring(intLastBackslash + 1)
            If (strFileName.Trim().Length <= 0) Then
                ltlAlert.Text = "alert('请选择导入文件.')"
                Return
            End If

            strUserFileName = strFileName 'file name without path

            Dim i As Integer = 0
            While (i < 1000)
                strFileName = strCatFolder & "\f" & i.ToString() & strUserFileName
                If Not (File.Exists(strFileName)) Then
                    Exit While
                End If
                i = i + 1
            End While

            If Not (filename.PostedFile Is Nothing) Then
                If (filename.PostedFile.ContentLength > 8388608) Then
                    ltlAlert.Text = "alert('上传的文件最大为 8 MB.')"
                    Return
                End If
                Try
                    filename.PostedFile.SaveAs(strFileName)
                Catch
                    ltlAlert.Text = "alert('上传文件失败(1002)！.')"
                    Return
                End Try

                If (File.Exists(strFileName)) Then
                    'Dim _old_code, _new_code, _status As String
                    Dim _old_code, _new_code As String
                    Dim _qty As Decimal
                    'Dim _qty2 As Decimal
                    'Dim id As Integer
                    'Dim issize As String

                    Dim myDatatable As DataTable = Me.GetExcelContents(strFileName)
                    With myDatatable
                        If .Rows.Count > 0 Then

                            Dim Title1 = Replace(.Columns(0).ColumnName, " ", "")
                            If Title1 <> "部件号" Then
                                ltlAlert.Text = "alert('出错：\r\n导入的excel标题第一列必须为：部件号; 请检查！')"
                                Return

                            End If

                            Dim Title2 = Replace(.Columns(1).ColumnName, " ", "")

                            If Title2 <> "QAD号" Then
                                ltlAlert.Text = "alert('出错：\r\n导入的excel标题第二列必须为：QAD号; 检查！')"
                                Return

                            End If
                            Dim Title3 = Replace(.Columns(2).ColumnName, " ", "")
                            If Title3 <> "套数" Then
                                ltlAlert.Text = "alert('出错：\r\n导入的excel标题第三列必须为：套数; 请检查！')"
                                Return

                            End If

                            'If .Columns(0).ColumnName <> "部件号" Or .Columns(1).ColumnName <> "QAD号" Or .Columns(2).ColumnName <> "套数" Then
                            If Title1 <> "部件号" Or Title2 <> "QAD号" Or Title3 <> "套数" Then
                                ltlAlert.Text = "alert('出错：\r\n导入的excel标题前三列必须为：部件号;  QAD号;  套数，请检查！')"
                                Return

                            End If

                            strSQL = " Delete From CodeConvert Where userID='" & Session("uID") & "' "
                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                            '转换成CodeConvert格式
                            Dim table As New DataTable("temp")
                            Dim column As DataColumn
                            Dim row As DataRow

                            column = New DataColumn()
                            column.DataType = System.Type.GetType("System.String")
                            column.ColumnName = "code"
                            table.Columns.Add(column)

                            column = New DataColumn()
                            column.DataType = System.Type.GetType("System.String")
                            column.ColumnName = "qad"
                            table.Columns.Add(column)

                            column = New DataColumn()
                            column.DataType = System.Type.GetType("System.Decimal")
                            column.ColumnName = "qty"
                            table.Columns.Add(column)

                            column = New DataColumn()
                            column.DataType = System.Type.GetType("System.Int32")
                            column.ColumnName = "userid"
                            table.Columns.Add(column)

                            column = New DataColumn()
                            column.DataType = System.Type.GetType("System.String")
                            column.ColumnName = "domain"

                            table.Columns.Add(column)

                            For i = 0 To .Rows.Count - 1
                                _old_code = Nothing
                                _new_code = Nothing
                                _qty = 0
                                '_status = Nothing
                                'id = 0
                                'issize = Nothing
                                '_qty2 = 0
                                'Response.Write(.Rows(i).Item(0) & "/" & .Rows(i).Item(1) & "/" & .Rows(i).Item(2) & "<br>")

                                If .Rows(i).IsNull(0) Then
                                    _old_code = ""
                                Else
                                    _old_code = .Rows(i).Item(0)
                                    If (_old_code.Length > 50) Then
                                        ltlAlert.Text = "alert('出错，导入的excel第" & i + 1 & "行的部件号长度大于50')"
                                    End If
                                End If

                                If .Rows(i).IsNull(1) Then
                                    _new_code = ""
                                Else
                                    _new_code = .Rows(i).Item(1)
                                    If (_new_code.Length > 14) Then
                                        ltlAlert.Text = "alert('出错，导入的excel第" & i + 1 & "行的QAD号长度大于14')"
                                    End If
                                End If

                                If .Rows(i).IsNull(2) Then
                                    _qty = 0
                                Else
                                    Dim qt As String
                                    qt = .Rows(i).Item(2)
                                    Try
                                        _qty = Convert.ToDecimal(qt)
                                    Catch ex As Exception
                                        ltlAlert.Text = "alert('出错，导入的excel第" & i + 1 & "行的套数不是数字')"
                                        _qty = 0
                                    End Try
                                End If

                                'id = 0

                                row = table.NewRow()
                                row("code") = IIf(_old_code.Trim().Length() = 0, "NoCode", _old_code.Trim())
                                row("qad") = IIf(_new_code.Trim().Length() = 0, "NoQAD", _new_code.Trim())
                                row("qty") = _qty
                                row("userid") = Convert.ToInt32(Session("uID"))
                                row("domain") = RadioButtonList1.SelectedValue.ToString().Trim()

                                table.Rows.Add(row)
                            Next

                            If table IsNot Nothing And table.Rows.Count > 0 Then
                                Dim bulkCopy As New SqlBulkCopy(chk.dsn0())
                                bulkCopy.DestinationTableName = "CodeConvert"

                                bulkCopy.ColumnMappings.Clear()

                                bulkCopy.ColumnMappings.Add("code", "code")
                                bulkCopy.ColumnMappings.Add("qad", "qad")
                                bulkCopy.ColumnMappings.Add("qty", "qty")
                                bulkCopy.ColumnMappings.Add("userid", "userid")
                                bulkCopy.ColumnMappings.Add("domain", "domain")

                                Try
                                    bulkCopy.WriteToServer(table)
                                Catch
                                    ltlAlert.Text = "alert('代码转换失败，请重试！');"
                                    cs = False
                                Finally
                                    table.Dispose()
                                    bulkCopy.Close()
                                End Try
                            End If
                        End If
                    End With
                    myDatatable.Dispose()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    'ltlAlert.Text = "alert('产品简称导入成功.'); window.location.href='" & chk.urlRand("/product/ProductSimpleCodeImport.aspx") & "';"

                    If cs = True Then
                        Dim params As SqlParameter = New SqlParameter("@uid", Session("uID"))
                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, "CodeQadConvert", params)

                        strSQL = " Select c.code, c.qad,qty,qty2,Case isnull(c.isInQAD,'') When '' Then N'未在' Else N'已在' End,isnull(i.isChecked,N'未做'),i.item_qad_desc1,i.item_qad_desc2,i.description, case isnull(i.status,'')  when 0 then N'使用' when 1  Then N'试用' when 2  Then N'停用' else N''End " _
                                & " From tcpc0.dbo.CodeConvert c left outer Join tcpc0.dbo.Items i on i.code=c.code Where c.userID='" & Session("uid") & "' Order By c.id "
                        Session("EXSQL") = strSQL
                        Session("EXHeader") = "QAD号部件号转换" & DateTime.Now.ToString()
                        Session("EXTitle") = "350^<b>部件号</b>~^200^<b>QAD号</b>~^<b>套数</b>~^<b>只数</b>~^<b>是否在QAD中</b>~^<b>QAD BOM</b>~^200^<b>QAD描述1</b>~^200^<b>QAD描述2</b>~^500^<b>描述</b>~^200^<b>状态</b>~^"

                        ltlAlert.Text = "window.open('/public/exportExcel.aspx', '_blank');"
                    End If
                End If
            End If
        End Sub

        'Function find(ByVal oldcode As String) As String
        '    Try
        '        If oldcode.IndexOf("-成") > 0 Then
        '            oldcode = oldcode.Replace("-成", "")
        '        End If
        '        If oldcode.IndexOf("-待") > 0 Then
        '            oldcode = oldcode.Replace("-待", "")
        '        End If
        '        If oldcode.IndexOf("-移") > 0 Then
        '            oldcode = oldcode.Replace("-移", "")
        '        End If
        '        If oldcode.IndexOf("P-") >= 0 Then
        '            oldcode = oldcode.Remove(0, 2)
        '        End If
        '        If oldcode.IndexOf("/") > 0 Then
        '            oldcode = oldcode.Replace("/", "-")
        '        End If
        '        If oldcode.IndexOf(" ") > 0 Then
        '            oldcode = oldcode.Replace(" ", "-")
        '        End If
        '        ' -- WH、BK、Y、BL、RD、GR、OR、@@=GY、PUR .
        '        If oldcode.IndexOf("DXD") >= 0 Then
        '            If oldcode.IndexOf("WH") > 0 Then
        '                oldcode = oldcode.Replace("WH", "-WH-")
        '            End If
        '            If oldcode.IndexOf("BK") > 0 Then
        '                oldcode = oldcode.Replace("BK", "-BK-")
        '            End If
        '            If oldcode.IndexOf("BL") > 0 Then
        '                oldcode = oldcode.Replace("BL", "-BL-")
        '            End If
        '            If oldcode.IndexOf("RD") > 0 Then
        '                oldcode = oldcode.Replace("RD", "-RD-")
        '            End If
        '            If oldcode.IndexOf("GR") > 0 Then
        '                oldcode = oldcode.Replace("GR", "-GR-")
        '            End If
        '            If oldcode.IndexOf("OR") > 0 Then
        '                oldcode = oldcode.Replace("OR", "-OR-")
        '            End If
        '            If oldcode.IndexOf("OR") > 0 Then
        '                oldcode = oldcode.Replace("GY", "-GY-")
        '            End If
        '            If oldcode.IndexOf("Y") > 0 And oldcode.IndexOf("GY") <= 0 Then
        '                oldcode = oldcode.Replace("Y", "-Y-")
        '            End If
        '            If oldcode.IndexOf("PUR") > 0 Then
        '                oldcode = oldcode.Replace("PUR", "-PUR-")
        '            End If
        '        End If

        '        If oldcode.IndexOf("BT") >= 0 Then
        '            oldcode = oldcode.Replace("BT", "BT-")
        '        End If

        '        If oldcode.IndexOf("--") >= 0 Then
        '            oldcode = oldcode.Replace("--", "-")
        '        End If

        '        If oldcode.Substring(0, 3) = "LS-" Then
        '            oldcode = oldcode.Replace("LS-", "LS1-1-")
        '        Else
        '            If oldcode.Substring(0, 2) = "LS" And oldcode.Substring(2, 1) <> "-" And oldcode.Substring(3, 1) = "-" Then
        '                oldcode = oldcode.Insert(3, "-1")
        '            End If
        '        End If
        '    Catch
        '        Response.Write(oldcode & "<br>")
        '    End Try

        '    Dim sqlstr As String
        '    sqlstr = "select isnull(id,0) from items where code=N'" & oldcode & "' "
        '    If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, sqlstr) > 0 Then
        '        Return oldcode
        '    Else
        '        Return ""
        '    End If

        'End Function

    End Class

End Namespace
