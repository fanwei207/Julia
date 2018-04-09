<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MF_mstr.aspx.cs" Inherits="IT_MF_step" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div >
            <table style="width: 850px; height: 5px;">
                <tr>
                    <td>
                        <asp:Label ID="lblstatus" runat="server" Text="status:"></asp:Label>
                        <asp:DropDownList ID="ddlstatus" runat="server">
                            <asp:ListItem Value="2">--</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">PUBLIC</asp:ListItem>
                            <asp:ListItem Value="1">PRIVATE</asp:ListItem>
                        </asp:DropDownList>
                        Keywords:
                        <asp:TextBox ID="txtkeywords" runat="server" CssClass="SmallTextBox" Height="20px"
                            Width="250px" />
                        &nbsp;<asp:Button ID="btn_messageselect" runat="server" Text="Query" 
                            CssClass="SmallButton2" onclick="btn_messageselect_Click" Width="50px"
                             />
                    </td>
                    <td style="text-align: right;">
                       
                        <asp:Button ID="btn_new" runat="server" Text="NEW" CssClass="SmallButton2" 
                            onclick="btn_new_Click" Width="35px"  />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                AllowPaging="True" Width="1100px" DataKeyNames="FM_id"
                PageSize="22" onpageindexchanging="gv_PageIndexChanging" 
                onrowcommand="gv_RowCommand">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Owner" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Message" Width="610px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="NO.">
                        <ItemTemplate>
                          
                            <asp:Label ID="lblid" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate>
                         <asp:LinkButton ID="reply" runat="server" Font-Bold="False" Font-Size="11px" CommandName="detail"
                                Font-Underline="True"  Text='<%# Bind("FM_title") %>' Style="padding-left: 5px;"></asp:LinkButton>
                                </br>&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("FM_Decription") %>'></asp:Label>
                          
                           
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="600px" />
                        <ItemStyle HorizontalAlign="Left" Width="600px" VerticalAlign="Top" />
                    </asp:TemplateField>

                    

                     <asp:BoundField DataField="FM_Authorize" HeaderText="Authorize">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>

                     <asp:TemplateField HeaderText="Author">
                        <ItemTemplate>
                          
                            <asp:Label ID="lblcreateName" runat="server" Text='<%# Bind("FM_createName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                    </asp:TemplateField>

                 

                     <asp:BoundField DataField="FM_Totalread" HeaderText="Total Read">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>

                     <asp:TemplateField HeaderText="Last Visit">
                        <ItemTemplate>
                          
                            <asp:Label ID="lblvisitName" runat="server" Text='<%# Bind("FM_visitName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Last Review">
                        <ItemTemplate>
                          
                            <asp:Label ID="lblreviewName" runat="server" Text='<%# Bind("FM_reviewName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                    </asp:TemplateField>
                  
                    
                </Columns>
            </asp:GridView>
          
            <br />
        </div>
    </form>
</body>
</html>

