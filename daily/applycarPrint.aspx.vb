Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class applycarPrint
        Inherits BasePage
    Dim chk As New adamClass
    Protected WithEvents Labelmemory As System.Web.UI.WebControls.Label
    'Protected WithEvents ltlAlert As Literal
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected WithEvents TextBoxquery As System.Web.UI.WebControls.TextBox
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
        If Not IsPostBack Then
             
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = "SELECT u.userID, u.userName, g.name AS Expr1, g.englishName FROM tcpc0.dbo.users u INNER JOIN Organization g ON g.organizationID = u.organizationID where u.userid=" & Session("uID")
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Item(0) = Session("uID") Then
                            headline1.Text = .Rows(i).Item(2)
                            tailline1.Text = .Rows(i).Item(2)
                            tailline2.Text = "申请日期和时间:"
                            tailline3.Text = "联系人：" & .Rows(i).Item(1)
                            tailline4.Text = "部门经理：             "
                        End If
                    Next
                End If
            End With
            table1BindData()
            table2BindData()
        End If
    End Sub
    Private Sub table1BindData()

        Dim Query As String
        Dim ds As DataSet
        Dim cardate As String
        If Request("id") <> "" Then
            Query = " Select usedDateTime From applyCarDetails where carID='" & Request("id") & "'"
            cardate = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)
        End If
        Query = "SELECT carID, [to], toTel, toFax, cc, sb, [from], fromTel, fromFax, usedDate, comments FROM applyCar "
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
        With ds.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1
                    objtto.Text = .Rows(i).Item(1)
                    objttotel.Text = .Rows(i).Item(2)
                    objttofax.Text = .Rows(i).Item(3)
                    objtcc.Text = .Rows(i).Item(4)
                    objtsb.Text = .Rows(i).Item(5)
                    objtfrom.Text = .Rows(i).Item(6)
                    objtfromtel.Text = .Rows(i).Item(7)
                    objtfromfax.Text = .Rows(i).Item(8)
                    If Request("id") <> "" Then
                        objtdate.Text = cardate
                    Else
                        objtdate.Text = .Rows(i).Item(9)
                    End If
                    objtother.Text = .Rows(i).Item(10)
                Next
            End If
        End With
    End Sub
    Private Sub table2BindData()
        Dim tabledate As DateTime
        If Request("printno") <> "" Then
            tabledate = CDate(Request("printno"))
        End If
        Dim Query As String
        Dim Reader As DataSet
        If Request("id") = "" Then
            Query = "SELECT carID, cartime, startPlace, endPlace, Qty, personNum,comments FROM applyCarDetails where datepart(month,usedDateTime)='" & tabledate.Month.ToString & "' and datepart(day,usedDateTime)='" & tabledate.Day.ToString & "'"
        Else
            Query = "SELECT carID, cartime, startPlace, endPlace, Qty, personNum,comments FROM applyCarDetails where carID='" & Request("id") & "'"
        End If
        Reader = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("gcarid", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("guseddate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gstartplace", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gendplace", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gqty", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("gperson", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("comments", System.Type.GetType("System.String")))
        Dim datetrasfer As Date
        With Reader.Tables(0)
            If (.Rows.Count > 0) Then
                Dim i As Integer
                Dim dr1 As DataRow
                For i = 0 To .Rows.Count - 1
                    dr1 = dt.NewRow()
                    dr1.Item("gcarid") = i + 1
                    dr1.Item("gUsedDate") = .Rows(i).Item(1)
                    dr1.Item("gstartplace") = .Rows(i).Item(2).ToString().Trim()
                    dr1.Item("gendplace") = .Rows(i).Item(3).ToString().Trim()
                    dr1.Item("gqty") = .Rows(i).Item(4).ToString().Trim()
                    dr1.Item("gperson") = .Rows(i).Item(5).ToString().Trim()
                    dr1.Item("comments") = .Rows(i).Item(6).ToString().Trim()
                    dt.Rows.Add(dr1)
                Next
            End If
        End With
        Dim dv As DataView
        dv = New DataView(dt)
        dgOrderDetail.DataSource = dv
        dgOrderDetail.DataBind()
    End Sub
    Public Function BindTemp(ByVal temppoint As Integer)
        Dim tempstr(143) As String
        Dim i, h, m As Integer
        i = 0
        For h = 0 To 11
            For m = 0 To 59 Step 10
                i = i + 1
                If h = 0 And m = 0 Then
                    tempstr(i - 1) = "12:00 AM"
                ElseIf h = 0 Then
                    tempstr(i - 1) = "12" & ":" & m.ToString().Trim() & " AM"
                ElseIf m = 0 Then
                    tempstr(i - 1) = h.ToString().Trim() & ":" & "00" & " AM"
                Else
                    tempstr(i - 1) = h.ToString().Trim() & ":" & m.ToString().Trim() & " AM"
                End If
            Next
            m = 0
        Next
        For h = 12 To 23
            For m = 0 To 59 Step 10
                i = i + 1
                If h = 12 And m = 0 Then
                    tempstr(i - 1) = "12:00 PM"
                ElseIf h = 12 Then
                    tempstr(i - 1) = "12" & ":" & m.ToString().Trim() & " PM"
                ElseIf m = 0 Then
                    tempstr(i - 1) = (h - 12).ToString().Trim() & ":" & "00" & " PM"
                Else
                    tempstr(i - 1) = (h - 12).ToString().Trim() & ":" & m.ToString().Trim() & " PM"
                End If
            Next
            m = 0
        Next
        Return tempstr(temppoint)
    End Function
End Class

End Namespace
