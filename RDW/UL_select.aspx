<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UL_select.aspx.cs" Inherits="RDW_UL_select" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 163px;
        }
    </style>
</head>
<body>
     <div>
          <form id="Form1" method="post" runat="server">
                <table>
                    <tr>
                        <td>
                            产品规格:<asp:TextBox ID="txtProject" runat="server" Width="300px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                        </td>
                        <td>
                            完成时间:<asp:TextBox ID="txtDate1" runat="server" Width="100px" CssClass="SmallTextBox Date"
                        TabIndex="1"></asp:TextBox>-
                            <asp:TextBox ID="txtDate2" runat="server" Width="100px" CssClass="SmallTextBox Date"
                        TabIndex="1"></asp:TextBox>
                        </td>
                        <td>
                            UL Model:<asp:TextBox ID="txtModel" runat="server" Width="100px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                            <asp:Label ID="lblmid" runat="server" Text="" Visible ="false"></asp:Label>
                        </td>
                         <td>
                           &nbsp;&nbsp; <asp:Button ID="btnSelect" runat="server" CssClass="SmallButton2" TabIndex="9" Text="Query"
                        Width="50px" CausesValidation="false" OnClick="btnSelect_Click"  />

                             
                              &nbsp;&nbsp; <asp:Button ID="btnEXCEL" runat="server" CssClass="SmallButton2" TabIndex="9" Text="EXCEL"
                        Width="50px" CausesValidation="false" OnClick="btnEXCEL_Click"  />
                        </td>
                    </tr>

                </table>

    <asp:GridView ID="gvRDW" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle" PageSize="20" 
                Width="980px" DataKeyNames="UL_msrtID,UL_bsdnbr,UL_bsddate,UL_id"  AllowPaging="True" OnPageIndexChanging="gvRDW_PageIndexChanging"  
               >
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="No." Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Description" Width="370px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="StartDate" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="EndDate" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Complete" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Duration" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <%--  <asp:TableCell Text="Pre" Width="40px" HorizontalAlign="center"></asp:TableCell>--%>
                            <asp:TableCell Text="Member" Width="130px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Approver" Width="130px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                     <asp:BoundField DataField="UL_Project" HeaderText="产品规格" HtmlEncode="False">
                        <HeaderStyle Width="400px" HorizontalAlign="Center" />
                        <ItemStyle Width="400px" HorizontalAlign="Left" />
                    </asp:BoundField>

                      <asp:BoundField DataField="UL_bsddate" HeaderText="样品寄出日期" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>


                      <asp:BoundField DataField="UL_bsdnbr" HeaderText="出运单号" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                      <asp:BoundField DataField="UL_docDate" HeaderText="文件发出日期" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>
                  
                     <asp:BoundField DataField="UL_model" HeaderText="UL Model" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                     <asp:BoundField DataField="UL_FinDate" HeaderText="UL完成时间" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>

                    
                </Columns>
            </asp:GridView>
    
        &nbsp;&nbsp;&nbsp;
    
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>

