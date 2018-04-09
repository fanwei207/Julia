Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
'Imports System.Data.OleDb
'Imports System.Data.Odbc
Imports System.Data
Imports WOrder

Partial Class wo_cost_wo_workproImport
    Inherits BasePage
    Public chk As New adamClass
    Dim nRet As Integer
    Dim strSQL As String
    Dim reader As SqlDataReader

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""
        If Not (IsPostBack) Then
            If Request("err") = "y" Then
                Session("EXTitle1") = "500^<b>出错信息</b>~^"
                Session("EXHeader1") = ""
                Session("EXSQL1") = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" & Session("uID") & "' order by id"
                ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
            End If

            FileTypeDropDownList1.SelectedIndex = 0
            Dim item1 As ListItem
            item1 = New ListItem("Excel (.xls) file")
            item1.Value = 0
            FileTypeDropDownList1.Items.Add(item1)
        End If
    End Sub

    Sub uploadPartBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadPartBtn.ServerClick
        If (Session("uID") = Nothing) Then
            Exit Sub
        End If
        ImportExcelFile()
    End Sub

    Sub ImportExcelFile()
        Dim _id As Integer = 0

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer

        'Dim ErrorRecord As Integer
        Dim strerr As String
        Dim IsError As Boolean
        Dim haveError As Boolean

        'strCatFolder = Server.MapPath("/import")
        strCatFolder = Server.MapPath("/import")
        If Not Directory.Exists(strCatFolder) Then
            Try
                Directory.CreateDirectory(strCatFolder)
            Catch
                ltlAlert.Text = "alert('To upload file is failure.')"
                Return
            End Try
        End If

        strUserFileName = filename1.PostedFile.FileName
        intLastBackslash = strUserFileName.LastIndexOf("\")
        strFileName = strUserFileName.Substring(intLastBackslash + 1)
        If (strFileName.Trim().Length <= 0) Then
            ltlAlert.Text = "alert('Please choose the file.')"
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

        If Not (filename1.PostedFile Is Nothing) Then
            If (filename1.PostedFile.ContentLength > 8388608) Then
                ltlAlert.Text = "alert('The max size is 8 MB.')"
                Return
            End If
            Try
                filename1.PostedFile.SaveAs(strFileName)
            Catch
                ltlAlert.Text = "alert('To upload file is failure.')"
                Return
            End Try

            If (File.Exists(strFileName)) Then
                Dim _Tec As String
                Dim _Proc As String
                Dim _total As String


                'Dim myDataset As DataSet
                Dim myDatadt As DataTable
                Try
                    'myDataset = chk.getExcelContents(strFileName)
                    myDatadt = GetExcelContents(strFileName)

                Catch
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    ltlAlert.Text = "alert('File must be Excel.');"
                    Exit Sub
                Finally
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                End Try
                'Try 

                With myDatadt
                    If .Rows.Count > 0 Then
                        If .Columns(0).ColumnName <> "工艺流程" Or .Columns(1).ColumnName <> "工序名称" Or .Columns(2).ColumnName <> "指标" Then
                            'myDataset.Reset()
                            ltlAlert.Text = "alert('模板不正确.'); "
                            Exit Sub
                        End If

                        DeleteHis()

                        Dim column As DataColumn
                        Dim rowError As DataRow
                        Dim rowTemp As DataRow

                        '构建ImportError
                        Dim tblError As New DataTable("import_error")

                        column = New DataColumn()
                        column.DataType = System.Type.GetType("System.String")
                        column.ColumnName = "errInfo"
                        tblError.Columns.Add(column)

                        column = New DataColumn()
                        column.DataType = System.Type.GetType("System.Int32")
                        column.ColumnName = "uID"
                        tblError.Columns.Add(column)

                        column = New DataColumn()
                        column.DataType = System.Type.GetType("System.Int32")
                        column.ColumnName = "plantCode"
                        tblError.Columns.Add(column)

                        '构建wo_tec_temp
                        Dim tblTemp As New DataTable("tec_temp")

                        column = New DataColumn()
                        column.DataType = System.Type.GetType("System.Int32")
                        column.ColumnName = "ln"
                        tblTemp.Columns.Add(column)

                        column = New DataColumn()
                        column.DataType = System.Type.GetType("System.String")
                        column.ColumnName = "tec"
                        tblTemp.Columns.Add(column)

                        column = New DataColumn()
                        column.DataType = System.Type.GetType("System.String")
                        column.ColumnName = "proc"
                        tblTemp.Columns.Add(column)

                        column = New DataColumn()
                        column.DataType = System.Type.GetType("System.Decimal")
                        column.ColumnName = "total"
                        tblTemp.Columns.Add(column)

                        column = New DataColumn()
                        column.DataType = System.Type.GetType("System.Int32")
                        column.ColumnName = "createdby"
                        tblTemp.Columns.Add(column)

                        haveError = False

                        For i = 0 To .Rows.Count - 1  '// 循环Excel文件
                            Dim strProc As String = ""
                            Dim strRun As String = ""
                            Dim strRdesc As String = ""
                            Dim strPdesc As String = ""

                            IsError = False
                            strerr = ""

                            If .Rows(i).IsNull(0) Then
                                _Tec = ""
                            Else
                                _Tec = .Rows(i).Item(0)
                            End If

                            If .Rows(i).IsNull(1) Then
                                _Proc = ""
                            Else
                                _Proc = .Rows(i).Item(1)
                            End If

                            If .Rows(i).IsNull(2) Then
                                _total = ""
                            Else
                                _total = .Rows(i).Item(2)
                            End If

                            If _Tec.Trim.Length = 0 Then
                                strerr = "行" & i + 2.ToString & ",工艺流程不能为空！"
                                'ErrorMsg(strerr)
                                IsError = True
                                haveError = True

                                rowError = tblError.NewRow()
                                rowError("errInfo") = strerr
                                rowError("uid") = Convert.ToInt32(Session("uID"))
                                rowError("plantCode") = Convert.ToInt32(Session("PlantCode"))

                                tblError.Rows.Add(rowError)
                            Else
                                If _Proc.Trim.Length = 0 Then
                                    strerr = "行" & i + 2.ToString & ",工序名称不能为空！"
                                    'ErrorMsg(strerr)
                                    IsError = True
                                    haveError = True

                                    rowError = tblError.NewRow()
                                    rowError("errInfo") = strerr
                                    rowError("uid") = Convert.ToInt32(Session("uID"))
                                    rowError("plantCode") = Convert.ToInt32(Session("PlantCode"))

                                    tblError.Rows.Add(rowError)
                                Else
                                    If _total.Trim.Length = 0 Then
                                        strerr = "行" & i + 2.ToString & ",指标不能为空！"
                                        'ErrorMsg(strerr)
                                        IsError = True
                                        haveError = True

                                        rowError = tblError.NewRow()
                                        rowError("errInfo") = strerr
                                        rowError("uid") = Convert.ToInt32(Session("uID"))
                                        rowError("plantCode") = Convert.ToInt32(Session("PlantCode"))

                                        tblError.Rows.Add(rowError)
                                    Else
                                        If Not IsNumeric(_total) Then
                                            strerr = "行" & i + 2.ToString & ",指标必须为数字！"
                                            'ErrorMsg(strerr)
                                            IsError = True
                                            haveError = True

                                            rowError = tblError.NewRow()
                                            rowError("errInfo") = strerr
                                            rowError("uid") = Convert.ToInt32(Session("uID"))
                                            rowError("plantCode") = Convert.ToInt32(Session("PlantCode"))

                                            tblError.Rows.Add(rowError)
                                        End If
                                    End If
                                End If
                            End If

                            If IsError = False Then
                                rowTemp = tblTemp.NewRow()
                                rowTemp("ln") = i + 2
                                rowTemp("tec") = _Tec.Trim()
                                rowTemp("proc") = _Proc.Trim()
                                rowTemp("total") = Decimal.Parse(_total.Trim(), System.Globalization.NumberStyles.Any)
                                rowTemp("createdby") = Convert.ToInt32(Session("uID"))

                                tblTemp.Rows.Add(rowTemp)
                            End If
                        Next

                        IsError = False

                        If haveError = True Then
                            If tblError IsNot Nothing And tblError.Rows.Count > 0 Then
                                Dim bulkCopy As New SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction)
                                bulkCopy.DestinationTableName = "ImportError"

                                bulkCopy.ColumnMappings.Clear()

                                bulkCopy.ColumnMappings.Add("errInfo", "ErrorInfo")
                                bulkCopy.ColumnMappings.Add("uID", "userID")
                                bulkCopy.ColumnMappings.Add("plantCode", "plantID")

                                Try
                                    bulkCopy.WriteToServer(tblError)
                                Catch
                                    IsError = True
                                Finally
                                    tblError.Dispose()
                                    bulkCopy.Close()
                                End Try
                            End If
                        Else
                            If tblTemp IsNot Nothing And tblTemp.Rows.Count > 0 Then
                                Dim bulkCopy As New SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction)
                                bulkCopy.DestinationTableName = "Wo_Tec_Temp"

                                bulkCopy.ColumnMappings.Clear()

                                bulkCopy.ColumnMappings.Add("ln", "line")
                                bulkCopy.ColumnMappings.Add("tec", "wo_tec")
                                bulkCopy.ColumnMappings.Add("proc", "wo_procName")
                                bulkCopy.ColumnMappings.Add("total", "wo_gl")
                                bulkCopy.ColumnMappings.Add("createdby", "wo_createdBy")

                                Try
                                    bulkCopy.WriteToServer(tblTemp)
                                Catch
                                    IsError = True
                                Finally
                                    tblError.Dispose()
                                    bulkCopy.Close()
                                End Try

                            End If
                        End If
                    End If
                End With
                'myDataset.Reset()
                If IsError = False Then
                    If haveError = True Then
                        ltlAlert.Text = "alert('工艺工序导入结束，有错误！'); window.location.href='" & chk.urlRand("/wo_cost/wo_workproImport.aspx?err=y") & "';"
                    Else
                        Dim params As SqlParameter = New SqlParameter("@uid", Session("uID"))
                        Dim retValue As Integer = SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, "sp_wo_InsertWoTec", params)

                        If retValue = 1 Then
                            ltlAlert.Text = "alert('工艺工序导入成功！'); window.location.href='" & chk.urlRand("/wo_cost/wo_workproImport.aspx") & "';"
                        ElseIf retValue = 2 Then
                            ltlAlert.Text = "alert('工艺工序导入结束，有错误！'); window.location.href='" & chk.urlRand("/wo_cost/wo_workproImport.aspx?err=y") & "';"
                        Else
                            ltlAlert.Text = "alert('工艺工序导入失败，请重试！'); window.location.href='" & chk.urlRand("/wo_cost/wo_workproImport.aspx") & "';"
                        End If
                    End If
                Else
                    ltlAlert.Text = "alert('工艺工序导入失败，请重试！'); window.location.href='" & chk.urlRand("/wo_cost/wo_workproImport.aspx") & "';"
                End If
            End If
        End If
    End Sub

    Sub DeleteHis()

        strSQL = " Delete From ImportError Where userID = '" & Session("uID") & "' "
        strSQL &= " Delete From wo_partcost_tmp Where userID = '" & Session("uID") & "' "
        strSQL &= " Delete From wo_tec_temp Where wo_createdBy = '" & Session("uID") & "' "
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
    End Sub

End Class
