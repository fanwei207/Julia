Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Web.Mail


Namespace tcpc


    Partial Class wo_order_attach
        Inherits System.Web.UI.Page
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim StrSql As String
        Dim ds As DataSet
        Dim ret As Integer
        Protected WithEvents File1 As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents panel1 As System.Web.UI.WebControls.Panel


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

                If Request("id") <> "" Then
                    Dim reader1 As SqlDataReader
                    StrSql = " select woo_nbr,woo_req from wo_order"
                    StrSql &= " where deletedBy is null "
                    StrSql &= " and woo_id ='" & Request("id") & "' "
                    reader1 = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
                    With reader1.Read()
                        lbl_nbr.Text = reader1(0).ToString()
                        lbl_dispcontent.Text = reader1(1).ToString()
                    End With
                    reader1.Close()

                End If

                BindAttend()

                If Datagrid1.Items.Count <= 0 Then
                    Datagrid1.Visible = False
                Else
                    Datagrid1.Visible = True
                End If

            End If
        End Sub

        Sub BindAttend()
            StrSql = "select att_id,att_nbr,att_name,createdName,createdDate from wo_attach where att_nbr = '" & lbl_nbr.Text & "' "

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, StrSql)

            Dim total As Integer = 0
            Dim dtl As New DataTable
            dtl.Columns.Add(New DataColumn("attid", System.Type.GetType("System.Int32")))
            dtl.Columns.Add(New DataColumn("docid", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("attname", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("attUser", System.Type.GetType("System.String")))
            dtl.Columns.Add(New DataColumn("attdate", System.Type.GetType("System.String")))
            Dim drow As DataRow
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        drow = dtl.NewRow()
                        drow.Item("attid") = .Rows(i).Item(0).ToString().Trim()
                        drow.Item("docid") = .Rows(i).Item(1).ToString().Trim()
                        drow.Item("attname") = .Rows(i).Item(2).ToString().Trim()
                        drow.Item("attuser") = .Rows(i).Item(3).ToString().Trim()
                        If IsDBNull(.Rows(i).Item(4)) = False Then
                            drow.Item("attdate") = Format(.Rows(i).Item(4), "yy-MM-dd")
                        Else
                            drow.Item("attdate") = ""
                        End If

                        dtl.Rows.Add(drow)
                    Next
                End If
            End With
            Dim dvw As DataView
            dvw = New DataView(dtl)
            Datagrid1.DataSource = dvw
            Datagrid1.DataBind()
            ds.Reset()
            If Datagrid1.Items.Count <= 0 Then
                Datagrid1.Visible = False
            Else
                Datagrid1.Visible = True
            End If

        End Sub


        Private Sub btn_input_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_input.Click
            Dim fname As String = ""
            Dim intLastBackslash As Integer


            If filename.Value.Trim.Length <= 0 Then
                ltlAlert.Text = "alert('请选择上传文件！')"
                Return
            End If


            fname = filename.PostedFile.FileName
            intLastBackslash = fname.LastIndexOf("\")
            fname = fname.Substring(intLastBackslash + 1)
            If (fname.Trim().Length <= 0) Then
                ltlAlert.Text = "alert('请选择上传文件.')"
                Return
            End If


            Dim imgdatastream As Stream
            Dim imgdatalen As Integer
            Dim imgtype As String
            Dim imgdata() As Byte

            imgdatastream = filename.PostedFile.InputStream
            imgdatalen = filename.PostedFile.ContentLength
            imgtype = filename.PostedFile.ContentType

            ReDim imgdata(imgdatalen)

            imgdatastream.Read(imgdata, 0, imgdatalen)

            StrSql = "wo_InsertAttachTemp"

            Dim params(5) As SqlParameter
            params(0) = New SqlParameter("@fname", fname)
            params(1) = New SqlParameter("@imgdata", imgdata)
            params(2) = New SqlParameter("@imgtype", imgtype)
            params(3) = New SqlParameter("@uID", Session("uID"))
            params(4) = New SqlParameter("@username", Session("uName"))
            params(5) = New SqlParameter("@nbr", lbl_nbr.Text)

            ret = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.StoredProcedure, StrSql, params)
            If ret < 0 Then
                ltlAlert.Text = "alert('上传失败')"
                Exit Sub
            End If

            BindAttend()
        End Sub

        Private Sub datagrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Datagrid1.ItemCommand

            If e.CommandName.CompareTo("docview") = 0 Then
                ltlAlert.Text = "var w=window.open('wo_docview.aspx?attid=" & e.Item.Cells(0).Text.Trim() & "','docitem','menubar=No,scrollbars = yes,resizable=yes,width=500,height=500,top=200,left=300'); w.focus();"
            ElseIf e.CommandName.CompareTo("docdelete") = 0 Then
                If e.Item.Cells(3).Text.Trim() <> Session("uName") Then
                    ltlAlert.Text = "alert('只能删除本次上传附件！')"
                    Exit Sub
                Else
                    StrSql = "delete from wo_attach where att_id = '" & e.Item.Cells(0).Text.Trim() & "' and createdName=N'" & Session("uName") & "'"
                    SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, StrSql)

                End If

            End If
            BindAttend()

        End Sub


        Private Sub btn_back_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back.Click
            ltlAlert.Text = "window.close();"
        End Sub

        
    End Class

End Namespace
