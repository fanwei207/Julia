'*@     Create By   :   Ye Bin    
'*@     Create Date :   2005-6-24
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   View Part In/Out/Ret Summary
Imports adamFuncs
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace tcpc

Partial Class partIOSum
        Inherits BasePage 
    'Protected WithEvents ltlAlert As Literal
    Dim strSql As String
    Dim reader As SqlDataReader
    Public chk As New adamClass
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
            If Request("name") <> "--" Then
                lblTitle.Text = "状态为" & Request("name") & "的<b>" & Server.UrlDecode(Request("partCode")) & "&nbsp;/&nbsp;" & Server.UrlDecode(Request("partdesc"))
            Else
                lblTitle.Text = "<b>" & Server.UrlDecode(Request("partCode")) & "&nbsp;/&nbsp;" & Server.UrlDecode(Request("partDesc"))
            End If
            lblOut.Text = "<b>出库总数：</b>0.00"
            lblIn.Text = "<b>入库总数：</b>0.00"
            lblRetIn.Text = "<b>部门退库总数：</b>0.00"
            lblRetOut.Text = "<b>退供应商总数：</b>0.00"
            lblMove.Text = "<b>移库总数：</b>0.00"
            BindData()
        End If
        ltlAlert.Text = "Form1.txtStartDate.focus();"
    End Sub

    Sub BindData()
        Dim strout As Decimal = 0.0
        Dim strin As Decimal = 0.0
        Dim strretin As Decimal = 0.0
        Dim strretout As Decimal = 0.0
        Dim strmove As Decimal = 0.0
        Dim strstart As String = txtStartDate.Text.Trim()
        Dim strend As String = txtEndDate.Text.Trim()
        
        strSql = " Select Isnull(Sum(tran_qty),0) From Part_tran Where part_id='" & Request("id") & "' And warehouseID='" & Request("pid") _
               & "' And tran_type='O' And Isnull(status,0)='" & Request("st") & "'"
        If strstart.Trim().Length > 0 Then
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "' And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "'"
            End If
        Else
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql = " Select * From Part_tran Where 1=2 "
            End If
        End If
        strout = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

        strSql = " Select Isnull(Sum(tran_qty),0) From Part_tran Where part_id='" & Request("id") & "' And warehouseID='" & Request("pid") _
               & "' And tran_type='I' And Isnull(status,0)='" & Request("st") & "'"
        If strstart.Trim().Length > 0 Then
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "' And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "'"
            End If
        Else
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql = " Select * From Part_tran Where 1=2 "
            End If
        End If
        strin = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

        strSql = " Select Isnull(Sum(tran_qty),0) From Part_tran Where part_id='" & Request("id") & "' And warehouseID='" & Request("pid") _
               & "' And tran_type='DR' And Isnull(status,0)='" & Request("st") & "'"
        If strstart.Trim().Length > 0 Then
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "' And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "'"
            End If
        Else
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql = " Select * From Part_tran Where 1=2 "
            End If
        End If
        strretin = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

        strSql = " Select Isnull(Sum(tran_qty),0) From Part_tran Where part_id='" & Request("id") & "' And warehouseID='" & Request("pid") _
               & "' And tran_type='RS' And Isnull(status,0)='" & Request("st") & "'"
        If strstart.Trim().Length > 0 Then
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "' And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "'"
            End If
        Else
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql = " Select * From Part_tran Where 1=2 "
            End If
        End If
        strretout = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

        strSql = " Select Isnull(Sum(tran_qty),0) From Part_tran Where part_id='" & Request("id") & "' And warehouseID='" & Request("pid") _
               & "' And tran_type='PM' And Isnull(status,0)='" & Request("st") & "'"
        If strstart.Trim().Length > 0 Then
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "' And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql &= " And tran_date>='" & CDate(strstart.Trim()) & "'"
            End If
        Else
            If strend.Trim().Length > 0 Then
                strSql &= " And tran_date<'" & CDate(strend.Trim()).AddDays(1) & "'"
            Else
                strSql = " Select * From Part_tran Where 1=2 "
            End If
        End If
        strmove = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, strSql)

        lblOut.Text = "<b>出库总数：</b>&nbsp;" & Format(Math.Abs(strout), "##,##0.00").ToString().Trim()
        lblIn.Text = "<b>入库总数：</b>&nbsp;" & Format(strin, "#,##0.00").ToString().Trim()
        lblRetIn.Text = "<b>部门退库总数：</b>&nbsp;" & Format(strretin, "#,##0.00").ToString().Trim()
        lblRetOut.Text = "<b>退供应商总数：</b>&nbsp;" & Format(Math.Abs(strretout), "#,##0.00").ToString().Trim()
        lblMove.Text = "<b>移库总数：</b>&nbsp;" & Format(strmove, "#,##0.00").ToString().Trim()
    End Sub

    Public Sub CloseClisk(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        ltlAlert.Text = "window.close();"
    End Sub

    Public Sub ReportClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReport.Click
        If IsDate(txtStartDate.Text.Trim()) = False Then
            ltlAlert.Text = "alert('起始日期不是日期类型！');"
            Exit Sub
        End If
        If IsDate(txtEndDate.Text.Trim()) = False Then
            ltlAlert.Text = "alert('截止日期不是日期类型！');"
            Exit Sub
        End If
        BindData()
    End Sub

End Class

End Namespace
