Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc

    Partial Class doc_manager_admin
        Inherits BasePage 
        Dim chk As New adamClass
        Dim item As ListItem
        Dim strSQL As String
        Dim ds As DataSet
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

            ltlAlert.Text = ""
            If Not IsPostBack Then  
                strSQL = " Select plantID, description From tcpc0.dbo.plants Where plantID<100 Order By plantid"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            ddlCompany.Items.Add(item)
                        Next
                        ddlCompany.SelectedValue = Session("PlantCode")
                    End If
                End With
                ds.Reset()

                LoadDepartment() 
                LoadDocType()

                If Request("cateid") <> Nothing Then
                    ddlCategory.SelectedValue = Request("cateid")
                Else
                    Response.Redirect("/qaddoc/qad_typelist.aspx", True)
                End If

                If Request("uID") = Nothing Then
                    Response.Redirect("/qaddoc/qad_typelist.aspx", True)
                ElseIf Request("uID").ToString().Trim() <> Convert.ToString(Session("uID")) Then
                    Response.Redirect("/qaddoc/qad_typelist.aspx", True)
                End If

                BindData()
            End If
        End Sub

        Sub LoadDocType()
            Dim ls As ListItem
            Dim reader As SqlDataReader
            strSQL = " Select typeid, typename From qaddoc.dbo.DocumentType Where isDeleted Is Null " 
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
            While (reader.Read())
                ls = New ListItem
                ls.Value = reader(0)
                ls.Text = reader(1).ToString.Trim

                ddlCategory.Items.Add(ls)
            End While
            reader.Close()
        End Sub

        Sub LoadDepartment()
            item = New ListItem("--")
            item.Value = 0
            dept.Items.Add(item)
            strSQL = " Select departmentID, name From tcpc" & ddlCompany.SelectedValue & ".dbo.departments Where issalary=1 Order By name"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        dept.Items.Add(item)
                    Next
                End If
            End With
            ds.Reset()
            dept.SelectedIndex = 0
        End Sub

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
            Dim i As Integer

            CheckBoxList1.Items.Clear()

            If dept.SelectedIndex = 0 Then
                strSQL = " Select Distinct u.userID, u.userName, m.doc_acc_catid, u.userNo " _
                       & " From tcpc0.dbo.users u " _
                       & " Inner Join qaddoc.dbo.DocumentAccess m On u.userID=m.doc_acc_userid And m.doc_acc_level = 0 And m.doc_acc_catid=" & ddlCategory.SelectedValue _
                       & " Where u.leavedate Is Null And u.deleted=0 And u.isactive=1 And u.organizationID=" & Session("orgID") _
                       & " Order by u.UserName "
            Else
                strSQL = " Select Distinct u.userID, u.userName, m.doc_acc_catid, u.userNo " _
                       & " From tcpc0.dbo.users u " _
                       & " Inner Join qaddoc.dbo.DocumentAccess m On u.userID=m.doc_acc_userid And m.doc_acc_level = 0 And m.doc_acc_catid=" & ddlCategory.SelectedValue _
                       & " Where u.leavedate Is Null And u.deleted=0 And u.isactive=1 And u.organizationID=" & Session("orgID") _
                       & " Union " _
                       & " Select Distinct u.userID, u.userName, m.doc_acc_catid, u.userNo " _
                       & " From tcpc0.dbo.users u " _
                       & " Left Outer Join qaddoc.dbo.DocumentAccess m On u.userID=m.doc_acc_userid And m.doc_acc_level = 0 And m.doc_acc_catid=" & ddlCategory.SelectedValue _
                       & " Where u.departmentID=" & dept.SelectedValue & " And u.plantCode = " & ddlCompany.SelectedValue _
                       & " And u.leavedate Is Null And u.deleted=0 And u.isactive=1 And u.organizationID=" & Session("orgID") _
                       & " Order by u.UserName "
            End If

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1) & "  --  工号: " & .Rows(i).Item(3))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        CheckBoxList1.Items.Add(item)
                        If Not .Rows(i).IsNull(2) Then
                            CheckBoxList1.Items(i).Selected = True
                        End If
                    Next i
                End If
            End With
            ds.Reset()

            Dim j As Integer = 0
            For i = 0 To CheckBoxList1.Items.Count - 1
                If CheckBoxList1.Items(i).Selected = True Then
                    j = j + 1
                End If
            Next
            Label1.Text = "   Administrator: " & j.ToString
        End Sub
        
        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            Dim i As Integer
            For i = 0 To CheckBoxList1.Items.Count - 1   
                If CheckBoxList1.Items(i).Selected = False Then 
                    strSQL = " Delete From qaddoc.dbo.DocumentAccess " _
                           & " Where doc_acc_catid=" & ddlCategory.SelectedItem.Value & " And doc_acc_userid=" & CheckBoxList1.Items(i).Value _
                           & " And doc_acc_level = 0 "
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL) 
                Else
                    If CheckBoxList1.Items(i).Selected = True Then
                        strSQL = " If Not Exists(Select * From qaddoc.dbo.DocumentAccess Where doc_acc_catid = '" & ddlCategory.SelectedItem.Value _
                               & "' And doc_acc_userid='" & CheckBoxList1.Items(i).Value & "') " _
                               & " Begin " _
                               & " Insert Into qaddoc.dbo.DocumentAccess(doc_acc_catid, doc_acc_catname, doc_acc_userid, doc_acc_username, doc_acc_level, " _
                               & "      createdBy, createdDate, approvedBy, approvedDate) " _
                               & " Values(" & ddlCategory.SelectedItem.Value & ", N'" & ddlCategory.SelectedItem.Text & "', " & CheckBoxList1.Items(i).Value _
                               & ", N'" & CheckBoxList1.Items(i).Text.Substring(0, CheckBoxList1.Items(i).Text.IndexOf("--")) & "', 0, '" & Session("uID") _
                               & "', getdate(), '" & Session("uID") & "', getdate()) " _
                               & " End " _
                               & " Else " _
                               & " Begin " _
                               & " Update qaddoc.dbo.DocumentAccess " _
                               & " Set doc_acc_level = 0, createdBy = '" & Session("uID") & "', createdDate = getdate(), approvedBy = '" & Session("uID") _
                               & "', approvedDate = getdate() " _
                               & " Where doc_acc_catid = '" & ddlCategory.SelectedItem.Value & "' And doc_acc_userid='" & CheckBoxList1.Items(i).Value & "'" _
                               & " End "
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                    End If
                End If
                'ds.Reset()
            Next

            BindData()

            ltlAlert.Text = "alert('    Save completed.    ');"
        End Sub

        Private Sub dept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dept.SelectedIndexChanged
            BindData()
        End Sub

        Private Sub ddlCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCompany.SelectedIndexChanged
            dept.Items.Clear()
            LoadDepartment()
            BindData()
        End Sub

        Protected Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
            Response.Redirect("/qaddoc/qad_typelist.aspx", True)
        End Sub
    End Class

End Namespace

