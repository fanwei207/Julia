Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
    Partial Class access
        Inherits BasePage
        Dim chk As New adamClass
        Dim item As ListItem
        Dim strSQL As String
        'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal
        Dim ds As DataSet
        Public scrollPos As Integer = 0
        Public scrollPosL As Integer = 0

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
                
                If Request("uid") <> "" Then
                    Button1.Visible = True
                Else
                    Button1.Visible = False
                End If
                role.SelectedIndex = 0
                item = New ListItem("--")
                item.Value = 0
                role.Items.Add(item)
                strSQL = "SELECT departmentID,name From departments where issalary=1 order by name"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            role.Items.Add(item)
                        Next
                        role.SelectedIndex = 1
                    End If
                End With
                ds.Reset()

                loadUser()
                loadMenuRoot()
                BindData()
            End If

            scrollPos = Request("pos")
            scrollPosL = Request("posL")
            If (scrollPos = Nothing) Then
                scrollPos = 0
            End If
            If (scrollPosL = Nothing) Then
                scrollPosL = 0
            End If

            Dim saveScrollPosition As String
            saveScrollPosition = "<script language='javascript'>"
            saveScrollPosition = saveScrollPosition & "document.getElementById('Panel1').onscroll=saveScrollPosition1;"
            saveScrollPosition = saveScrollPosition & "</script>"

            Dim cstype As Type = Me.[GetType]()
            ClientScript.RegisterStartupScript(cstype, "saveScroll", saveScrollPosition)
            'RegisterStartupScript("saveScroll", saveScrollPosition)
        End Sub

        Sub loadUser()
            While DropDownList1.Items.Count > 0
                DropDownList1.Items.RemoveAt(0)
            End While

            If role.SelectedIndex = 0 Then
                Exit Sub
            End If

            dropMenuRoot.SelectedIndex = -1

            Dim id As Integer = 0
            If Request("uid") <> Nothing Then
                id = Request("uid")
                role.Enabled = False
                role.SelectedIndex = 0
            End If

            DropDownList1.SelectedIndex = -1
            'If Request("uid") = Nothing Then
            item = New ListItem("--")
            item.Value = 0
            DropDownList1.Items.Add(item)
            'End If
            If id > 0 Then
                strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and leaveDate is null and deleted=0 and leavedate is null and isactive=1 and roleID<>1 And userID=" & id & " and organizationID=" & Session("orgID") & " Order by RTRIM(LTRIM(userno))  "
            Else
                strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and leaveDate is null and deleted=0 and leavedate is null and isactive=1 and roleID<>1 And departmentID=" & role.SelectedValue & " and organizationID=" & Session("orgID") & " Order by RTRIM(LTRIM(userno))  "
            End If
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1) & "-" & .Rows(i).Item("userno"))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        DropDownList1.Items.Add(item)
                        If id = item.Value Then
                            If Request("uid") = Nothing Then
                                DropDownList1.SelectedIndex = i + 1
                            Else
                                DropDownList1.SelectedIndex = i
                            End If
                        End If
                    Next
                End If
            End With
            ds.Reset()

            CheckBoxList1.Items.Clear()
            Label1.Text = ""

            If DropDownList1.Items.Count = 0 Then
                Exit Sub
            End If

            'If DropDownList1.SelectedIndex > 0 Then
            strSQL = "SELECT m.id,m.name,a.userID,m.description,m.isMenu From tcpc0.dbo.Menu m Left Outer Join tcpc0.dbo.AccessRule a On m.id=a.moduleID and a.userID=" & DropDownList1.SelectedItem.Value & " where m.sortOrder is not null and m.url<>'null' Order by m.sortOrder "
            'Response.Write(strSQL)
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Item(4) = True Then
                            'item = New ListItem(.Rows(i).Item(1) & ":&nbsp;&nbsp;&nbsp;&nbsp;<font color=purple>" & .Rows(i).Item(3) & "</font>")
                            item = New ListItem(.Rows(i).Item(1))
                        Else
                            'item = New ListItem(" -- " & .Rows(i).Item(1) & ":&nbsp;&nbsp;&nbsp;&nbsp;<font color=purple>" & .Rows(i).Item(3) & "</font>")
                            item = New ListItem(" -- " & .Rows(i).Item(1))
                        End If
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        CheckBoxList1.Items.Add(item)
                    Next i
                End If
            End With
            ds.Reset()

            If id > 0 Then
                DropDownList1.SelectedIndex = 1
                DropDownList1.Enabled = False
            End If
        End Sub

        Function loadMenuRoot()
            dropMenuRoot.SelectedIndex = -1
            'If Request("uid") = Nothing Then
            item = New ListItem("--")
            item.Value = 0
            dropMenuRoot.Items.Add(item)
            strSQL = "SELECT id,name,description,isMenu From tcpc0.dbo.Menu where parentID = 0 and sortOrder is not null and isDisable = 0 Order by sortOrder "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item("name") & "--" & .Rows(i).Item("description"))
                        item.Value = Convert.ToInt32(.Rows(i).Item("id"))
                        dropMenuRoot.Items.Add(item)
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

        Private Sub BindData()
            If DropDownList1.Items.Count = 0 Or DropDownList1.SelectedValue = 0 Then
                Exit Sub
            End If

            Dim total As Integer = 0
            Dim totalSel As Integer = 0
            Dim i As Integer
            Dim j As Integer
          
            strSQL = "SELECT a.userID,a.moduleID From tcpc0.dbo.AccessRule a Left Outer Join tcpc0.dbo.Menu m  On m.id=a.moduleID and a.userID=" & DropDownList1.SelectedItem.Value & " where m.sortOrder is not null and m.url<>'null' Order by m.sortOrder "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            For j = 0 To CheckBoxList1.Items.Count - 1
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If CheckBoxList1.Items(j).Value = ds.Tables(0).Rows(i).Item("moduleID") Then

                        CheckBoxList1.Items(j).Selected = True
                        totalSel = totalSel + 1
                        Exit For
                    End If
                Next i
            Next j
         
            ds.Reset()

            Label1.Text = totalSel.ToString() & " / " & CheckBoxList1.Items.Count
        End Sub

        Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
            RadioButtonList1.SelectedIndex = -1
            If DropDownList1.Items.Count = 0 Or DropDownList1.SelectedIndex = 0 Then
                Dim i As Integer
                For i = 0 To CheckBoxList1.Items.Count - 1
                    CheckBoxList1.Items(i).Selected = False
                Next
            End If
            BindData()
        End Sub

        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            If DropDownList1.Items.Count = 0 Or DropDownList1.SelectedIndex = 0 Then
                Exit Sub
            End If

            Dim str As String
            If dropMenuRoot.SelectedIndex > 0 Then
                str = " With Menu2(id,name,parentID) As " _
                     & " ( select id,name,parentID from tcpc0.dbo.Menu where parentID = " & dropMenuRoot.SelectedItem.Value & "Union all " _
                     & " select  M.id,M.name,M.parentID  from tcpc0.dbo.Menu M Inner Join Menu2 On Menu2.id = M.parentID) " _
                     & " select * into #MenuTemp from Menu2 " _
                     & " Delete From tcpc0.dbo.AccessRule " _
                     & " Where userID = " & DropDownList1.SelectedItem.Value & " And Exists (select id from #MenuTemp Mt where moduleID = Mt.id )" _
                     & " drop table  #MenuTemp"
            Else

                str = "Delete From tcpc0.dbo.AccessRule Where userID=" & DropDownList1.SelectedItem.Value
            End If

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str)

            Dim i As Integer
            Dim ret As Integer
            For i = CheckBoxList1.Items.Count - 1 To 0 Step -1
                ret = CheckBoxList1.Items(i).Value
                While (ret > 0) 'repeat untill to root
                    'userid, moduleid,ischecked 
                    ret = Nodes(DropDownList1.SelectedItem.Value, ret, CheckBoxList1.Items(i).Selected)
                    'Response.Write(ret & ",")
                End While
            Next

            'SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.StoredProcedure, "sp_hr_UpdateAccessPublicModule")

            ltlAlert.Text = "alert('    保存成功！    ');"

            BindData()
        End Sub

        Private Sub role_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles role.SelectedIndexChanged
            loadUser()
            'BindData()
        End Sub

        Function Nodes(ByVal ur As String, ByVal md As String, ByVal isChecked As Boolean) As Integer
            Dim i As Integer
            Dim str As String
            Dim reader As SqlDataReader

            Nodes = 0
            strSQL = "SELECT isnull(a.userID,0),m.id,m.parentID From tcpc0.dbo.Menu m Left Outer Join tcpc0.dbo.AccessRule a On m.id=a.moduleID and a.userID=" & ur & " and a.moduleID=" & md & " where m.id=" & md
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    'Response.Write(.Rows(i).Item(0) & "---" & .Rows(i).Item(1) & "---" & .Rows(i).Item(2) & "<br>")
                    If .Rows(i).Item(0) = 0 Then
                        If isChecked = True Then
                            str = "Insert Into tcpc0.dbo.AccessRule(userID,moduleID,createdBy,createdDate) Values(" & ur & "," & md & "," & Session("uID") & ",getdate())"
                            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str)
                        End If
                        'Else
                        '    If isChecked = False Then
                        '        'str = "Select a.moduleID from tcpc0.dbo.menu m Inner Join tcpc0.dbo.AccessRule a On a.moduleID=m.id Where m.parentID=" & md
                        '        'reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, str)
                        '        'If Not (reader.Read()) Then
                        '        str = "Delete From tcpc0.dbo.AccessRule Where userID=" & ur & " and moduleID=" & md
                        '        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str)
                        '        'End If
                        '        ''reader.Close()
                        '    End If
                    End If
                    Nodes = .Rows(i).Item(2)
                End If
            End With
            ds.Reset()
            Return Nodes
        End Function
        Protected Sub dropMenuRoot_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropMenuRoot.SelectedIndexChanged
            CheckBoxList1.Items.Clear()
            LoadMenu(Convert.ToInt32(dropMenuRoot.SelectedValue), 0)
            BindData()
        End Sub
        Function LoadMenu(ByVal strPar As Integer, ByVal ll As Integer)
            Dim strA As String
            strA = ""
            ll = ll + 1
            Dim j As Integer
            For j = 1 To ll - 1
                strA = "---" + strA
            Next j
            strSQL = "SELECT m.id,m.name,m.description,m.isMenu,m.url, m.isPublic From tcpc0.dbo.Menu m where parentID = '" & strPar & "' and m.isDisable=0 and m.sortOrder is not null Order by m.sortOrder "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Item(4).ToString().Trim() <> "null" And Convert.ToBoolean(.Rows(i).Item(5)) <> True Then
                            item = New ListItem(strA & .Rows(i).Item(1).ToString() & " -- " & .Rows(i).Item("description"))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            CheckBoxList1.Items.Add(item)
                        End If
                        LoadMenu(Convert.ToInt32(.Rows(i).Item(0)), ll)
                    Next i
                End If
            End With
            ds.Reset()
        End Function

        Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            Response.Redirect(chk.urlRand("/admin/personnellist1.aspx"))
        End Sub
    End Class

End Namespace

