Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO


Namespace tcpc

Partial Class perf_hist_fine
        Inherits BasePage
    Public chk As New adamClass
    'Protected WithEvents ltlAlert As Literal
    Dim StrSql As String
    Dim ds As DataSet
    Dim nRet As Integer


    Dim item As ListItem


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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then

                txb_yy.Text = Format(Today, "yyyy")
                txb_mm.Text = Format(Today, "MM")

                StrSql = " Select top 1 perf_createddate from perf_details "
                StrSql &= " where perf_deleted is null and perf_hist_id='" & Request("hid") & "' order by perf_createddate"
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If .Rows.Count > 0 Then
                        If Not IsDBNull(.Rows(0).Item(0)) Then
                            txb_yy.Text = Format(.Rows(0).Item(0), "yyyy")
                            txb_mm.Text = Format(.Rows(0).Item(0), "MM")
                        End If
                    End If
                End With
                ds.Reset()

                dd_dept.SelectedIndex = 0
                item = New ListItem("--")
                item.Value = 0
                dd_dept.Items.Add(item)

                StrSql = "SELECT departmentID,name From departments where isnull(active,0)=1 and isnull(isSalary,0)=1 order by departmentID "
                ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)
                With ds.Tables(0)
                    If (.Rows.Count > 0) Then
                        Dim i As Integer
                        For i = 0 To .Rows.Count - 1
                            item = New ListItem(.Rows(i).Item(1))
                            item.Value = Convert.ToInt32(.Rows(i).Item(0))
                            dd_dept.Items.Add(item)
                        Next
                    End If
                End With
                ds.Reset()

                BindData()
            End If
        End Sub

        Function createSQL() As String
            Dim ct As Decimal = 0
            StrSql = "select perf_rate from tcpc0.dbo.perf_rate where perf_type=N'" & Session("PlantCode") & "'"
            ct = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, StrSql)

            If Not IsNumeric(txb_yy.Text) Then
                txb_yy.Text = Format(Today, "yyyy")
            End If
            If Not IsNumeric(txb_mm.Text) Then
                txb_mm.Text = Format(Today, "MM")
            End If

            createSQL = "select * from ( "
            createSQL &= " Select case when a.perf_userid is null then e.perf_userid else a.perf_userid end as perf_userid "
            createSQL &= " ,case when a.perf_userno is null then e.perf_userno else a.perf_userno end as perf_userno "
            createSQL &= " ,case when a.perf_uname is null then e.perf_uname else a.perf_uname end  as perf_username "
            createSQL &= " ,case when e.perf_dept is null then ( SELECT TOP 1 d.perf_dept FROM perf_details d WHERE d.perf_userid = a.perf_userid order by d.perf_detail_id desc ) else e.perf_dept end as perf_dept"
            createSQL &= " ,case when a.mark is null then 0 else a.mark end  as mark "
            createSQL &= " ,case when isnull(e.perf_rate,0) = 0 then 0 - isnull(a.mark,0) * " & ct & " when (isnull(e.perf_rate,0)>0 and isnull(e.perf_rate,0)<100) then 0 - isnull(a.mark,0)*isnull(e.perf_rate,0) else (isnull(e.perf_rate,0)- 100 - isnull(a.mark,0)) * 0.3 end as perf_rate ,case when isnull(e.perf_rate,0)<100 then N'元' else '%' end as status"
            createSQL &= " from (Select perf_userid,perf_userno,perf_uname,  sum(isnull(perf_mark,0)) as mark "
            createSQL &= " from perf_details "
            createSQL &= " where perf_deleted is null and perf_hist_id='" & Request("hid") & "' "
            createSQL &= " group by perf_userid,perf_userno,perf_uname  ) as a "
            createSQL &= " full outer join perf_employee e on e.perf_userid = a.perf_userid  "
            createSQL &= " ) b "
            If dd_dept.SelectedIndex > 0 Then
                createSQL &= " where perf_dept=N'" & dd_dept.SelectedItem.Text.Trim() & "' "
            End If
            createSQL &= " order by perf_dept ,perf_userno " 
        End Function

    Sub BindData()
       
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, createSQL())

        Dim total As Integer

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("perf_no", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("perf_name", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("perf_dept", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("perf_mark", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("perf_fine", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("user_id", System.Type.GetType("System.Int32")))

        Dim drow As DataRow
        With ds.Tables(0)
            If .Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To .Rows.Count - 1

                    drow = dtl.NewRow()
                    drow.Item("perf_no") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("perf_name") = .Rows(i).Item(2).ToString().Trim()
                    drow.Item("perf_dept") = .Rows(i).Item(3).ToString().Trim()
                    drow.Item("perf_mark") = Format(.Rows(i).Item(4), "##0.####")
                    drow.Item("perf_fine") = Format(.Rows(i).Item(5), "##0.####") & .Rows(i).Item(6).ToString()
                    drow.Item("user_id") = .Rows(i).Item(0)

                    dtl.Rows.Add(drow)
                    total = total + 1

                Next
            End If
        End With
        ds.Reset()

        Dim dvw As DataView
        dvw = New DataView(dtl)

        Datagrid1.DataSource = dvw
        Datagrid1.DataBind()

    End Sub
    Private Sub btn_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_list.Click
        BindData()
    End Sub
    Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click 
            Dim EXHeader As String = txb_yy.Text & "年" & txb_mm.Text & "月 考评奖罚率报表  导出日期：" & Format(DateTime.Today, "yyyy-MM-dd")
            Dim EXTitle As String = "<b>工号</b>~^<b>姓名</b>~^160^<b>部门</b>~^<b>本月累计</b>~^150^<b>本月奖罚</b>~^<b>奖罚率</b>~^"
            Dim ExSql As String = createSQL()
            Me.ExportExcel(chk.dsnx, EXHeader, EXTitle, ExSql, False)

        End Sub
    Private Sub btn_back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_back.Click
        Response.Redirect("perf_hist_list.aspx?hid=" & Request("hid"))
    End Sub
End Class

End Namespace













