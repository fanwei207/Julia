Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class stationeryin
        Inherits BasePage
    Dim chk As New adamClass

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Textbox3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cMsg1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    'Protected WithEvents ltlAlert As Literal

    Shared type As New Hashtable


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        
        If Not IsPostBack Then
             
            type.Clear()
            InDate.Text = DateTime.Today
            stationeryDropDownList()
            price.Text = type.Item(stationery.SelectedValue)
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If PDQty.Text = "" Then
            ltlAlert.Text = "alert('数量不能为空!');Form1.PDQty.focus();"
            Exit Sub
        End If
        Literal1.Text = ""
        Dim Query As String
        Dim reader As SqlDataReader
        Dim script As String
        Dim nt As String = note.Text.Trim()
        Dim qty As Integer = CInt(PDQty.Text)
        If (nt.Length > 255) Then
            Literal1.Text = "<script language=javascript> alert('文本最大长度为255个字符！') </script>"
            Exit Sub
        End If
        Dim uid As String = Session("uID")
        Dim orgid As String = Session("orgID")

        Query = "Insert into Stationery (stationeryTypeID,quantity,createdDate,comments,isOut,createdBy,organizationID)"
        Query = Query & " Values ('"
        Query = Query & stationery.SelectedValue & " ',"
        Query = Query & "'" & qty & "',"
        Query = Query & "'" & InDate.Text & "',"
        Query = Query & "N'" & chk.sqlEncode(note.Text) & "',"
        Query = Query & "'0','" & uid & "','" & orgid & "')"
        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

        PDQty.Text = ""
        note.Text = ""
        totalprice.Text = ""
        Literal1.Text = "<script language=javascript> alert('进货登记已成功！') </script>"

    End Sub
    Sub stationeryDropDownList()
        Dim query As String
        Dim cmd As SqlCommand
        Dim con As SqlConnection
        Dim reader As SqlDataReader
        Dim ls As New ListItem
        query = " select c.systemCodeID,c.systemCodeName,c.comments from tcpc0.dbo.systemCode c " _
              & " inner join tcpc0.dbo.systemCodeType s on c.systemCodeTypeID=s.systemCodeTypeID " _
              & " where s.systemCodeTypeName='Stationery Type' order by systemCodeID"
        con = New SqlConnection(chk.dsnx)
        cmd = New SqlCommand(query, con)
        con.Open()
        reader = cmd.ExecuteReader()
        While (reader.Read())
            ls = New ListItem
            ls.Value = reader(0)
            ls.Text = reader(1)
            stationery.Items.Add(ls)
            If Not type.ContainsKey(reader(0).ToString()) Then
                type.Add(reader(0).ToString(), reader(2).ToString())
            End If
        End While
        reader.Close()
        con.Close()

    End Sub
    Sub pricechang(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If PDQty.Text = "" Then
            ltlAlert.Text = "alert('数量不能为空!');Form1.PDQty.focus();"
            totalprice.Text = ""
            Exit Sub
        End If
        'If Not IsNumeric(price.Text) Then
        'ltlAlert.Text = "alert('输入单价有误!');Form1.price.focus();"
        'Exit Sub
        'End If
        totalprice.Text = (CDec(price.Text.Trim()) * CInt(PDQty.Text.Trim())).ToString()
        ltlAlert.Text = "Form1.note.focus();"
    End Sub

    Sub stationerychange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        price.Text = type.Item(stationery.SelectedValue)
        If PDQty.Text <> "" Then
            totalprice.Text = (CDec(price.Text.Trim()) * CInt(PDQty.Text.Trim())).ToString()
        Else
            totalprice.Text = ""
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect(chk.urlRand("\daily\stationeryinlist.aspx"))
    End Sub
End Class

End Namespace
