Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb

Partial Class init_Wo_tecChange
    Inherits BasePage
    Public chk As New adamClass
    Dim nRet As Integer
    Dim strSQL As String
    Dim reader As SqlDataReader

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""
        If Not (IsPostBack) Then

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

        Dim ds As DataSet

        Dim _id As Integer = 0

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer

        'Dim ErrorRecord As Integer
        Dim strerr As String
        Dim IsError As Boolean

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
                Dim _NewProc As String
                Dim _total As String
                Dim startT As String
                Dim endT As String



                Dim myDataset As DataTable
                Try
                    myDataset = Me.GetExcelContents(strFileName)
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

                With myDataset
                    If .Rows.Count > 0 Then
                        If .Columns(0).ColumnName <> "工艺流程" Or .Columns(1).ColumnName <> "原工序名称" Or .Columns(2).ColumnName <> "新工序名称" Or .Columns(3).ColumnName <> "指标" Or .Columns(5).ColumnName <> "结束日期" Then
                            myDataset.Reset()
                            ltlAlert.Text = "alert('模板不正确.'); "
                            Exit Sub
                        End If

                        DeleteHis()
                        For i = 0 To .Rows.Count - 1  '// 循环Excel文件

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
                                _NewProc = ""
                            Else
                                _NewProc = .Rows(i).Item(2)
                            End If

                            If .Rows(i).IsNull(3) Then
                                _total = ""
                            Else
                                _total = .Rows(i).Item(3)
                            End If

                            If .Rows(i).IsNull(4) Then
                                startT = ""
                            Else
                                startT = .Rows(i).Item(4)
                            End If

                            If .Rows(i).IsNull(5) Then
                                endT = ""
                            Else
                                endT = .Rows(i).Item(5)
                            End If

                            If _Tec.Trim.Length = 0 Then
                                ltlAlert.Text = "alert( '行 " & i + 2.ToString & ",工艺流程不能为空！');"
                                myDataset.Reset()
                                Exit Sub
                            Else

                                If _Proc.Trim.Length = 0 Then
                                    ltlAlert.Text = "alert( '行 " & i + 2.ToString & ",工序名称不能为空！');"
                                    myDataset.Reset()
                                    Exit Sub
                                Else
                                    If _total.Trim.Length = 0 Then
                                        ltlAlert.Text = "alert( '行 " & i + 2.ToString & ",指标不能为空！');"
                                        myDataset.Reset()
                                        Exit Sub
                                    Else
                                        If Not IsNumeric(_total) Then
                                            ltlAlert.Text = "alert( '行 " & i + 2.ToString & ",指标必须为数字！');"
                                            myDataset.Reset()
                                            Exit Sub
                                        End If
                                    End If
                                End If


                                If startT.Trim.Length > 0 Then
                                    If Not IsDate(startT.Trim) Then
                                        ltlAlert.Text = "alert( '行 " & i + 2.ToString & ",开始日期有错误！');"
                                        myDataset.Reset()
                                        Exit Sub
                                    End If
                                Else
                                    ltlAlert.Text = "alert( '行 " & i + 2.ToString & ",开始日期必须输入！');"
                                    myDataset.Reset()
                                    Exit Sub
                                End If

                                If endT.Trim.Length > 0 Then
                                    If Not IsDate(startT.Trim) Then
                                        ltlAlert.Text = "alert( '行 " & i + 2.ToString & ",结束日期有错误！');"
                                        myDataset.Reset()
                                        Exit Sub
                                    End If
                                Else
                                    ltlAlert.Text = "alert( '行 " & i + 2.ToString & ",结束日期必须输入！');"
                                    myDataset.Reset()
                                    Exit Sub

                                End If
                            End If


                            strSQL = " INSERT INTO wo_ChangeTec(wo_tec, wo_proc, wo_NewProc, wo_gl, wo_start, wo_end, plantcode, wo_creat) "
                            strSQL &= " VALUES( '" & _Tec & "',N'" & _Proc & "',"
                            If _NewProc.Trim.Length = 0 Then
                                strSQL &= " N'" & _Proc.Trim & "' ,"
                            Else
                                strSQL &= " N'" & _NewProc.Trim & "' ,"
                            End If

                            strSQL &= " cast('" & _total.Trim & "' as float), "

                            If startT.Trim.Length = 0 Then
                                strSQL &= " NULL, "
                            Else
                                strSQL &= " '" & startT.Trim & "' ,"
                            End If

                            If endT.Trim.Length = 0 Then
                                strSQL &= " NULL, "
                            Else
                                strSQL &= " '" & endT.Trim & "' ,"
                            End If
                            strSQL &= "  '" & Convert.ToInt32(Session("plantcode")) & "','" & Convert.ToInt32(Session("uid")) & "' ) "
                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                        Next

                    End If
                End With
                myDataset.Reset()

                strSQL = "sp_wo_ChangeTecNew1"
                Dim param(2) As SqlParameter
                param(0) = New SqlParameter("@uID", Convert.ToInt32(Session("uID")))
                param(1) = New SqlParameter("@plantCode", Convert.ToInt32(Session("plantCode")))
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.StoredProcedure, strSQL, param) <= 0 Then
                    ltlAlert.Text = "alert('导入成功.');"
                Else
                    ltlAlert.Text = "alert('导入失败.');"
                End If



            End If
        End If
    End Sub

    Sub DeleteHis()
        strSQL = " Delete From wo_ChangeTec Where wo_creat = '" & Session("uID") & "' AND PlantCode ='" & Session("plantcode") & "' "
        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
    End Sub
End Class
