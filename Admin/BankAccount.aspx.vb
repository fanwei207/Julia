Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Data.OleDb


Namespace tcpc

    Partial Class BankAccount
        Inherits BasePage
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Protected strTempBank As String
        Shared type As String = "-1"


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
            If Not IsPostBack() Then
               
                BankTypeDropDownList()
                BindData()
            End If
        End Sub

        Sub BankTypeDropDownList()
            Dim item As ListItem
            Dim query As String
            Dim reader As SqlDataReader


            item = New ListItem("--")
            item.Value = 0
            BankType.Items.Add(item)
            query = " SELECT id, name FROM Bank WHERE organizationID='" & Session("orgID") & "'"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                BankType.Items.Add(item)
            End While
            reader.Close()
            BankType.SelectedIndex = 0
        End Sub
         
        Sub BindData()
            Dim query As String
            Dim dst As DataSet
            Dim i As Integer
            query = " SELECT u.userID, u.userNo, u.userName, b.name, u.bankAccountNo,isnull(d.name,''), isnull(w.name,''), u.enterdate, u.leavedate, u.bankAccountloc " _
                  & " FROM tcpc0.dbo.users u" _
                  & " LEFT OUTER JOIN Bank b ON u.bankID = b.id " _
                  & " left outer JOIN departments d on d.departmentID=u.departmentID " _
                  & " left outer JOIN  workshop w on w.id=u.workshopID " _
                  & " WHERE u.plantCode='" & Session("PlantCode") & "' and u.organizationID='" & Session("orgID") & "' AND u.roleID>1  "
            If leave.Checked Then
                query = query & " and u.leavedate is not null "
            Else
                query = query & " AND u.leavedate IS NULL"
            End If
            If txtUserNo.Text.Trim() <> "" Then
                query = query & " AND u.userNo ='" & txtUserNo.Text.Trim() & "'"
            End If
            If txtUserName.Text.Trim() <> "" Then
                query = query & " AND u.userName like N'%" & txtUserName.Text.Trim() & "%'"
            End If
            If BankType.SelectedValue <> 0 Then
                query = query & " AND b.id='" & BankType.SelectedValue & "'"
            End If
            query = query & " order by u.userID"

            'Session("EXTitle") = "<b>工号</b>~^<b>姓名</b>~^<b>开户银行</b>~^<b>银行帐号</b>~^<b>部门</b>~^<b>工段</b>~^<b>入公司日期</b>~^<b>离职日期</b>~^"
            'Session("EXSQL") = query
            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("UserNo", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("USerName", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("Bank", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("BankAccount", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("userID", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("EnterDate", System.Type.GetType("System.DateTime")))
            dtl.Columns.Add(New DataColumn("LeaveDate", System.Type.GetType("System.DateTime")))
            dtl.Columns.Add(New DataColumn("BankLoc", System.Type.GetType("System.String")))
            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("gsort") = i + 1
                        drow.Item("UserNo") = .Rows(i).Item("userNo").ToString().Trim()
                        drow.Item("UserName") = .Rows(i).Item("userName").ToString().Trim()
                        drow.Item("Bank") = .Rows(i).Item("name").ToString().Trim()
                        drow.Item("BankAccount") = .Rows(i).Item("bankAccountNo").ToString().Trim()
                        drow.Item("EnterDate") = .Rows(i).Item("EnterDate")
                        drow.Item("LeaveDate") = IIf(.Rows(i).Item("LeaveDate") Is Nothing, "", .Rows(i).Item("LeaveDate"))
                        drow.Item("userID") = .Rows(i).Item("userID").ToString().Trim()
                        drow.Item("BankLoc") = .Rows(i).Item("bankAccountloc").ToString().Trim()
                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            Dim dvw As DataView
            dvw = New DataView(dtl)

            Try
                dgBankAccount.DataSource = dvw
                dgBankAccount.DataBind()
            Catch
            End Try
        End Sub

        Private Sub dgBankAccount_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBankAccount.ItemCreated
            Select Case e.Item.ItemType
                Case ListItemType.Item

                Case ListItemType.AlternatingItem

                Case ListItemType.EditItem
                    CType(e.Item.Cells(6).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(200)
            End Select
        End Sub

        Public Sub Edit_edit(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBankAccount.EditCommand
            Dim str As String
            str = CType(e.Item.FindControl("lblBank"), Label).Text.Trim()
            banktypelistindexstore(str)
            dgBankAccount.EditItemIndex = e.Item.ItemIndex
            BindData()
        End Sub

        Public Sub Edit_cancel(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBankAccount.CancelCommand
            dgBankAccount.EditItemIndex = -1
            type = "-1"
            BindData()
        End Sub

        Public Sub Edit_update(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBankAccount.UpdateCommand
            Dim query, str As String
            Dim strbankID As String = CType(e.Item.FindControl("BankDropdownlist"), DropDownList).SelectedValue.Trim()
            Dim strAccountNo As String = CType(e.Item.Cells(6).Controls(0), TextBox).Text
            Dim strAccountLoc As String = CType(e.Item.Cells(7).Controls(0), TextBox).Text
            If strAccountNo.Trim().Length > 0 Then
                'Modified by baoxin 20071201
                If strbankID = "0" Then
                    ltlAlert.Text = "alert('银行没有选择！');"
                    Exit Sub
                Else
                    query = " SELECT bankAccountNo FROM tcpc0.dbo.users WHERE bankAccountNo=N'" & strAccountNo.Trim() & "' AND bankID='" & strbankID & "'  And leaveDate is null"
                    str = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, query)
                    If str <> Nothing Then
                        ltlAlert.Text = "alert('输入的账号已经存在，请检查后重新输入！');"
                        Exit Sub
                    ElseIf strAccountNo.Trim.Length > 30 Then
                        ltlAlert.Text = "alert('输入的账号长度超标，请检查后重新输入！');"
                        Exit Sub
                    Else
                        query = " UPDATE tcpc0.dbo.users SET BankID='" & strbankID & "'," _
                              & " bankAccountNo='" & strAccountNo.Trim() & "'," _
                              & " bankAccountloc=N'" & strAccountLoc.Trim() & "'" _
                              & " WHERE userID='" & e.Item.Cells(9).Text.Trim() & "'"

                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)
                        dgBankAccount.EditItemIndex = -1
                        BindData()
                    End If
                End If
            Else
                query = " UPDATE tcpc0.dbo.users SET"
                If strbankID = "0" Then
                    query &= " BankID=null ,"
                Else
                    query &= " BankID='" & strbankID & "' ,"
                End If
                query &= " bankAccountNo=null "
                query &= " WHERE userID='" & e.Item.Cells(9).Text.Trim() & "'"

                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, query)
                dgBankAccount.EditItemIndex = -1
                BindData()
            End If
            type = "-1"
        End Sub

        Private Sub BtnShowAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShowAll.Click
            dgBankAccount.CurrentPageIndex = 0
            BindData()
        End Sub

        Private Function banktypelistindexstore(ByVal str As String)
            Dim query As String
            Dim ds As DataSet
            query = " SELECT id, name FROM Bank WHERE organizationID='" & Session("orgID") & "'"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)
            Dim ii As Integer = 0
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If str = .Rows(i).Item(1).ToString().Trim() Then
                            type = ii
                        End If
                        ii = ii + 1
                    Next
                End If
            End With
        End Function

        Public Function BankListSource()
            Dim query As String
            Dim con As SqlConnection
            Dim cmd As SqlCommand

            query = " SELECT 0 as id,'--'as name "
            query &= " union "
            query &= " SELECT id, name FROM Bank WHERE organizationID='" & Session("orgID") & "' order by id "
            con = New SqlConnection(chk.dsnx)
            cmd = New SqlCommand(query, con)
            con.Open()
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
            con.Close()
        End Function

        Public Sub BankSetDDI(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim ed As System.Web.UI.WebControls.DropDownList
            ed = sender
            ed.SelectedIndex = ed.Items.IndexOf(ed.Items.FindByText(strTempBank))
            Dim str As String = type
            Dim i As Integer
            i = CInt(str)
            ed.SelectedIndex = CInt(i) + 1
        End Sub

        Private Sub txtUserName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserName.TextChanged
            BindData()
        End Sub

        Private Sub txtUserNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserNo.TextChanged
            BindData()
        End Sub

        Private Sub BankType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BankType.SelectedIndexChanged
            BindData()
        End Sub

        Private Sub dgBankAccount_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgBankAccount.PageIndexChanged
            dgBankAccount.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Public Sub uploadBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uploadBtn.ServerClick
            If (Session("uID") = Nothing) Then
                Exit Sub
            End If
            Dim strSQL As String
            Dim reader As SqlDataReader
            
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
                    Dim bankname As String
                    Dim bankcode As String
                    Dim bankloc As String
                    Dim y As String
                    'for basic information
 
                    Dim myDataset As DataTable = Me.GetExcelContents(strFileName)
                    Dim idhashtable As New Hashtable
                    idhashtable.Clear()
                    With myDataset

                        If .Rows.Count > 0 Then
                            For i = 0 To .Rows.Count - 1
                                Dim id As String = "0"
                                Dim bid As String = "0"
                                Dim account As String = "0"
                                If .Rows(i).IsNull(0) Then
                                    _code = ""
                                Else
                                    _code = .Rows(i).Item(0)
                                End If

                                If .Rows(i).IsNull(1) Then
                                    name = ""
                                Else
                                    name = .Rows(i).Item(1).ToString().Trim
                                    name = name.Replace(" ", "")
                                End If

                                strSQL = "select userID,isnull(bankAccountNo,0) from tcpc0.dbo.users where userNo=N'" & _code & "'and replace(username,' ','') like N'%" & name & "%'"
                                strSQL &= " and plantCode='" & Session("PlantCode") & "'"
                                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                                While reader.Read
                                    id = reader(0).ToString()
                                    account = reader(1)
                                End While
                                reader.Close()

                                If id = "0" Then

                                    ltlAlert.Text = "alert('工号或姓名错误(0) --行 " & (i + 2).ToString & "')"
                                    'Response.Write(strSQL)
                                    Dim myEnumerator As IDictionaryEnumerator = idhashtable.GetEnumerator()
                                    While myEnumerator.MoveNext()
                                        strSQL = " update tcpc0.dbo.users set bankID=null, bankAccountNo=null where userID='" & myEnumerator.Value & "'"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                    End While

                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If


                                'whether the bank is exist ------------------------------------------------
                                If .Rows(i).IsNull(2) Then
                                    bankname = ""
                                Else
                                    strSQL = "select isnull(id,0) from Bank where name=N'" & .Rows(i).Item(2) & "' "
                                    bankname = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)
                                End If
                                If bankname = "" Then
                                    ltlAlert.Text = "alert('银行名称有误(0) --行 " & (i + 2).ToString & "')"
                                    Dim myEnumerator As IDictionaryEnumerator = idhashtable.GetEnumerator()
                                    While myEnumerator.MoveNext()
                                        strSQL = " update tcpc0.dbo.users set bankID=null,bankAccountNo=null where userID='" & myEnumerator.Value & "'"
                                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                                    End While

                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                End If '---------------------------------------------------------------------

                                If .Rows(i).IsNull(3) Then
                                    bankcode = "0"
                                Else
                                    bankcode = .Rows(i).Item(3)
                                End If
                                If .Rows(i).IsNull(4) Then
                                    ltlAlert.Text = "alert('归属地错误(0) --行 " & (i + 2).ToString & "')"
                                    myDataset.Reset()
                                    File.Delete(strFileName)
                                    Return
                                Else
                                    bankloc = .Rows(i).Item(4)
                                End If

                                strSQL = " Select isnull(userID,0) From tcpc0.dbo.users where bankID='" & bankname & "' and bankAccountNo='" & bankcode & "' "
                                bid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL)

                                strSQL = " update tcpc0.dbo.users set bankID='" & bankname & "',bankAccountNo='" & bankcode & "',bankAccountloc=N'" & bankloc & "' where cast(userID as varchar)='" & id & "' "
                                SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

                                idhashtable.Add(i + 1, id.ToString)
                            Next
                        End If
                    End With
                    myDataset.Reset()
                    If (File.Exists(strFileName)) Then
                        File.Delete(strFileName)
                    End If
                    idhashtable.Clear()
                End If
            End If
        End Sub

        Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
            Dim query As String
            query = " SELECT u.userID, u.userNo, u.userName, b.name, u.bankAccountNo, isnull(bankAccountloc,''), isnull(d.name,''), isnull(w.name,''), u.enterdate, u.leavedate " _
                  & " FROM tcpc0.dbo.users u" _
                  & " LEFT OUTER JOIN Bank b ON u.bankID = b.id " _
                  & " left outer JOIN departments d on d.departmentID=u.departmentID " _
                  & " left outer JOIN  workshop w on w.id=u.workshopID " _
                  & " WHERE u.plantCode='" & Session("PlantCode") & "' and u.organizationID='" & Session("orgID") & "' AND u.roleID>1  "
            If leave.Checked Then
                query = query & " and u.leavedate is not null "
            Else
                query = query & " AND u.leavedate IS NULL"
            End If
            If txtUserNo.Text.Trim() <> "" Then
                query = query & " AND u.userNo ='" & txtUserNo.Text.Trim() & "'"
            End If
            If txtUserName.Text.Trim() <> "" Then
                query = query & " AND u.userName like N'%" & txtUserName.Text.Trim() & "%'"
            End If
            If BankType.SelectedValue <> 0 Then
                query = query & " AND b.id='" & BankType.SelectedValue & "'"
            End If
            query = query & " order by u.userID"

            Dim title As String = "<b>工号</b>~^<b>姓名</b>~^<b>开户银行</b>~^<b>银行帐号</b>~^<b>归属地</b>~^<b>部门</b>~^<b>工段</b>~^<b>入公司日期</b>~^<b>离职日期</b>~^"

            ExportExcel(chk.dsnx, title, query, False)

        End Sub
    End Class

End Namespace
