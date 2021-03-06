'!*******************************************************************************!
'* @@ NAME				:	CoatDetail.aspx.vb
'* @@ AUTHOR			:	Xin Bao
'*
'* @@ DESCRIPTION		:	Code behind file for CoatDetail.aspx
'*					
'* @@ REQUIRED OBJECTS	:	NA
'*
'* @@ INCLUDE FILES		:	adamFuncs.dll and Microsoft.ApplicationBlocks.Data.dll  in '/bin' : Security Functions
'*
'* @@ TEMPLATE DATE		:	April 13 2005
'*
'!-------------------------------------------------------------------------------! 
'* @@ HISTORY			:	NA
'*	
'!*******************************************************************************!
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class CoatDetail
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents ltlAlert As Literal
        Dim strSql As String
        Dim reader As SqlDataReader
        Dim chk As New adamClass
        Dim ds As DataSet

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not IsPostBack Then
                SaleBind(0)
            End If
        End Sub

        Sub SaleBind(ByVal temp As Integer)
            Dim ltotal As Integer = 0
            Dim stotal As Integer = 0
            Dim wtotal As Integer = 0

            strSql = " select u.userNo,uu.uniform,uu.uniformDate,d.name,u.userName,userUniformDetailID,uu.num From  User_UniformDetail uu inner join tcpc0.dbo.users u on u.userID=uu.userID "
            strSql &= " left outer join Departments d ON d.departmentID=u.departmentID where uu.userID=" & Request("uid")
            strSql &= " order by userUniformDetailID desc "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)
            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("name", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("uniform", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("departmentName", System.Type.GetType("System.String")))
            'dt.Columns.Add(New DataColumn("workshop", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("uniformdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("type", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("uniformnum", System.Type.GetType("System.Int32")))
            Dim i As Integer
            With ds.Tables(0)
                If (.Rows.Count > 0) Then

                    Dim dr1 As DataRow
                    For i = 0 To .Rows.Count - 1
                        dr1 = dt.NewRow()
                        dr1.Item("ID") = .Rows(i).Item(5)
                        dr1.Item("userID") = .Rows(i).Item(0)
                        dr1.Item("name") = .Rows(i).Item(4).ToString().Trim()
                        dr1.Item("departmentName") = .Rows(i).Item(3).ToString().Trim()
                        dr1.Item("uniformdate") = Format(.Rows(i).Item(2), "yyyy-MM-dd")
                        dr1.Item("type") = .Rows(i).Item(1)
                        Select Case .Rows(i).Item(1)
                            Case 1
                                dr1.Item("uniform") = "长夹克"
                                ltotal = ltotal + .Rows(i).Item(6)
                            Case 2
                                dr1.Item("uniform") = "短夹克"
                                stotal = stotal + .Rows(i).Item(6)
                            Case 3
                                dr1.Item("uniform") = "白大褂"
                                wtotal = wtotal + .Rows(i).Item(6)
                        End Select
                        dr1.Item("uniformnum") = .Rows(i).Item(6)
                        dt.Rows.Add(dr1)
                    Next
                End If
            End With
            Dim dv As DataView
            dv = New DataView(dt)
            Try
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch
            End Try

            lnum.Text = ltotal.ToString()
            snum.Text = stotal.ToString()
            wnum.Text = wtotal.ToString()
        End Sub

        Sub export_click(ByVal sender As Object, ByVal e As System.EventArgs)
            strSql = " select u.userNo,u.userName,d.name,case When uu.uniform <3 Then case When uu.uniform=2 then N'短夹克' else N'长夹克' end else N'白大褂' end,uu.uniformDate,uu.num From  User_UniformDetail uu inner join tcpc0.dbo.users u on u.userID=uu.userID "
            strSql &= " left outer join Departments d ON d.departmentID=u.departmentID where uu.userID=" & Request("uid")
            strSql &= " order by userUniformDetailID desc "
            Session("EXTitle1") = "<b>工号</b>~^<b>姓名</b>~^120^<b>部门</b>~^<b>服装</b>~^<b>日期</b>~^<b>数量</b>~^"
            Session("EXSQL1") = strSql

            ltlAlert.Text = "window.open('/public/exportExcel1.aspx', '_blank');"
        End Sub
    End Class

End Namespace
