'*@     Create By   :   Ye Bin    
'*@     Create Date :   2007-6-12
'*@     Modify By   :   NA
'*@     Modify Date :   NA
'*@     Function    :   NA
Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace tcpc
Partial Class TranQtyAmount
        Inherits BasePage 
    Public chk As New adamClass
    Dim nRet As Integer
    Dim dst As DataSet
    Dim strsql As String

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
            month.SelectedValue = CInt(DateTime.Now().Month)
            year.Text = DateTime.Now().Year
            BindData()
        End If
    End Sub

    Sub BindData()
        strsql = " Select Convert(char(10), enterdate, 101 ), userName, userno, Sum(qty) From ( " _
               & " Select pt.createdDate As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc1.dbo.Part_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.createdDate, u.userName, u.userNo, pt.createdDate " _
               & " Having Year(pt.createdDate)='" & year.Text.Trim() & "' And Month(pt.createdDate)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.enter_date As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc1.dbo.product_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.enter_date, u.userName, u.userNo, pt.enter_date " _
               & " Having Year(pt.enter_date)='" & year.Text.Trim() & "' And Month(pt.enter_date)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.createdDate As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc2.dbo.Part_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.createdDate, u.userName, u.userNo, pt.createdDate" _
               & " Having Year(pt.createdDate)='" & year.Text.Trim() & "' And Month(pt.createdDate)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.enter_date As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc2.dbo.product_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.enter_date, u.userName, u.userNo, pt.enter_date " _
               & " Having Year(pt.enter_date)='" & year.Text.Trim() & "' And Month(pt.enter_date)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select Convert(char(10), pt.createdDate, 101) As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc3.dbo.Part_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.createdDate, u.userName, u.userNo, pt.createdDate " _
               & " Having Year(pt.createdDate)='" & year.Text.Trim() & "' And Month(pt.createdDate)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.enter_date As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc3.dbo.product_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.enter_date, u.userName, u.userNo, pt.enter_date " _
               & " Having Year(pt.enter_date)='" & year.Text.Trim() & "' And Month(pt.enter_date)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.createdDate As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc4.dbo.Part_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.createdDate, u.userName, u.userNo, pt.createdDate " _
               & " Having Year(pt.createdDate)='" & year.Text.Trim() & "' And Month(pt.createdDate)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.enter_date As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc4.dbo.product_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.enter_date, u.userName, u.userNo, pt.enter_date " _
               & " Having Year(pt.enter_date)='" & year.Text.Trim() & "' And Month(pt.enter_date)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.createdDate As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc5.dbo.Part_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.createdDate, u.userName, u.userNo, pt.createdDate " _
               & " Having Year(pt.createdDate)='" & year.Text.Trim() & "' And Month(pt.createdDate)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.enter_date As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc5.dbo.product_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.enter_date, u.userName, u.userNo, pt.enter_date " _
               & " Having Year(pt.enter_date)='" & year.Text.Trim() & "' And Month(pt.enter_date)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.createdDate As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc6.dbo.Part_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.createdDate, u.userName, u.userNo, pt.createdDate " _
               & " Having Year(pt.createdDate)='" & year.Text.Trim() & "' And Month(pt.createdDate)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select  pt.enter_date As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc6.dbo.product_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.enter_date, u.userName, u.userNo, pt.enter_date " _
               & " Having Year(pt.enter_date)='" & year.Text.Trim() & "' And Month(pt.enter_date)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.createdDate As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc7.dbo.Part_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.createdDate, u.userName, u.userNo, pt.createdDate " _
               & " Having Year(pt.createdDate)='" & year.Text.Trim() & "' And Month(pt.createdDate)='" & month.SelectedValue & "'" _
               & " Union " _
               & " Select pt.enter_date As enterdate, u.userName, u.userNo, Count(*) As qty " _
               & " From tcpc7.dbo.product_tran pt " _
               & " Inner Join tcpc0.dbo.Users u ON u.userID = pt.createdBy And u.plantCode='" & Session("PlantCode") & "'"
        If txtUserNo.Text.Trim().Length > 0 Then
            strsql &= " And u.userno='" & txtUserNo.Text.Trim() & "'"
        End If
        strsql &= " Group By pt.enter_date, u.userName, u.userNo, pt.enter_date " _
               & " Having Year(pt.enter_date)='" & year.Text.Trim() & "' And Month(pt.enter_date)='" & month.SelectedValue & "'" _
               & " ) Derivedtbl " _
               & " Group By Convert(char(10), enterdate, 101 ),userName, userno " _
               & " Order By userno, Convert(char(10), enterdate, 101 ) "
        'Response.Write(strsql)
        'exit Sub 

        Session("EXSQL") = strsql
        Session("EXHeader") = "/Purchase/TranQtyAmountPrint.aspx?year=" & year.Text.Trim() & "&month=" & month.SelectedItem.Text.Trim() & "&uc=" & txtUserNo.Text.Trim() & "^~^"
        Session("EXTitle") = strsql

        dst = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, strsql)

        Dim dtl As New DataTable
        dtl.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dtl.Columns.Add(New DataColumn("userno", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("userName", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("enter_date", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("enterdate", System.Type.GetType("System.DateTime")))
        dtl.Columns.Add(New DataColumn("account", System.Type.GetType("System.String")))
        dtl.Columns.Add(New DataColumn("qty", System.Type.GetType("System.Int32")))

        With dst.Tables(0)
            If (.Rows.Count > 0) Then
                lblCount.Text = "数量: " & .Rows.Count.ToString()
                Dim i As Integer
                Dim drow As DataRow
                For i = 0 To .Rows.Count - 1
                    drow = dtl.NewRow()
                    drow.Item("gsort") = i + 1
                    drow.Item("userno") = .Rows(i).Item(2).ToString().Trim()
                    drow.Item("userName") = .Rows(i).Item(1).ToString().Trim()
                    drow.Item("enter_date") = Format(CDate(.Rows(i).Item(0)), "yyyy-MM-dd")
                    drow.Item("enterdate") = .Rows(i).Item(0)
                    drow.Item("account") = Format(.Rows(i).Item(3), "#,##0")
                    drow.Item("qty") = .Rows(i).Item(3)
                    dtl.Rows.Add(drow)
                Next
            End If
        End With
        Dim dvw As DataView
        dvw = New DataView(dtl) 
            Session("orderby") = "gsort"

            Try
                dvw.Sort = Session("orderby") & Session("orderdir")
                dgList.DataSource = dvw
                dgList.DataBind()
            Catch
            End Try
        End Sub

    Private Sub dgList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
        dgList.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Private Sub dgList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgList.SortCommand
        Session("orderby") = e.SortExpression.ToString()
        If Session("orderdir") = " ASC" Then
            Session("orderdir") = " DESC"
        Else
            Session("orderdir") = " ASC"
        End If
        BindData()
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        dgList.CurrentPageIndex = 0
        BindData()
    End Sub
End Class

End Namespace
