Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports adamFuncs
Imports System.Data.OleDb


Namespace tcpc

    Partial Class workprocedureimport
        Inherits BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents ltlAlert As Literal
        Public chk As New adamClass
        Protected WithEvents price As System.Web.UI.WebControls.Button

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here 
            If Not (IsPostBack) Then
                Dim item As ListItem

                item = New ListItem("Excel (.xls) file")
                item.Value = 0
                Dropdownlist3.Items.Add(item)
                Dropdownlist3.SelectedIndex = 0

                year.Text = DateTime.Now.Year
                month.SelectedValue = DateTime.Now.Month
                typdropdown()
            End If
        End Sub

        Sub typdropdown()
            Dim j As Integer = 1
            type.Items.Add(New ListItem("--", "0"))
            Dim strSql As String
            Dim reader As SqlDataReader
            strSql = " Select DISTINCT  deductName From workProcedure "
            strSql &= "  Where (deductRate IS NOT NULL) AND (deductName IS NOT NULL) "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                Dim tempListItem As New ListItem
                tempListItem.Value = j
                tempListItem.Text = reader(0)
                type.Items.Add(tempListItem)
                j = j + 1
            End While
            reader.Close()

        End Sub

        Public Sub uploadBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadBtn.ServerClick
            If (Session("uID") = Nothing) Then
                Exit Sub
            End If

            If (Dropdownlist3.SelectedIndex = 0) Then
                ImportExcelFile()
            End If
        End Sub

        Private Sub ImportExcelFile()
            Dim strSQL As String
            Dim reader As SqlDataReader
            Dim bsalary As Decimal
            Dim days As Decimal
            Dim price As Decimal
            Dim hours As Decimal

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
                    Dim _code As String

                    Dim name As String
                    Dim guideLine As String
                    Dim unitPrice As Decimal
                    Dim lowestDailyWage As Decimal
                    Dim newprice As Decimal
                    Dim y As String
                    'for basic information
                    strSQL = " Select basicSalary,monthlyAvgDays,basicUnitPrice,dailyHours From BaseInfo "
                    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                    While reader.Read
                        bsalary = reader(0)
                        days = reader(1)
                        price = reader(2)
                        hours = reader(3)
                    End While
                    reader.Close()

                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset

                        If .Rows.Count > 0 Then
                            For i = 0 To .Rows.Count - 1

                                If .Rows(i).IsNull(0) Then
                                    _code = ""
                                Else
                                    _code = .Rows(i).Item(0)
                                End If


                                If .Rows(i).IsNull(1) Then
                                    guideLine = "0"
                                Else
                                    guideLine = Math.Round(.Rows(i).Item(1), 0).ToString()
                                End If

                                If .Rows(i).IsNull(2) Then
                                    unitPrice = "0"
                                Else
                                    unitPrice = .Rows(i).Item(2)
                                End If

                                If .Rows(i).IsNull(3) Then
                                    lowestDailyWage = "0"
                                Else
                                    lowestDailyWage = .Rows(i).Item(3)
                                End If
                                'fileds validation 
                                If (_code.Trim().Length <= 0) Or (unitPrice = 0 And lowestDailyWage = 0) Then

                                    ltlAlert.Text = "alert('行 " & i.ToString & "')"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If

                                If CInt(guideLine) = 0 Then
                                    y = "92"
                                    newprice = Math.Round(lowestDailyWage + (bsalary / days - price), 2)
                                    'newprice = Format(newprice, "##,##0.00")
                                Else
                                    y = "91"
                                    unitPrice = unitPrice / CInt(guideLine)
                                    newprice = unitPrice + (bsalary / days - price) / CInt(guideLine) / hours
                                End If
                                strSQL = " Insert into workProcedure(typeID,name,usedTimes,guideLine,unitPrice,lowestDailyWage,newdeductprice,organizationID) Values "
                                strSQL &= " ('" & y & "',N'" & _code & "','0','" & guideLine & "','" & unitPrice & "','" & lowestDailyWage & "','" & newprice & "','" & Session("orgID") & "' )"
                                'strSQL = "update  workProcedure Set guideLine='" & guideLine & "',unitPrice='" & unitPrice & "',lowestDailyWage='" & lowestDailyWage & "',newdeductprice='" & newprice & "' "
                                'strSQL &= " Where id='" & _code & "' "

                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                            Next
                        End If
                    End With
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    'Catch
                    'ltlAlert.Text = "alert('Import is unsuccessful (1002).')"
                    'Return
                    'End Try
                    ltlAlert.Text = "alert('单价导入成功.')"
                End If
            End If
        End Sub

        Sub outfile_click(ByVal sender As Object, ByVal e As System.EventArgs)
            ltlAlert.Text = "window.open('/admin/workprodureexcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=850,height=500,top=0,left=0') "
        End Sub

        ' Save for deductmoney
        Sub save_click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim strSql As String
            If type.SelectedIndex = 0 Then
                ltlAlert.Text = "alert('选择自损类!');"
                Exit Sub
            End If
            If money.Text = "" Then
                ltlAlert.Text = "alert('扣款不能为空!');"
                Exit Sub
            End If

            strSql = " Update workProcedure set deductPrice='" & money.Text & "' Where deductName=N'" & type.SelectedItem.Text & "' "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
        End Sub

        'Update for workshop
        Sub update_price(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim strSql As String
            Dim reader As SqlDataReader
            Dim bsalary As Decimal
            Dim days As Decimal
            Dim price As Decimal
            Dim hours As Decimal
            Dim newprice As Decimal
            Dim bdate As String = year.Text.Trim & "-" & month.SelectedValue & "-01"

            strSql = " Select basicSalary,monthlyAvgDays,basicUnitPrice,dailyHours From BaseInfo where year(workdate)='" & year.Text.Trim & "' and month(workdate)='" & month.SelectedValue & "' "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While reader.Read
                bsalary = reader(0)
                days = reader(1)
                price = reader(2)
                hours = reader(3)
            End While
            reader.Close()


            strSql = " Select * From workProcedure  "
            strSql &= " Where lastone=1 and (unitPrice <> 0 OR lowestDailyWage <> 0)"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            Dim upflag As Boolean = False
            While reader.Read
                Dim askprice As Decimal = 0
                If reader(1) = 0 Then
                    ' guideLine=0 计时
                    askprice = Math.Round(price, 2)
                    If askprice > reader(11) Then
                        'newprice = Math.Round(reader(3) + (bsalary / days - price), 2)
                        newprice = askprice
                    Else
                        newprice = reader(11)
                    End If
                    'newprice = Format(newprice, "##,##0.00")
                Else
                    ' guideLine>0 计件
                    askprice = Math.Round(price / reader(4) / hours, 6)
                    If askprice > reader(11) Then
                        'newprice = reader(2) + (bsalary / days - price) / reader(1) / hours
                        newprice = askprice
                    Else
                        newprice = reader(11)
                    End If
                End If
                If DateDiff(DateInterval.Day, reader(18), CDate(bdate)) > 0 Then
                    strSql = " Insert Into workprocedure (typeID,name,usedTimes,guideLine,unitPrice,lowestDailyWage,deductRate, organizationID,deductName,deductPrice,newdeductprice,flag,departmentID,workshopID,wallowance,wpercent,wdate,code,lastone,newdeductprice1)"
                    strSql &= " Values('" & reader(1) & "',N'" & chk.sqlEncode(reader(2)) & "','" & reader(3) & "','" & reader(4) & "','" & reader(5) & "','" & reader(6) & "',"
                    If reader(7).ToString() = "" Then
                        strSql &= " null,"
                    Else
                        strSql &= " '" & reader(7) & "',"
                    End If
                    strSql &= " '" & reader(8) & "',"
                    If reader(9).ToString() = "" Then
                        strSql &= " null,null,"
                    Else
                        strSql &= " '" & reader(9) & "','" & reader(10) & "',"
                    End If
                    strSql &= " '" & newprice.ToString & "','0','" & reader(14) & "','" & reader(15) & "','" & reader(16) & "','" & reader(17) & "','" & bdate & "','" & reader(19) & "',1,'" & reader(11) & "')"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

                    strSql = "Update workProcedure set lastone=0 "
                    strSql &= " Where id='" & reader(0) & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                Else
                    If DateDiff(DateInterval.Day, reader(18), CDate(bdate)) = 0 Then
                        strSql = " update workprocedure set newdeductprice='" & newprice.ToString & "' where id='" & reader(0) & "' "
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                    Else
                        Dim jid As Integer = 0
                        strSql = "select From workprocedure where wdate='" & bdate & "' and code='" & reader(19).ToString & "' "
                        jid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                        If jid > 0 Then
                            strSql = " update workprocedure set newdeductprice='" & newprice.ToString & "' where id='" & jid.ToString & "' "
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                        Else
                            strSql = " Insert Into workprocedure (typeID,name,usedTimes,guideLine,unitPrice,lowestDailyWage,deductRate, organizationID,deductName,deductPrice,newdeductprice,flag,departmentID,workshopID,wallowance,wpercent,wdate,code,lastone,newdeductprice1)"
                            strSql &= " Values('" & reader(1) & "',N'" & chk.sqlEncode(reader(2)) & "','" & reader(3) & "','" & reader(4) & "','" & reader(5) & "','" & reader(6) & "',"
                            If reader(7).ToString() = "" Then
                                strSql &= " null,"
                            Else
                                strSql &= " '" & reader(7) & "',"
                            End If
                            strSql &= " '" & reader(8) & "',"
                            If reader(9).ToString() = "" Then
                                strSql &= " null,null,"
                            Else
                                strSql &= " '" & reader(9) & "','" & reader(10) & "',"
                            End If
                            strSql &= " '" & newprice.ToString & "','0','" & reader(14) & "','" & reader(15) & "','" & reader(16) & "','" & reader(17) & "','" & bdate & "','" & reader(19) & "',0,'" & reader(11) & "')"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                        End If

                    End If
                End If
                'Response.Write(strSql)
                'Exit Sub




                upflag = False
            End While
            reader.Close()
            strSql = " Update Indicator set workProcedure=workProcedure+1 "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)

            ltlAlert.Text = " alert('更新完毕');"
            'Response.Redirect(chk.urlRand("workprocedure.aspx"))
        End Sub

        Sub back_click(ByVal sender As Object, ByVal e As System.EventArgs)
            Response.Redirect(chk.urlRand("workprocedure.aspx"))
        End Sub


        Sub excel_click(ByVal sender As Object, ByVal e As System.EventArgs)
            'salary()

            'yeah()
            'install()

            Dim strSQL As String
            Dim reader As SqlDataReader
            Dim bsalary As Decimal
            'Dim type As String = money.Text.Trim


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
                    Dim _code As String

                    Dim name As String
                    Dim guideLine As String
                    Dim unitPrice As Decimal
                    Dim lowestDailyWage As Decimal
                    Dim newprice As Decimal
                    Dim y As String
                    'for basic information


                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    With myDataset

                        If .Rows.Count > 0 Then
                            For i = 0 To .Rows.Count - 1
                                y = ""
                                'If .Rows(i).IsNull(0) Then
                                '    _code = ""
                                'Else
                                '    _code = .Rows(i).Item(0)
                                '    If (IsDate(_code.Trim()) = False) Then
                                '        ltlAlert.Text = "alert('行 " & i.ToString & "')"
                                '        myDataset.Reset()
                                '        File.Delete(strFileName)
                                '        Return
                                '    End If

                                'End If


                                If .Rows(i).IsNull(0) Then
                                    guideLine = ""
                                Else
                                    guideLine = .Rows(i).Item(0)

                                End If

                                strSQL = " Insert into zx(uid) Values ('" & guideLine & "')"
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                            Next
                        End If
                    End With
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If

                End If
            End If
        End Sub
        Sub install()
            Dim strSql As String
            Dim reader As SqlDataReader
            Dim query As String
            Dim roletype As New Hashtable
            Dim j As Integer
            For j = 0 To 50
                strSql = " Select top 100 code,name,isnull(enterdate,'') as enterdate,department,isnull(duty,'') as duty,isnull(birthday,'') as birthday,isnull(leavedate,'') as leavedate,isnull(IDcard,'') as IDcard,isnull(edulevel,'') as edulevel,"
                strSql &= " isnull(phone,'') as phone,isnull(mobile,'') as mobile,isnull(register,'') as register,isnull(address,'') as address,isnull(worktype,'') as worktype,isnull(workshop,'') as workshop,isnull(wgroup,'') as wgroup,isnull(introducer,'') as introducer,isnull(woff,'') as woff,isnull(contracttype,'') as contracttype,isnull(kind,'') as kind,"
                strSql &= " isnull(insuretype,'') as insuretype,isnull(insureitems,'') as insureitems,isnull(membership,'') as membership,isnull(married,'') as married,isnull(memo,'') as memo,isok From bbbbb where isok='0' "
                'Response.Write(strSql)
                'Exit Sub
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
                While reader.Read
                    'Dim enterdate As String


                    Dim department As String = ""
                    If reader("department").trim() <> "" Then
                        query = " Select departmentID From departments where name=N'" & reader("department") & "' "
                        department = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, query)
                    End If

                    Dim wid As String = ""
                    If reader("workshop").trim() <> "" Then
                        query = " Select id From Workshop where name=N'" & reader("workshop").trim() & "' and departmentID='" & department & "' "
                        wid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, query)
                    End If


                    'Dim pid As String = ""
                    'If reader("register") <> "" Then
                    '    query = "select systemCodeID From tcpc0.dbo.systemCode where systemCodeTypeID='6' and systemCodeName=N'" & reader("register").substring(0, 2) & "'"
                    '    pid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, query)
                    'End If

                    Dim did As String = ""
                    If reader("wgroup").trim() <> "" Then
                        query = "Select id From Workshop where name=N'" & reader("wgroup").trim() & "' and departmentID='" & department & "' "
                        did = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, query)

                    End If

                    strSql = " update bbbbb set isok='1' where code='" & reader("code") & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
                End While
                reader.Close()
            Next
            Dim myEnumerator As IDictionaryEnumerator = roletype.GetEnumerator()
            While myEnumerator.MoveNext()
                Response.Write(myEnumerator.Key & "/" & myEnumerator.Value & "<br>")
            End While
        End Sub


    End Class

End Namespace
