'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-3-17
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   Select ID field to Print 
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


Partial Class IDCardPrint
        Inherits BasePage
    Dim chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim nRet As Integer

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
        If Not IsPostBack Then
           
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        ltlAlert.Text = ""
        Session("CardList") = Nothing
        Dim ss As String = txtCode.Text.Trim
        Dim tt As String
            Dim Prints As String = ""
        Dim ind As Integer
        Dim strSql As String
        Dim ID As String
        While (ss.Length > 0)
            If ss.IndexOf(",") <> -1 Or ss.IndexOf("，") <> -1 Then
                If ss.IndexOf(",") <> -1 Then
                    ind = ss.IndexOf(",")
                ElseIf ss.IndexOf("，") <> -1 Then
                    ind = ss.IndexOf("，")
                End If
                tt = ss.Substring(0, ind)
                ss = ss.Substring(ind + 1)
            Else
                tt = ss
                ss = ""
            End If

            ind = tt.IndexOf("-")
            If ind <> -1 Then

                strSql = " Select userID From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and userNo='" & tt.Substring(0, ind).Trim() & "'"
                ID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                Prints = Prints & ID & "-"
                ID = ""
                strSql = " Select userID From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and userNo='" & tt.Substring(ind + 1).Trim() & "'"
                ID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                Prints = Prints & ID & ","
            Else
                strSql = " Select userID From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and userNo='" & tt.Trim() & "'"
                ID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                Prints = Prints & ID & ","
            End If
        End While

        If txtCode.Text.Trim().Length > 0 Then
            Prints = Prints.Substring(0, Prints.Length - 1)
            ltlAlert.Text = " var w; w=window.open('PrintCard.aspx?id=" & Prints & "','PrintCard', 'toolbar=1,location=0,directories=0,status=0,menubar=1,resizable=1,scrollbars=1,width=780,height=500');w.focus();"
        Else
            ltlAlert.Text = "alert('需要打印的员工工号范围不能为空！');"
        End If
    End Sub

        Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
            ltlAlert.Text = ""
            Session("CardList") = Nothing
            Dim ss As String = txtCode.Text.Trim
            Dim tt As String
            Dim Prints As String
            Dim ind As Integer
            Dim strSql As String
            Dim ID As String
            While (ss.Length > 0)
                If ss.IndexOf(",") <> -1 Or ss.IndexOf("，") <> -1 Then
                    If ss.IndexOf(",") <> -1 Then
                        ind = ss.IndexOf(",")
                    ElseIf ss.IndexOf("，") <> -1 Then
                        ind = ss.IndexOf("，")
                    End If
                    tt = ss.Substring(0, ind)
                    ss = ss.Substring(ind + 1)
                Else
                    tt = ss
                    ss = ""
                End If

                ind = tt.IndexOf("-")
                If ind <> -1 Then

                    strSql = " Select userID From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and userNo='" & tt.Substring(0, ind).Trim() & "'"
                    ID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                    Prints = Prints & ID & "-"
                    ID = ""
                    strSql = " Select userID From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and userNo='" & tt.Substring(ind + 1).Trim() & "'"
                    ID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                    Prints = Prints & ID & ","
                Else
                    strSql = " Select userID From tcpc0.dbo.users Where plantCode='" & Session("PlantCode") & "' and userNo='" & tt.Trim() & "'"
                    ID = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)
                    Prints = Prints & ID & ","
                End If
            End While

            If txtCode.Text.Trim().Length > 0 Then
                Prints = Prints.Substring(0, Prints.Length - 1)
                ltlAlert.Text = " var w; w=window.open('NewPrintCard.aspx?id=" & Prints & "','PrintCard', 'toolbar=1,location=0,directories=0,status=0,menubar=1,resizable=1,scrollbars=1,width=780,height=500');w.focus();"
            Else
                ltlAlert.Text = "alert('需要打印的员工工号范围不能为空！');"
            End If
        End Sub
    End Class

End Namespace
