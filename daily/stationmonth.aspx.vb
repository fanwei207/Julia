Imports adamFuncs
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace tcpc

Partial Class stationmonth
        Inherits BasePage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Dim chk As New adamClass

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

    Private Sub BindData()
        Dim query As String
        Dim ds As DataSet
        Dim i As Integer
        Dim g As Integer = 1
        query = " Select s.createdDate,s.quantity,s.isOut,isnull(c.comments,'0') as price "
        query &= " From Stationery s INNER JOIN tcpc0.dbo.systemCode c ON s.stationeryTypeID=c.systemCodeID "
        query &= " INNER JOIN tcpc0.dbo.systemCodeType st ON c.systemCodeTypeID=st.systemCodeTypeID and st.systemCodeTypeName='Stationery Type' "
        query &= " Where  s.organizationID=" & Session("orgID") & " Order by s.createdDate desc"
      
        ds = SqlHelper.ExecuteDataset(chk.dsnx, CommandType.Text, query)

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("gsort", System.Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("inDate", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("intotal", System.Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("outtotal", System.Type.GetType("System.String")))
        'dt.Columns.Add(New DataColumn("preQty", System.Type.GetType("System.Int32")))
        Dim datetemp As Integer
        Dim instation As Decimal = 0
        Dim outstation As Decimal = 0
        Dim flag As Boolean = False
        Dim date1 As DateTime
        Dim j As Integer

        With ds.Tables(0)
            Dim dr1 As DataRow
            If (.Rows.Count > 0) Then
                For i = 0 To .Rows.Count - 1
                    If (.Rows(i).Item(0).month) <> datetemp Then
                        datetemp = .Rows(i).Item(0).month
                        If flag = True Then
                            dr1.Item("intotal") = instation.ToString()
                            dr1.Item("outtotal") = outstation.ToString()
                            dt.Rows.Add(dr1)
                            instation = 0
                            outstation = 0
                            g = g + 1
                        End If
                        If i = 0 Then
                            j = DateTime.Now.Month
                            While (j > datetemp)
                                dr1 = dt.NewRow()
                                dr1.Item("gsort") = g
                                dr1.Item("inDate") = .Rows(i).Item(0).year & "-" & j.ToString()
                                dr1.Item("intotal") = instation.ToString()
                                dr1.Item("outtotal") = outstation.ToString()
                                dt.Rows.Add(dr1)
                                g = g + 1
                                j = j - 1
                            End While
                        Else
                            j = CInt(.Rows(i - 1).Item(0).month) - 1
                            While (j > datetemp)
                                dr1 = dt.NewRow()
                                dr1.Item("gsort") = g
                                dr1.Item("inDate") = .Rows(i).Item(0).year & "-" & j.ToString()
                                dr1.Item("intotal") = instation.ToString()
                                dr1.Item("outtotal") = outstation.ToString()
                                dt.Rows.Add(dr1)
                                g = g + 1
                                j = j - 1
                            End While
                        End If


                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = g
                        date1 = .Rows(i).Item(0)
                        dr1.Item("inDate") = Format(date1, "yyyy-MM")
                    End If
                    If .Rows(i).Item("isOut") = False Then
                        instation = instation + CDec(.Rows(i).Item("quantity")) * CDec(.Rows(i).Item("price"))
                    Else
                        outstation = outstation + CDec(.Rows(i).Item("quantity")) * CDec(.Rows(i).Item("price"))
                    End If

                    flag = True
                Next
                If flag = True Then
                    dr1.Item("intotal") = instation.ToString()
                    dr1.Item("outtotal") = outstation.ToString()
                    dt.Rows.Add(dr1)
                End If

                If .Rows(.Rows.Count - 1).Item(0).month <> 1 Then
                    Dim n As Integer
                    n = CInt(.Rows(.Rows.Count - 1).Item(0).month) - 1
                    g = g + 1
                    While (n >= 1)
                        dr1 = dt.NewRow()
                        dr1.Item("gsort") = g
                        dr1.Item("inDate") = .Rows(.Rows.Count - 1).Item(0).year & "-" & n.ToString()
                        dr1.Item("intotal") = 0
                        dr1.Item("outtotal") = 0
                        dt.Rows.Add(dr1)
                        g = g + 1
                        n = n - 1
                    End While
                End If

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

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub
   
End Class

End Namespace
