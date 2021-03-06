Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc


Namespace tcpc

    Partial Class wo_group_detail
        Inherits BasePage
        Dim chk As New adamClass
        Dim item As ListItem
        Dim strSQL As String
        Dim ds As DataSet
        Dim reader As SqlDataReader
        'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal

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
            If Not IsPostBack Then

                Dim ls As ListItem
                Select Case Session("PlantCode")
                    Case "1"
                        ls = New ListItem
                        ls.Value = "1000"
                        ls.Text = "1000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2100"
                        ls.Text = "2100"
                        dd_site.Items.Add(ls)
                    Case "2"
                        ls = New ListItem
                        ls.Value = "2000"
                        ls.Text = "2000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1200"
                        ls.Text = "1200"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "3000"
                        ls.Text = "3000"
                        dd_site.Items.Add(ls)
                    Case "5"
                        ls = New ListItem
                        ls.Value = "4000"
                        ls.Text = "4000"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "1400"
                        ls.Text = "1400"
                        dd_site.Items.Add(ls)
                        ls = New ListItem
                        ls.Value = "2400"
                        ls.Text = "2400"
                        dd_site.Items.Add(ls)
                End Select


                loadCostCenter()


                item = New ListItem("--")
                item.Value = 0
                dd_dept.Items.Add(item)
                strSQL = "SELECT departmentID,name From departments where issalary=1 order by name"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            dd_dept.Items.Add(item)
                        Next
                    End If
                End With
                ds.Reset()
                dd_dept.SelectedIndex = 0

                item = New ListItem("--")
                item.Value = 0
                dropWorkshop.Items.Add(item)

                item = New ListItem("--")
                item.Value = 0
                dropWorkType.Items.Add(item)

                loadGroup()

                BindData()
            End If
        End Sub
        Function loadCostCenter() As Boolean
            While dd_cc.Items.Count > 0
                dd_cc.Items.RemoveAt(0)
            End While

            Dim dm As String = ""
            If dd_site.SelectedValue = "1000" Or dd_site.SelectedValue = "1200" Or dd_site.SelectedValue = "1400" Then
                dm = "szx"
            ElseIf dd_site.SelectedValue = "2000" Or dd_site.SelectedValue = "2100" Or dd_site.SelectedValue = "2400" Then
                dm = "zql"
            ElseIf dd_site.SelectedValue = "3000" Then
                dm = "zqz"
            ElseIf dd_site.SelectedValue = "4000" Then
                dm = "yql"
            ElseIf dd_site.SelectedValue = "5000" Then
                dm = "tcb"
            ElseIf dd_site.SelectedValue = "6000" Then
                dm = "ytc"
            ElseIf dd_site.SelectedValue = "7000" Then
                dm = "zf"
            ElseIf dd_site.SelectedValue = "8000" Then
                dm = "zjn"
            ElseIf dd_site.SelectedValue = "9000" Then
                dm = "thk"
            ElseIf dd_site.SelectedValue = "a000" Then
                dm = "jql"
            ElseIf dd_site.SelectedValue = "b000" Then
                dm = "sf"
            ElseIf dd_site.SelectedValue = "c000" Then
                dm = "hql"
            End If


            item = New ListItem("--")
            item.Value = 0
            dd_cc.Items.Add(item)
            dd_cc.SelectedIndex = 0

            Dim conn As OdbcConnection = Nothing
            Dim comm As OdbcCommand = Nothing
            Dim dr As OdbcDataReader

            Dim connectionString As String = ConfigurationSettings.AppSettings("SqlConn.Conn9")
            ''DSN=MFGPRO;UID=ZXDZ;HOST=10.3.0.75;PORT=60013;DB=mfgtrain;PWD=zxdz;

            Dim sql As String = "Select cc_ctr,cc_desc from PUB.cc_mstr where cc_domain='" & dm & "'and cc_active=1 order by cc_ctr"
            Try
                conn = New OdbcConnection(connectionString)
                conn.Open()
                comm = New OdbcCommand(sql, conn)
                dr = comm.ExecuteReader()
                While (dr.Read())
                    If dr.GetValue(0).ToString() <> "" Then
                        If Session("uRole") = 1 Then
                            item = New ListItem(dr.GetValue(0).ToString() & " " & dr.GetValue(1).ToString())
                            item.Value = dr.GetValue(0).ToString()
                            dd_cc.Items.Add(item)
                        Else
                            strSQL = " select perm_id from tcpc0.dbo.wo_cc_permission where perm_ccid='" & dr.GetValue(0) & "' and perm_userid='" & Session("uID") & "'"
                            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL) > 0 Then
                                item = New ListItem(dr.GetValue(0).ToString() & " " & dr.GetValue(1).ToString())
                                item.Value = dr.GetValue(0).ToString()
                                dd_cc.Items.Add(item)
                            End If
                        End If
                    End If
                End While
                dr.Close()
                conn.Close()

            Catch oe As OdbcException
                'Response.Write(oe.Message)
            Finally
                If conn.State = System.Data.ConnectionState.Open Then
                    conn.Close()
                End If
                If Not comm Is Nothing Then
                    comm.Dispose()
                End If
                If Not conn Is Nothing Then
                    conn.Dispose()
                End If
            End Try
        End Function
        Function loadGroup() As Boolean
            While dd_group.Items.Count > 0
                dd_group.Items.RemoveAt(0)
            End While

            If dd_cc.SelectedIndex = 0 Then
                Exit Function
            End If

            strSQL = "SELECT wog_id,wog_name From tcpc0.dbo.wo_group Where wog_site='" & dd_site.SelectedValue & "' and wog_cc='" & dd_cc.SelectedValue & "' and deletedBy is null Order by wog_name "
            'Response.Write(strSQL)
            'Exit Function

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = .Rows(i).Item(0)
                        dd_group.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()
        End Function

        Private Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
            If RadioButtonList1.SelectedIndex = 0 Then
                Dim i As Integer
                For i = 0 To CheckBoxList1.Items.Count - 1
                    CheckBoxList1.Items(i).Selected = True
                Next
            Else
                If RadioButtonList1.SelectedIndex = 1 Then
                    Dim i As Integer
                    For i = 0 To CheckBoxList1.Items.Count - 1
                        CheckBoxList1.Items(i).Selected = False
                    Next
                End If
            End If
        End Sub

        'Add by Simon Oct 30, 2009
        Function LoadWorkshop() As Boolean

            dropWorkshop.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            dropWorkshop.Items.Add(item)

            If dd_dept.SelectedIndex <> 0 Then
                strSQL = " SELECT id,name FROM Workshop WHERE departmentID =  " & dd_dept.SelectedValue & " ORDER BY name "
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                While reader.Read()
                    item = New ListItem(reader(1))
                    item.Value = reader(0)
                    dropWorkshop.Items.Add(item)
                End While
                reader.Close()
            End If

        End Function


        Function LoadWorktype() As Boolean
            dropWorkType.Items.Clear()
            item = New ListItem("--")
            item.Value = 0
            dropWorkType.Items.Add(item)

            If dropWorkshop.SelectedIndex <> 0 Then
                strSQL = "SELECT id,name FROM Workshop WHERE workshopID = " & dropWorkshop.SelectedValue & " ORDER BY name "
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                While reader.Read()
                    item = New ListItem(reader(1))
                    item.Value = reader(0)
                    dropWorkType.Items.Add(item)
                End While
                reader.Close()
            End If

        End Function




        Private Sub BindData()
            CheckBoxList1.Items.Clear()
            If dd_group.Items.Count = 0 Then
                Exit Sub
            End If

            strSQL = " SELECT u.userID,u.userNo,u.userName,d.wod_id From tcpc0.dbo.users u left outer join tcpc0.dbo.wo_group_detail d on d.wod_user_id=u.userID and d.wod_site='" & dd_site.SelectedValue & "' and d.wod_cc='" & dd_cc.SelectedValue & "' and wod_group_id='" & dd_group.SelectedValue & "' Where u.plantCode='" & Session("PlantCode") & "' and u.leaveDate is null and u.deleted=0 and u.isactive=1  and u.organizationID=" & Session("orgID")
            If dd_dept.SelectedIndex <> 0 Then
                strSQL &= " AND u.departmentID=" & dd_dept.SelectedValue
            End If
            If dropWorkshop.SelectedIndex <> 0 Then
                strSQL &= " AND u.workshopID =" & dropWorkshop.SelectedValue
            End If
            If dropWorkType.SelectedIndex <> 0 Then
                strSQL &= " AND u.workprocedureID =" & dropWorkType.SelectedValue
            End If

            strSQL &= " Order by u.userNo "

            Dim total As Integer = 0

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(2) & "--" & .Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        CheckBoxList1.Items.Add(item)
                        If Not .Rows(i).IsNull(3) Then
                            CheckBoxList1.Items(i).Selected = True
                            total = total + 1
                        End If
                    Next i
                End If
            End With
            ds.Reset()
            lbl_qty.Text = "人数： " & total.ToString()

            strSQL = "SELECT count(wod_id) From tcpc0.dbo.wo_group_detail where wod_site='" & dd_site.SelectedValue & "' and wod_cc='" & dd_cc.SelectedValue & "' and wod_group_id='" & dd_group.SelectedValue & "'"
            lbl_all.Text = "人数： " & SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSQL).ToString()


        End Sub
        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            If dd_group.Items.Count = 0 Then
                Exit Sub
            End If

            Dim i As Integer
            For i = 0 To CheckBoxList1.Items.Count - 1
                strSQL = "SELECT wod_user_id From tcpc0.dbo.wo_group_detail Where wod_site='" & dd_site.SelectedItem.Value & "' and wod_cc='" & dd_cc.SelectedValue & "' and wod_group_id='" & dd_group.SelectedValue & "' and wod_user_id=" & CheckBoxList1.Items(i).Value
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                If ds.Tables(0).Rows.Count > 0 Then
                    If CheckBoxList1.Items(i).Selected = False Then
                        strSQL = "Delete From tcpc0.dbo.wo_group_detail Where wod_site='" & dd_site.SelectedItem.Value & "' and wod_cc='" & dd_cc.SelectedValue & "' and wod_group_id='" & dd_group.SelectedValue & "' and wod_user_id=" & CheckBoxList1.Items(i).Value
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                    End If
                Else
                    If CheckBoxList1.Items(i).Selected = True Then
                        Dim str As String = CheckBoxList1.Items(i).Text
                        strSQL = "Insert Into tcpc0.dbo.wo_group_detail(wod_site,wod_cc,wod_group_id,wod_group_name,wod_user_id ,wod_user_no,wod_user_name,createdDate,createdBy) "
                        strSQL &= " Values('" & dd_site.SelectedItem.Value & "','" & dd_cc.SelectedItem.Value & "','" & dd_group.SelectedItem.Value & "',N'" & dd_group.SelectedItem.Text & "','"
                        strSQL &= CheckBoxList1.Items(i).Value & "','" & str.Substring(str.IndexOf("--") + 2) & "',N'" & str.Substring(0, str.IndexOf("--")) & "',getdate(),'" & Session("uID") & "')"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                    End If
                End If
                ds.Reset()
            Next

            ltlAlert.Text = "alert('    保存成功！    ');"
        End Sub

        Protected Sub dd_site_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_site.SelectedIndexChanged
            loadCostCenter()

            loadGroup()

            lbl_qty.Text = ""
            lbl_all.Text = ""

            BindData()
        End Sub

        Protected Sub dd_cc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_cc.SelectedIndexChanged
            loadGroup()

            lbl_qty.Text = ""
            lbl_all.Text = ""

            BindData()

        End Sub

        Protected Sub dd_dept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_dept.SelectedIndexChanged
            LoadWorkshop()

            RadioButtonList1.SelectedIndex = -1

            BindData()

        End Sub

    
        Protected Sub dd_group_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dd_group.SelectedIndexChanged
            lbl_qty.Text = ""
            lbl_all.Text = ""

            BindData()

        End Sub

        Protected Sub dropWorkshop_SelectChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropWorkshop.SelectedIndexChanged
            LoadWorktype()
            RadioButtonList1.SelectedIndex = -1
            BindData()
        End Sub

        Protected Sub dropWorkType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropWorkType.SelectedIndexChanged
            RadioButtonList1.SelectedIndex = -1
            BindData()
        End Sub
    End Class

End Namespace

