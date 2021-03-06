Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc


Partial Class outputImport
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
                Dim userNo, userName, dtime, workprec, output, holiday As String
                Dim workprecid, guideline, unitprice As String

                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset
                        If .Rows.Count > 0 Then
                            strSQL = " delete from JobSalaryImport where createdBy=" & Session("uID")
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                            strSQL = " Insert into test_for_salary_import (createdBy,createdDate,notes) values(" & Session("uID") & ",getdate(),'import start')"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                            Dim str As String
                            For i = 0 To .Rows.Count - 1
                                'a
                                If .Rows(i).IsNull(0) Then
                                    userNo = ""
                                Else
                                    userNo = .Rows(i).Item(0)
                                    While userNo.Substring(0, 1) = "0"
                                        userNo = userNo.Substring(1)
                                    End While
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
                                    workprec = ""
                                Else
                                    workprec = .Rows(i).Item(3)
                                End If
                                'e
                                If .Rows(i).IsNull(4) Then
                                    output = ""
                                Else
                                    output = .Rows(i).Item(4)
                                End If
                                'f
                                If .Rows(i).IsNull(5) Then
                                    holiday = "0"
                                Else
                                    holiday = .Rows(i).Item(5)
                                End If

                                If userNo.Trim().Length <= 0 And userName.Trim().Length <= 0 And workprec.Trim().Length <= 0 Then

                                Else
                                    If (userNo.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，工号不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (userName.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，姓名不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    strSQL = " select u.userid from tcpc0.dbo.users u Inner Join tcpc0.dbo.Systemcode sc on sc.systemCodeID=u.workTypeID Inner Join tcpc0.dbo.SystemCodeType sct on sct.systemCodeTypeID=sc.systemCodeTypeID and sct.systemCodeTypeName=N'Work Type' where u.userName= N'" & userName & "' and u.userNo='" & userNo.Trim & "' and u.plantCode='" & Session("PlantCode") & "'"
                                    If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) <= 0 Then
                                        ltlAlert.Text = "alert('文件格式错误(2) --行 " & (i + 2).ToString & "，工号和姓名不匹配或不是计件工" & userNo.Trim & "！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    End If

                                    If (dtime.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，日期不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    Else
                                        If (IsDate(dtime.Trim()) = False) Then
                                            ltlAlert.Text = "alert('文件格式错误(9) --行 " & (i + 2).ToString & "，日期格式非法！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If

                                    workprecid = ""
                                    guideline = ""
                                    unitprice = ""
                                    If (workprec.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，工序不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    Else
                                        strSQL = "select top 1 id,guideLine,newdeductprice from workProcedure where replace(name,' ','')=N'" & workprec.Trim.Replace(" ", "") & "' and wdate<='" & dtime.Trim() & "' and flag=0 order by wdate desc"
                                        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                                        If reader.Read() Then
                                            workprecid = reader(0).ToString()
                                            guideline = reader(1).ToString()
                                            unitprice = reader(2).ToString()
                                        Else
                                            reader.Close()
                                            ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，工序不存在！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                        reader.Close()
                                    End If
                                    'Add by baoxin in 2006 
                                    If (guideline = "0") Then
                                        If (CDec(unitprice) * CDec(output) / 8) >= 999.999999 Then
                                            ltlAlert.Text = "alert('行 " & (i + 2).ToString & "工时/产量输入有误！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    Else
                                        If (CDec(unitprice) * CDec(output) >= 999.999999) Then
                                            ltlAlert.Text = "alert('行 " & (i + 2).ToString & "工时/产量输入有误！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                        End If
                                    End If

                                    If (output.Trim().Length <= 0) Then
                                        ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，工时产量不能为空！');"
                                        myDataset.Reset()
                                        File.Delete(strFileName)
                                        Return
                                    Else
                                        If Not IsNumeric(output.Trim()) Then
                                            ltlAlert.Text = "alert('行 " & (i + 2).ToString & "，工时产量必须为数字！');"
                                            myDataset.Reset()
                                            File.Delete(strFileName)
                                            Return
                                        End If
                                    End If


                                    str = userNo.Trim & "~" & userName.Trim & "~" & dtime.Trim & "~" & workprecid & "~"
                                    str &= guideline.Trim & "~" & unitprice.Trim & "~" & output.Trim & "~" & holiday & "~"
                                    str &= "~~~~~~~" & (i + 2).ToString & "~" & "~^"

                                    'Response.Write(str)

                                    strSQL = "salary_SaveJobSalaryImport"
                                    Dim params(1) As SqlParameter
                                    params(0) = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                                    params(1) = New SqlParameter("@str", str)
                                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.StoredProcedure, strSQL, params)
                                End If
                            Next
                            'strSQL = " Insert into test_for_salary_import (createdBy,createdDate,notes) values(" & Session("uID") & ",getdate(),'import end')"
                            'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                            Dim mistake As Integer = 0
                            strSQL = "Salary_MoveJobSalaryImport"
                            Dim param As SqlParameter
                            param = New SqlParameter("@uid", Convert.ToInt32(Session("uID")))
                            mistake = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, param)

                            'strSQL = " Insert into test_for_salary_import (createdBy,createdDate,notes) values(" & Session("uID") & ",getdate(),'move import end')"
                            'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                            If mistake > 0 Then
                                ltlAlert.Text = "alert('日产量信息导入成功.');"
                            Else
                                ltlAlert.Text = "alert('日产量信息导入失败,请重新导入.');"
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
