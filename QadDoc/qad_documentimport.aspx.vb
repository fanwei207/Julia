Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb



' modified by wangcaixia 20130428 

Namespace tcpc

    Partial Class qad_documentimport
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strSql As String



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
                    Session("EXTitle1") = "80^<b>No</b>~^120^<b>Category</b>~^60^<b>Type</b>~^180^<b>DocName</b>~^80^<b>Version</b>~^150^<b>Description</b>~^60^<b>Stop</b>~^50^<b>isPublic</b>~^50^<b>For all items</b>~^200^<b>FileName</b>~^300^<b>Error</b>~^"
                    Session("EXHeader1") = ""
                    Session("EXSQL1") = " Select FNO,typename,catename,name,version,description,case when isstop = 0 then 0 else 1 end ,case when ispublic=0 then 0 else 1 end ,case when isall=0 then 0 else 1 end,filename,errcode From qaddoc.dbo.qad_document_tmp Where createdby='" & Session("uID") & "' and (errcode is not null or errcode <> '') order by id"
                    ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
                End If

                FileTypeDropDownList1.SelectedIndex = 0
                Dim item1 As ListItem
                item1 = New ListItem("Excel (.xls) file")
                item1.Value = 0
                FileTypeDropDownList1.Items.Add(item1)
            End If
        End Sub

        Sub ImportExcelFile()
            Dim strSQL As String
            'Dim ds As DataSet

            Dim strFileName As String = ""
            Dim strCatFolder As String
            Dim strUserFileName As String
            Dim intLastBackslash As Integer

            Dim ErrorRecord As Integer
            Dim IsError As Integer

            strCatFolder = Server.MapPath("/qaddocimport")
            If Not Directory.Exists(strCatFolder) Then
                Try
                    Directory.CreateDirectory(strCatFolder)
                Catch
                    ltlAlert.Text = "alert('To upload file is failure.')"
                    Return
                End Try
            End If

            If Not Directory.Exists("d:\\qaddoc\\") Then
                Try
                    Directory.CreateDirectory("d:\\qaddoc\\")
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
                If (filename1.PostedFile.ContentLength > 20971520) Then
                    ltlAlert.Text = "alert('The max size is 20 MB.')"
                    Return
                End If
                Try
                    filename1.PostedFile.SaveAs(strFileName)
                Catch
                    ltlAlert.Text = "alert('To upload file is failure.')"
                    Return
                End Try
            End If

            If (File.Exists(strFileName)) Then
                Dim _FNO As String
                Dim _type As String
                Dim _category As String
                Dim _name As String
                Dim _version As String
                Dim _description As String
                Dim _docLevel As String
                Dim _picNo As String
                Dim _isstop As String
                Dim _ispublic As String
                Dim _isall As String
                Dim _docname As String
                'Dim myDataset As DataSet
                Dim myDatadt As DataTable
                Dim imgdatastream As Stream
                Dim imgdatalen As Integer
                Dim imgtype As String
                Dim imgdata() As Byte
                Dim paramData As SqlParameter
                Dim _accFileName As String '关联文件名
                Dim size As Decimal
                Dim accSize As Decimal


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
                        If .Columns(0).ColumnName <> "序号" Or .Columns(1).ColumnName <> "类型" Or .Columns(2).ColumnName <> "分类" Or .Columns(3).ColumnName <> "文档名称" Or .Columns(4).ColumnName <> "版本号" Or .Columns(5).ColumnName <> "描述" Or .Columns(6).ColumnName <> "Doc Level" Or .Columns(7).ColumnName <> "停用" Or .Columns(8).ColumnName <> "公开" Or .Columns(9).ColumnName <> "适用所有" Or .Columns(10).ColumnName <> "上传文件名称（带扩展名）" Or .Columns(11).ColumnName <> "图号" Or .Columns(12).ColumnName <> "关联文件名（带扩展名）" Then
                            'myDataset.Reset()
                            ltlAlert.Text = "alert('Template is incorrect.'); "
                            Exit Sub
                        End If

                        strSQL = " Delete From qaddoc.dbo.qad_document_tmp Where createdby = '" & Session("uID") & "' "
                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                        ErrorRecord = 0
                        IsError = 0


                        For i = 0 To .Rows.Count - 1

                            imgdatastream = Nothing
                            imgdatalen = 0
                            imgtype = ""
                            imgdata = Nothing

                            If .Rows(i).IsNull(0) Then
                                _FNO = ""
                            Else
                                _FNO = .Rows(i).Item(0)
                            End If

                            If .Rows(i).IsNull(1) Then
                                _type = ""
                            Else
                                _type = .Rows(i).Item(1)
                            End If

                            If .Rows(i).IsNull(2) Then
                                _category = ""
                            Else
                                _category = .Rows(i).Item(2)
                            End If

                            If .Rows(i).IsNull(3) Then
                                _name = ""
                            Else
                                _name = .Rows(i).Item(3)
                            End If

                            If .Rows(i).IsNull(4) Then
                                _version = ""
                            Else
                                _version = .Rows(i).Item(4)
                            End If

                            If .Rows(i).IsNull(5) Then
                                _description = ""
                            Else
                                _description = .Rows(i).Item(5)
                            End If

                            If .Rows(i).IsNull(6) Then
                                _docLevel = 0
                            Else
                                _docLevel = .Rows(i).Item(6)
                            End If

                            If .Rows(i).IsNull(7) Then
                                _isstop = ""
                            Else
                                _isstop = .Rows(i).Item(7)
                            End If

                            If .Rows(i).IsNull(8) Then
                                _ispublic = ""
                            Else
                                _ispublic = .Rows(i).Item(8)
                            End If
                            'modify by shanzm 2015-06-08:通过导入的，不能是public的
                            _ispublic = "0"

                            If .Rows(i).IsNull(9) Then
                                _isall = ""
                            Else
                                _isall = .Rows(i).Item(9)
                            End If
                            'modify by shanzm 2015-06-08:通过导入的，不能是public的
                            _isall = "0"

                            If .Rows(i).IsNull(10) Then
                                _docname = ""
                            Else
                                _docname = .Rows(i).Item(10)
                            End If

                            If .Rows(i).IsNull(11) Then
                                _picNo = ""
                            Else
                                _picNo = .Rows(i).Item(11)
                            End If

                            If .Rows(i).IsNull(12) Then
                                _accFileName = ""
                            Else
                                _accFileName = .Rows(i).Item(12)
                            End If

                            If _type <> "" And _category <> "" Then

                                If File.Exists("d:\\qaddoc\\" & _docname) Then

                                    imgdatastream = File.OpenRead("d:\\qaddoc\\" & _docname)
                                    If imgdatastream.CanRead() Then

                                        imgdatalen = imgdatastream.Length
                                        imgtype = Path.GetExtension("d:\\qaddoc\\" & _docname)
                                        size = CType(imgdatalen, Decimal) / 1024

                                        '查找关联文件
                                        If (_accFileName.Length > 0) Then

                                            If Not File.Exists("d:\\qaddoc\\" & _accFileName) Then
                                                strSQL = "insert into qaddoc.dbo.qad_document_tmp(typename,catename,name,version,description,docLevel,ispublic,isall,filename,createdby,errcode,FNO,isstop, pictureNo) values(N'" & _type & "',N'" & _category & "',N'" & _name & "',N'" & _version & "',N'" & _description & "',N'" & _docLevel & "',N'" & _ispublic & "',N'" & _isall & "',N'" & _docname & "','" & Session("uID") & "',N'关联文件不存在','" & _FNO & "',N'" & _isstop & "',N'" & _picNo & "') "
                                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                                Continue For
                                            Else
                                                Dim accFileStream As Stream = File.OpenRead("d:\\qaddoc\\" & _accFileName)
                                                accSize = CType(accFileStream.Length, Decimal) / 1024
                                                accFileStream.Close()
                                            End If

                                        End If

                                        'ReDim imgdata(imgdatalen)
                                        'imgdatastream.Read(imgdata, 0, imgdatalen)

                                        'paramData = New SqlParameter("@imgdata", SqlDbType.Image)
                                        'paramData.Value = imgdata


                                        strSQL = "insert into qaddoc.dbo.qad_document_tmp(typename,catename,name,version,description,docLevel,ispublic,isall,filename,createdby,content,contype,FNO,isstop, pictureNo, accFileName,size,accSize) values(N'" & _type & "',N'" & _category & "',N'" & _name & "',N'" & _version & "',N'" & _description & "',N'" & _docLevel & "',N'" & _ispublic & "',N'" & _isall & "',N'" & _docname & "','" & Session("uID") & "', NULL,'" & imgtype & "','" & _FNO & "','" & _isstop & "',N'" & _picNo & "', N'" & _accFileName & "'," & size & "," & accSize & ") "
                                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                                        imgdatastream.Close()

                                    Else
                                        '插入临时表表示文件不可读
                                        IsError = -1
                                        strSQL = "insert into qaddoc.dbo.qad_document_tmp(typename,catename,name,version,description,docLevel,ispublic,isall,filename,createdby,errcode,FNO,isstop, pictureNo) values(N'" & _type & "',N'" & _category & "',N'" & _name & "',N'" & _version & "',N'" & _description & "',N'" & _docLevel & "',N'" & _ispublic & "',N'" & _isall & "',N'" & _docname & "','" & Session("uID") & "',N'文件不可读','" & _FNO & "','" & _isstop & "',N'" & _picNo & "') "
                                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                                    End If


                                Else
                                    '插入临时表，表示找不到文件
                                    IsError = -2
                                    strSQL = "insert into qaddoc.dbo.qad_document_tmp(typename,catename,name,version,description,docLevel,ispublic,isall,filename,createdby,errcode,FNO,isstop, pictureNo) values(N'" & _type & "',N'" & _category & "',N'" & _name & "',N'" & _version & "',N'" & _description & "',N'" & _docLevel & "',N'" & _ispublic & "',N'" & _isall & "',N'" & _docname & "','" & Session("uID") & "',N'找不到导入文件','" & _FNO & "','" & _isstop & "',N'" & _picNo & "') "
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                End If


                            End If

                        Next
                    End If
                End With
                'myDataset.Reset()
                Dim tableid As Integer
                '正确的直接处理掉，错误的导出
                If updata.Checked = False Then
                    strSQL = "qaddoc.dbo.qad_docimport1"
                    tableid = 0
                Else
                    strSQL = "qaddoc.dbo.qad_docimport1update"
                    'tableid = 1
                    tableid = 0
                End If



                Dim params(2) As SqlParameter
                params(0) = New SqlParameter("@uID", Session("uID"))
                params(1) = New SqlParameter("@iserr", SqlDbType.Bit)
                params(1).Direction = ParameterDirection.Output

               

                Dim dataSet As DataSet
                dataSet = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.StoredProcedure, strSQL, params)

                Dim filename As String
                Dim filepath As String
                Dim accFileName As String

                If (Convert.ToBoolean(params(1).Value) = False) Then
                    tableid = 0
                End If

                For i = 0 To dataSet.Tables(tableid).Rows.Count - 1
                    ' Try
                    filename = dataSet.Tables(tableid).Rows(i)("FileName").ToString()
                    filepath = "\\TecDocs\\" & dataSet.Tables(tableid).Rows(i)("typeid").ToString() & "\\" & dataSet.Tables(tableid).Rows(i)("cateid").ToString() & "\\"
                    accFileName = dataSet.Tables(tableid).Rows(i)("accFileName").ToString()
                    'Catch ex As Exception
                    '    If tableid = 0 Then
                    '        tableid = 1
                    '    Else
                    '        tableid = 0
                    '    End If
                    '    filename = dataSet.Tables(tableid).Rows(i)("FileName").ToString()
                    '    filepath = "\\TecDocs\\" & dataSet.Tables(tableid).Rows(i)("typeid").ToString() & "\\" & dataSet.Tables(tableid).Rows(i)("cateid").ToString() & "\\"
                    '    accFileName = dataSet.Tables(tableid).Rows(i)("accFileName").ToString()
                    'End Try


                    'Response.Write("filename=" & filename & "  accFileName=" & dataSet.Tables(0).Rows(i)("accFileName").ToString() & "<br />")
                    'Response.Write("filename=" & filename & "  accFileName=" & accFileName & "<br />")

                    If Not (Directory.Exists(Server.MapPath(filepath))) Then
                        Directory.CreateDirectory(Server.MapPath(filepath))
                    End If

                    Try
                        '如果是PDF文件，则要查找一下同名AI文件，同时转入
                        If accFileName.Length > 0 Then

                            File.Move("d:\\qaddoc\\" & accFileName, Server.MapPath(filepath) & accFileName)
                        End If

                        File.Move("d:\\qaddoc\\" & filename, Server.MapPath(filepath) & filename)

                    Catch ex As Exception

                    End Try
                Next

                If Convert.ToBoolean(params(1).Value) Then
                    ltlAlert.Text = "alert('部分文件未正确导入!请查看导出的具体原因!'); window.location.href='" & chk.urlRand("/qaddoc/qad_documentimport.aspx?err=y") & "';"
                Else
                    ltlAlert.Text = "alert('导入成功!');"
                End If
            End If
        End Sub

        Protected Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
            If (Session("uID") = Nothing) Then
                Exit Sub
            End If
            ImportExcelFile()
        End Sub
    End Class

End Namespace
