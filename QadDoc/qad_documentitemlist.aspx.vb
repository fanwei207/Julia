Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
    Partial Class qad_documentitemlist
        Inherits BasePage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        'Protected WithEvents ltlAlert As Literal


        Public chk As New adamClass
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents Button1 As System.Web.UI.WebControls.Button


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
                Panel1.Visible = False
                selectall.Visible = False
                cancelall.Visible = False
                btnAdd2.Visible = False
 
            End If
        End Sub

        Private Sub BindData()
            Dim strSQL As String
            Dim ds As DataSet
            strSQL = "select di.id,di.qad,desc0=ISNULL(item_qad_desc1,'')+ISNULL(item_qad_desc2,'') From qaddoc.dbo.DocumentItem di" _
                   & " LEFT JOIN (" _
                   & " SELECT ROW_NUMBER() OVER (PARTITION BY item_qad ORDER BY status, itemVersion DESC,itemSubVersion DESC) AS rowNumber,item_qad,item_qad_desc1,item_qad_desc2 FROM tcpc0.dbo.Items) i" _
                   & " ON di.qad COLLATE Chinese_PRC_CI_AS=i.item_qad COLLATE Chinese_PRC_CI_AS AND i.rowNumber=1 where docid=" & Request("id") & " order by qad"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSQL)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("desc", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("ID") = .Rows(i).Item(0).ToString().Trim()
                        dr1.Item("Name") = .Rows(i).Item(1).ToString().Trim()
                        dr1.Item("desc") = .Rows(i).Item(2).ToString().Trim()
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)

            Try
                dv.Sort = "gsort Asc"
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try
        End Sub

        Public Sub DeleteBtn(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
            If (e.CommandName.CompareTo("DeleteBtn") = 0) Then
                Dim strsql1 As String
                'strsql1 = "select count(*) from QadDoc.dbo.DocumentItem where id =" & e.Item.Cells(4).Text() & " and DATEDIFF(hh,isnull(createddate,'1900-1-1'),GETDATE())<=1"
                'If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql1) >= 1 Then
                '    strsql1 = "delete from qaddoc.dbo.DocumentItem where id = " & e.Item.Cells(4).Text()
                '    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql1)
                '    DataGrid1.EditItemIndex = -1
                '    BindData()
                '    Exit Sub
                'End If
                Dim params1(3) As SqlParameter
                params1(0) = New SqlParameter("@docId", Request.QueryString("id"))
                params1(1) = New SqlParameter("@part", e.Item.Cells(1).Text())
                params1(2) = New SqlParameter("@lockPart", SqlDbType.VarChar, 500)
                params1(2).Direction = ParameterDirection.Output
                If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, "qaddoc.dbo.sp_qad_checkLockUL", params1) = 1 Then
                    ltlAlert.Text = "alert('The part is locked by " & params1(2).Value.ToString() & "，can not delete！');"
                    Exit Sub
                End If

                strsql1 = "select count(*) from QadDoc.dbo.DocumentItemApprove where appvResult is null and docitemid =" & e.Item.Cells(4).Text()

                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql1) >= 1 Then
                    ltlAlert.Text = "alert('正在删除审批中,不可再次删除');"
                    Exit Sub
                End If

                strsql1 = "select count(*) from QadDoc.dbo.DocumentItem where id =" & e.Item.Cells(4).Text() & " and DATEDIFF(hh,isnull(createddate,'1900-1-1'),GETDATE())<=1"
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql1) >= 1 Then
                    strsql1 = " UPDATE det SET det.Mold_lock = 1,det.Mold_lockdate = GETDATE()	FROM qaddoc.dbo.DocumentItem item	LEFT JOIN  qadplan..Mold_qad   qad ON item.qad collate chinese_prc_ci_as = qad.Mold_qad collate chinese_prc_ci_as 	LEFT JOIN qadplan..Mold_det  det ON det.Mold_mstrID = qad.Mold_mstrID	WHERE     item.id = " & e.Item.Cells(4).Text()
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql1)

                    strsql1 = "INSERT INTO qadplan..mold_doc ( MoldDoc_ID,Mold_DetID,doc_ID,updateDocBy,updateDocName,updateDocDate,isLock,updateDocType) SELECT NEWID(), det.Mold_ID," & Request.QueryString("id") & ",doc.modifiedBy,doc.modifiedname,GETDATE(),1,'U' FROM  QadDoc.dbo.Documents doc   LEFT JOIN  qadplan..Mold_qad   qad ON qad.Mold_qad = '" & e.Item.Cells(1).Text() & "'    LEFT JOIN qadplan..Mold_det  det ON det.Mold_mstrID = qad.Mold_mstrID WHERE     det.Mold_ID IS NOT NULL and   doc.id = " & Request.QueryString("id")
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql1)


                    strsql1 = "delete from qaddoc.dbo.DocumentItem where id = " & e.Item.Cells(4).Text()
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql1)

                    DataGrid1.EditItemIndex = -1
                    BindData()
                    Exit Sub
                End If

                Dim strSQL As String
                strSQL = "QadDoc.dbo.sp_qad_throwDocumentItemAppvForDel"
                Dim params(2) As SqlParameter
                params(0) = New SqlParameter("@docitemid", e.Item.Cells(4).Text())
                params(1) = New SqlParameter("@userId", Session("uID"))
                Dim result As String = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strSQL, params).ToString()
                DataGrid1.EditItemIndex = -1
                BindData()
                If result = "1" Then
                    ltlAlert.Text = "alert('进入删除审批中，请等待审批');"
                End If

            End If
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Private Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
            Dim strsql As String
            If txbadd.Text.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('Item is required');"
                Exit Sub
            End If
            'strsql = "select count(id) from qaddoc.dbo.qad_items where qad=N'" & txbadd.Text.Trim & "' "
            'strsql = "select count(*) from qad_data.dbo.pt_mstr where pt_part=N'" & txbadd.Text.Trim & "' "
            strsql = "select count(*) from tcpc0.dbo.items where item_qad=N'" & txbadd.Text.Trim & "' and status<>2"

            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) <= 0 Then
                ltlAlert.Text = "alert('Item is not exist');"
                Exit Sub
            End If

            strsql = "select count(id) from qaddoc.dbo.DocumentItem where docid = " & Request.QueryString("id").ToString() & " and qad = " & txbadd.Text.Trim()

            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) >= 1 Then
                ltlAlert.Text = "alert('Item can not be repeat');"
                Exit Sub
            End If

            Dim params1(3) As SqlParameter
            params1(0) = New SqlParameter("@docId", Request.QueryString("id"))
            params1(1) = New SqlParameter("@part", txbadd.Text.Trim)
            params1(2) = New SqlParameter("@lockPart", SqlDbType.VarChar, 500)
            params1(2).Direction = ParameterDirection.Output
            If SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, "qaddoc.dbo.sp_qad_checkLockULForAdd", params1) = 1 Then
                ltlAlert.Text = "alert('The part is locked by" & params1(2).Value.ToString() & ",can not associate the document！');"
                Exit Sub
            End If

            strsql = "select count(id) from qaddoc.dbo.DocumentItem_Bak where docid = " & Request.QueryString("id").ToString()

            If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) >= 1 Then
                strsql = "select count(*) from QadDoc.dbo.DocumentItemApprove where docid =" & Request.QueryString("id").ToString() & " and appvResult is null and qad is not null"

                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) >= 1 Then
                    ltlAlert.Text = "alert('正在升级审批中');"
                    Exit Sub
                Else
                    ltlAlert.Text = "alert('审批拒绝');"
                    Exit Sub
                End If

            End If

            Dim params(4) As SqlParameter
            Dim cnt As Integer
            strsql = "qaddoc.dbo.qad_documentitemAdd"
            params(0) = New SqlParameter("@docID", Request.QueryString("id"))
            params(1) = New SqlParameter("@qad", txbadd.Text.Trim)
            params(2) = New SqlParameter("@uID", Session("uID"))
            params(3) = New SqlParameter("@message", SqlDbType.NVarChar, 100)
            params(3).Direction = ParameterDirection.Output
            cnt = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strsql, params)
            Dim message As String = params(3).Value.ToString()
            'Response.Write(cnt)
            If cnt < 0 Then
                ltlAlert.Text = "alert('It's failure.');"
                Exit Sub
            ElseIf message <> "" Then
                ltlAlert.Text = "alert('" & message & "');"
                Exit Sub
            End If
            txbadd.Text = ""
            DataGrid1.CurrentPageIndex = 0

            BindData()
        End Sub

        Private Sub btnfind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfind.Click
            Panel1.Visible = False
            selectall.Visible = False
            cancelall.Visible = False
            btnAdd2.Visible = False
            selectall.Checked = False
            cancelall.Checked = False
            Dim ds As DataSet
            Dim strsql As String
            'strsql = "SELECT qad, qad + '/' + ISNULL(oldcode, '') + '/' + ISNULL(desc1, '') + ISNULL(desc2, '') + '/'+ isnull(article,'')  as description from qaddoc.dbo.qad_items where qad like '%" & txbadd.Text & "%' order by qad asc"
            strsql = "SELECT pt_part as qad, pt_part + '/' + ISNULL(pt_desc1, '') + ISNULL(pt_desc2, '') + '/'+ isnull(pt_article,'')  as description from qad_data.dbo.pt_mstr where pt_part like '%" & txbadd.Text & "%' order by pt_part asc"
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strsql)
            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("qad", System.Type.GetType("System.String")))
            'dt.Columns.Add(New DataColumn("oldcode", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("desc", System.Type.GetType("System.String")))
            dt = ds.Tables(0)
            chkqad.DataSource = dt
            chkqad.DataTextField = "description"
            chkqad.DataValueField = "qad"
            chkqad.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                Panel1.Visible = True
                selectall.Visible = True
                cancelall.Visible = True
                btnAdd2.Visible = True
            End If
        End Sub

        Private Sub selectall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectall.CheckedChanged
            If selectall.Checked Then
                Dim i As Integer
                For i = 0 To chkqad.Items.Count - 1
                    chkqad.Items(i).Selected = True
                Next
            End If
        End Sub

        Private Sub cancelall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelall.CheckedChanged
            If cancelall.Checked Then
                Dim i As Integer
                For i = 0 To chkqad.Items.Count - 1
                    chkqad.Items(i).Selected = False
                Next
            End If
        End Sub

        Protected Sub btnAdd2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd2.Click
            Dim strsql As String
            Dim i As Integer
            For i = 0 To chkqad.Items.Count - 1

                If chkqad.Items(i).Selected Then
                    Dim params(3) As SqlParameter
                    Dim cnt As Integer
                    strsql = "qaddoc.dbo.qad_documentitemAdd"
                    params(0) = New SqlParameter("@docID", Request.QueryString("id"))
                    params(1) = New SqlParameter("@qad", chkqad.Items(i).Value.ToString())
                    params(2) = New SqlParameter("@uID", Session("uID"))
                    cnt = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, strsql, params)
                    'Response.Write(cnt)
                    If cnt < 0 Then
                        ltlAlert.Text = "alert('It's failure.');"
                        Exit Sub
                    End If
                End If
            Next
            txbadd.Text = ""
            DataGrid1.CurrentPageIndex = 0

            BindData()
        End Sub
    End Class

End Namespace
