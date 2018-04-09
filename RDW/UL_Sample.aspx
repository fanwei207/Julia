<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UL_Sample.aspx.cs" Inherits="RDW_UL_Sample" %>

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
                <table width="1000">
                    <tr>
                        <td>
                            产品规格:<asp:TextBox ID="txtProject" runat="server" Width="300px" CssClass="SmallTextBox"
                        TabIndex="1" Enabled="False"></asp:TextBox>
                        </td>

                        <td>
                            运号单:<asp:TextBox ID="txtbsd" runat="server" Width="200px" CssClass="SmallTextBox"
                        TabIndex="1" ></asp:TextBox>
                        </td>
                        <td>
                            送样时间:<asp:TextBox ID="txtDate1" runat="server" Width="100px" CssClass="SmallTextBox Date"
                        TabIndex="1"></asp:TextBox>
                        </td>
                        <td>
                           
                         <td width="150">
                           &nbsp;&nbsp; <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="9" Text="save"
                        Width="50px" CausesValidation="false"   />
                       
                           &nbsp;&nbsp; <asp:Button ID="btnback" runat="server" CssClass="SmallButton2" TabIndex="9" Text="back"
                        Width="50px" CausesValidation="false" OnClick="btnback_Click"   />
                        </td>
                    </tr>

                </table>

    <asp:GridView ID="gvRDW" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle" PageSize="20" 
                Width="980px" DataKeyNames="bsd_nbr" 
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
                     <asp:BoundField DataField="bsd_nbr" HeaderText="运单号" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>

                     <asp:BoundField DataField="bsd_receiptName" HeaderText="接收方" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>

                   <asp:BoundField DataField="bsd_shipto" HeaderText="送达地" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>

                      <asp:BoundField DataField="bsd_rmks" HeaderText="备注" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>

                     <asp:BoundField DataField="userName" HeaderText="寄送人" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>

                     <asp:BoundField DataField="bsd_sendeddate" HeaderText="寄送时间" HtmlEncode="False">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
    
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
