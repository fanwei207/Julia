Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
Partial Class personnelsearch
        Inherits BasePage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents ltlAlert As Literal
    Dim item As ListItem
    Dim Query As String
    Dim reader As SqlDataReader
    Dim chk As New adamClass
    Protected WithEvents coattype As System.Web.UI.WebControls.DropDownList

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
            educationdropdownlist()
            agedropdownlist()
            contractdropdownlist()
            occupationdropdownlist()
            sexdropdownlist()
            provincedropdownlist()
            'funddropdownlist()
            insurancedropdownlist()
            item = New ListItem("--")
            item.Value = 0
            lu.Items.Add(item)
            item = New ListItem("工会会员")
            item.Value = 1
            lu.Items.Add(item)
            item = New ListItem("非工会会员")
            item.Value = 2
            lu.Items.Add(item)
            specialWorkType.Items.Add(New ListItem("--", "0"))
            specialWorkType.Items.Add(New ListItem("焊锡", "1"))
            specialWorkType.Items.Add(New ListItem("打氯仿", "2"))


            item = New ListItem("--")
            item.Value = 0
                Dropdownlist1.Items.Add(item)
                Dropdownlist1.SelectedIndex = 0
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Work Type' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                Dropdownlist1.Items.Add(item)
            End While
            reader.Close()


            item = New ListItem("--")
            item.Value = 0
                Dropdownlist2.Items.Add(item)
                Dropdownlist2.SelectedIndex = 0
            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Employ Type' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                Dropdownlist2.Items.Add(item)
            End While
            reader.Close()

            workshop.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
                workshop.Items.Add(item)
                workshop.SelectedIndex = 0
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

    Sub roledropdownlist()

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

    Sub educationdropdownlist()

        item = New ListItem("--")
        item.Value = 0
            education.Items.Add(item)
            education.SelectedIndex = 0
        Query = " SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID " _
              & " Where st.systemCodeTypeName='Education' order by s.systemCodeID "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
        While reader.Read()
            item = New ListItem(reader(1))
            item.Value = Convert.ToInt32(reader(0))
            education.Items.Add(item)
        End While
        reader.Close()
    End Sub

    Sub agedropdownlist()

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
            age.SelectedIndex = 0
    End Sub

    Sub contractdropdownlist()

        item = New ListItem("--")
        item.Value = 0
        contract.Items.Add(item)
        Query = " SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID " _
              & " Where st.systemCodeTypeName='Contract Type' order by s.systemCodeID "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
        While reader.Read()
            item = New ListItem(reader(1))
            item.Value = Convert.ToInt32(reader(0))
            contract.Items.Add(item)
        End While
        item = New ListItem("无合同")
        item.Value = -1
        contract.Items.Add(item)
        item = New ListItem("有合同")
        item.Value = -2
        contract.Items.Add(item)
            reader.Close()
            contract.SelectedIndex = 0
    End Sub

    Sub occupationdropdownlist()

        item = New ListItem("--")
        item.Value = 0
            occupation.Items.Add(item)
            occupation.SelectedIndex = 0
        Query = " SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID " _
              & " Where st.systemCodeTypeName='Occupation' order by s.systemCodeID "
        reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
        While reader.Read()
            item = New ListItem(reader(1))
            item.Value = Convert.ToInt32(reader(0))
            occupation.Items.Add(item)
        End While
        reader.Close()
    End Sub

    Sub sexdropdownlist()

        item = New ListItem("--")
        item.Value = 0
            sex.Items.Add(item)
            sex.SelectedIndex = 0
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

        item = New ListItem("--")
        item.Value = 0
            province.Items.Add(item)
            province.SelectedIndex = 0
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

    Private Sub Department_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Department.SelectedIndexChanged
        If Department.SelectedValue <> 0 Then
            workshopDropDownList()
        Else
            workshop.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
                workshop.Items.Add(item)
                workshop.SelectedIndex = 0
        End If
       
    End Sub

    Sub workshopDropDownList()
        Dim dst As DataSet
        workshop.Items.Clear()

        item = New ListItem("--")
        item.Value = 0
        workshop.Items.Add(item)
            workshop.SelectedIndex = 0

        Query = " SELECT w.id, w.name FROM Workshop w INNER JOIN departments d ON w.departmentID = d.departmentID " _
              & " WHERE d.name=N'" & Department.SelectedItem.Text.Trim() & "' AND w.workshopID IS NULL Order by w.code  "

        dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(1))
                    item.Value = Convert.ToInt32(.Rows(i).Item(0))
                    workshop.Items.Add(item)
                Next
            End If
        End With
        dst.Reset()
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
            Dim x As String = ""
            Dim y As String = ""
            Dim z As String = ""
            Dim w As String = ""
            Dim k As String = ""

            If chkall.Checked Then
                s = "&all=true"
            End If

            If houseFund.Checked Then
                t = "&hF=true"
            End If

            If medicalFund.Checked Then
                x = "&mF=true"
            End If

            If unemployFund.Checked Then
                y = "&uF=true"
            End If

            If retiredFund.Checked Then
                z = "&rF=true"
            End If

            If sretiredFund.Checked Then
                w = "&sF=true"
            End If

            If sclear.Checked Then
                k = "&sc=true"
            End If

            s = "/admin/personnellist.aspx?dp=" & Department.SelectedValue _
            & "&ro=" & role.SelectedValue & "&ed=" & education.SelectedValue _
            & "&age=" & age.SelectedValue & "&ct=" & contract.SelectedValue _
            & "&op=" & occupation.SelectedValue & "&sex=" & sex.SelectedValue _
            & "&t1=" & certificate.Text.Trim() & "&insurance=" & insurance.SelectedValue _
            & "&name=" & Name.Text.Trim() & "&pv=" & province.SelectedValue _
            & "&note=" & memo.Text.Trim() & "&fund=" & workshop.SelectedValue _
            & "&lu=" & lu.SelectedValue & "&id=" & code.SkinID _
            & "&entfr=" & enterdatefr.Text.Trim() & "&entto=" & enterdateto.Text.Trim() _
            & "&jcfs=" & Dropdownlist1.SelectedValue & "&ygxz=" & Dropdownlist2.SelectedValue _
            & "&levfr=" & leavedatefr.Text.Trim() & "&levto=" & leavedateto.Text.Trim() & "&sWT=" & specialWorkType.SelectedValue _
            & "&idc=" & IDcode.Text.Trim() & "&aa=" & area.Text.Trim() & "&bg=" & begood.Text.Trim() & s & t & x & y & z & w & k

            Response.Redirect(s)

        End Sub
    End Class

End Namespace
