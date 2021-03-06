Imports System
Imports adamFuncs
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

    Partial Class workprocedureEdit
        Inherits BasePage
        'Protected WithEvents ltlAlert As Literal
        Dim chk As New adamClass
        Dim item As ListItem
        Dim Query As String
        Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
        Dim reader As SqlDataReader


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents cMsg As System.Web.UI.WebControls.ValidationSummary
        Protected WithEvents rolename As System.Web.UI.WebControls.TextBox
        Protected WithEvents departmentName As System.Web.UI.WebControls.TextBox
        Protected WithEvents PersonnelType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cMsg3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
        Protected WithEvents IDNo As System.Web.UI.WebControls.TextBox
        Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here

            If Not IsPostBack Then
                InitDropDownList()
                Departmentdropdownlist()
                If Request("id") = "" Then
                    Button3.Visible = True
                Else
                    Button2.Visible = True

                    Dim i As Integer
                    Query = "Select w.name,isnull(w.typeID,0),w.guideLine,w.unitPrice, w.lowestDailyWage,isnull(w.deductName,''),w.deductRate,w.deductPrice,isnull(w.newdeductprice,'0'),w.flag,isnull(w.departmentID,0),isnull(w.workshopID,0),isnull(w.wallowance,0),isnull(w.wpercent,0),w.wdate,w.code "
                    Query = Query & " From workprocedure w "
                    Query &= " left outer join departments d on d.departmentID=w.departmentID"
                    Query &= " left outer join workshop wp on wp.id=w.workshopID"
                    Query &= " Where w.id= " & Request("id")
                    'Response.Write(Query)
                    'Exit Sub
                    reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)

                    While (reader.Read())
                        gname.Text = reader(0).ToString()
                        For i = 1 To gcategory.Items.Count - 1
                            If gcategory.Items(i).Value = reader(1) Then
                                gcategory.SelectedIndex = i
                                Exit For
                            End If
                        Next
                        guideline.Text = reader(2).ToString()
                        unitprice.Text = reader(3).ToString()
                        lowest.Text = reader(4).ToString()
                        deduct.Text = reader(5).ToString()
                        deductPrice.Text = reader(7).ToString()
                        deductRate.Text = reader(6).ToString()
                        price.Text = reader(8)
                        If reader(9) = True Then
                            stopused.Checked = True
                        Else
                            stopused.Checked = False
                        End If

                        For i = 1 To department.Items.Count - 1
                            If department.Items(i).Value = reader(10) Then
                                department.SelectedIndex = i
                                Exit For
                            End If
                        Next

                        If reader(11) = 0 Then
                            workshop.Items.Add(New ListItem("--", "0"))
                        Else
                            If reader(10) > 0 Then
                                workshopDropDownList()
                                For i = 1 To workshop.Items.Count - 1
                                    If workshop.Items(i).Value = reader(11) Then
                                        workshop.SelectedIndex = i
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                        wallowance.Text = reader(12)
                        wpercent.Text = reader(13)
                        wdate.Text = reader(14)
                        changedate.Text = reader(14)
                        wcode.Text = reader(15)
                        nprice.Text = reader(8)
                    End While
                    reader.Close()

                End If
            End If
        End Sub
        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            Dim strInt As String
            Dim strFloat As String
            strInt = "^[0-9]+$"
            strFloat = "^[0-9.]+$"
            If gname.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('工序名称不能为空');"
                Exit Sub
            End If
            If guideline.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(guideline.Text.Trim(), strInt, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('指标必须为整数');"
                Exit Sub
            End If
            If unitprice.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(unitprice.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('单价必须为数字');"
                Exit Sub
            End If
            If lowest.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(lowest.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('最低工资必须为数字');"
                Exit Sub
            End If
            If deductRate.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(deductRate.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('扣款率必须为数字');"
                Exit Sub
            End If
            If deductPrice.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(deductPrice.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('扣款金额必须为数字');"
                Exit Sub
            End If
            If wallowance.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(wallowance.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('岗位补贴必须为数字');"
                Exit Sub
            End If
            If wpercent.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(wpercent.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('产量限制必须为数字');"
                Exit Sub
            End If
            If wdate.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('日期不能为空');"
                Exit Sub
            End If
            If wdate.Text.Trim().Length <> 0 Then
                Try
                    Dim dt As DateTime
                    dt = Convert.ToDateTime(wdate.Text.Trim())
                Catch ex As Exception
                    ltlAlert.Text = "alert('日期格式不正确');"
                    Exit Sub
                End Try
            End If



            If department.SelectedValue = 0 Then
                ltlAlert.Text = "alert('请选择部门');"
                Exit Sub
            End If
            Dim ds As DataSet
            Query = " Select id From BaseInfo where  year(workdate)='" & Year(CDate(wdate.Text.Trim)) & "' and  month(workdate)='" & Month(CDate(wdate.Text.Trim)) & "' "
            ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
            If ds.Tables.Count <= 0 Then
                ltlAlert.Text = "alert('所填日期的工资基础信息未维护');"
                Exit Sub
            End If
            'modify
            Dim type As String
            Query = "Select systemcodename from tcpc0.dbo.systemCode where systemcodeid=" & gcategory.SelectedValue
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While (reader.Read())
                type = reader(0)
            End While
            reader.Close()

            Dim strG As String
            Dim strU As String
            Dim strL As String
            Dim stops As String

            If stopused.Checked Then
                stops = "1"
            Else
                stops = "0"
            End If


            If type = "计件" Then
                If guideline.Text.Trim.Length <= 0 Or unitprice.Text.Trim.Length <= 0 Then
                    ltlAlert.Text = "alert('请输入指标和单价');"
                    Exit Sub
                End If
                strG = guideline.Text.Trim
                strU = unitprice.Text.Trim
                strL = "0"
                Query = " Select basicSalary,monthlyAvgDays,basicUnitPrice,dailyHours From BaseInfo where year(workdate)='" & Year(CDate(wdate.Text.Trim)) & "' and  month(workdate)='" & Month(CDate(wdate.Text.Trim)) & "' "
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
                While reader.Read
                    price.Text = (reader(2) / CDec(strG) / reader(3)).ToString()
                    'Exit Sub
                End While
                reader.Close()
                'Response.Write(price.Text & "/" & nprice.Text)
                If Session("PlantCode") <> 2 And Session("PlantCode") <> 3 Then
                    If CDec(nprice.Text) > CDec(price.Text) Then
                        price.Text = nprice.Text
                    End If
                End If

                '    End If
                'End If
            Else
                If lowest.Text.Trim.Length <= 0 Then
                    ltlAlert.Text = "alert('请输入最低工资');"
                    Exit Sub
                End If
                strG = "0"
                strU = "0"
                strL = lowest.Text

                Query = " Select  basicSalary,monthlyAvgDays,basicUnitPrice,dailyHours From BaseInfo where year(workdate)='" & Year(CDate(wdate.Text.Trim)) & "' and  month(workdate)='" & Month(CDate(wdate.Text.Trim)) & "' "
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
                While reader.Read
                    price.Text = reader(2).ToString()
                End While
                reader.Close()
                If Session("PlantCode") <> 2 And Session("PlantCode") <> 3 Then
                    If CDec(nprice.Text) > CDec(price.Text) Then
                        price.Text = nprice.Text
                    End If
                End If
                'End If
                '        End If
            End If

            If Convert.ToDecimal(strU) >= 1000 Then
                ltlAlert.Text = "alert('单价不能大于等于1000');"
                Exit Sub
            End If
            If Convert.ToDecimal(strL) >= 1000 Then
                ltlAlert.Text = "alert('最低工资不能大于等于1000');"
                Exit Sub
            End If

            If DateDiff(DateInterval.Day, CDate(changedate.Text.Trim), CDate(wdate.Text.Trim)) = 0 Then
                Query = " Update workprocedure Set name= N'" & chk.sqlEncode(gname.Text) & "', "
                Query = Query & "typeID='" & gcategory.SelectedValue & "',"
                Query = Query & "guideLine='" & strG & "',"
                Query = Query & "unitPrice='" & strU & "',"
                If deduct.Text.Length > 0 And deductRate.Text.Trim.Length > 0 Then
                    If Convert.ToDecimal(deductRate.Text.Trim) >= 100 Then
                        ltlAlert.Text = "alert('扣款率不能大于等于100');"
                        Exit Sub
                    End If
                    Query = Query & "deductName = N'" & deduct.Text & "',"
                    Query = Query & "deductPrice = '" & deductPrice.Text & "',"
                    Query = Query & "deductRate='" & deductRate.Text & "', "
                End If
                Query = Query & " newdeductprice='" & price.Text.ToString() & "',"
                Query = Query & "lowestDailyWage='" & strL & "' ,"
                Query = Query & " flag='" & stops & "',"
                Query &= " departmentID='" & department.SelectedValue & "',workshopID='" & workshop.SelectedValue & "'"
                Query &= " ,wallowance='" & wallowance.Text.Trim & "',wpercent='" & wpercent.Text & "' "
                Query = Query & " Where id=" & Request("id")
            Else
                Dim last As String = "0"
                Query = " select id,wdate from workprocedure where lastone=1 and code='" & wcode.Text & "'"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
                While reader.Read
                    If DateDiff(DateInterval.Day, reader(1), CDate(wdate.Text.Trim)) < 0 Then
                        last = "1"
                    Else
                        Query = "update workprocedure set lastone=0 where id ='" & reader(0) & "' "
                        SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)
                    End If
                End While
                reader.Close()


                Query = "Insert Into workprocedure (typeID,name,usedTimes,guideLine,unitPrice,lowestDailyWage,deductRate, organizationID,deductName,deductPrice,newdeductprice,flag,departmentID,workshopID,wallowance,wpercent,wdate,code,lastone,newdeductprice1)"
                Query &= " (Select top 1 '" & gcategory.SelectedValue & "',N'" & chk.sqlEncode(gname.Text) & "',0,'" & strG & "','" & strU & "','" & strL & "',"
                If deduct.Text.Trim.Length <= 0 Or deductRate.Text.Trim.Length <= 0 Then
                    Query &= " null,1,null,null,"
                Else
                    If Convert.ToDecimal(deductRate.Text.Trim) >= 100 Then
                        ltlAlert.Text = "alert('扣款率不能大于等于100');"
                        Exit Sub
                    End If
                    Query &= " '" & deductRate.Text & "',1,'" & deduct.Text & "','" & deductPrice.Text & "',"
                End If
                Query &= " '" & price.Text.ToString() & "','" & stops & "','" & department.SelectedValue & "','" & workshop.SelectedValue & "','" & wallowance.Text.Trim & "','" & wpercent.Text.Trim & "','" & wdate.Text.Trim & "','" & wcode.Text & "','" & last & "','" & nprice.Text & "')"
            End If

            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            Query = " Update Indicator set workProcedure=workProcedure+1 "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            Response.Redirect(chk.urlRand("/admin/workprocedure.aspx"))
        End Sub

        Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
            Dim strInt As String
            Dim strFloat As String
            strInt = "^[0-9]+$"
            strFloat = "^[0-9.]+$"
            If gname.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('工序名称不能为空');"
                Exit Sub
            End If
            If guideline.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(guideline.Text.Trim(), strInt, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('指标必须为整数');"
                Exit Sub
            End If
            If unitprice.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(unitprice.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('单价必须为数字');"
                Exit Sub
            End If
            If lowest.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(lowest.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('最低工资必须为数字');"
                Exit Sub
            End If
            If deductRate.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(deductRate.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('扣款率必须为数字');"
                Exit Sub
            End If
            If deductPrice.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(deductPrice.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('扣款金额必须为数字');"
                Exit Sub
            End If
            If wallowance.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(wallowance.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('岗位补贴必须为数字');"
                Exit Sub
            End If
            If wpercent.Text.Trim().Length <> 0 And System.Text.RegularExpressions.Regex.IsMatch(wpercent.Text.Trim(), strFloat, System.Text.RegularExpressions.RegexOptions.Multiline) = False Then
                ltlAlert.Text = "alert('产量限制必须为数字');"
                Exit Sub
            End If
            If wdate.Text.Trim().Length = 0 Then
                ltlAlert.Text = "alert('日期不能为空');"
                Exit Sub
            End If
            If wdate.Text.Trim().Length <> 0 Then
                Try
                    Dim dt As DateTime
                    dt = Convert.ToDateTime(wdate.Text.Trim())
                Catch ex As Exception
                    ltlAlert.Text = "alert('工序名称不能为空');"
                    Exit Sub
                End Try
            End If




            If department.SelectedValue = 0 Then
                ltlAlert.Text = "alert('请选择部门');"
                Exit Sub
            End If
            'new
            Dim type As String
            Query = "Select systemcodename from tcpc0.dbo.systemCode where systemcodeid=" & gcategory.SelectedValue
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While (reader.Read())
                type = reader(0)
            End While
            reader.Close()

            Dim strG As String
            Dim strU As String
            Dim strL As String
            Dim stops As String

            If stopused.Checked Then
                stops = "1"
            Else
                stops = "0"
            End If

            If type = "计件" Then
                If guideline.Text.Trim.Length <= 0 Then
                    ltlAlert.Text = "alert('请输入指标。');"
                    Exit Sub
                End If
                strG = guideline.Text.Trim
                'strU = unitprice.Text.Trim
                strL = "0"
                'Calculate the newprice for workprocedure 

                Query = " Select basicSalary,monthlyAvgDays,basicUnitPrice,dailyHours From BaseInfo where year(workdate)='" & Year(CDate(wdate.Text.Trim)) & "' and  month(workdate)='" & Month(CDate(wdate.Text.Trim)) & "'"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
                While reader.Read
                    price.Text = (reader(2) / CDec(strG) / reader(3)).ToString()
                End While
                reader.Close()
                strU = price.Text
            Else
                'If lowest.Text.Trim.Length <= 0 Then
                '    ltlAlert.Text = "alert('请输入最低工资。');"
                '    Exit Sub
                'End If
                strG = "0"
                strU = "0"
                'strL = lowest.Text
                'Calculate the newprice for workprocedure 
                Query = " Select basicSalary,monthlyAvgDays,basicUnitPrice,dailyHours From BaseInfo where year(workdate)='" & Year(CDate(wdate.Text.Trim)) & "' and  month(workdate)='" & Month(CDate(wdate.Text.Trim)) & "'"
                reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
                While reader.Read
                    price.Text = reader(2).ToString()
                End While
                reader.Close()
                strL = price.Text
            End If
            If Convert.ToDecimal(strU) >= 1000 Then
                ltlAlert.Text = "alert('单价不能大于等于1000');"
                Exit Sub
            End If
            'If Convert.ToDecimal(strL) >= 1000 Then
            '    ltlAlert.Text = "alert('最低工资不能大于等于1000');"
            '    Exit Sub
            'End If

            'Dim pid As Integer = 0
            'Query = " select top 1 id From workprocedure name=N'" & chk.sqlEncode(gname.Text) & "' "
            'pid = SqlHelper.ExecuteScalar(chk.dsnx, CommandType.Text, Query)
            'If pid > 0 Then
            '    ltlAlert.Text = "alert('所输入的名字有重复');"
            '    Exit Sub
            'End If
            Query = "Insert Into workprocedure (typeID,name,usedTimes,guideLine,unitPrice,lowestDailyWage,deductRate, organizationID,deductName,deductPrice,newdeductprice,flag,departmentID,workshopID,wallowance,wpercent,wdate,code,lastone)"
            Query &= " values ( '" & gcategory.SelectedValue & "',N'" & chk.sqlEncode(gname.Text) & "',0,'" & strG & "','" & strU & "','" & strL & "',"
            If deduct.Text.Trim.Length <= 0 Or deductRate.Text.Trim.Length <= 0 Then
                Query &= " null,1,null,null,"
            Else
                If Convert.ToDecimal(deductRate.Text.Trim) >= 100 Then
                    ltlAlert.Text = "alert('扣款率不能大于等于100');"
                    Exit Sub
                End If
                Query &= " '" & deductRate.Text & "',1,'" & deduct.Text & "','" & deductPrice.Text & "',"
            End If
            Query &= " '" & price.Text.ToString() & "','" & stops & "','" & department.SelectedValue & "','" & workshop.SelectedValue & "',"
            If wallowance.Text.Trim = "" Then
                Query &= " null,"
            Else
                Query &= " '" & wallowance.Text.Trim & "',"
            End If
            If wpercent.Text.Trim = "" Then
                Query &= " null,"
            Else
                Query &= " '" & wpercent.Text.Trim & "',"
            End If
            Query &= " '" & wdate.Text.Trim & "',null,1 )  Select @@IDENTITY "

            Dim nid As Integer = 0
            nid = SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.Text, Query)
            If nid > 0 Then
                Query = " Update workprocedure set code='" & nid.ToString & "' where id='" & nid.ToString & "'"
            End If
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            Query = " Update Indicator set workProcedure=workProcedure+1 "
            SqlHelper.ExecuteNonQuery(chk.dsnx, CommandType.Text, Query)

            Response.Redirect(chk.urlRand("/admin/workprocedure.aspx"))
        End Sub
        Sub InitDropDownList()


            Query = "SELECT systemCodeID,systemCodeName From tcpc0.dbo.systemCode s Inner Join tcpc0.dbo.systemCodeType st On st.systemCodeTypeID=s.systemCodeTypeID Where st.systemCodeTypeName='Work Procedure Type' order by s.systemCodeID"
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                gcategory.Items.Add(item)
            End While
            reader.Close()
            gcategory.SelectedIndex = 0
        End Sub
        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            Response.Redirect(chk.urlRand("/admin/workprocedure.aspx"))
        End Sub

        Sub Departmentdropdownlist()

            item = New ListItem("--")
            item.Value = 0
            department.Items.Add(item)
            Query = " SELECT departmentID,Name From Departments WHERE isSalary='1' AND  active='1' order by departmentID "
            reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, Query)
            While reader.Read()
                item = New ListItem(reader(1))
                item.Value = Convert.ToInt32(reader(0))
                department.Items.Add(item)
            End While
            reader.Close()
            department.SelectedIndex = 0
        End Sub

        Private Sub Department_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles department.SelectedIndexChanged
            If department.SelectedValue <> 0 Then
                workshopDropDownList()
            Else
                workshop.Items.Clear()

                item = New ListItem("--")
                item.Value = 0
                workshop.Items.Add(item)
                workshop.SelectedIndex = 0
            End If

        End Sub

        Sub workshopDropDownList()
            Dim dst As DataSet
            workshop.Items.Clear()

            item = New ListItem("--")
            item.Value = 0
            workshop.Items.Add(item)
            workshop.SelectedIndex = 0

            Query = " SELECT w.id, w.name FROM Workshop w INNER JOIN departments d ON w.departmentID = d.departmentID " _
                  & " WHERE d.name=N'" & department.SelectedItem.Text.Trim() & "' AND w.workshopID IS NULL Order by w.code  "

            dst = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, Query)
            With dst.Tables(0)
                If (.Rows.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To .Rows.Count - 1
                        item = New ListItem(.Rows(i).Item(1))
                        item.Value = Convert.ToInt32(.Rows(i).Item(0))
                        workshop.Items.Add(item)
                    Next
                End If
            End With
            dst.Reset()
        End Sub

        Protected Sub wdate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdate.TextChanged

        End Sub
    End Class

End Namespace
