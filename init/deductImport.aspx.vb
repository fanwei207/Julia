Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb

Namespace tcpc
Partial Class deductImport
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer

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
            
            FileTypeDropDownList1.SelectedIndex = 0
            Dim item1 As ListItem
            item1 = New ListItem("Excel (.xls) file with Sheet1$")
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

        Dim strSQL1 As String

        Dim strFileName As String = ""
        Dim strCatFolder As String
        Dim strUserFileName As String
        Dim intLastBackslash As Integer
        Dim reader As SqlDataReader

        strCatFolder = Server.MapPath("/import")
        If Not Directory.Exists(strCatFolder) Then
            Try
                Directory.CreateDirectory(strCatFolder)
            Catch
                ltlAlert.Text = "alert('创建文件目录失败(1001)！.')"
                Return
            End Try
        End If

        strUserFileName = filename1.PostedFile.FileName
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

        If Not (filename1.PostedFile Is Nothing) Then
            If (filename1.PostedFile.ContentLength > 8388608) Then
                ltlAlert.Text = "alert('上传的文件最大为 8 MB.')"
                Return
            End If
            Try
                filename1.PostedFile.SaveAs(strFileName)
            Catch
                ltlAlert.Text = "alert('上传文件失败(1002)！.')"
                Return
            End Try

            If (File.Exists(strFileName)) Then
                Dim userNo, dtime, type, deductType, qty, price, notes As String
                Dim typeID, deductTypeID As String

                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset
                        If .Rows.Count > 0 Then
                            strSQL = " delete from DeductMoneyImport where createdBy=" & Session("uID")
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                            Dim str As String
                            For i = 0 To .Rows.Count - 1
                                Dim line As String = (i + 2).ToString()
                                'a
                                If .Rows(i).IsNull(0) Then
                                    userNo = ""
                                Else
                                    userNo = .Rows(i).Item(0)
                                End If
                                'b
                                If .Rows(i).IsNull(1) Then
                                    dtime = ""
                                Else
                                    dtime = .Rows(i).Item(1)
                                End If
                                'c
                                If .Rows(i).IsNull(2) Then
                                    type = ""
                                Else
                                    type = .Rows(i).Item(2)
                                End If
                                'd
                                If .Rows(i).IsNull(3) Then
                                    deductType = ""
                                Else
                                    deductType = .Rows(i).Item(3)
                                End If
                                'e
                                If .Rows(i).IsNull(4) Then
                                    qty = "0"
                                Else
                                    qty = .Rows(i).Item(4)
                                End If
                                'f
                                If .Rows(i).IsNull(5) Then
                                    price = "0"
                                Else
                                    price = .Rows(i).Item(5)
                                End If
                                'g
                                If .Rows(i).IsNull(6) Then
                                    notes = ""
                                Else
                                    notes = .Rows(i).Item(6)
                                End If
                                If userNo.Trim().Length <= 0 And dtime.Trim().Length <= 0 Then

                                Else
                                    If (userNo.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & line & "，工号不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    strSQL = " select u.userid from tcpc0.dbo.users u Inner Join tcpc0.dbo.Systemcode sc on sc.systemCodeID=u.workTypeID Inner Join tcpc0.dbo.SystemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID and sct.systemCodeTypeName=N'Work Type' where u.userNo='" & userNo.Trim & "' and u.plantCode='" & Session("PlantCode") & "'"
                                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) <= 0 Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & line & "，工号不存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (dtime.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & line & "，日期不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    Else
                                        If (IsDate(dtime.Trim()) = False) Then
                                            ltlAlert.Text = "alert('文件格式错误(9) --行 " & line & "，日期格式非法！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If

                                    typeID = "0"
                                    deductTypeID = "0"
                                    If (type.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & line & "，扣款性质不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    Else

                                        strSQL = "select systemcodeid from tcpc0.dbo.systemcode sc Inner Join tcpc0.dbo.systemcodetype sct on sct.systemcodetypeid=sc.systemcodetypeid and sct.systemCodeTypeName='Deduct Money Type' where sc.systemCodeName=N'" & type.Trim.Replace(" ", "") & "'"
                                        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                                        If reader.Read() Then
                                            typeID = reader(0).ToString()
                                        End If
                                        reader.Close()
                                    End If
                                    If (deductType.Trim().Length <= 0 And notes.Trim.Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & line & "，扣款事项和备注不能同时为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf (deductType.Trim().Length > 0) Then
                                        Dim ok As Integer = 0
                                        strSQL = "select id from DeductMoneyType where name=N'" & deductType.Trim.Replace(" ", "") & "' and deductTypeID='" & typeID & "'"
                                        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                                        If reader.Read() Then
                                            deductTypeID = reader(0).ToString()
                                            ok = 1
                                        End If
                                        reader.Close()
                                        If ok = 0 Then
                                            ltlAlert.Text = "alert('行 " & line & "，扣款事项不存在或者不属于性质项！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If

                                    If (qty.Trim().Length = "0" And price.Trim.Length = "0") Then
                                        ltlAlert.Text = "alert('行 " & line & "，扣款数量和金额不能同时为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf (qty.Trim() <> "0" And price.Trim <> "0") Then
                                        ltlAlert.Text = "alert('行 " & line & "，扣款数量和金额不能同时存在！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf (qty.Trim() <> "0") Then
                                        If type.Trim() = "违纪" Or type.Trim() = "质量" Then
                                            ltlAlert.Text = "alert('行 " & line & "，违纪或质量需输入扣款金额！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                        If Not IsNumeric(qty.Trim()) Then
                                            ltlAlert.Text = "alert('行 " & line & "，扣款数量必须是数字！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    ElseIf (price.Trim() <> "0") Then
                                        If type.Trim() = "自损" Or type.Trim() = "请假" Then
                                            ltlAlert.Text = "alert('行 " & line & "，自损或请假需输入扣款数量！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                        If Not IsNumeric(price.Trim()) Then
                                            ltlAlert.Text = "alert('行 " & line & "，扣款金额必须是数字！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If

                                    str = userNo.Trim & "~" & dtime.Trim & "~" & typeID & "~"
                                    str &= deductTypeID.Trim & "~" & notes.Trim & "~" & qty.Trim & "~" & price & "~^"

                                    'Response.Write(str)
                                    'Exit Sub
                                    strSQL = "salary_SaveDeductMoneyImport"
                                    Dim params(1) As SqlParameter
                                    params(0) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                                    params(1) = New SqlParameter("@str", str.Trim())
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                End If
                            Next
                            Dim mistake As Integer = 0
                            strSQL = "Salary_MoveDeductImport"
                            Dim param As SqlParameter
                            param = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                            mistake = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, param)

                            If mistake > 0 Then
                                ltlAlert.Text = "alert('扣款信息导入成功.');"
                            Else
                                ltlAlert.Text = "alert('扣款信息导入失败,请重新导入.');"
                            End If

                        End If
                    End With
                myDataset.Reset()
                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If
            End If
        End If
    End Sub

End Class

End Namespace
