Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration
Partial Class computermanage_computerimport
    Inherits BasePage
    Dim nRet As Integer
    Dim strSql As String
    Dim ds As DataSet
    Public chk As New adamClass
    Dim reader As SqlDataReader
    'Protected WithEvents ltlAlert As Literal
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ltlAlert.Text = ""
        If Not IsPostBack Then  
            If Request("err") = "y" Then
                Session("EXTitle1") = "60^<b>ID</b>~^150^<b>类型</b>~^150^<b>资产编号</b>~^250^<b>所属部门</b>~^130^<b>工号</b>~^300^<b>CPU</b>~^60^<b>内存</b>~^150^<b>硬盘</b>~^150^<b>显示器</b>~^250^<b>键盘</b>~^130^<b>鼠标</b>~^300^<b>IP地址</b>~^60^<b>MAC地址</b>~^150^<b>操作系统</b>~^150^<b>是否连网</b>~^250^<b>领用日期</b>~^130^<b>归还日期</b>~^300^<b>描述</b>~^300^<b>错误原因</b>~^"
                Session("EXHeader1") = ""
                Session("EXSQL1") = " Select FNO,typeid,assetno,departmentid,userno,cpu,memory,harddisk,display,keyboard,mouse,ip,mac,os,internet,begindate,enddate,description,errcode From computerimport_temp Where createdby='" & Session("uID") & "' and (errcode is not null or errcode <> '') order by id"
                ltlAlert.Text = "window.open('/public/exportExcel1.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');"
            End If

            FileTypeDropDownList1.SelectedIndex = -1
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
        Dim IsError As Integer
        Dim strerr As String = ""

        strCatFolder = Server.MapPath("/computerimport")
        If Not Directory.Exists(strCatFolder) Then
            Try
                Directory.CreateDirectory(strCatFolder)
            Catch
                ltlAlert.Text = "alert('上传文件失败.')"
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
            If (filename1.PostedFile.ContentLength > 20971520) Then
                ltlAlert.Text = "alert('上传的文件最大为 20 MB.')"
                Return
            End If
            Try
                filename1.PostedFile.SaveAs(strFileName)
            Catch
                ltlAlert.Text = "alert('上传文件失败.')"
                Return
            End Try
        End If

        If (File.Exists(strFileName)) Then
            Dim _FNO As String
            Dim _typeid As String
            Dim _assetno As String
            Dim _departmentid As String
            Dim _userno As String
            Dim _cpu As String
            Dim _memory As String
            Dim _harddisk As String
            Dim _display As String
            Dim _keyboard As String
            Dim _mouse As String
            Dim _ip As String
            Dim _mac As String
            Dim _os As String
            Dim _internet As String
            Dim _begindate As String
            Dim _enddate As String
            Dim _description As String
            Dim myDataset As DataTable
            Dim paramData As SqlParameter

            Try
                myDataset = Me.GetExcelContents(strFileName)
            Catch
                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If
                ltlAlert.Text = "alert('导入文件必须是Excel格式.');"
                Exit Sub
            Finally
                If (File.Exists(strFileName)) Then
                    File.Delete(strFileName)
                End If
            End Try


            'Try

            With myDataset
                If .Rows.Count > 0 Then
                    If .Columns(0).ColumnName <> "ID" Or .Columns(1).ColumnName <> "类型" Or .Columns(2).ColumnName <> "资产编号" Or .Columns(3).ColumnName <> "所属部门" Or .Columns(4).ColumnName <> "工号" Then
                        myDataset.Reset()
                        ltlAlert.Text = "alert('导入文件不是部件目录导入模版.'); "
                        Exit Sub
                    End If
                    strSQL = " Delete From computerimport_temp Where createdby = '" & Session("uID") & "' "
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                    IsError = 0

                    For i = 0 To .Rows.Count - 1

                        If .Rows(i).IsNull(0) Then
                            _FNO = ""
                        Else
                            _FNO = .Rows(i).Item(0)
                        End If

                        If .Rows(i).IsNull(1) Then
                            _typeid = ""
                        Else
                            _typeid = .Rows(i).Item(1)
                            Dim SQL As String = " Select top 1 typeid  From computertype Where typename = N'" & _typeid & "' "
                            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, SQL)
                            If Not reader.HasRows Then
                                ltlAlert.Text = "alert('第" & i + 1 & "行的类型不存在，请重新导入.')"
                                Exit Sub
                            Else
                                While reader.Read
                                    _typeid = reader(0).ToString()
                                End While
                            End If
                        End If

                        If .Rows(i).IsNull(2) Then
                            _assetno = ""
                        Else
                            _assetno = .Rows(i).Item(2)
                            Dim SQL As String = " Select top 1 id  From computerregister Where assetno = N'" & _assetno & "' union Select top 1 id From computerimport_temp Where assetno = N'" & _assetno & "'"
                            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, SQL)
                            If reader.HasRows Then
                                ltlAlert.Text = "alert('第" & i + 1 & "行的资产编号有重复，请重新导入.')"
                                Exit Sub
                            Else
                                While reader.Read
                                    _departmentid = reader(0).ToString()
                                End While
                            End If
                        End If

                        If .Rows(i).IsNull(3) Then
                            _departmentid = ""
                        Else
                            _departmentid = .Rows(i).Item(3)
                            Dim SQL As String = " Select top 1 departmentID  From Departments Where name = N'" & _departmentid & "' "
                            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, SQL)
                            If Not reader.HasRows Then
                                ltlAlert.Text = "alert('第" & i + 1 & "行的部门不存在，请重新导入.')"
                                Exit Sub
                            Else
                                While reader.Read
                                    _departmentid = reader(0).ToString()
                                End While
                            End If
                        End If

                        If .Rows(i).IsNull(4) Then
                            _userno = ""
                        Else
                            _userno = .Rows(i).Item(4)
                            Dim SQL As String = " Select top 1 userID  From tcpc0.dbo.Users Where userNo = '" & _userno & "' and plantCode = " & Session("plantCode").ToString()
                            Dim reader As SqlDataReader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, SQL)
                            If Not reader.HasRows Then
                                ltlAlert.Text = "alert('第" & i + 1 & "行的工号不存在，请重新导入.')"
                                Exit Sub
                            End If

                        End If

                        If .Rows(i).IsNull(5) Then
                            _cpu = ""
                        Else
                            _cpu = .Rows(i).Item(5)
                        End If

                        If .Rows(i).IsNull(6) Then
                            _memory = ""
                        Else
                            _memory = .Rows(i).Item(6)
                        End If

                        If .Rows(i).IsNull(7) Then
                            _harddisk = ""
                        Else
                            _harddisk = .Rows(i).Item(7)
                        End If

                        If .Rows(i).IsNull(8) Then
                            _display = ""
                        Else
                            _display = .Rows(i).Item(8)
                        End If

                        If .Rows(i).IsNull(9) Then
                            _keyboard = ""
                        Else
                            _keyboard = .Rows(i).Item(9)
                        End If

                        If .Rows(i).IsNull(10) Then
                            _mouse = ""
                        Else
                            _mouse = .Rows(i).Item(10)
                        End If

                        If .Rows(i).IsNull(11) Then
                            _ip = ""
                        Else
                            _ip = .Rows(i).Item(11)
                        End If

                        If .Rows(i).IsNull(12) Then
                            _mac = ""
                        Else
                            _mac = .Rows(i).Item(12)
                        End If
                        If .Rows(i).IsNull(13) Then
                            _os = ""
                        Else
                            _os = .Rows(i).Item(13)
                        End If

                        If .Rows(i).IsNull(14) Then
                            _internet = ""
                        Else
                            _internet = .Rows(i).Item(14)
                        End If

                        If .Rows(i).IsNull(15) Then
                            _begindate = ""
                        Else
                            _begindate = .Rows(i).Item(15)
                        End If

                        If .Rows(i).IsNull(16) Then
                            _enddate = ""
                        Else
                            _enddate = .Rows(i).Item(16)
                        End If
                        If .Rows(i).IsNull(17) Then
                            _description = ""
                        Else
                            _description = .Rows(i).Item(17)
                        End If

                        If (_typeid <> "" And _departmentid <> "") Then
                            strSQL = "insert into computerimport_temp(typeid,assetno,departmentid,userno,cpu,memory,harddisk,display,keyboard,mouse,ip,mac,os,internet,begindate,enddate,description,createdby,FNO) values(N'" & _typeid & "',N'" & _assetno & "',N'" & _departmentid & "',N'" & _userno & "',N'" & _cpu & "',N'" & _memory & "',N'" & _harddisk & "',N'" & _display & "',N'" & _keyboard & "',N'" & _mouse & "',N'" & _ip & "',N'" & _mac & "',N'" & _os & "',N'" & _internet & "',N'" & _begindate & "',N'" & _enddate & "',N'" & _description & "','" & Session("uID") & "','" & _FNO & "') "
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL, paramData)
                        End If
                    Next
                End If
            End With
            myDataset.Reset()

            strSQL = "computerimport"
            Dim params(3) As SqlParameter
            params(0) = New SqlParameter("@uID", Session("uID"))
            params(1) = New SqlParameter("@iserr", IsError)
            params(2) = New SqlParameter("@plantCode", Session("plantCode").ToString())

            IsError = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params)

            If IsError >= 0 Then
                ltlAlert.Text = "alert('电脑信息导入成功.'); window.location.href='" & chk.urlRand("/computermanage/computerimport.aspx") & "';"
            Else
                ltlAlert.Text = "alert('电脑信息导入结束，有错误！'); window.location.href='" & chk.urlRand("/computermanage/computerimport.aspx?err=y") & "';"
            End If
            'Catch ex As Exception
            '   ltlAlert.Text = "alert('电脑信息导入失败！" & ex.Message & "');"
            '  Return
            ' End Try
        End If
    End Sub

End Class
