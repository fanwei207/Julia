'* @@ NAME				:	accessByInputting.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for accessByInputting.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	 November 21 2006
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web.UI.WebControls
Imports System


Namespace tcpc

Partial Class accessByInputting
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim chk As New adamClass
    Dim item As ListItem
    Dim strSQL As String

    'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Dim ds As DataSet
    Dim reader As SqlDataReader
    Public scrollPos As Integer = 0

    Public scrollPosL As Integer = 0


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

                role.SelectedIndex = 0
                item = New ListItem("--")
                item.Value = 0
                role.Items.Add(item)
                workshop.Items.Add(item)
                strSQL = "SELECT departmentID,name From departments where issalary=1 order by name"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
                While reader.Read
                    item = New ListItem(reader(1))
                    item.Value = Convert.ToInt32(reader(0))
                    role.Items.Add(item)
                    workshop.Items.Add(item)
                End While
                reader.Close()
                role.SelectedIndex = 0

                loadUser()

                'BindData()
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


    End Sub

        Sub loadUser()
            While DropDownList1.Items.Count > 0
                DropDownList1.Items.RemoveAt(0)
            End While

            item = New ListItem("--")
            item.Value = 0
            DropDownList1.Items.Add(item)

            strSQL = "SELECT u.userID,u.userName,u.userNO From tcpc0.dbo.users u Inner Join tcpc0.dbo.systemCode sc On sc.systemCodeID=u.workTypeID and sc.systemcodename like N'%计时%' Inner Join tcpc0.dbo.systemCodeType sct On sct.systemCodeTypeID=sc.systemCodeTypeID and sct.systemCodeTypeName='Work Type' Where u.plantCode='" & Session("PlantCode") & "' and u.deleted=0 and u.isactive=1 and u.roleID<>1 and u.organizationID=" & Session("orgID")
            If role.SelectedIndex > 0 Then
                strSQL &= " And u.departmentID=" & role.SelectedValue
            End If
            strSQL &= " Order by u.userNO  "
            'Response.Write(strSQL)
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While reader.Read
                item = New ListItem(reader(1) & "-" & reader(2))
                item.Value = Convert.ToInt32(reader(0))
                DropDownList1.Items.Add(item)
            End While
            reader.Close()
             
            CheckBoxList1.Items.Clear()
            Label1.Text = ""

            If DropDownList1.Items.Count = 0 Then
                Exit Sub
            End If

            'If DropDownList1.SelectedIndex > 0 Then
            strSQL = "SELECT w.id,d.departmentID,w.name,d.name From Workshop w Inner Join departments  d on d.departmentID=w.departmentID and d.isSalary=1 where w.workshopID is null order by d.departmentID,w.id "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            Dim dID As Integer = 0
            While reader.Read
                'If dID <> reader(1) Then
                '    dID = reader(1)
                '    item = New ListItem(reader(3))
                '    item.Value = 0
                '    CheckBoxList1.Items.Add(item)
                'End If
                item = New ListItem(reader(2) & "&nbsp;&nbsp;&nbsp;--&nbsp;&nbsp;&nbsp;" & reader(3))
                item.Value = Convert.ToInt32(reader(0))
                CheckBoxList1.Items.Add(item)
            End While
            reader.Close() 
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
        CheckBoxList1.Items.Clear()
        If DropDownList1.Items.Count = 0 Then
            Exit Sub
        End If

        Dim total As Integer = 0
        Dim totalSel As Integer = 0

        strSQL = "SELECT w.id,d.departmentID,w.name,d.name,a.userID From Workshop w Inner Join departments  d on d.departmentID=w.departmentID and d.isSalary=1 "
        strSQL &= " Left outer join  InputtingAccess a on a.workshopID=w.id "

        If DropDownList1.SelectedIndex > 0 Then
            strSQL &= " and a.userID='" & DropDownList1.SelectedValue & "' "
        End If
            'strSQL &= " where (w.workshopID Is Not null) "
            strSQL &= " where (w.workshopID IS NULL) "
        If workshop.SelectedIndex > 0 Then
            strSQL &= " and d.departmentID='" & workshop.SelectedValue & "' "
        End If
        strSQL &= " order by d.departmentID,w.id "

        'Response.Write(strSQL)
        Dim i As Integer = 0
        Dim dID As Integer = 0
      
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
        While reader.Read
            'If dID <> reader(1) Then
            '    dID = reader(1)
            '    i = i + 1
            '    ' CheckBoxList1.Items(i).Attributes("Enabled") = "False"
            'End If   '// department name id not including in it
            item = New ListItem(reader(2) & "&nbsp;&nbsp;&nbsp;--&nbsp;&nbsp;&nbsp;" & reader(3))
            item.Value = Convert.ToInt32(reader(0))
            CheckBoxList1.Items.Add(item)

            If IsDBNull(reader(4)) = False Then
                CheckBoxList1.Items(i).Selected = True
                totalSel = totalSel + 1
            End If
            total = total + 1
            i = i + 1
        End While
        reader.Close()

        Label1.Text = totalSel.ToString() & " / " & total.ToString()
    End Sub
    Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        RadioButtonList1.SelectedIndex = -1
        BindData()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If DropDownList1.Items.Count = 0 Then
            Exit Sub
        End If

        Dim str As String
        'str = "Delete From InputtingAccess Where userID=" & DropDownList1.SelectedItem.Value
        'SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, str)

        Dim i As Integer
        Dim ret As Integer
        For i = 0 To CheckBoxList1.Items.Count - 1
            ' CheckBoxList1.Items(i).Value
            strSQL = "SELECT userID From InputtingAccess Where userID=" & DropDownList1.SelectedItem.Value & " and workshopID=" & CheckBoxList1.Items(i).Value
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            If ds.Tables(0).Rows.Count > 0 Then
                If CheckBoxList1.Items(i).Selected = False Then
                    strSQL = " Delete From InputtingAccess Where userID=" & DropDownList1.SelectedItem.Value & " and workshopID=" & CheckBoxList1.Items(i).Value
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                End If
            Else
                If CheckBoxList1.Items(i).Selected = True Then
                    strSQL = " insert into InputtingAccess(userID,workshopID,createdBy,createdDate) Values ('" & DropDownList1.SelectedValue & "','" & CheckBoxList1.Items(i).Value & "'," & Session("uID") & ",getdate()) "
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)
                End If
            End If
            ds.Reset()
        Next
        strSQL = " Update Indicator set workProcedure=workProcedure+1,users=users+1 "
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSQL)

        ltlAlert.Text = "alert('保存成功！');"

        BindData()
    End Sub
    Private Sub role_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles role.SelectedIndexChanged
        loadUser()
        'BindData()
    End Sub
    Private Sub workshop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles workshop.SelectedIndexChanged
        RadioButtonList1.SelectedIndex = -1
        If DropDownList1.SelectedIndex = 0 Then
            strSQL = "SELECT departmentID,name From departments where issalary=1 order by name"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While reader.Read
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                role.Items.Add(item)
                workshop.Items.Add(item)
            End While
            reader.Close()
        Else
            BindData()
        End If
        loadworkshop()
    End Sub

    Function loadworkshop()
        While Uworkshop.Items.Count > 0
            Uworkshop.Items.RemoveAt(0)
        End While
        item = New ListItem("--")
        item.Value = 0
        Uworkshop.Items.Add(item)
        If workshop.SelectedIndex > 0 Then
            strSQL = "SELECT ID,name From workshop where departmentID='" & workshop.SelectedValue & "'and workshopID is null order by name"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSQL)
            While reader.Read
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                Uworkshop.Items.Add(item)
            End While
            reader.Close()
        End If
        BindData()
    End Function

    Private Sub Uworkshop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Uworkshop.SelectedIndexChanged

    End Sub
End Class

End Namespace
