Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class offdutydetail
        Inherits BasePage
        Dim dst As DataSet
        Dim query As String
        Shared sortOrder As String = ""
        Shared sortDir As String = "ASC"
        'Protected WithEvents ltlAlert As System.Web.UI.WebControls.Literal
        Dim chk As New adamClass


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

                BindData()
            End If
        End Sub
        Sub BindData()
            Dim startdate As DateTime = CDate(Request("sdate"))
            Dim enddate As DateTime = CDate(Request("edate"))
            Dim i As Integer = 0

            query = " SELECT u.userNo, u.userName, d.workdate,d.deductNum,d.comments FROM DeductMoney d "
            query &= " INNER JOIN tcpc0.dbo.users u ON d.userCode = u.userID  and u.plantCode='" & Session("PlantCode") & "' "
            query &= " INNER JOIN tcpc0.dbo.systemCode sc ON d.typeID = sc.systemCodeID AND sc.systemCodeName = N'请假' "
            query &= " INNER JOIN tcpc0.dbo.systemCodeType sct ON sc.systemCodeTypeID = sct.systemCodeTypeID AND sct.systemCodeTypeName = 'Deduct Money Type' "
            query &= " WHERE  d.userCode='" & Request("id") & "' and d.organizationID=" & Session("orgID") & " AND d.workDate>='" & startdate & "' AND d.workDate<'" & enddate.AddDays(1) & "'"
            query &= " Order By d.workDate "

            Session("EXSQL") = query
            Session("EXTitle") = "<b>工号</b>~^<b>姓名</b>~^<b>日期</b>~^<b>请假天数</b>~^500^<b>备注</b>~^"

            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("Date", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("starttime", System.Type.GetType("System.Decimal")))
            dtl.Columns.Add(New DataColumn("endtime", System.Type.GetType("System.String")))

            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Label1.Text = "工号：" & .Rows(i).Item(0).ToString()
                    Label2.Text = "姓名：" & .Rows(i).Item(1).ToString()
                    Dim drow As DataRow
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("gsort") = i + 1
                        drow.Item("Date") = Format(.Rows(i).Item(2), "yyyy-MM-dd")
                        drow.Item("starttime") = .Rows(i).Item(3).ToString().Trim()
                        drow.Item("endtime") = .Rows(i).Item(4).ToString().Trim()
                        dtl.Rows.Add(drow)
                    Next
                    Button1.Enabled = True
                Else
                    Button1.Enabled = False
                End If
            End With
            Dim dvw As DataView
            dvw = New DataView(dtl)

            Try
                Datagrid2.DataSource = dvw
                Datagrid2.DataBind()
            Catch
            End Try
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            ltlAlert.Text = " var w=window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); "
        End Sub

        Protected Sub Datagrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Response.Redirect("offduty.aspx")
        End Sub
    End Class

End Namespace
