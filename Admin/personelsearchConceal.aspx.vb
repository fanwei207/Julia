'!*******************************************************************************!
'* @@ NAME				:	personelsearchConceal.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for personelsearchConceal.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	April 5 2006
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class personelsearchConceal
        Inherits  BasePage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Dim item As ListItem
        Dim Query As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass
        'Protected WithEvents ltlAlert As Literal

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then
                Departmentdropdownlist()
                roledropdownlist()
                agedropdownlist()
                sexdropdownlist()
                provincedropdownlist()

                insurancedropdownlist()
            End If
        End Sub

        Sub insurancedropdownlist()

            item = New ListItem("--")
            item.Value = 0
            insurance.Items.Add(item)
            insurance.SelectedIndex = 0
            Query = " SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID " _
                  & " Where st.systemCodeTypeName='Insurance Type' order by s.systemCodeID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                insurance.Items.Add(item)
            End While
            item = New ListItem("无保险")
            item.Value = -1
            insurance.Items.Add(item)
            item = New ListItem("有保险")
            item.Value = -2
            insurance.Items.Add(item)
            reader.Close()
        End Sub

        Sub Departmentdropdownlist()

            item = New ListItem("--")
            item.Value = 0
            Department.Items.Add(item)
            Department.SelectedIndex = 0
            Query = " SELECT departmentID,Name From Departments WHERE isSalary='1' AND  active='1' order by departmentID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                Department.Items.Add(item)
            End While
            reader.Close()
        End Sub

        Sub roledropdownlist1()

            item = New ListItem("--")
            item.Value = 0
            role.Items.Add(item)
            role.SelectedIndex = 0
            Query = " SELECT roleID,roleName From roles where roleID>1 order by roleID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                role.Items.Add(item)
            End While
            reader.Close()
        End Sub
        '工号分布情况
        Function BindUserNoMax() As String

            BindUserNoMax = "<tr>"

            Query = " Select Cate "
            Query &= "  , loginName = Max(loginName) "
            Query &= "From("
            Query &= "          Select LoginName "
            Query &= "              , Case When loginName Like '[0-9]%' And loginName Not Like '%[a-zA-Z]' Then N'纯数字' "
            Query &= "                     When loginName Like '[a-zA-Z][a-zA-Z]%' Then N'纯英文' "
            Query &= "                     When loginName Like '%[a-zA-Z]' Then N'试用人员' "
            Query &= "                     Else Left(loginName, 1) + N' 开头' "
            Query &= "                End As Cate "
            Query &= "          From tcpc0.dbo.users "
            Query &= "          Where(plantCode = " & Session("PlantCode") & ") "
            Query &= "              And loginName <> '' "
            Query &= "          Union "
            Query &= "          Select LoginName "
            Query &= "              , Case When loginName Like '[0-9]%' And loginName Not Like '%[a-zA-Z]' Then N'纯数字' "
            Query &= "                     When loginName Like '[a-zA-Z][a-zA-Z]%' Then N'纯英文' "
            Query &= "                     When loginName Like '%[a-zA-Z]' Then N'试用人员' "
            Query &= "                     Else Left(loginName, 1) + N' 开头' "
            Query &= "                End As Cate "
            Query &= "          From tcpc0.dbo.UserApprove "
            Query &= "          Where(plantCode = " & Session("PlantCode") & ") "
            Query &= "              And loginName <> '' "
            Query &= "     ) mstr "
            Query &= "Group By Cate "
            Query &= "Order By Cate "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()

                BindUserNoMax &= "<td>" & reader("Cate") & " : " & reader("loginName") & "</td>"
            End While

            BindUserNoMax &= "</tr>"

            reader.Close()
        End Function

        Sub agedropdownlist()
            age.SelectedIndex = 0
            item = New ListItem("--")
            item.Value = 0
            age.Items.Add(item)
            item = New ListItem("20岁以下")
            item.Value = 1
            age.Items.Add(item)
            item = New ListItem("20-30")
            item.Value = 2
            age.Items.Add(item)
            item = New ListItem("30-40")
            item.Value = 3
            age.Items.Add(item)
            item = New ListItem("40-50")
            item.Value = 4
            age.Items.Add(item)
            item = New ListItem("50-60")
            item.Value = 5
            age.Items.Add(item)
            item = New ListItem("60岁以上")
            item.Value = 6
            age.Items.Add(item)
        End Sub

        Sub sexdropdownlist()
            sex.SelectedIndex = 0
            item = New ListItem("--")
            item.Value = 0
            sex.Items.Add(item)
            Query = " SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID " _
                  & " Where st.systemCodeTypeName='Sex' order by s.systemCodeID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                sex.Items.Add(item)
            End While
            reader.Close()
        End Sub

        Sub provincedropdownlist()
            province.SelectedIndex = 0
            item = New ListItem("--")
            item.Value = 0
            province.Items.Add(item)
            Query = " SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID " _
                  & " Where st.systemCodeTypeName='Province' order by s.systemCodeID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                province.Items.Add(item)
            End While
            reader.Close()
        End Sub

        Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
            If (enterdatefr.Text.Trim().Length > 0) Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(enterdatefr.Text.Trim())
                Catch ex As Exception
                    ltlAlert.Text = "alert('进厂时间 格式不正确！');"
                End Try
            End If

            If (enterdateto.Text.Trim().Length > 0) Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(enterdateto.Text.Trim())
                Catch ex As Exception
                    ltlAlert.Text = "alert('进厂时间 格式不正确！');"
                End Try
            End If

            If (leavedatefr.Text.Trim().Length > 0) Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(leavedatefr.Text.Trim())
                Catch ex As Exception
                    ltlAlert.Text = "alert('离职时间 格式不正确！');"
                End Try
            End If

            If (leavedateto.Text.Trim().Length > 0) Then
                Try
                    Dim _dt As DateTime = Convert.ToDateTime(leavedateto.Text.Trim())
                Catch ex As Exception
                    ltlAlert.Text = "alert('离职时间 格式不正确！');"
                End Try
            End If

            Dim s As String = ""
            Dim t As String = ""

            If chkall.Checked Then
                s = "&all=true"
            End If

            s = "/admin/personnellist.aspx?dp=" & Department.SelectedValue _
                & "&ro=" & role.SelectedValue & "&ed=0" _
                & "&age=" & age.SelectedValue & "&ct=0" _
                & "&op=0&sex=" & sex.SelectedValue _
                & "&t1=&insurance=" & insurance.SelectedValue _
                & "&name=" & Name.Text.Trim() & "&pv=" & province.SelectedValue _
                & "&note=&fund=0" _
                & "&lu=0&id=" & code.Text.Trim() _
                & "&entfr=" & enterdatefr.Text.Trim() & "&entto=" & enterdateto.Text.Trim() _
                & "&jcfs=0&ygxz=0" _
                & "&conceal=0" _
                & "&levfr=" & leavedatefr.Text.Trim() & "&levto=" & leavedateto.Text.Trim() & "&sWT=0&idc=" & IDcode.Text.Trim() & "&RoleType=" & ddl_RoleType.SelectedValue & s & t
            Response.Redirect(s)

        End Sub

        Protected Sub ddl_RoleType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_RoleType.SelectedIndexChanged
            roleDropDownList()
        End Sub
        Sub roleDropDownList()
            Dim dst As DataSet
            role.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
            role.Items.Add(item)
            role.SelectedIndex = 0

            Dim strSQL As String
            Select Case ddl_RoleType.SelectedIndex
                Case 0
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0)  From roles where roleID>1 order by roleID "
                Case 1
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " _
                       & " From Roles where roleID>=100 and roleID<300 " _
                       & " Order by roleID"
                Case 2
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " _
                       & " From Roles where roleID>=300 and roleID<500 " _
                       & " Order by roleID"
                Case 3
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " _
                       & " From Roles where roleID>=500 and roleID<1000 " _
                       & " Order by roleID"
                Case 4
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " _
                       & " From Roles where roleID>=1000 and roleID<5000 " _
                       & " Order by roleID"
            End Select

            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(2))
                        item.Value = Convert.ToInt32(.Rows(i).Item(1))
                        role.Items.Add(item)
                    Next
                End If
            End With
            dst.Reset()
        End Sub
    End Class

End Namespace
