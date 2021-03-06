Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class peopleAttendanceImport
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
                Dim userNo, userName, dtime, holiday, start1, end1, rest1, over24, start2, end2, rest2, twotimes As String

                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset
                        If .Rows.Count > 0 Then
                            strSQL = " delete from PieceAttendanceImport where createdBy=" & Session("uID")
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                            strSQL = " delete from OvernightImport where createdBy=" & Session("uID")
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                            Dim str As String
                            For i = 0 To .Rows.Count - 1
                                'a
                                If .Rows(i).IsNull(0) Then
                                    userNo = ""
                                Else
                                    userNo = .Rows(i).Item(0)
                                End If
                                'b
                                If .Rows(i).IsNull(1) Then
                                    userName = ""
                                Else
                                    userName = .Rows(i).Item(1)
                                End If
                                'c
                                If .Rows(i).IsNull(2) Then
                                    dtime = ""
                                Else
                                    dtime = .Rows(i).Item(2)
                                End If
                                'd
                                If .Rows(i).IsNull(3) Then
                                    holiday = "0"
                                Else
                                    holiday = .Rows(i).Item(3)
                                End If
                                'e
                                If .Rows(i).IsNull(4) Then
                                    start1 = "0000"
                                Else
                                    start1 = .Rows(i).Item(4)
                                    While (start1.Length < 4)
                                        start1 = "0" & start1
                                    End While
                                End If
                                'f
                                If .Rows(i).IsNull(5) Then
                                    end1 = "0000"
                                Else
                                    end1 = .Rows(i).Item(5)
                                    While (end1.Length < 4)
                                        end1 = "0" & end1
                                    End While
                                End If
                                'g
                                If .Rows(i).IsNull(6) Then
                                    rest1 = "0"
                                Else
                                    rest1 = .Rows(i).Item(6)
                                End If
                                'h
                                If .Rows(i).IsNull(7) Then
                                    over24 = "0"
                                Else
                                    over24 = .Rows(i).Item(7)
                                End If
                                'i
                                If .Rows(i).IsNull(8) Then
                                    start2 = "0000"
                                    While (start2.Length < 4)
                                        start2 = "0" & start2
                                    End While

                                Else
                                    start2 = .Rows(i).Item(8)
                                End If
                                'j
                                If .Rows(i).IsNull(9) Then
                                    end2 = "0000"
                                Else
                                    end2 = .Rows(i).Item(9)
                                    While (end2.Length < 4)
                                        end2 = "0" & end2
                                    End While
                                End If
                                'k
                                If .Rows(i).IsNull(10) Then
                                    rest2 = "0"
                                Else
                                    rest2 = .Rows(i).Item(10)
                                End If
                                'l
                                If .Rows(i).IsNull(11) Then
                                    twotimes = "0"
                                Else
                                    twotimes = .Rows(i).Item(11)
                                End If
                                If userNo.Trim().Length <= 0 And userName.Trim().Length <= 0 And start1.Trim() = "0000" Then

                                Else
                                    If (userNo.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，工号不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (userName.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，姓名不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    strSQL = " select u.userid from tcpc0.dbo.users u Inner Join tcpc0.dbo.Systemcode sc on sc.systemCodeID=u.workTypeID Inner Join tcpc0.dbo.SystemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID and sct.systemCodeTypeName=N'Work Type' where u.userName= N'" & userName & "' and u.userNo='" & userNo.Trim & "' and u.plantCode='" & Session("PlantCode") & "' "
                                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) <= 0 Then
                                        ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，工号和姓名不匹配或不是计件工！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (dtime.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，日期不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    Else
                                        If (IsDate(dtime.Trim()) = False) Then
                                            ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，日期格式非法！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If
                                    Dim Tstart As Decimal = 0
                                    Dim Tend As Decimal = 0
                                    If (start1.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，上班1不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf Not IsNumeric(start1.Trim()) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，上班1必须为数字！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf CInt(start1.Substring(0, 2)) > 23 Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，上班1格式不对！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf CInt(start1.Substring(2, 2)) > 59 Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，上班1格式不对！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                    Tstart = CInt(start1.Substring(0, 2)) + Math.Round(CInt(start1.Substring(2, 2)) / 60, 2)
                                    If (end1.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，下班1不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf Not IsNumeric(end1.Trim()) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，下班1必须为数字！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf CInt(end1.Substring(0, 2)) > 23 Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，下班1格式不对！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf CInt(end1.Substring(2, 2)) > 59 Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，下班1格式不对！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                    Tend = CInt(end1.Substring(0, 2)) + Math.Round(CInt(end1.Substring(2, 2)) / 60, 2)



                                    If Not IsNumeric(rest1.Trim()) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，休息1必须为数字！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                    ' the first time is limites <=11
                                    If Tend < Tstart Then
                                        Tend = Tend + 24
                                    ElseIf Tend - Tstart - rest1 > 11.0 Then
                                        'Response.Write(start1 & "<br>" & end1 & "<br>" & (Tend - Tstart - rest1).ToString())
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，第一段时间不能大于11小时！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If
                                    'If (CInt(end1.Substring(0, 2)) + (CInt(end1.Substring(2, 2)) / 60) - (CInt(start1.Substring(0, 2)) + (CInt(start1.Substring(2, 2)) / 60)) - rest1) > 11 Then
                                    '    ltlAlert.Text = "alert('行 " & i + 2.ToString & "，上班1不能大于11小时！');"
                                    '    myDataset.Reset()
                                    '    File.Delete(strFileName)
                                    '    Return
                                    'End If


                                    If (start2.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，上班2不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf Not IsNumeric(start2.Trim()) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，上班2必须为数字！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf CInt(start2.Substring(0, 2)) > 23 Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，上班2格式不对！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf CInt(start2.Substring(2, 2)) > 59 Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，上班2格式不对！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (end2.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，下班2不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf Not IsNumeric(end2.Trim()) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，下班2必须为数字！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf CInt(end2.Substring(0, 2)) > 23 Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，下班2格式不对！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    ElseIf CInt(end2.Substring(2, 2)) > 59 Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，下班2格式不对！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If Not IsNumeric(rest2.Trim()) Then
                                        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，休息2必须为数字！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    'If twotimes <> "0" Then
                                    '    If start2 = "0000" And end2 = "0000" And rest2 = "0" Then
                                    '        ltlAlert.Text = "alert('行 " & i + 2.ToString & "，有翻班，所以上班2必须有数据！');"
                                    '        myDataset.Reset()
                                    '        File.Delete(strFileName)
                                    '        Return
                                    '    End If
                                    'End If

                                    str = userNo.Trim & "~" & userName.Trim & "~" & dtime.Trim & "~考勤~"
                                    str &= "~~~" & holiday & "~"
                                    str &= start1 & "~" & end1 & "~" & rest1 & "~" & over24 & "~" & start2 & "~" & end2 & "~" & rest2 & "~" & i + 1.ToString & "~" & twotimes & "~^"

                                    strSQL = "salary_SaveJobSalaryImport"
                                    Dim params(1) As SqlParameter
                                    params(0) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                                    params(1) = New SqlParameter("@str", str)
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                End If
                            Next
                            Dim mistake As Integer = 0
                            strSQL = "Salary_MovePieceAttendanceImport"
                            Dim param As SqlParameter
                            param = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                            mistake = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, param)
                            If mistake > 0 Then
                                ltlAlert.Text = "alert('考勤信息导入成功.');"
                            Else
                                ltlAlert.Text = "alert('考勤信息导入失败,请重新导入.');"
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
