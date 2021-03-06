Imports adamFuncs
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Odbc

Namespace tcpc
    Partial Class qad_itemsearch
        Inherits BasePage 
        Public chk As New adamClass
        'Protected WithEvents ltlAlert As Literal
        Dim nRet As Integer
        Dim strSql As String
        Dim ds As DataSet
        Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel

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
                BindItemData()
            End If
        End Sub

        Sub BindItemData()
            strSql = " select Top 100 id, qad, oldcode, [status], [desc] = desc1 + desc2 from qaddoc.dbo.qad_items where qad is not null"
            If txbqad.Text.Trim <> "" Then
                strSql &= " and qad like '%" & txbqad.Text.Trim & "%' "
            End If
            If txbold.Text.Trim <> "" Then
                strSql &= " and oldcode like '%" & txbold.Text.Trim & "%' "
            End If
            If txbdesc.Text.Trim <> "" Then
                strSql &= " and desc1 + desc2 like N'%" & txbdesc.Text.Trim & "%' "
            End If
            If txbstatus.Text.Trim <> "" Then
                strSql &= " and status = N'" & txbstatus.Text.Trim & "'"
            End If

            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
            'Response.Write(strSql)
            'Exit Sub

            Label1.Text = "Total: " & ds.Tables(0).Rows.Count.ToString()

            Try
                DgItem.DataSource = ds
                DgItem.DataBind()
            Catch
            End Try
        End Sub

        Private Sub DgItem_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DgItem.PageIndexChanged
            DgItem.CurrentPageIndex = e.NewPageIndex
            DgItem.SelectedIndex = -1
            BindItemData()
        End Sub

        Private Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
            BindItemData()
            txbqad.Text = ""
            txbstatus.Text = ""
            txbdesc.Text = ""
        End Sub

        Private Sub DgItem_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgItem.ItemCommand
            If (e.CommandName.CompareTo("assdoc") = 0) Then  
                Response.Redirect("/qaddoc/qad_bomviewdoc.aspx?url=qad_itemsearch.aspx&part=" & e.Item.Cells(1).Text.Trim) 
            ElseIf (e.CommandName.CompareTo("assdocNew") = 0) Then
                Response.Redirect("/qaddoc/qad_bomviewdocNew.aspx?url=qad_itemsearch.aspx&part=" & e.Item.Cells(1).Text.Trim)
            ElseIf (e.CommandName.CompareTo("upddoc") = 0) Then
                Dim conn As OdbcConnection
                Dim comm As OdbcCommand
                Dim dr As OdbcDataReader
                Dim connectionString As String = ConfigurationManager.AppSettings("SqlConn.Conn9")
                Dim strsql As String = "select distinct pt_part,pt_desc1,pt_desc2,pt_status,pt_article from PUB.pt_mstr where pt_part = '" & e.Item.Cells(1).Text.Trim & "' with (nolock) "
                
                Try
                    conn = New OdbcConnection(connectionString)
                    conn.Open()
                    comm = New OdbcCommand(strsql, conn)
                    dr = comm.ExecuteReader()
                    While (dr.Read()) 
                        strsql = "Update qaddoc.dbo.qad_items set desc1 =N'" & dr.GetValue(1) & "',desc2=N'" & dr.GetValue(2) & "',status=N'" & dr.GetValue(3) & "',article=N'" & dr.GetValue(4) & "' where qad='" & e.Item.Cells(1).Text.Trim & "'"
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strsql)

                        ltlAlert.Text = "alert('The item description has been updated.');"
                    End While
                    dr.Close()
                    conn.Close()
                    comm.Dispose()

                Catch oe As OdbcException
                    Response.Write(oe.Message)
                Finally
                    If conn.State = System.Data.ConnectionState.Open Then
                        conn.Close()
                    End If
                  
                End Try

                conn.Dispose()

                BindItemData()

            End If
        End Sub
    End Class

End Namespace
