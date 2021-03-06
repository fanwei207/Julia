Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class sickleaveinput
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
                If Request("sdate") = "" Then
                    name2value.Text = Format(DateTime.Now, "yyyy-MM-dd")
                Else
                    name2value.Text = Request("sdate")
                End If

                datebind(0)
                Uid.Text = ""
            End If
        End Sub

        Sub datebind(ByVal temp As Integer)
            Dim i As Integer
            strSql = " Select s.id,s.creatby,u.userNo,s.username,s.startdate,s.enddate,DATEDIFF([Day],s.startdate,s.enddate)+1 as sickdays,isnull(s.comment,'')as comment,u1.username as uname,s.creatdate From SickLeave s "
            strSql &= " INNER JOIN tcpc0.dbo.users u ON u.userID=s.userCode "
            strSql &= " INNER JOIN tcpc0.dbo.users u1 ON u1.userID=s.creatby "
            strSql &= " Where s.startdate >= '" & name2value.Text & " ' "
            'strSql &= " and u.isTemp='" & Session("temp") & "'"
            If Textbox1.Text <> "" Then
                strSql &= " and s.enddate <='" & Textbox1.Text & "'"
            End If

            'If Session("uRole") > 1 Then
            '    strSql &= " and ( s.creatby=" & Session("uID") & " or s.creatby in (select worker From Manager_Worker where manager='" & Session("uID") & "')) "
            'End If

            If temp > 0 Then
                If workerNoSearch.Text.Trim() <> "" Then
                    strSql &= " and cast(u.userNo as varchar)='" & workerNoSearch.Text.Trim & "'"
                End If
                If workerNameSearch.Text.Trim() <> "" Then
                    strSql &= " and lower(s.username) like N'%" & workerNameSearch.Text.Trim.ToLower() & "%'"
                End If
                strSql &= " and u.plantCode='" & Session("PlantCode") & "'"
            End If
            strSql &= " order by s.id desc"
            'Response.Write(strSql)
            'Exit Sub
            Session("EXTitle") = "<b>工号</b>~^<b>姓名</b>~^<b>起始日期</b>~^<b>结束日期</b>~^<b>天数</b>~^120^<b>备注</b>~^<b>录入员</b>~^<b>录入日期</b>~^"
            Session("EXSQL") = strSql
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, strSql)

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("Date", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userID", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("starttime", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("endtime", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("remark", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("sickdays", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("inputer", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("inputdate", System.Type.GetType("System.String")))
            dt.Columns.Add(New DataColumn("ID", System.Type.GetType("System.Int32")))
            dt.Columns.Add(New DataColumn("inputID", System.Type.GetType("System.Int32")))
            'dt.Columns.Add(New DataColumn("middate", System.Type.GetType("System.String")))
            With ds.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim dr1 As DataRow

                    For i = 0 To .Rows.Count - 1


                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = i + 1
                        dr1.Item("userID") = .Rows(i).Item("userNo")
                        dr1.Item("userName") = .Rows(i).Item("username")
                        dr1.Item("starttime") = String.Format("{0:yyyy-MM-dd}", .Rows(i).Item("startdate"))
                        dr1.Item("endtime") = String.Format("{0:yyyy-MM-dd}", .Rows(i).Item("enddate"))
                        dr1.Item("remark") = .Rows(i).Item("comment")
                        dr1.Item("ID") = .Rows(i).Item("id")
                        dr1.Item("inputID") = .Rows(i).Item("creatby")
                        dr1.Item("sickdays") = .Rows(i).Item("sickdays")
                        dr1.Item("inputer") = .Rows(i).Item("uname")
                        dr1.Item("inputdate") = String.Format("{0:yyyy-MM-dd}", .Rows(i).Item("creatdate"))

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

        End Sub

        Private Sub dgreturnDetail_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
            If e.Item.ItemType = ListItemType.EditItem Then

                CType(e.Item.Cells(3).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(80)
                CType(e.Item.Cells(3).Controls(0), TextBox).CssClass = "SmallTextBox Date"

                CType(e.Item.Cells(4).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(80)
                CType(e.Item.Cells(4).Controls(0), TextBox).CssClass = "SmallTextBox Date"

                CType(e.Item.Cells(5).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(60)
                CType(e.Item.Cells(5).Controls(0), TextBox).CssClass = "SmallTextBox Date"

                CType(e.Item.Cells(6).Controls(0), TextBox).Width = System.Web.UI.WebControls.Unit.Pixel(300)
                CType(e.Item.Cells(6).Controls(0), TextBox).CssClass = "SmallTextBox Date"
            End If
        End Sub

        Public Sub Edit_edit(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = e.Item.ItemIndex
            datebind(1)
        End Sub

        Public Sub Edit_cancel(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
            DataGrid1.EditItemIndex = -1
            datebind(1)
        End Sub

        Public Sub Edit_delete(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand

            strSql = " Delete From SickLeave Where id =" & e.Item.Cells(11).Text.Trim()
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            datebind(1)
        End Sub
        Public Sub Edit_update(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand


            Dim sd As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text
            Dim ed As String = CType(e.Item.Cells(4).Controls(0), TextBox).Text
            Dim pp As String = CType(e.Item.Cells(6).Controls(0), TextBox).Text
            Dim mm As Integer
            If IsDate(sd) = False Then
                ltlAlert.Text = "alert('起始日期不正确!');"
                Exit Sub
            End If
            If IsDate(ed) = False Then
                ltlAlert.Text = "alert('结束日期不正确!');"
                Exit Sub
            End If


            strSql = " Update  SickLeave SET startdate='" & sd.Trim() & "',enddate='" & ed.Trim() & "',comment=N'" & chk.sqlEncode(pp.Trim()) & "',flag='0',sickdays='0',num='0' "
            strSql &= " Where id =" & e.Item.Cells(11).Text.Trim()
            'Response.Write(strSql)
            'Exit Sub
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            DataGrid1.EditItemIndex = -1
            datebind(1)
        End Sub

        Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            datebind(1)
        End Sub

        Sub BtnSave_click(ByVal sender As Object, ByVal e As System.EventArgs)

            If number.Text.Trim() = "" Then
                ltlAlert.Text = "alert('工号不能为空！');Form1.number.focus();"
                Exit Sub
            End If
            If Textbox1.Text.Trim() = "" Then
                ltlAlert.Text = "alert('结束日期不能为空！');"
                Exit Sub
            End If

            strSql = "insert into SickLeave (usercode,username,startdate,enddate,creatby,creatdate,organizationID,flag,num,sickdays "
            If comment.Text.Trim() <> "" Then
                strSql &= " ,comment) Values ('" & Uid.Text.Trim() & "',N'" & name.Text.Trim() & "','" & name2value.Text & "','" & Textbox1.Text & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','0','0','" & DateDiff(DateInterval.Day, CDate(name2value.Text), CDate(Textbox1.Text)) + 1 & "',N'" & chk.sqlEncode(comment.Text) & "')"
                'strSql &= " ,comment) Values ('" & number.Text.Trim() & "',N'" & name.Text.Trim() & "','" & name2value.Text & "','" & textbox1.Text & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','0',,'0','0',N'" & comment.Text & "')"
            Else
                strSql &= " ) Values ('" & Uid.Text.Trim() & "',N'" & name.Text.Trim() & "','" & name2value.Text & "','" & Textbox1.Text & "','" & Session("uID") & "','" & DateTime.Now.ToString() & "','" & Session("orgID") & "','0','0','" & DateDiff(DateInterval.Day, CDate(name2value.Text), CDate(Textbox1.Text)) + 1 & "')"
            End If
            'Response.Write(strSql)
            'Exit Sub
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, strSql)
            comment.Text = ""
            name.Text = ""
            Uid.Text = ""
            number.Text = ""
            datebind(0)
        End Sub

        Sub searchRecord(ByVal s As Object, ByVal e As System.EventArgs)
            datebind(1)
        End Sub
        Sub namevalue_change(ByVal sender As Object, ByVal e As System.EventArgs)
            If number.Text.Trim() <> "" Then
                Dim exitFlag As Boolean = False
                strSql = " SELECT userName,leaveDate,userID " _
                    & " FROM tcpc0.dbo.users " _
                    & " WHERE cast(userNo as varchar)='" & number.Text.Trim() & "'"
                'strSql &= " and isTemp='" & Session("temp") & "'"
                strSql &= " and plantCode='" & Session("PlantCode") & "'"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, strSql)
                While reader.Read
                    exitFlag = True

                    If reader("leaveDate").ToString() <> "" Then
                        If DateAdd(DateInterval.Month, 2, CDate(reader("leaveDate"))) < DateTime.Now Then
                            ltlAlert.Text = "alert('此员工已离职！');Form1.number.focus();"
                            number.Text = ""
                            name.Text = ""
                            Uid.Text = ""
                            Exit Sub
                        Else
                            ltlAlert.Text = "alert('此员工属于离职员工！');Form1.comment.focus();"
                        End If
                    Else
                        ltlAlert.Text = "Form1.comment.focus();"
                    End If
                    name.Text = reader(0)
                    Uid.Text = reader(2)
                    'ltlAlert.Text = "Form1.comment.focus();"
                End While
                reader.Close()
                If exitFlag = False Then
                    ltlAlert.Text = "alert('工号不存在！');Form1.number.focus();"
                    number.Text = ""
                    name.Text = ""
                    Uid.Text = ""
                End If
            End If
        End Sub
    End Class

End Namespace
