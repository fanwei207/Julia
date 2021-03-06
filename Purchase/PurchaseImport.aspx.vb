Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc

Partial Class PurchaseImport
        Inherits BasePage

    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Protected WithEvents Comparevalidator3 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents uploadProdReplaceBtn As System.Web.UI.HtmlControls.HtmlInputButton
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

            If Request("err") = "y" Then
                Session("EXTitle1") = "500^<b>错误原因</b>~^"
                Session("EXHeader1") = ""
                Session("EXSQL1") = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" & Session("uID") & "'"
                ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
            End If

            filetypeDDL.SelectedIndex = 0
            Dim item As ListItem
            item = New ListItem("Excel (.xls) file")
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
        Dim ds As DataSet
        Dim reader As SqlDataReader

        Dim ret As Integer
        Dim retRecord As Integer = 0

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer
        Dim ErrorRecord As Integer

        strCatFolder = Server.MapPath("/import")
        If Not Directory.Exists(strCatFolder) Then
            Try
                Directory.CreateDirectory(strCatFolder)
            Catch
                ltlAlert.Text = "alert('创建文件目录失败(1001)！.')"
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
        While (i < 2000)
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
                ltlAlert.Text = "alert('上传文件失败.')"
                Return
            End Try

            If (File.Exists(strFileName)) Then
                Dim order_id, prod_id, procurement_code, part_prod_id, rate, manufactory_code, delivery_code, first_partin_date, last_partin_date, d_notes As String
                Dim plan_qty(), real_qty(), plan_date(), detail_notes() As String
                Dim len, j As Integer
                Dim tempid As String
                Dim boolError As Boolean = False
                Dim boolError1 As Boolean = False
                Dim line As Integer

                    'Dim myDataset As DataSet
                    Dim mydt As DataTable
                Try
                        'myDataset = chk.getExcelContents(strFileName)
                        mydt = GetExcelContents(strFileName)
                Catch
                    ltlAlert.Text = "alert('上传文件必须是Excel格式文件.')"
                    Return
                End Try
                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If

                Try
                        With mydt
                            Dim i11 As Integer
                            Dim colcount As Integer = 0
                            For i11 = 0 To .Columns.Count - 1
                                If (.Columns(i11).ColumnName.IndexOf("F") = -1) Then
                                    colcount = colcount + 1
                                End If
                            Next

                            If .Rows.Count > 0 Then
                                strSQL = " Delete From tcpc0.dbo.ImportError Where userID='" & Session("uID") & "'and plantID='" & Session("PlantCode") & "'"
                                SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strSQL)
                                ErrorRecord = 0

                                strSQL = " Delete From tempPurchase "
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                strSQL = "Delete From tempPurchase_detail"
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                len = (colcount - 16) / 4
                                ReDim plan_qty(len), real_qty(len), plan_date(len), detail_notes(len)
                                For i = 0 To .Rows.Count - 1
                                    line = line + 2
                                    If Not (.Rows(i).IsNull(1) And .Rows(i).IsNull(2)) Then
                                        order_id = Nothing
                                        prod_id = Nothing
                                        procurement_code = Nothing
                                        part_prod_id = Nothing
                                        rate = Nothing
                                        manufactory_code = Nothing
                                        delivery_code = Nothing
                                        first_partin_date = Nothing
                                        last_partin_date = Nothing
                                        d_notes = Nothing

                                        boolError = False
                                        boolError1 = False

                                        If .Rows(i).IsNull(1) Then
                                            prod_id = ""
                                        Else
                                            prod_id = .Rows(i).Item(1)
                                        End If

                                        If .Rows(i).IsNull(2) Then
                                            order_id = ""
                                        Else
                                            order_id = .Rows(i).Item(2)
                                        End If

                                        If .Rows(i).IsNull(8) Then
                                            procurement_code = ""
                                        Else
                                            procurement_code = .Rows(i).Item(8)
                                        End If

                                        If .Rows(i).IsNull(9) Then
                                            part_prod_id = ""
                                        Else
                                            part_prod_id = .Rows(i).Item(9)
                                        End If

                                        If .Rows(i).IsNull(10) Then
                                            rate = "1.0"
                                        Else
                                            If (IsNumeric(.Rows(i).Item(10))) Then
                                                rate = .Rows(i).Item(10)
                                            Else
                                                ltlAlert.Text = "alert('第" & line & "行比率不是数字');"
                                                Exit Sub
                                            End If
                                        End If


                                        If .Rows(i).IsNull(12) Then
                                            manufactory_code = ""
                                        Else
                                            manufactory_code = .Rows(i).Item(12)
                                        End If

                                        If .Rows(i).IsNull(13) Then
                                            delivery_code = ""
                                        Else
                                            delivery_code = .Rows(i).Item(13)
                                        End If

                                        If .Rows(i).IsNull(14) Then
                                            first_partin_date = ""
                                        Else
                                            If (IsDate(.Rows(i).Item(14))) Then
                                                first_partin_date = .Rows(i).Item(14)
                                            Else
                                                ltlAlert.Text = "alert('第" & line & "行首期到货日期不是日期');"
                                                Exit Sub
                                            End If
                                        End If

                                        If .Rows(i).IsNull(15) Then
                                            last_partin_date = ""
                                        Else
                                            If (IsDate(.Rows(i).Item(15))) Then
                                                last_partin_date = .Rows(i).Item(15)
                                            Else
                                                ltlAlert.Text = "alert('第" & line & "行必须到货日期不是日期');"
                                                Exit Sub
                                            End If

                                        End If


                                        If .Rows(i).IsNull(16) Then
                                            d_notes = ""
                                        Else
                                            d_notes = .Rows(i).Item(16)
                                        End If

                                        For j = 0 To len - 1
                                            If .Rows(i).IsNull(j * 4 + 18) Then
                                                plan_qty(j) = ""
                                            Else
                                                If (IsNumeric(.Rows(i).Item(j * 4 + 18))) Then
                                                    plan_qty(j) = .Rows(i).Item(j * 4 + 18)
                                                Else
                                                    'Response.Write("不是数字")
                                                    ltlAlert.Text = "alert('第" & line & "行计划数不是数字');"
                                                    Exit Sub
                                                End If

                                            End If

                                            If .Rows(i).IsNull(j * 4 + 19) Then
                                                real_qty(j) = ""
                                            Else
                                                If (IsNumeric(.Rows(i).Item(j * 4 + 19))) Then
                                                    real_qty(j) = .Rows(i).Item(j * 4 + 19)
                                                Else

                                                    ltlAlert.Text = "alert('第" & line & "行实到数不是数字');"
                                                    Exit Sub
                                                End If

                                            End If

                                            If .Rows(i).IsNull(j * 4 + 17) Then
                                                plan_date(j) = ""
                                            Else
                                                If (IsDate(.Rows(i).Item(j * 4 + 17))) Then
                                                    plan_date(j) = .Rows(i).Item(j * 4 + 17)
                                                Else
                                                    ltlAlert.Text = "alert('第" & line & "行计划日期不是日期');"
                                                    Exit Sub
                                                End If

                                            End If

                                            If .Rows(i).IsNull(j * 4 + 20) Then
                                                detail_notes(j) = ""
                                            Else
                                                detail_notes(j) = .Rows(i).Item(j * 4 + 20)
                                            End If
                                        Next
                                        strSQL = "insert into tempPurchase (order_code,prod_id,procurement_code,part_prod_id,rate,manufactory_code,delivery_code,first_partin_date,last_partin_date,d_notes,line) " _
                                        & "values('" & order_id & "','" & prod_id & "',N'" & procurement_code & "',N'" & part_prod_id & "','" & rate & "'," _
                                        & "'" & manufactory_code & "','" & delivery_code & "','" & first_partin_date & "','" & last_partin_date & "',N'" & d_notes & "','" & line & "')"

                                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                                        strSQL = "select top 1 id from tempPurchase order by id desc "
                                        tempid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                        For j = 0 To len - 1
                                            strSQL = "insert into tempPurchase_detail (tempPurchaseid,plan_qty,real_qty,plan_date,notes) " _
                                                    & "values('" & tempid & "','" & plan_qty(j) & "','" & real_qty(j) & "','" & plan_date(j) & "',N'" & detail_notes(j) & "')"
                                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                        Next
                                    End If
                                Next
                            End If
                        End With
                        'myDataset.Reset()

                    strSQL = "Purchase_PurchaseImport"
                    Dim params(3) As SqlParameter
                    params(0) = New SqlParameter("@modify_by", Session("uID"))
                    params(1) = New SqlParameter("@role", Session("uRole"))
                    params(2) = New SqlParameter("@locate", Session("PlantCode"))
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSQL, params)

                    strSQL = "select count(*) from tcpc0.dbo.ImportError where userID='" & Session("uID") & "'"
                    retRecord = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)

                    If retRecord = 0 Then
                        ltlAlert.Text = "alert('导入成功！');window.location.href='/Purchase/PurchaseImport.aspx?rm=" & DateTime.Now() & Rnd() & "';"
                    Else
                        ltlAlert.Text = "alert('导入有错误！');window.location.href='/Purchase/PurchaseImport.aspx?err=y';"
                    End If
                Catch
                    ltlAlert.Text = "alert('导入文件失败！');"
                    Return
                End Try
            End If
        End If
    End Sub
End Class

End Namespace
