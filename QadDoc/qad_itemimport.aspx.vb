Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb

Namespace tcpc

    Partial Class qad_itemimport
        Inherits BasePage 
        Public chk As New adamClass
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
                If Request("err") = "y" Then
                    Session("EXTitle1") = "120^<b>QAD Item</b>~^60^<b>Status</b>~^300^<b>Description</b>~^80^<b>QAD Desc1</b>~^80^<b>QAD Desc2</b>~^300^<b>Error</b>~^"
                    Session("EXHeader1") = ""
                    Session("EXSQL1") = " Select qad,status,desc0,desc1,desc2,errcode From qaddoc.dbo.qad_items_tmp Where createdby='" & Session("uID") & "' order by id"
                    ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
                End If

                FileTypeDropDownList1.SelectedIndex = 0
                Dim item1 As ListItem
                item1 = New ListItem("Excel (.xls) file")
                item1.Value = 0
                FileTypeDropDownList1.Items.Add(item1)
            End If
        End Sub 

        Protected Sub btnImport_Click(sender As Object, e As System.EventArgs) Handles btnImport.Click
            If (Session("uID") = Nothing) Then
                Exit Sub
            End If
            ImportExcelFile()
        End Sub

        Sub ImportExcelFile()
            Dim strSQL As String

            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer

            Dim ErrorRecord As Integer
            Dim strerr As String
            Dim IsError As Boolean

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
                    Dim _qad As String
                    Dim _status As String
                    Dim _desc0 As String
                    Dim _desc1 As String
                    Dim _desc2 As String   
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

                    Dim dt As DataTable = myDatadt
                    If dt.Rows.Count > 0 Then
                        If dt.Columns(0).ColumnName <> "QAD" Or dt.Columns(1).ColumnName <> "状态" Or dt.Columns(2).ColumnName <> "详细描述" Then
                            'myDataset.Reset()
                            ltlAlert.Text = "alert('Template is incorrect.'); "
                            Exit Sub
                        End If

                        strSQL = " Delete From qaddoc.dbo.qad_items_tmp Where createdby = '" & Session("uID") & "' "
                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                        ErrorRecord = 0
                        IsError = False 
                    End If


                    Dim TempTable As New DataTable("TempTable")
                    Dim TempColumn As DataColumn
                    Dim TempRow As DataRow
                       
                    TempColumn = New DataColumn()
                    TempColumn.DataType = System.Type.GetType("System.String")
                    TempColumn.ColumnName = "qad"
                    TempTable.Columns.Add(TempColumn)

                    TempColumn = New DataColumn()
                    TempColumn.DataType = System.Type.GetType("System.String")
                    TempColumn.ColumnName = "status"
                    TempTable.Columns.Add(TempColumn)

                    TempColumn = New DataColumn()
                    TempColumn.DataType = System.Type.GetType("System.String")
                    TempColumn.ColumnName = "desc0"
                    TempTable.Columns.Add(TempColumn)

                    TempColumn = New DataColumn()
                    TempColumn.DataType = System.Type.GetType("System.String")
                    TempColumn.ColumnName = "desc1"
                    TempTable.Columns.Add(TempColumn)

                    TempColumn = New DataColumn()
                    TempColumn.DataType = System.Type.GetType("System.String")
                    TempColumn.ColumnName = "desc2"
                    TempTable.Columns.Add(TempColumn)

                    TempColumn = New DataColumn()
                    TempColumn.DataType = System.Type.GetType("System.String")
                    TempColumn.ColumnName = "createdby"
                    TempTable.Columns.Add(TempColumn)

                    TempColumn = New DataColumn()
                    TempColumn.DataType = System.Type.GetType("System.String")
                    TempColumn.ColumnName = "errcode"
                    TempTable.Columns.Add(TempColumn) 
                    Try 
                        With myDatadt
                            If .Rows.Count > 0 Then
                                For i = 0 To .Rows.Count - 1
                                    strerr = ""

                                    If .Rows(i).IsNull(0) Then
                                        _qad = ""
                                    Else
                                        _qad = .Rows(i).Item(0)
                                    End If

                                    If .Rows(i).IsNull(1) Then
                                        _status = ""
                                    Else
                                        _status = .Rows(i).Item(1)
                                    End If

                                    If .Rows(i).IsNull(2) Then
                                        _desc0 = ""
                                    Else
                                        _desc0 = .Rows(i).Item(2)
                                    End If

                                    If .Rows(i).IsNull(3) Then
                                        _desc1 = " "
                                    Else
                                        _desc1 = .Rows(i).Item(3)
                                    End If

                                    If .Rows(i).IsNull(4) Then
                                        _desc2 = ""
                                    Else
                                        _desc2 = .Rows(i).Item(4)
                                    End If

                                    If _qad.Trim.Length <> 14 Or IsNumeric(_qad) = False Then
                                        strerr &= "The length of item must be 14;"
                                        IsError = True
                                    End If
                                    If _status.Trim.Length > 10 Then
                                        strerr &= "The max length of status is 10;"
                                        IsError = True
                                    End If
                                    If _desc0.Length > 500 Then
                                        strerr &= "The max length of description is 500;"
                                        IsError = True
                                    End If
                                    If _desc1.Length > 24 Then
                                        strerr &= "The max length of QAD desc1 is 24;"
                                        IsError = True
                                    End If
                                    If _desc2.Length > 24 Then
                                        strerr &= "The max length of QAD desc2 is 24;"
                                        IsError = True
                                    End If

                                    TempRow = TempTable.NewRow()
                                    TempRow("qad") = _qad
                                    TempRow("status") = _status
                                    TempRow("desc0") = _desc0
                                    TempRow("desc1") = _desc1
                                    TempRow("desc2") = _desc2
                                    TempRow("createdby") = Session("uID").ToString()
                                    TempRow("errcode") = strerr
                                    TempTable.Rows.Add(TempRow)

                                    'strSQL = "insert into qaddoc.dbo.qad_items_tmp(qad,status,desc0,desc1,desc2,createdby,errcode) values('" & _qad & "','" & _status & "',N'" & _desc0 & "',N'" & _desc1 & "',N'" & _desc2 & "','" & Session("uID") & "',N'" & strerr & "') "
                                    'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL) 
                                Next
                            Else
                                ltlAlert.Text = "alert('It's failure');"
                                Return
                            End If
                        End With

                        'TempTable有数据的情况下批量复制到数据库里
                        If (TempTable IsNot Nothing And TempTable.Rows.Count > 0) Then 
                            Dim connect As String = ConfigurationManager.AppSettings("SqlConn.Conn_qaddoc") 
                            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(connect, SqlBulkCopyOptions.UseInternalTransaction)
                                bulkCopy.DestinationTableName = "qad_items_tmp"
                                bulkCopy.ColumnMappings.Clear()
                                bulkCopy.ColumnMappings.Add("qad", "qad")
                                bulkCopy.ColumnMappings.Add("status", "status")
                                bulkCopy.ColumnMappings.Add("desc0", "desc0")
                                bulkCopy.ColumnMappings.Add("desc1", "desc1")
                                bulkCopy.ColumnMappings.Add("desc2", "desc2")
                                bulkCopy.ColumnMappings.Add("createdby", "createdby")
                                bulkCopy.ColumnMappings.Add("errcode", "errcode")

                                Try 
                                    bulkCopy.WriteToServer(TempTable)

                                Catch ex As Exception
                                    ltlAlert.Text = "alert('导入时出错，请联系系统管理员A！');"
                                    Return
                                Finally 
                                    TempTable.Dispose()
                                    bulkCopy.Close()
                                End Try
                            End Using
                        End If
                        'myDataset.Reset() 
                        strSQL = "qaddoc.dbo.qad_itemimport"
                        Dim params(2) As SqlParameter
                        params(0) = New SqlParameter("@uID", Session("uID"))
                        params(1) = New SqlParameter("@iserr", IsError)
                        ErrorRecord = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                        If ErrorRecord >= 0 Then
                            ltlAlert.Text = "alert('零件导入成功.'); window.location.href='" & chk.urlRand("/qaddoc/qad_itemimport.aspx") & "';"
                        Else
                            ltlAlert.Text = "alert('零件导入结束，有错误！'); window.location.href='" & chk.urlRand("/qaddoc/qad_itemimport.aspx?err=y") & "';"
                        End If           
                    Catch
                        ltlAlert.Text = "alert('导入时出现错误，请联系系统管理员2！');"
                        Return
                    End Try
                End If
            End If 
        End Sub

     
    End Class 
End Namespace
