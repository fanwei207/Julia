Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb

Namespace tcpc

Partial Class docitemimport
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
                    Session("EXTitle1") = "60^<b>No</b>~^150^<b>Category</b>~^150^<b>Type</b>~^250^<b>DocName</b>~^130^<b>QAD Item</b>~^300^<b>Error</b>~^"
                    Session("EXHeader1") = ""
                    Session("EXSQL1") = " Select FNO,typename,catename,name,qad,errcode From qaddoc.dbo.qad_docitem_tmp Where createdby='" & Session("uID") & "' and (errcode is not null or errcode <> '') order by id"
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
        Dim strSQL As String
        Dim ds As DataSet
        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer
        Dim ErrorRecord As Integer
        Dim IsError As Integer
        Dim strerr As String = ""

        strCatFolder = Server.MapPath("/qaddocitemimport")
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
            If (filename1.PostedFile.ContentLength > 20971520) Then
                    ltlAlert.Text = "alert('The max size of file is 20 MB.')"
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
            Dim _typename As String
            Dim _catename As String
            Dim _name As String
            Dim _qad As String
                Dim myDataset As DataTable
            Dim paramData As SqlParameter

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
                        If .Columns(0).ColumnName <> "序号" Or .Columns(1).ColumnName <> "类型" Or .Columns(2).ColumnName <> "分类" Or .Columns(3).ColumnName <> "文档名称" Or .Columns(4).ColumnName <> "QAD号" Then
                            myDataset.Reset()
                            ltlAlert.Text = "alert('Template is incorrect.'); "
                            Exit Sub
                        End If
                        strSQL = " Delete From qaddoc.dbo.qad_docitem_tmp Where createdby = '" & Session("uID") & "' "
                        SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                        ErrorRecord = 0
                        IsError = 0

                        For i = 0 To .Rows.Count - 1

                            If .Rows(i).IsNull(0) Then
                                _FNO = ""
                            Else
                                _FNO = .Rows(i).Item(0)
                            End If

                            If .Rows(i).IsNull(1) Then
                                _typename = ""
                            Else
                                _typename = .Rows(i).Item(1)
                            End If

                            If .Rows(i).IsNull(2) Then
                                _catename = ""
                            Else
                                _catename = .Rows(i).Item(2)
                            End If

                            If .Rows(i).IsNull(3) Then
                                _name = ""
                            Else
                                _name = .Rows(i).Item(3)
                            End If

                            If .Rows(i).IsNull(4) Then
                                _qad = ""
                            Else
                                _qad = .Rows(i).Item(4)
                            End If

                            If _typename <> "" And _catename <> "" And _name <> "" And _qad <> "" Then
                                If System.Text.Encoding.Default.GetBytes(_qad).Length <> 14 Or IsNumeric(_qad) = False Then
                                    strerr &= "The length of QAD item must be 14"
                                    IsError = -1
                                    strSQL = "insert into qaddoc.dbo.qad_docitem_tmp(typename,catename,name,qad,createdby,FNO,errcode) values(N'" & _typename & "',N'" & _catename & "',N'" & _name & "',N'" & _qad & "','" & Session("uID") & "','" & _FNO & "',N'" & strerr & "') "
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL, paramData)
                                Else

                                    strSQL = "insert into qaddoc.dbo.qad_docitem_tmp(typename,catename,name,qad,createdby,FNO) values(N'" & _typename & "',N'" & _catename & "',N'" & _name & "',N'" & _qad & "','" & Session("uID") & "','" & _FNO & "') "
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL, paramData)
                                End If
                            End If
                        Next
                    End If
                End With
            myDataset.Reset()

            strSQL = "qaddoc.dbo.qad_docitemimport"
            Dim params(2) As SqlParameter
            params(0) = New SqlParameter("@uID", Session("uID"))
            params(1) = New SqlParameter("@iserr", IsError)

            ErrorRecord = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)

            If ErrorRecord >= 0 Then
                    ltlAlert.Text = "alert('It's done.'); window.location.href='" & chk.urlRand("/qaddoc/qad_docitemimport.aspx") & "';"
            Else
                    ltlAlert.Text = "alert('Error'); window.location.href='" & chk.urlRand("/qaddoc/qad_docitemimport.aspx?err=y") & "';"
            End If
            'Catch ex As Exception
            '   ltlAlert.Text = "alert('文档零件关联失败！" & ex.Message & "');"
            '  Return
            ' End Try
        End If
    End Sub 
    End Class
End Namespace

