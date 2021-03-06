
Imports System.Data.SqlClient
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class supplyedit
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim strTemp As String
    Dim strParamquery As String = "sp_setSystemChanges"
    Dim param(2) As SqlParameter

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cMsg14 As System.Web.UI.WebControls.RegularExpressionValidator


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
           
            initialize()
            If Request("id") <> Nothing Then
                BindData()
            End If
        End If
    End Sub

    Private Sub BindData()
        Dim Query As String
        Dim vCompanyid As Integer = Request("id")
        Dim ds As DataSet

        Query = " Select s.company_id, isNull(s.company_name,''), isnull(s.company_code,''), isNull(s.street,''), isnull(s.city,''), isnull(s.state,''), " _
              & " isnull(s.country,''), isNull(s.zip,''), isNull(s.phone,''), isNull(s.fax,'') , isNull(s.mobile,''), isNull(s.email,''), " _
              & " isNull(s.contact,'') ,s.active , isnull(s.comments,''), company_type ,isnull(prefixOfUPC1,''),isnull(prefixOfUPC2,'')" _
              & " From Companies s where company_id=" & Request("id")

        ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, Query)

        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim j As Integer
                For i = 0 To .Rows.Count - 1
                    BXname.Text = .Rows(i).Item(1)
                    bxaddress.Text = .Rows(i).Item(3)
                    city.Text = .Rows(i).Item(4)
                    bxzip.Text = .Rows(i).Item(7)
                    bxphone.Text = .Rows(i).Item(8)
                    bxFax.Text = .Rows(i).Item(9)
                    bxMobile.Text = .Rows(i).Item(10)
                    bxEmail.Text = .Rows(i).Item(11)
                    bxcontactname.Text = .Rows(i).Item(12)
                    If .Rows(i).IsNull(13) Or .Rows(i).Item(13) = True Then
                        active.Checked = True
                    Else
                        active.Checked = False
                    End If
                    code.Text = .Rows(i).Item(2)

                    For j = 0 To place.Items.Count - 1
                        If place.Items(j).Value = .Rows(i).Item(5) Then
                            place.SelectedIndex = j
                            Exit For
                        End If
                    Next
                    For j = 0 To country.Items.Count - 1
                        If country.Items(j).Value = .Rows(i).Item(6) Then
                            country.SelectedIndex = j
                            Exit For
                        End If
                    Next
                    comments.Text = .Rows(i).Item(14)
                    companytype.SelectedValue = .Rows(i).Item(15)
                    prefixUPC1.Text = .Rows(i).Item(16)
                    prefixUPC2.Text = .Rows(i).Item(17)
                Next
            End If
        End With
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("supplylist.aspx?companyType=" & Request("companyType"))

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        BindData()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim vCompanyid As Integer = Request("id")
        Dim str1 As String = BXname.Text
        If BXname.Text.ToString.Trim.Length = 0 Then
            ltlAlert.Text = "alert('名称不能为空.')"
            Exit Sub
        End If
        If companytype.SelectedValue = "" Then
            ltlAlert.Text = "alert('请确认有编辑权限.');"
            Exit Sub
        End If

        Dim strSQL As String

        Dim act As Integer
        If active.Checked = True Then
            act = 1
        Else
            act = 0
        End If

        If vCompanyid <> 0 Then
            strSQL = " Update Companies set company_name=N'" & BXname.Text.Trim() & "'," _
                   & " company_code=N'" & code.Text.Trim() & "'," _
                   & " comments=N'" & comments.Text.Trim() & "'," _
                   & " street=N'" & bxaddress.Text.Trim() & "'," _
                   & " city=N'" & city.Text.Trim() & "'," _
                   & " state=N'" & place.SelectedValue & "'," _
                   & " country=N'" & country.SelectedValue & "'," _
                   & " zip='" & bxzip.Text.Trim() & "'," _
                   & " phone='" & bxphone.Text.Trim() & "'," _
                   & " mobile='" & bxMobile.Text.Trim() & "'," _
                   & " fax='" & bxFax.Text.Trim() & "'," _
                   & " email='" & bxEmail.Text.Trim() & "'," _
                   & " active='" & act.ToString().Trim() & "'," _
                   & " prefixOfUPC1='" & prefixUPC1.Text.Trim() & "'," _
                   & " prefixOfUPC2='" & prefixUPC2.Text.Trim() & "'," _
                   & " contact=N'" & bxcontactname.Text.Trim() & "'," _
                   & " company_type='" & companytype.SelectedValue & "'" _
                   & " Where company_id = " & vCompanyid
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
            'Response.Write(strSQL)
            'Exit Sub
        Else
            Dim vname As String = BXname.Text.Trim()
            strSQL = "SELECT company_id FROM companies where company_name='" & BXname.Text.Trim() & "'"
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    ltlAlert.Text = "alert('公司名称有重复！')"
                    Exit Sub
                End If
            End With
            ds.Reset()

            strSQL = " Insert Into Companies(company_type, company_code, company_Name, street,city, state, country, zip, phone, " _
                   & " fax, mobile, email, contact, comments, active,deleted,prefixOfUPC1,prefixOfUPC2) " _
                   & " Values('" & companytype.SelectedValue & "',N'" & code.Text.Trim() & "', N'" & BXname.Text.Trim() & "',N'" & bxaddress.Text.Trim() _
                   & "',N'" & city.Text.Trim() & "',N'" & place.SelectedValue & "',N'" & country.SelectedValue & "','" & bxzip.Text.Trim() & "','" _
                   & bxphone.Text.Trim() & "','" & bxFax.Text.Trim() & "', '" & bxMobile.Text.Trim() & "', '" & bxEmail.Text.Trim() & "', N'" _
                   & bxcontactname.Text.Trim() & "', N'" & comments.Text.Trim() & "','" & act.ToString().Trim() & "',0,'" & prefixUPC1.Text.Trim() _
                   & "','" & prefixUPC2.Text.Trim() & "') Select @@Identity "
            Dim siD As Integer = CInt(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, strSQL))
            strSQL = " Update Companies Set pwd=N'" & chk.encryptPWD(siD) & "' Where company_id='" & siD & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL)
            ltlAlert.Text = "alert('供应商代码为'" & _code.Text.Trim() & "'的密码为'" & siD & "');"
        End If

        Response.Redirect("supplylist.aspx")
    End Sub

    Private Sub initialize()
        Dim strSQL As String
        Dim ds As DataSet
        Dim item As ListItem
        strSQL = "SELECT s.systemCodeID, s.systemCodeName FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Industry') ORDER BY systemCodeID"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        item = New ListItem("--")
        item.Value = 0

        strSQL = "SELECT s.systemCodeID, s.systemCodeName FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Country and Area') ORDER BY systemCodeID"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        item = New ListItem("--")
        item.Value = 0
        country.Items.Add(item)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(1))
                    item.Value = Convert.ToInt32(.Rows(i).Item(0))
                    country.Items.Add(item)
                Next
            End If
        End With
        ds.Reset()
        country.SelectedIndex = 0

        strSQL = "SELECT s.systemCodeID, s.systemCodeName FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Province') ORDER BY systemCodeID"
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        item = New ListItem("--")
        item.Value = 0
        place.Items.Add(item)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(1))
                    item.Value = Convert.ToInt32(.Rows(i).Item(0))
                    place.Items.Add(item)
                Next
            End If
        End With
        ds.Reset()
        place.SelectedIndex = 0

        'strSQL = "SELECT s.systemCodeID, s.systemCodeName FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID WHERE (st.systemCodeTypeName = 'Company Type') ORDER BY systemCodeID"
        strSQL = "  SELECT s.systemCodeID,  s.systemCodeName  FROM tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st on st.SystemCodeTypeID=s.SystemCodeTypeID "
        If Session("uRole") <> 1 Then
            strSQL &= " Inner Join tcpc0.dbo.User_SupplyClient uw On uw.systemCodeID = s.systemCodeID and uw.userID='" & Session("uID") & "'"
        End If
        strSQL &= " WHERE (st.systemCodeTypeName = 'Company Type'  ) ORDER BY s.systemCodeID  "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    item = New ListItem(.Rows(i).Item(1))
                    item.Value = Convert.ToInt32(.Rows(i).Item(0))
                    companytype.Items.Add(item)
                Next
            End If
        End With
        ds.Reset()
        companytype.SelectedIndex = 0
    End Sub
End Class

End Namespace
