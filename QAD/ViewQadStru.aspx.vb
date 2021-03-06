'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-09-21
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   List Item Qad Structure
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc


    Partial Class ViewQadStru
        Inherits BasePage
        Dim chk As New adamClass
        Dim strsql As String
        Dim dst As DataSet
        Dim nRet As Integer

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents BtnTrans As System.Web.UI.WebControls.Button
        'Protected WithEvents ltlAlert As Literal


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ltlAlert.Text = ""
            If Not IsPostBack Then
                clearPartTemp()
                If Request("parent") <> Nothing Then
                    GetPart(Request("parent"), 1)
                    lblProdCode.Text = "父零件为<font size='4pt'>" & Request("parent") & "</font>"
                    BindData()
                End If
            End If
        End Sub

        Sub clearPartTemp()
            strsql = " Delete From Cal_Qad_Stru_tmp Where createdBy='" & Session("uID") & "' And plantID='" & Session("plantCode") & "'"
            SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
        End Sub

        Sub GetPart(ByVal parent As String, ByVal Qty As Decimal, Optional ByVal tag As Integer = 0)
            Dim reader As SqlDataReader
            strsql = " Select parent, child, qty, Isnull(stru_type,''), Isnull(rej_rate,0), Isnull(startdate,''), Isnull(enddate,''), Isnull(processNo,''), Isnull(LT,''), Isnull(posCode,'') From Qad_Stru Where parent='" & parent & "'"

            reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
            While reader.Read()
                strsql = " Select Count(*) From Qad_Stru Where parent='" & reader(1) & "'"
                If SqlHelper.ExecuteScalar(chk.dsn0, CommandType.Text, strsql) <= 0 Then
                    strsql = " Insert Into Cal_Qad_Stru_tmp(parent, child, qty, stru_type, rej_rate, startdate, enddate, processNo, LT, posCode, tag, createdBy, plantID) " _
                           & " Values('" & reader(0) & "','" & reader(1) & "','" & reader(2) & "','" & reader(3) & "','" & reader(4) & "','" & reader(5) & "','" & reader(6) & "','" & reader(7) _
                           & "','" & reader(8) & "','" & reader(9) & "','" & tag & "','" & Session("uID") & "','" & Session("plantCode") & "')"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
                Else
                    strsql = " Insert Into Cal_Qad_Stru_tmp(parent, child, qty, stru_type, rej_rate, startdate, enddate, processNo, LT, posCode, tag, createdBy, plantID) " _
                           & " Values('" & reader(0) & "','" & reader(1) & "','" & reader(2) & "','" & reader(3) & "','" & reader(4) & "','" & reader(5) & "','" & reader(6) & "','" & reader(7) _
                           & "','" & reader(8) & "','" & reader(9) & "','" & tag & "','" & Session("uID") & "','" & Session("plantCode") & "')"
                    SqlHelper.ExecuteNonQuery(chk.dsn0, CommandType.Text, strsql)
                    GetPart(reader(1), reader(2) * Qty, tag + 1)
                End If
            End While
            reader.Close()
        End Sub

        Private Sub BtnReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
            Response.Redirect(chk.urlRand("/Qad/Item_Qad_list.aspx?code=" & Request("code") & "&qad=" & Request("qad") & "&pg=" & Request("pg")), True)
        End Sub

        Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
            dgPart.CurrentPageIndex = 0
            BindData()
        End Sub

        Sub BindData()
            Dim qty As Decimal
            Dim reader As SqlDataReader
            strsql = " Select c.child, c.qty, Isnull(c.stru_type,''), Isnull(c.rej_rate,0), Isnull(c.startdate,''), Isnull(c.enddate,''), Isnull(c.processNo,''), Isnull(c.LT,''), Isnull(c.posCode,''), c.tag, isnull(i.item_qad_desc1,'')+isnull(i.item_qad_desc2,'') From tcpc0.dbo.Cal_Qad_Stru_tmp  c " _
                   & " Inner Join tcpc0.dbo.items i on i.item_qad = c.child " _
                       & " Where c.CreatedBy='" & Session("uID") & "' And c.plantID='" & Session("plantCode") & "'"
            If CheckBox1.Checked = False Then
                strsql = strsql & " and tag = 0 "
            End If
            strsql = strsql & " Order By c.id "
            dst = SqlHelper.ExecuteDataset(chk.dsn0, CommandType.Text, strsql)

            Dim dtlPart As New DataTable
            dtlPart.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("child", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("qty", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("stru", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("rej", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("start", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("end", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("pno", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("lt", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("pos", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("replace", System.Type.GetType("System.String")))
            dtlPart.Columns.Add(New DataColumn("description", System.Type.GetType("System.String")))

            With dst.Tables(0)
                If .Rows.Count > 0 Then
                    Dim i As Integer
                    Dim j As Integer
                    Dim drowPart As DataRow
                    Dim strPos As String = ""
                    For i = 0 To .Rows.Count - 1
                        drowPart = dtlPart.NewRow()
                        drowPart.Item("gsort") = i + 1
                        Dim tag As String = ""
                        For j = 1 To .Rows(i).Item(9)
                            tag = tag & "---"
                        Next
                        drowPart.Item("child") = tag & " " & .Rows(i).Item(0).ToString().Trim()
                        qty = CDec(.Rows(i).Item(1))
                        If qty = CInt(qty) Then
                            drowPart.Item("qty") = CInt(qty)
                        Else
                            drowPart.Item("qty") = qty
                        End If
                        drowPart.Item("stru") = .Rows(i).Item(2).ToString().Trim()
                        drowPart.Item("rej") = .Rows(i).Item(3).ToString().Trim()
                        If Year(.Rows(i).Item(4).ToString().Trim()) = "1900" Then
                            drowPart.Item("start") = ""
                        Else
                            drowPart.Item("start") = .Rows(i).Item(4).ToString().Trim()
                        End If
                        If Year(.Rows(i).Item(5).ToString().Trim()) = "1900" Then
                            drowPart.Item("end") = ""
                        Else
                            drowPart.Item("end") = .Rows(i).Item(5).ToString().Trim()
                        End If
                        drowPart.Item("pno") = .Rows(i).Item(6).ToString().Trim()
                        drowPart.Item("lt") = .Rows(i).Item(7).ToString().Trim()
                        strsql = " Select newchild From Qad_stru_replace Where parent='" & Request("parent") & "' And oldchild='" & .Rows(i).Item(0).ToString().Trim() & "'"
                        reader = SqlHelper.ExecuteReader(chk.dsn0, CommandType.Text, strsql)
                        While reader.Read()
                            strPos &= reader(0) & ","
                        End While
                        reader.Close()
                        If strPos.Trim() <> "" Then
                            drowPart.Item("replace") = strPos.Substring(0, Len(strPos.Trim()) - 1).Trim()
                        Else
                            drowPart.Item("replace") = ""
                        End If
                        drowPart.Item("pos") = .Rows(i).Item(8).ToString().Trim()
                        drowPart.Item("description") = .Rows(i).Item(10).ToString().Trim()
                        dtlPart.Rows.Add(drowPart)
                        strPos = ""
                    Next
                End If
            End With
            Try
                dgPart.DataSource = dtlPart
                dgPart.DataBind()
            Catch
            End Try
        End Sub
    End Class

End Namespace
