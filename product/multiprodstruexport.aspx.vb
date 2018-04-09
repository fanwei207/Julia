Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb

Namespace tcpc
    Partial Class multiprodstruexport
        Inherits BasePage
        Dim chk As New adamClass
        Dim strSQL As String


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
                    Dim _code, _qad As String
                    Dim _id As Long

                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset
                        If .Rows.Count > 0 Then
                            strSQL = " Delete From CalPart_temp_stru Where createdBy='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
                            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                            For i = 0 To .Rows.Count - 1
                                _code = Nothing
                                _qad = Nothing
                                _id = 0

                                If .Rows(i).IsNull(0) Then
                                    _code = ""
                                Else
                                    _code = .Rows(i).Item(0)
                                End If

                                If .Rows(i).IsNull(1) Then
                                    _qad = ""
                                Else
                                    _qad = .Rows(i).Item(1)
                                End If

                                'If chk100.Checked = True Then
                                strSQL = " Select id From Items Where item_qad = '" & chk.sqlEncode(_qad.Trim()) & "'"
                                _id = Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL))

                                If _id <= 0 Then
                                    strSQL = " Select id From Items Where code = '" & chk.sqlEncode(_code.Trim()) & "'"
                                    _id = Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strSQL))
                                End If

                                strSQL = " Insert Into CalPart_temp_stru(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode)" _
                                       & " Values(0, '" & _id & "', '" & _id & "', 1, 'Final', '', 0, '" & Session("uID") & "','" & Session("plantCode") & "', '')"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)

                                GetPart(_id, 1)
                                'End If

                                strSQL = " Insert Into CalPart_temp_stru(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode)" _
                                       & " Values(-1, 0, 0, 1, '', '', 0, '" & Session("uID") & "','" & Session("plantCode") & "', '')"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                            Next
                        End If
                    End With
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If

                    strSQL = " Select Case cts.tag When 0 Then Case cts.ProductStruID When 0 Then N'产品:-->' + i.code Else Isnull(i.code, '') End " _
                           & " When 1 Then '>' + i.code When 2 Then '>>' + i.code When 3 Then '>>>' + i.code When 4 Then '>>>>' + i.code " _
                           & " When 5 Then '>>>>>' + i.code When 6 Then '>>>>>>' + i.code End As Code, Isnull(i.item_qad,'') As Qad, " _
                           & " Case isnull(i.code, '') When '' Then '' Else Cast(cts.numOfChild As Float) End As Qty, cts.childCategory, cts.notes, cts.posCode " _
                           & " , tag " _
                           & " From tcpc0.dbo.CalPart_temp_stru cts " _
                           & " Left Outer Join tcpc0.dbo.Items i On i.id = cts.childID " _
                           & " Where cts.createdBy='" & Session("uID") & "' " _
                           & " Order By cts.id"

                    Session("EXSQL") = strSQL
                    Session("EXTitle") = "250^<b>部件号</b>~^150^<b>QAD</b>~^150^<b>数量</b>~^120^<b>分类(Part/Prod)</b>~^<b>备注</b>~^<b>位号</b>~^<b>层级</b>~^"

                    ltlAlert.Text = "var w = window.open('/public/exportExcel.aspx?rm=" & DateTime.Now().ToString() _
                                  & "','_blank','menubar=No,scrollbars = No,resizable=No,width=100,height=100,top=0,left=0'); w.focus();"
                End If
            End If
        End Sub

        Sub GetPart(ByVal proID As Long, ByVal Qty As Decimal, Optional ByVal tag As Integer = 0)
            Dim reader As SqlDataReader
            strSQL = "Select ProductStruID,childID,isnull(numOfChild,0),childCategory,notes, posCode From Product_Stru Where productID=" & proID & " And numOfChild<>0 "
            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strSQL)
            While reader.Read()
                If reader("childCategory") = "PART" Then
                    strSQL = "Insert Into CalPart_temp_stru(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & reader(0) & "','" & Request("id") & "','" & reader(1) & "'," & reader(2) & "*" & Qty & ",'" & reader(3) & "',N'" & reader(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & reader(5) & "') "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                Else
                    strSQL = "Insert Into CalPart_temp_stru(ProductStruID, ProductID, childID, numOfChild, childCategory, notes, tag, createdBy, plantID, posCode) Values('" & reader(0) & "','" & Request("id") & "','" & reader(1) & "'," & reader(2) & "*" & Qty & ",'" & reader(3) & "',N'" & reader(4) & "'," & tag & "," & Session("uID") & "," & Session("plantCode") & ",N'" & reader(5) & "') "
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                    GetPart(reader(1), reader(2) * Qty, tag + 1)
                End If
            End While
            reader.Close()
        End Sub
    End Class
End Namespace
