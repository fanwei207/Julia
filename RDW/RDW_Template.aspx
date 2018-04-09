<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_Template.aspx.cs" Inherits="RDW_Template" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" bgcolor="white" border="0" style="width: 830px">
            <tr>
                <td>
                    Template Name<asp:TextBox ID="txtProject" runat="server" Width="234px" TabIndex="1"
                        CssClass="SmallTextBox4"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="dropOwner" runat="server" Width="144px" DataTextField="UserName"
                        DataValueField="UserID" Visible="false">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Query"
                        Width="50px" OnClick="btnQuery_Click" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="9" Text="New Template"
                        Width="80px" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
            <asp:GridView ID="gvRDW" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                Width="950px" CssClass="GridViewStyle" PageSize="20" OnPreRender="gvRDW_PreRender"
                DataKeyNames="RDW_MstrID" OnPageIndexChanging="gvRDW_PageIndexChanging" OnRowCommand="gvRDW_RowCommand"
                OnRowDeleting="gvRDW_RowDeleting" OnRowDataBound="gvRDW_RowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="950px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Template Name" Width="270px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Template Comments" Width="300px" HorizontalAlign="center"></asp:TableCell>                            
                            <asp:TableCell Text="Creator" Width="80px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="CreateDate" Width="80px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Step" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Copy" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Del" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Template Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkProject" runat="server" CommandArgument='<%# Bind("RDW_MstrID") %>'
                                CommandName="myEdit" Font-Bold="False" Font-Size="11px" Font-Underline="True"
                                ForeColor="Black" Text='<%# Bind("RDW_Project") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="230px" />
                        <ItemStyle HorizontalAlign="Left" Width="270px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="RDW_Memo" HeaderText="Template Comments">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="RDW_Leader" HeaderText="Leader" Visible="False">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDW_Creater" HeaderText="Creator">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RDW_CreatedDate" HeaderText="CreateDate">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Step">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnTask" runat="server" Text="Edit" ForeColor="Black" CommandName="editTask"
                                CommandArgument='<%# Bind("RDW_MstrID") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Underline="True" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Copy">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnCopy" runat="server" Text="Copy" ForeColor="Black" CommandName="copyTask"
                                CommandArgument='<%# Bind("RDW_MstrID") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Underline="True" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Del" ForeColor="Black" CommandName="Delete"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Underline="True" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
