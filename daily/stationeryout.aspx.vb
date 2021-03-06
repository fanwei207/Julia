Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class stationeryout
        Inherits BasePage
    Dim chk As New adamClass

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cMsg1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
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
             
            InDate.Text = DateTime.Today
            stationeryDropDownList()
            userIDDropDownList()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Query As String
        Dim judge1 As Decimal = 0
        Dim judge2 As Decimal = 0

        Dim reader As SqlDataReader
            If quantity.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('数量不能为空！');Form1.quantity.focus();"
                Exit Sub
            End If

            If CInt(quantity.Text) = 0 Then
                ltlAlert.Text = "alert('输入数量不能为零！');Form1.quantity.focus();"
                Exit Sub
            End If

            Literal1.Text = ""
            Query = " Select quantity,isOut From Stationery Where stationeryTypeID='" & stationery.SelectedValue & "' order by stationeryID " 'and isOut='0'"

            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read
                If reader(1) = False Then

                    judge1 = judge1 + CDec(reader(0).ToString())
                Else
                    judge2 = judge2 + CDec(reader(0).ToString())
                End If
            End While
            reader.Close()

            If (judge1 - judge2) < CInt(quantity.Text) Then
                ltlAlert.Text = "alert('输入数量大于库存数！');Form1.quantity.focus();"
                Exit Sub
            End If
             
            Dim script As String
            Dim nt As String = note.Text.Trim()
            Dim qty As Integer = CInt(quantity.Text)
            If (nt.Length > 255) Then
                Literal1.Text = "<script language=javascript> alert('文本最大长度为255个字符！') </script>"
                Exit Sub
            End If
            Dim uid As String = Session("uID")
            Dim orgid As String = Session("orgID")

            Query = "Insert into Stationery (stationeryTypeID,quantity,userID,createdDate,comments,isOut,createdBy,organizationID)"
            Query = Query & " Values ('"
            Query = Query & stationery.SelectedValue & "',"
            Query = Query & "'" & qty & "',"
            Query = Query & "'" & userID.SelectedValue & "',"
            Query = Query & "'" & InDate.Text & "',"
            Query = Query & "N'" & chk.sqlEncode(note.Text) & "',"
            Query = Query & "'1','" & uid & "','" & orgid & "')"
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            quantity.Text = ""
            note.Text = ""
            Literal1.Text = "<script language=javascript> alert('申领登记已成功！') </script>"
    End Sub
    Sub userIDDropDownList()
        Dim query As String
        Dim cmd As SqlCommand
        Dim con As SqlConnection
        Dim reader As SqlDataReader
        Dim ls As New ListItem
        query = "select userID,userName from tcpc0.dbo.users " _
              & "Order by userID "
        con = New SqlConnection(chk.dsnx)
        cmd = New SqlCommand(query, con)
        con.Open()
        reader = cmd.ExecuteReader()
        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1)
            userID.Items.Add(ls)
        End While
        reader.Close()
        con.Close()
    End Sub
    Sub stationeryDropDownList()
        Dim query As String
        Dim cmd As SqlCommand
        Dim con As SqlConnection
        Dim reader As SqlDataReader
        Dim ls As New ListItem
        query = " select c.systemCodeID,c.systemCodeName from tcpc0.dbo.systemCode c " _
              & " inner join tcpc0.dbo.systemCodeType s on c.systemCodeTypeID=s.systemCodeTypeID " _
              & " where s.systemCodeTypeName='Stationery Type'order by systemCodeID"
        con = New SqlConnection(chk.dsnx)
        cmd = New SqlCommand(query, con)
        con.Open()
        reader = cmd.ExecuteReader()
        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1)
            stationery.Items.Add(ls)
        End While
        reader.Close()
        con.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect(chk.urlRand("\daily\stationeryoutlist.aspx"))
    End Sub
End Class

End Namespace
